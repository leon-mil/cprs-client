/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmUnavailableDays.cs
Programmer    : Christine Zhang
Creation Date : Sept. 7 2017
Parameters    : 
Inputs        : N/A
Outputs       : N/A
Description   : create unavailable Days screen to view or update unavailable days
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
***********************************************************************/
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
using System.IO;
using System.Management;

namespace Cprs
{
    public partial class frmUnavailableDays : frmCprsParent
    {
        private UnavailableDaysData data_object;
        private DateTime dtime;
        private string first_month;
        private string second_month;
        
        private string cut_day;
        private DateTime firstDayInMonth;
        private DateTime lastDayInMonth;

        public frmUnavailableDays()
        {
            InitializeComponent();
        }

        private void frmUnavailableDays_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            //file combo date 
            dtime = DateTime.Now; 
            DateTime dtime1 = dtime.AddMonths(1);

            first_month = dtime.Month.ToString("00") + dtime.Year.ToString();
            second_month = dtime1.Month.ToString("00") + dtime1.Year.ToString();
            string current_month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dtime.Month);
            string next_month = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dtime1.Month);

            data_object = new UnavailableDaysData();

            cbMon.Items.Add(current_month + " " + dtime.Year);
            cbMon.Items.Add(next_month + " " + dtime1.Year);
            cbMon.SelectedIndex = 0;

            //get data 
            GetData();

            //set up buttons

            btnAdd.Enabled = false;
            btnCut.Enabled = false;
            btnDelete.Enabled = false;

            //Only HQ manager and programmer can do add, delete and cut
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                btnAdd.Enabled = true;
                btnCut.Enabled = true;
                btnDelete.Enabled = true;
            }
            
        }

        private void GetData()
        {
            string selected_month;
            string selected_year;

            //get selected month number
            if (cbMon.SelectedIndex == 0)
            {
                selected_month = first_month.Substring(0, 2);
                selected_year = first_month.Substring(2, 4);
            }
            else
            {
                selected_month = second_month.Substring(0, 2);
                selected_year = second_month.Substring(2, 4);
            }

            DataTable dt = data_object.GetUnavailableDays(selected_year, selected_month);

            //populate the data grid
            dgData.DataSource = dt;

            //format the datagrid and columns
            dgData.RowHeadersVisible = false;  // set it to false if not needed
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgData.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            for (int i = 0; i < dgData.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    dgData.Columns[i].HeaderText = "WEEKDAY";
                    dgData.Columns[i].Width = 240;
                }
                if (i == 1)
                {
                    dgData.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    
                    dgData.Columns[i].HeaderText = "DAY";
                    dgData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                }
            }

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            
            //get cut day
            cut_day = data_object.GetUnavailableCutDay(selected_year, selected_month);
            string month_name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt16(selected_month));
            txtCut.Text =  month_name + " " + cut_day;
        }


        private void cbMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtime = DateTime.Now;
            DateTime dtime1 = dtime.AddMonths(1);
           
            if (cbMon.SelectedIndex == 0)
            {
                firstDayInMonth = new DateTime(dtime.Year, dtime.Month, 1);
                lastDayInMonth = new DateTime(dtime.Year, dtime.Month, DateTime.DaysInMonth(dtime.Year, dtime.Month));
            }
            else
            {
                firstDayInMonth = new DateTime(dtime1.Year, dtime1.Month, 1);
                lastDayInMonth = new DateTime(dtime1.Year, dtime1.Month, DateTime.DaysInMonth(dtime1.Year, dtime1.Month)); 
            }

            GetData();
        }

        private void frmUnavailableDays_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            DateTime selection_date;
            if (cbMon.SelectedIndex == 0)
                selection_date = dtime;
            else
                selection_date = dtime.AddMonths(1);

            frmCalendarPopup popup = new frmCalendarPopup(firstDayInMonth, lastDayInMonth, selection_date);
            popup.CheckType = "Add";
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
            DateTime sday = popup.SelectedDate;
            if (sday == DateTime.MinValue)
                return;

            //update flag to Unavailable
            data_object.UpdateUnavailableDays(sday.Month.ToString("00"), sday.Day.ToString("00"), sday.Year.ToString(), "N");

            //reload data
            GetData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string days = dgData.SelectedRows[0].Cells[1].Value.ToString().Trim();
            string dayofweek = dgData.SelectedRows[0].Cells[0].Value.ToString().Trim();

            string selected_month;
            string selected_year;

            //get selected month number
            if (cbMon.SelectedIndex == 0)
            {
                selected_month = first_month.Substring(0, 2);
                selected_year = first_month.Substring(2, 4);
            }
            else
            {
                selected_month = second_month.Substring(0, 2);
                selected_year = second_month.Substring(2, 4);
            }
            HashSet<DateTime> holiday_table = GeneralFunctions.GetHolidays(Convert.ToInt16(selected_year));
            DateTime ddate = new DateTime(Convert.ToInt16(selected_year), Convert.ToInt16(selected_month), Convert.ToInt16(days));
            if (dayofweek == "Saturday" || dayofweek == "Sunday" || holiday_table.Contains(ddate))
            {
                MessageBox.Show("Weekends and Holidays cannot be deleted.");
                return;
            }
            data_object.UpdateUnavailableDays(selected_month, days, selected_year, "A");
            GetData();
           
        }

        private void btnCut_Click(object sender, EventArgs e)
        {
            DateTime selection_date;
            string selected_month;
            string selected_year;

            //get selected month number
            if (cbMon.SelectedIndex == 0)
            {
                selected_month = first_month.Substring(0, 2);
                selected_year = first_month.Substring(2, 4);
            }
            else
            {
                selected_month = second_month.Substring(0, 2);
                selected_year = second_month.Substring(2, 4);
            }
            selection_date = new DateTime(Convert.ToInt16(selected_year), Convert.ToInt16(selected_month), Convert.ToInt16(cut_day));

            frmCalendarPopup popup = new frmCalendarPopup(firstDayInMonth, lastDayInMonth, selection_date);
            popup.CheckType = "Cut";
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
            
            DateTime sday = popup.SelectedDate;
            if (sday == DateTime.MinValue)
                return;
            
            //update old cut day to available day
            data_object.UpdateUnavailableDays(sday.Month.ToString("00"), cut_day, sday.Year.ToString(), "A");

            //update selected day to cut day
            data_object.UpdateUnavailableDays(sday.Month.ToString("00"), sday.Day.ToString("00"), sday.Year.ToString(), "C");

            //display cut day in textbox
            string month_name = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(sday.Month);
            txtCut.Text = month_name + " " + sday.Day.ToString("00");
            cut_day = sday.Day.ToString("00");
        }
        
    }
}
