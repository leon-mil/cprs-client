/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmCeAddReferralPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/29/2015

Inputs:             ID

Parameters:	    None
                 
Outputs:            None

Description:	    This screen will allow the user to add Improvements Referrals

Detailed Design:    Detailed Design for Improvements Referrals

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
    public partial class frmCeAddReferralPopup : Form
    {
        private string Id;

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

        //Id comes from the frmImpReferral.cs Screen

        public frmCeAddReferralPopup(string id)
        {
            InitializeComponent();
            Id = id;
        }

        private void frmAddReferralPopup_Load(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //assign and save the groupbox radio button selections to variables

            string refCheckedButton = gbChooseRef.Controls.OfType<RadioButton>()
                           .FirstOrDefault(rb => rb.Checked == true).Text;
            string grpCheckedButton = gbChooseGrp.Controls.OfType<RadioButton>()
                           .FirstOrDefault(rb => rb.Checked == true).Text;

            ImpReferralData referral = new ImpReferralData();

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
                        case "Correct Flags": Reftype = "2"; break;
                        case "Data Issue": Reftype = "3"; break;
                        case "Free Form": Reftype = "5"; break;
                        default: Reftype = "1"; break;
                    }

                    switch (grpCheckedButton)
                    {
                        case "HQ Supervisor": Refgroup = "1"; break;
                        case "HQ Analyst": Refgroup = "2"; break;
                        default: Refgroup = "1"; break;
                    }

                    //add the referral to the CeReferral table

                    referral.AddCEReferral(Id, Reftype, Refgroup, Refstatus, Refnote, Usrnme, Prgdtm);
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
