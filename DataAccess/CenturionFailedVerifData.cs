/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CenturionFailedVerifData.cs	    	
Programmer:         Christine Zhang
Creation Date:      6/19/2019
Inputs:             None
Parameters:	        
Outputs:	        
Description:	    data layer to get data for centurion failed verification 
Detailed Design:    None 
Other:	            Called by: frmCenturionFailedVerif
 
Revision History:	
***************************************************************************************
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
    public class CenturionFailedVerifData
    {
        /*Retrieve failed verification data for centurion*/
        public DataTable GetFailedVerifData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT c.ID, RESPID, NEWTC, OWNER,VARNME,VALUE,REASON,REVIEW,USRNME,PRGDTM ";
                sqlQuery = sqlQuery + " FROM dbo.CENTLIST c, dbo.sample s, dbo.master m where c.id = s.id and s.masterid = m.masterid order by id";
              
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }

            }

            return dt;
        }

        /*update record to resolved */
        public void UpdateFailedVerifData(string id)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                using (SqlCommand sql_command = new SqlCommand(@"update dbo.CENTLIST
                        set REVIEW = 'Y', USRNME =@USRNME, PRGDTM = @PRGDTM 
                        where id = @ID", sql_connection))
                {
                    try
                    {
                        sql_command.Parameters.AddWithValue("@ID", id);
                        sql_command.Parameters.AddWithValue("@USRNME", UserInfo.UserName);
                        sql_command.Parameters.AddWithValue("@PRGDTM", DateTime.Now);

                        sql_connection.Open();

                        //Execute the query.
                        sql_command.ExecuteNonQuery();
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sql_connection.Close();
                    }
                }
            }

        }
    }
}
