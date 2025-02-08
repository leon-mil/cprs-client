

/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmNPCAttempts.cs

 Programmer    : Diane Musachio

 Creation Date : 11/22/2016

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display NPC Attempts Report by total,
                 by date or by interviewer

 Detail Design : Detailed User Requirements for NPC Attempts Report 

 Other         : Called by: menu -> reports -> NPC Attempts

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/

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
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Globalization;

namespace Cprs
{
    public partial class frmNPCAttempts : frmCprsParent
    {
        DataTable table = new DataTable();
        NPCAttemptsData npc = new NPCAttemptsData();
        Object selectedStatp;
        List<string> dy = new List<string>();
        string selected_date;
        string selected_user;
        DataTable dtt1;
        DataTable dtb1;
        DataTable dtt2;
        DataTable dtb2;

        public frmNPCAttempts()
        {
            InitializeComponent();     
        }

        //load form 
        private void frmNPCAttempts_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            GetMonthsList();
 
            cbStatp.DataSource = dy;
            cbStatp.SelectedIndex = 0;
            tabs.SelectedIndex = 0;
        }

        string CurrMonth = string.Empty;

        //calculate stat periods for dropdown combobox 
        private void GetMonthsList()
        {
            DateTime today = DateTime.Now;

            string currYearMon1 = string.Empty;
            string currYearMon2 = string.Empty;
            string currYearMon3 = string.Empty;
            string currYearMon4 = string.Empty;
            string currYearMon5 = string.Empty;
            string currYearMon6 = string.Empty;
            string currYearMon7 = string.Empty;
            string currYearMon8 = string.Empty;
            string currYearMon9 = string.Empty;
            string currYearMon10 = string.Empty;
            string currYearMon11 = string.Empty;
            string currYearMon12 = string.Empty;

            //current month
            CurrMonth = (GeneralFunctions.CurrentYearMon());
            //get the last 12 months 
            DateTime.Now.ToString("yyyyMM");
            currYearMon1  = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            currYearMon2  = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
            currYearMon3  = DateTime.Now.AddMonths(-3).ToString("yyyyMM");
            currYearMon4  = DateTime.Now.AddMonths(-4).ToString("yyyyMM");
            currYearMon5  = DateTime.Now.AddMonths(-5).ToString("yyyyMM");
            currYearMon6  = DateTime.Now.AddMonths(-6).ToString("yyyyMM");
            currYearMon7  = DateTime.Now.AddMonths(-7).ToString("yyyyMM");
            currYearMon8  = DateTime.Now.AddMonths(-8).ToString("yyyyMM");
            currYearMon9  = DateTime.Now.AddMonths(-9).ToString("yyyyMM");
            currYearMon10 = DateTime.Now.AddMonths(-10).ToString("yyyyMM");
            currYearMon11 = DateTime.Now.AddMonths(-11).ToString("yyyyMM");
            currYearMon12 = DateTime.Now.AddMonths(-12).ToString("yyyyMM");

            dy.Add(CurrMonth);
            dy.Add(currYearMon1);
            dy.Add(currYearMon2);
            dy.Add(currYearMon3);
            dy.Add(currYearMon4);
            dy.Add(currYearMon5);
            dy.Add(currYearMon6);
            dy.Add(currYearMon7);
            dy.Add(currYearMon8);
            dy.Add(currYearMon9);
            dy.Add(currYearMon10);
            dy.Add(currYearMon11);
            dy.Add(currYearMon12);
        }

        //get top datagrid on tab 1 (by interviewer)
        private void GetT1()
        {
            //if not current month
            if (selectedStatp.ToString() != CurrMonth)
            {
                dtt1 = npc.GetNPCAttemptsDay(selectedStatp, "0000");
                dgt1.DataSource = dtt1;
            }
            //if current month
            else
            {
                dtt1 = npc.GetCurrMonthData("");
                dgt1.DataSource = dtt1;
            }

            //set up columns
            setItemColumnHeader(dgt1);
        }

        //get top datagrid on tab 2 (by date)
        private void GetT2()
        {
            //if not current month
            if (selectedStatp.ToString() != CurrMonth)
            {
                //Update the project grid to reflect changes to the selection
                dtt2 = npc.GetNPCAttemptsPastInterviewer(selectedStatp, selected_user);
                dgt2.DataSource = dtt2;
            }
            //if current month
            else
            {
                dtt2 = npc.GetInterviewerData("");
                dgt2.DataSource = dtt2;
            }

            //set up columns
            setItemColumnHeader(dgt2);
            
        }

