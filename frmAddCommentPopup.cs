/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmAddCommentPopup.cs	    	

Programmer:         Cestine Gill

Creation Date:      08/25/2015

Inputs:             None

Parameters:		    None
                 
Outputs:		    COMMTEXT

Description:	    This screen will allow the user to add either project or respondent
 *                  comments to the dbo.RESP_PROJ_COMMENTS table

Detailed Design:    Detailed User Requirements for History Display Screen 

Other:	            Called from: frmHistory.cs
 
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
    public partial class frmAddCommentPopup : Form
    {
        private string Id;
        private string Respid;

        private const int CP_NOCLOSE_BUTTON = 0x200;

        public string AddType = "";

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        //Carry over the values for Id and Respid from frmHistory

        public frmAddCommentPopup(string id, string respid)
        {
            InitializeComponent();
            Id = id;
            Respid = respid;
        }

        private void frmAddCommentPopup_Load(object sender, EventArgs e)
        {
            displayGrpbox();
        }

        private void displayGrpbox()
        {
            //If the respid value is blank then it matches id. The value
            //for respid is blanked out in frmHistory when it is the same
            //as id

            //if respid is blank, then the user can only add a project comment
            //the id in the project_comment table will be populated 
            //by the value for id. If the id and respid are not equal
            //then allow the user to choose whether they want to add either a 
            //respondent comment or a project comment

            if (Respid == Id)
                gbChooseType.Visible = false;
            else
            {
                gbChooseType.Visible = true;
                if (Id == string.Empty && Respid !=string.Empty)
                {
                    rbtnProject.Checked = false;
                    rbtnProject.Enabled = false;
                    rbtnRespondent.Checked = true;
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            HistoryData history = new HistoryData();

            if (txtRemark.Text == "")
            {
                MessageBox.Show("Comment cannot be blank");
                this.Dispose();
            }
            else
            {
                string new_comment = "";
                if (txtRemark.Text != "")
                    new_comment = txtRemark.Text;

                if (new_comment != "")
                {
                    string Commdate = DateTime.Now.ToString("yyyyMMdd");
                    string Commtime = DateTime.Now.ToString("HHmmss");
                    string Commtext = new_comment;
                    string Usrnme = UserInfo.UserName;
                   
                    //check if the project radio button is chosen. Or whether the
                    //groupbox is visible. If so, populate the 
                    //id field with the id.
                    //If the respondent radio button is chosen, populate the 
                    //respid fields with the respid

                    if (rbtnProject.Checked || gbChooseType.Visible == false)
                    {
                        history.AddProjectRemark(Id, Usrnme, Commdate, Commtime, Commtext);
                        AddType = "Proj";
                    }
                    else
                    {
                        history.AddRespondentRemark(Respid, Usrnme, Commdate, Commtime, Commtext);
                        AddType = "Resp";
                    }

                }

                this.Dispose();
            }

        }

    }

}
