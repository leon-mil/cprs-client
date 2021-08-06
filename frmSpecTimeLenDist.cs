/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecTimeLenDist.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 5/8/2019
Inputs          : None                 
Parameters      : selected survey, calling form
Outputs         : None	
Description     : Display Special Time len distribution
Detailed Design : Detailed Design for Time length distribution
Other           :	            
Revision History:	
**************************************************************************************************
Modified Date   :  8/4/2021
Modified By     :  Christine
Keyword         :  
Change Request  :  8421
Description     :  correct title and column name in excel file
**************************************************************************************************/
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
using System.IO;
using System.Collections;
using DGVPrinterHelper;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Globalization;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmSpecTimeLenDist : frmCprsParent
    {
        public frmSpecTimeLenDist()
        {
            InitializeComponent();
        }
        public frmSpecTimeLen CallingForm = null;
        public string SelectedSurvey;

        private SpecTimelenData data_object;
        private DataTable dtmonth = new DataTable();
        private DataTable dtcum = new DataTable();
        private string saveFilename;
        private frmMessageWait waiting;
        private bool data_loading;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private bool call_callingFrom = false;
        private void frmSpecTimeLenDist_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");
            if (SelectedSurvey == "P")
                lblTitle.Text = "STATE AND LOCAL";
            else if (SelectedSurvey == "N")
                lblTitle.Text = "NONRESIDENTIAL";
            else
                lblTitle.Text = "MULTIFAMILY";

            int year = DateTime.Now.Year;
            label2.Text = "Project Completed in " + (year - 2).ToString() + "01-" + (year - 1).ToString() + "12";

            GetData();
        }

        private void GetData()
        {
            data_loading = true;

            data_object = new SpecTimelenData();
            dtmonth = data_object.GetTimeLenDataDistribution(SelectedSurvey);
            if (dtmonth.Rows.Count > 0)
            {
                dtmonth.Rows[0][0] = "mn start";
                dtmonth.Rows[dtmonth.Rows.Count-1][0] = "48 + mn";
            }

            dtcum = dtmonth.Clone();
            decimal c1 = 0;
            decimal c2 = 0;
            decimal c3 = 0;
            decimal c4 = 0;
            decimal c5 = 0;
            decimal c6 = 0;
            decimal c0 = 0;

            foreach (DataRow row in dtmonth.Rows)
            {
                DataRow dr = dtcum.NewRow();
                c1 = c1 + Convert.ToDecimal(row["c1"]);
                c2 = c2 + Convert.ToDecimal(row["c2"]);
                c3 = c3 + Convert.ToDecimal(row["c3"]);
                c4 = c4 + Convert.ToDecimal(row["c4"]);
                if (SelectedSurvey != "M")
                {
                    c5 = c5 + Convert.ToDecimal(row["c5"]);
                    c6 = c6 + Convert.ToDecimal(row["c6"]);
                }
                c0 = c0 + Convert.ToDecimal(row["c0"]);
                dr[0] = row[0].ToString();
                dr[1] = c1;
                dr[2] = c2;
                dr[3] = c3;
                dr[4] = c4;
                if (SelectedSurvey != "M")
                {
                    dr[5] = c5;
                    dr[6] = c6;
                    dr[7] = c0;
                }
                else
                    dr[5] = c0;
                dtcum.Rows.Add(dr);
            }

            dgData.DataSource = null;
            dgData.DataSource = dtmonth;
            SetColumnHeader();

            data_loading = false;     
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgData.Columns[0].HeaderText = "Month";
            dgData.Columns[0].Width = 100;

            if (SelectedSurvey == "M")
            {
                dgData.Columns[1].HeaderText = "<3000";
                dgData.Columns[1].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = "3000 to 4999";
                dgData.Columns[2].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = "5000 to 9999";
                dgData.Columns[3].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = ">=10000";
                dgData.Columns[4].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = "All";
                dgData.Columns[5].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            {
                dgData.Columns[1].HeaderText = "<250";
                dgData.Columns[1].DefaultCellStyle.Format = "0.0\\%"; 
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = "250 to 999";
                dgData.Columns[2].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = "1000 to 2999";
                dgData.Columns[3].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = "3000 to 4999";
                dgData.Columns[4].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = "5000 to 9999";
                dgData.Columns[5].DefaultCellStyle.Format = "0.0\\%"; 
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = ">=10000";
                dgData.Columns[6].DefaultCellStyle.Format = "0.0\\%"; 
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].HeaderText = "All";
                dgData.Columns[7].DefaultCellStyle.Format = "0.0\\%";
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void frmSpecTimeLenDist_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }
            this.Close();
        }

        private void btnCases_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSpecTimeLenDistCases popup = new frmSpecTimeLenDistCases();
            popup.CallingForm = this;
            popup.SelectedSurvey = SelectedSurvey;
            popup.Show();
        }

        private void rdbValue_CheckedChanged(object sender, EventArgs e)
        {
            if (!data_loading)
            {
                dgData.DataSource = null;
                dgData.DataSource = dtcum;
                SetColumnHeader();
            }
        }

        private void rdbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (!data_loading)
            {
                dgData.DataSource = null;
                dgData.DataSource = dtmonth;
                SetColumnHeader();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            if (rdbMonth.Checked)
                printer.Title =  lblTitle.Text + " " + rdbMonth.Text + " " + lbltitle2.Text;
            else
                printer.Title =  lblTitle.Text + " " + rdbValue.Text + " " + lbltitle2.Text;

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = label2.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Time Length Distribution";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgData.Columns[0].Width = 100;
            int num_cols = 7;
            if (SelectedSurvey == "M")
                num_cols = 5;
            for (int i = 1; i < num_cols; i++)
            {
                dgData.Columns[i].Width = 100;
            }

            printer.PrintDataGridViewWithoutDialog(dgData);

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Cursor.Current = Cursors.Default;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            bool cv_exist = data_object.CheckLotExist("LOTPDSES");

            if (!cv_exist)
            {
                MessageBox.Show("Table cannot be created, variances need to be run.");
                return;
            }

            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";
            string year = (DateTime.Now.Year - 1).ToString().Substring(2);
            if (SelectedSurvey == "P")
                saveFileDialog1.FileName = "t5" + year + ".xls";
            else if (SelectedSurvey == "N")
                saveFileDialog1.FileName = "t4" + year + ".xls";
            else
                saveFileDialog1.FileName = "t6" + year + ".xls";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //delete exist file
            FileInfo fileInfo = new FileInfo(saveFilename);

            //disable form
            this.Enabled = false;

            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = string.Empty;
            string sfilename2 = string.Empty;
            string year = (DateTime.Now.Year - 1).ToString().Substring(2);

            if (SelectedSurvey == "P")
            {
                sfilename = dir + "\\t5" + year + ".xls";
                sfilename2 = dir + "\\t5h" + year + ".xls";
            }
            else if (SelectedSurvey == "N")
                sfilename = dir + "\\t4" + year + ".xls";
            else
                sfilename = dir + "\\t6" + year + ".xls";
          
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            // Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            //create sheet tables
            ExportSEToExcel(SelectedSurvey);
            if (SelectedSurvey == "P" || SelectedSurvey == "N")
                ExportToExcel(SelectedSurvey);
            else
                ExportMToExcel();

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            if (SelectedSurvey == "P")
            {
                //Initialize variables
                xlApp = new Microsoft.Office.Interop.Excel.Application();
                xlWorkBook = xlApp.Workbooks.Add(misValue);

                ExportHighwaySEToExcel();
                ExportHighwayToExcel();

                // Save file & Quit application
                xlApp.DisplayAlerts = false; //Supress overwrite request
                xlWorkBook.SaveAs(sfilename2, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
            }

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

        private void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }

        private void ExportToExcel(string survey)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 7.5;
            xlWorkSheet.Rows.Font.Name = "Arial";
            
            int year = DateTime.Now.Year;
            string title1 = string.Empty;
            string title2 = string.Empty;
           
            if (survey == "N")
            {
                title1 = "Table 4. Percent Distribution of Value Put in Place Each Month from Start to Completion for Private Nonresidential";
                title2 = "Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
                xlWorkSheet.Name = "T4";
            }
            else if (survey == "P")
            {
                title1 = "Table 5. Percent Distribution of Value Put in Place Each Month from Start to Completion for State and Local";
                title2 = "Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
                xlWorkSheet.Name = "T5";
            }
            
            Microsoft.Office.Interop.Excel.Range cellRange;
           
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "O"]);

            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            cellRange.Font.Size = 9;
            cellRange.RowHeight = 10;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "O"]);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Font.Size = 9;
            cellRange.RowHeight = 10;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "O"]);
            xlWorkSheet.Cells[4, 1] = "(Details may not add to totals due to rounding.)";
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build table
            xlWorkSheet.Cells[5, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[5, 2] = "Value of Project (Thousands of dollars)";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 15]);
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 1, 1, 1, 1);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[6, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 1], xlWorkSheet.Cells[6, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 2] = "$10,000 or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 2], xlWorkSheet.Cells[6, 3]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 4] = "$5,000 - $9,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 4], xlWorkSheet.Cells[6, 5]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 6] = "$3,000 - $4,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 6], xlWorkSheet.Cells[6, 7]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 8] = "$1,000 - $2,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 8], xlWorkSheet.Cells[6, 9]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 10] = "$250 - $999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 10], xlWorkSheet.Cells[6, 11]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 12] = "Less than $250";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 12], xlWorkSheet.Cells[6, 13]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 14] = "All Values";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 14], xlWorkSheet.Cells[6, 15]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[7, 1] = "Month work put in place ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, 1], xlWorkSheet.Cells[7, 1]);
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).ColumnWidth = 18;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            Drawbox(cellRange, 1, 1, 1, 1);

            for (int i = 2; i <= 15; i++)
            {
                if (i % 2 == 0)
                    xlWorkSheet.Cells[7, i] = "Monthly";
                else
                    xlWorkSheet.Cells[7, i] = "Cum.";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 7;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0.0\\% ";

                cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, i], xlWorkSheet.Cells[7, i]);
                Drawbox(cellRange, 1, 1, 1, 1);
            }

            int iRow = 7; //We start at row 6
          
            int row_num = 0;
            foreach (DataRow r in dtmonth.Rows)
            {
                iRow++;

                if (row_num == 0)
                    xlWorkSheet.Cells[iRow, 1] = "Month of start";
                else
                    xlWorkSheet.Cells[iRow, 1] = r["mons"].ToString().Replace("mn", "month after start");

                Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 1], xlWorkSheet.Cells[iRow, 1]);
                if (row_num == 0)
                    Drawbox(cellRange1, 1, 1, 1, 0);
                else
                    Drawbox(cellRange1, 1, 1, 0, 0);

                xlWorkSheet.Cells[iRow, 2] = r["c6"].ToString();
                Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 2], xlWorkSheet.Cells[iRow, 2]);
                SetCellStyle(cellRange2, row_num);
                
                DataRow rr = dtcum.Rows[row_num];

                xlWorkSheet.Cells[iRow, 3] = rr["c6"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 3], xlWorkSheet.Cells[iRow, 3]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 4] = r["c5"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 4], xlWorkSheet.Cells[iRow, 4]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 5] = rr["c5"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 5], xlWorkSheet.Cells[iRow, 5]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 6] = r["c4"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 6], xlWorkSheet.Cells[iRow, 6]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 7] = rr["c4"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 7], xlWorkSheet.Cells[iRow, 7]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 8] = r["c3"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 8], xlWorkSheet.Cells[iRow, 8]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 9] = rr["c3"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 9], xlWorkSheet.Cells[iRow, 9]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 10] = r["c2"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 10], xlWorkSheet.Cells[iRow, 10]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 11] = rr["c2"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 11], xlWorkSheet.Cells[iRow, 11]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 12] = r["c1"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 12], xlWorkSheet.Cells[iRow, 12]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 13] = rr["c1"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 13], xlWorkSheet.Cells[iRow, 13]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 14] = r["c0"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 14], xlWorkSheet.Cells[iRow, 14]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 15] = rr["c0"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 15], xlWorkSheet.Cells[iRow, 15]);
                SetCellStyle(cellRange2, row_num);

                row_num++;
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 5;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 5;
            xlWorkSheet.PageSetup.LeftMargin = 10;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void SetCellStyle(Microsoft.Office.Interop.Excel.Range cellRange2, int row_num)
        {
            if (row_num == 0)
                Drawbox(cellRange2, 1, 1, 1, 0);
            else
                Drawbox(cellRange2, 1, 1, 0, 0);
        }

        private void ExportSEToExcel(string survey)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 7.5;
            xlWorkSheet.Rows.Font.Name = "Arial";

            int year = DateTime.Now.Year;
            string title1 = string.Empty;
            string title2 = string.Empty;

            DataTable dtse = data_object.GetTimeLenLotPdsesData(survey);

            if (survey == "N")
            {
                title1 = "Table 4a. Standard Errors of Percent Distribution of Value Put in Place Each Month from Start to Completion for Private ";
                title2 = "Nonresidential Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
                xlWorkSheet.Name = "T4a";
            }
            else if (survey == "P")
            {
                title1 = "Table 5a. Standard Errors of Percent Distribution of Value Put in Place Each Month from Start to Completion for State and Local";
                title2 = "Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
                xlWorkSheet.Name = "T5a";
            }
            else
            {
                title1 = "Table 6a. Standard Errors of Percent Distribution of Value Put in Place Each Month from Start to Completion";
                title2 = "for Private Multifamily Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
                xlWorkSheet.Name = "T6a";
            }

            Microsoft.Office.Interop.Excel.Range cellRange;
            if (survey != "M")
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "O"]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "K"]);

            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            cellRange.Font.Size = 9;
            cellRange.RowHeight = 10;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            if (survey != "M")
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "O"]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "K"]);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Font.Size = 9;
            cellRange.RowHeight = 10;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            if (survey != "M")
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "O"]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "K"]);
            xlWorkSheet.Cells[4, 1] = "(Details may not add to totals due to rounding.)";
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build table
            xlWorkSheet.Cells[5, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[5, 2] = "Value of Project (Thousands of dollars)";
            if (survey != "M")
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 15]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 11]);
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 1, 1, 1, 1);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[6, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 1], xlWorkSheet.Cells[6, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 2] = "$10,000 or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 2], xlWorkSheet.Cells[6, 3]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 4] = "$5,000 - $9,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 4], xlWorkSheet.Cells[6, 5]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 6] = "$3,000 - $4,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 6], xlWorkSheet.Cells[6, 7]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            if (survey != "M")
            {
                xlWorkSheet.Cells[6, 8] = "$1,000 - $2,999";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 8], xlWorkSheet.Cells[6, 9]);
                cellRange.Merge(Type.Missing);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Drawbox(cellRange, 1, 1, 1, 1);

                xlWorkSheet.Cells[6, 10] = "$250 - $999";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 10], xlWorkSheet.Cells[6, 11]);
                cellRange.Merge(Type.Missing);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Drawbox(cellRange, 1, 1, 1, 1);

                xlWorkSheet.Cells[6, 12] = "Less than $250";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 12], xlWorkSheet.Cells[6, 13]);
                cellRange.Merge(Type.Missing);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Drawbox(cellRange, 1, 1, 1, 1);

                xlWorkSheet.Cells[6, 14] = "All Values";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 14], xlWorkSheet.Cells[6, 15]);
                cellRange.Merge(Type.Missing);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Drawbox(cellRange, 1, 1, 1, 1);
            }
            else
            {
                xlWorkSheet.Cells[6, 8] = "$Less than $3,000";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 8], xlWorkSheet.Cells[6, 9]);
                cellRange.Merge(Type.Missing);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Drawbox(cellRange, 1, 1, 1, 1);

                xlWorkSheet.Cells[6, 10] = "All Values";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 10], xlWorkSheet.Cells[6, 11]);
                cellRange.Merge(Type.Missing);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                Drawbox(cellRange, 1, 1, 1, 1);
            }

            xlWorkSheet.Cells[7, 1] = "Month work put in place ";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).ColumnWidth = 18;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, 1], xlWorkSheet.Cells[7, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            int num_cols = 15;
            if (survey == "M")
                num_cols = 11;

            for (int i = 2; i <= num_cols; i++)
            {
                if (i % 2 == 0)
                    xlWorkSheet.Cells[7, i] = "Monthly";
                else
                    xlWorkSheet.Cells[7, i] = "Cum.";

                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 7;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0.000";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, i], xlWorkSheet.Cells[7, i]);

                Drawbox(cellRange, 1, 1, 1, 1);
            }

            int iRow = 7; //We start at row 6

            int row_num = 0;
            foreach (DataRow r in dtse.Rows)
            {
                iRow++;

                if (row_num == 0)
                    xlWorkSheet.Cells[iRow, 1] = "Month of start";
                else if (row_num == dtse.Rows.Count - 1)
                    xlWorkSheet.Cells[iRow, 1] = r["month"].ToString() + " + month after start";
                else
                    xlWorkSheet.Cells[iRow, 1] = r["month"].ToString() + " month after start";

                Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 1], xlWorkSheet.Cells[iRow, 1]);
                SetCellStyle(cellRange1, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 2] = r["VG6MONSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 2] = r["VG4MONSE"].ToString();
                Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 2], xlWorkSheet.Cells[iRow, 2]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 3] = r["VG6CUMSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 3] = r["VG4CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 3], xlWorkSheet.Cells[iRow, 3]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 4] = r["VG5MONSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 4] = r["VG3MONSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 4], xlWorkSheet.Cells[iRow, 4]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 5] = r["VG5CUMSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 5] = r["VG3CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 5], xlWorkSheet.Cells[iRow, 5]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 6] = r["VG4MONSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 6] = r["VG2MONSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 6], xlWorkSheet.Cells[iRow, 6]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 7] = r["VG4CUMSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 7] = r["VG2CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 7], xlWorkSheet.Cells[iRow, 7]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 8] = r["VG3MONSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 8] = r["VG1MONSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 8], xlWorkSheet.Cells[iRow, 8]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 9] = r["VG3CUMSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 9] = r["VG1CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 9], xlWorkSheet.Cells[iRow, 9]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 10] = r["VG2MONSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 10] = r["VG0MONSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 10], xlWorkSheet.Cells[iRow, 10]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                    xlWorkSheet.Cells[iRow, 11] = r["VG2CUMSE"].ToString();
                else
                    xlWorkSheet.Cells[iRow, 11] = r["VG0CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 11], xlWorkSheet.Cells[iRow, 11]);
                SetCellStyle(cellRange2, row_num);

                if (survey != "M")
                {
                    xlWorkSheet.Cells[iRow, 12] = r["VG1MONSE"].ToString();
                    cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 12], xlWorkSheet.Cells[iRow, 12]);
                    SetCellStyle(cellRange2, row_num);

                    xlWorkSheet.Cells[iRow, 13] = r["VG1CUMSE"].ToString();
                    cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 13], xlWorkSheet.Cells[iRow, 13]);
                    SetCellStyle(cellRange2, row_num);

                    xlWorkSheet.Cells[iRow, 14] = r["VG0MONSE"].ToString();
                    cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 14], xlWorkSheet.Cells[iRow, 14]);
                    SetCellStyle(cellRange2, row_num);

                    xlWorkSheet.Cells[iRow, 15] = r["VG0CUMSE"].ToString();
                    cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 15], xlWorkSheet.Cells[iRow, 15]);
                    SetCellStyle(cellRange2, row_num);
                }

                row_num++;
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 5;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 5;
            xlWorkSheet.PageSetup.LeftMargin = 10;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void ExportHighwaySEToExcel()
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 7.5;
            xlWorkSheet.Rows.Font.Name = "Arial";

            int year = DateTime.Now.Year;
            string title1 = string.Empty;
            string title2 = string.Empty;

            DataTable dthyse = data_object.GetTimeLenLOTPDHWYSESData();
            title1 = "Table 5HA. Standard Errors of Percent Distribution of Value Put in Place Each Month from Start to Completion for State and Local Highway and Street";
            title2 = "Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
            xlWorkSheet.Name = "H5A";
         
            Microsoft.Office.Interop.Excel.Range cellRange;
           
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "O"]);

            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            cellRange.Font.Size = 9;
            cellRange.RowHeight = 10;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "O"]);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Font.Size = 9;
            cellRange.RowHeight = 10;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "O"]);
          
            xlWorkSheet.Cells[4, 1] = "(Details may not add to totals due to rounding.)";
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build table
            xlWorkSheet.Cells[5, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[5, 2] = "Value of Project (Thousands of dollars)";
         
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 15]);
          
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 1, 1, 1, 1);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[6, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 1], xlWorkSheet.Cells[6, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 2] = "$10,000 or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 2], xlWorkSheet.Cells[6, 3]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 4] = "$5,000 - $9,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 4], xlWorkSheet.Cells[6, 5]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 6] = "$3,000 - $4,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 6], xlWorkSheet.Cells[6, 7]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 8] = "$1,000 - $2,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 8], xlWorkSheet.Cells[6, 9]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 10] = "$250 - $999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 10], xlWorkSheet.Cells[6, 11]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 12] = "Less than $250";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 12], xlWorkSheet.Cells[6, 13]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 14] = "All Values";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 14], xlWorkSheet.Cells[6, 15]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);
           
            xlWorkSheet.Cells[7, 1] = "Quarter after start work put in place";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).ColumnWidth = 18;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, 1], xlWorkSheet.Cells[7, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            int num_cols = 15;

            for (int i = 2; i <= num_cols; i++)
            {
                if (i % 2 == 0)
                    xlWorkSheet.Cells[7, i] = "Quarterly";
                else
                    xlWorkSheet.Cells[7, i] = "Cum.";

                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 7;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0.000";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, i], xlWorkSheet.Cells[7, i]);

                Drawbox(cellRange, 1, 1, 1, 1);
            }

            int iRow = 7; //We start at row 6

            int row_num = 0;
            foreach (DataRow r in dthyse.Rows)
            {
                iRow++;

                if (row_num == 0)
                    xlWorkSheet.Cells[iRow, 1] = "Quarter of start";
                else if (row_num == 1)
                    xlWorkSheet.Cells[iRow, 1] = "1st Quarter after start";
                else if (row_num == 2)
                    xlWorkSheet.Cells[iRow, 1] = "2nd Quarter after start";
                else if (row_num == 3)
                    xlWorkSheet.Cells[iRow, 1] = "3rd Quarter after start";
                else if (row_num == 15)
                    xlWorkSheet.Cells[iRow, 1] = "15th + Quarter after start";
                else
                    xlWorkSheet.Cells[iRow, 1] = r["qtr"].ToString() + "th Quarter after start";

                Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 1], xlWorkSheet.Cells[iRow, 1]);
                SetCellStyle(cellRange1, row_num);

                xlWorkSheet.Cells[iRow, 2] = r["VG6QTRSE"].ToString();
                Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 2], xlWorkSheet.Cells[iRow, 2]);
                SetCellStyle(cellRange2, row_num);
                xlWorkSheet.Cells[iRow, 3] = r["VG6CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 3], xlWorkSheet.Cells[iRow, 3]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 4] = r["VG5QTRSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 4], xlWorkSheet.Cells[iRow, 4]);
                SetCellStyle(cellRange2, row_num);
                xlWorkSheet.Cells[iRow, 5] = r["VG5CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 5], xlWorkSheet.Cells[iRow, 5]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 6] = r["VG4QTRSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 6], xlWorkSheet.Cells[iRow, 6]);
                SetCellStyle(cellRange2, row_num);
                xlWorkSheet.Cells[iRow, 7] = r["VG4CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 7], xlWorkSheet.Cells[iRow, 7]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 8] = r["VG3QTRSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 8], xlWorkSheet.Cells[iRow, 8]);
                SetCellStyle(cellRange2, row_num);
                xlWorkSheet.Cells[iRow, 9] = r["VG3CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 9], xlWorkSheet.Cells[iRow, 9]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 10] = r["VG2QTRSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 10], xlWorkSheet.Cells[iRow, 10]);
                SetCellStyle(cellRange2, row_num);
                xlWorkSheet.Cells[iRow, 11] = r["VG2CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 11], xlWorkSheet.Cells[iRow, 11]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 12] = r["VG1QTRSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 12], xlWorkSheet.Cells[iRow, 12]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 13] = r["VG1CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 13], xlWorkSheet.Cells[iRow, 13]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 14] = r["VG0QTRSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 14], xlWorkSheet.Cells[iRow, 14]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 15] = r["VG0CUMSE"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 15], xlWorkSheet.Cells[iRow, 15]);
                SetCellStyle(cellRange2, row_num);
                
                row_num++;
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 5;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 5;
            xlWorkSheet.PageSetup.LeftMargin = 10;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void ExportMToExcel()
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 7.5;
            xlWorkSheet.Rows.Font.Name = "Arial";
          
            int year = DateTime.Now.Year;
           
            string title1 = "Table 6. Percent Distribution of Value Put in Place Each Month from Start to Completion for Private Multifamily";
            string title2 = "Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
            xlWorkSheet.Name = "T6";
            
            Microsoft.Office.Interop.Excel.Range cellRange;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "K"]);
            cellRange.RowHeight = 10;
            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            cellRange.Font.Size = 9;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "K"]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 10;
            cellRange.Font.Size = 9;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "K"]);
            xlWorkSheet.Cells[4, 1] = "(Details may not add to totals due to rounding.)";
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build table
            xlWorkSheet.Cells[5, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[5, 2] = "Value of Project (Thousands of dollars)";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 11]);
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 1, 1, 1, 1);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[6, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 1], xlWorkSheet.Cells[6, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 2] = "$10,000 or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 2], xlWorkSheet.Cells[6, 3]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 4] = "$5,000 - $9,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 4], xlWorkSheet.Cells[6, 5]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 6] = "$3,000 - $4,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 6], xlWorkSheet.Cells[6, 7]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 8] = "$Less than $3000";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 8], xlWorkSheet.Cells[6, 9]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 10] = "All Values";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 10], xlWorkSheet.Cells[6, 11]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[7, 1] = "Month work put in place ";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).ColumnWidth = 18;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, 1], xlWorkSheet.Cells[7, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            for (int i = 2; i <= 11; i++)
            {
                if (i % 2 == 0)
                    xlWorkSheet.Cells[7, i] = "Monthly";
                else
                    xlWorkSheet.Cells[7, i] = "Cum.";

                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 7;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0.0\\% ";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, i], xlWorkSheet.Cells[7, i]);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                Drawbox(cellRange, 1, 1, 1, 1);
            }

            int iRow = 7; //We start at row 6

            int row_num = 0;
            foreach (DataRow r in dtmonth.Rows)
            {
                iRow++;

                if (row_num == 0)
                    xlWorkSheet.Cells[iRow, 1] = "Month of start";
                else
                    xlWorkSheet.Cells[iRow, 1] = r["mons"].ToString().Replace("mn", "month after start");

                Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 1], xlWorkSheet.Cells[iRow, 1]);
                SetCellStyle(cellRange1, row_num);

                xlWorkSheet.Cells[iRow, 2] = r["c4"].ToString();
                Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 2], xlWorkSheet.Cells[iRow, 2]);
                SetCellStyle(cellRange2, row_num);

                DataRow rr = dtcum.Rows[row_num];

                xlWorkSheet.Cells[iRow, 3] = rr["c4"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 3], xlWorkSheet.Cells[iRow, 3]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 4] = r["c3"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 4], xlWorkSheet.Cells[iRow, 4]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 5] = rr["c3"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 5], xlWorkSheet.Cells[iRow, 5]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 6] = r["c2"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 6], xlWorkSheet.Cells[iRow, 6]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 7] = rr["c2"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 7], xlWorkSheet.Cells[iRow, 7]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 8] = r["c1"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 8], xlWorkSheet.Cells[iRow, 8]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 9] = rr["c1"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 9], xlWorkSheet.Cells[iRow, 9]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 10] = r["c0"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 10], xlWorkSheet.Cells[iRow, 10]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 11] = rr["c0"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 11], xlWorkSheet.Cells[iRow, 11]);
                SetCellStyle(cellRange2, row_num);

                row_num++;
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 5;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 5;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void ExportHighwayToExcel()
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 7.5;
            xlWorkSheet.Rows.Font.Name = "Arial";

            int year = DateTime.Now.Year;
         
            DataTable dthy = data_object.GetTimeLenLOTPDHWYESTData();

            string title1 = "Table 5H. Quarterly Percent Distribution of Value Put in Place from Start to Completion for State and Local Highway and Street ";
            string title2 = "Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value.";
            xlWorkSheet.Name = "H5";

            Microsoft.Office.Interop.Excel.Range cellRange;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "O"]);
            cellRange.RowHeight = 10;
            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            cellRange.Font.Size = 9;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "O"]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 10;
            cellRange.Font.Size = 9;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "O"]);
            xlWorkSheet.Cells[4, 1] = "(Details may not add to totals due to rounding.)";
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build table
            xlWorkSheet.Cells[5, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[5, 2] = "Value of Project (Thousands of dollars)";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 15]);
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 1, 1, 1, 1);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[6, 1] = " ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 1], xlWorkSheet.Cells[6, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 2] = "$10,000 or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 2], xlWorkSheet.Cells[6, 3]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 4] = "$5,000 - $9,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 4], xlWorkSheet.Cells[6, 5]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 6] = "$3,000 - $4,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 6], xlWorkSheet.Cells[6, 7]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 8] = "$1,000 - $2,999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 8], xlWorkSheet.Cells[6, 9]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 10] = "$250 - $999";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 10], xlWorkSheet.Cells[6, 11]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 12] = "Less than $250";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 12], xlWorkSheet.Cells[6, 13]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[6, 14] = "All Values";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 14], xlWorkSheet.Cells[6, 15]);
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            Drawbox(cellRange, 1, 1, 1, 1);

            xlWorkSheet.Cells[7, 1] = "Quarter after start work put in place";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).ColumnWidth = 18;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, 1], xlWorkSheet.Cells[7, 1]);
            Drawbox(cellRange, 1, 1, 1, 1);

            for (int i = 2; i <= 15; i++)
            {
                if (i % 2 == 0)
                    xlWorkSheet.Cells[7, i] = "Quarterly";
                else
                    xlWorkSheet.Cells[7, i] = "Cum.";

                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 7;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0.0\\% ";

                cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, i], xlWorkSheet.Cells[7, i]);
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                Drawbox(cellRange, 1, 1, 1, 1);
            }

            int iRow = 7; //We start at row 6

            int row_num = 0;
            foreach (DataRow r in dthy.Rows)
            {
                iRow++;

                if (row_num == 0)
                    xlWorkSheet.Cells[iRow, 1] = "Quarter of start";
                else if (row_num == 1)
                    xlWorkSheet.Cells[iRow, 1] = "1st Quarter after start";
                else if (row_num == 2)
                    xlWorkSheet.Cells[iRow, 1] = "2nd Quarter after start";
                else if (row_num == 3)
                    xlWorkSheet.Cells[iRow, 1] = "3rd Quarter after start";
                else if (row_num == 15)
                    xlWorkSheet.Cells[iRow, 1] = "15th + Quarter after start";
                else 
                    xlWorkSheet.Cells[iRow, 1] = r["qtr"] + "th Quarter after start";

                Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 1], xlWorkSheet.Cells[iRow, 1]);
                SetCellStyle(cellRange1, row_num);

                xlWorkSheet.Cells[iRow, 2] = r["VG6QTREST"].ToString();
                Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 2], xlWorkSheet.Cells[iRow, 2]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 3] = r["VG6CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 3], xlWorkSheet.Cells[iRow, 3]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 4] = r["VG5QTREST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 4], xlWorkSheet.Cells[iRow, 4]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 5] = r["VG5CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 5], xlWorkSheet.Cells[iRow, 5]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 6] = r["VG4QTREST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 6], xlWorkSheet.Cells[iRow, 6]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 7] = r["VG4CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 7], xlWorkSheet.Cells[iRow, 7]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 8] = r["VG3QTREST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 8], xlWorkSheet.Cells[iRow, 8]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 9] = r["VG3CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 9], xlWorkSheet.Cells[iRow, 9]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 10] = r["VG2QTREST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 10], xlWorkSheet.Cells[iRow, 10]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 11] = r["VG2CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 11], xlWorkSheet.Cells[iRow, 11]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 12] = r["VG1QTREST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 12], xlWorkSheet.Cells[iRow, 12]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 13] = r["VG1CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 13], xlWorkSheet.Cells[iRow, 13]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 14] = r["VG0QTREST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 14], xlWorkSheet.Cells[iRow, 14]);
                SetCellStyle(cellRange2, row_num);

                xlWorkSheet.Cells[iRow, 15] = r["VG0CUMEST"].ToString();
                cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, 15], xlWorkSheet.Cells[iRow, 15]);
                SetCellStyle(cellRange2, row_num);

                row_num++;
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 5;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 5;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void Drawbox(Microsoft.Office.Interop.Excel.Range borderRange, int left, int right, int top, int bottom)
        {
            borderRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            Microsoft.Office.Interop.Excel.Borders border = borderRange.Borders;
            if (right == 0)
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            else
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            if (left == 0)
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            else
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            if (top == 0)
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            else
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            if (bottom == 0)
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            else
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        }

    }
}
