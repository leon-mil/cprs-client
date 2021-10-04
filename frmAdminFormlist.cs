/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmAdminFormlist.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/28/2021
Inputs:             None                                  
Parameters:         None                 
Outputs:            None
Description:	    This program displays the Form list table 
Detailed Design:    Detailed Design for form list
Other:	            Called from: Parent screen menu Admin -> Formlist
 
Revision:	
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
    public partial class frmAdminFormlist : frmCprsParent
    {
        public frmAdminFormlist()
        {
            InitializeComponent();
        }

        private FormListData dataObject;

        private void frmAdminFormlist_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            dataObject = new FormListData();
            GetData();

            //set up buttons

            btnAdd.Enabled = false;
            btnDelete.Enabled = false;

            //Only HQ manager and programmer can do add and delete 
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer || UserInfo.GroupCode == EnumGroups.HQAnalyst || UserInfo.GroupCode== EnumGroups.HQSupport)
            {
                btnAdd.Enabled = true;
                if (dgData.Rows.Count > 0)
                    btnDelete.Enabled = true;
            }
            
        }

        private void GetData()
        {
            DataTable dt = dataObject.GetFormlist();
            dgData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgData.DataSource = null;
            dgData.DataSource = dt;
            dgData.RowHeadersVisible = false;

            dgData.Columns[0].HeaderText = "RESPID";
            dgData.Columns[0].Width = 150;
            dgData.Columns[1].HeaderText = "RESPONDENT ORGANIZATION";
            dgData.Columns[1].Width = 400;
            dgData.Columns[2].HeaderText = "USER";
            dgData.Columns[2].Width = 100;
            dgData.Columns[3].HeaderText = "DATE/TIME";
            dgData.Columns[3].Width = 150;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmRespidPopup popup = new frmRespidPopup(false, false, true);
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();  //show child

            //if the popup was cancelled, set status to old value
            if (popup.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string respid = popup.NewRespid;
                if (!dataObject.CheckExistinFormlist(respid))
                {
                    //Add to database
                    dataObject.AddFormlist(respid);
                    GetData();
                    if (dgData.Rows.Count > 0 && !btnDelete.Enabled )
                        btnDelete.Enabled = true;
                }
                else
                {
                    MessageBox.Show("The respid entered already exists in the form list table.");
                }

            }
            popup.Dispose();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           string respid = dgData.CurrentRow.Cells[0].Value.ToString();  
           DialogResult dr = MessageBox.Show("Are you sure you want to delete Respid " + respid + " ?",
                      "Question", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                //delete from database 
                dataObject.DeleteFormlist(respid);
                GetData();

                if (dgData.Rows.Count == 0)
                    btnDelete.Enabled = false;
            }
        }
    }
}
