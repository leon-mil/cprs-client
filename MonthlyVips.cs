/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.MonthVip.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/4/2015
Inputs:             None
Parameters:	        id
Outputs:	        
Description:	    This class creates the getters and setters and stores
                    the monthly vip data that will be used in the c700 screen
                    
Detailed Design:    None 
Other:	            Called By: frmC700
 
Revision History:	
***************************************************************************************
 Modified Date :  5/24/2022
 Modified By   :  Christine zhang
 Keyword       :  
 Change Request:  
 Description   :  get rid of cumdbvip, cumdbvipr properties
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CprsBLL
{
    public class MonthlyVips
    {
        private bool with_all_vip = true;

        public string Id { get; private set; }
        public List<MonthlyVip> monthlyViplist = new List<MonthlyVip>();
        
        public bool Withallvips
        {
            get { return with_all_vip; }
            set { with_all_vip = value; }
        }
       

        //Get total vip
        public int GetCumvip()
        {
            int cumvip = 0;
            foreach (var value in monthlyViplist)
            {
                cumvip = cumvip + value.Vipdata;
            }
            return cumvip;        
        }

        

        //Get total percent vip to 5C
        public int GetCumPercent(int itm5c)
        {
            int cvip = GetCumvip();
            int cumpercent = 0;
            if (itm5c >0)
               cumpercent = Convert.ToInt32((double)cvip/itm5c * 100);  
        
            return cumpercent; 
            
        }

        //Get total reported vip
        public int GetCumvipr()
        {
            int cumvip = 0;
            foreach (var value in monthlyViplist)
            {
                cumvip = cumvip + value.Vipdatar;
            }
            return cumvip;
        }

        //Get total Cum vip to 5C
        public int GetCumPercentr(int itm5cr)
        {
            int cvip = GetCumvipr();
            int cumpercentr = 0;
            if (itm5cr >0)
               cumpercentr = Convert.ToInt32((double)cvip / itm5cr * 100);

            return cumpercentr; 

        }

        //Get total months of vip
        public int GetSumMons()
        {
            int summ = 0;
            foreach (var value in monthlyViplist)
            {
                if (value.Vipflag != "B" && value.Vipflag != "")
                    summ = summ + 1;
            }
            return summ;

        }

        //Get monthly vip for a date
        public MonthlyVip GetMonthVip(string monDate)
        {
            MonthlyVip mp = null;
            foreach (var value in monthlyViplist)
            {
                if (value.Date6 == monDate)
                {
                    mp = value;
                    break;
                }
            }
            return mp;

        }

        //Get monthly vip list under a time range
        public List<MonthlyVip> GetMonthVipList(string strtdate, string enddate)
        {
            List<MonthlyVip> mvl = new List<MonthlyVip>();
            foreach (var value in monthlyViplist)
            {
                if (strtdate != "" && enddate !="")
                {
                    if ((Convert.ToInt32(value.Date6) >= Convert.ToInt32(strtdate)) && (Convert.ToInt32(value.Date6) <= Convert.ToInt32(enddate)))
                    {
                        mvl.Add(value);
                    }
                }
                else if (strtdate == "" && enddate != "")
                {
                    if (Convert.ToInt32(value.Date6) <= Convert.ToInt32(enddate))
                    {
                        mvl.Add(value);
                    }
                }
                else if (strtdate != "" && enddate =="")
                {
                    if (Convert.ToInt32(value.Date6) >= Convert.ToInt32(strtdate))
                    {
                        mvl.Add(value);
                    }
                }

            }
            return mvl;

        }

        //update pct5cs, pct5cr when itm5c, itm5cr changed
        public void UpdatePct5cs(int itm5c, int itm5cr)
        {
            foreach (var value in monthlyViplist)
            {
                if (itm5c >0)
                    value.Pct5c = Convert.ToInt32((double)value.Vipdata / itm5c * 100);
                if (itm5cr >0)
                    value.Pct5cr = Convert.ToInt32((double)value.Vipdatar / itm5cr * 100); 
            }
        }

        /*Get Vip Flags */
        public List<string> VipFlags()
        {
            List<string> Vflags = new List<string>();
            foreach (var value in monthlyViplist)
            {
                if (!Vflags.Contains(value.Vipflag))
                  Vflags.Add(value.Vipflag);                
            }

            return Vflags;
        }

        //set is modified flag
        private bool isModified = false;
        public bool IsModified
        {
            get 
            {
                foreach (var value in monthlyViplist)
                {
                    if (value.vs == "a" ||value.vs == "m"|| value.vs == "d")
                    {
                        isModified = true;
                        
                    }
                                            
                 }
                return isModified;           
            }         
        }

        /*Reinitial status */
        public void ResetStatus()
        {
            foreach (var value in monthlyViplist)
            {
                value.vs = value.vsOld;
            }
            isModified = false;
        }


        //Construction
        public MonthlyVips(string id)
        {
            this.Id = id;
        }
    }

    //Monthly Vip Class
    public class MonthlyVip 
    {
        public string Date6 { get; set; }
        public string Date8 { get; set; }

        [DisplayName("VIPDATAR")]
        public int Vipdatar { get; set; }

        private int pct5cr = 0;
        [DisplayName("PCT5CR")]
        public int Pct5cr
        {
            get
            {
                return pct5cr;
            }
            set
            {
                pct5cr = value;
            }
        }
        
        private int vipdata =0;
        [DisplayName("VIPDATA")]
        public int Vipdata
        {
            get
            {
                return this.vipdata;
            }
            set
            {
                vipdata = value;
            }
        }

        private string vipflag = "B";
        [DisplayName("VIPFLAG")]
        public string Vipflag 
        {
            get
            {
                return this.vipflag;
            }
            set
            {
                vipflag = value;
            } 
        
        }
       
        
        //Pct5c property
        private int pct5c=0;
        [DisplayName("PCT5C")]
        public int Pct5c
        {
            get
            {
                return pct5c;
            }
            set
            {
                pct5c = value;
            }
        }
       
        //VipStatus property
        public string vs { get; set; }

        public string vsOld { get; set; }
        
    }
}
