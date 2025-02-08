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
    public partial class frmError : Form
    {
        private Exception errorText;

        public frmError(Exception error_text)
        {
            InitializeComponent();
            errorText = error_text;
        }

        private void frmError_Load(object sender, EventArgs e)
        {
            txtError.Text = errorText.ToString();
        }
    }
}
