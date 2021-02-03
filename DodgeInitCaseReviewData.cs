/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : DodgeInitCaseReviewData.cs

 Programmer    : Diane Musachio

 Creation Date : 2/15/2017

 Inputs        : N/A

 Paramaters    : ID,  CBINDEX, VAR, VAR2, CHOICE, CHOICE2  

 Output        : Data from DCPReview View
                   
 Description   : These classes get data from the view: dbo.DCPReview
 
 Detail Design : Detailed User Requirements for Dodge Initial Case Review

 Other         : Called from:

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
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
    public class DodgeInitialReviewData
    {

        /* This class uses an SQL connection to access the view dbo.DCPReview */

        public DataTable GetDCPReview()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select ID, RESPID, THQWORKED, HQNME, TNPCWORKED, REV1NME, REV2NME, " +
                    " NEWTC, OWNER, PROJDESC, PROJLOC from dbo.DCPReview order by id";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        /* This class uses an SQL connection to access the table dcpinitial
          and returns the Ids*/

        public bool GetDCPids(string id)
        {
            bool record_found = false;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select ID from dbo.DCPINITIAL where ID =  " + GeneralData.AddSqlQuotes(id) +
                   " order by ID" ;

                SqlCommand command = new SqlCommand(sqlQuery, sql_connection);

                try
                {
                    sql_connection.Open();
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

        //Populate values in dropdown lists 
        public DataTable GetValueList(int cbIndex)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    //If 1st review selected get distinct rev1nme 
                    if (cbIndex == 6)
                    {
                        using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                        {
                            try
                            {
                                SqlCommand sql_command = new SqlCommand("select distinct rev1nme from dbo.DCPINITIAL where rev1nme != '' order by rev1nme", sql_connection);

                                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                                {
                                    da.Fill(dt);

                                    DataRow dr = dt.NewRow();
                                    dr[0] = " ";

                                    dt.Rows.InsertAt(dr, 0);
                                }
                            }
                            catch (SqlException ex)
                            {
                                throw ex;
                            }
                        }
                    }

                    //If 2nd review selected get distinct rev2nme 
                    else if (cbIndex == 7)
                    {
                        using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                        {
                            try
                            {
                                SqlCommand sql_command = new SqlCommand("select distinct rev2nme from dbo.DCPINITIAL where rev2nme != '' order by rev2nme", sql_connection);

                                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                                {
                                    da.Fill(dt);

                                    DataRow dr = dt.NewRow();
                                    dr[0] = " ";

                                    dt.Rows.InsertAt(dr, 0);
                                }
                            }
                            catch (SqlException ex)
                            {
                                throw ex;
                            }
                        }
                    }
                    //If 2nd review selected get distinct rev2nme 
                    else if (cbIndex == 8)
                    {
                        using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                        {
                            try
                            {
                                SqlCommand sql_command = new SqlCommand("select distinct hqnme from dbo.DCPINITIAL where hqnme != '' order by hqnme", sql_connection);

                                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                                {
                                    da.Fill(dt);

                                    DataRow dr = dt.NewRow();
                                    dr[0] = " ";

                                    dt.Rows.InsertAt(dr, 0);
                                }
                            }
                            catch (SqlException ex)
                            {
                                throw ex;
                            }
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

        //Get results of searches
        public DataTable SearchDCPReview(string var, string choice, string var2, string choice2)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlwhere1 = "";
                string sqlwhere2 = "";
                string sqlselection = " select ID, RESPID, THQWORKED, HQNME, TNPCWORKED, REV1NME, REV2NME, " +
                    " NEWTC, OWNER, PROJDESC, PROJLOC from dbo.DCPReview where ";
                string sqlorder = " order by ID ";

                //ID search
                if (var == "ID")
                {
                    sqlwhere1 = sqlselection + " ID = @CHOICE";
                }
                //RESPID search
                else if (var == "RESPID")
                {
                    sqlwhere1 = sqlselection + "RESPID = @CHOICE";
                }
                //SURVEY search
                else if (var == "SURVEY")
                {
                    sqlwhere1 = sqlselection + "SURVEY = @CHOICE";
                }
                //NEWTC search
                else if (var == "NEWTC")
                {
                    sqlwhere1 = sqlselection + " (NEWTC = @CHOICE or TC2 = @CHOICE)";
                }
                //HQ WORK STATUS search
                else if (var == "THQWORKED")
                {
                    sqlwhere1 = sqlselection + "THQWORKED = @CHOICE";
                }
                //NPC WORK STATUS search
                else if (var == "TNPCWORKED")
                {
                    sqlwhere1 = sqlselection + "TNPCWORKED = @CHOICE";
                }
                //NPC 1st REVIEW search
                else if (var == "REV1NME")
                {
                    sqlwhere1 = sqlselection + " REV1NME = @CHOICE";
                }
                //NPC 2nd REVIEW search
                else if (var == "REV2NME")
                {
                    sqlwhere1 = sqlselection + " REV2NME = @CHOICE";
                }
                //HQ REVIEW search
                else if (var == "HQNME")
                {
                    sqlwhere1 = sqlselection + " HQNME = @CHOICE";
                }

                //if 2nd search criteria selected
                //ID search
                if (var2 == "ID")
                {
                    sqlwhere2 = " ID = @CHOICE2";
                }
                //RESPID search
                else if (var2 == "RESPID")
                {
                    sqlwhere2 = " RESPID = @CHOICE2";
                }
                //SURVEY search
                else if (var2 == "SURVEY")
                {
                    sqlwhere2 = " SURVEY = @CHOICE2";
                }
                //NEWTC search
                else if (var2 == "NEWTC")
                {
                    sqlwhere2 = " (NEWTC = @CHOICE2 or TC2 = @CHOICE2)";
                }
                //HQ WORK STATUS search
                else if (var2 == "THQWORKED")
                {
                    sqlwhere2 = " THQWORKED = @CHOICE2";
                }
                //NPC WORK STATUS search
                else if (var2 == "TNPCWORKED")
                {
                    sqlwhere2 = " TNPCWORKED = @CHOICE2";
                }
                //NPC 1st REVIEW search
                else if (var2 == "REV1NME")
                {
                    sqlwhere2 = " REV1NME = @CHOICE2";
                }
                //NPC 2nd REVIEW search
                else if (var2 == "REV2NME")
                {
                    sqlwhere2 = " REV2NME = @CHOICE2";
                }
                //HQ REVIEW search
                else if (var2 == "HQNME")
                {
                    sqlwhere2 = " HQNME = @CHOICE2";
                }

                string sqlwhere = "";

                SqlCommand sql_command = new SqlCommand(sqlwhere, connection);


                //create selection clause based on one search or two
                if (var2 != null)
                {
                    sql_command.Parameters.AddWithValue("@VAR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(var);
                    sql_command.Parameters.AddWithValue("@VAR2", SqlDbType.NVarChar).Value = var2;
                    sql_command.Parameters.AddWithValue("@CHOICE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(choice);
                    sql_command.Parameters.AddWithValue("@CHOICE2", SqlDbType.NVarChar).Value = choice2;

                    sqlwhere = sqlwhere1 + " and " + sqlwhere2 + sqlorder;
                }
                else
                {
                    sql_command.Parameters.AddWithValue("@VAR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(var);
                    sql_command.Parameters.AddWithValue("@CHOICE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(choice);

                    sqlwhere = sqlwhere1 + sqlorder;
                }

                sql_command.CommandText = sqlwhere;

                // Create a DataAdapter to run the command and fill the DataTable

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }            
    }
}
