/**************************************************************************************
Econ App Name  : CPRS
Project Name   : CPRS Interactive Screens System
Program Name   : CprsDAL.TabMonToMonData.cs	    	
Programmer     : Christine Zhang
Creation Date  : 01/3/2017
Inputs         : dbo.vipproj
Parameters     : newtc, suvey and show preChange
Outputs        : None
Description    : data layer to get month to month data
Detailed Design: None 
Other          : Called by: frmTabMonToMon
 
Revision Hist  :	
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
    public class TabMonToMonData
    {
        /*Retrieve month to month data for an owner and newtc */
        public DataTable GetMonthToMonthData(string survey, string newtc, bool show_preChange)
        {
            DataTable dt = new DataTable();
            string getTc = newtc.Trim();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
                string andQuery = "";

                if (survey == "P")
                    andQuery = " and owner in ('S', 'L', 'P')";
                else if (survey == "F")
                    andQuery = " and owner in ('C', 'D', 'F')";
                else if (survey == "N")
                    andQuery = " and owner = 'N'";
                else if (survey == "M")
                    andQuery = " and owner = 'M'";
                else
                    andQuery = " and owner in ('T', 'G', 'E', 'O', 'W', 'R')";

                string t1 = @"1T";
                if (show_preChange)
                {
                    if (String.Equals(getTc, t1, StringComparison.Ordinal))
                    {
                        sqlQuery = @"SELECT id,  newtc, status, seldate, strtdate, compdate, v0-v1 as diff, v0, fv0, v1, fv1, fwgt, round(v0/fwgt,0) as uwv0, round(v1/fwgt, 0) as uwv1 ";
                        sqlQuery = sqlQuery + " from dbo.vipproj where v0-v1 <> 0 and tabtc >= '20' and tabtc <='39'"  + andQuery;
                    }
                    else
                    {
                        sqlQuery = @"SELECT id,  newtc, status, seldate, strtdate, compdate, v0-v1 as diff, v0, fv0, v1, fv1, fwgt, round(v0/fwgt, 0) as uwv0, round(v1/fwgt, 0) as uwv1 ";
                        sqlQuery = sqlQuery + " from dbo.vipproj where v0-v1 <> 0 and newtc like '" + getTc + "%'" + andQuery;
                    }
                }
                else
                {
                    if (String.Equals(getTc, t1, StringComparison.Ordinal))
                    {
                        sqlQuery = @"SELECT id,  newtc, status, seldate, strtdate, compdate, v1-v2 as diff, v1, fv1, v2, fv2, fwgt, round(v1/fwgt, 0) as uwv1, round(v2/fwgt, 0) as uwv2 ";
                        sqlQuery = sqlQuery + " from dbo.vipproj where v1-v2 <> 0 and tabtc >= '20' and tabtc <='39'" + andQuery;
                    }
                    else
                    {
                        sqlQuery = @"SELECT id,  newtc, status, seldate, strtdate, compdate, v1-v2 as diff, v1, fv1, v2, fv2, fwgt, round(v1/fwgt, 0) as uwv1, round(v2/fwgt, 0) as uwv2 ";
                        sqlQuery = sqlQuery + " from dbo.vipproj where v1-v2 <> 0 and newtc like '" + getTc + "%'" + andQuery;
                    }
                }

                sqlQuery = sqlQuery + " order by diff desc ";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

            }

            return dt;
        }
    }
}
