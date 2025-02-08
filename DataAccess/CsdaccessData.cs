/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CsdaccessData.cs	    	
Programmer:         Christine Zhang
Creation Date:      02/01/2011
Inputs:             id, action
Parameters:	        None 
Outputs:	        None
Description:	    add record to ceaccess table
Detailed Design:    None 
Other:	            Called by: frmc700
 
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
    public class CsdaccessData
    {
        public void AddCsdaccessData(string id, string action)
        {
            /* find out database table name */
            string db_table = "dbo.csdaccess";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (STATP, ID, ACTION, USRNME, PRGDTM) "
                        + "Values (@STATP, @ID, @ACTION, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@STATP", DateTime.Now.ToString("yyyyMM"));
            insert_command.Parameters.AddWithValue("@ID", id);
            insert_command.Parameters.AddWithValue("@ACTION", action);
            insert_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
            insert_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

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
    }
}
