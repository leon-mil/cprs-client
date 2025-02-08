/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       Cprs.GeneralFunctions.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2015
Inputs:             None
Parameters:	        None 
Outputs:	        
Description:	    This class create to store gneral functions are used in whole cprs project
                    
Detailed Design:    None 
Other:	            
 
Revision History:	
*********************************************************************
 Modified Date : 2/7/2025 
 Modified By   : Leon Mil, Christine Zhang 
 Keyword       :  
 Change Request: N/A  
 Description   : The `ExtractYearFromSDate` method extracts the four-digit year from a 6-character "yyyymm" string, validating input and throwing an `ArgumentException` if invalid. 
                 Added to GeneralFunctions.cs for standardized year extraction and **frmTableMonRelease.cs** to improve `start_year` handling, enhancing readability, accuracy, and consistency.
****************************************************************************************/

using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Net.Mail;
using System.IO;
using System.Linq;

namespace Cprs
{
    public static class GeneralFunctions
    {
        public static string Wrap(this string str, int maxLength)
        {
            return Wrap(str, maxLength, "");
        }

        /// <summary>
        /// Forces the string to word wrap so that each line doesn't exceed the maxLineLength.
        /// </summary>
        /// <param name="str">The string to wrap.</param>
        /// <param name="maxLength">The maximum number of characters per line.</param>
        /// <param name="prefix">Adds this string to the beginning of each line.</param>
        /// <returns></returns>

        public static string Wrap(this string str, int maxLength, string prefix)
        {
            if (string.IsNullOrEmpty(str)) return "";
            if (maxLength <= 0) return prefix + str;

            var lines = new List<string>();

            // breaking the string into lines makes it easier to process.
            foreach (string line in str.Split("\n".ToCharArray()))
            {
                var remainingLine = line.Trim();
                do
                {
                    var newLine = GetLine(remainingLine, maxLength - prefix.Length);
                    lines.Add(newLine);
                    remainingLine = remainingLine.Substring(newLine.Length).Trim();
                    // Keep iterating as int as we've got words remaining 
                    // in the line.
                } while (remainingLine.Length > 0);
            }

            return string.Join(Environment.NewLine + prefix, lines.ToArray());
        }

        private static string GetLine(string str, int maxLength)
        {
            // The string is less than the max length so just return it.
            if (str.Length <= maxLength) return str;

            // Search backwords in the string for a whitespace char
            // starting with the char one after the maximum length
            // (if the next char is a whitespace, the last word fits).
            for (int i = maxLength; i >= 0; i--)
            {
                if (char.IsWhiteSpace(str[i]))
                    return str.Substring(0, i).TrimEnd();
            }

            // No whitespace chars, just break the word at the maxlength.
            return str.Substring(0, maxLength);
        }

        //Check email address
        public static bool isEmail(string inputEmail)
        {
            // There must be exactly one @

            int firstAtSign = inputEmail.IndexOf("@");
            if (firstAtSign == -1)
                return false;

            int lastAtSign = inputEmail.LastIndexOf("@");
            if (lastAtSign != firstAtSign)
                return false;

            string local = inputEmail.Substring(firstAtSign+1);

            // Can't begin or end with . or have two .. in a row.
            if (!ValidatePeriodInEmail(local))
                return false;

            return true;
        }

        private static bool ValidatePeriodInEmail(string label)
        {
            if (string.IsNullOrEmpty(label))
                return false;

            int pindex = label.IndexOf(".");
            if (pindex == -1)
                return false;

            // Can't have two periods in a row.
            if (label.Contains(".."))
                return false;

            // Can't begin or end with a period.
            if (label[0] == '.')
                return false;

            if (label[label.Length - 1] == '.')
                return false;

            return true;
        }

        //check the string has special chars or not
        public static bool HasSpecialCharsInCityState(string check_string)
        {
            var regexItem = new Regex("^[a-zA-Z\x20'-]*$");

            if (regexItem.IsMatch(check_string))
            {
                return (false);
            }
            else
            {
                return (true);
            }
        }


        //Get the current month and year
        public static string CurrentYearMon()
        {
            string currentMonth = DateTime.Now.ToString("MM");
            string currentYear = DateTime.Now.Year.ToString();
            string CurYearMon = currentYear + currentMonth;
            return CurYearMon;
        }

