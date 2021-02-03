/************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmCeProjectAudit.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/23/2015

Inputs:             CprsDLL.CeProjectAuditData
                   
Parameters:		    None 

Outputs:		    None	

Description:	    This screen will review/search changes made to 
                    CeAudit Improvement Projects table

Detailed Design:    Detailed User Requirements for the Ceaudit Project Audit 
                    Information Screen 

Other:	            Called from: 
 
Revision History:	
**********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**********************************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CprsDAL;
using System.Text.RegularExpressions;

namespace Cprs
{
    public partial class frmCeProjectAudit : Cprs.frmCprsParent
    {

        //public property for this form 

        public string Id = string.Empty;
        public Form callingForm = null;

        //Private variables

        private DataTable dtItem = null;
        private CeProjectAuditData dataObject;

        private string[] projSearch = { "ID", "VARNME", "USER", "DATE" };

        public frmCeProjectAudit()
        {
            InitializeComponent();
            
            //create instance of data object

            dataObject = new CeProjectAuditData();
        }

        private void frmCeProjectAudit_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            GetProjAudit();
            showSearchItems(projSearch);                    
        }

        public void showSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
        }

          private void GetProjAudit()
          {
              txtValueItem.Visible = false;
              cbValueItem.Visible = false;
              lbldatef.Visible = false;

              dgItem.RowHeadersVisible = false;

              //for all audit data, show search functions

              if (Id == "")
              {
                  /* Retrieve audits data */

                  dtItem = dataObject.GetProjectItemAudits("", "", "", "");

                  /************************this will change**********************************/
                  btnPrevious.Visible = false;
              }
              else
              {
                  //for one case, hide search functions
                  btnPrevious.Visible = true;
                  groupBox1.Visible = false;
                  dtItem = dataObject.GetProjectItemAudits(Id, "", "", "");
              }

              dgItem.DataSource = dtItem;
              setItemColumnHeader();

              /*resize the columns*/

              for (int i = 0; i < dgItem.ColumnCount; i++)
              {

                  dgItem.Columns[0].Width = 100;
                  dgItem.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
              }
            
          }

        private void setItemColumnHeader()
        {
            dgItem.Columns[0].HeaderText = "ID";
            dgItem.Columns[1].HeaderText = "INTERVIEW";
            dgItem.Columns[2].HeaderText = "JOBCODE";
            dgItem.Columns[3].HeaderText = "VARNME";
            dgItem.Columns[4].HeaderText = "OLDVAL";
            dgItem.Columns[4].DefaultCellStyle.Format = "N0";
            dgItem.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgItem.Columns[5].HeaderText = "OLDFLAG";
            dgItem.Columns[6].HeaderText = "NEWVAL";
            dgItem.Columns[6].DefaultCellStyle.Format = "N0";
            dgItem.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgItem.Columns[7].HeaderText = "NEWFLAG";
            dgItem.Columns[8].HeaderText = "USER";
            dgItem.Columns[9].HeaderText = "DATE/TIME";
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            //for VARNME and USRNME, show combo box

            if (cbItem.SelectedIndex == 1 || cbItem.SelectedIndex == 2)
            {
                PopulateValueCombo(cbItem.SelectedIndex);
                cbValueItem.Visible = true;
                txtValueItem.Visible = false;
                lbldatef.Visible = false;
            }

             // for id and prgdtm, show text box

            else
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;

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
                txtValueItem.Focus();
            }

            txtValueItem.Text = "";
        }

        /*Setup value combo data, based on tab and combo index */

        private void PopulateValueCombo(int cbIndex)
        {

            /*for Item audit */

            cbValueItem.DataSource = dataObject.GetValueList(cbIndex);

            /*for varnme combobox */

            if (cbIndex == 1)
            {
                cbValueItem.ValueMember = "varnme";
                cbValueItem.DisplayMember = "varnme";
            }

            /* for usrnme combobox */

            else
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
            string id = "";
            string varnme = "";
            string usrnme = "";
            string prgdtm = "";

            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;

            if (Id == "")
            {
 
                //for id

                if (cbItem.SelectedIndex == 0)
                {
                    id = txtValueItem.Text.Trim();
                    if ((id.Length != 7))
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

                // for varnme

                else if (cbItem.SelectedIndex == 1)
                {
                    varnme = cbValueItem.Text.Trim();

                    if (varnme == "")
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

            if ((id == "") && (varnme == "") && (usrnme == "") && (prgdtm == ""))
                dt = dtItem;
            else
                dt = dataObject.GetProjectItemAudits(id, varnme, usrnme, prgdtm);

            dgItem.DataSource = dt;
            setItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if ((dt.Rows.Count == 0) && ((id == "") || (varnme == "") || 
                (usrnme == "") || (prgdtm == "")))
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

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
            callingForm.Show();
        }

        private void frmCeProjectAudit_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }
    }
}
