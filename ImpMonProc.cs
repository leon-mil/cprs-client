/*************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsBLL.ImpMonProc.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/30/2015

Inputs:             None

Parameters:	    None 

Outputs:	    None	

Description:	    This class creates structures the getters and setters 
 *                  and stores the data that will be used in the 
 *                  Improvements Monthly Processing screen

Detailed Design : Detailed Design for Improvements Monthly Processing 

Other           : Called By: frmImpMonProc.cs
 
Revision History:	
*********************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :   
*********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class ImpMonProc
    {

        private string statp;
        private string task01a;
        private string task01b;
        private string task02a;
        private string task02b;
        private string task03a;
        private string task03b;
        private string task04a;
        private string task04b;

         public ImpMonProc() { }

         public string Statp
         {
             get { return statp; }
             set { statp = value; }
         }

         public string Task01a
         {
             get { return task01a; }
             set { task01a = value; }
         }

         public string Task01b
         {
             get { return task01b; }
             set { task01b = value; }
         }

         public string Task02a
         {
             get { return task02a; }
             set { task02a = value; }
         }

         public string Task02b
         {
             get { return task02b; }
             set { task02b = value; }
         }

         public string Task03a
         {
             get { return task03a; }
             set { task03a = value; }
         }

         public string Task03b
         {
             get { return task03b; }
             set { task03b = value; }
         }

         public string Task04a
         {
             get { return task04a; }
             set { task04a = value; }
         }

         public string Task04b
         {
             get { return task04b; }
             set { task04b = value; }
         }
    }
}
