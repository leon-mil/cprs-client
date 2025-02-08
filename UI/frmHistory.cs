/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmHistory.cs	    	

Programmer:         Cestine Gill

Creation Date:      06/12/2015

Inputs:             ID
                    RESPID
                    RESPORG
                    RESPNAME
                    

Parameters:		    None
                 
Outputs:		    None

Description:	    This program displays the History Address data in table form 
                    for a selected ID and RESPID from the Name Address or C700 screen

Detailed Design:    Detailed User Requirements for Comments Display Screen 

Other:	            Called from: frmName.cs and frmC700.cs
 
Revision History:	
***********************************************************************************
 Modified Date :  6/3/2020
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request: CR# 4231 
 Description   :  Add code to show hyperlinks
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
using System.Diagnostics;

namespace Cprs
{
    public partial class frmHistory : Form
    {
        public string Id;
        public string Respid;
        public string Resporg;
        public string Respname;

        private HistoryData dataObject;

        public frmHistory()
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

        private void frmHistory_Load(object sender, EventArgs e)
        {
           
            //Display the id and the respid on the form
            
            DisplayIdRespid();

            DisplayRespondentInfo();

            LoadTables();

            //display the tab name when page is loaded

            lblTab.Text = tbComments.SelectedTab.Text;

            if (tbComments.SelectedTab.Text == "PROJECT")
            {
                lblTab.Location = new System.Drawing.Point(580, 53);
            }
            if (tbComments.SelectedTab.Text == "RESPONDENT")
            {
                lblTab.Location = new System.Drawing.Point(563, 53);
            }

        }

        private void LoadTables()
        {

            dataObject = new HistoryData();

            //Get Project History
            if (Id != "")
                GetProjHistory();
            

            //If Respid not equal blank then get Respondent Comments

            if (Respid != "")
            {
                GetRespHistory();
            }

            if (dgProjHistory.RowCount > 0)
                tbComments.SelectedIndex = 0;                 
            else
                tbComments.SelectedIndex = 1 ; 

            if (dgRespHistory.RowCount == 0 && dgProjHistory.RowCount == 0)
                btnPrint.Enabled = false;
            else
                btnPrint.Enabled = true;

            tbComments.Refresh();

            DisableEnableTab();
        }

        private void tbComments_Selected(object sender, TabControlEventArgs e)
        {
            if (tbComments.SelectedIndex == 0)
            {
                lblTab.Text = tbProject.Text;
                lblTab.Location = new System.Drawing.Point(580, 53);
            }
            if (tbComments.SelectedIndex == 1)
            {
                lblTab.Text = tbRespondent.Text;
                lblTab.Location = new System.Drawing.Point(563, 53);
            }
        }

        private void DisplayIdRespid()
        {
            //This Value is set and passed from the calling form

            txtId.Text = Id;
            txtRespid.Text = Respid;
        }

        private void btnAddRemark_Click(object sender, EventArgs e)
        {
            frmAddCommentPopup popup = new frmAddCommentPopup(Id, Respid);
            popup.ShowDialog();

            string added_type = popup.AddType;
            LoadTables();
            DisableEnableTab();
            if (added_type != "")
            {
                if (added_type == "Proj")
                    tbComments.SelectedIndex = 0;
                else
                    tbComments.SelectedIndex = 1;
            }
        }

        private void DisplayRespondentInfo()
        {
            lblRespname.Text = Respname;
            lblResporg.Text = Resporg;
        }

        //Checks and disables the tab if 
        //there in no data output within it

        private void DisableEnableTab()
        {

            if (dgProjHistory.DataSource == null || dgProjHistory.Rows.Count == 0)
                tbProject.Enabled = false;
            else
                tbProject.Enabled = true;
            

            if (dgRespHistory.DataSource == null || dgRespHistory.Rows.Count == 0)
                tbRespondent.Enabled = false;
            else
                tbRespondent.Enabled = true;
        }

