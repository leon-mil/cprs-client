/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCenturionFailedVerif.cs	    	
Programmer:         Christine Zhang
Creation Date:      6/19/2019
Inputs:             None
Parameters:	        None 
Outputs:	        
Description:	    This screen display data for failed verification in centurion load program. 
Detailed Design:    None 
Other:	            Called by: Main form
 
Revision History:	
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
using System.Threading;


namespace Cprs
{
    public partial class frmCenturionFailedVerif : frmCprsParent
    {
        public frmCenturionFailedVerif()
        {
            InitializeComponent();
        }

        private MySector ms;
        private CenturionFailedVerifData data_object;
        private void frmCenturionFailedVerif_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            //get my sector data
            MySectorsData md = new MySectorsData();
            ms = md.GetMySectorData(UserInfo.UserName);
            if (ms == null)
                ckMySec.Visible = false;
            else
                ckMySec.Visible = true;

            data_object = new CenturionFailedVerifData();

            GetData();

        }

        private void GetData()
        {
            DataTable table = data_object.GetFailedVerifData();
            DataTable table1 = table.Clone();

            int num_row = table.Rows.Count;

            //get only my sector data, if not set in my sector, delete the sectors
            if (ckMySec.Checked)
            {
                for (int i = 0; i < num_row; i++)
                {
                    DataRow dr = table.Rows[i];

                    // do something with dr
                    if (ms.CheckInMySector(dr["NEWTC"].ToString()))
                        table1.ImportRow(dr);
                }

                dgData.DataSource = table1;
            }
            else
            {
                dgData.DataSource = table;
            }
            SetColumnHeader();
            lblCases.Text = dgData.Rows.Count.ToString() + " CASES";

        }

        private void SetColumnHeader()
        {
            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[0].Width = 80;
            
            dgData.Columns[1].HeaderText = "RESPID";
            dgData.Columns[1].Width = 80;
            
            dgData.Columns[2].HeaderText = "NEWTC";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[2].Width = 80;
            dgData.Columns[3].HeaderText = "OWNER";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[3].Width = 80;
            dgData.Columns[4].HeaderText = "FIELD";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[4].Width = 100;
            dgData.Columns[5].HeaderText = "VALUE";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[5].Width = 100;
            dgData.Columns[6].HeaderText = "REASON";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[6].Width = 210;
            
            dgData.Columns[7].HeaderText = "REVIEW";
            dgData.Columns[7].Width = 80;
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[8].HeaderText = "USER";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[8].Width = 80;
            dgData.Columns[9].HeaderText = "DATE/TIME";
            dgData.Columns[9].Width = 160;

        }

        private void ckMySec_CheckedChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            int index = dgData.CurrentRow.Index;

            this.Hide();         // hide parent

            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

            string val1 = dgData["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();

            int xcnt = 0;
            frmC700 fC700 = new frmC700();
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                string val = dr.Cells["ID"].Value.ToString();

                Idlist.Add(val);
                if (val == val1)
                {
                    fC700.CurrIndex = xcnt;
                }
                xcnt = xcnt + 1;
            }

            fC700.Id = val1;
            fC700.Idlist = Idlist;
            fC700.CallingForm = this;

            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

            fC700.ShowDialog();  // show child
        }
            
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DGVPrinter printer = new DGVPrinter();
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Failed Verification Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            dgData.Columns[0].Width = 60;
            dgData.Columns[1].Width = 60;
            dgData.Columns[2].Width = 60;
            dgData.Columns[3].Width = 60;
            dgData.Columns[4].Width = 80;
            dgData.Columns[5].Width = 80;
            dgData.Columns[6].Width = 200;
            dgData.Columns[7].Width = 60;
            dgData.Columns[9].Width = 140;
            printer.PrintDataGridViewWithoutDialog(dgData);

            dgData.Columns[0].Width = 80;
            dgData.Columns[1].Width = 80;
            dgData.Columns[2].Width = 80;
            dgData.Columns[3].Width = 80;
            dgData.Columns[4].Width = 100;
            dgData.Columns[5].Width = 100;
            dgData.Columns[6].Width = 210;
            dgData.Columns[7].Width = 80;
            dgData.Columns[8].Width = 80;
            dgData.Columns[9].Width = 160;

            Cursor.Current = Cursors.Default;
        }

        private void frmCenturionFailedVerif_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            int index = dgData.CurrentRow.Index;

            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

            //update screen
            dgData["REVIEW", index].Value = "Y";
            dgData["USRNME", index].Value = UserInfo.UserName;
            dgData["PRGDTM", index].Value = DateTime.Now;

            //update datatable
            string id = dgData["ID", index].Value.ToString();
            data_object.UpdateFailedVerifData(id);


        }
    }
}
