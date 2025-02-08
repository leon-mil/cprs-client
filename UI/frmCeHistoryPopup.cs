
/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCeHistoryPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/26/2015
Inputs:             ID
Parameters:		    None                
Outputs:		    None

Description:	    This program displays the CeHistory data
                    for a  case

Detailed Design:    Detailed User Requirements for Improvement Screen 

Other:	            Called from: frmImprovements
 
Revision History:	
***********************************************************************************
 Modified Date :  6/3/2020
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request: CR# 4231 
 Description   :  Add code to show hyperlinks
***********************************************************************************/
using System;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using DGVPrinterHelper;
using System.Diagnostics;
namespace Cprs
{
    public partial class frmCeHistoryPopup : Form
    {
        /*Form global variable */
        private Cecomments cecomms;
        private CecommentData cecommdata;

        /*private property */
        private string id = null;

        public frmCeHistoryPopup(string passed_id)
        {
            InitializeComponent();
            id = passed_id;
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

        private void frmCeHistoryPopup_Load(object sender, EventArgs e)
        {
            Loadform();
        }

        private void Loadform()
        {
            cecommdata = new CecommentData();
            cecomms = cecommdata.GetCecommentData(id);

            dgHist.DataSource = cecomms.Cecommentlist;

            dgHist.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgHist.RowHeadersVisible = false; // set it to false if not needed

            /*Set up header and visible */
            for (int i = 0; i < dgHist.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgHist.Columns[i].HeaderText = "DATE";
                    dgHist.Columns[i].Width = 80;
                }

                if (i == 1)
                {
                    dgHist.Columns[i].Visible = false;
                }

                if (i == 2)
                {
                    dgHist.Columns[i].HeaderText = "USER";
                    dgHist.Columns[i].Width = 100;
                }

                if (i == 3)
                {
                    dgHist.Columns[i].HeaderText = "COMMENTS";
                    
                    dgHist.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            lblMcd.Text = "ID: " + id;
        }


        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            frmCecommentPopup popup = new frmCecommentPopup(id);
            popup.CecommentUpdated += new frmCecommentPopup.CecommentUpdateHandler(CecommentForm_ButtonClicked);
            popup.ShowDialog();
        }

        // handles the event from frmCecommentPopup
        private void CecommentForm_ButtonClicked(object sender, CecommentUpdateEventArgs e)
        {
            // update the forms values from the event args
            Loadform();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            
            printer.Title = "IMPROVEMENTS COMMENTS";
            printer.SubTitle = "ID: " + id;
           
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
           
            printer.printDocument.DocumentName = "Improvements Comments";
            
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
          
            //resize the column
            dgHist.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgHist.Columns[3].Width = 700;
            printer.PrintDataGridViewWithoutDialog(dgHist);

            //resize back the column
            dgHist.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            Cursor.Current = Cursors.Default;
        }

        private void dgHist_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            foreach (DataGridViewRow r in dgHist.Rows)
            {
                if (System.Uri.IsWellFormedUriString(r.Cells["COMMTEXT"].Value.ToString(), UriKind.Absolute))
                {
                    r.Cells["COMMTEXT"] = new DataGridViewLinkCell();
                    // Note that if I want a different link colour for example it must go here
                    DataGridViewLinkCell c = r.Cells["COMMTEXT"] as DataGridViewLinkCell;
                    c.LinkColor = Color.Green;
                }
            }
        }

        private void dgHist_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (System.Uri.IsWellFormedUriString(dgHist.Rows[e.RowIndex].Cells["COMMTEXT"].Value.ToString(), UriKind.Absolute))
            {
                string sUrl = dgHist.Rows[e.RowIndex].Cells["COMMTEXT"].Value.ToString();

                ProcessStartInfo sInfo = new ProcessStartInfo(sUrl);

                Process.Start(sInfo);
            }
        }
    }
}

