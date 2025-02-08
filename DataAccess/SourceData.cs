/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.SourceData.cs	    	

Programmer:         Kevin Montgomery

Creation Date:      03/25/2015

Inputs:             Masterid

Parameters:	    None 

Outputs:	    Factor data	

Description:	    This function establishes the data connection and reads in 
                    the data, based upon the masterid number, from the Source Display View 
                    using the sp_SourceDisplay stored procedure that will be used for the 
                    Source Address Display screen

Detailed Design:    None 

Other:	            Called by: frmSource
 
Revision History:	
****************************************************************************************
 Modified Date :  11/25/2015
 Modified By   :  Cestine Gill
 Keyword       :  
 Change Request:  
 Description   :  changed screen to display frame id number (fin)
****************************************************************************************
 Modified Date :  05/24/2016
 Modified By   :  Cestine Gill
 Keyword       :  
 Change Request:  
 Description   :  changed screen use masterid in lieu of dodgenum
*****************************************************************************************
Modified Date  :  08/17/2016
Modified By    : Srini
Keyword        :
Change Request :
Description    : Update to Read Factor9 (Owner2) fields and store in Factor class
******************************************************************************************/
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
    public static class SourceData
    {
        public static Factor GetFactor(int masterid)
        {

            Factor factor = new Factor();

            //Establish the connection to the database. Use the table SOURCE_DISPLAY, to
            //Lookup data based upon the masterid

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("Select * From dbo.SOURCE_DISPLAY WHERE MASTERID = @MASTERID", sql_connection);
                sql_command.CommandTimeout = 0;

                sql_command.Parameters.AddWithValue("@MASTERID", masterid);

                //Using a data reader, convert to data to strings and store the retrieved fields 
                //into the CPRSBLL Factor class

                try
                {
                    sql_connection.Open();
                    SqlDataReader reader = sql_command.ExecuteReader(CommandBehavior.SingleRow);
                    if (reader.Read())
                    {
                        factor.Fin = reader["Fin"].ToString();
                        factor.Masterid = (int)reader["Masterid"];
                        factor.Source = reader["Source"].ToString();
                        factor.Seldate = reader["Seldate"].ToString();
                        factor.Newtc = reader["Newtc"].ToString();
                        factor.Projselv = (int)reader["Projselv"];
                        factor.Fwgt = (decimal)reader["Fwgt"];

                        factor.State = reader["State"].ToString();
                        factor.Dodgecou = reader["Dodgecou"].ToString();

                        if (reader["Bldgs"] != DBNull.Value)
                        {
                            factor.Bldgs = (int)reader["Bldgs"];
                        }
                        else
                        {
                            factor.Bldgs = 0;
                        }

                        if (reader["Units"] != DBNull.Value)
                        {
                            factor.Units = (int)reader["Units"];
                        }
                        else
                        {
                            factor.Units = 0;
                        }

                        factor.Id = reader["Id"].ToString();
                        factor.Owner = reader["Owner"].ToString();

                        factor.Projdesc = reader["Projdesc"].ToString();
                        factor.Projloc = reader["Projloc"].ToString();
                        factor.Pcityst = reader["Pcityst"].ToString();

                        factor.F3resporg = reader["F3resporg"].ToString();
                        factor.F3respname = reader["F3respname"].ToString();
                        factor.F3addr1 = reader["F3addr1"].ToString();
                        factor.F3addr2 = reader["F3addr2"].ToString();
                        factor.F3addr3 = reader["F3addr3"].ToString();
                        factor.F3zip = reader["F3zip"].ToString();
                        factor.F3phone = reader["F3phone"].ToString();
                        factor.F3email = reader["F3email"].ToString();
                        factor.F3weburl = reader["F3weburl"].ToString();

                        factor.F4resporg = reader["F4resporg"].ToString();
                        factor.F4respname = reader["F4respname"].ToString();
                        factor.F4addr1 = reader["F4addr1"].ToString();
                        factor.F4addr2 = reader["F4addr2"].ToString();
                        factor.F4addr3 = reader["F4addr3"].ToString();
                        factor.F4zip = reader["F4zip"].ToString();
                        factor.F4phone = reader["F4phone"].ToString();
                        factor.F4email = reader["F4email"].ToString();
                        factor.F4weburl = reader["F4weburl"].ToString();

                        factor.F5resporg = reader["F5resporg"].ToString();
                        factor.F5respname = reader["F5respname"].ToString();
                        factor.F5addr1 = reader["F5addr1"].ToString();
                        factor.F5addr2 = reader["F5addr2"].ToString();
                        factor.F5addr3 = reader["F5addr3"].ToString();
                        factor.F5zip = reader["F5zip"].ToString();
                        factor.F5phone = reader["F5phone"].ToString();
                        factor.F5email = reader["F5email"].ToString();
                        factor.F5weburl = reader["F5weburl"].ToString();

                        factor.F7resporg = reader["F7resporg"].ToString();
                        factor.F7respname = reader["F7respname"].ToString();
                        factor.F7addr1 = reader["F7addr1"].ToString();
                        factor.F7addr2 = reader["F7addr2"].ToString();
                        factor.F7addr3 = reader["F7addr3"].ToString();
                        factor.F7zip = reader["F7zip"].ToString();
                        factor.F7phone = reader["F7phone"].ToString();
                        factor.F7email = reader["F7email"].ToString();
                        factor.F7weburl = reader["F7weburl"].ToString();

                        factor.F9resporg = reader["F9resporg"].ToString();
                        factor.F9respname = reader["F9respname"].ToString();
                        factor.F9addr1 = reader["F9addr1"].ToString();
                        factor.F9addr2 = reader["F9addr2"].ToString();
                        factor.F9addr3 = reader["F9addr3"].ToString();
                        factor.F9zip = reader["F9zip"].ToString();
                        factor.F9phone = reader["F9phone"].ToString();
                        factor.F9email = reader["F9email"].ToString();
                        factor.F9weburl = reader["F9weburl"].ToString();
                    }
                    else
                    {
                        factor = null;
                    }
                    reader.Close(); //close reader
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close(); //close database connection
                }
            }
            return factor; //return the factor data
        }


        public static string GetCounty(string state, string dodgecou)
        {
            string county="";

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
              
                    SqlCommand sql_command = new SqlCommand("Select COUNTY From dbo.COUNTYLIST WHERE STATE = @state and FIPSCOU = @dodgecou", sql_connection);
                    sql_command.CommandTimeout = 0;

                    sql_command.Parameters.Add("@STATE", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(state);
                    sql_command.Parameters.Add("@DODGECOU", SqlDbType.NVarChar).Value = GeneralData.NullIfEmpty(dodgecou);

                    try
                    {
                        //Open the connection to the database

                        sql_connection.Open();

                        SqlDataReader dr = sql_command.ExecuteReader();

                        if (dr.Read())
                        {
                            county = dr[0].ToString();
                        }

                        return county;
                    }
                    catch (SqlException ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        sql_connection.Close(); //close database connection
                    }
                
            }
        }
    }
}