        //sets up column headers, aligns them and makes them not sortable
        private void setItemColumnHeader(DataGridView dataGridNum)
        {
            if (dataGridNum.ColumnCount > 0)
            {
                dataGridNum.Columns[0].HeaderText = "        ";
                dataGridNum.Columns[1].HeaderText = "INITIALS";
                dataGridNum.Columns[2].HeaderText = "STSFD";
                dataGridNum.Columns[3].HeaderText = "CONTACT";
                dataGridNum.Columns[3].Width = 70;
                dataGridNum.Columns[4].HeaderText = "POC";
                dataGridNum.Columns[5].HeaderText = "RESCH";
                dataGridNum.Columns[6].HeaderText = "BUSY";
                dataGridNum.Columns[7].HeaderText = "RING";
                dataGridNum.Columns[8].HeaderText = "DSCNT";
                dataGridNum.Columns[9].HeaderText = "REFUSE";
                dataGridNum.Columns[10].HeaderText = "PROM to RPT";
                dataGridNum.Columns[11].HeaderText = "LVM";
                dataGridNum.Columns[12].HeaderText = "FORM";
                dataGridNum.Columns[13].HeaderText = "I-NET";
                dataGridNum.Columns[14].HeaderText = "ATTEMPT";
                dataGridNum.Columns[15].HeaderText = "REF";
                dataGridNum.Columns[16].HeaderText = "HOUR";
                dataGridNum.Columns[17].HeaderText = "MINUTE";
                

                dataGridNum.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

                //make columns not sortable
                foreach (DataGridViewColumn dgvc in dataGridNum.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        //if top grid on tab 1 changes
        private void dgt1_SelectionChanged(object sender, EventArgs e)
        {
            int selectedRowCount = dgt1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            //if a row is selected
            if (selectedRowCount > 0)
            {
                //obtain interviewer
                selected_user = dgt1.SelectedRows[0].Cells[0].Value.ToString();

                //if selected is not current month
                if (selectedStatp.ToString() != CurrMonth)
                {
                    dtb1 = npc.GetNPCAttemptsPastInterviewer(selectedStatp, selected_user);
                    dgb1.DataSource = dtb1;
                }
                //if selected is current month
                else
                {
                    //if not total
                    if (selected_user != "total000")
                    {
                        // Update the project grid to reflect changes to the selection
                        dtb1 = npc.GetInterviewerData(selected_user);
                        dgb1.DataSource = dtb1;
                    }
                    //if total
                    else
                    {
                        dtb1 = npc.GetInterviewerData("");
                        dgb1.DataSource = dtb1;
                    }
                }

                setItemColumnHeader(dgb1);

                //populate label on screen
                lblInterviewer.Text = "Interviewer : " + dgt1.SelectedRows[0].Cells[0].FormattedValue.ToString();
            }
        }

        private string display_selected_date;

        //if top datagrid on tab 2 selection changes
        private void dgt2_SelectionChanged(object sender, EventArgs e)
        {
             int selectedRowCount = dgt2.Rows.GetRowCount(DataGridViewElementStates.Selected);

             if (selectedRowCount > 0)
             {
                selected_date = dgt2.SelectedRows[0].Cells[0].Value.ToString();

                // formats selected date to MMM dd
                display_selected_date = dgt2.SelectedRows[0].Cells[0].FormattedValue.ToString();

                //if selection is not current month
                if (selectedStatp.ToString() != CurrMonth)
                {
                    if (selected_date != "total")
                    {
                        //Update the project grid to reflect changes to the selection.
                        dtb2 = npc.GetNPCAttemptsDay(selectedStatp, selected_date);
                        dgb2.DataSource = dtb2;
                    }
                    else
                    {
                        dtb2 = npc.GetNPCAttemptsDay(selectedStatp, "0000");
                        dgb2.DataSource = dtb2;
                    }                   
                }
                //if selection is current month
                else
                {
                    if (selected_date == "total")
                    {
                        dtb2 = npc.GetCurrMonthData("");
                        dgb2.DataSource = dtb2;
                    }
                    else
                    {
                        dtb2 = npc.GetCurrMonthData(selected_date);
                        dgb2.DataSource = dtb2;
                    }
                }

                // populate label on screen
                if (display_selected_date != "TOTAL")
                {
                    lblDate.Text = "Date : " + display_selected_date;
                }
                else
                {
                    lblDate.Text = "Date : " + "TOTAL";
                }

                setItemColumnHeader(dgb2);              
           }
        }

        //update top grids if statperiod selection changes
        private void cbstatp_SelectionChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex == -1)
                return;

            selected_date = "total";
            selected_user = "total000";

            selectedStatp = cbStatp.SelectedValue;

            if (tabs.SelectedIndex != 0)
                tabs.SelectedIndex = 0;
            else
                GetT1();
        }

        //if tabs change update datagrids
        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabs.SelectedIndex == -1)
                return;

