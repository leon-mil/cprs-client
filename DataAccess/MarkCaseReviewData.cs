/**********************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.MarkCaseReviewData.cs	    	

Programmer      : Cestine Gill

Creation Date   : 09/04/2015

Inputs          : dbo.sp_MarkReview 
                  dbo.sp_MarkReviewHQ
                  Userinfo.Username
                  frmMarkCaseReview.cs 
                  ID, USRNME

Parameters      : None 

Outputs         : Mark Case Review data	

Description:	  This class establishes the data connection and reads in 
                  the data, based upon the ID that will be used for the 
                  Marked Case Review screen 

Detailed Design : Marked Case Review Detail Design

Other           : Called by: 
 
Revision History:	
*********************************************************************
 Modified Date  :  
 Modified By    :  
 Keyword        :  
 Change Request :  
 Description    :   
***********************************************************************/
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
    public class MarkCaseReviewData
    {

        /*Access the stored procedure that retrieves the Project ID Marked cases only for the 
         * specific user that access the case if the 
         * User accessing the screen is not a Programmer or HQ Supervisor*/

        public DataTable GetMarkCase(string usrnme, string id, string newtc, string prgdtm)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("dbo.sp_CSDMarkReview", sql_connection);
                    sql_command.CommandType = CommandType.StoredProcedure;

                    sql_command.Parameters.Add("@USRTYPE", SqlDbType.Char).Value = "S";
                    sql_command.Parameters.Add("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                    sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);
                    sql_command.Parameters.Add("@NEWTC", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);
                    sql_command.Parameters.Add("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        da.Fill(dt);
                    }
                    
                    return dt;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /*Access the stored procedure that retrieves the Respondent RESPID Marked cases only for the 
      * specific user that access the case if the 
      * User accessing the screen is not a Programmer or HQ Supervisor*/

        public DataTable GetRSPMarkCase(string usrnme, string respid, string prgdtm)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("dbo.sp_RSPMarkReview", sql_connection);
                    sql_command.CommandType = CommandType.StoredProcedure;
                    sql_command.Parameters.Add("@USRTYPE", SqlDbType.Char).Value = GeneralData.NullIfEmpty("S");
                    sql_command.Parameters.Add("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                    sql_command.Parameters.Add("@RESPID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(respid);
                    sql_command.Parameters.Add("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        da.Fill(dt);
                    }
                    
                    return dt;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /*Get data to set up the unique username value combobox in the Mark Case Review screen 
         *The combobox data will display only the User accessing the screen*/

        public DataTable GetValueList(int cbIndex, string user)
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

                        SqlCommand sql_command = new SqlCommand("select distinct newtc from dbo.mark_case_review order by newtc", sql_connection);
                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            da.Fill(dt);

                            DataRow dr = dt.NewRow();
                            dr[0] = " ";

                            dt.Rows.InsertAt(dr, 0);
                        }
                    }

                    /*The combobox data will display unique usrname*/

                    else if (cbIndex == 2)
                    {
                       
                        SqlCommand sql_command = new SqlCommand("select distinct usrnme from dbo.csdmark order by usrnme", sql_connection);
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

        //Delete the row from the CSDMARK table for the selected ID and USERNAME

        public void DeleteIDRow(string id, string user)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.CSDMARK WHERE ID = @ID AND USRNME = @USER", sql_connection);

                    sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);
                    sql_command.Parameters.Add("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(user);

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

        /*Get data to set up the unique username value combobox in the Respondent Mark Case Review screen 
      *The combobox data will display only the User accessing the screen*/

        public DataTable GetRSPValueList(int cbIndex, string user)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    if (cbIndex == 1)
                    {
                        SqlCommand sql_command = new SqlCommand("select distinct usrnme from dbo.rspmark order by usrnme", sql_connection);
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


        //Delete the row from the RSPMARK table for the selected RESPID and USERNAME

        public void DeleteRSPRow(string respid, string user)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.RSPMARK WHERE RESPID = @RESPID AND USRNME = @USER", sql_connection);

                    sql_command.Parameters.Add("@RESPID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(respid);
                    sql_command.Parameters.Add("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(user);

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

        //check respid exist in resmark table
        public bool CheckRespidinMarkcases(string check_respid)
        {
            bool RespIdExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select respid from dbo.RSPMARK where respid = " + GeneralData.AddSqlQuotes(check_respid);
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
            return RespIdExist;
        }

        //check id exists in mark case reveiw view
        public bool CheckIdinMarkcases(string check_id)
        {
            bool RespIdExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select id from dbo.MARK_CASE_REVIEW where id = " + GeneralData.AddSqlQuotes(check_id);
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
            return RespIdExist;
        }



    }
}
