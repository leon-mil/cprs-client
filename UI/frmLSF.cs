/********************************************************************
 Econ App Name : CPRS
 
 Project Name  : CPRS Interactive Screens System

 Program Name  : frmLSF.cs

 Programmer    : Diane Musachio

 Creation Date : 5/11/2017

 Inputs        : N/A

 Parameters    : N/A
 
 Output        : N/A
  
 Description   : This program will display screen to review and 
                edit Late Selection Factors

 Detail Design : Detailed User Requirements Late Selection Factors

 Other         : Called by: Tabulations -> Maintenance -> Late Selection Factors

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
 *  Modified Date : 06/06/19
 Modified By   : Diane Musachio
 Keyword       : dm060619
 Change Request: 
 Description   : entering a decimal produced an error
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
    public partial class frmLSF : frmCprsParent
    {
        LSFdata lsf = new LSFdata();
        List<string> dy = new List<string>();

        private string survey;
        private string currmonth;
        private string table;
        private string oldval;
        private int rowindex;
        private int columnindex; 

        public frmLSF()
        {
            InitializeComponent();

            //set default owner to N
            rd1n.Checked = true;
            survey = "N";
        }

        private void frmLSF_Load(object sender, EventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            currmonth = DateTime.Now.ToString("MM");

            //only editable in month of May
            if (currmonth == "05")
            {
                dgData.ReadOnly = false;
                dgData.Columns[0].ReadOnly = true;

                table = "LSFANN";
            }
            //otherwise reads in non-editable data
            else
            {
                dgData.ReadOnly = true;
                table = "LSFTAB";
            }
            rd1p.Checked = true;
            LoadData();
        }

        private void LoadData()
        {
            DataTable dt = new DataTable();

            //get data from specific table and owner(survey)
            dt = lsf.GetLSFdata(table,survey);

            dgData.DataSource = dt;
        }

        //if owner selection is changed
        private void RadioButton_CheckedChanged(object sender, EventArgs e)
        {
            var radiobutton = (RadioButton)sender;
            if (radiobutton.Checked)
            {
                if (sender == rd1f)
                {
                    survey = "F";
                    lblTitle.Text = "FEDERAL LATE SELECTION FACTORS";

                    //make all data columns editable
                    for (int i = 1; i < dgData.ColumnCount; i++)
                    {
                        dgData.Columns[i].ReadOnly = false;
                    }
                }
                else if (sender == rd1m)
                {
                    survey = "M";
                 
                    lblTitle.Text = "MULTIFAMILY LATE SELECTION FACTORS";

                    //only editable columns is tc00
                    dgData.Columns[1].ReadOnly = false;
                    for (int i = 2; i < dgData.ColumnCount; i++)
                    {
                        dgData.Columns[i].ReadOnly = true;
                    }
                }
                else if (sender == rd1n)
                {
                    survey = "N";
                    
                    lblTitle.Text = "NONRESIDENTIAL LATE SELECTION FACTORS";

                    //all data columns editable
                    for (int i = 1; i < dgData.ColumnCount; i++)
                    {
                        dgData.Columns[i].ReadOnly = false;
                    }
                }
                else if (sender == rd1p)
                {
                    survey = "P";
                    lblTitle.Text = "STATE AND LOCAL LATE SELECTION FACTORS";

                    //all data columns editable
                    for (int i = 1; i < dgData.ColumnCount; i++)
                    {
                        dgData.Columns[i].ReadOnly = false;
                    }
                }
                else if (sender == rd1u)
                {
                    survey = "U";
                    
                    lblTitle.Text = "UTILITIES LATE SELECTION FACTORS";

                    //tc16 and tc19 columns editable
                    for (int i = 1; i < 17; i++)
                    {
                        dgData.Columns[i].ReadOnly = true;
                    }
                    dgData.Columns[17].ReadOnly = false;
                    dgData.Columns[18].ReadOnly = false;
                    dgData.Columns[19].ReadOnly = true;
                }

                LoadData();
            }
        }

        //set max insertion length to 4 digits
        private void dgData_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (e.Control is TextBox)
            {
                (e.Control as TextBox).MaxLength = 4;
                 e.Control.KeyPress += new KeyPressEventHandler(dgData_KeyPress);
            }
        }

        //initialize values at entry of cell edit
        private void dgData_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            rowindex = dgData.CurrentRow.Index;
            columnindex = dgData.CurrentCell.ColumnIndex;
            oldval = (dgData[columnindex, rowindex].Value ?? "").ToString();           
        }

        //update edited data
        private void dgData_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string lsfno = (dgData[0, rowindex].Value ?? 0).ToString();
            string owner = survey;
            string newtc = dgData.Columns[columnindex].HeaderText;
            string newval = (dgData[columnindex, rowindex].Value ?? "").ToString();
            bool allZeros = Regex.IsMatch(newval, @"^[0]+(\.[0]+)?$");

            if (!allZeros) 
            { 
                if (oldval != newval)
                {
                    lsf.UpdateLSFANNReview(lsfno, owner, newtc, newval);
                }
            }
            else
            {
                dgData[columnindex, rowindex].Value = oldval;
            }
        }

        //dm060619
        //ensures all entries are numeric or one decimal
        private void dgData_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!dgData.ReadOnly)
            {
                if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && (e.KeyChar != '.'))
                {
                    e.Handled = true;
                }

                if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
                {
                    e.Handled = true;
                }
            }        
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

        //close form
        private void frmLSF_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }

    }
}
