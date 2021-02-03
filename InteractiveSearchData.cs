/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : /CPRSDAL/InteractivesSearchData.cs
Programmer    : Christine Zhang
Creation Date : Sept. 22 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frmwhereSrch.cs
Change Request: 
Specification : Interactives Search Specifications  
Rev History   : See Below

Other         : call from frmWhereSrch.cs, frmWhereStore.cs and frmWhereRecall.cs
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
    public class InteractiveSearchData
    {
         /*Get Interactives Search Data */
        public DataTable GetInteractivesSearchData(string where_cause, bool includeUnsample, string add_cols, ref string error_message)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
               
                StringBuilder sql = new StringBuilder("SELECT RTRIM(FIN) as FIN, MASTERID, ID, RESPID, OWNER, NEWTC, SELDATE, STRTDATE, RVITM5C");
                if (add_cols != "" )
                {
                    sql.Append(","+ add_cols); //Average loop times 4~ or more
                }
                sql.Append(" FROM dbo.INTERACTIVE_SEARCH WHERE ");
                sql.Append(where_cause);
                if (!includeUnsample)
                    sql.Append(" AND ISSAMPLE = '1'");
                sql.Append(" order by FIN");
                 
                SqlCommand command = new SqlCommand(sql.ToString(), sql_connection);

                try
                {
                    SqlDataAdapter da = new SqlDataAdapter(command);

                    da.Fill(dt);
                }
                catch (SqlException ex)
                {
                    error_message = "Execute exception issue: " + ex.Message;
                }
                catch (InvalidOperationException ex) // This will catch SqlConnection Exception
                {
                    error_message = "InvalidOperation Exception issue: " + ex.Message;
                }
                catch (Exception ex) // This will catch every Exception
                {
                    error_message = "Exception Message: " + ex.Message;
                    
                }
                finally // don't forget to close your connection when exception occurs.
                {
                    sql_connection.Close();
                }
            }
            return dt;
        }

        /*Get all column name from view */
        public List<string> GetColumnNames()
        {
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT name FROM sys.columns WHERE object_id = OBJECT_ID('dbo.INTERACTIVE_SEARCH')";
            SqlCommand command = new SqlCommand(sql, connection);

            List<string> clist = new List<string>();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    string name = reader["NAME"].ToString(); 
                    clist.Add(name);
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

            return clist;

        }

        //Get empty table 
        public DataTable GetEmptySearchTable()
        {
            // Here we create a DataTable with 18 columns.

            DataTable table = new DataTable();

            table.Columns.Add("ROW", typeof(int));
            table.Columns.Add("FIN", typeof(string));
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("RESPID", typeof(string));
            table.Columns.Add("OWNER", typeof(string));
            table.Columns.Add("NEWTC", typeof(string));
            table.Columns.Add("SELDATE", typeof(string));
            table.Columns.Add("STRTDATE", typeof(string));
            table.Columns.Add("RVITM5C", typeof(int));
           
            return table;
        }

        //Get Empty where table
        public DataTable GetEmptyWhereTable()
        {
            // Here we create a DataTable with 18 columns.

            DataTable table = new DataTable();

            table.Columns.Add("ROW", typeof(int));
            table.Columns.Add("WHERE", typeof(string));

            return table;
        }

        /*Retrieve variables data */
        public DataTable GetVariables()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * FROM dbo.Varlist ";
                SqlCommand command = new SqlCommand(sql, sql_connection);

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }


        /*Retrieve criteria data */
        public List<SearchCriteria> GetSearchCriteriaData()
        {
            List<SearchCriteria> CriteriaList = new List<SearchCriteria>() ;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Seqno, wheretxt FROM dbo.SaveSearch WHERE USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName) + " order by seqno ";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    SearchCriteria sc = new SearchCriteria();
                    sc.Seqno = Convert.ToInt32(reader["SEQNO"]);
                    sc.Wheretext = reader["WHERETXT"].ToString();
                  
                    CriteriaList.Add(sc);
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
            return CriteriaList;
        }

        /*Add criteria data */
        public void AddSearchCriteriaData(int seqno, string wherecl)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.SaveSearch (usrnme, seqno, wheretxt)"
                            + "Values (@USRNME, @SEQNO, @WHERETXT)";

            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
            insert_command.Parameters.AddWithValue("@SEQNO", seqno);
            insert_command.Parameters.AddWithValue("@WHERETXT", wherecl);
            
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

        /*Update criteria data */
        public bool UpdateSearchCriteriaData(int seqno, string wherecl)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.SaveSearch SET " +
                                "WHERETXT = @WHERETXT " +
                                "WHERE SEQNO = @SEQNO" + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@WHERETXT", wherecl);
                update_command.Parameters.AddWithValue("@SEQNO", seqno);
               
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

        /*Update criteria data seqno */
        public bool UpdateSeqno(int seqno)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.SaveSearch SET " +
                                "WHERE SEQNO = @SEQNO" + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@SEQNO", seqno);

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


        /*Delete criteria data */
        public bool DeleteSearchCriteria(List<SearchCriteria> wList, int seqno)
        {
            //Remove from list
            wList.RemoveAt(seqno-1);

            //Update list
            foreach (SearchCriteria sc in wList)
            {
                if (sc.Seqno > seqno)
                {
                    sc.Seqno = sc.Seqno - 1;
                }
            }
            
            //Delete the record from database
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            string usql = "DELETE FROM dbo.SaveSearch WHERE USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName); 
            SqlCommand delete_command = new SqlCommand(usql, sql_connection);
            try
            {
                sql_connection.Open();
                int count = delete_command.ExecuteNonQuery();
                if (count > 0)
                {
                    //Update seqno
                    foreach (SearchCriteria sc in wList)
                    {
                       AddSearchCriteriaData(sc.Seqno, sc.Wheretext);
                    }

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



}
