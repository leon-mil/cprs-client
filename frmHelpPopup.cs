/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmHelpPopup.cs	    	
Programmer:         Diane Musachio
Creation Date:      7/11/2017
Inputs:             title, filename
Parameters:	        n/a
Outputs:	        Description help screen 	
Description:	    This screen displays information from text file
Detailed Design:    None 
Other:	            Called by: frmFlagTypeReport.cs
Revision History:	
****************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
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
    public partial class frmHelpPopup : Form
    {

        public frmHelpPopup()
        {
            InitializeComponent();
        }

        public string filename = string.Empty;
        public string title = string.Empty;

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

        private void frmTextPopup_Load(object sender, EventArgs e)
        {          
            string filepath = GlobalVars.HelpDir;
           
            filename = filepath + filename;
            lblHelp.Text = title;
            txtContent.Text = File.ReadAllText(filename);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
