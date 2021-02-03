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
    public partial class frmActiveSel : Form
    {
        public string selectedActive = "";

        public frmActiveSel()
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

        private void frmActiveSel_Load(object sender, EventArgs e)
        {
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string checked_items = "";

            // Next show the object title and check state for each item selected. 
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                if (checked_items == "")
                    checked_items = itemChecked.ToString().Substring(0, 1);
                else
                    checked_items = checked_items + "," + itemChecked.ToString().Substring(0, 1);

            }

            selectedActive = checked_items;

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }
    }
}
