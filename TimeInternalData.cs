/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL.TimeInternalData.cs	    	
Programmer:         Srini Natarajan
Creation Date:      07/7/2017
Inputs:             None
Parameters:	        None
Outputs:	        Internal Time series data	
Description:	    This function establishes the data connection and get series data            
Detailed Design:    None 
Other:	            Called by: frmTimeInternal.cs
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
    public class TimeInternalData
    {
        //retrieves total data
        public DataTable GetTotalData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
               
                string sqlQuery = @"select * from TIMEINTERNALTOT where DATE6 <= '" + sdate + "' ORDER BY DATE6 DESC";
                using (SqlCommand cmd = new SqlCommand(sqlQuery, sql_connection))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);

                }

                //need convert to integer, also need convert change% column, add column caption
                dt.Columns[0].Caption = "Date";
                dt.Columns[1].Caption = "  Total  ";
                dt.Columns[2].Caption = "%Change";
                dt.Columns[3].Caption = "Private";
                dt.Columns[4].Caption = "%Change";
                dt.Columns[5].Caption = "Priv Res";
                dt.Columns[6].Caption = "%Change";
                dt.Columns[7].Caption = "Priv NonRes";
                dt.Columns[8].Caption = "%Change";
                dt.Columns[9].Caption = "Public";
                dt.Columns[10].Caption = "%Change";
                dt.Columns[11].Caption = "State Local";
                dt.Columns[12].Caption = "%Change";
                dt.Columns[13].Caption = "Federal";
                dt.Columns[14].Caption = "%Change";

                return dt;  //dt, maxLavel, minLavel;
            }
        }

        //retrieves Private data
        public DataTable GetPrivateData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                
                SqlCommand sql_command = new SqlCommand("dbo.sp_TimeInternalPriv", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            //need convert to integer, also need convert change% column, add column caption
            dt.Columns[0].Caption = "Date";
            dt.Columns[1].Caption = "  Total ";
            dt.Columns[2].Caption = "%Change";
            dt.Columns[3].Caption = "Residential";
            dt.Columns[4].Caption = "%Change";
            dt.Columns[5].Caption = "New Single Family";
            dt.Columns[6].Caption = "%Change";
            dt.Columns[7].Caption = "New Multifamily";
            dt.Columns[8].Caption = "%Change";
            dt.Columns[9].Caption = "Nonresidential";
            dt.Columns[10].Caption = "%Change";
            dt.Columns[11].Caption = "Lodging";
            dt.Columns[12].Caption = "%Change";
            dt.Columns[13].Caption = "Office";
            dt.Columns[14].Caption = "%Change";
            dt.Columns[15].Caption = "General";
            dt.Columns[16].Caption = "%Change";
            dt.Columns[17].Caption = "Financial";
            dt.Columns[18].Caption = "%Change";
            dt.Columns[19].Caption = "Commercial";
            dt.Columns[20].Caption = "%Change";
            dt.Columns[21].Caption = "Automotive";
            dt.Columns[22].Caption = "%Change";
            dt.Columns[23].Caption = "Sale";
            dt.Columns[24].Caption = "%Change";
            dt.Columns[25].Caption = "Services/Parts";
            dt.Columns[26].Caption = "%Change";
            dt.Columns[27].Caption = "Parking";
            dt.Columns[28].Caption = "%Change";
            dt.Columns[29].Caption = "Food/Beverage";
            dt.Columns[30].Caption = "%Change";
            dt.Columns[31].Caption = "Food";
            dt.Columns[32].Caption = "%Change";
            dt.Columns[33].Caption = "Dining/Drinking";
            dt.Columns[34].Caption = "%Change";
            dt.Columns[35].Caption = "Multi-retail";
            dt.Columns[36].Caption = "%Change";
            dt.Columns[37].Caption = "General Merchandise";
            dt.Columns[38].Caption = "%Change";
            dt.Columns[39].Caption = "Shopping Center";
            dt.Columns[40].Caption = "%Change";
            dt.Columns[41].Caption = "Shopping Mall";
            dt.Columns[42].Caption = "%Change";
            dt.Columns[43].Caption = "Other Commercial";
            dt.Columns[44].Caption = "%Change";
            dt.Columns[45].Caption = "Drug Store";
            dt.Columns[46].Caption = "%Change";
            dt.Columns[47].Caption = "Building Supply Store";
            dt.Columns[48].Caption = "%Change";
            dt.Columns[49].Caption = "Other Stores";
            dt.Columns[50].Caption = "%Change";
            dt.Columns[51].Caption = "Warehouse";
            dt.Columns[52].Caption = "%Change";
            dt.Columns[53].Caption = "General Commercial";
            dt.Columns[54].Caption = "%Change";
            dt.Columns[55].Caption = "Mini-storage";
            dt.Columns[56].Caption = "%Change";
            dt.Columns[57].Caption = "Health Care";
            dt.Columns[58].Caption = "%Change";
            dt.Columns[59].Caption = "Hospital";
            dt.Columns[60].Caption = "%Change";
            dt.Columns[61].Caption = "Medical Building";
            dt.Columns[62].Caption = "%Change";
            dt.Columns[63].Caption = "Special Care";
            dt.Columns[64].Caption = "%Change";
            dt.Columns[65].Caption = "Educational";
            dt.Columns[66].Caption = "%Change";
            dt.Columns[67].Caption = "Preschool";
            dt.Columns[68].Caption = "%Change";
            dt.Columns[69].Caption = "Primary/Secondary";
            dt.Columns[70].Caption = "%Change";
            dt.Columns[71].Caption = "Higher Education";
            dt.Columns[72].Caption = "%Change";
            dt.Columns[73].Caption = "Instructional";
            dt.Columns[74].Caption = "%Change";
            dt.Columns[75].Caption = "Dormitory";
            dt.Columns[76].Caption = "%Change";
            dt.Columns[77].Caption = "Sports/Recreation";
            dt.Columns[78].Caption = "%Change";
            dt.Columns[79].Caption = "Other Educational";
            dt.Columns[80].Caption = "%Change";
            dt.Columns[81].Caption = "Gallery/Museum";
            dt.Columns[82].Caption = "%Change";
            dt.Columns[83].Caption = "Religious";
            dt.Columns[84].Caption = "%Change";
            dt.Columns[85].Caption = "House of Worship";
            dt.Columns[86].Caption = "%Change";
            dt.Columns[87].Caption = "Other Religious";
            dt.Columns[88].Caption = "%Change";
            dt.Columns[89].Caption = "Auxiliary Building";
            dt.Columns[90].Caption = "%Change";
            dt.Columns[91].Caption = "Amusement and Recreation";
            dt.Columns[92].Caption = "%Change";
            dt.Columns[93].Caption = "Theme/Amusement Park";
            dt.Columns[94].Caption = "%Change";
            dt.Columns[95].Caption = "Sports";
            dt.Columns[96].Caption = "%Change";
            dt.Columns[97].Caption = "Fitness";
            dt.Columns[98].Caption = "%Change";
            dt.Columns[99].Caption = "Performance/Meeting Center";
            dt.Columns[100].Caption = "%Change";
            dt.Columns[101].Caption = "Social Center";
            dt.Columns[102].Caption = "%Change";
            dt.Columns[103].Caption = "Movie Theater/Studio";
            dt.Columns[104].Caption = "%Change";
            dt.Columns[105].Caption = "Transportation";
            dt.Columns[106].Caption = "%Change";
            dt.Columns[107].Caption = "Air";
            dt.Columns[108].Caption = "%Change";
            dt.Columns[109].Caption = "Land";
            dt.Columns[110].Caption = "%Change";
            dt.Columns[111].Caption = "Communication";
            dt.Columns[112].Caption = "%Change";
            dt.Columns[113].Caption = "Power";
            dt.Columns[114].Caption = "%Change";
            dt.Columns[115].Caption = "Electric";
            dt.Columns[116].Caption = "%Change";
            dt.Columns[117].Caption = "Manufacturing";
            dt.Columns[118].Caption = "%Change";
            dt.Columns[119].Caption = "Food/Beverage/Tobacco";
            dt.Columns[120].Caption = "%Change";
            dt.Columns[121].Caption = "Chemical";
            dt.Columns[122].Caption = "%Change";
            dt.Columns[123].Caption = "Plastic/Rubber";
            dt.Columns[124].Caption = "%Change";
            dt.Columns[125].Caption = "Nonmetallic Mineral";
            dt.Columns[126].Caption = "%Change";
            dt.Columns[127].Caption = "Fabricated metal";
            dt.Columns[128].Caption = "%Change";
            dt.Columns[129].Caption = "Computer/Electronic/Electrical";
            dt.Columns[130].Caption = "%Change";
            dt.Columns[131].Caption = "Transportation Equipment";
            dt.Columns[132].Caption = "%Change";

            return dt;
        }

        //retrieves Public data
        public DataTable GetPublicData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                SqlCommand sql_command = new SqlCommand("dbo.sp_TimeInternalPub", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            //need convert to integer, also need convert change% column, add column caption
            dt.Columns[0].Caption = "Date ";
            dt.Columns[1].Caption = "  Total ";
            dt.Columns[2].Caption = "%Change";
            dt.Columns[3].Caption = "Residential";
            dt.Columns[4].Caption = "%Change";
            dt.Columns[5].Caption = "MultiFamily";
            dt.Columns[6].Caption = "%Change";
            dt.Columns[7].Caption = "Nonresidential";
            dt.Columns[8].Caption = "%Change";
            dt.Columns[9].Caption = "Office";
            dt.Columns[10].Caption = "%Change";
            dt.Columns[11].Caption = "Commercial";
            dt.Columns[12].Caption = "%Change";
            dt.Columns[13].Caption = "Automotive";
            dt.Columns[14].Caption = "%Change";
            dt.Columns[15].Caption = "Parking";
            dt.Columns[16].Caption = "%Change";
            dt.Columns[17].Caption = "Health Care";
            dt.Columns[18].Caption = "%Change";
            dt.Columns[19].Caption = "Hospital";
            dt.Columns[20].Caption = "%Change";
            dt.Columns[21].Caption = "Medical Building";
            dt.Columns[22].Caption = "%Change";
            dt.Columns[23].Caption = "Special Care";
            dt.Columns[24].Caption = "%Change";
            dt.Columns[25].Caption = "Educational";
            dt.Columns[26].Caption = "%Change";
            dt.Columns[27].Caption = "Primary/Secondary";
            dt.Columns[28].Caption = "%Change";
            dt.Columns[29].Caption = "Elementary";
            dt.Columns[30].Caption = "%Change";
            dt.Columns[31].Caption = "Middle/Junior High";
            dt.Columns[32].Caption = "%Change";
            dt.Columns[33].Caption = "High";
            dt.Columns[34].Caption = "%Change";
            dt.Columns[35].Caption = "Higher Education";
            dt.Columns[36].Caption = "%Change";
            dt.Columns[37].Caption = "Instructional";
            dt.Columns[38].Caption = "%Change";
            dt.Columns[39].Caption = "Dormitory";
            dt.Columns[40].Caption = "%Change";
            dt.Columns[41].Caption = "Sports/Recreation";
            dt.Columns[42].Caption = "%Change";
            dt.Columns[43].Caption = "Infrastructure";
            dt.Columns[44].Caption = "%Change";
            dt.Columns[45].Caption = "Other Educational";
            dt.Columns[46].Caption = "%Change";
            dt.Columns[47].Caption = "Library/Archive";
            dt.Columns[48].Caption = "%Change";
            dt.Columns[49].Caption = "Public Safety";
            dt.Columns[50].Caption = "%Change";
            dt.Columns[51].Caption = "Correctional";
            dt.Columns[52].Caption = "%Change";
            dt.Columns[53].Caption = "Detention";
            dt.Columns[54].Caption = "%Change";
            dt.Columns[55].Caption = "Police/Sheriff";
            dt.Columns[56].Caption = "%Change";
            dt.Columns[57].Caption = "Other Public Safety";
            dt.Columns[58].Caption = "%Change";
            dt.Columns[59].Caption = "Fire/Rescue";
            dt.Columns[60].Caption = "%Change";
            dt.Columns[61].Caption = "Amusement and Recreation";
            dt.Columns[62].Caption = "%Change";
            dt.Columns[63].Caption = "Sports";
            dt.Columns[64].Caption = "%Change";
            dt.Columns[65].Caption = "Performance/Meeting Center";
            dt.Columns[66].Caption = "%Change";
            dt.Columns[67].Caption = "Convention Center";
            dt.Columns[68].Caption = "%Change";
            dt.Columns[69].Caption = "Social Center";
            dt.Columns[70].Caption = "%Change";
            dt.Columns[71].Caption = "Neighborhood Center";
            dt.Columns[72].Caption = "%Change";
            dt.Columns[73].Caption = "Park/Camp";
            dt.Columns[74].Caption = "%Change";
            dt.Columns[75].Caption = "Transportation";
            dt.Columns[76].Caption = "%Change";
            dt.Columns[77].Caption = "Air";
            dt.Columns[78].Caption = "%Change";
            dt.Columns[79].Caption = "Air Passenger Terminal";
            dt.Columns[80].Caption = "%Change";
            dt.Columns[81].Caption = "Runway";
            dt.Columns[82].Caption = "%Change";
            dt.Columns[83].Caption = "Land";
            dt.Columns[84].Caption = "%Change";
            dt.Columns[85].Caption = "Land Passenger Terminal";
            dt.Columns[86].Caption = "%Change";
            dt.Columns[87].Caption = "Mass Transit";
            dt.Columns[88].Caption = "%Change";
            dt.Columns[89].Caption = "Water";
            dt.Columns[90].Caption = "%Change";
            dt.Columns[91].Caption = "Dock/Marina";
            dt.Columns[92].Caption = "%Change";
            dt.Columns[93].Caption = "Power";
            dt.Columns[94].Caption = "%Change";
            dt.Columns[95].Caption = "Highway and Street";
            dt.Columns[96].Caption = "%Change";
            dt.Columns[97].Caption = "Pavement";
            dt.Columns[98].Caption = "%Change";
            dt.Columns[99].Caption = "Lighting";
            dt.Columns[100].Caption = "%Change";
            dt.Columns[101].Caption = "Bridge";
            dt.Columns[102].Caption = "%Change";
            dt.Columns[103].Caption = "Rest Facility";
            dt.Columns[104].Caption = "%Change";
            dt.Columns[105].Caption = "Sewage and Waste Disposal";
            dt.Columns[106].Caption = "%Change";
            dt.Columns[107].Caption = "Sewage/Dry Waste";
            dt.Columns[108].Caption = "%Change";
            dt.Columns[109].Caption = "Sewage Treatment Plant";
            dt.Columns[110].Caption = "%Change";
            dt.Columns[111].Caption = "Line/Pump Station";
            dt.Columns[112].Caption = "%Change";
            dt.Columns[113].Caption = "Waste Water";
            dt.Columns[114].Caption = "%Change";
            dt.Columns[115].Caption = "Waste Water Treatment Plant";
            dt.Columns[116].Caption = "%Change";
            dt.Columns[117].Caption = "Line/Drain";
            dt.Columns[118].Caption = "%Change";
            dt.Columns[119].Caption = "Water Supply";
            dt.Columns[120].Caption = "%Change";
            dt.Columns[121].Caption = "Water Treatment Plant";
            dt.Columns[122].Caption = "%Change";
            dt.Columns[123].Caption = "Line";
            dt.Columns[124].Caption = "%Change";
            dt.Columns[125].Caption = "Pump Station";
            dt.Columns[126].Caption = "%Change";
            dt.Columns[127].Caption = "Conservation and Development";
            dt.Columns[128].Caption = "%Change";
            dt.Columns[129].Caption = "Dam/Levee";
            dt.Columns[130].Caption = "%Change";
            dt.Columns[131].Caption = "Breakwater/Jetty";
            dt.Columns[132].Caption = "%Change";
            return dt;
        }

        //retrieves Federal data
        public DataTable GetFederalData(string sdate)
        {
            DataTable dt = new DataTable();
            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                
                SqlCommand sql_command = new SqlCommand("dbo.sp_TimeInternalFed", sql_connection);
                sql_command.CommandType = CommandType.StoredProcedure;
                sql_command.Parameters.Add("@Survey_month", SqlDbType.Char).Value = GeneralData.NullIfEmpty(sdate);
                SqlDataAdapter da = new SqlDataAdapter(sql_command);
                da.Fill(dt);
            }

            //need convert to integer, also need convert change% column, add column caption
            dt.Columns[0].Caption = "Date";
            dt.Columns[1].Caption = " Total ";
            dt.Columns[2].Caption = "%Change";
            dt.Columns[3].Caption = "Residential";
            dt.Columns[4].Caption = "%Change";
            dt.Columns[5].Caption = "Nonresidential";
            dt.Columns[6].Caption = "%Change";
            dt.Columns[7].Caption = "Office";
            dt.Columns[8].Caption = "%Change";
            dt.Columns[9].Caption = "Commercial";
            dt.Columns[10].Caption = "%Change";
            dt.Columns[11].Caption = "Health Care";
            dt.Columns[12].Caption = "%Change";
            dt.Columns[13].Caption = "Educational";
            dt.Columns[14].Caption = "%Change";
            dt.Columns[15].Caption = "Public Safety";
            dt.Columns[16].Caption = "%Change";
            dt.Columns[17].Caption = "Amusement and Recreation";
            dt.Columns[18].Caption = "%Change";
            dt.Columns[19].Caption = "Transportation";
            dt.Columns[20].Caption = "%Change";
            dt.Columns[21].Caption = "Power";
            dt.Columns[22].Caption = "%Change";
            dt.Columns[23].Caption = "Highway and Streets ";
            dt.Columns[24].Caption = "%Change";
            dt.Columns[25].Caption = "Conservation and Development";
            dt.Columns[26].Caption = "%Change";
            return dt;
        }
    }
}
