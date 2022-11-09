/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.SpecManufacturingMon.cs	    	
Programmer:         Chrstine Zhang
Creation Date:      10/26/2022
Inputs:             
Parameters:	       
Outputs:	        spec manufacturing monthly data	
Description:	    This class gets the data from vipsttab

Detailed Design:    None 
Other:	            Called by: frmSpecManufacturingMon.cs
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
    public class SpecManufacturingMonData
    {
        //Get current month in vipsttab table
        public string GetSdateFromVIPSTTAB()
        {
            string sdate = "";
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT Top 1 sdate FROM dbo.VIPSTTAB";
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    sdate = reader["SDATE"].ToString();
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

            return sdate;

        }

        //get data from value table based on select month
        public DataTable GetSpecManufacturingMonData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "sp_TSpecManufacturingVal";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@mdate", SqlDbType.Char).Value = sdate;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //get data from create excel table
        public DataTable GetSpecManufacturingMonDataExcel(string sdate, string sdate1, string sdate2, string sdate3, string sdate4, string sdatep)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string stored_name;

                stored_name = "sp_TSpecManufacturingVal2";

                SqlCommand sql_command = new SqlCommand(stored_name, sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.Add("@mdate", SqlDbType.Char).Value = sdate;
                sql_command.Parameters.Add("@mdate1", SqlDbType.Char).Value = sdate1;
                sql_command.Parameters.Add("@mdate2", SqlDbType.Char).Value = sdate2;
                sql_command.Parameters.Add("@mdate3", SqlDbType.Char).Value = sdate3;
                sql_command.Parameters.Add("@mdate4", SqlDbType.Char).Value = sdate4;
                sql_command.Parameters.Add("@mdatep", SqlDbType.Char).Value = sdatep;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }
            return dt;
        }

        //get data from cvs table
        public DataTable GetSpecManufacturingMonDataCvs(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"select SDATE,V20IXV00,V20IXV01,V20IXV11,V20IXV12,V20IXV02,V20IXV21,V20IXV22,V20IXV03,V20IXV31,V20IXV32,V20IXV33,V20IXV04,V20IXV41,V20IXV42
                     from dbo.MANUPRV where sdate =" + sdate;

                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

    }
}
