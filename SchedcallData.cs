/**************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : CprsDAL.SchedcallData.cs	    	
Programmer      : Christine Zhang     
Creation Date   : Oct. 19 2017  
Inputs          : id, schedcall, schedhist, 
Parameters      : 
Outputs         : next id, next respid
Description     : data layer to get data for schedcall
Detailed Design : None 
Other           : Called by: frmC700, frmTFU
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
using System.Globalization;

namespace CprsDAL
{
    public class SchedCallData
    {
        /*Get sched_call Data */
        public Schedcall GetSchedCallData(string id)
        {
            Schedcall scall = new Schedcall(id);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM dbo.Sched_call WHERE ID = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    scall.Callreq = reader["CALLREQ"].ToString();
                    scall.Calltpe = reader["CALLTPE"].ToString();
                    scall.Priority = reader["PRIORITY"].ToString();
                    scall.Accesday = reader["ACCESDAY"].ToString();
                    scall.Accestms = reader["ACCESTMS"].ToString();
                    scall.Accestme = reader["ACCESTME"].ToString();
                    scall.Accescde = reader["ACCESCDE"].ToString();
                    scall.Callstat = reader["CALLSTAT"].ToString();
                    scall.Callcnt = Convert.ToInt32(reader["CALLCNT"]);
                    scall.Complete = reader["COMPLETE"].ToString();
                    scall.LVMcnt = Convert.ToInt32(reader["LVMCNT"]);
                    scall.Apptdate = reader["APPTDATE"].ToString();
                    scall.Appttime = reader["APPTTIME"].ToString();
                    scall.Apptends = reader["APPTENDS"].ToString();
                    scall.Coltec = reader["COLTEC"].ToString();
                    scall.Added = reader["ADDED"].ToString();
                    scall.PriorityDesc = GetPriorityDesc(scall.Priority);
                    scall.IsModified = false;
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

            return scall;

        }

        //Get Priority Description 
        private string GetPriorityDesc(string priority)
        {
            string desc = string.Empty;
            if (priority.Trim() != "")
            {
                switch (Convert.ToInt16(priority))
                {
                    case 1:
                        desc = "High Special Priority TC- Phone";
                        break;
                    case 2:
                        desc = "Med Special Priority TC - Phone";
                        break;
                    case 3:
                        desc = "Low Special Priority TC - Phone";
                        break;
                    case 4:
                        desc = "3+ Month Behind – Mail + Cent";
                        break;
                    case 5:
                        desc = "Abeyance - Phone";
                        break;
                    case 6:
                        desc = "Abeyance – Mail + Cent";
                        break;
                    case 7:
                        desc = "Initial DCP";
                        break;
                    case 8:
                        desc = "3+ Month Behind - Phone";
                        break;
                    case 9:
                        desc = "2 Month Behind - Phone";
                        break;
                    case 10:
                        desc = "1 Month Behind - Phone";
                        break;
                    case 11:
                        desc = "Regular Phone";
                        break;
                    case 12:
                        desc = "Initial MF";
                        break;
                    case 13:
                        desc = "2 Month Behind - Mail";
                        break;
                    case 14:
                        desc = "1 Month Behind - Mail";
                        break;
                    case 15:
                        desc = "Imputed Start";
                        break;
                    case 16:
                        desc = "High Special Priority TC - Mail";
                        break;
                    case 17:
                        desc = "Med Special Priority TC - Mail";
                        break;
                    case 18:
                        desc = "Low Special Priority TC - Mail";
                        break;
                    case 19:
                        desc = "2 Month Behind - Cent";
                        break;
                    case 20:
                        desc = "1 Month Behind - Cent";
                        break;
                    case 21:
                        desc = "Regular - Cent";
                        break;
                    case 22:
                        desc = "Regular - Mail";
                        break;
                    case 23:
                        desc = "Coltec I,A,S and Status 7";
                        break;

                    default:
                        desc = "";
                        break;
                }
            }

            return desc;
        }

        //check SchedCall record exist or not
        private bool CheckSchedcallExist(string sid)
        {
            bool s_exist = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select id from dbo.SCHED_CALL where id = " + GeneralData.AddSqlQuotes(sid);
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

            return s_exist;
        }

        //add schedcall record
        private void AddSchedcallData(Schedcall sc)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.Sched_call (ID, CALLREQ, CALLTPE, PRIORITY, ACCESDAY, ACCESTMS, ACCESTME, ACCESCDE, ACCESNME, CALLSTAT, CALLCNT, COMPLETE, LVMCNT, APPTDATE, APPTTIME, APPTENDS, COLTEC, ADDED) "
                        + "Values (@ID, @CALLREQ, @CALLTPE, @PRIOITY,@ACCESDAY, @ACCESTMS, @ACCESTME, @ACCESCDE, @ACCESNME, @CALLSTAT, @CALLCNT, @COMPLETE,@LVMCNT, @APPTDATE, @APPTTIME,@APPTENDS, @COLTEC, @ADDED)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@ID", sc.Id);
            insert_command.Parameters.AddWithValue("@CALLREQ", sc.Callreq);
            insert_command.Parameters.AddWithValue("@CALLTPE", sc.Calltpe);
            insert_command.Parameters.AddWithValue("@PRIOITY", sc.Priority);
            insert_command.Parameters.AddWithValue("@ACCESDAY", sc.Accesday);
            insert_command.Parameters.AddWithValue("@ACCESTMS", sc.Accestms);
            insert_command.Parameters.AddWithValue("@ACCESTME", sc.Accestme);
            insert_command.Parameters.AddWithValue("@ACCESCDE", sc.Accescde);
            insert_command.Parameters.AddWithValue("@ACCESNME", sc.Accesnme);
            insert_command.Parameters.AddWithValue("@CALLSTAT", sc.Callstat);
            insert_command.Parameters.AddWithValue("@CALLCNT", sc.Callcnt);
            insert_command.Parameters.AddWithValue("@COMPLETE", sc.Complete);
            insert_command.Parameters.AddWithValue("@LVMCNT", sc.LVMcnt);
            insert_command.Parameters.AddWithValue("@APPTDATE", sc.Apptdate);
            insert_command.Parameters.AddWithValue("@APPTTIME", sc.Appttime);
            insert_command.Parameters.AddWithValue("@APPTENDS", sc.Apptends);
            insert_command.Parameters.AddWithValue("@COLTEC", sc.Coltec);
            insert_command.Parameters.AddWithValue("@ADDED", sc.Added);

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

        //update schedcall record
        private bool UpdateSchedcall(Schedcall sc)
        {
            string db_table = "dbo.Sched_call";

            //search record exist or not.  if not exist, add record, otherwise update record
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                               "CALLREQ = @CALLREQ, " +
                               "CALLTPE = @CALLTPE, " +
                               "PRIORITY = @PRIORITY, " +
                               "ACCESDAY = @ACCESDAY, " +
                               "ACCESTMS = @ACCESTMS, " +
                               "ACCESTME = @ACCESTME, " +
                               "ACCESCDE = @ACCESCDE, " +
                               "ACCESNME = @ACCESNME, " +
                               "CALLSTAT = @CALLSTAT, " +
                               "CALLCNT = @CALLCNT, " +
                               "COMPLETE = @COMPLETE, " +
                               "LVMCNT = @LVMCNT, " +
                               "APPTDATE = @APPTDATE, " +
                               "APPTTIME = @APPTTIME, " +
                               "APPTENDS = @APPTENDS, " +
                               "COLTEC = @COLTEC, " +
                               "ADDED = @ADDED " +
                                "WHERE ID = @ID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@ID", sc.Id);
                update_command.Parameters.AddWithValue("@CALLREQ", sc.Callreq);
                update_command.Parameters.AddWithValue("@CALLTPE", sc.Calltpe);
                update_command.Parameters.AddWithValue("@PRIORITY", sc.Priority);
                update_command.Parameters.AddWithValue("@ACCESDAY", sc.Accesday);
                update_command.Parameters.AddWithValue("@ACCESTMS", sc.Accestms);
                update_command.Parameters.AddWithValue("@ACCESTME", sc.Accestme);
                update_command.Parameters.AddWithValue("@ACCESCDE", sc.Accescde);
                update_command.Parameters.AddWithValue("@ACCESNME", sc.Accesnme);
                update_command.Parameters.AddWithValue("@CALLSTAT", sc.Callstat);
                update_command.Parameters.AddWithValue("@CALLCNT", sc.Callcnt);
                update_command.Parameters.AddWithValue("@COMPLETE", sc.Complete);
                update_command.Parameters.AddWithValue("@LVMCNT", sc.LVMcnt);
                update_command.Parameters.AddWithValue("@APPTDATE", sc.Apptdate);
                update_command.Parameters.AddWithValue("@APPTTIME", sc.Appttime);
                update_command.Parameters.AddWithValue("@APPTENDS", sc.Apptends);
                update_command.Parameters.AddWithValue("@COLTEC", sc.Coltec);
                update_command.Parameters.AddWithValue("@ADDED", sc.Added);
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


        /*Save sched call data base on table */
        public void SaveSchedcallData(Schedcall so)
        {
            if (CheckSchedcallExist(so.Id))
                UpdateSchedcall(so);
            else
                AddSchedcallData(so);

            so.IsModified = false;

        }

        //add new sched hist record
        public void AddSchedHistData(Schedhist shist)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.Sched_HIST (ID, OWNER, CALLSTAT, ACCESDAY, ACCESTMS, ACCESTME, ACCESCDE, ACCESNME) "
                        + "Values (@ID, @OWNER, @CALLSTAT, @ACCESDAY, @ACCESTMS, @ACCESTME, @ACCESCDE, @ACCESNME)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@ID", shist.Id);
            insert_command.Parameters.AddWithValue("@OWNER", shist.Owner);
            insert_command.Parameters.AddWithValue("@CALLSTAT", shist.Callstat);
            insert_command.Parameters.AddWithValue("@ACCESDAY", shist.Accesday);
            insert_command.Parameters.AddWithValue("@ACCESTMS", shist.Accestms);
            insert_command.Parameters.AddWithValue("@ACCESTME", shist.Accestme);
            insert_command.Parameters.AddWithValue("@ACCESCDE", shist.Accescde);
            insert_command.Parameters.AddWithValue("@ACCESNME", shist.Accesnme);

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

        //get next case for interviewer
        public void GetNextCase(int workday, ref string next_respid, ref string next_projectid)
        {
            string owner_str = string.Empty;
            string priority_str = string.Empty;

            //get Npc interviewer related case
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                if (UserInfo.ContFD == "Y")
                    owner_str = "'C' , 'D' , 'F'";
                if (UserInfo.ContMF == "Y")
                {
                    if (owner_str != string.Empty)
                        owner_str = owner_str + ", 'M'";
                    else
                        owner_str = "'M'";
                }
                if (UserInfo.ContSL == "Y")
                {
                    if (owner_str != string.Empty)
                        owner_str = owner_str + ", 'S', 'L', 'P'";
                    else
                        owner_str = "'S', 'L', 'P'";
                }

                if (UserInfo.ContNR == "Y")
                {
                    if (owner_str != string.Empty)
                        owner_str = owner_str + ",'N', 'T', 'E' , 'G' , 'R' , 'O','W'";
                    else
                        owner_str = "'N', 'T', 'E' , 'G' , 'R' , 'O','W'";
                }

                if (owner_str != string.Empty)
                    owner_str = " owner in (" + owner_str + ")";
                else
                    owner_str = " owner = ''";
            }

            //get cases for priority in work day, referrl and pr
            
            if (workday < 4)
                priority_str = "  priority < 7 ";
            else if (workday >=4 && workday < 6)
                priority_str = " priority < 8 ";
            else if (workday >= 6 && workday < 9)
                priority_str = " priority < 12 ";
            else if (workday >= 9 && workday < 13)
                priority_str = " priority < 16 ";
            else if (workday >= 13 && workday < 16)
                priority_str = " priority < 22 ";

            string sql = "WITH t2 AS( ";
            sql = sql + " select  sc.id, r.respid, callstat, r.rstate, apptdate, appttime, apptends, priority, calltpe, owner ";
            sql = sql + " from dbo.sched_call sc, dbo.sample s, dbo.respondent r, dbo.master m ";
            sql = sql + " where sc.id = s.id and s.respid = r.respid and s.masterid = m.masterid and complete = 'N' ";
            sql = sql + " and callreq = 'Y' and apptdate <= (FORMAT(GetDate(), 'MM') + FORMAT(GetDate(), 'dd')) ";
            sql = sql + "  and resplock = '' and calltpe <> 'W' ";
            sql = sql + " union ";
            sql = sql + " select sc.id, r.respid, callstat, r.rstate, apptdate, appttime, apptends, priority, calltpe, owner ";
            sql = sql + " from dbo.sched_call sc, dbo.sample s, dbo.respondent r, dbo.master m ";
            sql = sql + " where sc.id = s.id and s.respid = r.respid and s.masterid = m.masterid and complete = 'N' and callreq = 'Y'";
            if (priority_str != string.Empty)
                sql = sql + " and " + priority_str;
            sql = sql + "  and calltpe = 'W' and resplock = '') ";
            sql = sql + "  select * from t2 ";
            if (owner_str != string.Empty)
                sql = sql + " where " + owner_str;
            sql = sql + " order by calltpe, apptdate, priority, appttime, ID";
            

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            //find next available cases
            if (dt.Rows.Count >0)
            {
                string todate = DateTime.Now.ToString("MMdd");
                string curtime = DateTime.Now.ToString("HHmm");

                //check appt date, appt time
                foreach (DataRow row in dt.Rows)
                {
                    string calltype = row["calltpe"].ToString();
                    string rstate = row["rstate"].ToString();
                    string apptdate = row["apptdate"].ToString();
                    string apptime = row["appttime"].ToString();
                    string apptend = row["apptends"].ToString();

                    DateTime parsetime = DateTime.ParseExact(apptime, "HHmm", System.Globalization.CultureInfo.InvariantCulture);

                    string apptime2 = parsetime.AddHours(1).ToString("HHmm");

                    //check apptdate less than today
                    if (apptdate.Trim() != "" && Convert.ToInt32(apptdate) < Convert.ToInt32(todate))
                    {
                        //find their time whether is good to call
                        if (CheckIfGoodBussinessTime(rstate))
                        {
                            if (UpdateRespIDLockForUser(row["Respid"].ToString(), UserInfo.UserName))
                            {
                                next_respid = row["Respid"].ToString();
                                next_projectid = row["ID"].ToString();
                                break;
                            }
                        }
                    }

                    //apptdate is today or apptdate is empty
                    else
                    {
                        //hard appointment
                        if (calltype == "H")
                        {
                            // check appt time, the current time must greater than apptime and less than apptime +1
                            if (Convert.ToInt32(curtime) >= Convert.ToInt32(apptime) && Convert.ToInt32(curtime) <= Convert.ToInt32(apptime2))
                            {
                                if (UpdateRespIDLockForUser(row["Respid"].ToString(), UserInfo.UserName))
                                {
                                    next_respid = row["Respid"].ToString();
                                    next_projectid = row["ID"].ToString();
                                    break;
                                }
                            }
                        }

                        //soft appointment
                        else if (calltype == "S")
                        {
                            // check appt time, the current time must greater than apptime and less than apptends
                            if (Convert.ToInt32(curtime) >= Convert.ToInt32(apptime) && Convert.ToInt32(curtime) < Convert.ToInt32(apptend))
                            {
                                if (UpdateRespIDLockForUser(row["Respid"].ToString(), UserInfo.UserName))
                                {
                                    next_respid = row["Respid"].ToString();
                                    next_projectid = row["ID"].ToString();
                                    break;
                                }
                            }
                        }
                        else
                        {
                            //without appointment, check it is good time to call or not based on timezone
                            if (CheckIfGoodBussinessTime(rstate))
                            {
                                if (UpdateRespIDLockForUser(row["Respid"].ToString(), UserInfo.UserName))
                                {
                                    next_respid = row["Respid"].ToString();
                                    next_projectid = row["ID"].ToString();
                                    break;
                                }  
                            }
                        }

                    }
                }
            }
        }

        //check timezone time, if it is from 8:00 - 17:00
        private bool CheckIfGoodBussinessTime(string rstate)
        {
            bool good_time = false;

            //get timezone current time
            string resp_time = Convert.ToDateTime(GeneralDataFuctions.GetTimezoneCurrentTime(rstate)).ToString("HH:mm");
            TimeSpan resp_now = TimeSpan.Parse(resp_time);

            TimeSpan start = new TimeSpan(8, 0, 0); //8 o'clock AM
            TimeSpan end = new TimeSpan(19, 0, 0); //7 o'clock PM

            string mytime = DateTime.Now.ToString("HH:mm");
            TimeSpan now = TimeSpan.Parse(mytime);

            if ((now > start) && (now < end) && (resp_now > start && resp_now < end))
            {
                good_time = true;
            }

            return good_time;
        }

        //lock the user when the resplock was empty
        private bool UpdateRespIDLockForUser(string Respid, string username)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = sql_connection.BeginTransaction("Transaction");

                string usql = "UPDATE dbo.Respondent SET " +
                                "RESPLOCK = @RESPLOCK " +
                                "WHERE RESPID = @RESPID and RESPLOCK = ''";
                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@RESPID", Respid);
                update_command.Parameters.AddWithValue("@RESPLOCK", username);

                update_command.Transaction = transaction;

                try
                {
                    int count = update_command.ExecuteNonQuery();
                    if (count > 0)
                    {
                        transaction.Commit();
                        return true;
                    }
                    else
                    {
                        transaction.Rollback();
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public DataTable GetSchedHistDataByID(string id)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();

                sql = "select * from dbo.sched_hist where id = " + GeneralData.AddSqlQuotes(id) + " order by accesday desc,accestme desc";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, c))
                {
                    da.Fill(dt);

                }
                c.Close();
            }

            return dt;

        }


    }

}
