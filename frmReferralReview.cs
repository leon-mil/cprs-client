/**********************************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System

Program Name    : frmReferralReview.cs

Programmer      : Cestine Gill

Creation Date   : 12/22/2015

Inputs          : RESPID, ID

Parameters      : None
                  
Outputs         : None

Description     : This screen allows Analysts to review/search referral notes

Detailed Design : Detailed Design for Referral Review

Other           : Called from: frmCprsParent.cs Menu
 
Revision History:	
***********************************************************************************
Modified Date  :  10/12/2016
Modified By    :  Chritine Zhang
Keyword        :  
Change Request :  
Description    :  change tab style
***********************************************************************************
Modified Date  :  06/06/2017
Modified By    :  Chritine Zhang
Keyword        :  None
Change Request :  CR 205
Description    :  Update Popup messages to conform to other screens
***********************************************************************************
Modified Date  :  12/10/2020
Modified By    :  Chritine Zhang
Keyword        :  None
Change Request :  CR 
Description    :  Fix a bug when click C700 screen, then select menu search, create an error
***********************************************************************************
Modified Date  :  1/6/2020
Modified By    :  Chritine Zhang
Keyword        :  None
Change Request :  CR 7850
Description    :  add my sector to screen
***********************************************************************************
Modified Date  :  4/29/2021
Modified By    :  Chritine Zhang
Keyword        :  None
Change Request :  CR
Description    :  add Refuser column and assign referral
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
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmReferralReview : Cprs.frmCprsParent
    {
        public static frmReferralReview Current;

        //public property for this form 

        public string Respid = string.Empty;
        public string Id = string.Empty;

        //private string user = UserInfo.UserName;

        //Private variables
        private DataTable dt = null;
        private MySector ms;
        List<string> dy = new List<string>();
        
        private ReferralData dataObject;

        //Populate the drop down search by combobox depending on
        //whether project search or respondent search

        private string[] idSearch = { "ID", "NEWTC", "TYPE", "STATUS", "USER", "GROUP", "ASSIGNED", "DATE" };
        private string[] rspSearch = { "RESPID", "TYPE", "STATUS", "USER", "GROUP", "ASSIGNED", "DATE" };
        private List<string> dodgeinitlist;
        
        public frmReferralReview()
        {
            InitializeComponent();
        }

        //initially load the datagrid

        private void frmReferralReview_Load(object sender, EventArgs e)
        {

            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            // get my sector data
            MySectorsData md = new MySectorsData();
            ms = md.GetMySectorData(UserInfo.UserName);

            //if user does not exist in sectors dataset then user will
            //not have access to sector checkbox
            if (ms == null)
                ckMySec.Visible = false;
            else
            {
                ckMySec.Visible = true;
                if (ms.Sect00 == "Y")
                {
                    dy.Add("00");
                }
                if (ms.Sect01 == "Y")
                {
                    dy.Add("01");
                }
                if (ms.Sect02 == "Y")
                {
                    dy.Add("02");
                }
                if (ms.Sect03 == "Y")
                {
                    dy.Add("03");
                }
                if (ms.Sect04 == "Y")
                {
                    dy.Add("04");
                }
                if (ms.Sect05 == "Y")
                {
                    dy.Add("05");
                }
                if (ms.Sect06 == "Y")
                {
                    dy.Add("06");
                }
                if (ms.Sect07 == "Y")
                {
                    dy.Add("07");
                }
                if (ms.Sect08 == "Y")
                {
                    dy.Add("08");
                }
                if (ms.Sect09 == "Y")
                {
                    dy.Add("09");
                }
                if (ms.Sect10 == "Y")
                {
                    dy.Add("10");
                }
                if (ms.Sect11 == "Y")
                {
                    dy.Add("11");
                }
                if (ms.Sect12 == "Y")
                {
                    dy.Add("12");
                }
                if (ms.Sect13 == "Y")
                {
                    dy.Add("13");
                }
                if (ms.Sect14 == "Y")
                {
                    dy.Add("14");
                }
                if (ms.Sect15 == "Y")
                {
                    dy.Add("15");
                }
                if (ms.Sect16 == "Y")
                {
                    dy.Add("16");
                }
                if (ms.Sect19 == "Y")
                {
                    dy.Add("19");
                }
                if (ms.Sect1T == "Y")
                {
                    for (int i = 20; i <= 39; i++)
                    {
                        dy.Add(i.ToString());
                    }
                }
            }

            SetButtonTxt();

            lblTab.Text = tbReferrals.SelectedTab.Text;

            GetProj();
            dodgeinitlist = dataObject.GetDodgeInitialList();

            showProjSearchItems(idSearch);

            GetResp();
            
            //display the row count on the screen

            if (dgProjReferrals.RowCount == 0)
            {              
                lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";
                btnPrint.Enabled = false;
                btnData.Enabled = false;
            }
            else
            {              
                lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " PROJECT REFERRALS";
                btnPrint.Enabled = true;
                btnData.Enabled = true;   
            }

            btnAssign.Visible = false;
        }

        private void GetProj()
        {
            dataObject = new ReferralData();

            // set search components to invisible

            txtPROJValueItem.Visible = false;
            cbPROJValueItem.Visible = false;
            lbldatef.Visible = false;

            dgProjReferrals.RowHeadersVisible = false;

            //populate the data grid without any search criteria

            DataTable dtProjItem = dataObject.GetProjReferralReviewTable("", "", "", "", "", "", "", "");
            SetupProjCols(dtProjItem);
           
            lblTab.Location = new System.Drawing.Point(575, 97);
            if (ckMySec.Checked)
            {
                DataTable dtmyProjItem = dtProjItem.Clone();
                foreach (DataRow row in dtProjItem.Rows)
                {
                    string ntc = row["NEWTC"].ToString().Substring(0, 2);
                    if (dy.Contains(ntc))
                        dtmyProjItem.ImportRow(row);
                }
                dgProjReferrals.DataSource = dtmyProjItem;
            }
            else
               dgProjReferrals.DataSource = dtProjItem;

            setProjItemColumnHeader();

            lblCasesCount.Text = dgProjReferrals.RowCount.ToString() + " PROJECT REFERRALS";
        }

        private void SetupProjCols(DataTable dt)
        {
            //reassign the value from the database to a 
            //user-friendlier representation in the combobox

            int reftypeColumn = 2; //Put your column number here
            int refstatusColumn = 3;
            int refgroupColumn = 5;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                switch (dt.Rows[i][reftypeColumn].ToString())
                {
                    case "1":
                        dt.Rows[i][reftypeColumn] = "Late Receipt";
                        break;
                    case "2":
                        dt.Rows[i][reftypeColumn] = "Correct Flags";
                        break;
                    case "3":
                        dt.Rows[i][reftypeColumn] = "Data Issue";
                        break;
                    case "4":
                        dt.Rows[i][reftypeColumn] = "PNR/Address";
                        break;
                    case "5":
                        dt.Rows[i][reftypeColumn] = "Other";
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
                    case "3":
                        dt.Rows[i][refgroupColumn] = "NPC Supervisor";
                        break;
                    case "4":
                        dt.Rows[i][refgroupColumn] = "NPC Clerk";
                        break;

                }
                switch (dt.Rows[i][refstatusColumn].ToString())
                {
                    case "A":
                        dt.Rows[i][refstatusColumn] = "Active";
                        break;
                    case "P":
                        dt.Rows[i][refstatusColumn] = "Pending";
                        break;
                    case "C":
                        dt.Rows[i][refstatusColumn] = "Complete";
                        break;
                }
            }
        }

        private void SetupLabel()
        {
            if (dgProjReferrals.RowCount == 0)
            {
                tbReferrals.SelectedTab = tbRespondent;
                lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";
            }
            else
            {
                tbReferrals.SelectedTab = tbProject;
                lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " PROJECT REFERRALS";
            }
            tbReferrals.Refresh();
        }

        //format the table columns

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
                    dgProjReferrals.Columns[i].Width = 60;

                }
                if (i == 1)
                {
                    dgProjReferrals.Columns[i].HeaderText = "NEWTC";
                    dgProjReferrals.Columns[i].Width = 60;

                }
                if (i == 2)
                {
                    dgProjReferrals.Columns[i].HeaderText = "TYPE";
                    dgProjReferrals.Columns[i].Width = 80;

                }
                if (i == 3)
                {
                    dgProjReferrals.Columns[i].HeaderText = "STATUS";
                    dgProjReferrals.Columns[i].Width = 80;

                }
                if (i == 4)
                {
                    dgProjReferrals.Columns[i].HeaderText = "USER";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 5)
                {
                    dgProjReferrals.Columns[i].HeaderText = "GROUP";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 6)
                {
                    dgProjReferrals.Columns[i].HeaderText = "ASSIGNED";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 7)
                {
                    dgProjReferrals.Columns[i].HeaderText = "DATE/TIME";
                    dgProjReferrals.Columns[i].Width = 120;

                }
                if (i == 8)
                {
                    dgProjReferrals.Columns[i].HeaderText = "NOTE";
                    dgProjReferrals.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgProjReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void GetResp()
        {
            dataObject = new ReferralData();

            txtRESPValueItem.Visible = false;
            cbRESPValueItem.Visible = false;
            lbldatef.Visible = false;

            dgRespReferrals.RowHeadersVisible = false;

            //populate the data grid without any search criteria

            DataTable dtRSPItem = dataObject.GetRespReferralReviewTable("", "","", "", "", "", "");
            SetupRespCols(dtRSPItem);

            lblTab.Location = new System.Drawing.Point(565, 97);
            
            dgRespReferrals.DataSource = dtRSPItem;

            setRespItemColumnHeader();
            lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";

        }

        private void SetupRespCols(DataTable rdt)
        {
            //reassign the value from the database to a 
            //user-friendlier representation in the table

            int reftypeColumn = 1; //Put your column number here
            int refgroupColumn = 4;
            int refstatusColumn = 2;

            for (int i = 0; i < rdt.Rows.Count; i++)
            {

                switch (rdt.Rows[i][reftypeColumn].ToString())
                {
                    case "1":
                        rdt.Rows[i][reftypeColumn] = "Late Receipt";
                        break;
                    case "2":
                        rdt.Rows[i][reftypeColumn] = "Correct Flags";
                        break;
                    case "3":
                        rdt.Rows[i][reftypeColumn] = "Data Issue";
                        break;
                    case "4":
                        rdt.Rows[i][reftypeColumn] = "PNR/Address";
                        break;
                    case "5":
                        rdt.Rows[i][reftypeColumn] = "Other";
                        break;
                }

                switch (rdt.Rows[i][refgroupColumn].ToString())
                {
                    case "1":
                        rdt.Rows[i][refgroupColumn] = "HQ Supervisor";
                        break;
                    case "2":
                        rdt.Rows[i][refgroupColumn] = "HQ Analyst";
                        break;
                    case "3":
                        rdt.Rows[i][refgroupColumn] = "NPC Supervisor";
                        break;
                    case "4":
                        rdt.Rows[i][refgroupColumn] = "NPC Clerk";
                        break;

                }
                switch (rdt.Rows[i][refstatusColumn].ToString())
                {
                    case "A":
                        rdt.Rows[i][refstatusColumn] = "Active";
                        break;
                    case "P":
                        rdt.Rows[i][refstatusColumn] = "Pending";
                        break;
                    case "C":
                        rdt.Rows[i][refstatusColumn] = "Complete";
                        break;
                }
            }
       }
        
        //format the respondent datagrid columns

        private void setRespItemColumnHeader()
        {
            //lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";

            dgRespReferrals.RowHeadersVisible = false;
            dgRespReferrals.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgRespReferrals.RowHeadersVisible = false;  // set it to false if not needed
            dgRespReferrals.ShowCellToolTips = false;

            for (int i = 0; i < dgRespReferrals.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgRespReferrals.Columns[i].HeaderText = "RESPID";
                    dgRespReferrals.Columns[i].Width = 60;

                }
                if (i == 1)
                {
                    dgRespReferrals.Columns[i].HeaderText = "TYPE";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 2)
                {
                    dgRespReferrals.Columns[i].HeaderText = "STATUS";
                    dgRespReferrals.Columns[i].Width = 80;

                }
                if (i == 3)
                {
                    dgRespReferrals.Columns[i].HeaderText = "USER";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 4)
                {
                    dgRespReferrals.Columns[i].HeaderText = "GROUP";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 5)
                {
                    dgRespReferrals.Columns[i].HeaderText = "ASSIGNED";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 6)
                {
                    dgRespReferrals.Columns[i].HeaderText = "DATE/TIME";
                    dgRespReferrals.Columns[i].Width = 120;

                }
                if (i == 7)
                {
                    dgRespReferrals.Columns[i].HeaderText = "NOTE";
                    dgRespReferrals.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgRespReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

        }

        //Changes the page title label dependent to which tab is selected
        //and displays the search criteria combo box dependent on the 
        //tab selected

        private void tbReferralReview_Selected(object sender, TabControlEventArgs e)
        {

            if (tbReferrals.SelectedIndex == 0)
            {
                if (ms != null)
                    ckMySec.Visible = true;
                lblTab.Text = tbProject.Text;
                lblTab.Location = new System.Drawing.Point(575, 97);
                showProjSearchItems(idSearch);
                
                //Display the row count when selected tab change

                lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " PROJECT REFERRALS";
                if (dgProjReferrals.Rows.Count == 0)
                {
                    btnPrint.Enabled = false;
                    btnData.Enabled = false;
                }
                else
                {
                    btnPrint.Enabled = true;
                    btnData.Enabled = true;
                }
            }
            if (tbReferrals.SelectedIndex == 1)
            {
                ckMySec.Visible = false;
                lblTab.Text = tbRespondent.Text;
                lblTab.Location = new System.Drawing.Point(565, 97);
                showRSPSearchItems(rspSearch);

                //Display the row count when selected tab change

                lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";
                if (dgRespReferrals.Rows.Count == 0)
                {
                    btnPrint.Enabled = false;
                    btnData.Enabled = false;
                }
                else
                {
                    btnPrint.Enabled = true;
                    btnData.Enabled = true;
                }
            }
        }

        //Set Label for Data button

        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:   
                    btnData.Text = "C-700/DODGE INITIAL";
                    break;
                case EnumGroups.HQManager:    
                    btnData.Text = "C-700/DODGE INITIAL";
                    break;
                case EnumGroups.HQAnalyst:  
                    btnData.Text = "C-700/DODGE INITIAL";
                    break;
                case EnumGroups.NPCManager:
                    btnData.Text = "TFU/DODGE INITIAL";
                    break;
                case EnumGroups.NPCLead:  
                    btnData.Text = "TFU/DODGE INITIAL";
                    break;
                case EnumGroups.NPCInterviewer:  
                    btnData.Text = "TFU/DODGE INITIAL";
                    break;
                case EnumGroups.HQSupport:  
                    btnData.Text = "C-700/DODGE INITIAL";
                    break;
                case EnumGroups.HQMathStat:  
                    btnData.Text = "C-700/DODGE INITIAL";
                    break;
                case EnumGroups.HQTester:  
                    btnData.Text = "C-700/DODGE INITIAL";
                    break;
            }
        }


        //Populate the project search criteria combobox

        public void showProjSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
            GetProj();
        }

        //Populate the respondent search criteria combobox

        public void showRSPSearchItems(string[] search)
        {
            cbItem.Items.Clear();
            cbItem.Items.AddRange(search);
            GetResp();
        }


        //Checks and disables the tab if 
        //there in no data output within it

        public void DisableEnableTab()
        {

            if (dgProjReferrals.DataSource == null || dgProjReferrals.Rows.Count == 0)
                tbProject.Enabled = false;
            else
                tbProject.Enabled = true;

            if (dgRespReferrals.DataSource == null || dgRespReferrals.Rows.Count == 0)
                tbRespondent.Enabled = false;
            else
                tbRespondent.Enabled = true;
        }

        //refresh the table whenever a different tab is selected.
        //The search criteria combo box 
        //will change depending on the tab selected

        private void tbReferralReview_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbReferrals.Refresh();

            if (tbReferrals.SelectedIndex == 0)
            {
                cbRESPValueItem.Visible = false;
                txtRESPValueItem.Visible = false;
                SetButtonTxt();
            }
            if (tbReferrals.SelectedIndex == 1)
            {
                cbPROJValueItem.Visible = false;
                txtPROJValueItem.Visible = false;
                btnData.Text = "DATA";
            }
        }

        //The search criteria combo box 
        //will change depending on the search combobox selected

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (tbReferrals.SelectedIndex == 0)
            {
                //for USRNME, REFTYPE, REFSTATUS, GROUP, ASSIGNED show combo boxes

                if (cbItem.SelectedIndex == 1 ||
                    cbItem.SelectedIndex == 2 ||
                    cbItem.SelectedIndex == 3 ||
                    cbItem.SelectedIndex == 4 ||
                    cbItem.SelectedIndex == 5 ||
                    cbItem.SelectedIndex == 6)
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

                    if (cbItem.SelectedIndex == 7)
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

            if (tbReferrals.SelectedIndex == 1)
            {
                if (cbItem.SelectedIndex == 1 ||
                    cbItem.SelectedIndex == 2 ||
                    cbItem.SelectedIndex == 3 ||
                    cbItem.SelectedIndex == 4 ||
                    cbItem.SelectedIndex == 5)
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

                    if (cbItem.SelectedIndex == 6)
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

        private void txtPROJValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //for id only allow numbers
            if (cbItem.SelectedIndex == 0)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
            //for date - allow back slashes and numbers only
            else if (cbItem.SelectedIndex == 6)
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
            else if (cbItem.SelectedIndex == 5)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '/';
            }
        }

        /*Setup value combo data, based on tab and combo index */

        private void PopulateProjValueCombo(int cbIndex)
        {

            /*for Item access */

            dt = dataObject.GetProjValueList(cbIndex);

            /* for newtc combo */

            if (cbIndex == 1)
            {
                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "newtc";
                cbPROJValueItem.DisplayMember = "newtc";
            }

            /* for type combo */

            if (cbIndex == 2)
            {

                //reassign the value from the database to a 
                //user-friendlier representation in the combobox

                //add a desciption column

                dt.Columns.Add("description", typeof(String));
                
                foreach (DataRow dr in dt.Rows)
                {
                    
                    if (dr[0].ToString() == "1") //select a row where reftype = "1"
                    {
                        //Update the column value
                        dr[1] = "Late Receipt"; //description column index
                    }
                    if (dr[0].ToString() == "2") //select a row where reftype = "2"
                    {                        
                        dr[1] = "Correct Flags"; 
                    }
                    if (dr[0].ToString() == "3") //select a row where reftype = "3"
                    {                     
                        dr[1] = "Data Issue";
                    }
                    if (dr[0].ToString() == "4") //select a row where reftype = "4"
                    {
                        dr[1] = "PNR/Address";
                    }
                    if (dr[0].ToString() == "5") //select a row where reftype = "5"
                    {
                        dr[1] = "Other";
                    }
                }

                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "reftype"; // column name which you want to select              
                cbPROJValueItem.DisplayMember = "description"; //Field in the datatable which you want to be the value of the combobox 
            }

            /* for status combo */

            if (cbIndex == 3)
            {

                 dt.Columns.Add("description", typeof(String));

                 foreach (DataRow dr in dt.Rows)
                 {
                     if (dr[0].ToString() == "A") //select row where refstatus = "A"
                     {
                        //Update column value
                         dr[1] = "Active"; //description column index
                     }
                    if (dr[0].ToString() == "P") //select row where refstatus = "A"
                    {
                        //Update column value
                        dr[1] = "Pending"; //description column index
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

            if (cbIndex == 4)
            {
                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "usrnme";
                cbPROJValueItem.DisplayMember = "usrnme";  
            }

            /* for group combo */

            if (cbIndex == 5)
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
                    if (dr[0].ToString() == "3") //select a row where refgroup = "3"
                    {
                        dr[1] = "NPC Supervisor";
                    }
                    if (dr[0].ToString() == "4") //select a row where refgroup = "4"
                    {
                        dr[1] = "NPC Clerk";
                    }
                }
                 
                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "refgroup";
                cbPROJValueItem.DisplayMember = "description";  
            }

            /* for usrnme combo */

            if (cbIndex == 6)
            {
                cbPROJValueItem.DataSource = dt;
                cbPROJValueItem.ValueMember = "refuser";
                cbPROJValueItem.DisplayMember = "refuser";
            }


            cbPROJValueItem.SelectedIndex = -1;
        }

        private void PopulateRSPValueCombo(int cbIndex)
        {
            /*for Item access */

            dt = dataObject.GetRespValueList(cbIndex);
            
            /* for type combo box */

            if (cbIndex == 1)
            {
                dt.Columns.Add("description", typeof(String));

                foreach (DataRow dr in dt.Rows)
                {

                    if (dr[0].ToString() == "1") //select a row where reftype = "1"
                    {
                        //Update the column value
                        dr[1] = "Late Receipt"; //description column index
                    }
                    if (dr[0].ToString() == "2") //select a row where reftype = "2"
                    {
                        dr[1] = "Correct Flags";
                    }
                    if (dr[0].ToString() == "3") //select a row where reftype = "3"
                    {
                        dr[1] = "Data Issue";
                    }
                    if (dr[0].ToString() == "4") //select a row where reftype = "4"
                    {
                        dr[1] = "PNR/Address";
                    }
                    if (dr[0].ToString() == "5") //select a row where reftype = "5"
                    {
                        dr[1] = "Other";
                    }
                }

                cbRESPValueItem.DataSource = dt;                
                cbRESPValueItem.ValueMember = "reftype"; // name of the column of data that supplies the possible control values
                cbRESPValueItem.DisplayMember = "description"; //the name of the column of data that supplies the visible list items

            }

            /* for status combo box */

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
                    if (dr[0].ToString() == "P") //select row where refstatus = "A"
                    {
                        //Update column value
                        dr[1] = "Pending"; //description column index
                    }
                    if (dr[0].ToString() == "C") //select row where reftype = "C"
                    {
                        dr[1] = "Complete";
                    }
                }

                cbRESPValueItem.DataSource = dt;
                cbRESPValueItem.ValueMember = "refstatus";
                cbRESPValueItem.DisplayMember = "description"; 
            }

            /* for usrnme combo box */

            if (cbIndex == 3)
            {
                cbRESPValueItem.DataSource = dt;
                cbRESPValueItem.ValueMember = "usrnme";
                cbRESPValueItem.DisplayMember = "usrnme"; 
            }

            /* for group combo box */

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
                    if (dr[0].ToString() == "3") //select a row where refgroup = "3"
                    {
                        dr[1] = "NPC Supervisor";
                    }
                    if (dr[0].ToString() == "4") //select a row where refgroup = "4"
                    {
                        dr[1] = "NPC Clerk";
                    }
                }

                cbRESPValueItem.DataSource = dt;
                cbRESPValueItem.ValueMember = "refgroup";
                cbRESPValueItem.DisplayMember = "description";
            }

            /* for assigned combo box */

            if (cbIndex == 5)
            {
                cbRESPValueItem.DataSource = dt;
                cbRESPValueItem.ValueMember = "refuser";
                cbRESPValueItem.DisplayMember = "refuser";
            }

            cbRESPValueItem.SelectedIndex = -1;
        }

        //search

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

        //depending on the tab selected,
        //perfomr the search and re-populate the datagrid

        private void SearchItem()
        {
            if (tbReferrals.SelectedIndex == 0)
            {
                string id = "";
                string usrnme = "";
                string prgdtm = "";
                string type = "";
                string status = "";
                string group = "";
                string assigned = "";
                string newtc = "";

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
                        else if (!dataObject.CheckReferralExistInAll(id))
                        {
                            MessageBox.Show("Invalid ID.");
                            txtPROJValueItem.Text = "";
                            txtPROJValueItem.Focus();
                            return;
                        }
                    }

                   // for newtc

                    else if (cbItem.SelectedIndex == 1)
                    {
                        newtc = cbPROJValueItem.Text.Trim();
                        if (newtc == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbPROJValueItem.Focus();
                        }
                    }

                    // for type

                    else if (cbItem.SelectedIndex == 2)
                    {
                        type = cbPROJValueItem.Text.Trim();

                        if (type == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbPROJValueItem.Focus();
                        }

                        //Reassign the radio button values to their database equivalent

                        switch (type)
                        {
                            case "Late Receipt": type = "1"; break;
                            case "Correct Flags": type = "2"; break;
                            case "Data Issue": type = "3"; break;
                            case "PNR/Address": type = "4"; break;
                            case "Other": type = "5"; break;
                        }
                    }

                    // for status

                    else if (cbItem.SelectedIndex == 3)
                    {
                        status = cbPROJValueItem.Text.Trim();

                        if (status == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbPROJValueItem.Focus();
                        }

                        switch (status)
                        {
                            case "Active": status = "A"; break;
                            case "Pending": status = "P"; break;
                            case "Complete": status = "C"; break;
                        }
                    }

                    // for usrname

                    else if (cbItem.SelectedIndex == 4)
                    {
                        usrnme = cbPROJValueItem.Text.Trim();
                        if (usrnme == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbPROJValueItem.Focus();
                        }
                    }

                    // for group

                    else if (cbItem.SelectedIndex == 5) 
                    {
                        group = cbPROJValueItem.Text.Trim();

                        if (group == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbPROJValueItem.Focus();
                        }

                        switch (group)
                        {
                            case "HQ Supervisor": group = "1"; break;
                            case "HQ Analyst": group = "2"; break;
                            case "NPC Supervisor": group = "3"; break;
                            case "NPC Clerk": group = "4"; break;
                        }
                    }
                    else if (cbItem.SelectedIndex == 6)
                    {
                        assigned = cbPROJValueItem.Text.Trim();
                        if (assigned == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }
                    }

                    // for prgdtm

                    else if (cbItem.SelectedIndex == 7)
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

                if ((usrnme == "") && (id == "") && (prgdtm == "") && (type == "") && 
                    (status == "") && (group == "") && (newtc == "") && (assigned == ""))
                {
                    dt = dataObject.GetProjReferralReviewTable("", "", "", "", "", "", "", "");
                    SetupProjCols(dt);
                }
                else
                {
                    dt = dataObject.GetProjReferralReviewTable(id, type, group, assigned, status, newtc, usrnme, prgdtm);
                    SetupProjCols(dt);

                }
                if (ckMySec.Checked)
                {
                    DataTable dtmyProjItem = dt.Clone();
                    foreach (DataRow row in dt.Rows)
                    {
                        string ntc = row["NEWTC"].ToString().Substring(0, 2);
                        if (dy.Contains(ntc))
                            dtmyProjItem.ImportRow(row);
                    }
                    dgProjReferrals.DataSource = dtmyProjItem;
                }
                else
                    dgProjReferrals.DataSource = dt;

                //display the row count on the screen after search

                lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " PROJECT REFERRALS";

                setProjItemColumnHeader();

                Cursor.Current = Cursors.Default;

                if ((dt.Rows.Count == 0) && ((usrnme != "") || (id != "") || 
                    (prgdtm != "") || (type != "") || (status != "") || 
                    (group != "") || (newtc != "") || (assigned !="")))
                {
                    MessageBox.Show("No data to display.");
                }
            }

            if (tbReferrals.SelectedIndex == 1)
            {
                string respid = "";
                string usrnme = "";
                string prgdtm = "";
                string type = "";
                string status = "";
                string group = "";
                string assigned = "";

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
                        //check id exists in referral
                        else if (!dataObject.CheckReferralExist(null, respid))
                        {
                            MessageBox.Show("Invalid RESPID.");
                            txtPROJValueItem.Text = "";
                            txtPROJValueItem.Focus();
                            return;
                        }

                    }

                    // for type

                    else if (cbItem.SelectedIndex == 1)
                    {
                        type = cbRESPValueItem.Text.Trim();

                        if (type == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }

                        //Reassign the radio button values to their database equivalent

                        switch (type)
                        {
                            case "Late Receipt": type = "1"; break;
                            case "Correct Flags": type = "2"; break;
                            case "Data Issue": type = "3"; break;
                            case "PNR/Address": type = "4"; break;
                            case "Other": type = "5"; break;
                        }
                    }

                    // for status

                    else if (cbItem.SelectedIndex == 2)
                    {
                        status = cbRESPValueItem.Text.Trim();

                        if (status == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }

                        switch (status)
                        {
                            case "Active": status = "A"; break;
                            case "Pending": status = "P"; break;
                            case "Complete": status = "C"; break;
                        }
                    }

                    // for usrname

                    else if (cbItem.SelectedIndex == 3)
                    {
                        usrnme = cbRESPValueItem.Text.Trim();

                        if (usrnme == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }
                    }

                    // for group

                    else if (cbItem.SelectedIndex == 4)
                    {
                        group = cbRESPValueItem.Text.Trim();

                        if (group == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }

                        switch (group)
                        {
                            case "HQ Supervisor": group = "1"; break;
                            case "HQ Analyst": group = "2"; break;
                            case "NPC Supervisor": group = "3"; break;
                            case "NPC Clerk": group = "4"; break;
                        }
                    }

                    else if (cbItem.SelectedIndex == 5)
                    {
                        assigned = cbRESPValueItem.Text.Trim();
                        if (assigned == "")
                        {
                            MessageBox.Show("A value must be selected from the Drop Down Menu.");
                            cbRESPValueItem.Focus();
                        }
                    }

                    // for prgdtm

                    else if (cbItem.SelectedIndex == 6)
                    {
                        /*Verify date, and convert date to MM/DD/YYYY format */

                        if (String.IsNullOrEmpty(txtRESPValueItem.Text.Trim()))
                        {
                            MessageBox.Show("Please enter a date.");
                            txtRESPValueItem.Text = "";
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
                                return;
                            }
                        }
                    }
                }
                else
                    respid = Respid;

                if ((usrnme == "") && (respid == "") && (prgdtm == "") && (type == "") && (status == "") && (group == "")&& (assigned == ""))
                    rdt = dataObject.GetRespReferralReviewTable("", "","", "", "", "", "");
                else
                    rdt = dataObject.GetRespReferralReviewTable(respid, type, group, assigned, status, usrnme, prgdtm);

                SetupRespCols(rdt);
                dgRespReferrals.DataSource = rdt;

                //display the row count on the screen

                lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";

                setRespItemColumnHeader();

                Cursor.Current = Cursors.Default;

                if ((rdt.Rows.Count == 0) && ((usrnme != "") || (respid != "") || 
                    (prgdtm != "") || (type != "") || (status != "") || (group != "")))
                {
                    MessageBox.Show("No data to display.");
                }
            }
        }

        //find selected id
        private void HighlightRowForId(string id)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dgProjReferrals.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(id))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            
            if (rowIndex >= 0)
                dgProjReferrals.Rows[rowIndex].Selected = true;
            
        }


        //Clear the search combo boxes

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPROJValueItem.Clear();
            txtRESPValueItem.Clear();
            if (tbReferrals.SelectedIndex == 0)
            {
                cbItem.SelectedIndex = -1;
                cbPROJValueItem.SelectedIndex = -1;
               
                cbPROJValueItem.Visible = false;
                txtPROJValueItem.Visible = false;
            }

            if (tbReferrals.SelectedIndex == 1)
            {
                
                cbItem.SelectedIndex = -1;
                cbRESPValueItem.SelectedIndex = -1;
                
                cbRESPValueItem.Visible = false;
                txtRESPValueItem.Visible = false;
            }

            lbldatef.Visible = false;

            SearchItem();
        }

        private void btnData_Click(object sender, EventArgs e)
        {
            if (tbReferrals.SelectedTab.Text == "PROJECT")
            {
                if (dgProjReferrals.SelectedRows.Count > 0)
                {
                    string id = dgProjReferrals.SelectedRows[0].Cells[0].Value.ToString();

                    if (dodgeinitlist.Count>0 && dodgeinitlist.Contains(id))
                    { 
                        List<string> Idlist = new List<string>();
                        int curr_index = 0;
                        int cnt = 0;
                        foreach (DataGridViewRow dr in dgProjReferrals.Rows)
                        {
                            string val = dr.Cells["ID"].Value.ToString();

                            if (dodgeinitlist.Contains(val) && !Idlist.Contains(val))
                            {
                                Idlist.Add(val);
                                if (val == id)
                                    curr_index = cnt;

                                cnt = cnt + 1;
                            }
                        }

               
                        this.Hide();   // hide parent
                       frmDodgeInital fDodgeInit = new frmDodgeInital();

                        //Get the id for the selected row and pass to form
                        fDodgeInit.Id = id;
                        fDodgeInit.Idlist = Idlist;
                        fDodgeInit.CurrIndex = curr_index;
                        fDodgeInit.CallingForm = this;
                        fDodgeInit.EntryPnt = "REV";

                        GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                        fDodgeInit.ShowDialog();  // show child
                                                  //   this.Show();
                       // showProjSearchItems(idSearch);
                        SearchItem2();
                        HighlightRowForId(id);
                        GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");

                        return;
                    }
                   
                    if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                    {
                        frmTfu tfu = new frmTfu();
                        SampleData sdata = new SampleData();
                        Sample smp = sdata.GetSampleData(id);
                        tfu.RespId = smp.Respid;
                        tfu.CallingForm = this;

                        GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
                        tfu.ShowDialog();   // show child
                        SearchItem2();
                        HighlightRowForId(id);
                        GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
                    }
                    else
                    {
                        //Get the values for the selected row data

                        List<string> Idlist = new List<string>();
                        int curr_index = 0;
                        int cnt = 0;
                        foreach (DataGridViewRow dr in dgProjReferrals.Rows)
                        {
                            string val = dr.Cells["ID"].Value.ToString();

                            if (!dodgeinitlist.Contains(val) && !Idlist.Contains(val))
                            {
                                Idlist.Add(val);
                                if (val == id)
                                    curr_index = cnt;

                                cnt = cnt + 1;
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
                        SearchItem2();
                        HighlightRowForId(id);
                        GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
                    } 
                }
              
            }
            else if (tbReferrals.SelectedTab.Text == "RESPONDENT")
            {
                if (dgRespReferrals.SelectedRows.Count > 0)
                {
                    //Get the values for the selected row data

                    string Respid = dgRespReferrals.SelectedRows[0].Cells[0].Value.ToString();

                    this.Hide();   // hide parent

                    frmRespAddrUpdate fRAU = new frmRespAddrUpdate();

                    //Get the Respid for the selected row and pass to form

                    fRAU.RespondentId = Respid;

                    fRAU.CallingForm = this;

                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                    fRAU.ShowDialog();  // show child
                    //this.Show();
                    SearchItem2();
                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");

                }
            }
           
        }

        /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (tbReferrals.SelectedIndex == 0)
                printer.Title = "Project Referrals";
            else
                printer.Title = "Respondent Referrals";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            if (tbReferrals.SelectedIndex == 0)
                printer.printDocument.DocumentName = "Project Referrals";
            else
                printer.printDocument.DocumentName = "Respondent Referrals";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (tbReferrals.SelectedIndex == 0)
            {
                //resize the note column
                dgProjReferrals.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgProjReferrals.Columns[8].Width = 300;
                printer.PrintDataGridViewWithoutDialog(dgProjReferrals);

                //resize back the note column
                dgProjReferrals.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            else
            {
                //resize the note column
                dgRespReferrals.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgRespReferrals.Columns[7].Width = 400;
                printer.PrintDataGridViewWithoutDialog(dgRespReferrals);

                //resize back the note column
                dgRespReferrals.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            Cursor.Current = Cursors.Default;
           
        }

        private void frmReferralReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void ckMySec_CheckedChanged(object sender, EventArgs e)
        {
            txtPROJValueItem.Clear();
            txtRESPValueItem.Clear();

            cbItem.SelectedIndex = -1;
            cbPROJValueItem.SelectedIndex = -1;

            cbPROJValueItem.Visible = false;
            txtPROJValueItem.Visible = false;

            lbldatef.Visible = false;
            GetProj();
          //  SetupLabel();             
   
        }

        private void dgProjReferrals_SelectionChanged(object sender, EventArgs e)
        {
            if (dgProjReferrals.SelectedRows.Count > 0)
            {
                string col_group = dgProjReferrals.SelectedRows[0].Cells[5].Value.ToString();
                if ((UserInfo.GroupCode== EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead) &&(col_group == "NPC Supervisor" ||col_group == "NPC Clerk"))
                    btnAssign.Visible = true;
                else
                    btnAssign.Visible = false;
            }
        }

        private void btnAssign_Click(object sender, EventArgs e)
        {
            if (tbReferrals.SelectedTab.Text == "PROJECT")
            {
                if (dgProjReferrals.SelectedRows.Count > 0)
                {
                    //Get the values for the selected row data

                    string SelId = dgProjReferrals.SelectedRows[0].Cells[0].Value.ToString();
                    string Reftype = dgProjReferrals.SelectedRows[0].Cells[2].Value.ToString();
                    string Refstatus = dgProjReferrals.SelectedRows[0].Cells[3].Value.ToString();
                    string Refgroup = dgProjReferrals.SelectedRows[0].Cells[5].Value.ToString();
                    string Refuser = dgProjReferrals.SelectedRows[0].Cells[6].Value.ToString();
                    string Prgdtm = dgProjReferrals.SelectedRows[0].Cells[7].Value.ToString();
                    string Refnote = dgProjReferrals.SelectedRows[0].Cells[8].Value.ToString();
                    string Refcase = lblTab.Text;

                    frmReferralReviewAssignPopup popup = new frmReferralReviewAssignPopup(SelId, Reftype, Refgroup, Refuser, Refstatus, Prgdtm, Refnote, Refcase);
                    popup.StartPosition = FormStartPosition.CenterParent;
                    popup.ShowDialog();
                    if (popup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        return;
                    else
                    {
                        SearchItem2();
                        HighlightRowForId(SelId);
                      
                    }
                }
            }
            else if (tbReferrals.SelectedTab.Text == "RESPONDENT")
            {
                if (dgRespReferrals.SelectedRows.Count > 0)
                {
                    //Get the values for the selected row data
                    string SelId = dgRespReferrals.SelectedRows[0].Cells[0].Value.ToString();
                    string Reftype = dgRespReferrals.SelectedRows[0].Cells[1].Value.ToString();
                    string Refgroup = dgRespReferrals.SelectedRows[0].Cells[4].Value.ToString();
                    string Refuser = dgRespReferrals.SelectedRows[0].Cells[5].Value.ToString();
                    string Refstatus = dgRespReferrals.SelectedRows[0].Cells[2].Value.ToString();
                    string Prgdtm = dgRespReferrals.SelectedRows[0].Cells[6].Value.ToString();
                    string Refnote = dgRespReferrals.SelectedRows[0].Cells[7].Value.ToString();
                    string Refcase = lblTab.Text;
                    
                    frmReferralReviewAssignPopup popup = new frmReferralReviewAssignPopup(SelId, Reftype, Refgroup, Refuser, Refstatus, Prgdtm, Refnote, Refcase);
                    popup.StartPosition = FormStartPosition.CenterParent;
                    popup.ShowDialog();
                    if (popup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        return;
                    else
                    {
                        SearchItem2();
                        HighlightRowForId(SelId);
                    }
                }
            }
        }

        private void SearchItem2()
        {
            if (tbReferrals.SelectedIndex == 0)
            {
                string id = "";
                string usrnme = "";
                string prgdtm = "";
                string type = "";
                string status = "";
                string group = "";
                string assigned = "";
                string newtc = "";

                DataTable dt;

                Cursor.Current = Cursors.WaitCursor;

                if (Id == "")
                {

                    //for id

                    if (cbItem.SelectedIndex == 0)
                    {
                        id = txtPROJValueItem.Text.Trim();
                    }

                    // for newtc

                    else if (cbItem.SelectedIndex == 1)
                    {
                        newtc = cbPROJValueItem.Text.Trim();
                    }

                    // for type

                    else if (cbItem.SelectedIndex == 2)
                    {
                        type = cbPROJValueItem.Text.Trim();

                        //Reassign the radio button values to their database equivalent

                        switch (type)
                        {
                            case "Late Receipt": type = "1"; break;
                            case "Correct Flags": type = "2"; break;
                            case "Data Issue": type = "3"; break;
                            case "PNR/Address": type = "4"; break;
                            case "Other": type = "5"; break;
                        }
                    }

                    // for status

                    else if (cbItem.SelectedIndex == 3)
                    {
                        status = cbPROJValueItem.Text.Trim();

                        switch (status)
                        {
                            case "Active": status = "A"; break;
                            case "Pending": status = "P"; break;
                            case "Complete": status = "C"; break;
                        }
                    }

                    // for usrname

                    else if (cbItem.SelectedIndex == 4)
                    {
                        usrnme = cbPROJValueItem.Text.Trim();
                    }

                    // for group

                    else if (cbItem.SelectedIndex == 5)
                    {
                        group = cbPROJValueItem.Text.Trim();

                        switch (group)
                        {
                            case "HQ Supervisor": group = "1"; break;
                            case "HQ Analyst": group = "2"; break;
                            case "NPC Supervisor": group = "3"; break;
                            case "NPC Clerk": group = "4"; break;
                        }
                    }
                    else if (cbItem.SelectedIndex == 6)
                    {
                        assigned = cbPROJValueItem.Text.Trim();
                    }

                    // for prgdtm

                    else if (cbItem.SelectedIndex == 7)
                    {
                        /*Verify date, and convert date to MM/DD/YYYY format */

                            if (GeneralFunctions.VerifyDate(txtPROJValueItem.Text.Trim()))
                                prgdtm = GeneralFunctions.ConvertDateFormat(txtPROJValueItem.Text);

                    }
                }
                else
                    id = Id;

                if ((usrnme == "") && (id == "") && (prgdtm == "") && (type == "") &&
                    (status == "") && (group == "") && (newtc == "") && (assigned == ""))
                {
                    dt = dataObject.GetProjReferralReviewTable("", "", "", "", "", "", "", "");
                    SetupProjCols(dt);
                }
                else
                {
                    dt = dataObject.GetProjReferralReviewTable(id, type, group, assigned, status, newtc, usrnme, prgdtm);
                    SetupProjCols(dt);

                }
                if (ckMySec.Checked)
                {
                    DataTable dtmyProjItem = dt.Clone();
                    foreach (DataRow row in dt.Rows)
                    {
                        string ntc = row["NEWTC"].ToString().Substring(0, 2);
                        if (dy.Contains(ntc))
                            dtmyProjItem.ImportRow(row);
                    }
                    dgProjReferrals.DataSource = dtmyProjItem;
                }
                else
                    dgProjReferrals.DataSource = dt;

                //display the row count on the screen after search

                lblCasesCount.Text = dgProjReferrals.Rows.Count.ToString() + " PROJECT REFERRALS";

                setProjItemColumnHeader();

                Cursor.Current = Cursors.Default;

            }

            if (tbReferrals.SelectedIndex == 1)
            {
                string respid = "";
                string usrnme = "";
                string prgdtm = "";
                string type = "";
                string status = "";
                string group = "";
                string assigned = "";

                DataTable rdt;

                Cursor.Current = Cursors.WaitCursor;

                if (Respid == "")
                {

                    //for respid

                    if (cbItem.SelectedIndex == 0)
                    {
                        respid = txtRESPValueItem.Text.Trim();
                    }

                    // for type

                    else if (cbItem.SelectedIndex == 1)
                    {
                        type = cbRESPValueItem.Text.Trim();

                        //Reassign the radio button values to their database equivalent

                        switch (type)
                        {
                            case "Late Receipt": type = "1"; break;
                            case "Correct Flags": type = "2"; break;
                            case "Data Issue": type = "3"; break;
                            case "PNR/Address": type = "4"; break;
                            case "Other": type = "5"; break;
                        }
                    }

                    // for status

                    else if (cbItem.SelectedIndex == 2)
                    {
                        status = cbRESPValueItem.Text.Trim();

                        switch (status)
                        {
                            case "Active": status = "A"; break;
                            case "Pending": status = "P"; break;
                            case "Complete": status = "C"; break;
                        }
                    }

                    // for usrname

                    else if (cbItem.SelectedIndex == 3)
                    {
                        usrnme = cbRESPValueItem.Text.Trim();
                    }

                    // for group

                    else if (cbItem.SelectedIndex == 4)
                    {
                        group = cbRESPValueItem.Text.Trim();

                        switch (group)
                        {
                            case "HQ Supervisor": group = "1"; break;
                            case "HQ Analyst": group = "2"; break;
                            case "NPC Supervisor": group = "3"; break;
                            case "NPC Clerk": group = "4"; break;
                        }
                    }

                    else if (cbItem.SelectedIndex == 5)
                    {
                        assigned = cbRESPValueItem.Text.Trim();
                    }

                    // for prgdtm

                    else if (cbItem.SelectedIndex == 6)
                    {
                        /*Verify date, and convert date to MM/DD/YYYY format */
                        if (GeneralFunctions.VerifyDate(txtRESPValueItem.Text.Trim()))
                            prgdtm = GeneralFunctions.ConvertDateFormat(txtRESPValueItem.Text);
                             
                    }
                }
                else
                    respid = Respid;

                if ((usrnme == "") && (respid == "") && (prgdtm == "") && (type == "") && (status == "") && (group == "") && (assigned == ""))
                    rdt = dataObject.GetRespReferralReviewTable("", "", "", "", "", "", "");
                else
                    rdt = dataObject.GetRespReferralReviewTable(respid, type, group, assigned, status, usrnme, prgdtm);

                SetupRespCols(rdt);
                dgRespReferrals.DataSource = rdt;

                //display the row count on the screen

                lblCasesCount.Text = dgRespReferrals.Rows.Count.ToString() + " RESPONDENT REFERRALS";

                setRespItemColumnHeader();

                Cursor.Current = Cursors.Default;
            }
        }


    }
}
