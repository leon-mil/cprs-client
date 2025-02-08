/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmImprovement.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/04/2015
Inputs:             Id
                    Callingform
                    Idlist
                    CurrIndex
                    
Parameters:		    None                
Outputs:		    None

Description:	    This screen displays the Cesample, cejobs, ceflags
                    data in table for a Id

Detailed Design:    Detailed User Requirements for Improvement Screen 

Other:	            Called from: ceimprovement search screen, frmImpMarkCaseReview.cs
 
Revision History:	
***********************************************************************************
 Modified Date :  Feb. 4 2020
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request: CR#3943 
 Description   :  Add help for job
***********************************************************************************
 Modified Date :  Mar. 6 2024
 Modified By   :  Christine Zhang
 Keyword       :  20240306cz
 Change Request:  
 Description   :  replace detcode with jobidcode
***********************************************************************************/

using System.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmImprovements : Cprs.frmCprsParent
    {
        /******public properties ********/
/*Required */
public string Id;
        public Form CallingForm = null;

        /*Optional */
        public List<string> Idlist = null;
        public int CurrIndex = 0;
                
        /*Form global variable */
        private Cesample cesamp;
        private CesampleData cesampdata;
        private Cejobs jobs;
        private CejobsData jobsdata;
        private Cecomments cecom;
        private CecommentData cecomdata;
        private CemarkData cmarkdata;
        private CeauditData cadata;
        private CeflagsData cflagsData;
        private CeaccessData caccessdata;
        private ImpReferralData impreferralData;

        private delegate void ShowLockMessageDelegate();

        private Ceflags cflags;
        private float finwt;
        private Cejob job;
        private Cejob pjob = null;

        private List<Ceaudit> Ceauditlist = new List<Ceaudit>();
 
        private bool editable = false;
        private bool isCurrInterview = false;
        private string default_flag = "";
        private string locked_by = String.Empty;

        /*flag to use closing the calling form */
        private bool call_callingFrom = false;

        //flag to check the data has been saved or not
        private bool data_saved = false;

        public frmImprovements()
        {           
            InitializeComponent();
        }

        private void frmImprovements_Load(object sender, EventArgs e)
        {
            /*check required properties are set */
            if (String.IsNullOrEmpty(Id) || (CallingForm == null))
            {
                MessageBox.Show("Id, CallingForm are required properties for showing Improvement form!");
                this.Dispose();
            }

            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            LoadForm();
            DisplayForm();

        }

        /*load form from database*/
        private void LoadForm(bool checklock = true)
        {
            cesampdata = new CesampleData();

            /*lock cesample */
            if (checklock)
            {
                locked_by = cesampdata.CheckIsLocked(Id);
                if (String.IsNullOrEmpty(locked_by))
                {
                    bool locked = cesampdata.UpdateLock(Id, UserInfo.UserName);
                    editable = true;
                    lblLock.Visible = false;
                }
                else
                {
                    editable = false;
                    lblLock.Visible = true;
                }
            }
            

            /*Cesample */
            cesamp = cesampdata.GetCesampleTable(Id);

            /*Comment history */
            cecomdata = new CecommentData();
            cecom = cecomdata.GetCecommentData(Id);

            /*Cemark */
            cmarkdata = new CemarkData();
            
            /*Cejobs */
            jobsdata = new CejobsData();
            jobs = jobsdata.GetCejobs(Id);

            /*load flag info */
            cflags = new Ceflags(jobs, cesamp.Interview, cesamp.Propval, cesamp.Finwt);
            cflagsData = new CeflagsData();

            /*load referral data */
            impreferralData = new ImpReferralData();

            /*load interview data */
            List <string> joblist = jobs.GetInterviewlist();
            cbInterview.DataSource = joblist;
            if (joblist.Count > 0)
                cbInterview.SelectedIndex = 0;

            Ceauditlist.Clear();

            /*Add record to csdaccess */
            caccessdata = new CeaccessData();
            data_saved = false;
          
        }

        /*display form */
        private void DisplayForm()
        {
            /*display cesample data */
            txtId.Text = cesamp.Id;
            txtPropval.Text = cesamp.Propval.ToString("#,#");
            txtYrbuit.Text = cesamp.GetDisplayYrbuilt();
            txtUnits.Text = cesamp.GetDisplayUnits();
            txtState.Text = cesamp.Stater;
            txtWeight.Text = cesamp.Finwt.ToString("#,#");
            txtIncome.Text = cesamp.Income.ToString("#,#");
            txtYrset.Text = cesamp.Yrset;
            txtCity.Text = cesamp.City;
            txtZip.Text = cesamp.Zip;
            txtSurvdate.Text = cesamp.Survdate;
            txtInterview.Text = cesamp.Interview;
            txtBedrooms.Text = cesamp.Bedroom.ToString();
            txtFullbath.Text = cesamp.Bathroom.ToString();
            txtHalfbath.Text = cesamp.Hlfbath.ToString();


            /*display referral */
            lblReferral.Visible = impreferralData.CheckReferralExist(cesamp.Id);

            /*display the first comment */
            if (cecom.Cecommentlist.Count() != 0)
            {
                dgComments.DataSource = cecom.Cecommentlist;
                dgComments.Columns[0].Width = 60;
                dgComments.Columns[1].Visible  = false;
                dgComments.Columns[2].Width = 60;
                dgComments.ColumnHeadersVisible = false;
                
            }
            else
            {
                dgComments.DataSource = null;
               
            }


            if (CheckCeMarkExists())
                lblMark.Text = "MARKED";
            else
                lblMark.Text = "";


            /*if there is mcdlist, set count boxes */
            if (Idlist != null)
            {
                txtCurrentrec.Text = (CurrIndex + 1).ToString();
                txtTotalrec.Text = Idlist.Count.ToString();
            }
            else
            {
                txtCurrentrec.Text = "1";
                txtTotalrec.Text = "1";
            }

            /*display default flag */
            toolTip1.SetToolTip(BtnClear, "Delete Default Flag");
            txtDflag.Text = "";
            default_flag = "";

            this.tabControl1.SelectedIndex = 0;

            BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));

        }

        private void ShowLockMessage()
        {
            /*show message if the case locked by someone */
            if (locked_by != "")
                MessageBox.Show("The case is locked by " + locked_by + ", cannot be edited.");
        }

        private void cbInterview_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayJobsAll();
        }
        private void cbJobcode_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayJob();
        }

        /*Load all jobs for selected interview */
        private void DisplayJobsAll()
        {
            /*Only current interview can be editable */
            if (editable)
            {
                if (cbInterview.SelectedItem.ToString() == cesamp.Interview)
                    isCurrInterview = true;
                else
                    isCurrInterview = false;
            }

            /*setup jobcode list for the selected interview */
            List<string> jobcodelist = jobs.GetJobcodelist(cbInterview.SelectedItem.ToString());
            cbJobcode.DataSource = jobcodelist;
            if (jobcodelist.Count > 0)
                cbJobcode.SelectedIndex = 0;
            else
            {
                cbJobcode.Text = "";
                DisplayJob();
            }

            if (jobcodelist.Count() > 1)
                lblJobcode.Text = "*JOB CODE";
            else
                lblJobcode.Text = "JOB CODE";
            txtNumjobs.Text = jobcodelist.Count().ToString();
            txtValuejobs.Text = jobs.GetJobsValueForInterview(cbInterview.SelectedItem.ToString()).ToString("#,#");
            

        }

        /*display a job for selected interview and jobcode */
        private void DisplayJob()
        {
            string selected_interview = cbInterview.SelectedItem.ToString();

            string pre_interview = "0";
            if (cbInterview.SelectedIndex < cbInterview.Items.Count - 1)
                pre_interview = cbInterview.Items[cbInterview.SelectedIndex + 1].ToString();

            if (pre_interview != "0")
            {
                lblInterview.Text = "*INTERVIEW";
                //cbInterview.BackColor = Color.Red;
                //cbInterview.ForeColor = Color.White;
            }
            else
            {
                lblInterview.Text = "INTERVIEW";
                //cbInterview.BackColor = Color.White;
                //cbInterview.ForeColor = Color.Black;
            }

            if (cbJobcode.Items.Count > 0)
            {
                string selected_jobcode = cbJobcode.SelectedItem.ToString();
                job = jobs.GetJobForInterviewJobcode(selected_interview, selected_jobcode);

                if (pre_interview != "0")
                    pjob = jobs.GetJobForInterviewJobcode(pre_interview, selected_jobcode);
                else
                    pjob = null;

                txtTotal.Text = job.Tcost > 0 ? job.Tcost.ToString("#,#") : job.Tcost.ToString();
                txtTcon.Text = job.Tcon > 0 ? job.Tcon.ToString("#,#") : job.Tcon.ToString();
                txtTamt.Text = job.Tamt > 0 ? job.Tamt.ToString("#,#") : job.Tamt.ToString();
                txtTren.Text = job.Tren > 0 ? job.Tren.ToString("#,#") : job.Tren.ToString();
                string who_val = job.Who.ToString();
                if (who_val == "1")
                    txtWho.Text = "DIY";
                else if (who_val == "2")
                    txtWho.Text = "CONTRACTOR";
                else if (who_val == "3")
                    txtWho.Text = "BOTH";
                
                txtCecode.Text = job.Jobcod;
                txtDesc.Text = job.Wrkdesc;
                txtAddinfo.Text = job.Addinfo;
                lblTotEx.Text = job.Teqp.ToString();

                /*Build Cost table */
                List<DisplayCejob> dCejobList = new List<DisplayCejob>();

                /*Create first line of job */
                DisplayCejob dj1 = new DisplayCejob();
                dj1.Cost = "CONTRACTED COST";
                dj1.Sv1 = job.Con1;
                dj1.Fsv1 = job.Fcon1;
                dj1.Sv2 = job.Con2;
                dj1.Fsv2 = job.Fcon2;
                dj1.Sv3 = job.Con3;
                dj1.Fsv3 = job.Fcon3;
                dj1.Sv4 = job.Con4;
                dj1.Fsv4 = job.Fcon4;
                dj1.Psv = (pjob == null ? 0 : pjob.Con1);
                dj1.Fpsv = (pjob == null ? "B" : pjob.Fcon1);

                /*Create second line of job */
                DisplayCejob dj2 = new DisplayCejob();
                dj2.Cost = "PURCHASED MATERIALS";
                dj2.Sv1 = job.Amt1;
                dj2.Fsv1 = job.Famt1;
                dj2.Sv2 = job.Amt2;
                dj2.Fsv2 = job.Famt2;
                dj2.Sv3 = job.Amt3;
                dj2.Fsv3 = job.Famt3;
                dj2.Sv4 = job.Amt4;
                dj2.Fsv4 = job.Famt4;
                dj2.Psv = (pjob == null ? 0 : pjob.Amt1);
                dj2.Fpsv = (pjob == null ? "B" : pjob.Famt1);

                /*Create third line of job */
                DisplayCejob dj3 = new DisplayCejob();
                dj3.Cost = "RENTED MATERIALS";
                dj3.Sv1 = job.Ren1;
                dj3.Fsv1 = job.Fren1;
                dj3.Sv2 = job.Ren2;
                dj3.Fsv2 = job.Fren2;
                dj3.Sv3 = job.Ren3;
                dj3.Fsv3 = job.Fren3;
                dj3.Sv4 = job.Ren4;
                dj3.Fsv4 = job.Fren4;
                dj3.Psv = (pjob == null ? 0 : pjob.Ren1);
                dj3.Fpsv = (pjob == null ? "B" : pjob.Fren1);

                /*Create fourth line of job */
                finwt = cesamp.GetFinwtForInterview(selected_interview);

                /*Get previous finwt */
                float pft = cesamp.GetFinwtForInterview(pre_interview);

                DisplayCejob dj4 = new DisplayCejob();
                dj4.Cost = "WEIGHTED MONTHLY COST";
                dj4.Sv1 = Convert.ToInt64((job.Ren1 + job.Con1 + job.Amt1) * finwt);
                dj4.Fsv1 = "";
                dj4.Sv2 = Convert.ToInt64((job.Ren2 + job.Con2 + job.Amt2) * finwt);
                dj4.Fsv2 = "";
                dj4.Sv3 = Convert.ToInt64((job.Ren3 + job.Con3 + job.Amt3) * finwt);
                dj4.Fsv3 = "";
                dj4.Sv4 = Convert.ToInt64((job.Ren4 + job.Con4 + job.Amt4) * finwt);
                dj4.Fsv4 = "";
                dj4.Psv = (pjob == null ? 0 : Convert.ToInt32((pjob.Ren1 + pjob.Con1 + pjob.Amt1) * pft));
                dj4.Fpsv = "";

                dCejobList.Add(dj1);
                dCejobList.Add(dj2);
                dCejobList.Add(dj3);
                dCejobList.Add(dj4);

                var source = new BindingSource();
                source.DataSource = dCejobList;
                dgcost.DataSource = source;

                SetupCostGrid();

                /*Build Appliances table */
                List<DisplayAppliance> dappList = new List<DisplayAppliance>();

                /*Create first line of Applicance */
                DisplayAppliance da1 = new DisplayAppliance();
                da1.Seq = 1;
                da1.Code = job.Eqpcode1;
                da1.Expense = job.Eqp1;
                da1.Flag = job.Feqp1;

                DisplayAppliance da2 = new DisplayAppliance();
                da2.Seq = 2;
                da2.Code = job.Eqpcode2;
                da2.Expense = job.Eqp2;
                da2.Flag = job.Feqp2;

                DisplayAppliance da3 = new DisplayAppliance();
                da3.Seq = 3;
                da3.Code = job.Eqpcode3;
                da3.Expense = job.Eqp3;
                da3.Flag = job.Feqp3;

                DisplayAppliance da4 = new DisplayAppliance();
                da4.Seq = 4;
                da4.Code = job.Eqpcode4;
                da4.Expense = job.Eqp4;
                da4.Flag = job.Feqp4;

                DisplayAppliance da5 = new DisplayAppliance();
                da5.Seq = 5;
                da5.Code = job.Eqpcode5;
                da5.Expense = job.Eqp5;
                da5.Flag = job.Feqp5;

                DisplayAppliance da6 = new DisplayAppliance();
                da6.Seq = 6;
                da6.Code = job.Eqpcode6;
                da6.Expense = job.Eqp6;
                da6.Flag = job.Feqp6;

                dappList.Add(da1);
                dappList.Add(da2);
                dappList.Add(da3);
                dappList.Add(da4);
                dappList.Add(da5);
                dappList.Add(da6);

                dga.DataSource = dappList;
                SetupApplianceGrid();
            }
            else
            {
                txtDesc.Text = "";
                txtAddinfo.Text = "";
                txtTotal.Text = "";
                txtTcon.Text = "";
                txtTamt.Text = "";
                txtTren.Text = "";
                txtWho.Text = "";
                txtCecode.Text = "";

                dga.DataSource = null;
                dgcost.DataSource = null;
           
            }
            SetCeflags();
            SetupEditable();

        }

        private void SetCeflags()
        {
            /*Set ceflags for only current interview */
            if (cbInterview.SelectedItem.ToString() == cesamp.Interview)
            {
                if (cflags != null && cbJobcode.Text != "")
                {
                    List<string> desclist = new List<string>();
                    desclist = cflags.CeflagDescList(job.Jobidcode);
                    if (desclist.Count > 0)
                        listFlag.DataSource = desclist;
                    else
                        listFlag.DataSource = null;
                }
                else
                    listFlag.DataSource = null;
            }
            else
                listFlag.DataSource = null;

        }

        /*Setup form editable */
        private void SetupEditable()
        {
            if (!editable || !isCurrInterview)
            {
                btnDelete.Enabled = false;
                btnProcess.Enabled = false;
                btnRefresh.Enabled = false;
                BtnClear.Enabled = false;
          
                dga.EditMode = DataGridViewEditMode.EditProgrammatically;
                dgcost.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                btnDelete.Enabled = true;
                btnProcess.Enabled = true;
                btnRefresh.Enabled = true;
                BtnClear.Enabled = true;
                dga.EditMode = DataGridViewEditMode.EditOnEnter;
                dgcost.EditMode = DataGridViewEditMode.EditOnEnter;
            }

        }

        /*Setup dgcost column header */
        private string GetDisplayDate(string pinterview, int numcol)
        {
            if (pinterview == cesamp.Interview)
            {
                if (numcol == 1)
                    return cesamp.Survdate;
                else if (numcol == 2)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -1);
                else if (numcol == 3)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -2);
                else if (numcol == 4)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -3);
            }
            else if (Convert.ToInt32(pinterview) == Convert.ToInt32(cesamp.Interview) - 1)
            {
                if (numcol == 1)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -3);
                else if (numcol == 2)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -4);
                else if (numcol == 3)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -5);
                else if (numcol == 4)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -6);
            }
            else if (Convert.ToInt32(pinterview) == Convert.ToInt32(cesamp.Interview) - 2)
            {
                if (numcol == 1)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -6);
                else if (numcol == 2)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -7);
                else if (numcol == 3)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -8);
                else if (numcol == 4)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -9);
            }
            else if (Convert.ToInt32(pinterview) == Convert.ToInt32(cesamp.Interview) - 3)
            {
                if (numcol == 1)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -9);
                else if (numcol == 2)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -10);
                else if (numcol == 3)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -11);
                else if (numcol == 4)
                    return GetMonthDateFromCurrDate(cesamp.Survdate, -12);
            }

            return "";

        }

        /*obtain pervious date of the number of previous month */
        private string GetMonthDateFromCurrDate(string curr_date, int nummon)
        {
            DateTime currDate = DateTime.ParseExact(curr_date, "yyyyMM", new DateTimeFormatInfo());
            string preDate = currDate.AddMonths(nummon).ToString("yyyyMM");

            return preDate;
        }

        /*Set up dgcost grid */
        private void SetupCostGrid()
        {
            string selected_interview = cbInterview.SelectedItem.ToString();
            dgcost.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgcost.RowHeadersVisible = false; // set it to false if not needed

            for (int i = 0; i < dgcost.ColumnCount; i++)
            {

                if (i == 1 || i == 3 || i == 5 || i == 7 || i == 9)
                {
                    dgcost.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgcost.Columns[i].Width = 80;
                    dgcost.Columns[i].DefaultCellStyle.Format = "N0";
                    DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dgcost.Columns[i];//here index of column
                    cvip.MaxInputLength = 8;

                    if (i == 9)
                    {
                        dgcost.Columns[i].HeaderText = "PREVIOUS " + GetDisplayDate(selected_interview, (i - 1) / 2);
                        dgcost.Columns[i].ReadOnly = true;
                        if (pjob != null)
                        {
                            dgcost.Columns[i].DefaultCellStyle.BackColor = Color.Blue;
                            dgcost.Columns[i].DefaultCellStyle.ForeColor = Color.White;
                        }
                        else
                        {
                            dgcost.Columns[i].DefaultCellStyle.BackColor = Color.White;
                            dgcost.Columns[i].DefaultCellStyle.ForeColor = Color.Black;
                        }

                    }
                    else
                        dgcost.Columns[i].HeaderText = GetDisplayDate(selected_interview, (i + 1) / 2);

                }

                if (i == 0)
                {
                    dgcost.Columns[i].HeaderText = "COST";
                    dgcost.Columns[i].Width = 180;
                    dgcost.Columns[i].ReadOnly = true;

                }

                if (i == 2 || i == 4 || i == 6 || i == 8 || i == 10)
                {
                    dgcost.Columns[i].HeaderText = "";
                    dgcost.Columns[i].Width = 20;
                    dgcost.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgcost.Columns[i].HeaderText = "F";
                    if (i ==2 || i==4 || i==6|| i==8)
                        dgcost.Columns[i].ReadOnly = false;
                    else
                        dgcost.Columns[i].ReadOnly = true;
                }

            }

            //make row 3, read only
            dgcost.Rows[3].ReadOnly = true;

            this.dgcost.CellValidating += new DataGridViewCellValidatingEventHandler(dgcost_CellValidating);
            this.dgcost.CellEndEdit += new DataGridViewCellEventHandler(dgcost_CellEndEdit);
            this.dgcost.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgcost_EditingControlShowing);

        }

        private long oldvalue;
        private string oldflag;
        private bool isvalid;
        public void dgcost_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgcost.Columns[e.ColumnIndex].ReadOnly == false && e.RowIndex != 3)
            {
                isvalid = true;
            }
            else
            {
                isvalid = false;
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                oldvalue = Convert.ToInt64(dgcost[e.ColumnIndex, e.RowIndex].Value);
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 8)
                oldflag = dgcost[e.ColumnIndex, e.RowIndex].Value.ToString();          

        }


        public void dgcost_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!isvalid)
            {
                if (e.ColumnIndex == 1 || e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
                    dgcost[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                else
                    dgcost[e.ColumnIndex, e.RowIndex].Value = oldflag;

                return;
            }

            if (e.ColumnIndex == 1 || e.ColumnIndex == 3 || e.ColumnIndex == 5 || e.ColumnIndex == 7)
            {
                int newvalue =0;
                 try
                 {
                     newvalue = Convert.ToInt32(dgcost[e.ColumnIndex, e.RowIndex].Value);
                 }                     
                catch (OverflowException)
                {
                    MessageBox.Show("The number is too big.", "Error",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgcost[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                    return;
                }
                
                if (oldvalue != newvalue)
                {
                    if ((default_flag == "") || (dgcost[e.ColumnIndex, e.RowIndex].Value.ToString() == "0"))
                    {
                        frmCeflagSelPopup popup = new frmCeflagSelPopup(newvalue);
                        
                        DialogResult dialogresult = popup.ShowDialog();
                        if (dialogresult == DialogResult.OK)
                        {
                            dgcost[e.ColumnIndex + 1, e.RowIndex].Value = popup.selectedFlag;
                            if (popup.setDefault)
                            {
                                default_flag = popup.selectedFlag;
                                txtDflag.Text = default_flag;

                            }
                        }

                        popup.Dispose();
                    }
                    else
                    {
                        dgcost[e.ColumnIndex + 1, e.RowIndex].Value = default_flag;
                    }
                    //dgcost[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

                    string newflag = dgcost[e.ColumnIndex + 1, e.RowIndex].Value.ToString();

                    oldvalue = newvalue;
                    UpdateCejob(e.ColumnIndex, e.RowIndex, newvalue, newflag);

                    txtTotal.Text = job.Tcost > 0 ? job.Tcost.ToString("#,#") : job.Tcost.ToString();
                    txtTcon.Text = job.Tcon > 0 ? job.Tcon.ToString("#,#") : job.Tcon.ToString();
                    txtTamt.Text = job.Tamt > 0 ? job.Tamt.ToString("#,#") : job.Tamt.ToString();
                    txtTren.Text = job.Tren > 0 ? job.Tren.ToString("#,#") : job.Tren.ToString();
                    txtValuejobs.Text = jobs.GetJobsValueForInterview(cbInterview.SelectedItem.ToString()).ToString("#,#");

                    dgcost[e.ColumnIndex, 3].Value = finwt * (Convert.ToInt32(dgcost[e.ColumnIndex, 0].Value) + Convert.ToInt32(dgcost[e.ColumnIndex, 1].Value) + Convert.ToInt32(dgcost[e.ColumnIndex, 2].Value));
                  
                }
            }
            else if (e.ColumnIndex == 2 || e.ColumnIndex == 4 || e.ColumnIndex == 6 || e.ColumnIndex == 8)
            {
                string new_flag = dgcost[e.ColumnIndex, e.RowIndex].Value.ToString();
                if (oldflag != new_flag)
                {
                    if (new_flag != "A" && new_flag != "B")
                    {
                        MessageBox.Show("Flag must be in (A, B)");
                        dgcost[e.ColumnIndex, e.RowIndex].Value = oldflag;
                    }
                    else
                        UpdateCostFlag(e.ColumnIndex, e.RowIndex, new_flag);
                }

            }
        }

        private static KeyPressEventHandler NumbericCheckHandler = new KeyPressEventHandler(NumbericCheck);
        private static void NumbericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.';
        }
        private void dgcost_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if ((dgcost.CurrentCell.ColumnIndex == 1) || (dgcost.CurrentCell.ColumnIndex == 3) || (dgcost.CurrentCell.ColumnIndex == 5) || (dgcost.CurrentCell.ColumnIndex == 7))
            {
                e.Control.KeyPress -= NumbericCheckHandler;
                e.Control.KeyPress += NumbericCheckHandler;
            }

        }

        private void SetupApplianceGrid()
        {
            dga.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dga.RowHeadersVisible = false; // set it to false if not needed

            for (int i = 0; i < dga.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dga.Columns[i].HeaderText = "NO.";
                    dga.Columns[i].ReadOnly = true;
                    dga.Columns[i].Width = 40;
                }
                if (i == 1)
                {
                    dga.Columns[i].HeaderText = "CODE";
                    dga.Columns[i].ReadOnly = true;
                    dga.Columns[i].Width = 60;
                }
                if (i == 2)
                {
                    dga.Columns[i].HeaderText = "EXPENSE";
                    dga.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dga.Columns[i].Width = 100;
                    dga.Columns[i].DefaultCellStyle.Format = "N0";
                    dga.Columns[i].ReadOnly = true;
                    dga.Columns[i].Visible = false;
                }
                if (i == 3)
                {
                    dga.Columns[i].HeaderText = "F";
                    dga.Columns[i].Width = 20;
                    dga.Columns[i].ReadOnly = true;
                    dga.Columns[i].Visible = false;
                }

            }
           
            this.dga.CellValidating += new DataGridViewCellValidatingEventHandler(dga_CellValidating);
            this.dga.CellEndEdit += new DataGridViewCellEventHandler(dga_CellEndEdit);
            this.dga.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dga_EditingControlShowing);

        }


        public void dga_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //isvalid = false;
            //if (dga.Columns[e.ColumnIndex].ReadOnly == false)
            //{
            //    if (e.ColumnIndex == 2)
            //    { 
            //        oldvalue = Convert.ToInt32(dga[e.ColumnIndex, e.RowIndex].Value);
            //    //else if (e.ColumnIndex == 3)
            //    //    oldflag = dga[e.ColumnIndex, e.RowIndex].Value.ToString();

            //        isvalid = true;
            //    }
            //}      

        }
        
        public void dga_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //bool delapp = false;
            //if (!isvalid)
            //{
            //    if (e.ColumnIndex == 2)
            //        dga[e.ColumnIndex, e.RowIndex].Value = oldvalue;
            //    else if (e.ColumnIndex == 3)
            //        dga[e.ColumnIndex, e.RowIndex].Value = oldflag;
            //    return;
            //}

            //if (e.ColumnIndex == 2)
            //{
            //    if (oldvalue != Convert.ToInt32(dga[e.ColumnIndex, e.RowIndex].Value))
            //    {
            //        int newvalue = Convert.ToInt32(dga[e.ColumnIndex, e.RowIndex].Value);
            //        if ((default_flag == "") || (dga[e.ColumnIndex, e.RowIndex].Value.ToString() == "0"))
            //        {
            //            frmCeflagSelPopup popup = new frmCeflagSelPopup(newvalue);

            //            DialogResult dialogresult = popup.ShowDialog();
            //            if (dialogresult == DialogResult.OK)
            //            {
            //                dga[e.ColumnIndex + 1, e.RowIndex].Value = popup.selectedFlag;
            //                if (popup.selectedFlag == "B")
            //                {
            //                    dga[e.ColumnIndex - 1, e.RowIndex].Value = "";
            //                    dga[2, e.RowIndex].ReadOnly = true;
            //                }
            //                if (popup.setDefault)
            //                {
            //                    default_flag = popup.selectedFlag;
            //                    txtDflag.Text = default_flag;                             
            //                }
            //            }

            //            popup.Dispose();
            //        }
            //        else
            //        {
            //            dga[e.ColumnIndex + 1, e.RowIndex].Value = default_flag;
            //        }

            //        //dga[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;

            //        string newflag = dga[e.ColumnIndex + 1, e.RowIndex].Value.ToString();
                    
            //        UpdateAppliance(e.RowIndex, newvalue, newflag, delapp);

            //        oldvalue = Convert.ToInt32(dga[e.ColumnIndex, e.RowIndex].Value);
            //    }
           // }
            //else if (e.ColumnIndex == 3 && oldflag != dga[e.ColumnIndex, e.RowIndex].Value.ToString())
            //{
            //    string new_flag = dga[e.ColumnIndex, e.RowIndex].Value.ToString();
            //    if (new_flag != "A" && new_flag != "B")
            //    {
            //        MessageBox.Show("Flag must be in (A, B)");
            //        dga[e.ColumnIndex, e.RowIndex].Value = oldflag;
            //    }
            //    else
            //        UpdateApplianceFlag(e.RowIndex, new_flag);

            //}
        }

        private void dga_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dga.CurrentCell.ColumnIndex == 2)
            {
                e.Control.KeyPress -= NumbericCheckHandler;
                e.Control.KeyPress += NumbericCheckHandler;
            }

        }

        /*Add ceaudit data */
        private void AddCeauditData(string avarnme, int aoldval, string aoldflag, int anewval, string anewflag)
        {
            /*Get audit record from list */
            Ceaudit au = (from Ceaudit j in Ceauditlist
                          where j.Interview == cbInterview.SelectedItem.ToString() && j.Jobidcode == cbJobcode.SelectedItem.ToString()
                          && j.Varnme == avarnme 
                        select j).SingleOrDefault();

            /*if there is no record, add one, otherwise update the record */
            if (au == null)
            {
                Ceaudit ca = new Ceaudit();
                ca.Id = Id;
                ca.Interview = cbInterview.SelectedItem.ToString();
                ca.Jobidcode = cbJobcode.SelectedItem.ToString();
                ca.Varnme = avarnme;
                ca.Oldval = aoldval;
                ca.Oldflag = aoldflag;
                ca.Newval = anewval;
                ca.Newflag = anewflag;
                ca.Usrnme = UserInfo.UserName;
                ca.Progdtm = DateTime.Now;

                Ceauditlist.Add(ca);
            }
            else
            {
                au.Newval = anewval;
                au.Newflag = anewflag;
                au.Progdtm = DateTime.Now;
            }

        }

        /*Update cejob */
        private void UpdateCejob(int col, int row, int newval, string newflag)
        {
            if (col == 1)
            {
                if (row == 0)
                {
                    AddCeauditData("CON1", job.Con1, job.Fcon1, newval, newflag);
                    job.Con1 = newval;
                    job.Fcon1 = newflag;                  
                }
                else if (row == 1)
                {
                    AddCeauditData("AMT1", job.Amt1, job.Famt1, newval, newflag);
                    job.Amt1 = newval;
                    job.Famt1 = newflag;
                }
                else if (row == 2)
                {
                    AddCeauditData("REN1", job.Ren1, job.Fren1, newval, newflag);
                    job.Ren1 = newval;
                    job.Fren1 = newflag;
                }
            }

            if (col == 3)
            {
                if (row == 0)
                {
                    AddCeauditData("CON2", job.Con2, job.Fcon2, newval, newflag);
                    job.Con2 = newval;
                    job.Fcon2 = newflag;
                }
                else if (row == 1)
                {
                    AddCeauditData("AMT2", job.Amt2, job.Famt2, newval, newflag);
                    job.Amt2 = newval;
                    job.Famt2 = newflag;
                }
                else if (row == 2)
                {
                    AddCeauditData("REN2", job.Ren2, job.Fren2, newval, newflag);
                    job.Ren2 = newval;
                    job.Fren2 = newflag;
                }
            }

            if (col == 5)
            {
                if (row == 0)
                {
                    AddCeauditData("CON3", job.Con3, job.Fcon3, newval, newflag);
                    job.Con3 = newval;
                    job.Fcon3 = newflag;
                }
                else if (row == 1)
                {
                    AddCeauditData("AMT3", job.Amt3, job.Famt3, newval, newflag);
                    job.Amt3 = newval;
                    job.Famt3 = newflag;
                }
                else if (row == 2)
                {
                    AddCeauditData("REN3", job.Ren3, job.Fren3, newval, newflag);
                    job.Ren3 = newval;
                    job.Fren3 = newflag;
                }
            }

            if (col == 7)
            {
                if (row == 0)
                {
                    AddCeauditData("CON4", job.Con4, job.Fcon4, newval, newflag);
                    job.Con4 = newval;
                    job.Fcon4 = newflag;
                }
                else if (row == 1)
                {
                    AddCeauditData("AMT4", job.Amt4, job.Famt4, newval, newflag);
                    job.Amt4 = newval;
                    job.Famt4 = newflag;
                }
                else if (row == 2)
                {
                    AddCeauditData("REN4", job.Ren4, job.Fren4, newval, newflag);
                    job.Ren4 = newval;
                    job.Fren4 = newflag;
                }
            }

        }

        /*Update applicance */
        private void UpdateAppliance(int row, int newval, string newflag="", bool delapp = false)
        {
            if (delapp)
            {
                if (row == 0)
                {
                    AddCeauditData("EQP1", job.Eqp1, job.Feqp1, 0, newflag);
                    job.Eqpcode1 = "";
                    job.Eqp1 = 0;
                    job.Feqp1 = "";
                }
                else if (row == 1)
                {
                    AddCeauditData("EQP2", job.Eqp2, job.Feqp2, 0, newflag);
                    job.Eqpcode2 = "";
                    job.Eqp2 = 0;
                    job.Feqp2 = "";
                }
                else if (row == 2)
                {
                    AddCeauditData("EQP3", job.Eqp3, job.Feqp3, 0, newflag);
                    job.Eqpcode3 = "";
                    job.Eqp3 = 0;
                    job.Feqp3 = "";
                }
                else if (row == 3)
                {
                    AddCeauditData("EQP4", job.Eqp4, job.Feqp4, 0, newflag);
                    job.Eqpcode4 = "";
                    job.Eqp4 = 0;
                    job.Feqp4 = "";
                }
                else if (row == 4)
                {
                    AddCeauditData("EQP5", job.Eqp5, job.Feqp5, 0, newflag);
                    job.Eqpcode5 = "";
                    job.Eqp5 = 0;
                    job.Feqp5 = "";
                }
                else
                {
                    AddCeauditData("EQP6", job.Eqp6, job.Feqp6, 0, newflag);
                    job.Eqpcode6 = "";
                    job.Eqp6 = 0;
                    job.Feqp6 = "";
                }

                return;
            }

            if (row == 0)
            {
                AddCeauditData("EQP1", job.Eqp1, job.Feqp1, newval, newflag);
                job.Eqp1 = newval;
                job.Feqp1 = newflag;
            }
            else if (row == 1)
            {
                AddCeauditData("EQP2", job.Eqp2, job.Feqp2, newval, newflag);
                job.Eqp2 = newval;
                job.Feqp2 = newflag;
            }
            else if (row == 2)
            {
                AddCeauditData("EQP1", job.Eqp3, job.Feqp3, newval, newflag);
                job.Eqp3 = newval;
                job.Feqp3 = newflag;
            }
            else if (row == 3)
            {
                AddCeauditData("EQP1", job.Eqp4, job.Feqp4, newval, newflag);
                job.Eqp4 = newval;
                job.Feqp4 = newflag;
            }
            else if (row == 4)
            {
                AddCeauditData("EQP5", job.Eqp5, job.Feqp5, newval, newflag);
                job.Eqp5 = newval;
                job.Feqp5 = newflag;
            }
            else
            {
                AddCeauditData("EQP6", job.Eqp6, job.Feqp6, newval, newflag);
                job.Eqp6 = newval;
                job.Feqp6 = newflag;
            }

        }

        /*Update cost Flag*/
        private void UpdateCostFlag(int col, int row, string newflag)
        {
             if (col == 2)
            {
                if (row == 0)
                {
                    job.Fcon1 = newflag;                  
                }
                else if (row == 1)
                {
                    job.Famt1 = newflag;
                }
                else if (row == 2)
                {
                    job.Fren1 = newflag;
                }
            }

            if (col == 4)
            {
                if (row == 0)
                {
                    job.Fcon2 = newflag;
                }
                else if (row == 1)
                {
                    job.Famt2 = newflag;
                }
                else if (row == 2)
                {
                    job.Fren2 = newflag;
                }
            }

            if (col == 6)
            {
                if (row == 0)
                {
                    job.Fcon3 = newflag;
                }
                else if (row == 1)
                {
                    job.Famt3 = newflag;
                }
                else if (row == 2)
                {
                    job.Fren3 = newflag;
                }
            }

            if (col == 8)
            {
                if (row == 0)
                {                 
                    job.Fcon4 = newflag;
                }
                else if (row == 1)
                {
                    job.Famt4 = newflag;
                }
                else if (row == 2)
                {
                    job.Fren4 = newflag;
                }
            }

            job.Dflag = Dirty.modify;
        }

        /*Update applicance Flag*/
        private void UpdateApplianceFlag(int row, string newflag)
        {
            if (row == 0)           
                job.Feqp1 = newflag;           
            else if (row == 1)           
                job.Feqp2 = newflag;            
            else if (row == 2)            
                job.Feqp3 = newflag;            
            else if (row == 3)        
                job.Feqp4 = newflag;
            else if (row == 4)
                job.Feqp5 = newflag;
            else
                job.Feqp6 = newflag;

            job.Dflag = Dirty.modify;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            /*Save data */
            SaveData();

            MessageBox.Show("Data has been saved succefully!");

            /*Reload form */
            LoadForm(false);
        }

        private void SaveData()
        {
            /*update edit field in cesample table if cejobs have been updated */
            if (jobs.IsModified())
            {
                cesampdata.UpdateEdit(Id);

                /*Save jobs */
                jobsdata.SaveCejobs(jobs);

                /*Reload jobs */
                jobs = jobsdata.GetCejobs(Id);

                /*load flag info */
                cflags = new Ceflags(jobs, cesamp.Interview, cesamp.Propval, cesamp.Finwt);
                
                /*Save ceflags */
                cflagsData.SaveCeflagsData(Id, cflags.GetMainCeflags());

                /*Save ceaudit data, clear Ceauditlist */
                if (Ceauditlist.Count > 0)
                {
                    cadata = new CeauditData();
                    foreach (Ceaudit element in Ceauditlist)
                    {
                        cadata.AddCeauditData(element);
                    }
                    Ceauditlist.Clear();
                }

                data_saved = true;
                
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadForm(false);
            txtDflag.Text = "";
            default_flag = "";
            
        }


        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the current job?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                //set job flag
                job.Dflag = Dirty.delete;

                //Delete record in ceauditlist
                Ceauditlist.RemoveAll(x => ((x.Id == Id) && (x.Interview == cbInterview.SelectedItem.ToString()) && (x.Jobidcode == cbJobcode.SelectedItem.ToString())));

                //Add record to ceauditlist
                AddCeauditData("CON1", job.Con1, job.Fcon1, 0, "D");
                AddCeauditData("AMT1", job.Amt1, job.Famt1, 0, "D");
                AddCeauditData("REN1", job.Ren1, job.Fren1, 0, "D");
                AddCeauditData("CON2", job.Con2, job.Fcon2, 0, "D");
                AddCeauditData("AMT2", job.Amt2, job.Famt2, 0, "D");
                AddCeauditData("REN2", job.Ren2, job.Fren2, 0, "D");
                AddCeauditData("CON3", job.Con3, job.Fcon3, 0, "D");
                AddCeauditData("AMT3", job.Amt3, job.Famt3, 0, "D");
                AddCeauditData("REN3", job.Ren3, job.Fren3, 0, "D");
                AddCeauditData("CON4", job.Con4, job.Fcon4, 0, "D");
                AddCeauditData("AMT4", job.Amt4, job.Famt4, 0, "D");
                AddCeauditData("REN4", job.Ren4, job.Fren4, 0, "D");

                //Reset job
                DisplayJobsAll();    
            }           
            
        }

        private void btnNextCase_Click(object sender, EventArgs e)
        {
            if (Idlist == null)
            {
                if (jobs.IsModified())
                {
                    DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        SaveData();
                    }

                }

                if (data_saved)
                    caccessdata.AddCeaccessData(Id, "UPDATE");
                else
                    caccessdata.AddCeaccessData(Id, "BROWSE");

                frmIdPopup popup = new frmIdPopup();
                popup.CallingForm = this;
                popup.StartPosition = FormStartPosition.CenterParent;
                DialogResult dialogresult = popup.ShowDialog();
                if (dialogresult == DialogResult.OK)
                {
                    /*unlock cesample */
                    if (editable)
                    {
                        bool locked = cesampdata.UpdateLock(Id, "");
                    }

                    Id = popup.NewId;

                    LoadForm();
                    DisplayForm();
                }

            }
            else
            {
                if (CurrIndex == Idlist.Count - 1)
                {
                    MessageBox.Show("You are at the last observation");
                }
                else
                {
                    if (jobs.IsModified())
                    {
                        DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result2 == DialogResult.Yes)
                        {
                            SaveData();
                        }

                    }

                    if (data_saved)
                        caccessdata.AddCeaccessData(Id, "UPDATE");
                    else
                        caccessdata.AddCeaccessData(Id, "BROWSE");

                    /*unlock cesample */
                    if (editable)
                    {
                        bool locked = cesampdata.UpdateLock(Id, "");
                    }

                    CurrIndex = CurrIndex + 1;
                    Id = Idlist[CurrIndex];

                    LoadForm();
                    DisplayForm();
                }

            }
        }

        private void btnPrevCase_Click(object sender, EventArgs e)
        {
            if (CurrIndex == 0)
            {
                MessageBox.Show("You are at the first observation");
            }
            else
            {
                if (jobs.IsModified())
                {
                    DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }

                if (data_saved)
                    caccessdata.AddCeaccessData(Id, "UPDATE");
                else
                    caccessdata.AddCeaccessData(Id, "BROWSE");

                /*unlock cesample */
                if (editable)
                {
                    bool locked = cesampdata.UpdateLock(Id, "");
                }

                CurrIndex = CurrIndex - 1;
                Id = Idlist[CurrIndex];
                LoadForm();
                DisplayForm();
            }

        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            frmCeHistoryPopup fm = new frmCeHistoryPopup(Id);
            fm.StartPosition = FormStartPosition.CenterParent;
            fm.ShowDialog();

            /*retrieve comment data in case changed */
            cecom = cecomdata.GetCecommentData(Id);
            if (cecom.Cecommentlist.Count() != 0)
            {
                dgComments.DataSource = cecom.Cecommentlist;
                if (tabControl1.SelectedIndex == 0)
                {
                    dgComments.Columns[0].Width = 60;
                    dgComments.Columns[1].Visible = false;
                    dgComments.Columns[2].Width = 60;
                    dgComments.ColumnHeadersVisible = false;
                }  
            }

        }

        private void frmImprovements_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (!call_callingFrom)
            {
                if (jobs.IsModified())
                {
                    DialogResult result3 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result3 == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }
            }

            if (data_saved)
                caccessdata.AddCeaccessData(Id, "UPDATE");
            else
                caccessdata.AddCeaccessData(Id, "BROWSE");

            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");

            /*unlock cesample */
            if (editable)
            {
                bool locked = cesampdata.UpdateLock(Id, "");
            }

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
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

        private void btnMark_Click(object sender, EventArgs e)
        {
            frmCeMarkcasesPopup popup = new frmCeMarkcasesPopup();
            popup.Id = Id;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
          
            if (CheckCeMarkExists())
                lblMark.Text = "MARKED";
            else
                lblMark.Text = "";

        }

        //check mark notes exists or not
        private bool CheckCeMarkExists()
        {
            bool mark_exist = false;
            DataTable dtData = new DataTable();
            CemarkData cmd = new CemarkData();
            dtData = cmd.GetCemarklist(Id);
            if (dtData != null && dtData.Rows.Count > 0)
                mark_exist = true;
            
            return mark_exist;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (default_flag == "")
            {
                MessageBox.Show("There is no default flag to delete. ");
                return;
            }
            else
            {
               default_flag = "";
               txtDflag.Text = "";
            }
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (jobs.IsModified())
            {
                DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                {
                    SaveData();
                }
            }

            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }
            this.Close();
        }

        private void btnAduit_Click(object sender, EventArgs e)
        {
            frmCeProjectAuditPopup fca = new frmCeProjectAuditPopup(Id);
            fca.StartPosition = FormStartPosition.CenterParent;
            fca.ShowDialog();
                       
        }

        private void btnHelpApp_Click(object sender, EventArgs e)
        {
            frmCehelpsPopup popup = new frmCehelpsPopup(2);
        
            //Point location = new Point(500, 280);
            //popup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            //popup.Location = location;

            popup.ShowDialog();   
        }

        private void btnHelpState_Click(object sender, EventArgs e)
        {
            frmCehelpsPopup popup = new frmCehelpsPopup(3);

            //Point location = new Point(500, 200);
            //popup.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            //popup.Location = location;

            popup.ShowDialog();   
        }

        private void btnHelpCode_Click(object sender, EventArgs e)
        {
            frmCehelpsPopup popup = new frmCehelpsPopup(1);

            popup.ShowDialog();   
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void btnReferral_Click(object sender, EventArgs e)
        {
            frmImpReferral fImpReferral = new frmImpReferral();

            fImpReferral.Id = txtId.Text;

            fImpReferral.ShowDialog();  //show child

            lblReferral.Visible = impreferralData.CheckReferralExist(txtId.Text);
            
        }

        private void txtValuejobs_TextChanged(object sender, EventArgs e)
        {

        }

        private void dga_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //if (dga.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            //{
            //    e.Cancel = true;
            //}
        }

        private void dgcost_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgcost.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        private void dga_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.ColumnIndex == 2 && dga[e.ColumnIndex - 1, e.RowIndex].Value.ToString().Trim() == "")
            //{
            //    dga[e.ColumnIndex, e.RowIndex].ReadOnly = true;
                
            //}
        }

        private void btnHelpJob_Click(object sender, EventArgs e)
        {
            frmCehelpsPopup popup = new frmCehelpsPopup(4);

            popup.ShowDialog();
        }

        private void btnDelete_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnDelete.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnDelete.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnProcess_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnProcess.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnProcess.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnRefresh_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnRefresh.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnRefresh.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnHist_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnHist.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnHist.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }
    }  
}
