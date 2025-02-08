/************************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.RespAddrData.cs	    	

Programmer      : Diane Musachio

Creation Date   : 08/27/2015

Inputs          : Respid

Parameters      : None 

Outputs         : Respondent Address data	

Description     : These classes establish the data connections and read in 
                  the data, based upon the respid, from the Resp Proj Listing View
                  and the sp_Rupdates stored procedure

Detailed Design : None 

Other           : Called by: frmRespAddrUpdate
 
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
    //Uses stored procedure to retrieve data for the respondent

    public class RespAddrData
    {
        public RespAddr GetRespAddr(string respid)
        {
            RespAddr respaddr = new RespAddr();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                //SqlConnection connectionString;
                SqlCommand sql_command;

                sql_command = new SqlCommand("dbo.sp_Rupdates", connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = respid;

                try
                {
                    connection.Open();

                    SqlDataReader reader =
                        sql_command.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        respaddr.Respid = reader["Respid"].ToString();
                        respaddr.Resporg = reader["Resporg"].ToString();
                        respaddr.Respname = reader["Respname"].ToString();
                        respaddr.Respname2 = reader["Respname2"].ToString();
                        respaddr.Respnote = reader["Respnote"].ToString();
                        respaddr.Factoff = reader["Factoff"].ToString();
                        respaddr.Othrresp = reader["Othrresp"].ToString();
                        respaddr.Addr1 = reader["Addr1"].ToString();
                        respaddr.Addr2 = reader["Addr2"].ToString();
                        respaddr.Addr3 = reader["Addr3"].ToString();
                        respaddr.Phone = reader["Phone"].ToString();
                        respaddr.Phone2 = reader["Phone2"].ToString();
                        respaddr.Ext = reader["Ext"].ToString();
                        respaddr.Ext2 = reader["Ext2"].ToString();
                        respaddr.Fax = reader["Fax"].ToString();
                        respaddr.Zip = reader["Zip"].ToString();
                        respaddr.Email = reader["Email"].ToString();
                        respaddr.Web = reader["Weburl"].ToString();
                        respaddr.Lag = (int)reader["Lag"];
                        respaddr.Rstate = reader["Rstate"].ToString();
                        respaddr.Resplock = reader["Resplock"].ToString();
                        respaddr.Coltec = reader["Coltec"].ToString();
                        respaddr.Colhist = reader["Colhist"].ToString();
                    }
                    else
                    {
                        respaddr = null;
                    }

                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    connection.Close();
                }
            }
            return respaddr;
        }
    }

    //Retrieves project description data and sorts it to display in project tab

    public class GetProjDesc
    {

        public DataTable GetProjDescTable(string respid)
        {
            DataTable dt = new DataTable();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("select * from dbo.RESP_PROJ_LIST where RESPID=@RESPID ORDER BY ID ASC", connection);

                sql_command.Parameters.Add("@RESPID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(respid);

                // Create a DataAdapter to run the command and fill the DataTable

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }
        //Retrieve counts from sample
        public DataTable GetInactiveCounts(string respid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                    string Query = @"select RESPID
                     from dbo.SAMPLE  where RESPID = @RESPID and ACTIVE = 'I' and Status = '4'";

                    using (SqlCommand cmd = new SqlCommand(Query, sql_connection))
                    {
                    cmd.Parameters.AddWithValue("@RESPID", respid);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }

                    return dt;

            }
        }
    }

    ////Retrieves history listing data and sorts it to display in history tab

   
    public class UpdateResp
    {
        public RespAddr UpdateRespData(string respid, string respname, string respname2, string resporg,
            string respnote, string factoff, string othrresp, string addr1, string addr2,
            string addr3, string phone, string phone2, string ext, string ext2, string fax, string zip, string email,
            string weburl, string lag, string rstate, string coltec)
        {
            RespAddr respaddrup = new RespAddr();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string Query = "update dbo.respondent set RESPNAME = @RESPNAME" +
                               ",RESPNAME2 = @RESPNAME2" +
                               ",RESPORG = @RESPORG" +
                               ",RESPNOTE = @RESPNOTE" +
                               ",FACTOFF = @FACTOFF" +
                               ",OTHRRESP = @OTHRRESP" +
                               ",ADDR1 = @ADDR1" +
                               ",ADDR2 = @ADDR2" +
                               ",ADDR3 = @ADDR3" +
                               ",PHONE = @PHONE" +
                               ",PHONE2 = @PHONE2" +
                               ",EXT = @EXT" +
                               ",EXT2 = @EXT2" +
                               ",FAX = @FAX" +
                               ",ZIP = @ZIP" +
                               ",EMAIL = @EMAIL" +
                               ",WEBURL = @WEBURL" +
                               ",LAG = @LAG" +
                               ",RSTATE = @RSTATE" +
                               ",COLTEC = @COLTEC" +
                               " WHERE RESPID = @RESPID";

                try
                {
                    SqlCommand sql_command = new SqlCommand(Query, connection);

                    connection.Open();

                    sql_command.Parameters.AddWithValue("@RESPID", respid);
                    sql_command.Parameters.AddWithValue("@RESPNAME", respname);
                    sql_command.Parameters.AddWithValue("@RESPNAME2", respname2);
                    sql_command.Parameters.AddWithValue("@RESPORG", resporg);
                    sql_command.Parameters.AddWithValue("@RESPNOTE", respnote);
                    sql_command.Parameters.AddWithValue("@FACTOFF", factoff);
                    sql_command.Parameters.AddWithValue("@OTHRRESP", othrresp);
                    sql_command.Parameters.AddWithValue("@ADDR1", addr1);
                    sql_command.Parameters.AddWithValue("@ADDR2", addr2);
                    sql_command.Parameters.AddWithValue("@ADDR3", addr3);
                    sql_command.Parameters.AddWithValue("@PHONE", phone);
                    sql_command.Parameters.AddWithValue("@EXT", ext);
                    sql_command.Parameters.AddWithValue("@PHONE2", phone2);
                    sql_command.Parameters.AddWithValue("@EXT2", ext2);
                    sql_command.Parameters.AddWithValue("@FAX", fax);
                    sql_command.Parameters.AddWithValue("@ZIP", zip);
                    sql_command.Parameters.AddWithValue("@EMAIL", email);
                    sql_command.Parameters.AddWithValue("@WEBURL", weburl);
                    sql_command.Parameters.AddWithValue("@LAG", lag);
                    sql_command.Parameters.AddWithValue("@RSTATE", rstate);
                    sql_command.Parameters.AddWithValue("@COLTEC", coltec);

                    SqlDataReader reader = sql_command.ExecuteReader();

                    while (reader.Read())
                    {
                    }

                    connection.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
            return respaddrup;
        }

    }

}