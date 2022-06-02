/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmC700.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/20/2015
Inputs:             Id
                    Callingform
                    Idlist
                    CurrIndex
                    Database source
                    Editmode
Parameters:		    None                
Outputs:		    None

Description:	    This screen displays/edit the Sample, Master, Soc, respondent,
                    cprsflags
                    and monthly vip data for a Id

Detailed Design:    Detailed User Requirements for C700 Screen 

Other:	            Called from: C700 search screen,
 
Revision History:	
***********************************************************************************
 Modified Date :  8/6/2019
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR3422 
 Description   :  Add History button
***********************************************************************************
 Modified Date :  9/23/2019
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   :  check select newtc popup, if nonresidential, 0000 is invalid
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
 Modified Date :  07/22/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : bug fix if screen is locked, and go to next case that isn't locked
                 dgFlag should be enabled
***********************************************************************************
 Modified Date :  09/17/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#7577
 Description   :disable the function that allow user change only 5c, strtdate,compdate, vip flag
***********************************************************************************
 Modified Date :  10/6/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#7650
 Description   :update description for flag5, flag6, remove "or Blank"
***********************************************************************************
 Modified Date :  12/7/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#7757
 Description   : if oflg is 1, fwgt field shows yellow background, otherwise show white
                 background
                 Save oadj and oflg during save data process
***********************************************************************************
 Modified Date :  1/21/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#
 Description   : SaveSchedCall() only update callstat when vip satisfied
***********************************************************************************
Modified Date :  2/9/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#
 Description   : allow analyst access respondent referrals
***********************************************************************************
Modified Date :  5/24/2022
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR#
 Description   : correct cumvip and pctvip for longer than 240 months cases, show all data
***********************************************************************************/

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

namespace Cprs
{
    public partial class frmC700 : Cprs.frmCprsParent
    {
        /*********************** Public Properties  *******************/
        /* Required */
        public string Id;
        public Form CallingForm = null;

        /*Optional*/
        public List<string> Idlist = null;
        public int CurrIndex = 0;

        public TypeEditMode EditMode = TypeEditMode.Edit;

        /***************************************************************/

        /*Form global variable */
        private CsdaccessData csda;
        private Sample samp;
        private SampleData sampdata;
        private Master mast;
        private MasterData mastdata;
        private Soc soc;
        private SocData socdata;
        private Respondent resp;
        private RespondentData respdata;
        private MonthlyVipsData mvsdata;
        private MonthlyVips mvs;
        private MonthlyVips mvsall;
        private Schedcall sc = null;

        private DataTable pdata;
        private DataTable rdata;
        private HistoryData hd;
        private SchedCallData scdata;
       
        private ProjMarkData pmarkdata;
        
        private RespMarkData rmarkdata;
        private CprflagsData flagdata;
        private Cprflags cflags;
        private ReferralData refdata;
        bool referral_exist = false;

        private List<Cpraudit> cprauditlist = new List<Cpraudit>();
        private ProjectAuditData auditData;
        private List<Vipaudit> vipauditlist = new List<Vipaudit>();
        private List<Respaudit> Respauditlist = new List<Respaudit>();

        private bool editable = false;
        private string default_flag = "";
        private string locked_by = String.Empty;

        private bool formloading = false;
        private string old_text;

        private bool status_changed = false;
        private int owner_changed = 0;
        private bool data_flag_changed = false;

        private float old_fwgt = 0;

        /*flag to use closing the form */
        private bool call_callingFrom = false;

        private TypeDBSource dbsource = TypeDBSource.Default;

        private string currYearMon = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2");

        /*Set up flags */
        private bool reject = false;

        private delegate void ShowLockMessageDelegate();

        //save current monthly vip row
        private int saveRow = 0;

        //flag to check the data has been saved or not
        private bool data_saved = false;

        private bool lag_changed = false;

        private bool referral_updated = false;

        private MySector ms;

        public frmC700()
        {
            InitializeComponent();
        }

        private void frmC700_Load(object sender, EventArgs e)
        {
            /*check required properties are set */
            if (String.IsNullOrEmpty(Id) || (CallingForm == null))
            {
                MessageBox.Show("Id, CallingForm are required properties for showing C700 form.");
                this.Dispose();
            }

            //load and display form
            LoadForm();
            DisplayForm();
        }

        /*load form from database*/
        private void LoadForm(bool from_refresh = false)
        {
            //inital flags
            status_changed = false;
            owner_changed = 0;

            /*Sample */
            sampdata = new SampleData();

            dbsource = sampdata.GetDatabaseSource(Id);
            samp = sampdata.GetSampleData(Id);
            old_fwgt = samp.Fwgt;

            /*Master */
            mastdata = new MasterData();
            mast = mastdata.GetMasterData(samp.Masterid);

            /*Soc */
            socdata = new SocData();
            if (mast.Owner == "M")
                soc = socdata.GetSocData(samp.Masterid);

            /*Respondent */
            respdata = new RespondentData();
            resp = respdata.GetRespondentData(samp.Respid);

            /*load monthly vips */
            mvsdata = new MonthlyVipsData();
            mvs = new MonthlyVips(Id);
            mvsall = new MonthlyVips(Id);
            mvs = mvsdata.GetDisplayMonthlyVips(Id, dbsource, samp.Rvitm5c, samp.Rvitm5cr);
            mvsall = mvsdata.GetMonthlyVips(Id, dbsource);

            //rebuild Status combo
            SetupStatusCombo();

            /*get flag string from database, generate cpr flags list*/
            flagdata = new CprflagsData();
            Dataflags flags = flagdata.GetCprflagsData(Id);
            cflags = new Cprflags(flags, mast.Owner, "edit");

            /* referral data */
            refdata = new ReferralData();
            referral_exist = refdata.CheckReferralExist(samp.Id, samp.Respid);
            lblReferral.Visible = referral_exist;
            referral_updated = false;

            /*Comment history */
            hd = new HistoryData();
            pdata = hd.GetProjCommentTable(Id, true);
            rdata = hd.GetRespCommentTable(samp.Respid, true);

            pmarkdata = new ProjMarkData();
            rmarkdata = new RespMarkData();

            auditData = new ProjectAuditData();

            //sched_call 
            scdata = new SchedCallData();
            sc = scdata.GetSchedCallData(Id);
            if (sc == null)
            {
                sc = new Schedcall(Id);
                sc.Added = "Y";
                sc.Coltec = resp.Coltec;
                sc.Callreq = "N";
                sc.Calltpe = "W";

                //set priority
                if (resp.Coltec == "P")
                    sc.Priority = "11";
                else if (resp.Coltec == "C")
                    sc.Priority = "21";
                else if (resp.Coltec == "F")
                    sc.Priority = "22";
                else
                    sc.Priority = "23";              
            }

            sc.Accestms = DateTime.Now.ToString("HHmmss");
            sc.IsModified = false;

            /*lock Respondent */
            if (!from_refresh)
            {
                locked_by = GeneralDataFuctions.ChkRespIDIsLocked(samp.Respid);
                if (String.IsNullOrEmpty(locked_by)) 
                {
                    bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, UserInfo.UserName);
                    editable = true;
                    lblLock.Visible = false;

                }
                else
                {
                    editable = false;
                    lblLock.Visible = true;
                }
            }

            //get my sector data
            MySectorsData md = new MySectorsData();
            ms = md.GetMySectorData(UserInfo.UserName);

            //if the screen is set to display, set editable to false.
            if (EditMode == TypeEditMode.Display)
                editable = false;

            cprauditlist.Clear();
            vipauditlist.Clear();
            Respauditlist.Clear();

            /*Add record to csdaccess */
            csda = new CsdaccessData();

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");

            data_saved = false;
        }


