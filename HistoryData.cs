/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.HistoryData.cs	    	

Programmer:         Cestine Gill

Creation Date:      06/11/2015

Inputs:             Id, Respid

Parameters:	        None 

Outputs:	        History data	

Description:	    This function establishes the data connection and reads in 
                    the data, based upon the id and respid, from the History Display View 
                    using the sp_HistoryDisplay stored procedure that will be used for the 
                    History Display screen

Detailed Design:    None 

Other:	            Called by: frmHistory
 
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
    public class HistoryData
    {
        /*Get Project Data */
        //search the table to find rows where the ID matches the ID 
        //for the Project Comments Table

        public DataTable GetProjCommentTable(string id, bool show_short = false)
        {      
                DataTable dt = new DataTable();
                using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    string sql_string  = string.Empty;
                    if (show_short)
                        sql_string = "Select top 2 (substring(commdate, 1, 4)+'-'+substring(commdate, 5, 2)+ '-' + substring(commdate, 7, 2)) + ' ' + (substring(commtime, 1, 2)+ ':'+ substring(commtime, 3,2) + ':' + substring(commtime, 5,2)) as commdate, USRNME, COMMTEXT From dbo.PROJECT_COMMENT WHERE ID = @ID ORDER BY COMMDATE DESC, COMMTIME DESC";
                    else
                        sql_string = "Select (substring(commdate, 1, 4)+'-'+substring(commdate, 5, 2)+ '-' + substring(commdate, 7, 2)) + ' ' + (substring(commtime, 1, 2)+ ':'+ substring(commtime, 3,2) + ':' + substring(commtime, 5,2)) as commdate, USRNME, COMMTEXT From dbo.PROJECT_COMMENT WHERE ID = @ID ORDER BY COMMDATE DESC, COMMTIME DESC";

                    SqlCommand sql_command = new SqlCommand(sql_string, sql_connection);
                    sql_command.CommandTimeout = 0;
                    sql_command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                    
                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

            return dt;

        }

        /*Get Respondent Data */
        //search the table to find rows where the RESPID matches the Respid 
        //for the Respondent Comments Table

        public DataTable GetRespCommentTable(string respid, bool show_short = false)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql_string = string.Empty;
                if (show_short)
                    sql_string = "Select top 2 (substring(commdate, 1, 4) + '-' + substring(commdate, 5, 2) + '-' + substring(commdate, 7, 2)) + ' ' + (substring(commtime, 1, 2) + ':' + substring(commtime, 3, 2) + ':' + substring(commtime, 5, 2)) as commdate, USRNME, COMMTEXT From dbo.RESPONDENT_COMMENT WHERE RESPID = @RESPID ORDER BY COMMDATE DESC, COMMTIME DESC";
                else
                    sql_string = "Select (substring(commdate, 1, 4)+'-'+substring(commdate, 5, 2)+ '-' + substring(commdate, 7, 2)) + ' ' + (substring(commtime, 1, 2)+ ':'+ substring(commtime, 3,2) + ':' + substring(commtime, 5,2)) as commdate, USRNME, COMMTEXT From dbo.RESPONDENT_COMMENT WHERE RESPID = @RESPID ORDER BY COMMDATE DESC, COMMTIME DESC";

                SqlCommand sql_command = new SqlCommand(sql_string, sql_connection);
                sql_command.CommandTimeout = 0;
                sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }


        // When the user clicks the button to add the project remark,
        // Updates the fields in the dbo.RESP_PROJ_COMMENT table

        public void AddProjectRemark(string id, string usrnme, string commdate, string commtime, string commtext)
        {
            //DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.PROJECT_COMMENT(ID, USRNME, COMMDATE, COMMTIME, COMMTEXT) VALUES (@ID, @USRNME, @COMMDATE, @COMMTIME, @COMMTEXT )", sql_connection);
                    sql_command.CommandTimeout = 0;
                    sql_command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                    sql_command.Parameters.Add("@USRNME", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(usrnme);
                    sql_command.Parameters.Add("@COMMDATE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(commdate);
                    sql_command.Parameters.Add("@COMMTIME", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(commtime);
                    sql_command.Parameters.Add("@COMMTEXT", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(commtext);
                   
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


        // When the user clicks the button to add the respondent remark,
        // Updates the fields in the dbo.RESP_PROJ_COMMENT table

        public void AddRespondentRemark(string respid, string usrnme, string commdate, string commtime, string commtext)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.RESPONDENT_COMMENT(RESPID, USRNME, COMMDATE, COMMTIME, COMMTEXT) VALUES (@RESPID, @USRNME, @COMMDATE, @COMMTIME, @COMMTEXT )", sql_connection);
                    sql_command.CommandTimeout = 0;
                    sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);
                    sql_command.Parameters.Add("@USRNME", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(usrnme);
                    sql_command.Parameters.Add("@COMMDATE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(commdate);
                    sql_command.Parameters.Add("@COMMTIME", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(commtime);
                    sql_command.Parameters.Add("@COMMTEXT", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(commtext);

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