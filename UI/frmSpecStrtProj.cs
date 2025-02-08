/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmSpecStrtProj.cs	    	
Programmer:         Christine Zhang
Creation Date:      9/6/2018
Inputs:             None                                   
Parameters:	        None              
Outputs:	        None
Description:	    This program displays the annual compare strtdate and seldate data
                    for project over 100000 
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
    public partial class frmSpecStrtProj : frmCprsParent
    {
        public frmSpecStrt CallingForm = null;

        /*flag to use closing the form */
        private bool call_callingFrom = false;

        private int cur_year;
        private int cur_mon;

        public frmSpecStrtProj()
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

        private void frmSpecStrtProj_Load(object sender, EventArgs e)
        {
            string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            cur_mon = Convert.ToInt16(sdate.Substring(4, 2));
            cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            if (cur_mon >= 6)
            {
                cbYear.Items.Add("ALL");
                cbYear.Items.Add(cur_year - 2);
                cbYear.Items.Add(cur_year - 1);
                cbYear.Items.Add(cur_year);
            }
            else
            {
                cbYear.Items.Add("ALL");
                cbYear.Items.Add(cur_year - 3);
                cbYear.Items.Add(cur_year - 2);
                cbYear.Items.Add(cur_year - 1);
            }
            cbYear.SelectedIndex = 0;
            //  LoadData();
        }

        private void frmSpecStrtProj_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void LoadData()
        {
            string selyear = string.Empty;

            if (cbYear.Text == "ALL")
                selyear = "";
            else
                selyear = cbYear.Text;

            //Get current data
            SpecStrtData dataObject = new SpecStrtData();
            DataTable strtdate_table = dataObject.GetSpecStrtDateProjData(selyear);
            dgData.DataSource = null;
            dgData.DataSource = strtdate_table;

            //set up column header
            SetColumnHeader();

            int y1;
            int y2;
            
            if (cbYear.Text == "ALL")
            {
                if (cur_mon >= 6)
                {
                    y1 = cur_year - 2;
                    y2 = cur_year;
                    lblTitle.Text = "PROJECTS VALUED OVER $100 M SELECTED FROM  " + y1.ToString() + "01 - " + y2.ToString() + "06";
                }
                else if (cur_mon < 2)
                {
                    y1 = cur_year - 3;
                    y2 = cur_year - 1;
                    lblTitle.Text = "PROJECTS VALUED OVER $100 M SELECTED FROM  " + y1.ToString() + "01 - " + y2.ToString() + "06";
                }
                else
                {
                    y1 = cur_year - 3;
                    y2 = cur_year - 1;
                    lblTitle.Text = "PROJECTS VALUED OVER $100 M SELECTED FROM  " + y1.ToString() + "01 - " + y2.ToString() + "12";
                }
            }
            else if (cbYear.Text == cur_year.ToString() && cur_mon >= 6)
                lblTitle.Text = "PROJECTS VALUED OVER $100 M SELECTED FROM  " + cbYear.Text + "01 - " + cbYear.Text + "06";
            else if (cbYear.Text == (cur_year - 1).ToString() && (cur_mon < 2))
                lblTitle.Text = "PROJECTS VALUED OVER $100 M SELECTED FROM  " + cbYear.Text + "01 - " + cbYear.Text + "06";
            else
                lblTitle.Text = "PROJECTS VALUED OVER $100 M SELECTED FROM  " + cbYear.Text + "01 - " + cbYear.Text + "12";

            //set up range label
            Decimal range = Decimal.Parse(strtdate_table.Rows[9][3].ToString()) + Decimal.Parse(strtdate_table.Rows[9][4].ToString()) + Decimal.Parse(strtdate_table.Rows[9][5].ToString()) + Decimal.Parse(strtdate_table.Rows[9][6].ToString()) + Decimal.Parse(strtdate_table.Rows[9][7].ToString());

            label5.Text = "Within Dodge's Range " + Math.Round(range, 0).ToString() + "%";
        }

        private void SetColumnHeader()
        {
            dgData.Columns[0].HeaderText = "Months";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[1].HeaderText = "-4";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].HeaderText = "-3";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].HeaderText = "-2";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].HeaderText = "-1";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = "0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].HeaderText = "1";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[7].HeaderText = "2";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "3";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[9].HeaderText = "4";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[10].HeaderText = "Total";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dgData.ClearSelection();
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
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

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Special Strtdate Project Over 1 M";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            for (int a = 1; a < 10; a = a + 1)
            {
                dgData.Columns[a].Width = 60;
            }
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
