
/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : frmSpecialPriorityTC.cs
 Programmer    : Diane Musachio
 Creation Date : 10/19/2017
 Inputs        : N/A
 Parameters    : N/A
 Output        : N/A
 Description   : This program will display screen to review and
                 enter Special Priority TC
 Detail Design : Detailed Design for Special Priority TC
 Other         : Called by: Home -> Setup -> Call Scheduler ->
                          Special Priority TC
 Revisions     : See Below
 *********************************************************************
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
using CprsBLL;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Threading;

namespace Cprs
{
    public partial class frmSpecialPriorityTC : frmCprsParent
    {
        private DataTable dt;
        private frmMessageWait waiting;

        SpecialTCData data_object = new SpecialTCData();

        public frmSpecialPriorityTC()
        {
            InitializeComponent();
        }

        private void frmSpecialPriorityTC_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            //initialize all buttons as disabled
            btnAdd.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnApply.Enabled = false;
            UnavailableDaysData undata = new UnavailableDaysData();
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int today = DateTime.Now.Day;
            
            int cut_day = Convert.ToInt32(undata.GetUnavailableCutDay(year.ToString(), month.ToString("d2")));

            // If the user is HQManger or Programmer then button is available - otherwise it's disabled
            if (UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.Programmer)
            {
                btnAdd.Enabled = true;
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnApply.Enabled = true;
            }

            //if more than one current user 
            //or current day is greater than cut day in Sched_day table 
            //then gray out apply button
            if ((GeneralDataFuctions.CheckCurrentUsers() > 1) ||
                (today > cut_day))
            {
                btnApply.Enabled = false;
            }

            GetData();
        }

        //get data and format for display
        private void GetData()
        {
            dt = data_object.GetSpecialTcData();
            dgData.DataSource = dt;

            dgData.Columns[0].HeaderText = "SURVEY";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgData.Columns[1].HeaderText = "NEWTC";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgData.Columns[2].HeaderText = "MIN";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[2].DefaultCellStyle.Format = "N0";

            dgData.Columns[3].HeaderText = "MAX";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].DefaultCellStyle.Format = "N0";

