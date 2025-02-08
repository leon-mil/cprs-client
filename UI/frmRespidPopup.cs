
/**********************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmRespidPopup.cs	    	
Programmer      : Diane Musachio
Creation Date   : 08/27/2015
Inputs          : 
                
Parameters      : None                
Outputs         : New respid

Description     : This screen will allow the user enter new RESPID

Detailed Design : Detailed User Requirements for Improvement Screen 

Other           : Called from: frmRespAddrUpdate.cs
 
Revision History:	
***********************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
    public partial class frmRespidPopup : Form
    {
        public string NewRespid = "";

        private bool checkid = false;
        private bool checkinitial = false;
        private bool callfromformlist = false;

        public frmRespidPopup(bool checkId = false, bool checkInitial = false, bool callfromFormlist = false)
        {
            InitializeComponent();
            checkid = checkId;
            checkinitial= checkInitial;
            callfromformlist = callfromFormlist;
            if (callfromFormlist)
                label1.Text = "Enter a Respid";
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

        private void txtRespid_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                btnOK.PerformClick();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtRespid.TextLength < 7)
            {
                if (!callfromformlist)
                    MessageBox.Show("RESPID/ID should be 7 digits.");
                else
                    MessageBox.Show("RESPID should be 7 digits.");

                txtRespid.Focus();
                this.DialogResult = DialogResult.None;
            }
            else
            {
                if (!GeneralDataFuctions.ChkRespid(txtRespid.Text.Trim()))
                {
                    if (checkid)
                    {
                        if (GeneralDataFuctions.ValidateSampleId(txtRespid.Text.Trim()))
                        {
                            SampleData sampdata = new SampleData();
                            Sample s = sampdata.GetSampleData(txtRespid.Text.Trim());

                            if (checkinitial)
                            {                            
                                if ( CheckInitialCases(s.Masterid))
                                { 
                                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen.");
                                    txtRespid.Focus();
                                    this.DialogResult = DialogResult.None;
                                }
                                else
                                {
                                    NewRespid = s.Respid;
                                    this.DialogResult = DialogResult.OK;
                                }
                            }
                            else
                            { 
                                NewRespid = s.Respid;
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Invalid RESPID/ID.");

                            txtRespid.Focus();
                            this.DialogResult = DialogResult.None;
                        }
                    }
                    else
                    {
                        if (!callfromformlist)
                            MessageBox.Show("Invalid RESPID/ID.");
                        else
                            MessageBox.Show("Invalid RESPID.");
                        
                        txtRespid.Focus();
                        this.DialogResult = DialogResult.None;
                    }
                }
                else
                {
                    if (checkinitial)
                    {
                        if (GeneralDataFuctions.ValidateSampleId(txtRespid.Text.Trim()))
                        {
                            SampleData sampdata = new SampleData();
                            Sample s = sampdata.GetSampleData(txtRespid.Text.Trim());

                            if (CheckInitialCases(s.Masterid))
                            {
                                MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen.");
                                txtRespid.Focus();
                                this.DialogResult = DialogResult.None;
                            }
                            else
                            {
                                NewRespid = txtRespid.Text;
                                this.DialogResult = DialogResult.OK;
                            }
                        }
                        else
                        {
                            NewRespid = txtRespid.Text;
                            this.DialogResult = DialogResult.OK;
                        }
                    }
                    else
                    {
                        NewRespid = txtRespid.Text;
                        this.DialogResult = DialogResult.OK;
                    }                   
                }               
            }
        }

        //check the case is initial case or not
        private bool CheckInitialCases(int mid)
        {
            MasterData mdata = new MasterData();
            Master m = mdata.GetMasterData(mid);

            if (m.Seldate == GeneralFunctions.CurrentYearMon())
              return true;
            
            return false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }

        private void frmRespidPopup_Load(object sender, EventArgs e)
        {
            if (checkid)
                label1.Text = "Enter a new RESPID/ID:";
            else
                label1.Text = "Enter a new RESPID:";

        }

        private void txtRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
