/**********************************************************************************
Econ App Name  : CPRS

Project Name   : CPRS Interactive Screens System

Program Name   : frmDCPInitPopup.cs	    	

Programmer     :         

Creation Date  :      

Inputs         : None

Parameters     : None
                 
Outputs        : SelectedDCPId

Description    : This screen will allow the user to enter a 7 Digit Id
                 to load data into the Dodge Intial Screen

Detailed Design: Detailed Design for Dodge Initial Address Screen 

Other          : Called from: frmDodgeInitial.cs
 
Revision Hist  :	
***********************************************************************************
Modified Date  :  6/10/2021
Modified By    :  Christine Zhang
Keyword        :  
Change Request :  CR#8284
Description    :  enable the select button for NPC users
***********************************************************************************
Modified Date  :  7/13/2021
Modified By    :  Christine Zhang
Keyword        :  
Change Request :  CR 8349
Description    : Get ids base on timezone; 
                 For getting the case for Grade 5 clerk and NPC lead/manager it  
                 will be the same logic as  getting a case for Grade 4 interviewer 
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using CprsBLL;
using CprsDAL;
using System.Linq;
using System.Globalization;


namespace Cprs
{
    public partial class frmDCPInitPopup : Form
    {
        public bool selflg = false;
        public string SelectedDCPId = string.Empty;
       
        MfInitialData mfidata = new MfInitialData();
        DodgeInitialData DodgInitdata = new DodgeInitialData();
       
        public frmDCPInitPopup()
        {
            InitializeComponent();
            txtID.SelectionLength = 0;
            txtID.SelectionLength = txtID.Text.Length;
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
        
        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer ||UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead)
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

                DataTable dtId = DodgInitdata.GetNextCaseGrade4();
                this.DialogResult = DialogResult.OK;
                if (dtId.Rows.Count == 0)
                {
                    DialogResult result = MessageBox.Show("All cases have been reviewed.", "Info", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    selflg = true;
                    SelectedDCPId = dtId.Rows[0][0].ToString();     
                }
            }

            //if ((UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "5") ||
            //    UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead)
            //{
            //    DataTable dtId = DodgInitdata.GetNextCaseGrade4();
            //    this.DialogResult = DialogResult.OK;

            //    if (dtId.Rows.Count == 0)
            //    {
            //        DialogResult result = MessageBox.Show("All cases have been reviewed", "Info", MessageBoxButtons.OK);
            //        if (result == DialogResult.OK)
            //        {
            //            this.Close();
            //        }
            //    }
            //    else
            //    {
            //        SelectedDCPId = dtId.Rows[0][0].ToString();
            //    }
            //}

            if (UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCLead)
            {
                DataTable dtId = DodgInitdata.GetIDListforNExtInitial();
                this.DialogResult = DialogResult.OK;
                if (DodgInitdata.ChkHqSector(UserInfo.UserName))
                {
                    DodgInitdata.GetHQSectors();
                    SelectedDCPId = DodgInitdata.id4HQsect;
                    if (SelectedDCPId == "")
                    {
                        DialogResult result = MessageBox.Show("All cases have been reviewed", "Info", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            this.Close();
                            this.DialogResult = DialogResult.Abort;
                        }
                    }
                }
                else
                {
                    if (dtId.Rows.Count == 0)
                    {
                        DialogResult result = MessageBox.Show("All cases have been reviewed", "Info", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        SelectedDCPId = dtId.Rows[0][0].ToString();
                    }
                }
            }
        }
        
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            this.DialogResult = DialogResult.Abort;
        }

        private void btnOKDCP_Click(object sender, EventArgs e)
        {
            //Check if length less than 7
            if (txtID.Text.Length < 7)
            {
                MessageBox.Show("ID should be 7 digits.");
                txtID.Focus();
                this.DialogResult = DialogResult.None;
            }
            else if (txtID.Text == "")
            {
                MessageBox.Show("ID should be 7 digits.");
                txtID.Focus();
                this.DialogResult = DialogResult.None;
            }
            else
            {
                bool idexist;
                idexist = GeneralDataFuctions.ValidateDCPInitialId(txtID.Text);
                //update the data with DCP Initial data for the entered ID

                if (!idexist)
                {
                    MessageBox.Show("Invalid ID.");
                    txtID.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                    SelectedDCPId = txtID.Text;
            }
        }

        private void frmDCPInitPopup_Load(object sender, EventArgs e)
        {
            this.ActiveControl = txtID;
              
        }

        private void txtID_Enter(object sender, EventArgs e)
        {
            txtID.SelectionLength = 0;
            txtID.SelectionLength = txtID.Text.Length;
            txtID.Focus();
            txtID.Select(txtID.SelectionStart, txtID.SelectionLength);
        }

        private void txtID_Click(object sender, EventArgs e)
        {
            txtID.SelectionLength = 0;
            txtID.SelectionLength = txtID.Text.Length;
            txtID.Focus();
            txtID.Select(txtID.SelectionStart, txtID.SelectionLength);
        }

        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtID_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 7))
            {
                btnOKDCP_Click(sender, e);
                this.DialogResult = DialogResult.OK;
            }
        }
    }
}
