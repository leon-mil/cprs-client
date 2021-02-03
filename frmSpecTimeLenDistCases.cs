/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmSpecTimeLenDistCases.cs	    	
Programmer      : Christine Zhang   
Creation Date   : 5/9/2019
Inputs          : None                 
Parameters      : select survy, calling form
Outputs         : None	
Description     : Display Special Time length distribution display cases
Detailed Design : Detailed Design for Time length deistribution display cases
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
using System.Diagnostics;

namespace Cprs
{
    public partial class frmSpecTimeLenDistCases : frmCprsParent
    {
        public frmSpecTimeLenDistCases()
        {
            InitializeComponent();
        }

        public frmSpecTimeLenDist CallingForm = null;
        public string SelectedSurvey;

        private SpecTimelenData data_object;
        private DataTable dtmain = new DataTable();
        private bool call_callingFrom = false;
        private void frmSpecTimeLenDistCases_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");


            if (SelectedSurvey == "P")
            {
                lblTitle.Text = "STATE AND LOCAL";
                panel1.Visible = false;
                panel2.Visible = true;
                label4.Text = "Cases in Value Group " + rdb1.Text;
            }
            else if (SelectedSurvey == "N")
            {
                lblTitle.Text = "NONRESIDENTIAL";
                panel1.Visible = false;
                panel2.Visible = true;
                label4.Text = "Cases in Value Group " + rdb1.Text;
            }
            else
            {
                lblTitle.Text = "MULTIFAMILY";
                panel1.Visible = true;
                panel2.Visible = false;
                label4.Text = "Cases in Value Group " + rdbm1.Text;
            }
            data_object = new SpecTimelenData();

            dtmain = data_object.GetTimeLenDataDistributionCases(SelectedSurvey);

            SetDataByVG();
        }

        private void SetDataByVG()
        {
            int vg = 0;
            if (SelectedSurvey != "M")
            {
                if (rdb1.Checked)
                    vg = 1;
                else if (rdb2.Checked)
                    vg = 2;
                else if (rdb3.Checked)
                    vg = 3;
                else if (rdb4.Checked)
                    vg = 4;
                else if (rdb5.Checked)
                    vg = 5;
                else if (rdb6.Checked)
                    vg = 6;
            }
            else
            {
                if (rdbm1.Checked)
                    vg = 1;
                else if (rdbm2.Checked)
                    vg = 2;
                else if (rdbm3.Checked)
                    vg = 3;
                else if (rdbm4.Checked)
                    vg = 4;
            }

            if (vg == 0)
            {
                dgData.DataSource = dtmain;
                lblCount.Text = dtmain.Rows.Count.ToString();
            }
            else
            {
                DataRow[] drarray;
                drarray = dtmain.Select("VG = " + vg);

                if (drarray.Length > 0)
                {
                    dgData.DataSource = drarray.CopyToDataTable();
                    lblCount.Text = drarray.Length.ToString();
                }
            }

            SetColumnHeader();
        }

        private void SetColumnHeader()
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
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }
            this.Close();
        }

        private void frmSpecTimeLenDistCases_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void rdb1_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdb1.Text;
        }

        private void rdb2_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdb2.Text;
        }

        private void rdb3_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdb3.Text;
        }

        private void rdb5_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdb5.Text;
        }

        private void rdb6_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdb6.Text;
        }

        private void rdbAll_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdbAll.Text;
        }

        private void rdbm1_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdbm1.Text;
        }

        private void rdbm2_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdbm2.Text;
        }

        private void rdbm3_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdbm3.Text;
        }

        private void rdbm4_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdbm4.Text;
        }

        private void rdbmAll_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdbmAll.Text;
        }

        private void rdb4_CheckedChanged(object sender, EventArgs e)
        {
            SetDataByVG();
            label4.Text = "Cases in Value Group " + rdb4.Text;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (dgData.RowCount > 235)
            {
                if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    return;
                }
            }
          
            Cursor.Current = Cursors.WaitCursor;
            DGVPrinter printer = new DGVPrinter();
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitle = label4.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Display Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            for (int i = 0; i < 13; i++)
            {
                dgData.Columns[i].Width = 75;
            }

            printer.PrintDataGridViewWithoutDialog(dgData);

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
           
            Cursor.Current = Cursors.Default;
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
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

    }
}
