/**************************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmNewAuthUserPopup.cs	    	
Programmer:         Diane Musachio
Creation Date:      10/28/2016
Inputs:             If edit - userid, grpcde and printer               
Parameters:		    None 
Outputs:		     	
Description:	    This popup allows user to either view or add, edit, delete users from
                    SCHED_ID table

Detailed Design:    Detailed User Requirements for the Authorized Users Information Screen 
Other:	            called from: frmAuthUsers.cs
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
    public partial class frmNewAuthUserPopup : Form
    {
        /*********************** Public Properties  *******************/
        /* optional - if edit */
        public string UserId;
        public string oldval;
        public string printer;
        public string Grade;
        public string Initsl;
        public string Initnr;
        public string Initfd;
        public string Initmf;
        public string Contsl;
        public string Contnr;
        public string Contfd;
        public string Contmf;

        public Form CallingForm = null;

        /***************************************************************/
        // private strings
        private string location = "";

        private AuthorizedUsersData dataObject;

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

        public frmNewAuthUserPopup()
        {
            InitializeComponent();
        }

        //determine if editing
        private bool edit = false;

        private void frmNewAuthUserPopup_Load(object sender, EventArgs e)
        {
            //Create list of roles
            CreateDataSource();

            cbRole.SelectedIndex = -1;
            cbPrintq.SelectedIndex = -1;
            cbGrade.SelectedIndex = -1;

            //combo box is enabled for the following roles
            if (UserInfo.GroupCode == EnumGroups.Programmer || UserInfo.GroupCode == EnumGroups.HQManager ||
                UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.HQTester || 
                UserInfo.GroupCode == EnumGroups.HQSupport)
            {
                cbRole.Enabled = true;
            }
            //disable combo box for all others
            else 
            {
                cbRole.Enabled = false;
            }
            

            //If there is a userid provided then the popup was called from the edit button
                if (!String.IsNullOrWhiteSpace(UserId))
            {
                txtUser.Text = UserId;

                //disable the userid text as this cannot be updated for edit
                txtUser.Enabled = false;

                edit = true;

                txtRole.Text = GetDisplayRoleText(oldval);
                cbRole.Text = txtRole.Text;

                cbPrintq.Text = printer;

                if (oldval == "5")
                {
                    cbGrade.Enabled = true;
                    cklRole.Enabled = true;
                    groupBox1.Enabled = true;
                    cbGrade.Text = Grade;

                    if (UserInfo.GroupCode == EnumGroups.NPCLead)
                        cbGrade.Enabled = false;

                    for (int i = 0; i < cklRole.Items.Count; i++)
                    {
                        if (Initsl == "Y")
                        {
                           cklRole.SetItemCheckState(0, CheckState.Checked);                                   
                        }
                        if (Initnr == "Y")
                        {
                            cklRole.SetItemCheckState(1, CheckState.Checked);
                        }
                        if (Initfd == "Y")
                        {
                            cklRole.SetItemCheckState(2, CheckState.Checked);
                        }
                        if (Initmf == "Y")
                        {
                            cklRole.SetItemCheckState(3, CheckState.Checked);
                        }
                        if (Contsl == "Y")
                        {
                            cklRole.SetItemCheckState(4, CheckState.Checked);
                        }
                        if (Contnr == "Y")
                        {
                            cklRole.SetItemCheckState(5, CheckState.Checked);
                        }
                        if (Contfd == "Y")
                        {
                            cklRole.SetItemCheckState(6, CheckState.Checked);
                        }
                        if (Contmf == "Y")
                        {
                            cklRole.SetItemCheckState(7, CheckState.Checked);
                        }
                    }                      
                }
                else
                {
                    groupBox1.Enabled = false;
                    Grade = "";
                    Initsl = "";
                    Initnr = "";
                    Initfd = "";
                    Initmf = "";
                    Contsl = "";
                    Contnr = "";
                    Contfd = "";
                    Contmf = "";
                }
            }
            //if no values provided then the popup was called from add or delete button
            else
            {
                txtUser.Text = "";
                txtRole.Text = "";
                cbPrintq.Text = "";
                txtUser.Enabled = true;
                groupBox1.Enabled = false;
                Grade = "";
                Initsl="";
                Initnr="";
                Initfd="";
                Initmf="";
                Contsl="";
                Contnr="";
                Contfd="";
                Contmf="";
            }

        }

        private string role_text;

        private void CreateDataSource()
        {
            BindingSource source = new BindingSource();
            List<string> dataSource = new List<string>();

            dataSource.Add("0 - Programmer");
            dataSource.Add("1 - HQManager");
            dataSource.Add("2 - HQAnalyst");
            dataSource.Add("3 - NPCManager");
            dataSource.Add("4 - NPCLead");
            dataSource.Add("5 - NPCInterviewer");
            dataSource.Add("6 - HQSupport");
            dataSource.Add("7 - HQMathStat");
            dataSource.Add("8 - HQTester");

            cbRole.DataSource = dataSource;
        }

        private string groupcode;

        //from role get display text
        private string GetDisplayRoleText(string role_code)
        {
            string coltec_text = string.Empty;
            if (role_code == "0")
                role_text = "0 - Programmer";
            else if (role_code == "1")
                role_text = "1 - HQManager";
            else if (role_code == "2")
                role_text = "2 - HQAnalyst";
            else if (role_code == "3")
                role_text = "3 - NPCManager";
            else if (role_code == "4")
                role_text = "4 - NPCLead";
            else if (role_code == "5")
                role_text = "5 - NPCInterviewer";
            else if (role_code == "6")
                role_text = "6 - HQSupport";
            else if (role_code == "7")
                role_text = "7 - HQMathStat";
            else if (role_code == "8")
                role_text = "8 - HQTester";
            else
                role_text = "";

            if (role_code == ("3") || role_code == ("4") || role_code == ("5"))
            {
                location = "NPC";
            }
            else
            {
                location = "HQ";
            }

            groupcode = role_code;

            //populates printer combobox with all printers from selected location
            PopulatePrint(location);

            return role_text;
        }

        //populates printer combobox with all printers from selected location
        private void PopulatePrint(string location)
        {
            AuthorizedUsersData dataObject = new AuthorizedUsersData();

            cbPrintq.DataSource = dataObject.GetPrinterTable(location);
            cbPrintq.ValueMember = "NAME";
            cbPrintq.DisplayMember = "NAME";
            cbPrintq.SelectedIndex = -1;            
        }

        //click save button
        private void btnSave_Click(object sender, EventArgs e)
        {
            dataObject = new AuthorizedUsersData();
           
            //check if the factors are blank, if so, display messagebox            
            if (txtUser.Text.Length < 8)
            {
                MessageBox.Show("Cannot save - UserName must be 8 characters");
            }
            else if ((dataObject.CheckUsernameExist(txtUser.Text)) && (edit != true))
            {
                MessageBox.Show("Cannot save - UserName already exists");
            }
            else if ((txtUser.Text == "" || txtRole.Text == "" || cbPrintq.Text == ""))
            {
                if (txtUser.Text == "")
                {
                    MessageBox.Show("Please enter a james bond id");
                }
                else if (txtRole.Text == "")
                {
                    MessageBox.Show("Please select a role for the user");
                }
                else if (cbPrintq.Text == "")
                {
                    MessageBox.Show("Please select a print queue");
                }
            }
            else if ((cbRole.Text.Substring(0, 1) == "5"))
            {
                 populate_cklRole();

                if (cbGrade.Text == "")
                {
                   MessageBox.Show("Please select a grade");
                   cbGrade.Focus();
                 }
                else if ((icount == 0) && (ccount == 0))
                {
                   MessageBox.Show("Please select at least one initial survey or at least one continue survey");
                }
                else
                {
                    addAuthorizedUser();
                }
            }
            else
            {
                Grade = "";
                Initsl = "";
                Initfd = "";
                Initnr = "";
                Initmf = "";
                Contsl = "";
                Contfd = "";
                Contnr = "";
                Contmf = "";
                addAuthorizedUser();
            }          
        }

        private void addAuthorizedUser()
        {
            string usrnme = txtUser.Text.ToString();
            string grade = cbGrade.Text;
            string printq = cbPrintq.Text;
            string grpcde = groupcode;
            string prgdtm;
            string newname = UserInfo.UserName;
           
            DateTime dt = DateTime.Now;
            prgdtm = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);
        
            //if edit update sched_id table and if role changed add edit to useraudit 
            if (edit)
            {
                dataObject.UpdateAuthorizedUser(usrnme, grpcde, grade, printq, Initsl, Initnr, Initfd, Initmf, Contsl, Contnr, Contfd, Contmf);
                if (oldval != grpcde)
                {
                    dataObject.AddUserAuditData(newname, "EDIT", oldval, grpcde, usrnme, prgdtm);
                    //send email
                    string suj = "CPRS Authorized User was Edited";
                    string mbody = "";
                    string from = UserInfo.UserName + "@census.gov";

                    //send email to appropriate users
                    List<string> tolist = new List<string>();
                    tolist = GeneralDataFuctions.GetJobEmails("USR1");
                    if (tolist.Count > 0)
                        GeneralFunctions.SendEmail(suj, mbody, from, tolist);
                }
            }
            //id is added to sched_id and to useraudit
            else
            {
                dataObject.AddAuthorizedUser( usrnme, grpcde, grade, printq, Initsl, Initnr, Initfd, Initmf, Contsl, Contnr, Contfd, Contmf);
                dataObject.AddUserAuditData(newname, "ADD", "", grpcde, usrnme, prgdtm); 
                
                //send email
                string suj = "Authorized User was Added to CPRS";
                string mbody = "";
                string from = UserInfo.UserName + "@census.gov";

                //send email to appropriate users
                List<string> tolist = new List<string>();
                tolist = GeneralDataFuctions.GetJobEmails("USR1");
                if (tolist.Count > 0)
                   GeneralFunctions.SendEmail(suj, mbody, from, tolist);
            }

            this.Refresh();
            this.Dispose();
        }

        private void cbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            cklRole.Enabled = false;
            cbGrade.Enabled = false;
            groupBox1.Enabled = false;

                if (cbRole.SelectedIndex > -1)
                {
                    string grp= cbRole.Text.Substring(0, 1);
                    groupcode = grp;

                    txtRole.Text = GetDisplayRoleText(grp);

                    switch (grp)
                    {
                        case "0":
                            location = "HQ";
                            break;
                        case "1":
                            location = "HQ";
                            break;
                        case "2":
                            location = "HQ";
                            break;
                        case "3":
                            location = "NPC";
                            break;
                        case "4":
                            location = "NPC";
                            break;
                        case "5":
                            location = "NPC";
                            groupBox1.Enabled = true;
                            cbGrade.Enabled = true;
                            cklRole.Enabled = true;
                            break;
                        case "6":
                            location = "HQ";
                            break;
                        case "7":
                            location = "HQ";
                            break;
                        case "8":
                            break;
                    }
                }

                PopulatePrint(location);

                if (groupcode != "5")
                {
                    cbGrade.Text = "";

                    for (int i = 0; i < cklRole.Items.Count; i++)
                    {
                        cklRole.SetItemCheckState(i, CheckState.Unchecked);
                    }

                    this.Refresh();
                }
        }
       
        private void cbPrintq_SelectIndexChanged(object sender, EventArgs e)
        {
            if (cbPrintq.SelectedIndex > -1)
            {
                DataRowView drv = (DataRowView)cbPrintq.SelectedItem;
                string valueOfItem = drv["NAME"].ToString();
                cbPrintq.Text = valueOfItem;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private int icount;
        private int ccount;

        //goes through the checklist of roles to determine if the box is checked
        private void populate_cklRole()
        {
            icount = 0;
            ccount = 0;

            for (int i = 0; i < cklRole.Items.Count; i++)
            {
                if (cklRole.GetItemCheckState(i) == CheckState.Checked)
                {
                    if (i == 0)
                    {
                        Initsl = "Y";
                        icount++;
                    }
                    if (i == 1)
                    {
                        Initfd = "Y";
                        icount++;
                    }
                    if (i == 2)
                    {
                        Initnr = "Y";
                        icount++;
                    }
                    if (i == 3)
                    {
                        Initmf = "Y";
                        icount++;
                    }
                    if (i == 4)
                    {
                        Contsl = "Y";
                        ccount++;
                    }
                    if (i == 5)
                    {
                        Contfd = "Y";
                        ccount++;
                    }
                    if (i == 6)
                    {
                        Contnr = "Y";
                        ccount++;
                    }
                    if (i == 7)
                    {
                        Contmf = "Y";
                        ccount++;
                    }
                }              
                else
                {
                    if (i == 0)
                    {
                        Initsl = "N";
                    }
                    if (i == 1)
                    {
                        Initfd = "N";
                    }
                    if (i == 2)
                    {
                        Initnr = "N";
                    }
                    if (i == 3)
                    {
                        Initmf = "N";
                    }
                    if (i == 4)
                    {
                        Contsl = "N";
                    }
                    if (i == 5)
                    {
                        Contfd = "N";
                    }
                    if (i == 6)
                    {
                        Contnr = "N";
                    }
                    if (i == 7)
                    {
                        Contmf = "N";
                    }
                }
            }
        }
    }
}