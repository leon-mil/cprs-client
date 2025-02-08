/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmRspAudit.cs
Programmer    : Christine Zhang
Creation Date : Sept. 9 2015
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create resp audit screen to view respondent audit data
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
    public partial class frmRspAudit : Cprs.frmCprsParent
    {
        private RespAuditData dataObject;

        public frmRspAudit()
        {
            InitializeComponent();

            //create instance of data object
            dataObject = new RespAuditData();
        }

        private void frmRspAudit_Load(object sender, EventArgs e)
        {

            lbldatef.Visible = false;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;

            dgItem.DataSource = dataObject.GetRspAudits("", "", "", "");
            dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgItem.RowHeadersVisible = false;
            setItemColumnHeader();

            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");
        }

        private void setItemColumnHeader()
        {
            dgItem.Columns[0].HeaderText = "RESPID";
            dgItem.Columns[0].Width = 75;
            dgItem.Columns[1].HeaderText = "VARNME";
            dgItem.Columns[1].Width = 85;
            dgItem.Columns[2].HeaderText = "OLDVAL";
            dgItem.Columns[2].Width = 355;
            dgItem.Columns[3].HeaderText = "NEWVAL";
            dgItem.Columns[3].Width = 355;
            dgItem.Columns[4].HeaderText = "USER";
            dgItem.Columns[4].Width = 75;
            dgItem.Columns[5].HeaderText = "DATE/TIME";
            dgItem.Columns[5].Width = 120;
        }


        private bool loading_item = false;
        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for Varnme and USRNME, show combo box
            if (cbItem.SelectedIndex == 1 || cbItem.SelectedIndex == 2)
            {
                loading_item = true;
                PopulateValueCombo(cbItem.SelectedIndex);
                cbValueItem.Visible = true;
                cbValueItem.Focus();
                txtValueItem.Visible = false;
                lbldatef.Visible = false;
                loading_item = false;
            }
            // for csdnum and progam, show text box
            else
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;
                txtValueItem.Focus();

                if (cbItem.SelectedIndex == 3)
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



        /*Setup value combo data, based on combo index */
        private void PopulateValueCombo(int cbIndex)
        {
            cbValueItem.DataSource = dataObject.GetValueList(cbIndex);

            /*for action combo */
            if (cbIndex == 1)
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


        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            Search(true);
        }

        /*Item Search */
        private void Search(bool from_search = false)
        {
            string respid = "";
            string varnme = "";
            string usrnme = "";
            string prgdtm = "";
            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;

            if (from_search && cbItem.SelectedIndex == -1)
            {
                MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                return;
            }
            // for respid
            else if (cbItem.SelectedIndex == 0)
            {
                //Validate length of id is 7 
                respid = txtValueItem.Text.Trim();
                if (!(respid.Length == 7))
                {
                    MessageBox.Show("RESPID should be 7 digits.");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
                else
                {
                    if (!GeneralDataFuctions.ChkRespid(respid))
                    {
                        MessageBox.Show("Invalid RESPID.");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }
                }
            }
            else if (cbItem.SelectedIndex == 1)
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
            else if (cbItem.SelectedIndex == 2)
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
            else if (cbItem.SelectedIndex == 3)
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
            dt = dataObject.GetRspAudits(respid, varnme, usrnme, prgdtm);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            loading_item = true;
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            lbldatef.Visible = false;
            Search();
            loading_item = false;
        }

        private void txtValueItem_TextChanged(object sender, EventArgs e)
        {
            if (cbItem.SelectedIndex == 0)
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

        private void frmRspAudit_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
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
