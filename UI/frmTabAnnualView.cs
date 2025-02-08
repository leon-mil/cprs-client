/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabAnnualView.cs
Programmer    : Diane Musachio
Creation Date : June 21, 2019
Parameters    : survey, year, oldcalculated, newcalculated, oldtc, newtc
Inputs        : N/A
Outputs       : N/A
Description   : create screen to display annual tabulations
            non-seasonally adjusted yearly totals for all 5 previous yeas
                           
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
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmTabAnnualView : frmCprsParent
    {
        /****** public properties *******/
        /* Required */

        public Form CallingForm = null;
        public string survey;
        public string year1;

        //old and new factors broken down by 4 digit tc
        public DataTable oldtcs;
        public DataTable newtcs;

        //old and new factors totaled by 2 digit tc 
        public DataTable oldcalculated;
        public DataTable newcalculated;
      
        /*flag to use closing the calling form */
        private bool call_callingFrom = false;

        //------------------------------------

        private DataTable resultFactors;
        private DataTable oldFactors;
        private DataTable newFactors;
        private DataTable oldtcs_raw;
        private DataTable newtcs_raw;
        private DataTable result_raw;
        private bool show_subtc;

        TabAnnualTabsData data_object = new TabAnnualTabsData();

        public frmTabAnnualView()
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            InitializeComponent();
        }

        private void frmTabAnnualView_Load(object sender, EventArgs e)
        {
            string year5 = (Convert.ToInt16(year1) - 4).ToString();

            lblTitle.Text = survey + " " + year5  + "-" 
                + year1 + " VIP ANNUAL TOTALS";

            //true if four digit tc level
            show_subtc = false;

            //calculate all old and newfactor totals
            CalculateTotals();

            //setup the initial display grid for old and new factors at 2 digit summary level
            SetupDataGrid(resultFactors);
        }

        //calculate all old and newfactor totals
        private void CalculateTotals()
        {
            //get all totals for 2 digit summary
            oldFactors = gettotals(oldcalculated);
            newFactors = gettotals(newcalculated);
            resultFactors = getResults(oldFactors, newFactors);

            //get all tc totals by 3 and 4 digit level
            oldtcs_raw = gettotals(oldtcs);
            newtcs_raw = gettotals(newtcs);           
            result_raw = getResults(oldtcs_raw, newtcs_raw);
        }

        //set up datagrid properties
        private void SetupDataGrid(DataTable resultname)
        {
            string year5 = (Convert.ToInt16(year1) - 4).ToString();
            string year4 = (Convert.ToInt16(year1) - 3).ToString();
            string year3 = (Convert.ToInt16(year1) - 2).ToString();
            string year2 = (Convert.ToInt16(year1) - 1).ToString();

            dgData.DataSource = resultname;

            if (dgData.Rows.Count > 23)
            {
                dgData.Columns[0].Width = 250;
                dgData.ScrollBars = ScrollBars.Vertical;
            }
            else
            {
                dgData.Columns[0].Width = 270;
                dgData.ScrollBars = ScrollBars.None;
            }
          
            dgData.AutoGenerateColumns = false;
            dgData.RowTemplate.Height = 22;
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[0].DataPropertyName = resultname.Columns["newtc"].ColumnName;
            dgData.Columns[1].HeaderText = "Old Factors " + year1;
            dgData.Columns[1].Width = 57;
            dgData.Columns[1].DataPropertyName = resultname.Columns["oldyr1"].ColumnName;
            dgData.Columns[2].HeaderText = "New Factors " + year1;
            dgData.Columns[2].Width = 57;
            dgData.Columns[2].DataPropertyName = resultname.Columns["newyr1"].ColumnName;
            dgData.Columns[3].HeaderText = "% Change " + year1;
            dgData.Columns[3].Width = 50;
            dgData.Columns[3].DefaultCellStyle.Format = "D2";
            dgData.Columns[3].DataPropertyName = resultname.Columns["yr1diff"].ColumnName;
            dgData.Columns[4].HeaderText = "Old Factors " + year2;
            dgData.Columns[4].Width = 57;
            dgData.Columns[4].DataPropertyName = resultname.Columns["oldyr2"].ColumnName;
            dgData.Columns[5].HeaderText = "New Factors " + year2;
            dgData.Columns[5].Width = 57;
            dgData.Columns[5].DataPropertyName = resultname.Columns["newyr2"].ColumnName;
            dgData.Columns[6].HeaderText = "% Change " + year2;
            dgData.Columns[6].Width = 50;
            dgData.Columns[6].DefaultCellStyle.Format = "D2";
            dgData.Columns[6].DataPropertyName = resultname.Columns["yr2diff"].ColumnName;
            dgData.Columns[7].HeaderText = "Old Factors " + year3;
            dgData.Columns[7].Width = 57;
            dgData.Columns[7].DataPropertyName = resultname.Columns["oldyr3"].ColumnName;
            dgData.Columns[8].HeaderText = "New Factors " + year3;
            dgData.Columns[8].Width = 57;
            dgData.Columns[8].DataPropertyName = resultname.Columns["newyr3"].ColumnName;
            dgData.Columns[9].HeaderText = "% Change " + year3;
            dgData.Columns[9].Width = 50;
            dgData.Columns[9].DefaultCellStyle.Format = "D2";
            dgData.Columns[9].DataPropertyName = resultname.Columns["yr3diff"].ColumnName;
            dgData.Columns[10].HeaderText = "Old Factors " + year4;
            dgData.Columns[10].Width = 57;
            dgData.Columns[10].DataPropertyName = resultname.Columns["oldyr4"].ColumnName;
            dgData.Columns[11].HeaderText = "New Factors " + year4;
            dgData.Columns[11].Width = 57;
            dgData.Columns[11].DataPropertyName = resultname.Columns["newyr4"].ColumnName;
            dgData.Columns[12].HeaderText = "% Change " + year4;
            dgData.Columns[12].Width = 50;
            dgData.Columns[12].DefaultCellStyle.Format = "D2";
            dgData.Columns[12].DataPropertyName = resultname.Columns["yr4diff"].ColumnName;
            dgData.Columns[13].HeaderText = "Old Factors " + year5;
            dgData.Columns[13].Width = 57;
            dgData.Columns[13].DataPropertyName = resultname.Columns["oldyr5"].ColumnName;
            dgData.Columns[14].HeaderText = "New Factors " + year5;
            dgData.Columns[14].Width = 57;
            dgData.Columns[14].DataPropertyName = resultname.Columns["newyr5"].ColumnName;
            dgData.Columns[15].HeaderText = "% Change " + year5;
            dgData.Columns[15].Width = 50;
            dgData.Columns[15].DefaultCellStyle.Format = "D2";
            dgData.Columns[15].DataPropertyName = resultname.Columns["yr5diff"].ColumnName;

            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                //make column unsortable
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //format cells in datagrid to represent 2 digit percent for each year 
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
            if ((e.RowIndex >= 0))
            {
                if (((e.ColumnIndex == 3) || (e.ColumnIndex == 6) || (e.ColumnIndex == 9)
                    || (e.ColumnIndex == 12) || (e.ColumnIndex == 15)) && (e.Value != null))
                {
                    double d = double.Parse(e.Value.ToString());
                    e.Value = d.ToString("N2");
                }
            }
        }

        //create new table for storing totals by year
        private DataTable getnewDatatable()
        {
            DataTable dt1;

            dt1 = new DataTable();
            dt1.Columns.Add("NEWTC");
            dt1.Columns.Add("YEAR1");
            dt1.Columns.Add("YEAR2");
            dt1.Columns.Add("YEAR3");
            dt1.Columns.Add("YEAR4");
            dt1.Columns.Add("YEAR5");

            return dt1;
        }

        //calculate totals to store in datatable
        private DataTable gettotals(DataTable dt)
        {
            DataTable dt1;
            dt1 = getnewDatatable();

            DataRow rowold;

            foreach (DataRow calcrow in dt.Rows)
            {
                rowold = dt1.NewRow();

                rowold["NEWTC"] = calcrow["NEWTC"];

                decimal yrtot = 0;

                // sum 12 month totals for each of 5 years
                for (int month = 1; month <= 12; month++)
                {
                    yrtot += Convert.ToDecimal(calcrow[month]);
                }

                rowold["YEAR1"] = yrtot;
                yrtot = 0;

                for (int month = 13; month <= 24; month++)
                {
                    yrtot += Convert.ToDecimal(calcrow[month]);
                }

                rowold["YEAR2"] = yrtot;
                yrtot = 0;

                for (int month = 25; month <= 36; month++)
                {
                    yrtot += Convert.ToDecimal(calcrow[month]);
                }

                rowold["YEAR3"] = yrtot;
                yrtot = 0;

                for (int month = 37; month <= 48; month++)
                {
                    yrtot += Convert.ToDecimal(calcrow[month]);
                }

                rowold["YEAR4"] = yrtot;
                yrtot = 0;

                for (int month = 49; month <= 60; month++)
                {
                    yrtot += Convert.ToDecimal(calcrow[month]);
                }

                rowold["YEAR5"] = yrtot;

                dt1.Rows.Add(rowold);

            }

            return dt1;
        }

        //create table structure for datagrid display
        private DataTable GetNewResults()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("newtc");
            dt.Columns.Add("oldyr1");
            dt.Columns.Add("newyr1");
            dt.Columns.Add("yr1diff");
            dt.Columns.Add("oldyr2");
            dt.Columns.Add("newyr2");
            dt.Columns.Add("yr2diff");
            dt.Columns.Add("oldyr3");
            dt.Columns.Add("newyr3");
            dt.Columns.Add("yr3diff");
            dt.Columns.Add("oldyr4");
            dt.Columns.Add("newyr4");
            dt.Columns.Add("yr4diff");
            dt.Columns.Add("oldyr5");
            dt.Columns.Add("newyr5");
            dt.Columns.Add("yr5diff");

            return dt;
        }

        //place old factor and new factor information in one table
        private DataTable getResults(DataTable oldval, DataTable newval)
        {
            DataTable dt = GetNewResults();

            DataRow row;

            for (int i = 0; i < oldval.Rows.Count; i++)
            {
                row = dt.NewRow();

                decimal oldyr1 = Math.Round(Convert.ToDecimal(oldval.Rows[i]["YEAR1"]));
                decimal oldyr2 = Math.Round(Convert.ToDecimal(oldval.Rows[i]["YEAR2"]));
                decimal oldyr3 = Math.Round(Convert.ToDecimal(oldval.Rows[i]["YEAR3"]));
                decimal oldyr4 = Math.Round(Convert.ToDecimal(oldval.Rows[i]["YEAR4"]));
                decimal oldyr5 = Math.Round(Convert.ToDecimal(oldval.Rows[i]["YEAR5"]));
                decimal newyr1 = Math.Round(Convert.ToDecimal(newval.Rows[i]["YEAR1"]));
                decimal newyr2 = Math.Round(Convert.ToDecimal(newval.Rows[i]["YEAR2"]));
                decimal newyr3 = Math.Round(Convert.ToDecimal(newval.Rows[i]["YEAR3"]));
                decimal newyr4 = Math.Round(Convert.ToDecimal(newval.Rows[i]["YEAR4"]));
                decimal newyr5 = Math.Round(Convert.ToDecimal(newval.Rows[i]["YEAR5"]));

                row["newtc"] = oldval.Rows[i]["NEWTC"];
                row["oldyr1"] = oldyr1;
                row["oldyr2"] = oldyr2;
                row["oldyr3"] = oldyr3;
                row["oldyr4"] = oldyr4;
                row["oldyr5"] = oldyr5;
                row["newyr1"] = newyr1;
                row["newyr2"] = newyr2;
                row["newyr3"] = newyr3;
                row["newyr4"] = newyr4;
                row["newyr5"] = newyr5;

                if (oldyr1 != 0)
                {
                    string diff = (((newyr1 - oldyr1) / oldyr1) * 100).ToString("0.00");
                    row["yr1diff"] = diff;
                }
                else row["yr1diff"] = 0.ToString("0.00");

                if (oldyr2 != 0)
                {
                    row["yr2diff"] = (((newyr2 - oldyr2) / oldyr2) * 100).ToString("0.00");
                }
                else row["yr2diff"] = 0.ToString("0.00");
                if (oldyr3 != 0)
                {
                    row["yr3diff"] = (((newyr3 - oldyr3) / oldyr3) * 100).ToString("0.00");
                }
                else row["yr3diff"] = 0.ToString("0.00");

                if (oldyr4 != 0)
                {
                    row["yr4diff"] = (((newyr4 - oldyr4) / oldyr4) * 100).ToString("0.00");
                }
                else row["yr4diff"] = 0.ToString("0.00");

                if (oldyr5 != 0)
                {
                    row["yr5diff"] = (((newyr5 - oldyr5) / oldyr5) * 100).ToString("0.00");
                }
                else row["yr5diff"] = 0.ToString("0.00");

                dt.Rows.Add(row);
            }

            return dt;
        }

        //double click to reveal subtcs
        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!show_subtc && dgData.CurrentRow.Index != 0)
            {
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

                DataView dv = new DataView();
                dv = result_raw.DefaultView;

                dv.RowFilter = "Substring(NEWTC, 1, 2) = " + tcfilter.AddSqlQuotes();
                dv.Sort = "NEWTC ASC";

                DataTable totals_raw = new DataTable();
                DataTable totals = new DataTable();

                totals_raw = dv.ToTable().Copy();
                totals = dv.ToTable().Copy();

                string owner;

                if (survey.ToString().Substring(0, 1) == "S")
                {
                    owner = "P";
                }
                else
                {
                    owner = survey.ToString().Substring(0, 1);
                }

                DataColumn coldesc = new DataColumn();
                coldesc.ColumnName = "coldesc";
                totals_raw.Columns.Add(coldesc);


                DataColumn cold = new DataColumn();
                cold.ColumnName = "coldesc";
                totals.Columns.Add(cold);

                var group3 = totals_raw.AsEnumerable()
                   .Where(n => n["newtc"].ToString().Substring(0, 2) == tcfilter)
                   .GroupBy(r => r["newtc"].ToString().Substring(0, 3))
                   .Select(g =>
                   {
                       var row = totals.NewRow();
                       row["newtc"] = g.Key;

                       foreach (DataColumn column in totals_raw.Columns)
                       {
                           if ((column.ColumnName != "newtc") && (column.ColumnName != "coldesc"))
                           {
                               row[column.ColumnName] = g.Sum(r => Convert.ToDecimal(r[column]));
                           }
                       }

                       List<string> tcdesc;
                       tcdesc = data_object.GetTCDescription(row[0].ToString(), owner);
                       string star;
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
                           row["coldesc"] = Indent(3) + star + row["newtc"].ToString() + " " + tcdesc[0];
                       }
                       return row;
                   });

                totals.ImportRow(total_row);
                foreach (DataRow row in totals.Rows)
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

                foreach (var row3 in group3)
                {
                    totals.Rows.Add(row3);
                }
                
                var total_all = totals.AsEnumerable()
                    .OrderBy(r => r["newtc"].ToString().Trim()).CopyToDataTable();

                foreach (DataRow dr in total_all.Rows)
                {
                    dr["newtc"] = dr["coldesc"];
                }

                total_all.Columns.Remove("coldesc");

                SetupDataGrid(total_all);

                lbllabel.Text = "Double Click any Line to Restore";

            }
            else if (show_subtc)
            {
                //go back to main tc 
                show_subtc = false;

                SetupDataGrid(resultFactors);

                lbllabel.Text = "Double Click a Line to Expand";
            }
        }

        private string Indent(int count)
        {
            return "".PadLeft(count);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }

            this.Close();
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
            Margins margins = new Margins(0, 30, 0, 30);

            //loop through columns determine visibility
            for (int i = 0; i < 16; i++)
            {
                dgPrint.Columns[i].Visible = true;
            }
            for (int j = 16; j < dgPrint.Columns.Count; j ++)
            {
                dgPrint.Columns[j].Visible = false;
            }

            printer.PrintMargins = margins;
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Annual View Table";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            dgPrint.Columns[0].Width = 230;
            //resize column
            printer.PrintDataGridViewWithoutDialog(dgPrint);
           

            Cursor.Current = Cursors.Default;
        }

        private void frmTabAnnualView_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
