using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using CprsBLL;
using CprsDAL;
using System.Management;
using System.Drawing.Printing;

namespace Cprs
{
    public partial class frmWelcome : Form
    {
        public frmWelcome()
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

        private void frmWelcome_Load(object sender, EventArgs e)
        {

            txtJbondid.Text = Environment.GetEnvironmentVariable("UserName");

        }

        private void btnContinue_Click(object sender, EventArgs e)
        {
            //Check database connection
            int connected = GeneralDataFuctions.CheckDatabaseConnection();
           
            if (connected == 1)
            {
                MessageBox.Show("                      The CPRS Database is unavailable." + "\n" + "\n" + "Please contact Special Applications Standardization Branch" + "\n" + "\n" + "Email: 'emd.all.sasb.list@census.gov' or Call 301-763-7359", "No Access to Database");
                Application.Exit();
                return;
            }
            else if (connected == 2)
            {
                MessageBox.Show("                      The CPRS Database is unavailable." + "\n" + "\n" + "Please contact Special Applications Standardization Branch" + "\n" + "\n" + "Email: 'emd.all.sasb.list@census.gov' or Call 301-763-7359", "Database Down");

                Application.Exit();
                return;
            }

            //get user info
            UserInfoData data_object = new UserInfoData();
            if (!data_object.GetUserInfo())
            {
                MessageBox.Show("You do not have access to this system.");
                Application.Exit();
                return;
            }

            if (UserInfo.GroupCode == EnumGroups.HQTester || (UserInfo.GroupCode == EnumGroups.HQSupport && UserInfo.UserName != "sheck001"))
            {
                GlobalVars.Databasename = "CPRSDEV";
            }
            else
            {
                frmDatabaseSelectionPopup popup = new frmDatabaseSelectionPopup();
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.ShowDialog();
                GlobalVars.Databasename = popup.SelectedDatabase;
            }

            //refresh user info
            data_object.GetUserInfo();

            //Check SysStatus table
            if (!data_object.CheckUserAvailable(UserInfo.GroupCode))
            {
                MessageBox.Show("    The System is not available.");
                Application.Exit();
                return;
            }

            if (GeneralData.IsNpcAccessControlEnabled())
            {
                if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                    UserInfo.GroupCode == EnumGroups.NPCLead ||
                    UserInfo.GroupCode == EnumGroups.NPCInterviewer)
                {
                    TimeSpan start = GeneralData.GetNpcAccessStartTime();
                    if (DateTime.Now.TimeOfDay < start)
                    {
                        string display = DateTime.Today.Add(start).ToString("h tt");
                        MessageBox.Show($"This application is locked for NPC users until {display}.");
                        Application.Exit();
                        return;
                    }
                }
            }

            CurrentUsersData cuData = new CurrentUsersData();
            int cprSession = cuData.GetSessionInfo();
            if (cprSession > 2)
            {
                MessageBox.Show("                                    You have two Active Sessions." + "\n" + "\n" + "Please contact your supervisor to unlock one or both of your sessions","Max sessions");
                Application.Exit();
                return;
            }
            else
            {
                GlobalVars.Session = cprSession;
                cuData.AddCurrentUsersData("SYSTEM");
            }

            //add record to cpraccess
            GeneralDataFuctions.AddCpraccessData("SYSTEM", "ENTER");

            frmHome form = new frmHome();
            form.Show();
            this.Hide();  
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
/*
        private List<string> GetPrinters()
        {
            List<string> printerNames = new List<string>();

            // Use the ObjectQuery to get the list of configured printers
            System.Management.ObjectQuery oquery =
                new System.Management.ObjectQuery("SELECT * FROM Win32_Printer");

            System.Management.ManagementObjectSearcher mosearcher =
                new System.Management.ManagementObjectSearcher(oquery);

            System.Management.ManagementObjectCollection moc = mosearcher.Get();

            foreach (ManagementObject mo in moc)
            {
                System.Management.PropertyDataCollection pdc = mo.Properties;
                foreach (System.Management.PropertyData pd in pdc)
                {
                    if ((bool)mo["Network"])
                    {
                        //Console.Out.WriteLine(pd.Name);
                        //printerNames.Add(mo[pd.Name]);
                        printerNames.Add(pd.Name);
                    }
                }
            }

            return printerNames;

        } 

        private List<string> PopulateInstalledPrinters()
        {
            List<string> printerNames = new List<string>();

            // Add list of installed printers found to the combo box. 
            // The pkInstalledPrinters string will be used to provide the display string.
            String pkInstalledPrinters;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                printerNames.Add(pkInstalledPrinters);
            }

            return printerNames;
        } */

    }
}
