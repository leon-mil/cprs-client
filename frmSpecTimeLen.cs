/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecTimeLen.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 4/10/2019
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display Special Time length tabulation
Detailed Design : Detailed Design for Time length
Other           :	            
Revision History:	
**************************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
using System.Diagnostics;

namespace Cprs
{
    public partial class frmSpecTimeLen : frmCprsParent
    {
        public frmSpecTimeLen()
        {
            InitializeComponent();
        }
        private SpecTimelenData data_object;
        private string selsurvey;
        private int seltype;
        private string saveFilename;
        private frmMessageWait waiting;
        private bool data_loading;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private void frmSpecTimeLen_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //Initial survey to state and local, avg month
            selsurvey = "P";
            seltype = 0;

            int year = DateTime.Now.Year;
            label2.Text = "Project Completed in " + (year - 2).ToString() + "01-" + (year - 1).ToString() + "12";

            GetData();  
        }

        private void GetData()
        {
            data_loading = true;

            data_object = new SpecTimelenData();
            DataTable dt = data_object.GetTimeLenData(selsurvey, seltype);
            dgData.DataSource = null;
            dgData.DataSource = dt;
            SetColumnHeader();

            data_loading = false;

            btnWork.Enabled = false;
        }
        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 200;
            dgData.Columns[1].Visible = false;

            if (selsurvey == "M")
            {
                dgData.Columns[2].HeaderText = "<3000";
                dgData.Columns[2].DefaultCellStyle.Format = "N1";
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = "3000 to 4999";
                dgData.Columns[3].DefaultCellStyle.Format = "N1";
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = "5000 to 9999";
                dgData.Columns[4].DefaultCellStyle.Format = "N1";
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = ">=10000";
                dgData.Columns[5].DefaultCellStyle.Format = "N1";
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = "All";
                dgData.Columns[6].DefaultCellStyle.Format = "N1";
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            { 
                dgData.Columns[2].HeaderText = "<250";
                dgData.Columns[2].DefaultCellStyle.Format = "N1";
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = "250 to 999";
                dgData.Columns[3].DefaultCellStyle.Format = "N1";
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = "1000 to 2999";
                dgData.Columns[4].DefaultCellStyle.Format = "N1";
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = "3000 to 4999";
                dgData.Columns[5].DefaultCellStyle.Format = "N1";
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = "5000 to 9999";
                dgData.Columns[6].DefaultCellStyle.Format = "N1";
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].HeaderText = ">=10000";
                dgData.Columns[7].DefaultCellStyle.Format = "N1";
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[8].HeaderText = "All";
                dgData.Columns[8].DefaultCellStyle.Format = "N1";
                dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }


