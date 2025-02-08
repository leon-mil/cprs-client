
/**********************************************************************************
Econ App Name:     CPRS
Project Name:      CPRS Interactive Screens System
Program Name:      frmWhereRecall.cs		    	
Programmer:        Christine Zhang
Creation Date:     Sept. 23 2015
Inputs:                                
Parameters:		                    
Outputs:		    Where clause
Description:	    Select where clause from the form
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
    public partial class frmWhereRecall : Form
    {
        public string WhereClause = "";
        private InteractiveSearchData dataObject;
        private List<SearchCriteria> clist;

        public frmWhereRecall()
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

        private void frmWhereRecall_Load(object sender, EventArgs e)
        {
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
                        dgData.Columns[i].Width = 60;
                    }

                    if (i == 1)
                    {
                        dgData.Columns[i].HeaderText = "WHERE";
                        dgData.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }

                btnDelete.Enabled = true;
                btnSelect.Enabled = true;
            }
            else
            {
                dgData.DataSource = dataObject.GetEmptyWhereTable();
                btnDelete.Enabled = false;
                btnSelect.Enabled = false; 
            }
        }


        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (clist.Count > 0)
            {
                WhereClause = dgData.CurrentRow.Cells[1].Value.ToString().Trim();
                this.Close();
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
                    {
                        LoadData();
                    }
                }
            }
        }
    }
}
