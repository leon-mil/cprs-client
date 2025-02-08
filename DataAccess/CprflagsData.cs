/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CprflagsData.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/01/2015
Inputs:             id, 
Parameters:	        None 
Outputs:	         flag string
Description:	    data layer to retrieve and save flags
Detailed Design:    None 
Other:	            Called by: frmC700, frmTFU
 
Revision History:	
***************************************************************************************
 Modified Date :  10/1/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR#3591
 Description   :  Add flag 47, 48
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
    public class CprflagsData
    {
        /*Retrieve cemark data */
        public Dataflags GetCprflagsData(string id)
        {
            /* find out database table name */
            string db_table =  "dbo.DATA_FLAGS";

            Dataflags dataflags = new Dataflags();

            string flags = "";
            for (int i = 0; i < 48; i++)
            {
                flags = flags + "0";
            }
            string reportflags = "";
            for (int i = 0; i < 48; i++)
            {
                reportflags = reportflags + "0";
            }
            dataflags.currflags = flags;
            dataflags.reportflags = reportflags;
            dataflags.Id = id;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT curm_flag, report_flag FROM " + db_table + " WHERE id = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    dataflags.currflags = reader["CURM_FLAG"].ToString();
                    dataflags.reportflags = reader["REPORT_FLAG"].ToString();

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
            return dataflags;
        }

        /*Vaildate ID from table*/
        private bool CheckIdExist(string id)
        {
            bool record_found = false;

            /* get data table */
            string db_table = "dbo.DATA_FLAGS" ;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * FROM " + db_table + " where ID = " + GeneralData.AddSqlQuotes(id);
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandTimeout = 0;

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
            return record_found;
        }

        private void AddFlagData(string id, string curm_flag, string report_flag)
        {
            string db_table = "dbo.DATA_FLAGS";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (id, curm_flag, report_flag)"
                            + "Values (@id, @CURM_FLAG, @REPORT_FLAG)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.CommandTimeout = 0;

            insert_command.Parameters.AddWithValue("@id", id);
            insert_command.Parameters.AddWithValue("@CURM_FLAG", curm_flag);
            insert_command.Parameters.AddWithValue("@REPORT_FLAG", report_flag);

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

        private void UpdateFlagData(string id, string curm_flag, string report_flag)
        {
            /* find out database table name */
            string db_table = "dbo.DATA_FLAGS";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                                "CURM_FLAG = @CURM_FLAG, " + 
                                "REPORT_FLAG = @REPORT_FLAG " +
                                "WHERE ID = @id";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.CommandTimeout = 0;

                update_command.Parameters.AddWithValue("@ID", id);
                update_command.Parameters.AddWithValue("@CURM_FLAG", curm_flag);
                update_command.Parameters.AddWithValue("@REPORT_FLAG", report_flag);

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

        /*Delete flag data */
        private bool DeleteFlagData(string id)
        {
            /* find out database table name */
            string db_table = "dbo.DATA_FLAGS";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string usql = "DELETE FROM " + db_table + " WHERE ID = @ID "; ;

            SqlCommand delete_command = new SqlCommand(usql, sql_connection);
            delete_command.CommandTimeout = 0;

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


        /*Save  data flags */
        public bool SaveflagsData(string id, string fflag, string rflag)
        {
            bool saved = false;

            if (CheckIdExist(id))
            {
                UpdateFlagData(id, fflag, rflag);
            }
            else 
            {
                AddFlagData(id, fflag, rflag);
            }

            saved = true;

            return saved;

        }
    }
}
