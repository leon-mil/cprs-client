/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.ImpReferralData.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/29/2015

Inputs:             Id, reftype, refgroup, refstatus, refnote, usrnme, prgdtm

Parameters:	     None 

Outputs:	    Improvements Referral table	

Description:	    This function establishes the data connection and reads in 
                    the data, based upon the improvements id
                    using the ce_referral table, refnote_ce_grpcde view,
                    and the sp_CeReferralReview stored procedure
                    that will be used for the Improvements Referral
                    and the Improvements Referral Review screen

Detailed Design:    Detailed Design for Improvement Referral
                    Detailed Design for Improvement Referral Review

Other:	            Called by: frmImpReferralReview.cs, frmImpReferral.cs
 
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
    public class ImpReferralData
    {

        //Check Improvement Referral Exists

        public bool CheckReferralExist(string id)
        {
            bool record_found = false;

            /* get data table */

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * FROM CEREFERRAL where ID = " + GeneralData.AddSqlQuotes(id) + " and refstatus = 'A'";
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

            return record_found;
        }


        /*Get Improvements Data */
        //search the table to find rows where the ID matches the id in the view, dbo.REFNOTE_CE_GRPCDE
        //for the Improvements Referral Table

        public DataTable GetCEReferralTable(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select REFTYPE, REFSTATUS, USRNME, REFGROUP, PRGDTM, REFNOTE From dbo.REFNOTE_CE_GRPCDE WHERE ID = @ID AND REFSTATUS = @STATUS ORDER BY PRGDTM DESC", sql_connection);

                sql_command.Parameters.AddWithValue("@ID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                sql_command.Parameters.AddWithValue("@STATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty("A"); //Only display active cases

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        // When the user clicks the button to add the improvements referral,
        // Updates the fields in the dbo.CEREFERRAL table

        public void AddCEReferral(string id, string reftype, string refgroup, string refstatus, string refnote, string usrnme, string prgdtm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.CEREFERRAL(ID, REFTYPE, REFGROUP, REFSTATUS, REFNOTE, USRNME, PRGDTM) VALUES (@ID, @REFTYPE, @REFGROUP, @REFSTATUS, @REFNOTE, @USRNME, @PRGDTM )", sql_connection);

                    sql_command.Parameters.AddWithValue("@ID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                    sql_command.Parameters.AddWithValue("@REFTYPE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(reftype);
                    sql_command.Parameters.AddWithValue("@REFGROUP", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(refgroup);
                    sql_command.Parameters.AddWithValue("@REFSTATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(refstatus);
                    sql_command.Parameters.AddWithValue("@REFNOTE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(refnote);
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

        // When the user clicks the button to UPDATE referral,
        // Updates the note and/or status fields

        public DataTable UpdateCEReferral(string id, string reftype, string refgroup, string refstatus, string prgdtm, string refnote)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CEREFERRAL SET refstatus = @Refstatus, refnote = @Refnote WHERE [ID] = @Id AND [REFTYPE] = @Reftype AND [REFGROUP] = @Refgroup AND [Prgdtm] = @Prgdtm", sql_connection);

                    sql_command.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                    sql_command.Parameters.AddWithValue("@Reftype", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(reftype);
                    sql_command.Parameters.AddWithValue("@Refgroup", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(refgroup);
                    sql_command.Parameters.AddWithValue("@Refstatus", refstatus);
                    sql_command.Parameters.AddWithValue("@Prgdtm", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(prgdtm);
                    sql_command.Parameters.AddWithValue("@Refnote", refnote);

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

        // When the user clicks the button to UPDATE referral,
        // Updates the status field

        public DataTable UpdateCEStatusReferral(string id, string reftype, string refgroup, string prgdtm, string refstatus)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CEREFERRAL SET refstatus = @Refstatus WHERE [ID] = @Id AND [REFTYPE] = @Reftype AND [REFGROUP] = @Refgroup AND [Prgdtm] = @Prgdtm", sql_connection);

                    sql_command.Parameters.AddWithValue("@Id", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                    sql_command.Parameters.AddWithValue("@Reftype", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(reftype);
                    sql_command.Parameters.AddWithValue("@Refgroup", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(refgroup);
                    sql_command.Parameters.AddWithValue("@Prgdtm", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(prgdtm);
                    sql_command.Parameters.AddWithValue("@Refstatus", refstatus);

                    //Open the connection.

                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        //Execute the query
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

        /******************************IMPROVEMENTS REFERRAL REVIEW DATA**********************************/

        public DataTable GetCEReferralReviewTable(string id, string type, string group, string status, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_CeReferralReview", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.AddWithValue("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);
                sql_command.Parameters.AddWithValue("@TYPE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(type);
                sql_command.Parameters.AddWithValue("@GROUP", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(group);
                sql_command.Parameters.AddWithValue("@STATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(status);
                sql_command.Parameters.AddWithValue("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                sql_command.Parameters.AddWithValue("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        /*Get data to set up the unique values in the comboboxes in the Improvements Referral Review screen*/
      

        public DataTable GetCEValueList(int cbIndex)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {

                    /*The combobox data will display unique reftype*/

                    if (cbIndex == 1)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct reftype from dbo.cereferral order by reftype", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                        /*The combobox data will display unique refstatus*/

                    else if (cbIndex == 2)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct refstatus from dbo.cereferral order by refstatus", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique usrnme*/

                    if (cbIndex == 3)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct usrnme from dbo.cereferral order by usrnme", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique refgroup*/

                    if (cbIndex == 4)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct refgroup from dbo.cereferral order by refgroup", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }
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
