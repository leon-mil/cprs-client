
/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmIdPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/27/2015
Inputs:             ID
                
Parameters:		    None                
Outputs:		    None

Description:	    This screen will allow the user enter new ID

Detailed Design:    Detailed User Requirements for Improvement Screen 

Other:	            Called from: frmImprovement
 
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
    public partial class frmIdPopup : Form
    {
        public string NewId = "";
        public Form CallingForm = null;

        private CesampleData cdata;
        private SampleData sdata;

        public frmIdPopup()
        {
            InitializeComponent();
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

    
        private void frmMcdidPopup_Load(object sender, EventArgs e)
        {
            if (CallingForm.Name == "frmImprovements")
                cdata = new CesampleData();
            else
                sdata = new SampleData();

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtMcdid.TextLength < 7)
            {
                MessageBox.Show("ID should be 7 digits.");
                txtMcdid.Focus();
                this.DialogResult = DialogResult.None;
            }
            else
            {
                bool idexist;
                if (CallingForm.Name == "frmImprovements")
                    idexist = cdata.CheckIdExist(txtMcdid.Text.Trim());
                else
                    idexist = GeneralDataFuctions.ValidateSampleId(txtMcdid.Text.Trim());

                if (!idexist)
                {
                    MessageBox.Show("Invalid ID.");
                    txtMcdid.Focus();
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                    if (CallingForm.Name != "frmImprovements")
                    {
                        //Check Initial case
                        Sample sampNew = sdata.GetSampleData(txtMcdid.Text);
                        MasterData mastdata = new MasterData();
                        Master mastNew = mastdata.GetMasterData(sampNew.Masterid);
                        string curr_survey_month = GeneralFunctions.CurrentYearMon();

                        //check Id is Initial case or not
                        string seldate = mastNew.Seldate;
                        if (seldate == curr_survey_month && mastNew.Owner != "M")
                        {
                            MessageBox.Show("ID entered is for an initial case and should be accessed through initial case screen.");
                            txtMcdid.Focus();
                            this.DialogResult = DialogResult.None;
                            return;
                        }
                        else
                            NewId = txtMcdid.Text.Trim();
                    }
                    else 
                        NewId = txtMcdid.Text.Trim();


                    this.DialogResult = DialogResult.OK;

                }
            }

        }


        private void txtMcdid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtMcdid_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(sender, e);
            }
        }
    }
}
