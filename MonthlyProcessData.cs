/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.MonthlyProcessData.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/28/2016
Inputs:             
Parameters:	        None 
Outputs:	        
Description:	    data layer tor retrieve and update mnthprcss data
Detailed Design:    None 
Other:	            Called by: frmMonthky process
 
Revision History:	
****************************************************************************************
 Modified Date :  11/18/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR  3751 
 Description   :  add special vip load button
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
    public class MonthlyProcessData
    {
       
        //Retrieve all of the Monthly Processing Data from the mnthprcss table. This will
        //display when the page loads

        public DataTable GetMonProcessData()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    SqlCommand sql_command = new SqlCommand("select * from dbo.Mnthprcss order by STATP DESC", sql_connection);

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return dt;
        }

        //Check whether the current stat period exists in the database
        public bool CheckMonthExists(string sStatp)
        {
            bool data_exist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select statp from dbo.Mnthprcss where statp = " + GeneralData.AddSqlQuotes(sStatp);
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
            return data_exist;
        }

        //Get a month process data
        public MonthlyProcessRec GetProcessingForMonth(string sStatp)
        {
            MonthlyProcessRec mp = new MonthlyProcessRec();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "Select TASK01A, TASK01B,TASK02A, TASK02B,TASK03A, TASK03B,TASK04A, TASK04B,TASK05A, TASK05B,TASK06A, TASK06B,TASK07A, TASK07B,TASK08A, TASK08B,TASK09A, TASK09B,TASK10A, TASK10B, TASK11A, TASK11B, TASK12A, TASK12B From dbo.Mnthprcss WHERE Statp = " + GeneralData.AddSqlQuotes(sStatp);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    mp.Task1a = reader["TASK01A"].ToString().Trim();
                    mp.Task1b = reader["TASK01B"].ToString().Trim();
                    mp.Task2a = reader["TASK02A"].ToString().Trim();
                    mp.Task2b = reader["TASK02B"].ToString().Trim();
                    mp.Task3a = reader["TASK03A"].ToString().Trim();
                    mp.Task3b = reader["TASK03B"].ToString().Trim();
                    mp.Task4a = reader["TASK04A"].ToString().Trim();
                    mp.Task4b = reader["TASK04B"].ToString().Trim();
                    mp.Task5a = reader["TASK05A"].ToString().Trim();
                    mp.Task5b = reader["TASK05B"].ToString().Trim();
                    mp.Task6a = reader["TASK06A"].ToString().Trim();
                    mp.Task6b = reader["TASK06B"].ToString().Trim();
                    mp.Task7a = reader["TASK07A"].ToString().Trim();
                    mp.Task7b = reader["TASK07B"].ToString().Trim();
                    mp.Task8a = reader["TASK08A"].ToString().Trim();
                    mp.Task8b = reader["TASK08B"].ToString().Trim();
                    mp.Task9a = reader["TASK09A"].ToString().Trim();
                    mp.Task9b = reader["TASK09B"].ToString().Trim();
                    mp.Task10a = reader["TASK10A"].ToString().Trim();
                    mp.Task10b = reader["TASK10B"].ToString().Trim();
                    mp.Task11a = reader["TASK11A"].ToString().Trim();
                    mp.Task11b = reader["TASK11B"].ToString().Trim();
                    mp.Task12a = reader["TASK12A"].ToString().Trim();
                    mp.Task12b = reader["TASK12B"].ToString().Trim();
                }
                else
                {
                    return null;
                }

            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return mp;
        }



        // If the current stat period does not exist, insert it into the table
        public void AddMonthRow(string sStatp)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    string ssql = "INSERT INTO dbo.Mnthprcss (Statp, Task01A, Task01B, Task02A, Task02B, Task03A, Task03B, Task04A, Task04B,";
                    ssql = ssql + " Task05A, Task05B, Task06A, Task06B, Task07A, Task07B, Task08A, Task08B, Task09A, Task09B, Task10A, Task10B, Task11A, Task11B, Task12A, Task12B)";
                    ssql = ssql + " Values(@sStatp, '', '', '', '', '', '', '','','','', '', '', '', '', '', '', '', '', '','','','', '', '')";

                    SqlCommand sql_command = new SqlCommand(ssql, sql_connection);
                    sql_command.Parameters.Add("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);

                    //Open the connection.
                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable
                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        //Execute the query.
                        sql_command.ExecuteNonQuery();
                    }
                }

                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close(); //close database connection
                }
            }
        }

        //Run process
        public bool RunProcessUpdate(string sStatp, int task, bool run11 = false)
        {
            string tdate = DateTime.Today.ToString("dd-MMM-yyyy").ToUpper();

            string taskA = "";
            string taskB = "";
            if (task < 10)
            {
                taskA = "Task0" + task.ToString() + "A";
                taskB = "Task0" + task.ToString() + "B";
            }
            else
            {
                taskA = "Task" + task.ToString() + "A";
                taskB = "Task" + task.ToString() + "B";
            }
            
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("UPDATE dbo.Mnthprcss SET " + taskA + " = @" + taskA + ", " + taskB + "= @" + taskB + " WHERE [Statp] = @sStatp", sql_connection);

                sql_command.Parameters.AddWithValue("@sStatp", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sStatp);
                sql_command.Parameters.AddWithValue("@"+taskA, tdate);
                if (run11)
                    sql_command.Parameters.AddWithValue("@" + taskB, "4");
                else
                    sql_command.Parameters.AddWithValue("@"+taskB, "3");

                try
                {
                    sql_connection.Open();
                    int count = sql_command.ExecuteNonQuery();
                    if (count > 0)
                        return true;
                    else
                        return false;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close();
                }
            }

        }

    }
}
