/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecTimeLenWorksheet.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 4/24/2019
Inputs          : None                 
Parameters      : selected TC, selectedSurvey, calling form and Editable
Outputs         : None	
Description     : Display Special Time len worksheet 
Detailed Design : Detailed Design for Time length worksheet
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
using System.Drawing.Printing;
namespace Cprs
{
    public partial class frmSpecTimeLenWorksheet : frmCprsParent
    {
        public frmSpecTimeLenWorksheet()
        {
            InitializeComponent();
        }

        public frmSpecTimeLen CallingForm = null;
        public string SelectedTc;
        public string SelectedSurvey;
        public bool Editable = true;

        private SpecTimelenData data_object;
        private DataTable dtProject = new DataTable();
        private DataTable clonetable;
        private bool call_callingFrom = false;
        private bool isApply = false;
        private bool isModified = false;
        private bool isSave = false;

        private void frmSpecTimeLenWorksheet_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");
            if (SelectedSurvey == "P")
                lblTitle.Text = "STATE AND LOCAL WORKSHEET";
            else if (SelectedSurvey == "N")
                lblTitle.Text = "NONRESIDENTIAL WORKSHEET";
            else
                lblTitle.Text = "MULTIFAMILY WORKSHEET";

            int year = DateTime.Now.Year;
            label2.Text = "Project Completed in " + (year - 2).ToString() + "01-" + (year - 1).ToString() + "12";

            //update lock
            LotlockData lock_data = new LotlockData();
            if (Editable)
            {
                lock_data.UpdateLotLock(SelectedSurvey, true);
            }

            data_object = new SpecTimelenData();
            
            List<string> exlist = data_object.GetLottabexData(DateTime.Now.Year.ToString(), SelectedSurvey, SelectedTc);
            string excluded_str = "";
            if (exlist.Count >0)
            {
                foreach (string id in exlist)
                {
                    if (excluded_str == "")
                        excluded_str = id;
                    else
                        excluded_str = excluded_str + " " + id;

                    excludelist.Items.Add(id);
                }
            }

            GetMainData(excluded_str);
            GetProjectData(exlist);

            if (!Editable)
            {
                btnExclude.Enabled = false;
                btnRefresh.Enabled = false;
                btnApply.Enabled = false;
                btnSave.Enabled = false;
            }
        }

        private void GetMainData(string excluded)
        {
            DataTable dtm = data_object.GetTimeLenDataWorksheet(SelectedSurvey, SelectedTc, excluded);
            dgMain.DataSource = dtm; 
            SetColumnHeader();
        }

        private void GetProjectData(List<string> exlist)
        {
            dtProject = data_object.GetTimeLenWorksheetProjects(SelectedSurvey, SelectedTc, exlist);
            dgData.DataSource = dtProject;
            clonetable = dtProject.Clone();
            SetColumnHeaderProj();
        }

