/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.AuthorizedUserAuditData.cs	    	

Programmer:         Srini Natarajan

Creation Date:      09/12/2016

Inputs:             Tables: USERAUDIT, ACCOUNTRVW.

Parameters:	    None 

Outputs:	    UserAudit and Accountrvw 	

Description:	    This class reads the data from the UsersAudit and Accountrvw tables.

Detailed Design:    None.

Other:	            Called by:  frmAuthUserAuditPopup.cs, 
 *                              frmAuthUserAnnAuditPopup.cs, 
 *                              frmAuthUserAnnAddPopup.cs 
 
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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using CprsBLL;

namespace CprsDAL
{
    //Functions to populate the the Authorized users audit popups
   public class AuthorizedUserAuditData
    {
       //get data from useraudit
        public DataTable GetAuthorizedUsersData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT USERNAME, ACTION, OLDVAL, NEWVAL, USRNME, PRGDTM FROM dbo.USERAUDIT order by PRGDTM Desc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        // Get current Year audit Review
        public void GetCurrYearAuditReview(string curryear, int revnum, ref string accounts, ref string comment)
        {
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT accounts, commtext FROM dbo.accountrvw WHERE YEAR = " + GeneralData.AddSqlQuotes(curryear) + " and revnum = " + revnum;
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        accounts = reader["ACCOUNTS"].ToString();
                        comment = reader["COMMTEXT"].ToString();
                    }
                }

                reader.Close();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }

       // get data from Accountrvw table.
        public DataTable GetAnnualAuthorizedUsersData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT YEAR, REVNUM, USRNME, PRGDTM, ACCOUNTS, COMMTEXT FROM dbo.ACCOUNTRVW Order by Year desc, revnum desc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);

                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //Check if the current year is present.
        public bool CheckCurrYearRec(string currYear, int revnum)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select year from dbo.accountrvw where YEAR = " + GeneralData.AddSqlQuotes(currYear) + " and revnum = " + revnum;
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

        //Add current year if not present.
        public void AddCurYear(string year, int revnum, string accounts, string commtext, string usrnme, DateTime prgdtm)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

        string isql = "insert dbo.Accountrvw (Year, revnum, accounts, commtext, usrnme, prgdtm)"
                        + " Values (@YEAR, @REVNUM, @ACCOUNTS, @COMMTEXT, @USRNME, @PRGDTM)";
        SqlCommand insert_command = new SqlCommand(isql, sql_connection);
        insert_command.Parameters.AddWithValue("@YEAR", year);
        insert_command.Parameters.AddWithValue("@REVNUM", revnum);
        insert_command.Parameters.AddWithValue("@ACCOUNTS", accounts);
        insert_command.Parameters.AddWithValue("@COMMTEXT", commtext);
        insert_command.Parameters.AddWithValue("@USRNME", usrnme);
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

       //Update the changes to annual audit
        public void UpdateAnnAuthUserComment(string CommText, string Year, int Revnum,  string Accounts, string usrnme, DateTime prgdtm)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.accountrvw set Commtext = @COMMTEXT, Year =  @YEAR, Revnum=@Revnum, Accounts = @ACCOUNTS, usrnme = @USRNME, prgdtm = @PRGDTM " +
                                " WHERE Year = @YEAR and revnum=@revnum";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@COMMTEXT", CommText);
                    sql_command.Parameters.AddWithValue("@YEAR", Year);
                    sql_command.Parameters.AddWithValue("@REVNUM", Revnum);
                    sql_command.Parameters.AddWithValue("@ACCOUNTS", Accounts);
                    sql_command.Parameters.AddWithValue("@USRNME", usrnme);
                    sql_command.Parameters.AddWithValue("@PRGDTM", prgdtm);

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
                    connection.Close(); //close database connection
                }
            }
        }
    }

}
