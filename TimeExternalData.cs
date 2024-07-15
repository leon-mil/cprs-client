/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsDAL/TimeExternalData.cs	    	
Programmer:         Diane Musachio
Creation Date:      07/20/2017
Inputs:             None
Parameters:	        sdate, tableType, seasonal
Outputs:	        Tsar Series data	
Description:	    This function establishes the data connection and get external series
                    data.                   
Detailed Design:    None 
Other:	            Called by: frmTimeExternal.cs
Revision History:	
****************************************************************************************
 Modified Date :  04/02/2019
 Modified By   :  Diane Musachio
 Keyword       :  dm040219
 Change Request:  CR #3034
 Description   :  modify private multifamily tables to extract from VCC21 instead of X0021
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
using CprsDAL;

namespace CprsDAL
{
    public class TimeExternalData
    {
        //get series data for series viewer
        public DataTable GetExternalTimeData(string sdate, string tableType, string seasonal)
        {
            DataTable dt = new DataTable();
            SqlCommand sql_command;

            string stable;
            string stable1;
            string stable2;

            using (SqlConnection sql_connection = new SqlConnection(GeneralData.getConnectionString()))
            {
                stable = null;
                stable1 = null;
                stable2 = null;

                //TOTAL TABLE  
                if (tableType == "T")
                {
                    if (seasonal == "SAA")
                    {
                        stable = "SAATOT";
                        stable1 = "SAAPRV";
                    }
                    else
                    {
                        stable = "UNATOT";
                        stable1 = "UNAPRV";
                    }

                    sql_command = new SqlCommand(@" Select t.DATE6, t.TXXXX" + seasonal +
                        @",  t.T00XX" + seasonal + @",  t.TNRXX" + seasonal + @",  t.T01XX" + seasonal +
                        @",  t.T02XX" + seasonal + @",  t.T03XX" + seasonal + @",  t.T04XX" + seasonal +
                        @",  t.T05XX" + seasonal + @",  t.T06XX" + seasonal + @",  t.T07XX" + seasonal +
                        @",  t.T08XX" + seasonal + @",  t.T09XX" + seasonal + @",  t.T10XX" + seasonal +
                        @",  t.T11XX" + seasonal + @",  t.T12XX" + seasonal + @",  t.T13XX" + seasonal +
                        @",  t.T14XX" + seasonal + @",  t.T15XX" + seasonal + @",  t.T20IX" + seasonal +
                        @", t1.VXXXX" + seasonal + @", t1.V00XX" + seasonal + @", t1.VNRXX" + seasonal +
                        @", t1.V01XX" + seasonal + @", t1.V02XX" + seasonal + @", t1.V03XX" + seasonal +
                        @", t1.V04XX" + seasonal + @", t1.V05XX" + seasonal + @", t1.V06XX" + seasonal +
                        @", t1.V08XX" + seasonal + @", t1.V09XX" + seasonal + @", t1.V10XX" + seasonal +
                        @", t1.V11XX" + seasonal + @", t1.V20IX" + seasonal +
                        @",  t.PXXXX" + seasonal + @",  t.P00XX" + seasonal + @",  t.PNRXX" + seasonal +
                        @",  t.P02XX" + seasonal + @",  t.P03XX" + seasonal + @",  t.P04XX" + seasonal +
                        @",  t.P05XX" + seasonal + @",  t.P07XX" + seasonal + @",  t.P08XX" + seasonal +
                        @",  t.P09XX" + seasonal + @",  t.P11XX" + seasonal + @",  t.P12XX" + seasonal +
                        @",  t.P13XX" + seasonal + @",  t.P14XX" + seasonal + @",  t.P15XX" + seasonal +

                        @" From dbo." + stable +
                        "  as t LEFT OUTER JOIN dbo." + stable1 + @" as t1 ON t.DATE6 = t1.DATE6 
                        where t.DATE6 <= '" + sdate + "' ORDER BY DATE6 DESC", sql_connection);

                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);

                    //populate header texts
                    dt.Columns[0].Caption = "Date";
                    dt.Columns[1].Caption = "Total\n\rConstruction";
                    dt.Columns[2].Caption = "Total\n\rResidential";
                    dt.Columns[3].Caption = "Total\n\rNonresidential";
                    dt.Columns[4].Caption = "Total\n\rLodging";
                    dt.Columns[5].Caption = "Total\n\rOffice";
                    dt.Columns[6].Caption = "Total\n\rCommercial";
                    dt.Columns[7].Caption = "Total\n\rHealth care";
                    dt.Columns[8].Caption = "Total\n\rEducational";
                    dt.Columns[9].Caption = "Total\n\rReligious";
                    dt.Columns[10].Caption = "Total\n\rPublic safety";
                    dt.Columns[11].Caption = "Total\n\rAmusement and recreation";
                    dt.Columns[12].Caption = "Total\n\rTransportation";
                    dt.Columns[13].Caption = "Total\n\rCommunication";
                    dt.Columns[14].Caption = "Total\n\rPower";
                    dt.Columns[15].Caption = "Total\n\rHighway and street";
                    dt.Columns[16].Caption = "Total\n\rSewage and waste disposal";
                    dt.Columns[17].Caption = "Total\n\rWater supply";
                    dt.Columns[18].Caption = "Total\n\rConservation and development";
                    dt.Columns[19].Caption = "Total\n\rManufacturing";
                    dt.Columns[20].Caption = "Total\n\rPrivate Construction";
                    dt.Columns[21].Caption = "Private\n\rResidential";
                    dt.Columns[22].Caption = "Private\n\rNonresidential";
                    dt.Columns[23].Caption = "Private\n\rLodging";
                    dt.Columns[24].Caption = "Private\n\rOffice";
                    dt.Columns[25].Caption = "Private\n\rCommercial";
                    dt.Columns[26].Caption = "Private\n\rHealth care";
                    dt.Columns[27].Caption = "Private\n\rEducational";
                    dt.Columns[28].Caption = "Private\n\rReligious";
                    dt.Columns[29].Caption = "Private\n\rAmusement and recreation";
                    dt.Columns[30].Caption = "Private\n\rTransportation";
                    dt.Columns[31].Caption = "Private\n\rCommunication";
                    dt.Columns[32].Caption = "Private\n\rPower";
                    dt.Columns[33].Caption = "Private\n\rManufacturing";
                    dt.Columns[34].Caption = "Total\n\rPublic Construction";
                    dt.Columns[35].Caption = "Public\n\rResidential";
                    dt.Columns[36].Caption = "Public\n\rNonresidential";
                    dt.Columns[37].Caption = "Public\n\rOffice";
                    dt.Columns[38].Caption = "Public\n\rCommercial";
                    dt.Columns[39].Caption = "Public\n\rHealth care";
                    dt.Columns[40].Caption = "Public\n\rEducation";
                    dt.Columns[41].Caption = "Public\n\rPublic safety";
                    dt.Columns[42].Caption = "Public\n\rAmusement and recreation";
                    dt.Columns[43].Caption = "Public\n\rTransportation";
                    dt.Columns[44].Caption = "Public\n\rPower";
                    dt.Columns[45].Caption = "Public\n\rHighway and street";
                    dt.Columns[46].Caption = "Public\n\rSewage and waste disposal";
                    dt.Columns[47].Caption = "Public\n\rWater supply";
                    dt.Columns[48].Caption = "Public\n\rConservation and development";

                    //loop through rows to format date column
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string date_value = dt.Rows[i][0].ToString();
                        DateTime col = DateTime.ParseExact(date_value, "yyyyMM", null);
                        dt.Rows[i][0] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                        string stdate = "200201";
                        DateTime startdate = DateTime.ParseExact(stdate, "yyyyMM", null);

                        //filter through date column and if date is less than 200201 for
                        //specific tsar series do not output data - insert a zero which then
                        //gets blankout out in excel file
                        if (col < startdate)
                        {
                            dt.Rows[i]["T00XX" + seasonal] = 0;
                            dt.Rows[i]["TNRXX" + seasonal] = 0;
                            dt.Rows[i]["T01XX" + seasonal] = 0;
                            dt.Rows[i]["T02XX" + seasonal] = 0;
                            dt.Rows[i]["T03XX" + seasonal] = 0;
                            dt.Rows[i]["T04XX" + seasonal] = 0;
                            dt.Rows[i]["T05XX" + seasonal] = 0;
                            dt.Rows[i]["T06XX" + seasonal] = 0;
                            dt.Rows[i]["T07XX" + seasonal] = 0;
                            dt.Rows[i]["T08XX" + seasonal] = 0;
                            dt.Rows[i]["T09XX" + seasonal] = 0;
                            dt.Rows[i]["T10XX" + seasonal] = 0;
                            dt.Rows[i]["T11XX" + seasonal] = 0;
                            dt.Rows[i]["T12XX" + seasonal] = 0;
                            dt.Rows[i]["T13XX" + seasonal] = 0;
                            dt.Rows[i]["T14XX" + seasonal] = 0;
                            dt.Rows[i]["T15XX" + seasonal] = 0;
                            dt.Rows[i]["T20IX" + seasonal] = 0;
                            dt.Rows[i]["P00XX" + seasonal] = 0;
                            dt.Rows[i]["PNRXX" + seasonal] = 0;
                            dt.Rows[i]["P02XX" + seasonal] = 0;
                            dt.Rows[i]["P03XX" + seasonal] = 0;
                            dt.Rows[i]["P04XX" + seasonal] = 0;
                            dt.Rows[i]["P05XX" + seasonal] = 0;
                            dt.Rows[i]["P07XX" + seasonal] = 0;
                            dt.Rows[i]["P08XX" + seasonal] = 0;
                            dt.Rows[i]["P09XX" + seasonal] = 0;
                            dt.Rows[i]["P11XX" + seasonal] = 0;
                            dt.Rows[i]["P12XX" + seasonal] = 0;
                            dt.Rows[i]["P13XX" + seasonal] = 0;
                            dt.Rows[i]["P14XX" + seasonal] = 0;
                            dt.Rows[i]["P15XX" + seasonal] = 0;
                        }
                    }
                }
                //PUBLIC TABLE
                else if (tableType == "P")
                {
                    if (seasonal == "SAA")
                    {
                        stable = "SAATOT";
                        stable1 = "SAAPUB";
                        stable2 = "SAAFED";
                    }
                    else
                    {
                        stable = "UNATOT";
                        stable1 = "UNAPUB";
                        stable2 = "UNAFED";
                    }

                    sql_command = new SqlCommand(@" Select t.DATE6, t.PXXXX" + seasonal +
                        @",  t.P00XX" + seasonal + @",  t.PNRXX" + seasonal + @",  t.P02XX" + seasonal +
                        @",  t.P03XX" + seasonal + @",  t.P04XX" + seasonal + @",  t.P05XX" + seasonal +
                        @",  t.P07XX" + seasonal + @",  t.P08XX" + seasonal + @",  t.P09XX" + seasonal +
                        @",  t.P11XX" + seasonal + @",  t.P12XX" + seasonal + @",  t.P13XX" + seasonal +
                        @",  t.P14XX" + seasonal + @",  t.P15XX" + seasonal + @", t1.SXXXX" + seasonal +
                        @", t1.S00XX" + seasonal + @", t1.SNRXX" + seasonal + @", t1.S02XX" + seasonal +
                        @", t1.S03XX" + seasonal + @", t1.S04XX" + seasonal + @", t1.S05XX" + seasonal +
                        @", t1.S07XX" + seasonal + @", t1.S08XX" + seasonal + @", t1.S09XX" + seasonal +
                        @", t1.S11XX" + seasonal + @", t1.S12XX" + seasonal + @", t1.S13XX" + seasonal +
                        @", t1.S14XX" + seasonal + @", t1.S15XX" + seasonal + @", t2.FXXXX" + seasonal +
                        @", t2.F00XX" + seasonal + @", t2.FNRXX" + seasonal + @", t2.F02XX" + seasonal +
                        @", t2.F03XX" + seasonal + @", t2.F04XX" + seasonal + @", t2.F05XX" + seasonal +
                        @", t2.F07XX" + seasonal + @", t2.F08XX" + seasonal + @", t2.F09XX" + seasonal +
                        @", t2.F11XX" + seasonal + @", t2.F12XX" + seasonal + @", t2.F15XX" + seasonal +
                        @" From dbo." + stable +
                        @" as t
                           RIGHT OUTER JOIN dbo." + stable1 + @" as t1 on  t.DATE6 = t1.DATE6
                           LEFT  OUTER JOIN dbo." + stable2 + @" as t2 ON t1.DATE6 = t2.DATE6 
                           where t.DATE6 <= '" + sdate + "' ORDER BY t.DATE6 DESC", sql_connection);

                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);

                    //populate header texts
                    dt.Columns[0].Caption = "Date";
                    dt.Columns[1].Caption = "Total\n\rPublic Construction";
                    dt.Columns[2].Caption = "Public\n\rResidential";
                    dt.Columns[3].Caption = "Public\n\rNonresidential";
                    dt.Columns[4].Caption = "Public\n\rOffice";
                    dt.Columns[5].Caption = "Public\n\rCommercial";
                    dt.Columns[6].Caption = "Public\n\rHealth care";
                    dt.Columns[7].Caption = "Public\n\rEducational";
                    dt.Columns[8].Caption = "Public\n\rPublic safety";
                    dt.Columns[9].Caption = "Public\n\rAmusement and recreation";
                    dt.Columns[10].Caption = "Public\n\rTransportation";
                    dt.Columns[11].Caption = "Public\n\rPower";
                    dt.Columns[12].Caption = "Public\n\rHighway and street";
                    dt.Columns[13].Caption = "Public\n\rSewage and waste disposal";
                    dt.Columns[14].Caption = "Public\n\rWater supply";
                    dt.Columns[15].Caption = "Public\n\rConservation and development";
                    dt.Columns[16].Caption = "Total\n\rState and Local Construction";
                    dt.Columns[17].Caption = "State\n\rResidential";
                    dt.Columns[18].Caption = "State\n\rNonresidential";
                    dt.Columns[19].Caption = "State\n\rOffice";
                    dt.Columns[20].Caption = "State\n\rCommercial";
                    dt.Columns[21].Caption = "State\n\rHealth care";
                    dt.Columns[22].Caption = "State\n\rEducational";
                    dt.Columns[23].Caption = "State\n\rPublic safety";
                    dt.Columns[24].Caption = "State\n\rAmusement and recreation";
                    dt.Columns[25].Caption = "State\n\rTransportation";
                    dt.Columns[26].Caption = "State\n\rPower";
                    dt.Columns[27].Caption = "State\n\rHighway and street";
                    dt.Columns[28].Caption = "State\n\rSewage and waste disposal";
                    dt.Columns[29].Caption = "State\n\rWater supply";
                    dt.Columns[30].Caption = "State\n\rConservation and development";
                    dt.Columns[31].Caption = "Total\n\rFederal Construction";
                    dt.Columns[32].Caption = "Federal\n\rResidential";
                    dt.Columns[33].Caption = "Federal\n\rNonresidential";
                    dt.Columns[34].Caption = "Federal\n\rOffice";
                    dt.Columns[35].Caption = "Federal\n\rCommercial";
                    dt.Columns[36].Caption = "Federal\n\rHealth care";
                    dt.Columns[37].Caption = "Federal\n\rEducational";
                    dt.Columns[38].Caption = "Federal\n\rPublic safety";
                    dt.Columns[39].Caption = "Federal\n\rAmusement and recreation";
                    dt.Columns[40].Caption = "Federal\n\rTransportation";
                    dt.Columns[41].Caption = "Federal\n\rPower";
                    dt.Columns[42].Caption = "Federal\n\rHighway and street";
                    dt.Columns[43].Caption = "Federal\n\rConservation and development";

                    //loop through rows to format date column
                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string date_value = dt.Rows[i][0].ToString();
                        DateTime col = DateTime.ParseExact(date_value, "yyyyMM", null);
                        dt.Rows[i][0] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                        string stdate = "200201";
                        DateTime startdate = DateTime.ParseExact(stdate, "yyyyMM", null);

                        //filter through date column and if date is less than 200201 for
                        //specific tsar series do not output data - insert a zero which then
                        //gets blankout out in excel file
                        if (col < startdate)
                        {
                            dt.Rows[i]["P00XX" + seasonal] = 0;
                            dt.Rows[i]["PNRXX" + seasonal] = 0;
                            dt.Rows[i]["P02XX" + seasonal] = 0;
                            dt.Rows[i]["P03XX" + seasonal] = 0;
                            dt.Rows[i]["P04XX" + seasonal] = 0;
                            dt.Rows[i]["P05XX" + seasonal] = 0;
                            dt.Rows[i]["P07XX" + seasonal] = 0;
                            dt.Rows[i]["P08XX" + seasonal] = 0;
                            dt.Rows[i]["P09XX" + seasonal] = 0;
                            dt.Rows[i]["P11XX" + seasonal] = 0;
                            dt.Rows[i]["P12XX" + seasonal] = 0;
                            dt.Rows[i]["P13XX" + seasonal] = 0;
                            dt.Rows[i]["P14XX" + seasonal] = 0;
                            dt.Rows[i]["P15XX" + seasonal] = 0;
                            dt.Rows[i]["F00XX" + seasonal] = 0;
                            dt.Rows[i]["FNRXX" + seasonal] = 0;
                            dt.Rows[i]["F02XX" + seasonal] = 0;
                            dt.Rows[i]["F03XX" + seasonal] = 0;
                            dt.Rows[i]["F04XX" + seasonal] = 0;
                            dt.Rows[i]["F05XX" + seasonal] = 0;
                            dt.Rows[i]["F07XX" + seasonal] = 0;
                            dt.Rows[i]["F08XX" + seasonal] = 0;
                            dt.Rows[i]["F09XX" + seasonal] = 0;
                            dt.Rows[i]["F11XX" + seasonal] = 0;
                            dt.Rows[i]["F12XX" + seasonal] = 0;
                            dt.Rows[i]["F15XX" + seasonal] = 0;
                        }
                    }
                }
                //STATE AND LOCAL TABLE   

