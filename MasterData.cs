/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.MasterData.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/05/2015
Inputs:             masterid, master record
Parameters:	        None 
Outputs:	        Master data	
Description:	    data layer to add, update master
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
    public class MasterData
    {
        /*Retrieve Master data */
        public Master GetMasterData(int masterid)
        {
            /* find out database table name */
            string db_table = "dbo.Master";

            Master mast = new Master(masterid);
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
                    mast.Dodgenum = reader["DODGENUM"].ToString();
                    mast.Fin = reader["FIN"].ToString();
                    mast.Fipstate = reader["FIPSTATE"].ToString();
                    mast.Dodgecou = reader["DODGECOU"].ToString().Trim();
                    mast.Owner  = reader["OWNER"].ToString();
                    mast.Projselv = (int)reader["PROJSELV"];
                    mast.Tvalue = (int)reader["TVALUE"];
                    mast.Stratid = (int)reader["STRATID"];
                    mast.Mtf = reader["MTF"].ToString().Trim();
                    mast.Seldate = reader["SELDATE"].ToString().Trim();
                   
                    mast.Source = reader["SOURCE"].ToString().Trim();
                    mast.Structcd = reader["STRUCTCD"].ToString().Trim();
                    mast.Newtc = reader["Newtc"].ToString().Trim();
                    mast.Dmf = Double.Parse(reader["DMF"].ToString());

                    mast.IsModified = false;
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

            mast.Fipstater = GeneralDataFuctions.GetFipState(mast.Fipstate);

            return mast;

        }

        

        /*Save master Data */
        public bool SaveMasterData(Master mo)
        {
            /* find out database table name */
            string db_table = "dbo.Master";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string usql = "UPDATE " + db_table + " SET " +
                                "OWNER = @OWNER, " +
                                "NEWTC = @NEWTC " +
                                "WHERE MASTERID = @MASTERID";

                SqlCommand update_command = new SqlCommand(usql, sql_connection);
                update_command.CommandTimeout = 0;

                update_command.Parameters.AddWithValue("@MASTERID", mo.Masterid);
                update_command.Parameters.AddWithValue("@OWNER", mo.Owner );
                update_command.Parameters.AddWithValue("@NEWTC", mo.Newtc );
                
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

        //Get sample weight value
        public float GetSampWeight(Master mast)
        {
            float sampwt = 0;
            string sqlq;
            
            if (mast.Source.Equals("NP", StringComparison.OrdinalIgnoreCase))
            {
                sqlq = "select round(samprate*socwt, 3) as sampwt from dbo.sampslct s, dbo.soc s1, dbo.master m " +
                         " where s.STRATID = m.STRATID and m.masterid = s1.masterid and m.MASTERID = " + mast.Masterid;
            }
            else if (mast.Owner == "M" )
            {
                sqlq = "select round(samprate*socwt*uaf, 3) as sampwt from dbo.sampslct s, dbo.soc s1, dbo.master m, dbo.MONTHLY_UAF m1 " +
                         " where s.STRATID = m.STRATID and m.masterid = s1.masterid and m1.DATE6 = m.SELDATE and m.MASTERID = " + mast.Masterid;
            }
            else
            {
                sqlq = "select samprate as sampwt from dbo.sampslct where stratid = " + mast.Stratid;
            }

            sampwt = GetSampwt(sqlq);
          

            return sampwt;
        }

        private float GetSampwt(string sqlq)
        {
            float sampwt = 0;
            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            SqlCommand command = new SqlCommand(sqlq, connection);
            command.CommandTimeout = 0;

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow);

                if (reader.Read())
                {
                    sampwt = float.Parse(reader["SAMPWT"].ToString());
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

            return sampwt;
        }
    }
}
