/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TableSpecialPrivateData.cs	    	
Programmer:         Christine Zhang
Creation Date:      6/27/2017
Inputs:             
Parameters:	        Series 
Outputs:	        Special Private Table	
Description:	    This class gets the data from UNAPRV, SAAPRV table
Detailed Design:    None 
Other:	            Called by: frmTableSpecialPrivate.cs
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
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using CprsBLL;

namespace CprsDAL
{
    public class TableSpecialPrivateData
    {
        public DataTable GetSpecialPrivateTable(string seriesname)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_TableSpecialPrivate", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                
                sql_command.Parameters.Add("@Series", SqlDbType.Char).Value = GeneralData.NullIfEmpty(seriesname);
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