        /*display form */
        private void DisplayForm()
        {
            /*set up load form flag */
            formloading = true;

            /*load data to form */
            txtId.Text = samp.Id;
            if (samp.Id == samp.Respid)
                txtRespid.Text = "";
            else
                txtRespid.Text = samp.Respid;
            txtSelDate.Text = mast.Seldate;
            txtSelValue.Text = mast.Projselv > 0 ? mast.Projselv.ToString("#,#") : mast.Projselv.ToString();
            txtFwgt.Text = samp.Fwgt.ToString("N2");
            if (samp.Oflg == "1")
                txtFwgt.BackColor = Color.Yellow;
            else
                txtFwgt.BackColor = Color.White;
            txtNewtc.Text = mast.Newtc;
            txtSource.Text = mast.Source;
            txtFin.Text = mast.Fin;
            txtCnty.Text = mast.Dodgecou;
            txtContract.Text = samp.Contract;
            txtProjDesc.Text = samp.Projdesc;
            txtContact.Text = resp.Respname;
            txtPcityst.Text = samp.Pcityst;
            txtRespnote.Text = resp.Respnote;
            if (!string.IsNullOrEmpty((resp.Phone)))
            {
                txtPhone.Text = resp.Phone;
            }
            else
                txtPhone.Text = "";

            if (!String.IsNullOrEmpty(resp.Ext.Trim()))
                txtExt.Text = resp.Ext;
            else
                txtExt.Text = "";

            txtColHist.Text = resp.Colhist;

            txtItem5ar.Text = samp.Item5ar > 0 ? samp.Item5ar.ToString("#,#") : samp.Item5ar.ToString();
            txtItem5a.Text = samp.Item5a > 0 ? samp.Item5a.ToString("#,#") : samp.Item5a.ToString();
            txtFlag5a.Text = samp.Flag5a;
            txtItem5br.Text = samp.Item5br > 0 ? samp.Item5br.ToString("#,#") : samp.Item5br.ToString();
            txtItem5b.Text = samp.Item5b > 0 ? samp.Item5b.ToString("#,#") : samp.Item5b.ToString();
            txtFlag5b.Text = samp.Flag5b;
            txtRvitm5cr.Text = samp.Rvitm5cr > 0 ? samp.Rvitm5cr.ToString("#,#") : samp.Rvitm5cr.ToString();
            txtRvitm5c.Text = samp.Rvitm5c > 0 ? samp.Rvitm5c.ToString("#,#") : samp.Rvitm5c.ToString();
            txtFlagr5c.Text = samp.Flagr5c;
            txtItem6r.Text = samp.Item6r > 0 ? samp.Item6r.ToString("#,#") : samp.Item6r.ToString();
            txtItem6.Text = samp.Item6 > 0 ? samp.Item6.ToString("#,#") : samp.Item6.ToString();
            txtFlagItm6.Text = samp.Flagitm6;
            txtCapexpr.Text = samp.Capexpr > 0 ? samp.Capexpr.ToString("#,#") : samp.Capexpr.ToString();
            txtCapexp.Text = samp.Capexp > 0 ? samp.Capexp.ToString("#,#") : samp.Capexp.ToString();
            txtFlagcap.Text = samp.Flagcap;

            if (!String.IsNullOrEmpty(samp.Strtdate))
                txtStrtdate.Text = samp.Strtdate;
            else
                txtStrtdate.Text = "";
            txtStrtdater.Text = samp.Strtdater;
            txtFlagStrtdate.Text = samp.Flagstrtdate;
            if (!String.IsNullOrEmpty(samp.Compdate))
                txtCompdate.Text = samp.Compdate;
            else
                txtCompdate.Text = "";
            txtCompdater.Text = samp.Compdater;
            txtFlagCompdate.Text = samp.Flagcompdate;
            txtFutcompdr.Text = samp.Futcompdr;
            if (!String.IsNullOrEmpty(samp.Futcompd.Trim()))
                txtFutcompd.Text = samp.Futcompd;
            else
                txtFutcompd.Text = "";
            txtFlagFutcompd.Text = samp.Flagfutcompd;

            if (mast.Owner == "M")
            {
                txtBldgs.Text = soc.Rbldgs.ToString();
                txtUnits.Text = soc.Runits.ToString("N0");
                txtCostpu.Text = soc.Costpu.ToString("N0");
                txtBldgs.ReadOnly = false;
                txtBldgs.TabStop = true;
                txtUnits.ReadOnly = false;
                txtUnits.TabStop = true;
                txtCostpu.ReadOnly = false;
            }
            else
            {
                txtBldgs.ReadOnly = true;
                txtBldgs.TabStop = false;
                txtUnits.ReadOnly = true;
                txtUnits.TabStop = false;
                txtCostpu.ReadOnly = true;

                txtBldgs.Text = "";
                txtUnits.Text = "";
                txtCostpu.Text = "";
            }

            //set up status combo
            cbStatus.SelectedValue = samp.Status;
            txtStatus.Text = cbStatus.Text;

            cbColtec.SelectedItem = GetDisplayColtecText(resp.Coltec);
            txtColtec.Text = cbColtec.Text;

            txtSurvey.Text = mast.Owner;

            /*Show and hide Capexp base on owner */
            if (mast.Owner == "N" || mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W")
                SetupCapexp(true);
            else
                SetupCapexp(false);

            cbSurvey.SelectedItem = mast.Owner;
            cbLag.SelectedItem = resp.Lag.ToString();
            txtLag.Text = cbLag.Text;

            txtFipst.Text = mast.Fipstater;

            /*display monthly vips */
            saveRow = 0;
            displayMonthlyVips(true);

            /*display vip related fields*/
            SetupVipRelated();

            //set up screen editable
            SetupEditable();

            /*Show reject flag */
            if (dbsource == TypeDBSource.Hold)
                lblReject.Visible = true;
            else
                lblReject.Visible = false;

            /*set up referer button based on my sector */
           // if (ms != null && !ms.CheckInMySector(mast.Newtc))
                //btnRef.Enabled = false;
            //else
                btnRef.Enabled = true;

            /*Show referral flag */
            if (referral_exist)
                lblReferral.Visible = true;
            else
                lblReferral.Visible = false;

            //if there is flag, highlight related fields with red
            HighlightFieldsFromFlags();

            //set up flag grid
            SetupDataFlag();

            if (GeneralDataFuctions.CheckDodgeSlip(mast.Masterid))
                btnSlip.Enabled = true;
            else
                btnSlip.Enabled = false;

            txtDflag.Text = "";
            default_flag = "";

            /*display the first comment */
            if (pdata.Rows.Count != 0)
            {
                dgPcomments.DataSource = pdata;
                dgPcomments.Columns[0].Width = 75;
                dgPcomments.Columns[1].Width = 60;
                dgPcomments.ColumnHeadersVisible = false;
            }
            else
            {
                dgPcomments.DataSource = null;
            }

            /*Respondent comment list */
            if (rdata.Rows.Count != 0)
            {
                dgRcomments.DataSource = rdata;
            }
            else
                dgRcomments.DataSource = null;


            if (CheckMarkExists())
                lblMark.Text = "MARKED";
            else
                lblMark.Text = "";

            tabControl1.SelectedIndex = 0;

            /*if there is idlist, set count boxes */
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

            //set highlight to newtc field
            this.ActiveControl = txtNewtc;

            //disable name & address button if seldate is current month
            if (mast.Seldate == GeneralFunctions.CurrentYearMon())
                btnName.Enabled = false;

            formloading = false;

            BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
        }

        private void SetupVipRelated()
        {
            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
            txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();

            //if the display monthly vip didn't have all vips, add *
            if (!mvs.Withallvips)
            {
                txtCumvipr2.Visible = true;
                txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                txtCumvip2.Visible = true;
                txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                txtCompr.Text = mvsall.GetCumPercentr(samp.Rvitm5cr).ToString();
                txtComp.Text = mvsall.GetCumPercent(samp.Rvitm5c).ToString();
                txtSumMnths.Text = mvsall.GetSumMons().ToString();
                
                txtCumvipr.Visible = true;
                txtCumvip.Visible = true;

            }
            else
            {
                txtSumMnths.Text = mvs.GetSumMons().ToString();
                txtCumvipr2.Text = txtCumvipr.Text;
                txtCumvip2.Text = txtCumvip.Text;
                txtCumvipr.Visible = false;
                txtCumvip.Visible = false;
                txtCompr.Text = mvs.GetCumPercentr(samp.Rvitm5cr).ToString();
                txtComp.Text = mvs.GetCumPercent(samp.Rvitm5c).ToString();
            }
        }

        //show lock message
        private void ShowLockMessage()
        {
            /*show message if the case locked by someone */
            if (locked_by != "")
                MessageBox.Show("The case is locked by " + locked_by + ", cannot be edited.");
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
                coltec_text= "A-Admin";

            return coltec_text;

        }

        //set up flag grid
        private void SetupDataFlag()
        {
            if (cflags.displayFlaglist.Count > 0)
            {
                dgFlags.DataSource = null;
                dgFlags.DataSource = cflags.displayFlaglist;
                dgFlags.Columns[0].Visible = false;
                dgFlags.Columns[1].Visible = false;
                dgFlags.Columns[2].ReadOnly = true;
                dgFlags.Columns[2].HeaderText = "FLAG";
                dgFlags.Columns[3].Width = 60;
                dgFlags.Columns[3].HeaderText = "BYPASS";
            }
            else
                dgFlags.DataSource = null;

            dgFlags.Refresh();

            //initial data flag changed flag
            data_flag_changed = false;
        }

        /*Setup form editable */
        private void SetupEditable()
        {
            if (!editable || EditMode == TypeEditMode.Display)
            {
                //set all textbox as read only
                txtColHist.ReadOnly = true;
                txtNewtc.ReadOnly = true;
                if (mast.Owner == "M")
                {
                    txtBldgs.ReadOnly = true;
                    txtUnits.ReadOnly = true;
                    txtCostpu.ReadOnly = true;

                }
                txtProjDesc.ReadOnly = true;
                txtPcityst.ReadOnly = true;
                txtRespnote.ReadOnly = true;
                txtRespnote.BackColor = txtRespnote.BackColor;
                txtRespnote.ForeColor = Color.Red;
                txtContact.ReadOnly = true;
                txtPhone.ReadOnly = true;
                txtExt.ReadOnly = true;
                txtDflag.ReadOnly = true;
                txtRvitm5c.ReadOnly = true;
                txtItem5a.ReadOnly = true;
                txtItem5b.ReadOnly = true;
                txtCapexp.ReadOnly = true;
                txtStrtdate.ReadOnly = true;
                txtCompdate.ReadOnly = true;
                txtContract.ReadOnly = true;
                txtFutcompd.ReadOnly = true;
                txtItem6.ReadOnly = true;

                cbStatus.Enabled = true;
                cbStatus.Visible = false;
                txtStatus.Visible = true;
                cbColtec.Visible = false;
                txtColtec.Visible = true;
                cbSurvey.Visible = false;
                txtFipst.Visible = true;
                cbLag.Visible = false;
                txtLag.Visible = true;

                txtSurvey.Visible = true;

                btnProcess.Enabled = false;
                btnRefresh.Enabled = false;
                BtnClear.Enabled = false;
                btnNewtc.Enabled = false;

                dgVip.EditMode = DataGridViewEditMode.EditProgrammatically;
                dgFlags.EditMode = DataGridViewEditMode.EditProgrammatically;
            }
            else
            {
                cbStatus.Visible = true;
                if (mast.Owner == "M" && samp.Status == "5")
                    cbStatus.Enabled = false;
                else
                    cbStatus.Enabled = true;

                txtStatus.Visible = false;
                cbColtec.Visible = true;
                txtColtec.Visible = false;
                cbLag.Visible = true;
                txtLag.Visible = false;

                txtFipst.Visible = true;

                txtNewtc.ReadOnly = false;
                if (mast.Owner == "M")
                {
                    txtBldgs.ReadOnly = false;
                    txtUnits.ReadOnly = false;
                    txtCostpu.ReadOnly = true;
                    cbSurvey.Visible = false;
                    txtSurvey.Visible = true;
                    txtSurvey.TabStop = false;
                }
                else
                {
                    cbSurvey.Visible = true;
                    txtSurvey.Visible = false;
                }
                txtProjDesc.ReadOnly = false;
                txtPcityst.ReadOnly = false;
                txtRespnote.ReadOnly = true;
                txtRespnote.BackColor = txtRespnote.BackColor;
                txtRespnote.ForeColor = Color.Red;
                txtContact.ReadOnly = false;
                txtPhone.ReadOnly = false;
                txtExt.ReadOnly = false;
                txtDflag.ReadOnly = false;
                txtRvitm5c.ReadOnly = false;
                txtItem5a.ReadOnly = false;
                txtItem5b.ReadOnly = false;
                txtCapexp.ReadOnly = false;
                txtStrtdate.ReadOnly = false;
                txtCompdate.ReadOnly = false;
                txtContract.ReadOnly = false;
                txtFutcompd.ReadOnly = false;
                txtItem6.ReadOnly = false;

                btnProcess.Enabled = true;
                btnRefresh.Enabled = true;
                btnNewtc.Enabled = true;
                BtnClear.Enabled = true;
                dgVip.EditMode = DataGridViewEditMode.EditOnEnter;
                dgFlags.EditMode = DataGridViewEditMode.EditOnEnter;

            }
        }

        /*Hide and show Captial Exp */
        private void SetupCapexp(bool showfields)
        {
            if (!showfields)
            {
                label34.Visible = false;
                txtCapexp.Visible = false;
                txtCapexpr.Visible = false;
                txtFlagcap.Visible = false;
            }
            else
            {
                label34.Visible = true;
                txtCapexp.Visible = true;
                txtCapexpr.Visible = true;
                txtFlagcap.Visible = true;
            }
        }

        /*set up grid for monthly vips */
        private void displayMonthlyVips(bool go_first_row = false)
        {
            //Save current row
            if (dgVip.Rows.Count > 0 && !go_first_row)
                saveRow = dgVip.FirstDisplayedCell.RowIndex;

            dgVip.DataSource = null;
            dgVip.DataSource = mvs.monthlyViplist;

            if (saveRow != 0 && saveRow < dgVip.Rows.Count)
                dgVip.FirstDisplayedScrollingRowIndex = saveRow;
            else
                dgVip.FirstDisplayedScrollingRowIndex = 0;

            dgVip.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgVip.RowHeadersVisible = false; // set it to false if not needed

            dgVip.AutoResizeColumns();
            for (int i = 0; i < dgVip.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgVip.Columns[i].HeaderText = "DATE6";
                    dgVip.Columns[i].Visible = false;

                }
                else if (i == 1)
                {
                    dgVip.Columns[i].HeaderText = "DATE8";
                    dgVip.Columns[i].ReadOnly = true;
                    dgVip.Columns[i].Width = 40;
                    dgVip.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (i == 2)
                {
                    dgVip.Columns[i].HeaderText = "VIPDATAR";
                    dgVip.Columns[i].Visible = true;
                    dgVip.Columns[i].ReadOnly = true;
                    dgVip.Columns[i].Width = 60;
                    dgVip.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                    dgVip.Columns[i].DefaultCellStyle.Format = "N0";

                }
                else if (i == 3)
                {
                    dgVip.Columns[i].HeaderText = "PCT5CR";
                    dgVip.Columns[i].Visible = true;
                    dgVip.Columns[i].ReadOnly = true;
                    dgVip.Columns[i].Width = 60;
                    dgVip.Columns[i].DefaultCellStyle.BackColor = Color.LightGray;
                }
                else if (i == 4)
                {
                    dgVip.Columns[i].HeaderText = "VIPDATA";
                    dgVip.Columns[i].Visible = true;
                    dgVip.Columns[i].Width = 60;
                    DataGridViewTextBoxColumn cvip = (DataGridViewTextBoxColumn)dgVip.Columns[i];//here index of column
                    cvip.MaxInputLength = 6;
                    dgVip.Columns[i].DefaultCellStyle.Format = "N0";

                }
                else if (i == 5)
                {
                    dgVip.Columns[i].HeaderText = "VIPFLAG";
                    dgVip.Columns[i].ReadOnly = true;
                    dgVip.Columns[i].Width = 60;
                }
                else if (i == 6)
                {
                    dgVip.Columns[i].HeaderText = "PCT5C";
                    dgVip.Columns[i].Width = 60;
                    DataGridViewTextBoxColumn vipp = (DataGridViewTextBoxColumn)dgVip.Columns[i];//here index of column
                    vipp.MaxInputLength = 3;
                }
                else
                    dgVip.Columns[i].Visible = false;

            }

            this.dgVip.CellValidating += new DataGridViewCellValidatingEventHandler(dgVip_CellValidating);
            this.dgVip.CellEndEdit += new DataGridViewCellEventHandler(dgVip_CellEndEdit);
            this.dgVip.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(dgVip_EditingControlShowing);

        }

        private int oldvalue;
        private string oldflag;
        private string oldvs;
        private bool isvalid;
        private Int32 currentRow;
        private Int32 currentCell;
        private bool resetRow = false;

        //validating datagrid cell
        private void dgVip_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgVip.Columns[e.ColumnIndex].ReadOnly == false) 
            {
                if (e.ColumnIndex == 4 || e.ColumnIndex == 6)
                {
                    oldvalue = Convert.ToInt32(dgVip[e.ColumnIndex, e.RowIndex].Value);
                    oldvs = dgVip[7, e.RowIndex].Value.ToString();

                    isvalid = true;
                }
                else if (e.ColumnIndex == 5)
                {
                    oldflag = dgVip[e.ColumnIndex, e.RowIndex].Value.ToString();
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

        //update datagrid cell
        public void dgVip_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (!editable || EditMode == TypeEditMode.Display)
                return;

            if (!isvalid)
            {
                if (e.ColumnIndex == 4)
                {
                    dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                    dgVip[7, e.RowIndex].Value = oldvs;

                }
                else if (e.ColumnIndex == 5)
                {
                    dgVip[e.ColumnIndex, e.RowIndex].Value = oldflag;
                    dgVip[7, e.RowIndex].Value = oldvs;
                }
                else if (e.ColumnIndex == 6)
                {
                    dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                    dgVip[7, e.RowIndex].Value = oldvs;

                }

                return;
            }
            else
            {
                bool blankflag = false;

                //dgVip[e.ColumnIndex, e.RowIndex].Style.ForeColor = Color.Red;
                if (e.ColumnIndex == 4)
                {
                    int newvalue = 0;

                    newvalue = Convert.ToInt32(dgVip[e.ColumnIndex, e.RowIndex].Value);

                    if (oldvalue != newvalue)
                    {
                        string date6 = dgVip[0, e.RowIndex].Value.ToString();
                        int i5c = int.Parse(txtRvitm5c.Text.Replace(",", ""));
                        int i5cr = int.Parse(txtRvitm5cr.Text.Replace(",", ""));

                        if (txtStrtdater.Text.Trim() == "")
                        {
                            MessageBox.Show("A start date must be entered before VIP data can be entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtCompdate.Text.Trim().Length > 0) && (Convert.ToInt32(date6) > Convert.ToInt32(txtCompdate.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP after completion date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtStrtdate.Text.Trim().Length > 0) && (Convert.ToInt32(date6) < Convert.ToInt32(txtStrtdate.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP before Start Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if (newvalue > 0 && i5c > 0 && newvalue > i5c)
                        {
                            MessageBox.Show("VIP for " + date6 + " is greater than 100% of 5C.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else
                        {
                            string oldflag = dgVip[e.ColumnIndex + 1, e.RowIndex].Value.ToString();
                            string newflag = default_flag;

                            if ((default_flag == "") || (dgVip[e.ColumnIndex, e.RowIndex].Value.ToString() == "0"))
                            {
                                frmC700flagSelPopup popup = new frmC700flagSelPopup(newvalue);
                                popup.StartPosition = FormStartPosition.CenterParent;
                                DialogResult dialogresult = popup.ShowDialog();
                                
                                if (dialogresult == DialogResult.OK)
                                {
                                    dgVip[e.ColumnIndex + 1, e.RowIndex].Value = popup.selectedFlag;
                                    newflag = popup.selectedFlag;
                                    if (popup.setDefault)
                                    {
                                        default_flag = popup.selectedFlag;
                                        txtDflag.Text = default_flag;
                                    }
                                    else if (popup.selectedFlag == "B")
                                        blankflag = true;
                                }

                                popup.Dispose();
                            }
                            else
                            {
                                newflag = default_flag;
                                dgVip[e.ColumnIndex + 1, e.RowIndex].Value = default_flag;
                            }


                            if (i5c > 0)
                            {
                                dgVip[e.ColumnIndex + 2, e.RowIndex].Value = Convert.ToInt32((double)newvalue / i5c * 100);
                            }
                            else
                            {
                                dgVip[e.ColumnIndex + 2, e.RowIndex].Value = 0;
                            }

                            if (newflag == "R" || blankflag)
                            {
                                dgVip[2, e.RowIndex].Value = newvalue;
                                if (i5cr > 0)
                                    dgVip[3, e.RowIndex].Value = Convert.ToInt32((double)newvalue / i5cr * 100);
                            }

                            if (oldvs == "i" || oldvs == "a")
                                dgVip[7, e.RowIndex].Value = "a";
                            else if ((oldvs == "e") || (oldvs == "m"))
                            {
                                if (blankflag)
                                    dgVip[7, e.RowIndex].Value = "d";
                                else
                                    dgVip[7, e.RowIndex].Value = "m";
                            }
                            else if (oldvs == "d")
                                dgVip[7, e.RowIndex].Value = "m";

                            txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();

                            if (mvs.Withallvips)
                            {
                                txtComp.Text = mvs.GetCumPercent(i5c).ToString();
                                txtSumMnths.Text = mvs.GetSumMons().ToString();
                                txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                                txtCompr.Text = mvs.GetCumPercentr(i5c).ToString();
                                txtCumvip2.Text = txtCumvip.Text;
                                txtCumvipr2.Text = txtCumvipr.Text;
                            }
                            else
                            {
                                //update mvsall
                                foreach (var value in mvsall.monthlyViplist)
                                {
                                    if (value.Date6 == date6)
                                    {
                                        value.Vipdata = newvalue;
                                        value.Vipflag = dgVip[e.ColumnIndex + 1, e.RowIndex].Value.ToString();
                                        if (newflag == "R" || blankflag)
                                        {
                                            value.Vipdatar = newvalue;
                                        }
                                    }
                                }

                                txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                                txtComp.Text = mvsall.GetCumPercent(i5c).ToString();
                                txtSumMnths.Text = mvsall.GetSumMons().ToString();
                                txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                                txtCompr.Text = mvsall.GetCumPercentr(i5c).ToString();
                            }

                           
                            AddVipauditData(date6, oldvalue, oldflag, newvalue, newflag);

                            oldvalue = newvalue;

                            resetRow = true;
                            currentRow = e.RowIndex;
                            currentCell = e.ColumnIndex;

                        }

                    }
                }
                else if (e.ColumnIndex == 6)
                {
                    int newvalue = 0;

                    newvalue = Convert.ToInt32(dgVip[e.ColumnIndex, e.RowIndex].Value);

                    if (oldvalue != newvalue)
                    {
                        string date6 = dgVip[0, e.RowIndex].Value.ToString();
                        int i5c = int.Parse(txtRvitm5c.Text.Replace(",", ""));
                        int i5cr = int.Parse(txtRvitm5cr.Text.Replace(",", ""));

                        if (txtStrtdater.Text.Trim() == "")
                        {
                            MessageBox.Show("A start date must be entered before VIP data can be entered", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtCompdate.Text.Trim().Length > 0) && (Convert.ToInt32(date6) > Convert.ToInt32(txtCompdate.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP Percent after completion date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgVip[e.ColumnIndex, e.RowIndex].Value = oldvalue;
                            dgVip[7, e.RowIndex].Value = oldvs;
                        }
                        else if ((txtStrtdate.Text.Trim().Length > 0) && (Convert.ToInt32(date6) < Convert.ToInt32(txtStrtdate.Text)))
                        {
                            MessageBox.Show("Cannot enter VIP Percent before start date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            string oldflag = dgVip[e.ColumnIndex - 1, e.RowIndex].Value.ToString();
                            string newflag = default_flag;
                            if ((default_flag == "") || (dgVip[e.ColumnIndex, e.RowIndex].Value.ToString() == "0"))
                            {
                                frmC700flagSelPopup popup = new frmC700flagSelPopup(newvalue);
                                popup.StartPosition = FormStartPosition.CenterParent;
                                DialogResult dialogresult = popup.ShowDialog();
                                if (dialogresult == DialogResult.OK)
                                {
                                    newflag = popup.selectedFlag;
                                    dgVip[e.ColumnIndex - 1, e.RowIndex].Value = popup.selectedFlag;

                                    if (popup.setDefault)
                                    {
                                        default_flag = popup.selectedFlag;
                                        txtDflag.Text = default_flag;
                                    }
                                    else if (popup.selectedFlag == "B")
                                        blankflag = true;
                                }

                                popup.Dispose();
                            }
                            else
                            {
                                newflag = default_flag;
                                dgVip[e.ColumnIndex - 1, e.RowIndex].Value = default_flag;
                            }

                            int vipvalue = 0;
			                int oldvipvalue = 0;
                            oldvipvalue = (int)dgVip[e.ColumnIndex - 2, e.RowIndex].Value;
                            if (i5c > 0)
                            {
                                vipvalue = Convert.ToInt32(newvalue * 0.01 * i5c);
                                dgVip[e.ColumnIndex - 2, e.RowIndex].Value = vipvalue;
                            }
                            else
                            {
                                dgVip[e.ColumnIndex, e.RowIndex].Value = 0;
                            }

                            if (newflag == "R" || blankflag)
                            {
                                dgVip[2, e.RowIndex].Value = vipvalue;
                                if (i5cr > 0)
                                    dgVip[3, e.RowIndex].Value = Convert.ToInt32((double)vipvalue / i5cr * 100);
                            }

                            if (oldvs == "i" || oldvs == "a")
                                dgVip[7, e.RowIndex].Value = "a";
                            else if ((oldvs == "e") || (oldvs == "m"))
                            {
                                if (blankflag)
                                    dgVip[7, e.RowIndex].Value = "d";
                                else
                                    dgVip[7, e.RowIndex].Value = "m";
                            }
                            else if (oldvs == "d")
                                dgVip[7, e.RowIndex].Value = "m";

                            txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                            
                            if (mvs.Withallvips)
                            {
                                txtComp.Text = mvs.GetCumPercent(i5c).ToString();
                                txtSumMnths.Text = mvs.GetSumMons().ToString();
                                txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                                txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                                txtCompr.Text = mvs.GetCumPercentr(i5c).ToString();
                                txtCumvip2.Text = txtCumvip.Text;
                                txtCumvipr2.Text = txtCumvipr.Text;
                            }
                            else
                            {
                                //update mvsall
                                foreach (var value in mvsall.monthlyViplist)
                                {
                                    if (value.Date6 == date6)
                                    {
                                        value.Vipdata = vipvalue;
                                        value.Vipflag = dgVip[e.ColumnIndex - 1, e.RowIndex].Value.ToString();
                                        if (newflag == "R" || blankflag)
                                        {
                                            value.Vipdatar = vipvalue;
                                        }
                                    }
                                }

                                txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                                txtComp.Text = mvsall.GetCumPercent(i5c).ToString();
                                txtSumMnths.Text = mvsall.GetSumMons().ToString();
                                txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                                txtCompr.Text = mvsall.GetCumPercentr(i5c).ToString();
                            }

                            AddVipauditData(date6, oldvipvalue, oldflag, vipvalue, newflag);

                            oldvalue = newvalue;

                            resetRow = true;
                            currentRow = e.RowIndex;
                            currentCell = e.ColumnIndex;
                        }
                    }

                }
            }
        }

        private static KeyPressEventHandler NumbericCheckHandler = new KeyPressEventHandler(NumbericCheck);
        private static void NumbericCheck(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '-';
        }

        //only enter numeric
        private void dgVip_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgVip.CurrentCell.ColumnIndex == 4 || dgVip.CurrentCell.ColumnIndex == 6)
            {
                e.Control.KeyPress -= NumbericCheckHandler;
                e.Control.KeyPress += NumbericCheckHandler;
            }

        }
        
        // for only edit flag cell (the function was comment out for CR#7577)
        private void dgVip_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit && e.RowIndex !=-1)
            //{
            //    if (e.ColumnIndex == 5)
            //    {
            //        string date6 = dgVip[0, e.RowIndex].Value.ToString();
            //        if (txtStrtdate.Text.Trim().Length == 0)
            //        {
            //            // MessageBox.Show("Cannot enter flag without Start Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        else if ((txtStrtdate.Text.Trim().Length > 0) && (Convert.ToInt32(date6) < Convert.ToInt32(txtStrtdate.Text)))
            //        {
            //            MessageBox.Show("Cannot enter flag before Start Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        else if ((txtCompdate.Text.Trim().Length > 0) && (Convert.ToInt32(date6) > Convert.ToInt32(txtCompdate.Text)))
            //        {
            //            MessageBox.Show("Cannot enter flag after Completion Date.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        }
            //        else
            //        {
            //            int newvalue = Convert.ToInt32(dgVip.Rows[e.RowIndex].Cells[e.ColumnIndex - 1].Value);
            //            frmC700flagSelPopup popup = new frmC700flagSelPopup(newvalue);
            //            popup.StartPosition = FormStartPosition.CenterParent;
            //            DialogResult dialogresult = popup.ShowDialog();
            //            if (dialogresult == DialogResult.OK)
            //            {
            //                if (dgVip.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != popup.selectedFlag)
            //                {
            //                    string oldflag = dgVip.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            //                    dgVip.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = popup.selectedFlag;
            //                    oldvs = dgVip[7, e.RowIndex].Value.ToString();

            //                    string newflag = popup.selectedFlag;
                                
            //                    if (popup.selectedFlag == "R")
            //                    {
            //                        int i5c = int.Parse(txtRvitm5c.Text.Replace(",", ""));
            //                        int i5cr = int.Parse(txtRvitm5cr.Text.Replace(",", ""));

            //                        dgVip[2, e.RowIndex].Value = newvalue;
            //                        if (i5cr > 0)
            //                            dgVip[3, e.RowIndex].Value = Convert.ToInt32((double)newvalue / i5cr * 100);
            //                        else
            //                            dgVip[3, e.RowIndex].Value = 0;

            //                        txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
            //                        txtComp.Text = mvs.GetCumPercent(i5c).ToString();
            //                        txtSumMnths.Text = mvs.GetSumMons().ToString();
            //                        txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
            //                        txtCompr.Text = mvs.GetCumPercentr(i5c).ToString();
            //                    }
            //                    AddVipauditData(date6, newvalue, oldflag, newvalue, newflag);
            //                }
            //            }

            //            if (oldvs == "i" || oldvs == "a")
            //                dgVip[7, e.RowIndex].Value = "a";
            //            else if ((oldvs == "e") || (oldvs == "m"))
            //            {
            //                if(popup.selectedFlag == "B")
            //                    dgVip[7, e.RowIndex].Value = "d";
            //                else
            //                    dgVip[7, e.RowIndex].Value = "m";
            //            }
            //            else if (oldvs == "d")
            //                dgVip[7, e.RowIndex].Value = "m";

            //            popup.Dispose();
            //        }
            //    }
            //}
        }

        //in case data enter is too large and create error
        private void dgVip_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgVip.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
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
                ca.Id = Id;
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

        private void btnReturn_Click(object sender, EventArgs e)
        {
            if (editable && (EditMode == TypeEditMode.Edit))
            {
                if (!ValidateFormData())
                    return;
                if (CheckFormChanged())
                {
                    //DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result2 == DialogResult.Yes)
                    //{
                        if (!SaveData())
                            return;
                        
                    //}

                }
                if (data_saved || referral_updated)
                    SaveSchedcall();

                if (CallingForm != null && CallingForm.Name != "frmName")
                    GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");

                if (data_saved)
                    csda.AddCsdaccessData(Id, "UPDATE");
                else
                    csda.AddCsdaccessData(Id, "BROWSE");
            }

            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }

            this.Close();
        }

        private void frmC700_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*unlock respondent */
            if (editable)
            {
                bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
            }

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
            
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }

        /*Add cpraudit data */
        private void AddCprauditData(string avarnme, string aoldval, string aoldflag, string anewval, string anewflag)
        {
            /*Get audit record from list */
            Cpraudit au = (from Cpraudit j in cprauditlist
                           where j.Varnme == avarnme
                           select j).SingleOrDefault();

            /*if there is no record, add one, otherwise update the record */
            if (au == null)
            {
                Cpraudit ca = new Cpraudit();
                ca.Id = Id;
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
            /*Get audit record from list */
            Respaudit au = (from Respaudit j in Respauditlist
                            where j.Varnme == avarnme
                            select j).SingleOrDefault();

            /*if there is no record, add one, otherwise update the record */
            if (au == null)
            {
                Respaudit ca = new Respaudit();
                ca.Respid = samp.Respid;
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

        /*Read data from screen to class */
        private void ReadFromScreen()
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
                    if (samp.Compdate.Equals(txtCompdate.Text, StringComparison.Ordinal) && samp.Compdate !="")
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
                    if ((mast.Owner == "N" || mast.Owner == "T" || mast.Owner == "E" || mast.Owner == "G" || mast.Owner == "R" || mast.Owner == "O" || mast.Owner == "W") && (!txtCapexp.Visible))
                    {
                        txtCapexp.Text = "0";
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

            //check coltec change
            if (cbColtec.Text.Substring(0,1) != resp.Coltec)
            {
                AddRspauditData("COLTEC", resp.Coltec, cbColtec.Text.Substring(0, 1));
                resp.Coltec = cbColtec.Text.Substring(0, 1);
            }

            if (!resp.Colhist.Equals(txtColHist.Text, StringComparison.Ordinal))
                resp.Colhist = txtColHist.Text;

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

                if (soc.Costpu != Convert.ToInt32(txtCostpu.Text.Replace(",", "")))
                {
                    soc.Costpu = Convert.ToInt32(txtCostpu.Text.Replace(",", ""));
                }

            }
            if (!samp.Pcityst.Equals(txtPcityst.Text, StringComparison.Ordinal))
                samp.Pcityst = txtPcityst.Text;

            if (!samp.Projdesc.Equals(txtProjDesc.Text, StringComparison.Ordinal))
                samp.Projdesc = txtProjDesc.Text;

            if (!resp.Respname.Equals(txtContact.Text, StringComparison.Ordinal))
            {
                AddRspauditData("RESPNAME", resp.Respname, txtContact.Text);
                resp.Respname = txtContact.Text;
            }

            if (!resp.Respnote.Equals(txtRespnote.Text, StringComparison.Ordinal))
            {
                AddRspauditData("RESPNOTE", resp.Respnote, txtRespnote.Text);
                resp.Respnote = txtRespnote.Text;
            }

            if (resp.Lag != Convert.ToInt32(cbLag.Text))
            {
                AddRspauditData("LAG", resp.Lag.ToString(), cbLag.Text);
                resp.Lag = Convert.ToInt32(cbLag.Text);
                lag_changed = true;
            }

            string rphone = new string(txtPhone.Text.Where(char.IsDigit).ToArray());
            if (!resp.Phone.Equals(rphone, StringComparison.Ordinal))
            {
                AddRspauditData("PHONE", resp.Phone, rphone);
                resp.Phone = rphone;
            }

            if (!resp.Ext.Equals(txtExt.Text.Trim(), StringComparison.Ordinal))
            {
                AddRspauditData("EXT", resp.Ext, txtExt.Text.Trim());
                resp.Ext = txtExt.Text.Trim();
            }

            int rc = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
            if (samp.Rvitm5c != rc)
            {
                AddCprauditData("RVITM5C", samp.Rvitm5c.ToString(), samp.Flagr5c, rc.ToString(), txtFlagr5c.Text);
                samp.Rvitm5c = rc;
            }
            rc = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
            if (samp.Rvitm5cr != rc)
                samp.Rvitm5cr = rc;
            if (samp.Flagr5c != txtFlagr5c.Text)
                samp.Flagr5c = txtFlagr5c.Text;

            if (samp.Item5c == 0 && (txtFlagr5c.Text == "R" || txtFlagr5c.Text == "A"))
            {
                samp.Item5c = rc;
                samp.Flag5c = txtFlagr5c.Text;
            }

            rc = Convert.ToInt32(txtItem5a.Text.Replace(",", ""));
            if (samp.Item5a != rc)
            {
                AddCprauditData("ITEM5A", samp.Item5a.ToString(), samp.Flag5a, rc.ToString(), txtFlag5a.Text);
                samp.Item5a = rc;
            }
            rc = Convert.ToInt32(txtItem5ar.Text.Replace(",", ""));
            if (samp.Item5ar != rc)
                samp.Item5ar = rc;
            if (samp.Flag5a != txtFlag5a.Text)
                samp.Flag5a = txtFlag5a.Text;

            rc = Convert.ToInt32(txtItem5b.Text.Replace(",", ""));
            if (samp.Item5b != rc)
            {
                AddCprauditData("ITEM5B", samp.Item5b.ToString(), samp.Flag5b, rc.ToString(), txtFlag5b.Text);
                samp.Item5b = rc;
            }
            rc = Convert.ToInt32(txtItem5br.Text.Replace(",", ""));
            if (samp.Item5br != rc)
                samp.Item5br = rc;
            if (samp.Flag5b != txtFlag5b.Text)
                samp.Flag5b = txtFlag5b.Text;


            rc = Convert.ToInt32(txtCapexp.Text.Replace(",", ""));
            if (samp.Capexp != rc)
            {
                AddCprauditData("CAPEXP", samp.Capexp.ToString(), samp.Flagcap, rc.ToString(), txtFlagcap.Text);
                samp.Capexp = rc;
            }
            rc = Convert.ToInt32(txtCapexpr.Text.Replace(",", ""));
            if (samp.Capexpr != rc)
                samp.Capexpr = rc;
            if (samp.Flagcap != txtFlagcap.Text)
                samp.Flagcap = txtFlagcap.Text;

            rc = Convert.ToInt32(txtItem6.Text.Replace(",", ""));
            if (samp.Item6 != rc)
            {
                AddCprauditData("ITEM6", samp.Item6.ToString(), samp.Flagitm6, rc.ToString(), txtFlagItm6.Text);
                samp.Item6 = rc;
            }

            rc = Convert.ToInt32(txtItem6r.Text.Replace(",", ""));
            if (samp.Item6r != rc)
                samp.Item6r = rc;
            if (samp.Flagitm6 != txtFlagItm6.Text)
                samp.Flagitm6 = txtFlagItm6.Text;


            if (!samp.Contract.Equals(txtContract.Text, StringComparison.Ordinal))
            {
                AddCprauditData("CONTRACT", samp.Contract.ToString(), "", txtContract.Text, "");
                samp.Contract = txtContract.Text;
            }

            if (!samp.Strtdate.Equals(txtStrtdate.Text, StringComparison.Ordinal))
            {
                AddCprauditData("STRTDATE", samp.Strtdate, samp.Flagstrtdate, txtStrtdate.Text.Trim(), txtFlagStrtdate.Text);
                samp.Strtdate = txtStrtdate.Text;

                if ((txtFlagStrtdate.Text == "R" || txtFlagStrtdate.Text == "A") && samp.Repsdate == "")
                    samp.Repsdate = currYearMon;
                if (txtStrtdate.Text == "" && samp.Repsdate != "")
                    samp.Repsdate = "";
            }
            if (!samp.Strtdater.Equals(txtStrtdater.Text, StringComparison.Ordinal))
                samp.Strtdater = txtStrtdater.Text;
            if (samp.Flagstrtdate != txtFlagStrtdate.Text)
                samp.Flagstrtdate = txtFlagStrtdate.Text;

            if (!samp.Compdate.Equals(txtCompdate.Text, StringComparison.Ordinal))
            {
                if (samp.Compdate != "" && txtCompdate.Text == "")
                {
                    if (samp.Status == "4" || samp.Status == "5" || samp.Status == "6")
                        samp.Active = "I";
                    else if (samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8")
                        samp.Active = "A";
                }
                else if (samp.Compdate == "" && txtCompdate.Text != "" && samp.Status == "1" || samp.Status == "2" || samp.Status == "3" || samp.Status == "7" || samp.Status == "8")
                {
                    samp.Active = "C";
                }

                //in case compdate flag didn't get set
                if (txtCompdater.Text.Trim() == "")
                    txtFlagCompdate.Text = "B";

                AddCprauditData("COMPDATE", samp.Compdate, samp.Flagcompdate, txtCompdate.Text.Trim(), txtFlagCompdate.Text);
                samp.Compdate = txtCompdate.Text;
                if (txtFlagCompdate.Text == "R")
                    samp.Repcompd = currYearMon;
				else if (txtFlagCompdate.Text == "B")
					samp.Repcompd = "";
            }
            if (!samp.Compdater.Equals(txtCompdater.Text, StringComparison.Ordinal))
                samp.Compdater = txtCompdater.Text;
            if (samp.Flagcompdate != txtFlagCompdate.Text)
                samp.Flagcompdate = txtFlagCompdate.Text;

            if (!samp.Futcompd.Equals(txtFutcompd.Text.Trim(), StringComparison.Ordinal))
            {
                AddCprauditData("FUTCOMPD", samp.Futcompd, samp.Flagfutcompd, txtFutcompd.Text.Trim(), txtFlagFutcompd.Text);
                samp.Futcompd = txtFutcompd.Text;
            }
            if (!samp.Futcompdr.Equals(txtFutcompdr.Text, StringComparison.Ordinal))
                samp.Futcompdr = txtFutcompdr.Text;
            if (samp.Flagfutcompd != txtFlagFutcompd.Text)
                samp.Flagfutcompd = txtFlagFutcompd.Text;

        }

        //Verify form data
        private bool ValidateFormData()
        {
            //check newtc
            if (!ValidateNewtc())
            {
                MessageBox.Show("The Newtc value entered is invalid.");
                txtNewtc.Focus();
                return false;
            }

            //check units and bldgs
            if (txtSurvey.Text == "M")
            {
                if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < 2)
                {
                    MessageBox.Show("Runits must be a number between 2 and 9999.");
                    txtUnits.Focus();
                    return false;

                }
                if (txtUnits.Text != "" && txtBldgs.Text != "")
                {
                    if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2)
                    {
                        MessageBox.Show("Runits must be greater than or equal to two times Rbldgs.");
                        txtUnits.Focus();
                        return false;
                    }
                }

            }

            //check strtdate
            if (txtStrtdate.Modified)
            {
                if (!CheckStrtdateEnter())
                {
                    txtStrtdate.Focus();
                    return false;
                }
            }

            //check compdate 
            if (txtCompdate.Modified)
            {
                if (!CheckCompdateEnter())
                {
                    txtCompdate.Focus();
                    return false;
                }
            }

            if (txtFutcompd.Modified)
            {
                if (!CheckFutcompdEnter())
                {
                    txtFutcompd.Focus();
                    return false;
                }
            }

            //check Rvitm5C
            if (txtRvitm5c.Modified)
            {
                if (!VerifyRvitm5c())
                {
                    txtRvitm5c.Focus();
                    return false;
                }
            }

            //check item5a
            if (txtItem5a.Modified)
            {
                if (!CheckItem5aEnter())
                {
                    txtItem5a.Focus();
                    return false;
                }
            }

            //check item5b
            if (txtItem5b.Modified)
            {
                if (!CheckItem5bEnter())
                {
                    txtItem5b.Focus();
                    return false;
                }
            }

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

            return true;

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
                if (samp.IsModified || mast.IsModified || resp.IsModified || mvs.IsModified || data_flag_changed)
                    return true;
                else
                    return false;
            }
            else if (samp.IsModified || mast.IsModified || resp.IsModified || soc.IsModified || mvs.IsModified || data_flag_changed)
                return true;
            else
                return false;
        }
        
        //Check vip satisfied or not
        private bool CheckVipSatisfied()
        {
            bool vip_satisfied = false;

            string month_date = DateTime.Today.AddMonths(-1).ToString("yyyyMM");
            if (samp.Compdate != "")
                vip_satisfied = true;
            else
            {
                //Convert sdate to datatime
                var dt = DateTime.ParseExact(month_date, "yyyyMM", CultureInfo.InvariantCulture);
                int lag = 0;
                
                if (mvs.GetMonthVip(month_date).Vipflag == "R" || mvs.GetMonthVip(month_date).Vipflag == "A")
                    vip_satisfied = true;
                else
                {
                    if (cbLag.SelectedIndex > 0)
                    {
                        lag = cbLag.SelectedIndex;
                        month_date = (dt.AddMonths(-lag)).ToString("yyyyMM", CultureInfo.InvariantCulture);
                        if (mvs.GetMonthVip(month_date).Vipflag == "R" || mvs.GetMonthVip(month_date).Vipflag == "A")
                            vip_satisfied = true;
                    }
                }
            }

            return vip_satisfied;
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
            //if current owner is 'N' the new owner is not, set change flag
            else if (oldowner == "N" )
            {
                if (newowner != "N")
                    changed = true;
            }
            //if current owner is 'T, E,G,R,O,W' the new owner is not, set change flag
            else if (oldowner == "T" || oldowner == "E" || oldowner == "G" || oldowner == "R" || oldowner == "O" || oldowner == "W")
            {
                if (newowner != "T" && newowner != "E" && newowner != "G" && newowner != "R" && newowner != "O" && newowner != "W")
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

            if (editable && (EditMode == TypeEditMode.Edit))
            {
                if (ValidateFormData())
                {
                    if (CheckFormChanged())
                    {
                        DialogResult result3 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result3 == DialogResult.Yes)
                        {

                            if (!SaveData())
                                can_close = false;
                        }                        
                    }
                    if (data_saved || referral_updated)
                        SaveSchedcall();
                }
                else
                    can_close = false;
            }

            if (data_saved)
                csda.AddCsdaccessData(Id, "UPDATE");
            else
                csda.AddCsdaccessData(Id, "BROWSE");

            return can_close;

        }


        /*Save all Data */
        private bool SaveData()
        {
            TypeDBSource dbsave = TypeDBSource.Default;
            float sampwt = mastdata.GetSampWeight(mast);

            //set fwgt
            float new_fwgt = samp.Fwgt;
            float new_oadj = samp.Oadj;

            bool report_reject = false;

            //obtain data flag from current objects
            Dataflags data_flags = cflags.GetMainFlags(samp, mast, soc, mvs, cprauditlist, vipauditlist, status_changed, owner_changed, sampwt, "edit", ref new_fwgt, ref new_oadj,  ref reject, ref report_reject);
            if (new_fwgt != samp.Fwgt)
            {
                txtFwgt.Text = new_fwgt.ToString("N2");
            }

            //if it is reject case, show message
            if (reject)
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
                {
                    samp.Oflg = "1";
                    txtFwgt.BackColor = Color.Yellow;
                }
                else
                {
                    txtFwgt.BackColor = Color.White;
                    samp.Oflg = "0";
                }
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
                {
                    sampdata.AddSampleData(samp, dbsave);   
                }
                else
                {
                    sampdata.SaveSampleData(samp, dbsave);

                    //the original data from hold, but there is no reject, delete the record from hold table
                    if (dbsource == TypeDBSource.Hold && dbsave == TypeDBSource.Default)
                    {
                        sampdata.DeleteSampleData(samp.Id, dbsource);
                    }
                }
                samp.IsModified = false;
            }

            /*save master */
            if (mast.IsModified)
            {
                mastdata.SaveMasterData(mast);
                mast.IsModified = false;
            }

            /*save respondent */
            if (resp.IsModified)
            {
                respdata.SaveRespondentData(resp);
                resp.IsModified = false;
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
                    txtCostpu.Text = soc.Costpu.ToString("N0");
                }
            }

            ///*Save flags */
            ///temp code, need get report flags
            flagdata.SaveflagsData(Id, data_flags.currflags, data_flags.reportflags);

            /*Save cpraudit data, clear Cprauditlist */
            if (cprauditlist.Count > 0)
            {
                foreach (Cpraudit element in cprauditlist)
                {
                    auditData.AddCprauditData(element);
                }
                cprauditlist.Clear();
            }

            /*Save vipaudit data, clear vipauditlist */
            if (vipauditlist.Count > 0)
            {
                foreach (Vipaudit element in vipauditlist)
                {
                    auditData.AddVipauditData(element);
                }
                vipauditlist.Clear();
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

            //reseet modified flags
            txtRvitm5c.Modified = false;
            txtItem5a.Modified = false;
            txtItem5b.Modified = false;
            txtStrtdate.Modified = false;
            txtCompdate.Modified = false;
            txtFutcompd.Modified = false;

            status_changed = false;
            owner_changed = 0;

            data_saved = true;

            return true;
        }

        //check other ids for vip satisfied
        private bool CheckVipstatifiedForID(string cid)
        {
            bool satisfied = false;
            int lag = Convert.ToInt32(cbLag.Text) + 1;

            SampleData sdata = new SampleData();
            MonthlyVipsData mdata = new MonthlyVipsData();

            TypeDBSource dsource = sdata.GetDatabaseSource(cid);
            Sample sp = sdata.GetSampleData(cid);
            if (sp.Compdate == "")
            {
                MonthlyVips monvip = mdata.GetMonthlyVips(cid, dsource);
                MonthlyVip mvv = monvip.GetMonthVip(DateTime.Now.AddMonths(-1).ToString("yyyyMM"));
                if (mvv != null && mvv.Vipflag == "R")
                    satisfied = true;
                else
                {
                    MonthlyVip mvv1 = monvip.GetMonthVip(DateTime.Now.AddMonths(-lag).ToString("yyyyMM"));
                    if (mvv1 != null && mvv1.Vipflag == "R")
                        satisfied = true;
                }
            }
            else
                satisfied = true;

            return satisfied;
        }

        private void SaveSchedcall()
        {
            int time_factor = GeneralDataFuctions.GetTimezoneFactor(resp.Rstate);

            if (lag_changed)
            {
                //get all active project for the respid
                List<string> aidlist = GeneralDataFuctions.GetActiveProjectIds(resp.Respid);
                if (aidlist.Count > 0)
                {
                    foreach (string aid in aidlist)
                    {
                        Sample asamp = sampdata.GetSampleData(aid);
                        Master amaster = mastdata.GetMasterData(asamp.Masterid);

                        Schedcall sscc = scdata.GetSchedCallData(aid);

                        if (samp.Id == aid && (samp.Compdate != "" || referral_exist || CheckVipSatisfied()))
                        {
                            //if (referral_exist)
                            //    sscc.Callstat = "";
                            //else
                                sscc.Callstat = "V";
                            sscc.Callreq = "N";
                            sscc.Complete = "Y";
                        }
                        else if (asamp.Compdate != "" || CheckVipstatifiedForID(aid))
                        {
                            sscc.Callstat = "V";
                            sscc.Callreq = "N";
                            sscc.Complete = "Y";
                        }
                        else
                        {
                            if (!CheckVipstatifiedForID(aid) && sscc.Callstat == "V")
                               sscc.Callstat = "";
                            sscc.Callreq = "Y";
                            sscc.Complete = "N";

                        }
                        sscc.Callcnt = sc.Callcnt + 1;
                        sscc.Accescde = resp.Coltec;
                        sscc.Accesday = DateTime.Now.ToString("MMdd");
                        sscc.Accestms = sc.Accestms;
                        sscc.Accestme = DateTime.Now.ToString("HHmmss");
                        sscc.Accesnme = UserInfo.UserName;
                    
                        scdata.SaveSchedcallData(sscc);

                        Schedhist sh = new Schedhist(aid);

                        sh.Owner = amaster.Owner;
                        if (sscc.Callstat != "V")
                            sh.Callstat = " ";
                        else
                            sh.Callstat = sscc.Callstat;
                        sh.Accesday = DateTime.Now.ToString("MMdd");
                        sh.Accestms = sscc.Accestms;
                        sh.Accestme = DateTime.Now.ToString("HHmmss");
                        sh.Accesnme = UserInfo.UserName;
                        sh.Accescde = resp.Coltec;

                        scdata.AddSchedHistData(sh);
                    }
                    lag_changed = false;
                }
            }
            else
            {
                if (samp.Status != "1")
                {
                    sc.Complete = "Y";
                    sc.Callreq = "N";
                    //sc.Callstat = " ";
                }
                //if vip satisfied
                else if (samp.Compdate != "" || CheckVipSatisfied() || referral_exist)
                {
                    //if (referral_exist)
                    //    sc.Callstat = " ";
                    //else
                        sc.Callstat = "V";
                    sc.Complete = "Y";
                    sc.Callreq = "N";
                }
                else
                {
                    if (sc.Added == "Y")
                    {
                        sc.Complete = "N";
                        sc.Callreq = "Y";
                        sc.Callstat = "";
                        sc.Apptdate = "";
                        sc.Appttime = (8 + time_factor).ToString("00") + "00";
                        sc.Apptends = "1700";
                    }
                    if (sc.Callstat == "V" && !CheckVipSatisfied())
                      sc.Callstat = " ";

                }
                sc.Callcnt = sc.Callcnt + 1;
                sc.Accescde = resp.Coltec;
                sc.Accesday = DateTime.Now.ToString("MMdd");
                sc.Accestme = DateTime.Now.ToString("HHmmss");
                sc.Accesnme = UserInfo.UserName;
            
                scdata.SaveSchedcallData(sc);

                //add record to sched_hist table
                Schedhist shist = new Schedhist(Id);
                shist.Owner = mast.Owner;
                if (sc.Callstat != "V" )
                    shist.Callstat = " ";
                else
                    shist.Callstat = sc.Callstat;
                shist.Accesday = DateTime.Now.ToString("MMdd");
                shist.Accestms = sc.Accestms;
                shist.Accestme = DateTime.Now.ToString("HHmmss");
                shist.Accesnme = UserInfo.UserName;
                shist.Accescde = resp.Coltec;

                scdata.AddSchedHistData(shist);
            }
        }

        /*Set default flag on textbox */
        private void SetDefaultFlag(TextBox itemBox, TextBox flagBox, bool checkDefault = true)
        {
            if (!editable || EditMode == TypeEditMode.Display || itemBox.Text == "")
                return;

            if (checkDefault)
            {
                if ((default_flag == "") || (itemBox.Text == "0"))
                {
                    int nvalue = Convert.ToInt32(itemBox.Text.Replace(",", ""));
                    frmC700flagSelPopup popup = new frmC700flagSelPopup(nvalue);
                    popup.StartPosition = FormStartPosition.CenterParent;
                    DialogResult dialogresult = popup.ShowDialog();
                    if (dialogresult == DialogResult.OK)
                    {
                        flagBox.Text = popup.selectedFlag;
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
                    flagBox.Text = default_flag;
                }
            }
            else
            {
                int nvalue = Convert.ToInt32(itemBox.Text.Replace(",", ""));
                frmC700flagSelPopup popup = new frmC700flagSelPopup(nvalue);
                popup.StartPosition = FormStartPosition.CenterParent;
                DialogResult dialogresult = popup.ShowDialog();
                if (dialogresult == DialogResult.OK)
                {
                    flagBox.Text = popup.selectedFlag;
                }

                popup.Dispose();

            }
        }

        private void txtFlag5a_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtItem5a.Text != "")
            //    {
            //        SetDefaultFlag(txtItem5a, txtFlag5a, false);
            //        if (txtFlag5a.Text == "R" || txtFlag5b.Text == "B")
            //            txtItem5ar.Text = txtItem5a.Text;
            //    }
            //}

        }

