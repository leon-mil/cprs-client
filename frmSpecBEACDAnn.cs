/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecBEACDAnn.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 10/17/2018
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display Annual Special BEA tabulation

Detailed Design : Detailed Design for special bea Annual
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
    public partial class frmSpecBEACDAnn : frmCprsParent
    {
        public frmSpecBEACDAnn()
        {
            InitializeComponent();
        }

        private SpecBEAData data_object;
        private int cur_year;
        private string sdate;
        private int sel_factor;
        private string subtc= string.Empty;
        private bool show_subtc;
        private DataTable stored_main = null;
        private bool form_loading = false;
        private string saveFilename;
        private frmMessageWait waiting;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private void frmSpecBEACDAnn_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            form_loading = true;

            data_object = new SpecBEAData();
            sdate = data_object.GetSdateFromVIPBea();

            cur_year = Convert.ToInt16(sdate.Substring(0, 4));
           
            if (Convert.ToInt16(sdate.Substring(4, 2)) > 2)
            {
                cbYear.Items.Add(cur_year - 1);
                cbYear.Items.Add(cur_year - 2);
                cbYear.Items.Add(cur_year - 3);
                cbYear.Items.Add(cur_year - 4);
                cbYear.Items.Add(cur_year - 5);
            }
            else
            {
                cbYear.Items.Add(cur_year - 2);
                cbYear.Items.Add(cur_year - 3);
                cbYear.Items.Add(cur_year - 4);
                cbYear.Items.Add(cur_year - 5);
                cbYear.Items.Add(cur_year - 6);
            }

            sel_factor = 0;
            subtc = "";
            show_subtc = false;
            cbYear.SelectedIndex = 0;

            data_object = new SpecBEAData();
            stored_main = data_object.GetFedBeaAnnTable(cbYear.Text,  sel_factor);
            DataTable tblFiltered = stored_main.AsEnumerable()
                                    .Where(row => row.Field<int>("ddown") <=2)
            .OrderBy(row => row.Field<String>("newtc"))
            .CopyToDataTable();

            dgData.DataSource = tblFiltered;
            
            //set up column header
            SetColumnHeader();

            form_loading = false;       
        }

        private void LoadData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DataTable tblFiltered;

            stored_main = data_object.GetFedBeaAnnTable(cbYear.Text, sel_factor);

            //Get current data
            
            tblFiltered = stored_main.AsEnumerable()
            .Where(row => row.Field<int>("ddown") <= 2)
            .OrderBy(row => row.Field<String>("newtc"))
            .CopyToDataTable();
            
            dgData.DataSource = tblFiltered;

            //set up column header
            SetColumnHeader();

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
            
            for (int i = 1; i <= 12; i++)
            {
                int j1 = 3 * i - 2;
                int mm = 13 - i;
                if (mm >=10)
                    dgData.Columns[j1].HeaderText = "Civil " + cbYear.Text + mm;
                else
                    dgData.Columns[j1].HeaderText = "Civil " + cbYear.Text + "0"+mm;
                dgData.Columns[j1].DefaultCellStyle.Format = "N0";
                dgData.Columns[j1].DefaultCellStyle.NullValue = "0";
                dgData.Columns[j1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                int j2 = 3 * i -1;
                if (mm >=10)
                    dgData.Columns[j2].HeaderText = "Defense " + cbYear.Text + mm;
                else
                    dgData.Columns[j2].HeaderText = "Defense " + cbYear.Text + "0" + mm;
                dgData.Columns[j2].DefaultCellStyle.Format = "N0";
                dgData.Columns[j2].DefaultCellStyle.NullValue = "0";
                dgData.Columns[j2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                int j3 = 3 * i;
                if (mm >=10)
                    dgData.Columns[j3].HeaderText = "Total " + cbYear.Text + mm;
                else
                    dgData.Columns[j3].HeaderText = "Total " + cbYear.Text + "0" + mm;
                dgData.Columns[j3].DefaultCellStyle.Format = "N0";
                dgData.Columns[j3].DefaultCellStyle.NullValue = "0";
                dgData.Columns[j3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }


            dgData.Columns[37].HeaderText = "Civil " + cbYear.Text;
            dgData.Columns[37].DefaultCellStyle.Format = "N0";
            dgData.Columns[37].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[38].HeaderText = "Defense " + cbYear.Text;
            dgData.Columns[38].DefaultCellStyle.Format = "N0";
            dgData.Columns[38].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[39].HeaderText = "Total " + cbYear.Text;
            dgData.Columns[39].DefaultCellStyle.Format = "N0";
            dgData.Columns[39].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[40].Visible = false;
            dgData.Columns[41].Visible = false;
            dgData.Columns[42].Visible = false;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }


        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
                LoadData();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            if (sel_factor ==0)
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "BEA Annual Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            dgData.Columns[0].Width = 120;
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 200;
            
            Cursor.Current = Cursors.Default;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "FedBEAAnn.xls";

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

        private void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }


        private void frmSpecBEACDAnn_FormClosing(object sender, FormClosingEventArgs e)
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
                
                subtc = dgData.CurrentRow.Cells[0].Value.ToString().Substring(0,2);

                //Get data
                DataTable tblFiltered = stored_main.AsEnumerable()
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
                LoadData();
                lbllabel.Text = "Double Click a Line to Expand";
            }

            //set up column header
            SetColumnHeader();
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\FedBEAAnn.xls";

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            if (Convert.ToInt16(sdate.Substring(4, 2)) > 2)
            {
                ////create sheet tables
                ExportToExcel1((cur_year - 5).ToString());
                ExportToExcel1((cur_year - 4).ToString());
                ExportToExcel1((cur_year - 3).ToString());
                ExportToExcel1((cur_year - 2).ToString());
                ExportToExcel1((cur_year - 1).ToString());
            }
            else
            {
                ////create sheet tables
                ExportToExcel1((cur_year - 6).ToString());
                ExportToExcel1((cur_year - 5).ToString());
                ExportToExcel1((cur_year - 4).ToString());
                ExportToExcel1((cur_year - 3).ToString());
                ExportToExcel1((cur_year - 2).ToString());
            }

           
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

        private void ExportToExcel1(string sel_year)
        {
            string stitle = string.Empty;
            stitle = "FEDERAL VIP NOT SEASONALLY ADJUSTED ";

            string subtitle = "Thousand of Dollars - " + lblFactor.Text;
            string tabname = sel_year;

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = tabname;

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
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 40]);
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
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, 40]);
            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Bold = true;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, 40]);
            cellRange.Font.Bold = true;
            cellRange.RowHeight = 36;
            xlWorkSheet.Cells[4, 1] = "Type of Construction";
            for (int i = 1; i <= 12; i++)
            {
                int j = 3 * i - 1;
                int mm = 13 - i;
                if (mm >= 10)
                    xlWorkSheet.Cells[4, j] = "Civil \n" + sel_year + mm;
                else
                    xlWorkSheet.Cells[4, j] = "Civil \n" + sel_year + "0" + mm;
               
                j = 3 * i;
                if (mm >= 10)
                    xlWorkSheet.Cells[4, j] = "Defense \n" + sel_year + mm;
                else
                    xlWorkSheet.Cells[4, j] = "Defense \n" + sel_year + "0" + mm;

                j = 3 * i+1;
                if (mm>=10)
                    xlWorkSheet.Cells[4, j] = "Total \n" + sel_year + mm;
                else
                    xlWorkSheet.Cells[4, j] = "Total \n" + sel_year + "0" + mm;
            }
            xlWorkSheet.Cells[4, 38] = "Civil \n" + sel_year;
            xlWorkSheet.Cells[4, 39] = "Defense \n" + sel_year;
            xlWorkSheet.Cells[4, 40] = "Total \n" + sel_year;

            ///*for datatable */
            DataTable dt = data_object.GetFedBeaAnnTable(sel_year, sel_factor);

            ////Setup the column header row (row 5)

            ////Set the font size, text wrap of columns and format for the entire worksheet
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 30; 
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 2; i <= 40; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 10;
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
                    iCol++;
                    if (iCol ==1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t"+ r[c.ColumnName].ToString();
                    else if (iCol <=40)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }
        }
    }
}
