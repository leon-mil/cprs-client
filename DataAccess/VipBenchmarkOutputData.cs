/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.VIPBenchmarkOutputData.cs
Programmer:         Diane Musachio
Creation Date:      3/8/2019
Inputs:             dbo.VIPBENCH
Parameters:	       
Outputs:	       
Description:	    data layer to get data for VIP Benchmark Output data
Detailed Design:    None
Other:	            Called by: frmBenOutputData.cs

Revision History:	
***************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
****************************************************************************************/
using System.Data;
using System.Data.SqlClient;
using System;
using CprsBLL;

namespace CprsDAL
{
    public class VipBenchmarkOutputData
    {
        //retrieves data from vipBench table
        public DataTable GetBenchmarkOutputData()
        {
          
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select DATE6, V20IXBMO, F12XXBMO, X0360BMO, X1123BMO, X1124BMO, X111XBMO,
                       X1116BMO, X0013BMO from dbo.VIPBENCH order by DATE6 desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }

            return dt;

        }
    }
}
