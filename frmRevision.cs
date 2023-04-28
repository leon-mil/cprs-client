/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmRevision.cs

 Programmer    : Diane Musachio

 Creation Date : 4/11/2017

 Inputs        : N/A

 Parameters    : Newtc, Survey
 
 Output        : N/A
  
 Description   : This program will compare current revisions with prior ones
                 for VIP Cases

 Detail Design : Detailed User Requirements for Revision Analysis 

 Other         : Called by: menu -> tabulations -> monthly -> revisions

 Revisions     : See Below
 *********************************************************************
 Modified Date : Dec 2, 2020
 Modified By   : Diane Musachio
 Keyword       : dm12022020
 Change Request: 
 Description   : re -sort after changes made
 *********************************************************************
 Modified Date : Dec 17, 2020
 Modified By   : Diane Musachio
 Keyword       : dm12172020
 Change Request: 
 Description   : fixed screen error when deleting row
 *********************************************************************
 Modified Date : April 27 2023
 Modified By   : Christine Zhang
 Keyword       : 
 Change Request: 
 Description   : Bug fix - show changes when survey changed
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
using System.Drawing.Printing;
using System.Globalization;

namespace Cprs
{
    public partial class frmRevision : frmCprsParent
    {
        public frmTotalVip CallingForm = null;

        public string Newtc;
        public string Survey;
        private string revisionNumber;

        private string sdate;
        private string pmonth;
        private string currmon;
        private string priorMonth1;
        private string priorMonth2;
        private string priorMonth3;
        private string priorMonth4;
        private string pmons1;
        private string pmons2;
        private string pmons3;
        private string pmons4;
        private string caseSubset;
        private string rev;

        private bool enterform = false;
        private bool onet = false;

        DataTable di = new DataTable();
        DataTable di1 = new DataTable();
        DataTable dt1 = new DataTable();
        DataTable dtSearch = new DataTable();
        DataView dv = new DataView();

        private bool call_callingFrom = false;

        public frmRevision()
        {
            InitializeComponent();

            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
        }

        private void frmRevision_Load(object sender, EventArgs e)
        {
            enterform = true;

            //update appropriate datatables with james bond id
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //get sdate from vipsadj table
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            ////Convert sdate to datetime for titles
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            priorMonth1 = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            priorMonth2 = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            priorMonth3 = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            priorMonth4 = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);

            pmons1 = (dt.AddMonths(-1)).ToString("MMM", CultureInfo.InvariantCulture);
            pmons2 = (dt.AddMonths(-2)).ToString("MMM", CultureInfo.InvariantCulture);
            pmons3 = (dt.AddMonths(-3)).ToString("MMM", CultureInfo.InvariantCulture);
            pmons4 = (dt.AddMonths(-4)).ToString("MMM", CultureInfo.InvariantCulture);

            rev = " 1st ";
            pmonth = priorMonth1;
            currmon = pmons1;
            caseSubset = "ALL CASES";
            UpdateTitles();
            revisionNumber = "REV1";
            FilterData(revisionNumber);

            enterform = false;
        }

        //dynamically set the headers
        private void UpdateTitles()
        {
            lblTitle1.Text = GeneralFunctions.GetSurveyText(Survey) + " " + sdate + rev + "Revision Analysis";
            lblTitle2.Text = "For " +  pmonth + " VIP";
            lblTitle3.Text = caseSubset;
            lblTitle4.UseMnemonic = false;
            lblTitle4.Text = "Newtc: " + Newtc + " " + GeneralDataFuctions.GetTCDesciption(Newtc);
        }

