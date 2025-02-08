/*
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : DodgeNotes.cs

 Programmer    : Diane Musachio

 Creation Date : 4/2/2015

 Inputs        : n/a

 Paramaters    : n/a 
 
 Output        : n/a
 
 Description   : a structure/class to store data from dcpnotes data table

 Detail Design : Detailed User Requirements for Slip Display Screen

 Forms using this code: frmSlipDisplay
 
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
        public static class DodgeNotes
        {

            /* class to return notes from dcpnotes data table where type = "ADD" */
            public class Add
            {
                private string addnotes;

                public Add() { }

                public string Addnotes
                {
                    get { return addnotes; }
                    set { addnotes = value; }
                }

            }

            /* class to return notes from dcpnotes data table where type = "ITM" */
            public class Item
            {
                private string itemnotes;

                public Item() { }

                public string Itemnotes
                {
                    get { return itemnotes; }
                    set { itemnotes = value; }
                }
            }

            /* class to return notes from dcpnotes data table where type = "TLE" */
            public class Title
            {
                private string titlenotes;

                public Title() { }

                public string Titlenotes
                {
                    get { return titlenotes; }
                    set { titlenotes = value; }
                }
            }

            /* class to return notes from dcpnotes data table where type = "VAL" */
             public class Value
            {
                private string valuenotes;

                public Value() { }

                public string Valuenotes
                   {
                        get { return valuenotes; }
                        set { valuenotes = value; }
                    }
            }

             /* class to return notes from dcpnotes data table where type = "REP" */
             public class Reporter
             {
                 private string reporternotes;

                 public Reporter() { }

                 public string Reporternotes
                 {
                     get { return reporternotes; }
                     set { reporternotes = value; }
                 }
             }

        }   
        
    }