        private void SetColumnHeaderProj()
        {
            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[1].HeaderText = "Newtc";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[2].HeaderText = "Status";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[3].HeaderText = "Seldate";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[4].HeaderText = "Rvitm5c";
            dgData.Columns[4].DefaultCellStyle.Format = "N0";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = "Strtdate";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[6].HeaderText = "Compdate";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[7].HeaderText = "Months";
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "WT Months";
            dgData.Columns[8].DefaultCellStyle.Format = "N1";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[9].HeaderText = "Fwgt";
            dgData.Columns[9].DefaultCellStyle.Format = "N1";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[10].HeaderText = "Cum VIP";
            dgData.Columns[10].DefaultCellStyle.Format = "N0";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[11].HeaderText = "VG";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[12].HeaderText = "Excluded";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[13].Visible = false;

            lblCount.Text = dtProject.Rows.Count.ToString();
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgMain.Columns[0].HeaderText = "Type of Construction";
            dgMain.Columns[0].Width = 235;
            dgMain.Columns[1].Visible = false;

            if (SelectedSurvey == "M")
            {
                dgMain.Columns[2].HeaderText = "<3000";
                dgMain.Columns[2].DefaultCellStyle.Format = "N1";
                dgMain.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[3].HeaderText = "3000 to 4999";
                dgMain.Columns[3].DefaultCellStyle.Format = "N1";
                dgMain.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[4].HeaderText = "5000 to 9999";
                dgMain.Columns[4].DefaultCellStyle.Format = "N1";
                dgMain.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[5].HeaderText = ">=10000";
                dgMain.Columns[5].DefaultCellStyle.Format = "N1";
                dgMain.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[6].HeaderText = "All";
                dgMain.Columns[6].DefaultCellStyle.Format = "N1";
                dgMain.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }
            else
            {
                dgMain.Columns[2].HeaderText = "<250";
                dgMain.Columns[2].DefaultCellStyle.Format = "N1";
                dgMain.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[3].HeaderText = "250 to 999";
                dgMain.Columns[3].DefaultCellStyle.Format = "N1";
                dgMain.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[4].HeaderText = "1000 to 2999";
                dgMain.Columns[4].DefaultCellStyle.Format = "N1";
                dgMain.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[5].HeaderText = "3000 to 4999";
                dgMain.Columns[5].DefaultCellStyle.Format = "N1";
                dgMain.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[6].HeaderText = "5000 to 9999";
                dgMain.Columns[6].DefaultCellStyle.Format = "N1";
                dgMain.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[7].HeaderText = ">=10000";
                dgMain.Columns[7].DefaultCellStyle.Format = "N1";
                dgMain.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgMain.Columns[8].HeaderText = "All";
                dgMain.Columns[8].DefaultCellStyle.Format = "N1";
                dgMain.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgMain.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgMain.ClearSelection();
        }

        //Verify Form Closing from menu
        public override bool VerifyFormClosing()
        {
            bool can_close = true;

            if (Editable && isModified)
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                        can_close = false;
            }
             
            return can_close;
        }

        private void frmSpecTimeLenWorksheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            LotlockData lock_data = new LotlockData();
            if (Editable)
                lock_data.UpdateLotLock(SelectedSurvey, false);

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void btnExclude_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection rows = dgData.SelectedCells;
            if (rows.Count == 0)
            {
                MessageBox.Show("You have to select a case");
                return;
            }
            
            string sel_id = dgData["ID", rows[0].RowIndex].Value.ToString();
            if (Convert.ToInt16(dgData["excluded", rows[0].RowIndex].Value) == 0)
            {
                dgData["excluded", rows[0].RowIndex].Value = 1;
                excludelist.Items.Add(sel_id);
                isModified = true;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedCellCollection rows = dgData.SelectedCells;
            if (rows.Count == 0)
            {
                MessageBox.Show("You have to select a case");
                return;
            }

            string sel_id = dgData["ID", rows[0].RowIndex].Value.ToString();
            if (Convert.ToInt16(dgData["excluded", rows[0].RowIndex].Value) == 1)
            {
                dgData["excluded", rows[0].RowIndex].Value = 0;
                excludelist.Items.Remove(sel_id);
                isModified = true;
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            string ex_str = "";
            if (!isModified) return;

            //update exclude flag
            if (excludelist.Items.Count > 0)
            {
                foreach (string id in excludelist.Items)
                {
                    if (ex_str == "")
                        ex_str = id;
                    else
                        ex_str = ex_str + " " + id;
                }
            }
            GetMainData(ex_str);
            isApply = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!isModified)
                return;

            if (!isApply)
            {
                MessageBox.Show("You must apply changes before saving data to tables");
                return;
            }
            SaveData();

            //set buttom focus
            dgData.Rows[0].Selected = true;

            MessageBox.Show("Save Data compeleted.");
            isApply = false;
            isModified = false;
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (isModified)
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            int index = 0;
            string val1 = string.Empty;
            List<string> Idlist = new List<string>();

            //check current selected control
            
            DataGridViewSelectedCellCollection rows = dgData.SelectedCells;
            if (rows.Count == 0)
            {
                MessageBox.Show("You have to select a case");
                return;
            }
            index = rows[0].RowIndex;
            val1 = dgData["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down
            int cnt = 0;
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                string val = dgData["ID", cnt].Value.ToString();
                Idlist.Add(val);
                cnt = cnt + 1;
            }
           
            this.Hide();

            frmC700 fC700 = new frmC700();
            fC700.Id = val1;
            fC700.Idlist = Idlist;
            fC700.CurrIndex = index;
            
            fC700.CallingForm = this;

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            fC700.ShowDialog();  // show child

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (Editable && isModified)
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            LotlockData lock_data = new LotlockData();
            if (Editable)
                lock_data.UpdateLotLock(SelectedSurvey, false);

            if (CallingForm != null)
            {
                CallingForm.Show();
                if (isSave)
                  CallingForm.RefreshForm();
                call_callingFrom = true;
            }
            this.Close();
        }

