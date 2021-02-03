/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       NPCActiveLoad.cs	    	

Programmer:         Srini Natarajan
Creation Date:      10/18/2015

Inputs:             None
Parameters:	        None 

Outputs:	        Active work load 	
Description:	    This screen tabulates data from Sched_call, Accesscde, Vipnme for current month. 

Detailed Design:    None 
Other:	            Called by: Main form
 
Revision History:	
****************************************************************************************
 Modified Date :  October 23, 2017
 Modified By   :  Kevin J Montgomery
 Keyword       :  
 Change Request:  
 Description   :  Updated  Vip Satisfied  to  Callstat = V
                  Updated Centurion Accescde from W to C
****************************************************************************************/

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
using System.Drawing.Printing;
using System.IO;
using System.Collections;
using DGVPrinterHelper;
using System.Threading;

namespace Cprs
{
    public partial class frmNPCActiveLoad : frmCprsParent
    {
        private NPCActiveLoadData dataObject;
        string CurrMonth = string.Empty;
        string currYearMon1 = string.Empty;
        string currYearMon2 = string.Empty;
        string currYearMon3 = string.Empty;
        string currYearMon4 = string.Empty;
        string currYearMon5 = string.Empty;
        string currYearMon6 = string.Empty;
        string currYearMon7 = string.Empty; 
        string currYearMon8 = string.Empty;
        string currYearMon9 = string.Empty;
        string currYearMon10 = string.Empty;
        string currYearMon11 = string.Empty;
        string currYearMon12 = string.Empty;
        string added = string.Empty;
        string ownerVal = string.Empty;
        public string Id;
        public List<string> Idlist = null;
        public int CurrIndex = 0;

        DataGridView dgNum = new DataGridView();
        DataGridView dgBotm = new DataGridView();
        Label lblCases;
        string coltecVal = string.Empty;

        private bool form_loading = false;
        
        public frmNPCActiveLoad()
        {
            InitializeComponent();
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");
        }

        private void frmNPCActiveLoad_Load(object sender, EventArgs e)
        {
            FormLoad();
        }

        private void FormLoad()
        {
             form_loading = true;

            dataObject = new NPCActiveLoadData();

            //create instance of data object
            GetMonthsList();
            List<string> dy = new List<string>();
            dy.Add(CurrMonth);
            dy.Add(currYearMon1);
            dy.Add(currYearMon2);
            dy.Add(currYearMon3);
            dy.Add(currYearMon4);
            dy.Add(currYearMon5);
            dy.Add(currYearMon6);
            dy.Add(currYearMon7);
            dy.Add(currYearMon8);
            dy.Add(currYearMon9);
            dy.Add(currYearMon10);
            dy.Add(currYearMon11);
            dy.Add(currYearMon12);

            cbStatp.DataSource = dy;
            cbStatp.SelectedIndex = 0;
            
            tabs.SelectedIndex = 0;
            form_loading = false;

            coltecVal = "F"; 
            dgt1.DataSource = dataObject.GetFirstMonthData(coltecVal);
            setItemColumnHeader(dgt1);
            rd1a.Checked = true;

            SetButtonTxt();
        }

        private void GetMonthsList()
        {
            DateTime today = DateTime.Now;
            //current month
            CurrMonth = (GeneralFunctions.CurrentYearMon());
            //get the last 12 months 
            DateTime.Now.ToString("yyyyMM");
            currYearMon1 = DateTime.Now.AddMonths(-1).ToString("yyyyMM");
            currYearMon2 = DateTime.Now.AddMonths(-2).ToString("yyyyMM");
            currYearMon3 = DateTime.Now.AddMonths(-3).ToString("yyyyMM");
            currYearMon4 = DateTime.Now.AddMonths(-4).ToString("yyyyMM");
            currYearMon5 = DateTime.Now.AddMonths(-5).ToString("yyyyMM");
            currYearMon6 = DateTime.Now.AddMonths(-6).ToString("yyyyMM");
            currYearMon7 = DateTime.Now.AddMonths(-7).ToString("yyyyMM");
            currYearMon8 = DateTime.Now.AddMonths(-8).ToString("yyyyMM");
            currYearMon9 = DateTime.Now.AddMonths(-9).ToString("yyyyMM");
            currYearMon10 = DateTime.Now.AddMonths(-10).ToString("yyyyMM");
            currYearMon11 = DateTime.Now.AddMonths(-11).ToString("yyyyMM");
            currYearMon12 = DateTime.Now.AddMonths(-12).ToString("yyyyMM");
        }

