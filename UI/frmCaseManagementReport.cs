/**************************************************************************************
Econ App Name  : CPRS
Project Name   : Construction Progress Report Survey
Program Name   : frmCaseManagementReport.cs	    	

Programmer     : Srini Natarajan
Creation Date  : 12/19/2016

Inputs         : None
Parameters     : None 
Outputs        : Case Management Report
 
Description    : This screen displays data from case_hist table for previous months and 
                 tabulates data from Master, Sample, Respondent, Case_Hist, presample,
                 dcpinitial, psamp_hist, dcp_hist for current month. 

Detailed Design: None 
Other          : Called by: Main form
 
Revision Hist  : See Below	
****************************************************************************************
Modified Date  :  5/28/2019
Modified By    :  Christine Zhang
Keyword        :  
Change Request :  CR#3195
Description    :  display vip% for each column in current tab
****************************************************************************************
Modified Date  :  8/6/2019
Modified By    :  Christine Zhang
Keyword        :  
Change Request :  CR#???
Description    :  print screen instead print grid
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using CprsBLL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;

namespace Cprs
{

    public partial class frmCaseManagementReport : frmCprsParent
    {
        private CaseMgmtRprtData dataObject;
        string CurrMonth = string.Empty;
        string currYearMon1 = string.Empty;
        string currYearMon2 = string.Empty;
        string currYearMon3 = string.Empty;
        string currYearMon4 = string.Empty;
        string currYearMon5 = string.Empty;
        string currYearMon6 = string.Empty;
        string currYearMon7 = string.Empty;
        string currYearMon8 = string.Empty;
        string currYearMon9 = string.Empty;
        string currYearMon10 = string.Empty;
        string currYearMon11 = string.Empty;
        string currYearMon12 = string.Empty;
        //DataGridView dgNum = new DataGridView();

        private delegate void ShowMessageDelegate();
        private bool form_loading = false;

        public frmCaseManagementReport()
        {
            InitializeComponent();
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");
        }

        private void frmCaseManagementReport_Load(object sender, EventArgs e)
        {
            form_loading = true;

            GetMonthsList();
            //create instance of data object
            List<string> dy = new List<string>();
            dy.Add(CurrMonth);
            dy.Add(currYearMon1);
            dy.Add(currYearMon2);
            dy.Add(currYearMon3);
            dy.Add(currYearMon4);
            dy.Add(currYearMon5);
            dy.Add(currYearMon6);
            dy.Add(currYearMon7);
            dy.Add(currYearMon8);
            dy.Add(currYearMon9);
            dy.Add(currYearMon10);
            dy.Add(currYearMon11);
            dy.Add(currYearMon12);
            cbStatp.DataSource = dy;

            dataObject = new CaseMgmtRprtData();

            DataTable dt = dataObject.GetCaseHistPrevMon("1", cbStatp.Text);
            if (dt.Rows.Count == 0)
            {
                BeginInvoke(new ShowMessageDelegate(ShowMessage));
            }
            else
                FormLoad();

            form_loading = false;
        }

        private void ShowMessage()
        {
            /*show message if no data exist */
            MessageBox.Show("Data hasn't been loaded. Cannot show the report.");
            this.Close();
            frmHome fH = new frmHome();
            fH.Show();
        }

        private void FormLoad()
        {
            Cursor.Current = Cursors.WaitCursor;
            if (tabs.SelectedTab == tabPage1)
            {
                DataTable table = new DataTable();
                dgt1.DataSource = table;
                tabs.SelectedTab = tabPage1;
                getCaseHist("1", cbStatp.Text);
               // UpdateFirstColumn1(dgt1);
            }

            if (tabs.SelectedTab == tabPage2)
            {
                if (cbStatp.Text != CurrMonth)
                {
                    DataTable table2 = new DataTable();
                    dgt2.DataSource = table2;
                    getCaseHist("2", cbStatp.Text);
                    UpdateFirstColumn2(dgt2);
                    setItemColumnHeader(dgt2);
                }
                else
                {
                    //use the stored procedure sp_NPCCaseMgmtInit
                    dgt2.DataSource = dataObject.GetInitialtab();
                    setItemColumnHeader(dgt2);
                }
            }

            if (tabs.SelectedTab == tabPage3)
            {
                
                if (cbStatp.Text != CurrMonth)
                {
                    DataTable table3 = new DataTable();
                    dgt3.DataSource = table3;
                    getCaseHist("3", cbStatp.Text);
                    UpdateFirstColumn3(dgt3);
                    setItemColumnHeader(dgt3);

                    buildViptable();
                }
                else
                {
                    //use the stored procedure sp_NPCCaseMgmtCurr
                    dgt3.DataSource = dataObject.GetCurrenttab(cbStatp.Text);
                    setItemColumnHeader(dgt3);

                    buildViptable();
                }
            }

            Cursor.Current = Cursors.Default;
        }

        private void buildViptable()
        {
            //get table from datagrid
            DataTable dt = (DataTable)dgt3.DataSource;

            //get same structure as top table
            // Create a DataTable and add two Columns to it
            DataTable dtvip = new DataTable();
            dtvip.Columns.Add("Name", typeof(string));
            dtvip.Columns.Add("c0", typeof(decimal));
            dtvip.Columns.Add("c1", typeof(decimal));
            dtvip.Columns.Add("c2", typeof(decimal));
            dtvip.Columns.Add("c3", typeof(decimal));
            dtvip.Columns.Add("c4", typeof(decimal));
            dtvip.Columns.Add("c5", typeof(decimal));

            //add vip row
            DataRow dr = dtvip.NewRow();
            dr[0] = "VIP%";
            dr[1] = Math.Round((1-Convert.ToDecimal(dt.Rows[1][1])/Convert.ToDecimal(dt.Rows[0][1]))*100,1, MidpointRounding.AwayFromZero);
            dr[2] = Math.Round((1-Convert.ToDecimal(dt.Rows[1][2])/ Convert.ToDecimal(dt.Rows[0][2]))*100, 1, MidpointRounding.AwayFromZero);
            dr[3] = Math.Round((1-Convert.ToDecimal(dt.Rows[1][3])/ Convert.ToDecimal(dt.Rows[0][3]))*100, 1, MidpointRounding.AwayFromZero);
            dr[4] = Math.Round((1-Convert.ToDecimal(dt.Rows[1][4])/ Convert.ToDecimal(dt.Rows[0][4]))*100, 1, MidpointRounding.AwayFromZero);
            dr[5] = Math.Round((1-Convert.ToDecimal(dt.Rows[1][5])/ Convert.ToDecimal(dt.Rows[0][5]))*100, 1, MidpointRounding.AwayFromZero);
            dr[6] = Math.Round((1-Convert.ToDecimal(dt.Rows[1][6])/ Convert.ToDecimal(dt.Rows[0][6]))*100, 1, MidpointRounding.AwayFromZero);

            dtvip.Rows.Add(dr);

            dgVip.DataSource = dtvip;

            dgVip.Columns[0].Width = 350;

            dgVip.Columns[1].Width = 90;
            dgVip.Columns[1].DefaultCellStyle.Format = "0.0\\%";
            dgVip.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgVip.Columns[2].Width = 90;
            dgVip.Columns[2].DefaultCellStyle.Format = "0.0\\%";
            dgVip.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgVip.Columns[3].Width = 80;
            dgVip.Columns[3].DefaultCellStyle.Format = "0.0\\%";
            dgVip.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgVip.Columns[4].Width = 110;
            dgVip.Columns[4].DefaultCellStyle.Format = "0.0\\%";
            dgVip.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgVip.Columns[5].Width = 90;
            dgVip.Columns[5].DefaultCellStyle.Format = "0.0\\%";
            dgVip.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgVip.Columns[6].Width = 80;
            dgVip.Columns[6].DefaultCellStyle.Format = "0.0\\%";
            dgVip.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void UpdateFirstColumn1(DataGridView datagridNam)
        {
            // Here we add the row headers for 'START' tab.
            datagridNam.Rows[0].Cells[0].Value = "LAST MONTH'S UNCOMPLETED CASES";
            datagridNam.Rows[1].Cells[0].Value = "        ACTIVE ";
            datagridNam.Rows[2].Cells[0].Value = "        NONRESPONSE";
            datagridNam.Rows[3].Cells[0].Value = "LAST MONTH'S INITIALS";
            datagridNam.Rows[4].Cells[0].Value = "RE-ENTERING ABEYANCE CASES";
            datagridNam.Rows[5].Cells[0].Value = "TOTAL MONTHLY WORKLOAD";
            datagridNam.Rows[6].Cells[0].Value = "TOTAL ACTIVE MONTHLY WORKLOAD BY COLTEC";
            datagridNam.Rows[7].Cells[0].Value = "        FORM CASES";
            datagridNam.Rows[8].Cells[0].Value = "        PHONE CASES";
            datagridNam.Rows[9].Cells[0].Value = "        CENTURION CASES";
            datagridNam.Rows[10].Cells[0].Value = "        INTERNET CASES";
            datagridNam.Rows[11].Cells[0].Value = "        ADMINISTRATIVE CASES";
            datagridNam.Rows[12].Cells[0].Value = "        SPECIAL CASES";
            datagridNam.Rows[13].Cells[0].Value = "JVILLE PLANNED WORKLOAD";
        }

        private void UpdateFirstColumn2(DataGridView dataGridNum)
        {
            // Here we add the row headers for 'INITIAL' tab.
            dataGridNum.Rows[0].Cells[0].Value = "THIS MONTH'S INITIALS/CAPI";
            dataGridNum.Rows[1].Cells[0].Value = "       NOT PROCESSED";
            dataGridNum.Rows[2].Cells[0].Value = "       ASSIGNED";
            dataGridNum.Rows[3].Cells[0].Value = "       AWAITING REVIEW";
            dataGridNum.Rows[4].Cells[0].Value = "       FINISHED REVIEW";
            dataGridNum.Rows[5].Cells[0].Value = "       STATUS CHANGE FROM 1";
            dataGridNum.Rows[6].Cells[0].Value = "               INITIALS THAT HAVE BEEN REMOVED FROM THE WORKLOAD";
            dataGridNum.Rows[7].Cells[0].Value = "               INITIALS THAT HAVE BEEN REMOVED FROM THE ACTIVE WORKLOAD";
            dataGridNum.Rows[8].Cells[0].Value = "               INITIALS THAT HAVE BEEN SENT INTO ABEYANCE";
            dataGridNum.Rows[9].Cells[0].Value = "THIS MONTH'S ACTIVE INITIALS";
        }

        private void UpdateFirstColumn3(DataGridView dataGridname)
        {
            // Here we add the row headers for 'CURRENT' tab.
            dataGridname.Rows[0].Cells[0].Value = "JVILLE ACTUAL WORKLOAD BY COLLECTION METHOD";
            dataGridname.Rows[1].Cells[0].Value = "       NO DATA COLLECTED";
            dataGridname.Rows[2].Cells[0].Value = "               CALL ATTEMPT MADE";
            dataGridname.Rows[3].Cells[0].Value = "               NO CALL ATTEMPT MADE";
            dataGridname.Rows[4].Cells[0].Value = "       DATA COLLECTION via FORM";
            dataGridname.Rows[5].Cells[0].Value = "       DATA COLLECTION via CENTURION";
            dataGridname.Rows[6].Cells[0].Value = "       DATA COLLECTION via PHONE/TFU";
            dataGridname.Rows[7].Cells[0].Value = "       DATA COLLECTION via INTERNET";
            dataGridname.Rows[8].Cells[0].Value = "       DATA COLLECTION via ADMINISTRATIVE";
            dataGridname.Rows[9].Cells[0].Value = "       DATA COLLECTION via SPECIAL";
            dataGridname.Rows[10].Cells[0].Value = "PROJECTS REMOVED FROM WORKLOAD THIS MONTH";
            dataGridname.Rows[11].Cells[0].Value = "PROJECTS REMOVED FROM ACTIVE WORKLOAD";
            dataGridname.Rows[12].Cells[0].Value = "PROJECTS RECEIVING A COMPLETION DATE THIS MONTH";
            dataGridname.Rows[13].Cells[0].Value = "PROJECTS ADDED TO THE WORKLOAD";
            dataGridname.Rows[14].Cells[0].Value = "THIS MONTH'S UNCOMPLETED CASES";
        }

        private void GetMonthsList()
        {
            DateTime today = DateTime.Now;
            //current month
            CurrMonth = (GeneralFunctions.CurrentYearMon());
            //get the last 12 months 
            DateTime.Now.ToString("yyyyMM");
            currYearMon1 = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            currYearMon2 = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
            currYearMon3 = DateTime.Now.AddMonths(-3).ToString("yyyyMM");
            currYearMon4 = DateTime.Now.AddMonths(-4).ToString("yyyyMM");
            currYearMon5 = DateTime.Now.AddMonths(-5).ToString("yyyyMM");
            currYearMon6 = DateTime.Now.AddMonths(-6).ToString("yyyyMM");
            currYearMon7 = DateTime.Now.AddMonths(-7).ToString("yyyyMM");
            currYearMon8 = DateTime.Now.AddMonths(-8).ToString("yyyyMM");
            currYearMon9 = DateTime.Now.AddMonths(-9).ToString("yyyyMM");
            currYearMon10 = DateTime.Now.AddMonths(-10).ToString("yyyyMM");
            currYearMon11 = DateTime.Now.AddMonths(-11).ToString("yyyyMM");
            currYearMon12 = DateTime.Now.AddMonths(-12).ToString("yyyyMM");
        }

       
        private void setItemColumnHeader(DataGridView dataGridNum)
        {
            foreach (DataGridViewColumn dgvc in dataGridNum.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridNum.Columns[0].HeaderText = "                                      ";
            dataGridNum.Columns[0].Width = 350;
            dataGridNum.Columns[1].HeaderText = "ALL SURVEYS";
            dataGridNum.Columns[1].Width = 90;
            dataGridNum.Columns[1].DefaultCellStyle.Format = "N0";
            dataGridNum.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridNum.Columns[2].HeaderText = "STATE & LOCAL";
            dataGridNum.Columns[2].Width = 90;
            dataGridNum.Columns[2].DefaultCellStyle.Format = "N0";
            dataGridNum.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridNum.Columns[3].HeaderText = "FEDERAL";
            dataGridNum.Columns[3].Width = 80;
            dataGridNum.Columns[3].DefaultCellStyle.Format = "N0";
            dataGridNum.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridNum.Columns[4].HeaderText = "NONRESIDENTIAL";
            dataGridNum.Columns[4].Width = 110;
            dataGridNum.Columns[4].DefaultCellStyle.Format = "N0";
            dataGridNum.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridNum.Columns[5].HeaderText = "MULTIFAMILY";
            dataGridNum.Columns[5].Width = 90;
            dataGridNum.Columns[5].DefaultCellStyle.Format = "N0";
            dataGridNum.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridNum.Columns[6].HeaderText = "UTILITIES";
            dataGridNum.Columns[6].Width = 80;
            dataGridNum.Columns[6].DefaultCellStyle.Format = "N0";
            dataGridNum.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridNum.Rows[0].Selected = true;
        }

        private void PopulateTabStart(string SelMon)
        {
            dgt1.DataSource = dataObject.GetCaseHistPrevMon("1", cbStatp.Text);
            UpdateFirstColumn1(dgt1);
            setItemColumnHeader(dgt1);
        }

        private void PopulateTabInitial(string statp)
        {
            if (cbStatp.Text == CurrMonth)
            {
                dgt2.DataSource = dataObject.GetInitialtab();
                UpdateFirstColumn2(dgt2);
                setItemColumnHeader(dgt2);
            }
            else
            {
                dgt2.DataSource = dataObject.GetCaseHistPrevMon("2", cbStatp.Text);
                UpdateFirstColumn2(dgt2);
                setItemColumnHeader(dgt2);
            }
        }

        private void PopulateTabCurrent(string SelMon)
        {
            if (cbStatp.Text == CurrMonth)
            {
                dgt3.DataSource = dataObject.GetCurrenttab(cbStatp.Text);
                UpdateFirstColumn3(dgt3);
                setItemColumnHeader(dgt3);
            }
            else
            {
                dgt3.DataSource = dataObject.GetCaseHistPrevMon("3", cbStatp.Text);
                UpdateFirstColumn3(dgt3);
                setItemColumnHeader(dgt3);
            }
            buildViptable();
        }

        private void getCaseHist(string Selindex, string statp)
        {
            if (tabs.SelectedTab == tabPage1)
            {
                PopulateTabStart(cbStatp.Text);
            }
            if (tabs.SelectedTab == tabPage2)
            {
                PopulateTabInitial(cbStatp.Text);
            }
            if (tabs.SelectedTab == tabPage3)
            {
                PopulateTabCurrent(cbStatp.Text);
            }
        }

        ////Set the tab control names based on tab page selection.
        //private void selectTabPage(TabPage pageName)
        //{
        //    if (pageName == tabPage1)
        //    {
        //        dgNum = dgt1;
        //    }
        //    if (pageName == tabPage2)
        //    {
        //        dgNum = dgt2;
        //    }
        //    if (pageName == tabPage3)
        //    {
        //        dgNum = dgt3;
        //    }
        //}
        
        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormLoad();
        }

        Bitmap memoryImage;
        //Print the current selection for all 3 tabs
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

        //Print multiple datagrids based on selected month
        //private void PrintData(DataGridView dataGridNumb, string MonText)
        //{
        //    MonText = cbStatp.Text;
        //    Cursor.Current = Cursors.WaitCursor;
        //    dgPrint.DataSource = null;
        //    if (dataGridNumb == dgt1)
        //    {
        //        dgPrint.DataSource = dataObject.GetCaseHistPrevMon("1", cbStatp.Text);
        //        UpdateFirstColumn1(dgPrint);
        //        setItemColumnHeader(dgPrint);
        //    }
        //    if (dataGridNumb == dgt2)
        //    {
        //        if (cbStatp.Text == CurrMonth)
        //        {
        //            dgPrint.DataSource = dataObject.GetInitialtab();
        //            UpdateFirstColumn2(dgPrint);
        //            setItemColumnHeader(dgPrint);
        //        }
        //        else
        //        {
        //            dgPrint.DataSource = dataObject.GetCaseHistPrevMon("2", cbStatp.Text);
        //            UpdateFirstColumn2(dgPrint);
        //            setItemColumnHeader(dgPrint);
        //        }
        //    }
        //    if (dataGridNumb == dgt3)
        //    {
        //        if (cbStatp.Text == CurrMonth)
        //        {
        //            dgPrint.DataSource = dataObject.GetCurrenttab(cbStatp.Text);
        //            UpdateFirstColumn3(dgPrint);
        //            setItemColumnHeader(dgPrint);
        //        }
        //        else
        //        {
        //            dgPrint.DataSource = dataObject.GetCaseHistPrevMon("3", cbStatp.Text);
        //            UpdateFirstColumn3(dgPrint);
        //            setItemColumnHeader(dgPrint);
        //        }
        //    }
        //    DGVPrinter printer = new DGVPrinter();
        //    printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
        //    printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
        //    printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
        //    printer.SubTitleAlignment = StringAlignment.Near;
        //    printer.Title = "Case Management Report";
        //    if (dataGridNumb == dgt1)
        //    { 
        //        printer.SubTitle = "Start                                                                                           " + cbStatp.Text;
        //    }
        //    if (dataGridNumb == dgt2)
        //    {
        //        printer.SubTitle = "Initial                                                                                         " + cbStatp.Text;
        //    }
        //    if (dataGridNumb == dgt3)
        //    {
        //        printer.SubTitle = "Current                                                                                         " + cbStatp.Text;
        //    }
        //    printer.PageSettings.Landscape = true;
        //    printer.Userinfo = UserInfo.UserName;
        //    dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
        //    dgPrint.Columns[0].Width = 320;
        //    dgPrint.Columns[1].Width = 100;
        //    dgPrint.Columns[1].DefaultCellStyle.Format = "N0";
        //    dgPrint.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgPrint.Columns[2].Width = 100;
        //    dgPrint.Columns[2].DefaultCellStyle.Format = "N0";
        //    dgPrint.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgPrint.Columns[3].Width = 90;
        //    dgPrint.Columns[3].DefaultCellStyle.Format = "N0";
        //    dgPrint.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgPrint.Columns[4].Width = 120;
        //    dgPrint.Columns[4].DefaultCellStyle.Format = "N0";
        //    dgPrint.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgPrint.Columns[5].Width = 100;
        //    dgPrint.Columns[5].DefaultCellStyle.Format = "N0";
        //    dgPrint.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    dgPrint.Columns[6].Width = 90;
        //    dgPrint.Columns[6].DefaultCellStyle.Format = "N0";
        //    dgPrint.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        //    printer.PageNumbers = true;
        //    printer.PageNumberInHeader = true;
        //    printer.HeaderCellAlignment = StringAlignment.Near;
        //    printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
        //    printer.printDocument.DocumentName = "Case Management Report";
        //    printer.Footer = " ";
        //    printer.PrintDataGridViewWithoutDialog(dgPrint);
        //    dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        //    Cursor.Current = Cursors.Default;
        //}

        //when the month selected is changed recount the totals and default to Start tab
        private void cbStatp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loading)
                return;

            Cursor.Current = Cursors.WaitCursor;
            
            if (tabs.SelectedTab == tabPage1)
            {
                PopulateTabStart(cbStatp.Text);
            }
            if (tabs.SelectedTab == tabPage2)
            {
                PopulateTabInitial(cbStatp.Text);
            }
            if (tabs.SelectedTab == tabPage3)
            {
                PopulateTabCurrent(cbStatp.Text);
            }
            tabs.SelectedTab = tabPage1;

            Cursor.Current = Cursors.Default;

        }

        private void frmCaseManagementReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }
    }
}