                else if (tableType == "S")
                {
                    if (seasonal == "SAA")
                    {
                        stable = "SAAPUB";
                    }
                    else
                    {
                        stable = "UNAPUB";
                    }

                    sql_command = new SqlCommand(@" Select t.DATE6, t.SXXXX" + seasonal +
                        @", t.S00XX" + seasonal + @", t.S002X" + seasonal + @", t.SNRXX" + seasonal +
                        @", t.S02XX" + seasonal + @", t.S03XX" + seasonal + @", t.S031X" + seasonal +
                        @", t.S0313" + seasonal + @", t.S04XX" + seasonal + @", t.S041X" + seasonal +
                        @", t.S042X" + seasonal + @", t.S043X" + seasonal + @", t.S05XX" + seasonal +
                        @", t.S052X" + seasonal + @", t.S0521" + seasonal + @", t.S0522" + seasonal +
                        @", t.S0523" + seasonal + @", t.S053X" + seasonal + @", t.S0531" + seasonal +
                        @", t.S0535" + seasonal + @", t.S0538" + seasonal + @", t.S0539" + seasonal +
                        @", t.S055X" + seasonal + @", t.S0553" + seasonal + @", t.S07XX" + seasonal +
                        @", t.S071X" + seasonal + @", t.S0711" + seasonal + @", t.S0712" + seasonal +
                        @", t.S072X" + seasonal + @", t.S0721" + seasonal + @", t.S08XX" + seasonal +
                        @", t.S082X" + seasonal + @", t.S084X" + seasonal + @", t.S0844" + seasonal +
                        @", t.S085X" + seasonal + @", t.S0852" + seasonal + @", t.S086X" + seasonal +
                        @", t.S09XX" + seasonal + @", t.S091X" + seasonal + @", t.S0911" + seasonal +
                        @", t.S0912" + seasonal + @", t.S092X" + seasonal + @", t.S0921" + seasonal +
                        @", t.S0923" + seasonal + @", t.S093X" + seasonal + @", t.S0931" + seasonal +
                        @", t.S11XX" + seasonal + @", t.S12XX" + seasonal + @", t.S12CC" + seasonal +
                        @", t.S1213" + seasonal + @", t.S1216" + seasonal + @", t.S1219" + seasonal +
                        @", t.S13XX" + seasonal + @", t.S131X" + seasonal + @", t.S1312" + seasonal +
                        @", t.S13CC" + seasonal + @", t.S132X" + seasonal + @", t.S1321" + seasonal +
                        @", t.S1322" + seasonal + @", t.S14XX" + seasonal + @", t.S1411" + seasonal +
                        @", t.S1413" + seasonal + @", t.S1414" + seasonal + @", t.S15XX" + seasonal +
                        @", t.S1521" + seasonal + @", t.S1522" + seasonal +
                        @" From dbo." + stable +
                        @" as t where DATE6 <= '" + sdate + "' ORDER BY DATE6 DESC", sql_connection);

                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string date_value = dt.Rows[i][0].ToString();
                        DateTime col = DateTime.ParseExact(date_value, "yyyyMM", null);
                        dt.Rows[i][0] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    //populate header texts
                    dt.Columns[0].Caption = "Date";
                    dt.Columns[1].Caption = "Total\n\rState and Local Construction";
                    dt.Columns[2].Caption = "Residential";
                    dt.Columns[3].Caption = "Multifamily";
                    dt.Columns[4].Caption = "Nonresidential";
                    dt.Columns[5].Caption = "Office";
                    dt.Columns[6].Caption = "Commercial";
                    dt.Columns[7].Caption = "Automotive";
                    dt.Columns[8].Caption = "Parking";
                    dt.Columns[9].Caption = "Health care";
                    dt.Columns[10].Caption = "Hospital";
                    dt.Columns[11].Caption = "Medical building";
                    dt.Columns[12].Caption = "Special care";
                    dt.Columns[13].Caption = "Educational";
                    dt.Columns[14].Caption = "Primary/secondary";
                    dt.Columns[15].Caption = "Elementary";
                    dt.Columns[16].Caption = "Middle/junior high";
                    dt.Columns[17].Caption = "High";
                    dt.Columns[18].Caption = "Higher education";
                    dt.Columns[19].Caption = "Instructional";
                    dt.Columns[20].Caption = "Dormitory";
                    dt.Columns[21].Caption = "Sports/recreation";
                    dt.Columns[22].Caption = "Infrastructure";
                    dt.Columns[23].Caption = "Other educational";
                    dt.Columns[24].Caption = "Library/archive";
                    dt.Columns[25].Caption = "Public Safety";
                    dt.Columns[26].Caption = "Correctional";
                    dt.Columns[27].Caption = "Detention";
                    dt.Columns[28].Caption = "Police/sheriff";
                    dt.Columns[29].Caption = "Other public safety";
                    dt.Columns[30].Caption = "Fire/rescue";
                    dt.Columns[31].Caption = "Amusement and recreation";
                    dt.Columns[32].Caption = "Sports";
                    dt.Columns[33].Caption = "Performance/ meeting center";
                    dt.Columns[34].Caption = "Convention center";
                    dt.Columns[35].Caption = "Social center";
                    dt.Columns[36].Caption = "Neighborhood center";
                    dt.Columns[37].Caption = "Park/camp";
                    dt.Columns[38].Caption = "Transportation";
                    dt.Columns[39].Caption = "Air";
                    dt.Columns[40].Caption = "Air passenger terminal";
                    dt.Columns[41].Caption = "Runway";
                    dt.Columns[42].Caption = "Land";
                    dt.Columns[43].Caption = "Land passenger terminal";
                    dt.Columns[44].Caption = "Mass transit";
                    dt.Columns[45].Caption = "Water";
                    dt.Columns[46].Caption = "Dock/marina";
                    dt.Columns[47].Caption = "Power";
                    dt.Columns[48].Caption = "Highway and street";
                    dt.Columns[49].Caption = "Pavement";
                    dt.Columns[50].Caption = "Lighting";
                    dt.Columns[51].Caption = "Bridge";
                    dt.Columns[52].Caption = "Rest facility";
                    dt.Columns[53].Caption = "Sewage and waste disposal";
                    dt.Columns[54].Caption = "Sewage/dry waste";
                    dt.Columns[55].Caption = "Sewage treatment plant";
                    dt.Columns[56].Caption = "Line/pump station";
                    dt.Columns[57].Caption = "Waste water";
                    dt.Columns[58].Caption = "Waste water treatment plant";
                    dt.Columns[59].Caption = "Line/drain";
                    dt.Columns[60].Caption = "Water supply";
                    dt.Columns[61].Caption = "Water treatment plant";
                    dt.Columns[62].Caption = "Line";
                    dt.Columns[63].Caption = "Pump station";
                    dt.Columns[64].Caption = "Conservation and development";
                    dt.Columns[65].Caption = "Dam/levee";
                    dt.Columns[66].Caption = "Breakwater/jetty";
                }
                //FEDERAL TABLE  
                
