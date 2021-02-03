/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmFlagTypeReport.cs	    	
Programmer:         Christine Zhang/Diane Musachio
Creation Date:      5/24/2017
Inputs:             None
Parameters:	        None 
Outputs:	        Flag Type Report load 	
Description:	    This screen displays tabulated data from Data_FLAGs
Detailed Design:    None 
Other:	            Called by: Main form 
Revision History:	
****************************************************************************************
 Modified Date :  9/30/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR#3591
 Description   :  Add data flag 47, 48
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using CprsBLL;
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;
using System.Reflection;

namespace Cprs
{
    public partial class frmFlagTypeReport : frmCprsParent
    {
        public frmFlagTypeReport()
        {
            InitializeComponent();
        }

        private MySector ms;
        private FlagReportData data_object;
        List<string> dy = new List<string>();
        private int ftype = 0;
        private string newtcs;
        private string flagLocation;

        private void frmFlagTypeReport_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            //get user role and determine what radio buttons are visible
            if (UserInfo.GroupCode == EnumGroups.Programmer ||
                UserInfo.GroupCode == EnumGroups.HQManager ||
                UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                UserInfo.GroupCode == EnumGroups.HQSupport ||
                UserInfo.GroupCode == EnumGroups.HQTester)
            {
                ftype = 0;
                rdHq.Visible = true;
                rdNpc.Visible = true;
                rdHq.Checked = true;

                // get my sector data
                MySectorsData md = new MySectorsData();
                ms = md.GetMySectorData(UserInfo.UserName);

                //if user does not exist in sectors dataset then user will
                //not have access to sector checkbox
                if (ms == null)
                    ckMySec.Visible = false;
                else
                {
                    ckMySec.Visible = true;
                }

            }
            else if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                     UserInfo.GroupCode == EnumGroups.NPCLead) 
            {
                ftype = 1;
                rdHq.Visible = false;
                rdNpc.Visible = false;
                ckMySec.Visible = false;
                lblType.Visible = false;
            }
       
            newtcs = "";
            GetData();
        }

        private void GetData()
        {
            dgData.DataSource = null;
            data_object = new FlagReportData();
            DataTable dt = data_object.GetFlagReportData(ftype, newtcs);
            dgData.DataSource = dt;
            dgPrint.DataSource = dt;

            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgPrint.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        //refresh data
        public void RefreshData()
        {
            GetData();
        }

        //want reject lines to have red text
        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (e.Value.ToString().Substring(4, 6) == "REJECT" || e.Value.ToString().Substring(5,6) == "REJECT")
                {
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }

        //takes selected row and determines if it's a reject to be displayed in red
        private void dgData_SelectionChanged(object sender, EventArgs e)
        {
            if (dgData.SelectedCells.Count > 0)
            {
                int selectedrowindex = dgData.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgData.Rows[selectedrowindex];
                string flagLoc= Convert.ToString(selectedRow.Cells[2].Value);
                
                //if flag is single digit or double digit
                if (flagLoc.Substring(2, 1) == ")")
                {
                    flagLocation = flagLoc.Substring(1, 1);
                }
                else
                {
                    flagLocation = flagLoc.Substring(1, 2);
                }
                
            }
        }

        //headquarters radio box
        private void rdHq_Click(object sender, EventArgs e)
        {
            ftype = 0;
            rdHq.Checked = true;
            rdNpc.Checked = false;
            GetData();
        }

        //npc radio box
        private void rdNpc_Click(object sender, EventArgs e)
        {
            ftype = 1;
            rdNpc.Checked = true;
            rdHq.Checked = false;
            GetData();
        }
    
        //if sector box checked create list of newtcs in Sector for the current user
        private void ckMySec_CheckedChanged(object sender, EventArgs e)
        {
            if (ckMySec.Checked)
            {
                if (ms.Sect00 == "Y")
                {
                    dy.Add("00");
                }
                if (ms.Sect01 == "Y")
                {
                    dy.Add("01");
                }
                if (ms.Sect02 == "Y")
                {
                    dy.Add("02");
                }
                if (ms.Sect03 == "Y")
                {
                    dy.Add("03");
                }
                if (ms.Sect04 == "Y")
                {
                    dy.Add("04");
                }
                if (ms.Sect05 == "Y")
                {
                    dy.Add("05");
                }
                if (ms.Sect06 == "Y")
                {
                    dy.Add("06");
                }
                if (ms.Sect07 == "Y")
                {
                    dy.Add("07");
                }
                if (ms.Sect08 == "Y")
                {
                    dy.Add("08");
                }
                if (ms.Sect09 == "Y")
                {
                    dy.Add("09");
                }
                if (ms.Sect10 == "Y")
                {
                    dy.Add("10");
                }
                if (ms.Sect11 == "Y")
                {
                    dy.Add("11");
                }
                if (ms.Sect12 == "Y")
                {
                    dy.Add("12");
                }
                if (ms.Sect13 == "Y")
                {
                    dy.Add("13");
                }
                if (ms.Sect14 == "Y")
                {
                    dy.Add("14");
                }
                if (ms.Sect15 == "Y")
                {
                    dy.Add("15");
                }
                if (ms.Sect16 == "Y")
                {
                    dy.Add("16");
                }
                if (ms.Sect19 == "Y")
                {
                    dy.Add("19");
                }
                if (ms.Sect1T == "Y")
                {
                    dy.Add("1T");
                }

                newtcs = string.Join(",", dy.ToArray());

            }
            else
            {
                newtcs = "";
            }

            GetData();

        }

        //button to display details about flags
        private void btnDescription_Click(object sender, EventArgs e)
        {
            frmHelpPopup popup = new frmHelpPopup();
           
            popup.filename = "Flag Details.txt";
            popup.title = "FLAG HELP DESCRIPTIONS";
            popup.ShowDialog();
        }

        //button to open review scheduler with ids from database
        private void btnReview_Click(object sender, EventArgs e)
        {
            frmReviewScheduler fm = new frmReviewScheduler();

            if (dgData.SelectedRows[0].Cells[3].Value.ToString() == "0")
            {
                MessageBox.Show("There is no data to display.");
            }
            else
            {
                fm.srow = flagLocation;
                fm.flagTitle = dgData.SelectedRows[0].Cells[2].Value.ToString();
                fm.mySectors = newtcs;

                //if hq button checked display curm_flag data
                if (ftype == 0)
                {
                    fm.flagname = "CURM_FLAG";
                }
                //otherwise if npc checked display report_flag data
                else
                {
                    fm.flagname = "REPORT_FLAG";
                }

                fm.CallingForm = this;
                fm.Show();  // show child

                this.Hide();

            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Flag Type Report";

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            if (rdHq.Checked)
                printer.SubTitle = "HQ Flags";
            else
                printer.SubTitle = "NPC Flags";
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);

            printer.PageNumbers = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Flag Type Report Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;
            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;
            printer.Footer = " ";
            
            dgPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgPrint.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgPrint.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgPrint.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            printer.PrintDataGridViewWithoutDialog(dgPrint);
            Cursor.Current = Cursors.Default;
        }

        private void frmFlagTypeReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

    }
}
