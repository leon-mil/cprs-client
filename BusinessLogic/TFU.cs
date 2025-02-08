
/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.TFU.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/16/2015
Inputs:             None
Parameters:	        None 
Outputs:	        
Description:	    define strutures used in TFU screen
Detailed Design:    None 
Other:	            Called By: frmTFU
 
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
using System.ComponentModel;

namespace CprsBLL
{
    public enum TypeTFUEntryPoint
    {
        NPC = 1,
        FORM = 2,
        SEARCH = 3
    }

    public class TFUProject
    {
        [DisplayName("ID")]
        public string Id { get; set; }

        [DisplayName("PROJDESC")]
        public string Projdesc { get; set; }

        public string Contract { get; set; }

        [DisplayName("")]
        public string Satisfied { get; set; }

        public string Compdater { get; set; }

        public string Compdate { get; set; }

        public string Status {get; set;}
       
        public string Priority { get; set; }

        public string Callreq { get; set; }

        public string Owner { get; set; }


    }
}
