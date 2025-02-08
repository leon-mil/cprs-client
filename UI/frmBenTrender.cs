/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmBenTrender.cs

 Programmer    : Diane Musachio

 Creation Date : 3/1/2019

 Inputs        : n

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will allow users to display and 
       update VIP Benchmark Trender Data

 Detail Design : Detailed User Requirements for VIP Benchmark Trender

 Other         : Called by: Tabulations -> Benchmark -> Trender

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
using System.Globalization;

namespace Cprs
{
    public partial class frmBenTrender : frmCprsParent
    {
        DataTable viptrend = new DataTable();
        VipBenchmarkTrenderData data_object = new VipBenchmarkTrenderData();

        List<string> toclist = new List<string>();

        private bool formloading;

        public frmBenTrender()
        {
            InitializeComponent();
        }

        private void frmBenTrender_Load(object sender, EventArgs e)
        {
            formloading = true;

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            SetupData();

            SetupDropdown();

            cbSeries.DataSource = toclist;
            cbSeries.SelectedIndex = 0;

            SetupDatagrid();

            //include this code to prevent mouse scrolling in dropdownlist
            this.cbSeries.MouseWheel += new MouseEventHandler(CbSeries_MouseWheel);

            formloading = false;
        }

    

        //get values for the combobox
        private void SetupDropdown()
        {
            toclist.Add("Manufacturing");
            toclist.Add("Federal Highway");
            toclist.Add("Farm");
            toclist.Add("Gas");
            toclist.Add("Oil");
            toclist.Add("Electric");
            toclist.Add("Electric Transmission");
            toclist.Add("Improvements");
        }

        private void GetData()
        {
            viptrend = data_object.GetBenchmarkTrenderData(series);
            dgData.DataSource = viptrend;
       
            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        private void SetupDatagrid()
        {
            dgData.Columns[0].ReadOnly = true;
            dgData.Columns[0].HeaderText = "DATE";
            dgData.Columns[1].HeaderText = "CURRENT DATA";
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].ReadOnly = false;
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].HeaderText = "PREVIOUS DATA";
            dgData.Columns[2].ReadOnly = true;
            dgData.Columns[3].HeaderText = "USER";
            dgData.Columns[3].ReadOnly = true;
            dgData.Columns[4].HeaderText = "DATE/TIME";
            dgData.Columns[4].ReadOnly = true;

            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        //if last year does not exist insert a blank row
        //for each series in datatable for each month
        private void SetupData()
        {
            string nextyr = DateTime.Now.AddYears(+1).ToString("yyyy");

            bool latest = data_object.GetStatInfo(nextyr);

 
            if (latest == false)
            {
                List<string> addtoc = new List<string>();

                addtoc.Add("201");
                addtoc.Add("202");
                addtoc.Add("203");
                addtoc.Add("204");
                addtoc.Add("205");
                addtoc.Add("206");
                addtoc.Add("207");
                addtoc.Add("208");

                foreach (string toc in addtoc)
                {
                    data_object.InsertRows(nextyr + "12", toc);
                    data_object.InsertRows(nextyr + "11", toc);
                    data_object.InsertRows(nextyr + "10", toc);
                    data_object.InsertRows(nextyr + "09", toc);
                    data_object.InsertRows(nextyr + "08", toc);
                    data_object.InsertRows(nextyr + "07", toc);
                    data_object.InsertRows(nextyr + "06", toc);
                    data_object.InsertRows(nextyr + "05", toc);
                    data_object.InsertRows(nextyr + "04", toc);
                    data_object.InsertRows(nextyr + "03", toc);
                    data_object.InsertRows(nextyr + "02", toc);
                    data_object.InsertRows(nextyr + "01", toc);
                }
            }
        }

        private string series;
        private void cbSeries_SelectedValueChanged(object sender, EventArgs e)
        {

            if (cbSeries.SelectedValue.ToString() == "Manufacturing")
            {
                series = "201";
            }
            else if (cbSeries.SelectedValue.ToString() == "Federal Highway")
            {
                series = "202";
            }
            else if (cbSeries.SelectedValue.ToString() == "Farm")
            {
                series = "203";
            }
            else if (cbSeries.SelectedValue.ToString() == "Gas")
            {
                series = "204";
            }
            else if (cbSeries.SelectedValue.ToString() == "Oil")
            {
                series = "205";
            }
            else if (cbSeries.SelectedValue.ToString() == "Electric")
            {
                series = "206";
            }
            else if (cbSeries.SelectedValue.ToString() == "Electric Transmission")
            {
                series = "207";
            }
            else if (cbSeries.SelectedValue.ToString() == "Improvements")
            {
                series = "208";
            }

            GetData();
        }

        //event to ensure only numbers can be entered in editable field
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);
        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-';
        }
        private void dgData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                (e.Control as TextBox).MaxLength = 7;
                e.Control.KeyPress += new KeyPressEventHandler(dgData_KeyPress);
            }
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

        private string oldval;
        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (!formloading)
            {
                int rowindex = dgData.CurrentRow.Index;

                oldval = (dgData[1, rowindex].Value ?? "").ToString();
            }
        }

        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgData.CurrentRow.Index;

            string statp = dgData[0, rowindex].Value.ToString();

            string newval = (dgData[1, rowindex].Value ?? "").ToString();

            if (oldval != newval)
            {
                data_object.UpdateVipBenchmark(statp, series, newval, oldval, UserInfo.UserName);

                dgData[2, rowindex].Value = oldval;
                dgData[3, rowindex].Value = UserInfo.UserName;
                dgData[4, rowindex].Value = DateTime.Now;
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
       
        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = label2.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.SubTitle = " Series = " + cbSeries.Text;
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 9.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;

            printer.printDocument.DocumentName = "VIP Benchmark Trendsetter";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";


            printer.PrintDataGridViewWithoutDialog(dgData);

            Cursor.Current = Cursors.Default;
        }

        private void frmVipBenTrender_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
