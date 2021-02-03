/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmTFU.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/20/2015
Inputs:             RespId, callingform
               
Parameters:		    Respid, callingform                
Outputs:		    None

Description:	    This screen display a list of cases for a respid,
                    displays/edit the Sample, Master, Soc, respondent,
                    cprsflags, sched_call
                    and monthly vip data for a selected case

Detailed Design:    Detailed User Requirements 

Other:	            
 
Revision History:	
***********************************************************************************
 Modified Date :  3/26/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  3026
 Description   :  when reschedule, add appt date to last call resolution
***********************************************************************************
 Modified Date :  3/27/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  
 Description   :  fix the issue of vip satisfied if the lag exists
 ***********************************************************************************
 Modified Date :  6/22/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR3250
 Description:    Change callback for LVM from the next work day to three work days
 ***********************************************************************************
 Modified Date :  9/19/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR3570
 Description:    Add selection date and remove county
 ***********************************************************************************
 Modified Date :  01/09/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR3839
 Description   : for Utilities check newtc and owner
***********************************************************************************
 Modified Date :  04/20/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : bug fix if change strtdate, vip before strtdate wasn't deleted if
                 vip=0 and vipflag = 'B'
***********************************************************************************
 Modified Date :  04/27/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : bug fix if vipdatar in compdate is 0, don't allow add compdate
************************************************************************************
 Modified Date :  09/17/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : for call stat=8, save all cases's sched call information
************************************************************************************
 Modified Date :  12/7/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : update oadj and oflg field during save data
************************************************************************************
 Modified Date :  12/29/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : fix two saving issues: Save to hold table if report reject is false but reject 
                 is true; skip saving sched data if in NPC and call stat is V in one of cases with same respid
************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Linq;
using System.Globalization;
using System.Drawing.Printing;
using DGVPrinterHelper;
//using System.Linq.Dynamic;

namespace Cprs
{
    public partial class frmTfu : Cprs.frmCprsParent
    {
        /*********************** Public Properties  *******************/
        /* Required */
        public string RespId = "0000000";
        
        /*Optional*/
        public Form CallingForm = null;
        public bool Editable = true;

        /***************************************************************/

        private TypeTFUEntryPoint enterPoint;

        /*Form global variable */
        private Sample samp;
        private SampleData sampdata;
        private Master mast;
        private MasterData mastdata;
        private Soc soc;
        private SocData socdata;
        private Respondent resp;
        private RespondentData respdata;
        private TFUData tfudata;
        private MonthlyVipsData mvsdata;
        private MonthlyVips mvs;
        private DataTable pdata;
        private DataTable rdata;
        private ReferralData refdata;
        private bool referral_exist = false;
        private CsdaccessData csda;
        private SchedCallData scheddata;
        private Schedcall schedcall;

        private ProjMark pmark;
        private ProjMarkData pmarkdata;
        private RespMark rmark;
        private RespMarkData rmarkdata;
        private List<ProjMark> pmarklist = new List<ProjMark>();
        private List<RespMark> rmarklist = new List<RespMark>();

        private List<Cpraudit> cprauditlist = new List<Cpraudit>();
        private ProjectAuditData auditData;
        private List<Vipaudit> vipauditlist = new List<Vipaudit>();
        private List<Respaudit> Respauditlist = new List<Respaudit>();

        private CprflagsData flagdata;
        private Cprflags cflags;
        private HistoryData hd;

        private List<TFUProject> projectlist;
        private List<TFUProject> projectalllist;
        private string id;
        private TypeDBSource dbsource = TypeDBSource.Default;

        private string locked_by = string.Empty;
        private bool editable = true;

        //flag to check the data has been saved or not
        private bool data_saved = false;

        //flag to check the sched_call data has been saved or not
        private bool sched_data_saved = false;

        /*flag to use closing the form */
        private bool call_callingFrom = false;

        private string currYearMon = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2");

        private string old_text;
        private delegate void ShowLockMessageDelegate();
        private delegate void ShowAllCompleteDelegate();

        private delegate void PrintScreenDelegate();

        private bool formloading = false;

        private bool lag_changed = false;

        private int owner_changed = 0;
        private bool status_changed = false;
        private string rst = string.Empty;

        /*Set up flags */
        private bool reject = false;

        private float old_fwgt = 0;

        private List<string> finished_cases;

        /*time factor for the respid */
        private int time_factor = 0;

        private int cut_day = 0;

        public frmTfu()
        {
            InitializeComponent();
        }

        private void frmTfu_Load(object sender, EventArgs e)
        {
            if (CallingForm != null)
                enterPoint = TypeTFUEntryPoint.SEARCH;

            //invisible flag labels
            lblLock.Visible = false;
            lblMark.Visible = false;
            lblReferral.Visible = false;
            lblReject.Visible = false;

            if (RespId != "0000000")
            {
                LoadForm();
                DisplayForm();

                //add record to cpraccess
                if (enterPoint == TypeTFUEntryPoint.SEARCH)
                {
                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
                    GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");
                }
            }
            else
            {
                editable = false;
                SetupEditable();
                SetupProjectEditable(editable);
                GeneralDataFuctions.UpdateCurrentUsersData("TFU");
                GeneralDataFuctions.AddCpraccessData("TFU", "ENTER");
            }

            int mon = DateTime.Now.Month;
            int year = DateTime.Now.Year;

            UnavailableDaysData undata = new UnavailableDaysData();
            cut_day = Convert.ToInt32(undata.GetUnavailableCutDay(year.ToString(), mon.ToString("00")));
        }

        private void LoadForm(bool from_refresh = false, bool fromTFU = false)
        {
            formloading = true;

            /*Respondent */
            respdata = new RespondentData();
            resp = respdata.GetRespondentData(RespId);
            time_factor = GeneralDataFuctions.GetTimezoneFactor(resp.Rstate);

            tfudata = new TFUData();
            auditData = new ProjectAuditData();

            /*lock Respondent */
            if (!Editable)
            {
                editable = false;
                lblLock.Visible = true;
            }
            else if (!from_refresh)
            {
                locked_by = GeneralDataFuctions.ChkRespIDIsLocked(RespId);
                if (String.IsNullOrEmpty(locked_by))
                {
                    bool locked = GeneralDataFuctions.UpdateRespIDLock(RespId, UserInfo.UserName);
                    editable = true;
                    lblLock.Visible = false;
                }
                else
                {
                    editable = false;
                    lblLock.Visible = true;
                }

                
            }
            else if (fromTFU)
            {
                editable = true;
                lblLock.Visible = false;
            }


            //rebuild resolution combo
            DataTable dtblDataSource = new DataTable();
            dtblDataSource.Columns.Add("Text");
            dtblDataSource.Columns.Add("Value");
            // dtblDataSource.Rows.Add("", -1);
            dtblDataSource.Rows.Add("0-Referral", 0);
            dtblDataSource.Rows.Add("1-Ring", 1);
            dtblDataSource.Rows.Add("2-Busy", 2);
            dtblDataSource.Rows.Add("3-Disconnected", 3);
            dtblDataSource.Rows.Add("4-Refusal", 4);
            dtblDataSource.Rows.Add("5-Reschedule", 5);
            dtblDataSource.Rows.Add("6-LVM", 6);
            dtblDataSource.Rows.Add("7-Promise to Report", 7);
            dtblDataSource.Rows.Add("8-New POC", 8);
            dtblDataSource.Rows.Add("9-Contacted", 9);

            cbCall.DataSource = null;
            cbCall.DataSource = dtblDataSource;
            cbCall.DisplayMember = "Text";
            cbCall.ValueMember = "Value";
            cbCall.SelectedIndex = -1;
            txtcall.Text = "";

            hd = new HistoryData();

            Respauditlist.Clear();

            id = null;
            data_saved = false;
            sched_data_saved = false;

            finished_cases = new List<string>();
        }

        //get project list data
        private List<TFUProject> GetProjectList()
        {
            projectalllist = tfudata.GetTFUProjectsForRespid(RespId);

            if (enterPoint == TypeTFUEntryPoint.FORM)
            {
                IEnumerable<TFUProject> projectlist1 = from j in projectalllist
                                                       where j.Compdate == "" && (j.Status == "1" || j.Status == "2" || j.Status == "3" || j.Status == "7" || j.Status == "8")
                                                       orderby j.Id
                                                       select j;
                projectlist = projectlist1.ToList();
            }
            else if (enterPoint == TypeTFUEntryPoint.SEARCH)
            {
                IEnumerable<TFUProject> projectlist1 = from j in projectalllist
                                                       where j.Compdate == "" && (j.Status == "1" || j.Status == "4")
                                                       orderby j.Id
                                                       select j;
                projectlist = projectlist1.ToList();
            }
            else if (enterPoint == TypeTFUEntryPoint.NPC)
            {
                IEnumerable<TFUProject> projectlist1 = from j in projectalllist
                                                       where j.Callreq != null
                                                       orderby j.Priority, id
                                                       select j;
                projectlist = projectlist1.ToList();
                
            }

            return projectlist;
        }

        private void DisplayRespondentData()
        {
            txtRespid.Text = RespId;

            txtColHist.Text = resp.Colhist;
            cbColtec.SelectedItem = GetDisplayColtecText(resp.Coltec);
            txtColtec.Text = cbColtec.Text;

            txtRespname.Text = resp.Respname;
            txtRespname2.Text = resp.Respname2;

            txtRespOrg.Text = resp.Resporg;
            txtFactOff.Text = resp.Factoff;
            txtOthrResp.Text = resp.Othrresp;
            txtRespnote.Text = resp.Respnote;

            txtAddr1.Text = resp.Addr1;
            txtAddr2.Text = resp.Addr2;
            txtAddr3.Text = resp.Addr3;

            txtZip.Text = resp.Zip;
            txtEmail.Text = resp.Email;
            txtWebAddr.Text = resp.Weburl;
            txtFax.Text = resp.Fax;
            txtExt.Text = resp.Ext;
            txtPhone.Text = resp.Phone;
            txtPhone2.Text = resp.Phone2;
            txtExt2.Text = resp.Ext2;

            txtZone.Text = GeneralDataFuctions.GetTimezone(resp.Rstate);
            txtTime.Text = GeneralDataFuctions.GetTimezoneCurrentTime(resp.Rstate);

            cbLag.SelectedItem = resp.Lag.ToString();
            txtLag.Text = cbLag.Text;
        }

        private void SetupRespondentEditable(bool editable)
        {
            txtRespid.ReadOnly = editable;

            txtColHist.ReadOnly = true;
            txtLag.ReadOnly = true;

            txtRespname.ReadOnly = editable;
            txtRespname2.ReadOnly = editable;

            txtRespOrg.ReadOnly = editable;
            txtFactOff.ReadOnly = editable;
            txtOthrResp.ReadOnly = editable;
            txtRespnote.ReadOnly = editable;

            txtAddr1.ReadOnly = editable;
            txtAddr2.ReadOnly = editable;
            txtAddr3.ReadOnly = editable;

            txtZip.ReadOnly = editable;
            txtEmail.ReadOnly = editable;
            txtWebAddr.ReadOnly = editable;
            txtExt.ReadOnly = editable;
            txtPhone.ReadOnly = editable;
            txtPhone2.ReadOnly = editable;
            txtExt2.ReadOnly = editable;

            txtZone.Text = GeneralDataFuctions.GetTimezone(resp.Rstate);
            txtTime.Text = GeneralDataFuctions.GetTimezoneCurrentTime(resp.Rstate);

            cbLag.SelectedItem = resp.Lag.ToString();
            if (!editable)
            {
                cbCall.Visible = false;
                txtcall.Visible = true;
                txtLag.Visible = true;
                cbLag.Visible = false;
            }
            else
            {
                cbLag.Visible = true;
                txtLag.Visible = false;
                cbCall.Visible = true;
                txtcall.Visible = false;
            }
        }

        //display respondent
        private void DisplayForm()
        {
            DisplayRespondentData();

            projectlist = GetProjectList();
            if (projectlist.Count > 0)
            {
                DisplayProjectGrid(projectlist);
                txtTotProjects.Text = dgProject.Rows.Count.ToString();

                //headlight frist cell
                dgProject.CurrentCell = dgProject[0, 0];
                id = dgProject.SelectedRows[0].Cells[0].FormattedValue.ToString();

                // Update the project grid to reflect changes to the selection.
                LoadProjectData();
                DisplayProject();
            }
            else
            {
                dgProject.DataSource = null;
                
                id = null;
                txtTotProjects.Text = "0";
                
                SetupProjectEditable(false);
                ResetProject();
            }

            if ((enterPoint == TypeTFUEntryPoint.SEARCH || enterPoint == TypeTFUEntryPoint.FORM) && projectalllist.Count > 0)
            {
                btnAll.Visible = true;
                groupBox3.Text = "Projects - ACTIVE CASES";
            }
            else
            {
                btnAll.Visible = false;
                groupBox3.Text = "Projects";
            }

            //set up screen editable
            SetupEditable();

            if ((enterPoint == TypeTFUEntryPoint.SEARCH || enterPoint == TypeTFUEntryPoint.FORM) && projectalllist.Count == 0)
                BeginInvoke(new ShowAllCompleteDelegate(ShowAllCompletedMessage));

            BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));

            this.ActiveControl = txtRespOrg;

