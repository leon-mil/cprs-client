/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsBLL.ImpTabReview.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/28/2015

Inputs:             None

Parameters:	        None 

Outputs:	        C30Tab Data - Winsorized and UnWinsorized

Description:	    This class creates the getters and setters and stores
                    the data that will be used in the Improvements Tab Review screen

Detailed Design:    None 

Other:	            Called By: frmImpTabReview.cs
 
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
   public class ImpTabReview
    {
       private string date6;
       private decimal lag0;
       private decimal lag1;
       private decimal lag2;
       private decimal lag3;
       private int tcases;
       private int tjobs;
       private int avgcost;

       public ImpTabReview() { }

       public string Date6
       {
           get { return date6; }
           set { date6 = value; }
       }

       public decimal Lag0
       {
           get { return lag0; }
           set { lag0 = value; }
       }

       public decimal Lag1
       {
           get { return lag1; }
           set { lag1 = value; }
       }

       public decimal Lag2
       {
           get { return lag2; }
           set { lag2 = value; }
       }

       public decimal Lag3
       {
           get { return lag3; }
           set { lag3 = value; }
       }

       public int Tcases
       {
           get { return tcases; }
           set { tcases = value; }
       }

       public int Tjobs
       {
           get { return tjobs; }
           set { tjobs = value; }
       }

       public int Avgcost
       {
           get { return avgcost; }
           set { avgcost = value; }
       }
    }
}
