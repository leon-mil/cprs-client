
/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : MonthlyVipsData.cs
Programmer    : Christine Zhang
Creation Date : May 6 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frm.cs
Change Request: 
Specification :  Specifications  
Rev History   : See Below

Other         : N/A
 ***********************************************************************/
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
    public class MonthlyVipsData
    {
        //Get monthly vips
        public MonthlyVips GetMonthlyVips(string id, TypeDBSource dbsource = TypeDBSource.Default)
        {
            string db_table = "dbo.MONTHLY_VIP_DATA";
            if (dbsource == TypeDBSource.Hold)
                db_table = "dbo.VIP_REJECT_Hold";
            
            MonthlyVips monvips = new MonthlyVips(id);

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * , FORMAT(CONVERT(DATE,date6+'01'), N'MMM yyyy', 'en-US') AS date8 FROM ";
            sql = sql + db_table + " WHERE ID = " + GeneralData.AddSqlQuotes(id) + " order by date6 DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            List<MonthlyVip> result = new List<MonthlyVip>();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    MonthlyVip mv = new MonthlyVip();
                    mv.Date6 = reader["DATE6"].ToString();
                    mv.Date8 = reader["DATE8"].ToString();
                    mv.Vipdata = (int)reader["VIPDATA"];
                    mv.Vipdatar = (int)reader["VIPDATAR"];
                    mv.Vipflag = reader["VIPFLAG"].ToString();
         
                    mv.vs = "e";
                    mv.vsOld = "e";

                    result.Add(mv);
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

             monvips.monthlyViplist = result;
            
            return monvips;
        }

        /*Create a new monthly vip list */
        private List<MonthlyVip> CreateDisplaytMonthlyVips()
        {
            List<MonthlyVip> result = new List<MonthlyVip>();

            DateTime dt = DateTime.Today;
            for (int i = 1; i <= 240; i++)
            {
                MonthlyVip mv = new MonthlyVip();
                mv.Date6 = DateTime.Today.AddMonths(-i).ToString("yyyyMM");
                mv.Date8 = DateTime.Today.AddMonths(-i).ToString("MMM, yyyy");
                
                mv.Vipdata = 0;
                mv.Vipdatar = 0;
                mv.Vipflag = "B";
                mv.Pct5c = 0;
                mv.Pct5cr = 0;

                mv.vs = "i";
                mv.vsOld = "i";
                result.Add(mv);
            }
            return result;
        }

        //Get display monthly vips for C700 screen
        public MonthlyVips GetDisplayMonthlyVips(string id, TypeDBSource dbsource = TypeDBSource.Default, int rvitm5c=0, int rvitm5cr=0)
        {
            string db_table = "dbo.MONTHLY_VIP_DATA";
            if (dbsource == TypeDBSource.Hold)
                db_table = "dbo.VIP_REJECT_Hold";

            MonthlyVips monvips = new MonthlyVips(id);
            List<MonthlyVip> displaymvps = CreateDisplaytMonthlyVips();

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM ";
            sql = sql + db_table + " WHERE Id = " + GeneralData.AddSqlQuotes(id) + " order by date6 DESC";
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string d6 = reader["DATE6"].ToString();

                    /*find monthly vip from display list */
                    MonthlyVip mv = displaymvps.Find(x => x.Date6 == d6);
                    if (mv == null)
                    {
                        monvips.Withallvips = false;
                        break;
                    }
           
                    mv.Vipdata = (int)reader["VIPDATA"];
                    mv.Vipdatar = (int)reader["VIPDATAR"];

                    if (mv.Vipdata != 0 && rvitm5c > 0)
                    {
                        mv.Pct5c = Convert.ToInt32((double)mv.Vipdata / rvitm5c * 100);
                        if (mv.Pct5c > 999)
                            mv.Pct5c = 999;
                    }
                    if (mv.Vipdatar != 0 && rvitm5cr > 0)
                    {
                        mv.Pct5cr = Convert.ToInt32((double)mv.Vipdatar / rvitm5cr * 100);
                        if (mv.Pct5cr > 999)
                            mv.Pct5cr = 999;
                    }

                    mv.Vipflag = reader["VIPFLAG"].ToString();

                    mv.vs = "e";
                    mv.vsOld = "e";
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

            monvips.monthlyViplist = displaymvps;

            return monvips;

        }

        /*Save monthly vip */
        public bool SaveMonthlyVips(MonthlyVips monVips, TypeDBSource dbsource = TypeDBSource.Default)
        {
            string db_table = "dbo.MONTHLY_VIP_DATA";
            if (dbsource == TypeDBSource.Hold)
                db_table = "dbo.VIP_REJECT_Hold";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                foreach (MonthlyVip mv in monVips.monthlyViplist)
                {
                    if (mv.vsOld == "e" &&  mv.vs == "m")
                    {
                        string usql = "UPDATE " + db_table + " SET " +
                                 "VIPDATA = @VIPDATA, " +
                                 "VIPDATAR = @VIPDATAR, " +
                                 "VIPFLAG = @VIPFLAG " +
                                 "WHERE ID = @ID AND DATE6 = @DATE6";

                        SqlCommand update_command = new SqlCommand(usql, sql_connection);
                        update_command.CommandTimeout = 0;

                        update_command.Parameters.AddWithValue("@ID", monVips.Id);
                        update_command.Parameters.AddWithValue("@DATE6", mv.Date6);
                        update_command.Parameters.AddWithValue("@VIPDATA", mv.Vipdata);
                        update_command.Parameters.AddWithValue("@VIPDATAR", mv.Vipdatar);
                        update_command.Parameters.AddWithValue("@VIPFLAG", mv.Vipflag);

                        try
                        {
                            int count = update_command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }

                        mv.vsOld = "e";
                        mv.vs = "e";
                    }
                    else if (mv.vsOld == "i" && (mv.vs == "a" || mv.vs == "m") && (mv.Vipflag != "B"))
                    {
                        string isql = "insert " + db_table + " (id, date6, vipdata, vipdatar, vipflag)"
                             + "Values (@ID, @DATE6, @VIPDATA, @VIPDATAR, @VIPFLAG)";
                        SqlCommand insert_command = new SqlCommand(isql, sql_connection);
                        insert_command.CommandTimeout = 0;

                        insert_command.Parameters.AddWithValue("@ID", monVips.Id);
                        insert_command.Parameters.AddWithValue("@DATE6", mv.Date6);
                        insert_command.Parameters.AddWithValue("@VIPDATA", mv.Vipdata);
                        insert_command.Parameters.AddWithValue("@VIPDATAR", mv.Vipdatar);
                        insert_command.Parameters.AddWithValue("@VIPFLAG", mv.Vipflag);

                        try
                        {
                            insert_command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }

                        mv.vsOld = "e";
                        mv.vs = "e";
                    }
                    else if (mv.vsOld == "e" && mv.vs == "d")
                    {
                        string usql = "DELETE FROM " + db_table +
                             " WHERE ID = @ID AND DATE6 = @DATE6";

                        SqlCommand delete_command = new SqlCommand(usql, sql_connection);
                        delete_command.CommandTimeout = 0;

                        delete_command.Parameters.AddWithValue("@ID", monVips.Id);
                        delete_command.Parameters.AddWithValue("@DATE6", mv.Date6);

                        try
                        {
                            int count = delete_command.ExecuteNonQuery();
                            if (count > 0)
                            {
                                mv.vsOld = "i";
                                mv.vs = "i";
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

                }
                sql_connection.Close();

            }
            
            return true;
        }

        //Insert new records to monthly vip table
        public void AddMonthlyVips(MonthlyVips monVips, TypeDBSource dbsource = TypeDBSource.Default)
        {
            string db_table = "dbo.MONTHLY_VIP_DATA";
            if (dbsource == TypeDBSource.Hold)
                db_table = "dbo.VIP_REJECT_Hold";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                foreach (MonthlyVip mv in monVips.monthlyViplist)
                {
                    if (mv.vs != "i" && mv.vs != "d")
                    {
                        string isql = "insert " + db_table + " (id, date6, vipdata, vipdatar, vipflag)"
                                + "Values (@ID, @DATE6, @VIPDATA, @VIPDATAR, @VIPFLAG)";
                        SqlCommand insert_command = new SqlCommand(isql, sql_connection);
                        insert_command.CommandTimeout = 0;

                        insert_command.Parameters.AddWithValue("@ID", monVips.Id);
                        insert_command.Parameters.AddWithValue("@DATE6", mv.Date6);
                        insert_command.Parameters.AddWithValue("@VIPDATA", mv.Vipdata);
                        insert_command.Parameters.AddWithValue("@VIPDATAR", mv.Vipdatar);
                        insert_command.Parameters.AddWithValue("@VIPFLAG", mv.Vipflag);

                        try
                        {
                            insert_command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }

                        mv.vsOld = "e";
                        mv.vs = "e";
                    }
                }

                sql_connection.Close();
            }
            
        }

        //Delete monthly vip data from table
        public bool DeleteMonthlyVips(string id, TypeDBSource dbsource = TypeDBSource.Default)
        {
            string db_table = "dbo.MONTHLY_VIP_DATA";
            if (dbsource == TypeDBSource.Hold)
                db_table = "dbo.VIP_REJECT_Hold";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                string usql = "DELETE FROM " + db_table +
                              " WHERE ID = @ID ";

                SqlCommand delete_command = new SqlCommand(usql, sql_connection);
                delete_command.CommandTimeout = 0;

                delete_command.Parameters.AddWithValue("@ID", id);
              
                try
                {
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
                finally
                {
                    sql_connection.Close();
                }

            }
        }

        //Get cum monthly vips
        public int GetCumVipsFormDB(string id, TypeDBSource dbsource = TypeDBSource.Default, bool reportVal = false)
        {
            int totalvip = 0;
            string sql = string.Empty;

            string db_table = "dbo.MONTHLY_VIP_DATA";
            if (dbsource == TypeDBSource.Hold)
                db_table = "dbo.VIP_REJECT_Hold";

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            if (!reportVal)
                sql = "SELECT SUM(VIPDATA) as total FROM ";
            else
                sql = "SELECT SUM(VIPDATAR) as total FROM ";
            sql = sql + db_table + " WHERE ID = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            List<MonthlyVip> result = new List<MonthlyVip>();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    { 
                        if (!reader.IsDBNull(0))
                            totalvip = Convert.ToInt32(reader["total"]);
                    }
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

            return totalvip;
        }



    } 
}
