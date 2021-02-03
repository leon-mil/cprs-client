/**********************************************************************************
Econ App Name  : CPRS
Project Name   : CPRS Interactive Screens System
Program Name   : frmMFComparePopup      	    	
Programmer     : Diane Musachio        
Creation Date  : May 19, 2016    
Inputs         : MasterId
                 MasterIdList
                 Callingform
                 CurrIndex            
Parameters     : none		                  
Outputs        : none 		   

Description    : A popup screen from MFInitial screen that displays
                 data for current Presample case and data for a sample case 
                 selected from the Search Results table

Detailed Design: Multi-family Initial Address Design   

Other          : called by: frmMFInital.cs	          
 
Rev History    : See Below	
***********************************************************************************
Modified Date  :  November 11, 2020
Modified By    :  Diane Musachio
Keyword        :  DM20201118
Change Request :  
Description    :  on comment button if respid = '' then make respid = id
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
    public partial class frmMfComparePopup : Form
    {
        /* Public */
        public Form CallingForm = null;
        public frmMfComparePopup Current;
        public List<int> Masteridnumlist = null;
        public int CurrIndex = 0;
        public int Masterid;//Variable used when the form is called from the MF Initial Screen
        public int Dupmaster;
        public bool compareflg = true;
        public string Id;

        public string fin1;
        public string seldate1;
        public string fipst1;
        public string status1;
        public string frcde1;
        public string newtc1;
        public string respid1;
        public string obldgs1;
        public string ounits1;
        public string rbldgs1;
        public string runits1;
        public string projdesc1;
        public string projloc1;
        public string pcityst1;
        public string resporg1;
        public string factoff1;
        public string othrresp1;
        public string addr1_1;
        public string addr2_1;
        public string addr3_1;
        public string zip1;
        public string respname1_1;
        public string phone1_1;
        public string ext1_1;
        public string respname2_1;
        public string phone2_1;
        public string ext2_1;
        public string strtdate1;
        public bool isSample;

        //variable used to disable the duplicate 
        //button if the case is locked
        public bool flgIsLocked;
        
        MfCompare comp = new MfCompare();
        MfCompareData compdata = new MfCompareData();

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmMfComparePopup()
        {
            InitializeComponent();
        }
        
        private void frmMfComparePopup_Load(object sender, EventArgs e)
        {
           
            DisplayForm();

            Masterid = Masteridnumlist[CurrIndex];
            
            LoadForm();

            /* If there is a list, set count boxes */
            if (Masteridnumlist != null)
            {
                txtCurrentrec.Text = (CurrIndex + 1).ToString();
                txtTotalrec.Text = Masteridnumlist.Count.ToString();
            }

        }

        //Displays current (original) presample case
        private void DisplayForm()
        {
            txtFin1.Text = fin1;
            txtSelDate1.Text = seldate1;
            txtFipst1.Text = fipst1;
            txtStatus1.Text = status1;
            txtFrCde1.Text = frcde1;
            txtNewtc1.Text = newtc1;
            txtRespid1.Text = respid1;
            txtOBldgs1.Text = obldgs1;
            txtOUnits1.Text = ounits1;
            txtRBldgs1.Text = rbldgs1;
            txtRUnits1.Text = runits1;
            txtProjDesc1.Text = projdesc1;
            txtProjLoc1.Text = projloc1;
            txtPCitySt1.Text = pcityst1;
            txtRespOrg1.Text = resporg1;
            txtFactOff1.Text = factoff1;
            txtOthrResp1.Text = othrresp1;
            txtAddr1_1.Text = addr1_1;
            txtAddr2_1.Text = addr2_1;
            txtAddr3_1.Text = addr3_1;
            txtZip1.Text = zip1;
            txtRespname1_1.Text = respname1_1;
            txtPhone_1.Text = phone1_1;
            txtExt_1.Text = ext1_1;
            txtRespname2_1.Text = respname2_1;
            txtPhone2_1.Text = phone2_1;
            txtExt2_1.Text = ext2_1;
            txtStrtdate1.Text = strtdate1;
            txtMaster.Text = Id;

            if (flgIsLocked == true)
                btnDuplicate.Enabled = false;
            if (flgIsLocked == false)
                btnDuplicate.Enabled = true;
            
        }

        private string fipstate2;
        private string status;

        //Displays sample case (Matched) selected from Search Results table
        private void LoadForm()
        {
            comp = compdata.GetCompareData(Masterid, isSample);

            txtFin2.Text = comp.Psu + " " + comp.Bpoid + " " + comp.Sched;

            //convert fipstate numeric code to alphabetical abbreviation
            fipstate2 = GeneralDataFuctions.GetFipState(comp.Fipstate);

            txtFipst2.Text = fipstate2;

            //display status number and definition
            if (comp.Status == "1")
            {
                status = "1-Active";
            }
            else if (comp.Status == "2")
            {
                status = "2-PNR";
            } 
            else if (comp.Status == "3")
            {
                status = "3-DC PNR";
            } 
            else if (comp.Status == "4")
            {
                status = "4-Abeyance";
            } 
            else if (comp.Status == "5")
            {
                status = "5-Duplicate";
            }
            else if (comp.Status == "6")
            {
                status = "6-Out of Scope";
            } 
            else if (comp.Status == "7")
            {
                status = "7-DC Refusal";
            } 
            else if (comp.Status == "8")
            {
                status = "8-Refusal";
            }

            txtStatus2.Text = status;
            txtSeldate2.Text = comp.Seldate;
            txtFrCde2.Text = comp.Frcde;
            txtNewtc2.Text = comp.Newtc;
            txtRespid2.Text = comp.Respid;
            txtOBldgs2.Text = comp.Bldgs;
            txtOUnits2.Text = comp.Units;
            txtRBldgs2.Text = comp.Rbldgs;
            txtRUnits2.Text = comp.Runits;
            txtProjDesc2.Text = comp.ProjDesc;
            txtProjLoc2.Text = comp.Projloc;
            txtPcitySt2.Text = comp.PCitySt;
            txtRespOrg2.Text = comp.Resporg;
            txtFactOff2.Text = comp.Factoff;
            txtOthrResp2.Text = comp.Othrresp;
            txtAddr1_2.Text = comp.Addr1;
            txtAddr2_2.Text = comp.Addr2;
            txtAddr3_2.Text = comp.Addr3;
            txtZip2.Text = comp.Zip;
            txtRespname1_2.Text = comp.Respname;
            txtPhone1_2.Text = comp.Phone;
            txtExt1_2.Text = comp.Ext;
            txtRespname2_2.Text = comp.Respname2;
            txtPhone2_2.Text = comp.Phone2;
            txtExt2_2.Text = comp.Ext2;
            txtStrtdate2.Text = comp.Strtdate;
            txtId.Text = comp.Id;           
        }

        //this controls the color of text on the contacts tab
        //if primary or secondary is blank the tab text will be black
        //otherwise the tabs are blue
        private void tbContact_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == 0)
            {
                if (txtRespname1_1.Text == "")
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
                         Brushes.DarkBlue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
            if (e.Index == 1)
            {
                if (txtRespname2_1.Text == "")
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
                         Brushes.DarkBlue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
        }

        //this controls the color of text on the contacts tab
        //if primary or secondary is blank the tab text will be black
        //otherwise the tabs are blue
        private void tbContact2_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index == 0)
            {
                if (txtRespname1_2.Text == "")
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
                         Brushes.DarkBlue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
            if (e.Index == 1)
            {
                if (txtRespname2_2.Text == "")
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
                         Brushes.DarkBlue,
                         new PointF(e.Bounds.X + 3, e.Bounds.Y + 3));
                }
            }
        }

        //Allows user to go back one case in the search list
        private void btnPrevCase_Click(object sender, EventArgs e)
        {
            if (CurrIndex == 0)
            {
                MessageBox.Show("You are at the first observation");
            }
            else
            {
                CurrIndex = CurrIndex - 1;
                Masterid = Masteridnumlist[CurrIndex];
                txtCurrentrec.Text = (CurrIndex + 1).ToString();
                LoadForm();
            }
        }

        //Identifies the current case Parent and returns to the Initial Address Screen
        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;           
            Dupmaster = Masterid;        
            this.Close();   // close compare
        }

        //Allows user to view comments for the selected sample case
        private void btnComments_Click(object sender, EventArgs e)
        {
            frmHistory fHistory = new frmHistory();
            string respid2 = null;

            fHistory.Id = txtId.Text;

            //DM20201118 if respid2 = blank then set respid2 = id for comments
            if (txtRespid2.Text.Trim() == "")
            {
                respid2 = txtId.Text;
            }
            else respid2 = txtRespid2.Text;

            fHistory.Respid = respid2;
            fHistory.Resporg = txtRespOrg2.Text;
            fHistory.Respname = txtRespname1_2.Text;
           
            fHistory.ShowDialog();  //show history screen
        }

        Bitmap memoryImage;

        //Prints the current display to the default printer
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

        //Allows user to go forward one case in the search list
        private void btnNextCase_Click(object sender, EventArgs e)
        {
            if (CurrIndex == Masteridnumlist.Count - 1)
            {
                MessageBox.Show("You are at the last observation");
            }
            else
            {
                CurrIndex = CurrIndex + 1;
                Masterid = Masteridnumlist[CurrIndex];
                txtCurrentrec.Text = (CurrIndex + 1).ToString();
                LoadForm();
            }
        }

        //Close the popup screen and return to MFInitial screen
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
            }
            this.Close();
        }
    }
}

