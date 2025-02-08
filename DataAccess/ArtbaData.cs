/**************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : CprsDAL.ArtbaData.cs	    	
Programmer      : Christine Zhang     
Creation Date   : May 1 2017  
Inputs          : None
Parameters      : sdate, premon1, premon2, premon3, premon4
Outputs         : Artba data	
Description     : data layer to get data for Artba screens
Detailed Design : None 
Other           : Called by: frmArtbaMon, frmArtbaAnn
Revision History:	
***************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
    public class ArtbaData
    {
        /*Retrieve Artab monthly data */
        public DataTable GetArtbaMonData(string sdate, string premon1, string premon2, string premon3, string premon4)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_ArtbaMon", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@survey_month", SqlDbType.Char).Value = sdate;
                sql_command.Parameters.Add("@premonth1", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon1);
                sql_command.Parameters.Add("@premonth2", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon2);
                sql_command.Parameters.Add("@premonth3", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon3);
                sql_command.Parameters.Add("@premonth4", SqlDbType.Char).Value = GeneralData.NullIfEmpty(premon4);

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }

        /*Retrieve Artba annual data */
        public DataTable GetArtabAnnData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {

                SqlCommand sql_command = new SqlCommand("dbo.sp_ArtbaAnn", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;

                sql_command.Parameters.Add("@survey_month", SqlDbType.Char).Value = sdate;

                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            return dt;
        }
    }
}