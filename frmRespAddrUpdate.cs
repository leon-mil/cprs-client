/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmRespAddrUpdate.cs

 Programmer    : Diane Musachio

 Creation Date : 8/27/2015

 Inputs        : n/a

 Paramaters    : n/a
 
 Output        : n/a
 
 Description   : This program displays and allows user to edit respondent information

 Detail Design : Detailed User Requirements for Respondent Address Update Screen

 Other         :  Forms Using this code: 
 
                  Called by: 

 Revisions     : See Below
 *********************************************************************
 Modified Date : 06/09/2017
 Modified By   : Diane Musachio
 Keyword       : 060917dm
 Change Request: CR#109
 Description   : Validate Canadian zip format and us zip format separately
 *********************************************************************
 Modified Date : 10/06/2017 
 Modified By   : Diane Musachio
 Keyword       : dm100617
 Change Request: 
 Description   : Added time zone 
 *********************************************************************
 * *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using System.Net.Mail;
using System.Globalization;
using System.Collections;
using CprsBLL;
using CprsDAL;
using DGVPrinterHelper;
using System.Windows.Forms.VisualStyles;
using System.IO;
using System.Diagnostics;

namespace Cprs
{
    public partial class frmRespAddrUpdate : Cprs.frmCprsParent
    {
        /* Required */
        public Form CallingForm = null;

        public string RespondentId;

        public string TxtId;

        public frmRespAddrUpdate Current;

        private bool editable;

        private string locked_by;

        TextBox txt;

        RespAddr respaddress;

        RespAddr respaddrupdate;

        RespAuditData radata = new RespAuditData();

        RespAddrData rad = new RespAddrData();

        private List<RespAudit> Respauditlist = new List<RespAudit>();

        //get user name from environment variable

        string user_name = UserInfo.UserName;

        //this triggers the form to load then display the lock message

        private delegate void ShowLockMessageDelegate();

        private delegate void PrintScreenDelegate();

        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

        private bool first;

        private bool done = true;

        private bool canada = false;

        public frmRespAddrUpdate()
        {
            Current = this;

            InitializeComponent();

            tbContact.DrawMode = TabDrawMode.OwnerDrawFixed;

            first = true;

            anytxtmodified = false;
        }

        private void frmRespAddrUpdate_Load(object sender, EventArgs e)
        {
            this.Show();

            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("DATA ENTRY");

            RemoveTxtChanged();
            SetButtonTxt();

            //if a respondent id is present that means the form was accessed by the 
            //frmMarkCaseReview and therefore displays the previous button to return
            //to that form upon click

            if (!String.IsNullOrWhiteSpace(RespondentId))
            {
                btnNextRespid.Text = "PREVIOUS";

                txtRespid.Text = RespondentId; 

                respaddress = rad.GetRespAddr(RespondentId);

                DisplayRespAddr();

                DisplayProjDesc();

                DisplayHistoryList();

                tbContact.DrawItem += new DrawItemEventHandler(tbContact_DrawItem);

                tbContact.Refresh();

                BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));

                SetTxtChanged();
            }
            else
            {
               
                frmRespidPopup fRespid = new frmRespidPopup(true);

                fRespid.ShowDialog();  // show child
                if (fRespid.DialogResult == DialogResult.OK)
                {
                        RemoveTxtChanged();

                        RespondentId = fRespid.NewRespid;

                        first = false;

                        ResetParameters();

                        respaddress = rad.GetRespAddr(RespondentId);

                        DisplayRespAddr();

                        DisplayProjDesc();

                        DisplayHistoryList();

                        BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));

