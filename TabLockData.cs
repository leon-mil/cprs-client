/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TabLockData.cs	    	
Programmer:         Christine Zhang
Creation Date:      1/10/2017
Inputs:             
Parameters:	        None 
Outputs:	        Tab Lock data	
Description:	    data layer to check lock or update
Detailed Design:    None 
Other:	            Called by: frmTotalvip
 
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
    public class TabLockData
    {
        //check whether the tc has been locked or not
        public string GetTabLock(string tc)
        {
            string locked_by = "";
            try
            {
                SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

                string Query = "SELECT USRNME from dbo.TABLOCK WHERE TC2 =" + GeneralData.AddSqlQuotes(tc);

                SqlCommand sql_command = new SqlCommand(Query, connection);

                connection.Open();

                using (SqlDataReader reader = sql_command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        locked_by = reader["USRNME"].ToString();
                    }
                    
                    reader.Close();
                    connection.Close();
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return locked_by.Trim();
        }

        //Update Tablock table, if locktable = true, update usrnme with user name
        //otherwise set usrnme to blank
        public bool UpdateTabLock(string tc, bool locktable)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                string usql = "UPDATE dbo.TABLOCK SET " +
                                "USRNME = @USRNME " +
                                "WHERE TC2 = @TC2";
                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                if (locktable)
                    update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                else
                    update_command.Parameters.AddWithValue("@USRNME", "");
                update_command.Parameters.AddWithValue("@TC2", tc);
                try
                {
                    int count = update_command.ExecuteNonQuery();
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


        }
    }
}
