/***************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.ImpMonProcessData.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/30/2015

Inputs:             dbo.CEPRCSS

Parameters:	    None 

Outputs:	    None	

Description:	    This program will execute the CRUD actions on the 
 *                  database table and output the table to the screen

Detailed Design:    Detailed Design for Improvements Monthly Processing 

Other:	            Called from: frmImpMonthProc.cs
 
Revision History:	
*********************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
*********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;


namespace CprsDAL
{
    public class ImpMonProcessData
    {

        //Convert Month to uppercase

        string tdate = DateTime.Today.ToString("dd-MMM-yyyy").ToUpper();

        //Retrieve all of the Monthly Processing Data from the CEPRESS table. This will
        //display when the page loads

        public DataTable GetMonProcessData()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    SqlCommand sql_command = new SqlCommand("select * from dbo.CEPRCSS order by STATP DESC", sql_connection);

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {

                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return dt;


        }

        //Check whether the current stat period exists in the database

        public DataTable CheckStatp(string sStatp)
        {
            DataTable dt1 = new DataTable();

            try
            {
                using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                {

                    SqlCommand sql_command = new SqlCommand("Select statp From dbo.CEPRCSS WHERE Statp = @sStatp", sql_connection);
                    sql_command.Parameters.Add("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);

                    // Create a DataAdapter to run the command and fill the DataTable

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        da.Fill(dt1);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return dt1;


        }

        // If the current stat period does not exist, insert it into the table

        public void InsertRow(string sStatp)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.CEPRCSS (Statp, Task01A, Task01B, Task02A, Task02B, Task03A, Task03B, Task04A, Task04B) VALUES (@sStatp, ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' )", sql_connection);
                    sql_command.Parameters.Add("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);

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

        // When the user clicks the button to run the CE Load process,
        // Updates the date and status fields in the CEPRCSS table

        public DataTable RunCELoadUpdate(string sStatp)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CEPRCSS SET Task01A = @Task01A, Task01B = @Task01B WHERE [Statp] = @sStatp", sql_connection);

                    sql_command.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command.Parameters.AddWithValue("@Task01A", tdate);
                    sql_command.Parameters.AddWithValue("@Task01B", "3");

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

        // When the user clicks the button to run the Tabulations process,
        // Updates the date and status fields in the CEPRCSS table

        public DataTable RunTabulationsUpdate(string sStatp)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CEPRCSS SET Task02A = @Task02A, Task02B = @Task02B WHERE [Statp] = @sStatp", sql_connection);

                    sql_command.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command.Parameters.AddWithValue("@Task02A", tdate);
                    sql_command.Parameters.AddWithValue("@Task02B", "3");

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

        // When the user clicks the button to run the Forecasting process,
        // Updates the date and status fields in the CEPRCSS table

        public DataTable RunForecastingUpdate(string sStatp)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {

                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CEPRCSS SET Task03A = @Task03A, Task03B = @Task03B WHERE [Statp] = @sStatp", sql_connection);

                    sql_command.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command.Parameters.AddWithValue("@Task03A", tdate);
                    sql_command.Parameters.AddWithValue("@Task03B", "3");

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

        // When the user clicks the button to run the VIP Load process,
        // Updates the date and status fields in the CEPRCSS table

        public DataTable RunVIPLoadUpdate(string sStatp)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CEPRCSS SET Task04A = @Task04A, Task04B = @Task04B WHERE [Statp] = @sStatp", sql_connection);

                    sql_command.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command.Parameters.AddWithValue("@Task04A", tdate);
                    sql_command.Parameters.AddWithValue("@Task04B", "3");

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

        // When the user clicks the button to run the Tabulations process,
        // Updates the date and status fields in the CEPRCSS table

        public void Reset(string sStatp)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {                   
                    SqlCommand sql_command1 = new SqlCommand("UPDATE dbo.CEPRCSS SET Task01A = '', Task01B = '' WHERE [Statp] = @sStatp AND [Task01B] = '2'", sql_connection);
                    SqlCommand sql_command2 = new SqlCommand("UPDATE dbo.CEPRCSS SET Task02A = '', Task02B = '' WHERE [Statp] = @sStatp AND [Task02B] = '2'", sql_connection);
                    SqlCommand sql_command3 = new SqlCommand("UPDATE dbo.CEPRCSS SET Task03A = '', Task03B = '' WHERE [Statp] = @sStatp AND [Task03B] = '2'", sql_connection);
                    SqlCommand sql_command4 = new SqlCommand("UPDATE dbo.CEPRCSS SET Task04A = '', Task04B = '' WHERE [Statp] = @sStatp AND [Task04B] = '2'", sql_connection);

                    sql_command1.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command2.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command3.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    sql_command4.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                    
                    //Open the connection.

                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable
                  
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command1))
                        {
                            //Execute the query.
                            sql_command1.ExecuteNonQuery();
                        }
                    
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command2))
                        {
                            //Execute the query.
                            sql_command2.ExecuteNonQuery();
                        }

                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command3))
                        {
                            //Execute the query.
                            sql_command3.ExecuteNonQuery();
                        }
                    
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command4))
                        {
                            //Execute the query.
                            sql_command4.ExecuteNonQuery();
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