        private void txtItem5a_Enter(object sender, EventArgs e)
        {
            old_text = txtItem5a.Text;
        }
        private void txtItem5a_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckItem5aEnter())
            {
                txtItem5a.Text = old_text;
                if (txtFlag5a.Text == "R" || txtFlag5a.Text == "B")
                    txtItem5ar.Text = txtItem5a.Text;
                e.Cancel = true;
            }
        }

        //Verify Item5a
        private bool CheckItem5aEnter()
        {
            int value = Convert.ToInt32(txtItem5a.Text.Replace(",", ""));
            if (value > 0 && txtStrtdate.Text == "")
            {
                MessageBox.Show("Item 5A rejected, must have a value for Start Date.");
                return false;
            }
            return true;
        }

        private void txtItem5a_Validated(object sender, EventArgs e)
        {
            if (old_text != txtItem5a.Text)
            {
                SetDefaultFlag(txtItem5a, txtFlag5a);
                if (txtFlag5a.Text == "R" || txtFlag5a.Text == "B")
                    txtItem5ar.Text = txtItem5a.Text;

                txtItem5a.Modified = false;
            }
        }

        private void txtItem5a_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtItem5a.Text != "")
                {
                    value = Convert.ToInt32(txtItem5a.Text.Replace(",", ""));
                    txtItem5a.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtItem5a.SelectionStart = txtItem5a.Text.Length;
                }
                else
                    txtItem5a.Text = "0";

                txtItem5a.Modified = true;

            }
        }
        private void txtItem5a_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }
        private void txtItem5a_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit && old_text != txtItem5a.Text)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtItem5a.Text != "")
                    {
                        if (!CheckItem5aEnter())
                        {
                            txtItem5a.Text = old_text;
                            if (txtFlag5a.Text == "R")
                                txtItem5ar.Text = txtItem5a.Text;
                            return;
                        }
                        SetDefaultFlag(txtItem5a, txtFlag5a);
                        if (txtFlag5a.Text == "R" || txtFlag5a.Text == "B")
                            txtItem5ar.Text = txtItem5a.Text;

                        txtItem5a.Modified = false;
                        old_text = txtItem5a.Text;
                    }
                }
            }
        }


        private void txtItem5b_Enter(object sender, EventArgs e)
        {
            old_text = txtItem5b.Text;
        }

        private void txtItem5b_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckItem5bEnter())
            {
                txtItem5b.Text = old_text;
                if (txtFlag5b.Text == "R" || txtFlag5b.Text == "B")
                    txtItem5br.Text = txtItem5b.Text;
                e.Cancel = true;
            }
        }

        //Verify Item5b
        private bool CheckItem5bEnter()
        {
            int value = Convert.ToInt32(txtItem5b.Text.Replace(",", ""));
            if (value > 0 && txtStrtdate.Text == "")
            {
                MessageBox.Show("Item 5B rejected, must have a value for Start Date.");
                return false;
            }
            return true;
        }


        private void txtItem5b_Validated(object sender, EventArgs e)
        {
            if (old_text != txtItem5b.Text)
            {
                SetDefaultFlag(txtItem5b, txtFlag5b);
                if (txtFlag5b.Text == "R" || txtFlag5b.Text == "B")
                    txtItem5br.Text = txtItem5b.Text;

                txtItem5b.Modified = false;
            }
        }
        private void txtItem5b_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtItem5b.Text != "")
                {
                    value = Convert.ToInt32(txtItem5b.Text.Replace(",", ""));
                    txtItem5b.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtItem5b.SelectionStart = txtItem5b.Text.Length;
                }
                else
                    txtItem5b.Text = "0";
            }
        }

        private void txtFlag5b_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtItem5b.Text != "")
            //    {
            //        SetDefaultFlag(txtItem5b, txtFlag5b, false);
            //        if (txtFlag5b.Text == "R" || txtFlag5b.Text == "B")
            //            txtItem5br.Text = txtItem5b.Text;
            //    }
            //}
        }

        private void txtItem5b_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtItem5b_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtItem5b.Text)
                {
                    if (txtItem5b.Text != "")
                    {
                        if (!CheckItem5bEnter())
                        {
                            txtItem5b.Text = old_text;
                            if (txtFlag5b.Text == "R")
                                txtItem5br.Text = txtItem5b.Text;
                            return;
                        }
                    }
                    SetDefaultFlag(txtItem5b, txtFlag5b);
                    if (txtFlag5b.Text == "R")
                        txtItem5br.Text = txtItem5b.Text;

                    txtItem5b.Modified = false;
                    old_text = txtItem5b.Text;
                }
            }

        }

        private void txtRvitm5c_Enter(object sender, EventArgs e)
        {
            old_text = txtRvitm5c.Text;
        }

        private void txtRvitm5c_Validating(object sender, CancelEventArgs e)
        {
            if (!VerifyRvitm5c() && old_text != txtRvitm5c.Text)
            {
                //txtRvitm5c.Focus();
                e.Cancel = true;
            }
        }

        //Verify Rvitm5C
        private bool VerifyRvitm5c()
        {
            int value = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
            if (value == 0 && txtFlagr5c.Text != "B" && mvs.GetCumvipr()>0)
            {
                MessageBox.Show("Item 5C cannot be zero.");
                txtRvitm5c.Text = old_text;
                if (txtFlagr5c.Text == "R")
                {
                    txtRvitm5cr.Text = old_text;
                }
                UpdateRelatedVIps();

                return false;
            }
            else if (value > 0 && txtStrtdate.Text == "")
            {
                MessageBox.Show("Item 5C rejected, must have a value for Start Date.");
                txtRvitm5c.Text = old_text;
                if (txtFlagr5c.Text == "R")
                {
                    txtRvitm5cr.Text = old_text;
                }
                UpdateRelatedVIps();

                return false;
            }
            return true;
        }

        private void txtRvitm5c_Validated(object sender, EventArgs e)
        {
            if (old_text != txtRvitm5c.Text)
            {
                SetDefaultFlag(txtRvitm5c, txtFlagr5c);
                if (txtFlagr5c.Text == "R" || txtFlagr5c.Text == "B")
                    txtRvitm5cr.Text = txtRvitm5c.Text;

                UpdateRelatedVIps();
                txtRvitm5c.Modified = false;
            }
        }
        
        //Updated related Vips data
        private void UpdateRelatedVIps()
        {
            int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
            int it5cr = Convert.ToInt32(txtRvitm5cr.Text.Replace(",", ""));
            if (it5c > 0)
            {
                mvs.UpdatePct5cs(it5c, it5cr);
                displayMonthlyVips();
                if (!mvs.Withallvips)
                {
                    txtComp.Text = mvsall.GetCumPercent(it5c).ToString();
                    txtCompr.Text = mvsall.GetCumPercentr(it5c).ToString();
                }
                else
                {
                    txtComp.Text = mvs.GetCumPercent(it5c).ToString();
                    txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                }

                txtRvitm5c.Modified = false;

                //if it is mulitfamily
                SetupCostput();
            }
        }

        private void txtRvitm5c_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtRvitm5c.Text != "")
                {
                    value = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                    txtRvitm5c.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtRvitm5c.SelectionStart = txtRvitm5c.Text.Length;
                }
                else
                    txtRvitm5c.Text = "0";
            }
        }

        private void txtRvitm5c_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtRvitm5c.Text)
                {
                    if (VerifyRvitm5c())
                    {
                        SetDefaultFlag(txtRvitm5c, txtFlagr5c);
                        if (txtFlagr5c.Text == "R" || txtFlagr5c.Text == "B")
                            txtRvitm5cr.Text = txtRvitm5c.Text;

                        //update vip data
                        UpdateRelatedVIps();
                        old_text = txtRvitm5c.Text;
                    }
                    
                }
            }

        }

        private void txtRvitm5c_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFlagr5c_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtRvitm5c.Text != "" && Convert.ToInt32(txtRvitm5c.Text.Replace(",", "")) > 0)
            //    {
            //        SetDefaultFlag(txtRvitm5c, txtFlagr5c, false);
            //        if (txtFlagr5c.Text == "R" || txtFlagr5c.Text == "B")
            //        {
            //            txtRvitm5cr.Text = txtRvitm5c.Text;

            //            UpdateRelatedVIps();
            //        }
            //    }
            //}
        }


        private void txtItem6_Enter(object sender, EventArgs e)
        {
            old_text = txtItem6.Text;
        }

        private void txtItem6_Validated(object sender, EventArgs e)
        {
            if (old_text != txtItem6.Text)
            {
                SetDefaultFlag(txtItem6, txtFlagItm6);
                if (txtFlagItm6.Text == "R" || txtFlagItm6.Text == "B")
                    txtItem6r.Text = txtItem6.Text;
            }
        }

        private void txtItem6_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtItem6.Text != "")
                {
                    value = Convert.ToInt32(txtItem6.Text.Replace(",", ""));
                    txtItem6.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtItem6.SelectionStart = txtItem6.Text.Length;
                }
                else
                    txtItem6.Text = "0";

            }
        }
        private void txtItem6_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtItem6.Text)
                {
                    SetDefaultFlag(txtItem6, txtFlagItm6);
                    if (txtFlagItm6.Text == "R" || txtFlagItm6.Text == "B")
                        txtItem6r.Text = txtItem6.Text;
                    old_text = txtItem6.Text;
                }
            }

        }
        private void txtItem6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtFlagItm6_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtItem6.Text != "")
            //    {
            //        SetDefaultFlag(txtItem6, txtFlagItm6, false);
            //        if (txtFlagItm6.Text == "R")
            //            txtItem6r.Text = txtItem6.Text;
            //    }
            //}
        }

        private void txtCapexp_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtCapexp.Text != "")
                {
                    value = Convert.ToInt32(txtCapexp.Text.Replace(",", ""));
                    txtCapexp.Text = value > 0 ? value.ToString("#,#") : value.ToString();
                    txtCapexp.SelectionStart = txtCapexp.Text.Length;
                }
                else
                    txtCapexp.Text = "0";
            }
        }

        private void txtCapexp_Validated(object sender, EventArgs e)
        {
            if (txtCapexp.Text != old_text)
            {
                SetDefaultFlag(txtCapexp, txtFlagcap);
                if (txtFlagcap.Text == "R")
                    txtCapexpr.Text = txtCapexp.Text;
            }
        }

        private void txtCapexp_Enter(object sender, EventArgs e)
        {
            old_text = txtCapexp.Text;
        }

        private void txtCapexp_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter && txtCapexp.Text != old_text)
                {
                    SetDefaultFlag(txtCapexp, txtFlagcap);
                    if (txtFlagcap.Text == "R")
                        txtCapexpr.Text = txtCapexp.Text;
                    old_text = txtCapexp.Text;
                }
            }
        }

        private void txtCapexp_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFlagcap_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtCapexp.Text != "")
            //    {
            //        SetDefaultFlag(txtCapexp, txtFlagcap, false);
            //        if (txtFlagcap.Text == "R")
            //            txtCapexpr.Text = txtCapexp.Text;
            //    }
            //}
        }


        private void txtFlagStrtdate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtStrtdate.Text != "")
            //    {
            //        SetDefaultFlag(txtStrtdate, txtFlagStrtdate, false);
            //        if (txtFlagStrtdate.Text == "R")
            //            txtStrtdater.Text = txtStrtdate.Text;
            //    }
            //}
        }

        private void txtFlagCompdate_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtCompdate.Text != "")
            //    {
            //        SetDefaultFlag(txtCompdate, txtFlagCompdate, false);
            //        if (txtFlagCompdate.Text == "R")
            //            txtCompdater.Text = txtCompdate.Text;
            //    }
            //}
        }

        private void txtFlagFutcompd_KeyDown(object sender, KeyEventArgs e)
        {
            //if (editable && EditMode == TypeEditMode.Edit)
            //{
            //    if (txtFutcompd.Text != "")
            //    {
            //        SetDefaultFlag(txtFutcompd, txtFlagFutcompd, false);
            //        if (txtFlagFutcompd.Text == "R")
            //            txtFutcompdr.Text = txtFutcompd.Text;
            //    }
            //}
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
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (old_text != txtBldgs.Text)
                {
                    if (txtBldgs.Text == "" || Convert.ToInt32(txtBldgs.Text) < 1)
                    {
                        MessageBox.Show("Rbldgs must be a number between 1 and 999.");
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
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtBldgs.Text)
                    {
                        if (txtBldgs.Text == "" || Convert.ToInt32(txtBldgs.Text) < 1)
                        {
                            MessageBox.Show("Rbldgs must be a number between 1 and 999.");
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
                    MessageBox.Show("Runits must be a number between 2 and 9999.");
                    txtUnits.Text = old_text;
                    e.Cancel = true;
                }
                else if (txtUnits.Text != "" && txtBldgs.Text != "")
                {
                    if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2)
                    {
                        MessageBox.Show("Runits must be greater than or equal to two times Rbldgs.");
                        txtUnits.Text = old_text;
                        e.Cancel = true;
                    }
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
            if (editable && EditMode == TypeEditMode.Edit)
            {
             if (e.KeyCode == Keys.Enter)
                {
                    if (old_text != txtUnits.Text)
                    {
                        if (txtUnits.Text == "" || Convert.ToInt32(txtUnits.Text.Replace(",", "")) < 2)
                        {
                            MessageBox.Show("Runits must be a number between 2 and 9999.");
                            txtUnits.Text = old_text;
                        }
                        else if (txtUnits.Text != "" && txtBldgs.Text != "")
                        {
                            if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2)
                            {
                                MessageBox.Show("Runits must be greater than or equal to two times Rbldgs.");
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
        }

        private void txtUnits_Leave(object sender, EventArgs e)
        {
            if (old_text != txtUnits.Text)
            {
                if (txtUnits.Text == "" || Convert.ToInt32(txtUnits.Text.Replace(",", "")) < 2)
                {
                    MessageBox.Show("Runits must be a number between 2 and 9999.");
                    txtUnits.Text = old_text;
                    return;
                }
                else if (txtUnits.Text != "" && txtBldgs.Text != "")
                {
                    if (Convert.ToInt32(txtUnits.Text.Replace(",", "")) < Convert.ToInt32(txtBldgs.Text) * 2)
                    {
                        MessageBox.Show("Runits must be greater than or equal to two times Rbldgs.");
                        txtUnits.Text = old_text;
                        return;
                    }
                    else
                    {
                        old_text = txtUnits.Text;

                        //if it is mulitfamily
                        SetupCostput();
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
                int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                if (it5c > 0)
                {
                    int iunits = Convert.ToInt32(txtUnits.Text.Replace(",", ""));
                    if (iunits > 0)
                    {
                        double yy = (double)it5c / iunits;
                        txtCostpu.Text = Math.Round(yy).ToString("N0");
                    }
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (ValidateFormData())
            {
                if (CheckFormChanged())
                {
                    if (SaveData())
                    {
                        flagdata = new CprflagsData();
                        Dataflags flags = flagdata.GetCprflagsData(Id);
                        cflags = new Cprflags(flags, mast.Owner, "edit");
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

        //select newtc
        private void btnNewtc_Click(object sender, EventArgs e)
        {
            frmNewtcSel popup = new frmNewtcSel();
            popup.CaseOwner = mast.Owner;
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                string old_tc = txtNewtc.Text;
                txtNewtc.Text = popup.SelectedNewtc;
                if (!ValidateNewtc())
                {
                    MessageBox.Show("The Newtc value entered is invalid.");
                    txtNewtc.Text = old_tc;
                }
            }

            popup.Dispose();
        }

        private void txtPhone_Enter(object sender, EventArgs e)
        {
            old_text = txtPhone.Text;
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
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
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Phone number is invalid.");
                    txtPhone.Focus();
                    txtPhone.Text = old_text;
                }
            }
        }

        private void btnPrevCase_Click(object sender, EventArgs e)
        {
            if (CurrIndex == 0)
            {
                MessageBox.Show("You are at the first observation.");
            }
            else
            {
                if (editable && (EditMode == TypeEditMode.Edit))
                {
                    if (!ValidateFormData())
                        return;
                    if (CheckFormChanged())
                    {
                        //DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (result2 == DialogResult.Yes)
                        //{
                            
                            if (!SaveData())
                                return;
                            
                        //}

                    }
                    if (data_saved || referral_updated)
                        SaveSchedcall();

                    bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
                }

                //add csd access data
                if (data_saved)
                    csda.AddCsdaccessData(Id, "UPDATE");
                else
                    csda.AddCsdaccessData(Id, "BROWSE");
                
                CurrIndex = CurrIndex - 1;
                Id = Idlist[CurrIndex];

                LoadForm();
                DisplayForm();
            }

        }

        private void btnNextCase_Click(object sender, EventArgs e)
        {
            if (Idlist == null || Idlist.Count ==1)
            {
                if (editable && (EditMode == TypeEditMode.Edit))
                {
                    if (!ValidateFormData())
                        return;
                    if (CheckFormChanged())
                    {
                        //DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        //if (result2 == DialogResult.Yes)
                        //{
                           
                            if (!SaveData())
                                return;
                            
                        //}                        
                    }
                    if (data_saved || referral_updated)
                        SaveSchedcall();
                }

                if (data_saved)
                    csda.AddCsdaccessData(Id, "UPDATE");
                else
                    csda.AddCsdaccessData(Id, "BROWSE");

                frmIdPopup popup = new frmIdPopup();
                popup.CallingForm = this;
                popup.StartPosition = FormStartPosition.CenterParent;
                DialogResult dialogresult = popup.ShowDialog();
                if (dialogresult == DialogResult.OK)
                {
                    /*unlock respondent */
                    if (editable && (EditMode == TypeEditMode.Edit))
                    {
                        bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
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
                    MessageBox.Show("You are at the last observation.");
                }
                else
                {
                    if (editable && (EditMode == TypeEditMode.Edit))
                    {
                        if (!ValidateFormData())
                            return;
                        if (CheckFormChanged())
                        {
                            //DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            //if (result2 == DialogResult.Yes)
                            //{
                                if (!SaveData())
                                    return;
                                
                            //}

                        }
                        if (data_saved || referral_updated)
                            SaveSchedcall();

                        /*unlock respondent */
                        bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
                    }

                    if (data_saved)
                        csda.AddCsdaccessData(Id, "UPDATE");
                    else
                        csda.AddCsdaccessData(Id, "BROWSE");

                    CurrIndex = CurrIndex + 1;
                    Id = Idlist[CurrIndex];

                    LoadForm();
                    DisplayForm();
                }

            }
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

        private void txtStrtdate_Enter(object sender, EventArgs e)
        {
            old_text = txtStrtdate.Text;
        }

        private void txtStrtdate_Validating(object sender, CancelEventArgs e)
        {
            if (old_text != txtStrtdate.Text && !CheckStrtdateEnter())
            {
                txtStrtdate.Text = old_text;
                e.Cancel = true;
            }
            else
                old_text = txtStrtdate.Text;
        }

        private void txtStrtdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtStrtdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtStrtdate.Text)
                {
                    if (!CheckStrtdateEnter())
                    {
                        txtStrtdate.Text = old_text;
                    }
                    else
                        old_text = txtStrtdate.Text;
                }
            }
        }

        //Verify entered Strtdate
        private bool CheckStrtdateEnter()
        {
            if (txtStrtdate.Text == "")
            {
                if (txtCompdate.Text != "" || txtRvitm5c.Text != "0" || txtItem5a.Text != "0" || txtItem5b.Text != "0")
                {
                    MessageBox.Show("Start date cannot be blank.");
                    return false;
                }

                //if the project has future strtdate, status is "4", change to "1"
                if (old_text != "" &&  cbStatus.SelectedValue.ToString() == "4")
                {
                    cbStatus.SelectedIndex = 0;
                }

                if (mvs.GetSumMons() > 0)
                {
                    DialogResult result1 = MessageBox.Show("There is reported data, Erase?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        foreach (var value in mvs.monthlyViplist)
                        {
                            AddVipauditData(value.Date6, value.Vipdata, value.Vipflag, 0, "B");

                            value.Vipdata = 0;
                            value.Vipdatar = 0;
                            value.Vipflag = "B";
                            value.Pct5c = 0;
                            value.vs = "d";
                        }
                        displayMonthlyVips();
                        if (mvs.Withallvips)
                        {
                            txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                            int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                            txtComp.Text = mvs.GetCumPercent(it5c).ToString();
                            txtSumMnths.Text = mvs.GetSumMons().ToString();
                            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                            txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                            txtCumvip2.Text = txtCumvip.Text;
                            txtCumvipr2.Text = txtCumvipr.Text;
                        }
                        else
                        {
                            foreach (var value in mvsall.monthlyViplist)
                            {
                                value.Vipdata = 0;
                                value.Vipdatar = 0;
                                value.Vipflag = "B";
                                value.Pct5c = 0;
                                value.vs = "d";
                            }
                            txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                            txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                            int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                            txtComp.Text = mvsall.GetCumPercent(it5c).ToString();
                            txtSumMnths.Text = mvsall.GetSumMons().ToString();
                            txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                            txtCompr.Text = mvsall.GetCumPercentr(it5c).ToString();

                        }
                        
                    }
                    else
                    {
                        return false;
                    }
                }

                //blank out the futcompd if the start date is empty
                if (txtFutcompd.Text != "")
                {
                    txtFutcompd.Text = "";
                    txtFlagFutcompd.Text = "B";
                    txtFutcompdr.Text = "";
                }

                txtFlagStrtdate.Text = "B";
                txtStrtdater.Text = txtStrtdate.Text;
            }

            if (txtStrtdate.Text != "")
            {
                if (!ValidateDate(txtStrtdate.Text))
                {
                    MessageBox.Show("Not a valid Start Date.");
                    return false;
                }

                DateTime strt = DateTime.ParseExact(txtStrtdate.Text, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime sel = DateTime.ParseExact(mast.Seldate, "yyyyMM", CultureInfo.InvariantCulture);

                if (GeneralFunctions.GetNumberMonths(strt, sel) > 24)
                {
                    MessageBox.Show("Start Date cannot be earlier than 24 months before Selection Date.");
                    return false;
                }

                //Check report data in monthly vip, ask question, set to blank
                var rlist = from v in mvs.monthlyViplist where v.Vipflag == "R" && Convert.ToInt32(v.Date6) < Convert.ToInt32(txtStrtdate.Text) select v;
                if (rlist.ToList().Count > 0)
                {
                    DialogResult result1 = MessageBox.Show("Reported Vip data Prior to Start Date, Erase?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result1 == DialogResult.Yes)
                    {
                        foreach (var value in mvs.monthlyViplist)
                        {
                            if (Convert.ToInt32(value.Date6) < Convert.ToInt32(txtStrtdate.Text) )
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
                        int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                        txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                        if (mvs.Withallvips)
                        {
                            txtComp.Text = mvs.GetCumPercent(it5c).ToString();
                            txtSumMnths.Text = mvs.GetSumMons().ToString();
                            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                            txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                            txtCumvip2.Text = txtCumvip.Text;
                            txtCumvipr2.Text = txtCumvipr.Text;
                        }
                        else
                        {
                            foreach (var value in mvsall.monthlyViplist)
                            {
                                if (Convert.ToInt32(value.Date6) < Convert.ToInt32(txtStrtdate.Text))
                                {
                                    if (value.Vipdata > 0 || value.Vipflag != "B")

                                    value.Vipdata = 0;
                                    value.Vipflag = "B";
                                    value.Vipdatar = 0;
                                    
                                }
                            }
                            
                            txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                            txtComp.Text = mvsall.GetCumPercent(it5c).ToString();
                            txtSumMnths.Text = mvsall.GetSumMons().ToString();
                            txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                            txtCompr.Text = mvsall.GetCumPercentr(it5c).ToString();
                        }
                       
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    //Check impute data, set to blank
                    var ilist = from v in mvs.monthlyViplist where v.Vipflag != "R" && Convert.ToInt32(v.Date6) < Convert.ToInt32(txtStrtdate.Text) select v;
                    if (ilist.ToList().Count > 0)
                    {
                        foreach (var value in mvs.monthlyViplist)
                        {
                            if (Convert.ToInt32(value.Date6) < Convert.ToInt32(txtStrtdate.Text))
                            {
                                if ((value.Vipdata > 0 || value.Vipflag != "B" || value.vs == "m"))
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
                        txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                        int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));

                        if (mvs.Withallvips)
                        {
                            txtComp.Text = mvs.GetCumPercent(it5c).ToString();
                            txtSumMnths.Text = mvs.GetSumMons().ToString();
                            txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                            txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                            txtCumvip2.Text = txtCumvip.Text;
                            txtCumvipr2.Text = txtCumvipr.Text;
                        }
                        else
                        {
                            foreach (var value in mvsall.monthlyViplist)
                            {
                                if (Convert.ToInt32(value.Date6) < Convert.ToInt32(txtStrtdate.Text))
                                {
                                    if (value.Vipdata > 0 || value.Vipflag != "B")

                                        value.Vipdata = 0;
                                        value.Vipflag = "B";
                                        value.Vipdatar = 0;
                                }
                            }

                            txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                            txtComp.Text = mvsall.GetCumPercent(it5c).ToString();
                            txtSumMnths.Text = mvsall.GetSumMons().ToString();
                            txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                            txtCompr.Text = mvsall.GetCumPercentr(it5c).ToString();
                        }
                    }

                }

                //Update status
                if (Convert.ToInt32(txtStrtdate.Text) >= Convert.ToInt32(currYearMon) && cbStatus.SelectedValue.ToString() != "4")
                {
                    //check status combo, if there is '4'
                    if (cbStatus.Items.Count == 7)
                    {
                        //rebuild Status combo
                        SetupStatusCombo();
                    }
                    cbStatus.SelectedValue = 4;
                    if (txtFlag5a.Text != "R")
                    {
                        txtItem5a.Text = "0";
                        txtItem5ar.Text = "0";
                        txtItem5b.Text = "0";
                        txtItem5br.Text = "0";
                        txtRvitm5c.Text = "0";
                        txtRvitm5cr.Text = "0";
                        txtFlag5a.Text = "B";
                        txtFlag5b.Text = "B";
                        txtFlagr5c.Text = "B";
                    }
                }
                else if (Convert.ToInt32(txtStrtdate.Text) < Convert.ToInt32(currYearMon) && cbStatus.SelectedValue.ToString() == "4")
                {
                    cbStatus.SelectedValue = 1;
                }

                //Set default flag
                SetDefaultFlag(txtStrtdate, txtFlagStrtdate);
                if (txtFlagStrtdate.Text == "R")
                    txtStrtdater.Text = txtStrtdate.Text;

                txtStrtdate.Modified = false;
            }
            return true;
        }

        private void txtCompdate_Enter(object sender, EventArgs e)
        {
            old_text = txtCompdate.Text;
        }
        private void txtCompdate_Validating(object sender, CancelEventArgs e)
        {
            if (txtCompdate.Text != "")
            {
                if (old_text != txtCompdate.Text)
                {
                    if (!CheckCompdateEnter())
                    {
                        txtCompdate.Text = old_text;
                        e.Cancel = true;
                        return;
                    }
                    else
                        old_text = txtCompdate.Text;
                }

            }
        }

        private void txtCompdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtCompdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtCompdate.Text != old_text)
                    {
                        if (!CheckCompdateEnter())
                        {
                            txtCompdate.Text = old_text;
                            return;
                        }
                        else
                            old_text = txtCompdate.Text;
                    }
                }
            }
        }

        //Verify entered Compdate
        private bool CheckCompdateEnter()
        {
            if (txtCompdate.Text == "")
            {
                txtCompdater.Text = "";
                txtFlagCompdate.Text = "B";
                return true;
            }

            if (!ValidateDate(txtCompdate.Text))
            {
                MessageBox.Show("Completion Date is invalid.");
                return false;

            }
            
            if (cbStatus.SelectedValue.ToString() == "2" || cbStatus.SelectedValue.ToString() == "8")
            {
                MessageBox.Show("Completion Date cannot be entered for PNR or Refusal.");
                return false;

            }
            if (txtStrtdate.Text == "")
            {
                MessageBox.Show("Completion Date cannot be entered if no started.");
                return false;
            }
            if (txtStrtdate.Text != "" && Convert.ToInt32(txtStrtdate.Text) > Convert.ToInt32(txtCompdate.Text))
            {
                MessageBox.Show("Completion Date cannot be less than Start Date.");
                return false;
            }
            if (Convert.ToInt32(currYearMon) <= Convert.ToInt32(txtCompdate.Text))
            {
                MessageBox.Show("Completion Date cannot be a future date.");
                return false;
            }
            if (mvs.monthlyViplist.Count == 0)
            {
                MessageBox.Show("Completion Date with no VIP data.");
                return false;
            }
            if (mvs.GetMonthVip(txtCompdate.Text).Vipdata == 0 && mvs.GetMonthVip(txtCompdate.Text).Vipflag == "B")
            {
                MessageBox.Show("No VIP data entered for completion month.");
                return false;
            }

            //Check report data in monthly vip, ask question, set to blank
            var rlist = from v in mvs.monthlyViplist where (v.Vipflag == "R" || v.Vipflag == "A") && Convert.ToInt32(v.Date6) > Convert.ToInt32(txtCompdate.Text) select v;
            if (rlist.ToList().Count > 0)
            {
                DialogResult result1 = MessageBox.Show("Reported Vip data after Completion Date, Erase?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result1 == DialogResult.Yes)
                {
                    foreach (var value in mvs.monthlyViplist)
                    {
                        if (Convert.ToInt32(value.Date6) > Convert.ToInt32(txtCompdate.Text))
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
                    txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                    int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));

                    if (mvs.Withallvips)
                    {
                        txtComp.Text = mvs.GetCumPercent(it5c).ToString();
                        txtSumMnths.Text = mvs.GetSumMons().ToString();
                        txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                        txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                        txtCumvip2.Text = txtCumvip.Text;
                        txtCumvipr2.Text = txtCumvipr.Text;
                    }
                    else
                    {
                        foreach (var value in mvsall.monthlyViplist)
                        {
                            if (Convert.ToInt32(value.Date6) > Convert.ToInt32(txtCompdate.Text))
                            {
                                if (value.Vipdata > 0 || value.Vipflag != "B")
                                {
                                    value.Vipdata = 0;
                                    value.Vipflag = "B";
                                    value.Vipdatar = 0;
                                }
                            }
                        }

                        txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                        txtComp.Text = mvsall.GetCumPercent(it5c).ToString();
                        txtSumMnths.Text = mvsall.GetSumMons().ToString();
                        txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                        txtCompr.Text = mvsall.GetCumPercentr(it5c).ToString();
                    }

                }
                else
                {
                    txtCompdate.Text = old_text;
                    return false;
                }
            }
            else
            {
                //Check impute data, set to blank
                var ilist = from v in mvs.monthlyViplist where v.Vipflag == "M" && Convert.ToInt32(v.Date6) > Convert.ToInt32(txtCompdate.Text) select v;
                if (ilist.ToList().Count > 0)
                {
                    foreach (var value in mvs.monthlyViplist)
                    {
                        if (Convert.ToInt32(value.Date6) > Convert.ToInt32(txtCompdate.Text))
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
                    txtCumvip.Text = mvs.GetCumvip() > 0 ? mvs.GetCumvip().ToString("#,#") : mvs.GetCumvip().ToString();
                    int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                    txtComp.Text = mvs.GetCumPercent(it5c).ToString();

                    if (mvs.Withallvips)
                    {
                        txtComp.Text = mvs.GetCumPercent(it5c).ToString();
                        txtSumMnths.Text = mvs.GetSumMons().ToString();
                        txtCumvipr.Text = mvs.GetCumvipr() > 0 ? mvs.GetCumvipr().ToString("#,#") : mvs.GetCumvipr().ToString();
                        txtCompr.Text = mvs.GetCumPercentr(it5c).ToString();
                        txtCumvip2.Text = txtCumvip.Text;
                        txtCumvipr2.Text = txtCumvipr.Text;
                    }
                    else
                    {
                        foreach (var value in mvsall.monthlyViplist)
                        {
                            if (Convert.ToInt32(value.Date6) > Convert.ToInt32(txtCompdate.Text))
                            {
                                if (value.Vipdata > 0 || value.Vipflag != "B")
                                {
                                    value.Vipdata = 0;
                                    value.Vipflag = "B";
                                    value.Vipdatar = 0;
                                }
                            }
                        }

                        txtCumvip2.Text = mvsall.GetCumvip() > 0 ? mvsall.GetCumvip().ToString("#,#") : mvsall.GetCumvip().ToString();
                        txtComp.Text = mvsall.GetCumPercent(it5c).ToString();
                        txtSumMnths.Text = mvsall.GetSumMons().ToString();
                        txtCumvipr2.Text = mvsall.GetCumvipr() > 0 ? mvsall.GetCumvipr().ToString("#,#") : mvsall.GetCumvipr().ToString();
                        txtCompr.Text = mvsall.GetCumPercentr(it5c).ToString();
                    }
                }

            }

            SetDefaultFlag(txtCompdate, txtFlagCompdate);
            if (txtFlagCompdate.Text == "R")
                txtCompdater.Text = txtCompdate.Text;

            txtCompdate.Modified = false;

            return true;
        }

        private void txtFutcompd_Enter(object sender, EventArgs e)
        {
            old_text = txtFutcompd.Text;
        }

        private void txtFutcompd_Validating(object sender, CancelEventArgs e)
        {
            if (old_text != txtFutcompd.Text)
            {
                if (!CheckFutcompdEnter())
                {
                    txtFutcompd.Text = old_text;
                    e.Cancel = true;
                }
                else
                    old_text = txtFutcompd.Text;
            }
        }

        private void txtFutcompd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtFutcompd_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit && old_text != txtFutcompd.Text)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (!CheckFutcompdEnter())
                    {
                        txtFutcompd.Text = old_text;
                        return;
                    }
                    else
                        old_text = txtFutcompd.Text;

                }
            }
        }

        //Verify Futcompd textbox change
        private bool CheckFutcompdEnter()
        {
            if (txtFutcompd.Text == "")
            {
                txtFlagFutcompd.Text = "B";
                txtFutcompdr.Text = "";
                return true;
            }

            if (!ValidateDate(txtFutcompd.Text))
            {
                MessageBox.Show("Projected Completion Date is invalid.");
                return false;
            }
            else if (txtStrtdate.Text == "")
            {
                MessageBox.Show("Projected Completion Date cannot be entered if no started.");
                return false;
            }
            else if (txtFutcompd.Text != "" && txtStrtdate.Text != "" && Convert.ToInt32(txtFutcompd.Text) < Convert.ToInt32(txtStrtdate.Text))
            {
                MessageBox.Show("Projected completion date cannot be less than Start Date.");
                return false;
            }
       
            SetDefaultFlag(txtFutcompd, txtFlagFutcompd);
            if (txtFlagFutcompd.Text == "R")
                txtFutcompdr.Text = txtFutcompd.Text;

            txtFutcompd.Modified = false;

            return true;
        }

        //tab control index change event
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (dgPcomments.Rows.Count > 0)
                {
                    dgPcomments.Columns[0].Width = 75;
                    dgPcomments.Columns[1].Width = 60;
                    dgPcomments.ColumnHeadersVisible = false;
                }
            }
            else if (tabControl1.SelectedIndex == 1)
            {
                if (dgRcomments.Rows.Count > 0)
                {
                    dgRcomments.Columns[0].Width = 75;
                    dgRcomments.Columns[1].Width = 60;
                    dgRcomments.ColumnHeadersVisible = false;
                }
            }
            
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadForm(true);
            DisplayForm();
            txtDflag.Text = "";
            default_flag = "";
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            frmHistory fHistory = new frmHistory();

            fHistory.Id = txtId.Text;
            if (txtRespid.Text != "")
                fHistory.Respid = txtRespid.Text;
            else
                fHistory.Respid = txtId.Text;
            fHistory.Resporg = resp.Resporg;
            fHistory.Respname = txtContact.Text;

            fHistory.ShowDialog();  //show history screen

            pdata = hd.GetProjCommentTable(Id, true);
            rdata = hd.GetRespCommentTable(samp.Respid, true);

            //set up grid
            if (pdata.Rows.Count != 0)
            {
                dgPcomments.DataSource = pdata;
                if (tabControl1.SelectedIndex == 0)
                {
                    dgPcomments.Columns[0].Width = 75;
                    dgPcomments.Columns[1].Width = 60;
                }
            }
            else
            {
                dgPcomments.DataSource = null;
            }

            /*Respondent comment list */
            if (rdata.Rows.Count != 0)
            {
                dgRcomments.DataSource = rdata;
                if (tabControl1.SelectedIndex == 1)
                {
                    dgRcomments.Columns[0].Width = 75;
                    dgRcomments.Columns[1].Width = 60;
                }
            }
            else
            {
                dgRcomments.DataSource = null;
            }
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            frmMarkCasesPopup popup = new frmMarkCasesPopup();
            popup.Id = Id;
            popup.Respid = samp.Respid;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();      

            if (CheckMarkExists())
                lblMark.Text = "MARKED";
            else 
                lblMark.Text = "";
        }

        //check mark notes exists or not
        private bool CheckMarkExists()
        {
            bool mark_exist = false;
            DataTable dtpMark = pmarkdata.GetProjMarks(Id);
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

        private void btnName_Click(object sender, EventArgs e)
        {
            if (editable && (EditMode == TypeEditMode.Edit))
            {
                if (!ValidateFormData())
                    return;
                if (CheckFormChanged())
                {
                    //DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result2 == DialogResult.Yes)
                    //{
                        if (!SaveData())
                            return;
                        if (data_saved || referral_updated)
                            SaveSchedcall();
                        else
                        {
                            flagdata = new CprflagsData();
                            Dataflags flags = flagdata.GetCprflagsData(Id);
                            cflags = new Cprflags(flags, mast.Owner, "edit");
                            if (reject)
                                lblReject.Visible = true;
                            else
                                lblReject.Visible = false;

                            HighlightFieldsFromFlags();
                            SetupDataFlag();
                        }

                    //}

                }

                /*unlock respondent */
                bool locked = GeneralDataFuctions.UpdateRespIDLock(samp.Respid, "");
            }

            if (data_saved)
                csda.AddCsdaccessData(Id, "UPDATE");
            else
                csda.AddCsdaccessData(Id, "BROWSE");


            this.Hide();   // hide parent

            frmName fm = new frmName();
            fm.Id = Id;

            fm.Idlist = Idlist;
            fm.CurrIndex = CurrIndex;
            fm.CallingForm = this;

            fm.ShowDialog();  // show child

            if (editable && this.Visible)
            {
                //load new respid
                LoadForm();
                DisplayForm();
            }
        }


        //combo survey selected index event
        private void cbSurvey_SelectedIndexChanged(object sender, EventArgs e)
        {
            //set up cap expense field if the survey is N, T, E, G, R, O and W
            if (cbSurvey.Text == "N" || cbSurvey.Text == "T" || cbSurvey.Text == "E" || cbSurvey.Text == "G" || cbSurvey.Text == "R" || cbSurvey.Text == "O" || cbSurvey.Text == "W")
                SetupCapexp(true);
            else
                SetupCapexp(false);

        }

        //buton clear
        private void BtnClear_Click(object sender, EventArgs e)
        {
            if (default_flag == "")
            {
                MessageBox.Show("There is no default flag to delete.");
                return;
            }
            else
            {
                default_flag = "";
                txtDflag.Text = "";
            }
        }

        // Verify item5a, item5b and Rvitm5c
        private void VerifyMainData()
        {
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

            if (txtStrtdate.Text != "" && txtItem5a.Text != "0" && (txtFlag5a.Text == "R" || txtFlag5a.Text == "A" || txtFlag5a.Text == "O" || txtFlag5a.Text == "E") && (txtFlag5b.Text == "I" || txtFlag5b.Text == "M" || txtFlag5b.Text == "B"))
            {
                if (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B")
                {
                    txtRvitm5c.Text = txtItem5a.Text;
                    txtFlagr5c.Text = "E";
                    txtFlag5b.Text = "B";

                    //if it is mulitfamily
                    SetupCostput();
                    verify = "5C was made equal to 5A";
                }
            }
            else if (txtStrtdate.Text != "" && txtItem5a.Text != "0" && (txtFlag5a.Text == "R" || txtFlag5a.Text == "A" || txtFlag5a.Text == "O" || txtFlag5a.Text == "E") && txtItem5b.Text != "0" && (txtFlag5b.Text == "R" || txtFlag5b.Text == "A" || txtFlag5b.Text == "O" || txtFlag5b.Text == "E"))
            {
                if (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B")
                {
                    txtRvitm5c.Text = (Convert.ToInt32(txtItem5a.Text.Replace(",", "")) + Convert.ToInt32(txtItem5b.Text.Replace(",", ""))).ToString();
                    txtFlagr5c.Text = "E";

                    //if it is mulitfamily
                    SetupCostput();
                    verify = "5C was made equal to 5A + 5B";

                }
            }
            else if (txtRvitm5c.Text != "0" && (txtFlagr5c.Text == "R" || txtFlagr5c.Text == "A" || txtFlagr5c.Text == "O" || txtFlagr5c.Text == "E") && (txtFlag5b.Text == "I" || txtFlag5b.Text == "M" || txtFlag5b.Text == "B"))
            {
                if (txtFlag5a.Text == "I" || txtFlag5a.Text == "M" || txtFlag5a.Text == "B")
                {
                    txtItem5a.Text = txtRvitm5c.Text;
                    txtFlag5a.Text = "F";

                    //set up reported field
                    txtItem5ar.Text = txtItem5a.Text;
                    
                    txtItem5b.Text = "0";
                    txtFlag5b.Text = "B";

                    verify = "5A was made equal to 5C";
                }

            }

            else if (txtCompdate.Text != "" && (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B") && (sumvip > 0 && (notInbf.Count == 0) &&
                 (sumvip >= 0.95 * mast.Projselv && sumvip <= 1.05 * mast.Projselv)))
            {
                txtRvitm5c.Text = sumvip.ToString();
                txtFlagr5c.Text = "F";

                txtItem5a.Text = txtRvitm5c.Text;
                txtFlag5a.Text = "F";

                //if it is mulitfamily
                SetupCostput();

                verify = "5C, 5A was made equal to VIPSUM";
            }
            else if (txtCompdate.Text != "" && (txtFlagr5c.Text == "I" || txtFlagr5c.Text == "M" || txtFlagr5c.Text == "B") && (sumvip > 0 && (mvss.ToList().Count() > 0) &&
                (sumvip >= 0.95 * mast.Projselv && sumvip <= 1.05 * mast.Projselv)))
            {
                txtRvitm5c.Text = sumvip.ToString();
                txtFlagr5c.Text = "F";

                txtItem5a.Text = txtRvitm5c.Text;
                txtFlag5a.Text = "F";

                //if it is mulitfamily
                SetupCostput();

                verify = "5C, 5A was made equal to VIPSUM";
            }

            // If Compdate is not blank and Cumvip > Rvitm5c and Reported VIP for Completion month = 0 , set COMPDATE to last month with Reported VIP > 0, and remove the Reported VIP of 0 that was entered
            if ((txtCompdate.Text != "") && (txtFlagCompdate.Text == "R") && (sumvip >= int.Parse(txtRvitm5c.Text.Replace(",", ""))))
            {
                MonthlyVip lastvip = mvs.monthlyViplist.Find(x => x.Date6 == txtCompdate.Text);
                if (lastvip.Vipdata == 0 && lastvip.Vipflag == "R")
                {
                    //set compdate to previous month
                    int i = 0;
                    for ( i = 0; i < mvs.monthlyViplist.Count; i++) // Loop through List with for
                    {
                        if (mvs.monthlyViplist[i].Vipdata > 0 && (mvs.monthlyViplist[i].Vipflag == "R" || mvs.monthlyViplist[i].Vipflag == "A" || mvs.monthlyViplist[i].Vipflag == "I"))
                        {
                            txtCompdate.Text = mvs.monthlyViplist[i].Date6;
                            txtCompdater.Text = mvs.monthlyViplist[i].Date6;
                            break;
                        }
                    }
                    for (int j = i-1; j>=0; j--) // Loop through List with for
                    {
                        if (mvs.monthlyViplist[j].Vipdata == 0 && mvs.monthlyViplist[j].Vipflag == "R")
                        {
                            mvs.monthlyViplist[j].Vipflag = "B";
                            mvs.monthlyViplist[j].vs = "m";
                        }
                    }
                    dgVip.Refresh();
                    txtSumMnths.Text = mvs.GetSumMons().ToString();
               }
            }

            if (verify != "")
                MessageBox.Show("Verify: " + verify);

        }

        //flag of data grid cell content click event
        private void dgFlags_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            data_flag_changed = true;
        }

        //Highlight textbox and gridfields, set notification messages
        private void HighlightFieldsFromFlags()
        {
            List<string> messagelist = new List<string>();
            string process_date = DateTime.Now.AddMonths(-1).ToString("yyyyMM");

            //clean highlighed textbox and data grid
            UnHighlightTextBox(txtRvitm5c);
            UnHighlightTextBox(txtSumMnths);
            UnHighlightTextBox(txtItem5a);
            UnHighlightTextBox(txtItem5b);
            UnHighlightTextBox(txtItem6);
            UnHighlightTextBox(txtCapexp);
            UnHighlightTextBox(txtCompdate);
            UnHighlightTextBox(txtStrtdate);
            UnHighlightTextBox(txtFutcompd);
            UnHighlightDataGrid();

            bool flag4_set = false;

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
                        HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 3)
                    {
                        if (samp.Strtdate != "")
                        {
                            if (Convert.ToInt32(samp.Strtdate) < Convert.ToInt32(process_date))
                            {
                                if (samp.Compdate != "")
                                {
                                    List<MonthlyVip> mvlist = mvs.GetMonthVipList(samp.Strtdate, samp.Compdate);
                                    MonthlyVip mv = null;
                                    foreach (var v in mvlist)
                                    {
                                        if (mv != null)
                                        {
                                            if (v.Vipflag == "M" && mv.Vipflag != "M")
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
                                            if (mv.Vipflag != "M" && mv.Vipflag != "B" && v.Vipflag == "M")
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
                        HighlightDataGrid(samp.Strtdate);
                    }
                    else if (value.flagno == 5)
                    {
                        value.title = "REJECT: VIP of 0 in the completion month";
                        HighlightDataGrid(samp.Compdate);
                    }
                    else if (value.flagno == 6)
                    {
                        value.title = "REJECT: Unmatched Projected Dates";
                        HighlightTextBox(txtSumMnths);
                    }
                    else if (value.flagno == 7)
                    {
                        value.title = "REJECT: RVITM5C reported 0";
                        HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 8)
                    {
                        value.title = "REJECT: Project started prior to 24 months ago, cannot enter data";
                        HighlightDataGrid(samp.Strtdate);
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
                        HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 11)
                    {
                        value.title = "REJECT: Item5b without Item5a";
                        HighlightTextBox(txtItem5a);
                    }
                    else if (value.flagno == 12)
                    {
                        value.title = "FLAG: Early Start Date";
                       // HighlightTextBox(txtStrtdate);
                    }
                    else if (value.flagno == 13)
                    {
                       value.title = "FLAG: RVITM5C >= 3 X SELVAL";
                       // HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 14)
                    {
                        value.title = "FLAG: Selection value >= 3 X RVITM5C";
                        //HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 15)
                    {
                        value.title = "FLAG: Cumulative VIP = " + txtComp.Text + "% of RVITM5C";
                      //  HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 16)
                    {
                        var mv25 = from mp in mvs.monthlyViplist where mp.Pct5c >= 25 select mp;
                        if (mv25.ToList().Count > 0)
                        {
                            MonthlyVip m = mv25.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5c + "% of RVITM5C";
                          //  HighlightDataGrid(m.Date6);
                        }
                        
                    }
                    else if (value.flagno == 17)
                    {
                        var mv50 = from mp in mvs.monthlyViplist where mp.Pct5c >= 50 select mp;
                        if (mv50.ToList().Count > 0)
                        {
                            MonthlyVip m = mv50.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5c + "% of RVITM5C";
                        //    HighlightDataGrid(m.Date6);
                        }
                    }
                    else if (value.flagno == 18)
                    {
                        var mv75 = from mp in mvs.monthlyViplist where mp.Pct5c >= 75 select mp;
                        if (mv75.ToList().Count > 0)
                        {
                            MonthlyVip m = mv75.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5c + "% of RVITM5C";
                       //     HighlightDataGrid(m.Date6);
                        }
                    }
                    else if (value.flagno == 19)
                    {
                        value.title = "FLAG: Outlier: Old wt = " + old_fwgt + " New wt = " + txtFwgt.Text;
                    //    HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 20)
                    {
                        value.title = "FLAG: Item6 >= 50% of RVITM5C";
                  //      HighlightTextBox(txtItem6);
                    }
                    else if (value.flagno == 21)
                    {
                        value.title = "FLAG: Manufacturing project with no capital expenditures";
                  //      HighlightTextBox(txtCapexp);
                    }
                    else if (value.flagno == 22)
                    {
                        value.title = "FLAG: RVITM5C + ITEM6 = CAPEXP";
                   //     HighlightTextBox(txtCapexp);
                    }
                    else if (value.flagno == 23)
                    {
                        value.title = "FLAG: CAPEXP > RVITM5C";
                    //    HighlightTextBox(txtCapexp);
                    }
                    else if (value.flagno == 24)
                    {
                        value.title = "FLAG: Imputed VIP over RVITM5C";
                   //     HighlightTextBox(txtCumvip);
                    }
                    else if (value.flagno == 25)
                    {
                        string process_date3 = DateTime.Now.AddMonths(-4).ToString("yyyyMM");
                        var last3 = from mp in mvs.GetMonthVipList(process_date3, process_date) where (mp.Vipdata == 0) select mp;
                        MonthlyVip m = last3.ToList().First();
                        value.title = "FLAG: VIP of 0 in the last 3 months";
                 //       HighlightDataGrid(m.Date6);
                    }
                    else if (value.flagno == 26)
                    {
                        value.title = "FLAG: Completed before selected";
                 //       HighlightTextBox(txtCompdate);
                    }
                    else if (value.flagno == 27)
                    {
                        value.title = "FLAG: Completed W/CUM VIP < 95% of RVITM5C";
                //        HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 28)
                    {
                        value.title = "FLAG: No start date, projected completion date or Rvitm5C";
                //        if (samp.Strtdate != "" && samp.Futcompd != "" && samp.Flagr5c != "B")
               //             HighlightTextBox(txtRvitm5c);
                //        else if (samp.Strtdate != "" && samp.Futcompd == "" && samp.Rvitm5c > 0)
               //             HighlightTextBox(txtFutcompd);
               //         else
               //             HighlightTextBox(txtStrtdate);
                    }
                    else if (value.flagno == 29)
                    {
                        value.title = "FLAG: RVITM5C is 900% of previous reported value";
               //         HighlightTextBox(txtRvitm5c);
                    }
                    else if (value.flagno == 30)
                    {
                        value.title = "FLAG: Item6 is 900% of previous reported value";
              //          HighlightTextBox(txtItem6);
                    }
                    else if (value.flagno == 31)
                    {
                        value.title = "FLAG: Capex is 900% of previous reported value";
               //         HighlightTextBox(txtCapexp);
                    }
                    else if (value.flagno == 32)
                    {
                        value.title = "FLAG: VIP is 900% of previous reported value";
                        //string date66 = string.Empty;
                        //DataTable d32 = auditData.GetVIPAuditDataForID(samp.Id);
                        //foreach (DataRow row in d32.Rows)
                        //{
                        //    int Oldvip = Convert.ToInt32(row["OLDVIP"]);
                        //    int Newvip = Convert.ToInt32(row["NEWVIP"]);
                        //    if (Math.Abs((Oldvip - Newvip) / Oldvip) >= 9)
                        //    {
                        //        date66 = row["DATE6"].ToString();
                        //        break;
                        //    }
                        //}
//
               //         if (date66 != string.Empty)
             //               HighlightDataGrid(date66);
                    }
                    else if (value.flagno == 33)
                    {
                        value.title = "FLAG: CUMVIP with no RVITM5C";
            
                    }
                    else if (value.flagno == 34)
                    {
                        value.title = "FLAG: Projected Completion date does not align with percent complete";
           
                    }
                    else if (value.flagno == 35)
                    {
                        //value.title = "FLAG: Item5A";
                        //HighlightTextBox(txtItem5a);
                    }
                    else if (value.flagno == 36)
                    {
                        value.title = "FLAG: Within Survey Ownership Change";
                    }
                    else if (value.flagno == 37)
                    {
                        value.title = "REJECT: Reported VIP with no start date";
                    }
                    else if (value.flagno == 38)
                    {
                        value.title = "FLAG: Centurion Comments Field Contains Data";
                    }
                    else if (value.flagno == 39)
                    {
                        value.title = "FLAG: Outside of Range " + txtCostpu.Text.Replace(",", "") + ", Check RVITM5C and Units";
                    }
                    else if (value.flagno == 40)
                    {
                        value.title = "FLAG: Cumulative VIP = " + txtComp.Text + "% of RVITM5C, Revise RVITM5C";
       
                    }
                    else if (value.flagno == 41)
                    {
                        var mv20 = from mp in mvs.monthlyViplist where mp.Pct5c >= 20 select mp;
                        if (mv20.ToList().Count > 0)
                        {
                            MonthlyVip m = mv20.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5c + "% of RVITM5C";
        //                    HighlightDataGrid(m.Date6);
                        }
                    }
                    else if (value.flagno == 42)
                    {
                        var mv50 = from mp in mvs.monthlyViplist where mp.Pct5c >= 50 select mp;
                        if (mv50.ToList().Count > 0)
                        {
                            MonthlyVip m = mv50.ToList().First();
                            value.title = "FLAG: VIP = " + m.Pct5c + "% of RVITM5C";
            
                        }
                    }
                    else if (value.flagno == 43)
                    {
                        var mv75 = from mp in mvs.monthlyViplist where mp.Pct5c >= 75 select mp;
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
                tb.BackColor = System.Drawing.SystemColors.InactiveBorder;
            tb.ForeColor = Color.Black;
        }

        //Highlight a row vipdata
        private void HighlightDataGrid(string date6)
        {
            foreach (DataGridViewRow row in dgVip.Rows)
            {
                if (row.Cells[0].Value.ToString() == date6)
                {
                    row.Cells[4].Style.BackColor = Color.Red;
                    row.Cells[4].Style.ForeColor = Color.White;
                    break;
                }
            }
        }

        //Clear highlight in data grid
        private void UnHighlightDataGrid()
        {
            foreach (DataGridViewRow row in dgVip.Rows)
            {
                row.Cells[4].Style.BackColor = Color.White;
                row.Cells[4].Style.ForeColor = Color.Black;

            }
        }

        //disable bypass check box
        private void dgFlags_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.ColumnIndex == this.dgFlags.Columns["bypass"].Index && e.RowIndex > -1 && e.RowIndex != this.dgFlags.NewRowIndex)
            {
                int fno = Convert.ToInt32(this.dgFlags[0, e.RowIndex].Value);

                //disable check box if reject
                if (fno > 1 && fno < 12)
                {
                    e.PaintBackground(e.CellBounds, true);

                    Rectangle r = e.CellBounds;

                    r.Width = 13;

                    r.Height = 13;

                    r.X += e.CellBounds.Width / 2 - 7;

                    r.Y += e.CellBounds.Height / 2 - 7;

                    ControlPaint.DrawCheckBox(e.Graphics, r, ButtonState.Inactive);

                    e.Handled = true;
                }

            }

        }

        private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtNewtc_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter && old_text != txtNewtc.Text)
                {
                    if (!ValidateNewtc())
                    {
                        MessageBox.Show("The Newtc value entered is invalid.");
                        txtNewtc.Text = old_text;
                    }
                }
            }
        }

        private void txtNewtc_Enter(object sender, EventArgs e)
        {
            old_text = txtNewtc.Text;
        }

        private void txtNewtc_Leave(object sender, EventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit && old_text != txtNewtc.Text)
            {
                if (!ValidateNewtc())
                {
                    MessageBox.Show("The Newtc value entered is invalid.");
                    txtNewtc.Text = old_text;
                }
            }
        }

        private bool ValidateNewtc()
        {
            //check it validate newtc or not
            bool result;
            if (txtSurvey.Visible)
                result = GeneralDataFuctions.CheckNewTC(txtNewtc.Text, txtSurvey.Text);
            else
                result = GeneralDataFuctions.CheckNewTC(txtNewtc.Text, cbSurvey.Text);

            return result;
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            frmReferral fReferral = new frmReferral();
            
            fReferral.Id = txtId.Text;
            if (txtRespid.Text != "")
                fReferral.Respid = txtRespid.Text;
            else
                fReferral.Respid = txtId.Text;

            fReferral.ShowDialog();  //show child
            referral_updated = fReferral.IsChanged;

            refdata = new ReferralData();
            referral_exist = refdata.CheckReferralExist(samp.Id, samp.Respid);
            lblReferral.Visible = referral_exist;

        }

        private void btnAduit_Click(object sender, EventArgs e)
        {
            frmProjectAuditPopup fproj = new frmProjectAuditPopup(Id, samp.Respid, mast.Owner, mast.Newtc);

            fproj.ShowDialog();  //show child
        }

        private void btnSlip_Click(object sender, EventArgs e)
        {
            frmSlipDisplay fSD = new frmSlipDisplay();
            fSD.Id = txtId.Text;
            fSD.Dodgenum = mast.Dodgenum;
            fSD.Fin = mast.Fin;
            fSD.StartPosition = FormStartPosition.CenterParent;
            fSD.ShowDialog();  //show child 
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

            if ((old_text == "4" || old_text == "5" || old_text == "6") && (new_text != "5" && new_text != "6" && new_text != "4") && txtStrtdate.Text != "" && Convert.ToInt32(txtStrtdate.Text) >= Convert.ToInt32(currYearMon))
            {
                MessageBox.Show("Status change rejected, project has not started.");
                cbStatus.SelectedValue = Convert.ToInt32(old_text);
            }
            ////else if (old_text != "4" && old_text != "5" && new_text == "6" && txtStrtdate.Text != "" && Convert.ToInt32(txtStrtdate.Text) <= Convert.ToInt32(currYearMon))
            ////{
            ////    MessageBox.Show("Status change rejected, project has started.");
            ////    cbStatus.SelectedValue = Convert.ToInt32(old_text);
            ////}
            else if (new_text == "4" && txtStrtdate.Text == "")
            {
                MessageBox.Show("Status change rejected, Start Date is blank.");
                cbStatus.SelectedValue = Convert.ToInt32(old_text);
            }
            else if (new_text == "4" && txtStrtdate.Text != "" && (Convert.ToInt32(txtStrtdate.Text) < Convert.ToInt32(currYearMon)))
            {
                MessageBox.Show("Status Code change is inconsistent with Start Date.");
                cbStatus.SelectedValue = Convert.ToInt32(old_text);
            }

            //for multifamily, if the status is 5, show the parent popup screen to select parent id
            if (new_text == "5" && mast.Owner == "M")
            {
                frmParentIdPopup popup = new frmParentIdPopup(Id, mast.Masterid);
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog();  //show child

                //if the popup was cancelled, set status to old value
                if (popup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                {
                    cbStatus.SelectedValue = Convert.ToInt32(old_text);
                }
                else
                {
                    cbStatus.Enabled = false;
                }
                
            }
        }

        private void SetupStatusCombo()
        {
            DataTable dtblDataSource = new DataTable();
            dtblDataSource.Columns.Add("Text");
            dtblDataSource.Columns.Add("Value");

            dtblDataSource.Rows.Add("1-Active", 1);
            dtblDataSource.Rows.Add("2-PNR", 2);
            dtblDataSource.Rows.Add("3-DC PNR", 3);
            if (mvs.GetSumMons() == 0)
                dtblDataSource.Rows.Add("4-Abeyance", 4);
            dtblDataSource.Rows.Add("5-Duplicate", 5);
            dtblDataSource.Rows.Add("6-Out of Scope", 6);
            dtblDataSource.Rows.Add("7-DC Refusal", 7);
            dtblDataSource.Rows.Add("8-Refusal", 8);

            cbStatus.DataSource = null;
            cbStatus.DataSource = dtblDataSource;
            cbStatus.DisplayMember = "Text";
            cbStatus.ValueMember = "Value";
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

        private void txtUnits_TextChanged(object sender, EventArgs e)
        {
            if (!formloading)
            {
                int value = 0;
                if (txtUnits.Text != "")
                {
                    value = Convert.ToInt32(txtUnits.Text.Replace(",", ""));
                    txtUnits.Text = value > 0 ? value.ToString("N0") : value.ToString();
                    txtUnits.SelectionStart = txtUnits.Text.Length;
                }
                else
                    txtUnits.Text = "0";

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

        private void cbSurvey_Enter(object sender, EventArgs e)
        {
            //store old status
            old_text = cbSurvey.SelectedItem.ToString();
        }

        private void cbSurvey_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!ValidateNewtc())
            {
                MessageBox.Show("The Owner is not valid for this Newtc.");
                cbSurvey.SelectedItem = old_text;
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            SchedCallData scdata = new SchedCallData();
            DataTable dtsc = scdata.GetSchedHistDataByID(Id);
            if (dtsc.Rows.Count == 0)
            {
                MessageBox.Show("There are no Scheduler History records for this case");
                return;
            }

            frmSchedHistPopup popup = new frmSchedHistPopup();
            popup.Id = Id;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
        }

        private void txtPcityst_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

    }

}
