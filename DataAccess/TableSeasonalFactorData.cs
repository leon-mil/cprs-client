/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TableSeasonalFactorData.cs	    	
Programmer:         Christine Zhang
Creation Date:      8/7/2017
Inputs:             
Parameters:	        survey month, pm1, pm2, pm3, pm4 
Outputs:	        Seasonal Factor Table	
Description:	    This class gets the data from series table
Detailed Design:    None 
Other:	            Called by: frmTableSeasonalFactor.cs
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
    public class TableSeasonalFactorData
    {
        public DataTable GetTableSeasonalFactorData(string survey_type, string sdate, string pm1, string pm2, string pm3, string pm4)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                if (survey_type == "T")
                    stored_name = "sp_TableSeasonalFactorT";
                else if (survey_type == "V")
                    stored_name = "sp_TableSeasonalFactorV";
                else if (survey_type == "P")
                    stored_name = "sp_TableSeasonalFactorP";
                else if (survey_type == "S")
                    stored_name = "sp_TableSeasonalFactorS";
                else 
                    stored_name = "sp_TableSeasonalFactorF";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                sql_command.Parameters.Add("@premonth1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm1);
                sql_command.Parameters.Add("@premonth2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm2);
                sql_command.Parameters.Add("@premonth3", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm3);
                sql_command.Parameters.Add("@premonth4", SqlDbType.Char).Value = GeneralData.NullIfEmpty(pm4);
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
