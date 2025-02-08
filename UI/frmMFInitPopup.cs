/**********************************************************************************
Econ App Name  : CPRS

Project Name   : CPRS Interactive Screens System

Program Name   : frmMFInitPopup.cs	    	

Programmer     : Cestine Gill

Creation Date  : 05/06/2016

Inputs         : None

Parameters     : None
                 
Outputs        : Id

Description    : This screen will allow the user to enter a 7 Digit ID
                 to load data into the MFInitial Screen

Detailed Design: Detailed Design for Multifamily Initial Address Screen 

Other          : Called from: frmMfInitial.cs
 
Revision Hist  :	
***********************************************************************************
Modified Date  :  7/15/2021
Modified By    :  Christine Zhang
Keyword        :  
Change Request :  CR 8350
Description    :  enable select button and both grade 4,5 and NPC leaders will use
                  same method to select cases
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using CprsBLL;
using CprsDAL;


namespace Cprs
{
    public partial class frmMFInitPopup : Form
    {

        public string Id = "";
        public bool selflg = false;
       
        MfInitialData mfidata = new MfInitialData();

        public frmMFInitPopup()
        {

            InitializeComponent();

            txtFin.SelectionLength = txtFin.Text.Length;

            btnSelect.Visible = false;
            btnSelect.Enabled = false;
        }

        private void frmMFInitPopup_Load(object sender, EventArgs e)
        {

            this.ActiveControl = txtFin;

            //The select button visible to users in NPCManager
            //and NPCInterviewer groups

            if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                UserInfo.GroupCode == EnumGroups.NPCLead ||
                   UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
               btnSelect.Visible = true;
               btnSelect.Enabled = true;
                this.AcceptButton = btnSelect;
            }

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

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!(String.IsNullOrEmpty(txtFin.Text.Trim())))
                this.AcceptButton = btnOK;
        }

        private void mtxtFin_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "ID");
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

            if (txtFin.Text.Length == 0 || txtFin.Text.Length != 7)
            {
                MessageBox.Show("ID should be 7 digits.");
                txtFin.Focus();
                txtFin.Text = "";
                this.DialogResult = DialogResult.None;
            }
            else
            {
                bool finexist;

                Id = txtFin.Text.Trim();
                finexist = mfidata.CheckPresampleIdExist(Id);

                if (!finexist)
                {
                    MessageBox.Show("Invalid ID.");
                    txtFin.Text = "";
                    txtFin.Focus();
                    Id = null;
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        //This button retrieves the next case depending upon the user group
        //and the user grade.

        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead)
            {

                //check current time
                DateTime t1 = DateTime.Now;
                DateTime t2 = Convert.ToDateTime("8:00:00 AM");

                //if current time is less than 8 o'clock
                if ((DateTime.Compare(t1, t2)) < 0)
                {
                    MessageBox.Show("No cases are available until 8:00 AM");
                    this.Close();
                    this.DialogResult = DialogResult.Abort;
                    selflg = false;
                    return;
                }

                selflg = true;
                Id = mfidata.GetNextCaseGrade4(Id);
                this.DialogResult = DialogResult.OK;

                if (Id == string.Empty)
                {
                    DialogResult result = MessageBox.Show("All cases have been reviewed.", "Info", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                        this.DialogResult = DialogResult.Abort;
                        selflg = false;
                    }
                }                            
            }

            //if ((UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "5") ||
            //    UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead)
            //{

            //    Id = mfidata.GetNextCaseGrade5();
            //    this.DialogResult = DialogResult.OK;

            //    if (Id == string.Empty)
            //    {                 
            //            DialogResult result = MessageBox.Show("All cases are finished.", "Info", MessageBoxButtons.OK);
            //            if (result == DialogResult.OK)
            //            {
            //                this.Close();
            //                this.DialogResult = DialogResult.Abort;
            //            }                   
            //    }
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Abort;
        }

        private void txtFin_Enter(object sender, EventArgs e)
        {
            txtFin.SelectionStart = 0;
            txtFin.SelectionLength = txtFin.Text.Length;
            txtFin.Focus();
            txtFin.Select(txtFin.SelectionStart, txtFin.SelectionLength);
        }

        private void txtFin_MouseClick(object sender, MouseEventArgs e)
        {
            // sets the cursor to the beginning of the textbox 

            if (!txtFin.MaskCompleted)
            {
                txtFin.SelectionStart = 0;
                txtFin.SelectionLength = txtFin.Text.Length;
                txtFin.Select(txtFin.SelectionStart, txtFin.SelectionLength);
            }
        }
    }
}
