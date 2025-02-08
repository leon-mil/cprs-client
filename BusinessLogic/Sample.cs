/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Sample.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/4/2015
Inputs:             None
Parameters:	        
Outputs:	        Sample rec
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the C700 screen
                    
Detailed Design:    None 
Other:	            Called By: frmC700
 
Revision History:	
***************************************************************************************
 Modified Date :  January 25, 2021
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  
 Description   :  Add Projlength to Class
****************************************************************************************
Modified Date :  03/13/2023
Modified By   :  Christine Zhang
Keyword       :  
Change Request:  CR#917
Description   : add Tag to sample table
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public enum TypeStatus
    {
        Mail = 1,
        PNR = 2,
        Phone = 3,
        Abeyance = 4,
        Duplicate = 5,
        OS = 6,
        DCPNR = 7,
        Internet = 8

    }

    public enum TypeDBSource
    {
        Default = 1,       
        Hold =2       
    }

    public enum TypeEditMode
    {
        Display =1,
        Edit =2
    }

    public class Sample
    {
        public Sample (string cid )
        {
            id = cid;
        }
       
        private string id;
        public string Id
        {
            get { return id; }         
        }

        private int masterid;
        public int Masterid
        {
            get { return masterid; }
            set { masterid = value; }
        }

        private string respid;
        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value;}
        }

       private string projdesc;
       public string Projdesc
        {
            get { return projdesc; }
            set
            {
                if (value != projdesc)
                {
                    projdesc = value;
                    isModified = true;
                }
            }
        }

        private string contract;
        public string Contract
        {
            get { return contract; }
            set
            {
                if (value != contract)
                {
                    contract = value;
                    isModified = true;
                }
            }
        }

        private string projloc;
        public string Projloc
        {
            get { return projloc; }
            set
            {
                if (value != projloc)
                {
                    projloc = value;
                    isModified = true;
                }
            }
        }

        private string pcityst;
        public string Pcityst
        {
            get { return pcityst; }
            set
            {
                if (value != pcityst)
                {
                    pcityst = value;
                    isModified = true;
                }
            }
        }

        private float fwgt;
        public float Fwgt
        {
            get { return fwgt; }
            set
            {
                if (value != fwgt)
                {
                    fwgt = value;
                    isModified = true;
                }
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                if (value != status)
                {
                    status = value;
                    isModified = true;
                }
            }
        }

        private string statdate;
        public string Statdate
        {
            get { return statdate; }
            set
            {
                if (value != statdate)
                {
                    statdate = value;
                    isModified = true;
                }
            }
        }

        private string strtdate;
        public string Strtdate
        {
            get { return strtdate; }
            set
            {
                if (value != strtdate)
                {
                    strtdate = value;
                    isModified = true;
                }
            }
        }

        private string strtdater;
        public string Strtdater
        {
            get { return strtdater; }
            set
            {
                if (value != strtdater)
                {
                    strtdater = value;
                    isModified = true;
                }
            }
        }

        private string repsdate;
        public string Repsdate
        {
            get { return repsdate; }
            set
            {
                if (value != repsdate)
                {
                    repsdate = value;
                    isModified = true;
                }
            }
        }


        private string compdate ="";
        public string Compdate
        {
            get { return compdate; }
            set
            {
                if (value != compdate)
                {
                    compdate = value;
                    isModified = true;
                }
            }

        }

        private string compdater = "";
        public string Compdater
        {
            get { return compdater; }
            set
            {
                if (value != compdater)
                {
                    compdater = value;
                    isModified = true;
                }
            }
        }

        private int item5a;
        public int Item5a
        {
            get { return item5a; }
            set
            {
                if (value != item5a)
                {
                    item5a = value;
                    isModified = true;
                }
            }
        }

        private int item5ar;
        public int Item5ar
        {
            get { return item5ar; }
            set
            {
                if (value != item5ar)
                {
                    item5ar = value;
                    isModified = true;
                }
            }
        }


        private int item5b;
        public int Item5b
        {
            get { return item5b; }
            set
            {
                if (value != item5b)
                {
                    item5b = value;
                    isModified = true;
                }
            }
        }

        private int item5br;
        public int Item5br
        {
            get { return item5br; }
            set
            {
                if (value != item5br)
                {
                    item5br = value;
                    isModified = true;
                }
            }
        }

        private int item5c;
        public int Item5c
        {
            get { return item5c; }
            set
            {
                if (value != item5c)
                {
                    item5c = value;
                    isModified = true;
                }
            }
        }

        private int rvitm5c;
        public int Rvitm5c
        {
            get { return rvitm5c; }
            set
            {
                if (value != rvitm5c)
                {
                    rvitm5c = value;
                    isModified = true;
                }
            }
        }

        private int rvitm5cr;
        public int Rvitm5cr
        {
            get { return rvitm5cr; }
            set
            {
                if (value != rvitm5cr)
                {
                    rvitm5cr = value;
                    isModified = true;
                }
            }
        }

        private int item6;
        public int Item6
        {
            get { return item6; }
            set
            {
                if (value != item6)
                {
                    item6 = value;
                    isModified = true;
                }
            }
        }

        private int item6r;
        public int Item6r
        {
            get { return item6r; }
            set
            {
                if (value != item6r)
                {
                    item6r = value;
                    isModified = true;
                }
            }
        }

        private int capexp;
        public int Capexp
        {
            get { return capexp; }
            set
            {
                if (value != capexp)
                {
                    capexp = value;
                    isModified = true;
                }
            }
        }

        private int capexpr;
        public int Capexpr
        {
            get { return capexpr; }
            set
            {
                if (value != capexpr)
                {
                    capexpr = value;
                    isModified = true;
                }
            }
        }

        private string flag5a;
        public string Flag5a
        {
            get { return flag5a; }
            set
            {
                if (value != flag5a)
                {
                    flag5a = value;
                    isModified = true;
                }
            }
        }

        private string flag5b;
        public string Flag5b
        {
            get { return flag5b; }
            set
            {
                if (value != flag5b)
                {
                    flag5b = value;
                    isModified = true;
                }
            }
        }

        private string flag5c;
        public string Flag5c
        {
            get { return flag5c; }
            set
            {
                if (value != flag5c)
                {
                    flag5c = value;
                    isModified = true;
                }
            }
        }

        private string flagr5c;
        public string Flagr5c
        {
            get { return flagr5c; }
            set
            {
                if (value != flagr5c)
                {
                    flagr5c = value;
                    isModified = true;
                }
            }
        }

        private string flagitm6;
        public string Flagitm6
        {
            get { return flagitm6; }
            set
            {
                if (value != flagitm6)
                {
                    flagitm6 = value;
                    isModified = true;
                }
            }
        }

        private string flagcap;
        public string Flagcap
        {
            get { return flagcap; }
            set
            {
                if (value != flagcap)
                {
                    flagcap = value;
                    isModified = true;
                }
            }
        }

        private string flagcompdate;
        public string Flagcompdate
        {
            get { return flagcompdate; }
            set
            {
                if (value != flagcompdate)
                {
                    flagcompdate = value;
                    isModified = true;
                }
            }
        }

        private string futcompd = "";
        public string Futcompd
        {
            get { return futcompd; }
            set
            {
                if (value != futcompd)
                {
                    futcompd = value;
                    isModified = true;
                }
            }
        }

        private string futcompdr = "";
        public string Futcompdr
        {
            get { return futcompdr; }
            set
            {
                if (value != futcompdr)
                {
                    futcompdr = value;
                    isModified = true;
                }
            }
        }

        private string flagstrtdate;
        public string Flagstrtdate
        {
            get { return flagstrtdate; }
            set
            {
                if (value != flagstrtdate)
                {
                    flagstrtdate = value;
                    isModified = true;
                }
            }
        }

        private string flagfutcompd;
        public string Flagfutcompd
        {
            get { return flagfutcompd; }
            set
            {
                if (value != flagfutcompd)
                {
                    flagfutcompd = value;
                    isModified = true;
                }
            }
        }

        
        private string repcompd = "";
        public string Repcompd
        {
            get { return repcompd; }
            set
            {
                if (value != repcompd)
                {
                    repcompd = value;
                    isModified = true;
                }
            }
        }

        private string flgmodel;
        public string Flgmodel
        {
            get { return flgmodel; }
            set
            {
                if (value != flgmodel)
                {
                    flgmodel = value;
                    isModified = true;
                }
            }
        }

        private string model;
        public string Model
        {
            get { return model; }
            set
            {
                if (value != model)
                {
                    model = value;
                    isModified = true;
                }
            }
        }

        private float oadj;
        public float Oadj
        {
            get { return oadj; }
            set
            {
                if (value != oadj)
                {
                    oadj = value;
                    isModified = true;
                }
            }
        }


        private string oflg;
        public string Oflg
        {
            get { return oflg; }
            set
            {
                if (value != oflg)
                {
                    oflg = value;
                    isModified = true;
                }
            }
        }

        private string sampseq;
        public string Sampseq
        {
            get { return sampseq; }
            set
            {
                if (value != sampseq)
                {
                    sampseq = value;
                    isModified = true;
                }
            }
        }

        private int projlength;
        public int Projlength
        {
            get { return projlength; }
            set
            {
                if (value != projlength)
                {
                    projlength = value;
                    isModified = true;
                }
            }
        }

        private int tag;
        public int Tag
        {
            get { return tag; }
            set
            {
                if (value != tag)
                {
                    tag = value;
                    isModified = true;
                }
            }
        }

        private string active;
        public string Active
        {
            get { return active; }
            set
            {
                if (value != active)
                {
                    active = value;
                    isModified = true;
                }
            }
        }


        
    }

   
}