            dgData.Columns[4].HeaderText = "MIN";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].DefaultCellStyle.Format = "N0";

            dgData.Columns[5].HeaderText = "MAX";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].DefaultCellStyle.Format = "N0";

            dgData.Columns[6].HeaderText = "MIN";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].DefaultCellStyle.Format = "N0";

            dgData.Columns[7].HeaderText = "MAX";
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[7].DefaultCellStyle.Format = "N0";

            //make columns unsortable
            foreach (DataGridViewColumn dgvc in dgData.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            //gray out buttons if no records after refreshing data
            if (dgData.RowCount == 0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        //get and set values of priority
        private void recalculatePriority()
        {
            DataTable cp = new DataTable();

            cp = data_object.GetSchedCall();

            foreach (DataRow row in cp.Rows)
            {
                string newpriority = "";
                string.Format("{0:D2}", newpriority);

                string id = row["id"].ToString();
                string coltec = row["coltec"].ToString();
                string priority = row["priority"].ToString();
                string.Format("{0:D2}", priority);
                string spec_owner = row["spec_owner"].ToString();
                string xowner = row["xowner"].ToString();
                string status = row["status"].ToString();
                int valmin = Convert.ToInt32(row["valmin"].ToString());
                int valmax = Convert.ToInt32(row["valmax"].ToString());
                int lvalmin = 0;
                int lvalmax = valmin - 1;
                int hvalmin = valmax + 1;
                int hvalmax = 99999999;
                int rvitm5c = Convert.ToInt32(row["rvitm5c"].ToString());
                int num_imp = Convert.ToInt32(row["num_imp"].ToString());
                int lag = Convert.ToInt32(row["lag"].ToString());

                decimal fwgt = Convert.ToDecimal(row["fwgt"].ToString());
                decimal wtR5c = (rvitm5c * fwgt);

                int mb = num_imp - lag;

                if ((priority == "05") || (priority == "06") ||
                    (priority == "07") || (priority == "12") ||
                    (priority == "15"))
                {
                    newpriority = priority;
                }
                //coltecs I A S or status 7
                else if (((coltec == "I") || (coltec == "A") ||
                    (coltec == "S")) || (status == "7"))
                {
                    newpriority = "23";
                }
                else
                {
                     //special tc cases
                    if (spec_owner == xowner)
                    {
                        if ((wtR5c >= lvalmin) && (wtR5c <= lvalmax))
                        {
                            if (coltec == "P")
                            {
                                newpriority = "03";
                            }
                            else if ((coltec == "F" || coltec == "C"))
                            {
                                newpriority = "18";
                            }
                        }
                        else if ((wtR5c >= valmin) && (wtR5c <= valmax))
                        {
                            if (coltec == "P")
                            {
                                newpriority = "02";
                            }
                            else if ((coltec == "F" || coltec == "C"))
                            {
                                newpriority = "17";
                            }
                        }
                        else if ((wtR5c >= hvalmin) && (wtR5c <= hvalmax))
                        {
                            if (coltec == "P")
                            {
                                newpriority = "01";
                            }
                            else if ((coltec == "F" || coltec == "C"))
                            {
                                newpriority = "16";
                            }
                        }
                        else
                        {
                            newpriority = "XX";
                        }
                    }                
                   // regular priority
                   else 
                    {
                        if (coltec == "P")
                        {
                            if (mb <= 0)
                            {
                                newpriority = "11";
                            }
                            if (mb == 1)
                            {
                                newpriority = "10";
                            }
                            if (mb == 2)
                            {
                                newpriority = "09";
                            }
                            if (mb >= 3)
                            {
                                newpriority = "08";
                            }
                        }
                        else if (coltec == "F")
                        {
                            if (mb <= 0)
                            {
                                newpriority = "22";
                            }
                            if (mb == 1)
                            {
                                newpriority = "14";
                            }
                            if (mb == 2)
                            {
                                newpriority = "13";
                            }
                            if (mb >= 3)
                            {
                                newpriority = "04";
                            }
                        }
                        else if (coltec == "C")
                        {
                            if (mb <= 0)
                            {
                                newpriority = "21";
                            }
                            if (mb == 1)
                            {
                                newpriority = "20";
                            }
                            if (mb == 2)
                            {
                                newpriority = "19";
                            }
                            if (mb >= 3)
                            {
                                newpriority = "04";
                            }
                        }
                    }                
                }

                //apply new priority to sched_call
                if (newpriority != priority)
                {
                    data_object.UpdatePriority(id, newpriority);
                }
            }
        }

        //Clicking on add button displays frmSpecialPriorityTCEdit popu-up form with no pre-populated values
        private void btnAdd_Click(object sender, EventArgs e)
        {          
            frmSpecTCEditPopup popup = new frmSpecTCEditPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.owner = "";
            popup.tc = "";
            popup.ShowDialog();

            if (popup.DialogResult == DialogResult.OK)
            {
                //refresh data
                GetData();

                btnDelete.Enabled = true;
                btnEdit.Enabled = true;
            }
        }

        //Clicking on edit button displays frmSpecialPriorityTCEdit popu-up form with pre-populated values
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmSpecTCEditPopup popup = new frmSpecTCEditPopup();
            popup.StartPosition = FormStartPosition.CenterParent;

            //select row to be edited and get values to pass to popup
            DataGridViewRow row = this.dgData.SelectedRows[0];

            string owner = row.Cells[0].Value.ToString();
            string tc = row.Cells[1].Value.ToString();
            int valmin = Convert.ToInt32(row.Cells[4].Value);
            int valmax = Convert.ToInt32(row.Cells[5].Value);

            popup.owner = owner;
            popup.tc = tc;
            popup.minval = valmin;
            popup.maxval = valmax;

            popup.ShowDialog();

            if (popup.DialogResult == DialogResult.OK)
            {
                //refresh data
                GetData();
            }

        }

        //Clicking on delete button prompts user to verify if they want to delete selected row
        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected Survey/Newtc?", "Question", MessageBoxButtons.YesNo);

            if (dialogResult == DialogResult.Yes)
            {
                string owner = dgData.CurrentRow.Cells[0].Value.ToString();
                string newtc = dgData.CurrentRow.Cells[1].Value.ToString();
                int valmin = Convert.ToInt32(dgData.CurrentRow.Cells[4].Value);
                int valmax = Convert.ToInt32(dgData.CurrentRow.Cells[5].Value);
                string user = UserInfo.UserName;
                DateTime dt = DateTime.Now;

                //delete row
                data_object.DeleteSpecialTCData(owner, newtc);

                //pass information to audit trail
                data_object.UpdateSpecialTCAudit(owner, newtc, "DELETE", valmin, valmax, 0, 0, user, dt);

                GetData();
            }
        }

        //re-assesses priority status based on number of imputes and/or lag computation
        private void btnApply_Click(object sender, EventArgs e)
        {
            //set up backgroundwork to save table
            backgroundWorker1.RunWorkerAsync();
        }

        //run application
        public void Splashstart()
        {
            waiting = new frmMessageWait();
            Application.Run(waiting);
        }

        //displays popup that shows the TC Audit Trail information
        private void btnAudit_Click(object sender, EventArgs e)
        {
            frmSpecTCAuditPopup popup = new frmSpecTCAuditPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
        }

        //updates Cprs Access Data
        private void frmSpecialPriorityTC_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            /*start a new thread */
            Thread t = new Thread(new ThreadStart(Splashstart));
            t.Start();

            recalculatePriority();

            /*close message wait form, abort thread */
            waiting.ExternalClose();
            t.Abort();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            
            MessageBox.Show("Updates have been applied to Call Scheduler");
            this.Enabled = true;
        }

        private void btnAdd_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnEdit_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnDelete_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnApply_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }

        private void btnAudit_EnabledChanged(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.LightGray : Color.DarkBlue;
            btn.BackColor = btn.Enabled == false ? Color.LightGray : Color.White;
        }
    }
}
