/*********************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmName.cs	    	

Programmer:         Srini Natarajan

Creation Date:      04/20/2015

Inputs:             CprsBLL.NameAddr.cs
                    csdlist

Parameters:		    Id, DisplayType, CallingForm.

Outputs:		    Name and Address Display and List (List of csd numbers)	

Description:	    This program displays the Name and Address data for a selected ID from various search screens 
                    Search Screen. Allows users to edit data on records that are not locked by another user.

Detailed Design:    Detailed User Requirements for Name and Address Display Screen 

Other:	            Called from: frmMaster.cs, frmC700Srch.cs, frmAdhoc.cs,
 *                  frmSample.cs, frmRespondent.cs
 
Revision History:	
*********************************************************************
 Modified Date :  October 11, 2017
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR278
 Description   :  Add buttons for Replacing Contact Information using
                  Owner, Architect, Engineer, Contractor or Owner2 from Factors
**********************************************************************
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
Modified Date :  08/25/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : fix the bug save shed_hist without accestms 
***********************************************************************************
 Modified Date :  04/26/2021
 Modified By   :  Christine
 Keyword       :  
 Change Request: CR
 Description   : fix the bug save runit to audit and update costpu
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
    public partial class frmName : Cprs.frmCprsParent
    {
        /****** public properties *******/
