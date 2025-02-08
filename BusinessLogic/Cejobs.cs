/**********************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Cejobs.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2015
Inputs:             None
Parameters:	        id
Outputs:	        cejobs, cejob, DisplayAppliance, DisplayCejob
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen
                    the class include cejobs, cejob, DisplayAppliance, DisplayCejob
Detailed Design:    None 
Other:	            Called By: frmImprovement
 
Revision History:	
*********************************************************************
 Modified Date  :  3/6/2024
 Modified By    :  Christine Zhang
 Keyword        :  20240306cz
 Change Request :  CR 1434
 Description    :  Add jobidcode, replace detcode with jobidcode
*********************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Cejobs
    {
        public Cejobs (string passed_id )
        {
            Id = passed_id;
        }

        /*****************public properties ************************/
        public string Id { get; private set; }
        public List<Cejob> cejoblist = new List<Cejob>();

        /*****************public methods **************************/

        /*Find cejobs for previous interview */
        public List<Cejob> GetPreviousCejobs(string curr_interview)
        {
            //List<Cejob> precejoblist =;
            IEnumerable<Cejob> jobQuery =
            from cjob in cejoblist
            where Convert.ToInt16(cjob.Interview) < Convert.ToInt32(curr_interview)
            select cjob;

            return jobQuery.ToList();

        }

        /*Get interview list for the MCD */
        public List<string> GetInterviewlist()
        {
            List<string> distinctInterview = cejoblist
                            .GroupBy(p => p.Interview)
                            .Select(g => g.First().Interview)
                            .ToList();

            return distinctInterview;
        }

        /*Get jobcode list for an interview of the case */
        /*20240306cz change detcode to jobidcode */
        public List<string> GetJobcodelist(string interview)
        {
            var jobcodelist =
            from job in cejoblist
            where job.Interview == interview && job.Dflag != Dirty.delete
            select job.Jobidcode;

            return jobcodelist.ToList();
        }


        /*Get cejobs list for the interview */
        public List<Cejob> GetJobsForInterview(string interview)
        {
            IEnumerable<Cejob> jobs =
            from job in cejoblist
            where job.Interview  == interview
            select job;

            return jobs.ToList();
        }

        /*Get cejobs value for the interview */
        public int GetJobsValueForInterview(string interview)
        {
            IEnumerable<Cejob> jobs =
            from job in cejoblist
            where job.Interview == interview && job.Dflag != Dirty.delete
            select job;

            var sum =0;
            if (jobs.Count() !=0)
                sum = jobs.Sum(x => x.Tcost);

            return sum;
        }

        /*Get cejob for the interview and detcode */
        /*20240306cz change detcode to jobidcode */
        public Cejob GetJobForInterviewJobcode(string interview, string jobidcode)
        {
          
            Cejob cj = (from Cejob j in cejoblist
                        where j.Interview == interview && j.Jobidcode == jobidcode
                        select j).SingleOrDefault();
           
            return cj;
        }

        /*check whether cejobs get modified or not */
        public bool IsModified()
        {
            bool is_modified = false;
            foreach (Cejob job in cejoblist) 
            {
                if (job.Dflag != Dirty.initial)
                {
                    is_modified = true;
                    break;
                }
            }

            return is_modified;
        }

    }

    public enum Dirty { initial = 0,  modify = 1, delete = 2 };

    /********************Cejob structure ************************************/
    public class Cejob
    {
        /*Construction of Cejob */
        public Cejob (string passed_interview, string passed_jobidcode )
        {
            interview = passed_interview;
            jobidcode = passed_jobidcode;

            jobidcode_collapse = jobidcode;
            string jc = jobidcode.Substring(0, 3);
            if (jc == "111" || jc == "199" || jc == "212" || jc == "215" || jc == "219" || jc == "221" || jc == "230" || jc == "299" || jc == "316")
                jobidcode_collapse = "GPA9";
            else if (jc == "416" || jc == "419" || jc == "426" || jc == "430" || jc == "499")
                jobidcode_collapse = "GPB9";
            else if (jc == "323" || jc == "326" || jc == "399")
                jobidcode_collapse = "GPC9";
        }

        /*interview property */
        private string interview;
        public string Interview
        {
            get { return interview; }
        }

        /*detcode property */
        private string detcode = null;
        public string Detcode
        {
            get { return detcode; }
        }

        /*jobidcode property */
        private string jobidcode;
        public string Jobidcode
        {
            get { return jobidcode; }
        }

        /*jobidcode property */
        private string jobidcode_collapse;
        public string Jobidcode_collapse
        {
            get { return jobidcode_collapse;}
        }

        /*jobcode property */
        private string jobcod;
        public string Jobcod
        {
            get { return jobcod; }
            set {jobcod = value; }
        }

        /*wrkdesc property */
        private string wrkdesc;
        public string Wrkdesc
        {
            get { return wrkdesc; }
            set {wrkdesc = value; }
            
        }

        /*addinfo property */
        private string addinfo;
        public string Addinfo
        {
            get { return addinfo; }
            set { addinfo = value; }
        }

        /*propno property */
        private string propno;
        public string Propno
        {
            get { return propno; }
            set { propno = value; }
        }

        /*perbus property */
        private int perbus;
        public int Perbus
        {
            get { return perbus; }
            set { perbus = value; }
        }

        /*who property */
        private int who;
        public int Who
        {
            get { return who; }
            set { who = value; }
        }

        /*tcost property */
        private int tcost;
        public int Tcost
        {
            get { return (con1+con2+con3+con4+amt1+amt2+amt3+amt4+ren1+ren2+ren3+ren4); }
            set { tcost = value; }
        }

        /*TEQP property */

        public int Teqp;
        
        /*ismodified property */
        private Dirty  dflag = Dirty.initial;
        public Dirty Dflag
        {
            get { return dflag; }
            set { dflag = value; }
        }

        /*con1 property */
        private int con1;
        public int Con1
        {
            get { return con1; }
            set
            {
                if (value != this.con1)
                {
                    this.con1 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*con2 property */
        private int con2;
        public int Con2
        {
            get { return con2; }
            set
            {
                if (value != this.con2)
                {
                    this.con2 = value;
                    this.dflag = Dirty.modify;
                }
            }
            
        }

        /*con3 property */
        private int con3;
        public int Con3
        {
            get { return con3; }
            set
            {
                if (value != this.con3)
                {
                    this.con3 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*con4 property */
        private int con4;
        public int Con4
        {
            get { return con4; }
            set
            {
                if (value != this.con4)
                {
                    this.con4 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*tcon property */
        public int Tcon
        {
            get { return con1+con2+con3+con4; }
        }

        /*amt1 property */
        private int amt1;
        public int Amt1
        {
            get { return amt1; }
            set
            {
                if (value != this.amt1)
                {
                    this.amt1 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*amt2 property */
        private int amt2;
        public int Amt2
        {
            get { return amt2; }
            set
            {
                if (value != this.amt2)
                {
                    this.amt2 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*amt3 property */
        private int amt3;
        public int Amt3
        {
            get { return amt3; }
            set
            {
                if (value != this.amt3)
                {
                    this.amt3 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*amt4 property */
        private int amt4;
        public int Amt4
        {
            get { return amt4; }
            set
            {
                if (value != this.amt4)
                {
                    this.amt4 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*tamt property */
        public int Tamt
        {
            get { return amt1 + amt2 + amt3 + amt4; }
        }

        /*ren1 property */
        private int ren1;
        public int Ren1
        {
            get { return ren1; }
            set
            {
                if (value != this.ren1)
                {
                    this.ren1 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*ren2 property */
        private int ren2;
        public int Ren2
        {
            get { return ren2; }
            set
            {
                if (value != this.ren2)
                {
                    this.ren2 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*ren3 property */
        private int ren3;
        public int Ren3
        {
            get { return ren3; }
            set
            {
                if (value != this.ren3)
                {
                    this.ren3 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*ren4 property */
        private int ren4;
        public int Ren4
        {
            get { return ren4; }
            set
            {
                if (value != this.ren4)
                {
                    this.ren4 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*tren property */      
        public int Tren
        {
            get { return ren1 + ren2 + ren3 + ren4; }
        }

        /*eqpcode1 property */
        private string eqpcode1;
        public string Eqpcode1
        {
            get { return eqpcode1; }
            set { eqpcode1 = value; }
        }

        /*eqp1 property */
        private int eqp1;
        public int Eqp1
        {
            get { return eqp1; }
            set
            {
                if (value != this.eqp1)
                {
                    this.eqp1 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*eqpcode2 property */
        private string eqpcode2;
        public string Eqpcode2
        {
            get { return eqpcode2; }
            set { eqpcode2 = value; }
        }

        /*eqp2 property */
        private int eqp2;
        public int Eqp2
        {
            get { return eqp2; }
            set
            {
                if (value != this.eqp2)
                {
                    this.eqp2 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*eqpcode3 property */
        private string eqpcode3;
        public string Eqpcode3
        {
            get { return eqpcode3; }
            set { eqpcode3 = value; }
        }

        /*eqp3 property */
        private int eqp3;
        public int Eqp3
        {
            get { return eqp3; }
            set
            {
                if (value != this.eqp3)
                {
                    this.eqp3 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*eqpcode4 property */
        private string eqpcode4;
        public string Eqpcode4
        {
            get { return eqpcode4; }
            set { eqpcode4 = value; }
        }

        /*eqp4 property */
        private int eqp4;
        public int Eqp4
        {
            get { return eqp4; }
            set
            {
                if (value != this.eqp4)
                {
                    this.eqp4 = value;
                    this.dflag = Dirty.modify;
                }
            }
          
        }

        /*eqpcode5 property */
        private string eqpcode5;
        public string Eqpcode5
        {
            get { return eqpcode5; }
            set { eqpcode5 = value; }
        }

        /*eqp5 property */
        private int eqp5=0;
        public int Eqp5
        {
            get { return eqp5; }
            set
            {
                if (value != this.eqp5)
                {
                    this.eqp5 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*eqpcode6 property */
        private string eqpcode6;
        public string Eqpcode6
        {
            get { return eqpcode6; }
            set { eqpcode6 = value; }
        }

        /*eqp6 property */
        private int eqp6;
        public int Eqp6
        {
            get { return eqp6; }
            set
            {
                if (value != this.eqp6)
                {
                    this.eqp6 = value;
                    this.dflag = Dirty.modify;
                }
            }
        }

        /*tapp property */
        public int Tapp
        {
            get { return eqp1 + eqp2 + eqp3 + eqp4 + eqp5 +eqp6; }
        }

        /*fcon1 property */
        private string fcon1 = "B";
        public string Fcon1
        {
            get { return fcon1; }
            set { fcon1 = value; }
        }

        /*fcon2 property */
        private string fcon2 = "B";
        public string Fcon2
        {
            get { return fcon2; }
            set { fcon2 = value; }
        }

        /*fcon3 property */
        private string fcon3 = "B";
        public string Fcon3
        {
            get { return fcon3; }
            set { fcon3 = value; }
        }

        /*fcon4 property */
        private string fcon4 = "B";
        public string Fcon4
        {
            get { return fcon4; }
            set { fcon4 = value; }
        }

        /*famt1 property */
        private string famt1 = "B";
        public string Famt1
        {
            get { return famt1; }
            set { famt1 = value; }
        }

        /*famt2 property */
        private string famt2 = "B";
        public string Famt2
        {
            get { return famt2; }
            set { famt2 = value; }
        }

        /*famt3 property */
        private string famt3 = "B";
        public string Famt3
        {
            get { return famt3; }
            set { famt3 = value; }
        }

        /*famt4 property */
        private string famt4 = "B";
        public string Famt4
        {
            get { return famt4; }
            set { famt4 = value; }
        }

        /*fren1 property */
        private string fren1= "B";
        public string Fren1
        {
            get { return fren1; }
            set { fren1 = value; }
        }

        /*fren2 property */
        private string fren2="B";
        public string Fren2
        {
            get { return fren2; }
            set { fren2 = value; }
        }

        /*fren3 property */
        private string fren3= "B";
        public string Fren3
        {
            get { return fren3; }
            set { fren3 = value; }
        }

        /*fren4 property */
        private string fren4 = "B";
        public string Fren4
        {
            get { return fren4; }
            set { fren4 = value; }
        }

        /*feqp1 property */
        private string feqp1 = "B";
        public string Feqp1
        {
            get { return feqp1; }
            set { feqp1 = value; }
        }

        /*feqp2 property */
        private string feqp2 = "B";
        public string Feqp2
        {
            get { return feqp2; }
            set { feqp2 = value; }
        }

        /*feqp3 property */
        private string feqp3 = "B";
        public string Feqp3
        {
            get { return feqp3; }
            set { feqp3 = value; }
        }

        /*feqp4 property */
        private string feqp4 = "B";
        public string Feqp4
        {
            get { return feqp4; }
            set { feqp4 = value; }
        }

        /*feqp5 property */
        private string feqp5 = "B";
        public string Feqp5
        {
            get { return feqp5; }
            set { feqp5 = value; }
        }

        /*feqp6 property */
        private string feqp6 = "B";
        public string Feqp6
        {
            get { return feqp6; }
            set { feqp6 = value; }
        }
       
    }

    /*************** DisplayCejob class ***************/
    public class DisplayCejob
    {
        public DisplayCejob()
        {          
        }

        /*cost property */
        private string cost="";
        public string Cost
        {
            get { return cost; }
            set { cost = value; }
        }

        /*Survey value property */
        private long sv1=0;
        public long Sv1
        {
            get { return sv1; }
            set { sv1 = value; }
        }

        /*Survey value flag property */
        private string fsv1="";
        public string Fsv1
        {
            get { return fsv1; }
            set { fsv1 = value; }
        }

        /*Survey value property */
        private long sv2=0;
        public long Sv2
        {
            get { return sv2; }
            set { sv2 = value; }
        }

        /*Survey value flag property */
        private string fsv2="";
        public string Fsv2
        {
            get { return fsv2; }
            set { fsv2 = value; }
        }

        /*Survey value property */
        private long sv3=0;
        public long Sv3
        {
            get { return sv3; }
            set { sv3 = value; }
        }

        /*Survey value flag property */
        private string fsv3="";
        public string Fsv3
        {
            get { return fsv3; }
            set { fsv3 = value; }
        }

        /*Survey value property */
        private long sv4=0;
        public long Sv4
        {
            get { return sv4; }
            set { sv4 = value; }
        }

        /*Survey value flag property */
        private string fsv4="";
        public string Fsv4
        {
            get { return fsv4; }
            set { fsv4 = value; }
        }

        /*Previous survey value property */
        private int psv=0;
        public int Psv
        {
            get { return psv; }
            set { psv = value; }
        }

        /*Survey value flag property */
        private string fpsv="";
        public string Fpsv
        {
            get { return fpsv; }
            set { fpsv = value; }
        }

    }

    /*************** DisplayAppliance class ***************/
    public class DisplayAppliance
    {
        public DisplayAppliance()
        {
        }

        private int seq;
        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        private string code;
        public string Code
        {
            get { return code; }
            set { code = value; }
        }

        private int expense;
        public int Expense
        {
            get { return expense; }
            set { expense = value; }
        }

        private string flag="B";
        public string Flag
        {
            get { return flag; }
            set { flag = value; }
        }

    }
}
