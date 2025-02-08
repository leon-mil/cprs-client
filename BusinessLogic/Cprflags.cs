
/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Cprflags.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/30/2015
Inputs:             None
Parameters:	        flag string and owner
Outputs:	        cprflag string, display list
Description:	    This class creates the getters and setters and stores
                    the flag data that will be used in the c700 screen
                    
Detailed Design:    C700 Edit 
Other:	            Called By: frmc700
 
Revision History:	
*********************************************************************
 Modified Date :  3/26/2019
 Modified By   :  Christine
 Keyword       :  
 Change Request:  3025
 Description   :  for flag 29, status(2,8) change to status(3,7)
********************************************************************
 Modified Date :  10/6/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#7650
 Description   :update description for flag5, flag6, remove "or Blank"
***********************************************************************
 Modified Date :  12/7/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#????
 Description   : update GetMainFlags() to get new adoj value
***********************************************************************
Modified Date :  01/12/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#7867
 Description   : When check flag 22 add condition flagr5c = 'R'
***********************************************************************
*Modified Date :  09/14/2023
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#1212
 Description   : update criteria for setting up cpr flag23, flag24 
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace CprsBLL
{
    public class Cprflags
    {
        /*Construction */
        public Cprflags(Dataflags passed_flags, string passed_owner, string calling_from)
        {
            mainflag = passed_flags.currflags;
            reportflag = passed_flags.reportflags;
            owner = passed_owner;
            callfrom = calling_from;
  
            BuildFlagList(mainflag, reportflag);
        }

        public List<Cprflag> displayFlaglist = new List<Cprflag>();

        /*private properties */
        private string mainflag = String.Empty;
        private string reportflag = String.Empty;
        private string owner = String.Empty;
        private string process_date = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
        private List<Cprflag> cprflaglist = new List<Cprflag>();
        private List<Cprflag> reportflaglist = new List<Cprflag>();
        private string callfrom;


        //Build flag list
        public void BuildFlagList(string flag_str, string rflag_str)
        {
            if (flag_str == "")
            {
                throw new System.ArgumentException("Flag string cannot be empty");
            }

            //initial list
            displayFlaglist.Clear();
            cprflaglist.Clear();
            reportflaglist.Clear();

            // Use ToCharArray to convert string to array.
            char[] array = flag_str.ToCharArray();

            // Loop through array.
            for (int i = 0; i < array.Length; i++)
            {
                Cprflag cf = new Cprflag();

                cf.flagno = i;

                // Get character from array.
                if (array[i]=='0')
                    cf.value = '0';
                else
                    cf.value = '1';
                cf.bypass = (array[i] == '2') ? true : false;

                //get title
                if (i == 0)
                {
                    cf.title = "FLAG: Status Code Change";
                }
                else if (i == 1)
                {
                    cf.title = "FLAG: Ownership Code Change";
                }
                else if (i == 2)
                {
                    cf.title = "REJECT: ITEM5A + ITEM5B =/= RVITM5C";
                }
                else if (i == 3)
                {
                    cf.title = "REJECT: REPORT FOLLOWS IMPUTE (4)";
                }
                else if (i == 4)
                {
                    cf.title = "REJECT: VIP of 0 in the start month";
                }
                else if (i == 5)
                {
                    cf.title = "REJECT: VIP of 0 in the completion month";
                }
                else if (i == 6)
                {
                    cf.title = "REJECT: Unmatched Projected Dates";
                }
                else if (i == 7)
                {
                    cf.title = "REJECT: RVITM5C reported 0";
                }
                else if (i == 8)
                {
                    cf.title = "REJECT: Project started prior to 24 months ago, cannot enter data";
                }
                else if (i == 9)
                {
                    cf.title = "REJECT: Negative VIP";
                }
                else if (i == 10)
                {
                    cf.title = "REJECT: Completion date with no RVITM5C";
                }
                else if (i == 11)
                {
                    cf.title = "REJECT: Item5b without Item5a";
                }
                else if (i == 12)
                {
                    cf.title = "FLAG: Early Start Date";
                }
                else if (i == 13)
                {
                    cf.title = "FLAG: RVITM5C >= 3 TIMES SELVAL";
                }
                else if (i == 14)
                {
                    cf.title = "FLAG: Selection value >= 3 X RVITM5C";
                }
                else if (i == 15)
                {
                    cf.title = "FLAG: Cumulative VIP > 140% OF RVITM5C";
                }                
                else if (i == 16)
                {
                    cf.title = "FLAG: VIP 25% OF RVITM5C";  
                }
                else if (i == 17)
                {
                    cf.title = "FLAG: VIP 50% OF RVITM5C";
                }
                else if (i == 18)
                {
                   cf.title = "FLAG: VIP 75% OF RVITM5C";
                }
                else if (i == 19)
                {
                    cf.title = "FLAG: OUTLIER PROJECT";
                }
                else if (i == 20)
                {
                    cf.title = "FLAG: Item6 >= 50% of RVITM5C";
                }
                else if (i == 21)
                {
                    cf.title = "FLAG: Manufacturing project with no capital expenditures";
                }
                else if (i == 22)
                {
                    cf.title = "FLAG: RVITM5C + ITEM6 = CAPEXP";
                }
                else if (i == 23)
                {
                    cf.title = "FLAG: CAPEXP > RVITM5C";
                }
                else if (i == 24)
                {
                    cf.title = "FLAG: Imputed VIP over RVITM5C";
                }
                else if (i == 25)
                {
                    cf.title = "FLAG: VIP of 0 in the last 3 months";
                }
                else if (i == 26)
                {
                    cf.title = "FLAG: Completed before selected";
                }
                else if (i == 27)
                {
                    cf.title = "FLAG: Cumulative VIP < 95% OF RVITM5C";
                }
                else if (i == 28)
                {
                    cf.title = "FLAG: No start date, projected completion date or RVITM5C";
                }
                else if (i == 29)
                {
                    cf.title = "FLAG: RVITM5C is 900% of previous reported value";
                }
                else if (i == 30)
                {
                    cf.title = "FLAG: Item6 is 900% of previous reported value";
                }
                else if (i == 31)
                {
                    cf.title = "FLAG: Capex is 900% of previous reported value";
                }
                else if (i == 32)
                {
                    cf.title = "FLAG: VIP is 900% of previous reported value";
                }
                else if (i == 33)
                {
                    cf.title = "FLAG: CUMVIP with no RVITM5C";
                }
                else if (i == 34)
                {
                    cf.title = "FLAG: Projected Completion date does not align with percent complete";
                }
                else if (i == 35)
                {
                    // No flag 36 for now
                }
                else if (i == 36)
                {
                    cf.title = "FLAG: Ownership Change";
                }
                else if (i == 37)
                {
                    cf.title = "REJECT: Reported VIP with no start date";
                }
                else if (i ==38)
                {
                    cf.title = "FLAG: Centurion Comments Field Contains Data";
                }
                else if (i == 39)
                {
                   cf.title = "FLAG: COST PER UNIT OUT OF RANGE";
                }
                else if (i == 40)
                {
                   cf.title = "FLAG: Cumulative VIP > RVITM5C";
                }
                else if (i == 41)
                {
                    cf.title = "FLAG: VIP 20% of RVITM5C";
                }
                else if (i == 42)
                {
                    cf.title = "FLAG: VIP 50% OF RVITM5C";
                }
                else if (i == 43)
                {
                    cf.title = "FLAG: VIP 75% OF RVITM5C";
                }
                else if (i == 44)
                {
                    cf.title = "FLAG: Centurion Contact Data Does Not Match Current";
                }
                else if (i == 45)
                {
                    cf.title = "FLAG: PREVIOUSLY REPORTED VIP BLANKED OUT IN CENTURION";
                }
                else if (i == 46)
                {
                    cf.title = "FLAG: Analyst Data Updated by Reported";
                }
                else if (i == 47)
                {
                    cf.title = "FLAG: VIP = 999% of RVITM5C";
                }


                //total flags
                cprflaglist.Add(cf);

                //display flags
                if (callfrom == "edit")
                {
                    if (cf.value == '1' && !cf.bypass)
                        displayFlaglist.Add(cf);
                }
            }

            // Use ToCharArray to convert string to array.
            array = rflag_str.ToCharArray();

            // Loop through array.
            for (int i = 0; i < array.Length; i++)
            {
                Cprflag rf = new Cprflag();
                rf.flagno = i;

                // Get character from array.
                if (array[i] == '0')
                    rf.value = '0';
                else
                    rf.value = '1';

                rf.title = "";

                //total flags
                reportflaglist.Add(rf);

                //display flags
                if (callfrom != "edit")
                {
                    if (rf.value == '1')
                        displayFlaglist.Add(rf);
                }
            }    

        }

        //Get main flag strings
        public Dataflags GetMainFlags(Sample samp, Master mast, Soc soc, MonthlyVips mvs, List<Cpraudit> cprauditlist, List<Vipaudit> Vipauditlist, bool status_change, int owner_change, float sampwt, string calling_from, ref float new_fwgt, ref float new_oadj, ref bool reject, ref bool report_reject)
        {
            Dataflags dataflag = new Dataflags();

            //save 39, 45 flags, may not need it and will delete 
            //string f39 = cprflaglist.ElementAt(39).value.ToString();
            //string f45 = cprflaglist.ElementAt(45).value.ToString();
              
            if (soc == null)
            {
                //////if (samp.IsModified || mast.IsModified || mvs.IsModified)
                //////{
                    Setupflags(samp, mast, soc, mvs, cprauditlist, Vipauditlist, status_change, owner_change, sampwt, ref new_fwgt, ref new_oadj);
                    SetupReportflags(samp, mast, soc, mvs, cprauditlist, Vipauditlist, status_change, owner_change);
                //////}

            }
            else
            {
                ////////if (samp.IsModified || mast.IsModified || soc.IsModified || mvs.IsModified)
                ////////{
                    Setupflags(samp, mast, soc, mvs, cprauditlist, Vipauditlist, status_change, owner_change, sampwt, ref new_fwgt, ref new_oadj);
                    SetupReportflags(samp, mast, soc, mvs, cprauditlist, Vipauditlist, status_change, owner_change);
                ////}
            }

            //Reset bypass
            if (calling_from == "edit")
            {
                foreach (var value in cprauditlist)
                {
                    if (value.Varnme == "RVITM5C")
                    {
                        if (cprflaglist.ElementAt(13).bypass)
                            cprflaglist.ElementAt(13).bypass = false;
                        if (cprflaglist.ElementAt(14).bypass)
                            cprflaglist.ElementAt(14).bypass = false;
                        if (cprflaglist.ElementAt(15).bypass)
                            cprflaglist.ElementAt(15).bypass = false;
                        if (cprflaglist.ElementAt(16).bypass)
                            cprflaglist.ElementAt(16).bypass = false;
                        if (cprflaglist.ElementAt(17).bypass)
                            cprflaglist.ElementAt(17).bypass = false;
                        if (cprflaglist.ElementAt(18).bypass)
                            cprflaglist.ElementAt(18).bypass = false;
                        if (cprflaglist.ElementAt(19).bypass)
                            cprflaglist.ElementAt(19).bypass = false;
                        if (cprflaglist.ElementAt(22).bypass)
                            cprflaglist.ElementAt(22).bypass = false;
                        if (cprflaglist.ElementAt(23).bypass)
                            cprflaglist.ElementAt(23).bypass = false;
                        if (cprflaglist.ElementAt(27).bypass)
                            cprflaglist.ElementAt(27).bypass = false;
                        if (cprflaglist.ElementAt(28).bypass)
                            cprflaglist.ElementAt(28).bypass = false;
                        if (cprflaglist.ElementAt(29).bypass)
                            cprflaglist.ElementAt(29).bypass = false;
                        if (cprflaglist.ElementAt(40).bypass)
                            cprflaglist.ElementAt(40).bypass = false;
                        if (cprflaglist.ElementAt(41).bypass)
                            cprflaglist.ElementAt(41).bypass = false;
                        if (cprflaglist.ElementAt(42).bypass)
                            cprflaglist.ElementAt(42).bypass = false;
                        if (cprflaglist.ElementAt(43).bypass)
                            cprflaglist.ElementAt(43).bypass = false;

                    }
                    if (value.Varnme == "ITEM6")
                    {
                        if (cprflaglist.ElementAt(30).bypass)
                            cprflaglist.ElementAt(30).bypass = false;
                        if (cprflaglist.ElementAt(20).bypass)
                            cprflaglist.ElementAt(20).bypass = false;
                        if (cprflaglist.ElementAt(22).bypass)
                            cprflaglist.ElementAt(22).bypass = false;
                    }
                    if (value.Varnme == "STRTDATE")
                    {
                        if (cprflaglist.ElementAt(12).bypass)
                            cprflaglist.ElementAt(12).bypass = false;
                        if (cprflaglist.ElementAt(28).bypass)
                            cprflaglist.ElementAt(28).bypass = false;
                    }
                    if (value.Varnme == "COMPDATE")
                    {
                        if (cprflaglist.ElementAt(26).bypass)
                            cprflaglist.ElementAt(26).bypass = false;
                        
                    }
                    if (value.Varnme == "FUTCOMPD")
                    {
                        if (cprflaglist.ElementAt(34).bypass)
                            cprflaglist.ElementAt(34).bypass = false;
                        if (cprflaglist.ElementAt(28).bypass)
                            cprflaglist.ElementAt(28).bypass = false;
                    }
                    if (value.Varnme == "CAPEXP")
                    {
                        if (cprflaglist.ElementAt(21).bypass)
                            cprflaglist.ElementAt(21).bypass = false;
                        if (cprflaglist.ElementAt(22).bypass)
                            cprflaglist.ElementAt(22).bypass = false;
                    }
                }

                //Check bypass 
                foreach (Cprflag f in displayFlaglist)
                {
                    if (f.bypass == true)
                    {
                        if (cprflaglist.ElementAt(f.flagno).value == '1')
                            cprflaglist.ElementAt(f.flagno).bypass = true;
                    }
                    else
                    {
                        if (cprflaglist.ElementAt(f.flagno).value == '1')
                            cprflaglist.ElementAt(f.flagno).bypass = false;
                    }
                }
            }

            string fflag = "";
            foreach (Cprflag f in cprflaglist)
            {
                if (f.value == '1' && f.bypass)
                    f.value = '2';
                fflag = fflag + f.value;
            }

            string rflag = "";
            foreach (Cprflag f in reportflaglist)
            {
                if (callfrom == "edit")
                {
                    // if flag 39, 45 is bypass, set report flag to 0 in c700 screen
                    if (f.flagno == 38 || f.flagno == 44 || f.flagno == 45 )
                    {
                        if (cprflaglist[f.flagno].bypass)
                            f.value = '0';
                    }
                    //check reject flags in cprflaglist, if it is 0, set report flag to 0
                    else if (f.value == '1' && (f.flagno == 2 || f.flagno == 3 || f.flagno == 4 || f.flagno == 5 || f.flagno == 6 || f.flagno == 7 || f.flagno == 8 || f.flagno == 9 || f.flagno == 10 || f.flagno == 11 || f.flagno == 37))
                    {
                        if (cprflaglist[f.flagno].value == '0')
                            f.value = '0';
                    }
                }
                rflag = rflag + f.value;
            }

            //Get reject info
            IEnumerable<Cprflag> rejectflags =
            from j in cprflaglist
            where j.value == '1' && (j.flagno == 2 || j.flagno == 3 || j.flagno == 4 || j.flagno == 5 || j.flagno == 6 || j.flagno == 7 || j.flagno == 8 || j.flagno == 9 || j.flagno == 10 || j.flagno == 11 || j.flagno ==37)
            select j;
            if (rejectflags.ToList().Count() > 0)
                reject = true;
            else
                reject = false;

            //Get report reject info
            IEnumerable<Cprflag> rrejectflags =
            from j in reportflaglist
            where j.value == '1' && (j.flagno == 2 || j.flagno == 3 || j.flagno == 4 || j.flagno == 5 || j.flagno == 6 || j.flagno == 7 || j.flagno == 8 || j.flagno == 10 || j.flagno == 11)
            select j;
            if (rrejectflags.ToList().Count() > 0)
                report_reject = true;
            else
                report_reject = false;
            
            //set flags
            dataflag.currflags = fflag;
            dataflag.reportflags = rflag;

            return dataflag;
        }

        //Set up data flags
        private void Setupflags(Sample samp, Master mast, Soc soc, MonthlyVips mvs, List<Cpraudit> Cprauditlist, List<Vipaudit> Vipauditlist, bool status_change, int owner_change, float sampwt, ref float new_fwgt, ref float new_oadj)
        {
            DateTime d1;
            DateTime d2;
            int month_diff = 0;
            bool set_flag = false;

            //1 status change
            if (status_change)
                Setflag(0);
            else
                Setnoflag(0);

            //2 owner change
            if (owner_change == 2)
                Setflag(1);
            else
                Setnoflag(1);

            //3 Item5a + item5b <> rvitm5c
            if (((samp.Flag5a != "M" && samp.Flag5a != "B") || (samp.Flag5b != "M" && samp.Flag5b != "B")) && (samp.Flagr5c != "M" && samp.Flagr5c != "B"))
            {
                if (samp.Item5a + samp.Item5b != samp.Rvitm5c)
                    Setflag(2);
                else
                    Setnoflag(2);
            }
            else
                Setnoflag(2);

            //4 Report follows impute
            set_flag = false;
            if (samp.Strtdate != "" && (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8"))
            {
                if (Convert.ToInt32(samp.Strtdate) < Convert.ToInt32(process_date) && mvs.GetSumMons() >0)
                {
                    if (samp.Compdate != "")
                    {
                        List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdate, samp.Compdate);
                        MonthlyVip mv = null;
                        foreach (var value in mvlist)
                        {
                            if (mv != null)
                            {
                                if ((mv.Vipflag != "M") && value.Vipflag == "M")
                                {
                                    set_flag = true;
                                    break;
                                }
                            }
                            mv = value;
                        }
                        if (!set_flag)
                        {
                            foreach (var value in mvlist)
                            {
                                if (value.Vipdata == 0 && value.Vipflag == "B")
                                {
                                    set_flag = true;
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdate, DateTime.Now.AddMonths(-1).ToString("yyyyMM"));
                        
                        MonthlyVip mv = null;
                        foreach (var value in mvlist)
                        {
                            if (mv != null)
                            {
                                if (mv.Vipflag != "M" && mv.Vipflag != "B" && (value.Vipflag == "M" || value.Vipflag == "B"))
                                {
                                    set_flag = true;
                                    break;
                                }
                            }
                            mv = value;
                        }
                        //if (!set_flag)
                        //{
                        //    foreach (var value in mvlist)
                        //    {
                        //        if (value.Date6 != process_date)
                        //        {
                        //            if (value.Vipdata == 0 && value.Vipflag == "B")
                        //            {
                        //                set_flag = true;
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}
                    }
                }
            }
            if (set_flag)
                Setflag(3);
            else
                Setnoflag(3);

            //5 Vip of 0 in start month
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Strtdate != ""  && (samp.Compdate =="" || Convert.ToInt32(samp.Compdate) < Convert.ToInt32(DateTime.Now.AddYears(-2).ToString("yyyyMM"))))
            {
                MonthlyVip mv = mvs.GetMonthVip(samp.Strtdate);
                if (mv!= null && mv.Vipdata == 0 && mv.Vipflag != "B" && mv.Vipflag != "M")
                    Setflag(4);
                else
                    Setnoflag(4);
            }
            else
                Setnoflag(4);

            //6 Vip of 0 in completion month
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Compdate != "")
            {
                MonthlyVip mv = mvs.GetMonthVip(samp.Compdate);
                if (mv != null && (mv.Vipdata == 0 || mv.Vipflag == "B"))
                    Setflag(5);
                else
                    Setnoflag(5);
            }
            else
                Setnoflag(5);

            //7 Unmatched project dates
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Compdate != "")
            {
                string start_date = "";
                string end_date = "";
                foreach (var value in mvs.monthlyViplist)
                {
                    if (value.Vipdata >= 0 && value.Vipflag != "B")
                    {
                        end_date = value.Date6;
                        break;
                    }
                }
                foreach (var value in mvs.monthlyViplist.AsEnumerable().Reverse())
                {
                    if (value.Vipdata >= 0 && value.Vipflag != "B")
                    {
                        start_date = value.Date6;
                        break;
                    }
                }

                if ((start_date != "") && (end_date != ""))
                {
                    d1 = DateTime.ParseExact(start_date, "yyyyMM", CultureInfo.InvariantCulture);
                    d2 = DateTime.ParseExact(end_date, "yyyyMM", CultureInfo.InvariantCulture);
                                        
                    month_diff = (d2.Year - d1.Year) * 12 + (d2.Month - d1.Month);
                }

                if ((samp.Strtdate != start_date) || (samp.Compdate != end_date) || (month_diff != mvs.GetSumMons()-1))
                {
                    Setflag(6);
                }
                else
                    Setnoflag(6);

            }
            else
                Setnoflag(6);


            //8 Rvitm5c reported 0
            if (samp.Rvitm5c == 0 && (samp.Flagr5c == "A" || samp.Flagr5c == "R") && (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8"))
                Setflag(7);
            else
                Setnoflag(7);

            //9 Started more than 2 years ago
            string process_date2 = DateTime.Now.AddYears(-2).ToString("yyyyMM");
            Setnoflag(8);
            if (samp.Strtdate !="" && (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8"))
            {
                if ( Convert.ToInt32(samp.Strtdate) < Convert.ToInt32(process_date2) && (mvs.GetCumvip()==0))
                    Setflag(8);
            }
            
            //10 Negative VIP
            set_flag = false;
            if (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8")
            {
                foreach (var value in mvs.GetMonthVipList(samp.Strtdate, process_date))
                {
                    if (value.Vipdata < 0)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setflag(9);
            else
                Setnoflag(9);

            //11 Completion date with no Rvitm5c
            if (samp.Compdate != "" && samp.Rvitm5c == 0 && samp.Flagr5c == "B")
                Setflag(10);
            else
                Setnoflag(10);

            //12 Item5b without Item5a
            if (samp.Item5b != 0 && ((samp.Item5a == 0 && samp.Flag5a == "B") || samp.Flag5a == "M"))
                Setflag(11);
            else
                Setnoflag(11);

            //13 Start before seldate
            Setnoflag(12);
            if (samp.Strtdate != "")
            {
                d1 = DateTime.ParseExact(mast.Seldate, "yyyyMM", CultureInfo.InvariantCulture);
                d2 = DateTime.ParseExact(samp.Strtdate, "yyyyMM", CultureInfo.InvariantCulture);

                if (d1.AddMonths(-6) > d2 && samp.Compdate == "" && samp.Flagstrtdate == "R")
                    Setflag(12);
            }
                

            //14 Rvitm5c>= 3 times selval
            if (samp.Flagr5c != "M" && mast.Projselv >0 && samp.Rvitm5c >= 3 * mast.Projselv)
                Setflag(13);
            else
                Setnoflag(13);

            //15 selval > 3*rvitm5c
            if (samp.Flagr5c != "M" && samp.Flagr5c != "B" && mast.Projselv >= 3 * samp.Rvitm5c)
                Setflag(14);
            else
                Setnoflag(14);

            //16 Cump vip > 140%  
            if ((mast.Owner != "T" && mast.Owner != "E" && mast.Owner != "G" && mast.Owner != "R" && mast.Owner != "O" && mast.Owner != "W") && samp.Rvitm5c > 0 && mvs.GetCumPercent(samp.Rvitm5c) > 140)
                Setflag(15);
            else
                Setnoflag(15);

            //17 Vip 25% of rvitm5c
            var mv25 = from mp in mvs.monthlyViplist where mp.Pct5c >= 25 select mp;
            if (mast.Owner != "M" && samp.Rvitm5c >= 10000 && mv25 != null && mv25.ToList().Count > 0)
                Setflag(16);
            else
                Setnoflag(16);

            //18 Vip 50% of Rvitm5C
            var mv50 = from mp in mvs.monthlyViplist where mp.Pct5c >= 50 select mp;
            if (mast.Owner != "M" && (samp.Rvitm5c >= 1000 && samp.Rvitm5c < 10000) && mv50 != null && mv50.ToList().Count > 0)
                Setflag(17);
            else
                Setnoflag(17);

            //19 Vip 75% of Rvitm5c
            var mv75 = from mp in mvs.monthlyViplist where mp.Pct5c >= 75 select mp;
            if (mast.Owner != "M" && (samp.Rvitm5c > 75 && samp.Rvitm5c < 1000) && mv75 != null && mv75.ToList().Count > 0)
                Setflag(18);
            else
                Setnoflag(18);

            //20 Outlier project
            int totc=0;
            if ((samp.Compdate != "") || (mvs.GetCumvip() > samp.Rvitm5c)) 
                totc = mvs.GetCumvip() ;
            else
                totc = samp.Rvitm5c;

            int outlier = (int)(sampwt * totc);

            Setnoflag(19);
            if ((sampwt != 1.0) && (mast.Owner != "M"))
            {
                List<string> nlist = new List<string> { "N", "E", "G", "R", "O", "W", "T" };

                string ntc = mast.Newtc.Substring(0, 2);
                if ((ntc == "00" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "00" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "01" && nlist.Contains(mast.Owner) && outlier > 250000) ||
                    (ntc == "01" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "02" && nlist.Contains(mast.Owner) && outlier > 400000) ||
                    (ntc == "02" && !nlist.Contains(mast.Owner) && outlier > 200000) ||
                    (ntc == "03" && nlist.Contains(mast.Owner) && outlier > 600000) ||
                    (ntc == "03" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "04" && nlist.Contains(mast.Owner) && outlier > 250000) ||
                    (ntc == "04" && !nlist.Contains(mast.Owner) && outlier > 50000) ||
                    (ntc == "05" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "05" && !nlist.Contains(mast.Owner) && outlier > 300000) ||
                    (ntc == "06" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "06" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "07" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "07" && !nlist.Contains(mast.Owner) && outlier > 200000) ||
                    (ntc == "08" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "08" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "09" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "09" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "10" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "10" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "11" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "11" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "12" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "12" && !nlist.Contains(mast.Owner) && outlier > 450000) ||
                    (ntc == "13" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "13" && !nlist.Contains(mast.Owner) && outlier > 200000) ||
                    (ntc == "14" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "14" && !nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "15" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "15" && !nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "16" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "16" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (ntc == "19" && nlist.Contains(mast.Owner) && outlier > 100000) ||
                    (ntc == "19" && !nlist.Contains(mast.Owner) && outlier > 150000) ||
                    (Convert.ToInt32(ntc) >= 20 && Convert.ToInt32(ntc) <= 39 && nlist.Contains(mast.Owner) && outlier > 200000) ||
                    (Convert.ToInt32(ntc) >= 20 && Convert.ToInt32(ntc) <= 39 && !nlist.Contains(mast.Owner) && outlier > 150000))
                {
                    new_oadj = CalOadj(samp, mast, mvs, sampwt, 0);
                    new_fwgt = Calfwgt(samp, mast, soc, mvs, sampwt, 0);

                    Setflag(19);
                }
                else
                {
                    new_oadj = CalOadj(samp, mast, mvs, sampwt, 1.00);
                    new_fwgt = Calfwgt(samp, mast, soc, mvs, sampwt, 1.00);
                }
            }
            else
            {
                new_oadj = CalOadj(samp, mast, mvs, sampwt, 1.00);
                new_fwgt = Calfwgt(samp, mast, soc, mvs, sampwt, 1.00);
            }
               
                       

            //21 Item6 50% or more Rvitm5c
            if (samp.Rvitm5c > 0 && samp.Item6 >= samp.Rvitm5c * 0.5)
                Setflag(20);
            else
                Setnoflag(20);

            //22 Item7 blank, newtc20-39
            if ((mast.Owner == "N" && Convert.ToInt32(mast.Newtc.Substring(0, 2)) >= 20) && samp.Rvitm5c > 0 && (samp.Flag5c == "R" || samp.Flag5c == "A" || samp.Flag5c == "O") && samp.Capexp == 0 && samp.Flagcap == "B")
                Setflag(21);
            else
                Setnoflag(21);

            //23 Rvitm5c + item6 = item7
            if (((mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") || (mast.Owner == "N" && Convert.ToInt32(mast.Newtc.Substring(0, 2)) >= 20)) && samp.Rvitm5c > 0 && (samp.Flag5c == "R" || samp.Flag5c == "A" || samp.Flag5c == "O") && (samp.Rvitm5c + samp.Item6 == samp.Capexp))
                Setflag(22);
            else
                Setnoflag(22);

            //24 Item7> Rvitm5c
            if (((mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") || (mast.Owner == "N" && Convert.ToInt32(mast.Newtc.Substring(0, 2)) >= 20)) && samp.Rvitm5c > 0 && (samp.Flag5c == "R" || samp.Flag5c == "A" || samp.Flag5c == "O") && samp.Capexp > samp.Rvitm5c)
                Setflag(23);
            else
                Setnoflag(23);

            //25 Vip impute over 5C
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Compdate == "" && (mvs.GetCumvipr() > samp.Rvitm5c * 0.9)
                && mvs.GetCumvip() > samp.Rvitm5c && mvs.GetMonthVip(process_date).Vipflag == "M")
                Setflag(24);
            else
                Setnoflag(24);

            //26 Vip of 0 in last 3 months
            Setnoflag(25);
            if (mvs.GetSumMons() > 0)
            {
                //need change*****
                if (samp.Strtdate != "" && samp.Compdate == "")
                {
                    string process_date3 = DateTime.Now.AddMonths(-3).ToString("yyyyMM");

                    //if start date less than process_date3
                    if (Convert.ToInt32(samp.Strtdate) <= Convert.ToInt32(process_date3))
                    {
                        var last3 = from mp in mvs.GetMonthVipList(process_date3, process_date) where (mp.Vipdata == 0 && (mp.Vipflag == "A" || mp.Vipflag == "R")) select mp;
                        if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8")  && last3.ToList().Count == 3)
                            Setflag(25);
                    }
                    //else if (Convert.ToInt32(samp.Strtdate) <= Convert.ToInt32(process_date))
                    //{
                    //    var last = from mp in mvs.GetMonthVipList(samp.Strtdate, process_date) where (mp.Vipdata == 0 && (mp.Vipflag == "A" || mp.Vipflag == "R")) select mp;            
                    //    if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Strtdate != "" && last.ToList().Count > 0)
                    //        Setflag(25);
                    //}
                }
            }
            

            //27 Completed before selection date
            Setnoflag(26);
            if (mast.Seldate != "" && samp.Compdate != "")
            {
                if (Convert.ToInt32(mast.Seldate) > Convert.ToInt32(samp.Compdate))
                    Setflag(26);
            }
               

            //28 Cum vip < 95% of Rvitm5c
            if (samp.Compdate != "" && samp.Rvitm5c > 0 && mvs.GetCumPercent(samp.Rvitm5c) < 95)
                Setflag(27);
            else
                Setnoflag(27);

            //29 Missing key imputation variable
            Setnoflag(28);
            if (samp.Status == "3" || samp.Status == "7") 
            {
                if (samp.Status != "" && samp.Futcompd != "" && samp.Flagr5c == "B")  
                    Setflag(28);
                else if (samp.Strtdate != "" && samp.Futcompd == "" && samp.Rvitm5c != 0 )
                    Setflag(28);
                else if (samp.Strtdate == "" && samp.Futcompd !="" && samp.Rvitm5c !=0)
                    Setflag(28);
            }
               

            //30 900% of previous rvitm5c
            set_flag = false;
            foreach (var value in Cprauditlist)
            {
                if (value.Varnme == "RVITM5C" && Convert.ToInt32(value.Oldval) > 0 && Convert.ToInt32(value.Newval) > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((Convert.ToInt32(value.Oldval) - Convert.ToInt32(value.Newval)) / Convert.ToInt32(value.Oldval)) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setflag(29);
            else
                Setnoflag(29);


            //31 900% of previous item6
            set_flag = false;
            foreach (var value in Cprauditlist)
            {
                if (value.Varnme == "ITEM6" && Convert.ToInt32(value.Oldval) > 0 && Convert.ToInt32(value.Newval) > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((Convert.ToInt32(value.Oldval) - Convert.ToInt32(value.Newval)) / Convert.ToInt32(value.Oldval)) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setflag(30);
            else
                Setnoflag(30);

            //32 900% of previous capexp
            set_flag = false;
            foreach (var value in Cprauditlist)
            {
                if (value.Varnme == "CAPEXP" && Convert.ToInt32(value.Oldval) > 0 && Convert.ToInt32(value.Newval) > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((Convert.ToInt32(value.Oldval) - Convert.ToInt32(value.Newval)) / Convert.ToInt32(value.Oldval)) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setflag(31);
            else
                Setnoflag(31);

            //33 900% of previous VIP
            set_flag = false;
            foreach (var value in Vipauditlist)
            {
                if (value.Oldvip > 0 && value.Newvip > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((value.Oldvip - value.Newvip) / value.Oldvip) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setflag(32);
            else
                Setnoflag(32);


            //34 Cumvip but no 5c
            if (mvs.GetCumvip() > 0 && samp.Rvitm5c == 0 && samp.Flagr5c == "B")
                Setflag(33);
            else
                Setnoflag(33);

            //35 Target Speed of Project
            Setnoflag(34);
            if (samp.Compdate == "" && samp.Strtdate!= "" && samp.Futcompd != "" && samp.Rvitm5c > 0)
            {
                DateTime strt = DateTime.ParseExact(samp.Strtdate, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime fut = DateTime.ParseExact(samp.Futcompd, "yyyyMM", CultureInfo.InvariantCulture);

                int estlot = GetNumberMonthsDiff(strt, fut) + 1;
                int v = mvs.GetCumvip(); int m = mvs.GetSumMons();
                bool me = ((double)v /(double)m) * ((double)estlot/(double)samp.Rvitm5c) < 0.5; 
                
                bool my = ((double)v /(double)m) * ((double)estlot/(double)samp.Rvitm5c) > 1.5;

                if (mvs.GetSumMons() > 3 && (me || my))
                    Setflag(34);
            }


            ////36 Item5A is 0
            //if (samp.Item5a == 0 && samp.Flag5a != "B")
            //    Setflag(35);
            //else
            //    Setnoflag(35);

            //37 WITHIN SURVEY owner changed
            if (owner_change == 1)
                Setflag(36);
            else
                Setnoflag(36);

            //38 REPORTED VIP WITH NO STRAT DATE 
            if (mvs.GetCumvipr() > 0 && samp.Strtdater == "")
                Setrflag(37);
            else
                Setnorflag(37);

            //39 CENTURION COMMENTS -- cannot be changed

            //40 owner is M, cost per unit out of range
            if (mast.Owner == "M" && soc.Costpu != 0)
            {
                int region = GetRegion(mast.Fipstate);

                if (region == 1 && (soc.Costpu < 50 || soc.Costpu > 400))
                    Setflag(39);
                else if (region == 2 && (soc.Costpu < 50 || soc.Costpu > 400))
                    Setflag(39);
                else if (region == 3 && (soc.Costpu < 50 || soc.Costpu > 400))
                    Setflag(39);
                else if (region == 4 && (soc.Costpu < 50 || soc.Costpu > 450))
                    Setflag(39);
                else
                    Setnoflag(39);
            }
            else
                Setnoflag(39);

            //41 Cum vip > 110% of rvitm5c

            if ((mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") && samp.Rvitm5c > 0 && mvs.GetCumPercent(samp.Rvitm5c) > 100)
                Setflag(40);
            else
                Setnoflag(40);

            // 42 vip 20% of rvimt5c (VG1)
            var mv20 = from mp in mvs.monthlyViplist where mp.Pct5c >= 20 select mp;
            if (mast.Owner == "M" && soc.Runits >= 200 && mv20 != null && mv20.ToList().Count > 0)
                Setflag(41);
            else
                Setnoflag(41);

            //43 vip 50% of rvitm5c (VG2)
            if (mast.Owner == "M" && (soc.Runits >= 100 && soc.Runits < 200) && mv50 != null && mv50.ToList().Count > 0)
                Setflag(42);
            else
                Setnoflag(42);

            //44 Vip 75% of RVIMT5C(VG3)
            var mv751 = from mp in mvs.monthlyViplist where mp.Pct5c >= 75 select mp;
            if (mast.Owner == "M" && (soc.Runits >= 2 && soc.Runits < 100) && mv751 != null && mv751.ToList().Count > 0)
                Setflag(43);
            else
                Setnoflag(43);

            //45 RESPONDENT UPDATE -- cannot be changed

            //46 PREVIOUSLY REPORTED VIP BLANKED OUT IN CENTURION"

            //47 Analyst data update by reported
            set_flag = false;
            foreach (var value in Vipauditlist)
            {
                if (value.Oldvip > 0 && value.Newvip > 0 && value.Oldflag == "A" && value.Newflag == "R")
                {
                    set_flag = true;
                    break;  
                }
            }
            if (set_flag)
                Setflag(46);
            else
                Setnoflag(46);


            //48 Vip 999% of rvitm5c
            var mv999 = from mp in mvs.monthlyViplist where mp.Pct5c >= 999 select mp;
            if (mv999.ToList().Count > 0)
                Setflag(47);
            else
                Setnoflag(47);

        }

        //Set up data flags
        private void SetupReportflags(Sample samp, Master mast, Soc soc, MonthlyVips mvs, List<Cpraudit> Cprauditlist, List<Vipaudit> Vipauditlist, bool status_change, int owner_change)
        {
            DateTime d1;
            DateTime d2;
            int month_diff = 0;
            bool set_flag = false;

            if (status_change)
                Setrflag(0);
            else
                Setnorflag(0);

            //2 owner change
            if (owner_change == 2)
                Setrflag(1);
            else
                Setnorflag(1);


            //3 Item5a + item5b <> rvitm5c
            if (samp.Item5ar + samp.Item5br != samp.Rvitm5cr)
                Setrflag(2);
            else
                Setnorflag(2);      

            //4 Report follows impute
            set_flag = false;
            if (samp.Strtdater != "" && (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8"))
            {
                if (Convert.ToInt32(samp.Strtdater) < Convert.ToInt32(process_date) && mvs.GetSumMons() > 0)
                {
                    if (samp.Compdater != "")
                    {
                        List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdater, samp.Compdater);
                       
                        MonthlyVip mv = null;
                        foreach (var value in mvlist)
                        {
                            if (mv != null)
                            {
                                if ((mv.Vipflag != "M") && value.Vipflag == "M")
                                {
                                    set_flag = true;
                                    break;
                                }
                            }
                            mv = value;
                        }
                        if (!set_flag)
                        {
                            foreach (var value in mvlist)
                            {
                                if (value.Vipdatar == 0 && value.Vipflag == "B")
                                {
                                    set_flag = true;
                                    break;
                                }
                            }
                        }

                    }
                    else
                    {
                        List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdater, DateTime.Now.AddMonths(-1).ToString("yyyyMM"));
                        
                        MonthlyVip mv = null;
                        foreach (var value in mvlist)
                        {
                            if (mv != null)
                            {
                                if (mv.Vipflag != "M" && mv.Vipflag != "B" && (value.Vipflag == "M" || value.Vipflag == "B"))
                                {
                                    set_flag = true;
                                    break;
                                }
                            }
                            mv = value;
                        }
                        //if (!set_flag)
                        //{
                        //    foreach (var value in mvlist)
                        //    {
                        //        if (value.Date6 != process_date)
                        //        {
                        //            if (value.Vipdatar == 0 && value.Vipflag == "B")
                        //            {
                        //                set_flag = true;
                        //                break;
                        //            }
                        //        }
                        //    }
                        //}

                    }
                }
            }
            if (set_flag)
                Setrflag(3);
            else
                Setnorflag(3);

            //5 Vip of 0 in start month
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Strtdater != "" && Convert.ToInt32(samp.Strtdater) <= Convert.ToInt32(process_date))
            {
                MonthlyVip mv = mvs.GetMonthVip(samp.Strtdater);
                if (mv!= null && mv.Vipdata == 0 && mv.Vipflag != "B" && mv.Vipflag != "M")
                    Setrflag(4);
                else
                    Setnorflag(4);
            }
            else
                Setnorflag(4);

            //6 Vip of 0 in completion month
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Compdater != "")
            {
                MonthlyVip mv = mvs.GetMonthVip(samp.Compdater);
                if (mv != null && (mv.Vipdata == 0 || mv.Vipflag == "B" ))
                    Setrflag(5);
                else
                    Setnorflag(5);
            }
            else
                Setnorflag(5);

            //7 Unmatched project dates
            if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Compdater != "")
            {
                string start_date = "";
                string end_date = "";
                foreach (var value in mvs.monthlyViplist)
                {
                    if (value.Vipdatar >= 0 && value.Vipflag != "B")
                    {
                        end_date = value.Date6;
                        break;
                    }
                }
                foreach (var value in mvs.monthlyViplist.AsEnumerable().Reverse())
                {
                    if (value.Vipdatar >= 0 && value.Vipflag != "B")
                    {
                        start_date = value.Date6;
                        break;
                    }
                }

                if ((start_date != "") && (end_date != ""))
                {
                    d1 = DateTime.ParseExact(start_date, "yyyyMM", CultureInfo.InvariantCulture);
                    d2 = DateTime.ParseExact(end_date, "yyyyMM", CultureInfo.InvariantCulture);

                    month_diff = (d2.Year - d1.Year) * 12 + (d2.Month - d1.Month);
                }

                if ((samp.Strtdater != start_date) || (samp.Compdater != end_date) || (month_diff != mvs.GetSumMons() - 1))
                {
                    Setrflag(6);
                }
                else
                    Setnorflag(6);

            }
            else
                Setnorflag(6);


            //8 Rvitm5c reported 0
            if (samp.Rvitm5cr == 0 && (samp.Flagr5c == "A" || samp.Flagr5c == "R") && (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8"))
                Setrflag(7);
            else
                Setnorflag(7);

            //9 Started more than 2 years ago
            string process_date2 = DateTime.Now.AddYears(-2).ToString("yyyyMM");
            Setnorflag(8);
            if (samp.Strtdater != "" && (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8"))
            {
                if (Convert.ToInt32(samp.Strtdater) < Convert.ToInt32(process_date2) && (mvs.GetCumvipr() == 0))
                    Setrflag(8);
            }

            //10 HQ only

            //11 Completion date with no Rvitm5c
            if (samp.Compdater != "" && samp.Rvitm5cr == 0 && samp.Flagr5c == "B")
                Setrflag(10);
            else
                Setnorflag(10);

            //12 Item5b without Item5a
            if (samp.Item5br != 0 && (samp.Item5ar == 0 && samp.Flag5a == "B"))
                Setrflag(11);
            else
                Setnorflag(11);

            //13 HQ only

            //14 Rvitm5c>= 3 times selval
            if (samp.Flagr5c != "M" && mast.Projselv >0 && samp.Rvitm5cr >= 3 * mast.Projselv)
                Setrflag(13);
            else
                Setnorflag(13);

            //15 selval > 3*rvitm5c
            if (samp.Rvitm5cr >0 && mast.Projselv >= 3 * samp.Rvitm5cr)
                Setrflag(14);
            else
                Setnorflag(14);

            //16 Cump vip > 140%  
            if ((mast.Owner != "T" && mast.Owner != "E" && mast.Owner != "G" && mast.Owner != "R" && mast.Owner != "O" && mast.Owner != "W") && samp.Rvitm5cr > 0 && mvs.GetCumPercentr(samp.Rvitm5cr) > 140)
                Setrflag(15);
            else
                Setnorflag(15);

            //17 Vip 25% of rvitm5c
            var mv25 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 25 select mp;
            if (mast.Owner != "M" && samp.Rvitm5cr >= 10000 && mv25 != null && mv25.ToList().Count > 0)
                Setrflag(16);
            else
                Setnorflag(16);

            //18 Vip 50% of Rvitm5C
            var mv50 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 50 select mp;
            if (mast.Owner != "M" && (samp.Rvitm5cr >= 1000 && samp.Rvitm5cr < 10000) && mv50 != null && mv50.ToList().Count > 0)
                Setrflag(17);
            else
                Setnorflag(17);

            //19 Vip 75% of Rvitm5c
            var mv75 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 75 select mp;
            if (mast.Owner != "M" && (samp.Rvitm5cr > 75 && samp.Rvitm5cr <= 1000) && mv75 != null && mv75.ToList().Count > 0)
                Setrflag(18);
            else
                Setnorflag(18);

            //20 HQ only

            //21 Item6 50% or more Rvitm5c
            if (samp.Rvitm5cr > 0 && samp.Item6r >= samp.Rvitm5cr * 0.5)
                Setrflag(20);
            else
                Setnorflag(20);

            //22 Item7 blank, newtc20-39
            if ((mast.Owner == "N" && Convert.ToInt32(mast.Newtc.Substring(0, 2)) >= 20) && samp.Rvitm5cr > 0 && (samp.Flagr5c == "R" || samp.Flagr5c == "A" || samp.Flagr5c == "O") && samp.Capexpr == 0 && samp.Flagcap == "B")
                Setrflag(21);
            else
                Setnorflag(21);

            //23 Rvitm5c + item6 = item7
            if (((mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") || (mast.Owner == "N" && Convert.ToInt32(mast.Newtc.Substring(0, 2)) >= 20)) && samp.Rvitm5cr > 0 && (samp.Flagr5c == "R" || samp.Flagr5c == "A" || samp.Flagr5c == "O") && (samp.Rvitm5cr + samp.Item6r == samp.Capexpr))
                Setrflag(22);
            else
                Setnorflag(22);

            //24 Item7> Rvitm5c
            if (((mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") || (mast.Owner == "N" && Convert.ToInt32(mast.Newtc.Substring(0, 2)) >= 20)) && samp.Rvitm5cr > 0 && (samp.Flagr5c == "R" || samp.Flagr5c == "A" || samp.Flagr5c == "O") && samp.Capexpr > samp.Rvitm5cr)
                Setrflag(23);
            else
                Setnorflag(23);

            //25 HQ only

            //26 Vip of 0 in last 3 months
            Setnorflag(25);
            if (mvs.GetSumMons() > 0)
            {
                //need change*****
                if (samp.Strtdater != "" && samp.Compdater == "")
                {
                    string process_date3 = DateTime.Now.AddMonths(-3).ToString("yyyyMM");

                    //if start date less than process_date3
                    if (Convert.ToInt32(samp.Strtdater) <= Convert.ToInt32(process_date3))
                    {
                        var last3 = from mp in mvs.GetMonthVipList(process_date3, process_date) where (mp.Vipdatar == 0 && (mp.Vipflag == "A" || mp.Vipflag == "R")) select mp;
                        if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && last3.ToList().Count == 3)
                            Setrflag(25);
                    }
                    //else if (Convert.ToInt32(samp.Strtdater) <= Convert.ToInt32(process_date))
                    //{
                    //    var last = from mp in mvs.GetMonthVipList(samp.Strtdater, process_date) where (mp.Vipdatar == 0 && mp.Vipflag != "B") select mp;
                    //    if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && samp.Strtdater != "" && last.ToList().Count > 0)
                    //        Setrflag(25);
                    //}
                }
            }


            //27 Completed before selection date
            Setnorflag(26);
            if (mast.Seldate != "" && samp.Compdater != "")
            {
                if (Convert.ToInt32(mast.Seldate) > Convert.ToInt32(samp.Compdater))
                    Setrflag(26);
            }

            //28 Cum vip < 95% of Rvitm5c
            if (samp.Compdater != "" && samp.Rvitm5cr > 0 && mvs.GetCumPercentr(samp.Rvitm5cr) < 95)
                Setrflag(27);
            else
                Setnorflag(27);

            //29 Missing key imputation variable
            Setnorflag(28);
            if (samp.Status == "3" || samp.Status == "7")
            {
                if (samp.Status != "" && samp.Futcompdr != "" && samp.Flagr5c == "B")
                    Setrflag(28);
                else if (samp.Strtdater != "" && samp.Futcompdr == "" && samp.Rvitm5cr != 0)
                    Setrflag(28);
                else if (samp.Strtdater == "" && samp.Futcompdr != "" && samp.Rvitm5cr != 0)
                    Setrflag(28);
            }


            //30 900% of previous rvitm5c
            set_flag = false;
            foreach (var value in Cprauditlist)
            {
                if (value.Varnme == "RVITM5C" && Convert.ToInt32(value.Oldval) > 0 && Convert.ToInt32(value.Newval) > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((Convert.ToInt32(value.Oldval) - Convert.ToInt32(value.Newval)) / Convert.ToInt32(value.Oldval)) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setrflag(29);
            else
                Setnorflag(29);


            //31 900% of previous item6
            set_flag = false;
            foreach (var value in Cprauditlist)
            {
                if (value.Varnme == "ITEM6" && Convert.ToInt32(value.Oldval) > 0 && Convert.ToInt32(value.Newval) > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((Convert.ToInt32(value.Oldval) - Convert.ToInt32(value.Newval)) / Convert.ToInt32(value.Oldval)) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setrflag(30);
            else
                Setnorflag(30);

            //32 900% of previous capexp
            set_flag = false;
            foreach (var value in Cprauditlist)
            {
                if (value.Varnme == "CAPEXP" && Convert.ToInt32(value.Oldval) > 0 && Convert.ToInt32(value.Newval) > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((Convert.ToInt32(value.Oldval) - Convert.ToInt32(value.Newval)) / Convert.ToInt32(value.Oldval)) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setrflag(31);
            else
                Setnorflag(31);

            //33 900% of previous VIP
            set_flag = false;
            foreach (var value in Vipauditlist)
            {
                if (value.Oldvip > 0 && value.Newvip > 0 && value.Oldflag == "R" && value.Newflag == "R")
                {
                    if (Math.Abs((value.Oldvip - value.Newvip) / value.Oldvip) >= 9)
                    {
                        set_flag = true;
                        break;
                    }
                }
            }
            if (set_flag)
                Setrflag(32);
            else
                Setnorflag(32);


            //34 Cumvip but no 5c
            if (mvs.GetCumvipr() > 0 && samp.Rvitm5cr == 0 && samp.Flagr5c == "B")
                Setrflag(33);
            else
                Setnorflag(33);

            //35 Target Speed of Project
            Setnorflag(34);
            if (samp.Compdater == "" && samp.Strtdater != "" && samp.Futcompdr != "" && samp.Rvitm5cr > 0)
            {
                DateTime strt = DateTime.ParseExact(samp.Strtdater, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime fut = DateTime.ParseExact(samp.Futcompdr, "yyyyMM", CultureInfo.InvariantCulture);

                int estlot = GetNumberMonthsDiff(strt, fut) + 1;
                int v = mvs.GetCumvipr(); int m = mvs.GetSumMons();
                bool me = ((double)v / (double)m) * ((double)estlot / (double)samp.Rvitm5cr) < 0.5;

                bool my = ((double)v / (double)m) * ((double)estlot / (double)samp.Rvitm5cr) > 1.5;

                if (mvs.GetSumMons() > 3 && (me || my))
                    Setrflag(34);
            }


            ////36 Item5A is 0
            //if (samp.Item5ar == 0 && samp.Flag5a != "B")
            //    Setrflag(35);
            //else
            //    Setnorflag(35);

            //37 WITHIN SURVEY -- cannot be changed
            if (owner_change == 1)
                Setrflag(36);
            else
                Setnorflag(36);

            //38 REPORTED VIP WITH NO STRAT DATE
            if (mvs.GetCumvipr() > 0 && samp.Strtdater == "")
                Setrflag(37);
            else
                Setnorflag(37);

            //39 CENTURION COMMENTS -- cannot be changed


            //40 owner is M, cost per unit out of range
            if (mast.Owner == "M" && soc.Costpu != 0)
            {
                int region = GetRegion(mast.Fipstate);

                if (region == 1 && (soc.Costpu < 50 || soc.Costpu > 400))
                    Setrflag(39);
                else if (region == 2 && (soc.Costpu < 50 || soc.Costpu > 400))
                    Setrflag(39);
                else if (region == 3 && (soc.Costpu < 50 || soc.Costpu > 400))
                    Setrflag(39);
                else if (region == 4 && (soc.Costpu < 50 || soc.Costpu > 450))
                    Setrflag(39);
                else
                    Setnorflag(39);
            }
            else
                Setnorflag(39);

            //41 Cum vip > 110% of rvitm5cr
            
            if ((mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") && samp.Rvitm5cr > 0 && mvs.GetCumPercent(samp.Rvitm5cr) > 100)
                Setrflag(40);
            else
                Setnorflag(40);

            // 42 vip 20% of rvimt5c (VG1)
            var mv20 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 20 select mp;
            if (mast.Owner == "M" && soc.Runits >= 200 && mv20 != null && mv20.ToList().Count > 0)
                Setrflag(41);
            else
                Setnorflag(41);

            //43 vip 50% of rvitm5c (VG2)
            var m50 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 50 select mp;
            if (mast.Owner == "M" && (soc.Runits >= 100 && soc.Runits < 200) && mv50 != null && m50.ToList().Count > 0)
                Setrflag(42);
            else
                Setnorflag(42);

            //44 Vip 75% of RVIMT5C(VG3)
            var mv751 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 75 select mp;
            if (mast.Owner == "M" && (soc.Runits >= 2 && soc.Runits < 100) && mv751 != null && mv751.ToList().Count > 0)
                Setrflag(43);
            else
                Setnorflag(43);

            //45 RESPONDENT UPDATE -- cannot be changed

            //47 Analyst data update by reported
            set_flag = false;
            foreach (var value in Vipauditlist)
            {
                if (value.Oldvip > 0 && value.Newvip > 0 && value.Oldflag == "A" && value.Newflag == "R")
                {
                    set_flag = true;
                    break;
                }
            }
            if (set_flag)
                Setrflag(46);
            else
                Setnorflag(46);


            //47 Vip 999% of rvitm5c
            var mv999 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 999 select mp;
            if (mv999.ToList().Count > 0)
                Setrflag(47);
            else
                Setnorflag(47);

        }


        //Get region from fipstate
        private int GetRegion(string fipstate)
        {
            int region = 0;

            List<string> rg1 = new List<string> { "09", "23", "25", "33", "34", "36", "42", "50" };
            List<string> rg2 = new List<string> { "17", "18", "19", "20", "26", "27", "29", "31", "38", "39", "46", "55" };
            List<string> rg3 = new List<string> { "01", "05", "10", "11", "12", "13", "21", "22", "24", "28", "37", "40", "45", "48", "51", "54" };
            List<string> rg4 = new List<string> { "02", "04", "06", "08", "15", "16", "30", "32", "35", "41", "49", "53", "56" };

            if (rg1.Contains(fipstate))
                region = 1;
            else if (rg2.Contains(fipstate))
                region = 2;
            else if (rg3.Contains(fipstate))
                region = 3;
            else if (rg4.Contains(fipstate))
                region = 4;

            return region;
        }


        //set flag 1
        private void Setflag(int index)
        {
            if (cprflaglist.ElementAt(index).value  == '0')        
                cprflaglist.ElementAt(index).value = '1';           
        }

        //set flag 0
        private void Setnoflag(int index)
        {
            if (cprflaglist.ElementAt(index).value != '0')
                cprflaglist.ElementAt(index).value = '0';
        }

        //set flag 1
        private void Setrflag(int index)
        {
            if (reportflaglist.ElementAt(index).value == '0')
                reportflaglist.ElementAt(index).value = '1';
        }

        //set flag 0
        private void Setnorflag(int index)
        {
            if (reportflaglist.ElementAt(index).value != '0')
                reportflaglist.ElementAt(index).value = '0';
        }

        private float CalOadj(Sample samp, Master mast, MonthlyVips mvs, float sampwt, double cal_oadj)
        {
            int totc;
            if ((samp.Compdate != "") || (mvs.GetCumvip() > samp.Rvitm5c))
                totc = mvs.GetCumvip();
            else
                totc = samp.Rvitm5c;

            double temp_oadj = 0;
            double oadj = 0;

            if (cal_oadj == 0)
            {
                int cutoff = GetCutoff(mast);
                if (totc > 0)
                {
                    if (totc > cutoff)
                        temp_oadj = (double)totc / (double)(totc * sampwt);
                    else
                        temp_oadj = (double)cutoff / (double)(totc * sampwt);

                    oadj = Math.Round(temp_oadj, 2);

                    if (oadj > 1.00)
                        oadj = 1.00;
                }
                else
                    oadj = 1.00;
            }
            else
                oadj = cal_oadj;

            float new_oadj = Convert.ToSingle(oadj);

            return new_oadj;
        }


        //Calcualte fwgt
        private float Calfwgt(Sample samp, Master mast, Soc soc, MonthlyVips mvs, float sampwt, double cal_oadj)
        {
            int totc;
            if ((samp.Compdate != "") || (mvs.GetCumvip() > samp.Rvitm5c))
                totc = mvs.GetCumvip();
            else
                totc = samp.Rvitm5c;

            double temp_oadj = 0;
            double oadj = 0;
            double aem = 0;
            double ddf = 0;

            if (cal_oadj == 0)
            {
                int cutoff = GetCutoff(mast);
                if (totc > 0)
                {
                    if (totc > cutoff)
                        temp_oadj = (double)totc / (double)(totc * sampwt);
                    else
                        temp_oadj = (double)cutoff / (double)(totc * sampwt);

                    oadj = Math.Round(temp_oadj, 5);

                    if (oadj > 1.00000)
                        oadj = 1.00000;
                }
                else
                    oadj = 1.0;
            }
            else
                oadj = cal_oadj;

            if (samp.Compdate != "" && (mvs.GetCumvip() < samp.Rvitm5c / 2))
            {
                if (Convert.ToInt32(samp.Compdate) >= 199601)
                    totc = samp.Rvitm5c;
            }

            if (totc > 0)
            {
                double bb = (double)(totc + samp.Item6)/(double)totc;
                aem = bb;
            }
            else
                aem = 1.0;

            if (mast.Owner == "M" || (mast.Source == "NP"))
                ddf = 1.000;
            else if (mast.Owner == "N" || mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W")
                ddf = 0.990;
            else
                ddf = 0.993;

            decimal st = (decimal)sampwt;

            decimal rfwgt = decimal.Round(st*(decimal)(oadj * aem * ddf * mast.Dmf), 2, MidpointRounding.AwayFromZero);
            
            float fwgt = Convert.ToSingle(rfwgt);

            if (fwgt > 999999.99)
                fwgt = (float)999999;

            return fwgt;
        }

        /*determaine cutoff value */
        private int GetCutoff(Master mast)
        {
            int cutoff = 0;
            List<string> nlist1 = new List<string> { "05", "06", "07", "08", "09", "10", "11", "12", "13", "14", "15" , "16", "19"};
            List<string> nlist2 = new List<string> { "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37", "38", "39" };

            string newtc2 = mast.Newtc.Substring(0, 2);
            if (mast.Owner == "N" || mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W")
            {
                if (newtc2 == "00")
                    cutoff = 100000;
                else if (newtc2 == "01")
                    cutoff = 250000;
                else if (newtc2 == "02")
                    cutoff = 400000;
                else if (newtc2 == "03")
                    cutoff = 600000;
                else if (newtc2 == "04")
                    cutoff = 250000;
                else if (nlist1.Contains(newtc2))
                    cutoff = 100000;
                else if (nlist2.Contains(newtc2))
                    cutoff = 200000;
            }
            else 
            {
                if (newtc2 == "00")
                    cutoff = 150000;
                else if (newtc2 == "01")
                    cutoff = 150000;
                else if (newtc2 == "02")
                    cutoff = 200000;
                else if (newtc2 == "03")
                    cutoff = 150000;
                else if (newtc2 == "04")
                    cutoff = 50000;
                else if (newtc2 == "05")
                    cutoff = 300000;
                else if (newtc2 == "06")
                    cutoff = 150000;
                else if (newtc2 == "07")
                    cutoff = 200000;
                else if (newtc2 == "08" || newtc2 == "09" || newtc2 == "10" || newtc2 == "11" || newtc2 == "16" || newtc2 == "19")
                    cutoff = 150000;
                else if (newtc2 == "12")
                    cutoff = 450000;
                else if (newtc2 == "13")
                    cutoff = 200000;
                else if (newtc2 == "14")
                    cutoff = 100000;
                else if (newtc2 == "15")
                    cutoff = 100000;
                else if (nlist2.Contains(newtc2))
                    cutoff = 150000;
            }

            return cutoff;
        }

        //Get number of months difference between two datetimes
        private int GetNumberMonthsDiff( DateTime dt1, DateTime dt2)
        {
            DateTime earlyDate = (dt1 > dt2) ? dt2.Date : dt1.Date;
            DateTime lateDate = (dt1 > dt2) ? dt1.Date : dt2.Date;

            // Start with 1 month's difference and keep incrementing
            // until we overshoot the late date
            int monthsDiff = 1;
            while (earlyDate.AddMonths(monthsDiff) <= lateDate)
            {
                monthsDiff++;
            }

            return monthsDiff - 1;
        }


     /*Construction*/
     public class Cprflag
     {          
        /*properties*/
        public int flagno { get; set; }
        public char value { get; set; }
        public string title { get; set; }
        public bool bypass { get; set; }     

     }
  }
}
