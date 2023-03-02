/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmResponseRate.cs
Programmer    : Christine Zhang
Creation Date : July. 11 2019
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : Display response rate data
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
    public partial class frmResponseRate : frmCprsParent
    {
        public frmResponseRate()
        {
            InitializeComponent();
        }

        private bool form_loading = false;
        private ResponseRateData data_object;
        private string owner = string.Empty;
        private string rate = string.Empty;
        private string rev = string.Empty;
        private string sdate;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;
        private string saveFilename;
        private frmMessageWait waiting;

        private void frmResponseRate_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            form_loading = true;
            
            //set up default selection
            cbRev.SelectedIndex = 0;
            rdt.Checked = true;
            rdvip_urr.Checked = true;

            //get survey date
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            data_object = new ResponseRateData();
            GetData();

            form_loading = false;    
        }

        private void GetData()
        {
            rev = cbRev.SelectedIndex.ToString();

            if (rdt.Checked)
                owner = "T";
            else if (rds.Checked)
                owner = "S";
            else if (rdp.Checked)
                owner = "N";
            else if (rdm.Checked)
                owner = "M";
            else if (rdf.Checked)
                owner = "F";

            if (rdvip_urr.Checked)
                rate = "1";
            else if (rdvip_tqrr.Checked)
                rate = "2";
            else if (rdvip_imp.Checked)
                rate = "3";
            else if (rdsc_urr.Checked)
                rate = "4";
            else if (rdsc_tqrr.Checked)
                rate = "5";
            else if (rdsc_imp.Checked)
                rate = "6";

            //get data
            dgt0.DataSource = null;
            dgt0.DataSource = data_object.GetResponseRateTable(sdate.Substring(0, 4), owner, rate, rev);
            setItemColumnHeader();

            //set title
            if (owner == "T")
                lbltitle.Text = "TOTAL RESPONSE RATE FOR " + cbRev.Text.ToUpper();
            else if (owner == "S")
                lbltitle.Text = "STATE AND LOCAL RESPONSE RATE FOR " + cbRev.Text.ToUpper();
            else if (owner == "N")
                lbltitle.Text = "NONRESIDENTAL RESPONSE RATE FOR " + cbRev.Text.ToUpper();
            else if (owner == "M")
                lbltitle.Text = "MULTIFAMILY RESPONSE RATE FOR " + cbRev.Text.ToUpper();
            else
                lbltitle.Text = "FEDERAL RESPONSE RATE FOR " + cbRev.Text.ToUpper();

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgt0.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void setItemColumnHeader()
        {
            dgt0.Columns[0].HeaderText = "Type of Construction";
            dgt0.Columns[0].Width = 120;
            dgt0.Columns[1].HeaderText = "Rate";
            dgt0.Columns[1].Width = 210;

            int j = 0;
            if (rev == "1")
                j = 1;
            else if (rev == "2")
                j = 2;

            DateTime ddt = new DateTime(Convert.ToInt16(sdate.Substring(0, 4)), Convert.ToInt16(sdate.Substring(4, 2)), 1);
            for (int i=2; i< dgt0.ColumnCount; i++)
            {
                dgt0.Columns[i].HeaderText = ddt.AddMonths(-j).ToString("yyyyMM");
                dgt0.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgt0.Columns[i].Width = 70;
                j = j + 1;
            }
        }

        private void frmResponseRate_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void rdp_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdp.Checked)
                GetData();
        }

        private void rds_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rds.Checked)
                GetData();
        }

        private void rdf_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdf.Checked)
                GetData();
        }

        private void rdm_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdm.Checked)
                GetData();
        }

        private void rdt_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdt.Checked)
                GetData();
        }

        private void rdvip_urr_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdvip_urr.Checked)
                GetData();
        }

        private void rdvip_tqrr_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdvip_tqrr.Checked)
                GetData();
        }

        private void rdvip_imp_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdvip_imp.Checked)
                GetData();
        }

        private void rdsc_urr_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdsc_urr.Checked)
                GetData();
        }

        private void rdsc_tqrr_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdsc_tqrr.Checked )
                GetData();
        }

        private void rdsc_imp_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdsc_imp.Checked)
                GetData();
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
           
            DGVPrinter printer = new DGVPrinter();
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.Title = lbltitle.Text;
            string rate_type = string.Empty;
            if (rate == "1")
                rate_type = rdvip_urr.Text;
            else if (rate == "2")
                rate_type = rdvip_tqrr.Text;
            else if (rate == "3")
                rate_type = rdvip_imp.Text;
            else if (rate == "4")
                rate_type = rdsc_urr.Text;
            else if (rate == "5")
                rate_type = rdsc_tqrr.Text;
            else if (rate == "6")
                rate_type = rdsc_imp.Text;

            printer.SubTitle = rate_type;
            printer.PageSettings.Landscape = true;
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Response Rate";
            printer.Footer = " ";
            
            printer.PrintDataGridViewWithoutDialog(dgt0);

            Cursor.Current = Cursors.Default;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save an File";
           
            if (owner == "T")
                saveFileDialog1.FileName = "PrelTotRR.xlsx";
            else if (owner == "S")
                saveFileDialog1.FileName = "PrelSlRR.xlsx";
            else if (owner == "F")
                saveFileDialog1.FileName = "PrelFedRR.xlsx";
            else if (owner == "N")
                saveFileDialog1.FileName = "PrelNrRR.xlsx";
            else
                saveFileDialog1.FileName = "PrelMfRR.xlsx";

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

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();

            string file1 = "";
            string file2 = "";
            string file3 = "";

            if (owner == "F")
            {
                file1 = "PrelFedRR.xlsx";
                file2 = "Rev1FedRR.xlsx";
                file3 = "Rev2FedRR.xlsx";
            }
            else if (owner == "M")
            {
                file1 = "PrelMfRR.xlsx";
                file2 = "Rev1MfRR.xlsx";
                file3 = "Rev2MfRR.xlsx";
            }
            else if (owner == "N")
            {
                file1 = "PrelNrRR.xlsx";
                file2 = "Rev1NrRR.xlsx";
                file3 = "Rev2NrRR.xlsx";
            }
            else if (owner == "S")
            {
                file1 = "PrelSlRR.xlsx";
                file2 = "Rev1SlRR.xlsx";
                file3 = "Rev2SlRR.xlsx";
            }
            else
            {
                file1 = "PrelTotRR.xlsx";
                file2 = "Rev1TotRR.xlsx";
                file3 = "Rev2TotRR.xlsx";
            }

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ////create sheet table for premli
            sfilename = dir + "\\"+ file1;
            for (int i = 6; i >= 1; i--)
                ExportToExcel(owner, i.ToString(), "0");

            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Supress overwrite request
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ////create sheet table for first rev
            sfilename = dir + "\\" + file2;
            for (int i = 6; i >= 1; i--)
                ExportToExcel(owner, i.ToString(), "1");

            // Save file & Quit application
            xlWorkBook.SaveAs(sfilename, Microsoft.Office.Interop.Excel.XlFileFormat.xlOpenXMLWorkbook, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);

            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ////create sheet table for 2nd rev
            sfilename = dir + "\\" + file3;
            for (int i = 6; i >= 1; i--)
                ExportToExcel(owner, i.ToString(), "2");

            // Save file & Quit application
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
            MessageBox.Show("File has been created");   
        }

        //export output for owne, rev
        private void ExportToExcel(string owner, string rate, string rev)
        {
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();

            if (rate == "1")
                xlWorkSheet.Name = "VIP URR";
            else if (rate == "2")
                xlWorkSheet.Name = "VIP TQRR";
            else if (rate == "3")
                xlWorkSheet.Name = "VIP IMP";
            else if (rate == "4")
                xlWorkSheet.Name = "5C URR";
            else if (rate == "5")
                xlWorkSheet.Name = "5C TQRR";
            else if (rate == "6")
                xlWorkSheet.Name = "5C IMP";

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
          
            xlWorkSheet.Activate();

            ///*for datatable */
            int line = 1;
            DataTable dt = data_object.GetResponseRateTable(sdate.Substring(0,4), owner, rate, rev);
            //create data for each rate
            CreateOutput(line, dt, rate, rev);
        }

        private void CreateOutput(int line_start, DataTable dt, string rate, string rev )
        {
            string stitle = string.Empty;
            string sowner = string.Empty;
            string srev = string.Empty;

            if (owner == "T")
                sowner = "TOTAL";
            else if (owner == "F")
                sowner = "FEDERAL";
            else if (owner == "S")
                sowner = "STATE AND LOCAL";
            else if (owner == "M")
                sowner = "MULTIFAMILY";
            else
                sowner = "NONRESIDENTAL";

            if (rev == "0")
                srev = " PRELIMINARY";
            else if (rev == "1")
                srev = " FIRST REVISION";
            else 
                srev = " SECOND REVISION";

            stitle = sowner + " RESPONSE RATE REPORTS FOR " + srev;

            //Add a title
            xlWorkSheet.Cells[line_start, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange;

            titleRange = xlApp.get_Range(xlWorkSheet.Cells[line_start, 1], xlWorkSheet.Cells[line_start, dt.Columns.Count]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 25;

            ////Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Populate headers, assume row[0] contains the titles and row[3] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[line_start+2, 1], xlWorkSheet.Cells[line_start + 2, dt.Columns.Count]);
            cellRange.Font.Bold = true;

            xlWorkSheet.Cells[line_start + 2, 1] = "Type of Construction";
            xlWorkSheet.Cells[line_start + 2, 2] = "Rate";
            int j = 0;
            if (rev == "1")
                j = 1;
            else if (rev == "2")
                j = 2;

            DateTime ddt = new DateTime(Convert.ToInt16(sdate.Substring(0, 4)), Convert.ToInt16(sdate.Substring(4, 2)), 1);
            for (int i = 3; i < dt.Columns.Count+1; i++)
            {
                xlWorkSheet.Cells[line_start + 2, i] = "\t" + ddt.AddMonths(-j).ToString("yyyyMM");
                j = j + 1;
            }

            //Setup the column header row 

            //Set the font size, text wrap of columns and format for the entire worksheet

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 25;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[2, Type.Missing]).ColumnWidth = 35;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[2, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            for (int i = 3; i <= dt.Columns.Count; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 16;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0.00";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }

            ////Populate rest of the data. Start at row[4] 
            int iRow = line_start + 2; //We start at row 5

            int iCol = 0;

            foreach (DataRow r in dt.Rows)
            {
                iCol = 0;
                iRow++;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;

                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }

        }

        private void cbRev_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
                GetData();
        }
    }
}
