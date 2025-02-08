/**************************************************************************************
Econ App Name:      Construction Progress Report Survey(CPRS)
Project Name:       Construction Progress Report Survey(CPRS)
Program Name:       CprsDAL.NPCSpecialData.cs	    	
Programmer:         Christine Zhang
Creation Date:      1/21/2020
Inputs:             None
Parameters:	        None 
Outputs:	        NPC Special data	
Description:	    Get NPC Special data 
Detailed Design:    Detailed Design for NPC Specials
Other:	            Called by: frmNPCSpecials, frmNPCSpecialsPopup.cs
 
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
    public class NPCSpecialsData
    {
        /*Retrieve NPC Special data */
        public DataTable GetNPCSpecialData(string type)
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "";

            if (type == "CR")
                sql = "SELECT USERNAME, n.RESPID, COLTEC = CASE WHEN COLTEC2 <> ''  THEN COLTEC1 + '/' +COLTEC2 else COLTEC1 end,CSTATUS1,CSTATUS2,CSTATUS3,CSTATUS4,CSTATUS5,RESPORG FROM dbo.NPC_SPECIALS n, dbo.Respondent r where n.respid = r.respid order by username, respid";
            else
                sql = "SELECT USERNAME,n.RESPID,COLTEC = CASE WHEN COLTEC2 <> ''  THEN COLTEC1 + '/' +COLTEC2 else COLTEC1 end,RSTATUS1,RSTATUS2,RSTATUS3,RSTATUS4,RSTATUS5,RSTATUS6,RESPORG FROM dbo.NPC_SPECIALS n, dbo.Respondent r where n.respid = r.respid order by username, respid";
           
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                SqlDataAdapter ds = new SqlDataAdapter(cmd);
                ds.Fill(dt);
            }

            return dt;
        }

        public NPCSpecial GetNPCSpecial(string respid)
        {
            NPCSpecial npc = new NPCSpecial(respid);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT USERNAME,n.RESPID,COLTEC = CASE WHEN COLTEC2 <> ''  THEN COLTEC1 + '/' +COLTEC2 else COLTEC1 end,CSTATUS1,CSTATUS2,CSTATUS3,CSTATUS4,CSTATUS5, RSTATUS1,RSTATUS2,RSTATUS3,RSTATUS4,RSTATUS5,RSTATUS6, Resporg FROM dbo.NPC_SPECIALS n, dbo.Respondent r where n.respid = " + respid + " and n.respid = r.respid" ;

            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    npc.Username = reader["USERNAME"].ToString();
                    npc.Coltec = reader["COLTEC"].ToString();
                    npc.Cstatus1 = reader["CStatus1"].ToString();
                    npc.Cstatus2 = reader["CStatus2"].ToString();
                    npc.Cstatus3 = reader["CStatus3"].ToString();
                    npc.Cstatus4 = reader["CStatus4"].ToString();
                    npc.Cstatus5 = reader["CStatus5"].ToString();
                    npc.Rstatus1 = reader["RStatus1"].ToString();
                    npc.Rstatus2 = reader["RStatus2"].ToString();
                    npc.Rstatus3 = reader["RStatus3"].ToString();
                    npc.Rstatus4 = reader["RStatus4"].ToString();
                    npc.Rstatus5 = reader["RStatus5"].ToString();
                    npc.Rstatus6 = reader["RStatus6"].ToString();
                    npc.Resporg = reader["Resporg"].ToString();
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
            return npc;
        }

        /*Get latest update */
        public string GetLatestUpdate()
        {
            string latest_update = string.Empty;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select Convert(varchar, max(prgdtm),101) as lastdate from dbo.NPC_SPECIALS";
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            latest_update = reader["lastdate"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
            return latest_update;
        }

        public DataTable GetNPCUserList()
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();

                sql = "Select usrnme From dbo.sched_id where grpcde in ('3', '4', '5') order by usrnme ";
                   

                using (SqlDataAdapter da = new SqlDataAdapter(sql, c))
                {
                    da.Fill(dt);

                }
                c.Close();
            }

            return dt;
        }

        public bool CheckRespidExist(string respid)
        {
            bool RespIdExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select distinct respid from dbo.NPC_SPECIALS where respid = " + GeneralData.AddSqlQuotes(respid);
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
            return RespIdExist;
        }

        public void AddNPCSpecialsData(NPCSpecial npc)
        {
            /* find out database table name */
            string db_table = "dbo.NPC_SPECIALS";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (RESPID, USERNAME,COLTEC1,COLTEC2,CSTATUS1,CSTATUS2,CSTATUS3,CSTATUS4, CSTATUS5,RSTATUS1,RSTATUS2,RSTATUS3,RSTATUS4,RSTATUS5,RSTATUS6,INSTTEXT,USRNME,PRGDTM ) "
                        + "Values (@RESPID, @USERNAME, @COLTEC1, @COLTEC2, @CSTATUS1, @CSTATUS2, @CSTATUS3, @CSTATUS4, @CSTATUS5, @RSTATUS1,@RSTATUS2,@RSTATUS3,@RSTATUS4,@RSTATUS5,@RSTATUS6, @INSTTEXT, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            string coltec1 = string.Empty;
            string coltec2 = string.Empty;
            if (npc.Coltec.Length > 1)
            {
                coltec1 = npc.Coltec.Substring(0, 1);
                coltec2 = npc.Coltec.Substring(2, 1);
            }
            else
                coltec1 = npc.Coltec.Substring(0, 1);

            insert_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(npc.Respid);
            insert_command.Parameters.AddWithValue("@USERNAME", npc.Username );
            insert_command.Parameters.AddWithValue("@COLTEC1", coltec1);
            insert_command.Parameters.AddWithValue("@COLTEC2", coltec2);
            insert_command.Parameters.AddWithValue("@CSTATUS1", npc.Cstatus1);
            insert_command.Parameters.AddWithValue("@CSTATUS2", npc.Cstatus2);
            insert_command.Parameters.AddWithValue("@CSTATUS3", npc.Cstatus3);
            insert_command.Parameters.AddWithValue("@CSTATUS4", npc.Cstatus4);
            insert_command.Parameters.AddWithValue("@CSTATUS5", npc.Cstatus5);
            insert_command.Parameters.AddWithValue("@RSTATUS1", npc.Rstatus1);
            insert_command.Parameters.AddWithValue("@RSTATUS2", npc.Rstatus2);
            insert_command.Parameters.AddWithValue("@RSTATUS3", npc.Rstatus3);
            insert_command.Parameters.AddWithValue("@RSTATUS4", npc.Rstatus4);
            insert_command.Parameters.AddWithValue("@RSTATUS5", npc.Rstatus5);
            insert_command.Parameters.AddWithValue("@RSTATUS6", npc.Rstatus6);
            insert_command.Parameters.AddWithValue("@INSTTEXT", "");
            insert_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
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

        public bool UpdateNPCSpecialsData(NPCSpecial npc)
        {
            /* find out database table name */
            string db_table = "dbo.NPC_SPECIALS";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                                "USERNAME = @USERNAME," +
                                "COLTEC1 = @COLTEC1, " +
                                "COLTEC2 = @COLTEC2, " +
                                "CSTATUS1 = @CSTATUS1," +
                                "CSTATUS2 = @CSTATUS2," +
                                "CSTATUS3 = @CSTATUS3," +
                                "CSTATUS4 = @CSTATUS4," +
                                "CSTATUS5 = @CSTATUS5," +
                                "RSTATUS1 = @RSTATUS1," +
                                "RSTATUS2 = @RSTATUS2," +
                                "RSTATUS3 = @RSTATUS3," +
                                "RSTATUS4 = @RSTATUS4," +
                                "RSTATUS5 = @RSTATUS5," +
                                "RSTATUS6 = @RSTATUS6," +
                                "USRNME = @USRNME," +
                                "PRGDTM = @PRGDTM  " +
                                "WHERE RESPID = @RESPID";

                string coltec1 = string.Empty;
                string coltec2 = string.Empty;
                if (npc.Coltec.Length >1)
                {
                    coltec1 = npc.Coltec.Substring(0, 1);
                    coltec2 = npc.Coltec.Substring(2, 1);
                }
                else
                    coltec1 = npc.Coltec.Substring(0, 1);

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@RESPID", npc.Respid);
                update_command.Parameters.AddWithValue("@USERNAME", npc.Username);
                update_command.Parameters.AddWithValue("@COLTEC1", coltec1);
                update_command.Parameters.AddWithValue("@COLTEC2", coltec2);
                update_command.Parameters.AddWithValue("@CSTATUS1", npc.Cstatus1);
                update_command.Parameters.AddWithValue("@CSTATUS2", npc.Cstatus2);
                update_command.Parameters.AddWithValue("@CSTATUS3", npc.Cstatus3);
                update_command.Parameters.AddWithValue("@CSTATUS4", npc.Cstatus4);
                update_command.Parameters.AddWithValue("@CSTATUS5", npc.Cstatus5);
                update_command.Parameters.AddWithValue("@RSTATUS1", npc.Rstatus1);
                update_command.Parameters.AddWithValue("@RSTATUS2", npc.Rstatus2);
                update_command.Parameters.AddWithValue("@RSTATUS3", npc.Rstatus3);
                update_command.Parameters.AddWithValue("@RSTATUS4", npc.Rstatus4);
                update_command.Parameters.AddWithValue("@RSTATUS5", npc.Rstatus5);
                update_command.Parameters.AddWithValue("@RSTATUS6", npc.Rstatus6);
                update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                update_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

                try
                {
                    sql_connection.Open();
                    int count = update_command.ExecuteNonQuery();
                    if (count > 0)
                        return true;
                    else
                        return false;
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

        public string RetrieveNPCSpecialInstr(string respid)
        {
            string instr_str = string.Empty;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select INSTTEXT from dbo.NPC_SPECIALS where respid = " + GeneralData.AddSqlQuotes(respid);
                SqlCommand command = new SqlCommand(sql, connection);
                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            instr_str = reader["INSTTEXT"].ToString();
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return instr_str;
        }

        public bool UpdateNPCSpecialsInstr(string respid, string insttext)
        {
            /* find out database table name */
            string db_table = "dbo.NPC_SPECIALS";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                                "INSTTEXT = @INSTTEXT, " +
                                 "USRNME = @USRNME," +
                                "PRGDTM = @PRGDTM " +
                                "WHERE RESPID = @RESPID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@RESPID", respid);
                update_command.Parameters.AddWithValue("@INSTTEXT", insttext);
                update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                update_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);
                try
                {
                    sql_connection.Open();
                    int count = update_command.ExecuteNonQuery();
                    if (count > 0)
                        return true;
                    else
                        return false;
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

        public bool DeleteNPCSpecialsData(string respid)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string usql = "DELETE FROM dbo.NPC_SPECIALS WHERE respid = " + respid;

            SqlCommand delete_command = new SqlCommand(usql, sql_connection);

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