        private void ChkOwnerValColF()
        {
            if (rd1a.Checked == true)       //All surveys
            { ownerVal = ""; }
            if (rd1p.Checked == true)       //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rd1f.Checked == true)       //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rd1n.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rd1m.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rd1u.Checked == true)       //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void ChkOwnerValColC()
        {
            if (rdAllColC.Checked == true)       //All surveys
            { ownerVal = ""; }
            if (rdPubColC.Checked == true)       //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rdFedColC.Checked == true)       //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rdNonRColC.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rdMultFColC.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rdUtilColC.Checked == true)       //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void ChkOwnerValColP()
        {
            if (rdAllColP.Checked == true)          //All surveys
            { ownerVal = ""; }
            if (rdPubColP.Checked == true)          //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rdFedColP.Checked == true)          //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rdNonRColP.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rdMultFColP.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rdUtilColP.Checked == true)         //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void ChkOwnerValColI()
        {
            if (rdAllColI.Checked == true)          //All surveys
            { ownerVal = ""; }
            if (rdPubColI.Checked == true)          //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rdFedColI.Checked == true)          //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rdNonRColI.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rdMultFColI.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rdUtilColI.Checked == true)         //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void ChkOwnerValColS()
        {
            if (rdAllColS.Checked == true)          //All surveys
            { ownerVal = ""; }
            if (rdPubColS.Checked == true)          //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rdFedColS.Checked == true)          //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rdNonRColS.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rdMultFColS.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rdUtilColS.Checked == true)         //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void ChkOwnerValColA()
        {
            if (rdAllColA.Checked == true)          //All surveys
            { ownerVal = ""; }
            if (rdPubColA.Checked == true)          //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rdFedColA.Checked == true)          //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rdNonRColA.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rdMultFColA.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rdUtilColA.Checked == true)         //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void ChkOwnerValColCA()
        {
            if (rdAllColCA.Checked == true)          //All surveys
            { ownerVal = ""; }
            if (rdPubColCA.Checked == true)          //State & Local
            { ownerVal = " and OWNER IN ('S', 'L', 'P')"; }
            if (rdFedColCA.Checked == true)          //Federal
            { ownerVal = " and OWNER IN ('C', 'D', 'F')"; }
            if (rdNonRColCA.Checked == true)        //Non Residential
            { ownerVal = " and OWNER = 'N'"; }
            if (rdMultFColCA.Checked == true)        //Multi family
            { ownerVal = " and OWNER = 'M'"; }
            if (rdUtilColCA.Checked == true)         //Utilities
            { ownerVal = " and OWNER IN ('T', 'G', 'E', 'O', 'W', 'R')"; }
        }

        private void populateTheLowerGrid(TabPage pgNam)
        {
            string swhere = "";
            ownerVal = "";
            selectTabPage(tabs.SelectedTab);

            //check if a row is selected before populating the lower grid.
            if (dgNum.SelectedRows.Count > 0)
            {
                int selectedrowindex = dgNum.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dgNum.Rows[selectedrowindex];
                string selRowshow = selectedrowindex.ToString();
                
                if (cbStatp.Text == CurrMonth)
                {
                    if (pgNam == tabPage7)
                    {
                        swhere = "ADDED = 'Y'" + ownerVal;
                    }
                    else
                    {
                        swhere = "sc.coltec = '" + coltecVal + "' and added = 'N'" + ownerVal;
                    }
                    if (selectedrowindex == 0)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 1)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where sc.callstat <> 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 2)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where ACCESCDE = 'F' and sc.callstat = 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 3)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where ACCESCDE = 'C' and sc.callstat = 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 4)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where ACCESCDE = 'P' and callstat = 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 5)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where ACCESCDE = 'I' and callstat = 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 6)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where ACCESCDE = 'S' and callstat = 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }
                    else if (selectedrowindex == 7)
                    {
                        dgBotm.DataSource = dataObject.getLwrGrd("where ACCESCDE = 'A' and callstat = 'V' and " + swhere);
                        int rowCount = dgBotm.RowCount;
                        lblCases.Text = rowCount.ToString() + " CASES";
                    }

                    //set column alignment
                    foreach (DataGridViewColumn column in dgBotm.Columns)
                    {
                        if ( column.Name == "RVITM5C")
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        else
                            column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    }
                   
                    dgBotm.Columns["ACCESCDE"].Visible = false;
                    dgBotm.Columns["CALLSTAT"].Visible = false;

                    if (pgNam == tabPage1)
                        { RadBtnsEnableTab1(); }
                    if (pgNam == tabPage2)
                        { RadBtnsEnableTab2(); }
                    if (pgNam == tabPage3)
                        { RadBtnsEnableTab3(); }
                    if (pgNam == tabPage4)
                        { RadBtnsEnableTab4(); }
                    if (pgNam == tabPage5)
                        { RadBtnsEnableTab5(); }
                    if (pgNam == tabPage6)
                        { RadBtnsEnableTab6(); }
                    if (pgNam == tabPage7)
                        { RadBtnsEnableTab7(); }
                }
                else
                {
                    if (pgNam == tabPage1)
                        { RadBtnDisableTab1(); }
                    if (pgNam == tabPage2)
                        { RabBtnsDisableTab2(); }
                    if (pgNam == tabPage3)
                        { RadBtnsDisableTab3(); }
                    if (pgNam == tabPage4)
                        { RadBtnsDisableTab4();}
                    if (pgNam == tabPage5)
                        { RadBtnsDisableTab5(); }
                    if (pgNam == tabPage6)
                        { RadBtnsDisableTab6(); }
                    if (pgNam == tabPage7)
                        { RadBtnsDisableTab7(); }
                }
            }
        }

