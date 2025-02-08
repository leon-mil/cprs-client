/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCemarkPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/06/2015
Inputs:             ID
                
Parameters:		    None                
Outputs:		    newMark

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
    public partial class frmCemarkPopup : Form
    {
        private string mcdid;

        private CemarkData cmarkdata;
        private Cemark cmark;

        public frmCemarkPopup(string passed_mcdid)
        {
            InitializeComponent();
            mcdid = passed_mcdid;
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

        private void frmCemarkPopup_Load(object sender, EventArgs e)
        {
            /*Cemark */
            cmarkdata = new CemarkData();
            cmark = cmarkdata.GetCemarkData(mcdid);
            if (cmark != null)
            {
                txtMark.Text = cmark.Marktext;
                btnDelete.Enabled = true;
            }
            else
            {
                txtMark.Text = "";
                btnDelete.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string new_mark = "";
            string pt = "";

            if (txtMark.Text.Trim() == "")
            {
                MessageBox.Show("Mark note cannot be blank.");
                return;
            }
            else 
            {
                new_mark = txtMark.Text;
                if (cmark == null)
                {
                    Cemark cc = new Cemark();
                    cc.Id = mcdid;
                    cc.Usrnme = UserInfo.UserName;
                    cc.Marktext = new_mark;
                    pt = DateTime.Now.ToString();
                    cmarkdata.AddCemarkData(cc);
                }
                else
                {
                    cmark.Marktext = new_mark;
                    cmark.Usrnme = UserInfo.UserName;
                    pt = DateTime.Now.ToString();
                    cmarkdata.UpdateCemarkData(cmark);
                }
            }

            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the mark?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                cmarkdata.DeleteCemark(mcdid);
                cmark = null;

                this.Dispose();
            }           
        }

      
        
    }
}
