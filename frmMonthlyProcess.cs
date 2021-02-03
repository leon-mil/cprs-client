/************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmMonthlyProcess.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/28/2016
Inputs:             CprsDAL.MonthlyProcessData tables
Parameters:         None              
Outputs:            None
Description:	    This program runs the Monthly Processing 
                    displays the results.

Detailed Design:    Detailed Design for  Monthly
                    Processing
Other:	            Called from: 
Revision History:	
*********************************************************************
Modified Date   :   02/16/2018
Modified By     :   Kevin J Montgomery
Keyword         :   20180216kjm
Change Request  :   CR 349
Description     :   Remove Run Email reminders column
*********************************************************************
Modified Date   :   11/18/2019
Modified By     :   Christine Zhang
Keyword         :   
Change Request  :   CR 
Description     :   Special vip run column
*********************************************************************/
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
using System.Diagnostics;

namespace Cprs
{
    public partial class frmMonthlyProcess: frmCprsParent
    {
        public frmMonthlyProcess()
        {
            InitializeComponent();
        }
        private string sStatp = DateTime.Now.ToString("yyyyMM");

        private void frmMonthlyProcess_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");
           
            //Get data, set up data grid
            GetMonProcessing();
            if ( GlobalVars.Databasename == "CPRSTEST")
            {
                btnCenturion.Enabled = false;
                btnDaily.Enabled = false;
                btnMonthly.Enabled = false;
            }
        }

        //get monthly processing data from data table
        private void GetMonProcessing()
        {
            string currMonth = DateTime.Now.ToString("yyyyMM");

            MonthlyProcessData dataObject = new MonthlyProcessData();
            if (!dataObject.CheckMonthExists(currMonth))
                dataObject.AddMonthRow(currMonth);

            dgMonProc.DataSource = dataObject.GetMonProcessData();

            dgMonProc.RowHeadersVisible = false;

            dgMonProc.Columns[0].Width = 20;
            dgMonProc.Columns[0].HeaderText = "MONTH";
            dgMonProc.Columns[0].Frozen = true;

            dgMonProc.Columns[1].Width = 25;
            dgMonProc.Columns[1].HeaderText = "DCP\nSAMPLE";
            
            dgMonProc.Columns[2].HeaderText = "STATUS";

            dgMonProc.Columns[3].Width = 25;
            dgMonProc.Columns[3].HeaderText =  "DCP\nC700 FORMS";
            
            dgMonProc.Columns[4].HeaderText = "STATUS";

            dgMonProc.Columns[5].Width = 25;
            dgMonProc.Columns[5].HeaderText = "MF\nPRESAMPLE";

            dgMonProc.Columns[6].HeaderText = "STATUS";

            dgMonProc.Columns[7].Width = 25;
            dgMonProc.Columns[7].HeaderText = "MF\nSAMPLE &\nC700 FORMS";

            dgMonProc.Columns[8].HeaderText = "STATUS";

            dgMonProc.Columns[9].Width = 25;
            dgMonProc.Columns[9].HeaderText = "MF\nWEIGHTING";

            dgMonProc.Columns[10].HeaderText = "STATUS";

            dgMonProc.Columns[11].Width = 25;
            dgMonProc.Columns[11].HeaderText = "FEDERAL\nVIP LOAD";

            dgMonProc.Columns[12].HeaderText = "STATUS";

            dgMonProc.Columns[13].Width = 25;
            dgMonProc.Columns[13].HeaderText = "SPECIAL\nVIP LOAD";

            dgMonProc.Columns[14].HeaderText = "STATUS";

            dgMonProc.Columns[15].Width = 25;
            dgMonProc.Columns[15].HeaderText = "PRELIMINARY\nTABULATIONS";

            dgMonProc.Columns[16].HeaderText = "STATUS";

            dgMonProc.Columns[17].Width = 25;
            dgMonProc.Columns[17].HeaderText = "FINAL\nTABULATIONS";
         
            dgMonProc.Columns[18].HeaderText = "STATUS";

            dgMonProc.Columns[19].Width = 25;
            dgMonProc.Columns[19].HeaderText = "TSAR\nPROCESSING";

            dgMonProc.Columns[20].HeaderText = "STATUS";

            dgMonProc.Columns[21].Width = 25;
            dgMonProc.Columns[21].HeaderText = "CONTINUING\nC700 FORMS";
          
            dgMonProc.Columns[22].HeaderText = "STATUS";

            dgMonProc.Columns[23].Width = 25;
            dgMonProc.Columns[23].HeaderText = "NEXT MONTH\nPROCESSING";

            dgMonProc.Columns[24].HeaderText = "STATUS";
        }

        private void btnMonthly_Click(object sender, EventArgs e)
        {
            frmMonthlyProcessPopup fM = new frmMonthlyProcessPopup();

            fM.ShowDialog();  //show history screen

            GetMonProcessing();
        }

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

        private void btnDaily_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to run the process?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!CheckOtherUsersExist())
                {

                    string bat_file1 = "start_daily.bat";

                    Process proc1 = null;

                    try
                    {
                        proc1 = new Process();
                        proc1.StartInfo.FileName = GlobalVars.BatchDir + bat_file1;

                        proc1.StartInfo.UseShellExecute = false;
                        proc1.StartInfo.CreateNoWindow = true;
                        proc1.Start();
                        proc1.WaitForExit();
                        proc1.Close();

                        MessageBox.Show("Daily Processing has been submitted. Do not close any popup windows. !");
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

        private void dgMonProc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.Value.ToString() == "1")
            {
                e.Value = "COMPLETED";
            }
            else if (e.Value.ToString() == "2")
            {
                e.Value = "ERROR";
            }
            else if (e.Value.ToString() == "3" || e.Value.ToString() == "4")
            {
                e.Value = "SUBMITTED";
            }
        }

        private void frmMonthlyProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnCenturion_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to run the process?", "Question", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                if (!CheckOtherUsersExist())
                {
                    string fileName = string.Empty;

                    // Show the dialog and get result.

                    openFileDialog1.Filter = "Data file|*.dat";
                    openFileDialog1.Title = "Select an File";
                    openFileDialog1.InitialDirectory = GlobalVars.CenturionDir;
                    openFileDialog1.FileName = "";
                    DialogResult result = openFileDialog1.ShowDialog();

                    if (result != DialogResult.OK)
                        return;

                    fileName = openFileDialog1.FileName;

                    string bat_file2 = "start_centurion.bat";

                    Process proc2 = null;
                    try
                    {
                        proc2 = new Process();
                        proc2.StartInfo.FileName = GlobalVars.BatchDir + bat_file2;
                        proc2.StartInfo.Arguments = fileName;
                        proc2.StartInfo.UseShellExecute = false;
                        proc2.StartInfo.CreateNoWindow = true;
                        proc2.Start();
                        proc2.WaitForExit();
                        proc2.Close();

                        MessageBox.Show("Centurion Processing has been submitted. Do not close any popup windows !");
                    }
                    catch
                    {
                        return;
                    }
                }
            }
        }

    }
}
