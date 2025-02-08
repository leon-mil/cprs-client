/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCecommentPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/06/2015
Inputs:             MCDID               
Parameters:		    None                
Outputs:		    Date8, NewComments
Description:	    This screen will allow the user to add cecomments
Detailed Design:    Detailed User Requirements for Improvement Screen 
Other:	            Called from: frmImprovement, frmcehistoryPopup
 
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
    public partial class frmCecommentPopup : Form
    {
        private string id;

        // add a delegate
        public delegate void CecommentUpdateHandler(
            object sender, CecommentUpdateEventArgs e);

        // add an event of the delegate type
        public event CecommentUpdateHandler CecommentUpdated;

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

        public frmCecommentPopup(string mid)
        {
            InitializeComponent();
            id = mid;
        }

        private void frmCecommentPopup_Load(object sender, EventArgs e)
        {
            txtRemark.Visible = true;
            cbApp.Visible = false;
            cbCost.Visible = false;
            label1.Text = "Enter Comment:";
        }

        private void btnApp_Click(object sender, EventArgs e)
        {
            txtRemark.Visible = false;
            txtRemark.Text = "";
            cbApp.Visible = true;
            cbCost.Visible = false;
            cbCost.SelectedIndex =-1;
            label1.Text = "Select Appliance Adjustment Comment From List:";
        }

        private void btnCost_Click(object sender, EventArgs e)
        {
            txtRemark.Visible = false;
            txtRemark.Text = "";
            cbApp.Visible = false;
            cbApp.SelectedIndex =-1;
            cbCost.Visible = true;
            label1.Text = "Select Current Month Cost Adjustment Comment From List:";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CecommentData cecomdata = new CecommentData();
            if (txtRemark.Visible && txtRemark.Text == "")
                this.Dispose();
            else if (cbApp.Visible && cbApp.SelectedIndex == -1)
                this.Dispose();
            else if (cbCost.Visible && cbCost.SelectedIndex == -1)
                this.Dispose();
            else
            {
                string new_comment = "";
                if (txtRemark.Text != "")
                    new_comment = txtRemark.Text;
                else if (cbApp.SelectedIndex >= 0)
                    new_comment = cbApp.SelectedItem.ToString();
                else if (cbCost.SelectedIndex >= 0)
                    new_comment = cbCost.SelectedItem.ToString();

                if (new_comment != "")
                {
                    Cecomment cc = new Cecomment();
                    cc.Commdate = DateTime.Now.ToString("yyyyMMdd");
                    cc.Commtime = DateTime.Now.ToString("HHmmss");
                    cc.Commtext = new_comment;

                    cecomdata.AddCecommentData(id, cc);

                    // instance the event args and pass it each value
                    CecommentUpdateEventArgs args = new CecommentUpdateEventArgs(cc.Commdate, new_comment);

                    // raise the event with the updated arguments
                    CecommentUpdated(this, args);
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
