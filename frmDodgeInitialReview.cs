/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmDodgeInitialReview.cs

 Programmer    : Diane Musachio

 Creation Date : 02/15/17

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display List and Status of 
                 Dodge Initial Cases

 Detail Design : Detailed User Requirements for Dodge Initial Case Review 

 Other         : Called by: menu -> Case Management -> Dodge Initial Case Review

 Revisions     : See Below
 *********************************************************************
 Modified Date : 06/06/17
 Modified By   : Diane Musachio
 Keyword       : None
 Change Request: CR 201
 Description   : Update Popup Messages to conform with other screens
 *********************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DGVPrinterHelper;
using System.Drawing.Printing;
using System.Globalization;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Collections;
using System.Windows.Forms.VisualStyles;
using System.IO;
using CprsDAL;
using CprsBLL;

namespace Cprs
{
    public partial class frmDodgeInitialReview : frmCprsParent
    {
        private bool enteredItem = false;
        private bool enteredValue = false;
        private bool empty = false;

        DataTable dt = new DataTable();
        DodgeInitialReviewData dodge = new DodgeInitialReviewData();

        public frmDodgeInitialReview()
        {
            InitializeComponent();
        }

        private void frmDodgeInitialReview_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("SEARCH/REVIEW");

            dgData.DataSource = dodge.GetDCPReview();
            dt = dodge.GetDCPReview();

            DisplayColumns();

            CalculateTotals();

            txtValueItem.Visible = false;
            cbValueItem.Visible = false;
            txtValueItem2.Visible = false;
            cbValueItem2.Visible = false;
        }

        //Display columns in specific order
        private void DisplayColumns()
        {
            dgData.Columns[0].HeaderText = "ID";
            dgData.Columns[0].Width = 79;
            dgData.Columns[1].HeaderText = "RESPID";
            dgData.Columns[1].Width = 79;
            dgData.Columns[2].HeaderText = "HQ WORK STATUS";
            dgData.Columns[2].Width = 87;
            dgData.Columns[3].HeaderText = "HQ REVIEW";
            dgData.Columns[3].Width = 83;
            dgData.Columns[4].HeaderText = "NPC WORK STATUS";
            dgData.Columns[4].Width = 87;
            dgData.Columns[5].HeaderText = "NPC 1st REVIEW";
            dgData.Columns[5].Width = 83;
            dgData.Columns[6].HeaderText = "NPC 2nd REVIEW";
            dgData.Columns[6].Width = 83;
            dgData.Columns[7].HeaderText = "NEWTC";
            dgData.Columns[7].Width = 69;
            dgData.Columns[8].HeaderText = "OWNER";
            dgData.Columns[8].Width = 69;
            dgData.Columns[9].HeaderText = "PROJECT DESCRIPTION";
            dgData.Columns[9].Width = 200;
            dgData.Columns[10].HeaderText = "PROJECT LOCATION";
            dgData.Columns[10].Width = 200;
        }

        private void CalculateTotals()
        {
            //Initialize counts of status
            int NPCnotstartedSum = 0;
            int NPCreviewedSum = 0;
            int NPCfinishedSum = 0;
            int HQnotstartedSum = 0;
            int HQreviewedSum = 0;
            int HQfinishedSum = 0;
            int totalSum = 0;

            //get counts of status of projects at NPC to display on screen
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string status = dt.Rows[i]["TNPCWORKED"].ToString();

                //Case not started
                if (status == "NOT STARTED")
                {
                    ++NPCnotstartedSum;
                }
                //Case under 1st or 2nd review
                else if (status == "REVIEWED")
                {
                    ++NPCreviewedSum;
                }
                //Case is finished
                else if (status == "FINISHED")
                {
                    ++NPCfinishedSum;
                }

                //Total number of cases
                ++totalSum;
            }

            //get counts of status of projects at HQ to display on screen
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string status = dt.Rows[i]["THQWORKED"].ToString();

