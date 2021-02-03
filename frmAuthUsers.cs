/**************************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmAuthUsers.cs	    	
Programmer:         Diane Musachio
Creation Date:      10/28/2016
Inputs:             None                 
Parameters:		    None 
Outputs:		    None	
Description:	    Allows appropriate users to either view or add, edit, delete users from
                    SCHED_ID table and update audit trails

Detailed Design:    Detailed User Requirements for the Authorized Users Screen 
Other:	            
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
using DGVPrinterHelper;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmAuthUsers : frmCprsParent
    {
        private AuthorizedUsersData dataObject;

        public frmAuthUsers()
        {
            InitializeComponent();
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "ENTER");
            GeneralDataFuctions.UpdateCurrentUsersData("ADMINISTRATIVE");
        }

        private void frmAuthUsers_Load(object sender, EventArgs e)
        {
            // If the user is NPC Lead or NPC Manager the users displayed will only be NPC Users
            // and the add and delete buttons will be disabled
            if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                GetNPCUsers();
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
            }
            //All users will be displayed
            else
            {
                GetDTUsers();

                //Only the following staff are allowed to add and delete users 
                if (UserInfo.GroupCode == EnumGroups.Programmer || UserInfo.GroupCode == EnumGroups.HQManager ||
                    UserInfo.GroupCode == EnumGroups.HQSupport || UserInfo.GroupCode == EnumGroups.HQTester)
                {
                    btnAdd.Enabled = true;
                    btnDelete.Enabled = true;
                    btnEdit.Enabled = true;
                }
                //Disable add and delete buttons for other users
                else
                {
                    btnAdd.Enabled = false;
                    btnDelete.Enabled = false;
                    btnEdit.Enabled = false;
                }
            }

            if (UserInfo.GroupCode == EnumGroups.Programmer || UserInfo.GroupCode == EnumGroups.HQManager
                || UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                btnAnnual.Enabled = true;
            }
            else btnAnnual.Enabled = false;
                
        }

        //Get all authorized users from CprsDAL data
        private void GetDTUsers()
        {
            dataObject = new AuthorizedUsersData();

            DataTable dtUsers;

            //populate datagrid with all users
            dtUsers = dataObject.GetAuthorizedUsersTable();
            dgUsers.DataSource = dtUsers;
            dgUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        //Get NPC authorized users from CprsDAL data
        private void GetNPCUsers()
        {
            dataObject = new AuthorizedUsersData();

            DataTable dtUsers;

            //populate datagrid with npc users
            dtUsers = dataObject.GetNPCAuthorizedUsersTable();
            dgUsers.DataSource = dtUsers;
            dgUsers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
   
        }

        //Format the datagrid view cells to display the groupcode and description for the roles assigned
        private void dgUsers_CellFormatting(object sender, System.Windows.Forms.DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == -1)
                return;

            if (dgUsers.Columns[e.ColumnIndex].Name.Equals("ROLE"))
            {
                String stringValue = e.Value as string;
                if (stringValue == null) return;

                switch (stringValue)
                {
                    case "0":
                        e.Value = "Programmer";
                        break;
                    case "1":
                        e.Value = "HQManager";
                        break;
                    case "2":
                        e.Value = "HQAnalyst";
                        break;
                    case "3":
                        e.Value = "NPCManager";
                        break;
                    case "4":
                        e.Value = "NPCLead";
                        break;
                    case "5":
                        e.Value = "NPCInterviewer";
                        break;
                    case "6":
                        e.Value = "HQSupport";
                        break;
                    case "7":
                        e.Value = "HQMathStat";
                        break;
                    case "8":
                        e.Value = "HQTester";
                        break;
                }
            }
        }

        //Click on add button displays frmNewAuthUserPopup.cs form with no pre-populated values
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmNewAuthUserPopup fUser = new frmNewAuthUserPopup();
            fUser.UserId = "";
            fUser.ShowDialog();

            // If the user is NPC Lead or NPC Manager the users displayed will only be NPC Users
            if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                GetNPCUsers();
            }
            // otherwise all authorized users are displayed
            else
            {
                GetDTUsers();
            }
        }

        //string to determine username to delete
        public string user;

        //Click on delete button 
        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Display the verification popup to ensure the user 
            //wants to delete the selected case (a case will always be selected - 
            //the default selection is the first row so 
            //there was no need to validate if a case is selected)

            frmVerifyDeletePopup popup = new frmVerifyDeletePopup();

            DialogResult dialogresult = popup.ShowDialog();

            if (dialogresult == DialogResult.Yes)
            {
                //username to delete
                string user = dgUsers.CurrentRow.Cells[0].Value.ToString();
                string prgdtm;

                //assign program data and time for audit trails
                DateTime dt = DateTime.Now;
                prgdtm = string.Format("{0:yyyy-MM-dd HH:mm:ss}", dt);

                //delete row
                dataObject.DeleteRow(user);

                //pass information to audit trail
                dataObject.AddUserAuditData(UserInfo.UserName, "DELETE", "", "", user, prgdtm);

                //send email
                string suj = "CPRS Authorized User was Deleted";
                string mbody = "";
                string from = UserInfo.UserName + "@census.gov";

                List<string> tolist = new List<string>();
                tolist = GeneralDataFuctions.GetJobEmails("USR1");

                // if rows in tolist then send email to appropriate user
                if (tolist.Count > 0)
                    GeneralFunctions.SendEmail(suj, mbody, from, tolist);

                //Refresh the data grid display
                GetDTUsers();
            }

            popup.Dispose();
        }

        //click on edit button
        private void btnEdit_Click(object sender, EventArgs e)
        {
            frmNewAuthUserPopup fUser = new frmNewAuthUserPopup();

            //select row to be edited and get values to pass to popup
            DataGridViewRow row = this.dgUsers.SelectedRows[0];

            string user = row.Cells[0].Value.ToString();
            string oldval = row.Cells[1].Value.ToString();
            string grade = row.Cells[2].Value.ToString();
            string printq = row.Cells[3].Value.ToString();
            string initsl = row.Cells[4].Value.ToString();
            string initnr = row.Cells[5].Value.ToString();
            string initfd = row.Cells[6].Value.ToString();
            string initmf = row.Cells[7].Value.ToString();
            string contsl = row.Cells[8].Value.ToString();
            string contnr = row.Cells[9].Value.ToString();
            string contfd = row.Cells[10].Value.ToString();
            string contmf = row.Cells[11].Value.ToString();

            fUser.CallingForm = this;
            fUser.UserId = user;
            fUser.oldval = oldval;
            fUser.printer = printq;
            fUser.Grade = grade;
            fUser.Initsl = initsl;
            fUser.Initnr = initnr;
            fUser.Initfd = initfd;
            fUser.Initmf = initmf;
            fUser.Contsl = contsl;
            fUser.Contnr = contnr;
            fUser.Contfd = contfd;
            fUser.Contmf = contmf;
            fUser.ShowDialog();

            // If the user is NPC Lead or NPC Manager the users displayed will only be NPC Users
            if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                GetNPCUsers();
            }
            // otherwise all authorized users are displayed
            else
            {
                GetDTUsers();
            }
        }

        //access user audit table
        private void btnAudit_Click(object sender, EventArgs e)
        {
            frmAuthUserAuditPopup fAudit = new frmAuthUserAuditPopup();

            fAudit.ShowDialog();  //show audit table
        }

        //access annual audit review screen
        private void btnAnnual_Click(object sender, EventArgs e)
        {
            frmAuthUserAnnAuditPopup fAAudit = new frmAuthUserAnnAuditPopup();

            fAAudit.ShowDialog();  //show annual audit table
        }

        private void frmAuthUsers_FormClosing(object sender, FormClosingEventArgs e)
        {
            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("ADMINISTRATIVE", "EXIT");
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            PrintData();
        }

        private void PrintData()
        {
            Cursor.Current = Cursors.WaitCursor;

            DGVPrinter printer = new DGVPrinter();
            printer.Title = "AUTHORIZED USERS";
            printer.TitleFont = new Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular);
            printer.UserInfoFont = new Font(FontFamily.GenericSansSerif, 8.25F, FontStyle.Regular);
            printer.Userinfo = UserInfo.UserName;

            printer.PageNumbers = true;
            printer.PageNumberInHeader = true;

            printer.HeaderCellAlignment = StringAlignment.Near;
            printer.PrintRowHeaders = false ;

            printer.printDocument.PrinterSettings.PrinterName = @UserInfo.PrinterQ;
            printer.printDocument.DocumentName = "Authorized Users Print";
            printer.printDocument.DefaultPageSettings.Landscape = true;

            printer.Footer = " ";
            Margins margins = new Margins(30, 30, 30, 30);
            printer.PrintMargins = margins;
            printer.PrintDataGridViewWithoutDialog(dgUsers);

            Cursor.Current = Cursors.Default;
        }
    }      
}
