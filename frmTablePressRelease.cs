/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTablePressRelease.cs
Programmer    : Christine Zhang
Creation Date : Jul. 25 2017
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create Table Press release screen to view related data
                and create excels
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date : 7/16/2019
Modified By   : Christine Zhang
Keyword       :
Change Request: CR # 3330
Description   :for 2019 only, need allow table 4 for July survey month
***********************************************************************
Modified Date : 6/23/2020
Modified By   : Christine Zhang
Keyword       :
Change Request: None
Description   : get current survey month from SAATOT table instead of VIPSADJ table
***********************************************************************
Modified Date : 11/18/2020
Modified By   : Kevin Montgomery
Keyword       : 20201118kjm
Change Request: CR # 7732
Description   : Update footnotes on Tables 1,2 and 4
                Move footnote 2  to footnote 3
                Add new footnote 2 - Includes private residential improvements
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
    public partial class frmTablePressRelease : frmCprsParent
    {

        private TablePressReleaseData data_object;
        private frmMessageWait waiting;
        private string saveFilename;
        private string sdate;
        
        //permonth variables
        private string pm1;
        private string pm2;
        private string pm3;
        private string pm4;
        private string pm5;
        private string mon1;
        private string mon2;
        private string MYD;
        private string curmons; 
        private string pmon1;
        private string pmon2;
        private string pmon3;
        private string pmon4;
        private string pmon5;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private delegate void ShowMessageDelegate();

        public frmTablePressRelease()
        {
            InitializeComponent();
        }

        private void frmPressRelease_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            //define data object;
            data_object = new TablePressReleaseData();

            //get survey month
            sdate = GeneralDataFuctions.GetMaxMonthDateinAnnTable();

            //set up table 4
            string smon = sdate.Substring(4, 2);

            if (smon == "12" || smon == "01" || smon == "02" || smon == "05")
                rdt4.Visible = true;
            else
                rdt4.Visible = false;
            

            rdt1.Checked = true;

            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
            pm1 = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            pm2 = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            pm3 = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            pm4 = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            pm5 = (dt.AddYears(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
           
            //get abbreviate month name
            curmons = (dt.ToString("MMM yyyy", CultureInfo.InvariantCulture));
            pmon1 = (dt.AddMonths(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            pmon2 = (dt.AddMonths(-2)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            pmon3 = (dt.AddMonths(-3)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            pmon4 = (dt.AddMonths(-4)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            pmon5 = (dt.AddYears(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);

            //Press release date
            DateTime sDate = dt.AddMonths(+2);

            lblt1.Text = "Percent Change " + curmons + " from -";

            //Get frist work day
            DateTime firstworkday = GeneralFunctions.GetFirstBusinessDay(sDate.Year, sDate.Month);

            MYD = DateTimeFormatInfo.CurrentInfo.GetMonthName(firstworkday.Month) + " " + firstworkday.Day + ", " + sDate.Year;

            //set up month variable
            string syear = sdate.Substring(0, 4);
            if (smon == "12")
            {
                mon1 = syear + smon;
                mon2 = (Convert.ToInt32(syear) - 1).ToString() + smon;
            }
            else
            {
                mon1 = (Convert.ToInt32(syear) - 1).ToString() + "12";
                mon2 = (Convert.ToInt32(syear) - 2).ToString() + "12";
            }
                
            GetData();

        }

        //Get Data
        private void GetData()
        {
            int tno = 0;

            if (rdt1.Checked)
                tno = 1;
            else if (rdt2.Checked)
                tno = 2;
            else if (rdt3.Checked)
                tno = 3;
            else if (rdt4.Visible && rdt4.Checked)
                tno = 4;

            //set up title
            SetUpdateTitle(tno);

            //get data
            if (tno==1 || tno ==2 || tno ==3)
                dgData.DataSource= data_object.GetTableData(tno, sdate, pm1, pm2, pm3, pm4, pm5);
            else if (tno ==4)
                dgData.DataSource = data_object.GetTable4Data(mon1, mon2);

            //set datagrid column 
            SetColumnHeader(tno);

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SetUpdateTitle(int tno)
        {
            if (tno ==1)
            {
                lblTitle.Text = "Table 1. Value of Construction Put in Place in the United States";
                lblTitle2.Text = "Seasonally Adjusted Annual Rate";
                lblTitle3.Text = "(Millions of Dollars. Details may not add to totals due to rounding.)";
                lblTitle3.Visible = true;
                lblTitle2.Visible = true;
            }
            else if (tno == 2)
            {
                lblTitle.Text = "Table 2. Value of Construction Put in Place in the United States";
                lblTitle2.Text = "Not Seasonally Adjusted";
                lblTitle3.Text = "(Millions of Dollars. Details may not add to totals due to rounding.)";
                lblTitle3.Visible = true;
                lblTitle2.Visible = true;
            }
            else if (tno ==3)
            {
                lblTitle.Text = "Table 3. Coefficients of Variation and Standard Errors by Type of Construction";
                lblTitle2.Text = "(Percent)";
                lblTitle3.Visible = false;
                lblTitle2.Visible = true;
            }
            else
            {
                lblTitle.Text = "Table 4. Annual Value of Construction Put in Place in the United States";
                lblTitle3.Text = "(Millions of Dollars. Details may not add to totals due to rounding.)";
                lblTitle3.Visible = true;
                lblTitle2.Visible = false;
            }
        }


        private void SetColumnHeader(int tno)
        {
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            if (tno == 1)
            {
                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 320;
                dgData.Columns[1].HeaderText = curmons;
                dgData.Columns[1].Width = 100;
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = pmon1;
                dgData.Columns[2].Width = 100;
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = pmon2;
                dgData.Columns[3].Width = 100;
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = pmon3;
                dgData.Columns[4].Width = 100;
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = pmon4;
                dgData.Columns[5].Width = 95;
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = pmon5;
                dgData.Columns[6].Width = 95;
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].HeaderText = pmon1;
                dgData.Columns[7].Width = 95;
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[8].HeaderText = pmon5;
                dgData.Columns[8].Width = 96;
                dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                lblt1.Visible = true;
                lblt2.Visible = false;
                lblt31.Visible = false;
                lblt32.Visible = false;
            }
            else if (tno == 2)
            {
                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 280;
                dgData.Columns[1].HeaderText = curmons;
                dgData.Columns[1].Width = 90;
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = pmon1;
                dgData.Columns[2].Width = 90;
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = pmon2;
                dgData.Columns[3].Width = 90;
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = pmon3;
                dgData.Columns[4].Width = 90;
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = pmon4;
                dgData.Columns[5].Width = 90;
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = pmon5;
                dgData.Columns[6].Width = 90;
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].HeaderText = curmons;
                dgData.Columns[7].Width = 90;
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[8].HeaderText = pmon5;
                dgData.Columns[8].Width = 90;
                dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[9].HeaderText = "Percent Change";
                dgData.Columns[9].Width = 100;
                dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                lblt1.Visible = false;
                lblt2.Visible = true;
                lblt31.Visible = false;
                lblt32.Visible = false;
            }
            else if (tno == 3)
            {
                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 282;
                dgData.Columns[1].HeaderText = "Monthly estimate";
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[1].Width = 160;
                dgData.Columns[2].HeaderText = "Year-to-date estimate";
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].Width = 160;
                dgData.Columns[3].HeaderText = "Year-to-date change";
                dgData.Columns[3].Width = 160;
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = "Month-to-month change";
                dgData.Columns[4].Width = 160;
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = "Month-to-month change from prev. year";
                dgData.Columns[5].Width = 180;
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                lblt1.Visible = false;
                lblt2.Visible = false;
                lblt31.Visible = true;
                lblt32.Visible = true;
            }
            else
            {
                string smon = sdate.Substring(4, 2);
                string syear = sdate.Substring(0, 4);
                string pyear;
                string p1year;
                if (smon == "12")
                {
                    pyear = syear;
                    p1year = (Convert.ToInt16(syear) - 1).ToString(); 
                }
                else
                {
                    pyear = (Convert.ToInt16(syear) - 1).ToString();
                    p1year = (Convert.ToInt16(syear) - 2).ToString();
                }

                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 300;
                dgData.Columns[1].HeaderText = pyear;
                dgData.Columns[1].Width = 200;
                dgData.Columns[2].HeaderText = p1year;
                dgData.Columns[2].Width = 200;
                dgData.Columns[3].HeaderText = "Percent change";
                dgData.Columns[3].Width = 200;
                dgData.Columns[4].HeaderText = "Coefficient of Variation";
                dgData.Columns[4].Width = 200;

                lblt1.Visible = false;
                lblt2.Visible = false;
                lblt31.Visible = false;
                lblt32.Visible = false;
            }
        }

        private void rdt1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt1.Checked)
                GetData();
        }


        private void rdt2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt2.Checked )
                GetData();
        }

        private void rdt3_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt3.Checked)
                GetData();
        }

        private void rdt4_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt4.Checked)
                GetData();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text + " " + lblTitle2.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            
            printer.SubTitle = lblTitle3.Text;
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Press Release Table";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            //resize column
            dgData.Columns[0].Width = 150;

            printer.PrintDataGridViewWithoutDialog(dgData);

            if (rdt1.Checked)
                dgData.Columns[0].Width = 320;
            else if (rdt2.Checked)
                dgData.Columns[0].Width = 280;
            else if (rdt3.Checked)
                dgData.Columns[0].Width = 282;
            else if (rdt4.Visible && rdt4.Checked)
                dgData.Columns[0].Width = 300;

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
            saveFileDialog1.FileName = "release.xls";
            
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

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\release.xls";

            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            string stitle;
            string subtitle;
            DataTable dtab;

            //create sheet tables
            if (rdt4.Visible)
            {
                dtab = data_object.GetTable4Data(mon1, mon2);
                stitle = "Table 4. Annual Value of Construction Put in Place in the United States";
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                ExportToExcel4(dtab, stitle, subtitle);
            }

            dtab = data_object.GetTableData(3, sdate, pm1, pm2, pm3, pm4, pm5);
            stitle = "Table 3. Coefficients of Variation and Standard Errors by Type of Construction";
            subtitle = "(Percent)";
            ExportToExcel3(dtab, stitle, subtitle);

            dtab = data_object.GetTableData(2, sdate, pm1, pm2, pm3, pm4, pm5);
            stitle = "Table 2. Value of Construction Put in Place in the United States, Not Seasonally Adjusted";
            subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
            ExportToExcel2(dtab, stitle, subtitle);

            dtab = data_object.GetTableData(1, sdate, pm1, pm2, pm3, pm4, pm5);
            stitle = "Table 1. Value of Construction Put in Place in the United States, Seasonally Adjusted Annual Rate";
            subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
            ExportToExcel1(dtab, stitle, subtitle);

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

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("File has been created");
        }

        private void ExportToExcel1(DataTable dt, string stitle, string subtitle)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "Table1";

            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 10;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "I"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.Characters[1, 8].Font.Bold = false;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "I"]);
            titleRange2.Merge(Type.Missing);

            //add an empty row

            //add percent change
            xlWorkSheet.Cells[4, 8] = "Percent change\n" + curmons + " from -";

            //Span the title across columns H through I
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, "I"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.Font.Bold = false;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange3.RowHeight = 22;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "I"]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

            string smon = sdate.Substring(4, 2);

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[5, 1] = "            Type of Construction\n";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "A"]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[5, 2] = cmm + "p";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 2]);
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
                xlWorkSheet.Cells[5, 7] = pmm5 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 7], xlWorkSheet.Cells[5, 7]);
                cellRange.Characters[9, 1].Font.Superscript = true;
            }
            else
            {
                xlWorkSheet.Cells[5, 5] = pmm3;
                xlWorkSheet.Cells[5, 6] = pmm4;
                xlWorkSheet.Cells[5, 7] = pmm5;
            }
            xlWorkSheet.Cells[5, 8] = pmm1;
            xlWorkSheet.Cells[5, 9] = pmm5;

            //Setup the column header row (row 5)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "I"]);

            //Set the header row fonts bold
            headerRange.Font.Bold = false;
            headerRange.RowHeight = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Put a border around the header row
            headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                //For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[64, s]);
                writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
               Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
               Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
               Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                border = writeRange.Borders;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 7.5;
                    if (s == "H" || s == "I")
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 25.00;
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                }
            }

            Microsoft.Office.Interop.Excel.Range lineRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "G"]);
            border = lineRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            Microsoft.Office.Interop.Excel.Range lineRange2 = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "G"]);
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

                if (iRow == 6 || iRow == 8 || iRow == 10 || iRow == 28 || iRow == 30 || iRow == 34 || iRow == 47 || iRow == 49 || iRow == 51)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "I"]);
                    cellRange.RowHeight = 8;
                }

                //set up Residential with subscript 2 -  CR 7732

                if (iRow == 31)
                {
                    xlWorkSheet.Cells[iRow, 1] = "Residential2";
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.RowHeight = 12;
                    cellRange.Characters[12, 1].Font.Superscript = true;
                }

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)

                if (iRow == 7 || iRow == 29 || iRow == 48)
                {
                    foreach (string s in strColumns)
                    {
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Rows[iRow, Type.Missing]).Font.Bold = true;
                    }

                    //set up Total Private Construction with subscript 1

                    if (iRow == 29)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.RowHeight = 12;
                        cellRange.Characters[27, 1].Font.Superscript = true;
                    }
                    //set up Total Public Construction with subscript 3

                    else if (iRow == 48)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Public Construction3";
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.RowHeight = 12;
                        cellRange.Characters[26, 1].Font.Superscript = true;
                    }
                }

            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "I"]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            // add empty line
            Microsoft.Office.Interop.Excel.Range footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, "I"]);
            footRange20.Merge(Type.Missing);
            footRange20.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "I"]);
            footRange2.Merge(Type.Missing);
            footRange2.RowHeight = 12;
            xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of private construction not shown separately:";
            footRange2.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "I"]);
            footRange3.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

            //add empty line
            Microsoft.Office.Interop.Excel.Range footRange21 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, "I"]);
            footRange21.Merge(Type.Missing);
            footRange21.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "I"]);
            footRange4.Merge(Type.Missing);
            footRange4.RowHeight = 12;
            xlWorkSheet.Cells[iRow + 7, 1] = "2Includes private residential improvements.";
            footRange4.Characters[1, 1].Font.Superscript = true;

            //add empty line
            Microsoft.Office.Interop.Excel.Range footRange22 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "I"]);
            footRange22.Merge(Type.Missing);
            footRange22.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "I"]);
            footRange5.Merge(Type.Missing);
            footRange5.RowHeight = 12;
            xlWorkSheet.Cells[iRow + 9, 1] = "3Includes the following categories of public construction not shown separately:";
            footRange5.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, "I"]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 10, 1] = "lodging, religious, communication, and manufacturing.";

            //add empty line
            Microsoft.Office.Interop.Excel.Range footRange23 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, "I"]);
            footRange23.Merge(Type.Missing);
            footRange23.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange7 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 12, "A"], xlWorkSheet.Cells[iRow + 12, "I"]);
            footRange7.Merge(Type.Missing);
            footRange7.WrapText = true;
            xlWorkSheet.Cells[iRow + 12, 1] = "Data are at an annual rate, adjusted for seasonality but not price changes. Source: U.S. Census Bureau, Construction Spending, " + MYD + "."; 
           
            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, "I"]);
            footRange8.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 13, 1] = "Table 3 provides estimated measures of sampling variability. Additional information on the survey methodology may be found at";

            Microsoft.Office.Interop.Excel.Range footRange10 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 14, "A"], xlWorkSheet.Cells[iRow + 14, "I"]);
            footRange10.Merge(Type.Missing);
            footRange10.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 14, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange10.Font.Name = "Arial";
            footRange10.Font.Size = 8;

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 40;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 40;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
  
        }

        private void ExportToExcel2(DataTable dt, string stitle, string subtitle)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "Table2";
            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 10;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "J"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.Characters[1, 8].Font.Bold = false;
            titleRange.Font.Name = "Arial";

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "J"]);
            titleRange2.Merge(Type.Missing);

            //add an empty row

            //add year to change
            xlWorkSheet.Cells[4, 8] = "Year-to-date";

            //Span the title across columns H through J
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, "J"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.Font.Bold = false;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "J"]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            var dti = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
            string curmons = (dti.ToString("MMM yyyy", CultureInfo.InvariantCulture));
            string pmon1 = (dti.AddMonths(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon2 = (dti.AddMonths(-2)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon3 = (dti.AddMonths(-3)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon4 = (dti.AddMonths(-4)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string pmon5 = (dti.AddYears(-1)).ToString("MMM yyyy", CultureInfo.InvariantCulture);
            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

            string smon = sdate.Substring(4, 2);

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[5, 1] = "           Type of Construction\n";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 1]);
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            xlWorkSheet.Cells[5, 2] = cmm + "p";
             cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[5, 2]);
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
                xlWorkSheet.Cells[5, 7] = pmm5 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 7], xlWorkSheet.Cells[5, 7]);
                cellRange.Characters[9, 1].Font.Superscript = true;
            }
            else
            {
                xlWorkSheet.Cells[5, 5] = pmm3;
                xlWorkSheet.Cells[5, 6] = pmm4;
                xlWorkSheet.Cells[5, 7] = pmm5;
            }
            xlWorkSheet.Cells[5, 8] = cmm;
            xlWorkSheet.Cells[5, 9] = pmm5;
            xlWorkSheet.Cells[5, 10] = "Percent change";

            //Setup the column header row (row 5)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "J"]);

            //Set the header row fonts bold
            headerRange.Font.Bold = false;
            headerRange.RowHeight = 33;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Put a border around the header row
            headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                //For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[64, s]);

                writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                border = writeRange.Borders;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 7.00;
                    if (s == "J")
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 23.00;
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                }
            }

            Microsoft.Office.Interop.Excel.Range lineRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "G"]);
            border = lineRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            Microsoft.Office.Interop.Excel.Range lineRange2 = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "G"]);
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

                if (iRow == 6 || iRow == 8 || iRow == 10 || iRow == 28 || iRow == 30 || iRow == 34 || iRow == 47 || iRow == 49 || iRow == 51)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "I"]);
                    cellRange.RowHeight = 8;
                }

                //set up Residential with subscript 2 -  CR 7732

                if (iRow == 31)
                {
                    xlWorkSheet.Cells[iRow, 1] = "Residential2";
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.RowHeight = 12;
                    cellRange.Characters[12, 1].Font.Superscript = true;
                }

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)

                if (iRow == 7 || iRow == 29 || iRow == 48)
                {
                    foreach (string s in strColumns)
                    {
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Rows[iRow, Type.Missing]).Font.Bold = true;
                    }

                    //set up Total Private Construction with subscript 1

                    if (iRow == 29)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.RowHeight = 12;
                        cellRange.Characters[27, 1].Font.Superscript = true;
                    }

                    //set up Total Public Construction with subscript 3
              
                    else if (iRow == 48)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Public Construction3";
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.RowHeight = 12;
                        cellRange.Characters[26, 1].Font.Superscript = true;
                    }
                }

            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "J"]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            // add empty line
            Microsoft.Office.Interop.Excel.Range footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, "I"]);
            footRange20.Merge(Type.Missing);
            footRange20.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "J"]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of private construction not shown separately:";
            footRange2.RowHeight = 12;
            footRange2.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "J"]);
            footRange3.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

            // add empty line
            Microsoft.Office.Interop.Excel.Range footRange21 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, "I"]);
            footRange21.Merge(Type.Missing);
            footRange21.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "J"]);
            footRange4.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 7, 1] = "2Includes private residential improvements.";
            footRange4.RowHeight = 12;
            footRange4.Characters[1, 1].Font.Superscript = true;

            // add empty line
            Microsoft.Office.Interop.Excel.Range footRange22 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "I"]);
            footRange22.Merge(Type.Missing);
            footRange22.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "J"]);
            footRange5.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 9, 1] = "3Includes the following categories of public construction not shown separately:";
            footRange5.RowHeight = 12;
            footRange5.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, "J"]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 10, 1] = "lodging, religious, communication, and manufacturing.";

            // add empty line
            Microsoft.Office.Interop.Excel.Range footRange23 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, "I"]);
            footRange23.Merge(Type.Missing);
            footRange23.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange7 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 12, "A"], xlWorkSheet.Cells[iRow + 12, "J"]);
            footRange7.Merge(Type.Missing);
            footRange7.WrapText = true;
            footRange7.Rows.AutoFit();
            xlWorkSheet.Cells[iRow + 12, 1] = "Source: U.S. Census Bureau, Construction Spending, " + MYD + ".";

            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, "J"]);
            footRange8.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 13, 1] = "Table 3 provides estimated measures of sampling variability. Additional information on the survey methodology may be found at";

            Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 14, "A"], xlWorkSheet.Cells[iRow + 14, "J"]);
            footRange9.Merge(Type.Missing);
            footRange9.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 14, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange9.Font.Name = "Arial";
            footRange9.Font.Size = 8;

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 20;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 40;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

        }

        private void ExportToExcel3(DataTable dt, string stitle, string subtitle)
        {
            if (rdt4.Visible)
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            else
                xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = "Table3";
            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 10;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "F"]);
            titleRange.Merge(Type.Missing);
            
            //Increase the font-size of the title
            titleRange.Font.Size = 9;

            //Make the title bold
            titleRange.Font.Bold = true;
            
            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.Characters[1, 8].Font.Bold = false;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;
            
            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "F"]);
            titleRange2.Merge(Type.Missing);

            //add an empty row

            //add coefficient of variation, standard error
            xlWorkSheet.Cells[4, 2] = "Coefficient of variation";

            //Span the title across columns B through C
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "B"], xlWorkSheet.Cells[4, "C"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            
            xlWorkSheet.Cells[4, 4] = "Standard error";

            //Span the title across columns H through J
            titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "D"], xlWorkSheet.Cells[4, "F"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "F"]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[5, 1] = "             Type of Construction\n";
            xlWorkSheet.Cells[5, 2] = "Monthly\nestimate";
            xlWorkSheet.Cells[5, 3] = "Year-to-date\nestimate";
            xlWorkSheet.Cells[5, 4] = "Year-to-date\nchange";
            xlWorkSheet.Cells[5, 5] = "Month-to-month\nchange";
            xlWorkSheet.Cells[5, 6] = "Month-to-month\nchange from prev. year";
           
            //Setup the column header row (row 5)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "F"]);

            //Set the header row fonts bold
            headerRange.Font.Bold = false;
            headerRange.RowHeight = 33;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Put a border around the header row
            headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                //For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[64, s]);

                writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                border = writeRange.Borders;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
               
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 13.00;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "0.0";
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 24.50;
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

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                if (iRow == 7 || iRow == 29 || iRow == 48)
                {
                    foreach (string s in strColumns)
                    {
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Rows[iRow, Type.Missing]).Font.Bold = true;
                    }

                    //set up Total Private Construction with subscript 1
                    if (iRow == 29)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Private Construction";
                        Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        //cellRange0.Characters[27, 1].Font.Superscript = true;
                    }
                    else if (iRow == 48)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Public Construction";
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        //cellRange.Characters[26, 1].Font.Superscript = true;
                    }
                }
            }

            //add text after grid
            
            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "F"]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "NA Not applicable";
            
            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "F"]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "Source: U.S. Census Bureau, Construction Spending, " +MYD  + "."  + " Additional information on the survey methodology may be found at";
            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "F"]);
            footRange4.Merge(Type.Missing);
            footRange4.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 5, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange4.Font.Name = "Arial";
            footRange4.Font.Size = 8;

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 20;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 40;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

        }

        private void ExportToExcel4(DataTable dt, string stitle, string subtitle)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1); 
            xlWorkSheet.Name = "Table4";
            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 10;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "E"]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 9;
            titleRange.Characters[1, 8].Font.Bold = false;

            //Make the title bold
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "E"]);
            titleRange2.Merge(Type.Missing);

            //add an empty row

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


            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[4, 1] = "\n            Type of Construction";
            if (sdate.Substring(4, 2) == "12")
                xlWorkSheet.Cells[4, 2] = mon1.Substring(0, 4)+"p";
            else
                xlWorkSheet.Cells[4, 2] = mon1.Substring(0, 4) + "r";

            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 2], xlWorkSheet.Cells[4, 2]);
            cellRange.Characters[5, 1].Font.Superscript = true;

            if (sdate.Substring(4, 2) == "05")
            {
                xlWorkSheet.Cells[4, 3] = mon2.Substring(0, 4) + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 3], xlWorkSheet.Cells[4, 3]);
                cellRange.Characters[5, 1].Font.Superscript = true;
            }
            else
                xlWorkSheet.Cells[4, 3] = mon2.Substring(0, 4);

            xlWorkSheet.Cells[4, 4] = "Percent change";
            xlWorkSheet.Cells[4, 5] = "Coefficient of\nvariation";
            
            //Setup the column header row (row 5)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, "E"]);

            //Set the header row fonts bold
            headerRange.RowHeight  = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Put a border around the header row
            headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                //For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[66, s]);

                writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

                border = writeRange.Borders;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 14.00;
                    if (s == "D" || s == "E")
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else if (s != "C")
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 24.00;
                    border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                }
            }

            //Populate rest of the data. Start at row[5] 
            int iRow = 5; //We start at row 5

            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;

                    if (iRow > 4 && iCol == 3)
                    {
                        cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "C"], xlWorkSheet.Cells[iRow, "C"]);
                        cellRange.NumberFormat = "#,###";
                    }
                    
                    xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                }

                if (iRow == 3 || iRow == 5 || iRow == 7 || iRow == 9 || iRow == 27 || iRow == 29 || iRow == 33 || iRow == 49 || iRow == 51 || iRow == 53)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "I"]);
                    cellRange.RowHeight = 8;
                }

                //set up Residential with subscript 2 -  CR 7732

                if (iRow == 30)
                {
                    xlWorkSheet.Cells[iRow, 1] = "Residential2";
                    Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange2.RowHeight = 12;
                    cellRange2.Characters[12, 1].Font.Superscript = true;
                }

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)

                if (iRow == 6 || iRow == 28 || iRow == 50)
                {
                    foreach (string s in strColumns)
                    {
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Rows[iRow, Type.Missing]).Font.Bold = true;
                    }

                    if (iRow == 28)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                        Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange1.RowHeight = 12;
                        cellRange1.Characters[27, 1].Font.Superscript = true;
                    }
                    else if (iRow == 50)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Public Construction3";
                        Microsoft.Office.Interop.Excel.Range cellRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange3.RowHeight = 12;
                        cellRange3.Characters[26, 1].Font.Superscript = true;
                    }
                }
            }

            //add text after grid
            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "E"]);
            footRange1.Merge(Type.Missing);
            if (sdate.Substring(4, 2) != "12")
            {
                xlWorkSheet.Cells[iRow + 2, 1] = "NA Not applicable rRevised";
            }
            else
            {
                xlWorkSheet.Cells[iRow + 2, 1] = "NA Not applicable rPreliminary";
            }
            footRange1.Characters[19, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, "E"]);
            footRange20.Merge(Type.Missing);
            footRange20.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "E"]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of private construction not shown separately:";
            footRange2.RowHeight = 12;
            footRange2.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "E"]);
            footRange3.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "highway and street, and conservation and development.";

            footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, "E"]);
            footRange20.Merge(Type.Missing);
            footRange20.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "E"]);
            footRange4.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 7, 1] = "2Includes private residential improvements.";
            footRange4.RowHeight = 12;
            footRange4.Characters[1, 1].Font.Superscript = true;

            footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "E"]);
            footRange20.Merge(Type.Missing);
            footRange20.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "E"]);
            footRange5.Merge(Type.Missing);
            footRange5.RowHeight = 12;
            xlWorkSheet.Cells[iRow + 9, 1] = "3Includes the following categories of public construction not shown separately:";
            footRange5.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, "E"]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 10, 1] = "lodging, religious, communication, and manufacturing.";

            footRange20 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, "E"]);
            footRange20.Merge(Type.Missing);
            footRange20.RowHeight = 8;

            Microsoft.Office.Interop.Excel.Range footRange7 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 12, "A"], xlWorkSheet.Cells[iRow + 12, "E"]);
            footRange7.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 12, 1] = "Source: U.S. Census Bureau, Construction Spending, " + MYD + ". Additional information on the survey methodology may be found at";
            footRange7.WrapText = true;

            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, "E"]);
            footRange8.Merge(Type.Missing);
            footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 13, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange8.Font.Name = "Arial";
            footRange8.Font.Size = 8;

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlPortrait;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 40;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 50;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;
        }

        private void frmTablePressRelease_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "EXIT");
        }

    }
}
