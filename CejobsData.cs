/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CejobsData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             id cejob record
Parameters:	        None 
Outputs:	        Cejobs data
Description:	    data layer to get, save cejobs
Detailed Design:    None 
Other:	            Called by: frmImprovement
 
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
    public class CejobsData
    {
        public Cejobs GetCejobs(string id)
        {
            Cejobs cj = new Cejobs(id);

            SqlConnection connection = new SqlConnection(GeneralData.getConnectionString());

            string sql = "SELECT * ";
            sql = sql + "FROM dbo.Cejobs WHERE ID = " + GeneralData.AddSqlQuotes(id) + " order by interview desc, detcode";
            SqlCommand command = new SqlCommand(sql, connection);
            List<Cejob> result = new List<Cejob>();
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    string inv = reader["INTERVIEW"].ToString();
                    string dtc = reader["DETCODE"].ToString();

                    Cejob j = new Cejob(inv, dtc);
                    j.Jobcod = reader["JOBCOD"].ToString();
                    j.Wrkdesc = reader["WRKDESC"].ToString();
                    j.Who = (int)reader["WHO"];
                    j.Propno = reader["PROPNO"].ToString();
                    j.Addinfo = reader["ADDINFO"].ToString();
                    j.Tcost = (int)reader["TCOST"];
                    j.Con1 = (int)reader["CON1"];
                    j.Con2 = (int)reader["CON2"];
                    j.Con3 = (int)reader["CON3"];
                    j.Con4 = (int)reader["CON4"];
                    j.Amt1 = (int)reader["AMT1"];
                    j.Amt2 = (int)reader["AMT2"];
                    j.Amt3 = (int)reader["AMT3"];
                    j.Amt4 = (int)reader["AMT4"];
                    j.Ren1 = (int)reader["REN1"];
                    j.Ren2 = (int)reader["REN2"];
                    j.Ren3 = (int)reader["REN3"];
                    j.Ren4 = (int)reader["REN4"];
                    j.Eqpcode1 = reader["EQPCODE1"].ToString();
                    j.Eqp1 = (int)reader["EQP1"];
                    j.Eqpcode2 = reader["EQPCODE2"].ToString();
                    j.Eqp2 = (int)reader["EQP2"];
                    j.Eqpcode3 = reader["EQPCODE3"].ToString();
                    j.Eqp3 = (int)reader["EQP3"];
                    j.Eqpcode4 = reader["EQPCODE4"].ToString();
                    j.Eqp4 = (int)reader["EQP4"];
                    j.Eqpcode5 = reader["EQPCODE5"].ToString();
                    j.Eqp5 = (int)reader["EQP5"];
                    j.Eqpcode6 = reader["EQPCODE6"].ToString();
                    j.Eqp6 = (int)reader["EQP6"];
                    j.Fcon1 = reader["FCON1"].ToString();
                    j.Fcon2 = reader["FCON2"].ToString();
                    j.Fcon3 = reader["FCON3"].ToString();
                    j.Fcon4 = reader["FCON4"].ToString();
                    j.Famt1 = reader["FAMT1"].ToString();
                    j.Famt2 = reader["FAMT2"].ToString();
                    j.Famt3 = reader["FAMT3"].ToString();
                    j.Famt4 = reader["FAMT4"].ToString();
                    j.Fren1 = reader["FREN1"].ToString();
                    j.Fren2 = reader["FREN2"].ToString();
                    j.Fren3 = reader["FREN3"].ToString();
                    j.Fren4 = reader["FREN4"].ToString();
                    j.Feqp1 = reader["FEQP1"].ToString();
                    j.Feqp2 = reader["FEQP2"].ToString();
                    j.Feqp3 = reader["FEQP3"].ToString();
                    j.Feqp4 = reader["FEQP4"].ToString();
                    j.Feqp5 = reader["FEQP5"].ToString();
                    j.Feqp6 = reader["FEQP6"].ToString();
                    j.Teqp = (int)reader["TEQP"];
                    j.Dflag = Dirty.initial;

                    result.Add(j);
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

            cj.cejoblist = result;

            return cj;
        }

        public bool SaveCejobs(Cejobs jobs)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                sql_connection.Open();

                foreach (Cejob j in jobs.cejoblist)
                {
                   if (j.Dflag == Dirty.modify)
                   {
                     string usql = "UPDATE dbo.Cejobs SET " +
                                 "WRKDESC = @WRKDESC, " +
                                 "ADDINFO = @ADDINFO, " +
                                 "TCOST = @TCOST, " +
                                 "CON1 = @CON1, " +
                                 "CON2 = @CON2, " +
                                 "CON3 = @CON3, " +
                                 "CON4 = @CON4, " +
                                 "AMT1 = @AMT1, " +
                                 "AMT2 = @AMT2, " +
                                 "AMT3 = @AMT3, " +
                                 "AMT4 = @AMT4, " +
                                 "REN1 = @REN1, " +
                                 "REN2 = @REN2, " +
                                 "REN3 = @REN3, " +
                                 "REN4 = @REN4, " +
                                 "FCON1 = @FCON1, " +
                                 "FCON2 = @FCON2, " +
                                 "FCON3 = @FCON3, " +
                                 "FCON4 = @FCON4, " +
                                 "FAMT1 = @FAMT1, " +
                                 "FAMT2 = @FAMT2, " +
                                 "FAMT3 = @FAMT3, " +
                                 "FAMT4 = @FAMT4, " +
                                 "FREN1 = @FREN1, " +
                                 "FREN2 = @FREN2, " +
                                 "FREN3 = @FREN3, " +
                                 "FREN4 = @FREN4, " +
                                 "EQP1 = @EQP1," +
                                 "EQP2 = @EQP2," +
                                 "EQP3 = @EQP3," +
                                 "EQP4 = @EQP4," +
                                 "EQP5 = @EQP5," +
                                 "EQP6 = @EQP6," +
                                 "EQPCODE1 = @EQPCODE1," +
                                 "EQPCODE2 = @EQPCODE2," +
                                 "EQPCODE3 = @EQPCODE3," +
                                 "EQPCODE4 = @EQPCODE4," +
                                 "EQPCODE5 = @EQPCODE5," +
                                 "EQPCODE6 = @EQPCODE6," +
                                 "FEQP1 = @FEQP1," +
                                 "FEQP2 = @FEQP2," +
                                 "FEQP3 = @FEQP3," +
                                 "FEQP4 = @FEQP4," +
                                 "FEQP5 = @FEQP5," +
                                 "FEQP6 = @FEQP6 " +
                                 "WHERE ID = @ID AND INTERVIEW = @INTERVIEW AND DETCODE = @DETCODE";

                        SqlCommand update_command = new SqlCommand(usql, sql_connection);
                        update_command.Parameters.AddWithValue("@ID", jobs.Id);
                        update_command.Parameters.AddWithValue("@INTERVIEW", j.Interview);
                        update_command.Parameters.AddWithValue("@DETCODE", j.Detcode);
                        update_command.Parameters.AddWithValue("@WRKDESC", j.Wrkdesc);
                        update_command.Parameters.AddWithValue("@ADDINFO", j.Addinfo);
                        update_command.Parameters.AddWithValue("@TCOST", j.Tcost);
                        update_command.Parameters.AddWithValue("@CON1", j.Con1);
                        update_command.Parameters.AddWithValue("@CON2", j.Con2);
                        update_command.Parameters.AddWithValue("@CON3", j.Con3);
                        update_command.Parameters.AddWithValue("@CON4", j.Con4);
                        update_command.Parameters.AddWithValue("@AMT1", j.Amt1);
                        update_command.Parameters.AddWithValue("@AMT2", j.Amt2);
                        update_command.Parameters.AddWithValue("@AMT3", j.Amt3);
                        update_command.Parameters.AddWithValue("@AMT4", j.Amt4);
                        update_command.Parameters.AddWithValue("@REN1", j.Ren1);
                        update_command.Parameters.AddWithValue("@REN2", j.Ren2);
                        update_command.Parameters.AddWithValue("@REN3", j.Ren3);
                        update_command.Parameters.AddWithValue("@REN4", j.Ren4);
                        update_command.Parameters.AddWithValue("@FCON1", j.Fcon1);
                        update_command.Parameters.AddWithValue("@FCON2", j.Fcon2);
                        update_command.Parameters.AddWithValue("@FCON3", j.Fcon3);
                        update_command.Parameters.AddWithValue("@FCON4", j.Fcon4);
                        update_command.Parameters.AddWithValue("@FAMT1", j.Famt1);
                        update_command.Parameters.AddWithValue("@FAMT2", j.Famt2);
                        update_command.Parameters.AddWithValue("@FAMT3", j.Famt3);
                        update_command.Parameters.AddWithValue("@FAMT4", j.Famt4);
                        update_command.Parameters.AddWithValue("@FREN1", j.Fren1);
                        update_command.Parameters.AddWithValue("@FREN2", j.Fren2);
                        update_command.Parameters.AddWithValue("@FREN3", j.Fren3);
                        update_command.Parameters.AddWithValue("@FREN4", j.Fren4);
                        update_command.Parameters.AddWithValue("@EQP1", j.Eqp1);
                        update_command.Parameters.AddWithValue("@EQP2", j.Eqp2);
                        update_command.Parameters.AddWithValue("@EQP3", j.Eqp3);
                        update_command.Parameters.AddWithValue("@EQP4", j.Eqp4);
                        update_command.Parameters.AddWithValue("@EQP5", j.Eqp5);
                        update_command.Parameters.AddWithValue("@EQP6", j.Eqp6);
                        update_command.Parameters.AddWithValue("@EQPCODE1", j.Eqpcode1);
                        update_command.Parameters.AddWithValue("@EQPCODE2", j.Eqpcode2);
                        update_command.Parameters.AddWithValue("@EQPCODE3", j.Eqpcode3);
                        update_command.Parameters.AddWithValue("@EQPCODE4", j.Eqpcode4);
                        update_command.Parameters.AddWithValue("@EQPCODE5", j.Eqpcode5);
                        update_command.Parameters.AddWithValue("@EQPCODE6", j.Eqpcode6);
                        update_command.Parameters.AddWithValue("@FEQP1", j.Feqp1);
                        update_command.Parameters.AddWithValue("@FEQP2", j.Feqp2);
                        update_command.Parameters.AddWithValue("@FEQP3", j.Feqp3);
                        update_command.Parameters.AddWithValue("@FEQP4", j.Feqp4);
                        update_command.Parameters.AddWithValue("@FEQP5", j.Feqp5);
                        update_command.Parameters.AddWithValue("@FEQP6", j.Feqp6);

                        try
                        {
                            int count = update_command.ExecuteNonQuery();
                      
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }

                    }
                   
                    else if (j.Dflag  == Dirty.delete)
                    {
                        string usql = "DELETE FROM dbo.Cejobs" +
                                      " WHERE ID = @ID AND INTERVIEW = @INTERVIEW AND DETCODE = @DETCODE";

                        SqlCommand delete_command = new SqlCommand(usql, sql_connection);
                        delete_command.Parameters.AddWithValue("@ID", jobs.Id);
                        delete_command.Parameters.AddWithValue("@INTERVIEW", j.Interview);
                        delete_command.Parameters.AddWithValue("@DETCODE", j.Detcode);

                        try
                        {
                            int count = delete_command.ExecuteNonQuery();
                        }
                        catch (SqlException ex)
                        {
                            throw ex;
                        }

                    }

                }
                sql_connection.Close();

            }

            return true;
        }

    }
}
