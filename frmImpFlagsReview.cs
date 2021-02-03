/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmImpFlagsReview.cs	    	

Programmer:         Cestine Gill

Creation Date:      09/09/2015

Inputs:             None

Parameters:		    None
                 
Outputs:		    None

Description:	    This program displays the screen to review cases failing Edits

Detailed Design:    Detailed Design for Improvements Flag Review 

Other:	            Called from: 
 
Revision History:	
***********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
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
    public partial class frmImpFlagsReview : Cprs.frmCprsParent
    {

        #region Member Variables

        //StringFormat strFormat; //Used to format the grid rows.
        //ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        //ArrayList arrColumnWidths = new ArrayList();//Used to save column widths
        //int iCellHeight = 0; //Used to get/set the datagridview cell height
        //int iTotalWidth = 0; //
        //int iRow = 0;//Used as counter
        //bool bFirstPage = false; //Used to check whether we are printing first page
        //bool bNewPage = false;// Used to check whether we are printing a new page
        //int iHeaderHeight = 0; //Used for the header height
        //int pageCount = 1;
        string print_selection = "";
        #endregion

        private ImpFlagReviewData dataObject;

        public frmImpFlagsReview()
        {
            InitializeComponent();
            dataObject = new ImpFlagReviewData();
        }

        private void frmImpFlagsReview_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            DataTable dt = new DataTable();

            /*Display the Flags Table*/

            dt = dataObject.CreateFlagsTable();

            /*resize the columns*/

            dgFlagCnt.DataSource = dt;

            dgFlagCnt.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgFlagCnt.RowHeadersVisible = false;  // set it to false if not needed
            dgFlagCnt.ShowCellToolTips = false;

            dgFlagCnt.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgFlagCnt.Columns[1].Width = 150;
            dgFlagCnt.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgFlagCnt.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void dgFlagCnt_SelectionChanged(object sender, EventArgs e)
        {
            if (dgFlagCnt.CurrentCell == null)
                return;

            DataTable dt = new DataTable();

            /*resize the columns*/

            dgCasesFound.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgCasesFound.RowHeadersVisible = false;  // set it to false if not needed
            dgCasesFound.ShowCellToolTips = false;

            for (int i = 0; i < dgCasesFound.ColumnCount; i++)
            {
                
                dgCasesFound.Columns[i].Width = 80;
                dgCasesFound.Columns[9].Width = 100;

                if (i == 1 || i == 3)
                {
                    dgCasesFound.Columns[i].Width = 100;
                }

                if (i == 2 || i == 4)
                {
                    dgCasesFound.Columns[i].Width = 105;
                }

                dgCasesFound.Columns[7].HeaderText  = "WEIGHT";
                dgCasesFound.Columns[8].HeaderText  = "COST";
                dgCasesFound.Columns[9].HeaderText  = "WEIGHTED COST";
                dgCasesFound.Columns[10].HeaderText = "JOBS";
                dgCasesFound.Columns[11].HeaderText = "JOBCODE";

                if (i == 0 || i == 1 || i == 2 || i == 4 || i == 6 || i == 10 || i == 11 || i == 12)
                {
                    dgCasesFound.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }


                if (i == 3 || i == 5 || i == 7 || i == 8 || i == 9)
                {
                    dgCasesFound.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }


            }

            /*Get the current selected row. Depending on the selected row, the table will display
             the ID's info based on Records in CeFlags whose Curr_flag position(flag Number) = ‘1’*/

            int srow = dgFlagCnt.CurrentCell.RowIndex + 1;

            switch (dgFlagCnt.CurrentCell.RowIndex)
            {
                case 0:
                    dt = dataObject.GetFlgProjects(srow);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 1:
                    dt = dataObject.GetFlgProjects(srow);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 2:
                    dt = dataObject.GetFlgProjects(srow);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 3:
                    dt = dataObject.GetFlgProjects(srow);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 4:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                    //skip 5, 6, 7, 8, 9 flags, show flag 10...
                case 5:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 6:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 7:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 8:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 9:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 10:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 11:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 12:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 13:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 14:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 15:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                case 16:
                    dt = dataObject.GetFlgProjects(srow+5);
                    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                    break;
                //case 17:
                //    dt = dataObject.GetFlgProjects(srow);
                //    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                //    break;
                //case 18:
                //    dt = dataObject.GetFlgProjects(srow);
                //    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                //    break;
                //case 19:
                //    dt = dataObject.GetFlgProjects(srow);
                //    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                //    break;
                //case 20:
                //    dt = dataObject.GetFlgProjects(srow);
                //    lblCasesCount.Text = dt.Rows.Count.ToString() + " CASES";
                //    break;
            }

            dgCasesFound.DataSource = dt;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgCasesFound.RowCount == 0)
            {
                MessageBox.Show("The Flag Projects Cases lists is empty. Nothing to print.");
            }
            else
            {
                /*When user clicks print display popup to allow user to choose which table to print*/

                frmImpFlgPrintSel popup = new frmImpFlgPrintSel();

                DialogResult dialogresult = popup.ShowDialog();

                if (dialogresult == DialogResult.OK)
                {
                    print_selection = popup.PrintSelection;

                    if ((print_selection == "Flag Counts" && dgFlagCnt.RowCount > 115) || (print_selection == "Flag Projects" && dgCasesFound.RowCount >= 115))
                    {
                        if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            if (print_selection == "Flag Counts")
                                PrintData(dgFlagCnt, "FLAG COUNTS", "Improvements Flag Counts Print");

                            if (print_selection == "Flag Projects")
                                PrintData(dgCasesFound, "FLAG PROJECTS", "Improvements Flag Projects Print");
                        }
                    }
                    else
                    {

                        if (print_selection == "Flag Counts")
                            PrintData(dgFlagCnt, "FLAG COUNTS", "Improvements Flag Counts Print");
                    
                        if (print_selection == "Flag Projects")
                            PrintData(dgCasesFound, "FLAG PROJECTS", "Improvements Flag Projects Print");
                    }
                }

                else if (dialogresult == DialogResult.Cancel)
                {
                    popup.Dispose();
                }
                popup.Dispose();
            }
        }

        private void PrintData(DataGridView dgPrint, string title, string docname)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = title;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = docname;
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (print_selection == "Flag Counts")
            {
                dgPrint.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                dgPrint.Columns[0].Width = 700;
            }
            else
            {
                for (int i = 0; i < dgPrint.ColumnCount; i++)
                {
                    if (i ==0 || i == 6 || i == 10 || i == 12)
                        dgPrint.Columns[i].Width = 60;
                    else
                        dgPrint.Columns[i].Width = 80;
                }
            }
            
            printer.PrintDataGridViewWithoutDialog(dgPrint);

            if (print_selection == "Flag Counts")
            {
                dgPrint.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            else
            {
                for (int i = 0; i < dgPrint.ColumnCount; i++)
                {
                    if (i == 1 || i == 3 || i == 9)
                        dgPrint.Columns[i].Width = 100;
                    else if (i == 2 || i == 4)
                        dgPrint.Columns[i].Width = 105;
                    else
                        dgPrint.Columns[i].Width = 80;
                }
            }  
        }

       
        private void btnData_Click(object sender, EventArgs e)
        {
            if (dgCasesFound.RowCount > 0)
            {
                this.Hide();   // hide parent

                frmImprovements fm = new frmImprovements();

                DataGridViewSelectedRowCollection rows = dgCasesFound.SelectedRows;

                int index = dgCasesFound.CurrentRow.Index;

                string val1 = dgCasesFound["ID", index].Value.ToString();

                fm.Id = val1;

                List<string> Idlist = new List<string>();

                int cnt = 0;

                foreach (DataGridViewRow dr in dgCasesFound.Rows)
                {
                    string val = dgCasesFound["ID", cnt].Value.ToString();
                    Idlist.Add(val);
                    cnt = cnt + 1;
                }

                fm.Idlist = Idlist;
                fm.CurrIndex = index;

                fm.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");

                fm.ShowDialog();  // show child

                //refresh screen
                dgFlagCnt.DataSource = dataObject.CreateFlagsTable();

                /*resize the columns*/

                dgFlagCnt.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dgFlagCnt.RowHeadersVisible = false;  // set it to false if not needed
                dgFlagCnt.ShowCellToolTips = false;

                for (int i = 0; i < dgFlagCnt.ColumnCount; i++)
                {
                    dgFlagCnt.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgFlagCnt.Columns[1].Width = 150;
                    dgFlagCnt.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            }
            else
            {
                MessageBox.Show("There is no project selected");
            }
        }

        private void frmImpFlagsReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

        
    }
}
