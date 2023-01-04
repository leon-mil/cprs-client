/**********************************************************************************
Econ App Name  : CPRS

Project Name   : CPRS Interactive Screens System

Program Name   : frmMfInitial.cs	    	

Programmer     : Cestine Gill

Creation Date  : 04/07/2016

Inputs         : Masteridnumlist                   

Parameters     : None
                  
Outputs        : Multi Family Name and Address Display and List 

Description    : This program displays the Name and Address data for a 
                 Presample case. Allows users to edit data on records 
                 that are not locked by another user.

Detailed Design: Multi Family Initial Address Detailed Design

Other:           Called from: Call Scheduling > Initials > Multi Family
                              frmMfInitialReview
 
Revision History: See Below	
***********************************************************************************
Modified Date : Jan. 13, 2017 
Modified By   : Christine Zhang 
Keyword       : 20170113CZ 
Change Request:  
Description   : Do not allow Bldgs or Units to be blank 
***********************************************************************************
Modified Date : Mar. 5, 2018 
Modified By   : Christine Zhang
Keyword       : 20180303CZ
Change Request:  362
Description   : add lock all cases with same psu and bpoid and allow presample search
***********************************************************************************
Modified Date : August 7, 2018 
Modified By   : Christine Zhang
Keyword       : 
Change Request: 
Description   : add cpraccess code INITIAL if the user is interviewer, othervise
                DATA_ENTRY
***********************************************************************************
Modified Date : November 18, 2020 
Modified By   : Diane Musachio  
Keyword       : DM20201118
Change Request: 
Description   : updated code to fix error when saving comment and exiting by top menu
***********************************************************************************
Modified Date : Jan. 25, 2021 
Modified By   : Christine Zhang  
Keyword       : CZ20210125
Change Request: 
Description   : updated code to fix error when saving comment and exiting by top menu
***********************************************************************************
Modified Date : May 24, 2021 
Modified By   : Christine Zhang  
Keyword       : CZ20210524
Change Request: CR8238
Description   : if grad 4 access and 1st review completed, invisible check box for check complete
***********************************************************************************
Modified Date :  07/22/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR 8385
 Description   : Add Pending check box
***********************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using CprsBLL;
using CprsDAL;
using System.Linq;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;


namespace Cprs
{
    public partial class frmMfInitial : frmCprsParent
    {

        /* Public */

        public frmMfInitial Current;
        public int CurrIndex = 0;
        public string Id;//Variable used when the form is called from the Review Screen
        public string EntryPoint;

        /*Optional*/
        public frmMfInitialReview CallingForm = null;
        public List<string> Idlist = null;
        public string Respid;

        //Variables used when the form is called from the Popup
        private string Psu;
        private string Place;
        private string Bpoid;
        private string Sched;
        private string id;
        private bool selflg;
        private bool compareflg;//Used when the form is loaded from the Compare screen Duplicate button

        //respondent audit hold variables
        private string oldRespOrg;
        private string newRespOrg;
        private string oldFactoff;
        private string newFactoff;
        private string oldOthrresp;
        private string newOthrresp;
        private string oldAddr1;
        private string newAddr1;
        private string oldAddr2;
        private string newAddr2;
        private string oldAddr3;
        private string newAddr3;
        private string oldZip;
        private string newZip;
        private string oldRespnote;
        private string newRespnote;
        private string oldRespname;
        private string newRespname;
        private string oldRespname2;
        private string newRespname2;
        private string oldExt;
        private string newExt;
        private string oldExt2;
        private string newExt2;
        private string oldEmail;
        private string newEmail;
        private string oldWeburl;
        private string newWeburl;
        private string oldPhone;
        private string newPhone;
        private string oldPhone2;
        private string newPhone2;
        private string oldFax;
        private string newFax;

        private string oldRespid;
        private string newRespid;
        private string oldStatcde;
        private string newStatcde;
        private string oldStrtdte;
        private string newStrtdte;
        private string oldRbldgs;
        private string newRbldgs;
        private string oldRunits;
        private string newRunits;
        private string oldCommtext;
        private string newCommtext;
        private string user = UserInfo.UserName;
        private string rstate;

        //Set the comment fields to empty so will not populate the
        //table unless a comment is entered

        private string usrnme;
        private string commentdate;
        private string commenttime;

        private string accesday = DateTime.Now.ToString("MMdd");
        private string accestms;
        private string accestme;

        private string fiveplus;

        Presample presample;
        RespAddr respaddress;

        private bool first;
        private bool done = true;

        //bool to indicate that the case is locked
        private bool flgIsLocked = false;

        private bool editable = true;
        private string presamplocked_by;
        private string resplocked_by;
        private bool notvalid;

        //add variables to keep the textbox contents
        private bool anytxtmodified = false;
        private string newval = string.Empty;
        private string oldval = string.Empty;

        private string access_code = string.Empty;

        TextBox txt;
        MaskedTextBox mtxt;
        ComboBox cbobox;

        MfInitialData mfidata = new MfInitialData();

        private bool IsSearchSample = true;

        //Used for audit*/
        private ProjectAuditData auditData;

        private List<Respaudit> Respauditlist = new List<Respaudit>();

        MfInitialSearchData mfisearchdata = new MfInitialSearchData();
        private MfInitialSearchData dataObject;

        //this triggers the form to load then display the lock message
        private delegate void ShowLockMessageDelegate();

        public frmMfInitial()
        {
            Current = this;
            InitializeComponent();
            first = true;
            tbContact.DrawMode = TabDrawMode.OwnerDrawFixed;
            anytxtmodified = false;
        }

        private void frmMfInital_Load(object sender, EventArgs e)
        {
            this.Show();
            
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.InitMF== "N")
            {
                MessageBox.Show("You do not have permission to access this area");
                this.Close();
                frmHome fH = new frmHome();
                fH.Show();
                return;
            }
            label55.Text = "CUI//SP-CENS" + "\n" + "DISCLOSURE PROHIBITED: TITLE 13 USC";

            RemoveTxtChanged();

            dataObject = new MfInitialSearchData();

            auditData = new ProjectAuditData();
            Respauditlist.Clear();

            /* If there is a list, set count boxes */

            if (Idlist != null)
            {
                txtCurrentrec.Text = (CurrIndex + 1).ToString();
                txtTotalrec.Text = Idlist.Count.ToString();
            }

            /* Setup Search Section */

            SetHiddenSearchLbl();
            ResetSearchParameters();
            cbSeldate.SelectedIndex = 0;

            btnAudit.Enabled = false;
            btnCompare.Enabled = false;

            dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
            FormatMfiData();

            lblRecordCount.Text = " ";

            //reassign the value from the database to a 
            //user-friendlier representation in the 
            //status code combobox

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

            cbStatCode.DataSource = null;
            cbStatCode.DataSource = dtblDataSource;
            cbStatCode.DisplayMember = "Text";
            cbStatCode.ValueMember = "Value";

            rdSample.Checked = true;

            chkpending.Visible = false;
            chkpending.Checked = false;

            access_code = "DATA ENTRY";
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
                access_code = "INITIAL";

            GeneralDataFuctions.AddCpraccessData(access_code, "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData(access_code);

            //if a EntryPoint value is present that means the form was accessed by the 
            //Review form and therefore displays the previous button to return
            //to that form upon click

            if (EntryPoint == "REV")
            {
                LoadFormFromReview();
            }
            else
            {
                LoadFormFromPopup();
            }

            anytxtmodified = false;
        }

        private void LoadFormFromReview()
        {
            id = Id;

           // frmMfInitialReview fMFInitReview = new frmMfInitialReview();

            chkComplete.Visible = false;
            gbListCount.Visible = true;
            btnPrevCase.Visible = true;
            btnNextCase.Visible = true;

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                btnAudit.Enabled = true;
            else
                btnAudit.Enabled = false;

            //Clear the txt_txtChange Event upon Form Initialization
            //remove handler before making a change

            RemoveTxtChanged();

            //The button will display previous and
            //return the user to the review screen

            btnNextInitial.Text = "PREVIOUS";
            first = false;
            resetfields();

            //Display the presample data

            DisplayPresampAddress();
            SetTxtChanged();
            compareflg = false;
        }

        private void LoadFormFromPopup()
        {

            //The button will display Next Initial and
            //allow the user to enter a new FIN

            btnNextInitial.Text = "NEXT INITIAL";
            btnPrevCase.Visible = false;
            btnNextCase.Visible = false;
            gbListCount.Visible = false;

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                btnAudit.Enabled = true;
            else
                btnAudit.Enabled = false;

            // Display the popup for the user to enter 
            //the FIN (PSU, PLACE, SCHED) when the form
            //is loaded

            frmMFInitPopup fMFInit = new frmMFInitPopup();
            fMFInit.ShowDialog();  // show child

            selflg = fMFInit.selflg;

            dupmaster =0;
            dupflag = "0";

            //Once the user clicks ok then load the form

            if (fMFInit.DialogResult == DialogResult.OK)
            {

                RemoveTxtChanged();

                // populate the value that is entered into the popup.
                // These values will be used the load the textboxes 
                // with presample data

                first = false;

                resetfields();

                Id = fMFInit.Id;
                id = Id;

                // Get data from PRESAMPLE table
                DisplayPresampAddress(selflg);
                SetTxtChanged();
                compareflg = false;

                
            }

            if ((fMFInit.DialogResult == DialogResult.Abort) && (first == true))
            {
                this.Close();
                frmHome fH = new frmHome();
                fH.Show();
            }
        }

        //Populate the PSamp_Hist table

        private void UpdateCaseAccessRecord()
        {
            //Time that user exited case
            accestme = DateTime.Now.ToString("HHmmss");

            mfidata.AddPsampHistRecs(id, Psu, Place, Bpoid, Sched, user, accesday, accestms, accestme);
        }

        private void GetLockedBy()
        {
            presamplocked_by = mfidata.GetPresampleLockedBy(id);
            resplocked_by = GeneralDataFuctions.ChkRespIDIsLocked(txtRespid.Text.Trim());

            // If locked show message
            BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
        }

        //function to get the state from addr3 for use in the 
        //GeneralDataFuctions.GetTimezone function

        private string GetState(string addr3)
        {
            string[] words = GeneralFunctions.SplitWords(addr3);
            int num_word = words.Count();
            string rst = string.Empty;

            if (words[num_word - 1] == "CANADA")
            {
                rst = words[num_word - 2];
                return rst;
            }
            else
            {
                rst = words[num_word - 1];
                return rst;
            }
        }

        //Display data from the PRESAMPLE table

        private void DisplayPresampAddress(bool callingfromRefresh= false)
        {
            anytxtmodified = false;

            presample = mfidata.GetPresample(id);

            string rst = GetState(presample.Addr3.Trim());

            string review_status = presample.Worked.Trim();

            txtZone.Text = GeneralDataFuctions.GetTimezone(rst);

            txtFin.Text = presample.Psu + " " + presample.Bpoid + " " + presample.Sched;

            Psu = presample.Psu;
            Bpoid = presample.Bpoid;
            Sched = presample.Sched;
            Place = presample.Place;

            txtSelDate.Text = presample.Seldate;
            txtFipst.Text = presample.Fipstater;
            cbStatCode.SelectedValue = presample.Status;
            txtFrCde.Text = presample.Frcde;
            cbNewtc.Text = presample.Newtc;
            txtRespid.Text = presample.Respid;
            txtBldgs.Text = presample.Bldgs;
            txtUnits.Text = presample.Units;
            txtRBldgs.Text = presample.Rbldgs;
            txtRUnits.Text = presample.Runits;
            txtStrtDate.Text = presample.Strtdate;
            txtProjDesc.Text = presample.ProjDesc;
            txtProjLoc.Text = presample.Projloc;
            txtPCitySt.Text = presample.PCitySt;
            txtRespOrg.Text = presample.Resporg;
            txtFactOff.Text = presample.Factoff;
            txtOthrResp.Text = presample.Othrresp;
            txtAddr1.Text = presample.Addr1.Trim();
            txtAddr2.Text = presample.Addr2.Trim();
            txtAddr3.Text = presample.Addr3.Trim();
            txtZip.Text = presample.Zip.Trim();
            txtRespnote.Text = presample.Respnote;
            txtRespname.Text = presample.Respname;
            txtPhone.Text = presample.Phone;
            txtExt.Text = presample.Ext.Trim();
            txtRespname2.Text = presample.Respname2;
            txtPhone2.Text = presample.Phone2;
            txtExt2.Text = presample.Ext2.Trim();
            txtFax.Text = presample.Fax.Trim();
            txtEmail.Text = presample.Email.Trim();
            txtWebUrl.Text = presample.Weburl;
            txtCommtext.Text = presample.Commtext;
            txtStrtDate.Text = presample.Strtdate;
            txtMasterid.Text = presample.Id;
            txtReview.Text = setStatus();

            dupmaster = presample.Dupmaster;
            dupflag = presample.Dupflag;

            //Time that case was entered
            accestms = DateTime.Now.ToString("HHmmss");

            //check for lock by someone
            if (!callingfromRefresh)
                GetLockedBy();

           // tbContact.DrawItem += new DrawItemEventHandler(tbContact_DrawItem);
            tbContact.Refresh();
            tbContact.SelectedTab = tbpPrimary;

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
            {
                btnAudit.Enabled = true;
                RespAddrData rad = new RespAddrData();
                respaddress = rad.GetRespAddr(txtRespid.Text);
            }
            else
            {
                btnAudit.Enabled = false;
            }

            if (presample.Worked == "2" || (presample.Worked == "1") && (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4"))
            {
                chkComplete.Visible = false;
                chkComplete.Checked = false;
                chkpending.Checked = false;
                chkpending.Visible = false;
            }
            else
            {
                chkComplete.Visible = true;
                chkComplete.Checked = false;
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                {
                    chkpending.Visible = true;
                    if (presample.Worked == "3")
                        chkpending.Checked = true;
                    else
                        chkpending.Checked = false;
                }
                
            }
            
        }

        // If the user enters a RESPID, then
        // replace the textbox values with data
        // from the RESPONDENT table

        private void DisplayRespAddress(bool check_lock=true)
        {

            newRespid = respaddress.Respid;

            string rst = GetState(respaddress.Addr3.Trim());

            txtZone.Text = GeneralDataFuctions.GetTimezone(rst);

            txtRespOrg.Text = respaddress.Resporg;
            txtFactOff.Text = respaddress.Factoff;
            txtOthrResp.Text = respaddress.Othrresp;
            txtAddr1.Text = respaddress.Addr1;
            txtAddr2.Text = respaddress.Addr2;
            txtAddr3.Text = respaddress.Addr3;
            txtZip.Text = respaddress.Zip;
            txtRespnote.Text = respaddress.Respnote;
            txtRespname.Text = respaddress.Respname;
            txtPhone.Text = respaddress.Phone;
            txtExt.Text = respaddress.Ext;
            txtRespname2.Text = respaddress.Respname2;
            txtPhone2.Text = respaddress.Phone2;
            txtExt2.Text = respaddress.Ext2;
            txtFax.Text = respaddress.Fax;
            txtEmail.Text = respaddress.Email;
            txtWebUrl.Text = respaddress.Web;

            txtReview.Text = setStatus();

            //check for lock by someone
            if (check_lock)
                GetLockedBy();

            tbContact.DrawItem += new DrawItemEventHandler(tbContact_DrawItem);
            tbContact.Refresh();
            tbContact.SelectedTab = tbpPrimary;

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                btnAudit.Enabled = true;
            else
                btnAudit.Enabled = false;

        }

        //Check if the Case is Locked by Another User and Display User Name that has Respid Locked

        private void ShowLockMessage()
        {
            string lck = "";

            if ((presamplocked_by != "" )|| (resplocked_by != "" ))
            {
                if (presamplocked_by != "")
                    lck = presamplocked_by;

                if (resplocked_by != "")
                    lck = resplocked_by;

                MessageBox.Show("This case is locked by " + lck + ", cannot be edited.");

                flgIsLocked = true;

                editable = false;
                lblLockedBy.Visible = true;

                //grey out refresh and restore buttons if locked
                btnRefresh.Enabled = false;
                btnRestore.Enabled = false;

                lockTextboxes();

            }
            else
            {
                editable = true;
                lblLockedBy.Visible = false;

                btnRefresh.Enabled = true;

                //Make the restore button visible only to 
                //NPC Managers and Grade 5 NPC Interviewers

                if ((UserInfo.GroupCode == EnumGroups.NPCManager ||
                     UserInfo.Grade == "5" || UserInfo.GroupCode == EnumGroups.NPCLead) && presample.Worked == "1")
                    btnRestore.Visible = true;
                else
                    btnRestore.Visible = false;

                flgIsLocked = false;

                unlockTextboxes();

                UpdatePresampleLock();

                if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    UpdateRespondentLock();
            }

        }

        private void UpdateRespondentLock()
        {
            resplocked_by = UserInfo.UserName;
            GeneralDataFuctions.UpdateRespIDLock(txtRespid.Text, resplocked_by);
        }

        private void UpdatePresampleLock()
        {
            //lock all ID's with the same PSU and BPOID
            presamplocked_by = UserInfo.UserName;
            mfidata.UpdatePresampLock(Psu, Bpoid, presamplocked_by);

        }

        private void relsPresampleLock()
        {
            //check the RESPLOCK field in the presample table
            presamplocked_by = mfidata.GetPresampleLockedBy(id);

            //Only unlock if you had locked it. Cannot unlock other user's lock.
            if (presamplocked_by == UserInfo.UserName)
            {
                string locked_by = "";
                mfidata.UpdatePresampLock(Psu,Bpoid, locked_by);
            }
        }

        private void relsRespondentLock()
        {
            //check the RESPLOCK field in the respondent table
            resplocked_by = GeneralDataFuctions.ChkRespIDIsLocked(txtRespid.Text);

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
            {
                //Only unlock if locked by user. Cannot unlock another user's lock.
                string locked_by = "";
                if (resplocked_by == UserInfo.UserName)
                {
                    GeneralDataFuctions.UpdateRespIDLock(txtRespid.Text, locked_by);
                }
            }
        }

        private string setStatus()
        {
            string status = string.Empty;

            switch (presample.Worked)
            {
                case "0": status = "NOT STARTED"; break;
                case "1": status = "REVIEW"; break;
                case "2": status = "FINISHED"; break;
                case "3": status = "PENDING"; break;
            }
            return status;
        }

        private void resetfields()
        {
            txtFin.Text = "";
            txtSelDate.Text = "";
            txtFipst.Text = "";
            cbStatCode.Text = "";
            txtFrCde.Text = "";
            cbNewtc.Text = "";
            txtBldgs.Text = "";
            txtUnits.Text = "";
            txtRBldgs.Text = "";
            txtRUnits.Text = "";
            txtStrtDate.Text = "";
            txtProjDesc.Text = "";
            txtProjLoc.Text = "";
            txtPCitySt.Text = "";
            txtRespOrg.Text = "";
            txtFactOff.Text = "";
            txtOthrResp.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtAddr3.Text = "";
            txtZip.Text = "";
            txtRespnote.Text = "";
            txtRespname.Text = "";
            txtRespname2.Text = "";
            txtPhone.Text = "";
            txtPhone2.Text = "";
            txtExt.Text = "";
            txtExt2.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtWebUrl.Text = "";
            txtCommtext.Text = "";
            chkComplete.Checked = false;
            
            lblLockedBy.Visible = false;
            anytxtmodified = false;
            newval = string.Empty;
            oldval = string.Empty;
            dupmaster = 0;
            dupflag = "0";

            //reset search criteria fields
            txtOwner.Text = "";
            txtContact.Text = "";
            txtProjD.Text = "";
            txtProjL.Text = "";
            txtPhone1s.Text = "";
            txtPhone2s.Text = "";
            txtSeldate1.Text = "";
            txtSeldate2.Text = "";
            chkExact.Checked = false;
            cbSeldate.Text = "";
            btnCompare.Enabled = false;

            resplocked_by = "";
            presamplocked_by = "";

            dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
            FormatMfiData();
            lblRecordCount.Text = " ";

            txtSeldate1.Text = "";
            txtSeldate2.Text = "";

           // if (chkpending.Visible) && 
        }

        private void unlockTextboxes()
        {
            txtRBldgs.ReadOnly = false;
            txtRUnits.ReadOnly = false;
            txtStrtDate.ReadOnly = false;
            txtProjDesc.ReadOnly = false;
            txtProjLoc.ReadOnly = false;
            txtPCitySt.ReadOnly = false;
            txtRespOrg.ReadOnly = false;
            txtFactOff.ReadOnly = false;
            txtOthrResp.ReadOnly = false;
            txtAddr1.ReadOnly = false;
            txtAddr2.ReadOnly = false;
            txtAddr3.ReadOnly = false;
            txtZip.ReadOnly = false;
            txtRespnote.ReadOnly = false;
            txtRespname.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtExt.ReadOnly = false;
            txtRespname2.ReadOnly = false;
            txtPhone2.ReadOnly = false;
            txtExt2.ReadOnly = false;
            txtFax.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtWebUrl.ReadOnly = false;
            txtCommtext.ReadOnly = false;
            cbStatCode.Enabled = true;
            cbNewtc.Enabled = true;
            btnRespid.Enabled = true;
            lblLockedBy.Visible = false;
        }

        //If Case is Locked Textboxes Need to Not Accept Text Input
        private void lockTextboxes()
        {
            txtRBldgs.ReadOnly = true;
            txtRUnits.ReadOnly = true;
            txtStrtDate.ReadOnly = true;
            txtProjDesc.ReadOnly = true;
            txtProjLoc.ReadOnly = true;
            txtPCitySt.ReadOnly = true;
            txtRespOrg.ReadOnly = true;
            txtFactOff.ReadOnly = true;
            txtOthrResp.ReadOnly = true;
            txtAddr1.ReadOnly = true;
            txtAddr2.ReadOnly = true;
            txtAddr3.ReadOnly = true;
            txtZip.ReadOnly = true;
            txtRespnote.ReadOnly = true;
            txtRespname.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtExt.ReadOnly = true;
            txtRespname2.ReadOnly = true;
            txtPhone2.ReadOnly = true;
            txtExt2.ReadOnly = true;
            txtFax.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtWebUrl.ReadOnly = true;
            txtCommtext.ReadOnly = true;
            cbStatCode.Enabled = false;
            cbNewtc.Enabled = false;

            txtRespid.BackColor = Color.White;
            txtRBldgs.BackColor = Color.White;
            txtRUnits.BackColor = Color.White;
            txtStrtDate.BackColor = Color.White;
            txtProjDesc.BackColor = Color.White;
            txtProjLoc.BackColor = Color.White;
            txtPCitySt.BackColor = Color.White;
            txtRespOrg.BackColor = Color.White;
            txtFactOff.BackColor = Color.White;
            txtOthrResp.BackColor = Color.White;
            txtAddr1.BackColor = Color.White;
            txtAddr2.BackColor = Color.White;
            txtAddr3.BackColor = Color.White;
            txtZip.BackColor = Color.White;
            txtRespnote.BackColor = Color.White;
            txtRespname.BackColor = Color.White;
            txtPhone.BackColor = Color.White;
            txtExt.BackColor = Color.White;
            txtRespname2.BackColor = Color.White;
            txtPhone2.BackColor = Color.White;
            txtExt2.BackColor = Color.White;
            txtFax.BackColor = Color.White;
            txtEmail.BackColor = Color.White;
            txtWebUrl.BackColor = Color.White;
            txtCommtext.BackColor = Color.White;

            btnRespid.Enabled = false;

            if (chkpending.Visible)
                chkpending.Enabled = false;
        }

        //Set up the txt_txtChange Event to be called after Form Initialization
        //Add the control's handler to the event using the += operator to
        //raise the text changed event

        private void SetTxtChanged()
        {
            txtRespid.TextChanged += new EventHandler(txt_TextChanged);
            cbStatCode.TextChanged += new EventHandler(txt_TextChanged);
            txtStrtDate.TextChanged += new EventHandler(txt_TextChanged);
            cbNewtc.TextChanged += new EventHandler(txt_TextChanged);
            txtRBldgs.TextChanged += new EventHandler(txt_TextChanged);
            txtRUnits.TextChanged += new EventHandler(txt_TextChanged);
            txtBldgs.TextChanged += new EventHandler(txt_TextChanged);
            txtUnits.TextChanged += new EventHandler(txt_TextChanged);
            txtProjDesc.TextChanged += new EventHandler(txt_TextChanged);
            txtProjLoc.TextChanged += new EventHandler(txt_TextChanged);
            txtPCitySt.TextChanged += new EventHandler(txt_TextChanged);
            txtRespOrg.TextChanged += new EventHandler(txt_TextChanged);
            txtFactOff.TextChanged += new EventHandler(txt_TextChanged);
            txtOthrResp.TextChanged += new EventHandler(txt_TextChanged);
            txtAddr1.TextChanged += new EventHandler(txt_TextChanged);
            txtAddr2.TextChanged += new EventHandler(txt_TextChanged);
            txtAddr3.TextChanged += new EventHandler(txt_TextChanged);
            txtZip.TextChanged += new EventHandler(txt_TextChanged);
            txtRespname.TextChanged += new EventHandler(txt_TextChanged);
            txtPhone.TextChanged += new EventHandler(txt_TextChanged);
            txtExt.TextChanged += new EventHandler(txt_TextChanged);
            txtFax.TextChanged += new EventHandler(txt_TextChanged);
            txtRespname2.TextChanged += new EventHandler(txt_TextChanged);
            txtPhone2.TextChanged += new EventHandler(txt_TextChanged);
            txtExt2.TextChanged += new EventHandler(txt_TextChanged);
            txtRespnote.TextChanged += new EventHandler(txt_TextChanged);
            txtEmail.TextChanged += new EventHandler(txt_TextChanged);
            txtWebUrl.TextChanged += new EventHandler(txt_TextChanged);
            txtCommtext.TextChanged += new EventHandler(txt_TextChanged);
            chkComplete.CheckedChanged += new EventHandler(txt_TextChanged);
            chkpending.CheckedChanged += new EventHandler(txt_TextChanged);
        }

        //Clear the txt_txtChange Event due to Next Respid Form Initialization
        //Remove the controls from delegate using the -= operator
        private void RemoveTxtChanged()
        {
            txtRespid.TextChanged -= new EventHandler(txt_TextChanged);
            cbStatCode.TextChanged -= new EventHandler(txt_TextChanged);
            txtStrtDate.TextChanged -= new EventHandler(txt_TextChanged);
            cbNewtc.TextChanged -= new EventHandler(txt_TextChanged);
            txtRBldgs.TextChanged -= new EventHandler(txt_TextChanged);
            txtRUnits.TextChanged -= new EventHandler(txt_TextChanged);
            txtBldgs.TextChanged -= new EventHandler(txt_TextChanged);
            txtUnits.TextChanged -= new EventHandler(txt_TextChanged);
            txtProjDesc.TextChanged -= new EventHandler(txt_TextChanged);
            txtProjLoc.TextChanged -= new EventHandler(txt_TextChanged);
            txtPCitySt.TextChanged -= new EventHandler(txt_TextChanged);
            txtOwner.TextChanged -= new EventHandler(txt_TextChanged);
            txtRespOrg.TextChanged -= new EventHandler(txt_TextChanged);
            txtFactOff.TextChanged -= new EventHandler(txt_TextChanged);
            txtOthrResp.TextChanged -= new EventHandler(txt_TextChanged);
            txtAddr1.TextChanged -= new EventHandler(txt_TextChanged);
            txtAddr2.TextChanged -= new EventHandler(txt_TextChanged);
            txtAddr3.TextChanged -= new EventHandler(txt_TextChanged);
            txtZip.TextChanged -= new EventHandler(txt_TextChanged);
            txtRespname.TextChanged -= new EventHandler(txt_TextChanged);
            txtPhone.TextChanged -= new EventHandler(txt_TextChanged);
            txtExt.TextChanged -= new EventHandler(txt_TextChanged);
            txtFax.TextChanged -= new EventHandler(txt_TextChanged);
            txtRespname2.TextChanged -= new EventHandler(txt_TextChanged);
            txtPhone2.TextChanged -= new EventHandler(txt_TextChanged);
            txtExt2.TextChanged -= new EventHandler(txt_TextChanged);
            txtRespnote.TextChanged -= new EventHandler(txt_TextChanged);
            txtEmail.TextChanged -= new EventHandler(txt_TextChanged);
            txtWebUrl.TextChanged -= new EventHandler(txt_TextChanged);
            txtCommtext.TextChanged -= new EventHandler(txt_TextChanged);
            chkComplete.CheckedChanged -= new EventHandler(txt_TextChanged);
            chkpending.CheckedChanged -= new EventHandler(txt_TextChanged);
        }

        //Method to Determine if Relevant Textboxes and Masked Textboxes Have Been Modified

        private void txt_TextChanged(object sender, EventArgs e)
        {

            if (sender is TextBox)
            {
                txt = new TextBox();

                if (sender == txtRespid)
                {
                    //needed so that user does not get the popup asking to
                    //save changes if the user deletes the respid but makes
                    //no other changes to the data
                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldval = presample.Respid.Trim().ToString();
                        newval = txtRespid.Text.Trim().ToString();
                    }

                    oldRespid = presample.Respid.ToString().Trim();
                    newRespid = txtRespid.Text.ToString().Trim();
                }

                if (sender == txtRBldgs)
                {
                    oldval = presample.Rbldgs.Trim().ToString();
                    newval = txtRBldgs.Text.Trim().ToString();

                    oldRbldgs = presample.Rbldgs.ToString().Trim();
                    newRbldgs = txtRBldgs.Text.ToString().Trim();
                }

                if (sender == txtRUnits)
                {
                    oldval = presample.Runits.Trim().ToString();
                    newval = txtRUnits.Text.Trim().ToString();

                    oldRunits = presample.Runits.ToString().Trim();
                    newRunits = txtRUnits.Text.ToString().Trim();
                }

                if (sender == txtProjDesc)
                {
                    oldval = presample.ProjDesc.Trim().ToString();
                    newval = txtProjDesc.Text.Trim().ToString();
                }

                if (sender == txtProjLoc)
                {
                    oldval = presample.Projloc.Trim().ToString();
                    newval = txtProjLoc.Text.Trim().ToString();
                }

                if (sender == txtPCitySt)
                {
                    oldval = presample.PCitySt.Trim().ToString();
                    newval = txtPCitySt.Text.Trim().ToString();
                }

                if (sender == txtRespOrg)
                {
                    oldval = presample.Resporg.Trim().ToString();
                    newval = txtRespOrg.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldRespOrg = respaddress.Resporg.ToString().Trim();
                        newRespOrg = txtRespOrg.Text.ToString().Trim();
                    }
                }

                if (sender == txtFactOff)
                {
                    oldval = presample.Factoff.Trim().ToString();
                    newval = txtFactOff.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldFactoff = respaddress.Factoff.Trim().ToString();
                        newFactoff = txtFactOff.Text.Trim().ToString();
                    }
                }

                if (sender == txtOthrResp)
                {
                    oldval = presample.Othrresp.Trim().ToString();
                    newval = txtOthrResp.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldOthrresp = respaddress.Othrresp.Trim().ToString();
                        newOthrresp = txtOthrResp.Text.Trim().ToString();
                    }
                }

                if (sender == txtAddr1)
                {
                    oldval = presample.Addr1.Trim().ToString();
                    newval = txtAddr1.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldAddr1 = respaddress.Addr1.Trim().ToString();
                        newAddr1 = txtAddr1.Text.Trim().ToString();
                    }
                }

                if (sender == txtAddr2)
                {
                    oldval = presample.Addr2.Trim().ToString();
                    newval = txtAddr2.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldAddr2 = respaddress.Addr2.Trim().ToString();
                        newAddr2 = txtAddr2.Text.Trim().ToString();
                    }
                }

                if (sender == txtAddr3)
                {
                    oldval = presample.Addr3.Trim().ToString();
                    newval = txtAddr3.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldAddr3 = respaddress.Addr3.Trim().ToString();
                        newAddr3 = txtAddr3.Text.Trim().ToString();
                    }
                }

                if (sender == txtZip)
                {
                    oldval = presample.Zip.Trim().ToString();
                    newval = txtZip.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldZip = respaddress.Zip.Trim().ToString();
                        newZip = txtZip.Text.Trim().ToString();
                    }
                }

                if (sender == txtRespnote)
                {
                    oldval = presample.Respnote.Trim().ToString();
                    newval = txtRespnote.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldRespnote = respaddress.Respnote.Trim().ToString();
                        newRespnote = txtRespnote.Text.Trim().ToString();
                    }
                }

                if (sender == txtStrtDate)
                {
                    oldval = presample.Strtdate.Trim().ToString();
                    newval = txtStrtDate.Text.Trim().ToString();

                    oldStrtdte = presample.Strtdate.Trim().ToString();
                    newStrtdte = txtStrtDate.Text.Trim().ToString();
                }

                if (sender == txtRespname)
                {
                    oldval = presample.Respname.Trim().ToString();
                    newval = txtRespname.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldRespname = respaddress.Respname.Trim().ToString();
                        newRespname = txtRespname.Text.Trim().ToString();
                    }
                }

                if (sender == txtExt)
                {
                    oldval = presample.Ext.Trim().ToString();
                    newval = txtExt.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldExt = respaddress.Ext.Trim().ToString();
                        newExt = txtExt.Text.Trim().ToString();
                    }
                }

                if (sender == txtRespname2)
                {
                    oldval = presample.Respname2.Trim().ToString();
                    newval = txtRespname2.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldRespname2 = respaddress.Respname2.Trim().ToString();
                        newRespname2 = txtRespname2.Text.Trim().ToString();
                    }
                }

                if (sender == txtExt2)
                {
                    oldval = presample.Ext2.Trim().ToString();
                    newval = txtExt2.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldExt2 = respaddress.Ext2.Trim().ToString();
                        newExt2 = txtExt2.Text.Trim().ToString();
                    }
                }

                if (sender == txtCommtext)
                {
                    oldval = presample.Commtext.Trim().ToString();
                    newval = txtCommtext.Text.Trim().ToString();

                    oldCommtext = presample.Commtext.Trim().ToString();
                    newCommtext = txtCommtext.Text.Trim().ToString();

                    /*DM20201118 if changed then populate these*/
                 /*   if (oldCommtext != newCommtext)
                    {
                        commentdate = presample.Commdate;
                        commenttime = presample.Commtime;
                        usrnme = presample.Usrnme;
                    } */
                } 

                if (sender == txtEmail)
                {
                    oldval = presample.Email.Trim().ToString();
                    newval = txtEmail.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldEmail = respaddress.Email.Trim().ToString();
                        newEmail = txtEmail.Text.Trim().ToString();
                    }
                }

                if (sender == txtWebUrl)
                {
                    oldval = presample.Weburl.Trim().ToString();
                    newval = txtWebUrl.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldWeburl = respaddress.Web.Trim().ToString();
                        newWeburl = txtWebUrl.Text.Trim().ToString();
                    }
                }
            }

            //Must Declare Masked Textboxes in this Way to Avoid Exceptions

            else if (sender is MaskedTextBox)
            {

                mtxt = new MaskedTextBox();

                if (sender == txtPhone)
                {
                    oldval = presample.Phone.Trim().ToString();
                    newval = txtPhone.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldPhone = respaddress.Phone.Trim().ToString();
                        newPhone = txtPhone.Text.Trim().ToString();
                    }
                }

                if (sender == txtPhone2)
                {
                    oldval = presample.Phone2.Trim().ToString();
                    newval = txtPhone2.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldPhone2 = respaddress.Phone2.Trim().ToString();
                        newPhone2 = txtPhone2.Text.Trim().ToString();
                    }
                }

                if (sender == txtFax)
                {
                    oldval = presample.Fax.Trim().ToString();
                    newval = txtFax.Text.Trim().ToString();

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                    {
                        oldFax = respaddress.Fax.Trim().ToString();
                        newFax = txtFax.Text.Trim().ToString();
                    }
                }
            }

            else if (sender is ComboBox)
            {
                cbobox = new ComboBox();

                if (sender == cbStatCode)
                {
                    oldval = presample.Status.ToString().Trim();
                    newval = cbStatCode.SelectedValue.ToString().Trim();

                    oldStatcde = presample.Status.ToString().Trim();
                    newStatcde = cbStatCode.SelectedValue.ToString().Trim();
                }
            }
            else if (sender is CheckBox)
            {
                if (sender == chkComplete)
                {
                    oldval = "0";
                    newval = "1"; 
                }
                if (sender == chkpending)
                {
                    oldval = "0";
                    newval = "3";
                }
            }

            if (oldval != newval)
            {
                anytxtmodified = true;
            }
            else
            {
                anytxtmodified = false;
            }

            DateTime prgdtm = DateTime.Now;
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
                ca.Respid = txtRespid.Text;
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

        /*Validate the date string */
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

                    if (Convert.ToInt32(input_year) < 2000 || Convert.ToInt32(input_year) > 2050)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(input_mon) > 12 || Convert.ToInt32(input_mon) == 0)
                        return false;
                }
            }
            return true;
        }

        /**************C. 9 STATUS CODE Changes************************/
        private int dupmaster;
        private string dupflag;
        private string selectedStatCode;

        private bool cboStatCode_Check()
        {
            int iStrtDate;
            int iCurMonYr;

            if (string.IsNullOrWhiteSpace(txtStrtDate.Text))
            { iStrtDate = 0; }
            else
            { iStrtDate = int.Parse(txtStrtDate.Text); }

            iCurMonYr = int.Parse(GeneralFunctions.CurrentYearMon());

            //if status code is changed to 4 and start date is blank, reject change
            //if status code is changed to 4 and start date is not blank but less
            //than the current  processing month, reject change
            if (newStatcde == "4")
            {
                if (iStrtDate == 0)
                {
                    DialogResult result = MessageBox.Show("Status change was rejected", "Invalid Selection!", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        cbStatCode.Focus();
                        cbStatCode.SelectedValue = oldStatcde;
                        return false;
                    }
                }
                else if (iStrtDate != 0 && iStrtDate < iCurMonYr)
                {
                    DialogResult result = MessageBox.Show("Status change rejected, Project has started", "Invalid Selection!", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        cbStatCode.Focus();
                        cbStatCode.SelectedValue = oldStatcde;
                        return false;
                    }
                }
            }

            //If the value of start date is greater than or equal to the 
            //current month, the stat code is 4. 
            if ((oldStatcde == "4" || selectedStatCode == "4") &&
                (newStatcde == "1" || newStatcde == "2" ||
                newStatcde == "3" || newStatcde == "6" ||
                newStatcde == "7" || newStatcde == "8"))
            {
                if (iStrtDate >= iCurMonYr && iStrtDate != 0)
                {
                    DialogResult result = MessageBox.Show("Status change rejected, Project has not started", "Invalid Selection!", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        cbStatCode.Focus();
                        cbStatCode.SelectedValue = 4;
                        return false;
                    }
                }
            }

            if (newStatcde == "5" && compareflg == false)
            {

                frmMfParentPopup fMFParent = new frmMfParentPopup(Psu, Bpoid, id);
                fMFParent.ShowDialog();

                if (fMFParent.DialogResult == DialogResult.OK)
                {
                    dupmaster = fMFParent.Dupmaster;
                    dupflag = fMFParent.Dupflag;

                    if (dupflag == "P") dupflag = "1";
                    if (dupflag == "S") dupflag = "0";
                }

                if (fMFParent.DialogResult == DialogResult.Cancel)
                {
                    cbStatCode.SelectedValue = oldStatcde;
                }
            }

            //if status not 5 then set dupmaster to 0 and set dupflag to 0
            if (newStatcde != "5" && compareflg == false)
            {
                dupmaster =0;
                dupflag = "0";
            }

            return true;
        }

        private void cbStatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = (ComboBox)sender;

            if (!cb.Focused)
            {
                return;
            }

            if (editable)
            {
                if (oldStatcde != cbStatCode.SelectedValue.ToString() ||
                    selectedStatCode != cbStatCode.SelectedValue.ToString())
                {
                    if (!cboStatCode_Check())
                    {
                        cbStatCode.SelectedValue = oldStatcde;
                    }
                }
            }
        }

        /**************C. 10 Start Date Changes************************/

        private string currYearMon = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2");

        private void txtStrtdate_Enter(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtStrtDate.Text.Trim())) txtStrtDate.Clear();
            txtStrtDate.SelectionStart = 0;
        }

        private void txtStrtdate_Validating(object sender, CancelEventArgs e)
        {
            if (!CheckStrtdateEnter())
            {
                txtStrtDate.Text = oldStrtdte;
                e.Cancel = true;
            }
            else
                oldStrtdte = txtStrtDate.Text;
        }

        //allow only numbers to be entered
        private void txtStrtdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //Verify entered Strtdate
        private bool CheckStrtdateEnter()
        {
            if (txtStrtDate.Text == "")
            {
                //if the project has future strtdate, status is "4", change to "1"
                if (oldStrtdte != "" && (Convert.ToInt32(oldStrtdte) > Convert.ToInt32(currYearMon)) && cbStatCode.SelectedValue.ToString() == "4")
                {
                    cbStatCode.SelectedIndex = 0;
                }
            }

            if (txtStrtDate.Text != "")
            {
                if (!ValidateDate(txtStrtDate.Text))
                {
                    MessageBox.Show("Not a valid start date");
                    return false;
                }

                DateTime strt = DateTime.ParseExact(txtStrtDate.Text, "yyyyMM", CultureInfo.InvariantCulture);
                DateTime sel = DateTime.ParseExact(txtSelDate.Text, "yyyyMM", CultureInfo.InvariantCulture);

                if (GeneralFunctions.GetNumberMonths(strt, sel) > 24)
                {
                    MessageBox.Show("Start Date cannot be earlier than 24 months before Selection Date");
                    return false;
                }

                //Update status
                //If strtdate accepted and strtdate greater than or equal to the current month
                //and the status is not '5' then set status to 4
                if (Convert.ToInt32(txtStrtDate.Text) >= Convert.ToInt32(currYearMon) && cbStatCode.SelectedValue.ToString() != "5")
                {
                    cbStatCode.SelectedValue = 4;
                    selectedStatCode = "4";
                }

                // if the strtdate less than the current month and the status not '5' then 
                //set status to 1
                else if (Convert.ToInt32(txtStrtDate.Text) < Convert.ToInt32(currYearMon) && cbStatCode.SelectedValue.ToString() != "5")
                {
                    cbStatCode.SelectedValue = 1;
                }

            }
            return true;
        }

        private void txtStrtdate_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable)
            {
                if (e.KeyCode == Keys.Enter && oldStrtdte != txtStrtDate.Text)
                {
                    if (!CheckStrtdateEnter())
                    {
                        txtStrtDate.Text = oldStrtdte;
                    }
                    else
                        oldStrtdte = txtStrtDate.Text;
                }
            }
        }

        /**************C. 11 Address Building and Unit Changes************************/

        //Can only enter numbers
        private void txtNumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private int numRbldgs;
        private int numRunits;

        private void txtRbldgs_Check()
        {
            if (txtRBldgs.Text.Trim() != string.Empty)
                numRbldgs = int.Parse(txtRBldgs.Text);
            else
                numRbldgs = 0;
            if (txtRUnits.Text.Trim() != string.Empty)
                numRunits = int.Parse(txtRUnits.Text);
            else
                numRunits = 0;

            if (!(numRbldgs >= 1 && numRbldgs <= 999))
            {
                MessageBox.Show("Rbldgs must be between 1 and 999", "Invalid Entry!", MessageBoxButtons.OK);
                notvalid = true;
                anytxtmodified = false;
                txtRBldgs.Focus();
                txtRBldgs.Text = oldRbldgs;
            }
            else
            {
                if (((2 * numRbldgs) - 1) >= numRunits)
                {
                    DialogResult result = MessageBox.Show("Runits must be greater than or equal to two times Rbldgs", "Invalid Entry!", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        anytxtmodified = false;
                        txtRBldgs.Focus();
                        txtRBldgs.Text = oldRbldgs;
                        done = false;
                        return;
                    }
                }
            }
        }

        private void txtRunits_Check()
        {
            if (txtRBldgs.Text.Trim() != string.Empty)
                numRbldgs = int.Parse(txtRBldgs.Text);
            else
                numRbldgs = 0;
            if (txtRUnits.Text.Trim() != string.Empty)
                numRunits = int.Parse(txtRUnits.Text);
            else
                numRunits = 0;

            if (!(numRunits >= 2 && numRunits <= 9999))
            {
                MessageBox.Show("Runits must be between 2 and 9999", "Invalid Entry!", MessageBoxButtons.OK);
                notvalid = true;
                anytxtmodified = false;
                txtRUnits.Focus();
                txtRUnits.Text = oldRunits;
            }
            else
            {
                if (((2 * numRbldgs) - 1) >= numRunits)
                {
                    DialogResult result = MessageBox.Show("Runits must be greater than or equal to two times Rbldgs", "Invalid Entry!", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        anytxtmodified = false;
                        txtRUnits.Focus();
                        txtRUnits.Text = oldRunits;
                        done = false;
                        return;
                    }
                }
            }
        }

        private void txtRBldgs_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRbldgs_Check();
            }
        }

        private void txtRUnits_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                txtRunits_Check();
            }
        }

        private void txtRUnits_Leave(object sender, EventArgs e)
        {
            txtRunits_Check();
        }

        private void txtRBldgs_Leave(object sender, EventArgs e)
        {
            txtRbldgs_Check();
        }

        /***************************************************************/
        /**************C. 32 Review Status Entry************************/

        private string worked;
        private string rev1nme;
        private string rev2nme;
        private string revnme;

        //Update the review status if checkbox checked
        private void chkCompleteUpdate()
        {
            worked = presample.Worked.Trim();
            rev1nme = presample.Rev1nme.Trim();
            rev2nme = presample.Rev2nme.Trim();
            revnme = "";
            int reviewNum;

            if (editable)
            {
                if ((rev1nme == "" && worked == "0")|| (rev2nme == "" && worked == "3"))
                {
                    revnme = user;
                    if (chkComplete.Checked)
                        worked = "1";
                    else if (chkpending.Checked)
                        worked = "3";
                    reviewNum = 1;
                    mfidata.UpdateReviewStatus(id, worked, revnme, reviewNum);
                }
                else if (((rev1nme != "" && worked == "1" && rev1nme != user) || (rev2nme != "" && worked == "3" && rev1nme != user)) && !(UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4"))
                {
                    revnme = user;
                    if (chkComplete.Checked)
                        worked = "2";
                    else if (chkpending.Checked)
                        worked = "3";
                    reviewNum = 2;
                    mfidata.UpdateReviewStatus(id, worked, revnme, reviewNum);
                }
            }
            rev1nme = presample.Rev1nme.Trim();
        }

        private void chkComplete_CheckChanged(object sender, EventArgs e)
        {
            rev1nme = presample.Rev1nme.Trim();
            if (chkpending.Visible)
            {
                if (chkComplete.Checked)
                    chkpending.Enabled = false;
                else
                    chkpending.Enabled = true;
            }
            anytxtmodified = true;
        }

        private void chkpending_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpending.Checked)
                chkComplete.Enabled = false;
            else
                chkComplete.Enabled = true;

            anytxtmodified = true;
        }

        /********************************************************************/

        //Validate addr1 input by user is in acceptable format

        private void txtAddr1_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if (txtAddr1.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Address is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtAddr1.Focus();
                    txtAddr1.Text = presample.Addr1;
                    return;
                }
            }
        }

        //validate City State field

        private bool ValidateCityState()
        {
            bool result = true;

            //check valid state
            if (string.IsNullOrWhiteSpace(txtAddr3.Text) || GeneralFunctions.HasSpecialCharsInCityState(txtAddr3.Text))
            {
                MessageBox.Show("City/State is invalid!");
                result = false;
            }
            else
            {
                string[] words = GeneralFunctions.SplitWords(txtAddr3.Text.Trim());
                int num_word = words.Count();
                string rst = string.Empty;
                if (num_word < 2)
                {
                    MessageBox.Show("City/State is invalid!");
                    result = false;
                }
                else
                {
                    if (words[num_word - 1] == "CANADA")
                    {
                        rst = words[num_word - 2];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, true))
                        {
                            MessageBox.Show("State/Province is invalid!");
                            result = false;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                            {
                                rstate = rst;
                            }
                            //set the timezone textbox to the new timezone - CANADA
                            txtZone.Text = GeneralDataFuctions.GetTimezone(rst);
                        }
                    }
                    else
                    {
                        rst = words[num_word - 1];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, false))
                        {
                            MessageBox.Show("State/Province is invalid!");
                            result = false;
                        }
                        else
                        {
                            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                            {
                                rstate = rst;
                            }
                            //set the timezone textbox to the new timezone - US
                            txtZone.Text = GeneralDataFuctions.GetTimezone(rst);
                        }
                    }
                }
            }

            return result;
        }

        //Validate addr3 input by user is in acceptable format

        private void txtAddr3_Leave(object sender, EventArgs e)
        {
            if (!ValidateCityState())
            {
                txtAddr3.Focus();
                txtAddr3.Text = presample.Addr3;
            }
        }

        /**************C. 12 Comments Entry************************/

        private void txtComment_Leave(object sender, EventArgs e)
        {
            if (oldCommtext != newCommtext && (!String.IsNullOrEmpty(txtCommtext.Text.Trim())))
            {
                commentdate = DateTime.Now.ToString("yyyyMMdd");
                commenttime = DateTime.Now.ToString("HHmmss");
                usrnme = UserInfo.UserName;
            }

            //If the user deletes the comment then delete the values in the comment info fields
            if (oldCommtext != newCommtext && (String.IsNullOrEmpty(txtCommtext.Text.Trim())))
            {
                commentdate = String.Empty;
                commenttime = String.Empty;
                usrnme = String.Empty;
            }
        }

        //Check address 3 is Canada or not
        private bool CheckAddress3IsCanada()
        {
            string[] words = GeneralFunctions.SplitWords(txtAddr3.Text.Trim());
            int num_word = words.Count();

            if (words[num_word - 1] == "CANADA")
                return true;
            else
                return false;
        }

        //Validate zip is valid US or Canadian

        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            bool isCanada = CheckAddress3IsCanada();
            if (txtZip.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtZip.Focus();
                    txtZip.Text = presample.Zip.Trim();
                    return;
                }
            }
            else if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
            {
                MessageBox.Show("Zip Code is invalid");
                txtZip.Focus();
                txtZip.Text = presample.Zip.Trim();
                return;
            }
        }

        //Validate phone number is valid length
        //make sure masked textbox format is set to exclude literals and prompts

        //private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        //{

        //    if (e.KeyCode == Keys.Enter)
        //    {
        //        if (oldPhone != txtPhone.Text)
        //        {
        //            if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
        //            {
        //                DialogResult result = MessageBox.Show("Phone number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

        //                if (result == DialogResult.OK)
        //                {

        //                    txtPhone.Focus();
        //                    txtPhone.Text = presample.Phone;
        //                    return;
        //                }
        //            }
        //        }
        //    }

        //}

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtPhone.Focus();
                    txtPhone.Text = presample.Phone;
                    return;
                }
            }
        }

        private void txtPhone2_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtPhone2.Focus();
                    txtPhone2.Text = presample.Phone2;
                    return;
                }
            }
        }

        private void txtExt_Enter(object sender, EventArgs e)
        {
            txtExt.SelectionStart = 0;
            txtExt.SelectionLength = txtExt.Text.Length;
        }

        private void txtExt_MouseClick(object sender, MouseEventArgs e)
        {
            txtExt.SelectionStart = 0;
            txtExt.SelectionLength = txtExt.Text.Length;
        }

        private void txtExt2_Enter(object sender, EventArgs e)
        {
            txtExt2.SelectionStart = 0;
            txtExt2.SelectionLength = txtExt2.Text.Length;
        }

        private void txtExt2_MouseClick(object sender, MouseEventArgs e)
        {
            txtExt2.SelectionStart = 0;
            txtExt2.SelectionLength = txtExt2.Text.Length;
        }

        //Validate fax number is valid length
        //make sure masked textbox format is set to exclude literals and prompts

        private void txtFax_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Fax number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtFax.Focus();
                    txtFax.Text = presample.Fax;
                    return;
                }
            }
        }

        //Validate that the email address is in acceptable format

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if (txtEmail.Text.Trim() != "")
            {

                string inputEmail = txtEmail.Text.Trim();

                if (GeneralFunctions.isEmail(inputEmail))
                {
                    //Valid Email Address
                }
                else
                {
                    DialogResult result = MessageBox.Show("Email Address is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtEmail.Focus();
                        txtEmail.Text = presample.Email;
                        return;
                    }
                }
            }
        }

        private void txtWebUrl_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if (txtWebUrl.Text.Trim() != "")
            {
                if (!GeneralData.IsValidURL(txtWebUrl.Text))
                {
                    DialogResult result = MessageBox.Show("Web Address is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtWebUrl.Focus();
                        txtWebUrl.Text = presample.Weburl;
                        return;
                    }
                }
            }
        }

        private void SaveData()
        {
            //re-validate the fields again before saving

            notvalid = false;

            txtRbldgs_Check();
            txtRunits_Check();

            //Update FIVEPLUS based on the value of RUNITS

            if (numRunits < 5) fiveplus = "0";
            else fiveplus = "1";

            //if status not 5 then set dupmaster to 0 and set dupflag to 0
            //if (oldStatcde != "5" && newStatcde != "5")
            //{
            //    dupmaster = 0;
            //    dupflag = "0";
            //}

            //Ensure that if txtCommtext is blank or unchanged, comment
            //field are not updated
            if (txtCommtext.Text.Trim() == "")
            {
                commentdate = String.Empty;
                commenttime = String.Empty;
                usrnme = String.Empty;
            }
            else if (oldCommtext == newCommtext)
            {
                commentdate = presample.Commdate;
                commenttime = presample.Commtime;
                usrnme = presample.Usrnme;
            }
            //CZ20210125
            else
            {
                commentdate = DateTime.Now.ToString("yyyyMMdd");
                commenttime = DateTime.Now.ToString("HHmmss");
                usrnme = UserInfo.UserName;
            }

            if (txtAddr1.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Address is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtAddr1.Focus();
                    txtAddr1.Text = presample.Addr1;
                    return;
                }
            }

            //if (txtAddr3.Text.Trim() != "")
            //{
                if (!ValidateCityState())
                {
                    txtAddr3.Focus();
                    txtAddr3.Text = presample.Addr3;
                }
            //}

            bool isCanada = CheckAddress3IsCanada();
            if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtZip.Focus();
                    txtZip.Text = presample.Zip.Trim();
                    return;
                }
            }

            if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtPhone.Focus();
                    txtPhone.Text = presample.Phone;
                    return;
                }
            }

            if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtPhone2.Focus();
                    txtPhone2.Text = presample.Phone2;
                    return;
                }
            }

            if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Fax number is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtFax.Focus();
                    txtFax.Text = presample.Fax;
                    return;
                }
            }

            if (txtEmail.Text.Trim() != "")
            {
                string inputEmail = txtEmail.Text.Trim();

                if (GeneralFunctions.isEmail(inputEmail))
                {
                    //Valid Email Address
                }
                else
                {
                    DialogResult result = MessageBox.Show("Email Address is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtEmail.Focus();
                        txtEmail.Text = presample.Email;
                        return;
                    }
                }
            }

            if (txtWebUrl.Text.Trim() != "")
            {
                if (!GeneralData.IsValidURL(txtWebUrl.Text))
                {
                    DialogResult result = MessageBox.Show("Web Address is invalid!", "Invalid Entry!", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtWebUrl.Focus();
                        txtWebUrl.Text = presample.Weburl;
                        return;
                    }
                }
            }

            /*************** PRESAMPLE UPDATE *******************/
            
            mfidata.UpdatePresampleData(id, txtRespid.Text, cbStatCode.SelectedValue.ToString(),
               txtRBldgs.Text, txtRUnits.Text, fiveplus, txtStrtDate.Text, txtProjDesc.Text, txtProjLoc.Text,
               txtPCitySt.Text, txtRespOrg.Text, txtFactOff.Text, txtOthrResp.Text, txtAddr1.Text,
               txtAddr2.Text, txtAddr3.Text, txtZip.Text, txtRespnote.Text, txtRespname.Text,
               txtPhone.Text, txtExt.Text, txtRespname2.Text, txtPhone2.Text, txtExt2.Text,
               txtFax.Text, txtEmail.Text, txtWebUrl.Text, txtCommtext.Text, commentdate, commenttime, usrnme,
               dupmaster, dupflag);

            //if user checks the review complete checkbox,
            //update the rev1name or rev2name fields in the
            //Presample table

            if (chkComplete.Checked || chkpending.Checked)
                chkCompleteUpdate();

            /*************** RESPONDENT UPDATE *******************/
            //update respondent table if there is a value for respid           
            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()) || !String.IsNullOrWhiteSpace(txtRespid.Text.Trim()))

                mfidata.UpdateRespondentData(txtRespid.Text, txtRespOrg.Text,
                    txtRespname.Text, txtRespname2.Text, txtPhone.Text, txtPhone2.Text,
                    txtExt.Text, txtExt2.Text, txtAddr1.Text, txtAddr2.Text, txtAddr3.Text,
                    txtZip.Text, txtFax.Text, txtFactOff.Text, txtOthrResp.Text,
                    txtRespnote.Text, txtEmail.Text, txtWebUrl.Text, rstate);

            /*************** RESPONDENT AUDIT *******************/
            //update the respondent audit table if there is a value for respid
            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()) || !String.IsNullOrWhiteSpace(txtRespid.Text.Trim()))
            {
                if (oldRespOrg != newRespOrg)
                    AddRspauditData("RESPORG", oldRespOrg, newRespOrg);
                if (oldRespname != newRespname)
                    AddRspauditData("RESPNAME", oldRespname, newRespname);
                if (oldRespname2 != newRespname2)
                    AddRspauditData("RESPNAME2", oldRespname2, newRespname2);
                if (oldPhone != newPhone)
                    AddRspauditData("PHONE", oldPhone, newPhone);
                if (oldPhone2 != newPhone2)
                    AddRspauditData("PHONE2", oldPhone2, newPhone2);
                if (oldExt != newExt)
                    AddRspauditData("EXT", oldExt, newExt);
                if (oldExt2 != newExt2)
                    AddRspauditData("EXT2", oldExt2, newExt2);
                if (oldAddr1 != newAddr1)
                    AddRspauditData("ADDR1", oldAddr1, newAddr1);
                if (oldAddr2 != newAddr2)
                    AddRspauditData("ADDR2", oldAddr2, newAddr2);
                if (oldAddr3 != newAddr3)
                    AddRspauditData("ADDR3", oldAddr3, newAddr3);
                if (oldZip != newZip)
                    AddRspauditData("ZIP", oldZip, newZip);
                if (oldFax != newFax)
                    AddRspauditData("FAX", oldFax, newFax);
                if (oldFactoff != newFactoff)
                    AddRspauditData("FACTOFF", oldFactoff, newFactoff);
                if (oldOthrresp != newOthrresp)
                    AddRspauditData("OTHRRESP", oldOthrresp, newOthrresp);
                if (oldRespnote != newRespnote)
                    AddRspauditData("RESPNOTE", oldRespnote, newRespnote);
                if (oldEmail != newEmail)
                    AddRspauditData("EMAIL", oldEmail, newEmail);
                if (oldWeburl != newWeburl)
                    AddRspauditData("WEBURL", oldWeburl, newWeburl);
            }

            //initial statcde
            oldStatcde = "0";
            newStatcde = "0";

            if (Respauditlist.Count > 0)
            {
                foreach (Respaudit element in Respauditlist)
                {
                    //This is function is located in
                    //ProjectAuditData.cs
                    auditData.AddRspauditData(element);
                }

                Respauditlist.Clear();
            }
            anytxtmodified = false;
        }

        private void DataChanged()
        {
            done = true;

            if (anytxtmodified)
            {
                DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                { 
                    SaveData();

                    if (notvalid)
                    {
                        done = false;
                    }
                }
            }
        }

        private string CallReviewCompDialg()
        {
            string returnedstring = string.Empty;

            //Check if the record is locked
            if (editable)
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    if ((chkComplete.Visible && !chkComplete.Checked) && (chkpending.Visible && !chkpending.Checked))                 
                    {
                        frmRevCompMsgbox fRevComp = new frmRevCompMsgbox();
                        DialogResult result3 = fRevComp.ShowDialog();
                        returnedstring = fRevComp.RevCompRtn;
                    }
                }              
            }

            return returnedstring;
        }

        private void frmMfInitial_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (editable)
            {
                relsPresampleLock();
                relsRespondentLock();
            }

            //Update psamp_history if cancel button is 
            //not clicked when user enters screen
            if (id != string.Empty && id != null)
                UpdateCaseAccessRecord();

            GeneralDataFuctions.AddCpraccessData(access_code, "EXIT");
        }

        //Verify Form closing
        public override bool VerifyFormClosing()
        {
            DataChanged();

            bool can_close = true;

            if (done == true)
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    string returnval = CallReviewCompDialg();
                    if (returnval == "Yes")
                    {
                        chkComplete.Checked = true;
                        SaveData();
                    }
                    else if (returnval == "Cancel")
                        can_close = false;
                }
            }
            else
                can_close = false;

            return can_close;
        }

        //this controls the color of text on the contacts tab
        //if primary or secondary is blank the tab text will be black
        //otherwise the tabs are blue

        private void tbContact_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == 0)
            {
                if (txtContact.Text == "")
                {
                    e.Graphics.DrawString(tbContact.TabPages[e.Index].Text,
                        tbContact.Font,
                        Brushes.Black,
                        new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
                else
                {
                    e.Graphics.DrawString(tbContact.TabPages[e.Index].Text,
                         tbContact.Font,
                         Brushes.Blue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
            if (e.Index == 1)
            {
                if (txtRespname2.Text == "")
                {
                    e.Graphics.DrawString(tbContact.TabPages[e.Index].Text,
                        tbContact.Font,
                        Brushes.Black,
                        new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
                else
                {
                    e.Graphics.DrawString(tbContact.TabPages[e.Index].Text,
                         tbContact.Font,
                         Brushes.Blue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
        }

        /*************** C 30. RESTORE*******************/
        //Allow user to restore the original values for the contact fields
        //for the initial case being viewed

        private void RestoreOriginalValues()
        {

            anytxtmodified = false;

            presample = mfidata.GetPresample(id);

            string rst = GetState(presample.OAddr3.Trim());

            txtZone.Text = GeneralDataFuctions.GetTimezone(rst);

            txtFin.Text = presample.Psu + " " + presample.Bpoid + " " + presample.Sched;

            //mastid = presample.Masterid;

            txtRespid.Clear();

            txtRespOrg.Text = presample.OResporg;
            txtFactOff.Text = presample.OFactoff;
            txtOthrResp.Text = presample.OOthrresp;
            txtAddr1.Text = presample.OAddr1;
            txtAddr2.Text = presample.OAddr2;
            txtAddr3.Text = presample.OAddr3;
            txtZip.Text = presample.OZip;
            txtRespnote.Text = presample.ORespnote;
            txtRespname.Text = presample.ORespname;
            txtPhone.Text = presample.OPhone;
            txtExt.Text = presample.OExt;
            txtRespname2.Text = presample.ORespname2;
            txtPhone2.Text = presample.OPhone2;
            txtExt2.Text = presample.OExt2;
            txtFax.Text = presample.OFax;
            txtEmail.Text = presample.OEmail;
            txtWebUrl.Text = presample.OWeburl;

            txtReview.Text = setStatus();

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
            {
                btnAudit.Enabled = true;
            }
        }

        /******SEARCH SECTION******/

        //if "Between" is selected, then tp Seldate2 is displayed"

        private void cbSeldate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSeldate.SelectedItem != null && cbSeldate.SelectedItem.ToString() == "Between")
            {
                lblSeldateto.Visible = true;
                txtSeldate2.Visible = true;
            }
            else
            {
                lblSeldateto.Visible = false;
                txtSeldate2.Visible = false;
            }
        }

        private void SetHiddenSearchLbl()
        {
            lblSeldateto.Visible = false;
            txtSeldate1.Visible = true;
            txtSeldate2.Visible = false;
        }

        /*Blank the search textboxes*/

        private void ResetSearchParameters()
        {
            txtOwner.Text = "";
            txtContact.Text = "";
            txtProjD.Text = "";
            txtProjL.Text = "";
            txtPhone1s.Text = "";
            txtPhone2s.Text = "";

            cbSeldate.SelectedIndex = -1;
            txtSeldate1.Text = "";
            txtSeldate2.Text = "";

            chkExact.Checked = false;

            dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
            FormatMfiData();

            lblRecordCount.Text = " ";
        }

        /*Keydown event for the search criteria textboxes*/

        private void txt_SearchCriteria_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        /*Enter event for the search criteria textboxes*/

        private void txt_SearchCriteria_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        /*Leave event for the search criteria textboxes*/
        private void txt_SearchCriteria_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
            //Search();
        }

        private void txtSeldate1_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE1");
        }

        private void txtSeldate2_TextChanged(object sender, EventArgs e)
        {
            GeneralFunctions.CheckIntegerField(sender, "SELDATE2");
        }

        private void txtSeldate1_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
                Search();
        }
        private void txtSeldate2_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 6))
                Search();
        }
        private bool VerifyParameters()
        {
            //Verify any parameter entered
            if (txtOwner.Text.Trim() == "" && txtContact.Text.Trim() == "" &&
                txtProjD.Text.Trim() == "" && txtProjL.Text == "" &&
                //cbSeldate.Text.Trim() == "" && 
                txtPhone1s.Text.Trim() == "" && txtPhone2s.Text.Trim() == "" &&
                txtSeldate1.Text.Trim() == "" && txtSeldate2.Text.Trim() == "")
            {
                return true;
            }

            if (txtOwner.Text.Trim().Length > 0)
            {
                string[] words = txtOwner.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Owner to search.");
                    txtOwner.Focus();
                    txtOwner.SelectAll();
                    return false;
                }
            }

            if (txtContact.Text.Trim().Length > 0)
            {
                string[] words = txtContact.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Contact to search.");
                    txtContact.Focus();
                    txtContact.SelectAll();
                    return false;
                }
            }

            if (txtProjD.Text.Trim().Length > 0)
            {
                string[] words = txtProjD.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Project Description to search.");
                    txtProjD.Focus();
                    txtProjD.SelectAll();
                    return false;
                }
            }

            if (txtProjL.Text.Trim().Length > 0)
            {
                string[] words = txtProjL.Text.Trim().Split();
                if (words.Count() > 3)
                {
                    MessageBox.Show("Please Enter 1 to 3 words on Project Location to search.");
                    txtProjL.Focus();
                    txtProjL.SelectAll();
                    return false;
                }
            }

            //Verify Between fields
            if (!VerifyBetween())
                return false;

            return true;

        }

        private Boolean VerifyBetween()
        {
            Boolean result = false;

            if (cbSeldate.Text == "Between")
                if (!GeneralFunctions.VerifyBetweenParameters(txtSeldate1.Text, txtSeldate2.Text, "Seldate"))
                    return result;

            return result = true;
        }

        private void Search()
        {
            if (VerifyParameters())
            {
                this.Cursor = Cursors.WaitCursor;
                GetMfiData();
                this.Cursor = Cursors.Default;
            }
        }

        private void GetMfiData()
        {
            DataTable dt = GetDataTable();

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There is no match data found.");
                btnCompare.Enabled = false;
            }

            if (dt.Rows.Count > 0)
            {
                btnCompare.Enabled = true;
            }

            dgInitialsSrch1.DataSource = dt;

            FormatMfiData();

            if (dt.Rows.Count == 1)
                lblRecordCount.Text = dt.Rows.Count.ToString() + " record found.";
            else
                lblRecordCount.Text = dt.Rows.Count.ToString() + " records found.";
        }

        // Place the row number in the row header
        private void SetHeaderCellValue()
        {
            foreach (DataGridViewRow row in dgInitialsSrch1.Rows)
            {
                row.HeaderCell.Value = String.Format("{0}", row.Index + 1);
            }
            dgInitialsSrch1.AutoResizeRowHeadersWidth(
                DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders);
        }

        private void FormatMfiData()
        {
            dgInitialsSrch1.RowHeadersVisible = true;  // set it to false if not needed
            dgInitialsSrch1.ShowCellToolTips = false;
            dgInitialsSrch1.DefaultCellStyle.ForeColor = Color.Black;
            dgInitialsSrch1.RowHeadersDefaultCellStyle.Font = new Font(dgInitialsSrch1.Font, FontStyle.Regular);

            SetHeaderCellValue();

            for (int i = 0; i < dgInitialsSrch1.ColumnCount; i++)
            {

                //Set columns Header Text
                if (i == 0)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "MASTERID";
                    dgInitialsSrch1.Columns[i].Visible = false;

                }
                if (i == 1)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "ID";
                    dgInitialsSrch1.Columns[i].Width = 60;

                }
                if (i == 2)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "FIN";
                    dgInitialsSrch1.Columns[i].Width = 125;
                }
                if (i == 3)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "STATUS";
                    dgInitialsSrch1.Columns[i].Width = 80;
                }
                if (i == 4)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "DESCRIPTION";
                    dgInitialsSrch1.Columns[i].Width = 245;
                }
                if (i == 5)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "LOCATION";
                    dgInitialsSrch1.Columns[i].Width = 238;
                }
                if (i == 6)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "ORGANIZATION";
                    dgInitialsSrch1.Columns[i].Width = 200;
                }
                if (i == 7)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "CONTACT";
                    dgInitialsSrch1.Columns[i].Width = 131;
                }
                if (i == 8)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "PHONE";
                    dgInitialsSrch1.Columns[i].Width = 80;
                }
                if (i == 9)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "PHONE2";
                    dgInitialsSrch1.Columns[i].Width = 80;
                }
            }

        }

     
        private DataTable GetDataTable()
        {
            string owner1 = "";
            string owner2 = "";
            string owner3 = "";

            string contact1 = "";
            string contact2 = "";
            string contact3 = "";

            string projdesc1 = "";
            string projdesc2 = "";
            string projdesc3 = "";

            string projloc1 = "";
            string projloc2 = "";
            string projloc3 = "";

            //get flag whether it is sample or presample
            if (rdSample.Checked)
                IsSearchSample = true;
            else
                IsSearchSample = false;

            //split owner, contact, projdescs and projlocs

            if (txtOwner.Text.Trim().Length > 0)
            {
                string[] owner = txtOwner.Text.Trim().Split(' ');
                if (owner.Length > 0)
                {
                    if (owner.Length == 3)
                    {
                        owner1 = owner[0];
                        owner2 = owner[1];
                        owner3 = owner[2];
                    }
                    else if (owner.Length == 2)
                    {
                        owner1 = owner[0];
                        owner2 = owner[1];
                    }
                    else
                        owner1 = owner[0];
                }
            }

            if (txtContact.Text.Trim().Length > 0)
            {
                string[] contact = txtContact.Text.Trim().Split(' ');
                if (contact.Length > 0)
                {
                    if (contact.Length == 3)
                    {
                        contact1 = contact[0];
                        contact2 = contact[1];
                        contact3 = contact[2];
                    }
                    else if (contact.Length == 2)
                    {
                        contact1 = contact[0];
                        contact2 = contact[1];
                    }
                    else
                        contact1 = contact[0];
                }
            }

            if (txtProjD.Text.Trim().Length > 0)
            {
                string[] projdescs = txtProjD.Text.Trim().Split(' ');
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

            if (txtProjL.Text.Trim().Length > 0)
            {
                string[] projlocs = txtProjL.Text.Trim().Split(' ');
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

            DataTable dt = dataObject.GetMFInitialData(id, Psu, Bpoid, owner1, owner2, owner3, contact1, contact2, contact3,
                           projdesc1, projdesc2, projdesc3, projloc1, projloc2, projloc3,
                           txtPhone1s.Text.Trim(), txtPhone2s.Text.Trim(), txtSeldate1.Text.Trim(),
                           cbSeldate.Text, txtSeldate2.Text.Trim(), IsSearchSample, chkExact.Checked);
            return dt;
        }

        /*****END SEARCH SECTION*****/

        /********BUTTONS************/

        private void btnNextInitial_Click(object sender, EventArgs e)
        {

            //returns to review screen
            if (btnNextInitial.Text == "PREVIOUS")
            {
                if ((chkComplete.Visible && !chkComplete.Checked) && (chkpending.Visible && !chkpending.Checked))
                {
                    string returnval =CallReviewCompDialg();
                    if (returnval == "Cancel")
                    {
                        return;
                    }
                    else
                    { SaveData(); }
                }
                else if ((chkComplete.Visible && chkComplete.Checked) || (chkpending.Visible && chkpending.Checked))
                {
                    SaveData();
                }
                if (anytxtmodified)
                   SaveData();
                      
                if (CallingForm != null)
                {
                    CallingForm.Show();
                }
                this.Close();
            }

            // Or show popup to enter next initial
            if (btnNextInitial.Text == "NEXT INITIAL")
            {
                frmMFInitPopup fMFInit = new frmMFInitPopup();
                string returnval = CallReviewCompDialg();
                if (returnval == "Yes")
                {
                    chkComplete.Checked = true;
                    SaveData();
                    if (notvalid == true)
                        return;
                }
                else if (returnval == "Cancel")
                    return;
                else 
                {
                    if (anytxtmodified)
                    {
                        SaveData();
                        if (notvalid == true)
                            return;
                    }
                }

                /*Show the popup*/
                fMFInit.Id = Id;
                fMFInit.ShowDialog();

                //Return user to the home screen if there are no more cases to process
                if (fMFInit.DialogResult == DialogResult.Abort && selflg)
                {
                    //if (fMFInit.Id == string.Empty)
                    //{
                        this.Close();
                        frmHome fH = new frmHome();
                        fH.Show();
                    //}

                }
               
                //Update psamp_history
                UpdateCaseAccessRecord();

                anytxtmodified = false;

                if (fMFInit.DialogResult == DialogResult.OK)
                {
                    selflg = fMFInit.selflg;

                    if (editable)
                    {
                        relsPresampleLock();
                        relsRespondentLock();
                    }

                    RemoveTxtChanged();

                    first = false;
                    rdSample.Checked = true;
                    IsSearchSample = true;

                    resetfields();

                    Id = fMFInit.Id;
                    id = Id;

                    DisplayPresampAddress(selflg);
                    
                    //In case, case didn't get locked
                    if (editable && selflg)
                    {
                        UpdatePresampleLock();
                        UpdateRespondentLock();
                    }

                    txtFin.Text = Psu + " " + Bpoid + " " + Sched;

                    SetTxtChanged();

                    compareflg = false;

                    //Return the focus to the first tab index

                    txtRBldgs.Focus();

                }
            }
        }

        private void btnNextCase_Click(object sender, EventArgs e)
        {
            if (CurrIndex == Idlist.Count - 1)
            {
                MessageBox.Show("You are at the last observation");
            }
            else
            {
                if (editable)
                {
                    if ((chkComplete.Visible && !chkComplete.Checked) && (chkpending.Visible && !chkpending.Checked))
                    {
                        string returnval = CallReviewCompDialg();
                        if (returnval == "Cancel")
                        {
                            return;
                        }
                        else
                          SaveData(); 
                    }
                    else if ((chkComplete.Visible && chkComplete.Checked) || (chkpending.Visible && chkpending.Checked))
                    {
                         SaveData();
                    }

                    if (anytxtmodified)
                    {
                        DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result2 == DialogResult.Yes)
                        {
                            SaveData();
                            if (notvalid == true)
                                return;
                        }
                    }
                }

                relsPresampleLock();
                relsRespondentLock();

                rdSample.Checked = true;
                IsSearchSample = true;

                //Update psamp_history
                UpdateCaseAccessRecord();

                CurrIndex = CurrIndex + 1;
                Id = Idlist[CurrIndex];
                txtCurrentrec.Text = (CurrIndex + 1).ToString();

                LoadFormFromReview();
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
                if (editable)
                {
                    if (anytxtmodified)
                    {
                        DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (result2 == DialogResult.Yes)
                        {
                            SaveData();
                            if (notvalid == true)
                                return;
                        }
                    }
                }

                relsPresampleLock();
                relsRespondentLock();

                rdSample.Checked = true;
                IsSearchSample = true;

                //Update psamp_history
                UpdateCaseAccessRecord();

                CurrIndex = CurrIndex - 1;
                Id = Idlist[CurrIndex];
                txtCurrentrec.Text = (CurrIndex + 1).ToString();

                LoadFormFromReview();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        //Restores the original values for the Contact fields

        private void btnRestore_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("This will restore the contact fields to the original values. Continue?", "Question", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                RestoreOriginalValues();
            }
        }

        //Displays original data when reset button is selected

        private void btnResetSearch_Click(object sender, EventArgs e)
        {
            ResetSearchParameters();
            SetHiddenSearchLbl();
            cbSeldate.SelectedIndex = 0;

            rdSample.Checked = true;
            IsSearchSample = true;

            dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
            FormatMfiData();
            btnCompare.Enabled = false;
            lblRecordCount.Text = " ";
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {

            MfCompareData compdata = new MfCompareData();
            frmMfComparePopup fmMFC = new frmMfComparePopup();
            fmMFC.StartPosition = FormStartPosition.CenterParent;

            int index = dgInitialsSrch1.CurrentRow.Index;

            int masterid = Convert.ToInt32(dgInitialsSrch1["MASTERID", index].Value);

            // Store Masterid in list for Page Up and Page Down
            List<int> Masteridlist = new List<int>();
            int cnt = 0;
            foreach (DataGridViewRow dr in dgInitialsSrch1.Rows)
            {
                //Get the masterid

                int val = Convert.ToInt32(dgInitialsSrch1["MASTERID", cnt].Value);

                Masteridlist.Add(val);
                cnt = cnt + 1;
            }

            //variable used to disable the duplicate 
            //button on the compare screen if the case is locked            

            fmMFC.flgIsLocked = flgIsLocked;

            fmMFC.fin1 = txtFin.Text;
            fmMFC.seldate1 = txtSelDate.Text;
            fmMFC.fipst1 = txtFipst.Text;
            fmMFC.status1 = cbStatCode.Text;
            fmMFC.frcde1 = txtFrCde.Text;
            fmMFC.newtc1 = cbNewtc.Text;
            fmMFC.respid1 = txtRespid.Text;
            fmMFC.obldgs1 = txtBldgs.Text;
            fmMFC.ounits1 = txtUnits.Text;
            fmMFC.rbldgs1 = txtRBldgs.Text;
            fmMFC.runits1 = txtRUnits.Text;
            fmMFC.projdesc1 = txtProjDesc.Text;
            fmMFC.projloc1 = txtProjLoc.Text;
            fmMFC.pcityst1 = txtPCitySt.Text;
            fmMFC.resporg1 = txtRespOrg.Text;
            fmMFC.factoff1 = txtFactOff.Text;
            fmMFC.othrresp1 = txtOthrResp.Text;
            fmMFC.addr1_1 = txtAddr1.Text;
            fmMFC.addr2_1 = txtAddr2.Text;
            fmMFC.addr3_1 = txtAddr3.Text;
            fmMFC.zip1 = txtZip.Text;
            fmMFC.respname1_1 = txtRespname.Text;
            fmMFC.phone1_1 = txtPhone.Text;
            fmMFC.ext1_1 = txtExt.Text;
            fmMFC.respname2_1 = txtRespname2.Text;
            fmMFC.phone2_1 = txtPhone2.Text;
            fmMFC.ext2_1 = txtExt2.Text;
            fmMFC.strtdate1 = txtStrtDate.Text;
            fmMFC.isSample = IsSearchSample;

            //Get the masterid for the selected row and pass to form
            fmMFC.Id = id;
            fmMFC.Masterid = masterid;
            fmMFC.Masteridnumlist = Masteridlist;
            fmMFC.CurrIndex = index;
            fmMFC.CallingForm = this;

            fmMFC.ShowDialog();  // show child

            //Sets the value for dupmaster and stat code when coming from the Compare popup

            if (fmMFC.DialogResult == DialogResult.OK && flgIsLocked == false)
            {
                dupmaster = fmMFC.Masterid;
                compareflg = fmMFC.compareflg;
                dupflag = "0";
                cbStatCode.Focus();
                cbStatCode.SelectedValue = 5;
            }
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
            {
                frmRspAuditPopup fRspAuditPopup = new frmRspAuditPopup(txtRespid.Text);
                fRspAuditPopup.ShowDialog();  //show child
            }
        }

        private string saveNewRespid;

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            saveNewRespid = newRespid;

            //Set the screen back to the values in the PRESAMPLE table
            presample = mfidata.GetPresample(id);
            DisplayPresampAddress(true);
            rdSample.Checked = true;
            IsSearchSample = true;

            //if the user added a respid then release the lock
            //for that respondent id when the screen is refreshed
            if (!String.IsNullOrEmpty(saveNewRespid))
            {
                string locked_by = "";
                GeneralDataFuctions.UpdateRespIDLock(saveNewRespid, locked_by);
            }

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                btnAudit.Enabled = true;
            else
                btnAudit.Enabled = false;
        }

        private void btnRespid_Click(object sender, EventArgs e)
        {

            frmChangeRespidPopup fCRP = new frmChangeRespidPopup();

            fCRP.Respid = txtRespid.Text.ToString();
            fCRP.Id = id;

            fCRP.ShowDialog();  //show child

            if (fCRP.DialogResult == DialogResult.OK)
            {
                txtRespid.Text = fCRP.NewRespid;

                if (txtRespid.Text != "")
                {
                    RespAddrData rad = new RespAddrData();
                    respaddress = rad.GetRespAddr(txtRespid.Text);

                    UpdateRespondentLock();
                    DisplayRespAddress(false);
                    anytxtmodified = true;

                    if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                        btnAudit.Enabled = true;
                }
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();
        }

        Bitmap memoryImage;

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
           
        }

        private void txtPCitySt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

       
    }
}
