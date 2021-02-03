/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : ProjectAuditData.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : datasets:CPRAUDIT, VIPAUDIT database, 
Outputs       : data tables
Description   : get project audit data from audit tables and store precedures
Change Request: 
Detail Design :   
Rev History   : See Below
Other         : called by frmProjectAudit
 ***********************************************************************
Modified Date : Jan 10, 2018
Modified By   : Christine Zhange
Keyword       :
Change Request: Defect fixed 247
Description   : No Longer use Stored procedure
***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using CprsBLL;

namespace CprsDAL
{
    public class ProjectAuditData
    {
        /*Get project Item audit data */
        public DataTable GetProjectItemAudits(string survey, string id, string newtc, string varnme, string usrnme, string prgdtm)
        {  
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "Select v.ID, OWNER, NEWTC, VARNME, OLDVAL, OLDFLAG, NEWVAL, NEWFLAG, USRNME, PRGDTM From dbo.CPRAUDIT v, dbo.SAMPLE s, dbo.MASTER m  where v.ID = S.ID and S.MASTERID = m.MASTERID";
                if (id != "")
                    sql = sql + " and v.Id = " + GeneralData.AddSqlQuotes(id);
                if (survey != "")
                {
                    if (survey == "S")
                        sql = sql + " and owner in ('S', 'L', 'P')";
                    else if (survey == "F")
                        sql = sql + " and owner in ('C', 'D', 'F')";
                    else
                        sql = sql + " and owner = " + GeneralData.AddSqlQuotes(survey);
                }
                if (newtc != "")
                    sql = sql + " and NEWTC =" + newtc;
                if (varnme != "")
                    sql = sql + " and VARNME =" + GeneralData.AddSqlQuotes(varnme);
                if (usrnme != "")
                    sql = sql + " and USRNME =" + GeneralData.AddSqlQuotes(usrnme);
                if (prgdtm != "")
                    sql = sql + " and Convert(varchar, PRGDTM, 101) = " + GeneralData.AddSqlQuotes(prgdtm);

                sql = sql + " order by PRGDTM DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, sql_connection))
                {
                    da.Fill(dt);
                }
            }

            return dt;

        }

        /*Get project Vip audit data */
        public DataTable GetProjectVipAudits(string survey, string id, string newtc, string date6, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                    string sql = "Select v.ID, OWNER, NEWTC, DATE6, OLDVIP, OLDFLAG, NEWVIP, NEWFLAG, USRNME, PRGDTM From dbo.VIPAUDIT v, dbo.SAMPLE s, dbo.MASTER m  where v.ID = S.ID and S.MASTERID = m.MASTERID";
                    if (id != "")
                        sql = sql + " and v.Id = " + GeneralData.AddSqlQuotes(id);
                    if (survey != "")
                    {
                        if (survey == "S")
                            sql = sql + " and owner in ('S', 'L', 'P')";
                        else if (survey == "F")
                            sql = sql + " and owner in ('C', 'D', 'F')";
                        else
                            sql = sql + " and owner = " + GeneralData.AddSqlQuotes(survey);
                    }
                    if (newtc != "")
                        sql = sql + " and NEWTC =" + newtc;
                    if (date6 != "")
                        sql = sql + " and date6 =" + date6;
                    if (usrnme != "")
                        sql = sql + " and USRNME =" + GeneralData.AddSqlQuotes(usrnme);
                    if (prgdtm != "")
                        sql = sql + " and Convert(varchar, PRGDTM, 101) = " + GeneralData.AddSqlQuotes(prgdtm);

                sql = sql + " order by PRGDTM DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, sql_connection))
                    {
                        da.Fill(dt);
                    }
                  
            }

