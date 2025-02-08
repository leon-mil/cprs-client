/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.MfInitialSearchData.cs	    	

Programmer:         Cestine Gill

Creation Date:      04/07/2016

Inputs:             dbo.sp_MFInitialSearch, dbo.sp_MFInitialSearchExact

Parameters:	        None 

Outputs:	        Multi Family Name and Address Search data	

Description:	    This function establishes the data connection and reads in 
                    the search data based on the search criteria entered on the
 *                  screen

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
    public class MfInitialSearchData
    {
        /*Get MF Initial search data */
        public DataTable GetMFInitialData(string id, string psu, string bpoid, string owner1, string owner2, string owner3,
            string contact1, string contact2, string contact3,
            string projdesc1, string projdesc2, string projdesc3,
            string projloc1, string projloc2, string projloc3,
            string phone1, string phone2,
            string seldate1, string seldateOperator, string seldate2, bool isample, bool exact)
        {

            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sp_name;

                if (isample)
                {
                    sp_name = "dbo.sp_MFInitialSearch";
                    if (exact)
                        sp_name = "dbo.sp_MFInitialSearchExact";
                }
                else
                {
                    sp_name = "dbo.sp_MFInitialSearchInPresample";
                    if (exact)
                        sp_name = "dbo.sp_MFInitialSearchExactInPresample";
                }

                SqlCommand sql_command = new SqlCommand(sp_name, sql_connection);

                sql_command.CommandType = CommandType.StoredProcedure;
                if (!isample)
                    sql_command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = id;
                sql_command.Parameters.Add("@PSU", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(psu);
                sql_command.Parameters.Add("@BPOID", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(bpoid);

                sql_command.Parameters.Add("@OWNER1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner1);
                sql_command.Parameters.Add("@OWNER2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner2);
                sql_command.Parameters.Add("@OWNER3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner3);

                sql_command.Parameters.Add("@CONTACT1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact1);
                sql_command.Parameters.Add("@CONTACT2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact2);
                sql_command.Parameters.Add("@CONTACT3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(contact3);

                sql_command.Parameters.Add("@PROJDESC1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projdesc1);
                sql_command.Parameters.Add("@PROJDESC2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projdesc2);
                sql_command.Parameters.Add("@PROJDESC3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projdesc3);

                sql_command.Parameters.Add("@PROJLOC1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projloc1);
                sql_command.Parameters.Add("@PROJLOC2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projloc2);
                sql_command.Parameters.Add("@PROJLOC3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projloc3);

                sql_command.Parameters.Add("@PHONE1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(phone1);
                sql_command.Parameters.Add("@PHONE2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(phone2);

                sql_command.Parameters.Add("@SELDATE1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldate1);
                sql_command.Parameters.Add("@SELDATEOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldateOperator);
                sql_command.Parameters.Add("@SELDATE2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldate2);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        /*create a empty table */
        public DataTable GetEmptyTable()
        {
            // Here we create a DataTable with 7 columns.

            DataTable table = new DataTable();
            table.Columns.Add("MASTERID", typeof(int));
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("FIN", typeof(string));
            table.Columns.Add("STATUS CODE", typeof(string));
            table.Columns.Add("DESCRIPTION", typeof(string));
            table.Columns.Add("LOCATION", typeof(string));
            table.Columns.Add("ORGANIZATION", typeof(string));
            table.Columns.Add("CONTACT", typeof(string));
            table.Columns.Add("PHONE", typeof(string));
            table.Columns.Add("PHONE2", typeof(string));

            return table;
        }

    }
}
