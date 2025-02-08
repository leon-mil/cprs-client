/***********************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.NameAddrAuditData.cs	    	

Programmer      : Srini Natarajan

Creation Date   : 12/09/2015

Inputs          : dbo.sp_respaudit

Parameters      : None 

Outputs         : data table

Description     : 	   

Detailed Design : None 

Other           : Called by: frmRespAudit.cs
 
Revision History:	
************************************************************************************
Modified Date   : 8/27/2015 
Modified By     : Diane Musachio
Keyword         : 20150827dm 
Change Request  : None
Description     : Add routine AddRespAuditData 
************************************************************************************/
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
    
    public class NameAddrAuditData
    {

    public static void AddNameRspAudit(string Respid, string Varnme, string Oldval, string Newval, string Usrnme, DateTime Prgdtm)    
        {
            if (Oldval.Trim() == Newval.Trim())
                return;

            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.rspaudit (respid, varnme, oldval, newval, usrnme, prgdtm)"
                            + " Values (@RESPID, @VARNME, @OLDVAL, @NEWVAL, @USRNME, @PRGDTM)";

            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@RESPID", Respid);
            insert_command.Parameters.AddWithValue("@VARNME", Varnme);
            insert_command.Parameters.AddWithValue("@OLDVAL", Oldval.Trim());
            insert_command.Parameters.AddWithValue("@NEWVAL", Newval.Trim());
            insert_command.Parameters.AddWithValue("@USRNME", Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", Prgdtm);

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

    public static void AddNameCprAudit(string id, string Varnme, string Oldflag, string Oldval, string Newflag, string Newval, string Usrnme, DateTime Prgdtm)
    {
        if (Oldval.Trim() == Newval.Trim() && Oldflag == Newflag) return;

        SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

        string isql = "insert dbo.cpraudit (id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm)"
                        + " Values (@ID, @VARNME, @OLDFLAG, @OLDVAL, @NEWFLAG, @NEWVAL, @USRNME, @PRGDTM)";
        SqlCommand insert_command = new SqlCommand(isql, sql_connection);
        insert_command.Parameters.AddWithValue("@ID", id);
        insert_command.Parameters.AddWithValue("@VARNME", Varnme);
        insert_command.Parameters.AddWithValue("@OLDFLAG", Oldflag);
        insert_command.Parameters.AddWithValue("@OLDVAL", Oldval.Trim());
        insert_command.Parameters.AddWithValue("@NEWFLAG", Newflag);
        insert_command.Parameters.AddWithValue("@NEWVAL", Newval.Trim());
        insert_command.Parameters.AddWithValue("@USRNME", Usrnme);
        insert_command.Parameters.AddWithValue("@PRGDTM", Prgdtm);
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

    }
}
