/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.WorksheetData.cs	    	
Programmer:         Christine Zhang
Creation Date:      1/31/2017
Inputs:             None
Parameters:	        survey date, owner, level and newtc
Outputs:	        worksheet data	
Description:	    data layer to get data for worksheet
Detailed Design:    Vip Worksheet Detailed Design
Other:	            Called by: frmWorksheet
 
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
using System.Globalization;


namespace CprsDAL
{
    public class WorksheetData
    {
        /*Retrieve total vip data for an owner and level and newtc*/
        public DataTable GetVipTotalData(string sdate, string owner, string level, string newtc)
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
                sqlQuery = sqlQuery + " r4= case when pm3=0 then 0 else cast((cm4-pm3)*100/pm3  as decimal(10,2)) end ";
                sqlQuery = sqlQuery + " from dbo.vipsadj v, dbo.PUBTCLIST p ";
                sqlQuery = sqlQuery + " where v.NEWTC =p.newtc and sdate = " + GeneralData.AddSqlQuotes(sdate);
                if (level == "1")
                    sqlQuery = sqlQuery + " and v.ddown = " + GeneralData.AddSqlQuotes(level);
                else
                    sqlQuery = sqlQuery + " and (p.newtc = " + GeneralData.AddSqlQuotes(newtc) + ")";
                sqlQuery = sqlQuery + " and owner = " + GeneralData.AddSqlQuotes(owner) + " and v.newtc = " + GeneralData.AddSqlQuotes(newtc);
                sqlQuery = sqlQuery + " order by p.newtc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

            }

