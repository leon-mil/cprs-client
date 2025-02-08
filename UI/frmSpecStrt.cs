/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmSpecStrt.cs	    	
Programmer:         Christine Zhang
Creation Date:      8/29/2018
Inputs:             None                                   
Parameters:	        None              
Outputs:	        None
Description:	    This program displays the annual compare strtdate and seldate data 
Detailed Design:    Detailed Design for Spec strtdate

Other:	            
Revision Referrals:	
***********************************************************************************
Modified Date :  12/16/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR 162
 Description   : fix the problems in excel file
***********************************************************************************
 Modified Date : 2/21/2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR885
 Description   : update excel file name from .xls to .xlsx
 **********************************************************************************/

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
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Globalization;

namespace Cprs
{
    public partial class frmSpecStrt : frmCprsParent
    {
        public frmSpecStrt()
        {
            InitializeComponent();
        }

        private int cur_year;
        private int cur_mon;
        private string saveFilename;
        private frmMessageWait waiting;
        private SpecStrtData dataObject;
        private string sdate;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private void frmTabSpecStrtdate_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();
            
            cur_mon = Convert.ToInt16(sdate.Substring(4,2));
            cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            if (cur_mon >=6)
            {
                cbYear.Items.Add(cur_year);
                cbYear.Items.Add(cur_year - 1);
                cbYear.Items.Add(cur_year - 2);
            }
            else
            {
                cbYear.Items.Add(cur_year-1);
                cbYear.Items.Add(cur_year - 2);
                cbYear.Items.Add(cur_year - 3);
            }
            cbYear.SelectedIndex = 0;

          //  LoadData();
        }

