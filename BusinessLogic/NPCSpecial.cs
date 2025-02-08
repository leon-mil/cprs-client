/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.NPCSpecial.cs	    	
Programmer:         Christine Zhang
Creation Date:      01/28/2020
Inputs:             None
Parameters:	        None 
Outputs:	        NPC Special
Description:	    
Detailed Design:    None 
Other:	            Called By: frmNPCSpecial
 
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
    public class NPCSpecial
    {
        public NPCSpecial(string rid)
        {
            respid = rid;
        }

        private string respid;
        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        private string resporg;
        public string Resporg
        {
            get { return resporg; }
            set { resporg = value; }
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        private string coltec;
        public string Coltec
        {
            get { return coltec; }
            set { coltec = value; }
        }

        private string cstatus1;
        public string Cstatus1
        {
            get { return cstatus1; }
            set { cstatus1 = value; }
        }

        private string cstatus2;
        public string Cstatus2
        {
            get { return cstatus2; }
            set { cstatus2 = value; }
        }

        private string cstatus3;
        public string Cstatus3
        {
            get { return cstatus3; }
            set { cstatus3 = value; }
        }

        private string cstatus4;
        public string Cstatus4
        {
            get { return cstatus4; }
            set { cstatus4 = value; }
        }

        private string cstatus5;
        public string Cstatus5
        {
            get { return cstatus5; }
            set { cstatus5 = value; }
        }

        private string rstatus1;
        public string Rstatus1
        {
            get { return rstatus1; }
            set { rstatus1 = value; }
        }

        private string rstatus2;
        public string Rstatus2
        {
            get { return rstatus2; }
            set { rstatus2 = value; }
        }

        private string rstatus3;
        public string Rstatus3
        {
            get { return rstatus3; }
            set { rstatus3 = value; }
        }

        private string rstatus4;
        public string Rstatus4
        {
            get { return rstatus4; }
            set { rstatus4 = value; }
        }

        private string rstatus5;
        public string Rstatus5
        {
            get { return rstatus5; }
            set { rstatus5 = value; }
        }

        private string rstatus6;
        public string Rstatus6
        {
            get { return rstatus6; }
            set { rstatus6 = value; }
        }
    }
}
