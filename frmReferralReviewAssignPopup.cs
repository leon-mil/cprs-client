/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmReferralReviewAssignPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      5/11/2021
Inputs:             Id, Respid, Reftype, Refstatus, Refuser
                    Refgroup, Refcase, Refnote, Prgdtm, Usrnme

Parameters:	    None                
Outputs:	    None
Description:	    This screen will allow the user to assign NPC clarks either project or respondent
                    Referrals

Detailed Design:    Detailed Design for Referrals

Other:	            Called From: frmReferralReview.cs
 
Revision History:	
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
namespace Cprs
{
    public partial class frmReferralReviewAssignPopup : Form
    {
        public frmReferralReviewAssignPopup()
        {
            InitializeComponent();
        }

        private string id;
        private string respid;
        private string reftype;
        private string refstatus;
        private string refgroup;
        private string refcase;
        
        private string refnote;
        private string prgdtm;
       
        private string refuser;

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

        private void frmReferralReviewAssignPopup_Load(object sender, EventArgs e)
        {
            lblCase.Text = refcase;
            txtType.Text = reftype;
            txtGroup.Text = refgroup;
            ReferralData referral = new ReferralData();
            DataTable dt = referral.GetNPCClarksList();

            cbclerks.DataSource = dt;
            cbclerks.DisplayMember = "usrnme";
            cbclerks.ValueMember = "usrnme";
            if (refuser.Trim() != "")
                cbclerks.SelectedValue = refuser;
            cbclerks.Focus();
        }
       
        public frmReferralReviewAssignPopup(string Id, string Reftype, string Refgroup, string Refuser, string Refstatus, string Prgdtm, string Refnote, string Refcase)
        {
            InitializeComponent();
            if (Refcase == "PROJECT")
                id = Id;
            else 
                respid = Id;
            reftype = Reftype;
            refgroup = Refgroup;
            refstatus = Refstatus;
            refcase = Refcase;
            prgdtm = Prgdtm;
            refuser = Refuser;
            refnote = Refnote;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (cbclerks.SelectedIndex > 0)
            {
                refgroup = "4";
                refuser = cbclerks.Text;

                string rtype = "";
                if (reftype == "Late Receipt")
                    rtype = "1";
                else if (reftype == "Correct Flags")
                    rtype = "2";
                else if (reftype == "Data Issue")
                    rtype = "3";
                else if (reftype == "PNR/Address")
                    rtype = "4";
                else
                    rtype = "5";

                ReferralData referral_data = new ReferralData();


                //update the project referral in the Project_Referral table
                //update the respondent referral in the Respondent_Referral table

                if (refcase == "PROJECT")
                {
                    referral_data.UpdateProjectReferral(id, rtype, refgroup, refuser, refstatus.Substring(0, 1), prgdtm, refnote);
                }

                else if (refcase == "RESPONDENT")
                {
                    referral_data.UpdateRespondentReferral(respid, rtype, refgroup, refuser, refstatus.Substring(0, 1), prgdtm, refnote);
                }

            }
           
        }
    }
}
