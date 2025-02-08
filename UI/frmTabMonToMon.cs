/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTabMonToMon.cs
Programmer    : Christine Zhang
Creation Date : Jan. 3 2017
Parameters    : Newtc, Survey
Inputs        : Vipproj
Outputs       : N/A
Description   : Month to Month screen to view vip change data
Detail Design : 
Other         : Called from frmTotalVip
Rev History   : See Below
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
using System.Globalization;
using System.Text.RegularExpressions;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmTabMonToMon : frmCprsParent
    {
        public frmTotalVip CallingForm = null;
        public string Newtc;
        public string Survey;

        private bool show_preChange;
        private TabMonToMonData data_object;
        private string sdate;
        private DataTable datatable;
        private DataTable clonetable;

        /*flag to use closing the form */
        private bool call_callingFrom = false;

        public frmTabMonToMon()
        {
            InitializeComponent();
        }

        private void frmTabMonToMon_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //get sdate from vipsadj table
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable(); ;

            lblTitle1.Text = GeneralFunctions.GetSurveyText(Survey) + " " + sdate + " Monthly Change Analysis";
            lblTitle2.Text = "For Prel and Rev1";
            lblTitle3.UseMnemonic = false;
            lblTitle3.Text = "Newtc: " + Newtc + " " + GeneralDataFuctions.GetTCDesciption(Newtc);

            show_preChange = true;
            data_object = new TabMonToMonData();
            GetData();
            
            SetTitles(datatable);
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;

            btnRev.Text = "REV1 to REV2";
        }


        private bool GetData()
        {
            datatable = data_object.GetMonthToMonthData(Survey, Newtc, show_preChange);

            //save table
            clonetable = datatable.Clone();

            if (datatable.Rows.Count == 0)
            {
                MessageBox.Show("No data exists");
                return false;
            }
            else
            {
                dgData.DataSource = null; 
                dgData.DataSource = datatable;
                SetColumnHeader();
                dgData.Refresh();

                return true;
            }
        }
        
        //set counts
        private void SetTitles(DataTable dt)
        {
            int num_dec = 0;
            int num_inc = 0;
            int wd = 0;
            int wi = 0;

            if (dt == null)
            {
                lblDec.Text = "0";
                lblInc.Text = "0";
                lblDp.Text = "0%";
                lblIp.Text = "0%";
                lblTot1.Text = "0";
                lblwd.Text = "0";
                lblwi.Text = "0";
                lbltot2.Text = "0";

                return;
            }

            foreach (DataRow row in dt.Rows)
            {
                int diff = Convert.ToInt32(row["diff"]);
                if (diff > 0)
                {
                    num_inc++;
                    wi = wi + diff;
                }
                if (diff < 0)
                {
                    num_dec++;
                    wd = wd + diff;
                }
            }

            int tot = num_dec + num_inc;
            lblDec.Text = num_dec.ToString("###,#");
            lblInc.Text = num_inc.ToString("###,#");
            double ddp = Convert.ToDouble(num_dec)*100/Convert.ToDouble(tot);
            int dp = Convert.ToInt32(Math.Round(ddp, MidpointRounding.AwayFromZero)); 
            double dip = Convert.ToDouble(num_inc)*100/Convert.ToDouble(tot);
            int ip = Convert.ToInt32(Math.Round(dip, MidpointRounding.AwayFromZero)); 
            lblDp.Text = dp.ToString()+"%";
            lblIp.Text = ip.ToString() + "%";
            lblTot1.Text = tot.ToString("###,#");
            lblwd.Text = wd.ToString("###,#");
            lblwi.Text = wi.ToString("###,#");
            lbltot2.Text = (wd + wi).ToString("###,#");

        }

        private void SetColumnHeader()
        {
            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            //get month name
            string curmon = (dt.ToString("MMMM", CultureInfo.InvariantCulture));
            string premon = (dt.AddMonths(-1)).ToString("MMMM", CultureInfo.InvariantCulture);
            string ppmon = (dt.AddMonths(-2)).ToString("MMMM", CultureInfo.InvariantCulture);

            //get abbreviate month name
            string curmons = (dt.ToString("MMM", CultureInfo.InvariantCulture));
            string premons = (dt.AddMonths(-1)).ToString("MMM", CultureInfo.InvariantCulture);
            string ppmons = (dt.AddMonths(-2)).ToString("MMM", CultureInfo.InvariantCulture);

            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[0].Width = 60;
            dgData.Columns[1].HeaderText = "Newtc";
            dgData.Columns[1].Width = 60;
            dgData.Columns[2].HeaderText = "Status";
            dgData.Columns[2].Width = 60;
            dgData.Columns[3].HeaderText = "Seldate";
            dgData.Columns[3].Width = 60;
            dgData.Columns[4].HeaderText = "Strtdate";
            dgData.Columns[5].HeaderText = "Compdate";
            dgData.Columns[6].HeaderText = "Change";
            dgData.Columns[6].DefaultCellStyle.Format = "N0";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            if (show_preChange)
            {
                dgData.Columns[7].HeaderText = "Wgt " + curmons;
                dgData.Columns[7].DefaultCellStyle.Format = "N0";
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].Width = 100;
                dgData.Columns[8].HeaderText = "F";
                dgData.Columns[8].Width = 40;
                dgData.Columns[9].HeaderText = "Wgt " + premons;
                dgData.Columns[9].DefaultCellStyle.Format = "N0";
                dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[9].Width = 100;
                dgData.Columns[10].HeaderText = "F";
                dgData.Columns[10].Width = 40;
                dgData.Columns[11].HeaderText = "Fwgt";
                dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[11].Width = 60;
                dgData.Columns[12].HeaderText = curmon;
                dgData.Columns[12].DefaultCellStyle.Format = "N0";
                dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[12].Width = 100;
                dgData.Columns[13].HeaderText = premon;
                dgData.Columns[13].DefaultCellStyle.Format = "N0";
                dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[13].Width = 100;
            }
            else
            {
                dgData.Columns[7].HeaderText = "Wgt " + premons;
                dgData.Columns[7].DefaultCellStyle.Format = "N0";
                dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[7].Width = 100;
                dgData.Columns[8].HeaderText = "F";
                dgData.Columns[8].Width = 40;
                dgData.Columns[9].HeaderText = "Wgt " + ppmons;
                dgData.Columns[9].DefaultCellStyle.Format = "N0";
                dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[9].Width = 100;
                dgData.Columns[10].HeaderText = "F";
                dgData.Columns[10].Width = 40;
                dgData.Columns[11].HeaderText = "Fwgt";
                dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[11].Width = 60;
                dgData.Columns[12].HeaderText = premon;
                dgData.Columns[12].DefaultCellStyle.Format = "N0";
                dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[12].Width = 100;
                dgData.Columns[13].HeaderText = ppmon;
                dgData.Columns[13].DefaultCellStyle.Format = "N0";
                dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[13].Width = 100;
            }

           
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            //for status, show combo box
            if (cbItem.SelectedIndex == 1)
            {
                cbValueItem.Visible = true;
                txtValueItem.Visible = false;
                cbValueItem.Focus();
                
            }
            // for newtc, selection date, show text box
            else
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;
                if (cbItem.SelectedIndex == 0)
                    txtValueItem.MaxLength = 4;
                else
                    txtValueItem.MaxLength = 6;
                txtValueItem.Focus();
            }
            txtValueItem.Text = "";
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

        private void btnRev_Click(object sender, EventArgs e)
        {
            if (show_preChange)
            {
                show_preChange = false;
                lblTitle2.Text = "For Rev1 to Rev2";
                btnRev.Text = "PREL to REV1";
            }
            else
            {
                show_preChange = true;
                lblTitle2.Text = "For Prel to Rev1";
                btnRev.Text = "REV1 to REV2";
            }
            
            GetData();
            SetTitles(datatable);

            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Text = "";
        }

        private void frmTabMonToMon_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
            
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            txtValueItem.Text = "";
            cbValueItem.SelectedIndex = -1;
            txtValueItem.Visible = false;
            cbValueItem.Visible = false;

            dgData.DataSource = datatable;
            SetColumnHeader();
            SetTitles(datatable);
        }
       

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            string cvalue;
            string colname = cbItem.Text;

            if (cbItem.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a value from dropdown");
                return;
            }
            if ((cbItem.SelectedIndex ==0 || cbItem.SelectedIndex ==2) && txtValueItem.Text == "")
            {
                MessageBox.Show("Please enter a " + cbItem.Text + " value");
                return;
            }
            if (cbItem.SelectedIndex ==1 && cbValueItem.SelectedIndex == -1)
            {
                MessageBox.Show("Please enter a " + cbItem.Text + " value");
                return;
            }
            
            IEnumerable<DataRow> query;
            if (colname == "NEWTC")
            {
                cvalue = txtValueItem.Text.Trim();
                if (cvalue.Length != 4 || !GeneralDataFuctions.CheckNewTC(cvalue))
                {
                    MessageBox.Show("Invalid Newtc");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
                query = from myRow in datatable.AsEnumerable()
                              where myRow.Field<string>(colname).StartsWith(cvalue)
                              select myRow;
            }
            else if (colname == "STATUS")
            {
                cvalue = cbValueItem.Text;
                query = from myRow in datatable.AsEnumerable()
                              where myRow.Field<string>(colname) == cvalue
                              select myRow;
            }
            else
            {
                cvalue = txtValueItem.Text.Trim();
                if (cvalue.Length !=6 || !GeneralFunctions.ValidateDateWithRange(cvalue))
                {
                    MessageBox.Show("Invalid Seldate.");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
                query = from myRow in datatable.AsEnumerable()
                              where myRow.Field<string>("seldate").StartsWith(cvalue)
                              select myRow;
            }

            // Create a table from the query.
            if (query.Count() > 0)
            {
                DataTable boundTable = query.CopyToDataTable<DataRow>();
                dgData.DataSource = boundTable;
                SetColumnHeader();
                SetTitles(boundTable);
            }
            else
            {
                MessageBox.Show("No data exists");
                dgData.DataSource = clonetable;
                SetTitles(null);
            }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
     
            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;
            int index = dgData.CurrentRow.Index;
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

        private void btnExport_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text file|*.txt";
            saveFileDialog1.Title = "Save an File";
            var result =saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            // Choose whether to write header. Use EnableWithoutHeaderText instead to omit header.
            dgData.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            // Select all the cells
            dgData.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dgData.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            System.IO.File.WriteAllText(saveFileDialog1.FileName, Clipboard.GetText(TextDataFormat.CommaSeparatedValue));

            dgData.ClearSelection();
            dgData.Rows[0].Selected = true;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgData.RowCount > 230)
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
            printer.Title = lblTitle1.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = lblTitle2.Text + "   " + lblTitle3.Text;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Month to Month Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            
            dgData.Columns[7].Width = 60;
            dgData.Columns[8].Width = 30;
            dgData.Columns[9].Width = 60;
            dgData.Columns[10].Width = 30;
            dgData.Columns[11].Width = 40;
            dgData.Columns[12].Width = 80;
            dgData.Columns[13].Width = 80;
            
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgData.Columns[7].Width = 100;
            dgData.Columns[8].Width = 40;
            dgData.Columns[9].Width = 100;
            dgData.Columns[10].Width = 40;
            dgData.Columns[11].Width = 60;
            dgData.Columns[12].Width = 100;
            dgData.Columns[13].Width = 100;
            Cursor.Current = Cursors.Default;
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void lblTitle3_SizeChanged(object sender, EventArgs e)
        {
            lblTitle3.Left = (this.ClientSize.Width - lblTitle3.Size.Width) / 2;

        }

        private void btnC700_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnRev_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnExport_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
