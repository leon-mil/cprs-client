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

        private void frmHelp_Load(object sender, EventArgs e)
        {
            string file_name = "";

            if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.NPCManager)
                file_name = GlobalVars.HelpDir + "CPRS II Manual_Manager.pdf";
            else if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                file_name = GlobalVars.HelpDir + "CPRS II Manual_Interviewer.pdf";
            else
                file_name = GlobalVars.HelpDir + "CPRS II Manual.pdf";

            axAcroPDF1.LoadFile(@file_name);
            axAcroPDF1.setZoom(100);
        }
    }

}
