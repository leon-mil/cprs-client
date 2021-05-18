/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmReferral.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/29/2015

Inputs:             ID
                    RESPID
                                     
Parameters:	    None
                 
Outputs:	    None

Description:	    This program displays the Referral data in table form 
                    for a selected ID and RESPID from the Name Address, 
                    C700 or TFU screens

Detailed Design:    Detailed Design for Referrals

Other:	            Called from: frmName.cs, frmC700.cs, frmTFU.cs
 
Revision Referrals:	
***********************************************************************************
 Modified Date :  1/12/2016
 Modified By   :  Christine
 Keyword       :  
 Change Request:  
 Description   :  Change Tab style
***********************************************************************************
Modified Date :  2/19/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request:  
 Description   :  Allow Analyst update referral which created by others
***********************************************************************************
Modified Date :  4/29/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request:  
 Description   :  Add refuser (assigned) column
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmReferral : Form
    {
        public string Id;
        public string Respid;
        public bool IsChanged;

        #region Member Variables
        private ReferralData dataObject;
        private bool data_loading;
        private MySector ms;

        #endregion

        public frmReferral()
        {
            InitializeComponent();
        }

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

        private void frmReferral_Load(object sender, EventArgs e)
        {

            DisableEnableTab();

            //get my sector data
            MySectorsData md = new MySectorsData();
            ms = md.GetMySectorData(UserInfo.UserName);

            //Display the id and the respid on the form

            DisplayIdRespid();

            LoadTables();

            //display the tab name when page is loaded

            lblTab.Text = tbReferrals.SelectedTab.Text;
            IsChanged = false;

        }

        //This method will re-populate the datagrids after
        //after each datasource refresh occurs

        private void LoadTables()
        {
            data_loading = true;
            dataObject = new ReferralData();

            //Get Project Referrals

            GetProjReferrals();
            DisableEnableTab();

            //If Respid not equal blank then get Respondent Referrals

            if (Respid != "")
            {
                GetRespReferrals();
                DisableEnableTab();
            }

            
            if (dgProjReferrals.RowCount == 0)
            {
                tbReferrals.SelectedTab = tbRespondent;
                if (dgRespReferrals.RowCount == 0)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }
            else
            {
                tbReferrals.SelectedTab = tbProject;
                if (dgProjReferrals.RowCount == 0)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }

            tbReferrals.Refresh();

            data_loading = false;
            if (tbReferrals.SelectedIndex == 0)
                SetButtons(dgProjReferrals);
            else
                SetButtons(dgRespReferrals);
        }

        //Changes the page title label dependent to which table is selected

        private void tbReferrals_Selected(object sender, TabControlEventArgs e)
        {

            if (tbReferrals.SelectedIndex == 0)
            {
                lblTab.Text = tbProject.Text;
                lblTab.Location = new System.Drawing.Point(557, 53);
            }
            if (tbReferrals.SelectedIndex == 1)
            {
                lblTab.Text = tbRespondent.Text;
                lblTab.Location = new System.Drawing.Point(538, 53);
            }
        }

        private void DisplayIdRespid()
        {
            //This Value is set and passed from the calling form

            txtId.Text = Id;
            txtRespid.Text = Respid;
        }

        //Checks and disables the tab if 
        //there in no data output within it

        private void DisableEnableTab()
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

   
        //Populates and formats the Project table

        private void GetProjReferrals()
        {
            DataTable dtProj = new DataTable();

            //populate the data grid with the referral cases for 
            //the ID passed from the calling form

            dtProj = dataObject.GetProjReferralTable(Id);
            DataTable dt = dtProj.Clone();

            //if (dtProj.Rows.Count > 0 && (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead))
            //{
            //    string filterExp = "USRNME = '" + UserInfo.UserName + "' or REFGROUP = 3";
            //    string sortExp = "PRGDTM";
            //    DataRow[] drarray;
            //    drarray = dtProj.Select(filterExp, sortExp, DataViewRowState.CurrentRows);

            //    if (drarray.Length > 0)
            //        dt = drarray.CopyToDataTable();
            //}
            //else if (dtProj.Rows.Count > 0 && UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            //{
            //    string filterExp = "USRNME = '" + UserInfo.UserName + "' or REFGROUP = 3";
            //    string sortExp = "PRGDTM";
            //    DataRow[] drarray;
            //    drarray = dtProj.Select(filterExp, sortExp, DataViewRowState.CurrentRows);

            //    if (drarray.Length > 0)
            //        dt = drarray.CopyToDataTable();
            //}
            //else
                dt = dtProj;

            dgProjReferrals.DataSource = dt;

            //format the table and columns

            dgProjReferrals.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgProjReferrals.RowHeadersVisible = false;  // set it to false if not needed
            dgProjReferrals.ShowCellToolTips = false;

            for (int i = 0; i < dgProjReferrals.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgProjReferrals.Columns[i].HeaderText = "TYPE";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 1)
                {
                    dgProjReferrals.Columns[i].HeaderText = "STATUS";
                    dgProjReferrals.Columns[i].Width = 80;
                    dgProjReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                }
                if (i == 2)
                {
                    dgProjReferrals.Columns[i].HeaderText = "USER";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 3)
                {
                    dgProjReferrals.Columns[i].HeaderText = "GROUP";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 4)
                {
                    dgProjReferrals.Columns[i].HeaderText = "ASSIGNED";
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
                if (i == 7)
                {
                    dgProjReferrals.Columns[i].Visible = false;
                }
               
                
            }

            //reassign the value from the database to a 
            //user-friendlier representation in the table

            int reftypeColumn = 0; //Put your column number here
            int refgroupColumn = 3;
            int refstatusColumn = 1;

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
                    case "C":
                        dt.Rows[i][refstatusColumn] = "Complete";
                        break;
                    case "P":
                        dt.Rows[i][refstatusColumn] = "Pending";
                        break;
                }
            }
        }

        //Populates and formats the Respondent table 

        private void GetRespReferrals()
        {
            DataTable dtResp = new DataTable();

            //populate the data grid with the referral cases for 
            //the RESPID passed from the calling form

            dtResp = dataObject.GetRespReferralTable(Respid);
            DataTable dt = dtResp.Clone();

            //if (dtResp.Rows.Count > 0 && (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead))
            //{
            //    string filterExp = "USRNME = '" + UserInfo.UserName + "' or REFGROUP = 3";
            //    string sortExp = "PRGDTM";
            //    DataRow[] drarray;
            //    drarray = dtResp.Select(filterExp, sortExp, DataViewRowState.CurrentRows);

            //    if (drarray.Length > 0)
            //        dt = drarray.CopyToDataTable();
            //}
            //else if (dtResp.Rows.Count > 0 && UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            //{
            //    string filterExp = "USRNME = '" + UserInfo.UserName + "' or REFGROUP = 3";
            //    /*  string filterExp = "USRNME = '" + UserInfo.UserName + "'"; ;*/
            //    string sortExp = "PRGDTM";
            //    DataRow[] drarray;
            //    drarray = dtResp.Select(filterExp, sortExp, DataViewRowState.CurrentRows);

            //    if (drarray.Length > 0)
            //        dt = drarray.CopyToDataTable();
            //}
            //else
                dt = dtResp;

            dgRespReferrals.DataSource = dt;

            //format the datagrid and columns

            dgRespReferrals.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgRespReferrals.RowHeadersVisible = false;  // set it to false if not needed
            dgRespReferrals.ShowCellToolTips = false;

            for (int i = 0; i < dgRespReferrals.ColumnCount; i++)
            {

                if (i == 0)
                {
                    dgRespReferrals.Columns[i].HeaderText = "TYPE";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 1)
                {
                    dgRespReferrals.Columns[i].HeaderText = "STATUS";
                    dgRespReferrals.Columns[i].Width = 80;
                    dgRespReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                }
                if (i == 2)
                {
                    dgRespReferrals.Columns[i].HeaderText = "USER";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 3)
                {
                    dgRespReferrals.Columns[i].HeaderText = "GROUP";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 4)
                {
                    dgRespReferrals.Columns[i].HeaderText = "ASSIGNED";
                    dgRespReferrals.Columns[i].Width = 100;

                }
                if (i == 5)
                {
                    dgRespReferrals.Columns[i].HeaderText = "DATE/TIME";
                    dgRespReferrals.Columns[i].Width = 120;

                }
                if (i == 6)
                {
                    dgRespReferrals.Columns[i].HeaderText = "NOTE";
                    dgRespReferrals.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgRespReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
              
            }

            //reassign the value from the database to a 
            //user-friendlier representation in the table

            int reftypeColumn = 0; //Put your column number here
            int refgroupColumn = 3;
            int refstatusColumn = 1;

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                switch (dt.Rows[i][reftypeColumn].ToString())
                {
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
                    case "C":
                        dt.Rows[i][refstatusColumn] = "Complete";
                        break;
                    case "P":
                        dt.Rows[i][refstatusColumn] = "Pending";
                        break;
                }
            }
        }
        
        private void tbReferrals_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbReferrals.Refresh();
        }

        //Prevents selection of the tab if it is disabled

        private void tbReferrals_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        //Display the update referral popup and retain the datagrid values

        private void btnUpdReferral_Click(object sender, EventArgs e)
        {
            if (tbReferrals.SelectedTab.Text == "PROJECT")
            {
                if (dgProjReferrals.SelectedRows.Count > 0)
                {
                    //Get the values for the selected row data

                    string Reftype = dgProjReferrals.SelectedRows[0].Cells[0].Value.ToString();
                    string Refstatus = dgProjReferrals.SelectedRows[0].Cells[1].Value.ToString();
                    string Usrnme = dgProjReferrals.SelectedRows[0].Cells[2].Value.ToString();
                    string Refgroup = dgProjReferrals.SelectedRows[0].Cells[3].Value.ToString();
                    string Refuser = dgProjReferrals.SelectedRows[0].Cells[4].Value.ToString();
                    string Prgdtm = dgProjReferrals.SelectedRows[0].Cells[5].Value.ToString();
                    string Refnote = dgProjReferrals.SelectedRows[0].Cells[6].Value.ToString();
                    
                    string Refcase = lblTab.Text;

                    frmUpdReferralPopup URpopup = new frmUpdReferralPopup(Id, Respid, Reftype, Refstatus, Refgroup, Refuser, Refcase, Refnote, Prgdtm, Usrnme);

                    URpopup.ShowDialog();
                    if (URpopup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        return;
                    else
                    {
                        LoadTables();
                        DisableEnableTab();
                        IsChanged = true;
                    }
                }
            }
            else if (tbReferrals.SelectedTab.Text == "RESPONDENT")
            {
                if (dgRespReferrals.SelectedRows.Count > 0)
                {
                    //Get the values for the selected row data

                    string Reftype = dgRespReferrals.SelectedRows[0].Cells[0].Value.ToString();
                    string Refgroup = dgRespReferrals.SelectedRows[0].Cells[3].Value.ToString();
                    string Refuser = dgRespReferrals.SelectedRows[0].Cells[4].Value.ToString();
                    string Usrnme = dgRespReferrals.SelectedRows[0].Cells[2].Value.ToString();
                    string Refnote = dgRespReferrals.SelectedRows[0].Cells[6].Value.ToString();
                    string Refstatus = dgRespReferrals.SelectedRows[0].Cells[1].Value.ToString();
                    string Prgdtm = dgRespReferrals.SelectedRows[0].Cells[5].Value.ToString();
                    
                    string Refcase = lblTab.Text;

                    frmUpdReferralPopup URpopup = new frmUpdReferralPopup(Id, Respid, Reftype, Refstatus, Refgroup,Refuser, Refcase, Refnote, Prgdtm, Usrnme);

                    URpopup.ShowDialog();
                    if (URpopup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                        return;
                    else
                    {
                        LoadTables();
                        DisableEnableTab();
                        tbReferrals.SelectedTab = tbRespondent;
                        IsChanged = true;
                    }
                }
            }
        }

        //Display the Add Referral Popup

        private void btnAddReferral_Click(object sender, EventArgs e)
        {
            frmAddReferralPopup ARpopup = new frmAddReferralPopup(Id, Respid);
            ARpopup.ShowDialog();
            if (ARpopup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            else
            {
                LoadTables();
                DisableEnableTab();
                if (tbReferrals.SelectedIndex == 0)
                {
                    SetButtons(dgProjReferrals);
                    if (dgProjReferrals.RowCount > 0)
                        btnPrint.Enabled = true;
                }
                else
                {
                    SetButtons(dgRespReferrals);
                    if (dgRespReferrals.RowCount > 0)
                        btnPrint.Enabled = true;
                }
                IsChanged = true;
            }
        }

        //close the window

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled ? Color.DarkBlue : Color.Gray;
        }

        private void btnUpdReferral_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            var drawBrush = new SolidBrush(btn.ForeColor);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            btnUpdReferral.Text = string.Empty; //remove the button text
            e.Graphics.DrawString("UPDATE REFERRAL", btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();
        }

 /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (tbReferrals.SelectedIndex == 0)
            {
                printer.Title = "Project Referrals";
                printer.SubTitle = "ID: " + Id;
            }
            else
            {
                printer.Title = "Respondent Referrals";
                printer.SubTitle = "RESPID: " + Respid;
            }
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
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
                dgProjReferrals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgProjReferrals.Columns[6].Width = 400;
                printer.PrintDataGridViewWithoutDialog(dgProjReferrals);

                //resize back the note column
                dgProjReferrals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            else
            {
                //resize the note column
                dgRespReferrals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgRespReferrals.Columns[6].Width = 400;
                printer.PrintDataGridViewWithoutDialog(dgRespReferrals);

                //resize back the note column
                dgRespReferrals.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            Cursor.Current = Cursors.Default;
        }

        
        //set update, add buttons
        private void SetButtons(DataGridView dg)
        {
            if (dg.SelectedRows.Count > 0)
            {
                //get user name and newtc from row
                string Usrnme = dg.SelectedRows[0].Cells[2].Value.ToString();
               
                btnUpdReferral.Enabled = true;
                btnAddReferral.Enabled = true;

                if (UserInfo.GroupCode == EnumGroups.HQAnalyst)
                {
                    if (UserInfo.UserName != Usrnme)
                    {
                        if (tbReferrals.SelectedIndex == 0)
                        {
                            string newtc = dg.SelectedRows[0].Cells[8].Value.ToString();
                            
                            /*set up referer button based on my sector */
                            if (ms != null && !ms.CheckInMySector(newtc))
                                btnUpdReferral.Enabled = false;
                        }
                       // else
                            //btnUpdReferral.Enabled = false;
                    }
                }
                else if (UserInfo.GroupCode == EnumGroups.HQMathStat || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                {
                    if (UserInfo.UserName != Usrnme)
                        btnUpdReferral.Enabled = false;
                }

            }
            else
                btnUpdReferral.Enabled = false;
        }

        private void dgProjReferrals_SelectionChanged(object sender, EventArgs e)
        {
            if (!data_loading)
                SetButtons(dgProjReferrals);
        }

        private void dgRespReferrals_SelectionChanged(object sender, EventArgs e)
        {
            if (!data_loading)
                SetButtons(dgRespReferrals);
        }
    }
}
