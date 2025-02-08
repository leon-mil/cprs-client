/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TotalVipData.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/15/2016
Inputs:             None
Parameters:	        survey date, owner, level and newtc
Outputs:	        totalvip data	
Description:	    data layer to get data for totalvip
Detailed Design:    None 
Other:	            Called by: frmTotalVip
 
Revision History:	
***************************************************************************************
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
    public class TotalVipData
    {
        /*Retrieve total vip data for an owner and level and newtc*/
        public DataTable GetVipTotalData(string sdate, string owner, string level, string newtc="")
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT title = case when owner = 'T' and pubt = 'Y' then p.NEWTC+ ' ' +TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'T' and pubt = 'N' then '*' + p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'N' and pubn = 'Y' then p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'N' and pubn = 'N' then '*' + p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'P' and pubp = 'Y' then p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'P' and pubp = 'N' then '*' + p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'M' and pubm = 'Y' then p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'M' and pubm = 'N' then '*' + p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'F' and pubf = 'Y' then p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'F' and pubf = 'N' then '*' + p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'U' and pubu = 'Y' then p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " when owner = 'U' and pubu = 'N' then '*' + p.newtc + ' ' + TCDESCRIPTION ";
                       sqlQuery = sqlQuery + " end, Cm0, cm1, cm2,  pm0, pm1, ";
                       sqlQuery = sqlQuery + " p1=CASE WHEN cm1 = 0 THEN 0 ELSE cast((cm0-cm1)*100/cm1  as decimal(10,2)) end,";
                       sqlQuery = sqlQuery + " p2 =Case when cm2=0 then 0 else cast((cm1-cm2)*100/cm2  as decimal(10,2))  end,";
                       sqlQuery = sqlQuery + " r1 = case when pm0=0 then 0 else cast((cm1-pm0)*100/pm0  as decimal(10,2)) end,";
                       sqlQuery = sqlQuery + " r2= case when pm1=0 then 0 else cast((cm2-pm1)*100/pm1  as decimal(10,2)) end, cm3, cm4, pm2, pm3,";
                       sqlQuery = sqlQuery + " p3=CASE WHEN cm3 = 0 THEN 0 ELSE cast((cm2-cm3)*100/cm3  as decimal(10,2)) end,";
                       sqlQuery = sqlQuery + " p4 =Case when cm4=0 then 0 else cast((cm3-cm4)*100/cm4  as decimal(10,2))  end,";
                       sqlQuery = sqlQuery + " r3= case when pm2=0 then 0 else cast((cm3-pm2)*100/pm2  as decimal(10,2)) end,";
                       sqlQuery = sqlQuery + " r4= case when pm3=0 then 0 else cast((cm4-pm3)*100/pm3  as decimal(10,2)) end, v.newtc, U_CM0, U_CM1, U_CM2, U_PM0, U_PM1 ";
                       sqlQuery = sqlQuery + " from dbo.vipsadj v, dbo.PUBTCLIST p ";
                       sqlQuery = sqlQuery + " where v.NEWTC =p.newtc and sdate = " + GeneralData.AddSqlQuotes(sdate);
                       if (level == "1")
                           sqlQuery = sqlQuery + " and v.ddown = " + GeneralData.AddSqlQuotes(level);
                       else
                           sqlQuery = sqlQuery + " and (p.newtc = '" + newtc + "' or p.newtc like replace('" + newtc + "%', ' ', ''))";
                       sqlQuery = sqlQuery + " and owner = " + GeneralData.AddSqlQuotes(owner);
                       sqlQuery = sqlQuery + " order by p.newtc";

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
