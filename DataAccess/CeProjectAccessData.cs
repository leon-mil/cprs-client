/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.CeProjectAccessData.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/24/2015

Inputs:             

Parameters:	    None 

Outputs:	    CeProjectAccess data	

Description:	   

Detailed Design:    None 

Other:	            Called by: 
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
********************************************************************* 
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace CprsDAL
{
    public class CeProjectAccessData
    {

        /*Get project Item access data */
        public DataTable GetProjectItemAccess(string statp, string ID, string action, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_CeAccessItem", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@STATP", SqlDbType.Char).Value = GeneralData.NullIfEmpty(statp);
                sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(ID);
                sql_command.Parameters.Add("@ACTION", SqlDbType.Char).Value = GeneralData.NullIfEmpty(action);
                sql_command.Parameters.Add("@USRNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                sql_command.Parameters.Add("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);
                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }



        /*Get data to setup value combobox in project access screen */
        public DataTable GetValueList(int cbIndex, string selected_survey_date)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();

                if (cbIndex == 1)
                    sql = "select distinct action from dbo.ceaccess order by action";
                else if (cbIndex == 2)
                    sql = "select distinct usrnme from dbo.ceaccess where statp = '" + selected_survey_date + "' order by usrnme";

                using (SqlDataAdapter da = new SqlDataAdapter(sql, c))
                {
                    da.Fill(dt);

                    DataRow dr = dt.NewRow();
                    dr[0] = " ";

                    dt.Rows.InsertAt(dr, 0);
                }
                c.Close();
            }

            return dt;
        }

        /*Get data to setup value combobox in project access screen */
        public DataTable GetValueList()
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();

                    sql = "select distinct STATP from dbo.ceaccess order by STATP DESC";

                    using (SqlDataAdapter da = new SqlDataAdapter(sql, c))
                    {
                        da.Fill(dt);
                        DataRow[] foundRows;
                        string tt = DateTime.Now.ToString("yyyyMM");
                        foundRows = dt.Select("STATP = '" + tt + "'");
                        if (foundRows.Count() == 0)
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = DateTime.Now.ToString("yyyyMM");
                            dt.Rows.InsertAt(dr, 0);
                        }                      
                    }
                c.Close();
            }
            return dt;
        }
    }
}
     