/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.VIPBenchmarkTrenderData.cs
Programmer:         Diane Musachio
Creation Date:      3/1/2019
Inputs:             dbo.VIPTREND
Parameters:	        year, series, statp, toc, uvipdata, prevdata, user
Outputs:	       
Description:	    data layer to get data for VIP Benchmark Trenders
Detailed Design:    None
Other:	            Called by: frmBenTrender.cs
                               
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
    public class VipBenchmarkTrenderData
    {
        /*Get latest date if get the info return true, otherwise return false */
        public bool GetStatInfo(string year)
        {
            bool date_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT DATE6 FROM dbo.VIPTREND where substring(DATE6, 1, 4) = @NEXTYR order by DATE6 desc";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@NEXTYR", year);

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

        //retrieves data from viptrend table 
        public DataTable GetBenchmarkTrenderData(string series)
        {
            string sqlQuery;

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                    sqlQuery = @"select DATE6, UVIPDATA, PREVDATA, USRNME, 
                         PRGDTM from dbo.VIPTREND where @series = TOC order by DATE6 desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SERIES", series);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }
   
        // If the next year does not exist, insert it into the table
        public void InsertRows(string statp, string toc)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand(@"INSERT INTO dbo.VIPTREND (date6, toc, uvipdata, prevdata, usrnme, prgdtm) 
                        VALUES (@STATP, @TOC, @UVIPDATA, @PREVDATA, @USRNME, @PRGDTM)", sql_connection);
                    sql_command.Parameters.AddWithValue("@STATP", statp);
                    sql_command.Parameters.AddWithValue("@TOC", toc);
                    sql_command.Parameters.AddWithValue("@UVIPDATA", 0);
                    sql_command.Parameters.AddWithValue("@PREVDATA", 0);
                    sql_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                    sql_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

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

        //updates viptrend table with edited data
        public void UpdateVipBenchmark(string statp, string toc, string uvipdata, string prevdata, string user)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                string usql = "UPDATE dbo.VIPTREND SET " +
                                "UVIPDATA = @UVIPDATA, " +
                                "PREVDATA = @PREVDATA, " +
                                "USRNME = @USRNME, " +
                                "PRGDTM = @PRGDTM  " +
                                "WHERE DATE6 = @STATP and TOC = @TOC";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@TOC", toc);
                update_command.Parameters.AddWithValue("@UVIPDATA", Convert.ToDouble(uvipdata));
                update_command.Parameters.AddWithValue("@PREVDATA", Convert.ToDouble(prevdata));
                update_command.Parameters.AddWithValue("@STATP", statp);
                update_command.Parameters.AddWithValue("@USRNME", user);
                update_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

                try
                {
                    sql_connection.Open();

                    //Execute the query.

                    update_command.ExecuteNonQuery();

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
