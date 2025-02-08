/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmBF.cs

 Programmer    : Diane Musachio

 Creation Date : 5/15/2017

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display screen to review and 
                edit Boost Factors

 Detail Design : Detailed User Requirements Boost Factors

 Other         : Called by: Tabulations -> Maintenance -> Boost Factors

 Revisions     : See Below
 *********************************************************************
 Modified Date : 05/06/17
 Modified By   : Kevin Montgomery
 Keyword       : None
 Change Request: CR 202
 Description   : Make editable in June for Testers
 *********************************************************************
 Modified Date : 07/10/17
 Modified By   : Kevin Montgomery
 Keyword       : None
 Change Request: CR 211
 Description   : Change Back to make editable in May only
 *********************************************************************
 Modified Date : 06/06/19
 Modified By   : Diane Musachio
 Keyword       : dm060619
 Change Request: 
 Description   : entering a decimal produced an error removed keypress 
    event from datagrid and only entered keypress through editing control
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
using System.Text.RegularExpressions;

namespace Cprs
{
    public partial class frmBF : frmCprsParent
    {
        BFdata bf = new BFdata();
        List<string> dy = new List<string>();
        private string survey;
        private string currentyear;
        private string newstatp;
        private string oldval;
        private int rowindex;
        private int columnindex;
        private string table;
        private DateTime latestStatp;


        public frmBF()
        {
            InitializeComponent();

            //default to nonresidential
            rd1n.Checked = true;
            survey = "N";

            //make all data columns non-editable except tc1T
            for (int i = 0; i < 19; i++)
            {
                dgData.Columns[i].ReadOnly = true;

            }

            dgData.Columns[19].ReadOnly = false;

        }

        private void frmBF_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            string currmonth = DateTime.Now.ToString("MM");

            //only editable in month of May for bstann
            if (currmonth == "05")
            { 
                btnInsert.Visible = true;

                table = "BSTANN";
            }
            //otherwise data is uneditable in bsttab
            else
            {
                dgData.ReadOnly = true;
                btnInsert.Visible = false;

                table = "BSTTAB";
            }

            //set state and local to default
            rd1p.Checked = true;

            LoadData();
        }

        private void LoadData()
        {
            GetList();

            DataTable dt = new DataTable();
            dgData.DataSource = null;
            dt = bf.GetBFdata(survey, dy, table);
            dgData.DataSource = dt;
            dgData.Sort(dgData.Columns[0], ListSortDirection.Descending);

            dgData.Refresh();
            dgData.Update();
        }

        //get list of dates for past five years
        private void GetList()
        {
            //initialize list
            dy.Clear();

            //get dates from table by survey
            DataTable dates = new DataTable();
            dates = bf.GetMonthList(table, survey);
            

            //eliminate beyond 5 years of current date
            string fiveyears = DateTime.Now.AddYears(-6).ToString("yyyy");

            currentyear = DateTime.Now.ToString("yyyy");
            
            foreach (DataRow dr in dates.Rows)
            {
                if (dr[0].ToString().Substring(0, 4) == fiveyears)
                {
                    break;
                }

                dy.Add(dr[0].ToString());
            }

            //check to see if months have already been inserted
            for (int i = 0; i < dy.Count; i++)
            {
                if (i == 0)
                {
                    latestStatp = DateTime.ParseExact(dy[i].ToString(), "yyyyMM", null);
                }

                //if june of current year is present then months have been inserted
                if (dy[i].ToString().Substring(0, 4) == currentyear)
                {
                    if (dy[i].ToString().Substring(4, 2) == "06")
                    {
                        btnInsert.Enabled = false;
                    }
                }
            }
        }