                else if (tableType == "F")
                {
                    if (seasonal == "SAA")
                    {
                        stable = "SAAFED";
                    }
                    else
                    {
                        stable = "UNAFED";
                    }

                    sql_command = new SqlCommand(@" Select t.DATE6, t.FXXXX" + seasonal +
                        @", t.F00XX" + seasonal + @", t.FNRXX" + seasonal + @", t.F02XX" + seasonal +
                        @", t.F03XX" + seasonal + @", t.F04XX" + seasonal + @", t.F05XX" + seasonal +
                        @", t.F07XX" + seasonal + @", t.F08XX" + seasonal + @", t.F09XX" + seasonal +
                        @", t.F11XX" + seasonal + @", t.F12XX" + seasonal + @", t.F15XX" + seasonal +
                        @" From dbo." + stable +
                        @" as t where DATE6 <= '" + sdate + "' ORDER BY DATE6 DESC", sql_connection);

                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);

                    dt.Columns[0].Caption = "Date";
                    dt.Columns[1].Caption = "Total\n\rFederal Construction";
                    dt.Columns[2].Caption = "Residential";
                    dt.Columns[3].Caption = "Nonresidential";
                    dt.Columns[4].Caption = "Office";
                    dt.Columns[5].Caption = "Commercial";
                    dt.Columns[6].Caption = "Health care";
                    dt.Columns[7].Caption = "Educational";
                    dt.Columns[8].Caption = "Public safety";
                    dt.Columns[9].Caption = "Amusement and recreation";
                    dt.Columns[10].Caption = "Transportation";
                    dt.Columns[11].Caption = "Power";
                    dt.Columns[12].Caption = "Highway and street";
                    dt.Columns[13].Caption = "Conservation and development";

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string date_value = dt.Rows[i][0].ToString();
                        DateTime col = DateTime.ParseExact(date_value, "yyyyMM", null);
                        dt.Rows[i][0] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                        string stdate = "200201";
                        DateTime startdate = DateTime.ParseExact(stdate, "yyyyMM", null);

