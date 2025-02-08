
/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmVarSelPopup.cs
Programmer    : Christine Zhang
Creation Date : Sept 23 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Select variable
Change Request: 
Specification : 
Rev History   : See Below

Other         : N/A
 ***********************************************************************/
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing.Printing;
using CprsDAL;

namespace Cprs
{
    public partial class frmVarSelPopup : Form
    {
        public string SelectedVar = "";
        public frmVarSelPopup()
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

        private void frmVarSelPopup_Load(object sender, System.EventArgs e)
        {
            InteractiveSearchData idata = new InteractiveSearchData();
            DataTable dt = idata.GetVariables();

            dgVar.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgVar.RowHeadersVisible = false; // set it to false if not needed
            dgVar.DataSource = dt;
            //dgVar.Columns[0].Width = 80;
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            string selected_value = dgVar.CurrentRow.Cells[0].FormattedValue.ToString();
            SelectedVar = selected_value;
        }

       
    }
}
