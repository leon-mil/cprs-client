using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Cprs
{
    public partial class frmRespPrintSel : Form
    {
        public string PrintSelection = "";

        public frmRespPrintSel()
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


        private void btnOK_Click(object sender, EventArgs e)
        {
            if (rdbResp.Checked)
                PrintSelection = "Respondent";
            if (rdbProject.Checked)
                PrintSelection = "Project";
        }

        private void rdbResp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbResp.Checked)
                rdbProject.Checked = false;
        }

        private void rdbProject_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbProject.Checked)
                rdbResp.Checked = false;
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            PrintSelection = "";
        }
    }
}
