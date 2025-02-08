/*********************************************************************
Econ App Name:       CPRS
Project Name:        CPRS Interactive Screens System
Program Name:        cprs.frmAddSpecialEventPopup.cs	    	
Programmer:          christine Zhang
Creation Date:       03/27/2023
Inputs:              None
Parameters:		     None.
Outputs:		     Add new event.
Description:	     This program displays the popup to add new event.
Detailed Design:     None 
Other:	             Called from: frmSpecialEvent.
 
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
    public partial class frmSpecialEventAddPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        public int Eventid;
        private int event_id;
        private SpecialEventsData dataObject;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmSpecialEventAddPopup()
        {
            InitializeComponent();
        }

        private void frmSpecialEventAddPopup_Load(object sender, EventArgs e)
        {
            event_id = Eventid;
            dataObject = new SpecialEventsData();

            if (event_id > 0)
            {
                this.Text = "Update Special Event";
                txtDescription.Text = dataObject.GetEventDescription(event_id);
            }
            else
            {
                txtDescription.Text = "";
                this.Text = "Add Special Event";
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Event Description must be entered");
                return;
            }
            if (event_id >0)
                dataObject.UpdateEvent(event_id, txtDescription.Text);
            else
                dataObject.AddEvent(txtDescription.Text);

            this.Dispose();
        }
    }
}
