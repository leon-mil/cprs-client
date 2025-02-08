/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.MonthlyAuditData.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/20/2016
Inputs:             auditrvw record
Parameters:	        None 
Outputs:	        None
Description:	    data layer to add monthly audit data
Detailed Design:    None 
Other:	            Called by: frmMonthlyAuditReview
 
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
    public class MonthlyAuditReviewData
    {
        //get data from auditrvw table
        public DataTable GetMonthlyAuditReviewTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select statp, usrnme, prgdtm, case when activity = 'N' then 'Normal' else 'Unusual' end as 'ACT', commtext From dbo.auditrvw ORDER BY statp desc", sql_connection);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
               
            }

            return dt;
        }

        // Get current Month audit Review
        public void GetCurrentMonthAuditReview(string statp, ref string activity, ref string comment)
        {
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT activity,commtext FROM dbo.auditrvw WHERE statp = " + GeneralData.AddSqlQuotes(statp);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        activity = reader["ACTIVITY"].ToString();
                        comment = reader["COMMTEXT"].ToString();
                    }
                }
                
                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            
        }

        //add new record to auditrvw table
        public void AddCurrentAuditData(string statp, string activity, string commtext)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.auditrvw (statp, activity, commtext, usrnme, prgdtm)"
                            + "Values (@statp, @activity, @commtext, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@statp", statp);
            insert_command.Parameters.AddWithValue("@activity", activity);
            insert_command.Parameters.AddWithValue("@commtext", commtext);
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

        //update current audit record
        public DataTable UpdateCurrentAuditData(string statp, string activity, string commtext)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.auditrvw  SET activity = @activity, commtext = @commtext, usrnme =@usrnme, prgdtm =@prgdtm WHERE [statp] = @statp", sql_connection);

                    sql_command.Parameters.AddWithValue("@statp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(statp);
                    sql_command.Parameters.AddWithValue("@activity", activity);
                    sql_command.Parameters.AddWithValue("@commtext", commtext);
                    sql_command.Parameters.AddWithValue("@usrnme", UserInfo.UserName);
                    sql_command.Parameters.AddWithValue("@Prgdtm", DateTime.Now);

                    //Open the connection.
                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable
                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        //Execute the query.
                        sql_command.ExecuteNonQuery();
                        da.Fill(dt);
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

                return dt;
            }

        }
    }
}
