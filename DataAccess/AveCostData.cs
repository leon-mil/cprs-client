/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : CPRSDAL.AveCostData.cs

 Programmer    : Diane Musachio

 Creation Date : 3/9/2017

 Inputs        : N/A

 Paramaters    : prioryr, region, division, fipstate, sdateSel

 Output        : N/A
                   
 Description   : These classes get data from the view: dbo.AVGCOST
 
 Detail Design : Detailed User Requirements for Average Cost Multi Family

 Other         : Called from: frmAveCost.cs

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace CprsDAL
{
    public class AveCostData
    {

        /* This class uses an SQL connection to access the view dbo.avgcost */

        public DataTable GetAveCost(string prioryr, string region, string division, string fipstate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select STRTDATE, TUNITS, TVALUE, (TVALUE/TUNITS)*1000 as CPU 
                     from dbo.AVGCOST where (region = @region and division = @division and fipstate = @fipstate and styear 
                     = @prioryr) order by strtdate";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@PRIORYR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(prioryr);
                    cmd.Parameters.AddWithValue("@REGION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(region);
                    cmd.Parameters.AddWithValue("@DIVISION", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(division);
                    cmd.Parameters.AddWithValue("@FIPSTATE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(fipstate);
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        /* This class uses an SQL connection to access the view dbo.DCPReview */

        public DataTable GetAveCostState(string prioryr, string fipstate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select STRTDATE, TUNITS, TVALUE, (TVALUE/TUNITS)*1000 as CPU 
                     from dbo.AVGCOST where (fipstate = @fipstate and styear 
                     = @prioryr) order by strtdate";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@PRIORYR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(prioryr);
                    cmd.Parameters.AddWithValue("@FIPSTATE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(fipstate);
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

           /* This class uses an SQL connection to access dbo.vipproj */

        public DataTable GetVipprojData(string sdateSel)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select v.ID, v.NEWTC, v.V0, v.FV0, v.V1, v.FV1, v.V2, v.FV2, (v.CUMVIP/(v.RVITM5C * v.FWGT)) *100 AS PCT, s.pcityst AS PCITYST, v.SELDATE,
                     v.STRTDATE, v.UNITS, v.RVITM5C, v.ITEM6, v.FSWGT, v.FWGT, ((v.RVITM5C*v.FWGT)/(v.UNITS*v.FSWGT))*1000 AS CPU, v.region, v.division, v.fipstate
                     from dbo.VIPPROJ v, dbo.SAMPLE s where ((v.STRTDATE = @sdateSel and v.owner = 'M') and (v.ID = s.ID))";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SDATESEL", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(sdateSel);
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }
    }
}
