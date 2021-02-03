/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmCeUpdReferralPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/23/2015

Inputs:             Id, Reftype, Refstatus,
                    Refgroup, Refcase, Refnote, Prgdtm, Usrnme

Parameters:	    None
                 
Outputs:	    None

Description:	    This screen will allow the user to update Improvements Referrals

Detailed Design:    Detailed Design for Improvement Referrals

Other:	            Called From: frmImpReferral.cs
 
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
    public partial class frmCeUpdReferralPopup : Form
    {
        private string id;
        private string reftype;
        private string refstatus;
        private string refgroup;
        private string usrnme;
        private string refnote;
        private string prgdtm;

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

        //update variables are obtained from the frmImpReferral.cs Screen

        public frmCeUpdReferralPopup(string Id, string Reftype, string Refstatus,
                                   string Refgroup, string Refnote, string Prgdtm, string Usrnme)
        {
            InitializeComponent();

            id = Id;
            reftype = Reftype;
            refgroup = Refgroup;
            refstatus = Refstatus;
            refnote = Refnote;
            usrnme = Usrnme;
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
        }

        //Make the referral note text field editable only if the user
        //accessing the screen is the note's owner or a Programmer or HQ Analyst Supervisor.   
        //otherwise the field is read only.

        private void validateUser()
        {

            if ((usrnme == UserInfo.UserName) ||
               (usrnme != UserInfo.UserName &&
               (UserInfo.GroupCode == EnumGroups.HQManager ||
               UserInfo.GroupCode == EnumGroups.Programmer)))
            {
                txtRemark.ReadOnly = false;
            }
            else
            {
                txtRemark.ReadOnly = true;
            }
        }

        //When a user clicks into the referral note text field
        //the user will get a messagebox if the user accessing the screen is not
        //1. A programmer
        //2. The note's owner
        //3. An HQ Analyst Supervisor if the note's owner is an HQ Analyst.
        //otherwise the field user can click into the textbox and update the field.

        private void txtRemark_Click(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                UserInfo.GroupCode == EnumGroups.HQManager)
            {
                if ((usrnme != UserInfo.UserName &&
                    UserInfo.GroupCode != EnumGroups.HQManager))
                {
                    MessageBox.Show(usrnme + " or an HQ supervisor can update this note. You may only update the status.");
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //assign and save the status groupbox radio button selection to variable

            string statusCheckedButton = gbChooseStatus.Controls.OfType<RadioButton>()
                          .FirstOrDefault(rb => rb.Checked == true).Text;

            ImpReferralData referral = new ImpReferralData();

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

                    DateTime dt = DateTime.Now;

                    Prgdtm = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);

                    Refnote = new_comment;

                    //Reassign the radio button values to their database equivalent

                    switch (statusCheckedButton)
                    {
                        case "Active": refstatus = "A"; break;
                        case "Complete": refstatus = "C"; break;
                        default: refstatus = "A"; break;
                    }

                    switch (reftype)
                    {
                        case "Correct Flags": reftype = "2"; break;
                        case "Data Issue": reftype = "3"; break;
                        case "Free Form": reftype = "5"; break;
                    }

                    switch (refgroup)
                    {
                        case "HQ Supervisor": refgroup = "1"; break;
                        case "HQ Analyst": refgroup = "2"; break;
                    }

                    //Allow update to the improvements referral in the CeReferral table
                    //if the user accessing the screen is:
                    //1. A programmer
                    //2. The note's owner
                    //3. An HQ Analyst Supervisor if the note's owner is an HQ Analyst.
                    //otherwise the user can only update the refstatus field.

                    if (UserInfo.GroupCode == EnumGroups.Programmer)
                    {
                        referral.UpdateCEReferral(id, reftype, refgroup, refstatus, prgdtm, Refnote);
                    }

                    else if (UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                        UserInfo.GroupCode == EnumGroups.HQManager)
                    {
                        if ((usrnme == UserInfo.UserName) ||
                            (usrnme != UserInfo.UserName &&
                            UserInfo.GroupCode == EnumGroups.HQManager))
                        {
                            referral.UpdateCEReferral(id, reftype, refgroup, refstatus, prgdtm, Refnote);
                        }
                        else if ((usrnme != UserInfo.UserName) ||
                            (usrnme != UserInfo.UserName &&
                            UserInfo.GroupCode != EnumGroups.HQManager))
                        {
                            referral.UpdateCEStatusReferral(id, reftype, refgroup, prgdtm, refstatus);
                        }
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
