/**********************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : frmMarkCaseReview.cs	    	

Programmer      : Cestine Gill

Creation Date   : 09/04/2015

Inputs          : RESPID, ID

Parameters      : None
                  
Outputs         : None

Description     : This screen allows Analysts to review/search marked cases

Detailed Design : Marked Case Review Detailed Design

Other           : Called from: 
 
Revision History:	
***********************************************************************************
 Modified Date  :  8/27/2015
 Modified By    :  Diane Musachio
 Keyword        :  None
 Change Request :  None
 Description    :  Add code for Data Button to Call Respondent Update screen
***********************************************************************************
 Modified Date  :  10/12/2016
 Modified By    :  Christine Zhang
 Keyword        :  None
 Change Request :  None
 Description    :  Change Tab style
 ***********************************************************************************
 Modified Date  :  06/06/2016
 Modified By    :  Christine Zhang
 Keyword        :  None
 Change Request :  CR 200
 Description    :  Update Popup Messages to conform with other screens
***********************************************************************************
 Modified Date  :  09/19/2019
 Modified By    :  Christine Zhang
 Keyword        :  None
 Change Request :  CR 3572
 Description    :  allow HQ manager, programmer and Person who created delete marks
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
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmMarkCaseReview : Cprs.frmCprsParent
    {
        public static frmMarkCaseReview Current;

        //public property for this form 

        public string Respid = string.Empty;
        public string Id = string.Empty;

        private string user = UserInfo.UserName;

        //Private variables

        private DataTable dtIDItem = null;
        private DataTable dtRSPItem = null;

        private MarkCaseReviewData dataObject;

        //Populate the drop down search by combobox depending on
        //whether project search or respondent search

        private string[] idSearch = { "ID", "NEWTC", "USER", "DATE" };
        private string[] rspSearch = { "RESPID", "USER", "DATE" };

        public frmMarkCaseReview()
        {
            InitializeComponent();
        }

        private void frmMarkCase_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            SetButtonTxt();

            lblTab.Text = tbCaseReview.SelectedTab.Text;

            GetProj();
            showSearchItems(idSearch);

            GetResp();

            if (dgProjMarkCase.RowCount == 0)
            {
                lblCasesCount.Text = dgRespMarkCase.Rows.Count.ToString() + " RESPONDENT MARK CASES";
            }
            else
            {
                lblCasesCount.Text = dgProjMarkCase.Rows.Count.ToString() + " PROJECT MARK CASES";
            }

        }

        private void GetProj()
        {
            dataObject = new MarkCaseReviewData();

            txtIDValueItem.Visible = false;
            cbIDValueItem.Visible = false;
            cbItem.SelectedIndex = -1;

            lbldatef.Visible = false;

            dgProjMarkCase.RowHeadersVisible = false;

            dtIDItem = dataObject.GetMarkCase("", "", "", "");
            
            lblTab.Location = new System.Drawing.Point(580, 97);

            dgProjMarkCase.DataSource = dtIDItem;

            setProjItemColumnHeader();

            if (tbCaseReview.SelectedIndex == 0)
            {
                if (dtIDItem.Rows.Count > 0)
                {
                    btnData.Enabled = true;

                    if (dgProjMarkCase.RowCount > 0)
                    {
                        string user1 = dgProjMarkCase.Rows[0].Cells[4].Value.ToString();
                        if (user1 != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                            btnDelete.Enabled = false;
                        else
                            btnDelete.Enabled = true;
                    }
                    else
                        btnDelete.Enabled = false;
                }
                else
                {
                    btnData.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void GetResp()
        {
            dataObject = new MarkCaseReviewData();
            cbItem.SelectedIndex = -1;
            txtRESPValueItem.Visible = false;
            cbRESPValueItem.Visible = false;

            lbldatef.Visible = false;

            dgRespMarkCase.RowHeadersVisible = false;

            dtRSPItem = dataObject.GetRSPMarkCase("", "", "");
            
            lblTab.Location = new System.Drawing.Point(563, 97);

            dgRespMarkCase.DataSource = dtRSPItem;

            setRespItemColumnHeader();

            if (tbCaseReview.SelectedIndex == 1)
            {
                if (dtRSPItem.Rows.Count > 0)
                {
                    btnData.Enabled = true;
                    if (dgRespMarkCase.RowCount > 0)
                    {
                        string user1 = dgRespMarkCase.CurrentRow.Cells[1].Value.ToString();
                        if (user1 != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                            btnDelete.Enabled = false;
                        else
                            btnDelete.Enabled = true;
                    }
                    else
                        btnDelete.Enabled = false;
                }
                else
                {
                    btnData.Enabled = false;
                    btnDelete.Enabled = false;
                }
            }
        }

        private void LoadTables()
        {
            GetProj();
            GetResp();

            if (tbCaseReview.SelectedIndex == 0)
                lblCasesCount.Text = dgProjMarkCase.Rows.Count.ToString() + " PROJECT MARK CASES";
            if (tbCaseReview.SelectedIndex == 1)
                lblCasesCount.Text = dgRespMarkCase.Rows.Count.ToString() + " RESPONDENT MARK CASES";
            
        }

        private void tbCaseReview_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbCaseReview.Refresh();

            if (tbCaseReview.SelectedIndex == 0)
            {
                GetProj();
                cbRESPValueItem.Visible = false;
                txtRESPValueItem.Visible = false;
                lblCasesCount.Text = dgProjMarkCase.Rows.Count.ToString() + " PROJECT MARK CASES";


            }
            if (tbCaseReview.SelectedIndex == 1)
            {
                GetResp();
                cbIDValueItem.Visible = false;
                txtIDValueItem.Visible = false;
                lblCasesCount.Text = dgRespMarkCase.Rows.Count.ToString() + " RESPONDENT MARK CASES";
            }
        }

        private void tbCaseReview_Selected(object sender, TabControlEventArgs e)
        {

            if (tbCaseReview.SelectedIndex == 0)
            {
                lblTab.Text = tbProject.Text;
                lblTab.Location = new System.Drawing.Point(580, 97);
                showSearchItems(idSearch);
                SetButtonTxt();

            }
            if (tbCaseReview.SelectedIndex == 1)
            {
                lblTab.Text = tbRespondent.Text;
                lblTab.Location = new System.Drawing.Point(563, 97);
                showSearchItems(rspSearch);
                btnData.Text = "DATA";

            }
        }

        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:
                    btnData.Text = "C-700";
                    break;
                case EnumGroups.HQManager:
                    btnData.Text = "C-700";
                    break;
                case EnumGroups.HQAnalyst:
                    btnData.Text = "C-700";
                    break;
                case EnumGroups.NPCManager:
                    btnData.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:
                    btnData.Text = "TFU";
                    break;
                case EnumGroups.NPCInterviewer:
                    btnData.Text = "TFU";
                    break;
                case EnumGroups.HQSupport:
                    btnData.Text = "C-700";
                    break;
                case EnumGroups.HQMathStat:
                    btnData.Text = "C-700";
                    break;
                case EnumGroups.HQTester:
                    btnData.Text = "C-700";
                    break;
            }
        }

        public void showSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
        }

        public void showRSPSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
        }

        //Populates and formats the Project Mark Case table 

        private void setProjItemColumnHeader()
        {

            int width = dgProjMarkCase.RowHeadersWidth;

            for (int i = 0; i < dgProjMarkCase.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "ID";
                    dgProjMarkCase.Columns[i].Width = 85;
                }
                if (i == 1)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "NEWTC";
                    dgProjMarkCase.Columns[i].Width = 85;
                }
                if (i == 2)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "OWNER";
                    dgProjMarkCase.Columns[i].Width = 80;
                }
                if (i == 3)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "RVITM5C";
                    dgProjMarkCase.Columns[i].Width = 90;
                }
                if (i == 4)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "USER";
                    dgProjMarkCase.Columns[i].Width = 110;
                }
                if (i == 5)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "DATE/TIME";
                    dgProjMarkCase.Columns[i].Width = 120;
                }
                if (i == 6)
                {
                    dgProjMarkCase.Columns[i].HeaderText = "NOTE";

                    //Do not allow sorting on the NOTES column

                    dgProjMarkCase.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                    //This code is necessary to set the minimum length 
                    //of the comments column 
                    //while still showing the scroll bar when needed. 

                    dgProjMarkCase.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    width += dgProjMarkCase.Columns[i].Width;

                    if (width < dgProjMarkCase.Width)
                    {
                        dgProjMarkCase.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        //Populates and formats the Mark Case table 

        private void setRespItemColumnHeader()
        {

            int width = dgRespMarkCase.RowHeadersWidth;

            for (int i = 0; i < dgRespMarkCase.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgRespMarkCase.Columns[i].HeaderText = "RESPID";
                    dgRespMarkCase.Columns[i].Width = 85;
                }
                if (i == 1)
                {
                    dgRespMarkCase.Columns[i].HeaderText = "USER";
                    dgRespMarkCase.Columns[i].Width = 110;
                }

                if (i == 2)
                {
                    dgRespMarkCase.Columns[i].HeaderText = "DATE/TIME";
                    dgRespMarkCase.Columns[i].Width = 120;
                }

                if (i == 3)
                {
                    dgRespMarkCase.Columns[i].HeaderText = "NOTE";

                    //Do not allow sorting on the NOTES column

                    dgRespMarkCase.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                    //This code is necessary to set the minimum length 
                    //of the comments column 
                    //while still showing the scroll bar when needed. 

                    dgRespMarkCase.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    width += dgRespMarkCase.Columns[i].Width;

                    if (width < dgRespMarkCase.Width)
                    {
                        dgRespMarkCase.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
            }
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tbCaseReview.SelectedIndex == 0)
            {
                //for NEWTC and USRNME, show combo box

                if (cbItem.SelectedIndex == 1 || cbItem.SelectedIndex == 2)
                {
                    PopulateIDValueCombo(cbItem.SelectedIndex);

                    cbIDValueItem.Visible = true;
                    txtIDValueItem.Visible = false;

                    lbldatef.Visible = false;

                }

                // for id/respid and prgdtm, show text box

                else
                {
                    cbIDValueItem.Visible = false;
                    txtIDValueItem.Visible = true;
                    txtIDValueItem.Focus();
                    if (cbItem.SelectedIndex == 3)
                    {
                        lbldatef.Visible = true;
                        txtIDValueItem.MaxLength = 10;
                        
                    }
                    else
                    {
                        lbldatef.Visible = false;
                        txtIDValueItem.MaxLength = 7; //if id
                    }
                }

                txtIDValueItem.Text = "";
            }

            if (tbCaseReview.SelectedIndex == 1)
            {
                if (cbItem.SelectedIndex == 1)
                {
                    PopulateRSPValueCombo(cbItem.SelectedIndex);

                    cbRESPValueItem.Visible = true;
                    txtRESPValueItem.Visible = false;

                    lbldatef.Visible = false;
                }

               // for id/respid and prgdtm, show text box

                else
                {
                    cbRESPValueItem.Visible = false;
                    txtRESPValueItem.Visible = true;
                    txtRESPValueItem.Focus();
                    if (cbItem.SelectedIndex == 2)
                    {
                        lbldatef.Visible = true;
                        txtRESPValueItem.MaxLength = 10;
                    }
                    else
                    {
                        lbldatef.Visible = false;
                        txtRESPValueItem.MaxLength = 7;
                    }
                }

                txtRESPValueItem.Text = "";
            }
        }

        private void txtIDValueItem_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtRESPValueItem_KeyPress(object sender, KeyPressEventArgs e)
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

        private void PopulateIDValueCombo(int cbIndex)
        {

            /*for Item access */

            cbIDValueItem.DataSource = dataObject.GetValueList(cbIndex, user);

            // for newtc

            if (cbIndex == 1)
            {
                cbIDValueItem.ValueMember = "newtc";
                cbIDValueItem.DisplayMember = "newtc";
            }

            /* for usrnme combo */

            else if (cbIndex == 2)
            {
                cbIDValueItem.ValueMember = "usrnme";
                cbIDValueItem.DisplayMember = "usrnme";
            }

            cbIDValueItem.SelectedIndex = -1;
        }

        private void PopulateRSPValueCombo(int cbIndex)
        {

            /*for Item access */

            cbRESPValueItem.DataSource = dataObject.GetRSPValueList(cbIndex, user);

            /* for usrnme combo */

            if (cbIndex == 1)
            {
                cbRESPValueItem.ValueMember = "usrnme";
                cbRESPValueItem.DisplayMember = "usrnme";
            }

            cbRESPValueItem.SelectedIndex = -1;
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
            if (tbCaseReview.SelectedIndex == 0)
            {

                string id = "";
                string usrnme = "";
                string prgdtm = "";
                string newtc = "";

                DataTable dt;

                Cursor.Current = Cursors.WaitCursor;

                if (Id == "")
                {
                    
                    //for id

                    if (cbItem.SelectedIndex == 0)
                    {
                        id = txtIDValueItem.Text.Trim();

                        if (!(id.Length == 7))
                        {
                            MessageBox.Show("ID should be 7 digits.");
                            txtIDValueItem.Text = "";
                            txtIDValueItem.Focus();
                            return;
                        }
                        //check id exists in mark case
                        else if (!dataObject.CheckIdinMarkcases(id))
                        {
                            MessageBox.Show("Invalid ID.");
                            txtIDValueItem.Text = "";
                            txtIDValueItem.Focus();
                            return;
                        }
                    }

                    // for newtc

                    else if (cbItem.SelectedIndex == 1)
                    {
                        newtc = cbIDValueItem.Text.Trim();
                        if (newtc == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbIDValueItem.Focus();
                        }
                    }

                    // for usrnme

                    else if (cbItem.SelectedIndex == 2)
                    {
                        usrnme = cbIDValueItem.Text.Trim();
                        if (usrnme == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbIDValueItem.Focus();
                        }
                    }

                    // for prgdtm

                    else if (cbItem.SelectedIndex == 3)
                    {

                        /*Verify date, and convert date to MM/DD/YYYY format */
                        if (String.IsNullOrEmpty(txtIDValueItem.Text.Trim()))
                        {
                            MessageBox.Show("Please enter a date.");
                            txtIDValueItem.Focus();
                            return;
                        }
                        else
                        {
                            if (GeneralFunctions.VerifyDate(txtIDValueItem.Text.Trim()))
                                prgdtm = GeneralFunctions.ConvertDateFormat(txtIDValueItem.Text);

                            else
                            {
                                MessageBox.Show("Please enter correct date format.");
                                txtIDValueItem.Text = "";
                                txtIDValueItem.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                    id = Id;


                //search all items. If the user is a Programmer or HQSupervisor then
                //search by the username selected username in the combobox. 
                //Otherwise, search only the cases owned by the logged in user

                if ((usrnme == "") && (id == "") && (newtc == "") && (prgdtm == ""))
                {
                    dt = dataObject.GetMarkCase(usrnme, id, newtc, prgdtm);
                }
                else
                {
                    dt = dataObject.GetMarkCase(usrnme, id, newtc, prgdtm);
                }

                dgProjMarkCase.DataSource = dt;
                setProjItemColumnHeader();

                Cursor.Current = Cursors.Default;

                //If a search performed and no records found
                if ((dt.Rows.Count == 0) && ((usrnme != "") || (id != "") || (newtc != "") || (prgdtm != "")))
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
                    string user1 = dgProjMarkCase.CurrentRow.Cells[4].Value.ToString();
                    if (user1 != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                        btnDelete.Enabled = false;
                    else
                        btnDelete.Enabled = true;

                }
                lblCasesCount.Text = dgProjMarkCase.Rows.Count.ToString() + " PROJECT MARK CASES";
            }

            if (tbCaseReview.SelectedIndex == 1)
            {
                string respid = "";
                string usrnme = "";
                string prgdtm = "";

                DataTable rdt;

                Cursor.Current = Cursors.WaitCursor;

                if (Respid == "")
                {

                    //for respid

                    if (cbItem.SelectedIndex == 0)
                    {
                        respid = txtRESPValueItem.Text.Trim();
                        if (!(respid.Length == 7))
                        {
                            MessageBox.Show("RESPID should be 7 digits.");
                            txtRESPValueItem.Text = "";
                            txtRESPValueItem.Focus();
                            return;
                        }
                        //check respid exists
                        else if (!dataObject.CheckRespidinMarkcases(respid))
                        {
                            MessageBox.Show("Invalid RESPID.");
                            txtRESPValueItem.Text = "";
                            txtRESPValueItem.Focus();
                            return;
                        }
                    }

                    // for usrnme

                    else if (cbItem.SelectedIndex == 1)
                    {
                        usrnme = cbRESPValueItem.Text.Trim();
                        if (usrnme == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }
                    }

                    // for prgdtm

                    else if (cbItem.SelectedIndex == 2)
                    {

                        /*Verify date, and convert date to MM/DD/YYYY format */
                        if (String.IsNullOrEmpty(txtRESPValueItem.Text.Trim()))
                        {
                            MessageBox.Show("Please enter a date.");
                            txtRESPValueItem.Focus();
                            return;
                        }
                        else                    
                        {
                            if (GeneralFunctions.VerifyDate(txtRESPValueItem.Text.Trim()))
                                prgdtm = GeneralFunctions.ConvertDateFormat(txtRESPValueItem.Text);
                            else
                            {
                                MessageBox.Show("Please enter correct date format.");
                                txtRESPValueItem.Text = "";
                                txtRESPValueItem.Focus();
                                return;
                            }
                        }
                    }
                }
                else
                    respid = Respid;

                //search all items. If the user is a Programmer or HQSupervisor then
                //search by the username selected username in the combobox. 
                //Otherwise, search only the cases owned by the logged in user

                if ((usrnme == "") && (respid == "") && (prgdtm == ""))
                {
                    rdt = dataObject.GetRSPMarkCase(usrnme, respid, prgdtm);
                }
                else
                {
                    rdt = dataObject.GetRSPMarkCase(usrnme, respid, prgdtm);
                }

                dgRespMarkCase.DataSource = rdt;
                setRespItemColumnHeader();

                Cursor.Current = Cursors.Default;

                //If a search performed and no records found
                if ((rdt.Rows.Count == 0) && ((usrnme != "")|| (respid != "") || (prgdtm != "")))
                {
                    MessageBox.Show("No data to display.");
                }

                //If the are no rows in the table
                if (rdt.Rows.Count == 0)
                {
                    btnData.Enabled = false;
                    btnDelete.Enabled = false;
                }

                if (rdt.Rows.Count > 0)
                {
                    btnData.Enabled = true;
                    string user1 = dgRespMarkCase.CurrentRow.Cells[1].Value.ToString();
                    if (user1 != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                        btnDelete.Enabled = false;
                    else
                        btnDelete.Enabled = true;
                }

                lblCasesCount.Text = dgRespMarkCase.Rows.Count.ToString() + " RESPONDENT MARK CASES";
            }
        }

        //Clear the search results

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (tbCaseReview.SelectedIndex == 0)
            {
                cbItem.SelectedIndex = -1;
                cbIDValueItem.SelectedIndex = -1;

                cbIDValueItem.Visible = false;
                txtIDValueItem.Visible = false;

            }
            if (tbCaseReview.SelectedIndex == 1)
            {
                cbItem.SelectedIndex = -1;
                cbRESPValueItem.SelectedIndex = -1;

                cbRESPValueItem.Visible = false;
                txtRESPValueItem.Visible = false;
            }

            lbldatef.Visible = false;

            SearchItem();
        }

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled ? Color.DarkBlue : Color.Gray;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            //Display the verification popup to ensure the user 
            //wants to delete the marked case

            frmVerifyDeletePopup popup = new frmVerifyDeletePopup();

            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.Yes)
            {

                if (tbCaseReview.SelectedIndex == 0)
                {
                    string id = dgProjMarkCase.CurrentRow.Cells[0].Value.ToString();
                    string user = dgProjMarkCase.CurrentRow.Cells[4].Value.ToString();

                    //Assign the dataObject coming from the DAL
                    //Refresh the data grid display

                    dataObject.DeleteIDRow(id, user);

                    cbItem.SelectedIndex = -1;
                    cbIDValueItem.SelectedIndex = -1;

                    cbIDValueItem.Visible = false;
                    txtIDValueItem.Visible = false;
                    lbldatef.Visible = false;

                    DataTable dt;

                    dt = dataObject.GetMarkCase("", "", "", "");
                    dgProjMarkCase.DataSource = dt;
                    lblCasesCount.Text = dgProjMarkCase.Rows.Count.ToString() + " PROJECT MARK CASES";

                    setProjItemColumnHeader();

                    if (dt.Rows.Count > 0)
                    {
                        btnData.Enabled = true;
                        string user1 = dgProjMarkCase.CurrentRow.Cells[4].Value.ToString();
                        if (user1 != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                            btnDelete.Enabled = false;
                        else
                            btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnData.Enabled = false;
                        btnDelete.Enabled = false;
                    }

                }

                if (tbCaseReview.SelectedIndex == 1)
                {
                    string respid = dgRespMarkCase.CurrentRow.Cells[0].Value.ToString();
                    string user = dgRespMarkCase.CurrentRow.Cells[1].Value.ToString();

                    //Assign the dataObject coming from the DAL
                    //Refresh the data grid display

                    dataObject.DeleteRSPRow(respid, user);

                    DataTable dt;
                    dt = dataObject.GetRSPMarkCase("", "", "");
                    
                    dgRespMarkCase.DataSource = dt;
                    setRespItemColumnHeader();
                    lblCasesCount.Text = dgRespMarkCase.Rows.Count.ToString() + " RESPONDENT MARK CASES";

                    cbItem.SelectedIndex = -1;
                    cbRESPValueItem.SelectedIndex = -1;

                    cbRESPValueItem.Visible = false;
                    txtRESPValueItem.Visible = false;
                    lbldatef.Visible = false;

                    if (dt.Rows.Count > 0)
                    {
                        btnData.Enabled = true;
                        string user1 = dgRespMarkCase.CurrentRow.Cells[1].Value.ToString();
                        if (user1 != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                            btnDelete.Enabled = false;
                        else
                            btnDelete.Enabled = true;
                    }
                    else
                    {
                        btnData.Enabled = false;
                        btnDelete.Enabled = false;
                    }

                }

            }

            popup.Dispose();

        }

        private void btnData_Click(object sender, EventArgs e)
        {

            if (tbCaseReview.SelectedIndex == 0)
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    string id = dgProjMarkCase.CurrentRow.Cells[0].Value.ToString();
                    frmTfu tfu = new frmTfu();
                    SampleData sdata = new SampleData();
                    Sample smp = sdata.GetSampleData(id);
                    tfu.RespId = smp.Respid;
                    tfu.CallingForm = this;

                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
                    tfu.ShowDialog();   // show child
                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
                }
                else
                {
                    //Get the id for the selected row data
                    string id = dgProjMarkCase.CurrentRow.Cells[0].Value.ToString();

                    List<string> Idlist = new List<string>();
                    int curr_index = 0;
                    int cnt = 0;
                    foreach (DataGridViewRow dr in dgProjMarkCase.Rows)
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

                    frmC700 fC = new frmC700();

                    //Get the id for the selected row and pass to form

                    fC.Id = id;
                    fC.Idlist = Idlist;
                    fC.CurrIndex = curr_index;
                    fC.CallingForm = this;

                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                    fC.ShowDialog();  // show child

                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
                }
            }
            else
            {
                this.Hide();   // hide parent

                frmRespAddrUpdate fRAU = new frmRespAddrUpdate();

                //Get the Respid for the selected row and pass to form

                fRAU.RespondentId = dgRespMarkCase.CurrentRow.Cells[0].Value.ToString();
                fRAU.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fRAU.ShowDialog();  // show child

                GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");

            }

            LoadTables();
        }


        /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (tbCaseReview.SelectedIndex == 0)
                printer.Title = "PROJECT MARKED CASE REVIEW";
            else
                printer.Title = "RESPONDENT MARKED CASE REVIEW";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            if (tbCaseReview.SelectedIndex == 0)
                printer.printDocument.DocumentName = "Project Mark Cases";
            else
                printer.printDocument.DocumentName = "Respondent Mark Cases";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (tbCaseReview.SelectedIndex == 0)
            {
                //resize the note column
                dgProjMarkCase.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgProjMarkCase.Columns[6].Width = 300;
                printer.PrintDataGridViewWithoutDialog(dgProjMarkCase);

                //resize back the note column
                dgProjMarkCase.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            else
            {
                //resize the note column
                dgRespMarkCase.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgRespMarkCase.Columns[3].Width = 400;
                printer.PrintDataGridViewWithoutDialog(dgRespMarkCase);

                //resize back the note column
                dgRespMarkCase.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            Cursor.Current = Cursors.Default;
        }

  
        private void frmMarkCaseReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void dgProjMarkCase_SelectionChanged(object sender, EventArgs e)
        {
            if (dgProjMarkCase.CurrentRow == null) return;

            string user = dgProjMarkCase.CurrentRow.Cells[4].Value.ToString();
            if (user != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                btnDelete.Enabled = false;
            else
                btnDelete.Enabled = true;
        }

        private void dgRespMarkCase_SelectionChanged(object sender, EventArgs e)
        {
            if (dgRespMarkCase.CurrentRow == null) return;

            string user = dgRespMarkCase.CurrentRow.Cells[1].Value.ToString();
            if (user != UserInfo.UserName && UserInfo.GroupCode != EnumGroups.HQManager && UserInfo.GroupCode != EnumGroups.Programmer)
                btnDelete.Enabled = false;
            else
                btnDelete.Enabled = true;
        }
    }
}
