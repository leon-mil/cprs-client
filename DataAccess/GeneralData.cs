/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : GeneralData.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Common static functions and data functions
Change Request: 
Detailed Design: N/A
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date : Diane Musachio
Modified By   : June 8, 2017
Keyword       : None
Change Request: CR 109
Description   : Added 2 new functions - IsCanadianZipCode & IsUsZipCode
************************************************************************
Modified Date : Aug 15, 2017
Modified by   : Christine Zhang
Keyword       : None
Change Request: None
Description   : Added  routine  Checkfin
***********************************************************************
Modified Date : Apr 10, 2018
Modified by   : Christine Zhang
Keyword       : None
Change Request: None
Description   : Added routine  GetActiveProjectIds
***********************************************************************
Modified Date :  01/09/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR3839
 Description   : for Utilities check newtc and owner
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net.Mail;
using CprsBLL;

namespace CprsDAL
{
    /****** General Static Functions *******/

    public static class GeneralData
    {
        /*Get connection string from app.config file */

        public static string getConnectionString()
        {
            string conn_str = string.Empty;
            if (GlobalVars.Databasename == "CPRSDEV")
            {
                conn_str = ConfigurationManager.ConnectionStrings["Cprs.Properties.Settings.DBDev"].ConnectionString;
            }
            else if (GlobalVars.Databasename == "CPRSTEST")
            {
                conn_str = ConfigurationManager.ConnectionStrings["Cprs.Properties.Settings.DBTest"].ConnectionString;
            }
            else
                conn_str = ConfigurationManager.ConnectionStrings["Cprs.Properties.Settings.DBProd"].ConnectionString;

            return conn_str;
        }

        // Indicates whether NPC access restrictions are enabled
        public static bool IsNpcAccessControlEnabled()
        {
            string setting = ConfigurationManager.AppSettings["NpcAccessControlEnabled"];
            if (!bool.TryParse(setting, out bool enabled))
            {
                enabled = false;
            }
            return enabled;
        }

        // Returns the time of day NPC users may begin using the application
        public static TimeSpan GetNpcAccessStartTime()
        {
            string setting = ConfigurationManager.AppSettings["NpcAccessStartTime"];
            if (!TimeSpan.TryParse(setting, out TimeSpan start))
            {
                start = new TimeSpan(8, 0, 0);
            }
            return start;
        }

        /* convert empty string to null */

        public static string NullIfEmpty(string value)
        {
            return string.IsNullOrEmpty(value.Trim()) ? null : value.Trim();
        }

        /* Put a string between double quotes */

        public static string AddSqlQuotes(this string value)
        {
            return "\'" + value + "\'";
        }

        /* Reads string from end of string */

        public static string Right(this string value, int length)
        {
            return value.Substring(value.Length - length);
        }

       
        /* Validates if web address is in correct format */
        public static bool IsValidURL(string weburl)
        {
            //bool result = Regex.IsMatch(weburl, @"(HTTP|HTTPS)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?");

            bool result = Regex.IsMatch(weburl, @"((HT|F)TP(S?)\:\/\/|~/|/)?([\w]+:\w+@)?([a-zA-Z]{1}([\w\-]+\.)+([\w]{2,5}))(:[\d]{1,5})?((/?\w+/)+|/?)(\w+\.[\w]{3,4})?((\?\w+=\w+)?(&\w+=\w+)*)?");
            return result;
        }

        /* Validates if value is numeric */

        public static bool IsNumeric(string name)
        {
            if (name == "[0-9]*")
            {
                return true;
            }
            else
                return false;
        }

        /* Validates zipcode is correct format for us or canada */

        public static bool IsUsorCanadianZipCode(string zipCode)
        {
            string pattern = @"^\d{5}(-\d{4})?$|(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} \d{1}[A-Z]{1}\d{1})$";

            Regex uscanZipRegEx = new Regex(pattern, RegexOptions.IgnoreCase);

            return uscanZipRegEx.IsMatch(zipCode);
        }

        /* Validates zipcode is correct format for canada */

        public static bool IsCanadianZipCode(string zipCode)
        {
            string pattern = @"(^[ABCEGHJKLMNPRSTVXY]{1}\d{1}[A-Z]{1} \d{1}[A-Z]{1}\d{1})$";

            Regex canadaZipRegEx = new Regex(pattern, RegexOptions.IgnoreCase);

            return canadaZipRegEx.IsMatch(zipCode);
        }

