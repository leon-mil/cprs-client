/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTableSeasonalFactor.cs
Programmer    : Christine Zhang
Creation Date : Aug. 17 2017
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create Table seasonal factor screen to view related data
                and create excels
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :6/23/2020
Modified By   :Christine
Keyword       :
Change Request:
Description   :get current survey month from SAATOT table instead of VIPSADJ table
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
    public partial class frmTableSeasonalFactor : frmCprsParent
    {
        private TableSeasonalFactorData data_object;
        private frmMessageWait waiting;
        private string saveFilename;
        private string sdate;

        //permonth variables
        private string pm1;
        private string pm2;
        private string pm3;
        private string pm4;
       
        private string curmons;
        private string pmon1;
        private string pmon2;
        private string pmon3;
        private string pmon4;
       
        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private delegate void ShowMessageDelegate();

        public frmTableSeasonalFactor()
        {
            InitializeComponent();
        }

        private void frmTableSeasonalFactor_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            //define data object;
            data_object = new TableSeasonalFactorData();

            //get survey month
            sdate = GeneralDataFuctions.GetMaxMonthDateinAnnTable();

            //Check SAATOT table to see survey month data was loaded or not
            if (GeneralDataFuctions.CheckSurveyMonthDataExist(sdate))
            {

                //Convert sdate to datatime
                var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
                pm1 = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
                pm2 = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
                pm3 = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
                pm4 = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);

                //get abbreviate month name
                curmons = (dt.ToString("MMM yyyy", CultureInfo.InvariantCulture));
                pmon1 = (dt.AddMonths(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
                pmon2 = (dt.AddMonths(-2)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
                pmon3 = (dt.AddMonths(-3)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
                pmon4 = (dt.AddMonths(-4)).ToString("MMM yyyy", CultureInfo.InvariantCulture);

                rdt.Checked = true;

                GetData();
            }
            else
            {
                BeginInvoke(new ShowMessageDelegate(ShowMessage));
            }
        }

        private void ShowMessage()
        {
            /*show message if no data exist */
            MessageBox.Show("Survey Month data hasn't been loaded. Cannot show the release data");
            this.Close();
            frmHome fH = new frmHome();
            fH.Show();
        }

        private void GetData()
        {
            string survey_type;
            if (rdt.Checked)
                survey_type = "T";
            else if (rdv.Checked)
                survey_type = "V";
            else if (rdp.Checked)
                survey_type = "P";
            else if (rds.Checked)
                survey_type = "S";
            else
                survey_type = "F";

            SetUpdateTitle(survey_type);
            
            //get the data
            dgData.DataSource = data_object.GetTableSeasonalFactorData(survey_type, sdate, pm1, pm2, pm3, pm4);
            
            SetColumnHeader();

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //set up title based on survey type
        private void SetUpdateTitle(string survey_type)
        {
            if (survey_type == "T")
                lblTitle2.Text = "Value of Total Construction Put in Place";
            else if (survey_type == "V")
                lblTitle2.Text = "Value of Private Construction Put in Place";
            else if (survey_type == "P")
                lblTitle2.Text = "Value of Public Construction Put in Place";
            else if (survey_type == "S")
                lblTitle2.Text = "Value of State and Local Construction Put in Place";
            else
                lblTitle2.Text = "Value of Federal Construction Put in Place";
        }

        //set column header
        private void SetColumnHeader()
        {
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            
            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 300;
            dgData.Columns[1].HeaderText = curmons;
            dgData.Columns[1].Width = 125;
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].HeaderText = pmon1;
            dgData.Columns[2].Width = 125;
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].HeaderText = pmon2;
            dgData.Columns[3].Width = 125;
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].HeaderText = pmon3;
            dgData.Columns[4].Width = 125;
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = pmon4;
            dgData.Columns[5].Width = 125;
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

        }

        private void frmTableSeasonalFactor_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "EXIT");
        }

        private void rdt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt.Checked)
                GetData();
        }

        private void rdv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdv.Checked)
                GetData();
        }

        private void rdp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdp.Checked)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitle = lblTitle2.Text;
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Seasonal Factors Table";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            //resize column
            dgData.Columns[0].Width = 150;
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 300;
           
            Cursor.Current = Cursors.Default;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "totalsf.xlsx";

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
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            string stitle;
            string subtitle;
            string sfilename;
            string ssheetname;

            DataTable dtab;

            //create sheet tables
            dtab = data_object.GetTableSeasonalFactorData("T", sdate, pm1, pm2, pm3, pm4);
            stitle = "Factors Used to Seasonally Adjust Estimates of the";
            subtitle = "Value of Total Construction Put in Place";
            sfilename = dir + "\\totalsf.xlsx";
            ssheetname = "Total SF";
            ExportToExcel(sfilename, dtab, stitle, subtitle, "T", ssheetname);

            dtab = data_object.GetTableSeasonalFactorData("V", sdate, pm1, pm2, pm3, pm4);
            stitle = "Factors Used to Seasonally Adjust Estimates of the";
            subtitle = "Value of Private Construction Put in Place";
            sfilename = dir + "\\privsf.xlsx";
            ssheetname = "Private SF";
            ExportToExcel(sfilename, dtab, stitle, subtitle, "V", ssheetname);

            dtab = data_object.GetTableSeasonalFactorData("P", sdate, pm1, pm2, pm3, pm4);
            stitle = "Factors Used to Seasonally Adjust Estimates of the";
            subtitle = "Value of Public Construction Put in Place";
            sfilename = dir + "\\pubsf.xlsx";
            ssheetname = "Public SF";
            ExportToExcel(sfilename, dtab, stitle, subtitle, "P", ssheetname);

            dtab = data_object.GetTableSeasonalFactorData("S", sdate, pm1, pm2, pm3, pm4);
            stitle = "Factors Used to Seasonally Adjust Estimates of the";
            subtitle = "Value of State and Local Construction Put in Place";
            sfilename = dir + "\\statesf.xlsx";
            ssheetname = "State SF";
            ExportToExcel(sfilename, dtab, stitle, subtitle, "S", ssheetname);

            dtab = data_object.GetTableSeasonalFactorData("F", sdate, pm1, pm2, pm3, pm4);
            stitle = "Factors Used to Seasonally Adjust Estimates of the";
            subtitle = "Value of Federal Construction Put in Place";
            sfilename = dir + "\\fedsf.xlsx";
            ssheetname = "Fed SF";
            ExportToExcel(sfilename, dtab, stitle, subtitle, "F", ssheetname);

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");
        }

        //create excels for total
        private void ExportToExcel(string sfilename, DataTable dt, string stitle, string subtitle, string survey_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            string smon = sdate.Substring(4, 2);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;

            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 11;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "F"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "F"]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange2.Font.Size = 9;
            titleRange2.Font.Bold = true;
            
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title3
            xlWorkSheet.Cells[3, 1] = "(percent)";
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, "F"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            //add an empty row

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[5, 1] = "Type of Construction:";
            xlWorkSheet.Cells[5, 2] = cmm + "p";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 2]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[5, 3] = pmm1 + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 3], xlWorkSheet.Cells[5, 3]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[5, 4] = pmm2 + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 4], xlWorkSheet.Cells[5, 4]);
            cellRange.Characters[9, 1].Font.Superscript = true;

            if (smon == "05")
            {
                xlWorkSheet.Cells[5, 5] = pmm3 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 5], xlWorkSheet.Cells[5, 5]);
                cellRange.Characters[9, 1].Font.Superscript = true;
                xlWorkSheet.Cells[5, 6] = pmm4 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 6], xlWorkSheet.Cells[5, 6]);
                cellRange.Characters[9, 1].Font.Superscript = true;
            }
            else
            {
                xlWorkSheet.Cells[5, 5] = pmm3;
                xlWorkSheet.Cells[5, 6] = pmm4;
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "F"]);

            //Set the header row hight
            headerRange.RowHeight = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            strColumns = new string[] { "A", "B", "C", "D", "E", "F"};
           
            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.50;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 28.00;
                }

            }

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

                //bold rows 
                if (survey_type == "T")
                {
                    if (iRow == 7 || iRow == 29 || iRow == 46)
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
                else if (survey_type == "V")
                {
                    if (iRow == 7 || iRow == 9|| iRow == 13 || iRow == 15 || iRow ==17 || iRow ==21 || iRow == 41 || iRow ==46 || iRow ==56 || iRow ==61 || iRow ==69 ||iRow ==73 ||iRow==75 ||iRow ==78 )
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        if (iRow == 46)
                            xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                }
                else if (survey_type == "P")
                {
                    if (iRow == 7 || iRow == 25 || iRow == 43)
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
                else if (survey_type == "S")
                {
                    if (iRow == 7 || iRow == 9 || iRow == 12 || iRow == 14 || iRow == 16 || iRow == 20 || iRow == 25 || iRow == 38 || iRow == 45 || iRow == 53 || iRow == 63 || iRow == 65 || iRow == 71 || iRow == 79 || iRow == 84)
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                        if (iRow == 63)
                            xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                }
                else
                {
                    if (iRow == 7)
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "F"]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            //repeat title header 
            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$6";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 40;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 0;
            xlWorkSheet.PageSetup.LeftMargin = 80;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
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
    }
}
