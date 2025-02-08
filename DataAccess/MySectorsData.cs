/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.MySectorsData.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/5/2016
Inputs:             None
Parameters:	        None 
Outputs:	        current users data	
Description:	    data layer to add, delete, edit my sectors
Detailed Design:    None 
Other:	            Called by: frmMySectors
 
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
    public class MySectorsData
    {
        //Get data from HQSectors
        public DataTable GetMySectors()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select * from dbo.HQSector order by usrnme", sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        //Get user name for adding to HQSector
        public List<string> GetHQSectorUsersAdd()
        {
            List<string> names = new List<string>();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT USRNME from dbo.sched_id WHERE USRNME not in (select usrnme from dbo.HQSector) and Grpcde not in (3, 4,5, 7, 8) and USRNME <> 'aaweb001' order by USRNME";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    names.Add("");
                    while (reader.Read())
                    {
                        names.Add(reader["USRNME"].ToString());
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

            return names;

        }

        //Get my sector for a user
        public MySector GetMySectorData(string username)
        {
            MySector ms = null;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * from HQSector WHERE USRNME = " + GeneralData.AddSqlQuotes(username);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ms = new MySector(username);

                    ms.Sect00 = reader["SECT00"].ToString();
                    ms.Sect01 = reader["SECT01"].ToString();
                    ms.Sect02 = reader["SECT02"].ToString();
                    ms.Sect03 = reader["SECT03"].ToString();
                    ms.Sect04 = reader["SECT04"].ToString();
                    ms.Sect05 = reader["SECT05"].ToString();
                    ms.Sect06 = reader["SECT06"].ToString();
                    ms.Sect07 = reader["SECT07"].ToString();
                    ms.Sect08 = reader["SECT08"].ToString();
                    ms.Sect09 = reader["SECT09"].ToString();
                    ms.Sect10 = reader["SECT10"].ToString();
                    ms.Sect11 = reader["SECT11"].ToString();
                    ms.Sect12 = reader["SECT12"].ToString();
                    ms.Sect13 = reader["SECT13"].ToString();
                    ms.Sect14 = reader["SECT14"].ToString();
                    ms.Sect15 = reader["SECT15"].ToString();
                    ms.Sect16 = reader["SECT16"].ToString();
                    ms.Sect19 = reader["SECT19"].ToString();
                    ms.Sect1T = reader["SECT1T"].ToString();
                    
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

            return ms;
        }

        //Save my sector
        public bool UpdateMySectorData(MySector ms)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.HQSECTOR SET " +
                                "SECT00 = @SECT00, " +
                                "SECT01 = @SECT01, " +
                                "SECT02 = @SECT02, " +
                                "SECT03 = @SECT03, " +
                                "SECT04 = @SECT04, " +
                                "SECT05 = @SECT05, " +
                                "SECT06 = @SECT06, " +
                                "SECT07 = @SECT07, " +
                                "SECT08 = @SECT08, " +
                                "SECT09 = @SECT09, " +
                                "SECT10 = @SECT10, " +
                                "SECT11 = @SECT11, " +
                                "SECT12 = @SECT12, " +
                                "SECT13 = @SECT13, " +
                                "SECT14 = @SECT14, " +
                                "SECT15 = @SECT15, " +
                                "SECT16 = @SECT16, " +
                                "SECT19 = @SECT19, " +
                                "SECT1T = @SECT1T " +
                                "WHERE USRNME = " + GeneralData.AddSqlQuotes(ms.Username);

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@USRNME", ms.Username);
                update_command.Parameters.AddWithValue("@SECT00", ms.Sect00);
                update_command.Parameters.AddWithValue("@SECT01", ms.Sect01);
                update_command.Parameters.AddWithValue("@SECT02", ms.Sect02);
                update_command.Parameters.AddWithValue("@SECT03", ms.Sect03);
                update_command.Parameters.AddWithValue("@SECT04", ms.Sect04);
                update_command.Parameters.AddWithValue("@SECT05", ms.Sect05);
                update_command.Parameters.AddWithValue("@SECT06", ms.Sect06);
                update_command.Parameters.AddWithValue("@SECT07", ms.Sect07);
                update_command.Parameters.AddWithValue("@SECT08", ms.Sect08);
                update_command.Parameters.AddWithValue("@SECT09", ms.Sect09);
                update_command.Parameters.AddWithValue("@SECT10", ms.Sect10);
                update_command.Parameters.AddWithValue("@SECT11", ms.Sect11);
                update_command.Parameters.AddWithValue("@SECT12", ms.Sect12);
                update_command.Parameters.AddWithValue("@SECT13", ms.Sect13);
                update_command.Parameters.AddWithValue("@SECT14", ms.Sect14);
                update_command.Parameters.AddWithValue("@SECT15", ms.Sect15);
                update_command.Parameters.AddWithValue("@SECT16", ms.Sect16);
                update_command.Parameters.AddWithValue("@SECT19", ms.Sect19);
                update_command.Parameters.AddWithValue("@SECT1T", ms.Sect1T);
                
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

        /*Add mysector data */
        public void AddMySectorData(MySector ms)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.HQSECTOR (usrnme, sect00, sect01, sect02, sect03, sect04, sect05, sect06, sect07, sect08, sect09, sect10, sect11, sect12, sect13, sect14, sect15, sect16, sect19, sect1T)"
                            + "Values (@USRNME, @SECT00, @SECT01,  @SECT02,  @SECT03,  @SECT04,  @SECT05,  @SECT06, @SECT07,  @SECT08,  @SECT09,  @SECT10,  @SECT11,  @SECT12,  @SECT13,  @SECT14,  @SECT15,  @SECT16, @SECT19,  @SECT1T)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@USRNME", ms.Username);
            insert_command.Parameters.AddWithValue("@SECT00", ms.Sect00);
            insert_command.Parameters.AddWithValue("@SECT01", ms.Sect01);
            insert_command.Parameters.AddWithValue("@SECT02", ms.Sect02);
            insert_command.Parameters.AddWithValue("@SECT03", ms.Sect03);
            insert_command.Parameters.AddWithValue("@SECT04", ms.Sect04);
            insert_command.Parameters.AddWithValue("@SECT05", ms.Sect05);
            insert_command.Parameters.AddWithValue("@SECT06", ms.Sect06);
            insert_command.Parameters.AddWithValue("@SECT07", ms.Sect07);
            insert_command.Parameters.AddWithValue("@SECT08", ms.Sect08);
            insert_command.Parameters.AddWithValue("@SECT09", ms.Sect09);
            insert_command.Parameters.AddWithValue("@SECT10", ms.Sect10);
            insert_command.Parameters.AddWithValue("@SECT11", ms.Sect11);
            insert_command.Parameters.AddWithValue("@SECT12", ms.Sect12);
            insert_command.Parameters.AddWithValue("@SECT13", ms.Sect13);
            insert_command.Parameters.AddWithValue("@SECT14", ms.Sect14);
            insert_command.Parameters.AddWithValue("@SECT15", ms.Sect15);
            insert_command.Parameters.AddWithValue("@SECT16", ms.Sect16);
            insert_command.Parameters.AddWithValue("@SECT19", ms.Sect19);
            insert_command.Parameters.AddWithValue("@SECT1T", ms.Sect1T);

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

        //delete my sector data
        public void DeleteMySectorData(string user)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.HQSector WHERE USRNME = @USRNME", sql_connection);

                    sql_command.Parameters.Add("@USRNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(user);

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