        private void LoadData()
        {
            string selyear = cbYear.Text;
            string selsurvey = string.Empty;
            string selvalue = string.Empty;

            string title2 = string.Empty;

            //Get selected survey
            if (rdAll.Checked)
            {
                selsurvey = "";
                title2 = "ALL SURVEYS - ";
            }
            else if (rdPrivate.Checked)
            {
                selsurvey = "N";
                title2 = "PRIVATE - ";
            }
            else if (rdState.Checked)
            {
                selsurvey = "P";
                title2 = "STATE AND LOCAL - ";
            }
            else
            {
                selsurvey = "F";
                title2 = "FEDERAL - ";
            }

            //Get selected value
            if (rdV1.Checked)
            {
                selvalue = "";
                title2 = title2 + "ALL VALUES";
            }
            else if (rdV2.Checked)
            {
                selvalue = "1";
                title2 = title2 + rdV2.Text;
            }
            else if (rdV3.Checked)
            {
                selvalue = "2";
                title2 = title2 + rdV3.Text;
            }
            else
            {
                selvalue = "3";
                title2 = title2 + rdV4.Text;
            }

            //Get current data
            dataObject = new SpecStrtData();
            DataTable strtdate_table = dataObject.GetSpecStrtDateData(selyear, selsurvey, selvalue);
            dgData.DataSource = strtdate_table;

            //set up range label
            int range_t = (Int32.Parse(strtdate_table.Rows[0][5].ToString()) + Int32.Parse(strtdate_table.Rows[0][7].ToString()) + Int32.Parse(strtdate_table.Rows[0][9].ToString()) + Int32.Parse(strtdate_table.Rows[0][11].ToString()) + Int32.Parse(strtdate_table.Rows[0][13].ToString()));
            int range_sum= Int32.Parse(strtdate_table.Rows[0][19].ToString());
            decimal range2 = ((decimal)range_t /(decimal) range_sum) * 100;
            int range = (int)Math.Round(range2, 0);
           
            label5.Text = "Within Dodge's Range " + range.ToString() + "%";
            label1.Text = title2;

            if (cbYear.Text == cur_year.ToString() || (cbYear.Text == (cur_year-1).ToString() && (cur_mon ==1)))
                lblTitle.Text = "COMPARISON OF DODGE'S START DATE vs CENSUS START DATE IN " + cbYear.Text + " (6 months)";
            else
                lblTitle.Text = "COMPARISON OF DODGE'S START DATE vs CENSUS START DATE IN " + cbYear.Text;

            //set up column header
            SetColumnHeader();
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 160;
            dgData.Columns[0].Frozen = true;
            dgData.Columns[1].HeaderText = "-4";
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DefaultCellStyle.NullValue = "0";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].HeaderText = "-4%";
            dgData.Columns[2].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[2].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].HeaderText = "-3";
            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.NullValue = "0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].HeaderText = "-3%";
            dgData.Columns[4].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[4].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = "-2";
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.NullValue = "0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].HeaderText = "-2%";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[6].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[7].HeaderText = "-1";
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.NullValue = "0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "-1%";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[8].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[9].HeaderText = "0";
            dgData.Columns[9].DefaultCellStyle.NullValue = "0";
            dgData.Columns[9].DefaultCellStyle.Format = "N0";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[10].HeaderText = "0%";
            dgData.Columns[10].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[10].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[11].HeaderText = "1";
            dgData.Columns[11].DefaultCellStyle.NullValue = "0";
            dgData.Columns[11].DefaultCellStyle.Format = "N0";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[12].HeaderText = "1%";
            dgData.Columns[12].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[12].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[13].HeaderText = "2";
            dgData.Columns[13].DefaultCellStyle.NullValue = "0";
            dgData.Columns[13].DefaultCellStyle.Format = "N0";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[14].HeaderText = "2%";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[14].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[14].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[15].HeaderText = "3";
            dgData.Columns[15].DefaultCellStyle.NullValue = "0";
            dgData.Columns[15].DefaultCellStyle.Format = "N0";
            dgData.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[16].HeaderText = "3%";
            dgData.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[16].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[16].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[17].HeaderText = "4";
            dgData.Columns[17].DefaultCellStyle.Format = "N0";
            dgData.Columns[17].DefaultCellStyle.NullValue = "0";
            dgData.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[18].HeaderText = "4%";
            dgData.Columns[18].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[18].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[18].DefaultCellStyle.NullValue = "0%";
            dgData.Columns[19].HeaderText = "All";
            dgData.Columns[19].DefaultCellStyle.Format = "N0";
            dgData.Columns[19].DefaultCellStyle.NullValue = "0";
            dgData.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgData.ClearSelection();
        }

        private void btnSummary_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSpecStrtSum frmSummary = new frmSpecStrtSum();
            frmSummary.CallingForm = this;
            frmSummary.ShowDialog();      
        }

        private void btnProject_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSpecStrtProj frmProject = new frmSpecStrtProj();
            frmProject.CallingForm = this;
            frmProject.ShowDialog();
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            rdAll.Checked = true;
            rdV1.Checked = true;
            LoadData();
        }

        private void rdPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPrivate.Checked)
                LoadData();
        }

        private void rdState_CheckedChanged(object sender, EventArgs e)
        {
            if (rdState.Checked)
                LoadData();
        }

        private void rdFederal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFederal.Checked)
                LoadData();
        }

        private void rdV1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdV1.Checked)
                LoadData();
        }

        private void rdV2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdV2.Checked)
                LoadData();
        }

        private void rdV3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdV3.Checked)
                LoadData();
        }

        private void rdV4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdV4.Checked)
                LoadData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.SubTitle = label1.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Special Strtdate";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (int a = 1; a < 20; a = a + 1)
            {
                dgData.Columns[a].Width = 40;
            }
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //release printer
            GeneralFunctions.releaseObject(printer);
            Cursor.Current = Cursors.Default;
        }

        private void frmSpecStrt_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "StartComp.xlsx";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //delete exist file
            //FileInfo fileInfo = new FileInfo(saveFilename);
            //string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            //if (GeneralFunctions.IsFileinUse(dir + "\\StartComp.xlsx"))
            //{
            //    MessageBox.Show(saveFilename + " is in use. Please close it.");
            //    return;
            //}


            //disable form
            this.Enabled = false;

            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }

        private void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\StartComp.xlsx";

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ExportToExcel2();
            ExportToExcel3();

            ////create sheet tables
            ExportToExcel1(cbYear.Items[2].ToString(), "3");
            ExportToExcel1(cbYear.Items[2].ToString(), "2");
            ExportToExcel1(cbYear.Items[2].ToString(), "1");
            ExportToExcel1(cbYear.Items[2].ToString(), "");

            ExportToExcel1(cbYear.Items[1].ToString(), "3");
            ExportToExcel1(cbYear.Items[1].ToString(), "2");
            ExportToExcel1(cbYear.Items[1].ToString(), "1");
            ExportToExcel1(cbYear.Items[1].ToString(), "");   

            ExportToExcel1(cbYear.Items[0].ToString(), "3");
            ExportToExcel1(cbYear.Items[0].ToString(), "2");  
            ExportToExcel1(cbYear.Items[0].ToString(), "1");
            ExportToExcel1(cbYear.Items[0].ToString(), "");

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            //Release objects
            GeneralFunctions.releaseObject(xlWorkSheet);
            GeneralFunctions.releaseObject(xlWorkBook);
            GeneralFunctions.releaseObject(xlApp);

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("File has been created");
        }

        private void ExportToExcel1(string selyear, string value_group)
        {
            string stitle = string.Empty;
            if ((selyear == cur_year.ToString() && (cur_mon >=6) )|| (selyear == (cur_year - 1).ToString() && (cur_mon == 1)))
                stitle = "Comparison of Dodge's Start Date vs Census Start Date in " + selyear + " (6 months)";
            else
                stitle = "Comparison of Dodge's Start Date vs Census Start Date in " + selyear;
           
            string subtitle= "";
            string tabname ="";
            if (value_group == "")
            {
                subtitle = "All Values";
                tabname = selyear + " - All Value Group";
            }
            else if (value_group == "1")
            {
                subtitle = "Projects Value $5 Million or More";
                tabname = selyear + " - Value Group1";
            }
            else if (value_group == "2")
            {
                subtitle = "Projects Value $750,000 to $4,999,000";
                tabname = selyear + " - Value Group2";
            }
            else
            {
                subtitle = "Projects Value less than $750,000";
                tabname = selyear + " - Value Group3";
            }

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = tabname;

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            xlWorkSheet.Activate();
            xlWorkSheet.Application.ActiveWindow.SplitColumn = 20;
            xlWorkSheet.Application.ActiveWindow.SplitRow = 5;
            xlWorkSheet.Application.ActiveWindow.FreezePanes = true;
            
            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;
            
            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "T"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 16; 

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "T"]);
            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Bold = true;
            titleRange2.Font.Size = 12;
            titleRange2.RowHeight = 16;

          
            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[4, 1] = "           \n";
            xlWorkSheet.Cells[4, 2] = "Census start date\n is four months or \n more prior to the \n Dodge start date";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "B"], xlWorkSheet.Cells[4, "C"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);
            cellRange.RowHeight = 50;

            xlWorkSheet.Cells[4, 4] = "Census start date\n is three months \n prior to the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "D"], xlWorkSheet.Cells[4, "E"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 6] = "Census start date\n is two months \n prior to the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "F"], xlWorkSheet.Cells[4, "G"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 8] = "Census start date\n is one month \n prior to the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, "I"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 10] = "Census start date\n and Dodge start \n date are the same";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "J"], xlWorkSheet.Cells[4, "K"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 12] = "Census start date\n is one month \n after the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "L"], xlWorkSheet.Cells[4, "M"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 14] = "Census start date\n is two months \n after the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "N"], xlWorkSheet.Cells[4, "O"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 16] = "Census start date\n is three months \n after the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "P"], xlWorkSheet.Cells[4, "Q"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, 18] = "Census start date\n is four months \n after the \n Dodge start date";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "R"], xlWorkSheet.Cells[4, "S"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[4, "T"] = "All Cases";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "T"], xlWorkSheet.Cells[4, "T"]);
            cellRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignTop;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[5, 2] = "Obs";
            xlWorkSheet.Cells[5, 3] = "%";
            xlWorkSheet.Cells[5, 4] = "Obs";
            xlWorkSheet.Cells[5, 5] = "%";
            xlWorkSheet.Cells[5, 6] = "Obs";
            xlWorkSheet.Cells[5, 7] = "%";
            xlWorkSheet.Cells[5, 8] = "Obs";
            xlWorkSheet.Cells[5, 9] = "%";
            xlWorkSheet.Cells[5, 10] = "Obs";
            xlWorkSheet.Cells[5, 11] = "%";
            xlWorkSheet.Cells[5, 12] = "Obs";
            xlWorkSheet.Cells[5, 13] = "%";
            xlWorkSheet.Cells[5, 14] = "Obs";
            xlWorkSheet.Cells[5, 15] = "%";
            xlWorkSheet.Cells[5, 16] = "Obs";
            xlWorkSheet.Cells[5, 17] = "%";
            xlWorkSheet.Cells[5, 18] = "Obs";
            xlWorkSheet.Cells[5, 19] = "%";
           
            xlWorkSheet.Cells[5, 20] = " ";

            /*for ALL */
            DataTable dt = dataObject.GetSpecStrtDateData(selyear, "", value_group);
            int range_t = (Int32.Parse(dt.Rows[0][5].ToString()) + Int32.Parse(dt.Rows[0][7].ToString()) + Int32.Parse(dt.Rows[0][9].ToString()) + Int32.Parse(dt.Rows[0][11].ToString()) + Int32.Parse(dt.Rows[0][13].ToString()));
            int range_sum = Int32.Parse(dt.Rows[0][19].ToString());
            decimal range2 = ((decimal)range_t / (decimal)range_sum) * 100;
            int range = (int)Math.Round(range2, 0);
           // int range = Int32.Parse(dt.Rows[0][6].ToString()) + Int32.Parse(dt.Rows[0][8].ToString()) + Int32.Parse(dt.Rows[0][10].ToString()) + Int32.Parse(dt.Rows[0][12].ToString()) + Int32.Parse(dt.Rows[0][14].ToString());

            //Setup the column header row (row 5)
            xlWorkSheet.Cells[6, 1] = "All ";
           
            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T"};
            foreach (string s in strColumns)
            {
                if (s != "A") 
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8;
                else
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 20;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            }

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, "A"], xlWorkSheet.Cells[6, "A"]);
            cellRange.Font.Bold = true;

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            Microsoft.Office.Interop.Excel.Range borderRange = xlApp.get_Range(xlWorkSheet.Cells[8, "F"], xlWorkSheet.Cells[24, "G"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[8, "H"], xlWorkSheet.Cells[24, "I"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[8, "J"], xlWorkSheet.Cells[24, "K"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[8, "L"], xlWorkSheet.Cells[24, "M"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[8, "N"], xlWorkSheet.Cells[24, "O"]);
            Drawbox(borderRange);

            //add an frozen panel
           // Microsoft.Office.Interop.Excel.Range freezeRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[5, "T"]);
           // freezeRange.Application.ActiveWindow.FreezePanes = true;

            //Populate rest of the data. Start at row[6] 
            int iRow = 7; //We start at row 6
           
            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    //xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                    if (iCol == 3 || iCol == 5 || iCol == 7 || iCol == 9 || iCol == 11 || iCol == 13 || iCol == 15 || iCol == 17 || iCol == 19)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString() + '%';
                    else
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();

                }

                if (iRow == 8 || iRow == 10 || iRow == 12 || iRow == 14 || iRow == 16 || iRow == 18 || iRow == 20 || iRow == 22 || iRow == 24)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "T"]);
                    cellRange.Interior.Color = Color.LightSkyBlue;
                } 

            }
            xlWorkSheet.Cells[25, 6] = range.ToString()+'%';
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[25, "F"], xlWorkSheet.Cells[25, "O"]);
            cellRange.Font.Bold = true;
            cellRange.Merge(Type.Missing);
            cellRange.Interior.Color = Color.Yellow;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            DrawFullbox(cellRange);

            /*****for Private *******/
            dt = dataObject.GetSpecStrtDateData(selyear, "N", value_group);
            range_t = (Int32.Parse(dt.Rows[0][5].ToString()) + Int32.Parse(dt.Rows[0][7].ToString()) + Int32.Parse(dt.Rows[0][9].ToString()) + Int32.Parse(dt.Rows[0][11].ToString()) + Int32.Parse(dt.Rows[0][13].ToString()));
            range_sum = Int32.Parse(dt.Rows[0][19].ToString());
            range2 = ((decimal)range_t / (decimal)range_sum) * 100;
            range = (int)Math.Round(range2, 0);
            //range = Int32.Parse(dt.Rows[0][6].ToString()) + Int32.Parse(dt.Rows[0][8].ToString()) + Int32.Parse(dt.Rows[0][10].ToString()) + Int32.Parse(dt.Rows[0][12].ToString()) + Int32.Parse(dt.Rows[0][14].ToString());

            //Setup the column header row (row 5)
            xlWorkSheet.Cells[27, 1] = "Private";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[27, "A"], xlWorkSheet.Cells[27, "T"]);
            cellRange.Font.Bold = true;

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            iRow = 28; //We start at row 6

            iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 3 || iCol == 5 || iCol == 7 || iCol == 9 || iCol == 11 || iCol == 13 || iCol == 15 || iCol == 17 || iCol == 19)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString() + '%';
                    else
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }

                if (iRow == 29 || iRow == 31 || iRow == 33 || iRow == 35 || iRow == 37 || iRow == 39 || iRow == 41 || iRow == 43 || iRow == 45)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "T"]);
                    cellRange.Interior.Color = Color.LightSkyBlue;
                }

            }
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[29, "F"], xlWorkSheet.Cells[45, "G"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[29, "H"], xlWorkSheet.Cells[45, "I"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[29, "J"], xlWorkSheet.Cells[45, "K"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[29, "L"], xlWorkSheet.Cells[45, "M"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[29, "N"], xlWorkSheet.Cells[45, "O"]);
            Drawbox(borderRange);

            xlWorkSheet.Cells[46, 6] = range.ToString() + '%';
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[46, "F"], xlWorkSheet.Cells[46, "O"]);
            cellRange.Font.Bold = true;
            cellRange.Merge(Type.Missing);
            cellRange.Interior.Color = Color.Yellow;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            DrawFullbox(cellRange);

            /*****for State and Local *******/
            dt = dataObject.GetSpecStrtDateData(selyear, "P", value_group);
            range_t = (Int32.Parse(dt.Rows[0][5].ToString()) + Int32.Parse(dt.Rows[0][7].ToString()) + Int32.Parse(dt.Rows[0][9].ToString()) + Int32.Parse(dt.Rows[0][11].ToString()) + Int32.Parse(dt.Rows[0][13].ToString()));
            range_sum = Int32.Parse(dt.Rows[0][19].ToString());
            range2 = ((decimal)range_t / (decimal)range_sum) * 100;
            range = (int)Math.Round(range2, 0);
            // range = Int32.Parse(dt.Rows[0][6].ToString()) + Int32.Parse(dt.Rows[0][8].ToString()) + Int32.Parse(dt.Rows[0][10].ToString()) + Int32.Parse(dt.Rows[0][12].ToString()) + Int32.Parse(dt.Rows[0][14].ToString());

            //Setup the column header row (row 5)
            xlWorkSheet.Cells[48, 1] = "State and Local";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[48, "A"], xlWorkSheet.Cells[48, "T"]);
            cellRange.Font.Bold = true;

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            iRow = 49; //We start at row 6

            iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 3 || iCol == 5 || iCol == 7 || iCol == 9 || iCol == 11 || iCol == 13 || iCol == 15 || iCol == 17 || iCol == 19)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString() + '%';
                    else
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }

                if (iRow == 50 || iRow == 52 || iRow == 54 || iRow == 56 || iRow == 58 || iRow == 60 || iRow == 62 || iRow == 64 || iRow == 66)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "T"]);
                    cellRange.Interior.Color = Color.LightSkyBlue;
                }

            }
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[50, "F"], xlWorkSheet.Cells[66, "G"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[50, "H"], xlWorkSheet.Cells[66, "I"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[50, "J"], xlWorkSheet.Cells[66, "K"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[50, "L"], xlWorkSheet.Cells[66, "M"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[50, "N"], xlWorkSheet.Cells[66, "O"]);
            Drawbox(borderRange);

            xlWorkSheet.Cells[67, 6] = range.ToString() + '%';
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[67, "F"], xlWorkSheet.Cells[67, "O"]);
            cellRange.Font.Bold = true;
            cellRange.Merge(Type.Missing);
            cellRange.Interior.Color = Color.Yellow;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            DrawFullbox(cellRange);

            dt = dataObject.GetSpecStrtDateData(selyear, "F", value_group);
            range_t = (Int32.Parse(dt.Rows[0][5].ToString()) + Int32.Parse(dt.Rows[0][7].ToString()) + Int32.Parse(dt.Rows[0][9].ToString()) + Int32.Parse(dt.Rows[0][11].ToString()) + Int32.Parse(dt.Rows[0][13].ToString()));
            range_sum = Int32.Parse(dt.Rows[0][19].ToString());
            range2 = ((decimal)range_t / (decimal)range_sum) * 100;
            range = (int)Math.Round(range2, 0);
           // range = Int32.Parse(dt.Rows[0][6].ToString()) + Int32.Parse(dt.Rows[0][8].ToString()) + Int32.Parse(dt.Rows[0][10].ToString()) + Int32.Parse(dt.Rows[0][12].ToString()) + Int32.Parse(dt.Rows[0][14].ToString());

            //Setup the column header row (row 5)
            xlWorkSheet.Cells[69, 1] = "Federal";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[69, "A"], xlWorkSheet.Cells[69, "T"]);
            cellRange.Font.Bold = true;

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            iRow = 70; //We start at row 6

            iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 3 || iCol == 5 || iCol == 7 || iCol == 9 || iCol == 11 || iCol == 13 || iCol == 15 || iCol == 17 || iCol == 19)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString() + '%';
                    else
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }

                if (iRow == 71 || iRow == 73 || iRow == 75 || iRow == 77 || iRow == 79 || iRow == 81 || iRow == 83 || iRow == 85 || iRow == 87)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "T"]);
                    cellRange.Interior.Color = Color.LightSkyBlue;
                }
            }
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[71, "F"], xlWorkSheet.Cells[87, "G"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[71, "H"], xlWorkSheet.Cells[87, "I"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[71, "J"], xlWorkSheet.Cells[87, "K"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[71, "L"], xlWorkSheet.Cells[87, "M"]);
            Drawbox(borderRange);
            borderRange = xlApp.get_Range(xlWorkSheet.Cells[71, "N"], xlWorkSheet.Cells[87, "O"]);
            Drawbox(borderRange);

            xlWorkSheet.Cells[88, 6] = range.ToString() + '%';
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[88, "F"], xlWorkSheet.Cells[88, "O"]);
            cellRange.Font.Bold = true;
            cellRange.Merge(Type.Missing);
            cellRange.Interior.Color = Color.Yellow;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            DrawFullbox(cellRange);

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 40;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 40;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

        }

        private void ExportToExcel2()
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "Late & Abeyance";

            xlWorkSheet.Rows.Font.Size = 9;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            string stitle = string.Empty;
            
            stitle = (cur_year-2).ToString() + " - " + cur_year.ToString() + " Late versus Abeyance";

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "D"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 16;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            /*for ALL */
            DataTable dt = dataObject.GetSpecStrtDateSumData( "", "");
            addSumTable(xlWorkSheet, dt, 3, "All Surveys - All Values");

            /*for Private all value */
            dt = dataObject.GetSpecStrtDateSumData("N", "");
            addSumTable(xlWorkSheet, dt, 9, "Private Survey - All Values");

            /*for State and Local all value */
            dt = dataObject.GetSpecStrtDateSumData("P", "");
            addSumTable(xlWorkSheet, dt, 15, "State and Local Survey - All Values");

            /*for Federal all value */
            dt = dataObject.GetSpecStrtDateSumData("F", "");
            addSumTable(xlWorkSheet, dt, 21, "Federal Survey - All Values");

            /*for ALL */
            dt = dataObject.GetSpecStrtDateSumData("", "1");
            addSumTable(xlWorkSheet, dt, 27, "All Surveys - $5 Million or more");

            /*for Private all value */
            dt = dataObject.GetSpecStrtDateSumData("N", "1");
            addSumTable(xlWorkSheet, dt, 33, "Private Survey - $5 Million or more");

            /*for State and Local all value */
            dt = dataObject.GetSpecStrtDateSumData("P", "1");
            addSumTable(xlWorkSheet, dt, 39, "State and Local Survey - $5 Million or more");

            /*for Federal all value */
            dt = dataObject.GetSpecStrtDateSumData("F", "1");
            addSumTable(xlWorkSheet, dt, 45, "Federal Survey - $5 Million or more");

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 40;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 40;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void addSumTable(Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet1, DataTable dt, int line_start, string title)
        {
            xlWorkSheet1.Cells[line_start, 1] = title;
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, "A"], xlWorkSheet.Cells[line_start, "A"]);
            cellRange.Font.Bold = true;

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            xlWorkSheet.Cells[line_start+1, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 1], xlWorkSheet.Cells[line_start + 1, 1]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            xlWorkSheet.Cells[line_start + 1, 2] = dt.Columns[1].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 2], xlWorkSheet.Cells[line_start + 1, 2]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 1, 3] = dt.Columns[2].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 3], xlWorkSheet.Cells[line_start + 1, 3]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 1, 4] = dt.Columns[3].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 4], xlWorkSheet.Cells[line_start + 1, 4]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            cellRange.Font.Bold = true;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D" };
            foreach (string s in strColumns)
            {
                if (s != "A")
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 20;
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 60;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).Font.Bold = true;
                }
            }

            int iRow = line_start + 1; //We start at row 6

            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                    {
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        DrawFullbox(cellRange);
                    }
                    else
                    {
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString() + "%";
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                        DrawFullbox(cellRange);
                    }
                }
            }
        }

        private void ExportToExcel3()
        {
            string start_date = string.Empty;
            string end_date = string.Empty;

            if (cur_mon >= 5)
            {
                start_date = (cur_year - 2).ToString() + "01";
                end_date = cur_year.ToString() + "06";
            }
            else if (cur_mon <=1)
            {
                start_date = (cur_year - 3).ToString() + "01";
                end_date = (cur_year-1).ToString() + "06";
            }
            else
            {
                start_date = (cur_year - 3).ToString() + "01";
                end_date = (cur_year - 1).ToString() + "12";
            }

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "Projects over $100M";

            xlWorkSheet.Rows.Font.Size = 9;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            /*for ALL */
            DataTable dt = dataObject.GetSpecStrtDateProjData("");
            Decimal range = Decimal.Parse(dt.Rows[9][3].ToString()) + Decimal.Parse(dt.Rows[9][4].ToString()) + Decimal.Parse(dt.Rows[9][5].ToString()) + Decimal.Parse(dt.Rows[9][6].ToString()) + Decimal.Parse(dt.Rows[9][7].ToString());
            string label = Math.Round(range, 0).ToString() + "%";
            addProjTable(xlWorkSheet, dt, 1, "Projects Valued over $100 M selected from " + start_date + "-" + end_date, label);

            dt = dataObject.GetSpecStrtDateProjData(end_date.Substring(0, 4));
            range = Decimal.Parse(dt.Rows[9][3].ToString()) + Decimal.Parse(dt.Rows[9][4].ToString()) + Decimal.Parse(dt.Rows[9][5].ToString()) + Decimal.Parse(dt.Rows[9][6].ToString()) + Decimal.Parse(dt.Rows[9][7].ToString());
            label = Math.Round(range, 0).ToString() + "%";
            addProjTable(xlWorkSheet, dt, 16, "Projects Valued over $100 M selected from " + end_date.Substring(0, 4) + "01" + "-" + end_date, label);

            int y = Convert.ToInt16(end_date.Substring(0, 4)) - 1;
            dt = dataObject.GetSpecStrtDateProjData(y.ToString());
            range = Decimal.Parse(dt.Rows[9][3].ToString()) + Decimal.Parse(dt.Rows[9][4].ToString()) + Decimal.Parse(dt.Rows[9][5].ToString()) + Decimal.Parse(dt.Rows[9][6].ToString()) + Decimal.Parse(dt.Rows[9][7].ToString());
            label = Math.Round(range, 0).ToString() + "%";
            addProjTable(xlWorkSheet, dt, 31, "Projects Valued over $100 M selected from " + y.ToString() + "01" + "-" + y.ToString() + "12", label);

            dt = dataObject.GetSpecStrtDateProjData(start_date.Substring(0, 4));
            range = Decimal.Parse(dt.Rows[9][3].ToString()) + Decimal.Parse(dt.Rows[9][4].ToString()) + Decimal.Parse(dt.Rows[9][5].ToString()) + Decimal.Parse(dt.Rows[9][6].ToString()) + Decimal.Parse(dt.Rows[9][7].ToString());
            label = Math.Round(range, 0).ToString() + "%";
            addProjTable(xlWorkSheet, dt, 46, "Projects Valued over $100 M selected from " + start_date + "-" + start_date.Substring(0, 4) + "12", label);

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 40;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 40;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void addProjTable(Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet1, DataTable dt, int line_start, string title, string label)
        {
            xlWorkSheet1.Cells[line_start, 1] = title;
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, "A"], xlWorkSheet.Cells[line_start, "K"]);
            cellRange.Font.Bold = true;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            cellRange.Merge(Type.Missing);

            xlWorkSheet.Cells[line_start + 1, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 1], xlWorkSheet.Cells[line_start + 1, 1]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            xlWorkSheet.Cells[line_start + 1, 2] = "Late";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 2], xlWorkSheet.Cells[line_start + 1, 3]);
            cellRange.Merge(Type.Missing);
            cellRange.Font.Bold = true;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            xlWorkSheet.Cells[line_start + 1, 4] = "Within Dodge's Range";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 4], xlWorkSheet.Cells[line_start + 1, 8]);
            cellRange.Merge(Type.Missing);
            cellRange.Font.Bold = true;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            xlWorkSheet.Cells[line_start + 1, 9] = "Early";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 9], xlWorkSheet.Cells[line_start + 1, 10]);
            cellRange.Merge(Type.Missing);
            cellRange.Font.Bold = true;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange);
            xlWorkSheet.Cells[line_start + 1, 11] = "";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 11], xlWorkSheet.Cells[line_start + 1, 11]);
            Drawbox(cellRange);

            xlWorkSheet.Cells[line_start + 2, "A"] = dt.Columns[0].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "A"], xlWorkSheet.Cells[line_start + 2, "A"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "B"] = dt.Columns[1].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "B"], xlWorkSheet.Cells[line_start + 2, "B"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "C"] = dt.Columns[2].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "C"], xlWorkSheet.Cells[line_start + 2, "C"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "D"] = dt.Columns[3].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "D"], xlWorkSheet.Cells[line_start + 2, "D"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "E"] = dt.Columns[4].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "E"], xlWorkSheet.Cells[line_start + 2, "E"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "F"] = dt.Columns[5].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "F"], xlWorkSheet.Cells[line_start + 2, "F"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "G"] = dt.Columns[6].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "G"], xlWorkSheet.Cells[line_start + 2, "G"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "H"] = dt.Columns[7].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "H"], xlWorkSheet.Cells[line_start + 2, "H"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "I"] = dt.Columns[8].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "I"], xlWorkSheet.Cells[line_start + 2, "I"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "J"] = dt.Columns[9].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "J"], xlWorkSheet.Cells[line_start + 2, "J"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 2, "K"] = dt.Columns[10].Caption;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 2, "K"], xlWorkSheet.Cells[line_start + 2, "K"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            DrawFullbox(cellRange);
            cellRange.Font.Bold = true;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "K" };
            foreach (string s in strColumns)
            {
                if (s != "A")
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 10;
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 15;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).Font.Bold = true;
                }
            }

            int iRow = line_start + 2; //We start at row 6

            int iCol = 0;
            int row2 = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;
                row2++;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                    {
                        string colval = r[c.ColumnName].ToString();
                        xlWorkSheet.Cells[iRow, iCol] = colval;
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    }
                    else
                    {
                        if (row2 == 2)
                        {
                            cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                            cellRange.NumberFormat = "@";
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString(); 
                        }
                        else
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight; 
                    }
                    if (row2 == 2)
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow-1, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        DrawFullbox(cellRange);
                    }
                }

                if (row2 == 2)
                    row2 = 0;
            }

            //percentage line
            xlWorkSheet.Cells[line_start + 13, "D"] = label;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 13, "D"], xlWorkSheet.Cells[line_start + 13, "H"]);
            cellRange.Merge(Type.Missing);
            cellRange.Interior.Color = Color.Yellow;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            DrawFullbox(cellRange);
        }


        private void Drawbox(Microsoft.Office.Interop.Excel.Range borderRange)
        {
            borderRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            Microsoft.Office.Interop.Excel.Borders border = borderRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
        }
        private void DrawFullbox(Microsoft.Office.Interop.Excel.Range borderRange)
        {
            borderRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            Microsoft.Office.Interop.Excel.Borders border = borderRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            dgData.CurrentCell = null;
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAll.Checked)
                LoadData();
        }
    }
}
