/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsBLL.NameAddrfactor.cs	    	

Programmer:         Srini Natarajan

Creation Date:      03/24/2016

Inputs:             None

Parameters:	        None 

Outputs:	        Contractor Factor data	

Description:	    This class creates the getters and setters and stores
                    the Contractor data that will be used to replace in the Name Address Display screen

Detailed Design:    None 

Other:	            Called By: frmName.cs
 
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

namespace CprsBLL
{
    public class NameAddrFactor
    {
        private string f7resporg;
        private string f7respname;
        private string f7addr1;
        private string f7addr2;
        private string f7addr3;
        private string f7zip;
        private string f7phone;
        private string f7email;
        private string f7weburl;
        private string timezone;

        public NameAddrFactor() { }

        //************* Contractor ***************

        public string F7resporg
        {
            get { return f7resporg; }
            set { f7resporg = value; }
        }

        public string F7respname
        {
            get { return f7respname; }
            set { f7respname = value; }
        }

        public string F7addr1
        {
            get { return f7addr1; }
            set { f7addr1 = value; }
        }

        public string F7addr2
        {
            get { return f7addr2; }
            set { f7addr2 = value; }
        }

        public string F7addr3
        {
            get { return f7addr3; }
            set { f7addr3 = value; }
        }

        public string F7zip
        {
            get { return f7zip; }
            set { f7zip = value; }
        }

        public string F7phone
        {
            get { return f7phone; }
            set { f7phone = value; }
        }

        public string F7email
        {
            get { return f7email; }
            set { f7email = value; }
        }

        public string F7weburl
        {
            get { return f7weburl; }
            set { f7weburl = value; }
        }
        public string Timezone
        {
            get { return timezone; }
            set { timezone = value; }
        }
        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        private string rstate;
        public string Rstate
        {
            get { return rstate; }
            set
            {
                if (value != rstate)
                {
                    rstate = value;
                    isModified = true;
                }
            }
        }

    }
}
