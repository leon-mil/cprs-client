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
    partial class frmRespCentpwdPopup : Form
    {

        public frmRespCentpwdPopup(string Passwd)
        {
            InitializeComponent();
            txtPasswd.Text = Passwd;
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

        }

        private void frmRespCentpwdPopup_Load(object sender, EventArgs e)
        {

        }
    }
}
