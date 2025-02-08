/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmBenOutputData.cs

 Programmer    : Diane Musachio

 Creation Date : 3/8/2019

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will allow users to display  VIP Benchmark Output Data

 Detail Design : Detailed User Requirements for VIP Benchmark Output

 Other         : Called by: Tabulations -> Benchmark -> Output

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
    public partial class frmBenOutputData : frmCprsParent
    {
        DataTable vipoutput = new DataTable();
        VipBenchmarkOutputData data_object = new VipBenchmarkOutputData();
        private bool formloading;
        List<string> toclist = new List<string>();

        public frmBenOutputData()
        {
            InitializeComponent();
        }

        private void frmBenOutputData_Load(object sender, EventArgs e)
        {
            formloading = true;

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            SetupDropdown();

            cbSeries.DataSource = toclist;
            cbSeries.SelectedIndex = 0;

            //get all data
            vipoutput = data_object.GetBenchmarkOutputData();

            FilterData();

            //include this code to prevent mouse scrolling in dropdownlist
            this.cbSeries.MouseWheel += new MouseEventHandler(CbSeries_MouseWheel);

            formloading = false;
        }

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

        private void  FilterData()
        {
            dgData.DataSource = vipoutput;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                dgvc.Visible = false;
            }
      
            dgData.Columns[0].ReadOnly = true;
            dgData.Columns[0].HeaderText = "DATE";
            dgData.Columns[1].HeaderText = "BENCHMARK DATA";
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DataPropertyName = filter;
            dgData.Columns[1].ReadOnly = true;

            dgData.Columns[0].Visible = true;
            dgData.Columns[1].Visible = true;
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;            
        }

        private string filter;

        private void cbSeries_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSeries.SelectedValue.ToString() == "Manufacturing")
            {
                filter = "V20IXBMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Federal Highway")
            {
                filter = "F12XXBMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Farm")
            {
                filter = "X0360BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Gas")
            {
                filter = "X1123BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Oil")
            {
                filter = "X1124BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Electric")
            {
                filter = "X111XBMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Electric Transmission")
            {
                filter = "X1116BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Improvements")
            {
                filter = "X0013BMO";
            }

            if (!formloading)
            {
                FilterData();
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

        private void frmBenOutputData_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
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

            printer.printDocument.DocumentName = "VIP Benchmark Output Data";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgData);

            Cursor.Current = Cursors.Default;
        }
    }
}
