/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.DcpDupData.cs	    	
Programmer:         Diane Musachio
Creation Date:      3/29/2018
Inputs:             none
Parameters:	        fin, username, value
Outputs:	        none	
Description:	    gets research data, master data, updates locks, keeps

Detailed Design:    DCP Duplication Research Design Document
Other:	            Called by: frmDcpDup.cs
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
    public class DcpDupData
    {
        /*gets data from dbo.research*/
        public DataTable GetResearchData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqls = @"Select FIN, PRIMST, PRIMCNTY, OWNER, PROJSELV, TVALUE, 
                         PRIMTC, SELDATE, MRN, MTF, DDASTART, DDASEQ, DELCOD, SAMP, KEEP, LOCKID
                        from dbo.Research order by FIN";

                SqlCommand sql_command = new SqlCommand(sqls, sql_connection);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*gets data from dbo.master*/
        public DataTable GetMasterRecord(string fin)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqls = @"Select FIN, FIPSTATE, DODGECOU, OWNER, PROJSELV, TVALUE,
                         NEWTC, SELDATE, MRN, MTF, DDASTART, DDASEQ
                        from dbo.Master where FIN = '" + fin + "'";

                SqlCommand sql_command = new SqlCommand(sqls, sql_connection);
                sql_command.Parameters.AddWithValue("@FIN", fin);

                // Create a DataAdapter to run the command and fill the DataTable
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*Update lock field in research*/
        public bool UpdateLock(string username)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string sqls = "UPDATE dbo.Research SET " +
                                "LOCKID = @LOCKID";

                SqlCommand sql_command = new SqlCommand(sqls, sql_connection);
                sql_command.Parameters.AddWithValue("@LOCKID", username);

                try
                {
                    sql_command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }

        /*Update keep field  in research */
        public bool UpdateKeep(string fin, string value)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string sqls = "UPDATE dbo.Research SET " +
                                "KEEP = @VALUE where FIN = @FIN";

                SqlCommand sql_command = new SqlCommand(sqls, sql_connection);
                sql_command.Parameters.AddWithValue("@FIN", fin);
                sql_command.Parameters.AddWithValue("@VALUE", value);

                try
                {
                    sql_command.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }
        }
    }
}