            return dt;
        }

        /*Retrieve main data for an owner and level and newtc*/
        public DataTable GetMainData(string sdate, string owner, string newtc)
        {
            DataTable dt = new DataTable();
            string where_clause = "where owner = " + GeneralData.AddSqlQuotes(owner) + " and newtc = " + GeneralData.AddSqlQuotes(newtc) + " and sdate = " + GeneralData.AddSqlQuotes(sdate);
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                DateTime dtime = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

                //get month name
                string curmon = (dtime.ToString("MMMM", CultureInfo.InvariantCulture));
                string premon = (dtime.AddMonths(-1)).ToString("MMMM", CultureInfo.InvariantCulture);
                string ppmon = (dtime.AddMonths(-2)).ToString("MMMM", CultureInfo.InvariantCulture);
                string p3mon = (dtime.AddMonths(-3)).ToString("MMMM", CultureInfo.InvariantCulture);
                string p4mon = (dtime.AddMonths(-4)).ToString("MMMM", CultureInfo.InvariantCulture);

                string sqlQuery = @" select 1 as n, '" + curmon + "' as month, u_cm0, l_cm0, c_cm0,  b_cm0, s_cm0, cm0, lsf0,ucf, bst0, saf0 from dbo.VIPSADJ " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select  2 as n, '" + premon + "' as month, u_cm1, l_cm1, c_cm1,  b_cm1, s_cm1, cm1, lsf1, ucf, bst1, saf1 from dbo.VIPSADJ " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 3 as n, '" + ppmon + "' as month, u_cm2, l_cm2, c_cm2, b_cm2, s_cm2, cm2, lsf2, ucf, bst2, saf2 from dbo.VIPSADJ " + where_clause; 
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 4 as n, " + "'Previous " + premon + "' as month, u_pm0, l_pm0, c_pm0, b_pm0, s_pm0,  pm0, lsf0, ucf, bst1, saf1 from dbo.VIPSADJ " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 5 as n," + "'Previous " + ppmon + "' as month, u_pm1, l_pm1, c_pm1, b_pm1, s_pm1, pm1, lsf1, ucf, bst2, saf2 from dbo.VIPSADJ  " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 6 as n, '" + p3mon + "' as month, u_cm3, l_cm3, c_cm3,  b_cm3, s_cm3, cm3, lsf3, ucf, bst3, saf3 from dbo.VIPSADJ " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 7 as n, '" + p4mon + "' as month, u_cm4, l_cm4, c_cm4,  b_cm4, s_cm4, cm4, lsf4, ucf, bst4, saf4 from dbo.VIPSADJ " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 8 as n, " + "'Previous " + p3mon + "' as month, u_pm2, l_pm2, c_pm2, b_pm2, s_pm2,  pm2, lsf2, ucf, bst3, saf3 from dbo.VIPSADJ " + where_clause;
                       sqlQuery = sqlQuery + " union ";
                       sqlQuery = sqlQuery + " select 9 as n," + "'Previous " + p4mon + "' as month, u_pm3, l_pm3, c_pm3, b_pm3, s_pm3, pm3, lsf3, ucf, bst4, saf4 from dbo.VIPSADJ  " + where_clause;
                       sqlQuery = sqlQuery + " order by n";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //get where clause for newtc when the newtc is 1T
        private string getNewtcStringForIT(string newtc)
        {
            string newtc_str;
            newtc = newtc.Trim();
            if (newtc == "1T")
                newtc_str = " substring(newtc, 1, 2) in ('20', '21', '22', '23', '24', '25', '26', '27', '28', '29', '30', '31', '32', '33', '34', '35', '36', '37', '38', '39')";
            else if (newtc == "1T0")
                newtc_str = " substring(newtc, 1, 3) in ('200', '210', '220', '230', '240', '250', '260', '270', '280', '290', '300', '310', '320', '330', '340', '350', '360', '370', '380', '390')";
            else if (newtc == "1T1")
                newtc_str = " substring(newtc, 1, 3) in ('201', '211', '221', '231', '241', '251', '261', '271', '281', '291', '301', '311', '321', '331', '341', '351', '361', '371', '381', '391')";
            else if (newtc == "1T2")
                newtc_str = " substring(newtc, 1, 3) in ('202', '212', '222', '232', '242', '252', '262', '272', '282', '292', '302', '312', '322', '332', '342', '352', '362', '372', '382', '392')";
            else if (newtc == "1T3")
                newtc_str = " substring(newtc, 1, 3) in ('203', '213', '223', '233', '243', '253', '263', '273', '283', '293', '303', '313', '323', '333', '343', '353', '363', '373', '383', '393')";
            else
                newtc_str = " newtc like replace('" + newtc + "%', ' ', '')";

            return newtc_str;
        }


        //get preliminary, rev1 and rev2 change data based table no
        public DataTable GetChangeData(string owner, string newtc, int table_no)
        {
            DataTable dt = new DataTable();

            newtc = newtc.Trim();

            string rev_mn = "CM_0";
            if (table_no == 1)
                rev_mn = "CM_0";
            else if (table_no == 2)
                rev_mn = "CM_1";
            else if (table_no == 3)
                rev_mn = "CM_2";
            else if (table_no == 4)
                rev_mn = "CM_3";
            else
                rev_mn = "CM_4";
            
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                newtc = newtc.Trim();
                string newtc_str = getNewtcStringForIT(newtc);

                string sqlQuery = @" select ID, WGTVIP, VIPFLAG, WGTDIF, owner, newtc from dbo.vipupd where owner = " + GeneralData.AddSqlQuotes(owner) + " and " + newtc_str + " and revmn =" +  GeneralData.AddSqlQuotes(rev_mn) + " order by id";
                
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //Update preliminary, rev1 and rev2 change data
        public void UpdateChangeData(string owner, string newtc, DataTable  dt, int table_no)
        {
           newtc = newtc.Trim();

           string rev_mn = "CM_0";
           if (table_no == 1)
               rev_mn = "CM_0";
           else if (table_no == 2)
               rev_mn = "CM_1";
           else if (table_no == 3)
               rev_mn = "CM_2";
           else if (table_no == 4)
               rev_mn = "CM_3";
           else
               rev_mn = "CM_4";
           
            //Delete older data
           string newtc_str = getNewtcStringForIT(newtc);
           string sqlQuery = @"Delete from dbo.vipupd where owner = "  + GeneralData.AddSqlQuotes(owner) + " and " + newtc_str + " and revmn = " + GeneralData.AddSqlQuotes(rev_mn);
           DeleteChangeData(sqlQuery);

            //Add back data from dt
           AddChangeData( dt, table_no);

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

        /*Add data to data to vipupd table */
        private void AddChangeData(DataTable dt, int table_no)
        {
            string rev_mn = "CM_0";
            if (table_no == 1)
                rev_mn = "CM_0";
            else if (table_no == 2)
                rev_mn = "CM_1";
            else if (table_no == 3)
                rev_mn = "CM_2";
            else if (table_no == 4)
                rev_mn = "CM_3";
            else
                rev_mn = "CM_4";
                
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            sql_connection.Open();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                String InsertQuery = string.Empty;

                InsertQuery = "INSERT INTO dbo.vipupd " +
                              "(id, owner, newtc, revmn, wgtdif, wgtvip, vipflag) " +
                              "VALUES ('" + dt.Rows[i]["ID"].ToString() + "','" + dt.Rows[i]["OWNER"].ToString() + "','" + dt.Rows[i]["NEWTC"].ToString() + "','" + rev_mn + "','" + Convert.ToInt32(dt.Rows[i]["WGTDIF"]) + "','" + Convert.ToInt32(dt.Rows[i]["WGTVIP"]) + "','" + dt.Rows[i]["VIPFLAG"].ToString()  + "')";

                using (var dbcm = new SqlCommand(InsertQuery, sql_connection))
                {
                    dbcm.ExecuteNonQuery();
                }
                
            }
            sql_connection.Close();
        }


        //get case table
        public DataTable GetCasesData(string owner, string newtc)
        {
            DataTable dt = new DataTable();

            //set up owner statement
            string owner_str = GetOwnerStr(owner);

            //set up newtc statement
            string newtc_str = getNewtcStringForIT(newtc);

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select id, newtc, status, round(v0,0) as v0, fv0, round(v1, 0) as v1, fv1, round(v2, 0) as v2, fv2, round(v3, 0) as v3, fv3, round(v4, 0) as v4, fv4, round(v5, 0) as v5, fv5, cumvip, (cumvip/(rvitm5c*fwgt))*100 as pct, strtdate, compdate, RVITM5C, ITEM6, fwgt, 0 as isDiff";
                sqlQuery = sqlQuery + " from dbo.VIPPROJ ";
                sqlQuery = sqlQuery + " where " + newtc_str + owner_str+  " and (fv0 <> '' or fv1 <> '' or fv2 <> '' or fv3 <> '' or fv4 <> '' or fv5 <> '')";
                sqlQuery = sqlQuery + " order by id";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //update vip proj table
        public void UpdateVipProj(DataTable dt)
        {
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if ((int)dt.Rows[i]["IsDiff"] > 0)
                {
                    using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                    {
                        string usql = "UPDATE dbo.vipproj SET " +
                                        "V0 = @V0, " +
                                        "V1 = @V1, " +
                                        "V2 = @V2, " +
                                        "V3 = @V3, " +
                                        "V4 = @V4, " +
                                        "Cumvip = @Cumvip " +
                                        "WHERE ID = @ID";

                        SqlCommand update_command = new SqlCommand(usql, sql_connection);
                        update_command.Parameters.AddWithValue("@ID", dt.Rows[i]["ID"].ToString());
                        update_command.Parameters.AddWithValue("@V0", Convert.ToInt32(dt.Rows[i]["V0"]));
                        update_command.Parameters.AddWithValue("@V1", Convert.ToInt32(dt.Rows[i]["V1"]));
                        update_command.Parameters.AddWithValue("@V2", Convert.ToInt32(dt.Rows[i]["V2"]));
                        update_command.Parameters.AddWithValue("@V3", Convert.ToInt32(dt.Rows[i]["V3"]));
                        update_command.Parameters.AddWithValue("@V4", Convert.ToInt32(dt.Rows[i]["V4"]));
                        update_command.Parameters.AddWithValue("@Cumvip", Convert.ToInt32(dt.Rows[i]["Cumvip"]));

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

        //based on survey, get where owner string
        private string GetOwnerStr(string survey, bool withand = true)
        {
            string owner_str = string.Empty;
            if (withand)
            {
                if (survey == "N")
                    owner_str = " and owner = 'N'";
                else if (survey == "F")
                    owner_str = " and owner in ('C', 'D', 'F') ";
                else if (survey == "P")
                    owner_str = " and owner in ('S', 'L', 'P')";
                else if (survey == "M")
                    owner_str = " and owner = 'M'";
                else
                    owner_str = " and owner in ('T', 'E', 'G', 'R', 'O', 'W')";
            }
            else
            {
                if (survey == "N")
                    owner_str = " owner = 'N'";
                else if (survey == "F")
                    owner_str = " owner in ('C', 'D', 'F') ";
                else if (survey == "P")
                    owner_str = " owner in ('S', 'L', 'P')";
                else if (survey == "M")
                    owner_str = " owner = 'M'";
                else
                    owner_str = " owner in ('T', 'E', 'G', 'R', 'O', 'W')";
            
            }

            return owner_str;
        }

        //update vip proj table
        public void SaveVipsadj(string survey, string newtc)
        {
            string tc2;

            newtc = newtc.Trim();
            if (newtc.Length > 2)
                tc2 = newtc.Substring(0, 2);
            else
                tc2 = newtc;

            string owner_str = GetOwnerStr(survey);

            DataTable dt = new DataTable();
            
            
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //Calculate 4 digits newtc
                string sqlQuery = @"SELECT distinct newtc FROM dbo.vipsadj where tc2 = " + GeneralData.AddSqlQuotes(tc2) + owner_str + " and Len(newtc) =4 and (u_cm0 >0 or u_cm1> 0 or u_cm2 >0 or u_cm3 >0 or u_cm4 >0)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            UpdateVipsadjTable(survey, dt);

            //Calculate 3 digits newtc
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                dt = new DataTable();
                string sqlQuery = @"SELECT distinct newtc FROM dbo.vipsadj where tc2 = " + GeneralData.AddSqlQuotes(tc2) + owner_str + " and Len(newtc) =3 and (u_cm0 >0 or u_cm1> 0 or u_cm2 >0 or u_cm3 >0 or u_cm4 >0)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                   
            }
            UpdateVipsadjTable(survey, dt);

            //Calculate 2 digits newtc
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                dt = new DataTable();
                string sqlQuery = @"SELECT distinct newtc FROM dbo.vipsadj where tc2 = " + GeneralData.AddSqlQuotes(tc2) + owner_str + " and Len(newtc) =2 and (u_cm0 >0 or u_cm1> 0 or u_cm2 >0 or u_cm3 >0 or u_cm4 >0)";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            UpdateVipsadjTable(survey, dt);

            if (Convert.ToInt32(tc2) > 19)
            {
                UpdateVipsadj1T(survey, newtc);
            }
                

            //Calculate total of total
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                
                DataTable dtt = new DataTable();

                //Calculate total for a survey
                string sqlquery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                sqlquery = sqlquery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                sqlquery = sqlquery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                sqlquery = sqlquery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                sqlquery = sqlquery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                sqlquery = sqlquery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                sqlquery = sqlquery + " FROM dbo.vipsadj where owner = " + GeneralData.AddSqlQuotes(survey) + " and tclevel <>0 and ddown =1 and tc2 < '20'"; 
                using (SqlCommand cmd = new SqlCommand(sqlquery, sql_connection))
                {
                    sql_connection.Open();
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtt);
                    sql_connection.Close();
                }


                //update dbo.vipsadj for a survey, newtc
                string updateQuery = string.Empty;

                updateQuery = "UPDATE dbo.vipsadj SET ";
                updateQuery = updateQuery + " u_cm0 = " + Convert.ToInt32(dtt.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dtt.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dtt.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dtt.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dtt.Rows[0]["u_cm4"]);
                updateQuery = updateQuery + ", l_cm0 = " + Convert.ToInt32(dtt.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dtt.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dtt.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dtt.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dtt.Rows[0]["l_cm4"]);
                updateQuery = updateQuery + " ,c_cm0 = " + Convert.ToInt32(dtt.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dtt.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dtt.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dtt.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dtt.Rows[0]["c_cm4"]);
                updateQuery = updateQuery + " ,b_cm0 = " + Convert.ToInt32(dtt.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dtt.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dtt.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dtt.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dtt.Rows[0]["b_cm4"]);
                updateQuery = updateQuery + " ,s_cm0 = " + Convert.ToInt32(dtt.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dtt.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dtt.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dtt.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dtt.Rows[0]["s_cm4"]);
                updateQuery = updateQuery + " ,cm0 = " + Convert.ToDecimal(dtt.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dtt.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dtt.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dtt.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dtt.Rows[0]["cm4"]);
                updateQuery = updateQuery + " where tc2 = '' and tclevel =0 and ddown =1 and owner =" + GeneralData.AddSqlQuotes(survey);
                using (var dbcm = new SqlCommand(updateQuery, sql_connection))
                {
                    try
                    {
                        sql_connection.Open();
                        int count = dbcm.ExecuteNonQuery();

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


                //Calculate total for all survey
                dtt = new DataTable();
                sqlquery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                sqlquery = sqlquery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                sqlquery = sqlquery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                sqlquery = sqlquery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                sqlquery = sqlquery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                sqlquery = sqlquery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                sqlquery = sqlquery + " FROM dbo.vipsadj where owner <> 'T' and tclevel =0 and ddown =1";
                using (SqlCommand cmd = new SqlCommand(sqlquery, sql_connection))
                {
                    sql_connection.Open();
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtt);
                    sql_connection.Close();
                }


                //update dbo.vipsadj for T survey
                updateQuery = string.Empty;

                updateQuery = "UPDATE dbo.vipsadj SET ";
                updateQuery = updateQuery + " u_cm0 = " + Convert.ToInt32(dtt.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dtt.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dtt.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dtt.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dtt.Rows[0]["u_cm4"]);
                updateQuery = updateQuery + ", l_cm0 = " + Convert.ToInt32(dtt.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dtt.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dtt.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dtt.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dtt.Rows[0]["l_cm4"]);
                updateQuery = updateQuery + " ,c_cm0 = " + Convert.ToInt32(dtt.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dtt.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dtt.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dtt.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dtt.Rows[0]["c_cm4"]);
                updateQuery = updateQuery + " ,b_cm0 = " + Convert.ToInt32(dtt.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dtt.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dtt.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dtt.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dtt.Rows[0]["b_cm4"]);
                updateQuery = updateQuery + " ,s_cm0 = " + Convert.ToInt32(dtt.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dtt.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dtt.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dtt.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dtt.Rows[0]["s_cm4"]);
                updateQuery = updateQuery + " ,cm0 = " + Convert.ToDecimal(dtt.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dtt.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dtt.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dtt.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dtt.Rows[0]["cm4"]);
                updateQuery = updateQuery + " where owner = 'T' and tc2 = '' and tclevel=0";
                using (var dbcm = new SqlCommand(updateQuery, sql_connection))
                {
                    try
                    {
                        sql_connection.Open();
                        int count = dbcm.ExecuteNonQuery();

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
        
        //update particular newtc data in dt table
        private void UpdateVipsadjTable(string survey, DataTable dt)
        {
            string owner_str = GetOwnerStr(survey);
            
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int u_cm0 = 0;
                    int u_cm1 = 0;
                    int u_cm2 = 0;
                    int u_cm3 = 0;
                    int u_cm4 = 0;
                    string ntc = dt.Rows[i][0].ToString().Trim();
                    string sqlQuery = @"SELECT sum(round(v0,0)) as sv0, sum(round(v1,0)) as sv1, sum(round(v2,0)) as sv2, sum(round(v3,0)) as sv3, sum(round(v4,0)) as sv4 FROM dbo.vipproj where newtc like replace('" + ntc + "%', ' ', '')" + owner_str;
                    using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                    {
                        sql_connection.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            if (reader["sv0"] != System.DBNull.Value)
                                u_cm0 = Convert.ToInt32(reader["sv0"]);
                            if (reader["sv1"] != System.DBNull.Value)
                                u_cm1 = Convert.ToInt32(reader["sv1"]);
                            if (reader["sv2"] != System.DBNull.Value)
                                u_cm2 = Convert.ToInt32(reader["sv2"]);
                            if (reader["sv3"] != System.DBNull.Value)
                                u_cm3 = Convert.ToInt32(reader["sv3"]);
                            if (reader["sv4"] != System.DBNull.Value)
                                u_cm4 = Convert.ToInt32(reader["sv4"]);
                        }
                        sql_connection.Close();
                    }

                    using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                    {
                        //update dbo.vipsadj for a survey, newtc
                        String updateQuery = string.Empty;

                        updateQuery = "UPDATE dbo.vipsadj SET ";
                        updateQuery = updateQuery + " u_cm0 = " + u_cm0 + ", u_cm1 = " + u_cm1 + ", u_cm2 = " + u_cm2 + ", u_cm3 = " + u_cm3 + ", u_cm4 = " + u_cm4;
                        updateQuery = updateQuery + ", l_cm0 = round(" + u_cm0 + "*lsf0, 0), l_cm1 = round(" + u_cm1 + "*lsf1, 0), l_cm2 = round(" + u_cm2 + "*lsf2, 0), l_cm3 = round(" + u_cm3 + "*lsf3, 0), l_cm4 = round(" + u_cm4 + "*lsf4, 0)";
                        updateQuery = updateQuery + " ,c_cm0 = round(" + u_cm0 + "*lsf0*ucf , 0" + "), c_cm1 = round(" + u_cm1 + "*lsf1*ucf , 0" + "), c_cm2 = round(" + u_cm2 + "*lsf2*ucf , 0" + "), c_cm3 = round(" + u_cm3 + "*lsf3*ucf , 0" + "), c_cm4 = round(" + u_cm4 + "*lsf4*ucf , 0" + ")";
                        updateQuery = updateQuery + " ,b_cm0 = round(" + u_cm0 + "*lsf0*ucf*bst0, 0), b_cm1 = round(" + u_cm1 + "*lsf1*ucf*bst1 , 0), b_cm2 = round(" + u_cm2 + "*lsf2*ucf*bst2 , 0), b_cm3 = round(" + u_cm3 + "*lsf3*ucf*bst3 , 0), b_cm4 = round(" + u_cm4 + "*lsf4*ucf*bst4 , 0)";
                        updateQuery = updateQuery + " ,s_cm0 = round(" + u_cm0 + "*lsf0*ucf*bst0*12/saf0 , 0), s_cm1 = round(" + u_cm1 + "*lsf1*ucf*bst1*12/saf1 , 0), s_cm2 = round(" + u_cm2 + "*lsf2*ucf*bst2*12/saf2 , 0), s_cm3 = round(" + u_cm3 + "*lsf3*ucf*bst3*12/saf3 , 0), s_cm4 = round(" + u_cm4 + "*lsf4*ucf*bst4*12/saf4 , 0)";
                        updateQuery = updateQuery + " ,cm0 = round((" + u_cm0 + "*lsf0*ucf*bst0*12/saf0)/1000000, 2), cm1 = round((" + u_cm1 + "*lsf1*ucf*bst1*12/saf1)/1000000, 2), cm2 = round((" + u_cm2 + "*lsf2*ucf*bst2*12/saf2)/1000000, 2), cm3 = round((" + u_cm3 + "*lsf3*ucf*bst3*12/saf3)/1000000, 2), cm4 = round((" + u_cm4 + "*lsf4*ucf*bst4*12/saf4)/1000000, 2)";
                        updateQuery = updateQuery + " where newtc = " + GeneralData.AddSqlQuotes(ntc) + " and owner = " +  GeneralData.AddSqlQuotes(survey);
                        using (var dbcm = new SqlCommand(updateQuery, sql_connection))
                        {
                            try
                            {
                                sql_connection.Open();
                                int count = dbcm.ExecuteNonQuery();

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

                    DataTable dtt = new DataTable();

                    //update total for all surveys
                    string sqlquery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                    sqlquery = sqlquery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                    sqlquery = sqlquery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                    sqlquery = sqlquery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                    sqlquery = sqlquery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                    sqlquery = sqlquery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                    sqlquery = sqlquery + " FROM dbo.vipsadj where owner <> 'T'  and newtc = " + GeneralData.AddSqlQuotes(ntc);
                    using (SqlCommand cmd = new SqlCommand(sqlquery, sql_connection))
                    {
                        sql_connection.Open();
                        SqlDataAdapter ds = new SqlDataAdapter(cmd);
                        ds.Fill(dtt);
                        sql_connection.Close();
                    }

                    string uQuery = string.Empty;

                    uQuery = "UPDATE dbo.vipsadj SET ";
                    uQuery = uQuery + " u_cm0 = " + Convert.ToInt32(dtt.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dtt.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dtt.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dtt.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dtt.Rows[0]["u_cm4"]);
                    uQuery = uQuery + ", l_cm0 = " + Convert.ToInt32(dtt.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dtt.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dtt.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dtt.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dtt.Rows[0]["l_cm4"]);
                    uQuery = uQuery + " ,c_cm0 = " + Convert.ToInt32(dtt.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dtt.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dtt.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dtt.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dtt.Rows[0]["c_cm4"]);
                    uQuery = uQuery + " ,b_cm0 = " + Convert.ToInt32(dtt.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dtt.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dtt.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dtt.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dtt.Rows[0]["b_cm4"]);
                    uQuery = uQuery + " ,s_cm0 = " + Convert.ToInt32(dtt.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dtt.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dtt.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dtt.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dtt.Rows[0]["s_cm4"]);
                    uQuery = uQuery + " ,cm0 = " + Convert.ToDecimal(dtt.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dtt.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dtt.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dtt.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dtt.Rows[0]["cm4"]);
                    uQuery = uQuery + " where owner = 'T' and newtc = " + GeneralData.AddSqlQuotes(ntc);
                    using (var dbcm = new SqlCommand(uQuery, sql_connection))
                    {
                        try
                        {
                            sql_connection.Open();
                            int count = dbcm.ExecuteNonQuery();

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

        //update particular newtc data in dt table
        private void UpdateVipsadj1T(string survey, string newtc)
        {
            
            List<string> nlist = new List<string>();
            nlist.Add("1T0");
            nlist.Add("1T1");
            nlist.Add("1T2");
            nlist.Add("1T3");
            
            
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                for (int i = 0; i < nlist.Count; i++)
                {
                    string newtc_str1 = string.Empty;
                    string ntc = nlist[i];
                    if (nlist[i] == "1T0")
                        newtc_str1 = " newtc in ('200', '210', '220', '230', '240', '250', '260', '270', '280', '290', '300', '310', '320', '330', '340', '350', '360', '370', '380', '390')";
                    else if (nlist[i] == "1T1")
                        newtc_str1 = " newtc in ('201', '211', '221', '231', '241', '251', '261', '271', '281', '291', '301', '311', '321', '331', '341', '351', '361', '371', '381', '391')";
                    else if (nlist[i] == "1T2")
                        newtc_str1 = " newtc in ('202', '212', '222', '232', '242', '252', '262', '272', '282', '292', '302', '312', '322', '332', '342', '352', '362', '372', '382', '392')";
                    else if (nlist[i] == "1T3")
                        newtc_str1 = " newtc in ('203', '213', '223', '233', '243', '253', '263', '273', '283', '293', '303', '313', '323', '333', '343', '353', '363', '373', '383', '393')";

                    DataTable dt1 = new DataTable();

                    //update 1T0 or 1T1 or 1T2 or 1T3 for the survey
                    string sqlquery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                    sqlquery = sqlquery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                    sqlquery = sqlquery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                    sqlquery = sqlquery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                    sqlquery = sqlquery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                    sqlquery = sqlquery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                    sqlquery = sqlquery + " FROM dbo.vipsadj where owner = " + GeneralData.AddSqlQuotes(survey) + "  and " + newtc_str1;
                    using (SqlCommand cmd = new SqlCommand(sqlquery, sql_connection))
                    {
                        sql_connection.Open();
                        SqlDataAdapter ds = new SqlDataAdapter(cmd);
                        ds.Fill(dt1);
                        sql_connection.Close();
                    }

                    string uQuery = string.Empty;

                    uQuery = "UPDATE dbo.vipsadj SET ";
                    uQuery = uQuery + " u_cm0 = " + Convert.ToInt32(dt1.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dt1.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dt1.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dt1.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dt1.Rows[0]["u_cm4"]);
                    uQuery = uQuery + ", l_cm0 = " + Convert.ToInt32(dt1.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dt1.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dt1.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dt1.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dt1.Rows[0]["l_cm4"]);
                    uQuery = uQuery + " ,c_cm0 = " + Convert.ToInt32(dt1.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dt1.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dt1.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dt1.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dt1.Rows[0]["c_cm4"]);
                    uQuery = uQuery + " ,b_cm0 = " + Convert.ToInt32(dt1.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dt1.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dt1.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dt1.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dt1.Rows[0]["b_cm4"]);
                    uQuery = uQuery + " ,s_cm0 = " + Convert.ToInt32(dt1.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dt1.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dt1.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dt1.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dt1.Rows[0]["s_cm4"]);
                    uQuery = uQuery + " ,cm0 = " + Convert.ToDecimal(dt1.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dt1.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dt1.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dt1.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dt1.Rows[0]["cm4"]);
                    uQuery = uQuery + " where owner = " + GeneralData.AddSqlQuotes(survey) + " and newtc = " + GeneralData.AddSqlQuotes(ntc);
                    using (var dbcm = new SqlCommand(uQuery, sql_connection))
                    {
                        try
                        {
                            sql_connection.Open();
                            int count = dbcm.ExecuteNonQuery();

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

                    //update total for all surveys
                    DataTable dtt = new DataTable();

                    //update total for all surveys
                    sqlquery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                    sqlquery = sqlquery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                    sqlquery = sqlquery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                    sqlquery = sqlquery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                    sqlquery = sqlquery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                    sqlquery = sqlquery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                    sqlquery = sqlquery + " FROM dbo.vipsadj where owner <> 'T'  and newtc = " + GeneralData.AddSqlQuotes(ntc);
                    using (SqlCommand cmd = new SqlCommand(sqlquery, sql_connection))
                    {
                        sql_connection.Open();
                        SqlDataAdapter ds = new SqlDataAdapter(cmd);
                        ds.Fill(dtt);
                        sql_connection.Close();
                    }

                    uQuery = string.Empty;

                    uQuery = "UPDATE dbo.vipsadj SET ";
                    uQuery = uQuery + " u_cm0 = " + Convert.ToInt32(dtt.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dtt.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dtt.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dtt.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dtt.Rows[0]["u_cm4"]);
                    uQuery = uQuery + ", l_cm0 = " + Convert.ToInt32(dtt.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dtt.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dtt.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dtt.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dtt.Rows[0]["l_cm4"]);
                    uQuery = uQuery + " ,c_cm0 = " + Convert.ToInt32(dtt.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dtt.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dtt.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dtt.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dtt.Rows[0]["c_cm4"]);
                    uQuery = uQuery + " ,b_cm0 = " + Convert.ToInt32(dtt.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dtt.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dtt.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dtt.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dtt.Rows[0]["b_cm4"]);
                    uQuery = uQuery + " ,s_cm0 = " + Convert.ToInt32(dtt.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dtt.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dtt.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dtt.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dtt.Rows[0]["s_cm4"]);
                    uQuery = uQuery + " ,cm0 = " + Convert.ToDecimal(dtt.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dtt.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dtt.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dtt.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dtt.Rows[0]["cm4"]);
                    uQuery = uQuery + " where owner = 'T' and newtc = " + GeneralData.AddSqlQuotes(ntc);
                    using (var dbcm = new SqlCommand(uQuery, sql_connection))
                    {
                        try
                        {
                            sql_connection.Open();
                            int count = dbcm.ExecuteNonQuery();

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

                //update 1T
                DataTable dt9 = new DataTable();

                //update total for all surveys
                string squery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                squery = squery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                squery = squery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                squery = squery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                squery = squery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                squery = squery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                squery = squery + " FROM dbo.vipsadj where owner = " + GeneralData.AddSqlQuotes(survey) + "  and newtc in ('1T0', '1T1', '1T2', '1T3')";
                using (SqlCommand cmd = new SqlCommand(squery, sql_connection))
                {
                    sql_connection.Open();
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt9);
                    sql_connection.Close();
                }


                string Query = "UPDATE dbo.vipsadj SET ";
                Query = Query + " u_cm0 = " + Convert.ToInt32(dt9.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dt9.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dt9.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dt9.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dt9.Rows[0]["u_cm4"]);
                Query = Query + ", l_cm0 = " + Convert.ToInt32(dt9.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dt9.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dt9.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dt9.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dt9.Rows[0]["l_cm4"]);
                Query = Query + " ,c_cm0 = " + Convert.ToInt32(dt9.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dt9.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dt9.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dt9.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dt9.Rows[0]["c_cm4"]);
                Query = Query + " ,b_cm0 = " + Convert.ToInt32(dt9.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dt9.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dt9.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dt9.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dt9.Rows[0]["b_cm4"]);
                Query = Query + " ,s_cm0 = " + Convert.ToInt32(dt9.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dt9.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dt9.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dt9.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dt9.Rows[0]["s_cm4"]);
                Query = Query + " ,cm0 = " + Convert.ToDecimal(dt9.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dt9.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dt9.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dt9.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dt9.Rows[0]["cm4"]);
                Query = Query + " where owner = " + GeneralData.AddSqlQuotes(survey) + " and newtc = '1T'";
                using (var dbcm = new SqlCommand(Query, sql_connection))
                {
                    try
                    {
                        sql_connection.Open();
                        int count = dbcm.ExecuteNonQuery();

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
                
                DataTable dtt9 = new DataTable();

                //update total for all surveys
                squery = @"SELECT sum(u_cm0) as u_cm0, sum(u_cm1) as u_cm1, sum(u_cm2) as u_cm2, sum(u_cm3) as u_cm3, sum(u_cm4) as u_cm4, ";
                squery = squery + " sum(l_cm0) as l_cm0, sum(l_cm1) as l_cm1, sum(l_cm2) as l_cm2, sum(l_cm3) as l_cm3, sum(l_cm4) as l_cm4,";
                squery = squery + " sum(c_cm0) as c_cm0, sum(c_cm1) as c_cm1, sum(c_cm2) as c_cm2, sum(c_cm3) as c_cm3, sum(c_cm4) as c_cm4,";
                squery = squery + " sum(b_cm0) as b_cm0, sum(b_cm1) as b_cm1, sum(b_cm2) as b_cm2, sum(b_cm3) as b_cm3, sum(b_cm4) as b_cm4,";
                squery = squery + " sum(s_cm0) as s_cm0, sum(s_cm1) as s_cm1, sum(s_cm2) as s_cm2, sum(s_cm3) as s_cm3, sum(s_cm4) as s_cm4,";
                squery = squery + " sum(cm0) as cm0, sum(cm1) as cm1, sum(cm2) as cm2, sum(cm3) as cm3, sum(cm4) as cm4 ";
                squery = squery + " FROM dbo.vipsadj where owner <> 'T'  and newtc = '1T'" ;
                using (SqlCommand cmd = new SqlCommand(squery, sql_connection))
                {
                    sql_connection.Open();
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtt9);
                    sql_connection.Close();
                }

             
                Query = "UPDATE dbo.vipsadj SET ";
                Query = Query + " u_cm0 = " + Convert.ToInt32(dtt9.Rows[0]["u_cm0"]) + ", u_cm1 = " + Convert.ToInt32(dtt9.Rows[0]["u_cm1"]) + ", u_cm2 = " + Convert.ToInt32(dtt9.Rows[0]["u_cm2"]) + ", u_cm3 = " + Convert.ToInt32(dtt9.Rows[0]["u_cm3"]) + ", u_cm4 = " + Convert.ToInt32(dtt9.Rows[0]["u_cm4"]);
                Query = Query + ", l_cm0 = " + Convert.ToInt32(dtt9.Rows[0]["l_cm0"]) + ", l_cm1 = " + Convert.ToInt32(dtt9.Rows[0]["l_cm1"]) + ", l_cm2 = " + Convert.ToInt32(dtt9.Rows[0]["l_cm2"]) + ", l_cm3 = " + Convert.ToInt32(dtt9.Rows[0]["l_cm3"]) + ", l_cm4 = " + Convert.ToInt32(dtt9.Rows[0]["l_cm4"]);
                Query = Query + " ,c_cm0 = " + Convert.ToInt32(dtt9.Rows[0]["c_cm0"]) + ", c_cm1 = " + Convert.ToInt32(dtt9.Rows[0]["c_cm1"]) + ", c_cm2 = " + Convert.ToInt32(dtt9.Rows[0]["c_cm2"]) + ", c_cm3 = " + Convert.ToInt32(dtt9.Rows[0]["c_cm3"]) + ", c_cm4 = " + Convert.ToInt32(dtt9.Rows[0]["c_cm4"]);
                Query = Query + " ,b_cm0 = " + Convert.ToInt32(dtt9.Rows[0]["b_cm0"]) + ", b_cm1 = " + Convert.ToInt32(dtt9.Rows[0]["b_cm1"]) + ", b_cm2 = " + Convert.ToInt32(dtt9.Rows[0]["b_cm2"]) + ", b_cm3 = " + Convert.ToInt32(dtt9.Rows[0]["b_cm3"]) + ", b_cm4 = " + Convert.ToInt32(dtt9.Rows[0]["b_cm4"]);
                Query = Query + " ,s_cm0 = " + Convert.ToInt32(dtt9.Rows[0]["s_cm0"]) + ", s_cm1 = " + Convert.ToInt32(dtt9.Rows[0]["s_cm1"]) + ", s_cm2 = " + Convert.ToInt32(dtt9.Rows[0]["s_cm2"]) + ", s_cm3 = " + Convert.ToInt32(dtt9.Rows[0]["s_cm3"]) + ", s_cm4 = " + Convert.ToInt32(dtt9.Rows[0]["s_cm4"]);
                Query = Query + " ,cm0 = " + Convert.ToDecimal(dtt9.Rows[0]["cm0"]) + ", cm1 = " + Convert.ToDecimal(dtt9.Rows[0]["cm1"]) + " ,cm2 = " + Convert.ToDecimal(dtt9.Rows[0]["cm2"]) + ", cm3 = " + Convert.ToDecimal(dtt9.Rows[0]["cm3"]) + ", cm4 = " + Convert.ToDecimal(dtt9.Rows[0]["cm4"]);
                Query = Query + " where owner = 'T' and newtc = '1T'" ;
                using (var dbcm = new SqlCommand(Query, sql_connection))
                {
                    try
                    {
                        sql_connection.Open();
                        int count = dbcm.ExecuteNonQuery();

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
}
