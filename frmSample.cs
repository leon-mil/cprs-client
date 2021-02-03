/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmSample.cs
Programmer    : 
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Search sample table
Change Request: 
Specification : Sample Search Specifications  
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
    public partial class frmSample : Cprs.frmCprsParent
    {
       
        private SampleSearchData dataObject;
        private string curr_survey_month;

        public frmSample()
        {
          
            InitializeComponent();

            dataObject = new SampleSearchData();
        }

        private void frmSample_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            ResetParameters();

            SetHiddenLbl();
            SetButtonTxt();

            SetOperatorCombo();

            PopulateFipStateCombo();

            dgSample.DataSource = dataObject.GetEmptyTable();
            dgSample.RowHeadersVisible = true;
            dgSample.AutoResizeColumns();

            lblRecordCount.Text = " ";

            //Add key down event to All textbox

            this.KeyPreview = true;

            txtId.KeyDown += new KeyEventHandler(txtId_KeyDown);

            txtRespid.KeyDown += new KeyEventHandler(txtRespid_KeyDown);

            txtFin.KeyDown += new KeyEventHandler(txtFin_KeyDown);

            txtSeldate.KeyDown += new KeyEventHandler(txtSeldate_KeyDown);
            txtSeldate1.KeyDown += new KeyEventHandler(txtSeldate1_KeyDown);

            txtNewtc.KeyDown += new KeyEventHandler(txtNewtc_KeyDown);
            txtNewtc1.KeyDown += new KeyEventHandler(txtNewtc1_KeyDown);

            txtProjselv.KeyDown += new KeyEventHandler(txtProjselv_KeyDown);
            txtProjselv1.KeyDown += new KeyEventHandler(txtProjselv1_KeyDown);

            txtRvitm5c.KeyDown += new KeyEventHandler(txtRvitm5c_KeyDown);
            txtRvitm5c1.KeyDown += new KeyEventHandler(txtRvitm5c1_KeyDown);

            txtPsu.KeyDown += new KeyEventHandler(txtPsu_KeyDown);

            txtPsuPlace.KeyDown += new KeyEventHandler(txtPsuPlace_KeyDown);    

            txtRunits.KeyDown += new KeyEventHandler(txtRunits_KeyDown);
            txtRunits1.KeyDown += new KeyEventHandler(txtRunits1_KeyDown);
            txtId.Focus();

            curr_survey_month = GeneralFunctions.CurrentYearMon();
        }


        private void txtPsu_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 5))
                Search();
        }

        private void txtPsuPlace_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
                Search();
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

        private void txtFin_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 9))
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

        private void txtProjselv_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtProjselv1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
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

        private void ResetParameters()
        {
           txtFin.Text = "";
           txtId.Text = "";
           txtRespid.Text = "";
           txtPsu.Text = "";
           txtPsuPlace.Text = "";
           
           cbFipStateSel.SelectedIndex = -1;
           cbSourceSel.SelectedIndex = 0;
           txtOwner.Text = "";
           txtSeldate.Text = "";
           txtSeldate1.Text = "";
           txtNewtc.Text = "";
           txtNewtc1.Text = "";
           txtProjselv.Text = "";
           txtProjselv1.Text = "";
           txtRvitm5c.Text = "";
           txtRvitm5c1.Text = "";
           txtRunits.Text = "";
           txtRunits1.Text = "";
        }

        private void SetOperatorCombo()
        {
            cbId.SelectedIndex = 0;
            cbRespid.SelectedIndex = 0;
            cbFin.SelectedIndex = 0;
            cbSource.SelectedIndex = 0;
            cbFipstate.SelectedIndex = 0;

            cbOwner.SelectedIndex = 0;
            cbSeldate.SelectedIndex = 0;
            cbNewtc.SelectedIndex = 0;
            cbProjselv.SelectedIndex = 0;
            cbRvitm5c.SelectedIndex = 0;

            cbPsuPlace.SelectedIndex = 0;
            cbPsu.SelectedIndex = 0;
            cbRunits.SelectedIndex = 0;
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

            lbProjselvto.Visible = false;
            txtProjselv1.Visible = false;

            lbRvitm5cto.Visible = false;
            txtRvitm5c1.Visible = false;

            lbRunitsto.Visible = false;
            txtRunits1.Visible = false;
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

        private void cbProjselv_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbProjselv.SelectedItem != null && cbProjselv.SelectedItem.ToString() == "Between")
            {
                lbProjselvto.Visible = true;
                txtProjselv1.Visible = true;
            }
            else
            {
                lbProjselvto.Visible = false;
                txtProjselv1.Visible = false;
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
            if (cbProjselv.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtProjselv.Text, txtProjselv1.Text, "Projselv"))
                    return result;
            if (cbRvitm5c.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtRvitm5c.Text, txtRvitm5c1.Text, "Rvitm5c"))
                    return result;
            if (cbRunits.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtRunits.Text, txtRunits1.Text, "Runits"))
                    return result;

            return result = true;

        }


        private void txtPsu_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "PSU");
            if (txtPsu.Text.Length == 5)
                cbSourceSel.Focus();
        }
        private void txtPsuPlace_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "BPOID");
            if (txtPsuPlace.Text.Length == 6)
                cbFipStateSel.Focus();
        }

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "ID");
        }

        private void txtRespid_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RESPID");
            if (txtRespid.Text.Length == 7)
                txtSeldate.Focus();
        }

        private void txtFin_TextChanged(object sender, EventArgs e)
        {
         //   GeneralFunctions.CheckIntegerField(sender, "FIN");
        }

        private void txtNewtc_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "NEWTC");
            if (txtNewtc.Text.Length == 4)
            {
                if (!txtNewtc1.Visible)
                    txtPsuPlace.Focus();
                else
                    txtNewtc1.Focus();

            }
        }
        private void txtNewtc1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "NEWTC1");
            if (txtNewtc1.Text.Length == 4)
                txtPsuPlace.Focus();
        }

        private void txtProjselv_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "PROJSELV");
        }
        private void txtProjselv1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "PROJSELV1");
        }

        private void txtSeldate_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE");
            if (txtSeldate.Text.Length == 6)
            {
                if (!txtSeldate1.Visible)
                    txtPsu.Focus();
                else
                    txtSeldate1.Focus();
            }
        }
        private void txtSeldate1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE1");
            if (txtSeldate1.Text.Length == 6)
                txtPsu.Focus();
         }

        private void txtRvitm5c_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RVITM5C");
        }
        private void txtRvitm5c1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RVITM5C1");
        }

        private void txtRunits_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RUNITS");
        }
        private void txtRunits1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RUNITS1");
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

        private void Search()
        {

            // Verify if any parameter entered

            if (txtFin.Text == "" && txtId.Text == "" && txtRespid.Text == "" &&
                cbFipStateSel.Text == "" && cbSourceSel.Text == "" && txtOwner.Text == "" &&
                txtSeldate.Text == "" && txtNewtc.Text == "" &&
                txtProjselv.Text == "" && txtRvitm5c.Text == "" &&
                txtPsu.Text == "" && txtPsuPlace.Text == "" && 
                txtRunits.Text == "")
            {
                MessageBox.Show("Please Enter Search Criteria.");
                txtId.Focus();
                return;
            }
            
            //Check if other parameters with Id

            if (txtId.Text != "" &&
                (txtFin.Text != "" || txtRespid.Text != "" ||
                txtPsu.Text != "" || txtPsuPlace.Text != "" || 
                cbFipStateSel.Text != "" || cbSourceSel.Text != "" ||
                txtOwner.Text != "" || txtSeldate.Text != "" ||
                txtNewtc.Text != "" || txtProjselv.Text != "" || txtRvitm5c.Text != "" || txtRunits.Text != ""))
             {
                 MessageBox.Show("Other Search Criteria should not be included with Id Search.", "Entry Error");
                 ResetParameters();
                 SetHiddenLbl();
                 SetOperatorCombo();
                 txtId.Focus();
                 return;
             }
                    
            //Check if other parameters with FIN

             if (txtFin.Text != "" && cbFin.SelectedItem.ToString() == "Equals" &&
                 (txtId.Text != "" || txtRespid.Text != "" || 
                 txtPsu.Text != "" || txtPsuPlace.Text != "" || 
                 cbFipStateSel.Text != "" || cbSourceSel.Text != "" ||
                 txtOwner.Text != "" || txtSeldate.Text != "" ||
                 txtNewtc.Text != "" || txtProjselv.Text != "" || txtRvitm5c.Text != "" || txtRunits.Text != ""))
             {
                 MessageBox.Show("Other Search Criteria should not be included with Dodgenum Search.", "Entry Error");

                 ResetParameters();
                 SetHiddenLbl();
                 SetOperatorCombo();
                 txtId.Focus();
                 return;
             }

            if (txtFin.Text != "" && cbFin.Text == "Equals")
            {
                string fin = txtFin.Text.Trim();

                if (!GeneralDataFuctions.CheckFin(fin))
                {
                    MessageBox.Show("Invalid FIN.");
                    txtFin.Text = "";
                    txtFin.Focus();
                    return;
                }
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
                GetSampleData();
                this.Cursor = Cursors.Default;
            }
        }

        //Verify date fields
        private Boolean VerifyDateFields()
        {
            Boolean result = false;
            if (txtSeldate.Text != "" && cbSeldate.Text != "StartsWith")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtSeldate.Text))
                {
                    MessageBox.Show("Seldate is not valid");
                    txtSeldate.Focus();
                    return result;
                }
            }

            if (txtSeldate1.Text != "")
            {
                if (!GeneralFunctions.ValidateDateWithRange(txtSeldate1.Text))
                {
                    MessageBox.Show("Seldate is not valid");
                    txtSeldate1.Focus();
                    return result;
                }
            }

            result = true;
            return result;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private DataTable GetDataTable1()
        {
            string fst = string.Empty;

            if (cbFipStateSel.Text != "")
                fst = cbFipStateSel.SelectedValue.ToString();

            DataTable dt = dataObject.GetSampleSearchData(txtId.Text, txtRespid.Text, txtFin.Text,
                    cbFin.Text, cbSourceSel.Text, fst, txtOwner.Text, txtSeldate.Text, cbSeldate.Text,
                    txtSeldate1.Text, txtNewtc.Text, cbNewtc.Text, txtNewtc1.Text, txtProjselv.Text,
                    cbProjselv.Text, txtProjselv1.Text, txtRvitm5c.Text, cbRvitm5c.Text,
                    txtRvitm5c1.Text,txtRunits.Text, cbRunits.Text, txtRunits1.Text, txtPsu.Text,
                    txtPsuPlace.Text);

            return dt;

        }

        private void GetSampleData()
        {
            dgSample.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgSample.RowHeadersVisible = false; // set it to false if not needed

            DataTable dt = GetDataTable1();
            
            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");
                txtId.Focus();
            }

            dgSample.DataSource = dt;

            for (int i = 0; i < dgSample.ColumnCount; i++)
            {
                //Source(4), Dodgenum(8), Pct5c6(13),Item6(14),Capexp(15), active(19), Costpu(22),Psu(23) and Psupl(24) are not shown

                if (i == 4 || i == 8 || i == 13 || i == 14 || i == 15 || i == 19 || i >= 22)
                {
                    dgSample.Columns[i].Visible = false;
                }

                //ID
                if (i == 0)
                {
                    dgSample.Columns[i].Width = 90;
                    dgSample.Columns[i].Frozen = true;
                }

                //FIN
                if (i == 1)
                {
                    dgSample.Columns[i].Width = 150;
                    dgSample.Columns[i].Frozen = true;
                }

                //Respid(2),Status(3),Owner(5) and Newtc(6) are smaller
                if (i == 2|| i == 3 || i == 5 || i == 6)
                {
                    dgSample.Columns[i].Width = 80;
                }

                //Fipstate(7),Projselv(10) are wider
                if (i == 7 || i == 10)
                {
                    dgSample.Columns[i].Width = 95;
                }

                //Rvitm5c(11) are wider
                if (i == 11)
                {
                    dgSample.Columns[i].Width = 90;
                    dgSample.Columns[i].DefaultCellStyle.Format = "N0";
                }


                //Fwgt(12) is smallest
                if (i == 12)
                {
                    dgSample.Columns[i].Width = 70;
                }

                //Rbldgs(20) and Runits(21) are small
                if (i == 20 || i == 21)
                {
                    dgSample.Columns[i].Width = 80;
                }
             

                //ID(0), Respid(2), Status(3), Owner(5), Newtc(6), Fipstate(7),Seldate(9), Strtdate(16), Compdate(17), Futcompd(18) are Centered
                if (i == 0 || i == 2 || i == 3 || i == 5 || i == 6 || i == 7 || i == 9 || i == 16 || i == 17 || i == 18)
                {
                    dgSample.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //Projselv(10),Rvitm5c(11), Fwgt(12), Rbldgs(20), Runits(21) are displayed right adjusted
                if (i == 10 || i == 11 || i == 12 || i == 20 || i == 21)
                {
                    dgSample.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                //Projselv(10),Rvitm5c(11), Rbldgs(20), Runits(21) are displayed comma format
                if (i == 10 || i == 11 || i == 20 || i == 21)
                {
                    dgSample.Columns[i].DefaultCellStyle.Format = "N0";
                }

            }
            dgSample.RowHeadersVisible = true;

            if (dt.Rows.Count == 1)
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASE FOUND";
            else
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASES FOUND";

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetParameters();
            SetHiddenLbl();
            SetOperatorCombo();

            dgSample.DataSource = dataObject.GetEmptyTable();

            lblRecordCount.Text = " ";
            txtId.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {

            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to print.");
            }
            else
            {
                if (dgSample.RowCount >= 150)
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
            foreach (DataGridViewRow dr in dgSample.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgSample.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            SetHeaderCellValue();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "SAMPLE SEARCH";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            if (dgSample.SortedColumn == null)
                printer.SubTitle = BuildSearchCriteria();
            else
                printer.SubTitle = BuildSearchCriteria() + "  Sorted By: " + dgSample.SortedColumn.Name;
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            /*Hide the columns */
            printer.HideColumns.Add("dodgenum");
            printer.HideColumns.Add("status");
            
            printer.HideColumns.Add("projselv");
            printer.HideColumns.Add("fwgt");
            printer.HideColumns.Add("compdate");
            printer.HideColumns.Add("futcompd");
            printer.HideColumns.Add("rbldgs");
            printer.HideColumns.Add("runits");

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Sample Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //printer.PrintPreviewDataGridView(dgSample);
            printer.PrintDataGridViewWithoutDialog(dgSample);

            Cursor.Current = Cursors.Default;

        }

        private string BuildSearchCriteria()
        {
            string criteria = "Search Criteria: ";

            if (txtFin.Text != "")
                criteria += " Fin = " + txtFin.Text;
            if (txtId.Text != "")
                criteria += " Id = " + txtId.Text;
            if (txtRespid.Text != "")
                criteria += " Respid = " + txtRespid.Text;
            if (cbSourceSel.Text != "")
                criteria += " Source = " + cbSourceSel.Text;
            if (cbFipStateSel.Text != "")
                criteria += " Fipstate = " + cbFipStateSel.Text.Substring(0, 2);
            if (txtOwner.Text.Trim() != "")
                criteria += " Owner = " + txtOwner.Text;
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
            if (txtProjselv.Text != "")
            {
                if ((cbProjselv.Text == "Between") && (txtProjselv1.Text != ""))
                    criteria += " Projselv " + GeneralFunctions.ConvertOperatorToSymbol(cbProjselv.Text) + " (" + txtProjselv.Text + " - " + txtProjselv1.Text + ")";
                else
                    criteria += " Projselv " + GeneralFunctions.ConvertOperatorToSymbol(cbProjselv.Text) + " " + txtProjselv.Text;
            }
            if (txtRvitm5c.Text != "")
            {
                if ((cbRvitm5c.Text == "Between") && (txtRvitm5c1.Text != ""))
                    criteria += " Rvitm5c " + GeneralFunctions.ConvertOperatorToSymbol(cbRvitm5c.Text) + " (" + txtRvitm5c.Text + " - " + txtRvitm5c1.Text + ")";
                else
                    criteria += " Rvitm5c " + GeneralFunctions.ConvertOperatorToSymbol(cbRvitm5c.Text) + " " + txtRvitm5c.Text;
            }
            if (txtRunits.Text != "")
            {
                if ((cbRunits.Text == "Between") && (txtRunits1.Text != ""))
                    criteria += " Runits " + GeneralFunctions.ConvertOperatorToSymbol(cbRunits.Text) + " (" + txtRunits.Text + " - " + txtRunits1.Text + ")";
                else
                    criteria += " Runits " + GeneralFunctions.ConvertOperatorToSymbol(cbRunits.Text) + " " + txtRunits.Text;
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
            int index = dgSample.CurrentRow.Index;
            string seldate = dgSample["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgSample["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }
            this.Hide();    // hide parent

            DataGridViewSelectedRowCollection rows = dgSample.SelectedRows;

            string val1 = dgSample["ID", index].Value.ToString();

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();
            frmName fName = new frmName();
            int xcnt = 0;

            foreach (DataGridViewRow dr in dgSample.Rows)
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

            fName.ShowDialog();   // show child    

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

                DataGridViewSelectedRowCollection rows = dgSample.SelectedRows;

                int index = dgSample.CurrentRow.Index;

                string mid = dgSample["MASTERID", index].Value.ToString();

                frmSource fm = new frmSource();

                // Store Masterid in list for Page Up and Page Down

                List<int> Masteridlist = new List<int>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgSample.Rows)
                {
                    string val1 = dgSample["MASTERID", cnt].Value.ToString();
                    int val = Int32.Parse(val1);
                    Masteridlist.Add(val);
                    cnt = cnt + 1;
                }

                fm.Masterid = Int32.Parse(mid);
                fm.Masteridlist = Masteridlist;
                fm.CurrIndex = index;
                fm.CallingForm = this;

          //      GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fm.ShowDialog();  // show child 

           //     GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
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
            int index = dgSample.CurrentRow.Index;
            string seldate = dgSample["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgSample["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }

            DataGridViewSelectedRowCollection rows = dgSample.SelectedRows;

            this.Hide();        // hide parent

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                string resp = dgSample["RESPID", index].Value.ToString();
                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

           //     GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {
                string val1 = dgSample["ID", index].Value.ToString();

                // Store Id in list for Page Up and Page Down

                List<string> Idlist = new List<string>();

                int xcnt = 0;
                frmC700 fC700 = new frmC700();
                foreach (DataGridViewRow dr in dgSample.Rows)
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

          //      GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fC700.ShowDialog();  // show child
            }

          //  GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
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
            txtRespid.BackColor = (Color)txtRespid.Tag;
        }

        private void txtFin_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtFin_Leave(object sender, EventArgs e)
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

        private void txtProjselv_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtProjselv_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtProjselv1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtProjselv1_Leave(object sender, EventArgs e)
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

        private void txtPsuPlace_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtPsuPlace_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtPsu_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtPsu_Leave(object sender, EventArgs e)
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

        private void dgSample_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgSample.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmSample_FormClosing(object sender, FormClosingEventArgs e)
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

        private void txtNewtc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtProjselv_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtProjselv1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
                e.Handled = false;
            else
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

        private void txtPsu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPsuPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbFipstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbSource_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbOwner_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbPsuPlace_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbPsu_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
