/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.ImpTabReviewData.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/28/2015

Inputs:             None

Parameters:	        None 

Outputs:	        Winsorized and Unwinsorized data	

Description:	    This function establishes the data connection and reads in 
                    the Winsorized and Unwinsorized data for output to
 *                  the screen

Detailed Design:    None 

Other:	            Called by:
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
********************************************************************* 
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
    public class ImpTabReviewData
    {

        /*Get Winsorized Data */
        
        public DataTable GetWinsorizedTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select * From dbo.C30TAB ORDER BY DATE6 DESC", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        /*Get UnWinsorized Data */
       
        public DataTable GetUnWinsorizedTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select * From dbo.C30TABUW ORDER BY DATE6 DESC", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

    }
}
