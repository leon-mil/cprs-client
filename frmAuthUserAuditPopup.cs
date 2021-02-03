/*********************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System
 
Program Name:       cprs.frmAuthUserAuditPopup.cs	    	

Programmer:         Srini Natarajan

Creation Date:      09/12/2016

Inputs:             UserAudit 

Parameters:         None

Outputs:	    None

Description:	    This program displays the Display data from UserAudit table.

Detailed Design:    None 

Other:	            
 
Called from:        frmAuthUsers.cs 
 
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

namespace Cprs
{
    public partial class frmAuthUserAuditPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        public static frmAuthUserAuditPopup Current;
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

        public frmAuthUserAuditPopup()
        {
            InitializeComponent();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void frmAuthUserAuditPopup_Load(object sender, EventArgs e)
        {
            //get the data from UserAudit table.
            GetDataTable1();
        }

        private DataTable GetDataTable1()
        {
            dataObject = new AuthorizedUserAuditData();
            DataTable dt = dataObject.GetAuthorizedUsersData();
            dgData.DataSource = dt;
            dgData.Columns[0].HeaderText = "AUTHORIZED USER";
            dgData.Columns[1].HeaderText = "ACTION";
            dgData.Columns[2].HeaderText = "OLDVAL";
            SetOldVal();
            dgData.Columns[3].HeaderText = "NEWVAL";
            setNewVal();
            dgData.Columns[4].HeaderText = "USER";
            dgData.Columns[5].HeaderText = "DATE/TIME";
            return dt;
        }

        //Convert the code to description for Old Val
        private void SetOldVal()
        {
            string data = string.Empty;
            int indexOfYourColumn = 2;
            foreach (DataGridViewRow row in dgData.Rows)
            {
                data = (String)row.Cells[indexOfYourColumn].Value;
                if (data == "0")
                {
                    data = "Programmer";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "1")
                {
                    data = "HQManager";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "2")
                {
                    data = "HQAnalyst";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "3")
                {
                    data = "NPCManager";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "4")
                {
                    data = "NPCLead";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "5")
                {
                    data = "NPCInterviewer";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "6")
                {
                    data = "HQSupport";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "7")
                {
                    data = "HQMathStat";
                    row.Cells[indexOfYourColumn].Value = data;
                }
                if (data == "8")
                {
                    data = "HQTester";
                    row.Cells[indexOfYourColumn].Value = data;
                }
            }
        }

        //Convert the code to description for New Val
            private void setNewVal()
            {
                string data = string.Empty;
                int indexOfYourColumn = 3;
                foreach (DataGridViewRow row in dgData.Rows)
                {
                    data = (String)row.Cells[indexOfYourColumn].Value;
                    if (data == "0")
                    {
                        data = "Programmer";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "1")
                    {
                        data = "HQManager";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "2")
                    {
                        data = "HQAnalyst";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "3")
                    {
                        data = "NPCManager";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "4")
                    {
                        data = "NPCLead";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "5")
                    {
                        data = "NPCInterviewer";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "6")
                    {
                        data = "HQSupport";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "7")
                    {
                        data = "HQMathStat";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                    if (data == "8")
                    {
                        data = "HQTester";
                        row.Cells[indexOfYourColumn].Value = data;
                    }
                }       
            }
    }
}
