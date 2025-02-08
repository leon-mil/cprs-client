/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.NPCActiveLoadData.cs	    	

Programmer:         Srini Natarajan
Creation Date:      10/18/2015

Inputs:             WORK_HIST table.
Parameters:	        coltec and month processed.(statp)

Outputs:	        Active load data	
Description:	    This class gets the data from WORK_HIST table

Detailed Design:    None 
Other:	            Called by: frmNPCActiveLoad.cs
 
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
    public class NPCActiveLoadData

    {
        // PREVIOUS MONTHS - get data from CURR_HIST table for NPC Workload Report Form.
        public DataTable GetColtecForm(string colTec, string statp)
        {
            DataTable dtColtec = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select ACCESCDE, TOTALL, TOTPUB, TOTFED, TOTNON, TOTMUL, TOTUTY from WORK_HIST where COLTEC = '" + colTec + "' AND STATP = '" + statp + "'";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtColtec);
                }
            }
            return dtColtec;
        }

        /*Get first month Data */
        public DataTable GetFirstMonthData(string coltec)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_NPCWorkLoadReport", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                if (coltec == "X")
                    sql_command.Parameters.Add("@COLTEC", SqlDbType.Char).Value = DBNull.Value;
                else
                    sql_command.Parameters.Add("@COLTEC", SqlDbType.Char).Value = GeneralData.NullIfEmpty(coltec);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        /*Populate the lower grid for the current month */
        public DataTable getLwrGrd(string swhere)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT DISTINCT sc.ID, s.RESPID, sc.COLTEC, COLHIST, OWNER,NEWTC, STRTDATE, COMPDATE, FORMAT(RVITM5C, 'N0') as RVITM5C, ACCESCDE, CALLSTAT, CASE WHEN c.MARKTEXT IS NOT NULL THEN 'Y' WHEN rm.MARKTEXT is not null then 'Y' ELSE 'N' 
                                END AS MARKED from dbo.sched_call sc inner join dbo.sample s on sc.id = s.id inner join dbo.master m on s.masterid = m.masterid inner join dbo.respondent r on s.respid = r.respid
                                left join dbo.csdmark c on s.ID = c.id left join dbo.RSPMARK rm on s.respid = rm.RESPID " + swhere;
                using (SqlCommand sql_command = new SqlCommand(sqlQuery, sql_connection))
                {
                    sql_command.Parameters.Add("@COLTEC", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(swhere);
                    SqlDataAdapter ds = new SqlDataAdapter(sql_command);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

    }
}
