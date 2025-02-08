/*************************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmMfInitialReview.cs

 Programmer    : Diane Musachio

 Creation Date : 5/10/2016

 Inputs        : n/a

 Paramaters    : n/a
 
 Output        : n/a
 
 Description   : This program displays and allows user to view Multi-family
                 Initial Review Screen

 Detail Design : Detailed User Requirements for Multi-family Initial Review Screen

 Other         :  Forms Using this code: frmMFInital.cs 
 
                  Called by: 

 Revisions     : See Below
 *********************************************************************
 Modified Date : 4/5/2017
 Modified By   : Christine Zhang
 Keyword       : 20170405cz
 Change Request: CR
 Description   : Add ID to Search Variables. 
                 Remove PSU/PLACE as Search Variable
                 Remove PSU/PLACE/SCHED as Search Variable
 **********************************************************************
 Modified Date : 6/5/2017
 Modified By   : Diane Musachio
 Keyword       : None
 Change Request: Dev CR 201
 Description   : Standardize Popup Message between other screens
 **********************************************************************
 Modified Date : 7/27/2021
 Modified By   : Christine Zhang
 Keyword       : 20210721
 Change Request: CR 8385
 Description   : Add Pending status            
 **********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using System.Net.Mail;
using System.Globalization;
using System.Collections;
using CprsBLL;
using CprsDAL;
using DGVPrinterHelper;
using System.Windows.Forms.VisualStyles;
using System.IO;
using System.Diagnostics;

namespace Cprs
{
    public partial class frmMfInitialReview : Cprs.frmCprsParent
    {
        GetMFInitialReview mf = new GetMFInitialReview();

        DataTable dtMF = new DataTable();

        public frmMfInitialReview()
        {
            InitializeComponent();
            PopulateDG();
        }

        //variables to sum status
        private int notstartedSum;
        private int reviewedSum;
        private int finishedSum;
        private int totalSum;
        private int pendingSum; 

        //printing variables
        private int myLocation;
        private int page;

        //to determine if first search boxes are not empty
        private bool enteredItem = false;
        private bool enteredValue = false;
        private bool empty = false;
       
        //Populates datagrid 
        private void PopulateDG()
        {
            dtMF = mf.GetMFInitRevData();

            dgMFInitRev.DataSource = dtMF;

            DisplayColumns();

            CalculateTotals();

            //Needed worked for status determination - don't need for output window
            dtMF.Columns.Remove("WORKED");

            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            txtValueItem2.Visible = false;
            cbValueItem2.Visible = false;
        }

        private void CalculateTotals()
        {
            //Initialize counts of status
            notstartedSum = 0;
            reviewedSum = 0;
            finishedSum = 0;
            pendingSum = 0;
            totalSum = 0;

            //get counts of status of projects to display on screen
            for (int i = 0; i < dtMF.Rows.Count; i++)
            {
                string status = dtMF.Rows[i]["TWORKED"].ToString();

                //Case not started
                if (status == "NOT STARTED")
                {
                    ++notstartedSum;
                }
                //Case under 1st or 2nd review
                else if (status == "REVIEWED")
                {
                    ++reviewedSum;
                }
                //Case is finished
                else if (status == "FINISHED")
                {
                    ++finishedSum;
                }
                //Case is pending
                else if (status == "PENDING")
                {
                    ++pendingSum;
                }

                //Total number of cases
                ++totalSum;
            }

            //counts of status
            txtNotStart.Text = notstartedSum.ToString();
            txtReview.Text = reviewedSum.ToString();
            txtFinished.Text = finishedSum.ToString();
            txtpending.Text = pendingSum.ToString();
            txtTotal.Text = totalSum.ToString();
        }

       
        //Display columns in specific order
        private void DisplayColumns()
        {
            dgMFInitRev.Columns[0].HeaderText = "ID";
            dgMFInitRev.Columns[0].Width = 95;
            dgMFInitRev.Columns[1].HeaderText = "PSU";
            dgMFInitRev.Columns[1].Width = 70;
            dgMFInitRev.Columns[2].HeaderText = "BPOID";
            dgMFInitRev.Columns[2].Width = 70;
            dgMFInitRev.Columns[3].HeaderText = "SCHED";
            dgMFInitRev.Columns[3].Width = 70;
            dgMFInitRev.Columns[4].HeaderText = "WORK STATUS";
            dgMFInitRev.Columns[4].Width = 100;
            dgMFInitRev.Columns[5].HeaderText = "DUPLICATE";
            dgMFInitRev.Columns[5].Width = 90;
            dgMFInitRev.Columns[6].HeaderText = "1st REVIEW";
            dgMFInitRev.Columns[6].Width = 100;
            dgMFInitRev.Columns[7].HeaderText = "2nd REVIEW";
            dgMFInitRev.Columns[7].Width = 100;
            dgMFInitRev.Columns[8].HeaderText = "PROJECT DESCRIPTION";
            dgMFInitRev.Columns[8].Width = 220;
            dgMFInitRev.Columns[9].HeaderText = "PROJECT LOCATION";
            dgMFInitRev.Columns[9].Width = 220;
        }

        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            enteredItem = true;

            //determine if first search boxes are populated
            //in order to enable second search boxes
            EnablingSecondSearch();

            //Select ID from first combobox
            //make visible a textbox
            if (cbItem.SelectedIndex == 0)
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;
                txtValueItem.Focus();
                txtValueItem.Text = "";
            }

            //Select Work Status from first combobox
            //make visible a second combobox
            if (cbItem.SelectedIndex == 1)
            {
                txtValueItem.Visible = false;
                cbValueItem.Visible = true;
                PopulateValueCombo(cbItem.SelectedIndex);
                cbValueItem.Focus();
            }

            //Select Duplicate from first combobox
            //make visible a second combobox
            if (cbItem.SelectedIndex == 2)
            {
                txtValueItem.Visible = false;
                cbValueItem.Visible = true;
                PopulateValueCombo(cbItem.SelectedIndex);
                cbValueItem.Focus();
            }
            
            //Select 1st Review from first combobox
            //make visible a second combobox
            if (cbItem.SelectedIndex == 3)
            {
                txtValueItem.Visible = false;
                cbValueItem.Visible = true;
                PopulateValueCombo(cbItem.SelectedIndex);
            }

            //Select 2nd Review from first combobox
            //make visible a second combobox
            if (cbItem.SelectedIndex == 4)
            {
                txtValueItem.Visible = false;
                cbValueItem.Visible = true;
                PopulateValueCombo(cbItem.SelectedIndex);
            }

            cbValueItem.Text = "";
            cbItem2.DataSource = null;
            cbValueItem2.DataSource = null;

            cbItem2.SelectedIndex = -1;
            cbValueItem2.SelectedIndex = -1;
            cbValueItem2.Items.Clear();
            txtValueItem2.Text = "";

            cbItem2.Enabled = false;
            cbValueItem2.Enabled = false;
            txtValueItem2.Enabled = false;       
        }

        private void cbItem2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //select id
            if (cbItem2.SelectedIndex == 0)
            {
                txtValueItem2.Visible = true;
                cbValueItem2.Visible = false;
                txtValueItem2.Text = "";
                txtValueItem2.Focus();           
            }

            //Select Work Status from first combobox
            //make visible a second combobox
            if (cbItem2.SelectedIndex == 1)
            {
                txtValueItem2.Visible = false;
                cbValueItem2.Visible = true;
                PopulateValueCombo2(cbItem2.SelectedIndex);
                cbValueItem2.Focus();
            }

            //Select Duplicate from first combobox
            //make visible a second combobox
            if (cbItem2.SelectedIndex ==2)
            {
                txtValueItem2.Visible = false;
                cbValueItem2.Visible = true;
                PopulateValueCombo2(cbItem2.SelectedIndex);
                cbValueItem2.Focus();
            }

            //Select 1st Review from first combobox
            //make visible a second combobox
            if (cbItem2.SelectedIndex == 3)
            {
                txtValueItem2.Visible = false;
                cbValueItem2.Visible = true;
                PopulateValueCombo2(cbItem2.SelectedIndex);
            }

            //Select 2nd Review from first combobox
            //make visible a second combobox
            if (cbItem2.SelectedIndex == 4)
            {
                txtValueItem2.Visible = false;
                cbValueItem2.Visible = true;
                PopulateValueCombo2(cbItem2.SelectedIndex);
            }

            cbValueItem2.Text = "";
        }

        //String to populate search combobox
        private string[] StatusSearch = { "", "NOT STARTED", "REVIEWED", "FINISHED", "PENDING" };
        private string[] StatusSearch2 = { "", "NOT STARTED", "REVIEWED", "FINISHED", "PENDING" };

        //String to populate duplicate combobox
        private string[] DuplicateSearch = { "", "Y", "N" };
        private string[] DuplicateSearch2 = { "", "Y", "N" };

        //function to populate combobox with appropriate search string
        private void showSearchItems(string[] search)
        {
            cbValueItem.DataSource = null;
            cbValueItem.Items.Clear();
            cbValueItem.Items.AddRange(search);
        }

        //function to populate combobox with appropriate search string
        private void showSearchItems2(string[] search2)
        {
            cbValueItem2.DataSource = null;
            cbValueItem2.Items.Clear();
            cbValueItem2.Items.AddRange(search2);
        }

        //Populate 2nd combobox if needed
        private void PopulateValueCombo(int cbIndex)
        {
           
            //work status
            if (cbIndex == 1)
            {
                showSearchItems(StatusSearch);
                cbValueItem.ValueMember = "tworked";
                cbValueItem.DisplayMember = "work status";
            }

            //duplicate
            if (cbIndex == 2)
            {
                showSearchItems(DuplicateSearch);
                cbValueItem.ValueMember = "duplicate";
                cbValueItem.DisplayMember = "duplicate";
            }

            //1st review - populates with distinct rev1nme
            if (cbIndex == 3)
            {
                cbValueItem.DataSource = mf.GetValueList(cbIndex);
                cbValueItem.ValueMember = "rev1nme";
                cbValueItem.DisplayMember = "rev1nme";
            }

            //2nd review - populates with distinct rev2nme
            if (cbIndex == 4)
            {
                cbValueItem.DataSource = mf.GetValueList(cbIndex);
                cbValueItem.ValueMember = "rev2nme";
                cbValueItem.DisplayMember = "rev2nme";
            }

            cbValueItem.SelectedIndex = -1;
        }

        //Validate that psupl and pluplsched are integers upon entry into textbox
        private void txtValueItem_TextChanged(object sender, EventArgs e)
        {
            //create a regular expression to check for a number
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");

            //validate the text
            Control ctr = (Control)sender;

            //verifies the text is numeric
            if (!String.IsNullOrEmpty(ctr.Text))
            {
                if (!regex.IsMatch(ctr.Text))
                {
                    MessageBox.Show("ID must be a number.", "Entry Error");
                    ctr.Text = "";
                    enteredValue = false;
                }
                else 
                {
                   enteredValue = true;                  
                }

                EnablingSecondSearch();
            }
            //disables second search boxes if cleared out text from first selection
            else
            {
                cbItem2.DataSource = null;
                cbValueItem2.DataSource = null;

                cbItem2.SelectedIndex = -1;
                cbValueItem2.SelectedIndex = -1;
                cbValueItem2.Items.Clear();
                txtValueItem2.Text = "";

                cbItem2.Enabled = false;
                cbValueItem2.Enabled = false;
                txtValueItem2.Enabled = false;             
            }
        }

        //Validate there was a selection made in the combobox
        //in order to activate the second search boxes
        private void cbValueItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbValueItem.SelectedIndex >= 0)
            {
                enteredValue = true;

                //don't enable for rev1nme or rev2nme yet
                if ((cbItem.SelectedIndex != 3) && (cbItem.SelectedIndex != 4))
                {
                    EnablingSecondSearch();
                }
                else if (cbValueItem.SelectedIndex > 0)
                    {
                        EnablingSecondSearch();
                    }
                else
                {
                    cbItem2.DataSource = null;
                    cbValueItem2.DataSource = null;

                    cbItem2.SelectedIndex = -1;
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Items.Clear();
                    txtValueItem2.Text = "";

                    cbItem2.Enabled = false;
                    cbValueItem2.Enabled = false;
                    txtValueItem2.Enabled = false;
                }

            }

            enteredValue = false;
        }
        //Validate that psupl and pluplsched are integers upon entry into textbox
        private void txtValueItem2_TextChanged(object sender, EventArgs e)
        {
            //create a regular expression to check for a number
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");

            //validate the text
            Control ctr = (Control)sender;

            //verifies the text is numeric
            if (!String.IsNullOrEmpty(ctr.Text))
            {
                if (!regex.IsMatch(ctr.Text))
                {
                    MessageBox.Show("ID must be a number.", "Entry Error");
                    ctr.Text = "";
                }
            }
        }

        //prevents user from entering anything except digits
        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        //activates or inactivates second search boxes
        //based on if there are selections to first search boxes
        private void EnablingSecondSearch() 
        {
           if ((enteredItem == true) && (enteredValue == true))
           {
               cbItem2.Enabled = true;
               cbValueItem2.Enabled = true;
               txtValueItem2.Enabled = true;
           }
           else
           {
               cbItem2.Enabled = false;
               cbValueItem2.Enabled = false;
               txtValueItem2.Enabled = false;
            }
        }

        //Populate 2nd combobox if needed
        private void PopulateValueCombo2(int cbIndex2)
        {
            //work status
            if (cbIndex2 == 1)
            {
                showSearchItems2(StatusSearch2);
                cbValueItem2.ValueMember = "tworked";
                cbValueItem2.DisplayMember = "work status";
            }

            //duplicate
            if (cbIndex2 == 2)
            {
                showSearchItems2(DuplicateSearch2);
                cbValueItem2.ValueMember = "duplicate";
                cbValueItem2.DisplayMember = "duplicate";
            }

            //1st review - populates with distinct rev1nme
            if (cbIndex2 == 3)
            {
                cbValueItem2.DataSource = mf.GetValueList(cbIndex2);
                cbValueItem2.ValueMember = "rev1nme";
                cbValueItem2.DisplayMember = "rev1nme";
            }

            //2nd review - populates with distinct rev2nme
            if (cbIndex2 == 4)
            {
                cbValueItem2.DataSource = mf.GetValueList(cbIndex2);
                cbValueItem2.ValueMember = "rev2nme";
                cbValueItem2.DisplayMember = "rev2nme";
            }

            cbValueItem2.SelectedIndex = -1;
        }

        private string var;
        private string var2;
        private string txt; //to hold variable for search
        private string txt2; //to hold variable for 2nd dropdown search

        //Search for results based on index entry of 1st combobox
        private void SearchItem()
        {
           
            //validate an entry is chosen from the combobox
            if (cbItem.SelectedIndex == 0)
            {
                empty = false;

                var = "ID";

                string id = txtValueItem.Text.Trim();

                if (!(id.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    empty = true;
                    return;
                }
                else if (!(mf.GetPresampleIds(id)))
                {
                    MessageBox.Show("Invalid ID");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    cbItem2.Enabled = false;
                    enteredValue = false;
                    return;
                }
                else
                {
                    txt = id;
                }
            }

            if (cbItem.SelectedIndex == 1)
            {
                empty = false;

                var = "TWORKED";
             
                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    txt = cbValueItem.Text.Trim();
                }
            }

            //validate an entry is chosen from the combobox
            else if (cbItem.SelectedIndex == 2)
            {
                empty = false;

                var = "DUPLICATE";
             
                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    txt = cbValueItem.Text.Trim();
                }
            }

            //validate an entry is chosen from the combobox
            else if (cbItem.SelectedIndex == 3)
            {
                empty = false;

                var = "REV1NME";

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    txt = cbValueItem.Text.Trim();
                }
            }

            //validate an entry is chosen from the combobox
            else if (cbItem.SelectedIndex == 4)
            {
                empty = false;

                var = "REV2NME";

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    txt = cbValueItem.Text.Trim();
                }
            }

            //necessary to null these for the parameters to DAL
            var2 = null;
            txt2 = null;

            //Validate length of id is 7 
            if (cbItem2.SelectedIndex == 0)
            {
                empty = false;

                var2 = "ID";

                string id2 = txtValueItem2.Text.Trim();

                if (!(id2.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits.");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    empty = true;
                }
                else if (!(mf.GetPresampleIds(id2)))
                {
                    MessageBox.Show("Invalid ID.");
                    txtValueItem2.Focus();
                    txtValueItem2.Clear();
                    enteredValue = false;
                    return;
                }
                else
                {
                    txt2 = id2;
                }
            }

            //validate an entry is chosen from the combobox
            if (cbItem2.SelectedIndex == 1)
            {
                empty = false;

                var2 = "TWORKED";

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.Focus();
                    empty = true;
                }
                else
                {
                    txt2 = cbValueItem2.Text.Trim();
                }
            }

            //validate an entry is chosen from the combobox
            else if (cbItem2.SelectedIndex == 2)
            {
                empty = false;

                var2 = "DUPLICATE";

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.Focus();
                    empty = true;
                }
                else
                {
                    txt2 = cbValueItem2.Text.Trim();
                }
            }

            //validate an entry is chosen from the combobox
            else if (cbItem2.SelectedIndex == 3)
            {
                empty = false;

                var2 = "REV1NME";

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.Focus();
                    empty = true;
                }
                else
                {
                    txt2 = cbValueItem2.Text.Trim();
                }
            }

            //validate an entry is chosen from the combobox
            else if (cbItem2.SelectedIndex == 4)
            {
                empty = false;

                var2 = "REV2NME";

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.Focus();
                    empty = true;
                }
                else
                {
                    txt2 = cbValueItem2.Text.Trim();
                }
            }
            
            if (empty == false)
            {
                dtMF = mf.SearchPresample(var, txt, var2, txt2);

                dgMFInitRev.DataSource = dtMF;

                if (dtMF.Rows.Count == 0)
                {
                    MessageBox.Show("No data to display.");
                }
            } 
        }

        //Initiate search button
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (cbItem.Text.Trim() == "")
            {
                MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                cbValueItem.Focus();
            }
            else
            {
                SearchItem();
            }
        }

        //Clear the search results
        private void btnClear_Click(object sender, EventArgs e)
        {
            enteredItem = false;
            enteredValue = false;

            cbValueItem.DataSource = null;
            cbValueItem2.DataSource = null;
    
            cbItem.SelectedIndex = -1;
            cbValueItem.SelectedIndex = -1;
            cbValueItem.Items.Clear();
            txtValueItem.Text = "";

            cbItem2.SelectedIndex = -1;
            cbValueItem2.SelectedIndex = -1;
            cbValueItem2.Items.Clear();
            txtValueItem2.Text = "";

            cbItem2.Enabled = false;
            cbValueItem2.Enabled = false;
            txtValueItem2.Enabled = false;

            PopulateDG();
        }

        //This will pass parameters to MultiFamily Screen

        //public string Masterid;
        //public List<string> Masteridnumlist = null;
        
        private void btnData_Click(object sender, EventArgs e)
        {
            MfInitialData mfidata = new MfInitialData();
            frmMfInitial fMFInit = new frmMfInitial();

            this.Hide(); // hide parent

            int index = dgMFInitRev.CurrentRow.Index;

            //string PSU = dgMFInitRev["PSU", index].Value.ToString();
            //string PLACE = dgMFInitRev["PLACE", index].Value.ToString();
            //string SCHED = dgMFInitRev["SCHED", index].Value.ToString();
            string id = dgMFInitRev["ID", index].Value.ToString();
            //int masterid = mfidata.GetPresampMasterId(PSU, PLACE, SCHED);

            // Store id in list for Page Up and Page Down
            List<string> idlist = new List<string>();

            int cnt = 0;           
            foreach (DataGridViewRow dr in dgMFInitRev.Rows)
            {
                ////Get the id
                //string psu = dgMFInitRev["PSU", cnt].Value.ToString();
                //string place = dgMFInitRev["PLACE", cnt].Value.ToString();
                //string sched = dgMFInitRev["SCHED", cnt].Value.ToString();

                //int val = mfidata.GetPresampMasterId(psu, place, sched);
                string val = dgMFInitRev["ID", cnt].Value.ToString();

                idlist.Add(val);
                cnt = cnt + 1;
            }
              
            //Get the masterid for the selected row and pass to form
            fMFInit.Id = id;
            fMFInit.Idlist = idlist;
            fMFInit.CurrIndex = index;
            fMFInit.CallingForm = this;
            fMFInit.EntryPoint = "REV";
            
            fMFInit.ShowDialog();  // show child

            SearchItem();
            CalculateTotals();
            HighlightRowForId(id);
            return;
        }

        private void HighlightRowForId(string id)
        {
            int rowIndex = -1;
            foreach (DataGridViewRow row in dgMFInitRev.Rows)
            {
                if (row.Cells[0].Value.ToString().Equals(id))
                {
                    rowIndex = row.Index;
                    break;
                }
            }

            if (rowIndex >= 0)
                dgMFInitRev.Rows[rowIndex].Selected = true;

        }

        //This code populates row header of datagrid dynamically
        private void  dgMFInitRev_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgMFInitRev.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        //This button prints 2 bitmaps and the datagrid information
        private void btnPrint_Click(object sender, EventArgs e)
        {
            printDocument1 = new PrintDocument();

            if (printDocument1.PrinterSettings.IsValid)
            {
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DocumentName = "Multi Family Initial Case Review";
                printDocument1.OriginAtMargins = false;

                CapturePanel();

                CapturePanel2();
 
                myLocation = 0;

                //Notify the user the printout will be over 5 pages - if they want to change their mind
                if (dgMFInitRev.RowCount >= 100)
                {
                    if (MessageBox.Show("The printout will contain more then 5 pages. Continue to Print?", "Printout Large", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                          printDocument1.Print();
                    }
                }
                else
                {
                    printDocument1.Print();
                }            
            }
            else
            {
                MessageBox.Show("Printer is invalid");
            }
        }

        Bitmap memoryImage;
        Bitmap memoryImage2;
        Bitmap resized;
        Bitmap resized2;

        //Capture the panel on screen to print
        private void CapturePanel()
        {
            memoryImage = new Bitmap(panel1.Width, panel1.Height);
            this.panel1.DrawToBitmap(memoryImage, new Rectangle(0, 0, panel1.Width, panel1.Height));
            resized = new Bitmap(memoryImage, new Size(memoryImage.Width * 9 / 10, memoryImage.Height * 9 / 10));
        }

        //Capture the count panel to print 
        private void CapturePanel2()
        {
            memoryImage2 = new Bitmap(panel2.Width, panel2.Height);
            this.panel2.DrawToBitmap(memoryImage2, new Rectangle(0, 0, panel2.Width, panel2.Height));
            resized2 = new Bitmap(memoryImage2, new Size(memoryImage2.Width, memoryImage2.Height));
        }

        //print 2 bitmaps and datagrid
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            FontFamily fontFamily = new FontFamily("Courier New");
            PointF pointF1 = new PointF(0, 50);
            SolidBrush solidBrush = new SolidBrush(Color.Black);
            Graphics graphics = e.Graphics;
            List<string> Linetoprint = new List<string>();
            
            System.Drawing.Font fntDrawString = new System.Drawing.Font(fontFamily, 10, FontStyle.Bold);
            System.Drawing.Font fntDrawString1 = new System.Drawing.Font(fontFamily, 10, FontStyle.Bold);
            float pageHeight = e.MarginBounds.Height;
            float fontHeight = fntDrawString.GetHeight();

            int startX = 25;
            int startY = 5;
            int offsetY = 40;

            try
            {
                Linetoprint.Add("      MULTI FAMILY INITIAL CASE REVIEW");
                Linetoprint.Add(" ");
                Linetoprint.Add("------------------------------------------------------------------------------------------------------------------------");
                Linetoprint.Add(string.Format("{0,-8}{1,-7}{2,-8}{3,-10}{4,-12}{5,-4}{6,-12}{7,-12}{8,-50}\n", "ID","PSU", "BPOID", "SCHED",
                    "WORK STATUS", "DUP", "1ST REVIEW", "2ND REVIEW", "PROJECT DESCRIPTION"));
                Linetoprint.Add("------------------------------------------------------------------------------------------------------------------------");

                foreach (DataGridViewRow row in dgMFInitRev.Rows)
                {
                    string psu = row.Cells["PSU"].Value.ToString();
                    string bpoid = row.Cells["BPOID"].Value.ToString();
                    string sched = row.Cells["SCHED"].Value.ToString();
                    string id = row.Cells["id"].Value.ToString();
                    string tworked = row.Cells["TWORKED"].Value.ToString();
                    string duplicate = row.Cells["DUPLICATE"].Value.ToString();
                    string rev1nme = row.Cells["REV1NME"].Value.ToString();
                    string rev2nme = row.Cells["REV2NME"].Value.ToString();
                    string projdesc = row.Cells["PROJDESC"].Value.ToString();

                    Linetoprint.Add(string.Format("{0,-8}{1,-7}{2,-8}{3,-10}{4,-12}{5,-4}{6,-12}{7,-12}{8,-50}", 
                        id, psu, bpoid, sched, tworked, duplicate, rev1nme, rev2nme, projdesc));
                    Linetoprint.Add(" ");
                }

                e.HasMorePages = false;
                while (myLocation < Linetoprint.Count)
                {
                    if (myLocation == 0)
                    {
                        e.Graphics.DrawImage(resized, 0, 50);
                        e.Graphics.DrawImage(resized2, 400, 200);
                        graphics.DrawString("MULTI FAMILY INITIAL CASE REVIEW", fntDrawString1, solidBrush, 400, 20);
                        startX = 25;
                        startY = 10;
                        offsetY = 440;
                        page = 0;
                        graphics.DrawString(Linetoprint[myLocation], fntDrawString, solidBrush, startX, offsetY);
                        myLocation++;
                    }

                    startX = 25;
                    startY = 10;
                                              
                    graphics.DrawString(Linetoprint[myLocation], fntDrawString, solidBrush, startX, startY + offsetY);
                    offsetY += (int)FontHeight;
                    myLocation++;

                    if (offsetY >= pageHeight)
                    {
                        offsetY = 0;
                        page++;
 
            String strDate = DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToShortTimeString();

            //Draw Date
            e.Graphics.DrawString(strDate, fntDrawString, Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width -
                                    e.Graphics.MeasureString(strDate, fntDrawString, e.MarginBounds.Width).Width), 10);

            //Draw username
            e.Graphics.DrawString(UserInfo.UserName, fntDrawString, Brushes.Black, 40, 10);

                        graphics.DrawString("Page " + page, fntDrawString, solidBrush, 850, 25);                
                        e.HasMorePages = true;
                        return;
                    }
                }     
            }
            catch (Exception exc)
            {
               MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } 
        }
    }
}

            
