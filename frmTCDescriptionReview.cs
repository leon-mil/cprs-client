/**********************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmTCDescriptionReview.cs	    	

Programmer:         Cestine Gill

Creation Date:      03/17/2016

Inputs:             None
                                     
Parameters:         None
                 
Outputs:            None

Description:	    This program displays the NEWTCList table 

Detailed Design:    Detailed Design for NEWTC Description Review

Other:	            Called from: Parent screen menu: Setup -> TC Description
 
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
    public partial class frmTCDescriptionReview : Cprs.frmCprsParent
    {
        private TCDescriptionReviewData dataObject;

        public frmTCDescriptionReview()
        {
            InitializeComponent();
            dataObject = new TCDescriptionReviewData();
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

        private void frmTCReview_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            PopulateNewtcCombo();
            DisplayNewTCTable();

            //set up buttons

            btnAddDescription.Enabled = false;
            btnUpdateDescription.Enabled = false;
            btnDelete.Enabled = false;

            //Only HQ manager and programmer can do add, Update and Delete
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                btnAddDescription.Enabled = true;
                btnUpdateDescription.Enabled = true;
                btnDelete.Enabled = true;
            }



        }

        //Populate the newtc combo box

        private void PopulateNewtcCombo()
        {
            cbNewtc.DataSource = dataObject.GetNewTCValueList();
            cbNewtc.ValueMember = "newtc";
            cbNewtc.DisplayMember = "newtc";
            cbNewtc.SelectedIndex = -1; //display a blank as the first line the drop down combobox
        }

        //Populates and formats the TC Review table 

        private void DisplayNewTCTable()
        {

            DataTable dtTCReview;

            int width = dgNewTCDesc.RowHeadersWidth;

            //populate the data grid without parameter in order to display all rows in the table

            dtTCReview = dataObject.GetNewTCTable("");

            dgNewTCDesc.DataSource = dtTCReview;

            //format the datagrid and columns

            dgNewTCDesc.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgNewTCDesc.RowHeadersVisible = false;  // set it to false if not needed
            dgNewTCDesc.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            for (int i = 0; i < dgNewTCDesc.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgNewTCDesc.Columns[i].HeaderText = "NEWTC";
                    dgNewTCDesc.Columns[i].Width = 85;
                    
                }
                if (i == 1)
                {
                    dgNewTCDesc.Columns[i].HeaderText = "DESCRIPTION";
                    dgNewTCDesc.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

                    width += dgNewTCDesc.Columns[i].Width;

                    if (width < dgNewTCDesc.Width)
                    {
                        dgNewTCDesc.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }

            }

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            DataTable dt;
            cbNewtc.SelectedIndex = -1;
            dt = dataObject.GetNewTCTable("");
            dgNewTCDesc.DataSource = dt;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchNewTC();
        }

        /* Populated the datagrid with the chosen newtc from 
         * the search when needed otherwise populate the datagrid
         * with all cases */

        private void SearchNewTC()
        {
            string newtc = "";

            DataTable dt;

            newtc = cbNewtc.Text;

            if ((newtc == " "))
            {
                MessageBox.Show("Please select a Newtc to Search on.");
            }
            else
            {
                dt = dataObject.GetNewTCTable(newtc);

                dgNewTCDesc.DataSource = dt;
            }
        }

        //Display the Add TCDescription popup.

        private void btnAddDescription_Click(object sender, EventArgs e)
        {
            frmAddTCDescriptionPopup ARpopup = new frmAddTCDescriptionPopup();
            ARpopup.ShowDialog();

            cbNewtc.SelectedIndex = -1;
            DisplayNewTCTable();
          
        }

        private void btnUpdateDescription_Click(object sender, EventArgs e)
        {
            string Newtc = dgNewTCDesc.CurrentRow.Cells[0].Value.ToString();
            string Description = dgNewTCDesc.CurrentRow.Cells[1].Value.ToString();

            frmUpdTCDescriptionPopup URpopup = new frmUpdTCDescriptionPopup(Newtc, Description);

            URpopup.ShowDialog();

            cbNewtc.SelectedIndex = -1;

            DisplayNewTCTable();
               
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

            string Newtc = dgNewTCDesc.CurrentRow.Cells[0].Value.ToString();
            string Description = dgNewTCDesc.CurrentRow.Cells[1].Value.ToString();

            //Get the count of newtc in the table. If only one left do not delete

            DataTable dt;

            dt = dataObject.GetNewTCTable(Newtc);

            if (dt.Rows.Count == 1)
            {
                MessageBox.Show("This is the last description for this NEWTC. Cannot delete.");
            }
            else
            {
                //Display the verification popup to ensure the user 
                //wants to delete the marked case

                frmVerifyDeletePopup popup = new frmVerifyDeletePopup();

                DialogResult dialogresult = popup.ShowDialog();

                if (dialogresult == DialogResult.Yes)
                {
                    //Assign the dataObject coming from the DAL
                    //Refresh the data grid display

                    dataObject.DeleteNewtcDescription(Newtc, Description);

                    cbNewtc.SelectedIndex = -1;
                   
                    DisplayNewTCTable();
                }

                popup.Dispose();
            }
        }

        private void frmTCDescriptionReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnAddDescription_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnUpdateDescription_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnDelete_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
