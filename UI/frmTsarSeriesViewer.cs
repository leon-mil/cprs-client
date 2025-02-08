
/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmTsarSeriesViewer.cs
Programmer:         Christine Zhang
Creation Date:      5/7/2017
Inputs:             Stable, Series
Parameters:         None
Outputs:            None
Description:	    This program displays the series table
Detailed Design:    Detailed Design 
Other:	            Called from: Parent screen menu Press Release - View Tsar Series
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
    public partial class frmTsarSeriesViewer :frmCprsParent
    {
     
        public frmTsarSeriesViewer()
        {
            InitializeComponent();
        }

        string stable = string.Empty ;
        string series = string.Empty ;

        private void frmTsarSeriesViewer_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("PUBLICATIONS");

            this.Show();
            ShowSelection();
            if (series == "")
            {
                frmHome fh = new frmHome();
                fh.Show();
                this.Close();
            }
        }

        //show selection dialog
        private void ShowSelection()
        {
            frmTsarSeriesSelectionPopup fp = new frmTsarSeriesSelectionPopup();
            fp.StartPosition = FormStartPosition.CenterParent;
            fp.ShowDialog();
            stable = fp.SeriesTable;
            series = fp.SeriesName;
            
            //if the user didn't click cancel from popup, load data
            if (series != "")
            {
                TsarSeriesData dataObject = new TsarSeriesData();
                dgData.DataSource = dataObject.GetTsarSeriesData(stable, series);
                dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                //make column unsortable
                foreach (DataGridViewColumn dgvc in dgData.Columns)
                {
                    dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                if (series.Substring(5) == "SFC")
                    dgData.Columns[1].DefaultCellStyle.Format = "N10";
                else
                    dgData.Columns[1].DefaultCellStyle.Format = "N1";

                lblTitle2.Text = GetTOCDesc(series.Substring(1,2));
            }
        }

        //get Toc description based on category
        private string GetTOCDesc(string cat)
        {
            string desc = string.Empty;
            if (cat == "") return desc;
            if (cat != "XX" && cat !="NR" && cat !="CC" && Convert.ToInt32(cat) > 19)
                cat = "20";

            switch (cat)
            {
                case "XX":
                    desc = "Total";
                    break;
                case "00":
                    desc = "Residential";
                    break;
                case "NR":
                    desc = "Non Residential";
                    break;
                case "01":
                    desc = "Lodging";
                    break;
                case "02":
                    desc = "Office";
                    break;
                case "03":
                    desc = "Commercial";
                    break;
                case "04":
                    desc = "Health Care";
                    break;
                case "05":
                    desc = "Educational";
                    break;
                case "06":
                    desc = "Religious";
                    break;
                case "07":
                    desc = "Public Safety";
                    break;
                case "08":
                    desc = "Amusement and Recreation";
                    break;
                case "09":
                    desc = "Transportation";
                    break;
                case "10":
                    desc = "Communication";
                    break;
                case "11":
                    desc = "Power";
                    break;
                case "12":
                    desc = "Highway and Street";
                    break;
                case "13":
                    desc = "Sewage and Waste disposal";
                    break;
                case "14":
                    desc = "Water Supply";
                    break;
                case "15":
                    desc = "Conservation and Development";
                    break;
                case "16":
                    desc = "Regulated Power";
                    break;
                case "19":
                    desc = "Regulated Transportation";
                    break;
                case "20":
                    desc = "Manufacturing";
                    break;
                case "CC":
                    desc = "Combined";
                    break;
                default:
                    desc = "";
                    break;
            }

            return desc;
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            ShowSelection();
        }

        Bitmap memoryImage;
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // PrintData();
            printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
            printDocument1.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printDocument1.DefaultPageSettings.Landscape = true;
            memoryImage = GeneralFunctions.CaptureScreen(this);
            printDocument1.Print();
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "Tsar Series " + series;

            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;

            //printer.PorportionalColumns = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Tsar Series Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";

            printer.PrintDataGridViewWithoutDialog(dgData);
            Cursor.Current = Cursors.Default;
        }

        private void frmTsarSeriesViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("PUBLICATIONS", "EXIT");
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            GeneralFunctions.PrintCapturedScreen(memoryImage, UserInfo.UserName, e);
        }
    }
}
