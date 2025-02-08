/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmAddTCDescriptionPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      3/18/2016

Inputs:             None
 
Parameters:	        None
                 
Outputs:	        None

Description:	    This screen will allow the user to add a 
 * Newtc Description to the dbo.NEWTCLIST Table

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
    public partial class frmAddTCDescriptionPopup : Form
    {
      
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

       public frmAddTCDescriptionPopup()
        {
            InitializeComponent();
            dataObject = new TCDescriptionReviewData();
        }

        private void frmAddTCDescription_Load(object sender, EventArgs e)
        {
            PopulateNewtcCombo();
        }

        //Populate the newtc combo box

        private void PopulateNewtcCombo()
        {
            cbNewtc.DataSource = dataObject.GetNewTCValueList();
            cbNewtc.ValueMember = "newtc";
            cbNewtc.DisplayMember = "newtc";
            cbNewtc.SelectedIndex = -1; //display a blank line the drop down for the first value
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            dataObject = new TCDescriptionReviewData();

            //check if the description is blank, if so, display messagebox

            if (txtDescription.Text == "" || cbNewtc.Text == "" || cbNewtc.Text == " ")
            {
                if (txtDescription.Text == "")
                {
                    MessageBox.Show("Description cannot be blank.");
                }
                else
                {
                    MessageBox.Show("Please choose a NewTC");
                }

            }

            //add the description to the dbo.NEWTCLIST table

            else
            {

                string newtc = cbNewtc.Text;
                string description = txtDescription.Text;

                dataObject.AddNewTCDescription(newtc, description);

                this.Dispose();

            }
        }
    }
}
