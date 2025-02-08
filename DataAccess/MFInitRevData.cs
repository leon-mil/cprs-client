
/************************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.MFInitRevData.cs	    	

Programmer      : Diane Musachio

Creation Date   : 05/09/2016

Inputs          : dbo.presample_v

Parameters      : None 

Outputs         : 

Description     : Gets data from cprsdev to populate Multi-family Initial review screen

Detailed Design : None 

Other           : Called by: frm MFInitRev.cs
 
Revision History:	
**************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
**************************************************************************************/
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
    public class GetMFInitialReview
    {
        //populate data grid prior to searches
        public DataTable GetMFInitRevData()
        {
            DataTable mf = new DataTable();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
             //   SqlCommand sql_command = new SqlCommand("select ID, PSU, BPOID, SCHED, TWORKED, DUPLICATE, REV1NME, REV2NME, PROJDESC, PROJLOC, WORKED from dbo.PRESAMPLE_V ORDER BY PSU, BPOID, SCHED", connection);
                SqlCommand sql_command = new SqlCommand("select ID, PSU, BPOID, SCHED, TWORKED, DUPLICATE, REV1NME, REV2NME, PROJDESC, PROJLOC, WORKED from dbo.PRESAMPLE_V ORDER BY ID", connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(mf);
            }
            return mf;
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
                    if (cbIndex == 3)
                    {
                        using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                        {
                            try
                            {
                                SqlCommand sql_command = new SqlCommand("select distinct rev1nme from dbo.presample_v where rev1nme != '' order by rev1nme", sql_connection);

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
                    else if (cbIndex == 4)
                    {
                        using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                        {
                            try
                            {
                                SqlCommand sql_command = new SqlCommand("select distinct rev2nme from dbo.presample_v where rev2nme != '' order by rev2nme", sql_connection);

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

        //Check to see if id is valid in presample view
        /* This class uses an SQL connection to access the table dcpinitial
          and returns the Ids*/

        public bool GetPresampleIds(string id)
        {
            bool record_found = false;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select ID from dbo.PRESAMPLE_V where ID =  " + GeneralData.AddSqlQuotes(id) +
                   " order by ID";

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

        //Get results of searches
        public DataTable SearchPresample(string var, string choice, string var2, string choice2)
        {

            DataTable dt = new DataTable();

                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                     string sqlwhere1 = "";
                     string sqlwhere2 = "";
                     string sqlselection = " select ID, PSU, BPOID, SCHED, TWORKED, DUPLICATE, REV1NME, REV2NME, PROJDESC, PROJLOC from dbo.PRESAMPLE_V where ";
                  // string sqlorder = " order by PSU, BPOID, SCHED ";
                     string sqlorder = " order by ID";

                     //ID search
                     if (var == "ID")
                     {
                         sqlwhere1 = sqlselection + "ID = @CHOICE";
                     }
                    
                     //masterid search
                     else if (var == "MASTERID")
                     {
                         sqlwhere1 = sqlselection + "MASTERID = @CHOICE";
                     }
                     //tworked search
                     else if (var == "TWORKED")
                     {
                         sqlwhere1 = sqlselection + "TWORKED = @CHOICE";
                     }
                     //duplicate search
                     else if (var == "DUPLICATE")
                     {
                         sqlwhere1 = sqlselection + "DUPLICATE = @CHOICE";
                     }
                     //rev1nme search
                     else if (var == "REV1NME")
                     {
                         sqlwhere1 = sqlselection + "REV1NME = @CHOICE";
                     }
                     //rev2nme search
                     else if (var == "REV2NME")
                     {
                         sqlwhere1 = sqlselection + "REV2NME = @CHOICE";
                     }

                     ////*if second part of search is selected get additional clause
                     if (var2== "ID")
                     {
                         sqlwhere2 = "ID = @CHOICE2";
                     }
                     //masterid search
                     else if (var2 == "MASTERID")
                     {
                         sqlwhere2 = "MASTERID = @CHOICE2";
                     }
                     //tworked search
                     else if (var2 == "TWORKED")
                     {
                         sqlwhere2 = "TWORKED = @CHOICE2";
                     }
                     //duplicate search
                     else if (var2 == "DUPLICATE")
                     {
                         sqlwhere2 = "DUPLICATE = @CHOICE2";
                     }
                     //rev1nme search
                     else if (var2 == "REV1NME")
                     {
                         sqlwhere2 = "REV1NME = @CHOICE2";
                     }
                     //rev2nme search
                     else if (var2 == "REV2NME")
                     {
                         sqlwhere2 = "REV2NME = @CHOICE2";
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
                     else if (var != null)
                     {
                         sql_command.Parameters.AddWithValue("@VAR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(var);
                         sql_command.Parameters.AddWithValue("@CHOICE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(choice);
                         
                         sqlwhere = sqlwhere1 + sqlorder;
                         
                     }
                     else
                        sqlwhere = " select ID, PSU, BPOID, SCHED, TWORKED, DUPLICATE, REV1NME, REV2NME, PROJDESC, PROJLOC from dbo.PRESAMPLE_V order by id";

                    sql_command.CommandText = sqlwhere;

                    // Create a DataAdapter to run the command and fill the DataTable

                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

                return dt;
        }            

            
//        //Get results of searches
//        public DataTable Search(string choice)
//        {
//            DataTable dt = new DataTable();

         
//                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
//                {
//                    SqlCommand sql_command = new SqlCommand(@"select PSU, PLACE, SCHED, TWORKED, DUPLICATE, 
//                    REV1NME, REV2NME, PROJDESC, PROJLOC from dbo.PRESAMPLE_V where " + @choice + " ORDER BY PSU, PLACE, SCHED", connection);

//                    sql_command.Parameters.Add("@CHOICE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(choice);

//                    // Create a DataAdapter to run the command and fill the DataTable

//                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
//                    da.Fill(dt);
//                }
            


//            return dt;
//        }
        
    }
}

