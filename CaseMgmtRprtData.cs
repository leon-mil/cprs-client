/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CaseMgmtRprtData.cs	    	

Programmer:         Srini Natarajan
Creation Date:      12/19/2016

Inputs:             Master, Sample, respondent, case_hist, presample, dcpinitial, psamp_hist, dcp_hist.
Parameters:	        month processed(statp) and tab number.

Outputs:	        Case data	
Description:	    This class gets the data from work_hist table

Detailed Design:    None 
Other:	            Called by: frmCaseManagementReport.cs
 
Revision History:	
****************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  ccc
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
    public class CaseMgmtRprtData
    {

        /*Get current month Data for Initial tab*/
        public DataTable GetInitialtab()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_NPCCaseMgmtInit", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        /*Get current month Data for Current tab*/
        public DataTable GetCurrenttab(string statp)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_NPCCaseMgmtCurr", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;
                sql_command.Parameters.Add("@STATP", SqlDbType.Char).Value = GeneralData.NullIfEmpty(statp);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        /* get Previous month data */
        public DataTable GetCaseHistPrevMon(string tab, string statp)
        {
            DataTable dtColtec = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select STATP, TOTALL, TOTPUB, TOTFED, TOTNON, TOTMUL, TOTUTY from CASE_HIST where tab = '" + tab + "' AND STATP = '" + statp + "'";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtColtec);
                }
            }
            return dtColtec;
        }
    }
}
