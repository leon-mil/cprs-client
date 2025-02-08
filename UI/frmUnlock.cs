/************************************************************************
Econ App Name:      CPRS
  
Project Name:       CPRS Interactive Screens System
 
Program Name:       frmUnlock.cs	
 
Programmer:         Srini Natarajan

Creation Date:      10/05/2016
 
Inputs:             None
 
Parameters:         None
 
Outputs:            None
 
Description:	    This form displays the Current Users, Respondents and PreSamples in different tabs.
          
Detailed Design:    None
 
Other:	            Called from: Main form
Revision History:	
*********************************************************************
Modified Date   :  3/2/2023
Modified By     :  Christine Zhang
Keyword         :  
Change Request  :  
Description     : Add function to unlock tab lock
*********************************************************************/

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
    public partial class frmUnlock : frmCprsParent
    {
        private UnlockData dataObject;
        
        public frmUnlock()
        {
            InitializeComponent();
        }

        private void frmUnlock_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");
            GetDataTableUsers();
            //GetDataTableResp();
            //GetDataTablePreSamp();
            //GetDataTableSpecialcase();
            //GetDataTableDupResearch();
        }

        /* popupalate Current users data grid */
        private void GetDataTableUsers()
        {
            dataObject = new UnlockData();
            dgUsers.DataSource = dataObject.GetCurrentUsersData();
            dgUsers.RowHeadersVisible = false;
            dgUsers.Columns[0].HeaderText = "USER";
            dgUsers.Columns[0].Width = 550;
            dgUsers.Columns[1].HeaderText = "LAST TIME IN";
            dgUsers.Columns[1].Width = 560;
        }

        /* popupalate Respondent data grid */
        private void GetDataTableResp()
        {
            dataObject = new UnlockData();
            dgResp.DataSource = dataObject.GetRespondentData();
            dgResp.RowHeadersVisible = false;
            dgResp.Columns[0].HeaderText = "RESPID";
            dgResp.Columns[0].Width = 150;
            dgResp.Columns[1].HeaderText = "ORGANIZATION";
            dgResp.Columns[1].Width = 525;
            dgResp.Columns[2].HeaderText = "CONTACT";
            dgResp.Columns[2].Width = 260;
            dgResp.Columns[3].HeaderText = "LOCKED BY";
            dgResp.Columns[3].Width = 180;
        }

        /* popupalate Presample data grid */
        private void GetDataTablePreSamp()
        {
            dataObject = new UnlockData();
            dgSample.DataSource = dataObject.GetPreSampleData(); 
            dgSample.RowHeadersVisible = false;
            dgSample.Columns[0].HeaderText = "ID";
            dgSample.Columns[0].Width = 115;
            dgSample.Columns[1].HeaderText = "PSU";
            dgSample.Columns[1].Width = 115;
            dgSample.Columns[2].HeaderText = "BPOID";
            dgSample.Columns[2].Width = 125;
            dgSample.Columns[3].HeaderText = "SCHED";
            dgSample.Columns[3].Width = 125;
            dgSample.Columns[4].HeaderText = "PROJECT DESCRIPTION";
            dgSample.Columns[4].Width = 510;
            dgSample.Columns[5].HeaderText = "LOCKED BY";
            dgSample.Columns[5].Width = 125;
        }

        private void GetDataTableSpecialcase()
        {
            dataObject = new UnlockData();
            dgSpecial.DataSource = dataObject.GetSpecialcaseData();
            dgSpecial.RowHeadersVisible = false;
            dgSpecial.Columns[0].HeaderText = "FIN";
            dgSpecial.Columns[0].Width = 180;
            dgSpecial.Columns[1].HeaderText = "PSU";
            dgSpecial.Columns[1].Width = 100;
            dgSpecial.Columns[2].HeaderText = "BPOID";
            dgSpecial.Columns[2].Width = 100;
            dgSpecial.Columns[3].HeaderText = "SCHED";
            dgSpecial.Columns[3].Width = 100;
            dgSpecial.Columns[4].HeaderText = "PROJECT DESCRIPTION";
            dgSpecial.Columns[4].Width = 510;
            dgSpecial.Columns[5].HeaderText = "LOCKED BY";
            dgSpecial.Columns[5].Width = 125;
        }

        private void GetDataTableDupResearch()
        {
            dataObject = new UnlockData();
            dgDup.DataSource = dataObject.GetDupResearchData();
            dgDup.RowHeadersVisible = false;
            dgDup.Columns[0].HeaderText = "FIN";
            dgDup.Columns[0].Width = 180;
            dgDup.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgDup.Columns[1].HeaderText = "FIPSTATE";
            dgDup.Columns[1].Width = 100;
            dgDup.Columns[2].HeaderText = "COUNTY";
            dgDup.Columns[2].Width = 100;
            dgDup.Columns[3].HeaderText = "OWNER";
            dgDup.Columns[3].Width = 100;
            dgDup.Columns[4].HeaderText = "PROJSELV";
            dgDup.Columns[4].Width = 100;
            dgDup.Columns[5].HeaderText = "SELDATE";
            dgDup.Columns[5].Width = 100;
            dgDup.Columns[6].HeaderText = "NEWTC";
            dgDup.Columns[6].Width = 100;
            dgDup.Columns[7].HeaderText = "LOCKED BY";
            dgDup.Columns[7].Width = 125;
        }

        /* popupalate Current users data grid */
        private void GetDataTableTab()
        {
            dataObject = new UnlockData();
            dgTab.DataSource = dataObject.GetTablockData();
            dgTab.RowHeadersVisible = false;
            dgTab.Columns[0].HeaderText = "TC";
            dgTab.Columns[0].Width = 550;
            dgTab.Columns[1].HeaderText = "USER";
            dgTab.Columns[1].Width = 560;
        }

        private void frmUnlock_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        /* Unlock the record or delete user lock depending on the tab selected */
        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (tbResearch.SelectedTab == tbUsers)
            {
                Unlock_User();
            }
            else if (tbResearch.SelectedTab == tbResp)
            {
                UnLock_RespLock();
            }
            else if (tbResearch.SelectedTab == tbPreSamp)
            {
                UnLock_PreSamp();
            }
            else if (tbResearch.SelectedTab == tbSpecialcase)
            {
                UnLock_Specialcase();
            }
            else if (tbResearch.SelectedTab == tbTab)
            {
                UnLock_Tablock();
            }
            else
                UnLock_Dup();
        }

        private void Unlock_User()
        {
            //check if a user is selected before unlocking.
            if (dgUsers.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to unlock this User?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    string UserId;
                    int selectedrowindex = dgUsers.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgUsers.Rows[selectedrowindex];
                    UserId = Convert.ToString(selectedRow.Cells["USRNME"].Value);
                    dataObject.DeleteCurrUser(UserId);
                    dataObject.UpdateRespondentLock(UserId);
                    dataObject.UpdatePreSampleLock(UserId);
                    dataObject.UpdateSpecialcaseLock(UserId);
                    dataObject.UpdateDupResearchLock(UserId);
                    GetDataTableUsers();
                }
            }
            else
            {
                MessageBox.Show("You must select a user to unlock");
            }        
        }

        //Refreshing as you select the tab
        private void UnLock_RespLock()
        {
            //check if a respid is selected before unlocking.
            if (dgResp.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to unlock this Respid?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    string respId;
                    int selectedrowindex = dgResp.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgResp.Rows[selectedrowindex];
                    respId = Convert.ToString(selectedRow.Cells["RESPID"].Value);
                    dataObject.ClearRespIDLock(respId);

                    GetDataTableResp();
                }
            }
            else
            {
                MessageBox.Show("You must select a Respid to unlock");
            }
        }

        private void UnLock_PreSamp()
        {
            //check if a Presample is selected before unlocking.
            if (dgSample.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to unlock this PreSample Case?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    string SampId;
                    int selectedrowindex = dgSample.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgSample.Rows[selectedrowindex];
                    SampId = Convert.ToString(selectedRow.Cells["ID"].Value);
                    dataObject.ClearPreSampLock(SampId);
                    GetDataTablePreSamp();
                }
            }
            else
            {
                MessageBox.Show("You must select a PreSample case to unlock");
            }
        }

        private void UnLock_Specialcase()
        {
            //check if a Presample is selected before unlocking.
            if (dgSpecial.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to unlock this Special Case?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    string fin;
                    int selectedrowindex = dgSpecial.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgSpecial.Rows[selectedrowindex];
                    fin = Convert.ToString(selectedRow.Cells["FIN"].Value);
                    dataObject.ClearSpecialcaseLock(fin);
                    GetDataTableSpecialcase();
                }
            }
            else
            {
                MessageBox.Show("You must select a Special case to unlock");
            }
        }

        private void UnLock_Dup()
        {
            //check if a research is selected before unlocking.
            if (dgDup.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to unlock this duplicate research?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    int selectedrowindex = dgDup.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgDup.Rows[selectedrowindex];
                    string lockid = Convert.ToString(selectedRow.Cells["LOCKID"].Value);
                    dataObject.UpdateDupResearchLock(lockid);
                    GetDataTableDupResearch();
                }
            }
            else
            {
                MessageBox.Show("You must select a case to unlock");
            }
        }

        private void UnLock_Tablock()
        {
            //check if a user is selected before unlocking.
            if (dgTab.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to unlock this TC?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    string tc;
                    int selectedrowindex = dgTab.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgTab.Rows[selectedrowindex];
                    tc = Convert.ToString(selectedRow.Cells["TC2"].Value);
                    
                    dataObject.ClearTabLock(tc);

                    GetDataTableTab();
                }
            }
            else
            {
                MessageBox.Show("You must select a TC to unlock");
            }
        }

        private void tbUnlock_Click(object sender, EventArgs e)
        {
            if (tbResearch.SelectedTab == tbUsers)
            {
                GetDataTableUsers();
            }
            else if (tbResearch.SelectedTab == tbResp)
            {
                GetDataTableResp();
            }
            else if (tbResearch.SelectedTab == tbPreSamp)
            {
                GetDataTablePreSamp();
            }
            else if (tbResearch.SelectedTab == tbSpecialcase)
            {
                GetDataTableSpecialcase();
            }
            else if (tbResearch.SelectedTab == tbTab)
            {
                GetDataTableTab();
            }
            else
                GetDataTableDupResearch();
        }
    }
}
