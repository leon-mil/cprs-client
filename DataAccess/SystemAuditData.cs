/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : SystemAuditData.cs
Programmer    : Christine Zhang
Creation Date : Sept 3 2015
Parameters    : N/A
Inputs        : datasets:CPRACCESS 
              : store precedures: sp_systemAduit
Outputs       : data tables
Description   : get project access data from database tables and store precedures
Change Request: 
Detail Design :   
Rev History   : See Below
Other         : called by frmSystemAudit
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
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
    public class SystemAuditData
    {

        /*Get system audit data */
        public DataTable GetSystemAudit(string statp, string module, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_SystemAudit", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@STATP", SqlDbType.Char).Value = GeneralData.NullIfEmpty(statp);
                sql_command.Parameters.Add("@MODULE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(module);
                sql_command.Parameters.Add("@USRNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                sql_command.Parameters.Add("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);

                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                {
                    da.Fill(dt);
                }
            }

            return dt;

        }

        /*Get data to setup value combobox in screen */
        public DataTable GetValueList(string field)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();

                if (field == "STATP")
                    sql = "select distinct STATP from dbo.cpraccess order by STATP DESC";
                else
                    sql = "select distinct " + field + " from dbo.cpraccess order by " + field;

                using (SqlDataAdapter da = new SqlDataAdapter(sql, c))
                {
                    da.Fill(dt);

                    if (field == "STATP")
                    {
                        DataRow[] foundRows;
                        string tt = DateTime.Now.ToString("yyyyMM");
                        foundRows = dt.Select("STATP = '" + tt + "'");
                        if (foundRows.Count() == 0)
                        {
                            DataRow dr = dt.NewRow();

                            if (field == "STATP")
                                dr[0] = DateTime.Now.ToString("yyyyMM");
                            else
                                dr[0] = " ";

                            dt.Rows.InsertAt(dr, 0);
                        }
                    }
                    else
                    {
                        DataRow dr = dt.NewRow();
                        dr[0] = " ";

                        dt.Rows.InsertAt(dr, 0);
                    }
                }
                c.Close();
            }

            return dt;
        }
    }
}
