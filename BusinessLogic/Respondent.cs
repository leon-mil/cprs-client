
/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Respondent.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/4/2015
Inputs:             None
Parameters:	        id
Outputs:	        
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the c700 screen
                    
Detailed Design:    None 
Other:	            Called By: frmC700
 
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
    public class Respondent
    {
        public Respondent(string rid)
        {
            respid = rid;
        }
       
        private string respid;
        public string Respid
        {
            get { return respid; }
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        private string resporg;
        public string Resporg
        {
            get { return resporg; }
            set
            {
                if (value != resporg)
                {
                    resporg = value;
                    isModified = true;
                }
            }
        }


        private string respname;
        public string Respname
        {
            get { return respname; }
            set
            {
                if (value != respname)
                {
                    respname = value;
                    isModified = true;
                }
            }
        }

        private string respname2 = "";
        public string Respname2
        {
            get { return respname2; }
            set
            {
                if (value != respname2)
                {
                    respname2 = value;
                    isModified = true;
                }
            }
        }

        private string respnote;
        public string Respnote
        {
            get { return respnote; }
            set
            {
                if (value != respnote)
                {
                    respnote = value;
                    isModified = true;
                }
            }
        }

        private string addr1;
        public string Addr1
        {
            get { return addr1; }
            set
            {
                if (value != addr1)
                {
                    addr1 = value;
                    isModified = true;
                }
            }
        }

        private string addr2;
        public string Addr2
        {
            get { return addr2; }
            set
            {
                if (value != addr2)
                {
                    addr2 = value;
                    isModified = true;
                }
            }
        }


        private string addr3;
        public string Addr3
        {
            get { return addr3; }
            set
            {
                if (value != addr3)
                {
                    addr3 = value;
                    isModified = true;
                }
            }
        }

        private string zip;
        public string Zip
        {
            get { return zip; }
            set
            {
                if (value != zip)
                {
                    zip = value;
                    isModified = true;
                }
            }
        }

        private string phone;
        public string Phone
        {
            get { return phone; }
            set
            {
                if (value != phone)
                {
                    phone = value;
                    isModified = true;
                }
            }
        }

        private string phone2 = "";
        public string Phone2
        {
            get { return phone2; }
            set
            {
                if (value != phone2)
                {
                    phone2 = value;
                    isModified = true;
                }
            }
        }

        private string ext;
        public string Ext
        {
            get { return ext; }
            set
            {
                if (value != ext)
                {
                    ext = value;
                    isModified = true;
                }
            }
        }

        private string ext2 = "";
        public string Ext2
        {
            get { return ext2; }
            set
            {
                if (value != ext2)
                {
                    ext2 = value;
                    isModified = true;
                }
            }
        }

        private string fax;
        public string Fax
        {
            get { return fax; }
            set
            {
                if (value != fax)
                {
                    fax = value;
                    isModified = true;
                }
            }
        }


        private string factoff;
        public string Factoff
        {
            get { return factoff; }
            set
            {
                if (value != factoff)
                {
                    factoff = value;
                    isModified = true;
                }
            }
        }


        private string othrresp;
        public string Othrresp
        {
            get { return othrresp; }
            set
            {
                if (value != othrresp)
                {
                    othrresp = value;
                    isModified = true;
                }
            }
        }

        private string email;
        public string Email
        {
            get { return email; }
            set
            {
                if (value != email)
                {
                    email = value;
                    isModified = true;
                }
            }
        }


        private string weburl;
        public string Weburl
        {
            get { return weburl; }
            set
            {
                if (value != weburl)
                {
                    weburl = value;
                    isModified = true;
                }
            }
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

        private int lag;
        public int Lag
        {
            get { return lag; }
            set
            {
                if (value != lag)
                {
                    lag = value;
                    isModified = true;
                }
            }
        }


        private string resplock;
        public string Resplock
        {
            get { return resplock; }
            set
            {
                if (value != resplock)
                {
                    resplock = value;
                    isModified = true;
                }
            }
        }

        private string centpwd;
        public string Centpwd
        {
            get { return centpwd; }
            set
            {
                if (value != centpwd)
                {
                    centpwd = value;
                    isModified = true;
                }
            }
        }

        private string coltec;
        public string Coltec
        {
            get { return coltec; }
            set
            {
                if (value != coltec)
                {
                    coltec = value;
                    isModified = true;
                }
            }
        }

        private string colhist;
        public string Colhist
        {
            get { return colhist; }
            set
            {
                if (value != colhist)
                {
                    colhist = value;
                    isModified = true;
                }
            }
        } 
            

    }
}
