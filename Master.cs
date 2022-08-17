/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Master.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/4/2015
Inputs:             None
Parameters:	        id
Outputs:	        
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the C700 screen
                    
Detailed Design:    None 
Other:	            Called By: frmC700
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
********************************************************************* 
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Master
    {

        public Master (int mid)
        {
            masterid = mid;
        }

        private int masterid;
        public int Masterid
        {
            get { return masterid; }
            set { masterid = value; }
        }
       
        private string dodgenum;
        public string Dodgenum
        {
            get { return dodgenum; }
            set { dodgenum = value; }
        }

        private string fin;
        public string Fin
        {
            get { return fin; }
            set { fin = value; }
        }

        private string dodgecou;
        public string Dodgecou
        {
            get { return dodgecou; }
            set { dodgecou = value; }
        }

        private bool isModified = false;
        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        private string owner;
        public string Owner
        {
            get { return owner; }
            set
            {
                if (value != owner)
                {
                    owner = value;
                    isModified = true;
                }
            }
        }

        private string seldate;
        public string Seldate
        {
            get { return seldate; }
            set
            {
                if (value != seldate)
                {
                    seldate = value;
                    isModified = true;
                }
            }
        }

        private string fipstate;
        public string Fipstate
        {
            get { return fipstate; }
            set
            {
                if (value != fipstate)
                {
                    fipstate = value;
                    isModified = true;
                }
            }
        }

        private string fipstater;
        public string Fipstater
        {
            get { return fipstater; }
            set
            {                   
                fipstater = value;
            }
        }


        private int projselv;
        public int Projselv
        {
            get { return projselv; }
            set
            {
                if (value != projselv)
                {
                    projselv = value;
                    isModified = true;
                }
            }
        }

        private int tvalue;
        public int Tvalue
        {
            get { return tvalue; }
            set
            {
                if (value != tvalue)
                {
                    tvalue = value;
                    isModified = true;
                }
            }
        }

        private string mtf;
        public string Mtf
        {
            get { return mtf; }
            set
            {
                if (value != mtf)
                {
                    mtf = value;
                    isModified = true;
                }
            }
        }

        private string structcd;
        public string Structcd
        {
            get { return structcd; }
            set
            {
                if (value != structcd)
                {
                    structcd = value;
                    isModified = true;
                }
            }
        }

        private string newtc = String.Empty;
        public string Newtc
        {
            get { return newtc; }
            set
            {
                if (value != newtc)
                {
                    newtc = value;
                    isModified = true;
                }
            }
        }

        private string source;
        public string Source
        {
            get { return source; }
            set
            {
                if (value != source)
                {
                    source = value;
                    isModified = true;
                }
            }
        }

        private string chip;
        public string Chip
        {
            get { return chip; }
            set
            {
                if (value != chip)
                {
                    chip = value;
                    isModified = true;
                }
            }
        }

        private int stratid;
        public int Stratid
        {
            get { return stratid; }
            set { stratid = value; }
        }

        private double dmf=1.00;
        public double Dmf
        {
            get { return dmf; }
            set { dmf = value; }

        }

    }
}
