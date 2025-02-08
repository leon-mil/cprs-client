/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.CCMailData.cs	    	

Programmer:         Cestine Gill

Creation Date:      02/23/2016

Inputs:             None

Parameters:	        None 

Outputs:	        CCMail data	

Description:	    This function establishes the data connection and reads in 
                    the data from the CCMail table and allows users to add 
 * email addresses to the table

Detailed Design:    Detailed Design for CCMail

Other:	            Called by: frmCCMail.cs, frmAddCCMailPopup.cs
 
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
    public class CCMailData
    {
        /************RETRIEVE CCMail DATA**********/

        public DataTable GetCCMailTable(string jobflag)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //If not searching by jobflag, output all the rows

                if (jobflag == "")
                {
                    SqlCommand sql_command = new SqlCommand("Select * From dbo.CCMail ORDER BY EMAIL, JOBFLAG ASC", sql_connection);
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

                //if searching by jobflag, only output the rows for that jobflag

                else
                {
                    SqlCommand sql_command = new SqlCommand("Select * From dbo.CCMail where jobflag = @JOBFLAG ORDER BY EMAIL, JOBFLAG DESC", sql_connection);
                    sql_command.Parameters.Add("@JOBFLAG", SqlDbType.Char).Value = GeneralData.NullIfEmpty(jobflag);
                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

            }

            return dt;
        }

        /*Vaildate that the email does not already exist in the ccmail table*/

        public bool CheckCCMailExist(string jobflag, string email)
        {
            bool record_found = false;
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                SqlCommand sql_command = new SqlCommand("SELECT * FROM dbo.CCMAIL WHERE jobflag = @JOBFLAG AND email = @EMAIL", sql_connection);
                sql_command.Parameters.AddWithValue("@JOBFLAG", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(jobflag);
                sql_command.Parameters.AddWithValue("@EMAIL", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(email);
            
             //Open the connection.

                    sql_connection.Open();
                SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);

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
            return record_found;
            }
        


        /************ADD CCMAIL DATA**********/

        // When the user clicks the button to add ccmail,
        // Updates the fields in the dbo.CCMail table

        public void AddCCMail(string jobflag, string email, string usrnme, string prgdtm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.CCMAIL(JOBFLAG, EMAIL, USRNME, PRGDTM) VALUES (@JOBFLAG, @EMAIL, @USRNME, @PRGDTM )", sql_connection);

                    sql_command.Parameters.AddWithValue("@JOBFLAG", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(jobflag);
                    sql_command.Parameters.AddWithValue("@EMAIL", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(email);
                    sql_command.Parameters.AddWithValue("@USRNME", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(usrnme);
                    sql_command.Parameters.AddWithValue("@PRGDTM", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(prgdtm);
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

        //Delete the row from the CCMAIL table for the selected JOBFLAG and EMAIL

        public void DeleteCCMailRow(string jobflag, string email)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.CCMAIL WHERE JOBFLAG = @JOBFLAG AND EMAIL = @EMAIL", sql_connection);

                    sql_command.Parameters.Add("@JOBFLAG", SqlDbType.Char).Value = GeneralData.NullIfEmpty(jobflag);
                    sql_command.Parameters.Add("@EMAIL", SqlDbType.Char).Value = GeneralData.NullIfEmpty(email);

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
