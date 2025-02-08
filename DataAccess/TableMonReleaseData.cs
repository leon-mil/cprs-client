/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TableMonReleaseData.cs	    	
Programmer:         Christine Zhang
Creation Date:      8/7/2017
Inputs:             
Parameters:	        survey month, pm1, pm2, pm3, pm4, pm5 
Outputs:	        Monthly Release Table	
Description:	    This class gets the data from series table
Detailed Design:    None 
Other:	            Called by: frmTableMonRelease.cs
Revision History:	
****************************************************************************************
 Modified Date :  3/11/2019
 Modified By   :  Christine
 Keyword       :  
 Change Request:  3002
 Description   :  Create annual tables
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
    public class TableMonReleaseData
    {
        //get data for Seasonally Adjusted Annual Rate
        public DataTable GetTableSAAData(string survey_type, string sdate, string pm1, string pm2, string pm3, string pm4, string pm5)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;
               
                stored_name = "dbo.sp_TableMonReleaseSAA";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                sql_command.Parameters.Add("@premonth1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm1);
                sql_command.Parameters.Add("@premonth2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm2);
                sql_command.Parameters.Add("@premonth3", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm3);
                sql_command.Parameters.Add("@premonth4", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm4);
                sql_command.Parameters.Add("@premonth5", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm5);
                sql_command.Parameters.Add("@survey_type", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey_type);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //reformat column with number format
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        if (row[0].ToString().Trim() != "")
                        {
                            if (i < dt.Columns.Count - 2)
                                row[i] = Convert.ToInt32(row[i]).ToString("N0");
                            else
                                row[i] = Convert.ToDouble(row[i]).ToString("N1");
                        }
                    }
         
                }
            }
            return dt;
        }


        //get table for Not Seasonally Adjusted
        public DataTable GetTableUNAData(string survey_type, string sdate, string pm1, string pm2, string pm3, string pm4, string pm5)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name = "dbo.sp_TableMonReleaseUNA";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                sql_command.Parameters.Add("@premonth1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm1);
                sql_command.Parameters.Add("@premonth2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm2);
                sql_command.Parameters.Add("@premonth3", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm3);
                sql_command.Parameters.Add("@premonth4", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm4);
                sql_command.Parameters.Add("@premonth5", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm5);
                sql_command.Parameters.Add("@survey_type", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey_type);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //reformat column with number format
                foreach (DataRow row in dt.Rows)
                {
                    for (int i = 1; i < dt.Columns.Count; i++)
                    {
                        if (row[0].ToString().Trim() != "")
                        {
                            if (i < dt.Columns.Count - 1)
                                row[i] = Convert.ToInt32(row[i]).ToString("N0");
                            else
                                row[i] = Convert.ToDouble(row[i]).ToString("N1");
                        }
                    } 
                }
            }
            return dt;
        }

        public DataTable GetAnnualReleaseData(string survey_type, string startyear, string endyear)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name = string.Empty;

                if (survey_type == "T")
                    stored_name = "dbo.sp_AnnualReleaseTotal";
                else if (survey_type == "V")
                    stored_name = "dbo.sp_AnnualReleasePrivate";
                else if (survey_type == "P")
                    stored_name = "dbo.sp_AnnualReleasePublic";
                else if (survey_type == "S")
                    stored_name = "dbo.sp_AnnualReleaseSL";
                else
                    stored_name = "dbo.sp_AnnualReleaseFederal";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@startyear", SqlDbType.Char).Value = GeneralData.NullIfEmpty(startyear);
                sql_command.Parameters.Add("@endyear", SqlDbType.Char).Value = GeneralData.NullIfEmpty(endyear);
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

      
    }
}
