/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmBenExport.cs

 Programmer    : Diane Musachio

 Creation Date : 3/26/2019

 Inputs        : n/a

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will allow users to select year range  
              for benchmark series and export benchmark data

 Detail Design : Detailed User Requirements for VIP Benchmark Export

 Other         : Called by: Tabulations -> Benchmark -> Export Data

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
    public partial class frmBenExport : frmCprsParent
    {
        List<string> toclist = new List<string>();

        List<string> startYearlist = new List<string>();
        List<string> endYearlist = new List<string>();

        VipBenchmarkExportData data_object = new VipBenchmarkExportData();
        public frmBenExport()
        {
            InitializeComponent();
        }

        private void frmBenExport_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            SetupDropdown();
            PopulateStartYears();
            PopulateEndYears();
    
            cbStart.DataSource = startYearlist;        
            cbEnd.DataSource = endYearlist;
            cbSeries.DataSource = toclist;

            //include this code to prevent mouse scrolling in dropdownlist
            this.cbSeries.MouseWheel += new MouseEventHandler(CbSeries_MouseWheel);
            this.cbStart.MouseWheel += new MouseEventHandler(CbSeries_MouseWheel);
            this.cbEnd.MouseWheel += new MouseEventHandler(CbSeries_MouseWheel);
        }

        //get values for the combobox
        private void SetupDropdown()
        {
            toclist.Add("Manufacturing");
            toclist.Add("Federal Highway");
            toclist.Add("Farm");
            toclist.Add("Gas");
            toclist.Add("Oil");
            toclist.Add("Electric");
            toclist.Add("Electric Transmission");
            toclist.Add("Improvements");
        }

        //populate list of dates for start year
        private void PopulateStartYears()
        {
            startYearlist.Add(DateTime.Now.AddYears(-6).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(-5).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(-4).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(-3).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(-2).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(-1).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(0).ToString("yyyy"));
            startYearlist.Add(DateTime.Now.AddYears(+1).ToString("yyyy"));
        }

        //populate list of dates for end year
        private void PopulateEndYears()
        {
            endYearlist.Add(DateTime.Now.AddYears(-6).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(-5).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(-4).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(-3).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(-2).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(-1).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(0).ToString("yyyy"));
            endYearlist.Add(DateTime.Now.AddYears(+1).ToString("yyyy"));
        }

        private string toc;
        private string oseries;

        private void cbSeries_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cbSeries.SelectedValue.ToString() == "Manufacturing")
            {
                cbStart.SelectedItem = startYearlist[0].ToString();
                cbEnd.SelectedItem = endYearlist[4].ToString();
                toc = "201";
                oseries = "V20IXBMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Federal Highway")
            {
                cbStart.SelectedItem = startYearlist[3].ToString();
                cbEnd.SelectedItem = endYearlist[6].ToString();
                toc = "202";
                oseries = "F12XXBMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Farm")
            {
                cbStart.SelectedItem = startYearlist[4].ToString();
                cbEnd.SelectedItem = endYearlist[7].ToString();
                toc = "203";
                oseries = "X0360BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Gas")
            {
                cbStart.SelectedItem = startYearlist[4].ToString();
                cbEnd.SelectedItem = endYearlist[7].ToString();
                toc = "204";
                oseries = "X1123BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Oil")
            {
                cbStart.SelectedItem = startYearlist[4].ToString();
                cbEnd.SelectedItem = endYearlist[7].ToString();
                toc = "205";
                oseries = "X1124BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Electric")
            {
                cbStart.SelectedItem = startYearlist[4].ToString();
                cbEnd.SelectedItem = endYearlist[5].ToString();
                toc = "206";
                oseries = "X111XBMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Electric Transmission")
            {
                cbStart.SelectedItem = startYearlist[4].ToString();
                cbEnd.SelectedItem = endYearlist[5].ToString();
                toc = "207";
                oseries = "X1116BMO";
            }
            else if (cbSeries.SelectedValue.ToString() == "Improvements")
            {
                cbStart.SelectedItem = startYearlist[4].ToString();
                cbEnd.SelectedItem = endYearlist[5].ToString();
                toc = "208";
                oseries = "X0013BMO";
            }        
        }

        //include this code to prevent mouse scrolling in dropdownlist
        private void CbSeries_MouseWheel(object sender, MouseEventArgs e)
        {
            HandledMouseEventArgs hme = e as HandledMouseEventArgs;
            if (hme != null)
            {
                hme.Handled = true;
            }
        }

        private int start;
        
        private void cbStart_SelectedIndexChanged(object sender, EventArgs e)
        {
            start = Convert.ToInt32(cbStart.SelectedItem);
        }

        private int end;

        private void cbEnd_SelectedIndexChanged(object sender, EventArgs e)
        {
            end = Convert.ToInt32(cbEnd.SelectedItem);
        }

        //verify data for selected series and update start and end dates
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (start >= end)
            {
                MessageBox.Show("Start and End Years are not valid");
            }
            else
            {
                DateTime startmonth = DateTime.ParseExact((start - 1).ToString() + "12", "yyyyMM", System.Globalization.CultureInfo.InvariantCulture);
                DateTime endmonth = DateTime.ParseExact(end.ToString() + "12", "yyyyMM", System.Globalization.CultureInfo.InvariantCulture);
               
                bool months_valid = false;

                //if month has missing data or 0 value vipdata display message//
                while (startmonth <= endmonth)
                {
                    if (!data_object.CheckMonthExistsWithData(startmonth.ToString("yyyyMM"), toc))
                    {
                        MessageBox.Show("Monthly Data for Series is missing or has zero values");
                        months_valid = false;
                        break;
                    }
                    else
                    {
                        months_valid = true;
                    }

                    startmonth = startmonth.AddMonths(1);
                }

                //if months have valid data check to see if start and end year have missing or 0 vipdata
                if (months_valid)
                {
                    //if manufacturing only check two values
                    if (cbSeries.SelectedValue.ToString() == "Manufacturing")
                    {
                        if (!(data_object.CheckYearExistsWithData((start - 1).ToString(), toc) &&
                             data_object.CheckYearExistsWithData(end.ToString(), toc)))
                        {
                            MessageBox.Show("Annual Data for Series is missing or has zero values");
                        }
                        else
                        {
                            data_object.UpdateVipBenchmark((start - 1).ToString(), end.ToString(), oseries);
                            MessageBox.Show("Vip Benchmark updated successfully");
                        }
                    }
                    //if not manufacturing loop through years
                    else
                    {
                        DateTime startyear = DateTime.ParseExact((start - 1).ToString(), "yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        DateTime endyear = DateTime.ParseExact((end).ToString(), "yyyy", System.Globalization.CultureInfo.InvariantCulture);
                        bool year_valid = false;

                        //if year has missing data or 0 value vipdata display message//
                        while (startyear <= endyear)
                        {
                            if (!data_object.CheckYearExistsWithData((startyear).ToString("yyyy"), toc))
                            {
                                MessageBox.Show("Annual Data for Series is missing or has zero values");
                                year_valid = false;
                                break;
                            }
                            else
                            {
                                year_valid = true;
                            }

                            startyear = startyear.AddYears(1);
                        }

                        if (year_valid == true)
                        {
                            data_object.UpdateVipBenchmark((start - 1).ToString(), end.ToString(), oseries);
                            MessageBox.Show("Vip Benchmark updated successfully");
                        }
                    }
                }
            }
        }

        private void frmBenExport_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