        /* The ExtractYearFromSDate method extracts the year from a 6-character "yyyymm" string, 
         * ensuring valid input. It standardizes year extraction and improves `start_year` 
         * handling for accuracy and consistency.      
         */
        public static int ExtractYearFromSDate(string sdate)
        {
            if (string.IsNullOrWhiteSpace(sdate))
                throw new ArgumentException("sdate cannot be null, empty, or whitespace.");

            if (sdate.Length != 6 || !int.TryParse(sdate.Substring(0, 4), out int year))
                throw new ArgumentException("sdate must be in 'yyyymm' format");

            return year;
        }

        /*text box enter event */

        public static void TextBoxEnter(object sender)
        {
            //validate the text
            Control ctr = (Control)sender;

            ctr.Tag = ctr.BackColor;
            ctr.BackColor = Color.Yellow;
        }

        /*text box leave event */

        public static void TextBoxLeave(object sender)
        {
            //validate the text
            Control ctr = (Control)sender;
            ctr.BackColor = (Color)ctr.Tag;
        }

        /*check the input of textbox reach to max length*/
        /*if maxlen is 0, check the length greater than 0 */

        public static bool TextBoxKeyDownCheckMaxLen(object sender, KeyEventArgs e, int maxlen)
        {
            TextBox ctr = (TextBox)sender;
            if ((e.KeyCode == Keys.Enter) && !e.Handled)
            {
                if (maxlen == 0)
                {
                    if (ctr.TextLength > maxlen )
                        e.Handled = true;
                }
                else if (ctr.TextLength == maxlen )
                    e.Handled = true;

                if (e.Handled)
                    return true;
            }

            return false;
        }

        /*Check the text input is integer */

        public static void CheckIntegerField(object sender, string field_name)
        {
            //create a regular expression to check for a number
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");

            //validate the text
            Control ctr = (Control)sender;

            if (!String.IsNullOrEmpty(ctr.Text))
            {
                if (!regex.IsMatch(ctr.Text))
                {
                    MessageBox.Show("The " + field_name + " value must be all numeric.", "Entry Error");
                    ctr.Text = "";
                }
            }
        }

        /*Verify two textbox fields, two fields cannot be empty.  Second field is greater than first field */

        public static Boolean VerifyBetweenParameters(string field1, string field2, string field_name)
        {
            Boolean result = false;

            if (field1.Length > 0 && field2.Length > 0)
            {
                if (Convert.ToInt32(field1) >= Convert.ToInt32(field2))
                {
                    MessageBox.Show("The Second " + field_name + " should be greater than the first\n\n", "Invalid Input", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                    return result;
                }
            }
            else if (field1.Length > 0 || field2.Length > 0)
            {
                if (String.IsNullOrEmpty(field1))
                {
                    MessageBox.Show("The first " + field_name + " fields cannot be empty\n\n", "Invalid Input", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return result;
                }
                else if (String.IsNullOrEmpty(field2))
                {
                    MessageBox.Show("The second " + field_name + " fields cannot be empty\n\n", "Invalid Input", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return result;
                }
            }

            return result = true;
        }

        /*Verify date input */

        public static bool VerifyDate(string d)
        {
            DateTime tempDate;

            return DateTime.TryParse(d, out tempDate) ? true : false;
        }

        /*Check it is a good date string or not */
        public static bool ValidateDateWithRange(string input_date)
        {
            input_date = input_date.Trim();
            if (input_date.Length != 0)
            {
                if (input_date.Length != 6)
                {
                    return false;
                }
                else
                {
                    string input_year = input_date.Substring(0, 4);
                    string input_mon = input_date.Substring(4, 2);

                    if (Convert.ToInt32(input_year) < 1990 || Convert.ToInt32(input_year) > 2050)
                    {
                        return false;
                    }
                    if (Convert.ToInt32(input_mon) > 12 || Convert.ToInt32(input_mon) == 0)
                        return false;
                }
            }

            return true;
        }

       
        /*Convert M/D/YYYY to MM/DD/YYYY*/

        public static string ConvertDateFormat(string date_convert)
        {
            DateTime dt = DateTime.Parse(date_convert);
            string newdate = dt.ToString("MM/dd/yyyy");
            return newdate;
        }

        /*Convert operator to symbol */
        public static string ConvertOperatorToSymbol(string so)
        {
            string ostr = string.Empty;

            if (so == "Equals")
                ostr = "=";
            else if (so == "StartsWith")
                ostr = "StartsWith";
            else if (so == "LessThanOrEqual")
                ostr = "<=";
            else if (so == "GreaterThanOrEqual")
                ostr = ">=";
            else if (so == "Between")
                ostr = "In";

            return ostr;
        }

        /*Split words into arrat */
        public static string[] SplitWords(string s)
        {
            //
            // Split on all non-word characters.
            // ... Returns an array of all the words.
            //
            return Regex.Split(s, @"\W+");
            // @      special verbatim string syntax
            // \W+    one or more non-word characters together
        }

        /*find different months between two dates */
        public static int GetNumberMonths(this DateTime dt1, DateTime dt2, bool use_abs = false)
        {
            DateTime earlyDate;
            DateTime lateDate;
            if (use_abs)
            {
                earlyDate = (dt1 > dt2) ? dt2.Date : dt1.Date;
                lateDate = (dt1 > dt2) ? dt1.Date : dt2.Date;
            }
            else
            {
                earlyDate =  dt1.Date;
                lateDate = dt2.Date;
            }

            // Start with 1 month's difference and keep incrementing
            // until we overshoot the late date
            int monthsDiff = 1;
            while (earlyDate.AddMonths(monthsDiff) <= lateDate)
            {
                monthsDiff++;
            }

            return monthsDiff - 1;
        }

        //Get image of form screen
        public static Bitmap CaptureScreen(Form passed_form)
        {
            Graphics myGraphics = passed_form.CreateGraphics();
            Size s = passed_form.Size;
            Bitmap memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(passed_form.Location.X, passed_form.Location.Y, 0, 0, s);

            return memoryImage;

        }

        //Print screen image
        public static void PrintCapturedScreen(Bitmap screenImage, string username, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Font font = new Font("Times New Roman", 10.0f);
            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

            //Draw Page number
            e.Graphics.DrawString("Page 1 ", font, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                    e.Graphics.MeasureString("Page 1   " + strDate, font, e.MarginBounds.Width).Width), 10);

            //Draw Date
            e.Graphics.DrawString(strDate, font, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, font, e.MarginBounds.Width).Width), 10);

            //Draw username
            e.Graphics.DrawString(username, font, Brushes.Black, 40, 10);

            e.Graphics.DrawImage(screenImage, 40, 40, 950, 700);

        }

