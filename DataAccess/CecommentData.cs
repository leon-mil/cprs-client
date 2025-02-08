/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CecommentData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             id, cecomment data
Parameters:	        None 
Outputs:	        Cecomments data	
Description:	    data layer tor retrieve and add comment data
Detailed Design:    None 
Other:	            Called by: frmImprovement, frmCeHistoryPopup
 
Revision History:	
****************************************************************************************
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

namespace CprsDAL
{
    public class CecommentData
    {
        /*Retrieve comment data */
        public Cecomments GetCecommentData(string id)
        {
            Cecomments  ccs = new Cecomments(id);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Commdate, commtime, usrnme, commtext FROM dbo.cecomment WHERE id = " + GeneralData.AddSqlQuotes(id) + " order by commdate DESC, commtime DESC";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Cecomment  cc = new Cecomment();
                    cc.Commdate = reader["COMMDATE"].ToString();
                    cc.Commtime = reader["COMMTIME"].ToString();         
                    cc.Usrnme = reader["USRNME"].ToString();
                    cc.Commtext = reader["COMMTEXT"].ToString();
                    ccs.Cecommentlist.Add(cc);
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

            return ccs;

        }

        /*Add cecomment data */
        public void AddCecommentData(string id, Cecomment cc)
        {
             SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.cecomment (id, commdate, commtime, commtext, usrnme)"
                            + "Values (@id, @COMMDATE, @COMMTIME, @COMMTEXT, @USRNME)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@id", id);
            insert_command.Parameters.AddWithValue("@commdate", cc.Commdate);
            insert_command.Parameters.AddWithValue("@COMMTIME", cc.Commtime);
            insert_command.Parameters.AddWithValue("@COMMTEXT", cc.Commtext);
            insert_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);

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

    }
}
