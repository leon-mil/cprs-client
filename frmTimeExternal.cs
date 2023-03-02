
/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : frmTimeExternal.cs
 Programmer    : Diane Musachio
 Creation Date : 07/19/2017
 Inputs        : N/A
 Parameters    : N/A
 Output        : totsatime.xlsx, tottime.xlsx, privsatime.xlsx,
                 privtime.xlsx, pubsatime.xlsx, pubtime.xlsx,
                 slsatime.xlsx, sltime.xlsx, fedsatime.xlsx, fedtime.xlsx
 Description   : This program will display screen to External time
                 series tables and the user can create related excel files
 Detail Design : Detailed Design for External Time Series
 Other         : Called by: Publication -> External Time Series
 Revisions     : See Below
 *********************************************************************
 Modified Date : 2/21/2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR885
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
    public partial class frmTimeExternal : frmCprsParent
    {
        private TimeExternalData data_object;
        private string sdate;
        private string tableType = "";
        private string seasonal = "";
        private string saveFilename;
        private string[] strColumns;
        private DataTable dt;
        private DataTable dtTSAA;
        private DataTable dtTUNA;
        private DataTable dtPSAA;
        private DataTable dtPUNA;
        private DataTable dtSSAA;
        private DataTable dtSUNA;
        private DataTable dtFSAA;
        private DataTable dtFUNA;
        private DataTable dtVSAA;
        private DataTable dtVUNA;
        private frmMessageWait waiting;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //Declare Excel Interop variables
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        public frmTimeExternal()
        {
            InitializeComponent();
        }

        private void frmTimeExternal_Load(object sender, EventArgs e)
        {
            //get current survey month
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            dgData.DataSource = null;

            //default table upon entry is total seasonally adjusted
            tableType = "T";
            seasonal = "SAA";
            GetData();
            dt = dtTSAA;           
            SetUpGrid();
                        
        }

        //Gets data for screens
        private void GetData()
        {
            data_object = new TimeExternalData();
            dtTSAA = data_object.GetExternalTimeData(sdate, "T", "SAA");
            dtTUNA = data_object.GetExternalTimeData(sdate, "T", "UNA");
            dtPSAA = data_object.GetExternalTimeData(sdate, "P", "SAA");
            dtPUNA = data_object.GetExternalTimeData(sdate, "P", "UNA");
            dtSSAA = data_object.GetExternalTimeData(sdate, "S", "SAA");
            dtSUNA = data_object.GetExternalTimeData(sdate, "S", "UNA");
            dtFSAA = data_object.GetExternalTimeData(sdate, "F", "SAA");
            dtFUNA = data_object.GetExternalTimeData(sdate, "F", "UNA");
            dtVSAA = data_object.GetExternalTimeData(sdate, "V", "SAA");
            dtVUNA = data_object.GetExternalTimeData(sdate, "V", "UNA");
        }

        //Sets up datagrid for screens
        private void SetUpGrid()
        {
            dgData.DataSource = dt;

            dgData.ReadOnly = true;

            dgData.ColumnHeadersHeight = 50;
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;

            //Freeze Date column
            dgData.Columns[0].Frozen = true;

            //Format Date Column
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[0].DefaultCellStyle.Format = "MMM-yy";
            dgData.Columns[0].DefaultCellStyle.FormatProvider = CultureInfo.InvariantCulture;

            //Populate HeaderTexts for datagrid
            //Total
            if (tableType == "T")
            {
                for (int j = 0; j < 49; j++)
                {
                    dgData.Columns[j].HeaderText = dt.Columns[j].Caption;
                }
            }
            //Public
            else if (tableType == "P")
            {
                for (int j = 0; j < 44; j++)
                {
                    dgData.Columns[j].HeaderText = dt.Columns[j].Caption;
                }
            }
            //Federal
            else if (tableType == "F")
            {
                for (int j = 0; j < 14; j++)
                {
                    dgData.Columns[j].HeaderText = dt.Columns[j].Caption;
                }
            }
            //Private or State and Local
            else if ((tableType == "V") || (tableType == "S"))
            {
                for (int j = 0; j < 67; j++)
                {
                    dgData.Columns[j].HeaderText = dt.Columns[j].Caption;
                }
            }

            //make columns unsortable and assign width
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;

                if (dgvc.Index == 0)
                {
                    dgvc.Width = 60;
                }
                else
                {
                    dgvc.Width = 125;
                }
            }
        }

        //formats data cells in datagrid
        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex != 0)
            {
                e.CellStyle.Format = "N0";
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //Clears out 0 and displays a blank 
                if (e.Value.ToString() == "0")
                {
                    e.Value = string.Empty;
                    e.FormattingApplied = true;
                }
            }
            else
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;            
            }
        }

        //center title based on size of string
        private void lblTitle_SizeChanged(object sender, EventArgs e)
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Size.Width) / 2;
            lblSA.Left = (this.ClientSize.Width - lblSA.Size.Width) / 2;
        }

        //subsets data based on which table type radio button is checked
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            if (radiobutton.Checked)
            {
                //total
                if (sender == rdt)
                {
                    rdt.Checked = true;
                    tableType = "T";

                    if (seasonal == "SAA")
                        dt = dtTSAA;
                    else
                        dt = dtTUNA;
                          
                    lblTitle.Text = "Value of Total Construction Put in Place";
                }
                //private
                else if (sender == rdv)
                {
                    rdv.Checked = true;
                    tableType = "V";

                    if (seasonal == "SAA")
                        dt = dtVSAA;
                    else
                        dt = dtVUNA;

                    lblTitle.Text = "Value of Private Construction Put in Place";
                }
                //public
                else if (sender == rdp)
                {
                    rdp.Checked = true;
                    tableType = "P";

                    if (seasonal == "SAA")
                        dt = dtPSAA;
                    else
                        dt = dtPUNA;

                    lblTitle.Text = "Value of Public Construction Put in Place";
                }
                //state and local
                else if (sender == rds)
                {
                    rds.Checked = true;
                    tableType = "S";

                    if (seasonal == "SAA")
                        dt = dtSSAA;
                    else
                        dt = dtSUNA;

                    lblTitle.Text = "Value of State and Local Construction Put in Place";
                }
                //federal
                else if (sender == rdf)
                {
                    rdf.Checked = true;
                    tableType = "F";

                    if (seasonal == "SAA")
                        dt = dtFSAA;
                    else
                        dt = dtFUNA;

                    lblTitle.Text = "Value of Federal Construction Put in Place";
                }

                SetUpGrid();
            }
        }

        //subsets data based on which seasonal radio button is checked
        private void RadioType_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            if (radiobutton.Checked)
            {
                //total
                if (sender == rdsa)
                {
                    rdsa.Checked = true;
                    lblSA.Text = "Seasonally Adjusted Annual Rate";
                    seasonal = "SAA";

                    if (tableType == "F")
                        dt = dtFSAA;

                    else if (tableType == "T")
                        dt = dtTSAA;

                    else if (tableType == "P")
                        dt = dtPSAA;

                    else if (tableType == "S")
                        dt = dtSSAA;

                    else if (tableType == "V")
                        dt = dtVSAA;

                    else dt = null;

                }
                //private
                else if (sender == rdnsa)
                {
                    rdnsa.Checked = true;
                    lblSA.Text = "Not Seasonally Adjusted";
                    seasonal = "UNA";

                    if (tableType == "F")
                        dt = dtFUNA;

                    else if (tableType == "T")
                        dt = dtTUNA;

                    else if (tableType == "P")
                        dt = dtPUNA;

                    else if (tableType == "S")
                        dt = dtSUNA;

                    else if (tableType == "V")
                        dt = dtVUNA;

                    else dt = null;

                }

                SetUpGrid();
            }
        }

        //button to create Time Series tables
        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save a File";

            saveFileDialog1.FileName = "totsatime.xlsx";
          
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
            string last_col;

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            //delete exist file
            GeneralFunctions.DeleteFile(saveFilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

            string stitle;
            string stitle1;

            string sfilename;
            string ssheetname;

            //Creates Total Seasonally Adjusted Excel File
            tableType = "T";
            seasonal = "SAA";
            dt = dtTSAA;
            stitle = "Value of Construction Put in Place - Seasonally Adjusted Annual Rate";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "AW";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW"};
            sfilename = dir + "\\totsatime.xlsx";
            ssheetname = "Total SA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Total Not Seasonally Adjusted Excel File
            tableType = "T";
            seasonal = "UNA";
            dt = dtTUNA;
            stitle = "Value of Construction Put in Place - Not Seasonally Adjusted";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "AW";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW"};
            sfilename = dir + "\\tottime.xlsx";
            ssheetname = "Total NSA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Private Seasonally Adjusted Excel File
            tableType = "V";
            seasonal = "SAA";
            dt = dtVSAA;
            stitle = "Value of Private Construction Put in Place - Seasonally Adjusted Annual Rate";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "BO";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA",
                    "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN",
                    "BO"};
            sfilename = dir + "\\privsatime.xlsx";
            ssheetname = "Private SA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Private Not Seasonally Adjusted Excel File
            tableType = "V";
            seasonal = "UNA";
            dt = dtVUNA;
            stitle = "Value of Private Construction Put in Place - Not Seasonally Adjusted";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "BO";
            strColumns = new string[]  { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA",
                    "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN",
                    "BO"};
            sfilename = dir + "\\privtime.xlsx";
            ssheetname = "Private NSA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Public Seasonally Adjusted Excel File
            tableType = "P";
            seasonal = "SAA";
            dt = dtPSAA;
            stitle = "Value of Public Construction Put in Place - Seasonally Adjusted Annual Rate";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "AR";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR"};
            sfilename = dir + "\\pubsatime.xlsx";
            ssheetname = "Public SA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Public Not Seasonally Adjusted Excel File
            tableType = "P";
            seasonal = "UNA";
            dt = dtPUNA;
            stitle = "Value of Public Construction Put in Place - Not Seasonally Adjusted";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "AR";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR"};
            sfilename = dir + "\\pubtime.xlsx";
            ssheetname = "Public NSA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates State and Local Seasonally Adjusted Excel File
            tableType = "S";
            seasonal = "SAA";
            dt = dtSSAA;
            stitle = "Value of State and Local Construction Put in Place - Seasonally Adjusted Annual Rate";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "BO";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA",
                    "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN",
                    "BO"};
            sfilename = dir + "\\slsatime.xlsx";
            ssheetname = "State SA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates State and Local Not Seasonally Adjusted Excel File
            tableType = "S";
            seasonal = "UNA";
            dt = dtSUNA;
            stitle = "Value of State and Local Construction Put in Place - Not Seasonally Adjusted";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "BO";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "AA",
                    "AB", "AC", "AD", "AE", "AF", "AG", "AH", "AI", "AJ", "AK", "AL", "AM", "AN",
                    "AO", "AP", "AQ", "AR", "AS", "AT", "AU", "AV", "AW", "AX", "AY", "AZ", "BA",
                    "BB", "BC", "BD", "BE", "BF", "BG", "BH", "BI", "BJ", "BK", "BL", "BM", "BN",
                    "BO"};
            sfilename = dir + "\\sltime.xlsx";
            ssheetname = "State NSA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Federal Seasonally Adjusted Excel File
            tableType = "F";
            seasonal = "SAA";
            dt = dtFSAA;
            stitle = "Value of Federal Construction Put in Place - Seasonally Adjusted Annual Rate";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "N";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N"};
            sfilename = dir + "\\fedsatime.xlsx";
            ssheetname = "Fed SA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            //Creates Federal Not Seasonally Adjusted Excel File
            tableType = "F";
            seasonal = "UNA";
            dt = dtFUNA;
            stitle = "Value of Federal Construction Put in Place - Not Seasonally Adjusted";
            stitle1 = "(Millions of dollars. Details may not add to totals due to rounding.)";
            last_col = "N";
            strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                    "L", "M", "N"};
            sfilename = dir + "\\fedtime.xlsx";
            ssheetname = "Fed NSA";
            ExportToExcel(sfilename, dt, stitle, stitle1, last_col, ssheetname);

            
            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");         
        }

        private void ExportToExcel(string sfilename, DataTable dt, string stitle, string stitle1, string last_col, string ssheetname)
        {
            DataTable copydt;
            copydt = dt.Copy();

            foreach (DataRow row in copydt.Rows)
            {
                for (int i = 1; i < copydt.Columns.Count; i++)
                    copydt.AsEnumerable().ToList().ForEach(p => p.SetField<Decimal>(i, Math.Round(p.Field<Decimal>(i), 0, MidpointRounding.AwayFromZero)));
            }

            //delete existing file
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
            xlWorkSheet.Cells[2, 1] = stitle;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange.Merge(Type.Missing);

            titleRange.Font.Size = 9;
           
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[3, 1] = stitle1;

            //Span the title across columns 
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, last_col]);
            titleRange2.Merge(Type.Missing);

            titleRange2.Font.Size = 8;
            titleRange2.Font.Bold = true;
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Span the title across columns 
            Microsoft.Office.Interop.Excel.Range titleRange4 = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, last_col]);
            titleRange4.Merge(Type.Missing);

            //Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, last_col]);
            //Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            //border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
            //    Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            //border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
            //    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
            //    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
            //    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;


            //Convert current survey month to datetime
            var dti = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            //Press release date assignments
            string fm2 = (dti.AddMonths(+2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string press_month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(fm2.Substring(4, 2)));
            string press_year = fm2.Substring(0, 4);

            //3 month dates using superscripts
            string curmons = (dti.ToString("MMM-yy", CultureInfo.InvariantCulture));
            string pmon1 = (dti.AddMonths(-1)).ToString("MMM-yy", CultureInfo.InvariantCulture);
            string pmon2 = (dti.AddMonths(-2)).ToString("MMM-yy", CultureInfo.InvariantCulture);

            //Setup the column header row 
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, last_col]);
            int j = 0;
            foreach (DataColumn c in copydt.Columns)
            {
                j++;
                xlWorkSheet.Cells[5, j] = c.Caption;
            }

            //Set the header row fonts bold
            headerRange.Font.Bold = false;
            headerRange.RowHeight = 33;
            
            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            ////Put a border around the header row
            //headerRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
            //    Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
            //    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
            //    Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);

            //Populate headers, assume rows[0,1,2] contain the title and row[5] contains all the headers
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;

                ////For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, s], xlWorkSheet.Cells[dt.Rows.Count + 6, s]);

                //Microsoft.Office.Interop.Excel.Borders border = writeRange.Borders;
                //border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                //border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //if (s != last_col)
                //    border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                //else
                //    border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

                //writeRange.BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous,
                //Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin,
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic,
                //Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic);
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 12.00;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                //format columns to display numbers
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,##0";
                }
                //format date column as text so "MMM-yy" format displays correctly
                else
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).EntireColumn.NumberFormat = "@";
            }

            //Populate rest of the data
            int iRow = 5;
            int jColumn = 0;

            foreach (DataRow r in copydt.Rows)
            {
                iRow++;
                jColumn = 0;
                
                foreach (DataColumn c in copydt.Columns)
                {
                    jColumn++;
                    xlWorkSheet.Cells[iRow, jColumn] = r[c.ColumnName];
                }

                //set up superscripts Total of Construction,
                if ((iRow == 6) && (tableType == "T"))
                {
                    Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "B"], xlWorkSheet.Cells[iRow - 1, "B"]);
                    cellRange.Font.Bold = true;
                    xlWorkSheet.Cells[iRow - 1, "B"] = "Total\n\rConstruction1";
                    cellRange.Characters[20, 1].Font.Superscript = true;

                    xlWorkSheet.Cells[iRow - 1, "U"] = "Total\n\rPrivate Construction2";
                    Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "U"], xlWorkSheet.Cells[iRow - 1, "U"]);
                    cellRange0.Characters[28, 1].Font.Superscript = true;

                    xlWorkSheet.Cells[iRow - 1, "AI"] = "Total\n\rPublic Construction3";
                    Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "AI"], xlWorkSheet.Cells[iRow - 1, "AI"]);
                    cellRange1.Characters[27, 1].Font.Superscript = true;    
                }
                //set up superscripts Total Private Construction
                else if ((iRow == 6) && (tableType == "V"))
                {
                    Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "B"], xlWorkSheet.Cells[iRow - 1, "B"]);
                    cellRange.Font.Bold = true;
                    xlWorkSheet.Cells[iRow - 1, "B"] = "Total\n\rPrivate Construction1";
                    cellRange.Characters[28, 1].Font.Superscript = true;

                    xlWorkSheet.Cells[iRow - 1, "C"] = "Residential (inc. Improvements)2";
                    Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "C"], xlWorkSheet.Cells[iRow - 1, "C"]);
                    cellRange0.Characters[32, 1].Font.Superscript = true;
                }
                //set up superscripts Total Public Construction
                else if ((iRow == 6) && (tableType == "P"))
                {
                    Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "B"], xlWorkSheet.Cells[iRow - 1, "B"]);
                    cellRange.Font.Bold = true;
                    xlWorkSheet.Cells[iRow - 1, "B"] = "Total\n\rPublic Construction1";
                    cellRange.Characters[27, 1].Font.Superscript = true;

                    xlWorkSheet.Cells[iRow - 1, "Q"] = "Total\n\rState and Local Construction2";
                    Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "Q"], xlWorkSheet.Cells[iRow - 1, "Q"]);
                    cellRange0.Characters[36, 1].Font.Superscript = true;

                    xlWorkSheet.Cells[iRow - 1, "AF"] = "Total\n\rFederal Construction3";
                    Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "AF"], xlWorkSheet.Cells[iRow - 1, "AF"]);
                    cellRange1.Characters[28, 1].Font.Superscript = true;
                }
            }

            //set up superscripts of first 3 dates
            xlWorkSheet.Cells[6, "A"] = curmons + "p";
            Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[6, "A"], xlWorkSheet.Cells[6, "A"]);
            cellRange2.Characters[7, 1].Font.Superscript = true;

            xlWorkSheet.Cells[7, "A"] = pmon1 + "r";
            Microsoft.Office.Interop.Excel.Range cellRange3 = xlApp.get_Range(xlWorkSheet.Cells[7, "A"], xlWorkSheet.Cells[7, "A"]);
            cellRange3.Characters[7, 1].Font.Superscript = true;

            xlWorkSheet.Cells[8, "A"] = pmon2 + "r";
            Microsoft.Office.Interop.Excel.Range cellRange4 = xlApp.get_Range(xlWorkSheet.Cells[8, "A"], xlWorkSheet.Cells[8, "A"]);
            cellRange4.Characters[7, 1].Font.Superscript = true;

            //set up footnotes for Total Construction
            if (tableType == "T")
            {
                iRow++;
        
                Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "AW"]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
                footRange1.Characters[1, 1].Font.Superscript = true;
                footRange1.Characters[15, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "AW"]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "1Detailed types of construction not available prior to 2002.";
                footRange2.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, "AW"]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 6, 1] = "2Includes the following categories of private construction not shown separately:";
                footRange3.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "AW"]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 7, 1] = " public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

                Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "AW"]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 9, 1] = "3Includes the following categories of public construction not shown separately:";
                footRange5.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, "AW"]);
                footRange6.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 10, 1] = " lodging, religious, communication, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange6a = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, "AW"]);
                footRange6a.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 11, 1] = " Detailed types of construction not available prior to 2002.";

                Microsoft.Office.Interop.Excel.Range footRange7 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, "AW"]);
                footRange7.Merge(Type.Missing);
                footRange7.WrapText = true;
                xlWorkSheet.Cells[iRow + 13, 1] = "Source: U.S. Census Bureau, Construction Spending, " + press_month + " 1, " + press_year + ". Additional information on the survey methodology may be found at  ";

                Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 14, "A"], xlWorkSheet.Cells[iRow + 14, "AW"]);
                footRange9.Merge(Type.Missing);
                footRange9.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 14, 1], "http://www.census.gov/construction/c30/meth.html", 
                           Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange9.Font.Name = "Arial";
                footRange9.Font.Size = 8;
                footRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange6.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange6a.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange7.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                
                footRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }
            //set up footnotes for Private Construction
            else if (tableType == "V")
            {
                iRow++;

                Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "BO"]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
                footRange1.Characters[1, 1].Font.Superscript = true;
                footRange1.Characters[15, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "BO"]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "1Total private construction includes the following categories of construction not shown separately:";
                footRange2.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "BO"]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = " public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "BO"]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 7, 1] = "2Private residential improvements does not include expenditures to rental, vacant, or seasonal properties.";
                footRange4.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange7 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "BO"]);
                footRange7.Merge(Type.Missing);
                footRange7.WrapText = true;
                xlWorkSheet.Cells[iRow + 9, 1] = "Source: U.S. Census Bureau, Construction Spending, " + press_month + " 1, " + press_year + ". Additional information on the survey methodology may be found at  "; 

                Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, "BO"]);
                footRange9.Merge(Type.Missing);
                footRange9.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 10, 1], "http://www.census.gov/construction/c30/meth.html",
                           Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange9.Font.Name = "Arial";
                footRange9.Font.Size = 8;

                footRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft; 
                footRange7.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }
            //set up footnotes for Public Construction
            else if (tableType == "P")
            {
                iRow++;

                Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "AR"]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
                footRange1.Characters[1, 1].Font.Superscript = true;
                footRange1.Characters[15, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "AR"]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of construction not shown separately:  "; 
                footRange2.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "AR"]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = " lodging, religious, communication, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, "AR"]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 6, 1] = " Detailed types of construction not available prior to 2002.";

                Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "AR"]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 8, 1] = "2Includes the following categories of construction not shown separately:";
                footRange5.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "AR"]);
                footRange6.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 9, 1] = " lodging, religious, communication, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange6a = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, "AR"]);
                footRange6a.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 11, 1] = "3Includes the following categories of  federal construction not shown separately:  ";
                footRange6a.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange6b = xlApp.get_Range(xlWorkSheet.Cells[iRow + 12, "A"], xlWorkSheet.Cells[iRow + 12, "AR"]);
                footRange6b.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 12, 1] = " lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange6c = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, "AR"]);
                footRange6c.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 13, 1] = " Detailed types of construction not available prior to 2002.";

                Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 15, "A"], xlWorkSheet.Cells[iRow + 15, "AR"]);
                footRange8.Merge(Type.Missing);
                footRange8.WrapText = true;
                xlWorkSheet.Cells[iRow + 15, 1] = "Source: U.S. Census Bureau, Construction Spending, " + press_month + " 1, " + press_year + ". Additional information on the survey methodology may be found at  ";

                Microsoft.Office.Interop.Excel.Range footRange10 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 16, "A"], xlWorkSheet.Cells[iRow + 16, "AR"]);
                footRange10.Merge(Type.Missing);
                footRange10.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 16, 1], "http://www.census.gov/construction/c30/meth.html",
                           Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange10.Font.Name = "Arial";
                footRange10.Font.Size = 8;

                footRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange6.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange6a.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange6b.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange6c.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                
                footRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }
            //set up footnotes for State and Local Construction
            else if (tableType == "S")
            {
                iRow++;

                Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "BO"]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
                footRange1.Characters[1, 1].Font.Superscript = true;
                footRange1.Characters[15, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "BO"]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "Note: Total state and local construction includes the following categories of construction not shown separately:";

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "BO"]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "BO"]);
                footRange8.Merge(Type.Missing);
                footRange8.WrapText = true;
                xlWorkSheet.Cells[iRow + 7, 1] = "Source: U.S. Census Bureau, Construction Spending, " + press_month + " 1, " + press_year + ". Additional information on the survey methodology may be found at  "; 

              
                Microsoft.Office.Interop.Excel.Range footRange10 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "BO"]);
                footRange10.Merge(Type.Missing);
                footRange10.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 8, 1], "http://www.census.gov/construction/c30/meth.html",
                           Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange10.Font.Name = "Arial";
                footRange10.Font.Size = 8;

                footRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                
                footRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }
            //set up footnotes for Federal Construction
            else if (tableType == "F")
            {
                iRow++;

                Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "N"]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
                footRange1.Characters[1, 1].Font.Superscript = true;
                footRange1.Characters[15, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, "N"]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "Note: Total federal construction includes the following categories of construction not shown separately:";

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, "N"]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, "N"]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 6, 1] = "Detailed types of construction not available prior to 2002.";

                Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, "N"]);
                footRange8.Merge(Type.Missing);
                footRange8.WrapText = true;
                xlWorkSheet.Cells[iRow + 8, 1] = "Source: U.S. Census Bureau, Construction Spending, " + press_month + " 1, " + press_year + ". Additional information on the survey methodology may be found at  "; 

                Microsoft.Office.Interop.Excel.Range footRange10 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, "N"]);
                footRange10.Merge(Type.Missing);
                footRange10.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 9, 1], "http://www.census.gov/construction/c30/meth.html",
                           Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange10.Font.Name = "Arial";
                footRange10.Font.Size = 8;

                footRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                footRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;             
            }

            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$5";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 20;
            xlWorkSheet.PageSetup.RightMargin = 10;
            xlWorkSheet.PageSetup.BottomMargin = 20;
            xlWorkSheet.PageSetup.LeftMargin = 10;

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            //Release objects
            GeneralFunctions.releaseObject(xlWorkSheet);
            GeneralFunctions.releaseObject(xlWorkBook);
            GeneralFunctions.releaseObject(xlApp);
        }

        private void frmTimeExternal_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "EXIT");
        }
    }
}
