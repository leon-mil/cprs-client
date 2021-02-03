/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.AnnualProcessData.cs
Programmer:         Christine Zhang
Creation Date:      5/20/2019
Inputs:             None
Parameters:	        year
Outputs:	        None
Description:	    data layer to set start and end date for annually
                    processing
Detailed Design:    None
Other:	            Called by: frmAnnualProcess.cs

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
    public class AnnualProcessData
    {
        /*Get latest date if cannot get the info, add new row */
        public void CheckCurrentYearProcessInfo(string year)
        {
           
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * FROM dbo.ANNPRCSS where YEAR = @CURRYR";

                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@CURRYR", year);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            reader.Close();
                            try
                            {
                                SqlCommand sql_command = new SqlCommand(@"INSERT INTO dbo.ANNPRCSS(year, 
                                Task01A, Task01B, Task02A, Task02B, Task03A, Task03B,Task04A, Task04B,
                                Task05A, Task05B, Task06A, Task06B, Task07A, Task07B,Task08A, Task08B, Task09A, Task09B, Task10A, Task10B) 
                                VALUES (@YEAR, '','','','','','','','','','','','','','','','','','','','')", connection);
                                sql_command.Parameters.AddWithValue("@YEAR", year);

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
                        }
                       
                    }
                    connection.Close();
                }
            }
        }

        //retrieves data from Annual process 
        public DataTable GetAnnualPrcssData()
        {
            string sqlQuery;

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                sqlQuery = @"select * from dbo.ANNPRCSS order by YEAR desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        public void UpdateTask(string task)
        {
            DateTime dt = DateTime.Now;
            string currdate = string.Format("{0:dd-MMM-yyyy}", dt);
            string year = DateTime.Now.ToString("yyyy");

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                using (SqlCommand sql_command = new SqlCommand(@"update dbo.ANNPRCSS
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
