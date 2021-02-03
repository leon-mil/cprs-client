/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.MfInitialData.cs	    	

Programmer:         Cestine Gill

Creation Date:      04/07/2016

Inputs:             Masterid, Respid, psu, place, sched
                    VIEW: GETNEXTCASE
 * 
Parameters:	        None 

Outputs:	        Multi Family Name and Address presample and 
 *                  respondent data	

Description:	    This function establishes the data connection and reads in 
                    the data, based on the masterid to populate the multifamily
 *                  address screen

Detailed Design:    Multi Family Initial Address Detailed Design 

Other:	            Called by: frmMfInitial.cs
 
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
    public class MfInitialData
    {
        //Obtain the PreSample data

        public Presample GetPresample(string id)
        {
            Presample mfinitial = new Presample();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * from dbo.PRESAMPLE where ID = @ID";

                SqlCommand command = new SqlCommand(sql, sql_connection);

                command.Parameters.AddWithValue("@ID", id);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader =
                            command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        mfinitial.Id = id;
                        mfinitial.Masterid = (int)reader["MASTERID"];
                        mfinitial.Psu = reader["Psu"].ToString();
                        mfinitial.Place = reader["Place"].ToString();
                        mfinitial.Bpoid = reader["BPOID"].ToString();
                        mfinitial.Sched = reader["Sched"].ToString();
                        mfinitial.Status = reader["Status"].ToString();
                        mfinitial.Seldate = reader["Seldate"].ToString();
                        mfinitial.Newtc = reader["Newtc"].ToString();
                        mfinitial.Fipstate = reader["Fipstate"].ToString();
                        mfinitial.Strtdate = reader["Strtdate"].ToString();

                        mfinitial.Frcde = reader["Frcde"].ToString();
                        mfinitial.Respid = reader["Respid"].ToString();
                        mfinitial.Resporg = reader["Resporg"].ToString().Trim();
                        mfinitial.Respname = reader["Respname"].ToString().Trim();
                        mfinitial.Respname2 = reader["Respname2"].ToString().Trim();
                        mfinitial.Respnote = reader["Respnote"].ToString().Trim();
                        mfinitial.Factoff = reader["Factoff"].ToString().Trim();
                        mfinitial.Othrresp = reader["Othrresp"].ToString().Trim();
                        mfinitial.Addr1 = reader["Addr1"].ToString().Trim();
                        mfinitial.Addr2 = reader["Addr2"].ToString().Trim();
                        mfinitial.Addr3 = reader["Addr3"].ToString().Trim();
                        mfinitial.Zip = reader["Zip"].ToString().Trim();
                        mfinitial.Phone = reader["Phone"].ToString().Trim();
                        mfinitial.Phone2 = reader["Phone2"].ToString().Trim();
                        mfinitial.Ext = reader["Ext"].ToString().Trim();
                        mfinitial.Ext2 = reader["Ext2"].ToString().Trim();
                        mfinitial.Fax = reader["Fax"].ToString().Trim();
                        mfinitial.Email = reader["Email"].ToString().Trim();
                        mfinitial.Weburl = reader["Weburl"].ToString().Trim();
                        mfinitial.ProjDesc = reader["Projdesc"].ToString().Trim();
                        mfinitial.Projloc = reader["Projloc"].ToString().Trim();
                        mfinitial.PCitySt = reader["Pcityst"].ToString().Trim();
                        mfinitial.Commtext = reader["Commtext"].ToString().Trim();

                        mfinitial.Commdate = reader["Commdate"].ToString();
                        mfinitial.Commtime = reader["Commtime"].ToString(); 
                        mfinitial.Usrnme = reader["Usrnme"].ToString();

                        mfinitial.RespLock = reader["Resplock"].ToString();
                        mfinitial.Worked = reader["Worked"].ToString().Trim();
                        mfinitial.Rev1nme = reader["Rev1nme"].ToString();
                        mfinitial.Rev2nme = reader["Rev2nme"].ToString();
                        mfinitial.Dupmaster = (int)reader["Dupmaster"];
                        mfinitial.Dupflag = reader["Dupflag"].ToString();

                        if (reader["Bldgs"] != DBNull.Value)
                        {
                            mfinitial.Bldgs = reader["Bldgs"].ToString();
                        }
                        else
                        {
                            mfinitial.Bldgs = (0).ToString();
                        }

                        if (reader["Units"] != DBNull.Value)
                        {
                            mfinitial.Units = reader["Units"].ToString();
                        }
                        else
                        {
                            mfinitial.Units = (0).ToString();
                        }
                        if (reader["Rbldgs"] != DBNull.Value)
                        {
                            mfinitial.Rbldgs = reader["Rbldgs"].ToString();
                        }
                        else
                        {
                            mfinitial.Rbldgs = (0).ToString();
                        }

                        if (reader["Runits"] != DBNull.Value)
                        {
                            mfinitial.Runits = reader["Runits"].ToString();
                        }
                        else
                        {
                            mfinitial.Runits = (0).ToString();
                        }

                        mfinitial.OResporg = reader["OResporg"].ToString().Trim();
                        mfinitial.ORespname = reader["ORespname"].ToString().Trim();
                        mfinitial.ORespname2 = reader["ORespname2"].ToString().Trim();
                        mfinitial.ORespnote = reader["ORespnote"].ToString().Trim();
                        mfinitial.OFactoff = reader["OFactoff"].ToString().Trim();
                        mfinitial.OOthrresp = reader["OOthrresp"].ToString().Trim();
                        mfinitial.OAddr1 = reader["OAddr1"].ToString().Trim();
                        mfinitial.OAddr2 = reader["OAddr2"].ToString().Trim();
                        mfinitial.OAddr3 = reader["OAddr3"].ToString().Trim();
                        mfinitial.OZip = reader["OZip"].ToString().Trim();
                        mfinitial.OPhone = reader["OPhone"].ToString().Trim();
                        mfinitial.OPhone2 = reader["OPhone2"].ToString().Trim();
                        mfinitial.OExt = reader["OExt"].ToString().Trim();
                        mfinitial.OExt2 = reader["OExt2"].ToString().Trim();
                        mfinitial.OFax = reader["OFax"].ToString().Trim();
                        mfinitial.OEmail = reader["OEmail"].ToString().Trim();
                        mfinitial.OWeburl = reader["OWeburl"].ToString().Trim();
                    }
                    else
                    {
                        mfinitial = null;
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close();
                }

                mfinitial.Fipstater = GeneralDataFuctions.GetFipState(mfinitial.Fipstate);

                return mfinitial;
            }
        }

        /*Validate FIN */
        public bool CheckValidId(string psu, string bpoid, string id)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT PSU, BPOID, ID from dbo.PRESAMPLE where psu = " + GeneralData.AddSqlQuotes(psu) +
                    " and BPOID =  " + GeneralData.AddSqlQuotes(bpoid) +
                    " and ID =  " + GeneralData.AddSqlQuotes(id);
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

        
        /*Validate FIN */

        public bool CheckPresampleIdExist(string id)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT MASTERID from dbo.PRESAMPLE where ID = @ID";

                SqlCommand command = new SqlCommand(sql, connection);

                command.Parameters.AddWithValue("@ID", id);

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

        //Updates the Presample History Table with any case accessed by the user

        public void AddPsampHistRecs(string id, string psu, string place, string bpoid, string sched, string usrnme, string accesday, string accestms, string accestme)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.PSAMP_HIST(ID, PSU, PLACE, BPOID, SCHED, ACCESDAY, ACCESTMS, ACCESTME, ACCESNME) VALUES (@ID, @PSU, @PLACE, @BPOID, @SCHED, @ACCESDAY, @ACCESTMS, @ACCESTME, @ACCESNME )", sql_connection);

                    sql_command.Parameters.AddWithValue("@ID", id);
                    sql_command.Parameters.AddWithValue("@PSU", psu);
                    sql_command.Parameters.AddWithValue("@PLACE", place);
                    sql_command.Parameters.AddWithValue("@BPOID", bpoid);
                    sql_command.Parameters.AddWithValue("@SCHED", sched);
                    sql_command.Parameters.AddWithValue("@ACCESDAY", accesday);
                    sql_command.Parameters.AddWithValue("@ACCESTMS", accestms);
                    sql_command.Parameters.AddWithValue("@ACCESTME", accestme);
                    sql_command.Parameters.AddWithValue("@ACCESNME", usrnme);

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

        /*Get Presample locked by */

        public string GetPresampleLockedBy(string id)
        {
            string presamplocked_by = String.Empty;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            string sql = "SELECT RESPLOCK FROM dbo.PRESAMPLE WHERE ID = " + id;
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    presamplocked_by = reader["RESPLOCK"].ToString().Trim();

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

            return presamplocked_by;
        }

        public void UpdatePresampLock(string psu, string bpoid, string locked_by)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.Presample SET RESPLOCK = @RESPLOCK WHERE [PSU] = @PSU and [BPOID] = @bpoid", sql_connection);

                    sql_command.Parameters.AddWithValue("@PSU", psu);
                    sql_command.Parameters.AddWithValue("@BPOID", bpoid);
                    sql_command.Parameters.AddWithValue("@RESPLOCK", SqlDbType.Char).Value = locked_by;

                    //Open the connection.
                    sql_connection.Open();

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

        //If user enters 0000000 and the respid is not blank restore the respid
        //field to blank in the presample table and unlock the respid in the 
        //respondent table

        public void BreakRespLink(string respid, string id)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.Presample SET RESPID = @RESPID WHERE [ID] = @ID", sql_connection);

                    sql_command.Parameters.AddWithValue("@ID", id);
                    sql_command.Parameters.AddWithValue("@RESPID", SqlDbType.Char).Value = respid;

                    //Open the connection.
                    sql_connection.Open();

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

        // Update the review status fields: worked, rev1nme, rev2nme
        public void UpdateReviewStatus(string id, string worked, string revnme, int reviewNum)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    if (reviewNum == 1)
                    {
                        SqlCommand sql_command = new SqlCommand("UPDATE dbo.Presample SET WORKED = @WORKED, REV1NME = @REVNME WHERE [ID] = @ID", sql_connection);
                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@WORKED", SqlDbType.Char).Value = GeneralData.NullIfEmpty(worked);
                        sql_command.Parameters.AddWithValue("@REVNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(revnme);

                        //Open the connection.
                        sql_connection.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            //Execute the query.
                            sql_command.ExecuteNonQuery();
                        }

                        sql_connection.Close(); //close database connection              

                    }
                    if (reviewNum == 2)
                    {
                        SqlCommand sql_command = new SqlCommand("UPDATE dbo.Presample SET WORKED = @WORKED, REV2NME = @REVNME WHERE [ID] = @ID", sql_connection);

                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@WORKED", SqlDbType.Char).Value = GeneralData.NullIfEmpty(worked);
                        sql_command.Parameters.AddWithValue("@REVNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(revnme);

                        //Open the connection.
                        sql_connection.Open();

                        using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                        {
                            //Execute the query.
                            sql_command.ExecuteNonQuery();
                        }

                        sql_connection.Close(); //close database connection  
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

        public void UpdatePresampleData(string id, string respid, string statcode,
            string rbldgs, string runits, string fiveplus, string strtdate,
            string projdesc, string projloc, string pcityst, string resporg,
            string factoff, string othrresp, string addr1, string addr2, string addr3, string zip,
            string respnote, string respname, string phone, string ext, string respname2,
            string phone2, string ext2, string fax, string email, string weburl, string commtext, string commdate,
            string commtime, string usrnme, int dupmaster, string dupflag)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.presample " +
                               "set " +
                               "RESPID = @RESPID " +
                               ",STATUS = @STATUS " +
                               ",RBLDGS = @RBLDGS " +
                               ",RUNITS = @RUNITS " +
                               ",FIVEPLUS = @FIVEPLUS " +
                               ",STRTDATE = @STRTDATE " +
                               ",PROJDESC = @PROJDESC " +
                               ",PROJLOC = @PROJLOC " +
                               ",PCITYST = @PCITYST " +
                               ",RESPORG = @RESPORG " +
                               ",FACTOFF = @FACTOFF " +
                               ",OTHRRESP = @OTHRRESP " +
                               ",ADDR1 = @ADDR1 " +
                               ",ADDR2 = @ADDR2 " +
                               ",ADDR3 = @ADDR3 " +
                               ",ZIP = @ZIP " +
                               ",RESPNOTE = @RESPNOTE " +
                               ",RESPNAME = @RESPNAME " +
                               ",PHONE = @PHONE " +
                               ",EXT = @EXT " +
                               ",RESPNAME2 = @RESPNAME2 " +
                               ",PHONE2 = @PHONE2 " +
                               ",EXT2 = @EXT2 " +
                               ",FAX = @FAX " +
                               ",EMAIL = @EMAIL " +
                               ",WEBURL = @WEBURL " +
                               ",COMMTEXT = @COMMTEXT " +
                               ",COMMDATE = @COMMDATE " +
                               ",COMMTIME = @COMMTIME " +
                               ",USRNME = @USRNME " +
                               ",DUPMASTER = @DUPMASTER " +
                               ",DUPFLAG = @DUPFLAG " +
                               " WHERE ID = @ID ";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, sql_connection);

                    sql_command.Parameters.AddWithValue("@ID", id);
                    sql_command.Parameters.AddWithValue("@RESPID", respid);
                    sql_command.Parameters.AddWithValue("@STATUS", statcode);
                    sql_command.Parameters.AddWithValue("@RBLDGS", rbldgs);
                    sql_command.Parameters.AddWithValue("@RUNITS", runits);
                    sql_command.Parameters.AddWithValue("@FIVEPLUS", fiveplus);
                    sql_command.Parameters.AddWithValue("@STRTDATE", strtdate);
                    sql_command.Parameters.AddWithValue("@PROJDESC", projdesc);
                    sql_command.Parameters.AddWithValue("@PROJLOC", projloc);
                    sql_command.Parameters.AddWithValue("@PCITYST", pcityst);
                    sql_command.Parameters.AddWithValue("@RESPORG", resporg);
                    sql_command.Parameters.AddWithValue("@FACTOFF", factoff);
                    sql_command.Parameters.AddWithValue("@OTHRRESP", othrresp);
                    sql_command.Parameters.AddWithValue("@ADDR1", addr1);
                    sql_command.Parameters.AddWithValue("@ADDR2", addr2);
                    sql_command.Parameters.AddWithValue("@ADDR3", addr3);
                    sql_command.Parameters.AddWithValue("@ZIP", zip);
                    sql_command.Parameters.AddWithValue("@RESPNOTE", respnote);
                    sql_command.Parameters.AddWithValue("@RESPNAME", respname);
                    sql_command.Parameters.AddWithValue("@PHONE", phone);
                    sql_command.Parameters.AddWithValue("@EXT", ext);
                    sql_command.Parameters.AddWithValue("@RESPNAME2", respname2);
                    sql_command.Parameters.AddWithValue("@PHONE2", phone2);
                    sql_command.Parameters.AddWithValue("@EXT2", ext2);
                    sql_command.Parameters.AddWithValue("@FAX", fax);
                    sql_command.Parameters.AddWithValue("@EMAIL", email);
                    sql_command.Parameters.AddWithValue("@WEBURL", weburl);
                    sql_command.Parameters.AddWithValue("@COMMTEXT", commtext);
                    sql_command.Parameters.AddWithValue("@COMMDATE", commdate);
                    sql_command.Parameters.AddWithValue("@COMMTIME", commtime);
                    sql_command.Parameters.AddWithValue("@USRNME", usrnme);
                    sql_command.Parameters.AddWithValue("@DUPMASTER", dupmaster);
                    sql_command.Parameters.AddWithValue("@DUPFLAG", dupflag);

                    //Open the connection.
                    sql_connection.Open();

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

        //apply updates to the respondent table

        public void UpdateRespondentData(string respid, string resporg, string respname,
                string respname2, string phone, string phone2, string ext, string ext2,
                string addr1, string addr2, string addr3, string zip, string fax,
                string factoff, string othrresp, string respnote, string email, string weburl, string rstate)
        {

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.respondent " +
                               "set " +
                               "RESPORG = @RESPORG " +
                               ",RESPNAME = @RESPNAME " +
                               ",RESPNAME2 = @RESPNAME2 " +
                               ",PHONE = @PHONE " +
                               ",PHONE2 = @PHONE2 " +
                               ",EXT = @EXT " +
                               ",EXT2 = @EXT2 " +
                               ",ADDR1 = @ADDR1 " +
                               ",ADDR2 = @ADDR2 " +
                               ",ADDR3 = @ADDR3 " +
                               ",ZIP = @ZIP " +
                               ",FAX = @FAX " +
                               ",FACTOFF = @FACTOFF " +
                               ",OTHRRESP = @OTHRRESP " +
                               ",RESPNOTE = @RESPNOTE " +
                               ",EMAIL = @EMAIL " +
                               ",WEBURL = @WEBURL " +
                               ",RSTATE = @RSTATE" +
                               " WHERE RESPID = @RESPID ";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, sql_connection);

                    sql_command.Parameters.AddWithValue("@RESPID", respid);
                    sql_command.Parameters.AddWithValue("@RESPORG", resporg);
                    sql_command.Parameters.AddWithValue("@FACTOFF", factoff);
                    sql_command.Parameters.AddWithValue("@OTHRRESP", othrresp);
                    sql_command.Parameters.AddWithValue("@ADDR1", addr1);
                    sql_command.Parameters.AddWithValue("@ADDR2", addr2);
                    sql_command.Parameters.AddWithValue("@ADDR3", addr3);
                    sql_command.Parameters.AddWithValue("@ZIP", zip);
                    sql_command.Parameters.AddWithValue("@RESPNOTE", respnote);
                    sql_command.Parameters.AddWithValue("@RESPNAME", respname);
                    sql_command.Parameters.AddWithValue("@PHONE", phone);
                    sql_command.Parameters.AddWithValue("@EXT", ext);
                    sql_command.Parameters.AddWithValue("@RESPNAME2", respname2);
                    sql_command.Parameters.AddWithValue("@PHONE2", phone2);
                    sql_command.Parameters.AddWithValue("@EXT2", ext2);
                    sql_command.Parameters.AddWithValue("@FAX", fax);
                    sql_command.Parameters.AddWithValue("@EMAIL", email);
                    sql_command.Parameters.AddWithValue("@WEBURL", weburl);
                    sql_command.Parameters.AddWithValue("@RSTATE", rstate);

                    //Open the connection.
                    sql_connection.Open();

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

        /*Get Presample masterid */

        public int GetPresampMasterid(string id)
        {
            int pid = 0;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            string sql = "SELECT * FROM dbo.PRESAMPLE WHERE ID = " + GeneralData.AddSqlQuotes(id);
                   
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    pid = (int)reader["MASTERID"];

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

            return pid;
        }

        /*Get the Sample Master ID */

        public int GetSampleMasterId(string id)
        {
            int masterid = 0;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            string sql = "SELECT MASTERID FROM dbo.SAMPLE WHERE ID = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    masterid = (int)reader["MASTERID"];
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

            return masterid;
        }

        /*Get presample masterid from the search view */
        public int GetMasterid(string fin)
        {
            int masterid = 0;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "SELECT * from dbo.MF_INITIAL where FIN = @FIN";

                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                //Pass the parameters values.
                sql_command.Parameters.AddWithValue("@FIN", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(fin);
                try
                {
                    sql_connection.Open();
                    SqlDataReader reader =
                            sql_command.ExecuteReader(CommandBehavior.SingleRow);

                    while (reader.Read())
                    {
                        masterid = (int)reader["MASTERID"];
                    }

                    sql_connection.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return masterid;
        }

        //Get the next masterid for the next unworked case for NPCInterviewers who have a user grade of 4
        public string GetNextCaseGrade4()
        {
            string id = string.Empty;
            string sql;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql = "Select id from dbo.Presample left outer join dbo.Respondent on dbo.Presample.Respid = dbo.Respondent.Respid";
                sql = sql + " where worked = '0' and  (dbo.PRESAMPLE.RESPLOCK = '' OR dbo.PRESAMPLE.RESPLOCK IS NULL) AND (dbo.RESPONDENT.RESPLOCK = '' OR dbo.RESPONDENT.RESPLOCK IS NULL) order by ID";
               
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader["ID"].ToString();
                            break;
                        }
                    }

                   // while (reader.NextResult()) ;

                    reader.Close();
                    reader.Dispose();//always good idea to do proper cleanup

                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close();
                    sql_connection.Dispose();

                }

                return id;
            }

        }

        //Get the next masterid for the next unworked case for NPCInterviewers who have a user grade of 5
        //as well as NPCManagers and NPCLeads
        public string GetNextCaseGrade5()
        {
            string id = string.Empty;
            string sql = string.Empty;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql = "Select id from dbo.Presample left outer join dbo.Respondent on dbo.Presample.Respid = dbo.Respondent.Respid";
                sql = sql + " where (worked = '0' OR WORKED = '1') and  (dbo.PRESAMPLE.RESPLOCK = '' OR dbo.PRESAMPLE.RESPLOCK IS NULL) AND (dbo.RESPONDENT.RESPLOCK = '' OR dbo.RESPONDENT.RESPLOCK IS NULL) ";
                sql = sql + " AND REV1NME <> @JBONDID ORDER BY worked desc, ID";
                
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                sql_command.Parameters.AddWithValue("@JBONDID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(UserInfo.UserName);
                
                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);

                    while (reader.Read())
                    {                    
                        id = reader["ID"].ToString();
                        break;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close();
                }

                return id;
            }
        }

        /*Get Presample Respid*/

        public string GetPresampleRespid(string id)
        {
            string presamprespid = String.Empty;

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            string sql = "SELECT RESPID FROM dbo.PRESAMPLE WHERE ID = " + id;
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    presamprespid = reader["RESPID"].ToString().Trim();

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

            return presamprespid;
        }

    }
}
