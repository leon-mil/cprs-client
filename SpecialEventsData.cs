/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.SpecialEventData.cs	    	
Programmer:         Christine Zhang
Creation Date:      03/27/2023
Inputs:             None
Parameters:	        None 
Outputs:	        Special Events data	
Description:	    This function establishes the data connection and reads in 
                    the data from the SPECIAL_EVENTS table and allows users to add,
                    delete and update events
Detailed Design:   
Other:	            Called by: frmSpecialEvents.cs
 
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
    public class SpecialEventsData
    {
        /************RETRIEVE DATA**********/

        public DataTable GetEvents()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select * From dbo.SPECIAL_EVENTS ORDER BY EVENTID", sql_connection);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
               
            }

            return dt;
        }

        public String GetEventDescription(int eventid)
        {
            string event_description = string.Empty;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT EVENTDESCRIPTION FROM dbo.SPECIAL_EVENTS WHERE EVENTID = " + eventid;
                SqlCommand command = new SqlCommand(sql, sql_connection);

                sql_connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        event_description = reader["EVENTDESCRIPTION"].ToString();
                    }
                  
                }
                sql_connection.Close();
            }
            return event_description;

        }


        /************ADD DATA**********/

        // When the user clicks the button to add events
        // Updates the fields in the dbo.SPECIAL_EVENTS table

        public void AddEvent(string eventdescription)
        {
  
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.SPECIAL_EVENTS(EVENTDESCRIPTION, USRNME, PRGDTM) VALUES (@EVENTDESCRIPTION, @USRNME, @PRGDTM )", sql_connection);

                    sql_command.Parameters.AddWithValue("@EVENTDESCRIPTION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(eventdescription);
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


        /************UPDATE DESCRIPTION DATA**********/

        // When the user clicks the button to update the description,
        // Updates the fields in the dbo.Special_events table

        public void UpdateEvent(int eventid, string eventdescription)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.SPECIAL_EVENTS SET eventdescription = @EVENTDESCRIPTION," +
                        "USRNME = @USRNME," +
                        "PRGDTM = @PRGDTM" +
                        " WHERE [EVENTID] = @EVENTID", sql_connection);

                    sql_command.Parameters.AddWithValue("@EVENTID", SqlDbType.Int).Value = eventid;
                    sql_command.Parameters.AddWithValue("@EVENTDESCRIPTION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(eventdescription);
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


        /************DELETE DESCRIPTION DATA**********/

        // When the user clicks the button to delete the event
        // Deletes the field in the dbo.SPECIAL_EVENTS table

        public void DeleteEvent(int eventid)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.SPECIAL_EVENTS WHERE EVENTID = @eventid", sql_connection);

                    sql_command.Parameters.AddWithValue("@EVENTID", SqlDbType.Int).Value = eventid;
                    
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

