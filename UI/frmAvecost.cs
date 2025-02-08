/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmAvecost.cs

 Programmer    : Diane Musachio

 Creation Date : 03/8/17

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display data for Average Cost of Multifamily project

 Detail Design : Detailed User Requirements for Average Cost Multifamily

 Other         : Called by: menu -> Tabulations -> monthly -> select survey:
                 multifamily -> select a row -> worksheet button -> average cost button
 
 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Globalization;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Collections;
using System.Windows.Forms.VisualStyles;
using System.IO;
using CprsDAL;
using CprsBLL;

namespace Cprs
{
    public partial class frmAvecost : frmCprsParent
    {
        public Form CallingForm = null;

        private bool call_callingFrom = false;

        private string sdate;
        private string region = "0";
        private string division = "0";
        private string state = "00";
        private string sdateSel;
        private string regiontext = "US";
        private string divisiontext = "US";
        private string statetext = "US";
        private string filter;
        private int curmonth = 0;
        private string priorYear;
        private string currentYear;
        private string currmonth = string.Empty;

        private bool formloading = false;

        private AveCostData dataObject;

        public frmAvecost()
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            InitializeComponent();
        }

        private void frmAvecost_Load(object sender, EventArgs e)
        {

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            PopulateFipStateCombo();

            lblTitle.Text = "Multifamily  " + sdate + " Average Cost";

            cbRegion.SelectedIndex = 0;
            cbDiv.SelectedIndex = 0;
            cbState.SelectedIndex = 0;

            SetUpData(dgPriorYr);
            dgPriorYr.ClearSelection();
            SetUpData(dgCurrentYr);
            dgCurrentYr.ClearSelection();
        }

