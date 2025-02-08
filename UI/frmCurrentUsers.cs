/**************************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCurrentUsers.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2016
Inputs:             None                 
Parameters:		    None 
Outputs:		    None	
Description:	    

Detailed Design:    Detailed User Requirements for the Current Users Information Screen 
Other:	            
Revision History:	
**************************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using DGVPrinterHelper;
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmCurrentUsers : frmCprsParent
    {
        private CurrentUsersData dataObject;

        public frmCurrentUsers()
        {
            InitializeComponent();
        }

        private void frmCurrentUsers_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            dataObject = new CurrentUsersData();

            GetData();
            
            
        }

        private void GetData()
        {
            DataTable dt = dataObject.GetCurrentUsersData();
            dgUsers.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

            dgUsers.DataSource = null;
            dgUsers.DataSource = dt;
            dgUsers.RowHeadersVisible = false;

            dgUsers.Columns[0].HeaderText = "USERS";
            dgUsers.Columns[1].HeaderText = "SESSION";
            dgUsers.Columns[2].HeaderText = "TIMEIN";
            dgUsers.Columns[3].HeaderText = "CURRENT LOCATION";

            dgPrint.DataSource = null;
            dgPrint.DataSource = dt;
            dgPrint.RowHeadersVisible = false;
            dgPrint.Columns[0].HeaderText = "USERS";
            dgPrint.Columns[1].HeaderText = "SESSION";
            dgPrint.Columns[2].HeaderText = "TIMEIN";
            dgPrint.Columns[3].HeaderText = "CURRENT LOCATION";
            dgPrint.Columns[3].Width = 300;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (dgUsers.RowCount == 0)
            {
                MessageBox.Show("The Current user list is empty. Nothing to print.");
            }
            else
            {
                DGVPrinter printer = new DGVPrinter();
                printer.Title = "CURRENT USERS";

                printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
                printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
                printer.Userinfo = UserInfo.UserName;

                printer.PageNumbers = true;
                printer.PageNumberInHeader = true;

                printer.PorportionalColumns = true;
                printer.HeaderCellAlignment = StringAlignment.Near;
               
                printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
                printer.printDocument.DocumentName = "Current Users Print";
                
                printer.Footer = " ";
               
                printer.PrintDataGridViewWithoutDialog(dgPrint);
                

                Cursor.Current = Cursors.Default;               
            }
        }

        private void frmCurrentUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
