/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.UnlockData.cs	    	

Programmer:         Srini Natarajan

Creation Date:      10/05/2016

Inputs:             None 

Parameters:	        None 

Outputs:	        Current Users, Respondent and PreSample	

Description:	    This class reads the data from the Current_Users, Respondent and PreSample tables.

Detailed Design:    None.

Other:	            Called by:  frmUnlock.cs

 
Revision History:	
****************************************************************************************
 Modified Date :  03/02/2023
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  
 Description   :  add function to unlock tab
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
    public class UnlockData
    {
        //get data from current users table.
        public DataTable GetCurrentUsersData()
        {
            DataTable dtUsers = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT USRNME, MAX(TIMEIN) FROM dbo.CURRENT_USERS WHERE USRNME <> '" + UserInfo.UserName + "' GROUP BY USRNME";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtUsers);
                }
            }
            return dtUsers;
        }

        //get data from respondent table.
        public DataTable GetRespondentData()
        {
            DataTable dtResp = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT RESPID, RESPORG, RESPNAME, RESPLOCK FROM dbo.RESPONDENT WHERE RESPLOCK <> '' AND RESPLOCK <> 'aasys001'"; 
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtResp);
                }
            }
            return dtResp;
        }

        //get data from Pre Sample table.
        public DataTable GetPreSampleData()
        {
            DataTable dtPSamp = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT ID, PSU, BPOID, SCHED, PROJDESC, RESPLOCK FROM dbo.PRESAMPLE WHERE RESPLOCK <> ''AND RESPLOCK <> 'aasys001'"; 
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtPSamp);
                }
            }
            return dtPSamp;
        }

        //get data from Special case table.
        public DataTable GetSpecialcaseData()
        {
            DataTable dtPSamp = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT FIN, PSU, BPOID, SCHED, PROJDESC, LOCKID FROM dbo.SPECIALCASE WHERE LOCKID <> ''";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtPSamp);
                }
            }
            return dtPSamp;
        }

        //get data from Dup research table.
        public DataTable GetDupResearchData()
        {
            DataTable dtPSamp = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT FIN, primst, primcnty, owner, projselv, seldate, primtc, LOCKID FROM dbo.RESEARCH WHERE LOCKID <> ''";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtPSamp);
                }
            }
            return dtPSamp;
        }

        //get data from tablock table.
        public DataTable GetTablockData()
        {
            DataTable dtTab = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT Tc2, USRNME FROM dbo.Tablock WHERE USRNME <> ''";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtTab);
                }
            }
            return dtTab;
        }

        /*Clear the lock field in tablock table*/
        public void ClearTabLock(string Tc2)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                string usql = "UPDATE dbo.Tablock SET " +
                                "USRNME = '' " +
                                "WHERE Tc2 = @Tc2";
                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@Tc2", Tc2);
                try
                {
                    int count = update_command.ExecuteNonQuery();
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

        //Delete the account_user row selected.
        public bool DeleteCurrUser(string Usrnme)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            string usql = "DELETE FROM CURRENT_USERS WHERE USRNME = @USRNME";
            SqlCommand delete_command = new SqlCommand(usql, sql_connection);
            delete_command.Parameters.AddWithValue("@USRNME", Usrnme);
            try
            {
                sql_connection.Open();
                int count = delete_command.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
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

        /*Clear the lock field in Respondent table for an individual respid*/
        public void ClearRespIDLock(string Respid)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                string usql = "UPDATE dbo.Respondent SET " +
                                "RESPLOCK = '' " +
                                "WHERE RESPID = @RESPID";
                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@RESPID", Respid);
                try
                {
                    int count = update_command.ExecuteNonQuery();
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

        /* Clear the lock field in PreSample table for an individual id*/
        public void ClearPreSampLock(string id)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.Presample set RESPLOCK = '' where ID = @id";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@ID", id);
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
                    connection.Close(); //close database connection
                }
            }
        }

        /* Clear the lock field in PreSample table for an individual fin*/
        public void ClearSpecialcaseLock(string fin)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.Specialcase set LOCKID = '' where FIN = @FIN";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@FIN", fin);
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
                    connection.Close(); //close database connection
                }
            }
        }

        /* Clear the lock field in research table for an individual fin*/
        public void ClearDupResearchLock(string fin)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.Research set LOCKID = '' where FIN = @FIN";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@FIN", fin);
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
                    connection.Close(); //close database connection
                }
            }
        }

        /* removes the resplock in Respondent. Does it for all cases per user*/
        public void UpdateRespondentLock(string usrnme)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.RESPONDENT set RESPLOCK = ''" + " WHERE RESPLOCK = '" + usrnme + "'";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@RESPLOCK", usrnme);

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
                    connection.Close(); //close database connection
                }
            }
        }


        /* remove the resplock in PreSample. Does it for all cases per user*/
        public void UpdatePreSampleLock(string usrnme)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.PRESAMPLE set RESPLOCK = ''" + " WHERE RESPLOCK = '" + usrnme + "'";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@RESPLOCK", usrnme);

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
                    connection.Close(); //close database connection
                }
            }
        }

        /* remove the resplock in Special case. Does it for all cases per user*/
        public void UpdateSpecialcaseLock(string usrnme)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.Specialcase set LOCKID = ''" + " WHERE LOCKID = '" + usrnme + "'";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@LOCKID", usrnme);

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
                    connection.Close(); //close database connection
                }
            }
        }

         /* remove the resplock in dup research. Does it for all cases per user*/
        public void UpdateDupResearchLock(string usrnme)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.Research set LOCKID = ''" + " WHERE LOCKID = '" + usrnme + "'";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    connection.Open();
                    sql_command.Parameters.AddWithValue("@LOCKID", usrnme);

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
                    connection.Close(); //close database connection
                }
            }
        }
    }
}
