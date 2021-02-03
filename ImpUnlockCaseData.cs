/**************************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : CprsDAL.ImpUnlockCaseData.cs	    	

Programmer      : Cestine Gill

Creation Date   : 08/13/2015

Inputs          : dbo.CESAMPLE

Parameters      : None 

Outputs         : Locked cases retrieved from dbo.CESAMPLE

Description     : This function establishes the data connection, queries the
                  database table and outputs the result to the screen

Detailed Design : Improvements Unlock Case Design Document 

Other           : Called by: frmImpUnlockCase
 
Revision History:	
****************************************************************************************
 Modified Date  :  
 Modified By    :  
 Keyword        :  
 Change Request :  
 Description    :   
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
    public class ImpUnlockCaseData
    {

        // Retrieve all of the locked cases from the CESAMPLE data table

        public DataTable GetLockedCasesData()
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
                {
                    SqlCommand sql_command = new SqlCommand("SELECT Id, Survdate, Interview, State, YrBuilt, Yrset, Income, Propval, Finwt, Lockid FROM dbo.CESAMPLE WHERE LOCKID != '' ORDER BY ID", sql_connection);

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {

                        da.Fill(dt);
                    }
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }

            return dt;
        }

        //Verifies that the field was unlocked

        public DataTable VerifyUnlocked(string id)
        {
            DataTable dt = new DataTable();

            //Query the data table to check if the Case was successfully unlocked

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("SELECT ID FROM dbo.CESAMPLE WHERE dbo.CESAMPLE.ID = @id AND LOCKID != ''", sql_connection);

                    sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {

                        da.Fill(dt);
                    }
                }
                catch (SqlException ex)
                {
                    throw ex;
                }

                return dt;
            }


        }

        // Update the lockid fields in the CESAMPLE table when user clicks UNLOCK button

        public DataTable UnlockCase(string id)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("UPDATE dbo.CESAMPLE SET LOCKID = ' ' WHERE ID = @id", sql_connection);

                    sql_command.Parameters.Add("@ID", SqlDbType.Char).Value = GeneralData.NullIfEmpty(id);

                    //Open the connection.

                    sql_connection.Open();

                    // Create a DataAdapter to run the command and fill the DataTable

                    using (SqlDataAdapter da = new SqlDataAdapter(sql_command))
                    {
                        //Execute the query.

                        sql_command.ExecuteNonQuery();
                        da.Fill(dt);

                    }
                }

                catch (SqlException ex)
                {
                    throw ex;
                }
                finally
                {
                    sql_connection.Close(); //close database connection
                }

                return dt;
            }
        }
    }
}
