/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCemarkPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/06/2015
Inputs:             ID
                
Parameters:		    None                
Outputs:		    Date8, newMark

Description:	    This screen will allow the user to add cemark, delete cemark

Detailed Design:    Detailed User Requirements for Improvement Screen 

Other:	            Called from: frmImprovement
 
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
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmMarkPopup : Form
    {
        private string id;
        private string respid;

        private ProjMarkData pmarkdata;
        private ProjMark pmark;
        private RespMarkData rmarkdata;
        private RespMark rmark;

        public frmMarkPopup(string passed_id, string passed_respid)
        {
            InitializeComponent();
            id = passed_id;
            respid = passed_respid;
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

        private void frmMarkPopup_Load(object sender, EventArgs e)
        {
            /*Proj mark */
            rdbP.Checked = true;
            rdbR.Checked = false;

            if (respid == id)
            {
                rdbP.Enabled = false;
                rdbR.Enabled = false;
            }

            pmarkdata = new ProjMarkData();
            pmark = pmarkdata.GetProjmarkData(id);

            rmarkdata = new RespMarkData();
            rmark = rmarkdata.GetRespmarkData(respid);

            if (rdbP.Checked)
            {                
                if (pmark != null)
                {
                    txtMark.Text = pmark.Marktext;
                    btnDelete.Enabled = true;
                }
                else
                {
                    txtMark.Text = "";
                    btnDelete.Enabled = false;
                }
            }
        }

        private void rdbR_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbR.Checked)
            {
                if (rmark != null)
                {
                    txtMark.Text = rmark.Marktext;
                    btnDelete.Enabled = true;
                }
                else
                {
                    txtMark.Text = "";
                    btnDelete.Enabled = false;
                }
                rdbP.Checked = false;
            }
        }

        private void rdbP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbP.Checked)
            {
                if (pmark != null)
                {
                    txtMark.Text = pmark.Marktext;
                    btnDelete.Enabled = true;
                }
                else
                {
                    txtMark.Text = "";
                    btnDelete.Enabled = false;
                }
                rdbR.Checked = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string new_mark = "";

            if (txtMark.Text.Trim() == "")
            {
                MessageBox.Show("Mark note cannot be blank.");
                return;
            }
            else
            {
                new_mark = txtMark.Text;
                if (rdbP.Checked)
                {
                    if (pmark == null)
                    {
                        ProjMark cc = new ProjMark();
                        cc.Id = id;
              
                        cc.Usrnme = UserInfo.UserName;
                        cc.Marktext = new_mark;
                        pmarkdata.AddProjMarkData(cc);
                    }
                    else
                    {
                        pmark.Marktext = new_mark;
                        
                        pmark.Usrnme = UserInfo.UserName;
                        pmarkdata.UpdateProjMarkData(pmark);
                    }
                }
                else
                {
                    if (rmark == null)
                    {
                        RespMark cc = new RespMark();
                        cc.Respid = respid;
                        
                        cc.Usrnme = UserInfo.UserName;
                        cc.Marktext = new_mark;
                        rmarkdata.AddRespMarkData(cc);
                    }
                    else
                    {
                        rmark.Marktext = new_mark;
                        
                        rmark.Usrnme = UserInfo.UserName;
                        rmarkdata.UpdateRespMarkData(rmark);
                    }

                }
            }

    
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the mark?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (rdbP.Checked)
                {
                    pmarkdata.DeleteProjMark(id);
                    pmark = null;
                }
                else
                {
                    rmarkdata.DeleteRespMark(respid);
                    rmark = null;
                }

               this.Dispose();

            }           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
