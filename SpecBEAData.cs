/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : CPRSDAL.SpecbeaData.cs
 Programmer    : Christine Zhang
 Creation Date : 10/24/2018
 Inputs        : N/A
 Paramaters    : year, fator
 Output        : N/A               
 Description   : get data from dbo.vipbea, dbo.BSTTAB or dbo.BSTANN 
                 dbo.lsfann or dbo.lsfttab to display by year
 Detail Design : Detailed User Requirements for Federal Bea, State and local
 Other         : Called from: frmSpecBEACDMon.cs, frmSpecSLBEASLMon.cs
                  frmSpecBEACDAnn.cs, frmSpecBEASLAnn.cs
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
    public class SpecBEAData
    {
        //Retrieve dates for year 
        public DataTable GetFedBeaAnnTable(string year, int factor)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecFedbeaann";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@factor", SqlDbType.Int).Value = factor;
               
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][40].ToString() == "3")
                        dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                    else if (dt.Rows[i][40].ToString() == "4")
                        dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                }
            }
            return dt;
        }

        //Get state two chars from code
        public string GetSdateFromVIPBea()
        {
            string sdate = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Top 1 sdate FROM dbo.VIPBEA";
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

        public DataTable GetFedBeaMonTable(int factor)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecFedbeamon";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@factor", SqlDbType.Int).Value = factor;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ddown"].ToString() == "3")
                        dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                    else if (dt.Rows[i]["ddown"].ToString() == "4")
                        dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                }
            }
            return dt;
        }

        public DataTable GetSLBeaAnnTable(string year, int factor)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecSLbeaann";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@Year", SqlDbType.Char).Value = GeneralData.NullIfEmpty(year);
                sql_command.Parameters.Add("@factor", SqlDbType.Int).Value = factor;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][40].ToString() == "3")
                        dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                    else if (dt.Rows[i][40].ToString() == "4")
                        dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                }
            }
            return dt;
        }

        public DataTable GetSLBeaMonTable(int factor)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecSLbeamon";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@factor", SqlDbType.Int).Value = factor;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ddown"].ToString() == "3")
                        dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                    else if (dt.Rows[i]["ddown"].ToString() == "4")
                        dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                }
            }
            return dt;
        }

        public DataTable GetSLBeaMonCVsTable()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "dbo.sp_TSpecSLbeamonCVs";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                //add description
                for (int i = 1; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["ddown"].ToString() == "3")
                        dt.Rows[i][0] = "  " + dt.Rows[i][0].ToString();
                    else if (dt.Rows[i]["ddown"].ToString() == "4")
                        dt.Rows[i][0] = "    " + dt.Rows[i][0].ToString();
                }
            }
            return dt;
        }


    }
}
