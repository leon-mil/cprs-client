/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmSlipDisplay.cs

 Programmer    : Diane Musachio

 Creation Date : 3/25/2015

 Inputs        : Id and Fin from Name and Address , Data from dcpnotes, dcpslips data tables

 Paramaters    : Id and Fin
 
 Output        : Windows form screen that displays Dodge Slip information and data from
                 Dcpslips and Dcpnotes
 
 Description   : This program captures a current ID from the Name and Address Screen in the CPRS interactive screen and 
                 displays the data from dcpnotes and dcpslips on a form that the analyst can open and print.

 Detail Design : Detailed User Requirements for Slip Display Screen

 Forms Using this code: n/a
 
 Called by     : n/a

 Other         : 

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using System.IO;
using CprsBLL;
using CprsDAL;
using System.Threading.Tasks;
using System.Configuration;

namespace Cprs
{
    public partial class frmSlipDisplay : Form
    {
        public string Id;
        public string Dodgenum;
        public string Fin;

        //These lists hold the data to be viewed in datagrid views

        public List<DodgeNotes.Title> TitleList;

        public List<DodgeNotes.Value> ValueList;

        public List<DodgeNotes.Add> AddList;

        public List<DodgeNotes.Item> ItemList;

        public List<DodgeNotes.Reporter> ReporterList;

        DodgeSlipData dsd = new DodgeSlipData();

        public static frmSlipDisplay Current;

        private PrintPreviewDialog printPreviewDialog1 = new PrintPreviewDialog();

        public frmSlipDisplay()
        {
            InitializeComponent();

            tbPtype.DrawMode = TabDrawMode.OwnerDrawFixed;

            Current = this;
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCP = base.CreateParams;
                myCP.ClassStyle = myCP.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCP;
            }
        }


        Slips slip;

        private void frmSlipDisplay_Load(object sender, EventArgs e)
        {

            ResetParameters();

            slip = dsd.GetSlips(Id);

            DisplaySlip();

            DisableTab();

            //obtains title data and assigns it as datasource to title data grid

            TitleList = dsd.Gettitle(slip.Masterid);

            dgTLE.DataSource = TitleList;
            dgTLE.Columns[0].Width = 1130;


            //obtains value data and assigns it as datasource to value data grid

            ValueList = dsd.Getvalue(slip.Masterid);

            dgVAL.DataSource = ValueList;
            dgVAL.Columns[0].Width = 1130;


            //obtains additional information data and assigns it as datasource to add data grid

            AddList = dsd.Getadd(slip.Masterid);

            dgADD.DataSource = AddList;
            dgADD.Columns[0].Width = 1150;


            //obtains item data and assigns it as datasource to item data grid

            ItemList = dsd.Getitem(slip.Masterid);

            dgITM.DataSource = ItemList;
            dgITM.Columns[0].Width = 1150;


            //obtains reporter data and assigns it as datasource to reporter data grid

            ReporterList = dsd.Getreporter(slip.Masterid);

            dgREP.DataSource = ReporterList;
            dgREP.Columns[0].Width = 1150;

        }

        //If tab is selected refresh data shown

