/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmVipReviewAnn.cs

 Programmer    : Diane Musachio

 Creation Date : 4/26/2017

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will allow user to directly enter annual data
                 for 51 states

 Detail Design : Detailed User Requirements for VIP Review 

 Other         : Called by: Tabulations -> VIP Review -> Annual 

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
    public partial class frmVipReviewAnn : frmCprsParent
    {
        DataTable annualdata = new DataTable();
        VipReviewData viprev = new VipReviewData();
        List<string> dy = new List<string>();
        private string oldval;
        private bool statexists;
        private bool formloading;

        public frmVipReviewAnn()
        {
            InitializeComponent();
        }

        private void frmVipReviewAnn_Load(object sender, EventArgs e)
        {
            formloading = true;

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //create list of years for combo box display
            GetListYears();

            //load data
            GetData();

            //include this code to prevent mouse scrolling in dropdownlist
            this.cbDate.MouseWheel += new MouseEventHandler(cbDate_MouseWheel);

            formloading = false;
        }

        //get values for the combobox
        private void GetListYears()
        {
            DataTable dates = new DataTable();
            dates = viprev.GetAnnualList();

            string now = DateTime.Now.AddYears(-1).ToString("yyyy");

            //if last year exists then add all distinct dates to list 
            foreach (DataRow dr in dates.Rows)
            {
                if (now == dr[0].ToString())
                {
                    statexists = true;
                }
                dy.Add(dr[0].ToString());
            }

            //if last year does not exist insert a blank row
            //for each state in datatable and add year to list
            if (!statexists)
            {
                viprev.InsertRows(now, "01");
                viprev.InsertRows(now, "02");
                viprev.InsertRows(now, "04");
                viprev.InsertRows(now, "05");
                viprev.InsertRows(now, "06");
                viprev.InsertRows(now, "08");
                viprev.InsertRows(now, "09");
                viprev.InsertRows(now, "10");
                viprev.InsertRows(now, "11");
                viprev.InsertRows(now, "12");
                viprev.InsertRows(now, "13");
                viprev.InsertRows(now, "15");
                viprev.InsertRows(now, "16");
                viprev.InsertRows(now, "17");
                viprev.InsertRows(now, "18");
                viprev.InsertRows(now, "19");
                viprev.InsertRows(now, "20");
                viprev.InsertRows(now, "21");
                viprev.InsertRows(now, "22");
                viprev.InsertRows(now, "23");
                viprev.InsertRows(now, "24");
                viprev.InsertRows(now, "25");
                viprev.InsertRows(now, "26");
                viprev.InsertRows(now, "27");
                viprev.InsertRows(now, "28");
                viprev.InsertRows(now, "29");
                viprev.InsertRows(now, "30");
                viprev.InsertRows(now, "31");
                viprev.InsertRows(now, "32");
                viprev.InsertRows(now, "33");
                viprev.InsertRows(now, "34");
                viprev.InsertRows(now, "35");
                viprev.InsertRows(now, "36");
                viprev.InsertRows(now, "37");
                viprev.InsertRows(now, "38");
                viprev.InsertRows(now, "39");
                viprev.InsertRows(now, "40");
                viprev.InsertRows(now, "41");
                viprev.InsertRows(now, "42");
                viprev.InsertRows(now, "44");
                viprev.InsertRows(now, "45");
                viprev.InsertRows(now, "46");
                viprev.InsertRows(now, "47");
                viprev.InsertRows(now, "48");
                viprev.InsertRows(now, "49");
                viprev.InsertRows(now, "50");
                viprev.InsertRows(now, "51");
                viprev.InsertRows(now, "53");
                viprev.InsertRows(now, "54");
                viprev.InsertRows(now, "55");
                viprev.InsertRows(now, "56");

                dy.Add(now.ToString());
            }

            //if new statperiod added - resort the list for dropdown box
            var sortedList = (from s in dy orderby s descending select s).ToList();
            cbDate.DataSource = sortedList;

            //set default value of combobox to current survey year
            cbDate.Text = DateTime.Now.AddYears(-1).ToString("yyyy");
        }

        //get annual data from vip farm and display state name for 
        //each fipstate
        private void GetData()
        {
            DataTable annualdata;
            annualdata = viprev.GetAnnualData(cbDate.Text);

            for (int i = 0; i < annualdata.Rows.Count; i++)
            {
                string stateDisplay;

                if (annualdata.Rows[i][0].ToString() == "01")
                {
                    stateDisplay = "01 Alabama";
                }
                else if (annualdata.Rows[i][0].ToString() == "02")
                {
                    stateDisplay = "02 Alaska";
                }
                else if (annualdata.Rows[i][0].ToString() == "04")
                {
                    stateDisplay = "04 Arizona";
                }
                else if (annualdata.Rows[i][0].ToString() == "05")
                {
                    stateDisplay = "05 Arkansas";
                }
                else if (annualdata.Rows[i][0].ToString() == "06")
                {
                    stateDisplay = "06 California";
                }
                else if (annualdata.Rows[i][0].ToString() == "08")
                {
                    stateDisplay = "08 Colorado";
                }
                else if (annualdata.Rows[i][0].ToString() == "09")
                {
                    stateDisplay = "09 Connecticut";
                }
                else if (annualdata.Rows[i][0].ToString() == "10")
                {
                    stateDisplay = "10 Delaware";
                }
                else if (annualdata.Rows[i][0].ToString() == "11")
                {
                    stateDisplay = "11 District of Columbia";
                }
                else if (annualdata.Rows[i][0].ToString() == "12")
                {
                    stateDisplay = "12 Florida";
                }
                else if (annualdata.Rows[i][0].ToString() == "13")
                {
                    stateDisplay = "13 Georgia";
                }
                else if (annualdata.Rows[i][0].ToString() == "15")
                {
                    stateDisplay = "15 Hawaii";
                }
                else if (annualdata.Rows[i][0].ToString() == "16")
                {
                    stateDisplay = "16 Idaho";
                }
                else if (annualdata.Rows[i][0].ToString() == "17")
                {
                    stateDisplay = "17 Illinois";
                }
                else if (annualdata.Rows[i][0].ToString() == "18")
                {
                    stateDisplay = "18 Indiana";
                }
                else if (annualdata.Rows[i][0].ToString() == "19")
                {
                    stateDisplay = "19 Iowa";
                }
                else if (annualdata.Rows[i][0].ToString() == "20")
                {
                    stateDisplay = "20 Kansas";
                }
                else if (annualdata.Rows[i][0].ToString() == "21")
                {
                    stateDisplay = "21 Kentucky";
                }
                else if (annualdata.Rows[i][0].ToString() == "22")
                {
                    stateDisplay = "22 Louisiana";
                }
                else if (annualdata.Rows[i][0].ToString() == "23")
                {
                    stateDisplay = "23 Maine";
                }
                else if (annualdata.Rows[i][0].ToString() == "24")
                {
                    stateDisplay = "24 Maryland";
                }
                else if (annualdata.Rows[i][0].ToString() == "25")
                {
                    stateDisplay = "25 Massachusetts";
                }
                else if (annualdata.Rows[i][0].ToString() == "26")
                {
                    stateDisplay = "26 Michigan";
                }
                else if (annualdata.Rows[i][0].ToString() == "27")
                {
                    stateDisplay = "27 Minnesota";
                }
                else if (annualdata.Rows[i][0].ToString() == "28")
                {
                    stateDisplay = "28 Mississippi";
                }
                else if (annualdata.Rows[i][0].ToString() == "29")
                {
                    stateDisplay = "29 Missouri";
                }
                else if (annualdata.Rows[i][0].ToString() == "30")
                {
                    stateDisplay = "30 Montana";
                }
                else if (annualdata.Rows[i][0].ToString() == "31")
                {
                    stateDisplay = "31 Nebraska";
                }
                else if (annualdata.Rows[i][0].ToString() == "32")
                {
                    stateDisplay = "32 Nevada";
                }
                else if (annualdata.Rows[i][0].ToString() == "33")
                {
                    stateDisplay = "33 New Hampshire";
                }
                else if (annualdata.Rows[i][0].ToString() == "34")
                {
                    stateDisplay = "34 New Jersey";
                }
                else if (annualdata.Rows[i][0].ToString() == "35")
                {
                    stateDisplay = "35 New Mexico";
                }
                else if (annualdata.Rows[i][0].ToString() == "36")
                {
                    stateDisplay = "36 New York";
                }
                else if (annualdata.Rows[i][0].ToString() == "37")
                {
                    stateDisplay = "37 North Carolina";
                }
                else if (annualdata.Rows[i][0].ToString() == "38")
                {
                    stateDisplay = "38 North Dakota";
                }
                else if (annualdata.Rows[i][0].ToString() == "39")
                {
                    stateDisplay = "39 Ohio";
                }
                else if (annualdata.Rows[i][0].ToString() == "40")
                {
                    stateDisplay = "40 Oklahoma";
                }
                else if (annualdata.Rows[i][0].ToString() == "41")
                {
                    stateDisplay = "41 Oregon";
                }
                else if (annualdata.Rows[i][0].ToString() == "42")
                {
                    stateDisplay = "42 Pennsylvania";
                }
                else if (annualdata.Rows[i][0].ToString() == "44")
                {
                    stateDisplay = "44 Rhode Island";
                }
                else if (annualdata.Rows[i][0].ToString() == "45")
                {
                    stateDisplay = "45 South Carolina";
                }
                else if (annualdata.Rows[i][0].ToString() == "46")
                {
                    stateDisplay = "46 South Dakota";
                }
                else if (annualdata.Rows[i][0].ToString() == "47")
                {
                    stateDisplay = "47 Tennessee";
                }
                else if (annualdata.Rows[i][0].ToString() == "48")
                {
                    stateDisplay = "48 Texas";
                }
                else if (annualdata.Rows[i][0].ToString() == "49")
                {
                    stateDisplay = "49 Utah";
                }
                else if (annualdata.Rows[i][0].ToString() == "50")
                {
                    stateDisplay = "50 Vermont";
                }
                else if (annualdata.Rows[i][0].ToString() == "51")
                {
                    stateDisplay = "51 Virginia";
                }
                else if (annualdata.Rows[i][0].ToString() == "53")
                {
                    stateDisplay = "53 Washington";
                }
                else if (annualdata.Rows[i][0].ToString() == "54")
                {
                    stateDisplay = "54 West Virginia";
                }
                else if (annualdata.Rows[i][0].ToString() == "55")
                {
                    stateDisplay = "55 Wisconsin";
                }
                else if (annualdata.Rows[i][0].ToString() == "56")
                {
                    stateDisplay = "56 Wyoming";
                }
                else
                {
                    stateDisplay = "";
                }

                annualdata.Rows[i][0] = stateDisplay;
            }

            dgData.DataSource = annualdata;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //include this code to prevent mouse scrolling in dropdownlist
        private void cbDate_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
            {
                hme.Handled = true;
                dgData.Focus();
            }

        }

        //doing this directly in code leads to slow performance
        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //override the row formatting defaults by cell style
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

        //if new date selected reload data
        private void cbDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                GetData();
            }
        }

        //upon entry of editable cell reset index and assign the old value
        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            int rowindex = dgData.CurrentRow.Index;

            oldval = (dgData[1, rowindex].Value ?? "").ToString();
        }

        //upon completion of edit assign values and update vip review trail
        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            int rowindex = dgData.CurrentRow.Index;
            string state = (dgData[0, rowindex].Value ?? "").ToString().Substring(0, 2);
            string newval = (dgData[1, rowindex].Value ?? "").ToString();
            string seldate = cbDate.Text;

            if (oldval != newval)
            {
                viprev.UpdateVipFarmReview(state, newval, oldval, seldate);

                dgData[2, rowindex].Value = oldval;
                dgData[3, rowindex].Value = UserInfo.UserName;
                dgData[4, rowindex].Value = DateTime.Now;
            }
        }

        //ensures edit only displays if numeric
        private static KeyPressEventHandler NumericCheckHandler = new KeyPressEventHandler(NumericCheck);
        private static void NumericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != '-';
        }

        private void dgData_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && ((e.KeyChar != '.')
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

        private void frmVipReviewAnn_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        //Button to print data
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

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

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.Title = "UPDATE VIP FARM DATA";
            printer.SubTitle = "Year = " + cbDate.Text;
            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.Userinfo = UserInfo.UserName;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPrint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "VIP Review Annual Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            printer.PrintDataGridViewWithoutDialog(dgPrint);

            Cursor.Current = Cursors.Default;
        }

    }
}