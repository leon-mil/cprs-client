/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabAnnualBeaRev.cs
Programmer    : Christine Zhang
Creation Date : April. 1 2019
Parameters    : 
Inputs        : SelectedTc, SelectedSurvey, Callingform
Outputs       : N/A
Description   : create annual bea revision screen compare vip data
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
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
using DGVPrinterHelper;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;
using System.Threading;
using System.Globalization;

namespace Cprs
{
    public partial class frmTabAnnualBeaRev : frmCprsParent
    {
        public frmTabAnnualBeaRev()
        {
            InitializeComponent();
        }

        public frmTabAnnualBea CallingForm = null;
        public string SelectedTc;
        public string SelectedSurvey;

        private string sdate;
        private int year1;
        private int year2;
        private string subset_case = "ALL CASES";

        private TabAnnualBeaData data_object = new TabAnnualBeaData();
        private DataTable dtmain;

        private bool call_callingFrom = false;

        private void frmTabAnnualBeaRev_Load(object sender, EventArgs e)
        {
            //update appropriate datatables with james bond id
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //get sdate from vipsadj table
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            int cur_year = Convert.ToInt16(sdate.Substring(0, 4));
            int cur_mon = Convert.ToInt16(sdate.Substring(4, 2));

            if (cur_mon > 2)
            {
                year1 = cur_year - 1;
                year2 = cur_year - 2;
            }
            else
            {
                year1 = cur_year - 2;
                year2 = cur_year - 3;
            }

            for (int i = 12; i >0; i--)
            {
                if (i<10)
                    cbYear.Items.Add(year1.ToString() + "0" + i.ToString());
                else
                    cbYear.Items.Add(year1.ToString() + i.ToString());
            }
            for (int i = 12; i>0; i--)
            {
                if (i < 10)
                    cbYear.Items.Add(year2.ToString() + "0" + i.ToString());
                else
                    cbYear.Items.Add(year2.ToString() + i.ToString());
            }

            cbYear.SelectedIndex = 0;
            txtValueItem.Visible = false;
        }
        
        //set up titles
        private void UpdateTitles()
        {
            lblTitle.Text = GeneralFunctions.GetSurveyText(SelectedSurvey) + " Revision Analysis";
            lblTitle1.Text = "For " + cbYear.Text + " VIP";
            lblTitle2.Text = subset_case;
            lblTitle3.UseMnemonic = false;
            lblTitle3.Text = "Newtc: " + SelectedTc + " " + GeneralDataFuctions.GetTCDesciption(SelectedTc);
        }

        //set counts for display panel
        private void SetTitles(DataTable dt)
        {
            int num_dec = 0;
            int num_inc = 0;
            int wd = 0;
            int wi = 0;
            int dp = 0;
            int ip = 0;
            double ddp = 0;
            double dip = 0;
            int blank = 0;

            if (dt == null)
            {
                lblDecP.Text = "0";
                lblIncP.Text = "0";
                lblDecPctP.Text = "0%";
                lblIncPctP.Text = "0%";
                lblTotP.Text = "0";
                lblDecW.Text = "0";
                lblIncW.Text = "0";
                lblTotW.Text = "0";

                return;
            }

            //calculates counts for increases and decreases
            foreach (DataRow row in dt.Rows)
            {
                int diff = Convert.ToInt32(row["change"]);
                if (diff > 0)
                {
                    num_inc++;
                    wi = wi + diff;
                }
                else if (diff < 0)
                {
                    num_dec++;
                    wd = wd + diff;
                }
                else
                {
                    blank = blank + 1;
                }
            }

            int tot = num_dec + num_inc;

            //calculates percentages of increases and decreases
            if (tot > 0)
            {
                ddp = Convert.ToDouble(num_dec) * 100 / Convert.ToDouble(tot);
                dp = Convert.ToInt32(Math.Round(ddp, MidpointRounding.AwayFromZero));
                dip = Convert.ToDouble(num_inc) * 100 / Convert.ToDouble(tot);
                ip = Convert.ToInt32(Math.Round(dip, MidpointRounding.AwayFromZero));
                lblDecPctP.Text = dp.ToString() + "%";
                lblIncPctP.Text = ip.ToString() + "%";
            }
            else
            {
                lblDecPctP.Text = "0%";
                lblIncPctP.Text = "0%";
            }

            lblDecP.Text = num_dec.ToString("N0");
            lblIncP.Text = num_inc.ToString("N0");
            lblTotP.Text = tot.ToString("N0");
            lblDecW.Text = wd.ToString("N0");
            lblIncW.Text = wi.ToString("N0");
            lblTotW.Text = (wd + wi).ToString("N0");
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                CallingForm.RefreshForm(false);
                call_callingFrom = true;
            }

