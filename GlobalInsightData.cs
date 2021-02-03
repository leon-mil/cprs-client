/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.GlobalInsightData.cs	    	
Programmer:         Christine Zhang
Creation Date:      4/18/2017
Inputs:             None
Parameters:	        sdate, premon1, premon2, premon3, premon4
Outputs:	        Global Insight data	
Description:	    data layer to get data for global insight screens
Detailed Design:    None 
Other:	            Called by: frmGlobalInsightMon, frmGlobalInsightAnn
 
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
    public class GlobalInsightData
    {
        /*Retrieve Global Insight month data */
        public DataTable GetGlobalInsightMonData(string sdate, string premon1, string premon2, string premon3, string premon4)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
               
                SqlCommand sql_command = new SqlCommand("dbo.sp_GlobalInsightMon", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@survey_month", SqlDbType.Char).Value = sdate;
                sql_command.Parameters.Add("@premonth1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon1);
                sql_command.Parameters.Add("@premonth2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon2);
                sql_command.Parameters.Add("@premonth3", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon3);
                sql_command.Parameters.Add("@premonth4", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon4);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*Retrieve Global Insight annual data */
        public DataTable GetGlobalInsightAnnData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_GlobalInsightAnn", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@survey_month", SqlDbType.Char).Value = sdate;
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }
    }
}
