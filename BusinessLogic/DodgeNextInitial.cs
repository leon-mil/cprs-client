/****************************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : DodgeNextInitial.cs

 Programmer    : Srini Natarajan
 Creation Date : 03/10/2017

 Inputs        : n/a
 Parameters    : n/a
 Output        : n/a
 
 Description   : a structure/class to store data from DODGE INITIAL DISPLAY View
 Detail Design : Detailed User Requirements for Dodge Initial Address Display Screen
 
 Other         : Called by CprsDAL.DodgeInitialData.cs and used by frmName

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
    class DodgeNextInitial
    {
        private string id;
        private string rev1nme;
        private string rev2nme;
        private string worked;
        private string hqworked;
        private string oresporg;
        private string ofactoff;
        private string oothrresp;
        private string oaddr1;
        private string oaddr2;
        private string oaddr3;
        private string ozip;
        private string orespname;
        private string orespname2;
        private string orespnote;
        private string ophone;
        private string ophone2;
        private string oext;
        private string oext2;
        private string ofax;
        private string oemail;
        private string oweburl;
        private string ocontract;
           
        public DodgeNextInitial() { }

        public string Id
        {
            get { return id; }
            set { id = value; }
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
        public string Worked
        {
            get { return worked; }
            set { worked = value; }
        }
        public string HQWorked
        {
            get { return hqworked; }
            set { hqworked = value; }
        }
        public string OResporg
        {
            get { return oresporg; }
            set { oresporg = value; }
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
        public string OContract
        {
            get { return ocontract; }
            set { ocontract = value; }
        }
    }
}
