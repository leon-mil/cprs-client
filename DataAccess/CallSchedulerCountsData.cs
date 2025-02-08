/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CallSchedulerCountData.cs	    	
Programmer:         Christine Zhang
Creation Date:      10/2/2017
Inputs:             
Parameters:	        survey, column and row
Outputs:	        None
Description:	    data layer to tabluation for call scheduler count
Detailed Design:    None 
Other:	            Called by: frmCallSchedulerCounts.cs
 
Revision History:	
****************************************************************************************
 Modified Date :  8/11/2020
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR7510
 Description   :  remove the added cases from calculation
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
    public class CallSchedulerCountsData
    {
        //get data for contact tab
        public DataTable GetContactStatusData(string survey)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_CallSchedCountsForContact", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@SURVEY", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //get data for Vip tab
        public DataTable GetVIPStatusData(string survey)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_CallSchedCountsForVIP", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@SURVEY", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }


        //get contact status cases
        public DataTable GetContactStatusCases(string survey, int column, int row)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string owner_string = string.Empty;
                string callstat_string = string.Empty;

                //check survey
                if (survey == "P")
                    owner_string = " and owner = 'N'";
                else if (survey == "S")
                    owner_string = " and owner in ('S', 'L', 'P')";
                else if (survey == "F")
                    owner_string = " and owner in ('C', 'D', 'F')";
                else if (survey == "M")
                    owner_string = " and owner = 'M'";
                else if (survey == "U")
                    owner_string = " and owner in ('T', 'G', 'E', 'O', 'W', 'R')";

                //check callstat
                if (column == 2)
                    callstat_string = " and callstat in ('5', '7', '4', '8', '9', 'V' )";
                else if (column == 3)
                    callstat_string = " and callstat in ('', '1', '2', '3', '6')";
                else if (column == 5)
                    callstat_string = " and callstat = '0'";

                //check row coltec and priority
                string where_string = string.Empty;
                if (row == 0)
                    where_string = " and PRIORITY in ('01', '02', '03', '16', '17','18')";
                else if (row == 1)
                    where_string = " and PRIORITY ='01'";
                else if (row == 2)
                    where_string = " and PRIORITY ='02'";
                else if (row == 3)
                    where_string = " and PRIORITY ='03'";
                else if (row == 4)
                    where_string = " and PRIORITY = '16'";
                else if (row == 5)
                    where_string = " and PRIORITY = '17'";
                else if (row == 6)
                    where_string = " and PRIORITY = '18'";
                else if (row == 7)
                    where_string = " and PRIORITY in ('04', '06', '07', '12', '15','13', '14', '19', '20', '21', '22') ";
                else if (row == 8)
                    where_string = " and PRIORITY in ('07', '12')";
                else if (row == 9)
                    where_string = " and PRIORITY  = '06'";
                else if (row == 10)
                    where_string = " and PRIORITY = '15'";
                else if (row == 11)
                    where_string = " and PRIORITY ='04' ";
                else if (row == 12)
                    where_string = " and PRIORITY ='13'";
                else if (row == 13)
                    where_string = " and PRIORITY ='14'";
                else if (row == 14)
                    where_string = " and PRIORITY = '22'";
                else if (row == 15)
                    where_string = " and PRIORITY = '19'";
                else if (row == 16)
                    where_string = " and PRIORITY = '20'";
                else if (row == 17)
                    where_string = " and PRIORITY = '21'";
                else if (row ==18)
                    where_string = " and PRIORITY in ('05', '08', '09', '10', '11')";
                else if (row == 19)
                    where_string = " and PRIORITY = '05'";
                else if (row == 20)
                    where_string = " and PRIORITY = '08'";
                else if (row == 21)
                    where_string = " and PRIORITY = '09'";
                else if (row == 22)
                    where_string = " and PRIORITY = '10'";
                else if (row == 23)
                    where_string = " and PRIORITY = '11'";
                else if (row == 24)
                    where_string = " and PRIORITY = '23'";

                string sqlQuery = @"SELECT c.ID, s.Respid, r.coltec, colhist, owner, newtc, strtdate, compdate, rvitm5c from dbo.sample s, dbo.master m, dbo.sched_call c, dbo.respondent r  where s.masterid = m.masterid and c.id = s.id and s.respid = r.respid and ADDED = 'N'";
                sqlQuery = sqlQuery + where_string + owner_string + callstat_string + " order by ID"; 
                using (SqlCommand sql_command = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(sql_command);
                    ds.Fill(dt);
                }

              
            }

            return dt;
        }

        //get VIP status cases
        public DataTable GetVIPStatusCases(string survey, int column, int row)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string owner_string = string.Empty;
                string callstat_string = string.Empty;

                //check survey
                if (survey == "P")
                    owner_string = " and owner = 'N'";
                else if (survey == "S")
                    owner_string = " and owner in ('S', 'L', 'P')";
                else if (survey == "F")
                    owner_string = " and owner in ('C', 'D', 'F')";
                else if (survey == "M")
                    owner_string = " and owner = 'M'";
                else if (survey == "U")
                    owner_string = " and owner in ('T', 'G', 'E', 'O', 'W', 'R')";

                //check callstat, accescde
                if (column == 2)
                    callstat_string = " and callstat = 'V'";
                else if (column == 3)
                    callstat_string = " and callstat <> 'V'";
                else if (column == 5)
                    callstat_string = " and accescde = 'F'";
                else if (column == 6)
                    callstat_string = " and accescde = 'C'";
                else if (column == 7)
                    callstat_string = " and accescde = 'P'";
                else if (column == 8)
                    callstat_string = " and accescde = 'I'";
                else if (column == 9)
                    callstat_string = " and accescde = 'A'";

                //check row priority
                string where_string = string.Empty;
                if (row == 0)
                    where_string = " and PRIORITY in ('01', '02', '03', '16', '17','18')";
                else if (row == 1)
                    where_string = " and PRIORITY ='01''";
                else if (row == 2)
                    where_string = " and PRIORITY ='02' ";
                else if (row == 3)
                    where_string = " and PRIORITY ='03'";
                else if (row == 4)
                    where_string = " and PRIORITY = '16'";
                else if (row == 5)
                    where_string = " and PRIORITY = '17'";
                else if (row == 6)
                    where_string = " and PRIORITY = '18'";
                else if (row == 7)
                    where_string = " and PRIORITY in ('04', '06', '07', '12', '15','13', '14', '19', '20', '21', '22') ";
                else if (row == 8)
                    where_string = " and PRIORITY in ('07', '12')";
                else if (row == 9)
                    where_string = " and PRIORITY  = '06'";
                else if (row == 10)
                    where_string = " and PRIORITY = '15'";
                else if (row == 11)
                    where_string = " and PRIORITY ='04' ";
                else if (row == 12)
                    where_string = " and PRIORITY ='13'";
                else if (row == 13)
                    where_string = " and PRIORITY ='14'";
                else if (row == 14)
                    where_string = " and PRIORITY = '22'";
                else if (row == 15)
                    where_string = " and PRIORITY = '19'";
                else if (row == 16)
                    where_string = " and PRIORITY = '20'";
                else if (row == 17)
                    where_string = " and PRIORITY = '21'";
                else if (row == 18)
                    where_string = " and PRIORITY in ('05', '08', '09', '10', '11')";
                else if (row == 19)
                    where_string = " and PRIORITY = '05'";
                else if (row == 20)
                    where_string = " and PRIORITY = '08'";
                else if (row == 21)
                    where_string = " and PRIORITY = '09'";
                else if (row == 22)
                    where_string = " and PRIORITY = '10'";
                else if (row == 23)
                    where_string = " and PRIORITY = '11'";
                else if (row == 24)
                    where_string = " and PRIORITY = '23'";

                string sqlQuery = @"SELECT c.ID, s.Respid, r.coltec, colhist, owner, newtc, strtdate, compdate, rvitm5c from dbo.sample s, dbo.master m, dbo.sched_call c, dbo.respondent r  where s.masterid = m.masterid and c.id = s.id and s.respid = r.respid and added='N'";
                sqlQuery = sqlQuery + where_string + owner_string + callstat_string + " order by ID";
                using (SqlCommand sql_command = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(sql_command);
                    ds.Fill(dt);
                }

            }

            return dt;
        }

        public DataTable GetEmptyTable()
        {
            // Here we create a DataTable with 9 columns.

            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("RESPID", typeof(string));
            table.Columns.Add("COLTEC", typeof(char));
            table.Columns.Add("COLHIST", typeof(string));

            table.Columns.Add("OWNER", typeof(char));
            table.Columns.Add("NEWTC", typeof(string));
            table.Columns.Add("STRTDATE", typeof(string));
            table.Columns.Add("COMPDATE", typeof(string));
            table.Columns.Add("RVITM5C", typeof(int));
            return table;
        }


    }
}
