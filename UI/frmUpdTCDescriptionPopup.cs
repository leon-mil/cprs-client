/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmUpdTCDescriptionPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      3/18/2016

Inputs:             None
 
Parameters:	        Newtc, Description
                 
Outputs:	        None

Description:	    This screen will allow the user to update Newtc in the dbo.NEWTCLIST Table

Detailed Design:    Detailed Design for NEWTC Description Review

Other:	            Called From: frmTCDescriptionReview.cs
 
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
    public partial class frmUpdTCDescriptionPopup : Form
    {
        public string newtc;
        public string old_description;

        private TCDescriptionReviewData dataObject;

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

        public frmUpdTCDescriptionPopup(string Newtc, string Description)
        {
            InitializeComponent();

            newtc = Newtc;
            old_description = Description;
        }

        private void frmUpdTCDescription_Load(object sender, EventArgs e)
        {

            //Display values from the calling form

            txtNewtc.Text = newtc;
            txtDescription.Text = old_description;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
              dataObject = new TCDescriptionReviewData();

            //check if the decription is blank, if so, display messagebox
           
                if (txtDescription.Text == "")
                {
                    MessageBox.Show("Description cannot be blank.");
                    txtDescription.Text = old_description;
                }

            //update the description in the dbo.NEWTCLIST table

            else
            {
                string newtc = txtNewtc.Text;
                string new_description = txtDescription.Text;
                
                dataObject.UpdateNewtcDescription(newtc, old_description, new_description);

                this.Dispose();

            }
        }
    }
}
