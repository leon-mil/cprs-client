/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTableMonRelease.cs
Programmer    : Christine Zhang
Creation Date : Aug. 7 2017
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create Table monthly release screen to view related data
                and create excels
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :3/11/2019
Modified By   :Christine
Keyword       :
Change Request:3002
Description   :Add an annual button to create annual tables
***********************************************************************
Modified Date :8/5/2019
Modified By   :Christine
Keyword       :
Change Request:3003
Description   :make the annual button enable on survey month June in 2019
***********************************************************************
Modified Date :6/23/2020
Modified By   :Christine
Keyword       :
Change Request:
Description   :get current survey month from SAATOT table instead of VIPSADJ table
***********************************************************************
Modified Date :2/1/2021
Modified By   :Christine
Keyword       :
Change Request:CR#7920
Description   :Update column and related footnote (S) to X
***********************************************************************
Modified Date :1/31/2022
Modified By   :Christine
Keyword       :
Change Request:
Description   :create annual table from 2012 instead of 2009
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
    public partial class frmTableMonRelease : frmCprsParent
    {

        private TableMonReleaseData data_object;
        private frmMessageWait waiting;
        private string saveFilename;
        private string sdate;  /*for monthly date from SAATOT */
     
        //permonth variables
        private string pm1;
        private string pm2;
        private string pm3;
        private string pm4;
        private string pm5;
        private string curmons;
        private string pmon1 ;
        private string pmon2 ;
        private string pmon3;
        private string pmon4;
        private string pmon5;
        private string foottitle;
        private string MYD;

        private string table_type = string.Empty;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private delegate void ShowMessageDelegate();

        public frmTableMonRelease()
        {
            InitializeComponent();
        }

        private void frmTableMonRelease_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            //define data object;
            data_object = new TableMonReleaseData();

            //get survey month
            sdate = GeneralDataFuctions.GetMaxMonthDateinAnnTable();
           
            //only Jan, Feb, April and Dec. can show the annual table button
            int curmon = Convert.ToInt16(sdate.Substring(4, 2));
            if (curmon == 1 || curmon == 2 || curmon == 4 || curmon == 12)
                btnAnnual.Enabled = true;
            else
                btnAnnual.Enabled = false;
           
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

            lblt1.Text = "Percent Change " + curmons + " from -";

            //set seasonal adjust and total as default
            rdsa.Checked = true;
            rdt.Checked = true;

            GetData(); 
            
        }
      
        private void GetData()
        {
            int tno = 0;

            if (rdsa.Checked)
                tno = 1;
            else 
                tno = 2;

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
           
            SetUpdateTitle(tno, survey_type);
            if (tno ==1)
                dgData.DataSource = data_object.GetTableSAAData(survey_type, sdate, pm1, pm2, pm3, pm4, pm5);
            else
                dgData.DataSource = data_object.GetTableUNAData(survey_type, sdate, pm1, pm2, pm3, pm4, pm5);
            SetColumnHeader(tno);

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SetUpdateTitle(int tno, string survey_type)
        {
            if (survey_type == "T")
                lblTitle.Text = "Value of Construction Put in Place";
            else if (survey_type == "V")
                lblTitle.Text = "Value of Private Construction Put in Place";
            else if (survey_type == "P")
                lblTitle.Text = "Value of Public Construction Put in Place";
            else if (survey_type == "S")
                lblTitle.Text = "Value of State and Local Construction Put in Place";
            else
                lblTitle.Text = "Value of Federal Construction Put in Place";

            if (tno == 1)
            {
                lblTitle2.Text = "Seasonally Adjusted Annual Rate";
            }
            else if (tno == 2)
            {
                lblTitle2.Text = "Not Seasonally Adjusted";
            }

            if ((survey_type == "T") || (survey_type == "P"))
                lblTitle3.Text = "(Millions of dollars. Details may not add to totals due to rounding.)";
            else
                lblTitle3.Text = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
        }

        private void SetColumnHeader(int tno)
        {
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            if (tno == 1)
            {
                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 300;
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
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = pmon5;
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].HeaderText = pmon1;
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[8].HeaderText = pmon5;
                dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (!rdf.Checked)
                {
                    dgData.Columns[5].Width = 95;
                    dgData.Columns[6].Width = 95;
                    dgData.Columns[7].Width = 96;
                    dgData.Columns[8].Width = 96;
                }
                else
                {
                    dgData.Columns[5].Width = 100;
                    dgData.Columns[6].Width = 100;
                    dgData.Columns[7].Width = 100;
                    dgData.Columns[8].Width = 100;
                }
                
                lblt1.Visible = true;
                lblt2.Visible = false;
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
              
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[8].HeaderText = pmon5;
                
                dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[9].HeaderText = "Percent Change";
                
                dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (!rdf.Checked)
                {
                    dgData.Columns[7].Width = 88;
                    dgData.Columns[8].Width = 88;
                    dgData.Columns[9].Width = 88;
                }
                else
                {
                   
                    dgData.Columns[7].Width = 94;
                    dgData.Columns[8].Width = 94;
                    dgData.Columns[9].Width = 94;
                }
                lblt1.Visible = false;
                lblt2.Visible = true;
                
            }  
        }


        private void rdsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdsa.Checked)
            {
                rdnsa.Checked = false;
                GetData();
            }
        }

        private void rdnsa_CheckedChanged(object sender, EventArgs e)
        {
            if (rdnsa.Checked)
            {
                rdsa.Checked = false;
                GetData();
            }
        }

        private void rdt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdt.Checked)
            {
                rdf.Checked = false;
                rds.Checked = false;
                rdv.Checked = false;
                rdp.Checked = false;

                GetData();
            }
        }

        private void rdv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdv.Checked)
            {
                rdt.Checked = false;
                rds.Checked = false;
                rdf.Checked = false;
                rdp.Checked = false;

                GetData();
            }
        }

        private void rdp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdp.Checked)
            {
                rdt.Checked = false;
                rds.Checked = false;
                rdv.Checked = false;
                rdf.Checked = false;

                GetData();
            }
        }

        private void rds_CheckedChanged(object sender, EventArgs e)
        {
            if (rds.Checked)
            {
                rdt.Checked = false;
                rdf.Checked = false;
                rdv.Checked = false;
                rdp.Checked = false;

                GetData();
            }
        }

        private void rdf_CheckedChanged(object sender, EventArgs e)
        {
            if (rdf.Checked)
            {
                rdt.Checked = false;
                rds.Checked = false;
                rdv.Checked = false;
                rdp.Checked = false;

                GetData();
            }
        }

        private void frmTableMonRelease_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "EXIT");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text + " " + lblTitle2.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitle = lblTitle3.Text;
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Monthly Release Table";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            //resize column
            dgData.Columns[0].Width = 150;

            printer.PrintDataGridViewWithoutDialog(dgData);

            if (rdsa.Checked)
                dgData.Columns[0].Width = 300;
            else if (rdnsa.Checked)
                dgData.Columns[0].Width = 280;
            
            Cursor.Current = Cursors.Default;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "totsa.xls";

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
            string ssheetname;
            string sfilename;

            DataTable dtab;

            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
            DateTime sDate = dt.AddMonths(+2);
            int month1 = sDate.Month;

            //Get frist work day
            DateTime firstworkday = GeneralFunctions.GetFirstBusinessDay(sDate.Year, sDate.Month);

            MYD = DateTimeFormatInfo.CurrentInfo.GetMonthName(firstworkday.Month) + " " + firstworkday.Day + ", " + sDate.Year;

            if (month1 == 1 || month1 == 2 || month1 == 3 || month1 == 12)
              foottitle = "10:00 AM EST " + firstworkday.ToString("MM/dd/yy");
            else
              foottitle = "10:00 AM EDT " + firstworkday.ToString("MM/dd/yy");

            if (table_type == "Month")
            {
                //create sheet tables
                dtab = data_object.GetTableSAAData("T", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Construction Put in Place - Seasonally Adjusted Annual Rate";
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                sfilename = dir + "\\totsa.xls";
                ssheetname = "Total SA";
                ExportToExcelT(sfilename, dtab, stitle, subtitle, "SA", ssheetname);

                dtab = data_object.GetTableUNAData("T", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Construction Put in Place - Not Seasonally Adjusted";
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                sfilename = dir + "\\tot.xls";
                ssheetname = "Total NSA";
                ExportToExcelT(sfilename, dtab, stitle, subtitle, "NSA", ssheetname);

                dtab = data_object.GetTableSAAData("V", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Private Construction Put in Place - Seasonally Adjusted Annual Rate";
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\privsa.xls";
                ssheetname = "Priv SA";
                ExportToExcelV(sfilename, dtab, stitle, subtitle, "SA", ssheetname);

                dtab = data_object.GetTableUNAData("V", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Private Construction Put in Place - Not Seasonally Adjusted";
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\priv.xls";
                ssheetname = "Priv NSA";
                ExportToExcelV(sfilename, dtab, stitle, subtitle, "NSA", ssheetname);

                dtab = data_object.GetTableSAAData("P", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Public Construction Put in Place - Seasonally Adjusted Annual Rate";
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                sfilename = dir + "\\pubsa.xls";
                ssheetname = "Pub SA";
                ExportToExcelP(sfilename, dtab, stitle, subtitle, "SA", ssheetname);

                dtab = data_object.GetTableUNAData("P", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Public Construction Put in Place - Not Seasonally Adjusted";
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                sfilename = dir + "\\pub.xls";
                ssheetname = "Pub NSA";
                ExportToExcelP(sfilename, dtab, stitle, subtitle, "NSA", ssheetname);

                dtab = data_object.GetTableSAAData("S", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of State and Local Construction Put in Place - Seasonally Adjusted Annual Rate";
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\slsa.xls";
                ssheetname = "State SA";
                ExportToExcelS(sfilename, dtab, stitle, subtitle, "SA", ssheetname);

                dtab = data_object.GetTableUNAData("S", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of State and Local Construction Put in Place - Not Seasonally Adjusted";
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\sl.xls";
                ssheetname = "State NSA";
                ExportToExcelS(sfilename, dtab, stitle, subtitle, "NSA", ssheetname);

                dtab = data_object.GetTableSAAData("F", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Federal Construction Put in Place - Seasonally Adjusted Annual Rate";
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\fedsa.xls";
                ssheetname = "Fed SA";
                ExportToExcelF(sfilename, dtab, stitle, subtitle, "SA", ssheetname);

                dtab = data_object.GetTableUNAData("F", sdate, pm1, pm2, pm3, pm4, pm5);
                stitle = "Value of Federal Construction Put in Place - Not Seasonally Adjusted";
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\fed.xls";
                ssheetname = "Fed NSA";
                ExportToExcelF(sfilename, dtab, stitle, subtitle, "NSA", ssheetname);
            }
            else
            {
                //  string start_year = "2008";
                //change start year from 2009 to 2012
                string start_year = "2012";
                int month2 = Convert.ToInt32(sdate.Substring(4,2));
                string end_year;
                if (month2 == 12)
                    end_year = (Convert.ToInt16(sdate.Substring(0, 4))).ToString();
                else
                    end_year = (Convert.ToInt16(sdate.Substring(0,4)) -1).ToString();

                //comment out for 2020, it will show 11 years data .
               // if (((Convert.ToInt16(end_year) - Convert.ToInt16(start_year)) > 10) && ((Convert.ToInt16(end_year) - Convert.ToInt16(start_year))<=20))
               //     start_year = "2018";

                //create sheet tables
                dtab = data_object.GetAnnualReleaseData("T", start_year, end_year);
                stitle = "Annual Value of Construction Put in Place " + start_year + " - " + end_year;
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                sfilename = dir + "\\Total.xls";
                ssheetname = "Total";
                ExportAnnualToExcel("T", sfilename, dtab, stitle, subtitle, ssheetname, start_year, end_year);

                //create sheet tables
                dtab = data_object.GetAnnualReleaseData("V", start_year, end_year);
                stitle = "Annual Value of Private Construction Put in Place " + start_year + " - " + end_year;
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\Private.xls";
                ssheetname = "Private";
                ExportAnnualToExcel("V", sfilename, dtab, stitle, subtitle, ssheetname, start_year, end_year);

                dtab = data_object.GetAnnualReleaseData("P", start_year, end_year);
                stitle = "Annual Value of Public Construction Put in Place " + start_year + " - " + end_year;
                subtitle = "(Millions of dollars. Details may not add to totals due to rounding.)";
                sfilename = dir + "\\Public.xls";
                ssheetname = "Public";
                ExportAnnualToExcel("P", sfilename, dtab, stitle, subtitle, ssheetname, start_year, end_year);

                dtab = data_object.GetAnnualReleaseData("S", start_year, end_year);
                stitle = "Annual Value of State and Local Construction Put in Place " + start_year + " - " + end_year;
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\State.xls";
                ssheetname = "State";
                ExportAnnualToExcel("S", sfilename, dtab, stitle, subtitle, ssheetname, start_year, end_year);

                dtab = data_object.GetAnnualReleaseData("F", start_year, end_year);
                stitle = "Annual Value of Federal Construction Put in Place " + start_year + " - " + end_year;
                subtitle = "(Millions of dollars. Details may not add to totals since all types of construction are not shown separately.)";
                sfilename = dir + "\\Federal.xls";
                ssheetname = "Federal";
                ExportAnnualToExcel("F", sfilename, dtab, stitle, subtitle, ssheetname, start_year, end_year);
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

        //create excels for total
        private void ExportToExcelT(string sfilename, DataTable dt, string stitle, string subtitle, string tsar_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            string smon = sdate.Substring(4, 2);

            string last_col = "A";
            if (tsar_type == "SA")
                last_col = "I";
            else
                last_col = "J";

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;
            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
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
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add percent change
            if (tsar_type == "SA")
                xlWorkSheet.Cells[4, 8] = "Percent change\n" + curmons + " from -"; 
            else
                xlWorkSheet.Cells[4, 8] = "Year-to-date";

            //Span the title across columns H through I
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            titleRange3.Merge(Type.Missing);
            if (tsar_type == "SA")
                titleRange3.RowHeight = 22;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

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

            if (tsar_type == "SA")
            {
                xlWorkSheet.Cells[5, 8] = pmm1;
                xlWorkSheet.Cells[5, 9] = pmm5;
            }
            else
            {
                xlWorkSheet.Cells[5, 8] = cmm;
                xlWorkSheet.Cells[5, 9] = pmm5;
                xlWorkSheet.Cells[5, 10] = "Percent\n Change";
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, last_col]);

            //Set the header row fonts bold
            //  headerRange.Font.Bold = true;
            headerRange.RowHeight = 24;
            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            if (tsar_type == "SA")
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            else
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.50;
                    if ((tsar_type == "SA" && (s == "H" || s== "I")) || (tsar_type == "NSA" && s== "J"))
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
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

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                if (iRow == 7 || iRow == 29 || iRow == 46)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;

                    //set up Total Private Construction with subscript 1
                    if (iRow == 29)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                        
                        cellRange.Characters[27, 1].Font.Superscript = true;
                    }
                    else if (iRow == 46)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Public Construction2";
                        
                        cellRange.Characters[26, 1].Font.Superscript = true;
                        //if (tsar_type != "SA")
                            xlWorkSheet.HPageBreaks.Add(cellRange);

                    }
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of private construction not shown separately:";
            footRange2.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, last_col]);
            footRange3.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, last_col]);
            footRange4.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 7, 1] = "2Includes the following categories of public construction not shown separately:";
            footRange4.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, last_col]);
            footRange5.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 8, 1] = "lodging, religious, communication, and manufacturing.";

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, last_col]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 10, 1] = "Source: U.S.Census Bureau, Construction Spending, " + MYD + ". Additional information on the survey methodology may be found at ";

            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, last_col]);
            footRange8.Merge(Type.Missing);
            footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 11, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange8.Font.Name = "Arial";
            footRange8.Font.Size = 8;

            Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, last_col]);
            footRange9.Merge(Type.Missing);
            footRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[iRow + 13, 1] = foottitle;

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$6";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 40;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 10;
            xlWorkSheet.PageSetup.LeftMargin = 80;

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


        //create excels for private
        private void ExportToExcelV(string sfilename, DataTable dt, string stitle, string subtitle, string tsar_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            string smon = sdate.Substring(4, 2);

            string last_col = "A";
            if (tsar_type == "SA")
                last_col = "I";
            else
                last_col = "J";

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
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
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
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add percent change
            if (tsar_type == "SA")
                xlWorkSheet.Cells[3, 8] = "Percent change\n" + curmons + " from -";
            else
                xlWorkSheet.Cells[3, 8] = "Year-to-date";

            //Span the title across columns H through I
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[3, "H"], xlWorkSheet.Cells[3, last_col]);
            titleRange3.Merge(Type.Missing);
            if (tsar_type == "SA")
                titleRange3.RowHeight = 22;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[3, "H"], xlWorkSheet.Cells[3, last_col]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[4, 1] = "Type of Construction:";
            xlWorkSheet.Cells[4, 2] = cmm + "p";
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 2], xlWorkSheet.Cells[4, 2]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[4, 3] = pmm1 + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 3], xlWorkSheet.Cells[4, 3]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            xlWorkSheet.Cells[4, 4] = pmm2 + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 4], xlWorkSheet.Cells[4, 4]);
            cellRange.Characters[9, 1].Font.Superscript = true;
            if (smon == "05")
            {
                xlWorkSheet.Cells[4, 5] = pmm3 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 5], xlWorkSheet.Cells[4, 5]);
                cellRange.Characters[9, 1].Font.Superscript = true;
                xlWorkSheet.Cells[4, 6] = pmm4 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 6], xlWorkSheet.Cells[4, 6]);
                cellRange.Characters[9, 1].Font.Superscript = true;
                xlWorkSheet.Cells[4, 7] = pmm5 + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[4, 7], xlWorkSheet.Cells[4, 7]);
                cellRange.Characters[9, 1].Font.Superscript = true;
            }
            else
            {
                xlWorkSheet.Cells[4, 5] = pmm3;
                xlWorkSheet.Cells[4, 6] = pmm4;
                xlWorkSheet.Cells[4, 7] = pmm5;
            }

            if (tsar_type == "SA")
            {
                xlWorkSheet.Cells[4, 8] = pmm1;
                xlWorkSheet.Cells[4, 9] = pmm5;
            }
            else
            {
                xlWorkSheet.Cells[4, 8] = cmm;
                xlWorkSheet.Cells[4, 9] = pmm5;
                xlWorkSheet.Cells[4, 10] = "Percent\n Change";
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, last_col]);

            //Set the header row fonts bold
            headerRange.RowHeight = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            if (tsar_type == "SA")
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            else
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.50;
                    if ((tsar_type == "SA" && (s == "H" || s == "I")) || (tsar_type == "NSA" && s == "J"))
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    if (tsar_type == "SA")
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 34.00;
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 30.00;
                }

            }

            //Populate rest of the data. Start at row[6] 
            int iRow = 5; //We start at row 6

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

                //make row smaller
                if (iRow == 5 || iRow == 7 || iRow == 11 || iRow == 13 || iRow == 15 || iRow == 19 || iRow == 39 || iRow == 44 || iRow == 54 || iRow == 59 || iRow == 67 || iRow == 71 || iRow == 73 || iRow == 76)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "I"]);
                    cellRange.RowHeight = 9;
                }

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                if (iRow == 6 || iRow == 8 || iRow == 12 || iRow ==14 || iRow == 16 || iRow ==20 || iRow ==40 || iRow == 45 || iRow ==55 ||iRow ==60 || iRow == 68 || iRow == 72 || iRow == 74 || iRow == 77)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;

                    //set up Total Private Construction with subscript 1
                    if (iRow == 6)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                        cellRange.RowHeight = 12;
                        cellRange.Characters[27, 1].Font.Superscript = true;
                    }
                    else if (iRow == 8)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Residential (inc. Improvements)2";
                        cellRange.RowHeight = 12;
                        cellRange.Characters[32, 1].Font.Superscript = true;
                    }
                    else if (iRow == 45)
                    {
                        xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 3, 1] = "1Total private construction includes the following categories of construction not shown separately:";
            footRange2.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, last_col]);
            footRange3.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "public safety, highway and street, sewage and waste disposal, water supply, and conservation and development.";

            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, last_col]);
            footRange4.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "2Private residential improvements does not include expenditures to rental, vacant, or seasonal properties.";
            footRange4.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, last_col]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 6, 1] = "Source: U.S.Census Bureau, Construction Spending, " + MYD + "." + " Additional information on the survey methodology may be found at ";

            if (tsar_type == "SA")
            {
                Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "G"]);
               // footRange8.Merge(Type.Missing);
                footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 7, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange8.Font.Name = "Arial";
                footRange8.Font.Size = 8;

                Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "H"], xlWorkSheet.Cells[iRow + 7, "I"]);
                 footRange9.Merge(Type.Missing);
                footRange9.Value = foottitle;
                footRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }
            else
            {
                Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, "H"]);
               // footRange8.Merge(Type.Missing);
                footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 7, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
                footRange8.Font.Name = "Arial";
                footRange8.Font.Size = 8;

                Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "I"], xlWorkSheet.Cells[iRow + 7, "J"]);
                footRange9.Merge(Type.Missing);
                footRange9.Value = foottitle;
                footRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }


            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$5";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 20;
            xlWorkSheet.PageSetup.RightMargin = 40;
            xlWorkSheet.PageSetup.BottomMargin = 0;
            xlWorkSheet.PageSetup.LeftMargin = 80;

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

        //create excels for public
        private void ExportToExcelP(string sfilename, DataTable dt, string stitle, string subtitle, string tsar_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            string smon = sdate.Substring(4, 2);

            string last_col = "A";
            if (tsar_type == "SA")
                last_col = "I";
            else
                last_col = "J";

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
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
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
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
           // titleRange2.Font.Size = 10;
           // titleRange2.Font.Name = "Arial";
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add percent change
            if (tsar_type == "SA")
                xlWorkSheet.Cells[4, 8] = "Percent change\n" + curmons + " from -";
            else
                xlWorkSheet.Cells[4, 8] = "Year-to-date";

            //Span the title across columns H through I
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            titleRange3.Merge(Type.Missing);
            if (tsar_type == "SA")
                titleRange3.RowHeight = 22;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

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

            if (tsar_type == "SA")
            {
                xlWorkSheet.Cells[5, 8] = pmm1;
                xlWorkSheet.Cells[5, 9] = pmm5;
            }
            else
            {
                xlWorkSheet.Cells[5, 8] = cmm;
                xlWorkSheet.Cells[5, 9] = pmm5;
                xlWorkSheet.Cells[5, 10] = "Percent\n Change";
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, last_col]);

            //Set the header row row hight
            headerRange.RowHeight = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            if (tsar_type == "SA")
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            else
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.50;
                    if ((tsar_type == "SA" && (s == "H" || s == "I")) || (tsar_type == "NSA" && s == "J"))
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
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

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                if (iRow == 7 || iRow == 25 || iRow == 43)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;

                    //set up Total Private Construction with subscript 1
                    if (iRow == 7)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Public Construction1";
                        cellRange.RowHeight = 12;
                        cellRange.Characters[26, 1].Font.Superscript = true;
                    }
                    else if (iRow == 25)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total State and Local Construction1";
                        cellRange.RowHeight = 12;
                        cellRange.Characters[35, 1].Font.Superscript = true;
                    }
                    else if (iRow == 43)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "Total Federal Construction2";
                        cellRange.RowHeight = 12;
                        cellRange.Characters[27, 1].Font.Superscript = true;
                        xlWorkSheet.HPageBreaks.Add(cellRange);
                    }
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of construction not shown separately:";
            footRange2.RowHeight = 12;
            footRange2.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, last_col]);
            footRange3.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication and manufacturing.";

            Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, last_col]);
            footRange4.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 7, 1] = "2Includes the following categories of federal construction not shown separately:";
            footRange4.RowHeight = 12;
            footRange4.Characters[1, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, last_col]);
            footRange5.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 8, 1] = "lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, last_col]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 10, 1] = "Source: U.S.Census Bureau, Construction Spending, " + MYD + "." + " Additional information on the survey methodology may be found at ";

           
            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 11, "A"], xlWorkSheet.Cells[iRow + 11, last_col]);
            footRange8.Merge(Type.Missing);
            footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 11, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange8.Font.Name = "Arial";
            footRange8.Font.Size = 8;

            Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 13, "A"], xlWorkSheet.Cells[iRow + 13, last_col]);
            footRange9.Merge(Type.Missing);
            footRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[iRow + 13, 1] = foottitle;
            

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$6";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 50;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 50;
            xlWorkSheet.PageSetup.LeftMargin = 80;

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

        //create excels for state and local
        private void ExportToExcelS(string sfilename, DataTable dt, string stitle, string subtitle, string tsar_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            string smon = sdate.Substring(4, 2);

            string last_col = "A";
            if (tsar_type == "SA")
                last_col = "I";
            else
                last_col = "J";

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
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
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
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add percent change
            if (tsar_type == "SA")
                xlWorkSheet.Cells[4, 8] = "Percent change\n" + curmons + " from -";
            else
                xlWorkSheet.Cells[4, 8] = "Year-to-date";

            //Span the title across columns H through I
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            titleRange3.Merge(Type.Missing);
            if (tsar_type == "SA")
                titleRange3.RowHeight = 22;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

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

            if (tsar_type == "SA")
            {
                xlWorkSheet.Cells[5, 8] = pmm1;
                xlWorkSheet.Cells[5, 9] = pmm5;
            }
            else
            {
                xlWorkSheet.Cells[5, 8] = cmm;
                xlWorkSheet.Cells[5, 9] = pmm5;
                xlWorkSheet.Cells[5, 10] = "Percent\n Change";
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, last_col]);

            //Set the header row fonts bold
            headerRange.RowHeight = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            if (tsar_type == "SA")
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            else
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.50;
                    if ((tsar_type == "SA" && (s == "H" || s == "I")) || (tsar_type == "NSA" && s == "J"))
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
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

                //make row smaller
                if (iRow == 6 || iRow == 8 || iRow == 11 || iRow == 13 || iRow == 15 || iRow == 19 || iRow == 24 || iRow == 37 || iRow == 44 || iRow == 52 || iRow == 62 || iRow == 64 || iRow == 70 || iRow == 78 || iRow == 83)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "I"]);
                    cellRange.RowHeight = 9;
                }

                //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                if (iRow == 7 || iRow == 9 || iRow == 12 || iRow == 14 || iRow == 16 || iRow == 20 || iRow == 25 || iRow == 38 || iRow == 45 || iRow == 53 || iRow == 63 || iRow == 65 || iRow == 71 || iRow == 79 || iRow == 84)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;
                    if (iRow == 53)
                        xlWorkSheet.HPageBreaks.Add(cellRange);
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "Note: Total state and local construction includes the following categories of construction not shown separately:";

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, and manufacturing.";

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, last_col]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 7, 1] = "Source: U.S.Census Bureau, Construction Spending, " + MYD + ". Additional information on the survey methodology may be found at ";

            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, last_col]);
            footRange8.Merge(Type.Missing);
            footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 8, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange8.Font.Name = "Arial";
            footRange8.Font.Size = 8;

            Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, last_col]);
            footRange9.Merge(Type.Missing);
            footRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[iRow + 10, 1] = foottitle;
            
            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;
            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$6";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 20;
            xlWorkSheet.PageSetup.LeftMargin = 80;

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

        //create excels for Federal
        private void ExportToExcelF(string sfilename, DataTable dt, string stitle, string subtitle, string tsar_type, string ssheetname)
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
            string smon = sdate.Substring(4, 2);

            string last_col = "A";
            if (tsar_type == "SA")
                last_col = "I";
            else
                last_col = "J";

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;
            xlWorkSheet.Rows.Font.Size = 8;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 12;

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
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
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);

            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //add an empty row

            //add percent change
            if (tsar_type == "SA")
                xlWorkSheet.Cells[4, 8] = "Percent change\n" + curmons + " from -";
            else
                xlWorkSheet.Cells[4, 8] = "Year-to-date";

            //Span the title across columns H through I
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            titleRange3.Merge(Type.Missing);
            if (tsar_type == "SA")
                titleRange3.RowHeight = 22;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            Microsoft.Office.Interop.Excel.Range tRange = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, last_col]);
            Microsoft.Office.Interop.Excel.Borders border = tRange.Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            string cmm = curmons.Substring(0, 3) + "\n" + curmons.Substring(4);
            string pmm1 = pmon1.Substring(0, 3) + "\n" + pmon1.Substring(4);
            string pmm2 = pmon2.Substring(0, 3) + "\n" + pmon2.Substring(4);
            string pmm3 = pmon3.Substring(0, 3) + "\n" + pmon3.Substring(4);
            string pmm4 = pmon4.Substring(0, 3) + "\n" + pmon4.Substring(4);
            string pmm5 = pmon5.Substring(0, 3) + "\n" + pmon5.Substring(4);

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

            if (tsar_type == "SA")
            {
                xlWorkSheet.Cells[5, 8] = pmm1;
                xlWorkSheet.Cells[5, 9] = pmm5;
            }
            else
            {
                xlWorkSheet.Cells[5, 8] = cmm;
                xlWorkSheet.Cells[5, 9] = pmm5;
                xlWorkSheet.Cells[5, 10] = "Percent\n Change";
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, last_col]);

            //Set the header row fonts bold
            headerRange.RowHeight = 24;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Set the font size, text wrap of columns and format for the entire worksheet
            string[] strColumns;
            if (tsar_type == "SA")
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I" };
            else
                strColumns = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };

            foreach (string s in strColumns)
            {
                if (s != "A")
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 8.50;
                    if ((tsar_type == "SA" && (s == "H" || s == "I")) || (tsar_type == "NSA" && s == "J"))
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "##0.0";
                    else
                        ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).NumberFormat = "#,###";
                }
                else
                {
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[s, Type.Missing]).ColumnWidth = 26.00;
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
                if (iRow == 7)
                {
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, last_col]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;
            footRange1.Characters[15, 1].Font.Superscript = true;

            Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "Note: Total federal construction includes the following categories of construction not shown separately:";

            Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, last_col]);
            footRange2.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";

            Microsoft.Office.Interop.Excel.Range footRange6 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, last_col]);
            footRange6.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 7, 1] = "Source: U.S.Census Bureau, Construction Spending, " + MYD + ". Additional information on the survey methodology may be found at ";

            Microsoft.Office.Interop.Excel.Range footRange8 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, last_col]);
            footRange8.Merge(Type.Missing);
            footRange8.Hyperlinks.Add(xlWorkSheet.Cells[iRow + 8, 1], "http://www.census.gov/construction/c30/meth.html", Type.Missing, "<www.census.gov/construction/c30/meth.html>", "<www.census.gov/construction/c30/meth.html>");
            footRange8.Font.Size = 8;
            footRange8.Font.Name = "Arial";

            Microsoft.Office.Interop.Excel.Range footRange9 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 10, "A"], xlWorkSheet.Cells[iRow + 10, last_col]);
            footRange9.Merge(Type.Missing);
            footRange9.Cells.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            xlWorkSheet.Cells[iRow + 10, 1] = foottitle;
            
            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 40;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 40;
            xlWorkSheet.PageSetup.LeftMargin = 80;

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
            saveFileDialog1.FileName = "Total.xls";

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

        //create excels for total
        private void ExportAnnualToExcel(string survey_type, string sfilename, DataTable dt, string stitle, string subtitle, string ssheetname, string start_year, string end_year )
        {
            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);
           
            //get number of years
            int num_diff = Int32.Parse(end_year) - Int32.Parse(start_year)+1;

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            xlWorkSheet.Name = ssheetname;
            xlWorkSheet.Rows.Font.Size = 9;
            xlWorkSheet.Rows.Font.Name = "Arial";
           
            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, num_diff+1]);
            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 11;
            titleRange.RowHeight = 14;

            //Make the title bold
            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[2, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, num_diff + 1]);
            titleRange2.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            xlWorkSheet.Cells[4, 1] = "Type of Construction:";

            Microsoft.Office.Interop.Excel.Range celRange;
            //add column header
            for (int i = 0; i < num_diff; i++)
            {
                if (Int32.Parse(start_year) + i == (Int32.Parse(end_year) - 1) && sdate.Substring(5,1) == "4")
                {
                    xlWorkSheet.Cells[4, 2 + i] = (Int32.Parse(start_year) + i).ToString() + 'r';
                    celRange = xlApp.get_Range(xlWorkSheet.Cells[4, 2 + i], xlWorkSheet.Cells[4, 2 + i]);
                    celRange.Characters[5, 1].Font.Superscript = true;
                }
                else if (Int32.Parse(start_year) + i == Int32.Parse(end_year))
                {
                    if (sdate.Substring(4, 2) == "12")
                        xlWorkSheet.Cells[4, 2 + i] = (Int32.Parse(start_year) + i).ToString() + 'p';
                    else
                        xlWorkSheet.Cells[4, 2 + i] = (Int32.Parse(start_year) + i).ToString() + 'r';
                    celRange = xlApp.get_Range(xlWorkSheet.Cells[4, 2 + i], xlWorkSheet.Cells[4, 2 + i]);
                    celRange.Characters[5, 1].Font.Superscript = true;
                }
                else
                    xlWorkSheet.Cells[4, 2 + i] = (Int32.Parse(start_year) + i).ToString();               
            }

            //Setup the column header row (row 6)
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, num_diff+1]);
       
            //Set the header row fonts bold
            headerRange.Font.Bold = true; 
            headerRange.RowHeight = 12;
            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 30;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            for (int i = 2; i <= num_diff + 1; i++)
            {
                if (num_diff ==2)
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 30;
                else if (num_diff <= 4)
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 20;
                else
                    ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 8.5;

                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,###";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }

            Microsoft.Office.Interop.Excel.Range colRange = xlApp.get_Range(xlWorkSheet.Cells[4, 1], xlWorkSheet.Cells[4, num_diff + 1]);
            colRange.NumberFormat = "@";

            //Populate rest of the data. Start at row[6] 
            int iRow = 5; //We start at row 6

            int iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                //create extra line
                if (survey_type == "T")
                {
                    if ((int)r["seq"] == 2 || (int)r["seq"] == 3 || (int)r["seq"] == 20 || (int)r["seq"] == 21 || (int)r["seq"] == 22 || (int)r["seq"] == 37 || (int)r["seq"] == 38 || (int)r["seq"] == 39)
                        iRow++;
                }
                else if (survey_type == "V")
                {
                    if ((int)r["seq"] == 2 || (int)r["seq"] == 6 || (int)r["seq"] == 7 || (int)r["seq"] == 8 || (int)r["seq"] == 11 || (int)r["seq"] == 32 || (int)r["seq"] == 36 || (int)r["seq"] == 45 || (int)r["seq"] == 49 || (int)r["seq"] == 50 || (int)r["seq"] == 57||(int)r["seq"] == 61 || (int)r["seq"] == 62 || (int)r["seq"] == 66 || (int)r["seq"] == 67 || (int)r["seq"] == 68)
                        iRow++;
                }
                else if (survey_type == "P")
                {
                    if ((int)r["seq"] == 2 || (int)r["seq"] == 3 || (int)r["seq"] == 16 || (int)r["seq"] == 17 || (int)r["seq"] == 18 || (int)r["seq"] == 31 || (int)r["seq"] == 32 || (int)r["seq"] == 33)
                        iRow++;
                }
                else if (survey_type == "S")
                {
                    if ((int)r["seq"] == 2 || (int)r["seq"] == 4 || (int)r["seq"] == 5 || (int)r["seq"] == 6 || (int)r["seq"] == 10 || (int)r["seq"] == 14 || (int)r["seq"] == 30 || (int)r["seq"] == 36 || (int)r["seq"] == 43 || (int)r["seq"] == 54 || (int)r["seq"] == 57 || (int)r["seq"] == 66 || (int)r["seq"] == 73 || (int)r["seq"] == 80)
                        iRow++;
                }
                else
                {
                    if ((int)r["seq"] == 2 || (int)r["seq"] == 3)
                        iRow++;
                }


                iCol = 0;

                int col09 = 2;
                int col10 = 2;
                //Find 2009 column and 2010 column
                int ydiff = 2009 - Int32.Parse(start_year);
                if (ydiff > 0)
                    col09 = col09 + ydiff;

                ydiff = 2010 - Int32.Parse(start_year);
                if (ydiff > 0)
                    col10 = col10 + ydiff;

                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName != "seq")
                    { 
                        iCol++;
                        if (iCol >= 2)
                        {
                            if (survey_type == "V")
                            {
                                if ((iCol >= col10 && (int)r["seq"] == 73) || (iCol >= col09 && (int)r["seq"] == 83))
                                {
                                    xlWorkSheet.Cells[iRow, iCol] = "X";
                                    xlApp.get_Range(xlWorkSheet.Cells[iRow, iCol], xlWorkSheet.Cells[iRow, iCol]).NumberFormat = "@";
                                }
                                else
                                    xlWorkSheet.Cells[iRow, iCol] = Math.Round(Convert.ToDouble(r[c.ColumnName]), MidpointRounding.AwayFromZero);
                            }
                            else
                                xlWorkSheet.Cells[iRow, iCol] = Math.Round(Convert.ToDouble(r[c.ColumnName]), MidpointRounding.AwayFromZero);
                        }
                        else
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName];
                    }
                }

                if (survey_type == "T")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if ((int)r["seq"] == 1 || (int)r["seq"] == 20 || (int)r["seq"] == 37)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        //set up Total Private Construction with subscript 1
                        if ((int)r["seq"] == 20)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";

                            cellRange.Characters[27, 1].Font.Superscript = true;
                        }
                        else if ((int)r["seq"] == 37)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Public Construction2";

                            cellRange.Characters[26, 1].Font.Superscript = true;

                            //  xlWorkSheet.HPageBreaks.Add(cellRange);

                        }
                    }
                }
                else if (survey_type == "V")
                {
                    //bold rows (Total of Construction, Total Private Construction, Total Public Construction)
                    if ((int)r["seq"] == 1 || (int)r["seq"] == 2 || (int)r["seq"] == 6 || (int)r["seq"] == 7 || (int)r["seq"] == 8 || (int)r["seq"] == 11 || (int)r["seq"] == 32 || (int)r["seq"] == 36 || (int)r["seq"] == 45 || (int)r["seq"] == 49 || (int)r["seq"] == 50 || (int)r["seq"] == 57 || (int)r["seq"] == 61 || (int)r["seq"] == 62 || (int)r["seq"] == 66 || (int)r["seq"] == 67 || (int)r["seq"] == 68)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        //set up Total Private Construction with subscript 1
                        if ((int)r["seq"] == 1)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Private Construction1";
                            cellRange.Characters[27, 1].Font.Superscript = true;
                        }
                        else if ((int)r["seq"] == 61)
                            xlWorkSheet.HPageBreaks.Add(xlApp.get_Range(xlWorkSheet.Cells[iRow + 1, "A"], xlWorkSheet.Cells[iRow + 1, "A"]));

                    }
                    else if ((int)r["seq"] == 5)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "            Improvements2";
                        xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]).Characters[25, 1].Font.Superscript = true;
                    }
                    else if ((int)r["seq"] == 70)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "            Textile/apparel/leather/furniture3";
                        xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]).Characters[46, 1].Font.Superscript = true;
                    }
                    else if ((int)r["seq"] == 72)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "            Paper/Print/Publishing4";
                        xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]).Characters[35, 1].Font.Superscript = true;
                    }
                    else if ((int)r["seq"] == 73)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "            Print/Publishing4";
                        xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]).Characters[29, 1].Font.Superscript = true;
                    }
                    else if ((int)r["seq"] == 83)
                    {
                        xlWorkSheet.Cells[iRow, 1] = "            Furniture3";
                        xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]).Characters[22, 1].Font.Superscript = true;
                    }
                    else if ((int)r["seq"] == 31 )
                        xlWorkSheet.HPageBreaks.Add(xlApp.get_Range(xlWorkSheet.Cells[iRow+1, "A"], xlWorkSheet.Cells[iRow+1, "A"]));

                }
                else if (survey_type == "P")
                {
                    //bold rows (Total of Public Construction, Total State and Local Construction, Total Federal Construction)
                    if ((int)r["seq"] == 1 || (int)r["seq"] == 16 || (int)r["seq"] == 31)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;

                        //set up Total Private Construction with subscript 1
                        if ((int)r["seq"] == 1)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Public Construction1";
                            cellRange.Characters[26, 1].Font.Superscript = true;
                        }
                        else if ((int)r["seq"] == 16)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total State and Local Construction1";
                            cellRange.Characters[35, 1].Font.Superscript = true;
                        }
                        else if ((int)r["seq"] == 31)
                        {
                            xlWorkSheet.Cells[iRow, 1] = "Total Federal Construction2";
                            cellRange.Characters[27, 1].Font.Superscript = true;
                        }

                    }
                    else if ((int)r["seq"] == 30)
                        xlWorkSheet.HPageBreaks.Add(xlApp.get_Range(xlWorkSheet.Cells[iRow + 1, "A"], xlWorkSheet.Cells[iRow + 1, "A"]));
                }
                else if (survey_type == "S")
                {
                    if ((int)r["seq"] == 1 ||(int)r["seq"] == 2 || (int)r["seq"] == 4 || (int)r["seq"] == 5 || (int)r["seq"] == 6 || (int)r["seq"] == 10 || (int)r["seq"] == 14 || (int)r["seq"] == 30 || (int)r["seq"] == 36 || (int)r["seq"] == 43 || (int)r["seq"] == 54 || (int)r["seq"] == 57 || (int)r["seq"] == 66 || (int)r["seq"] == 73 || (int)r["seq"] == 80)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                    else if ((int)r["seq"] == 29 || (int)r["seq"] == 56)
                        xlWorkSheet.HPageBreaks.Add(xlApp.get_Range(xlWorkSheet.Cells[iRow + 1, "A"], xlWorkSheet.Cells[iRow + 1, "A"]));
                }
                else
                {
                    if ((int)r["seq"] == 1)
                    {
                        Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                        cellRange.Font.Bold = true;
                    }
                }
            }

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, num_diff+1]);
            footRange1.Merge(Type.Missing);
            if (sdate.Substring(4,2) == "12")
                xlWorkSheet.Cells[iRow + 2, 1] = "p Preliminary";
            else
                xlWorkSheet.Cells[iRow + 2, 1] = "r Revised";
            footRange1.Characters[1, 1].Font.Superscript = true;

            if (survey_type == "T")
            {
                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of private construction not shown separately:";
                footRange2.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, num_diff + 1]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "highway and street and conservation and development.";

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, num_diff + 1]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 7, 1] = "2Includes the following categories of public construction not shown separately:";
                footRange4.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, num_diff + 1]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 8, 1] = "lodging, religious, communication, and manufacturing.";
            }
            else if (survey_type == "V")
            {
                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "1Total Private construction includes the following categories of construction not shown separately:";
                footRange2.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, num_diff + 1]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "highway and street and conservation and development.";

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 6, "A"], xlWorkSheet.Cells[iRow + 6, num_diff + 1]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 6, 1] = "2Private residential improvements does not include expenditures to rental, vacant, or seasonal properties.";
                footRange4.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, num_diff + 1]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 7, 1] = "3As of 2009, furniture is in textile/apparel/leather/furniture.";
                footRange5.Characters[1, 1].Font.Superscript = true;

                footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, num_diff + 1]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 8, 1] = "4As of 2010, print/publishing is in paper/print/publishing.";
                footRange5.Characters[1, 1].Font.Superscript = true;

                footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 9, "A"], xlWorkSheet.Cells[iRow + 9, num_diff + 1]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 9, 1] = "X Estimates are not applicable/not available.";

            }
            else if (survey_type == "P")
            {
                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "1Includes the following categories of construction not shown separately:";
                footRange2.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange3 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, num_diff + 1]);
                footRange3.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, and manufacturing.";

                Microsoft.Office.Interop.Excel.Range footRange4 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 7, "A"], xlWorkSheet.Cells[iRow + 7, num_diff + 1]);
                footRange4.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 7, 1] = "2Includes the following categories of federal construction not shown separately:";
                footRange4.Characters[1, 1].Font.Superscript = true;

                Microsoft.Office.Interop.Excel.Range footRange5 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 8, "A"], xlWorkSheet.Cells[iRow + 8, num_diff + 1]);
                footRange5.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 8, 1] = "lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";
            }
            else if (survey_type == "S")
            {
                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "Note: Total state and local construction includes the following categories of construction not shown separately:";
                footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, and manufacturing.";
            }
            else 
            {
                Microsoft.Office.Interop.Excel.Range footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 4, 1] = "Note: Total federal construction includes the following categories of construction not shown separately:";
                footRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, num_diff + 1]);
                footRange2.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "lodging, religious, communication, sewage and waste disposal, water supply, and manufacturing.";
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            xlWorkSheet.PageSetup.PrintTitleRows = "$A$1:$J$5";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 30;
            xlWorkSheet.PageSetup.RightMargin = 20;
            xlWorkSheet.PageSetup.BottomMargin = 40;
            xlWorkSheet.PageSetup.LeftMargin = 20;

            xlWorkSheet.PageSetup.RightFooter = foottitle;

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
    }
}