        //when radio button selected - title of form changes and relevant 
        //columns are made editable
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            if (radiobutton.Checked)
            {
                if (sender == rd1f)
                {
                    survey = "F";
                 
                    lblTitle.Text = "FEDERAL BOOST FACTORS";

                    //make all data columns non-editable
                    for (int i = 0; i < dgData.ColumnCount; i++)
                    {
                        dgData.Columns[i].ReadOnly = true;
                    }

                    btnInsert.Enabled = false;

                }
                else if (sender == rd1n)
                {
                    survey = "N";
              
                    lblTitle.Text = "NONRESIDENTIAL BOOST FACTORS";

                    //make all data columns non-editable except tc1T
                    for (int i = 0; i < 19; i++)
                    {
                        dgData.Columns[i].ReadOnly = true;
                    }
                    dgData.Columns[19].ReadOnly = false;

                    btnInsert.Enabled = true;
                }
                else if (sender == rd1p)
                {
                    survey = "P";
                    lblTitle.Text = "STATE AND LOCAL BOOST FACTORS";

                    dgData.Columns[0].ReadOnly = true;


                    //make all data columns non-editable
                    for (int i = 1; i < dgData.ColumnCount; i++)
                    {
                        dgData.Columns[i].ReadOnly = false;

                    }
                    btnInsert.Enabled = true;
                }
                else if (sender == rd1u)
                {
                    survey = "U";
                  
                    lblTitle.Text = "UTILITIES BOOST FACTORS";

                    //tc16 and tc19 columns editable
                    for (int i = 0; i < 17; i++)
                    {
                        dgData.Columns[i].ReadOnly = true;
 
                    }
                    dgData.Columns[17].ReadOnly = false;
                    dgData.Columns[18].ReadOnly = false;
                    dgData.Columns[19].ReadOnly = true;

                    btnInsert.Enabled = true;
                }
               
                LoadData();
            }
        }

        //set max insertion length to 5 digits
        private void dgData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                (e.Control as TextBox).MaxLength = 5;
                e.Control.KeyPress += new KeyPressEventHandler(dgData_KeyPress);
            }
        }

        //initialize values at entry of cell edit
        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            rowindex = dgData.CurrentRow.Index;
            columnindex = dgData.CurrentCell.ColumnIndex;
            oldval = (dgData[columnindex, rowindex].Value ?? 0).ToString();
        }

        //update edited data
        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string sdate = (dgData[0, rowindex].Value ?? 0).ToString();
            string owner = survey;
            string newtc = dgData.Columns[columnindex].HeaderText;
            string newval = (dgData[columnindex, rowindex].Value ?? "0.000").ToString();
            bool allZeros = Regex.IsMatch(newval, @"^[0]+(\.[0]+)?$");

            if ((!allZeros) && (newval != ""))
            {
                if (oldval != newval)
                {
                   bf.UpdateBSTANNReview(sdate, owner, newtc, newval);
                }
            }
            else
            {
                dgData[columnindex, rowindex].Value = oldval;
            }
        }

        //ensures all entries are numerical or one decimal
        private void dgData_KeyPress(object sender, KeyPressEventArgs e)
        {
            //dm060619 
            //if (!dgData.ReadOnly) 
            //{
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
          //  }
            
        }

        //if bad data cancel update
        private void dgData_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            if (dgData.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == DBNull.Value)
            {
                e.Cancel = true;
            }
        }

        //center title based on size of string
        private void lblTitle_SizeChanged(object sender, EventArgs e)
        {
            lblTitle.Left = (this.ClientSize.Width - lblTitle.Size.Width) / 2;
        }

        //if button clicked insert 12 stat periods with most recent value forwarded to all 12 months
        private void btnInsert_Click(object sender, EventArgs e)
        {
            //insert 12 statperiods to datatable
            for (int j = 1; j < 13; j++)
            {
                for (int i = 1; i < dgData.ColumnCount; i++)
                {
                    if (i > 0)
                    {
                        string newtc = dgData.Columns[i].HeaderText;
                        string bsf = dgData[i, 0].Value.ToString();

                        newstatp = latestStatp.AddMonths(+j).ToString("yyyyMM");

                        bf.InsertRows(newstatp, survey, newtc, bsf);
                    }
                }
                dy.Add(newstatp);
            }

            LoadData();
        }

        //close form
        private void frmBF_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

    }
}
