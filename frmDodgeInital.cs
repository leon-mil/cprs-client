/******************************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
  
Program Name  : frmDodgeInitial.cs

Programmer    : Srini Natarajan
Creation Date : 01/24/2017

Parameters    : ID
Inputs        : 

Outputs       : DCPINITIAL data.

Description   : This screen outputs the data from the DCP Initial table for review.

Detail Design : 
Other         : 
Rev History   : See Below
******************************************************************************
Modified Date : October 12, 2017
Modified By   : Christine Zhang
Keyword       :
Change Request: CR 277
Description   :  Add buttons for Replacing Contact Information using
                 Owner, Architect, Engineer, Contractor or Owner2 from Factors
*****************************************************************************
Modified Date : Augest. 7, 2018 
Modified By   : Christine Zhang
Keyword       : 
Change Request: 
Description   : add cpraccess code INITIAL if the user is interviewer, othervise
                DATA_ENTRY
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
Modified Date :  01/14/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR3855
 Description   : Don't allow NPC edit status
***********************************************************************************
Modified Date :  07/22/2020
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : Add Pending check box
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using CprsBLL;
using CprsDAL;
using System.Linq;
using System.Globalization;

namespace Cprs
{
    public partial class frmDodgeInital : frmCprsParent
    {
        /****** public properties *******/
        /* Required */
        public string Id;
        public Form CallingForm = null;

        /* Optional */
        public List<string> Idlist = null;
        public int CurrIndex = 0;
        public TypeEditMode EditMode = TypeEditMode.Edit;
        public string EntryPnt;

        //------------------------------------
        private string id;
        private string Respid;
        
        private string coltec_code;
        string coltec_text = string.Empty;
        string statcode_text = string.Empty;

        private bool notvalid;
        private bool editable = false;

        private delegate void ShowPermissionDelegate(); 
        private delegate void ShowLockMessageDelegate();
        private string locked_by = String.Empty;
        private NameData namedata;
        private DodgeInitialData dodgeinitialdata;

        private bool btnResetClicked = false;

        private List<ProjMark> pmarklist = new List<ProjMark>();
        private List<RespMark> rmarklist = new List<RespMark>();
        RespAuditData radata = new RespAuditData();

        ReferralData refdata;

        /* global Variables*/
        private CsdaccessData csda;

        /*Textbox and Combobox to flag changes before saving data and auditing */
        TextBox txt;
        ComboBox cbobox;

        private string accesday = DateTime.Now.ToString("MMdd");
        private string accestms;
        private string accestme;
        
        private bool anytxtmodified = false;
        //add variables to keep the textbox content
        private string newval; //= string.Empty;
        private string oldval; //= string.Empty;
        private string varnme;
        private string masteridSaved;
        private string oldRespid;
        private string old1Respid;
        private string old2Respid;
        private string old3Respid;
        private bool bRespid = false;
        private string newStatCode;
        private string oldStatCode;
        private bool bStatcode = false;
        private string newStrtdater;
        private string oldStrtdater;
        private bool bStrtdater = false;
        private string newProjDesc;
        private string oldProjDesc;
        private bool bProjdesc = false;
        private string newProjLoc;
        private string oldProjLoc;
        private bool bProjloc = false;
        private string newProjCitySt;
        private string oldProjCitySt;
        private bool bProjcityst = false;
        private string newCollTec;
        private string oldCollTec;
        private bool bCollTec = false;
        private string newSurvey;
        private string oldSurvey;
        private bool bSurvey = false;
        private string oldNewTC;
        private string newNewTC;
        private bool bNewTC = false;
        private string newContractNum;
        private string oldContractNum;
        private bool bContractNum = false;
        
        private string newRespOrg;
        private string oldRespOrg;
        private bool bRespOrg = false;
        private string newFactOfficial;
        private string oldFactOfficial;
        private bool bFactOfficial = false;
        private string newOtherResp;
        private string oldOtherResp;
        private bool bOtherResp = false;
        private string newAddr1;
        private string oldAddr1;
        private bool bAddr1 = false;
        private string newAddr2;
        private string oldAddr2;
        private bool bAddr2 = false;
        private string newAddr3;
        private string oldAddr3;
        private bool bAddr3 = false;
        private string newZipCode;
        private string oldZipCode;
        private bool bZipCode = false;
        private string newPersontoCont;
        private string oldPersontoCont;
        private bool bPersontoCont = false;
        private string newPhoneNum;
        private string oldPhoneNum;
        private bool bPhoneNum = false;
        private string newExt;
        private string oldExt;
        private bool bExt = false;
        private string newFaxNumber;
        private string oldFaxNumber;
        private bool bFaxNumber = false;
        private string newPersontoCont2;
        private string oldPersontoCont2;
        private bool bPersontoCont2 = false;
        private string newPhoneNum2;
        private string oldPhoneNum2;
        private bool bPhoneNum2 = false;
        private string newExt2;
        private string oldExt2;
        private bool bExt2 = false;
        private string newSplnote;
        private string oldSplnote;
        private bool bSplnote = false;
        private string newEmailAddr;
        private string oldEmailAddr;
        private bool bEmailAddr = false;
        private string oldLag;
        private string newLag;
        private bool bLag;
        private string newWebAddr;
        private string oldWebAddr;
        private bool bWebAddr = false;

        private bool done = false;
       
        private bool LckdbyUsrElsewhere = false;
        private string user = UserInfo.UserName;
       
        DodgeInitial dodgeinitial;
        DodgeInitialrestore dodgeinitialrestore;
        private DodgeInitialData dataObject;
        private string returnedstring = string.Empty;

        private string currYearMon = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString("d2");

        public frmDodgeInital()
        {
            InitializeComponent();
        }

        Factor nameaddrfactor;
        NameAddrResp RespDetails;

        private string owner_stringSL = string.Empty;
        private string owner_stringNR = string.Empty;
        private string owner_stringFD = string.Empty;
        private string access_code = string.Empty;
        private void frmDodgeInital_Load(object sender, EventArgs e)
        {
            Show();
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.InitFD == "N" && UserInfo.InitNR == "N" && UserInfo.InitSL == "N")
            {
                MessageBox.Show("You do not have permission to access this area");
                this.Close();
                frmHome fH = new frmHome();
                fH.Show();
                return;
            }

            dataObject = new DodgeInitialData();
            dodgeinitial = new DodgeInitial();
            DataTable dtblDataSource = new DataTable();
            dtblDataSource.Columns.Add("Text");
            dtblDataSource.Columns.Add("Value");

            dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
            FormatdodgeIniDG();

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

            access_code = "DATA ENTRY";
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
                access_code = "INITIAL";
            
            namedata = new NameData();
            dodgeinitialdata = new DodgeInitialData();
            csda = new CsdaccessData();
            refdata = new ReferralData();

            chkpending.Visible = false;

            //if a EntryPoint value is present that means the form was accessed by the 
            //Review form and therefore displays the previous button to return to that form upon click
            if (EntryPnt == "REV")
            {
                /* If there is a list, set count boxes */
                if (Idlist != null)
                {
                    txtCurrentrec.Text = (CurrIndex + 1).ToString();
                    txtTotalrec.Text = Idlist.Count.ToString();
                }
                LoadFormFromReview();
            }
            else
            {
                LoadFormFromPopup();
            }
            
            lblReferral.Visible = refdata.CheckReferralExist(dodgeinitial.Id, dodgeinitial.Respid);
        }

        private void LoadFormFromPopup()
        {
            //Blank form loaded with a box for entering ID
            Show();
            
            GeneralDataFuctions.AddCpraccessData(access_code, "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData(access_code);
          
            SetButtonTxt();

            if (String.IsNullOrWhiteSpace(txtId.Text))
            {
                frmDCPInitPopup popup = new frmDCPInitPopup();
                DialogResult dialogresult = popup.ShowDialog();
                if (dialogresult == DialogResult.OK && popup.SelectedDCPId != "")
                {
                    Id = popup.SelectedDCPId;
                    if (popup.selflg) btnResetClicked = true;
                  //  dodgeinitial = DodgeInitialData.GetDodgeInitialData(Id);    // GetDodgeInitialData(Id);, viewcode
                    GetDataDodgeInitial();
                    txtNewtc.Focus();
                    if (popup.selflg) btnResetClicked = false;
                }
                else
                {
                    this.Close();
                    frmHome fH = new frmHome();
                    fH.Show();
                    return;
                }
                popup.Dispose();
            }
            else
            {
                if (dodgeinitial.Respid == "")
                {
                    Respid = txtId.Text; 
                }
                else
                {
                    Respid = txtRespid.Text; 
                }
               // dodgeinitial = DodgeInitialData.GetDodgeInitialData(Id);    // GetDodgeInitialData(Id);
                                                                            
                GetDataDodgeInitial();
                txtNewtc.Focus();
            }

            if (dodgeinitial.Respid == "")
            {
                Respid = txtId.Text;
            }
            else
            {
                Respid = txtRespid.Text;
            }

            gbListCount.Visible = false;
            btnPrevCase.Visible = false;
            btnNextCase.Visible = false;
            ResetAuditVariables();
        }

        private void LoadFormFromReview()
        {
            id = Id;
            LckdbyUsrElsewhere = false;
            SetButtonTxt();

            GeneralDataFuctions.AddCpraccessData(access_code, "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData(access_code);

            if (!String.IsNullOrEmpty(txtRespid.Text.Trim()))
                { btnAudit.Enabled = true; }
            else
                { btnAudit.Enabled = false; }

            btnNextInitial.Text = "PREVIOUS";
            resetfields();

            GetDataDodgeInitial();
            txtNewtc.Focus();
            if (dodgeinitial.Respid == null || dodgeinitial.Respid == "")
            { Respid = Id; }
            else
            { Respid = dodgeinitial.Respid; }

            ResetAuditVariables();
            gbListCount.Visible = true;
            btnPrevCase.Visible = true;
            btnNextCase.Visible = true;
        }

        private void EnableEdits()
        {
            //Enable the fields for editing
            btnRespid.Enabled = true;
            btnRefresh.Enabled = true;
           
            btnRestore.Enabled = true;
            cbStatCode.Enabled = true;
            cboSurvey.Enabled = true;
            cboSurvey.BackColor = Color.White;
            chkComplete.Enabled = true;
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                txtNewtc.Visible = true;
                txtNewtc.Enabled = false;
                
                cbStatCode.Enabled = false;
                if (dodgeinitial.Worked == "0")
                {
                    chkNeedFurRev.Visible = false;
                    chkNeedFurRev.Enabled = false;
                    chkNeedFurRev.Checked = false;
                    chkComplete.Visible = true;
                    chkComplete.Enabled = true;
                    chkpending.Visible = true;
                    chkpending.Enabled = true;
                }
                else if (dodgeinitial.Worked == "1")
                {
                    if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4")
                    {
                        chkpending.Visible = false;
                        chkComplete.Visible = false;
                    }
                    else
                    {
                        chkComplete.Visible = true;
                        chkpending.Visible = true;
                    }
                    chkNeedFurRev.Visible = false;
                }
                else if (dodgeinitial.Worked == "2")
                {
                    chkNeedFurRev.Visible = false;
                    chkComplete.Visible = false;
                    chkpending.Visible = false;
                    chkpending.Enabled = false;
                }
                else if (dodgeinitial.Worked == "3")
                {
                    chkNeedFurRev.Visible = false;
                    chkComplete.Visible = true;
                    chkComplete.Enabled = false;
                    chkpending.Visible = true;
                    chkpending.Enabled = true;
                   
                }

                txtStrtDater.ReadOnly = false;
                txtStrtDater.BackColor = Color.White;
                
            }
            else
            {
                txtNewtc.Visible = true;
                txtNewtc.Enabled = true;
                btnNewtc.Visible = true;
                btnNewtc.Enabled = true;
                if (dodgeinitial.HQWorked == "0" )
                {
                    chkNeedFurRev.Visible = true;
                    chkNeedFurRev.Enabled = true;
                    chkNeedFurRev.Checked = false;
                    chkComplete.Visible = true;
                }
                else
                { chkNeedFurRev.Visible = false; }
                if(dodgeinitial.HQWorked == "2")
                {
                    chkNeedFurRev.Visible = false;
                    chkComplete.Visible = false;
                }
                txtStrtDater.ReadOnly = true;
                txtStrtDater.BackColor = Color.LightGray;
                
            }
            
            txtContractNum.ReadOnly = false;
            txtProjDesc.ReadOnly = false;
            txtProjLoc.ReadOnly = false;
            txtPCitySt.ReadOnly = false;
            txtFactOff.ReadOnly = false;
            txtOthrResp.ReadOnly = false;
            txtAddr1.ReadOnly = false;
            txtAddr2.ReadOnly = false;
            txtAddr3.ReadOnly = false;
            txtZip.ReadOnly = false;
            txtRespOrg.ReadOnly = false;
            txtRespname.ReadOnly = false;
            txtRespname2.ReadOnly = false;
            txtRespnote.ReadOnly = false;
            txtPhone.ReadOnly = false;
            txtPhone2.ReadOnly = false;
            txtExt1.ReadOnly = false;
            txtExt2.ReadOnly = false;
            txtFax.ReadOnly = false;
            txtEmail.ReadOnly = false;
            txtWebUrl.ReadOnly = false;
            
            txtStrtDate.ReadOnly = true;
           
            txtFlagStrtDate.ReadOnly = true;
           
            cboCollTec.Enabled = true;
            txtLag.Enabled = true;

            //set source buttons
            SetReplaceButtons();
        }

        private void DisableEdits()
        {
            //disable the fields. User cannot edit.
            txtRespid.ReadOnly = true;
            btnRespid.Enabled = false;
            btnReplaceC.Enabled = false;
            btnReplaceO.Enabled = false;
            btnReplaceO2.Enabled = false;
            btnReplaceA.Enabled = false;
            btnReplaceE.Enabled = false;
            btnRestore.Enabled = false;
            btnRefresh.Enabled = false;
            cboSurvey.Enabled = false;
            cbStatCode.Enabled = false;
            txtNewtc.ReadOnly = true;
            btnNewtc.Enabled = false;
            txtContractNum.ReadOnly = true;
            txtProjDesc.ReadOnly = true;
            txtProjLoc.ReadOnly = true;
            txtPCitySt.ReadOnly = true;
            txtFactOff.ReadOnly = true;
            txtOthrResp.ReadOnly = true;
            txtAddr1.ReadOnly = true;
            txtAddr2.ReadOnly = true;
            txtAddr3.ReadOnly = true;
            txtZip.ReadOnly = true;
            txtRespOrg.ReadOnly = true;
            txtRespname.ReadOnly = true;
            txtRespname2.ReadOnly = true;
            txtRespnote.ReadOnly = true;
            txtPhone.ReadOnly = true;
            txtPhone2.ReadOnly = true;
            txtExt1.ReadOnly = true;
            txtExt1.BackColor = Color.LightGray;
            txtExt2.ReadOnly = true;
            txtFax.ReadOnly = true;
            txtEmail.ReadOnly = true;
            txtWebUrl.ReadOnly = true;
            txtStrtDater.ReadOnly = true;
            txtStrtDater.BackColor = Color.LightGray;
            txtStrtDate.ReadOnly = true;
            txtFlagStrtDate.ReadOnly = true;
            cboCollTec.Enabled = false;
            txtLag.Enabled = false;
            txtColhist.ReadOnly = true;
            txtZone.ReadOnly = true;
            chkComplete.Enabled = false;
            chkNeedFurRev.Enabled = false;
            if (chkpending.Visible)
                chkpending.Enabled = false;
        }

        //This code populates row header of datagrid dynamically
        private void dgInitialsSrch1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgInitialsSrch1.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 1, e.RowBounds.Location.Y + 4);
            }
        }

        private void FormatdodgeIniDG()
        {
            dgInitialsSrch1.RowHeadersVisible = true;  // set it to false if not needed
            dgInitialsSrch1.ShowCellToolTips = false;

            SetHeaderCellValue();

            for (int i = 0; i < dgInitialsSrch1.ColumnCount; i++)
            {

                //Set columns Header Text

                if (i == 0)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "RESPID";
                    dgInitialsSrch1.Columns[i].Width = 95;
                }
                if (i == 1)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "ORGANIZATION";
                    dgInitialsSrch1.Columns[i].Width = 320;
                }
                if (i == 2)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "CONTACT";
                    dgInitialsSrch1.Columns[i].Width = 320;
                }
                if (i == 3)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "PHONE1";
                    dgInitialsSrch1.Columns[i].Width = 200;
                }
                if (i == 4)
                {
                    dgInitialsSrch1.Columns[i].HeaderText = "PHONE2";
                    dgInitialsSrch1.Columns[i].Width = 205;
                }
            }
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

        private void GetDataDodgeInitial()
        {
            RemoveTxtChanged();
            dodgeinitial = DodgeInitialData.GetDodgeInitialData(Id);    // GetDodgeInitialData(Id);

            string review_status = dodgeinitial.Worked.Trim();
            txtId.Text = dodgeinitial.Id;
            masteridSaved = dodgeinitial.Masterid.ToString();
            txtRespid.Text = dodgeinitial.Respid;
            txtSelDate.Text = dodgeinitial.Seldate;
            txtSelval.Text = Convert.ToInt32(dodgeinitial.Selvalue).ToString("#,#");
            string statCodeConv = GetDisplayStatCodeText(dodgeinitial.Statuscode);
            cbStatCode.Text = statCodeConv;

            //Coltec should display with description with only the code saved.
            coltec_code = dodgeinitial.Coltec;
            cboCollTec.SelectedItem = GetDisplayColtecText(coltec_code); //resp.Coltec);
            cboCollTec.Text = coltec_text;

            txtColhist.Text = dodgeinitial.Colhist;
            txtFipst.Text = dodgeinitial.Fipstate;
            txtCnty.Text = dodgeinitial.Dodgecou;
            cboSurvey.Text = dodgeinitial.Survey; //Owner
            txtNewtc.Text = dodgeinitial.Newtc;
            txtMrn.Text = dodgeinitial.Mrn;
            txtFin.Text = dodgeinitial.Fin;     //Frame ID
            txtContractNum.Text = dodgeinitial.Contract;
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                txtStrtDater.Visible = true;
                txtStrtDater.Enabled = true;
                txtStrtDate.Visible = false;
                txtFlagStrtDate.Visible = false;
                lblEdited.Visible = false;
                txtStrtDater.Text = dodgeinitial.Strtdater;
                txtStrtDate.Text = dodgeinitial.Strtdate;
            }
            else
            {
                txtStrtDater.Visible = true;

                txtStrtDate.Visible = true;
               
                txtFlagStrtDate.Visible = true;
                
                txtFlagStrtDate.Text = dodgeinitial.Flagstrtdate;
                txtStrtDater.Text = dodgeinitial.Strtdater;
                txtStrtDate.Text = dodgeinitial.Strtdate;
                lblEdited.Visible = true;
            }
            txtProjDesc.Text = dodgeinitial.ProjDesc;
            txtProjLoc.Text = dodgeinitial.Projloc;
            txtLag.Text = dodgeinitial.Lag;
            if(dodgeinitial.Lag == "0")
            { txtLag.BackColor = Color.White; }
            else
            { txtLag.BackColor = Color.Yellow; }
            txtPCitySt.Text = dodgeinitial.Pcityst;
            txtZone.Text = dodgeinitial.Timezone;
            txtRespOrg.Text = dodgeinitial.Resporg;
            //Respondent info
            txtRespname.Text = dodgeinitial.Respname;
            txtRespname2.Text = dodgeinitial.Respname2;
            txtPhone.Text = dodgeinitial.Phone;
            txtPhone2.Text = dodgeinitial.Phone2;
            txtExt1.Text = dodgeinitial.Ext;
            txtExt2.Text = dodgeinitial.Ext2;
            txtFax.Text = dodgeinitial.Fax;
            txtFactOff.Text = dodgeinitial.Factoff; //Factor Official
            txtOthrResp.Text = dodgeinitial.Othrresp;
            txtAddr1.Text = dodgeinitial.Addr1;
            txtAddr2.Text = dodgeinitial.Addr2;
            txtRespnote.Text = dodgeinitial.Respnote; //special note
            txtEmail.Text = dodgeinitial.Email;
            txtAddr3.Text = dodgeinitial.Addr3;
            txtZip.Text = dodgeinitial.Zip;
            txtWebUrl.Text = dodgeinitial.Weburl;

            lblReferral.Visible = refdata.CheckReferralExist(dodgeinitial.Id, dodgeinitial.Respid);

            string rst2 = string.Empty;
            if (dodgeinitial.Addr3.TrimEnd() != "")
            {
                rst2 = dodgeinitial.Addr3.Substring(dodgeinitial.Addr3.TrimEnd().Length - 2);
            }

            txtZone.Text = GeneralDataFuctions.GetTimezone(rst2);

            //Time that case was entered
            accestms = DateTime.Now.ToString("HHmmss");

            chkNeedFurRev.Visible = false;
            chkComplete.Visible = false;
            chkComplete.Checked = false;
            chkpending.Visible = false;
            chkpending.Checked = false;
           
            if (dodgeinitial.Worked != "2" && UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4")
            {
                chkNeedFurRev.Visible = false;
                if (dodgeinitial.Worked == "0" || dodgeinitial.Worked == "3")
                {
                    chkComplete.Visible = true;
                    chkComplete.Enabled = true;
                    chkpending.Visible = true;
                    chkpending.Enabled = true;
                }
                else if (dodgeinitial.Worked == "1")
                {
                    chkComplete.Visible = false;
                    chkComplete.Enabled = false;
                    chkpending.Visible = false;
                    chkpending.Enabled = false;
                }
                else if (dodgeinitial.Worked == "3")
                {
                    chkComplete.Visible = true;
                    chkComplete.Enabled = false;
                    chkpending.Visible = true;
                    chkpending.Enabled = true;
                    chkpending.Checked = true;
                }
            }
            else if (dodgeinitial.Worked != "2"  && (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "5"))
            {
                chkComplete.Visible = true;
                chkComplete.Enabled = true;
                chkpending.Visible = true;
                chkpending.Enabled = true;
                chkNeedFurRev.Visible = false;

                if (dodgeinitial.Worked == "3")
                {
                    chkpending.Checked = true;
                    chkComplete.Enabled = false;
                }
                
            }
           
           else if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCLead && UserInfo.GroupCode != EnumGroups.NPCInterviewer)
            {
                if (dodgeinitial.HQWorked == "0")
                {
                    chkNeedFurRev.Visible = true;
                    chkNeedFurRev.Checked = false;
                    chkComplete.Visible = true;
                    chkComplete.Checked = false;
                    chkpending.Visible = false;
                    chkpending.Checked = false;
                }
                if (dodgeinitial.HQWorked == "1")
                {
                    chkNeedFurRev.Visible = false;
                    chkComplete.Checked = false;
                    chkComplete.Visible = true;
                    chkpending.Visible = false;
                    chkpending.Checked = false;
                }
                if (dodgeinitial.HQWorked == "2")
                {
                    chkNeedFurRev.Visible = false;
                    chkComplete.Visible = false;
                    chkpending.Visible = false;
                    chkpending.Enabled = false;
                }
            }
           
            
            //get factors
            nameaddrfactor = SourceData.GetFactor(dodgeinitial.Masterid);

            //check for lock by someone
            if (txtRespid.Text == "")
            {
                Respid = Id;
            }
            else
            {
                Respid = dodgeinitial.Respid;
            }
            editable = true;
            if (!btnResetClicked)
            {
                locked_by = GeneralDataFuctions.ChkRespIDIsLocked(Respid);
                if (String.IsNullOrEmpty(locked_by))
                {
                    EnableEdits();
                    lblLockedBy.Text = "";
                    lblLockedBy.Visible = false;
                    GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
                }
                else
                {
                    lblLockedBy.Visible = true;
                    lblLockedBy.Text = "LOCKED";
                    editable = false;
                    DisableEdits();
                    if (Convert.ToString(locked_by) == UserInfo.UserName)
                        LckdbyUsrElsewhere = true;
                    
                    BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
                }
            }

            //Check to see if there is data in factors 7. If not disable the REPLACE button.
            string FINCheck = dodgeinitial.Fin.Substring(0, 2);
            if (FINCheck == "66")
            {
                //Gray out SLIP and SOURCE buttons.
                btnSlip.Enabled = false;
                btnSource.Enabled = false;
            }
            else
            {
                if (!GeneralDataFuctions.CheckDodgeSlip(dodgeinitial.Masterid))
                    btnSlip.Enabled = false;
                else
                    btnSlip.Enabled = true;

                //Disable Slip button if there is no Dodge info for this project.
                btnSource.Enabled = true;
            }
            ClearSearchFlds();
            chkWorkStatus();
            SetTxtChanged();
        }

        //Set up the txt_txtChange Event to be called after Form Initialization
        //Add the control's handler to the event using the += operator to
        //raise the text changed event
        private void SetTxtChanged()
        {
            txtRespid.TextChanged += new EventHandler(txt_TextChanged);
            cbStatCode.TextChanged += new EventHandler(txt_TextChanged);
            txtStrtDate.TextChanged += new EventHandler(txt_TextChanged);
            txtNewtc.TextChanged += new EventHandler(txt_TextChanged);
            txtContractNum.TextChanged += new EventHandler(txt_TextChanged);
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
            txtExt1.TextChanged += new EventHandler(txt_TextChanged);
            txtFax.TextChanged += new EventHandler(txt_TextChanged);
            txtRespname2.TextChanged += new EventHandler(txt_TextChanged);
            txtPhone2.TextChanged += new EventHandler(txt_TextChanged);
            txtExt2.TextChanged += new EventHandler(txt_TextChanged);
            txtRespnote.TextChanged += new EventHandler(txt_TextChanged);
            txtEmail.TextChanged += new EventHandler(txt_TextChanged);
            txtWebUrl.TextChanged += new EventHandler(txt_TextChanged);
            chkComplete.CheckedChanged += new EventHandler(txt_TextChanged);
            chkpending.CheckedChanged += new EventHandler(txt_TextChanged);
            chkNeedFurRev.CheckedChanged += new EventHandler(txt_TextChanged);
            cboCollTec.TextChanged += new EventHandler(txt_TextChanged);
            cboSurvey.TextChanged += new EventHandler(txt_TextChanged);
        }

        //Clear the txt_txtChange Event due to Next Respid Form Initialization
        //Remove the controls from delegate using the -= operator
        private void RemoveTxtChanged()
        {
            txtRespid.TextChanged -= new EventHandler(txt_TextChanged);
            cbStatCode.TextChanged -= new EventHandler(txt_TextChanged);
            txtStrtDate.TextChanged -= new EventHandler(txt_TextChanged);
            txtNewtc.TextChanged -= new EventHandler(txt_TextChanged);
            txtContractNum.TextChanged -= new EventHandler(txt_TextChanged);
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
            txtExt1.TextChanged -= new EventHandler(txt_TextChanged);
            txtFax.TextChanged -= new EventHandler(txt_TextChanged);
            txtRespname2.TextChanged -= new EventHandler(txt_TextChanged);
            txtPhone2.TextChanged -= new EventHandler(txt_TextChanged);
            txtExt2.TextChanged -= new EventHandler(txt_TextChanged);
            txtRespnote.TextChanged -= new EventHandler(txt_TextChanged);
            txtEmail.TextChanged -= new EventHandler(txt_TextChanged);
            txtWebUrl.TextChanged -= new EventHandler(txt_TextChanged);
            chkComplete.CheckedChanged -= new EventHandler(txt_TextChanged);
            chkpending.CheckedChanged -= new EventHandler(txt_TextChanged);
            chkNeedFurRev.CheckedChanged -= new EventHandler(txt_TextChanged);
            cboCollTec.TextChanged -= new EventHandler(txt_TextChanged);
            cboSurvey.TextChanged -= new EventHandler(txt_TextChanged);
        }

        private void resetfields()
        {
            RemoveTxtChanged();
            anytxtmodified = false;
            txtFin.Text = "";
            txtSelDate.Text = "";
            txtSelval.Text = "";
            txtFipst.Text = "";
            txtCnty.Text = "";
            cbStatCode.Text = "";
            txtNewtc.Text = "";
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
            txtExt1.Text = "";
            txtExt2.Text = "";
            txtFax.Text = "";
            txtEmail.Text = "";
            txtWebUrl.Text = "";
            cboSurvey.TextChanged -= new EventHandler(txt_TextChanged);
            cboSurvey.Text = "";
            chkComplete.Checked = false;
            chkpending.Checked = false;
            chkNeedFurRev.Checked = false;
            lblLockedBy.Visible = false;
            newval = string.Empty;
            oldval = string.Empty;
           
            //reset search criteria fields
            txtOwner.Text = "";
            txtContact.Text = "";
            txtPhone1s.Text = "";
            txtPhone2s.Text = "";
            chkExact.Checked = false;
            txtSelDate.Text = "";
            txtSelval.Text = "";
            lblRecordCount.Text = " ";
            returnedstring = "";
            SetTxtChanged();
        }
        
        private string Reviewtext = null;
        private void chkWorkStatus()
        {
            UserInfoData data_object = new UserInfoData();
            //Work Status. 
            //If user is NPC Manager/NPC Lead/NPC Interviewer
            Reviewtext = "";
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                Reviewtext = dodgeinitial.Worked;
            }
            else
            {
                Reviewtext = dodgeinitial.HQWorked;
            }

            if (Reviewtext == "0") 
            {
                txtReview.Text = "NOT STARTED";
            }
            else if (Reviewtext == "1")
            {
                txtReview.Text = "REVIEWED";
            }
            else if (Reviewtext == "2")
            {
                txtReview.Text = "FINISHED";
            }
            else if (Reviewtext == "3")
            {
                txtReview.Text = "PENDING";
            }
            else
            {
                txtReview.Text = "";
            }
        }

        //from coltec code to get display text
        private string GetDisplayColtecText(string coltec_code)
        {
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

        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:
                    btnTFU.Text = "C-700";
                    break;
                case EnumGroups.HQManager:
                    btnTFU.Text = "C-700";
                    break;
                case EnumGroups.HQAnalyst:
                    btnTFU.Text = "C-700";
                    break;
                case EnumGroups.NPCManager:
                    btnTFU.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:
                    btnTFU.Text = "TFU";
                    break;
                case EnumGroups.NPCInterviewer:
                    btnTFU.Text = "TFU";
                    break;
                case EnumGroups.HQSupport:
                    btnTFU.Text = "C-700";
                    break;
                case EnumGroups.HQMathStat:
                    btnTFU.Text = "C-700";
                    break;
                case EnumGroups.HQTester:
                    btnTFU.Text = "C-700";
                    break;
            }
        }

        // ******************************************************************
         private void btnNewtc_Click(object sender, EventArgs e)
         {
             frmNewtcSel popup = new frmNewtcSel();
             popup.CaseOwner = cboSurvey.Text;
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
                popup.ViewOnly = true;
            else
                popup.ViewOnly = false;

             DialogResult dialogresult = popup.ShowDialog();
             if (dialogresult == DialogResult.OK)
             {
                RemoveTxtChanged();
                string old_tc = txtNewtc.Text;
                txtNewtc.Text = popup.SelectedNewtc;
                if (!ValidateNewtc())
                {
                    MessageBox.Show("The Newtc value entered is invalid.");
                    txtNewtc.Text = old_tc;
                }
                else
                {
                    bNewTC = true;
                    SetTxtChanged();
                }
             }
             popup.Dispose();
         }

        // ********************************************************************
        
        private void btnRespid_Click(object sender, EventArgs e)
         {
            DateTime prgdtm = DateTime.Now;
            string usrnme = Environment.UserName;
            string id = txtId.Text;
            string newflag = "";
            string oldflag = "";
            string masterid = dodgeinitial.Masterid.ToString();
            //Open a popup window depending on the text in Respid textbox.
            frmRespidEditPopup popup = new frmRespidEditPopup(txtRespid.Text, Id);
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK && popup.newRespid != "")
            {
                bRespid = true;
                string newRespid = popup.newRespid;

                //newRespid is exist respid + exist
                if (newRespid.Length > 7)
                {
                    //respid was valid and not locked by another user so update the record after locking it.
                    old1Respid = "";
                    if (txtRespid.Text == "")
                        old1Respid = Id;
                    else
                         old1Respid = txtRespid.Text;

                    Respid = newRespid.Substring(0, 7);//txtRespid.Text;
                    
                    txtRespid.ReadOnly = false;
                    txtRespid.Text = Respid;
                    txtRespid.ReadOnly = true;
                    
                    GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
                    GeneralDataFuctions.UpdateRespIDLock(old1Respid, "");

                  
                        //Get all the field values like resporg and respname from respondent table and display
                        // on the screen overwriting the existing information.
                        RespDetails = NameData.RespDataRead(Respid.ToString());
                        txtRespOrg.Text = RespDetails.Resporg;
                        txtRespname.Text = RespDetails.Respname;
                        txtRespname2.Text = RespDetails.Respname2;
                        txtOthrResp.Text = RespDetails.Othrresp;
                        txtAddr1.Text = RespDetails.Addr1;
                        txtAddr2.Text = RespDetails.Addr2;
                        txtAddr3.Text = RespDetails.Addr3;
                        PopulateRstate();
                        txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
                        txtZip.Text = RespDetails.Zip;
                        txtPhone.Text = RespDetails.Phone;
                        txtPhone2.Text = RespDetails.Phone2;
                        txtExt1.Text = RespDetails.Ext;
                        txtExt2.Text = RespDetails.Ext2;
                        txtFax.Text = RespDetails.Fax;
                        txtFactOff.Text = RespDetails.Factoff;
                        txtOthrResp.Text = RespDetails.Othrresp;
                        txtRespnote.Text = RespDetails.Respnote;
                        txtEmail.Text = RespDetails.Email;
                        txtWebUrl.Text = RespDetails.Weburl;
                        txtColhist.Text = RespDetails.Colhist;
                        cboCollTec.SelectedItem = GetDisplayColtecText(RespDetails.Coltec);
                        cboCollTec.Text = coltec_text;

                        //only update sample table
                        namedata.updateRespid4ID(Id, Respid);
                        oldval = old1Respid;
                        if (txtRespid.Text == "")
                        { newval = dodgeinitial.Id; }
                        else
                        { newval = txtRespid.Text; }
                        varnme = "RESPID";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                        
                        ChkNumProjForRespid(old1Respid);

                    ResetAuditRespVariables();
                    updateDodgeInitialObject(RespDetails);

                }
                else if (newRespid == "New")
                {
                    old2Respid = "";
                    if (txtRespid.Text == "")
                        old2Respid = Id;
                    else
                        old2Respid = txtRespid.Text; 
                    
                    //string relsUsrName = "";
                    GeneralDataFuctions.UpdateRespIDLock(old2Respid, "");
                    
                    string newSelRespid = NameData.GetLastValidRespid();
                    int intRespid = Convert.ToInt32(newSelRespid) + 1;
                    newSelRespid = intRespid.ToString().PadLeft(7, '0');
                    this.txtRespid.TextChanged -= new System.EventHandler(this.txt_TextChanged);
                    txtRespid.ReadOnly = false;
                    txtRespid.Text = newSelRespid;
                    Respid = txtRespid.Text;
                    
                    txtRespid.ReadOnly = true;
                    PopulateRstate();
                    namedata.InsertNewRespid(newSelRespid, txtRespOrg.Text, txtRespname.Text, txtAddr1.Text, txtAddr2.Text, txtAddr3.Text, txtZip.Text, txtPhone.Text, txtExt1.Text,
                                                txtFax.Text, txtFactOff.Text, txtOthrResp.Text, txtRespnote.Text, txtEmail.Text, txtWebUrl.Text, rstatevalue, "0",
                                                UserInfo.UserName, "", cboCollTec.Text.Substring(0, 1), txtColhist.Text, txtRespname2.Text, txtPhone2.Text, txtExt2.Text, UserInfo.UserName, "");
                    namedata.updateRespid4ID(Id, newSelRespid);
                    
                    bRespid = true;
                       
                    oldval = old2Respid;
                    if (txtRespid.Text == "")
                    { newval = dodgeinitial.Id; }
                    else
                    { newval = txtRespid.Text; }
                    varnme = "RESPID";
                    NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                    
                    ChkNumProjForRespid(old2Respid);

                    ResetAuditRespVariables();
                }
                else
                {
                    old3Respid = "";
                    if (txtRespid.Text == "")
                        old3Respid = Id; 
                    else
                        old3Respid = txtRespid.Text; 

                        //delete old respid...
                        //PopulateRstate();
                        if (!GeneralDataFuctions.ChkRespid(Id.ToString()))
                        {
                            PopulateRstate();
                            namedata.InsertNewRespid(Id, txtRespOrg.Text, txtRespname.Text, txtAddr1.Text, txtAddr2.Text, txtAddr3.Text, txtZip.Text, txtPhone.Text, txtExt1.Text,
                                                     txtFax.Text, txtFactOff.Text, txtOthrResp.Text, txtRespnote.Text, txtEmail.Text, txtWebUrl.Text, rstatevalue, "0",
                                                     UserInfo.UserName, "", cboCollTec.Text.Substring(0, 1), txtColhist.Text, txtRespname2.Text, txtPhone2.Text, txtExt2.Text, UserInfo.UserName, "");
                        }
                        //update Sample table
                        namedata.updateRespid4ID(Id, Id);
                        Respid = Id;
                        
                        txtRespid.ReadOnly = false;
                        txtRespid.Text = "";
                        
                        txtRespid.ReadOnly = false;
                        GeneralDataFuctions.UpdateRespIDLock(old3Respid, "");

                        GeneralDataFuctions.UpdateRespIDLock(Id, UserInfo.UserName);
                        
                        //Audit the changes
                        oldval = old3Respid;
                        if (txtRespid.Text == "")
                        {
                            newval = dodgeinitial.Id;
                           
                        }
                        else
                        { newval = txtRespid.Text; }
                        varnme = "RESPID";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);

                        ChkNumProjForRespid(old3Respid);
                        ResetAuditRespVariables();
                    
                }
            }
            popup.Dispose();
        }

        private void updateDodgeInitialObject(NameAddrResp respdetail)
        {
            dodgeinitial.Resporg = respdetail.Resporg;
            dodgeinitial.Respname = respdetail.Respname;
            dodgeinitial.Respname2 = respdetail.Respname2;
            dodgeinitial.Othrresp = respdetail.Othrresp;
            dodgeinitial.Addr1 = respdetail.Addr1;
            dodgeinitial.Addr2 = respdetail.Addr2;
            dodgeinitial.Addr3 = respdetail.Addr3;
            
            dodgeinitial.Zip= respdetail.Zip;
            dodgeinitial.Phone = respdetail.Phone;
            dodgeinitial.Phone2 = respdetail.Phone2;
            dodgeinitial.Ext = respdetail.Ext;
            dodgeinitial.Ext2 = respdetail.Ext2;
            dodgeinitial.Fax = respdetail.Fax;
            dodgeinitial.Factoff = respdetail.Factoff;
            dodgeinitial.Othrresp = respdetail.Othrresp;
            dodgeinitial.Respnote = respdetail.Respnote;
            dodgeinitial.Email = respdetail.Email;
            dodgeinitial.Weburl = respdetail.Weburl;
            dodgeinitial.Colhist = respdetail.Colhist;
            dodgeinitial.Coltec = respdetail.Coltec;
        }

        //Check # of projects associated with respid and delete respid in Respondent table if there are none left in Sample table
        private void ChkNumProjForRespid(string Origresp)
        {
            string numRespid = NameData.ChkRespProjnum(Origresp);
            if (Convert.ToInt32(numRespid) == 0)
            {
                namedata.DeleteRespid(Origresp);
            }
        }

        private string PopulateRstate()
        {
            if (txtAddr3.Text.Trim() != "")
            {
                if (txtAddr3.TextLength < 6)
                {
                    MessageBox.Show("Address is invalid.");
                    txtAddr3.Focus();
                    txtAddr3.Text = dodgeinitial.Addr3;
                    return rstatevalue = "xx";
                }
                else
                {
                    rstate = GeneralData.Right(txtAddr3.Text, 6);
                    if (rstate == "CANADA")
                    {
                        //remove blank spaces
                        string noblanks = txtAddr3.Text.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                        string canadastate = GeneralData.Right(noblanks, 8);
                        rstatevalue = canadastate.Substring(0, 2);
                    }
                    else
                    {
                        rstatevalue = GeneralData.Right(txtAddr3.Text.TrimEnd(), 2);
                    }
                    return rstatevalue;
                }
            }
            return rstatevalue;
        }
        
        private string rstatevalue= "";
       
        private string rstate;
        //validate City State field
        private bool ValidateCityState()
        {
            bool result = true;

            if (txtAddr3.Text.Trim() == "") return result;

            //check valid state
            if (GeneralFunctions.HasSpecialCharsInCityState(txtAddr3.Text.Trim()))
            {
                MessageBox.Show("City/State is invalid.");
                txtAddr3.Focus();
                txtAddr3.Text = dodgeinitial.Addr3;
                result = false;
            }
            else
            {
                string[] words = GeneralFunctions.SplitWords(txtAddr3.Text.Trim());
                int num_word = words.Count();
                string rst = string.Empty;
                if (num_word < 2)
                {
                    MessageBox.Show("City/State is invalid.");
                    txtAddr3.Focus();
                    txtAddr3.Text = dodgeinitial.Addr3;
                    result = false;
                }
                else
                {
                    if (words[num_word - 1] == "CANADA")
                    {
                        rst = words[num_word - 2];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, true))
                        {
                            MessageBox.Show("State/Province is invalid.");
                            txtAddr3.Focus();
                            txtAddr3.Text = dodgeinitial.Addr3;
                            result = false;
                        }
                        else rstate = rst;
                    }
                    else
                    {
                        rst = words[num_word - 1];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, false))
                        {
                            MessageBox.Show("State/Province is invalid.");
                            txtAddr3.Focus();
                            txtAddr3.Text = dodgeinitial.Addr3;
                            string rst2 = dodgeinitial.Addr3.Substring(dodgeinitial.Addr3.Length - 2);
                            txtZone.Text = GeneralDataFuctions.GetTimezone(rst2);
                            result = false;
                        }
                        else rstate = rst;
                    }
                }
            }
            return result;
        }

        //Verify Form closing 
        public override bool VerifyFormClosing()
        {
            bool can_close;
            can_close = true;
            done = true;

            if (anytxtmodified)
            {
                if (!ValidateData())
                    return false;

                DataChanged();
            }

            if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
            {
                if (dodgeinitial.HQWorked != "2") //&& dodgeinitial.HQWorked != "2")
                {
                    if ((chkComplete.Checked == false && chkComplete.Visible) && (chkNeedFurRev.Visible == true && chkNeedFurRev.Checked == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        {
                            return false;
                        }
                        if (returnedstring == "Yes")
                        {
                            chkCompleteUpdate();
                        }
                    }

                    if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        {
                            return false;
                        }
                        if (returnedstring == "Yes")
                        {
                            chkCompleteUpdate();
                        }
                    }
                }
            }

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                if (dodgeinitial.Worked != "2" ) 
                {
                    if ((chkComplete.Checked == false && chkComplete.Visible) && (chkpending.Visible && !chkpending.Checked)) //|| (chkNeedFurRev.Visible == true && chkNeedFurRev.Checked == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        {
                            return false;
                        }
                        if (returnedstring == "Yes")
                        {
                            chkCompleteUpdate();
                        }
                    }
                }
            }
            if (anytxtmodified)
            {
                SaveData();
            }
            
            
            if (done == false)
            { can_close = false; }

            if (can_close == true)
            {
                if (LckdbyUsrElsewhere == false)
                {
                    relsRespLock();
                }
            }
            // releaseAllLocks();

            return can_close;
        }
        
        // ********************************************************************
         private void btnSearch_Click(object sender, EventArgs e)
         {
             Search();
         }

         public void DataChanged()
         {
            done = true;
                 
                 //booleans to check if any data has changed. If Yes, then save data. (bRespid || won't apply to respid changes since respid changes are instant)
                 if ( bStatcode || bSurvey || bNewTC || bContractNum || bProjdesc || bProjloc || bProjcityst ||
                     bRespOrg || bFactOfficial || bOtherResp || bAddr1 || bAddr2 || bAddr3 || bZipCode || bLag || bCollTec || bStrtdater ||
                     bPersontoCont || bPhoneNum || bExt || bFaxNumber || bPersontoCont2 || bPhoneNum2 || bExt2 || bSplnote || bEmailAddr || bWebAddr)
                 {
                     DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                     if (result2 == DialogResult.Yes)
                     {
                            { SaveData(); }
                     }
                    
                 }
            ResetAuditVariables();
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

        private bool ValidateData()
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    DialogResult result = MessageBox.Show("Fax number is invalid.", "OK", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtFax.Focus();
                        txtFax.Text = dodgeinitial.Fax;
                        return false;
                    }
                }

                if (txtZip.Text.Trim() != "")
                {
                    bool isCanada = CheckAddress3IsCanada();

                    if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
                    {
                        DialogResult result = MessageBox.Show("Zip Code is invalid.", "OK", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            notvalid = true;
                            txtZip.Focus();
                            txtZip.Text = dodgeinitial.Zip.Trim();
                            return false;
                        }
                    }
                }
               

                if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtPhone.Focus();
                        txtPhone.Text = dodgeinitial.Phone2;
                        return false;
                    }
                }

                if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtPhone2.Focus();
                        txtPhone2.Text = dodgeinitial.Phone;
                        return false;
                    }
                }

                if (!GeneralDataFuctions.CheckUtilitiesNewTCOwner(txtNewtc.Text, cboSurvey.Text))
                {
                    MessageBox.Show("NEWTC not valid for Ownership.", "OK", MessageBoxButtons.OK);

                    return false;
                }
            }

            return true;
        }

        private void SaveData()
         {
             notvalid = false;
            

             DateTime prgdtm = DateTime.Now;
             string usrnme = Environment.UserName;
             string id = txtId.Text;
             string newflag = "";
             string oldflag = "";
             string masterid = dodgeinitial.Masterid.ToString();

             //update any changes to NewTC and Survey(Owner) to Master table fields
             if ((bSurvey) || (bNewTC))
             {
                 if (bSurvey == false)
                 {
                     if (cboSurvey.Visible == false)
                     { newSurvey = "M"; }
                     else
                     {
                        newSurvey = cboSurvey.Text;
                    }
                 }
                 if (bNewTC == false)
                 { newNewTC = dodgeinitial.Newtc; }
                 else
                { newNewTC = txtNewtc.Text; }
                 namedata.UpdateMasterFlds(newSurvey, newNewTC, masterid);

                 if (bSurvey)
                 {
                     oldval = oldSurvey;
                     newval = newSurvey;
                     varnme = "OWNER";
                     NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                 }
                 if (bNewTC)
                 {
                    if (oldNewTC == "")
                        oldNewTC = dodgeinitial.Newtc.ToString().Trim();
                     oldval = oldNewTC;
                     newval = newNewTC;
                     varnme = "NEWTC";
                     NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                 }
             }

             //update any changes to Sample table fields. 5 fields.
             if ((bStatcode) || (bContractNum) || (bProjdesc) || (bProjloc) || (bProjcityst) || (bRespid) || (bStrtdater))
             {
                SampleData sdata = new SampleData();
                Sample samp = sdata.GetSampleData(id);
                 if (bStatcode == false)
                 {
                    newStatCode = cbStatCode.Text;
                 }
                 else
                {
                    newStatCode = cbStatCode.Text;
                    samp.Status = newStatCode.Substring(0, 1);
                }
                 if (bContractNum == false)
                 {
                    newContractNum = dodgeinitial.Contract.Trim().ToString();
                }
                 else
                {
                    newContractNum = txtContractNum.Text;
                    samp.Contract = newContractNum;
                }
                 if (bProjdesc == false)
                 {
                    newProjDesc = dodgeinitial.ProjDesc.Trim().ToString();
                 }
                 else
                {
                    newProjDesc = txtProjDesc.Text;
                    samp.Projdesc = newProjDesc;
                }
                 if (bProjloc == false)
                 {
                    newProjLoc = dodgeinitial.Projloc.Trim().ToString();
                }
                else
                {
                    newProjLoc = txtProjLoc.Text;
                    samp.Projloc = newProjLoc;
                }
                 if (bProjcityst == false)
                 {
                    newProjCitySt = dodgeinitial.Pcityst;
                }
                 else
                {
                    newProjCitySt = txtPCitySt.Text;
                    samp.Pcityst = newProjCitySt;
                }
                if (bStrtdater == false)
                {
                    newStrtdater = dodgeinitial.Strtdater;
                }
                else
                {
                    newStrtdater = txtStrtDater.Text;
                    samp.Strtdater = newStrtdater;
                    samp.Strtdate = newStrtdater;
                    samp.Flagstrtdate = "R";
                    if (samp.Repsdate == "")
                        samp.Repsdate = currYearMon;

                    if (samp.Status == "4")
                        samp.Active = "I";
                }
                sdata.SaveSampleData(samp);
              
                 //Audit values entered into the appropriate table.
                 if (bContractNum)
                 {
                     oldval = oldContractNum;
                     newval = newContractNum;
                     varnme = "CONTRACT";
                     NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                 }
                 if (bStatcode)
                 {
                     oldval = dodgeinitial.Statuscode;
                     newval = newStatCode.Substring(0, 1);
                     varnme = "STATUS";
                     NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);

                    //Set up active value
                    if ((oldval == "1" || oldval == "2" || oldval == "3" || oldval == "7" || oldval == "8") && (newval == "4" || newval == "5" || newval == "6"))
                        namedata.UpdateActive(Id, "I");
                    else if ((oldval == "4" || oldval == "5" || oldval == "6") && (newval == "1" || newval == "2" || newval == "3" || newval == "7" || newval == "8"))
                        namedata.UpdateActive(Id, "A");
                }
                if (bStrtdater)
                {
                    oldval = oldStrtdater;
                    newval = newStrtdater;
                    varnme = "STRTDATE";
                    newflag = "R";
                    NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                }
            }

             //If Respid is changed ignore all the changes to the Respondent table fields.
             if ((bCollTec) || (bRespOrg) || (bFactOfficial) || (bOtherResp) || (bPersontoCont) || (bAddr1) || (bAddr2) || (bAddr3) || (bPersontoCont2) || (bSplnote) || (bEmailAddr) || (bWebAddr)
                 || (bZipCode) || (bPhoneNum) || (bLag) || (bExt) || (bFaxNumber) || (bPhoneNum2) || (bExt2))
             {
                if (bCollTec == false)
                {
                    newCollTec = dodgeinitial.Coltec;}  
                else
                {
                    newCollTec = cboCollTec.Text;
                }
                 if (bRespOrg == false)
                 { newRespOrg = dodgeinitial.Resporg; }
                 else
                { newRespOrg = txtRespOrg.Text; }
                 if (bFactOfficial == false)
                 { newFactOfficial = dodgeinitial.Factoff; }
                 else
                { newFactOfficial = txtFactOff.Text; }
                 if (bOtherResp == false)
                 { newOtherResp = dodgeinitial.Othrresp; }
                 else
                { newOtherResp = txtOthrResp.Text; }
                 if (bPersontoCont == false)
                 { newPersontoCont = dodgeinitial.Respname; }
                 else
                { newPersontoCont = txtRespname.Text; }
                 if (bAddr1 == false)
                 { newAddr1 = dodgeinitial.Addr1; }
                 else
                { newAddr1 = txtAddr1.Text; }
                 if (bAddr2 == false)
                 { newAddr2 = dodgeinitial.Addr2; }
                else
                { newAddr2 = txtAddr2.Text; }
                if (bAddr3 == false)
                 { newAddr3 = dodgeinitial.Addr3; }
                else
                { newAddr3 = txtAddr3.Text; }
                if (bPersontoCont2 == false)
                 { newPersontoCont2 = dodgeinitial.Respname2; }
                else
                { newPersontoCont2 = txtRespname2.Text; }
                 if (bSplnote == false)
                 { newSplnote = dodgeinitial.Respnote; }
                 else
                { newSplnote = txtRespnote.Text; }
                 if (bEmailAddr == false)
                 { newEmailAddr = dodgeinitial.Email; }
                 else
                { newEmailAddr = txtEmail.Text; }
                 if (bWebAddr == false)
                 { newWebAddr = dodgeinitial.Weburl; }
                 else
                { newWebAddr = txtWebUrl.Text; }
                 if (bZipCode == false)
                 { newZipCode = dodgeinitial.Zip; }
                 else
                { newZipCode = txtZip.Text; }
                 if (bPhoneNum == false)
                 { newPhoneNum = dodgeinitial.Phone; }
                 else
                { newPhoneNum = txtPhone.Text; }
                 if (bExt == false)
                 { newExt = dodgeinitial.Ext; }
                 else
                { newExt = txtExt1.Text; }
                 if (bFaxNumber == false)
                 { newFaxNumber = dodgeinitial.Fax; }
                else
                { newFaxNumber = txtFax.Text; }
                 if (bLag == false)
                 { newLag = dodgeinitial.Lag; }
                 else
                { newLag = txtLag.Text; }
                 if (bPhoneNum2 == false)
                 { newPhoneNum2 = dodgeinitial.Phone2; }
                 else
                { newPhoneNum2 = txtPhone2.Text; }
                 if (bExt2 == false)
                 { newExt2 = dodgeinitial.Ext2; }
                 else
                { newExt2 = txtExt2.Text; }
                string addr3 = "";
                if (txtAddr3.Text.Trim() != "")
                    addr3 = txtAddr3.Text.Substring(txtAddr3.Text.TrimEnd().Length - 2, 2);
                 namedata.UpdateRespondentFlds(newCollTec.Substring(0, 1), newRespOrg, newFactOfficial, newOtherResp, newPersontoCont, newAddr1, newAddr2, newAddr3, newPersontoCont2, newSplnote,
                                                 newEmailAddr, newWebAddr, addr3,
                                                 newZipCode, newPhoneNum, newExt, newFaxNumber, newPhoneNum2, newExt2, newLag, Respid);
                 if (bCollTec)
                 {
                     oldval = oldCollTec.Substring(0, 1);
                     newval = newCollTec.Substring(0, 1);
                     varnme = "COLTEC";
                    if (oldval != newval)
                    { NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm); }
                 }
                 if (bRespOrg)
                 {
                     oldval = oldRespOrg;
                     newval = newRespOrg;
                     varnme = "RESPORG";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bFactOfficial)
                 {
                     oldval = oldFactOfficial;
                     newval = newFactOfficial;
                     varnme = "FACTOFF";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bOtherResp)
                 {
                     oldval = oldOtherResp;
                     newval = newOtherResp;
                     varnme = "OTHRRESP";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bLag)
                 {
                     oldval = oldLag;
                     newval = newLag;
                     varnme = "LAG";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bPersontoCont)
                 {
                     oldval = oldPersontoCont;
                     newval = newPersontoCont;
                     varnme = "RESPNAME";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bAddr1)
                 {
                     oldval = oldAddr1;
                     newval = newAddr1;
                     varnme = "ADDR1";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bAddr2)
                 {
                     oldval = oldAddr2;
                     newval = newAddr2;
                     varnme = "ADDR2";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bAddr3)
                 {
                     oldval = oldAddr3;
                     newval = newAddr3;
                     varnme = "ADDR3";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bPersontoCont2)
                 {
                     oldval = oldPersontoCont2;
                     newval = newPersontoCont2;
                     varnme = "RESPNAME2";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bSplnote)
                 {
                     oldval = oldSplnote;
                     newval = newSplnote;
                     varnme = "RESPNOTE";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bEmailAddr)
                 {
                     oldval = oldEmailAddr;
                     newval = newEmailAddr;
                     varnme = "EMAIL";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bWebAddr)
                 {
                     oldval = oldWebAddr;
                     newval = newWebAddr;
                     varnme = "WEBURL";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bZipCode)
                 {
                     oldval = oldZipCode;
                     newval = newZipCode;
                     varnme = "ZIP";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bPhoneNum)
                 {
                     oldval = oldPhoneNum;
                     newval = newPhoneNum;
                     varnme = "PHONE";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bExt)
                 {
                     oldval = oldExt;
                     newval = newExt;
                     varnme = "EXT";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bFaxNumber)
                 {
                     oldval = oldFaxNumber;
                     newval = newFaxNumber;
                     varnme = "FAX";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bPhoneNum2)
                 {
                     oldval = oldPhoneNum2;
                     newval = newPhoneNum2;
                     varnme = "PHONE2";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
                 if (bExt2)
                 {
                     oldval = oldExt2;
                     newval = newExt2;
                     varnme = "EXT2";
                     NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                 }
             }
            if (chkComplete.Checked || chkpending.Checked)
            { chkCompleteUpdate(); }
            if (chkNeedFurRev.Checked)
            { needFurtherRev(); }
            ResetAuditVariables();
            anytxtmodified = false;

            dodgeinitial = DodgeInitialData.GetDodgeInitialData(Id);
        }

        private void ResetAuditRespVariables()
        {
            newCollTec = "";
            oldCollTec = "";
            bCollTec = false;

            bRespOrg = false;
            newRespOrg = "";
            oldRespOrg = "";

            newFactOfficial = "";
            oldFactOfficial = "";
            bFactOfficial = false;

            newOtherResp = "";
            oldOtherResp = "";
            bOtherResp = false;

            newAddr1 = "";
            oldAddr1 = "";
            bAddr1 = false;

            newAddr2 = "";
            oldAddr2 = "";
            bAddr2 = false;

            newAddr3 = "";
            oldAddr3 = "";
            bAddr3 = false;

            newZipCode = "";
            oldZipCode = "";
            bZipCode = false;

            newPersontoCont = "";
            oldPersontoCont = "";
            bPersontoCont = false;

            newPhoneNum = "";
            oldPhoneNum = "";
            bPhoneNum = false;

            newExt = "";
            oldExt = "";
            bExt = false;

            newFaxNumber = "";
            oldFaxNumber = "";
            bFaxNumber = false;

            newPersontoCont2 = "";
            oldPersontoCont2 = "";
            bPersontoCont2 = false;

            newPhoneNum2 = "";
            oldPhoneNum2 = "";
            bPhoneNum2 = false;

            //newLag = "";
            //oldLag = "";
            //bLag = false;

            newExt2 = "";
            oldExt2 = "";
            bExt2 = false;

            newSplnote = "";
            oldSplnote = "";
            bSplnote = false;

            newEmailAddr = "";
            oldEmailAddr = "";
            bEmailAddr = false;

            newWebAddr = "";
            oldWebAddr = "";
            bWebAddr = false;
        }

         //Boolean, old and new values of fields used in Audit are reset when starting a new record
        private void ResetAuditVariables()
        {
            returnedstring = "";
            newval = string.Empty;
            oldval = string.Empty;
            oldval = "";
            varnme = "";
            
            oldRespid = "";
            old1Respid = "";
            old2Respid = "";
            old3Respid = "";
            bRespid = false;
            newStatCode = "";
            oldStatCode = "";
            bStatcode = false;
            newProjDesc = "";
            oldProjDesc = "";
            bProjdesc = false;
            newProjLoc = "";
            oldProjLoc = "";
            bProjloc = false;
            newProjCitySt = "";
            oldProjCitySt = "";
            bProjcityst = false;
            newCollTec = "";
            oldCollTec = "";
            bCollTec = false;
            newSurvey = "";
            oldSurvey = "";
            bSurvey = false;
            oldNewTC = "";
            newNewTC = "";
            bNewTC = false;
            newContractNum = "";
            oldContractNum = "";
            bContractNum = false;
            
            newRespOrg = "";
            oldRespOrg = "";
            
            bRespOrg = false;
            newFactOfficial = "";
            oldFactOfficial = "";
            bFactOfficial = false;
            newOtherResp = "";
            oldOtherResp = "";
            bOtherResp = false;
            newAddr1 = "";
            oldAddr1 = "";
            bAddr1 = false;
            newAddr2 = "";
            oldAddr2 = "";
            bAddr2 = false;
            newAddr3 = "";
            oldAddr3 = "";
            bAddr3 = false;
            newZipCode = "";
            oldZipCode = "";
            bZipCode = false;
            newPersontoCont = "";
            oldPersontoCont = "";
            bPersontoCont = false;
            newPhoneNum = "";
            oldPhoneNum = "";
            bPhoneNum = false;
            newExt = "";
            oldExt = "";
            bExt = false;
            newFaxNumber = "";
            oldFaxNumber = "";
            bFaxNumber = false;
            newPersontoCont2 = "";
            oldPersontoCont2 = "";
            bPersontoCont2 = false;
            newPhoneNum2 = "";
            oldPhoneNum2 = "";
            bPhoneNum2 = false;
            newLag = "";
            oldLag = "";
            bLag = false;
            newExt2 = "";
            oldExt2 = "";
            bExt2 = false;
            newSplnote = "";
            oldSplnote = "";
            bSplnote = false;
            newEmailAddr = "";
            oldEmailAddr = "";
            bEmailAddr = false;
            newWebAddr = "";
            oldWebAddr = "";
            bWebAddr = false;
        }

        private void relsRespLock()
        {
            //Only unlock if you had locked it. Cannot unlock other user's lock.
            if (Respid == "" || Respid == null)
            {
                Respid = txtRespid.Text;
            }
            if (txtRespid.Text == "" && txtId.Text == "")
            {
                 return;
            }
             else
             {
                 if (dodgeinitial.RespLock == UserInfo.UserName || string.IsNullOrWhiteSpace(dodgeinitial.RespLock))
                 {
                    if (Respid == "" || Respid == null)
                    {
                        if (txtRespid.Text == "" || txtRespid.Text == null)
                        { Respid = txtId.Text; }
                    }
                   
                    string relsUsrName = "";
                     GeneralDataFuctions.UpdateRespIDLock(Respid, relsUsrName);
                 }
             }
         }

         private void Search()
         {
             if (txtRespIDs.Text.Trim() == "" && txtOwner.Text.Trim() == "" && txtContact.Text.Trim() == "" && txtPhone1s.Text.Trim() == "" && txtPhone2s.Text.Trim() == "" && txtEmails.Text.Trim() == "")
             {
                 MessageBox.Show("Please Enter Search Criteria.");
                 txtRespIDs.Focus();
                 return;
             }
            if (txtRespIDs.TextLength > 0)
            {
                if (txtRespIDs.TextLength < 7)
                {
                    MessageBox.Show("RESPID should be 7 digits.");
                    txtRespIDs.Focus();
                    txtRespIDs.Text = "";
                    this.DialogResult = DialogResult.None;
                    return;
                }
                else
                {
                    bool idexist;
                    idexist = GeneralDataFuctions.ChkRespid(txtRespIDs.Text);
                    //update the data with DCP Initial data for the entered ID
                    if (!idexist)
                    {
                        MessageBox.Show("Invalid RESPID");
                        txtRespIDs.Focus();
                        txtRespIDs.Text = "";
                        this.DialogResult = DialogResult.None;
                        return;
                    }
                }
            }

            if (txtRespIDs.Text.Trim() != "" && (txtOwner.Text.Trim() != "" || txtContact.Text.Trim() != "" || txtPhone1s.Text.Trim() != "" || txtPhone2s.Text.Trim() != "" || txtEmails.Text.Trim() != ""))
             {
                 MessageBox.Show("Other Criteria should not be included in Respid search.", "Entry Error");
                 txtRespIDs.Text = "";
                 txtOwner.Text = "";
                 txtContact.Text = "";
                 txtPhone1s.Text = "";
                 txtPhone2s.Text = "";
                 txtEmails.Text = "";
                 txtRespIDs.Focus();
                 return;
             }
             if (txtOwner.Text.Trim().Length > 0)
             {
                 string[] words = txtOwner.Text.Trim().Split();
                 if (words.Count() > 3)
                 {
                     MessageBox.Show("Please Enter 1 to 3 words for Organization to search.");
                     txtOwner.Focus();
                     txtOwner.SelectAll();
                     return;
                 }
             }
             if (txtContact.Text.Length > 0)
             {
                 string[] words = txtContact.Text.Trim().Split();
                 if (words.Count() > 3)
                 {
                     MessageBox.Show("Please Enter 1 to 3 words for Contact to search.");
                     txtContact.Focus();
                     txtContact.SelectAll();
                     return;
                 }
             }
             if (txtEmails.Text.Length > 0)
             {
                 string words = txtEmails.Text.Trim();
                if (!GeneralFunctions.isEmail(words))
                {
                    MessageBox.Show("Email address is invalid.");
                    txtEmails.Focus();
                    txtEmails.Text = "";
                    return;
                }
            }
             this.Cursor = Cursors.WaitCursor;
             GetRespondentData();
             this.Cursor = Cursors.Default;
         }

         private void GetRespondentData()
         {
             dgInitialsSrch1.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
             dgInitialsSrch1.RowHeadersVisible = false; // set it to false if not needed
             DataTable dt = GetDataTable();
             if (dt.Rows.Count == 0)
             {
                 MessageBox.Show("There were no records found.");
                 dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
                 lblRecordCount.Text = "0 Cases";
                 txtRespIDs.Focus();
                 return;
             }
             dgInitialsSrch1.DataSource = dt;
             for (int i = 0; i < dgInitialsSrch1.ColumnCount; i++)
             {
                 if (i == 0)
                 {
                     dgInitialsSrch1.Columns[i].Width = 95;
                 }

                 if (i == 1)
                     dgInitialsSrch1.Columns[i].HeaderText = "ORGANIZATION";
                 if (i == 2)
                     dgInitialsSrch1.Columns[i].HeaderText = "CONTACT";

                 if (i == 1 || i == 2)
                 {
                     dgInitialsSrch1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                     dgInitialsSrch1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.BottomLeft;
                     dgInitialsSrch1.Columns[i].Width = 320;
                 }
                 if (i == 3)
                     dgInitialsSrch1.Columns[i].HeaderText = "PHONE1";
                 if (i == 4)
                     dgInitialsSrch1.Columns[i].HeaderText = "PHONE2";
                 else if (i == 3 || i == 4)
                 {
                     dgInitialsSrch1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    // dgInitialsSrch1.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                     dgInitialsSrch1.Columns[i].Width = 160;
                 }
                dgInitialsSrch1.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
             }
             dgInitialsSrch1.RowHeadersVisible = true; // set it to false if not needed
             dgInitialsSrch1.AutoResizeRowHeadersWidth(0, DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);

            if (dt.Rows.Count == 1)
                 lblRecordCount.Text = dt.Rows.Count.ToString() + " Case Found";  
             else
                 lblRecordCount.Text = dt.Rows.Count.ToString() + " Cases Found"; 
         }

         private DataTable GetDataTable()
         {
             string org1 = "";
             string org2 = "";
             string org3 = "";
             string contact1 = "";
             string contact2 = "";
             string contact3 = "";
             string emails = "";
             string phone1s = "";
             string phone2s = "";

             //splict owners and contacts
             if (txtOwner.Text.Trim().Length > 0)
             {
                 string[] owners = txtOwner.Text.Trim().Split(' ');
                 if (owners.Length > 0)
                 {
                     if (owners.Length == 3)
                     {
                         org1 = owners[0];
                         org2 = owners[1];
                         org3 = owners[2];
                     }
                     else if (owners.Length == 2)
                     {
                         org1 = owners[0];
                         org2 = owners[1];
                     }
                     else
                         org1 = owners[0];
                 }
             }

             if (txtContact.Text.Trim().Length > 0)
             {
                 string[] contacts = txtContact.Text.Trim().Split(' ');
                 if (contacts.Length > 0)
                 {
                     if (contacts.Length == 3)
                     {
                         contact1 = contacts[0];
                         contact2 = contacts[1];
                         contact3 = contacts[2];
                     }
                     else if (contacts.Length == 2)
                     {
                         contact1 = contacts[0];
                         contact2 = contacts[1];
                     }
                     else
                         contact1 = contacts[0];
                 }
             }

             if (txtEmails.Text.Trim().Length > 0)
             {
                 emails = txtEmails.Text;                                                                                 
             }
             else
             { txtEmails.Text = ""; }
             
             if (txtPhone1s.Text != "")
             { phone1s = txtPhone1s.Text.ToString(); }
             if (txtPhone2s.Text != "")
             {phone2s = txtPhone2s.Text.ToString();}

             dataObject = new DodgeInitialData();
             DataTable dt = dataObject.GetRespondentSearchData(txtRespIDs.Text, org1, org2, org3, contact1, contact2, contact3, emails, phone1s, phone2s, chkExact.Checked);

            //get rid of current respid
            dt.AcceptChanges();
            foreach (DataRow row in dt.Rows)
            {
                if (row["Respid"].ToString() == Respid)
                    row.Delete();
            }
            dt.AcceptChanges();

            return dt;

         }
            
        private void ClearSearchFlds()
        {
            txtRespIDs.Text = "";
            txtOwner.Text = "";

            txtContact.Text = "";
            txtPhone1s.Text = "";
            txtPhone2s.Text = "";
            txtEmails.Text = "";

            chkExact.Checked = false;
            dataObject = new DodgeInitialData();
            dgInitialsSrch1.DataSource = dataObject.GetEmptyTable();
            FormatdodgeIniDG();
            lblRecordCount.Text = "0 Cases";

            txtRespIDs.Focus();
        }

         private void btnResetSearch_Click(object sender, EventArgs e)
         {
            ClearSearchFlds();
         }

        // *********************************************************************
         private void btnSlip_Click(object sender, EventArgs e)
         {
             frmSlipDisplay fSD = new frmSlipDisplay();
             fSD.Id = txtId.Text;
             fSD.Dodgenum = dodgeinitial.Dodgenum;
             fSD.Fin = dodgeinitial.Fin;
             fSD.ShowDialog();  //show child
         }

         private void btnSource_Click(object sender, EventArgs e)
         {
             frmSourceAddrPopup fmSAP = new frmSourceAddrPopup(dodgeinitial.Masterid);
             fmSAP.ShowDialog();  //show child
             if (fmSAP.DialogResult == DialogResult.Cancel)
             {
                 fmSAP.Dispose();
             }
         }

         private void btnTFU_Click(object sender, EventArgs e)
         {
             relsRespLock();
             chkBrowseUpdate();
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (!ValidateData())
                    return;
                txtModifiedStatus();
            }

             //Display TFU on the C-700 button for NPC users
             if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
             {
                this.Hide(); // hide parent
                frmTfu tfu = new frmTfu();
                if (txtRespid.Text == "")
                    tfu.RespId = txtId.Text;
                else
                     tfu.RespId = txtRespid.Text;
                tfu.CallingForm = this;

                tfu.ShowDialog();   // show child
            }
             else
             {
                 this.Hide();         // hide parent
                 frmC700 fC700 = new frmC700();
                 if (Idlist != null)
                 {
                     //if number of records greater than 1 then get a list.         
                     if (Idlist.Count == 1)
                     { fC700.Id = txtId.Text; }
                     else
                     {
                         fC700.Id = txtId.Text;
                         fC700.Idlist = Idlist;
                         fC700.CurrIndex = CurrIndex;
                         fC700.Id = Idlist[CurrIndex];
                     }
                 }
                 else
                 {
                     fC700.Id = txtId.Text;
                 }

                 fC700.CallingForm = this;
                 fC700.ShowDialog();  // show child
             }

            if (LckdbyUsrElsewhere == false && !is_closing && editable)
            {
                GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
            }

            // frmDodgeInital_Load(btnTFU, EventArgs.Empty);
        }

         private void txtModifiedStatus()
         {
             if (anytxtmodified == true)
             {
                 SaveData();
             }
         }

         private void chkBrowseUpdate()
         {
             if (bRespid || bStatcode || bSurvey || bNewTC || bContractNum || bProjdesc || bProjloc || bProjcityst ||
                 bRespOrg || bFactOfficial || bOtherResp || bAddr1 || bAddr2 || bAddr3 || bZipCode || bLag || bCollTec ||
                 bPersontoCont || bPhoneNum || bExt || bFaxNumber || bPersontoCont2 || bPhoneNum2 || bExt2 || bSplnote || bEmailAddr || bWebAddr)
             {
                 csda.AddCsdaccessData(Id, "UPDATE");
             }
             else
             {
                 csda.AddCsdaccessData(Id, "BROWSE");
             }
         }

         private void btnRefresh_Click(object sender, EventArgs e)
         {
            RemoveTxtChanged();
            
                 btnResetClicked = true;
                 ResetAuditVariables();
                 if (txtRespid.Text == "")
                 { Respid = Id; }
                 else
                 {
                     Id = txtId.Text;
                 }
                // dodgeinitial = DodgeInitialData.GetDodgeInitialData(Id);
                 GetDataDodgeInitial();
             
             SetTxtChanged();
            btnResetClicked = false;
        }

      
         private void btnHist_Click(object sender, EventArgs e)
         {
             frmHistory fHistory = new frmHistory();
             fHistory.Id = txtId.Text;
             if (txtRespid.Text == "")
                fHistory.Respid = txtId.Text;
             else
                 fHistory.Respid = txtRespid.Text;
             fHistory.Resporg = txtRespOrg.Text;
             fHistory.Respname = txtRespname.Text;
            fHistory.StartPosition = FormStartPosition.CenterParent;
            fHistory.ShowDialog();  //show child
             if (fHistory.DialogResult == DialogResult.Cancel)
             {
                 fHistory.Dispose();
             }
         }

         private void btnAudit_Click(object sender, EventArgs e)
         {
             Respid = txtRespid.Text;
             if (txtRespid.Text == "")
             { Respid = txtId.Text; }
             frmRspAuditPopup fRspAuditPopup = new frmRspAuditPopup(Respid);
             fRspAuditPopup.ShowDialog();  //show child
         }

         Bitmap memoryImage;
         private void btnPrint_Click(object sender, EventArgs e)
         {
             this.Update();
             printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
             printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
             printDocument1.DefaultPageSettings.Landscape = true;
             memoryImage = GeneralFunctions.CaptureScreen(this);
             printDocument1.Print();
         }

         //Populate the dcp_Hist table
         private void UpdateCaseAccessRecord()
         {
            if (accestms =="")
                accestms = DateTime.Now.ToString("HHmmss");

            //Time that user exited case
            accestme = DateTime.Now.ToString("HHmmss");

             dodgeinitialdata.AddDCPHistRecs(id, user, accesday, accestms, accestme);
         }
         /***************************************************************/
         /**************C. 32 Review Status Entry************************/

        private string worked;
        private string rev1nme;
        private string rev2nme;
        private string revnme;
        private string hqworked;
        private string hqnme;
        int reviewNum;

        //Update the review status if checkbox checked
        private void chkCompleteUpdate()
        {
            worked = dodgeinitial.Worked.Trim();
            rev1nme = dodgeinitial.Rev1nme.Trim();
            rev2nme = dodgeinitial.Rev2nme.Trim();
            hqworked = dodgeinitial.HQWorked.Trim();
            revnme = "";
            id = txtId.Text;
            if (editable)
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    if ((rev1nme == "" && worked == "0") || (rev2nme == "" && worked == "3"))
                    {
                        revnme = user;
                        if (chkComplete.Checked)
                            worked = "1";
                        else if (chkpending.Checked)
                            worked = "3";
                        reviewNum = 1;
                        dodgeinitialdata.UpdateDodgeIniRevStatusNPC(id, worked, revnme, reviewNum);
                    }
                    else if (((rev1nme != "" && worked == "1" && rev1nme != user) || (rev2nme != "" && worked == "3" && rev1nme != user)) && !(UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4"))
                    {
                        revnme = user;
                        if (chkComplete.Checked)
                            worked = "2";
                        else if (chkpending.Checked)
                            worked = "3"; 
                        reviewNum = 2;
                        dodgeinitialdata.UpdateDodgeIniRevStatusNPC(id, worked, revnme, reviewNum);
                    }
                        
                 }

                if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
                    {
                        if (hqworked == "0" || hqworked =="1")
                        {
                            worked = "2";
                            hqnme = user;
                            reviewNum = 2;
                            dodgeinitialdata.UpdateDodgeIniRevStatus(id, worked, hqnme, reviewNum);
                        }
                    }
               }
                rev1nme = dodgeinitial.Rev1nme.Trim();
        }

        private void chkNeedFurRev_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNeedFurRev.Checked == true)
            {
                chkComplete.Checked = false;
            }
        }

        private void needFurtherRev()
        {
            //Visible only for HQ  users. 
            hqworked = dodgeinitial.HQWorked.Trim();
            if (chkNeedFurRev.Checked)
            {
                if (hqworked == "0")
                {
                    id = txtId.Text;
                    hqworked = "1";
                    hqnme = user;
                    reviewNum = 1;
                    dodgeinitialdata.UpdateDodgeIniRevStatus(id, hqworked, hqnme, reviewNum);
                }
            }
        }

        private void RevComplete()
        {
            rev1nme = dodgeinitial.Rev1nme.Trim();
            if (chkComplete.Checked)
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    if (dodgeinitial.Worked != "2")
                    {
                            chkCompleteUpdate();
                            done = true;
                    }
                }
            }

            if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
            {
                if (dodgeinitial.Worked != "2")
                {
                    chkCompleteUpdate();
                    done = true;
                }
            }
        }

        private void chkComplete_CheckChanged(object sender, EventArgs e)
         {
            rev1nme = dodgeinitial.Rev1nme.Trim();
            if (chkComplete.Checked)
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    if (dodgeinitial.Worked != "2")
                    {
                        if ((rev1nme == user) && (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4"))
                        {
                            MessageBox.Show("You performed the first review. Cannot complete the second review.");
                            chkComplete.Checked = false;
                            //notvalid = true;
                            //anytxtmodified = false;
                            //done = false;
                            return;
                        }
                    }
                    if (chkpending.Visible) chkpending.Enabled = false;
                }
            }
            else
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    if (chkpending.Visible) chkpending.Enabled = true;
                }
            }

            if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
            {
                if (dodgeinitial.Worked != "2")
                {
                    chkCompleteUpdate();
                }
            }

            if (chkComplete.Checked == true)
            {
                chkNeedFurRev.Checked = false;
            }
         }

        private void ShowPermissionMessage()
        {
            MessageBox.Show("You do not have permission to access to access this area");
        }

        //Check if the Case is Locked by Another User and Display User Name that has Respid Locked
         private void ShowLockMessage()
         {
             if (locked_by != "") //&& locked_by != UserInfo.UserName
                 MessageBox.Show("This case is locked by " + locked_by + ", cannot be edited.");
         }

        private void CallReviewCompDialg()
        {
            //Check if the record is locked
            if (editable)
            {
                if ((chkNeedFurRev.Visible && !chkNeedFurRev.Checked) || ((chkComplete.Visible && !chkComplete.Checked) && (chkpending.Visible && !chkpending.Checked)))
                {
                    frmRevCompMsgbox fRevComp =new frmRevCompMsgbox();
                    DialogResult result3 = fRevComp.ShowDialog();
                    returnedstring = fRevComp.RevCompRtn;
                    if (returnedstring == "Yes")
                    {
                        chkComplete.Checked = true;
                    }
                    if (returnedstring == "Cancel")
                    {
                        return;
                    }
                }
            }
        }

        //private void chkReviewButtons()
        //{
        //    //If both Need Further and complete visible but not checked
        //    if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == true && chkNeedFurRev.Checked == false))
        //    {
        //        CallReviewCompDialg();
        //        if (returnedstring == "Cancel")
        //        { return; }
        //    }
        //    if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == false))
        //    {
        //        CallReviewCompDialg();
        //        if (returnedstring == "Cancel")
        //        { return; }
        //    }
        //}

        // ********************************************************************
         private void btnNextInitial_Click(object sender, EventArgs e)
         {
            if (!ValidateData())
                return;

             if (btnNextInitial.Text == "PREVIOUS")
             {
                if (dodgeinitial.Worked != "2" && dodgeinitial.HQWorked != "2")
                {
                    if (chkNeedFurRev.Checked == false && chkComplete.Checked == false && (chkpending.Visible && !chkpending.Checked))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        {
                            return;
                        }
                        else
                        { SaveData(); }
                    }
                }
                if (anytxtmodified)
                 {
                    SaveData();
                 }

                if (LckdbyUsrElsewhere == false)
                    {
                        relsRespLock();
                    }

                if (CallingForm != null)
                 {
                    //if (CallingForm.Name == "frmReferralReview" )
                    //      frmDodgeInitialReview CallingForm = new frmDodgeInitialReview();
 

                    CallingForm.Show();
                }
                this.Close();
            }

                         
             // Or show popup to enter next initial
            if (btnNextInitial.Text == "NEXT INITIAL")
            {
                frmDCPInitPopup fDCPInit = new frmDCPInitPopup();
                //Only if Worked not equals 2 and hqworked not equals 2
                if ((dodgeinitial.Worked) != "2" || (dodgeinitial.HQWorked) != "2")
                {
                    //NPC Users
                    if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                    {
                        if (chkComplete.Checked == false && dodgeinitial.Worked != "2" && (chkpending.Visible && !chkpending.Checked))
                        {
                            CallReviewCompDialg();
                            if (returnedstring == "Cancel")
                            { return; }
                        }
                    }

                    if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
                    {
                        if (dodgeinitial.HQWorked != "2") //&& dodgeinitial.HQWorked != "2")
                        {
                            //If both Need Further and complete visible but not checked
                            if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == true && chkNeedFurRev.Checked == false))
                            {
                                CallReviewCompDialg();
                                if (returnedstring == "Cancel")
                                { return; }
                            }
                            if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == false))
                            {
                                CallReviewCompDialg();
                                if (returnedstring == "Cancel")
                                { return; }
                            }
                        }
                    }
                }
                if (chkComplete.Checked || chkpending.Checked || chkNeedFurRev.Checked)
                {
                    anytxtmodified = true;
                }
                
                if (anytxtmodified)
                {
                    SaveData();
                }

                /*Show the popup*/
                fDCPInit.ShowDialog();

                //Return user to the home screen if there are no more cases to process
                if (fDCPInit.DialogResult == DialogResult.Abort && fDCPInit.selflg) 
                {
                  
                        this.Close();
                        frmHome fH = new frmHome();
                        fH.Show();
                       // GeneralDataFuctions.AddCpraccessData(access_code, "EXIT");
                        return;
                        
              
                }

                //Update dcp_hist
                id = Id;
                UpdateCaseAccessRecord();
                anytxtmodified = false;

                if (fDCPInit.DialogResult == DialogResult.OK)
                {
                    //Release the lockof the previous ID before loading the new one.
                        if (LckdbyUsrElsewhere == false)
                        {
                            relsRespLock();
                        }
                   // releaseAllLocks();
                    RemoveTxtChanged();
                    chkNeedFurRev.Checked = false;
                    chkComplete.Checked = false;
                    chkpending.Checked = false;
                    if (fDCPInit.SelectedDCPId == "")
                    {
                        this.Close();
                        frmHome fH = new frmHome();
                        fH.Show();
                        return;
                    }
                    else
                    {
                        Id = fDCPInit.SelectedDCPId;
                        id = Id;
                        //  dodgeinitial = DodgeInitialData.GetDodgeInitialData(Id);
                        //ignore lock
                        if (fDCPInit.selflg)
                            btnResetClicked = true;

                        GetDataDodgeInitial();
                        txtNewtc.Focus();
                        SetTxtChanged();
                        fDCPInit.Dispose();

                        if (fDCPInit.selflg)
                            btnResetClicked = false;
                    }
                }
                else
                {
                    return;
                }
            }
         }

         private void btnPrevCase_Click(object sender, EventArgs e)
         {
            chkBrowseUpdate();

            if (!ValidateData())
                return;
            txtModifiedStatus();
            
            if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
            {
                if (dodgeinitial.HQWorked != "2") //&& dodgeinitial.HQWorked != "2")
                {
                    //If both Need Further and complete visible but not checked
                    if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == true && chkNeedFurRev.Checked == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        { return; }
                    }
                    if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        { return; }
                    }
                }
            }
            if (notvalid == true)
            { return; }


            if (Idlist != null)
            {
              if (CurrIndex == 0)
                {
                    MessageBox.Show("You are at the first observation");
                }
                else
                {
                    if (LckdbyUsrElsewhere == false)
                    {
                        //releaseAllLocks();
                        GeneralDataFuctions.UpdateRespIDLock(Respid, "");
                    }

                    //Update dcp_hist
                    id = Id;
                    UpdateCaseAccessRecord();

                    CurrIndex = CurrIndex - 1;
                    Id = Idlist[CurrIndex];
                    txtCurrentrec.Text = (CurrIndex + 1).ToString();
                    LoadFormFromReview();
                    if (txtRespid.Text == "")
                    { dodgeinitial.Respid = Id; }
                    else
                    { dodgeinitial.Respid = txtRespid.Text; }

                     if (LckdbyUsrElsewhere == false)
                    {
                        GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
                    }
                    ClearSearchFlds();
                }
            }
         }

         private void btnNextCase_Click(object sender, EventArgs e)
         {
            chkBrowseUpdate();

            if (!ValidateData())
                return;
            txtModifiedStatus();
            
            if (UserInfo.GroupCode != EnumGroups.NPCManager && UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.GroupCode != EnumGroups.NPCLead)
            {
                if (dodgeinitial.HQWorked != "2") //&& dodgeinitial.HQWorked != "2")
                {
                    //If both Need Further and complete visible but not checked
                    if ((chkComplete.Visible && chkComplete.Checked == false) && (chkNeedFurRev.Visible == true && chkNeedFurRev.Checked == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        { return; }
                    }
                    if ((chkComplete.Checked == false) && (chkNeedFurRev.Visible == false))
                    {
                        CallReviewCompDialg();
                        if (returnedstring == "Cancel")
                        { return; }
                    }
                }
            }
            if (notvalid == true)
            { return; }

            if (Idlist != null && Idlist.Count != 1)
             {
                 if (CurrIndex == Idlist.Count - 1)
                 {
                     MessageBox.Show("You are at the last observation");
                 }
                 else
                 {
                     if (editable)
                     {
                         if (anytxtmodified)
                         {
                                 SaveData(); 
                                //release the lock of old respids
                                if (bRespid)
                                {
                                   // releaseAllLocks();
                                }
                         }
                     }
                    id = Id;
                    UpdateCaseAccessRecord();
                    if (LckdbyUsrElsewhere == false)
                    {
                        GeneralDataFuctions.UpdateRespIDLock(Respid, "");
                    }

                    // releaseAllLocks();
                    CurrIndex = CurrIndex + 1;
                     Id = Idlist[CurrIndex];
                     txtCurrentrec.Text = (CurrIndex + 1).ToString();
                     LoadFormFromReview();
                     if (txtRespid.Text == "")
                     { dodgeinitial.Respid = Id; }
                     else
                     { dodgeinitial.Respid = txtRespid.Text; }
                    if (LckdbyUsrElsewhere == false)
                    { 
                        GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
                    }
                    ClearSearchFlds();
                 }
             }
         }

         private void txt_TextChanged(object sender, EventArgs e)
         {
             if (sender is TextBox)
             {
                txt = new TextBox();
                newval = (sender as TextBox).Text;
                anytxtmodified = true;
             //   editable = true;

                 if (sender == txtRespid)
                 {
                     varnme = "RESPID";
                     string newRespid = txtRespid.Text.ToString().Trim();
                    if (dodgeinitial.Respid == null)
                        oldRespid = dodgeinitial.Id.ToString().Trim();
                    else
                    { oldRespid = dodgeinitial.Respid.ToString().Trim(); }

                    if (newRespid != oldRespid)
                     {
                         bRespid = true;
                     }
                     else
                     { bRespid = false; }
                 }

                 if (sender == txtNewtc)
                 {
                     GeneralFunctions.CheckIntegerField(sender, "NEWTC");
                     varnme = "NEWTC";
                     newNewTC = txtNewtc.Text.ToString().Trim();
                     oldNewTC = dodgeinitial.Newtc.ToString().Trim(); 
                     if (newNewTC != oldNewTC)
                     { bNewTC = true; }
                     else
                     { bNewTC = false; }
                 }

                 if (sender == txtContractNum)
                 {
                     varnme = "CONTRACT";
                     newContractNum = txtContractNum.Text.ToString().Trim();
                     if (oldContractNum == "" || oldContractNum == null)
                     { oldContractNum = " "; }
                     else
                    { oldContractNum = dodgeinitial.Contract.ToString().Trim(); }


                    if (newContractNum != oldContractNum)
                     { bContractNum = true; }
                     else
                     { bContractNum = false; }
                 }

                if (sender == txtProjDesc)
                {
                    varnme = "PROJDESC";
                    newProjDesc = txtProjDesc.Text.ToString().Trim();
                    oldProjDesc = dodgeinitial.ProjDesc.ToString().Trim();
                    if (newProjDesc != oldProjDesc)
                    { bProjdesc = true; }
                    else
                    { bProjdesc = false; }
                }

               if (sender == txtProjLoc)
                {
                    varnme = "PROJLOC";
                    newProjLoc = txtProjLoc.Text.ToString().Trim();
                     oldProjLoc = dodgeinitial.Projloc.ToString().Trim(); 

                     if (newProjLoc != oldProjLoc)
                     { bProjloc = true; }
                     else
                     { bProjloc = false; }
                 }

                if (sender == txtPCitySt)
                {
                    varnme = "PCITYST";
                    newProjCitySt = txtPCitySt.Text.ToString().Trim();
                    oldProjCitySt = dodgeinitial.Pcityst.ToString().Trim(); 

                     if (newProjCitySt != oldProjCitySt)
                     { bProjcityst = true; }
                     else
                     { bProjcityst = false; }
                 }

                 if (sender == txtRespOrg)
                 {
                     varnme = "RESPORG";
                    newRespOrg = txtRespOrg.Text.ToString().Trim();
                     oldRespOrg = dodgeinitial.Resporg.ToString().Trim(); 
                     if(newRespOrg != oldRespOrg)
                     { bRespOrg = true; }
                     else
                     { bRespOrg = false; }
                 }

                 if (sender == txtFactOff)
                 {
                     varnme = "FACTOFF";
                     newFactOfficial = txtFactOff.Text.ToString().Trim();
                    oldFactOfficial = dodgeinitial.Factoff.ToString().Trim(); 

                    if (newFactOfficial != oldFactOfficial)
                     { bFactOfficial = true; }
                     else
                     { bFactOfficial = false; }
                 }

                if (sender == txtOthrResp)
                {
                    varnme = "OTHRRESP";
                    newOtherResp = txtOthrResp.Text.ToString().Trim();
                     oldOtherResp = dodgeinitial.Othrresp.ToString().Trim();

                     if (newOtherResp != oldOtherResp)
                     { bOtherResp = true; }
                     else
                     { bOtherResp = false; }
                 }

                 if (sender == txtAddr1)
                 {
                     varnme = "ADDR1";
                     newAddr1 = txtAddr1.Text.ToString().Trim();
                     oldAddr1 = dodgeinitial.Addr1.ToString().Trim(); 

                    if (newAddr1 != oldAddr1)
                     { bAddr1 = true; }
                     else
                     { bAddr1 = false; }
                 }

                 if (sender == txtAddr2)
                 {
                     varnme = "ADDR2";
                     newAddr2 = txtAddr2.Text.ToString().Trim();
                    oldAddr2 = dodgeinitial.Addr2.ToString().Trim(); 

                    if (newAddr2 != oldAddr2)
                     { bAddr2 = true; }
                     else
                     { bAddr2 = false; }
                 }

                 if (sender == txtAddr3)
                 {
                     varnme = "ADDR3";
                     newAddr3 = txtAddr3.Text.ToString().Trim();
                    oldAddr3 = dodgeinitial.Addr3.ToString().Trim();

                    if (newAddr3 != oldAddr3)
                     {bAddr3 = true; }
                     else
                     { bAddr3 = false; }
                 }

                if (sender == txtZip)
                {
                    varnme = "ZIP";
                    newZipCode = txtZip.Text.ToString().Trim();
                     oldZipCode = dodgeinitial.Zip.ToString().Trim(); 
                
                     if (newZipCode != oldZipCode)
                     { bZipCode = true; }
                     else
                     { bZipCode = false; }
                 }

                 if (sender == txtRespname)
                 {
                     varnme = "RESPNAME";
                     newPersontoCont = txtRespname.Text.ToString().Trim();
                    oldPersontoCont = dodgeinitial.Respname.ToString().Trim(); 

                     if (newPersontoCont != oldPersontoCont)
                     { bPersontoCont = true; }
                     else
                     { bPersontoCont = false; }
                 }

                if (sender == txtRespname2)
                {
                    varnme = "RESPNAME2";
                    newPersontoCont2 = txtRespname2.Text.ToString().Trim();
                     oldPersontoCont2 = dodgeinitial.Respname2.ToString().Trim(); 
                
                     if (newPersontoCont2 != oldPersontoCont2)
                     { bPersontoCont2 = true; }
                     else
                     { bPersontoCont2 = false; }
                 }

                 if (sender == txtRespnote)
                 {
                    varnme = "RESPNOTE";
                     newSplnote = txtRespnote.Text.ToString().Trim();
                    oldSplnote = dodgeinitial.Respnote.ToString().Trim(); 

                    if (newSplnote != oldSplnote)
                     { bSplnote = true; }
                     else
                     { bSplnote = false; }
                 }

                 if (sender == txtEmail)
                 {
                     varnme = "EMAIL";
                     newEmailAddr = txtEmail.Text.ToString().Trim();
                    oldEmailAddr = dodgeinitial.Email.ToString().Trim(); 

                    if (newEmailAddr != oldEmailAddr)
                     { bEmailAddr = true; }
                     else
                     { bEmailAddr = false; }
                 }

                 if (sender == txtWebUrl)
                 {
                     varnme = "WEBURL";
                     newWebAddr = txtWebUrl.Text.ToString().Trim();
                    oldWebAddr = dodgeinitial.Weburl.ToString().Trim(); 

                     if (newWebAddr != oldWebAddr)
                     { bWebAddr = true; }
                     else
                     { bWebAddr = false; }
                 }
                if (sender == txtStrtDater)
                {
                    varnme = "STRTDATE";
                    newStrtdater = txtStrtDater.Text.ToString().Trim();
                    oldStrtdater = dodgeinitial.Strtdater.ToString().Trim();

                    if (newStrtdater == "" && cbStatCode.SelectedIndex == 3)
                    {
                        cbStatCode.SelectedIndex = 0;
                    }
                    else if (newStrtdater != oldStrtdater)
                    {
                        if (newStrtdater.Length == 6 && CheckStrtdater())
                        {
                            txtStrtDate.Text = newStrtdater;

                            if (Convert.ToInt32(newStrtdater) < Convert.ToInt32(GeneralFunctions.CurrentYearMon()) && cbStatCode.SelectedIndex == 3)
                                cbStatCode.SelectedIndex = 0;
                            else if (Convert.ToInt32(newStrtdater) >= Convert.ToInt32(GeneralFunctions.CurrentYearMon()) && cbStatCode.SelectedIndex != 3)
                                cbStatCode.SelectedIndex = 3;
                        }
                    }

                    if (newStrtdater != oldStrtdater)
                        bStrtdater = true;
                    else
                    { bStrtdater = false; }
                }
            }
             else if (sender is MaskedTextBox)
             {
                 txt = new TextBox();
                 newval = (sender as MaskedTextBox).Text;
                 anytxtmodified = true;

                 if (sender == txtPhone2)
                 {
                     varnme = "PHONE2";
                     newPhoneNum2 = txtPhone2.Text.ToString().Trim();
                    oldPhoneNum2 = dodgeinitial.Phone2.ToString().Trim();

                     if (newPhoneNum2 != oldPhoneNum2)
                     { bPhoneNum2 = true; }
                     else
                     { bPhoneNum2 = false; }
                 }

                 if (sender == txtFax)
                 {
                     varnme = "FAX";
                     newFaxNumber = txtFax.Text.ToString().Trim();
                    oldFaxNumber = dodgeinitial.Fax.ToString().Trim();

                    if (newFaxNumber != oldFaxNumber)
                     { bFaxNumber = true; }
                     else
                     { bFaxNumber = false; }
                 }

                 if (sender == txtPhone)
                 {
                     varnme = "PHONE";
                     newPhoneNum = txtPhone.Text.ToString().Trim();
                    oldPhoneNum = dodgeinitial.Phone.ToString().Trim(); 

                     if (newPhoneNum != oldPhoneNum)
                     { bPhoneNum = true; }
                     else
                     { bPhoneNum = false; }
                 }

                 if (sender == txtExt1)
                 {
                     varnme = "EXT";
                     newExt = txtExt1.Text.ToString().Trim();
                    oldExt = dodgeinitial.Ext.ToString().Trim();

                     if (newExt != oldExt)
                     { bExt = true; }
                     else
                     { bExt = false; }
                 }

                 if (sender == txtExt2)
                 {
                     varnme = "EXT2";
                     newExt2 = txtExt2.Text.ToString().Trim();
                    oldExt2 = dodgeinitial.Ext2.ToString().Trim(); 

                     if (newExt2 != oldExt2)
                     { bExt2 = true; }
                     else
                     { bExt2 = false; }
                 }

            }

             else if (sender is ComboBox)
             {
                 cbobox = new ComboBox();
                 newval = (sender as ComboBox).Text;
                 anytxtmodified = true;

                 if (sender == cbStatCode)
                 {
                    varnme = "STATUS";
                    newStatCode = cbStatCode.Text.ToString().Trim();
                    if (oldStatCode == null || oldStatCode == "")
                    {
                        oldStatCode = GetDisplayStatCodeText(dodgeinitial.Statuscode);
                    }

                     if (newStatCode != oldStatCode)
                     {
                         bStatcode = true;
                     }
                     else
                     { bStatcode = false; }
                 }

                 if (sender == txtLag)
                 {
                     varnme = "LAG";
                     newLag = txtLag.Text.ToString().Trim();
                      oldLag = dodgeinitial.Lag.ToString().Trim(); 

                     if (newLag != oldLag)
                     { bLag = true; }
                     else
                     { bLag = false; }
                 }

                 if (sender == cboCollTec)
                 {
                     varnme = "COLTEC";
                     newCollTec = cboCollTec.Text.ToString().Trim();
                     oldCollTec = dodgeinitial.Coltec.ToString().Trim(); 

                     if (newCollTec != oldCollTec)
                     { bCollTec = true; }
                     else
                     { bCollTec = false; }
                 }

                 //check if the survey code is 'M' accordingly use the textbox or combobox
                 if (sender == cboSurvey)
                 {
                    if (!ValidateNewtc())
                    {
                        MessageBox.Show("The Owner is not valid for this Newtc.");
                        cboSurvey.SelectedItem = old_text;
                        return;
                    }

                    varnme = "OWNER";
                     if (cboSurvey.Visible == false)
                     {
                         newSurvey = "M";
                     }
                     else
                     {
                         newSurvey = cboSurvey.Text.ToString().Trim();
                     }
                      oldSurvey = dodgeinitial.Survey.ToString().Trim();
 
                     if (newSurvey != oldSurvey)
                     { bSurvey = true; }
                     else
                     { bSurvey = false; }
                 }
             }
         }

         //from stat code code to get display text
         private string GetDisplayStatCodeText(string stat_code)
         {
             if (stat_code == "1")
                 statcode_text = "1-Active";
             else if (stat_code == "2")
                 statcode_text = "2-PNR";
             else if (stat_code == "3")
                 statcode_text = "3-DC PNR";
             else if (stat_code == "4")
                 statcode_text = "4-Abeyance";
             else if (stat_code == "5")
                 statcode_text = "5-Duplicate";
             else if (stat_code == "6")
                 statcode_text = "6-Out of Scope";
             else if (stat_code == "7")
                 statcode_text = "7-DC Refusal";
             else
                 statcode_text = "8-Refusal";
             return statcode_text;
         }

         private void btnRestore_Click(object sender, EventArgs e)
         {
             DialogResult dialogResult = MessageBox.Show("This will restore the contact fields to the original values. Continue?", "Question", MessageBoxButtons.YesNo);

             if (dialogResult == DialogResult.Yes)
             {
                 RestoreOriginalValues();
             }
         }

         /*************** C 30. RESTORE*******************/
         //Allow user to restore the original values for the contact fields
         //for the initial case being viewed from the dcpInitial Table.
         private void RestoreOriginalValues()
         {
           dodgeinitialrestore = DodgeInitialData.dcpInitial(Id);
             string rst = GetState(dodgeinitialrestore.OAddr3.Trim());
             txtZone.Text = GeneralDataFuctions.GetTimezone(rst);
            txtProjDesc.Text = dodgeinitialrestore.OProjdesc;
            txtProjLoc.Text = dodgeinitialrestore.OProjloc;
             txtPCitySt.Text = dodgeinitialrestore.OPcityst;
             txtContractNum.Text = dodgeinitialrestore.OContract;
             txtRespOrg.Text = dodgeinitialrestore.OResporg;
             txtFactOff.Text = dodgeinitialrestore.OFactoff;
             txtOthrResp.Text = dodgeinitialrestore.OOthrresp;
             txtAddr1.Text = dodgeinitialrestore.OAddr1;
             txtAddr2.Text = dodgeinitialrestore.OAddr2;
             txtAddr3.Text = dodgeinitialrestore.OAddr3;
             txtZip.Text = dodgeinitialrestore.OZip;
             txtRespnote.Text = dodgeinitialrestore.ORespnote;
             txtRespname.Text = dodgeinitialrestore.ORespname;
             txtPhone.Text = dodgeinitialrestore.OPhone;
             txtExt1.Text = dodgeinitialrestore.OExt;
             txtRespname2.Text = dodgeinitialrestore.ORespname2;
             txtPhone2.Text = dodgeinitialrestore.OPhone2;
             txtExt2.Text = dodgeinitialrestore.OExt2;
             txtFax.Text = dodgeinitialrestore.OFax;
             txtEmail.Text = dodgeinitialrestore.OEmail;
             txtWebUrl.Text = dodgeinitialrestore.OWeburl;

            bProjdesc = true;
            bProjloc = true;
            bProjcityst = true;
            bContractNum = true;
            bRespOrg = true;
            bFactOfficial = true;
            bOtherResp = true;
            bAddr1 = true;
            bAddr2 = true;
            bAddr3 = true;
            bZipCode = true;
            bSplnote = true;
            bPersontoCont = true;
            bPhoneNum = true;
            bExt = true;
            bFaxNumber = true;
            bPersontoCont2 = true;
            bExt2 = true;
            bEmailAddr = true;
            bWebAddr = true;

            anytxtmodified = true;
        }

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

        bool is_closing = false;
         private void frmDodgeInital_FormClosing(object sender, FormClosingEventArgs e)
         {
               // releaseAllLocks();
            //Update dcp_hist if cancel button is not clicked when user enters screen
            //If user has no permission to access this form, do not update case access record
            if (UserInfo.GroupCode != EnumGroups.NPCInterviewer && UserInfo.InitFD != "N" && UserInfo.InitNR != "N" && UserInfo.InitSL != "N")
            {
                id = Id;
                if (id != string.Empty && id != null)
                { UpdateCaseAccessRecord(); }
            }
            GeneralDataFuctions.AddCpraccessData(access_code, "EXIT");
            is_closing = true;
        }

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }
            if (editable && EditMode == TypeEditMode.Edit)
            {
                string inputEmail = txtEmail.Text;
                if (txtEmail.Text.Trim() != "")
                {
                    if (!GeneralFunctions.isEmail(inputEmail))
                    {
                        MessageBox.Show("Email address is invalid.");
                        txtEmail.Focus();
                        txtEmail.Text = dodgeinitial.Email;
                        done = false;
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
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (txtWebUrl.Text.Trim() != "")
                {
                    if (!GeneralData.IsValidURL(txtWebUrl.Text))
                    {
                        DialogResult result = MessageBox.Show("Web address is invalid.", "OK", MessageBoxButtons.OK);

                        if (result == DialogResult.OK)
                        {
                            txtWebUrl.Focus();
                            txtWebUrl.Text = dodgeinitial.Weburl;
                            done = false;
                        }
                    }
                }
            }
         }

         private void txtZip_Leave(object sender, EventArgs e)
         {
            if (btnRefresh.Focused)
            {
                return;
            }

            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (txtZip.Text.Trim() != "")
                {
                    bool isCanada = CheckAddress3IsCanada();

                    if ((isCanada && !GeneralData.IsCanadianZipCode(txtZip.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZip.Text.Trim())))
                    {
                        MessageBox.Show("Zip Code is invalid.");
                        txtZip.Focus();
                        txtZip.Text = dodgeinitial.Zip.Trim();
                        return;
                        
                    }
                }
                //else
                //{
                //    DialogResult result = MessageBox.Show("Zip Code is invalid.");
                //    if (result == DialogResult.OK)
                //    {
                //        txtZip.Focus();
                //        txtZip.Text = dodgeinitial.Zip.Trim();
                //        return;
                //    }
                //}
            }
        }

         private void txtPhone_Leave(object sender, EventArgs e)
         {
             if (btnRefresh.Focused)
             { return; }

            if (editable && EditMode == TypeEditMode.Edit)
            {
                if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
               {
                 MessageBox.Show("Phone number is invalid.");
                 txtPhone.Focus();
                 txtPhone.Text = oldPhoneNum;
                }
            }
         }

         private void txtPhone2_Leave(object sender, EventArgs e)
         {
             if (btnRefresh.Focused)
             { return; }
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtPhone2.Focus();
                        txtPhone2.Text = dodgeinitial.Phone2;
                        return;
                    }
                }
            }
         }

         private void txtPhone2_KeyDown(object sender, KeyEventArgs e)
         {
             if (editable && EditMode == TypeEditMode.Edit)
             {
                 if (e.KeyCode == Keys.Enter)
                 {
                     if (oldPhoneNum2 != txtPhone2.Text)
                     {
                         if ((!txtPhone2.MaskFull) && (txtPhone2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                         {
                             MessageBox.Show("Phone number is invalid.");
                             txtPhone2.Focus();
                             txtPhone2.Text = oldPhoneNum2;
                         }
                         else
                             oldPhoneNum2 = txtPhone2.Text;
                     }
                 }
             }
         }

         private void txtPhone2_Enter(object sender, EventArgs e)
         {
             if (sender is MaskedTextBox)
             {
                 (sender as MaskedTextBox).Focus();
                 (sender as MaskedTextBox).SelectionStart = 0;
             }
         }

         private void txtPhone_Enter(object sender, EventArgs e)
         {
             if (sender is MaskedTextBox)
             {
                 (sender as MaskedTextBox).Focus();
                 (sender as MaskedTextBox).SelectionStart = 0;
             }
         }

         private void txtPhone_KeyDown(object sender, KeyEventArgs e)
         {
             if (editable && EditMode == TypeEditMode.Edit)
             {
                 if (e.KeyCode == Keys.Enter)
                 {
                     if (oldPhoneNum != txtPhone.Text)
                     {
                         if ((!txtPhone.MaskFull) && (txtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                         {
                             MessageBox.Show("Phone number is invalid.");
                             txtPhone.Focus();
                             txtPhone.Text = oldPhoneNum;
                         }
                         else
                             oldPhoneNum = txtPhone.Text;
                     }
                 }
             }
         }

         private void txtFax_Enter(object sender, EventArgs e)
         {
             if (sender is MaskedTextBox)
             {
                 (sender as MaskedTextBox).Focus();
                 (sender as MaskedTextBox).SelectionStart = 0;
             }
         }

         private void txtFax_KeyDown(object sender, KeyEventArgs e)
         {
             if (editable && EditMode == TypeEditMode.Edit)
             {
                 if (e.KeyCode == Keys.Enter)
                 {
                     if (oldFaxNumber != txtFax.Text)
                     {
                         if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                         {
                             MessageBox.Show("Fax number is invalid.");
                             txtFax.Focus();
                             txtFax.Text = oldFaxNumber;
                         }
                         else
                             oldFaxNumber = txtFax.Text;
                     }
                 }
             }
         }

         private void txtFax_Leave(object sender, EventArgs e)
         {
             if (btnRefresh.Focused)
             {
                 return;
             }
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if ((!txtFax.MaskFull) && (txtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    DialogResult result = MessageBox.Show("Fax number is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtFax.Focus();
                        txtFax.Text = dodgeinitial.Fax;
                        return;
                    }
                }
            }
         }

         private void txtZip_Enter(object sender, EventArgs e)
         {
             txtZip.SelectionLength = 0;
         }

         private void txtAddr3_Leave(object sender, EventArgs e)
         {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (!ValidateCityState())
                {
                    txtAddr3.Focus();
                    txtAddr3.Text = dodgeinitial.Addr3;
                }
                PopulateRstate();
                txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
         }

         private void txtNewtc_Leave(object sender, EventArgs e)
         {
             //check the value entered against the NEWTCLIST table in the database.
             if (btnRefresh.Focused)
             {
                 return;
             }
            if (editable && EditMode == TypeEditMode.Edit)
            {
                string newtcval = txtNewtc.Text;

                if (!ValidateNewtc())
                {
                    MessageBox.Show("The Newtc value entered is invalid.");
                    txtNewtc.Focus();
                    txtNewtc.Text = dodgeinitial.Newtc;
                    done = false;
                    return;
                }
            }
         }
         private bool ValidateNewtc()
         {
             //check it validate newtc or not
             bool NewTCresult;
             NewTCresult = GeneralDataFuctions.CheckNewTC(txtNewtc.Text, cboSurvey.Text);
             return NewTCresult;
         }

         private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
         {
             e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
         }

         private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
         {
             GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
         }
        private void txtAddr1_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            { return; }
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (dodgeinitial.Addr1.Trim()!= "" && string.IsNullOrWhiteSpace(txtAddr1.Text))
                {
                    MessageBox.Show("Address is invalid.");
                    txtAddr1.Focus();
                    txtAddr1.Text = dodgeinitial.Addr1.Trim();
                    done = false;
                }
            }
        }

        string old_text = string.Empty;
        private void cbStatCode_Enter(object sender, EventArgs e)
        {
            //store old status
            old_text = cbStatCode.Text;
        }

        private void cbStatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newStatCode == "4-Abeyance" && txtStrtDater.Text.Trim() == "")
            {
                MessageBox.Show("Status change rejected, start date is blank!");
                cbStatCode.Text = old_text;
                cbStatCode.Focus();
                return;
            }
            else if (newStatCode == "4-Abeyance" && txtStrtDater.Text.Trim() != "" && ( Convert.ToInt32(txtStrtDater.Text.Trim()) < Convert.ToInt32(GeneralFunctions.CurrentYearMon())))
            {
                //Display TFU message for NPC users
                ////if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                ////{
                ////    MessageBox.Show("Status code changes not allowed, Please enter on TFU screen!");
                ////    cbStatCode.Text = oldStatCode;
                ////    cbStatCode.Focus();
                ////    return;
                ////}
                ////else
                ////{
                ////    MessageBox.Show("Status code changes not allowed, Please enter on C700 screen!");
                ////    cbStatCode.Text = oldStatCode;
                ////    cbStatCode.Focus();
                ////    return;
                ////}

                MessageBox.Show("Status change rejected, Project has not started!");
                cbStatCode.Text = old_text;
                cbStatCode.Focus();
                return; 
            }

            if (txtStrtDater.Text.Trim() != "" && (old_text == "4-Abeyance") && (newStatCode == "1-Active" || newStatCode == "2-PNR" || newStatCode == "3-DC PNR"
                || newStatCode == "7-DC Refusal" || newStatCode == "8-Refusal"))
            {
                if (Convert.ToInt32(txtStrtDater.Text.Trim()) >= Convert.ToInt32(GeneralFunctions.CurrentYearMon()))
                {
                    MessageBox.Show("Status change rejected, Project has not started!");
                    cbStatCode.SelectedValue = 4;
                    cbStatCode.Focus();
                    return;
                }
            }
        }

        private void txtPhone1s_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtPhone2s_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }


        private void txtRespIDs_Click(object sender, EventArgs e)
        {
            txtRespIDs.SelectionLength = 0;
            txtRespIDs.SelectionLength = txtRespIDs.Text.Length;
            txtRespIDs.Focus();
            txtRespIDs.Select(txtRespIDs.SelectionStart, txtRespIDs.SelectionLength);
        }

        private void txtRespIDs_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRespIDs_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 7))
                Search();
        }

        private void txtOwner_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtContact_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtEmails_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtPhone1s_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void txtPhone2s_KeyDown(object sender, KeyEventArgs e)
        {
            if (GeneralFunctions.TextBoxKeyDownCheckMaxLen(sender, e, 0))
                Search();
        }

        private void cboSurvey_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReplaceC_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Contractor info?", "Replace with Contractor Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtRespOrg.Text = nameaddrfactor.F7resporg;
                txtRespname.Text = nameaddrfactor.F7respname;
                txtAddr1.Text = nameaddrfactor.F7addr1;
                txtAddr2.Text = nameaddrfactor.F7addr2;
                txtAddr3.Text = nameaddrfactor.F7addr3;
                txtZip.Text = nameaddrfactor.F7zip;
                txtPhone.Text = nameaddrfactor.F7phone;
                txtEmail.Text = nameaddrfactor.F7email;
                txtWebUrl.Text = nameaddrfactor.F7weburl;
                if (nameaddrfactor.F7respname != " ")
                    txtFactOff.Text = "ATTN " + nameaddrfactor.F7respname;
                else
                    txtFactOff.Text = "";
                txtOthrResp.Text = "";
                txtFax.Text = "";
                txtRespname2.Text = "";
                txtPhone2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceO2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Owner2 info?", "Replace with Owner2 Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtRespOrg.Text = nameaddrfactor.F9resporg;
                txtRespname.Text = nameaddrfactor.F9respname;
                txtAddr1.Text = nameaddrfactor.F9addr1;
                txtAddr2.Text = nameaddrfactor.F9addr2;
                txtAddr3.Text = nameaddrfactor.F9addr3;
                txtZip.Text = nameaddrfactor.F9zip;
                txtPhone.Text = nameaddrfactor.F9phone;
                txtEmail.Text = nameaddrfactor.F9email;
                txtWebUrl.Text = nameaddrfactor.F9weburl;
                if (nameaddrfactor.F9respname != " ")
                    txtFactOff.Text = "ATTN " + nameaddrfactor.F9respname;
                else
                    txtFactOff.Text = "";
                txtOthrResp.Text = "";
                txtFax.Text = "";
                txtRespname2.Text = "";
                txtPhone2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceE_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Engineer info?", "Replace with Engineer Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtRespOrg.Text = nameaddrfactor.F5resporg;
                txtRespname.Text = nameaddrfactor.F5respname;
                txtAddr1.Text = nameaddrfactor.F5addr1;
                txtAddr2.Text = nameaddrfactor.F5addr2;
                txtAddr3.Text = nameaddrfactor.F5addr3;
                txtZip.Text = nameaddrfactor.F5zip;
                txtPhone.Text = nameaddrfactor.F5phone;
                txtEmail.Text = nameaddrfactor.F5email;
                txtWebUrl.Text = nameaddrfactor.F5weburl;
                if (nameaddrfactor.F5respname != " ")
                    txtFactOff.Text = "ATTN " + nameaddrfactor.F5respname;
                else
                    txtFactOff.Text = "";
                txtOthrResp.Text = "";
                txtFax.Text = "";
                txtRespname2.Text = "";
                txtPhone2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceA_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Architect info?", "Replace with Architect Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtRespOrg.Text = nameaddrfactor.F4resporg;
                txtRespname.Text = nameaddrfactor.F4respname;
                txtAddr1.Text = nameaddrfactor.F4addr1;
                txtAddr2.Text = nameaddrfactor.F4addr2;
                txtAddr3.Text = nameaddrfactor.F4addr3;
                txtZip.Text = nameaddrfactor.F4zip;
                txtPhone.Text = nameaddrfactor.F4phone;
                txtEmail.Text = nameaddrfactor.F4email;
                txtWebUrl.Text = nameaddrfactor.F4weburl;
                if (nameaddrfactor.F4respname != " ")
                    txtFactOff.Text = "ATTN " + nameaddrfactor.F4respname;
                else
                    txtFactOff.Text = "";
                txtOthrResp.Text = "";
                txtFax.Text = "";
                txtRespname2.Text = "";
                txtPhone2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceO_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Owner info?", "Replace with Owner Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtRespOrg.Text = nameaddrfactor.F3resporg;
                txtRespname.Text = nameaddrfactor.F3respname;
                txtAddr1.Text = nameaddrfactor.F3addr1;
                txtAddr2.Text = nameaddrfactor.F3addr2;
                txtAddr3.Text = nameaddrfactor.F3addr3;
                txtZip.Text = nameaddrfactor.F3zip;
                txtPhone.Text = nameaddrfactor.F3phone;
                txtEmail.Text = nameaddrfactor.F3email;
                txtWebUrl.Text = nameaddrfactor.F3weburl;
                if (nameaddrfactor.F3respname != " ")
                    txtFactOff.Text = "ATTN " + nameaddrfactor.F3respname;
                else
                    txtFactOff.Text = "";
                txtOthrResp.Text = "";
                txtFax.Text = "";
                txtRespname2.Text = "";
                txtPhone2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void SetReplaceButtons()
        {
            //Check to see if there is NP case, disable all source buttons
            string FINCheck = dodgeinitial.Fin.Substring(0, 2);
            if (FINCheck == "66")
            {
                //Gray out REPLACE, SLIP and SOURCE buttons.
                btnReplaceC.Enabled = false;
                btnReplaceO.Enabled = false;
                btnReplaceA.Enabled = false;
                btnReplaceE.Enabled = false;
                btnReplaceO2.Enabled = false;
            }
            else
            {
                //disable owner button if there is no F3
                if (string.IsNullOrWhiteSpace(nameaddrfactor.F3resporg) && string.IsNullOrWhiteSpace(nameaddrfactor.F3respname))
                    btnReplaceO.Enabled = false;
                else
                    btnReplaceO.Enabled = true;

                //disable architect button if there is no F4
                if (string.IsNullOrWhiteSpace(nameaddrfactor.F4resporg) && string.IsNullOrWhiteSpace(nameaddrfactor.F4respname))
                    btnReplaceA.Enabled = false;
                else
                    btnReplaceA.Enabled = true;

                //disable engineer button if there is no F5
                if (string.IsNullOrWhiteSpace(nameaddrfactor.F5resporg) && string.IsNullOrWhiteSpace(nameaddrfactor.F5respname))
                    btnReplaceE.Enabled = false;
                else
                    btnReplaceE.Enabled = true;

                //disable contractor button if there is no F7
                if (string.IsNullOrWhiteSpace(nameaddrfactor.F7resporg) && string.IsNullOrWhiteSpace(nameaddrfactor.F7respname))
                    btnReplaceC.Enabled = false;
                else
                    btnReplaceC.Enabled = true;

                //disable owner2 button if there is no F9
                if (string.IsNullOrWhiteSpace(nameaddrfactor.F9resporg) && string.IsNullOrWhiteSpace(nameaddrfactor.F9respname))
                    btnReplaceO2.Enabled = false;
                else
                    btnReplaceO2.Enabled = true;
               
            }
        }

        private void txtStrtDater_Enter(object sender, EventArgs e)
        {
            old_text = txtStrtDater.Text;
        }
        private void txtStrtDater_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        private void txtStrtDater_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }
            if (txtStrtDater.Text.Trim() != "")
            {
                if (!CheckStrtdater())
                    return;
            }
        }

        private bool CheckStrtdater()
        {
            if (!ValidateDate(txtStrtDater.Text.Trim()))
            {
                MessageBox.Show("Not a valid Start Date.");
                txtStrtDater.Focus();
                txtStrtDater.Text = old_text;
                done = false;
                return false;
            }
            else
            {
                DateTime strt = DateTime.ParseExact(txtStrtDater.Text.Trim(), "yyyyMM", CultureInfo.InvariantCulture);
                DateTime sel = DateTime.ParseExact(txtSelDate.Text, "yyyyMM", CultureInfo.InvariantCulture);

                if (GeneralFunctions.GetNumberMonths(strt, sel) > 24)
                {
                    MessageBox.Show("Start Date cannot be earlier than 24 months before Selection Date.");
                    txtStrtDater.Focus();
                    txtStrtDater.Text = old_text;
                    done = false;
                    return false;
                }
            }

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

        

        private void btnRef_Click(object sender, EventArgs e)
        {
            frmReferral fReferral = new frmReferral();

            fReferral.Id = txtId.Text;
            if (txtRespid.Text != "")
                fReferral.Respid = txtRespid.Text;
            else
                fReferral.Respid = txtId.Text;

            fReferral.ShowDialog();  //show child

            lblReferral.Visible = refdata.CheckReferralExist(fReferral.Id, fReferral.Respid);
        }

        private void cboSurvey_Enter(object sender, EventArgs e)
        {
            //store old status
            old_text = cboSurvey.SelectedItem.ToString();
        }

        private void dgInitialsSrch1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtPCitySt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void chkpending_CheckedChanged(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                if (chkpending.Checked)
                    chkComplete.Enabled = false;
                else
                    chkComplete.Enabled = true;
            }

        }
    }
}
