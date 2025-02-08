/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.SpecManufacturingAnn.cs	    	
Programmer:         Diane Musachio
Creation Date:      9/6/2018
Inputs:             
Parameters:	        Col
Outputs:	        spec manufacturing annual data	
Description:	    This class gets the data from vipsttab

Detailed Design:    None 
Other:	            Called by: frmSpecManufacturingAnn.cs
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
    public class SpecManufacturingAnnData
    {
      
        public DataTable GetSpecManufacturingAnnData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select  *
                     from dbo.MANUFACTURE order by mgroup";
               
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }
        public DataTable GetLSFAnnData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select  *
                     from dbo.LSFANN  where NEWTC = '1T' AND OWNER = 'N' order by LSFNO";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }

        public DataTable GetBstAnnData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select  *
                     from dbo.BSTANN  where NEWTC = '1T' AND OWNER = 'N' order by SDATE";

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