        private void tbPtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbPtype.Refresh();
        }

        //If tab is selected and not enabled then cancel

        private void tbPtype_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (!e.TabPage.Enabled)
            {
                e.Cancel = true;
            }
        }

        //Blank out parameters

        private void ResetParameters()
        {
            //txtDodgenum.Text = "";
            txtFin.Text = "";
            txtId.Text = "";
            txtProjTitle.Text = "";
            txtProjValue.Text = "";
            txtAddr1.Text = "";
            txtAddr2.Text = "";
            txtCity.Text = "";
            txtCounty.Text = "";
            txtState.Text = "";
            txtZip.Text = "";
            txtTargetStartd.Text = "";
            txtTargetCompd.Text = "";
            txtTypeofWork.Text = "";
            txtContrMethod.Text = "";
            txtContr1.Text = "";
            txtContr2.Text = "";
            txtFactorCode.Text = "";
            txtOwnerClass.Text = "";
            txtNumStoriesAbove.Text = "";
            txtNumberStoriesBelow.Text = "";
            txtTotSquare.Text = "";
            txtNumBldgs.Text = "";
            txtProjStatus.Text = "";
            txtProjGroup.Text = "";
            txtPPtype.Text = "";
            txtPtype1.Text = "";
            txtPtype2.Text = "";
            txtPtype3.Text = "";
            txtPtype4.Text = "";
            txtPtype5.Text = "";
            txtPtype6.Text = "";
            txtPtype7.Text = "";
            txtPtype8.Text = "";
            txtPtype9.Text = "";
            txtPtype10.Text = "";
            txtPtype11.Text = "";
            txtPtype12.Text = "";
            txtPtype13.Text = "";
            txtPtype14.Text = "";
            txtPtype15.Text = "";
            txtPtype16.Text = "";
            txtPtype17.Text = "";
            txtPtype18.Text = "";
            txtPtype19.Text = "";
            txtPtype20.Text = "";
        }

        //Assign text boxes on form to data coming in from database

        private void DisplaySlip()
        {
            //txtDodgenum.Text = Dodgenum;
            txtFin.Text = Fin;
            txtId.Text = Id;
            txtProjTitle.Text = slip.Title;
            txtProjValue.Text = slip.Valuation;
            txtAddr1.Text = slip.Taddr1;
            txtAddr2.Text = slip.Taddr2;
            txtCity.Text = slip.Tcity;
            txtCounty.Text = slip.Tcounty;
            txtState.Text = slip.Tstate;
            txtZip.Text = slip.Tzip;
            txtTargetStartd.Text = slip.Tstrtdate;
            txtTargetCompd.Text = slip.Tcompdate;
            txtTypeofWork.Text = slip.Worktype;
            txtContrMethod.Text = slip.Contmeth;
            txtContr1.Text = slip.Contnbr1;
            txtContr2.Text = slip.Contnbr2;
            txtFactorCode.Text = slip.Fcode;
            txtOwnerClass.Text = slip.Ownclass;
            txtNumStoriesAbove.Text = slip.Storyabv;
            txtNumberStoriesBelow.Text = slip.Storybel;
            txtTotSquare.Text = slip.Tsqrarea;
            txtNumBldgs.Text = slip.Numbldgs;
            txtProjStatus.Text = slip.Projstat;
            txtProjGroup.Text = slip.Projgroup;
            txtPPtype.Text = slip.Pptype;
            txtPtype1.Text = slip.Ptype1;
            txtPtype2.Text = slip.Ptype2;
            txtPtype3.Text = slip.Ptype3;
            txtPtype4.Text = slip.Ptype4;
            txtPtype5.Text = slip.Ptype5;
            txtPtype6.Text = slip.Ptype6;
            txtPtype7.Text = slip.Ptype7;
            txtPtype8.Text = slip.Ptype8;
            txtPtype9.Text = slip.Ptype9;
            txtPtype10.Text = slip.Ptype10;
            txtPtype11.Text = slip.Ptype11;
            txtPtype12.Text = slip.Ptype12;
            txtPtype13.Text = slip.Ptype13;
            txtPtype14.Text = slip.Ptype14;
            txtPtype15.Text = slip.Ptype15;
            txtPtype16.Text = slip.Ptype16;
            txtPtype17.Text = slip.Ptype17;
            txtPtype18.Text = slip.Ptype18;
            txtPtype19.Text = slip.Ptype19;
            txtPtype20.Text = slip.Ptype20;
        }

        //Set tabs to true or false depending on if the text field is populated

        private void DisableTab()
        {
            txtPtype1.Enabled = true;
            txtPtype2.Enabled = true;
            txtPtype3.Enabled = true;
            txtPtype4.Enabled = true;
            txtPtype5.Enabled = true;
            txtPtype6.Enabled = true;
            txtPtype7.Enabled = true;
            txtPtype8.Enabled = true;
            txtPtype9.Enabled = true;
            txtPtype10.Enabled = true;
            txtPtype11.Enabled = true;
            txtPtype12.Enabled = true;
            txtPtype13.Enabled = true;
            txtPtype14.Enabled = true;
            txtPtype15.Enabled = true;
            txtPtype16.Enabled = true;
            txtPtype17.Enabled = true;
            txtPtype18.Enabled = true;
            txtPtype19.Enabled = true;
            txtPtype20.Enabled = true;

            if (string.IsNullOrEmpty(txtPtype1.Text) || txtPtype1.Text == " " || txtPtype1.TextLength <= 1)
            {
                tbPtype1.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype2.Text) || txtPtype2.Text == " " || txtPtype2.TextLength <= 1)
            {
                tbPtype2.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype3.Text) || txtPtype3.Text == " " || txtPtype3.TextLength <= 1)
            {
                tbPtype3.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype4.Text) || txtPtype4.Text == " " || txtPtype4.TextLength <= 1)
            {
                tbPtype4.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype5.Text) || txtPtype5.Text == " " || txtPtype5.TextLength <= 1)
            {
                tbPtype5.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype6.Text) || txtPtype6.Text == " " || txtPtype6.TextLength <= 1)
            {
                tbPtype6.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype7.Text) || txtPtype7.Text == " " || txtPtype7.TextLength <= 1)
            {
                tbPtype7.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype8.Text) || txtPtype8.Text == " " || txtPtype8.TextLength <= 1)
            {
                tbPtype8.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype9.Text) || txtPtype9.Text == " " || txtPtype9.TextLength <= 1)
            {
                tbPtype9.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype10.Text) || txtPtype10.Text == " " || txtPtype10.TextLength <= 1)
            {
                tbPtype10.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype11.Text) || txtPtype11.Text == " " || txtPtype11.TextLength <= 1)
            {
                tbPtype11.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype12.Text) || txtPtype12.Text == " " || txtPtype12.TextLength <= 1)
            {
                tbPtype12.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype13.Text) || txtPtype13.Text == " " || txtPtype13.TextLength <= 1)
            {
                tbPtype13.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype14.Text) || txtPtype14.Text == " " || txtPtype14.TextLength <= 1)
            {
                tbPtype14.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype15.Text) || txtPtype15.Text == " " || txtPtype15.TextLength <= 1)
            {
                tbPtype15.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype16.Text) || txtPtype16.Text == " " || txtPtype16.TextLength <= 1)
            {
                tbPtype16.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype17.Text) || txtPtype17.Text == " " || txtPtype17.TextLength <= 1)
            {
                tbPtype17.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype18.Text) || txtPtype18.Text == " " || txtPtype18.TextLength <= 1)
            {
                tbPtype18.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype19.Text) || txtPtype19.Text == " " || txtPtype19.TextLength <= 1)
            {
                tbPtype19.Enabled = false;
            }

            if (string.IsNullOrEmpty(txtPtype20.Text) || txtPtype20.Text == " " || txtPtype20.TextLength <= 1)
            {
                tbPtype20.Enabled = false;
            }

        }

        //Remove tabs that have no data

        private void tbPtype_DrawItem(object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            for (int i = tbPtype.TabPages.Count - 1; i >= 0; i--)
            {
                TabPage tabPage = tbPtype.TabPages[i];

                Color foreColor = SystemColors.WindowText;

                if (tabPage.Enabled ^= false)
                {
                    Rectangle tabRectangle = tbPtype.GetTabRect(i);
                    TextRenderer.DrawText(e.Graphics, tabPage.Text, this.tbPtype.Font, tabRectangle, foreColor);
                }
                else
                {
                    tbPtype.TabPages.RemoveAt(i);
                }
            }
        }


        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument1.PrinterSettings.PrinterName = UserInfo.PrinterQ;
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DocumentName = "Slip Display Print";
                printDocument1.Print();
            }
            catch
            { 
               MessageBox.Show("Printer is invalid");
            }
        }

        int myLocation = 0;
        private int page = 0;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Microsoft Sans Serif");
            PointF pointF1 = new PointF(10, 10);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Graphics graphics = e.Graphics;

            System.Drawing.Font fntDrawString = new System.Drawing.Font(fontFamily, 9, FontStyle.Bold);

            float pageHeight = e.MarginBounds.Height;
            float fontHeight = fntDrawString.GetHeight();

            int startX = 40;
            int startY = 30;
            int offsetY = 40;

            try
            {
                string UserName = UserInfo.UserName;

                List<string> Linetoprint = new List<string>();

                Linetoprint.Add("                                  SLIP DISPLAY");

                Linetoprint.Add(UserName);
                
                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblId.Text, txtId.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblFin.Text, txtFin.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblProjTitle.Text, txtProjTitle.Text));

                foreach (DodgeNotes.Title a in TitleList)
                {
                    bool notThere = string.IsNullOrEmpty(a.Titlenotes);
                    
                    if (notThere == false)
                    {
                        if (a.Titlenotes.Length > 120)
                        {
                            string shorttitle = a.Titlenotes.Substring(0, 120);
                            Linetoprint.Add(shorttitle);
                        }
                        else
                        {
                            Linetoprint.Add(a.Titlenotes);
                        }
                    }
                }

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblProjValue.Text, txtProjValue.Text));

                foreach (DodgeNotes.Value a in ValueList)
                {
                    bool notThere = string.IsNullOrEmpty(a.Valuenotes);

                    if (notThere == false)
                    {
                        if (a.Valuenotes.Length > 120)
                        {
                            string shortvalue = a.Valuenotes.Substring(0, 120);
                            Linetoprint.Add(shortvalue);
                        }
                        else
                        {
                            Linetoprint.Add(a.Valuenotes);
                        }
                    }
                }

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblProjGroup.Text, txtProjGroup.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblPPtype.Text, txtPPtype.Text));

                Linetoprint.Add("");

                //Print the tab control
                try
                {
                    foreach (Control objControl in tbPtype.Controls)
                    {
                        if (objControl.HasChildren)
                        {
                            if (objControl.Enabled == true)
                            {
                                foreach (Control objControl1 in objControl.Controls)
                                {
                                    if (objControl1 is TextBox)
                                    {
                                        Linetoprint.Add(string.Format("{0}   {1}", objControl.Text, objControl1.Text));

                                    }
                                }
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    throw ex;
                }

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblProjStat.Text, txtProjStatus.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblAddr1.Text, txtAddr1.Text));
                
                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblAddr2.Text, txtAddr2.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblCity.Text, txtCity.Text));
                
                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblCounty.Text, txtCounty.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblState.Text, txtState.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblZip.Text, txtZip.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblTotSquare.Text, txtTotSquare.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblTypeofwork.Text, txtTypeofWork.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblFactorCode.Text, txtFactorCode.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblOwnerClass.Text, txtOwnerClass.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblNumBldgs.Text, txtNumBldgs.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblStorAbove.Text, txtNumStoriesAbove.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblStoriesBelow.Text, txtNumberStoriesBelow.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblContrMethod.Text, txtContrMethod.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblContr1.Text, txtContr1.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblContr2.Text, txtContr2.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblTargetStart.Text, txtTargetStartd.Text));

                Linetoprint.Add("");

                Linetoprint.Add(string.Format("{0}   {1}", lblTgtCompd.Text, txtTargetCompd.Text));

                Linetoprint.Add("");
                Linetoprint.Add("");

                Linetoprint.Add("ADDITIONAL FEATURES INFORMATION");

                foreach (DodgeNotes.Add a in AddList)
                {
                    bool notThere = string.IsNullOrEmpty(a.Addnotes);

                    if (notThere == false)
                    {
                        if (a.Addnotes.Length > 120)
                        {
                            string shortadd = a.Addnotes.Substring(0, 120);
                            Linetoprint.Add(shortadd);
                        }
                        else
                        {
                            Linetoprint.Add(a.Addnotes);
                        }

                    }
                }

                Linetoprint.Add("");
                Linetoprint.Add("");

                Linetoprint.Add(" ITEM INCLUDES INFORMATION");

                foreach (DodgeNotes.Item a in ItemList)
                {
                    bool notThere = string.IsNullOrEmpty(a.Itemnotes);

                    if (notThere == false)
                    {
                        if (a.Itemnotes.Length > 120)
                        {
                            string shortitem = a.Itemnotes.Substring(0, 120);
                            Linetoprint.Add(shortitem);
                        }
                        else
                        {
                            Linetoprint.Add(a.Itemnotes);
                        }
                    }
                }

                Linetoprint.Add("");
                Linetoprint.Add("");

                Linetoprint.Add("REPORTER NOTES");

                foreach (DodgeNotes.Reporter a in ReporterList)
                {
                    bool notThere = string.IsNullOrEmpty(a.Reporternotes);

                    if (notThere == false)
                    {
                        if (a.Reporternotes.Length > 120)
                        {
                            string shortreporter = a.Reporternotes.Substring(0, 120);
                            Linetoprint.Add(shortreporter);
                        }
                        else
                        {
                            Linetoprint.Add(a.Reporternotes);
                        }
                    }
                }

                e.HasMorePages = false;
                while ( myLocation < Linetoprint.Count )
                {
                    graphics.DrawString(Linetoprint[myLocation], fntDrawString, solidBrush, startX, startY + offsetY);
                    offsetY += (int)FontHeight;
                    myLocation++;

                    if (offsetY >= pageHeight)
                    {
                        offsetY = 0;
                        e.HasMorePages = true;

                        page++;
                        String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

                        //Draw Date
                        e.Graphics.DrawString(strDate, fntDrawString, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                                e.Graphics.MeasureString(strDate, fntDrawString, e.MarginBounds.Width).Width), 10);

                        //Draw username
                        e.Graphics.DrawString(UserInfo.UserName, fntDrawString, Brushes.Black, 40, 10);

                        graphics.DrawString("Page " + page, fntDrawString, solidBrush, 850, 25);

                        return;

                    }
   
                }


            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            }

        
      //Go to previous screen

      private void btnPrevious_Click(object sender, EventArgs e)
      {

          this.Close();
      }

    private void dgADD_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
    {
        String info;

        //get note
        info = (dgADD.Rows[e.RowIndex].Cells[0].Value).ToString();
        MessageBox.Show(info, "Additional Features Information");
    }

        private void dgITM_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String info;

            //get note
            info = (dgITM.Rows[e.RowIndex].Cells[0].Value).ToString();
            MessageBox.Show(info, "Item Includes Information");
        }

        private void dgREP_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            String info;

            //get note
            info = (dgREP.Rows[e.RowIndex].Cells[0].Value).ToString();
            MessageBox.Show(info, "Reporter Notes");
        }
    }

}
