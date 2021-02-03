/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.VIPBenchmarkProcessData.cs
Programmer:         Diane Musachio
Creation Date:      4/2/2019
Inputs:            
Parameters:	        year
Outputs:	      
Description:	    data layer to set start and end date for series
                    processing
Detailed Design:    None
Other:	            Called by: frmBenRunBCF.cs

Revision History:	
***************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
****************************************************************************************/
using System.Data;
using System.Data.SqlClient;
using System;
using CprsBLL;

namespace CprsDAL
{
    public class VipBenchmarkProcessData
    {
        /*Get latest date if get the info return true, otherwise return false */
        public bool GetStatInfo(string year)
        {
            bool date_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT YEAR FROM dbo.BenchPrcss where YEAR = @CURRYR";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CURRYR", year);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            date_exist = true;
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }

            return date_exist;
        }

        // If current year does not exist, insert it into the table
        public void InsertRow(string year)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand(@"INSERT INTO dbo.BenchPrcss (year, 
                        Task01A, Task01B, Task02A, Task02B, Task03A, Task03B,Task04A, Task04B,
                        Task05A, Task05B, Task06A, Task06B, Task07A, Task07B,Task08A, Task08B) 
                        VALUES (@YEAR, '','','','','','','','','','','','','','','','')", sql_connection);
                    sql_command.Parameters.AddWithValue("@YEAR", year);

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

        //retrieves data from viptrend benchmark 
        public DataTable GetBenchmarkPrcssData()
        {
            string sqlQuery;

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                sqlQuery = @"select * from dbo.BenchPrcss order by YEAR desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        //verifies start and end dates have been entered
        public bool VerifyDatesEntered(string series)
        {
            bool dates_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = @"SELECT RSTART, REND FROM dbo.TsBench where OSERIES = @SERIES
                     and RSTART <> ''  and REND <> ''";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@SERIES", series);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            dates_exist = true;
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }

            return dates_exist;
        }


        public void UpdateTask(string task)
        {
            DateTime dt = DateTime.Now;
            string currdate = string.Format("{0:dd-MMM-yyyy}", dt);
            string year = DateTime.Now.ToString("yyyy");

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                using (SqlCommand sql_command = new SqlCommand(@"update dbo.BenchPrcss
                        set " + task.Trim() + "A = @CURRDATE, " + task.Trim() +
                        "B = '3' where YEAR = @YEAR", sql_connection))
                {
                    try
                    {
                        sql_command.Parameters.AddWithValue("@YEAR", year);
                        sql_command.Parameters.AddWithValue("@CURRDATE", currdate.ToUpper().Trim());
                      
                        sql_connection.Open();

                        //Execute the query.

                        sql_command.ExecuteNonQuery();
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
    }
}

