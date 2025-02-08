/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmMfParentPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      05/06/2016

Inputs:             Psu, Place, Sched

Parameters:		    None
                 
Outputs:		    Dupmaster, Dupflag

Description:	    This screen will allow the user to choose either presample or sample
 *                  parentid for duplicate cases

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
    public partial class frmMfParentPopup : Form
    {
 
        public int Dupmaster = 0;
        public string Dupflag = "";

        private string Psu;
        private string Bpoid;
        private string Id;
        private SampleData sdata;
        private MasterData mdata;
        MfInitialData mfidata = new MfInitialData();
        
        public frmMfParentPopup(string psu, string bpoid, string id)
        {
            InitializeComponent();
           
            //Carry over the values for Psu and Bpoid from frmMfInital
            Psu = psu;
            Bpoid = bpoid;
            Id = id;
        }

      private void frmMfParentPopup_Load(object sender, EventArgs e)
        {
            mfidata = new MfInitialData();
            sdata = new SampleData();
            mdata = new MasterData();
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

        //allow only numbers to be entered
        private void txtID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "FIN");
        }

        private void rbtnPreSample_Click(object sender, EventArgs e)
        {
            lblParentType.Text = "ENTER PARENT'S ID: ";
        }

        private void rbtnSample_Click(object sender, EventArgs e)
        {
            lblParentType.Text = "ENTER PARENT'S ID: ";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {       
             
            bool validated;
            bool idexist;

            if (rbtnPreSample.Checked)
            {
                if (Id == txtID.Text)
                {
                    MessageBox.Show("ID entered is for the current Presample case. It cannot be used as the Parent ID");
                    txtID.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    validated = mfidata.CheckValidId(Psu, Bpoid, txtID.Text);
                    if (!validated)
                    {
                        MessageBox.Show("Case is not valid.");
                        txtID.Focus();
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        Dupmaster = mfidata.GetPresampMasterid(txtID.Text);
                        Dupflag = "P";
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
            else if (rbtnSample.Checked)
            {

                idexist = GeneralDataFuctions.ValidateSampleId(txtID.Text);               

                if (!idexist)
                {
                    MessageBox.Show("Case is not valid.");
                    txtID.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else if (Id == txtID.Text)
                {
                    MessageBox.Show("ID entered is for the current Sample case. It cannot be used as the Parent ID");
                    txtID.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    Sample smp = sdata.GetSampleData(txtID.Text);
                    Master mst = mdata.GetMasterData(smp.Masterid);

                    if (mst.Owner != "M")                  
                    {
                        MessageBox.Show("Case is not valid.");
                        txtID.Focus();
                        this.DialogResult = DialogResult.None;
                    }
                    else if (smp.Status != "1" && smp.Status != "2" && smp.Status != "3" && smp.Status != "4" && smp.Status != "7")
                    {
                        MessageBox.Show("You must enter the ID of the Original Project.");
                        txtID.Focus();
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                    Dupmaster = mfidata.GetSampleMasterId(txtID.Text);
                    Dupflag = "S";
                    this.DialogResult = DialogResult.OK;
                    }
                }
            }       
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

    }
}
