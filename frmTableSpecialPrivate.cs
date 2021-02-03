/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : frmTableSpecialPrivate.cs
 Programmer    : Christine Zhang
 Creation Date : 6/28/2017
 Inputs        : N/A
 Parameters    : N/A
 Output        : N/A
 Description   : This program will display screen to Special Private table 
                 the user can create related excel file
 Detail Design :
 Other         : Called by: Publication -> Special Private Table
 Revisions     : See Below
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

namespace Cprs
{
    public partial class frmTableSpecialPrivate : frmCprsParent
    {
        public frmTableSpecialPrivate()
        {
            InitializeComponent();
        }

        private TableSpecialPrivateData data_object;
        private frmMessageWait waiting;
        private string saveFilename;

        private void frmTableSpecialPrivate_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            //define data object;
            data_object = new TableSpecialPrivateData();

            //load data
            LoadData();
        }

        //load data base on the radiobox selected
        private void LoadData()
        {
            DataTable dt = new DataTable();

            string series = string.Empty;

            //get radio box value
            if (rdRnsa.Checked)
            {
                series = "V00XXUNA";
                lblTitle.Text = "Value of Private Residential Construction Put in Place";
                lblTitle2.Text = "Not Seasonally Adjusted";
            }
            else if (rdRsa.Checked)
            {
                series = "V00XXSAA";
                lblTitle.Text = "Value of Private Residential Construction Put in Place";
                lblTitle2.Text = "Seasonally Adjusted Annual Rate";
            }
            else if (rdNnsa.Checked)
            {
                series = "VNRXXUNA";
                lblTitle.Text = "Value of Private Nonresidential Construction Put in Place";
                lblTitle2.Text = "Not Seasonally Adjusted";
            }
            else
            {
                series = "VNRXXSAA";
                lblTitle.Text = "Value of Private Nonresidential Construction Put in Place";
                lblTitle2.Text = "Seasonally Adjusted Annual Rate";
            }

            //get data from specific table 
            dt = data_object.GetSpecialPrivateTable(series);

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.DefaultCellStyle.Format = "N0";
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.DataSource = dt;

            dgData.Columns[0].HeaderText = "Year";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgData.Columns[1].HeaderText = "Jan";
            dgData.Columns[2].HeaderText = "Feb";
            dgData.Columns[3].HeaderText = "Mar";
            dgData.Columns[4].HeaderText = "Apr";
            dgData.Columns[5].HeaderText = "May";
            dgData.Columns[6].HeaderText = "Jun";
            dgData.Columns[7].HeaderText = "Jul";
            dgData.Columns[8].HeaderText = "Aug";
            dgData.Columns[9].HeaderText = "Sep";
            dgData.Columns[10].HeaderText = "Oct";
            dgData.Columns[11].HeaderText = "Nov";
            dgData.Columns[12].HeaderText = "Dec";

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }


