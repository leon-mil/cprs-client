/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabAnnualBeaUtilities.cs
Programmer    : Christine Zhang
Creation Date : May. 1 2019
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create annual bea screen for Utilities to view vip data
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
 Modified Date : 2/21/2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR#885
 Description   : update excel file name from .xls to .xlsx
 *********************************************************************/
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
using DGVPrinterHelper;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Globalization;

namespace Cprs
{
    public partial class frmTabAnnualBeaUtilities : frmCprsParent
    {
        public frmTabAnnualBeaUtilities()
        {
            InitializeComponent();
        }
        public frmTabAnnualBea CallingForm = null;
        public string Boost = string.Empty;

        private TabAnnualBeaData data_object;
        private bool form_loading = false;
        private string sdate;
        private int year1;
        private int year2;
        private frmMessageWait waiting;
        private string saveFilename;
        private bool call_callingFrom = false;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;


        private void frmTabAnnualBeaUtilities_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            form_loading = true;

            data_object = new TabAnnualBeaData();

            //get survey month
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();
            int cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            int cur_mon = Convert.ToInt16(sdate.Substring(4, 2));

            if (cur_mon > 1)
            {
                year1 = cur_year - 1;
                year2 = cur_year - 2;
            }
            else
            {
                year1 = cur_year - 2;
                year2 = cur_year - 3;
            }

            cbYear.Items.Add(year1);
            cbYear.Items.Add(year2);
            cbYear.SelectedIndex = 0;

            label1.Text = Boost;

            GetData();

            form_loading = false;

        }

        private void GetData()
        {
            int bt = 0;
            if (Boost == "Booted")
                bt = 1;

            DataTable dt = data_object.GetTabAnnualBeaDataUtitlities(cbYear.Text, bt);
            dgData.DataSource = dt;
            SetColumnHeader();

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            lblTitle.Text = "UTILITIES SUBTC " + cbYear.Text + " VIP";
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgData.Columns[0].HeaderText = "Type of Construction ";
            dgData.Columns[0].Width = 220;
            dgData.Columns[0].Frozen = true;
            dgData.Columns[1].Visible = false;

            for (int i = 2; i < 14; i++)
            {
                if (i < 11)
                    dgData.Columns[i].HeaderText = cbYear.Text + "0" + (i - 1).ToString();
                else
                    dgData.Columns[i].HeaderText = cbYear.Text + (i - 1).ToString();

                dgData.Columns[i].DefaultCellStyle.Format = "N0";
                dgData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[i].Width = 65;
            }

            dgData.Columns[14].HeaderText = cbYear.Text;
            dgData.Columns[14].DefaultCellStyle.Format = "N0";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[14].Width = 80;

        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
                GetData();
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = label1.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Annual BEA Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            //resize column
            dgData.Columns[0].Width = 128;
            for (int i = 2; i < 14; i++)
            {
                dgData.Columns[i].Width = 64;
            }
            dgData.Columns[14].Width = 70;

            printer.PrintDataGridViewWithoutDialog(dgData);

            //resize column
            dgData.Columns[0].Width = 220;
            for (int i = 2; i < 14; i++)
            {
                dgData.Columns[i].Width = 65;
            }
            dgData.Columns[14].Width = 80;

            //release printer
            GeneralFunctions.releaseObject(printer);
            Cursor.Current = Cursors.Default;
        }

        private void frmTabAnnualBeaUtilities_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save an File";

            saveFileDialog1.FileName = "cprs" + year2.ToString().Substring(2) + year1.ToString().Substring(2)  + "USubtc.xlsx";
            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //disable form
            this.Enabled = false;

            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }

        //run application
        public void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }


        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\cprs" + year2.ToString().Substring(2) + year1.ToString().Substring(2) + "USubtc.xlsx";

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            //create sheet tables
            ExportToExcel(year1.ToString());
            ExportToExcel(year2.ToString());

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
            MessageBox.Show("Files have been created");
        }

        private void ExportToExcel(string sel_year)
        {
            string stitle = string.Empty;

            string tabname = sel_year;

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = tabname;

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            xlWorkSheet.Activate();

            int bt = 0;
            if (label1.Text == "Boosted")
                bt = 1;

            string survey_str = "UTILITIES SUBTC NOT SEASONALLY ADJUSTED VIP";

            string title = string.Empty;
            if (bt==1)
                title = survey_str + " BOOSTED";
            else
                title = survey_str + " UNBOOSTED";

            //Add a title
            xlWorkSheet.Cells[1, 1] = title;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, 14]);
            titleRange.Merge(Type.Missing);

            //Make the title bold
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range cellRange;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 14]);
            cellRange.Font.Bold = true;
            cellRange.WrapText = true;
            xlWorkSheet.Cells[2, 1] = "Thousands of Dollars";
            cellRange.Merge(Type.Missing);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 14]);
            cellRange.Font.Bold = true;
           // cellRange.NumberFormat = "@";

            xlWorkSheet.Cells[4, 1] = "Type of Construction";
            for (int i = 2; i <= 13; i++)
            {
                if (i < 11)
                    xlWorkSheet.Cells[4, i] = "\t" +sel_year + "0" + (i - 1).ToString();
                else
                    xlWorkSheet.Cells[4, i] = "\t" + sel_year + (i - 1).ToString();

            }
            xlWorkSheet.Cells[4, 14] = "\t" + sel_year;

            ///*for datatable */
            DataTable dt = data_object.GetTabAnnualBeaDataUtitlities(sel_year, bt);

            ////Setup the column header row (row 5)

            ////Set the font size, text wrap of columns and format for the entire worksheet
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 37;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 2; i <= 14; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 12;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }

            ////Populate rest of the data. Start at row[5] 
            int iRow = 4; //We start at row 5

            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName != "newtc")
                    {
                        iCol++;
                        if (iCol == 1)
                            xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                        else
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                    }
                }
            }

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[21, 1]);
            cellRange.Font.Bold = true;

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

    }
}
