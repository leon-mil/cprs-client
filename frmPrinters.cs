/*********************************************************************
Econ App Name:       CPRS

Project Name:        CPRS Interactive Screens System
 
Program Name:        cprs.frmPrinters.cs	    	

Programmer:          Srini Natarajan

Creation Date:       09/27/2015

Inputs:              None

Parameters:		     None

Outputs:		     List of available Printers.

Description:	     This program displays the list of Printers from Printers table and 
                     allows the user to add or delete printers.

Detailed Design:     None 

 Other:	            Called from: Main form.
 
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
using DGVPrinterHelper;
using System.Collections;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmPrinters : Cprs.frmCprsParent
    {
        private PrinterData dataObject;

        public frmPrinters()
        {
            InitializeComponent();
        }

        private void frmPrinters_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
            //get the data from Printers table.
            dataObject = new PrinterData();
            GetDataTable1();   
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmAddPrinterPopup ARpopup = new frmAddPrinterPopup();
            ARpopup.ShowDialog();
            FormLoad();
        }

        private DataTable GetDataTable1()
        {
            DataTable dt = dataObject.GetPrinterslistData();
            dgData.DataSource = dt;
            this.dgData.Columns["PRINTERID"].Visible = false;
            dgData.Columns[1].HeaderText = "LOCATION";
            dgData.Columns[1].Width = 80;
            dgData.Columns[2].HeaderText = "PRINTER NAME";
            return dt;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //check if a printer is selected before deleting.
            if (dgData.SelectedRows.Count > 0)
            {
                //check before deleting
                DialogResult UserSel = MessageBox.Show("Are you sure you want to delete this printer?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (UserSel == DialogResult.Yes)
                {
                    string PrntrId;
                    int selectedrowindex = dgData.SelectedCells[0].RowIndex;
                    DataGridViewRow selectedRow = dgData.Rows[selectedrowindex];
                    PrntrId = Convert.ToString(selectedRow.Cells["PRINTERID"].Value);
                    dataObject.DeletePrinter(PrntrId);
                    FormLoad();
                }
            }
            else
            { 
                MessageBox.Show("You have to select a printer to delete");
                return;
            }
        }        
    }
}
