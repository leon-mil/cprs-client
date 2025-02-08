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
*********************************************************************
Modified Date   :   10/7/2024
Modified By     :   Christine Zhang
Keyword         :   
Change Request  :    
Description     :   get rid of two buttons for 
                    run daily process and centurion process
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

        private void btnMonthly_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnMonthly.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnMonthly.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

     
    }
}
