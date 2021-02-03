/***********************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.MySector.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/5/2016
Inputs:             None
Parameters:	        None 
Outputs:	        None
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the Mysector screen

Detailed Design:    None 

Other:	            Called By: frmMySectorPopup
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class MySector
    {
       
        public MySector(string user_name) 
        {
            Username = user_name;
        }
        public string Username { get; private set; }
        public string Sect00 { get; set; }
        public string Sect01 { get; set; }
        public string Sect02 { get; set; }
        public string Sect03 { get; set; }
        public string Sect04 { get; set; }
        public string Sect05 { get; set; }
        public string Sect06 { get; set; }
        public string Sect07 { get; set; }
        public string Sect08 { get; set; }
        public string Sect09 { get; set; }
        public string Sect10 { get; set; }
        public string Sect11 { get; set; }
        public string Sect12 { get; set; }
        public string Sect13 { get; set; }
        public string Sect14 { get; set; }
        public string Sect15 { get; set; }
        public string Sect16 { get; set; }
        public string Sect19 { get; set; }
        public string Sect1T { get; set; }

        //Check the newtc in my sector or not
        public bool CheckInMySector(string newtc)
        {
            bool in_sector = false;

            if (newtc.Length == 4)
                newtc = newtc.Substring(0, 2);

            if ((newtc == "00") && (Sect00 == "Y"))
                in_sector = true;
            else if ((newtc == "01") && (Sect01 == "Y"))
                in_sector = true;
            else if ((newtc == "02") && (Sect02 == "Y"))
                in_sector = true;
            else if ((newtc == "03") && (Sect03 == "Y"))
                in_sector = true;
            else if ((newtc == "04") && (Sect04 == "Y"))
                in_sector = true;
            else if ((newtc == "05") && (Sect05 == "Y"))
                in_sector = true;
            else if ((newtc == "06") && (Sect06 == "Y"))
                in_sector = true;
            else if ((newtc == "07") && (Sect07 == "Y"))
                in_sector = true;
            else if ((newtc == "08") && (Sect08 == "Y"))
                in_sector = true;
            else if ((newtc == "09") && (Sect09 == "Y"))
                in_sector = true;
            else if ((newtc == "10") && (Sect10 == "Y"))
                in_sector = true;
            else if ((newtc == "11") && (Sect11 == "Y"))
                in_sector = true;
            else if ((newtc == "12") && (Sect12 == "Y"))
                in_sector = true;
            else if ((newtc == "13") && (Sect13 == "Y"))
                in_sector = true;
            else if ((newtc == "14") && (Sect14 == "Y"))
                in_sector = true;
            else if ((newtc == "15") && (Sect15 == "Y"))
                in_sector = true;
            else if ((newtc == "16") && (Sect16 == "Y"))
                in_sector = true;
            else if ((newtc == "19") && (Sect1T == "Y"))
                in_sector = true;
            else if ((Convert.ToInt16(newtc) > 19) && (Sect1T == "Y"))
                in_sector = true;
            
            return in_sector;
        }
    }
}
