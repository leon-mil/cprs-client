/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : C700SearchData.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frmC700Srch.cs
Change Request: 
Specification : C700 Search Specifications  
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
    public class C700SearchData
    {
        /*Get C700 Search Data */
        public DataTable GetC700SearchData(string id, string respid, string source, string fipstate, string owner, string status, string active, string seldate,
                                            string seldateOperator, string seldate1, string newtc, string newtcOperator, string newtc1, string rvitm5c, 
                                            string rvitm5cOperator,string rvitm5c1, string item6, string item6Operator, string item61, string capexp, 
                                            string capexpOperator, string capexp1, string runits, string runitsOperator, string runits1, string costpu,
                                            string costpuOperator, string costpu1, string strtdate, string strtdateOperator, string strtdate1, string compdate,
                                            string compdateOperator, string compdate1, string futcompd, string futcompdOperator, string futcompd1)
                                            
        {
           
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_C700Searchx", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);
                sql_command.Parameters.Add("@RESPID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(respid);
                sql_command.Parameters.Add("@SOURCE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(source);
                sql_command.Parameters.Add("@FIPSTATE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(fipstate);
                sql_command.Parameters.Add("@OWNER", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(owner);
                sql_command.Parameters.Add("@STATUS", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(status);
                sql_command.Parameters.Add("@ACTIVE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(active);

                sql_command.Parameters.Add("@SELDATE1", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldate);
                sql_command.Parameters.Add("@SELDATEOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldateOperator);
                sql_command.Parameters.Add("@SELDATE2", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(seldate1);

                sql_command.Parameters.Add("@NEWTC1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc);
                sql_command.Parameters.Add("@NEWTCOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(newtcOperator);
                sql_command.Parameters.Add("@NEWTC2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(newtc1);

                sql_command.Parameters.Add("@RVITM5C1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(rvitm5c);
                sql_command.Parameters.Add("@RVITM5COPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(rvitm5cOperator);
                sql_command.Parameters.Add("@RVITM5C2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(rvitm5c1);

                sql_command.Parameters.Add("@ITEM61", SqlDbType.Int).Value = GeneralData.NullIfEmpty(item6);
                sql_command.Parameters.Add("@ITEM6OPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(item6Operator);
                sql_command.Parameters.Add("@ITEM62", SqlDbType.Int).Value = GeneralData.NullIfEmpty(item61);

                sql_command.Parameters.Add("@CAPEXP1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(capexp);
                sql_command.Parameters.Add("@CAPEXPOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(capexpOperator);
                sql_command.Parameters.Add("@CAPEXP2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(capexp1);

                sql_command.Parameters.Add("@RUNITS1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(runits);
                sql_command.Parameters.Add("@RUNITSOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(runitsOperator);
                sql_command.Parameters.Add("@RUNITS2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(runits1);

                sql_command.Parameters.Add("@COSTPU1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(costpu);
                sql_command.Parameters.Add("@COSTPUOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(costpuOperator);
                sql_command.Parameters.Add("@COSTPU2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(costpu1);

                sql_command.Parameters.Add("@STRTDATE1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(strtdate);
                sql_command.Parameters.Add("@STRTDATEOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(strtdateOperator);
                sql_command.Parameters.Add("@STRTDATE2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(strtdate1);

                sql_command.Parameters.Add("@COMPDATE1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(compdate);
                sql_command.Parameters.Add("@COMPDATEOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(compdateOperator);
                sql_command.Parameters.Add("@COMPDATE2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(compdate1);

                sql_command.Parameters.Add("@FUTCOMPD1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(futcompd);
                sql_command.Parameters.Add("@FUTCOMPDOPERATOR", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(futcompdOperator);
                sql_command.Parameters.Add("@FUTCOMPD2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(futcompd1);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;

        }

        public DataTable GetEmptyTable()
        {
            // Here we create a DataTable with 17 columns.

            DataTable table = new DataTable();

            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("FIN", typeof(string));
            table.Columns.Add("RESPID", typeof(string));
            table.Columns.Add("STATUS", typeof(string));
            table.Columns.Add("OWNER", typeof(char));

            table.Columns.Add("NEWTC", typeof(string));
            table.Columns.Add("FIPSTATE", typeof(string));
            table.Columns.Add("SELDATE", typeof(string));
            table.Columns.Add("RVITM5C", typeof(int));

            table.Columns.Add("FWGT", typeof(float));
            table.Columns.Add("PCT5C6", typeof(int));
            table.Columns.Add("STRTDATE", typeof(string));
            table.Columns.Add("COMPDATE", typeof(string));
            table.Columns.Add("FUTCOMPD", typeof(string));

            table.Columns.Add("RBLDGS", typeof(int));
            table.Columns.Add("RUNITS", typeof(int));
            table.Columns.Add("COSTPU", typeof(int));

            return table;
        }
    }
}
