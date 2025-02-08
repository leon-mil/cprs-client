/************************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.SpecialTCData.cs	    	

Programmer      : Diane Musachio

Creation Date   : 10/12/2017

Inputs          : 

Parameters      : owner, newtc, id, priority, tc, minval, maxval, username, prgdtm,
                   ominval, omaxval, nminval, nmaxval, action

Outputs         : None	

Description     : These classes establish the data connections and read in 
                  the data and/or modify it for special tc priorities

Detailed Design : Special Priority TC Detailed Design 

Other           : Called by: frmSpecialPriorityTC.cs
                             frmSpecTCEditPopup.cs
                             frmSpecTCAuditPopup.cs
Revision History:	
**************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
    public class SpecialTCData
    {
        /*Retrieve newtc data */
        public DataTable GetSpecialTcData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "Select OWNER, NEWTC, '0', VALMIN - 1, VALMIN, VALMAX, VALMAX + 1, '99,999,999' from dbo.SpecialTC order by OWNER, NEWTC";

                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*Check to see if Survey/TC combo exists */
        public bool CheckTcDataExists(string owner, string newtc)
        {
            bool surveyTcExists = false;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = @"Select OWNER, NEWTC from dbo.SpecialTC where OWNER = " + owner.AddSqlQuotes() + " and NEWTC = " + newtc.AddSqlQuotes();

                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);

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

            return surveyTcExists;

        }

        //edit or add special tcs
        public void UpdateSpecialTcData(string owner, string tc, int minval, int maxval, string username, DateTime prgdtm, bool edit)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string sql;

            if (edit)
            {
                sql = "update dbo.SpecialTC SET valmin = @VALMIN, valmax = @VALMAX, usrnme = @USRNME, prgdtm = @PRGDTM where owner = @OWNER and newtc = @NEWTC";
            }
            else
            {
                sql = "insert dbo.SpecialTC (owner, newtc, valmin, valmax, usrnme, prgdtm) "
                    + " Values (@OWNER, @NEWTC, @VALMIN, @VALMAX, @USRNME, @PRGDTM)";
            }

            SqlCommand sql_command = new SqlCommand(sql, sql_connection);

            sql_command.Parameters.AddWithValue("@OWNER", owner);
            sql_command.Parameters.AddWithValue("@NEWTC", tc);
            sql_command.Parameters.AddWithValue("@VALMIN", minval);
            sql_command.Parameters.AddWithValue("@VALMAX", maxval);
            sql_command.Parameters.AddWithValue("@USRNME", username);
            sql_command.Parameters.AddWithValue("@PRGDTM", prgdtm);

            try
            {
                sql_connection.Open();
                sql_command.ExecuteNonQuery();
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

        //update audit trail for special tc data
        public void UpdateSpecialTCAudit(string owner, string tc, string action, int ominval, int omaxval, int nminval, int nmaxval, string usrnme, DateTime prgdtm)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = @" insert dbo.TCAUDIT (owner, newtc, action, ovalmin, ovalmax, nvalmin, nvalmax, usrnme, prgdtm) 
                         Values (@OWNER, @NEWTC, @ACTION, @OVALMIN, @OVALMAX, @NVALMIN, @NVALMAX, @USRNME, @PRGDTM)";

            SqlCommand sql_command = new SqlCommand(sql, sql_connection);

            sql_command.Parameters.AddWithValue("@OWNER", owner);
            sql_command.Parameters.AddWithValue("@NEWTC", tc);
            sql_command.Parameters.AddWithValue("@ACTION", action);
            sql_command.Parameters.AddWithValue("@OVALMIN", ominval);
            sql_command.Parameters.AddWithValue("@OVALMAX", omaxval);
            sql_command.Parameters.AddWithValue("@NVALMIN", nminval);
            sql_command.Parameters.AddWithValue("@NVALMAX", nmaxval);
            sql_command.Parameters.AddWithValue("@USRNME", usrnme);
            sql_command.Parameters.AddWithValue("@PRGDTM", prgdtm);

            try
            {
                sql_connection.Open();
                sql_command.ExecuteNonQuery();
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

        //get data from special tc useraudit
        public DataTable GetTCAuditData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT * FROM dbo.TCAUDIT order by PRGDTM Desc";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //Delete the row from the SPECIAL TC table
        public void DeleteSpecialTCData(string owner, string newtc)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("DELETE FROM dbo.SPECIALTC WHERE OWNER = @OWNER and NEWTC = @NEWTC", sql_connection);

                    sql_command.Parameters.Add("@OWNER", SqlDbType.Char).Value = GeneralData.NullIfEmpty(owner);
                    sql_command.Parameters.Add("@NEWTC", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);

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

        //get data
        public DataTable GetSchedCall()
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"WITH t2 AS (SELECT        ID, COUNT(VIPDATA) AS num_imp
                           FROM            dbo.MONTHLY_VIP_DATA AS MONTHLY_VIP_DATA
                           WHERE        (VIPFLAG = 'M')
                           GROUP BY ID
                           UNION
                           SELECT        ID, 0 AS num_imp
                           FROM            dbo.MONTHLY_VIP_DATA AS MONTHLY_VIP_DATA
                           WHERE        (ID NOT IN (SELECT DISTINCT ID
                                                          FROM            dbo.MONTHLY_VIP_DATA AS MONTHLY_VIP_DATA
                                                          WHERE        (VIPFLAG = 'M'))))
                          SELECT        dbo.SCHED_CALL.ID, dbo.SCHED_CALL.PRIORITY,  dbo.SAMPLE.RVITM5C,  dbo.MASTER.NEWTC AS Expr1, dbo.RESPONDENT.LAG,   dbo.RESPONDENT.COLTEC, 
                              dbo.SAMPLE.FWGT, CASE WHEN dbo.MASTER.owner IN ('T', 'E', 'G', 'R', 'O', 'W') THEN 'U' WHEN dbo.MASTER.owner IN ('S', 'L', 'P') 
                              THEN 'P' WHEN dbo.MASTER.owner IN ('C', 'D', 'F') THEN 'F' WHEN dbo.MASTER.owner = 'N' THEN 'N' WHEN dbo.MASTER.owner = 'M' THEN 'M' END AS xowner,
                               SUBSTRING(dbo.MASTER.NEWTC, 1, 2) AS tc2, dbo.SAMPLE.STATUS, dbo.MASTER.OWNER AS Expr2, ISNULL(dbo.SPECIALTC.VALMIN, 0) AS VALMIN, 
                              ISNULL(dbo.SPECIALTC.VALMAX, 0) AS VALMAX, ISNULL(t2_1.num_imp, 0) AS num_imp, SPECIALTC.OWNER AS spec_owner
                           FROM            
                              dbo.SAMPLE LEFT OUTER JOIN
                              dbo.MASTER LEFT OUTER JOIN
                              dbo.SPECIALTC ON SUBSTRING(dbo.MASTER.NEWTC, 1, 2) = dbo.SPECIALTC.NEWTC ON 
                              dbo.SAMPLE.MASTERID = dbo.MASTER.MASTERID LEFT OUTER JOIN
                              dbo.RESPONDENT ON dbo.SAMPLE.RESPID = dbo.RESPONDENT.RESPID RIGHT OUTER JOIN
                              dbo.SCHED_CALL ON dbo.SAMPLE.ID = dbo.SCHED_CALL.ID LEFT OUTER JOIN
                              t2 AS t2_1 ON dbo.SCHED_CALL.ID = t2_1.ID";

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }

            return dt;
        }

        //update sched_call with new priorities
        public void UpdatePriority(string id, string priority)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "update dbo.SCHED_CALL SET PRIORITY = @PRIORITY where id = @ID";

                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                sql_command.Parameters.AddWithValue("@PRIORITY", priority);
                sql_command.Parameters.AddWithValue("@ID", id);

                try
                {
                    sql_connection.Open();
                    sql_command.ExecuteNonQuery();
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
