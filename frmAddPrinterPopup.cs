/*********************************************************************
Econ App Name:       CPRS

Project Name:        CPRS Interactive Screens System
 
Program Name:        cprs.frmAddPrinterPopup.cs	    	

Programmer:          Srini Natarajan

Creation Date:       09/27/2015

Inputs:              None

Parameters:		     None.

Outputs:		     Add new printer.

Description:	     This program displays the popup to add new printers.

Detailed Design:     None 

Other:	             Called from: frmPrinters.
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;

namespace Cprs
{
    public partial class frmAddPrinterPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;
        private PrinterData printerobject;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        public frmAddPrinterPopup()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtPrinterName.Text == "")
            {
                MessageBox.Show("Printer Name cannot be empty!");
                return;
            }

            //AddNewPrinter
            string location = string.Empty;
            if (rdHQ.Checked == true)
            {
                location = "HQ";
            }
            else
            {
                location = "NPC";
            }
            string name = txtPrinterName.Text;
            printerobject = new PrinterData();

            //Check if this printer already exists for this location
            if (printerobject.CheckPrinterExist(name, location))
            {
                MessageBox.Show("The printer already exists!");
                txtPrinterName.Text = "";
                txtPrinterName.Focus();
                return;
            }
            printerobject.AddNewPrinter(location, name);
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
