/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmNPCSpecials.cs
Programmer    : Christine Zhang
Creation Date : Jan 21 2020
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Display NPC Specials Info
Change Request: 
Detail Design : 
Rev History   : See Below
Other         : N/A
 ***********************************************************************
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
using CprsBLL;
using System.Globalization;

namespace Cprs
{
    public partial class frmNPCSpecials : frmCprsParent
    {
        public frmNPCSpecials()
        {
            InitializeComponent();
        }
        private NPCSpecialsData dataObject = new NPCSpecialsData();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNPCSpecialPopup popup = new frmNPCSpecialPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Respid = "";
            DialogResult dialogresult = popup.ShowDialog();
            
            GetNPCSpecials();
            if (dgcr.Rows.Count > 0)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnAddInstruction.Enabled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            //Get respid
            int index;
            string respid;
            if (tabControl1.SelectedIndex == 0)
            {
                index = dgcr.CurrentRow.Index;
                respid = dgcr["RESPID", index].Value.ToString();
            }
            else
            {
                index = dgrc.CurrentRow.Index;
                respid = dgrc["RESPID", index].Value.ToString();
            }
         
            frmNPCSpecialPopup popup = new frmNPCSpecialPopup();
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.Respid = respid;
            DialogResult dialogresult = popup.ShowDialog();
   
            GetNPCSpecials();
        }

        private void btnInstruction_Click(object sender, EventArgs e)
        {
            //Get respid
            int index;
            string respid;

            if (tabControl1.SelectedIndex == 0)
            {
                index = dgcr.CurrentRow.Index;
                respid = dgcr["RESPID", index].Value.ToString();
            }
            else
            {
                index = dgrc.CurrentRow.Index;
                respid = dgrc["RESPID", index].Value.ToString();
            }

            frmNPCSpecialInstPopup popup = new frmNPCSpecialInstPopup(respid);
            popup.StartPosition = FormStartPosition.CenterParent;
            popup.ShowDialog();

            lblUpdate.Text = dataObject.GetLatestUpdate();
        }

        private void frmNPCSpecials_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");

            GetNPCSpecials();      
            if (dgcr.Rows.Count ==0)
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnAddInstruction.Enabled = false;
            }
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "5")
            {
                btnAdd.Enabled = false;
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
            }
        }

        private void frmNPCSpecials_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        //Get NPC authorized users from CprsDAL data
        private void GetNPCSpecials()
        {
            DataTable dtcr = new DataTable();
            
            //populate datagrid with census to Respondents
            dtcr = dataObject.GetNPCSpecialData("CR");
            dgcr.DataSource = dtcr;
            dgcr.Columns[0].HeaderText = "USER";
            dgcr.Columns[3].HeaderText = "MAIL STATUS";
            dgcr.Columns[4].HeaderText = "PHONE STATUS";
            dgcr.Columns[5].HeaderText = "AD HOC EMAIL";
            dgcr.Columns[6].HeaderText = "SPREADSHEET STATUS";
            dgcr.Columns[7].HeaderText = "FAX STATUS";
            dgcr.Columns[8].Visible = false;

            //populate datagrid with respondent to census
            DataTable dtrc = new DataTable();
            dtrc = dataObject.GetNPCSpecialData("RC");
            dgrc.DataSource = dtrc;
            dgrc.Columns[0].HeaderText = "USER";
            dgrc.Columns[3].HeaderText = "MAIL STATUS";
            dgrc.Columns[4].HeaderText = "CENTURION";
            dgrc.Columns[5].HeaderText = "PHONE STATUS";
            dgrc.Columns[6].HeaderText = "EMAIL STATUS";
            dgrc.Columns[7].HeaderText = "SPREADSHEET STATUS";
            dgrc.Columns[8].HeaderText = "FAX STATUS";
            dgrc.Columns[9].Visible = false;

            lblUpdate.Text = dataObject.GetLatestUpdate();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Get respid
            DialogResult dialogResult = MessageBox.Show("This will be permanently deleted. CONTINUE?", "" ,MessageBoxButtons.YesNo);
        
            if (dialogResult == DialogResult.Yes)
            {
                int index = dgcr.CurrentRow.Index;
                string respid = dgcr["RESPID", index].Value.ToString();
                dataObject.DeleteNPCSpecialsData(respid);
                GetNPCSpecials();

                if (dgcr.Rows.Count == 0)
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnAddInstruction.Enabled = false;

                    lblUpdate.Text = DateTime.Now.ToString("MM/dd/yyyy");
                }
            }
        }

        private void dgcr_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewCell cell = dgcr.Rows[e.RowIndex].Cells[e.ColumnIndex];

                cell.ToolTipText = dgcr.Rows[e.RowIndex].Cells[8].Value.ToString();
            }
        }

        private void dgrc_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                DataGridViewCell cell = dgrc.Rows[e.RowIndex].Cells[e.ColumnIndex];

                cell.ToolTipText = dgrc.Rows[e.RowIndex].Cells[9].Value.ToString();
            }
        }

       
    }
}