        private void rd1n_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1n.Checked)
            {
                lblTitle.Text = "NONRESIDENTIAL";
                selsurvey = "N";
                GetData();
            }
        }

        private void rd1m_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1m.Checked)
            {
                lblTitle.Text = "MULTIFAMILY";
                selsurvey = "M";
                GetData();
            }
        }
        private void rd1p_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1p.Checked)
            {
                lblTitle.Text = "STATE AND LOCAL";
                selsurvey = "P";
                GetData();
            }
        }
        private void rdbMonth_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbMonth.Checked)
            {
                lbltitle2.Text = "Average Number of Months From Start to Completion";
                seltype = 0;
                GetData();
            }
        }

        private void rdbValue_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbValue.Checked)
            {
                lbltitle2.Text = "Average Value of Project (Millions of Dollars) From Start to Completion";
                seltype = 1;
                GetData();

                btnWork.Enabled = false;
            }     
        }

        private void frmSpecTimeLen_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
           
            printer.Title = lblTitle.Text + " " + lbltitle2.Text;
            
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
            printer.printDocument.DocumentName = "Time Length";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
          
            dgData.Columns[0].Width = 160;
            int num_cols = 8;
            if (rd1m.Checked)
                num_cols = 6;
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
            bool cv_exist = data_object.CheckLotExist("LOTAVCVS");
         
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
            if (rd1p.Checked)
                saveFileDialog1.FileName = "t2" + year + ".xls"; 
            else if (rd1n.Checked)
                saveFileDialog1.FileName = "t1" + year + ".xls";
            else
                saveFileDialog1.FileName = "t3" + year + ".xls";

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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = string.Empty;
           
            string year = (DateTime.Now.Year - 1).ToString().Substring(2);
            if (rd1p.Checked)
                sfilename = dir + "\\t2" + year + ".xls";
            else if (rd1n.Checked)
                sfilename = dir + "\\t1" + year + ".xls";
            else
                sfilename = dir + "\\t3" + year + ".xls";
  
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ////create sheet table
            if (rd1n.Checked)
            {
                ExportToExcelCV("N");
                ExportToExcel("N");  
            }
            else if (rd1p.Checked)
            {
                ExportToExcelCV("P");
                ExportToExcel("P");
            }
            else
            {
                ExportToExcelCV("M");
                ExportToExcel("M");  
            }


            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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

        private void ExportToExcel(string survey)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 9;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            /*Get tables for month and value */
            DataTable dt1 = data_object.GetTimeLenData(survey, 0);
            DataTable dt2 = data_object.GetTimeLenData(survey, 1);

            int year = DateTime.Now.Year;
            string title1 = string.Empty;
            string title2 = string.Empty;

            if (survey == "N")
            {
                title1 = "Table 1. Average Number of Months from Start to Completion for Private Nonresidential Construction ";
                title2 = "Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value and Type of Construction.";
                xlWorkSheet.Name = "T1";
            }
            else if (survey == "P")
            {
                title1 = "Table 2. Average Number of Months from Start to Completion for State and Local Construction Projects";
                title2 = "Completed in " + (year - 2).ToString() + " -  " + (year - 1).ToString() + ", by Value and Type of Construction.";
                xlWorkSheet.Name = "T2";
            }
            else if (survey == "M")
            {
                title1 = "Table 3. Average Number of Months from Start to Completion for Multifamily Construction Projects";
                title2 = "Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value and Type of Construction.";
                xlWorkSheet.Name = "T3";
            }
            Microsoft.Office.Interop.Excel.Range cellRange;
            if (survey == "N" || survey == "P")
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "D"]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "E"]);

            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "D"]);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build monthly/value table
            if (survey == "N")
            {
                CreateMonthTable(survey, dt1, " ", "02", "03", "All projects*", "Office", "Commercial", 4, 0);
                CreateValueTable(dt2, " ", "02", "03", 14);

                CreateMonthTable(survey, dt1, "04", "05", "06", "Health Care", "Education", "Religious", 17, 0);
                CreateValueTable(dt2, "04", "05", "06", 27);

                CreateMonthTable(survey, dt1, "08", "1T", null, "Amusement", "Manufacturing", "", 30, 0);
                CreateValueTable( dt2, "08", "1T", null, 40);
            }
            else if (survey == "P")
            {
                CreateMonthTable(survey, dt1, " ", "02", "05", "All projects*", "Office", "Education", 4, 0);
                CreateValueTable(dt2, " ", "02", "05", 14);

                CreateMonthTable(survey, dt1, "07", "08", "09", "Public Safety", "Amusement", "Transportation", 17, 0);
                CreateValueTable(dt2, "07", "08", "09", 27);

                CreateMonthTable(survey, dt1, "12", "13", "14", "Highway", "Sewer System", "Water Supply", 30, 0);
                CreateValueTable(dt2, "12", "13", "14", 40);
            }
            else
            {
                CreateMonthTable(survey, dt1, " ", "00", null, "All projects*", "Housing", "", 4, 0);
                CreateValueTable(dt2, " ", "00", null, 14);
            }

            if (survey == "N")
                xlWorkSheet.Cells[43, 1] = "*Includes the following categories not shown separately: hotel & motel, public safety, transportation, power,";
            else if (survey == "P" )
                xlWorkSheet.Cells[43, 1] = "*Includes the following categories not shown separately: housing, hotel & motel, commercial, health care, religious, ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[43, "A"], xlWorkSheet.Cells[43, "D"]);
            cellRange.Merge(Type.Missing);
            if (survey == "N")
                xlWorkSheet.Cells[44, 1] = "communication, highway, sewer system, water supply and conservation.";
            else if (survey == "P" )
                xlWorkSheet.Cells[44, 1] = "communication, power, conservation and manufacturing.";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[44, "A"], xlWorkSheet.Cells[44, "D"]);
            cellRange.Merge(Type.Missing);


            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 10;
            
            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void ExportToExcelCV(string survey)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Rows.Font.Size = 9;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            /*Get tables for month and value */
            DataTable dt1 = data_object.GetTimeLenCVData(survey);
            
            int year = DateTime.Now.Year;
            string title1 = string.Empty;
            string title2 = string.Empty;

            if (survey == "N")
            {
               title1 = "Table 1a. Coefficients of Variation of Average Number of Months from Start to Completion for Private";
                title2 = "Nonresidential Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value and Type of Construction.";
                xlWorkSheet.Name = "T1a";
            }
            else if (survey == "P")
            {
                title1 = "Table 2a. Coefficients of Variation of Average Number of Months from Start to Completion for State and Local";
                title2 = "Construction Projects Completed in " + (year - 2).ToString() + " -  " + (year - 1).ToString() + ", by Value and Type of Construction.";
                xlWorkSheet.Name = "T2a";
            }
            else if (survey == "M")
            {
                title1 = "Table 3a. Coefficients of Variation of Average Number of Months from Start to Completion for Multifamily";
                title2 = "Construction Projects Completed in " + (year - 2).ToString() + "-" + (year - 1).ToString() + ", by Value and Type of Construction.";
                xlWorkSheet.Name = "T3a";
            }
            Microsoft.Office.Interop.Excel.Range cellRange;
            if (survey == "N" || survey == "P")
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "D"]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "E"]);

            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            xlWorkSheet.Cells[1, 1] = title1;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "D"]);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[2, 1] = title2;
            cellRange.Merge(Type.Missing);

            //Alignment the header row horizontally
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            //build monthly/value table
            if (survey == "N")
            {
                CreateMonthTable(survey, dt1, " ", "02", "03", "All projects*", "Office", "Commercial", 4, 1);
                CreateValueTableCV(survey, dt1, " ", "02", "03", 14);

                CreateMonthTable(survey, dt1, "04", "05", "06", "Health Care", "Education", "Religious", 17, 1);
                CreateValueTableCV(survey, dt1, "04", "05", "06", 27);

                CreateMonthTable(survey, dt1, "08", "1T", null, "Amusement", "Manufacturing", "", 30, 1);
                CreateValueTableCV(survey, dt1, "08", "1T", null, 40);
            }
            else if (survey == "P")
            {
                CreateMonthTable(survey, dt1, " ", "02", "05", "All projects*", "Office", "Education", 4, 1);
                CreateValueTableCV(survey, dt1, " ", "02", "05", 14);

                CreateMonthTable(survey, dt1, "07", "08", "09", "Public Safety", "Amusement", "Transportation", 17, 1);
                CreateValueTableCV(survey, dt1, "07", "08", "09", 27);

                CreateMonthTable(survey, dt1, "12", "13", "14", "Highway", "Sewer System", "Water Supply", 30, 1);
                CreateValueTableCV(survey, dt1, "12", "13", "14", 40);
            }
            else
            {
                CreateMonthTable(survey, dt1, " ", "00", null, "All projects*", "Housing", "", 4, 1);
                CreateValueTableCV(survey,  dt1, " ", "00", null, 14);
            }

            if (survey == "N")
                xlWorkSheet.Cells[43, 1] = "*Includes the following categories not shown separately: hotel & motel, public safety, transportation, power,";
            else if (survey == "P")
                xlWorkSheet.Cells[43, 1] = "*Includes the following categories not shown separately: housing, hotel & motel, commercial, health care, religious, ";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[43, "A"], xlWorkSheet.Cells[43, "D"]);
            cellRange.Merge(Type.Missing);
            if (survey == "N")
                xlWorkSheet.Cells[44, 1] = "communication, highway, sewer system, water supply and conservation.";
            else if (survey == "P")
                xlWorkSheet.Cells[44, 1] = "communication, power, conservation and manufacturing.";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[44, "A"], xlWorkSheet.Cells[44, "D"]);
            cellRange.Merge(Type.Missing);


            // Page Setup
            //Set page orientation to Portrait
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 10;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void CreateMonthTable(string survey, DataTable dt1, string tc1, string tc2, string tc3, string des1, string des2, string des3, int line_start, int type)
        {
            //build monthly table
            xlWorkSheet.Cells[line_start, 1] = " ";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 1], xlWorkSheet.Cells[line_start, 1]);
            Drawbox(cellRange, 1, 1,1,1);

            xlWorkSheet.Cells[line_start, 2] = "Average Number of Months per Construction Type";
            if (tc3 != null)
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 2], xlWorkSheet.Cells[line_start, 4]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 2], xlWorkSheet.Cells[line_start, 3]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            cellRange.Merge(Type.Missing);
            
            Drawbox(cellRange, 1, 0, 1, 1);

            xlWorkSheet.Cells[line_start + 1, 1] = "Value of Project\n(Thousands of dollars)";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 1], xlWorkSheet.Cells[line_start + 1, 1]);
            Drawbox(cellRange, 0, 1, 1, 1);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 32;
            xlWorkSheet.Cells[line_start + 1, 2] = des1;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 2], xlWorkSheet.Cells[line_start + 1, 2]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            Drawbox(cellRange, 1, 1, 1, 1);
            cellRange.Font.Bold = true;
            xlWorkSheet.Cells[line_start + 1, 3] = des2;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 3], xlWorkSheet.Cells[line_start + 1, 3]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            if (tc3 != null)
                Drawbox(cellRange, 1, 1, 1, 1);
            else
                Drawbox(cellRange, 1, 0, 1, 1);
            cellRange.Font.Bold = true;
            if (tc3 != null)
            {
                xlWorkSheet.Cells[line_start + 1, 4] = des3;
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 4], xlWorkSheet.Cells[line_start + 1, 4]);
                Drawbox(cellRange, 1, 0, 1, 1);
                cellRange.Font.Bold = true;
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }
            DataTable dm;
            if (type == 0)
            {
                if (survey == "M")
                    dm = BuildMMonthTable(dt1, tc1, tc2);
                else
                    dm = BuildMonthTable(dt1, tc1, tc2, tc3);
            }
            else
            {
                if (survey == "M")
                    dm = BuildMMonthTableCV(dt1, tc1, tc2);
                else
                    dm = BuildMonthTableCV(survey, dt1, tc1, tc2, tc3);
            }
            

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            if (tc3 != null)
                strColumns = new string[] { "A", "B", "C", "D" };
            else
                strColumns = new string[] { "A", "B", "C" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 24;
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 24;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).Font.Bold = true;
                }
            }

            int iRow = line_start + 1; //We start at row 6
            int last_col = dm.Columns.Count;
            int iCol = 0;
            foreach (DataRow r in dm.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dm.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                    {
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                        Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        cellRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                        if (iRow == line_start +2)
                            Drawbox(cellRange1, 0, 1, 1, 0);
                        else
                            Drawbox(cellRange1, 0, 1, 0, 0);
                    }
                    else
                    {
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                        Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                        cellRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                        cellRange2.NumberFormat = "0.0";
                        if (iRow == line_start + 2)
                        {
                            if (iCol != last_col)
                                Drawbox(cellRange2, 1, 1, 1, 0);
                            else
                                Drawbox(cellRange2, 1, 0, 1, 0);
                        }
                        else
                        {
                            if (iCol != last_col)
                                Drawbox(cellRange2, 1, 1, 0, 0);
                            else
                                Drawbox(cellRange2, 1, 0, 0, 0);
                        }
                    }
                }
            }
        }

        private DataTable BuildMonthTable(DataTable dt1, string tc1, string tc2, string tc3)
        {
            DataTable dm = new DataTable();
            dm.Columns.Add("des", typeof(string));
            dm.Columns.Add("col1", typeof(decimal));
            dm.Columns.Add("col2", typeof(decimal));
            if (tc3!= null)
                dm.Columns.Add("col3", typeof(decimal));

            DataRow dr1 = null;
            if (tc1.Trim() == "")
                dr1 = dt1.Rows[0];
            else
                dr1 = dt1.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc1);
            DataRow dr2 = dt1.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc2);
            DataRow dr3 = null;
            if (tc3 != null)
                dr3 = dt1.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc3);
            
            DataRow dr = dm.NewRow();
            dr[0] = "All Values";
            dr[1] = dr1["C0"];
            dr[2] = dr2["C0"];
            if (tc3 != null)
                dr[3] = dr3["C0"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$10,000 or more";
            dr[1] = dr1["C6"];
            dr[2] = dr2["C6"];
            if (tc3 != null)
                dr[3] = dr3["C6"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$5,000 - $9,999";
            dr[1] = dr1["C5"];
            dr[2] = dr2["C5"];
            if (tc3 != null)
                dr[3] = dr3["C5"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$3,000 - $4,999";
            dr[1] = dr1["C4"];
            dr[2] = dr2["C4"];
            if (tc3 != null)
                dr[3] = dr3["C4"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$1,000 - $2,999";
            dr[1] = dr1["C3"];
            dr[2] = dr2["C3"];
            if (tc3 != null)
                dr[3] = dr3["C3"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$250 - $999";
            dr[1] = dr1["C2"];
            dr[2] = dr2["C2"];
            if (tc3 != null)
                dr[3] = dr3["C2"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "Less than $250";
            dr[1] = dr1["C1"];
            dr[2] = dr2["C1"];
            if (tc3 != null)
                dr[3] = dr3["C1"];
            dm.Rows.Add(dr);

            return dm;
        }

        private DataTable BuildMMonthTable(DataTable dt1, string tc1, string tc2)
        {
            DataTable dm = new DataTable();
            dm.Columns.Add("des", typeof(string));
            dm.Columns.Add("col1", typeof(decimal));
            dm.Columns.Add("col2", typeof(decimal));
          
            DataRow dr1 = dt1.Rows[0];
            DataRow dr2 = dt1.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc2);
           
            DataRow dr = dm.NewRow();
            dr[0] = "All Values";
            dr[1] = dr1["C0"];
            dr[2] = dr2["C0"];
           
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$10,000 or more";
            dr[1] = dr1["C4"];
            dr[2] = dr2["C4"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$5,000 - $9,999";
            dr[1] = dr1["C3"];
            dr[2] = dr2["C3"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$3,000 - $4,999";
            dr[1] = dr1["C2"];
            dr[2] = dr2["C2"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "Less than $3,000";
            dr[1] = dr1["C1"];
            dr[2] = dr2["C1"];
            dm.Rows.Add(dr);
            return dm;
        }

        private DataTable BuildMonthTableCV(string survey, DataTable dt1, string tc1, string tc2, string tc3)
        {
            DataTable dm = new DataTable();
            dm.Columns.Add("des", typeof(string));
            dm.Columns.Add("col1", typeof(decimal));
            dm.Columns.Add("col2", typeof(decimal));
            if (tc3 != null)
                dm.Columns.Add("col3", typeof(decimal));

            DataRow dr1 = dt1.Rows[0];
            
            string tcstr1 = string.Empty;
            string tcstr2 = tc2 + "XX";
            string tcstr3 = string.Empty;
            if (tc3 != null)
                tcstr3 = tc3 + "XX";

            if (tc1 == " ")
                tcstr1 = "XXXX";
            else
                tcstr1 = tc1 + "XX";

            string sy = string.Empty;
            if (survey == "P")
                sy = "S";
            else if (survey == "N")
                sy = "V";
            else
                sy = survey;

            DataRow dr = dm.NewRow();
            dr[0] = "All Values";
            dr[1] = dr1[sy + tcstr1 + "LV0"];
            dr[2] = dr1[sy + tcstr2 + "LV0"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV0"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$10,000 or more";
            dr[1] = dr1[sy + tcstr1 + "LV6"];
            dr[2] = dr1[sy + tcstr2 + "LV6"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV6"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$5,000 - $9,999";
            dr[1] = dr1[sy + tcstr1 + "LV5"];
            dr[2] = dr1[sy + tcstr2 + "LV5"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV5"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$3,000 - $4,999";
            dr[1] = dr1[sy + tcstr1 + "LV4"];
            dr[2] = dr1[sy + tcstr2 + "LV4"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV4"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$1,000 - $2,999";
            dr[1] = dr1[sy + tcstr1 + "LV3"];
            dr[2] = dr1[sy + tcstr2 + "LV3"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV3"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$250 - $999";
            dr[1] = dr1[sy + tcstr1 + "LV2"];
            dr[2] = dr1[sy + tcstr2 + "LV2"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV2"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "Less than $250";
            dr[1] = dr1[sy + tcstr1 + "LV1"];
            dr[2] = dr1[sy + tcstr2 + "LV1"];
            if (tc3 != null)
                dr[3] = dr1[sy + tcstr3 + "LV1"];
            dm.Rows.Add(dr);

            return dm;
        }

        private DataTable BuildMMonthTableCV(DataTable dt1, string tc1, string tc2)
        {
            DataTable dm = new DataTable();
            dm.Columns.Add("des", typeof(string));
            dm.Columns.Add("col1", typeof(decimal));
            dm.Columns.Add("col2", typeof(decimal));

            DataRow dr1 = dt1.Rows[0];
            string tcstr1 = string.Empty;
            string tcstr2 = tc2 + "XX";
            if (tc1 == " ")
                tcstr1 = "XXXX";
            else
                tcstr1 = tc1 + "XX";

            DataRow dr = dm.NewRow();
            dr[0] = "All Values";
            dr[1] = dr1["M" + tcstr1 + "LV0"];
            dr[2] = dr1["M" + tcstr2 + "LV0"];

            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$10,000 or more";
            dr[1] = dr1["M" + tcstr1 + "LV4"];
            dr[2] = dr1["M" + tcstr2 + "LV4"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$5,000 - $9,999";
            dr[1] = dr1["M" + tcstr1 + "LV3"];
            dr[2] = dr1["M" + tcstr2 + "LV3"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "$3,000 - $4,999";
            dr[1] = dr1["M" + tcstr1 + "LV2"];
            dr[2] = dr1["M" + tcstr2 + "LV2"];
            dm.Rows.Add(dr);

            dr = dm.NewRow();
            dr[0] = "Less than $3,000";
            dr[1] = dr1["M" + tcstr1 + "LV1"];
            dr[2] = dr1[ "M" + tcstr2 + "LV1"];
            dm.Rows.Add(dr);
            return dm;
        }

        private void CreateValueTable(DataTable dt2, string tc1, string tc2, string tc3, int line_start)
        {
            DataRow dr1 = null;
            if (tc1.Trim() == "")
                dr1 = dt2.Rows[0];
            else
                dr1 = dt2.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc1);
     
            DataRow dr2 = dt2.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc2);
            DataRow dr3 = null;
            if (tc3 != null)
                dr3 = dt2.AsEnumerable().SingleOrDefault(r => r.Field<string>("tc") == tc3);

            xlWorkSheet.Cells[line_start, 1] = "(Millions of dollars) ";
            Microsoft.Office.Interop.Excel.Range cellRange;
            if (tc3 != null)
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 1], xlWorkSheet.Cells[line_start, 4]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 1], xlWorkSheet.Cells[line_start, 3]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 0, 0, 0, 1);

            xlWorkSheet.Cells[line_start+1, 1] = "Average Value of Projects\nCosting $10 Million or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start+1, 1], xlWorkSheet.Cells[line_start+1, 1]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 32;
            Drawbox(cellRange, 0, 1, 1, 1);
            if (selsurvey == "M")
                xlWorkSheet.Cells[line_start + 1, 2] = dr1[5];
            else
                xlWorkSheet.Cells[line_start + 1, 2] = dr1[7];
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 2], xlWorkSheet.Cells[line_start + 1, 2]);
            cellRange.NumberFormat = "#.0";
            Drawbox(cellRange, 1, 1, 1, 1);
            if (selsurvey == "M")
                xlWorkSheet.Cells[line_start + 1, 3] = dr2[5];
            else
                xlWorkSheet.Cells[line_start + 1, 3] = dr2[7];
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 3], xlWorkSheet.Cells[line_start + 1, 3]);
            cellRange.NumberFormat = "#.0";
            if (tc3 != null)
                Drawbox(cellRange, 1, 1, 1, 1);
            else
                Drawbox(cellRange, 1, 0, 1, 1);
            if (tc3 != null)
            {
                xlWorkSheet.Cells[line_start + 1, 4] = dr3[7];
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 4], xlWorkSheet.Cells[line_start + 1, 4]);
                cellRange.NumberFormat = "#.0";
                Drawbox(cellRange, 1, 0, 1, 1);
            }

        }

        private void CreateValueTableCV(String survey, DataTable dt2, string tc1, string tc2, string tc3, int line_start)
        {
            DataRow dr1 = null;
            dr1 = dt2.Rows[0];
            
            string tcstr1 = string.Empty;
            string tcstr2 = tc2 + "XX";
            string tcstr3 = string.Empty;
            if (tc3 != null)
                tcstr3 = tc3 + "XX";

            if (tc1 == " ")
                tcstr1 = "XXXX";
            else
                tcstr1 = tc1 + "XX";
            string sy = string.Empty;
            if (survey == "P")
                sy = "S";
            else if (survey == "N")
                sy = "V";
            else
                sy = survey;

            xlWorkSheet.Cells[line_start, 1] = "(Millions of dollars) ";
            Microsoft.Office.Interop.Excel.Range cellRange;
            if (tc3 != null)
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 1], xlWorkSheet.Cells[line_start, 4]);
            else
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 1], xlWorkSheet.Cells[line_start, 3]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            cellRange.Merge(Type.Missing);
            Drawbox(cellRange, 0, 0, 0, 1);

            xlWorkSheet.Cells[line_start + 1, 1] = "Average Value of Projects\nCosting $10 Million or more";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 1], xlWorkSheet.Cells[line_start + 1, 1]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 32;
            Drawbox(cellRange, 0, 1, 1, 1);
            
            xlWorkSheet.Cells[line_start + 1, 2] = dr1[sy + tcstr1 + "LVP"];
            
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 2], xlWorkSheet.Cells[line_start + 1, 2]);
            cellRange.NumberFormat = "#.0";
            Drawbox(cellRange, 1, 1, 1, 1);
            xlWorkSheet.Cells[line_start + 1, 3] = dr1[sy + tcstr2 + "LVP"];
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 3], xlWorkSheet.Cells[line_start + 1, 3]);
            cellRange.NumberFormat = "#.0";
            if (tc3 != null)
                Drawbox(cellRange, 1, 1, 1, 1);
            else
                Drawbox(cellRange, 1, 0, 1, 1);
            if (tc3 != null)
            {
                xlWorkSheet.Cells[line_start + 1, 4] = dr1[sy + tcstr3 + "LVP"];
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start + 1, 4], xlWorkSheet.Cells[line_start + 1, 4]);
                cellRange.NumberFormat = "#.0";
                Drawbox(cellRange, 1, 0, 1, 1);
            }

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

        private void btnWork_Click(object sender, EventArgs e)
        {
            string selected_tc = dgData.SelectedRows[0].Cells["tc"].Value.ToString().Trim();

            List<string> exlist = data_object.GetLottabexData(DateTime.Now.Year.ToString(), selsurvey, selected_tc);
            //check if totvip =0, and excluded list =0
            if (Convert.ToInt32(dgData.SelectedRows[0].Cells["c0"].Value) == 0 && exlist.Count() == 0)
            {
                MessageBox.Show("No data exists for this TC.");
                return;
            }

            //check lock
            LotlockData lock_data = new LotlockData();

            string locked_by;
            bool can_edit = true;

            locked_by = lock_data.GetLotLock(selsurvey);
            if (locked_by != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("Survey is locked by " + locked_by + ". Continue with read only access?", "Verify", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;
                else
                    can_edit = false;
            }

            this.Hide();
            frmSpecTimeLenWorksheet popup = new frmSpecTimeLenWorksheet();
            popup.CallingForm = this;
            popup.SelectedTc = selected_tc;
            popup.SelectedSurvey = selsurvey;
            popup.Editable = can_edit;
            popup.Show();
        }

        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            if (data_loading) return;

           if (rdbMonth.Checked)
                btnWork.Enabled = (dgData.SelectedRows[0].Cells["tc"].Value.ToString().Trim() != "");
        }

        public void RefreshForm()
        {
            GetData();
        }

        /*Run batch file based on job */
        private bool RunBatchFile()
        {
            string bat_file = string.Empty;
            if (selsurvey == "P")
                bat_file = "start_slot.bat";
            else if (selsurvey == "N")
                bat_file = "start_nlot.bat";
            else
                bat_file = "start_mlot.bat";
            
            Process proc = null;
            try
            {
                proc = new Process();

                proc.StartInfo.FileName = GlobalVars.BatchDir + bat_file;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit();
                proc.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void btnVariances_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to run the process?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (RunBatchFile())
                    MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                else
                    return;
            }
            
        }

        private void btnDistru_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmSpecTimeLenDist popup = new frmSpecTimeLenDist();
            popup.CallingForm = this;
            popup.SelectedSurvey = selsurvey;
            popup.Show();
        }
    }
}
