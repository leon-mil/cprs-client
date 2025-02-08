/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmNPCSpecialInstPopup.cs
Programmer    : Christine Zhang
Creation Date : Jan 21 2020
Parameters    : respid
Inputs        : N/A
Outputs       : N/A
Description   : enter instruction
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

namespace Cprs
{
    public partial class frmNPCSpecialInstPopup : Form
    {
        private string respid;
        public frmNPCSpecialInstPopup(string passed_respid)
        {
            InitializeComponent();
            respid = passed_respid;
        }

        private NPCSpecialsData dataObject = new NPCSpecialsData();

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

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNPCSpecialInstPopup_Load(object sender, EventArgs e)
        {
            string instr = dataObject.RetrieveNPCSpecialInstr(respid);
            txtInstr.Text = instr;
            txtInstr.SelectionStart = txtInstr.Text.Length;
            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "5")
            {
                txtInstr.Enabled = false;
                btnSave.Enabled = false;
                btnClear.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool result = dataObject.UpdateNPCSpecialsInstr(respid, txtInstr.Text.Trim());

            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtInstr.Text = "";
        }
    }
}
