/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.VIPBenchmarkExportData.cs
Programmer:         Diane Musachio
Creation Date:      3/26/2019
Inputs:             dbo.VipTrend, dbo.VipAnnual
Parameters:	        rstart, rend, oseries
Outputs:	        dbo.tsbench
Description:	    data layer to set start and end date for series
                    processing
Detailed Design:    None
Other:	            Called by: frmBenExport.cs

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
    public class VipBenchmarkExportData
    {
        /*Verify series in VipTrend return true if no zero data otherwise return false */
        public bool CheckMonthExistsWithData(string statp, string toc)
        {
            bool data_valid = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql =  @"select DATE6 from dbo.Viptrend where DATE6 = " + statp.AddSqlQuotes() +
                    
                    " and toc = " + toc.AddSqlQuotes() + " and uvipdata > 0 and uvipdata is not null";

                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            return true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return data_valid;
        }

        //Check whether year exists in the database
        public bool CheckYearExistsWithData(string statp, string toc)
        {
            bool data_exist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select year from dbo.Vipannual where year = " + statp.AddSqlQuotes() +
                    " and toc = " + toc.AddSqlQuotes() + " and uvipdata > 0 and uvipdata is not null";

                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            return true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return data_exist;
        }

        //updates vip benchmark with edited data
        public void UpdateVipBenchmark(string rstart, string rend, string oseries)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                string usql = "UPDATE dbo.TSBENCH SET " +
                                "RSTART = @RSTART, " +
                                "REND = @REND, " +
                                "BPERIODS = @BPERIODS, " +
                                "PRGDTM = @PRGDTM, " +
                                "PRGNME = @PRGNME, " +
                                "USRNME = @USRNME " + 
                                "WHERE RSERIES = @OSERIES";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@RSTART", rstart + "12");
                update_command.Parameters.AddWithValue("@REND", rend.ToString() + "12");
                if (oseries == "V20IXBMO")
                {
                    update_command.Parameters.AddWithValue("@BPERIODS", 2);
                }
                else
                {
                    update_command.Parameters.AddWithValue("@BPERIODS", 
                        ((Convert.ToInt32(rend)) - Convert.ToInt32(rstart)) + 1);
                }
                update_command.Parameters.AddWithValue("@OSERIES", oseries);
                update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                update_command.Parameters.AddWithValue("@PRGNME", "export");
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
