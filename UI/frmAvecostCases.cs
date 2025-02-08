/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmAvecostCases.cs

 Programmer    : Diane Musachio

 Creation Date : 03/23/17

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will display all projects for selected
                 startdate from Average Cost Total Screen of Multifamily project

 Detail Design : Detailed User Requirements for Average Cost Multifamily

 Other         : Called by: menu -> Tabulations -> monthly -> select survey:
                 multifamily -> select a row -> worksheet button -> average cost button ->
                 display cases
 
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
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Globalization;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Collections;
using System.Windows.Forms.VisualStyles;
using System.IO;
using CprsDAL;
using CprsBLL;


namespace Cprs
{
    public partial class frmAvecostCases : frmCprsParent
    {
        public Form CallingForm = null;

        private bool call_callingFrom = false;

        public string sdate;
        public string region;
        public string division;
        public string state;
        public string sdateSel;
        public string regiontext;
        public string divisiontext;
        public string statetext;
        public string type;
        public DataView dv;

        private string currentMonth;
        private string priorMonth;
        private string ppriorMonth;

        //private AveCostData dataObject;

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmAvecostCases()
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            InitializeComponent();
        }

        private void frmAvecostCases_Load(object sender, EventArgs e)
        {
            lblTitle.Text = "Multifamily " + sdate + " Average Cost";
            lblTitle1.Text = "Cases Started " + sdateSel + " For ";
            lblTitle2.Text = "Region " + regiontext + "    Division " + divisiontext + "    State " + statetext;

            //assign dataview to datagrid
            dgData.DataSource = dv;

            if (dgData.ColumnCount > 0)
            {
                SetupHeaders();
            }

            lblTotal.Text = "TOTAL CASES:  " + dgData.RowCount;
        }

        private void SetupHeaders()
        {
            ////Convert sdate to datetime for column headers
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
            currentMonth = (dt.ToString("yyyyMM", CultureInfo.InvariantCulture));
            priorMonth = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            ppriorMonth = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);

            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[0].Width = 50;
            dgData.Columns[1].HeaderText = "Newtc";
            dgData.Columns[1].Width = 50;
            dgData.Columns[2].HeaderText = currentMonth;
            dgData.Columns[2].Width = 60;
            dgData.Columns[3].HeaderText = "F";
            dgData.Columns[3].Width = 20;
            dgData.Columns[4].HeaderText = priorMonth;
            dgData.Columns[4].Width = 60;
            dgData.Columns[5].HeaderText = "F";
            dgData.Columns[5].Width = 20;
            dgData.Columns[6].HeaderText = ppriorMonth;
            dgData.Columns[6].Width = 60;
            dgData.Columns[7].HeaderText = "F";
            dgData.Columns[7].Width = 20;
            dgData.Columns[8].HeaderText = "%Comp";
            dgData.Columns[8].DefaultCellStyle.Format = "N0";
            dgData.Columns[8].Width = 50;
            dgData.Columns[9].HeaderText = "Pcityst             ";
            dgData.Columns[9].Width = 180;
            dgData.Columns[10].HeaderText = "Seldate";
            dgData.Columns[10].Width = 60;
            dgData.Columns[11].HeaderText = "Strtdate";
            dgData.Columns[11].Width = 70;
            dgData.Columns[12].HeaderText = "Runits";
            dgData.Columns[12].Width = 55;
            dgData.Columns[13].HeaderText = "Rvitm5c";
            dgData.Columns[13].Width = 60;
            dgData.Columns[14].HeaderText = "Item6";
            dgData.Columns[14].Width = 60;
            dgData.Columns[15].HeaderText = "Fswgt";
            dgData.Columns[15].Width = 60;
            dgData.Columns[16].HeaderText = "Fwgt";
            dgData.Columns[16].Width = 55;
            dgData.Columns[17].HeaderText = "Cpu";
            dgData.Columns[17].DefaultCellStyle.Format = "N0";
            dgData.Columns[17].Width = 80;

