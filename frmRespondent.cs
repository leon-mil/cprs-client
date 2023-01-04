/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmRespondent.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Search Respondent table
Change Request: 
Specification : Respondent Search Specifications  
Rev History   : See Below

Other         : N/A
 ***********************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using CprsBLL;
using CprsDAL;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmRespondent : Cprs.frmCprsParent
    {
        private Respondent resp;
        private RespondentSearchData dataObject;
        private string curr_survey_month;

        public frmRespondent()
        {
            InitializeComponent();
            dataObject = new RespondentSearchData();
        }

        private void frmRespondent_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            label55.Text = "CUI//SP-CENS" + "\n" + "DISCLOSURE PROHIBITED: TITLE 13 USC";

            PopulateRStateCombo();
            SetButtonTxt();
            btnCentpwd.Enabled = false;
            dgResp.DataSource = dataObject.GetRespondentEmptyTable();
            dgResp.AutoResizeColumns();
            dgProject.DataSource = dataObject.GetProjectEmptyTable();
            dgProject.AutoResizeColumns();
            dgProject.RowHeadersVisible = true; // set it to false if not needed
            dgResp.RowHeadersVisible = true; 

            //Add key down event to textboxes

            this.KeyPreview = true;

            txtRespID.KeyDown += new KeyEventHandler(txtRespID_KeyDown);

            txtOwner.KeyDown += new KeyEventHandler(txtOwner_KeyDown);

            txtContact.KeyDown += new KeyEventHandler(txtContact_KeyDown);

            txtPhone.KeyDown += new KeyEventHandler(txtPhone_KeyDown);

            dgResp.SelectionChanged += new EventHandler(dgResp_SelectionChanged);

            //set highlight to respid field
            this.ActiveControl = txtRespID;

            curr_survey_month = GeneralFunctions.CurrentYearMon();
        }

        private void txtRespID_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RESPID");
            
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "PHONE");
            if (txtPhone.Text.Length == 10)
            {
                cbRstate.Focus();
            }
        }

      
        private void dgResp_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRowCount = dgResp.Rows.GetRowCount(DataGridViewElementStates.Selected);
            if (selectedRowCount > 0)
            {
                string selected_respid = dgResp.SelectedRows[0].Cells[0].FormattedValue.ToString();

                // Update the project grid to reflect changes to the selection.
                getProjectData(selected_respid);

                btnCentpwd.Enabled = true;
            }
        }

        private void txtRespID_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 7))
                Search();
        }

        private void txtOwner_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void PopulateRStateCombo()
        {
            cbRstate.DataSource = dataObject.GetRstateData();
            cbRstate.ValueMember = "RSTATE";
            cbRstate.DisplayMember = "RSTNAME";
            cbRstate.SelectedIndex = -1;
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
                case EnumGroups.NPCLead:
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCInterviewer:  
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtRespID.Text = "";
            txtOwner.Text = "";

            cbRstate.SelectedIndex = -1;

            txtContact.Text = "";
            txtPhone.Text = "";

            chkExact.Checked = false;

            dgResp.DataSource = dataObject.GetRespondentEmptyTable();
            dgProject.DataSource = dataObject.GetProjectEmptyTable();

            lblProjectCount.Text = "Projects:";
            lblRespCount.Text = "Respondents:";

            txtRespID.Focus();

            btnCentpwd.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
            if (txtRespID.Text.Trim() == "" && txtOwner.Text.Trim() == "" && txtContact.Text.Trim() == "" && txtPhone.Text.Trim() == "" && cbRstate.Text.Trim() == "")
            {
                MessageBox.Show("Please Enter Search Criteria.");
                txtRespID.Focus();
                return;
            }
            if (txtRespID.Text.Trim() != "" && (txtOwner.Text.Trim() != "" || txtContact.Text.Trim() != "" || txtPhone.Text.Trim() != "" || cbRstate.Text.Trim() != ""))
            {
                MessageBox.Show("Other Criteria should not be included in Respid search.","Entry Error");
                txtRespID.Text = "";
                txtOwner.Text = "";
                txtContact.Text = "";
                txtPhone.Text = "";

                cbRstate.SelectedIndex = -1;

                txtRespID.Focus();

                return;
            }

            if (txtRespID.Text.Trim() != "")
            {
                string respid = txtRespID.Text.Trim();

                if (!(respid.Length == 7))
                {
                    MessageBox.Show("RESPID should be 7 digits.");
                    txtRespID.Text = "";
                    txtRespID.Focus();
                    return;
                }
                else if (!GeneralDataFuctions.ChkRespid(respid))
                {
                    MessageBox.Show("Invalid RESPID.");
                    txtRespID.Text = "";
                    txtRespID.Focus();
                    return;
                }
            }

            if (txtOwner.Text.Trim().Length > 0)
            {
                string[] words = txtOwner.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words for Organization to search.");
                    txtOwner.Focus();
                    txtOwner.SelectAll();
                    return;
                }
            }
            if (txtContact.Text.Length > 0)
            {
                string[] words = txtContact.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words for Contact to search.");
                    txtContact.Focus();
                    txtContact.SelectAll();
                    return;
                }
            }

            this.Cursor = Cursors.WaitCursor;
            GetRespondentData();
            this.Cursor = Cursors.Default;

        }

        private DataTable GetDataTable()
        {
            string owner1 = "";
            string owner2 = "";
            string owner3 = "";
            string contact1 = "";
            string contact2 = "";
            string contact3 = "";

            //splict owners and contacts
            if (txtOwner.Text.Trim().Length > 0)
            {
                string[] owners = txtOwner.Text.Trim().Split(' ');
                if (owners.Length > 0)
                {
                    if (owners.Length == 3)
                    {
                        owner1 = owners[0];
                        owner2 = owners[1];
                        owner3 = owners[2];
                    }
                    else if (owners.Length == 2)
                    {
                        owner1 = owners[0];
                        owner2 = owners[1];
                    }
                    else
                        owner1 = owners[0];
                }
            }

            if (txtContact.Text.Trim().Length > 0)
            {
                string[] contacts = txtContact.Text.Trim().Split(' ');
                if (contacts.Length > 0)
                {
                    if (contacts.Length == 3)
                    {
                        contact1 = contacts[0];
                        contact2 = contacts[1];
                        contact3 = contacts[2];
                    }
                    else if (contacts.Length == 2)
                    {
                        contact1 = contacts[0];
                        contact2 = contacts[1];
                    }
                    else
                        contact1 = contacts[0];
                }
            }

            string rst = "";
            if (cbRstate.Text != "")
                rst = cbRstate.SelectedValue.ToString();

            DataTable dt = dataObject.GetRespondentSearchData(txtRespID.Text, owner1, owner2, owner3, contact1, contact2, contact3, txtPhone.Text, rst, chkExact.Checked);
           
            return dt;
        }

        private void GetRespondentData()
        {
            dgResp.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgResp.RowHeadersVisible = false; // set it to false if not needed
            DataTable dt = GetDataTable();
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");

                dgResp.DataSource = dataObject.GetRespondentEmptyTable();
                dgProject.DataSource = dataObject.GetProjectEmptyTable();
                lblProjectCount.Text = "Projects:";
                lblRespCount.Text = "Respondents:";
                txtRespID.Focus();
                return;
            }

            dgResp.DataSource = dt;
            for (int i = 0; i < dgResp.ColumnCount; i++)
            {
                if (i==0)
                {
                    dgResp.Columns[i].Width = 30;
                }

                if (i == 1)
                    dgResp.Columns[i].HeaderText = "ORGANIZATION";
                if (i == 2)
                    dgResp.Columns[i].HeaderText = "CONTACT";
                if (i == 1 || i == 2)
                {
                    dgResp.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    dgResp.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
                    dgResp.Columns[i].Width = 170;
                }
                else if (i == 3 || i == 4)
                {
                    dgResp.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgResp.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgResp.Columns[i].Width = 40;
                }
                else if (i == 5)
                {
                    dgResp.Columns[i].HeaderText = "SURVEYS";
                    dgResp.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgResp.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgResp.Columns[i].Width = 70;
                }
                
            }
            dgResp.RowHeadersVisible = true; // set it to false if not needed

            if (dt.Rows.Count == 1)
                lblRespCount.Text = "Respondents: " + dt.Rows.Count.ToString() + " Case Found";
            else
                lblRespCount.Text = "Respondents: " + dt.Rows.Count.ToString() + " Cases Found";

        }

        private void getProjectData(string respid)
        {
            DataTable dt;
            if ((UserInfo.GroupCode == EnumGroups.NPCLead) || (UserInfo.GroupCode == EnumGroups.NPCManager) || (UserInfo.GroupCode == EnumGroups.NPCInterviewer))
                dt = dataObject.GetProjectDataForNPC(respid);
            else
               dt= dataObject.GetProjectData(respid);
            dgProject.DataSource = dt;

            dgProject.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgProject.RowHeadersVisible = false; // set it to false if not needed

            for (int i = 0; i < dgProject.ColumnCount; i++)
            {

                //Dodgenum(16) is hidden
                if (i == 16)
                {
                    dgProject.Columns[i].Visible = false;
                }

                //ID
                if (i == 0)
                {
                    dgProject.Columns[i].Frozen = true;
                    dgProject.Columns[i].Width = 60;
                }

                //FIN
                if (i == 1)
                {
                    dgProject.Columns[i].Frozen = true;
                    dgProject.Columns[i].Width = 130;
                }

                if (i == 0 || i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 7 || i == 9 || i == 10 || i == 11)
                { 
                     dgProject.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (i == 8 || i == 12 || i == 13 || i == 14 || i == 15)
                {
                    dgProject.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                //Projselv(8), Rvitm5c(12) ,Rbldgs(14) and Runits(15) are displayed with comma format
                if (i == 8 || i == 12 || i == 14 || i == 15)
                {
                    dgProject.Columns[i].DefaultCellStyle.Format = "N0";
                }

                if (i == 3 || i == 4)
                {
                    dgProject.Columns[i].Width = 70;
                }

                if (i == 14 || i == 15)
                {
                    dgProject.Columns[i].Width = 80;
                }
            }

            dgProject.RowHeadersVisible = true;

            if (dt.Rows.Count == 1)
                lblProjectCount.Text = "Projects: " + dt.Rows.Count.ToString() + " Case Found";
            else
                lblProjectCount.Text = "Projects: " + dt.Rows.Count.ToString() + " Cases Found";

        }

        private string BuildSearchCriteria()
        {
            string str= "Search Criteria:";

            if (txtRespID.Text != "")
                str += " RespID = " + txtRespID.Text;
            if (txtOwner.Text != "")
                str += " Owner = \"" + txtOwner.Text + "\"";
            if (txtContact.Text != "")
                str += " Contact = \"" + txtContact.Text + "\"";
            if (txtPhone.Text != "")
                str += " Phone = " + txtPhone.Text;
            if (cbRstate.Text != "")
                str += " Rstate = " + cbRstate.Text.Substring(0, 2);

            return str;

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            string print_selection = "";

            if (dgResp.RowCount==0)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to print.");
            }
            else
            {
                frmRespPrintSel popup = new frmRespPrintSel();

                DialogResult dialogresult = popup.ShowDialog();
                
                if (dialogresult == DialogResult.OK)
                {
                    print_selection = popup.PrintSelection;
                }

                popup.Dispose();


                if ((print_selection == "Respondent" && dgResp.RowCount > 115) || (print_selection == "Project" && dgProject.RowCount >= 115))
                {
                    if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PrintData(print_selection);
                    }
                }
                else
                {
                    PrintData(print_selection);

                }
            }
        }

        private void SetHeaderCellValueProject()
        {
            int rowNumber = 1;
            foreach (DataGridViewRow dr in dgProject.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgProject.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void SetHeaderCellValueResp()
        {
            int rowNumber = 1;
            foreach (DataGridViewRow dr in dgResp.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgResp.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void PrintData(string print_selection)
        {
            Cursor.Current = Cursors.WaitCursor;
            SetHeaderCellValueProject();
            SetHeaderCellValueResp();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "RESPONDENT SEARCH";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitle = BuildSearchCriteria();
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Respondent Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            if (print_selection == "Respondent")
            {
                printer.HideColumns.Add("rstate");
                printer.HideColumns.Add("phone");

                if (dgResp.SortedColumn == null)
                    printer.SubTitle = BuildSearchCriteria();
                else
                    printer.SubTitle = BuildSearchCriteria() + "  Sorted By: " + dgResp.SortedColumn.Name;

                printer.PrintDataGridViewWithoutDialog(dgResp);
            }
            else
            {
                /*Hide the columns */
                printer.HideColumns.Add("dodgenum");
                printer.HideColumns.Add("status");
                printer.HideColumns.Add("projselv");
                printer.HideColumns.Add("fwgt");
                printer.HideColumns.Add("strtdate");
                printer.HideColumns.Add("compdate");
                printer.HideColumns.Add("futcompd");
                printer.HideColumns.Add("rbldgs");
                printer.HideColumns.Add("runits");

                if (dgProject.SortedColumn == null)
                    printer.SubTitle = BuildSearchCriteria();
                else
                    printer.SubTitle = BuildSearchCriteria() + "  Sorted By: " + dgProject.SortedColumn.Name;

                printer.PrintDataGridViewWithoutDialog(dgProject);
            }

            Cursor.Current = Cursors.Default;

        }

        
        private void btnName_Click(object sender, EventArgs e)
        {
            if (dgProject.RowCount == 0)
            {
                MessageBox.Show("The Projects Results list is empty. No Record Selected.");
                return;
            }
            int index = dgProject.CurrentRow.Index;
            string seldate = dgProject["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen.");
                return;
            }
            
            this.Hide(); // hide parent

            DataGridViewSelectedRowCollection rows = dgProject.SelectedRows;

            string val1 = dgProject["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();

            int xcnt = 0;
            frmName fName = new frmName();

            foreach (DataGridViewRow dr in dgProject.Rows)
            {
                string val = dr.Cells["ID"].Value.ToString();
                if (val.Length != 0)
                {
                    if (val.Length != 0)
                    {
                        seldate = dr.Cells["SELDATE"].Value.ToString();
                        if (seldate == curr_survey_month)
                        {
                            string owner = dr.Cells["OWNER"].Value.ToString();
                            if (owner == "M")
                            {
                                Idlist.Add(val);
                                if (val == val1)
                                { fName.CurrIndex = xcnt; }
                                xcnt = xcnt + 1;
                            }
                        }
                        else
                        {
                            Idlist.Add(val);
                            if (val == val1)
                            { fName.CurrIndex = xcnt; }
                            xcnt = xcnt + 1;
                        }
                    }
                }
            }

            fName.Id = val1;
               
            fName.Idlist = Idlist;
            fName.CallingForm = this;

         //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

            fName.ShowDialog(); // show child    

         //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");

            //refresh data in case respid was changed
            if (dgResp.SelectedRows.Count > 0)
            {
                string selected_respid = dgResp.SelectedRows[0].Cells[0].FormattedValue.ToString();

                // Update the project grid to reflect changes to the selection.
                getProjectData(selected_respid);
            }

        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (dgProject.RowCount == 0)
            {
                MessageBox.Show("The Projects Results list is empty. No Record Selected.");
            }
            else
            {
                this.Hide();        // hide parent

                DataGridViewSelectedRowCollection rows = dgProject.SelectedRows;

                int index = dgProject.CurrentRow.Index;

                string mid = dgProject["MASTERID", index].Value.ToString();

                frmSource fm = new frmSource();

                // Store Masterid in list for Page Up and Page Down

                List<int> Masteridlist = new List<int>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgProject.Rows)
                {
                    string val1 = dgProject["MASTERID", cnt].Value.ToString();
                    int val = Int32.Parse(val1);
                    Masteridlist.Add(val);
                    cnt = cnt + 1;
                }

                fm.Masterid = Int32.Parse(mid);
                fm.Masteridlist = Masteridlist;
                fm.CurrIndex = index;
                fm.CallingForm = this;

             //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fm.ShowDialog();  // show child

             //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (dgProject.RowCount == 0)
            {
                MessageBox.Show("The Projects Results list is empty. No Record Selected.");
                return;
            }

            //check initial cases
            int index = dgProject.CurrentRow.Index;
            string seldate = dgProject["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen.");
                return;
            }

            this.Hide();         // hide parent

            DataGridViewSelectedRowCollection rows = dgProject.SelectedRows;

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                string resp = dgProject["RESPID", index].Value.ToString();
                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

                //GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {
                string val1 = dgProject["ID", index].Value.ToString();

                // Store Id in list for Page Up and Page Down

                List<string> Idlist = new List<string>();

                int xcnt = 0;
                frmC700 fC700 = new frmC700();
                foreach (DataGridViewRow dr in dgProject.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    if (val.Length != 0)
                    {
                        if (val.Length != 0)
                        {
                            seldate = dr.Cells["SELDATE"].Value.ToString();
                            if (seldate == curr_survey_month)
                            {
                                string owner = dr.Cells["OWNER"].Value.ToString();
                                if (owner == "M")
                                {
                                    Idlist.Add(val);
                                    if (val == val1)
                                    { fC700.CurrIndex = xcnt; }
                                    xcnt = xcnt + 1;
                                }
                            }
                            else
                            {
                                Idlist.Add(val);
                                if (val == val1)
                                { fC700.CurrIndex = xcnt; }
                                xcnt = xcnt + 1;
                            }
                        }
                    }
                }

                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CallingForm = this;

                fC700.ShowDialog();  // show child

            }

            //refresh data in case respid was changed
            if (dgResp.SelectedRows.Count > 0)
            {
                string selected_respid = dgResp.SelectedRows[0].Cells[0].FormattedValue.ToString();

                // Update the project grid to reflect changes to the selection.
                getProjectData(selected_respid);
            }
        }

        private void txtRespID_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtRespID_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtOwner_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtOwner_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtContact_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtContact_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void dgResp_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgResp.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void dgProject_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgProject.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmRespondent_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void txtRespID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPhone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnCentpwd_Click(object sender, EventArgs e)
        {
            string selected_respid = dgResp.SelectedRows[0].Cells[0].FormattedValue.ToString();

            RespondentData respdata = new RespondentData();
            resp = respdata.GetRespondentData(selected_respid);

            frmRespCentpwdPopup popup = new frmRespCentpwdPopup(resp.Centpwd);
            DialogResult dialogresult = popup.ShowDialog();

            popup.Dispose();
        }

        private void btnCentpwd_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true; 
        }
    }
}
