/*********************************************************************
Econ App Name:       CPRS
Project Name:        CPRS Interactive Screens System
Program Name:        cprs.frmRoboCallsUpdatePopup.cs	    	
Programmer:          christine Zhang
Creation Date:       06/28/2023
Inputs:              None
Parameters:		     roboid.
Outputs:		     new day for selected row
Description:	     This program displays the popup to update day.
Detailed Design:     None 
Other:	             Called from: frmRoboCalls.
 
Revision History:	
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
    public partial class frmRoboCallsUpdatePopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        public int Roboid;

        private ROBOCallsData dataObject;

        public frmRoboCallsUpdatePopup()
        {
            InitializeComponent();
        }

      
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("Please select a day.");
                comboBox1.Focus();
            }

            else
            {
                dataObject.UpdateRobocallday(Roboid, comboBox1.Text);
                this.Dispose();
            }
        }

        private void frmRoboCallsUpdatePopup_Load(object sender, EventArgs e)
        {
            dataObject = new ROBOCallsData();
        }
    }
}
