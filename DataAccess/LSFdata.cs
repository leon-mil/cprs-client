/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : CPRSDAL.LSFdata.cs

 Programmer    : Diane Musachio

 Creation Date : 5/11/2017

 Inputs        : N/A
 c
 Paramaters    : table, survey, lsfno, owner, newtc, lsf

 Output        : N/A
                   
 Description   : get data from dbo.LSFTAB or dbo.LSFANN to display or edit on 
                frmLSF.cs screen depending on month
 
 Detail Design : Detailed User Requirements for Late Selection Factors

 Other         : Called from: frmLSF.cs

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
    public class LSFdata
    {
        //creates grid of data using pivot function
        public DataTable GetLSFdata(string table, string survey)
        {
            DataTable dt = new DataTable();

            string sql = @"select * from(
                     select lsfno, newtc, lsf 
                     from dbo." + table +
                     @" where (@SURVEY = OWNER)  and (@LSFNO = LSFNO)
                        and (NEWTC in ('00','01','02','03','04','05','06','07','08',
                                       '09','10','11','12','13','14','15','16','19','1T')) 
                     ) src
                     pivot
                     ( 
                     sum(lsf)
                     for newtc in ([00],[01],[02],[03],[04],[05],[06],[07],[08],
                                   [09],[10],[11],[12],[13],[14],[15],[16],[19],[1T])
                     )piv;
                    ";

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();

            //loops through the lsfno to create data array with values for tcs
            using (command)
            {
                for (int i = 1; i < 25; i++)
                {
                    command.Parameters.AddWithValue("@SURVEY", survey);
                    command.Parameters.AddWithValue("@LSFNO", i);
                    command.ExecuteNonQuery();
                    SqlDataAdapter ds = new SqlDataAdapter(command);
                    ds.Fill(dt);
                    command.Parameters.Clear();
                }
            }
            return dt;
        }

        //updates lsfann with edited data
        public void UpdateLSFANNReview(string lsfno, string owner, string newtc, string lsf)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = @"UPDATE dbo.lsfann SET LSF = @LSF
                 WHERE LSFNO = @LSFNO AND OWNER = @OWNER AND NEWTC = @NEWTC";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@LSFNO", Convert.ToInt32(lsfno));
                update_command.Parameters.AddWithValue("@OWNER", owner);
                update_command.Parameters.AddWithValue("@NEWTC", newtc);
                update_command.Parameters.AddWithValue("@LSF", Convert.ToDecimal(lsf));

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
               