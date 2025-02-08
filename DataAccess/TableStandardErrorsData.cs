/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TableStandardErrorsData.cs	    	
Programmer:         Christine Zhang
Creation Date:      8/31/2017
Inputs:             
Parameters:	        survey type, sdate
Outputs:	        StandardErrors Table	
Description:	    This class gets the data from series table
Detailed Design:    None 
Other:	            Called by: frmTableStandardErrors.cs
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
    public class TableStandardErrorsData
    {
        public DataTable GetTableStandardErrorsData(string survey_type, string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                if (survey_type == "T")
                    stored_name = "sp_TableStandardErrorT";
                else if (survey_type == "V")
                    stored_name = "sp_TableStandardErrorV";
                else if (survey_type == "S")
                    stored_name = "sp_TableStandardErrorS";
                else
                    stored_name = "sp_TableStandardErrorF";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetAnnTableStandardErrorsData(string survey_type, string year)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                if (survey_type == "T")
                    stored_name = "sp_AnnTableStandardErrorT";
                else if (survey_type == "V")
                    stored_name = "sp_AnnTableStandardErrorV";
                else if (survey_type == "S")
                    stored_name = "sp_AnnTableStandardErrorS";
                else
                    stored_name = "sp_AnnTableStandardErrorF";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);


                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }
    }
}
