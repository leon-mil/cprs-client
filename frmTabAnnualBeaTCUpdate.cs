/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabAnnualBeaTCUpdate.cs
Programmer    : Christine Zhang
Creation Date : April. 22 2019
Parameters    : SelectedTC, CallingForm, SelectedYear
Inputs        : N/A
Outputs       : N/A
Description   : create annual bea TC Update screen
Change Request: 
Detail Design : 
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
namespace Cprs
{
    public partial class frmTabAnnualBeaTCUpdate : frmCprsParent
    {
        public frmTabAnnualBea CallingForm = null;
        public string SelectedTc;
        public string SelectedYear;
        public bool Editable;

        private TabAnnualBeaData data_object = new TabAnnualBeaData();
        private DataTable dtProj;
        
        private AnnLockData lock_data;
        
        private bool data_modified;
        private bool call_callingFrom = false;
        private bool data_saved = false;

        public frmTabAnnualBeaTCUpdate()
        {
            InitializeComponent();
        }

        private void frmTabAnnualBeaTCUpdate_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //update lock
            lock_data = new AnnLockData();
            if (Editable)
                lock_data.UpdateTabLock(SelectedTc, true);
            
            lblTitle.Text = "UTILITIES " + SelectedYear + " PROJECTS";
            if (Editable)
                lblTC.Text = SelectedTc;
            else
                lblTC.Text = SelectedTc + "  Read Only";

            //get data
            GetProjectData();

            //set up Save button
            btnSave.Enabled = false;
            data_modified = false;

            if (!Editable)
                btnRefresh.Enabled = false;
        }

        //Get project Data and display it
        private void GetProjectData()
        {
            dtProj = data_object.GetTabAnnualBeaTCProjects(SelectedTc, SelectedYear);
            dgData.DataSource = dtProj;
            
            setItemColumnHeader();
        }

        private void setItemColumnHeader()
        {
            dgData.Columns[0].ReadOnly = true;
            dgData.Columns[0].HeaderText = "ID ";
            dgData.Columns[0].Width = 64;
            dgData.Columns[0].Frozen = true;
            dgData.Columns[1].HeaderText = "Newtc ";
            dgData.Columns[1].Width = 60;
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dgData.Columns[1];
            cvip.MaxInputLength = 4;
            if (!Editable)
                dgData.Columns[1].ReadOnly = true;
          
            for (int i = 2; i <= 25; i++)
            {
                dgData.Columns[i].ReadOnly = true;
                if (i % 2 == 0)
                {
                    if (i / 2 <= 9)
                        dgData.Columns[i].HeaderText = SelectedYear + "0" + (i / 2).ToString();
                    else
                        dgData.Columns[i].HeaderText = SelectedYear + (i / 2).ToString();

                    dgData.Columns[i].DefaultCellStyle.Format = "N0";
                    dgData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgData.Columns[i].Width = 64;  
                }
                else
                {
                    dgData.Columns[i].HeaderText = "F";
                    dgData.Columns[i].Width = 20;
                }
            }

            dgData.Columns[26].HeaderText = "Strtdate ";
            dgData.Columns[26].Width = 80;
            dgData.Columns[26].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[26].ReadOnly = true;

            dgData.Columns[27].HeaderText = "Compdate ";
            dgData.Columns[27].Width = 80;
            dgData.Columns[27].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[27].ReadOnly = true;

            dgData.Columns[28].HeaderText = "Rvitm5c";
            dgData.Columns[28].Width = 80;
            dgData.Columns[28].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[28].ReadOnly = true;
            dgData.Columns[28].DefaultCellStyle.Format = "N0";

            dgData.Columns[29].HeaderText = "Item6";
            dgData.Columns[29].Width = 80;
            dgData.Columns[29].DefaultCellStyle.Format = "N0";
            dgData.Columns[29].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[29].ReadOnly = true;

            dgData.Columns[30].HeaderText = "Fwgt ";
            dgData.Columns[30].Width = 60;
            dgData.Columns[30].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[30].DefaultCellStyle.Format = "N2";
            dgData.Columns[30].ReadOnly = true;

            dgData.Columns[31].HeaderText = "Status";
            dgData.Columns[31].Width = 60;
            dgData.Columns[31].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[31].ReadOnly = true;

            //modified column
            dgData.Columns[32].Visible = false;
         
            lblCount.Text = dtProj.Rows.Count.ToString("N0");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //refresh project data and update data
            GetProjectData();
            btnSave.Enabled = false;
            data_modified = false;
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (data_modified)
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!data_modified)
                return;

