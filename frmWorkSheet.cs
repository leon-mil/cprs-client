/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmWorksheet.cs	    	
Programmer:         Christine Zhang
Creation Date:      Jan. 31 2017
Inputs:             TC, Survey, Editable, callingform                
Parameters:		                    
Outputs:		    
Description:	    Display worksheets for 2, 3, 4 digits
Detailed Design:    
Other:	            call from frmTotalvip.cs
 
Revision History:	
***********************************************************************************
Modified Date :  5/14/2019
Modified By   :  Christine Zhang
Keyword       :  
Change Request:  CR#3144
Description   :  change from survey month 5 to 7, display 5 months for revision
***********************************************************************************
Modified Date :  9/19/2019
Modified By   :  Christine Zhang
Keyword       :  
Change Request:  CR#3573
Description   :  add real month to groupbox text
***********************************************************************************/
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
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmWorkSheet : frmCprsParent 
    {
        public frmTotalVip CallingForm = null;
        public string Newtc;
        public string Survey;
        public bool Editable = true;

        private bool call_callingFrom = false;
        private TabLockData lock_data;
        private WorksheetData work_data;
        private string sdate;
        private string tc2;
        private DataTable datatable;
        private DataTable curr_table;
        private DataTable rev1_table;
        private DataTable rev2_table;
        private DataTable rev3_table;
        private DataTable rev4_table;
        private DataTable clonetable;

        private bool v0_changed;
        private bool v1_changed;
        private bool v2_changed;
        private bool v3_changed;
        private bool v4_changed;

        private decimal old_cm0;
        private decimal old_cm1;
        private decimal old_cm2;
        private decimal old_cm3;
        private decimal old_cm4;
        private decimal old_pm0;
        private decimal old_pm1;
        private decimal old_pm2;
        private decimal old_pm3;
        private string last_focused;

        private bool isApply;
       
        public frmWorkSheet()
        {
            InitializeComponent();
        }

        private void frmWorkSheet_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //update lock
            lock_data = new TabLockData();
            if (Editable)
            {
                tc2 = Newtc.Substring(0, 2);
                if (tc2 != "1T" && Convert.ToInt16(tc2) > 19)
                    tc2 = "1T";
                lock_data.UpdateTabLock(tc2, true);
                if (Newtc.Substring(0, 2) == "1T")
                    Editable = false;
            }

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();
            
            lblTitle1.Text = GeneralFunctions.GetSurveyText(Survey) + " " + sdate + " VIP Worksheet";
            
            //Get all data
            GetTopData();
            GetMainData();
            GetCaseData();

            //Get changed data
            GetCurData();
            GetRev1Data();
            GetRev2Data();
            GetRev3Data();
            GetRev4Data();

            //Initial comboboxes
            cbVip1.SelectedIndex = 0;
            cbVip2.SelectedIndex = 0;
            cbVip3.SelectedIndex = 0;
            cbVip4.SelectedIndex = 0;
            cbVip5.SelectedIndex = 0;

            //set up readonly label
            lblReadOnly.Visible = !Editable;

            //Initial flags
            v0_changed = false;
            v1_changed = false;
            v2_changed = false;
            v3_changed = false;
            v4_changed = false;
            isApply = false;

            //set up Save button
            btnSave.Enabled = Editable;

            //set up search item
            txtValueItem.Visible = false;

            //set up Radio Ajust, cost buttons
            btnCost.Visible = false;
            if (Survey == "M")
                btnCost.Visible = true;

            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            //get month name
            string premon = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string ppmon = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string p3mon = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string p4mon = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            groupBox1.Text = "Cur Prel - "+ sdate;
            groupBox2.Text = "1st Rev - " + premon;
            groupBox3.Text = "2nd Rev - " + ppmon;
            groupBox5.Text = "3rd Rev - " + p3mon;
            groupBox7.Text = "4th Rev - " + p4mon;
        }

        
        //Get Top data
        private void GetTopData()
        {
            DataTable table;

            string level = "1";
            if (Newtc.Length > 2)
                level = "2";

            work_data = new WorksheetData();

            table = work_data.GetVipTotalData(sdate, Survey, level, Newtc);
            dgTc.DataSource = null;
            dgTc.DataSource = table;

            //save old cm0, cm1, cm2, cm3, cm4, pm0-pm3
            DataRow dr = table.Rows[0];
            old_cm0 = Convert.ToDecimal(dr["cm0"]);
            old_cm1 = Convert.ToDecimal(dr["cm1"]);
            old_cm2 = Convert.ToDecimal(dr["cm2"]);
            old_cm3 = Convert.ToDecimal(dr["cm3"]);
            old_cm4 = Convert.ToDecimal(dr["cm4"]);
            old_pm0 = Convert.ToDecimal(dr["pm0"]);
            old_pm1 = Convert.ToDecimal(dr["pm1"]);
            old_pm2 = Convert.ToDecimal(dr["pm2"]);
            old_pm3 = Convert.ToDecimal(dr["pm3"]);

            setTopColumnHeader();
        }

        //set up top grid header  
        private void setTopColumnHeader()
        {
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

            dgTc.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
        
            dgTc.Columns[0].HeaderText = "Type of Construction";
            dgTc.Columns[0].Width = 160;
            dgTc.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgTc.Columns[0].Frozen = true;

            dgTc.Columns[1].DefaultCellStyle.Format = "N1";
            dgTc.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[1].HeaderText = curmon;;
            dgTc.Columns[1].Width = 80;

            dgTc.Columns[2].DefaultCellStyle.Format = "N1";
            dgTc.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[2].HeaderText = premon;
            dgTc.Columns[2].Width = 80;

            dgTc.Columns[3].DefaultCellStyle.Format = "N1";
            dgTc.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[3].HeaderText = ppmon;
            dgTc.Columns[3].Width = 80;

            dgTc.Columns[4].DefaultCellStyle.Format = "N1";
            dgTc.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[4].HeaderText = "Previous " + premon;
            dgTc.Columns[4].Width = 155;

            dgTc.Columns[5].DefaultCellStyle.Format = "N1";
            dgTc.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[5].HeaderText = "Previous " + ppmon;
            dgTc.Columns[5].Width = 155;

            dgTc.Columns[6].DefaultCellStyle.Format = "N2";
            dgTc.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[6].HeaderText =  curmons + "-" + premons + "/" + premons;

            dgTc.Columns[7].DefaultCellStyle.Format = "N2";
            dgTc.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[7].HeaderText = premons + "-" + ppmons + "/" + ppmons;

            dgTc.Columns[8].DefaultCellStyle.Format = "N2";
            dgTc.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[8].HeaderText = "1st Rev Change";
            dgTc.Columns[8].Width = 130;

            dgTc.Columns[9].DefaultCellStyle.Format = "N2";
            dgTc.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[9].HeaderText = "2nd Rev Change";
            dgTc.Columns[9].Width = 130;

            dgTc.Columns[10].HeaderText = p3mon;
            dgTc.Columns[10].DefaultCellStyle.Format = "N1";
            dgTc.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[11].HeaderText = p4mon;
            dgTc.Columns[11].DefaultCellStyle.Format = "N1";
            dgTc.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[12].HeaderText = "Previous " + p3mon;
            dgTc.Columns[12].DefaultCellStyle.Format = "N1";
            dgTc.Columns[12].Width = 150;
            dgTc.Columns[12].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[13].HeaderText = "Previous " + p4mon;
            dgTc.Columns[13].DefaultCellStyle.Format = "N1";
            dgTc.Columns[13].Width = 150;
            dgTc.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[14].HeaderText = ppmons + "-" + p3mons + "/" + p3mons;
            dgTc.Columns[14].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[14].DefaultCellStyle.Format = "N2";
            dgTc.Columns[15].HeaderText = p3mons + "-" + p4mons + "/" + p4mons;
            dgTc.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[15].DefaultCellStyle.Format = "N2";
            dgTc.Columns[16].HeaderText = "3rd Rev Change";
            dgTc.Columns[16].DefaultCellStyle.Format = "N2";
            dgTc.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[16].Width = 130;
            dgTc.Columns[17].HeaderText = "4th Rev Change";
            dgTc.Columns[17].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTc.Columns[17].Width = 130;
            dgTc.Columns[17].DefaultCellStyle.Format = "N2";
        }

        //get monthly vip calculation 
        private void GetMainData()
        {
            DataTable table = work_data.GetMainData(sdate, Survey, Newtc);
            dgTot.DataSource = null;
            dgTot.DataSource = table;

            setMainColumnHeader();
        }

        private void setMainColumnHeader()
        {
            dgTot.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgTot.Columns[0].Visible = false;

            dgTot.Columns[1].HeaderText = "Month           ";
            dgTot.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            dgTot.Columns[2].Width = 120;
            dgTot.Columns[2].DefaultCellStyle.Format = "N0";
            dgTot.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[2].HeaderText = "Unadjusted VIP";

            dgTot.Columns[3].Width = 120;
            dgTot.Columns[3].DefaultCellStyle.Format = "N0";
            dgTot.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[3].HeaderText = "* LSF";

            dgTot.Columns[4].Width = 120;
            dgTot.Columns[4].DefaultCellStyle.Format = "N0";
            dgTot.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[4].HeaderText = "* Undercoverage";

            dgTot.Columns[5].Width = 120;
            dgTot.Columns[5].DefaultCellStyle.Format = "N0";
            dgTot.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[5].HeaderText = "* Benchmark";

            dgTot.Columns[6].Width = 120;
            dgTot.Columns[6].DefaultCellStyle.Format = "N0";
            dgTot.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgTot.Columns[6].HeaderText = "* 12/Seasonal";

            dgTot.Columns[7].Width = 150;
            dgTot.Columns[7].HeaderText = "Rounded Adjusted";
            dgTot.Columns[7].DefaultCellStyle.Format = "N1";
            dgTot.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgTot.Columns[8].Width = 50;
            dgTot.Columns[8].HeaderText = "LSF";
            dgTot.Columns[8].DefaultCellStyle.Format = "N2";
            dgTot.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgTot.Columns[9].Width = 50;
            dgTot.Columns[9].HeaderText = "UCF";
            dgTot.Columns[9].DefaultCellStyle.Format = "N2";
            dgTot.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgTot.Columns[10].Width = 50;
            dgTot.Columns[10].HeaderText = "BST";
            dgTot.Columns[10].DefaultCellStyle.Format = "N3";
            dgTot.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            dgTot.Columns[11].Width = 50;
            dgTot.Columns[11].HeaderText = "SAF";
            dgTot.Columns[11].DefaultCellStyle.Format = "N3";
            dgTot.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgTot.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

        }

        //Get all cases
        private void GetCaseData()
        {
            datatable = work_data.GetCasesData(Survey, Newtc);
            dgData.DataSource = datatable;
            //save table
            clonetable = datatable.Clone();
            lblCases.Text = datatable.Rows.Count.ToString("N0");
            setItemColumnHeader();

        }

        private void setItemColumnHeader()
        {
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);
            string mon = sdate.Substring(4); //mon = "05";
            //get month name
            string ppmonth1 = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string ppmonth2 = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string ppmonth3 = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string ppmonth4 = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string ppmonth5 = (dt.AddMonths(-5)).ToString("yyyyMM", CultureInfo.InvariantCulture);

            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[0].ReadOnly = true;
            dgData.Columns[1].HeaderText = "Newtc";
            dgData.Columns[1].ReadOnly = true;

            dgData.Columns[2].HeaderText = "Status";
            dgData.Columns[2].ReadOnly = true;

            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].HeaderText = sdate;
            DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dgData.Columns[3];//here index of column
            cvip.MaxInputLength = 6;

            dgData.Columns[4].HeaderText = "F";
            dgData.Columns[4].Width = 20;
            dgData.Columns[4].ReadOnly = true;
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].HeaderText = ppmonth1;
            cvip = (DataGridViewTextBoxColumn)dgData.Columns[5];//here index of column
            cvip.MaxInputLength = 6;

            dgData.Columns[6].HeaderText = "F";
            dgData.Columns[6].Width = 20;
            dgData.Columns[6].ReadOnly = true;
            dgData.Columns[7].DefaultCellStyle.Format = "N0";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[7].HeaderText = ppmonth2;
            cvip = (DataGridViewTextBoxColumn)dgData.Columns[7];//here index of column
            cvip.MaxInputLength = 6;

            dgData.Columns[8].HeaderText = "F";
            dgData.Columns[8].Width = 20;
            dgData.Columns[8].ReadOnly = true;
            dgData.Columns[9].DefaultCellStyle.Format = "N0";
            dgData.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[9].HeaderText = ppmonth3;
            if (mon!= "05") //will change back to "05 after 2019
                dgData.Columns[9].ReadOnly = true;
            else
                dgData.Columns[9].ReadOnly = false;
            cvip = (DataGridViewTextBoxColumn)dgData.Columns[9];//here index of column
            cvip.MaxInputLength = 6;

            dgData.Columns[10].HeaderText = "F";
            dgData.Columns[10].ReadOnly = true;
            dgData.Columns[10].Width = 20;
            dgData.Columns[10].ReadOnly = true;
            dgData.Columns[11].DefaultCellStyle.Format = "N0";
            dgData.Columns[11].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[11].HeaderText = ppmonth4;
            if (mon != "05") //will change back to "05 after 2019
                dgData.Columns[11].ReadOnly = true;
            else
                dgData.Columns[11].ReadOnly = false;
            cvip = (DataGridViewTextBoxColumn)dgData.Columns[11];//here index of column
            cvip.MaxInputLength = 6;

            dgData.Columns[12].HeaderText = "F";
            dgData.Columns[12].ReadOnly = true;
            dgData.Columns[12].Width = 20;
            dgData.Columns[13].DefaultCellStyle.Format = "N0";
            dgData.Columns[13].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[13].HeaderText = ppmonth5;
            dgData.Columns[13].ReadOnly = true;

            dgData.Columns[14].HeaderText = "F";
            dgData.Columns[14].Width = 20;
            dgData.Columns[14].ReadOnly = true;

            dgData.Columns[15].DefaultCellStyle.Format = "N0";
            dgData.Columns[15].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[15].HeaderText = "Cumvip";
            dgData.Columns[15].ReadOnly = true;

            dgData.Columns[16].DefaultCellStyle.Format = "N2";
            dgData.Columns[16].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[16].HeaderText = "%Comp";
            dgData.Columns[16].ReadOnly = true;

            dgData.Columns[17].HeaderText = "Strtdate";
            dgData.Columns[17].ReadOnly = true;
            dgData.Columns[18].HeaderText = "Compdate";
            dgData.Columns[18].ReadOnly = true;

            dgData.Columns[19].DefaultCellStyle.Format = "N0";
            dgData.Columns[19].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[19].HeaderText = "Rvitm5c";
            dgData.Columns[19].ReadOnly = true;

            dgData.Columns[20].DefaultCellStyle.Format = "N0";
            dgData.Columns[20].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[20].HeaderText = "Item6";
            dgData.Columns[20].ReadOnly = true;

            dgData.Columns[21].DefaultCellStyle.Format = "N2";
            dgData.Columns[21].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[21].HeaderText = "Fwgt";
            dgData.Columns[21].ReadOnly = true;

            dgData.Columns[22].Visible = false;
        }

       
        private void setChangeColumnHeader(DataGridView dg)
        {
            dg.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[0].HeaderText = "ID    ";

            dg.Columns[1].DefaultCellStyle.Format = "N0";
            dg.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[1].HeaderText = "Wgtvip";

            dg.Columns[2].HeaderText = "F";
            dg.Columns[2].Width = 20;

            dg.Columns[3].DefaultCellStyle.Format = "N0";
            dg.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dg.Columns[3].HeaderText = "Change";

            dg.Columns[4].Visible = false;
            dg.Columns[5].Visible = false;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dg.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //get change data for current month
        private void GetCurData()
        {
            curr_table = work_data.GetChangeData(Survey, Newtc, 1);
            dgCur.DataSource = curr_table;
            if (curr_table.Rows.Count == 0)
                lblTotCur.Text = "0";
            else
            {
                object sumdiff = curr_table.Compute("Sum(WGTDIF)", "");
                lblTotCur.Text = sumdiff.ToString();
            }
            setChangeColumnHeader(dgCur);
            dgCur.ClearSelection();
        }

        //Get change data for rev1
        private void GetRev1Data()
        {
            rev1_table = work_data.GetChangeData(Survey, Newtc, 2);
            dgRev1.DataSource = rev1_table;
            if (rev1_table.Rows.Count == 0)
                lblTotRev1.Text = "0";
            else
            {
                object sumdiff = rev1_table.Compute("Sum(WGTDIF)", "");
                lblTotRev1.Text = sumdiff.ToString();
            }
            setChangeColumnHeader(dgRev1);
            dgRev1.ClearSelection();
        }
        
        //Get change data for rev2
        private void GetRev2Data()
        {
            rev2_table = work_data.GetChangeData(Survey, Newtc, 3);
            dgRev2.DataSource = rev2_table;
            if (rev2_table.Rows.Count == 0)
                lblTotRev2.Text = "0";
            else
            {
                object sumdiff = rev2_table.Compute("Sum(WGTDIF)", "");
                lblTotRev2.Text = sumdiff.ToString();
            }
            setChangeColumnHeader(dgRev2);
            dgRev2.ClearSelection();
        }

        //Get change data for rev3
        private void GetRev3Data()
        {
            rev3_table = work_data.GetChangeData(Survey, Newtc, 4);
            dgRev3.DataSource = rev3_table;
            if (rev3_table.Rows.Count == 0)
                lblTotRev3.Text = "0";
            else
            {
                object sumdiff = rev3_table.Compute("Sum(WGTDIF)", "");
                lblTotRev3.Text = sumdiff.ToString();
            }
            setChangeColumnHeader(dgRev3);
            dgRev3.ClearSelection();
        }

        //Get change data for rev4
        private void GetRev4Data()
        {
            rev4_table = work_data.GetChangeData(Survey, Newtc, 5);
            dgRev4.DataSource = rev4_table;
            if (rev4_table.Rows.Count == 0)
                lblTotRev4.Text = "0";
            else
            {
                object sumdiff = rev4_table.Compute("Sum(WGTDIF)", "");
                lblTotRev4.Text = sumdiff.ToString();
            }
            setChangeColumnHeader(dgRev4);
            dgRev4.ClearSelection();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (Editable && (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed))
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }
            if (!Editable && (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed))
            {
                var result = MessageBox.Show("Changes were made. Do you want to Print?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                    printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
                    printDocument1.DefaultPageSettings.Landscape = true;
                    memoryImage = GeneralFunctions.CaptureScreen(this);
                    printDocument1.Print();

                }
            }

            if (Editable )
                lock_data.UpdateTabLock(tc2, false);

            if (CallingForm != null)
            {
                CallingForm.Show();
                CallingForm.RefreshForm(true);
                call_callingFrom = true;
            }

            this.Close();
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            string cvalue;
            string colname = cbItem.Text;
            IEnumerable<DataRow> query;

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

            cvalue = txtValueItem.Text.Trim();
            if (colname == "ID")
            {
                if (cvalue.Length != 7 || !GeneralDataFuctions.ValidateSampleId(cvalue))
                {
                    if (cvalue.Length != 7)
                    {
                        MessageBox.Show("ID should be 7 digits");
                    }
                    else
                    {
                        MessageBox.Show("Invalid ID");
                    }
                   
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }
            else if (colname == "NEWTC")
            {
                if (cvalue.Length != 4 || !GeneralDataFuctions.CheckNewTC(cvalue))
                {
                    MessageBox.Show("Invalid Newtc");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }
            else
            {
                if (cvalue.Length != 6 || !GeneralFunctions.ValidateDateWithRange(cvalue))
                {
                    MessageBox.Show("Invalid Strtdate");
                    txtValueItem.Text = "";
                    txtValueItem.Focus();
                    return;
                }
            }

            query = from myRow in datatable.AsEnumerable()
                where myRow.Field<string>(colname).StartsWith(cvalue)
                orderby myRow.Field<string>(colname) descending
                select myRow;
            
            // Create a table from the query.
            if (query.Count() > 0)
            {
                DataTable boundTable = query.CopyToDataTable<DataRow>();
                dgData.DataSource = boundTable;
                lblCases.Text = boundTable.Rows.Count.ToString("N0");
            }
            else
            {
                MessageBox.Show("No data exists");
                txtValueItem.Text = "";
                dgData.DataSource = clonetable;
                lblCases.Text = "0";
            }

            cbVip1.SelectedIndex = 0;
            cbVip2.SelectedIndex = 0;
            cbVip3.SelectedIndex = 0;
            cbVip4.SelectedIndex = 0;
            cbVip5.SelectedIndex = 0;

            UnselectedMiddleGrids();
        }

        //after search, clear, focus on buttom table
        private void UnselectedMiddleGrids()
        {
            dgCur.ClearSelection();
            dgRev1.ClearSelection();
            dgRev2.ClearSelection();
            dgRev3.ClearSelection();
            dgRev4.ClearSelection();

        }

        private void btnClearItem_Click(object sender, EventArgs e)
        {
            cbItem.SelectedIndex = -1;
            cbVip1.SelectedIndex = 0;
            cbVip2.SelectedIndex = 0;
            cbVip3.SelectedIndex = 0;
            cbVip4.SelectedIndex = 0;
            cbVip5.SelectedIndex = 0;
            txtValueItem.Visible = false;
            txtValueItem.Text = "";

            //GetCaseData();
            dgData.DataSource = null;
            dgData.DataSource = datatable;
            setItemColumnHeader();
            lblCases.Text = datatable.Rows.Count.ToString("N0");
            UnselectedMiddleGrids();
        }

        Bitmap memoryImage;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            decimal cm0=0;
            decimal cm1 = 0;
            decimal cm2 = 0;
            decimal cm3 = 0;
            decimal cm4 = 0;
            
            //update total table
            if (v0_changed)
                UpdateTotTableByRow(0, ref cm0);
            else
                cm0 = old_cm0;
            
            if (v1_changed)
                UpdateTotTableByRow(1, ref cm1);
            else
                cm1 = old_cm1;

            if (v2_changed)
                UpdateTotTableByRow(2, ref cm2);
            else
                cm2 = old_cm2;

            if (v3_changed)
                UpdateTotTableByRow(3, ref cm3);
            else
                cm3 = old_cm3;

            if (v4_changed)
                UpdateTotTableByRow(4, ref cm4);
            else
                cm4 = old_cm4;

            //update top table
            if (v0_changed || v1_changed)
            {
                decimal pct1 =0;
                if (cm1 >0)
                    pct1 = ((cm0 - cm1) / cm1) * 100;
                if (v0_changed)
                    dgTc[1, 0].Value = cm0;
                dgTc[6, 0].Value = pct1;
            }
            if (v1_changed)
            {
                decimal pct3 = 0;
                if (old_pm0 >0)
                  pct3=((cm1 - old_pm0) / old_pm0) * 100;
                dgTc[2, 0].Value = cm1;
                dgTc[8, 0].Value = pct3;
            }
            if (v2_changed || v1_changed)
            {
                decimal pct2 = 0;
                if (cm2 >0)
                    pct2 = ((cm1 - cm2)/cm2) * 100;
                dgTc[7, 0].Value = pct2;
            }
            if (v2_changed)
            {
                decimal pct4 = 0;
                if (old_pm1 >0)
                 pct4 = ((cm2 - old_pm1) / old_pm1) * 100;
                dgTc[3, 0].Value = cm2;
                dgTc[9, 0].Value = pct4;
            }
            if (v3_changed || v2_changed)
            {
                decimal pct2 = 0;
                if (cm3 > 0)
                    pct2 = ((cm2 - cm3) / cm3) * 100;
                dgTc[14, 0].Value = pct2;
            }
            if (v3_changed)
            {
                decimal pct4 = 0;
                if (old_pm2 > 0)
                    pct4 = ((cm3 - old_pm2) / old_pm2) * 100;
                dgTc[10, 0].Value = cm3;
                dgTc[16, 0].Value = pct4;
            }
            if (v4_changed || v3_changed)
            {
                decimal pct2 = 0;
                if (cm4 > 0)
                    pct2 = ((cm3 - cm4) / cm4) * 100;
                dgTc[15, 0].Value = pct2;
            }
            if (v4_changed)
            {
                decimal pct4 = 0;
                if (old_pm3 > 0)
                    pct4 = ((cm4 - old_pm3) / old_pm3) * 100;
                dgTc[11, 0].Value = cm4;
                dgTc[17, 0].Value = pct4;
            }

            isApply = true;

            //set buttom focus
           // dgData.ClearSelection();
            UnselectedMiddleGrids();
           // dgData.Rows[0].Cells[0].Selected = true;
        }

        //update total table by table row
        private void UpdateTotTableByRow(int lindex, ref decimal cm)
        {
            //get Sum(V0)
            int u_cm =0;
            if (lindex ==0)
               u_cm = Convert.ToInt32(datatable.Compute("Sum(V0)", ""));
            else if (lindex ==1)
                u_cm = Convert.ToInt32(datatable.Compute("Sum(V1)", ""));
            else if (lindex == 2)
                u_cm = Convert.ToInt32(datatable.Compute("Sum(V2)", ""));
            else if (lindex == 3)
                u_cm = Convert.ToInt32(datatable.Compute("Sum(V3)", ""));
            else if (lindex == 4)
                u_cm = Convert.ToInt32(datatable.Compute("Sum(V4)", ""));

            //update table cell
            if (lindex == 3 || lindex == 4)
                lindex = lindex + 2;

            //get Lsf, UCF, bst, saf
            double lsf = Convert.ToDouble(dgTot[8, lindex].Value);
            double ucf = Convert.ToDouble(dgTot[9, lindex].Value);
            double bst = Convert.ToDouble(dgTot[10, lindex].Value);
            double saf = Convert.ToDouble(dgTot[11, lindex].Value);

            int l_cm = (int)(Math.Round(u_cm * lsf, MidpointRounding.AwayFromZero));
            int c_cm = (int)Math.Round(u_cm * lsf * ucf, MidpointRounding.AwayFromZero);
            int b_cm = (int)(Math.Round(u_cm * lsf * ucf * bst, MidpointRounding.AwayFromZero));
            int s_cm = (int)(Math.Round((u_cm * lsf * ucf * bst * 12) / saf, MidpointRounding.AwayFromZero));
            double s= (u_cm * lsf * ucf * bst * 12)/(saf*1000000);
            cm =(decimal)Math.Round(s, 1);

            dgTot[2, lindex].Value = u_cm;
            dgTot[3, lindex].Value = l_cm;
            dgTot[4, lindex].Value = c_cm;
            dgTot[5, lindex].Value = b_cm;
            dgTot[6, lindex].Value = s_cm;
            dgTot[7, lindex].Value = cm;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!v0_changed && !v1_changed && !v2_changed && !v3_changed && !v4_changed)
                return;

            if (!isApply)
            {
                MessageBox.Show("You must apply changes before saving data to tables");
                return;
            }
            SaveData();

           //set buttom focus
            dgData.ClearSelection();
            UnselectedMiddleGrids();
            dgData.Rows[0].Cells[0].Selected = true;
            
            MessageBox.Show("Save Data compeleted.");
        }

        //Save data
        private void SaveData()
        {
            //update Vipupd table
            if (v0_changed)
                work_data.UpdateChangeData(Survey, Newtc, curr_table, 1);
            if (v1_changed)
                work_data.UpdateChangeData(Survey, Newtc, rev1_table, 2);
            if (v2_changed)
                work_data.UpdateChangeData(Survey, Newtc, rev2_table, 3);
            if (v3_changed)
                work_data.UpdateChangeData(Survey, Newtc, rev3_table, 4);
            if (v4_changed)
                work_data.UpdateChangeData(Survey, Newtc, rev4_table, 5);

            //update vipProj table
            work_data.UpdateVipProj(datatable);

            //update vipsadj table
            work_data.SaveVipsadj(Survey, Newtc);

            v0_changed = false;
            v1_changed = false;
            v2_changed = false;
            v3_changed = false;
            v4_changed = false;
            isApply = false;

            //initial modify column in case table
            for (int i = 0; i < datatable.Rows.Count; i++)
                datatable.Rows[i][22] = 0;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetTopData();
            GetMainData();
            GetCaseData();

            GetCurData();
            GetRev1Data();
            GetRev2Data();
            GetRev3Data();
            GetRev4Data();

            cbVip1.SelectedIndex = 0;
            cbVip2.SelectedIndex = 0;
            cbVip3.SelectedIndex = 0;
            cbVip4.SelectedIndex = 0;
            cbVip5.SelectedIndex = 0;
            txtValueItem.Text = "";
            cbItem.SelectedIndex = -1;

            v0_changed = false;
            v1_changed = false;
            v2_changed = false;
            v3_changed = false;
            v4_changed = false;
            isApply = false;

            UnselectedMiddleGrids();
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (Editable && (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed))
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            int index =0;
            string val1 = string.Empty;
            List<string> Idlist = new List<string>(); 

            //check current selected control
            if (last_focused == "dgData")
            {
                DataGridViewSelectedCellCollection rows = dgData.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgData["ID", index].Value.ToString();

                // Store Id in list for Page Up and Page Down
                int cnt = 0;
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    string val = dgData["ID", cnt].Value.ToString();
                    Idlist.Add(val);
                    cnt = cnt + 1;
                }
            }
            else if (last_focused == "dgCur")
            {
                DataGridViewSelectedCellCollection rows = dgCur.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgCur["ID", index].Value.ToString();
            }
            else if (last_focused == "dgRev1")
            {
                DataGridViewSelectedCellCollection rows = dgRev1.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgRev1["ID", index].Value.ToString();
            }
            else if (last_focused == "dgRev2")
            {
                DataGridViewSelectedCellCollection rows = dgRev2.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgRev2["ID", index].Value.ToString();
            }
            else if (last_focused == "dgRev3")
            {
                DataGridViewSelectedCellCollection rows = dgRev3.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgRev3["ID", index].Value.ToString();
            }
            else if (last_focused == "dgRev4")
            {
                DataGridViewSelectedCellCollection rows = dgRev4.SelectedCells;
                if (rows.Count == 0)
                {
                    MessageBox.Show("You have to select a case");
                    return;
                }
                index = rows[0].RowIndex;
                val1 = dgRev4["ID", index].Value.ToString();
            }

            this.Hide();

            frmC700 fC700 = new frmC700();
            fC700.Id = val1;
            if (last_focused == "dgData")
            {
                fC700.Idlist = Idlist;
                fC700.CurrIndex = index;
            }
            fC700.CallingForm = this;

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            fC700.ShowDialog();  // show child

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            if (Editable && (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed))
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            this.Hide();
            frmAvecost popup = new frmAvecost();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.CallingForm = this;
            
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            popup.ShowDialog();  // show child
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");  
        }

        private void ClearSearchBoxes(int cbIndex=0)
        {
            txtValueItem.Text = "";
            cbItem.SelectedIndex = -1;

            if (cbIndex==1)
            {
                cbVip2.SelectedIndex = 0;
                cbVip3.SelectedIndex = 0;
                cbVip4.SelectedIndex = 0;
                cbVip5.SelectedIndex = 0;
            }
            else if (cbIndex==2)
            {
                cbVip1.SelectedIndex = 0;
                cbVip3.SelectedIndex = 0;
                cbVip4.SelectedIndex = 0;
                cbVip5.SelectedIndex = 0;
            }
            else if (cbIndex ==3)
            {
                cbVip1.SelectedIndex = 0;
                cbVip2.SelectedIndex = 0;
                cbVip4.SelectedIndex = 0;
                cbVip5.SelectedIndex = 0;
            }
            else if (cbIndex == 4)
            {
                cbVip1.SelectedIndex = 0;
                cbVip3.SelectedIndex = 0;
                cbVip2.SelectedIndex = 0;
                cbVip5.SelectedIndex = 0;
            }
            else if (cbIndex == 5)
            {
                cbVip1.SelectedIndex = 0;
                cbVip2.SelectedIndex = 0;
                cbVip4.SelectedIndex = 0;
                cbVip3.SelectedIndex = 0;
            }
            else
            {
                cbVip1.SelectedIndex = 0;
                cbVip2.SelectedIndex = 0;
                cbVip3.SelectedIndex = 0;
                cbVip4.SelectedIndex = 0;
                cbVip5.SelectedIndex = 0;
            }
        }

        private void btnPrWvip_Click(object sender, EventArgs e)
        {
              SubsetVip(1, cbVip1.SelectedIndex);         
        }

        private void btnR1Wvip_Click(object sender, EventArgs e)
        {
              SubsetVip(2, cbVip2.SelectedIndex);    
        }

        private void btnR2Wvip_Click(object sender, EventArgs e)
        {
              SubsetVip(3, cbVip3.SelectedIndex);
        }

        private void btnR3Wvip_Click(object sender, EventArgs e)
        {
            SubsetVip(4, cbVip4.SelectedIndex);
        }

        private void btnR4Wvip_Click(object sender, EventArgs e)
        {
            SubsetVip(5, cbVip5.SelectedIndex);
        }

        private void btnPrR1_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query;
      
            var myInClause = new string[] {"R", "M", "A", "I", "O"};
            query = from myRow in datatable.AsEnumerable()
                    where myRow.Field<int>("v0") >= myRow.Field<int>("v1") * 3 && myInClause.Contains(myRow.Field<string>("fv0")) && myInClause.Contains(myRow.Field<string>("fv1")) && myRow.Field<int>("v0") >0
                    orderby myRow.Field<int>("v0") descending
                    select myRow;

            LoadCasesData(query);   
        }

        //load search data to grid
        private void LoadCasesData(IEnumerable<DataRow> query, int cbIndex =0)
        {
            if (query.Count() > 0)
            {
                DataTable boundTable = query.CopyToDataTable<DataRow>();
                dgData.DataSource = boundTable;
                lblCases.Text = boundTable.Rows.Count.ToString("N0");
            }
            else
            {
                MessageBox.Show("No data exists");
                txtValueItem.Text = "";
                dgData.DataSource = datatable;
                lblCases.Text = datatable.Rows.Count.ToString("N0");
            }

            //clear search 
            ClearSearchBoxes(cbIndex);
            UnselectedMiddleGrids();
        }

        private void btnR1Pr_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query;

            var myInClause = new string[] {"R", "M", "A", "I", "O"};
            query = from myRow in datatable.AsEnumerable()
                    where myRow.Field<int>("v1") >= myRow.Field<int>("v0") * 3 && myInClause.Contains(myRow.Field<string>("fv0")) && myInClause.Contains(myRow.Field<string>("fv1")) && myRow.Field<int>("v1") >0
                    orderby myRow.Field<int>("v1") descending
                    select myRow;

            LoadCasesData(query);
        }

        private void btnR1R2_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query;
            var myInClause = new string[] { "R", "M", "A", "I", "O" };
            query = from myRow in datatable.AsEnumerable()
                    where myRow.Field<int>("v1") >= myRow.Field<int>("v2") * 3 && myInClause.Contains(myRow.Field<string>("fv2")) && myInClause.Contains(myRow.Field<string>("fv1")) && myRow.Field<int>("v1") > 0
                    orderby myRow.Field<int>("v1") descending
                    select myRow;

            LoadCasesData(query);
        }

        private void btnR2R1_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query;
            var myInClause = new string[] { "R", "M", "A", "I", "O" };
            query = from myRow in datatable.AsEnumerable()
                    where myRow.Field<int>("v2") >= myRow.Field<int>("v1") * 3 && myInClause.Contains(myRow.Field<string>("fv2")) && myInClause.Contains(myRow.Field<string>("fv1")) && myRow.Field<int>("v2") > 0
                    orderby myRow.Field<int>("v2") descending
                    select myRow;

            LoadCasesData(query);
        }

        private void btnI6I5_Click(object sender, EventArgs e)
        {
            IEnumerable<DataRow> query;

            query = from myRow in datatable.AsEnumerable()
                    where myRow.Field<int>("item6") >= myRow.Field<int>("rvitm5c") * 0.5
                    orderby myRow.Field<int>("item6") descending
                    select myRow;

            LoadCasesData(query);
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GetCaseData();
        }

        //Verify Form Closing from menu
        public override bool VerifyFormClosing()
        {
            bool can_close = true;

            if (Editable)
            {
                if (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed)
                {
                    var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                    if (result == DialogResult.No)
                    {
                        can_close = false;
                    }
                    else
                    {
                        lock_data.UpdateTabLock(tc2, false);
                    }
                }
            }
            else
            {
                if (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed)
                {
                    var result = MessageBox.Show("Changes were made. Do you want to Print?", "Confirm", MessageBoxButtons.YesNo);
                    if (result == DialogResult.Yes)
                    {
                        printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                        printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
                        printDocument1.DefaultPageSettings.Landscape = true;
                        memoryImage = GeneralFunctions.CaptureScreen(this);
                        printDocument1.Print();
                
                    }
                }
            }

            return can_close;
        }

        private void frmWorkSheet_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Editable)
                lock_data.UpdateTabLock(tc2, false);

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        //process the click prev wgtvip, 1st rev wgtvip, 2nd rev wgtvip
        private void SubsetVip(int cbindex, int selected_index)
        {
            IEnumerable<DataRow> query;

            string fieldname;
            if (cbindex == 1)
                fieldname = "v0";
            else if (cbindex == 2)
                fieldname = "v1";
            else if (cbindex == 3)
                fieldname = "v2";
            else if (cbindex == 4)
                fieldname = "v3";
            else
                fieldname = "v4";

            if (selected_index == 0)
            {
                query = from myRow in datatable.AsEnumerable()
                        where myRow.Field<int>(fieldname) >= 5000
                        orderby myRow.Field<int>(fieldname) descending
                        select myRow;

            }
            else if (selected_index == 1)
            {
                query = from myRow in datatable.AsEnumerable()
                        where myRow.Field<int>(fieldname) >= 10000
                        orderby myRow.Field<int>(fieldname) descending
                        select myRow;
            }
            else 
            {
                query = from myRow in datatable.AsEnumerable()
                        where myRow.Field<int>(fieldname) >= 20000
                        orderby myRow.Field<int>(fieldname) descending
                        select myRow;
            }

            LoadCasesData(query, cbindex);
        }

        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgData.Columns[e.ColumnIndex].ReadOnly == false)
                {
                    string flag = dgData[e.ColumnIndex + 1, e.RowIndex].Value.ToString();

                    //Check flag,if flag is empty, not allow editable
                    if (flag == " " )
                    {
                        e.Cancel = true;
                        dgData.RefreshEdit();
                    }
                }
            }
        }

        private int oldvalue;
        private bool isvalid;
        private Int32 currentRow;
        private Int32 currentCell;
        private bool resetRow = false;

        private void dgData_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            isvalid = false;
            if (dgData.Columns[e.ColumnIndex].ReadOnly == false) 
            {
                string flag = dgData[e.ColumnIndex+1, e.RowIndex].Value.ToString();

                //Check compdate, if compdate <> "", not allow editable
                if (flag != " ")
                {
                    isvalid = true;

                   oldvalue = Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value);
                }
            }
        }

        //in search mode, if data changed, update related value in datatable 
        private void Updatedatatable(string id, int new_value, int col)
        {
            var case_item = (from x in datatable.AsEnumerable()
                        where x.Field<string>("ID") == id
                        select x).ToList();

            case_item[0][col] = new_value;
            case_item[0]["IsDiff"] = 1;

            datatable.AcceptChanges();
        }

        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            bool search_mode = false;

            if (dgData[e.ColumnIndex, e.RowIndex].Value.ToString() == "")
            {
                dgData[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                return;
            }

            if  (Convert.ToInt32(dgData[e.ColumnIndex, e.RowIndex].Value) == oldvalue)
                return;

            if (!isvalid)
            {
                dgData[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                return;
            }

            //check datatable in search mode
            DataTable dt_table = (DataTable)dgData.DataSource;
            if (dt_table.Rows.Count != datatable.Rows.Count)
                search_mode = true;

                //update curr, rev1-rev4 tables. 
                if (e.ColumnIndex ==3)
              {
                //update cumvip and %comp
                dgData[15, e.RowIndex].Value = Convert.ToInt32(dgData[15, e.RowIndex].Value) + (Convert.ToInt32(dgData[3, e.RowIndex].Value) - oldvalue);
                dgData[16, e.RowIndex].Value = ((float)Convert.ToInt32(dgData[15, e.RowIndex].Value) / (Convert.ToInt32(dgData[19, e.RowIndex].Value) * Convert.ToDouble(dgData[21, e.RowIndex].Value))) * 100;

                IEnumerable<DataRow> query;
                query = from myRow in curr_table.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString()
                        select myRow;
                
                if (query.Count() >0)
                {
                    //update row value
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[3, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[3, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    curr_table.AcceptChanges();
                    dgCur.DataSource = curr_table;
                }
                else
                {
                    DataRow newRow = curr_table.NewRow();

                    newRow["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow["WGTVIP"] = Convert.ToInt32(dgData[3, e.RowIndex].Value);
                    newRow["VIPFlag"] = dgData[4, e.RowIndex].Value.ToString();
                    newRow["WGTDIF"] = Convert.ToInt32(dgData[3, e.RowIndex].Value)- oldvalue;
                    newRow["OWNER"] = Survey;
                    newRow["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    curr_table.Rows.Add(newRow);
                    DataView dv = curr_table.DefaultView;
                    dv.Sort = "id";
                    dgCur.DataSource = dv.ToTable();
                }
                if (curr_table.Rows.Count > 0)
                {
                    int sumdiff = Convert.ToInt32(curr_table.Compute("Sum(WGTDIF)", ""));
                    if (sumdiff ==0)
                        lblTotCur.Text = "0";
                    else
                        lblTotCur.Text = sumdiff.ToString("#,#");
                }
                else
                    lblTotCur.Text = "0";

                v0_changed = true;
                dgCur.ClearSelection();
                if (search_mode)
                    Updatedatatable(dgData[0, e.RowIndex].Value.ToString(), Convert.ToInt32(dgData[3, e.RowIndex].Value), 3);

                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;

            }
            else if (e.ColumnIndex ==5)
            {
                //update comvip and %comp
                dgData[15, e.RowIndex].Value = Convert.ToInt32(dgData[15, e.RowIndex].Value) + (Convert.ToInt32(dgData[5, e.RowIndex].Value) - oldvalue);
                dgData[16, e.RowIndex].Value = ((float)Convert.ToInt32(dgData[15, e.RowIndex].Value) / (Convert.ToInt32(dgData[19, e.RowIndex].Value) * Convert.ToDouble(dgData[21, e.RowIndex].Value))) * 100;

                IEnumerable<DataRow> query;
                query = from myRow in rev1_table.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString()
                        select myRow;

                //if the id exist
                if (query.Count() > 0)
                {
                    //update row
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[5, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[5, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    rev1_table.AcceptChanges();
                    dgRev1.DataSource = rev1_table;
                }
                else
                {
                    //add new record
                    DataRow newRow = rev1_table.NewRow();
                    newRow["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow["WGTVIP"] = Convert.ToInt32(dgData[5, e.RowIndex].Value);
                    newRow["VIPFlag"] = dgData[6, e.RowIndex].Value.ToString();
                    newRow["WGTDIF"] = Convert.ToInt32(dgData[5, e.RowIndex].Value) - oldvalue;
                    newRow["OWNER"] = Survey;
                    newRow["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    rev1_table.Rows.Add(newRow);
                    DataView dv = rev1_table.DefaultView;
                    dv.Sort = "id";
                    dgRev1.DataSource = dv.ToTable(); 
                }
                if (rev1_table.Rows.Count > 0)
                {
                    int sumdiff = Convert.ToInt32(rev1_table.Compute("Sum(WGTDIF)", ""));
                    if (sumdiff == 0)
                        lblTotRev1.Text = "0";
                    else
                        lblTotRev1.Text = sumdiff.ToString("#,#");
                }
                else
                    lblTotRev1.Text = "0";

                v1_changed = true;
                dgRev1.ClearSelection();
                if (search_mode)
                    Updatedatatable(dgData[0, e.RowIndex].Value.ToString(), Convert.ToInt32(dgData[5, e.RowIndex].Value), 5);

                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;

            }
            else if (e.ColumnIndex == 7)
            {
                //update comvip and %comp
                dgData[15, e.RowIndex].Value = Convert.ToInt32(dgData[15, e.RowIndex].Value) + (Convert.ToInt32(dgData[7, e.RowIndex].Value) - oldvalue);
                dgData[16, e.RowIndex].Value = ((float)Convert.ToInt32(dgData[15, e.RowIndex].Value) / (Convert.ToInt32(dgData[19, e.RowIndex].Value) * Convert.ToDouble(dgData[21, e.RowIndex].Value))) * 100;

                IEnumerable<DataRow> query;
                query = from myRow in rev2_table.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString()
                        select myRow;

                //if the id exist
                if (query.Count() > 0)
                {
                    //update row
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[7, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[7, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    rev2_table.AcceptChanges();
                    dgRev2.DataSource = rev2_table;
                }
                else
                {
                    //add new record
                    DataRow newRow = rev2_table.NewRow();

                    newRow["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow["WGTVIP"] = Convert.ToInt32(dgData[7, e.RowIndex].Value);
                    newRow["VIPFlag"] = dgData[8, e.RowIndex].Value.ToString();
                    newRow["WGTDIF"] = Convert.ToInt32(dgData[7, e.RowIndex].Value) - oldvalue;
                    newRow["OWNER"] = Survey;
                    newRow["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    rev2_table.Rows.Add(newRow);
                    DataView dv = rev2_table.DefaultView;
                    dv.Sort = "id";
                    dgRev2.DataSource = dv.ToTable();
                }
                if (rev2_table.Rows.Count > 0)
                {
                    int sumdiff = Convert.ToInt32(rev2_table.Compute("Sum(WGTDIF)", ""));
                    if (sumdiff ==0)
                        lblTotRev2.Text = "0";
                    else 
                        lblTotRev2.Text = sumdiff.ToString("#,#");
                }
                else
                    lblTotRev2.Text = "0";

                v2_changed = true;
                dgRev2.ClearSelection();
                if (search_mode)
                    Updatedatatable(dgData[0, e.RowIndex].Value.ToString(), Convert.ToInt32(dgData[7, e.RowIndex].Value), 7);

                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;
            }
            else if (e.ColumnIndex == 9)
            {
                //update comvip and %comp
                dgData[15, e.RowIndex].Value = Convert.ToInt32(dgData[15, e.RowIndex].Value) + (Convert.ToInt32(dgData[9, e.RowIndex].Value) - oldvalue);
                dgData[16, e.RowIndex].Value = ((float)Convert.ToInt32(dgData[15, e.RowIndex].Value) / (Convert.ToInt32(dgData[19, e.RowIndex].Value)* Convert.ToDouble(dgData[21, e.RowIndex].Value))) * 100;

                IEnumerable<DataRow> query;
                query = from myRow in rev3_table.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString()
                        select myRow;

                //if the id exist
                if (query.Count() > 0)
                {
                    //update row
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[9, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[9, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    rev3_table.AcceptChanges();
                    dgRev3.DataSource = rev3_table;
                }
                else
                {
                    //add new record
                    DataRow newRow = rev3_table.NewRow();

                    newRow["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow["WGTVIP"] = Convert.ToInt32(dgData[9, e.RowIndex].Value);
                    newRow["VIPFlag"] = dgData[10, e.RowIndex].Value.ToString();
                    newRow["WGTDIF"] = Convert.ToInt32(dgData[9, e.RowIndex].Value) - oldvalue;
                    newRow["OWNER"] = Survey;
                    newRow["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    rev3_table.Rows.Add(newRow);
                    DataView dv = rev3_table.DefaultView;
                    dv.Sort = "id";
                    dgRev3.DataSource = dv.ToTable();
                }
                if (rev3_table.Rows.Count > 0)
                {
                    int sumdiff = Convert.ToInt32(rev3_table.Compute("Sum(WGTDIF)", ""));
                    if (sumdiff == 0)
                        lblTotRev3.Text = "0";
                    else
                        lblTotRev3.Text = sumdiff.ToString("#,#");
                }
                else
                    lblTotRev3.Text = "0";

                v3_changed = true;
                dgRev3.ClearSelection();
                if (search_mode)
                    Updatedatatable(dgData[0, e.RowIndex].Value.ToString(), Convert.ToInt32(dgData[9, e.RowIndex].Value), 9);

                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;
            }
            else if (e.ColumnIndex == 11)
            {
                //update comvip and %comp
                dgData[15, e.RowIndex].Value = Convert.ToInt32(dgData[15, e.RowIndex].Value) + (Convert.ToInt32(dgData[11, e.RowIndex].Value) - oldvalue);
                dgData[16, e.RowIndex].Value = ((float)Convert.ToInt32(dgData[15, e.RowIndex].Value) /(Convert.ToInt32(dgData[19, e.RowIndex].Value)* Convert.ToDouble(dgData[21, e.RowIndex].Value))) * 100;

                IEnumerable<DataRow> query;
                query = from myRow in rev4_table.AsEnumerable()
                        where myRow.Field<string>("ID") == dgData[0, e.RowIndex].Value.ToString()
                        select myRow;

                //if the id exist
                if (query.Count() > 0)
                {
                    //update row
                    foreach (var row in query)
                    {
                        row.SetField("WGTVIP", Convert.ToInt32(dgData[11, e.RowIndex].Value));
                        row.SetField("WGTDIF", row.Field<int>("WGTDIF") + (Convert.ToInt32(dgData[11, e.RowIndex].Value) - oldvalue));
                        if (row.Field<int>("WGTDIF") == 0)
                        {
                            row.Delete();
                            break;
                        }
                    }
                    rev4_table.AcceptChanges();
                    dgRev4.DataSource = rev4_table;
                }
                else
                {
                    //add new record
                    DataRow newRow = rev4_table.NewRow();

                    newRow["ID"] = dgData[0, e.RowIndex].Value.ToString();
                    newRow["WGTVIP"] = Convert.ToInt32(dgData[11, e.RowIndex].Value);
                    newRow["VIPFlag"] = dgData[12, e.RowIndex].Value.ToString();
                    newRow["WGTDIF"] = Convert.ToInt32(dgData[11, e.RowIndex].Value) - oldvalue;
                    newRow["OWNER"] = Survey;
                    newRow["NEWTC"] = dgData[1, e.RowIndex].Value.ToString();
                    rev4_table.Rows.Add(newRow);
                    DataView dv = rev4_table.DefaultView;
                    dv.Sort = "id";
                    dgRev4.DataSource = dv.ToTable();
                }
                if (rev4_table.Rows.Count > 0)
                {
                    int sumdiff = Convert.ToInt32(rev4_table.Compute("Sum(WGTDIF)", ""));
                    if (sumdiff == 0)
                        lblTotRev4.Text = "0";
                    else
                        lblTotRev4.Text = sumdiff.ToString("#,#");
                }
                else
                    lblTotRev4.Text = "0";

                v4_changed = true;
                dgRev4.ClearSelection();
                if (search_mode)
                    Updatedatatable(dgData[0, e.RowIndex].Value.ToString(), Convert.ToInt32(dgData[11, e.RowIndex].Value), 11);

                resetRow = true;
                currentRow = e.RowIndex;
                currentCell = e.ColumnIndex;
            }

            dgData[22, e.RowIndex].Value = 1;

        }

        private static KeyPressEventHandler NumbericCheckHandler = new KeyPressEventHandler(NumbericCheck);
        private static void NumbericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar);
        }

        private void dgData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgData.CurrentCell.ColumnIndex == 3 || dgData.CurrentCell.ColumnIndex == 5 || dgData.CurrentCell.ColumnIndex == 7 || dgData.CurrentCell.ColumnIndex == 9 || dgData.CurrentCell.ColumnIndex == 11)
            {
                e.Control.KeyPress -= NumbericCheckHandler;
                e.Control.KeyPress += NumbericCheckHandler;
            }
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValueItem.Visible = true;
            txtValueItem.Text = "";
            if (cbItem.SelectedIndex == 0)
                txtValueItem.MaxLength = 7;
            else if (cbItem.SelectedIndex == 1)
                txtValueItem.MaxLength = 4;
            else if (cbItem.SelectedIndex == 2)
                txtValueItem.MaxLength = 6;

            txtValueItem.Focus();
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        private void btnDist_Click(object sender, EventArgs e)
        {
            if (Editable && (v0_changed || v1_changed || v2_changed || v3_changed || v4_changed))
            {
                var result = MessageBox.Show("Data has changed and was not saved.  Continue to Exit?", "Confirm", MessageBoxButtons.YesNo);
                if (result == DialogResult.No)
                    return;
            }

            this.Hide();
            frmDistribution popup = new frmDistribution();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.CallingForm = this;
            popup.Newtc = Newtc;
            popup.Survey = Survey;

            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
            popup.ShowDialog();  // show child
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
        }

        private void dgCur_Enter(object sender, EventArgs e)
        {
            if (dgCur.Rows.Count == 0)
                return;

            last_focused = dgCur.Name;
            dgData.ClearSelection();
            dgRev1.ClearSelection();
            dgRev2.ClearSelection();
            dgRev3.ClearSelection();
            dgRev4.ClearSelection();
        }

        private void dgRev1_Enter(object sender, EventArgs e)
        {
            if (dgRev1.Rows.Count == 0)
                return;

            last_focused = dgRev1.Name;
            dgData.ClearSelection();
            dgCur.ClearSelection();
            dgRev2.ClearSelection();
            dgRev3.ClearSelection();
            dgRev4.ClearSelection();
        }

        private void dgRev2_Enter(object sender, EventArgs e)
        {
            if (dgRev2.Rows.Count == 0)
                return;

            last_focused = dgRev2.Name;
            dgData.ClearSelection();
            dgRev1.ClearSelection();
            dgCur.ClearSelection();
            dgRev3.ClearSelection();
            dgRev4.ClearSelection();
        }

        private void dgRev3_Enter(object sender, EventArgs e)
        {
            if (dgRev3.Rows.Count == 0)
                return;

            last_focused = dgRev3.Name;
            dgData.ClearSelection();
            dgRev1.ClearSelection();
            dgRev2.ClearSelection();
            dgCur.ClearSelection();
            dgRev4.ClearSelection();
        }

        private void dgRev4_Enter(object sender, EventArgs e)
        {
            if (dgRev4.Rows.Count == 0)
                return;

            last_focused = dgRev4.Name;
            dgData.ClearSelection();
            dgRev1.ClearSelection();
            dgRev2.ClearSelection();
            dgRev3.ClearSelection();
            dgCur.ClearSelection();
        }

        private void dgData_Enter(object sender, EventArgs e)
        {
            if (dgData.Rows.Count == 0)
                return;

            last_focused = dgData.Name;
            dgCur.ClearSelection();
            dgRev1.ClearSelection();
            dgRev2.ClearSelection();
            dgRev3.ClearSelection();
            dgRev4.ClearSelection();
        }

        private void txtValueItem_KeyDown(object sender, KeyEventArgs e)
        {
             if (e.KeyCode == Keys.Enter)
                btnSearchItem_Click(sender, e);
        }
        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                dgData.CurrentCell = dgData.Rows[currentRow].Cells[currentCell];
            }
        }

        private void btnCost_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnC700_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnDist_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnRefresh_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnApply_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnSave_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
