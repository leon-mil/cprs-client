/**********************************************************************************
Econ App Name  : CPRS
Project Name   : CPRS Interactive Screens System
Program Name   : frmNPNonResSpecialNA.cs
Programmer     : Diane Musachio        
Creation Date  : November 9, 2017

Inputs         : Fin
                 FinList
                 CallingForm
                 CurrIndex            
Parameters     : none		                  
Outputs        : none 		   

Description    : Display cases identified by the Survey of Construction as 
                 Non-Permit Non-Residential

Detailed Design: NP Special Cases

Other          : called by:   data entry -> special cases       
 
Rev History    : See Below	
***********************************************************************************
Modified Date  :  
Modified By    :  
Keyword        :  
Change Request :  
Description    :  
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmNPNonResSpecialNA : frmCprsParent
    {
        /******public properties ********/
        /*Required */
        public string fin;
        public frmNPNonResSpecialCases CallingForm = null;

        /*Optional */
        public List<string> Finlist = null;
        public int CurrIndex = 0;

        private bool formModified;

        private bool can_validate;

        private bool updatelock;
        private string lockedby;
        private string fipstate2;
        private bool editable = true;
        private bool can_close = true;

        /*flag to use closing the calling form */
        private bool call_callingFrom = false;
        private delegate void ShowLockMessageDelegate();

        NPSpecCases sc;
        NPSpecialCases npsc = new NPSpecialCases();

        public frmNPNonResSpecialNA()
        {
            InitializeComponent();
        }

        private void frmNPNonResSpecialNA_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("DATA ENTRY");

            sc = npsc.GetFinCase(fin);

            LoadForm();

            BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
        }

        private void LoadForm()
        {
            //convert fipstate numeric code to alphabetical abbreviation
            fipstate2 = GeneralDataFuctions.GetFipState(sc.Fipstate);

            txtPsu.Text = sc.Psu;
            txtBpoid.Text = sc.Bpoid;
            txtSched.Text = sc.Sched;
            txtSeldate.Text = sc.Seldate;
            int SelVal = Convert.ToInt32(sc.Projselv);
            txtProjselv.Text = sc.Projselv > 0 ? sc.Projselv.ToString("#,#") : sc.Projselv.ToString();
            txtFipsstate.Text = fipstate2;
            txtOwner.Text = sc.Owner;
            txtFwgt.Text = sc.Fwgt.ToString("N2");
            txtNewtc.Text = sc.Newtc;
            txtSource.Text = sc.Source;
            txtFin.Text = fin;
            txtProjDesc.Text = sc.Projdesc;
            txtProjLoc.Text = sc.Projloc;
            txtPcityst.Text = sc.Pcityst;
            txtResporg.Text = sc.Resporg;
            txtFactoff.Text = sc.Factoff;
            txtOthrresp.Text = sc.Othrresp;
            txtAddr1.Text = sc.Addr1;
            txtAddr2.Text = sc.Addr2;
            txtAddr3.Text = sc.Addr3;
            txtZip.Text = sc.Zip;
            txtRespname.Text = sc.Respname;
            mtxtPhone.Text = sc.Phone;
            txtExt.Text = sc.Ext;
            mtxtFax.Text = sc.Fax;

            txtCurrentrec.Text = (CurrIndex + 1).ToString();
            txtTotalrec.Text = Finlist.Count.ToString();

            can_validate = false;

            editable = true;

            txtNewtc.Focus();
        }

        //show lock message
        private void ShowLockMessage()
        {
            lockedby = npsc.ChkLocked(fin);

            if (!String.IsNullOrWhiteSpace(lockedby))
            {
                MessageBox.Show("Selected case is locked by " + lockedby + " and cannot be edited");

                lblLockedBy.Visible = true;
                btnRefresh.Enabled = false;
                btnNewtc.Enabled = false;
                editable = false;

                //If Case is Locked Textboxes Need to Not Accept Text Input
                //and Not Display Grayed out Textboxes
                txtNewtc.ReadOnly = true;
                txtProjDesc.ReadOnly = true;
                txtProjLoc.ReadOnly = true;
                txtPcityst.ReadOnly = true;
                txtResporg.ReadOnly = true;
                txtFactoff.ReadOnly = true;
                txtOthrresp.ReadOnly = true;
                txtAddr1.ReadOnly = true;
                txtAddr2.ReadOnly = true;
                txtAddr3.ReadOnly = true;
                txtZip.ReadOnly = true;
                txtRespname.ReadOnly = true;
                mtxtPhone.ReadOnly = true;
                txtExt.ReadOnly = true;
                mtxtFax.ReadOnly = true;
            }
            else
            {
                npsc.UpdateLock(fin, UserInfo.UserName);
                lockedby = UserInfo.UserName;
                btnRefresh.Enabled = true;
                btnNewtc.Enabled = true;
                lblLockedBy.Visible = false;

                txtNewtc.ReadOnly = false;
                txtProjDesc.ReadOnly = false;
                txtProjLoc.ReadOnly = false;
                txtPcityst.ReadOnly = false;
                txtResporg.ReadOnly = false;
                txtFactoff.ReadOnly = false;
                txtOthrresp.ReadOnly = false;
                txtAddr1.ReadOnly = false;
                txtAddr2.ReadOnly = false;
                txtAddr3.ReadOnly = false;
                txtZip.ReadOnly = false;
                txtRespname.ReadOnly = false;
                mtxtPhone.ReadOnly = false;
                txtExt.ReadOnly = false;
                mtxtFax.ReadOnly = false;
            }
        }
        private void btnNewtc_Click(object sender, EventArgs e)
        {
            frmNewtcSel popup = new frmNewtcSel();

            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                if (popup.SelectedNewtc != "")
                {
                    txtNewtc.Text = popup.SelectedNewtc;
                }
                else
                {
                    txtNewtc.Text = sc.Newtc;
                    txtNewtc.Focus();
                }
            }

            popup.Dispose();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.Disable;

            LoadForm();
        }
        private void SaveData()
        {
            sc.Fin = txtFin.Text;
            sc.Newtc = txtNewtc.Text;
            sc.Projdesc = txtProjDesc.Text;
            sc.Projloc = txtProjLoc.Text;
            sc.Pcityst = txtPcityst.Text;
            sc.Resporg = txtResporg.Text;
            sc.Factoff = txtFactoff.Text;
            sc.Othrresp = txtOthrresp.Text;
            sc.Addr1 = txtAddr1.Text;
            sc.Addr2 = txtAddr2.Text;
            sc.Addr3 = txtAddr3.Text;
            sc.Zip = txtZip.Text;
            sc.Respname = txtRespname.Text;
            sc.Phone = mtxtPhone.Text;
            sc.Ext = txtExt.Text;
            sc.Fax = mtxtFax.Text;

            npsc.UpdateNPSpecialCaseData(sc);
            
        }
        private bool CheckFormModified()
        {
            if ((txtNewtc.Text != sc.Newtc)
                || (txtProjDesc.Text != sc.Projdesc)
                || (txtProjLoc.Text != sc.Projloc)
                || (txtPcityst.Text != sc.Pcityst)
                || (txtResporg.Text != sc.Resporg)
                || (txtRespname.Text != sc.Respname)
                || (mtxtPhone.Text != sc.Phone.Trim())
                || (txtExt.Text != sc.Ext.Trim())
                || (mtxtFax.Text != sc.Fax.Trim())
                || (txtFactoff.Text != sc.Factoff)
                || (txtOthrresp.Text != sc.Othrresp)
                || (txtAddr1.Text != sc.Addr1)
                || (txtAddr2.Text != sc.Addr2)
                || (txtAddr3.Text != sc.Addr3)
                || (txtZip.Text != sc.Zip))
            {
                formModified = true;
                return true;
            }
            else
            {
                formModified = false;
                return false;
            }
        }
        private void btnPrint_Click(object sender, EventArgs e)
        {
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
        private void btnPrevCase_Click(object sender, EventArgs e)
        {
            CheckFormModified();

            if (formModified)
            {
                if (ValidData())
                {
                    SaveData();
                }
                else return;
            }

            if (CurrIndex == 0)
            {
                MessageBox.Show("You are at the first observation");
            }
            else
            {
                if (editable)
                {
                    updatelock = npsc.UpdateLock(fin, "");
                }

                CurrIndex = CurrIndex - 1;
                fin = Finlist[CurrIndex];
                sc = npsc.GetFinCase(fin);
                LoadForm();

                BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
            }
        }
        private void btnNextCase_Click(object sender, EventArgs e)
        {
            CheckFormModified();

            if (formModified)
            {
                if (ValidData())
                {
                    SaveData();
                }
                else return;
            }

            if (CurrIndex == Finlist.Count - 1)
            {
                MessageBox.Show("You are at the last observation");
            }
            else
            {
                if (editable)
                {
                    updatelock = npsc.UpdateLock(fin, "");
                }

                CurrIndex = CurrIndex + 1;
                fin = Finlist[CurrIndex];
                sc = npsc.GetFinCase(fin);
                LoadForm();

                BeginInvoke(new ShowLockMessageDelegate(ShowLockMessage));
            }
        }
        private void btnReturn_Click(object sender, EventArgs e)
        {
            CheckFormModified();

            if (formModified)
            {
                if (ValidData())
                {
                    SaveData();
                    /*unlock fin */
                    if (editable)
                    {
                        updatelock = npsc.UpdateLock(fin, "");
                    }
                }
                else return;
            }
            else
            {
                /*unlock fin */
                if (editable)
                {
                    updatelock = npsc.UpdateLock(fin, "");
                }
            }

            if (CallingForm != null)
            {
                CallingForm.Show();
                CallingForm.RefreshData();
                call_callingFrom = true;
            }

            this.Close();
        }
        private void txtNewtc_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private string rstate;

        //validate City State field
        private bool ValidateCityState()
        {
            bool result = true;

            //check valid state
            if (string.IsNullOrWhiteSpace(txtAddr3.Text) || GeneralFunctions.HasSpecialCharsInCityState(txtAddr3.Text))
            {
                MessageBox.Show("City/State is invalid.");
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
                    result = false;
                }
                else
                {
                    //if canada
                    if (words[num_word - 1] == "CANADA")
                    {
                        rst = words[num_word - 2];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, true))
                        {
                            MessageBox.Show("State/Province is invalid.");
                            txtAddr3.Focus();
                            txtAddr3.Text = sc.Addr3;
                            result = false;
                        }
                        else rstate = rst;
                    }
                    //if u.s.
                    else
                    {
                        rst = words[num_word - 1];
                        if (!GeneralDataFuctions.CheckValidRstate(rst, false))
                        {
                            MessageBox.Show("State/Province is invalid.");
                            txtAddr3.Focus();
                            txtAddr3.Text = sc.Addr3;
                            result = false;
                        }
                        else rstate = rst;
                    }
                }
            }
            return result;
        }
        private void txtNewtc_Validating(object sender, CancelEventArgs e)
        {
            string newtcval = txtNewtc.Text;
        
            if (!ValidateNewtc())
            {
                MessageBox.Show("The Newtc value entered is invalid.");
                txtNewtc.Focus();
                txtNewtc.Text = sc.Newtc;
                return;
           }
            
        }
        private bool ValidateNewtc()
        {
            //check it validate newtc or not
            bool NewTCresult;
            NewTCresult = GeneralDataFuctions.CheckNewTC(txtNewtc.Text, "*");
            return NewTCresult;
        }
        private void mtxtPhone_Validating(object sender, CancelEventArgs e)
        {
            if (can_validate)
            {
                if ((!mtxtPhone.MaskFull) && (mtxtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Phone number is invalid.");
                    mtxtPhone.Focus();
                    mtxtPhone.Text = sc.Phone;
                    return;
                }
            }
        }
        private void mtxtFax_Validating(object sender, CancelEventArgs e)
        {
            if (can_validate)
            {
                if ((!mtxtFax.MaskFull) && (mtxtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
                {
                    MessageBox.Show("Fax number is invalid.");
                    mtxtFax.Focus();
                    mtxtFax.Text = sc.Fax;
                    return;
                }
            }
        }
        //addr1 cannot be blank
        private void txtAddr1_Validating(object sender, CancelEventArgs e)
        {
            if (can_validate)
            {
                if (txtAddr1.Text.Trim() == "")
                {
                    DialogResult result = MessageBox.Show("Address is invalid.", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtAddr1.Focus();
                        txtAddr1.Text = sc.Addr1;
                        return;
                    }
                }
            }
        }
        private void txtAddr3_Validating(object sender, CancelEventArgs e)
        {
            if (can_validate)
            {
                if (!ValidateCityState())
                {
                    txtAddr3.Focus();
                    txtAddr3.Text = sc.Addr3;
                    return;
                }
            } 
        }

        private void txtZip_Validating(object sender, CancelEventArgs e)
        {
            if (can_validate)
            {
                ValidateCityState();

                //if canada and wrong format or us and wrong format
                if ((CheckAddress3IsCanada() == true) && (!GeneralData.IsCanadianZipCode(txtZip.Text.Trim())))
                {
                    DialogResult result = MessageBox.Show("Zip Code is invalid", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtZip.Focus();
                        txtZip.Text = sc.Zip.Trim();
                        return;
                    }
                }
                else if ((CheckAddress3IsCanada() == false) && (!GeneralData.IsUsZipCode(txtZip.Text.Trim())))
                {
                    DialogResult result = MessageBox.Show("Zip Code is invalid", "OK", MessageBoxButtons.OK);

                    if (result == DialogResult.OK)
                    {
                        txtZip.Focus();
                        txtZip.Text = sc.Zip.Trim();
                        return;
                    }
                }
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
        private bool ValidData()
        {
            //Validate addr1
            if (txtAddr1.Text.Trim() == "")
            {
                DialogResult result = MessageBox.Show("Address is invalid.", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtAddr1.Focus();
                    txtAddr1.Text = sc.Addr1;
                    return false;
                }
            }

            //Validate addr3
            if (!ValidateCityState())
            {
                txtAddr3.Focus();
                txtAddr3.Text = sc.Addr3;
                return false;
            }

            //Validate zip
            ValidateCityState();

            //if canada and wrong format or us and wrong format
            if ((CheckAddress3IsCanada() == true) && (!GeneralData.IsCanadianZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtZip.Focus();
                    txtZip.Text = sc.Zip.Trim();
                    return false;
                }
            }
            else if ((CheckAddress3IsCanada() == false) && (!GeneralData.IsUsZipCode(txtZip.Text.Trim())))
            {
                DialogResult result = MessageBox.Show("Zip Code is invalid", "OK", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    txtZip.Focus();
                    txtZip.Text = sc.Zip.Trim();
                    return false;
                }
            }

            //Validate Phone
            if ((!mtxtPhone.MaskFull) && (mtxtPhone.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Phone number is invalid.");
                mtxtPhone.Focus();
                mtxtPhone.Text = sc.Phone;
                return false;
            }

            //Validate fax
            if ((!mtxtFax.MaskFull) && (mtxtFax.Text.Trim(new Char[] { ' ', '(', ')', '-' }) != ""))
            {
                MessageBox.Show("Fax number is invalid.");
                mtxtFax.Focus();
                mtxtFax.Text = sc.Fax;
                return false;
            }

            return true;
        }

        //resume validation after disabled by refresh
        private void txtNewtc_Leave(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnablePreventFocusChange;
        }

        //resume validation after disabled by refresh
        private void txtAddr1_Leave(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnablePreventFocusChange;
        }

        //resume validation after disabled by refresh
        private void txtAddr3_Leave(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnablePreventFocusChange;
        }

        //resume validation after disabled by refresh
        private void txtZip_Leave(object sender, EventArgs e)
        {
            AutoValidate = AutoValidate.EnablePreventFocusChange;
        }
        private void frmNPNonResSpecialNA_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");

            /*unlock fin */
            if (editable)
            {
                updatelock = npsc.UpdateLock(fin, "");
            }

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        }
        //Verify Form closing from menu
        public override bool VerifyFormClosing()
        {
            can_close = true;

            CheckFormModified();

            if (formModified)
            {
                DialogResult result3 = MessageBox.Show("The data was changed, do you want to save it?", "Important Query", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result3 == DialogResult.Yes)
                {                  
                    if (ValidData())
                    {
                        SaveData();
                    }
                    else
                    {
                        can_close = false;
                    } 
                }
                else
                {
                    AutoValidate = AutoValidate.Disable;
                }
            }
            else
            {
                AutoValidate = AutoValidate.Disable;
            }

            return can_close;
        }

        private void txtAddr1_Enter(object sender, EventArgs e)
        {
            can_validate = true;
        }

        private void txtAddr3_Enter(object sender, EventArgs e)
        {
            can_validate = true;
        }

        private void txtZip_Enter(object sender, EventArgs e)
        {
            can_validate = true;
        }

        private void mtxtPhone_Enter(object sender, EventArgs e)
        {
            can_validate = true;
        }

        private void mtxtFax_Enter(object sender, EventArgs e)
        {
            can_validate = true;
        }

        private void btnRefresh_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnRefresh.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnRefresh.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }
    } 
}
