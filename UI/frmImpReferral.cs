/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmImpReferral.cs	    	

Programmer:         Cestine Gill

Creation Date:      12/29/2015

Inputs:             ID
                                     
Parameters:         None
                 
Outputs:            None

Description:	    This program displays the Referral data in table form 
                    for a selected ID from the Improvements screen

Detailed Design:    Detailed Design for Improvement Referrals

Other:	            Called from: frmImprovements.cs
 
Revision Referrals:	
***********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmImpReferral : Form
    {
        public string Id;

        private ImpReferralData dataObject;

        public frmImpReferral()
        {
            InitializeComponent();
        }

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

        private void frmReferral_Load(object sender, EventArgs e)
        {
            //This value is set on and passed from the calling form
            txtId.Text = Id;

            LoadTables();

            //Disable button if table empty

            DisableButton();
        }

        private void LoadTables()
        {
            dataObject = new ImpReferralData();
            GetCEReferrals();
        }

        //Populates and formats the Project table 

        private void GetCEReferrals()
        {
            DataTable dtProj = new DataTable();

            //populate the data grid with the referral cases for 
            //the ID passed from the calling form 

            dtProj = dataObject.GetCEReferralTable(Id);

            dgProjReferrals.DataSource = dtProj;

            //format the datagrid and columns

            dgProjReferrals.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgProjReferrals.RowHeadersVisible = false;  // set it to false if not needed
            dgProjReferrals.ShowCellToolTips = false;

            for (int i = 0; i < dgProjReferrals.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgProjReferrals.Columns[i].HeaderText = "TYPE";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 1)
                {
                    dgProjReferrals.Columns[i].HeaderText = "STATUS";
                    dgProjReferrals.Columns[i].Width = 80;
                    dgProjReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;

                }
                if (i == 2)
                {
                    dgProjReferrals.Columns[i].HeaderText = "USER";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 3)
                {
                    dgProjReferrals.Columns[i].HeaderText = "GROUP";
                    dgProjReferrals.Columns[i].Width = 100;

                }
                if (i == 4)
                {
                    dgProjReferrals.Columns[i].HeaderText = "DATE/TIME";
                    dgProjReferrals.Columns[i].Width = 120;

                }
                if (i == 5)
                {
                    dgProjReferrals.Columns[i].HeaderText = "NOTE";
                    dgProjReferrals.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    dgProjReferrals.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }

            //reassign the value from the database to a 
            //user-friendlier representation in the table

            int reftypeColumn = 0; //Put your column number here
            int refgroupColumn = 3;
            int refstatusColumn = 1;

            for (int i = 0; i < dtProj.Rows.Count; i++)
            {

                switch (dtProj.Rows[i][reftypeColumn].ToString())
                {
                    case "2":
                        dtProj.Rows[i][reftypeColumn] = "Correct Flags";
                        break;
                    case "3":
                        dtProj.Rows[i][reftypeColumn] = "Data Issue";
                        break;
                    case "5":
                        dtProj.Rows[i][reftypeColumn] = "Free Form";
                        break;
                }

                switch (dtProj.Rows[i][refgroupColumn].ToString())
                {
                    case "1":
                        dtProj.Rows[i][refgroupColumn] = "HQ Supervisor";
                        break;
                    case "2":
                        dtProj.Rows[i][refgroupColumn] = "HQ Analyst";
                        break;

                }
                switch (dtProj.Rows[i][refstatusColumn].ToString())
                {
                    case "A":
                        dtProj.Rows[i][refstatusColumn] = "Active";
                        break;
                    case "C":
                        dtProj.Rows[i][refstatusColumn] = "Complete";
                        break;
                }
            }
        }

        //Display the Update Referral popup.

        private void btnUpdReferral_Click(object sender, EventArgs e)
        {

            //Get the values for the selected row data to display on the popup screen

            string Reftype = dgProjReferrals.SelectedRows[0].Cells[0].Value.ToString();
            string Refgroup = dgProjReferrals.SelectedRows[0].Cells[3].Value.ToString();
            string Usrnme = dgProjReferrals.SelectedRows[0].Cells[2].Value.ToString();
            string Refnote = dgProjReferrals.SelectedRows[0].Cells[5].Value.ToString();
            string Refstatus = dgProjReferrals.SelectedRows[0].Cells[1].Value.ToString();
            string Prgdtm = dgProjReferrals.SelectedRows[0].Cells[4].Value.ToString();

            frmCeUpdReferralPopup URpopup = new frmCeUpdReferralPopup(Id, Reftype, Refstatus, Refgroup, Refnote, Prgdtm, Usrnme);

            URpopup.ShowDialog();
            LoadTables();

        }


        //Display the Add Referral popup.

        private void btnAddReferral_Click(object sender, EventArgs e)
        {
            frmCeAddReferralPopup ARpopup = new frmCeAddReferralPopup(Id);
            ARpopup.ShowDialog();
            LoadTables();
            DisableButton();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled ? Color.DarkBlue : Color.Gray;
        }

        private void btnUpdReferral_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            var drawBrush = new SolidBrush(btn.ForeColor);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            btnUpdReferral.Text = string.Empty; //remove the button text
            e.Graphics.DrawString("UPDATE REFERRAL", btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();
        }

        //Disable the update referral button if there are no referral
        //cases in the table

        private void DisableButton()
        {
            if (dgProjReferrals.RowCount != 0)
                btnUpdReferral.Enabled = true;

            else
                btnUpdReferral.Enabled = false;
        }

        /*Print the datagrid based upon the selected tab*/

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            
            printer.Title = "IMPROVEMENTS REFERRALS";
            
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Improvements Referrals";
           
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //resize the note column
            dgProjReferrals.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgProjReferrals.Columns[5].Width = 400;
            printer.PrintDataGridViewWithoutDialog(dgProjReferrals);

            //resize back the note column
            dgProjReferrals.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Cursor.Current = Cursors.Default;
        }

       
    }
}
