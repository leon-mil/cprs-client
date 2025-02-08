/*
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : CprsBLL.NameAddrResp.cs

 Programmer    : Srini Natarajan

 Creation Date : 3/24/2016

 Inputs        : n/a
 
 Parameters    : n/a
 
 Output        : n/a
 
 Description   : a structure/class to store data from Respondent table

 Detail Design : 
 
 Other         : Called by CprsDAL.NameData.cs and used by frmName

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class NameAddrResp
    {
        private string id;
        private string respid;
        private string factoff;
        private string othrresp;
        private string addr1;
        private string addr2;
        private string addr3;
        private string zip;
        private string resporg;
        private string respname;
        private string respname2;
        private string respnote;
        private string phone;
        private string phone2;
        private string ext;
        private string ext2;
        private string fax;
        private string email;
        private string weburl;
        private string coltec;
        private string colhist;
        private string structcd;
        private string resplock;
        private string fin;
        private string active;
           
        public NameAddrResp() { }

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
        public string Zip
        {
            get { return zip; }
            set { zip = value; }
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
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Weburl
        {
            get { return weburl; }
            set { weburl = value; }
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
        public string Structcd
        {
            get { return structcd; }
            set { structcd = value; }
        }
        public string RespLock
        {
            get { return resplock; }
            set { resplock = value; }
        }
        public string Fin
        {
            get { return fin; }
            set { fin = value; }
        }
        public string Active
        {
            get { return active; }
            set { active = value; }
        }
    }
}