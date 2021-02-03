/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : CPRSDAL.TabAnnualTabsData.cs

 Programmer    : Diane Musachio

 Creation Date : 5/1/2019

 Inputs        : N/A
 
 Paramaters    : 

 Output        : N/A
                   
 Description   : get data to calculate LSF and BST seasonally adjusted
        and nonseasonally adjusted annual tabulations

 Detail Design : Detailed User Requirements for Annual Tabulations

 Other         : Called from: Tabulations -> Annual -> Annual Tabulations

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
    public class TabAnnualTabsData
    {
        public DataTable GetAnnualData()
        {
            DataTable dt = new DataTable();

            string sql = @"select *, substring(newtc, 1, 2) as NEWTC2, substring(newtc, 1, 3) as NEWTC3
                from dbo.ANNUAL";

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();

            using (command)
            {
                command.ExecuteNonQuery();
                SqlDataAdapter ds = new SqlDataAdapter(command);
                ds.Fill(dt);
                command.Parameters.Clear();
            }

            return dt;
        }

        /*Retrieve description for newtc*/
        public List<string> GetTCDescription(string newtc, string survey)
        {
            List<string> tcdesc = new List<string>(2);

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string sqlQuery = "SELECT TCDESCRIPTION, PUB" + survey + " FROM dbo.PUBTCLIST where NEWTC = " 
                + newtc.AddSqlQuotes();

            SqlCommand command = new SqlCommand(sqlQuery, sql_connection);

            try
            {
                sql_connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    tcdesc.Add(reader["TCDESCRIPTION"].ToString());
                    tcdesc.Add(reader["PUB" + survey].ToString());                 
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

            return tcdesc;
        }
        public string GetCurrMonthDateinTable()
        {
            string sdate = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Top 1 sdate FROM dbo.ANNUAL";
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

        public DataTable GetLSFTabData(string owner, string newtc)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select LSFNO, LSF
                     from dbo.LSFTAB where owner = @OWNER and newtc = @NEWTC";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@OWNER", owner);
                    cmd.Parameters.AddWithValue("@NEWTC", newtc);
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }

        public DataTable GetLSFAnnData(string owner, string newtc)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select LSFNO, LSF
                     from dbo.LSFANN where owner = @OWNER and newtc = @NEWTC
                     order by OWNER, NEWTC, LSFNO";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@OWNER", owner);
                    cmd.Parameters.AddWithValue("@NEWTC", newtc);
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }

        public DataTable GetBstTabData(string owner, string year5, string newtc, string year1)
        {
            DataTable dt = new DataTable();

            string sql = @"select BST, SDATE
                             from dbo.BSTTAB 
                               where owner = " + owner.AddSqlQuotes() +
                                   " and substring(sdate, 1, 4) >= " + year5.AddSqlQuotes() +
                                   " and substring(sdate, 1, 4) <= " + year1.AddSqlQuotes() +
                                   " and newtc = " + newtc.AddSqlQuotes() +
                                   " order by sdate desc";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            using (SqlCommand cmd = new SqlCommand(sql, sql_connection))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(dt);
            }

            return dt;

        }

        public DataTable GetBstAnnData(string owner, string year5, string newtc, string year1)
        {
            DataTable dt = new DataTable();

            string sql = @"select BST, SDATE
                             from dbo.BSTANN 
                               where owner = " + owner.AddSqlQuotes() +
                                   " and substring(sdate, 1, 4) >= " + year5.AddSqlQuotes() +
                                   " and substring(sdate, 1, 4) <= " + year1.AddSqlQuotes() +
                                   " and newtc = " + newtc.AddSqlQuotes() +
                                   " order by sdate desc";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            using (SqlCommand cmd = new SqlCommand(sql, sql_connection))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(dt);
            }

            return dt;

        }
        public DataTable GetSafTabData(string owner, string year5, string newtc, string year1)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                string sql = @"select SAF, SDATE
                             from dbo.SAFTAB 
                               where owner = " + owner.AddSqlQuotes() +
                                   " and substring(sdate, 1, 4) >= " + year5.AddSqlQuotes() +
                                   " and substring(sdate, 1, 4) <= " + year1.AddSqlQuotes() +
                                   " and newtc = " + newtc.AddSqlQuotes() +
                                   " order by sdate desc";

                using (SqlCommand cmd = new SqlCommand(sql, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

                return dt;
            }
        }


        //Check whether the survey/prior year exists in the database
        public bool CheckMonthExists(string owner, string curryr)
        {
            bool data_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = @"select year from dbo.annexport where 
                     year = " + curryr.AddSqlQuotes() + " and owner = " +
                     owner.AddSqlQuotes();

                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            return true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return data_exist;
        }

        //Delete the row from the ANNEXPORT table for current survey and prior year
        public void DeleteRow(string owner, string prioryr)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand(@"DELETE FROM dbo.ANNEXPORT
                              WHERE OWNER = " + owner.AddSqlQuotes() + 
                              " AND YEAR = " + prioryr.AddSqlQuotes(), sql_connection);
            
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

        //Updates annexport with export datatable
        public bool InsertAnnExport(DataTable dt)
        {
            bool isSuccess;
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //Open the connection.
                sql_connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(sql_connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, null))
                {
                    bulkCopy.DestinationTableName = "dbo.ANNEXPORT";

                    try
                    {
                        bulkCopy.WriteToServer(dt);
                        isSuccess = true;
                    }
                    catch (Exception ex)
                    {
                        isSuccess = false;
                        throw ex;
                    }
                    finally
                    {
                        sql_connection.Close(); //close database connection
                    }
                }
                return isSuccess;
            }
        }
    }
}



               