/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TabAnnualBeaData.cs	    	
Programmer:         Christine Zhang
Creation Date:      3/20/2016
Inputs:             None
Parameters:	        survey date, survey, boost and newtc
Outputs:	        
Description:	    data layer to get data for Annproj
Detailed Design:    None 
Other:	            Called by: frmTabAnnualBea.cs, frmTabAnnualBeaWorksheet.cs
                    frmTabAnnualBeaRev.cs
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
    public class TabAnnualBeaData
    {
        //get data for Seasonally Adjusted Annual Rate
        public DataTable GetTabAnnualBeaData(string year, string survey_type, int boost)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_AnnualBea";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@survey", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survey_type);
                sql_command.Parameters.Add("@boost", SqlDbType.Int).Value = boost;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

            }
            return dt;
        }

        //check the year is current year or not
        private Boolean IsCurrentYear(string year)
        {
            string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();
            int cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            int cur_mon = Convert.ToInt16(sdate.Substring(4, 2));
            bool isCurryear = false;

            if (cur_mon > 2)
            {
                if (year == (cur_year - 1).ToString())
                    isCurryear = true;
            }
            else
            {
                if (year == (cur_year - 2).ToString())
                    isCurryear = true;
            }

            return isCurryear;
        }

        //Get project list for worksheet
        public DataTable GetTabAnnualBeaDataWorksheetProjects(string year, string survey, string newtc)
        {
            DataTable dt = new DataTable();

            bool isCurryear = IsCurrentYear(year);

            string getTc = newtc.Trim();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
                string andQuery = "";
                string colQuery = "";
                string flagQuery = "";

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
                andQuery = andQuery + " and status in ('1', '2', '3', '7', '8')";

                if (isCurryear)
                {
                    colQuery = "VCY01 as c1, FCY01 as f1, VCY02 as c2, FCY02 as f2, VCY03 as c3, FCY03 as f3, VCY04 as c4, FCY04 as f4, VCY05 as c5, FCY05 as f5, VCY06 as c6, FCY06 as f6, VCY07 as c7, FCY07 as f7, VCY08 as c8, FCY08 as f8, VCY09 as c9, FCY09 as f9, VCY10 as c10, FCY10 as f10, VCY11 as c11, FCY11 as f11, VCY12 as c12, FCY12 as f12 ";
                    flagQuery = " and (FCY01 <> '' or FCY02 <> '' or FCY03 <> '' or FCY04 <> '' or FCY05 <> '' or FCY06 <> '' or FCY07 <> '' or FCY08 <> '' or FCY09 <> '' or FCY10 <> '' or FCY11 <> '' or FCY12 <> '')";
                }
                else
                {
                    colQuery = "VPY01 as c1, FPY01 as f1, VPY02 as c2, FPY02 as f2, VPY03 as c3, FPY03 as f3, VPY04 as c4, FPY04 as f4, VPY05 as c5, FPY05 as f5, VPY06 as c6, FPY06 as f6, VPY07 as c7, FPY07 as f7, VPY08 as c8, FPY08 as f8, VPY09 as c9, FPY09 as f9, VPY10 as c10, FPY10 as f10, VPY11 as c11, FPY11 as f11, VPY12 as c12, FPY12 as f12 ";
                    flagQuery = " and (FPY01 <> '' or FPY02 <> '' or FPY03 <> '' or FPY04 <> '' or FPY05 <> '' or FPY06 <> '' or FPY07 <> '' or FPY08 <> '' or FPY09 <> '' or FPY10 <> '' or FPY11 <> '' or FPY12 <> '')";
                }
                string t1 = @"1T";

                if (String.Equals(getTc, t1, StringComparison.Ordinal))
                {
                    sqlQuery = @"SELECT id,  newtc," + colQuery + ", strtdate, compdate, rvitm5c, item6, fwgt, status, owner, cumvip, 0 as modified";
                    sqlQuery = sqlQuery + " from dbo.annproj where substring(newtc, 1, 2) >= '20' and substring(newtc, 1, 2) <='39'" + andQuery + flagQuery;
                }
                else
                {
                    sqlQuery = @"SELECT id,  newtc," + colQuery + ", strtdate, compdate, rvitm5c, item6, fwgt, status, owner, cumvip, 0 as modified";
                    sqlQuery = sqlQuery + " from dbo.annproj where newtc like '" + getTc + "%'" + andQuery + flagQuery;
                }

                sqlQuery = sqlQuery + " order by id";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //Get related lsf data
        public DataTable GetRelatedLSF(string year, string survey, string newtc)
        {
            DataTable dt = new DataTable();

            bool isCurryear = IsCurrentYear(year);

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
                if (isCurryear)
                    sqlQuery = @"Select lsf from dbo.lsfann where owner = '" + survey + "' and lsfno>=4 and lsfno <=15 and newtc = '" + newtc + "' order by lsfno desc";
                else
                    sqlQuery = @"Select lsf from dbo.lsfann where owner = '" + survey + "' and lsfno>=16 and lsfno <=27 and newtc = '" + newtc + "' order by lsfno desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

           return dt;
        }

        //Get related BST data
        public DataTable GetRelatedBST(string year, string survey, string newtc)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
                
                sqlQuery = @"Select bst from dbo.bstann where owner = '" + survey + "' and sdate between '" + year + "01'  and '" + year + "12'" + " and newtc = '" + newtc + "'";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        public DataTable BuildTopTable(string survey, DataTable dtproj, DataTable dtlsf, DataTable dtbst)
        {
            DataTable dt = new DataTable();

            double und;
            if (survey == "N" || survey == "U")
                und = 1.25;
            else if (survey == "F")
                und = 1.01;
            else
                und = 1.00;

            //Create table
            dt.Columns.Add("Type", typeof(string));
            dt.Columns.Add("c1", typeof(double));
            dt.Columns.Add("c2", typeof(double));
            dt.Columns.Add("c3", typeof(double));
            dt.Columns.Add("c4", typeof(double));
            dt.Columns.Add("c5", typeof(double));
            dt.Columns.Add("c6", typeof(double));
            dt.Columns.Add("c7", typeof(double));
            dt.Columns.Add("c8", typeof(double));
            dt.Columns.Add("c9", typeof(double));
            dt.Columns.Add("c10", typeof(double));
            dt.Columns.Add("c11", typeof(double));
            dt.Columns.Add("c12", typeof(double));

            //add vip row
            DataRow dr = dt.NewRow();
            dr[0] = "Unadjusted VIP";
            dr[1] = dtproj.AsEnumerable().Sum(row => row.Field <int>("c1"));
            dr[2] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c2"));
            dr[3] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c3"));
            dr[4] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c4"));
            dr[5] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c5"));
            dr[6] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c6"));
            dr[7] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c7"));
            dr[8] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c8"));
            dr[9] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c9"));
            dr[10] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c10"));
            dr[11] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c11"));
            dr[12] = dtproj.AsEnumerable().Sum(row => row.Field<int>("c12"));
            dt.Rows.Add(dr);

            //add lsf row
            dr = dt.NewRow();
            int num_lsf = dtlsf.Rows.Count;
            if (num_lsf <12)
            {
                for (int i =num_lsf; i<=12; i++)
                    dtlsf.Rows.Add(1.00);
            }
            dr[0] = "LSF";
            dr[1] = dtlsf.Rows[0][0];
            dr[2] = dtlsf.Rows[1][0];
            dr[3] = dtlsf.Rows[2][0];
            dr[4] = dtlsf.Rows[3][0];
            dr[5] = dtlsf.Rows[4][0];
            dr[6] = dtlsf.Rows[5][0];
            dr[7] = dtlsf.Rows[6][0];
            dr[8] = dtlsf.Rows[7][0];
            dr[9] = dtlsf.Rows[8][0];
            dr[10] = dtlsf.Rows[9][0];
            dr[11] = dtlsf.Rows[10][0];
            dr[12] = dtlsf.Rows[11][0];
            dt.Rows.Add(dr);

            //add lsf vip row
            dr = dt.NewRow();
            dr[0] = "*LSF VIP";
            for (int i = 1; i <= 12; i++)
            {
                dr[i] = Convert.ToInt32(Math.Round(Convert.ToDouble(dt.Rows[0][i]) * Convert.ToDouble(dt.Rows[1][i])));
            }
            dt.Rows.Add(dr);
            
            //add undercovrerage row
            dr = dt.NewRow();
            dr[0] = "Undercoverage";
            for (int i = 1; i <= 12; i++)
            {
                dr[i] = und;
            }
            
            dt.Rows.Add(dr);

            //add undercoverage vip row
            dr = dt.NewRow();
            dr[0] = "*Undercoverage VIP";
            for (int i = 1; i <= 12; i++)
            {
                dr[i] = Convert.ToInt32(Math.Round((Convert.ToDouble(dt.Rows[0][i]) * Convert.ToDouble(dt.Rows[1][i])*und),0,MidpointRounding.AwayFromZero)); 
            }

            dt.Rows.Add(dr);
            
            //add bst
            dr = dt.NewRow();
            dr[0] = "Benchmark";
            if (survey != "M")
            {
                dr[1] = dtbst.Rows[0][0];
                dr[2] = dtbst.Rows[1][0];
                dr[3] = dtbst.Rows[2][0];
                dr[4] = dtbst.Rows[3][0];
                dr[5] = dtbst.Rows[4][0];
                dr[6] = dtbst.Rows[5][0];
                dr[7] = dtbst.Rows[6][0];
                dr[8] = dtbst.Rows[7][0];
                dr[9] = dtbst.Rows[8][0];
                dr[10] = dtbst.Rows[9][0];
                dr[11] = dtbst.Rows[10][0];
                dr[12] = dtbst.Rows[11][0];
            }
            else
            {
                dr[1] = 1.00;
                dr[2] = 1.00;
                dr[3] = 1.00;
                dr[4] = 1.00;
                dr[5] = 1.00;
                dr[6] = 1.00;
                dr[7] = 1.00;
                dr[8] = 1.00;
                dr[9] = 1.00;
                dr[10] = 1.00;
                dr[11] = 1.00;
                dr[12] = 1.00;
            }
            dt.Rows.Add(dr);

            //Add bst vip row
            dr = dt.NewRow();
            dr[0] = "*Benchmark VIP";
            if (survey != "M")
            {
                for (int i = 1; i <= 12; i++)
                {
                    dr[i] = Convert.ToInt32(Math.Round((Convert.ToDouble(dt.Rows[0][i]) * Convert.ToDouble(dt.Rows[1][i]) * und * Convert.ToDouble(dtbst.Rows[i-1][0])), 0, MidpointRounding.AwayFromZero));
                }
            }
            else
            {
                for (int i = 1; i <= 12; i++)
                {
                    dr[i] = Convert.ToInt32(Math.Round((Convert.ToDouble(dt.Rows[0][i]) * Convert.ToDouble(dt.Rows[1][i]) * und * 1.00), 0, MidpointRounding.AwayFromZero));
                }
            }

            dt.Rows.Add(dr);

            return dt;
        }

        //Get update list for worksheet
        public DataTable GetTabAnnualBeaDataWorksheetUpdated(string year, string survey, string newtc)
        {
            DataTable dt = new DataTable();

            string getTc = newtc.Trim();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
               
                string t1 = @"1T";

                if (String.Equals(getTc, t1, StringComparison.Ordinal))
                {
                    sqlQuery = @"SELECT id,  newtc, Date6, wgtvip, vipflag, wgtdif from dbo.annupd where substring(date6, 1, 4) ='" + year + "' and owner = '" + survey + "'";
                    sqlQuery = sqlQuery + " and substring(newtc, 1, 2) >= '20' and substring(newtc, 1, 2) <='39'";
                }
                else
                {
                    sqlQuery = @"SELECT id,  newtc, Date6, wgtvip, vipflag, wgtdif from dbo.annupd where substring(date6, 1, 4) ='" + year + "' and owner = '" + survey + "'";
                    sqlQuery = sqlQuery + " and newtc like '" + getTc + "%'";
                }

                sqlQuery = sqlQuery + " order by id";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //update annproj table
        public void UpdateAnnProj(DataTable dt, string year)
        {
            bool isCurryear = IsCurrentYear(year);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i]["modified"] > 0)
                {
                    using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                    {
                        string usql;
                        SqlCommand update_command;
                        if (isCurryear)
                        {
                            usql = "UPDATE dbo.annproj SET " +
                                            "VCY01 = @VCY01, " +
                                            "VCY02 = @VCY02, " +
                                            "VCY03 = @VCY03, " +
                                            "VCY04 = @VCY04, " +
                                            "VCY05 = @VCY05, " +
                                            "VCY06 = @VCY06, " +
                                            "VCY07 = @VCY07, " +
                                            "VCY08 = @VCY08, " +
                                            "VCY09 = @VCY09, " +
                                            "VCY10 = @VCY10, " +
                                            "VCY11 = @VCY11, " +
                                            "VCY12 = @VCY12," +
                                            "Cumvip = @Cumvip " +
                                            "WHERE ID = @ID";

                            update_command = new SqlCommand(usql, sql_connection);
                            update_command.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                            update_command.Parameters.AddWithValue("@VCY01", Convert.ToInt32(dt.Rows[i]["c1"]));
                            update_command.Parameters.AddWithValue("@VCY02", Convert.ToInt32(dt.Rows[i]["c2"]));
                            update_command.Parameters.AddWithValue("@VCY03", Convert.ToInt32(dt.Rows[i]["c3"]));
                            update_command.Parameters.AddWithValue("@VCY04", Convert.ToInt32(dt.Rows[i]["c4"]));
                            update_command.Parameters.AddWithValue("@VCY05", Convert.ToInt32(dt.Rows[i]["c5"]));
                            update_command.Parameters.AddWithValue("@VCY06", Convert.ToInt32(dt.Rows[i]["c6"]));
                            update_command.Parameters.AddWithValue("@VCY07", Convert.ToInt32(dt.Rows[i]["c7"]));
                            update_command.Parameters.AddWithValue("@VCY08", Convert.ToInt32(dt.Rows[i]["c8"]));
                            update_command.Parameters.AddWithValue("@VCY09", Convert.ToInt32(dt.Rows[i]["c9"]));
                            update_command.Parameters.AddWithValue("@VCY10", Convert.ToInt32(dt.Rows[i]["c10"]));
                            update_command.Parameters.AddWithValue("@VCY11", Convert.ToInt32(dt.Rows[i]["c11"]));
                            update_command.Parameters.AddWithValue("@VCY12", Convert.ToInt32(dt.Rows[i]["c12"]));
                            update_command.Parameters.AddWithValue("@Cumvip", Convert.ToInt32(dt.Rows[i]["Cumvip"]));
                        }
                        else
                        {
                            usql = "UPDATE dbo.annproj SET " +
                                            "VPY01 = @VPY01, " +
                                            "VPY02 = @VPY02, " +
                                            "VPY03 = @VPY03, " +
                                            "VPY04 = @VPY04, " +
                                            "VPY05 = @VPY05, " +
                                            "VPY06 = @VPY06, " +
                                            "VPY07 = @VPY07, " +
                                            "VPY08 = @VPY08, " +
                                            "VPY09 = @VPY09, " +
                                            "VPY10 = @VPY10, " +
                                            "VPY11 = @VPY11, " +
                                            "VPY12 = @VPY12, " +
                                            "Cumvip = @Cumvip " +
                                            "WHERE ID = @ID";

                            update_command = new SqlCommand(usql, sql_connection);
                            update_command.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                            update_command.Parameters.AddWithValue("@VPY01", Convert.ToInt32(dt.Rows[i]["c1"]));
                            update_command.Parameters.AddWithValue("@VPY02", Convert.ToInt32(dt.Rows[i]["c2"]));
                            update_command.Parameters.AddWithValue("@VPY03", Convert.ToInt32(dt.Rows[i]["c3"]));
                            update_command.Parameters.AddWithValue("@VPY04", Convert.ToInt32(dt.Rows[i]["c4"]));
                            update_command.Parameters.AddWithValue("@VPY05", Convert.ToInt32(dt.Rows[i]["c5"]));
                            update_command.Parameters.AddWithValue("@VPY06", Convert.ToInt32(dt.Rows[i]["c6"]));
                            update_command.Parameters.AddWithValue("@VPY07", Convert.ToInt32(dt.Rows[i]["c7"]));
                            update_command.Parameters.AddWithValue("@VPY08", Convert.ToInt32(dt.Rows[i]["c8"]));
                            update_command.Parameters.AddWithValue("@VPY09", Convert.ToInt32(dt.Rows[i]["c9"]));
                            update_command.Parameters.AddWithValue("@VPY10", Convert.ToInt32(dt.Rows[i]["c10"]));
                            update_command.Parameters.AddWithValue("@VPY11", Convert.ToInt32(dt.Rows[i]["c11"]));
                            update_command.Parameters.AddWithValue("@VPY12", Convert.ToInt32(dt.Rows[i]["c12"]));
                            update_command.Parameters.AddWithValue("@Cumvip", Convert.ToInt32(dt.Rows[i]["Cumvip"]));

                        }

                        try
                        {
                            sql_connection.Open();
                            int count = update_command.ExecuteNonQuery();

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
                }
            }
        }
               
        //Update  change data
        public void UpdateChangeData(string survey, string newtc,  string year, DataTable dt)
        {
            newtc = newtc.Trim();

            //Delete older data
            string newtc_str = getNewtcString(newtc);
            string sqlQuery = @"Delete from dbo.annupd where owner = " + GeneralData.AddSqlQuotes(survey) + " and " + newtc_str + " and substring(date6, 1, 4) = " + GeneralData.AddSqlQuotes(year);
            DeleteChangeData(sqlQuery);

            //Add back data from dt
            AddChangeData(dt, survey);
        }

        private string GetsdateinVipsttabTable()
        {
            string sdate = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Top 1 sdate FROM dbo.vipsttab";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    sdate = reader["SDATE"].ToString();
                }
                else
                {
                    return null;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }

            return sdate;
        }

       
        /*create a empty table */
        private DataTable GetEmptyTable(string sdate)
        {
            // Here we create a DataTable with t columns.

            DataTable table = new DataTable();
          
            table.Columns.Add("OWNER", typeof(string));
            table.Columns.Add("FIPSTATE", typeof(string));
            table.Columns.Add("NEWTC", typeof(string));

            int month = Convert.ToInt16(sdate.Substring(4, 2));
            for (int i = month; i <= 24+month; i++)
            {
                table.Columns.Add("T"+i, typeof(int));
            }

            return table;
        }

        //update vipsttab table for the newtc, survey 
        public void UpdateVipBeaData(string survey, string newtc,  DataTable dt)
        {
            string sdate = GetsdateinVipsttabTable();
            int syear = Convert.ToInt16(sdate.Substring(0, 4));
            int smon = Convert.ToInt16(sdate.Substring(4, 2));

            DataTable dtt= GetEmptyTable(sdate);

            string save_id = string.Empty;
            string fs = string.Empty;
            
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                //get id
                string id = dt.Rows[i]["ID"].ToString();
                if (save_id != id)
                {
                    SampleData sampdata = new SampleData();
                    Sample samp = sampdata.GetSampleData(id);
                    MasterData mastdata = new MasterData();
                    Master mast = mastdata.GetMasterData(samp.Masterid);
        
                    string date6 = dt.Rows[i]["DATE6"].ToString();
                    int mondiff = (syear - Convert.ToInt16(date6.Substring(0, 4))) * 12 + (smon - Convert.ToInt16(date6.Substring(4, 2))) ;

                    DataRow dr = dtt.NewRow();
                   
                    dr["OWNER"] = mast.Owner;
                    dr["FIPSTATE"] = mast.Fipstate;
                    dr["NEWTC"] = dt.Rows[i]["NEWTC"].ToString();
                    for (int j = smon; j <= (24+smon); j++)
                    {
                        if (j== mondiff)
                            dr["T" + mondiff] = (int)dt.Rows[i]["WGTDIF"];
                        else
                            dr["T" +j] = 0;
                    }

                    dtt.Rows.Add(dr);
                }
                else
                {
                    string date6 = dt.Rows[i]["DATE6"].ToString();
                    int mondiff = (syear - Convert.ToInt16(date6.Substring(0, 4))) * 12 + (smon - Convert.ToInt16(date6.Substring(4, 2)));
                    dtt.Rows[dtt.Rows.Count-1]["T" + mondiff] = (int)dt.Rows[i]["WGTDIF"];
                }
            
                save_id = id;
            }

            //int numcount = dtt.Rows.Count;

            //update vipsttab and vipbea tables base on empty table
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            sql_connection.Open();

            
            for (int i = 0; i < dtt.Rows.Count; i++)
            {
                String updateQuery = string.Empty;

                string newtc4 = dtt.Rows[i]["NEWTC"].ToString();

                //update four and three digits newtc in vipsttab
                updateQuery = "update dbo.vipsttab " +
                              " set t" + smon + " = " + "t" + smon + " + (" + (int)dtt.Rows[i]["t" + smon] + "),";
                for (int k = smon+1; k <= 24+smon; k++)
                {
                    if (k < 24+smon)
                        updateQuery = updateQuery + "t" + k + " = " + "t" + k + " + (" + (int)dtt.Rows[i]["t" + k] + "),";
                    else
                        updateQuery = updateQuery + "t" + k + " = " + "t" + k + " + (" + (int)dtt.Rows[i]["t" + k] + ")";
                }

                updateQuery = updateQuery + " where (newtc = '" + newtc4 + "' or newtc = '" + newtc4.Substring(0, 3) + "') and owner = '" + survey + "' and fipstate = '" + dtt.Rows[i]["FIPSTATE"].ToString() + "'";

                using (var dbcm = new SqlCommand(updateQuery, sql_connection))
                {
                    dbcm.ExecuteNonQuery();
                }

                string owner = dtt.Rows[i]["OWNER"].ToString();

                //update four and three digits newtc in vipbea if survey is P or F
                if ((owner == "S" || owner == "L" ||owner == "C" || owner == "D"))
                {
                    //update four and three digits newtc in vipsttab
                    updateQuery = "update dbo.vipbea " +
                                  " set t" + smon + " = " + "t" + smon + " + (" + (int)dtt.Rows[i]["t" + smon] + "),";
                    for (int k = smon+1; k <= 24+smon; k++)
                    {
                        if (k < 24+smon)
                            updateQuery = updateQuery + "t" + k + " = " + "t" + k + " + (" + (int)dtt.Rows[i]["t" + k] + "),";
                        else
                            updateQuery = updateQuery + "t" + k + " = " + "t" + k + " + (" + (int)dtt.Rows[i]["t" + k] + ")";
                    }

                    updateQuery = updateQuery + " where (newtc = '" + newtc4 + "' or newtc = '" + newtc4.Substring(0, 3) + "') and owner = '" + survey + "'";

                    if (owner == "S" || owner == "C")
                        updateQuery = updateQuery + " and tab =1";
                    else
                        updateQuery = updateQuery + " and tab =2";

                    using (var dbcm = new SqlCommand(updateQuery, sql_connection))
                    {
                        dbcm.ExecuteNonQuery();
                    }

                }
            }

            sql_connection.Close();
        }

        //get where clause for newtc when the newtc is 1T
        private string getNewtcString(string newtc)
        {
            string newtc_str;
            newtc = newtc.Trim();
            if (newtc == "1T")
                newtc_str = " substring(newtc, 1, 2) in ('20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31', '32', '33', '34', '35', '36', '37', '38', '39')";
            else
                newtc_str = " substring(newtc, 1, 2) =" + GeneralData.AddSqlQuotes(newtc);

            return newtc_str;
        }

        /*Delete old changed data */
        private bool DeleteChangeData(string sql_string)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            SqlCommand delete_command = new SqlCommand(sql_string, sql_connection);

            try
            {
                sql_connection.Open();
                int count = delete_command.ExecuteNonQuery();
                if (count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /*Add data to data to annupd table */
        private void AddChangeData(DataTable dt, string survey)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            sql_connection.Open();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String InsertQuery = string.Empty;

                InsertQuery = "INSERT INTO dbo.annupd " +
                              "(id, owner, newtc, date6, wgtdif, wgtvip, vipflag) " +
                              "VALUES ('" + dt.Rows[i]["ID"].ToString() + "','" + survey + "','" + dt.Rows[i]["NEWTC"].ToString() + "','" + dt.Rows[i]["DATE6"].ToString() + "','" + Convert.ToInt32(dt.Rows[i]["WGTDIF"]) + "','" + Convert.ToInt32(dt.Rows[i]["WGTVIP"]) + "','" + dt.Rows[i]["VIPFLAG"].ToString() + "')";

                using (var dbcm = new SqlCommand(InsertQuery, sql_connection))
                {
                    dbcm.ExecuteNonQuery();
                }
            }
            sql_connection.Close();
        }

       //get revision data
        public DataTable GetBEARevisionData(string survey, string tc2, string date6)
        {
            bool isCurryear = IsCurrentYear(date6.Substring(0,4));
            string mon = date6.Substring(4, 2);

            string table = string.Empty;
            string colwgt = string.Empty;
            string colflag = string.Empty;
            if (IsCurrentYear(date6.Substring(0, 4)))
            {
                table = "dbo.VIPFINAL_CY";
                colwgt = "VCY" + mon;
                colflag = "FCY" + mon;
            }
            else
            {
                table = "dbo.VIPFINAL_PY";
                colwgt = "VPY" + mon;
                colflag = "FPY" + mon;
            }

            string owner_str = string.Empty;
            if (survey == "N")
                owner_str = "owner = 'N'";
	        else if (survey == "M")
                owner_str = "owner = 'M'";
	        else if (survey == "P")
                owner_str = "owner in ('S', 'P', 'L')";
	        else if (survey == "F")
                owner_str = "owner in ('C', 'D', 'F')";
	        else if (survey == "U")
                owner_str = "owner in ('T', 'E', 'G', 'R', 'O', 'W')";

            string not_owner_str = string.Empty;
            if (survey == "N")
                not_owner_str = "owner <> 'N'";
            else if (survey == "M")
                not_owner_str = "owner <> 'M'";
            else if (survey == "P")
                not_owner_str = "owner not in ('S', 'P', 'L')";
            else if (survey == "F")
                not_owner_str = "owner not in ('C', 'D', 'F')";
            else if (survey == "U")
                not_owner_str = "owner not in ('T', 'E', 'G', 'R', 'O', 'W')";

            string not_newtc_str = string.Empty;
            string newtc_str = string.Empty;

            if (tc2 == "1T")
                newtc_str = "substring(v.newtc, 1, 2) >= '20'";
            else
                newtc_str = "substring(v.newtc, 1, 2) = " + GeneralData.AddSqlQuotes(tc2);

            if (tc2 == "1T")
                not_newtc_str = "substring(v.newtc, 1, 2) < '20'";
            else
                not_newtc_str = "substring(v.newtc, 1, 2) <>" + GeneralData.AddSqlQuotes(tc2);


            string tc_str = string.Empty;
            string not_tc_str = string.Empty;
            if (tc2 == "1T")
                tc_str = "tabtc >= '20'";
            else
                tc_str = "tabtc =" + GeneralData.AddSqlQuotes(tc2);

            if (tc2 == "1T")
                not_tc_str = "tabtc < '20'";
            else
                not_tc_str = "tabtc <>" + GeneralData.AddSqlQuotes(tc2);

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = "With t2 as ( ";
                sqlQuery = sqlQuery + " select a.id, a.newtc, a.status, a.owner, a.seldate, a.strtdate, ";
                sqlQuery = sqlQuery + "(" + colwgt + "-v.WGTVIP) as change, " + colwgt + " as curWgt, " + colflag + " as curflag, v.WGTVIP as preWgt, v.VIPFLAG as preflag, a.fwgt as curfwgt, v.fwgt as prefwgt,";
                sqlQuery = sqlQuery + " Round(COALESCE(" + colwgt + "/ nullif(a.fwgt,0), 0), 0) as CURR, round(COALESCE(WGTVIP/ nullif(v.fwgt,0),0), 0) as Prev, v.owner as powner, v.status as pstatus, v.newtc as pnewtc ";
                sqlQuery = sqlQuery + " from dbo.annproj a, " + table + " v where a.id = v.id and  date6 = " + date6 + " and a." + owner_str + " and a." + tc_str + " and v."+ owner_str + " and " + newtc_str  + " and " + colwgt + " <> WGTVIP";
                sqlQuery = sqlQuery + " union ";
                sqlQuery = sqlQuery + " select a.id, a.newtc, a.status, a.owner, a.seldate, a.strtdate, ";
                sqlQuery = sqlQuery + "(" + colwgt + "-0) as change, " + colwgt + " as curWgt, " + colflag + " as curflag, 0 as preWgt, ' ' as preflag, a.fwgt as curfwgt, v.fwgt as prefwgt,";
                sqlQuery = sqlQuery + " Round(COALESCE(" + colwgt + "/ nullif(a.fwgt,0), 0), 0) as CURR, 0 as Prev, v.owner as powner, v.status as pstatus, v.newtc as pnewtc ";
                sqlQuery = sqlQuery + " from dbo.annproj a, " + table + " v where a.id = v.id and  date6 = " + date6 + " and a." + owner_str + " and a." + tc_str + " and (v."+ not_owner_str + " or " + not_newtc_str  + ") and " + colwgt + " > 0";
                sqlQuery = sqlQuery + " union ";
                sqlQuery = sqlQuery + " select a.id, a.newtc, a.status, a.owner, a.seldate, a.strtdate, ";
                sqlQuery = sqlQuery + "( 0-v.WGTVIP) as change, 0 as curWgt, ' ' as curflag, v.WGTVIP as preWgt, v.VIPFLAG as preflag, a.fwgt as curfwgt, v.fwgt as prefwgt,";
                sqlQuery = sqlQuery + " 0 as CURR, round(COALESCE(WGTVIP/ nullif(v.fwgt,0),0), 0) as Prev, v.owner as powner, v.status as pstatus, v.newtc as pnewtc ";
                sqlQuery = sqlQuery + " from dbo.annproj a, " + table + " v where a.id = v.id and  date6 = " + date6 + " and (a." + not_owner_str + " or a." + not_tc_str + ") and (v." + owner_str + " and " + newtc_str + ") and v.WGTVIP >0 ";
                sqlQuery = sqlQuery + " union ";
                sqlQuery = sqlQuery + " select a.id, a.newtc, a.status, a.owner, a.seldate, a.strtdate, ";
                sqlQuery = sqlQuery + colwgt + " as change, " + colwgt + " as curWgt, " + colflag + " as curflag, 0 as preWgt, ' ' as preflag, a.fwgt as curfwgt, 0.00 as prefwgt,";
                sqlQuery = sqlQuery + " Round(COALESCE(" + colwgt + "/ nullif(a.fwgt,0), 0), 0) as CURR, 0 as Prev, a.owner as powner, a.status as pstatus, a.newtc as pnewtc ";
                sqlQuery = sqlQuery + " from dbo.annproj a where a.id not in (select id from " + table + " where date6 = " + date6 + ") and a." + owner_str + " and " + tc_str + " and " + colwgt + "> 0 ";
                sqlQuery = sqlQuery + " union ";
                sqlQuery = sqlQuery + " select v.id, m.newtc, s.status, m.owner, m.seldate, s.strtdate, ";
                sqlQuery = sqlQuery + "(0-v.WGTVIP) as change, 0 as curWgt, ' ' as curflag, v.WGTVIP as preWgt, v.VIPFLAG as preflag, s.fwgt as curfwgt, v.fwgt as prefwgt,";
                sqlQuery = sqlQuery + " 0 as CURR, round(COALESCE(WGTVIP/ nullif(v.fwgt,0),0), 0) as Prev, v.owner as powner, v.status as pstatus, v.newtc as pnewtc ";
                sqlQuery = sqlQuery + " from " + table + " v, dbo.sample s, dbo.master m where v.id = s.id  and s.masterid = m.masterid and v.id not in (select id from dbo.annproj) and  date6 = " + date6 + " and v." + owner_str + " and " + newtc_str;
                sqlQuery = sqlQuery + " )";
                sqlQuery = sqlQuery + " select * from t2 order by change desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SURVEY", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(survey);
                    cmd.Parameters.AddWithValue("@TC2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(tc2);
                    cmd.Parameters.AddWithValue("@DATE6", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(date6);

                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //Get project list for update tc
        public DataTable GetTabAnnualBeaTCProjects(string tc, string selectedYear)
        {
            DataTable dt = new DataTable();

            string getTc = tc.Trim();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery;
                string andQuery = "";
                string colQuery = "";
                string flagQuery = "";

                andQuery = " and owner in ('T', 'G', 'E', 'O', 'W', 'R')";
                andQuery = andQuery + " and status in ('1', '2', '3', '7', '8')";

                if (IsCurrentYear(selectedYear))
                {
                    colQuery = "VCY01 as c1, FCY01 as f1, VCY02 as c2, FCY02 as f2, VCY03 as c3, FCY03 as f3, VCY04 as c4, FCY04 as f4, VCY05 as c5, FCY05 as f5, VCY06 as c6, FCY06 as f6, VCY07 as c7, FCY07 as f7, VCY08 as c8, FCY08 as f8, VCY09 as c9, FCY09 as f9, VCY10 as c10, FCY10 as f10, VCY11 as c11, FCY11 as f11, VCY12 as c12, FCY12 as f12 ";
                    flagQuery = " and (FCY01 <> '' or FCY02 <> '' or FCY03 <> '' or FCY04 <> '' or FCY05 <> '' or FCY06 <> '' or FCY07 <> '' or FCY08 <> '' or FCY09 <> '' or FCY10 <> '' or FCY11 <> '' or FCY12 <> '')";
                }
                else
                {
                    colQuery = "VPY01 as c1, FPY01 as f1, VPY02 as c2, FPY02 as f2, VPY03 as c3, FPY03 as f3, VPY04 as c4, FPY04 as f4, VPY05 as c5, FPY05 as f5, VPY06 as c6, FPY06 as f6, VPY07 as c7, FPY07 as f7, VPY08 as c8, FPY08 as f8, VPY09 as c9, FPY09 as f9, VPY10 as c10, FPY10 as f10, VPY11 as c11, FPY11 as f11, VPY12 as c12, FPY12 as f12 ";
                    flagQuery = " and (FPY01 <> '' or FPY02 <> '' or FPY03 <> '' or FPY04 <> '' or FPY05 <> '' or FPY06 <> '' or FPY07 <> '' or FPY08 <> '' or FPY09 <> '' or FPY10 <> '' or FPY11 <> '' or FPY12 <> '')";
                }
                sqlQuery = @"SELECT id,  newtc," + colQuery + ", strtdate, compdate, rvitm5c, item6, fwgt, status, 0 as modified";
                sqlQuery = sqlQuery + " from dbo.annproj where newtc like '" + getTc + "%'" + andQuery + flagQuery;
              
                sqlQuery = sqlQuery + " order by id";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //update annproj table
        public void UpdateAnnProjForTC(DataTable dt)
        {
       
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i]["modified"] > 0)
                {
                    using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                    {
                        string usql;
                        SqlCommand update_command;

                        usql = "UPDATE dbo.annproj SET " +
                                        "NEWTC = @NEWTC, TABTC = @TABTC " +
                                        "WHERE ID = @ID";

                        update_command = new SqlCommand(usql, sql_connection);
                        update_command.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                        update_command.Parameters.AddWithValue("@NEWTC", dt.Rows[i]["NEWTC"].ToString());
                        update_command.Parameters.AddWithValue("@TABTC", dt.Rows[i]["NEWTC"].ToString().Substring(0,2));

                        try
                        {
                            sql_connection.Open();
                            int count = update_command.ExecuteNonQuery();

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
                }
            }
        }

        //get data for Seasonally Adjusted Annual Rate for Utilities
        public DataTable GetTabAnnualBeaDataUtitlities(string year, int boost)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_AnnualBEAForUtilities";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@boost", SqlDbType.Int).Value = boost;
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

            }
            return dt;
        }


    } 
}
