/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmC700flagSelPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      11/10/2015
Inputs:             Nvalue
                
Parameters:		    None                
Outputs:		    selectedFlag, setDefault

Description:	    This screen will allow the user to select default flag

Detailed Design:    Detailed User Requirements for C700 Screen 

Other:	            Called from: frmC700
 
Revision History:	
***********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
***********************************************************************************/
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
    public partial class frmC700flagSelPopup : Form
    {
        /*Public properties */
        public string selectedFlag;
        public bool setDefault = false;

        private int newValue = 0;

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

        public frmC700flagSelPopup(int nValue)
        {
            InitializeComponent();
            newValue = nValue;
        }

        private void frmC700flagSelPopup_Load(object sender, EventArgs e)
        {
            if (newValue == 0)
            {
                cklFlag.Items.Add("B - Blank");
                checkBox1.Checked = false;
            }
            else
            {
                checkBox1.Checked = true;
            }
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
            setDefault = checkBox1.Checked; 
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
                    if (e.Index != ix) cklFlag.SetItemChecked(ix, false);
                if (e.Index == 4)
                    checkBox1.Checked = false;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string checked_item = GetCheckedItem();
            if (checked_item == "B")
            {
                MessageBox.Show("Cannot set B as default flag");
                checkBox1.Checked = false;
                return;
            }
        }

    }
}
