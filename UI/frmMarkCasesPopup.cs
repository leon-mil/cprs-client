/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmMarkcasesPopup.cs	    	
Programmer:         Christine
Creation Date:      08/23/2016
Inputs:             ID
                    RESPID   
Parameters:		    None            
Outputs:		    None
Description:	    This program displays the mark cases data in table form 
                    for a selected ID and RESPID from the Name Address or C700 screen
Detailed Design:    Detailed User Requirements for Display Screen 
Other:	            Called from: frmName.cs and frmC700.cs
Revision History:	
***********************************************************************************
 Modified Date :  10/12/2016
 Modified By   :  Christine
 Keyword       :  
 Change Request:  
 Description   :  change tab style
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
    public partial class frmMarkCasesPopup : Form
    {
        public string Id;
        public string Respid;

        private ProjMarkData pdataObject;
        private RespMarkData rdataObject;

        public frmMarkCasesPopup()
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

        private void frmMarkCasesPopup_Load(object sender, EventArgs e)
        {
            txtId.Text = Id;
            txtRespid.Text = Respid;

            LoadTables();
        }

        private void LoadTables()
        {
            pdataObject = new ProjMarkData();
            rdataObject = new RespMarkData();

            DisableEnableTab();

            //Get Project Mark

            GetProjMarks();
            DisableEnableTab();

            //If Respid not equal blank then get Respondent Comments

            if (Respid != "")
            {
                GetRespMarks();
                DisableEnableTab();
            }

            if (dgProjMark.RowCount == 0)
            {
                tbMarks.SelectedTab = tbRespondent;
                if (dgRespMark.RowCount == 0)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }
            else
            {
                tbMarks.SelectedTab = tbProject;
                if (dgProjMark.RowCount == 0)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }

            tbMarks.Refresh();
        }

        //Checks and disables the tab if 
        //there in no data output within it

        private void DisableEnableTab()
        {

            if (dgProjMark.DataSource == null || dgProjMark.Rows.Count == 0)
                tbProject.Enabled = false;
            else
                tbProject.Enabled = true;

            if (dgRespMark.DataSource == null || dgRespMark.Rows.Count == 0)
                tbRespondent.Enabled = false;
            else
                tbRespondent.Enabled = true;

            tbMarks.Refresh();
        }

        //Populates and formats the Project table 
        private void GetProjMarks()
        {
            DataTable dtProj = new DataTable();

            dtProj = pdataObject.GetProjMarks(Id);

            dgProjMark.DataSource = dtProj;

            dgProjMark.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgProjMark.RowHeadersVisible = false;  // set it to false if not needed

            foreach (DataGridViewColumn column in dgProjMark.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dgProjMark.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgProjMark.Columns[i].HeaderText = "DATE/TIME";
                    dgProjMark.Columns[i].Width = 140;

                }
                if (i == 1)
                {
                    dgProjMark.Columns[i].HeaderText = "USER";
                    dgProjMark.Columns[i].Width = 80;

                }
                if (i == 2)
                {
                    dgProjMark.Columns[i].HeaderText = "MARK NOTE";
                    dgProjMark.Columns[i].Width = 910;
                    dgProjMark.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                }
               
            }

        }

        //Populates and formats the Respondent table 
        private void GetRespMarks()
        {
            DataTable dtResp = new DataTable();

            dtResp = rdataObject.GetRespMarks(Respid);

            dgRespMark.DataSource = dtResp;

            dgRespMark.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgRespMark.RowHeadersVisible = false;  // set it to false if not needed
            //dgRespMark.ShowCellToolTips = false;

            foreach (DataGridViewColumn column in dgRespMark.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dgRespMark.ColumnCount; i++)
            {

                if (i == 0)
                {
                    dgRespMark.Columns[i].HeaderText = "DATE/TIME";
                    dgRespMark.Columns[i].Width = 140;
                }
                if (i == 1)
                {
                    dgRespMark.Columns[i].HeaderText = "USER";
                    dgRespMark.Columns[i].Width = 80;
                }
                if (i == 2)
                {
                    dgRespMark.Columns[i].HeaderText = "MARK NOTE";
                    dgRespMark.Columns[i].Width = 910;
                    dgRespMark.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                }
               
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmMarkPopup popup = new frmMarkPopup(Id, Respid);
            popup.ShowDialog();

            GetProjMarks();
            GetRespMarks();
            DisableEnableTab();

            if (dgProjMark.RowCount == 0)
            {
                tbMarks.SelectedTab = tbRespondent;
                if (dgRespMark.RowCount == 0)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }
            else
            {
                tbMarks.SelectedTab = tbProject;
                if (dgProjMark.RowCount == 0)
                    btnPrint.Enabled = false;
                else
                    btnPrint.Enabled = true;
            }
        }


        private void tbMarks_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (tbMarks.SelectedTab == tbProject)
            {
                printer.Title = "Project Mark Notes";
                printer.SubTitle = "ID: " + Id;
            }
            else
            {
                printer.Title = "Respondent Mark Notes";
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
            printer.printDocument.DocumentName = "Mark Notes Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (tbMarks.SelectedIndex == 0)
            {
                //resize the note column
                dgProjMark.Columns[2].Width = 600;
                printer.PrintDataGridViewWithoutDialog(dgProjMark);

                //resize back the note column
                dgProjMark.Columns[2].Width = 910;
            }
            else
            {
                //resize the note column
                dgRespMark.Columns[2].Width = 600;
                printer.PrintDataGridViewWithoutDialog(dgRespMark);

                //resize back the note column
                dgRespMark.Columns[2].Width = 910;
            }

            Cursor.Current = Cursors.Default;
        }
  
    }
}
