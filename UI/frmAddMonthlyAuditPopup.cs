/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmAddMonthlyAuditPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/20/2016
Inputs:             None
Parameters:		    None                
Outputs:		    None
Description:	    This program displays the auditrvw data
                   
Detailed Design:    Detailed User Requirements for Admin 

Other:	            Called from: frmMonthAuditReviewPopup.cs
 
Revision History:	
***********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using CprsBLL;

namespace Cprs
{
    public partial class frmAddMonthlyAuditPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmAddMonthlyAuditPopup()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string currmonth;
        private bool new_record = false;
        private MonthlyAuditReviewData mdata;

        private void frmAddMonthlyAuditPopup_Load(object sender, EventArgs e)
        {
            //get current month
            currmonth = DateTime.Now.Date.ToString("yyyyMM");
            lblTitle.Text = currmonth + " Current Review";

            string activity = "";
            string comment = "";
            mdata = new MonthlyAuditReviewData();

            mdata.GetCurrentMonthAuditReview(currmonth, ref activity, ref comment);
            if (activity == "")
            {
                activity = "N";
                new_record = true;
            }
            if (activity == "N")
            {
                rdNormal.Checked = true;
                rdUnusual.Checked = false;
                txtComment.Enabled = false;
            }
            else
            {
                rdNormal.Checked = false;
                rdUnusual.Checked = true;
                if (comment != "")
                    txtComment.Text = comment;
                txtComment.Enabled = true;
            }
               
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (rdUnusual.Checked == true && txtComment.Text.Trim().Length==0)
            {
                MessageBox.Show("Comment is required.");
                return;
            }

            string act;
            if (rdNormal.Checked)
                act = "N";
            else
                act = "U";

            if (new_record)
                mdata.AddCurrentAuditData(currmonth, act, txtComment.Text.Trim());
            else
                mdata.UpdateCurrentAuditData(currmonth, act, txtComment.Text.Trim());

            
            sendEmail();

            this.Close();

        }

        private void rdUnusual_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUnusual.Checked)
            {
                rdNormal.Checked = false;
                txtComment.Enabled = true;
            }
        }

        private void rdNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNormal.Checked)
            {
                rdUnusual.Checked = false;
                txtComment.Enabled = false;
                txtComment.Text = "";
            }
        }

        //send email
        private void sendEmail()
        {
            string suj = "CPR Monthly Audit Review was Completed";
            string mbody;
            if (rdNormal.Checked)
                mbody = "Normal Activity.";
            else
                mbody= "Unusual Activity." + System.Environment.NewLine + txtComment.Text;
            string from = UserInfo.UserName + "@census.gov";

            List<string> tolist = new List<string>();
            tolist = GeneralDataFuctions.GetJobEmails("USR3");
            if (tolist.Count > 0)
                GeneralFunctions.SendEmail(suj, mbody, from, tolist);
        }
    }
}
