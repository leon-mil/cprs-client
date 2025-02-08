
/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmMySector.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/5/2016
Inputs:             None                                   
Parameters:         None               
Outputs:            None
Description:	    This program displays the mysector table 
Detailed Design:    Detailed Design for mysector
Other:	            Called from: Parent screen menu Setup -> MySector

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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using DGVPrinterHelper;
using CprsBLL;

namespace Cprs
{
    public partial class frmMySectors : frmCprsParent
    {
        private MySectorsData dataObject;

        public frmMySectors()
        {
            InitializeComponent();
        }

        private void frmMySectors_Load(object sender, EventArgs e)
        {
            dataObject = new MySectorsData();

            GetData();

            //set up buttons

            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;

            //Only HQ manager and programmer can do add, delete and edit
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }


        }

        private void GetData()
        {
            DataTable dt = dataObject.GetMySectors();
            //dgData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

            dgData.DataSource = null;
            dgData.DataSource = dt;
            dgData.RowHeadersVisible = false;

            dgData.Columns[0].HeaderText = "USERS";
            dgData.Columns[0].Width = 60;
            for (int i = 0; i < 17; i++)
            {
                if (i < 10)
                {
                    dgData.Columns[i + 1].HeaderText = "SECT0" + i;
                    dgData.Columns[i + 1].Width = 55;
                }
                else
                {
                    dgData.Columns[i + 1].HeaderText = "SECT" + i;
                    dgData.Columns[i + 1].Width = 55;
                }
            }
           
            dgData.Columns[17].Width = 55;
            dgData.Columns[18].HeaderText = "SECT19";
            dgData.Columns[18].Width = 55;
            dgData.Columns[19].HeaderText = "SECT1T";
           
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmMySectorsPopup popup = new frmMySectorsPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Username = "";
            popup.ShowDialog();

            GetData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgData.SelectedRows.Count==0)
            {
                MessageBox.Show("There is no user selected.");
                return;
            }

            frmMySectorsPopup popup = new frmMySectorsPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Username = dgData.SelectedRows[0].Cells[0].Value.ToString();
            popup.ShowDialog();

            GetData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string user = dgData.CurrentRow.Cells[0].Value.ToString();
            
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete " + user + "?", "Question", MessageBoxButtons.YesNo);
            if(dialogResult == DialogResult.Yes)
            {
                dataObject.DeleteMySectorData(user);
                GetData();

            }
            
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void PrintData()
        {
            //reduce column width to fit print page
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[0].Width = 60;
            for (int i = 0; i < 17; i++)
            {
                if (i < 10)
                  dgData.Columns[i + 1].Width = 50;
                else
                    dgData.Columns[i + 1].Width = 50;
            }
            dgData.Columns[17].Width = 55;
            Cursor.Current = Cursors.WaitCursor;
            
            DGVPrinter printer = new DGVPrinter();
            printer.Title = "HQ Sectors";
           
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
           
            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.PrintMargins.Left = 5;
            printer.PrintMargins.Right = 40;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "HQ Sector Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgData);

            //restore column width
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgData.Columns[0].Width = 60;
            for (int i = 0; i < 17; i++)
            {
                if (i < 10)
                    dgData.Columns[i + 1].Width = 55;
                else
                    dgData.Columns[i + 1].Width = 55;
            }
            dgData.Columns[17].Width = 55;
            
            Cursor.Current = Cursors.Default;
        }

    }
}
