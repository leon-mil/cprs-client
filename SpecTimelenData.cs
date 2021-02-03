/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : CPRSDAL.SpecTimelenData.cs
 Programmer    : Christine Zhang
 Creation Date : 4/11/2019
 Inputs        : N/A
 Paramaters    : survey, type
 Output        : N/A               
 Description   : get data from sample, master, month_vip_data,LOTTABEX
    
 Detail Design : Detailed User Requirements for Timelen
                
 Other         : Called from: frmSpecTimeLen.cs, frmSpecTimeLenWorksheet
                 frmSpecTimeLenDist, frmSpecTimeLenDistCases.cs
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
using CprsBLL;
namespace CprsDAL
{
    public class SpecTimelenData
    {
        //Retrieve private data for type of Region, Division, State and division excel
        public DataTable GetTimeLenData(string survey, int type)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecTimeLen";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@survey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey);
                sql_command.Parameters.Add("@type", SqlDbType.Int).Value = type;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //Retrieve worksheet main data by survey, tc, and excluded ids
        public DataTable GetTimeLenDataWorksheet(string survey, string tc, string ids)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecTimeLenWorksheet";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@survey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey);
                sql_command.Parameters.Add("@tc", SqlDbType.Char).Value = GeneralData.NullIfEmpty(tc);
                sql_command.Parameters.Add("@ids", SqlDbType.Char).Value = ids;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //Get project list for worksheet
        public DataTable GetTimeLenWorksheetProjects(string survey, string tc, List<string> excluded)
        {
            DataTable dt = new DataTable();

            string getTc = tc.Trim();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
                string andQuery = "";
                string vgQuery = "";
                string t1 = @"1T";
                int year = DateTime.Now.Year;

                if (survey == "P")
                    andQuery = " and owner in ('S', 'L', 'P')";
                else if (survey == "N")
                    andQuery = " and owner = 'N'";
                else if (survey == "M")
                    andQuery = " and owner = 'M'";

                if (survey != "M")
                    vgQuery = " vg = case when cumvip < 250 then 1 when cumvip <= 999 then 2 when cumvip <= 2999 then 3  when cumvip >= 3000 and cumvip<= 4999 then 4 when cumvip >= 5000 and cumvip<= 9999 then 5 else 6 end";
                else
                    vgQuery = " vg = case  when cumvip < 3000 then 1 when cumvip >=3000 and cumvip<=4999 then 2 when cumvip >=5000 and cumvip<=9999 then 3 else 4 end";

                if (!String.Equals(getTc, t1, StringComparison.Ordinal))
                    andQuery = andQuery + " and substring(newtc, 1, 2) ='" + tc.Trim() + "'";
                else
                    andQuery = andQuery + " and substring(newtc, 1, 2) >= '20' and substring(newtc, 1, 2) <='39'";
                sqlQuery = @"with t2 as (select v.id,  count(date6) as months,  sum(vipdata) as cumvip from dbo.sample s, dbo.master m, dbo.MONTHLY_VIP_DATA v";
                sqlQuery = sqlQuery + " where s.masterid = m.masterid and s.id = v.id ";
                sqlQuery = sqlQuery + " and status = '1' and compdate between '" + (year -2) +"01' and '" + +(year - 1)+"12'";
                sqlQuery = sqlQuery + andQuery + " group by v.ID ) select t2.id, newtc, status, seldate, rvitm5c, strtdate, compdate,months, months* fwgt as wtmonths, fwgt, cumvip,";
                sqlQuery = sqlQuery + vgQuery + ", 0 as excluded, owner from t2, dbo.sample s, dbo.master m where t2.id = s.id and s.MASTERID = m.MASTERID order by id";
                
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            //update exclude flag
            if (excluded.Count > 0)
            {
                foreach (string id in excluded)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["id"].ToString() == id)
                        {
                            row["excluded"] = 1;
                            break;
                        }
                    }
                }
            }

            return dt;
        }

        //Get lottabex data
        public List<string> GetLottabexData(string year, string survey, string tc)
        {
            string andQuery = string.Empty;

            if (survey == "P")
                andQuery = " and owner in ('S', 'L', 'P')";
            else if (survey == "N")
                andQuery = " and owner = 'N'";
            else if (survey == "M")
                andQuery = " and owner = 'M'";

            List<string> list = new List<string>();
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT ID FROM dbo.LOTTABEX WHERE YEAR = '" + year + "' and tc = '" + tc + "'" + andQuery;
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(reader[0].ToString());
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return list;
        }

        public void DeleteLottabexData(string year, string survey, string tc)
        {
            string andQuery = string.Empty;

            if (survey == "P")
                andQuery = " and owner in ('S', 'L', 'P')";
            else if (survey == "N")
                andQuery = " and owner = 'N'";
            else if (survey == "M")
                andQuery = " and owner = 'M'";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.LOTTABEX WHERE Year = '" + year + "' AND tc = '" + tc + "'" + andQuery, sql_connection);

                    //Open the connection.
                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        //Execute the query.
                        sql_command.ExecuteNonQuery();
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

            }
        }

        //Add Lottabex data
        public void AddLottabexData(string id, string owner, string tc)
        {
            /* find out database table name */
            string db_table = "dbo.LOTTABEX";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (YEAR, ID, OWNER, TC) "
                        + "Values (@YEAR, @ID, @OWNER, @TC)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@YEAR", DateTime.Now.Year.ToString());
            insert_command.Parameters.AddWithValue("@ID", id);
            insert_command.Parameters.AddWithValue("@OWNER", owner);
            insert_command.Parameters.AddWithValue("@TC", tc);
          
            try
            {
                sql_connection.Open();
                insert_command.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                sql_connection.Close();
            }

        }

        //Retrieve Time Len CV DATA
        public DataTable GetTimeLenCVData(string survey)
        {
            DataTable dt = new DataTable();
            string sQuery = string.Empty;

            if (survey == "P")
                sQuery = "SXXXXLV0, SXXXXLV1,SXXXXLV2,SXXXXLV3,SXXXXLV4,SXXXXLV5,SXXXXLV6,SXXXXLVP,S02XXLV0,S02XXLV1,S02XXLV2,S02XXLV3,S02XXLV4,S02XXLV5,S02XXLV6,S02XXLVP,S05XXLV0,S05XXLV1,S05XXLV2,S05XXLV3,S05XXLV4,S05XXLV5,S05XXLV6,S05XXLVP,S07XXLV0,S07XXLV1,S07XXLV2,S07XXLV3,S07XXLV4,S07XXLV5,S07XXLV6,S07XXLVP,S09XXLV0,S09XXLV1,S09XXLV2,S09XXLV3,S09XXLV4,S09XXLV5,S09XXLV6,S09XXLVP,S08XXLV0,S08XXLV1,S08XXLV2,S08XXLV3,S08XXLV4,S08XXLV5,S08XXLV6,S08XXLVP,S12XXLV0,S12XXLV1,S12XXLV2,S12XXLV3,S12XXLV4,S12XXLV5,S12XXLV6,S12XXLVP,S13XXLV0,S13XXLV1,S13XXLV2,S13XXLV3,S13XXLV4,S13XXLV5,S13XXLV6,S13XXLVP,S14XXLV0,S14XXLV1,S14XXLV2,S14XXLV3,S14XXLV4,S14XXLV5,S14XXLV6,S14XXLVP";
            else if (survey == "N")
                sQuery = "VXXXXLV0,VXXXXLV1,VXXXXLV2,VXXXXLV3,VXXXXLV4,VXXXXLV5,VXXXXLV6,VXXXXLVP,V02XXLV0,V02XXLV1,V02XXLV2,V02XXLV3,V02XXLV4,V02XXLV5,V02XXLV6,V02XXLVP,V03XXLV0,V03XXLV1,V03XXLV2,V03XXLV3,V03XXLV4,V03XXLV5,V03XXLV6,V03XXLVP,V04XXLV0,V04XXLV1,V04XXLV2,V04XXLV3,V04XXLV4,V04XXLV5,V04XXLV6,V04XXLVP,V05XXLV0,V05XXLV1,V05XXLV2,V05XXLV3,V05XXLV4,V05XXLV5,V05XXLV6,V05XXLVP,V06XXLV0,V06XXLV1,V06XXLV2,V06XXLV3,V06XXLV4,V06XXLV5,V06XXLV6,V06XXLVP,V08XXLV0,V08XXLV1,V08XXLV2,V08XXLV3,V08XXLV4,V08XXLV5,V08XXLV6,V08XXLVP,V1TXXLV0,V1TXXLV1,V1TXXLV2,V1TXXLV3,V1TXXLV4,V1TXXLV5,V1TXXLV6,V1TXXLVP";
            else if (survey == "M")
                sQuery = "MXXXXLV0,MXXXXLV1,MXXXXLV2,MXXXXLV3,MXXXXLV4,MXXXXLVP,M00XXLV0,M00XXLV1,M00XXLV2,M00XXLV3,M00XXLV4,M00XXLVP";

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT " + sQuery + " from dbo.lotavcvs where year ='" + (DateTime.Now.Year-1).ToString() + "'";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                connection.Close();
            }
            return dt;
        }

        //Retrieve distribution data for Time len 
        public DataTable GetTimeLenDataDistribution(string survey)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecTimeLenDist";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@survey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey);
              
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //Get data for distribution display cases
        public DataTable GetTimeLenDataDistributionCases(string survey)
        {
            DataTable dt = new DataTable();
            DataTable dtlot = new DataTable();
            string sQuery = string.Empty;
            int year = DateTime.Now.Year;

            if (survey == "P")
            {
                sQuery = "with t2 as (select v.id,  count(date6) as months, sum(vipdata) as totvip from dbo.sample s, dbo.master m, dbo.MONTHLY_VIP_DATA v where s.masterid = m.masterid and s.id = v.id and status = '1' and compdate between '" +(year-2)+"01' and '"+(year-1)+"12' and owner in ('S', 'L', 'P') group by v.id)";
                sQuery = sQuery + "select t2.id, newtc, status, seldate, rvitm5c, strtdate, compdate, months, (months * fwgt) as wmoths, fwgt, totvip,";
                sQuery = sQuery + " vg = case when totvip < 250 then 1 when totvip <= 999 then 2 when totvip <= 2999 then 3 when totvip >= 3000 and totvip<= 4999 then 4 when totvip >= 5000 and totvip<= 9999 then 5 else 6 end, 0 as exclude";
                sQuery = sQuery + " from t2, dbo.sample s, dbo.master m where t2.id = s.id and s.MASTERID = m.MASTERID";
            }
            else if (survey == "N")
            {
                sQuery = "with t2 as (select v.id,  count(date6) as months, sum(vipdata) as totvip from dbo.sample s, dbo.master m, dbo.MONTHLY_VIP_DATA v where s.masterid = m.masterid and s.id = v.id and status = '1' and compdate between '" + (year - 2) + "01' and '" + (year - 1) + "12'  and owner = 'N' group by v.id)";
                sQuery = sQuery + "select t2.id, newtc, status, seldate, rvitm5c, strtdate, compdate, months, (months * fwgt) as wmoths, fwgt, totvip,";
                sQuery = sQuery + " vg = case when totvip < 250 then 1 when totvip <= 999 then 2 when totvip <= 2999 then 3 when totvip >= 3000 and totvip<= 4999 then 4 when totvip >= 5000 and totvip<= 9999 then 5 else 6 end, 0 as exclude";
                sQuery = sQuery + " from t2, dbo.sample s, dbo.master m where t2.id = s.id and s.MASTERID = m.MASTERID";
            }
            else if (survey == "M")
            {
                sQuery = "with t2 as (select v.id,  count(date6) as months, sum(vipdata) as totvip from dbo.sample s, dbo.master m, dbo.MONTHLY_VIP_DATA v where s.masterid = m.masterid and s.id = v.id and status = '1' and compdate between '" + (year - 2) + "01' and '" + (year - 1) + "12' and owner = 'M' group by v.id)";
                sQuery = sQuery + "select t2.id, newtc, status, seldate, rvitm5c, strtdate, compdate, months, (months * fwgt) as wmoths, fwgt, totvip,";
                sQuery = sQuery + " vg = case  when totvip < 3000 then 1 when totvip >=3000 and totvip<=4999 then 2 when totvip >=5000 and totvip<=9999 then 3 else 4 end, 0 as exclude";
                sQuery = sQuery + " from t2, dbo.sample s, dbo.master m where t2.id = s.id and s.MASTERID = m.MASTERID";
            }
                
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sQuery, connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                string sql = "select id from dbo.LOTTABEX where YEAR = '" + DateTime.Now.Year + "'";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtlot);
                }

                if (dtlot.Rows.Count >0)
                {
                    foreach (DataRow row in dtlot.Rows)
                    {
                        foreach(DataRow r in dt.Rows)
                        {
                            if (r["ID"].ToString() == row["ID"].ToString())
                                r["exclude"] = 1;
                        }
                    }
                }

                connection.Close();
            }

            return dt;

        }

        //check lotavc exists or not
        public bool CheckLotExist(string tablename)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * FROM dbo." + tablename + " WHERE Year = " + GeneralData.AddSqlQuotes((DateTime.Now.Year-1).ToString());
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return false;
        }


        //Retrieve distribution SE Data by survey
        public DataTable GetTimeLenLotPdsesData(string survey)
        {
            DataTable dt = new DataTable();
            string sQuery = string.Empty;
            string andQuery = string.Empty;

            if (survey == "P")
                andQuery = " and owner in ('S', 'L', 'P')";
            else if (survey == "N")
                andQuery = " and owner = 'N'";
            else if (survey == "M")
                andQuery = " and owner = 'M'";

            if (survey == "P")
                sQuery = "MONTH,VG0MONSE,VG0CUMSE,VG1MONSE,VG1CUMSE,VG2MONSE,VG2CUMSE,VG3MONSE,VG3CUMSE,VG4MONSE,VG4CUMSE,VG5MONSE,VG5CUMSE,VG6MONSE,VG6CUMSE";
            else if (survey == "N")
                sQuery = "MONTH,VG0MONSE,VG0CUMSE,VG1MONSE,VG1CUMSE,VG2MONSE,VG2CUMSE,VG3MONSE,VG3CUMSE,VG4MONSE,VG4CUMSE,VG5MONSE,VG5CUMSE,VG6MONSE,VG6CUMSE";
            else if (survey == "M")
                sQuery = "MONTH,VG0MONSE,VG0CUMSE,VG1MONSE,VG1CUMSE,VG2MONSE,VG2CUMSE,VG3MONSE,VG3CUMSE,VG4MONSE,VG4CUMSE";

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT " + sQuery + " from dbo.LOTPDSES where year ='" + (DateTime.Now.Year - 1).ToString() + "'" + andQuery + " order by year, month";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                connection.Close();
            }
            return dt;
        }


        //Retrieve distribution Highway sandard error data
        public DataTable GetTimeLenLOTPDHWYSESData()
        {
            DataTable dt = new DataTable();
            string sQuery = string.Empty;

            sQuery = "OWNER,QTR,VG0QTRSE,VG0CUMSE,VG1QTRSE,VG1CUMSE,VG2QTRSE,VG2CUMSE,VG3QTRSE,VG3CUMSE,VG4QTRSE,VG4CUMSE,VG5QTRSE,VG5CUMSE,VG6QTRSE,VG6CUMSE";
           
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT " + sQuery + " from dbo.LOTPDHWY_SES where year ='" + (DateTime.Now.Year - 1).ToString() + "' order by year, qtr";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                connection.Close();
            }
            return dt;
        }

        //Retrieve distribution Highway data
        public DataTable GetTimeLenLOTPDHWYESTData()
        {
            DataTable dt = new DataTable();
           
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT QTR, VG0QTREST,VG0CUMEST,VG1QTREST,VG1CUMEST,VG2QTREST,VG2CUMEST,VG3QTREST,VG3CUMEST,VG4QTREST, VG4CUMEST, VG5QTREST, VG5CUMEST, VG6QTREST,VG6CUMEST from dbo.LOTPDHWY_EST where year ='" + (DateTime.Now.Year - 1).ToString() + "' order by year, qtr";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                connection.Close();
            }
            return dt;
        }

    }
}
