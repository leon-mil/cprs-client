/**************************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmGlobalInsightAnn.cs	    	
Programmer:         Christine Zhang
Creation Date:      4/18/2017
Inputs:             None                 
Parameters:		    None 
Outputs:		    None	
Description:	    Display annual global insight tabulation

Detailed Design:    Detailed User Requirements for the global insight annual Screen 
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
    public partial class frmGlobalInsightAnn : frmCprsParent
    {
        public frmGlobalInsightAnn()
        {
            InitializeComponent();
        }

        private void frmGlobalInsightAnn_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            string sdate = GeneralDataFuctions.GetCurrMonthDateinTable();

            //get last year month name for table header
            string sddate = (Convert.ToInt32(sdate.Substring(0, 4)) -1).ToString() + "12";

            //Convert sdate to datatime
            var dt = DateTime.ParseExact(sddate, "yyyyMM", CultureInfo.InvariantCulture);

            //get 5 year month  for Column Headers
            string[] mons = new string[60]; /* n is an array of 60 string */

            /* initialize elements of array n */
            for (int i = 0; i < 60; i++)
            {
                mons[i] = (dt.AddMonths(-i)).ToString("yyyyMM", CultureInfo.InvariantCulture);
            }

            GlobalInsightData gdata = new GlobalInsightData();
            DataTable table = gdata.GetGlobalInsightAnnData(sdate);
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

            lblTitle.Text = "GLOBAL INSIGHT ANNUAL";

        }

        private void frmGlobalInsightAnn_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
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
            printer.printDocument.DocumentName = "Global Insight Annual Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dgData.Columns[0].Width = 150;

            printer.PrintDataGridViewWithoutDialog(dgData);
            dgData.Columns[0].Width = 200;

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

    }
}
