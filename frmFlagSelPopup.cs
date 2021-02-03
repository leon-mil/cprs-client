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
    public partial class frmFlagSelPopup : Form
    {
        public string selectedFlag = "";
        public bool set_default = false;

        public frmFlagSelPopup()
        {
            InitializeComponent();
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

            selectedFlag = checked_items;
            if (ckDefault.Checked)
                set_default = true;


        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
        }
        
    }
}
