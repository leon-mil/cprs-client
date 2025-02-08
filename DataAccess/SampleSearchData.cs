/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : SampleSearchData.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frmMaster.cs
Change Request: 
Specification : Sample Search Specifications  
Rev History   : See Below

Other         : N/A
************************************************************************
Modified      : January 7, 2016
Modified By   : Kevin Montgomery
Description   : Change Search paramaters, Use FIN instead of Dodgenum
                Change Search parameters,  Drop PSUPLACESCHED
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
    public class SampleSearchData
    {
        public DataTable GetSampleSearchData(string id, string respid, string fin, string finOperator, string source, 
                                             string fipstate, string owner, string seldate, string seldateOperator, string seldate1,
                                             string newtc, string newtcOperator, string newtc1, string projselv, string projselvOperator,
                                             string projselv1, string rvitm5c, string rvitm5cOperator, string rvitm5c1,
                                             string runits, string runitsOperator, string runits1, string psu, string bpoid)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_SampleSearchx", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);
                sql_command.Parameters.Add("@RESPID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(respid);

                sql_command.Parameters.Add("@FIN", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(fin);
                sql_command.Parameters.Add("@FINOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(finOperator);

                sql_command.Parameters.Add("@SOURCE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(source);
                sql_command.Parameters.Add("@FIPSTATE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(fipstate);
                sql_command.Parameters.Add("@OWNER", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner);

                sql_command.Parameters.Add("@SELDATE1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldate);
                sql_command.Parameters.Add("@SELDATEOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldateOperator);
                sql_command.Parameters.Add("@SELDATE2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldate1);

                sql_command.Parameters.Add("@NEWTC1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);
                sql_command.Parameters.Add("@NEWTCOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtcOperator);
                sql_command.Parameters.Add("@NEWTC2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc1);

                sql_command.Parameters.Add("@PROJSELV1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(projselv);
                sql_command.Parameters.Add("@PROJSELVOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(projselvOperator);
                sql_command.Parameters.Add("@PROJSELV2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(projselv1);

                sql_command.Parameters.Add("@RVITM5C1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(rvitm5c);
                sql_command.Parameters.Add("@RVITM5COPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(rvitm5cOperator);
                sql_command.Parameters.Add("@RVITM5C2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(rvitm5c1);

                sql_command.Parameters.Add("@RUNITS1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(runits);
                sql_command.Parameters.Add("@RUNITSOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(runitsOperator);
                sql_command.Parameters.Add("@RUNITS2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(runits1);

                sql_command.Parameters.Add("@PSU", SqlDbType.Char).Value = GeneralData.NullIfEmpty(psu);
                sql_command.Parameters.Add("@BPOID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(bpoid);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        public DataTable GetEmptyTable()
        {
            // Here we create a DataTable with 16 columns.

            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("FIN", typeof(string));
            table.Columns.Add("RESPID", typeof(string));
            table.Columns.Add("STATUS", typeof(string));
            table.Columns.Add("OWNER", typeof(string));

            table.Columns.Add("NEWTC", typeof(string));
            table.Columns.Add("FIPSTATE", typeof(string));         
            table.Columns.Add("SELDATE", typeof(string));
            table.Columns.Add("PROJSELV", typeof(int));

            table.Columns.Add("RVITM5C", typeof(int));
            table.Columns.Add("FWGT", typeof(float));
            table.Columns.Add("STRTDATE", typeof(string));
            table.Columns.Add("COMPDATE", typeof(string));
            table.Columns.Add("FUTCOMPD", typeof(string));

            table.Columns.Add("RBLDGS", typeof(int));
            table.Columns.Add("RUNITS", typeof(int));

            return table;
        }
    }
}
