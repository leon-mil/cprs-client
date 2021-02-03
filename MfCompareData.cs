/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       MfCompareData.cs
Programmer:         Diane Musachio
Creation Date:      May 19, 2016
Inputs:             masterid, fin
                    dbo.mf_initial
Parameters:	        none
Outputs:	        Multi Family name and address data
Description:	    Retrieves data for matched presample case
Detailed Design:    Multi Family Initial Address Detailed Design
Other:	            Called by: frmMfComparePopup.cs
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
    public class MfCompareData
    {
        //Obtain the Compare data
        public MfCompare GetCompareData(int masterid, bool isSample)
        {
            MfCompare mfcompare = new MfCompare();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql;
                if (isSample)
                    sql = "SELECT * from dbo.MF_INITIAL where MASTERID = @MASTERID";
                else
                    sql = "SELECT * from dbo.PRESAMPLE where MASTERID = @MASTERID";

                SqlCommand command = new SqlCommand(sql, sql_connection);

                command.Parameters.AddWithValue("@MASTERID", masterid);

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader =
                            command.ExecuteReader(CommandBehavior.SingleRow);

                    if (reader.Read())
                    {
                        mfcompare.Masterid = (int)reader["MASTERID"];
                        mfcompare.Id = reader["ID"].ToString();
                        mfcompare.Psu = reader["Psu"].ToString();
                        mfcompare.Bpoid = reader["Bpoid"].ToString();
                        mfcompare.Sched = reader["Sched"].ToString();
                        mfcompare.Status = reader["Status"].ToString();
                        mfcompare.Seldate = reader["Seldate"].ToString();
                        mfcompare.Newtc = reader["Newtc"].ToString();
                        mfcompare.Frcde = reader["Frcde"].ToString();
                        mfcompare.Fipstate = reader["Fipstate"].ToString();
                        mfcompare.Strtdate = reader["Strtdate"].ToString();
                        mfcompare.Respid = reader["Respid"].ToString();
                        mfcompare.Respname = reader["Respname"].ToString();
                        mfcompare.Respname2 = reader["Respname2"].ToString();
                        mfcompare.Resporg = reader["Resporg"].ToString();
                        mfcompare.Factoff = reader["Factoff"].ToString();
                        mfcompare.Othrresp = reader["Othrresp"].ToString();
                        mfcompare.Addr1 = reader["Addr1"].ToString();
                        mfcompare.Addr2 = reader["Addr2"].ToString();
                        mfcompare.Addr3 = reader["Addr3"].ToString();
                        mfcompare.Zip = reader["Zip"].ToString();
                        mfcompare.Phone = reader["Phone"].ToString();
                        mfcompare.Phone2 = reader["Phone2"].ToString();
                        mfcompare.Ext = reader["Ext"].ToString();
                        mfcompare.Ext2 = reader["Ext2"].ToString();
                        mfcompare.ProjDesc = reader["Projdesc"].ToString();
                        mfcompare.Projloc = reader["Projloc"].ToString();
                        mfcompare.PCitySt = reader["Pcityst"].ToString();

                        if (reader["Bldgs"] != DBNull.Value)
                        {
                            mfcompare.Bldgs = reader["Bldgs"].ToString();
                        }
                        else
                        {
                            mfcompare.Bldgs = (0).ToString();
                        }

                        if (reader["Units"] != DBNull.Value)
                        {
                            mfcompare.Units = reader["Units"].ToString();
                        }
                        else
                        {
                            mfcompare.Units = (0).ToString();
                        }
                        if (reader["Rbldgs"] != DBNull.Value)
                        {
                            mfcompare.Rbldgs = reader["Rbldgs"].ToString();
                        }
                        else
                        {
                            mfcompare.Rbldgs = (0).ToString();
                        }

                        if (reader["Runits"] != DBNull.Value)
                        {
                            mfcompare.Runits = reader["Runits"].ToString();
                        }
                        else
                        {
                            mfcompare.Runits = (0).ToString();
                        }
                    }
                    else
                    {
                        mfcompare = null;
                    }
                    reader.Close();
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close();
                }
                return mfcompare;
            }
        }

   
    }
}
      