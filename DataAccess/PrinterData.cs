/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsDAL.PrinterData.cs	    	

Programmer:         Srini Natarajan
Creation Date:      09/27/2016

Inputs:             None

Parameters:	        None 

Outputs:	        List of printers.	

Description:	    This class reads the data from Printers table, checks for exisiting printers and 
                    deletes Printers from the printers table.

Detailed Design:    None.

Other:	            Called by: frmPrinters and frmAddPrinterPopup
 
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
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using CprsBLL;

namespace CprsDAL
{
    public class PrinterData
    {

        //get data from Printers
        public DataTable GetPrinterslistData()
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sqlQuery = @"SELECT PRINTERID, LOCATION, NAME FROM dbo.PRINTERS";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter ds = new SqlDataAdapter(cmd);
                    ds.Fill(dt);
                }
            }
            return dt;
        }

        //add new printer
        public void AddNewPrinter(string location, string name)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            string isql = "insert dbo.Printers (location, name)"
                            + " Values (@LOCATION, @NAME)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@LOCATION", location);
            insert_command.Parameters.AddWithValue("@NAME", name);
            try
            {
                sql_connection.Open();
                insert_command.ExecuteNonQuery();
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

        //Delete a printer
        public bool DeletePrinter(string PrinterID)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());
            string usql = "DELETE FROM PRINTERS WHERE PrinterID = @PrinterID";
            SqlCommand delete_command = new SqlCommand(usql, sql_connection);
            delete_command.Parameters.AddWithValue("@PrinterID", PrinterID);
            try
            {
                sql_connection.Open();
                int count = delete_command.ExecuteNonQuery();
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

        //When a New Printer is entered manually its validated against the PRINTERS table.
        public bool CheckPrinterExist(string pName, string location)
        {
            bool printer_found = false;
            using (SqlConnection connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                string sql = "select name from dbo.Printers where NAME = " + GeneralData.AddSqlQuotes(pName) + " and LOCATION = " + GeneralData.AddSqlQuotes(location);
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
            return printer_found;
        }
    }
}
