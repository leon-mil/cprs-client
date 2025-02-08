/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCeProjectAuditPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/26/2015
Inputs:             ID
Parameters:		    None                
Outputs:		    None

Description:	    This program displays the Ceaudit data
                    for a id case

Detailed Design:    Detailed User Requirements for Improvement Screen 

Other:	            Called from: frmImprovements
 
Revision History:	
***********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
***********************************************************************************/

using System;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using CprsDAL;
using System.Text.RegularExpressions;

namespace Cprs
{
    public partial class frmCeProjectAuditPopup : Form
    {
        private string id;

        public frmCeProjectAuditPopup(string passed_id)
        {
            InitializeComponent();
            id = passed_id;
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

        //Private variables

        private DataTable dtItem = null;

        /*Create instance of CeProjectAuditData object */
        private CeProjectAuditData dataObject;

        private void frmCeProjectAuditPopup_Load(object sender, EventArgs e)
        {
            dataObject = new CeProjectAuditData();
            dtItem = dataObject.GetProjectItemAudits(id, "", "", "");

            dgItem.DataSource = dtItem;
        

            /*resize the columns*/

            dgItem.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

            for (int i = 0; i < dgItem.ColumnCount; i++)
            {
                dgItem.Columns[i].Width = 160;
                dgItem.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

            setItemColumnHeader();

        }

        private void setItemColumnHeader()
        {
            dgItem.Columns[0].HeaderText = "ID";
            dgItem.Columns[1].HeaderText = "INTERVIEW";
            dgItem.Columns[2].HeaderText = "JOBCODE";
            dgItem.Columns[3].HeaderText = "VARNME";
            dgItem.Columns[4].HeaderText = "OLDVAL";
            dgItem.Columns[4].DefaultCellStyle.Format = "N0";
            dgItem.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgItem.Columns[5].HeaderText = "OLDFLAG";
            dgItem.Columns[6].HeaderText = "NEWVAL";
            dgItem.Columns[6].DefaultCellStyle.Format = "N0";
            dgItem.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgItem.Columns[7].HeaderText = "NEWFLAG";
            dgItem.Columns[8].HeaderText = "USER";
            dgItem.Columns[9].HeaderText = "DATE/TIME";
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
