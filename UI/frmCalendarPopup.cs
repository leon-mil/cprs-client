/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmCalendarPopup.cs
Programmer    : Christine Zhang
Creation Date : Sept. 7 2017
Parameters    : MinDate,  MaxDate,  SelectionDate
Inputs        : checkType
Outputs       : Selectiondate
Description   : create a popup screen to select calendar day
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : call from frmUnavailableDays.cs
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

namespace Cprs
{
    public partial class frmCalendarPopup : Form
    {
        public frmCalendarPopup(DateTime MinDate, DateTime MaxDate, DateTime SelectionDate)
        {
            InitializeComponent();
            Mindate = MinDate;
            Maxdate = MaxDate;
            Selectiondate = SelectionDate;   
        }

        //public variable
        public DateTime SelectedDate = DateTime.MinValue;
        public string CheckType = "";

        //private variables
        private HashSet<DateTime> holiday_table;
        private DateTime Maxdate;
        private DateTime Mindate;
        private DateTime Selectiondate;

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void frmCalendarPopup_Load(object sender, EventArgs e)
        {
           
        }

        private void frmCalendarPopup_Shown(object sender, EventArgs e)
        {
            Calendar1.MinDate = Mindate;
            Calendar1.MaxDate = Maxdate;
            Calendar1.SelectionStart = Selectiondate;
            Calendar1.SelectionEnd = Selectiondate;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Calendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            holiday_table = GeneralFunctions.GetHolidays(Mindate.Year);

            //Validate weekend and holiday
            if (e.Start.DayOfWeek.ToString() == "Saturday" || e.Start.DayOfWeek.ToString() == "Sunday" || holiday_table.Contains(e.Start))
            {
                MessageBox.Show("Weekends and Holidays are not selectable.");
                return;
            }

            UnavailableDaysData data_object = new UnavailableDaysData();

            //check the date has exist or not
            if (CheckType == "Add")
            {
                if (data_object.CheckDuplicateDay(e.Start.Month.ToString("00"), e.Start.Day.ToString("00"), e.Start.Year.ToString(), "N"))
                {
                    MessageBox.Show("The date selected is already an unavailable day.");
                    return;
                }
                if (data_object.CheckDuplicateDay(e.Start.Month.ToString("00"), e.Start.Day.ToString("00"), e.Start.Year.ToString(), "C"))
                {
                    MessageBox.Show("Cut date cannot be made unavailable.");
                    return;
                }
            }
            else if (CheckType == "Cut")
            {
                if (data_object.CheckDuplicateDay(e.Start.Month.ToString("00"), e.Start.Day.ToString("00"), e.Start.Year.ToString(), "N"))
                {
                    MessageBox.Show("Cut date cannot be an unavailable day.");
                    return;
                }
            }
            
            SelectedDate = e.Start;

            this.Close();
        }

    }
}
