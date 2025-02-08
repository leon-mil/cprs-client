/**************************************************************************************
Econ App Name:      CPRS Interactive Screens
Project Name:       CPRS Interactive Screens 
Program Name:       frmDisplayNotesPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/15/2021
Inputs:             title, content
Parameters:	        n/a
Outputs:	        Display Full  Dodge Slip Notes information	
Description:	    Displays information from passed content
Detailed Design:    CR 160
Other:	            Called by: frmSlipDisplay.cs
Revision History:	
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using CprsBLL;

namespace Cprs
{
    public partial class frmDisplayNotesPopup : Form
    {
        public frmDisplayNotesPopup()
        {
            InitializeComponent();
        }
        public string title = string.Empty;
        public string content = string.Empty;
        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCP = base.CreateParams;
                myCP.ClassStyle = myCP.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCP;
            }
        }

        private void frmDisplayNotesPopup_Load(object sender, EventArgs e)
        {
            txtContent.Text = content;
            this.Text = title;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    
}
