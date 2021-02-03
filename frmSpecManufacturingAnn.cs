/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmSpecManufacturingAnn.cs

 Programmer    : Diane Musachio

 Creation Date : 9/20/2018

 Inputs        : dbo.MANUFACTURE (VIEW)
                 dbo.LSFANN
                 dbo.BSTANN

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display annual manufacturing not
                 seasonally adjusted value of private nonresidential 
                 data in a selected year.

 Detail Design : Detailed design for Special Manufacturing

 Other         : Called by: Tabulations -> Special -> 
                    Manufacturing

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
    public partial class frmSpecManufacturingAnn : frmCprsParent
    {
        private SpecManufacturingAnnData data_object;

        private DataTable dtTable;
        private DataTable curryr1;
        private DataTable lsf;
        private DataTable bst;
        private DataView view;

        private string month = "";
        private string sdate = "";
        private int stat00 = 0;
        private int year = 0;
        private int tableYear = 0;
        private int lsf00 = 0;

        // to identify column header as related to stat00
        private int k;

        //store lsf and bst data
        List<decimal> mylsf = new List<decimal>();
        decimal[] bstnum;
        decimal[] lsflist;

        private string saveFilename;
        private frmMessageWait waiting;
        private List<string> yearlist;

        //Declare Excel Interop variables
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;

        object misValue = System.Reflection.Missing.Value;

        public frmSpecManufacturingAnn()
        {
            InitializeComponent();
        }

        private void frmSpecManufacturingAnn_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            data_object = new SpecManufacturingAnnData();
            dtTable = data_object.GetSpecManufacturingAnnData();
            lsf = data_object.GetLSFAnnData();
            bst = data_object.GetBstAnnData();
            
            /* this determines lsf value for row in datagrid - stores in list -> array*/
            foreach (DataRow dr in lsf.Rows)
            {
                int lsfnum = int.Parse(dr["LSFNO"].ToString());
                mylsf.Add(decimal.Parse(dr["LSF"].ToString()));
            }
            for (int i = 24; i <= 76; i++)
            {
                decimal one = 1.00m;
                mylsf.Add(one);
            }

            /* lsf00=4 at first newtc  lsf00[3] then + 1 each iteration in lsf; if >24 lsf=1.00*/

            lsflist = mylsf.ToArray();

            sdate = "";

            sdate = dtTable.Rows[1][0].ToString();
            year = Convert.ToInt32(sdate.Substring(0, 4));
            month = sdate.Substring(4, 2).ToString();
            //month = 12.ToString();

            /* stat00 is the reference point in the table that
             establishes the beginning of the columns for the most
             recent year lsf00[3] is lsf = 4 starting point*/

            if (month == "12")
            {
                stat00 = 13;
                GetDropdown(year);
                //per 11/27/18 decision by cindy
                lsf00 = 0;
                tableYear = year;
            }
            else
            {
                
                if (month == "11")
                {
                    stat00 = 24;
                    lsf00 = 3;
                }
                else if (month == "10")
                {
                    stat00 = 23;
                    lsf00 = 3;
                }
                else if (month == "09")
                {
                    stat00 = 22;
                    lsf00 = 3;
                }
                else if (month == "08")
                {
                    stat00 = 21;
                    lsf00 = 3;
                }
                else if (month == "07")
                {
                    stat00 = 20;
                    lsf00 = 3;
                }
                else if (month == "06")
                {
                    stat00 = 19;
                    lsf00 = 3;
                }
                else if (month == "05")
                {
                    stat00 = 18;
                    lsf00 = 3;
                }
                else if (month == "04")
                {
                    stat00 = 17;
                    lsf00 = 3;
                }
                else if (month == "03")
                {
                    stat00 = 16;
                    lsf00 = 3;
                }
                else if (month == "02")
                {
                    stat00 = 15;
                    lsf00 = 2;
                }
                else if (month == "01")
                {
                    stat00 = 14;
                    lsf00 = 1;
                }
                GetDropdown(year - 1);
                tableYear = year - 1;
            }
        }

        //dropdown for years to display in combobox
        private void GetDropdown(int col)
        {
            yearlist = new List<string>();
            yearlist.Add(col.ToString());
            yearlist.Add((col - 1).ToString());
            yearlist.Add((col - 2).ToString());
            yearlist.Add((col - 3).ToString());
            yearlist.Add((col - 4).ToString());

            cbYear.DataSource = yearlist;
        }

        //get data and calculate based on lsf/bst and stat00 values
        private void GetData(DataGridView dg)
        {
            /* these accumulate total by row by column number for TOTAL row */
            decimal tOneSum = 0;
            decimal tTwoSum = 0;
            decimal tThreeSum = 0;
            decimal tFourSum = 0;
            decimal tFiveSum = 0;
            decimal tSixSum = 0;
            decimal tSevenSum = 0;
            decimal tEightSum = 0;
            decimal tNineSum = 0;
            decimal tTenSum = 0;
            decimal tElevenSum = 0;
            decimal tTwelveSum = 0;
            decimal all = 0;
            decimal total = 0;
             
            DataTable newTable = null;
            curryr1 = null;
            newTable = dtTable.Copy();
            this.view = new DataView(newTable);

            curryr1 = view.ToTable(false, "MGROUP", newTable.Columns[k].ColumnName,
                   newTable.Columns[k - 1].ColumnName, newTable.Columns[k - 2].ColumnName,
                   newTable.Columns[k - 3].ColumnName, newTable.Columns[k - 4].ColumnName,
                   newTable.Columns[k - 5].ColumnName, newTable.Columns[k - 6].ColumnName,
                   newTable.Columns[k - 7].ColumnName, newTable.Columns[k - 8].ColumnName,
                   newTable.Columns[k - 9].ColumnName, newTable.Columns[k - 10].ColumnName,
                   newTable.Columns[k - 11].ColumnName);

            curryr1.Columns.Add("YEAR", typeof(Decimal));

            dg.DataSource = curryr1;

            if (dg.Name == "dgData")
            {
                setColumnHeader(dg);
            }
            
            /* array to store bst for each cell of datagrid row by column*/
            bstnum = new decimal[12];
            int colnum = 0;

            foreach (DataGridViewColumn col in dg.Columns)
            {
                if (col.Name.Contains("TOT"))
                {
                    foreach (DataRow dr in bst.Rows)
                    {
                        if (dr["SDATE"].ToString() == sdate)
                        {
                            bstnum[colnum] = decimal.Parse(dr["BST"].ToString());
                            colnum++;
                        }
                    }
                }
            }

            //puts description instead of mgroup
            foreach (DataGridViewRow row in dg.Rows)
            {
                if (row.Cells[0].Value.ToString() == "00")
                {
                    row.Cells[0].Value = "   Plant";
                }
                else if (row.Cells[0].Value.ToString() == "10")
                {
                    row.Cells[0].Value = "   Cogeneration";
                }
                else if (row.Cells[0].Value.ToString() == "20")
                {
                    row.Cells[0].Value = "   Warehouse";
                }
                else if (row.Cells[0].Value.ToString() == "30")
                {
                    row.Cells[0].Value = "   Office";
                }

                decimal ucf = 1.25m;

                //get values from cells of datagrid
                int tOne = Convert.ToInt32(row.Cells[1].Value);
                int tTwo = Convert.ToInt32(row.Cells[2].Value);
                int tThree = Convert.ToInt32(row.Cells[3].Value);
                int tFour = Convert.ToInt32(row.Cells[4].Value);
                int tFive = Convert.ToInt32(row.Cells[5].Value);
                int tSix = Convert.ToInt32(row.Cells[6].Value);
                int tSeven = Convert.ToInt32(row.Cells[7].Value);
                int tEight = Convert.ToInt32(row.Cells[8].Value);
                int tNine = Convert.ToInt32(row.Cells[9].Value);
                int tTen = Convert.ToInt32(row.Cells[10].Value);
                int tEleven = Convert.ToInt32(row.Cells[11].Value);
                int tTwelve = Convert.ToInt32(row.Cells[12].Value);

                //use index as reference point to identify which lsfvalue to use
                int index = lsf00;

                decimal t1 = tOne * lsflist[index + 11] * bstnum[0] * ucf;
                decimal t2 = tTwo * lsflist[index + 10] * bstnum[1] * ucf;
                decimal t3 = tThree * lsflist[index + 9] * bstnum[2] * ucf;
                decimal t4 = tFour * lsflist[index + 8] * bstnum[3] * ucf;
                decimal t5 = tFive * lsflist[index + 7] * bstnum[4] * ucf;
                decimal t6 = tSix * lsflist[index + 6] * bstnum[5] * ucf;
                decimal t7 = tSeven * lsflist[index + 5] * bstnum[6] * ucf;
                decimal t8 = tEight * lsflist[index + 4] * bstnum[7] * ucf;
                decimal t9 = tNine * lsflist[index + 3] * bstnum[8] * ucf;
                decimal t10 = tTen * lsflist[index + 2] * bstnum[9] * ucf;
                decimal t11 = tEleven * lsflist[index + 1] * bstnum[10] * ucf;
                decimal t12 = tTwelve * lsflist[index] * bstnum[11] * ucf;

                //assign rounded values to cells
                row.Cells[1].Value = Decimal.Round(t1 / 1000);
                row.Cells[2].Value = Decimal.Round(t2 / 1000);
                row.Cells[3].Value = Decimal.Round(t3 / 1000);
                row.Cells[4].Value = Decimal.Round(t4 / 1000);
                row.Cells[5].Value = Decimal.Round(t5 / 1000);
                row.Cells[6].Value = Decimal.Round(t6 / 1000);
                row.Cells[7].Value = Decimal.Round(t7 / 1000);
                row.Cells[8].Value = Decimal.Round(t8 / 1000);
                row.Cells[9].Value = Decimal.Round(t9 / 1000);
                row.Cells[10].Value = Decimal.Round(t10 / 1000);
                row.Cells[11].Value = Decimal.Round(t11 / 1000);
                row.Cells[12].Value = Decimal.Round(t12 / 1000);

                //accumulate yearly totals
                tOneSum += t1;
                tTwoSum += t2;
                tThreeSum += t3;
                tFourSum += t4;
                tFiveSum += t5;
                tSixSum += t6;
                tSevenSum += t7;
                tEightSum += t8;
                tNineSum += t9;
                tTenSum += t10;
                tElevenSum += t11;
                tTwelveSum += t12;

                //add all columns
                all = t1 + t2 + t3 + t4 + t5 + t6 + t7 + t8 + t9 + t10 + t11 + t12;

                row.Cells[13].Value = Decimal.Round(all/ 1000);

                total += Decimal.Round(all/ 1000);
               
                
            }

            //add new row with totals for each column
            DataRow newRow = curryr1.NewRow();

            newRow[0] = "Total";
            newRow[1] = Decimal.Round(tOneSum / 1000);
            newRow[2] = Decimal.Round(tTwoSum / 1000);
            newRow[3] = Decimal.Round(tThreeSum / 1000);
            newRow[4] = Decimal.Round(tFourSum / 1000);
            newRow[5] = Decimal.Round(tFiveSum / 1000);
            newRow[6] = Decimal.Round(tSixSum / 1000);
            newRow[7] = Decimal.Round(tSevenSum / 1000);
            newRow[8] = Decimal.Round(tEightSum / 1000);
            newRow[9] = Decimal.Round(tNineSum / 1000);
            newRow[10] = Decimal.Round(tTenSum / 1000);
            newRow[11] = Decimal.Round(tElevenSum / 1000);
            newRow[12] = Decimal.Round(tTwelveSum / 1000);
            newRow[13] = total;

            //place new row on top line
            curryr1.Rows.InsertAt(newRow, 0);
            
        }

        private void setColumnHeader(DataGridView dg)
        {
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dg.Columns[0].HeaderText = "Type of Construction";
            dg.Columns[0].Width = 170;
            dg.Columns[1].HeaderText = cbYear.Text + "01";
            dg.Columns[1].Width = 75;
            dg.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[2].HeaderText = cbYear.Text + "02";
            dg.Columns[2].Width = 75;
            dg.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[3].HeaderText = cbYear.Text + "03";
            dg.Columns[3].Width = 75;
            dg.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[4].HeaderText = cbYear.Text + "04";
            dg.Columns[4].Width = 75;
            dg.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[5].HeaderText = cbYear.Text + "05";
            dg.Columns[5].Width = 75;
            dg.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[6].HeaderText = cbYear.Text + "06";
            dg.Columns[6].Width = 75;
            dg.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[7].HeaderText = cbYear.Text + "07";
            dg.Columns[7].Width = 75;
            dg.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[8].HeaderText = cbYear.Text + "08";
            dg.Columns[8].Width = 75;
            dg.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[9].HeaderText = cbYear.Text + "09";
            dg.Columns[9].Width = 75;
            dg.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[10].HeaderText = cbYear.Text + "10";
            dg.Columns[10].Width = 75;
            dg.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[11].HeaderText = cbYear.Text + "11";
            dg.Columns[11].Width = 75;
            dg.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[12].HeaderText = cbYear.Text + "12";
            dg.Columns[12].Width = 75;
            dg.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[13].HeaderText = cbYear.Text;
            dg.Columns[13].Width = 75;
            dg.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dg.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvc.DefaultCellStyle.Format = "N0";
            }
        }
        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            k = 0;

            if (cbYear.SelectedIndex == 0)
            {
                k = stat00;
                lsf00 = 3;
            }
            else if (cbYear.SelectedIndex == 1)
            {
                k = stat00 + 12;
                lsf00 = 15;
            }
            else if (cbYear.SelectedIndex == 2)
            {
                k = stat00 + 24;
                lsf00 = 27;
            }
            else if (cbYear.SelectedIndex == 3)
            {
                k = stat00 + 36;
                lsf00 = 39;
            }
            else if (cbYear.SelectedIndex == 4)
            {
                k = stat00 + 48;
                lsf00 = 51;
            }
        
            GetData(dgData);
            dgData.ClearSelection();
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
            string sfilename = dir + "\\Manufacturing.xls";
           
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            k = stat00;
            lsf00 = 3;
            GetData(dgDataExcel);

            ExportToExcel( yearlist[0]);

            k = stat00 + 12;
            lsf00 = 15;
            GetData(dgDataExcel);

            ExportToExcel(yearlist[1]);

            k = stat00 + 24;
            lsf00 = 27;
            GetData(dgDataExcel);

            ExportToExcel(yearlist[2]);

            k = stat00 + 36;
            lsf00 = 39;
            GetData(dgDataExcel);

            ExportToExcel(yearlist[3]);

            k = stat00 + 48;
            lsf00 = 51;
            GetData(dgDataExcel);

            ExportToExcel(yearlist[4]);

            //iterate through each worksheet to autofit all columns for printing
            foreach (Excel.Worksheet wrksht in xlWorkBook.Worksheets)
            {
                Excel.Range usedrange = wrksht.UsedRange;
                usedrange.Columns.AutoFit();     
            }

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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");
        }

        //create excel
        private void ExportToExcel(string sel_year)
        {
            string tabname = sel_year;
            string stitle = "Annual Value of Private Nonresidential Construction Put in Place";
            string stitle1 = "for Manufacturing " + sel_year;

            //xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add(After: xlWorkBook.Sheets[xlWorkBook.Sheets.Count]);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = tabname;

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;


            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            int last_col = 14;
            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N" };

            foreach (string s in strColumns)
            {

                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 10.00;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 20.00;
                }

            }

            //Span the title across columns A through N
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "N"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 16;

            //Make the title bold
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = stitle1;

            //Span the title across columns A through N
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange2.Font.Size = 10;
            titleRange2.Font.Bold = true;
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title3
            xlWorkSheet.Cells[3, 1] = "(Millions of dollars.  Details may not add to totals since all types of construction are not shown separately.)";
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, last_col]);
            titleRange3.Merge(Type.Missing);

            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, last_col]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 36;
            xlWorkSheet.Cells[5, 1] = "Type of Construction";

            ///*for datatable */
            DataTable dt = curryr1;

            foreach (DataColumn c in dt.Columns)
            {
                if (c.Caption != "MGROUP")
                {
                    for (int i = 1; i<=12 ; i++)
                    {
                        int mm = i;
                        if (mm >= 10)
                            xlWorkSheet.Cells[5, i+1] = sel_year + mm;
                        else
                            xlWorkSheet.Cells[5, i+1] = sel_year + "0" + mm;
                    }
                    xlWorkSheet.Cells[5, 14] = sel_year;
                }
            }

            ////Populate rest of the data. Start at row[6] 
            int iRow = 6; //We start at row 6

            int iCol = 0;

            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                Microsoft.Office.Interop.Excel.Range numRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, 1], xlWorkSheet.Cells[iRow, last_col]);
                numRange.NumberFormat = "#,##0";

                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= last_col)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];                  
                }
            }

            //repeat title header 
            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$N$6";
            

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 40;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 0;
            xlWorkSheet.PageSetup.LeftMargin = 80;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = true;

            //set as landscape
            xlWorkSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

            //set to fit on page for printing
            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;

            xlWorkSheet.PageSetup.PrintGridlines = true;
            xlWorkSheet.Select(Type.Missing);
     

        }
        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save a File";

            saveFileDialog1.FileName = "Manufacturing.xls";

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

        //close form
        private void frmSpecManufacturingAnn_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //The dgprint is a clone of the data so the
            //display doesn't change format

            //References to source and target grid.
            DataGridView sourceGrid = dgData;
            DataGridView targetGrid = this.dgPrint;

            //Copy all rows and cells.
            var targetRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow sourceRow in sourceGrid.Rows)
            {
                if (!sourceRow.IsNewRow)
                {
                    var targetRow = (DataGridViewRow)sourceRow.Clone();

                    foreach (DataGridViewCell cell in sourceRow.Cells)
                    {
                        targetRow.Cells[cell.ColumnIndex].Value = cell.Value;
                    }

                    targetRows.Add(targetRow);
                }
            }

            //Clear target columns and then clone all source columns.

            targetGrid.Columns.Clear();

            foreach (DataGridViewColumn column in sourceGrid.Columns)
            {
                targetGrid.Columns.Add((DataGridViewColumn)column.Clone());
            }

            targetGrid.Rows.AddRange(targetRows.ToArray());

            DGVPrinter printer = new DGVPrinter();

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.Title = "Annual Value of Private Nonresidential Construction Put in Place";
            printer.SubTitle = "for Manufacturing " + cbYear.Text;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.Userinfo = UserInfo.UserName;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPrint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
         
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.Columns[0].Width = 100;
            this.dgPrint.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            printer.PrintDataGridViewWithoutDialog(dgPrint);

            Cursor.Current = Cursors.Default;
        }

        private void frmSpecManufacturingAnn_Shown(object sender, EventArgs e)
        {
            dgData.ClearSelection();
        }
    }
}
