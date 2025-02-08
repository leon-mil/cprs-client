/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TablePressReleaseData.cs	    	
Programmer:         Christine Zhang
Creation Date:      7/25/2017
Inputs:             
Parameters:	        tableno, sdate, pm1, pm2, pm3, pm4, pm5 
                    mon1, mon2
Outputs:	        Press release Table	
Description:	    This class gets the data from series table
Detailed Design:    None 
Other:	            Called by: frmPressRelease.cs
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
    public class TablePressReleaseData
    {
        //get table 1,or table2 or table3 data
        public DataTable GetTableData(int tableno, string sdate, string pm1, string pm2, string pm3, string pm4, string pm5)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;
                if (tableno == 1)
                    stored_name = "dbo.sp_TablePressReleaseT1";
                else if (tableno == 2)
                    stored_name = "dbo.sp_TablePressReleaseT2";
                else
                    stored_name = "dbo.sp_TablePressReleaseT3";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                sql_command.Parameters.Add("@premonth1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm1);
                sql_command.Parameters.Add("@premonth2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm2);
                sql_command.Parameters.Add("@premonth3", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm3);
                sql_command.Parameters.Add("@premonth4", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm4);
                sql_command.Parameters.Add("@premonth5", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm5);
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //reformat column with number format
                foreach (DataRow row in dt.Rows)
                {
                    if (tableno == 1)
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
                    else if (tableno == 2)
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
            }
            return dt;
        }


        //get table 4 data 
        public DataTable GetTable4Data(string mon1, string mon2)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_TablePressReleaseT4", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@month1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(mon1);
                sql_command.Parameters.Add("@month2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(mon2);

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
                            {
                                row[i] = Convert.ToInt32(row[i]).ToString("N0");
                            }
                            else
                            {
                                if (row[i].ToString().Trim() != "NA")
                                    row[i] = Convert.ToDouble(row[i]).ToString("N1");
                            }
                        }
                    }
                }
            }
            return dt;
        }


    }
}
