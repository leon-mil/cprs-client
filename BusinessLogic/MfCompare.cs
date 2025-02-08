/**************************************************************************************
Econ App Name:     CPRS
 
Project Name:      CPRS Interactive Screens System

Program Name:      MFCompare.cs

Programmer:        Diane Musachio

Creation Date:     May 19, 2016

Inputs:            n/a
 
Parameters:        n/a
 
Output:            n/a
 
Description:       a structure/class to store data from View

Detail Design:     MultiFamily Initial Address
 
Other:             Called by: frmMFCompare.cs

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
    public class MfCompare
    {
        private int masterid;
        private string fin;
        private string frcde;
        private string respid;
        private string psu;
        private string bpoid;
        private string sched;
        private string seldate;
        private string status;
        private string fipstate;
        private string newtc;
        private string bldgs;
        private string units;
        private string rbldgs;
        private string runits;
        private string projdesc;
        private string projloc;
        private string pcityst;
        private string factoff;
        private string resporg;
        private string othrresp;
        private string addr1;
        private string addr2;
        private string addr3;
        private string zip;
        private string respname;
        private string respname2;
        private string phone;
        private string phone2;
        private string ext;
        private string ext2;    
        private string strtdate;
        private string id;
        
        public MfCompare() { }

        public string Fin
        {
            get { return fin; }
            set { fin = psu + " " + bpoid + " " + sched; }
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
       
        public string Strtdate
        {
            get { return strtdate; }
            set { strtdate = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }
    }
}
