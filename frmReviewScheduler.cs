/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmReviewScheduler.cs	    	
Programmer:         Diane Musachio
Creation Date:      6/24/2017
Inputs:             None
Parameters:	        flagname, srow, flagTitle, mySectors
Outputs:	       	
Description:	    This screen displays id with specific flag/flagtype from
Detailed Design:    None 
Other:	            Called by: frmFlagTypeReport.cs
Revision History:	
****************************************************************************************
 Modified Date :  March 28, 2019
 Modified By   :  Diane Musachio    
 Keyword       :  dm032819
 Change Request:  #3027
 Description   :  Add status field
**************************************************************************************** 
 Modified Date :  Sept. 24 2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR#3577
 Description   :  Previous button and C700 button switch location
 ****************************************************************************************
 Modified Date :  Sept. 24 2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR#3853
 Description   :  show comp% column, delete runits, rbldgs column
 ****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DGVPrinterHelper;
using CprsDAL;
using CprsBLL;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmReviewScheduler : frmCprsParent
    {
        public frmFlagTypeReport CallingForm = null;
        public string flagname;
        public string srow;
        public string flagTitle;
        public string mySectors;
        DataTable dt = new DataTable();

        /*flag to use closing the form */
        private bool call_callingFrom = false;

        private FlagReportData data_object;

        public frmReviewScheduler()
        {
            InitializeComponent();
        }

        private void frmReviewScheduler_Load(object sender, EventArgs e)
        {
            lblText.Text = flagTitle;
            SetButtonTxt();
            GetData();
            rd1a.Checked = true;
        }

        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQManager:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQAnalyst:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.NPCManager:
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCInterviewer:
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.HQSupport:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQMathStat:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQTester:
                    btnC700.Text = "C-700";
                    break;
            }
        }

        private void GetData()
        {      
            int srowint = Convert.ToInt32(srow);
            
            data_object = new FlagReportData();

            dt = data_object.GetIdList(srowint, flagname, mySectors.ToString().Trim().Replace(",", @"','"));

            dt.DefaultView.Sort = string.Empty;

            dgData.DataSource = dt;
            dgReview.DataSource = dt;

            lblTotal.Text = dgData.RowCount.ToString("N0");

            if (dgData.RowCount == 1)
            {
                lblCase.Text = "CASE";
            }
            else if (dgData.RowCount > 1)
            {
                lblCase.Text = "CASES";
            }
            else
            {
                btnC700.Enabled = false;
                btnPrint.Enabled = false;
            }
        }

        //subsets data based on which radio button is checked
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            string filter = "";

            GetData();

            var filteredDataTable = new DataTable();

            var radiobutton = (RadioButton)sender;
            if (radiobutton.Checked)
            {
                if (sender == rd1a)
                {
                    filter = "";
                }
                else if (sender == rd1f)
                {
                     filter = "owner in ( 'C','D','F')";
                }
                else if (sender == rd1n)
                {
                    filter = "owner = 'N'";
                }
                else if (sender == rd1p)
                {
                    filter = "owner in ( 'S','L','P')";
                }
                else if (sender == rd1u)
                {
                    filter = "owner in ( 'T','E','G','R','O','W')";
                }
                else if (sender == rd1m)
                {
                    filter = "owner = 'M'";
                }

                //use above criteria to filter datatable and store in data view
                DataView dv = new DataView(dt);
                dv.RowFilter = filter;

                //assign dataview to datagrid
                dgData.DataSource = dv;

                //dm032819 added status column in properties -> collection

                lblTotal.Text = dgData.RowCount.ToString("N0");

                if (dgData.RowCount == 1)
                {
                    lblCase.Text = "CASE";
                    btnC700.Enabled = true;
                    btnPrint.Enabled = true;
                }
                else if (dgData.RowCount > 1)
                {
                    lblCase.Text = "CASES";
                    btnC700.Enabled = true;
                    btnPrint.Enabled = true;
                }
                else if (dgData.RowCount == 0)
                {
                    lblCase.Text = "CASES";
                    btnC700.Enabled = false;
                    btnPrint.Enabled = false;
                }
            }
        }

        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //dm032819 changed column 6 to 7 since added status
            //displays state abbreviation instead of fipstate number
            if ((e.ColumnIndex == 7) && (e.RowIndex > -1))
            {
                string fipcode = dgData.Rows[e.RowIndex].Cells[7].Value.ToString();
                string state = GeneralDataFuctions.GetFipState(fipcode);
                e.Value = state;
            }
        }

        //center title based on size of string
        private void lblTitle_SizeChanged(object sender, EventArgs e)
        {
            lblText.Left = (this.ClientSize.Width - lblText.Size.Width) / 2;
        }

        //button to go to previous screen
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.RefreshData();
                CallingForm.Show();
                call_callingFrom = true;
            }

            this.Close();
        }

        //button to pass id list to c700
        private void btnC700_Click(object sender, EventArgs e)
        {
            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;
            if (rows.Count == 0)
                return;

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                int index = dgData.CurrentRow.Index;
                string selected_id = dgData["ID", index].Value.ToString();

                this.Hide(); // hide parent

                string resp = dgData["RESPID", index].Value.ToString();
                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

                tfu.ShowDialog();   // show child
                
            }
            else
            {

                frmC700 fC700 = new frmC700();

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

                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CurrIndex = index;
                fC700.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

                fC700.ShowDialog(); // show child
                
            }
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            
        }

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled ? Color.DarkBlue : Color.Gray;
        }

        //dm032819 changed column 6 to 7 since added status
        //dgReview is grid to print so need to display states abbreviations
        private void dgReview_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == 7) && (e.RowIndex > -1))
            {
                string fipcode = dgReview.Rows[e.RowIndex].Cells[7].Value.ToString();
                string state = GeneralDataFuctions.GetFipState(fipcode);
                e.Value = state;
            }
        }

        //Button to print data
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
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Review Scheduler Records";
            printer.SubTitle = lblText.Text;
            printer.SubTitleAlignment = StringAlignment.Center;
            Margins margins = new Margins(30, 35, 30, 30);
            
            printer.PrintMargins = margins;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Review Scheduler Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";


            printer.PrintDataGridViewWithoutDialog(dgReview);
            Cursor.Current = Cursors.Default;
        }

        private void frmReviewScheduler_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }
    }
}

