
/**********************************************************************************
Econ App Name    : CPRS

Project Name     : CPRS Interactive Screens System

Program Name     : frmImpUnlockCase.cs	    	

Programmer       : Cestine Gill

Creation Date    : 08/13/2015

Inputs           : None

Parameters       : None
                  
Outputs          : None

Description      : This screen allows uses to Unlock Cases

Detailed Design  : Improvements Unlock Case Design document 

Other            : Called from: 
 
Revision History :	
**********************************************************************************
 Modified Date   :  
 Modified By     :  
 Keyword         :  
 Change Request  :  
 Description     :  
***********************************************************************************/
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;
using CprsBLL;
using CprsDAL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace Cprs
{
    public partial class frmImpUnlockCase : Cprs.frmCprsParent
    {

        private ImpUnlockCaseData dataObject;

        public frmImpUnlockCase()
        {
            InitializeComponent();          
        }

        private void frmImpUnlockCase_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            dataObject = new ImpUnlockCaseData();

            //display the table

            GetLockedCases();

            //Disable button if table empty

            disableButton();
        }

        //Populate and formats the Unlock Case table 

        private void GetLockedCases()
        {
            try
            {
                dgLockedCases.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

                // set it to false if not needed

                dgLockedCases.RowHeadersVisible = false;
                dgLockedCases.ScrollBars = ScrollBars.Both;

                DataTable dtLockedCases = new DataTable();

                //Assign the dataObject coming in from the DAL

                dtLockedCases = dataObject.GetLockedCasesData();

                dgLockedCases.DataSource = dtLockedCases;

                /*Assign the column names and Width */

                dgLockedCases.Columns[0].HeaderText = "ID";
                dgLockedCases.Columns[0].Width = 110;
                dgLockedCases.Columns[1].HeaderText = "SURVEY DATE";
                dgLockedCases.Columns[1].Width = 110;
                dgLockedCases.Columns[2].HeaderText = "INTERVIEW";
                dgLockedCases.Columns[2].Width = 110;
                dgLockedCases.Columns[3].HeaderText = "STATE";
                dgLockedCases.Columns[3].Width = 110;
                dgLockedCases.Columns[4].HeaderText = "YRBUILT";
                dgLockedCases.Columns[4].Width = 110;
                dgLockedCases.Columns[5].HeaderText = "YRSET";
                dgLockedCases.Columns[5].Width = 110;
                dgLockedCases.Columns[6].HeaderText = "INCOME";
                dgLockedCases.Columns[6].Width = 110;
                dgLockedCases.Columns[7].HeaderText = "PROPVAL";
                dgLockedCases.Columns[7].Width = 110;
                dgLockedCases.Columns[8].HeaderText = "WEIGHT";
                dgLockedCases.Columns[8].Width = 110;
                dgLockedCases.Columns[9].HeaderText = "USER";
                dgLockedCases.Columns[9].Width = 110;
                dgLockedCases.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                //Disable Sort for all columns

                foreach (DataGridViewColumn column in dgLockedCases.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Table is empty", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Draw the greyed buttons if disabled. In order to grey the buttons,
        //The text in the button but first be removed and the button re-drawn

        private void btn_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled ? Color.DarkBlue : Color.Gray;
        }

        private void btnUnlock_Paint(object sender, PaintEventArgs e)
        {
            var btn = (Button)sender;
            var drawBrush = new SolidBrush(btn.ForeColor);
            var sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            btnUnlock.Text = string.Empty; //remove the button text
            e.Graphics.DrawString("UNLOCK", btn.Font, drawBrush, e.ClipRectangle, sf);
            drawBrush.Dispose();
            sf.Dispose();
        }

        //Disable the unlock button if there are not any locked cases

        private void disableButton()
        {
            DataTable dt = new DataTable();

            dt = dataObject.GetLockedCasesData();

            if (dt.Rows.Count == 0)
            {
                btnUnlock.Enabled = false;
            }
        }

        //Unlock the case

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            string id = dgLockedCases.CurrentRow.Cells[0].Value.ToString();

            dataObject.UnlockCase(id);

            GetLockedCases();
            
            DataTable dt1 = new DataTable();

            // Verify that the case was unlocked

            dt1 = dataObject.VerifyUnlocked(id);

            if (dt1.Rows.Count == 0)
            {               
                MessageBox.Show("The  ID " + id + " has been unlocked");
            }
          
            disableButton();
        }

        private void frmImpUnlockCase_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

    }
}
