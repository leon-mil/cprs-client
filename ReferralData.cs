/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.ReferralData.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/11/2015

Inputs:             Id, Respid, reftype, refgroup, refstatus, refnote, usrnme, prgdtm

Parameters:	    None 

Outputs:	    Referral data	

Description:	    This function establishes the data connection and reads in 
                    the data, based upon the id and respid 
                    using tables, project_referral and repondent_referral, 
                    the views, refnote_proj_grpcde and refnote_resp_grpcde,
                    the stored procedures, sp_PROJReferralReview and sp_RESPReferralReview
                    that will be used for the 
                    Referral screen and the Referral Review screen

Detailed Design:    Detailed Design for Referrals
                    Detailed Design for Referrals Review 

Other:	            Called by: frmReferral.cs, frmReferralReview.cs
 
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
    public class ReferralData
    {

        /************RETRIEVE REFERRAL DATA**********/

        //PROJECT
        //search the table to find rows where the ID matches the id 
        //in the view, dbo.REFNOTE_PROJ_GRPCDE
        //used for the Project Referral Table

        public DataTable GetProjReferralTable(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select REFTYPE, REFSTATUS, USRNME, REFGROUP, PRGDTM, REFNOTE, GRPCDE, NEWTC From dbo.REFNOTE_PROJ_GRPCDE WHERE ID = @ID AND REFSTATUS <> @STATUS ORDER BY PRGDTM DESC", sql_connection);

                sql_command.Parameters.AddWithValue("@ID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
                sql_command.Parameters.AddWithValue("@STATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty("C"); //display only active cases

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        //RESPONDENT
        //search the table to find rows where the RESPID matches the Respid 
        //in the view, dbo.REFNOTE_RESP_GRPCDE
        //for the Respondent Referral Table

        public DataTable GetRespReferralTable(string respid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select REFTYPE, REFSTATUS, USRNME, REFGROUP, PRGDTM, REFNOTE, GRPCDE From dbo.REFNOTE_RESP_GRPCDE WHERE RESPID = @RESPID AND REFSTATUS <> @STATUS ORDER BY PRGDTM DESC", sql_connection);

                sql_command.Parameters.AddWithValue("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);

                //display only active cases

                sql_command.Parameters.AddWithValue("@STATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty("C");

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

       /************ADD REFERRAL DATA**********/

        // PROJECT
        // When the user clicks the button to add the project referral,
        // Updates the fields in the dbo.PROJECT_REFERRAL table

        public void AddProjectReferral(string id, string reftype, string refgroup, string refstatus, string refnote, string usrnme, string prgdtm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.PROJECT_REFERRAL(ID, REFTYPE, REFGROUP, REFSTATUS, REFNOTE, USRNME, PRGDTM) VALUES (@ID, @REFTYPE, @REFGROUP, @REFSTATUS, @REFNOTE, @USRNME, @PRGDTM )", sql_connection);

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

        //RESPONDENT
        // When the user clicks the button to add the respondent referral,
        // Updates the fields in the dbo.RESPONDENT_REFERRAL table

        public void AddRespondentReferral(string respid, string reftype, string refgroup, string refstatus, string refnote, string usrnme, string prgdtm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.RESPONDENT_REFERRAL(RESPID, REFTYPE, REFGROUP, REFSTATUS, REFNOTE, USRNME, PRGDTM) VALUES (@RESPID, @REFTYPE, @REFGROUP, @REFSTATUS, @REFNOTE, @USRNME, @PRGDTM )", sql_connection);

                    sql_command.Parameters.AddWithValue("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);
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

        /************UPDATE REFERRAL DATA**********/

        // PROJECT - 
        // When the user clicks the button to UPDATE referral,
        // Updates the note and/or status fields

        public DataTable UpdateProjectReferral(string id, string reftype, string refgroup, string refstatus, string prgdtm, string refnote)
        {

            DataTable dt = new DataTable();
           
  
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.PROJECT_REFERRAL SET refstatus = @Refstatus, refnote = @Refnote WHERE [ID] = @Id AND [REFTYPE] = @Reftype AND [REFGROUP] = @Refgroup AND [Prgdtm] = @Prgdtm", sql_connection);

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

        // RESPONDENT - 
        // When the user clicks the button to UPDATE referral,
        // Updates the note and/or status fields

        public DataTable UpdateRespondentReferral(string respid, string reftype, string refgroup, string refstatus, string prgdtm, string refnote)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.RESPONDENT_REFERRAL SET refstatus = @Refstatus, refnote = @Refnote WHERE [RESPID] = @Respid AND [REFTYPE] = @Reftype AND [REFGROUP] = @Refgroup AND [Prgdtm] = @Prgdtm", sql_connection);

                    sql_command.Parameters.AddWithValue("@Respid", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);
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

        /*********** REVIEW REFERRAL DATA ***********/

        //PROJECT
        //search the table to find rows where the ID matches the ID 
        //for the Project Referral Review Table

        public DataTable GetProjReferralReviewTable(string id, string type, string group, string status, string newtc, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_ProjReferralReview", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.AddWithValue("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);
                sql_command.Parameters.AddWithValue("@TYPE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(type);
                sql_command.Parameters.AddWithValue("@GROUP", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(group);
                sql_command.Parameters.AddWithValue("@STATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(status);
                sql_command.Parameters.AddWithValue("@NEWTC", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);
                sql_command.Parameters.AddWithValue("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                sql_command.Parameters.AddWithValue("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        public List<string> GetDodgeInitialList()
        {
            List<string> idlist = new List<string>();
            string dt = DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM");
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select s.id from dbo.sample s, dbo.master m where s.MASTERID = m.MASTERID and m.SELDATE = " + dt + " and owner <> 'M'";
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                idlist.Add(reader["id"].ToString());
                            }
                        }
                        reader.Close();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                return idlist;
            }

        }

        //PROJECT
        /*Get data to set up the unique values in the comboboxes in the Referral Review screen Project table*/

        public DataTable GetProjValueList(int cbIndex)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {

                    /*The combobox data will display unique newtc*/

                    if (cbIndex == 1)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct newtc from dbo.refnote_proj_grpcde order by newtc", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique reftype*/

                    else if (cbIndex == 2)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct reftype from dbo.project_referral order by reftype", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique refstatus*/

                    else if (cbIndex == 3)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct refstatus from dbo.project_referral order by refstatus", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique usrname*/

                    if (cbIndex == 4)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct usrnme from dbo.project_referral order by usrnme", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);
                            DataRow dr = dt.NewRow();
                            dr[0] = " ";
                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique refgroup*/

                    if (cbIndex == 5)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct refgroup from dbo.project_referral order by refgroup", sql_connection);
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


        /*********** REVIEW REFERRAL DATA*************/

        //RESPONDENT
        //search the table to find rows where the ID matches the ID 
        //for the Respondent Referral Review Table

        public DataTable GetRespReferralReviewTable(string respid, string type, string group, string status, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_RSPReferralReview", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.AddWithValue("@RESPID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(respid);
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

        //RESPONDENT
        /*Get data to set up the unique values in the comboboxes in the Referral Review screen Respondent table*/

        public DataTable GetRespValueList(int cbIndex)
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
                        SqlCommand sql_command = new SqlCommand("select distinct reftype from dbo.respondent_referral order by reftype", sql_connection);
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
                        SqlCommand sql_command = new SqlCommand("select distinct refstatus from dbo.respondent_referral order by refstatus", sql_connection);
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
                        SqlCommand sql_command = new SqlCommand("select distinct usrnme from dbo.respondent_referral order by usrnme", sql_connection);
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
                        SqlCommand sql_command = new SqlCommand("select distinct refgroup from dbo.respondent_referral order by refgroup", sql_connection);
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

        //Check Referral Exists
        public bool CheckReferralExist(string id, string respid)
        {
            bool record_found = false;

            /* get data table */
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    string sql = "SELECT * FROM PROJECT_REFERRAL where ID = " + GeneralData.AddSqlQuotes(id) + " and refstatus in ( 'A', 'P')";
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
            }
            if (respid != null)
            {
                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    string sql = "SELECT * FROM RESPONDENT_REFERRAL where RESPID = " + GeneralData.AddSqlQuotes(respid) + " and refstatus in ( 'A', 'P')";
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
            }

            return record_found;
        }

        public bool CheckReferralExistInAll(string id)
        {
            bool record_found = false;

            /* get data table */
            if (id != null)
            {
                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    string sql = "SELECT * FROM PROJECT_REFERRAL where ID = " + GeneralData.AddSqlQuotes(id) ;
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
            }

            return record_found;
        }

    }


}