        private void frmTableSpecialPrivate_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "EXIT");
        }

        private void rdRnsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdRnsa.Checked)
            {
                rdRsa.Checked = false;
                rdNnsa.Checked = false;
                rdNsa.Checked = false;
                LoadData();
            }
        }

        private void rdRsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdRsa.Checked)
            {
                rdRnsa.Checked = false;
                rdNnsa.Checked = false;
                rdNsa.Checked = false;
                LoadData();
            }
        }

        private void rdNnsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNnsa.Checked)
            {
                rdRsa.Checked = false;
                rdRnsa.Checked = false;
                rdNsa.Checked = false;
                LoadData();
            }
        }

        private void rdNsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNsa.Checked)
            {
                rdRsa.Checked = false;
                rdNnsa.Checked = false;
                rdRnsa.Checked = false;
                LoadData();
            }
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
            if (rdRnsa.Checked)
                saveFileDialog1.FileName = "residentialnsa.xls";
            else if (rdRsa.Checked)
                saveFileDialog1.FileName = "residentialsa.xls";
            else if (rdNnsa.Checked)
                saveFileDialog1.FileName = "nonresidentialnsa.xls";
            else
                saveFileDialog1.FileName = "nonresidentialsa.xls";

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

            string tsar_series;
            string stitle;
            string sfilename;
            string ssheetname;

            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            for (int i = 1; i <= 4; i++)
            {
                if (i == 1)
                {
                    tsar_series = "V00XXUNA";
                    stitle = "Value of Private Residential Construction Put in Place excluding rental, vacant, and seasonal residential improvements - Not Seasonally Adjusted";
                    sfilename = dir + "\\residentialnsa.xls";
                    ssheetname = "Priv-Res NSA";
                }
                else if (i == 2)
                {
                    tsar_series = "V00XXSAA";
                    stitle = "Value of Private Residential Construction Put in Place excluding rental, vacant, and seasonal residential improvements - Seasonally Adjusted Annual Rate";
                    sfilename = dir + "\\residentialsa.xls";
                    ssheetname = "Priv-Res SA";
                }
                else if (i == 3)
                {
                    tsar_series = "VNRXXUNA";
                    stitle = "Value of Private Nonresidential Construction Put in Place - Not Seasonally Adjusted";
                    sfilename = dir + "\\nonresidentialnsa.xls";
                    ssheetname = "Priv-Nonres NSA";
                }
                else
                {
                    tsar_series = "VNRXXSAA";
                    stitle = "Value of Private Nonresidential Construction Put in Place - Seasonally Adjusted Annual Rate";
                    sfilename = dir + "\\nonresidentialsa.xls";
                    ssheetname = "Priv-Nonres SA";
                }

                DataTable dt = data_object.GetSpecialPrivateTable(tsar_series);
                ExportToExcel(dt, stitle, sfilename, ssheetname);
            }


            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();

        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");
        }


        private void ExportToExcel(DataTable dt, string stitle, string sfilename, string ssheetname)
        {
            //get current month
            string smon  = GeneralDataFuctions.GetCurrMonthDateinTable().Substring(4, 2);
            
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Declare Excel Interop variables
            Microsoft.Office.Interop.Excel.Application xlApp;
            Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
            Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 50;
            xlWorkSheet.PageSetup.RightMargin = 50;
            xlWorkSheet.PageSetup.BottomMargin = 50;
            xlWorkSheet.PageSetup.LeftMargin = 50;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through M
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, "M"]);
            titleRange.Merge(Type.Missing);

            //Center the title horizontally then vertically at the above defined range
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            //Increase the font-size of the title
            titleRange.Font.Size = 8;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.Font.Name = "Arial";

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[2, 1] = "(Millions of Dollars)";

            //Span the title across columns A through M
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, "M"]);
            titleRange2.Merge(Type.Missing);

            //Center the title horizontally then vertically at the above defined range
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter;

            //Increase the font-size of the title
            titleRange2.Font.Size = 8;
            titleRange2.Font.Name = "Arial";

            //Populate headers, assume row[0] contains the titles and row[3] contains all the headers
            int iCol = 0;
            foreach (DataColumn c in dt.Columns)
            {
                iCol++;
                xlWorkSheet.Cells[3, iCol] = dgData.Columns[iCol - 1].HeaderText;
            }

            //Setup the column header row (row 3)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, "M"]);

            //Set the header row fonts bold
            headerRange.Font.Bold = true;

            //Center the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Populate rest of the data. Start at row[3] since row[1][2] contains titles and row[0] contains headers
            int iRow = 3; //We start at row 3
            int last_row = dt.Rows.Count + iRow*2;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;
                
                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                }
               // iRow++;
            }

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M" };
            foreach (string s in strColumns)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).Font.Size = 8;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).Font.Name = "Arial";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).WrapText = true;
                
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.0;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).RowHeight = 17.50;

                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,#";
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns["A", Type.Missing]).Font.Bold = true;
                }
            }

            //if May, set up two years prior with subscript r
            if (smon == "05")
            {
                string previous2_year = dt.Rows[dt.Rows.Count - 3][0].ToString();
                xlWorkSheet.Cells[iRow - 2, 1] = previous2_year + "r";
                Microsoft.Office.Interop.Excel.Range cellRange0 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 2, "A"], xlWorkSheet.Cells[iRow - 2, "A"]);
                cellRange0.Characters[5, 1].Font.Superscript = true;
                cellRange0.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }

            //If Jan, Feb  or May, set up prior year with subscript r
            if (smon == "05" || Convert.ToInt16(smon) < 3)
            {
                string previous_year = dt.Rows[dt.Rows.Count - 2][0].ToString();
                xlWorkSheet.Cells[iRow - 1, 1] = previous_year + "r";
                Microsoft.Office.Interop.Excel.Range cellRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow - 1, "A"], xlWorkSheet.Cells[iRow - 1, "A"]);
                cellRange1.Characters[5, 1].Font.Superscript = true;
                cellRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            }

            if (smon == "01")
            {
                //set up current years with subscript p
                string last_year = dt.Rows[dt.Rows.Count - 1][0].ToString();
                xlWorkSheet.Cells[iRow, 1] = last_year + "p";
                Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                cellRange.Characters[5, 1].Font.Superscript = true;
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                //add text after grid
                iRow = iRow + 1;
                Microsoft.Office.Interop.Excel.Range txtRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "M"]);
                txtRange.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow, 1] = "p Preliminary r Revised";
                txtRange.Characters[1, 1].Font.Superscript = true;
                txtRange.Characters[15, 1].Font.Superscript = true;
            }
            else
            {
                //set up current years with subscript r
                string last_year = dt.Rows[dt.Rows.Count - 1][0].ToString();
                xlWorkSheet.Cells[iRow, 1] = last_year + "r";
                Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                cellRange.Characters[5, 1].Font.Superscript = true;
                cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

                //add text after grid
                iRow = iRow + 1;
                Microsoft.Office.Interop.Excel.Range txtRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "M"]);
                txtRange.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow, 1] = "r Revised";
                txtRange.Characters[1, 1].Font.Superscript = true;
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

         
            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = true;
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitle = lblTitle2.Text + " - Millions of Dollars";
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
      
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Special Private Table";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            //resize the note column
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (int i = 0; i <= 12; i++)
            {
                dgData.Columns[i].Width = 75;
            }

            printer.PrintDataGridViewWithoutDialog(dgData);

            //resize back the note column
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Cursor.Current = Cursors.Default;

        }

       
    }
}
