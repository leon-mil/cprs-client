/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmSchedHistPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      10/16/2018
Inputs:             ID
                    
Parameters:		    None               
Outputs:		    None
Description:	    This program displays the Sched History data in table form 
                    for a selected ID from the call scheduler count screen

Detailed Design:    

Other:	            Called from: frmCallSchedulerCount.cs
 
Revision History:	
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
using DGVPrinterHelper;


namespace Cprs
{
    public partial class frmSchedHistPopup : Form
    {
        public string Id;

        private SchedCallData data_object;
        public frmSchedHistPopup()
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


        private void frmSchedHistPopup_Load(object sender, EventArgs e)
        {
            data_object = new SchedCallData();

            //Get Project History
            if (Id != "")
            {
                txtId.Text = Id;
                DataTable data_table = data_object.GetSchedHistDataByID(Id);
                dgData.DataSource = data_table;
            }

            else
            {
                MessageBox.Show("There is no ID passed");
                this.Close();
                return;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
           
                printer.Title = "CALL SCHEDULER HISTORY";
                printer.SubTitle = "ID: " + Id;
           
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
           
            printer.printDocument.DocumentName = "Sched_Call_History";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            //resize the column
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[0].Width = 100;
            dgData.Columns[1].Width = 100;
            dgData.Columns[2].Width = 100;
            dgData.Columns[3].Width = 100;
            dgData.Columns[4].Width = 100;
            dgData.Columns[5].Width = 100;
            dgData.Columns[6].Width = 100;
            printer.PrintDataGridViewWithoutDialog(dgData);

            //resize back the column
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Cursor.Current = Cursors.Default;
        }

    }
}