            return dt;
        }

        /*Get data to setup value combobox in project audit screen */
        public  DataTable GetValueList(int tabIndex, int cbIndex)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();
                if (tabIndex == 1)
                {
                    if (cbIndex == 1)
                        
                        sql = "Select DISTINCT NEWTC From dbo.CPRAUDIT v, dbo.SAMPLE s, dbo.MASTER m  where v.ID=S.ID and S.MASTERID = m.MASTERID order by newtc ";
                    else if (cbIndex == 2)
                        sql = "Select distinct varnme from dbo.cpraudit";
                    else if (cbIndex == 3)
                        sql = "select distinct usrnme from dbo.cpraudit";
                }
                else if (tabIndex == 2)
                    if (cbIndex == 1)
                        sql = "Select DISTINCT NEWTC From dbo.VIPAUDIT v, dbo.SAMPLE s, dbo.MASTER m  where v.ID=S.ID and S.MASTERID = m.MASTERID order by newtc ";
                    else if (cbIndex == 3)
                        sql = "Select distinct usrnme from dbo.vipaudit";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, c))
                {
                    da.Fill(dt);

                    DataRow dr = dt.NewRow();
                    dr[0] = " ";
                    
                    dt.Rows.InsertAt(dr, 0);
                }
                c.Close();
            }

            return dt;
        }

        /*Add cpr audit record */
        public void AddCprauditData(Cpraudit ca)
        {
            if (ca.Oldval == ca.Newval & ca.Oldflag == ca.Newflag) return;

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.cpraudit (id, varnme, oldval, oldflag, newval, newflag, usrnme, prgdtm)"
                            + "Values (@id, @VARNME, @OLDVAL, @OLDFLAG, @NEWVAL, @NEWFLAG, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@id", ca.Id);
            insert_command.Parameters.AddWithValue("@VARNME", ca.Varnme);
            insert_command.Parameters.AddWithValue("@OLDVAL", ca.Oldval);
            insert_command.Parameters.AddWithValue("@OLDFLAG", ca.Oldflag);
            insert_command.Parameters.AddWithValue("@NEWVAL", ca.Newval);
            insert_command.Parameters.AddWithValue("@NEWFLAG", ca.Newflag);
            insert_command.Parameters.AddWithValue("@USRNME", ca.Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", ca.Progdtm);

            try
            {
                sql_connection.Open();
                insert_command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;

            }
            finally
            {
                sql_connection.Close();
            }

        }

        /*Add vip audit record */
        public void AddVipauditData(Vipaudit ca)
        {
            if (ca.Oldvip == ca.Newvip & ca.Oldflag == ca.Newflag) return;

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.vipaudit (id, date6, oldvip, oldflag, newvip, newflag, usrnme, prgdtm)"
                            + "Values (@id,  @DATE6, @OLDVIP, @OLDFLAG, @NEWVIP, @NEWFLAG, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@id", ca.Id);
            insert_command.Parameters.AddWithValue("@DATE6", ca.date6);
            insert_command.Parameters.AddWithValue("@OLDVIP", ca.Oldvip);
            insert_command.Parameters.AddWithValue("@OLDFLAG", ca.Oldflag);
            insert_command.Parameters.AddWithValue("@NEWVIP", ca.Newvip);
            insert_command.Parameters.AddWithValue("@NEWFLAG", ca.Newflag);
            insert_command.Parameters.AddWithValue("@USRNME", ca.Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", ca.Progdtm);

            try
            {
                sql_connection.Open();
                insert_command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;

            }
            finally
            {
                sql_connection.Close();
            }

        }

        /*Add Respondent audit record */
        public void AddRspauditData(Respaudit ra)
        {
            if (ra.Oldval == ra.Newval) return;

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.rspaudit (respid, varnme, oldval, newval, usrnme, prgdtm)"
                            + "Values (@RESPID, @VARNME, @OLDVAL, @NEWVAL, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@respid", ra.Respid);
            insert_command.Parameters.AddWithValue("@VARNME", ra.Varnme);
            insert_command.Parameters.AddWithValue("@OLDVAL", ra.Oldval);
            insert_command.Parameters.AddWithValue("@NEWVAL", ra.Newval);
            insert_command.Parameters.AddWithValue("@USRNME", ra.Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", ra.Progdtm);

            try
            {
                sql_connection.Open();
                insert_command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;

            }
            finally
            {
                sql_connection.Close();
            }

        }

        /*Retrieve projaudit data */
        public DataTable GetProjectAuditDataForID(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT VARNME, OLDVAL, OLDFLAG, NEWVAL, NEWFLAG, USRNME, PRGDTM FROM dbo.CPRAUDIT WHERE id = " + GeneralData.AddSqlQuotes(id) + " order by PRGDTM DESC";
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }


        /*Retrieve Vipaudit data */
        public DataTable GetVIPAuditDataForID(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT DATE6, OLDVIP, OLDFLAG, NEWVIP, NEWFLAG, USRNME, PRGDTM FROM dbo.VIPAUDIT WHERE id = " + GeneralData.AddSqlQuotes(id) + " order by PRGDTM DESC"; ;
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }


        /*Retrieve Resp audit data */
        public DataTable GetRespAuditDataForID(string respid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT VARNME, OLDVAL, NEWVAL, USRNME, PRGDTM FROM dbo.RSPAUDIT WHERE respid = " + GeneralData.AddSqlQuotes(respid) + " order by PRGDTM DESC"; ;
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }


    }
}
