/**************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : CprsDAL.Vip1UnitData.cs	    	
Programmer      : Christine Zhang     
Creation Date   : July 11 2024 
Inputs          : None
Parameters      : none
Outputs         : vip 1 unit data	
Description     : data layer to get data for vip 1 unit screens
Detailed Design : None 
Other           : Called by: frmVip1Unit
Revision History:	
***************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
    public class Vip1UnitData
    {
        /*Retrieve vip 1unit data */
        public DataTable GetVip1UnitData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                string sqlQuery = @"select date6, avgcost, c1price, c2price, totstart, othstart, ownstart from dbo.Vip1Unit order by date6 desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

            }

            return dt;
        }
    }
}

