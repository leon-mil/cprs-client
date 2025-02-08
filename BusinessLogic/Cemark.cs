/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Cemark.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/6/2015
Inputs:             None
Parameters:	        None 
Outputs:	        cemark, CemarkUpdateEventArgs
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen
                    include CemarkUpdateEventArgs structure and cemark structure
Detailed Design:    None 
Other:	            Called By: frmImprovement
 
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
    public class Cemark
    {
        /*Construction*/
        public Cemark()
        {          
        }

        /*properties*/
        public string Id { get; set; }
        public string Prgdtm { get; set; }
        public string Usrnme { get; set; }
        public string Marktext { get; set; }
           
    }


    ///*CemarkUpdateEventArgs */
    //public class CemarkUpdateEventArgs
    //{
    //    private string prgdtm;
    //    private string mark;

    //    // class constructor
    //    public CemarkUpdateEventArgs(string sPrgdtm, string sMark)
    //    {
    //        this.prgdtm = sPrgdtm;
    //        this.mark = sMark;
    //    }

    //    /*property Prgdtm */
    //    public string Prgdtm
    //    {
    //        get
    //        {
    //            return prgdtm;
    //        }
    //    }

    //    /*property mark */
    //    public string Mark
    //    {
    //        get
    //        {
    //            return mark;
    //        }
    //    }

    //}
}
