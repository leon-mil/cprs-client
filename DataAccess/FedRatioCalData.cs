/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.FedRatioCalData.cs	    	
Programmer:         Christine Zhang
Creation Date:      03/06/2017
Inputs:             
Parameters:	        statp 
Outputs:	        FedRatioCal data	
Description:	    data layer to calculation Federal Ratio, tabulation of Federal Ratio
Detailed Design:    Federal Ratio Adjust Detail Design
Other:	            Called by: frmTotalVip.cs
 
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
using System.Text.RegularExpressions;

namespace CprsDAL
{
    public class FedRatioCalData
    {
        //Get Original BST and B_CM value
        public DataTable GetOriginalBValue()
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select newtc, b_cm0 as b_cm0_o, b_cm1 as b_cm1_o,b_cm2 as b_cm2_o, b_cm3 as b_cm3_o,b_cm4 as b_cm4_o, bst0 as bst0_o, bst1 as bst1_o, bst2 as bst2_o, bst3 as bst3_o, bst4 as bst4_o";
                sqlQuery = sqlQuery + " from dbo.VIPSADJ ";
                sqlQuery = sqlQuery + " where owner = 'F' and newtc in ('','00', '01','02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '1T')";
                sqlQuery = sqlQuery + " order by newtc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //pass currnt month, return print out table
        public DataTable UpdateFedRatioCalData(string statp)
        {
            decimal VPr0 =0;
            decimal VPr1 =0;
            decimal VPr2 =0;
            decimal VPrp =0;
            decimal VPr3 =0;
            decimal VPr4 =0;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //get project value for c_cm0, c_cm1, c_cm2 and c_pr0 value 
                string sqlQuery = @"SELECT c_cm0, c_cm1, c_cm2, c_pr0, c_cm3, c_cm4 FROM dbo.vipsadj where owner = 'F' and tclevel = 0";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    sql_connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        VPr0 = Math.Round(Convert.ToDecimal(reader["C_CM0"])/1000, 1);
                        VPr1 = Math.Round(Convert.ToDecimal(reader["C_CM1"]) / 1000, 1);
                        VPr2 = Math.Round(Convert.ToDecimal(reader["C_CM2"]) / 1000, 1);
                        VPrp = Math.Round(Convert.ToDecimal(reader["C_PR0"]) / 1000, 1); 
                        VPr3 = Math.Round(Convert.ToDecimal(reader["C_CM3"]) / 1000, 1);
                        VPr4 = Math.Round(Convert.ToDecimal(reader["C_CM4"]) / 1000, 1);
                    } 
                }
            }

             var dt = DateTime.ParseExact(statp,"yyyyMM",CultureInfo.InvariantCulture);

            //get month name
            string p1mon = (dt.AddMonths(-1)).ToString("yyyyMM") ;
            string p2mon = (dt.AddMonths(-2)).ToString("yyyyMM");
            string ppmon = (dt.AddMonths(+1)).ToString("yyyyMM");
            string p3mon = (dt.AddMonths(-3)).ToString("yyyyMM");
            string p4mon = (dt.AddMonths(-4)).ToString("yyyyMM");

            //Get Agency data for VPr0, VPr1, VPr2, VPr3, VPr4, VPrp
            decimal VAg0 =GetVPrValue(statp);
            decimal VAg1 = GetVPrValue(p1mon);
            decimal VAg2 = GetVPrValue(p2mon);
            decimal VAgp = GetVPrValue(ppmon);
            decimal VAg3 = GetVPrValue(p3mon);
            decimal VAg4 = GetVPrValue(p4mon);

            //Get ratio value
            decimal rf0 = Math.Round(VAg0 / VPr0, 3);
            decimal rf1 = Math.Round(VAg1 / VPr1, 3);
            decimal rf2 = Math.Round(VAg2 / VPr2, 3);
            decimal rfp = Math.Round(VAgp / VPr0, 3);
            decimal rf3 = Math.Round(VAg3 / VPr3, 3);
            decimal rf4 = Math.Round(VAg4 / VPr4, 3);

            //update Bsttab, Bstann and Vipsadj
            SaveBstData(statp, rf0, "Bst0");
            SaveBstData(p1mon, rf1, "Bst1");
            SaveBstData(p2mon, rf2, "Bst2");
            SaveBstData(ppmon, rfp, "Bstp");

            if (statp.Substring(5, 1) == "5")
            {
                SaveBstData(p3mon, rf3, "Bst3");
                SaveBstData(p4mon, rf4, "Bst4");
            }

            //create a print out table
            DataTable table = new DataTable();
            table.Columns.Add("sdate", typeof(string));
            table.Columns.Add("VipA", typeof(decimal));
            table.Columns.Add("VipP", typeof(decimal));
            table.Columns.Add("VipF", typeof(decimal));

            if (statp.Substring(5, 1) == "5")
            {
                table.Rows.Add(p4mon, VAg4, VPr4, rf4);
                table.Rows.Add(p3mon, VAg3, VPr3, rf3); 
            }
            table.Rows.Add(p2mon, VAg2, VPr2, rf2);
            table.Rows.Add(p1mon, VAg1, VPr1, rf1);
            table.Rows.Add(statp, VAg0, VPr0, rf0);
            table.Rows.Add(ppmon, VAgp, VPr0, rfp);
            
            int rowcount = table.Rows.Count;
           
            return table;
        }

         //Get Uvipdata value
        private decimal GetVPrValue(string statp)
        {
            decimal uval = 0;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Uvipdata FROM dbo.viphist where toc = '109' and Date6 = " + GeneralData.AddSqlQuotes(statp);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    uval = Convert.ToDecimal(reader["Uvipdata"]);
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

            return uval;

        }

        /*Update Bsttab, Bstann and Vipsadj Data */
        private void SaveBstData(string statp, decimal rf, string bf)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                try
                {
                    string usql = "UPDATE dbo.Bsttab SET " +
                                    "BST = " + rf +
                                    "WHERE owner = 'F' and sdate = " + GeneralData.AddSqlQuotes(statp);

                    SqlCommand update_command = new SqlCommand(usql, sql_connection);


                    int count = update_command.ExecuteNonQuery();

                    usql = "UPDATE dbo.Bstann SET " +
                                    "BST = " + rf +
                                    "WHERE owner = 'F' and sdate = " + GeneralData.AddSqlQuotes(statp);

                    update_command = new SqlCommand(usql, sql_connection);
                    count = update_command.ExecuteNonQuery();

                    if (bf != "Bstp")
                    {
                        usql = "UPDATE dbo.Vipsadj SET " + bf + "=" + rf +
                                        "WHERE owner = 'F' and Tclevel >0";

                        update_command = new SqlCommand(usql, sql_connection);
                        count = update_command.ExecuteNonQuery();
                    }
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

        /*Update Vipsadj Data */
        public void UpdateVipData()
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                //update all 4 digit, 3 digit and 2 digits
                string usql = "UPDATE dbo.Vipsadj SET ";
                usql = usql + "B_CM0 = round(C_CM0*BST0, 0), B_CM1 = round(C_CM1*BST1, 0), B_CM2 = round(C_CM2*BST2, 0), B_CM3 = round(C_CM3*BST3, 0), B_CM4 = round(C_CM4*BST4, 0), ";
                usql = usql + "S_CM0 = round(C_CM0*BST0*12/SAF0, 0), S_CM1 = round(C_CM1*BST1*12/SAF1, 0), S_CM2 = round(C_CM2*BST2*12/SAF2, 0), S_CM3 = round(C_CM3*BST3*12/SAF3, 0), S_CM4 = round(C_CM4*BST4*12/SAF4, 0),";
                usql = usql + "CM0 = round((C_CM0*BST0*12/SAF0)/1000000, 1), CM1 = round((C_CM1*BST1*12/SAF1)/1000000, 1),CM2 = round((C_CM2*BST2*12/SAF2)/1000000, 1),CM3 = round((C_CM3*BST3*12/SAF3)/1000000, 1),CM4 = round((C_CM4*BST4*12/SAF4)/1000000, 1)";
                usql = usql + "WHERE owner = 'F' and Tclevel >0";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                int count = update_command.ExecuteNonQuery();
                sql_connection.Close();
                
                //get owner F total
                DataTable dtt = new DataTable();

                string sqlQuery = @"SELECT sum(B_CM0) as b_cm0, sum(B_CM1) as b_cm1, sum(B_CM2) as b_cm2, sum(B_CM3) as b_cm3, sum(B_CM4) as b_cm4, ";
                sqlQuery = sqlQuery + " sum(S_CM0) as s_cm0, sum(S_CM1) as s_cm1, sum(S_CM2) as s_cm2, sum(S_CM3) as s_cm3, sum(S_CM4) as s_cm4,";
                sqlQuery = sqlQuery + " round(sum(S_CM0)/1000000, 1) as cm0, round(sum(S_CM1)/1000000,1) as cm1, round(sum(S_CM2)/1000000,1) as cm2, round(sum(S_CM3)/1000000,1) as cm3, round(sum(S_CM4)/1000000,1) as cm4 ";
                sqlQuery = sqlQuery + " FROM dbo.vipsadj where owner = 'F' and ddown = 1 and Tclevel >0 and (newtc <'20' or newtc ='1T')";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    sql_connection.Open();
                    
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dtt);
                    sql_connection.Close();
                }
                
                //update owner F total
                usql = "UPDATE dbo.Vipsadj SET ";
                usql = usql + "B_CM0 = " + (int)dtt.Rows[0]["b_cm0"] + ", B_CM1 = " + (int)dtt.Rows[0]["b_cm1"] + ", B_CM2 = " + (int)dtt.Rows[0]["b_cm2"]+", B_CM3 = " + (int)dtt.Rows[0]["b_cm3"]+ ", B_CM4 = " + (int)dtt.Rows[0]["b_cm4"] + ",";
                usql = usql + "S_CM0 = " + (int)dtt.Rows[0]["s_cm0"] + ", S_CM1 = " + (int)dtt.Rows[0]["s_cm1"] + ", S_CM2 = " + (int)dtt.Rows[0]["s_cm2"]+", S_CM3 = " + (int)dtt.Rows[0]["s_cm3"]+ ", S_CM4 = " + (int)dtt.Rows[0]["s_cm4"] + ",";
                usql = usql + "CM0 = " + Convert.ToDecimal(dtt.Rows[0]["cm0"]) + ", CM1 = " + Convert.ToDecimal(dtt.Rows[0]["cm1"]) + ", CM2 = " + Convert.ToDecimal(dtt.Rows[0]["cm2"]) + ", CM3 = " + Convert.ToDecimal(dtt.Rows[0]["cm3"]) + ", CM4 = " + Convert.ToDecimal(dtt.Rows[0]["cm4"]) ;
                usql = usql + " WHERE owner = 'F' and Tclevel =0";
                using (SqlCommand cmd = new SqlCommand(usql, sql_connection))
                {
                    sql_connection.Open();
                    count = cmd.ExecuteNonQuery();
                    sql_connection.Close();
                }

                 //get owner T total
                DataTable dt = new DataTable();

                sqlQuery = @"SELECT distinct newtc, sum(B_CM0) as b_cm0, sum(B_CM1) as b_cm1, sum(B_CM2) as b_cm2, sum(B_CM3) as b_cm3, sum(B_CM4) as b_cm4, ";
                sqlQuery = sqlQuery + " sum(S_CM0) as s_cm0, sum(S_CM1) as s_cm1, sum(S_CM2) as s_cm2, sum(S_CM3) as s_cm3, sum(S_CM4) as s_cm4,";
                sqlQuery = sqlQuery + " round(sum(S_CM0)/1000000, 1) as cm0, round(sum(S_CM1)/1000000,1) as cm1, round(sum(S_CM2)/1000000,1) as cm2, round(sum(S_CM3)/1000000,1) as cm3, round(sum(S_CM4)/1000000,1) as cm4 ";
                sqlQuery = sqlQuery + " FROM dbo.vipsadj where owner <> 'T' group by newtc order by newtc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    sql_connection.Open();

                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                    sql_connection.Close();
                }

                //update owner T total
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    usql = "UPDATE dbo.Vipsadj SET ";
                    usql = usql + "B_CM0 = " + (int)dt.Rows[i]["b_cm0"] + ", B_CM1 = " + (int)dt.Rows[i]["b_cm1"] + ", B_CM2 = " + (int)dt.Rows[i]["b_cm2"] + ", B_CM3 = " + (int)dt.Rows[i]["b_cm3"] + ", B_CM4 = " + (int)dt.Rows[i]["b_cm4"] + ",";
                    usql = usql + "S_CM0 = " + (int)dt.Rows[i]["s_cm0"] + ", S_CM1 = " + (int)dt.Rows[i]["s_cm1"] + ", S_CM2 = " + (int)dt.Rows[i]["s_cm2"] + ", S_CM3 = " + (int)dt.Rows[i]["s_cm3"] + ", S_CM4 = " + (int)dt.Rows[i]["s_cm4"] + ",";
                    usql = usql + "CM0 = " + Convert.ToDecimal(dt.Rows[i]["cm0"]) + ", CM1 = " + Convert.ToDecimal(dt.Rows[i]["cm1"]) + ", CM2 = " + Convert.ToDecimal(dt.Rows[i]["cm2"]) + ", CM3 = " + Convert.ToDecimal(dt.Rows[i]["cm3"]) + ", CM4 = " + Convert.ToDecimal(dt.Rows[i]["cm4"]);
                    usql = usql + "WHERE owner = 'T' and NEWTC = " + dt.Rows[i]["NEWTC"].ToString();

                    try
                    {
                        sql_connection.Open();
                        count = update_command.ExecuteNonQuery();

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

        //get current month print
        public DataTable GetFedRatioCurrMonPrint(DataTable bt)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select title = p.NEWTC+ ' ' + TCDESCRIPTION, u_cm0, L_cm0, c_cm0, b_cm0, Lsf0, bst0";
                sqlQuery = sqlQuery + " from dbo.VIPSADJ v, dbo.PUBTCLIST p ";
                sqlQuery = sqlQuery + " where v.NEWTC =p.newtc  and owner = 'F' and v.newtc in ('', '00', '01','02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '1T')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            //add original value
            dt.Columns.Add("B_CM0_o", typeof(int)).SetOrdinal(dt.Columns.IndexOf("B_CM0"));
            dt.Columns.Add("BST0_o").SetOrdinal(dt.Columns.IndexOf("BST0"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["B_CM0_o"] = bt.Rows[i]["B_CM0_o"];
                dt.Rows[i]["BST0_o"] = bt.Rows[i]["BST0_o"];
            }

            return dt;
        }

        //get prior month print
        public DataTable GetFedRatioPriorMonPrint(DataTable bt)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select title = p.NEWTC+ ' ' + TCDESCRIPTION, u_cm1, L_cm1, c_cm1, b_cm1, Lsf1, bst1";
                sqlQuery = sqlQuery + " from dbo.VIPSADJ v, dbo.PUBTCLIST p";
                sqlQuery = sqlQuery + " where v.NEWTC =p.newtc and owner = 'F' and v.newtc in ('','00', '01','02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '1T')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            //add original value
            dt.Columns.Add("B_CM1_o", typeof(int)).SetOrdinal(dt.Columns.IndexOf("B_CM1"));
            dt.Columns.Add("BST1_o").SetOrdinal(dt.Columns.IndexOf("BST1"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["B_CM1_o"] = bt.Rows[i]["B_CM1_o"];
                dt.Rows[i]["BST1_o"] = bt.Rows[i]["BST1_o"];
            }

            return dt;
        }

        //get two prior month print
        public DataTable GetFedRatio2PriorMonPrint(DataTable bt)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select title = p.NEWTC+ ' ' + TCDESCRIPTION, u_cm2, L_cm2, c_cm2, b_cm2, Lsf2, bst2";
                sqlQuery = sqlQuery + " from dbo.VIPSADJ v, dbo.PUBTCLIST p";
                sqlQuery = sqlQuery + " where v.NEWTC =p.newtc  and owner = 'F' and v.newtc in ('','00', '01','02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '1T')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            //add original value
            dt.Columns.Add("B_CM2_o", typeof(int)).SetOrdinal(dt.Columns.IndexOf("B_CM2"));
            dt.Columns.Add("BST2_o").SetOrdinal(dt.Columns.IndexOf("BST2"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["B_CM2_o"] = bt.Rows[i]["B_CM2_o"];
                dt.Rows[i]["BST2_o"] = bt.Rows[i]["BST2_o"];
            }

            return dt;
        }

        //get three prior month print
        public DataTable GetFedRatio3PriorMonPrint(DataTable bt)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select title = p.NEWTC+ ' ' + TCDESCRIPTION, u_cm3, L_cm3, c_cm3, b_cm3, Lsf3, bst3";
                sqlQuery = sqlQuery + " from dbo.VIPSADJ v, dbo.PUBTCLIST p";
                sqlQuery = sqlQuery + " where v.NEWTC =p.newtc  and owner = 'F' and v.newtc in ('','00', '01','02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '1T')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            //add original value
            dt.Columns.Add("B_CM3_o", typeof(int)).SetOrdinal(dt.Columns.IndexOf("B_CM3"));
            dt.Columns.Add("BST3_o").SetOrdinal(dt.Columns.IndexOf("BST3"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["B_CM3_o"] = bt.Rows[i]["B_CM3_o"];
                dt.Rows[i]["BST3_o"] = bt.Rows[i]["BST3_o"];
            }

            return dt;
        }

        //get four prior month print
        public DataTable GetFedRatio4PriorMonPrint(DataTable bt)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select title = p.NEWTC+ ' ' + TCDESCRIPTION, u_cm4, L_cm4, c_cm4, b_cm4, Lsf4, bst4";
                sqlQuery = sqlQuery + " from dbo.VIPSADJ v, dbo.PUBTCLIST p";
                sqlQuery = sqlQuery + " where v.NEWTC =p.newtc  and owner = 'F' and v.newtc in ('','00', '01','02', '03', '04', '05', '06', '07', '08', '09', '10', '11', '12', '13', '14', '15', '1T')";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            //add original value
            dt.Columns.Add("B_CM4_o", typeof(int)).SetOrdinal(dt.Columns.IndexOf("B_CM4"));
            dt.Columns.Add("BST4_o").SetOrdinal(dt.Columns.IndexOf("BST4"));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["B_CM4_o"] = bt.Rows[i]["B_CM4_o"];
                dt.Rows[i]["BST4_o"] = bt.Rows[i]["BST4_o"];
            }

            return dt;
        }
    }
}
