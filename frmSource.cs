/**************************************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmSource.cs	    	

Programmer:         Cestine Gill

Creation Date:      03/25/2015

Inputs:             CprsBLL.Searchinfo
                    Masteridlist

Parameters:		    None 

Outputs:		    None	

Description:	    This program displays the Source Address data for a selected Masterid
                    from various Search Screens

Detailed Design:    Detailed User Requirements for Source Address Display Screen 

Other:	            Called from: frmMaster.cs, frmC700Srch.cs, frmAdhoc.cs,
                    frmSample.cs, frmRespondent.cs, frmWhereSrch.cs
 
Revision History:	
**********************************************************************************************
 Modified Date :  9/30/2015
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  
 Description   :  create public properties arry and calling form
***********************************************************************************************
 Modified Date :  11/25/2015
 Modified By   :  Cestine Gill
 Keyword       :  
 Change Request:  
 Description   :  changed screen to display frame id number (fin)
**************************************************************************************************
 Modified Date :  05/24/2016
 Modified By   :  Cestine Gill
 Keyword       :  
 Change Request:  
 Description   :  changed screen use masterid in lieu of dodgenum
 *************************************************************************************************
 Modified Date :  08/17/2016
 Modified By   :  Srini Natarajan
 Keyword       :  
 Change Request:  
 Description   :  added a new tab for owner2
**************************************************************************************************
 Modified Date :  10/12/2016
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  
 Description   :  change tab style
**************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms.VisualStyles;

namespace Cprs
{
    public partial class frmSource : Cprs.frmCprsParent
    {
        /******public properties ********/
        /*Required */
        public int Masterid;
        public Form CallingForm = null;
        private string county;
        System.Windows.Forms.ToolTip ToolTip1 = new System.Windows.Forms.ToolTip();

        /*Optional */
        public List<int> Masteridlist = null;
        public int CurrIndex = 0;

        private int offsetY = 0;
        private int startY = 0;
        private int startXLabel = 0;
        private int startXText = 0;

        /*flag to use closing the calling form */
        private bool call_callingFrom = false;

        public frmSource()
        {
            InitializeComponent();
        }

        //obtain the Factor data from the CPRSBLL class library
        Factor factor;

        private void frmSource_Load(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
               GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "ENTER");
            }
            else
            {
                GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            }

            /*check required properties are set */
            if ((Masterid == 0) || (CallingForm == null))
            {
                MessageBox.Show("Masterid, CallingForm are required properties for showing Source form.");
                this.Dispose();
            }

            ResetFields();

            //Call in the GetFactor function based on the Masterid
            //assign to the factor class
            //used to load the textboxes on the screen

            factor = SourceData.GetFactor(Masterid);

            DisplayFactor();
            DisableTab();

            /*if there is Masteridlist, set count boxes */
            if (Masteridlist != null)
            {
                txtCurrentrec.Text = (CurrIndex + 1).ToString();
                txtTotalrec.Text = Masteridlist.Count.ToString();
            }
            else
            {
                txtCurrentrec.Text = "1";
                txtTotalrec.Text = "1";
            }

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }
            this.Close();
        }


        private void frmSource_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
               GeneralDataFuctions.AddCpraccessData("DATA ENTRY", "EXIT");
            }
            else
            {
                GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
            }

            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();

        }

        // Assign the factor data to its corresponding text property

        private void DisplayFactor()
        {
            txtFin.Text = factor.Fin;
            txtSource.Text = factor.Source;
            txtSeldate.Text = factor.Seldate;
            txtNewtc.Text = factor.Newtc;

            txtProjselv.Text = factor.Projselv.ToString();
            int Projselv = Convert.ToInt32(txtProjselv.Text);
            txtProjselv.Text = string.Format("{0:#,###}", Projselv);

            txtFwgt.Text = factor.Fwgt.ToString();

            txtState.Text = factor.State;

            txtCnty.Text = factor.Dodgecou;

            if (txtCnty.Text != "   ")
            {
                county = SourceData.GetCounty(txtState.Text, txtCnty.Text);
                ToolTip1.SetToolTip(txtCnty, county);
                ToolTip1.AutoPopDelay = 5000;
            }

            txtId.Text = factor.Id;
            txtOwner.Text = factor.Owner;

            if (factor.Owner == "M")
            {
                txtBldgs.Text = factor.Bldgs.ToString();
                txtUnits.Text = factor.Units.ToString("N0");
            }
            else
            {
                txtBldgs.Text = "";
                txtUnits.Text = "";
            }

            txtProjdesc.Text = factor.Projdesc;
            txtProjloc.Text  = factor.Projloc;
            txtPcityst.Text  = factor.Pcityst;
            txtF3addr1.Text  = factor.F3addr1;
            txtF3addr2.Text  = factor.F3addr2;
            txtF3addr3.Text  = factor.F3addr3;
            txtF3email.Text  = factor.F3email;
            txtF3phone.Text  = factor.F3phone.Trim();

            //Formats the telephone number when the field is not blank 

            if (!string.IsNullOrEmpty((txtF3phone.Text)))
            {
                txtF3phone.Text = Regex.Replace(txtF3phone.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }

            txtF3respname.Text = factor.F3respname;
            txtF3resporg.Text = factor.F3resporg;
            txtF3weburl.Text = factor.F3weburl;
            txtF3zip.Text = factor.F3zip;

            txtF4addr1.Text = factor.F4addr1;
            txtF4addr2.Text = factor.F4addr2;
            txtF4addr3.Text = factor.F4addr3;
            txtF4email.Text = factor.F4email;
            txtF4phone.Text = factor.F4phone.Trim();

            if (!string.IsNullOrEmpty((txtF4phone.Text)))
            {
                txtF4phone.Text = Regex.Replace(txtF4phone.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }

            txtF4respname.Text = factor.F4respname;
            txtF4resporg.Text = factor.F4resporg;
            txtF4weburl.Text = factor.F4weburl;
            txtF4zip.Text = factor.F4zip;

            txtF5addr1.Text = factor.F5addr1;
            txtF5addr2.Text = factor.F5addr2;
            txtF5addr3.Text = factor.F5addr3;
            txtF5email.Text = factor.F5email;
            txtF5phone.Text = factor.F5phone.Trim();

            if (!string.IsNullOrEmpty((txtF5phone.Text)))
            {
                txtF5phone.Text = Regex.Replace(txtF5phone.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }

            txtF5respname.Text = factor.F5respname;
            txtF5resporg.Text = factor.F5resporg;
            txtF5weburl.Text = factor.F5weburl;
            txtF5zip.Text = factor.F5zip;

            txtF7addr1.Text = factor.F7addr1;
            txtF7addr2.Text = factor.F7addr2;
            txtF7addr3.Text = factor.F7addr3;
            txtF7email.Text = factor.F7email;
            txtF7phone.Text = factor.F7phone.Trim();

            if (!string.IsNullOrEmpty((txtF7phone.Text)))
            {
                txtF7phone.Text = Regex.Replace(txtF7phone.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }

            txtF7respname.Text = factor.F7respname;
            txtF7resporg.Text = factor.F7resporg;
            txtF7weburl.Text = factor.F7weburl;
            txtF7zip.Text = factor.F7zip;

            txtF9addr1.Text = factor.F9addr1;
            txtF9addr2.Text = factor.F9addr2;
            txtF9addr3.Text = factor.F9addr3;
            txtF9email.Text = factor.F9email;
            txtF9phone.Text = factor.F9phone.Trim();

            if (!string.IsNullOrEmpty((txtF9phone.Text)))
            {
                txtF9phone.Text = Regex.Replace(txtF9phone.Text, @"(\d{3})(\d{3})(\d{4})", "($1) $2-$3");
            }

            txtF9respname.Text = factor.F9respname;
            txtF9resporg.Text = factor.F9resporg;
            txtF9weburl.Text = factor.F9weburl;
            txtF9zip.Text = factor.F9zip;
        }

        private void tbFactors_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbFactors.Refresh();
        }

        //Prevents selection of the tab if it is disabled
        private void tbFactors_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        //Disable the tab if there in no data output within it
        private void DisableTab()
        {
            tbOwner.Enabled = true;
            tbArchitect.Enabled = true;
            tbEngineer.Enabled = true;
            tbContractor.Enabled = true;
            tbOwner2.Enabled = true;

            if
           (string.IsNullOrEmpty(txtF3resporg.Text) || txtF3resporg.Text == " " || txtF3resporg.TextLength <= 1)
            {
                tbOwner.Enabled = false;
            }

            if

          (string.IsNullOrEmpty(txtF4resporg.Text) || txtF4resporg.Text == " " || txtF4resporg.TextLength <= 1)
            {
                tbArchitect.Enabled = false;
            }

            if

          (string.IsNullOrEmpty(txtF5resporg.Text) || txtF5resporg.Text == " " || txtF5resporg.TextLength <= 1)
            {
                tbEngineer.Enabled = false;
            }

            if
          (string.IsNullOrEmpty(txtF7resporg.Text) || txtF7resporg.Text == " " || txtF7resporg.TextLength <= 1)
            {
                tbContractor.Enabled = false;
            }

            if
          (string.IsNullOrEmpty(txtF9resporg.Text) || txtF9resporg.Text == " " || txtF9resporg.TextLength <= 1)
            {
                tbOwner2.Enabled = false;
            }

        }


        //Removes data from the Text property

        private void ResetFields()
        {
            txtFin.Text = "";
            txtSource.Text = "";
            txtSeldate.Text = "";

            txtNewtc.Text = "";
            txtProjselv.Text = "";
            txtFwgt.Text = "";
            txtState.Text = "";
            txtCnty.Text = "";

            txtBldgs.Text = "";
            txtUnits.Text = "";
            txtId.Text = "";
            txtOwner.Text = "";
            txtProjdesc.Text = "";
            txtProjloc.Text = "";
            txtPcityst.Text = "";

            txtF3addr1.Text = "";
            txtF3addr2.Text = "";
            txtF3addr3.Text = "";
            txtF3email.Text = "";
            txtF3phone.Text = "";
            txtF3respname.Text = "";
            txtF3resporg.Text = "";
            txtF3weburl.Text = "";
            txtF3zip.Text = "";

            txtF4addr1.Text = "";
            txtF4addr2.Text = "";
            txtF4addr3.Text = "";
            txtF4email.Text = "";
            txtF4phone.Text = "";
            txtF4respname.Text = "";
            txtF4resporg.Text = "";
            txtF4weburl.Text = "";
            txtF4zip.Text = "";

            txtF5addr1.Text = "";
            txtF5addr2.Text = "";
            txtF5addr3.Text = "";
            txtF5email.Text = "";
            txtF5phone.Text = "";
            txtF5respname.Text = "";
            txtF5resporg.Text = "";
            txtF5weburl.Text = "";
            txtF5zip.Text = "";

            txtF7addr1.Text = "";
            txtF7addr2.Text = "";
            txtF7addr3.Text = "";
            txtF7email.Text = "";
            txtF7phone.Text = "";
            txtF7respname.Text = "";
            txtF7resporg.Text = "";
            txtF7weburl.Text = "";
            txtF7zip.Text = "";

            ToolTip1.RemoveAll();

        }

        //allows user to click through Masteridlist to the previous record
        //that is passed from the search screen.
        //checks the current position with the total number of records

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrIndex == 0)
            {
                MessageBox.Show("You are at the first observation.");
            }
            else
            {
                CurrIndex = CurrIndex - 1;
                Masterid = Masteridlist[CurrIndex];

                txtCurrentrec.Text = (CurrIndex + 1).ToString();

                factor = SourceData.GetFactor(Masterid);

                DisplayFactor();

                tbFactors.SelectedTab = tbOwner;

                DisableTab();

                tbFactors.Refresh();
            }
        }

        //allows user to click through Masteridlist to the next record
        //that is passed from the search screen.
        //checks the current position with the total number of records

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrIndex == Masteridlist.Count - 1)
            {
                MessageBox.Show("You are at the last observation.");
            }
            else
            {
                CurrIndex = CurrIndex + 1;
                Masterid = Masteridlist[CurrIndex];
                txtCurrentrec.Text = (CurrIndex + 1).ToString();

                factor = SourceData.GetFactor(Masterid);

                DisplayFactor();

                tbFactors.SelectedTab = tbOwner;

                DisableTab();

                tbFactors.Refresh();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1.PrinterSettings.PrinterName = UserInfo.PrinterQ;
            printDocument1.DocumentName = "Source Address Display Print";
            Margins margins = new Margins(100, 100, 100, 100);
            printDocument1.DefaultPageSettings.Margins = margins;

            printDocument1.Print();         
    
        }

        private void printDocument1_BeginPrint(object sender, PrintEventArgs e)
        {
            offsetY = 25;
            startY = 35;
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {

            FontFamily fontFamily = new FontFamily("Times New Roman");

            Font font = new Font(fontFamily, 16, FontStyle.Regular, GraphicsUnit.Pixel);
            PointF pointF1 = new PointF(10, 10);
            SolidBrush solidBrush = new SolidBrush(Color.Black);

            Graphics graphics = e.Graphics;

            System.Drawing.Font fntHeaderString = new Font("Times New Roman", 16, FontStyle.Bold);
            System.Drawing.Font fntLabelString = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            System.Drawing.Font fntTxtString = new System.Drawing.Font("Times New Roman", 10, FontStyle.Regular);

            string UserName = UserInfo.UserName ;
            String StrDate  = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();
            String PageName = "Page 1";

            float fontHeight = font.GetHeight();

            graphics.DrawString(UserName, fntLabelString, solidBrush, 5, 10);
            graphics.DrawString(PageName, fntLabelString, solidBrush, 400, 10);
            graphics.DrawString(StrDate, fntLabelString, solidBrush, 580, 10);

            graphics.DrawString("SOURCE ADDRESS DISPLAY", fntHeaderString, solidBrush, 250, startY);

            // Move down one line.
            offsetY += (int)fontHeight;

            graphics.DrawString("FRAME ID", fntLabelString, solidBrush, 5, startY + offsetY);
            graphics.DrawString("SRC", fntLabelString, solidBrush, 100, startY + offsetY);
            graphics.DrawString("DATE", fntLabelString, solidBrush, 200, startY + offsetY);

            graphics.DrawString("NEWTC ", fntLabelString, solidBrush, 300, startY + offsetY);
            graphics.DrawString("VALUE", fntLabelString, solidBrush, 400, startY + offsetY);
            graphics.DrawString("FWGT", fntLabelString, solidBrush, 500, startY + offsetY);

            offsetY += (int)fontHeight;

            graphics.DrawString(txtFin.Text, fntTxtString, solidBrush, 5, startY + offsetY);
            graphics.DrawString(txtSource.Text, fntTxtString, solidBrush, 100, startY + offsetY);
            graphics.DrawString(txtSeldate.Text, fntTxtString, solidBrush, 200, startY + offsetY);

            graphics.DrawString(txtNewtc.Text, fntTxtString, solidBrush, 300, startY + offsetY);
            graphics.DrawString(txtProjselv.Text, fntTxtString, solidBrush, 400, startY + offsetY);
            graphics.DrawString(txtFwgt.Text, fntTxtString, solidBrush, 500, startY + offsetY);

            // Move down two lines.
            offsetY += 2 * (int)fontHeight;

            graphics.DrawString("FIPS ST", fntLabelString, solidBrush, 5, startY + offsetY);
            graphics.DrawString("CNTY", fntLabelString, solidBrush, 100, startY + offsetY);

            graphics.DrawString("BLDGS", fntLabelString, solidBrush, 200, startY + offsetY);
            graphics.DrawString("UNITS", fntLabelString, solidBrush, 300, startY + offsetY);
            graphics.DrawString("OWNER", fntLabelString, solidBrush, 400, startY + offsetY);
            graphics.DrawString("ID", fntLabelString, solidBrush, 500, startY + offsetY);
            
            offsetY += (int)fontHeight;

            graphics.DrawString(txtState.Text, fntTxtString, solidBrush, 5, startY + offsetY);
            graphics.DrawString(txtCnty.Text, fntTxtString, solidBrush, 100, startY + offsetY);

            graphics.DrawString(txtBldgs.Text, fntTxtString, solidBrush, 200, startY + offsetY);
            graphics.DrawString(txtUnits.Text, fntTxtString, solidBrush, 300, startY + offsetY);
            graphics.DrawString(txtOwner.Text, fntTxtString, solidBrush, 400, startY + offsetY);
            graphics.DrawString(txtId.Text, fntTxtString, solidBrush, 500, startY + offsetY);
            
            offsetY += (int)fontHeight;

            string txtProjdesc1 = GeneralFunctions.Wrap(txtProjdesc.Text, 60);
            string txtProjloc1 = GeneralFunctions.Wrap(txtProjloc.Text, 60);

            // Move down one line.
            offsetY += (int)fontHeight;
            graphics.DrawString("PROJECT DESCRIPTION", fntLabelString, solidBrush, 5, startY + offsetY);
            graphics.DrawString(txtProjdesc1, fntTxtString, solidBrush, 200, startY + offsetY);
            offsetY += (int)fontHeight + 4;
            graphics.DrawString("PROJECT LOCATION", fntLabelString, solidBrush, 5, startY + offsetY);
            graphics.DrawString(txtProjloc1, fntTxtString, solidBrush, 200, startY + offsetY);
            offsetY += (int)fontHeight + 4;
            graphics.DrawString("PROJECT CITY/STATE", fntLabelString, solidBrush, 5, startY + offsetY);
            graphics.DrawString(txtPcityst.Text, fntTxtString, solidBrush, 200, startY + offsetY);

            // Move down one line.
            offsetY += 2 * font.Height;
            startXLabel = 20;
            startXText = 180;

            if (tbOwner.Enabled)
            {
                graphics.DrawString("OWNER", fntLabelString, solidBrush, 5, startY + offsetY);
                // Move down one line.
                offsetY += font.Height;
                graphics.DrawString("ORGANIZATION:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3resporg.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CONTACT:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3respname.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 1:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3addr1.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 2:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3addr2.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CITY/STATE:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3addr3.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                graphics.DrawString("ZIP CODE:", fntLabelString, solidBrush, 385, startY + offsetY);
                graphics.DrawString(txtF3zip.Text, fntTxtString, solidBrush, 460, startY + offsetY);
                graphics.DrawString("PHONE:", fntLabelString, solidBrush, 540, startY + offsetY); //startXLabel, startY + offsetY);
                graphics.DrawString(txtF3phone.Text, fntTxtString, solidBrush, 600, startY + offsetY); //startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("EMAIL:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3email.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("WEB:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF3weburl.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight + 5;
            }

            if (tbArchitect.Enabled)
            {
                graphics.DrawString("ARCHITECT", fntLabelString, solidBrush, 5, startY + offsetY);
                offsetY += font.Height;
                graphics.DrawString("ORGANIZATION:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4resporg.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CONTACT:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4respname.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 1:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4addr1.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 2:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4addr2.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CITY/STATE:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4addr3.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                graphics.DrawString("ZIP CODE:", fntLabelString, solidBrush, 385, startY + offsetY);
                graphics.DrawString(txtF4zip.Text, fntTxtString, solidBrush, 460, startY + offsetY);
                graphics.DrawString("PHONE:", fntLabelString, solidBrush, 540, startY + offsetY);
                graphics.DrawString(txtF4phone.Text, fntTxtString, solidBrush, 600, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("EMAIL:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4email.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("WEB:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF4weburl.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight + 5;
            }

            if (tbEngineer.Enabled)
            {
                graphics.DrawString("ENGINEER", fntLabelString, solidBrush, 5, startY + offsetY);
                offsetY += font.Height;
                graphics.DrawString("ORGANIZATION:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5resporg.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CONTACT:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5respname.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 1:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5addr1.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 2:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5addr2.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CITY/STATE:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5addr3.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                graphics.DrawString("ZIP CODE:", fntLabelString, solidBrush, 385, startY + offsetY);
                graphics.DrawString(txtF5zip.Text, fntTxtString, solidBrush, 460, startY + offsetY);
                graphics.DrawString("PHONE:", fntLabelString, solidBrush, 540, startY + offsetY);
                graphics.DrawString(txtF5phone.Text, fntTxtString, solidBrush, 600, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("EMAIL:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5email.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("WEB:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF5weburl.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight + 5;
            }

            if (tbContractor.Enabled)
            {
                graphics.DrawString("CONTRACTOR", fntLabelString, solidBrush, 5, startY + offsetY);
                offsetY += font.Height;
                graphics.DrawString("ORGANIZATION:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7resporg.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CONTACT:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7respname.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 1:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7addr1.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 2:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7addr2.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CITY/STATE:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7addr3.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                graphics.DrawString("ZIP CODE:", fntLabelString, solidBrush, 385, startY + offsetY);
                graphics.DrawString(txtF7zip.Text, fntTxtString, solidBrush, 460, startY + offsetY);
                graphics.DrawString("PHONE:", fntLabelString, solidBrush, 540, startY + offsetY);
                graphics.DrawString(txtF7phone.Text, fntTxtString, solidBrush, 600, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("EMAIL:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7email.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("WEB:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF7weburl.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight + 5;
            }

            if (tbOwner2.Enabled)
            {
                graphics.DrawString("OWNER2", fntLabelString, solidBrush, 5, startY + offsetY);
                offsetY += font.Height;
                graphics.DrawString("ORGANIZATION:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9resporg.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CONTACT:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9respname.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 1:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9addr1.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("ADDRESS 2:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9addr2.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("CITY/STATE:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9addr3.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                graphics.DrawString("ZIP CODE:", fntLabelString, solidBrush, 385, startY + offsetY);
                graphics.DrawString(txtF9zip.Text, fntTxtString, solidBrush, 460, startY + offsetY);
                graphics.DrawString("PHONE:", fntLabelString, solidBrush, 540, startY + offsetY);
                graphics.DrawString(txtF9phone.Text, fntTxtString, solidBrush, 600, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("EMAIL:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9email.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
                graphics.DrawString("WEB:", fntLabelString, solidBrush, startXLabel, startY + offsetY);
                graphics.DrawString(txtF9weburl.Text, fntTxtString, solidBrush, startXText, startY + offsetY);
                offsetY += (int)fontHeight;
            }
        
        }
    }
}