        //set counts for display panel
        private void SetTitles(DataTable dt)
        {
            int num_dec = 0;
            int num_inc = 0;
            int wd = 0;
            int wi = 0;
            int dp = 0;
            int ip = 0;
            double ddp = 0;
            double dip = 0;
            int blank = 0;

            if (dt == null)
            {
                lblDecP.Text = "0";
                lblIncP.Text = "0";
                lblDecPctP.Text = "0%";
                lblIncPctP.Text = "0%";
                lblTotP.Text = "0";
                lblDecW.Text = "0";
                lblIncW.Text = "0";
                lblTotW.Text = "0";

                return;
            }
        
            //calculates counts for increases and decreases
            foreach (DataRow row in dt.Rows)
            {
                int diff = Convert.ToInt32(row["change"]);
                if (diff > 0)
                {
                    num_inc++;
                    wi = wi + diff;
                }
                else if (diff < 0)
                {
                    num_dec++;
                    wd = wd + diff;
                }
                else
                {
                    blank = blank + 1;
                }
            }

            int tot = num_dec + num_inc;

            //calculates percentages of increases and decreases
            if (tot > 0)
            {
                ddp = Convert.ToDouble(num_dec) * 100 / Convert.ToDouble(tot);
                dp = Convert.ToInt32(Math.Round(ddp, MidpointRounding.AwayFromZero));
                dip = Convert.ToDouble(num_inc) * 100 / Convert.ToDouble(tot);
                ip = Convert.ToInt32(Math.Round(dip, MidpointRounding.AwayFromZero));
                lblDecPctP.Text = dp.ToString() + "%";
                lblIncPctP.Text = ip.ToString() + "%";
            }
            else
            {
                lblDecPctP.Text = "0%";
                lblIncPctP.Text = "0%";
            }
            
            lblDecP.Text = num_dec.ToString("N0");
            lblIncP.Text = num_inc.ToString("N0");
            lblTotP.Text = tot.ToString("N0");
            lblDecW.Text = wd.ToString("N0");
            lblIncW.Text = wi.ToString("N0");
            lblTotW.Text = (wd + wi).ToString("N0");
        }

