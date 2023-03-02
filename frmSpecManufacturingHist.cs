/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmSpecManufacturingMonHist.cs

 Programmer    : Christine Zhang

 Creation Date : 2/2/2023

 Inputs        : dbo.manutab
               
 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display historic manufacturing not
                 seasonally adjusted value of private nonresidential 
                 data in a selected year.

 Detail Design : Detailed design for Special Manufacturing Historic data
 Change Request: 

 Other         : Called by: Tabulations -> Special -> 
                    Manufacturing->monthly->historic

 Revisions     : See Below
 *********************************************************************
 Modified Date : 2/21/2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR#885
 Description   : update excel file name from .xls to .xlsx
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
    public partial class frmSpecManufacturingHist : frmCprsParent
    {
        private SpecManufacturingMonData data_object;

        //Declare Excel Interop variables
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;

        private string saveFilename;
        private frmMessageWait waiting;

        object misValue = System.Reflection.Missing.Value;

        public frmSpecManufacturingHist()
        {
            InitializeComponent();
        }

        private void frmSpecManufacturingHist_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            data_object = new SpecManufacturingMonData();

            dgData.DataSource = data_object.GetSpecManufacturingHistoricData();
            SetColumnHeader();


        }

        private void SetColumnHeader()
        {
            dgData.Columns[0].HeaderText = "Date";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[0].Width = 50;

            dgData.Columns[1].HeaderText = "US";
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[2].HeaderText = "Northeast";
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[3].HeaderText = "New England";
            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[4].HeaderText = "Mid Atlantic";
            dgData.Columns[4].DefaultCellStyle.Format = "N0";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[5].HeaderText = "Midwest";
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[6].HeaderText = "East North Central";
            dgData.Columns[6].DefaultCellStyle.Format = "N0";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[7].HeaderText = "West North Central";
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[8].HeaderText = "South";
            dgData.Columns[8].DefaultCellStyle.Format = "N0";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[9].HeaderText = "South Atlantic";
            dgData.Columns[9].DefaultCellStyle.Format = "N0";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[10].HeaderText = "East South Central";
            dgData.Columns[10].DefaultCellStyle.Format = "N0";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[11].HeaderText = "West South Central";
            dgData.Columns[11].DefaultCellStyle.Format = "N0";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[12].HeaderText = "West";
            dgData.Columns[12].DefaultCellStyle.Format = "N0";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[13].HeaderText = "Mountain";
            dgData.Columns[13].DefaultCellStyle.Format = "N0";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[14].HeaderText = "Pacific";
            dgData.Columns[14].DefaultCellStyle.Format = "N0";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void frmSpecManufacturingHist_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save a File";

            saveFileDialog1.FileName = "privmfgtime.xlsx";

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

       
        //create excel
        private void ExportToExcel()
        {

            string stitle = "Value of Private Manufacturing Construction Put in Place by Geographic Division - Not Seasonally Adjusted";
            string subtitle = "(Millions of Dollars. Details may not add to totals due to rounding.)";

            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "privmfgtime";

            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            int last_col = 15;

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
            xlWorkSheet.Cells[3, 1] = "";
            //Span the title across columns A through N
            Microsoft.Office.Interop.Excel.Range titleRange22 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, last_col]);
            titleRange22.Merge(Type.Missing);

            
            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[4, 1] = "Date";
            xlWorkSheet.Cells[4, 2] = "Total" + "\n" + "Manufacturing";
            xlWorkSheet.Cells[4, 3] = "Northeast";
            xlWorkSheet.Cells[4, 4] = "New England";
            xlWorkSheet.Cells[4, 5] = "Mid Atlantic";
            xlWorkSheet.Cells[4, 6] = "Midwest";
            xlWorkSheet.Cells[4, 7] = "East North" + "\n" + "Central";
            xlWorkSheet.Cells[4, 8] = "West North" + "\n" + "Central";
            xlWorkSheet.Cells[4, 9] = "South";
            xlWorkSheet.Cells[4, 10] = "South Atlantic";
            xlWorkSheet.Cells[4, 11] = "East South"+"\n" +"Central";
            xlWorkSheet.Cells[4, 12] = "West South" + "\n" + " Central";
            xlWorkSheet.Cells[4, 13] = "West";
            xlWorkSheet.Cells[4, 14] = "Mountain";
            xlWorkSheet.Cells[4, 15] = "Pacific";
           
            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, last_col]);

            //Set the header row fonts bold
           // headerRange.Font.Bold = true;
            headerRange.RowHeight = 24;
            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            ///*for datatable */

            DataTable dtt = data_object.GetSpecManufacturingHistoricData();

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 10.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 10.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "@";
                }
            }

            ////Populate rest of the data. Start at row[5] 
            int iRow = 4; //We start at row 5
            int iCol = 0;

            foreach (DataRow r in dtt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dtt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                    {
                        DateTime col = DateTime.ParseExact(r[c.ColumnName].ToString(), "yyyyMM", null);
                        string newdate = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                        if (iRow == 5)
                        {
                            xlWorkSheet.Cells[iRow, iCol] = newdate + "p";
                            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                            cellRange.Characters[7, 1].Font.Superscript = true;
                        }
                        else if (iRow == 6 || iRow == 7)
                        {
                            xlWorkSheet.Cells[iRow, iCol] = newdate + "r";
                            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]);
                            cellRange.Characters[7, 1].Font.Superscript = true;
                        }
                        else
                        {
                            xlWorkSheet.Cells[iRow, iCol] = newdate;

                        }
                    }
                    else if (iCol <= last_col)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                }
                
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, last_col]);
            footRange2.Merge(Type.Missing);
            footRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[iRow + 3, 1] = "Source: U.S. Census Bureau, Construction Spending";

            Microsoft.Office.Interop.Excel.Range footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, last_col]);
            footRange20.Merge(Type.Missing);
            footRange20.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            xlWorkSheet.Cells[iRow + 4, 1] = "Additional information on the survey methodology may be found at";

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, last_col]);
            footRange3.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 5, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "www.census.gov/construction/c30/meth.html", "www.census.gov/construction/c30/meth.html");
            footRange3.Merge(Type.Missing);
            footRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            footRange3.Font.Name = "Arial";
            footRange3.Font.Size = 8;

            xlWorkSheet.Cells[iRow + 6, 1] = "The Census Bureau has reviewed the data product for unauthorized disclosure of confidential information and has";
            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, last_col]);
            footRange4.Merge(Type.Missing);
            footRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            xlWorkSheet.Cells[iRow + 7, 1] = "approved the disclosure avoidance practices applied. (Approval ID: CBDRB-FY23-ESMD009-001)";
            Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, last_col]);
            footRange5.Merge(Type.Missing);
            footRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

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

      
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\" + "privmfgtime.xlsx";

            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ExportToExcel();

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Suppress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            //xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
    }
}
