
/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmImpTabReview.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/28/2015

Inputs:             None

Parameters:		    None
                    
Outputs:		    None

Description:	    This program displays the Winsorized and Unwinsorized 
                    Improvement data in table form

Detailed Design:    Detailed User Requirements for Improvements Display Screen 

Other:	            Called from: 
 
Revision History:	
***********************************************************************************
 Modified Date :  10/12/2016
 Modified By   :  christine Zhang
 Keyword       :  
 Change Request:  
 Description   :  change tab style and highlight color in datagrid
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
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmImpTabReview : Cprs.frmCprsParent
    {
       
        private ImpTabReviewData dataObject;

        public frmImpTabReview(System.Windows.Forms.Control f)
        {
            InitializeComponent();
        }

        public frmImpTabReview()
        {
            InitializeComponent();
        }

        private void frmImpTabReview_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            dataObject = new ImpTabReviewData();

            //display the table
            GetWinsorized();
            GetUnWinsorized();

            /*display the tab name when page is loaded*/
            lblTab.Text = tbImprovements.SelectedTab.Text;
            if (tbImprovements.SelectedTab.Text == "WINSORIZED")
            {
                lblTab.Location = new System.Drawing.Point(535, 86);
            }
            if (tbImprovements.SelectedTab.Text == "UNWINSORIZED")
            {
                lblTab.Location = new System.Drawing.Point(530, 86);
            }

        }

        /*Displays the tab header name at the top of the page based on the selected tab*/

        private void tbImprovements_Selected(object sender, TabControlEventArgs e)
        {

            if (tbImprovements.SelectedIndex == 0)
            {
                lblTab.Text = tbWinsorized.Text;
                lblTab.Location = new System.Drawing.Point(535, 86);
            }
            if (tbImprovements.SelectedIndex == 1)
            {
                lblTab.Text = tbUnwinsorized.Text;
                lblTab.Location = new System.Drawing.Point(530, 86);
            }
        }

        //Populates and formats the Winsorized table 
        private void GetWinsorized()
        {
            try
            {
                dgWinsorized.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

                // set it to false if not needed
                dgWinsorized.RowHeadersVisible = false;
                dgWinsorized.ScrollBars = ScrollBars.Both;

                DataTable dtWinsorized = new DataTable();

                //Assign the dataObject coming from the DLL

                dtWinsorized = dataObject.GetWinsorizedTable();

                dgWinsorized.DataSource = dtWinsorized;

                /*Assign the column names*/
                dgWinsorized.Columns[0].HeaderText = "DATE";
                dgWinsorized.Columns[1].HeaderText = "LAG0";
                dgWinsorized.Columns[2].HeaderText = "LAG1";
                dgWinsorized.Columns[3].HeaderText = "LAG2";
                dgWinsorized.Columns[4].HeaderText = "LAG3";
                dgWinsorized.Columns[5].HeaderText = "TOTAL CASES";
                dgWinsorized.Columns[6].HeaderText = "TOTAL JOBS";
                dgWinsorized.Columns[7].HeaderText = "AVG COST";

                for (int i = 0; i < dgWinsorized.ColumnCount; i++)
                {
                    dgWinsorized.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Table is blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Populates and formats the Winsorized table 
        private void GetUnWinsorized()
        {
            try
            {
                dgUnwinsorized.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

                // set it to false if not needed
                dgUnwinsorized.RowHeadersVisible = false;
                dgUnwinsorized.ScrollBars = ScrollBars.Both;

                DataTable dtUnWinsorized = new DataTable();

                dtUnWinsorized = dataObject.GetUnWinsorizedTable();

                dgUnwinsorized.DataSource = dtUnWinsorized;

                /*Assign the column names*/
                dgUnwinsorized.Columns[0].HeaderText = "DATE";
                dgUnwinsorized.Columns[1].HeaderText = "LAG0";
                dgUnwinsorized.Columns[2].HeaderText = "LAG1";
                dgUnwinsorized.Columns[3].HeaderText = "LAG2";
                dgUnwinsorized.Columns[4].HeaderText = "LAG3";
                dgUnwinsorized.Columns[5].HeaderText = "TOTAL CASES";
                dgUnwinsorized.Columns[6].HeaderText = "TOTAL JOBS";
                dgUnwinsorized.Columns[7].HeaderText = "AVG COST";

                for (int i = 0; i < dgUnwinsorized.ColumnCount; i++)
                {
                    dgUnwinsorized.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Table is blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (tbImprovements.SelectedIndex == 0)
                printer.Title = "IMPROVEMENTS WINSORIZED TAB REVIEW";
            else
                printer.Title = "IMPROVEMENTS UNWINSORIZED TAB REVIEW";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            if (tbImprovements.SelectedIndex == 0)
                printer.printDocument.DocumentName = "Improvements Winsorized Tab Review";
            else
                printer.printDocument.DocumentName = "Improvements Unwinsorized Tab Review";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (tbImprovements.SelectedIndex == 0)
            {
                //resize the column
                for (int i = 0; i < dgWinsorized.ColumnCount; i++)
                {
                    dgWinsorized.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgWinsorized.Columns[i].Width = 100;
                }
                printer.PrintDataGridViewWithoutDialog(dgWinsorized);

                //resize back the column
                for (int i = 0; i < dgWinsorized.ColumnCount; i++)
                {
                    dgWinsorized.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }

            }
            else
            {
                //resize the column
                for (int i = 0; i < dgUnwinsorized.ColumnCount; i++)
                {
                    dgUnwinsorized.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dgUnwinsorized.Columns[i].Width = 100;
                }
                printer.PrintDataGridViewWithoutDialog(dgUnwinsorized);

                //resize back the note column
                for (int i = 0; i < dgUnwinsorized.ColumnCount; i++)
                {
                    dgUnwinsorized.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            Cursor.Current = Cursors.Default;
        }

           
        private void frmImpTabReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

    }
}
