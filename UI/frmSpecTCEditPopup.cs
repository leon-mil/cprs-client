/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : frmSpecialPriorityTCEditPopup.cs
 Programmer    : Diane Musachio
 Creation Date : 10/19/2017
 Inputs        : N/A
 Parameters    : N/A
 Output        : N/A
 Description   : This program will display popup screen to add or edit
                 special tc that prioritizes call scheduler case
 Detail Design : Detailed Design for Special Priority TC
 Other         : Called by: frmSpecialPriorityTC
 Revisions     : See Below
 *********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using CprsBLL;

namespace Cprs
{
    public partial class frmSpecTCEditPopup : Form
    {
        //public variables
        public string tc;
        public string owner;
        public int minval;
        public int maxval;
        private bool edit = false;
       
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

        public frmSpecTCEditPopup()
        {
            InitializeComponent();
        }

        private void frmSpecialPriorityTCEditPopup_Load(object sender, EventArgs e)
        {
            CreateSurveyPulldown();

            CreateTCPulldown(owner);

            //if owner and tc have values then this is an edit
            if ((owner != "") && (tc != ""))
            {
                cbTC.Text = tc.Trim();
                cbTC.SelectedItem = tc;

                cbSurvey.Text = owner;

                txtMin.Text = minval.ToString();
                txtMax.Text = maxval.ToString();

                //not editable
                cbSurvey.Enabled = false;
                cbTC.Enabled = false;

                edit = true;
            }
            else
            {
                cbSurvey.SelectedIndex = -1;
                cbTC.SelectedIndex = -1;
            }
        }

        private void CreateSurveyPulldown()
        {
            BindingSource source = new BindingSource();
            List<string> dataSource = new List<string>();
            dataSource.Add("");
            dataSource.Add("N");
            dataSource.Add("P");
            dataSource.Add("F");
            dataSource.Add("M");
            dataSource.Add("U");

            cbSurvey.DataSource = dataSource;
        }

        private void CreateTCPulldown(string survey)
        {
            BindingSource source = new BindingSource();
            List<string> dataSource = new List<string>();
            dataSource.Add("");

            if ((survey == "N") || (survey == "P") || (survey == "F"))
            {
                dataSource.Add("00");
                dataSource.Add("01");
                dataSource.Add("02");
                dataSource.Add("03");
                dataSource.Add("04");
                dataSource.Add("05");
                dataSource.Add("06");
                dataSource.Add("07");
                dataSource.Add("08");
                dataSource.Add("09");
                dataSource.Add("10");
                dataSource.Add("11");
                dataSource.Add("12");
                dataSource.Add("13");
                dataSource.Add("14");
                dataSource.Add("15");
                dataSource.Add("16");
                dataSource.Add("19");
                dataSource.Add("20");
            }
            else if (survey == "M")
            {
                dataSource.Add("00");
            }
            else if (survey == "U")
            {
                dataSource.Add("16");
                dataSource.Add("19");
            }

            cbTC.DataSource = dataSource;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbSurvey_Click(object sender, EventArgs e)
        {
            CreateSurveyPulldown();
        }

        private void cbTC_Click(object sender, EventArgs e)
        {
            if (cbSurvey.SelectedIndex != -1)
            {
                CreateTCPulldown(cbSurvey.Text);
            }
            else
            {
                cbTC.SelectedIndex = -1;
            }
        }

        private void cbSurvey_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!edit)
            {
                cbTC.SelectedIndex = -1;
                cbTC.DataSource = null;
            }
        }

        private void txtMin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtMax_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
               (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //needed to prevent null exception if blank
            if ((txtMax.Text) == "")
            {
                txtMax.Text = "0";
            }

            //needed to prevent null exception if blank
            if ((txtMin.Text) == "")
            {
                txtMin.Text = "0";
            }

            //check that owner and tc have values entered
            if ((cbSurvey.Text == "") || (cbTC.Text == ""))
            {
                MessageBox.Show("Please enter values for Survey and Newtc");
                this.DialogResult = DialogResult.None;
            }                                    
            else
            {
                //check to see if survey/tc combination already exists and not edit
                var data_object = new SpecialTCData();
                if (data_object.CheckTcDataExists(cbSurvey.Text, cbTC.Text) && !edit)
                {
                    MessageBox.Show("Survey/TC already exists in Special TC List");
                    cbSurvey.SelectedIndex = -1;
                    cbTC.SelectedIndex = -1;
                    txtMax.Text = "0";
                    txtMin.Text = "0";
                    this.DialogResult = DialogResult.None;
                }
                //compare values of min and max text boxes
                else if (Convert.ToInt32(txtMax.Text) <=
                    Convert.ToInt32(txtMin.Text))
                {
                    MessageBox.Show("Maximum value must be greater than minimum value");
                    if (!edit)
                    {
                        txtMax.Text = "0";
                        txtMin.Text = "0";
                        this.DialogResult = DialogResult.None;
                    }
                    else
                    {
                        txtMax.Text = maxval.ToString();
                        txtMin.Text = minval.ToString();
                        this.DialogResult = DialogResult.None;
                    }
                }
                else if (Convert.ToInt32(txtMin.Text) <= 1)
                {
                    MessageBox.Show("Minimum value must be greater than 1");
                    this.DialogResult = DialogResult.None;
                }
                else if (Convert.ToInt32(txtMax.Text) >= 99999998)
                {
                    MessageBox.Show("Maximum value must be less than 99999998");
                    this.DialogResult = DialogResult.None;
                }
                //if survey/tc doesn't exist then it's an add or an edit
                else
                {
                    //assign program data and time 
                    DateTime dt = DateTime.Now;
                    string user = UserInfo.UserName;
                    int nminval = Int32.Parse(txtMin.Text);
                    int nmaxval = Int32.Parse(txtMax.Text);
                    data_object.UpdateSpecialTcData(cbSurvey.Text, cbTC.Text, nminval, nmaxval, user, dt, edit);

                    if (edit)
                    {
                        if ((minval != nminval) || (maxval != nmaxval))
                        {
                            //pass information to audit trail
                            data_object.UpdateSpecialTCAudit(owner, tc, "EDIT", minval, maxval, nminval, nmaxval, user, dt);
                            this.Dispose();
                            this.Refresh();
                            this.DialogResult = DialogResult.OK;
                        }
                        //if no changes then don't add to audit
                        else
                        {
                            this.Dispose();
                            this.Refresh();
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                    //add
                    else
                    {
                        data_object.UpdateSpecialTCAudit(cbSurvey.Text, cbTC.Text, "ADD", 0, 0, nminval, nmaxval, user, dt);
                        this.Dispose();
                        this.Refresh();
                        this.DialogResult = DialogResult.OK;
                    }
                }
            }
        }
    }
}
