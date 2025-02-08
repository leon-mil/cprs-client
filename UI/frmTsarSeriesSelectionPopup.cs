
/**********************************************************************************
Econ App Name  : CPRS
Project Name   : CPRS Interactive Screens System
Program Name   : frmTsarSeriesSelectionPopup.cs	    	
Programmer     : Christine Zhang
Creation Date  : 5/7/2017
Inputs         : None                                   
Parameters     : None               
Outputs        : SerieName SerieTable
Description    : Displays selection screen for tsar tables
Detailed Design: CPRS II - View Tsar Series Design 
Other          : Called from: frmTsarSeriesViewer.cs
Revision       :	
***********************************************************************************
 Modified Date : 7/27/17 
 Modified By   : Christine Zhang 
 Keyword       :  
 Change Request: CR 220 - View TSAR series Names
 Description   : Allow series ending in YTD,CVY,CVX,CVM,SEM,SEP,SEY as valid names
***********************************************************************************
Modified Date  : 8/3/2017
Modified By    : Christine Zhang
Keyword        :
Change Request : CR 223 - View TSAR series Accesible series names
Description    : Allow series ending in CVS, CVR and CVG as valid names
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
    public partial class frmTsarSeriesSelectionPopup : Form
    {
       
        //public variables
        public string SeriesName = string.Empty;
        public string SeriesTable = string.Empty;

        //private variables
        private string stype = string.Empty;
        private string survey = string.Empty;
        private string category = string.Empty;
        private int level = 2;
        private TsarSeriesData tsdata;
        
        public frmTsarSeriesSelectionPopup()
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

        private void frmTsarSeriesSelectionPopup_Load(object sender, EventArgs e)
        {
            tsdata = new TsarSeriesData();

            groupBox3.Enabled = false;
            groupBox4.Enabled = false;
            groupBox5.Enabled = false;
            dgData.Visible = false;
            dgData.RowHeadersVisible = false;

        }

        
        //Initial find the table for series name
        private string FindSeriesTable(string stext)
        {
             string stable = string.Empty;

            string fe = stext.Substring(0, 1);
            string le = stext.Substring(5, 3);

            //build lists
            List<string> fl = new List<string>(new string[] { "F", "N", "V", "X", "S", "P", "T", "U"});
            List<string> el = new List<string>(new string[] { "SAA", "UNA", "SFC", "YTD", "CVX", "CVM", "CVY", "SEM", "SEP", "SEY", "CVR", "CVS", "CVG" });

            //check exist in lists 
            if (fl.Exists(x => string.Equals(x, fe, StringComparison.OrdinalIgnoreCase)) && el.Exists(x => string.Equals(x, le, StringComparison.OrdinalIgnoreCase)))
            {
                string ttype = string.Empty;

                if (le == "SFC")
                    stable = "TSARSFC";
                else if (le =="CVR" || le== "CVS" || le == "CVG")
                {
                    stable = "TSARCVS";
                }
                else
                {
                    if (fe == "F")
                        ttype = "FED";
                    else if (fe == "N" || fe == "V" || fe == "X")
                        ttype = "PRV";
                    else if (fe == "S")
                        ttype = "PUB";
                    else if (fe == "T" || fe == "P")
                        ttype = "TOT";
                    else
                        ttype = "UTL";

                    stable = le + ttype;
                } 
            }

            return stable;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void InitialGroup2()
        {
            //Initial group 2's radiobox
            rb2XX.Checked = false;
            rb2NR.Checked = false;
            rb201.Checked = false;
            rb202.Checked = false;
            rb203.Checked = false;
            rb204.Checked = false;
            rb205.Checked = false;
            rb206.Checked = false;
            rb207.Checked = false;
            rb208.Checked = false;
            rb209.Checked = false;
            rb210.Checked = false;
            rb211.Checked = false;
            rb212.Checked = false;
            rb213.Checked = false;
            rb214.Checked = false;
            rb215.Checked = false;
            rb216.Checked = false;
            rb219.Checked = false;
            rb200.Checked = false;
            rb220.Checked = false;
            
            category = string.Empty;
        }

        private void InitialGroup3()
        {
            //Initial group 3's radiobox
            rb3Adjusted.Checked = false;
            rb3Unadjusted.Checked = false;
            rb3Seasonal.Checked = false;
            groupBox5.Enabled = false;
            stype = string.Empty;
        }

        private void InitialGroup4()
        {
            //Initial group 4's radiobox
            rb42.Checked = false;
            rb43.Checked = false;
            rb44.Checked = false;
            dgData.Visible = false;
            level = 2;
        }


        //Check a radiobox in a group, uncheck rest of radiobox in the group
        private void RbgroupChecked(RadioButton rb, string group)
        {
            if (!rb.Checked) return;
            if (group == "1")
            {
                groupBox3.Enabled = true;
                InitialGroup2();
                InitialGroup3();
                InitialGroup4();
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
            }
            else if (group == "2")
            {
                category = rb.Text.Substring(0, 2);
                
                groupBox4.Enabled = true;
                InitialGroup3();
                InitialGroup4();
                if (survey == "T" || survey == "P" || survey == "U")
                    rb3Seasonal.Enabled = false;
                else
                    rb3Seasonal.Enabled = true;

                groupBox5.Enabled = false;
                
            }
            else if (group == "3")
            {
                InitialGroup4();
                
                if (survey != "T" && survey != "P")
                    groupBox5.Enabled = true;
                else
                    groupBox5.Enabled = false;
            }

            foreach (var c in this.Controls)
            {
                if (c is RadioButton && (c as RadioButton).Name.Substring(2, 1) == group && (c as RadioButton).Name != rb.Name)
                {
                    (c as RadioButton).Checked = false;
                }
            }
            
        }

        private void rb1Total_CheckedChanged(object sender, EventArgs e)
        {
            survey = "T";
            RbgroupChecked(rb1Total, "1");
        }
        private void rb1Private_CheckedChanged(object sender, EventArgs e)
        {
            survey = "V";
            RbgroupChecked(rb1Private, "1");
        }

        private void rb1Public_CheckedChanged(object sender, EventArgs e)
        {
            survey = "P";
            RbgroupChecked(rb1Public, "1");
        }

        private void rb1State_CheckedChanged(object sender, EventArgs e)
        {
            survey = "S";
            RbgroupChecked(rb1State, "1");
        }

        private void rb1Federal_CheckedChanged(object sender, EventArgs e)
        {
            survey = "F";
            RbgroupChecked(rb1Federal, "1");
        }
        private void rbUtilities_CheckedChanged(object sender, EventArgs e)
        {
            survey = "U";
            RbgroupChecked(rbUtilities, "1");
        }
        private void rb2XX_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb2XX, "2");
        }

        private void rb200_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb200, "2");
        }

        private void rb2NR_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb2NR, "2");
        }

        private void rb201_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb201, "2");
        }

        private void rb202_CheckedChanged(object sender, EventArgs e)
        { 
            RbgroupChecked(rb202, "2");
        }

        private void rb203_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb203, "2");
        }

        private void rb204_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb204, "2");
        }

        private void rb205_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb205, "2");
        }

        private void rb206_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb206, "2");
        }

        private void rb207_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb207, "2");
        }

        private void rb208_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb208, "2");
        }

        private void rb209_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb209, "2");
        }

        private void rb210_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb210, "2");
        }

        private void rb211_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb211, "2");
        }

        private void rb212_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb212, "2");
        }

        private void rb213_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb213, "2");
        }

        private void rb214_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb214, "2");
        }

        private void rb215_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb215, "2");
        }

        private void rb216_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb216, "2");
        }

        private void rb219_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb219, "2");
        }

        private void rb220_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb220, "2");
        }

        private void rb3Unadjusted_CheckedChanged(object sender, EventArgs e)
        {
            stype = "UNA";
            RbgroupChecked(rb3Unadjusted, "3");
            if (survey == "T" || survey == "P")
            {
                if (rb3Unadjusted.Checked)
                    LoadSeries();
            }
            else
                groupBox5.Enabled = true;
        }

        private void rb3Adjusted_CheckedChanged(object sender, EventArgs e)
        {
            stype = "SAA";
            RbgroupChecked(rb3Adjusted, "3");
            if (survey == "T" || survey == "P")
            {
                if (rb3Adjusted.Checked)
                    LoadSeries();
            }
            else
                groupBox5.Enabled = true;
        }

        private void rb3Seasonal_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb3Seasonal, "3");
            stype = "SFC";
            if (survey == "T" && survey == "P")
            {
                if (rb3Seasonal.Checked)
                    LoadSeries();
            }
            else
                groupBox5.Enabled = true;
            
        }

        private void rb42_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb42, "4");
            level = 2;
            if (rb42.Checked)
                LoadSeries();
        }

        private void rb43_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb43, "4");
            level = 3;
            if (rb43.Checked)
                LoadSeries();
        }

        private void rb44_CheckedChanged(object sender, EventArgs e)
        {
            RbgroupChecked(rb44, "4");
            level = 4;
            if (rb44.Checked)
                LoadSeries();
        }

        //find related Series Name
        private void LoadSeries()
        {
            if (survey != "" && category != "" && stype != "")
            {
                DataTable dt = tsdata.GetTsarSeries(survey, category, stype, level);
                dgData.DataSource = null;
                dgData.DataSource = dt;
                if (dt.Rows.Count > 0)
                {
                    dgData.Visible = true;
                }
                else
                {
                    MessageBox.Show("No Series Exists");
                    dgData.Visible = false;
                    if (groupBox5.Enabled)
                        InitialGroup4();
                }
            }
        }

        private void dgData_DoubleClick(object sender, EventArgs e)
        {
            if (dgData.RowCount == 0) return;

            SeriesName = dgData.CurrentRow.Cells[0].Value.ToString();
            SeriesTable = FindSeriesTable(SeriesName);
            
            this.Close();
        }

        private void txtSeries_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtSeries.TextLength == 8)
                {
                    string stable = FindSeriesTable(txtSeries.Text);
                    if (stable == "")
                    {
                        MessageBox.Show("The Series name is incorrect.");
                        txtSeries.Text = "";
                        txtSeries.Focus();
                     
                        return;
                    }
                    if (!tsdata.CheckSeriesExist(stable, txtSeries.Text))
                    {
                        MessageBox.Show("The Series name is incorrect.");
                        txtSeries.Text = "";
                        txtSeries.Focus();
                        return;
                    }
                    SeriesName = txtSeries.Text;
                    SeriesTable = stable;
                    
                    this.Close();
                }
                else
                {
                    MessageBox.Show("The Series name must be 8 characters.");
                    txtSeries.Text = "";
                    txtSeries.Focus();
                    return;
                }
            }
        }

    }

}