        //Populates and formats the Project table 
        private void GetProjHistory()
        {
            DataTable dtProj = new DataTable();

            dtProj = dataObject.GetProjCommentTable(Id);

            dgProjHistory.DataSource = dtProj;

            dgProjHistory.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgProjHistory.RowHeadersVisible = false;  // set it to false if not needed
          //  dgProjHistory.ShowCellToolTips = false;

            foreach (DataGridViewColumn column in dgProjHistory.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dgProjHistory.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgProjHistory.Columns[i].HeaderText = "DATE/TIME";
                    dgProjHistory.Columns[i].Width = 110;

                }
                if (i == 1)
                {
                    dgProjHistory.Columns[i].HeaderText = "USER";
                    dgProjHistory.Columns[i].Width = 60;

                }
                if (i == 2)
                {
                    dgProjHistory.Columns[i].HeaderText = "COMMENT";
                    dgProjHistory.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

        }

        //Populates and formats the Respondent table 
        private void GetRespHistory()
        {
            DataTable dtResp = new DataTable();

            dtResp = dataObject.GetRespCommentTable(Respid);

            dgRespHistory.DataSource = dtResp;

            dgRespHistory.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgRespHistory.RowHeadersVisible = false;  // set it to false if not needed
       //     dgRespHistory.ShowCellToolTips = false;

            foreach (DataGridViewColumn column in dgRespHistory.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dgRespHistory.ColumnCount; i++)
            {

                if (i == 0)
                {
                    dgRespHistory.Columns[i].HeaderText = "DATE/TIME";
                    dgRespHistory.Columns[i].Width = 110;
                }
                if (i == 1)
                {
                    dgRespHistory.Columns[i].HeaderText = "USER";
                    dgRespHistory.Columns[i].Width = 60;
                }
                if (i == 2)
                {
                    dgRespHistory.Columns[i].HeaderText = "COMMENT";
                    dgRespHistory.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
        }


        private void tbComments_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbComments.Refresh();
        }

        //Prevents selection of the tab if it is disabled

        private void tbComments_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        
        /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (tbComments.SelectedIndex == 0)
            {
                printer.Title = "PROJECT COMMENTS";
                printer.SubTitle = "ID: " + Id;
            }
            else
            {
                printer.Title = "RESPONDENT COMMENTS";
                printer.SubTitle = "RESPID: " + Respid;
            }
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleFont= new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            if (tbComments.SelectedIndex == 0)
                printer.printDocument.DocumentName = "Project Comments";
            else
                printer.printDocument.DocumentName = "Respondent Comments";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (tbComments.SelectedIndex == 0)
            {
                //resize the column
                dgProjHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgProjHistory.Columns[2].Width = 500;
                printer.PrintDataGridViewWithoutDialog(dgProjHistory);

                //resize back the column
                dgProjHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            }
            else
            {
                //resize the column
                dgRespHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgRespHistory.Columns[2].Width = 500;
                printer.PrintDataGridViewWithoutDialog(dgRespHistory);

                //resize back the column
                dgRespHistory.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            Cursor.Current = Cursors.Default;
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgProjHistory_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dgProjHistory.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["COMMTEXT"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["COMMTEXT"] = new DataGridViewLinkCell();
                    // Note that if I want a different link colour for example it must go here
                    DataGridViewLinkCell c = r.Cells["COMMTEXT"] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;
                }
            }
        }

        private void dgProjHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (System.Uri.IsWellFormedUriString(dgProjHistory.Rows[e.RowIndex].Cells["COMMTEXT"].Value.ToString(), UriKind.Absolute))
            {
                string sUrl = dgProjHistory.Rows[e.RowIndex].Cells["COMMTEXT"].Value.ToString();

                ProcessStartInfo sInfo = new ProcessStartInfo(sUrl);

                Process.Start(sInfo);
            }

        }
    }
}

