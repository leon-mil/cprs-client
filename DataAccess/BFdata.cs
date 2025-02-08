/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : CPRSDAL.BFdata.cs

 Programmer    : Diane Musachio

 Creation Date : 5/15/2017

 Inputs        : N/A

 Paramaters    : table, survey, list dy, sdate, owner, newtc, bst

 Output        : N/A
                   
 Description   : get data from dbo.BSTTAB or dbo.BSTANN to display or edit on 
                frmBST.cs screen depending on month
 
 Detail Design : Detailed User Requirements for Boost Factors

 Other         : Called from: frmBF.cs

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
    public class BFdata
    {
        //Retrieve dates for monthly pull-down 
        public DataTable GetMonthList(string table, string survey)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select distinct top (72) SDATE
                     from dbo." + table + @" 
                     where owner = @survey 
                     order by SDATE desc";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@SURVEY", survey);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }

                return dt;
            }
        }

        //creates grid of data using pivot function
        public DataTable GetBFdata(string survey, List<string> dy, string table)
        {
            DataTable dt = new DataTable();

            string sql = @"select * from(
                     select sdate, newtc, bst
                     from dbo." + table +
                     @" where (@SURVEY = OWNER) and (@SDATE = SDATE)
                     and (NEWTC in ('00','01','02','03','04','05','06','07','08',
                                    '09','10','11','12','13','14','15','16','19','1T')) 
                     ) src
                     pivot
                     ( 
                     sum(bst)
                     for newtc in ([00],[01],[02],[03],[04],[05],[06],[07],[08],
                                   [09],[10],[11],[12],[13],[14],[15],[16],[19],[1T])
                     )piv;
                    ";

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();

            //loops through the dates to create data array with values for tcs
            using (command)
            {
                for (int i = 0; i < dy.Count; i++)
                {
                    command.Parameters.AddWithValue("@SURVEY", survey);
                    command.Parameters.AddWithValue("@SDATE", dy[i].ToString());
                    command.ExecuteNonQuery();
                    SqlDataAdapter ds = new SqlDataAdapter(command);
                    ds.Fill(dt);
                    command.Parameters.Clear();
                }
            }

            return dt;
        }

        //updates bstann with edited data
        public void UpdateBSTANNReview(string sdate, string owner, string newtc, string bst)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = @"UPDATE dbo.bstann SET BST = @BST
                 WHERE SDATE = @SDATE AND OWNER = @OWNER AND NEWTC = @NEWTC";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@SDATE", Convert.ToInt32(sdate));
                update_command.Parameters.AddWithValue("@OWNER", owner);
                update_command.Parameters.AddWithValue("@NEWTC", newtc);
                update_command.Parameters.AddWithValue("@BST", Convert.ToDecimal(bst));

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

        // If the current stat period does not exist, insert it into the table
        public void InsertRows(string sdate, string owner, string newtc, string bst)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand(@"INSERT INTO dbo.BSTANN 
                        (sdate, owner, newtc, bst) 
                        VALUES (@SDATE, @OWNER, @NEWTC, @BST)", sql_connection);

                    sql_command.Parameters.AddWithValue("@SDATE", Convert.ToInt32(sdate));
                    sql_command.Parameters.AddWithValue("@OWNER", owner);
                    sql_command.Parameters.AddWithValue("@NEWTC", newtc);
                    sql_command.Parameters.AddWithValue("@BST", Convert.ToDecimal(bst));

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
    }
}
               