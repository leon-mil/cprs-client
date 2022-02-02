/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTableStandardErrors.cs
Programmer    : Christine Zhang
Creation Date : Aug. 31 2017
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create Table Standard Errors screen to view related data
                and create excels
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :8/1/2019
Modified By   :Christine
Keyword       :
Change Request:
Description   :Add an annual button to create annual tables
***********************************************************************
Modified Date :6/23/2020
Modified By   :Christine
Keyword       :
Change Request:
Description   :get current survey month from SAATOT table instead of VIPSADJ table
***********************************************************************/
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
    public partial class frmTableStandardErrors : frmCprsParent
    {
        private TableStandardErrorsData data_object;
        private frmMessageWait waiting;
        private string saveFilename;
        private string sdate;
        private string table_type = string.Empty;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private delegate void ShowMessageDelegate();
        public frmTableStandardErrors()
        {
            InitializeComponent();
        }

        private void frmTableStandardErrors_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            //define data object;
            data_object = new TableStandardErrorsData();

            //get survey month from SAATOT
            sdate = GeneralDataFuctions.GetMaxMonthDateinAnnTable();

            //set default to total
            rdt.Checked = true;

            GetData();

            //only May and Dec. can show the annual table button
            int curmon = Convert.ToInt16(sdate.Substring(4, 2));
            if (curmon == 5 || curmon == 12)
                btnAnnual.Enabled = true;
            else
                btnAnnual.Enabled = false;            
        }
   
        private void GetData()
        {
            string survey_type;
            if (rdt.Checked)
                survey_type = "T";
            else if (rdv.Checked)
                survey_type = "V";
            else if (rds.Checked)
                survey_type = "S";
            else
                survey_type = "F";

            //get data
            dgData.DataSource = data_object.GetTableStandardErrorsData(survey_type, sdate);

           SetColumnHeader(survey_type);

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SetColumnHeader(string survey_type)
        {
            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 260;
            dgData.Columns[1].HeaderText = "Monthly estimate";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[1].Width = 150;
            dgData.Columns[2].HeaderText = "Year-to-date estimate";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].Width = 150;
            dgData.Columns[3].HeaderText = "Year-to-date change";
            dgData.Columns[3].Width = 150;
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].HeaderText = "Month-to-month change";
            dgData.Columns[4].Width = 150;
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = "Month-to-month change from same month in prior year";
            if (survey_type == "F")
                dgData.Columns[5].Width = 200;
            else
                dgData.Columns[5].Width = 180;
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void rdv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdv.Checked)
                GetData();
        }

        private void rds_CheckedChanged(object sender, EventArgs e)
        {
            if (rds.Checked)
                GetData();
        }

        private void rdf_CheckedChanged(object sender, EventArgs e)
        {
            if (rdf.Checked)
                GetData();
        }

        private void rdt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt.Checked)
                GetData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.SubTitle = label2.Text;
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Standard Errors Table";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            //resize column
            dgData.Columns[0].Width = 150;

            printer.PrintDataGridViewWithoutDialog(dgData);

            dgData.Columns[0].Width = 260;
            
            Cursor.Current = Cursors.Default;
        }

        //run application
        public void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "totalcv.xls";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            table_type = "Month";

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //disable form
            this.Enabled = false;

            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            string sfilename;
            string ssheetname;

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            DataTable dtab;

            if (table_type == "Month")
            {
                //create sheet tables
                dtab = data_object.GetTableStandardErrorsData("T", sdate);
                sfilename = dir + "\\totalcv.xls";
                ssheetname = "Total CV";
                ExportToExcel(sfilename, dtab, "T", ssheetname);

                dtab = data_object.GetTableStandardErrorsData("V", sdate);
                sfilename = dir + "\\privcv.xls";
                ssheetname = "Private CV";
                ExportToExcel(sfilename, dtab, "V", ssheetname);

                dtab = data_object.GetTableStandardErrorsData("S", sdate);
                sfilename = dir + "\\statecv.xls";
                ssheetname = "State CV";
                ExportToExcel(sfilename, dtab, "S", ssheetname);

                dtab = data_object.GetTableStandardErrorsData("F", sdate);
                sfilename = dir + "\\fedcv.xls";
                ssheetname = "Fed CV";
                ExportToExcel(sfilename, dtab, "F", ssheetname);
            }
            else
            {
                int month2 = Convert.ToInt32(sdate.Substring(4, 2));
                string syear;
                if (month2 == 12)
                    syear = Convert.ToInt16(sdate.Substring(0, 4)).ToString();
                else
                    syear = (Convert.ToInt16(sdate.Substring(0, 4)) - 1).ToString();

                //create sheet tables
                dtab = data_object.GetAnnTableStandardErrorsData("T", syear);
                sfilename = dir + "\\anntotcv.xls";
                ssheetname = "Total CV";
                AnnExportToExcel(sfilename, dtab, "T", ssheetname);

                dtab = data_object.GetAnnTableStandardErrorsData("V", syear);
                sfilename = dir + "\\annprivcv.xls";
                ssheetname = "Private CV";
                AnnExportToExcel(sfilename, dtab, "V", ssheetname);

                dtab = data_object.GetAnnTableStandardErrorsData("S", syear);
                sfilename = dir + "\\annstcv.xls";
                ssheetname = "State CV";
                AnnExportToExcel(sfilename, dtab, "S", ssheetname);

                dtab = data_object.GetAnnTableStandardErrorsData("F", syear);
                sfilename = dir + "\\annfedcv.xls";
                ssheetname = "Fed CV";
                AnnExportToExcel(sfilename, dtab, "F", ssheetname);
            }

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        //// to kill the EXCELsheet file process from process Bar
        //private void KillSpecificExcelFileProcess()
        //{
        //    foreach (Process clsProcess in Process.GetProcesses())
        //        if (clsProcess.ProcessName.Equals("EXCEL"))  //Process Excel?
        //            clsProcess.Kill();
        //}

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");
        }

        //create excels for total
        private void ExportToExcel(string sfilename, DataTable dt, string survey_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            
            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;

            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 11;

            //Add a title
            xlWorkSheet.Cells[1, 1] = "Coefficients of Variation and Standard Errors by Type of Construction";

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "F"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.Font.Name = "Arial";

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = "(percent)";

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "F"]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
            
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add coefficient of variation, standard error
            xlWorkSheet.Cells[4, 2] = "Coefficient of variation";

            //Span the title across columns B through C
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "B"], xlWorkSheet.Cells[4, "C"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.Font.Bold = false;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            xlWorkSheet.Cells[4, 4] = "Standard error";

            //Span the title across columns H through J
            titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "D"], xlWorkSheet.Cells[4, "F"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.Font.Bold = false;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "E"]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            Microsoft.Office.Interop.Excel.Range tRange2 = xlApp.get_Range(xlWorkSheet.Cells[4, "F"], xlWorkSheet.Cells[4, "F"]);
            Microsoft.Office.Interop.Excel.Borders border2 = tRange2.Borders;
            border2[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border2[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border2[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border2[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[5, 1] = "\n            Type of Construction\n";
            xlWorkSheet.Cells[5, 2] = "Monthly\nestimate";
            xlWorkSheet.Cells[5, 3] = "Year-to-date\nestimate";
            xlWorkSheet.Cells[5, 4] = "Year-to-date\nchange";
            xlWorkSheet.Cells[5, 5] = "Month-to-month\nchange";
            xlWorkSheet.Cells[5, 6] = "Month-to-month\nchange from same month in prior year";

            //Setup the column header row (row 5)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "E"]);

            //Set the header row fonts bold
            headerRange.RowHeight = 33;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Microsoft.Office.Interop.Excel.Borders border3 = headerRange.Borders;
            border3[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border3[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border3[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border3[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            Microsoft.Office.Interop.Excel.Range headerRange2 = xlApp.get_Range(xlWorkSheet.Cells[5, "F"], xlWorkSheet.Cells[5, "F"]);
            Microsoft.Office.Interop.Excel.Borders border4 = headerRange2.Borders;
            border4[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border4[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border4[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border4[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                ////For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[dt.Rows.Count+6, s]);
                writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                border = writeRange.Borders;
                
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 14.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                }
            }

            Microsoft.Office.Interop.Excel.Range lineRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "A"]);
            border = lineRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            Microsoft.Office.Interop.Excel.Range lineRange2 = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "A"]);
            border = lineRange2.Borders;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //Populate rest of the data. Start at row[6] 
            int iRow = 6; //We start at row 6

            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                }

                if (survey_type == "T")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if (iRow == 7 || iRow == 29 || iRow == 46)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        //set up Total Private Construction with subscript 1
                        if (iRow == 29)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                            Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                            cellRange0.RowHeight = 12;
                            cellRange0.Characters[27, 1].Font.Superscript = true;
                        }
                        else if (iRow == 46)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Public Construction2";
                            Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                            cellRange1.RowHeight = 12;
                            cellRange1.Characters[26, 1].Font.Superscript = true;
                           
                            xlWorkSheet.HPageBreaks.Add(cellRange);
                        }
                    }
                }
                else if (survey_type == "V")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if (iRow == 7 || iRow == 9 || iRow == 13 || iRow == 15 || iRow == 17 || iRow == 21 || iRow == 41 || iRow == 46 || iRow == 56 || iRow == 61 || iRow == 69 || iRow == 73 || iRow == 75 || iRow == 78)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        //set up Total Private Construction with subscript 1
                        if (iRow == 7)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                            Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                            cellRange0.Characters[27, 1].Font.Superscript = true;
                        }
                        else if (iRow == 9)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Residential (inc. Improvements)2";
                            Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                            cellRange1.Characters[32, 1].Font.Superscript = true;
                        }
                        else if (iRow == 61)
                            xlWorkSheet.HPageBreaks.Add(cellRange);
                    }

                }
                else if (survey_type == "S")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if (iRow == 7 || iRow == 9 || iRow == 12 || iRow == 14 || iRow == 16 || iRow == 20 || iRow == 25 || iRow == 38 || iRow == 45 || iRow == 53 || iRow == 63 || iRow == 65 || iRow == 71 || iRow == 79 || iRow == 84)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                        if (iRow == 63)
                          xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                }
                else
                {
                    if (iRow == 7)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
            }

                //add text after grid
                if (survey_type == "T")
                {
                    Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "F"]);
                    footRange1.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 2, 1] = "NA Not applicable";

                    Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "F"]);
                    footRange2.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of private construction not shown separately:";
                    footRange2.Characters[1, 1].Font.Superscript = true;

                    Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "F"]);
                    footRange3.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 5, 1] = "public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

                    Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "F"]);
                    footRange4.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 7, 1] = "2Includes the following categories of public construction not shown separately:";
                    footRange4.Characters[1, 1].Font.Superscript = true;
                    Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "F"]);
                    footRange5.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 8, 1] = "lodging, religious, communication, and manufacturing.";
                }
                else if (survey_type == "V")
                {
                    Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "F"]);
                    footRange1.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 2, 1] = "NA Not applicable";

                    Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "F"]);
                    footRange2.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 4, 1] = "1Total private construction includes the following categories of construction not shown separately:";
                    footRange2.Characters[1, 1].Font.Superscript = true;
                    Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "F"]);
                    footRange3.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 5, 1] = "public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

                    Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "F"]);
                    footRange4.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 7, 1] = "2Private residential improvements does not include expenditures to rental, vacant, or seasonal properties.";
                    footRange4.Characters[1, 1].Font.Superscript = true;

                }
                else if (survey_type == "S")
                {
                    Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "F"]);
                    footRange1.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 2, 1] = "Note: Total state and local construction includes the following categories of construction not shown separately:";
                    Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, "F"]);
                    footRange2.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 3, 1] = "lodging, religious, communication, and manufacturing.";
                }
                else
                {
                    Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "F"]);
                    footRange1.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 2, 1] = "Note: Total federal construction includes the following categories of construction not shown separately:";
                    Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, "F"]);
                    footRange2.Merge(Type.Missing);
                    xlWorkSheet.Cells[iRow + 3, 1] = "lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";
                }

                // Page Setup
                //Set page orientation to landscape
                xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
                xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$6";

                //Set margins
                xlWorkSheet.PageSetup.TopMargin = 40;
                xlWorkSheet.PageSetup.RightMargin = 20;
                xlWorkSheet.PageSetup.BottomMargin = 20;
                xlWorkSheet.PageSetup.LeftMargin = 20;

                //Set gridlines
                xlWorkBook.Windows[1].DisplayGridlines = false;
                xlWorkSheet.PageSetup.PrintGridlines = false;

                // Save file & Quit application
                xlApp.DisplayAlerts = false; //Supress overwrite request
                xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);         
                xlWorkBook.Close(true, misValue, misValue);
           
                try
                {
                    GeneralFunctions.releaseObject(xlWorkSheet);
                    GeneralFunctions.releaseObject(xlWorkBook);
                    xlApp.Quit();
                }
                catch { }
                finally
                {
                    GeneralFunctions.releaseObject(xlApp);
                }
        }

        private void AnnExportToExcel(string sfilename, DataTable dt, string survey_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;

            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 11;

            //Add a title
            xlWorkSheet.Cells[1, 1] = "Coefficients of Variation and Standard Errors by Type of Construction";

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "C"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.Font.Name = "Arial";

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = "(percent)";

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "C"]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title

            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add coefficient of variation, standard error
            xlWorkSheet.Cells[4, 2] = "Coefficient of variation";

            xlWorkSheet.Cells[4, 3] = "Standard error";

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "B"]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            Microsoft.Office.Interop.Excel.Range tRange2 = xlApp.get_Range(xlWorkSheet.Cells[4, "C"], xlWorkSheet.Cells[4, "C"]);
            Microsoft.Office.Interop.Excel.Borders border2 = tRange2.Borders;
            border2[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border2[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border2[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border2[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[5, 1] = "\n            Type of Construction\n";
            xlWorkSheet.Cells[5, 2] = "Annual\nestimate";
            xlWorkSheet.Cells[5, 3] = "Year-to-date\nchange";

            //Setup the column header row (row 5)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "C"]);

            //Set the header row fonts bold
            headerRange.RowHeight = 33;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            Microsoft.Office.Interop.Excel.Borders border3 = headerRange.Borders;
            border3[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border3[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border3[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border3[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            Microsoft.Office.Interop.Excel.Range headerRange2 = xlApp.get_Range(xlWorkSheet.Cells[5, "C"], xlWorkSheet.Cells[5, "C"]);
            Microsoft.Office.Interop.Excel.Borders border4 = headerRange2.Borders;
            border4[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border4[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border4[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border4[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                ////For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[dt.Rows.Count + 6, s]);
                writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                border = writeRange.Borders;

                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 18.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                }
            }

            Microsoft.Office.Interop.Excel.Range lineRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "A"]);
            border = lineRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            Microsoft.Office.Interop.Excel.Range lineRange2 = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "A"]);
            border = lineRange2.Borders;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            //Populate rest of the data. Start at row[6] 
            int iRow = 6; //We start at row 6

            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                }

                if (survey_type == "T")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if (iRow == 7 || iRow == 29 || iRow == 49)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
                else if (survey_type == "V")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if (iRow == 7 || iRow == 9 || iRow == 14 || iRow == 16 || iRow == 18 || iRow == 22 || iRow == 44 || iRow == 49 || iRow == 59 || iRow == 63 ||iRow == 64 || iRow == 66 || iRow == 74 || iRow == 79 || iRow == 81 || iRow == 87 || iRow == 89 || iRow == 91)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        if (iRow == 63)
                            xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                    
                }
                else if (survey_type == "S")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if (iRow == 7 || iRow == 9 || iRow == 12 || iRow == 14 || iRow == 16 || iRow == 21 || iRow == 26 || iRow == 43 || iRow == 50 || iRow == 58 || iRow == 69 || iRow == 70 || iRow == 74 || iRow == 84 || iRow == 92 || iRow == 100)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                        if (iRow == 69)
                            xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                }
                else
                {
                    if (iRow == 7)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
            }

            //add text after grid
            if (survey_type != "F")
            {
                Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "C"]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 2, 1] = "NA Not applicable";
            }
            
            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;
            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$6";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 20;
            xlWorkSheet.PageSetup.RightMargin = 100;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 100;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            try
            {
                GeneralFunctions.releaseObject(xlWorkSheet);
                GeneralFunctions.releaseObject(xlWorkBook);
                xlApp.Quit();
            }
            catch { }
            finally
            {
                GeneralFunctions.releaseObject(xlApp);
            }

        }

        private void btnAnnual_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "anntotcv.xls";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            table_type = "Annual";

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //disable form
            this.Enabled = false;

            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }
    }
}
