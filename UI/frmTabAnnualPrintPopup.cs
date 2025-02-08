
/**********************************************************************************
Econ App Name  : CPRS
Project Name   : CPRS Interactive Screens System
Program Name   : frmTabAnnualPrintPopup.cs	    	
Programmer     : Diane Musachio
Creation Date  : 6/24/2019
Inputs         :                                 
Parameters     :             
Outputs        : yearPrint
Description    : Displays selection options for printing
Detailed Design: Annual Tabulations Detailed Design
Other          : Called from: frmTabAnnualTabs.cs
Revision       :	
***********************************************************************************
Modified Date  : 
Modified By    : 
Keyword        :
Change Request : 
Description    :
**********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;

namespace Cprs
{
    public partial class frmTabAnnualPrintPopup : Form
    {
        //Public variables
        public string yearPrint;
        TabAnnualTabsData data_object = new TabAnnualTabsData();
        private string selected;

        public frmTabAnnualPrintPopup()
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

        private void frmTabAnnualPrintPopup_Load(object sender, EventArgs e)
        {
            //set radio button and trigger eventhandler to resolve selected initially
            rdYear1.Checked = true;
            rdYear1.Click += new EventHandler(radioButtons_CheckedChanged);

            //calculate date from current survey month
            string sdate = data_object.GetCurrMonthDateinTable();
            string year_survey = sdate.ToString().Substring(0, 4);
            rdYear1.Text = (Convert.ToInt16(year_survey) - 1).ToString();
            rdYear2.Text = (Convert.ToInt16(year_survey) - 2).ToString();
            rdYear3.Text = (Convert.ToInt16(year_survey) - 3).ToString();
            rdYear4.Text = (Convert.ToInt16(year_survey) - 4).ToString();
            rdYear5.Text = (Convert.ToInt16(year_survey) - 5).ToString();
            
            rdAllYrs.Text = "All Five Years";
        }

        //Check a radiobox in a group, uncheck rest of radiobox in the group
        //pass text of button as parameter for printing
        private void radioButtons_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            {
                selected = radioButton.Text;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            yearPrint = selected;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    } 
}
