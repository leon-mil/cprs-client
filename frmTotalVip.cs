/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmTotalVip.cs
Programmer    : Christine Zhang
Creation Date : Dec. 19 2016
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create TotalVip screen to view vip data
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date : March 30, 2021
Modified By   : Kevin Montgomery
Keyword       :
Change Request:
Description   : Comment out Printing of Ratio Adjust Listings
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
using System.IO;
using System.Management;


namespace Cprs
{
    public partial class frmTotalVip : frmCprsParent
    {
        private string owner = "";
        private bool show_subtc;
        private TotalVipData data_object;
        private string level = "1";
        private string selected_newtc;
        private MySector ms;
        private string sdate;
        private int selected_index;
        private bool data_loading = false;

        public frmTotalVip()
        {
            InitializeComponent();
        }

        private void frmTotalVip_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //get my sector data
            MySectorsData md = new MySectorsData();
            ms = md.GetMySectorData(UserInfo.UserName);
            if (ms == null)
                ckMySec.Visible = false;
            else
                ckMySec.Visible = true;

            data_object = new TotalVipData();
            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            //set initial variables, get into main tc
            level = "1";
            owner = "T";
            selected_newtc = "";
            selected_index = 0;
            GetData(owner, level, selected_newtc,false);
            SetColumnHeader();

            btnMonth.Enabled = false;
            btnWork.Enabled = false;
            btnRevision.Enabled = false;
            show_subtc = false;

            lblTitle.Text = "Total " + sdate + " Seasonally Adjusted VIP";
            lbllabel.Text = "Double Click a Line to Expand";

            //set button Federal Ajust
            btnAdjust.Visible = false;

        }

        //get data based on owner, level and newtc
        private void GetData(string owner, string level, string newtc, bool setSelection)
        {
            data_loading = true;
            DataTable table;
            table = data_object.GetVipTotalData(sdate, owner, level, newtc);

            int num_row = table.Rows.Count;

            //get only my sector data, if not set in my sector, delete the sectors
            if (level == "1" && ckMySec.Checked)
            {
                var ulist = new List<string>();
                if (ms.Sect00 == "N")
                    table.Rows[1].Delete();
                if (ms.Sect01 == "N")
                    table.Rows[2].Delete();
                if (ms.Sect02 == "N")
                    table.Rows[3].Delete();
                if (ms.Sect03 == "N")
                    table.Rows[4].Delete();
                if (ms.Sect04 == "N")
                    table.Rows[5].Delete();
                if (ms.Sect05 == "N")
                    table.Rows[6].Delete();
                if (ms.Sect06 == "N")
                    table.Rows[7].Delete();
                if (ms.Sect07 == "N")
                    table.Rows[8].Delete();
                if (ms.Sect08 == "N")
                    table.Rows[9].Delete();
                if (ms.Sect09 == "N")
                    table.Rows[10].Delete();
                if (ms.Sect10 == "N")
                    table.Rows[11].Delete();
                if (ms.Sect11 == "N")
                    table.Rows[12].Delete();
                if (ms.Sect12 == "N")
                    table.Rows[13].Delete();
                if (ms.Sect13 == "N")
                    table.Rows[14].Delete();
                if (ms.Sect14 == "N")
                    table.Rows[15].Delete();
                if (ms.Sect15 == "N")
                    table.Rows[16].Delete();
                if (ms.Sect16 == "N")
                    table.Rows[17].Delete();

                //for IT, delete all newtc from 1T to 39
                if (ms.Sect1T == "N")
                {
                    for (int i = 18; i < num_row; i++)
                    {
                        table.Rows[i].Delete();
                    }
                }  
            }
            
            dgData.DataSource = null;
            dgData.DataSource = table;
            
            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //set up selection
            dgData.ClearSelection();
            
            data_loading = false;

            if (dgData.Rows.Count > 0)
            { 
                if (setSelection)
                {
                    if (dgData.Rows.Count > selected_index)
                        dgData.Rows[selected_index].Selected = true;
                    else
                        dgData.Rows[0].Selected = true;
                }
                else
                {
                    dgData.Rows[0].Selected = true;
                }
            }
           
        }

