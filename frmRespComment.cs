/**********************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmRespComment.cs	    	
Programmer      : Diane Musachio
Creation Date   : 11/04/2015
Inputs          : RESPID
Parameters      : None                
Outputs         : None

Description     : This screen will allow the user enter new comment

Detailed Design : Detailed User Requirements for Respondent Update Screen 

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
    public partial class frmRespComment : Form
    {
        private string respid;
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
        
        public frmRespComment(string rid)
        {
            InitializeComponent();
            respid = rid;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateComm addcomm = new UpdateComm();

            if (txtComment.Text != String.Empty)
            {
                addcomm.Commtext = txtComment.Text;

                addcomm.Commdate = DateTime.Now.ToString("yyyyMMdd");

                addcomm.Commtime = DateTime.Now.ToString("HHmmss");

                //get user name from environment variable
                string user_name = UserInfo.UserName;

                HistoryData uc = new HistoryData();

                uc.AddRespondentRemark(respid, user_name, addcomm.Commdate, addcomm.Commtime, addcomm.Commtext);
         
                this.Dispose();
            }
            else
            {
                MessageBox.Show("No comment to update");
                this.Dispose();
                
            }
        }

        private void cancel_click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
