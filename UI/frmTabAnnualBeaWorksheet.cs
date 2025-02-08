/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabAnnualBeaWorksheet.cs
Programmer    : Christine Zhang
Creation Date : March. 26 2019
Parameters    : 
Inputs        : SelectedTc, SelectedSurvey, SelectedYear, Editable, Callingform
Outputs       : N/A
Description   : create annual bea worksheet screen to view annual vip data
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
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmTabAnnualBeaWorksheet : frmCprsParent 
    {
        public frmTabAnnualBeaWorksheet()
        {
            InitializeComponent();
        }

        public frmTabAnnualBea CallingForm = null;
        public string SelectedTc;
        public string SelectedSurvey;
        public string SelectedYear;
        public bool Editable = true;

        private TabAnnualBeaData data_object = new TabAnnualBeaData();
        private DataTable dtProj;
        private DataTable dtUpd;
        private DataTable dtSaveUpd;
        private AnnLockData lock_data;
        private DataTable clonetable;
        private bool data_modified;
        private bool isApply;
       
        private string last_focused;
        
        private bool call_callingFrom = false;
        private bool data_saved = false;
        private void frmTabAnnualBeaWorksheet_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //update lock
            lock_data = new AnnLockData();
            if (Editable)
            {
                lock_data.UpdateTabLock(SelectedTc, true);
            }

            //setup title
            if (SelectedSurvey == "P")
                lblTitle1.Text = "STATE AND LOCAL " + SelectedYear + " Worksheet";
            else if (SelectedSurvey == "F")
                lblTitle1.Text = "FEDERAL " + SelectedYear + " Worksheet";
            else if (SelectedSurvey == "N")
                lblTitle1.Text = "NONRESIDENTIAL " + SelectedYear + " Worksheet";
            else if (SelectedSurvey == "M")
                lblTitle1.Text = "MULTIFAMILY " + SelectedYear + " Worksheet";
            else 
                lblTitle1.Text = "Utilities " + SelectedYear + " Worksheet";

            lblTc.Text = "TC: " + SelectedTc;

            //get data
            GetProjectData();
            GetTotalData();
            GetUpdatedData();

            //set up Save button, if editable and user is hq manager or programmer
            btnSave.Enabled = false;
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
                btnSave.Enabled = Editable;

            //set up readonly label
            lblReadOnly.Visible = !Editable;

            //set up search item
            txtValueItem.Visible = false;

            data_modified = false;
            isApply = false;
        }

        private void GetTotalData()
        {
            DataTable dtlsf = data_object.GetRelatedLSF(SelectedYear, SelectedSurvey, SelectedTc);
            DataTable dtbst = data_object.GetRelatedBST(SelectedYear, SelectedSurvey, SelectedTc);
            DataTable dtmain = data_object.BuildTopTable(SelectedSurvey, dtProj, dtlsf, dtbst);

            dgTot.DataSource = dtmain;
            dgTot.Columns[0].HeaderText = "Type of Construction";
            dgTot.Columns[0].Width = 160;
            dgTot.Columns[1].HeaderText = SelectedYear + "01";
            dgTot.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[1].Width = 85;
            dgTot.Columns[2].HeaderText = SelectedYear + "02";
            dgTot.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[2].Width = 85;
            dgTot.Columns[3].HeaderText = SelectedYear + "03";
            dgTot.Columns[3].Width = 85;
            dgTot.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[4].HeaderText = SelectedYear + "04";
            dgTot.Columns[4].Width = 85;
            dgTot.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[5].HeaderText = SelectedYear + "05";
            dgTot.Columns[5].Width = 85;
            dgTot.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[6].HeaderText = SelectedYear + "06";
            dgTot.Columns[6].Width = 85;
            dgTot.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[7].HeaderText = SelectedYear + "07";
            dgTot.Columns[7].Width = 85;
            dgTot.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[8].HeaderText = SelectedYear + "08";
            dgTot.Columns[8].Width = 85;
            dgTot.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[9].HeaderText = SelectedYear + "09";
            dgTot.Columns[9].Width = 85;
            dgTot.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[10].HeaderText = SelectedYear + "10";
            dgTot.Columns[10].Width = 85;
            dgTot.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[11].HeaderText = SelectedYear + "11";
            dgTot.Columns[11].Width = 85;
            dgTot.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[12].HeaderText = SelectedYear + "12";
            dgTot.Columns[12].Width = 85;
            dgTot.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgTot.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //Get project Data and display it
        private void GetProjectData()
        {
            dtProj = data_object.GetTabAnnualBeaDataWorksheetProjects(SelectedYear, SelectedSurvey, SelectedTc);
            dgData.DataSource = dtProj;
            //save table
            clonetable = dtProj.Clone();

            setItemColumnHeader();
        }

        private void setItemColumnHeader()
         {
            dgData.Columns[0].HeaderText = "ID ";
            dgData.Columns[0].Width = 64;
            dgData.Columns[0].ReadOnly = true;
            dgData.Columns[0].Frozen = true;
            dgData.Columns[1].HeaderText = "Newtc ";
            dgData.Columns[1].Width = 60;
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[1].ReadOnly = true;

            for (int i = 2; i <= 25; i++)
            {
                if (i % 2 == 0)
                {
                    if (i / 2 <= 9)
                        dgData.Columns[i].HeaderText = SelectedYear + "0" + (i / 2).ToString();
                    else
                        dgData.Columns[i].HeaderText = SelectedYear + (i / 2).ToString();

                    dgData.Columns[i].DefaultCellStyle.Format = "N0";
                    dgData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgData.Columns[i].Width = 64;
                    DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dgData.Columns[i];//here index of column
                    cvip.MaxInputLength = 6;
                }
                else
                {
                    dgData.Columns[i].HeaderText = "F";
                    dgData.Columns[i].Width = 20;
                    dgData.Columns[i].ReadOnly = true;
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
            dgData.Columns[31].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[31].ReadOnly = true;
            dgData.Columns[30].DefaultCellStyle.Format = "N2";
            dgData.Columns[30].ReadOnly = true;

            dgData.Columns[31].HeaderText = "Status";
            dgData.Columns[31].Width = 60;

            dgData.Columns[32].HeaderText = "Owner ";
            dgData.Columns[32].Visible = false;
            dgData.Columns[32].ReadOnly = true;

            dgData.Columns[33].HeaderText = "Cumvip";
            dgData.Columns[33].Visible = false;
            dgData.Columns[33].DefaultCellStyle.Format = "N0";
            dgData.Columns[33].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[33].ReadOnly = true;

            //modified column
            dgData.Columns[34].Visible = false;
            dgData.Columns[34].ReadOnly = true;

            lblCases.Text = dtProj.Rows.Count.ToString("N0");

          //  dgData.ClearSelection();
        }

        private void GetUpdatedData()
        {
            dtUpd = data_object.GetTabAnnualBeaDataWorksheetUpdated(SelectedYear, SelectedSurvey, SelectedTc);
            dgUpdate.DataSource = dtUpd;
            dtSaveUpd = dtUpd.Clone();

            dgUpdate.Columns[0].HeaderText = "ID ";
            dgUpdate.Columns[0].Width = 80;
            dgUpdate.Columns[0].ReadOnly = true;
            dgUpdate.Columns[1].HeaderText = "Newtc ";
            dgUpdate.Columns[1].Width = 70;
            dgUpdate.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgUpdate.Columns[2].HeaderText = "Date6 ";
            dgUpdate.Columns[2].Width = 70;
            dgUpdate.Columns[3].HeaderText = "Wgtvip ";
            dgUpdate.Columns[3].Width = 100;
            dgUpdate.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgUpdate.Columns[3].DefaultCellStyle.Format = "N0";
            dgUpdate.Columns[4].HeaderText = "Vipflag ";
            dgUpdate.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgUpdate.Columns[4].Width = 80;
            dgUpdate.Columns[5].HeaderText = "Change ";
            dgUpdate.Columns[5].Width = 100;
            dgUpdate.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgUpdate.Columns[5].DefaultCellStyle.Format = "N0";
            dgUpdate.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgUpdate.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgUpdate.ClearSelection();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (Editable && data_modified && btnSave.Enabled)
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }
            if (!Editable && data_modified)
            {
                var result = MessageBox.Show("Changes were made. Do you want to Print?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                    printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
                    printDocument1.DefaultPageSettings.Landscape = true;
                    memoryImage = GeneralFunctions.CaptureScreen(this);
                    printDocument1.Print();
                }
            }

            if (Editable)
                lock_data.UpdateTabLock(SelectedTc, false);

            if (CallingForm != null)
            {
                CallingForm.Show();
                if (Editable && data_saved)
                  CallingForm.RefreshForm(true);
                call_callingFrom = true;
            }

            this.Close();    
        }

        //Verify Form Closing from menu
        public override bool VerifyFormClosing()
        {
            bool can_close = true;

            if (Editable)
            {
                if (data_modified && btnSave.Enabled)
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
            }
            else
            {
                if (data_modified)
                {
                    var result = MessageBox.Show("Changes were made. Do you want to Print?", "Confirm", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                        printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
                        printDocument1.DefaultPageSettings.Landscape = true;
                        memoryImage = GeneralFunctions.CaptureScreen(this);
                        printDocument1.Print();
                    }
                }
            }

            return can_close;
        }

        private void frmTabAnnualBeaWorksheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Editable)
                lock_data.UpdateTabLock(SelectedTc, false);

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void dgTot_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (e.ColumnIndex != 1)
            {
                if (e.RowIndex == 1 || e.RowIndex == 3)
                    dgTot.Rows[e.RowIndex].DefaultCellStyle.Format = "N2";
                else if (e.RowIndex == 5)
                    dgTot.Rows[e.RowIndex].DefaultCellStyle.Format = "N3";
                else
                    dgTot.Rows[e.RowIndex].DefaultCellStyle.Format = "N0";
            }
        }

        private static KeyPressEventHandler NumbericCheckHandler = new KeyPressEventHandler(NumbericCheck);
        private static void NumbericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgData.Columns[e.ColumnIndex].ReadOnly == false)
                {
                    string flag = dgData[e.ColumnIndex + 1, e.RowIndex].Value.ToString();

                    //Check flag,if flag is empty, not allow editable
                    if (flag == " ")
                    {
                        e.Cancel = true;
                        dgData.RefreshEdit();
                    }
                }
            }
        }

        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgData[e.ColumnIndex, e.RowIndex].Value.ToString() == "")
            {
                dgData[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                return;
            }

            if (Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) == oldvalue)
                return;

            if (!isvalid)
            {
                dgData[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                return;
            }

            //update tables. 
            if (!dgData.Columns[e.ColumnIndex].ReadOnly)
            {
                data_modified = true;

                //update cumvip 
                dgData[33, e.RowIndex].Value = Convert.ToInt32(dgData[33, e.RowIndex].Value) + (Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) - oldvalue);

                //update modified column
                dgData[34, e.RowIndex].Value = 1;

                //find the case in update table
                IEnumerable<DataRow> query;
                query = from myRow in dtSaveUpd.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString() && myRow.Field<string>("Date6") == dgData.Columns[e.ColumnIndex].HeaderText
                        select myRow;

                if (query.Count() > 0)
                {
                    //update row value
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    dtSaveUpd.AcceptChanges();
                }
                else
                {
                    DataRow newRow = dtSaveUpd.NewRow();

                    newRow["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow["WGTVIP"] = Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value);
                    newRow["VIPFlag"] = dgData[e.ColumnIndex + 1, e.RowIndex].Value.ToString();
                    newRow["WGTDIF"] = Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) - oldvalue;
                    newRow["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    newRow["DATE6"] = dgData.Columns[e.ColumnIndex].HeaderText;
                    dtSaveUpd.Rows.Add(newRow);
                }


                //find the case in update table
                query = from myRow in dtUpd.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString() && myRow.Field<string>("Date6") == dgData.Columns[e.ColumnIndex].HeaderText
                        select myRow;

                if (query.Count() > 0)
                {
                    //update row value
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    dtUpd.AcceptChanges();
                    dgUpdate.DataSource = dtUpd;
                }
                else
                {
                    DataRow newRow1 = dtUpd.NewRow();

                    newRow1["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow1["WGTVIP"] = Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value);
                    newRow1["VIPFlag"] = dgData[e.ColumnIndex+1, e.RowIndex].Value.ToString();
                    newRow1["WGTDIF"] = Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) - oldvalue;
                    newRow1["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    newRow1["DATE6"] = dgData.Columns[e.ColumnIndex].HeaderText;
                   
                    dtUpd.Rows.Add(newRow1);
                    DataView dv = dtUpd.DefaultView;
                    dv.Sort = "id";
                    dgUpdate.DataSource = dv.ToTable();
                }
                dgUpdate.ClearSelection();
              
                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;
            }
        }

        private void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
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

        private int oldvalue;
        private bool isvalid;
        private Int32 currentRow;
        private Int32 currentCell;
        private bool resetRow = false;
        private void dgData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            isvalid = false;
            if (dgData.Columns[e.ColumnIndex].ReadOnly == false)
            {
                string flag = dgData[e.ColumnIndex + 1, e.RowIndex].Value.ToString();

                //Check flag
                if (flag != " ")
                {
                    isvalid = true;
                    oldvalue = Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value);
                }
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //refresh project data and update data
            GetProjectData();
            GetUpdatedData();
            GetTotalData();
            data_modified = false;
            isApply = false;
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            DataTable pdata = dgData.DataSource as DataTable;
            if (pdata.Rows.Count != dtProj.Rows.Count)
            {
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    if (Convert.ToInt16(dr.Cells[34].Value) == 1)
                    {
                        string id = dr.Cells[0].Value.ToString();

                        //update dtProj
                        foreach (DataRow row in dtProj.Rows)
                        {
                            if (row["id"].ToString() == id)
                            {
                                row["c1"] = dr.Cells["c1"].Value;
                                row["c2"] = dr.Cells["c2"].Value;
                                row["c3"] = dr.Cells["c3"].Value;
                                row["c4"] = dr.Cells["c4"].Value;
                                row["c5"] = dr.Cells["c5"].Value;
                                row["c6"] = dr.Cells["c6"].Value;
                                row["c7"] = dr.Cells["c7"].Value;
                                row["c8"] = dr.Cells["c8"].Value;
                                row["c9"] = dr.Cells["c9"].Value;
                                row["c10"] = dr.Cells["c10"].Value;
                                row["c11"] = dr.Cells["c11"].Value;
                                row["c12"] = dr.Cells["c12"].Value;
                                row["modified"] = 1;
                            }
                        }
                    }
                }              
            }

            //apply new project data
            GetTotalData();
            isApply = true;
        }
       
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!data_modified)
                return;

            if (!isApply)
            {
                MessageBox.Show("You must apply changes before saving data to tables");
                return;
            }
            SaveData();

            //set buttom focus
            dgData.ClearSelection();
            dgUpdate.ClearSelection();
            dgData.Rows[0].Cells[0].Selected = true;

            MessageBox.Show("Save Data completed.");
        }

        private void SaveData()
        {
            
            if (data_modified)
            {
                //update Vip annual update table
                data_object.UpdateChangeData(SelectedSurvey, SelectedTc, SelectedYear, dtUpd);

                //update Annual proj table
                data_object.UpdateAnnProj(dtProj, SelectedYear);
                data_modified = false;
                isApply = false;

                //update Vipsttab/vipbea tables
                data_object.UpdateVipBeaData(SelectedSurvey, SelectedTc, dtSaveUpd);

                //initial modify column in case table
                for (int i = 0; i < dtProj.Rows.Count; i++)
                    dtProj.Rows[i][34] = false;

                //remove all records from dtSaveUpd table
                while (dtSaveUpd.Rows.Count > 0)
                    dtSaveUpd.Rows[0].Delete();

                data_saved = true;
            }
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

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (Editable && (data_modified) && btnSave.Enabled )
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            int index = 0;
            string val1 = string.Empty;
            List<string> Idlist = new List<string>();

            //check current selected control
            if (last_focused == "dgData")
            {
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
            }
            else if (last_focused == "dgUpdate")
            {
                DataGridViewSelectedCellCollection rows = dgUpdate.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgUpdate["ID", index].Value.ToString();
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

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

        private void dgUpdate_Enter(object sender, EventArgs e)
        {
            if (dgUpdate.Rows.Count == 0)
                return;

            last_focused = dgUpdate.Name;
            dgData.ClearSelection();
        }

        private void dgData_Enter(object sender, EventArgs e)
        {
            if (dgData.Rows.Count == 0)
                return;

            last_focused = dgData.Name;
            dgUpdate.ClearSelection();
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
            else if (colname == "NEWTC")
            {
                if (cvalue.Length != 4 || !GeneralDataFuctions.CheckNewTC(cvalue))
                {
                    MessageBox.Show("Invalid Newtc");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }
            else
            {
                if (cvalue.Length != 6 || !GeneralFunctions.ValidateDateWithRange(cvalue))
                {
                    MessageBox.Show("Invalid Strtdate");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }

            query = from myRow in dtProj.AsEnumerable()
                    where myRow.Field<string>(colname).StartsWith(cvalue)
                    orderby myRow.Field<string>(colname) descending
                    select myRow;

            // Create a table from the query.
            if (query.Count() > 0)
            {
                DataTable boundTable = query.CopyToDataTable<DataRow>();
                dgData.DataSource = boundTable;
                lblCases.Text = boundTable.Rows.Count.ToString("N0");
            }
            else
            {
                MessageBox.Show("No data exists");
                txtValueItem.Text = "";
                dgData.DataSource = clonetable;
                lblCases.Text = "0";
            }

            dgUpdate.ClearSelection();
        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            txtValueItem.Visible = false;
            txtValueItem.Text = "";

            DataTable pdata = dgData.DataSource as DataTable;
            if (pdata.Rows.Count != dtProj.Rows.Count)
            {
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    if (Convert.ToInt16(dr.Cells[34].Value) == 1)
                    {
                        string id = dr.Cells[0].Value.ToString();

                        //update dtProj
                        foreach (DataRow row in dtProj.Rows)
                        {
                            if (row["id"].ToString() == id)
                            {
                                row["c1"] = dr.Cells["c1"].Value;
                                row["c2"] = dr.Cells["c2"].Value;
                                row["c3"] = dr.Cells["c3"].Value;
                                row["c4"] = dr.Cells["c4"].Value;
                                row["c5"] = dr.Cells["c5"].Value;
                                row["c6"] = dr.Cells["c6"].Value;
                                row["c7"] = dr.Cells["c7"].Value;
                                row["c8"] = dr.Cells["c8"].Value;
                                row["c9"] = dr.Cells["c9"].Value;
                                row["c10"] = dr.Cells["c10"].Value;
                                row["c11"] = dr.Cells["c11"].Value;
                                row["c12"] = dr.Cells["c12"].Value;
                                row["modified"] = 1;
                            }
                        }
                    }
                }
            }

            //GetCaseData();
            dgData.DataSource = null;
            dgData.DataSource = dtProj;
            setItemColumnHeader();

            lblCases.Text = dtProj.Rows.Count.ToString("N0");
            cbItem.SelectedIndex = -1;
            dgUpdate.ClearSelection();
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValueItem.Visible = true;
            txtValueItem.Text = "";
            if (cbItem.SelectedIndex == 0)
                txtValueItem.MaxLength = 7;
            else if (cbItem.SelectedIndex == 1)
                txtValueItem.MaxLength = 4;
            else if (cbItem.SelectedIndex == 2)
                txtValueItem.MaxLength = 6;

            txtValueItem.Focus();
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

        private void btnSave_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
