/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.SpecProjectValueData.cs 
 	
Programmer:         Diane Musachio

Creation Date:      11/26/2018

Inputs:             dbo.PROJ_VALUE (view)

Parameters:	        prioryear, pprioryear

Outputs:	        spec project value data	

Description:	    This class gets the data from Proj_value view
                    which gets info from master and sample

Detailed Design:    None 
Other:	            Called by: frmSpecProjectVal.cs
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
    public class SpecProjectValueData
    {
        public DataTable GetProjValueData(string prioryear, string pprioryear)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select TC2X, COMPYEAR, PIDIFF, PRDIFF, IRDIFF, SURVEY 
                     from dbo.PROJ_VALUE where compyear = " + prioryear.AddSqlQuotes() +
                     " or compyear = " + pprioryear.AddSqlQuotes() +
                     " order by TC2X";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }

        /*Retrieve description for newtc*/
        public DataTable GetTCDescription()
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = "SELECT TCDESCRIPTION, NEWTC FROM dbo.PUBTCLIST";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }
    }
}

