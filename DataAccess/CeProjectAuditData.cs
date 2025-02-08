/***********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.CeProjectAuditData.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/23/2015

Inputs:             dbo.sp_CeAuditItem

Parameters:	        None 

Outputs:	        CeProjectAudit data	

Description:	   

Detailed Design:    None 

Other:	            Called by: frmCeProjectAudit.cs
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
********************************************************************* 
************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;

namespace CprsDAL
{
    public class CeProjectAuditData
    {
        /*Get project Item audit data */

        public DataTable GetProjectItemAudits(string ID, string varnme, string usrnme, string prgdtm)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_CeAuditItem", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
       
                sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(ID);
                sql_command.Parameters.Add("@VARNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(varnme);
                sql_command.Parameters.Add("@USRNME", SqlDbType.Char).Value = GeneralData.NullIfEmpty(usrnme);
                sql_command.Parameters.Add("@PRGDTM", SqlDbType.Char).Value = GeneralData.NullIfEmpty(prgdtm);
                using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                {
                    da.Fill(dt);
                }
            }
            return dt;
        }


        /*Get data to setup value combobox in project audit screen */

        public DataTable GetValueList(int cbIndex)
        {
            DataTable dt = new DataTable();
            string sql = string.Empty;

            using (SqlConnection c = new SqlConnection(GeneralData.getConnectionString()))
            {
                c.Open();
                
                    if (cbIndex == 1)
                        sql = "Select distinct varnme from dbo.ceaudit order by varnme";
                    else if (cbIndex == 2)
                        sql = "select distinct usrnme from dbo.ceaudit order by usrnme";
              
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
    }
}
