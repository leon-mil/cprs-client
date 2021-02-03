/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmCCMail.cs	    	

Programmer:         Cestine Gill

Creation Date:      02/22/2016

Inputs:             None
                                     
Parameters:         None
                 
Outputs:            None

Description:	    This program displays the CCMail table 

Detailed Design:    Detailed Design for CCMail

Other:	            Called from: Parent screen menu Setup -> CCMail
 
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
    public partial class frmCCMail : Cprs.frmCprsParent
    {

        private CCMailData dataObject;

        public frmCCMail()
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

        private void frmCCMail_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            dataObject = new CCMailData();

            CreateDataSource();

            cbJobFlag.SelectedIndex = -1;
            LoadTables();

            //set up buttons

            btnAddCCMail.Enabled = false;
            btnDeleteCCMail.Enabled = false;

            //Only HQ manager and programmer can do add and delete 
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                btnAddCCMail.Enabled = true;
                btnDeleteCCMail.Enabled = true;
            }

        }

        //Create the Datasource to be used in the Job Flag Combo Box

        private void CreateDataSource()
        {

            BindingSource source = new BindingSource();
            List<string> dataSource = new List<string>();

            dataSource.Add("DSMP - DCP SAMPLE/LOAD");
            dataSource.Add("DFRM - DCP FORMS ");
            dataSource.Add("MPRE - MF PRESAMPLE");
            dataSource.Add("MSMP - MF SAMPLE/FORMS");
            dataSource.Add("MWGT - MF WEIGHT");
            dataSource.Add("FVIP - FEDERAL VIP");
            dataSource.Add("PTAB - PRELIM  TABULATION");
            dataSource.Add("FTAB - FINAL TABULATION");
            dataSource.Add("TSAR - TSAR PROCESS");
            dataSource.Add("CFRM - CONTINUING FORMS");
            dataSource.Add("ROLL - ROLLOVER");
            dataSource.Add("CLOD - CE SAMPLE/LOAD");
            dataSource.Add("CTAB - CE TABULATIONS");
            dataSource.Add("CFOR - CE FORECASTING");
            dataSource.Add("CVIP - CE LOAD VIP");
            dataSource.Add("USR1 - USER EDIT");
            dataSource.Add("USR2 - USER REVIEW");
            dataSource.Add("USR3 - AUDIT REVIEW");
            dataSource.Add("CENT - CENTURION LOAD");
            dataSource.Add("BLST - EMAIL BLAST");

            cbJobFlag.DataSource = dataSource;

        }

        private void LoadTables()
        {
            dataObject = new CCMailData();
            GetCCMail();
        }

        // Sets the ToolTip text for cells in the Rating column.
        void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if ((e.ColumnIndex == this.dgCCMail.Columns["JOBFLAG"].Index)
                && e.Value != null)
            {
                DataGridViewCell cell =
                    this.dgCCMail.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.Value.Equals("DSMP"))
                {
                    cell.ToolTipText = "DCP SAMPLE/LOAD";
                }
                else if (e.Value.Equals("DFRM"))
                {
                    cell.ToolTipText = "DCP FORMS";
                }
                else if (e.Value.Equals("MPRE"))
                {
                    cell.ToolTipText = "MF PRESAMPLE";
                }
                else if (e.Value.Equals("MSMP"))
                {
                    cell.ToolTipText = "MF SAMPLE/FORMS";
                }
                else if (e.Value.Equals("MWGT"))
                {
                    cell.ToolTipText = "MF WEIGHT";
                }
                else if (e.Value.Equals("TSAR"))
                {
                    cell.ToolTipText = "TSAR PROCESS";
                }
                else if (e.Value.Equals("PTAB"))
                {
                    cell.ToolTipText = "PRELIM TABULATION";
                }
                else if (e.Value.Equals("FTAB"))
                {
                    cell.ToolTipText = "FINAL TABULATION";
                }
                else if (e.Value.Equals("CFRM"))
                {
                    cell.ToolTipText = "CONTINUING FORMS";
                }
                else if (e.Value.Equals("ROLL"))
                {
                    cell.ToolTipText = "ROLLOVER";
                }
                else if (e.Value.Equals("CLOD"))
                {
                    cell.ToolTipText = "CE SAMPLE/LOAD";
                }
                else if (e.Value.Equals("CTAB"))
                {
                    cell.ToolTipText = "CE TABULATIONS";
                }
                else if (e.Value.Equals("CFOR"))
                {
                    cell.ToolTipText = "CE FORECASTING";
                } 
                else if (e.Value.Equals("CVIP"))
                {
                    cell.ToolTipText = "CE LOAD VIP";
                }
                else if (e.Value.Equals("USR1"))
                {
                    cell.ToolTipText = "USER EDIT";
                }
                else if (e.Value.Equals("USR2"))
                {
                    cell.ToolTipText = "USER REVIEW";
                }
                else if (e.Value.Equals("USR3"))
                {
                    cell.ToolTipText = "AUDIT REVIEW";
                }
            }
        }
 
        //Populates and formats the CCMail table 

        private void GetCCMail()
        {
            DataTable dtCCMail;

            //populate the data grid

            dtCCMail = dataObject.GetCCMailTable("");

            dgCCMail.DataSource = dtCCMail;

            //format the datagrid and columns

            dgCCMail.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgCCMail.RowHeadersVisible = false;  // set it to false if not needed
            dgCCMail.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            

            for (int i = 0; i < dgCCMail.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgCCMail.Columns[i].HeaderText = "JOBFLAG";
                    dgCCMail.Columns[i].Width = 60;
                }
                if (i == 1)
                {
                    dgCCMail.Columns[i].HeaderText = "EMAIL";
                    dgCCMail.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
                if (i == 2)
                {
                    dgCCMail.Columns[i].HeaderText = "USER";
                    dgCCMail.Columns[i].Width = 60;
                }
                if (i == 3)
                {
                    dgCCMail.Columns[i].HeaderText = "DATE";
                    dgCCMail.Columns[i].Width = 120;
                }
            }

            if (dtCCMail.Rows.Count == 0)
            {
                btnDeleteCCMail.Enabled = false;
            }
            else
                btnDeleteCCMail.Enabled = true;
        }

  
        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchJobFlag();
        }

        /* Search the table by Job Flag, if necessary. Display the job flag selected otherwise display all the rows */

        private void SearchJobFlag()
        {
            string jobflag = "";

            DataTable dt;

            if (cbJobFlag.Text.ToString() == "")
                jobflag = "";
            else
                jobflag = cbJobFlag.Text.Substring(0, cbJobFlag.Text.IndexOf(" "));

            if ((jobflag == ""))
            {
                dt = dataObject.GetCCMailTable("");
                MessageBox.Show("Please select a jobflag.");
            }
            else
                dt = dataObject.GetCCMailTable(jobflag);

            dgCCMail.DataSource = dt;

            Cursor.Current = Cursors.Default;

            if (dt.Rows.Count == 0)
            {
                MessageBox.Show("No data to display.");
            }

        }

        //Display the Add CCMail popup.

        private void btnAddCCMail_Click(object sender, EventArgs e)
        {
            frmAddCCMailPopup ARpopup = new frmAddCCMailPopup();
            ARpopup.ShowDialog();

            LoadTables();
        }

        private void btnDeleteCCMail_Click(object sender, EventArgs e)
        {

            DataTable dt;
            dt = dataObject.GetCCMailTable("");

            if (dt.Rows.Count != 0)
            {

                string jobflag = dgCCMail.CurrentRow.Cells[0].Value.ToString();
                string email   = dgCCMail.CurrentRow.Cells[1].Value.ToString();

                //Display the verification popup to ensure the user 
                //wants to delete the marked case

                frmVerifyDeletePopup popup = new frmVerifyDeletePopup();

                DialogResult dialogresult = popup.ShowDialog();

                if (dialogresult == DialogResult.Yes)
                {

                    //Assign the dataObject coming from the DAL
                    //Refresh the data grid display

                    dataObject.DeleteCCMailRow(jobflag, email);

                    //re-load only the table

                    dt = dataObject.GetCCMailTable("");

                    dgCCMail.DataSource = dt;

                    if (dt.Rows.Count == 0)
                    {
                        btnDeleteCCMail.Enabled = false;
                    }
                }

                popup.Dispose();

            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            DataTable dt;

            cbJobFlag.SelectedIndex = -1;
            dt = dataObject.GetCCMailTable("");

            dgCCMail.DataSource = dt;
            dgCCMail.Update();
        }

        private void frmCCMail_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }
  
    }
}
