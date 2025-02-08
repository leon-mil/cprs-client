/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmMonthlyAuditReviewPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      09/20/2016
Inputs:             None
Parameters:		    None                
Outputs:		    None
Description:	    This program displays the auditrvw data
                   
Detailed Design:    Detailed User Requirements for Admin 

Other:	            Called from: frmSystemAudit
 
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
    public partial class frmMonthlyAuditReviewPopup : Form
    {
        public frmMonthlyAuditReviewPopup()
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


        /*Create instance of MonthlyAuditReviewData object */
        private MonthlyAuditReviewData dataObject;

        private void frmMonthlyAuditReviewPopup_Load(object sender, EventArgs e)
        {
            LoadForm();
        }

        private void LoadForm()
        {
            dataObject = new MonthlyAuditReviewData();
            dgData.DataSource = dataObject.GetMonthlyAuditReviewTable();

            /*resize the columns*/
            dgData.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;

            dgData.Columns[0].Width = 60;
            dgData.Columns[1].Width = 60;
            dgData.Columns[2].Width = 120;
            dgData.Columns[3].Width = 65;

            setItemColumnHeader();
        }

        private void setItemColumnHeader()
        {
            dgData.Columns[0].HeaderText = "MONTH";
            dgData.Columns[1].HeaderText = "USER";
            dgData.Columns[2].HeaderText = "DATE/TIME";
            dgData.Columns[3].HeaderText = "ACTIVITY";
            dgData.Columns[4].HeaderText = "COMMENT";
            dgData.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            frmAddMonthlyAuditPopup popup = new frmAddMonthlyAuditPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();
            LoadForm();
        }

    }
}