            //selected_date = "total";
            selected_user = "total000";

            if (tabs.SelectedIndex == 0)
            {
                GetT1();
            }
            else if (tabs.SelectedIndex == 1)
            {
                GetT2();
            }
        }

        //format cells to reflect "TOTAL" consistently throughout screens and datagrids and 
        //format date to MMM dd ex. FEB 07 for display purposes
        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                if (e != null)
                {
                    string strval = (string)e.Value.ToString();

                    if ((strval == "0000") || (strval == "total"))
                    {
                        {
                            e.Value = "TOTAL";
                        }
                    }
                    else try
                        {
                            string fmt = "00";

                            int month = Convert.ToInt32(strval.Substring(0, 2));

                            int dayval = Convert.ToInt32(strval.Substring(2, 2));

                            string monabbr = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month).ToUpper();

                            string dayval1 = dayval.ToString(fmt);

                            string monthday = monabbr + " " + dayval1;

                            e.Value = monthday;

                            e.FormattingApplied = true;
                        }
                        catch (FormatException)
                        {
                            e.FormattingApplied = false;
                        }
                }
                

            }
            
        }
        //format cells to reflect "TOTAL" consistently throughout screens and datagrids and 
        //format date to MMM dd ex. FEB 07 for display purposes
        private void dg_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
          //Column header has index -1; don't want it selectable
            if (e.RowIndex != -1)
            {
                if ((e.Value != null) && (e.ColumnIndex == 0))
                {
                    string strval = (string)e.Value.ToString();

                    if ((strval == "0000") || (strval == "total"))
                    {
                        {
                            e.Value = "TOTAL";
                        }
                    }
                    try
                    {
                        string fmt = "00";

                        int month = Convert.ToInt32(strval.Substring(0, 2));

                        int dayval = Convert.ToInt32(strval.Substring(2, 2));

                        string monabbr = CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(month).ToUpper();

                        string dayval1 = dayval.ToString(fmt);

                        string monthday = monabbr + " " + dayval1;

                        e.Value = monthday;

                        e.ParsingApplied = true;
                    }
                    catch (FormatException)
                    {
                        e.ParsingApplied = false;
                    }
                }
            }
            
        }

        private void dgInterviewer_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //Column header has index -1; don't want it selectable
            if (e.RowIndex != -1)
            {  
                if ((e.Value != null) && (e.ColumnIndex == 0))
                {
                   string strval = (string)e.Value.ToString();
 
                   if (strval == "total000")
                   {
                       e.Value = "TOTAL";
                   }
                }
            }
        }

        //select a cell in bottom grid of tab 1
        private void dgb1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Column header has index -1; don't want it selectable
            if (e.RowIndex != -1)
            {
                if (selectedStatp.ToString() == CurrMonth)
                {
                    if (e.ColumnIndex == 0 || e.ColumnIndex == 14 || e.ColumnIndex == 16 || e.ColumnIndex == 17)
                        return;

                    if (dgb1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Equals("0"))
                    {
                        MessageBox.Show("There is no data to display.");
                    }
                    else
                    {
                        frmNPCAttemptCases fm = new frmNPCAttemptCases();
                        fm.StatPeriod = selectedStatp.ToString();
                        fm.Interviewer = selected_user;
                        fm.Date = dgb1.Rows[e.RowIndex].Cells[0].Value.ToString();
                        fm.Type = dgb1.Columns[e.ColumnIndex].HeaderText;
                        fm.display_selected_date = dgb1.Rows[e.RowIndex].Cells[0].FormattedValue.ToString();
                        fm.CallingForm = this;

                        GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
                        this.Hide();
                        fm.ShowDialog();  // show child
                        GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
                    }
                }
            }
        }
        //select a cell in bottom grid of tab 2
        private void dgb2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Column header has index -1; don't want it selectable
            if (e.RowIndex != -1)
            {
                if (selectedStatp.ToString() == CurrMonth)
                {
                    if (e.ColumnIndex == 0 || e.ColumnIndex == 14 || e.ColumnIndex == 16 || e.ColumnIndex == 17)
                        return;
                   
                    if (dgb2.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "0")
                    {
                        MessageBox.Show("There is no data to display.");
                    }
                    else
                    {
                        frmNPCAttemptCases fm = new frmNPCAttemptCases();
                        fm.Date = selected_date;
                        fm.Interviewer = dgb2.Rows[e.RowIndex].Cells[0].Value.ToString();
                        fm.Type = dgb2.Columns[e.ColumnIndex].HeaderText;
                        fm.display_selected_date = display_selected_date;
                        fm.CallingForm = this;

                        GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
                        this.Hide();
                        fm.ShowDialog();  // show child   
                        GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
                    }
                }
            }
        }

        private void frmNPCAttempts_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }


        //print datagrids
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (tabs.SelectedTab == tabPage1)
            {
                Printgrid(dgt1);
                Printgrid(dgb1);
            }
            else
            {
                Printgrid(dgt2);
                Printgrid(dgb2);
            }
        }


        private void Printgrid(DataGridView dgrid)
        {
                Cursor.Current = Cursors.WaitCursor;

                DGVPrinter printer = new DGVPrinter();
                printer.Title = "NPC ATTEMPTS";
                printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
                
                if (dgrid == dgt1)
                {
                    printer.SubTitle = " Stat Period:  " + selectedStatp.ToString() + "       By Interviewer";
                    dgrid.DataSource = dtt1;
                }        
                if (dgrid == dgb1)
                {
                    if (selected_user == "total000")
                    {
                        selected_user = "TOTAL";
                    }
                    printer.SubTitle = " Stat Period:  " + selectedStatp.ToString() + "       By Interviewer: " + selected_user;
                    dgrid.DataSource = dtb1;
                }
                if (dgrid == dgt2)
                {
                    printer.SubTitle = " Stat Period:  " + selectedStatp.ToString() + "       By Date";
                    dgrid.DataSource = dtt2;
                }
                if (dgrid == dgb2)
                {
                    printer.SubTitle = " Stat Period:  " + selectedStatp.ToString() + "       By Date: " + display_selected_date;
                    dgrid.DataSource = dtb2;
                }

                printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
                printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);       
                printer.Userinfo = UserInfo.UserName;

                printer.PageNumbers = true;
                printer.PageNumberInHeader = true;

                printer.HeaderCellAlignment = StringAlignment.Near;
                printer.PrintRowHeaders = false;
                
                printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
                printer.printDocument.DocumentName = "NPC Attempts Print";
                printer.printDocument.DefaultPageSettings.Landscape = true;
                
                printer.Footer = " ";
                Margins margins = new Margins(30, 40, 30, 30);
                printer.PrintMargins = margins;

                    if (dgrid.RowCount > 1)
                    {
                        dgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                        dgrid.Columns[0].Width = 65;
                        dgrid.Columns[1].Width = 60;
                        dgrid.Columns[2].Width = 60;
                        dgrid.Columns[3].Width = 65;
                        dgrid.Columns[4].Width = 50;
                        dgrid.Columns[5].Width = 50;
                        dgrid.Columns[6].Width = 50;
                        dgrid.Columns[7].Width = 50;
                        dgrid.Columns[8].Width = 55;
                        dgrid.Columns[9].Width = 60;
                        dgrid.Columns[10].Width = 60;
                        dgrid.Columns[11].Width = 50;
                        dgrid.Columns[12].Width = 60;
                        dgrid.Columns[13].Width = 50;
                        dgrid.Columns[14].Width = 65;
                        dgrid.Columns[15].Width = 50;
                        dgrid.Columns[16].Width = 50;
                        dgrid.Columns[17].Width = 65;

                printer.PrintDataGridViewWithoutDialog(dgrid);
                        dgrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
          
                Cursor.Current = Cursors.Default;           
        }

        //necessary to remove dark blue highlight in first cell of bottom datagrids
        private void dgb2_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgb2.ClearSelection();
        }

        private void dgb1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            dgb1.ClearSelection();
        }

       
    }
}
