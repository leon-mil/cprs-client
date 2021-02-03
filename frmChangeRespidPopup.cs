/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmChangeRespidPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      06/15/2016

Inputs:             Masterid

Parameters:		    None
                 
Outputs:		    Respid

Description:	    This screen will allow the user to add a respid to the presample
 * table or unlink a presample case's existing respid

Detailed Design:    Detailed User Requirements for Multifamily Initial Address Screen 

Other:	            Called from: frmMfInitial.cs
 
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
    public partial class frmChangeRespidPopup : Form
    {
        /* Public */
        public string Respid;
        public string NewRespid;
        public string Id; //id used to break respondent link

        private string oldRespid;
        private string resplocked_by;

        MfInitialData mfidata = new MfInitialData();

        public frmChangeRespidPopup()
        {
            InitializeComponent();
            txtRespid.SelectionLength = txtRespid.Text.Length;
            this.ActiveControl = txtRespid;
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

        private void frmChangeRespidPopup_Load(object sender, EventArgs e)
        {
            //Hide the Delete radio button if the case does not
            //have a RESPID
            if (string.IsNullOrWhiteSpace(Respid))
                rbtnDelete.Visible = false;

            else
                this.ActiveControl = txtRespid;
        }

        private void txtRespid_TextChanged(object sender, EventArgs e)
        {
            NewRespid = txtRespid.Text.ToString().Trim();
            oldRespid = Respid;
        }

        //Check if the RESPID is locked, if so, give a messagebox and restore the old value

        private void CheckLocked()
        {
            resplocked_by = GeneralDataFuctions.ChkRespIDIsLocked(txtRespid.Text.Trim());

            string lck = "";

            if ((resplocked_by != ""))
            {
                if (resplocked_by != "")
                    lck = resplocked_by;

                DialogResult dialogResult = MessageBox.Show("This respondent is locked by " + lck + ", and cannot be assigned.", "Invalid Entry!", MessageBoxButtons.OK);

                if (dialogResult == DialogResult.OK)
                {
                    txtRespid.Text = oldRespid;
                    this.ActiveControl = txtRespid;
                    txtRespid.Focus();
                    this.DialogResult = DialogResult.None;

                }
            }
            else
            {
                NewRespid = txtRespid.Text;
            }
        }

        //Release the respondent lock

        private void relsRespondentLock()
        {
            resplocked_by = GeneralDataFuctions.ChkRespIDIsLocked(Respid);
            if (!String.IsNullOrEmpty(Respid))
            {
                string locked_by = "";

                //Only unlock if locked by user. Cannot unlock another user's lock.
                if (resplocked_by == UserInfo.UserName)
                {
                    GeneralDataFuctions.UpdateRespIDLock(Respid, locked_by);
                }
            }
        }

        //Only allow digits in the respid field

        private void txtRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //If the Delete radio button is selected:

            NewRespid = txtRespid.Text;

            if (rbtnDelete.Checked)
            {
                DialogResult dialogResult = MessageBox.Show("This will unlink this respid from this presample case. Continue?", "Question", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    if (oldRespid != "") //if the respid is not blank
                    {
                        NewRespid = "";
                        string respid = "";
                        //set the respid to blank in the presample table
                        mfidata.BreakRespLink(respid, Id);
                        relsRespondentLock(); //release the lock in the respondent table
                    }
                }
                else
                {
                    txtRespid.Focus();
                    this.DialogResult = DialogResult.None;
                }
            }

            //If the Add radio button is selected:

            if (rbtnAdd.Checked)
            {
                if (txtRespid.TextLength < 7)
                {
                    MessageBox.Show("You have entered a Respondent id with less than 7 digits");
                    txtRespid.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    if (!GeneralDataFuctions.ChkTrueRespid(txtRespid.Text))
                    {
                        MessageBox.Show("You entered an invalid RespID");

                        txtRespid.Text = oldRespid;
                        this.DialogResult = DialogResult.None;
                        this.ActiveControl = txtRespid;
                        txtRespid.Focus();
                    }
                    else
                    {
                        if (txtRespid.Text != "")
                        {
                            //If the user enters a respid that is already assigned to the case
                            //give a message

                            string presp = mfidata.GetPresampleRespid(Id);

                            if (presp == txtRespid.Text)
                            {
                                MessageBox.Show("This Respid is already assigned to this case");
                                txtRespid.Focus();
                                this.DialogResult = DialogResult.None;
                            }
                            else
                            {
                                CheckLocked();
                            }
                        }
                    }
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Places the cursor into the first position when the textbox is entered

        private void txtRespid_Enter(object sender, EventArgs e)
        {
            txtRespid.SelectionStart = 0;
            txtRespid.SelectionLength = txtRespid.Text.Length;
            txtRespid.Focus();
            txtRespid.Select(txtRespid.SelectionStart, txtRespid.SelectionLength);
        }

        private void txtRespid_MouseClick(object sender, MouseEventArgs e)
        {
            txtRespid.SelectionStart = 0;
            txtRespid.SelectionLength = txtRespid.Text.Length;
            txtRespid.Select(txtRespid.SelectionStart, txtRespid.SelectionLength);
        }

        //When the Delete radio button is clicked, hide the "Enter Respid:"
        //label and hide the respid textbox
        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            txtRespid.Visible = false;
        }

        private void rbtnAdd_Click(object sender, EventArgs e)
        {
            label1.Visible = true;
            txtRespid.Visible = true;
        }
    }
}

