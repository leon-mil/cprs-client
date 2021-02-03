
/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CurrentUsers.cs	    	
Programmer:         Christine Zhang
Creation Date:      8/1/2016
Inputs:             None
Parameters:	        None 
Outputs:	        current users data	
Description:	    data layer to add, delete, current user
Detailed Design:    None 
Other:	            Called by: frmwelcom, frmCprsparent, frmCurrentUsers
 
Revision History:	
***************************************************************************************
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
    public class CurrentUsersData
    {
        //Get current session from current_users table
        public int GetSessionInfo()
        {
            int session= 0;
            int count = 0;
            int new_session = 0;

            //get user name from environment variable, that was stored at startup
            string user_name = UserInfo.UserName;

            //get userid, groupcode and printq from sched_id table
            if (!String.IsNullOrEmpty(user_name))
            {
                using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    string sql = "SELECT SESSION FROM dbo.CURRENT_USERS WHERE USRNME = " + GeneralData.AddSqlQuotes(user_name) + " order by session ASC";
                    SqlCommand command = new SqlCommand(sql, connection);

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            count = 0;
                            while (reader.Read())
                            {
                                if (reader["session"].ToString() != "")
                                {
                                    session = Convert.ToInt32(reader["session"]);
                                    count++;
                                }
                            }
                        }
                        reader.Close();
                    }
                    connection.Close();
                }
            }

            if ((count == 2) || (count ==1 && session ==1) || (count==0))
                new_session = session + 1;
            else if (count ==1 && session ==2)
                new_session = 1;

            return new_session;

        }

        //add a record to current users table
        public void AddCurrentUsersData(string module)
        {
            /* find out database table name */
            string db_table = "dbo.CURRENT_USERS";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (USRNME, SESSION, TIMEIN, MODULE) "
                        + "Values (@USRNME, @SESSION, @TIMEIN, @MODULE)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
            insert_command.Parameters.AddWithValue("@SESSION", GlobalVars.Session);
            insert_command.Parameters.AddWithValue("@TIMEIN", DateTime.Now.ToString("HH:mm:ss"));
            insert_command.Parameters.AddWithValue("@MODULE", module);

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

        //add a record to current users table
        public bool DeleteCurrentUsersData()
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string usql = "DELETE FROM dbo.CURRENT_USERS WHERE USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName) + " AND SESSION = " + GlobalVars.Session;

            SqlCommand delete_command = new SqlCommand(usql, sql_connection);

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

        }


        //Get current users table
        public DataTable GetCurrentUsersData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select * from dbo.CURRENT_USERS ORDER BY USRNME, SESSION", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        //Check other users exists
        public bool CheckOtherUsersExist()
        {
            bool user_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand command = new SqlCommand("Select distinct USRNME from dbo.CURRENT_USERS where USRNME <>" + GeneralData.AddSqlQuotes(UserInfo.UserName), connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        user_exist = true;
                    }
                    reader.Close();
                }
                connection.Close();
            }

            return user_exist;
        }

    }
}
