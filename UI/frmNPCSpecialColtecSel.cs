
/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmNPCSpecialColtecSel.cs
Programmer    : Christine Zhang
Creation Date : Jan 21 2020
Parameters    : N/A
Inputs        : N/A
Outputs       : selectedColtec
Description   : select NPC Coltec 
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


namespace Cprs
{
    public partial class frmNPCSpecialColtecSel : Form
    {
        public frmNPCSpecialColtecSel()
        {
            InitializeComponent();
        }
        public string selectedColtec = "";

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
        private void frmNPCSpecialColtecSel_Load(object sender, EventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            string checked_items = "";

            if (checkedListBox1.CheckedItems.Count > 2)
            {
                MessageBox.Show("You only can select two coltecs");
                return;
            }

            // Next show the object title and check state for each item selected. 
            foreach (object itemChecked in checkedListBox1.CheckedItems)
            {
                if (checked_items == "")
                    checked_items = itemChecked.ToString().Substring(0, 1);
                else
                    checked_items = checked_items + "/" + itemChecked.ToString().Substring(0, 1);

            }

            selectedColtec = checked_items;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
