/*********************************************************************

Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmAuthUserAnnAuditPopup.cs	    	

Programmer:         Srini Natarajan

Creation Date:      09/12/2016

Inputs:             accountrvw table.

Parameters:         None

Outputs:            None

Description:	    This program displays the Display the List of Annual Audit data.
 
Detailed Design:    None 

Other:	            Called from: frmAuthUsers.cs
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**********************************************************************/

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
using DGVPrinterHelper;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmAuthUserAnnAuditPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private AuthorizedUserAuditData dataObject;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmAuthUserAnnAuditPopup()
        {
            InitializeComponent();
        }

        private void frmAuthUserAnnAuditPopup_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            //get the data from UserAudit table.
            GetDataTable1();            
        }

        private DataTable GetDataTable1()
        {
            dataObject = new AuthorizedUserAuditData();
            DataTable dt = dataObject.GetAnnualAuthorizedUsersData();
            dgData.DataSource = dt;
            
            dgData.Columns[0].HeaderText = "YEAR";
            dgData.Columns[0].Width = 60;
            dgData.Columns[1].HeaderText = "REVIEW NUMBER";
            dgData.Columns[1].Width = 60;
            dgData.Columns[2].HeaderText = "USER";
            dgData.Columns[2].Width = 70;
            dgData.Columns[3].HeaderText = "DATE/TIME";
            dgData.Columns[3].Width = 120;
            dgData.Columns[4].HeaderText = "ACCOUNTS";
            dgData.Columns[4].Width = 80;

            string data = string.Empty;
            
            foreach (DataGridViewRow row in dgData.Rows)
            {
                data = (String)row.Cells["ACCOUNTS"].Value;
                if (data == "A")
                {
                    data = "Acceptable";
                    row.Cells["ACCOUNTS"].Value = data;
                }
                else
                {
                    data = "Unacceptable";
                    row.Cells["ACCOUNTS"].Value = data;
                }
            }
 
            dgData.Columns[5].HeaderText = "COMMENTS";
            dgData.Columns[5].Width = 520;
            return dt;
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "ANNUAL AUDIT REVIEW";

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            /*Shrink the width of some columns to fit all columns in one page */
            DataGridViewColumn column1 = dgData.Columns[0];
            column1.Width = 60;
            DataGridViewColumn column2 = dgData.Columns[1];
            column2.Width = 60;
            DataGridViewColumn column3 = dgData.Columns[2];
            column3.Width = 70;
            DataGridViewColumn column4 = dgData.Columns[3];
            column4.Width = 120;
            DataGridViewColumn column5 = dgData.Columns[4];
            column5.Width = 80;
            DataGridViewColumn column6 = dgData.Columns[5];
            column6.Width = 460;

            printer.HeaderCellAlignment = StringAlignment.Near;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Annual Audit Review print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgData);

            dgData.Columns[0].Width = 60;
            dgData.Columns[1].Width = 70;
            dgData.Columns[2].Width = 70;
            dgData.Columns[3].Width = 120;
            dgData.Columns[4].Width = 80;
            dgData.Columns[5].Width = 520;
            Cursor.Current = Cursors.Default;
        }

        //Open Audit history screen
        private void button1_Click(object sender, EventArgs e)
        {
            frmAuthUserAnnAddPopup fAudit = new frmAuthUserAnnAddPopup();
            fAudit.ShowDialog();  //show history screen
            FormLoad();
        }
    }
}
