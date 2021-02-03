/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.NewtcSelData.cs	    	
Programmer:         Christine Zhang
Creation Date:      01/19/2016
Inputs:             
Parameters:	        None 
Outputs:	        None
Description:	    data layer to search newtc
Detailed Design:    None 
Other:	            Called by: frmNewtcSel.cs
 
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
using System.Text.RegularExpressions;

namespace CprsDAL
{
    public class NewtcSelData
    {
        /*Get Master Search Data */
        public DataTable GetNewtcSelSearchData(string newtc_desc)                                                      
        {
            DataTable dt = new DataTable();

            bool check_desc = true;

            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            if (regex.IsMatch(newtc_desc)) check_desc = false;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqls = "";
                if (check_desc)
                    sqls = " select NEWTC, TCDESCRIPTION from NEWTCLIST WHERE TCDESCRIPTION like '%' + @DESC + '%' order by NEWTC";
                else
                    sqls = " select NEWTC, TCDESCRIPTION from NEWTCLIST WHERE newtc like @DESC + '%' order by NEWTC";

                SqlCommand sql_command = new SqlCommand(sqls, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                sql_command.Parameters.AddWithValue("@Desc", SqlDbType.NVarChar).Value = newtc_desc;
                da.Fill(dt);
            }

            return dt;

        }

        /*Retrieve newtc data */
        public DataTable GetNewtcData(string owner = "*")
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqls = "";
                if (owner == "*" ||( owner == "T" || owner == "E" || owner == "G" || owner == "R" || owner == "O" || owner == "W"))
                    sqls = "Select NEWTC, TCDESCRIPTION from dbo.Newtclist order by NEWTC";
                else if (owner != "M" && owner != "T" && owner != "E" && owner != "G" && owner != "R" && owner != "O" && owner != "W")
                    sqls = "Select NEWTC, TCDESCRIPTION from dbo.Newtclist  where substring(newtc, 1, 2) not in ('16', '19')  order by NEWTC";
                else if (owner == "M")
                    sqls = "Select NEWTC, TCDESCRIPTION from dbo.Newtclist where NEWTC in ('0021', '0029') order by NEWTC";
               
                SqlCommand sql_command = new SqlCommand(sqls, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }
    }
}
