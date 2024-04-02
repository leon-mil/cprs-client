/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCallSchedulerCounts.cs	    	
Programmer:         Christine Zhang
Creation Date:      10/2/2017
Inputs:             None
Parameters:	        None 
Outputs:	        Display call scheduler counts	
Description:	    This screen tabulates data from Sched_call. 
Detailed Design:    None 
Other:	            Called by: Main form
Revision History:	
****************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
****************************************************************************************/
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
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;


namespace Cprs
{
    public partial class frmCallSchedulerCounts : frmCprsParent
    {
        public frmCallSchedulerCounts()
        {
            InitializeComponent();
        }

        private CallSchedulerCountsData data_object;
        private string survey;

        private void frmCallSchedulerCounts_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            data_object = new CallSchedulerCountsData();

            tabs.SelectedIndex = 0;
            rd1a.Checked = true;
            survey = "T";
            GetData();

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead)
                btnC700.Text = "TFU";
            else
                btnC700.Text = "C-700";
        }

        //get data
        private void GetData()
        {
            DataTable dt = new DataTable();

            //get contact data
            dt = data_object.GetContactStatusData(survey);
            dgt1.DataSource = dt;
  
            //get vip data
            dt = data_object.GetVIPStatusData(survey);
            dgt2.DataSource = dt;
  
            if (tabs.SelectedIndex == 0)
            {
                setupColumnHeader(dgt1);

                //highlight first row, second column, get related cases
                dgt1.ClearSelection();
                dgt1.Rows[0].Cells[1].Selected = true;
                DoCellClick(0, 1);
            }
            //for vip tab
            else
            {
                setupColumnHeader2(dgt2);

                //highlight first row, second column, get related cases
                dgt2.ClearSelection();
                dgt2.Rows[0].Cells[1].Selected = true;
                DoCellClick2(0, 1);
            }
        }

        private void setupColumnHeader(DataGridView dg)
        {
            dg.Columns[0].HeaderText = "CASE TYPE";
            dg.Columns[0].Width = 300;
            dg.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dg.Columns[1].HeaderText = "TOTAL CASES";
            dg.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[1].DefaultCellStyle.Format = "N0";
            dg.Columns[2].HeaderText = "TOTAL CONTACTED";
            dg.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[2].DefaultCellStyle.Format = "N0";
            dg.Columns[3].HeaderText = "NOT CONTACTED";
            dg.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[3].DefaultCellStyle.Format = "N0";
            dg.Columns[4].HeaderText = "PERCENT CONTACTED";
            dg.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[4].DefaultCellStyle.Format = "N1";
            dg.Columns[5].HeaderText = "TOTAL REFERRALS";
            dg.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[5].DefaultCellStyle.Format = "N0";

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dg.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void setupColumnHeader2(DataGridView dg)
        {
            dg.Columns[0].HeaderText = "CASE TYPE";
            dg.Columns[0].Width  =300;

            dg.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dg.Columns[1].HeaderText = "TOTAL CASES";
            dg.Columns[2].HeaderText = "TOTAL SATISFIED";
            dg.Columns[3].HeaderText = "TOTAL UNSATISFIED";
            dg.Columns[4].HeaderText = "PERCENT SATISFIED";
            dg.Columns[5].HeaderText = "BY FORM";
            dg.Columns[6].HeaderText = "BY CENTURION";
            dg.Columns[7].HeaderText = "BY PHONE";
            dg.Columns[8].HeaderText = "BY WEB";
            dg.Columns[9].HeaderText = "BY ADMIN";

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dg.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (dgvc.Index !=0)
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    if (dgvc.Index == 4)
                        dgvc.DefaultCellStyle.Format = "N1";
                    else
                        dgvc.DefaultCellStyle.Format = "N0";
                }
            }
        }

        private void rd1n_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1n.Checked)
            { 
                //for nonresidential
                survey = "P";
                GetData();
            }

        }

        private void rd1m_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1m.Checked)
            {
                //for multifamily
                survey = "M";
                GetData();
            }
        }

        private void rd1a_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1a.Checked)
            {
                //for all surveys
                survey = "T";
                GetData();
            }
        }

        private void rd1s_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1s.Checked)
            {
                //for state local
                survey = "S";
                GetData();
            }
        }

        private void rd1f_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1f.Checked)
            {
                //for federal
                survey = "F";
                GetData();
            }
        }

        private void rd1u_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1u.Checked)
            {
                //for utilities
                survey = "U";
                GetData();
            }
        }

        private void frmCallSchedulerCounts_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for contact tab
            if (tabs.SelectedIndex == 0)
            {
                setupColumnHeader(dgt1);

                //highlight first row, second column, get related cases
                dgt1.ClearSelection();
                dgt1.Rows[0].Cells[1].Selected = true;
                DoCellClick(0, 1);
            }
            //for vip tab
            else
            {
                setupColumnHeader2(dgt2);

                //highlight first row, second column, get related cases
                dgt2.ClearSelection();
                dgt2.Rows[0].Cells[1].Selected = true;
                DoCellClick2(0, 1);
            }
        }

        //vip grid cell click
        private void dgt2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //exist if first column or percent column
            if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
                return;

            DoCellClick2(e.RowIndex, e.ColumnIndex);
        }

        private void DoCellClick2(int rowindex, int colindex)
        {
            if (rowindex < 0) return;

            this.Cursor = Cursors.WaitCursor;

            DataTable dt = new DataTable();

            //check value 0
            if (dgt2.Rows[rowindex].Cells[colindex].Value.ToString() == "0")
            {
                dt = data_object.GetEmptyTable();
                btnC700.Enabled = false;
                btnNA.Enabled = false;
                btnHist.Enabled = false;
            }
            else
            {
                dt = data_object.GetVIPStatusCases(survey, colindex, rowindex);
                btnC700.Enabled = true;
                btnNA.Enabled = true;
                btnHist.Enabled = true;
            }

            dgb2.DataSource = dt;
            dgb2.Columns[0].HeaderText = "ID";
            dgb2.Columns[1].HeaderText = "RESPID";
            dgb2.Columns[2].HeaderText = "COLTEC";
            dgb2.Columns[3].HeaderText = "COLHIST";
            dgb2.Columns[4].HeaderText = "OWNER";
            dgb2.Columns[5].HeaderText = "NEWTC";
            dgb2.Columns[6].HeaderText = "STRTDATE";
            dgb2.Columns[7].HeaderText = "COMPDATE";
            dgb2.Columns[8].HeaderText = "RVITM5C";
            dgb2.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgb2.Columns[8].DefaultCellStyle.Format = "N0";

            if (dt.Rows.Count > 1)
                lblcount2.Text = dt.Rows.Count.ToString() + " CASES";
            else
                lblcount2.Text = dt.Rows.Count.ToString() + " CASE";
            this.Cursor = Cursors.Default;
        }

        //contact grid cell click
        private void dgt1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //exist if first column or percent column
            if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
                return;
            DoCellClick(e.RowIndex, e.ColumnIndex);
            
        }

        private void DoCellClick(int rowindex, int colindex)
        {
            if (rowindex < 0) return;

            this.Cursor = Cursors.WaitCursor;

            DataTable dt = new DataTable();

            //check value 0
            if (dgt1.Rows[rowindex].Cells[colindex].Value.ToString() == "0")
            {
                dt = data_object.GetEmptyTable();
                btnC700.Enabled = false;
                btnNA.Enabled = false;
                btnHist.Enabled = false;
            }
            else
            {
                dt = data_object.GetContactStatusCases(survey, colindex, rowindex);
                btnC700.Enabled = true;
                btnNA.Enabled = true;
                btnHist.Enabled = true;
            }

            dgb1.DataSource = dt;
            dgb1.Columns[0].HeaderText = "ID";
            dgb1.Columns[1].HeaderText = "RESPID";
            dgb1.Columns[2].HeaderText = "COLTEC";
            dgb1.Columns[3].HeaderText = "COLHIST";
            dgb1.Columns[4].HeaderText = "OWNER";
            dgb1.Columns[5].HeaderText = "NEWTC";
            dgb1.Columns[6].HeaderText = "STRTDATE";
            dgb1.Columns[7].HeaderText = "COMPDATE";
            dgb1.Columns[8].HeaderText = "RVITM5C";
            dgb1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgb1.Columns[8].DefaultCellStyle.Format = "N0";

            if (dt.Rows.Count > 1)
                lblcount1.Text = dt.Rows.Count.ToString() + " CASES";
            else
                lblcount1.Text = dt.Rows.Count.ToString() + " CASE";
            this.Cursor = Cursors.Default;
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.NPCManager ||UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                string resp;
                if (tabs.SelectedIndex == 0)
                {
                    //get id
                    resp = dgb1["RESPID", dgb1.CurrentRow.Index].Value.ToString();
                }
                else
                {
                    resp = dgb2["RESPID", dgb2.CurrentRow.Index].Value.ToString();
                }

                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

                tfu.ShowDialog();   // show child
                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");

                return;
            }

            string val1;

            // Store ID in list for Page Up and Page Down
            List<string> Idlist = new List<string>();
            frmC700 fC700 = new frmC700();
            int curr_index = 0;

            if (tabs.SelectedIndex == 0) 
            {
                //DataTable dt = (DataTable)dgb1.DataSource;
                
                //get id
                int index = dgb1.CurrentRow.Index;
                val1 = dgb1["ID", index].Value.ToString();

                int cnt = 0;
                foreach (DataGridViewRow dr in dgb1.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    Idlist.Add(val);
                    if (val == val1)
                        curr_index = cnt; 
                    cnt = cnt + 1;
                }
            }
            else
            {
                //DataTable dt = (DataTable)dgb2.DataSource;

                //get id
                int index = dgb2.CurrentRow.Index;
                val1 = dgb2["ID", index].Value.ToString();

                int cnt = 0;
                foreach (DataGridViewRow dr in dgb2.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    Idlist.Add(val);
                    if (val == val1)
                        curr_index = cnt;
                    cnt = cnt + 1;
                }
            }

            this.Hide();        // hide parent
            fC700.Id = val1;
            fC700.Idlist = Idlist;
            fC700.CurrIndex = curr_index;
            fC700.CallingForm = this;

            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

            fC700.ShowDialog(); // show child

            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
        }

        private void btnNA_Click(object sender, EventArgs e)
        {
            string val1;

            // Store ID in list for Page Up and Page Down
            List<string> Idlist = new List<string>();
            frmName fname = new frmName();
            int curr_index = 0;

            if (tabs.SelectedIndex == 0)
            {
                DataTable dt = (DataTable)dgb1.DataSource;

                //get id
                int index = dgb1.CurrentRow.Index;
                val1 = dgb1["ID", index].Value.ToString();

                int cnt = 0;
                foreach (DataGridViewRow dr in dgb1.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    Idlist.Add(val);
                    if (val == val1)
                        curr_index = cnt;
                    cnt = cnt + 1;
                }
            }
            else
            {
                DataTable dt = (DataTable)dgb2.DataSource;

                //get id
                int index = dgb2.CurrentRow.Index;
                val1 = dgb2["ID", index].Value.ToString();

                int cnt = 0;
                foreach (DataGridViewRow dr in dgb2.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    Idlist.Add(val);
                    if (val == val1)
                        curr_index = cnt;
                    cnt = cnt + 1;
                }
            }

            this.Hide();        // hide parent
            fname.Id = val1;
            fname.Idlist = Idlist;
            fname.CurrIndex = curr_index;
            fname.CallingForm = this;

            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

            fname.ShowDialog(); // show child

            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PrintData(dgt1, "Contact");
            PrintData(dgt2, "VIP");
            Cursor.Current = Cursors.Default;
        }

        //from survey, get full survey name, it is used for print
        private string GetSurveyText(string s)
        {
            if (s == "T")
                return "All Surveys";
            if (s == "S")
                return "State and Local";
            else if (s == "P")
                return "Private";
            else if (s == "F")
                return "Federal";
            else if (s == "M")
                return "Multifamily";
            else
                return "Utilities";
        }

        //Print multiple datagrids based on survey criteria
        private void PrintData(DataGridView dgP, string tabname)
        {
            //set dgPrint
            dgPrint.DataSource = null; 
            dgPrint.DataSource = dgP.DataSource;

            if (tabname == "Contact")
                setupColumnHeader(dgPrint);
            else
            {
                setupColumnHeader2(dgPrint);
                dgPrint.Columns[0].Width = 300;
                
                dgPrint.Columns[3].Width = 85;
                dgPrint.Columns[6].Width = 80;

            }

            DGVPrinter printer = new DGVPrinter();
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.Title = "Call Scheduler Counts: " + tabname;
            printer.SubTitle = "Survey: " + GetSurveyText(survey);
            printer.PageSettings.Landscape = true;
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Call Scheduler Counts: " + tabname;
            printer.Footer = " ";
            
            printer.PrintDataGridViewWithoutDialog(dgPrint);

        }

        private void dgt1_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell == null || e.StateChanged != DataGridViewElementStates.Selected)
                return;
            if (e.Cell.ColumnIndex == 0 || e.Cell.ColumnIndex == 4)
                e.Cell.Selected = false;
        }

        private void dgt2_CellStateChanged(object sender, DataGridViewCellStateChangedEventArgs e)
        {
            if (e.Cell == null  || e.StateChanged != DataGridViewElementStates.Selected)
                return;
            if (e.Cell.ColumnIndex == 0 || e.Cell.ColumnIndex ==4)
                e.Cell.Selected = false;

        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            string val1 = string.Empty;

            if (tabs.SelectedIndex == 0)
            {
                //get id
                int index = dgb1.CurrentRow.Index;
                val1 = dgb1["ID", index].Value.ToString();
            }
            else
            {
                //get id
                int index = dgb2.CurrentRow.Index;
                val1 = dgb2["ID", index].Value.ToString();
            }

            SchedCallData scdata = new SchedCallData();
            DataTable dtsc = scdata.GetSchedHistDataByID(val1);
            if (dtsc.Rows.Count == 0)
            {
                MessageBox.Show("There are no Scheduler History records for this case");
                return;
            }

            frmSchedHistPopup popup = new frmSchedHistPopup();
            popup.Id = val1;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
        }

        private void btnNA_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnC700_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnHist_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnPrint_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
