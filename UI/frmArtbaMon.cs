/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmArtbaMon.cs	    	
Programmer      :  Christine Zhang   
Creation Date   :  May 1 2017
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display monthly Artba tabulation

Detailed Design : Detailed Design for Artba monthly
Other           :	            
Revision History:	
**************************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
**************************************************************************************************/
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
using System.Globalization;
using System.Text.RegularExpressions;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmArtbaMon : Cprs.frmCprsParent
    {
        private string sdate;
        public frmArtbaMon()
        {
            InitializeComponent();
        }
        private void frmArtbaMon_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            //get 1st rev, 2nd rev, 3rd and 4th rev month
            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sdate, "yyyyMM", CultureInfo.InvariantCulture);

            //get month name
            string premon = (dt.AddMonths(-1)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string ppmon = (dt.AddMonths(-2)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string p3mon = (dt.AddMonths(-3)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            string p4mon = (dt.AddMonths(-4)).ToString("yyyyMM", CultureInfo.InvariantCulture);

            ArtbaData gdata = new ArtbaData();
            DataTable table = gdata.GetArtbaMonData(sdate, premon, ppmon, p3mon, p4mon);
            dgData.DataSource = table;

            dgData.Columns[0].HeaderText = "Type of Construction ";
            dgData.Columns[0].Width = 200;

            dgData.Columns[1].HeaderText = "4th Revision";
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[1].Width = 120;

            dgData.Columns[2].HeaderText = "3rd Revision";
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].Width = 120;

            dgData.Columns[3].HeaderText = "2nd Revision";
            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].Width = 120;

            dgData.Columns[4].HeaderText = "1st Revision";
            dgData.Columns[4].DefaultCellStyle.Format = "N0";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].Width = 120;

            dgData.Columns[5].HeaderText = "Current Preliminary";
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].Width = 120;

            dgData.Columns[6].HeaderText = "CV";
            dgData.Columns[6].DefaultCellStyle.Format = "N1";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].Width = 120;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            lblTitle.Text = @"ARTBA " +  sdate + " VIP FOR TRANSPORTATION TC'S";
        }
        private void btnExcel_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog save file
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text file|*.csv";
            saveFileDialog1.Title = "Save an File";
            var result = saveFileDialog1.ShowDialog();

            if (result != DialogResult.OK)
                return;

            dgData.MultiSelect = true;

            // Choose whether to write header. Use EnableWithoutHeaderText instead to omit header.
            dgData.ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableWithAutoHeaderText;
            // Select all the cells
            dgData.SelectAll();
            // Copy (set clipboard)
            Clipboard.SetDataObject(dgData.GetClipboardContent());
            // Paste (get the clipboard and serialize it to a file)
            System.IO.File.WriteAllText(saveFileDialog1.FileName, Clipboard.GetText(TextDataFormat.CommaSeparatedValue));

            dgData.ClearSelection();
            dgData.Rows[0].Selected = true;
            dgData.MultiSelect = false;
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DGVPrinter printer = new DGVPrinter();
            printer.Title = lblTitle.Text;
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitle = "Millions of Dollars";
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Artba Month Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[0].Width = 150;

            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 200;

            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void frmArtbaMon_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
