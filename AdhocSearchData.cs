/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : AdhocSearchData.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frmAdhoc.cs
Change Request: 
Specification : AdhocSearch Specifications  
Rev History   : See Below

Other         : N/A
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace CprsDAL
{
    public class AdhocSearchData
    {
        /*Get Adhoc search data */
        public DataTable GetAdhocData(string projdesc1, string projdesc2, string projdesc3, string projloc1, string projloc2, string projloc3, string pcityst1,
                                             string pcityst2, string pcityst3, string fipstate, string county, string factortype, string factor1, string factor2,
                                             string factor3, string owner, string newtc, string newtcOperator,
                                             string newtc1, int isSample)
        {
            
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_AdhocSearch", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@PROJDESC1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projdesc1);
                sql_command.Parameters.Add("@PROJDESC2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projdesc2);
                sql_command.Parameters.Add("@PROJDESC3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projdesc3);
                sql_command.Parameters.Add("@PROJLOC1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projloc1);
                sql_command.Parameters.Add("@PROJLOC2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projloc2);
                sql_command.Parameters.Add("@PROJLOC3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projloc3);
                sql_command.Parameters.Add("@PCITYST1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(pcityst1);
                sql_command.Parameters.Add("@PCITYST2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(pcityst2);
                sql_command.Parameters.Add("@PCITYST3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(pcityst3);
                sql_command.Parameters.Add("@FIPSTATE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(fipstate);
                sql_command.Parameters.Add("@DODGECOU", SqlDbType.Char).Value = GeneralData.NullIfEmpty(county);
                sql_command.Parameters.Add("@FACTORTYPE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(factortype);
                sql_command.Parameters.Add("@FACTOR1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(factor1);
                sql_command.Parameters.Add("@FACTOR2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(factor2);
                sql_command.Parameters.Add("@FACTOR3", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(factor3);
                sql_command.Parameters.Add("@OWNER", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner);
                sql_command.Parameters.Add("@NEWTC1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);
                sql_command.Parameters.Add("@NEWTCOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtcOperator);
                sql_command.Parameters.Add("@NEWTC2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc1);
                sql_command.Parameters.Add("@ISSAMPLE", SqlDbType.TinyInt).Value = isSample;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }
        
        /*create a empty table */
        public DataTable GetEmptyTable()
        {
            // Here we create a DataTable with 15 columns.

            DataTable table = new DataTable();
            table.Columns.Add("FIN", typeof(string));
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("STATUS", typeof(string));
            table.Columns.Add("OWNER", typeof(string));
            table.Columns.Add("NEWTC", typeof(string));
            table.Columns.Add("SELDATE", typeof(string));
            table.Columns.Add("DESCRIPTION", typeof(string));
            table.Columns.Add("LOCATION", typeof(string));
            table.Columns.Add("CITY", typeof(string));
            table.Columns.Add("COUNTY", typeof(string));
            table.Columns.Add("FOWNER", typeof(string));
            table.Columns.Add("FARCHITECT", typeof(string));
            table.Columns.Add("FENGINEER", typeof(string));
            table.Columns.Add("FCONTRACTOR", typeof(string));

            return table;
        }

        /*Get county data for afipstate, data will be used for a combo */
        public DataTable GetCountyDataForCombo(string fipstate)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_CountyByFipstate", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@fipstate", SqlDbType.Char).Value = fipstate;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr[0] = -1;
                dr[1] = " ";
                dt.Rows.InsertAt(dr, 0);
            }

            return dt;
        }

      
    }
}
