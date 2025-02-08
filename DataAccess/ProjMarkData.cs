/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.ProjMarkData.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/16/2015
Inputs:             id, ProjMark record
Parameters:	        None 
Outputs:	        ProjMark data	
Description:	    data layer to add, delete, update ProjMark
Detailed Design:    None 
Other:	            Called by: frmImprovement,frmProjMarkPopup
 
Revision History:	
***************************************************************************************
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
    public class ProjMarkData
    {
        //Retrieve ProjMark data for all users
        public DataTable GetProjMarks(string id)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT prgdtm, USRNME, MARKTEXT FROM dbo.CsdMark WHERE id = " + GeneralData.AddSqlQuotes(id) + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

            }

            return dt;
        }

        /*Retrieve ProjMark data */
        public ProjMark GetProjmarkData(string id)
        {
            ProjMark cms = new ProjMark();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT prgdtm, USRNME, MARKTEXT FROM dbo.CsdMark WHERE id = " + GeneralData.AddSqlQuotes(id) + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    cms.Id = id;
                    cms.Prgdtm = reader["PRGDTM"].ToString();
                    cms.Usrnme = reader["USRNME"].ToString();
                    cms.Marktext = reader["MARKTEXT"].ToString();
                    
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

        /*Add ProjMark data */
        public void AddProjMarkData(ProjMark cm)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.CSDMARK (id, marktext, usrnme, prgdtm)"
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

        /*Update ProjMark data */
        public bool UpdateProjMarkData(ProjMark cm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.CSDMARK SET " +
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


        /*Delete ProjMark data */
        public bool DeleteProjMark(string id)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string usql = "DELETE FROM dbo.CSDMARK WHERE ID = @ID and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName); ;

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

    public class RespMarkData
    {
        //Retrieve ProjMark data for all users
        public DataTable GetRespMarks(string respid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT prgdtm, USRNME, MARKTEXT FROM dbo.RSPMARK WHERE respid = " + GeneralData.AddSqlQuotes(respid) + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

            }

            return dt;
        }

        /*Retrieve RespMark data */
        public RespMark GetRespmarkData(string respid)
        {
            RespMark cms = new RespMark();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT PRGDTM, USRNME, MARKTEXT FROM dbo.RSPMARK WHERE respid = " + GeneralData.AddSqlQuotes(respid) + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    cms.Respid = respid;
                   
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

        /*Add RespMark data */
        public void AddRespMarkData(RespMark cm)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.RSPMARK (respid,  marktext, usrnme, prgdtm)"
                            + "Values (@RESPID, @MARKTEXT, @USRNME, @PRGDTM)";

            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@RESPID", cm.Respid);
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

        /*Update RespMark data */
        public bool UpdateRespMarkData(RespMark cm)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.RSPMARK SET " +
                                "MARKTEXT = @MARKTEXT, " +
                                "USRNME = @USRNME, " +
                                "PRGDTM = @PRGDTM " +
                                "WHERE RESPID = @RESPID" + " and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName);

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@RESPID", cm.Respid );
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


        /*Delete RespMark data */
        public bool DeleteRespMark(string respid)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string usql = "DELETE FROM dbo.RspMark WHERE RESPID = @RESPID and USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName); ;

            SqlCommand delete_command = new SqlCommand(usql, sql_connection);
            delete_command.Parameters.AddWithValue("@RESPID", respid);

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
