/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmSpecManufacturingMon.cs

 Programmer    : Christine Zhang

 Creation Date : 10/25/2022

 Inputs        : dbo.vipstatab 
                 dbo.LSFANN
                 dbo.BSTANN

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display monthly manufacturing not
                 seasonally adjusted value of private nonresidential 
                 data in a selected year.

 Detail Design : Detailed design for Special Manufacturing
 Change Request: CR#714

 Other         : Called by: Tabulations -> Special -> 
                    Manufacturing->monthly

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/

using CprsDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.IO;
using CprsBLL;
using System.Threading;

namespace Cprs
{
    public partial class frmSpecManufacturingMon : frmCprsParent
    {

        private SpecManufacturingMonData data_object;
        private string sdate;

        //Declare Excel Interop variables
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;

        private string saveFilename;
        private frmMessageWait waiting;

        object misValue = System.Reflection.Missing.Value;

        public frmSpecManufacturingMon()
        {
            InitializeComponent();
        }

        private void frmSpecManufacturingMon_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            DateTime dt = DateTime.Now;
            data_object = new SpecManufacturingMonData();
            sdate = data_object.GetSdateFromVIPSTTAB();

            //First Date
            DateTime firstDate = new DateTime(Convert.ToInt16(sdate.Substring(0, 4)), Convert.ToInt16(sdate.Substring(4, 2)), 1);
            cbMonth.Items.Add(sdate);

            DateTime nextMonth;
            string nMonth;

            for (int i = 1; i < 5; i++)
            {
                nextMonth = firstDate.AddMonths(-i);
                nMonth = nextMonth.Year.ToString() + nextMonth.Month.ToString("00");
                cbMonth.Items.Add(nMonth);
            }
            nextMonth = firstDate.AddMonths(-12);
            nMonth = nextMonth.Year.ToString() + nextMonth.Month.ToString("00");
            cbMonth.Items.Add(nMonth);

            cbMonth.SelectedIndex = 0;
            panel2.Visible = true;

            dgData.DataSource = data_object.GetSpecManufacturingMonData(sdate);
            SetColumnHeader();
            lblTitle.Text = "Monthly Value of Private Nonresidential Manufacturing Construction";
            this.label2.Text = "Millions of Dollars";

        }

        private void SetColumnHeader()
        {
            dgData.Columns[0].Visible = false;

            dgData.Columns[1].HeaderText = "   ";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[1].Width = 200;

            dgData.Columns[2].HeaderText = "US";
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[3].HeaderText = "Northeast";
            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[4].HeaderText = "New England";
            dgData.Columns[4].DefaultCellStyle.Format = "N0";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[5].HeaderText = "Mid Atlantic";
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[6].HeaderText = "Midwest";
            dgData.Columns[6].DefaultCellStyle.Format = "N0";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[7].HeaderText = "East North Central";
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[8].HeaderText = "West North Central";
            dgData.Columns[8].DefaultCellStyle.Format = "N0";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[9].HeaderText = "South";
            dgData.Columns[9].DefaultCellStyle.Format = "N0";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[10].HeaderText = "South Atlantic";
            dgData.Columns[10].DefaultCellStyle.Format = "N0";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[11].HeaderText = "East South Central";
            dgData.Columns[11].DefaultCellStyle.Format = "N0";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[12].HeaderText = "West South Central";
            dgData.Columns[12].DefaultCellStyle.Format = "N0";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[13].HeaderText = "West";
            dgData.Columns[13].DefaultCellStyle.Format = "N0";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[14].HeaderText = "Mountain";
            dgData.Columns[14].DefaultCellStyle.Format = "N0";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[15].HeaderText = "Pacific";
            dgData.Columns[15].DefaultCellStyle.Format = "N0";
            dgData.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SetColumnHeaderCvs()
        {
            dgData.Columns[0].HeaderText = "   ";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgData.Columns[1].HeaderText = "US";
            dgData.Columns[1].DefaultCellStyle.Format = "N1";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[2].HeaderText = "Northeast";
            dgData.Columns[2].DefaultCellStyle.Format = "N1";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[3].HeaderText = "New England";
            dgData.Columns[3].DefaultCellStyle.Format = "N1";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[4].HeaderText = "Mid Atlantic";
            dgData.Columns[4].DefaultCellStyle.Format = "N1";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[5].HeaderText = "Midwest";
            dgData.Columns[5].DefaultCellStyle.Format = "N1";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[6].HeaderText = "East North Central";
            dgData.Columns[6].DefaultCellStyle.Format = "N1";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[7].HeaderText = "West North Central";
            dgData.Columns[7].DefaultCellStyle.Format = "N1";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[8].HeaderText = "South";
            dgData.Columns[8].DefaultCellStyle.Format = "N1";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[9].HeaderText = "South Atlantic";
            dgData.Columns[9].DefaultCellStyle.Format = "N1";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[10].HeaderText = "East South Central";
            dgData.Columns[10].DefaultCellStyle.Format = "N1";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[11].HeaderText = "West South Central";
            dgData.Columns[11].DefaultCellStyle.Format = "N1";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[12].HeaderText = "West";
            dgData.Columns[12].DefaultCellStyle.Format = "N1";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[13].HeaderText = "Mountain";
            dgData.Columns[13].DefaultCellStyle.Format = "N1";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[14].HeaderText = "Pacific";
            dgData.Columns[14].DefaultCellStyle.Format = "N1";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void frmSpecManufacturingMon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            int sel_index = cbMonth.SelectedIndex;
            if (sel_index >= 0)
            {
                dgData.DataSource = data_object.GetSpecManufacturingMonData(cbMonth.SelectedItem.ToString());
                SetColumnHeader();
               
                panel2.Visible = (sel_index == 0);
            }
        }

        private void rdCV_CheckedChanged(object sender, EventArgs e)
        {
            cbMonth.Enabled = false;
            dgData.DataSource = data_object.GetSpecManufacturingMonDataCvs(sdate);
            SetColumnHeaderCvs();

            lblTitle.Text = "Coefficients of Variation for Private Nonresidential Manufacturing";
            this.label2.Text = "      (Percent)";

        }

        private void rdValue_CheckedChanged(object sender, EventArgs e)
        {
            cbMonth.Enabled = true;
            int sel_index = cbMonth.SelectedIndex;
            if (sel_index >= 0)
            {
                dgData.DataSource = data_object.GetSpecManufacturingMonData(cbMonth.SelectedItem.ToString());
                SetColumnHeader();
            }

            lblTitle.Text = "Table1. Monthly Value of Private Nonresidential Manufacturing Construction";
            this.label2.Text = "Millions of Dollars";
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save a File";

            saveFileDialog1.FileName = "ManufacturingMon.xls";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //delete exist file
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            //this.Enabled = false; prevents screen disappearing when commented out

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
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\ManufacturingMon.xls";

            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ExportToExcel();

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Suppress overwrite request
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

        //create excel
        private void ExportToExcel()
        {

            string stitle = "Table1. Value of Private Manufacturing Construction Put in Place by Geographic Division - Not Seasonally Adjusted";
            string subtitle = "(Millions of Dollars. Details may not add to totals due to rounding.)";

            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = sdate;

            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            int last_col = 8;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            //titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through N
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
            //titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row
            xlWorkSheet.Cells[3, 1] = "Note: These data are experimental. Users should take caution using estimates -sample sizes may be small and the standard errors may be large.";
            //Span the title across columns A through N
            Microsoft.Office.Interop.Excel.Range titleRange22 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, last_col]);
            titleRange22.Merge(Type.Missing);

            //Increase the font-size of the title
            //titleRange22.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Span the title in last col
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[5, last_col], xlWorkSheet.Cells[5, last_col]);
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange3.RowHeight = 32;
            
