/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmSystemAudit.cs
Programmer    : Christine Zhang
Creation Date : Sept. 3 2015
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create system audit screen to view access data
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
using CprsBLL;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cprs
{
    public partial class frmSystemAudit : Cprs.frmCprsParent
    {
        private SystemAuditData dataObject;
        private bool loadStatp = false;

        private delegate void SearchDelegate();

        public frmSystemAudit()
        {
            InitializeComponent();

            //create instance of data object
            dataObject = new SystemAuditData();
        }

        private void frmSystemAudit_Load(object sender, EventArgs e)
        {
            loadStatp = true;

            cbStatp.DataSource = dataObject.GetValueList("STATP");
            cbStatp.ValueMember = "statp";
            cbStatp.DisplayMember = "statp";

            cbStatp.SelectedIndex = 0;

            lbldatef.Visible = false;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;

            loadStatp = false;

            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            BeginInvoke(new SearchDelegate(Search));

            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer )
                btnReview.Enabled = true;
            else
                btnReview.Enabled = false;
        }

        private void setItemColumnHeader()
        {
            dgItem.Columns[0].HeaderText = "SURVEY DATE";
            dgItem.Columns[1].HeaderText = "MODULE";
            dgItem.Columns[2].HeaderText = "ACTION";
            dgItem.Columns[3].HeaderText = "USER";
            dgItem.Columns[4].HeaderText = "DATE/TIME";
        }

        private bool loading_item = false;
        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for ACTION and USRNME, show combo box
            if (cbItem.SelectedIndex == 0 || cbItem.SelectedIndex == 1)
            {
                loading_item = true;
                PopulateValueCombo(cbItem.SelectedIndex);
                cbValueItem.Visible = true;
                cbValueItem.Focus();
                txtValueItem.Visible = false;
                lbldatef.Visible = false;
                loading_item = false;
            }
            // for date, show text box
            else
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;
                txtValueItem.Focus();

                if (cbItem.SelectedIndex == 2)
                    lbldatef.Visible = true;
                else
                    lbldatef.Visible = false;
            }
            txtValueItem.Text = "";
        }

        /*Setup value combo data, based on combo index */
        private void PopulateValueCombo(int cbIndex)
        {
            /*for action combo */
            if (cbIndex == 0)
            {
                cbValueItem.DataSource = dataObject.GetValueList("MODULE");

                cbValueItem.ValueMember = "module";
                cbValueItem.DisplayMember = "module";
            }
            /* for usrnme combo */
            else
            {
                cbValueItem.DataSource = dataObject.GetValueList("USRNME");

                cbValueItem.ValueMember = "usrnme";
                cbValueItem.DisplayMember = "USER";
            }
            cbValueItem.SelectedIndex = -1;
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            SearchMore(true);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            loading_item = true;
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            lbldatef.Visible = false;
            SearchMore();
            loading_item = false;
        }

        /*Item Search */
        private void Search()
        {
            DataTable dt;

            //search all items
            dt = dataObject.GetSystemAudit(cbStatp.Text, "", "", "");
            dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgItem.RowHeadersVisible = false;

            dgItem.DataSource = dt;
            setItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if (dt.Rows.Count == 0)
            {
                groupBox1.Enabled = false;
                MessageBox.Show("No data to display.");
            }
            else
                groupBox1.Enabled = true;
        }

        private void SearchMore(bool from_search = false)
        {
            string module = "";
            string usrnme = "";
            string prgdtm = "";
            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;

            if (from_search && cbItem.SelectedIndex == -1)
            {
                MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                return;
            }
            // for module
            else if (cbItem.SelectedIndex == 0)
            {
                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Box");
                    cbValueItem.Focus();
                    return;
                }
                else
                    module = cbValueItem.Text;
            }
            // for usrnme
            else if (cbItem.SelectedIndex == 1)
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
            else if (cbItem.SelectedIndex == 2)
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
                    txtValueItem.Focus();
                    return;
                }
            }

            //search all items
            dt = dataObject.GetSystemAudit(cbStatp.Text, module, usrnme, prgdtm);
            dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgItem.RowHeadersVisible = false;

            dgItem.DataSource = dt;
            setItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if (dt.Rows.Count == 0)
            {
                //groupBox1.Enabled = false;
                MessageBox.Show("No data to display.");
            }

        }
       
        private void cbStatp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loadStatp)
            {
                loading_item = true;
                cbItem.SelectedIndex = -1;
                cbValueItem.SelectedIndex = -1;
                cbValueItem.Visible = false;
                txtValueItem.Visible = false;
                lbldatef.Visible = false;
                loading_item = false;
                Search();
            }
        }

        private void frmSystemAudit_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnReview_Click(object sender, EventArgs e)
        {
            frmMonthlyAuditReviewPopup popup = new frmMonthlyAuditReviewPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
        }

        private void cbValueItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loading_item)
                btnSearchItem_Click(sender, e);
        }

        private void txtValueItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchItem_Click(sender, e);
        }
    }
}
