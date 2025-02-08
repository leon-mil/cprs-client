/**************************************************************************************
Econ App Name  : CPRS

Project Name   : CPRS Interactive Screens System

Program Name   : CprsBLL.RespAddr.cs	    	

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
***************************************************************************************
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
    public class RespAddr
    {
        private string respid;
        private string resporg;
        private string respname;
        private string respname2;
        private string respnote;
        private string factoff;
        private string othrresp;
        private string addr1;
        private string addr2;
        private string addr3;
        private string phone;
        private string phone2;
        private string ext;
        private string ext2;
        private string fax;
        private string zip;
        private string email;
        private string web;
        private int lag;
        private string rstate;
        private string resplock;
        private string coltec;
        private string colhist;

        public RespAddr()
        {
        }

        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        public string Resporg
        {
            get { return resporg; }
            set { resporg = value; }
        }

        public string Respname
        {
            get { return respname; }
            set { respname = value; }
        }

        public string Respname2
        {
            get { return respname2; }
            set { respname2 = value; }
        }

        public string Respnote
        {
            get { return respnote; }
            set { respnote = value; }
        }

        public string Factoff
        {
            get { return factoff; }
            set { factoff = value; }
        }
        public string Othrresp
        {
            get { return othrresp; }
            set { othrresp = value; }
        }

        public string Addr1
        {
            get { return addr1; }
            set { addr1 = value; }
        }
        public string Addr2
        {
            get { return addr2; }
            set { addr2 = value; }
        }

        public string Addr3
        {
            get { return addr3; }
            set { addr3 = value; }
        }

        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Phone2
        {
            get { return phone2; }
            set { phone2 = value; }
        }

        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        public string Ext2
        {
            get { return ext2; }
            set { ext2 = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }

        public string Zip
        {
            get { return zip; }
            set { zip = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Web
        {
            get { return web; }
            set { web = value; }
        }

        public int Lag
        {
            get { return lag; }
            set { lag = value; }
        }

        public string Rstate
        {
            get { return rstate; }
            set { rstate = value; }
        }

        public string Resplock
        {
            get { return resplock; }
            set { resplock = value; }
        }

        public string Coltec
        {
            get { return coltec; }
            set { coltec = value; }
        }

        public string Colhist
        {
            get { return colhist; }
            set { colhist = value; }
        }
    }

    public class UpdateComm
    {
        private string respid;
        private string commdate;
        private string usrnme;
        private string commtext;
        private string commtime;

        public UpdateComm()
        {
        }

        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        public string Commdate
        {
            get { return commdate; }
            set { commdate = value; }
        }

        public string Usrnme
        {
            get { return usrnme; }
            set { usrnme = value; }
        }

        public string Commtext
        {
            get { return commtext; }
            set { commtext = value; }
        }

        public string Commtime
        {
            get { return commtime; }
            set { commtime = value; }
        }

    }

    public class UpdateLock
    {
        private string resplock;
        private string respid;

        public UpdateLock()
        {
        }

        public string Respid
        {
            get { return respid; }
            set { respid = value; }
        }

        public string Resplock
        {
            get { return resplock; }
            set { resplock = value; }
        }
    }

    public class MatchUser
    {
        private string username;
        private string userid;

        public MatchUser()
        {
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Userid
        {
            get { return userid; }
            set { userid = value; }
        }

    }

   
}
