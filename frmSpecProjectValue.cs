/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmSpecProjectValue.cs

 Programmer    : Diane Musachio

 Creation Date : 11/26/2018

 Inputs        : dbo.PROJ_VALUE (VIEW)

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display cases for
                 Project Value Percent Difference calculations.
                
 Detail Design : Detailed design for Special Project Value

 Other         : Called by: Tabulations -> Special -> 
                   Project Value

 Revisions     : See Below
 *********************************************************************
 Modified Date : 11/8/2021
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: 
 Description   : fix the bugs in creating excel file
 *********************************************************************
 Modified Date : 2/21/2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: CR885
 Description   : update excel file name from .xls to .xlsx
 *********************************************************************/
using CprsDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Globalization;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.IO;
using CprsBLL;
using System.Threading;

namespace Cprs
{
    public partial class frmSpecProjectValue : frmCprsParent
    {
        public frmSpecProjectValue()
        {
            InitializeComponent();
        }

        private DataTable dtTable;
        private DataTable TCDescription;
        private DataTable tcCounts;
        private string survey = "";

        private string selected_year = "";
        private string comparison = "";

        private string compvalue = "";
        private string filterSurvey;
        private string filterYear;
        private List<string> yearlist;
        private bool formloading = false;
        private string excelfile = "ValueComp.xlsx";
        private string selyr1;
        private string selyr2;
        SpecProjectValueData data_object;
        int year = DateTime.Now.Year;

        //Declare Excel Interop variables
        Excel.Application xlApp; 
        Excel.Workbook xlWorkBook;
        Excel.Worksheet xlWorkSheet;
        private frmMessageWait waiting;
        private string saveFilename;
        object misValue = System.Reflection.Missing.Value;

        private void frmSpecProjectValue_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            formloading = true;

            GetDropdown(year);
            cbYear.SelectedIndex = 0;

            rdV1.Checked = true;
            rdAll.Checked = true;

            selyr1 = (year - 2).ToString();
            selyr2 = (year - 1).ToString();

            data_object = new SpecProjectValueData();
            dtTable = data_object.GetProjValueData(selyr1, selyr2);

            //get datatable of newtc descriptions
            TCDescription = data_object.GetTCDescription();

            lblTitle.Text = comparison;
            lblTitle.Update();

            lblTitle2.Text = "Projects Completed From " + selected_year + " - " + survey;
            lblTitle2.Update();

            filterData();

            dgData.DataSource = tcCounts;

            //set up column headers
            SetColumnHeader(dgData);

            formloading = false;
        }

        private void filterData2(string filterYear2, string filterSurvey2, string compvalue2)
        {

            //use above criteria to filter datatable and store in data view
            DataView dv = new DataView(dtTable);
           
            if (filterYear2 != "")
            {
                dv.RowFilter = filterSurvey2 + " and " + filterYear2;
            }
            else
            {
                dv.RowFilter = filterSurvey2;
            }
            
            //assign dataview to datagrid
            tcCounts = GroupBy("TC2X", compvalue2, dv.ToTable());

            foreach (DataRow dr in tcCounts.Rows)
            {
                dr[0] = GetTCDescription(dr["TC2X"].ToString());
            }
        }

        private void filterData()
        {

            //use above criteria to filter datatable and store in data view
            DataView dv = new DataView(dtTable);

            if (!formloading)
            {
                if (filterYear != "")
                {
                    dv.RowFilter = filterSurvey + " and " + filterYear;
                }
                else
                {
                    dv.RowFilter = filterSurvey;
                }
            }

            //assign dataview to datagrid
            tcCounts = GroupBy("TC2X", compvalue, dv.ToTable());

            foreach (DataRow dr in tcCounts.Rows)
            {
                dr[0] = GetTCDescription(dr["TC2X"].ToString());
            }
        }

