/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmImpFlagsReview.cs	    	

Programmer:         Cestine Gill

Creation Date:      09/09/2015

Inputs:             None

Parameters:		    None
                 
Outputs:		    Improvements Flag Data

Description:	    This function establishes the data connection, reads in 
                    the data, creates the table and queries the database for the
                    data used in the Improvements Flag Review Screen

Detailed Design:    Detailed Design for Improvements Flag Review 

Other:	            Called by: frmImpFlagsReview 
 
Revision History:	
***********************************************************************************
 Modified Date  : 4/3/2024
 Modified By    : Christine Zhang
 Keyword        : 20240403cz
 Change Request : CR 1434
 Description    : replace detcode with jobidcode 
***********************************************************************************/
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

    public class ImpFlagReviewData
    {
        public string row1;
        public string row2;
        public string row3;
        public string row4;
        public string row5;
        public string row6;
        public string row7;
        public string row8;
        public string row9;
        public string row10;
        public string row11;
        public string row12;
        public string row13;
        public string row14;
        public string row15;
        public string row16;
        public string row17;
        public string row18;
        public string row19;
        public string row20;
        public string row21;

        /*Populate the Flags Count Table*/

        public DataTable CreateFlagsTable()
        {
            DataTable dt = new DataTable();

            for (int i = 1; i < 22; i++)
            {
                if (i == 1)
                {
                    row1 = GetFlgCnt(i);
                }
                if (i == 2)
                {
                    row2 = GetFlgCnt(i);
                }
                if (i == 3)
                {
                    row3 = GetFlgCnt(i);
                }
                if (i == 4)
                {
                    row4 = GetFlgCnt(i);
                }
                if (i == 5)
                {
                  //  row5 = GetFlgCnt(i);
                }
                if (i == 6)
                {
                  //  row6 = GetFlgCnt(i);
                }
                if (i == 7)
                {
                  //  row7 = GetFlgCnt(i);
                }
                if (i == 8)
                {
                  //  row8 = GetFlgCnt(i);
                }
                if (i == 9)
                {
                  //  row9 = GetFlgCnt(i);
                }
                if (i == 10)
                {
                    row10 = GetFlgCnt(i);
                }
                if (i == 11)
                {
                    row11 = GetFlgCnt(i);
                }
                if (i == 12)
                {
                    row12 = GetFlgCnt(i);
                }
                if (i == 13)
                {
                    row13 = GetFlgCnt(i);
                }
                if (i == 14)
                {
                    row14 = GetFlgCnt(i);
                }
                if (i == 15)
                {
                    row15 = GetFlgCnt(i);
                }
                if (i == 16)
                {
                    row16 = GetFlgCnt(i);
                }
                if (i == 17)
                {
                    row17 = GetFlgCnt(i);
                }
                if (i == 18)
                {
                    row18 = GetFlgCnt(i);
                }
                if (i == 19)
                {
                    row19 = GetFlgCnt(i);
                }
                if (i == 20)
                {
                    row20 = GetFlgCnt(i);
                }
                if (i == 21)
                {
                    row21 = GetFlgCnt(i);
                }             

            }
            dt.Columns.Add("DESCRIPTION");
            dt.Columns.Add("TOTAL CASES");

            dt.Rows.Add("FLAG – Contract Cost for DIY job ", row1);
            dt.Rows.Add("FLAG – Sum of appliances >= total contract cost ", row2);
            dt.Rows.Add("FLAG – Appliances not typical for this type of job ", row3);
            dt.Rows.Add("FLAG – Bad appliances cost ", row4);
            //dt.Rows.Add("FLAG – Bad Value for Appliance 2 ", row5);
            //dt.Rows.Add("FLAG – Bad Value for Appliance 3 ", row6);
            //dt.Rows.Add("FLAG – Bad Value for Appliance 4 ", row7);
            //dt.Rows.Add("FLAG – Bad Value for Appliance 5 ", row8);
            //dt.Rows.Add("FLAG – Bad Value for Appliance 6 ", row9);
            dt.Rows.Add("FLAG – Bad Value for contract expense ", row10);
            dt.Rows.Add("FLAG – Bad Value for purchased materials ", row11);
            dt.Rows.Add("FLAG – Bad Value for rented materials ", row12);
            dt.Rows.Add("FLAG – Bad Value for total job cost ", row13);
            dt.Rows.Add("FLAG – Contract job with no contract cost ", row14);
            dt.Rows.Add("FLAG – New Construction may be out of scope  ", row15);
            dt.Rows.Add("FLAG – DIY job without material costs ", row16);
            dt.Rows.Add("FLAG – No Monthly Expense Indicator ", row17);
            dt.Rows.Add("FLAG – Large expense relative to property value ", row18);
            dt.Rows.Add("FLAG – May be out of scope ", row19);
            dt.Rows.Add("FLAG – Match jobs found in the previous  interview ", row20);
            dt.Rows.Add("FLAG – Duplicate jobs found ", row21);

            return dt;
        }

        //Get the count of cases by Flag type

        public string GetFlgCnt(int r)
        {
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("SELECT COUNT(*) FROM dbo.CEFLAGS WHERE SUBSTRING(CURR_FLAG, @LenStart, 1) = 1", sql_connection);

                    SqlDataAdapter da = new SqlDataAdapter(sql_command);

                    sql_command.Parameters.AddWithValue("@LenStart", r);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    string str = dt.Rows[0][0].ToString();
                    return str;
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

        /*Populate the Flag Projects Table*/

        public DataTable GetFlgProjects(int srow)
        {

            DataTable dt = new DataTable();

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                try
                {
                    SqlCommand sql_command = new SqlCommand("SELECT ID, SURVDATE, INTERVIEW, PROPVAL, YRBUILT, INCOME, STATE,  FWGT, TCOST, WEIGHTEDCOST, JOBCOD, JCODE, EDITED	FROM dbo.IMP_FLAGS_REVIEW WHERE SUBSTRING(CURR_FLAG, @LenStart, 1) = 1 order by id ", sql_connection);

                    //Open the connection.

                    sql_connection.Open();

                    sql_command.Parameters.AddWithValue("@LenStart", srow);

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
