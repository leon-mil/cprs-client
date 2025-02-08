/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmAdHoc.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Search Adhoc
Change Request: 
Specification : Adhoc Search Specifications  
Rev History   : See Below

Other         : N/A
 ***********************************************************************
Modified Date :  3 / 23 / 2023
Modified By   :  Christine
Keyword       :  
Change Request: CR#945
Description   : change print to export button
************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using CprsBLL;
using CprsDAL;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmAdHoc : Cprs.frmCprsParent
    {
        
        private AdhocSearchData dataObject;
        private string curr_survey_month;

        public frmAdHoc()
        {
     
            InitializeComponent();

            dataObject = new AdhocSearchData();
        }

        private void frmAdHoc_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            SetHiddenLbl();
            SetButtonTxt();

            SetOperatorCombo();

            PopulateFipStateCombo();

            dgAdhoc.DataSource = dataObject.GetEmptyTable();
            dgAdhoc.AutoResizeColumns();
            dgAdhoc.RowHeadersVisible = true;

            lblRecordCount.Text = " ";

            //Add key down event to textbox

            this.KeyPreview = true;

            txtProjDesc.KeyDown += new KeyEventHandler(txtProjDesc_KeyDown);
            txtProjLoc.KeyDown += new KeyEventHandler(txtProjLoc_KeyDown);
            txtPcityst.KeyDown += new KeyEventHandler(txtPcityst_KeyDown);
            txtFactor.KeyDown += new KeyEventHandler(txtFactor_KeyDown);
           
            txtNewtc.KeyDown += new KeyEventHandler(txtNewtc_KeyDown);
            txtNewtc1.KeyDown += new KeyEventHandler(txtNewtc1_KeyDown);
            txtProjDesc.Focus();

            curr_survey_month = GeneralFunctions.CurrentYearMon();
        }

        private void SetHiddenLbl()
        {
            lblNewtcto.Visible = false;
            txtNewtc1.Visible = false;
            btnNewtc1.Visible = false;
        }

        private void ResetParameters()
        {

            txtProjDesc.Text = "";
            txtProjLoc.Text = "";
            txtPcityst.Text = "";

            cbFipStateSel.SelectedIndex = -1;
            cbCountySel.SelectedIndex = -1;

            cbFactor.SelectedIndex = -1;
            txtFactor.Text = "";
     
            txtOwner.Text = "";

            txtNewtc.Text = "";
            txtNewtc1.Text = "";

            chkSample.Checked = false;

            dgAdhoc.DataSource = dataObject.GetEmptyTable();

            lblRecordCount.Text = " ";
        }

        private void cbNewtc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNewtc.SelectedItem != null && cbNewtc.SelectedItem.ToString() == "Between")
            {
                lblNewtcto.Visible = true;
                txtNewtc1.Visible = true;
                btnNewtc1.Visible = true;
            }
            else
            {
                lblNewtcto.Visible = false;
                txtNewtc1.Visible = false;
                btnNewtc1.Visible = false;
            }
        }

        private void cbFipStateSel_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateCountyCombo();
        }

        private void SetOperatorCombo()
        {
            cbFipstate.SelectedIndex = 0;
            cbCounty.SelectedIndex = 0;
            cbNewtc.SelectedIndex = 0;
            cbOwner.SelectedIndex = 0;
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

        private void PopulateFipStateCombo()
        {
            cbFipStateSel.DataSource = GeneralDataFuctions.GetFipStateDataForCombo();
            cbFipStateSel.ValueMember = "FIPSTATE";
            cbFipStateSel.DisplayMember = "STATE1";

            cbFipStateSel.SelectedIndex = -1;
        }

        private void PopulateCountyCombo()
        {
            DataTable dt = new DataTable();

            if (cbFipStateSel.SelectedIndex == 0 || cbFipStateSel.SelectedIndex == -1)
                cbCountySel.DataSource = dt;
            else
            {
                cbCountySel.DataSource = dataObject.GetCountyDataForCombo(cbFipStateSel.SelectedValue.ToString());
                cbCountySel.ValueMember = "FIPSCOU";
                cbCountySel.DisplayMember = "COUNTY1";
                cbCountySel.SelectedIndex = -1;   
            }
        }

        private void btnNewtc_Click(object sender, EventArgs e)
        {
            frmNewtcSel popup = new frmNewtcSel();
            //Point location = new Point(460, 170);
            //popup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            //popup.Location = location;

            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                txtNewtc.Text = popup.SelectedNewtc;
            }

            popup.Dispose();

        }

        private void btnNewtc1_Click(object sender, EventArgs e)
        {
            frmNewtcSel popup = new frmNewtcSel();
            //Point location = new Point(460, 170);
            //popup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            //popup.Location = location;

            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                txtNewtc1.Text = popup.SelectedNewtc;
            }

            popup.Dispose();
        }

        private void btnOwner_Click(object sender, EventArgs e)
        {
            //Display frmSurveySel as a modal dialog form

            frmSurveySel popup = new frmSurveySel();

            //Point location = new Point(460, 170);
            //popup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            //popup.Location = location;

            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.OK)
            {
                txtOwner.Text = popup.selectedSurvey;
            }

            popup.Dispose();

        }

        private void txtProjDesc_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }


        private void txtProjLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtPcityst_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtFactor_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtStructcd_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 3))
                Search();
        }

        private void txtStructcd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 3))
                Search();
        }

        private void txtNewtc_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 4))
            {
                if (e.KeyCode == Keys.Enter)
                    Search();
            }
        }

        private void txtNewtc1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 4))
                Search();
        }

        private void txtNewtc_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "NEWTC");
        }

        private void txtNewtc1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "NEWTC1");
        }

        private void txtStructcd_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "STRUCTCD");
        }

        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to export.");
            }
            else
            {
                if (dgAdhoc.RowCount > 500)
                {
                    if (MessageBox.Show("The export will contain more than 500 cases. Continue to Export?", "Export Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        ExportData();
                    }
                }
                else
                {
                    ExportData();

                }
            }
        }

        private void ExportData()
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text file|*.dat";
            saveFileDialog1.Title = "Save an File";
            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            dgAdhoc.MultiSelect = true;
            dgAdhoc.RowHeadersVisible = false;

            // Choose whether to write header. Use EnableWithoutHeaderText instead to omit header.
            dgAdhoc.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            // Select all the cells
            dgAdhoc.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dgAdhoc.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            System.IO.File.WriteAllText(saveFileDialog1.FileName, Clipboard.GetText(TextDataFormat.CommaSeparatedValue));

            dgAdhoc.ClearSelection();
            dgAdhoc.RowHeadersVisible = true;
            dgAdhoc.Rows[0].Selected = true;
            dgAdhoc.MultiSelect = false;

            MessageBox.Show("File has been created.");

        }

        private void SetHeaderCellValue()
        {
            int rowNumber = 1;
            foreach (DataGridViewRow dr in dgAdhoc.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgAdhoc.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }


        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            SetHeaderCellValue();
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "ADHOC SEARCH";

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            if (dgAdhoc.SortedColumn == null)
                printer.SubTitle = BuildSearchCriteria();
            else
                printer.SubTitle = BuildSearchCriteria() + "  Sorted By: " + dgAdhoc.SortedColumn.Name;
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
           
            /*Hide the columns */
            printer.HideColumns.Add("Dodgecou");
            printer.HideColumns.Add("PROJDESC");
            printer.HideColumns.Add("PROJLOC");
            printer.HideColumns.Add("PCITYST");
            printer.HideColumns.Add("STRUCTCD");
            printer.HideColumns.Add("F3RESPORG");
            printer.HideColumns.Add("F4RESPORG");
            printer.HideColumns.Add("F5RESPORG");
            printer.HideColumns.Add("F7RESPORG");
            printer.HideColumns.Add("ISSAMPLE");

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Adhoc Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //printer.PrintPreviewDataGridView(dgSample);
            printer.PrintDataGridViewWithoutDialog(dgAdhoc);

            Cursor.Current = Cursors.Default;

        }

        private string GetFactor()
        {
            string factor = "All";
            if (cbFactor.SelectedIndex == 1)
                factor = "Owner";
            else if (cbFactor.SelectedIndex == 2)
                factor = "Architect";
            else if (cbFactor.SelectedIndex == 3)
                factor = "Engineer";
            else if (cbFactor.SelectedIndex == 4)
                factor = "Contractor";

            return factor;
        }
        private string BuildSearchCriteria()
        {
            string criteria = "Search Criteria: ";

            if (txtProjDesc.Text.Trim() != "")
                criteria += " Project Desc = '" + txtProjDesc.Text + "'";
            if (txtProjLoc.Text.Trim() != "")
                criteria += " Project Loc = '" + txtProjLoc.Text + "'";
            if (txtPcityst.Text != "")
                criteria += " City State = '" + txtPcityst.Text + "'";
            if (txtFactor.Text.Trim() != "")
            {
                string factor = GetFactor();
                criteria += " Factor in '" + factor + txtFactor.Text + "'";
            }
                
            if (cbFipStateSel.Text.Trim() != "")
                criteria += " Fipstate = " + cbFipStateSel.Text.Substring(0, 2);
            if (txtOwner.Text.Trim() != "")
                criteria += " Owner = '" + txtOwner.Text + "'";
            if (cbCountySel.Text.Trim() != "")
                criteria += " County = '" + cbCountySel.Text + "'";
           
            if (txtNewtc.Text != "")
            {
                if ((cbNewtc.Text == "Between") && (txtNewtc1.Text != ""))
                    criteria += " Newtc " + GeneralFunctions.ConvertOperatorToSymbol(cbNewtc.Text) + " (" + txtNewtc.Text + " - " + txtNewtc1.Text + ")";
                else
                    criteria += " Newtc " + GeneralFunctions.ConvertOperatorToSymbol(cbNewtc.Text) + " " + txtNewtc.Text;
            }

            if (chkSample.Checked)
                criteria += " Is Sample ";

                      
            return criteria;

        }


        private void btnName_Click(object sender, EventArgs e)
        {

            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. No Record Selected.");
                return;
            }
            
            DataGridViewSelectedRowCollection rows = dgAdhoc.SelectedRows;

            int index = dgAdhoc.CurrentRow.Index;

            string val1 = dgAdhoc["ID", index].Value.ToString();
                               

            if (val1 == "")
            {
                MessageBox.Show("The Selected case is not a Sample Case.");
                return;
            }

            //check initial cases
            string seldate = dgAdhoc["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgAdhoc["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }
               
            this.Hide(); // hide parent

            frmName fName = new frmName();

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();

            int xcnt = 0;

            foreach (DataGridViewRow dr in dgAdhoc.Rows)
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

           // GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

            fName.ShowDialog(); // show child

         //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
              
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. No Record Selected.");
            }
            else
            {
                this.Hide();        // hide parent

                DataGridViewSelectedRowCollection rows = dgAdhoc.SelectedRows;

                int index = dgAdhoc.CurrentRow.Index;

                string mid = dgAdhoc["MASTERID", index].Value.ToString();

                frmSource fm = new frmSource();

                // Store Masterid in list for Page Up and Page Down

                List<int> Masteridlist = new List<int>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgAdhoc.Rows)
                {
                    string val1 = dgAdhoc["MASTERID", cnt].Value.ToString();
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

            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. No Record Selected.");
                return;
            }
           
            DataGridViewSelectedRowCollection rows = dgAdhoc.SelectedRows;

            int index = dgAdhoc.CurrentRow.Index;

            string val1 = dgAdhoc["ID", index].Value.ToString();

            if (val1 == "")
            {
                MessageBox.Show("The Selected case is not a Sample Case.");
                return;
            }

            //check initial cases
            string seldate = dgAdhoc["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgAdhoc["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }

            this.Hide();        // hide parent

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                SampleData sdata = new SampleData();
                Sample ss = sdata.GetSampleData(val1);
                frmTfu tfu = new frmTfu();

                tfu.RespId = ss.Respid;
                tfu.CallingForm = this;

              //  GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {
                frmC700 fC700 = new frmC700();

                // Store Id in list for Page Up and Page Down

                List<string> Idlist = new List<string>();

                int xcnt = 0;

                foreach (DataGridViewRow dr in dgAdhoc.Rows)
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

             //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fC700.ShowDialog();  // show child
            }
         //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");

        }

        private Boolean VerifyBetween()
        {
            Boolean result = false;

            if (cbNewtc.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtNewtc.Text, txtNewtc1.Text, "Newtc"))
                    return result;

            return result = true;
        }

     
        private bool VerifyParmaters()
        {

            //Verify any paramter entered
            if (txtProjDesc.Text.Trim() == "" && txtProjLoc.Text == "" && cbFipStateSel.Text.Trim() == "" && txtOwner.Text.Trim() == "" && txtPcityst.Text.Trim() == ""
                  && txtFactor.Text.Trim() == "" && txtNewtc.Text.Trim() == "" && txtNewtc1.Text.Trim() == "" 
                  && !chkSample.Checked)
            {
                MessageBox.Show("Please Enter Search Criteria.");
                txtProjDesc.Focus();
                return false;
            }

            if (txtProjDesc.Text.Trim().Length > 0)
            {
                string[] words = txtProjDesc.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Project Description to search.");
                    txtProjDesc.Focus();
                    txtProjDesc.SelectAll();
                    return false;
                }
            }

            if (txtProjLoc.Text.Trim().Length > 0)
            {
                string[] words = txtProjLoc.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Project Location to search.");
                    txtProjLoc.Focus();
                    txtProjLoc.SelectAll();
                    return false;
                }
            }

            if (txtPcityst.Text.Trim().Length > 0)
            {
                string[] words = txtPcityst.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Project City Strate to search.");
                    txtPcityst.Focus();
                    txtPcityst.SelectAll();
                    return false;
                }
            }

            if (txtFactor.Text.Trim().Length > 0)
            {
                string[] words = txtFactor.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Factor to search.");
                    txtFactor.Focus();
                    txtFactor.SelectAll();
                    return false;
                }

                //Check Factor Type 
                if (cbFactor.Text.Trim() == "")
                {
                    MessageBox.Show("Please select type of Factor.");
                    return false;
                }
            }

            if (txtNewtc.Text.Trim() != "")
            {
                if (cbNewtc.Text == "Equals" || cbNewtc.Text == "GreaterThanOrEqual" || cbNewtc.Text == "Between")
                {
                    if (!(txtNewtc.Text.Length == 4) || ((cbNewtc.Text == "Between") && !(txtNewtc1.Text.Length == 4)))
                    {
                        MessageBox.Show("NEWTC should be 4 digits.");
                        txtNewtc.Text = "";
                        txtNewtc.Focus();
                        if (cbNewtc.Text == "Between") txtNewtc1.Text = "";
                        return false;
                    }
                    else if (!GeneralDataFuctions.CheckNewTC(txtNewtc.Text) || ((cbNewtc.Text == "Between") && !GeneralDataFuctions.CheckNewTC(txtNewtc1.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtNewtc.Text = "";
                        txtNewtc.Focus();
                        if (cbNewtc.Text == "Between") txtNewtc1.Text = "";
                        return false;
                    }
                }
            }

            //Verify Between fields
            if (!VerifyBetween())
                return false;

            return true;

        }

        private void Search()
        {
            if (VerifyParmaters())
            {
                this.Cursor = Cursors.WaitCursor;
                GetAdhocData();
                this.Cursor = Cursors.Default;
            }
        }

        private void GetAdhocData()
        {
            dgAdhoc.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgAdhoc.RowHeadersVisible = false; // set it to false if not needed

            DataTable dt = GetDataTable();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");
                txtProjDesc.Focus();
            }

            dgAdhoc.DataSource = dt;
            for (int i = 0; i < dgAdhoc.ColumnCount; i++)
            {
                //Set Lnewtc(6), Fipstate(11) and IsSample(17) as invisible
                if (i==1 || i == 6 || i == 11 || i == 17)
                    dgAdhoc.Columns[i].Visible = false;

                //Set columns Header Text


                if (i == 8)
                {
                    dgAdhoc.Columns[i].HeaderText = "DESCRIPTION";
                }
                if (i == 9)
                {
                    dgAdhoc.Columns[i].HeaderText = "LOCATION";
                }
                if (i == 10)
                {
                    dgAdhoc.Columns[i].HeaderText = "CITY";
                }
                if (i == 12)
                {
                    dgAdhoc.Columns[i].HeaderText = "COUNTY";
                }
                if (i == 13)
                {
                    dgAdhoc.Columns[i].HeaderText = "FOWNER";
                }
                if (i == 14)
                {
                    dgAdhoc.Columns[i].HeaderText = "FARCHITECT";
                }
                if (i == 15)
                {
                    dgAdhoc.Columns[i].HeaderText = "FENGINEER";
                }
                if (i == 16)
                {
                    dgAdhoc.Columns[i].HeaderText = "FCONTRACTOR";
                }

                //Set column width
                if (i == 0)
                {
                    dgAdhoc.Columns[i].Width = 110;
                    dgAdhoc.Columns[i].Frozen = true;
                }
                else if (i == 2)
                {
                    dgAdhoc.Columns[i].Width = 85;
                    dgAdhoc.Columns[i].Frozen = true;
                }
                else if (i == 4 || i == 3)
                    dgAdhoc.Columns[i].Width = 85;
                else if (i == 5)
                    dgAdhoc.Columns[i].Width = 80;
                
                else if (i == 7 || i == 12)
                    dgAdhoc.Columns[i].Width = 90;
                else
                    dgAdhoc.Columns[i].Width = 260;

                //ID(2),Status(3), Owner(4), Newtc(5),Seldate(7),County(12) are Centered
                if (i == 2 || i == 3 || i == 4 || i == 5 || i == 7 || i == 12)
                {
                    dgAdhoc.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

            }

            dgAdhoc.RowHeadersVisible = true;

            if (dt.Rows.Count == 1)
                lblRecordCount.Text = dt.Rows.Count.ToString() + " records found.";
            else
                lblRecordCount.Text = dt.Rows.Count.ToString() + " records found.";

        }

        private DataTable GetDataTable()
        {
            string projdesc1 = "";
            string projdesc2 = "";
            string projdesc3 = "";
            string projloc1 = "";
            string projloc2 = "";
            string projloc3 = "";
            string pcityst1 = "";
            string pcityst2 = "";
            string pcityst3 = "";
            string factor1 = "";
            string factor2 = "";
            string factor3 = "";

            //splict projdescs and projlocs
            if (txtProjDesc.Text.Trim().Length > 0)
            {
                string[] projdescs = txtProjDesc.Text.Trim().Split(' ');
                if (projdescs.Length > 0)
                {
                    if (projdescs.Length == 3)
                    {
                        projdesc1 = projdescs[0];
                        projdesc2 = projdescs[1];
                        projdesc3 = projdescs[2];
                    }
                    else if (projdescs.Length == 2)
                    {
                        projdesc1 = projdescs[0];
                        projdesc2 = projdescs[1];
                    }
                    else
                        projdesc1 = projdescs[0];
                } 
            }

            if (txtProjLoc.Text.Trim().Length > 0)
            {
                string[] projlocs = txtProjLoc.Text.Trim().Split(' ');
                if (projlocs.Length > 0)
                {
                    if (projlocs.Length == 3)
                    {
                        projloc1 = projlocs[0];
                        projloc2 = projlocs[1];
                        projloc3 = projlocs[2];
                    }
                    else if (projlocs.Length == 2)
                    {
                        projloc1 = projlocs[0];
                        projloc2 = projlocs[1];
                    }
                    else
                        projloc1 = projlocs[0];
                }
            }

            if (txtPcityst.Text.Trim().Length > 0)
            {
                string[] pcitysts = txtPcityst.Text.Trim().Split(' ');
                if (pcitysts.Length > 0)
                {
                    if (pcitysts.Length == 3)
                    {
                        pcityst1 = pcitysts[0];
                        pcityst2 = pcitysts[1];
                        pcityst3 = pcitysts[2];
                    }
                    else if (pcitysts.Length == 2)
                    {
                        pcityst1 = pcitysts[0];
                        pcityst2 = pcitysts[1];
                    }
                    else
                        pcityst1 = pcitysts[0];
                }
            }

            if (txtFactor.Text.Trim().Length > 0)
            {
                string[] factors = txtFactor.Text.Trim().Split(' ');
                if (factors.Length > 0)
                {
                    if (factors.Length == 3)
                    {
                        factor1 = factors[0];
                        factor2 = factors[1];
                        factor3 = factors[2];
                    }
                    else if (factors.Length == 2)
                    {
                        factor1 = factors[0];
                        factor2 = factors[1];
                    }
                    else
                        factor1 = factors[0];
                }
            }

            string fst = string.Empty;
            string county = string.Empty;
            int isSample = 1;

            if (cbFipStateSel.Text.Trim() != "")
              fst = cbFipStateSel.SelectedValue.ToString();
            if (cbCountySel.Text.Trim() != "")
              county = cbCountySel.SelectedValue.ToString();
            if (chkSample.Checked)
                isSample = 0;

            string factor = GetFactor();
            DataTable dt = dataObject.GetAdhocData(projdesc1, projdesc2, projdesc3, projloc1, projloc2, projloc3, pcityst1, pcityst2, pcityst3, fst, county,
                            factor, factor1, factor2, factor3, txtOwner.Text,txtNewtc.Text, cbNewtc.Text,
                            txtNewtc1.Text, isSample);
            return dt;

        }


        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            SetHiddenLbl();

            ResetParameters();

            SetOperatorCombo();

            dgAdhoc.DataSource = dataObject.GetEmptyTable();

            lblRecordCount.Text = " ";
            txtProjDesc.Focus();
        }

        private void txtProjDesc_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtProjDesc_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtProjLoc_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtProjLoc_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtPcityst_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtPcityst_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtFactor_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtFactor_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtNewtc_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtNewtc_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtNewtc1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtNewtc1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtStructcd_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtStructcd_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtStructcd1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtStructcd1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgAdhoc_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgAdhoc.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmAdHoc_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNewtc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFipstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbOwner_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbCounty_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
