/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmUpdReferralPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/29/2015

Inputs:             Id, Respid, Reftype, Refstatus,
                    Refgroup, Refcase, Refnote, Prgdtm, Usrnme

Parameters:	    None
                 
Outputs:	    None

Description:	    This screen will allow the user to update either project or respondent
                    Referrals

Detailed Design:    Detailed Design for Referrals

Other:	            Called From: frmReferral.cs
 
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
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmUpdReferralPopup : Form
    {
        private string id;
        private string respid;
        private string reftype;
        private string refstatus;
        private string refgroup;
        private string refcase;
        private string usrnme;
        private string refnote;
        private string prgdtm;
        private string owngrpcde;

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

        //update variables are obtained from the frmReferral.cs Screen

        public frmUpdReferralPopup(string Id, string Respid, string Reftype, string Refstatus, string Refgroup, string Refcase, string Refnote, string Prgdtm, string Usrnme, string Grpcde)
        {
            InitializeComponent();

            id = Id;
            respid = Respid;
            reftype = Reftype;
            refgroup = Refgroup;
            refstatus = Refstatus;
            refcase = Refcase;
            refnote = Refnote;
            usrnme = Usrnme;
            owngrpcde = Grpcde;
            prgdtm = Prgdtm;
        }

        private void frmUpdReferral_Load(object sender, EventArgs e)
        {
            DisplayReferral();
            validateUser();
        }

        //display the values transferred from the frmImpReferral Screen

        private void DisplayReferral()
        {
            lblCase.Text = refcase;
            txtType.Text = reftype;
            txtGroup.Text = refgroup;
            txtRemark.Text = refnote;

            //check the radio button based on the transferred value

            if (refstatus == "Active")
            {
                rbtnActive.Checked = true;
            }
            else if (refstatus == "Complete")
            {
                rbtnComplete.Checked = true;
            }
            else
            {
                rbtnPend.Checked = true;
            }
        }

        //if the note does not belong to the current user 
        //and the current user is a HQSupport, HQMathStat
        //or NPCInterviewer then
        //Disabled the textbox and status grpbox

        private void validateUser()
        {
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                rbtnComplete.Enabled = false;
                return;
            }

            if (usrnme == UserInfo.UserName ||
                (usrnme != UserInfo.UserName &&
                  (!(UserInfo.GroupCode == EnumGroups.HQSupport ||
                   UserInfo.GroupCode == EnumGroups.HQMathStat ||
                   UserInfo.GroupCode == EnumGroups.NPCInterviewer)))
                )
            {
                txtRemark.ReadOnly = false;
                gbChooseStatus.Enabled = true;
                btnSave.Enabled = true;
            }
            else
            {
                txtRemark.ReadOnly = true;
                gbChooseStatus.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //assign and save the status groupbox radio button selection to variable

            string statusCheckedButton = gbChooseStatus.Controls.OfType<RadioButton>()
                          .FirstOrDefault(rb => rb.Checked == true).Text;

            ReferralData referral = new ReferralData();

            if (txtRemark.Text == "")
            {
                MessageBox.Show("Referral Note cannot be blank");
            }
            else
            {
                string new_comment = "";

                if (txtRemark.Text != "")
                    new_comment = txtRemark.Text;

                if (new_comment != "")
                {

                    string refstatus;

                    string Prgdtm;
                    string Refnote;
                    string Usrnme = UserInfo.UserName;

                    //DateTime dt = DateTime.Now;

                    Prgdtm = DateTime.Now.ToString();

                    Refnote = new_comment;

                    //Reassign the radio button values to their database equivalent
                    //Although reftype and refgroup are not updated, they are still
                    //included for comparison to the note in the database
                    //in order to ensure that the correct note
                    //is updated since an ID or RESPID can have multiple notes

                    switch (statusCheckedButton)
                    {
                        case "Active": refstatus = "A"; break;
                        case "Complete": refstatus = "C"; break;
                        case "Pending": refstatus = "P"; break;
                        default: refstatus = "A"; break;
                    }

                    switch (reftype)
                    {
                        case "Late Receipt": reftype = "1"; break;
                        case "Correct Flags": reftype = "2"; break;
                        case "Data Issue": reftype = "3"; break;
                        case "PNR/Address": reftype = "4"; break;
                        case "Other": reftype = "5"; break;
                    }

                    switch (refgroup)
                    {
                        case "HQ Supervisor": refgroup = "1"; break;
                        case "HQ Analyst": refgroup = "2"; break;
                        case "NPC Supervisor": refgroup = "3"; break;
                        case "NPC Interviewer": refgroup = "4"; break;
                    }

                    //update the project referral in the Project_Referral table
                    //update the respondent referral in the Respondent_Referral table

                    if (refcase == "PROJECT")
                    {
                        referral.UpdateProjectReferral(id, reftype, refgroup, refstatus, prgdtm, Refnote);
                    }

                    else if (refcase == "RESPONDENT")
                    {
                        referral.UpdateRespondentReferral(respid, reftype, refgroup, refstatus, prgdtm, Refnote);
                    }
                }

                this.Refresh();
                this.Dispose();
            }
        }

        //return to the previous screen without saving the unprocessed referral.

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
