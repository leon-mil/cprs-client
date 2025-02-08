using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using CprsDAL;

namespace Cprs
{
    public partial class frmNewtcSel : Form
    {
        public string SelectedNewtc = "";

        public frmNewtcSel()
        {
            InitializeComponent();
        }

        public string CaseOwner = "*";
        public bool ViewOnly = false;
        private NewtcSelData data_object;

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

        private void frmNewtcSel_Load(object sender, EventArgs e)
        {
            data_object = new NewtcSelData();
            DataTable dt = data_object.GetNewtcData(CaseOwner);

            dgNewtc.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgNewtc.RowHeadersVisible = false; // set it to false if not needed
            dgNewtc.DataSource = dt;
            dgNewtc.Columns[0].Width = 80;

            dgNewtc.ClearSelection();

            if (ViewOnly)
                btnOK.Enabled = false;
            else
                btnOK.Enabled = true;

            this.KeyPreview = true;
            txtNewtc.KeyDown +=
                new KeyEventHandler(txtNewtc_KeyDown);

            this.ActiveControl = txtNewtc;

        }

        private void txtNewtc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtNewtc.TextLength > 0)
                    Search();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (dgNewtc.RowCount>0 && dgNewtc.SelectedCells.Count>0)
            {
                string selected_value = dgNewtc.CurrentRow.Cells[0].FormattedValue.ToString();
                SelectedNewtc = selected_value;
                
            }
            else
            {
                SelectedNewtc = "";
                this.DialogResult = DialogResult.Cancel;
            }
                
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void Search()
        {
           
            dgNewtc.DataSource = data_object.GetNewtcSelSearchData(txtNewtc.Text.Trim());
        }

    }
}
