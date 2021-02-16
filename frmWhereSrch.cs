/*********************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmWhereSrch.cs	    	
Programmer:         Christien Zhang
Creation Date:      09/24/2015
Inputs:             
Parameters:		    None 
Outputs:		   
Description:	    This program search construction project data and 
                    display match results.
Detailed Design:    Detailed User Requirements for interactive search Screen 
Other:	             
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
**********************************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using CprsBLL;
using CprsDAL;
using DGVPrinterHelper;

namespace Cprs
{
    public partial class frmWhereSrch : Cprs.frmCprsParent
    {
        private InteractiveSearchData dataObject;
        private string curr_survey_month;
        
        public frmWhereSrch()
        {
            InitializeComponent();
            
        }      

        private void btnStore_Click(object sender, EventArgs e)
        {
            if (txtWhere.Text.Trim() != "")
            {
                frmWhereStore fWS = new frmWhereStore();
                fWS.WhereClause = txtWhere.Text;
                fWS.ShowDialog(); 
                fWS.Dispose();
            }
            else
                MessageBox.Show("Cannot store empty where clause.");
        }

        private void btnRecall_Click(object sender, EventArgs e)
        {
            List<SearchCriteria> clist = dataObject.GetSearchCriteriaData();
            if (clist != null && clist.Count() != 0)
            {
                frmWhereRecall fRC = new frmWhereRecall();

                fRC.ShowDialog();  //show child
                if (fRC.WhereClause != "")
                    txtWhere.Text = fRC.WhereClause;
                fRC.Dispose();
            }
            else
            {
                MessageBox.Show("There are no saved where clauses. ");
            }
        }


        private void frmWhereSrch_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            SetButtonTxt();

            dataObject = new InteractiveSearchData();
            
            dgData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgData.RowHeadersVisible = false; // set it to false if not needed
            dgData.AutoGenerateColumns = true;
           
            dgData.Columns.Clear();
            dgData.DataSource = dataObject.GetEmptySearchTable();

            // Configure the details DataGridView so that its columns automatically
            // adjust their widths when the data changes.
            dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgData.RowHeadersVisible = true;

            txtWhere.Focus();

            curr_survey_month = GeneralFunctions.CurrentYearMon();
        }
        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:   
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQManager:    
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQAnalyst:  
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.NPCManager:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCInterviewer:  
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.HQSupport:  
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQMathStat:  
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQTester:  
                    btnC700.Text = "C-700";
                    break;
            }
        }


        //Get additional columns from where clause
        private string GetAddColumns(string wherecl)
        {
            string addcol = String.Empty;

            string[] w = GeneralFunctions.SplitWords(wherecl);
            List<string> wlist = new List<string>();
            foreach (string s in w)
            {
                wlist.Add(s);
            }

            List<string> collist = new List<string>();
            collist = dataObject.GetColumnNames();
            collist.Remove("ROW");
            collist.Remove("FIN");
            collist.Remove("MASTERID");
            collist.Remove("ID");
            collist.Remove("RESPID");
            collist.Remove("OWNER");
            collist.Remove("NEWTC");
            collist.Remove("SELDATE");
            collist.Remove("STRTDATE");
            collist.Remove("RVITM5C");

            // find join items
            var jlist = (from Item1 in wlist
                         join Item2 in collist
                         on Item1 equals Item2 
                         select new { Item1 }).ToList();

            //get add cols from list
            foreach (var element in jlist)
            {
                if (addcol == "")
                    addcol = element.Item1.ToString();
                else
                    addcol = addcol + ", " + element.Item1.ToString();                
            }

            return addcol;

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string txwhere = txtWhere.Text.Trim();
           
            if (txwhere == "")
            {
                MessageBox.Show("Please enter search citeria. ");
                txtWhere.Focus();
                return;
            }
            else if (txwhere.Contains("1=1"))
            {
                MessageBox.Show("Syntax Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWhere.Focus();
                return;
            }

            /*from where clause get additional columns to display */
            string add_cols = GetAddColumns(txwhere);
            
            string error_message = "";

            DataTable dt = dataObject.GetInteractivesSearchData(txwhere, chkSample.Checked, add_cols, ref error_message);
            if (error_message != "")
            {
                MessageBox.Show("Syntax Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtWhere.Focus();
                return;
            }
            else if (dt.Rows.Count == 0)
            {
                MessageBox.Show("There were no records found.");
                txtWhere.Focus();
            }

            //before load data, turn off rowheadersvisible
            dgData.RowHeadersVisible = false;

            if (dt.Rows.Count == 0)
                dgData.DataSource = dataObject.GetEmptySearchTable();
            else
            {
                dgData.DataSource = dt;
               
                dgData.Columns["Masterid"].Visible = false;
                dgData.Columns["Id"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgData.Columns["Respid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgData.Columns["Owner"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgData.Columns["Newtc"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgData.Columns["Seldate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgData.Columns["Strtdate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgData.Columns["Rvitm5c"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgData.Columns["Rvitm5c"].DefaultCellStyle.Format = "N0";

                if (dgData.Columns["Fipstate"] != null)
                    dgData.Columns["Fipstate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Status"] != null)
                    dgData.Columns["Status"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Coltec"] != null)
                    dgData.Columns["Coltec"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Compdate"] != null)
                    dgData.Columns["Compdate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Repsdate"] != null)
                    dgData.Columns["Repsdate"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Repcompd"] != null)
                    dgData.Columns["Repcompd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Structcd"] != null)
                    dgData.Columns["Structcd"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Psu"] != null)
                    dgData.Columns["Psu"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["Place"] != null)
                    dgData.Columns["Place"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["FLAGR5C"] != null)
                    dgData.Columns["FLAGR5C"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                if (dgData.Columns["FLAGITM6"] != null)
                    dgData.Columns["FLAGITM6"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


            //after load data, turn back rowheadersvisible
            dgData.RowHeadersVisible = true; 
           
            int itemcount = dt.Rows.Count;
            if (itemcount < 2)
                lblCount.Text = itemcount + " Case";
            else
                lblCount.Text = itemcount + " Cases";
        }

        private void btnVariables_Click(object sender, EventArgs e)
        {
            frmVarSelPopup popup = new frmVarSelPopup();

            DialogResult dialogresult = popup.ShowDialog();
            if (dialogresult == DialogResult.OK)
            {
                if (txtWhere.Text.Trim () == "")
                    txtWhere.Text = popup.SelectedVar.Trim() + " ";
                else
                    txtWhere.Text = txtWhere.Text + popup.SelectedVar.Trim() + " ";
            }

            txtWhere.Focus();
            txtWhere.SelectionStart = txtWhere.TextLength + 1;

            popup.Dispose();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtWhere.Text = "";
            chkSample.Checked = false;

            dgData.DataSource = dataObject.GetEmptySearchTable();
            dgData.Columns[0].Width = 60;
            lblCount.Text = "0 Case";
            txtWhere.Focus();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
             if ( dgData.RowCount ==0)
            {
                MessageBox.Show("The Search Results lists is empty. Nothing to print.");
            }
            else
            {
                if (dgData.RowCount >= 115)
                {
                    if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        PrintData();
                    }
                }
                else
                {
                    PrintData();

                }
            }
        }

        private void SetHeaderCellValue()
        {
            int rowNumber = 1;
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                dr.HeaderCell.Value = rowNumber;
                rowNumber = rowNumber + 1;
            }
            dgData.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            SetHeaderCellValue();

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "INTERACTIVE SEARCH";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            if (dgData.SortedColumn ==null)
                printer.SubTitle = txtWhere.Text + "  Sorted By: FIN";
            else
                printer.SubTitle = txtWhere.Text + "  Sorted By: "+ dgData.SortedColumn.Name ;
            printer.SubTitleFormatFlags = StringFormatFlags.LineLimit | StringFormatFlags.NoClip;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            List<string> collist = dataObject.GetColumnNames();

            /*Hide the columns */
            foreach (string col in collist)
            {
                if (col!= "ID" && col!="MASTERID" && col!="FIN" && col!="RESPID" && col!="SELDATE" && col !="NEWTC" && col!="STRTDATE" && col!="OWNER" && col!="RVITM5C")
                    printer.HideColumns.Add(col);               
            }

            /*Set column widths */
            Dictionary<string, float > dictionary = new Dictionary<string, float>();
            
            dictionary.Add("ID", 100);
            dictionary.Add("RESPID", 100);
            dictionary.Add("OWNER", 100);
            dictionary.Add("NEWTC", 60);
            dictionary.Add("SELDATE", 80);   
            dictionary.Add("STRTDATE", 80);
            dictionary.Add("RVITM5C", 100);
            printer.publicwidthoverrides = dictionary;
            
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = true;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Interactive Search Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.PrintDataGridViewWithoutDialog(dgData);

            Cursor.Current = Cursors.Default;

        }

        private void btnName_Click(object sender, EventArgs e)
        {
            if (lblCount.Text == "0 Case")
            {
                MessageBox.Show("The Search Results list is empty. No Record Selected.");
                return;
            }

            //check initial cases
            int index = dgData.CurrentRow.Index;
            string seldate = dgData["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgData["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }
           
            this.Hide();        // hide parent

            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

            string val1 = dgData["ID", index].Value.ToString();

            frmName fName = new frmName();

            // Store Id in list for Page Up and Page Down

            List<string> Idlist = new List<string>();

            int xcnt = 0;

            foreach (DataGridViewRow dr in dgData.Rows)
            {
                string val = dr.Cells["ID"].Value.ToString();
                if (val.Length != 0)
                {
                    if (val.Length != 0)
                    {
                        seldate = dr.Cells["SELDATE"].Value.ToString();
                        if (seldate == curr_survey_month)
                        {
                            string owner = dr.Cells["OWNER"].Value.ToString();
                            if (owner == "M")
                            {
                                Idlist.Add(val);
                                if (val == val1)
                                { fName.CurrIndex = xcnt; }
                                xcnt = xcnt + 1;
                            }
                        }
                        else
                        {
                            Idlist.Add(val);
                            if (val == val1)
                            { fName.CurrIndex = xcnt; }
                            xcnt = xcnt + 1;
                        }
                    }
                }
            }

            fName.Id = val1;
                
            fName.Idlist = Idlist;
            fName.CallingForm = this;

          //  GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

            fName.ShowDialog();    //show child  

         //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
        }

        private void btnSource_Click(object sender, EventArgs e)
        {
            if (lblCount.Text == "0 Case")
            {
                MessageBox.Show("The Search Results list is empty. No Record Selected.");
            }
            else
            {
                this.Hide();        // hide parent

                DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

                int index = dgData.CurrentRow.Index;

                string mid = dgData["MASTERID", index].Value.ToString();

                frmSource fm = new frmSource();

                // Store Masterid in list for Page Up and Page Down

                List<int> Masteridlist = new List<int>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    string val1 = dgData["MASTERID", cnt].Value.ToString();
                    int val = Int32.Parse(val1);
                    Masteridlist.Add(val);
                    cnt = cnt + 1;
                }

                fm.Masterid = Int32.Parse(mid);
                fm.Masteridlist = Masteridlist;
                fm.CurrIndex = index;
                fm.CallingForm = this;

            //    GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fm.ShowDialog();  // show child 

           //     GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            if (lblCount.Text == "0 Case")
            {
                MessageBox.Show("The Search Results list is empty. No Record Selected.");
                return;
            }

            //check initial cases
            int index = dgData.CurrentRow.Index;
            string seldate = dgData["SELDATE", index].Value.ToString();
            if (seldate == curr_survey_month)
            {
                string owner = dgData["OWNER", index].Value.ToString();
                if (owner != "M")
                {
                    MessageBox.Show("Selected case is an initial case and should be accessed through initial case screen");
                    return;
                }
            }

            this.Hide();        // hide parent

            DataGridViewSelectedRowCollection rows = dgData.SelectedRows;

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                string resp = dgData["RESPID", index].Value.ToString();
                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

             //   GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {

                string val1 = dgData["ID", index].Value.ToString();

                frmC700 fC700 = new frmC700();

                // Store Id in list for Page Up and Page Down

                List<string> Idlist = new List<string>();

                int xcnt = 0;

                foreach (DataGridViewRow dr in dgData.Rows)
                {
                    string val = dr.Cells["ID"].Value.ToString();
                    if (val.Length != 0)
                    {
                        if (val.Length != 0)
                        {
                            seldate = dr.Cells["SELDATE"].Value.ToString();
                            if (seldate == curr_survey_month)
                            {
                                string owner = dr.Cells["OWNER"].Value.ToString();
                                if (owner == "M")
                                {
                                    Idlist.Add(val);
                                    if (val == val1)
                                    { fC700.CurrIndex = xcnt; }
                                    xcnt = xcnt + 1;
                                }
                            }
                            else
                            {
                                Idlist.Add(val);
                                if (val == val1)
                                { fC700.CurrIndex = xcnt; }
                                xcnt = xcnt + 1;
                            }
                        }
                    }
                }

                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CallingForm = this;

              //  GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

                fC700.ShowDialog(); // show child
            }

           // GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
        }

        private void dgData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgData.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }

        }

        private void txtWhere_Enter(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxEnter(sender);
        }

        private void txtWhere_Leave(object sender, EventArgs e)
        {
            GeneralFunctions.TextBoxLeave(sender);
        }

        private void frmWhereSrch_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }


    }
}
