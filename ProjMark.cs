
/**************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : CprsBLL.Projmark.cs	    	
Programmer      : Christine Zhang
Creation Date   : 11/16/2015
Inputs          : None
Parameters      : None 
Outputs         : cemark, CemarkUpdateEventArgs
Description     : This class creates the getters and setters and stores
                  the data that will be used in the improvement screen
                  include CemarkUpdateEventArgs structure and cemark structure
Detailed Design : None 
Other           : Called By: frmImprovement
 
Revision History:	
****************************************************************************************
Modified Date  :  
Modified By    :  
Keyword        :  
Change Request :  
Description    :  
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class ProjMark
    {
        /*Construction*/
        public ProjMark()
        {          
        }

        /*properties*/
        public string Id { get; set; }
        public string Prgdtm { get; set; }
        public string Usrnme { get; set; }
        public string Marktext { get; set; }    
    }

    public class RespMark
    {
        /*Construction*/
        public RespMark()
        {
        }

        /*properties*/
        public string Respid { get; set; }
        public string Prgdtm { get; set; }
        public string Usrnme { get; set; }
        public string Marktext { get; set; }
    }
}
