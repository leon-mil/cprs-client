/**************************************************************************************************
Econ App Name   : CPRS
Project Name    : CPRS Interactive Screens System
Program Name    : frmVip1Unit.cs	    	
Programmer      : Christine Zhang   
Creation Date   : July 11 2024
Inputs          : None                 
Parameters      : None 
Outputs         : None	
Description     : Display vip 1 unit's average cost

Detailed Design : Detailed Design for Vip 1 unit
Other           :	            
Revision History:	
**************************************************************************************************
Modified Date   :  
Modified By     :  
Keyword         :  
Change Request  :  
Description     :  
**************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsDAL;
using CprsBLL;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Cprs
{
    public partial class frmVip1Unit : frmCprsParent
    {
        public frmVip1Unit()
        {
            InitializeComponent();
        }

        private void frmVip1Unit_Load(object sender, EventArgs e)
        {
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("TABULATIONS");

            //Get data from vip 1unit table
            Vip1UnitData gdata = new Vip1UnitData();
            DataTable table = gdata.GetVip1UnitData();
            dgData.DataSource = table;

            dgData.Columns[0].HeaderText = "Date6";
            dgData.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dgData.Columns[0].Width = 100;

            dgData.Columns[1].HeaderText = "Average Cost";
            dgData.Columns[1].DefaultCellStyle.Format = "N0";
            dgData.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
         
            dgData.Columns[2].HeaderText = "Average Cost - For Sale";
            dgData.Columns[2].DefaultCellStyle.Format = "N0";
            dgData.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
       
            dgData.Columns[3].HeaderText = "Average Cost - Contractor";
            dgData.Columns[3].DefaultCellStyle.Format = "N0";
            dgData.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
       
            dgData.Columns[4].HeaderText = "Total Starts";
            dgData.Columns[4].DefaultCellStyle.Format = "N0";
            dgData.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
         
            dgData.Columns[5].HeaderText = "Other Starts";
            dgData.Columns[5].DefaultCellStyle.Format = "N0";
            dgData.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            
            dgData.Columns[6].HeaderText = "Owner Starts";
            dgData.Columns[6].DefaultCellStyle.Format = "N0";
            dgData.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
         

        }

        private void frmVip1Unit_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("TABULATIONS", "EXIT");
        }
    }
}
