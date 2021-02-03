/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmMaster.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Search master table
Change Request: 
Specification : Master Search Specifications  
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
    public partial class frmMaster : Cprs.frmCprsParent
    {  
        private MasterSearchData dataObject;
        private string curr_survey_month;

        public frmMaster()
        {
            InitializeComponent();
        }

        private void frmMaster_Load(object sender, EventArgs e)
        {
            ResetParameters();
            
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            PopulateFipStateCombo();

            SetOperatorCombo();

            SetHiddenLbl();
            SetButtonTxt();

            dataObject = new MasterSearchData();
            dgMaster.DataSource = dataObject.GetEmptyTable();
            dgMaster.RowHeadersVisible = true;

            dgMaster.AutoResizeColumns();

            lblRecordCount.Text = " ";

            //Add key down event to All textboxes

            this.KeyPreview = true;

            txtId.KeyDown += new KeyEventHandler(txtId_KeyDown);

            txtFin.KeyDown += new KeyEventHandler(txtFin_KeyDown);

            txtSeldate.KeyDown += new KeyEventHandler(txtSeldate_KeyDown);
            txtSeldate1.KeyDown += new KeyEventHandler(txtSeldate1_KeyDown);

            txtNewtc.KeyDown += new KeyEventHandler(txtNewtc_KeyDown);
            txtNewtc1.KeyDown += new KeyEventHandler(txtNewtc1_KeyDown);

            txtProjselv.KeyDown += new KeyEventHandler(txtProjselv_KeyDown);
            txtProjselv1.KeyDown += new KeyEventHandler(txtProjselv1_KeyDown);

            txtTvalue.KeyDown += new KeyEventHandler(txtTvalue_KeyDown);
            txtTvalue1.KeyDown += new KeyEventHandler(txtTvalue1_KeyDown);

            txtRunits.KeyDown += new KeyEventHandler(txtRunits_KeyDown);
            txtRunits1.KeyDown += new KeyEventHandler(txtRunits1_KeyDown);
            txtFin.Focus();

            curr_survey_month = GeneralFunctions.CurrentYearMon();
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

        private void cbTvalue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTvalue.SelectedItem != null && cbTvalue.SelectedItem.ToString() == "Between")
            {
                lbTvalueto.Visible = true;
                txtTvalue1.Visible = true;
            }
            else
            {
                lbTvalueto.Visible = false;
                txtTvalue1.Visible = false;
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

        private void ResetParameters()
        {
            txtFin.Text = "";
            txtId.Text = "";
            cbFipStateSel.SelectedIndex = -1;
            txtOwner.Text = "";
            txtSeldate.Text = "";
            txtSeldate1.Text = "";
            txtNewtc.Text = "";
            txtNewtc1.Text = "";
            txtProjselv.Text = "";
            txtProjselv1.Text = "";

            txtTvalue.Text = "";
            txtTvalue1.Text = "";
            txtRunits.Text = "";
            txtRunits1.Text = "";
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

            lbTvalueto.Visible = false;
            txtTvalue1.Visible = false;

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


        private void SetOperatorCombo()
        {
            cbDodgenum.SelectedIndex = 0;
            cbId.SelectedIndex = 0;
            cbFipstate.SelectedIndex = 0;
            cbSeldate.SelectedIndex = 0;
            cbNewtc.SelectedIndex = 0;
            cbOwner.SelectedIndex = 0;
            cbProjselv.SelectedIndex = 0;
            cbRunits.SelectedIndex = 0;
            cbTvalue.SelectedIndex = 0;
        }

        private void PopulateFipStateCombo()
        {
            cbFipStateSel.DataSource = GeneralDataFuctions.GetFipStateDataForCombo();
            cbFipStateSel.ValueMember = "FIPSTATE";
            cbFipStateSel.DisplayMember = "STATE1";
           
            cbFipStateSel.SelectedIndex = -1;
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
            if (cbTvalue.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtTvalue.Text, txtTvalue1.Text, "Tvalue"))
                    return result;
            if (cbRunits.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtRunits.Text, txtRunits1.Text, "Runits"))
                    return result;

            return result = true;

        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
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

        private void txtTvalue_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }
        private void txtTvalue1_KeyDown(object sender, KeyEventArgs e)
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

        private void txtId_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "ID");
        }

        private void txtFin_TextChanged(object sender, EventArgs e)
        {
           // GeneralFunctions.CheckIntegerField(sender, "FIN");
        }

        private void txtNewtc_TextChanged(object sender, EventArgs e)
        {
            if (txtNewtc.TextLength == 4)
            {
                if (cbNewtc.Text == "Equals")
                    txtRunits.Focus();
                else if (txtNewtc1.Visible)
                    txtNewtc1.Focus();
            }          
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
                if (txtSeldate1.Visible)
                    txtSeldate1.Focus();
                else
                    txtTvalue.Focus();
            }
        }
        private void txtSeldate1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE1");
            if (txtSeldate1.Text.Length == 6)
                txtSeldate1.Focus();
        }

        private void txtTvalue_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "TVALUE");
        }
        private void txtTvalue1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "TVALUE1");
        }

        private void txtRunits_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RUNITS");
        }
        private void txtRunits1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "RUNITS1");
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

        private void Search()
        {
            // Verify if any parameters entered

            if (txtFin.Text == "" && txtId.Text == "" &&
                cbFipStateSel.Text.Trim() == "" && txtOwner.Text == "" &&
                txtSeldate.Text == "" && txtNewtc.Text == "" &&
                txtProjselv.Text == "" && txtTvalue.Text == "" && txtRunits.Text == "")
            {
                MessageBox.Show("Please Enter Search Criteria.");
                txtFin.Focus();
                return;
            }

            //Check if other parameters with Id

            if (txtId.Text != "" &&
                (txtFin.Text != "" || 
                 cbFipStateSel.Text.Trim() != "" ||  txtOwner.Text != "" || txtSeldate.Text != "" ||
                 txtNewtc.Text != "" || txtProjselv.Text != "" || txtTvalue.Text != "" || txtRunits.Text != ""))
             {
                 MessageBox.Show("Other Search Criteria should not be included with Id Search.", "Entry Error");

                 ResetParameters();
                 SetHiddenLbl();
                 SetOperatorCombo();
                 txtFin.Focus();
                 return;
             }

            //Check if other parameters with Fin

             if (txtFin.Text != "" && cbDodgenum.SelectedItem.ToString() == "Equals" &&
                 (txtId.Text != "" || 
                 cbFipStateSel.Text.Trim() != "" || txtOwner.Text != "" || txtSeldate.Text != "" ||
                 txtNewtc.Text != "" || txtProjselv.Text != "" || txtTvalue.Text != "" || txtRunits.Text != ""))
             {
                 MessageBox.Show("Other Search Criteria should not be included with Fin Search.", "Entry Error");

                 ResetParameters();
                 SetHiddenLbl();
                 SetOperatorCombo();
                 txtFin.Focus();
                 return;
             }

            if (txtFin.Text != "" && cbDodgenum.Text == "Equals")
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
                GetMasterData();
                this.Cursor = Cursors.Default;
            }   
        }

        private DataTable GetDataTable1()
        {
            string fst = string.Empty;

            if (cbFipStateSel.Text.Trim() != "")
              fst = cbFipStateSel.SelectedValue.ToString();

            DataTable dt = dataObject.GetMasterSearchData(txtFin.Text, cbDodgenum.Text, txtId.Text, fst, txtOwner.Text.Trim(), txtSeldate.Text, cbSeldate.Text, txtSeldate1.Text,
                                                 txtNewtc.Text, cbNewtc.Text, txtNewtc1.Text, txtProjselv.Text, cbProjselv.Text, txtProjselv1.Text, txtTvalue.Text, cbTvalue.Text, txtTvalue1.Text, txtRunits.Text, cbRunits.Text, txtRunits1.Text);
           
            return dt;
        }

        private void GetMasterData()
        {
            dgMaster.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgMaster.RowHeadersVisible = false; // set it to false if not needed

            DataTable dt = new DataTable();

            dgMaster.DataSource = dt;

            dt = GetDataTable1();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");
                txtFin.Focus();
            }

            dgMaster.DataSource = dt;

            for (int i = 0; i < dgMaster.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgMaster.Columns[i].Width = 120;
                }
                if (i == 1)
                {
                    dgMaster.Columns[i].Visible = false;
                }


                //ID
                if (i == 2 )
                {
                    dgMaster.Columns[i].Width = 90;
                    dgMaster.Columns[i].Frozen = true;
                }

                //Survey(4) and Newtc(6) are small
                if (i == 4 || i == 6)
                {
                    dgMaster.Columns[i].Width = 85;
                }

                //FIN(0),Fipstate(4),Projselv(6) and Structcd(9) are wider
                if (i == 5 || i == 7 || i == 9)
                {
                    dgMaster.Columns[i].Width = 105;
                }

                //Tvalue(7) is smallest
                if (i == 8)
                {
                    dgMaster.Columns[i].Width = 85;
                }

                //Rbldgs(10) and Runits(11) are small
                if (i == 10 || i == 11 )
                {
                    dgMaster.Columns[i].Width = 85;
                }


                //ID(2), Seldate(3), Survey(4), Fipstate(5), Newtc(6), are Centered
                if (i == 2 || i == 3 || i == 4 || i == 5 || i == 6 || i == 9 )
                {
                    dgMaster.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                //Projselv(11),Tvalue(12), Rbldgs(11), Runits(12) are displayed right adjusted and comma format
                if (i == 7 || i == 8 || i == 10 || i == 11)
                {
                    dgMaster.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgMaster.Columns[i].DefaultCellStyle.Format = "N0";
                }            
            }

            dgMaster.RowHeadersVisible = true; // set it to false if not needed

            if (dt.Rows.Count == 1)
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASE FOUND";
            else
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASES FOUND";

        }


        private void btnReset_Click(object sender, EventArgs e)
        {
            SetOperatorCombo();

            ResetParameters();
            SetHiddenLbl();

            dgMaster.DataSource = dataObject.GetEmptyTable();

            lblRecordCount.Text = " ";
            txtFin.Focus();
        }

        private void btnOwner_Click(object sender, EventArgs e)
        {
            //Display frmSurveySel as a modal dialog form
            
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

        private void btnName_Click(object sender, EventArgs e)
        {

            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. No Record Selected.");
                return;
            }

            DataGridViewSelectedRowCollection rows = dgMaster.SelectedRows;

            if (Convert.IsDBNull(rows[0].Cells["Id"].Value))
            {
                MessageBox.Show("The Selected case is not a Sample Case.");
                return;
            }

            int index = dgMaster.CurrentRow.Index;
            string seldate = dgMaster["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgMaster["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }
           
            this.Hide();   // hide parent
            string val1 = dgMaster["Id", index].Value.ToString();

            frmName fName = new frmName();
                   

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();

            int xcnt = 0;

            foreach (DataGridViewRow dr in dgMaster.Rows)
            {
                string val = dr.Cells["Id"].Value.ToString();

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

            fName.ShowDialog();  // show child         

          //  GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
        
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

                DataGridViewSelectedRowCollection rows = dgMaster.SelectedRows;

                int index = dgMaster.CurrentRow.Index;

                string mid = dgMaster["MASTERID", index].Value.ToString();

                frmSource fm = new frmSource();

                //Store Masterid in list for Page Up and Page Down

                List<int> Masteridlist = new List<int>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgMaster.Rows)
                {
                    string val1 = dgMaster["MASTERID", cnt].Value.ToString();
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
                MessageBox.Show("The Search Results lists is empty. No Record Selected.");
                return;
            }
            
            
            DataGridViewSelectedRowCollection rows = dgMaster.SelectedRows;

            if (Convert.IsDBNull(rows[0].Cells["ID"].Value))
            {
                MessageBox.Show("The Selected case is not a Sample Case.");
                return;
            }

            int index = dgMaster.CurrentRow.Index;
            string seldate = dgMaster["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgMaster["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }

            this.Hide();        // hide parent

            string val1 = dgMaster["Id", index].Value.ToString();

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                SampleData sdata = new SampleData();
                Sample ss = sdata.GetSampleData(val1);
                frmTfu tfu = new frmTfu();
                
                tfu.RespId = ss.Respid;
                tfu.CallingForm = this;

           //     GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {
                // Store Id in list for Page Up and Page Down

                List<string> Idlist = new List<string>();
                int xcnt = 0;

                int curr_index = 0;
                foreach (DataGridViewRow dr in dgMaster.Rows)
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
                                { curr_index = xcnt; }
                                xcnt = xcnt + 1;
                            }
                        }
                        else
                        {
                            Idlist.Add(val);
                            if (val == val1)
                            { curr_index = xcnt; }
                            xcnt = xcnt + 1;
                        }
                    }
                }

                frmC700 fC700 = new frmC700();
                fC700.CurrIndex = curr_index;
                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CallingForm = this;

         //       GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fC700.ShowDialog();   // show child
            }

        //    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
   
        }


        private string BuildSearchCriteria()
        {
            string criteria = "Search Criteria: ";

            if (txtFin.Text != "")
                criteria += " Fin = " + txtFin.Text;
            if (txtId.Text != "")
                criteria += " Id = " + txtId.Text;
            if (cbFipStateSel.Text != "")
                criteria += " Fipstate = " + cbFipStateSel.Text.Substring(0,2);
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
            if (txtTvalue.Text != "")
            {
                if ((cbTvalue.Text == "Between") && (txtTvalue1.Text != ""))
                    criteria += " Tvalue " + GeneralFunctions.ConvertOperatorToSymbol(cbTvalue.Text) + " (" + txtTvalue.Text + " - " + txtTvalue1.Text + ")";
                else
                    criteria += " Tvalue " + GeneralFunctions.ConvertOperatorToSymbol(cbTvalue.Text) + " " + txtTvalue.Text;
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

        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to print.");
            }
            else
            {
                if (dgMaster.RowCount >= 115)
                {
                    if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?","Printout Large",MessageBoxButtons.YesNo) == DialogResult.Yes)
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
            foreach (DataGridViewRow dr in dgMaster.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgMaster.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            SetHeaderCellValue();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "MASTER SEARCH";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            if (dgMaster.SortedColumn !=null)
                printer.SubTitle = BuildSearchCriteria() + "  Sorted By: " + dgMaster.SortedColumn.Name;
            else
                printer.SubTitle = BuildSearchCriteria();
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            /*Hide the columns */
            printer.HideColumns.Add("dodgenum");
            printer.HideColumns.Add("tvalue");
            printer.HideColumns.Add("stratid");
            printer.HideColumns.Add("stratid");
            printer.HideColumns.Add("rbldgs");
            printer.HideColumns.Add("runits");

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Master Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            
            printer.PrintDataGridViewWithoutDialog(dgMaster);

            Cursor.Current = Cursors.Default;

        }

        private void txtFin_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtFin_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtId_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtId_Leave(object sender, EventArgs e)
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

        private void txtTvalue_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtTvalue_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }


        private void txtTvalue1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtTvalue1_Leave(object sender, EventArgs e)
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

        private void dgMaster_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgMaster.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        private void frmMaster_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        private void txtFin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 32)
                e.Handled = false;
            else
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtTvalue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtTvalue1_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNewtc1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbId_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbFipstate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void cbOwner_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtNewtc1_TextChanged(object sender, EventArgs e)
        {
            if (txtNewtc1.Text.Length ==4)
            {
                txtRunits.Focus();
            }
        }
    }
}