        private void RadBtnsEnableTab1()
        {
            lblSelSurvey.Enabled = true;
            rd1a.Enabled = true;
            rd1f.Enabled = true;
            rd1m.Enabled = true;
            rd1n.Enabled = true;
            rd1p.Enabled = true;
            rd1u.Enabled = true;
        }

        private void RadBtnDisableTab1()
        {
            label10.Text = "0 CASES";
            dgb1.DataSource = null;
            lblSelSurvey.Enabled = false;
            rd1a.Enabled = false;
            rd1f.Enabled = false;
            rd1m.Enabled = false;
            rd1n.Enabled = false;
            rd1p.Enabled = false;
            rd1u.Enabled = false;
        }

        private void RadBtnsEnableTab2()
        {
            lblSurveyColC.Enabled = true;
            rdAllColC.Enabled = true;
            rdPubColC.Enabled = true;
            rdNonRColC.Enabled = true;
            rdFedColC.Enabled = true;
            rdMultFColC.Enabled = true;
            rdMultFColC.Enabled = true;
        }

        private void RabBtnsDisableTab2()
        {
            dgb2.DataSource = null;
            label11.Text = "0 CASES";
            lblSurveyColC.Enabled = false;
            rdAllColC.Enabled = false;
            rdPubColC.Enabled = false;
            rdNonRColC.Enabled = false;
            rdFedColC.Enabled = false;
            rdMultFColC.Enabled = false;
            rdUtilColC.Enabled = false;
        }

        private void RadBtnsEnableTab3()
        {
            lblSurveyColP.Enabled = true;
            rdAllColP.Enabled = true;
            rdPubColP.Enabled = true;
            rdNonRColP.Enabled = true;
            rdFedColP.Enabled = true;
            rdMultFColP.Enabled = true;
            rdUtilColP.Enabled = true;
        }

        private void RadBtnsDisableTab3()
        {
            dgb3.DataSource = null;
            label12.Text = " 0 CASES";
            lblSurveyColP.Enabled = false;
            rdAllColP.Enabled = false;
            rdPubColP.Enabled = false;
            rdNonRColP.Enabled = false;
            rdFedColP.Enabled = false;
            rdMultFColP.Enabled = false;
            rdUtilColP.Enabled = false;
        }

