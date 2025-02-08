
/**********************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmRespidPopup.cs	    	
Programmer      : Diane Musachio
Creation Date   : 08/27/2015
Inputs          : 
                
Parameters      : None                
Outputs         : New respid

Description     : This screen will allow the user enter new RESPID

Detailed Design : Detailed User Requirements for Improvement Screen 

Other           : Called from: frmRespAddrUpdate.cs
 
Revision History:	
***********************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
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
    public partial class frmRevCompMsgbox : Form
    {
        public string RevCompRtn = "";

        //private bool checkid = false;cc
        public frmRevCompMsgbox()
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

        private void btnYes_Click(object sender, EventArgs e)
        {
            RevCompRtn = "Yes";
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            RevCompRtn = "Cancel";
            this.Close();
        }

        private void frmRevCompMsgbox_Load(object sender, EventArgs e)
        {

        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            RevCompRtn = "No";
            this.Close();
        }
    }
}
