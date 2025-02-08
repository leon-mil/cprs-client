/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmDCPDup.cs	    	
Programmer:         Diane Musachio
Creation Date:      3/15/2018
Inputs:             None
Parameters:	        None 
Outputs:	       	None
Description:	    Display screen to display Potential DCP Duplicate Cases
Detailed Design:    None 
Other:	            Called by: Main form -> Data Entry -> DCP Duplicate Research
Revision History:	
****************************************************************************************
 Modified Date :  5/11/2020
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR 4230
 Description   :  Add DELCOD column
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
    public partial class frmDCPDup : frmCprsParent
    {
        public frmDCPDup()
        {
            InitializeComponent();
        }

        private DcpDupData data_object;
        private UnlockData unlock_data;
        private string locked_by;
        private string fin;
        private string keep;
        private int selindex=0;
        private delegate void ShowLockMessageDelegate();
        private bool editable = false;

        DataTable dt = new DataTable();

        private void frmDCPDUP_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("DATA ENTRY");

            data_object = new DcpDupData();

            GetData();

            //check to see if locked by another user
            CheckLock();
           
            //if no rows then all buttons are disabled
            if (dt.Rows.Count == 0)
            {
                btnPrint.Enabled = false;
                btnDup.Enabled = false;
                btnUnmark.Enabled = false;
                btnValid.Enabled = false;
            }

            setupColumnHeader(dgData);
        }

        //update datagrid with latest data
        private void GetData()
        {
            dt = data_object.GetResearchData();
            dgData.DataSource = dt;
            txtTotal.Text = dgData.RowCount.ToString();
        }

        //check to see if locked by another user
        private void CheckLock()
        {
            if (dgData.SelectedRows.Count > 0)
            {
                locked_by = dgData.Rows[0].Cells["LOCKID"].Value.ToString().Trim();

                if (locked_by != "")
                {
                    btnDup.Enabled = false;
                    btnUnmark.Enabled = false;
                    btnValid.Enabled = false;
                    btnPrint.Enabled = true;
                    editable = false;
                    lblLockedBy.Visible = true;
                }
                else
                {
                    //update lock in dbo.RESEARCH for all cases
                    data_object.UpdateLock(UserInfo.UserName);
                    editable = true;
                    lblLockedBy.Visible = false;

                    //will enable/disable buttons based on selected value
                    if (keep == "N")
                    {
                        btnDup.Enabled = false;
                        btnValid.Enabled = true;
                        btnUnmark.Enabled = true;
                        btnPrint.Enabled = true;
                    }
                    else if (keep == "Y")
                    {
                        btnValid.Enabled = false;
                        btnDup.Enabled = true;
                        btnUnmark.Enabled = true;
                        btnPrint.Enabled = true;
                    }
                    else
                    {
                        btnDup.Enabled = true;
                        btnUnmark.Enabled = false;
                        btnValid.Enabled = true;
                        btnPrint.Enabled = true;
                    }
                }

                BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
            }
        }

        //get data for fin from master datatable
        private void GetMasterData(string fin)
        {
            DataTable dm = new DataTable();

            //get master data
            dm = data_object.GetMasterRecord(fin);
            dgMaster.DataSource = dm;

            setupColumnHeader(dgMaster);

            //set selected to false so row is not highlighted
            dgMaster.Rows[0].Selected = false;
        }

        private void setupColumnHeader(DataGridView dg)
        {
            dg.Columns[0].HeaderText = "FIN";
            dg.Columns[0].Width = 105;
            dg.Columns[1].HeaderText = "FIPSTATE";
            dg.Columns[1].Width = 70;
            dg.Columns[2].HeaderText = "COUNTY";
            dg.Columns[2].Width = 65;
            dg.Columns[3].HeaderText = "OWNER";
            dg.Columns[3].Width = 60;
            dg.Columns[4].HeaderText = "PROJSELV";
            dg.Columns[4].Width = 80;
            dg.Columns[5].HeaderText = "TVALUE";
            dg.Columns[5].Width = 80;
            dg.Columns[6].HeaderText = "NEWTC";
            dg.Columns[6].Width = 65;
            dg.Columns[7].HeaderText = "SELDATE";
            dg.Columns[7].Width = 80;
            dg.Columns[8].HeaderText = "MASTERRPT";
            dg.Columns[8].Width = 110;
            dg.Columns[9].HeaderText = "MORE to FOLLOW";
            dg.Columns[9].Width = 80;
            dg.Columns[10].HeaderText = "DDA START DATE";
            dg.Columns[10].Width = 80;
            dg.Columns[11].HeaderText = "DDA SEQNUM";
            dg.Columns[11].Width = 80;

            if (dg == dgData)
            {
                dg.Columns[12].HeaderText = "DELCOD";
                dg.Columns[12].Width = 60;
                dg.Columns[13].HeaderText = "SAMP";
                dg.Columns[13].Width = 52;
                dg.Columns[14].HeaderText = "KEEP";
                dg.Columns[14].Width = 52;
                dg.Columns[15].Visible = false;
            }

            else
            {
                /*dm03162020 added else so dgData did not get unsortable*/
                //make columns unsortable
                foreach (DataGridViewColumn dgvc in dg.Columns)
               {
                  dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
               }
            }
        }

        //show lock message
        private void ShowLockMessage()
        {
            /*show message if the case locked by someone */
            if (locked_by != "") 
                MessageBox.Show("DCP Duplication Research is in use by " + locked_by + ".");
        }

        private void dgData_SelectionChanged(Object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count > 0)
            {
                dgData.CurrentRow.Selected = true;

                //get index for selected row
                    selindex = dgData.CurrentCell.RowIndex;

                keep = dgData.CurrentRow.Cells[14].Value.ToString().Trim();

                fin = dgData.CurrentRow.Cells[0].Value.ToString().Trim();

                GetMasterData(fin);

                if (editable)
                {
                    //will enable/disable buttons based on selected value
                    if (keep == "N")
                    {
                        btnDup.Enabled = false;
                        btnValid.Enabled = true;
                        btnUnmark.Enabled = true;
                        btnPrint.Enabled = true;
                    }
                    else if (keep == "Y")
                    {
                        btnValid.Enabled = false;
                        btnDup.Enabled = true;
                        btnUnmark.Enabled = true;
                        btnPrint.Enabled = true;
                    }
                    else
                    {
                        btnDup.Enabled = true;
                        btnUnmark.Enabled = false;
                        btnValid.Enabled = true;
                        btnPrint.Enabled = true;
                    }
                }              
            }         
        }

        //mark case as a duplicate by updating keep field to 'N'
        private void btnDup_Click(object sender, EventArgs e)
        { 
            //get index for selected row
            selindex = dgData.CurrentCell.RowIndex;

            dgData.Rows[selindex].Cells[14].Value = "N";

            //update value of keep
            data_object.UpdateKeep(fin, "N");

            //enable/disable buttons
            btnDup.Enabled = false;
            btnValid.Enabled = true;
            btnUnmark.Enabled = true;
        }

        //mark case as valid by updating keep field to 'Y'
        private void btnValid_Click(object sender, EventArgs e)
        {
            //get index for selected row
            selindex = dgData.CurrentCell.RowIndex;
            dgData.Rows[selindex].Cells[14].Value = "Y";

            //update value of keep
            data_object.UpdateKeep(fin, "Y");

            //enable/disable buttons
            btnValid.Enabled = false;
            btnDup.Enabled = true;
            btnUnmark.Enabled = true;
        }

        //unmark case by updating keep field to ' '
        private void btnUnmark_Click(object sender, EventArgs e)
        {
            //get index for selected row
            selindex = dgData.CurrentCell.RowIndex;

            //update value of keep
            dgData.Rows[selindex].Cells[14].Value = "";
            data_object.UpdateKeep(fin, "");

            //enable/disable buttons
            btnDup.Enabled = true;
            btnValid.Enabled = true;
            btnUnmark.Enabled = false;
        }

        //bind selection to current selected index to override first row highlighting by default
        private void dgData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!String.IsNullOrEmpty(fin) && e.ListChangedType == ListChangedType.Reset)
            {
                dgData.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (dgData.DataSource != null)
                    {
                        BindingContext[dgData.DataSource].Position = selindex;
                    }
                });
            }
        }

        //if form closing unlock research userinfo
        private void frmDCPDup_FormClosing(object sender, FormClosingEventArgs e)
        {
            unlock_data = new UnlockData();

            /*unlock user*/
            if (editable)
            {
                unlock_data.UpdateDupResearchLock(UserInfo.UserName);
            }

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");
        }

        //print datagrid
        private void btnPrint_Click(object sender, EventArgs e)
        {
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
            printer.Title = "DUPLICATION RESEARCH";
            
            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
  
            dgPrint.Columns[0].HeaderText = "FIN";
            dgPrint.Columns[0].Width = 90;
            dgPrint.Columns[1].HeaderText = "FIPSTATE";
            dgPrint.Columns[1].Width = 60;
            dgPrint.Columns[2].HeaderText = "COUNTY";
            dgPrint.Columns[2].Width = 60;
            dgPrint.Columns[3].HeaderText = "OWNER";
            dgPrint.Columns[3].Width = 60;
            dgPrint.Columns[4].HeaderText = "PROJSELV";
            dgPrint.Columns[4].Width = 70;
            dgPrint.Columns[5].HeaderText = "TVALUE";
            dgPrint.Columns[5].Width = 70;
            dgPrint.Columns[6].HeaderText = "NEWTC";
            dgPrint.Columns[6].Width = 60;
            dgPrint.Columns[7].HeaderText = "SELDATE";
            dgPrint.Columns[7].Width = 60;
            dgPrint.Columns[8].HeaderText = "MASTERRPT";
            dgPrint.Columns[8].Width = 80;
            dgPrint.Columns[9].HeaderText = "MORE to FOLLOW";
            dgPrint.Columns[9].Width = 60;
            dgPrint.Columns[10].HeaderText = "DDA START DATE";
            dgPrint.Columns[10].Width = 70;
            dgPrint.Columns[11].HeaderText = "DDA SEQNUM";
            dgPrint.Columns[11].Width = 70;
            dgPrint.Columns[12].HeaderText = "DELCOD";
            dgPrint.Columns[12].Width = 40;
            dgPrint.Columns[13].HeaderText = "SAMP";
            dgPrint.Columns[13].Width = 40;
            dgPrint.Columns[14].HeaderText = "KEEP";
            dgPrint.Columns[14].Width = 40;
            dgPrint.Columns[15].Visible = false;

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Duplication Research Print";
            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;
            printer.Footer = " ";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.PrintDataGridViewWithoutDialog(dgPrint);

            Cursor.Current = Cursors.Default;
        }

        private void btnDup_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnValid_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnUnmark_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}


