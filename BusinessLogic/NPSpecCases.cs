/*
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : CprsBLL.NPSpecCases.cs
 Programmer    : Diane Musachio
 Creation Date : 11/09/2017
 Inputs        : n/a
 Parameters    : fin
 Output        : n/a
 Description   : a structure/class to store data from NP Special Cases table
 Detail Design : NP Special Cases design document
 Other         : Called by CprsDAL.NPSpecCasesData.cs
                           frmNPNonResSpecialCases.cs
 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using CprsBLL;

namespace CprsBLL
{
    public class NPSpecCases
    {
        private string fin;
        private string psu;
        private string bpoid;
        private string sched;
        private string seldate;
        private string owner;
        private string fipstate;
        private string newtc;
        private string source;
        private int projselv;
        private float fwgt;
        private string projdesc;
        private string projloc;
        private string pcityst;
        private string resporg;
        private string factoff;
        private string othrresp;
        private string addr1;
        private string addr2;
        private string addr3;
        private string zip;
        private string respname;
        private string phone;
        private string ext;
        private string fax;

        public NPSpecCases(string fin)
        {
            finid = fin;
        }

        private string finid;
        public string Finid
        {
            get { return finid; }
        }

        public string Fin
        {
            get { return fin; }
            set { fin = value; }
        }

        public string Psu
        {
            get { return psu; }
            set { psu = value; }
        }

        public string Bpoid
        {
            get { return bpoid; }
            set { bpoid = value; }
        }

        public string Sched
        {
            get { return sched; }
            set { sched = value; }
        }

        public string Seldate
        {
            get { return seldate; }
            set { seldate = value; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
        }

        public string Fipstate
        {
            get { return fipstate; }
            set { fipstate = value; }
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

        public int Projselv
        {
            get { return projselv; }
            set { projselv = value; }
        }

        public float Fwgt
        {
            get { return fwgt; }
            set { fwgt = value; }
        }

        public string Projdesc
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

        public string Resporg
        {
            get { return resporg; }
            set { resporg = value; }
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

        public string Respname
        {
            get { return respname; }
            set { respname = value; }
        }
        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public string Ext
        {
            get { return ext; }
            set { ext = value; }
        }

        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }   
    }
}
