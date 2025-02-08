/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.AuthorizedUsersData.cs	    	

Programmer:         Diane Musachio

Creation Date:      10/31/2016

Inputs:             None

Parameters:	        None 

Outputs:	        Authorized user data	

Description:	    Get appropriate data from and apply updates to sched_id and audit tables

Detailed Design:    Detailed Design for Authorized Users

Other:	            Called by: frmAuthUsers.cs, frmNewAuthUserPopup.cs
 
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
    public class AuthorizedUsersData
    {
        /*Get all authorized users data */
        public DataTable GetAuthorizedUsersTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select usrnme, grpcde, grade, printq, " +
                    "initsl, initfd, initnr, initmf, contsl, contfd, contnr, contmf " +
                    "From dbo.SCHED_ID ORDER BY USRNME", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        /*Get only NPC authorized users data*/
        public DataTable GetNPCAuthorizedUsersTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select usrnme, grpcde, grade, printq, " +
                    "initsl, initfd, initnr, initmf, contsl, contfd, contnr, contmf "+
                    "From dbo.SCHED_ID where grpcde in ('3','4','5') ORDER BY USRNME", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*Vaildate that the username does not already exist*/
        public bool CheckUsernameExist(string username)
        {
            bool record_found = false;
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("SELECT * FROM dbo.SCHED_ID WHERE USRNME = @USERNAME", sql_connection);
                    sql_command.Parameters.AddWithValue("@USERNAME", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(username);

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

        /*Get printer location (HQ or NPC) and name*/
        public DataTable GetPrinterTable(string location)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select * From dbo.PRINTERS where location = @LOCATION " +
                    "order by LOCATION", sql_connection);

                sql_command.Parameters.AddWithValue("@LOCATION", location);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr[0] = -1;
                dr[1] = " ";
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }

        /************ ADD AUTHORIZED USER **********/

        // When the user clicks the button to add authorized user,
        // Updates the fields in the dbo.Sched_Id table
        public void AddAuthorizedUser( string usrnme, string grpcde, string grade, string printq, string Initsl, string Initnr,
            string Initfd, string Initmf, string Contsl, string Contnr, string Contfd, string Contmf)
        { 
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.Sched_Id ( USRNME, GRPCDE, GRADE, "
                        + " PRINTQ, INITSL, INITNR, INITFD, INITMF, CONTSL, CONTNR, CONTFD, CONTMF)"
                        + " VALUES (@USRNME, @GRPCDE, @GRADE, @PRINTQ, @INITSL, @INITNR, @INITFD, @INITMF, "
                        + "@CONTSL, @CONTNR, @CONTFD, @CONTMF)", sql_connection);

                    sql_command.Parameters.AddWithValue("@USRNME", usrnme);
                    sql_command.Parameters.AddWithValue("@GRPCDE", grpcde);
                    sql_command.Parameters.AddWithValue("@GRADE",  grade);
                    sql_command.Parameters.AddWithValue("@PRINTQ", printq);
                    sql_command.Parameters.AddWithValue("@INITSL", Initsl);
                    sql_command.Parameters.AddWithValue("@INITNR", Initnr);
                    sql_command.Parameters.AddWithValue("@INITFD", Initfd);
                    sql_command.Parameters.AddWithValue("@INITMF", Initmf);
                    sql_command.Parameters.AddWithValue("@CONTSL", Contsl);
                    sql_command.Parameters.AddWithValue("@CONTNR", Contnr);
                    sql_command.Parameters.AddWithValue("@CONTFD", Contfd);
                    sql_command.Parameters.AddWithValue("@CONTMF", Contmf);
                   
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

         /************ UPDATE AUTHORIZED USER **********/

        // When the user clicks the button to update authorized user,
        // Updates the fields in the dbo.Sched_Id table
        public void UpdateAuthorizedUser( string usrnme, string grpcde, string grade, string printq, string Initsl, string Initnr,
            string Initfd, string Initmf, string Contsl, string Contnr, string Contfd, string Contmf)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.Sched_Id " 
                        + " SET GRPCDE = @GRPCDE, GRADE = @GRADE, "
                        + " PRINTQ = @PRINTQ, INITSL = @INITSL, INITNR = @INITNR, INITFD = @INITFD, "
                        + " INITMF = @INITMF, CONTSL = @CONTSL, CONTNR = @CONTNR, CONTFD = @CONTFD, "
                        + " CONTMF= @CONTMF "
                        + " WHERE USRNME = @USRNME", sql_connection);

                    sql_command.Parameters.AddWithValue("@USRNME", usrnme);
                    sql_command.Parameters.AddWithValue("@GRPCDE", grpcde);
                    sql_command.Parameters.AddWithValue("@GRADE", grade);
                    sql_command.Parameters.AddWithValue("@PRINTQ", printq);
                    sql_command.Parameters.AddWithValue("@INITSL", Initsl);
                    sql_command.Parameters.AddWithValue("@INITNR", Initnr);
                    sql_command.Parameters.AddWithValue("@INITFD", Initfd);
                    sql_command.Parameters.AddWithValue("@INITMF", Initmf);
                    sql_command.Parameters.AddWithValue("@CONTSL", Contsl);
                    sql_command.Parameters.AddWithValue("@CONTNR", Contnr);
                    sql_command.Parameters.AddWithValue("@CONTFD", Contfd);
                    sql_command.Parameters.AddWithValue("@CONTMF", Contmf);

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

        /*Populates the user audit table based on action */
        public void AddUserAuditData(string username, string action, string oldval, string newval, string usr, string prgdtm) 
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert into dbo.useraudit (USERNAME, ACTION, OLDVAL, NEWVAL, USRNME, PRGDTM)"
                            + " Values (@USERNAME, @ACTION, @OLDVAL, @NEWVAL, @USRNME, @PRGDTM)";

            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@USERNAME", usr);
            insert_command.Parameters.AddWithValue("@ACTION", action);
            insert_command.Parameters.AddWithValue("@OLDVAL", oldval.Trim());
            insert_command.Parameters.AddWithValue("@NEWVAL", newval.Trim());
            insert_command.Parameters.AddWithValue("@USRNME", username);
            insert_command.Parameters.AddWithValue("@PRGDTM", prgdtm);

            try
            {
                sql_connection.Open();
                insert_command.ExecuteNonQuery();
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

        //Delete the row from the SCHED_ID table
        public void DeleteRow(string user)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.SCHED_ID WHERE USRNME = @USRNME", sql_connection);

                    sql_command.Parameters.Add("@USRNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(user);

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