                        //filter through date column and if date is less than 200201 for
                        //specific tsar series do not output data - insert a zero which then
                        //gets blankout out in excel file
                        if (col < startdate)
                        {
                            dt.Rows[i]["F00XX" + seasonal] = 0;
                            dt.Rows[i]["FNRXX" + seasonal] = 0;
                            dt.Rows[i]["F02XX" + seasonal] = 0;
                            dt.Rows[i]["F03XX" + seasonal] = 0;
                            dt.Rows[i]["F04XX" + seasonal] = 0;
                            dt.Rows[i]["F05XX" + seasonal] = 0;
                            dt.Rows[i]["F07XX" + seasonal] = 0;
                            dt.Rows[i]["F08XX" + seasonal] = 0;
                            dt.Rows[i]["F09XX" + seasonal] = 0;
                            dt.Rows[i]["F11XX" + seasonal] = 0;
                            dt.Rows[i]["F12XX" + seasonal] = 0;
                            dt.Rows[i]["F15XX" + seasonal] = 0;
                        }
                    }
                }
                //PRIVATE TABLE   
                
                else if (tableType == "V")
                {
                    if (seasonal == "SAA")
                    {
                        stable = "SAAPRV";
                    }
                    else
                    {
                        stable = "UNAPRV";
                    }

                    sql_command = new SqlCommand(@" Select t.DATE6, t.VXXXX" + seasonal +
                        @", t.V00XX" + seasonal + @", t.X0011" + seasonal + @", t.VCC21" + seasonal +
                        @", t.VNRXX" + seasonal + @", t.V01XX" + seasonal + @", t.V02XX" + seasonal +
                        @", t.N0211" + seasonal + @", t.N0215" + seasonal + @", t.V02CC" + seasonal + @", t.V03XX" + seasonal +
                        @", t.V031X" + seasonal + @", t.N0311" + seasonal + @", t.N0312" + seasonal +
                        @", t.N0313" + seasonal + @", t.V032X" + seasonal + @", t.N0321" + seasonal +
                        @", t.N0322" + seasonal + @", t.V033X" + seasonal + @", t.N0331" + seasonal +
                        @", t.N0332" + seasonal + @", t.N0333" + seasonal + @", t.V034X" + seasonal +
                        @", t.N0345" + seasonal + @", t.N0348" + seasonal + @", t.N0341" + seasonal +
                        @", t.V035X" + seasonal + @", t.N0351" + seasonal + @", t.N0354" + seasonal +
                        @", t.V04XX" + seasonal + @", t.V041X" + seasonal + @", t.V042X" + seasonal +
                        @", t.V043X" + seasonal + @", t.V05XX" + seasonal + @", t.V051X" + seasonal +
                        @", t.V052X" + seasonal + @", t.V053X" + seasonal + @", t.N0531" + seasonal +
                        @", t.N0535" + seasonal + @", t.N0538" + seasonal + @", t.V055X" + seasonal +
                        @", t.N0554" + seasonal + @", t.V06XX" + seasonal + @", t.V061X" + seasonal +
                        @", t.V062X" + seasonal + @", t.N0624" + seasonal + @", t.V08XX" + seasonal +
                        @", t.V081X" + seasonal + @", t.V082X" + seasonal + @", t.V083X" + seasonal +
                        @", t.V084X" + seasonal + @", t.V085X" + seasonal + @", t.V087X" + seasonal +
                        @", t.V09XX" + seasonal + @", t.V091X" + seasonal + @", t.V092X" + seasonal +
                        @", t.V10XX" + seasonal + @", t.V11XX" + seasonal + @", t.V111X" + seasonal +
                        @", t.V20IX" + seasonal + @", t.V20CC" + seasonal + @", t.V28XX" + seasonal +
                        @", t.V30XX" + seasonal + @", t.V32XX" + seasonal + @", t.V34XX" + seasonal +
                        @", t.V38CC" + seasonal + @", t.V37XX" + seasonal +
                        @" From dbo." + stable +
                        @" as t where DATE6 <= '" + sdate + "' ORDER BY DATE6 DESC", sql_connection);

                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);

                    for (int i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        string date_value = dt.Rows[i][0].ToString();
                        DateTime col = DateTime.ParseExact(date_value, "yyyyMM", null);
                        dt.Rows[i][0] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                    }

                    //populate header texts
                    dt.Columns[0].Caption = "Date";
                    dt.Columns[1].Caption = "Total\n\rPrivate Construction";
                    dt.Columns[2].Caption = "Residential (inc. improvements)";
                    dt.Columns[3].Caption = "New single family";
                    dt.Columns[4].Caption = "New multifamily";
                    dt.Columns[5].Caption = "Nonresidential";
                    dt.Columns[6].Caption = "Lodging";
                    dt.Columns[7].Caption = "Office";
                    dt.Columns[8].Caption = "General";
                    dt.Columns[9].Caption = "Data center";
                    dt.Columns[10].Caption = "Financial";
                    dt.Columns[11].Caption = "Commercial (inc. Farm)";
                    dt.Columns[12].Caption = "Automotive";
                    dt.Columns[13].Caption = "Sales";
                    dt.Columns[14].Caption = "Service/parts";
                    dt.Columns[15].Caption = "Parking";
                    dt.Columns[16].Caption = "Food/beverage";
                    dt.Columns[17].Caption = "Food";
                    dt.Columns[18].Caption = "Dining/drinking";
                    dt.Columns[19].Caption = "Multi-retail";
                    dt.Columns[20].Caption = "General merchandise";
                    dt.Columns[21].Caption = "Shopping center";
                    dt.Columns[22].Caption = "Shopping mall";
                    dt.Columns[23].Caption = "Other commercial";
                    dt.Columns[24].Caption = "Drug store";
                    dt.Columns[25].Caption = "Building supply store";
                    dt.Columns[26].Caption = "Other stores";
                    dt.Columns[27].Caption = "Warehouse";
                    dt.Columns[28].Caption = "General commercial";
                    dt.Columns[29].Caption = "Mini-storage";
                    dt.Columns[30].Caption = "Health care";
                    dt.Columns[31].Caption = "Hospital";
                    dt.Columns[32].Caption = "Medical building";
                    dt.Columns[33].Caption = "Special care";
                    dt.Columns[34].Caption = "Educational";
                    dt.Columns[35].Caption = "Preschool";
                    dt.Columns[36].Caption = "Primary/secondary";
                    dt.Columns[37].Caption = "Higher education";
                    dt.Columns[38].Caption = "Instructional";
                    dt.Columns[39].Caption = "Dormitory";
                    dt.Columns[40].Caption = "Sports/recreation";
                    dt.Columns[41].Caption = "Other educational";
                    dt.Columns[42].Caption = "Gallery/museum";
                    dt.Columns[43].Caption = "Religious";
                    dt.Columns[44].Caption = "House of worship";
                    dt.Columns[45].Caption = "Other religious";
                    dt.Columns[46].Caption = "Auxiliary building";
                    dt.Columns[47].Caption = "Amusement and Recreation";
                    dt.Columns[48].Caption = "Theme/amusement park";
                    dt.Columns[49].Caption = "Sports";
                    dt.Columns[50].Caption = "Fitness";
                    dt.Columns[51].Caption = "Performance/ meeting center";
                    dt.Columns[52].Caption = "Social center";
                    dt.Columns[53].Caption = "Movie theater/ studio";
                    dt.Columns[54].Caption = "Transportation";
                    dt.Columns[55].Caption = "Air";
                    dt.Columns[56].Caption = "Land";
                    dt.Columns[57].Caption = "Communication";
                    dt.Columns[58].Caption = "Power (inc. Gas and Oil)";
                    dt.Columns[59].Caption = "Electric";
                    dt.Columns[60].Caption = "Manufacturing";
                    dt.Columns[61].Caption = "Food/beverage/ tobacco";
                    dt.Columns[62].Caption = "Chemical";
                    dt.Columns[63].Caption = "Plastic/rubber";
                    dt.Columns[64].Caption = "Nonmetallic mineral";
                    dt.Columns[65].Caption = "Fabricated metal";
                    dt.Columns[66].Caption = "Computer/ electronic/ electrical";
                    dt.Columns[67].Caption = "Transportation equipment";
                }
                else
                {
                    sql_command = new SqlCommand("");

                    // Create a DataAdapter to run the command and fill the DataTable
                    SqlDataAdapter da = new SqlDataAdapter(sql_command);
                    da.Fill(dt);
                }

                return dt;
            }
        }
    }
}

