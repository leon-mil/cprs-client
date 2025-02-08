/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmNPCAttemptCases.cs

 Programmer    : Diane Musachio

 Creation Date : 12/15/2016

 Inputs        : Date, displayed date, interviewer, type

 Parameters    : N/A
 
 Output        : datagrid displaying id, respid, date, time start and time exit
                 for selected user, date, type and total # of cases
 
 Description   :  This program will display information from 
                  cell selected on frmNPCAttempts.cs 

 Detail Design : Detailed User Requirements for NPC Attempts Report 

 Other         : Called by: frmNPCAttempts.cs

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
    public partial class frmNPCAttemptCases : frmCprsParent
    {
        public Form CallingForm;
        public string Date;
        public string display_selected_date;
        public string Interviewer;
        public string Type;
        public string StatPeriod;

        NPCAttemptsData npc = new NPCAttemptsData();
        
        private string filter;
        private bool call_callingFrom;

        public frmNPCAttemptCases()
        {
            InitializeComponent();

        }

        //load the frmNPCAttemptCases table that displays user, date and times
        private void frmNPCAttemptCases_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            SetButtonTxt();
            txtInterviewer.Text = Interviewer;
            txtDate.Text = display_selected_date;
            txtType.Text = Type;

            //filter data depending on column criteria
            filterData();
        }

        private void filterData()
        {
            DataTable dt = new DataTable();
            DataTable di = new DataTable();
            var filteredDataTable = new DataTable();

            //get data if interviewer selection is default of total
            if (Interviewer == "total000") 
            {
                txtInterviewer.Text = "TOTAL";
              
               //get data for initials column
                if (String.Equals(Type, "INITIALS"))
                {
                    if (Date != "total")
                    {
                        di = npc.GetPopupInitsTotalDate(Date);
                        filter = "grpcde in ( '3','4','5')";
                    }
                    else
                    {
                        di = npc.GetPopupInitsTotals();
                        filter = "grpcde in ( '3','4','5')";
                    }

                    //use above criteria to filter datatable and store in data view
                    DataView dv = new DataView(di);
                    dv.RowFilter = filter;

                    //assign dataview to datagrid
                    dgCases.DataSource = dv;

                    //hide extra data columns used to filter criteria
                    dgCases.Columns[4].Visible = false;
                    dgCases.Columns[5].Visible = false;
                    dgCases.Columns[6].Visible = false;

                }
                else
                {
                    //obtain data for defaults of totals of all dates
                    if (Date == "total")
                    {
                        dt = npc.GetPopupTotal();
                    }
                    //obtain data for totals of selected dates
                    else
                    {
                        dt = npc.GetPopupTotalDate(Date);
                    }

                    if (String.Equals(Type, "STSFD"))
                    {
                        filter = "accescde = 'P' and callstat = 'V' and grpcde in ( '3','4','5')";
                    }
                    else if (String.Equals(Type, "CONTACT"))
                    {
                        filter = "accescde = 'P' and callstat = '9' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "POC"))
                    {
                        filter = "accescde = 'P' and callstat = '8' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "RESCH"))
                    {
                        filter = "accescde = 'P' and callstat = '5' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "BUSY"))
                    {
                        filter = "accescde = 'P' and callstat = '2' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "RING"))
                    {
                        filter = "accescde = 'P' and callstat = '1' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "DSCNT"))
                    {
                        filter = "accescde = 'P' and callstat = '3' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "REFUSE"))
                    {
                        filter = "accescde = 'P' and callstat = '4' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "PROM to RPT"))
                    {
                        filter = "accescde = 'P' and callstat = '7' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "LVM"))
                    {
                        filter = "accescde = 'P' and callstat = '6' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "FORM"))
                    {
                        filter = "accescde = 'F' and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "I-NET"))
                    {
                        filter = "accescde in ('I','S') and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "REF"))
                    {
                        filter = "accescde in ('F','I','S') and callstat = 'R' and grpcde in ('3','4','5')";
                    }

                    //use above criteria to filter datatable and store in data view
                    DataView dv = new DataView(dt);
                    dv.RowFilter = filter;

                    //assign dataview to datagrid
                    dgCases.DataSource = dv;

                    //hide extra data columns used to filter criteria
                    dgCases.Columns[4].Visible = false;
                    dgCases.Columns[5].Visible = false;
                    dgCases.Columns[6].Visible = false;
                    dgCases.Columns[7].Visible = false;
                    dgCases.Columns[8].Visible = false;

                }
            }
            // get data if an interviewer is selected
            else
            {
                //get data for initials column
                if (String.Equals(Type, "INITIALS"))
                {
                    if (Date != "total")
                    {
                        di = npc.GetPopupInits(Interviewer, Date);
                        filter = "grpcde in ( '3','4','5')";
                    }
                    else
                    {
                        di = npc.GetPopupInitsTotalInterviewer(Interviewer);
                        filter = "grpcde in ( '3','4','5')";
                    }

                    //use above criteria to filter datatable and store in data view
                    DataView dv = new DataView(di);
                    dv.RowFilter = filter;

                    //assign dataview to datagrid
                    dgCases.DataSource = dv;

                    //hide extra data columns used to filter criteria
                    dgCases.Columns[4].Visible = false;
                    dgCases.Columns[5].Visible = false;
                    dgCases.Columns[6].Visible = false;

                }
                else
                {
                    // get data by interviewer and selected date
                    if (Date != "total")
                    {
                        dt = npc.GetPopupData(Interviewer, Date);
                    }
                    //get data by interviewer and total of all dates
                    else
                    {
                        dt = npc.GetPopupDataTotInterviewer(Interviewer);
                    }

                    if (String.Equals(Type, "STSFD"))
                    {
                        filter = "accescde = 'P' and callstat = 'V'  and usrnme = " + GeneralData.AddSqlQuotes(Interviewer) + " and grpcde in ( '3','4','5')";
                    }
                    else if (String.Equals(Type, "CONTACT"))
                    {
                        filter = "accescde = 'P' and callstat = '9' and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "POC"))
                    {
                        filter = "accescde = 'P' and callstat = '8' and usrnme =  " + GeneralData.AddSqlQuotes(Interviewer) + " and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "RESCH"))
                    {
                        filter = "accescde = 'P' and callstat = '5' and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "BUSY"))
                    {
                        filter = "accescde = 'P' and callstat = '2' and usrnme =  " + GeneralData.AddSqlQuotes(Interviewer) + " and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "RING"))
                    {
                        filter = "accescde = 'P' and callstat = '1' and usrnme =  " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "DSCNT"))
                    {
                        filter = "accescde = 'P' and callstat = '3' and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "REFUSE"))
                    {
                        filter = "accescde = 'P' and callstat = '4'  and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "PROM to RPT"))
                    {
                        filter = "accescde = 'P' and callstat = '7'  and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "LVM"))
                    {
                        filter = "accescde = 'P' and callstat = '6'  and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "FORM"))
                    {
                        filter = "accescde = 'F'  and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "I-NET"))
                    {
                        filter = "accescde in ('I','S')  and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }
                    else if (String.Equals(Type, "REF"))
                    {
                        filter = "accescde in ('F','I','S') and callstat = 'R'  and usrnme =   " + GeneralData.AddSqlQuotes(Interviewer) + "  and grpcde in ('3','4','5')";
                    }

                    //use above criteria to filter datatable and store in data view
                    DataView dv = new DataView(dt);
                    dv.RowFilter = filter;

                    //assign dataview to datagrid
                    dgCases.DataSource = dv;

                    //hide extra data columns used to filter criteria
                    dgCases.Columns[4].Visible = false;
                    dgCases.Columns[5].Visible = false;
                    dgCases.Columns[6].Visible = false;
                    dgCases.Columns[7].Visible = false;
                    dgCases.Columns[8].Visible = false;

                }
            }

            //if no data found put out message
            if (dgCases.RowCount == 0)
            {
                MessageBox.Show("No data to display.");
                this.CallingForm.Show();
                this.Close();
            }
            //if data found output number of cases found
            else
            {
                txtTotal.Text = dgCases.RowCount.ToString();
            }         
        }

        //format start and end times in hh:mm format
        private void dg_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e != null)
            {
                if ((e.Value != null) && ((e.ColumnIndex == 2) || (e.ColumnIndex == 3)))
                {
                    try
                    {
                        string strval = e.Value.ToString().Substring(0, 4);
                        DateTime dt = DateTime.ParseExact(strval, "HHmm", CultureInfo.InvariantCulture);
                        string timestring = dt.ToString("HH:mm");
                        e.Value = timestring;
                        e.FormattingApplied = true;
                    }
                    catch (FormatException)
                    {
                        e.FormattingApplied = false;
                    }
                }
            }
        }

        //format start and end times in hh:mm format
        private void dg_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
        {
            if (e != null)
            {
                if ((e.Value != null) && ((e.ColumnIndex == 2) || (e.ColumnIndex == 3)))
                {
                    try
                    {
                        string strval = e.Value.ToString().Substring(0, 4);
                        DateTime dt = DateTime.ParseExact(strval, "HHmm", CultureInfo.InvariantCulture);
                        string timestring = dt.ToString("HH:mm");
                        e.Value = timestring;
                        e.ParsingApplied = true;
                    }
                    catch (FormatException)
                    {
                        e.ParsingApplied = false;
                    }
                }
            }
        }

        //depending on user role set button to c700 for hq staff
        // or tfu for npc staff
        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:   
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQManager:    
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQAnalyst:  
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.NPCManager:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCInterviewer:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.HQSupport: 
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQMathStat:  
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQTester:  
                    btnC700.Text = "C-700";
                    break;
            }
        }
        
        //return to previous form 
        private void btnPrevious_Click(object sender, EventArgs e)
        {  
            this.Close();

            if (CallingForm != null)
            {
                CallingForm.Show();
                call_callingFrom = true;
            }
        }

        //display c700 screen and provide list of ids
        private void btnC700_Click(object sender, EventArgs e)
        {
            bool idexist = false;

            DataGridViewSelectedRowCollection rows = dgCases.SelectedRows;

            int index = dgCases.CurrentRow.Index;
            string selected_id = dgCases["ID", index].Value.ToString();

            if (!(String.IsNullOrWhiteSpace(selected_id)))
            {
                idexist = GeneralDataFuctions.ValidateSampleId(selected_id);
            }

            //Display TFU on the C-700 button for NPC users
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                if (!idexist)
                {
                   MessageBox.Show("The TFU Screen is not available for this case.");
                   this.DialogResult = DialogResult.None;
                }
                else
                {
                    this.Hide(); // hide parent

                    string val1 = dgCases["ID", index].Value.ToString();
                    SampleData sampdata = new SampleData();
                    
                    Sample sp = sampdata.GetSampleData(val1);

                    frmTfu tfu = new frmTfu();

                    tfu.RespId = sp.Respid;
                    tfu.CallingForm = this;

                    GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

                    tfu.ShowDialog();   // show child
                    GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
                }
            }
            else
            {               
                if (!idexist)
                {
                    MessageBox.Show("The C-700 Screen is not available for this case.");
                    this.DialogResult = DialogResult.None;
                }
                else
                {
                this.Hide(); // hide parent

                string val1 = dgCases["ID", index].Value.ToString();

                // Store Id in list for Page Up and Page Down
                List<string> Idlist = new List<string>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgCases.Rows)
                {
                    string val = dgCases["ID", cnt].Value.ToString();
                    Idlist.Add(val);
                    cnt = cnt + 1;
                }

                frmC700 fC700 = new frmC700();
                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CurrIndex = index;
                fC700.CallingForm = this;
                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

                fC700.ShowDialog(); // show child
    
                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");

                }
            }
        }

        private void frmNPCAttemptCases_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
            
            /*Close the hide form */
            if (!call_callingFrom)
                CallingForm.Close();
        
        }

        //print npc attempts popup datagrid
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();

            printer.Title = "NPC ATTEMPT CASES";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;
            printer.SubTitle = "Stat Period: " + StatPeriod  + "     By Interviewer: " + Interviewer + "           By Date: " + display_selected_date +
                "           By Type: " + Type +  "         TOTAL CASES:  " + dgCases.RowCount.ToString();

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "NPC Attempt Cases Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;
            printer.PrintDataGridViewWithoutDialog(dgCases);

            Cursor.Current = Cursors.Default;
        }
    }
}
