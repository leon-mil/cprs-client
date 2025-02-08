/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.DistributionData.cs	    	
Programmer:         Christine Zhang
Creation Date:      7/13/2017
Inputs:             
Parameters:	        Newtc, survey, col, region, division, fipstate
Outputs:	        distribution data	
Description:	    This class gets the data from vipproj table

Detailed Design:    None 
Other:	            Called by: frmDistribution.cs
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
    public class DistributionData
    {
        public DataTable GetDistributionData(string col, string survey, string tc, string region, string division, string fipstate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_Distribution", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                
                sql_command.Parameters.Add("@COL", SqlDbType.Char).Value = col;
                sql_command.Parameters.Add("@Survey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey);
                sql_command.Parameters.Add("@tc", SqlDbType.Char).Value = GeneralData.NullIfEmpty(tc);
                sql_command.Parameters.Add("@region", SqlDbType.Char).Value = GeneralData.NullIfEmpty(region);
                sql_command.Parameters.Add("@division", SqlDbType.Char).Value = GeneralData.NullIfEmpty(division);
                sql_command.Parameters.Add("@fipstate", SqlDbType.Char).Value = GeneralData.NullIfEmpty(fipstate);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);

                da.Fill(dt);
            }

            if (dt.Rows.Count >0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    row[0] = row[0].ToString().TrimEnd();
                }

            }

            return dt;
        }
    }
}
