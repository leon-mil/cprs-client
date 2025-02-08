/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmAddCCMailPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      02/22/2015

Inputs:             None
                                     
Parameters:         None
                 
Outputs:            None

Description:	    This program allows users to add an email address to the CCMAIL table

Detailed Design:    Detailed Design for CCMail Review

Other:	            Called from: frmCCMail.cs
 
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

namespace Cprs
{
    public partial class frmAddCCMailPopup : Form
    {
        private CCMailData dataObject;

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

        public frmAddCCMailPopup()
        {
            InitializeComponent();
        }

        private void frmAddCCMailPopup_Load(object sender, EventArgs e)
        {
            CreateDataSource();

            //cbJobFlag.DataSource = dataObject.CreateJobflagTable();
            cbJobFlag.SelectedIndex = -1;
        }

        private void CreateDataSource()
        {

            BindingSource source = new BindingSource();
            List<string> dataSource = new List<string>();

            dataSource.Add("DSMP - DCP SAMPLE/LOAD");
            dataSource.Add("DFRM - DCP FORMS ");
            dataSource.Add("MPRE - MF PRESAMPLE");
            dataSource.Add("MSMP - MF SAMPLE/FORMS");
            dataSource.Add("MWGT - MF WEIGHT");
            dataSource.Add("FVIP - FEDERAL VIP");
            dataSource.Add("PTAB - PRELIM  TABULATION");
            dataSource.Add("FTAB - FINAL TABULATION");
            dataSource.Add("TSAR - TSAR PROCESS");
            dataSource.Add("CFRM - CONTINUING FORMS");
            dataSource.Add("ROLL - ROLLOVER");
            dataSource.Add("CLOD - CE SAMPLE/LOAD");
            dataSource.Add("CTAB - CE TABULATIONS");
            dataSource.Add("CFOR - CE FORECASTING");
            dataSource.Add("CVIP - CE LOAD VIP");
            dataSource.Add("USR1 - USER EDIT");
            dataSource.Add("USR2 - USER REVIEW");
            dataSource.Add("USR3 - AUDIT REVIEW");
            dataSource.Add("CENT - CENTURION LOAD");
            dataSource.Add("BLST - EMAIL BLAST");

            cbJobFlag.DataSource = dataSource;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataObject = new CCMailData();

            //check if the factors are blank, if so, display messagebox

            if (txtEmail.Text == "" || cbJobFlag.Text == "")
            {
                if (txtEmail.Text == "")
                {
                    MessageBox.Show("Please enter an Email Address");
                }
                if (txtEmail.Text != "" && cbJobFlag.Text == "")
                {
                    MessageBox.Show("Please select a job flag");
                }

            }

            else
            {
                //check if valid email address

                string inputEmail = txtEmail.Text;

                string findString = "@census.gov";

                bool govemail = inputEmail.EndsWith(findString);

                if (GeneralFunctions.isEmail(inputEmail))
                {
                    if (govemail)
                    {
                        addEmail();
                    }
                    else
                    {
                        MessageBox.Show("The email address entered is not valid");
                    }
                }
                else
                {
                    MessageBox.Show("The email address entered is not valid");
                    txtEmail.Focus();
                }
            }
        }

        //Check if the email already exists in the table. 
        //If not, add the email to the CCMail table

        private void addEmail()
        {
            string usrnme = UserInfo.UserName;
            string prgdtm;
            string jobflag = cbJobFlag.Text.Substring(0, cbJobFlag.Text.IndexOf(" "));

            //Check if the email already exists in the table.

            bool ccmailexist;

            ccmailexist = dataObject.CheckCCMailExist(jobflag, txtEmail.Text);

            if (ccmailexist == true)
            {
               MessageBox.Show("This email address already exists for this job flag");
            }
            else
            {
               DateTime dt = DateTime.Now;
               prgdtm = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);

               //Add the ccmail row to the table

               dataObject.AddCCMail(jobflag, txtEmail.Text, usrnme, prgdtm);
               this.Refresh();
               this.Dispose();
            }          
        }

        //return to the previous screen without saving

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

    }
}
