/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmArtbaAnn.cs	    	
Programmer      : Christine Zhang   
Creation Date   :  May 1 2017
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display Annual Artba tabulation

Detailed Design : Detailed Design for Artba Annual
Other           :	            
Revision History:  See Below
**************************************************************************************************
Modified Date   :  2/15/2024
Modified By     :  Christine
Keyword         :  cz2024215
Change Request  :  CR#1390
Description     :  display month data from oldest to new
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
    public partial class frmArtbaAnn : Cprs.frmCprsParent
    {
        private string sdate;
        public frmArtbaAnn()
        {
            InitializeComponent();
        }

        private void frmArtbaAnn_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            //get last year month
            string sddate = (Convert.ToInt32(sdate.Substring(0, 4)) - 1).ToString() + "12";

            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sddate, "yyyyMM", CultureInfo.InvariantCulture);

            //get 5 year month 
            string[] mons = new string[60]; /* n is an array of 60 string */

            /* initialize elements of array n */
            for (int i = 0; i < 60; i++)
            {
                mons[i] = (dt.AddMonths(-(59-i))).ToString("yyyyMM", CultureInfo.InvariantCulture);
            }

            ArtbaData gdata = new ArtbaData();
            DataTable table = gdata.GetArtabAnnData(sdate);
            dgData.DataSource = table;

            dgData.Columns[0].HeaderText = "Type of Construction ";
            dgData.Columns[0].Width = 200;
            dgData.Columns[0].Frozen = true;

            for (int i = 0; i < 60; i++)
            {
                dgData.Columns[i + 1].HeaderText = mons[i];
                dgData.Columns[i + 1].DefaultCellStyle.Format = "N0";
                dgData.Columns[i + 1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns[i + 1].Width = 60;
            }
            dgData.Columns["CV"].HeaderText = "CV";
            dgData.Columns["CV"].DefaultCellStyle.Format = "N1";
            dgData.Columns["CV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns["CV"].Width = 60;

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            lblTitle.Text = @"ARTBA ANNUAL VIP FOR TRANSPORTATION TC'S";
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
            printer.printDocument.DocumentName = "Artba Annual Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[0].Width = 150;

            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 200;
        }

        private void frmArtbaAnn_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