/* Required */
public string Id;
        public Form CallingForm = null;
 
        /* Optional */
        public List<string> Idlist = null;
        public int CurrIndex = 0;
        public static frmName Current;

        public TypeEditMode EditMode = TypeEditMode.Edit;
        //------------------------------------

        /*flag to use closing the calling form */
        private bool call_callingFrom = false;

        private string Respid;
        private string OrigRespid;
        private string viewcode;
        private string coltec_code;
        string coltec_text = string.Empty;
        private string stat_code;
        string statcode_text = string.Empty;

        /*Textbox and Combobox to flag changes before saving data and auditing */
        TextBox txt;
        ComboBox cbobox;
        
        private bool done = true;
        private bool notvalid;
        private bool editable = false;

        private delegate void ShowLockMessageDelegate();
        private string locked_by = String.Empty;
        private NameData namedata;
        private HistoryData hd;

        private DataTable NamProjCommlist;
        private DataTable NamRespCommlist;

        private List<ProjMark> pmarklist = new List<ProjMark>();
        private List<RespMark> rmarklist = new List<RespMark>();

        private ProjMark pmark;
        private ProjMarkData pmarkdata;
        private RespMark rmark;
        private RespMarkData rmarkdata;
        private Soc soc;
        private SocData socdata;

        RespAuditData radata = new RespAuditData();

         /* global Variables*/
        private CsdaccessData csda;

        private bool form_done = false;

        private MySector ms;

        public frmName()
        {
            Current = this;
            InitializeComponent();
            tabControl1.DrawMode = TabDrawMode.OwnerDrawFixed;
            anytxtmodified = false;
        }
        NameAddr nameaddr;
        NameAddrResp RespDetails;
        Factor nameaddrfactor;

        private void frmName_Load(object sender, EventArgs e)
        {
            resetfields();
            RemoveTxtChanged();
            SelViewCode();

            label55.Text = "CUI//SP-CENS" + "\n" + "DISCLOSURE PROHIBITED: TITLE 13 USC";

            namedata = new NameData();
            nameaddr = NameData.GetNameAddr(Id, viewcode);
            nameaddrfactor = SourceData.GetFactor(nameaddr.Masterid);
            /*Soc */
            socdata = new SocData();
            if (nameaddr.Survey == "M")
                soc = socdata.GetSocData(nameaddr.Masterid);

            if (nameaddr.Respid == "")
                 Respid = nameaddr.Id;
            else
                Respid = nameaddr.Respid;

            //get my sector data
            MySectorsData md = new MySectorsData();
            ms = md.GetMySectorData(UserInfo.UserName);

            //store the original respid for restoring from REFRESH button.
            /*Project marks */
            pmarklist.Clear();
            rmarklist.Clear();

            pmarkdata = new ProjMarkData();
            pmark = pmarkdata.GetProjmarkData(Id);
            pmarklist.Add(pmark);
            rmarkdata = new RespMarkData();
            rmark = rmarkdata.GetRespmarkData(Respid);
            rmarklist.Add(rmark);

            hd = new HistoryData();

            /*Add record to csdaccess */
            csda = new CsdaccessData();

            OrigRespid = Respid;

            /*lock Respondent */
            locked_by = GeneralDataFuctions.ChkRespIDIsLocked(Respid);
            if (String.IsNullOrEmpty(locked_by))
            {
                bool locked = GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
                editable = true;
                lblLockedBy.Visible = false;

            }
            else
            {
                editable = false;
                lblLockedBy.Visible = true;
            }
            
            DisplayAddress();
            
            /* If there is a list, set count boxes */
            this.tabControl1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl1_DrawItem);
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
          
        }

        Bitmap memoryImage;
        private void btnPrint_Click(System.Object sender, System.EventArgs e)
        {
            
            this.Update();
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
            //release the lock of old respids
            if (bRespid)
            {
                Respid = OrigRespid;
                relsRespLock();
                ChkNumProjForRespid(OrigRespid);
                Respid = newRespid;
                relsRespLock();
                Respid = oldRespid;
                relsRespLock();
            }
            chkBrowseUpdate();
            if (txtRespid.Text == "")
                Respid = Id;
            else
                Respid = txtRespid.Text;
            txtModifiedStatus();
            if (notvalid)
            { return; }

            //if calling form is C700 or Tfu, don't need unlock respid
            if (CallingForm == null || (CallingForm != null && CallingForm.Name != "frmC700"))
                relsRespLock();

            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }
            this.Close();
        }

        //Return updated Respid, for calling form
        public string GetUpdatedRespid()
        {
            if (txtRespid.Text == "")
                return Id;
            else
                return txtRespid.Text;
        }

        //based on the viewcode select the View from database to display. (Check which table the ID is in).
        private void SelViewCode()
        {
            if (NameData.CheckIdSampleHold(Id))
            {
                //If true use Name_addr_Display_ER view
                viewcode = "2";
                label2.Visible = true;
                label2.Text = "REJECT";
            }
            else
            {
                //Use Name_Addr_Display view
                viewcode = "1";
                label2.Visible = false;
            }
            return;
        }

        private void DisplayAddress()
        {
            RemoveTxtChanged();
            anytxtmodified = false;
            txtId.Text = nameaddr.Id;
            if (bRespid)
            {
                if (txtRespid.Text == "")
                { Respid = Id; }
                else
                { txtRespid.Text = Respid; }
            }
            else
            {
                if (nameaddr.Respid == "")
                { Respid = nameaddr.Id; }
                else
                { Respid = nameaddr.Respid; }
            }
            if (Id == Respid)
            { txtRespid.Text = ""; }
            else
            { txtRespid.Text = Respid; }
    
            txtSeldate.Text = nameaddr.Seldate;
            txtSelvalue.Text = nameaddr.Selvalue;
            int SelVal = Convert.ToInt32(txtSelvalue.Text);
            txtSelvalue.Text = string.Format("{0:#,###}", SelVal);
            
            txtFipsSt.Text = nameaddr.Fipstate;
            txtCnty.Text = nameaddr.Dodgecou;

            if (nameaddr.Survey == "M")
            {
                txtSurvey.Visible = true;
                txtSurvey.Text = nameaddr.Survey;
                txtSurvey.ReadOnly = true;
                

                txtRepBldgs.Text = nameaddr.Rbldgs;
                txtRepUnits.Text = Convert.ToInt32(nameaddr.Runits).ToString("N0");

                txtRepBldgs.ReadOnly = false;
                txtRepBldgs.TabStop  = true;
                txtRepUnits.ReadOnly = false;
                txtRepUnits.TabStop = true;

            }
            else
            {
                txtSurvey.Visible = true;
                txtSurvey.Text = nameaddr.Survey;
                txtSurvey.ReadOnly = true;
               

                txtRepBldgs.Text = "";
                txtRepUnits.Text = "";

                txtRepBldgs.ReadOnly = true;
                txtRepBldgs.TabStop = false;
                txtRepUnits.ReadOnly = true;
                txtRepUnits.TabStop = false;

            }


            if (nameaddr.Statuscode == "5" && nameaddr.Survey == "M")
            { cboStatCode.Enabled = false; }
            txtFwgt.Text = nameaddr.Fwgt;
            txtNewTC.Text = nameaddr.Newtc;

            txtSourceCode.Text = nameaddr.Source;

            txtFIN.Text = nameaddr.Fin;
            txtContractNum.Text = nameaddr.Contract;
            txtProjDesc.Text = nameaddr.ProjDesc;
            txtProjLoc.Text = nameaddr.Projloc;
            txtProjCitySt.Text = nameaddr.Pcityst;
            txtFactorOfficial.Text = nameaddr.Factoff;
            txtOtherResp.Text = nameaddr.Othrresp;
            txtAddr1.Text = nameaddr.Addr1;
            txtAddr2.Text = nameaddr.Addr2;
            txtCityState.Text = nameaddr.Addr3;
            txtZipcode.Text = nameaddr.Zip;
            txtOwner.Text = nameaddr.Resporg;
            txtPersontoCont1.Text = nameaddr.Respname;
            txtPersontoCont2.Text = nameaddr.Respname2;
            mtxtPhoneNum1.Text = nameaddr.Phone;
            mtxtPhoneNum2.Text = nameaddr.Phone2;
            txtExt1.Text = nameaddr.Ext;
            txtExt2.Text = nameaddr.Ext2;
            mtxtFaxNumber.Text = nameaddr.Fax;
            txtSplnote.Text = nameaddr.Respnote;
            txtEmailAddr.Text = nameaddr.Email;
            txtWebAddr.Text = nameaddr.Weburl;
            txtRvitm5cr.Text = nameaddr.Rvitm5cr;

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            { btnMark.Enabled = false; }
            if (txtRvitm5cr.Text != "0")
            {
                int Rit5cr = Convert.ToInt32(txtRvitm5cr.Text);
                txtRvitm5cr.Text = string.Format("{0:#,###}", Rit5cr);
            }
            txtRvitm5c.Text = nameaddr.Rvitm5c;
            if (txtRvitm5c.Text != "0")
            {
                int Rit5c = Convert.ToInt32(txtRvitm5c.Text);
                txtRvitm5c.Text = string.Format("{0:#,###}", Rit5c);
            }
            txtFlagR5c.Text = nameaddr.Flagr5c;
            
            MonthVIP();

            txtStrtDater.Text = nameaddr.Strtdater;
            txtStrtDate.Text = nameaddr.Strtdate;
            txtFlagStrtDate.Text = nameaddr.Flagstrtdate;
            txtCompDater.Text = nameaddr.Compdater;
            txtCompDate.Text = nameaddr.Compdate;
            txtFlagCompDate.Text = nameaddr.Flagcompdate;
            txtfutCompDr.Text = nameaddr.Futcompdr;
            txtFutCompD.Text = nameaddr.Futcompd;
            txtFlagFutCompD.Text = nameaddr.Flagfutcompd;
            txtColhist.Text = nameaddr.Colhist;
            cboLag.Text = nameaddr.Lag;

            //Coltec should display with description with only the code saved.
            coltec_code = nameaddr.Coltec;
            cboCollTec.SelectedItem = GetDisplayColtecText(coltec_code); //resp.Coltec);
            cboCollTec.Text = coltec_text;

            //Stat code should display with description with only the code saved.
            stat_code = nameaddr.Statuscode;
            cboStatCode.SelectedItem = GetDisplayStatCodeText(stat_code); //resp.Coltec);
            cboStatCode.Text = statcode_text;
           
            //check for lock by someone
            if (editable)
               EnableEdits();
             else
               DisableEdits();
                
            chkReferral();
            txtTimeZone.Text = nameaddr.Timezone;

            /*set up referer button based on my sector */
            if (ms != null && !ms.CheckInMySector(txtNewTC.Text))
                btnReferral.Enabled = false;
            else
                btnReferral.Enabled = true;

            //Check to see if there is NP case, disable all source buttons
            string FINCheck = nameaddr.Fin.Substring(0, 2);
            if (FINCheck == "66")
            {
                btnSlip.Enabled = false;
                btnSource.Enabled = false;
            }
            else
            {
                //Disable Slip button if there is no Dodge info for this project.
                if (!GeneralDataFuctions.CheckDodgeSlip(nameaddr.Masterid))
                    btnSlip.Enabled = false;
                else
                    btnSlip.Enabled = true;
                btnSource.Enabled = true;
            }

            //Check calling form, if it is Tfu, disable prev case and next case buttons
            if (CallingForm.Name == "frmTfu")
            {
                btnPrevCase.Enabled = false;
                btnNextCase.Enabled = false;
                btnC700.Enabled = false;
            }
            
                /* Display the Project comment tab*/
                NamProjCommlist = hd.GetProjCommentTable(Id, true);
            if (NamProjCommlist.Rows.Count != 0)
            {
                dgProjComments.ColumnHeadersVisible = false;
                dgProjComments.DataSource = NamProjCommlist;
                dgProjComments.Columns[0].Width = 75;
                dgProjComments.Columns[1].Width = 60;
            }
            else
            {
                dgProjComments.DataSource = null;
            }

            NamRespCommlist = hd.GetRespCommentTable(Respid, true);
            if (NamRespCommlist.Rows.Count != 0)
            {
                dgRespComments.ColumnHeadersVisible = false;
                dgRespComments.DataSource = NamRespCommlist;
            }
            else
            {
                dgRespComments.DataSource = null;
            }

            lblMark.Text = "";
            
            /*display the Project Mark */
            
           if ( CheckMarkExists())
            {
                lblMark.Text = "MARKED";
            }
            else
            {
                lblMark.Text = "";
            }

            /* Display the Respondent Mark tab*/
           
            if (CheckMarkExists())
            {
                lblMark.Text = "MARKED";
            }
            else
            {
                lblMark.Text = "";
            }
            
            this.tabControl2.SelectedIndex = 0;
            this.tabControl1.SelectedIndex = 0;
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
            chkUserGroup();
            BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
            SetTxtChanged();
            SetButtonTxt();
        }


        //set replace buttons
        private void SetReplaceButtons()
        {
            //Check to see if there is NP case, disable all source buttons
            string FINCheck = nameaddr.Fin.Substring(0, 2);
            if (FINCheck == "66")
            {
                //Gray out REPLACE buttons.
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

                if (txtSurvey.Text == "M")
                {
                    btnReplaceC.Enabled = false;
                    btnReplaceA.Enabled = false;
                    btnReplaceE.Enabled = false;
                    btnReplaceO2.Enabled = false;
                }
                else
                {
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
        }

        private void MonthVIP()
            {
                string AccCode = nameaddr.Accescde;
                string MonVIPDataId = nameaddr.Id;
                string vipflag;
                vipflag = NameData.ChkMonthVIPFlag(MonVIPDataId);
                if (vipflag == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flag5a == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flag5b == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flagcap == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flagitm6 == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flagr5c == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flagstrtdate == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flagcompdate == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (nameaddr.Flagfutcompd == "A")
                {
                    lblAccscode.Text = "ANALYST";
                }
                else if (AccCode == "")
                {
                    lblAccscode.Text = "";
                }
                else if (AccCode == "F")
                {
                    lblAccscode.Text = "FORM ENTRY";
                }
                else if (AccCode == "C")
                {
                    lblAccscode.Text = "CENTURION";
                }
                else if (AccCode == "P")
                {
                    lblAccscode.Text = "PHONE";
                }
                else if (AccCode == "I")
                {
                     lblAccscode.Text = "INTERNET";
                }
                else if (AccCode == "A")
                {
                     lblAccscode.Text = "ADMIN";
                }
                else
                {
                    lblAccscode.Text = " ";
                }
            }
 
        private void ShowLockMessage()
        {
            /*show message if the case locked by someone */
            if (locked_by != "")
                MessageBox.Show("The case is locked by " + locked_by + ", cannot be edited.");
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
            btnRefresh.Enabled = false;
            txtSeldate.ReadOnly = true;
            txtSelvalue.ReadOnly = true;
            cboStatCode.Enabled = false;
            txtFipsSt.ReadOnly = true;
           
            txtFwgt.ReadOnly = true;
            txtNewTC.ReadOnly = true;
            btnNewtc.Enabled = false;
            txtSourceCode.ReadOnly = true;
            txtRepBldgs.ReadOnly = true;
            txtRepUnits.ReadOnly = true;
            txtContractNum.ReadOnly = true;
            txtProjDesc.ReadOnly = true;
            txtProjLoc.ReadOnly = true;
            txtProjCitySt.ReadOnly = true;
            txtFactorOfficial.ReadOnly = true;
            txtOtherResp.ReadOnly = true;
            txtAddr1.ReadOnly = true;
            txtAddr2.ReadOnly = true;
            txtCityState.ReadOnly = true;
            txtZipcode.ReadOnly = true;
            txtOwner.ReadOnly = true;
            txtPersontoCont1.ReadOnly = true;
            txtPersontoCont2.ReadOnly = true;
            txtSplnote.ReadOnly = true;
            mtxtPhoneNum1.ReadOnly = true;
            mtxtPhoneNum2.ReadOnly = true;
            txtExt1.ReadOnly = true;
            txtExt2.ReadOnly = true;
            mtxtFaxNumber.ReadOnly = true;
            txtEmailAddr.ReadOnly = true;
            txtWebAddr.ReadOnly = true;
            txtRvitm5cr.ReadOnly = true;
            txtRvitm5c.ReadOnly = true;
            txtFlagR5c.ReadOnly = true;
            txtStrtDater.ReadOnly = true;
            txtStrtDate.ReadOnly = true;
            txtFlagStrtDate.ReadOnly = true;
            txtCompDater.ReadOnly = true;
            txtCompDate.ReadOnly = true;
            txtFlagCompDate.ReadOnly = true;
            txtfutCompDr.ReadOnly = true;
            txtFutCompD.ReadOnly = true;
            txtFlagFutCompD.ReadOnly = true;
            cboCollTec.Enabled = false;
            cboLag.Enabled = false;
            txtColhist.ReadOnly = true;
            txtTimeZone.ReadOnly = true;

            //Change the color to gray.
            txtRespid.BackColor = SystemColors.Control;
            txtSeldate.BackColor = SystemColors.Control;
            txtSelvalue.BackColor = SystemColors.Control;
            cboStatCode.BackColor = SystemColors.Control;
            txtFipsSt.BackColor = SystemColors.Control;
            
            txtFwgt.BackColor = SystemColors.Control;
            txtNewTC.BackColor = SystemColors.Control;
            txtSourceCode.BackColor = SystemColors.Control;
            txtRepBldgs.BackColor = SystemColors.Control;
            txtRepUnits.BackColor = SystemColors.Control;
            txtContractNum.BackColor = SystemColors.Control;
            txtProjDesc.BackColor = SystemColors.Control;
            txtProjLoc.BackColor = SystemColors.Control;
            txtProjCitySt.BackColor = SystemColors.Control;
            txtFactorOfficial.BackColor = SystemColors.Control;
            txtOtherResp.BackColor = SystemColors.Control;
            txtAddr1.BackColor = SystemColors.Control;
            txtAddr2.BackColor = SystemColors.Control;
            txtCityState.BackColor = SystemColors.Control;
            txtZipcode.BackColor = SystemColors.Control;
            txtOwner.BackColor = SystemColors.Control;
            txtPersontoCont1.BackColor = SystemColors.Control;
            txtPersontoCont2.BackColor = SystemColors.Control;
            txtSplnote.BackColor = SystemColors.Control;
            mtxtPhoneNum1.BackColor = SystemColors.Control;
            mtxtPhoneNum2.BackColor = SystemColors.Control;
            txtExt1.BackColor = SystemColors.Control;
            txtExt2.BackColor = SystemColors.Control;
            mtxtFaxNumber.BackColor = SystemColors.Control;
            txtEmailAddr.BackColor = SystemColors.Control;
            txtWebAddr.BackColor = SystemColors.Control;
            //txtRvitm5cr.BackColor = SystemColors.Control;
            //txtRvitm5c.BackColor = SystemColors.Control;
            //txtFlagR5c.BackColor = SystemColors.Control;
            //txtStrtDater.BackColor = SystemColors.Control;
            //txtStrtDate.BackColor = SystemColors.Control;
            //txtFlagStrtDate.BackColor = SystemColors.Control;
            //txtCompDater.BackColor = SystemColors.Control;
            //txtCompDate.BackColor = SystemColors.Control;
            //txtFlagCompDate.BackColor = SystemColors.Control;
            //txtfutCompDr.BackColor = SystemColors.Control;
            //txtFutCompD.BackColor = SystemColors.Control;
            //txtFlagFutCompD.BackColor = SystemColors.Control;
            cboCollTec.BackColor = SystemColors.Control;
            txtColhist.BackColor = SystemColors.Control;
            txtTimeZone.BackColor = SystemColors.Control;
            cboLag.BackColor = SystemColors.Control;
            this.Refresh();
            Application.DoEvents();
        }

        private void EnableEdits()
        {
            //Enable the fields for editing
            btnRespid.Enabled = true;
            btnRefresh.Enabled = true;
            
            //set source buttons
            SetReplaceButtons();

            if (txtSurvey.Text == "M")
            {
                txtRepBldgs.ReadOnly = false;
                txtRepBldgs.BackColor = Color.White;
                txtRepUnits.ReadOnly = false;
                txtRepUnits.BackColor = Color.White;
            }
            else
            {
                txtRepBldgs.ReadOnly = true;
                txtRepBldgs.BackColor = SystemColors.Control;
                txtRepBldgs.TabStop = false;
                txtRepUnits.ReadOnly = true;
                txtRepUnits.BackColor = SystemColors.Control;
                txtRepUnits.TabStop = false;
            }
            txtContractNum.ReadOnly = false;
            txtProjDesc.ReadOnly = false;
            txtProjLoc.ReadOnly = false;
            txtProjCitySt.ReadOnly = false;
            txtFactorOfficial.ReadOnly = false;
            txtOtherResp.ReadOnly = false;
            txtAddr1.ReadOnly = false;
            txtAddr2.ReadOnly = false;
            txtCityState.ReadOnly = false;
            txtZipcode.ReadOnly = false;
            txtOwner.ReadOnly = false;
            txtPersontoCont1.ReadOnly = false;
            txtPersontoCont2.ReadOnly = false;
            txtSplnote.ReadOnly = false;
            mtxtPhoneNum1.ReadOnly = false;
            mtxtPhoneNum2.ReadOnly = false;
            txtExt1.ReadOnly = false;
            txtExt2.ReadOnly = false;
            mtxtFaxNumber.ReadOnly = false;
            txtEmailAddr.ReadOnly = false;
            txtWebAddr.ReadOnly = false;
            txtStrtDater.ReadOnly = true;
            txtStrtDate.ReadOnly = true;
            txtFlagStrtDate.ReadOnly = true;
            txtfutCompDr.ReadOnly = true;
            txtFutCompD.ReadOnly = true;
            txtFlagFutCompD.ReadOnly = true;
            cboCollTec.Enabled = true;
            cboLag.Enabled = true;

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                txtNewTC.ReadOnly = true;               
                txtNewTC.BackColor = SystemColors.Control;
            }
            else
            {
                txtNewTC.ReadOnly = false;               
                txtNewTC.BackColor = Color.White;
            }

            //Change background color to white
            cboStatCode.BackColor = Color.White;
            
            txtContractNum.BackColor = Color.White;
            txtProjDesc.BackColor = Color.White;
            txtProjLoc.BackColor = Color.White;
            txtProjCitySt.BackColor = Color.White;
            txtFactorOfficial.BackColor = Color.White;
            txtOtherResp.BackColor = Color.White;
            txtAddr1.BackColor = Color.White;
            txtAddr2.BackColor = Color.White;
            txtCityState.BackColor = Color.White;
            txtZipcode.BackColor = Color.White;
            txtOwner.BackColor = Color.White;
            txtPersontoCont1.BackColor = Color.White;
            txtPersontoCont2.BackColor = Color.White;
            txtSplnote.BackColor = Color.White;
            mtxtPhoneNum1.BackColor = Color.White;
            mtxtPhoneNum2.BackColor = Color.White;
            txtExt1.BackColor = Color.White;
            txtExt2.BackColor = Color.White;
            mtxtFaxNumber.BackColor = Color.White;
            txtEmailAddr.BackColor = Color.White;
            txtWebAddr.BackColor = Color.White;
            cboCollTec.BackColor = Color.White;
            cboLag.BackColor = Color.White;
            this.Refresh();
            Application.DoEvents();
        }

        private void resetfields()
        {
            txtId.Text = "";
            txtRespid.Text = "";
            OrigRespid = "";
            newSelRespid = "";
            newExistRespid = "";
            delExistRespid = "";
            txtSeldate.Text = "";
            txtSelvalue.Text = "";
            cboStatCode.Enabled = true;
            cboStatCode.Text = "";
            cboLag.Text = "";
            txtFipsSt.Text = "";
          
            txtFwgt.Text = "";
            txtNewTC.Text = "";
            txtSourceCode.Text = "";
            txtRepBldgs.Text = "";
            txtRepUnits.Text = "";
            txtContractNum.Text = "";
            txtProjDesc.Text = "";
            txtProjLoc.Text = "";
            txtProjCitySt.Text = "";
            txtOwner.Text = "";
            txtFactorOfficial.Text = "";
            txtOtherResp.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtCityState.Text = "";
            txtZipcode.Text = "";
            txtPersontoCont1.Text = "";
            txtPersontoCont2.Text = "";
            txtSplnote.Text = "";
            mtxtPhoneNum1.Text = "";
            mtxtPhoneNum2.Text = "";
            txtExt1.Text = "";
            txtExt2.Text = "";
            mtxtFaxNumber.Text = "";
            txtEmailAddr.Text = "";
            txtWebAddr.Text = "";
            txtRvitm5cr.Text = "";
            txtRvitm5c.Text = "";
            txtFlagR5c.Text = "";
            txtStrtDater.Text = "";
            txtStrtDate.Text = "";
            txtFlagStrtDate.Text = "";
            txtCompDater.Text = "";
            txtCompDate.Text = "";
            txtFlagCompDate.Text = "";
            txtfutCompDr.Text = "";
            txtFutCompD.Text = "";
            txtFlagFutCompD.Text = "";
            lblAccscode.Text = "";
            cboCollTec.Text = "";
            txtColhist.Text = "";
           // lblLockedBy.Text = "";
            txtFIN.Text = "";
            numRespid = "";
            
            ParentIdPopupOpen = false;
            ResetAuditVariables();
        }

        //If Stat Code = 5 and Owner = Multi family then disable and popup a window.
        private void ChkStatCodeFive()
        {
            if (ParentIdPopupOpen == false)
            {
                if (cboStatCode.Text.Substring(0, 1) == "5" && txtSurvey.Text == "M")
                {
                    ParentIdPopupOpen = true;
                    frmParentIdPopup popup = new frmParentIdPopup(Id, nameaddr.Masterid);
                    popup.StartPosition = FormStartPosition.CenterParent;
                    popup.ShowDialog();  //show child

                    //if the popup was cancelled, set status to old value
                    if (popup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                    {
                        stat_code = nameaddr.Statuscode;
                        cboStatCode.SelectedItem = GetDisplayStatCodeText(stat_code); //resp.Coltec);
                        cboStatCode.Text = statcode_text;
                        ParentIdPopupOpen = false;
                    }
                    else
                    {
                        nameaddr.Statuscode = "5";
                        cboStatCode.Enabled = false;
                        ParentIdPopupOpen = false;
                    }
                }
            }
        }

        //Boolean, old and new values of fields used in Audit are reset when starting a new record
        private void ResetAuditVariables()
        {
            anytxtmodified = false;
            //add variables to keep the textbox content
            newval = string.Empty;
            oldval = string.Empty;
            oldval = "";
            varnme = "";
            newRespid = "";
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
            oldRepBldgs = "";
            newRepBldgs = "";
            bRepBldgs = false;
            oldRepUnits = "";
            newRepUnits = "";
            bRepUnits = false;
            newContractNum = "";
            oldContractNum = "";
            bContractNum = false;
            newOwner = "";
            oldOwner = "";
            bOwner = false;
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
            notvalid = false;
        }

        private void relsRespLock()
        {
            //Only unlock if you had locked it. Cannot unlock other user's lock.
            if (editable)
                GeneralDataFuctions.UpdateRespIDLock(Respid, "");
           
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

        private void btnPrevCase_Click(object sender, EventArgs e)
        {
            //release all the locks 
            if (bRespid)
            {
                Respid = OrigRespid;
                relsRespLock();
                ChkNumProjForRespid(OrigRespid);
                Respid = newRespid;
                relsRespLock();
                Respid = oldRespid;
                relsRespLock();
            }
            chkBrowseUpdate();
            if (txtRespid.Text == "")
            { Respid = Id; }
            else
            { Respid = txtRespid.Text; }
            txtModifiedStatus();
            if (notvalid == true)
            { return; }

            if (Idlist != null)
            {
                if (CurrIndex == 0)
                {
                    MessageBox.Show("You are at the first observation.");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(curRespid))
                    {
                        string relsUsrName = "";
                        GeneralDataFuctions.UpdateRespIDLock(curRespid, relsUsrName);
                        curRespid = "";
                        if (txtRespid.Text == "")
                        { Respid = nameaddr.Id; }
                        else
                        { Respid = txtRespid.Text; }
                        relsRespLock();
                    }
                    else
                    { relsRespLock(); }

                    CurrIndex = CurrIndex - 1;
                    Id = Idlist[CurrIndex];
                    txtCurrentrec.Text = (CurrIndex + 1).ToString();
                    frmName_Load(this, EventArgs.Empty);
                    txtNewTC.Focus();
                }
            }
            else
            {
                MessageBox.Show("You are at the first observation.");
            }
        }

        private void btnNextCase_Click(object sender, EventArgs e)
        {
            //release the lock of old respids
            if (bRespid)
            {
                Respid = OrigRespid;
                relsRespLock();
                ChkNumProjForRespid(OrigRespid);
                Respid = newRespid;
                relsRespLock();
                Respid = oldRespid;
                relsRespLock();
            }
            chkBrowseUpdate();
            if (txtRespid.Text == "")
            { Respid = Id; }
            else
            { Respid = txtRespid.Text; }
            txtModifiedStatus();
            if (notvalid == true)
            { return; }
             if (Idlist != null && Idlist.Count != 1)
            {
                if (CurrIndex == Idlist.Count - 1)
                {
                    MessageBox.Show("You are at the last observation.");
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(curRespid))
                    {
                        string relsUsrName = "";
                        GeneralDataFuctions.UpdateRespIDLock(curRespid, relsUsrName);
                        curRespid = "";
                        if (txtRespid.Text == "")
                            { Respid = nameaddr.Id; }
                        else
                            { Respid = txtRespid.Text; }
                        relsRespLock();
                    }
                    else
                    { relsRespLock(); }

                    CurrIndex = CurrIndex + 1;
                    Id = Idlist[CurrIndex];
                    txtCurrentrec.Text = (CurrIndex + 1).ToString();
                    frmName_Load(this, EventArgs.Empty);
                    txtNewTC.Focus();
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(curRespid))
                {
                    string relsUsrName = "";
                    GeneralDataFuctions.UpdateRespIDLock(curRespid, relsUsrName);
                    curRespid = "";
                    if (txtRespid.Text == "")
                    { Respid = nameaddr.Id; }
                    else
                    { Respid = txtRespid.Text; }
                    relsRespLock();
                }
                else
                { relsRespLock(); }
                frmIdPopup popup = new frmIdPopup();
                popup.CallingForm = this;
                DialogResult dialogresult = popup.ShowDialog();
                if (dialogresult == DialogResult.OK && popup.NewId != "" )
                {
                    Id = popup.NewId;
                    frmName_Load(this, EventArgs.Empty);
                    txtNewTC.Focus();
                }
                else 
                {
                    //When any field is changed, auditing table 'oldval' should reflect the correct value
                    Id = txtId.Text;
                    frmName_Load(this, EventArgs.Empty);
                    txtNewTC.Focus();
                }
                popup.Dispose();
            }
        }

        private void btnSlip_Click(object sender, EventArgs e)
        {
            //release the lock of old respids
            //if (bRespid)
            //{
            //    Respid = newRespid;
            //    relsRespLock();
            //    Respid = oldRespid;
            //    relsRespLock();
            //}
            //else
            //{
            //    relsRespLock();
            //}
                frmSlipDisplay fSD = new frmSlipDisplay();
                fSD.Id = txtId.Text;
                fSD.Dodgenum = nameaddr.Dodgenum;
                fSD.Fin = nameaddr.Fin;
                fSD.ShowDialog();  //show child
        }

        private void btnHist_Click(object sender, EventArgs e)
        {
            //this is the comments button
            frmHistory fHistory = new frmHistory();
            fHistory.Id = txtId.Text;
            if (txtRespid.Text != "")
                fHistory.Respid = txtRespid.Text;
            else
                fHistory.Respid = txtId.Text;
            fHistory.Resporg = txtOwner.Text;
            fHistory.Respname = txtPersontoCont1.Text;
            fHistory.ShowDialog();  //show child

            if (fHistory.DialogResult == DialogResult.Cancel)
            {
                    /*display the Project Mark */
                    NamProjCommlist = hd.GetProjCommentTable(Id, true);
                    if (NamProjCommlist.Rows.Count != 0)
                    {
                            dgProjComments.DataSource = NamProjCommlist;
                            if (tabControl2.SelectedIndex == 0)
                            {
                                dgProjComments.Columns[0].Width = 75;
                                dgProjComments.Columns[1].Width = 60;
                            }
                    }
                    /* Display the Respondent comment tab*/
                    NamRespCommlist = hd.GetRespCommentTable(Respid, true);
                    if (NamRespCommlist.Rows.Count != 0)
                    {
                        dgRespComments.DataSource = NamRespCommlist;
                        if (tabControl2.SelectedIndex == 1)
                        {
                            dgRespComments.Columns[0].Width = 75;
                            dgRespComments.Columns[1].Width = 60;
                        }
                    }
                        fHistory.Dispose();
                }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            //release the lock of old respids
            if (bRespid)
            {
                Respid = newRespid;
                relsRespLock();
                Respid = oldRespid;
                relsRespLock();
            }
            else
            { 
                relsRespLock(); 
            }
            chkBrowseUpdate();
            if (txtRespid.Text == "")
            { Respid = Id; }
            else
            { Respid = txtRespid.Text; }
            txtModifiedStatus();
            if (notvalid)
            { return; }

            this.Hide();

            //Display TFU on the C-700 button for NPC users
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                frmTfu tfu = new frmTfu();
                if (txtRespid.Text.Trim() != "")
                    tfu.RespId = txtRespid.Text;
                else
                    tfu.RespId = txtId.Text;
                tfu.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");
                tfu.ShowDialog();   // show child
                if (!form_done)
                {
                    GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");
                    frmName_Load(btnC700, EventArgs.Empty);
                }

            }
            else
            {
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
                if (!form_done)
                {
                    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
                    frmName_Load(btnC700, EventArgs.Empty);
                }
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (bRespid)
            {
                Respid = newRespid;
                relsRespLock();
                
                Respid = OrigRespid;
                relsRespLock();
                //Get all the field values like resporg and respname from respondent table and display
                // on the screen overwriting the existing information.
                RespDetails = NameData.RespDataRead(OrigRespid.ToString());
                getRespFieldData();
                namedata.updateRespid4ID(Id, OrigRespid);
                //audit the change of going back to the original value.
                oldval = newRespid;
                newval = OrigRespid;
                varnme = "RESPID";
                NameAddrAuditData.AddNameCprAudit(Id, varnme, "", oldval, "", newval, Environment.UserName, DateTime.Now);
                
                if (!string.IsNullOrWhiteSpace(newRespid))
                {
                    numRespid = NameData.ChkRespProjnum(newRespid);
                    if (Convert.ToInt32(numRespid) == 0)
                    {
                        namedata.DeleteRespid(newRespid);
                    }
                    string relsUsrName = "";
                    GeneralDataFuctions.UpdateRespIDLock(newRespid, relsUsrName);
                }
                if (!string.IsNullOrWhiteSpace(oldRespid))
                {
                    numRespid = NameData.ChkRespProjnum(oldRespid);
                    if (Convert.ToInt32(numRespid) == 0)
                    {
                        namedata.DeleteRespid(oldRespid);
                    }
                    string relsUsrName = "";
                    GeneralDataFuctions.UpdateRespIDLock(oldRespid, relsUsrName);
                }
                if (!string.IsNullOrWhiteSpace(old2Respid))
                {
                    numRespid = NameData.ChkRespProjnum(old2Respid);
                    if (Convert.ToInt32(numRespid) == 0)
                    {
                        namedata.DeleteRespid(old2Respid);
                    }
                    string relsUsrName = "";
                    GeneralDataFuctions.UpdateRespIDLock(old2Respid, relsUsrName);
                }
                if (!string.IsNullOrWhiteSpace(old3Respid))
                {
                    numRespid = NameData.ChkRespProjnum(old3Respid);
                    if (Convert.ToInt32(numRespid) == 0)
                    {
                        namedata.DeleteRespid(old3Respid);
                    }
                    string relsUsrName = "";
                    GeneralDataFuctions.UpdateRespIDLock(old3Respid, relsUsrName);
                }
                frmName_Load(this, EventArgs.Empty);
                ResetAuditVariables();
            }
            else
            {
                ResetAuditVariables();
                if (txtRespid.Text == "")
                { Respid = Id; }
                else
                { Respid = txtRespid.Text; }
                DisplayAddress();
            }
        }

        private void getRespFieldData()
        {
                txtOwner.Text = RespDetails.Resporg;
                txtPersontoCont1.Text = RespDetails.Respname;
                txtPersontoCont2.Text = RespDetails.Respname2;
                txtOtherResp.Text = RespDetails.Othrresp;
                txtAddr1.Text = RespDetails.Addr1;
                txtAddr2.Text = RespDetails.Addr2;
                txtCityState.Text = RespDetails.Addr3;
                PopulateRstate();
                txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
                txtZipcode.Text = RespDetails.Zip;
                mtxtPhoneNum1.Text = RespDetails.Phone;
                mtxtPhoneNum2.Text = RespDetails.Phone2;
                txtExt1.Text = RespDetails.Ext;
                txtExt2.Text = RespDetails.Ext2;
                mtxtFaxNumber.Text = RespDetails.Fax;
                txtFactorOfficial.Text = RespDetails.Factoff;
                txtOtherResp.Text = RespDetails.Othrresp;
                txtSplnote.Text = RespDetails.Respnote;
                txtEmailAddr.Text = RespDetails.Email;
                txtWebAddr.Text = RespDetails.Weburl;
                txtColhist.Text = RespDetails.Colhist;
                cboCollTec.Text = RespDetails.Coltec;
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
                DataTable dtrMark = rmarkdata.GetRespMarks(Respid);
                if (dtrMark != null && dtrMark.Rows.Count > 0)
                    mark_exist = true;
            }

            return mark_exist;
        }

        private void btnMark_Click(object sender, EventArgs e)
        {
            frmMarkCasesPopup popup = new frmMarkCasesPopup();
            popup.Id = Id;
            popup.Respid = Respid;
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();

            pmarklist.Clear();
            rmarklist.Clear();

            //refresh mark grid
            pmark = pmarkdata.GetProjmarkData(Id);
            pmarklist.Add(pmark);
            rmark = rmarkdata.GetRespmarkData(Respid);
            rmarklist.Add(rmark);

            if (CheckMarkExists())
                lblMark.Text = "MARKED";
            else
                lblMark.Text = "";
        }

        private void btnNewtc_Click(object sender, EventArgs e)
        {
            frmNewtcSel popup = new frmNewtcSel();
           
            popup.CaseOwner = txtSurvey.Text; 
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
                popup.ViewOnly = true;
            else
                popup.ViewOnly = false;
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                string old_tc = txtNewTC.Text;
                txtNewTC.Text = popup.SelectedNewtc;
                if (!ValidateNewtc())
                {
                    MessageBox.Show("The Newtc value entered is invalid.");
                    txtNewTC.Text = old_tc;
                }
            }
            popup.Dispose();
        }

        private void frmName_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bRespid)
            {
                Respid = newRespid;
                relsRespLock();
                Respid = oldRespid;
                relsRespLock();
            }
            else
            { relsRespLock(); }
            if (!call_callingFrom)
                CallingForm.Close();

            form_done = true;
        }

        private void SetTxtChanged()
        {
            this.txtRespid.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.cboStatCode.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.cboCollTec.TextChanged += new System.EventHandler(this.txt_TextChanged);
            
            this.txtNewTC.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtRepBldgs.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtRepUnits.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtContractNum.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtProjDesc.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtProjLoc.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtProjCitySt.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtOwner.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtStrtDater.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtStrtDate.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtFlagStrtDate.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtFactorOfficial.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtOtherResp.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtAddr1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtAddr2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtCityState.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtZipcode.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtPersontoCont1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.mtxtPhoneNum1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtExt1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.mtxtFaxNumber.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtPersontoCont2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.mtxtPhoneNum2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtExt2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.cboLag.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtSplnote.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtEmailAddr.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtWebAddr.TextChanged += new System.EventHandler(this.txt_TextChanged);
        }

        private void RemoveTxtChanged()
        {
            this.txtRespid.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.cboStatCode.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.cboCollTec.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            
            this.txtNewTC.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtRepBldgs.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtRepUnits.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtContractNum.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtProjDesc.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtProjLoc.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtProjCitySt.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtOwner.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtStrtDater.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtStrtDate.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtFlagStrtDate.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtFactorOfficial.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtOtherResp.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtAddr1.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtAddr2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtCityState.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtZipcode.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtPersontoCont1.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.mtxtPhoneNum1.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtExt1.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.mtxtFaxNumber.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtPersontoCont2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.mtxtPhoneNum2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtExt2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.cboLag.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtSplnote.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtEmailAddr.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtWebAddr.TextChanged -= new System.EventHandler(this.txt_TextChanged);
        }

        public void DataChanged()
        {
            done = true;

            //release the lock of old respids
            if (bRespid)
            {
                Respid = OrigRespid;
                relsRespLock();
                ChkNumProjForRespid(OrigRespid);
                Respid = newRespid;
                relsRespLock();
                Respid = oldRespid;
                relsRespLock();
            }

            if (bSurvey == false)
                newSurvey = nameaddr.Survey;
            if (bNewTC == false)
                newNewTC = nameaddr.Newtc;
           
            //booleans to check if any data has changed. If Yes, then save data. (bRespid || won't apply to respid changes since respid changes are instant)
            if ( bStatcode || bSurvey || bNewTC || bRepBldgs || bRepUnits || bContractNum || bProjdesc || bProjloc || bProjcityst ||
                bOwner || bFactOfficial || bOtherResp || bAddr1 || bAddr2 || bAddr3 || bZipCode ||bLag || bCollTec ||
                bPersontoCont || bPhoneNum || bExt || bFaxNumber || bPersontoCont2 || bPhoneNum2 || bExt2 || bSplnote || bEmailAddr || bWebAddr)
            {
                DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result2 == DialogResult.Yes)
                {
                    csda.AddCsdaccessData(Id, "UPDATE");
                    if (bNewTC)
                    { txtNewTC_Leave(txtNewTC, EventArgs.Empty); }
                    if (bWebAddr)
                    { txtWebAddr_Leave(txtWebAddr, EventArgs.Empty); }
                    if (bEmailAddr)
                    { txtEmailAddr_Leave(txtEmailAddr, EventArgs.Empty); }
                    if (bAddr1)
                    { txtAddr1_Leave(txtAddr1, EventArgs.Empty); }
                    if (bAddr3)
                    { txtCityState_Leave(txtCityState, EventArgs.Empty); }
                    if (bZipCode)
                    { txtZipcode_Leave(txtZipcode, EventArgs.Empty); }
                    if (bRespid)
                    { }
                    SaveData();
                    if (notvalid)
                    {
                        done = false;
                    }
                }
                else 
                {
                    csda.AddCsdaccessData(Id, "BROWSE");
                    if (!string.IsNullOrWhiteSpace(newSelRespid))
                    {   Respid = newSelRespid;
                        //delete the newly created resopid in the respondent table.
                        relsRespLock();
                    }
                    if (!string.IsNullOrWhiteSpace(oldRespid))
                    {
                        Respid = oldRespid;
                        relsRespLock();
                    }
                    if (!string.IsNullOrWhiteSpace(newExistRespid))
                    {
                        Respid = newExistRespid;
                        relsRespLock();
                    }
                    if (!string.IsNullOrWhiteSpace(Respid))                        
                    {
                        relsRespLock();
                    }
                    Respid = nameaddr.Respid;
                    relsRespLock();
                }
            }
        }

        //Verify Form closing
        public override bool VerifyFormClosing()
        {
            bool can_close = true;
            if (bSurvey == false)
                newSurvey = nameaddr.Survey;
            if (bNewTC == false)
                newNewTC = nameaddr.Newtc;
            if (!GeneralDataFuctions.CheckUtilitiesNewTCOwner(newNewTC, newSurvey))
            {
                MessageBox.Show("NEWTC not valid for Ownership.");
                notvalid = true;
                return false;
            }
            DataChanged();
           
            
            if (done == false)
                {can_close = false;}
            else
                { relsRespLock();  }

            return can_close;
        }

        private bool anytxtmodified = false;
        //add variables to keep the textbox content
        private string newval; //= string.Empty;
        private string oldval; //= string.Empty;
        private string varnme;
        private string newRespid;
        private string oldRespid;
        private string old1Respid;
        private string old2Respid;
        private string old3Respid;
        private bool bRespid = false;
        private string newStatCode;
        private string oldStatCode;
        private bool bStatcode = false;
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
        private string oldRepBldgs;
        private string newRepBldgs;
        private bool bRepBldgs = false;
        private string oldRepUnits;
        private string newRepUnits;
        private bool bRepUnits = false;
        private int newCostpu;
        private string newContractNum;
        private string oldContractNum;
        private bool bContractNum = false;
        private string newOwner;
        private string oldOwner;
        private bool bOwner = false;
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
        private string newSelRespid = string.Empty;
        private string newExistRespid = string.Empty;
        private string delExistRespid = string.Empty;
        private string numRespid;
        private string curRespid;
        private bool ParentIdPopupOpen = false;

        //Set up Method to Determine if Relevant Textboxes and Masked Textboxes Have Been Modified
        private void txt_TextChanged(object sender, EventArgs e)
        {
          if (sender is TextBox)
            {
                txt = new TextBox();
                newval = (sender as TextBox).Text;
                anytxtmodified = true;

                if (sender == txtRespid)
                {
                    varnme = "RESPID";
                    newRespid = txtRespid.Text.ToString().Trim();
                    if (nameaddr.Respid == "")
                    { oldRespid = nameaddr.Id.ToString().Trim(); }
                    else
                    {oldRespid = nameaddr.Respid.ToString().Trim();}

                    if (newRespid != oldRespid)
                    {
                        bRespid = true;
                        if (txtRespid.Text.Length == 7)
                        {

                        }
                    }
                    else
                    { bRespid = false; }
                }

                if (sender == txtNewTC)
                {
                    GeneralFunctions.CheckIntegerField(sender, "NEWTC");
                    varnme = "NEWTC";
                    newNewTC = txtNewTC.Text.ToString().Trim();
                    oldNewTC = nameaddr.Newtc.ToString().Trim();
                    if (newNewTC != oldNewTC)
                    { bNewTC = true; }
                    else
                    { bNewTC = false; }
                }

                if (sender == txtRepBldgs)
                {
                    varnme = "RBLDGS";
                    newRepBldgs = txtRepBldgs.Text.ToString().Trim();
                    oldRepBldgs = nameaddr.Rbldgs.ToString().Trim();
                    if (newRepBldgs != oldRepBldgs)
                    { bRepBldgs = true; }
                    else
                    { bRepBldgs = false; }
                }

                if (sender == txtRepUnits)
                {
                    varnme = "RUNITS";
                    newRepUnits = txtRepUnits.Text.Replace(",", "").ToString().Trim();
                    oldRepUnits = nameaddr.Runits.ToString().Trim();
                    if (newRepUnits != oldRepUnits)
                    {
                        bRepUnits = true;
                    }
                    else
                    { bRepUnits = false; }
                }

                if (sender == txtContractNum)
                {
                    varnme = "CONTRACT";
                    newContractNum = txtContractNum.Text.ToString().Trim();
                    oldContractNum = nameaddr.Contract.ToString().Trim();
                    if (newContractNum != oldContractNum)
                    { bContractNum = true; }
                    else
                    { bContractNum = false; }
                }

                if (sender == txtProjDesc)
                {
                    varnme = "PROJDESC";
                    newProjDesc = txtProjDesc.Text.ToString().Trim();
                    oldProjDesc = nameaddr.ProjDesc.ToString().Trim();
                    if (newProjDesc != oldProjDesc)
                    { bProjdesc = true; }
                    else
                    { bProjdesc = false; }
                }

                if (sender == txtProjLoc)
                {
                    varnme = "PROJLOC";
                    newProjLoc = txtProjLoc.Text.ToString().Trim();
                    oldProjLoc = nameaddr.Projloc.ToString().Trim();
                    if (newProjLoc != oldProjLoc)
                    { bProjloc = true; }
                    else
                    { bProjloc = false; }
                }

                if (sender == txtProjCitySt)
                {
                    varnme = "PCITYST";
                    newProjCitySt = txtProjCitySt.Text.ToString().Trim();
                    oldProjCitySt = nameaddr.Pcityst.ToString().Trim();
                    if (newProjCitySt != oldProjCitySt)
                    { bProjcityst = true; }
                    else
                    { bProjcityst = false; }
                }

                if (sender == txtOwner)
                {
                    varnme = "RESPORG";
                    newOwner = txtOwner.Text.ToString().Trim();
                    oldOwner = nameaddr.Resporg.ToString().Trim();
                    if (newOwner != oldOwner)
                    { bOwner = true; }
                    else
                    { bOwner = false; }
                }

                if (sender == txtFactorOfficial)
                {
                    varnme = "FACTOFF";
                    newFactOfficial = txtFactorOfficial.Text.ToString().Trim();
                    oldFactOfficial = nameaddr.Factoff.ToString().Trim();
                    if (newFactOfficial != oldFactOfficial)
                    { bFactOfficial = true; }
                    else
                    { bFactOfficial = false; }
                }

                if (sender == txtOtherResp)
                {
                    varnme = "OTHRRESP";
                  newOtherResp = txtOtherResp.Text.ToString().Trim();
                    oldOtherResp = nameaddr.Othrresp.ToString().Trim();
                    if (newOtherResp != oldOtherResp)
                    { bOtherResp = true; }
                    else
                    { bOtherResp = false; }
                }

                if (sender == txtAddr1)
                {
                    varnme = "ADDR1";
                    newAddr1 = txtAddr1.Text.ToString().Trim();
                    oldAddr1 = nameaddr.Addr1.ToString().Trim();
                    if (newAddr1 != oldAddr1)
                    { bAddr1 = true; }
                    else
                    { bAddr1 = false; }
                }

                if (sender == txtAddr2)
                {
                    varnme = "ADDR2";
                    newAddr2 = txtAddr2.Text.ToString().Trim();
                    oldAddr2 = nameaddr.Addr2.ToString().Trim();
                    if (newAddr2 != oldAddr2)
                    { bAddr2 = true; }
                    else
                    { bAddr2 = false; }
                }

                if (sender == txtCityState)
                {
                    varnme = "ADDR3";
                    newAddr3 = txtCityState.Text.ToString().Trim();
                    oldAddr3 = nameaddr.Addr3.ToString().Trim();
                    if (newAddr3 != oldAddr3)
                    { 
                        bAddr3 = true;
                    }
                    else
                    { bAddr3 = false; }
                }

                if (sender == txtZipcode)
                {
                    varnme = "ZIP";
                    newZipCode = txtZipcode.Text.ToString().Trim();
                    oldZipCode = nameaddr.Zip.ToString().Trim();
                    if (newZipCode != oldZipCode)
                    { bZipCode = true; }
                    else
                    { bZipCode = false; }
                }

                if (sender == txtPersontoCont1)
                {
                    varnme = "RESPNAME";
                    newPersontoCont = txtPersontoCont1.Text.ToString().Trim();
                    oldPersontoCont = nameaddr.Respname.ToString().Trim();
                    if (newPersontoCont != oldPersontoCont)
                    { bPersontoCont = true; }
                    else
                    { bPersontoCont = false; }
                }

                if (sender == txtPersontoCont2)
                {
                    varnme = "RESPNAME2";
                    newPersontoCont2 = txtPersontoCont2.Text.ToString().Trim();
                    oldPersontoCont2 = nameaddr.Respname2.ToString().Trim();
                    if (newPersontoCont2 != oldPersontoCont2)
                    { bPersontoCont2 = true; }
                    else
                    { bPersontoCont2 = false; }
                }

                if (sender == txtSplnote)
                {
                    varnme = "RESPNOTE";
                    newSplnote = txtSplnote.Text.ToString().Trim();
                    oldSplnote = nameaddr.Respnote.ToString().Trim();
                    if (newSplnote != oldSplnote)
                    { bSplnote = true; }
                    else
                    { bSplnote = false; }
                }

                if (sender == txtEmailAddr)
                {
                    varnme = "EMAIL";
                    newEmailAddr = txtEmailAddr.Text.ToString().Trim();
                    oldEmailAddr = nameaddr.Email.ToString().Trim();
                    if (newEmailAddr != oldEmailAddr)
                    { bEmailAddr = true; }
                    else
                    { bEmailAddr = false; }
                }

                if (sender == txtWebAddr)
                {
                    varnme = "WEBURL";
                    newWebAddr = txtWebAddr.Text.ToString().Trim();
                    oldWebAddr = nameaddr.Weburl.ToString().Trim();
                    if (newWebAddr != oldWebAddr)
                    { bWebAddr = true; }
                    else
                    { bWebAddr = false; }
                }
            }
            else if (sender is MaskedTextBox)
            {
                txt = new TextBox();
                newval = (sender as MaskedTextBox).Text;
                anytxtmodified = true;

                if (sender == mtxtPhoneNum2)
                {
                    varnme = "PHONE2";
                    newPhoneNum2 = mtxtPhoneNum2.Text.ToString().Trim();
                    oldPhoneNum2 = nameaddr.Phone2.ToString().Trim();
                    if (newPhoneNum2 != oldPhoneNum2)
                    { bPhoneNum2 = true; }
                    else
                    { bPhoneNum2 = false; }
                }

                if (sender == mtxtFaxNumber)
                {
                    varnme = "FAX";
                    newFaxNumber = mtxtFaxNumber.Text.ToString().Trim();
                    oldFaxNumber = nameaddr.Fax.ToString().Trim();
                    if (newFaxNumber != oldFaxNumber)
                    { bFaxNumber = true; }
                    else
                    { bFaxNumber = false; }
                }

                if (sender == mtxtPhoneNum1)
                {
                    varnme = "PHONE";
                    newPhoneNum = mtxtPhoneNum1.Text.ToString().Trim();
                    oldPhoneNum = nameaddr.Phone.ToString().Trim();
                    if (newPhoneNum != oldPhoneNum)
                    { bPhoneNum = true; }
                    else
                    { bPhoneNum = false; }
                }

                if (sender == txtExt1)
                {
                    varnme = "EXT";
                    newExt = txtExt1.Text.ToString().Trim();
                    oldExt = nameaddr.Ext.ToString().Trim();
                    if (newExt != oldExt)
                    { bExt = true; }
                    else
                    { bExt = false; }
                }

                if (sender == txtExt2)
                {
                    varnme = "EXT2";
                    newExt2 = txtExt2.Text.ToString().Trim();
                    oldExt2 = nameaddr.Ext2.ToString().Trim();
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

                if (sender == cboStatCode)
                {
                    varnme = "STATUS";
                    newStatCode = cboStatCode.Text.ToString().Trim();
                    oldStatCode = GetDisplayStatCodeText(nameaddr.Statuscode);

                    if (newStatCode != oldStatCode)
                        { 
                            
                            bStatcode = true;
                                if (newStatCode == "5-Duplicate")
                                {
                                    ChkStatCodeFive();
                                }
                        }
                    else
                        { bStatcode = false; }
                }

                if (sender == cboLag)
                {
                    varnme = "LAG";
                    newLag = cboLag.Text.ToString().Trim();
                    oldLag = nameaddr.Lag.ToString().Trim();
                    if (newLag != oldLag)
                    { bLag = true; }
                    else
                    { bLag = false; }
                }

                if (sender == cboCollTec)
                {
                    varnme = "COLTEC";
                    newCollTec = cboCollTec.Text.ToString().Trim();
                    oldCollTec = nameaddr.Coltec.ToString().Trim();
                    if (newCollTec != oldCollTec)
                    { bCollTec = true; }
                    else
                    { bCollTec = false; }
                }

              
            }
        }

        private void txtModifiedStatus()
        {
            if (bSurvey == false)
                newSurvey = nameaddr.Survey;
            if (bNewTC == false)
                newNewTC = nameaddr.Newtc;
            if (!GeneralDataFuctions.CheckUtilitiesNewTCOwner(newNewTC, newSurvey))
            {
                MessageBox.Show("NEWTC not valid for Ownership.");
                notvalid = true;
                return;
            }

            if (anytxtmodified == true)
            {
                SaveData();
            }
        }

        private void SaveData()
        {
            notvalid = false;

            //check newtc
            if (!ValidateNewtc())
            {
                MessageBox.Show("The Newtc value entered is invalid.");
                txtNewTC.Focus();
                notvalid = true;
                return;
            }

            if ((!mtxtFaxNumber.MaskFull) && (mtxtFaxNumber.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Fax number is invalid.");
                notvalid = true;
                mtxtFaxNumber.Focus();
                mtxtFaxNumber.Text = nameaddr.Fax;
                return;
            }

            if (txtZipcode.Text.Trim() != "")
            {
                bool isCanada = CheckAddress3IsCanada();

                if ((isCanada && !GeneralData.IsCanadianZipCode(txtZipcode.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZipcode.Text.Trim())))
                {
                    DialogResult result = MessageBox.Show("Zip Code is invalid.");
                  
                    notvalid = true;
                    txtZipcode.Focus();
                    txtZipcode.Text = nameaddr.Zip.Trim();
                    return;
                }
            }

            if ((!mtxtPhoneNum1.MaskFull) && (mtxtPhoneNum1.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid.");
            
                notvalid = true;
                mtxtPhoneNum1.Focus();
                mtxtPhoneNum1.Text = nameaddr.Phone2;
                return;
            }

            if ((!mtxtPhoneNum2.MaskFull) && (mtxtPhoneNum2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Phone number is invalid.", "OK");
                notvalid = true;
                mtxtPhoneNum2.Focus();
                mtxtPhoneNum2.Text = nameaddr.Phone;
                return;
            }

            //Check Utitlities owner and newtc
            //if ((bSurvey) || (bNewTC))
            //{
            //    if (bSurvey == false)
            //        newSurvey = nameaddr.Survey;
            //    if (bNewTC == false)
            //        newNewTC = nameaddr.Newtc;

            //    if (!GeneralDataFuctions.CheckUtilitiesNewTCOwner(newNewTC, newSurvey))
            //    {
            //        MessageBox.Show("NEWTC not valid for Ownership.");
            //        notvalid = true;
            //        return;
            //    }
            //}

                DateTime prgdtm = DateTime.Now;
                string usrnme = Environment.UserName;
                string id = txtId.Text;
                string newflag = "";
                string oldflag = "";
                string masterid = nameaddr.Masterid.ToString();

                //update any changes to NewTC and Survey(Owner) to Master table fields
                if ((bSurvey) || (bNewTC))
                {
                   
                    if (bNewTC == false)
                    { newNewTC = nameaddr.Newtc; }
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
                        oldval = oldNewTC;
                        newval = newNewTC;
                        varnme = "NEWTC";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                    }
                }

                //update any changes to soc table fields. 2 fields.
                if ((bRepBldgs) || (bRepUnits))
                {
                    //Runits should be at least 2 times rbldgs
                    int rBldgsNum = int.Parse(txtRepBldgs.Text);
                    int rUnitsNum = int.Parse(txtRepUnits.Text.Replace(",", ""));
                    if (((2 * rBldgsNum) - 1) >= rUnitsNum)
                    {
                        DialogResult result = MessageBox.Show("Runits must be greater than or equal to two times Rbldgs.", "Invalid Rbldgs or Runits.", MessageBoxButtons.OK);
                        if (result == DialogResult.OK)
                        {
                            notvalid = true;
                            txtRepBldgs.Focus();
                            txtRepBldgs.Text = nameaddr.Rbldgs;
                            txtRepUnits.Text = Convert.ToInt32(nameaddr.Runits).ToString("N0");
                            done = false;
                            return;
                        }
                    }
                    if (bRepBldgs == false)
                      newRepBldgs = nameaddr.Rbldgs;

                    if (bRepUnits == false)
                    {
                        newRepUnits = nameaddr.Runits;
                        newCostpu = soc.Costpu;
                    }
                    else
                    {
                        //Update costpu;
                        UpdateCostpu();

                    }

                   
                    //audit update for RBldgs
                    if (bRepBldgs)
                    {
                        oldval = nameaddr.Rbldgs.ToString().Trim(); 
                        newval = newRepBldgs;
                        varnme = "RBLDGS";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                    }
                    //audit update for RUnits
                    if (bRepUnits)
                    {
                        oldval = nameaddr.Runits.ToString().Trim();
                        newval = newRepUnits;
                        varnme = "RUNITS";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);

                    }

                    //update database SOC
                namedata.UpdateSocFlds(newRepBldgs, newRepUnits, newCostpu.ToString(), masterid);
            }

                //update any changes to Sample table fields. 5 fields.
                if ((bStatcode) || (bContractNum) || (bProjdesc) || (bProjloc) || (bProjcityst) || (bRespid))
                {
                    if (bStatcode == false)
                    { newStatCode = cboStatCode.Text; }
                    if (bContractNum == false)
                    { newContractNum = nameaddr.Contract.Trim().ToString(); }
                    if (bProjdesc == false)
                    { newProjDesc = nameaddr.ProjDesc.Trim().ToString(); }
                    if (bProjloc == false)
                    { newProjLoc = nameaddr.Projloc.Trim().ToString(); }
                    if (bProjcityst == false)
                    { newProjCitySt = nameaddr.Pcityst; }

                    if (txtRespid.Text == "")
                    { Respid =Id; }
                    else 
                    { Respid = txtRespid.Text.Trim(); }
                    namedata.UpdateSampleFlds(newStatCode.Substring(0, 1), newContractNum, newProjDesc, newProjLoc, newProjCitySt, masterid, viewcode);
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
                       oldval = oldStatCode.Substring(0, 1);
                       newval = newStatCode.Substring(0, 1);
                       
                       //Set up active value
                       if ((oldval == "1" || oldval == "2" || oldval == "3" || oldval == "7" || oldval == "8") && (newval == "4" || newval == "5" || newval == "6"))
                            namedata.UpdateActive(Id, "I");
                        else if ((oldval == "4" || oldval == "5" || oldval == "6") && (newval == "1" || newval == "2" || newval == "3" || newval == "7" || newval == "8"))
                            namedata.UpdateActive(Id, "A");
                    
                        varnme = "STATUS";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                      }
                }

                //If Respid is changed ignore all the changes to the Respondent table fields.
                    if ((bCollTec) || (bOwner) || (bFactOfficial) || (bOtherResp) || (bPersontoCont) || (bAddr1) || (bAddr2) || (bAddr3) || (bPersontoCont2) || (bSplnote) || (bEmailAddr) || (bWebAddr)
                        || (bZipCode) || (bPhoneNum) || (bLag) || (bExt) || (bFaxNumber) || (bPhoneNum2) || (bExt2))
                    {
                        if (bCollTec == false)
                        { newCollTec = coltec_text; } //nameaddr.Coltec;
                        if (bOwner == false)
                        { newOwner = nameaddr.Resporg; }
                        if (bFactOfficial == false)
                        { newFactOfficial = nameaddr.Factoff; }
                        if (bOtherResp == false)
                        { newOtherResp = nameaddr.Othrresp; }
                        if (bPersontoCont == false)
                        { newPersontoCont = nameaddr.Respname; }
                        if (bAddr1 == false)
                        { newAddr1 = nameaddr.Addr1; }
                        if (bAddr2 == false)
                        { newAddr2 = nameaddr.Addr2; }
                        if (bAddr3 == false)
                        { newAddr3 = nameaddr.Addr3; }
                        if (bPersontoCont2 == false)
                        { newPersontoCont2 = nameaddr.Respname2; }
                        if (bSplnote == false)
                        { newSplnote = nameaddr.Respnote; }
                        if (bEmailAddr == false)
                        { newEmailAddr = nameaddr.Email; }
                        if (bWebAddr == false)
                        { newWebAddr = nameaddr.Weburl; }
                        if (bZipCode == false)
                        { newZipCode = nameaddr.Zip; }
                        if (bPhoneNum == false)
                        { newPhoneNum = nameaddr.Phone; }
                        if (bExt == false)
                        { newExt = nameaddr.Ext; }
                        if (bFaxNumber == false)
                        { newFaxNumber = nameaddr.Fax; }
                        if (bLag == false)
                        { newLag = nameaddr.Lag; }
                        if (bPhoneNum2 == false)
                        { newPhoneNum2 = nameaddr.Phone2; }
                        if (bExt2 == false)
                        { newExt2 = nameaddr.Ext2; }

                        string addr3 = "";
                  if (txtCityState.Text != "")
                    addr3 = txtCityState.Text.Substring(txtCityState.Text.TrimEnd().Length - 2, 2);
                namedata.UpdateRespondentFlds(newCollTec.Substring(0, 1), newOwner, newFactOfficial, newOtherResp, newPersontoCont, newAddr1, newAddr2, newAddr3, newPersontoCont2, newSplnote,
                                                        newEmailAddr, newWebAddr, addr3,
                                                        newZipCode, newPhoneNum, newExt, newFaxNumber, newPhoneNum2, newExt2, newLag, Respid);

                        if (bCollTec)
                        {
                            oldval = oldCollTec.Substring(0, 1);
                            newval = newCollTec.Substring(0, 1);
                            varnme = "COLTEC";
                            NameAddrAuditData.AddNameRspAudit(Respid, varnme, oldval, newval, usrnme, prgdtm);
                        }
                        if (bOwner)
                        {
                            oldval = oldOwner;
                            newval = newOwner;
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

                            /*Save sched call for all projects of the respid */
                            
                            //get all active project for the respid
                            List<string> aidlist = GeneralDataFuctions.GetActiveProjectIds(Respid);
                            SampleData sampdata = new SampleData();
                            MasterData mastdata = new MasterData();
                            SchedCallData scdata = new SchedCallData();
                            if (aidlist.Count > 0)
                            {
                                foreach (string aid in aidlist)
                                {
                                    Sample asamp = sampdata.GetSampleData(aid);
                                    Master amaster = mastdata.GetMasterData(asamp.Masterid);

                                    Schedcall sscc = scdata.GetSchedCallData(aid);
                                    sscc.Accestms = DateTime.Now.ToString("HHmmss");
                                    if (CheckVipstatifiedForID(aid))
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
                                    sscc.Callcnt = sscc.Callcnt + 1;
                                    sscc.Accescde = nameaddr.Coltec;
                                    sscc.Accesday = DateTime.Now.ToString("MMdd");
                       
                                    sscc.Accestme = DateTime.Now.ToString("HHmmss");
                                    sscc.Accesnme = UserInfo.UserName;

                                    scdata.SaveSchedcallData(sscc);

                                    Schedhist sh = new Schedhist(aid);

                                    sh.Owner = amaster.Owner;
                                    sh.Callstat = sscc.Callstat;
                                    sh.Accesday = DateTime.Now.ToString("MMdd");
                                    sh.Accestms = sscc.Accestms;
                                    sh.Accestme = DateTime.Now.ToString("HHmmss");
                                    sh.Accesnme = UserInfo.UserName;
                                    sh.Accescde = nameaddr.Coltec;

                                    scdata.AddSchedHistData(sh);
                                }
                            }
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
                ResetAuditVariables();

            nameaddr = NameData.GetNameAddr(Id, viewcode);
            nameaddrfactor = SourceData.GetFactor(nameaddr.Masterid);
        }

        //check other ids for vip satisfied
        private bool CheckVipstatifiedForID(string cid)
        {
            bool satisfied = false;
            int lag = Convert.ToInt32(cboLag.Text) + 1;

            SampleData sdata = new SampleData();
            MonthlyVipsData mdata = new MonthlyVipsData();

            TypeDBSource dsource = sdata.GetDatabaseSource(cid);
            MonthlyVips monvip = mdata.GetMonthlyVips(cid, dsource);
            MonthlyVip mvv = monvip.GetMonthVip(DateTime.Now.AddMonths(-lag).ToString("yyyyMM"));
            if (mvv != null && mvv.Vipflag == "R")
                satisfied = true;

            return satisfied;
        }

        private void mtxtPhoneNum_Leave(object sender, EventArgs e)
        {

        }

        private void mtxtPhoneNum2_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {return;        }

            if ((!mtxtPhoneNum2.MaskFull) && (mtxtPhoneNum2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    mtxtPhoneNum2.Focus();
                    mtxtPhoneNum2.Text = nameaddr.Phone2;
                    return;
                }
            }
        }

        //Check address 3 is Canada or not
        private bool CheckAddress3IsCanada()
        {
            string[] words = GeneralFunctions.SplitWords(txtCityState.Text.Trim());
            int num_word = words.Count();

            if (words[num_word - 1] == "CANADA")
                return true;
            else
                return false;
        }

        private void txtZipcode_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {return; }

            if (txtZipcode.Text.Trim() != "")
            {
                bool isCanada = CheckAddress3IsCanada();
                
                if ((isCanada && !GeneralData.IsCanadianZipCode(txtZipcode.Text.Trim())) || (!isCanada && !GeneralData.IsUsZipCode(txtZipcode.Text.Trim())))
                {
                    DialogResult result = MessageBox.Show("Zip Code is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtZipcode.Focus();
                        txtZipcode.Text = nameaddr.Zip.Trim();
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Zip Code is invalid.");
                txtZipcode.Focus();
                txtZipcode.Text = nameaddr.Zip.Trim();
                done = false;
                return;
            }
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

        private void mtxtFaxNumber_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }

            if ((!mtxtFaxNumber.MaskFull) && (mtxtFaxNumber.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                DialogResult result = MessageBox.Show("Fax number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    mtxtFaxNumber.Focus();
                    mtxtFaxNumber.Text = nameaddr.Fax;
                    return;
                }
            }
        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl2.SelectedIndex == 0)
            {
                if ( dgProjComments.Rows.Count > 0)
                {
                    dgProjComments.Columns[0].Width = 75;
                    dgProjComments.Columns[1].Width = 60;
                    dgProjComments.ColumnHeadersVisible = false;
                }
            }
            else if (tabControl2.SelectedIndex == 1)
            {
                if (dgRespComments.Rows.Count > 0)
                {
                    dgRespComments.Columns[0].Width = 75;
                    dgRespComments.Columns[1].Width = 60;
                    dgRespComments.ColumnHeadersVisible = false;
                }
            }

            /* Display the Project comment tab*/
            NamProjCommlist = hd.GetProjCommentTable(Id, true);
            if (NamProjCommlist.Rows.Count != 0)
            {
                dgProjComments.ColumnHeadersVisible = false;
                dgProjComments.DataSource = NamProjCommlist;
                dgProjComments.Columns[0].Width = 75;
                dgProjComments.Columns[1].Width = 60;
            }
            else
            {
                dgProjComments.DataSource = null;
            }

            NamRespCommlist = hd.GetRespCommentTable(Respid, true);
            if (NamRespCommlist.Rows.Count != 0)
            {
                dgRespComments.ColumnHeadersVisible = false;
                dgRespComments.DataSource = NamRespCommlist;
                dgRespComments.Columns[0].Width = 75;
                dgRespComments.Columns[1].Width = 60;
            }
            else
            {
                dgRespComments.DataSource = null;
            }


         }

        private void txtEmailAddr_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }
            string inputEmail = txtEmailAddr.Text;
            if (txtEmailAddr.Text.Trim() != "")
            {
                if (GeneralFunctions.isEmail(inputEmail))
                {
                    //Email is valid
                }
                else
                {
                    MessageBox.Show("Email Address is invalid.");
                    txtEmailAddr.Focus();
                    txtEmailAddr.Text = nameaddr.Email;
                    done = false;
                }
            }
        }

        public string rstatevalue;
        public string validrstate;
        public string rstate;

        //validate City State field
        private bool ValidateCityState()
        {
            bool result = true;

            //check valid state
            if (string.IsNullOrWhiteSpace(txtCityState.Text) || GeneralFunctions.HasSpecialCharsInCityState(txtCityState.Text))
            {
                MessageBox.Show("City/State is invalid.");
                result = false;
            }
            else
            {
                string[] words = GeneralFunctions.SplitWords(txtCityState.Text.Trim());
                int num_word = words.Count();
                string rst = string.Empty;
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
                            MessageBox.Show("State/Province is invalid.");
                            txtCityState.Focus();
                            txtCityState.Text = nameaddr.Addr3;
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
                            txtCityState.Focus();
                            txtCityState.Text = nameaddr.Addr3;
                            result = false;
                        }
                        else rstate = rst;
                    }
                }
            }
            return result;
        }

        public string PopulateRstate()
        {
            if (txtCityState.TextLength < 6)
            {
                MessageBox.Show("Address is invalid.");
                txtCityState.Focus();
                txtCityState.Text = nameaddr.Addr3;
                return rstatevalue = "xx";
            }
            else
            {
                rstate = GeneralData.Right(txtCityState.Text, 6);
                if (rstate == "CANADA")
                {
                    //remove blank spaces
                    string noblanks = txtCityState.Text.Replace(" ", "").Replace("\t", "").Replace("\n", "").Replace("\r", "");
                    string canadastate = GeneralData.Right(noblanks, 8);
                    rstatevalue = canadastate.Substring(0, 2);
                }
                else
                {
                    rstatevalue = GeneralData.Right(txtCityState.Text.TrimEnd(), 2);
                }
                return rstatevalue;
            }
        }

        //Check for valid Web address
        private void txtWebAddr_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {
                return;
            }
            if (txtWebAddr.Text.Trim() != "")
            {
                if (!GeneralData.IsValidURL(txtWebAddr.Text))
                {
                    DialogResult result = MessageBox.Show("Web Address is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtWebAddr.Focus();
                        txtWebAddr.Text = nameaddr.Weburl;
                        done = false;
                    }
                }
            }
       }

        //validate the state code
        private void txtCityState_Leave(object sender, EventArgs e)
        {
            if (!ValidateCityState())
            {
                txtCityState.Focus();
                txtCityState.Text = nameaddr.Addr3;
            }   
            PopulateRstate();
            txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
        }

        private void txtExt_Enter(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                (sender as MaskedTextBox).Focus();
                (sender as MaskedTextBox).SelectionStart = 0;
            }
        }

        private void txtExt2_Enter(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                (sender as MaskedTextBox).Focus();
                (sender as MaskedTextBox).SelectionStart = 0;
            }
        }

        private void txtNewTC_Leave(object sender, EventArgs e)
        {
            //check the value entered against the NEWTCLIST table in the database.
            if (btnRefresh.Focused)
            {
                return;
            }
            string newtcval = txtNewTC.Text;
            
            if (!ValidateNewtc())
            {
                MessageBox.Show("The Newtc value entered is invalid.");
                txtNewTC.Focus();
                txtNewTC.Text = nameaddr.Newtc;
                done = false;
                return;
            }
        }

        private bool ValidateNewtc()
        {
            //check it validate newtc or not
            bool NewTCresult;
           
            NewTCresult = GeneralDataFuctions.CheckNewTC(txtNewTC.Text, txtSurvey.Text);
            
            return NewTCresult;
        }

        private void ShowReported()
        {
            lblReported.Visible = true;
            lblEdited.Visible = true;
            lblItem5C.Visible = true;
            lblStartDate.Visible = true;
            lblCompDate.Visible = true;
            lblProjComplDate.Visible = true;
            txtRvitm5cr.Visible = true;
            txtStrtDater.Visible = true;
            txtCompDater.Visible = true;
            txtfutCompDr.Visible = true;
            txtRvitm5c.Visible = true;
            txtFlagR5c.Visible = true;
            txtStrtDate.Visible = true;
            txtFlagStrtDate.Visible = true;
            txtCompDate.Visible = true;
            txtFlagCompDate.Visible = true;
            txtFutCompD.Visible = true;
            txtFlagFutCompD.Visible = true;
        }

        private void HideReported()
        {
            lblReported.Visible = false;
            lblEdited.Visible = false;
            lblItem5C.Visible = false;
            lblStartDate.Visible = false;
            lblCompDate.Visible = false;
            lblProjComplDate.Visible = false;
            txtRvitm5cr.Visible = false;
            txtStrtDater.Visible = false;
            txtCompDater.Visible = false;
            txtfutCompDr.Visible = false;
            txtRvitm5c.Visible = false;
            txtFlagR5c.Visible = false;
            txtStrtDate.Visible = false;
            txtFlagStrtDate.Visible = false;
            txtCompDate.Visible = false;
            txtFlagCompDate.Visible = false;
            txtFutCompD.Visible = false;
            txtFlagFutCompD.Visible = false;
        }

        private void chkReferral()
        {
            ReferralData refdata = new ReferralData();

            //Check if either the Respondent or Project Referral is true
            if (refdata.CheckReferralExist(Id, Respid))
            {
                label43.Text = "REFERRAL";
                label43.Visible = true;
            }
            else
            {
                label43.Visible = false;
            }
        }

        private void btnReferral_Click(object sender, EventArgs e)
        {
            frmReferral fReferral = new frmReferral();
            fReferral.Id = txtId.Text;
            fReferral.Respid = Respid;
            fReferral.ShowDialog();  //show child
            if (fReferral.DialogResult == DialogResult.Cancel)
                {
                    chkReferral();
                    fReferral.Dispose();
                }
        }

        private void chkUserGroup()
        {
            UserInfoData data_object = new UserInfoData();
                switch (UserInfo.GroupCode)
                {
                    case EnumGroups.Programmer:  
                        ShowReported();
                        break;
                    case EnumGroups.HQManager:   
                        ShowReported();
                        break;
                    case EnumGroups.HQAnalyst:  
                        ShowReported();
                        break;
                    case EnumGroups.NPCManager: 
                        HideReported();
                        break;
                    case EnumGroups.NPCLead:
                        HideReported();
                        break;
                    case EnumGroups.NPCInterviewer: 
                        HideReported();
                        break;
                    case EnumGroups.HQSupport:  
                        ShowReported();
                        break;
                    case EnumGroups.HQMathStat: 
                        ShowReported();
                        break;
                    case EnumGroups.HQTester:  
                        ShowReported();
                        break;
                }
          }

        private void cboStatCode_Check(object sender, EventArgs e)
        {
            //if changed from 4 to 1, 2, 3, 7, 8 and STRTDATE >= current month
            if (btnRefresh.Focused)
            {   return;}

            string oldStattext = nameaddr.Statuscode.ToString();
            string newStattext = cboStatCode.SelectedItem.ToString();
            if (oldStattext == newStattext)
                return;

            int iStrtDate;
            int iCurMonYr;
                
            if (string.IsNullOrWhiteSpace(txtStrtDate.Text))
            { iStrtDate = 0; }
            else
            { iStrtDate = int.Parse(txtStrtDate.Text); }
            iCurMonYr = int.Parse(GeneralFunctions.CurrentYearMon());

            if (oldStattext == "4" && newStattext == "1" || newStattext == "2" || newStattext == "3" ||
                 newStattext == "7" || newStattext == "8")
            {
                if (iStrtDate >= iCurMonYr)
                {
                    MessageBox.Show("Status change rejected, Project has not started.");
                    cboStatCode.SelectedItem = oldStattext;
                    done = false;
                }
            }

            if (oldStattext == "4" && newStattext == "1" )
            {
                if (iStrtDate <= iCurMonYr)
                {
                    MessageBox.Show("Status change rejected, Project has started.");
                    cboStatCode.SelectedItem = oldStattext;
                    done = false;
                }
            }
            
            if (oldStattext != "4" && newStattext == "4") 
            {
                if (iStrtDate != 0 && iStrtDate <= iCurMonYr)
                {
                    MessageBox.Show("Status change rejected, Project has started.");
                    cboStatCode.SelectedItem = oldStattext;
                    done = false;
                }
            }
            if (oldStattext != "4" && newStattext == "4")
            {
                if (iStrtDate == 0 )
                {
                    MessageBox.Show("Status change rejected.");
                    cboStatCode.SelectedItem = oldStattext;
                    done = false;
                }
            }
            oldStattext = newStattext;

        //Set the active field based on Statcode Changes. If Stat code is changed to 4, 5, 6 Active field should be set to 'I'
            if (newStattext == "4" || newStattext == "5" || newStattext == "6")
            { 
                string txtActive = "I";
                namedata.UpdateActive(Id, txtActive);
            }
        }

        private void txtZipcode_Enter(object sender, EventArgs e)
        {
            txtZipcode.SelectionLength = 0;
        }

        private void txtAddr1_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            { return; }

            if (string.IsNullOrWhiteSpace(txtAddr1.Text))
            {
                MessageBox.Show("Address is invalid.");
                txtAddr1.Focus();
                txtAddr1.Text = nameaddr.Addr1.Trim();
                done = false;
            }
        }

       
        private void txtRespid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void mtxtPhoneNum2_Enter(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                (sender as MaskedTextBox).Focus();
                (sender as MaskedTextBox).SelectionStart = 0;
            }
        }

        private void mtxtPhoneNum2_Leave_1(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
                return;

            if ((mtxtPhoneNum2.Text.Length != 10) && (mtxtPhoneNum2.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    mtxtPhoneNum2.Focus();
                    mtxtPhoneNum2.Text =  nameaddr.Phone2;
                    return;
                }
            }
        }

        private void mtxtPhoneNum1_Enter(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                (sender as MaskedTextBox).Focus();
                (sender as MaskedTextBox).SelectionStart = 0;
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
        
        //The contact tabs should be blue or black font depending on if they are blank or not.
        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == 0)
            {
                if (txtPersontoCont1.Text == "")
                {
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                        tabControl1.Font,
                        Brushes.Black,
                        new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
                else
                {
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                         tabControl1.Font,
                         Brushes.Blue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
            if (e.Index == 1)
            {
                if (txtPersontoCont2.Text == "")
                {
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                        tabControl1.Font,
                        Brushes.Black,
                        new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
                else
                {
                    e.Graphics.DrawString(tabControl1.TabPages[e.Index].Text,
                         tabControl1.Font,
                         Brushes.Blue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
        }

        private void txtRepBldgs_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRepBldgs_Enter(object sender, EventArgs e)
        {
            oldRepBldgs = txtRepBldgs.Text;
        }

        private void txtRepUnits_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtRepUnits_Enter(object sender, EventArgs e)
        {
            oldRepUnits = txtRepUnits.Text;
        }

        private void txtRepBldgs_Leave(object sender, EventArgs e)
        {
            if (oldRepBldgs != txtRepBldgs.Text)
            {
                if (txtRepBldgs.Text == "" || Convert.ToInt32(txtRepBldgs.Text) < 1)
                {
                    MessageBox.Show("Rbldgs must be a number between 1 and 999.");
                    txtRepBldgs.Text = oldRepBldgs;
                }
                else
                    oldRepBldgs = txtRepBldgs.Text;
            }
        }

        private void txtRepUnits_Leave(object sender, EventArgs e)
        {
            if (oldRepUnits != txtRepUnits.Text)
            {
                if (txtRepUnits.Text == "" || Convert.ToInt32(txtRepUnits.Text.Replace(",", "")) < 2)
                {
                    MessageBox.Show("Runits must be a number between 2 and 9999.");
                    txtRepUnits.Text = oldRepUnits;
                    txtRepUnits.Focus();
                }
                else
                {
                    oldRepUnits = txtRepUnits.Text;
                    txtRepUnits.Text = Convert.ToInt32(txtRepUnits.Text.Replace(",", "")).ToString("N0");
                }
            }
        }

        private void txtRespid_Leave(object sender, EventArgs e)
        {
            if ((txtRespid.Text.Length != 7 ) &&  (txtRespid.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Respondent ID must be 7 digits or blank.", "OK", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    txtRespid.Focus();
                    txtRespid.Text = nameaddr.Respid;
                    return;
                }
            }
        }

        string selRespChange = string.Empty;
        private void btnRespid_Click(object sender, EventArgs e)
        {
            DateTime prgdtm = DateTime.Now;
            string usrnme = Environment.UserName;
            string id = txtId.Text;
            string newflag = "";
            string oldflag = "";
            string masterid = nameaddr.Masterid.ToString();
            //Open a popup window depending on the text in Respid textbox.
            frmRespidEditPopup popup = new frmRespidEditPopup(txtRespid.Text, Id);
            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK && popup.newRespid != "" && popup.newRespid != " ")
            {
                bRespid = true;
                newRespid = popup.newRespid;
                if (newRespid.Length > 7)
                {
                    //respid was valid and not locked by another user so update the record after locking it.
                    old1Respid = "";
                    if (txtRespid.Text == "")
                    { old1Respid = Id; }
                    else
                    { old1Respid = txtRespid.Text; }
                    if (old1Respid != OrigRespid)
                    {
                        string relsUsrName = "";
                        GeneralDataFuctions.UpdateRespIDLock(old1Respid, relsUsrName); 
                    }
                    newRespid = newRespid.Substring(0, 7);//txtRespid.Text;
                    Respid = newRespid;
                    txtRespid.ReadOnly = false;
                    txtRespid.Text = Respid;
                    txtRespid.ReadOnly = true;
                    curRespid = Respid;
                    newExistRespid = Respid;
                    GeneralDataFuctions.UpdateRespIDLock(Respid, UserInfo.UserName);
                    if (old1Respid != OrigRespid)
                    {
                        //Get all the field values like resporg and respname from respondent table and display
                        // on the screen overwriting the existing information.
                        RespDetails = NameData.RespDataRead(Respid.ToString());
                        txtOwner.Text = RespDetails.Resporg;
                        txtPersontoCont1.Text = RespDetails.Respname;
                        txtPersontoCont2.Text = RespDetails.Respname2;
                        txtOtherResp.Text = RespDetails.Othrresp;
                        txtAddr1.Text = RespDetails.Addr1;
                        txtAddr2.Text = RespDetails.Addr2;
                        txtCityState.Text = RespDetails.Addr3;
                        PopulateRstate();
                        txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
                        txtZipcode.Text = RespDetails.Zip;
                        mtxtPhoneNum1.Text = RespDetails.Phone;
                        mtxtPhoneNum2.Text = RespDetails.Phone2;
                        txtExt1.Text = RespDetails.Ext;
                        txtExt2.Text = RespDetails.Ext2;
                        mtxtFaxNumber.Text = RespDetails.Fax;
                        txtFactorOfficial.Text = RespDetails.Factoff;
                        txtOtherResp.Text = RespDetails.Othrresp;
                        txtSplnote.Text = RespDetails.Respnote;
                        txtEmailAddr.Text = RespDetails.Email;
                        txtWebAddr.Text = RespDetails.Weburl;
                        txtColhist.Text = RespDetails.Colhist;
                        cboCollTec.SelectedItem = GetDisplayColtecText(RespDetails.Coltec);
                        cboCollTec.Text = coltec_text;
                        
                        //update Sample table
                        namedata.updateRespid4ID(Id, Respid);
                        //update the current values respondent fields to the respondent table.
                        namedata.UpdateRespondentFlds(cboCollTec.Text.Substring(0, 1), txtOwner.Text, txtFactorOfficial.Text, txtOtherResp.Text, txtPersontoCont1.Text, txtAddr1.Text, txtAddr2.Text, txtCityState.Text,
                                                        txtPersontoCont2.Text, txtSplnote.Text, txtEmailAddr.Text, txtWebAddr.Text, txtCityState.Text.Substring(txtCityState.Text.Length - 2, 2),
                                                        txtZipcode.Text, mtxtPhoneNum1.Text, txtExt1.Text, mtxtFaxNumber.Text, mtxtPhoneNum2.Text, txtExt2.Text, cboLag.Text, newSelRespid);
                        //enter audit data
                        if (bRespid)
                        {
                            oldval = old1Respid;
                            if (txtRespid.Text == "")
                            { newval = nameaddr.Id; }
                            else
                            { newval = txtRespid.Text; }
                            varnme = "RESPID";
                            NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                            ChkNumProjForRespid(old1Respid);
                        }
                    }
                    else
                    {
                        //Get all the field values like resporg and respname from respondent table and display
                        // on the screen overwriting the existing information.
                        RespDetails = NameData.RespDataRead(Respid.ToString());
                        txtOwner.Text = RespDetails.Resporg;
                        txtPersontoCont1.Text = RespDetails.Respname;
                        txtPersontoCont2.Text = RespDetails.Respname2;
                        txtOtherResp.Text = RespDetails.Othrresp;
                        txtAddr1.Text = RespDetails.Addr1;
                        txtAddr2.Text = RespDetails.Addr2;
                        txtCityState.Text = RespDetails.Addr3;
                        PopulateRstate();
                        txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
                        txtZipcode.Text = RespDetails.Zip;
                        mtxtPhoneNum1.Text = RespDetails.Phone;
                        mtxtPhoneNum2.Text = RespDetails.Phone2;
                        txtExt1.Text = RespDetails.Ext;
                        txtExt2.Text = RespDetails.Ext2;
                        mtxtFaxNumber.Text = RespDetails.Fax;
                        txtFactorOfficial.Text = RespDetails.Factoff;
                        txtOtherResp.Text = RespDetails.Othrresp;
                        txtSplnote.Text = RespDetails.Respnote;
                        txtEmailAddr.Text = RespDetails.Email;
                        txtWebAddr.Text = RespDetails.Weburl;
                        txtColhist.Text = RespDetails.Colhist;
                        cboCollTec.SelectedItem = GetDisplayColtecText(RespDetails.Coltec);
                        cboCollTec.Text = coltec_text;
                        //only update sample table
                        namedata.updateRespid4ID(Id, Respid);
                        oldval = old1Respid;
                        if (txtRespid.Text == "")
                        { newval = nameaddr.Id; }
                        else
                        { newval = txtRespid.Text; }
                        varnme = "RESPID";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                    }

                }
                else if (newRespid == "New")
                {
                    old2Respid = "";
                    if (txtRespid.Text == "")
                    { old2Respid = Id; }
                    else
                    { old2Respid = txtRespid.Text; }
                    if (old2Respid != OrigRespid)
                    {
                        string relsUsrName = "";
                        GeneralDataFuctions.UpdateRespIDLock(old2Respid, relsUsrName);
                    }
                    if (old2Respid != OrigRespid)
                    {
                        newSelRespid = NameData.GetLastValidRespid();
                        int intRespid = Convert.ToInt32(newSelRespid) + 1;
                        newSelRespid = intRespid.ToString().PadLeft(7, '0');
                        this.txtRespid.TextChanged -= new System.EventHandler(this.txt_TextChanged);
                        txtRespid.ReadOnly = false;
                        txtRespid.Text = newSelRespid;
                        curRespid = newSelRespid;
                        newRespid = newSelRespid;
                        txtRespid.ReadOnly = true;
                        PopulateRstate();
                        namedata.InsertNewRespid(newSelRespid, txtOwner.Text, txtPersontoCont1.Text, txtAddr1.Text, txtAddr2.Text, txtCityState.Text, txtZipcode.Text, mtxtPhoneNum1.Text, txtExt1.Text,
                                                 mtxtFaxNumber.Text, txtFactorOfficial.Text, txtOtherResp.Text, txtSplnote.Text, txtEmailAddr.Text, txtWebAddr.Text, rstatevalue, "0",
                                                 UserInfo.UserName, "", cboCollTec.Text.Substring(0, 1), txtColhist.Text, txtPersontoCont2.Text, mtxtPhoneNum2.Text, txtExt2.Text, UserInfo.UserName, "");
                        namedata.updateRespid4ID(Id, newSelRespid);
                        newRespid = newSelRespid;
                        bRespid = true;
                        if (bRespid)
                        {
                            oldval = old2Respid;
                            if (txtRespid.Text == "")
                            { newval = nameaddr.Id; }
                            else
                            { newval = txtRespid.Text; }
                            varnme = "RESPID";
                            NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                            ChkNumProjForRespid(old2Respid);
                        }
                    }
                    else
                    {
                        //add new respid
                        newSelRespid = NameData.GetLastValidRespid();
                        int intRespid = Convert.ToInt32(newSelRespid) + 1;
                        newSelRespid = intRespid.ToString().PadLeft(7, '0');
                        this.txtRespid.TextChanged -= new System.EventHandler(this.txt_TextChanged);
                        txtRespid.ReadOnly = false;
                        txtRespid.Text = newSelRespid;
                        curRespid = newSelRespid;
                        newRespid = newSelRespid;
                        txtRespid.ReadOnly = true;
                        PopulateRstate();
                        namedata.InsertNewRespid(newSelRespid, txtOwner.Text, txtPersontoCont1.Text, txtAddr1.Text, txtAddr2.Text, txtCityState.Text, txtZipcode.Text, mtxtPhoneNum1.Text, txtExt1.Text,
                                                 mtxtFaxNumber.Text, txtFactorOfficial.Text, txtOtherResp.Text, txtSplnote.Text, txtEmailAddr.Text, txtWebAddr.Text, rstatevalue, "0",
                                                 UserInfo.UserName, "", cboCollTec.Text.Substring(0, 1), txtColhist.Text, txtPersontoCont2.Text, mtxtPhoneNum2.Text, txtExt2.Text, UserInfo.UserName, "");
                        namedata.updateRespid4ID(Id, newSelRespid);
                        oldval = old2Respid;
                        if (txtRespid.Text == "")
                        { newval = nameaddr.Id; }
                        else
                        { newval = txtRespid.Text; }
                        varnme = "RESPID";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                    }
                }
                else
                {
                    old3Respid = "";
                    if (txtRespid.Text == "")
                    { old3Respid = Id; }
                    else
                    { old3Respid = txtRespid.Text; }
                    if (old3Respid != OrigRespid)
                    {
                        string relsUsrName = "";
                        GeneralDataFuctions.UpdateRespIDLock(old3Respid, relsUsrName);
                    }
                    if (old3Respid != OrigRespid)
                    {
                        string relsUsrName = "";
                        GeneralDataFuctions.UpdateRespIDLock(old3Respid, relsUsrName);
                        //delete old respid...txtCityState.Text.Substring(txtCityState.Text.Length - 2, 2) replace with rstatevalue
                        PopulateRstate();
                        if (!GeneralDataFuctions.ChkRespid(Id.ToString()))
                        {
                            namedata.InsertNewRespid(Id, txtOwner.Text, txtPersontoCont1.Text, txtAddr1.Text, txtAddr2.Text, txtCityState.Text, txtZipcode.Text, mtxtPhoneNum1.Text, txtExt1.Text,
                                                 mtxtFaxNumber.Text, txtFactorOfficial.Text, txtOtherResp.Text, txtSplnote.Text, txtEmailAddr.Text, txtWebAddr.Text, rstatevalue, "0",
                                                 UserInfo.UserName, "", cboCollTec.Text.Substring(0, 1), txtColhist.Text, txtPersontoCont2.Text, mtxtPhoneNum2.Text, txtExt2.Text, UserInfo.UserName, "");
                        }
                        //update Sample table
                        namedata.updateRespid4ID(Id, Id);
                        delExistRespid = Respid;
                        txtRespid.ReadOnly = false;
                        txtRespid.Text = "";
                        newRespid = Id;
                        txtRespid.ReadOnly = false;
                        //enter audit data
                        if (bRespid)
                        {
                            oldval = old3Respid;
                            if (txtRespid.Text == "")
                            {
                                newval = nameaddr.Id;
                                newRespid = nameaddr.Id;
                                curRespid = newRespid;
                            }
                            else
                            { newval = txtRespid.Text; }
                            varnme = "RESPID";
                            NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                            ChkNumProjForRespid(old3Respid);
                        }
                    }
                    else
                    {
                        //delete old respid...
                        PopulateRstate();
                        if (!GeneralDataFuctions.ChkRespid(Id.ToString()))
                        {
                            namedata.InsertNewRespid(Id, txtOwner.Text, txtPersontoCont1.Text, txtAddr1.Text, txtAddr2.Text, txtCityState.Text, txtZipcode.Text, mtxtPhoneNum1.Text, txtExt1.Text,
                                                     mtxtFaxNumber.Text, txtFactorOfficial.Text, txtOtherResp.Text, txtSplnote.Text, txtEmailAddr.Text, txtWebAddr.Text, rstatevalue, "0",
                                                     UserInfo.UserName, "", cboCollTec.Text.Substring(0, 1), txtColhist.Text, txtPersontoCont2.Text, mtxtPhoneNum2.Text, txtExt2.Text, UserInfo.UserName, "");
                        }
                        //update Sample table
                        namedata.updateRespid4ID(Id, Id);
                        delExistRespid = Respid;
                        txtRespid.ReadOnly = false;
                        txtRespid.Text = "";
                        newRespid = Id;
                        txtRespid.ReadOnly = false;
                        //Audit the changes
                        oldval = old3Respid;
                        if (txtRespid.Text == "")
                        {
                            newval = nameaddr.Id;
                            newRespid = nameaddr.Id;
                            curRespid = newRespid;
                        }
                        else
                        { newval = txtRespid.Text; }
                        varnme = "RESPID";
                        NameAddrAuditData.AddNameCprAudit(id, varnme, oldflag, oldval, newflag, newval, usrnme, prgdtm);
                    }
                }
            }
            popup.Dispose();
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            frmRspAuditPopup fRspAuditPopup = new frmRspAuditPopup(Respid);
            fRspAuditPopup.ShowDialog();  //show child
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
          
            frmSourceAddrPopup fmSAP = new frmSourceAddrPopup(nameaddr.Masterid);
            fmSAP.ShowDialog();  //show child

            if (fmSAP.DialogResult == DialogResult.Cancel)
            {
                fmSAP.Dispose();
            }
        }

        
        //Check if its update or browse to be entered in the csdAccess table 
        private void chkBrowseUpdate()
        {
            if (bRespid || bStatcode || bSurvey || bNewTC || bRepBldgs || bRepUnits || bContractNum || bProjdesc || bProjloc || bProjcityst ||
                bOwner || bFactOfficial || bOtherResp || bAddr1 || bAddr2 || bAddr3 || bZipCode || bLag || bCollTec ||
                bPersontoCont || bPhoneNum || bExt || bFaxNumber || bPersontoCont2 || bPhoneNum2 || bExt2 || bSplnote || bEmailAddr || bWebAddr)
            {
                csda.AddCsdaccessData(Id, "UPDATE");
            }
            else
            {
                csda.AddCsdaccessData(Id, "BROWSE");
            }
        }

        //Status code change validations.
        private void cboStatCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (newStatCode == "4-Abeyance" && txtStrtDate.Text.Trim() == "")
            {
                MessageBox.Show("Status change rejected, start date is blank.");
                cboStatCode.Text = oldStatCode;
                cboStatCode.Focus();
                return;
            }
            else if (newStatCode == "4-Abeyance" && (Convert.ToInt32(txtStrtDate.Text.Trim()) <= Convert.ToInt32(GeneralFunctions.CurrentYearMon())))
            {
                //Display TFU message for NPC users
                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
                {
                    MessageBox.Show("Status code changes not allowed, Please enter on TFU screen.");
                    cboStatCode.Text = oldStatCode;
                    cboStatCode.Focus();
                    return;
                }
                else
                {
                    MessageBox.Show("Status code changes not allowed, Please enter on C700 screen.");
                    cboStatCode.Text = oldStatCode;
                    cboStatCode.Focus();
                    return;
                }
            }

            if ((oldStatCode == "4-Abeyance") && (newStatCode == "1-Active" || newStatCode == "2-PNR" || newStatCode == "3-DC PNR" || newStatCode == "6-Out of Scope"
                || newStatCode == "7-DC Refusal" || newStatCode == "8-Refusal")) 
                if (Convert.ToInt32(txtStrtDate.Text.Trim()) >= Convert.ToInt32(GeneralFunctions.CurrentYearMon()))
            {
                MessageBox.Show("Status change rejected, Project has not started.");
                cboStatCode.SelectedValue = 4;
                cboStatCode.Focus();
                return;
            }
            else
            { }
        }

        private void mtxtPhoneNum1_Leave(object sender, EventArgs e)
        {
            if (btnRefresh.Focused)
            {return;}

                if ((!mtxtPhoneNum1.MaskFull) && (mtxtPhoneNum1.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Phone number is invalid.");
                    mtxtPhoneNum1.Focus();
                    mtxtPhoneNum1.Text = oldPhoneNum;
                }
            //}
        }


        private void mtxtPhoneNum1_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (oldPhoneNum != mtxtPhoneNum1.Text)
                    {
                        if ((!mtxtPhoneNum1.MaskFull) && (mtxtPhoneNum1.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                        {
                            MessageBox.Show("Phone number is invalid.");
                            mtxtPhoneNum1.Focus();
                            mtxtPhoneNum1.Text = oldPhoneNum;
                        }
                        else
                            oldPhoneNum = mtxtPhoneNum1.Text;
                    }
                }
            }
        }

        private void mtxtFaxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (oldFaxNumber != mtxtFaxNumber.Text)
                    {
                        if ((!mtxtFaxNumber.MaskFull) && (mtxtFaxNumber.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                        {
                            MessageBox.Show("Phone number is invalid.");
                            mtxtFaxNumber.Focus();
                            mtxtFaxNumber.Text = oldFaxNumber;
                        }
                        else
                            oldFaxNumber = mtxtFaxNumber.Text;
                    }
                }
            }
        }

        private void mtxtPhoneNum2_KeyDown(object sender, KeyEventArgs e)
        {
            if (editable && EditMode == TypeEditMode.Edit)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (oldPhoneNum2 != mtxtPhoneNum2.Text)
                    {
                        if ((!mtxtPhoneNum2.MaskFull) && (mtxtPhoneNum2.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                        {
                            MessageBox.Show("Phone number is invalid.");
                            mtxtPhoneNum2.Focus();
                            mtxtPhoneNum2.Text = oldPhoneNum2;
                        }
                        else
                            oldPhoneNum2 = mtxtPhoneNum2.Text;
                    }
                }
            }
        }

        private void mtxtFaxNumber_Enter(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                (sender as MaskedTextBox).Focus();
                (sender as MaskedTextBox).SelectionStart = 0;
            }
        }

        private void cboLag_DrawItem(object sender, DrawItemEventArgs e)
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

            e.Graphics.DrawString(cboLag.Items[index].ToString(), e.Font, System.Drawing.Brushes.Black, e.Bounds, StringFormat.GenericDefault);
            // Draw the focus rectangle if the mouse hovers over an item.
            e.DrawFocusRectangle();
        }

        private void txtNewTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnReplaceC_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Contractor info?", "Replace with Contractor Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtOwner.Text = nameaddrfactor.F7resporg;
                txtPersontoCont1.Text = nameaddrfactor.F7respname;
                txtAddr1.Text = nameaddrfactor.F7addr1;
                txtAddr2.Text = nameaddrfactor.F7addr2;
                txtCityState.Text = nameaddrfactor.F7addr3;
                txtZipcode.Text = nameaddrfactor.F7zip;
                mtxtPhoneNum1.Text = nameaddrfactor.F7phone;
                txtEmailAddr.Text = nameaddrfactor.F7email;
                txtWebAddr.Text = nameaddrfactor.F7weburl;
                if (nameaddrfactor.F7respname != " ")
                    txtFactorOfficial.Text = "ATTN " + nameaddrfactor.F7respname;
                else
                    txtFactorOfficial.Text = "";
                txtOtherResp.Text = "";
                mtxtFaxNumber.Text = "";
                txtPersontoCont2.Text = "";
                mtxtPhoneNum2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }


        private void btnReplaceO_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Owner info?", "Replace with Owner Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtOwner.Text = nameaddrfactor.F3resporg;
                txtPersontoCont1.Text = nameaddrfactor.F3respname;
                txtAddr1.Text = nameaddrfactor.F3addr1;
                txtAddr2.Text = nameaddrfactor.F3addr2;
                txtCityState.Text = nameaddrfactor.F3addr3;
                txtZipcode.Text = nameaddrfactor.F3zip;
                mtxtPhoneNum1.Text = nameaddrfactor.F3phone;
                txtEmailAddr.Text = nameaddrfactor.F3email;
                txtWebAddr.Text = nameaddrfactor.F3weburl;
                if (nameaddrfactor.F3respname != " ")
                    txtFactorOfficial.Text = "ATTN " + nameaddrfactor.F3respname;
                else
                    txtFactorOfficial.Text = "";
                
                txtOtherResp.Text = "";
                mtxtFaxNumber.Text = "";
                txtPersontoCont2.Text = "";
                mtxtPhoneNum2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceA_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Architect info?", "Replace with Architect Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtOwner.Text = nameaddrfactor.F4resporg;
                txtPersontoCont1.Text = nameaddrfactor.F4respname;
                txtAddr1.Text = nameaddrfactor.F4addr1;
                txtAddr2.Text = nameaddrfactor.F4addr2;
                txtCityState.Text = nameaddrfactor.F4addr3;
                txtZipcode.Text = nameaddrfactor.F4zip;
                mtxtPhoneNum1.Text = nameaddrfactor.F4phone;
                txtEmailAddr.Text = nameaddrfactor.F4email;
                txtWebAddr.Text = nameaddrfactor.F4weburl;
                if (nameaddrfactor.F4respname != " ")
                    txtFactorOfficial.Text = "ATTN " + nameaddrfactor.F4respname;
                else
                    txtFactorOfficial.Text = "";
                
                txtOtherResp.Text = "";
                mtxtFaxNumber.Text = "";
                txtPersontoCont2.Text = "";
                mtxtPhoneNum2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceE_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Engineer info?", "Replace with Engineer Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtOwner.Text = nameaddrfactor.F5resporg;
                txtPersontoCont1.Text = nameaddrfactor.F5respname;
                txtAddr1.Text = nameaddrfactor.F5addr1;
                txtAddr2.Text = nameaddrfactor.F5addr2;
                txtCityState.Text = nameaddrfactor.F5addr3;
                txtZipcode.Text = nameaddrfactor.F5zip;
                mtxtPhoneNum1.Text = nameaddrfactor.F5phone;
                txtEmailAddr.Text = nameaddrfactor.F5email;
                txtWebAddr.Text = nameaddrfactor.F5weburl;
                if (nameaddrfactor.F5respname != " ")
                    txtFactorOfficial.Text = "ATTN " + nameaddrfactor.F5respname;
                else
                    txtFactorOfficial.Text = "";
                txtOtherResp.Text = "";
                mtxtFaxNumber.Text = "";
                txtPersontoCont2.Text = "";
                mtxtPhoneNum2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }

        private void btnReplaceO2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Do you want to replace the Respondent info with Owner2 info?", "Replace with Owner2 Info?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                txtOwner.Text = nameaddrfactor.F9resporg;
                txtPersontoCont1.Text = nameaddrfactor.F9respname;
                txtAddr1.Text = nameaddrfactor.F9addr1;
                txtAddr2.Text = nameaddrfactor.F9addr2;
                txtCityState.Text = nameaddrfactor.F9addr3;
                txtZipcode.Text = nameaddrfactor.F9zip;
                mtxtPhoneNum1.Text = nameaddrfactor.F9phone;
                txtEmailAddr.Text = nameaddrfactor.F9email;
                txtWebAddr.Text = nameaddrfactor.F9weburl;
                if (nameaddrfactor.F9respname != " ")
                    txtFactorOfficial.Text = "ATTN " + nameaddrfactor.F9respname;
                else
                    txtFactorOfficial.Text = "";
                txtOtherResp.Text = "";
                mtxtFaxNumber.Text = "";
                txtPersontoCont2.Text = "";
                mtxtPhoneNum2.Text = "";
                txtExt2.Text = "";
                txtExt1.Text = "";
                PopulateRstate();
                txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstatevalue);
            }
        }
      
        

        private void txtProjCitySt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        //when rvitm5c or units were changed, costpu need updated
        private void UpdateCostpu()
        {
            //if it is mulitfamily
            if (txtSurvey.Text == "M")
            {
                int it5c = Convert.ToInt32(txtRvitm5c.Text.Replace(",", ""));
                if (it5c > 0)
                {
                    int iunits = Convert.ToInt32(txtRepUnits.Text.Replace(",", ""));
                    if (iunits > 0)
                    {
                        double yy = (double)it5c / iunits;
                        newCostpu = (int)Math.Round(yy);
                    }
                }
            }
        }
    }
}
