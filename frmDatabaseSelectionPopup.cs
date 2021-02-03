using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;

namespace Cprs
{
    public partial class frmDatabaseSelectionPopup : Form
    {
        public string SelectedDatabase = string.Empty;

        public frmDatabaseSelectionPopup()
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

        private void frmDatabaseSelectionPopup_Load(object sender, EventArgs e)
        {
            if (UserInfo.GroupCode != EnumGroups.Programmer)
            {
                cbDB.Items.Clear();

                cbDB.Items.Add("CPRSPROD");
                cbDB.Items.Add("CPRSTEST");

                cbDB.SelectedIndex = 0;
            }
            else
            {
                cbDB.Items.Clear();
               
                cbDB.Items.Add("CPRSPROD");
                cbDB.Items.Add("CPRSTEST");
                cbDB.Items.Add("CPRSDEV");

                cbDB.SelectedIndex = 0;
            }
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbDB.SelectedItem.ToString() == "CPRSTEST")
            {
                DialogResult result1 = MessageBox.Show("Are you sure you want to use Test Database?", "Important Question", MessageBoxButtons.YesNo);
                if (result1 == System.Windows.Forms.DialogResult.No)
                    return;
            }
            SelectedDatabase = cbDB.SelectedItem.ToString();
            this.Close();
        }
    }
}
