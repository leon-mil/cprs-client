/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecBEASLMon.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 11/05/2018
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display Monthly Special BEA tabulation

Detailed Design : Detailed Design for special bea Monthly
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


namespace Cprs
{
    public partial class frmSpecBEASLMon : frmCprsParent
    {
        public frmSpecBEASLMon()
        {
            InitializeComponent();
        }

        private SpecBEAData data_object;
        private int cur_year;
        private int sel_factor;
        private string subtc = string.Empty;
        private bool show_subtc;
        private DataTable stored_main = null;
        private DataTable display_table;
        private bool form_loading = false;
        private string saveFilename;
        private frmMessageWait waiting;
        private int monthsdiff;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private void frmSpecBEAMon_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            form_loading = true;

            DateTime dt = DateTime.Now;
            data_object = new SpecBEAData();
            string sdate = data_object.GetSdateFromVIPBea();

            cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            string end_date = (cur_year - 5).ToString() + "01";

            //First Date
            DateTime firstDate = new DateTime(cur_year, Convert.ToInt16(sdate.Substring(4, 2)), 1);

            //Second Date
            DateTime secondDate = new DateTime((cur_year - 5), 1, 1); //DateTime.Now;

            int m1 = (secondDate.Month - firstDate.Month);//for years
            int m2 = (secondDate.Year - firstDate.Year) * 12; //for months
            monthsdiff = Math.Abs(m1 + m2) + 1;

            cbMonth.Items.Add(sdate);

            for (int i = 1; i < monthsdiff; i++)
            {
                DateTime nextMonth = firstDate.AddMonths(-i);
                string nMonth = nextMonth.Year.ToString() + nextMonth.Month.ToString("00");
                cbMonth.Items.Add(nMonth);
            }

            sel_factor = 0;
            subtc = "";
            show_subtc = false;
            cbMonth.SelectedIndex = 0;

            stored_main = data_object.GetSLBeaMonTable(sel_factor);
            string[] selectedColumns = new[] { "newtc_str", "c" + monthsdiff, "d" + monthsdiff, "t" + monthsdiff, "ddown", "tc2", "newtc" };

            display_table = new DataView(stored_main).ToTable(false, selectedColumns);
            DataTable tblFiltered = display_table.AsEnumerable()
                                    .Where(row => row.Field<int>("ddown") <= 2)
            .OrderBy(row => row.Field<String>("newtc"))
            .CopyToDataTable();

            dgData.DataSource = tblFiltered;

            lbllabel.Text = "Double Click a Line to Expand";

            //set up column header
            SetColumnHeader();

            form_loading = false;
        }