        //set up datagrid display
        private void SetColumnHeader()
        {
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[0].Width = 60;
            dgData.Columns[1].HeaderText = "Newtc";
            dgData.Columns[1].Width = 60;
            dgData.Columns[2].HeaderText = "Status";
            dgData.Columns[2].Width = 60;
            dgData.Columns[3].HeaderText = "Owner";
            dgData.Columns[3].Width = 60;
            dgData.Columns[4].HeaderText = "Seldate";
            dgData.Columns[4].Width = 70;
            dgData.Columns[5].HeaderText = "Strtdate";
            dgData.Columns[5].Width = 70;
            dgData.Columns[6].HeaderText = "Compdate";
            dgData.Columns[6].Width = 70;
            dgData.Columns[7].HeaderText = "Change";
            dgData.Columns[7].Width = 80;
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "Curr Wgt " + currmon;
            dgData.Columns[8].DefaultCellStyle.Format = "N0";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].Width = 100;
            dgData.Columns[9].HeaderText = "F";
            dgData.Columns[9].Width = 40;
            dgData.Columns[10].HeaderText = "Prev Wgt " + currmon;
            dgData.Columns[10].DefaultCellStyle.Format = "N0";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[10].Width = 100;
            dgData.Columns[11].HeaderText = "F";
            dgData.Columns[11].Width = 40;
            dgData.Columns[12].HeaderText = "Fwgt";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[12].Width = 60;
            dgData.Columns[13].HeaderText = "Pwgt";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[13].Width = 60;
            dgData.Columns[14].HeaderText = "Curr " + currmon;
            dgData.Columns[14].DefaultCellStyle.Format = "N0";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[14].Width = 60;
            dgData.Columns[15].HeaderText = "Prev " + currmon;
            dgData.Columns[15].DefaultCellStyle.Format = "N0";
            dgData.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[15].Width = 60;
            dgData.Columns[16].Visible = false;
            dgData.Columns[17].Visible = false;
            dgData.Columns[18].Visible = false;
            dgData.Columns[19].Visible = false;
            dgData.Columns[20].Visible = false;
            dgData.Columns[21].Visible = false;
            dgData.Columns[22].Visible = false;
            dgData.Columns[23].Visible = false;
        }

        //query the data to be displayed for selected revision
        //create a dataview with all cases and create a filter
        //to subset the data for different revisions
        private void FilterData(string revisionNumber)
        {
            string selectSurvey;

            //assign values to parameters brought in from VIP screen
            string selectedNewtc2 = Newtc.Substring(0, 2).ToString();

            if (selectedNewtc2 == "1T")
            {
                onet = true;
            }

            if (Survey == "F")
            {
                selectSurvey = "FD";
            }
            else if (Survey == "P")
            {
                selectSurvey = "SL";
            }
            else if (Survey == "N")
            {
                selectSurvey = "NR";
            }
            else if (Survey == "U")
            {
                selectSurvey = "UT";
            }
            else if (Survey == "M")
            {
                selectSurvey = "MF";
            }
            else
            {
                selectSurvey = "T";
            }

            RevisionsData revision = new RevisionsData();

            //get data from dataview
            dt1 = revision.GetRevisionData(selectSurvey, selectedNewtc2, revisionNumber);
            dgData.DataSource = dt1;

            StringBuilder filter = new StringBuilder();

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                double change;

                //assign values
                if (!double.TryParse(dt1.Rows[i]["Change"].ToString(), out change)) change = 0;

                string id = dt1.Rows[i][0].ToString();
                string newtcData = dt1.Rows[i][1].ToString();
                string currtc2 = dt1.Rows[i][16].ToString();
                string prevtc2 = dt1.Rows[i][17].ToString();
                string currflag = dt1.Rows[i][9].ToString();
                string prevflag = dt1.Rows[i][11].ToString();
                string currtc2x = dt1.Rows[i][18].ToString();
                string prevtc2x = dt1.Rows[i][19].ToString();
                string currsurv = dt1.Rows[i][20].ToString();
                string prevsurv = dt1.Rows[i][21].ToString();
                string currstatus = dt1.Rows[i][22].ToString();
                string prevstatus = dt1.Rows[i][23].ToString();
            }

           filter.Append("(((( " + onet + "=true and currtc2x='1T' and prevtc2x='1T')" +
                          " or ( " + onet + "=false and currtc2 = " + selectedNewtc2.AddSqlQuotes() +
                          " and prevtc2 = " + selectedNewtc2.AddSqlQuotes() + ")))" +
                      " or ((" + onet + "=true and currtc2x='1T' and prevtc2x<>'1T')" +
                      " or  (" + onet + "=false and currtc2 = " + selectedNewtc2.AddSqlQuotes() + " " +
                      "      and prevtc2 <> " + selectedNewtc2.AddSqlQuotes() + "))" +
                      " or ((" + onet + "=true and currtc2x<>'1T' and prevtc2x='1T')" +
                      " or  (" + onet + "=false and currtc2<> " + selectedNewtc2.AddSqlQuotes() + " " +
                      "      and prevtc2= " + selectedNewtc2.AddSqlQuotes() + "))" +
                      " or ((prevsurv <> currsurv) and (currsurv = " + selectSurvey.AddSqlQuotes() +
                      "      or prevsurv = " + selectSurvey.AddSqlQuotes() + "))" +
                      " or ((currstatus <> prevstatus) and (currstatus='A' or currstatus='I')))");

            //use above criteria to filter datatable and store in data view
            dv = new DataView(dt1);
            dv.RowFilter = filter.ToString();
           
            di = new DataTable();
            di = dv.ToTable();

            //filter data with additional criteria that updated prevwvip and currwvip 
            //and recal
            //dm12172020 added else statements so doesn't attempt to delete row twice
            for (int i = 0; i < di.Rows.Count; i++)
            {
                double change;
                double currwvip;
                double prevwvip;
                
                if (!double.TryParse(di.Rows[i]["Change"].ToString(), out change)) change = 0;

                string id = di.Rows[i][0].ToString();
               
                string newtcData = di.Rows[i][1].ToString();
                if (!double.TryParse(di.Rows[i][8].ToString(), out currwvip)) currwvip = 0;
                if (!double.TryParse(di.Rows[i][10].ToString(), out prevwvip)) prevwvip = 0;
                string currtc2 = di.Rows[i][16].ToString();
                string prevtc2 = di.Rows[i][17].ToString();
                string currflag = di.Rows[i][9].ToString();
                string prevflag = di.Rows[i][11].ToString();
                string currsurv = di.Rows[i][20].ToString();
                string prevsurv = di.Rows[i][21].ToString();
                string currstatus = di.Rows[i][22].ToString();
                string prevstatus = di.Rows[i][23].ToString();

               
                //if tc changed
                if ((currtc2 == selectedNewtc2) && (prevtc2 != selectedNewtc2))
                {
                    double updatedprevwvip = 0;
                    di.Rows[i][10] = 0;
                    if (!double.TryParse(di.Rows[i][10].ToString(), out updatedprevwvip)) updatedprevwvip = 0;
                    change = currwvip - updatedprevwvip;
                    di.Rows[i]["Change"] = change;

                    if (change == 0)
                    {
                        di.Rows[i].Delete();
                    }
                }

                //if tc changed
                else if ((prevtc2 == selectedNewtc2) && (currtc2 != selectedNewtc2))
                {
                    double updatedcurrwvip = 0;
                    di.Rows[i][8] = 0;
                    if (!double.TryParse(di.Rows[i][8].ToString(), out updatedcurrwvip)) updatedcurrwvip = 0;
                    change = updatedcurrwvip - prevwvip;
                    di.Rows[i]["Change"] = change;

                    if (change == 0)
                    {
                        di.Rows[i].Delete();
                    }
                }

                //if owner changed
                else if ((currsurv == selectSurvey) && (prevsurv != currsurv))
                {
                    double updatedprevwvip = 0;
                    di.Rows[i][10] = 0;
                    if (!double.TryParse(di.Rows[i][10].ToString(), out updatedprevwvip)) updatedprevwvip = 0;
                    change = currwvip - updatedprevwvip;
                    di.Rows[i]["Change"] = change;

                    if (change == 0)
                    {
                        di.Rows[i].Delete();
                    }
                }

                //if survey changed
                else if ((prevsurv == selectSurvey) && (prevsurv != currsurv))
                {
                    double updatedcurrwvip = 0;
                    di.Rows[i][8] = 0;
                    if (!double.TryParse(di.Rows[i][8].ToString(), out updatedcurrwvip)) updatedcurrwvip = 0;
                    change = updatedcurrwvip - prevwvip;
                    di.Rows[i]["Change"] = change;

                    if (change == 0)
                    {
                        di.Rows[i].Delete();
                    }
                }

                //if status changed
                else if (currstatus != prevstatus)
                {
                    double updatedcurrwvip = 0;
                    di.Rows[i][8] = 0;
                    if (!double.TryParse(di.Rows[i][8].ToString(), out updatedcurrwvip)) updatedcurrwvip = 0;
                    change = updatedcurrwvip - prevwvip;
                    di.Rows[i]["Change"] = change;

                    if (change == 0)
                    {
                        di.Rows[i].Delete();
                    }
                }
                
            }

            //dm12022020
            //assign dataview to datagrid and display

            // Create DataView
            DataView view = new DataView(di);

            // Sort by Change column in descending order
            view.Sort = "change DESC";

            dgData.DataSource = view;
            SetColumnHeader();
            SetTitles(di);
                                
        }

        //radio button for first revision was activated
        private void rbFirstRev_CheckedChanged(object sender, EventArgs e)
        {
            if (!enterform)
            {
                cbItem.SelectedIndex = -1;
                txtValueItem.Text = "";
                cbValueItem.SelectedIndex = -1;
                txtValueItem.Visible = false;
                cbValueItem.Visible = false;
                caseSubset = "ALL CASES";
                rev = " 1st ";
                pmonth = priorMonth1;
                currmon = pmons1;
                UpdateTitles();
                revisionNumber = "REV1";
                FilterData(revisionNumber);
            }
        }

        //radio button for second revision was activated
        private void rbSecondRev_CheckedChanged(object sender, EventArgs e)
        {
            if (!enterform)
            {
                cbItem.SelectedIndex = -1;
                txtValueItem.Text = "";
                cbValueItem.SelectedIndex = -1;
                txtValueItem.Visible = false;
                cbValueItem.Visible = false;
                caseSubset = "ALL CASES";
                rev = " 2nd ";
                pmonth = priorMonth2;
                currmon = pmons2;
                UpdateTitles();
                revisionNumber = "REV2";
                FilterData(revisionNumber);
            }
        }

        //radio button for third revision was activated
        private void rbThirdRev_CheckedChanged(object sender, EventArgs e)
        {
            if (!enterform)
            {
                cbItem.SelectedIndex = -1;
                txtValueItem.Text = "";
                cbValueItem.SelectedIndex = -1;
                txtValueItem.Visible = false;
                cbValueItem.Visible = false;
                caseSubset = "ALL CASES";
                rev = " 3rd ";
                pmonth = priorMonth3;
                currmon = pmons3;
                UpdateTitles();
                revisionNumber = "REV3";
                FilterData(revisionNumber);
            }
        }

        //radio button for fourth revision was activated
        private void rbFourthRev_CheckedChanged(object sender, EventArgs e)
        {
            if (!enterform)
            {
                cbItem.SelectedIndex = -1;
                txtValueItem.Text = "";
                cbValueItem.SelectedIndex = -1;
                txtValueItem.Visible = false;
                cbValueItem.Visible = false;
                caseSubset = "ALL CASES";
                rev = " 4th ";
                pmonth = priorMonth4;
                currmon = pmons4;
                UpdateTitles();
                revisionNumber = "REV4";
                FilterData(revisionNumber);
            }
        }

        //takes user to previous screen
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                CallingForm.RefreshForm(false);
                call_callingFrom = true;
            }

            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        { 
            string colname = cbItem.Text;
            string txt;

            if (cbItem.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a value from dropdown");
                return;
            }
            if (txtValueItem.Text == "")
            {
                MessageBox.Show("Please enter a " + cbItem.Text + " value");
                return;
            }

            string filter = "";

            //validate id is 7 digits and valid
            if (cbItem.SelectedIndex == 0)
            {
                txtValueItem.Visible = true;

                if (!(txtValueItem.Text.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits.");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    return;
                }
                else
                {
                    if (GeneralDataFuctions.ValidateSampleId(txtValueItem.Text))
                    {
                        txt = txtValueItem.Text;
                        filter = "id = " + txt.AddSqlQuotes();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Id");
                        txtValueItem.Focus();
                        txtValueItem.Clear();
                        return;
                    }
                }
            }

            //validate newtc is 4 digit and in valid list or newtclist
            else if (cbItem.SelectedIndex == 1)
            {
                txtValueItem.Visible = true;

                if ((txtValueItem.Text.Trim() == "") || ((txtValueItem.TextLength != 4)))
                {
                    MessageBox.Show("Invalid NEWTC");
                    txtValueItem.Focus();
                    txtValueItem.Clear();

                    return;
                }
                else if (txtValueItem.Text.Length == 4)
                {
                    //check to see if 4 digit newtc is valid in newtclist table
                    if (!(GeneralDataFuctions.CheckNewTC(txtValueItem.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtValueItem.Focus();
                        txtValueItem.Clear();

                        return;
                    }
                    else
                    {
                        txt = txtValueItem.Text;
                        filter = "newtc = " + txt.AddSqlQuotes();
                    }
                }
            }

            //validate startdate
            if (cbItem.SelectedIndex == 2)
            {
                txtValueItem.Visible = true;

                if (!(txtValueItem.Text.Length == 6))
                {
                    MessageBox.Show("Invalid STRTDATE");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    return;
                }
                else
                {
                    txt = txtValueItem.Text;
                    filter = "strtdate = " + txt.AddSqlQuotes();
                }
            }

            //use above criteria to filter datatable and store in data view
            var filteredDataTable = new DataTable();

            DataView dv1 = new DataView(di);
            dv1.RowFilter = filter;

            dtSearch = dv1.ToTable();
            dgData.DataSource = dv1;

            SetColumnHeader();
            caseSubset = "ALL CASES";
            UpdateTitles();

            SetTitles(dtSearch);

            if (dtSearch.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
                txtValueItem.Focus();
                txtValueItem.Clear();
                return;
            }
        }

        //first combobox selection
        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbValueItem.Visible = false;
            txtValueItem.Visible = true;
            txtValueItem.Focus();
            txtValueItem.Clear();

            //verify id/respid is 7 digits in length
            if (cbItem.SelectedIndex == 0)
            {
                txtValueItem.MaxLength = 7;
            }

            //verify newtc is not over 4 digits in length
            else if (cbItem.SelectedIndex == 1)
            {
               txtValueItem.MaxLength = 4;
            }

            //verify strtdate is not over 6 digits in length
            if (cbItem.SelectedIndex == 2)
            {
                txtValueItem.MaxLength = 6;
            }

            txtValueItem.Text = "";
            
            cbValueItem.Text = "";
        }

        //center header
        private void lblTitle1_SizeChanged(object sender, EventArgs e)
        {
            lblTitle1.Left = (this.ClientSize.Width - lblTitle1.Size.Width) / 2;
        }

        //center header
        private void lblTitle2_SizeChanged(object sender, EventArgs e)
        {
            lblTitle2.Left = (this.ClientSize.Width - lblTitle2.Size.Width) / 2;
        }

        //center header
        private void lblTitle3_SizeChanged(object sender, EventArgs e)
        {
            lblTitle3.Left = (this.ClientSize.Width - lblTitle3.Size.Width) / 2;
        }

        //center header
        private void lblTitle4_SizeChanged(object sender, EventArgs e)
        {
            lblTitle4.Left = (this.ClientSize.Width - lblTitle4.Size.Width) / 2;
        }

        //prevents user from entering anything except digits
        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //Clear search selection
        private void btnClear_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "ALL CASES";
            UpdateTitles();
            FilterData(revisionNumber);
            SetColumnHeader();
        }

        //Button for Blank to Imputed
        private void btnBtoI_Click(object sender, EventArgs e)
        {
            caseSubset = "Blank to Impute";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag = '' and currflag in ('M','I')";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Blank to Reported
        private void btnBtoR_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Blank to Report";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag = '' and currflag in ('R','A','O')";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Reported to Blank
        private void btnRtoB_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Report to Blank";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag in ('R','A','O') and currflag = ''";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Imputed to Blank
        private void btnItoB_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Impute to Blank";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag in ('M','I') and currflag = ''";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Imputed to Reported
        private void btnItoR_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Impute to Report";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag in ('M','I') and currflag in ('R','A','O')";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        } 

        //Button for Imputed to Imputed
        private void btnItoI_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Impute to Impute";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag in ('M', 'I') and currflag in ('M', 'I')";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Reported to Reported
        private void btnRtoR_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Report to Report";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "prevflag in ('R','A','O') and currflag in ('R','A','O')";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Status Changed
        private void btnSChange_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Status Changed";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "currstatus <> prevstatus";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for Owner Changed
        private void btnOChange_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "Owner Changed";
            UpdateTitles();

            dv = new DataView(di);
            dv.RowFilter = "currsurv <> prevsurv";
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //Button for TC Changed
        private void btnTChange_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            caseSubset = "TC Changed";
            UpdateTitles();
            string filter;

            dv = new DataView(di);

            filter = "((currtc2 = '1T' and currtc2x <> prevtc2x) or (currtc2 <> '1T' and currtc2 <> prevtc2))";
           
            dv.RowFilter = filter;
            dv.Sort = "change DESC";

            //subset filter
            di1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(di1);

            if (di1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        //passes id list to c700
        private void btnC700_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

            int index = dgData.CurrentRow.Index;
            string selected_id = dgData["ID", index].Value.ToString();

            this.Hide(); // hide parent

            string val1 = dgData["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down
            List<string> Idlist = new List<string>();
            int cnt = 0;
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                string val = dgData["ID", cnt].Value.ToString();
                Idlist.Add(val);
                cnt = cnt + 1;
            }

            frmC700 fC700 = new frmC700();
            fC700.Id = val1;
            fC700.Idlist = Idlist;
            fC700.CurrIndex = index;
            fC700.CallingForm = this;
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            fC700.ShowDialog(); // show child

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
        }

        //closing form
        private void frmRevision_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();

        }


        //Button to print data
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgData.RowCount > 235)
            {
                if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PrintData();
                }
            }
            else
            {
                PrintData();
            }
        }

        private void PrintData()
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

            printer.Title = lblTitle1.Text;
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = lblTitle2.Text + "     " + lblTitle3.Text + "    " + lblTitle4.Text;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Revision Analysis Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            dgPrint.Columns[0].Width = 60;
            dgPrint.Columns[1].Width = 60;
            dgPrint.Columns[2].Width = 60;
            dgPrint.Columns[3].Width = 60;
            dgPrint.Columns[4].Width = 70;
            dgPrint.Columns[5].Width = 70;
            dgPrint.Columns[6].Width = 70;
            dgPrint.Columns[7].Width = 80;
            dgPrint.Columns[8].Width = 90;
            dgPrint.Columns[9].Width = 30;
            dgPrint.Columns[10].Width = 90;
            dgPrint.Columns[11].Width = 30;
            dgPrint.Columns[12].Width = 60;
            dgPrint.Columns[13].Width = 60;
            dgPrint.Columns[14].Width = 60;
            dgPrint.Columns[15].Width = 60;

            printer.PrintDataGridViewWithoutDialog(dgPrint);
            
            Cursor.Current = Cursors.Default;
        }

        private void txtValueItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
        }
    }
}