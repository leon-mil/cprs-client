/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmRespidEditPopup.cs

 Programmer    : Srini Natarajan

 Creation Date : 6/14/2016

 Inputs        : Respid

 Paramaters    : Respid
 
 Output        : Takes the Respid as input and creates New/updates existind/deletes respid on the Name and Address screen.
 Description   : This form captures the 7 digit Respid from user for existing Respid/deletes/creates new respid.

 Detail Design : A popup screen to get respid (existing/new/delete) from the User.

 Other         : Called by Name and Address form (frmName)

 Revisions     : See Below
 *********************************************************************
 Modified Date : 10/11/2017
 Modified By   : Christine
 Keyword       : 
 Change Request: 
 Description   : grey out if id equals respid
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CprsBLL;
using CprsDAL;


namespace Cprs
{
   
    public partial class frmRespidEditPopup : Form
    {
        public string newRespid;
        //public string seltdRespidChange;
        private string respid;
        private string locked_by;
        private bool canClose;
        private string idDupDel;

        public frmRespidEditPopup(string Respid, string iD1)
        {
            InitializeComponent();
            respid = Respid;
            idDupDel = iD1;
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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            canClose = true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //idDupDel = iD1;
            if (rdbtnExisting.Checked == true)
            {
                newRespid = txtNewRespid.Text;
                //Check if length less than 7
                if (txtNewRespid.TextLength < 7)
                {
                    MessageBox.Show("Respid should be 7 digits.");
                    txtNewRespid.Text = "";
                    txtNewRespid.Focus();
                    return;
                }
                if (respid == newRespid)
                {
                    MessageBox.Show("Respid is already assigned to the case.");
                    txtNewRespid.Text = "";
                    txtNewRespid.Focus();
                    return;
                }
                if (Convert.ToInt32(newRespid) >= 4000000)
                {
                    MessageBox.Show("Invalid Respid.");
                    txtNewRespid.Text = "";
                    txtNewRespid.Focus();
                    return;
                }

                //convert respid to integer for comparing.
                if (Convert.ToInt32(newRespid) >= 0000000 && Convert.ToInt32(newRespid) <= 3999999)
                //check for valid Respid from the Respondent table.
                {
                    if (GeneralDataFuctions.ChkRespid(newRespid.ToString()))
                    {
                        locked_by = GeneralDataFuctions.ChkRespIDIsLocked(newRespid);
                        if (locked_by != "")
                        {
                            MessageBox.Show("Requested Respid is in use by " + locked_by);
                            txtNewRespid.Text = "";
                            txtNewRespid.Focus();
                            canClose = false;
                        }
                        else
                        {
                            //the respid is valid so update the screen, the sample table and Audit table.
                            if (rdbtnExisting.Checked == true)
                            {
                                newRespid = txtNewRespid.Text + "Existing";
                            }
                            canClose = true;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Respid does not exist");
                        txtNewRespid.Text = "";
                    }
                }
            }
            if (rdbtnNew.Checked == true)
            {
                newRespid = "New";
                canClose = true;
            }

            if (rdbtnDelete.Checked == true)
            {
                if (respid != "")
                    //if (GeneralDataFuctions.ChkRespid(idDupDel.ToString()))
                    //{
                    //    MessageBox.Show("Respid cannot be deleted.");
                    //}
                    //else
                    {
                        newRespid = "Delete";
                        canClose = true;
                    }
                    
                else
                {
                    MessageBox.Show("Respid cannot be deleted.");
                    rdbtnDelete.Checked = false;
                    rdbtnExisting.Checked = true;
                    canClose = false;
                }
            }

         }

        private void frmRespidEditPopup_Load(object sender, EventArgs e)
        {
            if (respid == "")
                rdbtnDelete.Enabled = false;
        }

        private void rdbtnNew_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnNew.Checked)
            {
                label1.Visible = false;
                txtNewRespid.Text = "";
                txtNewRespid.Visible = false;
            }
        }

        private void rdbtnDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnDelete.Checked)
            {
                label1.Visible = false;
                txtNewRespid.Text = "";
                txtNewRespid.Visible = false;              
            }
        }

        private void rdbtnExisting_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbtnExisting.Checked)
            {
                label1.Visible = true;
                txtNewRespid.Visible = true;
            }
        }

        private void frmRespidEditPopup_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (canClose == true)
            { e.Cancel = false; }
            else
            { e.Cancel = true; }
        }

        private void txtNewRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
