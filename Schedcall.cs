/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Scheducall.cs	    	
Programmer:         Christine Zhang
Creation Date:      10/19/2017
Inputs:             None
Parameters:	        
Outputs:	        Sched call rec
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the C700, TFU screen
                    
Detailed Design:    None 
Other:	            Called By: frmC700, frmTFU
 
Revision History:	
*********************************************************************
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
    public class Schedcall
    {
         /*Construction*/
        public Schedcall(string cid)
        {
            id = cid;
        }

        /*properties*/
        private string id= string.Empty ;
        public string Id
        {
            get { return id; }
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        private string callreq = "N";
        public string Callreq
        {
            get { return callreq; }
            set
            {
                if (value != callreq)
                {
                    callreq = value;
                    isModified = true;
                }
            }
        }

        private string calltpe = "W";
        public string Calltpe
        {
            get { return calltpe; }
            set { calltpe = value; }
        }

        private string priority = "";
        public string Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        private string priorityDesc = "";
        public string PriorityDesc
        {
            get { return priorityDesc; }
            set { priorityDesc = value; }
        }

        private string accesday= string.Empty;
        public string Accesday
        {
            get { return accesday; }
            set
            {
                if (value != accesday)
                {
                    accesday = value;
                    isModified = true;
                }
            }
        }

        private string accestms = string.Empty;
        public string Accestms
        {
            get { return accestms; }
            set
            {
                if (value != accestms)
                {
                    accestms = value;
                    isModified = true;
                }
            }
        }

        private string accestme = string.Empty;
        public string Accestme
        {
            get { return accestme; }
            set
            {
                if (value != accestme)
                {
                    accestme = value;
                    isModified = true;
                }
            }
        }

        private string accescde = string.Empty;
        public string Accescde
        {
            get { return accescde; }
            set
            {
                if (value != accescde)
                {
                    accescde = value;
                    isModified = true;
                }
            }
        }

        private string accesnme = string.Empty;
        public string Accesnme
        {
            get { return accesnme; }
            set
            {
                if (value != accesnme)
                {
                    accesnme = value;
                    isModified = true;
                }
            }
        }


        private string callstat = string.Empty;
        public string Callstat
        {
            get { return callstat; }
            set
            {
                if (value != callstat)
                {
                    callstat = value;
                    isModified = true;
                }
            }
        }

        private int callcnt = 0;
        public int Callcnt
        {
            get { return callcnt; }
            set
            {
                if (value != callcnt)
                {
                    callcnt = value;
                    isModified = true;
                }
            }
        }

        private string complete = "N";
        public string Complete
        {
            get { return complete; }
            set
            {
                if (value != complete)
                {
                    complete = value;
                    isModified = true;
                }
            }
        }

        private int lvmcnt = 0;
        public int LVMcnt
        {
            get { return lvmcnt; }
            set
            {
                if (value != lvmcnt)
                {
                    lvmcnt = value;
                    isModified = true;
                }
            }
        }

        private string apptdate = string.Empty;
        public string Apptdate
        {
            get { return apptdate; }
            set
            {
                if (value != apptdate)
                {
                    apptdate = value;
                    isModified = true;
                }
            }
        }

        private string appttime = string.Empty;
        public string Appttime
        {
            get { return appttime; }
            set
            {
                if (value != appttime)
                {
                    appttime = value;
                    isModified = true;
                }
            }
        }

        private string apptends = string.Empty;
        public string Apptends
        {
            get { return apptends; }
            set
            {
                if (value != apptends)
                {
                    apptends = value;
                    isModified = true;
                }
            }
        }

        private string coltec;
        public string Coltec
        {
            get { return coltec; }
            set { coltec = value; }
        }

        private string added = "N";
        public string Added
        {
            get { return added; }
            set
            {
                if (value != added)
                {
                    added = value;
                    isModified = true;
                }
            }
        } 
    }

    public class Schedhist
    {
          /*Construction*/
        public Schedhist(string cid)
        {
            id = cid;
        }

        /*properties*/
        private string id;
        public string Id
        {
            get { return id; }
        }

        private string owner= string.Empty;
        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        private string accesday;
        public string Accesday
        {
            get { return accesday; }
            set { accesday = value; }
        }

        private string accestms;
        public string Accestms
        {
            get { return accestms; }
            set { accestms = value; }
        }

        private string accestme;
        public string Accestme
        {
            get { return accestme; }
            set { accestme = value; }
        }

        private string accesnme;
        public string Accesnme
        {
            get { return accesnme; }
            set { accesnme = value; }
        }

        private string accescde;
        public string Accescde
        {
            get { return accescde; }
            set { accescde = value; }
        }

        private string callstat= string.Empty;
        public string Callstat
        {
            get { return callstat; }
            set { callstat = value; }
        }

        
    }
}
