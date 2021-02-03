/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TFUData.cs	    	
Programmer:         Christine Zhang
Creation Date:      6/16/2016
Inputs:             
Parameters:	        None 
Outputs:	        data	
Description:	    data layer to retrieve tfu data
Detailed Design:    None 
Other:	            Called by: frmTfu
 
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
    public class TFUData
    {
        //get project list for the respid
        public List<TFUProject> GetTFUProjectsForRespid(string respid)
        {
            List<TFUProject> projectlist = new List<TFUProject>();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select s.ID, s.PROJDESC, contract, status, compdater, compdate, owner from dbo.sample s, dbo.master m";

                sql = sql + " WHERE s.masterid = m.masterid and RESPID = " + GeneralData.AddSqlQuotes(respid) + " and seldate <> '" + DateTime.Now.Year.ToString()+ DateTime.Now.ToString("MM") + "' order by ID";
                
                SqlCommand command = new SqlCommand(sql, sql_connection);
                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        TFUProject pv = new TFUProject();
                        pv.Id = reader["ID"].ToString();
                        pv.Projdesc = reader["PROJDESC"].ToString();
                        pv.Contract = reader["CONTRACT"].ToString();
                        pv.Status = reader["STATUS"].ToString().Trim();
                        pv.Compdater = reader["COMPDATER"].ToString().Trim();
                        pv.Compdate = reader["COMPDATE"].ToString().Trim();
                        pv.Satisfied = "";
                        pv.Owner= reader["OWNER"].ToString().Trim();
                        projectlist.Add(pv);
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

                //fill out sched_call info
                SchedCallData scdata = new SchedCallData();
                foreach (TFUProject pv in projectlist)
                {
                    Schedcall s = scdata.GetSchedCallData(pv.Id);
                    if (s != null)
                    {
                       
                        pv.Priority = s.Priority;
                       
                        pv.Callreq = s.Callreq;

                        if (s.Callstat == "V")
                            pv.Satisfied = "Y";
                    }
                    
                }

            }
            return projectlist;

        }
    }
}
