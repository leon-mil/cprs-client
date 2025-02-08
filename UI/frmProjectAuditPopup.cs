/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmProjectAuditPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      01/14/2016
Inputs:             ID
Parameters:		    None                
Outputs:		    None

Description:	    This program displays the audit data
                    for a id case

Detailed Design:    Detailed User Requirements for C700 Screen 

Other:	            Called from: frmC700
 
Revision History:	
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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;

namespace Cprs
{
    public partial class frmProjectAuditPopup : Form
    {
        private string id;
        private string respid;
        private string survey;
        private string newtc;

        public frmProjectAuditPopup(string passed_id, string passed_respid, string passed_owner, string passed_newtc)
        {
            InitializeComponent();
            id = passed_id;
            respid = passed_respid;
            survey = passed_owner;
            newtc = passed_newtc;
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

        //Private variables

        private DataTable dtp = null;
        private DataTable dtv = null;
        private DataTable dtr = null;

        /*Create instance of ProjectAuditData object */
        private ProjectAuditData dataObject;

        private void frmProjectAuditPopup_Load(object sender, EventArgs e)
        {
            txtID.Text = id;
            txtRespid.Text = respid;
            txtNewtc.Text = newtc;
            txtSurvey.Text = survey;

            dataObject = new ProjectAuditData();
            dtp = dataObject.GetProjectAuditDataForID(id);
            dtv = dataObject.GetVIPAuditDataForID(id);
            dtr = dataObject.GetRespAuditDataForID(respid);

            dgProject.DataSource = dtp;
            dgVip.DataSource = dtv;
            dgResp.DataSource = dtr;

            setItemColumnHeader();
            setVipColumnHeader();
            setRespColumnHeader();

        }

        private void setItemColumnHeader()
        {
            dgProject.Columns[0].HeaderText = "VARNME";
            dgProject.Columns[1].HeaderText = "OLDVAL";
            dgProject.Columns[2].HeaderText = "OLDFLAG";
            dgProject.Columns[3].HeaderText = "NEWVAL";
            dgProject.Columns[4].HeaderText = "NEWFLAG";
            dgProject.Columns[5].HeaderText = "USER";
            dgProject.Columns[6].HeaderText = "DATE";
        }

        private void setVipColumnHeader()
        {
            dgVip.Columns[0].HeaderText = "DATE6";
            dgVip.Columns[1].HeaderText = "OLDVAL";
            dgVip.Columns[2].HeaderText = "OLDFLAG";
            dgVip.Columns[3].HeaderText = "NEWVAL";
            dgVip.Columns[4].HeaderText = "NEWFLAG";
            dgVip.Columns[5].HeaderText = "USER";
            dgVip.Columns[6].HeaderText = "DATE";
        }

        private void setRespColumnHeader()
        {
            dgResp.Columns[0].HeaderText = "VARNME";
            dgResp.Columns[1].HeaderText = "OLDVAL";           
            dgResp.Columns[2].HeaderText = "NEWVAL";
            dgResp.Columns[3].HeaderText = "USER";
            dgResp.Columns[4].HeaderText = "DATE";
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
