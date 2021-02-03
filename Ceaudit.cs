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
    public class Ceaudit
    {
        public Ceaudit() {}

        public string Id { get; set; }
        public string Interview { get; set; }
        public string Detcode { get; set; }
        public string Varnme { get; set; }
        public int Oldval { get; set; }
        public string Oldflag { get; set; }
        public int Newval { get; set; }
        public string Newflag { get; set; }
        public string Usrnme { get; set; }
        public DateTime Progdtm { get; set; }
        
    }
}
