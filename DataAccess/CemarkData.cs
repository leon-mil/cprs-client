/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CemarkData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/06/2015
Inputs:             id, cemark record
Parameters:	        None 
Outputs:	        Cemark data	
Description:	    data layer to add, delete, update cemark
Detailed Design:    None 
Other:	            Called by: frmImprovement,frmCemarkPopup
 
Revision History:	
***************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
    public class CemarkData
    {
        /*Retrieve Cemark data for ID*/
        public DataTable GetCemarklist(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT PRGDTM, USRNME, MARKTEXT FROM dbo.CeMark WHERE id = " + GeneralData.AddSqlQuotes(id);
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

            }

            return dt;
        }


        /*Retrieve cemark data */
        public Cemark GetCemarkData(string id)
        {
            Cemark  cms = new Cemark();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT PRGDTM, USRNME, MARKTEXT FROM dbo.cemark WHERE id = " + GeneralData.AddSqlQuotes(id) + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    cms.Id = id;
                    
                    cms.Prgdtm = reader["PRGDTM"].ToString();
                    cms.Marktext = reader["MARKTEXT"].ToString();
                    cms.Usrnme = reader["USRNME"].ToString();
                }
                else
                    cms = null;
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
            return cms;
        }

        /*Add cemark data */
        public void AddCemarkData(Cemark cm)
        {
             SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.cemark (id, marktext, usrnme, prgdtm)"
                            + "Values (@ID, @MARKTEXT, @USRNME, @PRGDTM)";

            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@ID", cm.Id);
            insert_command.Parameters.AddWithValue("@MARKTEXT", cm.Marktext);
            insert_command.Parameters.AddWithValue("@USRNME", cm.Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

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

        /*Update cemark data */
        public bool UpdateCemarkData(Cemark cm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.Cemark SET " +
                                "MARKTEXT = @MARKTEXT, " +
                                "USRNME = @USRNME, " +
                                "PRGDTM = @PRGDTM " +
                                "WHERE ID = @ID" + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@ID", cm.Id);
                update_command.Parameters.AddWithValue("@MARKTEXT", cm.Marktext);
                update_command.Parameters.AddWithValue("@USRNME", cm.Usrnme);
                update_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

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


        /*Delete cemark data */
        public bool DeleteCemark(string id)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string usql = "DELETE FROM dbo.Cemark WHERE ID = @ID and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName); ;

            SqlCommand delete_command = new SqlCommand(usql, sql_connection);
            delete_command.Parameters.AddWithValue("@ID", id);

            try
            {
                sql_connection.Open();
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


        }

    }    
}
