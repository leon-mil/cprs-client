/*********************************************************************
Econ App Name   : CPRS

Project Name    : CPRS Interactive Screens System
 
Program Name    : cprs.frmAuthUserAnnAddPopup.cs	    	

Programmer      : Srini Natarajan

Creation Date   : 09/12/2015

Inputs          : None

Parameters      : None

Outputs         : accountrvw 

Description     : This program displays the Display the List of Annual Audit data.

Detailed Design : None 

Other           : Called from: frmAuthUserAnnAuditPopup.cs
 
Revision History:	
*********************************************************************
 Modified Date :  12/26/2019
 Modified By   :  Diane Musachio
 Keyword       :  dm122619
 Change Request:  
 Description   :  Send email if acceptable
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
using Cprs;

namespace Cprs
{
    public partial class frmAuthUserAnnAddPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private AuthorizedUserAuditData AuthUserData;
        private string CurrYear;
        private int CurrMon;
        
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmAuthUserAnnAddPopup()
        {
            InitializeComponent();
        }

        private void frmAuthUserAnnAddPopup_Load(object sender, EventArgs e)
        {
            AuthUserData = new AuthorizedUserAuditData();

            //get current year, current month
            CurrYear = DateTime.Now.Year.ToString();
            CurrMon = DateTime.Now.Month;

            string accounts = "";
            string comment = "";

            int revnum = 1;
            if (CurrMon > 7)
                revnum = 2;
            AuthUserData.GetCurrYearAuditReview(CurrYear, revnum, ref accounts, ref comment);

            //If there is no data for the current year then set accounts to A
            if (accounts == "")
            {
                accounts = "A";
            }
            if (accounts == "A")
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                textBox1.Enabled = false;
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                if (comment != "")
                    textBox1.Text = comment;
                textBox1.Enabled = true;
            }
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //save the changes to the accountrvw table
            string accStatus;
            Boolean curYearDel = false;
            string data = string.Empty;
            string commtextVal = string.Empty;
            DateTime prgdtm = DateTime.Now;

            if (radioButton1.Checked == true)
            { 
                accStatus = "A";
                commtextVal = " ";
            }
            else
            { 
                accStatus = "U";
                commtextVal = textBox1.Text;
            }

            if (accStatus == "U")
            {
                if (textBox1.Text.Length < 1 || textBox1.Text == null)
                {
                    MessageBox.Show("Comments cannot be empty");
                    return;
                }
            }

            int revnum = 1;
            if (CurrMon > 7)
                revnum = 2;

            if (AuthUserData.CheckCurrYearRec(CurrYear, revnum))
               curYearDel = true; 
            else
               curYearDel = false;

            if (curYearDel == false)
            {
                //run the insert function
                AuthUserData.AddCurYear(CurrYear, revnum, accStatus, commtextVal, UserInfo.UserName, prgdtm);
            }
            else
            {
                //run the update function
                AuthUserData.UpdateAnnAuthUserComment(commtextVal, CurrYear, revnum, accStatus, UserInfo.UserName, prgdtm);
            }

            //send email
            if (accStatus == "U")
            {
                string suj = "Authorized User Annual Report Update"; 
                string mbody = " Unacceptable" + System.Environment.NewLine +  textBox1.Text; 
                string from = UserInfo.UserName + "@census.gov";

                List<string> tolist = new List<string>();
                tolist = GeneralDataFuctions.GetJobEmails("USR2");
                if (tolist.Count > 0)
                GeneralFunctions.SendEmail(suj, mbody, from, tolist);
            }
            //dm122619, If acceptable, send email
            if (accStatus == "A")
            {
                string suj = "Authorized User Annual Report Update";
                string mbody = " Acceptable";
                string from = UserInfo.UserName + "@census.gov";

                List<string> tolist = new List<string>();
                tolist = GeneralDataFuctions.GetJobEmails("USR2");
                if (tolist.Count > 0)
                    GeneralFunctions.SendEmail(suj, mbody, from, tolist);
            }

            this.Close();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            CheckRadioButtons();
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            CheckRadioButtons();
        }

        //Enable/disable textbox based on radio button selection
        private void CheckRadioButtons()
        {
            if (radioButton1.Checked == true)
            {
                textBox1.Text = "";
                textBox1.Enabled = false;
            }
            else
            {
                textBox1.Enabled = true;
            }
        }
    }
}
