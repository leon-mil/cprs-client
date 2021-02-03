/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.RespondentData.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/05/2015
Inputs:             respid, respondent record
Parameters:	        None 
Outputs:	        Respondent data	
Description:	    data layer to add, update respondent
Detailed Design:    None 
Other:	            Called by: frmC700
 
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
    public class RespondentData
    {
        public Respondent GetRespondentData(string respid)
        {
            /* find out database table name */
            string db_table = "dbo.Respondent";

            Respondent resp = new Respondent(respid);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM " + db_table + " WHERE RESPID = " + GeneralData.AddSqlQuotes(respid);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    resp.Resporg = reader["RESPORG"].ToString().Trim(); ;
                    resp.Respname = reader["RESPNAME"].ToString().Trim(); ;
                    resp.Respname2 = reader["RESPNAME2"].ToString().Trim(); ;
                    resp.Addr1 = reader["ADDR1"].ToString().Trim();
                    resp.Addr2 = reader["ADDR2"].ToString().Trim();
                    resp.Addr3 = reader["ADDR3"].ToString().Trim();
                    resp.Zip = reader["Zip"].ToString().Trim();
                    resp.Phone = reader["PHONE"].ToString().Trim();
                    resp.Phone2 = reader["PHONE2"].ToString().Trim();
                    resp.Fax = reader["FAX"].ToString().Trim();
                    resp.Ext = reader["EXT"].ToString().Trim();
                    resp.Ext2 = reader["EXT2"].ToString().Trim();
                    resp.Factoff = reader["FACTOFF"].ToString().Trim();
                    resp.Othrresp = reader["OTHRRESP"].ToString().Trim();
                    resp.Respnote = reader["RESPNOTE"].ToString().Trim();
                    resp.Rstate = reader["RSTATE"].ToString().Trim(); ;
                    resp.Email = reader["EMAIL"].ToString().Trim(); ;
                    resp.Weburl = reader["Weburl"].ToString().Trim(); ;
                    resp.Lag = (int)reader["LAG"];
                    resp.Centpwd = reader["CENTPWD"].ToString().Trim(); ;
                    resp.Coltec = reader["COLTEC"].ToString().Trim(); ;
                    resp.Colhist = reader["COLHIST"].ToString().Trim(); ;

                    resp.IsModified = false;
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

            return resp;

        }

        /*Save Respondent data */
        public bool SaveRespondentData(Respondent resp)
        {
            /* find out database table name */
            string db_table =  "dbo.Respondent";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                                "RESPORG = @RESPORG, " +
                                "RESPNAME = @RESPNAME, " +
                                "RESPNAME2 = @RESPNAME2, " +
                                "ADDR1 = @ADDR1, " +
                                "ADDR2 = @ADDR2, " +
                                "ADDR3 = @ADDR3, " +
                                "ZIP = @ZIP, " +
                                "PHONE = @PHONE, " +
                                "PHONE2 = @PHONE2, " +
                                "FAX = @FAX, " +
                                "EXT = @EXT, " +
                                "EXT2 = @EXT2, " +
                                "FACTOFF = @FACTOFF, " +
                                "COLTEC = @COLTEC, " +
                                "COLHIST = @COLHIST, " +
                                "OTHRRESP = @OTHRRESP, " +
                                "RESPNOTE = @RESPNOTE, " +
                                "RSTATE = @RSTATE, " +
                                "LAG = @LAG, " +
                                "EMAIL = @EMAIL, " +
                                 "WEBURL = @WEBURL " +
                                "WHERE RESPID = @RESPID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.CommandTimeout = 0;

                update_command.Parameters.AddWithValue("@RESPID", resp.Respid);
                update_command.Parameters.AddWithValue("@RESPORG", resp.Resporg);
                update_command.Parameters.AddWithValue("@RESPNAME", resp.Respname);
                update_command.Parameters.AddWithValue("@RESPNAME2", resp.Respname2);
                update_command.Parameters.AddWithValue("@ADDR1", resp.Addr1);
                update_command.Parameters.AddWithValue("@ADDR2", resp.Addr2);
                update_command.Parameters.AddWithValue("@ADDR3", resp.Addr3);
                update_command.Parameters.AddWithValue("@ZIP", resp.Zip);
                update_command.Parameters.AddWithValue("@PHONE", resp.Phone);
                update_command.Parameters.AddWithValue("@PHONE2", resp.Phone2);
                update_command.Parameters.AddWithValue("@FAX", resp.Fax);
                update_command.Parameters.AddWithValue("@Ext", resp.Ext);
                update_command.Parameters.AddWithValue("@Ext2", resp.Ext2);
                update_command.Parameters.AddWithValue("@FACTOFF", resp.Factoff);
                update_command.Parameters.AddWithValue("@COLTEC", resp.Coltec);
                update_command.Parameters.AddWithValue("@COLHIST", resp.Colhist);
                update_command.Parameters.AddWithValue("@OTHRRESP", resp.Othrresp);
                update_command.Parameters.AddWithValue("@RESPNOTE", resp.Respnote);
                update_command.Parameters.AddWithValue("@RSTATE", resp.Rstate);
                update_command.Parameters.AddWithValue("@LAG", resp.Lag);
                update_command.Parameters.AddWithValue("@EMAIL", resp.Email);
                update_command.Parameters.AddWithValue("@WEBURL", resp.Weburl);

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

    }

}
