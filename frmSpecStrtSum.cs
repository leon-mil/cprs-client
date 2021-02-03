/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmSpecStrtSum.cs	    	
Programmer:         Christine Zhang
Creation Date:      9/5/2018
Inputs:             None                                   
Parameters:	        None              
Outputs:	        None
Description:	    This program displays the annual compare strtdate and seldate data
                    summary data 
Detailed Design:    Detailed Design for Spec strtdate

Other:	            
Revision Referrals:	
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;
namespace Cprs
{
    public partial class frmSpecStrtSum : frmCprsParent
    {
        public frmSpecStrt CallingForm = null;

        /*flag to use closing the form */
        private bool call_callingFrom = false;

        public frmSpecStrtSum()
        {
            InitializeComponent();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }

            this.Close();
        }


        private void frmSpecStrtSum_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            string selsurvey = string.Empty;
            string selvalue = string.Empty;

            string title2 = string.Empty;

            //Get selected survey
            if (rdAll.Checked)
            {
                selsurvey = "";
                title2 = rdAll.Text + " - ";
            }
            else if (rdPrivate.Checked)
            {
                selsurvey = "N";
                title2 = "Private - ";
            }
            else if (rdState.Checked)
            {
                selsurvey = "P";
                title2 = "State And Local - ";
            }
            else
            {
                selsurvey = "F";
                title2 = "Federal - ";
            }

            //Get selected value
            if (rdV1.Checked)
            {
                selvalue = "";
                title2 = title2 + rdV1.Text;
            }
            else if (rdV2.Checked)
            {
                selvalue = "1";
                title2 = title2 + rdV2.Text;
            }
           

            //Get current data
            SpecStrtData dataObject = new SpecStrtData();
            DataTable strtdate_table = dataObject.GetSpecStrtDateSumData(selsurvey, selvalue);
            dgData.DataSource = strtdate_table;

            label1.Text = title2;

            //set up column header
            SetColumnHeader();
        }

        private void SetColumnHeader()
        {
            dgData.Columns[0].HeaderText = "Category";
            dgData.Columns[1].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].DefaultCellStyle.Format = "0\\%";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            int cur_mon = Convert.ToInt16(sdate.Substring(4, 2));
            int cur_year = Convert.ToInt16(sdate.Substring(0, 4));

            if (cur_mon >= 6)
                dgData.Columns[3].HeaderText = cur_year.ToString() + " (6 Mons)";
            else if ( cur_mon <= 1)
                dgData.Columns[3].HeaderText = (cur_year-1).ToString() + " (6 Mons)";
            else
                dgData.Columns[3].HeaderText = (cur_year-1).ToString();

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgData.ClearSelection();
        }

        private void rdAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAll.Checked)
                LoadData();
        }

        private void rdPrivate_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPrivate.Checked)
                LoadData();
        }

        private void rdState_CheckedChanged(object sender, EventArgs e)
        {
            if (rdState.Checked)
                LoadData();
        }

        private void rdFederal_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFederal.Checked)
                LoadData();
        }

        private void rdV1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdV1.Checked)
                LoadData();
        }

        private void rdV2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdV2.Checked )
                LoadData();
        }

       

        private void frmSpecStrtSum_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitle = label1.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Near;

            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Special Strtdate Summary";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[1].Width = 100;
            dgData.Columns[2].Width = 100;
            dgData.Columns[3].Width = 100;
            
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            //release printer
            GeneralFunctions.releaseObject(printer);
            Cursor.Current = Cursors.Default;
        }

        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            dgData.CurrentCell = null;
        }
    }
}
