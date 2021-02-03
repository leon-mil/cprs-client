/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmC700Srch.cs
Programmer    : Christine
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Search C700 table
Change Request: 
Specification : C700 Search Specifications  
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
    public partial class frmC700Srch : Cprs.frmCprsParent
    {
        
        private C700SearchData dataObject;
        private string curr_survey_month;

        public frmC700Srch()
        {
            InitializeComponent();
        }

        private void frmC700Srch_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            SetHiddenLbl();
            SetButtonTxt();

            ResetParameters();

            SetOperatorCombo();

            PopulateFipStateCombo();

            dataObject = new C700SearchData();
            dgC700Srch.DataSource = dataObject.GetEmptyTable();

            dgC700Srch.AutoResizeColumns();
            dgC700Srch.RowHeadersVisible = true;

            lblRecordCount.Text = " ";

            //Add key down event to All textbox

            this.KeyPreview = true;

            txtId.KeyDown += new KeyEventHandler(txtId_KeyDown);

            txtRespid.KeyDown += new KeyEventHandler(txtRespid_KeyDown);

            txtSeldate.KeyDown += new KeyEventHandler(txtSeldate_KeyDown);
            txtSeldate1.KeyDown += new KeyEventHandler(txtSeldate1_KeyDown);

            txtNewtc.KeyDown += new KeyEventHandler(txtNewtc_KeyDown);
            txtNewtc1.KeyDown += new KeyEventHandler(txtNewtc1_KeyDown);

            txtRvitm5c.KeyDown += new KeyEventHandler(txtRvitm5c_KeyDown);
            txtRvitm5c1.KeyDown += new KeyEventHandler(txtRvitm5c1_KeyDown);

            txtItem6.KeyDown += new KeyEventHandler(txtItem6_KeyDown);
            txtItem61.KeyDown += new KeyEventHandler(txtItem61_KeyDown);

            txtCapexp.KeyDown += new KeyEventHandler(txtCapexp_KeyDown);
            txtCapexp1.KeyDown += new KeyEventHandler(txtCapexp1_KeyDown);

            txtRunits.KeyDown += new KeyEventHandler(txtRunits_KeyDown);
            txtRunits1.KeyDown += new KeyEventHandler(txtRunits1_KeyDown);

            txtCostpu.KeyDown += new KeyEventHandler(txtCostpu_KeyDown);
            txtCostpu1.KeyDown += new KeyEventHandler(txtCostpu1_KeyDown);

            txtStrtdate.KeyDown += new KeyEventHandler(txtStrtdate_KeyDown);
            txtStrtdate1.KeyDown += new KeyEventHandler(txtStrtdate1_KeyDown);

            txtCompdate.KeyDown += new KeyEventHandler(txtCompdate_KeyDown);
            txtCompdate1.KeyDown += new KeyEventHandler(txtCompdate1_KeyDown);

            txtFutcompd.KeyDown += new KeyEventHandler(txtFutcompd_KeyDown);
            txtFutcompd1.KeyDown += new KeyEventHandler(txtFutcompd1_KeyDown);

            txtId.Focus();

            curr_survey_month = GeneralFunctions.CurrentYearMon();
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 7))
                Search();
        
        }

        private void txtRespid_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 7))
                Search();
        }

        private void txtSeldate_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtSeldate.Text))
                    Search();
                else
                {
                    MessageBox.Show("Seldate is not valid.");
                    txtSeldate.Text = "";
                }

            }
        }
        private void txtSeldate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtSeldate1.Text))
                    Search();
                else
                {
                    MessageBox.Show("Seldate is not valid.");
                    txtSeldate1.Text = "";
                }

            }
        }

        private void txtNewtc_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 4))
                Search();
        }
        private void txtNewtc1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 4))
                Search();
        }


        private void txtRvitm5c_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtRvitm5c1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtItem6_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtItem61_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtCapexp_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtCapexp1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }


        private void txtRunits_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtRunits1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtCostpu_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtCostpu1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtStrtdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtStrtdate.Text))
                    Search();
                else
                {
                    MessageBox.Show("Strtdate is not valid.");
                    txtStrtdate.Text = "";
                }

            }
        }
        private void txtStrtdate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtStrtdate1.Text))
                    Search();
                else
                {
                    MessageBox.Show("Strtdate is not valid.");
                    txtStrtdate1.Text = "";
                }

            }
        }

        private void txtCompdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtCompdate.Text))
                    Search();
                else
                {
                    MessageBox.Show("Compdate is not valid.");
                    txtCompdate.Text = "";
                }

            }
        }
        private void txtCompdate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtCompdate1.Text))
                    Search();
                else
                {
                    MessageBox.Show("Compdate is not valid.");
                    txtCompdate1.Text = "";
                }

            }
        }

        private void txtFutcompd_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtFutcompd.Text))
                    Search();
                else
                {
                    MessageBox.Show("Futcompd is not valid.");
                    txtFutcompd.Text = "";
                }

            }
        }
        private void txtFutcompd1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
            {
                if (GeneralFunctions.ValidateDateWithRange(txtFutcompd1.Text))
                    Search();
                else
                {
                    MessageBox.Show("Futcompd is not valid.");
                    txtFutcompd1.Text = "";
                }

            }
        }


        private void SetOperatorCombo()
        {
            cbId.SelectedIndex = 0;
            cbRespid.SelectedIndex = 0;
            cbStatus.SelectedIndex = 0;
            cbSource.SelectedIndex = 0;
            cbFipstate.SelectedIndex = 0;

            cbActive.SelectedIndex = 0;
            cbOwner.SelectedIndex = 0;
            cbSeldate.SelectedIndex = 0;
            cbNewtc.SelectedIndex = 0;
            cbRvitm5c.SelectedIndex = 0;
            cbItem6.SelectedIndex = 0;


            cbCapexp.SelectedIndex = 0;
            cbRunits.SelectedIndex = 0;
            cbCostpu.SelectedIndex = 0;
            cbStrtdate.SelectedIndex = 0;
            cbCompdate.SelectedIndex = 0;
            cbFutcompd.SelectedIndex = 0;
        }

        private void PopulateFipStateCombo()
        {
            cbFipStateSel.DataSource = GeneralDataFuctions.GetFipStateDataForCombo();
            cbFipStateSel.ValueMember = "FIPSTATE";
            cbFipStateSel.DisplayMember = "STATE1";

            cbFipStateSel.SelectedIndex = -1;
        }


        private void SetHiddenLbl()
        {
            lbSeldateto.Visible = false;
            txtSeldate1.Visible = false;

            lbNewtcto.Visible = false;
            txtNewtc1.Visible = false;
            btnNewtc1.Visible = false;

            lbRvitm5cto.Visible = false;
            txtRvitm5c1.Visible = false;

            lbItem6to.Visible = false;
            txtItem61.Visible = false;

            lbCapexpto.Visible = false;
            txtCapexp1.Visible = false;

            lbRunitsto.Visible = false;
            txtRunits1.Visible = false;

            lbCostputo.Visible = false;
            txtCostpu1.Visible = false;

            lbStrtdateto.Visible = false;
            txtStrtdate1.Visible = false;

            lbCompdateto.Visible = false;
            txtCompdate1.Visible = false;

            lbFutcompdto.Visible = false;
            txtFutcompd1.Visible = false;
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

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "ID");
        }

        private void txtRespid_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RESPID");
            if (txtRespid.Text.Length == 7)
                btnOwner.Focus();
        }

        private void txtNewtc_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "NEWTC");
            if (txtNewtc.Text.Length == 4)
            {
                if (txtNewtc1.Visible)
                    txtNewtc1.Focus();
                else
                    txtStrtdate.Focus();
            }
        }
        private void txtNewtc1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "NEWTC1");
            if (txtNewtc1.Text.Length == 4)
                txtStrtdate.Focus();
        }

        private void txtSeldate_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE");
            if (txtSeldate.Text.Length == 6)
            {
                if (txtSeldate1.Visible)
                    txtSeldate1.Focus();
                else
                    txtCostpu.Focus();
            }
        }
        private void txtSeldate1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE1");
            if (txtSeldate1.Text.Length == 6)
                txtCostpu.Focus();
        }

        private void txtRvitm5c_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RVITM5C");
        }
        private void txtRvitm5c1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RVITM5C1");
        }

        private void txtItem6_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "ITEM6");
        }
        private void txtItem61_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "ITEM61");
        }

        private void txtCapexp_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "CAPEXP");
        }
        private void txtCapexp1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "CAPEXP1");
        }

        private void txtRunits_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RUNITS");
        }
        private void txtRunits1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RUNITS1");
        }

        private void txtCostpu_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "COSTPU");
        }
        private void txtCostpu1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "COSTPU1");
        }

        private void txtStrtdate_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "STRTDATE");
            if (txtStrtdate.Text.Length ==6)
            {
                if (txtStrtdate1.Visible)
                    txtStrtdate1.Focus();
                else
                    cbFipStateSel.Focus();
            }

        }
        private void txtStrtdate1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "STRTDATE1");
            if (txtSeldate1.Text.Length == 6)
                cbFipStateSel.Focus(); 
        }

        private void txtCompdate_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "COMPDATE");
            if (txtCompdate.Text.Length ==6)
            {
                if (txtCompdate1.Visible)
                    txtCompdate1.Focus();
                else
                    txtItem6.Focus();
            }
        }
        private void txtCompdate1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "COMPDATE1");
            if (txtCompdate1.Text.Length == 6)
                txtItem6.Focus();
        }

        private void txtFutcompd_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "FUTCOMPD");
            if ((txtFutcompd.Text.Length == 6) && (txtFutcompd1.Visible))
                txtFutcompd1.Focus();

        }
        private void txtFutcompd1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "FUTCOMPD1");
        }


        private void ResetParameters()
        {
            txtStatus.Text = "";
            txtId.Text = "";
            txtRespid.Text = "";
           
            cbFipStateSel.SelectedIndex = -1;
            cbSourceSel.SelectedIndex = 0;

            txtActive.Text = "";
            txtOwner.Text = "";
            txtSeldate.Text = "";
            txtSeldate1.Text = "";
            txtNewtc.Text = "";
            txtNewtc1.Text = "";
            txtRvitm5c.Text = "";
            txtRvitm5c1.Text = "";
            txtItem6.Text = "";
            txtItem61.Text = "";

            txtCapexp.Text = "";
            txtCapexp1.Text = "";
            txtRunits.Text = "";
            txtRunits1.Text = "";
            txtCostpu.Text = "";
            txtCostpu1.Text = "";
            txtStrtdate.Text = "";
            txtStrtdate1.Text = "";
            txtCompdate.Text = "";
            txtCompdate1.Text = "";
            txtFutcompd.Text = "";
            txtFutcompd1.Text = "";
        }


        private void cbSeldate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSeldate.SelectedItem != null && cbSeldate.SelectedItem.ToString() == "Between")
            {
                lbSeldateto.Visible = true;
                txtSeldate1.Visible = true;
            }
            else
            {
                lbSeldateto.Visible = false;
                txtSeldate1.Visible = false;
            }
        }

        private void cbNewtc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNewtc.SelectedItem != null && cbNewtc.SelectedItem.ToString() == "Between")
            {
                lbNewtcto.Visible = true;
                txtNewtc1.Visible = true;
                btnNewtc1.Visible = true;
            }
            else
            {
                lbNewtcto.Visible = false;
                txtNewtc1.Visible = false;
                btnNewtc1.Visible = false;
            }
        }

        private void cbRvitm5c_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRvitm5c.SelectedItem != null && cbRvitm5c.SelectedItem.ToString() == "Between")
            {
                lbRvitm5cto.Visible = true;
                txtRvitm5c1.Visible = true;
            }
            else
            {
                lbRvitm5cto.Visible = false;
                txtRvitm5c1.Visible = false;
            }
        }

        private void cbItem6_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbItem6.SelectedItem != null && cbItem6.SelectedItem.ToString() == "Between")
            {
                lbItem6to.Visible = true;
                txtItem61.Visible = true;
            }
            else
            {
                lbItem6to.Visible = false;
                txtItem61.Visible = false;
            }
        }

        private void cbCapexp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCapexp.SelectedItem != null && cbCapexp.SelectedItem.ToString() == "Between")
            {
                lbCapexpto.Visible = true;
                txtCapexp1.Visible = true;
            }
            else
            {
                lbCapexpto.Visible = false;
                txtCapexp1.Visible = false;
            }
        }

        private void cbRunits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbRunits.SelectedItem != null && cbRunits.SelectedItem.ToString() == "Between")
            {
                lbRunitsto.Visible = true;
                txtRunits1.Visible = true;
            }
            else
            {
                lbRunitsto.Visible = false;
                txtRunits1.Visible = false;
            }
        }

        private void cbCostpu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCostpu.SelectedItem != null && cbCostpu.SelectedItem.ToString() == "Between")
            {
                lbCostputo.Visible = true;
                txtCostpu1.Visible = true;
            }
            else
            {
                lbCostputo.Visible = false;
                txtCostpu1.Visible = false;
            }
        }

        private void cbStrtdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbStrtdate.SelectedItem != null && cbStrtdate.SelectedItem.ToString() == "Between")
            {
                lbStrtdateto.Visible = true;
                txtStrtdate1.Visible = true;
            }
            else
            {
                lbStrtdateto.Visible = false;
                txtStrtdate1.Visible = false;
            }
        }

        private void cbCompdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbCompdate.SelectedItem != null && cbCompdate.SelectedItem.ToString() == "Between")
            {
                lbCompdateto.Visible = true;
                txtCompdate1.Visible = true;
            }
            else
            {
                lbCompdateto.Visible = false;
                txtCompdate1.Visible = false;
            }
        }

        private void cbFutcompd_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFutcompd.SelectedItem != null && cbFutcompd.SelectedItem.ToString() == "Between")
            {
                lbFutcompdto.Visible = true;
                txtFutcompd1.Visible = true;
            }
            else
            {
                lbFutcompdto.Visible = false;
                txtFutcompd1.Visible = false;
            }
        }


        private void btnStatus_Click(object sender, EventArgs e)
        {
            frmStatusSel popup = new frmStatusSel();

            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.OK)
            {
                txtStatus.Text = popup.selectedStatus;
            }

            popup.Dispose();
        }

        private void btnActive_Click(object sender, EventArgs e)
        {
            frmActiveSel popup = new frmActiveSel();

            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.OK)
            {
                txtActive.Text = popup.selectedActive;
            }

            popup.Dispose();
        }


        private void btnOwner_Click(object sender, EventArgs e)
        {
            frmSurveySel popup = new frmSurveySel();

            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.OK)
            {
                txtOwner.Text = popup.selectedSurvey;
            }

            popup.Dispose();

        }

        private void btnNewtc_Click(object sender, EventArgs e)
        {
            frmNewtcSel popup = new frmNewtcSel();

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

            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                txtNewtc1.Text = popup.SelectedNewtc;
            }

            popup.Dispose();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to print.");
            }
            else
            {
                if (dgC700Srch.RowCount >= 150)
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
        }

        private void SetHeaderCellValue()
        {
            int rowNumber = 1;
            foreach (DataGridViewRow dr in dgC700Srch.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgC700Srch.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;
            SetHeaderCellValue();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "C700 SEARCH";
           
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            if (dgC700Srch.SortedColumn == null)
                printer.SubTitle = BuildSearchCriteria();
            else
                printer.SubTitle = BuildSearchCriteria() + "  Sorted By: " + dgC700Srch.SortedColumn.Name;
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
           
            /*Hide the columns */
            printer.HideColumns.Add("source");
            printer.HideColumns.Add("fwgt");
            printer.HideColumns.Add("compdate");
            printer.HideColumns.Add("futcompd");
            printer.HideColumns.Add("rbldgs");
            printer.HideColumns.Add("runits");
            printer.HideColumns.Add("costpu");
            printer.HideColumns.Add("Pct5c6");

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "C700 Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //printer.PrintPreviewDataGridView(dgSample);
            printer.PrintDataGridViewWithoutDialog(dgC700Srch);

            Cursor.Current = Cursors.Default;

        }

        private string BuildSearchCriteria()
        {
            string criteria = "Search Criteria: ";

            if (txtId.Text != "")
                criteria += " Id = " + txtId.Text;
            if (txtRespid.Text != "")
                criteria += " Respid = " + txtRespid.Text;
            if (txtStatus.Text != "")
                criteria += " Status = " + txtStatus.Text;
            if (cbSourceSel.Text != "")
                criteria += " Source = " + cbSourceSel.Text;
            if (cbFipStateSel.Text != "")
                criteria += " Fipstate = " + cbFipStateSel.Text.Substring(0, 2);
            if (txtOwner.Text.Trim() != "")
                criteria += " Owner = " + txtOwner.Text;
            if (txtActive.Text.Trim() != "")
                criteria += " Active = " + txtActive.Text;
            if (txtSeldate.Text != "")
            {
                if ((cbSeldate.Text == "Between") && (txtSeldate1.Text != ""))
                    criteria += " Seldate " + GeneralFunctions.ConvertOperatorToSymbol(cbSeldate.Text) + " (" + txtSeldate.Text + " - " + txtSeldate1.Text + ")";
                else
                    criteria += " Seldate " + GeneralFunctions.ConvertOperatorToSymbol(cbSeldate.Text) + " " + txtSeldate.Text;
            }
            if (txtNewtc.Text != "")
            {
                if ((cbNewtc.Text == "Between") && (txtNewtc1.Text != ""))
                    criteria += " Newtc " + GeneralFunctions.ConvertOperatorToSymbol(cbNewtc.Text) + " (" + txtNewtc.Text + " - " + txtNewtc1.Text + ")";
                else
                    criteria += " Newtc " + GeneralFunctions.ConvertOperatorToSymbol(cbNewtc.Text) + " " + txtNewtc.Text;
            }
            
            if (txtRvitm5c.Text != "")
            {
                if ((cbRvitm5c.Text == "Between") && (txtRvitm5c1.Text != ""))
                    criteria += " Rvitm5c " + GeneralFunctions.ConvertOperatorToSymbol(cbRvitm5c.Text) + " (" + txtRvitm5c.Text + " - " + txtRvitm5c1.Text + ")";
                else
                    criteria += " Rvitm5c " + GeneralFunctions.ConvertOperatorToSymbol(cbRvitm5c.Text) + " " + txtRvitm5c.Text;
            }
            if (txtItem6.Text != "")
            {
                if ((cbItem6.Text == "Between") && (txtItem61.Text != ""))
                    criteria += " Item6 " + GeneralFunctions.ConvertOperatorToSymbol(cbItem6.Text) + " (" + txtItem6.Text + " - " + txtItem61.Text + ")";
                else
                    criteria += " Item6 " + GeneralFunctions.ConvertOperatorToSymbol(cbItem6.Text) + " " + txtItem6.Text;
            }
            if (txtCapexp.Text != "")
            {
                if ((cbCapexp.Text == "Between") && (txtCapexp1.Text != ""))
                    criteria += " Capexp " + GeneralFunctions.ConvertOperatorToSymbol(cbCapexp.Text) + " (" + txtCapexp.Text + " - " + txtCapexp1.Text + ")";
                else
                    criteria += " Capexp " + GeneralFunctions.ConvertOperatorToSymbol(cbCapexp.Text) + " " + txtCapexp.Text;
            }
            if (txtRunits.Text != "")
            {
                if ((cbRunits.Text == "Between") && (txtRunits1.Text != ""))
                    criteria += " Runits " + GeneralFunctions.ConvertOperatorToSymbol(cbRunits.Text) + " (" + txtRunits.Text + " - " + txtRunits1.Text + ")";
                else
                    criteria += " Runits " + GeneralFunctions.ConvertOperatorToSymbol(cbRunits.Text) + " " + txtRunits.Text;
            }
            if (txtCostpu.Text != "")
            {
                if ((cbCostpu.Text == "Between") && (txtCostpu1.Text != ""))
                    criteria += " Costpu " + GeneralFunctions.ConvertOperatorToSymbol(cbCostpu.Text) + " (" + txtCostpu.Text + " - " + txtCostpu1.Text + ")";
                else
                    criteria += " Costpu " + GeneralFunctions.ConvertOperatorToSymbol(cbCostpu.Text) + " " + txtCostpu.Text;
            }
            if (txtStrtdate.Text != "")
            {
                if ((cbStrtdate.Text == "Between") && (txtStrtdate1.Text != ""))
                    criteria += " Strtdate " + GeneralFunctions.ConvertOperatorToSymbol(cbStrtdate.Text) + " (" + txtStrtdate.Text + " - " + txtStrtdate1.Text + ")";
                else
                    criteria += " Strtdate " + GeneralFunctions.ConvertOperatorToSymbol(cbStrtdate.Text) + " " + txtStrtdate.Text;
            }
            if (txtCompdate.Text != "")
            {
                if ((cbCompdate.Text == "Between") && (txtCompdate1.Text != ""))
                    criteria += " Compdate " + GeneralFunctions.ConvertOperatorToSymbol(cbCompdate.Text) + " (" + txtCompdate.Text + " - " + txtCompdate1.Text + ")";
                else
                    criteria += " Compdate " + GeneralFunctions.ConvertOperatorToSymbol(cbCompdate.Text) + " " + txtCompdate.Text;
            }
            if (txtFutcompd.Text != "")
            {
                if ((cbFutcompd.Text == "Between") && (txtFutcompd1.Text != ""))
                    criteria += " Futcompdate " + GeneralFunctions.ConvertOperatorToSymbol(cbFutcompd.Text) + " (" + txtFutcompd.Text + " - " + txtFutcompd1.Text + ")";
                else
                    criteria += " Futcompdate " + GeneralFunctions.ConvertOperatorToSymbol(cbFutcompd.Text) + " " + txtFutcompd.Text;
            }

            return criteria;

        }

 

        private void btnName_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Projects Results list is empty. No Record Selected.");
                return;
            }

            //check initial cases
            int index = dgC700Srch.CurrentRow.Index;
            string seldate = dgC700Srch["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgC700Srch["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }
            
            this.Hide();        // hide parent

            DataGridViewSelectedRowCollection rows = dgC700Srch.SelectedRows;

            string val1 = dgC700Srch["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();

            int xcnt = 0;
            frmName fName = new frmName();

            foreach (DataGridViewRow dr in dgC700Srch.Rows)
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

          //  GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

            fName.ShowDialog();    //show child     

         //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Projects Results list is empty. No Record Selected.");
            }
            else
            {
                this.Hide();        // hide parent

                DataGridViewSelectedRowCollection rows = dgC700Srch.SelectedRows;

                int index = dgC700Srch.CurrentRow.Index;

                string mid = dgC700Srch["MASTERID", index].Value.ToString();

                frmSource fm = new frmSource();

                // Store Masterid in list for Page Up and Page Down

                List<int> Masteridlist = new List<int>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgC700Srch.Rows)
                {
                    string val1 = dgC700Srch["MASTERID", cnt].Value.ToString();
                    int val = Int32.Parse(val1);
                    Masteridlist.Add(val);
                    cnt = cnt + 1;
                }

                fm.Masterid = Int32.Parse(mid);
                fm.Masteridlist = Masteridlist;
                fm.CurrIndex = index;
                fm.CallingForm = this;

            //    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fm.ShowDialog();  // show child 

            //    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Projects Results list is empty. No Record Selected.");
                return;
            }

            //check initial cases
            int index = dgC700Srch.CurrentRow.Index;
            string seldate = dgC700Srch["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgC700Srch["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }

            this.Hide();        // hide parent

            DataGridViewSelectedRowCollection rows = dgC700Srch.SelectedRows;

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                string resp = dgC700Srch["RESPID", index].Value.ToString();
                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

               // GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {
                string val1 = dgC700Srch["ID", index].Value.ToString();

                // Store ID in list for Page Up and Page Down

                List<string> Idlist = new List<string>();

                int xcnt = 0;
                frmC700 fC700 = new frmC700();

                foreach (DataGridViewRow dr in dgC700Srch.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
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

                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CallingForm = this;

               // GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fC700.ShowDialog(); // show child
            }

           // GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
        }

        private void txtId_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtId_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtRespid_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtRespid_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtSeldate_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }
        private void txtSeldate_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtSeldate1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtSeldate1_Leave(object sender, EventArgs e)
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

        private void txtRvitm5c_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtRvitm5c_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtRvitm5c1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtRvitm5c1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtItem6_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtItem6_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtItem61_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtItem61_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }


        private void txtCapexp_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtCapexp_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtCapexp1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtCapexp1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtRunits_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtRunits_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtRunits1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtRunits1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtCostpu_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtCostpu_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtCostpu1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtCostpu1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtStrtdate_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtStrtdate_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtStrtdate1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtStrtdate1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtCompdate_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtCompdate_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtCompdate1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtCompdate1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtFutcompd_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtFutcompd_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtFutcompd1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtFutcompd1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetParameters();
            SetHiddenLbl();
            SetOperatorCombo();

            dgC700Srch.DataSource = dataObject.GetEmptyTable();

            lblRecordCount.Text = " ";
            txtId.Focus();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {

            // Verify if any parameter entered

            if (txtId.Text     == "" && txtRespid.Text   == "" &&
                cbFipStateSel.Text == "" && cbSourceSel.Text == "" && 
                txtStatus.Text     == "" && txtActive.Text   == "" && txtOwner.Text == "" &&
                txtSeldate.Text    == "" && txtSeldate1.Text == "" && 
                txtNewtc.Text      == "" && txtNewtc1.Text   == "" &&
                txtRvitm5c.Text    == "" && txtRvitm5c1.Text == "" &&
                txtItem6.Text      == "" && txtItem61.Text   == "" &&
                txtCapexp.Text     == "" && txtCapexp1.Text  == "" &&
                txtRunits.Text     == "" && txtRunits1.Text  == "" &&
                txtCostpu.Text     == "" && txtCostpu1.Text  == "" &&
                txtStrtdate.Text   == "" && txtStrtdate.Text == "" &&
                txtCompdate.Text   == "" && txtCompdate.Text == "" &&
                txtFutcompd.Text   == "" && txtFutcompd1.Text == "")
                
            {
                MessageBox.Show("Please Enter Search Criteria.");
                txtId.Focus();
                return;
            }

            //Check if other parameters with Id

            if (txtId.Text != "" &&
                (txtRespid.Text  != "" || cbFipStateSel.Text != "" || cbSourceSel.Text != "" ||
                txtOwner.Text    != "" || txtStatus.Text   != "" || txtActive.Text != "" ||
                txtSeldate.Text  != "" || txtNewtc.Text    != "" ||
                txtRvitm5c.Text  != "" || txtItem6.Text    != "" || 
                txtCapexp.Text   != "" || txtRunits.Text   != "" ||
                txtStrtdate.Text != "" || txtCompdate.Text != "" || txtFutcompd.Text != ""))
            {
                MessageBox.Show("Other Search Criteria should not be included with ID Search.", "Entry Error");
                ResetParameters();
                SetHiddenLbl();
                SetOperatorCombo();
                txtId.Focus();
                return;
            }

            if (txtId.Text.Trim() != "")
            {
                string id = txtId.Text.Trim();

                if (!(id.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits.");
                    txtId.Text = "";
                    txtId.Focus();
                    return;
                }
                else if (!GeneralDataFuctions.ValidateSampleId(id))
                {
                    MessageBox.Show("Invalid ID.");
                    txtId.Text = "";
                    txtId.Focus();
                    return;
                }
            }

            if (txtRespid.Text.Trim() != "")
            {
                string respid = txtRespid.Text.Trim();

                if (!(respid.Length == 7))
                {
                    MessageBox.Show("RESPID should be 7 digits.");
                    txtRespid.Text = "";
                    txtRespid.Focus();
                    return;
                }
                else if (!GeneralDataFuctions.ChkRespid(respid))
                {
                    MessageBox.Show("Invalid RESPID.");
                    txtRespid.Text = "";
                    txtRespid.Focus();
                    return;
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
                        return;
                    }
                    else if (!GeneralDataFuctions.CheckNewTC(txtNewtc.Text) || ((cbNewtc.Text == "Between") && !GeneralDataFuctions.CheckNewTC(txtNewtc1.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtNewtc.Text = "";
                        txtNewtc.Focus();
                        if (cbNewtc.Text == "Between") txtNewtc1.Text = "";
                        return;
                    }
                }
            }

            if (VerifyDateFields() && VerifyBetween())
            {
                this.Cursor = Cursors.WaitCursor;
                GetC700Data();
                this.Cursor = Cursors.Default;
            }
        }

        private void GetC700Data()
        {
            dgC700Srch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgC700Srch.RowHeadersVisible = false; // set it to false if not needed

            DataTable dt = new DataTable();

            dgC700Srch.DataSource = dt;

            dt = GetDataTable1();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");
                txtId.Focus();
            }

            dgC700Srch.DataSource = dt;

            for (int i = 0; i < dgC700Srch.ColumnCount; i++)
            {
                //Source(4), Dodgenum(8),Projselv(10),Item6(14),Capexp(15), active(19),Psu(23) ,Psupl(24) are not shown

                if (i == 4 || i == 8 || i == 10 || i == 14 || i == 15 || i == 19 || i == 23 || i == 24)
                {
                    dgC700Srch.Columns[i].Visible = false;
                }


                //Id
                if (i == 0)
                {
                    dgC700Srch.Columns[i].Width = 90;
                    dgC700Srch.Columns[i].Frozen = true;
                }

                //FIN
                if (i == 1)
                {
                    dgC700Srch.Columns[i].Width = 130;
                    dgC700Srch.Columns[i].Frozen = true;
                }

                //Respid(2),Source(3),Survey(5) and Newtc(6) are smaller
                if (i == 2 || i == 3 || i == 5 || i == 6)
                {
                    dgC700Srch.Columns[i].Width = 80;
                }

                //Fipstate(7) is wider
                if (i == 7 )
                {
                    dgC700Srch.Columns[i].Width = 95;
                }

                //Rvitm5c(11) are wider
                if (i == 11)
                {
                    dgC700Srch.Columns[i].Width = 90;
                    dgC700Srch.Columns[i].DefaultCellStyle.Format = "N0";
                }


                //Fwgt(12)  are smallest
                if (i == 12 )
                {
                    dgC700Srch.Columns[i].Width = 70;
                }

                //Pct5c6(13), Rbldgs(20) and Runits(21) are small
                if (i == 13 || i == 20 || i == 21)
                {
                    dgC700Srch.Columns[i].Width = 80;
                }


                //ID(0), Respid(2),Status(3), Owner(5), Newtc(6), Fipstate(7), Seldate(9), Strtdate(16), Compdate(17), Futcompd(18) are Centered
                if (i == 0 || i ==2 || i == 3 || i == 5 || i == 6 || i == 7 || i == 9 || i == 16 || i == 17 || i == 18)
                {
                    dgC700Srch.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //Rvitm5c(11), Fwgt(12), Pct5c6(13), Rbldgs(20), Runits(21), costput are displayed right adjusted
                if (i == 11 || i == 12 || i == 13 || i == 20 || i == 21 || i== 22)
                {
                    dgC700Srch.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                //Rvitm5c(11), Rbldgs(20), Runits(21), Costpu(22) are displayed comma format
                if (i == 11 || i == 20 || i == 21 || i == 22)
                {
                    dgC700Srch.Columns[i].DefaultCellStyle.Format = "N0";
                }

            }
            //before load data, turn off rowheadersvisible
            dgC700Srch.RowHeadersVisible =true;

            if (dt.Rows.Count == 1)
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASE FOUND";
            else
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASES FOUND";

        }

        private DataTable GetDataTable1()
        {
            string fst = string.Empty;

            if (cbFipStateSel.Text != "")
                fst = cbFipStateSel.SelectedValue.ToString();

            DataTable dt = dataObject.GetC700SearchData(txtId.Text, txtRespid.Text, cbSourceSel.Text, fst, txtOwner.Text, txtStatus.Text,
                            txtActive.Text, txtSeldate.Text.Trim(), cbSeldate.Text, txtSeldate1.Text, txtNewtc.Text, cbNewtc.Text,
                            txtNewtc1.Text, txtRvitm5c.Text, cbRvitm5c.Text, txtRvitm5c1.Text, txtItem6.Text, cbItem6.Text,
                            txtItem61.Text, txtCapexp.Text, cbCapexp.Text, txtCapexp1.Text, txtRunits.Text, cbRunits.Text, txtRunits1.Text,
                            txtCostpu.Text, cbCostpu.Text, txtCostpu1.Text, txtStrtdate.Text, cbStrtdate.Text, txtStrtdate1.Text, txtCompdate.Text,
                            cbCompdate.Text, txtCompdate1.Text, txtFutcompd.Text, cbFutcompd.Text, txtFutcompd1.Text);

            return dt;
        }

        //Verify date fields
        private Boolean VerifyDateFields()
        {
            if (txtSeldate.Text != "" && cbSeldate.Text != "StartsWith")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtSeldate.Text))
                {
                    MessageBox.Show("Seldate is not valid");
                    txtSeldate.Focus();
                    return false;
                }
            }

            if (txtSeldate1.Text != "")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtSeldate1.Text))
                {
                    MessageBox.Show("Seldate is not valid");
                    txtSeldate1.Focus();
                    return false;
                }
            }

            if (txtStrtdate.Text != "" && cbStrtdate.Text != "StartsWith")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtStrtdate.Text))
                {
                    MessageBox.Show("Strtdate is not valid");
                    txtStrtdate.Focus();
                    return false;
                }
            }

            if (txtStrtdate1.Text != "")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtStrtdate1.Text))
                {
                    MessageBox.Show("Strtdate is not valid");
                    txtStrtdate1.Focus();
                    return false;
                }
            }

            if (txtCompdate.Text != "" && cbCompdate.Text != "StartsWith")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtCompdate.Text))
                {
                    MessageBox.Show("Compdate is not valid");
                    txtCompdate.Focus();
                    return false;
                }
            }

            if (txtCompdate1.Text != "")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtCompdate1.Text))
                {
                    MessageBox.Show("Compdate is not valid");
                    txtCompdate1.Focus();
                    return false;
                }
            }

            if (txtFutcompd.Text != "" && cbFutcompd.Text != "StartsWith")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtFutcompd.Text))
                {
                    MessageBox.Show("Futcompd is not valid");
                    txtFutcompd.Focus();
                    return false;
                }
            }

            if (txtFutcompd1.Text != "")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtFutcompd1.Text))
                {
                    MessageBox.Show("Futcompd is not valid");
                    txtFutcompd1.Focus();
                    return false;
                }
            }

            return true;
        }

        //Verify Between fields
        private Boolean VerifyBetween()
        {
            Boolean result = false;

            if (cbSeldate.Text == "Between")
            {
                if (!GeneralFunctions.VerifyBetweenParameters(txtSeldate.Text, txtSeldate1.Text, "Seldate"))
                    return result;
            }
            if (cbNewtc.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtNewtc.Text, txtNewtc1.Text, "Newtc"))
                    return result;

            if (cbRvitm5c.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtRvitm5c.Text, txtRvitm5c1.Text, "Rvitm5c"))
                    return result;

            if (cbItem6.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtItem6.Text, txtItem61.Text, "Item6"))
                    return result;

            if (cbCapexp.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtCapexp.Text, txtCapexp1.Text, "Capexp"))
                    return result;

            if (cbRunits.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtRunits.Text, txtRunits1.Text, "Runits"))
                    return result;

            if (cbCostpu.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtCostpu.Text, txtCostpu1.Text, "Costpu"))
                    return result;

            if (cbStrtdate.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtStrtdate.Text, txtStrtdate1.Text, "Strtdate"))
                    return result;

            if (cbCompdate.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtCompdate.Text, txtCompdate1.Text, "Compdate"))
                    return result;

            if (cbFutcompd.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtFutcompd.Text, txtFutcompd1.Text, "Futcompd"))
                    return result;

            return result = true;

        }

        private void dgC700Srch_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgC700Srch.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmC700Srch_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSeldate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtSeldate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNewtc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRvitm5c_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRvitm5c1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtItem6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtItem61_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCapexp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCapexp1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRunits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRunits1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCostpu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCostpu1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtStrtdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtStrtdate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCompdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCompdate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFutcompd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFutcompd1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbStatus_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbFipstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbActive_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbOwner_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
