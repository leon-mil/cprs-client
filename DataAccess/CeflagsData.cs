/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CeflagsData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             id, flag
Parameters:	        None 
Outputs:	        None
Description:	    data layer to Save ceflag
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
    public class CeflagsData
    {
        /*Save ceflags */
        public bool SaveCeflagsData(string id, string fflag)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE dbo.Ceflags SET " +
                                "CURR_FLAG = @CURR_FLAG, " +
                                "ORIG_FLAG = @ORIG_FLAG " +
                                "WHERE ID = @id";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@ID", id);
                update_command.Parameters.AddWithValue("@CURR_FLAG", fflag);
                update_command.Parameters.AddWithValue("@ORIG_FLAG", fflag);


                try
                {
                    sql_connection.Open();
                    int count = update_command.ExecuteNonQuery();
                    if (count > 0)
                        return true;
                    else
                        return false;
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
    
}
