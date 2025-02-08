/************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmImpAnnualProc.cs	    	
Programmer:         Christine Zhang
Creation Date:      05/20/2019
Inputs:             None
Parameters:         None              
Outputs:            None
Description:	    This program runs the Improvement Annual Processing 
                    displays the results.

Detailed Design:    Detailed Design for Improvement Anuualy Processing
Other:	           
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
    public partial class frmImpAnnualProc : frmCprsParent
    {
        public frmImpAnnualProc()
        {
            InitializeComponent();
        }

        private ImpAnnualProcessData data_object;

        private void frmImpAnnualProc_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("IMPROVEMENTS");

            data_object = new ImpAnnualProcessData();

            //check current year data, if doesn't exist, insert a blank row
            data_object.CheckYearProcessInfo(DateTime.Now.ToString("yyyy"));

            //display the table

            GetAnnualProcessingData();

        }

        //Get anuual process data
        private void GetAnnualProcessingData()
        {
            DataTable dt = data_object.GetAnnualPrcssData();
            dgData.DataSource = dt;

            dgData.Columns[0].HeaderText = "YEAR";
            dgData.Columns[1].HeaderText = "CE TRENDER";
            dgData.Columns[2].HeaderText = "STATUS";
            dgData.Columns[3].HeaderText = "CE VARIANCES";
            dgData.Columns[4].HeaderText = "STATUS";

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgData.ClearSelection();

            //set up buttons
            string searchExpression = "YEAR = " + DateTime.Now.Year;
            DataRow[] rows = dt.Select(searchExpression);
            DataRow dr = rows[0];

            btnTrender.Enabled = (dr["TASK01B"].ToString().TrimEnd() == "");
            btnVar.Enabled = (dr["TASK02B"].ToString().TrimEnd() == "");  

        }

        private void frmImpAnnualProc_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("IMPROVEMENTS", "EXIT");
        }

        private void dgData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetAnnualProcessingData();
        }

       
        //IMPORTANT - for testing do not run interactively from F5 debug... go to file
        //explorer on the computer and run executable (will not run the script otherwise
        //due to environmental setups)
        private void RunBatchFile(int job)
        {
            string bat_file = string.Empty;
            if (job == 1)
                bat_file = "start_cetrender.bat";
            else 
                bat_file = "start_cevariance.bat";
           
            Process proc = null;
            try
            {
                proc = new Process();

                proc.StartInfo.FileName = GlobalVars.BatchDir + bat_file;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.CreateNoWindow = true;

                proc.Start();

                proc.WaitForExit();
                Console.WriteLine("ExitCode: {0}", proc.ExitCode);
                proc.Close();
            }
            catch
            {
                throw new ApplicationException("No process was specified to run");
            }
        }

        private void btnTrender_Click(object sender, EventArgs e)
        {
            data_object.UpdateTask("Task01");
            btnTrender.Enabled = false;

            MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
            RunBatchFile(1);
            GetAnnualProcessingData();
        }

        private void btnVar_Click(object sender, EventArgs e)
        {
            data_object.UpdateTask("Task02");
            btnVar.Enabled = false;

            MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
            RunBatchFile(2);
            GetAnnualProcessingData();
        }

        private void btnTrender_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnVar_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
