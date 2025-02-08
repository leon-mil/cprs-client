/**************************************************************************************
Econ App Name  : CPRS

Project Name   : CPRS Interactive Screens System

Program Name   : CprsBLL.RespAudit.cs	    	

Programmer     : Diane Musachio

Creation Date  : 08/27/2015

Inputs         : None

Parameters     : None 

Outputs        : Respondent Address data	

Description    : This class creates the getters and setters and stores
                 the data that will be used in the Respondent Address Update screen

Detailed Design: None 

Other          : Called By: frmRespAddrUpdate.cs
 
Rev History    :	
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
using CprsBLL;

namespace CprsBLL
{
    public class RespAudit
    {
        private string respid;
        private string varnme;
        private string oldval;
        private string newval;
        
        private string usrnme;

        public RespAudit()
        {
        }

        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        public string Varnme
        {
            get { return varnme; }
            set { varnme = value; }
        }

        public string Oldval
        {
            get { return oldval; }
            set { oldval = value; }
        }

        public string Newval
        {
            get { return newval; }
            set { newval = value; }
        }
        public DateTime Prgdtm { get; set; }

        public string Usrnme
        {
            get { return usrnme; }
            set { usrnme = value; }
        }

    }
}