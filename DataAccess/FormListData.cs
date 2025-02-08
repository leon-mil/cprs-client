/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.FormListData.cs	    	
Programmer:         Christine Zhang
Creation Date:      9/28/2021
Inputs:             
Parameters:	        None 
Outputs:	        Form list data	
Description:	    data layer to list form list and update it
Detailed Design:    None 
Other:	            Called by: frmAdminFormList
 
Revision History:	
***************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
****************************************************************************************/
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
    public class FormListData
    {
        public DataTable GetFormlist()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT f.Respid, resporg, f.usrnme, f.prgdtm from dbo.formlist f, respondent r WHERE f.respid = r.respid";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);

                    ds.Fill(dt);
                }
            }
            return dt;

        }

        public bool CheckExistinFormlist(string respid)
        {
            bool respid_exist = false;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = @"Select * from dbo.Formlist where respid = '" + respid + "'";

                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            respid_exist = true;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return respid_exist;
        }

        public void AddFormlist(string respid)
        {
            /* find out database table name */
            string db_table = "dbo.Formlist";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (RESPID, USRNME, PRGDTM) "
                        + "Values (@RESPID, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@RESPID", respid);
            insert_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
            insert_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

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

        public void DeleteFormlist(string respid)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.Formlist WHERE RESPID = @RESPID", sql_connection);

                    sql_command.Parameters.Add("@RESPID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(respid);

                    //Open the connection.
                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        //Execute the query.
                        sql_command.ExecuteNonQuery();
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close(); //close database connection
                }
            }
        }
    }
}
