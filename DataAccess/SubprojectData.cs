/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.SubprojectData.cs	    	
Programmer:         Christine Zhang
Creation Date:      05/02/2016
Inputs:             none
Parameters:	        None 
Outputs:	        subproject data
Description:	    data layer tor retrieve and add comment data
Detailed Design:    None 
Other:	            Called by: frmParentPopu.cs
 
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
    public class SubprojectData
    {
        /*Get a new record */
        public bool AddSubprojectParent(int masterid, int parent_masterid, string status)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.Subproject SET " +
                                "Parent = @Parent, " +
                                "Status = @Status " +
                                "WHERE MASTERID = @Masterid";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@Parent", parent_masterid);
                update_command.Parameters.AddWithValue("@Status", status);
                update_command.Parameters.AddWithValue("@Masterid", masterid);

                try
                {
                    int count = update_command.ExecuteNonQuery();
                    if (count > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
            }

        }

        // get total buldgs and units for the parent
        public void GetSubprojectParentBldgsUnits(int parent_masterid, ref int tbldgs, ref int tunits)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "Select sum(bldgs) as tbldgs, sum(units) as tunits from dbo.subproject where parent=" + parent_masterid;
                SqlCommand command = new SqlCommand(usql, sql_connection);

                try
                {
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        tbldgs = (int)reader["tbldgs"];
                        tunits = (int)reader["tunits"];
                    }
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
