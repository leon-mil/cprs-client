/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecGeoPub.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 2/5/2018
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display Special Geographic public tabulation

Detailed Design : Detailed Design for geographic public
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
    public partial class frmSpecGeogPub : frmCprsParent
    {
        public frmSpecGeogPub()
        {
            InitializeComponent();
        }

        private SpecGeoData data_object;
        private int cur_year;
        private int sel_type;
        private string subtc = string.Empty;
        private bool show_subtc;
        private DataTable stored_main = null;
        private bool form_loading = false;
        private string saveFilename;
        private frmMessageWait waiting;
        private string sdate;
        private int y4;

        //Declare Excel Interop variables
        Microsoft.Office.Interop.Excel.Application xlApp;
        Microsoft.Office.Interop.Excel.Workbook xlWorkBook;
        Microsoft.Office.Interop.Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;


        private void frmSpecGeogPub_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            form_loading = true;

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();
            cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            SetupYearCombo();

            sel_type = 1;
            subtc = "";
            show_subtc = false;

            rdValue.Checked = true;
            rdRegion.Checked = true;

            data_object = new SpecGeoData();
            stored_main = data_object.GetGeoStateLocalTable(cbYear.Text, sel_type);
            DataTable tblFiltered = stored_main.AsEnumerable()
                                    .Where(row => row.Field<int>("ddown") <= 2)
           // .OrderBy(row => row.Field<String>("newtc"))
            .CopyToDataTable();

            dgData.DataSource = tblFiltered;

            //set up column header
            SetColumnHeader();

            form_loading = false;
        }

        private void LoadData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DataTable tblFiltered = new DataTable();

            if (rdRegion.Checked)
                sel_type = 1;
            else if (rdDivision.Checked)
                sel_type = 2;
            else if (rdState.Checked)
                sel_type = 3;
          
            stored_main = new DataTable();
            if (rdValue.Checked)
                stored_main = data_object.GetGeoStateLocalTable(cbYear.Text, sel_type);
            else if (rdCV.Checked)
                stored_main = data_object.GetGeoStateLocalCVTable(cbYear.Text, sel_type);
            
            dgData.DataSource = null;
            //Get current data
            if (sel_type == 1 || sel_type == 2)
            {
                tblFiltered = stored_main.AsEnumerable()
                .Where(row => row.Field<int>("ddown") <= 2)
               // .OrderBy(row => row.Field<String>("newtc"))
                .CopyToDataTable();

                dgData.DataSource = tblFiltered;
            }
            else if (sel_type == 3)
                dgData.DataSource = stored_main;


            //set up column header
            SetColumnHeader();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            int y1, y2, y3, y5;

            if (Convert.ToInt16(sdate.Substring(4, 2)) > 2)
            {
                y1 = cur_year - 5;
                y2 = cur_year - 4;
                y3 = cur_year - 3;
                y4 = cur_year - 2;
                y5 = cur_year - 1;
            }
            else
            {
                y1 = cur_year - 6;
                y2 = cur_year - 5;
                y3 = cur_year - 4;
                y4 = cur_year - 3;
                y5 = cur_year - 2;
            }

            string num_format = "N0";
            if (rdCV.Checked)
                num_format = "N1";

            if (rdRegion.Checked)
            {
                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 200;
                dgData.Columns[0].Frozen = true;
                dgData.Columns[1].HeaderText = "U.S. Total";
                dgData.Columns[1].DefaultCellStyle.Format = num_format; 
                dgData.Columns[1].DefaultCellStyle.NullValue = "0";
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = "Northeast";
                dgData.Columns[2].DefaultCellStyle.Format = num_format; 
                dgData.Columns[2].DefaultCellStyle.NullValue = "0";
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = "Midwest";
                dgData.Columns[3].DefaultCellStyle.Format = num_format;
                dgData.Columns[3].DefaultCellStyle.NullValue = "0";
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = "South";
                dgData.Columns[4].DefaultCellStyle.Format = num_format;
                dgData.Columns[4].DefaultCellStyle.NullValue = "0";
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = "West";
                dgData.Columns[5].DefaultCellStyle.Format = num_format;
                dgData.Columns[5].DefaultCellStyle.NullValue = "0";
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dgData.Columns[6].Visible = false;
                dgData.Columns[7].Visible = false;
                dgData.Columns[8].Visible = false;

                lbllabel.Text = "Double Click a Line to Expand";
            }

            if (rdDivision.Checked)
            {
                dgData.Columns[0].HeaderText = "Type of Construction";
                dgData.Columns[0].Width = 200;
                dgData.Columns[0].Frozen = true;
                dgData.Columns[1].HeaderText = "All Region";
                dgData.Columns[1].DefaultCellStyle.Format = num_format; 
                dgData.Columns[1].DefaultCellStyle.NullValue = "0";
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = "Total";
                dgData.Columns[2].DefaultCellStyle.Format = num_format;
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = "New England";
                dgData.Columns[3].DefaultCellStyle.Format = num_format;
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = "Middle Atlantic";
                dgData.Columns[4].DefaultCellStyle.Format = num_format;
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = "Total";
                dgData.Columns[5].DefaultCellStyle.Format = num_format;
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].HeaderText = "East North Central";
                dgData.Columns[6].DefaultCellStyle.Format = num_format;
                dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].HeaderText = "West North Central";
                dgData.Columns[7].DefaultCellStyle.Format = num_format;
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[8].HeaderText = "Total";
                dgData.Columns[8].DefaultCellStyle.Format = num_format;
                dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[9].HeaderText = "South Atlantic";
                dgData.Columns[9].DefaultCellStyle.Format = num_format;
                dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[10].HeaderText = "East South Central";
                dgData.Columns[10].DefaultCellStyle.Format = num_format;
                dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[11].HeaderText = "West South Central";
                dgData.Columns[11].DefaultCellStyle.Format = num_format;
                dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[12].HeaderText = "Total";
                dgData.Columns[12].DefaultCellStyle.Format = num_format;
                dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[13].HeaderText = "Mountain";
                dgData.Columns[13].DefaultCellStyle.Format = num_format;
                dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[14].HeaderText = "Pacific";
                dgData.Columns[14].DefaultCellStyle.Format = num_format;
                dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[15].Visible = false;
                dgData.Columns[16].Visible = false;
                dgData.Columns[17].Visible = false;

                lbllabel.Text = "Double Click a Line to Expand";
            }

            else if (rdState.Checked)
            {
                
                dgData.Columns[0].HeaderText = "State";
        
                dgData.Columns[1].HeaderText = y1.ToString();
                dgData.Columns[1].DefaultCellStyle.Format = num_format;
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[2].HeaderText = y2.ToString();
                dgData.Columns[2].DefaultCellStyle.Format = num_format;
                dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[3].HeaderText = y3.ToString();
                dgData.Columns[3].DefaultCellStyle.Format = num_format;
                dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[4].HeaderText = y4.ToString();
                dgData.Columns[4].DefaultCellStyle.Format = num_format;
                dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[5].HeaderText = y5.ToString();
                dgData.Columns[5].DefaultCellStyle.Format = num_format;
                dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[6].Visible = false;
                if (rdValue.Checked)
                    dgData.Columns[7].Visible = false;

            }

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
            {
                LoadData();
            }
        }

        private void SetupTitle()
        {
            if (rdValue.Checked)
            {
                lblTitle.Text = "ANNUAL VALUE OF STATE AND LOCAL CONSTRUCTION";

                if (rdRegion.Checked)
                    label1.Text = "By Region";
                else if (rdDivision.Checked)
                    label1.Text = "By Division";
                else
                    label1.Text = "By State";
                label2.Text = "Millions of Dollars";
            }
            else
            {
                label1.Text = "(Percent)";
                label2.Text = "";
                if (rdRegion.Checked)
                    lblTitle.Text = "COEFFICIENTS OF VARIATION FOR SELECTED TYPES OF CONSTRUCTION";

                else if (rdDivision.Checked)
                    lblTitle.Text = "COEFFICIENTS OF VARIATION FOR SELECTED TYPES OF CONSTRUCTION";
                else
                    lblTitle.Text = "COEFFICIENTS OF VARIATION";
            }

        }

        private void rdRegion_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdRegion.Checked)
            {
                SetupTitle();
                if (rdValue.Checked)
                    cbYear.Enabled = true;
                lbllabel.Visible = true;
                LoadData();
            }
        }

        private void rdDivision_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdDivision.Checked)
            {
                SetupTitle();
                if (rdValue.Checked)
                    cbYear.Enabled = true;
                lbllabel.Visible = true;
                LoadData();
            }
        }

        private void rdState_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdState.Checked)
            {
                SetupTitle();
                cbYear.Enabled = false;
                lbllabel.Visible = false;
                LoadData();
            }
        }

        private void rdValue_CheckedChanged(object sender, EventArgs e)
        {
            if (!form_loading && rdValue.Checked)
            {
                if (rdRegion.Checked || rdDivision.Checked)
                    cbYear.Enabled = true;
                else
                    cbYear.Enabled = false;
                SetupTitle();
                LoadData();
                btnTable.Enabled = true;
            }
        }

        private void SetupYearCombo()
        {
            cbYear.Items.Clear();

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

            cbYear.SelectedIndex = 0;

        }

        private void rdCV_CheckedChanged(object sender, EventArgs e)
        {
            SetupTitle();
            cbYear.Enabled = false;

            LoadData();
            btnTable.Enabled = false;
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xls";
            saveFileDialog1.Title = "Save an File";

            if (rdValue.Checked)
            {
                if (rdRegion.Checked)
                    saveFileDialog1.FileName = "SLRegion.xls";
                if (rdDivision.Checked)
                    saveFileDialog1.FileName = "SLDivision.xls";
                if (rdState.Checked)
                    saveFileDialog1.FileName = "SLState.xls";
            }
            
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

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            if (cbYear.Enabled)
                printer.Title = lblTitle.Text + " " + cbYear.Text;
            else
                printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = label1.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Geographic State Local Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            if (sel_type == 1 || sel_type == 3)
            {
                for (int i = 0; i <= 5; i++)
                {
                    dgData.Columns[i].Width = 160;
                }

            }
            else if (sel_type == 2)
            {
                dgData.Columns[0].Width = 120;
                for (int i = 1; i < 15; i++)
                {
                    dgData.Columns[i].Width = 60;
                }

            }
           
            printer.PrintDataGridViewWithoutDialog(dgData);
            if (sel_type == 1 || sel_type == 2)
            {
                dgData.Columns[0].Width = 200;
            }
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

            Cursor.Current = Cursors.Default;
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

            if (rdValue.Checked)
            {
                if (rdRegion.Checked)
                    sfilename = dir + "\\SLRegion.xls";
                if (rdDivision.Checked)
                    sfilename = dir + "\\SLDivision.xls";
                if (rdState.Checked)
                    sfilename = dir + "\\SLState.xls";
            }

            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ////create sheet tables
            ////create region sheets
            if (rdRegion.Checked)
            {
                if (Convert.ToInt16(sdate.Substring(4, 2)) > 2)
                {
                    ExportToExcelValue1((cur_year - 5).ToString());
                    ExportToExcelValue1((cur_year - 4).ToString());
                    ExportToExcelValue1((cur_year - 3).ToString());
                    ExportToExcelValue1((cur_year - 2).ToString());
                    ExportToExcelValue1((cur_year - 1).ToString());

                    ExportToExcelValueCV((cur_year - 1).ToString());
                }
                else
                {
                    ExportToExcelValue1((cur_year - 6).ToString());
                    ExportToExcelValue1((cur_year - 5).ToString());
                    ExportToExcelValue1((cur_year - 4).ToString());
                    ExportToExcelValue1((cur_year - 3).ToString());
                    ExportToExcelValue1((cur_year - 2).ToString());

                    ExportToExcelValueCV((cur_year - 2).ToString());
                }

            }
            else if (rdDivision.Checked)
            {
                //create division sheet
                ExportToExcelValue2();
            }
            else
            {
                //create sheet sheet
               ExportToExcelValue3();
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

        private void ExportToExcelValue1(string year)
        {
            string stitle = string.Empty;
            stitle = "Annual Value of State and Local Construction Put in Place By Region,\n for Selected Types of Construction";

            string subtitle = "(Millions of Dollars. Details may not add to toatals since all types of construction are not shown separately.)";
            string tabname = string.Empty;

            tabname = year;
          
            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = tabname;

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
           
            xlWorkSheet.Activate();

            ///*for datatable */
            DataTable dt = data_object.GetGeoStateLocalTable(year, 1);

      
            int reg120 = 0, reg121 = 0, reg122 = 0, reg123 = 0, reg124 = 0;
            int reg130 = 0, reg131 = 0, reg132 = 0, reg133 = 0, reg134 = 0;
            foreach (DataRow row in dt.Rows)
            {
                if (row["newtc"].ToString().Trim() == "1211" || row["newtc"].ToString().Trim() == "1212")
                {
                    reg120 = reg120 + (int)row["reg0"];
                    reg121 = reg121 + (int)row["reg1"];
                    reg122 = reg122 + (int)row["reg2"];
                    reg123 = reg123 + (int)row["reg3"];
                    reg124 = reg124 + (int)row["reg4"];
                }

                if ((row["newtc"].ToString().Trim() == "1314") || (row["newtc"].ToString().Trim() == "1315") || (row["newtc"].ToString().Trim() == "1316")||(row["newtc"].ToString().Trim() == "1317"))
                {
                    reg130 = reg130 + (int)row["reg0"];
                    reg131 = reg131 + (int)row["reg1"];
                    reg132 = reg132 +(int)row["reg2"];
                    reg133 = reg133 + (int)row["reg3"];
                    reg134 = reg134+(int)row["reg4"];
                } 
            }

            DataTable dtt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (row["newtc"].ToString().Trim() == "")
                {
                    row["newtc_str"] = "Total State and Local Construction*";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "00")
                {
                    row["newtc_str"] = "    Residential";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "002")
                {
                    row["newtc_str"] = "        Multi-family";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "NR")
                {
                    row["newtc_str"] = "    Nonresidential";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "02")
                {
                    row["newtc_str"] = "        Office";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "03")
                {
                    row["newtc_str"] = "        Commercial";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "031")
                {
                    row["newtc_str"] = "            Automotive";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0313")
                {
                    row["newtc_str"] = "                Parking";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "04")
                {
                    row["newtc_str"] = "        Health Care";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "041")
                {
                    row["newtc_str"] = "            Hospital";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "042")
                {
                    row["newtc_str"] = "            Medical building";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "043")
                {
                    row["newtc_str"] = "            Special Care";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "05")
                {
                    row["newtc_str"] = "        Educational";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "052")
                {
                    row["newtc_str"] = "            Primary/secondary";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0521")
                {
                    row["newtc_str"] = "                Elementary";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0522")
                {
                    row["newtc_str"] = "                Middle/Junior high";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0523")
                {
                    row["newtc_str"] = "                High";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "053")
                {
                    row["newtc_str"] = "            Higher education";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0531")
                {
                    row["newtc_str"] = "                Instructional";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0535")
                {
                    row["newtc_str"] = "                Dormitory";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0538")
                {
                    row["newtc_str"] = "                Sports/recreation";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0539")
                {
                    row["newtc_str"] = "                Infrastructure";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "055")
                {
                    row["newtc_str"] = "            Other educational";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0553")
                {
                    row["newtc_str"] = "                Library/archive";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "07")
                {
                    row["newtc_str"] = "        Public Safety";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "071")
                {
                    row["newtc_str"] = "            Correctional";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0711")
                {
                    row["newtc_str"] = "                Detention";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0712")
                {
                    row["newtc_str"] = "                Police/sheriff";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "072")
                {
                    row["newtc_str"] = "            Other public safety";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0721")
                {
                    row["newtc_str"] = "                Fire/rescue";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "08")
                {
                    row["newtc_str"] = "        Amusement and Recreation";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "082")
                {
                    row["newtc_str"] = "            Sports";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "084")
                {
                    row["newtc_str"] = "            Performance/meeting center";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0844")
                {
                    row["newtc_str"] = "                Convention center";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "085")
                {
                    row["newtc_str"] = "            Social center";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0852")
                {
                    row["newtc_str"] = "                Neighborhood center";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "086")
                {
                    row["newtc_str"] = "            Park/camp";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "09")
                {
                    row["newtc_str"] = "        Transportation";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "091")
                {
                    row["newtc_str"] = "            Air";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0911")
                {
                    row["newtc_str"] = "                Passenger terminal";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0912")
                {
                    row["newtc_str"] = "                Runway";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "092")
                {
                    row["newtc_str"] = "            Land";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0921")
                {
                    row["newtc_str"] = "                Passenger terminal";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0923")
                {
                    row["newtc_str"] = "                Mass transit";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "093")
                {
                    row["newtc_str"] = "            Water";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "0931")
                {
                    row["newtc_str"] = "                Dock/marina";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "11")
                {
                    row["newtc_str"] = "        Power";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "12")
                {
                    row["newtc_str"] = "        Highway and Street";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1211")
                {
                    row["newtc_str"] = "                Pavement";
                    row["newtc"] = "12CC";
                    row["reg0"] = reg120;
                    row["reg1"] = reg121;
                    row["reg2"] = reg122;
                    row["reg3"] = reg123;
                    row["reg4"] = reg124;
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "1213")
                {
                    row["newtc_str"] = "                Lighting";
                    dtt.ImportRow(row);
                } 
                else if (row["newtc"].ToString().Trim() == "1216")
                {
                    row["newtc_str"] = "                Bridge";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1219")
                {
                    row["newtc_str"] = "                Rest facility";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "13")
                {
                    row["newtc_str"] = "        Sewage and Waste Disposal";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "131")
                {
                    row["newtc_str"] = "            Sewage/dry waste";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1312")
                {
                    row["newtc_str"] = "                Plant";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1314")
                {
                    row["newtc_str"] = "                Line/pump station";
                    row["newtc"] = "13CC";
                    row["reg0"] = reg130;
                    row["reg1"] = reg131;
                    row["reg2"] = reg132;
                    row["reg3"] = reg133;
                    row["reg4"] = reg134;
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "132")
                {
                    row["newtc_str"] = "            Waste water";
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "1321")
                {
                    row["newtc_str"] = "                Plant";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1322")
                {
                    row["newtc_str"] = "                Line/drain";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "14")
                {
                    row["newtc_str"] = "        Water Supply";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1411")
                {
                    row["newtc_str"] = "                Plant";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1413")
                {
                    row["newtc_str"] = "                Line";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1414")
                {
                    row["newtc_str"] = "                Pump station";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "15")
                {
                    row["newtc_str"] = "        Conservation and Development";
                    dtt.ImportRow(row);
                }
                
                else if (row["newtc"].ToString().Trim() == "1521")
                { 
                    row["newtc_str"] = "                Dam/levee";
                    dtt.ImportRow(row);
                }   
                else if (row["newtc"].ToString().Trim() == "1522")
                {
                    row["newtc_str"] = "                Breakwater/jetty";
                    dtt.ImportRow(row);
                }   
            }

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange;

            titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]);


            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 30;

            ////Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            ////Give the title background color
            //titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            ////Add a title2
            xlWorkSheet.Cells[3, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 6]);

            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 6]);
            cellRange.Font.Bold = true;
            if (Convert.ToInt16(year) != y4)
                xlWorkSheet.Cells[5, 1] = "Type of Construction: " + year;
            else
            {
                xlWorkSheet.Cells[5, 1] = "Type of Construction: " + year + "r";
                cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "A"]);
                cellRange.Characters[27, 1].Font.Superscript = true;
            }
           
            xlWorkSheet.Cells[5, 2] = "U.S. Total";
            xlWorkSheet.Cells[5, 3] = "Northeast";
            xlWorkSheet.Cells[5, 4] = "Midwest";
            xlWorkSheet.Cells[5, 5] = "South";
            xlWorkSheet.Cells[5, 6] = "West";

            Microsoft.Office.Interop.Excel.Range tRange;
            Microsoft.Office.Interop.Excel.Borders border;
            for (int i = 2; i < 6; i++)
            {
                tRange = xlApp.get_Range(xlWorkSheet.Cells[5, i], xlWorkSheet.Cells[5, i]);
                border = tRange.Borders;
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            border = xlApp.get_Range(xlWorkSheet.Cells[5, 6], xlWorkSheet.Cells[5, 6]).Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            ////Setup the column header row (row 5)

            ////Set the font size, text wrap of columns and format for the entire worksheet
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 34;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            for (int i = 2; i <= 6; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 11;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }


            ////Populate rest of the data. Start at row[6] 
            int iRow = 5; //We start at row 5

            int iCol = 0;
            int row2 = 0;
            foreach (DataRow r in dtt.Rows)
            {
                row2++;
                iCol = 0;

                if ((int)r["ddown"] > 2)
                    iRow++;
                else
                {
                    iRow = iRow + 2;
                    //bold rows
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;
                }

                foreach (DataColumn c in dtt.Columns)
                {
                    iCol++;

                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= 6)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }

            //set line
            xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[iRow, 2]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, 3], xlWorkSheet.Cells[iRow, 3]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, 4], xlWorkSheet.Cells[iRow, 4]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, 5], xlWorkSheet.Cells[iRow, 5]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, 6]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "*State and Local Nonresidential Construction includes the following categories not shown separately:";
            
            footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, 6]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 3, 1] = "Lodging, Religious, Communication, and Manufacturing";

            footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 4, "A"], xlWorkSheet.Cells[iRow + 4, 6]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 4, 1] = "(S) Suppressed because estimate does not meet publication standards, but is included in total.";

            if (Convert.ToInt16(year) == y4)
            {
                footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 5, "A"], xlWorkSheet.Cells[iRow + 5, 6]);
                footRange1.Merge(Type.Missing);
                xlWorkSheet.Cells[iRow + 5, 1] = "rRevised";
                footRange1.Characters[1, 1].Font.Superscript = true;
            }
        }

        private void ExportToExcelValueCV(string year)
        {
            string stitle = string.Empty;
            stitle = "Coefficient of Variations By Region For Selected Types of Construction";

            string subtitle = "(Percent)";
            string tabname = string.Empty;

            tabname = year + "CV's";

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = tabname;

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
           // xlWorkSheet.Rows.RowHeight = 10;

            xlWorkSheet.Activate();

            ///*for datatable */
            DataTable dt = data_object.GetGeoStateLocalCVTable(year, 1);

            DataTable dtt = dt.Clone();
            foreach (DataRow row in dt.Rows)
            {
                if (row["newtc"].ToString().Trim() == "")
                {
                    row["newtc_str"] = "Total State and Local Construction*";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "00")
                {
                    row["newtc_str"] = "    Residential";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "002")
                {
                    row["newtc_str"] = "        Multi-family";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "NR")
                {
                    row["newtc_str"] = "    Nonresidential";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "02")
                {
                    row["newtc_str"] = "        Office";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "03")
                {
                    row["newtc_str"] = "        Commercial";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "031")
                {
                    row["newtc_str"] = "            Automotive";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0313")
                {
                    row["newtc_str"] = "                Parking";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "04")
                {
                    row["newtc_str"] = "        Health Care";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "041")
                {
                    row["newtc_str"] = "            Hospital";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "042")
                {
                    row["newtc_str"] = "            Medical building";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "043")
                {
                    row["newtc_str"] = "            Special Care";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "05")
                {
                    row["newtc_str"] = "        Educational";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "052")
                {
                    row["newtc_str"] = "            Primary/secondary";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0521")
                {
                    row["newtc_str"] = "                Elementary";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0522")
                {
                    row["newtc_str"] = "                Middle/Junior high";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0523")
                {
                    row["newtc_str"] = "                High";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "053")
                {
                    row["newtc_str"] = "            Higher education";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0531")
                {
                    row["newtc_str"] = "                Instructional";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0535")
                {
                    row["newtc_str"] = "                Dormitory";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0538")
                {
                    row["newtc_str"] = "                Sports/recreation";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0539")
                {
                    row["newtc_str"] = "                Infrastructure";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "055")
                {
                    row["newtc_str"] = "            Other educational";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0553")
                {
                    row["newtc_str"] = "                Library/archive";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "07")
                {
                    row["newtc_str"] = "        Public Safety";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "071")
                {
                    row["newtc_str"] = "            Correctional";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0711")
                {
                    row["newtc_str"] = "                Detention";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0712")
                {
                    row["newtc_str"] = "                Police/sheriff";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "072")
                {
                    row["newtc_str"] = "            Other public safety";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0721")
                {
                    row["newtc_str"] = "                Fire/rescue";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "08")
                {
                    row["newtc_str"] = "        Amusement and Recreation";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "082")
                {
                    row["newtc_str"] = "            Sports";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "084")
                {
                    row["newtc_str"] = "            Performance/meeting center";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0844")
                {
                    row["newtc_str"] = "                Convention center";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "085")
                {
                    row["newtc_str"] = "            Social center";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0852")
                {
                    row["newtc_str"] = "                Neighborhood center";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "086")
                {
                    row["newtc_str"] = "            Park/camp";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "09")
                {
                    row["newtc_str"] = "        Transportation";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "091")
                {
                    row["newtc_str"] = "            Air";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0911")
                {
                    row["newtc_str"] = "                Passenger terminal";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0912")
                {
                    row["newtc_str"] = "                Runway";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "092")
                {
                    row["newtc_str"] = "            Land";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0921")
                {
                    row["newtc_str"] = "                Passenger terminal";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0923")
                {
                    row["newtc_str"] = "                Mass transit";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "093")
                {
                    row["newtc_str"] = "            Water";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "0931")
                {
                    row["newtc_str"] = "                Dock/marina";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "11")
                {
                    row["newtc_str"] = "        Power";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "12")
                {
                    row["newtc_str"] = "        Highway and Street";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1211")
                {
                    row["newtc_str"] = "                Pavement";
                    row["newtc"] = "12CC";
                    
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "1213")
                {
                    row["newtc_str"] = "                Lighting";
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "1216")
                {
                    row["newtc_str"] = "                Bridge";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1219")
                {
                    row["newtc_str"] = "                Rest facility";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "13")
                {
                    row["newtc_str"] = "        Sewage and Waste Disposal";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "131")
                {
                    row["newtc_str"] = "            Sewage/dry waste";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1312")
                {
                    row["newtc_str"] = "                Plant";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1314")
                {
                    row["newtc_str"] = "                Line/pump station";
                    row["newtc"] = "13CC";
                    
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "132")
                {
                    row["newtc_str"] = "            Waste water";
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "1321")
                {
                    row["newtc_str"] = "                Plant";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1322")
                {
                    row["newtc_str"] = "                Line/drain";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "14")
                {
                    row["newtc_str"] = "        Water Supply";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1411")
                {
                    row["newtc_str"] = "                Plant";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1413")
                {
                    row["newtc_str"] = "                Line";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1414")
                {
                    row["newtc_str"] = "                Pump station";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "15")
                {
                    row["newtc_str"] = "        Conservation and Development";
                    dtt.ImportRow(row);
                }

                else if (row["newtc"].ToString().Trim() == "1521")
                {
                    row["newtc_str"] = "                Dam/levee";
                    dtt.ImportRow(row);
                }
                else if (row["newtc"].ToString().Trim() == "1522")
                {
                    row["newtc_str"] = "                Breakwater/jetty";
                    dtt.ImportRow(row);
                }
            }

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange;

            titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 6]);


            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 30;

            ////Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            ////Give the title background color
            //titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            ////Add a title2
            xlWorkSheet.Cells[3, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 6]);

            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 6]);
            cellRange.Font.Bold = true;

            xlWorkSheet.Cells[5, 1] = "Type of Construction: " + year;
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, "A"]);
            cellRange.Characters[27, 1].Font.Superscript = true;

            xlWorkSheet.Cells[5, 2] = "U.S. Total";
            xlWorkSheet.Cells[5, 3] = "Northeast";
            xlWorkSheet.Cells[5, 4] = "Midwest";
            xlWorkSheet.Cells[5, 5] = "South";
            xlWorkSheet.Cells[5, 6] = "West";

            Microsoft.Office.Interop.Excel.Range tRange;
            Microsoft.Office.Interop.Excel.Borders border;
            for (int i = 2; i < 6; i++)
            {
                tRange = xlApp.get_Range(xlWorkSheet.Cells[5, i], xlWorkSheet.Cells[5, i]);
                border = tRange.Borders;
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                    Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            border = xlApp.get_Range(xlWorkSheet.Cells[5, 6], xlWorkSheet.Cells[5, 6]).Borders;
            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;
            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle =
                Microsoft.Office.Interop.Excel.XlLineStyle.xlLineStyleNone;

            ////Setup the column header row (row 5)

            ////Set the font size, text wrap of columns and format for the entire worksheet
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 35;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            for (int i = 2; i <= 6; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 11;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "##0.0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }


            ////Populate rest of the data. Start at row[6] 
            int iRow = 5; //We start at row 5

            int iCol = 0;
            int row2 = 0;
            foreach (DataRow r in dtt.Rows)
            {
                row2++;
                iCol = 0;

                if ((int)r["ddown"] > 2)
                    iRow++;
                else
                {
                    iRow = iRow + 2;
                    //bold rows
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;
                }

                foreach (DataColumn c in dtt.Columns)
                {
                    iCol++;

                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= 6)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }

            //set line
            xlApp.get_Range(xlWorkSheet.Cells[5, 2], xlWorkSheet.Cells[iRow, 2]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, 3], xlWorkSheet.Cells[iRow, 3]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, 4], xlWorkSheet.Cells[iRow, 4]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, 5], xlWorkSheet.Cells[iRow, 5]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, 6]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "*State and Local Nonresidential Construction includes the following categories not shown separately:";

            footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, 6]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 3, 1] = "Lodging, Religious, Communication, and Manufacturing";

           
        }

        //create division sheet
        private void ExportToExcelValue2()
        {
            int y1 = 0; int y5 = 0;
            if (Convert.ToInt16(sdate.Substring(4, 2)) > 2)
            {
                y1 = cur_year - 5;
                y5 = cur_year - 1;
            }
            else
            {
                y1 = cur_year - 6;
                y5 = cur_year - 2;
            }

            string stitle = string.Empty;
            stitle = "Annual Value of State and Local Construction Put in Place By Geographic Division,\n for Selected Type of Construction: " + y1.ToString() + " to " + y5.ToString();

            string subtitle = "(Millions of Dollars. Details may not add to totals since all types of construction are not shown separately.)";
            string tabname = string.Empty;

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "Division ";

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            //xlWorkSheet.Rows.RowHeight = 10;

            xlWorkSheet.Activate();

            ///*for datatable */
            DataTable dtt = data_object.GetGeoStateLocalTable(y5.ToString(), 4);
 
            DataTable dt = dtt.Clone();
            foreach (DataRow row in dtt.Rows)
            {
                if (row["newtc"].ToString().Trim() != "")
                {
                    if (row["newtc"].ToString().Trim() == "00")
                        row["newtc_str"] = "Total Residential";
                    if (row["newtc"].ToString().Trim() == "NR")
                        row["newtc_str"] = "Total Nonresidential*";
                    if (row["newtc"].ToString().Trim() == "05")
                        row["newtc_str"] = "Educational";
                    if (row["newtc"].ToString().Trim() == "08")
                        row["newtc_str"] = "Amusement and Recreation";
                    if (row["newtc"].ToString().Trim() == "12")
                        row["newtc_str"] = "Highway and Street";
                    if (row["newtc"].ToString().Trim() == "13")
                        row["newtc_str"] = "Sewer and Waste Disposal";
                    if (row["newtc"].ToString().Trim() == "15")
                        row["newtc_str"] = "Conservation and Development";

                    dt.ImportRow(row);
                }
            }

            DataTable dtcv = data_object.GetGeoStateLocalCVTable(y5.ToString(), 2);
            DataRow[] dtcc = dtcv.Select("ddown < 3");
            DataTable dtccvv = dtcc.CopyToDataTable();

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange;

            titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 15]);

            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 30;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[3, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2;

            titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 15]);

            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[7, 15]);
            cellRange.Font.Bold = true;

            //column header text
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 4], xlWorkSheet.Cells[5, 15]);
            xlWorkSheet.Cells[5, 4] = "Northeast";
            xlWorkSheet.Cells[5, 7] = "Midwest";
            xlWorkSheet.Cells[5, 10] = "South";
            xlWorkSheet.Cells[5, 14] = "West";

            cellRange = xlApp.get_Range(xlWorkSheet.Cells[7, 1], xlWorkSheet.Cells[7, 15]);
            cellRange.RowHeight = 26;
            xlWorkSheet.Cells[7, 1] = " ";
            xlWorkSheet.Cells[7, 2] = "All\n Regions";
            xlWorkSheet.Cells[7, 3] = "Total";
            xlWorkSheet.Cells[7, 4] = "New\n England";
            xlWorkSheet.Cells[7, 5] = "Middle\n Atlantic";
            xlWorkSheet.Cells[7, 6] = "Total";
            xlWorkSheet.Cells[7, 7] = "East North\n Central";
            xlWorkSheet.Cells[7, 8] = "West North\n Central";
            xlWorkSheet.Cells[7, 9] = "Total";
            xlWorkSheet.Cells[7, 10] = "South\n Atlantic";
            xlWorkSheet.Cells[7, 11] = "East South\n Central";
            xlWorkSheet.Cells[7, 12] = "West South\n Central";
            xlWorkSheet.Cells[7, 13] = "Total";
            xlWorkSheet.Cells[7, 14] = "Mountain";
            xlWorkSheet.Cells[7, 15] = "Pacific";

            //Setup the column header row (row 7)

            ////Set the font size, text wrap of columns and format for the entire worksheet
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 28;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            for (int i = 2; i <= 15; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 12;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }

            int iRow = 7; //We start at row 7

            int iCol = 0;
            int group_row = 0;
            foreach (DataRow r in dt.Rows)
            {
                //print tc group, bold text
                if (group_row == 0)
                {
                    iRow = iRow + 2;
                    xlWorkSheet.Cells[iRow, 1] = r["newtc_str"];
                    //bold rows
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                    cellRange.Font.Bold = true;
                }

                //print data for the tc group
                iRow++;

                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName != "newtc_str" && c.ColumnName != "newtc")
                    {
                        iCol++;
                        if (iCol == 1 && group_row == 3)
                        {
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString() + "r";
                            cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, "A"], xlWorkSheet.Cells[iRow, "A"]);
                            cellRange.Characters[5, 1].Font.Superscript = true;
                        }
                        else
                            xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                    }
                }

                group_row++;
                if (group_row == 5)
                {
                    iRow = iRow + 2;
                    xlWorkSheet.Cells[iRow, 1] = "Relative standard error...(percent)";
                    cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, 2], xlWorkSheet.Cells[iRow, 15]);
                    cellRange.NumberFormat = "@";

                    DataRow[] dc = dtccvv.Select("newtc = '" + r["newtc"].ToString() + "'");

                    xlWorkSheet.Cells[iRow, 2] = dc[0]["c0"].ToString();
                    xlWorkSheet.Cells[iRow, 3] = dc[0]["c1"].ToString();
                    xlWorkSheet.Cells[iRow, 4] = dc[0]["c2"].ToString();
                    xlWorkSheet.Cells[iRow, 5] = dc[0]["c3"].ToString();
                    xlWorkSheet.Cells[iRow, 6] = dc[0]["c4"].ToString();
                    xlWorkSheet.Cells[iRow, 7] = dc[0]["c5"].ToString();
                    xlWorkSheet.Cells[iRow, 8] = dc[0]["c6"].ToString();
                    xlWorkSheet.Cells[iRow, 9] = dc[0]["c7"].ToString();
                    xlWorkSheet.Cells[iRow, 10] = dc[0]["c8"].ToString();
                    xlWorkSheet.Cells[iRow, 11] = dc[0]["c9"].ToString();
                    xlWorkSheet.Cells[iRow, 12] = dc[0]["c10"].ToString();
                    xlWorkSheet.Cells[iRow, 13] = dc[0]["c11"].ToString();
                    xlWorkSheet.Cells[iRow, 14] = dc[0]["c12"].ToString();
                    xlWorkSheet.Cells[iRow, 15] = dc[0]["c13"].ToString();

                    group_row = 0;
                }

            }

            //set line
            xlApp.get_Range(xlWorkSheet.Cells[5, "B"], xlWorkSheet.Cells[88, "B"]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, "E"], xlWorkSheet.Cells[88, "E"]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, "H"], xlWorkSheet.Cells[88, "H"]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            xlApp.get_Range(xlWorkSheet.Cells[5, "L"], xlWorkSheet.Cells[88, "L"]).Borders[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;

            //add text after grid

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, "P"]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "*State and Local Nonresidential Construction includes the following categories not shown separately:  Lodging, Commerical, Religious, Communication, Power, and Manufacturing";
            footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, 15]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 3, 1] = "rRevised";
            footRange1.Characters[1, 1].Font.Superscript = true;
        }

        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (sel_type == 3) return;
            if (!show_subtc)
                if (dgData.CurrentRow.Index == 0) return;
            
            if (dgData.CurrentRow.Cells["ddown"].Value.ToString() == "1") return;

            if (!show_subtc)
            {
                //go to subtc 
                show_subtc = true;

                subtc = dgData.CurrentRow.Cells[0].Value.ToString().Substring(0, 2);

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
                DataTable tblFiltered = stored_main.AsEnumerable()
                                   .Where(row => row.Field<int>("ddown") <= 2)
               //.OrderBy(row => row.Field<String>("newtc"))
               .CopyToDataTable();
                dgData.DataSource = tblFiltered;
                lbllabel.Text = "Double Click a Line to Expand";
            }

            //set up column header
            SetColumnHeader();
        }

        //create state sheet
        private void ExportToExcelValue3()
        {
            int y1 = 0; int y5 = 0;
            if (Convert.ToInt16(sdate.Substring(4, 2)) > 2)
            {
                y1 = cur_year - 5;
                y5 = cur_year - 1;
            }
            else
            {
                y1 = cur_year - 6;
                y5 = cur_year - 2;
            }

            string stitle = string.Empty;
            stitle = "Annual Value of State and Local Construction Put in Place By State, " + y1.ToString() + " - " + y5.ToString();

            string subtitle = "(Millions of Dollars. Details may not add to totals since all types of construction are not shown separately.)";
            string tabname = string.Empty;

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();
            xlWorkSheet.Name = "State ";

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            //xlWorkSheet.Rows.RowHeight = 10;

            xlWorkSheet.Activate();

            ///*for datatable */
            DataTable dt = data_object.GetGeoStateLocalTable(y5.ToString(), 3);
            DataTable dtcv = data_object.GetGeoStateLocalCVTable(y5.ToString(), 3);

            //Add a title
            xlWorkSheet.Cells[1, 1] = stitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange;

            titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, 1], xlWorkSheet.Cells[1, 7]);

            titleRange.Merge(Type.Missing);

            //Increase the font-size of the title
            titleRange.Font.Size = 12;
            titleRange.RowHeight = 13;

            //Make the title bold
            titleRange.Font.Bold = true;
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);

            //Add a title2
            xlWorkSheet.Cells[3, 1] = subtitle;

            //Span the title across columns A through I
            Microsoft.Office.Interop.Excel.Range titleRange2;

            titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[3, 1], xlWorkSheet.Cells[3, 7]);

            titleRange2.Merge(Type.Missing);
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Font.Size = 10;

            //Populate headers, assume row[0] contains the titles and row[5] contains all the headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, 7]);
            cellRange.Font.Bold = true;
            cellRange.NumberFormat = "@";

            //column header text
            xlWorkSheet.Cells[5, 2] = y1.ToString();
            xlWorkSheet.Cells[5, 3] = (y1 + 1).ToString();
            xlWorkSheet.Cells[5, 4] = (y1 + 2).ToString();
            xlWorkSheet.Cells[5, 5] = (y1 + 3) + "r";
            cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 5], xlWorkSheet.Cells[5, 5]);
            cellRange.Characters[5, 1].Font.Superscript = true;
            xlWorkSheet.Cells[5, 6] = (y1 + 4).ToString();
           
            xlWorkSheet.Cells[5, 7] = "RSE(%)";


            //Setup the column header row (row 7)

            ////Set the font size, text wrap of columns and format for the entire worksheet
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 28;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;

            for (int i = 2; i <= 6; i++)
            {
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 10;
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            }
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[7, Type.Missing]).ColumnWidth = 10;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[7, Type.Missing]).NumberFormat = "#,##0.0";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[7, Type.Missing]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            int iRow = 6; //We start at row 7

            int iCol = 0;

            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                if (r["fipstate"].ToString().Trim().Length ==1)
                    iRow++;
                
                iCol = 0;
                foreach (DataColumn c in dt.Columns)
                {
                    if (c.ColumnName != "seq" && c.ColumnName != "fipstate")
                    {
                        iCol++;
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                    }
                }

                DataRow[] dc = dtcv.Select("state_desc = '" + r["fipname"].ToString() + "'");
                xlWorkSheet.Cells[iRow, 7] = dc[0]["yy5"].ToString();
            }

            Microsoft.Office.Interop.Excel.Range footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 2, "A"], xlWorkSheet.Cells[iRow + 2, 7]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 2, 1] = "RSE - Relative Statndard Error";

            footRange1 = xlApp.get_Range(xlWorkSheet.Cells[iRow + 3, "A"], xlWorkSheet.Cells[iRow + 3, 7]);
            footRange1.Merge(Type.Missing);
            xlWorkSheet.Cells[iRow + 3, 1] = "rRevised";
            footRange1.Characters[1, 1].Font.Superscript = true;
        }

        private void frmSpecGeogPub_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

    }
}