            Microsoft.Office.Interop.Excel.Borders border = titleRange3.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //add "coefficient of variabtion" line
            xlWorkSheet.Cells[5, last_col] = "Coefficient\n" + "of\n" + " Variation";

            //set up header
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
            string pm1 = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string pm2 = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string pm3 = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string pm4 = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string pm5 = (dt.AddYears(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);

            //get abbreviate month name
            string curmons = (dt.ToString("MMM yyyy", CultureInfo.InvariantCulture));
            string pmon1 = (dt.AddMonths(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon2 = (dt.AddMonths(-2)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon3 = (dt.AddMonths(-3)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon4 = (dt.AddMonths(-4)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon5 = (dt.AddYears(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[6, 1] = "Type of Construction:";
            xlWorkSheet.Cells[6, 2] = cmm + "p";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 2], xlWorkSheet.Cells[6, 2]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[6, 3] = pmm1 + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 3], xlWorkSheet.Cells[6, 3]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[6, 4] = pmm2 + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[6, 4], xlWorkSheet.Cells[6, 4]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[6, 5] = pmm3;
            xlWorkSheet.Cells[6, 6] = pmm4;
            xlWorkSheet.Cells[6, 7] = pmm5;
            xlWorkSheet.Cells[6, 8] = cmm;

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[6, "A"], xlWorkSheet.Cells[6, last_col]);

            //Set the header row fonts bold
            //  headerRange.Font.Bold = true;
            headerRange.RowHeight = 24;
            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            ///*for datatable */

            DataTable dtt = data_object.GetSpecManufacturingMonDataExcel(sdate, pm1, pm2, pm3, pm4, pm5);

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H" };

            foreach (string s in strColumns)
            {

                if (s != "A" && s!="H")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 10.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else if (s=="H")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 10.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
                }
            }

            ////Populate rest of the data. Start at row[7] 
            int iRow = 7; //We start at row 7
            int iCol = 0;

            foreach (DataRow r in dtt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dtt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= last_col)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                }
                //bold rows (Total of Manufacturing)
                if (iRow == 7)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;

                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 3, 1] = "Source: U.S. Census Bureau, Construction Spending";
            xlWorkSheet.Cells[iRow + 4, 1] = "Additional information on the survey methodology may be found at";
            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, last_col]);
            footRange3.Merge(Type.Missing);
            footRange3.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 5, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange3.Font.Name = "Arial";
            footRange3.Font.Size = 8;
            xlWorkSheet.Cells[iRow + 6, 1] = "The Census Bureau has reviewed the data product for unauthorized disclosure of confidential information and has";
            xlWorkSheet.Cells[iRow + 7, 1] = "approved the disclosure avoidance practices applied. (Approval ID: CBDRB-FY23-ESMD009-001)";

            //repeat title header 
            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$N$6";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 40;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 80;

            //Set gridlines
            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

            //set as landscape
            xlWorkSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

            //set to fit on page for printing
            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;

            xlWorkSheet.PageSetup.PrintGridlines = true;
            xlWorkSheet.Select(Type.Missing);

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");
        }
    }
}