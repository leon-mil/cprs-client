/**************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : CprsDAL.FlagReportData.cs	    	
Programmer      : Christine Zhang/Diane Musachio   
Creation Date   : May 24 2017  
Inputs          : None
Parameters      : ftype, newtcs ,srowint, flagname, mySectors
Outputs         : FlagReport data	
Description     : data layer to get data for flag Report screens
Detailed Design : None 
Other           : Called by: frmFlagReport
Revision History:	
***************************************************************************************
 Modified Date :  March 28, 2019
 Modified By   :  Diane Musachio
 Keyword       :  dm032819
 Change Request:  #3027
 Description   :  add status value from sample
****************************************************************************************
Modified Date   : April 18, 2019
Modified By     : Kevin Montgomery
Keyword         :  
Change Request  : CR 2733 
Description     : Added Flag 46 Back
****************************************************************************************
Modified Date   : September 30, 2019
Modified By     : Christine Zhang
Keyword         :  
Change Request  : CR 3591
Description     : Added Flag 47 and Flag 48
****************************************************************************************
Modified Date   : September 30, 2019
Modified By     : Christine Zhang
Keyword         :  
Change Request  : CR 3853
Description     : Remove RUNItS and Rbldgs columns and add %comp column
****************************************************************************************
Modified Date   : April 21, 2020
Modified By     : Kevin Montgomery
Keyword         : 20200421kjm
Change Request  : CR xxxxx
Description     : Remove Flag 45
****************************************************************************************
 Modified Date :  10/6/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#7650
 Description   :update description for flag5, flag6, remove "or Blank"
***************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using CprsBLL;
using System.IO;

namespace CprsDAL
{
    public class FlagReportData
    {
        /*Retrieve FlagReport data */
