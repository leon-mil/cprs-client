/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmCalendarPopup.cs
Programmer    : Christine Zhang
Creation Date : Nov. 13 2017
Parameters    : id,  respid,  cut_date
Inputs        : 
Outputs       : Apptdate, Apptime, Apptend
Description   : create a popup screen to select resched time
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : call from frmTFU.cs
 ***********************************************************************
Modified Date :12/15/2020
Modified By   :Christine Zhang
Keyword       :
Change Request:
Description   :set Respondent time as default
***********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;
using System.Globalization;

namespace Cprs
{
    public partial class frmReschedulePopup : Form
    {
        public string Apptdate;
        public string Appttime;
        public string Apptend;

        private string id;
        private string respid;
        private DateTime cut_date;
        private Respondent resp;
        private int timeDifference =0;
        public frmReschedulePopup(string passed_id, string passed_respid)
        {
            InitializeComponent();
            id = passed_id;
            respid = passed_respid;
        }

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

        private void frmReschedulePopup_Load(object sender, EventArgs e)
        {
            RespondentData respdata = new RespondentData();

            lblId.Text = id;

            //get respondent data
            resp = respdata.GetRespondentData(respid);
            lblAddr.Text= resp.Addr3;
            lblRespid.Text = resp.Respid;
            lblOwner.Text = resp.Resporg;
            lblTimezone.Text = GeneralDataFuctions.GetTimezone(resp.Rstate);
            lblResptime.Text = GeneralDataFuctions.GetTimezoneCurrentTime(resp.Rstate);
            lbleInttime.Text = DateTime.Now.ToString();

            //set datetime picker1 for date
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "MMdd";
            
            UnavailableDaysData undata = new UnavailableDaysData();
            int cut_day = Convert.ToInt32(undata.GetUnavailableCutDay(DateTime.Now.Year.ToString(), DateTime.Now.Month.ToString("d2")));
            
            cut_date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, cut_day);
            if (cut_day == DateTime.Now.Day)
            {
                dateTimePicker1.Value = cut_date;
                dateTimePicker1.Enabled = false;
            }
            else
            {
                dateTimePicker1.MinDate = DateTime.Now;
                dateTimePicker1.MaxDate = cut_date;
                dateTimePicker1.Enabled = true;
            }

            TimeSpan timespan;
            timespan = Convert.ToDateTime(lblResptime.Text) - DateTime.Now;
            timeDifference = timespan.Hours;
        }

        //check is valid date time
        private bool IsValidDateTime(string date_time, string date_time_format)
        {
            DateTime dateTime;
            return DateTime.TryParseExact(date_time, date_time_format, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime enter_date = DateTime.Now;
            DateTime  begin_time = DateTime.Now;
            DateTime  end_time = DateTime.Now;

            //check three fields have been filled or not
            if (String.IsNullOrEmpty(dateTimePicker1.Text) || String.IsNullOrEmpty(txtEtime.Text) || String.IsNullOrEmpty(txtBtime.Text))
            {
                MessageBox.Show("All fields must be entered");
                DialogResult = DialogResult.None;
                return;
            }

            //verify date time fields
            if (!IsValidDateTime(txtBtime.Text, "HHmm"))
            {
                MessageBox.Show("Begin Time is not valid time");
                txtBtime.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            if (!IsValidDateTime(txtEtime.Text, "HHmm"))
            {
                MessageBox.Show("End Time is not valid time");
                DialogResult = DialogResult.None;
                txtEtime.Focus();
                return;
            }

            enter_date = dateTimePicker1.Value;

            //check begin time's hour is in business hours
            List<int> workhours = new List<int>() { 8, 9,10, 11, 12, 1, 2, 3, 4, 5 };
            List<int> afterhours = new List<int>() { 1, 2, 3, 4, 5};
            int hour = Convert.ToInt16(txtBtime.Text.Substring(0, 2));

            // if in respondent time
            if (rdRespondent.Checked)
            {
                hour = hour - timeDifference;
                if (hour > 12)
                    hour = hour - 12;
            }

            if (!workhours.Contains(hour) || (hour == 5 && (txtBtime.Text.Substring(2, 2) != "00")))
            {
                MessageBox.Show("Adjusted begin time will be outside of NPC work time");
                txtBtime.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            //convert to 24 hours
            if (hour <= 5)
                hour = hour + 12;
            
            begin_time = new DateTime(DateTime.Now.Year, enter_date.Month, enter_date.Day, hour, Convert.ToInt16(txtBtime.Text.Substring(2, 2)), 0);

            //verify begin time, must be greater than current time
            if (begin_time <= DateTime.Now)
            {
                MessageBox.Show("Begin Time must be greater than current time");
                txtBtime.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            //check end time's hour is in business hours
            hour = Convert.ToInt16(txtEtime.Text.Substring(0, 2));

            // if in respondent time
            if (rdRespondent.Checked)
            {
                hour = hour - timeDifference;
                if (hour > 12)
                    hour = hour - 12;
            }

            if (!workhours.Contains(hour) || (hour == 5 && (txtEtime.Text.Substring(2, 2)) != "00"))
            {
                // MessageBox.Show("End Time must be in 8:00am to 5:00pm NPC time");
                MessageBox.Show("Adjusted end time will be outside of NPC work time");
                txtEtime.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            //convert to 24 hours
            if (hour <= 5)
                hour = hour + 12;

            end_time = new DateTime(DateTime.Now.Year, enter_date.Month, enter_date.Day, hour, Convert.ToInt16(txtEtime.Text.Substring(2, 2)), 0);

            //verfiy end time, must be greater than or equal to begin time
            if (end_time < begin_time)
            {
                MessageBox.Show("End Time must be greater than or equal begin time");
                txtEtime.Focus();
                DialogResult = DialogResult.None;
                return;
            }

            Apptdate = dateTimePicker1.Value.ToString("MMdd"); 
            Appttime =begin_time.ToString("HHmm");
            Apptend = end_time.ToString("HHmm");

        }

       private void txtdate_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtBtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtEtime_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            HashSet<DateTime> holiday_table = GeneralFunctions.GetHolidays(DateTime.Now.Year);

            if (dateTimePicker1.Value.DayOfWeek == DayOfWeek.Saturday || dateTimePicker1.Value.DayOfWeek == DayOfWeek.Sunday || holiday_table.Contains(dateTimePicker1.Value))
            {
                MessageBox.Show("Weekends and Holidays cannot be selected.");
                dateTimePicker1.Text = "";
                return;
            }
        }

    }
}