        private void RadBtnsEnableTab4()
        {
            lblSurveyColI.Enabled = true;
            rdAllColI.Enabled = true;
            rdPubColI.Enabled = true;
            rdNonRColI.Enabled = true;
            rdFedColI.Enabled = true;
            rdMultFColI.Enabled = true;
            rdUtilColI.Enabled = true;
        }

        private void RadBtnsDisableTab4()
        {
            dgb4.DataSource = null;
            label13.Text = "0 CASES";
            lblSurveyColI.Enabled = false;
            rdAllColI.Enabled = false;
            rdPubColI.Enabled = false;
            rdNonRColI.Enabled = false;
            rdFedColI.Enabled = false;
            rdMultFColI.Enabled = false;
            rdUtilColI.Enabled = false;
        }

        private void RadBtnsEnableTab5()
        {
            lblSurveyColS.Enabled = true;
            rdAllColS.Enabled = true;
            rdPubColS.Enabled = true;
            rdNonRColS.Enabled = true;
            rdFedColS.Enabled = true;
            rdMultFColS.Enabled = true;
            rdUtilColS.Enabled = true;
        }

        private void RadBtnsDisableTab5()
        {
            dgb5.DataSource = null;
            label14.Text = "0 CASES";
            lblSurveyColS.Enabled = false;
            rdAllColS.Enabled = false;
            rdPubColS.Enabled = false;
            rdNonRColS.Enabled = false;
            rdFedColS.Enabled = false;
            rdMultFColS.Enabled = false;
            rdUtilColS.Enabled = false;
        }

        private void RadBtnsEnableTab6()
        {
            lblSurveyColA.Enabled = true;
            rdAllColA.Enabled = true;
            rdPubColA.Enabled = true;
            rdNonRColA.Enabled = true;
            rdFedColA.Enabled = true;
            rdMultFColA.Enabled = true;
            rdUtilColA.Enabled = true;
        }

        private void RadBtnsDisableTab6()
        {
            dgb6.DataSource = null;
            label15.Text = "0 CASES";
            lblSurveyColA.Enabled = false;
            rdAllColA.Enabled = false;
            rdPubColA.Enabled = false;
            rdNonRColA.Enabled = false;
            rdFedColA.Enabled = false;
            rdMultFColA.Enabled = false;
            rdUtilColA.Enabled = false;
        }

        private void RadBtnsEnableTab7()
        {
            lblSurveyColCA.Enabled = true;
            rdAllColCA.Enabled = true;
            rdPubColCA.Enabled = true;
            rdNonRColCA.Enabled = true;
            rdFedColCA.Enabled = true;
            rdMultFColCA.Enabled = true;
            rdUtilColCA.Enabled = true;
        }

        private void RadBtnsDisableTab7()
        {
            dgb7.DataSource = null;
            label16.Text = "0 CASES";
            lblSurveyColCA.Enabled = false;
            rdAllColCA.Enabled = false;
            rdPubColCA.Enabled = false;
            rdNonRColCA.Enabled = false;
            rdFedColCA.Enabled = false;
            rdMultFColCA.Enabled = false;
            rdUtilColCA.Enabled = false;
        }

        private void UpdateFirstColumn(DataGridView dataGridNum)
        {
            dataGridNum.Rows[0].Cells[0].Value = "TOTAL";
            dataGridNum.Rows[1].Cells[0].Value = "NOT VIP SATISFIED";
            dataGridNum.Rows[2].Cells[0].Value = "COLLECTED via FORM";
            dataGridNum.Rows[3].Cells[0].Value = "COLLECTED via CENTURION";
            dataGridNum.Rows[4].Cells[0].Value = "COLLECTED via PHONE/TFU";
            dataGridNum.Rows[5].Cells[0].Value = "COLLECTED via INTERNET";
            dataGridNum.Rows[6].Cells[0].Value = "COLLECTED via SPECIAL";
            dataGridNum.Rows[7].Cells[0].Value = "COLLECTED via ADMINISTRATIVE";
        }

