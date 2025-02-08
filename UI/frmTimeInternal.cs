/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       Cprs.TimeInternalData.cs	    	
Programmer:         Srini Natarajan
Creation Date:      07/7/2017
Inputs:             None
Parameters:	        
Outputs:	        Internal Time series 	
Description:	    This form displays the data Internal Time series data.
Detailed Design:    CPRS II - Internal Time SeriesDesign
Other:	            Called by: Main Menu
Revision History:	
****************************************************************************************
 Modified Date : 2/21/2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR885
 Description   : update excel file name from .xls to .xlsx
***************************************************************************************
Modified Date : 6 / 4 / 2024
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR1411
 Description   : add data center
 *************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Globalization;

namespace Cprs
{
    public partial class frmTimeInternal : frmCprsParent
    {
        public frmTimeInternal()
        {
            InitializeComponent();
        }

        private string sdate;
        private string saveFilename;
        private frmMessageWait waiting;

        //Declare Excel Interop variables
        Excel.Application xlApp;
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        object misValue = System.Reflection.Missing.Value;

        private void frmTimeInternal_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();
            GetData();
        }

        private void GetData()
        {

            if (rdtot.Checked == true)
            {
                GetTotalData();
                SetUpGridTot();
            }
            if (rdpriv.Checked == true)
            {
                GetPrivateData();
                SetupGridPriv();
            }
            if (rdstat.Checked == true)
            {
                GetPublicData();
                SetupGridPublic();
            }
            if (rdfed.Checked == true)
            {
                GetFederalData();
                SetUpGridFed();
            }

            //make table unsortable, except date column
            foreach (DataGridViewColumn column in dgData.Columns)
            {
                if (column.Index != 0)
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        //private string sdate;
        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex == 0)
            {
                string col0 = e.Value.ToString();
                DateTime col = DateTime.ParseExact(col0, "yyyyMM", null);
                e.Value = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                
            }
            else
            {
                e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            if (rdtot.Checked == true)
            {

                if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 8 || e.ColumnIndex == 10
                    || e.ColumnIndex == 12 || e.ColumnIndex == 14)
                {
                    e.CellStyle.Format = "#0.0\\%";
                }
                else
                {
                    e.CellStyle.Format = "#,#";
                }
            }

            if (rdpriv.Checked == true || rdstat.Checked == true)
            {

                if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 8 || e.ColumnIndex == 10
                || e.ColumnIndex == 12 || e.ColumnIndex == 14 || e.ColumnIndex == 16 || e.ColumnIndex == 18 || e.ColumnIndex == 20
                || e.ColumnIndex == 22 || e.ColumnIndex == 24 || e.ColumnIndex == 26 || e.ColumnIndex == 28 || e.ColumnIndex == 30
                || e.ColumnIndex == 32 || e.ColumnIndex == 34 || e.ColumnIndex == 36 || e.ColumnIndex == 38 || e.ColumnIndex == 40
                || e.ColumnIndex == 42 || e.ColumnIndex == 44 || e.ColumnIndex == 46 || e.ColumnIndex == 48 || e.ColumnIndex == 50
                || e.ColumnIndex == 52 || e.ColumnIndex == 54 || e.ColumnIndex == 56 || e.ColumnIndex == 58 || e.ColumnIndex == 60
                || e.ColumnIndex == 62 || e.ColumnIndex == 64 || e.ColumnIndex == 66 || e.ColumnIndex == 68 || e.ColumnIndex == 70
                || e.ColumnIndex == 72 || e.ColumnIndex == 74 || e.ColumnIndex == 76 || e.ColumnIndex == 78 || e.ColumnIndex == 80
                || e.ColumnIndex == 82 || e.ColumnIndex == 84 || e.ColumnIndex == 86 || e.ColumnIndex == 88 || e.ColumnIndex == 90
                || e.ColumnIndex == 92 || e.ColumnIndex == 94 || e.ColumnIndex == 96 || e.ColumnIndex == 98 || e.ColumnIndex == 100
                || e.ColumnIndex == 102 || e.ColumnIndex == 104 || e.ColumnIndex == 106 || e.ColumnIndex == 108 || e.ColumnIndex == 110
                || e.ColumnIndex == 112 || e.ColumnIndex == 114 || e.ColumnIndex == 116 || e.ColumnIndex == 118 || e.ColumnIndex == 120
                || e.ColumnIndex == 122 || e.ColumnIndex == 124 || e.ColumnIndex == 126 || e.ColumnIndex == 128 || e.ColumnIndex == 130 || e.ColumnIndex == 132)
                {
                    e.CellStyle.Format = "#0.0\\%";
                }
                else
                {
                    e.CellStyle.Format = "#,#";
                }
            }

