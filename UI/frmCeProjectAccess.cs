/**************************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmCeProjectAccess.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/23/2015

Inputs:             CprsBLL.Searchinfo
                   

Parameters:		    None 

Outputs:		    None	

Description:	    

Detailed Design:    Detailed User Requirements for the Ceaccess Project Access Information Screen 

Other:	            Called from: 
 
Revision History:	
**************************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**************************************************************************************************/
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
    public partial class frmCeProjectAccess : Cprs.frmCprsParent
    {
        //public property for this form 
        public string Id = string.Empty;

        //Private variables
        private DataTable dtItem = null;
        private CeProjectAccessData dataObject;

        private bool loadStatp = false;

        private delegate void SearchDelegate();

        //Populate the drop down search criteria combobox

        private string[] projSearch = { "ID", "ACTION", "USER", "DATE"};

        public frmCeProjectAccess()
        {
            InitializeComponent();

            //create instance of data object

            dataObject = new CeProjectAccessData();
        }

        private void frmCeProjectAccess_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            loadStatpComboBox();
            showSearchItems(projSearch);
        }

        private void loadStatpComboBox()
        {

            loadStatp = true;

            cbStatp.DataSource = dataObject.GetValueList();

            cbStatp.ValueMember = "statp";
            cbStatp.DisplayMember = "statp";

            cbStatp.SelectedIndex = 0;

            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            lbldatef.Visible = false;

            loadStatp = false;

            BeginInvoke(new SearchDelegate(statpSearch));
        }

        /*Load the datagrid with the results from the Statp Search */

        private void statpSearch()
        {
            DataTable dt;

            //search all items
            dt = dataObject.GetProjectItemAccess(cbStatp.Text, "", "", "", "");
            dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgItem.RowHeadersVisible = false;

            dgItem.DataSource = dt;
            setItemColumnHeader();

            for (int i = 0; i < dgItem.ColumnCount; i++)
            {
                dgItem.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            Cursor.Current = Cursors.Default;

            if (dt.Rows.Count == 0)
            {
                groupBox1.Enabled = false;
                MessageBox.Show("No data to display.");
            }
            else
                groupBox1.Enabled = true;
        }

        public void showSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
        }

        private void setItemColumnHeader()
        {
            dgItem.Columns[0].HeaderText = "SURVEY DATE";
            dgItem.Columns[1].HeaderText = "ID";
            dgItem.Columns[2].HeaderText = "ACTION";
            dgItem.Columns[3].HeaderText = "USER";
            dgItem.Columns[4].HeaderText = "DATE/TIME";
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for ACTION and USRNME, show combo box
            if (cbItem.SelectedIndex == 1 || cbItem.SelectedIndex == 2)
            {
                PopulateValueCombo(cbItem.SelectedIndex, cbStatp.Text);
                cbValueItem.Visible = true;
                txtValueItem.Visible = false;
                lbldatef.Visible = false;
            }
            // for id and prgdtm, show text box
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
                    txtValueItem.MaxLength = 7; //if id
                }
            }
            txtValueItem.Text = "";
        }

        private void cbStatp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!loadStatp)
                statpSearch();
        }

        /*Setup value combo data, based on tab and combo index */

        private void PopulateValueCombo(int cbIndex, string selected_survey_data)
        {
            /*for Item access */

            cbValueItem.DataSource = dataObject.GetValueList(cbIndex, selected_survey_data);

            /*for action combo */
            if (cbIndex == 1)
            {
                cbValueItem.ValueMember = "action";
                cbValueItem.DisplayMember = "action";
            }

            /* for usrnme combo */
            else if (cbIndex == 2)
            {
                cbValueItem.ValueMember = "usrnme";
                cbValueItem.DisplayMember = "usrnme";
            }

            cbValueItem.SelectedIndex = -1;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbItem.SelectedIndex == -1)
            {
                MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                return;
            }
            else
            SearchItem();
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //for id only allow numbers
            if (cbItem.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
                //for date - allow back slashes and numbers only
            else if (cbItem.SelectedIndex == 3)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
            }
        }

        /*Item Search */
        private void SearchItem()
        {
            string statp = "";
            string id = "";
            string action = "";
            string usrnme = "";
            string prgdtm = "";
            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;

            if (Id == "")
            {
                statp = cbStatp.Text;

                //for id
                if (cbItem.SelectedIndex == 0)
                {
                    id = txtValueItem.Text.Trim();
                    if (!(id.Length == 7))
                    {
                        MessageBox.Show("ID should be 7 digits.");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }
                    else
                    {
                        CesampleData data_object = new CesampleData();
                        if (!data_object.CheckIdExist(id))
                        {
                            MessageBox.Show("Invalid ID.");
                            txtValueItem.Text = "";
                            txtValueItem.Focus();
                            return;
                        }
                    }
                }
                // for action
                else if (cbItem.SelectedIndex == 1)
                {
                    action = cbValueItem.Text.Trim();
                    if (action == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbValueItem.Focus();
                        return;
                    }
                }
                // for usrnme
                else if (cbItem.SelectedIndex == 2)
                {
                    usrnme = cbValueItem.Text.Trim();
                    if (usrnme == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbValueItem.Focus();
                    }
                }
                // for prgdtm
                else if (cbItem.SelectedIndex == 3)
                {

                    /*Verify date, and convert date to MM/DD/YYYY format */
                     if (String.IsNullOrEmpty(txtValueItem.Text.Trim()))
                        {
                            MessageBox.Show("Please enter a date.");
                            txtValueItem.Text = "";
                            txtValueItem.Focus();
                            return;
                        }
                        else
                        {                 
                        if (GeneralFunctions.VerifyDate(txtValueItem.Text.Trim()))
                            prgdtm = GeneralFunctions.ConvertDateFormat(txtValueItem.Text);
                        else
                        {
                            MessageBox.Show("Please enter correct date format.");
                            txtValueItem.Text = "";
                            txtValueItem.Focus();
                            return;
                        }
                    }
                }
            }
            else
                id = Id;

            //search all items
            if ((statp == "") && (id == "") && (action == "") && (usrnme == "") && (prgdtm == ""))
                dt = dtItem;
            else
                dt = dataObject.GetProjectItemAccess(statp, id, action, usrnme, prgdtm);

            dgItem.DataSource = dt;
            setItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if ((dt.Rows.Count == 0) && ((statp == "") || (id == "") ||
                (action == "") || (usrnme == "") || (prgdtm == "")))
            {
                MessageBox.Show("No data to display.");
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            lbldatef.Visible = false;
            SearchItem();
        }

        private void frmCeProjectAccess_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

    }
}
