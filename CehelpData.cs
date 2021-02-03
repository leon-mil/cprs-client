/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CehelpData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/20/2015
Inputs:             help type
Parameters:	    None 
Outputs:	    None
Description:	    data layer to get cehelp
Detailed Design:    None 
Other:	            Called by: frmImprovement, frmCehelpsPopup
 
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

namespace CprsDAL
{
    public class CehelpData
    {
         /*Retrieve help data */
        public DataTable GetCehelp(int cetype)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql;

                if (cetype == 4)
                   sql = "SELECT HCODE,HJOB, HDESCRIPTION FROM dbo.cehelp WHERE htype = " + cetype + " order by hcode";
                else if (cetype == 2)
                    sql = "SELECT HCODE,HDESCRIPTION,HJOB as SCOPE FROM dbo.cehelp WHERE htype = " + cetype + " order by hcode";
                else
                    sql = "SELECT HCODE, HDESCRIPTION FROM dbo.cehelp WHERE htype = " + cetype + " order by hcode";
                SqlCommand command = new SqlCommand(sql, sql_connection);

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }
    }
}
