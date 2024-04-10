/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.CeauditData.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             ceaudit record
Parameters:	        None 
Outputs:	        None
Description:	    data layer to add ceaudit data
Detailed Design:    None 
Other:	            Called by: frmImprovement
 
Revision History:	
***************************************************************************************
 Modified Date  :  March 6, 2024
 Modified By    :  Christine Zhang
 Keyword        :  20240306cz
 Change Request :  CR 1434
 Description    :  replace detcode with jobidcode
***************************************************************************************/

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
    public class CeauditData
    {
        public void AddCeauditData(Ceaudit ca)
        {
            SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString());

            string isql = "insert dbo.ceaudit (id, interview, jobidcode, varnme, oldval, oldflag, newval, newflag, usrnme, prgdtm)"
                            + "Values (@id, @INTERVIEW, @JOBIDCODE, @VARNME, @OLDVAL, @OLDFLAG, @NEWVAL, @NEWFLAG, @USRNME, @PRGDTM)";
            SqlCommand insert_command = new SqlCommand(isql, sql_connection);
            insert_command.Parameters.AddWithValue("@id", ca.Id);
            insert_command.Parameters.AddWithValue("@INTERVIEW", ca.Interview);
            insert_command.Parameters.AddWithValue("@JOBIDCODE", ca.Jobidcode );
            insert_command.Parameters.AddWithValue("@VARNME", ca.Varnme);
            insert_command.Parameters.AddWithValue("@OLDVAL", ca.Oldval);
            //if (ca.Oldflag == "B") ca.Oldflag = "";
            insert_command.Parameters.AddWithValue("@OLDFLAG", ca.Oldflag);
            insert_command.Parameters.AddWithValue("@NEWVAL", ca.Newval);
            //if (ca.Newflag == "B") ca.Newflag = "";
            insert_command.Parameters.AddWithValue("@NEWFLAG", ca.Newflag);
            insert_command.Parameters.AddWithValue("@USRNME", ca.Usrnme);
            insert_command.Parameters.AddWithValue("@PRGDTM", ca.Progdtm);

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

    }
}