        /* Validates zipcode is correct format for us */

        public static bool IsUsZipCode(string zipCode)
        {
            string pattern = @"^\d{5}(-\d{4})?$";

            Regex usZipRegEx = new Regex(pattern, RegexOptions.IgnoreCase);

            return usZipRegEx.IsMatch(zipCode);
        }
    }

    /******General Data Functions used on many forms *******/

    public class GeneralDataFuctions
    {
        //Check Database connection return 0, connect successfully. 
        //return 1, user account was disabled.  return 2, sever was unavalible
        public static int CheckDatabaseConnection()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(GeneralData.getConnectionString()))
                {
                    conn.Open();
                }
            }

            catch (SqlException)
            {
                return 1;
            }
            catch (InvalidOperationException)
            {
                return 2;
            }

            return 0;
        }

        /* Get fipstate data for fipstate select combobox used in search forms */

        public static DataTable GetFipStateDataForCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_FipstateCode", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr[0] = -1;
                dr[1] = "";
                dt.Rows.InsertAt(dr, 0);
            }

            return dt;
        }

        //Method used to check that the sample id entered on the Next Initial popup exists in DCPINITIAL table
        public static bool ValidateDCPInitialId(string id)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT ID from dbo.DCPINITIAL where ID = " + GeneralData.AddSqlQuotes(id);
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
            return record_found;
        }

        /* validate rstate for US or canadian province */
        public static bool CheckValidRstate(string rstate, bool isCanada)
        {
            if (!String.IsNullOrEmpty(rstate))
            {
                try
                {
                    SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

                    string Query;
                    if (isCanada)
                        Query = "SELECT STABBR, STLOC FROM dbo.STATELIST WHERE STLOC = '1' AND STABBR = " + GeneralData.AddSqlQuotes(rstate);
                    else
                        Query = "SELECT STABBR, STLOC FROM dbo.STATELIST WHERE STLOC = '0' AND STABBR = " + GeneralData.AddSqlQuotes(rstate);

                    SqlCommand sql_command = new SqlCommand(Query, connection);

                    connection.Open();

                    using (SqlDataReader reader = sql_command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                return true;
                            }
                        }

                        reader.Close();
                        connection.Close();
                    }

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return false;

        }

        public static int GetTimezoneFactor(string statebbr)
        {
            int tf = -1;
            string currenttime = string.Empty;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "SELECT TIMEFACT FROM dbo.STATELIST WHERE STABBR = " + GeneralData.AddSqlQuotes(statebbr);

                SqlCommand sql_command = new SqlCommand(Query, connection);
                connection.Open();
                using (SqlDataReader reader = sql_command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tf = (int)reader["TIMEFACT"];
                        }
                    }
                    reader.Close();
                }
            }

            return tf;

        }

        //Get  current time for a time zone
        public static string GetTimezoneCurrentTime(string statebbr)
        {
            int tf = -1;
            string currenttime = string.Empty;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "SELECT TIMEFACT FROM dbo.STATELIST WHERE STABBR = " + GeneralData.AddSqlQuotes(statebbr);

                SqlCommand sql_command = new SqlCommand(Query, connection);
                connection.Open();
                using (SqlDataReader reader = sql_command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tf = (int)reader["TIMEFACT"];
                        }
                    }
                    reader.Close();
                }


                currenttime = DateTime.Now.AddHours(-tf).ToString();
            }

            return currenttime;

        }

        //From STABBR get timezone name
        public static string GetTimezone(string statebbr)
        {
            int tf = -1;
            string timezonename = string.Empty;

            if (statebbr == "")
                return "";

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "SELECT TIMEFACT FROM dbo.STATELIST WHERE STABBR = " + GeneralData.AddSqlQuotes(statebbr);

                SqlCommand sql_command = new SqlCommand(Query, connection);
                connection.Open();
                using (SqlDataReader reader = sql_command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            tf = (int)reader["TIMEFACT"];
                        }
                    }
                    reader.Close();
                }

                timezonename = GetTimeZoneName(tf);
            }

            return timezonename;
        }

        //from timefact get time zone name
        private static string GetTimeZoneName(int timefact)
        {
            switch (timefact)
            {
                case 0:
                    return "EASTERN";
                case 1:
                    return "CENTRAL";
                case 2:
                    return "MOUNTAIN";
                case 3:
                    return "PACIFIC";
                case 4:
                    return "ALASKAN";
                case 6:
                    return "HAWAIIAN";
                case -1:
                    return "ATLANTIC";
                case -2:
                    return "NEW FOUNDLAND";

                default:
                    return "";
            }

        }

        //When owner is T newtc isn't 1000, E, G, R newtc isn't 0925,1900, O W, tc should be 11, 16
        public static bool CheckUtilitiesNewTCOwner(string newtcval, string owner)
        {
            if (owner == "E" || owner == "G" || owner == "O" || owner == "W")
            {
                if (newtcval.Substring(0, 2) != "11" && newtcval.Substring(0, 2) != "16")
                    return false;
            }
            else if ((owner == "T") && (newtcval.Substring(0, 2) != "10"))
                return false;
            else if ((owner == "R") && (newtcval != "0925" && (newtcval.Substring(0, 2) != "19")))
                return false;

           return true;
        }

        //When a NewTC is entered manually its validated against the NEWTCLIST table.
        public static bool CheckNewTC(string newtcval, string owner = "*")
        {
            if (newtcval.Length != 4)
                return false;

            if (owner == "N" && newtcval == "0000")
                 return false;

            if (owner == "M")
            {
              //if (newtcval == "0021" || newtcval == "0022" || newtcval == "0023" || newtcval == "0029")   CR 260 & 262, Only 0021 is valid for MF
                if (newtcval == "0021" || newtcval == "0029")
                        return true;
                else
                    return false;
            }

            if( (newtcval.Substring(0,2) == "16" || newtcval.Substring(0, 2) == "19") && owner != "*")
            {
                if (owner != "T" && owner != "E" && owner != "G" && owner != "R" && owner != "O" && owner != "W")
                    return false;
            }
            
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqls = "";

                sqls = "Select NEWTC from dbo.Newtclist where NEWTC = " + GeneralData.AddSqlQuotes(newtcval);

                SqlCommand command = new SqlCommand(sqls, connection);

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
            return false;


        }

        /* Validate Fin in Master table */
        public static bool CheckFin(string fin)
        {
            bool FinExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select fin from dbo.Master where fin = " + GeneralData.AddSqlQuotes(fin);
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
            return FinExist;
        }

        /* validate respid in respondent table */

        //Check if a RespID exists!

        public static bool ChkRespid(string respid)
        {
            bool RespIdExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select respid from dbo.RESPONDENT where respid = " + GeneralData.AddSqlQuotes(respid);
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


        //Check if a True RespID exists!

        public static bool ChkTrueRespid(string respid)
        {
            bool TrueRespIdExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select respid from dbo.RESPONDENT where respid = " + GeneralData.AddSqlQuotes(respid) +
                    " AND (SUBSTRING(RESPID, 1, 1) <> '4')";
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
            return TrueRespIdExist;
        }

        /*Get lock field */

        public static string ChkRespIDIsLocked(string respid)
        {
            /* find out database table name */
            string db_table = "dbo.Respondent";
           
            string lock_me = String.Empty;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            

            string sql = "SELECT * FROM " + db_table + " WHERE respid = " + GeneralData.AddSqlQuotes(respid);
            SqlCommand command = new SqlCommand(sql, connection);
            
            try
            {
                connection.Open();
   
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
               
                if (reader.Read())
                {
                    lock_me = reader["RESPLOCK"].ToString().Trim();

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

            if (lock_me != String.Empty)
                return lock_me;
            else
                return String.Empty;
        }

        /*Update lock field */

        public static bool UpdateRespIDLock(string Respid, string username)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                SqlTransaction transaction;

                // Start a local transaction.
                transaction = sql_connection.BeginTransaction("Transaction");

                string usql = "UPDATE dbo.Respondent SET " +
                                "RESPLOCK = @RESPLOCK " +
                                "WHERE RESPID = @RESPID";
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


        //Method used to that the sample id entered on the parent popup exists
        public static bool ValidateSampleId(string sampleid)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT ID from dbo.SAMPLE where ID = " + GeneralData.AddSqlQuotes(sampleid);
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
            return record_found;
        }

        //Get state two chars from code
        public static string GetFipState(string Fipcode)
        {
            string state = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM dbo.FIPSRECODE WHERE FIPSTATE = " + GeneralData.AddSqlQuotes(Fipcode);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    state = reader["STATE"].ToString();
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

            return state;

        }

        //Get state two chars from code
        public static string GetTCDesciption(string newtc)
        {
            string desc = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT TCDESCRIPTION FROM dbo.Pubtclist WHERE newtc = " + GeneralData.AddSqlQuotes(newtc);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    desc = reader["TCDESCRIPTION"].ToString();
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

            return desc;

        }

        //check master id in dodge slip table
        public static bool CheckDodgeSlip(int MasterId)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT ID FROM dbo.DCPSLIPS WHERE MASTERID = " + MasterId;
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        return true;
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return false;
        }

        //add a record to cpraccess table
        public static void AddCpraccessData(string module, string action)
        {
            /* find out database table name */
            string db_table = "dbo.cpraccess";

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert " + db_table + " (STATP, MODULE, ACTION, USRNME, PRGDTM) "
                        + "Values (@STATP, @MODULE, @ACTION, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);

            insert_command.Parameters.AddWithValue("@STATP", DateTime.Now.ToString("yyyyMM"));
            insert_command.Parameters.AddWithValue("@MODULE", module);
            insert_command.Parameters.AddWithValue("@ACTION", action);
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


        /*Update Current users data */
        public static bool UpdateCurrentUsersData(string module)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.CURRENT_USERS SET " +
                                "MODULE = @MODULE " +
                                "WHERE USRNME = " + GeneralData.AddSqlQuotes(UserInfo.UserName) +
                                " AND SESSION = " + GlobalVars.Session;

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                update_command.Parameters.AddWithValue("@SESSION", GlobalVars.Session);
                update_command.Parameters.AddWithValue("@MODULE", module);

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

        //base on jobflag, get related email list
        public static List<string> GetJobEmails(string jobflag)
        {
            List<string> tolist = new List<string>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT email FROM dbo.CCMAIL WHERE JOBFLAG = " + GeneralData.AddSqlQuotes(jobflag);
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string email = reader["EMAIL"].ToString();
                        tolist.Add(email);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return tolist;

        }

        public static string GetCurrMonthDateinTable()
        {
            string sdate = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Top 1 sdate FROM dbo.vipsadj";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    sdate = reader["SDATE"].ToString();
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

            return sdate;
        }

        public static string GetMaxMonthDateinAnnTable()
        {
            string sdate = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "select max(date6) as sdate from dbo.SAATOT";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    sdate = reader["SDATE"].ToString();
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

            return sdate;
        }

        /*get count of users in current_users */
        public static int CheckCurrentUsers()
        {
            int rowCount = 0;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = @"Select * from dbo.CURRENT_USERS";

                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            rowCount++;
                        }
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

            return rowCount;
        }

        //get active project id list for a respid
        public static List<string> GetActiveProjectIds(string respid)
        {
            List<string> tolist = new List<string>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select s.ID from dbo.sample s, dbo.master m";

                sql = sql + " WHERE s.masterid = m.masterid and RESPID = " + GeneralData.AddSqlQuotes(respid) + " and status ='1' and compdate = '' order by ID";
                SqlCommand command = new SqlCommand(sql, connection);

                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string id = reader["ID"].ToString();
                        tolist.Add(id);
                    }
                    reader.Close();
                }
                connection.Close();
            }
            return tolist;

        }

        /* Validate survey date data exist in table */
        public static bool CheckSurveyMonthDataExist(string sdate)
        {
            bool FinExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select date6 from dbo.SAATOT where date6 = " + GeneralData.AddSqlQuotes(sdate);
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
            return FinExist;
        }

        //check timezone time, if it is from 8:00 - 17:00
        public static bool CheckIfGoodBussinessTime(string rstate)
        {
            bool good_time = false;

            //get timezone current time
            string resp_time = Convert.ToDateTime(GeneralDataFuctions.GetTimezoneCurrentTime(rstate)).ToString("HH:mm");
            TimeSpan resp_now = TimeSpan.Parse(resp_time);

            TimeSpan start = new TimeSpan(8, 0, 0); //8 o'clock AM
            TimeSpan end = new TimeSpan(19, 0, 0); //7 o'clock PM

            string mytime = DateTime.Now.ToString("HH:mm");
            TimeSpan now = TimeSpan.Parse(mytime);

            if ((now >= start) && (now < end) && (resp_now >= start && resp_now < end))
            {
                good_time = true;
            }

            return good_time;
        }

        //lock the user when the resplock was empty
        public static bool UpdateRespIDLockForUser(string Respid, string username)
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

    }
}
 