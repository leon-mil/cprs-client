/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabAnnualTab.cs
Programmer    : Diane Musachio
Creation Date : April 26, 2019
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create screen to display annual tabulations
Change Request: 
Detail Design : Detailed Design of Annual Tabulations
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
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
using System.Collections.Specialized;
using System.Collections;
using System.Reflection;
using System.Diagnostics;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmTabAnnualTabs : frmCprsParent
    {
        public Form CallingForm = null;
      
        TabAnnualTabsData data_object = new TabAnnualTabsData();

        //holds saved calculated values throughout processes
        private DataTable calculated = new DataTable();
        private DataTable newtotals = new DataTable();
        private DataTable dt = new DataTable();
        DataView dv = new DataView();

        //bools keep track for selected screens
        private bool show_subtc;
        private bool old_factors;
        private bool seasonal;
        private bool formloading = false;
        private bool formatint = false;
        private string sdate;

        //used throughout all of code so made global
        private string year1;
        private string year2; 
        private string year3; 
        private string year4;
        private string year5; 

        //retain for headings and repeated formats
        private string type;
        private string survey;
        private string money;
        private string owner;
        private int divisor;
        private decimal ucf;

        //keep string/lists
        private decimal[] col_value;
        private string[] colnames;

        //store dates to export later
        private string[] gridDates = new string[72];

        //store lsf and bst and saf data
        private decimal[] lsflist;
        private decimal[] bstlist;
        private decimal[] saflist;

        public frmTabAnnualTabs()
        {
            InitializeComponent();
        }

        private void frmTabAnnualTab_Load(object sender, EventArgs e)
        {
            //this is to prevent re-load error
            //if export to tsar pushed
            if (sender is Button)
            {
                return;
            }
            else
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            formloading = true;

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //not showing 3 digit and 4 digit tcs
            show_subtc = false;

            //calculate date from current survey month
            sdate = data_object.GetCurrMonthDateinTable();
            string year_survey = sdate.ToString().Substring(0, 4);
            year1 = (Convert.ToInt16(year_survey) - 1).ToString();
            year2 = (Convert.ToInt16(year_survey) - 2).ToString();
            year3 = (Convert.ToInt16(year_survey) - 3).ToString();
            year4 = (Convert.ToInt16(year_survey) - 4).ToString();
            year5 = (Convert.ToInt16(year_survey) - 5).ToString();

            //default selections 
            //state and local seasonally adjusted old factors
            rdseasadj.Checked = true;
            rdsl.Checked = true;
            seasonal = true;
            btnView.Enabled = false;
            old_factors = true;

            //checks to see if btnExport is enabled
            btnExportEnabled();

            //sets up load labels
            lblMoney.Text = "Billions of Dollars";
            type = " VIP SEASONALLY ADJUSTED AND BOOSTED";
            btnApply.Text = "APPLY NEW FACTORS";

            //get data from dataview
            dt = data_object.GetAnnualData();

            //build list of columns
            GetColumnsNames();

            //set up datatable to hold calculated nsa values
            CreateExport();

            //filter based on selected radio buttons
            FilterData();

            //set up datagrid headers
            GetColumnHeaders();

            formloading = false;
        }

        private DataTable dt_view = new DataTable();

        private void FilterData()
        {
            //hourglass
            Cursor.Current = Cursors.WaitCursor;

            //clear out export to tsar data
            export.Clear();

            lblTitle.Text = survey + " " + year5 + "-" + year1 + " " + type;

            dv = dt.DefaultView;
            dv.RowFilter = "owner = " + owner.AddSqlQuotes();
            dv.Sort = "NEWTC2 ASC";
           
            //stores original dataview table for later 
            dt_view = dv.ToTable(false, colnames);

            //clone data structures for storing calculations
            calculated = dv.ToTable(false, colnames).Clone();
            newtotals = dv.ToTable(false, colnames).Clone();

            //calculate all tsar data
            CalculateData();

            // Set cursor as default arrow
            Cursor.Current = Cursors.Default;
        }

        //gets column names
        private string[] GetColumnsNames()
        {
            string sdate2 = dt.Rows[0]["SDATE"].ToString().Substring(4, 2);

            int month = Convert.ToInt32(sdate2);
            
            int startColumn = 0;

            for (int i = month; month > 0; month--)
            {
                startColumn++;
            }

            //use builder to identify relevant columns for 2tc level
            var builder = new StringCollection();
          
            builder.Add("NEWTC");

            for (int i = 0; i < 60; i++)
            {
                builder.Add("T" + startColumn.ToString() + "TOT");

                startColumn++;
            }

            //create array of column names to be used in datasource
            colnames = new string[builder.Count];
            builder.CopyTo(colnames, 0);

            return colnames;
        }

        //set up datagrid headers
        private void GetColumnHeaders()
        {
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 270;
            dgData.Columns[0].Frozen = true;
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //start after the newtc columns
            int startcol = 1;

            // loop through 5 years to name the columns header text
            for (int k = 0; k < 5; k++)
            {
                int[] yr = new int[5];
                yr[k] = Convert.ToInt32(year1) - k;

                //loop through 12 months a year
                int j = 12;

                //loop through the next twelve columns
                for (int i = startcol; i <= startcol + 11; i++)
                {
                    dgData.Columns[i].Width = 70;
                    gridDates[i-1] = yr[k] + j.ToString("00");
                    dgData.Columns[i].HeaderText = yr[k] + j.ToString("00");
                    dgData.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    j--;
                }

                //increase the next set of columns to loop through
                startcol += 12;

                //make column unsortable
                foreach (DataGridViewColumn dgvc in dgData.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        //do all calcutions
        private void CalculateData()
        {
            decimal saf;
            decimal boost;
            string newtc;
            string newtc2;
            decimal lsfval;

            //array to hold values for TOTAL row
            decimal[] tot_value = new decimal[colnames.Count() - 1];

            //loop through columns and calculate factors/values
            foreach (string column_name in colnames)
            {
                if ((column_name != "NEWTC"))
                {
                    newtotals.Columns[column_name].DataType = typeof(decimal);
                }
            }

            //all 2 digit tcs we want to display on the screens
            //regardless of if they have values or not
            string[] tcList = new string[]{"00", "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11",
                               "12", "13", "14", "15", "16", "19", "20", "21", "22", "23", "24", "25",
                               "26", "27", "28", "29", "30", "31", "32", "33", "34", "35", "36", "37",
                               "38", "39"};

            //loop through 2 digit tcs and summarize their totals
            foreach (string tcitem in tcList)
            {
                string tcitem2;

                if (Convert.ToInt32(tcitem) >= 20)
                {
                    tcitem2 = "1T";
                }
                else
                {
                    tcitem2 = tcitem;
                }

                GetBST(owner, tcitem2);

                if (rdseasadj.Checked)
                {
                    GetSAF(owner, tcitem2);
                }

                //array to store cumulative totals for each column
                col_value = new decimal[colnames.Count() - 1];

                foreach (DataRow row in dt_view.Rows)
                {
                    //column counter
                    int count = 0;

                    newtc = row["NEWTC"].ToString();
                    newtc2 = row["NEWTC"].ToString().Substring(0, 2);

                    //if newtc2 equals to tc in description list
                    if (newtc2 == tcitem)
                    {
                        GetLSF(owner, tcitem2);

                        if (rdmulti.Checked)
                        {
                            boost = 1.0m;
                        }

                        var newrow = newtotals.NewRow();

                        newrow[0] = newtc;

                        //loop through columns and calculate factors/values
                        foreach (string column_name in colnames)
                        {
                            string date = dt.Rows[0]["SDATE"].ToString();
                            string year = dt.Rows[0]["SDATE"].ToString().Substring(0,4);
                            string prior = DateTime.Now.AddYears(-1).ToString("yyyy");
                            string prior5 = DateTime.Now.AddYears(-5).ToString("yyyy");
                            
                            if ((column_name != "NEWTC"))
                            {
                                //convert data columns from int to decimal for calculations 
                                calculated.Columns[column_name].DataType = typeof(decimal);

                                decimal weighted_value = Convert.ToDecimal(row[column_name]);
                                lsfval = lsflist[count + 3];

                                if (rdmulti.Checked)
                                {
                                    boost = 1.0m;
                                }
                                else
                                {
                                    boost = bstlist[count];
                                }

                                decimal calculated_value;

                                if (rdseasadj.Checked)
                                {
                                    saf = saflist[count];
                                    calculated_value = (weighted_value * boost * lsfval * ucf * 12) / (saf * divisor);
                                }
                                else
                                {
                                    calculated_value = weighted_value * boost * lsfval * ucf / divisor;                                   
                                }

                                //save cell as calculated value
                                row[column_name] = calculated_value;
                                newrow[column_name] = calculated_value;

                                //save accumulated values for total columns
                                col_value[count] += calculated_value;

                                //checks to see if btnExport is enabled
                                if (btnExport.Enabled)
                                {
                                    string owner_exp = owner;

                                    if (owner == "P")
                                    {
                                        owner_exp = "S";
                                    }

                                    ExportData(year, owner_exp, newtc, gridDates[count], calculated_value);                                   
                                }

                                count++;
                            }
                        }

                        newtotals.Rows.Add(newrow);
                    }
                }

                var drow = calculated.NewRow();

                List<string> tcdesc = data_object.GetTCDescription(tcitem, owner.ToString().Substring(0, 1));

                string star;
                if (tcdesc[1] == "N")
                {
                    star = "*";
                }
                else
                {
                    star = "";
                }

                drow[0] = star + tcitem + ' ' + tcdesc[0];

                int colnum = 1;

                //loop through array of accumulated values by newtc
                for (int i = 0; i < col_value.Length; i++)
                {
                    drow[colnum++] = Math.Round(col_value[i], 1, MidpointRounding.AwayFromZero);
                    decimal total_value = Math.Round(col_value[i], 1, MidpointRounding.AwayFromZero);
                    tot_value[i] += total_value;
                }

                calculated.Rows.Add(drow);
            }

            //calculate total row for all newtc2s by owner
            var drowt = calculated.NewRow();

            drowt[0] = "     Total";

            int colnumt = 1;

            for (int i = 0; i < tot_value.Length; i++)
            {
                drowt[colnumt++] = tot_value[i];
            }

            calculated.Rows.InsertAt(drowt, 0);

            dgData.DataSource = calculated;

        }

        private void GetLSF(string owner, string newtc)
        {
            DataTable lsf;

            if (old_factors == true)
            {
                lsf = data_object.GetLSFTabData(owner, newtc);
            }
            else
            {
                lsf = data_object.GetLSFAnnData(owner, newtc);
            }

            List<decimal> lsfarray = new List<decimal>();

            /* this determines lsf value for row in datagrid - stores in list -> array*/
            foreach (DataRow dr in lsf.Rows)
            {
                int lsfnum = int.Parse(dr["LSFNO"].ToString());
                lsfarray.Add(decimal.Parse(dr["LSF"].ToString()));
            }
            for (int i = 24; i <= 76; i++)
            {
                decimal one = 1.00m;
                lsfarray.Add(one);
            }

            lsflist = lsfarray.ToArray();
        }

        private void GetBST(string owner, string tcitem2)
        {
            DataTable bst;

            if (old_factors == true)
            {
                bst = data_object.GetBstTabData(owner, year5, tcitem2, year1);
            }
            else
            {
                bst = data_object.GetBstAnnData(owner, year5, tcitem2, year1);
            }

            List<decimal> bstarray = new List<decimal>();

            // get boost factor for matching date
            foreach (DataRow dr in bst.Rows)
            {
              //  decimal boost = decimal.Parse(dr["BST"].ToString());
                bstarray.Add(decimal.Parse(dr["BST"].ToString()));         
            }

            bstlist = bstarray.ToArray();
        }

        private void GetSAF(string owner, string tcitem2)
        {
            DataTable saf = data_object.GetSafTabData(owner, year5, tcitem2, year1);

            List<decimal> safarray = new List<decimal>();

            // get saf factor for matching date
            foreach (DataRow dr in saf.Rows)
            {
                safarray.Add(decimal.Parse(dr["SAF"].ToString()));
            }

            saflist = safarray.ToArray();
        }

        //double click to reveal subtcs
        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!show_subtc && dgData.CurrentRow.Index != 0)
            {
                DataTable totalfiltered = new DataTable();

                //go to subtc 
                show_subtc = true;

                string subtc = dgData.CurrentRow.Cells[0].Value.ToString();
                DataRow total_row = ((DataRowView)dgData.SelectedRows[0].DataBoundItem).Row;
                string[] subtcstring = subtc.Split(' ');
                string tcfilter;

                if (subtcstring[0].ToString().Substring(0, 1) == "*")
                {
                    tcfilter = subtcstring[0].Substring(1, 2);
                }
                else
                {
                    tcfilter = subtcstring[0];
                }

                dv = newtotals.DefaultView;
                dv.RowFilter = "Substring(NEWTC, 1, 2) = " + tcfilter.AddSqlQuotes();
                dv.Sort = "NEWTC ASC";

                //copy of datatable with only necessary columns to be used
                totalfiltered = dv.ToTable(false, colnames).Copy();

                //copy of data prior to groupData3 for summarizing correctly
                DataTable originalfilter = new DataTable();

                originalfilter = dv.ToTable(false, colnames).Copy();

                DataColumn coldesc = new DataColumn();
                coldesc.ColumnName = "coldesc";
                originalfilter.Columns.Add(coldesc);

                DataColumn cold = new DataColumn();
                cold.ColumnName = "coldesc";
                totalfiltered.Columns.Add(cold);

                var groupedData3 = originalfilter.AsEnumerable()
                                 .Where(n => n["newtc"].ToString().Substring(0, 2) == tcfilter)

                                 .GroupBy(r => r["newtc"].ToString().Substring(0, 3))

                                 .Select(g =>
                                 {
                                     var row = totalfiltered.NewRow();

                                     row["newtc"] = g.Key;

                                     foreach (string column in colnames)
                                     {
                                         if (column.ToString().Substring(0, 5) != "NEWTC")
                                         {
                                             row[column] = g.Sum(r => Convert.ToDecimal(r[column]));
                                         }
                                     }

                                     List<string> tcdesc = data_object.GetTCDescription(row["newtc"].ToString(), owner.ToString().Substring(0, 1));
                                     string star;
                                     if (tcdesc[1] == "N")
                                     {
                                         star = "*";
                                     }
                                     else
                                     {
                                         star = "";
                                     }
                                     row["coldesc"] = Indent(3) + star + row["newtc"].ToString() + " " + tcdesc[0];

                                     return row;
                                 });
                totalfiltered.ImportRow(total_row);

                foreach (DataRow row in totalfiltered.Rows)
                {
                    List<string> tcdesc;
                    tcdesc = data_object.GetTCDescription(row[0].ToString(), owner);
                    string[] subtcs = row[0].ToString().Split(' ');
                    string star;

                    if (subtcs.Count() <= 1)
                    {
                        if (tcdesc.Count != 0)
                        {
                            if (tcdesc[1] == "N")
                            {
                                star = "*";
                            }
                            else
                            {
                                star = "";
                            }
                            row["coldesc"] = Indent(6) + star + row["newtc"].ToString() + " " + tcdesc[0];
                        }
                    }
                    else
                    {
                        row["coldesc"] = row["newtc"];
                    }
                }
        
                foreach (var row3 in groupedData3)
                {
                    totalfiltered.Rows.Add(row3);
                }

                var totals = totalfiltered.AsEnumerable()
                    .OrderBy(r => r["newtc"].ToString().Trim()).CopyToDataTable();

                foreach (DataRow dr in totals.Rows)
                {
                    dr["newtc"] = dr["coldesc"];
                }

                totals.Columns.Remove("coldesc");

                dgData.DataSource = totals;

                lbllabel.Text = "Double Click any Line to Restore";

            }
            else if (show_subtc)
            {
                //go back to main tc 
                show_subtc = false;

                dgData.DataSource = calculated;

                lbllabel.Text = "Double Click a Line to Expand";
            }
        }

        private string Indent(int count)
        {
            return "".PadLeft(count);
        }

        private void rdtype_CheckedChanged(object sender, EventArgs e)
        {
            if (rdseasadj.Checked)
            {
                divisor = 1000000;
                money = "Billions of Dollars";
                lblMoney.Text = money;
                type = " VIP SEASONALLY ADJUSTED AND BOOSTED";
                dgData.DefaultCellStyle.Format = "#########0.0";
                formatint = false;
                //grey out ANNUAL button
                seasonal = true;
                btnView.Enabled = false;
            }
            else
            {
                divisor = 1000;
                money = "Millions of Dollars";
                lblMoney.Text = money;
                type = " VIP NOT SEASONALLY ADJUSTED AND BOOSTED";
                formatint = true;
                dgData.DefaultCellStyle.Format = "0";
                seasonal = false;
                btnView.Enabled = true;
            }

            //checks to see if btnExport is enabled
            btnExportEnabled();

            if (!formloading)
            {
                FilterData();
            }
            
        }

        //determine if btnExport is enabled
        private void btnExportEnabled()
        {
            //if month is June then export is enabled
            string CurrMonth = DateTime.Now.Month.ToString("0#");

            //determines when to enable btnExport
            if (!old_factors) //if new factors
            {
                if (seasonal)
                {
                    btnExport.Enabled = false;
                }
                else if ((CurrMonth == "06") || (CurrMonth == "07") || (CurrMonth == "08"))
                {
                    btnExport.Enabled = true;
                }
                else
                {
                    btnExport.Enabled = false;
                }
            }
            else
            {
                btnExport.Enabled = false;
            }
        }

        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            if (dgData.Columns[e.ColumnIndex].Name.Equals("NEWTC"))
            {
                String stringValue = e.Value.ToString();
                if (stringValue == null || stringValue.Trim() == "") return;

                if (stringValue.Substring(0, 1) == "*")
                {
                    string next = stringValue.Substring(1, 4);
                    string justNumbers = new String(next.Where(Char.IsDigit).ToArray());

                    switch (justNumbers.Length)
                    {
                        case 3:
                            e.Value = "    " + stringValue;
                            break;
                        case 4:
                            e.Value = "         " + stringValue;
                            break;
                        default:
                            e.Value = stringValue;
                            break;
                    }

                }
                else
                {
                    e.Value = stringValue;
                }
            }
  
            if ((e.RowIndex >= 0) && (formatint == true))
            {
                if (e.ColumnIndex != 0)
                {
                    decimal d = decimal.Parse(e.Value.ToString());
                    e.Value = Math.Round(d, 1, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void rdsurvey_CheckedChanged(object sender, EventArgs e)
        {
            if (rdsl.Checked)
            {
                survey = "STATE AND LOCAL ";
                owner = "P";
                ucf = 1.00m;                
            }
            else if (rdfed.Checked)
            {
                survey = "FEDERAL ";
                owner = "F";
                ucf = 1.01m;                               
            }
            else if (rdnonres.Checked)
            {
                survey = "NONRESIDENTIAL ";
                owner = "N";
                ucf = 1.25m;
            }
            else if (rdmulti.Checked)
            {
                survey = "MULTIFAMILY ";
                owner = "M";
                ucf = 1.00m;
            }

            if (!formloading)
            {
                FilterData();
            }
        }
        DataTable export = new DataTable();

        //set up datatable to hold calculated nsa values  
        private void CreateExport()
        {
            export.Columns.Add("year");
            export.Columns.Add("owner");
            export.Columns.Add("newtc");
            export.Columns.Add("sdate");
            export.Columns.Add("uvipdata");

            DataRow row = export.NewRow();
        }

        //export data to tsar rows
        private void ExportData(string year, string owner, string newtc, string sdate, decimal uvipdata)
        {
            decimal vipdata;
            vipdata = Math.Round(uvipdata, 1, MidpointRounding.AwayFromZero);
            export.Rows.Add(year, owner, newtc, sdate, vipdata);
        }

        //export to tsar event
        private void btnExport_Click(object sender, EventArgs e)
        {
            string owner_exp = owner;

            if (owner == "P")
            {
                owner_exp = "S";
            }

            //activate hourglass 
            Cursor.Current = Cursors.WaitCursor;

            //determine year to delete
            string year = DateTime.Now.ToString("yyyy");

            //if owner year exist then delete
            if (data_object.CheckMonthExists(owner_exp, year))
            {
                data_object.DeleteRow(owner_exp, year);
            }

            //insert export to datatable
            if (data_object.InsertAnnExport(export))
            {
                MessageBox.Show("Data has been Exported");
            }

            Cursor.Current = Cursors.Default;
        }


        private void btnApply_Click(object sender, EventArgs e)
        {
            if (old_factors == true)
            {
                lblFactor.Text = "New Factors";
                btnApply.Text = "APPLY OLD FACTORS";
                old_factors = false;
            }
            else
            {
                lblFactor.Text = "Old Factors";
                btnApply.Text = "APPLY NEW FACTORS";
                old_factors = true;
            }

            btnExportEnabled();

            lblFactor.Refresh();

            if (!formloading)
            {
                calculated.Clear();

                FilterData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            string yearPrint = string.Empty;

            frmTabAnnualPrintPopup fp = new frmTabAnnualPrintPopup();
            fp.StartPosition = FormStartPosition.CenterParent;

            if (fp.ShowDialog() == DialogResult.OK)
            {
                yearPrint = fp.yearPrint;

                if (yearPrint == "All Five Years")
                {
                    PrintData(year1);
                    PrintData(year2);
                    PrintData(year3);
                    PrintData(year4);
                    PrintData(year5);
                }
                else
                {
                    PrintData(yearPrint);
                }
            }
            else if (fp.ShowDialog() == DialogResult.Cancel)
            {
                this.btnPrint.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            }

            Cursor.Current = Cursors.Default;
        }

        //Print datagrids based on pop-up selection
        private void PrintData(string selyear)
        {
            Cursor.Current = Cursors.WaitCursor;
           
            DGVPrinter printer = new DGVPrinter();
            printer = new DGVPrinter();

            printer.Title = lblTitle.Text + "    " + lblFactor.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.SubTitle = lblMoney.Text;
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Annual Tabs";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            Margins margins = new Margins(30, 30, 30, 30);
            printer.PrintMargins = margins;

            //dgData.DataSource = calculated;

            //loop through columns and determine visibility
            for (int i = 0; i < colnames.Count(); i++)
            {
                dgData.Columns[i].Visible = false;
                dgData.Columns[0].Visible = true;

                if (selyear == year1.ToString())
                {
                    if ((i >= 1) && (i <= 12))
                    {
                        dgData.Columns[i].Visible = true;
                    }
                }
                else if (selyear == year2.ToString())
                {
                    if ((i >= 13) && (i <= 24))
                    {
                        dgData.Columns[i].Visible = true;
                    }
                }
                else if (selyear == year3.ToString())
                {
                    if ((i >= 25) && (i <= 36))
                    {
                        dgData.Columns[i].Visible = true;
                    }
                }
                else if (selyear == year4.ToString())
                {
                    if ((i >= 37) && (i <= 48))
                    {
                        dgData.Columns[i].Visible = true;
                    }
                }
                else if (selyear == year5.ToString())
                {
                    if ((i >= 49) && (i <= 60))
                    {
                        dgData.Columns[i].Visible = true;
                    }
                }
            }

            dgData.Columns[0].Width = 170;
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 270;
        }

        private DataTable oldtcs;
        private DataTable newtcs;
        private DataTable oldcalculated;
        private DataTable newcalculated;

        //calculate totals to pass to tabannualsview screen
        private void CalculateTotals()
        {
            old_factors = true;

            FilterData();

            oldcalculated = calculated.Copy();
            oldtcs = newtotals.Copy();

            old_factors = false;

            FilterData();

            newcalculated = calculated.Copy();
            newtcs = newtotals.Copy();
        }


        //view totals on new form
        private void btnView_Click(object sender, EventArgs e)
        {
            // MessageBox.Show("This functionality is currently not available.");

            this.Hide();

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");

            frmTabAnnualView popup = new frmTabAnnualView();
            popup.CallingForm = this;

            CalculateTotals();

            popup.survey = survey;
            popup.year1 = year1;
            popup.oldcalculated = oldcalculated;
            popup.newcalculated = newcalculated;
            popup.oldtcs = oldtcs;
            popup.newtcs = newtcs;
            popup.Show();
        }

        private void frmTabAnnualTabs_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

    }
}
