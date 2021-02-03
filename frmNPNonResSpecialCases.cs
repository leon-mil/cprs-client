/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System
 Program Name  : frmNPNonResSpecialCases.cs
 Programmer    : Diane Musachio
 Creation Date : 11/8/2017
 Inputs        : N/A
 Parameters    : N/A 
 Output        : N/A
 Description   : This program will display Non Permit Non Residential
              Cases as identified by Survey of Construction
 Detail Design : Detailed User Requirements for NP Special Cases
 Other         : Called by: menu -> data entry -> NP Special Case
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
using CprsDAL;
using CprsBLL;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Threading;

namespace Cprs
{
    public partial class frmNPNonResSpecialCases : frmCprsParent
    {
        NPSpecialCases data_object = new NPSpecialCases();
        private string fin1;

        public frmNPNonResSpecialCases()
        {
            InitializeComponent();
        }

        private void frmNPNonResSpecialCases_Load(object sender, EventArgs e)
        {
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("DATA ENTRY");

            // If the user is HQManager or Programmer or Analyst then buttons are available - otherwise disabled
            if ((UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer
               || UserInfo.GroupCode == EnumGroups.HQAnalyst))
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }

            GetData();
        }

        private void GetData()
        {
            DataTable dt = data_object.GetSpecialCases();
            dgData.DataSource = dt;
            dgPrint.DataSource = dt;
            dgData.ReadOnly = true;

            dgData.ColumnHeadersHeight = 50;
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomCenter;
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[0].Width = 160;

            dgData.Columns[0].HeaderText = "FIN";
            dgData.Columns[1].HeaderText = "PSU";
            dgData.Columns[2].HeaderText = "BPOID";
            dgData.Columns[3].HeaderText = "SCHED";
            dgData.Columns[4].HeaderText = "SELDATE";
            dgData.Columns[5].HeaderText = "OWNER";
            dgData.Columns[6].HeaderText = "FIPSTATE";
            dgData.Columns[7].HeaderText = "NEWTC";
            dgData.Columns[8].HeaderText = "SOURCE";
            dgData.Columns[9].HeaderText = "PROJSELV";
            dgData.Columns[10].HeaderText = "FWGT";

            if (dgData.RowCount == 0)
            {
                btnDelete.Enabled = false;
                btnEdit.Enabled = false;
                btnPrint.Enabled = false;
            }

            foreach (DataGridViewColumn column in dgData.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmNPNonResSpecialNA fp = new frmNPNonResSpecialNA();

            this.Hide(); // hide parent

            int index = dgData.CurrentRow.Index;

            fp.fin = fin1;

            List<string> finlist = new List<string>();

            int cnt = 0;
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                string val = dgData["FIN", cnt].Value.ToString();
                finlist.Add(val);
                cnt = cnt + 1;
            }

            fp.Finlist = finlist;
            fp.CurrIndex = index;
            fp.CallingForm = this;
            fp.Show();
            
        }

        //reload data
        public void RefreshData()
        {
            GetData();
        }

        //Clicking on delete button prompts user to verify if they want to delete selected row
        private void btnDelete_Click(object sender, EventArgs e)
        {
            string lockedby = data_object.ChkLocked(fin1).ToString();

            if (lockedby != "")
            {
                MessageBox.Show("Selected case is locked by " + lockedby);
            }

            if (data_object.ChkLocked(fin1).ToString() == "")
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected case?",
                    "Question", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    data_object.DeleteSpecCaseData(fin1);
                    GetData();
                }
            }      
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "NP Special Cases";

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PageSettings.Landscape = true;
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "NP Special Cases Print";
            printer.Userinfo = UserInfo.UserName;
            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.RowHeadersVisible = false;
            dgPrint.Columns[0].HeaderText = "FIN";
            dgPrint.Columns[1].HeaderText = "PSU";
            dgPrint.Columns[2].HeaderText = "BPOID";
            dgPrint.Columns[3].HeaderText = "SCHED";
            dgPrint.Columns[4].HeaderText = "SELDATE";
            dgPrint.Columns[5].HeaderText = "OWNER";
            dgPrint.Columns[6].HeaderText = "FIPSTATE";
            dgPrint.Columns[7].HeaderText = "NEWTC";
            dgPrint.Columns[8].HeaderText = "SOURCE";
            dgPrint.Columns[9].HeaderText = "PROJSELV";
            dgPrint.Columns[10].HeaderText = "FWGT";

            foreach (DataGridViewColumn c in dgPrint.Columns)
            {
                c.Width = 80;
            }

            dgPrint.Columns[0].Width = 180;
           
            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgPrint);
        }

        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count > 0)
            {
                fin1 = dgData.SelectedRows[0].Cells[0].Value.ToString().Trim();
            }
        }

        private void frmNPNonResSpecialCases_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");
        }
    }
}
