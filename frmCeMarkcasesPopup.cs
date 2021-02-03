
/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCeMarkcasesPopup.cs	    	
Programmer:         Christine
Creation Date:      09/7/2016
Inputs:             ID    
Parameters:		    None            
Outputs:		    None
Description:	    This program displays the mark cases data in table form 
                    for a selected ID 
Detailed Design:    Detailed User Requirements for Display Screen 
Other:	            Called from: frmImprovement.cs
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
    public partial class frmCeMarkcasesPopup : Form
    {
        public string Id;

        public frmCeMarkcasesPopup()
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

        private void frmCeMarkcasesPopup_Load(object sender, EventArgs e)
        {
            txtId.Text = Id;
            
            LoadTables();
        }

        private void LoadTables()
        {
            CemarkData cmd = new CemarkData();
           
            //Get cemarks

            DataTable dtProj = new DataTable();

            dtProj = cmd.GetCemarklist(Id);

            dgMark.DataSource = dtProj;

            dgMark.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgMark.RowHeadersVisible = false;  // set it to false if not needed

            foreach (DataGridViewColumn column in dgMark.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            for (int i = 0; i < dgMark.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgMark.Columns[i].HeaderText = "DATE/TIME";
                    dgMark.Columns[i].Width = 140;

                }
                if (i == 1)
                {
                    dgMark.Columns[i].HeaderText = "USER";
                    dgMark.Columns[i].Width = 80;

                }
                if (i == 2)
                {
                    dgMark.Columns[i].HeaderText = "MARK NOTE";
                    dgMark.Columns[i].Width = 910;
                    dgMark.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

                }
                
            }

            
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCemarkPopup popup = new frmCemarkPopup(Id);
            popup.ShowDialog();

            LoadTables();
           
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Mark Notes";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            //printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Mark Notes Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            dgMark.Columns[2].Width = 600;

            printer.PrintDataGridViewWithoutDialog(dgMark);
            dgMark.Columns[2].Width = 910;


            Cursor.Current = Cursors.Default;
        }


    }
}
