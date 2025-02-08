/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Cesample.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2015
Inputs:             Mcdid
Parameters:	        
Outputs:	        Cesample data
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen
Detailed Design:    None 
Other:	            Called By: frmImprovement
 
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
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Cesample
    {
        public Cesample (string mid )
        {
            id = mid;
        }

        /*readonly property mcdid */
        private string id;
        public string Id
        {
            get { return id; }         
        }

        /*property survdate */
        private string survdate;
        public string Survdate
        {
            get { return survdate; }
            set { survdate = value; }
        }

        /*property interview */
        private string interview;
        public string Interview
        {
            get { return interview; }
            set { interview = value; }
        }

        /*property state code*/
        private string state;
        public string State
        {
            get { return state; }
            set { state = value; }
        }

        /*property two chars state string */
        private string stater;
        public string Stater
        {
            get { return stater; }
            set { stater = value; }
        }

        /*property county */
        private string county;
        public string County
        {
            get { return county; }
            set { county = value; }
        }

        /*property cbsa */
        private string cbsa;
        public string Cbsa
        {
            get { return cbsa; }
            set { cbsa = value; }
        }

        /*property intsat */
        private string intsta;
        public string Intsta
        {
            get { return intsta; }
            set { intsta = value; }
        }

        /*property tenure */
        private string tenure;
        public string Tenure
        {
            get { return tenure; }
            set { tenure = value; }
        }

        /*property yrbuild */
        private string yrbuilt;
        public string Yrbuilt
        {
            get { return yrbuilt; }
            set { yrbuilt = value; }
        }

        /*property yrset */
        private string yrset;
        public string Yrset
        {
            get { return yrset; }
            set { yrset = value; }
        }

        /*property city */
        private string city;
        public string City
        {
            get { return city; }
            set { city = value; }
        }

        /*property zip */
        private string zip;
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        /*property units */
        private string units;
        public string Units
        {
            get { return units; }
            set { units = value; }
        }

        /*property income */
        private int income;
        public int Income
        {
            get { return income; }
            set { income = value; }
        }

        /*property propval */
        private int propval;
        public int Propval
        {
            get { return propval; }
            set { propval = value; }
        }

        /*property finwt */
        private float finwt;
        public float Finwt
        {
            get { return finwt; }
            set { finwt = value; }
        }

        /*property i1finwt */
        private float i1finwt;
        public float I1finwt
        {
            get { return i1finwt; }
            set { i1finwt = value; }
        }

        /*property i2finwt */
        private float i2finwt;
        public float I2finwt
        {
            get { return i2finwt; }
            set { i2finwt = value; }
        }

        /*property i3finwt */
        private float i3finwt;
        public float I3finwt
        {
            get { return i3finwt; }
            set { i3finwt = value; }
        }

        /*property i4finwt */
        private float i4finwt;
        public float I4finwt
        {
            get { return i4finwt; }
            set { i4finwt = value; }
        }

        /*property i5finwt */
        private float i5finwt;
        public float I5finwt
        {
            get { return i5finwt; }
            set { i5finwt = value; }
        }

        /*property bedroom */
        private int bedroom;
        public int Bedroom
        {
            get { return bedroom; }
            set { bedroom = value; }
        }

        /*property bathroom */
        private int bathroom;
        public int Bathroom
        {
            get { return bathroom; }
            set { bathroom = value; }
        }

        /*property hlfroom */
        private int hlfbath;
        public int Hlfbath
        {
            get { return hlfbath; }
            set { hlfbath = value; }
        }

        /********************* Public methods ***********************/

        /*get display test for units */
        public string GetDisplayUnits()
        {
            switch (units)
            {
                case "01":
                    return "Other Units";
                case "02":
                    return "Mobile Home";
                case "03":
                    return "One, Detached";
                case "04":
                    return "One, Attached";
                case "05":
                    return "2 Units";
                case "06":
                    return "3-4 Units";
                case "07":
                    return "5-9 Units";
                case "08":
                    return "10-19 Units";
                case "09":
                    return "20-49 Units";
                case "10":
                    return "50 + Units";

                default:
                    return "";
            }

        }

        /*Get display test for yrbuilt */
        public string GetDisplayYrbuilt()
        {
            switch (yrbuilt)
            {
                case "01":
                    return "Before 1960";
                case "02":
                    return "1960 - 1969";
                case "03":
                    return "1970 - 1979";
                case "04":
                    return "1980 - 1989";
                case "05":
                    return "1990 - 1994";
                case "06":
                    return "1995 - 1999";
                case "07":
                    return "2000 - 2004";
                case "08":
                    return "2005 - 2009";
                case "09":
                    return "2010 - 2014";
                case "10":
                    return "2015 - 2019";
                case "11":
                    return "2020 - 2024";
                case "12":
                    return "2025 - 2029";

                default:
                    return "";
            }

        }

        /*Find finwt for interview */
        public float GetFinwtForInterview(string pinterview)
        {
            if (pinterview == interview)
                return finwt;

            switch (pinterview)
	        {
	            case "1":
                    return i1finwt;
                case "2":
                    return i2finwt;
                case "3":
                    return i3finwt;
                case "4":
                    return i4finwt;
                default:
                    return i5finwt;
            }
        }   

    }
}
