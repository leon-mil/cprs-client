/************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmMonthlyProcessPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/28/2016
Inputs:             CprsDAL.MonthlyProcessData tables
Parameters:         None              
Outputs:            None
Description:	    This program runs the Monthly Processing popup
                    displays current month process.

Detailed Design:    Detailed Design for Monthly
                    Processing
Other:	            Called from: frmMonthlyProcess
Revision History:	
*********************************************************************
Modified Date   :   02/16/2018
Modified By     :   Kevin Montgomery
Keyword         :   20180216kjm
Change Request  :   CR 349
Description     :   Remove  Run Email Reminder Job
*********************************************************************
Modified Date  :  11/18/2019
 Modified By   :  Christine Zhang
 Keyword       :  
 Change Request:  CR 3751
 Description   :  add special vip load button
*********************************************************************
Modified Date   :   11/20/2024
Modified By     :   Leon Mil
Keyword         :   20241120-stat-period
Change Request  :   Configurable statistical period selection
Description     :   Documented the statistical-period handoff and batch
                    argument flow for monthly processing tasks.
**********************************************************************/
using CprsBLL;
using CprsDAL;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Cprs
{
    /// <summary>
    /// Popup form that orchestrates monthly processing tasks for a selected statistical period.
    /// </summary>
    public partial class frmMonthlyProcessPopup : Form
    {
        private const int CP_NOCLOSE_BUTTON = 0x200;

        /// <summary>
        /// Prevents the close button from appearing on the form's title bar.
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }
        
        private readonly MonthlyProcessingContext processingContext;
        private readonly string currMonth;
        private readonly string currMonDate;

        private MonthlyProcessData dataObject;
        private CurrentUsersData cd;
        
        /// <summary>
        /// Initializes the popup form using the configured statistical period.
        /// </summary>
        public frmMonthlyProcessPopup()
           : this(MonthlyProcessingSelector.GetCurrentContext())
        {
        }

        /// <summary>
        /// Initializes the popup form with the supplied statistical period.
        /// </summary>
        /// <param name="statisticalPeriod">The yyyyMM period to process.</param>
        /// <exception cref="ArgumentException">Thrown when no period is provided.</exception>
        public frmMonthlyProcessPopup(MonthlyProcessingContext processingContext)
        {
            this.processingContext = processingContext ?? throw new ArgumentNullException(nameof(processingContext));
            currMonth = this.processingContext.StatisticalPeriod;            
            currMonDate = DateTime.Today.ToString("dd-MMM-yyyy").ToUpper();
            InitializeComponent();
        }

        /// <summary>
        /// Closes the popup window.
        /// </summary>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Loads the current processing data for the selected month and initializes UI elements.
        /// </summary>
        private void frmMonthlyProcessPopup_Load(object sender, EventArgs e)
        {
            
            lblTitle.Text = currMonth + " MONTHLY PROCESSING";
            dataObject = new MonthlyProcessData();
            cd = new CurrentUsersData();

            LoadForm();
        }
        
        /// <summary>
        /// Retrieves processing data for the current month and updates control states accordingly.
        /// </summary>
        private void LoadForm()
        {
            // Get current month data           
            MonthlyProcessRec mp = dataObject.GetProcessingForMonth(currMonth);

            //set textbox
            txtR1.Text = mp.Task1a ;
            txtR2.Text = mp.Task2a;
            txtR3.Text = mp.Task3a;
            txtR4.Text = mp.Task4a;
            txtR5.Text = mp.Task5a;
            txtR6.Text = mp.Task6a;
            txtR7.Text = mp.Task7a;
            txtR8.Text = mp.Task8a;
            txtR9.Text = mp.Task9a;
            txtR10.Text = mp.Task10a;
            txtR11.Text = mp.Task11a;
            txtR12.Text = mp.Task12a;

            txtS1.Text = ConvertStatus(mp.Task1b);
            txtS2.Text = ConvertStatus(mp.Task2b);
            txtS3.Text = ConvertStatus(mp.Task3b);
            txtS4.Text = ConvertStatus(mp.Task4b);
            txtS5.Text = ConvertStatus(mp.Task5b);
            txtS6.Text = ConvertStatus(mp.Task6b);
            txtS7.Text = ConvertStatus(mp.Task7b);
            txtS8.Text = ConvertStatus(mp.Task8b);
            txtS9.Text = ConvertStatus(mp.Task9b);
            txtS10.Text = ConvertStatus(mp.Task10b);
            txtS11.Text = ConvertStatus(mp.Task11b);
            txtS12.Text = ConvertStatus(mp.Task12b);

            //set up buttons
            
            btnT01.Enabled = (mp.Task1a == "");
            btnT02.Enabled = (mp.Task2a== "" && mp.Task1b == "1");
            btnT03.Enabled = (mp.Task3a == "");
            btnT04.Enabled = (mp.Task4a == "" && mp.Task3b == "1");
            btnT05.Enabled = (mp.Task5a == "" && mp.Task4b == "1");
            btnT06.Enabled = (mp.Task6a == "");

            btnT07.Enabled = (mp.Task9a == "");

            if ((mp.Task8b == "2" || mp.Task8b == "3")|| mp.Task9a != "")
                btnT08.Enabled = false;
            else
                btnT08.Enabled = true;

            btnT09.Enabled  = (mp.Task9a  == "" && mp.Task8b == "1" && mp.Task5b == "1" && mp.Task2b == "1");
            btnT10.Enabled = (mp.Task9b == "1" &&  mp.Task11a == "");
            btnT11.Enabled  = (mp.Task11a  == "" && mp.Task10b == "1");
            btnT12.Enabled  = (mp.Task12a == "" && mp.Task11b == "1");

            btnRefresh.Enabled = (mp.Task12b != "1");

            //focus on close button
            btnClose.Focus();

        }
        
        /// <summary>
        /// Converts stored status codes into human-readable text for display.
        /// </summary>
        /// <param name="s">The raw status code value.</param>
        /// <returns>A descriptive status string.</returns>
        private string ConvertStatus(string s)
        {
            if (s == "1")
                return "COMPLETED";
            else if (s == "2")
                return "ERROR";
            else if (s == "3" || s == "4")
                return "SUBMITTED";
            else
                return "";
        }

        /// <summary>
        /// Determines whether other users are present in the system before running a batch task.
        /// </summary>
        /// <returns>True when other users exist; otherwise false.</returns>
        private bool CheckOtherUsersExist()
        {
            CurrentUsersData cd = new CurrentUsersData();
            if (cd.CheckOtherUsersExist())
            {
                MessageBox.Show("Other users are in the system, Process can not be run!");
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Refreshes the screen by reloading processing data for the active month.
        /// </summary>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadForm();
        }

        /// <summary>
        /// Executes the corresponding batch file for a process number while supplying the statistical period argument.
        /// </summary>
        /// <param name="job">The process identifier to run.</param>
        /// <param name="statisticalPeriod">The yyyyMM period passed into the batch script.</param>
        /// <returns>True when the batch file starts successfully; otherwise false.</returns>
        private bool RunBatchFile(int job, string statisticalPeriod)
        {
            string bat_file = "start_job" + job.ToString("00") + ".bat";
            Process proc = null;
            try
            {
                proc = new Process();

                proc.StartInfo.FileName = GlobalVars.BatchDir + bat_file;
                proc.StartInfo.Arguments = statisticalPeriod;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit();
                proc.Close();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Handles confirmation and execution logic for each monthly processing task.
        /// </summary>
        /// <param name="processno">The process number selected by the user.</param>
        private void RunProcess(int processno)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to run the process?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
              
                // RUN DCP SAMPLE 
                if (processno == 1)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR1.Text = currMonDate;
                        txtS1.Text = "SUBMITTED";
                        btnT01.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                // PRINT DCP C700 FORMS 
                else if (processno == 2)
                {
                    dataObject.RunProcessUpdate(currMonth, processno);
                    txtR2.Text = currMonDate;
                    txtS2.Text = "SUBMITTED";
                    btnT02.Enabled = false;
                    //RunBatchFile(processno);
                    RunBatchFile(processno, currMonth);

                    MessageBox.Show("Processing has been submitted. Do not close any popup windows !");

                }
                // RUN MF PRESAMPLE 
                else if (processno == 3)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR3.Text = currMonDate;
                        txtS3.Text = "SUBMITTED";
                        btnT03.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                // RUN MF SAMPLE AND FORMS
                else if (processno == 4)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR4.Text = currMonDate;
                        txtS4.Text = "SUBMITTED";
                        btnT04.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                // RUN MF WEIGHTING
                else if (processno == 5)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR5.Text = currMonDate;
                        txtS5.Text = "SUBMITTED";
                        btnT05.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");

                    }
                }
                // RUN FEDERAL VIP LOAD
                else if (processno == 6)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR6.Text = currMonDate;
                        txtS6.Text = "SUBMITTED";
                        btnT06.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                // RUN SPECIAL VIP LOAD
                else if (processno == 7)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR7.Text = currMonDate;
                        txtS7.Text = "SUBMITTED";
                        btnT07.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                // RUN PRELIMINARY TABS
                else if (processno == 8)
                {
                    dataObject.RunProcessUpdate(currMonth, processno);
                    txtR8.Text = currMonDate;
                    txtS8.Text = "SUBMITTED";
                    btnT08.Enabled = false;
                    if (btnT09.Enabled)
                        btnT09.Enabled = false;
                    //RunBatchFile(processno);
                    RunBatchFile(processno, currMonth);

                }
                // RUN FINAL TABS
                else if (processno == 9)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR9.Text = currMonDate;
                        txtS9.Text = "SUBMITTED";
                        btnT09.Enabled = false;
                        btnT08.Enabled = false;
                        btnT07.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                // RUN Tsar Process
                else if (processno == 10)
                {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR10.Text = currMonDate;
                        txtS10.Text = "SUBMITTED";
                        btnT10.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                    MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                }
                //PRINT CONTINUING C700 FORMS
                else if (processno == 11)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR11.Text = currMonDate;
                        txtS11.Text = "SUBMITTED";
                        btnT11.Enabled = false;
                        btnT10.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
                //RUN NEXT MONTH PROCESSING
                else if (processno == 12)
                {
                    if (!CheckOtherUsersExist())
                    {
                        dataObject.RunProcessUpdate(currMonth, processno);
                        txtR12.Text = currMonDate;
                        txtS12.Text = "SUBMITTED";
                        btnT12.Enabled = false;
                        //RunBatchFile(processno);
                        RunBatchFile(processno, currMonth);

                        MessageBox.Show("Processing has been submitted. Do not close any popup windows !");
                    }
                }
            }  
        }

        /// <summary>
        /// Triggers task 1 processing.
        /// </summary>
        private void btnT01_Click(object sender, EventArgs e)
        {
            RunProcess(1);  
        }

        /// <summary>
        /// Triggers task 2 processing.
        /// </summary>
        private void btnT02_Click(object sender, EventArgs e)
        {
           RunProcess(2);
        }

        /// <summary>
        /// Triggers task 3 processing.
        /// </summary>
        private void btnT03_Click(object sender, EventArgs e)
        {
            RunProcess(3);
        }

        /// <summary>
        /// Triggers task 4 processing.
        /// </summary>
        private void btnT04_Click(object sender, EventArgs e)
        {
            RunProcess(4);
        }

        /// <summary>
        /// Triggers task 5 processing.
        /// </summary>
        private void btnT05_Click(object sender, EventArgs e)
        {
            RunProcess(5);    
        }

        /// <summary>
        /// Triggers task 6 processing.
        /// </summary>
        private void btnT06_Click(object sender, EventArgs e)
        {
            RunProcess(6);      
        }

        /// <summary>
        /// Triggers task 7 processing.
        /// </summary>
        private void btnT07_Click(object sender, EventArgs e)
        {
            RunProcess(7);
        }

        /// <summary>
        /// Triggers task 8 processing.
        /// </summary>
        private void btnT08_Click(object sender, EventArgs e)
        {
            RunProcess(8);
        }

        /// <summary>
        /// Triggers task 9 processing.
        /// </summary>
        private void btnT9_Click(object sender, EventArgs e)
        {
            RunProcess(9);
        }

        /// <summary>
        /// Triggers task 10 processing.
        /// </summary>
        private void btnT10_Click(object sender, EventArgs e)
        {
            RunProcess(10);
        }

        /// <summary>
        /// Triggers task 11 processing.
        /// </summary>
        private void btnT11_Click(object sender, EventArgs e)
        {
            RunProcess(11);
        }

        /// <summary>
        /// Triggers task 12 processing.
        /// </summary>
        private void btnT12_Click(object sender, EventArgs e)
        {
            RunProcess(12);
        }
    }
}
