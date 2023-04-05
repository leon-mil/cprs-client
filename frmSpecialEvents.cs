/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmSpecialEvents.cs	    	
Programmer:         Christine Zhang
Creation Date:      03/27/2023
Inputs:             None                                   
Parameters:         None                
Outputs:            None
Description:	    This program displays the SPECIAL_EVENTS table 
Detailed Design:    
Other:	            Called from: Parent screen menu: Setup -> Special Events
 
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
    public partial class frmSpecialEvents : frmCprsParent
    {
        private SpecialEventsData dataObject;

        public frmSpecialEvents()
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

        private void frmSpecialEvents_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            //Get special events
            GetSpecialEvents();

            
        }

        private void GetSpecialEvents()
        {
            dataObject = new SpecialEventsData();

            //populate datagrid with npc users
            dgData.DataSource = dataObject.GetEvents();

            dgData.Columns[0].Visible = false;
            dgData.Columns[1].HeaderText = "EVENT DESCRIPTION";
            dgData.Columns[2].HeaderText = "USER";
            dgData.Columns[3].HeaderText = "Date/Time";
            dgData.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //set up buttons

            btnAdd.Enabled = false;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;

            //Only HQ manager and programmer can do add, Update and Delete
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                btnAdd.Enabled = true;
                if (dgData.RowCount > 0)
                {
                    btnUpdate.Enabled = true;
                    btnDelete.Enabled = true;
                }
                if (dgData.RowCount >= 4)
                    btnAdd.Enabled = false;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmSpecialEventAddPopup Apopup = new frmSpecialEventAddPopup();
            Apopup.Eventid = -1;
            Apopup.StartPosition = FormStartPosition.CenterParent; 
            Apopup.ShowDialog();
            if (Apopup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            else
            {
                GetSpecialEvents();
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            frmSpecialEventAddPopup Apopup = new frmSpecialEventAddPopup();
            Apopup.Eventid = Convert.ToInt32(dgData.CurrentRow.Cells[0].Value);
            Apopup.StartPosition = FormStartPosition.CenterParent;
            Apopup.ShowDialog();
            if (Apopup.DialogResult == System.Windows.Forms.DialogResult.Cancel)
                return;
            else
            {
                GetSpecialEvents();
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int eventid = Convert.ToInt32(dgData.CurrentRow.Cells[0].Value);

            DialogResult dialogResult = MessageBox.Show("Do you want to delete the event?", "Question?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                dataObject.DeleteEvent(eventid);
                GetSpecialEvents();
            }
            
        }

        private void frmSpecialEvents_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }
    }
}
