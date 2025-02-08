/**************************************************************************************
Econ App Name:      CPRS
 
Project Name:       CPRS Interactive Screens System
 
Program Name:       NPC Attempts Report
 
Programmer:         Diane Musachio
 
Creation Date:      11/22/2016
 
Inputs:             Stored Procedures: dbo.sp_NPCAttemptsbyDay, dbo.sp_NPCAttemptsbyUser
                    Tables: sched_hist, sched_id, psamp_hist, dcp_hist, attm_hist
 
Parameters:	        selected statperiod, selected access date, selected interviewer/user 

Outputs:	        NPC attempts by interviewer and by date
                    NPC cases by interviewer, by date or by total
 
Description:	    This program will display NPC Attempts Report by total,
                    by date or by interviewer

Detailed Design:    Detailed User Requirements for NPC Attempts Report 
 
Other:	            Called by: frmNPCAttempts.cs, frmNPCAttemptsCases.cs
 
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
    public class NPCAttemptsData
    {
        //get data by interviewer for current month
        public DataTable GetInterviewerData(string user)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_NPCAttemptsbyUser", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;
                sql_command.Parameters.Add("@USER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(user);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //get date by date for current month
        public DataTable GetCurrMonthData(string day)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_NPCAttemptsbyDay", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;
                sql_command.Parameters.Add("@DAY", SqlDbType.Char).Value = GeneralData.NullIfEmpty(day);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }
       
       //get data totals by months for prior months
        public DataTable GetNPCAttemptsDay(object statp, string accesday)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select usrnme, totinit, totstsfd, totcontact, totpoc, totresch, totbusy, totring, " +
                    " totdscnt, totrefus, totprorep, totlvm, totform, totinter, totattm, totrefer, tothour, " +
                    " totmins  " +
                    " from dbo.ATTM_HIST " +
                    " where STATP = " + statp.ToString() + " and accesday = " + GeneralData.AddSqlQuotes(accesday) + " order by totattm desc, usrnme";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //get data fby interviewer for prior months
        public DataTable GetNPCAttemptsPastInterviewer(object statp, string user)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select accesday, totinit, totstsfd,totcontact, totpoc, totresch, totbusy, totring, " +
                    " totdscnt, totrefus, totprorep, totlvm, totform, totinter, totattm,totrefer, tothour, " +
                    " totmins " +
                    " from dbo.ATTM_HIST " +
                    " where STATP = " + statp.ToString() + " and usrnme = " + GeneralData.AddSqlQuotes(user);

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //Get popup data by interviewer and accessdate for current month
        public DataTable GetPopupData(string interviewer, string accessdate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select sh.id, sh.accesday, sh.accestms, " +
                          " sh.accestme, sh.accescde, sh.callstat, si.grpcde, si.usrnme, sh.accesnme " +
                          " from dbo.SCHED_HIST sh, dbo.SCHED_ID si " +
                          " where sh.ACCESNME = " + GeneralData.AddSqlQuotes(interviewer) +
                          " and sh.ACCESDAY = " + GeneralData.AddSqlQuotes(accessdate) +
                          " and sh.ACCESNME = si.usrnme ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data for initials column for current month
        //if user is in either psamp hist or dcp hist on accessdate the user will be displayed in popup datagrid column
        public DataTable GetPopupInits(string interviewer, string accessdate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select psamp.id, psamp.accesday, psamp.accestms, " +
                          " psamp.accestme, psamp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.PSAMP_HIST psamp, dbo.SCHED_ID si" +
                          " where si.usrnme = psamp.accesnme" +
                          " and psamp.accesnme = " + GeneralData.AddSqlQuotes(interviewer) +
                          " and psamp.accesday = " + GeneralData.AddSqlQuotes(accessdate) +
                          " union " +
                          " select dcp.id, dcp.accesday, dcp.accestms, dcp.accestme, " +
                          " dcp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.DCP_HIST dcp, dbo.SCHED_ID si " +
                          " where dcp.accesnme = si.usrnme" +
                          " and dcp.accesnme = " + GeneralData.AddSqlQuotes(interviewer) +
                          " and dcp.accesday = " + GeneralData.AddSqlQuotes(accessdate);

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data for initials column for current month
        //if user is in either psamp hist or dcp hist on accessdate the user will be displayed in popup datagrid column
        public DataTable GetPopupInitsTotalInterviewer(string interviewer)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select psamp.id,  psamp.accesday, psamp.accestms, " +
                          " psamp.accestme, psamp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.PSAMP_HIST psamp, dbo.SCHED_ID si" +
                          " where si.usrnme = psamp.accesnme" +
                          " and psamp.accesnme = " + GeneralData.AddSqlQuotes(interviewer) +
                          " union  " +
                          " select dcp.id,  dcp.accesday, dcp.accestms, dcp.accestme, " +
                          " dcp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.DCP_HIST dcp, dbo.SCHED_ID si " +
                          " where dcp.accesnme = si.usrnme" +
                          " and dcp.accesnme = " + GeneralData.AddSqlQuotes(interviewer);

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data for initials column for current month
        //if user is in either psamp hist or dcp hist on accessdate the user will be displayed in popup datagrid column
        public DataTable GetPopupInitsTotalDate( string accessdate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select psamp.id, psamp.accesday, psamp.accestms, " +
                          " psamp.accestme, psamp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.PSAMP_HIST psamp, dbo.SCHED_ID si" +
                          " where si.usrnme = psamp.accesnme" +
                          " and psamp.accesday = " + GeneralData.AddSqlQuotes(accessdate) +
                          " union  " +
                          " select dcp.id, dcp.accesday, dcp.accestms, dcp.accestme, " +
                          " dcp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.DCP_HIST dcp, dbo.SCHED_ID si " +
                          " where dcp.accesnme = si.usrnme" +
                          " and dcp.accesday = " + GeneralData.AddSqlQuotes(accessdate);

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data for initials column for current month
        //if user is in either psamp hist or dcp hist on accessdate the user will be displayed in popup datagrid column
        public DataTable GetPopupInitsTotals()
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select psamp.id, psamp.accesday, psamp.accestms, " +
                          " psamp.accestme, psamp.accesnme, si.grpcde, si.usrnme " +
                          " from dbo.PSAMP_HIST psamp, dbo.SCHED_ID si" +
                          " where si.usrnme = psamp.accesnme" +
                          " union" +
                          " select dcp.id, dcp.accesday, dcp.accestms, dcp.accestme, dcp.accesnme, " +
                          " si.grpcde, si.usrnme " +
                          " from dbo.DCP_HIST dcp, dbo.SCHED_ID si " +
                          " where dcp.accesnme = si.usrnme";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data totals by date for current month
        public DataTable GetPopupTotalDate(string accessdate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
               string sqlQuery = @"select sh.id, sh.accesday, sh.accestms, " +
                         " sh.accestme, sh.accescde, sh.callstat, si.grpcde, si.usrnme, sh.accesnme " +
                         " from dbo.SCHED_HIST sh, dbo.SCHED_ID si " +
                         " where sh.ACCESDAY = " + GeneralData.AddSqlQuotes(accessdate) +
                         " and sh.ACCESNME = si.usrnme ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data totals by interviewer for current month 
        public DataTable GetPopupDataTotInterviewer(string interviewer)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select sh.id, sh.accesday, sh.accestms, " +
                          " sh.accestme, sh.accescde, sh.callstat, si.grpcde, si.usrnme, sh.accesnme " +
                          " from dbo.SCHED_HIST sh, dbo.SCHED_ID si " +
                          " where sh.ACCESNME = " + GeneralData.AddSqlQuotes(interviewer) +
                          " and sh.ACCESNME = si.usrnme ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        //Get popup data overall totals for current month
        public DataTable GetPopupTotal()
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select sh.id, sh.accesday, sh.accestms, " +
                          " sh.accestme, sh.accescde, sh.callstat, si.grpcde, si.usrnme, sh.accesnme " +
                          " from dbo.SCHED_HIST sh, dbo.SCHED_ID si " +
                          " where sh.ACCESNME = si.usrnme ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.CommandTimeout = 0;
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

    }
}
