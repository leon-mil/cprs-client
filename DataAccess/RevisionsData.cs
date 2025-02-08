/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : CPRSDAL.RevisionsData.cs

 Programmer    : Diane Musachio

 Creation Date : 4/12/2017

 Inputs        : N/A

 Paramaters    : survey, newtc, revnum

 Output        : N/A
                   
 Description   : These classes get data from the views: dbo.REV1, dbo.REV2, dbo.REV3, dbo.REV4
 
 Detail Design : Detailed User Requirements for Revision Analysis

 Other         : Called from: frmRevision.cs

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
    public class RevisionsData
    {
        /* This class uses an SQL connection to access the view dbo.REV1 */
        
        public DataTable GetRevisionData(string survey, string newtc2, string revnum)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                string sqlQuery = @"select ID, NEWTC, STATUS, OWNER, SELDATE, STRTDATE, COMPDATE,
                     CHANGE, CURRWVIP, CURRFLAG, PREVWVIP, PREVFLAG, CURRFWGT, PREVFWGT, 
                     COALESCE(CURRWVIP/ nullif(CURRFWGT,0), 0) as CURR,
                     COALESCE(PREVWVIP/ nullif(PREVFWGT,0), 0) as PREV,
                     CURRTC2, PREVTC2, CURRTC2X, PREVTC2X, CURRSURV, PREVSURV, CURRSTATUS, PREVSTATUS
                     from dbo." + revnum + @" 
                     where (CURRSURV = @SURVEY or PREVSURV = @SURVEY) and 
                           (CURRTC2 = @NEWTC2 or PREVTC2 = @NEWTC2 or CURRTC2X = @NEWTC2 or PREVTC2X = @NEWTC2)
                     order by CHANGE DESC ";
                
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SURVEY", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(survey);
                    cmd.Parameters.AddWithValue("@NEWTC2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtc2);
                    cmd.Parameters.AddWithValue("@REVNUM", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(revnum);

                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }      
    }
}