                        SetTxtChanged();
                }


                if ((fRespid.DialogResult == DialogResult.Cancel) && (first == true))
                {
                    this.Close();
                    frmHome fH = new frmHome();
                    fH.Show();
                }
            }

        }

        //this controls the color of text on the contacts tab
        //if primary or secondary is blank the tab text will be black
        //otherwise the tabs are blue
        private void tbContact_DrawItem(object sender, DrawItemEventArgs e)
        {
            
            if (e.Index == 0)
            {
                if (txtContact.Text.Trim() == "")
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
                         Brushes.Black,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
            if (e.Index == 1)
            {
                if (txtContact2.Text.Trim() == "")
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
                         Brushes.Black,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }

           
        }

        //If tab is selected refresh data shown and display appropriate button at bottom of screen

        private void tbProjHist_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbProjHist.Refresh();

            if (tbProjHist.SelectedTab == tbProjects)
            {
                SetButtonTxt();

                lblTotal.Visible = true;
                txtTotal.Visible = true;

                btnC700.Enabled = false;

                if (dgProjList.RowCount > 0)
                { btnC700.Enabled = true; }
            }

            if (tbProjHist.SelectedTab == tbHistory)
            {
                btnC700.Text = "NEW COMMENT";

                lblTotal.Visible = false;
                txtTotal.Visible = false;

                btnC700.Enabled = true;
            }
        }

        //Clear out Text Boxes and set Boolean values
        private void ResetParameters()
        {
            txtOwner.Text = "";
            txtContact.Text = "";
            txtContact2.Text = "";
            txtSpecNote.Text = "";
            txtFactorOff.Text = "";
            txtOtherResp.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtAddr3.Text = "";
            txtPhone.Text = "";
            txtPhone2.Text = "";
            txtExt.Text = "";
            txtExt2.Text = "";
            txtFax.Text = "";
            txtZip.Text = "";
            txtEmail.Text = "";
            txtWeb.Text = "";
            cboLag.Text = null;
            txtActive.Text = "0";
            txtComplete.Text = "0";
            txtAbeyance.Text = "0";
            cbColtec.Text = "";
            txtColhist.Text = "";
            lblLock.Visible = false;
            anytxtmodified = false;
            newval = string.Empty;
            oldval = string.Empty;
            tbProjHist.SelectedTab = tbProjects;
            txtTimeZone.ReadOnly = true;
            tbContact.SelectedTab = tbPrimary;
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
                case EnumGroups.NPCInterviewer:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:  
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

        //Display Respondent's Name and Address Information read in from database

        private void DisplayRespAddr()
        {
            txtRespid.Text = respaddress.Respid;
            txtOwner.Text = respaddress.Resporg;
            txtContact.Text = respaddress.Respname;
            txtContact2.Text = respaddress.Respname2;
            txtSpecNote.Text = respaddress.Respnote;
            txtFactorOff.Text = respaddress.Factoff;
            txtOtherResp.Text = respaddress.Othrresp;
            txtAddr1.Text = respaddress.Addr1;
            txtAddr2.Text = respaddress.Addr2;
            txtAddr3.Text = respaddress.Addr3;
            txtExt.Text = respaddress.Ext;
            txtExt2.Text = respaddress.Ext2;
            txtZip.Text = respaddress.Zip;
            txtEmail.Text = respaddress.Email;
            txtWeb.Text = respaddress.Web;
            txtPhone.Text = respaddress.Phone;
            txtPhone2.Text = respaddress.Phone2;
            txtFax.Text = respaddress.Fax;
            //cbColtec.Text = respaddress.Coltec;
            txtColhist.Text = respaddress.Colhist;
            int x = respaddress.Lag;
            cboLag.Text = x.ToString();
            locked_by = respaddress.Resplock;
            cbColtec.SelectedItem = GetDisplayColtecText(respaddress.Coltec);
            txtColtec.Text = cbColtec.Text;
            string rstate = respaddress.Rstate;
            //dm100617 added timezone
            txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstate);

            tbContact.DrawItem += new DrawItemEventHandler(tbContact_DrawItem);

            tbContact.Refresh();

        }

        //from coltec code to get display text
        private string GetDisplayColtecText(string coltec_code)
        {
            string coltec_text = string.Empty;
            if (coltec_code == "F")
                coltec_text = "F-Form";
            else if (coltec_code == "C")
                coltec_text = "C-Centurion";
            else if (coltec_code == "S")
                coltec_text = "S-Special";
            else if (coltec_code == "P")
                coltec_text = "P-Phone";
            else if (coltec_code == "I")
                coltec_text = "I-Internet";
            else
                coltec_text = "A-Admin";

            return coltec_text;

        }


        //Display the Project Description Information when Project Tab is selected

        private int activeSum;
        private int completeSum;
        private int abeyanceSum;
        private int totalSum;

        private void DisplayProjDesc()
        {
            GetProjDesc p = new GetProjDesc();

            DataTable dtProj = new DataTable();
            DataTable dtCounts = new DataTable();

            dtProj = p.GetProjDescTable(RespondentId);
            dtCounts = p.GetInactiveCounts(RespondentId);

            dgProjList.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgProjList.DataSource = dtProj;

            activeSum = 0;
            completeSum = 0;
            abeyanceSum = 0;
            totalSum = 0;
            //get counts of types of projects to display on screen

            for (int i = 0; i < dtProj.Rows.Count; i++)
            {
                string active = dtProj.Rows[i]["Active"].ToString();
                string status = dtProj.Rows[i]["Status"].ToString();

                if (active == "A")
                {
                    ++activeSum;
                }

                txtActive.Text = activeSum.ToString();

                if (active == "C")
                {
                    ++completeSum;
                }

                txtComplete.Text = completeSum.ToString();

                totalSum++;
            }

            for (int i = 0; i < dtCounts.Rows.Count; i++)
            {
                ++abeyanceSum;
            }

            txtAbeyance.Text = abeyanceSum.ToString();

            txtTotal.Text = totalSum.ToString();

            //populate wrk column based on complete value

            foreach (DataGridViewRow row in dgProjList.Rows)
            {
                string complete = row.Cells["Complete"].Value.ToString();

                if (complete == "Y")
                {
                    row.Cells["Wrk"].Value = "*";
                }

            }
            btnC700.Enabled = false;

            if (dgProjList.RowCount > 0)
            { btnC700.Enabled = true; }

        }

        //Display the History List information when History Tab is selected

        private void DisplayHistoryList()
        {
            HistoryData hd = new HistoryData();

            DataTable dtGetHist = new DataTable();

            dtGetHist = hd.GetRespCommentTable(RespondentId);

            dgGetHistory.DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dgGetHistory.DataSource = dtGetHist;
            dgPrint.DataSource = dtGetHist;
        }

        //Set up the txt_txtChange Event to be Called after Form Initialization

        private void SetTxtChanged()
        {
            this.txtOtherResp.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtContact.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtContact2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtAddr2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtAddr1.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtFactorOff.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtOwner.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtZip.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtExt.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtExt2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtFax.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtPhone.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtPhone2.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtWeb.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtAddr3.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtEmail.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.cboLag.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtSpecNote.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.txtColhist.TextChanged += new System.EventHandler(this.txt_TextChanged);
            this.cbColtec.TextChanged += new System.EventHandler(this.txt_TextChanged);
        }

        //Clear the txt_txtChange Event due to Next Respid Form Initialization

        private void RemoveTxtChanged()
        {
            this.txtOtherResp.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtContact.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtContact2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtAddr2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtAddr1.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtFactorOff.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtOwner.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtZip.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtExt.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtExt2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtFax.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtPhone.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtPhone2.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtWeb.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtAddr3.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtEmail.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.cboLag.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtSpecNote.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.txtColhist.TextChanged -= new System.EventHandler(this.txt_TextChanged);
            this.cbColtec.TextChanged -= new System.EventHandler(this.txt_TextChanged);
        }

        //Check if the Case is Locked by Another User and Display User Name that has Respid Locked

        private void ShowLockMessage()
        {
            if (!String.IsNullOrWhiteSpace(locked_by))
            {
                MessageBox.Show("The case is locked by " + locked_by + ", cannot be edited.");

                editable = false;
                lblLock.Visible = true;
                btnReset.Enabled = false;

                //If Case is Locked Textboxes Need to Not Accept Text Input
                //and Display Grayed out Textboxes
                txtRespid.ReadOnly = true;
                txtOwner.ReadOnly = true;
                txtContact.ReadOnly = true;
                txtContact2.ReadOnly = true;
                txtSpecNote.ReadOnly = true;
                txtFactorOff.ReadOnly = true;
                txtOtherResp.ReadOnly = true;
                txtAddr1.ReadOnly = true;
                txtAddr2.ReadOnly = true;
                txtAddr3.ReadOnly = true;
                txtZip.ReadOnly = true;
                txtPhone.ReadOnly = true;
                txtExt.ReadOnly = true;
                txtPhone2.ReadOnly = true;
                txtExt2.ReadOnly = true;
                txtFax.ReadOnly = true;
                txtEmail.ReadOnly = true;
                txtWeb.ReadOnly = true;
                //dm100617 added timezone
                txtTimeZone.ReadOnly = true;
                cboLag.Enabled = false;
                cbColtec.Enabled = false;
                cboLag.DrawItem += new DrawItemEventHandler(cboLag_DrawItem);
            }
            else
            {
                editable = true;
                lblLock.Visible = false;
                btnReset.Enabled = true;
                locked_by = UserInfo.UserName;

                //If Case is Editable then Set Resplock value to Current User's Information

                GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, locked_by);

                txtRespid.ReadOnly = false;
                txtOwner.ReadOnly = false;
                txtContact.ReadOnly = false;
                txtContact2.ReadOnly = false;
                txtSpecNote.ReadOnly = false;
                txtFactorOff.ReadOnly = false;
                txtOtherResp.ReadOnly = false;
                txtAddr1.ReadOnly = false;
                txtAddr2.ReadOnly = false;
                txtAddr3.ReadOnly = false;
                txtZip.ReadOnly = false;
                txtPhone.ReadOnly = false;
                txtPhone2.ReadOnly = false;
                txtExt.ReadOnly = false;
                txtExt2.ReadOnly = false;
                txtFax.ReadOnly = false;
                txtEmail.ReadOnly = false;
                txtWeb.ReadOnly = false;
                cboLag.Enabled = true;
                cbColtec.Enabled = true;
                cboLag.DrawItem += new DrawItemEventHandler(cboLag_DrawItem);
            }
        }

        private bool anytxtmodified = false;

        //add variables to keep the textbox content

        private string newval = string.Empty;

        private string oldval = string.Empty;

        private string varnme;

        private string coltec;

        //Set up Method to Determine if Relevant Textboxes and Masked Textboxes Have Been Modified

        private void txt_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox)
            {
                txt = new TextBox();

                if (sender == txtOwner)
                {
                    varnme = "RESPORG";
                    oldval = respaddress.Resporg.Trim().ToString();
                    newval = txtOwner.Text.Trim().ToString();
                }

                if (sender == txtContact)
                {
                    varnme = "RESPNAME";
                    oldval = respaddress.Respname.Trim().ToString();
                    newval = txtContact.Text.Trim().ToString();
                }

                if (sender == txtContact2)
                {
                    varnme = "RESPNAME2";
                    oldval = respaddress.Respname2.Trim().ToString();
                    newval = txtContact2.Text.Trim().ToString();
                }

                if (sender == txtFactorOff)
                {
                    varnme = "FACTOFF";
                    oldval = respaddress.Factoff.Trim().ToString();
                    newval = txtFactorOff.Text.Trim().ToString();
                }

                if (sender == txtOtherResp)
                {
                    varnme = "OTHRRESP";
                    oldval = respaddress.Othrresp.Trim().ToString();
                    newval = txtOtherResp.Text.Trim().ToString();
                }

                if (sender == txtAddr1)
                {
                    varnme = "ADDR1";
                    oldval = respaddress.Addr1.Trim().ToString();
                    newval = txtAddr1.Text.Trim().ToString();
                }

                if (sender == txtAddr2)
                {
                    varnme = "ADDR2";
                    oldval = respaddress.Addr2.Trim().ToString();
                    newval = txtAddr2.Text.Trim().ToString();
                }

                if (sender == txtAddr3)
                {
                    varnme = "ADDR3";
                    oldval = respaddress.Addr3.Trim().ToString();
                    newval = txtAddr3.Text.Trim().ToString();
                }

                if (sender == txtZip)
                {
                    varnme = "ZIP";
                    oldval = respaddress.Zip.Trim().ToString();
                    newval = txtZip.Text.Trim().ToString();
                }

                if (sender == txtEmail)
                {
                    varnme = "EMAIL";
                    oldval = respaddress.Email.Trim().ToString();
                    newval = txtEmail.Text.Trim().ToString();
                }

                if (sender == txtWeb)
                {
                    varnme = "WEBURL";
                    oldval = respaddress.Web.Trim().ToString();
                    newval = txtWeb.Text.Trim().ToString();
                }

                if (sender == txtSpecNote)
                {
                    varnme = "SPECNOTE";
                    oldval = respaddress.Respnote.Trim().ToString();
                    newval = txtSpecNote.Text.Trim().ToString();
                }

                if (sender == txtColhist)
                {
                    varnme = "COLHIST";
                    oldval = respaddress.Colhist.Trim().ToString();
                    newval = txtColhist.Text.Trim().ToString();
                }

                if (sender == txtExt)
                {
                    varnme = "EXT";
                    oldval = respaddress.Ext.Trim().ToString();
                    newval = txtExt.Text.Trim().ToString();
                }

                if (sender == txtExt2)
                {
                    varnme = "EXT2";
                    oldval = respaddress.Ext2.Trim().ToString();
                    newval = txtExt2.Text.Trim().ToString();
                }
            }
        
    
            //Must Declare Masked Textboxes in this Way to Avoid Exceptions

            if (sender is MaskedTextBox)
            {
                txt = new TextBox();

                if (sender == txtPhone) 
                {
                    varnme = "PHONE";
                    oldval = respaddress.Phone.Trim().ToString();
                    newval = txtPhone.Text.Trim().ToString();
                }
               
                if (sender == txtPhone2)
                {
                    varnme = "PHONE2";
                    oldval = respaddress.Phone2.Trim().ToString();
                    newval = txtPhone2.Text.Trim().ToString();
                }

                if (sender == txtFax)
                {
                    varnme = "FAX"; 
                    oldval = respaddress.Fax.Trim().ToString();
                    newval = txtFax.Text.Trim().ToString();
                }

            }

            if (sender is ComboBox)
            {
                txt = new TextBox();

                if (sender == cbColtec)
                {
                    varnme = "COLTEC";
                    oldval = respaddress.Coltec;
                    newval = cbColtec.Text.Substring(0, 1);
                    coltec = newval;
            
                }

                if (sender == cboLag)
                {
                    varnme = "LAG";
                    oldval = respaddress.Lag.ToString();
                    newval = cboLag.Text.Trim().ToString();
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

            /*Get audit record from list */

            if ((!((varnme == "SPECNOTE") || (varnme == "COLHIST")))  && anytxtmodified)
            {
                RespAudit au = (from RespAudit j in Respauditlist
                                where j.Varnme == varnme
                                select j).SingleOrDefault();

                /*if there is no record, add one, otherwise update the record */
                //This update indicates the variable was already changed once and is
                //being updated a second time

                if (au == null)
                {
                    RespAudit ra = new RespAudit();
                    ra.Respid = RespondentId;
                    ra.Varnme = varnme;
                    ra.Oldval = oldval;
                    ra.Newval = newval;
                    ra.Usrnme = UserInfo.UserName;
                    ra.Prgdtm = DateTime.Now;

                    Respauditlist.Add(ra);
                }
                else
                {
                    au.Newval = newval;
                    au.Prgdtm = DateTime.Now;
                }
            }


        }


       // private string rstatevalue;

        //public string validrstate;

        private string rstate;

        private RespAuditData raudit = new RespAuditData();

        private void txtAddr3_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            ValidateCityState();
  
        }

        //validate City State field
        private bool ValidateCityState()
        {
            bool result = true;

            //check valid state
            if (string.IsNullOrWhiteSpace(txtAddr3.Text) || GeneralFunctions.HasSpecialCharsInCityState(txtAddr3.Text))
            {
                MessageBox.Show("City/State is invalid.");
                txtAddr3.Focus();
                txtAddr3.Text = respaddress.Addr3;
                Respauditlist.RemoveAll(x => x.Varnme == "ADDR3" && x.Respid == this.txtRespid.Text);
                anytxtmodified = true;
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
                    txtAddr3.Text = respaddress.Addr3;
                    Respauditlist.RemoveAll(x => x.Varnme == "ADDR3" && x.Respid == this.txtRespid.Text);
                    anytxtmodified = true;
                    result = false;
                }
                //060917dm separated canada and us zip validations
                else
                {
                    if (words[num_word - 1] == "CANADA")
                    {
                        rst = words[num_word - 2];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, true))
                        {
                            MessageBox.Show("State/Province is invalid.");
                            txtAddr3.Focus();
                            txtAddr3.Text = respaddress.Addr3;
                            Respauditlist.RemoveAll(x => x.Varnme == "ADDR3" && x.Respid == this.txtRespid.Text);
                            anytxtmodified = true;
                            result = false;
                        }
                        else
                        {
                            rstate = rst;
                            //dm100617 added timezone
                            txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstate);
                            canada = true;
                        }
                    }
                    else
                    {
                        rst = words[num_word - 1];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, false))
                        {
                            MessageBox.Show("State/Province is invalid.");
                            txtAddr3.Focus();
                            txtAddr3.Text = respaddress.Addr3;
                            Respauditlist.RemoveAll(x => x.Varnme == "ADDR3" && x.Respid == this.txtRespid.Text);
                            anytxtmodified = true;
                            result = false;
                        }
                        else
                        {
                            rstate = rst;
                            //dm100617 added timezone
                            txtTimeZone.Text = GeneralDataFuctions.GetTimezone(rstate);
                            canada = false;
                        }
                    }
                }
            }

            return result;
        }

        //Validate email input by user is in acceptable format

        private void txtAddr1_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            if(txtAddr1.Text.Trim() == "")
            {
                 DialogResult result = MessageBox.Show("Address is invalid.", "OK", MessageBoxButtons.OK);

                 if (result == DialogResult.OK)
                 {
                     txtAddr1.Focus();
                     txtAddr1.Text = respaddress.Addr1;
                     Respauditlist.RemoveAll(x => x.Varnme == "ADDR1" && x.Respid == this.txtRespid.Text);
                     anytxtmodified = true;
                     return;
                 }
             }
        }

        //Validate email input by user is in acceptable format

        private void txtEmail_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
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
                        DialogResult result = MessageBox.Show("Email Address is invalid.", "OK", MessageBoxButtons.OK);

                        if (result == DialogResult.OK)
                        {
                            txtEmail.Focus();
                            txtEmail.Text = respaddress.Email;
                            Respauditlist.RemoveAll(x => x.Varnme == "EMAIL" && x.Respid == this.txtRespid.Text);
                            anytxtmodified = true;
                            return;
                        }
                    }
                }
            
        }

        //Validate web input by user is in acceptable format

        private void txtWeb_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            if (txtWeb.Text.Trim() != "")
            {
                if (!GeneralData.IsValidURL(txtWeb.Text))
                {
                    DialogResult result = MessageBox.Show("Web Address is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtWeb.Focus();
                        txtWeb.Text = respaddress.Web;
                        Respauditlist.RemoveAll(x => x.Varnme == "WEBURL" && x.Respid == this.txtRespid.Text);
                        anytxtmodified = true;
                        return;
                    }
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

        //Can only enter numbers
        private void txtNumbersOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //Validate phone number is valid length
        //make sure masked textbox format is set to exclude literals and prompts

        private void txtPhone_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            if ((txtPhone.MaskCompleted == false) && (txtPhone.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtPhone.Focus();
                    txtPhone.Text = respaddress.Phone;
                    Respauditlist.RemoveAll(x => x.Varnme == "PHONE" && x.Respid == this.txtRespid.Text);
                    anytxtmodified = true;
                    return;
                }
            }
        }

        private void txtPhone2_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            if ((txtPhone2.MaskCompleted == false) && (txtPhone2.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtPhone2.Focus();
                    txtPhone2.Text = respaddress.Phone2;
                    Respauditlist.RemoveAll(x => x.Varnme == "PHONE2" && x.Respid == this.txtRespid.Text);
                    anytxtmodified = true;
                    return;
                }
            }
        }

        //Validate fax number is valid length
        //make sure masked textbox format is set to exclude literals and prompts

        private void txtFax_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            if ((txtFax.MaskCompleted == false) && (txtFax.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Fax number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtFax.Focus();
                    txtFax.Text = respaddress.Fax;
                    Respauditlist.RemoveAll(x => x.Varnme == "FAX" && x.Respid == this.txtRespid.Text);
                    anytxtmodified = true;
                    return;
                }
            }
        }


        //Validate zip is valid us or canadian
        //060917dm separated canada and us zip validations
        private void txtZip_Leave(object sender, EventArgs e)
        {
            if (btnReset.Focused)
            {
                return;
            }

            ValidateCityState();

            //if canada and wrong format or us and wrong format
            if ((canada == true) && (!GeneralData.IsCanadianZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtZip.Focus();
                    txtZip.Text = respaddress.Zip.Trim();
                    Respauditlist.RemoveAll(x => x.Varnme == "ZIP" && x.Respid == this.txtRespid.Text);
                    anytxtmodified = true;
                    return;
                }
            }
            else if ((canada == false) && (!GeneralData.IsUsZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtZip.Focus();
                    txtZip.Text = respaddress.Zip.Trim();
                    Respauditlist.RemoveAll(x => x.Varnme == "ZIP" && x.Respid == this.txtRespid.Text);
                    anytxtmodified = true;
                    return;
                }
            }
        }

        private bool notvalid;

        private void SaveData()
        {
            notvalid = false;

            if ((txtFax.Text.Length != 10) && (txtFax.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Fax number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtFax.Focus();
                    txtFax.Text = respaddress.Fax;
                    Respauditlist.RemoveAll(x => x.Varnme == "FAX" && x.Respid == this.txtRespid.Text);
                }
            }

           
            if (txtAddr1.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Address is invalid", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtAddr1.Focus();
                        txtAddr1.Text = respaddress.Addr1;
                        Respauditlist.RemoveAll(x => x.Varnme == "ADDR1" && x.Respid == this.txtRespid.Text);
                    }
            }

            if ((txtPhone2.Text.Length != 10) && (txtPhone2.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtPhone2.Focus();
                    txtPhone2.Text = respaddress.Phone2;
                    Respauditlist.RemoveAll(x => x.Varnme == "PHONE2" && x.Respid == this.txtRespid.Text);
                }
            }

            if ((txtPhone.Text.Length != 10) && (txtPhone.Text.Trim() != ""))
            {
                DialogResult result = MessageBox.Show("Phone number is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtPhone.Focus();
                    txtPhone.Text = respaddress.Phone;
                    Respauditlist.RemoveAll(x => x.Varnme == "PHONE" && x.Respid == this.txtRespid.Text);
                }
            }

            if (txtWeb.Text.Trim() != "")
            {
                if (!GeneralData.IsValidURL(txtWeb.Text))
                {
                    DialogResult result = MessageBox.Show("Web Address is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtWeb.Focus();
                        txtWeb.Text = respaddress.Web;
                        Respauditlist.RemoveAll(x => x.Varnme == "WEBURL" && x.Respid == this.txtRespid.Text);
                    }
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
                    DialogResult result = MessageBox.Show("Email Address is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        notvalid = true;
                        txtEmail.Focus();
                        txtEmail.Text = respaddress.Email;
                        Respauditlist.RemoveAll(x => x.Varnme == "EMAIL" && x.Respid == this.txtRespid.Text);
                    }
                }
            }

            ValidateCityState();

            //060917dm separated canada and us zip validations
            //if canada and wrong format 
            if ((canada == true) && (!GeneralData.IsCanadianZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid.", "OK", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtZip.Focus();
                    txtZip.Text = respaddress.Zip.Trim();
                    Respauditlist.RemoveAll(x => x.Varnme == "ZIP" && x.Respid == this.txtRespid.Text);
                }
            }
            //if us and wrong format
            else if ((canada == false) && (!GeneralData.IsUsZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid.", "OK", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    notvalid = true;
                    txtZip.Focus();
                    txtZip.Text = respaddress.Zip.Trim();
                    Respauditlist.RemoveAll(x => x.Varnme == "ZIP" && x.Respid == this.txtRespid.Text);
                }
            }


            UpdateResp ur = new UpdateResp();
 
            respaddrupdate = ur.UpdateRespData(this.txtRespid.Text, this.txtContact.Text, this.txtContact2.Text, this.txtOwner.Text,
               this.txtSpecNote.Text, this.txtFactorOff.Text, this.txtOtherResp.Text, this.txtAddr1.Text,
               this.txtAddr2.Text, this.txtAddr3.Text, this.txtPhone.Text, this.txtPhone2.Text, this.txtExt.Text, this.txtExt2.Text, this.txtFax.Text,
               this.txtZip.Text, this.txtEmail.Text, this.txtWeb.Text, this.cboLag.Text, rstate , coltec);

            DateTime prgdtm = DateTime.Now;

                if (Respauditlist.Count > 0)
                {
                    foreach (RespAudit element in Respauditlist)
                    {
                        radata.AddRespauditData(element);
                    }

                    Respauditlist.Clear();

                    anytxtmodified = false;
                }
            
        }

        private void DataChanged()
        {
            done = true;

                if (anytxtmodified)
                {
                    DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        SaveData();

                        if (notvalid)
                        {
                            done = false;
                            anytxtmodified = true;
                        }
                    }
                }
        }

        private void MaskedTextBox_Enter(object sender, EventArgs e)
        {
            if (sender is MaskedTextBox)
            {
                (sender as MaskedTextBox).Focus();
                (sender as MaskedTextBox).SelectionStart = 0;
     
            }
        }

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled ? Color.DarkBlue : Color.Gray;
        }

        private void btnReset_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            var drawBrush = new SolidBrush(btn.ForeColor);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            btnReset.Text = string.Empty; //remove the button text
            e.Graphics.DrawString("REFRESH", btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();
        }


        private void cboLag_DrawItem(object sender, DrawItemEventArgs e)
        {
            int index = e.Index >= 0 ? e.Index : 0;

            float size = 0;
            System.Drawing.Font myFont;
            FontFamily family = null;

            System.Drawing.Color myColor = new System.Drawing.Color();
            if ((e.Index < 0) || (cboLag.Enabled))
            {
                myColor = System.Drawing.Color.White;
            }
            else
            {
                myColor = System.Drawing.Color.LightGray;
            }
            size = 8;
            family = FontFamily.GenericSansSerif;

            // Draw the background of the item.
            e.DrawBackground();

            //Create a square filled with the  color. Vary the size
            //of the rectangle based on the length ofname.
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

        private void btnNextRespid_Click(object sender, EventArgs e)
        {
            if (btnNextRespid.Text == "PREVIOUS")
            {
                if (anytxtmodified)
                {
                    DialogResult result2 = MessageBox.Show("The data was changed, do you want to save it?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (result2 == DialogResult.Yes)
                    {
                        SaveData();
                    }
                }

                if (CallingForm != null)
                {
                    CallingForm.Show();
                }
                this.Close();
            }
            else
            {
                if (anytxtmodified)
                {
                    SaveData();
                                  
                    respaddress = rad.GetRespAddr(RespondentId);
                }

                anytxtmodified = false;

                frmRespidPopup fRespid = new frmRespidPopup(true);

                fRespid.ShowDialog();  // show child

                if (fRespid.DialogResult == DialogResult.OK)
                {
                    if (editable)
                    {
                        GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, "");
                    }

                    RespondentId = fRespid.NewRespid;

                    RemoveTxtChanged();

                    ResetParameters();

                    respaddress = rad.GetRespAddr(RespondentId);

                    DisplayRespAddr();

                    DisplayProjDesc();

                    DisplayHistoryList();

                    BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));

                    SetTxtChanged();
                }
            }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (tbProjHist.SelectedTab == tbHistory)
            {
                frmRespComment newcomm = new frmRespComment(RespondentId);

                newcomm.ShowDialog();

                DisplayHistoryList();
            }
            else
            {
               if (anytxtmodified)
               {
                   SaveData();
               }

                DataGridViewSelectedRowCollection rows = dgProjList.SelectedRows;

                int index = dgProjList.CurrentRow.Index;

                if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
               {
                    if (locked_by == user_name)
                    {
                        GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, "");
                    }

                    frmTfu tfu = new frmTfu();
                   
                    tfu.RespId = RespondentId;
                    tfu.CallingForm = this;

                    GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");
                    tfu.ShowDialog();   // show child

                    RespondentId = tfu.RespId;

                    GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");

                    if (locked_by == user_name)
                    {
                        GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, locked_by);
                    }
                }
               else
               {
                   if (locked_by == user_name)
                   {
                        GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, "");
                   }

                   this.Hide();        // hide parent

                   frmC700 fC700 = new frmC700();

                   string val1 = dgProjList["ID", index].Value.ToString();

                   // Store ID in list for Page Up and Page Down

                   List<string> Idlist = new List<string>();

                   int cnt = 0;

                   foreach (DataGridViewRow dr in dgProjList.Rows)
                   {
                       string val = dgProjList["ID", cnt].Value.ToString();
                       Idlist.Add(val);
                       cnt = cnt + 1;
                   }

                   fC700.Id = val1;
                   fC700.Idlist = Idlist;
                   fC700.CurrIndex = index;
                   fC700.CallingForm = this;

                   GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");

                   fC700.ShowDialog(); // show child
                    SampleData sdata = new SampleData();
                    Sample samp = sdata.GetSampleData(fC700.Id);
                   RespondentId = samp.Respid;

                   GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");

                   if (locked_by == user_name)
                   { 
                       GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, locked_by);
                   }
                }

                respaddress = rad.GetRespAddr(RespondentId);

                DisplayRespAddr();

                DisplayProjDesc();

                DisplayHistoryList();

                tbContact.DrawItem += new DrawItemEventHandler(tbContact_DrawItem);

                tbContact.Refresh();

            }         
        }

        //Displays original data when reset button is selected

        private void btnReset_Click(object sender, EventArgs e)
        {
            {
                respaddress =rad.GetRespAddr(RespondentId);

                DisplayRespAddr();
            }
        }

        private void frmRespAddrUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (editable)
            {
                locked_by = "";

                GeneralDataFuctions.UpdateRespIDLock(this.txtRespid.Text, locked_by);
            }

            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");

        }

        //Verify Form closing
        public override bool VerifyFormClosing()
        {
            DataChanged();

            bool can_close = true;

            if (done == false)
            {
                can_close = false;
            }

            return can_close;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            BeginInvoke(new PrintScreenDelegate(PrintData));
        }

        Bitmap memoryImage;
        Bitmap resized;

        private void CaptureScreen()
        {
            memoryImage = new Bitmap(panel3.Width, panel3.Height);
            this.panel3.DrawToBitmap(memoryImage, new Rectangle(0, 0, panel3.Width, panel3.Height));
            resized = new Bitmap(memoryImage, new Size(memoryImage.Width * 9 / 10, memoryImage.Height * 9 / 10));
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


            DGVPrinter printer = new DGVPrinter();

            printer.Title = "Respondent Update";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Respondent Update Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            

            printer.Footer = " ";

            if (tbProjHist.SelectedTab == tbProjects)
            {
                /*Hide the columns */
                printer.HideColumns.Add("WRK");
                printer.HideColumns.Add("CONTRACT");
                printer.HideColumns.Add("STATCODE");
                printer.HideColumns.Add("PRIORITY");

                printer.PrintDataGridViewWithoutDialog(dgProjList);
            }
            else
            {
                dgPrint.Columns[2].Width = 600;
                printer.PrintDataGridViewWithoutDialog(dgPrint);
            }

            Cursor.Current = Cursors.Default;
        }

        private void printDocument1_PrintPage(System.Object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }

        private void dgProjList_Sorted(object sender, EventArgs e)
        {
            //populate wrk column based on complete value

            foreach (DataGridViewRow row in dgProjList.Rows)
            {
                string complete = row.Cells["Complete"].Value.ToString();

                if (complete == "Y")
                {
                    row.Cells["Wrk"].Value = "*";
                }

            }
        }
    }
}

