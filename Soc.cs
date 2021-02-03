/*****************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.SOC.cs	    	
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
***************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Soc
    {
         public Soc (int mid)
        {
            masterid = mid;
        }
       
        private int masterid;
        public int Masterid
        {
            get { return masterid; }
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        private string psu;
        public string Psu
        {
            get { return psu; }
            set
            {
                if (value != psu)
                {
                    psu = value;
                }
            }
        }

        private string place;
        public string Place
        {
            get { return place; }
            set
            {
                if (value != place)
                {
                    place = value;
                    isModified = true;
                }
            }
        }

        private string sched;
        public string Sched
        {
            get { return sched; }
            set
            {
                if (value != sched)
                {
                    sched = value;
                }
            }
        }


        private int bldgs;
        public int Bldgs
        {
            get { return bldgs; }
            set
            {
                if (value != bldgs)
                {
                    bldgs = value;
                    isModified = true;
                }
            }
        }

        private int rbldgs;
        public int Rbldgs
        {
            get { return rbldgs; }
            set
            {
                if (value != rbldgs)
                {
                    rbldgs = value;
                    isModified = true;
                }
            }
        }


        private int units;
        public int Units
        {
            get { return units; }
            set
            {
                if (value != units)
                {
                    units = value;
                    isModified = true;
                }
            }
        }

        private int runits;
        public int Runits
        {
            get { return runits; }
            set
            {
                if (value != runits)
                {
                    runits = value;
                    isModified = true;
                }
            }
        }


        private int costpu;
        public int Costpu
        {
            get { return costpu; }
            set
            {
                if (value != costpu)
                {
                    costpu = value;
                    isModified = true;
                }
            }
        }

        private float socwt;
        public float Socwt
        {
            get { return socwt; }
            set
            {
                if (value != socwt)
                {
                    socwt = value;
                    isModified = true;
                }
            }
        }

        private string unitflg;
        public string Unitflg
        {
            get { return unitflg; }
            set
            {
                if (value != unitflg)
                {
                    unitflg = value;
                    isModified = true;
                }
            }
        }
       
        
        public string psuplacesched 
        { 
            get { return this.psu + this.place + this.sched;} 
        }
    }
}
