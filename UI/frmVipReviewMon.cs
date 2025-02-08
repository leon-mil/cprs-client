/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmVipReviewMon.cs

 Programmer    : Diane Musachio

 Creation Date : 4/26/2017

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will allow user to enter VIP monthly data
                 for 10 types of construction

 Detail Design : Detailed User Requirements for VIP Review 

 Other         : Called by: Tabulations -> VIP Review -> Monthly 

 Revisions     : See Below
 *********************************************************************
 Modified Date : June 8, 2020 
 Modified By   : Diane Musachio
 Keyword       : 20200608dm
 Change Request: Previous CR 3301
 Description   : Update Dates displayed in Combo box to go back 
                 2 Years and 6 months
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
using System.Globalization;

namespace Cprs
{
    public partial class frmVipReviewMon : frmCprsParent
    {
        DataTable monthlydata = new DataTable();
        VipReviewData viprev = new VipReviewData();
        List<string> dy = new List<string>();
        private string oldval;
        private string twoyears;
        private bool formloading;

        public frmVipReviewMon()
        {
            InitializeComponent();
        }

        private void frmVipReviewMon_Load(object sender, EventArgs e)
        {
            formloading = true;

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //create list of months for combo box display
            GetListMonths();

            //load data
            GetData();

            //include this code to prevent mouse scrolling in dropdownlist
            this.cbDate.MouseWheel += new MouseEventHandler(CbSeries_MouseWheel);

            formloading = false;
        }

        //get values for the combobox
        private void GetListMonths()
        {
            DataTable dates = new DataTable();
            dates = viprev.GetMonthList();

            //gets the date of 2 years and 6 month from current survey month
            twoyears = DateTime.Now.AddMonths(-29).ToString("yyyyMM");

            foreach (DataRow dr in dates.Rows)
            {
                dy.Add(dr[0].ToString());

                if (dr[0].ToString() == twoyears)
                {
                    break;
                }
            }

            cbDate.DataSource = dy;

            //set default value of combobox to current survey month
            cbDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
        }

        //get monthly vip data from VIP hist
        private void GetData()
        {
            DataTable monthlydata;

            monthlydata = viprev.GetMonthlyData(cbDate.Text);

            //alters display to desired text string
            for (int i = 0; i < monthlydata.Rows.Count; i++)
            {
                string tocDisplay;

                if (monthlydata.Rows[i][0].ToString() == "101")
                {
                    tocDisplay = "101 1 UNIT";
                }
                else if (monthlydata.Rows[i][0].ToString() == "102")
                {
                    tocDisplay = "102 ADDS & ALTS";
                }
                else if (monthlydata.Rows[i][0].ToString() == "103")
                {
                    tocDisplay = "103 FARM NONRES";
                }
                else if (monthlydata.Rows[i][0].ToString() == "104")
                {
                    tocDisplay = "104 TELECOMMUNICATIONS";
                }
                else if (monthlydata.Rows[i][0].ToString() == "105")
                {
                    tocDisplay = "105 GAS";
                }
                else if (monthlydata.Rows[i][0].ToString() == "106")
                {
                    tocDisplay = "106 ELECTRIC";
                }
                else if (monthlydata.Rows[i][0].ToString() == "107")
                {
                    tocDisplay = "107 RAILROAD";
                }
                else if (monthlydata.Rows[i][0].ToString() == "108")
                {
                    tocDisplay = "108 PETROLEUM PIPE";
                }
                else if (monthlydata.Rows[i][0].ToString() == "109")
                {
                    tocDisplay = "109 FEDERAL AGENCY TOTAL";
                }
                else if (monthlydata.Rows[i][0].ToString() == "110")
                {
                    tocDisplay = "110 WIND";
                }
                else
                {
                    tocDisplay = "";
                }

                monthlydata.Rows[i][0] = tocDisplay;
            }

            dgData.DataSource = monthlydata;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        //doing this directly in code leads to slow performance
        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //overrides row default values for cells
            foreach (DataGridViewRow row in dgData.Rows)
            {
                row.Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[2].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
                row.Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                row.Cells[4].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
        }

        //after altering state display make column non-editable
        private void dgData_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgData.Columns[0].ReadOnly = true;
        }

        //if date changed in combobox - re-get data
        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                GetData();
            }
        }

        //at entry of editable cell reset rowindex and previous data assignment
        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int rowindex = dgData.CurrentRow.Index;

            oldval = (dgData[1, rowindex].Value ?? "").ToString();
        }

        //at end of edit assign values and apply
        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgData.CurrentRow.Index;
            string toc = (dgData[0, rowindex].Value ?? "").ToString().Substring(0, 3);
            string newval = (dgData[1, rowindex].Value ?? "").ToString();
            string seldate = cbDate.Text;

            if (oldval != newval)
            {
                viprev.UpdateVipReview(toc, newval, oldval, seldate);

                dgData[2, rowindex].Value = oldval;
                dgData[3, rowindex].Value = UserInfo.UserName;
                dgData[4, rowindex].Value = DateTime.Now;
            }
        }

        //event to ensure only numbers can be entered in editable field
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);
        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-';
        }

        private void dgData_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar)) && ((e.KeyChar != '.')
                        || ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)));
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
            if (e.Control is TextBox)
            {
                (e.Control as TextBox).MaxLength = 7;
                e.Control.KeyPress += new KeyPressEventHandler(dgData_KeyPress);
            }
        }

        //include this code to prevent mouse scrolling in dropdownlist
        private void CbSeries_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
            {
                hme.Handled = true;
                dgData.Focus();
            }

        }

        private void frmVipReviewMon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        //print screen
        Bitmap memoryImage;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

    }
}