                //Case not started
                if (status == "NOT STARTED")
                {
                    ++HQnotstartedSum;
                }
                //Case under 1st or 2nd review
                else if (status == "REVIEWED")
                {
                    ++HQreviewedSum;
                }
                //Case is finished
                else if (status == "FINISHED")
                {
                    ++HQfinishedSum;
                }
            }

            //counts of status
            txtNPCNotStarted.Text = NPCnotstartedSum.ToString();
            txtNPCReviewed.Text = NPCreviewedSum.ToString();
            txtNPCFinished.Text = NPCfinishedSum.ToString();
            txtHQNotStarted.Text = HQnotstartedSum.ToString();
            txtHQReviewed.Text = HQreviewedSum.ToString();
            txtHQFinished.Text = HQfinishedSum.ToString();
            txtTotal.Text = totalSum.ToString();
        }

        //activates or inactivates second search boxes
        //based on selections of first search boxes
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

        private string var; //hold variable name for search
        private string var2; //hold variable name for 2nd search
        private string txt; //hold value for search
        private string txt2; //hold value for 2nd search

        //validate and populate searches
        private void SearchItem()
        {
            DataTable dtdcp = new DataTable();

            //validate id is 7 digits and valid
            if (cbItem.SelectedIndex == 0)
            {
                empty = false;

                var = "ID";

                txtValueItem.Visible = true;

                if (!(txtValueItem.Text.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits.");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    cbItem2.Enabled = false;
                    enteredValue = false;
                    return;
                }
                else if (!(dodge.GetDCPids(txtValueItem.Text)))
                {
                    MessageBox.Show("Invalid ID.");
                        txtValueItem.Focus();
                        txtValueItem.Clear();
                        cbItem2.Enabled = false;
                        enteredValue = false;
                        return;
                }
                else
                {
                    enteredValue = true;
                    txt = txtValueItem.Text;
                }
            }
            //validate respid is 7 digits and valid
            else if (cbItem.SelectedIndex == 1)
            {
                empty = false;

                var = "RESPID";

                txtValueItem.Visible = true;

                if (!(txtValueItem.Text.Length == 7))
                {
                    MessageBox.Show("RESPID should be 7 digits.");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    cbItem2.Enabled = false;
                    enteredValue = false;
                    return;
                }
                else if (!(GeneralDataFuctions.ChkRespid(txtValueItem.Text))) 
                {
                    MessageBox.Show("Invalid RESPID.");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    cbItem2.Enabled = false;
                    enteredValue = false;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt = txtValueItem.Text;
                }
            }
            //validate survey
            else if (cbItem.SelectedIndex == 2)
            {
                empty = false;

                var = "SURVEY";

                cbValueItem.Visible = true;

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else if (cbValueItem.SelectedItem.ToString() == "NONRESIDENTIAL")
                {
                    enteredValue = true;
                    txt = "NR";
                }
                else if (cbValueItem.SelectedItem.ToString() == "STATE & LOCAL")
                {
                    enteredValue = true;
                    txt = "SL";
                }
                else if (cbValueItem.SelectedItem.ToString() == "FEDERAL")
                {
                    enteredValue = true;
                    txt = "FD";
                }
                else if (cbValueItem.SelectedItem.ToString() == "UTILITIES")
                {
                    enteredValue = true;
                    txt = "UT";
                }
            }
            //validate newtc is 2 or 4 digit and in valid list or newtclist
            else if (cbItem.SelectedIndex == 3)
            {
                empty = false;

                var = "NEWTC";

                txtValueItem.Visible = true;

                string fmt = "00";

                // create a list for valid 2 digit newtcs
                List<int> newtcList = new List<int>();
                for (int i = 0; i <= 16; i++)
                {
                    newtcList.Add(i);
                }
                for (int i = 19; i <= 39; i++)
                {
                    newtcList.Add(i);
                }

                string[] newtc_array = newtcList.Select(i => i.ToString(fmt)).ToArray();

                if ((txtValueItem.Text.Trim() == "") || ((txtValueItem.TextLength != 2) && (txtValueItem.TextLength != 4)))
                {
                    MessageBox.Show("Invalid NEWTC");
                    txtValueItem.Focus();
                    txtValueItem.Clear();
                    cbItem2.Enabled = false;
                    empty = true;
                    return;
                }
                else if (txtValueItem.Text.Length == 2)
                {
                    if (!(newtc_array.Contains(txtValueItem.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtValueItem.Focus();
                        txtValueItem.Clear();
                        cbItem2.Enabled = false;
                        enteredValue = false;
                        return;
                    }
                    else
                    {
                        enteredValue = true;
                        txt = txtValueItem.Text;
                    }
                }
                else if (txtValueItem.Text.Length == 4)
                {
                    //check to see if 4 digit newtc is valid in newtclist table
                    if (!(GeneralDataFuctions.CheckNewTC(txtValueItem.Text))) 
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtValueItem.Focus();
                        txtValueItem.Clear();
                        enteredValue = false;
                        cbItem2.Enabled = false;
                        return;
                    }
                    else
                    {
                        enteredValue = true;
                        txt = txtValueItem.Text;
                    }
                }
            }
            //check npc work status
            else if (cbItem.SelectedIndex == 4)
            {
                empty = false;

                var = "TNPCWORKED";

                cbValueItem.Visible = true;

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else if (cbValueItem.SelectedItem.ToString() == "NOT STARTED")
                {
                    enteredValue = true;
                    txt = cbValueItem.SelectedItem.ToString();
                }
                else if (cbValueItem.SelectedItem.ToString() == "REVIEWED")
                {
                    enteredValue = true;
                    txt = cbValueItem.SelectedItem.ToString();
                }
                else if (cbValueItem.SelectedItem.ToString() == "FINISHED")
                {
                    enteredValue = true;
                    txt = cbValueItem.SelectedItem.ToString();
                }
            }
            //check hq work status
            else if (cbItem.SelectedIndex == 5)
            {
                empty = false;

                var = "THQWORKED";

                cbValueItem.Visible = true;

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else if (cbValueItem.SelectedItem.ToString() == "NOT STARTED")
                {
                    enteredValue = true;
                    txt = cbValueItem.SelectedItem.ToString();
                }
                else if (cbValueItem.SelectedItem.ToString() == "REVIEWED")
                {
                    enteredValue = true;
                    txt = cbValueItem.SelectedItem.ToString();
                }
                else if (cbValueItem.SelectedItem.ToString() == "FINISHED")
                {
                    enteredValue = true;
                    txt = cbValueItem.SelectedItem.ToString();
                }
            }
            //check NPC 1st reviewer's name
            else if (cbItem.SelectedIndex == 6)
            {
                empty = false;

                var = "REV1NME";

                cbValueItem.Visible = true;

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt = cbValueItem.Text.Trim();
                }
            }
            //check NPC 2nd reviewer's name
            else if (cbItem.SelectedIndex == 7)
            {
                empty = false;

                var = "REV2NME";

                cbValueItem.Visible = true;

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt = cbValueItem.Text.Trim();
                }
            }
            //check HQ reviewer's name
            else if (cbItem.SelectedIndex == 8)
            {
                empty = false;

                var = "HQNME";

                cbValueItem.Visible = true;

                if (cbValueItem.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem.SelectedIndex = -1;
                    cbValueItem.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt = cbValueItem.Text.Trim();
                }
            }

            //necessary to null these for the parameters to DAL if selection of 2nd search not used
            var2 = null;
            txt2 = null;

            //validate id is 7 digits and valid
            if (cbItem2.SelectedIndex == 0)
            {
                empty = false;

                var2 = "ID";

                txtValueItem2.Visible = true;

                if (!(txtValueItem2.Text.Length == 7))
                {
                    MessageBox.Show("ID should be 7 digits.");
                    txtValueItem2.Focus();
                    txtValueItem2.Clear();
                    enteredValue = false;
                    return;
                }
                else if (!(dodge.GetDCPids(txtValueItem2.Text)))
                {
                    MessageBox.Show("Invalid ID.");
                    txtValueItem2.Focus();
                    txtValueItem2.Clear();
                    enteredValue = false;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt2 = txtValueItem2.Text;
                }
            }
            //validate respid is 7 digits and valid
            else if (cbItem2.SelectedIndex == 1)
            {
                empty = false;

                var2 = "RESPID";

                txtValueItem2.Visible = true;

                if (!(txtValueItem2.Text.Length == 7))
                {
                    MessageBox.Show("RESPID should be 7 digits.");
                    txtValueItem2.Focus();
                    txtValueItem2.Clear();
                    enteredValue = false;
                    return;
                }
                else if (!(GeneralDataFuctions.ChkRespid(txtValueItem2.Text)))
                {
                    MessageBox.Show("Invalid RESPID.");
                    txtValueItem2.Focus();
                    txtValueItem2.Clear();
                    enteredValue = false;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt2 = txtValueItem2.Text;
                }
            }
            //validate survey
            else if (cbItem2.SelectedIndex == 2)
            {
                empty = false;

                var2 = "SURVEY";

                cbValueItem2.Visible = true;

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Focus();
                    empty = true;
                    return;
                }
                else if (cbValueItem2.SelectedItem.ToString() == "NONRESIDENTIAL")
                {
                    enteredValue = true;
                    txt2 = "NR";
                }
                else if (cbValueItem2.SelectedItem.ToString() == "STATE & LOCAL")
                {
                    enteredValue = true;
                    txt2 = "SL";
                }
                else if (cbValueItem2.SelectedItem.ToString() == "FEDERAL")
                {
                    enteredValue = true;
                    txt2 = "FD";
                }
                else if (cbValueItem2.SelectedItem.ToString() == "UTILITIES")
                {
                    enteredValue = true;
                    txt2 = "UT";
                }
            }
            //validate newtc is 2 or 4 digit and in valid list or newtclist
            else if (cbItem2.SelectedIndex == 3)
            {
                empty = false;

                var2 = "NEWTC";

                txtValueItem2.Visible = true;

                string fmt = "00";

                // create a list for valid 2 digit newtcs
                List<int> newtcList = new List<int>();
                for (int i = 0; i <= 16; i++)
                {
                    newtcList.Add(i);
                }
                for (int i = 19; i <= 39; i++)
                {
                    newtcList.Add(i);
                }

                string[] newtc_array = newtcList.Select(i => i.ToString(fmt)).ToArray();

                if  ((txtValueItem2.Text.Trim() == "") || ((txtValueItem2.TextLength != 2) && (txtValueItem2.TextLength != 4)))
                {
                    MessageBox.Show("Invalid NEWTC");
                    txtValueItem2.Focus();
                    txtValueItem2.Clear();
                    empty = true;
                    return;
                }
                else if (txtValueItem2.Text.Length == 2)
                {
                    if (!(newtc_array.Contains(txtValueItem2.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtValueItem2.Focus();
                        txtValueItem2.Clear();
                        enteredValue = false;
                        return;
                    }
                    else
                    {
                        enteredValue = true;
                        txt2 = txtValueItem2.Text;
                    }
                }
                else if (txtValueItem2.Text.Length == 4)
                {
                    //check to see if 4 digit newtc is valid in newtclist table
                    if (!(GeneralDataFuctions.CheckNewTC(txtValueItem2.Text)))
                    {
                        MessageBox.Show("Invalid NEWTC");
                        txtValueItem2.Focus();
                        txtValueItem2.Clear();
                        enteredValue = false;
                        return;
                    }
                    else
                    {
                        enteredValue = true;
                        txt2 = txtValueItem2.Text;
                    }
                }
            }
            //check npc work status
            else if (cbItem2.SelectedIndex == 4)
            {
                empty = false;

                var2 = "TNPCWORKED";

                cbValueItem2.Visible = true;

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Focus();
                    empty = true;
                    return;
                }
                else if (cbValueItem2.SelectedItem.ToString() == "NOT STARTED")
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.SelectedItem.ToString();
                }
                else if (cbValueItem2.SelectedItem.ToString() == "REVIEWED")
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.SelectedItem.ToString();
                }
                else if (cbValueItem2.SelectedItem.ToString() == "FINISHED")
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.SelectedItem.ToString();
                }
            }
            //check hq work status
            else if (cbItem2.SelectedIndex == 5)
            {
                empty = false;

                var2 = "THQWORKED";

                cbValueItem2.Visible = true;

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Focus();
                    empty = true;
                    return;
                }
                else if (cbValueItem2.SelectedItem.ToString() == "NOT STARTED")
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.SelectedItem.ToString();
                }
                else if (cbValueItem2.SelectedItem.ToString() == "REVIEWED")
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.SelectedItem.ToString();
                }
                else if (cbValueItem2.SelectedItem.ToString() == "FINISHED")
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.SelectedItem.ToString();
                }
            }
            //check NPC 1st Reviewer's name
            else if (cbItem2.SelectedIndex == 6)
            {
                empty = false;

                var2 = "REV1NME";

                cbValueItem2.Visible = true;

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.Text.Trim();
                }
            }
            //check NPC 2nd Reviewer's name
            else if (cbItem2.SelectedIndex == 7)
            {
                empty = false;

                var2 = "REV2NME";

                cbValueItem2.Visible = true;

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.Text.Trim();
                }
            }
            //check HQ reviewer's name
            else if (cbItem2.SelectedIndex == 8)
            {
                empty = false;

                var2 = "HQNME";

                cbValueItem2.Visible = true;

                if (cbValueItem2.Text.Trim() == "")
                {
                    MessageBox.Show("A value must be selected from the Drop Down Menu.");
                    cbValueItem2.SelectedIndex = -1;
                    cbValueItem2.Focus();
                    empty = true;
                    return;
                }
                else
                {
                    enteredValue = true;
                    txt2 = cbValueItem2.Text.Trim();
                }
            }

            //search for the datagrid results based on criteria selected
            if (empty == false)
            {
                dtdcp = dodge.SearchDCPReview(var, txt, var2, txt2);

                dgData.DataSource = dtdcp;

                if (dtdcp.Rows.Count == 0)
                {
                    MessageBox.Show("No data to display.");
                }
            }
        }

        private void txtValueItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((cbItem.SelectedIndex == 0) || (cbItem2.SelectedIndex == 0) ||
                (cbItem.SelectedIndex == 1) || (cbItem2.SelectedIndex == 1) ||
                (cbItem.SelectedIndex == 3) || (cbItem2.SelectedIndex == 3))
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        //Strings to populate search comboboxes
        private string[] SurveySearch = { "", "NONRESIDENTIAL", "STATE & LOCAL", "FEDERAL", "UTILITIES" };
        private string[] StatusSearch = { "", "NOT STARTED", "REVIEWED", "FINISHED" };
        private string[] SurveySearch2 = { "", "NONRESIDENTIAL", "STATE & LOCAL", "FEDERAL", "UTILITIES" };
        private string[] StatusSearch2 = { "", "NOT STARTED", "REVIEWED", "FINISHED" };

        //function to populate 1st combobox with appropriate search string
        private void showSearchItems(string[] search)
        {
            cbValueItem.DataSource = null;
            cbValueItem.Items.Clear();
            cbValueItem.Items.AddRange(search);
        }

        //function to populate 2nd combobox with appropriate search string
        private void showSearchItems2(string[] search2)
        {
            cbValueItem2.DataSource = null;
            cbValueItem2.Items.Clear();
            cbValueItem2.Items.AddRange(search2);
        }

        //Populate VALUE combobox if needed
        private void PopulateValueCombo(int cbIndex)
        {
            //id, respid, newtc  non-comboboxes
            if ((cbIndex == 0) || (cbIndex == 1) || (cbIndex == 3))
            {
            }
            //survey
            else if (cbIndex == 2)
            {
                showSearchItems(SurveySearch);
                cbValueItem.ValueMember = "survey";
                cbValueItem.DisplayMember = "survey";
            }
            //hq work status
            else if (cbIndex == 4)
            {
                showSearchItems(StatusSearch);
                cbValueItem.ValueMember = "hqstatus";
                cbValueItem.DisplayMember = "hqstatus";
            }
            //npc work status
            else if (cbIndex == 5)
            {
                showSearchItems(StatusSearch);
                cbValueItem.ValueMember = "npcstatus";
                cbValueItem.DisplayMember = "npcstatus";
            }
            //npc 1st review
            else if (cbIndex == 6)
            {
                cbValueItem.DataSource = dodge.GetValueList(cbIndex);
                cbValueItem.ValueMember = "rev1nme";
                cbValueItem.DisplayMember = "rev1nme";
            }
            //npc 2nd review
            else if (cbIndex == 7)
            {
                cbValueItem.DataSource = dodge.GetValueList(cbIndex);
                cbValueItem.ValueMember = "rev2nme";
                cbValueItem.DisplayMember = "rev2nme";
            }
            //hq review
            else if (cbIndex == 8)
            {
                cbValueItem.DataSource = dodge.GetValueList(cbIndex);
                cbValueItem.ValueMember = "hqnme";
                cbValueItem.DisplayMember = "hqnme";
            }

            cbValueItem.SelectedIndex = -1;
        }

        //Populate 2nd VALUE combobox if needed
        private void PopulateValueCombo2(int cbIndex2)
        {
            //id, respid, newtc  non-comboboxes
            if ((cbIndex2 == 0) || (cbIndex2 == 1) || (cbIndex2 == 3))
            {
            }
            //survey
            else if (cbIndex2 == 2)
            {
                showSearchItems2(SurveySearch);
                cbValueItem2.ValueMember = "survey";
                cbValueItem2.DisplayMember = "survey";
            }
            //hq work status
            else if (cbIndex2 == 4)
            {
                showSearchItems2(StatusSearch);
                cbValueItem2.ValueMember = "hqstatus";
                cbValueItem2.DisplayMember = "hqstatus";
            }
            //npc work status
            else if (cbIndex2 == 5)
            {
                showSearchItems2(StatusSearch);
                cbValueItem2.ValueMember = "npcstatus";
                cbValueItem2.DisplayMember = "npcstatus";
            }
            //npc 1st review
            else if (cbIndex2 == 6)
            {
                cbValueItem2.DataSource = dodge.GetValueList(cbIndex2);
                cbValueItem2.ValueMember = "rev1nme";
                cbValueItem2.DisplayMember = "rev1nme";
            }
            //npc 2nd review
            else if (cbIndex2 == 7)
            {
                cbValueItem2.DataSource = dodge.GetValueList(cbIndex2);
                cbValueItem2.ValueMember = "rev2nme";
                cbValueItem2.DisplayMember = "rev2nme";
            }
            //hq review
            else if (cbIndex2 == 8)
            {
                cbValueItem2.DataSource = dodge.GetValueList(cbIndex2);
                cbValueItem2.ValueMember = "hqnme";
                cbValueItem2.DisplayMember = "hqnme";
            }

            cbValueItem2.SelectedIndex = -1;
        }

        //first combobox selection
        private void cbItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            enteredItem = true;

            //These searches allow data entry in text box
            //0=id 1=respid 3=newtc
            if (cbItem.SelectedIndex == 0 || cbItem.SelectedIndex == 1 || cbItem.SelectedIndex == 3)
            {
                cbValueItem.Visible = false;
                txtValueItem.Visible = true;
                txtValueItem.Focus();
                txtValueItem.Clear();

                //verify id/respid is 7 digits in length
                if ((cbItem.SelectedIndex == 0) || (cbItem.SelectedIndex == 1))
                {
                    txtValueItem.MaxLength = 7;
                }

                //verify newtc is not over 4 digits in length
                if (cbItem.SelectedIndex == 3)
                {
                    txtValueItem.MaxLength = 4;
                }

                txtValueItem.Text = "";
            }
            //make visible combobox dropdown searches for all other selections
            else
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

        //2nd set of combobox search selection
        private void cbItem2_SelectedIndexChanged(object sender, EventArgs e)
        {
            //These searches allow data entry in text box
            //0=id 1=respid 3=newtc
            if (cbItem2.SelectedIndex == 0 || cbItem2.SelectedIndex == 1 || cbItem2.SelectedIndex == 3)
            {
                cbValueItem2.Visible = false;
                txtValueItem2.Visible = true;
                txtValueItem2.Focus();
                txtValueItem2.Clear();

                //verify id is 7 digits in length
                if ((cbItem2.SelectedIndex == 0) || (cbItem2.SelectedIndex == 1))
                {
                    txtValueItem2.MaxLength = 7;
                }

                //verify newtc is not over 4 digits in length
                if (cbItem2.SelectedIndex == 3)
                {
                    txtValueItem2.MaxLength = 4;
                }

                txtValueItem2.Text = "";
            }
            //make visible combobox dropdown searches for all other selections
            else
            {
                txtValueItem2.Visible = false;
                cbValueItem2.Visible = true;
                PopulateValueCombo2(cbItem2.SelectedIndex);
                cbValueItem2.Focus();
            }

            cbValueItem2.Text = "";
        }

        //Validate that these selections contain only integers upon entry into textbox
        private void txtValueItem_TextChanged(object sender, EventArgs e)
        {
            //validate the text
            Control ctr = (Control)sender;

            if ((cbItem.SelectedIndex == -1) && (txtValueItem.Text != ""))
            {
                MessageBox.Show("A value must be selected from the Drop Down Menu.");
                txtValueItem.Clear();
                cbValueItem.SelectedIndex = -1;
                cbValueItem.Focus();
                empty = true;
                return;
            }

            //verifies the text is numeric
            if (!String.IsNullOrEmpty(ctr.Text))
            {
                if (cbItem.SelectedIndex == 0)
                {
                    enteredValue = false;
                    GeneralFunctions.CheckIntegerField(sender, "ID");
                }
                else if (cbItem.SelectedIndex == 1)
                {
                    enteredValue = false;
                    GeneralFunctions.CheckIntegerField(sender, "RESPID");
                }
                else if (cbItem.SelectedIndex == 3)
                {
                    enteredValue = false;
                    GeneralFunctions.CheckIntegerField(sender, "NEWTC");
                }

                if (txtValueItem.Text == "")
                {
                    cbItem2.Enabled = false;
                    enteredValue = false;
                }
                else
                { 
                    enteredValue = true;             
                }

                EnablingSecondSearch();
            }
            //disables second search boxes if cleared out non-numeric text from first selection
            else
            {

                cbItem2.DataSource = null;
                cbValueItem2.DataSource = null;

                cbValueItem2.Visible = false;
                txtValueItem2.Visible = false;

                cbItem2.SelectedIndex = -1;
                cbValueItem2.SelectedIndex = -1;
                cbValueItem2.Items.Clear();
                txtValueItem2.Text = "";

                cbItem2.Enabled = false;
                cbValueItem2.Enabled = false;
                txtValueItem2.Enabled = false;
            }
        }

        //Validate that these selections contain only integers upon entry into textbox
        private void txtValueItem2_TextChanged(object sender, EventArgs e)
        {
            //validate the text
            Control ctr = (Control)sender;

            if ((cbItem2.SelectedIndex == -1) && (txtValueItem2.Text != ""))
            {
                MessageBox.Show("A value must be selected from the Drop Down Menu.");
                txtValueItem2.Clear();
                cbValueItem2.SelectedIndex = -1;
                cbValueItem2.Focus();
                empty = true;
                return;
            }

            //verifies the text is numeric
            if (!String.IsNullOrEmpty(ctr.Text))
            {
                if (cbItem2.SelectedIndex == 0)
                {
                    enteredValue = false;
                    GeneralFunctions.CheckIntegerField(sender, "ID");
                }
                else if (cbItem2.SelectedIndex == 1)
                {
                    enteredValue = false;
                    GeneralFunctions.CheckIntegerField(sender, "RESPID");
                }
                else if (cbItem2.SelectedIndex == 3)
                {
                    enteredValue = false;
                    GeneralFunctions.CheckIntegerField(sender, "NEWTC");
                }

                enteredValue = true;
                txtValueItem2.Visible = true;
            }
            else
            {
                cbValueItem2.Visible = false;
            }
        }

        //Validate there was a selection made in the combobox
        //in order to activate the second search boxes
        private void cbValueItem_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbValueItem.SelectedIndex >= 0)
            {
                enteredValue = true;

                //don't enable for hqname, rev1nme or rev2nme yet
                if ((cbItem.SelectedIndex != 6) && (cbItem.SelectedIndex != 7) && (cbItem.SelectedIndex != 8))
                {
                    EnablingSecondSearch();
                }
                //but enable for other combo box selections
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

        //This code populates row header of datagrid dynamically
        private void dgData_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            using (SolidBrush b = new SolidBrush(dgData.RowHeadersDefaultCellStyle.ForeColor))
            {
                e.Graphics.DrawString((e.RowIndex + 1).ToString(), e.InheritedRowStyle.Font, b, e.RowBounds.Location.X + 10, e.RowBounds.Location.Y + 4);
            }
        }

        //search button
        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            if (cbItem.Text.Trim() == "")
            {
                MessageBox.Show("A search item must be selected from the Search By Drop Down Box.");
                cbValueItem.SelectedIndex = -1;
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

            //reload datagrid screen
            dgData.DataSource = dodge.GetDCPReview();
            dt = dodge.GetDCPReview();
            DisplayColumns();
            CalculateTotals();

            cbValueItem.Visible = false;
            txtValueItem.Visible = false;
            cbValueItem2.Visible = false;
            txtValueItem2.Visible = false;
        }

        //This will pass parameters to Dodge Initial Address Screen
        private void btnData_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("Functionality not implemented yet");
            DodgeInitialReviewData dodgeinit = new DodgeInitialReviewData();
            frmDodgeInital fDodgeInit = new frmDodgeInital();

            this.Hide(); // hide parent

            int index = dgData.CurrentRow.Index;

            string id = dgData["ID", index].Value.ToString();

            //// Store id in list for Page Up and Page Down
            List<string> idlist = new List<string>();

            int cnt = 0;           
            foreach (DataGridViewRow dr in dgData.Rows)
            {
                string val = dgData["ID", cnt].Value.ToString();

                idlist.Add(val);
                cnt = cnt + 1;
            }

            //Get the id for the selected row and pass to form
            fDodgeInit.Id = id;
            fDodgeInit.Idlist = idlist;
            fDodgeInit.CurrIndex = index;
            fDodgeInit.CallingForm = this;
            fDodgeInit.EntryPnt = "REV";

            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");

            fDodgeInit.Show();  // show child

        }

        //closing form
        private void frmDodgeInitialReview_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("SEARCH/REVIEW", "EXIT");
        }

        //print button
        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintDocument printDocument1 = new PrintDocument();

            if (printDocument1.PrinterSettings.IsValid)
            {
                printDocument1.PrintPage += new PrintPageEventHandler(printDocument1_PrintPage);
                printDocument1.DefaultPageSettings.Landscape = true;
                printDocument1.DocumentName = "Dodge Initial Case Review";
                printDocument1.OriginAtMargins = false;

                CapturePanel(); //search criteria

                CapturePanel2(); //counts

                if (dgData.RowCount >= 115)
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
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "DODGE INITIAL CASE REVIEW";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false;
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Dodge Initial Case Review";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            Margins margins = new Margins(30, 40, 30, 30);
            printer.PrintMargins = margins;

            Cursor.Current = Cursors.Default;

            try
            {
                e.Graphics.DrawString(UserInfo.UserName, new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular), Brushes.Black, 40, 10);
                e.Graphics.DrawImage(resized, 0, 50);
                e.Graphics.DrawImage(resized2, 200, 200);

                if (dgData.Rows.Count > 0)
                {
                    //resize columns for printing
                    dgData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                    dgData.Columns["ID"].Width = 75;
                    dgData.Columns["RESPID"].Width = 75;
                    dgData.Columns["THQWORKED"].Width = 87;
                    dgData.Columns["HQNME"].Width = 83;
                    dgData.Columns["TNPCWORKED"].Width = 87;
                    dgData.Columns["REV1NME"].Width = 83;
                    dgData.Columns["REV2NME"].Width = 83;
                    dgData.Columns["NEWTC"].Width = 69;
                    dgData.Columns["OWNER"].Width = 69;
                    dgData.Columns["PROJDESC"].Width = 150;
                    dgData.Columns["PROJLOC"].Width = 150;

                    printer.PrintDataGridViewWithoutDialog(dgData);

                    //put columns back to original size to fit screen
                    dgData.Columns["ID"].Width = 79;
                    dgData.Columns["RESPID"].Width = 79;
                    dgData.Columns["THQWORKED"].Width = 87;
                    dgData.Columns["HQNME"].Width = 83;
                    dgData.Columns["TNPCWORKED"].Width = 87;
                    dgData.Columns["REV1NME"].Width = 83;
                    dgData.Columns["REV2NME"].Width = 83;
                    dgData.Columns["NEWTC"].Width = 69;
                    dgData.Columns["OWNER"].Width = 69;
                    dgData.Columns["PROJDESC"].Width = 197;
                    dgData.Columns["PROJLOC"].Width = 197;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
