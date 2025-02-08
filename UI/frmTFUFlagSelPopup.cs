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
    public partial class frmTFUFlagSelPopup : Form
    {
        public frmTFUFlagSelPopup()
        {
            InitializeComponent();
        }

        /*Public properties */
        public string selectedFlag;
       
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

        private void frmTFUFlagSelPopup_Load(object sender, EventArgs e)
        {
            
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string checked_item = GetCheckedItem();
            if (checked_item == "")
            {
                MessageBox.Show("Please select one flag");
                this.DialogResult = DialogResult.None;
            }

            /*Set return properties */
            selectedFlag = checked_item;
        }

        private string GetCheckedItem()
        {
            string checked_item = string.Empty;
            foreach (object itemChecked in cklFlag.CheckedItems)
            {
                checked_item = itemChecked.ToString().Substring(0, 1);
                break;
            }

            return checked_item;
        }

        private void cklFlag_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Checked)
            {
                for (int ix = 0; ix < cklFlag.Items.Count; ++ix)
                {
                    if (e.Index != ix)
                        cklFlag.SetItemChecked(ix, false);
                }
            }
        }
    }
}
