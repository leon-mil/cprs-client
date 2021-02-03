/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.SpecStrtData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/29/2018
Inputs:             data record
Parameters:	        None 
Outputs:	        None
Description:	    data layer to add special strtdate annual data
Detailed Design:    None 
Other:	            Called by: frmSpecStrt.cs
 
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
    public class SpecStrtData
    {
        /*Get special strtdate data */
        public DataTable GetSpecStrtDateData(string selyear, string selsurvey, string selvalue)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("sp_TSpecialStrtdate", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@selyear", SqlDbType.Char).Value = GeneralData.NullIfEmpty(selyear);
                sql_command.Parameters.Add("@selsurvey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(selsurvey);
                sql_command.Parameters.Add("@selvalue", SqlDbType.Char).Value = GeneralData.NullIfEmpty(selvalue);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*get special strtdate summary data */
        public DataTable GetSpecStrtDateSumData(string selsurvey, string selvalue)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("sp_TSpecialStrtdateSummary", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@selsurvey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(selsurvey);
                sql_command.Parameters.Add("@selvalue", SqlDbType.Char).Value = GeneralData.NullIfEmpty(selvalue);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            dt.Columns[0].Caption = "CATEGORY";

            string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            int cur_mon = Convert.ToInt16(sdate.Substring(4, 2));
            int cur_year = Convert.ToInt16(sdate.Substring(0, 4));



            if (cur_mon >= 6)
            {
                dt.Columns[1].Caption = (cur_year - 2).ToString();
                dt.Columns[2].Caption = (cur_year - 1).ToString();
                dt.Columns[3].Caption = cur_year.ToString() + " (6 Mons)";
            }
            else if (cur_mon <=1)
            {
                dt.Columns[1].Caption = (cur_year - 3).ToString();
                dt.Columns[2].Caption = (cur_year - 2).ToString();
                dt.Columns[3].Caption = (cur_year - 1).ToString() + " (6 Mons)"; 
            }
            else
            {
                dt.Columns[1].Caption = (cur_year - 3).ToString();
                dt.Columns[2].Caption = (cur_year - 2).ToString();
                dt.Columns[3].Caption = (cur_year-1).ToString();
            }

            dt.Rows[0][0] = "3 + Prior (late)";
            dt.Rows[1][0] = "3 + After (abeyance)";
            dt.Rows[2][0] = "Within Target Range";

            return dt;
        }

        /*Get strtdate project over 10000 value */
        public DataTable GetSpecStrtDateProjData(string selyear)
        {
            DataTable dt = new DataTable();
            DataTable dtt = new DataTable();
            dtt.Columns.Add("Survey");
            dtt.Columns.Add("s0");
            dtt.Columns.Add("s1");
            dtt.Columns.Add("s2");
            dtt.Columns.Add("s3");
            dtt.Columns.Add("s4");
            dtt.Columns.Add("s5");
            dtt.Columns.Add("s6");
            dtt.Columns.Add("s7");
            dtt.Columns.Add("s8");
            dtt.Columns.Add("s9");

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("sp_TSpecialStrtdateProj", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@selyear", SqlDbType.Char).Value = GeneralData.NullIfEmpty(selyear);
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            string sname = string.Empty;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                if (dr["survey"].ToString() != "%")
                {
                    if (dr["survey"].ToString() == "F")
                        sname = "Federal";
                    else if (dr["survey"].ToString() == "P")
                        sname = "Public";
                    else if (dr["survey"].ToString() == "U")
                        sname = "Utilities";
                    else if (dr["survey"].ToString() == "N")
                        sname = "Nonres";
                    else
                        sname = "All";
                }
                
                if (dr["survey"].ToString() == "%")
                { 
                    if (dr["C9"].ToString() != "0")
                      dtt.Rows.Add(dr["survey"].ToString(), dr["C0"].ToString(), dr["C1"].ToString(), dr["C2"].ToString(), dr["C3"].ToString(), dr["C4"].ToString(), dr["C5"].ToString(), dr["C6"].ToString(), dr["C7"].ToString(), dr["C8"].ToString(), dr["C9"].ToString());
                    else
                      dtt.Rows.Add(dr["survey"].ToString(), dr["C0"].ToString(), dr["C1"].ToString(), dr["C2"].ToString(), dr["C3"].ToString(), dr["C4"].ToString(), dr["C5"].ToString(), dr["C6"].ToString(), dr["C7"].ToString(), dr["C8"].ToString(), "");
                }
                else
                {
                    if (dr["C9"].ToString() != "0")
                        dtt.Rows.Add(sname, Convert.ToInt32(dr["C0"]).ToString(), Convert.ToInt32(dr["C1"]).ToString(), Convert.ToInt32(dr["C2"]).ToString(), Convert.ToInt32(dr["C3"]).ToString(), Convert.ToInt32(dr["C4"]).ToString(), Convert.ToInt32(dr["C5"]).ToString(), Convert.ToInt32(dr["C6"]).ToString(), Convert.ToInt32(dr["C7"]).ToString(), Convert.ToInt32(dr["C8"]).ToString(), Convert.ToInt32(dr["C9"]).ToString());
                    else
                        dtt.Rows.Add(sname, Convert.ToInt32(dr["C0"]).ToString(), Convert.ToInt32(dr["C1"]).ToString(), Convert.ToInt32(dr["C2"]).ToString(), Convert.ToInt32(dr["C3"]).ToString(), Convert.ToInt32(dr["C4"]).ToString(), Convert.ToInt32(dr["C5"]).ToString(), Convert.ToInt32(dr["C6"]).ToString(), Convert.ToInt32(dr["C7"]).ToString(), Convert.ToInt32(dr["C8"]).ToString(), "");
                }
            }
    
            dtt.Columns[0].Caption = "Months";
            dtt.Columns[1].Caption = "-4";
            dtt.Columns[2].Caption = "-3";
            dtt.Columns[3].Caption = "-2";
            dtt.Columns[4].Caption = "-1";
            dtt.Columns[5].Caption = "0";
            dtt.Columns[6].Caption = "1";
            dtt.Columns[7].Caption = "2";
            dtt.Columns[8].Caption = "3";
            dtt.Columns[9].Caption = "4";
            dtt.Columns[10].Caption = "Total";

            return dtt;
        }
    }
}
