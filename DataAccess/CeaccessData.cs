/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CeaccessData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             id, action
Parameters:	        None 
Outputs:	        None
Description:	    add record to ceaccess table
Detailed Design:    None 
Other:	            Called by: frmImprovement
 
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
    public class CeaccessData
    {
        public void AddCeaccessData(string id, string action)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.ceaccess (STATP, ID, ACTION, USRNME, PRGDTM) "
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
