/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : DodgeSlipData.cs

 Programmer    : Diane Musachio

 Creation Date : 3/30/2015

 Inputs        : DCPSlips data table
                 DCPNotes data table

 Paramaters    : ID for : GetSlips, 
                 Masterid for : Gettitle, Getvalue, Getadd, Getitem, Getreporter

 Output        : Data from relevant fields for GetSlips class
                 Lists of results for Gettitle, Getvalue, Getadd, Getitem, Getreporter 
                   
 Description   : These classes get data from dcpnotes and dcpslips using the following stored procedures: dbo.sp_SlipsDisplay and dbo.sp_GetDcpnotes
 
 Detail Design : Detailed User Requirements for Slip Display Screen
 
 Forms using this code:  frmSlipDisplay
 
 Called from   : n/a

 Other         : 

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/

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
    public class DodgeSlipData
    {

        /* This class uses an SQL connection to access the stored procedure dbo.sp_SlipsDisplay 
           and returns the data from a specific Id to appropriate program*/

        public Slips GetSlips(string Id)
        {
            Slips slips = new Slips();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;
                sql_command = new SqlCommand("dbo.sp_SlipsDisplay", connection);
                sql_command.CommandTimeout = 0;

                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Id;

                try
                {
                    connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        slips.Masterid = (int)reader["Masterid"];
                        slips.Id = reader["Id"].ToString();
                        slips.Title = reader["Title"].ToString();
                        slips.Valuation = reader["Valuation"].ToString();
                        slips.Taddr1 = reader["Taddr1"].ToString();
                        slips.Taddr2 = reader["Taddr2"].ToString();
                        slips.Tcity = reader["Tcity"].ToString();
                        slips.Tcounty = reader["Tcounty"].ToString();
                        slips.Tstate = reader["Tstate"].ToString();
                        slips.Tzip = reader["Tzip"].ToString();
                        slips.Tstrtdate = reader["Tstrtdate"].ToString();
                        slips.Tcompdate = reader["Tcompdate"].ToString();
                        slips.Worktype = reader["Worktype"].ToString();
                        slips.Contmeth = reader["Contmeth"].ToString();
                        slips.Contnbr1 = reader["Contnbr1"].ToString();
                        slips.Contnbr2 = reader["Contnbr2"].ToString();
                        slips.Notice = reader["Notice"].ToString();
                        slips.Fcode = reader["Fcode"].ToString();
                        slips.Ownclass = reader["Ownclass"].ToString();
                        slips.Storyabv = reader["Storyabv"].ToString();
                        slips.Storybel = reader["Storybel"].ToString();
                        slips.Tsqrarea = reader["Tsqrarea"].ToString();
                        slips.Numbldgs = reader["Numbldgs"].ToString();
                        slips.Subcont = reader["Subcont"].ToString();
                        slips.Projstat = reader["Projstat"].ToString();
                        slips.Projgroup = reader["Projgroup"].ToString();
                        slips.Pptype = reader["Pptype"].ToString();
                        slips.Ptype1 = reader["Ptype1"].ToString();
                        slips.Ptype2 = reader["Ptype2"].ToString();
                        slips.Ptype3 = reader["Ptype3"].ToString();
                        slips.Ptype4 = reader["Ptype4"].ToString();
                        slips.Ptype5 = reader["Ptype5"].ToString();
                        slips.Ptype6 = reader["Ptype6"].ToString();
                        slips.Ptype7 = reader["Ptype7"].ToString();
                        slips.Ptype8 = reader["Ptype8"].ToString();
                        slips.Ptype9 = reader["Ptype9"].ToString();
                        slips.Ptype10 = reader["Ptype10"].ToString();
                        slips.Ptype11 = reader["Ptype11"].ToString();
                        slips.Ptype12 = reader["Ptype12"].ToString();
                        slips.Ptype13 = reader["Ptype13"].ToString();
                        slips.Ptype14 = reader["Ptype14"].ToString();
                        slips.Ptype15 = reader["Ptype15"].ToString();
                        slips.Ptype16 = reader["Ptype16"].ToString();
                        slips.Ptype17 = reader["Ptype17"].ToString();
                        slips.Ptype18 = reader["Ptype18"].ToString();
                        slips.Ptype19 = reader["Ptype19"].ToString();
                        slips.Ptype20 = reader["Ptype20"].ToString();
                    }
                    else
                    {
                        slips = null;
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

            return slips;
        }

        /* This class uses an SQL connection to access the stored procedure sp_GetDcpnotes 
           and returns a list of title data from a specific Masterid to appropriate program*/

        public List<DodgeNotes.Title> Gettitle(int masterid)
        {
            List<DodgeNotes.Title> Titlelist = new List<DodgeNotes.Title>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;

                sql_command = new SqlCommand("dbo.sp_GetDcpnotes", connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@MASTERID", SqlDbType.Int).Value = masterid;
                sql_command.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = "TLE";

                try
                {
                    connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    while (reader.Read())
                    {
                        DodgeNotes.Title tle = new DodgeNotes.Title();
                        tle.Titlenotes = reader["NOTE"].ToString();
                        Titlelist.Add(tle);
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
            }

            return Titlelist;
        }

        /* This class uses an SQL connection to access the stored procedure sp_GetDcpnotes
            and returns a list of value data from a specific Masterid to appropriate program*/

        public List<DodgeNotes.Value> Getvalue(int masterid)
        {
            List<DodgeNotes.Value> Valuelist = new List<DodgeNotes.Value>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;

                sql_command = new SqlCommand("dbo.sp_GetDcpnotes", connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@MASTERID", SqlDbType.Int).Value = masterid;
                sql_command.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = "VAL";

                try
                {
                    connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    while (reader.Read())
                    {
                        DodgeNotes.Value val = new DodgeNotes.Value();
                        val.Valuenotes = reader["NOTE"].ToString();
                        Valuelist.Add(val);
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
            }

            return Valuelist;
        }

        /* This class uses an SQL connection to access the stored procedure sp_GetDcpnotes
            and returns a list of additional data from a specific Masterid to appropriate program*/

        public List<DodgeNotes.Add> Getadd(int masterid)
        {
            List<DodgeNotes.Add> Addlist = new List<DodgeNotes.Add>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;
                sql_command = new SqlCommand("dbo.sp_GetDcpnotes", connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@MASTERID", SqlDbType.Int).Value = masterid;
                sql_command.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = "ADD";

                try
                {
                    connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    while (reader.Read())
                    {
                        DodgeNotes.Add add = new DodgeNotes.Add();
                        add.Addnotes = reader["NOTE"].ToString();
                        Addlist.Add(add);
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
            }

            return Addlist;
        }

        /* This class uses an SQL connection to access the stored procedure sp_GetDcpnotes
            and returns a list of item data from a specific Masterid to appropriate program*/

        public List<DodgeNotes.Item> Getitem(int masterid)
        {
            List<DodgeNotes.Item> Itemlist = new List<DodgeNotes.Item>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;
                sql_command = new SqlCommand("dbo.sp_GetDcpnotes", connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@MASTERID", SqlDbType.Int).Value = masterid;
                sql_command.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = "ITM";

                try
                {
                    connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    while (reader.Read())
                    {
                        DodgeNotes.Item itm = new DodgeNotes.Item();
                        itm.Itemnotes = reader["NOTE"].ToString();
                        Itemlist.Add(itm);
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
            }

            return Itemlist;
        }

        /* This class uses an SQL connection to access the stored procedure sp_GetDcpnotes
            and returns a list of reporter data from a specific Masterid to appropriate program*/

        public List<DodgeNotes.Reporter> Getreporter(int masterid)
        {
            List<DodgeNotes.Reporter> Reporterlist = new List<DodgeNotes.Reporter>();

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command;
                sql_command = new SqlCommand("dbo.sp_GetDcpnotes", connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@MASTERID", SqlDbType.Int).Value = masterid;
                sql_command.Parameters.Add("@TYPE", SqlDbType.NVarChar).Value = "REP";

                try
                {
                    connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader();

                    while (reader.Read())
                    {
                        DodgeNotes.Reporter rep = new DodgeNotes.Reporter();
                        rep.Reporternotes = reader["NOTE"].ToString();
                        Reporterlist.Add(rep);
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
            }

            return Reporterlist;
        }

    }
}
