/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.DodgeInitialData.cs	    	

Programmer:         Srini Natarajan
Creation Date:      01/24/2017

Inputs:             ID

Parameters:	        None 

Outputs:	        Dodge Initial data	

Description:	    This function establishes the data connection and reads in 
                    the data, based upon the ID, using the DODGE_INIT_ADDR 
                    View.

Detailed Design:    None 

Other:	            Called by: frmDodgeInitial
 
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
    public class DodgeInitialData
    {
        public List<string> HQSecitems = new List<string>();
        DodgeInitial dodgeinitial;
        //DodgeInitial DodgeInitialrestore;
        private string locked_by = String.Empty;
        private string item = string.Empty;

        public static DodgeInitial GetDodgeInitialData(string Id)
        {
            DodgeInitial dodgeinitial = new DodgeInitial();

            //Connect to database using the parameterized stored procedure.
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                string sql = " SELECT R.RESPORG, R.RESPNAME, R.RESPNAME2, R.ADDR1, R.ADDR2, R.ADDR3, R.ZIP, R.PHONE, R.PHONE2, R.EXT, R.EXT2, R.FAX, R.FACTOFF, R.OTHRRESP, ";
                sql = sql + " R.RESPNOTE, R.EMAIL, R.WEBURL, S.ID, S.RESPID, S.PROJDESC, S.CONTRACT, S.PROJLOC, S.PCITYST, S.FWGT, S.STATUS, S.STRTDATE, S.RVITM5C, ";
                sql = sql + " S.FLAGR5C, S.COMPDATE, S.FUTCOMPD, S.FLAGSTRTDATE, S.FLAGCOMPDATE, S.FLAGFUTCOMPD, M.DODGENUM, M.FIPSTATE, M.OWNER, M.PROJSELV, M.FIPSTATE, M.DODGECOU, ";
                sql = sql + "  M.SELDATE, M.SOURCE, M.NEWTC, S.STRTDATER, S.STRTDATE, S.RVITM5CR, S.COMPDATER, S.FUTCOMPDR, R.CENTPWD, S.FLAGCAP, S.FLAGITM6, S.FLAG5A, S.FLAG5B, ";
                sql = sql + "  R.COLTEC, R.COLHIST, M.STRUCTCD, R.RESPLOCK, M.FIN, M.MASTERID, S.ACTIVE, R.RSTATE, R.LAG, M.MRN, dbo.DCPINITIAL.HQWORKED,  ";
                sql = sql + "  dbo.DCPINITIAL.WORKED, dbo.DCPINITIAL.REV1NME, dbo.DCPINITIAL.REV2NME, dbo.DCPINITIAL.HQNME ";
                sql = sql + "  FROM  dbo.SAMPLE AS S INNER JOIN dbo.RESPONDENT AS R ON S.RESPID = R.RESPID INNER JOIN dbo.MASTER AS M ON S.MASTERID = M.MASTERID RIGHT OUTER JOIN dbo.DCPINITIAL ON S.ID = dbo.DCPINITIAL.ID ";
                sql = sql + " WHERE s.id = " + GeneralData.AddSqlQuotes(Id);
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        dodgeinitial.Id = reader["Id"].ToString();
                        dodgeinitial.Respid = reader["Respid"].ToString();
                        if (dodgeinitial.Id == dodgeinitial.Respid)
                        {
                            dodgeinitial.Respid = "";
                        }
                        dodgeinitial.Seldate = reader["Seldate"].ToString();
                        dodgeinitial.Selvalue = reader["Projselv"].ToString();
                        dodgeinitial.Statuscode = reader["Status"].ToString();
                        dodgeinitial.Fipstate = GeneralDataFuctions.GetFipState(reader["FIPSTATE"].ToString());
                        dodgeinitial.Survey = reader["Owner"].ToString();
                        dodgeinitial.Fwgt = reader["Fwgt"].ToString();
                        dodgeinitial.Dodgecou = reader["Dodgecou"].ToString();
                        dodgeinitial.Newtc = reader["Newtc"].ToString();
                        dodgeinitial.Masterid = Convert.ToInt32(reader["Masterid"]);
                        dodgeinitial.Dodgenum = reader["Dodgenum"].ToString();
                        dodgeinitial.Contract = reader["Contract"].ToString().Trim();
                        dodgeinitial.ProjDesc = reader["Projdesc"].ToString().Trim();
                        dodgeinitial.Projloc = reader["Projloc"].ToString().Trim();
                        dodgeinitial.Pcityst = reader["Pcityst"].ToString().Trim();
                        dodgeinitial.Factoff = reader["Factoff"].ToString().Trim();
                        dodgeinitial.Othrresp = reader["Othrresp"].ToString().Trim();
                        dodgeinitial.Addr1 = reader["Addr1"].ToString().Trim();
                        dodgeinitial.Addr2 = reader["Addr2"].ToString().Trim();
                        dodgeinitial.Addr3 = reader["Addr3"].ToString().Trim();
                        dodgeinitial.Zip = reader["Zip"].ToString().Trim();
                        dodgeinitial.Resporg = reader["Resporg"].ToString().Trim();
                        dodgeinitial.Respname = reader["Respname"].ToString().Trim();
                        dodgeinitial.Respname2 = reader["Respname2"].ToString().Trim();
                        dodgeinitial.Respnote = reader["Respnote"].ToString().Trim();
                        dodgeinitial.Phone = reader["Phone"].ToString().Trim();
                        dodgeinitial.Phone2 = reader["Phone2"].ToString().Trim();
                        dodgeinitial.Ext = reader["Ext"].ToString().Trim();
                        dodgeinitial.Ext2 = reader["Ext2"].ToString().Trim();
                        dodgeinitial.Fax = reader["Fax"].ToString().Trim();
                        dodgeinitial.Email = reader["Email"].ToString().Trim();
                        dodgeinitial.Weburl = reader["Weburl"].ToString().Trim();
                        dodgeinitial.Strtdater = reader["Strtdater"].ToString().Trim();
                        dodgeinitial.Strtdate = reader["Strtdate"].ToString().Trim();
                        dodgeinitial.Flagstrtdate = reader["Flagstrtdate"].ToString().Trim();
                        dodgeinitial.Coltec = reader["coltec"].ToString().Trim();
                        dodgeinitial.Colhist = reader["colhist"].ToString().Trim();
                        dodgeinitial.Structcd = reader["structcd"].ToString().Trim();
                        dodgeinitial.RespLock = reader["resplock"].ToString().Trim();
                        dodgeinitial.Fin = reader["fin"].ToString().Trim();
                        dodgeinitial.Mrn = reader["mrn"].ToString().Trim();
                        dodgeinitial.Active = reader["active"].ToString().Trim();
                        dodgeinitial.HQWorked = reader["hqworked"].ToString().Trim();
                        dodgeinitial.Lag = reader["Lag"].ToString().Trim();
                        dodgeinitial.Timezone = GeneralDataFuctions.GetTimezone(dodgeinitial.Rstate);
                        dodgeinitial.Worked = reader["worked"].ToString();
                        dodgeinitial.Rev1nme = reader["rev1nme"].ToString().Trim();
                        dodgeinitial.Rev2nme = reader["rev2nme"].ToString().Trim();
                    }
                    else
                    {
                        dodgeinitial = null;
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
                return dodgeinitial;
            }               
        }

        //Updates the dcp History Table with any case accessed by the user
        public void AddDCPHistRecs(string id, string usrnme, string accesday, string accestms, string accestme)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("INSERT INTO dbo.DCP_HIST(ID, ACCESDAY, ACCESTMS, ACCESTME, ACCESNME) VALUES (@ID, @ACCESDAY, @ACCESTMS, @ACCESTME, @ACCESNME )", sql_connection);

                    sql_command.Parameters.AddWithValue("@ID", id);
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

        // Update the review status fields: worked, rev1nme, rev2nme
        public void UpdateDodgeIniRevStatusNPC(string id, string worked, string revnme, int reviewNum)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    if (reviewNum == 1)
                    {
                        SqlCommand sql_command = new SqlCommand("UPDATE dbo.DCPINITIAL SET WORKED = @WORKED, REV1NME = @REV1NME WHERE [ID] = @ID", sql_connection);
                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@WORKED", SqlDbType.Char).Value = GeneralData.NullIfEmpty(worked);
                        sql_command.Parameters.AddWithValue("@REV1NME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(revnme);

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
                        SqlCommand sql_command = new SqlCommand("UPDATE dbo.DCPINITIAL SET WORKED = @WORKED, REV2NME = @REV2NME WHERE [ID] = @ID", sql_connection);
                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@WORKED", SqlDbType.Char).Value = GeneralData.NullIfEmpty(worked);
                        sql_command.Parameters.AddWithValue("@REV2NME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(revnme);

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

        // Update the review status fields: hqworked, hqnme
        public void UpdateDodgeIniRevStatus(string id, string worked, string revnme, int reviewNum)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    if (reviewNum == 1)
                    {
                        SqlCommand sql_command = new SqlCommand("UPDATE dbo.DCPINITIAL SET HQWORKED = @HQWORKED, HQNME = @HQNME WHERE [ID] = @ID", sql_connection);
                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@HQWORKED", SqlDbType.Char).Value = GeneralData.NullIfEmpty(worked);
                        sql_command.Parameters.AddWithValue("@HQNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(revnme);

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
                        SqlCommand sql_command = new SqlCommand("UPDATE dbo.DCPINITIAL SET HQWORKED = @HQWORKED, HQNME = @HQNME WHERE [ID] = @ID", sql_connection);

                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@HQWORKED", SqlDbType.Char).Value = GeneralData.NullIfEmpty(worked);
                        sql_command.Parameters.AddWithValue("@HQNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(revnme);

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

        //Restore data
        public static DodgeInitialrestore dcpInitial(string id)
        {
           DodgeInitialrestore dcpInitialData = new DodgeInitialrestore();
           //Connect to database using the parameterized stored procedure.
           using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
           {
               SqlCommand sql_command = new SqlCommand("select * from dbo.DCPINITIAL where ID=@ID", sql_connection);
               sql_command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(id);
               try
               {
                   sql_connection.Open();
                   SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);
                   if (reader.Read())
                   {
                       dcpInitialData.Id = reader["Id"].ToString();
                       dcpInitialData.OFactoff = reader["OFactoff"].ToString();
                       dcpInitialData.OProjdesc = reader["OProjdesc"].ToString();
                       dcpInitialData.OProjloc = reader["OProjloc"].ToString();
                       dcpInitialData.OPcityst = reader["OPcityst"].ToString();
                       dcpInitialData.OOthrresp = reader["OOthrresp"].ToString();
                       dcpInitialData.OContract = reader["OContract"].ToString();
                       dcpInitialData.OAddr1 = reader["OAddr1"].ToString();
                       dcpInitialData.OAddr2 = reader["OAddr2"].ToString();
                       dcpInitialData.OAddr3 = reader["OAddr3"].ToString();
                       dcpInitialData.OZip = reader["OZip"].ToString();
                       dcpInitialData.OResporg = reader["OResporg"].ToString();
                       dcpInitialData.ORespname = reader["ORespname"].ToString();
                       dcpInitialData.ORespname2 = reader["ORespname2"].ToString();
                       dcpInitialData.ORespnote = reader["ORespnote"].ToString();
                       dcpInitialData.OPhone = reader["OPhone"].ToString();
                       dcpInitialData.OPhone2 = reader["OPhone2"].ToString();
                       dcpInitialData.OExt = reader["OExt"].ToString();
                       dcpInitialData.OExt2 = reader["OExt2"].ToString();
                       dcpInitialData.OFax = reader["OFax"].ToString();
                       dcpInitialData.OEmail = reader["OEmail"].ToString();
                       dcpInitialData.OWeburl = reader["OWeburl"].ToString();
                   }
                   else
                   {
                       dcpInitialData = null;
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
               return dcpInitialData;
           }  
        }

       //replace Respondent fields with the old values before the changes were made.
       public static DodgeInitial RefreshDodgeInitial(string Id)
       {
           DodgeInitial RefreshDetails = new DodgeInitial();
           //Connect to database using the parameterized stored procedure.
           using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
           {
                string sql = " SELECT R.RESPORG, R.RESPNAME, R.RESPNAME2, R.ADDR1, R.ADDR2, R.ADDR3, R.ZIP, R.PHONE, R.PHONE2, R.EXT, R.EXT2, R.FAX, R.FACTOFF, R.OTHRRESP, ";
                sql = sql + " R.RESPNOTE, R.EMAIL, R.WEBURL, S.ID, S.RESPID, S.PROJDESC, S.CONTRACT, S.PROJLOC, S.PCITYST, S.FWGT, S.STATUS, S.STRTDATE, S.RVITM5C, ";
                sql = sql + " S.FLAGR5C, S.COMPDATE, S.FUTCOMPD, S.FLAGSTRTDATE, S.FLAGCOMPDATE, S.FLAGFUTCOMPD, M.DODGENUM, M.FIPSTATE, M.OWNER, M.PROJSELV, ";
                sql = sql + "  M.SELDATE, M.SOURCE, M.NEWTC, S.STRTDATER, S.STRTDATE, S.RVITM5CR, S.COMPDATER, S.FUTCOMPDR, R.CENTPWD, S.FLAGCAP, S.FLAGITM6, S.FLAG5A, S.FLAG5B, ";
                sql = sql + "  R.COLTEC, R.COLHIST, M.STRUCTCD, R.RESPLOCK, M.FIN, M.MASTERID, S.ACTIVE, R.RSTATE, R.LAG, M.MRN, dbo.DCPINITIAL.HQWORKED, ";
                sql = sql + "  dbo.DCPINITIAL.WORKED, dbo.DCPINITIAL.REV1NME, dbo.DCPINITIAL.REV2NME, dbo.DCPINITIAL.HQNME ";
                sql = sql + "  FROM  dbo.SAMPLE AS S INNER JOIN dbo.RESPONDENT AS R ON S.RESPID = R.RESPID INNER JOIN dbo.MASTER AS M ON S.MASTERID = M.MASTERID RIGHT OUTER JOIN dbo.DCPINITIAL ON S.ID = dbo.DCPINITIAL.ID ";
                sql = sql + " WHERE s.id = " + GeneralData.AddSqlQuotes(Id);
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);

                try
               {
                   sql_connection.Open();
                   SqlDataReader reader =
                           sql_command.ExecuteReader(CommandBehavior.SingleRow);
                   if (reader.Read())
                   {
                       RefreshDetails.Id = reader["Id"].ToString();
                       RefreshDetails.Respid = reader["Respid"].ToString();
                       RefreshDetails.Factoff = reader["Factoff"].ToString();
                       RefreshDetails.Othrresp = reader["Othrresp"].ToString();
                       RefreshDetails.Addr1 = reader["Addr1"].ToString();
                       RefreshDetails.Addr2 = reader["Addr2"].ToString();
                       RefreshDetails.Addr3 = reader["Addr3"].ToString();
                       RefreshDetails.Zip = reader["Zip"].ToString();
                       RefreshDetails.Resporg = reader["Resporg"].ToString();
                       RefreshDetails.Respname = reader["Respname"].ToString();
                       RefreshDetails.Respname2 = reader["Respname2"].ToString();
                       RefreshDetails.Respnote = reader["Respnote"].ToString();
                       RefreshDetails.Phone = reader["Phone"].ToString();
                       RefreshDetails.Phone2 = reader["Phone2"].ToString();
                       RefreshDetails.Ext = reader["Ext"].ToString();
                       RefreshDetails.Ext2 = reader["Ext2"].ToString();
                       RefreshDetails.Fax = reader["Fax"].ToString();
                       RefreshDetails.Email = reader["Email"].ToString();
                       RefreshDetails.Weburl = reader["Weburl"].ToString();
                       RefreshDetails.Coltec = reader["coltec"].ToString();
                       RefreshDetails.Colhist = reader["colhist"].ToString();
                       RefreshDetails.Contract = reader["contract"].ToString();
                       RefreshDetails.Newtc = reader["newtc"].ToString();
                       RefreshDetails.Owner = reader["owner"].ToString();
                       RefreshDetails.ProjDesc = reader["projdesc"].ToString();
                       RefreshDetails.Projloc = reader["projloc"].ToString();
                       RefreshDetails.Pcityst = reader["pcityst"].ToString();
                   }
                   else
                   {
                       RefreshDetails = null;
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
               return RefreshDetails;
           }
       }

       /*Get Respondent Data */
       public DataTable GetRespondentSearchData(string respid, string org1, string org2, string org3, string contact1, string contact2, string contact3, string email, string phone1, string phone2, bool exact) //, string contact3, string phone, string rstate, bool extact)
       {
           DataTable dt = new DataTable();
           using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
           {
               string sp_name = "dbo.sp_DCPInitialSearch";
               if (exact)
               { sp_name = "dbo.sp_DCPInitialSearchExact"; }

               SqlCommand sql_command = new SqlCommand(sp_name, sql_connection);
               sql_command.CommandType = CommandType.StoredProcedure;
               sql_command.CommandTimeout = 240;

               sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);
               sql_command.Parameters.Add("@ORG1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(org1);
               sql_command.Parameters.Add("@ORG2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(org2);
               sql_command.Parameters.Add("@ORG3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(org3);
               sql_command.Parameters.Add("@CONTACT1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact1);
               sql_command.Parameters.Add("@CONTACT2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact2);
               sql_command.Parameters.Add("@CONTACT3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact3);
               sql_command.Parameters.Add("@EMAIL", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(email);
               sql_command.Parameters.Add("@PHONE1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(phone1);
               sql_command.Parameters.Add("@PHONE2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(phone2);
               
               SqlDataAdapter da = new SqlDataAdapter(sql_command);
               da.Fill(dt);
           }
           return dt;
       }

        //Check if a User exists in HQSector table!
        public bool ChkHqSector(string usernme)
        {
            bool HqSecUsrNmeExist = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select usrnme from dbo.HQSECTOR where usrnme = " + GeneralData.AddSqlQuotes(UserInfo.UserName);
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
            return HqSecUsrNmeExist;
        }

        //get data from useraudit
        public string id4HQsect = string.Empty;
        public string GetIDfromNewTCCode(string sectorNumb)
        {
           
            //DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select ID from DCPREVIEW where SUBSTRING(NEWTC, 1, 2) = SUBSTRING('" + sectorNumb+ "', 5, 2) order by ID";   //, SUBSTRING (NEWTC, 1, 2) as NEWTCNUM
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    sql_connection.Open();
                    SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id4HQsect = reader["ID"].ToString();
                            return id4HQsect;
                        }
                    }
                }
            }
            return id4HQsect;
        }

        //Get the next available record for hq users with HQ sector data
        public string GetHQSectNewTCData(string item)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlHQcnt = string.Empty;
                string getNewTC2digits = item.Substring(item.Length - 2);
                if (getNewTC2digits == "1T")
                { sqlHQcnt = "select ID from DCPREVIEW where SUBSTRING(newtc, 1,2) >'19' and (HQWORKED = '0' OR HQWORKED = '1') AND hqnme <> @JBONDID and RESPLOCK = '' order by ID"; }
                else
                { sqlHQcnt = "select ID from DCPREVIEW where SUBSTRING(newtc, 1,2) = '" + getNewTC2digits + "' and (HQWORKED = '0' OR HQWORKED = '1') AND hqnme <> @JBONDID and RESPLOCK = '' order by ID"; }
                
                using (SqlCommand cmd = new SqlCommand(sqlHQcnt, sql_connection))
                {
                    cmd.Parameters.AddWithValue("@JBONDID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(UserInfo.UserName);
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                    foreach (DataRow row in dt.Rows)
                    {
                        string HQsecRespid;
                        string value = row[0].ToString();
                        id4HQsect = value;
                        dodgeinitial = new DodgeInitial();
                        dodgeinitial = DodgeInitialData.GetDodgeInitialData(id4HQsect);
                        if (dodgeinitial.Respid == "")
                        { HQsecRespid = dodgeinitial.Id; }
                        else
                        { HQsecRespid = dodgeinitial.Respid; }
                        locked_by = GeneralDataFuctions.ChkRespIDIsLocked(HQsecRespid);
                        if (Convert.ToString(locked_by) == UserInfo.UserName || Convert.ToString(locked_by) == "")
                        {
                            locked_by = "";
                            sql_connection.Close();
                            break;
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            return id4HQsect;
        }

        //Get the list of valid sector numbers for specific users.
        public string GetHQSectors()
        {
            dodgeinitial = new DodgeInitial();
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "select t.Subs from (select USRNME, u.subs, u.val from HQSECTOR s unpivot ( val for subs  in (SECT00, SECT01, SECT02, SECT03, SECT04, SECT05, SECT06, SECT07, SECT08, SECT09, SECT10, SECT11, SECT12, SECT13, SECT14, SECT15, SECT16, SECT19, SECT1T)) u where u.val = 'Y') T where t.usrnme = '" + UserInfo.UserName + "'"; 
            SqlCommand command = new SqlCommand(sql, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    item = reader.GetString(reader.GetOrdinal("Subs"));
                    HQSecitems.Add(item);
                    GetHQSectNewTCData(item);
                    if (id4HQsect != "")
                    {
                        return id4HQsect;
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
            return HQSecitems.ToString();
        }

        private string owner_stringSL = string.Empty;
        private string owner_stringNR = string.Empty;
        private string owner_stringFD = string.Empty;
        private string owner_string = string.Empty;
        //Get the next id for the next unworked case for NPCInterviewers who have a user grade of 4
        string id = string.Empty;
        private string sql = string.Empty;
        public DataTable GetNextCaseGrade4()
       {
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4")
            {
                if (UserInfo.InitFD == "Y")
                {
                    owner_string = "OWNER='F' or OWNER='C' or OWNER='D'";
                }
                if (UserInfo.InitSL == "Y")
                {
                    if (owner_string != "")
                    {
                        owner_string = owner_string + " OR";
                    }
                    owner_string = owner_string + " OWNER='P' or OWNER='S' or OWNER='L'";
                }
                if (UserInfo.InitNR == "Y")
                {
                    if (owner_string != "")
                    {
                        owner_string = owner_string + " OR";
                    }
                    owner_string = owner_string + " OWNER='N' or OWNER='T' or OWNER='G' or OWNER='R' or OWNER='O' or OWNER='W'";
                }
            }

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                if (owner_string == "")
                {
                    sql = "SELECT ID FROM dbo.DODGE_INITIAL_ADDR WHERE WORKED = '0' and RESPLOCK = '' ORDER BY ID";
                }
                else
                {
                    sql = "SELECT ID FROM dbo.DODGE_INITIAL_ADDR WHERE WORKED = '0' and RESPLOCK = '' and (" + owner_string + ") ORDER BY ID";
                }
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);
                try
                {
                    sql_connection.Open();
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
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
            }
            return dt;
    }

        //Get the next id for the next unworked case for NPC users who have a user grade of 5
        public DataTable GetNextCaseGrade5()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("SELECT ID FROM dbo.DODGE_INITIAL_ADDR WHERE (WORKED = '0' OR WORKED = '1') AND REV1NME <> @JBONDID and RESPLOCK = '' ORDER BY WORKED DESC, ID ASC", connection);
                sql_command.Parameters.AddWithValue("@JBONDID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(UserInfo.UserName);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //SELECT next available record for HQ users
        public DataTable GetIDListforNExtInitial()
        {
            DataTable dt = new DataTable();
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("SELECT ID FROM dbo.DODGE_INITIAL_ADDR WHERE (HQWORKED = '0' OR (HQWORKED = '1' and HQNME = @JBONDID)) and RESPLOCK = '' ORDER BY HQWORKED, ID", connection);
                sql_command.Parameters.AddWithValue("@JBONDID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(UserInfo.UserName);
                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //Get the next masterid for the next unworked case for NPCInterviewers who have a user grade of 5 as well as NPCManagers and NPCLeads
        public string GetNextCaseHQUser()
        {
            string id = string.Empty;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("SELECT ID FROM dbo.DODGE_INITIAL_ADDR WHERE (HQWORKED = '0' OR HQWORKED = '1') AND RESPLOCK = '' ORDER BY HQWORKED ASC, ID", sql_connection);
                sql_command.Parameters.AddWithValue("@JBONDID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(UserInfo.UserName);
                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);

                    while (reader.Read())
                    {
                        id = reader["ID"].ToString();
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
        
        /*create a empty table */
        public DataTable GetEmptyTable()
       {
           // Here we create a DataTable with 7 columns.
           DataTable table = new DataTable();
           table.Columns.Add("RESPID", typeof(string));
           table.Columns.Add("ORGANIZATION", typeof(string));
           table.Columns.Add("CONTACT", typeof(string));
           table.Columns.Add("PHONE", typeof(string));
           table.Columns.Add("PHONE2", typeof(string));
           return table;
       }

    }
}
