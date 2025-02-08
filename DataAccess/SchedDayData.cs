/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.SchedDayData.cs	    	

Programmer:         Christine Zhang

Creation Date:      1/20/2016

Inputs:             

Parameters:	        None 

Outputs:	        Sched day data	

Description:	    This function establishes the data connection and reads in 
                    the data, base ib Sched Day table
 

Detailed Design:    Detailed Design for c700

Other:	            Called by: frmc700
 
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
    public class SchedDayData
    {
        public string GetApptDay(string avlflag, string apptyear, string apptmon)
        {
            string app_day = "";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT APPTDAY from dbo.Sched_day where apptyear = " + apptyear + " and APPTMNTH = " + apptmon + " and avlflag = " + avlflag;

                SqlCommand command = new SqlCommand(sql, sql_connection);

                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    app_day = reader["APPTDAY"].ToString();
                }
                    
            }

            return app_day;
        }
    }
}
