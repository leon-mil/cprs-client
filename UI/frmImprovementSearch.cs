/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmImprovementSearch.cs
Programmer    : Srini Natarajan
Creation Date : August 12, 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Search ceSample and ceJobs tables for Improvements
Change Request: 
Specification : CPRS II - Improvements Search Screen   
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
    public partial class frmImprovementSearch : Cprs.frmCprsParent
    {
        #region Member Variables

        StringFormat strFormat; //Used to format the grid rows.
        ArrayList arrColumnLefts = new ArrayList();//Used to save left coordinates of columns
        ArrayList arrColumnWidths = new ArrayList();//Used to save column widths

        #endregion

        public static frmImprovementSearch Current;
        private ImprovSearchData dataObject;
        public string selIdtxt = ""; //{ get; set; }
        //{
        //    get { return this.selIdtxt; }
        //}
        string DefSurvDate = "";

        public frmImprovementSearch()
        {
            InitializeComponent();
            PopulateJCodeCombo();
            fillYrBuiltCombo();

        }
        
        private void frmImprovementSearch_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            ResetParameters();

            SetOperatorCombo();
            PopulateFipStateCombo();

            SetHiddenLbl();

            dataObject = new ImprovSearchData();
            dgImprovntSrch.DataSource = dataObject.GetEmptyTable();

            dgImprovntSrch.AutoResizeColumns();

            string DefSurvDate1;
            DefSurvDate1 = ImprovSearchData.getDefSurveyDate(DefSurvDate);
            lblDefSurvDateVal.Text = DefSurvDate1;
         
            lblRecordCount.Text = " ";

            this.KeyPreview = true;

            txtId.KeyDown += new KeyEventHandler(txtId_KeyDown);
            txtSurvDate1.KeyDown += new KeyEventHandler(txtSurvDate1_KeyDown);
            txtSurvDate2.KeyDown += new KeyEventHandler(txtSurvDate2_KeyDown);
            txtTCost1.KeyDown += new KeyEventHandler(txtTCost1_KeyDown);
            txtTCost2.KeyDown += new KeyEventHandler(txtTCost2_KeyDown);
            txtPropVal1.KeyDown += new KeyEventHandler(txtPropVal1_KeyDown);
            txtPropVal2.KeyDown += new KeyEventHandler(txtPropVal2_KeyDown);
            txtIncome1.KeyDown += new KeyEventHandler(txtIncome1_KeyDown);
            txtIncome2.KeyDown += new KeyEventHandler(txtIncome2_KeyDown);
            txtId.Focus();
        }

        private void PopulateFipStateCombo()
        {
            cbSelState.DataSource = GeneralDataFuctions.GetFipStateDataForCombo();
            cbSelState.ValueMember = "FIPSTATE";
            cbSelState.DisplayMember = "STATE1";
            cbSelState.SelectedIndex = -1;
        }

        private void PopulateJCodeCombo()
        {
            ImprovSearchData gdf = new ImprovSearchData();
            cbJCodeValue.DataSource = gdf.GetJCodeDataForCombo();
            cbJCodeValue.ValueMember = "JCODE";
            cbJCodeValue.DisplayMember = "JCODE";
            cbJCodeValue.SelectedIndex = -1;
        }

        private void fillYrBuiltCombo()
        {
            ImprovSearchData gdf = new ImprovSearchData();
            cbYrBuilt1.DataSource = gdf.GetYrBuiltForCombo();
            cbYrBuilt1.ValueMember = "YearBuiltCode";
            cbYrBuilt1.DisplayMember = "YearRangeCon";
            cbYrBuilt1.SelectedIndex = -1;
            cbYrBuilt2.DataSource = gdf.GetYrBuiltForCombo();
            cbYrBuilt2.ValueMember = "YearBuiltCode";
            cbYrBuilt2.DisplayMember = "YearRangeCon";
            cbYrBuilt2.SelectedIndex = -1;
        }

        private void SetOperatorCombo()
        {
            cbId.SelectedIndex = 0;
            cbState.SelectedIndex = 0;
            cbJCode.SelectedIndex = 0;
            cbSurvDate.SelectedIndex = 0;
            cbYrBuilt.SelectedIndex = 0;
            cbTCost.SelectedIndex = 0;
            cbAppliances.SelectedIndex = 0;
            cbPropVal.SelectedIndex = 0;
            cbIncome.SelectedIndex = 0;
            cbJobs.SelectedIndex = 0;
        }

        private void ResetParameters()
        {
            txtId.Text = "";
            cbSelState.SelectedIndex = -1;
            cbJCodeValue.Text = "";
            txtSurvDate1.Text = "";
            txtSurvDate2.Text = "";
            cbYrBuilt1.Text = "";
            cbYrBuilt2.Text = "";

            cbYrBuilt1.SelectedIndex = -1;
            cbYrBuilt2.SelectedIndex = -1;

            txtTCost1.Text = "";
            txtTCost2.Text = "";
            cbApplianceCode.SelectedIndex = -1;
            txtPropVal1.Text = "";
            txtPropVal2.Text = "";
            txtIncome1.Text = "";
            txtIncome2.Text = "";
            cbJobsSel.Text = "";
            //txtDefSurvDate.Text = "";
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            SetOperatorCombo();
            ResetParameters();

            dgImprovntSrch.DataSource = dataObject.GetEmptyTable();
            lblRecordCount.Text = " ";
            txtId.Focus();
        }

        private void SetHiddenLbl()
        {
            lblSurvDateTo.Visible = false;
            txtSurvDate2.Visible = false;

            lblYrBuiltTo.Visible = false;
            cbYrBuilt2.Visible = false;

            lblTcostTo.Visible = false;
            txtTCost2.Visible = false;

            lblPropValTo.Visible = false;
            txtPropVal2.Visible = false;

            lblIncomeTo.Visible = false;
            txtIncome2.Visible = false;
        }

        private void txtId_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 7))
                Search();
        }

        private void txtSurvDate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
                Search();
        }

        private void txtSurvDate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
                Search();
        }

        private void txtTCost1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtTCost2_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtPropVal1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtPropVal2_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtIncome1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtIncome2_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {   
            // Verify if any parameters entered
           if (txtId.Text == "" && cbSelState.Text == "" &&
                cbJCodeValue.Text == "" && txtSurvDate1.Text == "" &&
                txtSurvDate2.Text == "" && cbYrBuilt1.Text == "" &&
                cbYrBuilt2.Text == "" && txtTCost1.Text == "" && txtTCost2.Text == "" &&
                cbApplianceCode.Text == "" && txtPropVal1.Text == "" && txtPropVal2.Text == "" &&
                txtIncome1.Text == "" && txtIncome2.Text == "")
            {
                //MessageBox.Show("Please Enter Search Criteria.");
                //return;
            }

            //Check if other parameters with Id
            if (txtId.Text != "" &&
                (cbSelState.Text != "" ||
                 cbJCodeValue.Text != "" || txtSurvDate1.Text != "" || txtSurvDate2.Text != "" ||
                 cbYrBuilt1.Text != "" || cbYrBuilt2.Text != "" || txtTCost1.Text != "" || txtTCost2.Text != "" ||
                 cbApplianceCode.Text != "" || txtPropVal1.Text != "" || txtPropVal2.Text != "" || txtIncome1.Text != "" ||
                 txtIncome2.Text != "" || cbJobsSel.Text != ""))
            {
                MessageBox.Show("Other Search Criteria should not be included with Id Search.", "Entry Error");

                ResetParameters();
                SetHiddenLbl();
                SetOperatorCombo();
                txtId.Focus();
                return;
            }

            if (txtId.Text != "" &&
                    (cbSelState.Text == "" ||
                    cbJCodeValue.Text == "" || txtSurvDate1.Text == "" || txtSurvDate2.Text == "" ||
                    cbYrBuilt1.Text == "" || cbYrBuilt2.Text == "" || txtTCost1.Text == "" || txtTCost2.Text == "" ||
                    cbApplianceCode.Text == "" || txtPropVal1.Text == "" || txtPropVal2.Text == "" || txtIncome1.Text == "" ||
                    txtIncome2.Text == ""))
            {
               
                {
                    CesampleData cs = new CesampleData();
                    cs.CheckIdExist(txtId.Text);

                    //If the id is valid update the Improvements search screen to show relevent data
                    if ( cs.CheckIdExist(txtId.Text) != false)
                    {
                        //The entered ID is a valid number
                    }
                    else
                    {
                        MessageBox.Show("The ID entered is not valid!");
                        ResetParameters();
                        SetHiddenLbl();
                        SetOperatorCombo();
                        txtId.Focus();
                        this.DialogResult = DialogResult.None;
                        txtId.Focus();
                        return;
                    }
                }

            }

                    if (VerifyBetween())
                    {
                        this.Cursor = Cursors.WaitCursor;
                        GetImprovmntSrchData();
                        this.Cursor = Cursors.Default;
                    }
        }

        private Boolean VerifyBetween()
        {
            Boolean result = false;

            if (cbSurvDate.Text == "Between")
            {
                if (!GeneralFunctions.VerifyBetweenParameters(txtSurvDate1.Text, txtSurvDate2.Text, "SurvDate"))
                    return result;
            }
            if (cbYrBuilt.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(cbYrBuilt1.SelectedValue.ToString(), cbYrBuilt2.SelectedValue.ToString(), "Yrbuilt"))
                    return result;
            if (cbTCost.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtTCost1.Text, txtTCost2.Text, "Tcost"))
                    return result;
            if (cbPropVal.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtPropVal1.Text, txtPropVal2.Text, "Propval"))
                    return result;
            if (cbIncome.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtIncome1.Text, txtIncome2.Text, "Income"))
                    return result;
            return result = true;
        }

        private void GetImprovmntSrchData()
        {
            dgImprovntSrch.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgImprovntSrch.RowHeadersVisible = false; // set it to false if not needed

            DataTable dt = new DataTable();

            dgImprovntSrch.DataSource = dt;

            dt = GetDataTable1();

            if (txtId.Text != "" && dt.Rows.Count == 0)       
            {
                MessageBox.Show("The ID entered has no expenditure Data");
                txtId.Focus();
            }
            else if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");
                txtId.Focus();
            }
            

            dgImprovntSrch.DataSource = dt;

            for (int i = 0; i < dgImprovntSrch.ColumnCount; i++)
            {
                if (i == 2)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "INTERVIEW";
                }

                if (i == 7)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "WEIGHT";
                    dgImprovntSrch.Columns[i].Width = 90;
                }

                if (i == 8)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "TCOST";
                    dgImprovntSrch.Columns[i].Width = 90;
                }

                if (i == 9)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "WEIGHTED TCOST";
                    dgImprovntSrch.Columns[i].Width = 110;
                }

                if (i == 10)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "JOBS";
                    dgImprovntSrch.Columns[i].Width = 75;
                }
                if (i == 11)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "JOBCODE";
                    dgImprovntSrch.Columns[i].Width = 95;
                }
                if (i == 12)
                {
                    dgImprovntSrch.Columns[i].HeaderText = "EDITED";
                    dgImprovntSrch.Columns[i].Width = 80;
                }

                if (i == 0 || i == 4 || i == 6)
                {
                    dgImprovntSrch.Columns[i].Width = 75;
                }
                if (i == 1)
                {
                    dgImprovntSrch.Columns[i].Width = 100;
                }
                if (i == 2)
                {
                    dgImprovntSrch.Columns[i].Width = 110;
                }
                if (i == 3 || i == 5)
                {
                    dgImprovntSrch.Columns[i].Width = 90;
                }
                
                if (i == 0 || i == 1 || i == 2 || i == 4 || i == 6 || i == 10 || i == 11 || i == 12 )
                {
                    dgImprovntSrch.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                
                if (i == 3 || i == 5 || i == 7 || i == 8 || i == 9)
                {
                    dgImprovntSrch.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }

            if (dt.Rows.Count == 1)
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASE FOUND";
            else
                lblRecordCount.Text = dt.Rows.Count.ToString() + " CASES FOUND";
        }

        private DataTable GetDataTable1()
        {
            string fst = string.Empty;
            string cb1 = string.Empty;
            string cb2 = string.Empty;

            if (cbSelState.Text != "")
                fst = cbSelState.SelectedValue.ToString();

            if (cbYrBuilt1.Text != "")
                cb1 = cbYrBuilt1.SelectedValue.ToString();

            if (cbYrBuilt2.Text != "")
                cb2 = cbYrBuilt1.SelectedValue.ToString(); 

            DataTable dt = dataObject.GetImprovSearchData(txtId.Text, cbSelState.Text, cbJCodeValue.Text, txtSurvDate1.Text, txtSurvDate2.Text, cbSurvDate.Text, cb1, cb2, cbYrBuilt.Text, txtTCost1.Text, txtTCost2.Text,
                    cbTCost.Text, cbApplianceCode.Text, cbJobsSel.Text, txtPropVal1.Text, txtPropVal2.Text, cbPropVal.Text, txtIncome1.Text, txtIncome2.Text, cbIncome.Text);
            return dt;
        }

        private void cbSurvDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSurvDate.SelectedItem != null && cbSurvDate.SelectedItem.ToString() == "Between")
            {
                lblSurvDateTo.Visible = true;
                txtSurvDate2.Visible = true;
            }
            else
            {
                lblSurvDateTo.Visible = false;
                txtSurvDate2.Visible = false;
            }
        }

        private void cbYrBuilt_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbYrBuilt.SelectedItem != null && cbYrBuilt.SelectedItem.ToString() == "Between")
            {
                lblYrBuiltTo.Visible = true;
                cbYrBuilt2.Visible = true;
            }
            else
            {
                lblYrBuiltTo.Visible = false;
                cbYrBuilt2.Visible = false;
            }
        }

        private void cbTCost_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTCost.SelectedItem != null && cbTCost.SelectedItem.ToString() == "Between")
            {
                lblTcostTo.Visible = true;
                txtTCost2.Visible = true;
            }
            else
            {
                lblTcostTo.Visible = false;
                txtTCost2.Visible = false;
            }
        }

        private void cbPropVal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbPropVal.SelectedItem != null && cbPropVal.SelectedItem.ToString() == "Between")
            {
                lblPropValTo.Visible = true;
                txtPropVal2.Visible = true;
            }
            else
            {
                lblPropValTo.Visible = false;
                txtPropVal2.Visible = false;
            }
        }

        private void cbIncome_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbIncome.SelectedItem != null && cbIncome.SelectedItem.ToString() == "Between")
            {
                lblIncomeTo.Visible = true;
                txtIncome2.Visible = true;
            }
            else
            {
                lblIncomeTo.Visible = false;
                txtIncome2.Visible = false;
            }
        }

        //Allow only numbers to be entered
        private void txtPropVal1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Allow only numbers to be entered
        private void txtPropVal2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void txtIncome1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Allow only numbers to be entered
        private void txtIncome2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Allow only numbers to be entered
        private void txtTCost2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        //Allow only numbers to be entered
        private void txtTCost1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

        }

        //Allow only numbers to be entered
        private void txtSurvDate1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Allow only numbers to be entered
        private void txtSurvDate2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //Allow only numbers to be entered
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        //remove the coloring of text box when leaving
        private void txtId_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtSurvDate1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtSurvDate2_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtTCost1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtTCost2_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtPropVal1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtPropVal2_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtIncome1_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtIncome2_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        //Color the text box yellow when entering
        private void txtId_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtSurvDate1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtSurvDate2_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtPropVal1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtPropVal2_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtIncome1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtIncome2_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        //trim the selection to the first 2 characters of the drop down list.
        private void cbYrBuilt1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbYrBuilt1.SelectedIndex > 0)
                {
                    string value = cbYrBuilt1.Text.Substring(0, 2);
                    this.BeginInvoke((MethodInvoker)delegate { this.cbYrBuilt1.Text = value; });
                }
        }

        //trim the selection to the first 2 characters of the drop down list.
        private void cbYrBuilt2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbYrBuilt2.SelectedIndex > 0)
                {
                    string value = cbYrBuilt2.Text.Substring(0, 2);
                    this.BeginInvoke((MethodInvoker)delegate { this.cbYrBuilt2.Text = value; });
                }
        }

        private void txtTCost1_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtTCost2_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtTCost1_Leave_1(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void txtTCost2_Leave_1(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void btnPrintImprovSrch_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to print.");
            }
            else
            {
                strFormat = new StringFormat();
                strFormat.Alignment = StringAlignment.Near;
                strFormat.LineAlignment = StringAlignment.Center;
                strFormat.Trimming = StringTrimming.EllipsisCharacter;

                arrColumnLefts.Clear();
                arrColumnWidths.Clear();

                if (dgImprovntSrch.RowCount >= 150)
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

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "IMPROVEMENTS SEARCH";

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitle = BuildSearchCriteria();
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

           /*Shrink the width of some columns to fit all columns in one page */ 
            DataGridViewColumn column1 = dgImprovntSrch.Columns[1];
            column1.Width = 80;
            DataGridViewColumn column2 = dgImprovntSrch.Columns[2];
            column2.Width = 80;
            DataGridViewColumn column3 = dgImprovntSrch.Columns[3];
            column3.Width = 80;
            DataGridViewColumn column4 = dgImprovntSrch.Columns[4];
            column4.Width = 60;
            DataGridViewColumn column5 = dgImprovntSrch.Columns[5];
            column5.Width = 80;
            DataGridViewColumn column6 = dgImprovntSrch.Columns[6];
            column6.Width = 50;
            DataGridViewColumn column7 = dgImprovntSrch.Columns[7];
            column7.Width = 80;
            DataGridViewColumn column8 = dgImprovntSrch.Columns[8];
            column8.Width = 70;
            DataGridViewColumn column9 = dgImprovntSrch.Columns[9];
            column9.Width = 100;
            DataGridViewColumn column10 = dgImprovntSrch.Columns[10];
            column10.Width = 50;
            DataGridViewColumn column11 = dgImprovntSrch.Columns[11];
            column11.Width = 80;
            DataGridViewColumn column12 = dgImprovntSrch.Columns[12];
            column12.Width = 60;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Improvements Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgImprovntSrch);

            dgImprovntSrch.Columns[1].Width = 100;
            dgImprovntSrch.Columns[2].Width = 110;
            dgImprovntSrch.Columns[3].Width = 90;
            dgImprovntSrch.Columns[4].Width = 75;
            dgImprovntSrch.Columns[5].Width = 90;
            dgImprovntSrch.Columns[6].Width = 75;
            dgImprovntSrch.Columns[7].Width = 100;
            dgImprovntSrch.Columns[8].Width = 100;
            dgImprovntSrch.Columns[9].Width = 100;
            dgImprovntSrch.Columns[10].Width = 80;
            dgImprovntSrch.Columns[11].Width = 90;
            dgImprovntSrch.Columns[12].Width = 80;

            Cursor.Current = Cursors.Default;

        }

        private string BuildSearchCriteria()
        {
            string criteria = "Search Criteria: ";

            if (txtId.Text != "")
                criteria += " Id = " + txtId.Text;
            
            if (cbSelState.Text != "")
                criteria += " StateCode = " + cbSelState.Text.Substring(0, 2);
            if (cbJCodeValue .Text.Trim() != "")
                criteria += " JCode = " + cbJCodeValue.Text;
            if (txtSurvDate1.Text != "")
            {
                if ((txtSurvDate1.Text == "Between") && (txtSurvDate2.Text != ""))
                    criteria += " Survey Date " + GeneralFunctions.ConvertOperatorToSymbol(txtSurvDate1.Text) + " (" + txtSurvDate1.Text + " - " + txtSurvDate2.Text + ")";
                else
                    criteria += " Survey Date " + GeneralFunctions.ConvertOperatorToSymbol(txtSurvDate1.Text) + " " + txtSurvDate1.Text;
            }
            if (cbYrBuilt1.Text != "")
            {
                if ((cbYrBuilt1.Text == "Between") && (cbYrBuilt2.Text != ""))
                    criteria += " Year Built " + GeneralFunctions.ConvertOperatorToSymbol(cbYrBuilt1.Text) + " (" + cbYrBuilt1.Text + " - " + cbYrBuilt2.Text + ")";
                else
                    criteria += " Year Built " + GeneralFunctions.ConvertOperatorToSymbol(cbYrBuilt1.Text) + " " + cbYrBuilt1.Text;
            }
            if (cbApplianceCode.Text != "")
                criteria += " Appliance = " + cbApplianceCode.Text;
            if (cbJobsSel.Text != "")
                criteria += " Jobs = " + cbJobsSel.Text;
            if (txtTCost1.Text != "")
            {
                if ((txtTCost1.Text == "Between") && (txtTCost2.Text != ""))
                    criteria += " Total Cost " + GeneralFunctions.ConvertOperatorToSymbol(txtTCost1.Text) + " (" + txtTCost1.Text + " - " + txtTCost2.Text + ")";
                else
                    criteria += " Total Cost " + GeneralFunctions.ConvertOperatorToSymbol(txtTCost1.Text) + " " + txtTCost1.Text;
            }
            if (txtPropVal1.Text != "")
            {
                if ((txtPropVal1.Text == "Between") && (txtPropVal2.Text != ""))
                    criteria += " Prop Value " + GeneralFunctions.ConvertOperatorToSymbol(txtPropVal1.Text) + " (" + txtPropVal1.Text + " - " + txtPropVal2.Text + ")";
                else
                    criteria += " Prop Value " + GeneralFunctions.ConvertOperatorToSymbol(txtPropVal1.Text) + " " + txtPropVal1.Text;
            }
            if (txtIncome1.Text != "")
            {
                if ((txtIncome1.Text == "Between") && (txtIncome2.Text != ""))
                    criteria += " Income " + GeneralFunctions.ConvertOperatorToSymbol(txtIncome1.Text) + " (" + txtIncome1.Text + " - " + txtIncome2.Text + ")";
                else
                    criteria += " Income " + GeneralFunctions.ConvertOperatorToSymbol(txtIncome1.Text) + " " + txtIncome1.Text;
            }

            return criteria;
        }

        private void btnDataImprov_Click(object sender, EventArgs e)
        {
            if (lblRecordCount.Text == " " || lblRecordCount.Text == "0 CASES FOUND" || lblRecordCount.Text == null)
            {
                MessageBox.Show("The Search Results list is empty. No Record Selected.");
                txtId.Focus();
            }
            else
            {
                this.Hide();   // hide parent

                frmImprovements fm = new frmImprovements();

                DataGridViewSelectedRowCollection rows = dgImprovntSrch.SelectedRows;

                int index = dgImprovntSrch.CurrentRow.Index;

                string val1 = dgImprovntSrch["ID", index].Value.ToString();
                fm.Id = val1;

                List<string> Idlist = new List<string>();

                int cnt = 0;

                foreach (DataGridViewRow dr in dgImprovntSrch.Rows)
                {
                    string val = dgImprovntSrch["ID", cnt].Value.ToString();
                    Idlist.Add(val);
                    cnt = cnt + 1;
                }

                fm.Idlist = Idlist;
                fm.CurrIndex = index;

                fm.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");

                fm.ShowDialog();  // show child

                GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");

                //reload data
                Search();

            }
        }

        private void frmImprovementSearch_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

    }
}
