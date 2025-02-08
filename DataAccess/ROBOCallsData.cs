/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.ROBOCallsData.cs	    	
Programmer:         Christine Zhang
Creation Date:      06/28/2023
Inputs:             None
Parameters:	        None 
Outputs:	        Robocall data	
Description:	    This function establishes the data connection and reads in 
                    the data from the robocalls table and allows users to update days,
                    
Detailed Design:   
Other:	            Called by: frmROBOCalls.cs
 
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
    public class ROBOCallsData
    {
        /************RETRIEVE DATA**********/

        public DataTable GetRobocalls()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select * From dbo.ROBOCallS ORDER BY ROBOID", sql_connection);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

            }

            return dt;
        }

        /************UPDATE ROBODAY DATA**********/

        // When the user clicks the button to update the day,
        // Updates the fields in the dbo.ROBOCALLS table

        public void UpdateRobocallday(int roboid, string day, int Robocalls_type)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;

                try
                {
                    if (Robocalls_type == 0)
                    {
                        sql_command = new SqlCommand("UPDATE dbo.ROBOCALLS SET roboday = @roboday," +
                            "USRNME = @USRNME," +
                            "PRGDTM = @PRGDTM" +
                            " WHERE [ROBOID] = @roboid", sql_connection);

                        sql_command.Parameters.AddWithValue("@ROBOID", SqlDbType.Int).Value = roboid;
                        sql_command.Parameters.AddWithValue("@ROBODAY", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(day);
                        sql_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                        sql_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);
                    }
                    else
                    {
                        sql_command = new SqlCommand("UPDATE dbo.ROBOCALLS SET Robocall = @robocall," +
                            "USRNME = @USRNME," +
                            "PRGDTM = @PRGDTM" +
                            " WHERE [ROBOID] = @roboid", sql_connection);

                        sql_command.Parameters.AddWithValue("@ROBOID", SqlDbType.Int).Value = roboid;
                        sql_command.Parameters.AddWithValue("@ROBOCALL", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(day);
                        sql_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                        sql_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);
                    }

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