            this.Close();
        }

        private void frmTabAnnualBeaRev_FormClosing(object sender, FormClosingEventArgs e)
        {
            //update appropriate datatables with james bond id
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        private void btnBtoI_Click(object sender, EventArgs e)
        {
            subset_case = "Blank to Impute";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            
            txtValueItem.Visible = false;
            
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag = '' and curflag in ('M','I')";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }  
        }

        private void btnBtoR_Click(object sender, EventArgs e)
        {
            subset_case = "Blank to Report";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            
            txtValueItem.Visible = false;

            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag = '' and curflag in ('R','A','O')";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnRtoB_Click(object sender, EventArgs e)
        {
            subset_case = "Report to Blank";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            
            txtValueItem.Visible = false;
            
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag in ('R','A','O') and curflag = ''";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnItoB_Click(object sender, EventArgs e)
        {
            subset_case = "Impute to Blank";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
           
            txtValueItem.Visible = false;
        
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag in ('M','I') and curflag = ''";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnItoR_Click(object sender, EventArgs e)
        {
            subset_case = "Impute to Report";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            
            txtValueItem.Visible = false;
            
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag in ('M','I') and curflag in ('R','A','O')";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnItoI_Click(object sender, EventArgs e)
        {
            subset_case = "Impute to Impute";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            
            txtValueItem.Visible = false;
        
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag in ('M', 'I') and curflag in ('M', 'I')";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnRtoR_Click(object sender, EventArgs e)
        {
            subset_case = "Report to Report";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
        
            txtValueItem.Visible = false;
         
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "preflag in ('R','A','O') and curflag in ('R','A','O')";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnSChange_Click(object sender, EventArgs e)
        {
            subset_case = "Status Changed";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
           
            txtValueItem.Visible = false;
        
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            dv.RowFilter = "(status in ('1','2','3','7','8') and pstatus in ('4','5','6')) or (status in ('4', '5', '6') and pstatus not in ('4','5','6'))";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnOChange_Click(object sender, EventArgs e)
        {
            subset_case = "Owner Changed";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
          
            txtValueItem.Visible = false;
     
            UpdateTitles();

            DataView dv = new DataView(dtmain);

            string not_owner_str=string.Empty;

            if (SelectedSurvey == "N")
                not_owner_str = "powner <> 'N'";
            else if (SelectedSurvey == "M")
                not_owner_str = "powner <> 'M'";
            else if (SelectedSurvey == "P")
                not_owner_str = "powner not in ('S', 'P', 'L')";
            else if (SelectedSurvey == "F")
                not_owner_str = "powner not in ('C', 'D', 'F')";
            else if (SelectedSurvey == "U")
                not_owner_str = "powner not in ('T', 'E', 'G', 'R', 'O', 'W')";

            dv.RowFilter = not_owner_str;
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void btnTChange_Click(object sender, EventArgs e)
        {
            subset_case = "TC Changed";
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
           
            txtValueItem.Visible = false;
          
            UpdateTitles();

            DataView dv = new DataView(dtmain);
            if (SelectedTc != "1T")
                dv.RowFilter = "substring(newtc, 1,2) <> substring(pnewtc, 1, 2)";
            else
                dv.RowFilter = "substring(pnewtc, 1, 2) < '20'";
            dv.Sort = "change DESC";

            //subset filter
            DataTable dt1 = dv.ToTable();

            //assign dataview to datagrid
            dgData.DataSource = dv;

            SetColumnHeader();
            SetTitles(dt1);

            if (dt1.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
            }
        }

        private void cbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {
            dtmain = data_object.GetBEARevisionData(SelectedSurvey, SelectedTc, cbYear.Text);
            dgData.DataSource = dtmain;
            SetColumnHeader();

            SetTitles(dtmain);

            UpdateTitles();
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            dgData.Columns[0].HeaderText = "ID ";
            dgData.Columns[0].Width = 60;
            dgData.Columns[1].HeaderText = "Newtc ";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[1].Width = 45;
            dgData.Columns[2].HeaderText = "Status";
            dgData.Columns[2].Width = 50;
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[3].HeaderText = "Owner";
            dgData.Columns[3].Width = 45;
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[4].HeaderText = "Seldate";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[4].Width = 60;
            dgData.Columns[5].HeaderText = "Strtdate";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[5].Width = 60;
            dgData.Columns[6].HeaderText = "Change";
            dgData.Columns[6].Width = 60;
            dgData.Columns[6].DefaultCellStyle.Format = "N0";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[7].HeaderText = "Curr Wgt";
            dgData.Columns[7].Width = 60;
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "Flag";
            dgData.Columns[8].Width = 40;
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[9].HeaderText = "Prev Wgt";
            dgData.Columns[9].Width = 60;
            dgData.Columns[9].DefaultCellStyle.Format = "N0";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[10].HeaderText = "Flag";
            dgData.Columns[10].Width = 40;
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[11].HeaderText = "Fwgt";
            dgData.Columns[11].Width = 60;
            dgData.Columns[11].DefaultCellStyle.Format = "N2";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[12].HeaderText = "Pwgt";
            dgData.Columns[12].Width = 60;
            dgData.Columns[12].DefaultCellStyle.Format = "N2";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[13].HeaderText = "Curr";
            dgData.Columns[13].Width = 60;
            dgData.Columns[13].DefaultCellStyle.Format = "N0";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[14].HeaderText = "Prev";
            dgData.Columns[14].Width = 60;
            dgData.Columns[14].DefaultCellStyle.Format = "N0";
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[15].HeaderText = "Owner";
            dgData.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[15].Width = 60;
            dgData.Columns[16].Visible = false;
            dgData.Columns[17].Visible = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgData.RowCount > 235)
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
            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = lblTitle1.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Annual BEA Revision Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.PrintDataGridViewWithoutDialog(dgPrint);

            //release printer
            GeneralFunctions.releaseObject(printer);
            Cursor.Current = Cursors.Default;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            
            txtValueItem.Visible = false;
            subset_case = "ALL CASES";
            UpdateTitles();

            dgData.DataSource = dtmain;
            SetColumnHeader();
            SetTitles(dtmain);
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValueItem.Visible = true;
            txtValueItem.Focus();
            txtValueItem.Clear();

            //verify id/respid is 7 digits in length
            if (cbItem.SelectedIndex == 0)
            {
                txtValueItem.MaxLength = 7;
            }

            //verify newtc is not over 4 digits in length
            else if (cbItem.SelectedIndex == 1)
            {
                txtValueItem.MaxLength = 4;
            }

            //verify strtdate is not over 6 digits in length
            if (cbItem.SelectedIndex == 2)
            {
                txtValueItem.MaxLength = 6;
            }

            txtValueItem.Text = "";
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string colname = cbItem.Text;
            string txt;

            if (cbItem.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a value from dropdown");
                return;
            }
            if (txtValueItem.Text == "")
            {
                MessageBox.Show("Please enter a " + cbItem.Text + " value");
                return;
            }

            string filter = "";

            //validate id is 7 digits and valid
            if (cbItem.SelectedIndex == 0)
            {
                txtValueItem.Visible = true;

                if (!(txtValueItem.Text.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits.");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    return;
                }
                else
                {
                    if (GeneralDataFuctions.ValidateSampleId(txtValueItem.Text))
                    {
                        txt = txtValueItem.Text;
                        filter = "id = " + txt.AddSqlQuotes();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Id");
                        txtValueItem.Focus();
                        txtValueItem.Clear();
                        return;
                    }
                }
            }

            //validate newtc is 4 digit and in valid list or newtclist
            else if (cbItem.SelectedIndex == 1)
            {
                txtValueItem.Visible = true;

                if ((txtValueItem.Text.Trim() == "") || ((txtValueItem.TextLength != 4)))
                {
                    MessageBox.Show("Invalid NEWTC");
                    txtValueItem.Focus();
                    txtValueItem.Clear();

                    return;
                }
                else if (txtValueItem.Text.Length == 4)
                {
                    //check to see if 4 digit newtc is valid in newtclist table
                    if (!(GeneralDataFuctions.CheckNewTC(txtValueItem.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtValueItem.Focus();
                        txtValueItem.Clear();

                        return;
                    }
                    else
                    {
                        txt = txtValueItem.Text;
                        filter = "newtc = " + txt.AddSqlQuotes();
                    }
                }
            }

            //validate startdate
            if (cbItem.SelectedIndex == 2)
            {
                txtValueItem.Visible = true;

                if (!(txtValueItem.Text.Length == 6))
                {
                    MessageBox.Show("Invalid STRTDATE");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    return;
                }
                else
                {
                    txt = txtValueItem.Text;
                    filter = "strtdate = " + txt.AddSqlQuotes();
                }
            }

            //use above criteria to filter datatable and store in data view
            var filteredDataTable = new DataTable();

            DataView dv1 = new DataView(dtmain);
            dv1.RowFilter = filter;

            DataTable dtSearch = dv1.ToTable();
            dgData.DataSource = dv1;

            //SetColumnHeader();
            subset_case = "ALL CASES";
            UpdateTitles();
            SetTitles(dtSearch);

            if (dtSearch.Rows.Count == 0)
            {
                MessageBox.Show("No Data Exists");
                txtValueItem.Focus();
                txtValueItem.Clear();
                return;
            }
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

        private void txtValueItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnSearch_Click(sender, e);
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
