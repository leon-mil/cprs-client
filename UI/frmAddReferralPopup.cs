/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmAddReferralPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/23/2015

Inputs:             ID, RESPID

Parameters:         None
                 
Outputs:	    None

Description:	    This screen will allow the user to add either project or respondent
                    Referrals

Detailed Design:    Detailed Design Referrals 

Other:	            Called From: frmReferral.cs
 
Revision History:	
***********************************************************************************
Modified Date :  5/24/2021
Modified By   :  Christine Zhang
Keyword       :  
Change Request:  CR#8240
Description   :  change referral type of late receipt to Dodge initial, PNR/Address 
                 to MF Intial
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
    public partial class frmAddReferralPopup : Form
    {
        private string Id;
        private string Respid;

        //Id and Respid obtained from the frmReferral.cs Screen

        public frmAddReferralPopup(string id, string respid)
        {
            InitializeComponent();
            Id = id;
            Respid = respid;
        }

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

        private void frmAddReferralPopup_Load(object sender, EventArgs e)
        {
            displayGrpbox();
        }

        private void displayGrpbox()
        {

            if (Respid == Id || Respid == "")
            {
                gbChooseType.Visible = false;
            }
            else
            {
                gbChooseType.Visible = true;
            }
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                gbChooseGrp.Visible = false;
            else
                gbChooseGrp.Visible = true;

        }

        //When user clicks the Respondent radio button in gbChooseType,
        //change the radiobuttons settings if respondent to display
        //only applicable referral type. Only change appearance.

        private void rbtnRespondent_Click(object sender, EventArgs e)
        {
            rbtnLate.Visible = false;
            rbtnLate.Enabled = false;
            rbtnCorrect.Visible = false;
            rbtnCorrect.Enabled = false;
            rbtnData.Location = new Point(173, 19);
            rbtnData.Checked = true;
            rbtnPnr.Location = new Point(318, 19);
            rbtnFree.Location = new Point(492, 19);
        }

        //When user clicks the Project radio button in gbChooseType,
        //change the radiobuttons settings back to the original

        private void rbtnProject_Click(object sender, EventArgs e)
        {
            rbtnLate.Visible = true;
            rbtnLate.Enabled = true;
            rbtnCorrect.Visible = true;
            rbtnCorrect.Enabled = true;
            rbtnLate.Location = new Point(60, 19);
            rbtnCorrect.Location = new Point(183, 19);
            rbtnData.Location = new Point(333, 19);
            rbtnPnr.Location = new Point(464, 19);
            rbtnFree.Location = new Point(598, 19);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //assign and save the groupbox radio button selections to variables

            string refCheckedButton = gbChooseRef.Controls.OfType<RadioButton>()
                           .FirstOrDefault(rb => rb.Checked == true).Text;

            string grpCheckedButton;
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                grpCheckedButton = "NPC Sup/Lead";
            else
                grpCheckedButton = gbChooseGrp.Controls.OfType<RadioButton>()
                           .FirstOrDefault(rb => rb.Checked == true).Text;

            ReferralData referral = new ReferralData();

            if (txtRemark.Text == "")
            {
                MessageBox.Show("Referral Note must be entered");
            }
            else
            {
                string new_comment = "";
                if (txtRemark.Text != "")
                    new_comment = txtRemark.Text;

                if (new_comment != "")
                {
                    string Reftype;
                    string Refstatus = "A"; //default value
                    string Refgroup;
                    string Prgdtm;
                    string Refnote;
                    string Usrnme = UserInfo.UserName;

                    DateTime dt = DateTime.Now;

                    Prgdtm = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);

                    Refnote = new_comment;

                    //Reassign the radio button values to their database equivalent

                    switch (refCheckedButton)
                    {
                        case "Dodge Initial": Reftype = "1"; break;
                        case "Correct Flags": Reftype = "2"; break;
                        case "Data Issue": Reftype = "3"; break;
                        case "MF Initial": Reftype = "4"; break;
                        case "Other": Reftype = "5"; break;
                        default: Reftype = "1"; break;
                    }

                    switch (grpCheckedButton)
                    {
                        case "HQ Supervisor": Refgroup = "1"; break;
                        case "HQ Analyst": Refgroup = "2"; break;
                        case "NPC Sup/Lead": Refgroup = "3"; break;
                        case "NPC Interviewer": Refgroup = "4"; break;
                        default: Refgroup = "1"; break;
                    }

                    //check if the project radio button is chosen or whether the
                    //groupbox is visible. If so, populate the 
                    //id field with the id.
                    //If the respondent radio button is chosen, populate the 
                    //respid fields with the respid

                    //add the project referral to the Project_Referral table
                    //add the respondent referral to the Respondent_Referral table
                    string Refuser = "";

                    if (rbtnProject.Checked || gbChooseType.Visible == false)
                    {
                        referral.AddProjectReferral(Id, Reftype, Refgroup, Refuser, Refstatus, Refnote, Usrnme, Prgdtm);
                    }
                    else
                    {
                        referral.AddRespondentReferral(Respid, Reftype, Refgroup, Refuser,Refstatus, Refnote, Usrnme, Prgdtm);
                    }

                }

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
