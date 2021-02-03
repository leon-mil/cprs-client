/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.NameAddrmark.cs	    	
Programmer:         Srini Natarajan
Creation Date:      11/9/2015
Inputs:             None
Parameters:	        None 
Outputs:	        NameAddrMark, NamemarkUpdateEventArgs
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen
                    include CemarkUpdateEventArgs structure and cemark structure
Detailed Design:    None 
Other:	            Called By: frmName
 
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
    public class NameAddrmark
    {
        /*Construction*/
        public NameAddrmark()
        {          
        }

        /*properties*/
        public string Id { get; set; }
        public string Date8 { get; set; }
        public string Usrnme { get; set; }
        public string Marktext { get; set; }
        public string commtext { get; set; }
    }

    /*NameAddrmarkUpdateEventArgs */
    public class NamemarkUpdateEventArgs
    {
        private string date8;
        private string mark;
        private string comm;

        // class constructor
        public NamemarkUpdateEventArgs(string sDate8, string sMark, string sComm)
        {
            this.date8 = sDate8;
            this.mark = sMark;
            this.comm = sComm;
        }

        /*property date8 */
        public string Date8
        {
            get
            {
                return date8;
            }
        }

        /*property mark */
        public string Mark
        {
            get
            {
                return mark;
            }
        }

        /*property comm */
        public string Comm
        {
            get
            {
                return comm;
            }
        }
    
    }
}