        //send email function
        public static bool SendEmail(string subject, string messagebody, string fromAddress, List<string> toAddresslist)
        {
            //set up smtp server
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "mh-i.census.gov";
            smtp.Port = 25;
            smtp.EnableSsl = false;
            smtp.Credentials = new System.Net.NetworkCredential("username", "password");

            //set up mail
            MailMessage email = new MailMessage();
            email.Subject = subject;
            email.Body = messagebody;

            foreach (string eto in toAddresslist)
            {
                email.To.Add(new MailAddress(eto));
            }

            email.From = new MailAddress(fromAddress);
            email.Body = messagebody;

            try
            {
                smtp.Send(email);
                email.Dispose();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        //get survey text based on owner
        public static string GetSurveyText(string owner)
        {
            if (owner == "P")
                return "State and Local";
            else if (owner == "N")
                return "Nonresidential";
            else if (owner == "F")
                return "Federal";
            else if (owner == "M")
                return "Multifamily";
            else
                return "Utilities";
        }

        //delete file
        public static void DeleteFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                    //Could not delete the file, wait and try again
                    try
                    {
                        System.GC.Collect();
                        System.GC.WaitForPendingFinalizers();
                        File.Delete(fileName);
                    }
                    catch
                    {
                       
                    }
                }
            }
        }

        //release object
        public static void releaseObject(object obj)
        {
            try
            {
                while (System.Runtime.InteropServices.Marshal.ReleaseComObject(obj) != 0) { }
                 System.Runtime.InteropServices.Marshal.FinalReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }  
        }

        //get first work day from year and month
        public static DateTime GetFirstBusinessDay(int Year, int Month)
        {
            DateTime FirstOfMonth = default(DateTime);
            DateTime FirstBusinessDay = default(DateTime);
            FirstOfMonth = new DateTime(Year, Month, 1);
            if (FirstOfMonth.DayOfWeek == DayOfWeek.Sunday)
            {
                FirstBusinessDay = FirstOfMonth.AddDays(1);
            }
            else if (FirstOfMonth.DayOfWeek == DayOfWeek.Saturday)
            {
                FirstBusinessDay = FirstOfMonth.AddDays(2);
            }
            else
            {
                FirstBusinessDay = FirstOfMonth;
            }
            return FirstBusinessDay;
        }