            SaveData();

            //set buttom focus
            GetProjectData();
            btnSave.Enabled = false;
            
            MessageBox.Show("Save Data completed.");
        }

        private void SaveData()
        {
            //update Vip annual update table
            if (data_modified)
            {
                //update Annual proj table
                data_object.UpdateAnnProjForTC(dtProj);
                
                //initial modify column in case table
                for (int i = 0; i < dtProj.Rows.Count; i++)
                    dtProj.Rows[i][32] = false;
                data_modified = false;
                data_saved = true;
            }
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (data_modified)
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }
            if (Editable)
                lock_data.UpdateTabLock(SelectedTc, false);

            if (CallingForm != null)
            {
                CallingForm.Show();
                if (data_saved)
                    CallingForm.RefreshForm(true);
                call_callingFrom = true;
            }

            this.Close();
        }

        private static KeyPressEventHandler NumbericCheckHandler = new KeyPressEventHandler(NumbericCheck);
        private static void NumbericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dgData_Enter(object sender, EventArgs e)
        {
            if (dgData.Rows.Count == 0)
                return;
        }

        private string oldvalue;
        private bool isvalid;
        private Int32 currentRow;
        private Int32 currentCell;
        private bool resetRow = false;
        private void dgData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            isvalid = false;
            if (e.ColumnIndex ==1 && dgData.Columns[e.ColumnIndex].ReadOnly == false)
            {
                string new_tc = e.FormattedValue.ToString();

                //Check newtc
                if (GeneralDataFuctions.CheckNewTC(new_tc))
                {
                    if (new_tc.Substring(0, 2) == "09" || new_tc.Substring(0, 2) == "11" || new_tc.Substring(0, 2) == "16" || new_tc.Substring(0, 2) == "19")
                        isvalid = true;
                }
                    
                oldvalue = dgData[e.ColumnIndex, e.RowIndex].Value.ToString();
            }
        }

        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgData[e.ColumnIndex, e.RowIndex].Value.ToString() == oldvalue)
                return;

            if (!isvalid)
            {
                MessageBox.Show("Newtc is invalid");
                dgData[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                return;
            }

            //update tables. 
            if (!dgData.Columns[e.ColumnIndex].ReadOnly)
            {
                data_modified = true;
                btnSave.Enabled = true;

                //update modified column
                dgData[32, e.RowIndex].Value = 1;

                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;
            }
        }

        private void dgData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            int colIndex = dgData.CurrentCell.ColumnIndex;
            if (!dgData.Columns[colIndex].ReadOnly)
            {
                e.Control.KeyPress -= NumbericCheckHandler;
                e.Control.KeyPress += NumbericCheckHandler;
            }
        }

       
        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                dgData.CurrentCell = dgData.Rows[currentRow].Cells[currentCell];
            }
        }

        //Verify Form Closing from menu
        public override bool VerifyFormClosing()
        {
            bool can_close = true;

            if (data_modified )
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                {
                    can_close = false;
                }
                else
                {
                    lock_data.UpdateTabLock(SelectedTc, false);
                }
            }
          
            return can_close;
        }

        private void frmTabAnnualBeaTCUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Editable)
                lock_data.UpdateTabLock(SelectedTc, false);

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

       
    }
}
