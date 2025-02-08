/**************************************************************************************
Econ App Name  : CPRS

Project Name   : CPRS Interactive Screens System

Program Name   : CprsBLL.NameAddrAudit.cs	    	

Programmer     : Srini Natarajan

Creation Date  : 08/27/2015

Inputs         : None.

Parameters     : None.

Outputs        : Respondent Audit Trail data	

Description    : This class creates the getters and setters and stores
                 the data that will be used in the Respondent Audit Trail

Detailed Design: None 

Other          : Called By: frmName.cs
 
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
    public class NameAddrAudit
    {
        private string id;
        private string respid;
        private string varnme;
        private string oldval;
        private string oldflag;
        private string newflag;
        private string newval;
        //private DateTime prgdtm;
        private string usrnme;

        public NameAddrAudit()
        {
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
        
        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        public string Oldfalg
        {
            get { return oldflag; }
            set { oldflag = value; }
        }

        public string Newflag
        {
            get { return newflag; }
            set { newflag = value; }
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