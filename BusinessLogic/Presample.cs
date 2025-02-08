/**************************************************************************************
Econ App Name:     CPRS
 
Project Name:      CPRS Interactive Screens System

Program Name:      CprsBll.Presample.cs

Programmer:        Cestine Gill

Creation Date:     04/07/2016

Inputs:            n/a
 
Parameters:        n/a
 
Output:            n/a
 
Description:       a structure/class to store data from View

Detail Design:     Multi Family Initial Address Detailed Design
 
Other:             Called by CprsDAL.MfInitial.cs and used by frmMfInitial.cs

Revisions:         See Below
****************************************************************************************
Modified Date: 
Modified By: 
Keyword: 
Change Request: 
Description: 
***************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Presample
    {
        private string id;
        private int masterid;
        private string fin;
        private string respid;
        private string psu;
        private string place;
        private string bpoid;
        private string sched;
        private string seldate;
        private string status;
        private string fipstate;
        private string fipstater;
        private string frcde;
        private string newtc;

        private string source;
        private string bldgs;
        private string units;
        private string rbldgs;
        private string runits;
       
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
        private string strtdate;
        private string resplock;
        private string commtext;
        private string commdate;
        private string commtime;
        private string usrnme;
        private string worked;
        private string rev1nme;
        private string rev2nme;
        private int dupmaster;
        private string dupflag;

        private string oresporg;
        private string orespname;
        private string orespname2;
        private string orespnote;
        private string ofactoff;
        private string oothrresp;
        private string oaddr1;
        private string oaddr2;
        private string oaddr3;
        private string ozip;
        private string ophone;
        private string ophone2;
        private string oext;
        private string oext2;
        private string ofax;
        private string oemail;
        private string oweburl;

        public Presample() { }

        public string Fin
        {
            get { return fin; }
            set { fin = psu + " " + bpoid + " " + sched; }
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

        public string Seldate
        {
            get { return seldate; }
            set { seldate = value; }
        }

        public string Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Fipstate
        {
            get { return fipstate; }
            set { fipstate = value; }
        }

        public string Fipstater
        {
            get { return fipstater; }
            set { fipstater = value; }
        }

        public string Frcde
        {
            get { return frcde; }
            set { frcde = value; }
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
        public string Bldgs
        {
            get { return bldgs; }
            set { bldgs = value; }
        }
        public string Units
        {
            get { return units; }
            set { units = value; }
        }
        public int Masterid
        {
            get { return masterid; }
            set { masterid = value; }
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
        public string Bpoid
        {
            get { return bpoid; }
            set
            {
                if (value != null)
                {
                    bpoid = value;
                }
                else
                {
                    bpoid = string.Empty;
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
        public string PCitySt
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
        public string Strtdate
        {
            get { return strtdate; }
            set { strtdate = value; }
        }
        public string RespLock
        {
            get { return resplock; }
            set { resplock = value; }
        }
        public string Commtext
        {
            get { return commtext; }
            set { commtext = value; }
        }
        public string Commdate
        {
            get { return commdate; }
            set { commdate = value; }
        }
        public string Commtime
        {
            get { return commtime; }
            set { commtime = value; }
        }
        public string Usrnme
        {
            get { return usrnme; }
            set { usrnme = value; }
        }
        public string Worked
        {
            get { return worked; }
            set { worked = value; }
        }
        public string Rev1nme
        {
            get { return rev1nme; }
            set { rev1nme = value; }
        }
        public string Rev2nme
        {
            get { return rev2nme; }
            set { rev2nme = value; }
        }
        public int Dupmaster
        {
            get { return dupmaster; }
            set { dupmaster = value; }
        }
        public string Dupflag
        {
            get { return dupflag; }
            set { dupflag = value; }
        }

        public string OResporg
        {
            get { return oresporg; }
            set { oresporg = value; }
        }
        public string ORespname
        {
            get { return orespname; }
            set { orespname = value; }
        }
        public string ORespname2
        {
            get { return orespname2; }
            set { orespname2 = value; }
        }
        public string ORespnote
        {
            get { return orespnote; }
            set { orespnote = value; }
        }
        public string OFactoff
        {
            get { return ofactoff; }
            set { ofactoff = value; }
        }
        public string OOthrresp
        {
            get { return oothrresp; }
            set { oothrresp = value; }
        }
        public string OAddr1
        {
            get { return oaddr1; }
            set { oaddr1 = value; }
        }
        public string OAddr2
        {
            get { return oaddr2; }
            set { oaddr2 = value; }
        }
        public string OAddr3
        {
            get { return oaddr3; }
            set { oaddr3 = value; }
        }
        public string OZip
        {
            get { return ozip; }
            set { ozip = value; }
        }
        public string OPhone
        {
            get { return ophone; }
            set { ophone = value; }
        }
        public string OPhone2
        {
            get { return ophone2; }
            set { ophone2 = value; }
        }
        public string OExt
        {
            get { return oext; }
            set { oext = value; }
        }
        public string OExt2
        {
            get { return oext2; }
            set { oext2 = value; }
        }
        public string OFax
        {
            get { return ofax; }
            set { ofax = value; }
        }
        public string OEmail
        {
            get { return oemail; }
            set { oemail = value; }
        }
        public string OWeburl
        {
            get { return oweburl; }
            set { oweburl = value; }
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

}
