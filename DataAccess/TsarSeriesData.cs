/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TsarSeriesData.cs	    	
Programmer:         Christine Zhang
Creation Date:      05/3/2017
Inputs:             None
Parameters:	        survey, toc, type and level
Outputs:	        Tsar Series data	
Description:	    This function establishes the data connection and get series
                     data.                   
Detailed Design:    None 
Other:	            Called by: frmTsarSeriesViewer.cs, frmTsarSeriesSelectionPopup.cs
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
    public class TsarSeriesData
    {
        //Check Serie exist in the table or not
        public bool CheckSeriesExist(string seriestable, string seriesname)
        {
            bool SeriesExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + seriestable + "') and name = " + GeneralData.AddSqlQuotes(seriesname);
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
            return SeriesExist;
        }

        //Get series name list from popup screen, and pass survey, toc, type and level
        public DataTable GetTsarSeries(string survey, string toc, string type, int level)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(String));

            /* get data table */
            string db_table = SetupSerieTable(survey, type);
            
            string series = survey+toc+type;
            string sql_stat = string.Empty;

            if (survey == "T" || survey == "P")
            {
                if (toc != "20")
                    series = survey + toc + "XX" + type;
                else
                    series = survey + toc + "IX" + type;
                
                DataRow dr = dt.NewRow();
                dr["Name"] = series;
                dt.Rows.Add(dr);
                
            }
            else
            {
                if (level == 2)
                {
                    if (toc != "20")
                    {
                        sql_stat = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + db_table + "') and name <> 'DATE6' and (substring(name, 4,2) = 'XX') and substring(name, 2,2)=" + toc.AddSqlQuotes();
                    }
                    else
                        sql_stat = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + db_table + "') and name <> 'DATE6' and ((substring(name, 4,2) = 'IX') or (substring(name, 4,2) = 'XX')) and substring(name, 2,2) > '19'";

                    using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                    {
                        SqlCommand sql_command = new SqlCommand(sql_stat, sql_connection);

                        // Create a DataAdapter to run the command and fill the DataTable
                        SqlDataAdapter da = new SqlDataAdapter(sql_command);
                        da.Fill(dt);
                    }
                }
                else if (level == 3)
                {
                    
                    if (toc != "20")
                        sql_stat = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + db_table + "') and name <> 'DATE6' and (substring(name, 4,1) <> 'X' and (substring(name, 5,1) = 'X' or substring(name,5,1) = 'C')) and substring(name, 2,2)=" + toc.AddSqlQuotes();
                    else
                        sql_stat = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + db_table + "') and name <> 'DATE6' and ((substring(name, 4,2) <> 'IX' and substring(name, 4,2) <> 'XX') and (substring(name, 5,1) = 'X' or substring(name,5,1) = 'C')) and substring(name, 2,2) > '19' and substring(name, 2,2) <> 'XX'";
                    
                    
                    using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                    {
                        SqlCommand sql_command = new SqlCommand(sql_stat, sql_connection);

                        // Create a DataAdapter to run the command and fill the DataTable
                        SqlDataAdapter da = new SqlDataAdapter(sql_command);
                        da.Fill(dt);
                    }
                }
                else
                {
                    if (toc != "20")
                        sql_stat = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + db_table + "') and name <> 'DATE6' and (substring(name, 4,1) <> 'X' and (substring(name, 5,1) <> 'X') and substring(name, 5,1) <> 'C') and substring(name, 2,2)= " + toc.AddSqlQuotes(); 
                    else
                        sql_stat = "SELECT Name FROM sys.columns WHERE object_id = OBJECT_ID('dbo." + db_table + "') and name <> 'DATE6' and (substring(name, 4,1) <> 'X' and (substring(name, 5,1) <> 'X') and substring(name, 5,1) <> 'C') and substring(name, 2,2)> '19'";
                    using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                    {
                        SqlCommand sql_command = new SqlCommand(sql_stat, sql_connection);

                        // Create a DataAdapter to run the command and fill the DataTable
                        SqlDataAdapter da = new SqlDataAdapter(sql_command);
                        da.Fill(dt);
                    }

                }
            }

            //in Tsarsfc table,filter row based on survey
            if (type == "SFC" && dt.Rows.Count>0)
            {
                DataTable boundTable = new DataTable();
               
                if (survey == "V")
                {
                    var myInClause = new string[] { "V", "N", "X" };

                    var query = from myRow in dt.AsEnumerable()
                            where myInClause.Contains(myRow.Field<string>("name").Substring(0, 1))
                            select myRow;
                    if (query.Count() > 0)
                        boundTable = query.CopyToDataTable<DataRow>();

                    return boundTable;
                }
                else if (survey == "S")
                {
                    var query = from myRow in dt.AsEnumerable()
                                where (myRow.Field<String>("name").Substring(0, 1) == "S")
                                select myRow;
                    if (query.Count() > 0)
                        boundTable = query.CopyToDataTable<DataRow>();
                }

                else if (survey == "F")
                {
                    var query = from myRow in dt.AsEnumerable()
                                where (myRow.Field<String>("name").Substring(0, 1) == "F")
                                select myRow;
                    if (query.Count() > 0)
                        boundTable = query.CopyToDataTable<DataRow>();
                }

                return boundTable;
            }

            return dt;
        }

      
        //set up serie table based on survey and type
        private string SetupSerieTable(string survey, string type)
        {
            string db ="UNATOT";
            if (type == "SFC" && survey != "T" && survey != "P")
                db = "TSARSFC";
            else
            {
                if (survey == "T" || survey == "P")
                {
                    db = type + "TOT";
                }
                else if (survey == "V")
                {
                    db = type + "PRV";
                }
                else if (survey == "S")
                {
                    db = type + "PUB";
                }
                else if (survey == "F")
                {
                    db = type + "FED";
                }
                else if (survey == "U")
                    db = type + "UTL";
            }

            return db;
        }

        //get series data for series viewer
        public DataTable GetTsarSeriesData(string stable, string seriesname)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

                SqlCommand sql_command = new SqlCommand("Select DATE6, " + seriesname + " From dbo." + stable + " where DATE6 <= '" +sdate + "' ORDER BY DATE6 DESC", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

    }
}
