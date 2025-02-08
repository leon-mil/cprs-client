/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.TCDescriptionReviewData.cs	    	

Programmer:         Cestine Gill

Creation Date:      03/17/2016

Inputs:             None

Parameters:	        None 

Outputs:	        TCDescriptionReview data	

Description:	    This function establishes the data connection and reads in 
                    the data from the NEWTCLIST table and allows users to add,
                    delete and update newtc and tcdescriptions

Detailed Design:    Detailed Design for NEWTC Description Review

Other:	            Called by: frmTCDescriptionReview.cs, frmAddTCDescriptionPopup.cs,
                               frmUpdTCDescriptionPopup.cs
 
Revision History:	
****************************************************************************************
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
    public class TCDescriptionReviewData
    {
        /************RETRIEVE NewTC DATA**********/

        public DataTable GetNewTCTable(string newtc)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //If not searching by newtc, output all the rows

                if (newtc == "")
                {
                    SqlCommand sql_command = new SqlCommand("Select * From dbo.NEWTCLIST ORDER BY NEWTC ASC, TCDESCRIPTION", sql_connection);
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

                //if searching by newtc, only output the rows for that newtc

                else
                {
                    if (newtc.Substring(2, 2) == "00")
                        newtc = newtc.Substring(0, 2);
                    else if (newtc.Substring(3,1) == "0")
                        newtc = newtc.Substring(0, 3);
                    newtc = newtc + "%";

                    SqlCommand sql_command = new SqlCommand("Select * From dbo.NEWTCLIST where newtc like @NEWTC ORDER BY NEWTC, TCDESCRIPTION", sql_connection);
                    sql_command.Parameters.Add("@NEWTC", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);
                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

            }

            return dt;
        }

        /************ADD DESCRIPTION DATA**********/

        // When the user clicks the button to add newtc description,
        // Updates the fields in the dbo.NEWTCLIST table

        public void AddNewTCDescription(string newtc, string tcdescription)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.NEWTCLIST(NEWTC, TCDESCRIPTION) VALUES (@NEWTC, @TCDESCRIPTION)", sql_connection);

                    sql_command.Parameters.AddWithValue("@NEWTC", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtc);
                    sql_command.Parameters.AddWithValue("@TCDESCRIPTION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(tcdescription);
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


        /************UPDATE DESCRIPTION DATA**********/

        // When the user clicks the button to update the newtc description,
        // Updates the fields in the dbo.NEWTCLIST table

        public void UpdateNewtcDescription(string newtc, string old_description, string tcdescription)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.NEWTCLIST SET tcdescription = @TCDESCRIPTION WHERE [NEWTC] = @NEWTC AND [TCDESCRIPTION] = @OLDDESCRIPTION", sql_connection);

                    sql_command.Parameters.AddWithValue("@NEWTC", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtc);
                    sql_command.Parameters.AddWithValue("@TCDESCRIPTION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(tcdescription);
                    sql_command.Parameters.AddWithValue("@OLDDESCRIPTION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(old_description);

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


        /************DELETE DESCRIPTION DATA**********/

        // When the user clicks the button to delete the newtc description,
        // Deletes the field in the dbo.NEWTCLIST table

        public void DeleteNewtcDescription(string newtc, string tcdescription)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.NEWTCLIST WHERE NEWTC = @NEWTC AND TCDESCRIPTION = @TCDESCRIPTION", sql_connection);

                    sql_command.Parameters.AddWithValue("@NEWTC", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtc);
                    sql_command.Parameters.AddWithValue("@TCDESCRIPTION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(tcdescription);
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

        /*Get data to set up the unique newtc value in the combobox*/

        public DataTable GetNewTCValueList()
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    sql_connection.Open();
                    SqlCommand sql_command = new SqlCommand("select distinct newtc from dbo.NEWTCLIST order by newtc", sql_connection);
                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        da.Fill(dt);
                        DataRow dr = dt.NewRow();
                        dr[0] = " ";
                        dt.Rows.InsertAt(dr, 0);
                    }
                    sql_connection.Close();

                    return dt;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

    }
}