        //Get number of work day for today
        public static int GetNumBusinessDayforToday(int Year, int Month)
        {
            int num_day = 0;
            var currentDate = DateTime.Now;
            var monthIterator = new DateTime(Year, Month, 1);

            HashSet<DateTime> holiday_table = GeneralFunctions.GetHolidays(Year);

            int ddays = DateTime.DaysInMonth(Year, Month);
            for (int i = 0; i < ddays; ++i)
            {
                if (monthIterator <= currentDate)
                {
                    if (!monthIterator.Date.ToString("D").Contains("Sat") && !monthIterator.Date.ToString("D").Contains("Sun") && !holiday_table.Contains(monthIterator))
                        num_day++;

                    monthIterator = monthIterator.AddDays(1);
                }
            }
            return num_day;
        }

        //Get number of work day from a day to a day
        public static int GetBusinessDayBetweenDays(DateTime from, DateTime to)
        {
            var totalDays = 0;
            HashSet<DateTime> holiday_table = GeneralFunctions.GetHolidays(from.Year);
            for (var date = from.AddDays(1); date <= to; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday && !holiday_table.Contains(date))
                    totalDays++;
            }

            return totalDays;
        }
       

        public static string GetNextBusinessDay(DateTime seed_day)
        {
            string nextworkday = "";

            HashSet<DateTime> holiday_table = GeneralFunctions.GetHolidays(seed_day.Year);
            for (int i = 1; i < 5; ++i)
            {
                DateTime nextDay = seed_day.AddDays(i);
                if (!nextDay.Date.ToString("D").Contains("Sat") && !nextDay.Date.ToString("D").Contains("Sun") && !holiday_table.Contains(nextDay))
                {
                    nextworkday = nextDay.ToString("MMdd");
                    break;
                    
                }
            }

            return nextworkday;
        }

        public static HashSet<DateTime> GetHolidays(int year)
        {
            HashSet<DateTime> holidays = new HashSet<DateTime>();

            // New Years
            DateTime newYearsDate = AdjustForWeekendHoliday(new DateTime(year, 1, 1));
            holidays.Add(newYearsDate);

            // Memorial Day -- last monday in May 
            DateTime memorialDay = new DateTime(year, 5, 31);
            DayOfWeek dayOfWeek = memorialDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                memorialDay = memorialDay.AddDays(-1);
                dayOfWeek = memorialDay.DayOfWeek;
            }
            holidays.Add(memorialDay);

            // Independence Day
            DateTime independenceDay = AdjustForWeekendHoliday(new DateTime(year, 7, 4));
            holidays.Add(independenceDay);

            // Labor Day -- 1st Monday in September 
            DateTime laborDay = new DateTime(year, 9, 1);
            dayOfWeek = laborDay.DayOfWeek;
            while (dayOfWeek != DayOfWeek.Monday)
            {
                laborDay = laborDay.AddDays(1);
                dayOfWeek = laborDay.DayOfWeek;
            }
            holidays.Add(laborDay);


            // Thanksgiving Day -- 4th Thursday in November 
            var thanksgiving = (from day in Enumerable.Range(1, 30)
                                where new DateTime(year, 11, day).DayOfWeek == DayOfWeek.Thursday
                                select day).ElementAt(3);
            DateTime thanksgivingDay = new DateTime(year, 11, thanksgiving);
            holidays.Add(thanksgivingDay);

            // Christmas Day 
            DateTime christmasDay = AdjustForWeekendHoliday(new DateTime(year, 12, 25));
            holidays.Add(christmasDay);

            // Next year's new years check
            DateTime nextYearNewYearsDate = AdjustForWeekendHoliday(new DateTime(year + 1, 1, 1));
            if (nextYearNewYearsDate.Year == year)
                holidays.Add(nextYearNewYearsDate);

            return holidays;
        }

        private static DateTime AdjustForWeekendHoliday(DateTime holiday)
        {
            if (holiday.DayOfWeek == DayOfWeek.Saturday)
            {
                return holiday.AddDays(-1);
            }
            else if (holiday.DayOfWeek == DayOfWeek.Sunday)
            {
                return holiday.AddDays(1);
            }
            else
            {
                return holiday;
            }
        }

        public static bool IsFileinUse(string filename)
        {
            FileInfo file = new FileInfo(filename);
            FileStream stream = null;

            try
            {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }
            finally
            {
                if (stream != null)
                    stream.Close();
            }
            return false;
        }


    }
}
