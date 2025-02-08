
/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmWhereStore.cs	    	
Programmer:         Christine Zhang
Creation Date:      Sept. 23 2015
Inputs:             Where Clause                   
Parameters:		                    
Outputs:		    
Description:	    Store where clause screen
Detailed Design:    
Other:	            call from frmWhereSrch.cs
 
Revision History:	
***********************************************************************************
Modified Date :  
Modified By   :  
Keyword       :  
Change Request:  
Description   :  
***********************************************************************************/
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


namespace Cprs
{
    public partial class frmWhereStore : Form
    {
        public string WhereClause = "";
        private InteractiveSearchData dataObject;
        private List<SearchCriteria> clist;

        public frmWhereStore()
        {
            InitializeComponent();
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmWhereStore_Load(object sender, EventArgs e)
        {
            if (WhereClause != "")
                txtCriteria.Text = WhereClause;
            else
                txtCriteria.Text = "";

            dataObject = new InteractiveSearchData();
            LoadData();           
        }

        /*Get data and load to form */
        private void LoadData()
        {
            /*Get Data */
            clist = dataObject.GetSearchCriteriaData();

            /*Set data grid */
            if (clist != null && clist.Count > 0)
            {
                dgData.Columns.Clear();
                dgData.DataSource = clist;
                dgData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
                dgData.RowHeadersVisible = false; // set it to false if not needed

                /*Set up header and visible */
                for (int i = 0; i < dgData.ColumnCount; i++)
                {
                    if (i == 0)
                    {
                        dgData.Columns[i].HeaderText = "ROW";
                        dgData.Columns[i].Width = 30;
                    }

                    if (i == 1)
                    {
                        dgData.Columns[i].HeaderText = "WHERE";
                        dgData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }

                }

                btnDelete.Enabled = true;
               
            }
            else
            {
                dgData.DataSource = dataObject.GetEmptyWhereTable();

                btnDelete.Enabled = false;
                btnReplace.Enabled = false; 
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
             DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete the selected where clause?", "Question", MessageBoxButtons.YesNo);
             if (dialogResult == DialogResult.Yes)
             {
                 if (clist.Count > 0)
                 {
                     int seq = Convert.ToInt32(dgData.CurrentRow.Cells[0].Value);

                     if (dataObject.DeleteSearchCriteria(clist, seq))
                         LoadData();
                                             
                 }
                
             }                 
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
             DialogResult dialogResult = MessageBox.Show("Are you sure you want to replace the selected where clause?", "Question", MessageBoxButtons.YesNo);
             if (dialogResult == DialogResult.Yes)
             {
                 int seq = Convert.ToInt32(dgData.CurrentRow.Cells[0].Value);
                 if (dataObject.UpdateSearchCriteriaData(seq, txtCriteria.Text))
                 {
                     LoadData();
                     txtCriteria.Text = "";
                     btnAdd.Enabled = false;
                     btnReplace.Enabled = false;
                 }
             }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (clist != null && clist.Count == 10)
            {
                MessageBox.Show("The number of where clauses cannot be more than 10, cannot be added");
                return;
            }
            else
            {
                if (clist == null)
                    dataObject.AddSearchCriteriaData(1, txtCriteria.Text);
                else
                    dataObject.AddSearchCriteriaData(clist.Count + 1, txtCriteria.Text);

                //retrieve data
                LoadData();

                txtCriteria.Text = "";
                btnAdd.Enabled = false;
                btnReplace.Enabled = false;
            }
        }
    }
}
