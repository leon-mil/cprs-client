/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CesampleData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             id 
Parameters:	        None 
Outputs:	        Cesample data
Description:	    data layer to get cesample data, lock/unlock cesample, check id exist
                    update edit field
Detailed Design:    None 
Other:	            Called by: frmImprovement
 
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
    public class CesampleData
    {
        /* Get data table base on the db source  */
        public Cesample GetCesampleTable(string id)
        {

            Cesample cesamp = new Cesample(id);
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM dbo.Cesample WHERE ID = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    cesamp.Survdate = reader["SURVDATE"].ToString();
                    cesamp.Interview = reader["INTERVIEW"].ToString();
                    cesamp.State = reader["STATE"].ToString();
                    cesamp.County = reader["COUNTY"].ToString();
                    cesamp.Cbsa = reader["CBSA"].ToString();
                    cesamp.Intsta = reader["INTSTA"].ToString();
                    cesamp.Tenure = reader["TENURE"].ToString();
                    cesamp.Yrbuilt = reader["YRBUILT"].ToString();
                    cesamp.Yrset = reader["YRSET"].ToString();
                    cesamp.Units = reader["UNITS"].ToString();
                    cesamp.Propval = (int)reader["PROPVAL"];
                    cesamp.Income = (int)reader["INCOME"];
                    cesamp.Finwt = float.Parse(reader["FINWT"].ToString());
                    cesamp.I1finwt = float.Parse(reader["I1FINWT"].ToString());
                    cesamp.I2finwt = float.Parse(reader["I2FINWT"].ToString());
                    cesamp.I3finwt = float.Parse(reader["I3FINWT"].ToString());
                    cesamp.I4finwt = float.Parse(reader["I4FINWT"].ToString());
                    cesamp.City = reader["CITY"].ToString();
                    cesamp.Zip = reader["ZIP"].ToString();
                    cesamp.Bedroom = (int)reader["BEDROOM"];
                    cesamp.Bathroom = (int)reader["BATHROOM"];
                    cesamp.Hlfbath = (int)reader["HLFBATH"];

                    cesamp.Stater = GeneralDataFuctions.GetFipState(cesamp.State);

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

            return cesamp;

        }

        /*Get lock field */
        public string CheckIsLocked(string id)
        {
            string lock_me = String.Empty;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * FROM dbo.cesample WHERE id = " + GeneralData.AddSqlQuotes(id);
            SqlCommand command = new SqlCommand(sql, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    lock_me = reader["LOCKID"].ToString().Trim();

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

            if (lock_me != String.Empty)
                return lock_me;
            else
                return String.Empty;
        }

        /*Update lock field */
        public bool UpdateLock(string id, string username)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();
                
                string usql = "UPDATE dbo.Cesample SET " +
                                "LOCKID = @LOCKID " +
                                "WHERE ID = @ID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@ID", id);
                update_command.Parameters.AddWithValue("@LOCKID", username);

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

        /*Update Edit field */
        public bool UpdateEdit(string id)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                string usql = "UPDATE dbo.Cesample SET " +
                                "EDITED = @EDITED " +
                                "WHERE ID = @ID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.Parameters.AddWithValue("@ID", id);
                update_command.Parameters.AddWithValue("@EDITED", "*");

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

        /*Vaildate ID */
        public bool CheckIdExist(string id)
        {
            bool record_found = false;

            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select id from dbo.Cesample where ID = " +GeneralData.AddSqlQuotes(id);
                SqlCommand command = new SqlCommand(sql, connection);

                try
                {
                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        if (reader.HasRows)
                            return true;
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

            }
            return record_found;
        }

    }

}