            if (rdfed.Checked == true)
            {

                if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 8 || e.ColumnIndex == 10
                    || e.ColumnIndex == 12 || e.ColumnIndex == 14 || e.ColumnIndex == 16 || e.ColumnIndex == 18 || e.ColumnIndex == 20
                    || e.ColumnIndex == 22 || e.ColumnIndex == 24 || e.ColumnIndex == 26)
                {
                    e.CellStyle.Format = "#0.0\\%";
                }
                else
                {
                    e.CellStyle.Format = "#,#";
                }
            }
        }

        private void GetTotalData()
        {
            lblTitle.Text = "Magnificent 7 (From January 1993) Highs and Lows";
            dgData.Columns.Clear();

            TimeInternalData dataObject = new TimeInternalData();
            DataTable dt = dataObject.GetTotalData(sdate);
            dgData.DataSource = dt;

            string ColumnsTitle;

            //Loop through the columns to find the max and Min values

            for (int j = 1; j < dgData.ColumnCount; ++j)
            {
                ColumnsTitle = dt.Columns[j].ColumnName;
                decimal minLavel = Convert.ToDecimal(dt.Compute("min([" + ColumnsTitle + "])", string.Empty));
                decimal maxLavel = Convert.ToDecimal(dt.Compute("max([" + ColumnsTitle + "])", string.Empty));

                int rowIndex;
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    //Setting the High value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == maxLavel.ToString())
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Red;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }

                    //Setting the High value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == minLavel.ToString() )
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Blue;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }
                }
            }

            dgData.Columns[0].Frozen = true;
        }

        private void GetPrivateData()
        {
            lblTitle.Text = "Total Private Spending (From January 1993) Highs and Lows";
            dgData.Columns.Clear();
            TimeInternalData dataObject = new TimeInternalData();

            DataTable dt = dataObject.GetPrivateData(sdate);
            dgData.DataSource = dt;

            string ColumnsTitle;

            //Loop through the columns to find the max and Min values

            for (int j = 1; j < dgData.ColumnCount; ++j)
            {
                ColumnsTitle = dt.Columns[j].ColumnName;
                decimal minLavel = Convert.ToDecimal(dt.Compute("min([" + ColumnsTitle + "])", string.Empty));
                decimal maxLavel = Convert.ToDecimal(dt.Compute("max([" + ColumnsTitle + "])", string.Empty));

                int rowIndex;
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    //Setting the Highest value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == maxLavel.ToString())
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Red;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }

                    //Setting the min value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == minLavel.ToString() && row.Cells[j].Value.ToString() != "0.0")
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Blue;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }
                }
            }

            dgData.Columns[0].Frozen = true;
        }

        private void GetPublicData()
        {
            lblTitle.Text = "Total State and Local Spending (From January 1993) Highs and Lows";
            
            dgData.Columns.Clear();
            TimeInternalData dataObject = new TimeInternalData();

            DataTable dt = dataObject.GetPublicData(sdate);
            dgData.DataSource = dt;
            string ColumnsTitle;
            //Loop through the columns to find the max and Min values

            for (int j = 1; j < dgData.ColumnCount; ++j)
            {
                ColumnsTitle = dt.Columns[j].ColumnName;
                decimal minLavel = Convert.ToDecimal(dt.Compute("min([" + ColumnsTitle + "])", string.Empty));
                decimal maxLavel = Convert.ToDecimal(dt.Compute("max([" + ColumnsTitle + "])", string.Empty));

                int rowIndex;
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    //Setting the High value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == maxLavel.ToString())
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Red;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }

                    //Setting the High value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == minLavel.ToString())
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Blue;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }
                }
            }

            dgData.Columns[0].Frozen = true;
        }

        private void GetFederalData()
        {
            lblTitle.Text = "Total Federal Spending (From January 2002) Highs and Lows";
            
            dgData.Columns.Clear();
            TimeInternalData dataObject = new TimeInternalData();

            DataTable dt = dataObject.GetFederalData(sdate);
            dgData.DataSource = dt;
            //
            string ColumnsTitle;

            //Loop through the columns to find the max and Min values

            for (int j = 1; j < dgData.ColumnCount; ++j)
            {
                ColumnsTitle = dt.Columns[j].ColumnName;
                decimal minLavel = Convert.ToDecimal(dt.Compute("min([" + ColumnsTitle + "])", string.Empty));
                decimal maxLavel = Convert.ToDecimal(dt.Compute("max([" + ColumnsTitle + "])", string.Empty));

                int rowIndex;
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    //Setting the High value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == maxLavel.ToString())
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Red;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }

                    //Setting the High value to red and low value to blue

                    if (row.Cells[j].Value.ToString() == minLavel.ToString())
                    {
                        rowIndex = row.Index;
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Blue;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }
                }
            }
            dgData.Columns[0].Frozen = true;
        }


        private void SetUpGridTot()
        {
            dgData.ReadOnly = true;
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DataTable dt = (DataTable)dgData.DataSource;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dgData.Columns[i].HeaderText = dt.Columns[i].Caption;
            }

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
        }

        private void SetupGridPriv()
        {
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            DataTable dt = (DataTable)dgData.DataSource;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dgData.Columns[i].HeaderText = dt.Columns[i].Caption;
            }

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        private void SetupGridPublic()
        {
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            DataTable dt = (DataTable)dgData.DataSource;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dgData.Columns[i].HeaderText = dt.Columns[i].Caption;
            }

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }

        private void SetUpGridFed()
        {
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            DataTable dt = (DataTable)dgData.DataSource;
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                dgData.Columns[i].HeaderText = dt.Columns[i].Caption;
            }

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

        }


        private void btnTable_Click(object sender, EventArgs e)
        {
            string filename = "total_time_series_" + sdate + ".dat";

            // Displays a SaveFileDialog save file

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text file|*.txt";
            saveFileDialog1.FileName = filename;
            saveFileDialog1.Title = "Save an File";
            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //Write to cvs file
            TimeInternalData dataObject = new TimeInternalData();

            //total
            DataTable dt = dataObject.GetTotalData(sdate);

            FileInfo fileInfo = new FileInfo(saveFileDialog1.FileName);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            string sfilename = dir + "\\" + filename;
            DataTableToCSV("T", dt, sfilename);

            //private
            dt = dataObject.GetPrivateData(sdate);

            filename = "priv_time_series_" + sdate + ".dat";
            fileInfo = new FileInfo(saveFileDialog1.FileName);
            dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            sfilename = dir + "\\" + filename;
            DataTableToCSV("V", dt, sfilename);

            //Public
            dt = dataObject.GetPublicData(sdate);

            filename = "state_time_series_" + sdate + ".dat";
            fileInfo = new FileInfo(saveFileDialog1.FileName);
            dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            sfilename = dir + "\\" + filename;
            DataTableToCSV("S", dt, sfilename);

            //Federal
            dt = dataObject.GetFederalData(sdate);

            filename = "federal_time_series_" + sdate + ".dat";
            fileInfo = new FileInfo(saveFileDialog1.FileName);
            dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);
            sfilename = dir + "\\" + filename;
            DataTableToCSV("F", dt, sfilename);

            MessageBox.Show("Files have been created.");

        }

        private void DataTableToCSV(string table_type, DataTable table, string Filename)
        {
            //delete exist file
            try
            {
                File.Delete(Filename);
            }
            catch
            {
                MessageBox.Show("Cannot delete file");
            }

            //SetupCaptionforTotal(table);
            var result = new StringBuilder();
            for (int i = 0; i < table.Columns.Count; i++)
            {
                result.Append(table.Columns[i].Caption);
                result.Append(i == table.Columns.Count - 1 ? "\r\n" : ",");
            }

            foreach (DataRow row in table.Rows)
            {
                if (table_type == "T")
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            DateTime col = DateTime.ParseExact(row[i].ToString(), "yyyyMM", null);
                            string new_value = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                            result.Append(new_value);
                        }
                        else if (i == 2 || i == 4 || i == 6 || i == 8 || i == 10 || i == 12 || i == 14)
                        {
                            string value = row[i].ToString();
                            if (value != string.Empty)
                            {
                                float vv = float.Parse(row[i].ToString());
                                value = vv.ToString("F1", CultureInfo.InvariantCulture) + '%';
                            }
                            result.Append(value);
                        }
                        else
                        {
                            int value = Convert.ToInt32(row[i]);
                            result.Append(value.ToString());
                        }

                        result.Append(i == table.Columns.Count - 1 ? "\r\n" : ",");
                    }
                }
                else if (table_type == "V" || table_type == "S")
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            DateTime col = DateTime.ParseExact(row[i].ToString(), "yyyyMM", null);
                            string new_value = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                            result.Append(new_value);
                        }
                        else if (i == 2 || i == 4 || i == 6 || i == 8 || i == 10
                    || i == 12 || i == 14 || i == 16 || i == 18 || i == 20
                    || i == 22 || i == 24 || i == 26 || i == 28 || i == 20
                    || i == 32 || i == 34 || i == 36 || i == 38 || i == 40
                    || i == 42 || i == 44 || i == 46 || i == 48 || i == 50
                    || i == 52 || i == 54 || i == 56 || i == 58 || i == 60
                    || i == 62 || i == 64 || i == 66 || i == 68 || i == 70
                    || i == 72 || i == 74 || i == 76 || i == 78 || i == 80
                    || i == 82 || i == 84 || i == 86 || i == 88 || i == 90
                    || i == 92 || i == 94 || i == 96 || i == 98 || i == 100
                    || i == 102 || i == 104 || i == 106 || i == 108 || i == 110
                    || i == 112 || i == 114 || i == 116 || i == 118 || i == 120
                    || i == 122 || i == 124 || i == 126 || i == 128 || i == 130 || i == 132 || i == 134)
                        {
                            string value = row[i].ToString();
                            if (value != string.Empty)
                            {
                                float vv = float.Parse(row[i].ToString());
                                value = vv.ToString("F1", CultureInfo.InvariantCulture) + '%';
                            }
                            result.Append(value);
                        }
                        else
                        {
                            if (row[i] != DBNull.Value)
                            {
                                int value = Convert.ToInt32(row[i]);
                                result.Append(value.ToString());
                            }
                            else
                                result.Append(" ");
                        }

                        result.Append(i == table.Columns.Count - 1 ? "\r\n" : ",");
                    }
                }

                else if (table_type == "F")
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i == 0)
                        {
                            DateTime col = DateTime.ParseExact(row[i].ToString(), "yyyyMM", null);
                            string new_value = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);
                            result.Append(new_value);
                        }
                        else if (i == 2 || i == 4 || i == 6 || i == 8 || i == 10 || i == 12 || i == 14 || i == 16 || i == 18 || i == 20 || i == 22 || i == 24 || i == 26)
                        {
                            string value = row[i].ToString();
                            if (value != string.Empty)
                            {
                                float vv = float.Parse(row[i].ToString());
                                value = vv.ToString("F1", CultureInfo.InvariantCulture) + '%';
                            }
                            result.Append(value);
                        }
                        else
                        {
                            if (row[i] != DBNull.Value)
                            {
                                int value = Convert.ToInt32(row[i]);
                                result.Append(value.ToString());
                            }
                            else
                                result.Append(" ");
                        }

                        result.Append(i == table.Columns.Count - 1 ? "\r\n" : ",");
                    }
                }
            }

            File.WriteAllText(Filename, result.ToString());
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

            //Hide the columns not needed
            if (rdpriv.Checked == true)
            {
                printer.HideColumns.Add("SingleFam");
                printer.HideColumns.Add("PerNewSingFam");
                printer.HideColumns.Add("MultiFam");
                printer.HideColumns.Add("PerNewMultFam");
                printer.HideColumns.Add("General");
                printer.HideColumns.Add("PerGeneral");
                printer.HideColumns.Add("Financial");
                printer.HideColumns.Add("PerFinancial");

                printer.HideColumns.Add("Auto");
                printer.HideColumns.Add("PerAutomotive");
                printer.HideColumns.Add("Sale");
                printer.HideColumns.Add("PerSale");
                printer.HideColumns.Add("ServiceParts");
                printer.HideColumns.Add("PerServiceParts");
                printer.HideColumns.Add("Parking");
                printer.HideColumns.Add("PerParking");

                printer.HideColumns.Add("FoodBev");
                printer.HideColumns.Add("PerFoodBev");
                printer.HideColumns.Add("Food");
                printer.HideColumns.Add("PerFood");
                printer.HideColumns.Add("DrinkingDining");
                printer.HideColumns.Add("PerDrinkingDining");
                printer.HideColumns.Add("MultiRetail");
                printer.HideColumns.Add("PerMultiRetail");

                printer.HideColumns.Add("GenMerchan");
                printer.HideColumns.Add("PerGenMerchandise");
                printer.HideColumns.Add("ShoppnCenter");
                printer.HideColumns.Add("PerShoppnCenter");
                printer.HideColumns.Add("ShoppnMall");
                printer.HideColumns.Add("PerShoppnMall");
                printer.HideColumns.Add("OthrComm");
                printer.HideColumns.Add("PerOthrCommercial");

                printer.HideColumns.Add("DrugStore");
                printer.HideColumns.Add("PerDrugStore");
                printer.HideColumns.Add("BldgSuppStore");
                printer.HideColumns.Add("PerBldgSuppStore");
                printer.HideColumns.Add("OthrStores");
                printer.HideColumns.Add("PerOthrStores");
                printer.HideColumns.Add("Warehouses");
                printer.HideColumns.Add("PerWarehouses");

                printer.HideColumns.Add("GenComm");
                printer.HideColumns.Add("PerGenComm");
                printer.HideColumns.Add("MiniStorage");
                printer.HideColumns.Add("PerMiniStorage");
                printer.HideColumns.Add("Hospital");
                printer.HideColumns.Add("PerHospital");
                printer.HideColumns.Add("MedBuilding");
                printer.HideColumns.Add("PerMedBuilding");
                printer.HideColumns.Add("SplCare");
                printer.HideColumns.Add("PerSplCare");

                printer.HideColumns.Add("Preschool");
                printer.HideColumns.Add("PerPreschool");
                printer.HideColumns.Add("PrimSecon");
                printer.HideColumns.Add("PerPrimSecon");
                printer.HideColumns.Add("HigherEdu");
                printer.HideColumns.Add("PerHigherEdu");
                printer.HideColumns.Add("Instructional");
                printer.HideColumns.Add("PerInstructional");
                printer.HideColumns.Add("Dormitory");
                printer.HideColumns.Add("PerDormitary");

                printer.HideColumns.Add("SportsRec");
                printer.HideColumns.Add("PerSportsRec");
                printer.HideColumns.Add("OtherEdu");
                printer.HideColumns.Add("PerOtherEdu");
                printer.HideColumns.Add("GalleryMuseum");
                printer.HideColumns.Add("PerGalleryMuseum");

                printer.HideColumns.Add("HouseofWorship");
                printer.HideColumns.Add("PerHouseofWorship");
                printer.HideColumns.Add("OthrReligious");
                printer.HideColumns.Add("PerOthrReligious");
                printer.HideColumns.Add("AuxBldg");
                printer.HideColumns.Add("PerAuxBldg");

                printer.HideColumns.Add("ThemeAmusePark");
                printer.HideColumns.Add("PerThemeAmusePark");
                printer.HideColumns.Add("Sports");
                printer.HideColumns.Add("PerSports");
                printer.HideColumns.Add("Fitness");
                printer.HideColumns.Add("PerFitness");
                printer.HideColumns.Add("PerfMtgCenter");
                printer.HideColumns.Add("PerPerfMtgCenter");
                printer.HideColumns.Add("SocialCntr");
                printer.HideColumns.Add("PerSocialCntr");
                printer.HideColumns.Add("MovieThtrStudio");
                printer.HideColumns.Add("PerMovieThtrStudio");

                printer.HideColumns.Add("Air");
                printer.HideColumns.Add("PerAir");
                printer.HideColumns.Add("Land");
                printer.HideColumns.Add("PerLand");

                printer.HideColumns.Add("Electric");
                printer.HideColumns.Add("PerElectric");

            }

            if (rdstat.Checked == true)
            {
                printer.HideColumns.Add("MultiFamily");
                printer.HideColumns.Add("PerMultiFam");
                printer.HideColumns.Add("Automotive");
                printer.HideColumns.Add("PerAutomotive");
                printer.HideColumns.Add("Parking");
                printer.HideColumns.Add("PerParking");
                printer.HideColumns.Add("Hospital");
                printer.HideColumns.Add("PerHospital");

                printer.HideColumns.Add("MedicBldg");
                printer.HideColumns.Add("PerMedicalBldg");
                printer.HideColumns.Add("SplCare");
                printer.HideColumns.Add("PerSplCare");
                printer.HideColumns.Add("PrimSecndry");
                printer.HideColumns.Add("PerPrimSecondary");
                printer.HideColumns.Add("Elmntry");
                printer.HideColumns.Add("PerElementary");

                printer.HideColumns.Add("MddlJrHigh");
                printer.HideColumns.Add("PerMiddleJrHigh");
                printer.HideColumns.Add("High");
                printer.HideColumns.Add("PerHigh");
                printer.HideColumns.Add("HighrEdu");
                printer.HideColumns.Add("PerHighrEdu");
                printer.HideColumns.Add("Instructnl");
                printer.HideColumns.Add("PerInstructional");

                printer.HideColumns.Add("Dormitory");
                printer.HideColumns.Add("PerDormitory");
                printer.HideColumns.Add("SportsRecr");
                printer.HideColumns.Add("PerSportsRecr");
                printer.HideColumns.Add("Infrastructure");
                printer.HideColumns.Add("PerInfrastructure");
                printer.HideColumns.Add("OthrEductnl");
                printer.HideColumns.Add("PerOthrEducational");

                printer.HideColumns.Add("LibraryArch");
                printer.HideColumns.Add("PerLibraryArch");
                printer.HideColumns.Add("Correctnl");
                printer.HideColumns.Add("PerCorrectional");
                printer.HideColumns.Add("Detention");
                printer.HideColumns.Add("PerDetention");
                printer.HideColumns.Add("PoliceSheriff");
                printer.HideColumns.Add("PerPoliceSheriff");

                printer.HideColumns.Add("OthrPublicSafety");
                printer.HideColumns.Add("PerOthrPubSafety");
                printer.HideColumns.Add("FirenRescue");
                printer.HideColumns.Add("PerFirenRescue");
                printer.HideColumns.Add("Sports");
                printer.HideColumns.Add("PerSports");
                printer.HideColumns.Add("PerfMgmtCenter");
                printer.HideColumns.Add("PerPerfMtgCenter");
                printer.HideColumns.Add("ConvCenter");
                printer.HideColumns.Add("PerConvCenter");

                printer.HideColumns.Add("SocialCenter");
                printer.HideColumns.Add("PerSocialCenter");
                printer.HideColumns.Add("NeighbrHoodCntr");
                printer.HideColumns.Add("PerNeighbrhoodCenter");
                printer.HideColumns.Add("ParkCamp");
                printer.HideColumns.Add("PerParkCamp");
                printer.HideColumns.Add("Transprtatn");
                printer.HideColumns.Add("PerTransportation");
                printer.HideColumns.Add("Air");
                printer.HideColumns.Add("PerAir");

                printer.HideColumns.Add("AirPssngrTermnl");
                printer.HideColumns.Add("PerAirPassengerTerminal");
                printer.HideColumns.Add("Runway");
                printer.HideColumns.Add("PerRunway");
                printer.HideColumns.Add("Land");
                printer.HideColumns.Add("PerLand");

                printer.HideColumns.Add("LandPssngrTerminal");
                printer.HideColumns.Add("PerLandPassngrTermnl");
                printer.HideColumns.Add("MassTransit");
                printer.HideColumns.Add("PerMassTransit");
                printer.HideColumns.Add("Water");
                printer.HideColumns.Add("PerWater");

                printer.HideColumns.Add("DockMarina");
                printer.HideColumns.Add("PerDockMarina");
                printer.HideColumns.Add("Pavemnt");
                printer.HideColumns.Add("PerPavement");
                printer.HideColumns.Add("Lighting");
                printer.HideColumns.Add("PerLigting");
                printer.HideColumns.Add("Bridge");
                printer.HideColumns.Add("PerBridge");
                printer.HideColumns.Add("RestFacility");
                printer.HideColumns.Add("PerRestFacility");
                printer.HideColumns.Add("SewageDryWaste");
                printer.HideColumns.Add("PerSewageDryWaste");

                printer.HideColumns.Add("SewageTrtmntPlnt");
                printer.HideColumns.Add("PerSewageTrtmntPlant");
                printer.HideColumns.Add("LinePumpStation");
                printer.HideColumns.Add("PerLinePumpStation");
                printer.HideColumns.Add("WasteWater");
                printer.HideColumns.Add("PerWasteWater");

                printer.HideColumns.Add("WasteWtrTrtPlnt");
                printer.HideColumns.Add("PerWasteWaterTrtmntPlant");
                printer.HideColumns.Add("LineDrain");
                printer.HideColumns.Add("PerLineDrain");
                printer.HideColumns.Add("WatrTrtmntPlnt");
                printer.HideColumns.Add("PerWaterTrtmntPlant");

                printer.HideColumns.Add("Line");
                printer.HideColumns.Add("PerLine");
                printer.HideColumns.Add("PumpStation");
                printer.HideColumns.Add("PerPumpStation");
                printer.HideColumns.Add("DamLevee");
                printer.HideColumns.Add("PerDamLevee");
                printer.HideColumns.Add("BrkWtrJetty");
                printer.HideColumns.Add("PerBreakwaterJetty");

            }

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Internal Time Series";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[0].Width = 80;
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;

            //release printer
            GeneralFunctions.releaseObject(printer);
            Cursor.Current = Cursors.Default;
        }

        //To retain the high and low value highlights in Red and blue when the sort order is changed on the Date (first)column
        private void dgData_Sorted(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dgData.DataSource;

            for (int j = 1; j < dgData.ColumnCount; ++j)
            {
                string columnsTitle = dt.Columns[j].ColumnName;
                decimal minLavel = Convert.ToDecimal(dt.Compute("min([" + columnsTitle + "])", string.Empty));
                decimal maxLavel = Convert.ToDecimal(dt.Compute("max([" + columnsTitle + "])", string.Empty));

                int rowIndex;
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    rowIndex = row.Index;
                    if (row.Cells[j].Value.ToString() == maxLavel.ToString())
                    {
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Red;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }

                    if (row.Cells[j].Value.ToString() == minLavel.ToString())
                    {
                        dgData.Rows[rowIndex].Cells[j].Style.BackColor = Color.Blue;
                        dgData.Rows[rowIndex].Cells[j].Style.ForeColor = Color.White;
                    }
                }
            }
        }

        private void rdpriv_CheckedChanged(object sender, EventArgs e)
        {
            if (rdpriv.Checked)
            {
                Cursor.Current = Cursors.WaitCursor;
                GetData();
                Cursor.Current = Cursors.Default;
            }
        }

        private void rdtot_CheckedChanged(object sender, EventArgs e)
        {
            if (rdtot.Checked)
            {
                Cursor.Current = Cursors.WaitCursor;
                GetData();
                Cursor.Current = Cursors.Default;
            }
        }

        private void rdstat_CheckedChanged(object sender, EventArgs e)
        {
            if (rdstat.Checked)
            {
                Cursor.Current = Cursors.WaitCursor;
                GetData();
                Cursor.Current = Cursors.Default;
            }
        }

        private void rdfed_CheckedChanged(object sender, EventArgs e)
        {
            if (rdfed.Checked)
            {
                Cursor.Current = Cursors.WaitCursor;
                GetData();
                Cursor.Current = Cursors.Default;
            }
        }

        private void frmTimeInternal_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save a File";

            saveFileDialog1.FileName = "totsats.xlsx";

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

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Enabled = true;
            MessageBox.Show("Files have been created");
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

            //Creates Total Excel File
            TimeInternalData dataObject = new TimeInternalData();
            DataTable dt = dataObject.GetTotalData(sdate);
            foreach (DataRow dr in dt.Rows) // search whole table
            {
                DateTime col = DateTime.ParseExact(dr["Date6"].ToString(), "yyyyMM", null);
                dr["Date6"] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].Caption == "%Change")
                    {
                        if (dr[i].ToString()!= "")
                            dr[i] = Convert.ToDecimal(dr[i]) / 100;
                    }
                }
            }
            stitle = "Magnificent 7 ( from January 1993)";
            stitle1 = "Highs and Lows";

            sfilename = dir + "\\totsats.xlsx";
            ssheetname = "Total Mag 7";
            ExportToExcel(sfilename, dt, stitle, stitle1, ssheetname);

            //Create Private File
            dt = dataObject.GetPrivateData(sdate);
            foreach (DataRow dr in dt.Rows) // search whole table
            {
                DateTime col = DateTime.ParseExact(dr["Date6"].ToString(), "yyyyMM", null);
                dr["Date6"] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].Caption == "%Change")
                    {
                        if (dr[i].ToString() != "")
                            dr[i] = Convert.ToDecimal(dr[i]) / 100;
                    }
                }

            }

            stitle = "Total Private Spending (From January 1993) ";
            stitle1 = "Highs and Lows";

            sfilename = dir + "\\privsats.xlsx";
            ssheetname = "Total Private";
            ExportToExcel(sfilename, dt, stitle, stitle1, ssheetname);

            //Create Public File
            dt = dataObject.GetPublicData(sdate);
            foreach (DataRow dr in dt.Rows) // search whole table
            {
                DateTime col = DateTime.ParseExact(dr["Date6"].ToString(), "yyyyMM", null);
                dr["Date6"] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].Caption == "%Change")
                    {
                        if (dr[i].ToString() != "")
                            dr[i] = Convert.ToDecimal(dr[i]) / 100;
                    }
                }

            }

            stitle = "Total State and Local Spending (From January 1993)";
            stitle1 = "Highs and Lows";

            sfilename = dir + "\\slsats.xlsx";
            ssheetname = "Total State & Local";
            ExportToExcel(sfilename, dt, stitle, stitle1, ssheetname);


            //Create Federal File
            dt = dataObject.GetFederalData(sdate);
            foreach (DataRow dr in dt.Rows) // search whole table
            {
                DateTime col = DateTime.ParseExact(dr["Date6"].ToString(), "yyyyMM", null);
                dr["Date6"] = col.ToString("MMM-yy", System.Globalization.CultureInfo.InvariantCulture);

                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    if (dt.Columns[i].Caption == "%Change")
                    {
                        if (dr[i].ToString() != "")
                            dr[i] = Convert.ToDecimal(dr[i]) / 100;
                    }
                }

            }

            stitle = "Total Federal Spending (From January 2002)";
            stitle1 = "Highs and Lows";

            sfilename = dir + "\\fedsats.xlsx";
            ssheetname = "Total Federal";
            ExportToExcel(sfilename, dt, stitle, stitle1, ssheetname);

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void ExportToExcel(string sfilename, DataTable dt, string stitle, string stitle1,  string ssheetname)
        {
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

            int last_col = dt.Columns.Count;

            //Span the title across columns A through last col
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[2, 1], xlWorkSheet.Cells[2, last_col]);
            titleRange.Merge(Type.Missing);

            titleRange.Font.Bold = true;

            //Give the title background color
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Add a title2
            xlWorkSheet.Cells[3, 1] = stitle1;

            //Span the title across columns 
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, last_col]);
            titleRange2.Merge(Type.Missing);

            titleRange2.Font.Bold = true;
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            //Span the title across columns 
            Microsoft.Office.Interop.Excel.Range titleRange4 = xlApp.get_Range(xlWorkSheet.Cells[4, "A"], xlWorkSheet.Cells[4, last_col]);
            titleRange4.Merge(Type.Missing);

            //Setup the column header row 
            Microsoft.Office.Interop.Excel.Range headerRange = xlApp.get_Range(xlWorkSheet.Cells[5, "A"], xlWorkSheet.Cells[5, last_col]);
            int j = 0;
            foreach (DataColumn c in dt.Columns)
            {
                j++;
                xlWorkSheet.Cells[5, j] = c.Caption;
            }

            //Set the header row fonts bold
            headerRange.RowHeight = 22;

            //Alignment the header row horizontally
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Populate headers, assume rows[0,1,2] contain the title and row[5] contains all the headers
            headerRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;


            //Set the font size, text wrap of columns and format for the entire worksheet
            for (int i = 1; i < dt.Columns.Count+1; i++)
            {
        
                ////For populating only a single row with 'n' no. of columns.
                Microsoft.Office.Interop.Excel.Range writeRange = xlApp.get_Range(xlWorkSheet.Cells[4, i], xlWorkSheet.Cells[dt.Rows.Count + 6, i]);
               
                //format date column as text so "MMM-yy" format displays correctly
                if (i == 1)
                {
                    writeRange.EntireColumn.NumberFormat = "@";
                }
                //format columns to display numbers
                else if (i % 2 == 0)
                    writeRange.NumberFormat = "#,###";
                else
                    writeRange.NumberFormat = "0.0%";
            }

            //Populate rest of the data
            int iRow = 5;
            int jColumn = 0;

            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                jColumn = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    jColumn++;
                    
                     xlWorkSheet.Cells[iRow, jColumn] = r[c.ColumnName];  
                }
            }

            //highlight lowest and hightest value
            for (int k = 1; k < dt.Columns.Count; ++k)
            {
                string columnsTitle = dt.Columns[k].ColumnName;
                decimal minLavel = Convert.ToDecimal(dt.Compute("min([" + columnsTitle + "])", string.Empty));
                decimal maxLavel = Convert.ToDecimal(dt.Compute("max([" + columnsTitle + "])", string.Empty));

                //Populate rest of the data
                iRow = 5;
               
                foreach (DataRow r in dt.Rows)
                {
                    iRow++;
                    if (r[k].ToString() != "")
                    {
                        if (Convert.ToDecimal(r[k]) == maxLavel)
                        {
                            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[iRow, k+1], xlWorkSheet.Cells[iRow, k+1]);
                            //cellRange.Font.Color = System.Drawing.Color.White;
                            cellRange.Interior.Color = System.Drawing.Color.Red;
                        }
                        if ((Convert.ToDecimal(r[k]) == minLavel) && Convert.ToDouble(r[k]) != 0.0)
                        {
                            Microsoft.Office.Interop.Excel.Range cellRange2 = xlApp.get_Range(xlWorkSheet.Cells[iRow, k+1], xlWorkSheet.Cells[iRow, k+1]);
                            //cellRange2.Font.Color = System.Drawing.Color.White;
                            cellRange2.Interior.Color = System.Drawing.Color.Blue;
                        }
                    }

                }
                
            }

            // Page Setup
            //Set page orientation to landscape
            xlWorkSheet.PageSetup.Orientation = Microsoft.Office.Interop.Excel.XlPageOrientation.xlLandscape;

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 10;
            xlWorkSheet.PageSetup.RightMargin = 20;
            xlWorkSheet.PageSetup.BottomMargin = 0;
            xlWorkSheet.PageSetup.LeftMargin = 20;

            //Set gridlines
            xlWorkBook.Windows[1].DisplayGridlines = false;
            xlWorkSheet.PageSetup.PrintGridlines = false;

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
    }
}