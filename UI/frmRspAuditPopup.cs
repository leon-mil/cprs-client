/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmRspAuditPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      04/27/2016

Inputs:             Respid                   

Parameters:         None
                  
Outputs:            

Description:        This program displays the Respondent AUdit data for a 
 *                  selected RESPID 

Detailed Design:    Multi Family Initial Address Detailed Design

Other:              Called from: frmMFInitial.cs
 
Revision History:	
***********************************************************************************
Modified Date:  
Modified By:  
Keyword:  
Change Request:  
Description:  
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
    public partial class frmRspAuditPopup : Form
    {
        private string Respid;
        private DataTable dtr = null;

        /*Create instance of ProjectAuditData object */
        private ProjectAuditData dataObject;

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

        //Respid comes from frmMFInitial.cs screen

        public frmRspAuditPopup(string respid)
        {

            InitializeComponent();
            Respid = respid;
        }

        private void frmRspAuditPopup_Load(object sender, EventArgs e)
        {
            txtRespid.Text = Respid;
            dataObject = new ProjectAuditData();
            dtr = dataObject.GetRespAuditDataForID(Respid);
            dgRespAudit.DataSource = dtr;
            setRespColumnHeader();
        }

        private void setRespColumnHeader()
        {
            dgRespAudit.Columns[0].HeaderText = "VARNME";
            dgRespAudit.Columns[1].HeaderText = "OLDVAL";
            dgRespAudit.Columns[2].HeaderText = "NEWVAL";
            dgRespAudit.Columns[3].HeaderText = "USER";
            dgRespAudit.Columns[4].HeaderText = "DATE/TIME";
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
