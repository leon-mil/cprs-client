/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.LotlockData.cs	    	
Programmer:         Christine Zhang
Creation Date:      4/30/2019
Inputs:             
Parameters:	        None 
Outputs:	        lot lock data	
Description:	    data layer to check lock or update
Detailed Design:    None 
Other:	            Called by: frmSpecTimeLenWorksheet
 
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
    public class LotlockData
    {
        //check whether the tc has been locked or not
        public string GetLotLock(string owner)
        {
            string locked_by = "";
            try
            {
                SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

                string Query = "SELECT USRNME from dbo.LOTLOCK WHERE Owner =" + GeneralData.AddSqlQuotes(owner);

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

        //Update lotlock table, if locktable = true, update usrnme with user name
        //otherwise set usrnme to blank
        public bool UpdateLotLock(string owner, bool locktable)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                string usql = "UPDATE dbo.LOTLOCK SET " +
                                "USRNME = @USRNME " +
                                "WHERE OWNER = @owner";
                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                if (locktable)
                    update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                else
                    update_command.Parameters.AddWithValue("@USRNME", "");
                update_command.Parameters.AddWithValue("@owner", owner);
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
