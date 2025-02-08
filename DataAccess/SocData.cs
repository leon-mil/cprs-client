/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.SocData.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/10/2015
Inputs:             masterid, soc record
Parameters:	        None 
Outputs:	        soc data	
Description:	    data layer to get, update soc data
Detailed Design:    None 
Other:	            Called by: frmC700
 
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
    public class SocData
    {
        /*Get Soc data */
        public Soc GetSocData(int masterid)
        {
            /* find out database table name */
            string db_table = "dbo.Soc" ;

            Soc sc = new Soc(masterid);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM " + db_table + " WHERE MASTERID = " + masterid;
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    sc.Psu = reader["PSU"].ToString();
                    sc.Place = reader["PLACE"].ToString();
                    sc.Sched = reader["SCHED"].ToString();
                    sc.Bldgs  = (int)reader["Bldgs"];
                    sc.Rbldgs = (int)reader["Rbldgs"];
                    sc.Units  = (int)reader["Units"];
                    sc.Runits = (int)reader["Runits"];
                    sc.Costpu = (int)reader["Costpu"];
                    sc.Socwt = float.Parse(reader["SOCWT"].ToString());
                    sc.Unitflg = reader["UNITFLG"].ToString();
                    
                    sc.IsModified = false;
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

            return sc;

        }

        /*Save Soc Data */
        public bool SaveSocData(Soc so)
        {
            /* find out database table name */
            string db_table = "dbo.Soc";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                                "BLDGS = @BLDGS, " +
                                "RBLDGS = @RBLDGS, " +
                                "UNITS = @UNITS, " +
                                "RUNITS = @RUNITS, " +
                                "COSTPU = @COSTPU, " +
                                "UNITFLG = @UNITFLG " +
                                "WHERE MASTERID = @MASTERID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.CommandTimeout = 0;

                update_command.Parameters.AddWithValue("@MASTERID", so.Masterid);
                update_command.Parameters.AddWithValue("@BLDGS", so.Bldgs);
                update_command.Parameters.AddWithValue("@RBLDGS", so.Rbldgs);
                update_command.Parameters.AddWithValue("@UNITS", so.Units);
                update_command.Parameters.AddWithValue("@RUNITS", so.Runits);
                update_command.Parameters.AddWithValue("@COSTPU", so.Costpu);
                update_command.Parameters.AddWithValue("@UNITFLG", so.Unitflg);

                try
                {
                    sql_connection.Open();
                    int count = update_command.ExecuteNonQuery();
                    if (count > 0)
                        return true;
                    else
                        return false;
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