        private void getColtecGrid(int Selindex, string coltecVal, DataGridView dataGridNumb)
        {
            if (tabs.SelectedIndex == Selindex)
            {
                //If selection equal to current month
                if (cbStatp.Text == CurrMonth)
                {
                    //Call the stored procedure
                    dataGridNumb.DataSource = dataObject.GetFirstMonthData(coltecVal);
                }
                else
                {
                    dataGridNumb.DataSource = dataObject.GetColtecForm(coltecVal, cbStatp.Text);
                }

                if (dataGridNumb.RowCount != 0)
                {
                    UpdateFirstColumn(dataGridNumb);
                    dataGridNumb.RowHeadersVisible = false;
                }
            }
            foreach (DataGridViewColumn dgvc in dataGridNumb.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
                if (dgvc.Index != 0)
                {
                    dgvc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    dgvc.DefaultCellStyle.Format = "N0";
                }

            }

        }

        private void setItemColumnHeader(DataGridView dataGridNum)
        {
            dataGridNum.Columns[0].HeaderText = "                                      ";
            dataGridNum.Columns[0].Width = 200;
            dataGridNum.Columns[1].HeaderText = "ALL SURVEYS";
            dataGridNum.Columns[2].HeaderText = "STATE & LOCAL";
            dataGridNum.Columns[3].HeaderText = "FEDERAL";
            dataGridNum.Columns[4].HeaderText = "NONRESIDENTIAL";
            dataGridNum.Columns[5].HeaderText = "MULTIFAMILY";
            dataGridNum.Columns[6].HeaderText = "UTILITIES";
        }

        //Set the tab control names based on tab page selection.
        private void selectTabPage(TabPage pageName )
        {
            
            if (pageName == tabPage1)
            {
                dgNum = dgt1;
                dgBotm = dgb1;
                lblCases = label10;
                coltecVal = "F";
                ChkOwnerValColF();
            }
            if (pageName == tabPage2)
            {
                dgNum = dgt2;
                dgBotm = dgb2;
                lblCases = label11;
                coltecVal = "C";
                ChkOwnerValColC();
            }
            if (pageName == tabPage3)
            {
                dgNum = dgt3;
                dgBotm = dgb3;
                lblCases = label12;
                coltecVal = "P";
                ChkOwnerValColP();
            }
            if (pageName == tabPage4)
            {
                dgNum = dgt4;
                dgBotm = dgb4;
                lblCases = label13;
                coltecVal = "I";
                ChkOwnerValColI();
            }
            if (pageName == tabPage5)
            {
                dgNum = dgt5;
                dgBotm = dgb5;
                lblCases = label14;
                coltecVal = "S";
                ChkOwnerValColS();
            }
            if (pageName == tabPage6)
            {
                dgNum = dgt6;
                dgBotm = dgb6;
                lblCases = label15;
                coltecVal = "A";
                ChkOwnerValColA();
            }
            if (pageName == tabPage7)
            {
                dgNum = dgt7;
                dgBotm = dgb7;
                lblCases = label16;
                coltecVal = null;
                ChkOwnerValColCA();
            }
        }

        private void setColumnHeaders()
        {
            if (tabs.SelectedTab == tabPage1)
            {
                getColtecGrid(0, "F", dgt1);
                setItemColumnHeader(dgt1);
                rd1a.Checked = true;
            }
            else if (tabs.SelectedTab == tabPage2)
            {
                getColtecGrid(1, "C", dgt2);
                setItemColumnHeader(dgt2);
                rdAllColC.Checked = true;
            }
            else if (tabs.SelectedTab == tabPage3)
            {
                getColtecGrid(2, "P", dgt3);
                setItemColumnHeader(dgt3);
                rdAllColP.Checked = true;
            }
            else if (tabs.SelectedTab == tabPage4)
            {
                getColtecGrid(3, "I", dgt4);
                setItemColumnHeader(dgt4);
                rdAllColI.Checked = true;
            }
            else if (tabs.SelectedTab == tabPage5)
            {
                getColtecGrid(4, "S", dgt5);
                setItemColumnHeader(dgt5);
                rdAllColS.Checked = true;
            }
            else if (tabs.SelectedTab == tabPage6)
            {
                getColtecGrid(5, "A", dgt6);
                setItemColumnHeader(dgt6);
                rdAllColA.Checked = true;
            }
            else if (tabs.SelectedTab == tabPage7)
            {
                getColtecGrid(6, "X", dgt7);
                setItemColumnHeader(dgt7);
                rdAllColCA.Checked = true;
            }        
        }
        
