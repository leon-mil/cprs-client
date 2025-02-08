/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCehelp.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/20/2015
Inputs:             Help type
                    
Parameters:		    None                
Outputs:		    None

Description:	    This program displays the Cehelp data
                    in table

Detailed Design:    Detailed User Requirements for Improvement Screen 

Other:	            Called from: frmImprovements
 
Revision History:	
***********************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
***********************************************************************************/
using System;
using System.Linq;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;


namespace Cprs
{
    public partial class frmCehelpsPopup : Form
    {
        private int htype;

        public frmCehelpsPopup(int helpType)
        {
            InitializeComponent();
            htype = helpType;
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

        private void frmCehelps_Load(object sender, EventArgs e)
        {
            if (htype == 1)
                lblHelp.Text = "CECODE HELP";
            else if (htype == 2)
                lblHelp.Text = "APPLIANCE HELP";
            else if (htype == 3)
                lblHelp.Text = "STATE HELP";
            else
                lblHelp.Text = "JOB HELP";

            CehelpData chd = new CehelpData();

            dgHelp.DataSource = chd.GetCehelp(htype);
            dgHelp.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing;
            dgHelp.RowHeadersVisible = false; // set it to false if not needed

            /*Set up header and visible */
            for (int i = 0; i < dgHelp.ColumnCount; i++)
            {
                if (i == 0)
                {
                    dgHelp.Columns[i].HeaderText = "CODE";
                }
                if (htype != 4)
                {
                    if (i == 1)
                    {
                        dgHelp.Columns[i].HeaderText = "DESCRIPTION";
                        dgHelp.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        dgHelp.Columns[i].HeaderText = "JOB";
                    }
                    else if (i == 2)
                    {
                        dgHelp.Columns[i].HeaderText = "DESCRIPTION";
                        dgHelp.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                }

            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
