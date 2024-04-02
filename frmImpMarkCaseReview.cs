
/**********************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : frmImpMarkCaseReview.cs	    	

Programmer      : Cestine Gill

Creation Date   : 08/06/2015

Inputs          : None

Parameters      : None
                  
Outputs         : None

Description     : This screen allows Analysts to review/search marked cases

Detailed Design : Marked Case Review Detailed Design

Other           : Called from: 
 
Revision History:	
***********************************************************************************
 Modified Date  :  
 Modified By    :  
 Keyword        :  
 Change Request :  
 Description    :  
***********************************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmImpMarkCaseReview : Cprs.frmCprsParent
    {
        //public property for this form 

        public string Id = string.Empty;
        public string user = UserInfo.UserName;

        //Private variables

        private DataTable dtItem = null;
        private ImpMarkCaseReviewData dataObject;

        //Populate the drop down search criteria combobox

        private string[] markCaseSearch = { "ID", "USER", "DATE" };

        public frmImpMarkCaseReview()
        {
            InitializeComponent(); 
            dataObject = new ImpMarkCaseReviewData();
        }

        private void frmImpMarkCase_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            showSearchItems(markCaseSearch);

            //obtain the  data from the CPRSBLL ImpTabReview.cs 
            //class library 

            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            lbldatef.Visible = false;
            dgReview.RowHeadersVisible = false;

            //load all data
            dtItem = dataObject.GetImpMarkCase("", "", "");
            dgReview.DataSource = dtItem;
            setItemColumnHeader();

            lblCasesCount.Text = dgReview.Rows.Count.ToString() + " MARK CASES";
        }

        //Populates and formats the Mark Case table 

        private void setItemColumnHeader()
        {

            int width = dgReview.RowHeadersWidth;
           
            for (int i = 0; i < dgReview.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgReview.Columns[i].HeaderText = "ID";
                    dgReview.Columns[i].Width = 85;
                }
                if (i == 1)
                {
                    dgReview.Columns[i].HeaderText = "USER";
                    dgReview.Columns[i].Width = 110;
                }

                if (i == 2)
                {
                    dgReview.Columns[i].HeaderText = "DATE/TIME";
                    dgReview.Columns[i].Width = 110;
                }

                if (i == 3)
                {
                    dgReview.Columns[i].HeaderText = "NOTE";

                    //Do not allow sorting on the NOTES column

                    dgReview.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                    //This code is necessary to set the minimum length 
                    //of the comments column 
                    //while still showing the scroll bar when needed. 

                    dgReview.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    width += dgReview.Columns[i].Width;

                    if (width < dgReview.Width)
                    {
                        dgReview.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        //Populate the improvements search criteria combobox

        private void showSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for USRNME, show combo box

            if (cbItem.SelectedIndex == 1)
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

                if (cbItem.SelectedIndex == 2)
                {
                    lbldatef.Visible = true;
                    txtValueItem.MaxLength = 10;
                }
                else
                {
                    lbldatef.Visible = false;
                    txtValueItem.MaxLength = 7;
                }
                txtValueItem.Focus();
            }

            txtValueItem.Text = "";
        }

    private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //for id only allow numbers
            if (cbItem.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            //for date - allow back slashes and numbers only
            else if (cbItem.SelectedIndex == 2)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
            }
        }

        /*Setup value combo data, based on tab and combo index */

        private void PopulateValueCombo(int cbIndex)
        {
            /*for Item access */
         
            cbValueItem.DataSource = dataObject.GetValueList(cbIndex, user);

            /* for usrnme combo */

            if (cbIndex == 1)
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
    
        /*Item Search */

        private void SearchItem()
        {
            string id = "";
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
                    if (!(id.Length == 7))
                    {
                        MessageBox.Show("ID should be 7 digits.");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }
                    //check id exists in mark case
                    else if (!dataObject.CheckIdinMarkcases(id))
                    {
                        MessageBox.Show("Invalid ID.");
                        txtValueItem.Text = "";
                        txtValueItem.Focus();
                        return;
                    }
                }

                // for usrnme

                else if (cbItem.SelectedIndex == 1)
                {                    
                    usrnme = cbValueItem.Text.Trim();

                    if (usrnme == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbValueItem.Focus();
                    }
                }

                // for prgdtm

                else if (cbItem.SelectedIndex == 2)
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

           
             dt = dataObject.GetImpMarkCase(usrnme, id, prgdtm);
           
            dgReview.DataSource = dt;
            lblCasesCount.Text = dgReview.Rows.Count.ToString() + " MARK CASES";
            setItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if ((dt.Rows.Count == 0) && ((usrnme != "") || (id != "") || (prgdtm != "")))
            {
                MessageBox.Show("No data to display.");
            }

            if (dt.Rows.Count == 0)
            {
                btnData.Enabled = false;
                btnDelete.Enabled = false;
            }
            if (dt.Rows.Count > 0)
            {
                btnData.Enabled = true;
                string user1 = dgReview.CurrentRow.Cells[1].Value.ToString();
                if (user1 != UserInfo.UserName)
                    btnDelete.Enabled = false;
                else
                    btnDelete.Enabled = true;
            }
        }

        //Clear the search results

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            lbldatef.Visible = false;
            SearchItem();
        }

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             
                //Display the verification popup to ensure the user 
                //wants to delete the marked case

                frmVerifyDeletePopup popup = new frmVerifyDeletePopup();

                DialogResult dialogresult = popup.ShowDialog();

                if (dialogresult == DialogResult.Yes)
                {
                    string id = dgReview.CurrentRow.Cells[0].Value.ToString();
                    string user = dgReview.CurrentRow.Cells[1].Value.ToString();

                    //Assign the dataObject coming from the DAL
                    //Refresh the data grid display

                    dataObject.DeleteRow(id, user);

                    cbItem.SelectedIndex = -1;
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Visible = false;
                    txtValueItem.Visible = false;
                    lbldatef.Visible = false;

                DataTable dt;
                    dt = dataObject.GetImpMarkCase("", "", "");
                    
                    dgReview.DataSource = dt;
                    setItemColumnHeader();
                    lblCasesCount.Text = dgReview.Rows.Count.ToString() + " MARK CASES";

                if (dt.Rows.Count == 0)
                    {
                        btnDelete.Enabled = false;
                        btnData.Enabled = false;
                    }
                    else
                    {
                        string user1 = dgReview.CurrentRow.Cells[1].Value.ToString();
                        if (user1 != UserInfo.UserName)
                            btnDelete.Enabled = false;
                        else
                            btnDelete.Enabled = true;
                        btnData.Enabled = true;
                    }

            }

                popup.Dispose();
                             
        }

        private void btnData_Click(object sender, EventArgs e)
        {

            //Get the id for the selected row data

            string id = dgReview.CurrentRow.Cells[0].Value.ToString();
            List<string> Idlist = new List<string>();
            int curr_index = 0;
            int cnt = 0;
            foreach (DataGridViewRow dr in dgReview.Rows)
            {
                string val = dr.Cells["ID"].Value.ToString();
                if (val.Length != 0)
                {
                    if (!Idlist.Contains(val))
                    {
                        Idlist.Add(val);
                        if (val == id)
                            curr_index = cnt;

                        cnt = cnt + 1;
                    }
                }
            }
            this.Hide();   // hide parent

            frmImprovements fm = new frmImprovements();
            fm.Id = id;
            fm.Idlist = Idlist;
            fm.CurrIndex = curr_index;
            fm.CallingForm = this;

            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");

            fm.ShowDialog();  // show child

            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");

            //reload form
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            lbldatef.Visible = false;

            DataTable dt;
            dt = dataObject.GetImpMarkCase("", "", "");
            dgReview.DataSource = dt;
            setItemColumnHeader();
            lblCasesCount.Text = dgReview.Rows.Count.ToString() + " MARK CASES";
        }

        //Print the table

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = "IMPROVEMENTS MARKED CASE REVIEW";

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Improvements Marked Case";

            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //resize the note column
            dgReview.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgReview.Columns[3].Width = 400;
            printer.PrintDataGridViewWithoutDialog(dgReview);

            //resize back the note column
            dgReview.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Cursor.Current = Cursors.Default;
        }

       
        private void frmImpMarkCaseReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

        private void dgReview_SelectionChanged(object sender, EventArgs e)
        {
            //Get the user for the selected row data
            if (dgReview.CurrentRow == null) return;
            string user = dgReview.CurrentRow.Cells[1].Value.ToString();
            if (user != UserInfo.UserName)
                btnDelete.Enabled = false;
            else
                btnDelete.Enabled = true;
        }
    }
}
