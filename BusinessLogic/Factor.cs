/**************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       CprsBLL.factor.cs	    	

Programmer:         Kevin Montgomery

Creation Date:      03/25/2015

Inputs:             None

Parameters:	    None 

Outputs:	    Factor data	

Description:	    This class creates the getters and setters and stores
                    the data that will be used in the Source Address Display screen

Detailed Design:    None 

Other:	            Called By: frmSource
 
Revision History:	
****************************************************************************************
 Modified Date :  11/25/2015
 Modified By   :  Cestine Gill
 Keyword       :  
 Change Request:  
 Description   :  changed screen to display frame id number (fin)
****************************************************************************************
 Modified Date :  05/24/2016
 Modified By   :  Cestine Gill
 Keyword       :  
 Change Request:  
 Description   :  changed screen use masterid in lieu of dodgenum
****************************************************************************************
 Modified Date :  08/17/2016
 Modified By   :  Srini
 Keyword       :
 Change Request: 
 Description   :  Added Getters and Setters for New Factor9 (Owner2) Fields 
***************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Factor
    {
        private int masterid;
        private string fin;
        private string source;     
        private string seldate;
        private string newtc;
        private int projselv;
        private decimal fwgt;
       
        private string state;
        private string dodgecou;
        private int bldgs; 
        private int units;
        private string id;
        private string owner;
        private string projdesc;
        private string projloc;
        private string pcityst;

        private string f3resporg;
        private string f3respname;
        private string f3addr1;
        private string f3addr2;
        private string f3addr3;
        private string f3zip;
        private string f3phone;
        private string f3email;
        private string f3weburl;

        private string f4resporg;
        private string f4respname;
        private string f4addr1;
        private string f4addr2;
        private string f4addr3;
        private string f4zip;
        private string f4phone;
        private string f4email;
        private string f4weburl;

        private string f5resporg;
        private string f5respname;
        private string f5addr1;
        private string f5addr2;
        private string f5addr3;
        private string f5zip;
        private string f5phone;
        private string f5email;
        private string f5weburl;

        private string f7resporg;
        private string f7respname;
        private string f7addr1;
        private string f7addr2;
        private string f7addr3;
        private string f7zip;
        private string f7phone;
        private string f7email;
        private string f7weburl;

        private string f9resporg;
        private string f9respname;
        private string f9addr1;
        private string f9addr2;
        private string f9addr3;
        private string f9zip;
        private string f9phone;
        private string f9email;
        private string f9weburl;

        public Factor() { }

        public string Fin
        {
            get { return fin; }
            set { fin = value; }
        }

        public int Masterid
        {
            get { return masterid; }
            set { masterid = value; }
        }

        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        public string Seldate
        {
            get { return seldate; }
            set { seldate = value; }
        }

        public string Newtc
        {
            get { return newtc; }
            set { newtc = value; }
        }

        public int Projselv
        {
            get { return projselv; }
            set { projselv = value; }
        }

        public decimal Fwgt
        {
            get { return fwgt; }
            set { fwgt = value; }
        }

        public string State
        {
            get { return state; }
            set { state = value; }
        }

        public string Dodgecou
        {
            get { return dodgecou; }
            set { dodgecou = value; }
        }

        public int Bldgs
        {
            get { return bldgs; }
            set { bldgs = value; }
        }

        public int Units
        {
            get { return units; }
            set { units = value; }
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Owner
        {
            get { return owner; }
            set { owner = value; }
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

        //************* Owner ******************

        public string F3resporg
        {
            get { return f3resporg; }
            set { f3resporg = value; }
        }

        public string F3respname
        {
            get { return f3respname; }
            set { f3respname = value; }
        }

        public string F3addr1
        {
            get { return f3addr1; }
            set { f3addr1 = value; }
        }

        public string F3addr2
        {
            get { return f3addr2; }
            set { f3addr2 = value; }
        }

        public string F3addr3
        {
            get { return f3addr3; }
            set { f3addr3 = value; }
        }

        public string F3zip
        {
            get { return f3zip; }
            set { f3zip = value; }
        }

        public string F3phone
        {
            get { return f3phone; }
            set { f3phone = value; }
        }

        public string F3email
        {
            get { return f3email; }
            set { f3email = value; }
        }

        public string F3weburl
        {
            get { return f3weburl; }
            set { f3weburl = value; }
        }

        //************* Architect ******************

        public string F4resporg
        {
            get { return f4resporg; }
            set { f4resporg = value; }
        }

        public string F4respname
        {
            get { return f4respname; }
            set { f4respname = value; }
        }

        public string F4addr1
        {
            get { return f4addr1; }
            set { f4addr1 = value; }
        }

        public string F4addr2
        {
            get { return f4addr2; }
            set { f4addr2 = value; }
        }

        public string F4addr3
        {
            get { return f4addr3; }
            set { f4addr3 = value; }
        }

        public string F4zip
        {
            get { return f4zip; }
            set { f4zip = value; }
        }

        public string F4phone
        {
            get { return f4phone; }
            set { f4phone = value; }
        }

        public string F4email
        {
            get { return f4email; }
            set { f4email = value; }
        }

        public string F4weburl
        {
            get { return f4weburl; }
            set { f4weburl = value; }
        }

        //************* Enginneer ***************

        public string F5resporg
        {
            get { return f5resporg; }
            set { f5resporg = value; }
        }

        public string F5respname
        {
            get { return f5respname; }
            set { f5respname = value; }
        }

        public string F5addr1
        {
            get { return f5addr1; }
            set { f5addr1 = value; }
        }

        public string F5addr2
        {
            get { return f5addr2; }
            set { f5addr2 = value; }
        }

        public string F5addr3
        {
            get { return f5addr3; }
            set { f5addr3 = value; }
        }

        public string F5zip
        {
            get { return f5zip; }
            set { f5zip = value; }
        }

        public string F5phone
        {
            get { return f5phone; }
            set { f5phone = value; }
        }

        public string F5email
        {
            get { return f5email; }
            set { f5email = value; }
        }

        public string F5weburl
        {
            get { return f5weburl; }
            set { f5weburl = value; }
        }

        //************* Contractor ***************

        public string F7resporg
        {
            get { return f7resporg; }
            set { f7resporg = value; }
        }

        public string F7respname
        {
            get { return f7respname; }
            set { f7respname = value; }
        }

        public string F7addr1
        {
            get { return f7addr1; }
            set { f7addr1 = value; }
        }

        public string F7addr2
        {
            get { return f7addr2; }
            set { f7addr2 = value; }
        }

        public string F7addr3
        {
            get { return f7addr3; }
            set { f7addr3 = value; }
        }

        public string F7zip
        {
            get { return f7zip; }
            set { f7zip = value; }
        }

        public string F7phone
        {
            get { return f7phone; }
            set { f7phone = value; }
        }

        public string F7email
        {
            get { return f7email; }
            set { f7email = value; }
        }

        public string F7weburl
        {
            get { return f7weburl; }
            set { f7weburl = value; }
        }

        //************* Owner 2 ***************

        public string F9resporg
        {
            get { return f9resporg; }
            set { f9resporg = value; }
        }

        public string F9respname
        {
            get { return f9respname; }
            set { f9respname = value; }
        }

        public string F9addr1
        {
            get { return f9addr1; }
            set { f9addr1 = value; }
        }

        public string F9addr2
        {
            get { return f9addr2; }
            set { f9addr2 = value; }
        }

        public string F9addr3
        {
            get { return f9addr3; }
            set { f9addr3 = value; }
        }

        public string F9zip
        {
            get { return f9zip; }
            set { f9zip = value; }
        }

        public string F9phone
        {
            get { return f9phone; }
            set { f9phone = value; }
        }

        public string F9email
        {
            get { return f9email; }
            set { f9email = value; }
        }

        public string F9weburl
        {
            get { return f9weburl; }
            set { f9weburl = value; }
        }

    }
}
