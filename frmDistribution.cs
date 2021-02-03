/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmDistribution.cs
Programmer    : Christine Zhang
Creation Date : July 13 2017
Parameters    : NEWTC, Survey, Callingform
Inputs        : N/A
Outputs       : N/A
Description   : Display distribution table
Change Request: 
Specification : 
Rev History   : See Below

Other         : N/A
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
using System.Globalization;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmDistribution : frmCprsParent
    {
        public Form CallingForm = null;
        public string Newtc;
        public string Survey;

        private bool call_callingFrom = false;
        private DistributionData data_object;
        private bool form_loading;

        public frmDistribution()
        {
            InitializeComponent();
        }

        private void frmDistribution_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            form_loading = true;
            PopulateDataCombo();
            PopulateFipStateCombo();
            cbRegion.SelectedIndex = 0;
            cbDiv.SelectedIndex = 0;
            cbState.SelectedIndex = 0;
            cbDate.SelectedIndex = 0;

            lblTitle1.Text = GeneralFunctions.GetSurveyText(Survey) + " " + cbDate.Text + " " + "VIP DISTRIBUTION" ;

            data_object = new DistributionData();
            GetData();

            form_loading = false;
        }

        private void GetData()
        {
            string col = "";

            if (cbDate.SelectedIndex == 0)
                col = "V0";
            else if (cbDate.SelectedIndex == 1)
                col = "V1";
            else if (cbDate.SelectedIndex == 2)
                col = "V2";
            else if (cbDate.SelectedIndex == 3)
                col = "V3";
            else if (cbDate.SelectedIndex == 4)
                col = "V4";
            else if (cbDate.SelectedIndex == 5)
                col = "V5";

            string region = cbRegion.SelectedItem.ToString().Substring(0, 1);
            string division = cbDiv.SelectedItem.ToString().Substring(0,1);
            string fipstate = cbState.Text.Substring(0, 2);
            
            DataTable table = data_object.GetDistributionData(col, Survey, Newtc, region, division, fipstate);
            dgData.DataSource = table;

            setColumnHeader();
        }

        private void setColumnHeader()
        {
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgData.Columns[0].HeaderText = "Type of Construction";
            dgData.Columns[0].Width = 275;
            dgData.Columns[1].HeaderText = "Report";
            dgData.Columns[1].Width = 100;
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].HeaderText = "Impute";
            dgData.Columns[2].Width = 100;
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].HeaderText = "Report";
            dgData.Columns[3].Width = 100;
            dgData.Columns[3].DefaultCellStyle.Format = "N2";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].HeaderText = "Impute";
            dgData.Columns[4].Width = 100;
            dgData.Columns[4].DefaultCellStyle.Format = "N2";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = "Report";
            dgData.Columns[5].Width = 100;
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].HeaderText = "Impute";
            dgData.Columns[6].Width = 100;
            dgData.Columns[6].DefaultCellStyle.Format = "N0";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[7].HeaderText = "Report";
            dgData.Columns[7].Width = 100;
            dgData.Columns[7].DefaultCellStyle.Format = "N2";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "Impute";
            dgData.Columns[8].Width = 103;
            dgData.Columns[8].DefaultCellStyle.Format = "N2";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            if (dgData.RowCount > 23)
                dgData.Columns[8].DefaultCellStyle.Padding = new Padding(0, 0, 15, 0);
            dgData.Columns[9].Visible= false;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
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

        private void PopulateFipStateCombo()
        {
            DataTable dt = GeneralDataFuctions.GetFipStateDataForCombo();
            dt.Rows[0].Delete();
            DataRow dr = dt.NewRow();
            dr[0] = 0;
            dr[1] = "00-US";
            dt.Rows.InsertAt(dr, 0);
            cbState.DataSource = dt;
            cbState.ValueMember = "FIPSTATE";
            cbState.DisplayMember = "STATE1";
            cbState.SelectedIndex = 0;
        }

        private void PopulateDataCombo()
        {
            cbDate.Items.Clear();
            string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            /* initialize elements of array n */
            for (int i = 0; i < 6; i++)
            {
                string date = dt.AddMonths(-i).ToString("yyyyMM", CultureInfo.InvariantCulture);
                cbDate.Items.Add(date);
            }
        }

        private void frmDistribution_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
            {
                form_loading = true;
                cbDiv.SelectedIndex = 0;
                cbState.SelectedIndex = 0;
                cbRegion.SelectedIndex = 0;
                form_loading = false;
                GetData();

                lblTitle1.Text = GeneralFunctions.GetSurveyText(Survey) + " " + cbDate.Text + " " + "VIP DISTRIBUTION";
            }
        }

        private void cbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
            {
                form_loading = true;
                cbDiv.SelectedIndex = 0;
                cbState.SelectedIndex = 0;
                form_loading = false;
                GetData();
            }
        }

        private void cbDiv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
            {
                form_loading = true;
                cbRegion.SelectedIndex = 0;
                cbState.SelectedIndex = 0;
                form_loading = false;
                GetData();
            }
        }

        private void cbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!form_loading)
            {
                form_loading = true;
                cbRegion.SelectedIndex = 0;
                cbDiv.SelectedIndex = 0;
                form_loading = false;
                GetData();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = lblTitle1.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitle = lblTitle2.Text;
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "Distribution";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            //resize the note column
            dgData.Columns[0].Width = 175;
            
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 275;

            Cursor.Current = Cursors.Default;

        }
    }
}
