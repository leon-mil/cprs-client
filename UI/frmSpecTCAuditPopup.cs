
/********************************************************************
 Econ App Name : CPRS
 Project Name  : CPRS Interactive Screens System
 Program Name  : frmSpecialPriorityTCAuditPopup.cs
 Programmer    : Diane Musachio
 Creation Date : 10/19/2017
 Inputs        : N/A
 Parameters    : N/A
 Output        : N/A
 Description   : This program will display tc audit table
 Detail Design : Detailed Design for Special Priority TC
 Other         : Called by: frmSpecialPriorityTC.cs
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

namespace Cprs
{
    public partial class frmSpecTCAuditPopup : Form
    {
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

        public frmSpecTCAuditPopup()
        {
            InitializeComponent();
        }

        private void frmSpecialPriorityTCAuditPopup_Load(object sender, EventArgs e)
        {
            //get audit data to display
            var data_object = new SpecialTCData();
            dgData.DataSource = data_object.GetTCAuditData();

            dgData.Columns[0].HeaderText = "SURVEY";
            dgData.Columns[0].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[0].DefaultCellStyle.Format = "N0";

            dgData.Columns[1].HeaderText = "NEWTC";
            dgData.Columns[1].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[1].DefaultCellStyle.Format = "N0";

            dgData.Columns[2].HeaderText = "ACTION";
            dgData.Columns[2].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[2].DefaultCellStyle.Format = "N0";

            dgData.Columns[3].HeaderText = "OVALMIN";
            dgData.Columns[3].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[3].DefaultCellStyle.Format = "N0";

            dgData.Columns[4].HeaderText = "OVALMAX";
            dgData.Columns[4].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[4].DefaultCellStyle.Format = "N0";

            dgData.Columns[5].HeaderText = "NVALMIN";
            dgData.Columns[5].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[5].DefaultCellStyle.Format = "N0";

            dgData.Columns[6].HeaderText = "NVALMAX";
            dgData.Columns[6].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgData.Columns[6].DefaultCellStyle.Format = "N0";

            dgData.Columns[7].HeaderText = "USER";
            dgData.Columns[7].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
   
            dgData.Columns[8].HeaderText = "DATE/TIME";
            dgData.Columns[8].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgData.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        //return to previous screen
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
