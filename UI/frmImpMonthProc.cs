
/************************************************************************
Econ App Name:      CPRS

Project Name:       CPRS Interactive Screens System

Program Name:       frmImpMonthProc.cs	    	

Programmer:         Cestine Gill

Creation Date:      07/30/2015

Inputs:             CprsDAL.ImpMonProcessData tables

Parameters:         None
                 
Outputs:            None

Description:	    This program runs the Monthly Processing 
                    Improvement processes and displays the results.

Detailed Design:    Detailed Design for Improvements Monthly
                    Processing

Other:	            Called from: 
 
Revision History:	
*********************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :
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
    public partial class frmImpMonthProc : Cprs.frmCprsParent
    {
        //set and format the current stat period

        public string sStatp = DateTime.Now.ToString("yyyyMM");

        private ImpMonProcessData dataObject;

        public frmImpMonthProc()
        {
            InitializeComponent();
        }

        private void frmImpMonthProc_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            dataObject = new ImpMonProcessData();

            //display the table

            GetMonProcessing();

            //initially disable buttons
       
            btnRunForecasting.Enabled = false;
            btnRunTabulations.Enabled = false;
            btnRunVIPLoad.Enabled = false;

            //enable the buttons based on the values in the CePrcss table

            enableButton();

        }

        //Check to see if a row exists for the current month
        //If a row does not exist for the current month, Insert a 
        //row into table CePress and set the Statp to the current
        //month in the format YYYYMM. Set all other fields to blank

        protected void SearchAndUpdate()
        {
            DataTable dt1 = new DataTable();

            dt1 = dataObject.CheckStatp(sStatp);

            if (dt1.Rows.Count == 0)
            {
                dataObject.InsertRow(sStatp);
            }

        }


        //Populates and formats the Monthly Processing table 

        private void GetMonProcessing()
        {
            try
            {
                dgMonProc.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

               
                
                // set it to false if not needed

                dgMonProc.RowHeadersVisible = false;
                dgMonProc.ScrollBars = ScrollBars.Both;

                SearchAndUpdate();

                DataTable dtMonProc = new DataTable();

                //Assign the dataObject coming from the DAL

                dtMonProc = dataObject.GetMonProcessData();

                dgMonProc.DataSource = dtMonProc;

                /*Assign the column names and Width */
                for (int i = 0; i < dgMonProc.ColumnCount; i++)
                {
                    dgMonProc.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }

                dgMonProc.Columns[0].HeaderText = "MONTH";
                dgMonProc.Columns[0].Width = 90;                
                dgMonProc.Columns[1].HeaderText = "CE LOAD DATE";
                dgMonProc.Columns[1].Width = 112;
                dgMonProc.Columns[2].HeaderText = "CE LOAD STATUS";
                dgMonProc.Columns[2].Width = 121;
                dgMonProc.Columns[3].HeaderText = "TABULATION DATE";
                dgMonProc.Columns[3].Width = 135;
                dgMonProc.Columns[4].HeaderText = "TABULATION STATUS";
                dgMonProc.Columns[4].Width = 145;
                dgMonProc.Columns[5].HeaderText = "FORECASTING DATE";
                dgMonProc.Columns[5].Width = 140;
                dgMonProc.Columns[6].HeaderText = "FORECASTING STATUS";
                dgMonProc.Columns[6].Width = 155;
                dgMonProc.Columns[7].HeaderText = "VIP LOAD DATE";
                dgMonProc.Columns[7].Width = 121;
                dgMonProc.Columns[8].HeaderText = "VIP LOAD STATUS";
                dgMonProc.Columns[8].Width = 130;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Table is blank", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Convert the Status Information 

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
            else if (e.Value.ToString() == "3")
            {
                e.Value = "SUBMITTED";
            }
        }

        private void enableButton()
        {

            //Get the value of the Status

            string strCELoadStatus;
            string strTabulationStatus;            
            string strForecastingStatus;
            string strVIPStatus;
       
            strCELoadStatus = dgMonProc.Rows[0].Cells[2].Value.ToString();
            strTabulationStatus = dgMonProc.Rows[0].Cells[4].Value.ToString();
            strForecastingStatus = dgMonProc.Rows[0].Cells[6].Value.ToString();
            strVIPStatus = dgMonProc.Rows[0].Cells[8].Value.ToString();

            // Hide the Reset button based on group and whether the status in Error

            if ((strCELoadStatus == "2" || strTabulationStatus == "2" ||
                strForecastingStatus == "2" || strVIPStatus == "2") && (UserInfo.GroupCode == 0))
            
                btnReset.Visible = true;

            else
                btnReset.Visible = false;
            

            //Enable the Run Tabulations button if the CE Load status is 
            //Completed or if Submitted

            //Disable the CE Load button once the process has been submitted, 
            //completed, or in error

            if (strCELoadStatus == "1")
            {
                btnRunTabulations.Enabled = true;
                btnRunCELoad.Enabled = false;
            }

            else if (strCELoadStatus == "3" || strCELoadStatus == "2")
            {
                btnRunCELoad.Enabled = false;
            }
            else
            //added because the RUN CE Load button disabled after the reset button clicked
                btnRunCELoad.Enabled = true;

           //Enable the Run Forecasting button if the tabulations status is 
           //Completed or if Submitted

            //Disable the Run Tabulations button once the process has been 
            //submitted, completed, or in error

            if (strTabulationStatus == "1")
            {
                btnRunForecasting.Enabled = true;
                btnRunTabulations.Enabled = false;
            }
            else if (strTabulationStatus == "3" || strTabulationStatus == "2")
            {
                btnRunTabulations.Enabled = false;
            }

            //Enable the Run VIP Load button if the forecasting status is 
            //Completed or if Submitted

            //Disable the Run VIP Load button once the process has been 
            //submitted, completed, or in error

            if (strForecastingStatus == "1")
            {
                btnRunVIPLoad.Enabled = true;
                btnRunForecasting.Enabled = false;
            }
            else if (strForecastingStatus == "3" || strForecastingStatus == "2")
            {
                btnRunForecasting.Enabled = false;
            }

            //Disable the Run VIP Load button if the VIP status is 
            //Completed or if Submitted

            if (strVIPStatus == "1" || strVIPStatus == "3" || strVIPStatus == "2")
            {
                btnRunVIPLoad.Enabled = false;
             
            }
        }

        private void RunBatchFile(int job)
        {

            string bat_file = "start_ijob" + job.ToString("00") + ".bat";

            Process proc = null;
            try
            {
                proc = new Process();

                proc.StartInfo.FileName = GlobalVars.BatchDir + bat_file;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();
                proc.WaitForExit();
                proc.Close();
            }
            catch
            {
                throw new ApplicationException("No process was specified to run");
            }
        }

        private void btnRunCELoad_Click(object sender, EventArgs e)
        {
            //Display the verification popup to ensure the user 
            //wants to run the process

            frmVerificationPopup popup = new frmVerificationPopup();
            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.Yes)
            {
                DataTable dtRunCELoad = new DataTable();

                //Assign the dataObject coming from the DAL

                dtRunCELoad = dataObject.RunCELoadUpdate(sStatp);
                dgMonProc.DataSource = dtRunCELoad;
                
                //refresh the table after button click

                GetMonProcessing();

                RunBatchFile(1);

                //refresh the button after button click

                enableButton();
            }

            popup.Dispose();
        }

        private void btnRunTabulations_Click(object sender, EventArgs e)
        {
            //Display the verification popup to ensure the user 
            //wants to run the process

            frmVerifyTabPopup popup = new frmVerifyTabPopup();
            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.Yes)
            {
                DataTable dtRunTabulations = new DataTable();

                //Assign the dataObject coming from the DAL
               
                dtRunTabulations = dataObject.RunTabulationsUpdate(sStatp);
                dgMonProc.DataSource = dtRunTabulations;

                //refresh the table after button click

                GetMonProcessing();

                RunBatchFile(2);

                //refresh the button after button click

                enableButton();
            }
            else if (dialogresult == DialogResult.No)
            {
                MessageBox.Show("Process Cancelled");
            }
            popup.Dispose();
        }

        private void btnRunForecasting_Click(object sender, EventArgs e)
        {
            frmVerificationPopup popup = new frmVerificationPopup();
            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.Yes)
            {
                DataTable dtRunForecasting = new DataTable();

                //Assign the dataObject coming from the DAL

             
                dtRunForecasting = dataObject.RunForecastingUpdate(sStatp);
                dgMonProc.DataSource = dtRunForecasting;
                
                GetMonProcessing();

                RunBatchFile(3);

                enableButton();
            }
            else if (dialogresult == DialogResult.No)
            {
                MessageBox.Show("Process Cancelled");
            }
            popup.Dispose();
        }

        private void btnRunVIPLoad_Click(object sender, EventArgs e)
        {
            frmVerificationPopup popup = new frmVerificationPopup();
            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.Yes)
            {
                DataTable dtRunVIPLoad = new DataTable();

                //Assign the dataObject coming from the DAL
                
                dtRunVIPLoad = dataObject.RunVIPLoadUpdate(sStatp);
                dgMonProc.DataSource = dtRunVIPLoad;
                
                //Reload table

                GetMonProcessing();

                RunBatchFile(4);

                enableButton();
            }
            else if (dialogresult == DialogResult.No)
            {
                MessageBox.Show("Process Cancelled");
            }
            popup.Dispose();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            //refresh the table after button click

            GetMonProcessing();

            //refresh the button after button click

            enableButton();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {

            dataObject.Reset(sStatp);

            //Reload table

            GetMonProcessing();
            enableButton();
        }

        private void frmImpMonthProc_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

        private void btnRunCELoad_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnRunCELoad.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnRunCELoad.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnRunTabulations_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnRunTabulations.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnRunTabulations.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnRunForecasting_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnRunForecasting.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnRunForecasting.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnRunVIPLoad_EnabledChanged(object sender, EventArgs e)
        {
            Button currentButton = (Button)sender;
            btnRunVIPLoad.ForeColor = currentButton.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btnRunVIPLoad.BackColor = currentButton.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