            //hide extra data columns used to filter criteria
            dgData.Columns[18].Visible = false;
            dgData.Columns[19].Visible = false;
            dgData.Columns[20].Visible = false;

        }

        private void lblTitle2_SizeChanged(object sender, EventArgs e)
        {
            lblTitle2.Left = (this.ClientSize.Width - lblTitle2.Size.Width) / 2;
        }

        private void frmAvecostCases_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
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

        private void btnC700_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

            int index = dgData.CurrentRow.Index;
            string selected_id = dgData["ID", index].Value.ToString();

            this.Hide(); // hide parent

            string val1 = dgData["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down
            List<string> Idlist = new List<string>();
            int cnt = 0;
            foreach (DataGridViewRow dr in dgData.Rows)
            {
               string val = dgData["ID", cnt].Value.ToString();
               Idlist.Add(val);
               cnt = cnt + 1;
            }

            frmC700 fC700 = new frmC700();
            fC700.Id = val1;
            fC700.Idlist = Idlist;
            fC700.CurrIndex = index;
            fC700.CallingForm = this;
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            fC700.ShowDialog(); // show child

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgData.RowCount >= 100)
            {
                if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    PrintData();
                }
            }
            else
            {
                PrintData();
            }
        }

        private void PrintData()
        {
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

            //It's recommended to use the AddRange method (if available)
            //when adding multiple items to a collection.

            targetGrid.Rows.AddRange(targetRows.ToArray());
            
           // dgPrint.AutoGenerateColumns = false;
           // dgPrint.DataSource = dv;
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.Title = lblTitle.Text;
            printer.SubTitle = lblTitle1.Text + lblTitle2.Text;
            printer.PageSettings.Landscape = true;
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = lblTitle.Text;
            printer.Footer = " ";
            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            //this.dgPrint.Sort(dgPrint.Columns["id"], ListSortDirection.Ascending);
            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgPrint.Columns[0].HeaderText = "ID";
            dgPrint.Columns[0].Width = 58;
            dgPrint.Columns[1].HeaderText = "NEWTC";
            dgPrint.Columns[1].Width = 53;
            dgPrint.Columns[2].HeaderText = currentMonth;
            dgPrint.Columns[2].Width = 45;
            dgPrint.Columns[3].HeaderText = "F";
            dgPrint.Columns[3].Width = 20;
            dgPrint.Columns[4].HeaderText = priorMonth;
            dgPrint.Columns[4].Width = 45;
            dgPrint.Columns[5].HeaderText = "F";
            dgPrint.Columns[5].Width = 20;
            dgPrint.Columns[6].HeaderText = ppriorMonth;
            dgPrint.Columns[6].Width = 45;
            dgPrint.Columns[7].HeaderText = "F";
            dgPrint.Columns[7].Width = 20;
            dgPrint.Columns[8].HeaderText = "%COMP";
            dgPrint.Columns[8].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[8].Width = 60;
            dgPrint.Columns[9].HeaderText = "PCITYST";
            dgPrint.Columns[9].Width = 200;
            dgPrint.Columns[10].HeaderText = "SELDATE";
            dgPrint.Columns[10].Width = 60;
            dgPrint.Columns[11].HeaderText = "STRTDATE";
            dgPrint.Columns[11].Width = 70;
            dgPrint.Columns[12].HeaderText = "RUNITS";
            dgPrint.Columns[12].Width = 50;
            dgPrint.Columns[13].HeaderText = "RVITM5C";
            dgPrint.Columns[13].Width = 58;
            dgPrint.Columns[14].HeaderText = "ITEM6";
            dgPrint.Columns[14].Width = 50;
            dgPrint.Columns[15].HeaderText = "FSWGT";
            dgPrint.Columns[15].Width = 50;
            dgPrint.Columns[16].HeaderText = "FWGT";
            dgPrint.Columns[16].Width = 40;
            dgPrint.Columns[17].HeaderText = "CPU";
            dgPrint.Columns[17].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[17].Width = 58;

            //hide extra data columns used to filter criteria
            dgPrint.Columns[18].Visible = false;
            dgPrint.Columns[19].Visible = false;
            dgPrint.Columns[20].Visible = false;

          
            printer.PrintDataGridViewWithoutDialog(dgPrint);
        }
    }
}

