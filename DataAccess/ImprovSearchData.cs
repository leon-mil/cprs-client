/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : ImprovSearchData.cs
Programmer    : Srini Natarajan
Creation Date : August 12, 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : the data layer will be used in frmImprovementSearch.cs
Change Request: 
Specification : Improvement Search Specifications  
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
    
    public class ImprovSearchData
    {
        /*Get Improvement Search Data */
        public DataTable GetImprovSearchData(string ID, string state, string jcode, string survdate1, string survdate2, string survdateoperator, string yrbuilt1, string yrbuilt2, string yrbuiltoperator, string tcost1, string tcost2,
                    string tcostoperator, string appcode, string jobscode, string propval1, string propval2, string propvaloperator, string income1,  string income2, string incomeoperator)
        {
            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_ImprovmntSrch", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(ID);
                sql_command.Parameters.Add("@STATE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(state);
                sql_command.Parameters.Add("@JCODE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(jcode);
                sql_command.Parameters.Add("@SURVDATE1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survdate1);
                sql_command.Parameters.Add("@SURVDATE2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survdate2);
                sql_command.Parameters.Add("@SURVDATEOPERATOR", SqlDbType.Char).Value = GeneralData.NullIfEmpty(survdateoperator);
                sql_command.Parameters.Add("@YRBUILT1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(yrbuilt1);
                sql_command.Parameters.Add("@YRBUILT2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(yrbuilt2);
                sql_command.Parameters.Add("@YRBUILTOPERATOR", SqlDbType.Char).Value = GeneralData.NullIfEmpty(yrbuiltoperator);
                sql_command.Parameters.Add("@TCOST1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(tcost1);
                sql_command.Parameters.Add("@TCOST2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(tcost2);
                sql_command.Parameters.Add("@TCOSTOPERATOR", SqlDbType.Char).Value = GeneralData.NullIfEmpty(tcostoperator);
                sql_command.Parameters.Add("@APPCODE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(appcode);
                sql_command.Parameters.Add("@JOBSCODE", SqlDbType.Char).Value = GeneralData.NullIfEmpty(jobscode);
                sql_command.Parameters.Add("@PROPVAL1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(propval1);
                sql_command.Parameters.Add("@PROPVAL2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(propval2);
                sql_command.Parameters.Add("@PROPVALOPERATOR", SqlDbType.Char).Value = GeneralData.NullIfEmpty(propvaloperator);
                sql_command.Parameters.Add("@INCOME1", SqlDbType.Int).Value = GeneralData.NullIfEmpty(income1);
                sql_command.Parameters.Add("@INCOME2", SqlDbType.Int).Value = GeneralData.NullIfEmpty(income2);
                sql_command.Parameters.Add("@INCOMEOPERATOR", SqlDbType.Char).Value = GeneralData.NullIfEmpty(incomeoperator);
                
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        public DataTable GetEmptyTable()
        {
            // Here we create a DataTable with 13 columns.
            DataTable table = new DataTable();
            table.Columns.Add("ID", typeof(string));
            table.Columns.Add("SURVDATE", typeof(string));
            table.Columns.Add("INTERVIEW", typeof(string));
            table.Columns.Add("PROPVAL", typeof(char));
            table.Columns.Add("YRBUILT", typeof(string));
            table.Columns.Add("INCOME", typeof(string));
            table.Columns.Add("STATE", typeof(int));
            table.Columns.Add("WEIGHT", typeof(int));
            table.Columns.Add("TCOST", typeof(char));
            table.Columns.Add("WEIGHTED TCOST", typeof(string));
            table.Columns.Add("JOBS", typeof(string));
            table.Columns.Add("JOBCODE", typeof(int));
            table.Columns.Add("EDITED", typeof(int));
            return table;
        }

        //Select the distinct job Codes to be listed on the drop down
        public DataTable GetJCodeDataForCombo()
        {
            DataTable dt = new DataTable();
            string qryStrJCode = "select distinct JCODE from [dbo].[ce_search] order by JCODE";
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand(qryStrJCode, sql_connection);
                sql_command.CommandType = CommandType.Text;
                sql_connection.Open();

                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                {
                    da.Fill(dt);
                }
                DataRow dr = dt.NewRow();
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }

        //Select the year built list from the database
        public DataTable GetYrBuiltForCombo()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_YrBuiltList", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);

                DataRow dr = dt.NewRow();
                dr[0] = -1;
                dr[1] = " ";
                dt.Rows.InsertAt(dr, 0);
            }
            return dt;
        }

        //Get the default survey date from the ceSample table.
        public static string getDefSurveyDate(string DefSurvDate)
        {
            string DefSurvDateVal = String.Empty;
            string sql = "SELECT max(SurvDate) FROM [dbo].[ceSAMPLE]";
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand(sql, sql_connection);
                sql_command.CommandType = CommandType.Text;
                sql_connection.Open();

                sql_command.ExecuteNonQuery();
                DefSurvDateVal = Convert.ToString(sql_command.ExecuteScalar());
                sql_connection.Close();
            }

            return DefSurvDateVal;

        }
    }
}
