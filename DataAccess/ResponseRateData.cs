/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.ResponseRateData.cs	    	
Programmer:         Christine Zhang
Creation Date:      7/11/2019
Inputs:             
Parameters:	        None 
Outputs:	        Response Rate data	
Description:	    data layer to response rate screen
Detailed Design:    None 
Other:	            Called by: frmResponseRate.cs
 
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
    public class ResponseRateData
    {
        public DataTable GetResponseRateTable(string year, string owner, string rate, string rev)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_GetResponseData", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@owner", SqlDbType.Char).Value = GeneralData.NullIfEmpty(owner);
                sql_command.Parameters.Add("@rate", SqlDbType.Char).Value = GeneralData.NullIfEmpty(rate);
                sql_command.Parameters.Add("@rev", SqlDbType.Char).Value = GeneralData.NullIfEmpty(rev);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;

        }
    }
}
