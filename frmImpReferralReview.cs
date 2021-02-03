/**********************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : frmImpReferralReview.cs

Programmer      : Cestine Gill

Creation Date   : 12/29/2015

Inputs          : None

Parameters      : None
                  
Outputs         : None

Description     : This screen allows Analysts to review/search
                  improvements referral notes

Detailed Design : Detailed Design for Improvements Referral Review

Other           : Called from: frmCprsParent.cs Improvements Menu
 
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
    public partial class frmImpReferralReview : Cprs.frmCprsParent
    {
        public static frmImpReferralReview Current;

        //public property for this form 

        public string Id = string.Empty;

        //Private variables

        private DataTable dtProjItem = null;
        private DataTable dt = null;

        private ImpReferralData dataObject;
        //Populate the drop down search criteria combobox

        private string[] projSearch = { "ID", "TYPE", "STATUS", "USER", "GROUP", "DATE" };

        public frmImpReferralReview()
        {
            InitializeComponent();
        }

        private void frmImpReferralReview_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            GetProj();
            showSearchItems(projSearch);
        }

        //This method will re-populate the datagrid after
        //after each search occurs

        private void LoadTables()
        {           
            GetProj();
        }

        private void GetProj()
        {
            dataObject = new ImpReferralData();

            // set search components to invisible

            txtPROJValueItem.Visible = false;
            cbPROJValueItem.Visible = false;
            lbldatef.Visible = false;

            dgProjReferrals.RowHeadersVisible = false;

            //populate the data grid without any search criteria

            dtProjItem = dataObject.GetCEReferralReviewTable("", "", "", "", "", "");

            dgProjReferrals.DataSource = dtProjItem;

            //display the row count on the screen

            lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " REFERRALS";

            setProjItemColumnHeader();

            //reassign the value from the database to a 
            //user-friendlier representation in the table

            int reftypeColumn = 1; //Put your column number here
            int refgroupColumn = 4;
            int refstatusColumn = 2;

            for (int i = 0; i < dtProjItem.Rows.Count; i++)
            {

                switch (dtProjItem.Rows[i][reftypeColumn].ToString())
                {
                    case "2":
                        dtProjItem.Rows[i][reftypeColumn] = "Correct Flags";
                        break;
                    case "3":
                        dtProjItem.Rows[i][reftypeColumn] = "Data Issue";
                        break;
                    case "5":
                        dtProjItem.Rows[i][reftypeColumn] = "Free Form";
                        break;
                }

                switch (dtProjItem.Rows[i][refgroupColumn].ToString())
                {
                    case "1":
                        dtProjItem.Rows[i][refgroupColumn] = "HQ Supervisor";
                        break;
                    case "2":
                        dtProjItem.Rows[i][refgroupColumn] = "HQ Analyst";
                        break;

                }
                switch (dtProjItem.Rows[i][refstatusColumn].ToString())
                {
                    case "A":
                        dtProjItem.Rows[i][refstatusColumn] = "Active";
                        break;
                    case "C":
                        dtProjItem.Rows[i][refstatusColumn] = "Complete";
                        break;
                }
            }
        }

        //format the datagrid and columns

        private void setProjItemColumnHeader()
        {
            dgProjReferrals.RowHeadersVisible = false;
            dgProjReferrals.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgProjReferrals.RowHeadersVisible = false;  // set it to false if not needed
            dgProjReferrals.ShowCellToolTips = false;

            for (int i = 0; i < dgProjReferrals.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgProjReferrals.Columns[i].HeaderText = "ID";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 1)
                {
                    dgProjReferrals.Columns[i].HeaderText = "TYPE";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 2)
                {
                    dgProjReferrals.Columns[i].HeaderText = "STATUS";
                    dgProjReferrals.Columns[i].Width = 80;

                }
                if (i == 3)
                {
                    dgProjReferrals.Columns[i].HeaderText = "USER";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 4)
                {
                    dgProjReferrals.Columns[i].HeaderText = "GROUP";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 5)
                {
                    dgProjReferrals.Columns[i].HeaderText = "DATE/TIME";
                    dgProjReferrals.Columns[i].Width = 120;

                }
                if (i == 6)
                {
                    dgProjReferrals.Columns[i].HeaderText = "NOTE";
                    dgProjReferrals.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgProjReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        //Populate the improvements search criteria combobox

        private void showSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
        }

        //The search criteria combo box 
        //will change depending on the search item combobox selected

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            //for USRNME, REFTYPE, REFSTATUS, GROUP show combo boxes

            if (cbItem.SelectedIndex == 1 ||
                cbItem.SelectedIndex == 2 ||
                cbItem.SelectedIndex == 3 ||
                cbItem.SelectedIndex == 4)
            {
                PopulateProjValueCombo(cbItem.SelectedIndex);

                cbPROJValueItem.Visible = true;
                txtPROJValueItem.Visible = false;

                lbldatef.Visible = false;
            }

            // for id/respid and prgdtm, show text boxes

            else
            {
                cbPROJValueItem.Visible = false;
                txtPROJValueItem.Visible = true;
                txtPROJValueItem.Focus();
                if (cbItem.SelectedIndex == 5)
                {
                    lbldatef.Visible = true;
                    txtPROJValueItem.MaxLength = 10;
                }
                else
                {
                    lbldatef.Visible = false;
                    txtPROJValueItem.MaxLength = 7;
                }
            }

            txtPROJValueItem.Text = "";
        }

        private void txtPROJValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //for id only allow numbers
            if (cbItem.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            //for date - allow back slashes and numbers only
            else if (cbItem.SelectedIndex == 5)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
            }
        }

        /*Setup value combo data, based on tab and combo index */

        private void PopulateProjValueCombo(int cbIndex)
        {

            /*for Item access */

            dt = dataObject.GetCEValueList(cbIndex);

            /* for type combo */

            if (cbIndex == 1)
            {

                //reassign the value from the database to a 
                //user-friendlier representation in the combobox

                //add a desciption column

                dt.Columns.Add("description", typeof(String));

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0].ToString() == "2") //select a row where reftype = "2"
                    {
                        dr[1] = "Correct Flags";
                    }
                    if (dr[0].ToString() == "3") //select a row where reftype = "3"
                    {
                        dr[1] = "Data Issue";
                    }
                    if (dr[0].ToString() == "5") //select a row where reftype = "5"
                    {
                        dr[1] = "Free Form";
                    }
                }

                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "reftype"; // column name which you want to select              
                cbPROJValueItem.DisplayMember = "description"; //Field in the datatable which you want to be the value of the combobox 
            }

            /* for status combo */

            if (cbIndex == 2)
            {

                dt.Columns.Add("description", typeof(String));

                foreach (DataRow dr in dt.Rows)
                {
                    if (dr[0].ToString() == "A") //select row where refstatus = "A"
                    {
                        //Update column value
                        dr[1] = "Active"; //description column index
                    }
                    if (dr[0].ToString() == "C") //select row where reftype = "C"
                    {
                        dr[1] = "Complete";
                    }
                }

                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "refstatus";
                cbPROJValueItem.DisplayMember = "description";
            }

            /* for usrnme combo */

            if (cbIndex == 3)
            {
                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "usrnme";
                cbPROJValueItem.DisplayMember = "usrnme";
            }

            /* for group combo */

            if (cbIndex == 4)
            {

                dt.Columns.Add("description", typeof(String));

                foreach (DataRow dr in dt.Rows)
                {

                    if (dr[0].ToString() == "1") //select a row where refgroup = "1"
                    {
                        //Update column value
                        dr[1] = "HQ Supervisor"; // description column index
                    }
                    if (dr[0].ToString() == "2") //select a row where refgroup = "2"
                    {
                        dr[1] = "HQ Analyst";
                    }
                }

                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "refgroup";
                cbPROJValueItem.DisplayMember = "description";
            }

            cbPROJValueItem.SelectedIndex = -1;
        }

        //search button

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

        //perform the search and repopulate the datagrid

        private void SearchItem()
        {

            string id = "";
            string usrnme = "";
            string prgdtm = "";
            string type = "";
            string status = "";
            string group = "";

            DataTable dt;

            Cursor.Current = Cursors.WaitCursor;

            if (Id == "")
            {

                //for id

                if (cbItem.SelectedIndex == 0)
                {
                    id = txtPROJValueItem.Text.Trim();
                    if (!(id.Length == 7))
                    {
                        MessageBox.Show("ID should be 7 digits.");
                        txtPROJValueItem.Text = "";
                        txtPROJValueItem.Focus();
                        return;
                    }
                    //check id exists in referral
                    else if (!dataObject.CheckReferralExist(id))
                    {
                        MessageBox.Show("Invalid ID.");
                        txtPROJValueItem.Text = "";
                        txtPROJValueItem.Focus();
                        return;
                    }
                }

                // for type

                else if (cbItem.SelectedIndex == 1)
                {
                    type = cbPROJValueItem.Text;
                    
                    if (type == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbPROJValueItem.Focus();
                    }

                    //Reassign the radio button values to their database equivalent

                    switch (type)
                    {
                        case "Correct Flags": type = "2"; break;
                        case "Data Issue": type = "3"; break;
                        case "Free Form": type = "5"; break;
                    }
                }

                // for status

                else if (cbItem.SelectedIndex == 2)
                {
                    status = cbPROJValueItem.Text;
                 
                    if (status == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbPROJValueItem.Focus();
                    }

                    switch (status)
                    {
                        case "Active": status = "A"; break;
                        case "Complete": status = "C"; break;
                    }
                }

                // for usrname

                else if (cbItem.SelectedIndex == 3)
                {
                    usrnme = cbPROJValueItem.Text.Trim();
                    if (usrnme == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbPROJValueItem.Focus();
                    }
                }

                // for group

                else if (cbItem.SelectedIndex == 4)
                {
                    group = cbPROJValueItem.Text;

                    if (group == "")
                    {
                        MessageBox.Show("A value must be selected from the Drop Down Menu.");
                        cbPROJValueItem.Focus();
                    }
                    switch (group)
                    {
                        case "HQ Supervisor": group = "1"; break;
                        case "HQ Analyst": group = "2"; break;
                    }
                }

                // for prgdtm

                else if (cbItem.SelectedIndex == 5)
                {
                    /*Verify date, and convert date to MM/DD/YYYY format */
                    if (String.IsNullOrEmpty(txtPROJValueItem.Text.Trim()))
                    {
                        MessageBox.Show("Please enter a date.");
                        txtPROJValueItem.Text = "";
                        txtPROJValueItem.Focus();
                        return;
                    }
                    else
                    {
                        if (GeneralFunctions.VerifyDate(txtPROJValueItem.Text.Trim()))
                            prgdtm = GeneralFunctions.ConvertDateFormat(txtPROJValueItem.Text);
                        else
                        {
                            MessageBox.Show("Please enter correct date format.");
                            txtPROJValueItem.Text = "";
                            txtPROJValueItem.Focus();
                            return;
                        }
                    }
                }
            }
            else
                id = Id;

            if ((usrnme == "") && (id == "") && (prgdtm == "") && (type == "") && (status == "") && (group == ""))
            {
                dt = dtProjItem;
            }
            else
            {
                dt = dataObject.GetCEReferralReviewTable(id, type, group, status, usrnme, prgdtm);

                //When the table refreshes after the search re-assign the values

                int reftypeColumn = 1; //Put your column number here
                int refgroupColumn = 4;
                int refstatusColumn = 2;

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    switch (dt.Rows[i][reftypeColumn].ToString())
                    {
                        case "2":
                            dt.Rows[i][reftypeColumn] = "Correct Flags";
                            break;
                        case "3":
                            dt.Rows[i][reftypeColumn] = "Data Issue";
                            break;
                        case "5":
                            dt.Rows[i][reftypeColumn] = "Free Form";
                            break;
                    }

                    switch (dt.Rows[i][refgroupColumn].ToString())
                    {
                        case "1":
                            dt.Rows[i][refgroupColumn] = "HQ Supervisor";
                            break;
                        case "2":
                            dt.Rows[i][refgroupColumn] = "HQ Analyst";
                            break;
                    }
                    switch (dt.Rows[i][refstatusColumn].ToString())
                    {
                        case "A":
                            dt.Rows[i][refstatusColumn] = "Active";
                            break;
                        case "C":
                            dt.Rows[i][refstatusColumn] = "Complete";
                            break;
                    }
                }
            }

            dgProjReferrals.DataSource = dt;

            //display the row count on the screen after search

            lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " REFERRALS";

            setProjItemColumnHeader();

            Cursor.Current = Cursors.Default;

            if ((dt.Rows.Count == 0) && ((usrnme != "") || (id != "") || 
                (prgdtm != "") || (type != "") || (status != "") || (group != "")))
            {
                MessageBox.Show("No data to display.");
            }
        }

        //Clear the search combo boxes

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPROJValueItem.Clear();

            cbItem.SelectedIndex = -1;
            cbPROJValueItem.SelectedIndex = -1;

            cbPROJValueItem.Visible = false;
            txtPROJValueItem.Visible = false;

            SearchItem();
        }

        //Go to the Improvements Screen

        private void btnData_Click(object sender, EventArgs e)
        {
           
            if (dgProjReferrals.SelectedRows.Count > 0)
            {
                //Get the values for the selected row data

                string Id = dgProjReferrals.SelectedRows[0].Cells[0].Value.ToString();
                List<string> Idlist = new List<string>();
                int curr_index = 0;
                int cnt = 0;
                foreach (DataGridViewRow dr in dgProjReferrals.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    if (val.Length != 0)
                    {
                        if (!Idlist.Contains(val))
                        {
                            Idlist.Add(val);
                            if (val == Id)
                                curr_index = cnt;

                            cnt = cnt + 1;
                        }
                    }
                }

                this.Hide();   // hide parent

                frmImprovements fIMP = new frmImprovements();

                //Get the id for the selected row and pass to form

                fIMP.Id = Id;
                fIMP.Idlist = Idlist;
                fIMP.CurrIndex = curr_index;
                fIMP.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");

                fIMP.ShowDialog();  // show child

                GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");

                //reload data
                LoadTables();
            }
        }

        /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            
            printer.Title = "IMPROVEMENTS REFERRALS";
            
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
           
            printer.printDocument.DocumentName = "Improvement Referrals";
           
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //resize the note column
            dgProjReferrals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgProjReferrals.Columns[6].Width = 300;
            printer.PrintDataGridViewWithoutDialog(dgProjReferrals);

            //resize back the note column
            dgProjReferrals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Cursor.Current = Cursors.Default;
        }

     
        private void frmImpReferralReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }
    }
}
