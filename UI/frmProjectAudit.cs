/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmProjectAudit.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : optional ID
Inputs        : N/A
Outputs       : N/A
Description   : create project Audit screen include item audit and vip audit
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
***********************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CprsDAL;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cprs
{
    public partial class frmProjectAudit : Cprs.frmCprsParent
    {
        //public property for this form 
        public string Id = string.Empty;


        //Private variables
        private string selected_survey = string.Empty;

        private DataTable dtItem = null;
        private DataTable dtVip = null;
        private frmMessageWait waiting;
        private ProjectAuditData dataObject;

        public frmProjectAudit()
        {
            InitializeComponent();
            tabs.SelectedIndexChanged += new EventHandler(tabs_SelectedIndexChanged);

            //create instance of data object
            dataObject = new ProjectAuditData();
        }

    
        private void frmProjectAudit_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            //for all audit data, show search functions
            if (Id == "")
            {
                txtValueItem.Visible = false;
                cbValueItem.Visible = false;
                lbldatef.Visible = false;

                txtValueVip.Visible = false;
                lbldatef1.Visible = false;
                cbValueVip.Visible = false;

                //disable tab and radiobox
                tabs.Enabled = false;
                rdb1.Enabled = false;
                rdb2.Enabled = false;
                rdb3.Enabled = false;
                rdb4.Enabled = false;
                rdb5.Enabled = false;
                rdb6.Enabled = false;
                rdb7.Enabled = false;
                rdb8.Enabled = false;
                rdb9.Enabled = false;
                rdb10.Enabled = false;
                rdb11.Enabled = false;

                base.DisableMenus();

                //set up backgroundwork to retrieve all data
                backgroundWorker1.RunWorkerAsync();
                dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dgItem.RowHeadersVisible = false;

                dgItem.DataSource = null;
                
            }

            //for one case, hide search functions
            else
            {
                groupBox1.Visible = false;
                groupBox2.Visible = false;
                groupBox3.Visible = false;

                dgItem.DataSource = dataObject.GetProjectItemAudits("", Id, "", "", "", "");
                dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dgItem.RowHeadersVisible = false;
                setItemColumnHeader();
            }
        }

        /*show message wait form */
        public void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }

        /*Background do work */
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            /* Retrieve aduits data */
            dtItem = dataObject.GetProjectItemAudits("", "", "", "", "", "");
            dtVip = dataObject.GetProjectVipAudits("", "", "", "", "", "");

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*show data on aduit item page */
            dgItem.DataSource = dtItem;
            setItemColumnHeader();

           //enable tab and radiobox
            tabs.Enabled = true;
            rdb1.Enabled = true;
            rdb2.Enabled = true;
            rdb3.Enabled = true;
            rdb4.Enabled = true;
            rdb5.Enabled = true;
            rdb6.Enabled = true;
            rdb7.Enabled = true;
            rdb8.Enabled = true;
            rdb9.Enabled = true;
            rdb10.Enabled = true;
            rdb11.Enabled = true;

            base.EnableMenus();
        }


        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Id == "")
            {
                //for item tab, search Items
                if (tabs.SelectedTab == tabPage1)
                {
                    ClearSearch();
                    SearchItem();
                }

                //for vip tab, search Vip
                else
                {
                    ClearSearch1();
                    SearchVip();
                }
            }
        }
        private bool loading_item = false;
        
        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
           //for VARNME and USRNME, show combo box
            if (cbItem.SelectedIndex == 1 || cbItem.SelectedIndex == 2|| cbItem.SelectedIndex == 3)
            {
                loading_item = true;
                PopulateValueCombo(1, cbItem.SelectedIndex);
                cbValueItem.Visible = true;
                cbValueItem.Focus();
                txtValueItem.Visible = false;
                lbldatef.Visible = false;
                loading_item = false;
            }
             // for id and progam, show text box
            else
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;
                txtValueItem.Focus();

                if (cbItem.SelectedIndex == 4)
                {
                    lbldatef.Visible = true;
                    txtValueItem.MaxLength = 10;
                }
                else
                {
                    lbldatef.Visible = false;
                    txtValueItem.MaxLength = 7;
                }
            }
            txtValueItem.Text = "";
                
        }

        /*Setup value combo data, based on tab and combo index */
        private void PopulateValueCombo(int tabIndex, int cbIndex)
        {
            /*for Item audit */
            if (tabIndex == 1)
            {
                cbValueItem.DataSource = dataObject.GetValueList(tabIndex, cbIndex);

                if (cbIndex == 1)
                {
                    cbValueItem.ValueMember = "newtc";
                    cbValueItem.DisplayMember = "newtc";
                }
                /*for varnme combo */
                else if (cbIndex == 2)
                {
                    cbValueItem.ValueMember = "varnme";
                    cbValueItem.DisplayMember = "varnme";
                }
                /* for usrnme combo */
                else
                {
                    cbValueItem.ValueMember = "usrnme";
                    cbValueItem.DisplayMember = "USER";
                }
                cbValueItem.SelectedIndex = -1;
            }
            /*for Vip audit usrnme combo */
            else
            {
                DataTable dt = dataObject.GetValueList(tabIndex, cbIndex);
                cbValueVip.DataSource = dt;
                if (cbIndex == 1)
                {
                    cbValueVip.ValueMember = "newtc";
                    cbValueVip.DisplayMember = "newtc";
                }
                else
                {
                    cbValueVip.ValueMember = "usrnme";
                    cbValueVip.DisplayMember = "USER";
                }

                cbValueVip.SelectedIndex = -1;
            }
            
        }

        /*get select survey based on survey radiobox */
        private string getSelectSurvey()
        {
             selected_survey = "";

            if (rdb2.Checked)
            {
                selected_survey = "S";
            }
            else if (rdb3.Checked)
            {
                selected_survey = "N";
            }
            else if (rdb4.Checked)
            {
                selected_survey = "F";
            }
            else if (rdb5.Checked)
            {
                selected_survey = "M";
            }
            else if (rdb6.Checked)
            {
                selected_survey = "T";
            }
            else if (rdb7.Checked)
            {
                selected_survey = "E";
            }
            else if (rdb8.Checked)
            {
                selected_survey = "G";
            }
            else if (rdb9.Checked)
            {
                selected_survey = "R";
            }
            else if (rdb10.Checked)
            {
                selected_survey = "O";
            }
            else if (rdb11.Checked)
            {
                selected_survey = "W";
            }

            return selected_survey;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchItem(true);
        }

       
        /*Item Search */
        private void SearchItem(bool from_search = false)
        {
            string id = "";
            string newtc = "";
            string varnme = "";
            string usrnme = "";
            string prgdtm = "";
            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;

            if (Id == "")
            {
                selected_survey = getSelectSurvey();

                if (from_search && cbItem.SelectedIndex == -1)
                {
                    MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                    return;
                }
                //for id
                else if (cbItem.SelectedIndex == 0)
                {
                    //Validate length of id is 7 
                    if (txtValueItem.Text.Length != 7)
                    {
                        MessageBox.Show("ID should be 7 digits.");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }
                    else if (!GeneralDataFuctions.ValidateSampleId(txtValueItem.Text.Trim()))
                    {
                        MessageBox.Show("Invalid ID.");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }
                    else
                        id = txtValueItem.Text.Trim();
                }
                // for newtc
                else if (cbItem.SelectedIndex == 1)
                {
                    if (cbValueItem.Text.Trim() == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Box");
                        cbValueItem.Focus();
                        return;
                    }
                    else
                        newtc = cbValueItem.Text;
                }
                // for varnme
                else if (cbItem.SelectedIndex == 2)
                {
                    if (cbValueItem.Text.Trim() == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Box");
                        cbValueItem.Focus();
                        return;
                    }
                    else
                        varnme = cbValueItem.Text;
                }
                 // for usrnme
                else if (cbItem.SelectedIndex == 3)
                {
                    if (cbValueItem.Text.Trim() == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Box");
                        cbValueItem.Focus();
                        return;
                    }
                    else
                        usrnme = cbValueItem.Text;
                }
                // for prgdtm
                else if (cbItem.SelectedIndex == 4)
                {
                    /*Verify date, and convert date to MM/DD/YYYY format */
                    if (!String.IsNullOrEmpty(txtValueItem.Text.Trim()))
                    {
                        if (GeneralFunctions.VerifyDate(txtValueItem.Text.Trim()))
                            prgdtm = GeneralFunctions.ConvertDateFormat(txtValueItem.Text);
                        else
                        {
                            MessageBox.Show("Please enter correct date format");
                            txtValueItem.Text = "";
                            txtValueItem.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a date value");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }

                }
            }
            else
                id = Id;

            //search all items
            if ((selected_survey == "") && (id == "") && (newtc == "") && (varnme == "") && (usrnme == "") && (prgdtm == ""))
                dt = dtItem;
            else
                dt = dataObject.GetProjectItemAudits(selected_survey, id, newtc, varnme, usrnme, prgdtm);
            
            dgItem.DataSource = dt;
            setItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data to display.");
            }
        }

       
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSearch();
        }

        private void ClearSearch()
        {
            loading_item = true;
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            lbldatef.Visible = false;
            SearchItem(false);
            loading_item = true;
        }

        private void setItemColumnHeader()
        {
            dgItem.Columns[0].HeaderText = "ID";
            dgItem.Columns[0].Width = 60;
            dgItem.Columns[1].HeaderText = "OWNER";
            dgItem.Columns[1].Width = 60;
            dgItem.Columns[2].HeaderText = "NEWTC";
            dgItem.Columns[2].Width = 80;
            dgItem.Columns[3].HeaderText = "VARNME";
            dgItem.Columns[4].HeaderText = "OLDVAL";
            dgItem.Columns[5].HeaderText = "OLDFLAG";
            dgItem.Columns[6].HeaderText = "NEWVAL";
            dgItem.Columns[7].HeaderText = "NEWFLAG";
            dgItem.Columns[8].HeaderText = "USER";
            dgItem.Columns[9].HeaderText = "DATE/TIME";
        }

        private void setVipColumnHeader()
        {
            dgVip.Columns[0].HeaderText = "ID";
            dgVip.Columns[0].Width = 60;
            dgVip.Columns[1].HeaderText = "OWNER";
            dgVip.Columns[1].Width = 60;
            dgItem.Columns[2].HeaderText = "NEWTC";
            dgItem.Columns[2].Width = 80;
            dgVip.Columns[3].HeaderText = "DATE6";
            dgVip.Columns[4].HeaderText = "OLDVAL";
            dgVip.Columns[5].HeaderText = "OLDFLAG";
            dgVip.Columns[6].HeaderText = "NEWVAL";
            dgVip.Columns[7].HeaderText = "NEWFLAG";
            dgVip.Columns[8].HeaderText = "USER";
            dgVip.Columns[9].HeaderText = "DATE/TIME";
        }

        private void cbVip_SelectedIndexChanged(object sender, EventArgs e)
        {
            // for usrnme, show combo
            if (cbVip.SelectedIndex == 1 || cbVip.SelectedIndex == 3)
            {
                loading_item = true;
                PopulateValueCombo(2, cbVip.SelectedIndex);
                cbValueVip.Visible = true;
                cbValueVip.Focus();
                txtValueVip.Visible = false;
                lbldatef1.Visible = false;
                loading_item = false;
            }
            // for id, date6, prdtm, show text box
            else
            {
                txtValueVip.Visible = true;
                txtValueVip.Text = "";
                txtValueVip.Focus();
                cbValueVip.Visible = false;
                cbValueVip.SelectedIndex = -1;

                if (cbVip.SelectedIndex == 4)
                {
                    lbldatef1.Visible = true;
                    txtValueVip.MaxLength = 10;
                }
                else
                {
                    lbldatef1.Visible = false;
                    txtValueVip.MaxLength = 7;
                }
            }
        }

        private void btnSearch1_Click(object sender, EventArgs e)
        {
            SearchVip(true);
        }

        /*VIP Search */
        private void SearchVip(bool from_search = false)
        {
            string id = "";
            string newtc = "";
            string date6 = "";
            string usrnme = "";
            string prgdtm = "";
            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;
            if (Id == "")
            {
                selected_survey = getSelectSurvey();
                if (from_search && cbVip.SelectedIndex == -1)
                {
                    MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                    return;
                }
                if (cbVip.SelectedIndex == 0)
                {
                    //Validate length of id is 7 
                    if (txtValueVip.Text.Length != 7)
                    {
                        MessageBox.Show("ID should be 7 digits.");
                        txtValueVip.Text = "";
                        txtValueVip.Focus();
                        return;
                    }
                    else if (!GeneralDataFuctions.ValidateSampleId(txtValueVip.Text.Trim()))
                    {
                        MessageBox.Show("Invalid ID.");
                        txtValueVip.Text = "";
                        txtValueVip.Focus();
                        return;
                    }
                    else
                        id = txtValueVip.Text.Trim();
                    
                }
                else if (cbVip.SelectedIndex == 1)
                {
                    if (cbValueVip.Text.Trim() == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Box");
                        cbValueVip.Focus();
                        return;
                    }
                    else
                        newtc = cbValueVip.Text;
                }
                else if (cbVip.SelectedIndex == 2)
                {
                    date6 = txtValueVip.Text.Trim();
                }
                else if (cbVip.SelectedIndex == 3)
                {
                    if (cbValueVip.Text.Trim() == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Box");
                        cbValueVip.Focus();
                        return;
                    }
                    else
                        usrnme = cbValueVip.Text;
                }
                else if (cbVip.SelectedIndex == 4)
                {
                     /*Verify date, and convert date to MM/DD/YYYY format */
                    if (!String.IsNullOrEmpty(txtValueVip.Text.Trim()))
                    {
                        if (GeneralFunctions.VerifyDate(txtValueVip.Text.Trim()))
                            prgdtm = GeneralFunctions.ConvertDateFormat(txtValueVip.Text);
                        else
                        {
                            MessageBox.Show("Please enter correct date format");
                            txtValueVip.Text = "";
                            txtValueVip.Focus();
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Please enter a date value");
                        txtValueVip.Text = "";
                        txtValueVip.Focus();
                        return;
                    }
                }
            }
            else
                id = Id;

            if ((selected_survey == "") && (newtc == "") && (id == "") && (date6 == "") && (usrnme == "") && (prgdtm == ""))
                dt = dtVip;
            else
                 dt = dataObject.GetProjectVipAudits(selected_survey, id, newtc, date6, usrnme, prgdtm);

            dgVip.DataSource = dt;
            dgVip.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgVip.RowHeadersVisible = false;
            setVipColumnHeader();

            Cursor.Current = Cursors.Default;
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data to display.");
            }
           
        }

        private void btnClear1_Click(object sender, EventArgs e)
        {
            ClearSearch1();
        }

        private void ClearSearch1()
        {
            loading_item = true;
            cbVip.SelectedIndex = -1;
            cbValueVip.SelectedIndex = -1;
            cbValueVip.Visible = false;
            txtValueVip.Visible = false;
            lbldatef1.Visible = false;
            SearchVip(false);
            loading_item = false;

            setVipColumnHeader();
        }

        /*radio box events */

        //for all cases
        private void rdb1_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }
        // for state and local cases
        private void rdb2_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }
        // for NonRes cases
        private void rdb3_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }
        // for federal cases
        private void rdb4_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }
        // for Multi-Family cases
        private void rdb5_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }
        private void rdb6_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }

        private void rdb7_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }

        private void rdb8_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }

        private void rdb9_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }

        private void rdb10_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }

        private void rdb11_CheckedChanged(object sender, EventArgs e)
        {
            GoSearch(sender);
        }
        
        /*Search based on checked radiobox */
        private void GoSearch(object sender)
        {
            RadioButton rb = sender as RadioButton;
            if (rb != null)
            {
                if (rb.Checked)
                {
                    // when a radio button was checked, do auto Search
                    if (tabs.SelectedTab == tabPage1)
                        SearchItem();
                    else
                        SearchVip();
                }
            }
        }

        private void txtValueItem_TextChanged(object sender, EventArgs e)
        {
            if (cbItem.SelectedIndex ==0)
            {
                if (txtValueItem.Text.Length > 7)
                {
                    MessageBox.Show("The length of ID cannot be greater than 7");
                    txtValueItem.Text = txtValueItem.Text.Substring(0, 7);
                }

            }
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbItem.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
        }

        private void txtValueVip_TextChanged(object sender, EventArgs e)
        {
            if (cbVip.SelectedIndex == 0)
            {
                if (txtValueVip.Text.Length > 7)
                {
                    MessageBox.Show("The length of ID cannot be greater than 7");
                    txtValueVip.Text = txtValueVip.Text.Substring(0, 7);
                }

            }
        }

        private void txtValueVip_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbVip.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            else
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
        }

        private void frmProjectAudit_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*release memory*/
            dataObject = null;
            dtItem = null;
            dtVip = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void cbValueItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading_item)
                btnSearch_Click(sender, e);
        }

        private void txtValueItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
        }

        private void cbValueVip_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading_item )
                btnSearch1_Click(sender, e);
        }

        private void txtValueVip_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch1_Click(sender, e);
        }
    }
}
