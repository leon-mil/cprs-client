/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : CPRSDAL.SpecGeoData.cs
 Programmer    : Christine Zhang
 Creation Date : 11/15/2018
 Inputs        : N/A
 Paramaters    : Year, Type (1- Region, 2-Division, 3-State, 4-division excel)
 Output        : N/A               
 Description   : get data from dbo.VIPSTTAB, dbo.BSTANN,dbo.LSFANN
    
 Detail Design : Detailed User Requirements for Private Geographic
                 Detailed User Requirements for State and Local Geographic

 Other         : Called from: frmSpecGeoPriv.cs, frmSpecGeoPub.cs
                 
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
    public class SpecGeoData
    {
        //Retrieve private data for type of Region, Division, State and division excel
        public DataTable GetGeoPrivateTable(string year, int type)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecGeographicPriv";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                if (type == 3)
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                else
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@type", SqlDbType.Int).Value = type;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                if ((type == 1 || type == 2) && dt.Rows.Count >0)
                {
                    dt.Rows[0][0] = "  Total Nonresidential";
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ddown"].ToString() == "3")
                            dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                        else if (dt.Rows[i]["ddown"].ToString() == "4")
                            dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                    }
                }
               
            }
            return dt;
        }

        //Retrive public data for Region, Division, State and Division for excel
        public DataTable GetGeoStateLocalTable(string year, int type)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecGeographicSL";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                if (type == 3)
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                else
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@type", SqlDbType.Int).Value = type;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                if (type == 1 || type == 2)
                {
                    dt.Rows[0][0] = "  Total State and Local";
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ddown"].ToString() == "3")
                            dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                        else if (dt.Rows[i]["ddown"].ToString() == "4")
                            dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                    }
                    //insert NonResidential row
                    DataRow irow = dt.NewRow();
                    irow[0] = "  Nonresidential";
                    if (type ==1)
                    {
                        for (int i = 1; i <=5; i++)
                            irow[i] = int.Parse(dt.Rows[0][i].ToString()) - int.Parse(dt.Rows[1][i].ToString());
                    }
                    else
                    {
                        for (int i = 1; i <= 14; i++)
                            irow[i] = int.Parse(dt.Rows[0][i].ToString()) - int.Parse(dt.Rows[1][i].ToString());
                    }
                    irow["NEWTC"] = "NR";
                    irow["ddown"] = 1;
                    irow["tc2"] = "NR";

                    // insert before '01'
                    dt.Rows.InsertAt(irow, 17);

                }
                else if (type == 4)
                {
                   
                    //update newtc 00 to NR
                    
                    for (int i = 0; i <= 4; i++)
                    {
                        int reg0 = 0, reg1 = 0, reg2 = 0, reg3 = 0, reg4 = 0, div1 = 0, div2 = 0, div3 = 0, div4 = 0, div5 = 0, div6 = 0, div7 = 0, div8 = 0, div9 = 0;
                        string ye = dt.Rows[i]["yy"].ToString();
                        reg0 = (int)dt.Rows[i]["reg0"] - (int)dt.Rows[i+5]["reg0"];
                        reg1 = (int)dt.Rows[i]["reg1"] - (int)dt.Rows[i + 5]["reg1"];
                        div1 = (int)dt.Rows[i]["div1"] - (int)dt.Rows[i + 5]["div1"];
                        div2 = (int)dt.Rows[i]["div2"] - (int)dt.Rows[i + 5]["div2"];
                        reg2 = (int)dt.Rows[i]["reg2"] - (int)dt.Rows[i + 5]["reg2"];
                        div3 = (int)dt.Rows[i]["div3"] - (int)dt.Rows[i + 5]["div3"];
                        div4 = (int)dt.Rows[i]["div4"] - (int)dt.Rows[i + 5]["div4"];
                        reg3 = (int)dt.Rows[i]["reg3"] - (int)dt.Rows[i + 5]["reg3"];
                        div5 = (int)dt.Rows[i]["div5"] - (int)dt.Rows[i + 5]["div5"];
                        div6 = (int)dt.Rows[i]["div6"] - (int)dt.Rows[i + 5]["div6"];
                        div7 = (int)dt.Rows[i]["div7"] - (int)dt.Rows[i + 5]["div7"];
                        reg4 = (int)dt.Rows[i]["reg4"] - (int)dt.Rows[i + 5]["reg4"];
                        div8 = (int)dt.Rows[i]["div8"] - (int)dt.Rows[i + 5]["div8"];
                        div9 = (int)dt.Rows[i]["div9"] - (int)dt.Rows[i + 5]["div9"];

                        //insert NonResidential row
                        DataRow irow = dt.NewRow();
                        irow[0] = "  Total Residential";
                        irow[1] = ye;
                        
                        irow["reg0"] = reg0;
                        irow["reg1"] = reg1;
                        irow["div1"] = div1;
                        irow["div2"] = div2;
                        irow["reg2"] = reg2;
                        irow["div3"] = div3;
                        irow["div4"] = div4;
                        irow["reg3"] = reg3;
                        irow["div5"] = div5;
                        irow["div6"] = div6;
                        irow["div7"] = div7;
                        irow["reg4"] = reg4;
                        irow["div8"] = div8;
                        irow["div9"] = div9;
                        irow["NEWTC"] = "NR";

                        // insert before '01'
                        dt.Rows.InsertAt(irow, 10+i);
                    }

                }
            }
            return dt;
        }

        //Retrieve private data CV for for Region, Division and State
        public DataTable GetGeoPrivateCVTable(string year, int type)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecGeographicPrivCV";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                if (type == 3)
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                else
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@type", SqlDbType.Int).Value = type;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                if (type == 1 || type == 2)
                {
                    dt.Rows[0][0] = "  Total Nonresidential";
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["ddown"].ToString() == "3")
                            dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                        else if (dt.Rows[i]["ddown"].ToString() == "4")
                            dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                    }
                }
                
            }
            return dt;
        }

        //Retrieve private CV for for Region, Division and State
        public DataTable GetGeoStateLocalCVTable(string year, int type)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecGeographicSLCV";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                if (type == 3)
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                else
                    sql_command.Parameters.Add("@yy", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@type", SqlDbType.Int).Value = type;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                if (type == 1 || type == 2)
                {
                    dt.Rows[0][0] = "  Total Nonresidential";
                    for (int i = 1; i < dt.Rows.Count; i++)
                    {
                        if (dt.Rows[i]["newtc"].ToString() == "NR")
                            dt.Rows[i][0] = "  Nonresidential";
                        if (dt.Rows[i]["ddown"].ToString() == "3")
                            dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                        else if (dt.Rows[i]["ddown"].ToString() == "4")
                            dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                    }
                }

            }
            return dt;
        }

    }
}