            formloading = false;
        }

        private void DisplayProjectGrid(List<TFUProject> plist)
        {
            dgProject.DataSource = null;

            dgProject.DataSource = ConvertToDataTable(plist);

            dgProject.AutoResizeColumns();
            dgProject.Columns[0].Width = 50;
            dgProject.Columns[0].HeaderText = "ID";
            dgProject.Columns[1].HeaderText = "PROJECT DESCRIPTION";
            dgProject.Columns[1].Width = 220;
            dgProject.Columns[2].HeaderText = "CONTRACT";
            dgProject.Columns[2].Width = 120;
            //Vip Satisfied
            dgProject.Columns[3].HeaderText = "COMP";
            dgProject.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgProject.Columns[3].Width = 60;
            //Status
            dgProject.Columns[4].Visible = false;
           // priority
            dgProject.Columns[5].Visible = false;
            //callreq
            dgProject.Columns[6].Visible = false;
            //owner
            dgProject.Columns[7].Visible = false;
            
            dgProject.Columns[8].Visible = false;
            dgProject.Columns[9].Visible = false;
        }

        //load project data
        private void LoadProjectData()
        {
            formloading = true;

            /*Sample */
            sampdata = new SampleData();

            dbsource = sampdata.GetDatabaseSource(id);
            samp = sampdata.GetSampleData(id);
            old_fwgt = samp.Fwgt;

            /*Master */
            mastdata = new MasterData();
            mast = mastdata.GetMasterData(samp.Masterid);

            /*Soc */
            socdata = new SocData();
            if (mast.Owner == "M")
                soc = socdata.GetSocData(samp.Masterid);

            /*load monthly vips */
            mvsdata = new MonthlyVipsData();
            mvs = new MonthlyVips(id);
            mvs = mvsdata.GetDisplayMonthlyVips(id, dbsource, samp.Rvitm5c, samp.Rvitm5cr);

            //rebuild Status combo
            DataTable dtblDataSource = new DataTable();
            dtblDataSource.Columns.Add("Text");
            dtblDataSource.Columns.Add("Value");

            dtblDataSource.Rows.Add("1-Active", 1);
            dtblDataSource.Rows.Add("2-PNR", 2);
            dtblDataSource.Rows.Add("3-DC PNR", 3);
            dtblDataSource.Rows.Add("4-Abeyance", 4);
            dtblDataSource.Rows.Add("5-Duplicate", 5);
            dtblDataSource.Rows.Add("6-Out of Scope", 6);
            dtblDataSource.Rows.Add("7-DC Refusal", 7);
            dtblDataSource.Rows.Add("8-Refusal", 8);

            cbStatus.DataSource = null;
            cbStatus.DataSource = dtblDataSource;
            cbStatus.DisplayMember = "Text";
            cbStatus.ValueMember = "Value";

            /*Add record to csdaccess */
            csda = new CsdaccessData();

            /*get flag string from database, generate cpr flags list*/
            flagdata = new CprflagsData();
            Dataflags flags = flagdata.GetCprflagsData(id);
            cflags = new Cprflags(flags, mast.Owner, "report");

            /*load sched_call data */
            scheddata = new SchedCallData();
            schedcall = scheddata.GetSchedCallData(id);
            if (schedcall == null)
            {
                schedcall = new Schedcall(id);
                schedcall.Added = "Y";
                schedcall.Coltec = resp.Coltec;
                schedcall.Callreq = "N";
                schedcall.Calltpe = "W";
                schedcall.Callstat = "";
                schedcall.Apptends = "1700";
                schedcall.Appttime = (8+time_factor).ToString("00")+"00";
                
                //set priority
                if (resp.Coltec == "P" && samp.Status != "7")
                {
                    schedcall.Priority = "11";
                    schedcall.PriorityDesc = "Regualar Phone";
                }
                else if (resp.Coltec == "C" && samp.Status != "7")
                {
                    schedcall.Priority = "21";
                    schedcall.PriorityDesc = "Regular - Cent";
                }
                else if (resp.Coltec == "F" && samp.Status != "7")
                {
                    schedcall.Priority = "22";
                    schedcall.PriorityDesc = "Regular - Mail";
                }
                else
                {
                    schedcall.Priority = "23";
                    schedcall.PriorityDesc = "Coltec I,A,S and Status 7";
                }
            }

            schedcall.Accestms = DateTime.Now.ToString("HHmmss");
            schedcall.IsModified = false;
            sched_data_saved = false;

            /*Comment history */
            pdata = hd.GetProjCommentTable(id, true);
            rdata = hd.GetRespCommentTable(samp.Respid, true);

            /*Project marks */
            pmarklist.Clear();
            rmarklist.Clear();
            pmarkdata = new ProjMarkData();
            rmarkdata = new RespMarkData();
            if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                pmark = pmarkdata.GetProjmarkData(id);
                pmarklist.Add(pmark);
                
                rmark = rmarkdata.GetRespmarkData(samp.Respid);
                rmarklist.Add(rmark);
            }
          
            /* referral data */
            refdata = new ReferralData();
            referral_exist = refdata.CheckReferralExist(samp.Id, samp.Respid);

            cprauditlist.Clear();
            vipauditlist.Clear();

            lag_changed = false;

            this.ActiveControl = txtProjDesc;

            formloading = false;
        }


        //show lock message
        private void ShowLockMessage()
        {
            /*show message if the case locked by someone */
            if (locked_by != "")
                MessageBox.Show("The case is locked by " + locked_by + ", cannot be edited.");
        }

        //show all complete message
        private void ShowAllCompletedMessage()
        {
            MessageBox.Show("All projects are completed or inactive, Select All Cases to view all  projects.");
        }

        //set editable for respondent 
        private void SetupEditable()
        {
            if (editable)
            {
               
                //set up call resolution
                if (id != null && enterPoint != TypeTFUEntryPoint.SEARCH)
                {
                    cbCall.Visible = true;
                    txtcall.Visible = false;
                }
                else
                {
                    cbCall.Visible = false;
                    txtcall.Visible = true;
                }

                if (id != null)
                {
                    cbColtec.Visible = true;
                    txtColtec.Visible = false;
                    cbLag.Visible = true;
                    txtLag.Visible = false;

                    txtRespname.ReadOnly = false;
                    txtRespname2.ReadOnly = false;

                    txtRespOrg.ReadOnly = false;
                    txtFactOff.ReadOnly = false;
                    txtOthrResp.ReadOnly = false;
                    txtRespnote.ReadOnly = false;

                    txtAddr1.ReadOnly = false;
                    txtAddr2.ReadOnly = false;
                    txtAddr3.ReadOnly = false;

                    txtZip.ReadOnly = false;
                    txtEmail.ReadOnly = false;
                    txtWebAddr.ReadOnly = false;
                    txtFax.ReadOnly = false;
                    txtExt.ReadOnly = false;
                    txtPhone.ReadOnly = false;
                    txtExt2.ReadOnly = false;
                    txtPhone2.ReadOnly = false;
                }
                else
                {
                    cbColtec.Visible = false;
                    txtColtec.Visible = true;
                    cbLag.Visible = false;
                    txtLag.Visible = true;

                    txtRespname.ReadOnly = true;
                    txtRespname2.ReadOnly = true;

                    txtRespOrg.ReadOnly = true;
                    txtFactOff.ReadOnly = true;
                    txtOthrResp.ReadOnly = true;
                    txtRespnote.ReadOnly = true;

                    txtAddr1.ReadOnly = true;
                    txtAddr2.ReadOnly = true;
                    txtAddr3.ReadOnly = true;

                    txtZip.ReadOnly = true;
                    txtEmail.ReadOnly = true;
                    txtWebAddr.ReadOnly = true;
                    txtFax.ReadOnly = true;
                    txtExt.ReadOnly = true;
                    txtPhone.ReadOnly = true;
                    txtExt2.ReadOnly = true;
                    txtPhone2.ReadOnly = true;
                }
            }
            else
            {
                cbColtec.Visible = false;
                txtColtec.Visible = true;
                cbLag.Visible = false; ;
                txtLag.Visible = true;

                cbCall.Visible = false;
                txtcall.Visible = true;

                txtRespname.ReadOnly = true;
                txtRespname2.ReadOnly = true;

                txtRespOrg.ReadOnly = true;
                txtFactOff.ReadOnly = true;
                txtOthrResp.ReadOnly = true;
                txtRespnote.ReadOnly = true;

                txtAddr1.ReadOnly = true;
                txtAddr2.ReadOnly = true;
                txtAddr3.ReadOnly = true;

                txtZip.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtWebAddr.ReadOnly = true;
                txtFax.ReadOnly = true;
                txtExt.ReadOnly = true;
                txtPhone.ReadOnly = true;
                txtExt2.ReadOnly = true;
                txtPhone2.ReadOnly = true;

                if (RespId == "0000000")
                {
                    btnComments.Enabled = false;
                    btnPrint.Enabled = false;
                    
                    btnSlip.Enabled = false;
                    btnSource.Enabled = false;
                    btnReferral.Enabled = false;
                    btnAduit.Enabled = false;
                    btnName.Enabled = false;
                    btnNewtc.Enabled = false;
                    btnRefresh.Enabled = false;
                   
                    btnPrevious.Enabled = false;

                }
            }

            if (CallingForm == null)
            {
                btnPrevious.Enabled = false;
                btnTfu.Enabled = true;
                btnForm.Enabled = true;
            }
            else
            {
                btnPrevious.Enabled = true;
                btnTfu.Enabled = false;
                btnForm.Enabled = false;
            }
        }

        //display project info for a respondent
        private void ResetProject()
        {
            id = null;

            txtcall.Visible = true;
            cbCall.Visible = false;

            txtId.Text = "";
            txtFipst.Text = "";
            txtSeldate.Text = "";
            txtNewtc.Text = "";
            txtSelValue.Text = "";
            txtFwgt.Text = "";
            txtSurvey.Text = "";
            cbSurvey.SelectedIndex = -1;
            
            SetupCapexp(false);

            cbStatus.SelectedIndex = -1;
            txtStatus.Text = "";
            txtProjDesc.Text = "";
            txtProjLoc.Text = "";
            txtPcityst.Text = "";
            txtContract.Text = "";
            txtItem5ar.Text = "";
            txtFlag5a.Text = "";
            txtItem5br.Text = "";
            txtFlag5b.Text = "";
            txtRvitm5cr.Text = "";
            txtFlagr5c.Text = "";
            txtItem6r.Text = "";
            txtFlagItm6.Text = "";
            txtCapexpr.Text = "";
            txtFlagcap.Text = "";
            txtStrtdater.Text = "";
            txtFlagStrtdate.Text = "";
            txtCompdater.Text = "";
            txtFlagCompdate.Text = "";
            txtFutcompdr.Text = "";
            txtFlagFutcompd.Text = "";
            txtItem5ar.Text = "";
            txtItem5br.Text = "";
            txtRvitm5cr.Text = "";
            txtItem6r.Text = "";
            txtCostpu.Text = "";

            txtBldgs.Text = "";
            txtUnits.Text = "";
            txtCostpu.Text = "";

            txtcall.Text = "";
            txtCallcnt.Text = "";
            txtLastCallSch.Text = "";
            txtCaseGroup.Text = "";

            SetupProjectEditable(false);

            UnHighlightTextBox(txtRvitm5cr);
            UnHighlightTextBox(txtItem5ar);
            UnHighlightTextBox(txtItem5br);
            UnHighlightTextBox(txtItem6r);
            UnHighlightTextBox(txtCapexpr);
            UnHighlightTextBox(txtCompdater);
            UnHighlightTextBox(txtStrtdater);
            UnHighlightTextBox(txtFutcompdr);
            UnHighlightDataGrid();

            txtCumvipr.Text = "";
            txtCompr.Text = "";

            cbCall.SelectedIndex = -1;

            /*display the comment */
            dgRcomments.DataSource = null;

            /*display mark */
            dgPmark.DataSource = null;
            dgRmark.DataSource = null;

            dgPcomments.DataSource = null;
            dgRcomments.DataSource = null;

            cprauditlist.Clear();
            vipauditlist.Clear();
            samp = null;
            mast = null;
            soc = null;
            mvs = null;
            cflags = null;

            dgVip.DataSource = null;
            dgFlags.DataSource = null;

            lblMark.Visible = false;

            tabControl2.SelectedIndex = 0;

            lblReject.Visible = false;

            /*Show referral flag */
            lblReferral.Visible = false;
            lblMark.Visible = false;
            lblLock.Visible = false;

            btnSlip.Enabled = false;
            btnName.Enabled = false;
        }

        /*Hide and show Captial Exp */
        private void SetupCapexp(bool showfields)
        {
            if (!showfields)
            {
                label34.Visible = false;
                txtCapexpr.Visible = false;
                txtFlagcap.Visible = false;
            }
            else
            {
                label34.Visible = true;
                txtCapexpr.Visible = true;
                txtFlagcap.Visible = true;
            }
        }

        private void DisplayProject()
        {
            if (id == null)
            {
                ResetProject();
                return;
            }

            txtId.Text = id;
            txtFipst.Text = mast.Fipstater;
            txtSeldate.Text = mast.Seldate;
            txtNewtc.Text = mast.Newtc;
            txtSelValue.Text = mast.Projselv.ToString("#,#");
            txtFwgt.Text = samp.Fwgt.ToString("N2");
            txtSurvey.Text = mast.Owner;
            cbSurvey.SelectedItem = mast.Owner;

            if (mast.Owner == "N" || mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W")
                SetupCapexp(true);
            else
                SetupCapexp(false);

            cbStatus.SelectedValue = samp.Status;
            txtStatus.Text = cbStatus.Text;
            txtProjDesc.Text = samp.Projdesc;
            txtProjLoc.Text = samp.Projloc;
            txtPcityst.Text = samp.Pcityst;
            txtContract.Text = samp.Contract;
            txtItem5ar.Text = samp.Item5ar > 0 ? samp.Item5ar.ToString("#,#") : samp.Item5ar.ToString();
            txtFlag5a.Text = samp.Flag5a;
            txtItem5br.Text = samp.Item5br > 0 ? samp.Item5br.ToString("#,#") : samp.Item5br.ToString();
            txtFlag5b.Text = samp.Flag5b;
            txtRvitm5cr.Text = samp.Rvitm5cr > 0 ? samp.Rvitm5cr.ToString("#,#") : samp.Rvitm5cr.ToString();
            txtFlagr5c.Text = samp.Flagr5c;
            txtItem6r.Text = samp.Item6r > 0 ? samp.Item6r.ToString("#,#") : samp.Item6r.ToString();
            txtFlagItm6.Text = samp.Flagitm6;
            txtCapexpr.Text = samp.Capexpr > 0 ? samp.Capexpr.ToString("#,#") : samp.Capexpr.ToString();
            txtFlagcap.Text = samp.Flagcap;
            txtStrtdater.Text = samp.Strtdater;
            txtFlagStrtdate.Text = samp.Flagstrtdate;
            txtCompdater.Text = samp.Compdater;
            txtFlagCompdate.Text = samp.Flagcompdate;
            txtFutcompdr.Text = samp.Futcompdr;
            txtFlagFutcompd.Text = samp.Flagfutcompd;

            if (mast.Owner == "M")
            {
                txtBldgs.Text = soc.Rbldgs.ToString();
                txtUnits.Text = soc.Runits.ToString("N0");
                txtCostpu.Text = soc.Costpu.ToString("N0");
            }
            else
            {
                txtBldgs.Text = "";
                txtUnits.Text = "";
                txtCostpu.Text = "";
            }

            /*display monthly vips */
            displayMonthlyVips();

            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
            txtCompr.Text = mvs.GetCumPercentr(samp.Rvitm5cr).ToString();

           // cbCall.SelectedIndex = -1;
            txtCaseGroup.Text = schedcall.Priority;
            txtCallcnt.Text = schedcall.Callcnt.ToString();
            if (schedcall.Callstat  == "5")
                txtLastCallSch.Text = GetCallSchedText(schedcall.Callstat.Trim()) +" " + schedcall.Apptdate;
            else
                txtLastCallSch.Text = GetCallSchedText(schedcall.Callstat.Trim());
            
            SetupProjectEditable(editable);

            //if there is flag, highlight related fields with red
            HighlightFieldsFromFlags();

            SetupDataFlag();

            /*display the comment */
            if (pdata.Rows.Count != 0)
            {
                dgPcomments.DataSource = pdata;
                dgPcomments.Columns[0].HeaderText = "DATE";
                dgPcomments.Columns[1].HeaderText = "USER";
                dgPcomments.Columns[2].HeaderText = "COMMENT";

            }
            else
                dgPcomments.DataSource = null;

            /*Respondent comment list */
            if (rdata.Rows.Count != 0)
            {
                dgRcomments.DataSource = rdata;
                dgRcomments.Columns[0].HeaderText = "DATE";
                dgRcomments.Columns[1].HeaderText = "USER";
                dgRcomments.Columns[2].HeaderText = "COMMENT";
            }
            else
                dgRcomments.DataSource = null;


            /*display mark */
            if (pmark != null)
            {
                //dgMark.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dgPmark.RowHeadersVisible = false; // set it to false if not needed
                dgPmark.DataSource = pmarklist;

                dgPmark.Columns[2].HeaderText = "USER";
                dgPmark.Columns[3].HeaderText = "MARK NOTE";
            }
            else
            {
                dgPmark.DataSource = null;
            }

            if (rmark != null)
            {
                //dgMark.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
                dgRmark.RowHeadersVisible = false; // set it to false if not needed
                dgRmark.DataSource = rmarklist;
                
                dgRmark.Columns[2].HeaderText = "USER";
                dgRmark.Columns[3].HeaderText = "MARK NOTE";
            }
            else
            {
                dgRmark.DataSource = null;
            }

            if (CheckMarkExists())
                lblMark.Visible = true;
            else
                lblMark.Visible = false;

            tabControl2.SelectedIndex = 0;


            /*Show reject flag */
            if (dbsource == TypeDBSource.Hold)
                lblReject.Visible = true;
            else
                lblReject.Visible = false;

            /*Show referral flag */
            if (referral_exist)
                lblReferral.Visible = true;
            else
                lblReferral.Visible = false;
        }

        //set up project editable
        private void SetupProjectEditable(bool project_editable)
        {
            if (project_editable)
            {
                //txtcall.Visible = false;
                //cbCall.Visible = true;
                cbStatus.Visible = true;
                txtStatus.Visible = false;
                
                txtProjDesc.ReadOnly = false;
                txtProjLoc.ReadOnly = false;
                txtPcityst.ReadOnly = false;
                txtContract.ReadOnly = false;

                txtStrtdater.ReadOnly = false;
                txtFutcompdr.ReadOnly = false;
                txtItem5ar.ReadOnly = false;
                txtItem5br.ReadOnly = false;
                txtRvitm5cr.ReadOnly = false;
                txtItem6r.ReadOnly = false;
                txtCapexpr.ReadOnly = false;
                txtCompdater.ReadOnly = false;
                txtNewtc.ReadOnly = true;
                btnNewtc.Enabled = true;

                dgVip.EditMode = DataGridViewEditMode.EditOnEnter;

                if (mast.Owner == "M")
                {
                    txtBldgs.ReadOnly = false;
                    txtBldgs.TabStop = true;
                    txtUnits.ReadOnly = false;
                    txtUnits.TabStop = true;
                    txtCostpu.ReadOnly = true;
                    txtSurvey.Visible = true;
                    cbSurvey.Visible = false;
                }
                else
                {
                    txtBldgs.ReadOnly = true;
                    txtBldgs.TabStop = false;
                    txtUnits.ReadOnly = true;
                    txtUnits.TabStop = false;
                    txtCostpu.ReadOnly = true;
                    txtSurvey.Visible = false;
                    cbSurvey.Visible = true;
                }

                btnProcess.Enabled = true;
                btnRefresh.Enabled = true;
            }
            else
            {
               // txtcall.Visible = true;
               // cbCall.Visible = false;
                txtNewtc.ReadOnly = true;
                btnNewtc.Enabled = false;
                cbStatus.Visible = false;
                txtStatus.Visible = true;
                txtSurvey.Visible = true;
                cbSurvey.Visible = false;
                txtProjDesc.ReadOnly = true;
                txtProjLoc.ReadOnly = true;
                txtPcityst.ReadOnly = true;
                txtContract.ReadOnly = true;

                txtStrtdater.ReadOnly = true;
                txtFutcompdr.ReadOnly = true;
                txtItem5ar.ReadOnly = true;
                txtItem5br.ReadOnly = true;
                txtRvitm5cr.ReadOnly = true;
                txtItem6r.ReadOnly = true;
                txtCapexpr.ReadOnly = true;
                txtCompdater.ReadOnly = true;

                txtBldgs.ReadOnly = true;
                txtBldgs.TabStop = false;
                txtUnits.ReadOnly = true;
                txtUnits.TabStop = false;
                txtCostpu.ReadOnly = true;

                dgVip.EditMode = DataGridViewEditMode.EditProgrammatically;

                btnProcess.Enabled = false;
                btnRefresh.Enabled = false;
            }

            if ((UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager) && id != null)
            {
                btnMark.Visible = true;
                if (tabControl2.TabPages.Count ==4)
                {
                    tabControl2.TabPages.Add(tabPage7);
                    tabControl2.TabPages.Add(tabPage8);  
                }   
            }
            else
            {
                btnMark.Visible = false;
                tabControl2.TabPages.Remove(tabPage7);
                tabControl2.TabPages.Remove(tabPage8);  
            }

            // if the id is null, disable buttons
            if (id != null)
            {
                //disable name & address button if seldate is current month
                if (mast.Seldate == GeneralFunctions.CurrentYearMon())
                    btnName.Enabled = false;
                else
                    btnName.Enabled = true;
                btnReferral.Enabled = true;
                btnAduit.Enabled = true;
                btnComments.Enabled = true;
                btnSource.Enabled = true;

                if (GeneralDataFuctions.CheckDodgeSlip(samp.Masterid))
                    btnSlip.Enabled = true;
                else
                    btnSlip.Enabled = false;

                btnPrint.Enabled = true;
            }
            else
            {
                btnName.Enabled = false;
                btnReferral.Enabled = false;
                btnAduit.Enabled = false;
                btnComments.Enabled = false;
                btnSource.Enabled = false;
                btnSlip.Enabled = false;
                btnPrint.Enabled = false;
            }

        }

        //set up flag grid
        private void SetupDataFlag()
        {
            if (cflags != null && cflags.displayFlaglist.Count > 0)
            {
                dgFlags.DataSource = null;
                dgFlags.DataSource = cflags.displayFlaglist;
                dgFlags.Columns[0].Visible = false;
                dgFlags.Columns[1].Visible = false;
                dgFlags.Columns[2].ReadOnly = true;
                dgFlags.Columns[2].HeaderText = "FLAG";
                dgFlags.Columns[3].Visible = false;
            }
            else
                dgFlags.DataSource = null;

            dgFlags.Refresh();

        }

        //from coltec code to get display text
        private string GetDisplayColtecText(string coltec_code)
        {
            string coltec_text = string.Empty;
            if (coltec_code == "F")
                coltec_text = "F-Form";
            else if (coltec_code == "C")
                coltec_text = "C-Centurion";
            else if (coltec_code == "P")
                coltec_text = "P-Phone";
            else if (coltec_code == "I")
                coltec_text = "I-Internet";
            else if (coltec_code == "S")
                coltec_text = "S-Special";
            else
                coltec_text = "A-Admin";

            return coltec_text;

        }

        int saveRow = 0;
        /*set up grid for monthly vips */
        private void displayMonthlyVips()
        {
            ////Save current row
            if (dgVip.Rows.Count > 0)
                saveRow = dgVip.FirstDisplayedCell.RowIndex;

            vip_grid_set = false;
            dgVip.DataSource = null;
            dgVip.DataSource = mvs.monthlyViplist;
            
            if (tabControl2.SelectedIndex == 1)
                SetDgVip(dgVip);

            if (saveRow != 0 && saveRow < dgVip.Rows.Count)
                dgVip.FirstDisplayedScrollingRowIndex = saveRow;
            else
                dgVip.FirstDisplayedScrollingRowIndex = 0;

            dgVip.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgVip.RowHeadersVisible = false; // set it to false if not needed

            dgVip.AutoResizeColumns();

        }

        private void btnForm_Click(object sender, EventArgs e)
        {
            if (id != null )
            {
                if (cbCall.SelectedIndex == -1 && enterPoint == TypeTFUEntryPoint.NPC)
                {
                    MessageBox.Show("Please enter a Call resolution");
                    cbCall.Focus();
                    return;
                }

                bool data_modified = CheckFormChanged();

                if (data_modified)
                {
                    if (!ValidateFormData())
                        return;
                    if (enterPoint == TypeTFUEntryPoint.NPC)
                    {
                        if (!ValidateShedCall())
                            return;
                    }
                    if (!SaveData())
                        return;
                }
               
                if (data_saved || (schedcall.IsModified && !sched_data_saved))
                {
                    if (!SaveSchedCall())
                        return;
                }
                   
                if (enterPoint == TypeTFUEntryPoint.NPC && (finished_cases.Count > 0 && projectlist.Count > finished_cases.Count))
                {
                    //check satisified cases
                    IEnumerable<TFUProject> projectlistY = from j in projectalllist
                                                            where (j.Satisfied == "Y")
                                                            select j;

                    MessageBox.Show("There are " + (projectlist.Count - finished_cases.Count) + " remaining projects. A Call Resolution will need to be entered for each of the remaining projects.");

                    int rowIndex = 0;
                    foreach (DataGridViewRow rrow in dgProject.Rows)
                    {
                        string new_id = rrow.Cells["id"].Value.ToString();
                        bool exist = finished_cases.Any(s => new_id.Contains(s));
                        if (!exist)
                        {
                            rowIndex = rrow.Index;
                            break;
                        }
                    }
                    dgProject.Rows[rowIndex].Selected = true;
                    dgProject.CurrentCell = dgProject[0, rowIndex];
                    cbCall.Focus();
                    cbCall.SelectedIndex = -1;
                    return;
                }
            }
 
            
            frmRespidPopup popup = new frmRespidPopup(true, true);
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();  //show child

            //if the popup was cancelled, set status to old value
            if (popup.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                if (id != null)
                {
                    if (data_saved)
                        csda.AddCsdaccessData(id, "UPDATE");
                    else
                        csda.AddCsdaccessData(id, "BROWSE");
                }

                //unlock old respid
                /*unlock respondent */
                if (editable)
                {
                    bool locked = GeneralDataFuctions.UpdateRespIDLock(RespId, "");
                }

                RespId = popup.NewRespid;
                enterPoint = TypeTFUEntryPoint.FORM;
                lblTel.Text = "FORM ENTRY";
                LoadForm();
                DisplayForm();
            }
           
        }

        private void btnTfu_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                if (id != null && id != "")
                {
                    ////check remain cases
                    if (cbCall.SelectedIndex == -1 && enterPoint == TypeTFUEntryPoint.NPC)
                    {
                        MessageBox.Show("Please enter a Call resolution");
                        cbCall.Focus();
                        return;
                    }

                    bool data_modified = CheckFormChanged();
                      
                    if (data_modified)
                    {
                        if (!ValidateFormData())
                            return;
                        if (enterPoint == TypeTFUEntryPoint.NPC)
                        {
                            if (!ValidateShedCall())
                                return;
                        }

                        if (!SaveData())
                            return;
                    }

                   
                    if (data_saved || (schedcall.IsModified && !sched_data_saved ))
                    {
                        if (!SaveSchedCall())
                            return;
                    }
                }
            }

            if (id != null && id != "")
            {
                if (data_saved)
                    csda.AddCsdaccessData(id, "UPDATE");
                else
                    csda.AddCsdaccessData(id, "BROWSE");
            

                if (editable && enterPoint == TypeTFUEntryPoint.NPC)
                {
                    if (finished_cases.Count > 0 && projectlist.Count > finished_cases.Count)
                    {
                        //check satisified cases
                        IEnumerable<TFUProject> projectlistY = from j in projectalllist
                                                               where (j.Satisfied == "Y")
                                                               select j;

                        string call_resolution_string = string.Empty;
                        if (projectlistY.Count() > 0)
                            call_resolution_string = "Rescheduled call, Promised to Report";
                        else
                            call_resolution_string = "Ring, Busy, Left Message, No Answer, Disconnected, Rescheduled call, Promised to Report";

                        // MessageBox.Show("There are " + (projectlist.Count - finished_cases.Count) + " remaining projects.  Enter one of " + call_resolution_string + " call resolution for these project");
                        MessageBox.Show("There are " + (projectlist.Count - finished_cases.Count) + " remaining projects. A Call Resolution will need to be entered for each of the remaining projects.");

                        int rowIndex = 0;
                        foreach (DataGridViewRow rrow in dgProject.Rows)
                        {
                            string new_id = rrow.Cells["id"].Value.ToString();
                            bool exist = finished_cases.Any(s => new_id.Contains(s));
                            if (!exist)
                            {
                                rowIndex = rrow.Index;
                                break;
                            }
                        }
                        formloading = true;
                        dgProject.Rows[rowIndex].Selected = true;
                        formloading = false;
                        id = dgProject.SelectedRows[0].Cells[0].FormattedValue.ToString();

                        // Update the project grid to reflect changes to the selection.
                        LoadProjectData();
                        DisplayProject();
                        
                        //cbCall.Focus();
                        //cbCall.SelectedIndex = -1;
                        return;
                    }
                }
            }

            //Get Next TFU
            
            //check current time
            DateTime t1 = DateTime.Now;
            DateTime t2 = Convert.ToDateTime("8:00:00 AM");

            //if current time is less than 8 o'clock
            if ((DateTime.Compare(t1, t2)) < 0)
            {
                MessageBox.Show("No cases are available until 8:00 AM");
                return;
            }

            //check authorized user
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                if (UserInfo.ContSL != "Y" && UserInfo.ContNR != "Y" && UserInfo.ContMF != "Y" && UserInfo.ContFD != "Y")
                {
                    MessageBox.Show("You are not authorized user for TFU");
                    return;
                }
            }

          
            //if current day is greater than cut day
            int today = DateTime.Now.Day;
            
            if (today > cut_day)
            {
                MessageBox.Show("No cases are available after cut off date");
                return;
            }

            //get next case for the interviewer
            int workday = GeneralFunctions.GetNumBusinessDayforToday(DateTime.Now.Year, DateTime.Now.Month);

            string next_respid = "";
            string next_id = "";

            scheddata = new SchedCallData();
            scheddata.GetNextCase(workday, ref next_respid, ref next_id);
        
            if (next_respid == "")
            {
                MessageBox.Show("All cases are finished");
                
                return;
            }
            else
            {
                enterPoint = TypeTFUEntryPoint.NPC;
                lblTel.Text = "TFU";

                if (editable && samp != null)
                {
                    /*unlock respondent */
                    bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
                }

                RespId = next_respid;
                locked_by = "";

                LoadForm(true, true);
                DisplayForm();

                //set finished cases list
                finished_cases = new List<string>();
                finished_cases.Clear();

                //highlight id
                HighlightRowForId(next_id);
                id = next_id;

                // Update the project grid to reflect changes to the selection.
                LoadProjectData();
                DisplayProject();               
            }           
        }            
         
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                if (!ValidateFormData())
                    return;
                if (CheckFormChanged())
                {
                    if (enterPoint == TypeTFUEntryPoint.NPC)
                    {
                        if (!ValidateShedCall())
                            return;
                    }
                    if (!SaveData())
                        return;
                }

                bool locked;
                if (samp != null)
                {
                    if ((data_saved) || (schedcall.IsModified && !sched_data_saved))
                    {
                        if (!SaveSchedCall())
                        {
                            return;
                        }
                    }

                    locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");

                    if (data_saved)
                    {
                        csda.AddCsdaccessData(samp.Id, "UPDATE");
                    }
                    else
                        csda.AddCsdaccessData(samp.Id, "BROWSE");
                }

            }

            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;

                this.Close();
            }
            
        }

       
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //only load projectData
            resp = respdata.GetRespondentData(RespId);

            DisplayRespondentData();
            LoadProjectData();
            DisplayProject();

            lag_changed = false;
            owner_changed = 0;
            status_changed = false;

           // data_saved = false;
        }

        private void btnComments_Click(object sender, EventArgs e)
        {
            frmHistory fHistory = new frmHistory();

            fHistory.Id = txtId.Text;
            fHistory.Respid = txtRespid.Text;
            fHistory.Resporg = resp.Resporg;
            fHistory.Respname = resp.Respname;

            fHistory.ShowDialog();  //show history screen

            if (!String.IsNullOrEmpty(id))
                pdata = hd.GetProjCommentTable(id, true);
            rdata = hd.GetRespCommentTable(RespId, true);

            //set up grid
            if (!String.IsNullOrEmpty(id))
            {
                if (pdata.Rows.Count != 0)
                {
                    dgPcomments.DataSource = pdata;
                    if (tabControl2.SelectedIndex == 2)
                    {
                        dgPcomments.Columns[0].Width = 75;
                        dgPcomments.Columns[1].Width = 60;
                        dgPcomments.Columns[0].HeaderText = "DATE";
                        dgPcomments.Columns[1].HeaderText = "USER";
                        dgPcomments.Columns[2].HeaderText = "COMMENT";
                    }
                }
                else
                    dgPcomments.DataSource = null;
            }
            else
                dgPcomments.DataSource = null;

            /*Respondent comment list */
            if (rdata.Rows.Count != 0)
            {
                dgRcomments.DataSource = rdata;
                if (tabControl2.SelectedIndex == 3)
                {
                    dgRcomments.Columns[0].Width = 75;
                    dgRcomments.Columns[1].Width = 60;
                    dgRcomments.Columns[0].HeaderText = "DATE";
                    dgRcomments.Columns[1].HeaderText = "USER";
                    dgRcomments.Columns[2].HeaderText = "COMMENT";
                }
            }
            else
            {
                dgRcomments.DataSource = null;
            }
        }

        private void btnReferral_Click(object sender, EventArgs e)
        {
            frmReferral fReferral = new frmReferral();

            fReferral.Id = txtId.Text;
            fReferral.Respid = txtRespid.Text;

            fReferral.ShowDialog();  //show child

            refdata = new ReferralData();
            referral_exist = refdata.CheckReferralExist(samp.Id, samp.Respid);
            lblReferral.Visible = referral_exist;
        }

        /*Add cpraudit data */
        private void AddCprauditData(string avarnme, string aoldval, string aoldflag, string anewval, string anewflag)
        {
            if (aoldval.ToUpper().Trim() == anewval.ToUpper().Trim() && aoldflag == anewflag) return;

            /*Get audit record from list */
            Cpraudit au = (from Cpraudit j in cprauditlist
                           where j.Varnme == avarnme
                           select j).SingleOrDefault();

            /*if there is no record, add one, otherwise update the record */
            if (au == null)
            {
                Cpraudit ca = new Cpraudit();
                ca.Id = id;
                ca.Varnme = avarnme;
                ca.Oldval = aoldval;
                ca.Oldflag = aoldflag;
                ca.Newval = anewval;
                ca.Newflag = anewflag;
                ca.Usrnme = UserInfo.UserName;
                ca.Progdtm = DateTime.Now;

                cprauditlist.Add(ca);
            }
            else
            {
                au.Newval = anewval;
                au.Newflag = anewflag;
                au.Progdtm = DateTime.Now;
            }
        }

        /*Add respaudit data */
        private void AddRspauditData(string avarnme, string aoldval, string anewval)
        {
            //in case old value is same as new value
            if (aoldval.ToUpper().Trim() == anewval.ToUpper().Trim()) return;

            /*Get audit record from list */
            Respaudit au = (from Respaudit j in Respauditlist
                            where j.Varnme == avarnme
                            select j).SingleOrDefault();

            /*if there is no record, add one, otherwise update the record */
            if (au == null)
            {
                Respaudit ca = new Respaudit();
                ca.Respid = RespId;
                ca.Varnme = avarnme;
                ca.Oldval = aoldval;
                ca.Newval = anewval;
                ca.Usrnme = UserInfo.UserName;
                ca.Progdtm = DateTime.Now;

                Respauditlist.Add(ca);
            }
            else
            {
                au.Newval = anewval;
                au.Progdtm = DateTime.Now;
            }

        }

        //read from screen
        private void ReadFromScreen()
        {
            //Read respondent info

            //check coltec change
            if (cbColtec.Text != "" && cbColtec.Text.Substring(0, 1) != resp.Coltec)
            {
                AddRspauditData("COLTEC", resp.Coltec, cbColtec.Text.Substring(0, 1));
                resp.Coltec = cbColtec.Text.Substring(0, 1);
            }

            //check respname
            if (!resp.Respname.Equals(txtRespname.Text, StringComparison.Ordinal))
            {
                AddRspauditData("RESPNAME", resp.Respname, txtRespname.Text);
                resp.Respname = txtRespname.Text;
            }

            //check respname2
            if (!resp.Respname2.Equals(txtRespname2.Text, StringComparison.Ordinal))
            {
                AddRspauditData("RESPNAME2", resp.Respname2, txtRespname2.Text);
                resp.Respname2 = txtRespname2.Text;
            }

            //check resporg
            if (!resp.Resporg.Equals(txtRespOrg.Text, StringComparison.Ordinal))
            {
                AddRspauditData("RESPORG", resp.Resporg, txtRespOrg.Text);
                resp.Resporg = txtRespOrg.Text;
            }

            //check factoff
            if (!resp.Factoff.Equals(txtFactOff.Text, StringComparison.Ordinal))
            {
                AddRspauditData("FACTOFF", resp.Factoff, txtFactOff.Text);
                resp.Factoff = txtFactOff.Text;
            }

            //check othrresp
            if (!resp.Othrresp.Equals(txtOthrResp.Text, StringComparison.Ordinal))
            {
                AddRspauditData("OTHRRESP", resp.Othrresp, txtOthrResp.Text);
                resp.Othrresp = txtOthrResp.Text;
            }

            //check respnote
            if (!resp.Respnote.Equals(txtRespnote.Text, StringComparison.Ordinal))
            {
                AddRspauditData("RESPNOTE", resp.Respnote, txtRespnote.Text);
                resp.Respnote = txtRespnote.Text;
            }

            //check 
            //lag_changed = false;
            if (resp.Lag != Convert.ToInt32(cbLag.Text))
            {
                AddRspauditData("LAG", resp.Lag.ToString(), cbLag.Text);
                resp.Lag = Convert.ToInt32(cbLag.Text);
                lag_changed = true;
            }

            //check addr1
            if (!resp.Addr1.Equals(txtAddr1.Text.Trim(), StringComparison.Ordinal))
            {
                AddRspauditData("ADDR1", resp.Addr1, txtAddr1.Text.Trim());
                resp.Addr1 = txtAddr1.Text.Trim();
            }

            //check addr2
            if (!resp.Addr2.Equals(txtAddr2.Text.Trim(), StringComparison.Ordinal))
            {
                AddRspauditData("ADDR2", resp.Addr2, txtAddr2.Text.Trim());
                resp.Addr2 = txtAddr2.Text.Trim();
            }

            //check addr3
            if (!resp.Addr3.Equals(txtAddr3.Text.Trim(), StringComparison.Ordinal))
            {
                AddRspauditData("ADDR3", resp.Addr3, txtAddr3.Text.Trim());
                resp.Addr3 = txtAddr3.Text.Trim();

                //update rstate
                resp.Rstate = rst;
            }

            //check phone
            string rphone = new string(txtPhone.Text.Where(char.IsDigit).ToArray());
            if (!resp.Phone.Equals(rphone, StringComparison.Ordinal))
            {
                AddRspauditData("PHONE", resp.Phone, rphone);
                resp.Phone = rphone;
            }

            //check ext
            if (!resp.Ext.Equals(txtExt.Text.Trim(), StringComparison.Ordinal))
            {
                AddRspauditData("EXT", resp.Ext, txtExt.Text.Trim());
                resp.Ext = txtExt.Text.Trim();
            }

            //check phone2
            string rphone2 = new string(txtPhone2.Text.Where(char.IsDigit).ToArray());
            if (!resp.Phone2.Trim().Equals(rphone2, StringComparison.Ordinal))
            {
                AddRspauditData("PHONE", resp.Phone, rphone);
                resp.Phone2 = rphone2;
            }

            //check ext2
            if (!resp.Ext2.Equals(txtExt2.Text.Trim(), StringComparison.Ordinal))
            {
                AddRspauditData("EXT", resp.Ext, txtExt.Text.Trim());
                resp.Ext2 = txtExt2.Text.Trim();
            }

            //check fax
            string rfax = new string(txtFax.Text.Where(char.IsDigit).ToArray());
            if (!resp.Fax.Equals(rfax, StringComparison.Ordinal))
            {
                AddRspauditData("FAX", resp.Fax, rfax);
                resp.Fax = rfax;
            }

            //check zip
            if (!resp.Zip.Equals(txtZip.Text, StringComparison.Ordinal))
            {
                AddRspauditData("ZIP", resp.Zip, txtZip.Text.Trim());
                resp.Zip = txtZip.Text.Trim();
            }

            //check email
            if (!resp.Email.Equals(txtEmail.Text, StringComparison.Ordinal))
            {
                AddRspauditData("EMAIL", resp.Email, txtEmail.Text.Trim());
                resp.Email = txtEmail.Text.Trim();
            }

            //check webaddr
            if (!resp.Weburl.Equals(txtWebAddr.Text, StringComparison.Ordinal))
            {
                AddRspauditData("WEBURL", resp.Weburl, txtWebAddr.Text.Trim());
                resp.Weburl = txtWebAddr.Text.Trim();
            }

            if (id != null)
            {
                //get current status value
                if (cbStatus.SelectedValue.ToString() != samp.Status)
                {
                    //Set up active value
                    if ((samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8") && (cbStatus.SelectedValue.ToString() == "4" || cbStatus.SelectedValue.ToString() == "5" || cbStatus.SelectedValue.ToString() == "6"))
                    {
                        samp.Active = "I";
                    }
                    else if ((samp.Status == "4" || samp.Status == "5" || samp.Status == "6") && (cbStatus.SelectedValue.ToString() == "1" || cbStatus.SelectedValue.ToString() == "2" || cbStatus.SelectedValue.ToString() == "3" || cbStatus.SelectedValue.ToString() == "7" || cbStatus.SelectedValue.ToString() == "8"))
                    {
                        samp.Active = "A";
                        if (samp.Compdate.Equals(txtCompdater.Text, StringComparison.Ordinal) && samp.Compdate != "")
                            samp.Active = "C";
                    }

                  
                    AddCprauditData("STATUS", samp.Status, "", cbStatus.SelectedValue.ToString(), "");
                    status_changed = true;
                    samp.Status = cbStatus.SelectedValue.ToString();                   
                }
                else
                    status_changed = false;


                if (mast.Owner != "M")
                {
                    if (cbSurvey.Text != mast.Owner)
                    {
                        //blank out capexp
                        if ((mast.Owner == "N" || mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") && (!txtCapexpr.Visible))
                        {
                            txtCapexpr.Text = "0";
                            txtFlagcap.Text = "B";
                        }

                        // check owner change
                        if (OwnerChanged(mast.Owner, cbSurvey.Text))
                            owner_changed = 2;
                        else
                            owner_changed = 1;

                        AddCprauditData("OWNER", mast.Owner, "", cbSurvey.Text, "");

                        mast.Owner = cbSurvey.Text;
                    }
                }

                if (!mast.Newtc.Equals(txtNewtc.Text, StringComparison.Ordinal))
                {
                    AddCprauditData("NEWTC", mast.Newtc, "", txtNewtc.Text, "");
                    mast.Newtc = txtNewtc.Text;
                }

                //check rbldg and runits
                if (mast.Owner == "M")
                {
                    if (soc.Rbldgs != Convert.ToInt32(txtBldgs.Text))
                    {
                        AddCprauditData("RBLDGS", soc.Rbldgs.ToString(), "", txtBldgs.Text, "");
                        soc.Rbldgs = Convert.ToInt32(txtBldgs.Text);
                    }

                    if (soc.Runits != Convert.ToInt32(txtUnits.Text.Replace(",", "")))
                    {
                        AddCprauditData("RUNITS", soc.Runits.ToString(), "", txtUnits.Text.Replace(",", ""), "");
                        soc.Runits = Convert.ToInt32(txtUnits.Text.Replace(",", ""));
                    }

                    if (soc.Runits != Convert.ToInt32(txtUnits.Text.Replace(",", "")) || samp.Rvitm5cr != Convert.ToInt32(txtRvitm5cr.Text.Replace(",", "")))
                    {
                        int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                        if (it5c > 0)
                        {
                            int iunits = Convert.ToInt32(txtUnits.Text.Replace(",", ""));
                            if (iunits > 0)
                                txtCostpu.Text = Math.Round((double)((double)it5c / iunits), 1).ToString("N0");
                        }
                        soc.Costpu = Convert.ToInt32(txtCostpu.Text.Replace(",", ""));
                    }
                }
                if (!samp.Pcityst.Equals(txtPcityst.Text, StringComparison.Ordinal))
                    samp.Pcityst = txtPcityst.Text;

                if (!samp.Projdesc.Equals(txtProjDesc.Text, StringComparison.Ordinal))
                    samp.Projdesc = txtProjDesc.Text;

                if (!samp.Projloc.Equals(txtProjLoc.Text, StringComparison.Ordinal))
                    samp.Projloc = txtProjLoc.Text;

                if (!samp.Contract.Equals(txtContract.Text, StringComparison.Ordinal))
                {
                    AddCprauditData("CONTRACT", samp.Contract.ToString(), "", txtContract.Text, "");
                    samp.Contract = txtContract.Text;
                }

                if (!samp.Strtdater.Equals(txtStrtdater.Text, StringComparison.Ordinal))
                {
                    AddCprauditData("STRTDATE", samp.Strtdate, samp.Flagstrtdate, txtStrtdater.Text.Trim(), txtFlagStrtdate.Text);
                    if (txtFlagStrtdate.Text == "R" && samp.Repsdate == "")
                        samp.Repsdate = currYearMon;
                    if (txtStrtdater.Text == "" && samp.Repsdate != "")
                        samp.Repsdate = "";
                    samp.Strtdater = txtStrtdater.Text;
                    samp.Strtdate = txtStrtdater.Text;
                }
                if (samp.Flagstrtdate != txtFlagStrtdate.Text)
                    samp.Flagstrtdate = txtFlagStrtdate.Text;

                if (!samp.Futcompdr.Equals(txtFutcompdr.Text, StringComparison.Ordinal))
                {
                    AddCprauditData("FUTCOMPD", samp.Futcompd, samp.Flagfutcompd, txtFutcompdr.Text.Trim(), txtFlagFutcompd.Text);
                    samp.Futcompdr = txtFutcompdr.Text;
                    samp.Futcompd = txtFutcompdr.Text;
                }
                if (samp.Flagfutcompd != txtFlagFutcompd.Text)
                    samp.Flagfutcompd = txtFlagFutcompd.Text;

                int rc;
                rc = Convert.ToInt32(txtItem5ar.Text.Replace(",", ""));
                if (samp.Item5ar != rc)
                {
                    AddCprauditData("ITEM5A", samp.Item5a.ToString(), samp.Flag5a, rc.ToString(), txtFlag5a.Text);
                    samp.Item5ar = rc;
                    samp.Item5a = rc;
                }
                if (samp.Flag5a != txtFlag5a.Text)
                    samp.Flag5a = txtFlag5a.Text;

                rc = Convert.ToInt32(txtItem5br.Text.Replace(",", ""));
                if (samp.Item5br != rc)
                {
                    AddCprauditData("ITEM5B", samp.Item5b.ToString(), samp.Flag5b, rc.ToString(), txtFlag5b.Text);
                    samp.Item5br = rc;
                    samp.Item5b = rc;

                    if (mast.Owner == "M")
                        soc.Costpu = Convert.ToInt32(txtCostpu.Text.Replace(",", ""));
                }
                if (samp.Flag5b != txtFlag5b.Text)
                    samp.Flag5b = txtFlag5b.Text;

                rc = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                if (samp.Rvitm5cr != rc)
                {
                    AddCprauditData("RVITM5C", samp.Rvitm5c.ToString(), samp.Flagr5c, rc.ToString(), txtFlagr5c.Text);
                    samp.Rvitm5cr = rc;
                    samp.Rvitm5c = rc;
                }
                if (samp.Flagr5c != txtFlagr5c.Text)
                    samp.Flagr5c = txtFlagr5c.Text;

                if (samp.Item5c == 0 && txtFlagr5c.Text == "R")
                {
                    samp.Item5c = rc;
                    samp.Flag5c = txtFlagr5c.Text;
                }

                rc = Convert.ToInt32(txtItem6r.Text.Replace(",", ""));
                if (samp.Item6r != rc)
                {
                    AddCprauditData("ITEM6", samp.Item6.ToString(), samp.Flagitm6, rc.ToString(), txtFlagItm6.Text);
                    samp.Item6r = rc;
                    samp.Item6 = rc;
                }
                if (samp.Flagitm6 != txtFlagItm6.Text)
                    samp.Flagitm6 = txtFlagItm6.Text;

                rc = Convert.ToInt32(txtCapexpr.Text.Replace(",", ""));
                if (samp.Capexpr != rc)
                {
                    samp.Capexpr = rc;
                    AddCprauditData("CAPEXP", samp.Capexp.ToString(), samp.Flagcap, rc.ToString(), txtFlagcap.Text);
                    samp.Capexp = rc;
                }
                if (samp.Flagcap != txtFlagcap.Text)
                    samp.Flagcap = txtFlagcap.Text;

                if (!samp.Compdater.Equals(txtCompdater.Text.Trim(), StringComparison.Ordinal))
                {
                    if (samp.Compdater != "" && txtCompdater.Text == "")
                    {
                        if (samp.Status =="4" || samp.Status == "5" || samp.Status == "6")
                            samp.Active = "I";
                        else if (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8")
                            samp.Active = "A";
                    }
                    else if (samp.Compdater == "" && txtCompdater.Text != "" && samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8")
                    {
                        samp.Active = "C";
                    }
                    samp.Compdater = txtCompdater.Text;

                    //in case compdate flag didn't get set
                    if (txtCompdater.Text.Trim() == "")
                        txtFlagCompdate.Text = "B";

                    if (!samp.Compdate.Equals(txtCompdater.Text.Trim(), StringComparison.Ordinal))
                        AddCprauditData("COMPDATE", samp.Compdate, samp.Flagcompdate, txtCompdater.Text.Trim(), txtFlagCompdate.Text);
                    samp.Compdate = txtCompdater.Text;
					
					if (txtFlagCompdate.Text == "R")
						samp.Repcompd = currYearMon;
					else if (txtFlagCompdate.Text == "B")
						samp.Repcompd = "";
                }
                if (samp.Flagcompdate != txtFlagCompdate.Text)
                    samp.Flagcompdate = txtFlagCompdate.Text;

                //check sched call change
                if (cbCall.Visible && cbCall.SelectedIndex != -1)
                {
                    schedcall.Callstat = (cbCall.SelectedIndex - 1).ToString();
                    schedcall.IsModified = true;
                }

            }

        }

        //find out whether owner changed or not
        private bool OwnerChanged(string oldowner, string newowner)
        {
            bool changed = false;

            //if current owner is 'S, L. P' the new owner is not, set change flag
            if (oldowner == "S" || oldowner == "L" || oldowner == "P")
            {
                if (newowner != "S" && newowner != "L" && newowner != "P")
                    changed = true;
            }
            //if current owner is 'C,D,F' the new owner is not, set change flag
            else if (oldowner == "C" || oldowner == "D" || oldowner == "F")
            {
                if (newowner != "C" && newowner != "D" && newowner != "F")
                    changed = true;
            }
            //if current owner is 'N,T' the new owner is not, set change flag
            else if (oldowner == "N" || oldowner == "T")
            {
                if (newowner != "N" && newowner != "T")
                    changed = true;
            }
            //if current owner is 'E,G,R,O,M' the new owner is not, set change flag
            else if (oldowner == "E" || oldowner == "G" || oldowner == "R" || oldowner == "O" || oldowner == "M")
            {
                if (newowner != "E" && newowner != "G" && newowner != "R" && newowner != "O" && newowner != "M")
                    changed = true;

            }
            else if (oldowner == "M")
            {
                if (newowner != "M")
                    changed = true;

            }

            return changed;
        }

        //Verify Form Closing from menu
        public override bool VerifyFormClosing()
        {
            bool can_close = true;

            if (editable)
            {
                /*validate data change*/
                if (!ValidateFormData())
                {
                    return can_close = false;
                }

                bool data_modified = CheckFormChanged();
                if (data_modified)
                {
                    DialogResult result3 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result3 == DialogResult.Yes)
                    {
                        if (enterPoint == TypeTFUEntryPoint.NPC)
                        {
                            if (cbCall.SelectedIndex == -1)
                            {
                                MessageBox.Show("Please enter a Call resolution");
                                cbCall.Focus();
                                can_close = false;
                            }
                            else 
                            {
                                if (!ValidateShedCall())
                                    can_close = false;
                            }
                        }

                        if (can_close && !SaveData())
                                can_close = false;
                     
                    }
                }
                
                if (id != null && id != "" && can_close)
                {
                    if ((schedcall.IsModified && !sched_data_saved) || data_saved)
                    {
                        if (!SaveSchedCall())
                            can_close = false;
                    }
                }
            }

            if (csda != null)
            {
                if (id != null && id != "")
                {
                    if (can_close)
                    {
                        if (data_saved)
                            csda.AddCsdaccessData(id, "UPDATE");
                        else
                            csda.AddCsdaccessData(id, "BROWSE");
                    }
                   
                }
            }

            return can_close;

        }

        // Verify item5a, item5b and Rvitm5c
        private void VerifyMainData()
        {
            if (id == null || id == "") return;

            string verify = "";
            
            int sumvip = mvs.GetCumvip();

            //Find all values of VIPDATA, vipflag in ('R','A','O','B') 
            List<string> vf = mvs.VipFlags();
            List<string> bf = new List<string> { "R", "A", "O", "B" };
            List<string> notInbf = vf.Except(bf).ToList();

            //Find any values of vipflag not in ('R','A','O','B') 
            IEnumerable<MonthlyVip> mvss =
            from mv in mvs.monthlyViplist
            where mv.Vipflag != "R" && mv.Vipflag != "A" && mv.Vipflag != "O" && mv.Vipflag != "B"
            select mv;

            if (txtStrtdater.Text != "" && txtItem5ar.Text != "0" && (txtFlag5a.Text == "R" || txtFlag5a.Text == "A" || txtFlag5a.Text == "O" || txtFlag5a.Text == "E") && (txtFlag5b.Text == "I" || txtFlag5b.Text == "M" || txtFlag5b.Text == "B"))
            {
                if (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B")
                {
                    txtRvitm5cr.Text = txtItem5ar.Text;
                    txtFlagr5c.Text = "E";
                    txtFlag5b.Text = "B";

                    //if it is mulitfamily
                    SetupCostput();
                    verify = "5C was made equal to 5A";
                }
            }
            else if (txtStrtdater.Text != "" && txtItem5ar.Text != "0" && (txtFlag5a.Text == "R" || txtFlag5a.Text == "A" || txtFlag5a.Text == "O" || txtFlag5a.Text == "E") && txtItem5br.Text != "0" && (txtFlag5b.Text == "R" || txtFlag5b.Text == "A" || txtFlag5b.Text == "O" || txtFlag5b.Text == "E"))
            {
                if (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B")
                {
                    txtRvitm5cr.Text = (Convert.ToInt32(txtItem5ar.Text.Replace(",", "")) + Convert.ToInt32(txtItem5br.Text.Replace(",", ""))).ToString();
                    txtFlagr5c.Text = "E";

                    //if it is mulitfamily
                    SetupCostput();
                    verify = "5C was made equal to 5A + 5B";

                }
            }
            else if (txtRvitm5cr.Text != "0" && (txtFlagr5c.Text == "R" || txtFlagr5c.Text == "A" || txtFlagr5c.Text == "O" || txtFlagr5c.Text == "E") && (txtFlag5b.Text == "I" || txtFlag5b.Text == "M" || txtFlag5b.Text == "B"))
            {
                if (txtFlag5a.Text == "I" || txtFlag5a.Text == "M" || txtFlag5a.Text == "B")
                {
                    txtItem5ar.Text = txtRvitm5cr.Text;
                    txtFlag5a.Text = "F";

                    //set up reported field
                    txtItem5ar.Text = txtItem5ar.Text;

                    txtItem5br.Text = "0";
                    txtFlag5b.Text = "B";

                    verify = "5A was made equal to 5C";
                }

            }

            else if (txtCompdater.Text != "" && (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B") && (sumvip > 0 && (notInbf.Count == 0) &&
                 (sumvip >= 0.95 * mast.Projselv && sumvip <= 1.05 * mast.Projselv)))
            {
                txtRvitm5cr.Text = sumvip.ToString();
                txtFlagr5c.Text = "F";

                txtItem5ar.Text = txtRvitm5cr.Text;
                txtFlag5a.Text = "F";

                //if it is mulitfamily
                SetupCostput();

                verify = "5C, 5A was made equal to VIPSUM";
            }
            else if (txtCompdater.Text != "" && (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B") && (sumvip > 0 && (mvss.ToList().Count() > 0) &&
                (sumvip >= 0.95 * mast.Projselv && sumvip <= 1.05 * mast.Projselv)))
            {
                txtRvitm5cr.Text = sumvip.ToString();
                txtFlagr5c.Text = "F";

                txtItem5ar.Text = txtRvitm5cr.Text;
                txtFlag5a.Text = "F";

                //if it is mulitfamily
                SetupCostput();

                verify = "5C, 5A was made equal to VIPSUM";
            }

            // If Compdate is not blank and Cumvip > Rvitm5c and Reported VIP for Completion month = 0 , set COMPDATE to last month with Reported VIP > 0, and remove the Reported VIP of 0 that was entered
            if ((txtCompdater.Text != "") && (txtFlagCompdate.Text == "R") && (sumvip >= int.Parse(txtRvitm5cr.Text.Replace(",", ""))))
            {
                MonthlyVip lastvip = mvs.monthlyViplist.Find(x => x.Date6 == txtCompdater.Text);
                if (lastvip.Vipdatar == 0 && lastvip.Vipflag == "R")
                {
                    //set compdate to previous month
                    int i = 0;
                    for (i = 0; i < mvs.monthlyViplist.Count; i++) // Loop through List with for
                    {
                        if (mvs.monthlyViplist[i].Vipdatar > 0 && (mvs.monthlyViplist[i].Vipflag == "R" || mvs.monthlyViplist[i].Vipflag == "A" || mvs.monthlyViplist[i].Vipflag == "I"))
                        {
                            txtCompdater.Text = mvs.monthlyViplist[i].Date6;
                            txtCompdater.Text = mvs.monthlyViplist[i].Date6;
                            break;
                        }
                    }

                    for (int j = i - 1; j >= 0; j--) // Loop through List with for
                    {
                        if (mvs.monthlyViplist[j].Vipdatar == 0 && mvs.monthlyViplist[j].Vipflag == "R")
                        {
                            mvs.monthlyViplist[j].Vipflag = "B";
                            mvs.monthlyViplist[j].vs = "m";
                        }
                    }
                    dgVip.Refresh();
                }
            }

            if (verify != "")
                MessageBox.Show("Verify: " + verify);

        }

        //Check the form has been changed or not 
        private bool CheckFormChanged()
        {
            /*Validation*/
            VerifyMainData();

            /*Read from screen*/
            ReadFromScreen();

            /*Check changes */
            if (Object.Equals(soc, null))
            {
                if (samp != null && mast != null && mvs != null)
                {
                    
                    if (samp.IsModified || mast.IsModified || resp.IsModified || mvs.IsModified)
                        return true;
                    else
                        return false;
                   
                }
                else
                {
                    if (resp.IsModified)
                        return true;
                    else
                        return false;
                }
            }
            else if (samp.IsModified || mast.IsModified || resp.IsModified || soc.IsModified || mvs.IsModified  )
                return true;
            else
                return false;
        }

        private bool ValidateShedCall()
        {
            if (id != null && enterPoint == TypeTFUEntryPoint.NPC)
            {
                
                //if data was entered, call resolutions cannot be Ring, Busy, Disconnected and LVM
                if (lag_changed && cbCall.SelectedIndex != 9)
                {
                    MessageBox.Show("Lag was changed. Call resolution should be Contacted");
                    cbCall.Focus();
                    cbCall.SelectedIndex = -1;
                    return false;
                }
                else if (cbCall.SelectedIndex == 0 || cbCall.SelectedIndex == 1 || cbCall.SelectedIndex == 2 || cbCall.SelectedIndex == 3 || cbCall.SelectedIndex == 6 || cbCall.SelectedIndex == 7)
                {
                    MessageBox.Show("Data was changed. Call resolution cannot be Ring, Busy, Disconnected, LVM and Promise to Report");
                    cbCall.Focus();
                    cbCall.SelectedIndex = -1;
                    return false;
                }
                
            }
            return true;
        }

        //Verify form data
        private bool ValidateFormData()
        {
            
            //check phone
            if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Phone number is invalid.");
                txtPhone.Focus();
                return false;
            }

            //check phone2
            if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Phone number is invalid.");
                txtPhone2.Focus();
                return false;
            }

            //check fax
            if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Fax number is invalid.");
                txtFax.Focus();
                return false;
            }

            //check email
            if ((txtEmail.Text.Trim().Length !=0) && !GeneralFunctions.isEmail(txtEmail.Text.Trim()))
            {
                MessageBox.Show("Email address is invalid.");
                txtEmail.Focus();
                return false;
            }

            //check weburl
            if ((txtWebAddr.Text.Trim().Length != 0) && !GeneralData.IsValidURL(txtWebAddr.Text))
            {
                MessageBox.Show("Web address is invalid.");
                txtWebAddr.Focus();
                return false;
            }

            //check city state
            if (!ValidateCityState())
            {
                txtAddr3.Focus();
                return false;
            }
            else
                resp.Rstate = rst;

            //check zip code
            if (txtZip.Text.Trim() != "")
            {
                bool isCanada = CheckAddress3IsCanada();
                if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
                {
                    MessageBox.Show("Zip Code is invalid.");
                    txtZip.Focus();
                    return false;
                }
            }
            

            //check units and bldgs
            if (txtSurvey.Text == "M")
            {
                if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < 2)
                {
                    MessageBox.Show("Units must be a number between 2 and 9999.");
                    txtUnits.Focus();
                    return false;

                }
                if (txtUnits.Text != "" && txtBldgs.Text != "")
                {
                    if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2)
                    {
                        MessageBox.Show("Units must be 2 or more times than the Bldgs.");
                        txtUnits.Focus();
                        return false;
                    }
                }

            }

            //check strtdate
            if (txtStrtdater.Modified)
            {
                if (!CheckStrtdateEnter())
                {
                    txtStrtdater.Focus();
                    return false;
                }
            }

            //check compdate 
            if (txtCompdater.Modified)
            {
                if (!CheckCompdateEnter())
                {
                    txtCompdater.Focus();
                    return false;
                }
            }

            if (txtFutcompdr.Modified)
            {
                if (!CheckFutcompdEnter())
                {
                    txtFutcompdr.Focus();
                    return false;
                }
            }

            //check Rvitm5C
            if (txtRvitm5cr.Modified)
            {
                if (!VerifyRvitm5c())
                {
                    txtRvitm5cr.Focus();
                    return false;
                }
            }

            //check item5a
            if (txtItem5ar.Modified)
            {
                if (!CheckItem5aEnter())
                {
                    txtItem5ar.Focus();
                    return false;
                }
            }

            //check item5b
            if (txtItem5br.Modified)
            {
                if (!CheckItem5bEnter())
                {
                    txtItem5br.Focus();
                    return false;
                }
            }

            if (id != null)
            {
                string sury;
                if (txtSurvey.Visible)
                    sury = txtSurvey.Text;
                else
                    sury = cbSurvey.Text;

                if (!GeneralDataFuctions.CheckUtilitiesNewTCOwner(txtNewtc.Text, sury))
                {
                    MessageBox.Show("NEWTC not valid for Ownership.");
                    return false;
                }
            }

            return true;

        }

        /*Save all Data */
        private bool SaveData()
        {
            /*save respondent */
            if (resp.IsModified)
            {
                respdata.SaveRespondentData(resp);
                
                resp.IsModified = false;
                time_factor = GeneralDataFuctions.GetTimezoneFactor(resp.Rstate);
            }

            if (id != null)
            {

                TypeDBSource dbsave = TypeDBSource.Default;
                float sampwt = mastdata.GetSampWeight(mast);

                //set fwgt
                float new_fwgt = samp.Fwgt;
                float new_oadj = samp.Oadj;

                bool report_reject = false;

                //obtain data flag from current objects
                Dataflags data_flags = cflags.GetMainFlags(samp, mast, soc, mvs, cprauditlist, vipauditlist, status_changed, owner_changed, sampwt, "report", ref new_fwgt, ref new_oadj, ref reject, ref report_reject);
                if (new_fwgt != samp.Fwgt)
                {
                    txtFwgt.Text = new_fwgt.ToString("N2");
                }

                //if it is reject case, show message
                if (report_reject)
                {
                    DialogResult result1 = MessageBox.Show("There are rejects, would you like to correct?", "Important Question", MessageBoxButtons.YesNo);
                    if (result1 == System.Windows.Forms.DialogResult.Yes)
                    {
                        cflags.BuildFlagList(data_flags.currflags, data_flags.reportflags);
                        HighlightFieldsFromFlags();
                        SetupDataFlag();
                        lblReject.Visible = true;
                        return false;
                    }
                    
                    dbsave = TypeDBSource.Hold;
                    lblReject.Visible = true;
                }
                else if (reject)
                {
                    dbsave = TypeDBSource.Hold;
                    lblReject.Visible = false;
                }
                else
                {
                    dbsave = TypeDBSource.Default;
                    lblReject.Visible = false;
                }

                /*Save sample */
                if (new_oadj != samp.Oadj)
                {
                    samp.Oadj = new_oadj;
                    if (new_oadj < 1.00)
                        samp.Oflg = "1";
                    else
                        samp.Oflg = "0";
                }
                if (new_fwgt != samp.Fwgt)
                {
                    samp.Fwgt = new_fwgt;
                    txtFwgt.Text = new_fwgt.ToString("N2");
                }
                if (samp.IsModified || (dbsource != dbsave))
                {
                    // the original data from samp, but has reject, save to hold table
                    if (dbsource == TypeDBSource.Default && dbsave == TypeDBSource.Hold)
                        sampdata.AddSampleData(samp, dbsave);
                    else
                    {
                        sampdata.SaveSampleData(samp, dbsave);

                        //the original data from hold, but there is no reject, delete the record from hold table
                        if (dbsource == TypeDBSource.Hold && dbsave == TypeDBSource.Default)
                            sampdata.DeleteSampleData(samp.Id, dbsource);
                    }
                    samp.IsModified = false;
                }

                /*save master */
                if (mast.IsModified)
                {
                    mastdata.SaveMasterData(mast);
                    mast.IsModified = false;
                }

                /*save month vip */
                if ((mvs.IsModified) || (dbsource != dbsave))
                {
                    // the original data from default, but has reject, save to hold table
                    if (dbsource == TypeDBSource.Default && dbsave == TypeDBSource.Hold)
                    {
                        mvsdata.AddMonthlyVips(mvs, dbsave);
                        dbsource = TypeDBSource.Hold;
                    }
                    // the original data from hold, but there is no reject, delete the record from hold table and month vip data table
                    //add back in month vip data
                    else if (dbsource == TypeDBSource.Hold && dbsave == TypeDBSource.Default)
                    {
                        mvsdata.DeleteMonthlyVips(mvs.Id, dbsave);
                        mvsdata.DeleteMonthlyVips(mvs.Id, dbsource);

                        mvsdata.AddMonthlyVips(mvs, dbsave);
                        dbsource = TypeDBSource.Default;
                    }
                    else
                    {
                        mvsdata.SaveMonthlyVips(mvs, dbsave);
                    }

                    mvs.ResetStatus();
                }

                if (mast.Owner == "M")
                {
                    //save soc 
                    if (soc.IsModified)
                    {
                        socdata.SaveSocData(soc);
                        soc.IsModified = false; 
                    }
                }

                ///*Save flags */
                flagdata.SaveflagsData(id, data_flags.currflags, data_flags.reportflags);

                ///*Save cpraudit data, clear Cprauditlist */
                if (cprauditlist.Count > 0)
                {
                    foreach (Cpraudit element in cprauditlist)
                    {
                        if (element.Varnme == "CONTRACT" || element.Varnme == "STATUS")
                        {
                            int rowIndex = -1;
                            foreach (DataGridViewRow row in dgProject.Rows)
                            {
                                if (row.Cells[0].Value.ToString().Equals(element.Id))
                                {
                                    rowIndex = row.Index;
                                    break;
                                }
                            }

                            if (rowIndex >= 0)
                            {
                                if (element.Varnme == "CONTRACT")
                                    dgProject.Rows[rowIndex].Cells["CONTRACT"].Value = element.Newval;
                                else
                                    dgProject.Rows[rowIndex].Cells["STATUS"].Value = element.Newval;
                            }
                               
                        }
                        auditData.AddCprauditData(element);
                    }
                    cprauditlist.Clear();
                }

                ///*Save vipaudit data, clear vipauditlist */
                if (vipauditlist.Count > 0)
                {
                    foreach (Vipaudit element in vipauditlist)
                    {
                        auditData.AddVipauditData(element);
                    }
                    vipauditlist.Clear();
                }
            }

            /*Save rspaudit data, clear rspauditlist */
            if (Respauditlist.Count > 0)
            {
                foreach (Respaudit element in Respauditlist)
                {
                    auditData.AddRspauditData(element);
                }
                Respauditlist.Clear();
            }

            //check vip satisfied, update project datagrid, complete column

            if (id != null)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow row in dgProject.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(id))
                    {
                        rowIndex = row.Index;
                        break;
                    }
                }
                if (CheckVipsatisfied())
                {
                   if (rowIndex >= 0)
                        dgProject.Rows[rowIndex].Cells[3].Value = "Y";
                } 
                else
                {
                    if (rowIndex >= 0)
                        dgProject.Rows[rowIndex].Cells[3].Value = " ";
                }
                                      
            }
  
            //reseet modified flags
            txtRvitm5cr.Modified = false;
            txtItem5ar.Modified = false;
            txtItem5br.Modified = false;
            txtStrtdater.Modified = false;
            txtCompdater.Modified = false;
            txtFutcompdr.Modified = false;

            //lag_changed = false;
            owner_changed = 0;
            status_changed = false;

            data_saved = true;

            return true;
        }


        //Save Sched call data when the user click next row in project list or Next TFU
        private bool SaveSchedCall()
        {

            //if lag changed,  call resolution will be "Contacted"
            if (lag_changed)
            {
                //save all other cases
                foreach (DataGridViewRow row in dgProject.Rows)
                {
                    if (row.Cells["STATUS"].Value.ToString() == "1")
                    {
                        string proid = row.Cells["ID"].Value.ToString();
                        Schedcall sscc = scheddata.GetSchedCallData(proid);
                        if (sscc == null)
                        {
                            sscc = new Schedcall(proid);
                            sscc.Added = "Y";
                            sscc.Coltec = resp.Coltec;
                            sscc.Callreq = "N";
                            sscc.Calltpe = "W";
                            sscc.Callstat = "";

                            //set priority
                            if (resp.Coltec == "P" && samp.Status != "7")
                            {
                                sscc.Priority = "11";
                                sscc.PriorityDesc = "Regualar Phone";
                            }
                            else if (resp.Coltec == "C" && samp.Status != "7")
                            {
                                sscc.Priority = "21";
                                sscc.PriorityDesc = "Regular - Cent";
                            }
                            else if (resp.Coltec == "F" && samp.Status != "7")
                            {
                                sscc.Priority = "22";
                                sscc.PriorityDesc = "Regular - Mail";
                            }
                            else
                            {
                                sscc.Priority = "23";
                                sscc.PriorityDesc = "Coltec I,A,S and Status 7";
                            }
                        }

                        if (CheckVipstatifiedForID(proid))
                        {
                            sscc.Callstat = "V";
                            sscc.Callreq = "N";
                            sscc.Complete = "Y";
                        }
                        else
                        {
                            sscc.Callstat = "9";
                            sscc.Callreq = "Y";
                            sscc.Complete = "N";
                        }

                        if (enterPoint == TypeTFUEntryPoint.NPC)
                            schedcall.Accescde = "P";
                        else
                            schedcall.Accescde = resp.Coltec;

                        sscc.Accesday = schedcall.Accesday;
                        sscc.Accesnme = schedcall.Accesnme;
                       
                        sscc.Accestms = schedcall.Accestms;
                        sscc.Accestme = DateTime.Now.ToString("HHmmss");
                        sscc.Apptdate = schedcall.Apptdate;
                        sscc.Apptends = schedcall.Apptends;
                        sscc.Appttime = schedcall.Appttime;
                        sscc.Callcnt = sscc.Callcnt + 1;

                        sscc.Calltpe = schedcall.Calltpe;

                        scheddata.SaveSchedcallData(sscc);
                        finished_cases.Add(proid);

                        Schedhist sh = new Schedhist(proid);
                        sh.Owner = row.Cells["OWNER"].Value.ToString();
                        sh.Callstat = sscc.Callstat;
                        sh.Accesday = DateTime.Now.ToString("MMdd");
                        sh.Accestms = schedcall.Accestms;
                        sh.Accestme = DateTime.Now.ToString("HHmmss");
                        sh.Accesnme = UserInfo.UserName;
                        if (enterPoint == TypeTFUEntryPoint.NPC)
                            sh.Accescde = "P";
                        else
                            sh.Accescde = resp.Coltec;

                        scheddata.AddSchedHistData(sh);
                    }
                    
                }
                lag_changed = false;
                return true;
            }
            
            //Save sched call
          
            //for search, form entry, no call resolution
            if (cbCall.SelectedIndex == -1)
            {
                schedcall.Callstat = "";
                bool vip_sat = CheckVipsatisfied();
                 if (samp.Status != "1" )
                {
                    schedcall.Complete = "Y";
                    schedcall.Callreq = "N";
                }
                else if (!reject && (vip_sat || samp.Compdater != ""))
                {
                    schedcall.Complete = "Y";
                    schedcall.Callstat = "V";
                    schedcall.Callreq = "N";
                }
                 else
                {
                    schedcall.Apptdate = "";
                    schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                    schedcall.Apptends = "1700";
                }
              
                schedcall.Accescde = resp.Coltec;
            }
            // for form entry, tfu, has call reolution
            else if (cbCall.SelectedIndex != -1)
            {
                schedcall.Callstat = (cbCall.SelectedIndex).ToString();

                if (schedcall.Callstat == "0")
                {
                    //Referral
                    schedcall.Apptdate = "";
                    schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                    schedcall.Apptends = "1700";
                    schedcall.Calltpe = "W";
                    schedcall.Callreq = "N";
                }
                else if (schedcall.Callstat == "1" || schedcall.Callstat == "2")
                {
                    //reschedule in a hour later.
                    schedcall.Apptdate = DateTime.Now.ToString("MMdd");
                    schedcall.Callreq = "Y";
                    schedcall.Complete = "N";
                    if (DateTime.Now.Hour < 16)
                    {
                        schedcall.Appttime = DateTime.Now.AddHours(1).ToString("HHmm");
                        schedcall.Apptends = "1700";
                    }
                    else
                    {
                        if (DateTime.Today.Day == cut_day)
                        {
                            schedcall.Callreq = "N";
                            schedcall.Complete = "Y";
                        }
                        else
                        {
                            schedcall.Apptdate = GeneralFunctions.GetNextBusinessDay(DateTime.Now);
                            schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                            schedcall.Apptends = "1700";
                        }
                    }
                    schedcall.Calltpe = "S";
                }
                else if (schedcall.Callstat == "3")
                {
                    //sched to next work day
                    if (DateTime.Today.Day == cut_day)
                    {
                        schedcall.Callreq = "N";
                        schedcall.Complete = "Y";
                    }
                    else
                    {
                        schedcall.Apptdate = GeneralFunctions.GetNextBusinessDay(DateTime.Now);
                        schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                        schedcall.Apptends = "1700";
                        schedcall.Callreq = "Y";
                        schedcall.Complete = "N";
                    }
                    schedcall.Calltpe = "S";
                }
                else if (schedcall.Callstat == "4")
                {
                    //if sched is refusal, set complete 
                    schedcall.Complete = "Y";
                    schedcall.Apptdate = "";
                    schedcall.Appttime = "0800";
                    schedcall.Apptends = "1700";
                    schedcall.Calltpe = "W";
                    schedcall.Callreq = "N";

                }
                else if (schedcall.Callstat == "5")
                {
                    schedcall.Apptdate = sApptdate;
                    schedcall.Appttime = sAppttime;
                    schedcall.Apptends = sApptend;
                    if (sAppttime == sApptend)
                        schedcall.Calltpe = "H";
                    else
                        schedcall.Calltpe = "S";
                    schedcall.Complete = "N";
                    schedcall.Callreq = "Y";

                }
                else if (schedcall.Callstat == "6")
                {
                    //if current day +3 greater than cut day, don't need call
                    if (DateTime.Today.AddDays(3).Day > cut_day)
                    {
                        schedcall.Callreq = "N";
                        schedcall.Complete = "Y";
                    }
                    else
                    {
                        //left message, reschedule to next three work day
                        string first_day = GeneralFunctions.GetNextBusinessDay(DateTime.Now);
                        string sec_day = GeneralFunctions.GetNextBusinessDay(new DateTime(DateTime.Now.Year, Convert.ToInt32(first_day.Substring(0, 2)), Convert.ToInt32(first_day.Substring(2, 2))));
                        string third_day = GeneralFunctions.GetNextBusinessDay(new DateTime(DateTime.Now.Year, Convert.ToInt32(sec_day.Substring(0, 2)), Convert.ToInt32(sec_day.Substring(2, 2))));

                        schedcall.Apptdate = third_day;
                        schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                        schedcall.Apptends = "1700";

                        int count = schedcall.LVMcnt;
                        schedcall.LVMcnt = count + 1;

                        //if 3 times left message, set call req to "N"
                        if (schedcall.LVMcnt >= 3)
                        {
                            schedcall.Callreq = "N";
                            schedcall.Complete = "Y";
                        }
                        else
                        {
                            schedcall.Complete = "N";
                            schedcall.Callreq = "Y";
                        }
                    }
                    schedcall.Calltpe = "S";
                }
                else if (schedcall.Callstat == "7")
                {
                    //if current day +3 greater than cut day, don't need call
                    if (DateTime.Today.AddDays(3).Day > cut_day)
                    {
                        schedcall.Callreq = "N";
                        schedcall.Complete = "Y";
                    }
                    else
                    {
                        //if sched is promise to report, reschedule to three work days later
                        string first_day = GeneralFunctions.GetNextBusinessDay(DateTime.Now);
                        string sec_day = GeneralFunctions.GetNextBusinessDay(new DateTime(DateTime.Now.Year, Convert.ToInt32(first_day.Substring(0, 2)), Convert.ToInt32(first_day.Substring(2, 2))));
                        string third_day = GeneralFunctions.GetNextBusinessDay(new DateTime(DateTime.Now.Year, Convert.ToInt32(sec_day.Substring(0, 2)), Convert.ToInt32(sec_day.Substring(2, 2))));

                        schedcall.Apptdate = third_day;
                        schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                        schedcall.Apptends = "1700";
                        schedcall.Callreq = "Y";
                        schedcall.Complete = "N";
                    }
                    schedcall.Calltpe = "S";
                }
                else if (schedcall.Callstat == "8")
                {
                    // new poc
                    schedcall.Complete = "Y";
                    schedcall.Apptdate = "";
                    schedcall.Appttime = "0800";
                    schedcall.Apptends = "1700";
                    schedcall.Callreq = "N";
                }
                else if (schedcall.Callstat == "9")
                {
                    //if vip satisfied, set call resolution to Contacted
                    if (samp.Status != "1" )
                    {
                        schedcall.Complete = "Y";
                        schedcall.Callreq = "N";
                    }
                    else if (!reject && CheckVipsatisfied() || samp.Compdater != "")
                    {
                        schedcall.Complete = "Y";
                        schedcall.Callstat = "V";
                        schedcall.Callreq = "N";
                    }
                    else
                    {
                        //if contacted, but not satisfied, rescheduled to next work day
                        if (DateTime.Today.Day == cut_day)
                        {
                            schedcall.Callreq = "N";
                            schedcall.Complete = "Y";
                        }
                        else
                        {
                            schedcall.Apptdate = GeneralFunctions.GetNextBusinessDay(DateTime.Now);
                            schedcall.Appttime = (8 + time_factor).ToString("00") + "00";
                            schedcall.Apptends = "1700";
                            schedcall.Calltpe = "S";
                            schedcall.Callreq = "Y";
                            schedcall.Complete = "N";
                        }
                    }
                }

                if (enterPoint == TypeTFUEntryPoint.NPC)
                    schedcall.Accescde = "P";
                else
                    schedcall.Accescde = resp.Coltec;

                schedcall.Callcnt = schedcall.Callcnt + 1;
                 
            }
               
            schedcall.Accesday = DateTime.Now.ToString("MMdd");
            schedcall.Accestme = DateTime.Now.ToString("HHmmss");
            schedcall.Accesnme = UserInfo.UserName;

            scheddata.SaveSchedcallData(schedcall);
            schedcall.IsModified = false;

            //add record to sched_hist table
            Schedhist shist = new Schedhist(samp.Id);
            shist.Owner = mast.Owner;
            shist.Callstat = schedcall.Callstat;
            shist.Accesday = DateTime.Now.ToString("MMdd");
            shist.Accestms = schedcall.Accestms;
            shist.Accestme = DateTime.Now.ToString("HHmmss");
            shist.Accesnme = UserInfo.UserName;
           
            shist.Accescde = schedcall.Accescde;
           
            if (enterPoint == TypeTFUEntryPoint.NPC)
            {
                scheddata.AddSchedHistData(shist);
                if( !IsFinishedCases(schedcall.Id))
                    finished_cases.Add(schedcall.Id);
            }
            else 
            {
                scheddata.AddSchedHistData(shist);
            }
            
            if (enterPoint == TypeTFUEntryPoint.NPC)
            {
                //Save schedcall for all cases
                if (( schedcall.Callstat == "0" || schedcall.Callstat == "1" || schedcall.Callstat == "2" || schedcall.Callstat == "3" || schedcall.Callstat == "5" ||schedcall.Callstat == "6" || schedcall.Callstat == "7" || schedcall.Callstat == "8") && (projectlist.Count > 1))
                {
                    //save all other cases
                    foreach (TFUProject pro in projectlist)
                    {
                        if (pro.Id != id && !IsFinishedCases(pro.Id))
                        {
                            Schedcall sscc = scheddata.GetSchedCallData(pro.Id);
                            if (sscc.Callstat != "V")
                            {
                                sscc.Accescde = schedcall.Accescde;
                                sscc.Accesday = schedcall.Accesday;
                                sscc.Accesnme = schedcall.Accesnme;
                                sscc.Accestme = DateTime.Now.ToString("HHmmss");
                                sscc.Accestms = schedcall.Accestms;
                                sscc.Apptdate = schedcall.Apptdate;
                                sscc.Apptends = schedcall.Apptends;
                                sscc.Appttime = schedcall.Appttime;
                                sscc.Callcnt = sscc.Callcnt + 1;
                                sscc.Callstat = schedcall.Callstat;
                                sscc.Callreq = schedcall.Callreq;
                                sscc.Calltpe = schedcall.Calltpe;
                                sscc.Complete = schedcall.Complete;

                                if (schedcall.Callstat == "6")
                                {
                                    sscc.LVMcnt = sscc.LVMcnt + 1;
                                    if (sscc.LVMcnt >= 3)
                                    {
                                        schedcall.Callreq = "N";
                                        schedcall.Complete = "Y";
                                    }
                                }

                                scheddata.SaveSchedcallData(sscc);

                                Schedhist sh = new Schedhist(pro.Id);
                                sh.Owner = pro.Owner;
                                sh.Callstat = schedcall.Callstat;
                                sh.Accesday = DateTime.Now.ToString("MMdd");
                                sh.Accestms = schedcall.Accestms;
                                sh.Accestme = DateTime.Now.ToString("HHmmss");
                                sh.Accesnme = UserInfo.UserName;
                                sh.Accescde = schedcall.Accescde;

                                scheddata.AddSchedHistData(sh);
                            }
                            finished_cases.Add(pro.Id);
                        }
                    }
                }
            }

            //update call stat combo
            if (enterPoint == TypeTFUEntryPoint.NPC || enterPoint == TypeTFUEntryPoint.FORM)
            {
                //if call resolution is 4, update status to '8'
                if (schedcall.Callstat == "4")
                {
                    dbsource = sampdata.GetDatabaseSource(id);

                    string old_status = samp.Status;
                    string new_status = "7";

                    if (samp.Strtdater == "" || samp.Rvitm5cr == 0 || samp.Futcompd == "")
                        new_status = "8";

                    DialogResult result1 = MessageBox.Show("This will set the current projects status to " + new_status + ", do you want to continue", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        samp.Status = new_status;
                        sampdata.SaveSampleData(samp, dbsource);

                        //add audit record
                        Cpraudit element = new Cpraudit();
                        element.Id = id;
                        element.Varnme = "STATUS";
                        element.Newval = samp.Status;
                        element.Oldval = old_status;
                        element.Oldflag = "";
                        element.Newflag = "";
                        element.Progdtm = DateTime.Now;
                        element.Usrnme = UserInfo.UserName;
                        auditData.AddCprauditData(element);
                    }
                }
            }

            sched_data_saved = true;

            return true;
        }

        //check the case is finished case or not
        private bool IsFinishedCases(string id)
        {
            if (finished_cases.Count == 0)
                return false;

            foreach (string item in finished_cases)
            {
                if (item.Contains(id))
                    return true;
            }
            return false;
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            SavingData();
        }

        //Save data
        private void SavingData()
        {
            if (ValidateFormData())
            {
                if (CheckFormChanged())
                {
                    if (enterPoint == TypeTFUEntryPoint.NPC)
                    {
                        if (!ValidateShedCall())
                            return;
                    }

                    if (SaveData())
                    {
                        if (id != null)
                        {
                            flagdata = new CprflagsData();
                            Dataflags flags = flagdata.GetCprflagsData(id);
                            cflags = new Cprflags(flags, mast.Owner, "report");
                            if (reject)
                                lblReject.Visible = true;
                            else
                                lblReject.Visible = false;

                            HighlightFieldsFromFlags();
                            SetupDataFlag();
                        }
                       
                    }
                }
            }
            
        }

        private string GetCallSchedText(string call_stat)
        {
            string call_sched_text = string.Empty;

            if (call_stat == "")
                call_sched_text = "NO CALL ATTEMPTS";
            else if (call_stat == "V")
                call_sched_text = "VIP SATISFIED";
            else if (call_stat == "0")
                call_sched_text = "REFERRAL";
            else if (call_stat == "1")
                call_sched_text = "RING";
            else if (call_stat == "2")
                call_sched_text = "BUSY";
            else if (call_stat == "3")
                call_sched_text = "DISCONNECTED";
            else if (call_stat == "4")
                call_sched_text = "REFUSED";
            else if (call_stat == "5")
                call_sched_text = "RESCHEDULE";
            else if (call_stat == "6")
                call_sched_text = "LVM";
            else if (call_stat == "7")
                call_sched_text = "PROMISED to REPORT";
            else if (call_stat == "8")
                call_sched_text = "NEWPOC";
            else
                call_sched_text = "CONTACTED";

            return call_sched_text;

        }

        //Highlight textbox and gridfields, set notification messages
        private void HighlightFieldsFromFlags()
        {
            List<string> messagelist = new List<string>();
            string process_date = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            bool flag4_set = false;

            //clean highlighed textbox and data grid
            UnHighlightTextBox(txtRvitm5cr);
            UnHighlightTextBox(txtItem5ar);
            UnHighlightTextBox(txtItem5br);
            UnHighlightTextBox(txtItem6r);
            UnHighlightTextBox(txtCapexpr);
            UnHighlightTextBox(txtCompdater);
            UnHighlightTextBox(txtStrtdater);
            UnHighlightTextBox(txtFutcompdr);
            UnHighlightDataGrid();

            if (cflags.displayFlaglist.Count() > 0)
            {
                string message = string.Empty;
                string date6 = string.Empty;

                //get flag title and highlight related text box
                foreach (var value in cflags.displayFlaglist)
                {
                    if (value.flagno == 0)
                    {
                        value.title = "FLAG: Status Code Change";
                    }
                    else if (value.flagno == 1)
                    {
                        value.title = "FLAG: Ownership Code Change";
                    }
                    else if (value.flagno == 2)
                    {
                        value.title = "REJECT: ITEM5A + ITEM5B =/= RVITM5C";
                        HighlightTextBox(txtRvitm5cr);
                    }
                    else if (value.flagno == 3)
                    {
                        if (samp.Strtdater != "")
                        {
                            if (Convert.ToInt32(samp.Strtdater) < Convert.ToInt32(process_date) && mvs.GetSumMons() >0)
                            {
                                if (samp.Compdater != "")
                                {
                                    List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdater, samp.Compdater);
                                    MonthlyVip mv = null;
                                    foreach (var v in mvlist)
                                    {
                                        if (mv != null)
                                        {
                                            if (v.Vipflag == "M" && mv.Vipflag !="M")
                                            {
                                                value.title = "REJECT: Impute Precedes Reported for " + mv.Date8;
                                                HighlightDataGrid(mv.Date6);
                                                flag4_set = true;
                                                break;
                                            }
                                        }
                                        mv = v;
                                    }
                                    if (!flag4_set)
                                    {
                                        for (int i = mvlist.Count - 1; i >= 0; i--)
                                        {
                                            var v = mvlist[i];
                                            if (v.Date6 != process_date)
                                            {
                                                if (v.Vipdata == 0 && v.Vipflag == "B")
                                                {
                                                    value.title = "REJECT: Blank VIP for " + v.Date8;
                                                    HighlightDataGrid(v.Date6);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                 }
                                else
                                {
                                    List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdate, DateTime.Now.AddMonths(-1).ToString("yyyyMM"));
                                    MonthlyVip mv = null;
                                    foreach (var v in mvlist)
                                    {
                                        if (mv != null)
                                        {
                                            if (v.Vipflag == "M" && mv.Vipflag != "M" && mv.Vipflag != "B")
                                            {
                                                value.title = "REJECT: Impute Precedes Reported for " + mv.Date8;
                                                HighlightDataGrid(mv.Date6);
                                                flag4_set = true;
                                                break;
                                            }
                                        }
                                        mv = v;
                                    }
                                    if (!flag4_set)
                                    {
                                        for (int i = mvlist.Count - 1; i >= 0; i--)
                                        {
                                            var v = mvlist[i];
                                            if (v.Date6 != process_date)
                                            {
                                                if (v.Vipdata == 0 && v.Vipflag == "B")
                                                {
                                                    value.title = "REJECT: Blank VIP for " + v.Date8;
                                                    HighlightDataGrid(v.Date6);
                                                    break;
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }

                    }
                    else if (value.flagno == 4)
                    {
                        value.title = "REJECT: VIP of 0 in the start month";
                        HighlightDataGrid(samp.Strtdater);
                    }
                    else if (value.flagno == 5)
                    {
                        value.title = "REJECT: VIP of 0 in the completion month";
                        HighlightDataGrid(samp.Compdater);
                    }
                    else if (value.flagno == 6)
                    {
                        value.title = "REJECT: Unmatched Projected Dates";
                       // HighlightTextBox(txtSumMnths);
                    }
                    else if (value.flagno == 7)
                    {
                        value.title = "REJECT: RVITM5C reported 0";
                        HighlightTextBox(txtRvitm5cr);
                    }
                    else if (value.flagno == 8)
                    {
                        value.title = "REJECT: Project started prior to 24 months ago, cannot enter data";
                        HighlightDataGrid(samp.Strtdater);
                    }
                    else if (value.flagno == 9)
                    {
                        foreach (var v in mvs.GetMonthVipList(samp.Strtdate, process_date))
                        {
                            if (v.Vipdata < 0)
                            {
                                value.title = "REJECT: Negative VIP for " + v.Date8;
                                HighlightDataGrid(v.Date6);
                                break;
                            }
                        }

                    }
                    else if (value.flagno == 10)
                    {
                        value.title = "REJECT: Completion date with no RVITM5C";
                        HighlightTextBox(txtRvitm5cr);
                    }
                    else if (value.flagno == 11)
                    {
                        value.title = "REJECT: Item5b without Item5a";
                        HighlightTextBox(txtItem5ar);
                    }
                    else if (value.flagno == 12)
                    {
                        value.title = "FLAG: Early Start Date";
                      
                    }
                    else if (value.flagno == 13)
                    {
                        value.title = "FLAG: RVITM5C >= 3 X SELVAL";
                      
                    }
                    else if (value.flagno == 14)
                    {
                        value.title = "FLAG: Selection value >= 3 X RVITM5C";
                    
                    }
                    else if (value.flagno == 15)
                    {
                        value.title = "FLAG: Cumulative VIP = " + txtCompr.Text + "% of RVITM5C, Revise RVITM5C";
                    
                    }
                    else if (value.flagno == 16)
                    {
                        if (mast.Owner != "M")
                        {
                            var mv25 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 25 select mp;
                            if (mv25.ToList().Count > 0)
                            {
                                MonthlyVip m = mv25.ToList().First();
                                value.title = "FLAG: VIP = " + m.Pct5cr + "% of RVITM5C";
                               
                            }
                        }
                        else
                        {
                            var mv20 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 20 select mp;
                            if (mv20.ToList().Count > 0)
                            {
                                MonthlyVip m = mv20.ToList().First();
                                value.title = "FLAG: VIP = " + m.Pct5cr + "% of RVITM5C";
                               
                            }
                        }
                    }
                    else if (value.flagno == 17)
                    {
                        var mv50 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 50 select mp;
                        if (mv50.ToList().Count > 0)
                        {
                            MonthlyVip m = mv50.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5cr + "% of RVITM5C";
                           
                        }
                    }
                    else if (value.flagno == 18)
                    {
                        var mv75 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 75 select mp;
                        if (mv75.ToList().Count > 0)
                        {
                            MonthlyVip m = mv75.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5cr + "% of RVITM5C";
                          
                        }
                    }
                    else if (value.flagno == 19)
                    {
                        value.title = "FLAG: Outlier: Old wt = " + old_fwgt + " New wt = " + txtFwgt.Text;
                     
                    }
                    else if (value.flagno == 20)
                    {
                        value.title = "FLAG: Item6 >= 50% of RVITM5C";
                       
                    }
                    else if (value.flagno == 21)
                    {
                        value.title = "FLAG: Manufacturing project with no capital expenditures";
                     
                    }
                    else if (value.flagno == 22)
                    {
                        value.title = "FLAG: RVITM5C + ITEM6 = CAPEXP";
                      
                    }
                    else if (value.flagno == 23)
                    {
                        value.title = "FLAG: CAPEXP > RVITM5C";
                    
                    }
                    else if (value.flagno == 24)
                    {
                        value.title = "FLAG: Imputed VIP over RVITM5C";
                     
                    }
                    else if (value.flagno == 25)
                    {
                        string process_date3 = DateTime.Now.AddMonths(-4).ToString("yyyyMM");
                        var last3 = from mp in mvs.GetMonthVipList(process_date3, process_date) where (mp.Vipdatar == 0) select mp;
                        MonthlyVip m = last3.ToList().First();
                        value.title = "FLAG: VIP of 0 in the last 3 months";
                      
                    }
                    else if (value.flagno == 26)
                    {
                        value.title = "FLAG: Completed before selected";
                    
                    }
                    else if (value.flagno == 27)
                    {
                        value.title = "FLAG: Completed W/CUM VIP < 95% of RVITM5C";
                      
                    }
                    else if (value.flagno == 28)
                    {
                        value.title = "FLAG: No start date, projected completion date or RVITM5C";
                    
                    }
                    else if (value.flagno == 29)
                    {
                        value.title = "FLAG: RVITM5C is 900% of previous reported value";
                     
                    }
                    else if (value.flagno == 30)
                    {
                        value.title = "FLAG: Item6 is 900% of previous reported value";
                     
                    }
                    else if (value.flagno == 31)
                    {
                        value.title = "FLAG: Capex is 900% of previous reported value";
                     
                    }
                    else if (value.flagno == 32)
                    {
                        value.title = "FLAG: VIP is 900% of previous reported value";
                        
                    }
                    else if (value.flagno == 33)
                    {
                        value.title = "FLAG: CUMVIP with no Rvitm5c";
                   
                    }
                    else if (value.flagno == 34)
                    {
                        value.title = "FLAG: Projected Completion date does not align with percent complete";
                   
                    }
                    else if (value.flagno == 35)
                    {
                        //no need
                    }
                    else if (value.flagno == 36)
                    {
                        value.title = "FLAG: Within Survey Ownership Change";
                    }
                    else if (value.flagno == 37)
                    {
                        value.title = "REJECT: Reported VIP With No Start Date";
                    }
                    else if (value.flagno == 38)
                    {
                        value.title = "FLAG: Centurion Comments Field Contains Data";
                    }
                    else if (value.flagno == 39)
                    {
                        value.title = "FLAG: Outside of Range " + txtCostpu.Text + ", Check 5C and Units";
                    }
                    else if (value.flagno == 40)
                    {
                        value.title = "FLAG: Cumulative VIP = " + txtCompr.Text + "% of RVITM5C, Revise RVITM5C";
                
                    }
                    else if (value.flagno == 41)
                    {
                        var mv20 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 20 select mp;
                        if (mv20.ToList().Count > 0)
                        {
                            MonthlyVip m = mv20.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5cr + "% of RVITM5C";
                    
                        }
                    }
                    else if (value.flagno == 42)
                    {
                        var mv50 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 50 select mp;
                        if (mv50.ToList().Count > 0)
                        {
                            MonthlyVip m = mv50.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5cr + "% of RVITM5C";
                   
                        }
                    }
                    else if (value.flagno == 43)
                    {
                        var mv75 = from mp in mvs.monthlyViplist where mp.Pct5cr >= 75 select mp;
                        if (mv75.ToList().Count > 0)
                        {
                            MonthlyVip m = mv75.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5c + "% of RVITM5C";
                       
                        }
                    }
                    else if (value.flagno == 44)
                    {
                        value.title = "FLAG: Centurion Contact Data Does Not Match Current";
                    }
                    else if (value.flagno == 45)
                    {
                        value.title = "FLAG: Reported Vip blanked out in Centurion";
                    }
                    else if (value.flagno == 46)
                    {
                        value.title = "FLAG: Analyst Data Updated by Reported";
                    }
                    else if (value.flagno == 47)
                    {
                        value.title = "FLAG: VIP = 999% of RVITM5C";
                    }

                }
            }

        }

        //highlight text box
        private void HighlightTextBox(TextBox tb)
        {
            tb.BackColor = Color.Red;
            tb.ForeColor = Color.White;
        }

        //clean up highlighted text box
        private void UnHighlightTextBox(TextBox tb)
        {
            if (tb.ReadOnly == false)
                tb.BackColor = Color.White;
            else
                tb.BackColor = System.Drawing.SystemColors.Control;
            tb.ForeColor = Color.Black;
        }

        //Highlight a row vipdata
        private void HighlightDataGrid(string date6)
        {
            foreach (DataGridViewRow row in dgVip.Rows)
            {
                if (row.Cells[0].Value.ToString() == date6)
                {
                    row.Cells[2].Style.BackColor = Color.Red;
                    row.Cells[2].Style.ForeColor = Color.White;
                    break;
                }
            }
        }

        //Clear highlight in data grid
        private void UnHighlightDataGrid()
        {
            foreach (DataGridViewRow row in dgVip.Rows)
            {
                row.Cells[2].Style.BackColor = Color.White;
                row.Cells[2].Style.ForeColor = Color.Black;

            }
        }
        Bitmap memoryImage;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex != 0)
            {
                tabControl2.SelectedIndex = 0;
                tabControl2.SelectedTab.Refresh();
            }

            BeginInvoke(new PrintScreenDelegate(PrintData));
                
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            //wait screen refresh
            System.Threading.Thread.Sleep(500);

            //print screen
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();

            //print monthly vip data
            DateTime dt = DateTime.Today;

            //keep 5 years monthly vip dta
            string enddate = dt.AddMonths(-60).ToString("yyyyMM");

            List<MonthlyVip> dbp = (List<MonthlyVip>)dgVip.DataSource;
            List<MonthlyVip> mvl = new List<MonthlyVip>();
            foreach (MonthlyVip value in dbp)
            {
                if (Convert.ToInt32(value.Date6) >= Convert.ToInt32(enddate))
                {
                    mvl.Add(value);
                }
            }

            dgVipPrint.DataSource = null;
            dgVipPrint.DataSource = mvl;
            SetDgVip(dgVipPrint);
            
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Monthly VIP Data";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Monthly VIP Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            printer.PrintDataGridViewWithoutDialog(dgVipPrint);

            Cursor.Current = Cursors.Default;
        }
        
        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            frmMarkCasesPopup popup = new frmMarkCasesPopup();
            popup.Id = id;
            popup.Respid = samp.Respid;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();

            pmarklist.Clear();
            rmarklist.Clear();

            //refresh mark grid
            pmark = pmarkdata.GetProjmarkData(id);
            pmarklist.Add(pmark);
            rmark = rmarkdata.GetRespmarkData(RespId);
            rmarklist.Add(rmark);

            if (pmark != null)
            {
                dgPmark.DataSource = null;
                dgPmark.DataSource = pmarklist;
                if (tabControl2.SelectedIndex == 4)
                {
                    if (dgPmark.Rows.Count > 0)
                    {
                        dgPmark.Columns[0].Visible = false;
                        dgPmark.Columns[1].Visible = false;
                        dgPmark.Columns[2].Width = 60;
                        dgPmark.Columns[2].HeaderText = "USER";
                        dgPmark.Columns[3].HeaderText = "MARK NOTE";
                        
                    }
                }
            }
            else
            {
                dgPmark.DataSource = null;
            }

            if (rmark != null)
            {
                dgRmark.DataSource = null;
                dgRmark.DataSource = rmarklist;
                if (tabControl2.SelectedIndex == 5)
                {
                    if (dgRmark.Rows.Count > 0)
                    {
                        dgRmark.Columns[0].Visible = false;
                        dgRmark.Columns[1].Visible = false;
                        dgRmark.Columns[2].Width = 60;
                        dgRmark.Columns[2].HeaderText = "USER";
                        dgRmark.Columns[3].HeaderText = "MARK NOTE";

                    }
                }
            }
            else
            {
                dgRmark.DataSource = null;
            }
            if (CheckMarkExists())
                lblMark.Visible = true;
            else
                lblMark.Visible = false;
        }

        //check mark notes exists or not
        private bool CheckMarkExists()
        {
            bool mark_exist = false;
            DataTable dtpMark = pmarkdata.GetProjMarks(id);
            if (dtpMark != null && dtpMark.Rows.Count > 0)
                mark_exist = true;
            else
            {
                DataTable dtrMark = rmarkdata.GetRespMarks(samp.Respid);
                if (dtrMark != null && dtrMark.Rows.Count > 0)
                    mark_exist = true;
            }

            return mark_exist;
        }

        private void btnSlip_Click(object sender, EventArgs e)
        {
            frmSlipDisplay fSD = new frmSlipDisplay();
            fSD.Id = txtId.Text;
            fSD.Dodgenum = mast.Dodgenum;
            fSD.StartPosition = FormStartPosition.CenterParent;
            fSD.ShowDialog();  //show child
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            if (editable)
            {
                if (id != null)
                {
                    if (cbCall.SelectedIndex == -1 && enterPoint == TypeTFUEntryPoint.NPC)
                    {
                        MessageBox.Show("Please enter a Call resolution");
                        cbCall.Focus();
                        return;
                    }

                    if (!ValidateFormData())
                        return;

                    if (CheckFormChanged())
                    {
                        if (enterPoint == TypeTFUEntryPoint.NPC)
                        {
                            if (!ValidateShedCall())
                                return;
                        }

                        if (!SaveData())
                            return;
                    }

                    if ((data_saved ||(schedcall.IsModified && !sched_data_saved)) && enterPoint != TypeTFUEntryPoint.SEARCH)
                    {
                        if (!SaveSchedCall())
                        {
                            return;
                        }
                    }
                }

                if (enterPoint == TypeTFUEntryPoint.NPC)
                {
                    if (finished_cases.Count > 0 && projectlist.Count > finished_cases.Count)
                    {
                        //check satisified cases
                        IEnumerable<TFUProject> projectlistY = from j in projectalllist
                                                               where (j.Satisfied == "Y")
                                                               select j;

                        string call_resolution_string = string.Empty;
                        if (projectlistY.Count() > 0)
                            call_resolution_string = "Rescheduled call, Promised to Report";
                        else
                            call_resolution_string = "Ring, Busy, Left Message, No Answer, Disconnected, Rescheduled call, Promised to Report";

                        // MessageBox.Show("There are " + (projectlist.Count - finished_cases.Count) + " remaining projects.  Enter one of " + call_resolution_string + " call resolution for these project");
                        MessageBox.Show("There are " + (projectlist.Count - finished_cases.Count) + " remaining projects. A Call Resolution will need to be entered for each of the remaining projects.");

                        int rowIndex = 0;
                        foreach (DataGridViewRow rrow in dgProject.Rows)
                        {
                            string new_id = rrow.Cells["id"].Value.ToString();
                            bool exist = finished_cases.Any(s => new_id.Contains(s));
                            if (!exist)
                            {
                                rowIndex = rrow.Index;
                                break;
                            }
                        }
                        dgProject.Rows[rowIndex].Selected = true;
                        dgProject.CurrentCell = dgProject[0, rowIndex];
                        cbCall.Focus();
                        cbCall.SelectedIndex = -1;
                        return;
                    }
                   
                }
            }

            if (id != null && id != "")
            {
                if (data_saved)
                {
                    csda.AddCsdaccessData(id, "UPDATE");
                }
                else
                    csda.AddCsdaccessData(id, "BROWSE");

                /*unlock respondent */
                if (editable)
                {
                    bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
                }
            }


            this.Hide();   // hide parent

            frmName fm = new frmName();
            fm.Id = id;

            //fm.Idlist = idlist;
            //fm.CurrIndex = CurrIndex;
            fm.CallingForm = this;

            fm.ShowDialog();  // show child
            RespId = fm.GetUpdatedRespid();

            if (editable && this.Visible)
            {
                //load new respid
                LoadForm();
                DisplayForm();
            }

        }

        bool vip_grid_set = false;

        //Set VIP datagrid
        private void SetDgVip(DataGridView dg)
        {
            for (int i = 0; i < dg.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dg.Columns[i].Visible = false;

                }
                else if (i == 1)
                {
                    dg.Columns[i].HeaderText = "DATE";
                    dg.Columns[i].ReadOnly = true;
                    dg.Columns[i].Width = 150;
                    dg.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (i == 2)
                {
                    dg.Columns[i].HeaderText = "VIPDATAR";
                    dg.Columns[i].Visible = true;
                    dg.Columns[i].Width = 150;
                    dg.Columns[i].DefaultCellStyle.Format = "N0";   
                    DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dgVip.Columns[i];//here index of column
                    cvip.MaxInputLength = 6;

                }
                else if (i == 3)
                {
                    dg.Columns[i].HeaderText = "PCT5CR";
                    dg.Columns[i].Visible = true;
                    dg.Columns[i].Width = 150;
                    DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dg.Columns[i];//here index of column
                    cvip.MaxInputLength = 3;
                        
                }
                else if (i == 4)
                {
                    dg.Columns[i].Visible = false;
                }
                else if (i == 5)
                {
                    dg.Columns[i].HeaderText = "VIPFLAG";
                    dg.Columns[i].Visible = true;
                    dg.Columns[i].Width = 100;
                    dg.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                    dg.Columns[i].ReadOnly = true;
                }
                else
                    dg.Columns[i].Visible = false;
             }
        }


        //tab control index change event
        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (id == null || id == "") return;

            if (tabControl2.SelectedIndex == 1 && !vip_grid_set)
            {
                SetDgVip(dgVip);
                vip_grid_set = true;
                
                HighlightFieldsFromFlags();
            }
            else if (tabControl2.SelectedIndex == 2)
            {
                if (dgPcomments.Rows.Count > 0)
                {
                    dgPcomments.Columns[0].Width = 75;
                    dgPcomments.Columns[1].Width = 60;
                }
            }
            else if (tabControl2.SelectedIndex == 3)
            {
                if (dgRcomments.Rows.Count > 0)
                {
                    dgRcomments.Columns[0].Width = 75;
                    dgRcomments.Columns[1].Width = 60;
                }
            }
            else if (tabControl2.SelectedIndex == 4)
            {
                if (dgPmark.Rows.Count > 0)
                {
                    dgPmark.Columns[0].Visible = false;
                    dgPmark.Columns[1].Visible = false;
                    dgPmark.Columns[2].Width = 75;
                    dgPmark.Columns[2].HeaderText = "USER";
                    dgPmark.Columns[3].HeaderText = "MARK NOTE";


                }
            }
            else if (tabControl2.SelectedIndex == 5)
            {
                if (dgRmark.Rows.Count > 0)
                {
                    dgRmark.Columns[0].Visible = false;
                    dgRmark.Columns[1].Visible = false;
                    dgRmark.Columns[2].Width = 60;
                    dgRmark.Columns[2].HeaderText = "USER";
                    dgRmark.Columns[3].HeaderText = "MARK NOTE";


                }
            }
        }

        private void frmTfu_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*unlock respondent */
            if (editable)
            {
                bool locked = GeneralDataFuctions.UpdateRespIDLock(RespId, "");
            }

            //add record to cpraccess
            if (enterPoint == TypeTFUEntryPoint.SEARCH)
                GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
            else
                GeneralDataFuctions.AddCpraccessData("TFU", "EXIT");

            ///*Close the hide form */
            if (!call_callingFrom && (CallingForm != null))
                CallingForm.Close();
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            old_text = txtPhone.Text;
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtPhone.Text)
                    {
                        if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                        {
                            MessageBox.Show("Phone number is invalid.");
                            txtPhone.Focus();
                            txtPhone.Text = old_text;
                        }
                        else
                            old_text = txtPhone.Text;
                    }
                }
            }
        }

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (editable)
            {
                if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Phone number is invalid.");
                    txtPhone.Focus();
                    txtPhone.Text = old_text;
                }
            }
        }

        private void txtFax_Enter(object sender, EventArgs e)
        {
            old_text = txtFax.Text;
        }

        private void txtFax_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtFax.Text)
                    {
                        if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                        {
                            MessageBox.Show("Fax number is invalid.");
                            txtFax.Focus();
                            txtFax.Text = old_text;
                        }
                        else
                            old_text = txtFax.Text;
                    }
                }
            }
        }

        private void txtFax_Leave(object sender, EventArgs e)
        {
            if (editable)
            {
                if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Fax number is invalid.");
                    txtFax.Focus();
                    txtFax.Text = old_text;
                }
            }
        }

        private void txtPhone2_Enter(object sender, EventArgs e)
        {
            old_text = txtPhone2.Text;
        }

        private void txtPhone2_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtPhone2.Text)
                    {
                        if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                        {
                            MessageBox.Show("Phone number is invalid.");
                            txtPhone2.Focus();
                            txtPhone2.Text = old_text;
                        }
                        else
                            old_text = txtPhone2.Text;
                    }
                }
            }
        }

        private void txtPhone2_Leave(object sender, EventArgs e)
        {
            if (editable)
            {
                if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Phone number is invalid.");
                    txtPhone2.Focus();
                    txtPhone2.Text = old_text;
                }
            }
        }

        private void txtEmail_Enter(object sender, EventArgs e)
        {
            old_text = txtEmail.Text;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (txtEmail.Text.Trim() != "")
            {
                if (!GeneralFunctions.isEmail(txtEmail.Text.Trim()))
                {
                    MessageBox.Show("Email address is invalid.");
                    txtEmail.Focus();
                    txtEmail.Text = old_text;
                }
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && e.KeyCode == Keys.Enter)
            {
                if (txtEmail.Text.Trim() != "")
                {
                    if (!GeneralFunctions.isEmail(txtEmail.Text.Trim()))
                    {
                        MessageBox.Show("Email address is invalid.");
                        txtEmail.Text = old_text;
                    }
                }
            }
        }

        private void txtWebAddr_Enter(object sender, EventArgs e)
        {
            old_text = txtWebAddr.Text;
        }

        private void txtWebAddr_Leave(object sender, EventArgs e)
        {
            if (txtWebAddr.Text.Trim() != "")
            {
                if (!GeneralData.IsValidURL(txtWebAddr.Text))
                {
                    DialogResult result = MessageBox.Show("Web address is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtWebAddr.Focus();
                        txtWebAddr.Text = old_text;
                    }
                }
            }
        }

        private void txtWebAddr_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && e.KeyCode == Keys.Enter)
            {
                if (txtWebAddr.Text.Trim() != "")
                {
                    if (!GeneralData.IsValidURL(txtWebAddr.Text))
                    {
                        DialogResult result = MessageBox.Show("Web address is invalid.", "OK", MessageBoxButtons.OK);

                        if (result == DialogResult.OK)
                        {
                            txtWebAddr.Text = old_text;
                        }
                    }
                }
            }
        }

        private void txtZip_Enter(object sender, EventArgs e)
        {
            old_text = txtZip.Text;
        }

        //Check address 3 is Canada or not
        private bool CheckAddress3IsCanada()
        {
            string[] words = GeneralFunctions.SplitWords(txtAddr3.Text.Trim());
            int num_word = words.Count();

            if (num_word> 0 && words[num_word - 1] == "CANADA")
                return true;
            else
                return false;
        }

        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (editable && id != null)
            {
                if (txtZip.Text.Trim() != "")
                {
                    bool isCanada = CheckAddress3IsCanada();
                    if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
                    {
                        DialogResult result = MessageBox.Show("Zip Code is invalid.", "OK", MessageBoxButtons.OK);

                        if (result == DialogResult.OK)
                        {
                            txtZip.Focus();
                            txtZip.Text = old_text;
                        }
                    }
                }
            }
        }

        private void txtZip_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && e.KeyCode == Keys.Enter)
            {
                if (txtZip.Text.Trim() != "")
                {
                    bool isCanada = CheckAddress3IsCanada();
                    if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
                    {
                        DialogResult result = MessageBox.Show("Zip Code is invalid.", "OK", MessageBoxButtons.OK);

                        if (result == DialogResult.OK)
                        {
                            txtZip.Focus();
                            txtZip.Text = old_text;
                        }
                    }
                }
            }
        }

        private void txtAddr3_Enter(object sender, EventArgs e)
        {
            old_text = txtAddr3.Text;
        }

        private void txtAddr3_Leave(object sender, EventArgs e)
        {
            if (editable && id != null)
            {
                if (!ValidateCityState())
                {
                    txtAddr3.Focus();
                    txtAddr3.Text = old_text;
                }
                else if (txtAddr3.Text.Trim() != "")
                {
                    txtZone.Text = GeneralDataFuctions.GetTimezone(rst);
                    txtTime.Text = GeneralDataFuctions.GetTimezoneCurrentTime(rst);
                }
            }
        }

        private void txtAddr3_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && e.KeyCode == Keys.Enter)
            {
                if (!ValidateCityState())
                {
                    txtAddr3.Text = old_text;
                }
                else if (txtAddr3.Text.Trim() != "")
                {
                    txtZone.Text = GeneralDataFuctions.GetTimezone(rst);
                    txtTime.Text = GeneralDataFuctions.GetTimezoneCurrentTime(rst);
                }
            }
        }

        //validate City State field
        private bool ValidateCityState()
        {
            bool result= true;

            //check valid state
            if (string.IsNullOrWhiteSpace(txtAddr3.Text))
            {
                //MessageBox.Show("City/State is invalid.");
                //result = false;
            }
            else
            {
                string[] words = GeneralFunctions.SplitWords(txtAddr3.Text.Trim());
                int num_word = words.Count();
                
                if (num_word < 2)
                {
                    MessageBox.Show("City/State is invalid.");
                    result = false;
                }
                else
                {
                    if (words[num_word - 1] == "CANADA")
                    {
                        rst = words[num_word - 2];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, true))
                        {
                            MessageBox.Show(rst + " is not a valid province abbreviation in Canada");
                            result = false;
                        }
                    }
                    else
                    {
                        rst = words[num_word - 1];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, false))
                        {
                            MessageBox.Show(rst + " is not a valid state abbreviation in the US.");
                            result = false;
                        }
                    }
                }
            }

            return result;

        }


        private void btnNewtc_Click(object sender, EventArgs e)
        {
            frmNewtcSel newtcpopup = new frmNewtcSel();
            newtcpopup.CaseOwner = mast.Owner;
            newtcpopup.ViewOnly = true;
            DialogResult dialogresult = newtcpopup.ShowDialog();
          
            newtcpopup.Dispose();
        }


        private void txtExt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBldgs_Enter(object sender, EventArgs e)
        {
            old_text = txtBldgs.Text;
        }

        private void txtBldgs_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBldgs_Validating(object sender, CancelEventArgs e)
        {
            if (editable && id != null)
            {
                if (old_text != txtBldgs.Text)
                {
                    if (txtBldgs.Text == "" || Convert.ToInt32(txtBldgs.Text) < 1)
                    {
                        MessageBox.Show("RBLDGS must be a number between 1 and 999.");
                        txtBldgs.Text = old_text;
                        e.Cancel = true;
                    }
                    else
                        old_text = txtBldgs.Text;
                }

            }
        }

        private void txtBldgs_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtBldgs.Text)
                    {
                        if (txtBldgs.Text == "" || Convert.ToInt32(txtBldgs.Text) < 1)
                        {
                            MessageBox.Show("RBLDGS must be a number between 1 and 999.");
                            txtBldgs.Text = old_text;
                        }
                        else
                            old_text = txtBldgs.Text;
                    }
                }
            }

        }

        private void txtUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtUnits_Enter(object sender, EventArgs e)
        {
            old_text = txtUnits.Text;
        }
        private void txtUnits_Validating(object sender, CancelEventArgs e)
        {
            if (old_text != txtUnits.Text)
            {
                if (txtUnits.Text == "" || Convert.ToInt32(txtUnits.Text.Replace(",", "")) < 2)
                {
                    MessageBox.Show("Units must be a number between 2 and 9999");
                    txtUnits.Text = old_text;
                    e.Cancel = true;
                }
                else if (txtUnits.Text != "" && txtBldgs.Text != "" && (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2))
                {
                    MessageBox.Show("Units must be 2 or more times than the Bldgs.");
                    txtUnits.Text = old_text;
                    e.Cancel = true;
                }
                else
                {
                    old_text = txtUnits.Text;

                    //if it is mulitfamily
                    SetupCostput();
                }
            }
        }
        private void txtUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtUnits.Text)
                    {
                        if (txtUnits.Text == "" || Convert.ToInt32(txtUnits.Text.Replace(",", "")) < 2)
                        {
                            MessageBox.Show("RBLDGS must be a number between 2 and 9999.");
                            txtUnits.Text = old_text;
                        }
                        else if (txtUnits.Text != "" && txtBldgs.Text != "" && (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2))
                        {
                            MessageBox.Show("Units must be 2 or more times than the Bldgs.");
                            txtUnits.Text = old_text;
                        }
                        else
                        {
                           old_text = txtUnits.Text;

                           //set up costput
                           SetupCostput();
                        }
                    }
                }
            }
        }

        //when rvitm5c or units were changed, costpu need updated
        private void SetupCostput()
        {
            //if it is mulitfamily
            if (mast.Owner == "M")
            {
                int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                if (it5c > 0)
                {
                    int iunits = Convert.ToInt32(txtUnits.Text.Replace(",", ""));
                    if (iunits > 0)
                        txtCostpu.Text = Math.Round((double)((double)it5c / iunits)).ToString("N0");
                }
            }
        }

        private void txtStrtdater_Enter(object sender, EventArgs e)
        {
            old_text = txtStrtdater.Text.Trim();
        }

        private void txtStrtdater_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtStrtdater.Text.Trim())
                {
                    if (!CheckStrtdateEnter())
                    {
                        txtStrtdater.Text = old_text;
                    }
                    else
                        old_text = txtStrtdater.Text;
                }
            }
        }

        private void txtStrtdater_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtStrtdater_Validating(object sender, CancelEventArgs e)
        {
            if (editable && id != null && old_text != txtStrtdater.Text.Trim())
            {
                if (!CheckStrtdateEnter())
                {
                    txtStrtdater.Text = old_text;
                    e.Cancel = true;
                }
                else
                    old_text = txtStrtdater.Text;
            }
        }

        //Verify entered Strtdate
        private bool CheckStrtdateEnter()
        {
            txtStrtdater.Text = txtStrtdater.Text.Trim();
            if (txtStrtdater.Text == "")
            {
                if (txtCompdater.Text != "" || txtRvitm5cr.Text != "0" || txtItem5ar.Text != "0" || txtItem5br.Text != "0")
                {
                    MessageBox.Show("Start date cannot be blank");
                    return false;
                }
            
                 //if the project has future strtdate, status is "4", change to "1"
                if (old_text != ""  && cbStatus.SelectedValue.ToString() == "4")
                {
                    cbStatus.SelectedIndex = 0;
                }

                if (mvs.GetSumMons() > 0)
                {
                    //Check report data in monthly vip, ask question, set to blank
                    var rlist = from v in mvs.monthlyViplist where (v.Vipflag == "R" || v.Vipflag == "A") select v;
                    if (rlist.ToList().Count > 0)
                    {
                        DialogResult result1 = MessageBox.Show("Reported Vip data Prior to Start Date, Erase?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result1 == DialogResult.Yes)
                        {
                            foreach (var value in mvs.monthlyViplist)
                            {
                                if (value.Vipdatar > 0 || value.Vipflag != "B" || value.vs == "m")
                                {
                                    AddVipauditData(value.Date6, value.Vipdatar, value.Vipflag, 0, "B");
                                    value.Vipdatar = 0;
                                    value.Vipflag = "B";
                                    value.Pct5c = 0;
                                    value.Vipdata = 0;
                                    value.Pct5cr = 0;
                                    value.vs = "d";
                                }
                            }
                            displayMonthlyVips();
                            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                            int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                            txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        //Check impute data, set to blank
                        var ilist = from v in mvs.monthlyViplist where v.Vipflag == "M" select v;
                        if (ilist.ToList().Count > 0)
                        {
                            foreach (var value in mvs.monthlyViplist)
                            {
                                if  (value.Vipdata > 0 || value.Vipflag != "B" || value.vs == "m")
                                {
                                    AddVipauditData(value.Date6, value.Vipdatar, value.Vipflag, 0, "B");
                                    value.Vipdata = 0;
                                    value.Vipflag = "B";
                                    value.Pct5c = 0;
                                    value.Vipdatar = 0;
                                    value.Pct5cr = 0;
                                    value.vs = "d";
                                }
                            }
                            displayMonthlyVips();
                            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                            int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                            txtCompr.Text = mvs.GetCumPercent(it5c).ToString();
                        }
                    }
                }

                //blank out the futcompd if the start date is empty
                if (txtFutcompdr.Text != "")
                {
                    txtFutcompdr.Text = "";
                    txtFlagFutcompd.Text = "B";
                }

                txtFlagStrtdate.Text = "B";
            
            }

            if (txtStrtdater.Text != "")
            {
                if (!ValidateDate(txtStrtdater.Text))
                {
                    MessageBox.Show("Start Date is invalid.");
                    return false;
                }

                DateTime strt = DateTime.ParseExact(txtStrtdater.Text, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime sel = DateTime.ParseExact(mast.Seldate, "yyyyMM", CultureInfo.InvariantCulture);

                if (GeneralFunctions.GetNumberMonths(strt, sel) > 24)
                {
                    MessageBox.Show("Start Date cannot be earlier than 24 months before Selection Date.");
                    return false;
                }

                //Check report data in monthly vip, ask question, set to blank
                var rlist = from v in mvs.monthlyViplist where v.Vipflag == "R" && Convert.ToInt32(v.Date6) < Convert.ToInt32(txtStrtdater.Text) select v;
                if (rlist.ToList().Count > 0)
                {
                    DialogResult result1 = MessageBox.Show("Reported Vip data Prior to Start Date, Erase?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        foreach (var value in mvs.monthlyViplist)
                        {
                            if (Convert.ToInt32(value.Date6) < Convert.ToInt32(txtStrtdater.Text))
                            {
                                if (value.Vipdatar > 0 || value.Vipflag != "B" || value.vs == "m")
                                    AddVipauditData(value.Date6, value.Vipdata, value.Vipflag, 0, "B");
                                value.Vipdatar = 0;
                                value.Vipflag = "B";
                                value.Pct5c = 0;
                                value.Vipdata = 0;
                                value.Pct5cr = 0;
                                value.vs = "d";
                            }
                        }
                        displayMonthlyVips();
                        txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                        int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                        txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Check impute or other flag data, set to blank
                    var ilist = from v in mvs.monthlyViplist where v.Vipflag != "R" && Convert.ToInt32(v.Date6) < Convert.ToInt32(txtStrtdater.Text) select v;
                    if (ilist.ToList().Count > 0)
                    {
                        foreach (var value in mvs.monthlyViplist)
                        {
                            if (Convert.ToInt32(value.Date6) < Convert.ToInt32(txtStrtdater.Text))
                            {
                                if (value.Vipdata > 0 || value.Vipflag != "B" || value.vs == "m")
                                    AddVipauditData(value.Date6, value.Vipdata, value.Vipflag, 0, "B");
                                value.Vipdata = 0;
                                value.Vipflag = "B";
                                value.Pct5c = 0;
                                value.Vipdatar = 0;
                                value.Pct5cr = 0;
                                value.vs = "d";
                            }
                        }
                        displayMonthlyVips();
                        txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                        int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                        txtCompr.Text = mvs.GetCumPercent(it5c).ToString();         
                    }
                }

                //Update status
                if (Convert.ToInt32(txtStrtdater.Text) >= Convert.ToInt32(currYearMon) && cbStatus.SelectedValue.ToString() != "4")
                {
                    cbStatus.SelectedValue = 4;
                  
                }
                else if (Convert.ToInt32(txtStrtdater.Text) < Convert.ToInt32(currYearMon) && cbStatus.SelectedValue.ToString() == "4")
                {
                    cbStatus.SelectedValue = 1;
                }

                //Set default flag
                txtFlagStrtdate.Text = "R";
                txtStrtdater.Modified = false;
            }

            return true;
        }


        private void txtFutcompdr_Enter(object sender, EventArgs e)
        {
            old_text = txtFutcompdr.Text;
        }

        private void txtFutcompdr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFutcompdr_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null && old_text != txtFutcompdr.Text)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!CheckFutcompdEnter())
                    {
                        txtFutcompdr.Text = old_text;
                        return;
                    }
                    else
                    {
                        old_text = txtFutcompdr.Text;
                    }
                }
            }
        }

        private void txtFutcompdr_Validating(object sender, CancelEventArgs e)
        {
            if (editable && id != null && old_text != txtFutcompdr.Text)
            {
                if (!CheckFutcompdEnter())
                {
                    txtFutcompdr.Text = old_text;
                    e.Cancel = true;
                }
                else
                    old_text = txtFutcompdr.Text;
            }
        }

        //Verify Futcompd textbox change
        private bool CheckFutcompdEnter()
        {
            txtFutcompdr.Text = txtFutcompdr.Text.Trim();
            if (txtFutcompdr.Text == "")
            {
               txtFlagFutcompd.Text = "B";
                return true;
            }

            if (!ValidateDate(txtFutcompdr.Text))
            {
                MessageBox.Show("Projected Completion Date is invalid.");
                return false;
            }
            else if (txtStrtdater.Text == "")
            {
                MessageBox.Show("Projected Completion Date cannot be entered if no started.");
                return false;
            }
            else if (txtFutcompdr.Text != "" && txtStrtdater.Text != "" && Convert.ToInt32(txtFutcompdr.Text) < Convert.ToInt32(txtStrtdater.Text))
            {
                MessageBox.Show("Projected completion date cannot be less than Start Date.");
                return false;
            }

            txtFlagFutcompd.Text = "R";
            txtFutcompdr.Modified = false;

            return true;
        }

        /*Check it is a good date string or not */
        private bool ValidateDate(string input_date)
        {
            input_date = input_date.Trim();
            if (input_date.Length != 0)
            {
                if (input_date.Length != 6)
                {
                    return false;
                }
                else
                {
                    string input_year = input_date.Substring(0, 4);
                    string input_mon = input_date.Substring(4, 2);

                    if (Convert.ToInt32(input_year) < 1990 || Convert.ToInt32(input_year) > 2050)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(input_mon) > 12 || Convert.ToInt32(input_mon) == 0)
                        return false;
                }
            }

            return true;
        }

        private void txtItem5ar_Enter(object sender, EventArgs e)
        {
            old_text = txtItem5ar.Text;
        }

        private void txtItem5ar_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtItem5ar.Text != "")
                {
                    value = Convert.ToInt32(txtItem5ar.Text.Replace(",", ""));
                    txtItem5ar.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtItem5ar.SelectionStart = txtItem5ar.Text.Length;
                }
                else
                    txtItem5ar.Text = "0";

                txtItem5ar.Modified = true;
            }
        }

        /*Set default flag on textbox */
        private void SetFlag(TextBox itemBox, TextBox flagBox)
        {
            if (!editable || itemBox.Text == "")
                return;

            int nvalue = Convert.ToInt32(itemBox.Text.Replace(",", ""));
            if (nvalue == 0)
            {
                frmTFUFlagSelPopup popup = new frmTFUFlagSelPopup();
                popup.StartPosition = FormStartPosition.CenterParent;
                DialogResult dialogresult = popup.ShowDialog();

                if (dialogresult == DialogResult.OK)
                    flagBox.Text = popup.selectedFlag; 
                
                popup.Dispose();
                
            }
            else if (flagBox.Text != "R")
                flagBox.Text = "R";
        }

        //Verify Item5a
        private bool CheckItem5aEnter()
        {
            if (txtItem5ar.Text != "")
            {
                int value = Convert.ToInt32(txtItem5ar.Text.Replace(",", ""));
                if (value > 0 && txtStrtdater.Text == "")
                {
                    MessageBox.Show("Item 5A rejected, must have a value for Start Date.");
                    return false;
                }
            }
            return true;
        }

        private void txtItem5ar_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtItem5ar_Validating(object sender, CancelEventArgs e)
        {
            if (editable && !CheckItem5aEnter())
            {
                txtItem5ar.Text = old_text;
                e.Cancel = true;
            }
        }

        private void txtItem5ar_Validated(object sender, EventArgs e)
        {
            if (old_text != txtItem5ar.Text)
            {
                SetFlag(txtItem5ar, txtFlag5a);
                txtItem5ar.Modified = false;
            }
        }

        private void txtItem5ar_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null && old_text != txtItem5ar.Text)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtItem5ar.Text != "")
                    {
                        if (!CheckItem5aEnter())
                        {
                            txtItem5ar.Text = old_text;
                            return;
                        }
                        SetFlag(txtItem5ar, txtFlag5a);
                        old_text = txtItem5ar.Text;
                        txtItem5ar.Modified = false;
                    }
                }
            }
        }

        private void txtItem5br_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtItem5br.Text != "")
                {
                    value = Convert.ToInt32(txtItem5br.Text.Replace(",", ""));
                    txtItem5br.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtItem5br.SelectionStart = txtItem5br.Text.Length;
                }
                else
                    txtItem5br.Text = "0";

                txtItem5br.Modified = true;

            }
        }

        private void txtItem5br_Enter(object sender, EventArgs e)
        {
            old_text = txtItem5br.Text;
        }

        private void txtItem5br_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtItem5br.Text)
                {
                    if (txtItem5br.Text != "")
                    {
                        if (!CheckItem5bEnter())
                        {
                            txtItem5br.Text = old_text;
                            return;
                        }
                        SetFlag(txtItem5br, txtFlag5b);
                        old_text = txtItem5br.Text;
                    }

                    txtItem5br.Modified = false;
                }
            }
        }

        private void txtItem5br_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtItem5br_Validating(object sender, CancelEventArgs e)
        {
            if (editable && id != null && !CheckItem5bEnter())
            {
                txtItem5br.Text = old_text;
                e.Cancel = true;
            }
        }

        private void txtItem5br_Validated(object sender, EventArgs e)
        {
            if (old_text != txtItem5br.Text)
            {
                SetFlag(txtItem5br, txtFlag5b);
                txtItem5br.Modified = false;
            }
        }

        //Verify Item5b
        private bool CheckItem5bEnter()
        {
            if (txtItem5br.Text != "")
            {
                int value = Convert.ToInt32(txtItem5br.Text.Replace(",", ""));
                if (value > 0 && txtStrtdater.Text == "")
                {
                    MessageBox.Show("Item 5B rejected, must have a value for Start Date");
                    return false;
                }
            }
            return true;
        }


        private void txtRvitm5cr_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtRvitm5cr.Text != "")
                {
                    value = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                    txtRvitm5cr.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtRvitm5cr.SelectionStart = txtRvitm5cr.Text.Length;
                }
                else
                    txtRvitm5cr.Text = "0";

                txtRvitm5cr.Modified = true;

            }
        }

        private void txtRvitm5cr_Enter(object sender, EventArgs e)
        {
            old_text = txtRvitm5cr.Text;
        }

        private void txtRvitm5cr_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtRvitm5cr.Text)
                {
                    if (VerifyRvitm5c())
                    {
                        SetFlag(txtRvitm5cr, txtFlagr5c);
                        old_text = txtRvitm5cr.Text;

                        //update vip data
                        UpdateRelatedVIps();
                    }
                }
            }
        }

        private void txtRvitm5cr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRvitm5cr_Validating(object sender, CancelEventArgs e)
        {
            if (editable && id != null  && !VerifyRvitm5c() && old_text != txtRvitm5cr.Text)
            {
                e.Cancel = true;
            }
        }

        private void txtRvitm5cr_Validated(object sender, EventArgs e)
        {
            if (old_text != txtRvitm5cr.Text)
            {
                SetFlag(txtRvitm5cr, txtFlagr5c);
                UpdateRelatedVIps();
                txtRvitm5cr.Modified = false;
            }
        }


        //Verify Rvitm5C
        private bool VerifyRvitm5c()
        {
            int value = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
            if (value == 0 && txtFlagr5c.Text != "B" && mvs.GetCumvipr() > 0)
            {
                MessageBox.Show("Item 5C cannot be zero");
                txtRvitm5cr.Text = old_text;
                if (txtFlagr5c.Text == "R")
                {
                    txtRvitm5cr.Text = old_text;
                }
                UpdateRelatedVIps();

                return false;
            }
            else if (value > 0 && txtStrtdater.Text == "")
            {
                MessageBox.Show("Item 5C rejected, must have a value for Start Date.");
                txtRvitm5cr.Text = old_text;
                if (txtFlagr5c.Text == "R")
                {
                    txtRvitm5cr.Text = old_text;
                }
                UpdateRelatedVIps();

                return false;
            }
            return true;
        }

        //Updated related Vips data
        private void UpdateRelatedVIps()
        {
            int it5cr = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
            if (it5cr > 0)
            {
                mvs.UpdatePct5cs(it5cr, it5cr);
                displayMonthlyVips();
                txtCompr.Text = mvs.GetCumPercentr(it5cr).ToString();

                txtRvitm5cr.Modified = false;

                //if it is mulitfamily
                SetupCostput();
            }
        }

        private void txtItem6r_Enter(object sender, EventArgs e)
        {
            old_text = txtItem6r.Text;
        }

        private void txtItem6r_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtItem6r.Text != "")
                {
                    value = Convert.ToInt32(txtItem6r.Text.Replace(",", ""));
                    txtItem6r.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtItem6r.SelectionStart = txtItem6r.Text.Length;
                }
                else
                {
                    txtItem6r.Text = "0";
                }

                txtItem6r.Modified = true;
            }
        }

        private void txtItem6r_Validated(object sender, EventArgs e)
        {
            if (old_text != txtItem6r.Text)
                SetFlag(txtItem6r, txtFlagItm6);
        }

        private void txtItem6r_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtItem6r.Text)
                {
                    SetFlag(txtItem6r, txtFlagItm6);
                    old_text = txtItem6r.Text;
                }
            }
        }

        private void txtItem6r_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCapexpr_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtCapexpr.Text != "")
                {
                    value = Convert.ToInt32(txtCapexpr.Text.Replace(",", ""));
                    txtCapexpr.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtCapexpr.SelectionStart = txtCapexpr.Text.Length;
                }
                else
                    txtCapexpr.Text = "0";

                txtCapexpr.Modified = true;

            }
        }

        private void txtCapexpr_Enter(object sender, EventArgs e)
        {
            old_text = txtCapexpr.Text;
        }

        private void txtCapexpr_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter && txtCapexpr.Text != old_text)
                {
                    SetFlag(txtCapexpr, txtFlagcap);
                    old_text = txtCapexpr.Text;
                }
            }
        }

        private void txtCapexpr_Validated(object sender, EventArgs e)
        {
            if (txtCapexpr.Text != old_text && txtCapexpr.Text == "0")
                SetFlag(txtCapexpr, txtFlagcap);
        }


        private void txtCapexpr_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCompdater_Enter(object sender, EventArgs e)
        {
            old_text = txtCompdater.Text;
        }

        private void txtCompdater_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && id != null)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtCompdater.Text != old_text)
                    {
                        if (!CheckCompdateEnter())
                        {
                            txtCompdater.Text = old_text;
                            return;
                        }
                        else
                        {
                            old_text = txtCompdater.Text;
                        }
                    }
                }
            }
        }

        private void txtCompdater_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtCompdater_Validating(object sender, CancelEventArgs e)
        {
            if (editable && id != null && txtCompdater.Text != "")
            {
                if (old_text != txtCompdater.Text)
                {
                    if (!CheckCompdateEnter())
                    {
                        txtCompdater.Text = old_text;
                        e.Cancel = true;
                        return;
                    }
                    else
                    {
                        old_text = txtCompdater.Text;
                    }
                }

            }
        }

        //Verify entered Compdate
        private bool CheckCompdateEnter()
        {
            txtCompdater.Text = txtCompdater.Text.Trim();
            if (txtCompdater.Text == "")
            {
                txtFlagCompdate.Text = "B";
                return true;
            }

            if (!ValidateDate(txtCompdater.Text))
            {
                MessageBox.Show("Completion Date is invalid.");
                return false;
            }

            if (cbStatus.SelectedValue.ToString() == "2" || cbStatus.SelectedValue.ToString() == "3" || cbStatus.SelectedValue.ToString() == "7" || cbStatus.SelectedValue.ToString() == "8")
            {
                MessageBox.Show("Completion Date cannot be entered for PNR or Refusal.");
                return false;
            }
            if (txtStrtdater.Text == "")
            {
                MessageBox.Show("Completion Date cannot be entered if no started.");
                return false;
            }
            if (txtStrtdater.Text != "" && Convert.ToInt32(txtStrtdater.Text) > Convert.ToInt32(txtCompdater.Text))
            {
                MessageBox.Show("Completion Date cannot be less than Start Date.");
                return false;
            }
            if (Convert.ToInt32(currYearMon) <= Convert.ToInt32(txtCompdater.Text))
            {
                MessageBox.Show("Completion Date cannot be a future date.");
                return false;
            }
            if (mvs.monthlyViplist.Count == 0)
            {
                MessageBox.Show("Completion Date with no VIP data");
                return false;
            }
            if (mvs.GetMonthVip(txtCompdater.Text).Vipdatar == 0 && mvs.GetMonthVip(txtCompdater.Text).Vipflag != "R")
            {
                MessageBox.Show("No VIP data entered for completion month.");
                return false;
            }

            //Check report data in monthly vip, ask question, set to blank
            var rlist = from v in mvs.monthlyViplist where (v.Vipflag == "R" || v.Vipflag == "A") && Convert.ToInt32(v.Date6) > Convert.ToInt32(txtCompdater.Text) select v;
            if (rlist.ToList().Count > 0)
            {
                DialogResult result1 = MessageBox.Show("Reported Vip data after Completion Date, Erase?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes)
                {
                    foreach (var value in mvs.monthlyViplist)
                    {
                        if (Convert.ToInt32(value.Date6) > Convert.ToInt32(txtCompdater.Text))
                        {
                            if (value.Vipdata > 0 || value.vs == "m" || value.Vipflag != "B")
                                AddVipauditData(value.Date6, value.Vipdata, value.Vipflag, 0, "B");

                            value.Vipdata = 0;
                            value.Vipflag = "B";
                            value.Pct5c = 0;
                            value.Vipdatar = 0;
                            value.Pct5cr = 0;
                            value.vs = "d";
                        }
                    }
                    displayMonthlyVips();
                    txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                    int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                    txtCompr.Text = mvs.GetCumPercentr(it5c).ToString(); 
                }
                else
                {
                    txtCompdater.Text = old_text;
                    return false;
                }
            }
            else
            {
                rlist = from v in mvs.monthlyViplist where v.Vipflag == "M" && Convert.ToInt32(v.Date6) > Convert.ToInt32(txtCompdater.Text) select v;
                foreach (var value in mvs.monthlyViplist)
                {
                    if (Convert.ToInt32(value.Date6) > Convert.ToInt32(txtCompdater.Text))
                    {
                        if (value.Vipdata > 0 || value.vs == "m" || value.Vipflag != "B")
                            AddVipauditData(value.Date6, value.Vipdata, value.Vipflag, 0, "B");

                        value.Vipdata = 0;
                        value.Vipflag = "B";
                        value.Pct5c = 0;
                        value.Vipdatar = 0;
                        value.Pct5cr = 0;
                        value.vs = "d";
                    }
                }
                displayMonthlyVips();
                txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                int it5c = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
                txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
            }

            txtFlagCompdate.Text = "R";
            txtCompdater.Modified = false;

            return true;
        }

        private int oldvalue;
        private string oldvs;
        private bool isvalid;
        private Int32 currentRow;
        private Int32 currentCell;
        private bool resetRow = false;


        private static KeyPressEventHandler NumbericCheckHandler = new KeyPressEventHandler(NumbericCheck);
        private static void NumbericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar)  && e.KeyChar != '-';
        }

        //only enter numeric
        private void dgVip_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgVip.CurrentCell.ColumnIndex == 2 || dgVip.CurrentCell.ColumnIndex == 3)
            {
                e.Control.KeyPress -= NumbericCheckHandler;
                e.Control.KeyPress += NumbericCheckHandler;
            }

        }

        private void dgVip_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgVip.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        private void dgVip_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgVip.Columns[e.ColumnIndex].ReadOnly == false) 
            {
                if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
                {
                    oldvalue = Convert.ToInt32(dgVip[e.ColumnIndex, e.RowIndex].Value);
                    oldvs = dgVip[7, e.RowIndex].Value.ToString();

                    isvalid = true;
                }
            }
            else
            {
                isvalid = false;
                oldvs = dgVip[7, e.RowIndex].Value.ToString();
            }

        }

        private void dgVip_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editable)
                return;

            if (!isvalid)
            {
                if (e.ColumnIndex == 2)
                {
                    dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                    dgVip[7, e.RowIndex].Value = oldvs;
                }
                else if (e.ColumnIndex == 3)
                {
                    dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                    dgVip[7, e.RowIndex].Value = oldvs;
                }

                if (txtStrtdater.Text.Trim() == "")
                        MessageBox.Show("Cannot Enter VIP if there is no Start Date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //dgVip[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                if (e.ColumnIndex == 2)
                {
                    int newvalue = 0; 
                    newvalue = Convert.ToInt32(dgVip[e.ColumnIndex, e.RowIndex].Value);

                     if (oldvalue != newvalue)
                     {
                        string date6 = dgVip[0, e.RowIndex].Value.ToString();
                        int i5cr = int.Parse(txtRvitm5cr.Text.Replace(",", ""));

                        if (txtStrtdater.Text.Trim() == "")
                        {
                            MessageBox.Show("A start date must be entered before VIP data can be entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtCompdater.Text.Trim().Length > 0) && (Convert.ToInt32(date6) > Convert.ToInt32(txtCompdater.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP after completion date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtStrtdater.Text.Trim().Length > 0) && (Convert.ToInt32(date6) < Convert.ToInt32(txtStrtdater.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP before start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if (newvalue > 0 && i5cr > 0 && newvalue > i5cr)
                        {
                            MessageBox.Show("VIP for " + date6 + " is greater than 100% of 5C.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else
                        {
                            if (i5cr > 0)
                            {
                                dgVip[e.ColumnIndex + 1, e.RowIndex].Value = Convert.ToInt32((double)newvalue / i5cr * 100);

                                //update vipdata,pct5cr
                                dgVip[4, e.RowIndex].Value = newvalue;
                                dgVip[6, e.RowIndex].Value = Convert.ToInt32((double)newvalue / i5cr * 100);
                            }
                            else
                            {
                                dgVip[4, e.RowIndex].Value = newvalue;
                                dgVip[e.ColumnIndex + 1, e.RowIndex].Value = 0;
                            }

                            string nflag = "R";
                            string oflag = dgVip[5, e.RowIndex].Value.ToString();
                            
                            if (dgVip[e.ColumnIndex, e.RowIndex].Value.ToString() == "0")
                            {
                                frmTFUFlagSelPopup popup = new frmTFUFlagSelPopup();
                                popup.StartPosition = FormStartPosition.CenterParent;
                                DialogResult dialogresult = popup.ShowDialog();

                                if (dialogresult == DialogResult.OK)
                                {
                                    if (popup.selectedFlag == "B")
                                        nflag = "B";
                                }
                                popup.Dispose();
                            }
                            dgVip[5, e.RowIndex].Value = nflag;

                            if (oldvs == "i" || oldvs == "a")
                                dgVip[7, e.RowIndex].Value = "a";
                            else if ((oldvs == "e") || (oldvs == "m"))
                            {
                                if (nflag=="B")
                                    dgVip[7, e.RowIndex].Value = "d";
                                else
                                   dgVip[7, e.RowIndex].Value = "m";
                            }
                            else if (oldvs == "d")
                                dgVip[7, e.RowIndex].Value = "m";

                            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                            txtCompr.Text = mvs.GetCumPercentr(i5cr).ToString();

                            AddVipauditData(date6, oldvalue, oflag, newvalue, nflag);

                            oldvalue = newvalue;
                            resetRow = true;
                            currentRow = e.RowIndex;
                            currentCell = e.ColumnIndex;
                        }

                    }
                }
                else if (e.ColumnIndex == 3)
                {
                    int newvalue = 0;
                    int oldvipval = 0;

                    newvalue = Convert.ToInt32(dgVip[e.ColumnIndex, e.RowIndex].Value);
                    
                    if (oldvalue != newvalue)
                    {
                        string date6 = dgVip[0, e.RowIndex].Value.ToString();
                        int i5cr = int.Parse(txtRvitm5cr.Text.Replace(",", ""));

                        if (txtStrtdater.Text.Trim() == "")
                        {
                            MessageBox.Show("A start date must be entered before VIP data can be entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtCompdater.Text.Trim().Length > 0) && (Convert.ToInt32(date6) > Convert.ToInt32(txtCompdater.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP Percent after Completion Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtStrtdater.Text.Trim().Length > 0) && (Convert.ToInt32(date6) < Convert.ToInt32(txtStrtdater.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP Percent before Start Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if (newvalue > 100)
                        {
                            MessageBox.Show("VIP percent for " + date6 + " is greater than 100% of 5C.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else
                        {
                            string oflag = dgVip[5, e.RowIndex].Value.ToString();
                            int vipvalue = 0;
                            string nflag = "R";
                            if (dgVip[e.ColumnIndex, e.RowIndex].Value.ToString() == "0")
                            {
                                frmTFUFlagSelPopup popup = new frmTFUFlagSelPopup();
                                popup.StartPosition = FormStartPosition.CenterParent;
                                DialogResult dialogresult = popup.ShowDialog();

                                if (dialogresult == DialogResult.OK)
                                {
                                    if (popup.selectedFlag == "B")
                                        nflag = "B";
                                }
                                popup.Dispose();
                               
                            }

                            dgVip[5, e.RowIndex].Value = nflag;

                            oldvipval = Convert.ToInt32(dgVip[e.ColumnIndex - 1, e.RowIndex].Value);

                            if (i5cr > 0)
                            {
                                vipvalue = Convert.ToInt32(newvalue * 0.01 * i5cr);
                                dgVip[2, e.RowIndex].Value = vipvalue;
                               
                                //update vipdata,pct5cr
                                dgVip[4, e.RowIndex].Value = vipvalue;
                                
                                if (oldvs == "i" || oldvs == "a")
                                    dgVip[7, e.RowIndex].Value = "a";
                                else if ((oldvs == "e") || (oldvs == "m"))
                                {
                                    if (nflag == "B")
                                        dgVip[7, e.RowIndex].Value = "d";
                                    else
                                        dgVip[7, e.RowIndex].Value = "m";
                                }
                                else if (oldvs == "d")
                                    dgVip[7, e.RowIndex].Value = "m";

                                txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                                txtCompr.Text = mvs.GetCumPercentr(i5cr).ToString();

                                AddVipauditData(date6, oldvipval, oflag, vipvalue, nflag);

                                oldvalue = newvalue;
                            }
                            else
                            {
                                dgVip[e.ColumnIndex, e.RowIndex].Value = 0;
                            }

                            resetRow = true;
                            currentRow = e.RowIndex;
                            currentCell = e.ColumnIndex;
                        }
                    }

                }
            }
        }

        /*Add Vipaudit data */
        private void AddVipauditData(string date6, int aoldvip, string aoldflag, int anewvip, string anewflag)
        {
            /*Get audit record from list */
            Vipaudit au = (from Vipaudit j in vipauditlist
                           where j.date6 == date6
                           select j).SingleOrDefault();

            /*if there is no record, add one, otherwise update the record */
            if (au == null)
            {
                Vipaudit ca = new Vipaudit();
                ca.Id = id;
                ca.date6 = date6;
                ca.Oldvip = aoldvip;
                ca.Oldflag = aoldflag;
                ca.Newvip = anewvip;
                ca.Newflag = anewflag;
                ca.Usrnme = UserInfo.UserName;
                ca.Progdtm = DateTime.Now;

                vipauditlist.Add(ca);
            }
            else
            {
                au.Newvip = anewvip;
                au.Newflag = anewflag;
                au.Progdtm = DateTime.Now;
            }

        }

        private void cbSurvey_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set up cap expense field if the survey is N, T, E, G, R, O and W
            if (cbSurvey.Text == "N" || cbSurvey.Text == "T" || cbSurvey.Text == "E" || cbSurvey.Text == "G" || cbSurvey.Text == "R" || cbSurvey.Text == "O" || cbSurvey.Text == "W")
                SetupCapexp(true);
            else
                SetupCapexp(false);
        }

        private void cbStatus_Enter(object sender, EventArgs e)
        {
            //store old status
            old_text = cbStatus.SelectedValue.ToString();
        }

        private void cbStatus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            string new_text = cbStatus.SelectedValue.ToString();
            if (old_text == new_text)
                return;

            if ( new_text == "4")
            {
                MessageBox.Show("Status change rejected, Please change Start date to future date.");
                cbStatus.SelectedValue = Convert.ToInt32(old_text);
            }
            else if ((old_text == "4") && (new_text != "5" && new_text != "6" && new_text != "4") && txtStrtdater.Text != "" && Convert.ToInt32(txtStrtdater.Text) >= Convert.ToInt32(currYearMon))
            {
                MessageBox.Show("Status change rejected, project has not started.");
                cbStatus.SelectedValue = Convert.ToInt32(old_text);
            }
            ////else if (old_text != "4" && old_text != "5" && new_text == "6" && txtStrtdater.Text != "" && Convert.ToInt32(txtStrtdater.Text) <= Convert.ToInt32(currYearMon))
            ////{
            ////    MessageBox.Show("Status change rejected, project has started.");
            ////    cbStatus.SelectedValue = Convert.ToInt32(old_text);
            ////}
            

            //for multifamily, if the status is 5, show the parent popup screen to select parent id
            if (new_text == "5" && mast.Owner == "M")
            {
                frmParentIdPopup popup = new frmParentIdPopup(id, mast.Masterid);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog();  //show child

                //if the popup was cancelled, set status to old value
                if (popup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    cbStatus.SelectedValue = Convert.ToInt32(old_text);
                }
                else
                {
                    //AddCprauditData("STATUS", samp.Status, "", "5", "");
                    //status_changed = true;
                    //samp.Status = "5";
                    cbStatus.Enabled = false;
                }
            }

           
        }

        //check other ids for vip satisfied
        private bool CheckVipstatifiedForID(string cid)
        {
            bool satisfied = false;
            int lag = Convert.ToInt32(cbLag.Text)+1;

            SampleData sdata = new SampleData();
            MonthlyVipsData mdata = new MonthlyVipsData();

            TypeDBSource dsource = sdata.GetDatabaseSource(cid);
            Sample sp = sdata.GetSampleData(cid);
            if (sp.Compdater == "")
            {
                //check vip satisfied
                MonthlyVips monvip = mdata.GetMonthlyVips(cid, dsource);
                MonthlyVip mvv = monvip.GetMonthVip(DateTime.Now.AddMonths(-1).ToString("yyyyMM"));
                if (mvv != null && mvv.Vipflag == "R")
                    satisfied = true;
                else
                {
                    MonthlyVip mvv2 = monvip.GetMonthVip(DateTime.Now.AddMonths(-lag).ToString("yyyyMM"));
                    if (mvv2 != null && mvv2.Vipflag == "R")
                        satisfied = true;
                }
            }
            else
                satisfied = true;

            return satisfied;
        }

        //check current data
        private bool CheckVipsatisfied()
        {
            bool satisfied = false;
            if (samp.Compdater == "")
            {
                int lag = Convert.ToInt32(cbLag.Text) + 1;
                if (mvs.GetMonthVip(DateTime.Now.AddMonths(-1).ToString("yyyyMM")).Vipflag == "R")
                    satisfied = true;
                else if (mvs.GetMonthVip(DateTime.Now.AddMonths(-lag).ToString("yyyyMM")).Vipflag == "R")
                    satisfied = true;
            }
            else
                satisfied = true;

            return satisfied;

        }
        private string sApptdate;
        private string sAppttime;
        private string sApptend;
        private void cbCall_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!formloading && cbCall.SelectedIndex !=-1)
            {
                //if call resolution is reschedule
                if (cbCall.SelectedValue.ToString() == "5")
                {
                    frmReschedulePopup popup = new frmReschedulePopup(samp.Id, samp.Respid);
                    popup.StartPosition = FormStartPosition.CenterParent;
                    popup.ShowDialog();  //show child
                    if (popup.DialogResult == DialogResult.OK)
                    {
                        sApptdate = popup.Apptdate;
                        sAppttime = popup.Appttime;
                        sApptend = popup.Apptend;
                    }
                    else
                        cbCall.SelectedIndex = -1;
                }
                else if (cbCall.SelectedValue.ToString() == "7")
                {
                    DateTime cutday = new DateTime(DateTime.Now.Year, DateTime.Now.Month, cut_day);

                    //get number of bussiness day from today to cut_day
                    int num_days = GeneralFunctions.GetBusinessDayBetweenDays(DateTime.Now, cutday );
                    if (num_days < 2)
                    {
                        MessageBox.Show("Call Resolution is not valid at this time");
                        cbCall.SelectedIndex = -1;
                    }
                }
            }
        }

        private void cbCall_Enter(object sender, EventArgs e)
        {
            //store old status
            if (cbCall.SelectedIndex != -1)
                old_text = cbCall.SelectedValue.ToString();
            else
                old_text = "";
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            //clean project data section

            if (btnAll.Text.Equals("ALL CASES", StringComparison.Ordinal))
            {
                if (id != null)
                    no_selection = true;
                DisplayProjectGrid(projectalllist);
               
                btnAll.Text = "ACTIVE CASES";
                txtTotProjects.Text = projectalllist.Count().ToString();
                if (id != null)
                {
                    HighlightRowForId(id);
                }
                else
                {
                    dgProject.Rows[0].Selected = true;
                    id = dgProject.SelectedRows[0].Cells[0].FormattedValue.ToString();
                }

                groupBox3.Text = "Projects - ALL CASES";
                SetupEditable();

            }
            else
            {
                if (id != null && projectlist.Count >0)
                    no_selection = true;
                
                DisplayProjectGrid(projectlist);

                btnAll.Text = "ALL CASES";
                txtTotProjects.Text = projectlist.Count().ToString();
                groupBox3.Text = "Projects - ACTIVE CASES";

                if (projectlist.Count == 0)
                {
                    ResetProject();
                    SetupEditable();
                    return;
                }

                int row_index = -1;
                for (int i = 0; i < projectlist.Count; i++)
                {
                    if (projectlist[i].Id== id.ToString()) // (you use the word "contains". either equals or indexof might be appropriate)
                    {
                       row_index = i;
                        break;
                    }
                }

                if (row_index == -1)
                {
                    dgProject.CurrentCell = dgProject[0, 0];
                    id = dgProject.SelectedRows[0].Cells[0].FormattedValue.ToString();

                    // Update the project grid to reflect changes to the selection.
                    LoadProjectData();
                    DisplayProject();
                }
                else
                    HighlightRowForId(id);

                SetupEditable();
            }
        }

        private void cbLag_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index >= 0 ? e.Index : 0;

            float size = 0;
            System.Drawing.Font myFont;
            FontFamily family = null;

            System.Drawing.Color myColor = new System.Drawing.Color();
            if (e.Index <= 0)
            {
                myColor = System.Drawing.Color.White;
            }
            else
            {
                myColor = System.Drawing.Color.Yellow;
            }
            size = 8;
            family = FontFamily.GenericSansSerif;

            // Draw the background of the item.
            e.DrawBackground();

            //Create a square filled with the animals color. Vary the size
            //of the rectangle based on the length of the animals name.
            Rectangle rectangle = new Rectangle(1, e.Bounds.Top,
            e.Bounds.Width, e.Bounds.Height);
            e.Graphics.FillRectangle(new SolidBrush(myColor), rectangle);

            // Draw each string in the array, using a different size, color,
            // and font for each item.
            myFont = new Font(family, size, FontStyle.Regular);

            e.Graphics.DrawString(cbLag.Items[index].ToString(), e.Font, System.Drawing.Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();

        }

        private void cbLag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Check all project in project list
            //if (!formloading)
            //{   
            //    int selected_row = dgProject.SelectedRows[0].Index;
            //    if (btnAll.Text.Equals("ALL CASES", StringComparison.Ordinal))
            //    {
            //        foreach (TFUProject p in projectlist)
            //        {
            //            p.Satisfied = (CheckVipstatifiedForID(p.Id)) ? "Y" : "";
            //        }

            //        DisplayProjectGrid(projectlist);
            //    }
            //    else
            //    {
            //        foreach (TFUProject p in projectalllist)
            //        {
            //            p.Satisfied = (CheckVipstatifiedForID(p.Id)) ? "Y" : "";
            //        }

            //        DisplayProjectGrid(projectalllist);
            //    }
            //}
        }

        private void btnAduit_Click(object sender, EventArgs e)
        {
            if (id != null)
            {
                frmProjectAuditPopup fproj = new frmProjectAuditPopup(id, samp.Respid, mast.Owner, mast.Newtc);

                fproj.ShowDialog();  //show child
            }
        }

        private void txtUnits_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtUnits.Text != "")
                {
                    value = Convert.ToInt32(txtUnits.Text.Replace(",", ""));
                    txtUnits.Text = value > 0 ? value.ToString("N0") : value.ToString();
                }
                else
                    txtUnits.Text = "0";
            }
        }

        private void txtCaseGroup_MouseHover(object sender, EventArgs e)
        {
            if (schedcall == null)
                return;

            TextBox TB = (TextBox)sender;
            int VisibleTime = 1300;  //in milliseconds

            ToolTip tt = new ToolTip();
            tt.Show(schedcall.PriorityDesc, TB, 0, 0, VisibleTime);
        }       

        private void btnSource_Click(object sender, EventArgs e)
        {
            frmSourceAddrPopup fmSAP = new frmSourceAddrPopup(samp.Masterid);
            fmSAP.ShowDialog();  //show child

            if (fmSAP.DialogResult == DialogResult.Cancel)
            {
                fmSAP.Dispose();
            }
        }

        private void dgProject_SelectionChanged(object sender, EventArgs e)
        {
            if (formloading) return;
            if (no_selection) return;
            
            if (editable && (id !=null))
            {
                bool data_modified = CheckFormChanged();

                if (data_modified || data_saved)
                {
                    if (enterPoint == TypeTFUEntryPoint.NPC)
                    {
                        if (cbCall.SelectedIndex == -1)
                        {
                            MessageBox.Show("Please enter a Call resolution");
                            cbCall.Focus();
                           
                            HighlightRowForId(id);
                            return;
                        }
                    }
                    
                    if (!ValidateFormData())
                    {
                        HighlightRowForId(id);

                        return;
                    }
                    if (data_modified)
                    {
                        if (enterPoint == TypeTFUEntryPoint.NPC)
                        {
                            if (!ValidateShedCall())
                                return;
                        }
                        if (!SaveData())
                        {
                            HighlightRowForId(id);

                            return;
                        }
                    }
                }

                if ((data_saved) || (schedcall.IsModified && !sched_data_saved))
                {
                    if (enterPoint == TypeTFUEntryPoint.NPC || enterPoint == TypeTFUEntryPoint.FORM)
                    {
                         
                        if (!SaveSchedCall())
                        {
                            HighlightRowForId(id);
                            return;
                        }
                    }
                }

                if (data_saved)
                    csda.AddCsdaccessData(id, "UPDATE");
                else
                    csda.AddCsdaccessData(id, "BROWSE");
            }
           

            if (dgProject.SelectedRows.Count > 0)
            {
                id = dgProject.SelectedRows[0].Cells[0].FormattedValue.ToString();
  
                // Update the project grid to reflect changes to the selection.
                LoadProjectData();
                DisplayProject();
            }
        }
        

        //find selected id
        private void HighlightRowForId(string id)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dgProject.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(id))
                {
                    rowIndex = row.Index;
                    break;
                }
            }
            formloading = true;
            if (rowIndex >=0)
                dgProject.Rows[rowIndex].Selected = true;
            formloading = false;
        }


        private DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        private void dgProject_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (!String.IsNullOrEmpty(id) && e.ListChangedType == ListChangedType.Reset)
            {
                int rowIndex = -1;
                foreach (DataGridViewRow rrow in dgProject.Rows)
                {
                    if (rrow.Cells["id"].Value.ToString().Equals(id))
                    {
                        rowIndex = rrow.Index;
                        break;
                    }
                }
                
                dgProject.BeginInvoke((MethodInvoker)delegate ()
                {
                    if (rowIndex > 0)
                    {
                        dgProject.Rows[rowIndex].Selected = true;
                        dgProject.CurrentCell = dgProject[0, rowIndex];
                    }
                    else
                    {
                        if (dgProject.Rows.Count > 0)
                            dgProject.Rows[0].Selected = true;
                    }
                    no_selection = false;
                });

            }
        }

        //variable to block selectedrow change event
        bool no_selection = false;
        private void dgProject_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                if (dgProject.DataSource != null)
                {
                    no_selection = true;
                   BindingContext[dgProject.DataSource].Position = 0;
                }
            }

        }

        private void dgVip_SelectionChanged(object sender, EventArgs e)
        {
            if (resetRow)
            {
                resetRow = false;
                dgVip.CurrentCell = dgVip.Rows[currentRow].Cells[currentCell];
            }
        }

        private void txtItem5ar_MouseClick(object sender, MouseEventArgs e)
        {
            txtItem5ar.SelectAll();
        }

        private void txtItem5br_MouseClick(object sender, MouseEventArgs e)
        {
            txtItem5br.SelectAll();
        }

        private void txtRvitm5cr_MouseClick(object sender, MouseEventArgs e)
        {
            txtRvitm5cr.SelectAll();
        }

        private void txtItem6r_MouseClick(object sender, MouseEventArgs e)
        {
            txtItem6r.SelectAll();
        }

        private void txtCapexpr_MouseClick(object sender, MouseEventArgs e)
        {
            txtCapexpr.SelectAll();
        }

        private void txtStrtdater_MouseClick(object sender, MouseEventArgs e)
        {
            txtStrtdater.SelectAll();
        }

        private void txtFutcompdr_MouseClick(object sender, MouseEventArgs e)
        {
            txtFutcompdr.SelectAll();
        }
        private void txtAddr3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtPcityst_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }
    }
}
