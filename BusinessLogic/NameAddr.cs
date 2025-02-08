/*
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : NameAddr.cs

 Programmer    : Srini Natarajan

 Creation Date : 4/2/2015

 Inputs        : n/a
 
 Parameters    : n/a
 
 Output        : n/a
 
 Description   : a structure/class to store data from NAME_ADDR_DISPLAY View

 Detail Design : Detailed User Requirements for Name Address Display Screen
 
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
    public class NameAddr
    {
        private string id;
        private string respid;
        private string seldate;
        private string selvalue;
        private string statuscode;
        private string fipstate;
        private string dodgecou;
        private string survey;
        private string fwgt;
        private string newtc;
        private string source;
        private string rbldgs;
        private string runits;
        private string dodgenum;
        private int masterid;
        private string psu;
        private string place;
        private string sched;
        private string contract;
        private string projdesc;
        private string projloc;
        private string pcityst;
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
        private string rvitm5cr;
        private string rvitm5c;
        private string flagr5c;
        private string flagr5c1;
        private string flag5a;
        private string flag5b;
        private string flagitm6;
        private string flagcap;
        private string accescde;
        private string strtdater;
        private string strtdate;
        private string flagstrtdate;
        private string compdater;
        private string compdate;
        private string flagcompdate;
        private string futcompdr;
        private string futcompd;
        private string flagfutcompd;
        private string centpwd;
        private string coltec;
        private string colhist;
        private string structcd;
        private string resplock;
        private string fin;
        private string active;
        private string timezone;
        private string rstate;
        private string lag;
        private bool isModified = false;
           
        public NameAddr() { }

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
        public string Seldate
        {
            get { return seldate; }
            set { seldate = value; }
        }
        public string Selvalue
        {
            get { return selvalue; }
            set { selvalue = value; }
        }
        public string Statuscode
        {
            get { return statuscode; }
            set { statuscode = value; }
        }
        public string Fipstate
        {
            get { return fipstate; }
            set { fipstate = value; }
        }

        public string Dodgecou
        {
            get { return dodgecou; }
            set { dodgecou = value; }
        }

        public string Survey
        {
            get { return survey; }
            set { survey = value; }
        }
        public string Fwgt
        {
            get { return fwgt; }
            set { fwgt = value; }
        }
        public string Newtc
        {
            get { return newtc; }
            set { newtc = value; }
        }
        public string Source
        {
            get { return source; }
            set { source = value; }
        }
        public string Rbldgs
        {
            get { return rbldgs; }
            set { rbldgs = value; }
        }
        public string Runits
        {
            get { return runits; }
            set { runits = value; }
        }
        public int Masterid
        {
            get { return masterid; }
            set { masterid = value; }
        }
        public string Dodgenum
        {
            get { return dodgenum; }
            set { dodgenum = value; }
        }
        public string Psu
        {
            get { return psu; }
            set
            {
                if (value != null)
                {
                    psu = value;
                }
                else
                {
                    psu = string.Empty;
                }
            }
        }
        public string Place
        {
            get { return place; }
            set
            {
                if (value != null)
                {
                    place = value;
                }
                else
                {
                    place = string.Empty;
                }
            }
        }
        public string Sched
        {
            get { return sched; }
            set
            {
                if (value != null)
                {
                    sched = value;
                }
                else
                {
                    sched = string.Empty;
                }
            }
        }
        public string Contract
        {
            get { return contract; }
            set { contract = value; }
        }
        public string ProjDesc
        {
            get { return projdesc; }
            set { projdesc = value; }
        }
        public string Projloc
        {
            get { return projloc; }
            set { projloc = value; }
        }
        public string Pcityst
        {
            get { return pcityst; }
            set { pcityst = value; }
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
        public string Rvitm5cr
        {
            get { return rvitm5cr; }
            set { rvitm5cr = value; }
        }
        public string Rvitm5c
        {
            get { return rvitm5c; }
            set { rvitm5c = value; }
        }
        public string Flagr5c
        {
            get { return flagr5c; }
            set { flagr5c = value; }
        }
        public string Flagr5c1
        {
            get { return flagr5c1; }
            set { flagr5c1 = value; }
        }
        public string Flag5a
        {
            get { return flag5a; }
            set { flag5a = value; }
        }
        public string Flag5b
        {
            get { return flag5b; }
            set { flag5b = value; }
        }
        public string Flagitm6
        {
            get { return flagitm6; }
            set { flagitm6 = value; }
        }
        public string Flagcap
        {
            get { return flagcap; }
            set { flagcap = value; }
        }
        public string Accescde
        {
            get { return accescde; }
            set { accescde = value; }
        }
        public string Strtdater
        {
            get { return strtdater; }
            set { strtdater = value; }
        }
        public string Strtdate
        {
            get { return strtdate; }
            set { strtdate = value; }
        }
        public string Flagstrtdate
        {
            get { return flagstrtdate; }
            set { flagstrtdate = value; }
        }
        public string Compdater
        {
            get { return compdater; }
            set { compdater = value; }
        }
        public string Compdate
        {
            get { return compdate; }
            set { compdate = value; }
        }
        public string Flagcompdate
        {
            get { return flagcompdate; }
            set { flagcompdate = value; }
        }
        public string Futcompdr
        {
            get { return futcompdr; }
            set { futcompdr = value; }
        }
        public string Futcompd
        {
            get { return futcompd; }
            set { futcompd = value; }
        }
        public string Flagfutcompd
        {
            get { return flagfutcompd; }
            set { flagfutcompd = value; }
        }
        public string Centpwd
        {
            get { return centpwd; }
            set { centpwd = value; }
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

        public bool IsModified
        {
            get { return isModified; }
            set { isModified = value; }
        }

        public string Rstate
        {
            get { return rstate; }
            set { rstate = value; }
        }

        public string Lag
        {
            get { return lag; }
            set { lag = value; }
        }

        public string Timezone
        {
            get { return timezone; }
            set { timezone = value; }
        }
    }

    public class UpdateMast
    {
        private string owner;
        private string newtc;

        public UpdateMast()
        {
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public string Newtc
        {
            get { return newtc; }
            set { newtc = value; }
        }
    }
}