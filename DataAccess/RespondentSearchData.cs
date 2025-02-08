
/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : RespondentSearchData.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frmRespondent.cs
Change Request: 
Specification : Respondent Search Specifications  
Rev History   : See Below

Other         : N/A
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace CprsDAL
{
    public class RespondentSearchData
    {
        /*Get Respondent Data */
        public DataTable GetRespondentSearchData(string respid, string owner1, string owner2, string owner3, string contact1, string contact2, string contact3, string phone, string rstate, bool extact)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sp_name = "dbo.sp_RespSearch";
                if (extact)
                    sp_name = "dbo.sp_RespSearchExact";
                
                SqlCommand sql_command = new SqlCommand(sp_name, sql_connection);
                sql_command.CommandTimeout = 0;
                sql_command.CommandType = CommandType.StoredProcedure;
                
                sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);
                sql_command.Parameters.Add("@OWNER1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner1);
                sql_command.Parameters.Add("@OWNER2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner2);
                sql_command.Parameters.Add("@OWNER3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner3);
                sql_command.Parameters.Add("@CONTACT1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact1);
                sql_command.Parameters.Add("@CONTACT2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact2);
                sql_command.Parameters.Add("@CONTACT3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact3);
                sql_command.Parameters.Add("@PHONE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(phone);
                sql_command.Parameters.Add("@RSTATE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(rstate);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        /*Get Project data for a respid */
        public DataTable GetProjectData(string respid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                
                SqlCommand sql_command = new SqlCommand("dbo.sp_SampleMasterSocByRespID", sql_connection);

                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            
            return dt;

        }

        public DataTable GetProjectDataForNPC(string respid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_SampleMasterSocByRespIDForNPC", sql_connection);

                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        public DataTable GetProjectEmptyTable()
        {
            // Here we create a DataTable with four columns.

            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("FIN", typeof(string));
            table.Columns.Add("RESPID", typeof(string));
            table.Columns.Add("STATUS", typeof(string));
            table.Columns.Add("OWNER", typeof(string));
            table.Columns.Add("NEWTC", typeof(string));
            table.Columns.Add("FIPSTATE", typeof(char));
 
            table.Columns.Add("SELDATE", typeof(string));
            table.Columns.Add("PROJSELV", typeof(int));
            table.Columns.Add("STRTDATE", typeof(string));
            table.Columns.Add("COMPDATE", typeof(string));
            table.Columns.Add("FUTCOMPD", typeof(string));
            table.Columns.Add("RVITM5C", typeof(int));
            table.Columns.Add("FWGT", typeof(float));
            table.Columns.Add("RBLDGS", typeof(int));
            table.Columns.Add("RUNITS", typeof(int));
            

            return table;
        }

        public DataTable GetRespondentEmptyTable()
        {
            // Here we create a DataTable with four columns.

            DataTable table = new DataTable();
            table.Columns.Add("RESPID", typeof(string));
            table.Columns.Add("ORGANIZATION", typeof(string));
            table.Columns.Add("CONTACT", typeof(string));
            table.Columns.Add("PHONE", typeof(string));
            table.Columns.Add("RSTATE", typeof(string));
            table.Columns.Add("SURVEYS", typeof(char));

            return table;
        }

        public  DataTable GetRstateData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_Rstate", sql_connection);

                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr[0] = -1;
                dr[1] = "";
                dt.Rows.InsertAt(dr, 0);
            }

            return dt;

           }
    }
}