public DataTable GetFlagReportData(int ftype, string newtcs)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_FlagReport", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@flagtype", SqlDbType.Char).Value = ftype;
                sql_command.Parameters.Add("@Sectors", SqlDbType.Char).Value = newtcs;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //use for loop to fill title
                foreach (DataRow row in dt.Rows)
                {
                    int fno = Convert.ToInt32(row["flagno"]);
                    if (fno == 1)
                    {
                        //HQ flag only
                        if (ftype == 0)
                        {
                            row["title"] = "(1) FLAG - STATUS CODE CHANGE";
                            row["sqno"] = 17;
                        }
                        //if show report flags, delete it
                        else
                            row.Delete();
                    }
                    else if (fno == 2)
                    {
                        //HQ flag only
                        if (ftype == 0)
                        {
                            row["title"] = "(2) FLAG - OWNERSHIP CODE CHANGE";
                            row["sqno"] = 18;
                        }
                        else
                            row.Delete();
                    }
                    else if (fno == 3)
                    {
                        row["title"] = "(3) REJECT - ITEM5A + ITEM5B NOT EQUAL RVITM5C";
                        row["sqno"] = 1;
                    }
                    else if (fno == 4)
                    {
                        row["title"] = "(4) REJECT - REPORT FOLLOWS IMPUTE";
                        row["sqno"] = 2;
                    }
                    else if (fno == 5)
                    {
                        row["title"] = "(5) REJECT - VIP of 0 IN START MONTH";
                        row["sqno"] = 3;
                    }
                    else if (fno == 6)
                    {
                        row["title"] = "(6) REJECT - VIP of 0 IN COMPLETION MONTH";
                        row["sqno"] = 4;
                    }
                    else if (fno == 7)
                    {
                        row["title"] = "(7) REJECT - UNMATCHED PROJECT DATES";
                        row["sqno"] = 5;
                    }
                    else if (fno == 8)
                    {
                        row["title"] = "(8) REJECT - RVITM5C REPORTED 0";
                        row["sqno"] = 6;
                    }
                    else if (fno == 9)
                    {
                        row["title"] = "(9) REJECT - STARTED MORE THAN 2 YEARS AGO";
                        row["sqno"] = 7;
                    }
                    else if (fno == 10)
                    {
                        //HQ flag only
                        if (ftype == 0)
                        {
                            row["title"] = "(10) REJECT - NEGATIVE VIP";
                            row["sqno"] = 8;
                        }
                        //if show report flags, delete it
                        else
                            row.Delete();
                    }
                    else if (fno == 11)
                    {
                        row["title"] = "(11) REJECT - COMPLETION DATE WITH NO RVITM5C";
                        row["sqno"] = 9;
                    }
                    else if (fno == 12)
                    {
                        row["title"] = "(12) REJECT - ITEM5B WITHOUT ITEM5A";
                        row["sqno"] = 10;
                    }
                    else if (fno == 13)
                    {
                        //HQ flag only
                        if (ftype == 0)
                        {
                            row["title"] = "(13) FLAG - START BEFORE SELDATE";
                            row["sqno"] = 19;
                        }
                        //if show report flags, delete it
                        else
                            row.Delete();

                    }
                    else if (fno == 14)
                    {
                        row["title"] = "(14) FLAG - RVITM5C >= 3 TIMES SELVAL";
                        row["sqno"] = 12;
                    }
                    else if (fno == 15)
                    {
                        row["title"] = "(15) FLAG - SELVAL >= 3 TIMES RVITM5C";
                        row["sqno"] = 20;
                    }
                    else if (fno == 16)
                    {
                        row["title"] = "(16) FLAG - CUM VIP > 140% OF RVITM5C";
                        row["sqno"] = 21;
                    }
                    else if (fno == 17)
                    {
                        row["title"] = "(17) FLAG - VIP 25% OF RVITM5C (RVITM5C >= $10 MIL)";
                        row["sqno"] = 22;
                    }
                    else if (fno == 18)
                    {
                        row["title"] = "(18) FLAG - VIP 50% OF RVITM5C ($10 MIL > RVITM5C >= $1 MIL)";
                        row["sqno"] = 23;
                    }
                    else if (fno == 19)
                    {
                        row["title"] = "(19) FLAG - VIP 75% OF RVITM5C ($1 MIL > RVITM5C >= $75K)";
                        row["sqno"] = 24;
                    }
                    else if (fno == 20)
                    {
                        //HQ flag only
                        if (ftype == 0)
                        {
                            row["title"] = "(20) FLAG - OUTLIER PROJECT";
                            row["sqno"] = 13;
                        }
                        //if show report flags, delete it
                        else
                            row.Delete();
                    }
                    else if (fno == 21)
                    {
                        row["title"] = "(21) FLAG - ITEM6 50% OR MORE RVITM5C";
                        row["sqno"] = 14;
                    }
                    else if (fno == 22)
                    {
                        row["title"] = "(22) FLAG - ITEM7 BLANK, NEWTC 20-39";
                        row["sqno"] = 28;
                    }
                    else if (fno == 23)
                    {
                        row["title"] = "(23) FLAG - RVITM5C + ITEM6 = ITEM7";
                        row["sqno"] = 29;
                    }
                    else if (fno == 24)
                    {
                        row["title"] = "(24) FLAG - ITEM7 > RVITM5C";
                        row["sqno"] = 30;
                    }
                    else if (fno == 25)
                    {
                        if (ftype == 0)
                        {
                            row["title"] = "(25) FLAG - VIP IMPUTE OVER RVITM5C";
                            row["sqno"] = 31;
                        }
                        else
                            row.Delete();
                    }
                    else if (fno == 26)
                    {
                        row["title"] = "(26) FLAG - VIP OF 0 IN LAST 3 MONTHS";
                        row["sqno"] = 32;
                    }
                    else if (fno == 27)
                    {
                        row["title"] = "(27) FLAG - COMPLETED BEFORE SELECTION DATE";
                        row["sqno"] = 33;
                    }
                    else if (fno == 28)
                    {
                        row["title"] = "(28) FLAG - CUM VIP < 95% OF RVITM5C";
                        row["sqno"] = 34;
                    }
                    else if (fno == 29)
                    {
                        row["title"] = "(29) FLAG - MISSING KEY IMPUTATION VARIABLE";
                        row["sqno"] = 35;
                    }
                    else if (fno == 30)
                    {
                        row["title"] = "(30) FLAG - 900% of PREVIOUS RVITM5C";
                        row["sqno"] = 36;
                    }
                    else if (fno == 31)
                    {
                        row["title"] = "(31) FLAG - 900% of PREVIOUS ITEM6";
                        row["sqno"] = 37;
                    }
                    else if (fno == 32)
                    {
                        row["title"] = "(32) FLAG - 900% of PREVIOUS CAPEXP";
                        row["sqno"] = 38;
                    }
                    else if (fno == 33)
                    {
                        row["title"] = "(33) FLAG - 900% of PREVIOUS VIP";
                        row["sqno"] = 39;
                    }
                    else if (fno == 34)
                    {
                        row["title"] = "(34) FLAG - CUM VIP BUT NO RVITM5C";
                        row["sqno"] = 40;
                    }
                    else if (fno == 35)
                    {
                        row["title"] = "(35) FLAG - TARGET SPEED OF PROJECT";
                        row["sqno"] = 41;
                    }
                    else if (fno == 36)
                    {
                        //5/30/17 NO LONGER USED
                        row.Delete();
                    }
                    else if (fno == 37)
                    {
                        row["title"] = "(37) FLAG - WITHIN SURVEY OWNERSHIP CHANGE";
                        row["sqno"] = 43;
                    }
                    else if (fno == 38)
                    {
                        //HQ flag only
                        if (ftype == 0)
                        {
                            row["title"] = "(38) REJECT - REPORTED VIP WITH NO START DATE";
                            row["sqno"] = 11;
                        }
                        else
                            row.Delete();
                    }
                    else if (fno == 39)
                    {
                        row["title"] = "(39) FLAG - CENTURION COMMENTS";
                        row["sqno"] = 44;
                    }
                    else if (fno == 40)
                    {
                        row["title"] = "(40) FLAG - COST PER UNIT OUT OF RANGE";
                        row["sqno"] = 15;
                    }
                    else if (fno == 41)
                    {
                        row["title"] = "(41) FLAG - CUM VIP > RVITM5C";
                        row["sqno"] = 19;
                    }
                    else if (fno == 42)
                    {
                        row["title"] = "(42) FLAG - VIP 20% of RVITM5C (RUNITS >= 200)";
                        row["sqno"] = 21;
                    }
                    else if (fno == 43)
                    {
                        row["title"] = "(43) FLAG - VIP 50% OF RVITM5C (200 > RUNITS >= 100)";
                        row["sqno"] = 23;
                    }
                    else if (fno == 44)
                    {
                        row["title"] = "(44) FLAG - VIP 75% OF RVITM5C (100 > RUNITS > 1)";
                        row["sqno"] = 25;
                    }
                    else if (fno == 45)
                    {
                        //04/21/2020 No Longer Used
                        row.Delete();
                    }
                    else if (fno == 46)
                    {
                        row["title"] = "(46) FLAG - REPORTED VIP BLANKED OUT IN CENTURION";
                        row["sqno"] = 46;
                    }
                    else if (fno == 47)
                    {
                        row["title"] = "(47) FLAG - ANALYST DATA UPDATED BY REPORTED";
                        row["sqno"] = 47;
                    }
                    else if (fno == 48)
                    {
                        row["title"] = "(48) FLAG - VIP = 999% OF RVITM5C";
                        row["sqno"] = 16;
                    }
                }

                //sort by sqno 
                dt.DefaultView.Sort = "sqno ASC";
                dt = dt.DefaultView.ToTable();
            }

            return dt;
        }

        //populate fm.ReviewScheduler
        public DataTable GetIdList(int srowint, string flagname, string mySectors)
        {
            DataTable dt = new DataTable();
            string sample_table = "dbo.sample";

            List<int> reject_list = new List<int>();
            int[] array = new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 38 };
            reject_list.AddRange(array);

            // Finds first element greater than 20
            int result = reject_list.Find(item => item ==srowint);
            if (result > 0)
                sample_table = "dbo.sample_hold";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    string sql;

                    /*dm032819*/
                    if (mySectors == "")
                    {
                        sql = @"SELECT 
                        s.id, s.respid, s.rvitm5c, s.fwgt,
                        m.newtc, s.status, m.owner, m.projselv, m.seldate, m.fipstate, 0 as comp             
                      FROM " + sample_table + @" s, dbo.DATA_FLAGS df, 
                        dbo.master m left join dbo.soc soc on m.masterid = soc.masterid
                      WHERE SUBSTRING( " + flagname + ", " + srowint + @", 1) = 1
                        and m.masterid = s.masterid                       
                        and s.id = df.id
                      ORDER by id asc";
                    }
                    else
                    {
                        sql = @"with t2 as (SELECT s.id, s.respid, s.rvitm5c, s.fwgt, t22 =case when substring(m.newtc, 1, 2)< '20' then substring(m.newtc, 1, 2) else '1T' end,";
                        sql = sql + " m.newtc, s.status, m.owner, m.projselv, m.seldate, m.fipstate FROM " + sample_table + @" s, dbo.DATA_FLAGS df, dbo.master m left join dbo.soc soc on m.masterid = soc.masterid";
                        sql = sql + " WHERE SUBSTRING( " + flagname + ", " + srowint + @", 1) = 1 and m.masterid = s.masterid and s.id = df.id) select id, respid, rvitm5c, fwgt, newtc, status, owner, projselv, seldate, fipstate, 0 as comp from t2 where t22 in ('" + mySectors + @"') ORDER by id asc";
                    }

                    sql_connection.Open();
                    SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                    // Create a DataAdapter to run the command and fill the DataTable

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        da.Fill(dt);
                    }

                    MonthlyVipsData mvip_data = new MonthlyVipsData();

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow dr = dt.Rows[i];
                        int cumvip = 0;
                        int rvitm5c = Convert.ToInt32(dr["rvitm5c"]);

                        // Get cumlated vip
                        if (sample_table == "dbo.sample")
                            cumvip = mvip_data.GetCumVipsFormDB(dr["id"].ToString(), TypeDBSource.Default);
                        else
                            cumvip = mvip_data.GetCumVipsFormDB(dr["id"].ToString(), TypeDBSource.Hold);
                        if (rvitm5c >0)
                            dr["comp"] = Math.Round((double)cumvip *100/rvitm5c, 0);

                    }

                }

                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close(); //close database connection
                }

                return dt;
            }
        }
    }
}
