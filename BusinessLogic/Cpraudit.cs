/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Ceaudit.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2015
Inputs:             None
Parameters:	        None 
Outputs:	        None
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen

Detailed Design:    None 

Other:	            Called By: frmImprovement
 
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
    public class Cpraudit
    {
        public Cpraudit() {}

        public string Id { get; set; }
        public string Varnme { get; set; }
        public string Oldval { get; set; }
        public string Oldflag { get; set; }
        public string Newval { get; set; }
        public string Newflag { get; set; }
        public string Usrnme { get; set; }
        public DateTime Progdtm { get; set; }
    }

    public class Vipaudit
    {
        public Vipaudit() { }

        public string Id { get; set; }
        public string date6 { get; set; }
        public int Oldvip { get; set; }
        public string Oldflag { get; set; }
        public int Newvip { get; set; }
        public string Newflag { get; set; }
        public string Usrnme { get; set; }
        public DateTime Progdtm { get; set; }
    }

    public class Respaudit
    {
        public Respaudit() { }

        public string Respid { get; set; }
        public string Varnme { get; set; }
        public string Oldval { get; set; }
        public string Newval { get; set; }
        public string Usrnme { get; set; }
        public DateTime Progdtm { get; set; }
    }
}
