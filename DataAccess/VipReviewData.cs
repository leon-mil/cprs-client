/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.VIPReviewData.cs
Programmer:         Diane Musachio
Creation Date:      4/28/2017
Inputs:             N/A
Parameters:	        seldate, toc, uvipdata, prevdata, fipstate, statp, state
Outputs:	       
Description:	    data layer to get data for VIP Review
Detailed Design:    None
Other:	            Called by: frmVIPReviewMon.cs
                               frmVIPReviewAnn.cs
Revision History:	
***************************************************************************************
 Modified Date : June 26, 2019
 Modified By   : Diane Musachio
 Keyword       : dm062619
 Change Request: CR#3301
 Description   : display back to January of two years prior in dropdown
                 'New Date' modified to select top (50) from viphist
****************************************************************************************
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
    public class VipReviewData
    {
        //Retrieve dates for monthly pull-down 
        public DataTable GetMonthList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //dm062618 modified from top 35 to top 50 
                string sqlQuery = @"select distinct top (50) DATE6
                     from dbo.VIPHIST  
                     order by DATE6 DESC";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {                  
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        //retrieves data from viphist for monthly vip
        public DataTable GetMonthlyData(string seldate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select TOC, UVIPDATA,
                     PREVDATA, USRNME, PRGDTM
                     from dbo.VIPHIST  
                     where @seldate = DATE6 ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SELDATE", seldate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        //updates viphist with edited data
        public void UpdateVipReview(string toc, string uvipdata, string prevdata, string seldate)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                    string usql =   "UPDATE dbo.viphist SET " +
                                    "UVIPDATA = @UVIPDATA, " +
                                    "PREVDATA = @PREVDATA, " +
                                    "USRNME = @USRNME, " +
                                    "PRGDTM = @PRGDTM  " +
                                    "WHERE DATE6 = @SELDATE and TOC = @TOC";

                    SqlCommand update_command = new SqlCommand(usql, sql_connection);
                    update_command.Parameters.AddWithValue("@TOC", Convert.ToDouble(toc));
                    update_command.Parameters.AddWithValue("@UVIPDATA", Convert.ToDouble(uvipdata));
                    update_command.Parameters.AddWithValue("@PREVDATA", Convert.ToDouble(prevdata));
                    update_command.Parameters.AddWithValue("@SELDATE", (seldate));
                    update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                    update_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

                try
                {
                    sql_connection.Open();
                    int count = update_command.ExecuteNonQuery();
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
        
        //gets annual list from vipfarm
        public DataTable GetAnnualList()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select distinct YEAR
                     from dbo.VIPFARM 
                     order by YEAR DESC";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        //retrieves data from vipfarm for selected date
        public DataTable GetAnnualData(string seldate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select FIPSTATE, UVIPDATA,
                     PREVDATA, USRNME, PRGDTM
                     from dbo.VIPFARM  
                     where @SELDATE = YEAR";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SELDATE", seldate);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        //updates vipfarm with edited information
        public void UpdateVipFarmReview(string fipstate, string uvipdata, string prevdata, string seldate)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE dbo.vipfarm SET " +
                                "UVIPDATA = @UVIPDATA, " +
                                "PREVDATA = @PREVDATA, " +
                                "USRNME = @USRNME, " +
                                "PRGDTM = @PRGDTM  " +
                                "WHERE YEAR = @SELDATE and FIPSTATE = @FIPSTATE";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@FIPSTATE", fipstate);
                update_command.Parameters.AddWithValue("@UVIPDATA", Convert.ToDouble(uvipdata));
                update_command.Parameters.AddWithValue("@PREVDATA", Convert.ToDouble(prevdata));
                update_command.Parameters.AddWithValue("@SELDATE", seldate);
                update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                update_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

                try
                {
                    sql_connection.Open();
                    int count = update_command.ExecuteNonQuery();
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

        // If the current stat period does not exist, insert it into the table
        public void InsertRows(string statp, string state)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand(@"INSERT INTO dbo.VIPFARM (year, fipstate, uvipdata, prevdata, usrnme, prgdtm) 
                        VALUES (@STATP, @STATE, @UVIP, @PREVVIP, @USRNME, @PRGDTM)" , sql_connection);
                    sql_command.Parameters.AddWithValue("@STATP", statp);
                    sql_command.Parameters.AddWithValue("@STATE", state);
                    sql_command.Parameters.AddWithValue("@UVIP", 0);
                    sql_command.Parameters.AddWithValue("@PREVVIP", 0);
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
    }
}
