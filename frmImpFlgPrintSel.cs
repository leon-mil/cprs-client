/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmImpFlagsPrintSel.cs	    	

Programmer:         Cestine Gill

Creation Date:      09/09/2015

Inputs:             None

Parameters:		    None
                 
Outputs:		    print_selection

Description:	    This is a popup screen used in the Improvements Flags Review Screen
 * which will allow the user to choose whether to print the flag counts table or the 
 * flag projects table

Detailed Design:    Detailed Design for Improvements Flag Review 

Other:	            Called from: frmImpFlagsReview
 
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

namespace Cprs
{
    public partial class frmImpFlgPrintSel : Form
    {
        public string PrintSelection = "";

        public frmImpFlgPrintSel()
        {
            InitializeComponent();
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rdbFlgCnt.Checked)
                PrintSelection = "Flag Counts";
            if (rdbProjList.Checked)
                PrintSelection = "Flag Projects";
        }

        private void rdbFlgCnt_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbFlgCnt.Checked)
                rdbProjList.Checked = false;
        }

        private void rdbProjList_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProjList.Checked)
                rdbFlgCnt.Checked = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
