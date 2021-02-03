/***********************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.RespAuditData.cs	    	

Programmer      : Christine Zhang

Creation Date   : 09/09/2015

Inputs          : rspaudit

Parameters      : None 

Outputs         : data table

Description     : 	   

Detailed Design : None 

Other           : Called by: frmRespAudit.cs
 
Revision History:	
***********************************************************************************
Modified Date   : 8/27/2015 
Modified By     : Diane Musachio
Keyword         : 20150827dm 
Change Request  : None
Description     : Add routine AddRespAuditData 
***********************************************************************************/
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
    public class RespAuditData
    {
        public DataTable GetRspAudits(string respid, string varnme, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT RESPID, VARNME, OLDVAL, NEWVAL,  USRNME, PRGDTM FROM dbo.RSPAUDIT";
                if (respid != "")
                    sql = sql + " where respid = " + GeneralData.AddSqlQuotes(respid);

                if (varnme != "")
                    sql = sql + " where VARNME =" + GeneralData.AddSqlQuotes(varnme);
                if (usrnme != "")
                    sql = sql + " where USRNME =" + GeneralData.AddSqlQuotes(usrnme);
                if (prgdtm != "")
                    sql = sql + " where Convert(varchar, PRGDTM, 101) = " + GeneralData.AddSqlQuotes(prgdtm);

                sql = sql + " order by PRGDTM DESC";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, sql_connection))
                {
                    da.Fill(dt);
                }
            }
                
            return dt;
        }

        /*Get data to setup value combobox in resp audit screen */

        public DataTable GetValueList(int cbIndex)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();

                if (cbIndex == 1)
                    sql = "Select distinct varnme from dbo.rspaudit order by varnme";
                else if (cbIndex == 2)
                    sql = "select distinct usrnme from dbo.rspaudit order by usrnme";

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

        public void AddRespauditData(RespAudit ra)
        //public void AddRespauditData(string respid, string varnme, string oldval, string newval, string usrnme, DateTime prgdtm) 
        {
            if (ra.Newval.Trim() == ra.Oldval.Trim()) return;

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.rspaudit (respid, varnme, oldval, newval, usrnme, prgdtm)"
                            + " Values (@RESPID, @VARNME, @OLDVAL, @NEWVAL, @USRNME, @PRGDTM)";

            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@RESPID", ra.Respid);
            insert_command.Parameters.AddWithValue("@VARNME", ra.Varnme);
            insert_command.Parameters.AddWithValue("@OLDVAL", ra.Oldval.Trim());
            insert_command.Parameters.AddWithValue("@NEWVAL", ra.Newval.Trim());
            insert_command.Parameters.AddWithValue("@USRNME", ra.Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", ra.Prgdtm);

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

    }
}
