/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens Syste
Program Name:       CPRSDAL/NPSpecCasesData.cs
Programmer:         Diane Musachio 
Creation Date:      11/8/2017
Inputs:             dbo.SpecialCase
Parameters:	        
Outputs:	         
Description:	    This program will update, delete or get data
                    from NP Special Cases
Detailed Design:    Detailed User Requirements for NP Special Cases 
Other:	            Called by: frmNPNonResSpecialCases.cs
                               frmNpNonResSpecialNA.cs
Revision History:	
****************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**************************************************************************************/
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
    public class NPSpecialCases
    { 
        //Get data for specific fin
        public NPSpecCases GetFinCase(string finid)
        {
            NPSpecCases npspec = new NPSpecCases(finid);

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = @"select * from dbo.SpecialCase where fin = " + finid.AddSqlQuotes();

            try
            {
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                sql_connection.Open();
                SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    npspec.Psu = reader["PSU"].ToString();
                    npspec.Bpoid = reader["BPOID"].ToString();
                    npspec.Sched = reader["SCHED"].ToString();
                    npspec.Seldate = reader["SELDATE"].ToString();
                    npspec.Projselv = Convert.ToInt32(reader["PROJSELV"]);
                    npspec.Fipstate = reader["FIPSTATE"].ToString();
                    npspec.Owner = reader["OWNER"].ToString().Trim();
                    npspec.Fwgt = float.Parse(reader["FWGT"].ToString());
                    npspec.Newtc = reader["NEWTC"].ToString().Trim();
                    npspec.Source = reader["SOURCE"].ToString().Trim();
                    npspec.Fin = reader["FIN"].ToString().Trim();
                    npspec.Projdesc = reader["PROJDESC"].ToString().Trim();
                    npspec.Projloc = reader["PROJLOC"].ToString().Trim();
                    npspec.Pcityst = reader["PCITYST"].ToString().Trim();
                    npspec.Resporg = reader["RESPORG"].ToString().Trim();
                    npspec.Factoff = reader["FACTOFF"].ToString().Trim();
                    npspec.Othrresp = reader["OTHRRESP"].ToString().Trim();
                    npspec.Addr1 = reader["ADDR1"].ToString().Trim();
                    npspec.Addr2 = reader["ADDR2"].ToString().Trim();
                    npspec.Addr3 = reader["ADDR3"].ToString().Trim();
                    npspec.Zip = reader["ZIP"].ToString().Trim();
                    npspec.Respname = reader["RESPNAME"].ToString().Trim();
                    npspec.Phone = reader["PHONE"].ToString();
                    npspec.Ext = reader["EXT"].ToString();
                    npspec.Fax = reader["FAX"].ToString();
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
                sql_connection.Close();
            }

            return npspec;
        }

        //Get popup data overall totals for current month
        public DataTable GetSpecialCases()
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select fin, psu, bpoid, sched, seldate, owner, fipstate, newtc, 
                   source, projselv, fwgt from dbo.SpecialCase order by fin";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
                return dt;
            }
        }

        /*Get lock field */
        public string ChkLocked(string fin)
        {
            string locked = String.Empty;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Lockid FROM dbo.SpecialCase WHERE FIN =" + fin.AddSqlQuotes();

            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    locked = reader["LOCKID"].ToString().Trim();
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

            if (locked != String.Empty)
                return locked;
            else
                return String.Empty;
      }


        /*Update lock field */
        public bool UpdateLock(string fin, string username)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.Specialcase SET " +
                                "LOCKID = @LOCKID " +
                                "WHERE FIN = " + fin.AddSqlQuotes();

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@FIN", fin);
                update_command.Parameters.AddWithValue("@LOCKID", username);

                try
                {
                    int count = update_command.ExecuteNonQuery();
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
        }

        //delete row from specialcase
        public void DeleteSpecCaseData(string fin)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.specialcase WHERE fin = " + fin.AddSqlQuotes(), sql_connection);

                    sql_command.Parameters.Add("@fin", SqlDbType.Char).Value = GeneralData.NullIfEmpty(fin);

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

        public void UpdateNPSpecialCaseData(NPSpecCases sc)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.specialcase " +
                               "set " +                               
                               "NEWTC = @NEWTC " +
                               ",PROJDESC = @PROJDESC " +
                               ",PROJLOC = @PROJLOC " +
                               ",PCITYST = @PCITYST " +
                               ",RESPORG = @RESPORG " +
                               ",FACTOFF = @FACTOFF " +
                               ",RESPNAME = @RESPNAME " +
                               ",OTHRRESP = @OTHRRESP " +
                               ",ADDR1 = @ADDR1 " +
                               ",ADDR2 = @ADDR2 " +
                               ",ADDR3 = @ADDR3 " +
                               ",ZIP = @ZIP " +
                               ",PHONE = @PHONE " +
                               ",EXT = @EXT " +
                               ",FAX = @FAX " +                              
                               " WHERE FIN = @FIN ";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, sql_connection);

                    sql_command.Parameters.AddWithValue("@FIN", sc.Fin);
                    sql_command.Parameters.AddWithValue("@NEWTC", sc.Newtc);
                    sql_command.Parameters.AddWithValue("@PROJDESC", sc.Projdesc);
                    sql_command.Parameters.AddWithValue("@PROJLOC", sc.Projloc);
                    sql_command.Parameters.AddWithValue("@PCITYST", sc.Pcityst);
                    sql_command.Parameters.AddWithValue("@RESPORG", sc.Resporg);
                    sql_command.Parameters.AddWithValue("@FACTOFF", sc.Factoff);
                    sql_command.Parameters.AddWithValue("@OTHRRESP", sc.Othrresp);
                    sql_command.Parameters.AddWithValue("@RESPNAME", sc.Respname);
                    sql_command.Parameters.AddWithValue("@ADDR1", sc.Addr1);
                    sql_command.Parameters.AddWithValue("@ADDR2", sc.Addr2);
                    sql_command.Parameters.AddWithValue("@ADDR3", sc.Addr3);
                    sql_command.Parameters.AddWithValue("@ZIP", sc.Zip);
                    sql_command.Parameters.AddWithValue("@PHONE", sc.Phone);
                    sql_command.Parameters.AddWithValue("@EXT", sc.Ext);
                    sql_command.Parameters.AddWithValue("@FAX", sc.Fax);

                    //Open the connection.
                    sql_connection.Open();

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
    }
}





