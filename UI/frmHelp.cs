using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;

namespace Cprs
{
    public partial class frmHelp : frmCprsParent
    {
        public frmHelp()
        {
            InitializeComponent();
        }
        public string mnunum = "" ;

        private void frmHelp_Load(object sender, EventArgs e)
        {
            string file_name = "";
            switch (mnunum)
            {
                case "1":
                    if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
                        file_name = GlobalVars.HelpDir + "CPRS II Manual_Manager.pdf";
                    else if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                        file_name = GlobalVars.HelpDir + "CPRS II Manual_Interviewer.pdf";
                    else
                        file_name = GlobalVars.HelpDir + "CPRS II Manual.pdf";
                    break;
                case "2":
                    file_name = GlobalVars.HelpDir + "Centurion Manual for NPC.pdf";
                    break;
                case "3":
                    file_name = GlobalVars.HelpDir + "NPC Training DODGES RESPID.pdf";
                    break;
                case "4":
                    file_name = GlobalVars.HelpDir + "MF CAPI.pdf";
                    break;
                case "5":
                    file_name = GlobalVars.HelpDir + "Respondent Search.pdf";
                    break;
                case "6":
                    file_name = GlobalVars.HelpDir + "NPC Training TFU.pdf";
                    break;
                case "7":
                    file_name = GlobalVars.HelpDir + "NPC Training - Form Entry.pdf";
                    break;
                case "8":
                    file_name = GlobalVars.HelpDir + "Federal Contract Numbers Dodge Initials.pdf";
                    break;
                
            }
            
            axAcroPDF1.LoadFile(@file_name);
            axAcroPDF1.setZoom(100);
        }
    }

}
