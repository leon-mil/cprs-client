/*
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : Slips.cs

 Programmer    : Diane Musachio

 Creation Date : 4/2/2015

 Inputs        : n/a
 
 Parameters    : n/a
 
 Output        : n/a
 
 Description   : a structure/class to store data from dcpslips data table

 Detail Design : Detailed User Requirements for Slip Display Screen
 
 Forms using this code:  frmSlipDisplay
 
 Called by     : CprsDAL.DodgeSlipData.cs

 Other         : 

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
    public class Slips
    {

        // Data from DCPSLIPS table in SQL Database  

        private int masterid;
        private string id;     
        private string title;
        private string valuation;

        private string taddr1;
        private string taddr2;
        private string tcity;
        private string tcounty;
        private string tstate;
        private string tzip;
        
        private string tstrtdate;
        private string tcompdate;
        private string worktype;
        private string contmeth;
        private string contnbr1;
        private string contnbr2;

        private string notice;
       
        private string fcode;
        private string ownclass;
        private string storyabv;
        private string storybel;
        private string tsqrarea;
        private string numbldgs;
        private string subcont;
 
        private string projstat;
        private string projgroup;
        private string pptype;

        private string ptype1;
        private string ptype2;
        private string ptype3;
        private string ptype4;
        private string ptype5;
        private string ptype6;
        private string ptype7;
        private string ptype8;
        private string ptype9;
        private string ptype10;
        private string ptype11;
        private string ptype12;
        private string ptype13;
        private string ptype14;
        private string ptype15;
        private string ptype16;
        private string ptype17;
        private string ptype18;
        private string ptype19;
        private string ptype20;

        public Slips() { }

        /* The following code retrieves data from dcslips data table and returns the value of the appropriate variable */
        public int Masterid
        {
            get { return masterid; }
            set { masterid = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Valuation
        {
            get { return valuation; }
            set { valuation = value; }
        }

        public string Taddr1
        {
            get { return taddr1; }
            set { taddr1 = value; }
        }

        public string Taddr2
        {
            get { return taddr2; }
            set { taddr2 = value; }
        }

        public string Tcity
        {
            get { return tcity; }
            set { tcity = value; }
        }

        public string Tcounty
        {
            get { return tcounty; }
            set { tcounty = value; }
        }

        public string Tstate
        {
            get { return tstate; }
            set { tstate = value; }
        }

        public string Tzip
        {
            get { return tzip; }
            set { tzip = value; }
        }

        public string Tstrtdate
        {
            get { return tstrtdate; }
            set { tstrtdate = value; }
        }

        public string Tcompdate
        {
            get { return tcompdate; }
            set { tcompdate = value; }
        }

        public string Worktype
        {
            get { return worktype; }
            set { worktype = value; }
        }

        public string Contmeth
        {
            get { return contmeth; }
            set { contmeth = value; }
        }

        public string Contnbr1
        {
            get { return contnbr1; }
            set { contnbr1 = value; }
        }

        public string Contnbr2
        {
            get { return contnbr2; }
            set { contnbr2 = value; }
        }

        public string Notice
        {
            get { return notice; }
            set { notice = value; }
        }

        public string Fcode
        {
            get { return fcode; }
            set { fcode = value; }
        }

        public string Ownclass
        {
            get { return ownclass; }
            set { ownclass = value; }
        }

        public string Storyabv
        {
            get { return storyabv; }
            set { storyabv = value; }
        }

        public string Storybel
        {
            get { return storybel; }
            set { storybel = value; }
        }

        public string Tsqrarea
        {
            get { return tsqrarea; }
            set { tsqrarea = value; }
        }

        public string Numbldgs
        {
            get { return numbldgs; }
            set { numbldgs = value; }
        }
        public string Subcont
        {
            get { return subcont; }
            set { subcont = value; }
        }

        public string Projstat
        {
            get { return projstat; }
            set { projstat = value; }
        }

        public string Projgroup
        {
            get { return projgroup; }
            set { projgroup = value; }
        }

        public string Pptype
        {
            get { return pptype; }
            set { pptype = value; }
        }

        /* Project Type Codes */

        public string Ptype1
        {
            get { return ptype1; }
            set { ptype1 = value; }
        }

        public string Ptype2
        {
            get { return ptype2; }
            set { ptype2 = value; }
        }

        public string Ptype3
        {
            get { return ptype3; }
            set { ptype3 = value; }
        }

        public string Ptype4
        {
            get { return ptype4; }
            set { ptype4 = value; }
        }

        public string Ptype5
        {
            get { return ptype5; }
            set { ptype5 = value; }
        }

        public string Ptype6
        {
            get { return ptype6; }
            set { ptype6 = value; }
        }

        public string Ptype7
        {
            get { return ptype7; }
            set { ptype7 = value; }
        }

        public string Ptype8
        {
            get { return ptype8; }
            set { ptype8 = value; }
        }

        public string Ptype9
        {
            get { return ptype9; }
            set { ptype9 = value; }
        }

        public string Ptype10
        {
            get { return ptype10; }
            set { ptype10 = value; }
        }

        public string Ptype11
        {
            get { return ptype11; }
            set { ptype11 = value; }
        }

        public string Ptype12
        {
            get { return ptype12; }
            set { ptype12 = value; }
        }

        public string Ptype13
        {
            get { return ptype13; }
            set { ptype13 = value; }
        }

        public string Ptype14
        {
            get { return ptype14; }
            set { ptype14 = value; }
        }

        public string Ptype15
        {
            get { return ptype15; }
            set { ptype15 = value; }
        }

        public string Ptype16
        {
            get { return ptype16; }
            set { ptype16 = value; }
        }

        public string Ptype17
        {
            get { return ptype17; }
            set { ptype17 = value; }
        }

        public string Ptype18
        {
            get { return ptype18; }
            set { ptype18 = value; }
        }
        public string Ptype19
        {
            get { return ptype19; }
            set { ptype19 = value; }
        }
        public string Ptype20
        {
            get { return ptype20; }
            set { ptype20 = value; }
        }



    }
}