        private void SetColumnHeader()
        {
            if (!this.Visible)
                return;

            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sdate,"yyyyMM",CultureInfo.InvariantCulture);

            //get month name
            string curmon = (dt.ToString("MMMM", CultureInfo.InvariantCulture));
            string premon = (dt.AddMonths(-1)).ToString("MMMM", CultureInfo.InvariantCulture);
            string ppmon = (dt.AddMonths(-2)).ToString("MMMM", CultureInfo.InvariantCulture);
            string p3mon = (dt.AddMonths(-3)).ToString("MMMM", CultureInfo.InvariantCulture);
            string p4mon = (dt.AddMonths(-4)).ToString("MMMM", CultureInfo.InvariantCulture);

            //get abbreviate month name
            string curmons = (dt.ToString("MMM", CultureInfo.InvariantCulture));
            string premons = (dt.AddMonths(-1)).ToString("MMM", CultureInfo.InvariantCulture);
            string ppmons = (dt.AddMonths(-2)).ToString("MMM", CultureInfo.InvariantCulture);
            string p3mons = (dt.AddMonths(-3)).ToString("MMM", CultureInfo.InvariantCulture);
            string p4mons = (dt.AddMonths(-4)).ToString("MMM", CultureInfo.InvariantCulture);

            dgData.Columns[0].HeaderText = " ";
            dgData.Columns[0].Width = 165;
            dgData.Columns[0].Frozen = true;
            dgData.Columns[1].HeaderText = curmon;
            dgData.Columns[1].DefaultCellStyle.Format = "N1";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[1].Width = 100;
            dgData.Columns[2].HeaderText = premon;
            dgData.Columns[2].DefaultCellStyle.Format = "N1";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].Width = 100;
            dgData.Columns[3].HeaderText = ppmon;
            dgData.Columns[3].DefaultCellStyle.Format = "N1";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].Width = 100;
            dgData.Columns[4].HeaderText = "Previous " + premon;
            dgData.Columns[4].DefaultCellStyle.Format = "N1";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = "Previous " + ppmon;
            dgData.Columns[5].DefaultCellStyle.Format = "N1";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].HeaderText = curmons + "-" + premons + "/" + premons;
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[7].HeaderText = premons + "-" + ppmons + "/" + ppmons;
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[8].HeaderText = "1st Revision Change";
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[9].HeaderText = "2nd Revision Change";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[10].HeaderText = p3mon;
            dgData.Columns[10].DefaultCellStyle.Format = "N1";
            dgData.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[11].HeaderText = p4mon;
            dgData.Columns[11].DefaultCellStyle.Format = "N1";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[12].HeaderText = "Previous " + p3mon;
            dgData.Columns[12].DefaultCellStyle.Format = "N1";
            dgData.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[13].HeaderText = "Previous " + p4mon;
            dgData.Columns[13].DefaultCellStyle.Format = "N1";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[14].HeaderText = ppmons + "-" + p3mons + "/" + p3mons;
            dgData.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[15].HeaderText = p3mons + "-" + p4mons + "/" + p4mons;
            dgData.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[16].HeaderText = "3rd Revision Change";
            dgData.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[17].HeaderText = "4th Revision Change";
            dgData.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgData.Columns[18].Visible = false;
            dgData.Columns[19].Visible = false;
            dgData.Columns[20].Visible = false;
            dgData.Columns[21].Visible = false;
            dgData.Columns[22].Visible = false;
            dgData.Columns[23].Visible = false;
        }

        private void ckMySec_CheckedChanged(object sender, EventArgs e)
        {
           GetData(owner, level, selected_newtc,false);
           SetColumnHeader();
        }


        //Make rest of radiobutton unchecked, except the passed radio button 
        private void UpdateOtherRdBoxes(RadioButton rb)
        {
            foreach (Control c in this.Controls)
            {
                if (c is RadioButton && c.Name != rb.Name)
                {
                    ((RadioButton)c).Checked = false;
                } 
            }
        }

        //set enable for month to month and worksheet and revision buttons
        private void SetBtns()
        {
            int selected_index = 0;

            if (dgData.SelectedRows.Count > 0)
                selected_index = dgData.SelectedRows[0].Index;

            if (rd1a.Checked || ((level=="1") && selected_index == 0))
            {
                btnMonth.Enabled = false;
                btnWork.Enabled = false;
                btnRevision.Enabled = false;
            }
            else
            {
                btnMonth.Enabled = true;
                btnWork.Enabled = true;
                btnRevision.Enabled = true;
            }

            if (rd1f.Checked && (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer))
                btnAdjust.Visible = true;
            else
                btnAdjust.Visible = false;
        }

        private void rd1a_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1a.Checked)
            {
                owner = "T";
                UpdateOtherRdBoxes(rd1a);
                SetBtns();
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();
                lblTitle.Text = "Total " + sdate + " Seasonally Adjusted VIP";
            }
            
        }

        private void rd1p_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1p.Checked)
            {
                owner = "P";
                UpdateOtherRdBoxes(rd1p);
                SetBtns();
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();
                lblTitle.Text = "State and Local " + sdate + " Seasonally Adjusted VIP";
            }
        }

        private void rd1n_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1n.Checked)
            {
                owner = "N";
                UpdateOtherRdBoxes(rd1n);
                SetBtns();
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();
                lblTitle.Text = "Nonresidential " + sdate + " Seasonally Adjusted VIP";
            }
        }

        private void rd1f_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1f.Checked)
            {
                owner = "F";
                UpdateOtherRdBoxes(rd1f);
                SetBtns();
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();
                lblTitle.Text = "Federal " + sdate + " Seasonally Adjusted VIP";
            }
            
        }

        private void rd1m_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1m.Checked)
            {
                owner = "M";
                UpdateOtherRdBoxes(rd1m);
                SetBtns();
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();
                lblTitle.Text = "Multifamily " + sdate + " Seasonally Adjusted VIP";
            }
        }

        private void rd1u_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1u.Checked)
            {
                owner = "U";
                UpdateOtherRdBoxes(rd1u);
                SetBtns();
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();
                lblTitle.Text = "Utilities " + sdate + " Seasonally Adjusted VIP";
            }
            
        }

        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            if (!data_loading)
                selected_index = dgData.SelectedRows[0].Index;
            SetBtns();
        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            string snewtc = dgData.SelectedRows[0].Cells[18].Value.ToString().Trim();
            selected_newtc = snewtc.Substring(0, 2);

            if (Convert.ToDouble(dgData.SelectedRows[0].Cells[1].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[2].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[3].Value) == 0)
            {
                if (Convert.ToDouble(dgData.SelectedRows[0].Cells[19].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[20].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[21].Value) == 0)
                {
                    MessageBox.Show("No data exists for this TC.");
                    return;
                }
            }
            

            //}
            this.Hide();
            frmTabMonToMon popup = new frmTabMonToMon();
            
            popup.CallingForm = this;
            popup.Newtc = selected_newtc;
            popup.Survey = owner;

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            popup.Show();  // show child
        }


        private void dgData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (!show_subtc && dgData.CurrentRow.Index != 0)
            {
                //go to subtc 
                show_subtc = true;
                level = "2";

                selected_newtc = dgData.CurrentRow.Cells[18].Value.ToString().Trim();

                //Get data
                GetData(owner, level, selected_newtc,false);
                SetColumnHeader();

                if (ckMySec.Visible)
                    ckMySec.Enabled = false;

                lbllabel.Text = "Double Click any Line to Restore";

            }
            else if (show_subtc)
            {
                //go back to main tc 
                show_subtc = false;
                level = "1";
                selected_newtc = "";
                GetData(owner, level, selected_newtc, true);
                SetColumnHeader();

                if (ckMySec.Visible)
                    ckMySec.Enabled = true;

                lbllabel.Text = "Double Click a Line to Expand";
            }
        }

        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            if (dgData.Columns[e.ColumnIndex].Name.Equals("title"))
            {
                String stringValue = e.Value.ToString();
                if (stringValue == null || stringValue.Trim() == "") return;

                if (stringValue.Substring(0, 1) == "*")
                {
                    if (stringValue.Substring(1, 2) == "1T")
                    {
                        string next = stringValue.Substring(2, 2);
                        string justns = new String(next.Where(Char.IsDigit).ToArray());
                        if (justns.Length == 1)
                            e.Value = "    " + stringValue;
                        else if (justns.Length == 2)
                            e.Value = "          " + stringValue;
                        else
                            e.Value = stringValue;
                    }
                    else
                    {
                        string next = stringValue.Substring(1, 4);
                        string justNumbers = new String(next.Where(Char.IsDigit).ToArray());

                        switch (justNumbers.Length)
                        {
                            case 3:
                                e.Value = "    " + stringValue;
                                break;
                            case 4:
                                e.Value = "         " + stringValue;
                                break;
                            default:
                                e.Value = stringValue;
                                break;
                        }
                    }
                }
                else
                {
                    if (stringValue.Substring(0, 2) == "1T")
                    {
                        string next = stringValue.Substring(2, 2);
                        string justns = new String(next.Where(Char.IsDigit).ToArray());
                        if (justns.Length == 1)
                            e.Value = "    " + stringValue;
                        else if (justns.Length == 2)
                            e.Value = "          " + stringValue;
                        else
                            e.Value = stringValue;
                    }
                    else
                    {
                        string next = stringValue.Substring(0, 4);
                        string justNumbers = new String(next.Where(Char.IsDigit).ToArray());

                        switch (justNumbers.Length)
                        {
                            case 3:
                                e.Value = "    " + stringValue;
                                break;
                            case 4:
                                e.Value = "         " + stringValue;
                                break;
                            default:
                                e.Value = stringValue;
                                break;
                        }
                    }
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            PrintData(1);
            PrintData(2);
            Cursor.Current = Cursors.Default;
        }

        private void PrintData(int num)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Total VIP Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            /*Hide the columns */
            if (num == 1)
            {
                printer.HideColumns.Add("cm3");
                printer.HideColumns.Add("cm4");
                printer.HideColumns.Add("pm2");
                printer.HideColumns.Add("pm3");
                printer.HideColumns.Add("p3");
                printer.HideColumns.Add("p4");
                printer.HideColumns.Add("r3");
                printer.HideColumns.Add("r4");
                dgData.Columns[1].Width = 80;
                dgData.Columns[2].Width = 80;
                dgData.Columns[3].Width = 80;
            }
            else
            {
                printer.HideColumns.Add("cm0");
                printer.HideColumns.Add("cm1");
                printer.HideColumns.Add("cm2");
                printer.HideColumns.Add("pm0");
                printer.HideColumns.Add("pm1");
                printer.HideColumns.Add("p1");
                printer.HideColumns.Add("p2");
                printer.HideColumns.Add("r1");
                printer.HideColumns.Add("r2");
                
            }
            dgData.Columns[0].Width = 120;
            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 165;
            if (num ==1)
            {
                dgData.Columns[1].Width = 100;
                dgData.Columns[2].Width = 100;
                dgData.Columns[3].Width = 100;
            }
            
        }

        private void lblTitle_SizeChanged(object sender, EventArgs e)
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Size.Width) / 2;

        }

        private void frmTotalVip_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

        private void btnWork_Click(object sender, EventArgs e)
        {
            selected_newtc = dgData.SelectedRows[0].Cells[18].Value.ToString().Trim();
            
            if (Convert.ToDouble(dgData.SelectedRows[0].Cells[1].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[2].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[3].Value) == 0)
            {
                if (Convert.ToDouble(dgData.SelectedRows[0].Cells[19].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[20].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[21].Value) == 0)
                {
                    MessageBox.Show("No data exists for this TC.");
                    return;
                }
            }

            //check lock
            TabLockData lock_data = new TabLockData();
            string ntc = selected_newtc.Substring(0, 2);
            string tc;
            string locked_by;
            if ( ntc ==  "1T")
                tc = "";
            else if (Convert.ToInt16(ntc) > 16)
                tc = "1T";
            else
                tc = ntc;

            if (ntc == "1T")
            {
                this.Hide();
                ShowWorksheet(false);
                return;
            }

            locked_by = lock_data.GetTabLock(tc);
            if (locked_by != string.Empty)
            {
                DialogResult dialogResult = MessageBox.Show("TC is locked by " + locked_by + ". Continue with read only access?", "Verify", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.No)
                    return;
                else
                {
                    this.Hide();
                    ShowWorksheet(false);
                }
            }
            else
            {
                this.Hide();
                ShowWorksheet(true);   
            }

            //reset selected_newtc
            selected_newtc = selected_newtc.Substring(0, 2);
        }

        //Refresh screen after worksheet
        public void RefreshForm(bool reload_data)
        {
            if (reload_data)
            {
                //set initial variables, get into main tc
                string tc2 = selected_newtc.Substring(0, 2);
                
                GetData(owner, level, tc2, true);
                SetColumnHeader();

            }

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
        }

        //Display worksheet
        private void ShowWorksheet(bool isEditable)
        {
            frmWorkSheet popup = new frmWorkSheet();
            
            popup.CallingForm = this;
            popup.Newtc = selected_newtc;
            popup.Survey = owner;
            popup.Editable = isEditable;

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            popup.Show();
        }

        private void btnRevision_Click(object sender, EventArgs e)
        {
            string snewtc = dgData.SelectedRows[0].Cells[18].Value.ToString().Trim();
            selected_newtc = snewtc.Substring(0, 2);
            
            if (Convert.ToDouble(dgData.SelectedRows[0].Cells[1].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[2].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[3].Value) == 0)
            {
                if (Convert.ToDouble(dgData.SelectedRows[0].Cells[19].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[20].Value) == 0 && Convert.ToDouble(dgData.SelectedRows[0].Cells[21].Value) == 0)
                {
                    MessageBox.Show("No data exists for this TC.");
                    return;
                }
            }

            this.Hide();
            frmRevision popup = new frmRevision();
            popup.CallingForm = this;
            popup.Newtc = selected_newtc;
            popup.Survey = owner;

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            popup.Show();
            
        }
       
        private void btnAdjust_Click(object sender, EventArgs e)
        {
            
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text file|*.dat";
            saveFileDialog1.Title = "Save an File";
            saveFileDialog1.FileName = "federal_ratio_fact_" + sdate + ".dat";

            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            string file_Name = saveFileDialog1.FileName;
            FileInfo fileInfo = new FileInfo(file_Name);
            string dir = MapNetwork.Pathing.GetUNCPath(fileInfo.DirectoryName);

            //set up file name
            string fed_fact_file = dir + "\\federal_ratio_fact_" + sdate + ".dat";
            string fed_tab1_file = dir + "\\federal_ratio_tab1_" + sdate + ".dat";
            string fed_tab2_file = dir + "\\federal_ratio_tab2_" + sdate + ".dat";
            string fed_tab3_file = dir + "\\federal_ratio_tab3_" + sdate + ".dat";

            //set FedRatioCal data
            FedRatioCalData fd = new FedRatioCalData();

            //Save original BST and B_CM0
            DataTable bt = fd.GetOriginalBValue();

            //Calculate Federal Ratio
            DataTable dt = fd.UpdateFedRatioCalData(sdate);

            //Print Federal Ratio Factor 
            dgPrint.DataSource = dt;
            dgPrint.Columns[0].HeaderText = "Survey Date";
            dgPrint.Columns[1].HeaderText = "VIP from Agency";
            dgPrint.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[1].DefaultCellStyle.Format = "N1";
            dgPrint.Columns[2].HeaderText = "VIP from Projects";
            dgPrint.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[2].DefaultCellStyle.Format = "N1";
            dgPrint.Columns[3].HeaderText = "Adjustment Factor";
            dgPrint.Columns[3].Width = 200;
            //PrintFedRatio(); 
            SaveFile(fed_fact_file);

            //Tabluation Federal, update vipsadj table
            fd.UpdateVipData();

            //Reload Total Vip screen
            level = "1";
            owner = "F";
            selected_newtc = "";
            selected_index = 0;
            GetData(owner, level, selected_newtc, false);
            SetColumnHeader();
            rd1f.Checked = true;

            //Convert sdate to datatime
            var datet = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            //get month name
            string premon = (datet.AddMonths(-1)).ToString("yyyyMM");
            string ppmon = (datet.AddMonths(-2)).ToString("yyyyMM");

            //Print Federal Ratio Current month
            DataTable pt1 = fd.GetFedRatioCurrMonPrint(bt);
            dgPrint.DataSource = null;
            dgPrint.DataSource = pt1;
            SetPrintColumnHearder();
            //PrintAfterFedRation(sdate, 1);
            SaveFile(fed_tab1_file);

            //Print Federal Ratio Prior Month
            DataTable pt2 = fd.GetFedRatioPriorMonPrint(bt);
            dgPrint.DataSource = null;
            dgPrint.DataSource = pt2;
            SetPrintColumnHearder();
            //PrintAfterFedRation(premon, 2);
            SaveFile(fed_tab2_file);

            //Print Federal Ratio Two Month Prior Month
            DataTable pt3 = fd.GetFedRatio2PriorMonPrint(bt);
            dgPrint.DataSource = null;
            dgPrint.DataSource = pt3;
            SetPrintColumnHearder();
            //PrintAfterFedRation(ppmon, 3);
            SaveFile(fed_tab3_file);

            //if the current month is May
            string mm = sdate.Substring(4, 2);
            if (mm == "05")
            {
                string fed_tab4_file = dir + "\\federal_ratio_tab4_" + sdate + ".dat";
                string fed_tab5_file = dir + "\\federal_ratio_tab5_" + sdate + ".dat";
                
                //Print Federal Ratio 3 Prior Month
                DataTable pt4 = fd.GetFedRatio3PriorMonPrint(bt);
                dgPrint.DataSource = null;
                dgPrint.DataSource = pt4;
                SetPrintColumnHearder();
                //PrintAfterFedRation((datet.AddMonths(-3)).ToString("yyyyMM"), 4);
                SaveFile(fed_tab4_file);

                //Print Federal Ratio 4 Prior Month
                DataTable pt5 = fd.GetFedRatio4PriorMonPrint(bt);
                dgPrint.DataSource = null;
                dgPrint.DataSource = pt5;
                SetPrintColumnHearder();
                //PrintAfterFedRation((datet.AddMonths(-4)).ToString("yyyyMM"), 5);
                SaveFile(fed_tab5_file);
            }

            //MessageBox.Show("Ratio adjustment completed and listing have been printed to " + UserInfo.PrinterQ);
            MessageBox.Show("Ratio adjustment completed");
        }

      
        private void SaveFile(string filename)
        {
            string fname = filename;

            //check file exist or not
            if (System.IO.File.Exists(fname))
                System.IO.File.Delete(fname);

            // Choose whether to write header. Use EnableWithoutHeaderText instead to omit header.
            dgPrint.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            // Select all the cells
            dgPrint.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dgPrint.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            System.IO.File.WriteAllText(fname, Clipboard.GetText(TextDataFormat.CommaSeparatedValue));

            dgPrint.ClearSelection();
        }

        private void SetPrintColumnHearder()
        {
            dgPrint.Columns[0].HeaderText = "Type of Construction";
            dgPrint.Columns[0].Width = 200;
            dgPrint.Columns[1].HeaderText = "Unadjusted Vip";
            dgPrint.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[1].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[2].HeaderText = "VIP LSF";
            dgPrint.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[2].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[3].HeaderText = "VIP(1.01) Undercoverage";
            dgPrint.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[3].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[3].Width = 100;
            dgPrint.Columns[4].HeaderText = "Original VIP BST";
            dgPrint.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[4].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[5].HeaderText = "Revised VIP BST";
            dgPrint.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[5].DefaultCellStyle.Format = "N0";
            dgPrint.Columns[6].HeaderText = "LSF";
            dgPrint.Columns[7].HeaderText = "Original BST";
            dgPrint.Columns[8].HeaderText = "Revised BST";
        }

        private void PrintFedRatio()
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Ratio Adjustment Factors for Federal";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = "Survey Date = " + sdate;
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Ratio Adjust Factors Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgPrint);
            
        }

        //print After Ratio Adjust
        private void PrintAfterFedRation(string yd, int table_no)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Federal " + yd + " Not Seasonally Adjusted VIP" + " After Ratio Adjust";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = "Thousands of Dollars";
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "After Ratio Adjust Factors Print" + table_no;
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgPrint);
        }
        
    }
}
