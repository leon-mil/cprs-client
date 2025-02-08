/**************************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmMySectorsPopup.cs	    	
Programmer:         Christine Zhang
Creation Date:      12/05/2016
Inputs:             If edit, pass username              
Parameters:		    None 
Outputs:		     	
Description:	    This popup allows user to either view or add, edit, delete users from
                    HQSectors table

Detailed Design:    
Other:	            called from: frmMysectors.cs
Revision History:	
**************************************************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
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


namespace Cprs
{
    public partial class frmMySectorsPopup : Form
    {
        public frmMySectorsPopup()
        {
            InitializeComponent();
        }
        public string Username;

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

        private MySectorsData dataobject;
        private MySector ms;
        private bool addnew = false;
        private void frmMySectorsPopup_Load(object sender, EventArgs e)
        {
            dataobject = new MySectorsData();
            if (Username == "")
            {
                txtUser.Visible = false;
                cbUser.Visible = true;
                addnew = true;
                List<string> userlist = dataobject.GetHQSectorUsersAdd();
                if (userlist.Count == 0)
                {
                    MessageBox.Show("There is no users to add.");
                    this.Close();
                }
                cbUser.DataSource = userlist;
            }
            else
            {
                txtUser.Visible = true;
                cbUser.Visible = false;
                txtUser.Text = Username;
                
                ms = dataobject.GetMySectorData(Username);

                SetCkSecsItemValue(0, ms.Sect00);
                SetCkSecsItemValue(1, ms.Sect01);
                SetCkSecsItemValue(2, ms.Sect02);
                SetCkSecsItemValue(3, ms.Sect03);
                SetCkSecsItemValue(4, ms.Sect04);
                SetCkSecsItemValue(5, ms.Sect05);
                SetCkSecsItemValue(6, ms.Sect06);
                SetCkSecsItemValue(7, ms.Sect07);
                SetCkSecsItemValue(8, ms.Sect08);
                SetCkSecsItemValue(9, ms.Sect09);
                SetCkSecsItemValue(10, ms.Sect10);
                SetCkSecsItemValue(11, ms.Sect11);
                SetCkSecsItemValue(12, ms.Sect12);
                SetCkSecsItemValue(13, ms.Sect13);
                SetCkSecsItemValue(14, ms.Sect14);
                SetCkSecsItemValue(15, ms.Sect15);
                SetCkSecsItemValue(16, ms.Sect16);
                SetCkSecsItemValue(17, ms.Sect19);
                SetCkSecsItemValue(18, ms.Sect1T); 
            }
        }

        private void SetCkSecsItemValue(int index, string secVal)
        {
            if (secVal == "Y")
                ckSects.SetItemCheckState(index, CheckState.Checked);
            else
                ckSects.SetItemCheckState(index, CheckState.Unchecked);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //varify user name and at least one sector need checked
            if (addnew && cbUser.Text == "")
            {
                MessageBox.Show("There is no user selected, cannot save.");
                return;
            }

            bool chk = false;
            for (int i = 0; i <= (ckSects.Items.Count - 1); i++)
            {
                if (ckSects.GetItemChecked(i))
                {
                    chk = true;
                    break;
                }     
            }
            if (!chk)
            {
                MessageBox.Show("There is no sector selected, cannot save.");
                return;
            }

            if (addnew )
                ms = new MySector(cbUser.Text);
            
            //Read check boxes
            for (int i = 0; i <= (ckSects.Items.Count - 1); i++)
            {
                bool cked = ckSects.GetItemChecked(i);
                if (i==0)
                    ms.Sect00 = cked ? "Y" : "N";
                else if (i == 1)
                    ms.Sect01 = cked ? "Y" : "N";
                else if (i==2)
                    ms.Sect02 = cked ? "Y" : "N";
                else if (i == 3)
                    ms.Sect03 = cked ? "Y" : "N";
                else if (i == 4)
                    ms.Sect04 = cked ? "Y" : "N";
                else if (i == 5)
                    ms.Sect05 = cked ? "Y" : "N";
                else if (i == 6)
                    ms.Sect06 = cked ? "Y" : "N";
                else if (i == 7)
                    ms.Sect07 = cked ? "Y" : "N";
                else if (i == 8)
                    ms.Sect08 = cked ? "Y" : "N";
                else if (i == 9)
                    ms.Sect09 = cked ? "Y" : "N";
                else if (i == 10)
                    ms.Sect10 = cked ? "Y" : "N";
                else if (i == 11)
                    ms.Sect11 = cked ? "Y" : "N";
                else if (i == 12)
                    ms.Sect12 = cked ? "Y" : "N";
                else if (i == 13)
                    ms.Sect13 = cked ? "Y" : "N";
                else if (i == 14)
                    ms.Sect14 = cked ? "Y" : "N";
                else if (i == 15)
                    ms.Sect15 = cked ? "Y" : "N";
                else if (i == 16)
                    ms.Sect16 = cked ? "Y" : "N";
                else if (i == 17)
                    ms.Sect19 = cked ? "Y" : "N";
                else if (i == 18)
                    ms.Sect1T = cked ? "Y" : "N";
               
            } 
 
            //Save data
            if (!addnew)
                dataobject.UpdateMySectorData(ms);
            else
                dataobject.AddMySectorData(ms);

            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