        //Save data
        private void SaveData()
        {
            data_object.DeleteLottabexData(DateTime.Now.Year.ToString(), SelectedSurvey, SelectedTc);
           
            if (excludelist.Items.Count > 0)
            {
                foreach (string id in excludelist.Items)
                {
                    foreach (DataRow row in dtProject.Rows)
                    {
                        if (row["id"].ToString() == id)
                        {
                            data_object.AddLottabexData(row["id"].ToString(), row["owner"].ToString(), SelectedTc);
                        }
                    }
                }
            }

            isSave = true;
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            string cvalue;
            string colname = cbItem.Text;
            IEnumerable<DataRow> query;

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

            cvalue = txtValueItem.Text.Trim();
            if (colname == "ID")
            {
                if (cvalue.Length != 7 || !GeneralDataFuctions.ValidateSampleId(cvalue))
                {
                    if (cvalue.Length != 7)
                    {
                        MessageBox.Show("ID should be 7 digits");
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID");
                    }

                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }
            else
            {
                if (cvalue.Length != 6 || !GeneralFunctions.ValidateDateWithRange(cvalue))
                {
                    MessageBox.Show("Invalid Compdate");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }

            query = from myRow in dtProject.AsEnumerable()
                    where myRow.Field<string>(colname).StartsWith(cvalue)
                    orderby myRow.Field<string>(colname) descending
                    select myRow;

            // Create a table from the query.
            if (query.Count() > 0)
            {
                DataTable boundTable = query.CopyToDataTable<DataRow>();
                dgData.DataSource = boundTable;
                lblCount.Text = boundTable.Rows.Count.ToString("N0");
            }
            else
            {
                MessageBox.Show("No data exists");
                txtValueItem.Text = "";
                dgData.DataSource = clonetable;
                lblCount.Text = "0";
            }
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValueItem.Visible = true;
            txtValueItem.Text = "";
            if (cbItem.SelectedIndex == 0)
                txtValueItem.MaxLength = 7;
            else if (cbItem.SelectedIndex == 1)
                txtValueItem.MaxLength = 6;

            txtValueItem.Focus();
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            txtValueItem.Text = "";

            //update project based on excludelist
            if (excludelist.Items.Count > 0)
            {
                foreach (string id in excludelist.Items)
                {
                    foreach (DataRow row in dtProject.Rows)
                    {
                        if (row["id"].ToString() == id)
                        {
                            row["excluded"] = 1;
                        }
                    }
                }
            }
            dgData.DataSource = null;
            dgData.DataSource = dtProject;
            SetColumnHeaderProj();
        }

        private void btnVGM_Click(object sender, EventArgs e)
        {
            DataTable pdata = dgData.DataSource as DataTable;
            DataView dv = pdata.DefaultView;
            dv.Sort = "vg asc, months asc";
            dgData.DataSource = dv.ToTable();
        }

        private void txtValueItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearchItem_Click(sender, e);
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnVGWM_Click(object sender, EventArgs e)
        {
            DataTable pdata = dgData.DataSource as DataTable;
            DataView dv = pdata.DefaultView;
            dv.Sort = "vg asc, wtmonths asc";
            dgData.DataSource = dv.ToTable();
        }
    }
}