        //sets up column headers, aligns them and makes them not sortable
        private void SetUpData(DataGridView dataGridTable)
        {
            dataObject = new AveCostData();
            DataTable dtPriorYr = new DataTable();
            DataTable dtCurrentYr = new DataTable();

            ////Convert sdate to datetime
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            currentYear = (dt.ToString("yyyy", CultureInfo.InvariantCulture));
            currmonth = (dt.ToString("MM", CultureInfo.InvariantCulture));
            curmonth = Convert.ToInt16(currmonth);

            priorYear = (dt.AddYears(-1)).ToString("yyyy", CultureInfo.InvariantCulture);
            string[] priormonth = new string[12];
            string[] currentmonth = new string[12];

            if (dataGridTable.Name == "dgPriorYr")
            {
                if (state != "00")
                {
                    dgPriorYr.DataSource = dataObject.GetAveCostState(priorYear, state);
                }
                else
                {
                    dgPriorYr.DataSource = dataObject.GetAveCost(priorYear, region, division, state);
                }

                DataTable priordata = dgPriorYr.DataSource as DataTable;

                if (priordata != null)
                {
                    int endnum = 12;

                    for (int i = 0; i < endnum; i++)
                    {
                        for (int j = 1; j <= 12; j++)
                        {
                            priormonth[i] = (dt.AddYears(-1)).AddMonths(-curmonth + j).ToString("yyyyMM", CultureInfo.InvariantCulture);

                            DataRow[] foundmonth = priordata.Select("STRTDATE = '" + priormonth[i] + "'");

                            //if no data then insert row with zeros
                            if (foundmonth.Length == 0)
                            {
                                priordata.Rows.Add(priormonth[i], "0", "0", "0");
                            }
                        }
                    }
                }
                else
                {
                    //set up data for number of months in current year
                    for (int i = 0; i < 12; i++)
                    {
                        priormonth[i] = (dt.AddYears(-1)).AddMonths(i).ToString("yyyyMM", CultureInfo.InvariantCulture);

                        //if no data then insert row with zeros
                        priordata.Rows.Add(priormonth[i], "0", "0", "0");
                    }
                }

                DataView dv = priordata.DefaultView;
                dv.Sort = "strtdate";
                DataTable sortedMondata = dv.ToTable();
                dgPriorYr.DataSource = sortedMondata;
            }

            if (dataGridTable.Name == "dgCurrentYr")
            {
                if (state != "00")
                {
                    dgCurrentYr.DataSource = dataObject.GetAveCostState(currentYear, state);
                }
                else
                {
                    dgCurrentYr.DataSource = dataObject.GetAveCost(currentYear, region, division, state);
                }

                DataTable currentdata = dgCurrentYr.DataSource as DataTable;

                if (currentdata != null)
                {
                    for (int i = 0; i < curmonth; i++)
                    {
                        for (int j = 1; j <= curmonth; j++)
                        {
                            currentmonth[i] = dt.AddMonths(-curmonth + j).ToString("yyyyMM");

                            DataRow[] foundmonth = currentdata.Select("STRTDATE = '" + currentmonth[i] + "'");

                            //if no data insert rows with zeros
                            if (foundmonth.Length == 0)
                            {
                                currentdata.Rows.Add(currentmonth[i], "0", "0", "0");
                            }
                        }
                    }
                }
                else
                {
                    //set up for number of months in current year
                    for (int i = 0; i < curmonth; i++)
                    {
                        currentmonth[i] = dt.AddMonths(i).ToString("yyyyMM");

                        //if no data insert rows with zeros
                        currentdata.Rows.Add(currentmonth[i], "0", "0", "0");
                    }
                }

                DataView dvc = currentdata.DefaultView;
                dvc.Sort = "strtdate";
                DataTable sortedCurrdata = dvc.ToTable();
                dgCurrentYr.DataSource = sortedCurrdata;
            }

            dataGridTable.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridTable.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (dataGridTable.ColumnCount > 0)
            {
                dataGridTable.Columns[0].HeaderText = "Start Date     ";
                dataGridTable.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                dataGridTable.Columns[1].HeaderText = "# of Units Started";
                dataGridTable.Columns[1].DefaultCellStyle.Format = "N0";
                dataGridTable.Columns[1].Width = 190;

                dataGridTable.Columns[2].HeaderText = "Value of Units Started ($000)";
                dataGridTable.Columns[2].DefaultCellStyle.Format = "N0";
                dataGridTable.Columns[2].Width = 250;

                dataGridTable.Columns[3].HeaderText = "Average Cost Per Unit";
                dataGridTable.Columns[3].DefaultCellStyle.Format = "N0";
                dataGridTable.Columns[3].Width = 250;

                double sumUnits = 0;
                double sumValue = 0;
                double totalCPU = 0;

                for (int i = 0; i < dataGridTable.Rows.Count; i++)
                {
                    double dUnits = 0;
                    double.TryParse(dataGridTable.Rows[i].Cells[1].Value.ToString(), out dUnits);
                    sumUnits += dUnits;

                    double dValue = 0;
                    double.TryParse(dataGridTable.Rows[i].Cells[2].Value.ToString(), out dValue);
                    sumValue += dValue;
                }

                if ((sumValue != 0) && (sumUnits != 0))
                {
                    totalCPU = (sumValue / sumUnits) * 1000;
                }
                else
                {
                    totalCPU = 0;
                }

                if (dataGridTable.Name == "dgPriorYr")
                {
                    txtPYTtunits.Text = sumUnits.ToString("N0");
                    txtPYTtvalue.Text = sumValue.ToString("N0");
                    txtPYCpu.Text = totalCPU.ToString("N0");
                }
                else if (dataGridTable.Name == "dgCurrentYr")
                {
                    txtCYTtunits.Text = sumUnits.ToString("N0");
                    txtCYTtvalue.Text = sumValue.ToString("N0");
                    txtCYCpu.Text = totalCPU.ToString("N0");
                }

                foreach (DataGridViewColumn c in dataGridTable.Columns)
                {
                    c.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        private void PopulateFipStateCombo()
        {
            DataTable dt = GeneralDataFuctions.GetFipStateDataForCombo();
            dt.Rows[0].Delete();
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "00-US";
            dt.Rows.InsertAt(dr,0);
            cbState.DataSource = dt;
            cbState.ValueMember = "FIPSTATE";
            cbState.DisplayMember = "STATE1";
            cbState.SelectedIndex = 0;
        }

        //needed to modify so if an original combobox is on selected index 0 and 
        //one of the other comboboxes changes and user re-selects 0 by clicking on original combobox -
        //although selectedvalue doesn't really change the data needs to be refreshed and
        //other comboboxes need to be defaulted to selectedindex of 0
        private bool oneComboboxChanged = false;

        private void cbRegion_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((cbRegion.SelectedIndex > 0) || (oneComboboxChanged))
            {
                oneComboboxChanged = true;
                int selindex = cbRegion.SelectedIndex;
                region = cbRegion.Text.Substring(0, 1);
                regiontext = cbRegion.Text.Substring(3, cbRegion.Text.Length - 3);
                division = "0";
                state = "00";
                cbDiv.SelectedIndex = 0;
                cbState.SelectedIndex = 0;
                cbRegion.SelectedIndex = selindex;
            }
            else
            {
                region = "0";
                regiontext = "US";
            }

            if (!formloading)
            {
                SetUpData(dgPriorYr);
                SetUpData(dgCurrentYr);

                dgCurrentYr.ClearSelection();
                dgPriorYr.ClearSelection();
            }
        }

        private void cbDiv_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((cbDiv.SelectedIndex > 0) || (oneComboboxChanged))
            {
                oneComboboxChanged = true;
                int selindex = cbDiv.SelectedIndex;
                division = cbDiv.Text.Substring(0, 1);
                divisiontext = cbDiv.Text.Substring(3, cbDiv.Text.Length - 3);
                region = "0";
                state = "00";
                cbRegion.SelectedIndex = 0;
                cbState.SelectedIndex = 0;
                cbDiv.SelectedIndex = selindex;
            }
            else
            {
                division = "0";
                divisiontext = "US";
            }

            if (!formloading)
            {
                SetUpData(dgPriorYr);
                SetUpData(dgCurrentYr);

                dgCurrentYr.ClearSelection();
                dgPriorYr.ClearSelection();
            }
        }

        private void cbState_SelectedValueChanged(object sender, EventArgs e)
        {
            if ((cbState.SelectedIndex > 0) || (oneComboboxChanged))
            {
                oneComboboxChanged = true;
                int selindex = cbState.SelectedIndex;
                state = cbState.Text.Substring(0, 2);
                statetext = cbState.Text.Substring(3, 2);
                region = "0";
                division = "0";
                cbRegion.SelectedIndex = 0;
                cbDiv.SelectedIndex = 0;
                cbState.SelectedIndex = selindex;
            }
            else
            {
                state = "00";
                statetext = "US";
            }

            if (!formloading)
            {
                SetUpData(dgPriorYr);
                SetUpData(dgCurrentYr);

                dgCurrentYr.ClearSelection();
                dgPriorYr.ClearSelection();
            }
        }

        //can only choose one selection between dgPriorYr and dgCurrentYr
        //this code clears opposite dataset selection as needed
        private void dgPriorYr_SelectionChanged(object sender, EventArgs e)
        {
            if (dgPriorYr.SelectedRows.Count > 0)
            {
                sdateSel = dgPriorYr.SelectedRows[0].Cells[0].Value.ToString();
                dgCurrentYr.ClearSelection();
            }
        }

        //can only choose one selection between dgPriorYr and dgCurrentYr
        //this code clears opposite dataset selection as needed
        private void dgCurrentYr_SelectionChanged(object sender, EventArgs e)
        {
            if (dgCurrentYr.SelectedRows.Count > 0)
            {
                sdateSel = dgCurrentYr.SelectedRows[0].Cells[0].Value.ToString();
                dgPriorYr.ClearSelection();
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }

            this.Close();
        }

        private void frmAvecost_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void btnCases_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dataObject = new AveCostData();
            var filteredDataTable = new DataTable();

            filter = "";

            dt = dataObject.GetVipprojData(sdateSel);

            if (region != "0")
            {
                filter = " region = " + region;
            }
            else if (division != "0")
            {
                filter = " division = " + division;
            }
            else if (state != "00")
            {
                filter = " fipstate = " + state;
            }

            //use above criteria to filter datatable and store in data view
            DataView dv = new DataView(dt);
            dv.RowFilter = filter;

            if ((dgCurrentYr.SelectedRows.Count == 0) && (dgPriorYr.SelectedRows.Count == 0))
            {
                MessageBox.Show("Please Select a Start Date");
            }
            else if (dv.Count == 0)
            {
                MessageBox.Show("There are No Cases to Display");
            }
            else
            {
                frmAvecostCases fm = new frmAvecostCases();
                fm.CallingForm = this;
                fm.sdate = sdate;
                fm.sdateSel = sdateSel;
                fm.region = region;
                fm.regiontext = regiontext;
                fm.division = division;
                fm.divisiontext = divisiontext;
                fm.state = state;
                fm.statetext = statetext;
                fm.dv = dv;

                this.Hide();
                GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
                fm.ShowDialog();  // show child
                GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cbState.SelectedIndex = 0;
            cbRegion.SelectedIndex = 0;
            cbDiv.SelectedIndex = 0;

            dgPriorYr.ClearSelection();
            dgCurrentYr.ClearSelection();

            dgCurrentYr.Refresh();
            dgPriorYr.Refresh();
        }

        Bitmap memoryImage;

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

    }
}
