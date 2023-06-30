/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmRobocalls.cs	    	
Programmer:         Christine Zhang
Creation Date:      06/28/2023
Inputs:             None                                   
Parameters:         None                
Outputs:            None
Description:	    This program displays the ROBOCalls table 
Detailed Design:    
Other:	            Called from: Parent screen menu: Setup -> ROBOCalls
 
Revision Referrals:	
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
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;


namespace Cprs
{
    public partial class frmRobocalls : frmCprsParent
    {
        public frmRobocalls()
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

        private ROBOCallsData dataObject;

        private void frmRobocalls_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            //Get robo calls
            GetRoboCalls();
        }

        private void GetRoboCalls()
        {
            dataObject = new ROBOCallsData();

            //populate datagrid with robocalls
            dgData.DataSource = dataObject.GetRobocalls();

            dgData.Columns[0].Visible = false;
            dgData.Columns[1].HeaderText = "Month";
            dgData.Columns[2].HeaderText = "RoboCall Day";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[3].HeaderText = "User";
            dgData.Columns[4].HeaderText = "Date/Time";
            dgData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //set up buttons
            btnUpdate.Enabled = false;

            //Only HQ manager and programmer can do add, Update and Delete
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                if (dgData.RowCount > 0)
                {
                    btnUpdate.Enabled = true;
                }
            }
        }

        private void frmRobocalls_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmRoboCallsUpdatePopup Apopup = new frmRoboCallsUpdatePopup();
            Apopup.Roboid = Convert.ToInt32(dgData.CurrentRow.Cells[0].Value);
            Apopup.StartPosition = FormStartPosition.CenterParent;
            Apopup.ShowDialog();
            
            GetRoboCalls();
            
        }
    }
}
