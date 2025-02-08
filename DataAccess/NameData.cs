/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.NameData.cs	    	

Programmer:         Srini Natarajan
Creation Date:      03/25/2015

Inputs:             ID

Parameters:	        None 

Outputs:	        Name and Address data	

Description:	    This function establishes the data connection and reads in 
                    the data, based upon the Csd number, using the NAME_ADDR_DISPLAY 
                    View that will be used for the Name Address Display screen.

Detailed Design:    None 

Other:	            Called by: frmName
 
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
    public class NameData
    {
        public string GetParentMasterId(string parentId)
        {
            string ParentMasterId = String.Empty;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            string sql = "select MASTERID from NAME_ADDR_DISPLAY where ID = '" + parentId + "'"; // + GeneralData.AddSqlQuotes(respid);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    ParentMasterId = reader["MASTERID"].ToString().Trim();
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
            return ParentMasterId;            
        }

        public string GetTimeZone(string stateCode)
        {
            string timezonecode = String.Empty;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
            string sql = "select TIMEFACT from STATELIST where STABBR = '"+ stateCode + "'"; // + GeneralData.AddSqlQuotes(respid);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
                if (reader.Read())
                {
                    timezonecode = reader["TIMEFACT"].ToString().Trim();
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
            return timezonecode;
        }

        public static string ChkMonthVIPFlag(string MonVIPDataid)
        {
            //Connect to the MONTHLY_VIP_DATA table for Data Origin flags
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                //Create the SQL string to get a Dataset of all VIPFLAGs for the selected ID
                string chkId = "SELECT [VIPFLAG] FROM [dbo].[MONTHLY_VIP_DATA] where [ID] = '" + MonVIPDataid + "'";
                SqlCommand sql_commandDataOrg = new SqlCommand(chkId, sql_connection);
                sql_commandDataOrg.CommandTimeout = 0;

                //loop through the recordset and check for an 'A'
                string strMonVipFlag;
                strMonVipFlag = "";
                using (SqlDataReader rdr = sql_commandDataOrg.ExecuteReader())
                {
                    while (rdr.Read())
                    {
                        string strVIPflag = rdr.GetString(0); //The 0 stands for the first column of the result.
                        if (strVIPflag == "A")
                        {
                            strMonVipFlag = "A";
                            break;
                        }
                    }
                }
                sql_connection.Close();
                return strMonVipFlag;
            }
        }

        public static bool CheckIdSampleHold(string id)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select id from dbo.Sample_Hold where ID = " + GeneralData.AddSqlQuotes(id);
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
                
        public static NameAddr GetNameAddr(string Id, string viewcode)
        {
            NameAddr nameaddr = new NameAddr();

            //Connect to database using the parameterized stored procedure.
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_NameAddr", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                //Pass the paramters values (View code and ID Number).
                sql_command.Parameters.Add("@viewcode", SqlDbType.Char).Value = viewcode;
                sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = Id;

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader =
                            sql_command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        nameaddr.Id = reader["Id"].ToString();
                        nameaddr.Respid = reader["Respid"].ToString();
                        if (nameaddr.Id == nameaddr.Respid)
                        {
                            nameaddr.Respid = "";
                        }
                        nameaddr.Seldate = reader["Seldate"].ToString();
                        nameaddr.Selvalue = reader["Projselv"].ToString();
                        nameaddr.Statuscode = reader["Status"].ToString();
                        nameaddr.Fipstate = reader["State"].ToString();
                        nameaddr.Dodgecou = reader["Dodgecou"].ToString();
                        nameaddr.Survey = reader["Owner"].ToString();
                        nameaddr.Fwgt = reader["Fwgt"].ToString();
                        nameaddr.Newtc = reader["Newtc"].ToString();
                        nameaddr.Source = reader["Source"].ToString();
                        //nameaddr.Masterid = reader["Masterid"].ToString();
                        nameaddr.Masterid = Convert.ToInt32(reader["Masterid"]);
                        if (reader["Rbldgs"] != DBNull.Value)
                        {
                            nameaddr.Rbldgs = reader["Rbldgs"].ToString();
                        }
                        else
                        {
                            nameaddr.Rbldgs = (0).ToString();
                        }
                        if (reader["Runits"] != DBNull.Value)
                        {
                            nameaddr.Runits = reader["Runits"].ToString();
                        }
                        else
                        {
                            nameaddr.Runits = (0).ToString();
                        }
                        nameaddr.Dodgenum = reader["Dodgenum"].ToString();
                        nameaddr.Psu = reader["Psu"].ToString();
                        nameaddr.Place = reader["Place"].ToString();
                        nameaddr.Sched = reader["Sched"].ToString();
                        nameaddr.Contract = reader["Contract"].ToString();
                        nameaddr.ProjDesc = reader["Projdesc"].ToString();
                        nameaddr.Projloc = reader["Projloc"].ToString();
                        nameaddr.Pcityst = reader["Pcityst"].ToString();
                        nameaddr.Factoff = reader["Factoff"].ToString();
                        nameaddr.Othrresp = reader["Othrresp"].ToString();
                        nameaddr.Addr1 = reader["Addr1"].ToString();
                        nameaddr.Addr2 = reader["Addr2"].ToString();
                        nameaddr.Addr3 = reader["Addr3"].ToString();
                        nameaddr.Zip = reader["Zip"].ToString();
                        nameaddr.Resporg = reader["Resporg"].ToString();
                        nameaddr.Respname = reader["Respname"].ToString();
                        nameaddr.Respname2 = reader["Respname2"].ToString();
                        nameaddr.Respnote = reader["Respnote"].ToString();
                        nameaddr.Phone = reader["Phone"].ToString();
                        nameaddr.Phone2 = reader["Phone2"].ToString();
                        nameaddr.Ext = reader["Ext"].ToString();
                        nameaddr.Ext2 = reader["Ext2"].ToString();
                        nameaddr.Fax = reader["Fax"].ToString();
                        nameaddr.Email = reader["Email"].ToString();
                        nameaddr.Weburl = reader["Weburl"].ToString();
                        nameaddr.Rvitm5cr = reader["Rvitm5cr"].ToString();
                        nameaddr.Rvitm5c = reader["Rvitm5c"].ToString();
                        nameaddr.Flagr5c = reader["Flagr5c"].ToString();
                        nameaddr.Flag5a = reader["Flag5a"].ToString();
                        nameaddr.Flag5b = reader["Flag5b"].ToString();
                        nameaddr.Flagitm6 = reader["Flagitm6"].ToString();
                        nameaddr.Flagcap = reader["Flagcap"].ToString();
                        nameaddr.Accescde = reader["Accescde"].ToString();
                        nameaddr.Strtdater = reader["Strtdater"].ToString();
                        nameaddr.Strtdate = reader["Strtdate"].ToString();
                        nameaddr.Flagstrtdate = reader["Flagstrtdate"].ToString();
                        nameaddr.Compdater = reader["Compdater"].ToString();
                        nameaddr.Compdate = reader["Compdate"].ToString();
                        nameaddr.Flagcompdate = reader["Flagcompdate"].ToString();
                        nameaddr.Futcompdr = reader["Futcompdr"].ToString();
                        nameaddr.Futcompd = reader["Futcompd"].ToString();
                        nameaddr.Flagfutcompd = reader["Flagfutcompd"].ToString();
                        nameaddr.Centpwd = reader["centpwd"].ToString();
                        nameaddr.Coltec = reader["coltec"].ToString();
                        nameaddr.Colhist = reader["colhist"].ToString();
                        nameaddr.Structcd = reader["structcd"].ToString();
                        nameaddr.RespLock = reader["resplock"].ToString();
                        nameaddr.Fin = reader["fin"].ToString();
                        nameaddr.Active = reader["active"].ToString();
                        nameaddr.Rstate = reader["Rstate"].ToString();
                        nameaddr.Lag = reader["Lag"].ToString();
                        //sql_command.Parameters.AddWithValue("@LAG", lag);
                        nameaddr.Timezone = GeneralDataFuctions.GetTimezone(nameaddr.Rstate);
                    }
                    else
                    {
                        nameaddr = null;
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
                return nameaddr;
            }    
        }

        //When a NewTC is entered manually its validated against the NEWTCLIST table.
        public static bool CheckNewTCNum(string newtcval)
        {
            bool newtc_found = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select newtc from dbo.Newtclist where NEWTC = " + GeneralData.AddSqlQuotes(newtcval);
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
            return newtc_found;
        }

    public void UpdateMasterFlds(string owner, string newtc, string masterid)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.master set OWNER = @OWNER" +
                                ",NEWTC = @NEWTC" +
                                " WHERE MASTERID = @MASTERID";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    sql_command.CommandTimeout = 0;

                    connection.Open();
                    sql_command.Parameters.AddWithValue("@OWNER", owner);
                    sql_command.Parameters.AddWithValue("@NEWTC", newtc);
                    sql_command.Parameters.AddWithValue("@MASTERID", masterid);

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
                    connection.Close(); //close database connection
                }
            }
        }
        public void UpdateMasterChip(string chip, string masterid)
        {
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.master set CHIP = @CHIP" +
                                " WHERE MASTERID = @MASTERID";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    sql_command.CommandTimeout = 0;

                    connection.Open();
                    sql_command.Parameters.AddWithValue("@CHIP", chip);
                    sql_command.Parameters.AddWithValue("@MASTERID", masterid);

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
                    connection.Close(); //close database connection
                }
            }
        }

        //string strtdater, string strtdate, string flagstrtdate,
        public void UpdateSampleFlds(string status, string contract, string projdesc, string projloc, string pcityst, string masterid, string viewcode)
       {
           {
               //When the fields in the Name and Address form are edited update the Sample or Sample hold table
                   SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
               string tblName;
               if (viewcode == "1")
                   { 
                       tblName = "SAMPLE";
                   }
                   else
                   {
                       tblName = "SAMPLE_HOLD";
                   }
               using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
               {
                   string Query = "update " + tblName + " set STATUS = @STATUS" +
                                  ",CONTRACT = @CONTRACT" +
                                  ",PROJDESC = @PROJDESC" +
                                  ",PROJLOC = @PROJLOC" +
                                  ",PCITYST = @PCITYST" +
                                  " WHERE MASTERID = @MASTERID";
                    try
                   {
                       SqlCommand sql_command = new SqlCommand(Query, connection);
                       sql_command.CommandTimeout = 0;

                       connection.Open();
                       sql_command.Parameters.AddWithValue("@STATUS", status);
                       sql_command.Parameters.AddWithValue("@CONTRACT", contract);
                       sql_command.Parameters.AddWithValue("@PROJDESC", projdesc);
                       sql_command.Parameters.AddWithValue("@PROJLOC", projloc);
                       sql_command.Parameters.AddWithValue("@PCITYST", pcityst);
                       sql_command.Parameters.AddWithValue("@MASTERID", masterid);
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
                        connection.Close(); //close database connection
                    }
               }
           }
       }

       
        public void UpdateRespondentFlds(string coltec, string owner, string factoff, string otherresp, string respname, string addr1, string addr2, string addr3, string respname2, string specialnote,
                                   string emailaddr, string webaddr, string rstate, string zipcode, string phone1, string ext1, string fax, string phone2, string ext2, string lag, string respid)
       {
              using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
               {
                   string Query = "update dbo.respondent set COLTEC = @COLTEC" +
                                  ",RESPORG = @RESPORG" +
                                  ",FACTOFF = @FACTOFF" +
                                  ",OTHRRESP = @OTHRRESP" +
                                  ",RESPNAME = @RESPNAME" +
                                  ",ADDR1 = @ADDR1" +
                                  ",ADDR2 = @ADDR2" +
                                  ",ADDR3 = @ADDR3" +
                                  ",RESPNAME2 = @RESPNAME2" +
                                  ",RESPNOTE = @RESPNOTE" +
                                  ",EMAIL = @EMAIL" +
                                  ",WEBURL = @WEBURL" +
                                  ",RSTATE = @RSTATE" +
                                  ",ZIP = @ZIP" +
                                  ",PHONE = @PHONE" +
                                  ",EXT = @EXT" +
                                  ",LAG = @LAG" +
                                  ",FAX = @FAX" +
                                  ",PHONE2 = @PHONE2" +
                                  ",EXT2 = @EXT2" +
                                  " WHERE RESPID = @RESPID";
                   try
                   {
                       SqlCommand sql_command = new SqlCommand(Query, connection);
                       sql_command.CommandTimeout = 0;

                       connection.Open();
                       sql_command.Parameters.AddWithValue("@RESPID", respid);
                       sql_command.Parameters.AddWithValue("@COLTEC", coltec);
                       sql_command.Parameters.AddWithValue("@RESPORG", owner);
                       sql_command.Parameters.AddWithValue("@FACTOFF", factoff);
                       sql_command.Parameters.AddWithValue("@OTHRRESP", otherresp);
                       sql_command.Parameters.AddWithValue("@RESPNAME", respname);
                       sql_command.Parameters.AddWithValue("@ADDR1", addr1);
                       sql_command.Parameters.AddWithValue("@ADDR2", addr2);
                       sql_command.Parameters.AddWithValue("@ADDR3", addr3);
                       sql_command.Parameters.AddWithValue("@RESPNAME2", respname2);
                       sql_command.Parameters.AddWithValue("@RESPNOTE", specialnote);
                       sql_command.Parameters.AddWithValue("@EMAIL", emailaddr);
                       sql_command.Parameters.AddWithValue("@WEBURL", webaddr);
                       sql_command.Parameters.AddWithValue("@RSTATE", rstate);
                       sql_command.Parameters.AddWithValue("@ZIP", zipcode);
                       sql_command.Parameters.AddWithValue("@PHONE", phone1);
                       sql_command.Parameters.AddWithValue("@EXT", ext1);
                       sql_command.Parameters.AddWithValue("@FAX", fax);
                       sql_command.Parameters.AddWithValue("@LAG", lag);
                       sql_command.Parameters.AddWithValue("@PHONE2", phone2);
                       sql_command.Parameters.AddWithValue("@EXT2", ext2);
                       
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
                       connection.Close(); //close database connection
                   }
           }
       }

       /*Delete Respid if it has no associated projects in Sample table*/
       public bool DeleteRespid(string Respid)
       {
           SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
           string usql = "DELETE FROM RESPONDENT WHERE RESPID = @RESPID";
           SqlCommand delete_command = new SqlCommand(usql, sql_connection);
           delete_command.CommandTimeout = 0;

           delete_command.Parameters.AddWithValue("@RESPID", Respid);
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

       public void UpdateSocFlds(string repbldgs, string repunits, string costpu, string masterid)
       {
           {
               using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
               {
                   string Query = "update dbo.soc set RBLDGS = @RBLDGS" +
                                  ",RUNITS = @RUNITS" + ",COSTPU = @COSTPU" +
                                  " WHERE MASTERID = @MASTERID";
                   try
                   {
                       SqlCommand sql_command = new SqlCommand(Query, connection);
                       sql_command.CommandTimeout = 0;

                       connection.Open();
                       sql_command.Parameters.AddWithValue("@RBLDGS", repbldgs);
                       sql_command.Parameters.AddWithValue("@RUNITS", repunits);
                        sql_command.Parameters.AddWithValue("@COSTPU", costpu);
                        sql_command.Parameters.AddWithValue("@MASTERID", masterid);
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
                       connection.Close(); //close database connection
                   }
               }

           }
       } 

       //Check the number of projects for this respid in Sample table.
        public static string ChkRespProjnum(string respid)
       {
           string cntRespid;
           using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
           {
               string sql = "select count(respid) from dbo.SAMPLE where respid = " + GeneralData.AddSqlQuotes(respid);
               SqlCommand command = new SqlCommand(sql, connection);
               command.CommandTimeout = 0;

               try
               {
                   connection.Open();
                   Int32 count = (Int32)command.ExecuteScalar();
                   cntRespid = count.ToString();
               }
               catch (SqlException ex)
               {
                   throw ex;
               }
           }
           return cntRespid;
       }

        //replace Respondent fields with the new Respid entered, if its an existing Respid.
       public static NameAddrResp RespDataRead(string Respid)
       {
           NameAddrResp RespDetails = new NameAddrResp();
           //Connect to database using the parameterized stored procedure.
           using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
           {
               SqlCommand sql_command = new SqlCommand("select * from dbo.RESPONDENT where RESPID=@RESPID", sql_connection);
               sql_command.CommandTimeout = 0;

               sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(Respid);
               try
               {
                   sql_connection.Open();
                   SqlDataReader reader =
                           sql_command.ExecuteReader(CommandBehavior.SingleRow);
                   if (reader.Read())
                   {
                       RespDetails.Respid = reader["Respid"].ToString();
                       RespDetails.Factoff = reader["Factoff"].ToString();
                       RespDetails.Othrresp = reader["Othrresp"].ToString();
                       RespDetails.Addr1 = reader["Addr1"].ToString();
                       RespDetails.Addr2 = reader["Addr2"].ToString();
                       RespDetails.Addr3 = reader["Addr3"].ToString();
                       RespDetails.Zip = reader["Zip"].ToString();
                       RespDetails.Resporg = reader["Resporg"].ToString();
                       RespDetails.Respname = reader["Respname"].ToString();
                       RespDetails.Respname2 = reader["Respname2"].ToString();
                       RespDetails.Respnote = reader["Respnote"].ToString();
                       RespDetails.Phone = reader["Phone"].ToString();
                       RespDetails.Phone2 = reader["Phone2"].ToString();
                       RespDetails.Ext = reader["Ext"].ToString();
                       RespDetails.Ext2 = reader["Ext2"].ToString();
                       RespDetails.Fax = reader["Fax"].ToString();
                       RespDetails.Email = reader["Email"].ToString();
                       RespDetails.Weburl = reader["Weburl"].ToString();
                       RespDetails.Coltec = reader["coltec"].ToString();
                       RespDetails.Colhist = reader["colhist"].ToString();
                   }
                   else
                   {
                       RespDetails = null;
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
               return RespDetails;
           }    
       }



       public void UpdateActive(string id, string active)
       {
          using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
           {
               string Query = "update dbo.sample set ACTIVE = @ACTIVE where id = @id";
               try
               {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    sql_command.CommandTimeout = 0;

                    connection.Open();
                    sql_command.Parameters.AddWithValue("@ID", id);
                    sql_command.Parameters.AddWithValue("@ACTIVE", active);
                    
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
                   connection.Close(); //close database connection
               }
           }
       }

        //Get the last valid Respid, add 1 to it and assign it as new respid.
        public static string GetLastValidRespid()
       {
           string respid = String.Empty;
           SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());
           string sql = "select top 1 RESPID from RESPONDENT where respid < '3999999' order by RESPID desc"; // + GeneralData.AddSqlQuotes(respid);
           SqlCommand command = new SqlCommand(sql, connection);
           command.CommandTimeout = 0;

           try
           {
               connection.Open();
               SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);
               if (reader.Read())
               {
                   respid = reader["RESPID"].ToString().Trim();
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
               return respid;
       }

        //Insert a new respid
        public void InsertNewRespid(string respid, string resporg, string respname, string addr1, string addr2, string addr3, 
                                    string zip, string phone, string ext, string fax, string factoff, string othrresp, string respnote, 
                                    string email, string weburl, string rstate, string lag, string resplock, string centpwd,
                                    string coltec, string colhist, string respname2, string phone2, string ext2, string usrnme, string prgdtm)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.respondent (RESPID, RESPORG, RESPNAME, ADDR1, ADDR2, ADDR3, ZIP, PHONE, EXT, FAX, FACTOFF, OTHRRESP, "
                         + "RESPNOTE, EMAIL, WEBURL, RSTATE, LAG, RESPLOCK, CENTPWD, COLTEC, COLHIST, RESPNAME2, PHONE2, EXT2, USRNME, PRGDTM) "
                         + "Values (@RESPID,  @RESPORG, @RESPNAME, @ADDR1, @ADDR2, @ADDR3, @ZIP, @PHONE, @EXT, @FAX, @FACTOFF, @OTHRRESP, @RESPNOTE, "
                         + "@EMAIL, @WEBURL, @RSTATE, @LAG, @RESPLOCK, @CENTPWD, @COLTEC, @COLHIST, @RESPNAME2, @PHONE2, @EXT2, @USRNME, GETDATE())";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.CommandTimeout = 0;

            insert_command.Parameters.AddWithValue("@RESPID", respid);
            insert_command.Parameters.AddWithValue("@RESPORG", resporg);
            insert_command.Parameters.AddWithValue("@RESPNAME", respname);
            insert_command.Parameters.AddWithValue("@ADDR1", addr1);
            insert_command.Parameters.AddWithValue("@ADDR2", addr2);
            insert_command.Parameters.AddWithValue("@ADDR3", addr3);
            insert_command.Parameters.AddWithValue("@ZIP", zip);
            insert_command.Parameters.AddWithValue("@PHONE", phone);
            insert_command.Parameters.AddWithValue("@EXT", ext);
            insert_command.Parameters.AddWithValue("@FAX", fax);
            insert_command.Parameters.AddWithValue("@FACTOFF", factoff);
            insert_command.Parameters.AddWithValue("@OTHRRESP", othrresp);
            insert_command.Parameters.AddWithValue("@RESPNOTE", respnote);
            insert_command.Parameters.AddWithValue("@EMAIL", email);
            insert_command.Parameters.AddWithValue("@WEBURL", weburl);
            insert_command.Parameters.AddWithValue("@RSTATE", rstate);
            insert_command.Parameters.AddWithValue("@LAG", lag);
            insert_command.Parameters.AddWithValue("@RESPLOCK", resplock);
            insert_command.Parameters.AddWithValue("@CENTPWD", centpwd);
            insert_command.Parameters.AddWithValue("@COLTEC", coltec);
            insert_command.Parameters.AddWithValue("@COLHIST", colhist);
            insert_command.Parameters.AddWithValue("@RESPNAME2", respname2);
            insert_command.Parameters.AddWithValue("@PHONE2", phone2);
            insert_command.Parameters.AddWithValue("@EXT2", ext2);
            insert_command.Parameters.AddWithValue("@USRNME", usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", prgdtm);
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

        //Added for updating Name and Addresss screen with Contractor information
        public static NameAddrFactor ReplWithFactorsF7(string masterid)
        {
            //Factor factor = new Factor();
            NameAddrFactor nameaddrfactor = new NameAddrFactor();
            //Connect to database using the parameterized stored procedure.
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("select * from dbo.FACTORS where MASTERID=@MASTERID", sql_connection);
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@MASTERID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(masterid);
                try
                {
                    sql_connection.Open();
                    SqlDataReader reader =
                            sql_command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        nameaddrfactor.F7resporg = reader["F7Resporg"].ToString();
                        nameaddrfactor.F7respname = reader["F7Respname"].ToString();
                        nameaddrfactor.F7addr1 = reader["F7Addr1"].ToString();
                        nameaddrfactor.F7addr2 = reader["F7Addr2"].ToString();
                        nameaddrfactor.F7addr3 = reader["F7Addr3"].ToString();
                        nameaddrfactor.F7zip = reader["F7Zip"].ToString();
                        nameaddrfactor.F7phone = reader["F7Phone"].ToString();
                        nameaddrfactor.F7email = reader["F7Email"].ToString();
                        nameaddrfactor.F7weburl = reader["F7Weburl"].ToString();
                        nameaddrfactor.Timezone = GeneralDataFuctions.GetTimezone(nameaddrfactor.Rstate);

                    }
                    else
                    {
                        nameaddrfactor = null;
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
                return nameaddrfactor;
            }
        }

        //update the Respid for the corresponding ID.
        public void updateRespid4ID(string id, string respid)
        {
           bool in_hold = CheckIdSampleHold(id);
           using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.SAMPLE set RESPID = @RESPID WHERE ID = @ID";
                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);
                    sql_command.CommandTimeout = 0;

                    connection.Open();
                    sql_command.Parameters.AddWithValue("@RESPID", respid);
                    sql_command.Parameters.AddWithValue("@ID", id);

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
                    connection.Close(); //close database connection
                }

                if (in_hold)
                {
                    Query = "update dbo.SAMPLE_HOLD set RESPID = @RESPID WHERE ID = @ID";
                    try
                    {
                        SqlCommand sql_command = new SqlCommand(Query, connection);
                        connection.Open();
                        sql_command.Parameters.AddWithValue("@RESPID", respid);
                        sql_command.Parameters.AddWithValue("@ID", id);

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
                        connection.Close(); //close database connection
                    }
                }


            }
        }
    }
}