        private void LoadData()
        {
            Cursor.Current = Cursors.WaitCursor;
            stored_main = data_object.GetSLBeaMonTable(sel_factor);
            int sel_index = cbMonth.SelectedIndex;
            string[] selectedColumns = new[] { "newtc_str", "c" + (monthsdiff - sel_index).ToString(), "d" + (monthsdiff - sel_index).ToString(), "t" + (monthsdiff - sel_index).ToString(), "ddown", "tc2", "newtc" };

            display_table = new DataView(stored_main).ToTable(false, selectedColumns);
            DataTable tblFiltered = display_table.AsEnumerable()
                                    .Where(row => row.Field<int>("ddown") <= 2)
            .OrderBy(row => row.Field<String>("newtc"))
            .CopyToDataTable();

            dgData.DataSource = tblFiltered;

            //Get current data
            subtc = "";
            show_subtc = false;
            cbMonth.SelectedIndex = 0;

            lbllabel.Text = "Double Click a Line to Expand";

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 200;
            dgData.Columns[0].Frozen = true;

            dgData.Columns[1].HeaderText = "State " + cbMonth.Text;
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DefaultCellStyle.NullValue = "0";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[2].HeaderText = "Local " + cbMonth.Text;
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].DefaultCellStyle.NullValue = "0";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[3].HeaderText = "Total " + cbMonth.Text;
            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.NullValue = "0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[4].Visible = false;
            dgData.Columns[5].Visible = false;
            dgData.Columns[6].Visible = false;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        private void cbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loading)
                return;

            int sel_index = cbMonth.SelectedIndex;
            string[] selectedColumns = new[] { "newtc_str", "c" + (monthsdiff - sel_index).ToString(), "d" + (monthsdiff - sel_index).ToString(), "t" + (monthsdiff - sel_index).ToString(), "ddown", "tc2", "newtc" };
            display_table = new DataView(stored_main).ToTable(false, selectedColumns);
            DataTable tblFiltered = display_table.AsEnumerable()
                                    .Where(row => row.Field<int>("ddown") <= 2)
            .OrderBy(row => row.Field<String>("newtc"))
            .CopyToDataTable();

            dgData.DataSource = tblFiltered;
            SetColumnHeader();
            lbllabel.Text = "Double Click a Line to Expand";
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "SLBEAMon.xls";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //delete exist file
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            //if (GeneralFunctions.IsFileinUse(dir + "\\FedBEAAnn.xls"))
            //{
            //    MessageBox.Show(saveFilename + " is in use. Please close it.");
            //    return;
            //}

            //disable form
            this.Enabled = false;

            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (sel_factor == 0)
            {
                lblFactor.Text = "New Factors";
                btnApply.Text = "APPLY OLD FACTORS";
                sel_factor = 1;
            }
            else
            {
                btnApply.Text = "APPLY NEW FACTORS";
                lblFactor.Text = "Old Factors";
                sel_factor = 0;
            }
            LoadData();
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
            string sfilename = dir + "\\SLBEAMon.xls";

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            //create CV sheet tables
            ExportToExcelCvs();

            //create sheet tables
            ExportToExcel1();
            
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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("File has been created");
        }

        private void ExportToExcel1()
        {
            string stitle = string.Empty;
            stitle = "STATE AND LOCAL VIP NOT SEASONALLY ADJUSTED ";

            string subtitle = "Thousand of Dollars - " + lblFactor.Text;

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "MONTHLY BEA";

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            xlWorkSheet.Activate();
            //xlWorkSheet.Application.ActiveWindow.SplitColumn = 1;
            //xlWorkSheet.Application.ActiveWindow.SplitRow = 5;
            //xlWorkSheet.Application.ActiveWindow.FreezePanes = true;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, monthsdiff * 3]);
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
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, monthsdiff * 3+1]);
            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Bold = true;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, monthsdiff * 3+1]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 36;

            DateTime secondDate = new DateTime((cur_year - 5), 1, 1); //DateTime.Now;

            xlWorkSheet.Cells[4, 1] = "Type of Construction";

            for (int i = 1; i <= monthsdiff; i++)
            {
                DateTime nextMonth = secondDate.AddMonths(i-1);
                string nMonth = nextMonth.Year.ToString() + nextMonth.Month.ToString("00");

                int j = 3 * i - 1;
                xlWorkSheet.Cells[4, j] = "State \n" + nMonth;

                j = 3 * i;
                xlWorkSheet.Cells[4, j] = "Local \n" + nMonth;

                j = 3 * i + 1;
                xlWorkSheet.Cells[4, j] = "Total \n" + nMonth;

            }

          //Setup the column header row (row 5)

          //Set the font size, text wrap of columns and format for the entire worksheet
          ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 30;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 2; i <= monthsdiff * 3 + 1; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 10;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }

            ////Populate rest of the data. Start at row[5] 
            int iRow = 4; //We start at row 5

            int iCol = 0;
            foreach (DataRow r in stored_main.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in stored_main.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= monthsdiff * 3 + 1)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }
        }

        private void ExportToExcelCvs()
        {
            string stitle = string.Empty;
            stitle = "Coefficient of Variations for Selected Type of Construction";

            string subtitle = "(Percent.)";

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "MONTHLY BEA CV";

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            xlWorkSheet.Activate();
            //xlWorkSheet.Application.ActiveWindow.SplitColumn = 1;
            //xlWorkSheet.Application.ActiveWindow.SplitRow = 5;
            //xlWorkSheet.Application.ActiveWindow.FreezePanes = true;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 73]);
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
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 73]);
            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Bold = true;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 73]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 36;

            DateTime secondDate = new DateTime((cur_year - 2), 1, 1); //DateTime.Now;
            DataTable dcv = data_object.GetSLBeaMonCVsTable();

            xlWorkSheet.Cells[4, 1] = "Type of Construction";

            for (int i = 1; i <= 24; i++)
            {
                DateTime nextMonth = secondDate.AddMonths(i - 1);
                string nMonth = nextMonth.Year.ToString() + nextMonth.Month.ToString("00");

                int j = 3 * i - 1;
                xlWorkSheet.Cells[4, j] = "State \n" + nMonth;

                j = 3 * i;
                xlWorkSheet.Cells[4, j] = "Local \n" + nMonth;

                j = 3 * i + 1;
                xlWorkSheet.Cells[4, j] = "Total \n" + nMonth;

            }

          //Setup the column header row (row 5)

          //Set the font size, text wrap of columns and format for the entire worksheet
          ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 30;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 2; i <= 24 * 3 + 1; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 10;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0.0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }

            ////Populate rest of the data. Start at row[5] 
            int iRow = 4; //We start at row 5

            int iCol = 0;
            foreach (DataRow r in dcv.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dcv.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= 24 * 3 + 1)
                    {
                        if (r[c.ColumnName].ToString() == "")
                            xlWorkSheet.Cells[iRow, iCol] = " ";
                        else
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                    }
                }
            }
        }

        private void frmSpecBEASLMon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!show_subtc && dgData.CurrentRow.Index != 0)
            {
                //go to subtc 
                show_subtc = true;

                subtc = dgData.CurrentRow.Cells[0].Value.ToString().Substring(0, 2);

                //Get data
                DataTable tblFiltered = display_table.AsEnumerable()
               .Where(row => row.Field<String>("tc2") == subtc)
               .OrderBy(row => row.Field<String>("newtc"))
               .CopyToDataTable();

                dgData.DataSource = tblFiltered;

                lbllabel.Text = "Double Click any Line to Restore";

            }
            else if (show_subtc)
            {
                //go back to main tc 
                show_subtc = false;
                subtc = "";

                //Get data
                DataTable tblFiltered = display_table.AsEnumerable()
                                   .Where(row => row.Field<int>("ddown") <= 2)
               .OrderBy(row => row.Field<String>("newtc"))
               .CopyToDataTable();

                dgData.DataSource = tblFiltered;
                lbllabel.Text = "Double Click a Line to Expand";
            }

            //set up column header
            SetColumnHeader();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = lblFactor.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "BEA SL Month Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgData);
           
            Cursor.Current = Cursors.Default;
        }
    }
}