        private DataGridView SetColumnHeader(DataGridView dg)
        { 
            dg.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dg.Columns[0].HeaderText = "Type of Construction";
            dg.Columns[0].Width = 142;
            dg.Columns[0].Frozen = true;

            dg.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //all columns width
            for (int i = 1; i <= 23; i++)
            {
                dg.Columns[i].Width = 75;
            }

            //odd numbered columns formatted to display comma formatted integer
            //even numbered columns formatted to display rounded percentages
            for (int i = 1; i < 23; i++)
            {
                dg.Columns[i].DefaultCellStyle.Format = "N0";
                i++;
                dg.Columns[i].DefaultCellStyle.Format = "0\\%";
            }

            dg.Columns[1].HeaderText = "Over -100";
            dg.Columns[2].HeaderText = "Over -100%";
            dg.Columns[3].HeaderText = "-100 to -76";
            dg.Columns[4].HeaderText = "-100 to -76%";
            dg.Columns[5].HeaderText = "-75 to -51";
            dg.Columns[6].HeaderText = "-75 to -51%";
            dg.Columns[7].HeaderText = "-50 to -26";
            dg.Columns[8].HeaderText = "-50 to -26%";
            dg.Columns[9].HeaderText = "-25 to -1";
            dg.Columns[10].HeaderText = "-25 to -1%";
            dg.Columns[11].HeaderText = "0";
            dg.Columns[12].HeaderText = "0%";
            dg.Columns[13].HeaderText = "1 to 25";
            dg.Columns[14].HeaderText = "1 to 25%";
            dg.Columns[15].HeaderText = "26 to 50";
            dg.Columns[16].HeaderText = "26 to 50%";
            dg.Columns[17].HeaderText = "51 to 75";
            dg.Columns[18].HeaderText = "51 to 75%";
            dg.Columns[19].HeaderText = "76 to 100";
            dg.Columns[20].HeaderText = "76 to 100%";
            dg.Columns[21].HeaderText = "Over 100";
            dg.Columns[22].HeaderText = "Over 100%";
            dg.Columns[23].HeaderText = "All Cases";
            dg.Columns[23].DefaultCellStyle.Format = "N0";

            foreach (DataGridViewColumn dgvc in dg.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            return dg;
        }

        //filter data by various dropdown selections
        private DataTable GroupBy(string i_sGroupByColumn, string i_sAggregateColumn, DataTable i_dSourceTable)
        {
            DataTable dtGroup = new DataTable();

            //getting distinct values for group column
            dtGroup.Columns.Add("TC2X", typeof(string));
            dtGroup.Rows.Add("00");           
            dtGroup.Rows.Add("01");
            dtGroup.Rows.Add("02");
            dtGroup.Rows.Add("03");
            dtGroup.Rows.Add("04");
            dtGroup.Rows.Add("05");
            dtGroup.Rows.Add("06");
            dtGroup.Rows.Add("07");
            dtGroup.Rows.Add("08");
            dtGroup.Rows.Add("09");
            dtGroup.Rows.Add("11");
            dtGroup.Rows.Add("12");
            dtGroup.Rows.Add("13");
            dtGroup.Rows.Add("14");
            dtGroup.Rows.Add("15");
            dtGroup.Rows.Add("1T");

            //adding column for the row count
            dtGroup.Columns.Add("TotNOver", typeof(double));
            dtGroup.Columns.Add("PctNOver", typeof(double));
            dtGroup.Columns.Add("TotN100", typeof(double));
            dtGroup.Columns.Add("PctN100", typeof(double));
            dtGroup.Columns.Add("TotN75", typeof(double));
            dtGroup.Columns.Add("PctN75", typeof(double));
            dtGroup.Columns.Add("TotN50", typeof(double));
            dtGroup.Columns.Add("PctN50", typeof(double));
            dtGroup.Columns.Add("TotN25", typeof(double));
            dtGroup.Columns.Add("PctN25", typeof(double));

            dtGroup.Columns.Add("TotZero", typeof(double));
            dtGroup.Columns.Add("PctZero", typeof(double));

            dtGroup.Columns.Add("TotP25", typeof(double));
            dtGroup.Columns.Add("PctP25", typeof(double));
            dtGroup.Columns.Add("TotP50", typeof(double));
            dtGroup.Columns.Add("PctP50", typeof(double));
            dtGroup.Columns.Add("TotP75", typeof(double));
            dtGroup.Columns.Add("PctP75", typeof(double));
            dtGroup.Columns.Add("TotP100", typeof(double));
            dtGroup.Columns.Add("PctP100", typeof(double));
            dtGroup.Columns.Add("TotPOver", typeof(double));
            dtGroup.Columns.Add("PctPOver", typeof(double));

            dtGroup.Columns.Add("Totcases", typeof(double));

            //looping thru distinct values for the group, counting
            foreach (DataRow dr in dtGroup.Rows)
            {
                dr["TotNOver"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = -5");
                dr["TotN100"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = -4");
                dr["TotN75"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = -3");
                dr["TotN50"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = -2");
                dr["TotN25"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = -1");
                dr["TotZero"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = 0");
                dr["TotP25"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = 1");
                dr["TotP50"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = 2");
                dr["TotP75"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = 3");
                dr["TotP100"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = 4");
                dr["TotPOver"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'" + " and " + i_sAggregateColumn + " = 5");

                dr["Totcases"] = i_dSourceTable.Compute("Count(" + i_sAggregateColumn + ")", i_sGroupByColumn + " = '" + dr[i_sGroupByColumn] + "'");

                double Totcases = Convert.ToDouble(dr["Totcases"]);

                double TotNOver = Convert.ToDouble(dr["TotNOver"]);
                double TotN100 = Convert.ToDouble(dr["TotN100"]);
                double TotN75 = Convert.ToDouble(dr["TotN75"]);
                double TotN50 = Convert.ToDouble(dr["TotN50"]);
                double TotN25 = Convert.ToDouble(dr["TotN25"]);
                double TotZero = Convert.ToDouble(dr["TotZero"]);
                double TotP25 = Convert.ToDouble(dr["TotP25"]);
                double TotP50 = Convert.ToDouble(dr["TotP50"]);
                double TotP75 = Convert.ToDouble(dr["TotP75"]);
                double TotP100 = Convert.ToDouble(dr["TotP100"]);
                double TotPOver = Convert.ToDouble(dr["TotPOver"]);

                if (Totcases > 0)
                {
                    dr["PctNOver"] = TotNOver / Totcases * 100;
                    dr["PctN100"] = TotN100 / Totcases * 100;
                    dr["PctN75"] = TotN75 / Totcases * 100;
                    dr["PctN50"] = TotN50 / Totcases * 100;
                    dr["PctN25"] = TotN25 / Totcases * 100;
                    dr["PctZero"] = TotZero / Totcases * 100;
                    dr["PctP25"] = TotP25 / Totcases * 100;
                    dr["PctP50"] = TotP50 / Totcases * 100;
                    dr["PctP75"] = TotP75 / Totcases * 100;
                    dr["PctP100"] = TotP100 / Totcases * 100;
                    dr["PctPOver"] = TotPOver / Totcases * 100;
                }
                else
                {
                    dr["PctNOver"] = 0;
                    dr["PctN100"] = 0;
                    dr["PctN75"] = 0;
                    dr["PctN50"] = 0;
                    dr["PctN25"] = 0;
                    dr["PctZero"] = 0;
                    dr["PctP25"] = 0;
                    dr["PctP50"] = 0;
                    dr["PctP75"] = 0;
                    dr["PctP100"] = 0;
                    dr["PctPOver"] = 0;
                }
            }

            //to get percentage divisor or dividend needs to be a double...integers will result in zeros
            double sum = Convert.ToDouble(dtGroup.Compute("SUM(Totcases)", string.Empty));
            double sumnover = Convert.ToDouble(dtGroup.Compute("SUM(TotNOver)", string.Empty));
            double sumn100 = Convert.ToDouble(dtGroup.Compute("SUM(TotN100)", string.Empty));
            double sumn75 = Convert.ToDouble(dtGroup.Compute("SUM(TotN75)", string.Empty));
            double sumn50 = Convert.ToDouble(dtGroup.Compute("SUM(TotN50)", string.Empty));
            double sumn25 = Convert.ToDouble(dtGroup.Compute("SUM(TotN25)", string.Empty));
            double sumzero = Convert.ToDouble(dtGroup.Compute("SUM(TotZero)", string.Empty));
            double sump25 = Convert.ToDouble(dtGroup.Compute("SUM(TotP25)", string.Empty));
            double sump50 = Convert.ToDouble(dtGroup.Compute("SUM(TotP50)", string.Empty));
            double sump75 = Convert.ToDouble(dtGroup.Compute("SUM(TotP75)", string.Empty));
            double sump100 = Convert.ToDouble(dtGroup.Compute("SUM(TotP100)", string.Empty));
            double sumpover = Convert.ToDouble(dtGroup.Compute("SUM(TotPOver)", string.Empty));

            double pctnover = 0;
            double pctn100 = 0;
            double pctn75 = 0;
            double pctn50 = 0;
            double pctn25 = 0;
            double pctzero = 0;
            double pctp25 = 0;
            double pctp50 = 0;
            double pctp75 = 0;
            double pctp100 = 0;
            double pctpover = 0;

            if (sum > 0)
            {
                pctnover = sumnover / sum * 100;
                pctn100 = sumn100 / sum * 100;
                pctn75 = sumn75 / sum * 100;
                pctn50 = sumn50 / sum * 100;
                pctn25 = sumn25 / sum * 100;
                pctzero = sumzero / sum * 100;
                pctp25 = sump25 / sum * 100;
                pctp50 = sump50 / sum * 100;
                pctp75 = sump75 / sum * 100;
                pctp100 = sump100 / sum * 100;
                pctpover = sumpover / sum * 100;
            }

            //add new row with totals for each column
            DataRow newRow = dtGroup.NewRow();

            newRow[0] = "   Total";
            newRow[1] = sumnover;
            newRow[2] = pctnover;
            newRow[3] = sumn100;
            newRow[4] = pctn100;
            newRow[5] = sumn75;
            newRow[6] = pctn75;
            newRow[7] = sumn50;
            newRow[8] = pctn50;
            newRow[9] = sumn25;
            newRow[10] = pctn25;
            newRow[11] = sumzero;
            newRow[12] = pctzero;
            newRow[13] = sump25;
            newRow[14] = pctp25;
            newRow[15] = sump50;
            newRow[16] = pctp50;
            newRow[17] = sump75;
            newRow[18] = pctp75;
            newRow[19] = sump100;
            newRow[20] = pctp100;
            newRow[21] = sumpover;
            newRow[22] = pctpover;
            newRow[23] = sum;

            //inserts new row at top
            dtGroup.Rows.InsertAt(newRow, 0);

            
            //returning grouped/counted result
            return dtGroup;
        }

        //populate header on screen and get filter value based on comparison radio button selected
        private void radioButtonsComparison_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (rdV1.Checked)
            {
                comparison = "COMPARISON OF CENSUS ORIGINAL VALUE vs DODGE'S PROJECT VALUE";
                compvalue = "PIDIFF";
            }
            else if (rdV2.Checked)
            {
                comparison = "COMPARISON OF CENSUS FINAL VALUE vs DODGE'S PROJECT VALUE";
                compvalue = "PRDIFF";
            }
            else if (rdV3.Checked)
            {
                comparison = "COMPARISON OF CENSUS FINAL VALUE vs CENSUS ORIGINAL VALUE";
                compvalue = "IRDIFF";
            }

            if (!formloading)
            {
                lblTitle.Text = comparison;
                lblTitle.Update();

                lblTitle2.Text = "Projects Completed From " + selected_year + " - " + survey;
                lblTitle2.Update();

                filterData();

                dgData.DataSource = tcCounts;

                //set up column headers
                SetColumnHeader(dgData);
            }
        }

        //filter survey based on radio button survey selection
        private void radioButtonsSurvey_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (rdAll.Checked)
            {
                survey = "All Surveys";
                filterSurvey = " survey in ('F', 'P', 'N')";
            }
            else if (rdFederal.Checked)
            {
                survey = "Federal";
                filterSurvey = " survey = 'F'";
            }
            else if (rdState.Checked)
            {
                survey = "State and Local";
                filterSurvey = " survey = 'P'";
            }
            else if (rdPrivate.Checked)
            {
                survey = "Private";
                filterSurvey = " survey = 'N'";
            }

            if (!formloading)
            {
                lblTitle.Text = comparison;
                lblTitle.Update();

                lblTitle2.Text = "Projects Completed From " + selected_year + " - " + survey;
                lblTitle2.Update();

                filterData();

                dgData.DataSource = tcCounts;

                //set up column headers
                SetColumnHeader(dgData);
            }
        }

        //dropdown for years to display in combobox
        private void GetDropdown(int col)
        {
            yearlist = new List<string>();

            yearlist.Add("All Years".ToString());
            yearlist.Add((col - 1).ToString());
            yearlist.Add((col - 2).ToString());

            cbYear.DataSource = yearlist;
        }

        //code to fill in header and filter year based on year selection
        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbYear.SelectedIndex == 0)
            {
                selected_year = (year - 2).ToString() + " to " + (year - 1).ToString();
                filterYear = "";
            }
            else
            {
                selected_year = cbYear.Text;
                filterYear = " compyear = " + selected_year;
            }

            rdV1.Checked = true;
            rdAll.Checked = true;

            if (!formloading)
            {
                lblTitle.Text = comparison;
                lblTitle.Update();

                lblTitle2.Text = "Projects Completed From " + selected_year + " - " + survey;
                lblTitle2.Update();

                filterData();

                dgData.DataSource = tcCounts;

                //set up column headers
                SetColumnHeader(dgData);
            }
        }

        private string GetTCDescription(string tc)
        {
            string tcdesc = tc;

            foreach (DataRow row in TCDescription.Rows)
            {
                if (tc == row["Newtc"].ToString().Trim())
                    tcdesc = tc + " " + row["TCDESCRIPTION"].ToString();
            }
            return tcdesc;
        }


        string compname;

        //create excel
        private void ExportToExcel(string Year, string compvalue2)
        {
            string Title;
            string Title2;
            string compexcel = "";
            string filterYear2 = "";
            string filterSurvey2 = "";

            if (compvalue2 == "PIDIFF")
            {
                compname = "Orig Value to Proj Value";
                compexcel = "Comparison of Census Original Value vs Dodge's Project Value";
            }
            else if (compvalue2 == "PRDIFF")
            {
                compname = "Fin Value to Proj Value";
                compexcel = "Comparison of Census Final Value vs Dodge's Project Value";
            }
            else if (compvalue2 == "IRDIFF")
            {
                compname = "Fin Value to Orig Value";
                compexcel = "Comparison of Census Final Value vs Census Original Value";
            }

            xlWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)xlWorkBook.Worksheets.Add();

            foreach (Excel.Worksheet ws in xlWorkBook.Worksheets)
            {
                ws.Activate();
                ws.Application.ActiveWindow.SplitColumn = 1;
                ws.Application.ActiveWindow.SplitRow = 5;
                ws.Application.ActiveWindow.FreezePanes = true;
                ws.Application.ActiveWindow.DisplayGridlines = false;
            }

            if (Year == "")
            {
                xlWorkSheet.Name = "All - " + compname;
                filterYear2 = "";
                Title = compexcel;
                Title2 = "Projects Completed in " + (year - 2).ToString() + " and " + (year - 1).ToString();
            }
            else
            {
                xlWorkSheet.Name = Year + " - " + compname;// "  - Proj Value to Orig Value";
                filterYear2 = " compyear = " + Year;
                Title = compexcel;
                Title2 = "Projects Completed in " + Year; ;
            }

            xlWorkSheet.Rows.Font.Size = 10;
            xlWorkSheet.Rows.Font.Name = "Arial";
            xlWorkSheet.Rows.RowHeight = 15;

            //Add a title
            xlWorkSheet.Cells[1, 1] = Title;
            xlWorkSheet.Cells[2, 1] = Title2;
            xlWorkSheet.Columns.ColumnWidth = 13;

            int last_col = 24;

            //Span the title across columns A through X
            Microsoft.Office.Interop.Excel.Range titleRange = xlApp.get_Range(xlWorkSheet.Cells[1, "A"], xlWorkSheet.Cells[1, last_col]);
            titleRange.Merge(Type.Missing);
            titleRange.Font.Size = 14;
            titleRange.RowHeight = 31.5;
            titleRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
            titleRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Gray;

            //Span the title across columns A through X
            Microsoft.Office.Interop.Excel.Range titleRange2 = xlApp.get_Range(xlWorkSheet.Cells[2, "A"], xlWorkSheet.Cells[2, last_col]);
            titleRange2.Merge(Type.Missing);
            titleRange2.Font.Size = 14;
            titleRange2.RowHeight = 63.75;
            titleRange2.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            titleRange2.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Gray;

            //Span the title across columns A 
            Microsoft.Office.Interop.Excel.Range titleRange1 = xlApp.get_Range(xlWorkSheet.Cells[3, "A"], xlWorkSheet.Cells[3, last_col]);
            titleRange1.Merge(Type.Missing);
            titleRange1.RowHeight = 12;
            titleRange1.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
        
            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange3 = xlApp.get_Range(xlWorkSheet.Cells[4, "B"], xlWorkSheet.Cells[4, "C"]);
            titleRange3.Merge(Type.Missing);
            titleRange3.Font.Size = 10;
            titleRange3.RowHeight = 39;
            titleRange3.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange4 = xlApp.get_Range(xlWorkSheet.Cells[4, "D"], xlWorkSheet.Cells[4, "E"]);
            titleRange4.Merge(Type.Missing);
            titleRange4.Font.Size = 10;
            titleRange4.RowHeight = 39;
            titleRange4.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange5 = xlApp.get_Range(xlWorkSheet.Cells[4, "F"], xlWorkSheet.Cells[4, "G"]);
            titleRange5.Merge(Type.Missing);
            titleRange5.Font.Size = 10;
            titleRange5.RowHeight = 39;
            titleRange5.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange6 = xlApp.get_Range(xlWorkSheet.Cells[4, "H"], xlWorkSheet.Cells[4, "I"]);
            titleRange6.Merge(Type.Missing);
            titleRange6.Font.Size = 10;
            titleRange6.RowHeight = 39;
            titleRange6.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange7 = xlApp.get_Range(xlWorkSheet.Cells[4, "J"], xlWorkSheet.Cells[4, "K"]);
            titleRange7.Merge(Type.Missing);
            titleRange7.Font.Size = 10;
            titleRange7.RowHeight = 39;
            titleRange7.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange8 = xlApp.get_Range(xlWorkSheet.Cells[4, "L"], xlWorkSheet.Cells[4, "M"]);
            titleRange8.Merge(Type.Missing);
            titleRange8.Font.Size = 10;
            titleRange8.RowHeight = 39;
            titleRange8.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange9 = xlApp.get_Range(xlWorkSheet.Cells[4, "N"], xlWorkSheet.Cells[4, "O"]);
            titleRange9.Merge(Type.Missing);
            titleRange9.Font.Size = 10;
            titleRange9.RowHeight = 39;
            titleRange9.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange10 = xlApp.get_Range(xlWorkSheet.Cells[4, "P"], xlWorkSheet.Cells[4, "Q"]);
            titleRange10.Merge(Type.Missing);
            titleRange10.Font.Size = 10;
            titleRange10.RowHeight = 39;
            titleRange10.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange11 = xlApp.get_Range(xlWorkSheet.Cells[4, "R"], xlWorkSheet.Cells[4, "S"]);
            titleRange11.Merge(Type.Missing);
            titleRange11.Font.Size = 10;
            titleRange11.RowHeight = 39;
            titleRange11.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange12 = xlApp.get_Range(xlWorkSheet.Cells[4, "T"], xlWorkSheet.Cells[4, "U"]);
            titleRange12.Merge(Type.Missing);
            titleRange12.Font.Size = 10;
            titleRange12.RowHeight = 39;
            titleRange12.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;

            //Span the title across 2 columns 
            Microsoft.Office.Interop.Excel.Range titleRange13 = xlApp.get_Range(xlWorkSheet.Cells[4, "V"], xlWorkSheet.Cells[4, "W"]);
            titleRange13.Merge(Type.Missing);
            titleRange13.Font.Size = 10;
            titleRange13.RowHeight = 39;
            titleRange13.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight; 

            Microsoft.Office.Interop.Excel.Range titleRange14 = xlApp.get_Range(xlWorkSheet.Cells[4, "X"], xlWorkSheet.Cells[4, "X"]);
            titleRange14.Merge(Type.Missing);
            titleRange14.Font.Size = 10;
            titleRange14.RowHeight = 39;
            titleRange14.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight; 

            //Populate headers
            Microsoft.Office.Interop.Excel.Range cellRange = xlApp.get_Range(xlWorkSheet.Cells[5, 1], xlWorkSheet.Cells[5, last_col]);
            cellRange.Font.Size = 10;
            cellRange.RowHeight = 15;
            cellRange.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignRight;
            cellRange.Borders[Excel.XlBordersIndex.xlEdgeBottom].Color = Color.Gray;

            Microsoft.Office.Interop.Excel.Range highlightRange = xlApp.get_Range(xlWorkSheet.Cells[25, 2], xlWorkSheet.Cells[25, 11]);
            highlightRange.Merge(Type.Missing);
            highlightRange.Interior.ColorIndex = 6;
            highlightRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlightRange.NumberFormat = "#\\%";
            highlightRange.Borders.Color = Color.Black;
            Microsoft.Office.Interop.Excel.Range highlight1Range = xlApp.get_Range(xlWorkSheet.Cells[25, 12], xlWorkSheet.Cells[25, 13]);
            highlight1Range.Merge(Type.Missing);
            highlight1Range.NumberFormat = "#\\%";
            highlight1Range.Interior.ColorIndex = 3;
            highlight1Range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight1Range.Borders.Color = Color.Black;
            Microsoft.Office.Interop.Excel.Range highlight2Range = xlApp.get_Range(xlWorkSheet.Cells[25, 14], xlWorkSheet.Cells[25, 23]);
            highlight2Range.Merge(Type.Missing);
            highlight2Range.NumberFormat = "#\\%";
            highlight2Range.Interior.ColorIndex = 6;
            highlight2Range.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight2Range.Borders.Color = Color.Black;

            Microsoft.Office.Interop.Excel.Range highlightRange1 = xlApp.get_Range(xlWorkSheet.Cells[46, 2], xlWorkSheet.Cells[46, 11]);
            highlightRange1.Merge(Type.Missing);
            highlightRange1.Interior.ColorIndex = 6;
            highlightRange1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlightRange1.Borders.Color = Color.Black;
            highlightRange1.NumberFormat = "#\\%";
            Microsoft.Office.Interop.Excel.Range highlight1Range1 = xlApp.get_Range(xlWorkSheet.Cells[46, 12], xlWorkSheet.Cells[46, 13]);
            highlight1Range1.Merge(Type.Missing);
            highlight1Range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight1Range1.NumberFormat = "#\\%";
            highlight1Range1.Borders.Color = Color.Black;
            highlight1Range1.Interior.ColorIndex = 3;
            Microsoft.Office.Interop.Excel.Range highlight2Range1 = xlApp.get_Range(xlWorkSheet.Cells[46, 14], xlWorkSheet.Cells[46, 23]);
            highlight2Range1.Merge(Type.Missing);
            highlight2Range1.NumberFormat = "#\\%";
            highlight2Range1.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight2Range1.Borders.Color = Color.Black;
            highlight2Range1.Interior.ColorIndex = 6;

            Microsoft.Office.Interop.Excel.Range highlightRange2 = xlApp.get_Range(xlWorkSheet.Cells[67, 2], xlWorkSheet.Cells[67, 11]);
            highlightRange2.Merge(Type.Missing);
            highlightRange2.Interior.ColorIndex = 6;
            highlightRange2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlightRange2.Borders.Color = Color.Black;
            highlightRange2.NumberFormat = "#\\%";
            Microsoft.Office.Interop.Excel.Range highlight1Range2 = xlApp.get_Range(xlWorkSheet.Cells[67, 12], xlWorkSheet.Cells[67, 13]);
            highlight1Range2.Merge(Type.Missing);
            highlight1Range2.Interior.ColorIndex = 3;
            highlight1Range2.NumberFormat = "#\\%";
            highlight1Range2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight1Range2.Borders.Color = Color.Black;
            Microsoft.Office.Interop.Excel.Range highlight2Range2 = xlApp.get_Range(xlWorkSheet.Cells[67, 14], xlWorkSheet.Cells[67, 23]);
            highlight2Range2.Merge(Type.Missing);
            highlight2Range2.NumberFormat = "#\\%";
            highlight2Range2.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight2Range2.Borders.Color = Color.Black;
            highlight2Range2.Interior.ColorIndex = 6;

            Microsoft.Office.Interop.Excel.Range highlightRange3 = xlApp.get_Range(xlWorkSheet.Cells[88, 2], xlWorkSheet.Cells[88, 11]);
            highlightRange3.Merge(Type.Missing);
            highlightRange3.Interior.ColorIndex = 6;
            highlightRange3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlightRange3.Borders.Color = Color.Black;
            highlightRange3.NumberFormat = "#\\%";
            Microsoft.Office.Interop.Excel.Range highlight1Range3 = xlApp.get_Range(xlWorkSheet.Cells[88, 12], xlWorkSheet.Cells[88, 13]);
            highlight1Range3.Merge(Type.Missing);
            highlight1Range3.Interior.ColorIndex = 3;
            highlight1Range3.NumberFormat = "#\\%";
            highlight1Range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight1Range3.Borders.Color = Color.Black;
            Microsoft.Office.Interop.Excel.Range highlight2Range3= xlApp.get_Range(xlWorkSheet.Cells[88, 14], xlWorkSheet.Cells[88, 23]);
            highlight2Range3.Merge(Type.Missing);
            highlight2Range3.NumberFormat = "#\\%";
            highlight2Range3.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            highlight2Range3.Borders.Color = Color.Black;
            highlight2Range3.Interior.ColorIndex = 6;

            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).ColumnWidth = 25;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[1, Type.Missing]).HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            for (int i = 2; i < last_col; i++)
            {
                //even number columns
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "#,##0";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 9.57;
                i++;
                //odd number columns
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).NumberFormat = "0\\%";
                ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[i, Type.Missing]).ColumnWidth = 9.57;
               
            }


            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[24, Type.Missing]).NumberFormat = "#,##0";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Columns[24, Type.Missing]).ColumnWidth = 9.57;

            xlWorkSheet.Cells[4, 2] = "Percent\nDifference\nOver -100";
            xlWorkSheet.Cells[5, 2] = "Obs";
            xlWorkSheet.Cells[5, 3] = "%";
            xlWorkSheet.Cells[4, 4] = "Percent\nDifference\n-100 to -76";
            xlWorkSheet.Cells[5, 4] = "Obs";
            xlWorkSheet.Cells[5, 5] = "%";
            xlWorkSheet.Cells[4, 6] = "Percent\nDifference\n-75 to -51";
            xlWorkSheet.Cells[5, 6] = "Obs";
            xlWorkSheet.Cells[5, 7] = "%";
            xlWorkSheet.Cells[4, 8] = "Percent\nDifference\n-50 to -26";
            xlWorkSheet.Cells[5, 8] = "Obs";
            xlWorkSheet.Cells[5, 9] = "%";
            xlWorkSheet.Cells[4, 10] = "Percent\nDifference\n-25 to -1";
            xlWorkSheet.Cells[5, 10] = "Obs";
            xlWorkSheet.Cells[5, 11] = "%";
            xlWorkSheet.Cells[4, 12] = "Percent\nDifference\n0";
            xlWorkSheet.Cells[5, 12] = "Obs";
            xlWorkSheet.Cells[5, 13] = "%";
            xlWorkSheet.Cells[4, 14] = "Percent\nDifference\n1 to 25";
            xlWorkSheet.Cells[5, 14] = "Obs";
            xlWorkSheet.Cells[5, 15] = "%";
            xlWorkSheet.Cells[4, 16] = "Percent\nDifference\n26 to 50";
            xlWorkSheet.Cells[5, 16] = "Obs";
            xlWorkSheet.Cells[5, 17] = "%";
            xlWorkSheet.Cells[4, 18] = "Percent\nDifference\n51 to 75";
            xlWorkSheet.Cells[5, 18] = "Obs";
            xlWorkSheet.Cells[5, 19] = "%";
            xlWorkSheet.Cells[4, 20] = "Percent\nDifference\n76 to 100";
            xlWorkSheet.Cells[5, 20] = "Obs";
            xlWorkSheet.Cells[5, 21] = "%";
            xlWorkSheet.Cells[4, 22] = "Percent\nDifference\nOver 100";
            xlWorkSheet.Cells[5, 22] = "Obs";
            xlWorkSheet.Cells[5, 23] = "%";
            xlWorkSheet.Cells[4, 24] = "All\nCases";

            filterSurvey2 = " survey in ('F', 'P', 'N')";
            filterData2(filterYear2, filterSurvey2, compvalue2);

            ///*for datatable */
            DataTable dt = tcCounts;

            xlWorkSheet.Cells[6, 1]= "All";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[6, 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[6, 1]).Font.Bold = true;

            //////Populate rest of the data. Start at row[8] 
            int iRow = 7;
            int iCol = 0;

            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= last_col)
                    {
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                    }
                }

            }

            for (int i = 8; i <= 24; i++)
            {
                Microsoft.Office.Interop.Excel.Range borderRange = xlApp.get_Range(xlWorkSheet.Cells[i, 12], xlWorkSheet.Cells[i, 13]);
                Microsoft.Office.Interop.Excel.Range rowColor = xlApp.get_Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, last_col]);
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color.Black;
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color.Black;

                if (i == 8)
                {
                    borderRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                }

                if (i % 2 == 0)
                {
                    rowColor.Interior.Color = Color.LightBlue;
                }
            }

            highlightRange.Value = "=SUM(C8+ E8 + G8 + I8 + K8)";
            highlight1Range.Value = "= M8" ;
            highlight2Range.Value = "=SUM(O8+ Q8 + S8 + U8 + W8)" ;

            filterSurvey2 = " survey = 'N'";
            filterData2(filterYear2, filterSurvey2, compvalue2);

            ///*for datatable */
            dt = tcCounts;

            xlWorkSheet.Cells[27, 1] = "Private";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[27, 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[27, 1]).Font.Bold = true;

            //////Populate rest of the data. 
            iRow = 28;
            iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= last_col)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }


            for (int i = 29; i <= 45; i++)
            {
                Microsoft.Office.Interop.Excel.Range borderRange = xlApp.get_Range(xlWorkSheet.Cells[i, 12], xlWorkSheet.Cells[i, 13]);
                Microsoft.Office.Interop.Excel.Range rowColor = xlApp.get_Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, last_col]);
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color.Black;
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color.Black;


                if (i == 29)
                {
                    borderRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                }

                if (i % 2 != 0)
                {
                    rowColor.Interior.Color = Color.LightBlue;
                }
            }

            highlightRange1.Value = "=SUM(C29+ E29 + G29 + I29 + K29)";
            highlight1Range1.Value = "= M29";
            highlight2Range1.Value = "=SUM(O29+ Q29 + S29 + U29 + W29)";

            filterSurvey2 = " survey = 'P'";
            filterData2(filterYear2, filterSurvey2, compvalue2);

            ///*for datatable */
            dt = tcCounts;

            xlWorkSheet.Cells[48, 1] = "State and Local";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[48, 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[48, 1]).Font.Bold = true;

            //////Populate rest of the data.
            iRow = 49; 
            iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                    {
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    }
                    else if (iCol <= last_col)
                    {
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                    }
                }
            }

            for (int i = 50; i <= 66; i++)
            {
                Microsoft.Office.Interop.Excel.Range borderRange = xlApp.get_Range(xlWorkSheet.Cells[i, 12], xlWorkSheet.Cells[i, 13]);
                Microsoft.Office.Interop.Excel.Range rowColor = xlApp.get_Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, last_col]);
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color.Black;
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color.Black;

                if (i == 50)
                {
                    borderRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                }

                if (i % 2 == 0)
                {
                    rowColor.Interior.Color = Color.LightBlue; ;
                }
            }

            highlightRange2.Value = "=SUM(C50+ E50 + G50 + I50 + K50)";
            highlight1Range2.Value = "= M50";
            highlight2Range2.Value = "=SUM(O50+ Q50 + S50 + U50 + W50)";

            filterSurvey2 = " survey = 'F'";
            filterData2(filterYear2, filterSurvey2, compvalue2);
            ///*for datatable */
            dt = tcCounts;

            xlWorkSheet.Cells[69, 1] = "Federal";
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[69, 1]).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft;
            ((Microsoft.Office.Interop.Excel.Range)xlWorkSheet.Cells[69, 1]).Font.Bold = true;

            //////Populate rest of the data.
            iRow = 70;
            iCol = 0;
            foreach (DataRow r in dt.Rows)
            {
                iRow++;
                iCol = 0;

                foreach (DataColumn c in dt.Columns)
                {
                    iCol++;
                    if (iCol == 1)
                        xlWorkSheet.Cells[iRow, iCol] = "\t" + r[c.ColumnName].ToString();
                    else if (iCol <= last_col)
                        xlWorkSheet.Cells[iRow, iCol] = r[c.ColumnName].ToString();
                }
            }

            for (int i = 71; i <= 87; i++)
            {
                Microsoft.Office.Interop.Excel.Range borderRange = xlApp.get_Range(xlWorkSheet.Cells[i, 12], xlWorkSheet.Cells[i, 13]);
                Microsoft.Office.Interop.Excel.Range rowColor = xlApp.get_Range(xlWorkSheet.Cells[i, 1], xlWorkSheet.Cells[i, last_col]);
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeLeft].Color = Color.Black;
                borderRange.Borders[Excel.XlBordersIndex.xlEdgeRight].Color = Color.Black;

                if (i == 71)
                {
                    borderRange.Borders[Excel.XlBordersIndex.xlEdgeTop].Color = Color.Black;
                }

                if (i % 2 != 0)
                {
                    rowColor.Interior.Color = Color.LightBlue; ;
                }
            }

            highlightRange3.Value = "=SUM(C71+ E71 + G71 + I71 + K71)";
            highlight1Range3.Value = "= M71";
            highlight2Range3.Value = "=SUM(O71+ Q71 + S71 + U71 + W71)";

            //Set margins
            xlWorkSheet.PageSetup.TopMargin = 40;
            xlWorkSheet.PageSetup.RightMargin = 80;
            xlWorkSheet.PageSetup.BottomMargin = 0;
            xlWorkSheet.PageSetup.LeftMargin = 80;

            //set as landscape
            xlWorkSheet.PageSetup.Orientation = Excel.XlPageOrientation.xlLandscape;

            //set to fit on page for printing
            xlWorkSheet.PageSetup.FitToPagesWide = 1;
            xlWorkSheet.PageSetup.FitToPagesTall = false;

            xlWorkSheet.Select(Type.Missing);
        }

        private void btnTable_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file|*.xlsx";
            saveFileDialog1.Title = "Save a File";

            saveFileDialog1.FileName = excelfile;

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            //save file name
            saveFilename = saveFileDialog1.FileName;

            //delete exist file
            FileInfo fileInfo = new FileInfo(saveFilename);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            //this.Enabled = false; prevents screen disappearing when commented out

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
            string sfilename = dir + "\\" + excelfile;

            //delete exist file
            GeneralFunctions.DeleteFile(sfilename);

            //Initialize variables
            xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            ExportToExcel(selyr1, "IRDIFF");
            ExportToExcel(selyr2, "IRDIFF");
            ExportToExcel("".ToString(), "IRDIFF");

            ExportToExcel(selyr1, "PRDIFF");
            ExportToExcel(selyr2, "PRDIFF");
            ExportToExcel("".ToString(), "PRDIFF");

            ExportToExcel(selyr1, "PIDIFF");
            ExportToExcel(selyr2, "PIDIFF");
            ExportToExcel("".ToString(), "PIDIFF");
     
            // Save file & Quit application
            xlApp.DisplayAlerts = false; //Suppress overwrite request
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
            MessageBox.Show("Files have been created");
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            //The dgprint is a clone of the data so the
            //display doesn't change format

            //References to source and target grid.
            DataGridView sourceGrid = dgData;
            DataGridView targetGrid = this.dgPrint;

            //Copy all rows and cells.
            var targetRows = new List<DataGridViewRow>();

            foreach (DataGridViewRow sourceRow in sourceGrid.Rows)
            {
                if (!sourceRow.IsNewRow)
                {
                    var targetRow = (DataGridViewRow)sourceRow.Clone();

                    foreach (DataGridViewCell cell in sourceRow.Cells)
                    {
                        targetRow.Cells[cell.ColumnIndex].Value = cell.Value;
                    }

                    targetRows.Add(targetRow);
                }
            }

            //Clear target columns and then clone all source columns.

            targetGrid.Columns.Clear();

            foreach (DataGridViewColumn column in sourceGrid.Columns)
            {
                targetGrid.Columns.Add((DataGridViewColumn)column.Clone());
            }

            targetGrid.Rows.AddRange(targetRows.ToArray());

            DGVPrinter printer = new DGVPrinter();

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.Title = lblTitle.Text;
            printer.SubTitle = lblTitle2.Text;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.Userinfo = UserInfo.UserName;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPrint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Special Project Value";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgPrint.Columns[0].Width = 130;
            for (int i = 1; i < dgPrint.Columns.Count - 1; i++)
            {
                dgPrint.Columns[i].Width = 37;
            }
            dgPrint.Columns[dgPrint.Columns.Count - 1].Width = 45;

            this.dgPrint.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            printer.PrintDataGridViewWithoutDialog(dgPrint);

            Cursor.Current = Cursors.Default;
        }


        //close form
        private void frmSpecProjectValue_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

    }
}
