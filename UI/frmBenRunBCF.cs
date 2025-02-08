/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmBenRunBCF.cs

 Programmer    : Diane Musachio

 Creation Date : 4/2/2019

 Inputs        : n/a

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This screen will display Benchmark Processing Results

 Detail Design : Detailed User Requirements for VIP Benchmark Processing

 Other         : Called by: Tabulations -> Benchmark -> Processing

 Revisions     : See Below
 *********************************************************************
 Modified Date : 
 Modified By   : 
 Keyword       : 
 Change Request: 
 Description   : 
 *********************************************************************/

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
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Globalization;
using System.Diagnostics;



namespace Cprs
{
    public partial class frmBenRunBCF : frmCprsParent
    {
        VipBenchmarkProcessData data_object = new VipBenchmarkProcessData();
        private string curryr;

        public frmBenRunBCF()
        {
            InitializeComponent();
        }

        private void frmBenRunBCF_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            SetupData();

            GetData();
           
        }

        //if current year does not exist insert a blank row
        //with current year as year
        private void SetupData()
        {
            curryr = DateTime.Now.ToString("yyyy");

            bool latest = data_object.GetStatInfo(curryr);

            if (latest == false)
            {
                data_object.InsertRow(curryr);
            }
        }

        private void GetData()
        {
            dgData.DataSource = data_object.GetBenchmarkPrcssData();

            dgData.Columns[0].HeaderText = "YEAR";
            dgData.Columns[1].HeaderText = "MANUFACTURING";
            dgData.Columns[2].HeaderText = "STATUS";
            dgData.Columns[3].HeaderText = "FEDERAL HIGHWAY";
            dgData.Columns[4].HeaderText = "STATUS";
            dgData.Columns[5].HeaderText = "FARM";
            dgData.Columns[6].HeaderText = "STATUS";
            dgData.Columns[7].HeaderText = "GAS";
            dgData.Columns[8].HeaderText = "STATUS";
            dgData.Columns[9].HeaderText = "OIL";
            dgData.Columns[10].HeaderText = "STATUS";
            dgData.Columns[11].HeaderText = "ELECTRIC";
            dgData.Columns[12].HeaderText = "DATE";
            dgData.Columns[13].HeaderText = "ELEC TRANSMISSION";
            dgData.Columns[14].HeaderText = "STATUS";
            dgData.Columns[15].HeaderText = "IMPROVEMENTS";
            dgData.Columns[16].HeaderText = "STATUS";

            /* button names are named with numbers according to task so if error 
            or submitted the button can be grayed out programmatically */

            foreach (DataGridViewRow r in dgData.Rows)
            {
                //indicates which of 8 buttons to update
                int button = 0;
                
                //i determines column to evaluate
                for (int i = 2; i <= dgData.Columns.Count; i++)
                {
                    button++;

                    if (r.Cells[i].Value.ToString() == "1")
                    {
                        r.Cells[i].Value = "COMPLETED";

                        if (r.Cells[0].Value.ToString() == curryr)
                        {
                            this.Controls["btn" + (button).ToString()].Enabled = true;
                        }
                    }
                    else if (r.Cells[i].Value.ToString() == "2")
                    {
                        r.Cells[i].Value = "ERROR";

                        if (r.Cells[0].Value.ToString() == curryr)
                        {
                            this.Controls["btn" + (button).ToString()].Enabled = false;
                        }
                    }
                    else if (r.Cells[i].Value.ToString() == "3")
                    {
                        r.Cells[i].Value = "SUBMITTED";

                        if (r.Cells[0].Value.ToString() == curryr)
                        {
                            this.Controls["btn" + (button).ToString()].Enabled = false;
                        }
                    }
                    else
                    {
                        this.Controls["btn" + (button).ToString()].Enabled = true;
                    }

                    i++;
                }
            }

            //make column unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.Width = 150;
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgData.ClearSelection();
        }


        
        //Manufacturing button
        private void btn1_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("V20IXBMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task01");
                btn1.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(1);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //Federal Highway button
        private void btn2_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("F12XXBMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task02");
                btn2.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(2);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //Farm button
        private void btn3_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("X0360BMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task03");
                btn3.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(3);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //Gas button
        private void btn4_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("X1123BMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task04");
                btn4.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(4);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //Oil button
        private void btn5_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("X1124BMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task05");
                btn5.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(5);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        // Electric button
        private void btn6_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("X111XBMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task06");
                btn6.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(6);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //Elec Transmission button
        private void btn7_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("X1116BMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task07");
                btn7.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(7);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //Improvements button
        private void btn8_Click(object sender, EventArgs e)
        {
            if (data_object.VerifyDatesEntered("X0013BMI"))
            {
                //submit processing if start and end dates exist
                data_object.UpdateTask("Task08");
                btn8.Enabled = false;
                MessageBox.Show("Processing has been submitted. Do not close any popup windows!");
                RunBatchFile(8);
                GetData();
            }
            else
            {
                MessageBox.Show("Benchmark Start and End Dates have not been Entered");
            }
        }

        //IMPORTANT - for testing do not run interactively from F5 debug... go to file
        //explorer on the computer and run executable (will not run the script otherwise
        //due to environmental setups)
        private void RunBatchFile(int job)
        {
            string bat_file = "start_bjob" + job.ToString("00") + ".bat";

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

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            GetData();
        }

        private void frmBenRunBCF_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
