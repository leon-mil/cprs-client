/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.SearchCriteria.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/6/2015
Inputs:             None
Parameters:	        None 
Outputs:	        
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the whereStore screen
                    
Detailed Design:    None 
Other:	            Called By: frmWhereStore.cs
 
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
using System.Data.SqlClient;
using CprsBLL;

namespace CprsBLL
{

    public class SearchCriteria
    {

        /*Construction*/
        public SearchCriteria()
        {          
        }

        /*properties*/
        //public string Usrnme { get; set; }
        public int Seqno { get; set; }
        public string Wheretext { get; set; }

    }
}
