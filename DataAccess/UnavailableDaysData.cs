/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.UnavailabledaysData.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/7/2017
Inputs:             None
Parameters:	        year, month, day, flag
Outputs:	        unavailable days data	

Description:	    This function establishes the data connection and reads in 
                    the data from the shed_day table and allows users to update flag 
Detailed Design:    
Other:	            Called by: frmUnavailableDays.cs, frmUnavailableDaysPopup.cs
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
    public class UnavailableDaysData
    {
        //Get Unavilable Days
        public DataTable GetUnavailableDays(string year, string month)
        {
            
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select dayName = case AVLDOFW  When 1 Then 'Sunday' When 2 Then 'Monday' When 3 Then 'Tuesday' When 4 Then 'Wednesday' When 5 Then 'Thursday' When 6 Then 'Friday' When 7 Then 'Saturday' End, apptday From dbo.sched_day where apptyear = @year and apptmnth = @month and avlflag = 'N' ORDER BY Apptday", sql_connection);
                sql_command.Parameters.Add("@year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(month);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //get cut day
        public String GetUnavailableCutDay(string year, string month)
        {
            string cut_day ="";
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("Select apptday From dbo.sched_day where apptyear = '"+ year + "' and apptmnth = '" + month + "' and avlflag = 'C' ", sql_connection);
       
                sql_connection.Open();
                using (SqlDataReader reader = sql_command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                           cut_day = reader["apptday"].ToString();
                        }
                    }
                    reader.Close();
                } 
            }
            return cut_day;
        }

        //check the day is duplicated or not
        public bool CheckDuplicateDay(string month, string day, string year, string flag)
        {
            bool day_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select * from dbo.SCHED_DAY where apptyear = '" + year + "' and apptmnth = '" + month + "'  and apptday = '" + day  + "' and avlflag = " + GeneralData.AddSqlQuotes(flag) ;
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            return true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return day_exist;
        }

        //update unavilable days
        public bool UpdateUnavailableDays(string month, string day, string year, string flag)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.SCHED_DAY SET " +
                                "AVLFLAG= @flag " +
                                "WHERE APPTMNTH = @month and APPTYEAR = @year and APPTDAY = @day";

                SqlCommand sql_command = new SqlCommand(usql, sql_connection);
                sql_command.Parameters.AddWithValue("@month", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(month);
                sql_command.Parameters.AddWithValue("@day", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(day);
                sql_command.Parameters.AddWithValue("@year", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.AddWithValue("@flag", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(flag);

                try
                {
                    int count = sql_command.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }

        }
        
    }
}
