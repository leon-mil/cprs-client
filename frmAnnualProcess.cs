/************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmAnnualProcess.cs	    	
Programmer:         Christine Zhang
Creation Date:      05/20/2019
Inputs:             
Parameters:         None              
Outputs:            None
Description:	    This program runs the Annual Processing 
                    displays the results.

Detailed Design:    Detailed Design for Anuualy Processing
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
    public partial class frmAnnualProcess : frmCprsParent
    {
        public frmAnnualProcess()
        {
            InitializeComponent();
        }

        AnnualProcessData data_object;
        DataTable dt = new DataTable();
        private void frmAnnualProcess_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            data_object = new AnnualProcessData();

            //check current year data, if doesn't exist, insert a blank row
            data_object.CheckCurrentYearProcessInfo(DateTime.Now.ToString("yyyy"));

            //Get data, set up data grid
            GetAnnualProcessingData();
        }

       //Get anuual process data
        private void GetAnnualProcessingData()
        {
            dt = data_object.GetAnnualPrcssData();
            dgData.DataSource = dt;

            dgData.Columns[0].HeaderText = "YEAR";
            dgData.Columns[1].HeaderText = "ANNUAL VARIANCE";
            dgData.Columns[2].HeaderText = "STATUS";
            dgData.Columns[3].HeaderText = "ANNUAL LSF";
            dgData.Columns[4].HeaderText = "STATUS";
            dgData.Columns[5].HeaderText = "ANNUAL FACTORS";
            dgData.Columns[6].HeaderText = "STATUS";
            dgData.Columns[7].HeaderText = "VIP RATIOS";
            dgData.Columns[8].HeaderText = "STATUS";
            dgData.Columns[9].HeaderText = "BEA INDEX";
            dgData.Columns[10].HeaderText = "STATUS";
            dgData.Columns[11].HeaderText = "VIPHIST UPDATE";
            dgData.Columns[12].HeaderText = "STATUS";
            dgData.Columns[13].HeaderText = "FEDERAL BOOST";
            dgData.Columns[14].HeaderText = "STATUS";
            dgData.Columns[15].HeaderText = "1 UNIT LOAD";
            dgData.Columns[16].HeaderText = "STATUS";
            dgData.Columns[17].HeaderText = "TSAR PROCESSING";
            dgData.Columns[18].HeaderText = "STATUS";
            dgData.Columns[19].HeaderText = "SEASONAL UPDATE";
            dgData.Columns[20].HeaderText = "STATUS";

           
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

           
            btnP1.Enabled = (dr["TASK01B"].ToString().TrimEnd() == "");
            btnP2.Enabled = (dr["TASK02B"].ToString().TrimEnd() == "");
            btnP3.Enabled = (dr["TASK03B"].ToString().TrimEnd() == "");
            btnP4.Enabled = (dr["TASK04B"].ToString().TrimEnd() == "");
            btnP5.Enabled = (dr["TASK05B"].ToString().TrimEnd() == "");
            btnP6.Enabled = (dr["TASK06B"].ToString().TrimEnd() == "");
            btnP7.Enabled = (dr["TASK07B"].ToString().TrimEnd() == "");
            btnP8.Enabled = (dr["TASK08B"].ToString().TrimEnd() == "");
            btnP9.Enabled = (dr["TASK09B"].ToString().TrimEnd() == "");
            btnP10.Enabled = (dr["TASK10B"].ToString().TrimEnd() == "");

        }


        private void frmAnnualProcess_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
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
            else if (e.Value.ToString() == "3" )
            {
                e.Value = "SUBMITTED";
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetAnnualProcessingData();
        }

        private void ProcessTasks(int no)
        {
            //submit processing 
            if (no == 1)
            {
                data_object.UpdateTask("Task01");
                btnP1.Enabled = false;
            }
            else if (no == 2)
            {
                data_object.UpdateTask("Task02");
                btnP2.Enabled = false;
            }
            else if (no == 3)
            {
                data_object.UpdateTask("Task03");
                btnP3.Enabled = false;
            }
            else if (no == 4)
            {
                data_object.UpdateTask("Task04");
                btnP4.Enabled = false;
            }
            else if (no == 5)
            {
                data_object.UpdateTask("Task05");
                btnP5.Enabled = false;
            }
            else if (no == 6)
            {
                data_object.UpdateTask("Task06");
                btnP6.Enabled = false;
            }
            else if (no == 7)
            {
                data_object.UpdateTask("Task07");
                btnP7.Enabled = false;
            }
            else if (no == 8)
            {
                data_object.UpdateTask("Task08");
                btnP8.Enabled = false;
            }
            else if (no == 9)
            {
                data_object.UpdateTask("Task09");
                btnP9.Enabled = false;
            }
            else
            {
                data_object.UpdateTask("Task10");
                btnP10.Enabled = false;
            }

            MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
            RunBatchFile(no);
            GetAnnualProcessingData();
        }

        //IMPORTANT - for testing do not run interactively from F5 debug... go to file
        //explorer on the computer and run executable (will not run the script otherwise
        //due to environmental setups)
        private void RunBatchFile(int job)
        {
            string bat_file = string.Empty;
            if (job == 1)
                bat_file = "start_annvar.bat";
            else if (job ==2)
                bat_file = "start_annlsf.bat";
            else if (job == 3)
                bat_file = "start_annfact.bat";
            else if (job == 4)
                bat_file = "start_viprat.bat";
            else if (job == 5)
                bat_file = "start_beaindex.bat";
            else if (job == 6)
                bat_file = "start_histupd.bat";
            else if (job == 7)
                bat_file = "start_fedbst.bat";
            else if (job == 8)
                bat_file = "start_unitload.bat";
            else if (job == 9)
                bat_file = "start_anntsar.bat";
            else 
                bat_file = "start_seasupd.bat";

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

        private void btnP1_Click(object sender, EventArgs e)
        {
            ProcessTasks(1);   
        }

        private void btnP2_Click(object sender, EventArgs e)
        {
            ProcessTasks(2);
        }

        private void btnP3_Click(object sender, EventArgs e)
        {
            ProcessTasks(3);
        }

        private void btnP4_Click(object sender, EventArgs e)
        {
            ProcessTasks(4);
        }

        private void btnP5_Click(object sender, EventArgs e)
        {
            ProcessTasks(5);
        }

        private void btnP6_Click(object sender, EventArgs e)
        {
            ProcessTasks(6);
        }

        private void btnP7_Click(object sender, EventArgs e)
        {
            ProcessTasks(7);
        }

        private void btnP8_Click(object sender, EventArgs e)
        {
            ProcessTasks(8);
        }

        private void btnP9_Click(object sender, EventArgs e)
        {
            ProcessTasks(9);
        }

        private void btnP10_Click(object sender, EventArgs e)
        {
            ProcessTasks(10);
        }
    }
}
