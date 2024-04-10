/**********************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Ceflags.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2015
Inputs:             None
Parameters:	        Cejobs, current interview, finwt and propval
Outputs:	        Ceflags list and main ceflag string
Description:	    This class creates the getters and setters and stores
                    the data, generate ceflags. it will be used in the improvement screen
Detailed Design:    None 
Other:	            Called By: frmImprovement
 
Revision History:	
*********************************************************************
 Modified Date  :  3/6/2024
 Modified By    :  Christine Zhang
 Keyword        :  20240306cz
 Change Request :  CR 1434
 Description    :  Add jobidcode, replace detcode with jobidcode.
                   Update Setting of Edit flags
*********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Ceflags
    {
        /*Construction */
        public Ceflags(Cejobs pjobs, string pcurrInterview, int ppropval, float pfinwt)
        {
            jobs = pjobs;
            currInterview = pcurrInterview;
            propval = ppropval;
            finwt = pfinwt;

            BuildJobFlagList(jobs, currInterview, propval, finwt);
            CreateAllFlagList();
        }

        /*private properties */
        private List<Ceflag> ceflaglist = new List<Ceflag>();
        private string mainflag = String.Empty;
        private Cejobs jobs;
        private string currInterview;
        private int propval;
        private float finwt;

        /************************ public methods ******************/

        /*Build main ceflag of the current cejobs to use for save flag to database */
        public string GetMainCeflags()
        {
            BuildJobFlagList(jobs, currInterview, propval, finwt);
            return mainflag;
        }


        /*Method get Flag Description list for a job to use for display flags in improvement screen */
        public List<string> CeflagDescList(string jobidcode)
        {
            string fd;
            List<string> desclist = new List<string>();

            /*initial flags */
            string fflag = String.Empty;
            foreach (Ceflag f in ceflaglist)
	        {
	            if (f.jobidcode == jobidcode)
                {
                    fflag = f.flagstr;
                    break;
                }
	        }
           
            for (int i = 0; i < fflag.Length; i++)
            {
                string f = fflag.Substring(i, 1);
                if (f == "1")
                {
                    fd = Flaglist.ElementAt(i).ToString();
                    desclist.Add(fd);
                }
            }

            return desclist;
        }

        /*****************Private methods *******************************/

        /*Create flag list */
        private List<string> Flaglist = new List<string>();
        private void CreateAllFlagList()
        {
            Flaglist.Add("FLAG - Contract cost for DIY job");
            Flaglist.Add("FLAG - Sum of appliances >= total contract cost");
            Flaglist.Add("FLAG - Applicances not typical for this type of job");
            Flaglist.Add("FLAG - Bad applicance cost");
            Flaglist.Add("");
            Flaglist.Add("");
            Flaglist.Add("");
            Flaglist.Add("");
            Flaglist.Add("");
            Flaglist.Add("FLAG - Bad value for contract expense");
            Flaglist.Add("FLAG - Bad value for purchased materials");
            Flaglist.Add("FLAG - Bad value for rented materials");
            Flaglist.Add("FLAG - Bad value for total job cost");
            Flaglist.Add("FLAG - Contract job with no contract cost");
            Flaglist.Add("FLAG - New construction may be out of scope");
            Flaglist.Add("FLAG - DIY job without material costs");
            Flaglist.Add("FLAG - No Monthly Expense Indicator");
            Flaglist.Add("FLAG - Large expense relative to property value");
            Flaglist.Add("FLAG - May be out of scope");
            Flaglist.Add("FLAG - Match jobs found in the previous interview period");
            Flaglist.Add("FLAG - Duplicate jobs found");
            Flaglist.Add("");
        }
        
        /*Build job flag list base on current job and previous job */
        private void BuildJobFlagList(Cejobs jobs, string currInterview, int propval, float finwt)
        {
            if (jobs == null) return;
            if (jobs.cejoblist.Count() == 0) return;

            List<Cejob> jlist = jobs.GetJobsForInterview(currInterview);
            List<Cejob> pjlist = jobs.GetJobsForInterview((Convert.ToInt32(currInterview)-1).ToString());
            
            /*initial main flags,set each value to 0 */
            char[] mf = new char[22];
            for (int i = 0; i < mf.Length; i++)
            {
                mf[i] = '0';
            }
            mainflag = string.Empty;


            foreach (Cejob j in jlist)
            {
                /*set flag array and set default value to "0" */
                char[] f = new char[22];
                for (int i = 0; i < f.Length; i++)
                {
                    f[i] = '0';
                }

                int tmon1 = j.Con1 + j.Amt1 + j.Ren1;
                int tmon2 = j.Con2 + j.Amt2 + j.Ren2;
                int tmon3 = j.Con3 + j.Amt3 + j.Ren3;
                int tmon4 = j.Con4 + j.Amt4 + j.Ren4;

                string job1 = j.Jobidcode.Substring(0, 1);
                string job3 = j.Jobidcode.Substring(0, 3);

                if (job3=="215" || job3=="220" || job3=="230" || job3=="299" || job3=="316") 
                    job1 = "B";

                /*Set flags */
                if (j.Who == 1 && j.Tcon > 0)
                    f[0] = '1';

                if (j.Teqp > 0 && j.Tcon > 0 && j.Teqp >= j.Tcon)
                    f[1] = '1';

                if ((job1 == "1" || job1 == "B") && (j.Teqp > 0))
                    f[2] = '1';

                if ((j.Eqpcode1.Trim() != "") && (j.Teqp ==0))
                    f[3] = '1';

                /*Set flag - Bad Value for contract expense */
                if ((job1 == "1") && (j.Who == 2 || j.Who == 3) && (j.Tcon > 20000 || j.Tcon < 200))
                    f[9] = '1';
                else if ((job1 == "2") && (j.Who == 2 || j.Who == 3) && (j.Tcon > 20000 || j.Tcon < 50))
                    f[9] = '1';
                else if ((job1 == "B") && (j.Who == 2 || j.Who == 3) && (j.Tcon > 20000 || j.Tcon < 50))
                    f[9] = '1';
                else if ((job1 == "5") && (j.Who == 2 || j.Who == 3) && (j.Tcon > 20000 || j.Tcon < 50))
                    f[9] = '1';

                /*set flag 11 - Bad value for purchased material */
                if ((job1 == "1") && (j.Who == 2) && (j.Tamt > 0))
                    f[10] = '1';
                else if ((job1 == "2") && (j.Who == 2 || j.Who == 3) && (j.Tamt > 5000))
                    f[10] = '1';
                else if ((job1 == "3") && (j.Who == 2 || j.Who == 3) && (j.Tamt > 2000))
                    f[10] = '1';
                else if ((job1 == "5") && (j.Who == 2 || j.Who == 3) && (j.Tamt > 4000))
                    f[10] = '1';
                else if ((job1 == "1") && (j.Who == 1) && (j.Tamt > 4000 || j.Tamt < 1000))
                    f[10] = '1';
                else if (job1 == "2" && j.Who == 1 && j.Tamt > 6000)
                    f[10] = '1';
                else if (job1 == "B" && j.Who == 1 && j.Tamt > 5000)
                    f[10] ='1';
                else if (job1 == "5" && j.Who == 1 && j.Tamt > 5000)
                    f[10] = '1';
                else if (job1 == "1" && j.Who == 3 && (j.Tamt > 2000 || j.Tamt < 1000 ))
                    f[10] = '1';

                /* set flag12 - Bad value for rented material */
                if (j.Tren > 400)
                    f[11] = '1';

                /* set flag13 - Bad value for total job*/
                if ((job1 == "1") && (j.Who == 1) && (j.Tcost > 4000 || j.Tcost < 1000))
                    f[12] = '1';
                else if ((job1 == "2") && (j.Who == 1) && (j.Tcost > 10000 || j.Tcost < 10))
                    f[12] = '1';
                else if ((job1 == "3") && (j.Who == 1) && (j.Tcost > 4000 || j.Tcost < 10))
                    f[12] = '1';
                else if ((job1 == "5") && (j.Who == 1) && (j.Tcost > 6000 || j.Tcost < 5))
                    f[12] = '1';
                else if ((job1 == "1")  && (j.Who == 2 || j.Who == 3) && (j.Tcost > 25000 || j.Tcost < 50))
                    f[12] = '1';
                else if ((job1 == "2") && (j.Who == 2 || j.Who == 3) && (j.Tcost > 40000 || j.Tcost < 50))
                    f[12] = '1';
                else if ((job1 == "B") && (j.Who == 2 || j.Who == 3) && (j.Tcost > 30000 || j.Tcost < 50))
                    f[12] = '1';
                else if ((job1 == "5") && (j.Who == 2 || j.Who == 3) && (j.Tcost > 25000 || j.Tcost < 10))
                    f[12] = '1';
                else if (job3 =="316" && (j.Who == 2 || j.Who == 3) && (j.Tcost > 25000 || j.Tcost < 10))
                    f[12] = '1';

                /* set flag14 - Contract Job with no contract cost */
                if ((j.Who == 2 || j.Who == 3) && (j.Tcon == 0) && (j.Tcost > 0))
                    f[13] ='1';

                /*set flag15 - New construction may be out of scope */
                if (job1 == "1" && j.Tcost >= 0)
                    f[14] = '1';

                /*set flag16 - DIY job without material costs */
                if ((j.Who == 1 || j.Who == 3) && (j.Tamt == 0) && (j.Tren == 0) && (j.Tcon > 0))
                    f[15] = '1';

                /* set flag17 - No monthly Expense Indicator */
                if ((j.Con1 > 0 && j.Fcon1 == "O") || (j.Amt1 >0 && j.Famt1 == "O") || (j.Ren1 >0 && j.Fren1 == "O"))
                    f[16] = '1';
                
                    
                /*set flag18 - Large Expense relative to property value */
                if (j.Tcost != 0 && j.Tcost < 999999 && propval > 20000 && j.Tcost > propval * 0.10)
                    f[17] = '1';

                /*set flag19 - May be out of scope */
                if (job1 == "0")
                    f[18] = '1';

                /*find duplicated Jobidcode between current interview and previous interview*/
                bool prejob_found = false;
                if (pjlist.Count() != 0 && j.Tcost > 0)
                {
                    if (pjlist.Any(u => u.Jobidcode_collapse == j.Jobidcode_collapse && u.Tcost > 0)) { prejob_found = true; }

                }

                if (prejob_found)
                    f[19] = '1';

                /*find more case exist */
                var result = (from n in jlist
                              where n.Jobidcode_collapse.Substring(0,3) == j.Jobidcode_collapse.Substring(0,3)
                              select n.Jobidcode).ToList();

                if (result.Count>1)
                    f[20] = '1';

                string jobflag = "";

                /*Combine all flags into one string */
                for (int i = 0; i < f.Length; i++)
                {
                    jobflag += f[i];
                    if (f[i] =='1' && mf[i]!= f[i])
                        mf[i] = '1';                        
                }

                Ceflag cf = new Ceflag();
                cf.jobidcode = j.Jobidcode;
                cf.flagstr = jobflag;

                ceflaglist.Add(cf);
              
            }

            /*Combine all flags into one string */
            for (int i = 0; i < mf.Length; i++)
            {
                mainflag += mf[i];
            }


        }

        /*private ceflag structure */
        private class Ceflag
        {
            public string jobidcode { get; set; }
            public string flagstr { get; set; }
        }

    }

    
}