        private void tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loading) return;
            this.Cursor = Cursors.WaitCursor;
            
            setColumnHeaders();
            if (cbStatp.Text == CurrMonth)
            {
                populateTheLowerGrid(tabs.SelectedTab);
            }

            this.Cursor = Cursors.Default;
        }

        private void dgt1_SelectionChanged(object sender, EventArgs e)
        {
            if (form_loading) return;
            populateTheLowerGrid(tabs.SelectedTab);
            rd1a.Checked = true;
        }

        private void dgt2_SelectionChanged(object sender, EventArgs e)
        {
            //populateTheLowerGrid(tabs.SelectedTab);
            rdAllColC.Checked = false;
            rdAllColC.Checked = true;
        }

        private void dgt3_SelectionChanged(object sender, EventArgs e)
        {
            // populateTheLowerGrid(tabs.SelectedTab);
            rdAllColP.Checked = false;
            rdAllColP.Checked = true;
        }

        private void dgt4_SelectionChanged(object sender, EventArgs e)
        {
            //populateTheLowerGrid(tabs.SelectedTab);
            rdAllColI.Checked = false;
            rdAllColI.Checked = true;
        }
        
        private void dgt5_SelectionChanged_1(object sender, EventArgs e)
        {
            // populateTheLowerGrid(tabs.SelectedTab);
            rdAllColS.Checked = false;
            rdAllColS.Checked = true;
        }

        private void dgt6_SelectionChanged(object sender, EventArgs e)
        {
            // populateTheLowerGrid(tabs.SelectedTab);
            rdAllColA.Checked = false;
            rdAllColA.Checked = true;
        }

        private void dgt7_SelectionChanged(object sender, EventArgs e)
        {
            // populateTheLowerGrid(tabs.SelectedTab);
            rdAllColCA.Checked = false;
            rdAllColCA.Checked = true;
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            TabPage pageSel;
            pageSel = tabs.SelectedTab;
            selectTabPage(pageSel);
                if (lblCases.Text.Trim() == "0 CASES")
                {
                    MessageBox.Show("The Results list is empty. No Record Selected.");
                }
                else
                {
                    this.Hide(); // hide parent
                    DataGridViewSelectedRowCollection rows = dgBotm.SelectedRows;
                    int index = dgBotm.CurrentRow.Index;
                    string val1 = dgBotm["ID", index].Value.ToString();
                    //Store Id in list for Page Up and Page Down
                    List<string> Idlist = new List<string>();
                    int cnt = 0;
                    foreach (DataGridViewRow dr in dgBotm.Rows)
                    {
                        string val = dgBotm["ID", cnt].Value.ToString();
                        Idlist.Add(val);
                        cnt = cnt + 1;
                    }
                    frmName fName = new frmName();
                    fName.Id = val1;
                    fName.Idlist = Idlist;
                    fName.CurrIndex = index;
                    fName.CallingForm = this;
                    GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
                    fName.ShowDialog(); // show child    
                    GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
                    populateTheLowerGrid(tabs.SelectedTab);
                }
        }

        private void btnC700_Click(object sender, EventArgs e)
        {
            TabPage pageSel;
            pageSel = tabs.SelectedTab;
            selectTabPage(pageSel);
            if (lblCases.Text.Trim() == "0 CASES")
            {
                MessageBox.Show("The Results list is empty. No Record Selected.");
                return;
            }

            this.Hide(); // hide parent
            DataGridViewSelectedRowCollection rows = dgBotm.SelectedRows;
            int index = dgBotm.CurrentRow.Index;

            //Display TFU on the C-700 button for NPC users
            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                string resp = dgBotm["RESPID", index].Value.ToString();
                frmTfu tfu = new frmTfu();

                tfu.RespId = resp;
                tfu.CallingForm = this;

                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");

                tfu.ShowDialog();   // show child
            }
            else
            {
                string val1 = dgBotm["ID", index].Value.ToString();
                // Store Id in list for Page Up and Page Down
                List<string> Idlist = new List<string>();
                int cnt = 0;
                foreach (DataGridViewRow dr in dgBotm.Rows)
                {
                    string val = dgBotm["ID", cnt].Value.ToString();
                    Idlist.Add(val);
                    cnt = cnt + 1;
                }

                frmC700 fC700 = new frmC700();
                fC700.Id = val1;
                fC700.Idlist = Idlist;
                fC700.CurrIndex = index;
                fC700.CallingForm = this;
                GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
                fC700.ShowDialog(); // show child    
                   
            }
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            populateTheLowerGrid(tabs.SelectedTab);
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData("F");
            PrintData("C");
            PrintData("P");
            PrintData("I");
            PrintData("S");
            PrintData("A");
            PrintData("X");   
        }

        //Print multiple datagrids based on coltec criteria
        private void PrintData(string coltecVal)
        {
            Cursor.Current = Cursors.WaitCursor;
            dgPrint.DataSource = null;
            if (cbStatp.Text == CurrMonth)
                dgPrint.DataSource = dataObject.GetFirstMonthData(coltecVal);
            else
            {
                dgPrint.DataSource = dataObject.GetColtecForm(coltecVal, cbStatp.Text);
                UpdateFirstColumn(dgPrint);
            }
            setItemColumnHeader(dgPrint);
      
            DGVPrinter printer = new DGVPrinter();
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.SubTitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.SubTitleAlignment = StringAlignment.Center;

            string coltecLet;
            if (coltecVal == "X")
            { coltecLet = "Case Added"; }
            else
            { coltecLet = coltecVal; }
            printer.Title = "NPC Active Workload Report: Coltec " + coltecLet;
            printer.SubTitle = cbStatp.Text;
            printer.PageSettings.Landscape = true;
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;
            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "NPC Active Workload Report: Coltec " + coltecLet;
            printer.Footer = " ";
            printer.PrintDataGridViewWithoutDialog(dgPrint);
            
            Cursor.Current = Cursors.Default;
        }

        private void cbStatp_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (form_loading) return;

            if (tabs.SelectedIndex !=0)
                tabs.SelectedIndex = 0;
            else
                setColumnHeaders();

            if (cbStatp.Text == CurrMonth)
            {
                btnC700.Enabled = true;
                btnName.Enabled = true;
            }
            else
            {
                btnC700.Enabled = false;
                btnName.Enabled = false;
            }
        }

       
        private void rd1a_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1a.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rd1p_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1p.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rd1f_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1f.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rd1n_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1n.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rd1m_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1m.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rd1u_CheckedChanged(object sender, EventArgs e)
        {
            if (rd1u.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdAllColC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllColC.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdPubColC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPubColC.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdNonRColC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNonRColC.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdFedColC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFedColC.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdMultFColC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMultFColC.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdUtilColC_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUtilColC.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdAllColP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllColP.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdPubColP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPubColP.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdNonRColP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNonRColP.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdFedColP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFedColP.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdMultFColP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMultFColP.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdUtilColP_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUtilColP.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdAllColI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllColI.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdPubColI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPubColI.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdNonRColI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNonRColI.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdFedColI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFedColI.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdMultFColI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMultFColI.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdUtilColI_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUtilColI.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdAllColS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllColS.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdPubColS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPubColS.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdNonRColS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNonRColS.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdFedColS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFedColS.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdMultFColS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMultFColS.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdUtilColS_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUtilColS.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdAllColA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllColA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdPubColA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPubColA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdNonRColA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNonRColA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdFedColA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFedColA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdMultFColA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMultFColA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdUtilColA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUtilColA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdAllColCA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdAllColCA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdPubColCA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdPubColCA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdNonRColCA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdNonRColCA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdFedColCA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdFedColCA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdMultFColCA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdMultFColCA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void rdUtilColCA_CheckedChanged(object sender, EventArgs e)
        {
            if (rdUtilColCA.Checked)
                populateTheLowerGrid(tabs.SelectedTab);
        }

        private void frmNPCActiveLoad_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void SetButtonTxt()
        {
            UserInfoData data_object = new UserInfoData();

            switch (UserInfo.GroupCode)
            {
                case EnumGroups.Programmer:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQManager:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQAnalyst:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.NPCManager:
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.NPCLead:
                    btnC700.Text = "TFU";
                    break;
                case EnumGroups.HQSupport:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQMathStat:
                    btnC700.Text = "C-700";
                    break;
                case EnumGroups.HQTester:
                    btnC700.Text = "C-700";
                    break;
            }
        }

        
    }
}