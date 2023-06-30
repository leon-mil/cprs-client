/**********************************************************************************
Econ App Name   : Construction Progress Report Survey (CPRS)

Project Name    : CPRS 

Program Name    : frmCprsParent.cs	 
   	
Programmer      : Kevin Montgomery
Creation Date   :    
Inputs          : None
               
Parameters      : None   
             
Outputs         : None

Description:	: Enables menus

Detailed Design : None

Other           :	            
 
Revision History:	
***********************************************************************************
Modified Date   : March 23, 2021
Modified By     : Christine Zhang 
Keyword         : 20210323cz 
Change Request  : CR 8068
Description     : Expand Help Menu to include selections for other documents 
***********************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    public partial class frmCprsParent : Form
    {
        public frmCprsParent()
        {
            InitializeComponent();
            mnuHome.MouseHover += (s, e) => mnuHome.ShowDropDown();
            mnuSearch.MouseHover += (s, e) => mnuSearch.ShowDropDown();
        }

        //Public method to disable main menus
        public void DisableMenus()
        {
            mnuHome.Enabled = false;
            mnuSearch.Enabled = false;
            mnuCall.Enabled = false;
            mnuUtility.Enabled = false;
            mnuReport.Enabled = false;
            mnuAdmin.Enabled = false;
            mnuImprove.Enabled = false;
            mnuTabs.Enabled = false;
            mnuPress.Enabled = false;
            mnuNCE.Enabled = false;
            mnuHelp.Enabled = false;
        }

        //Pubic method to enable main menus
        public void EnableMenus()
        {
            mnuHome.Enabled = true;
            mnuSearch.Enabled = true;
            mnuCall.Enabled = true;
            mnuUtility.Enabled = true;
            mnuReport.Enabled = true;
            mnuAdmin.Enabled = true;
            mnuImprove.Enabled = true;
            mnuTabs.Enabled = true;
            mnuPress.Enabled = true;
            mnuNCE.Enabled = true;
            mnuHelp.Enabled = true;
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

        private void frmCprsParent_Load(object sender, EventArgs e)
        {
            //this.toolStripStatusLabel1.Padding = new Padding((int)(this.Size.Width - 200), 0, 0, 0);
            toolStripStatusLabel3.Text = string.Format("Role: {0}", UserInfo.GroupCode.ToString());
            toolStripStatusLabel2.Text = string.Format("Database: {0}", GlobalVars.Databasename);
            toolStripStatusLabel1.Text = string.Format("Session: {0}", GlobalVars.Session);

            //Turn off all searches except for Respondent search for all NPC Users

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                mnuMaster.Visible = false;
                mnuSample.Visible = false;
                mnuC700.Visible = false;
                mnuAdHoc.Visible = false;
                mnuInteractive.Visible = false;
            }

            //Turn off NCE, Press, Tabs and Improvements menus for all NPC Users

            mnuNCE.Visible = true;
            mnuPress.Visible = true;
            mnuTabs.Visible = true;
            mnuImprove.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCManager || UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.NPCLead)
            {
                mnuNCE.Visible = false;
                mnuPress.Visible = false;
                mnuTabs.Visible = false;
                mnuImprove.Visible = false;
            }

            //-------------------------------------------------------------------------
            // SETUP MENU
            //--------------------------------------------------------------------------
            //Turn off Setup menu for NPC Interviewers and HQMathStat

            mnuSetup.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer || UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuSetup.Visible = false;

                mnuCcmail.Visible = false;
                mnuTcDescription.Visible = false;
                mnuMySectors.Visible = false;
                munPrinters.Visible = false;
                mnuCallScheduler.Visible = false;
            }

            mnuCcmail.Visible = false;
            mnuTcDescription.Visible = false;
            mnuMySectors.Visible = false;
            munPrinters.Visible = false;
            mnuCallScheduler.Visible = false;

            if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                UserInfo.GroupCode == EnumGroups.HQManager ||
                UserInfo.GroupCode == EnumGroups.HQSupport ||
                UserInfo.GroupCode == EnumGroups.HQTester ||
                UserInfo.GroupCode == EnumGroups.Programmer)
            {
                mnuTcDescription.Visible = true;
                mnuMySectors.Visible = true;
            }

            if (UserInfo.GroupCode == EnumGroups.HQManager ||
                UserInfo.GroupCode == EnumGroups.HQSupport ||
                UserInfo.GroupCode == EnumGroups.HQTester ||
                UserInfo.GroupCode == EnumGroups.Programmer)
            {
                mnuCallScheduler.Visible = true;
                mnuCcmail.Visible = true;
            }

            if (UserInfo.GroupCode == EnumGroups.Programmer)
            {
                munPrinters.Visible = true;
            }


            //-------------------------------------------------------------------------
            // DATA ENTRY MENU
            //-------------------------------------------------------------------------

            mnuCall.Visible = true;
            mnuPhone.Visible = true;
            mnuRespUpd.Visible = true;
            mnuInitials.Visible = true;

            //Turn off TFU for all HQ

            if (UserInfo.GroupCode == EnumGroups.HQManager ||
                UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                UserInfo.GroupCode == EnumGroups.HQSupport ||
                UserInfo.GroupCode == EnumGroups.HQTester ||
                UserInfo.GroupCode == EnumGroups.Programmer ||
                UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuPhone.Visible = false;
            }

            //Turn off Initials and Respondent Updatefor HQMathStat

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuRespUpd.Visible = false;
                mnuInitials.Visible = false;
            }


            //Turn off NP Special Case for NPC users

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer ||
                UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                mnuNpSpecial.Visible = false;
                mnuDupResearch.Visible = false;
            }

            //--------------------------------------------------------------------------
            // CASE MANAGEMENT (UTILITY) MENU -
            //--------------------------------------------------------------------------

            mnuUtility.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4")
            {
                mnuUtility.Visible = false;
            }

            //Turn off Failed Verification Review for NPCLead and NPCManager and NPC Interview-Grade 5

            if (UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.NPCManager ||
                UserInfo.GroupCode == EnumGroups.NPCInterviewer
                )
            {
                mnuFailedVerificationReview.Visible = false;
            }

            //Turn off MF and Dodge Initial Case review for HQMathStat 

            mnuMfInitialReview.Visible = true;
            mnuDodgeInitialReview.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuMfInitialReview.Visible = false;
                mnuDodgeInitialReview.Visible = false;
            }

            //--------------------------------------------------------------------------------
            // REPORT MENU
            //--------------------------------------------------------------------------------

            mnuReport.Visible = true;

            //Turn off Report Menu for NPC Interviewer

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                mnuReport.Visible = false;
            }

            //Turn of NPC Attempts Report for NPClead and HQMathStat

            mnuAttempts.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCLead || UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuAttempts.Visible = false;
            }

            //Turn of Case Management Report for HQMathStat

            mnuCases.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuCases.Visible = false;
            }

            //Turn of NPC Active Workload Report for HQMathStat

            mnuWorkload.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuWorkload.Visible = false;
            }

            //Turn of Flag Report for HQMathStat

            mnuFlags.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuFlags.Visible = false;
            }

            //Turn of Call Scheduler Counts for HQMathStat

            mnuScheduler.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuScheduler.Visible = false;
            }

            //Turn off Response Rate Report for NPCLead and NPCManager

            if (UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.NPCManager)
            {
                mnuResponseRates.Visible = false;
            }

            //------------------------------------------------------------------------------------
            // ADMINISTRATION MENU
            //------------------------------------------------------------------------------------

            mnuAdmin.Visible = true;

            //Turn of Admin Menu for NPC Interviewer Grade 4

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "4")
            {
                mnuAdmin.Visible = false;
            }

            //System Audit

            mnuSAudit.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                UserInfo.GroupCode == EnumGroups.HQMathStat ||
                UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                mnuSAudit.Visible = false;
            }

            //Project Audit, Respondent Audit, Auth users

            mnuPAudit.Visible = true;
            mnuRAudit.Visible = true;
            mnuAuthUsers.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                mnuPAudit.Visible = false;
                mnuRAudit.Visible = false;
                mnuAuthUsers.Visible = false;
            }

            //Project Access, Current Users

            mnuPAccess.Visible = true;
            mnuCurUsers.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat || UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                mnuPAccess.Visible = false;
                mnuCurUsers.Visible = false;
            }

            //NPC Specials

            mnuNpcSpecials.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuNpcSpecials.Visible = false;
            }

            //Unlock

            mnuUnlock.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.HQMathStat)
            {
                mnuUnlock.Visible = false;
            }

            //Monthly and Annual processing

            mnuMonProc.Visible = true;
            mnuAnnualProc.Visible = true;

            if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.HQMathStat ||
                UserInfo.GroupCode == EnumGroups.NPCInterviewer)
            {
                mnuMonProc.Visible = false;
                mnuAnnualProc.Visible = false;
            }

            //Help menu
            if (UserInfo.GroupCode == EnumGroups.HQAnalyst || UserInfo.GroupCode == EnumGroups.HQMathStat || UserInfo.GroupCode == EnumGroups.Programmer ||
                UserInfo.GroupCode == EnumGroups.HQManager || UserInfo.GroupCode == EnumGroups.HQSupport || UserInfo.GroupCode == EnumGroups.HQTester)
            {
                mnuHelpTelephoneFollowUp.Visible = false;
                mnuHelpFormEntryProcedures.Visible = false;
            }

            //Form list menu
            mnuFormlist.Visible = false;
            if (UserInfo.GroupCode == EnumGroups.NPCManager ||
                UserInfo.GroupCode == EnumGroups.NPCLead ||
                UserInfo.GroupCode == EnumGroups.HQAnalyst ||
                UserInfo.GroupCode == EnumGroups.HQManager ||
                UserInfo.GroupCode == EnumGroups.HQSupport ||
                (UserInfo.GroupCode == EnumGroups.NPCInterviewer && UserInfo.Grade == "5") ||
                UserInfo.GroupCode == EnumGroups.Programmer)
            {
                mnuFormlist.Visible = true;
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                //delete current session form CURRENT_USERS table
                CurrentUsersData cuData = new CurrentUsersData();
                cuData.DeleteCurrentUsersData();

                this.Close();
                this.Dispose();

                //add record to cpraccess
                GeneralDataFuctions.AddCpraccessData("SYSTEM", "EXIT");

                Application.Exit();
            }
        }

        private void mnuMaster_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmMaster fM = new frmMaster();
                fM.Show();
            }
        }

        private void mnuSample_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSample fS = new frmSample();
                fS.Show();
            }
        }

        private void mnuC700_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmC700Srch fC = new frmC700Srch();
                fC.Show();
            }
        }

        private void mnuRespondent_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmRespondent fR = new frmRespondent();
                fR.Show();
            }
        }

        private void mnuAdHoc_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmAdHoc fA = new frmAdHoc();
                fA.Show();
            }
        }

        private void mnuInteractive_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmWhereSrch fWS = new frmWhereSrch();
                fWS.Show();
            }
        }
        //--------------------------------------------------------------------------------
        // HELP MENU
        //--------------------------------------------------------------------------------
       
    private void mnuHome_Click(object sender, EventArgs e)
        {
            //if (VerifyFormClosing())
            //{
            //    this.Close();
            //    frmHome fH = new frmHome();
            //    fH.Show();
            //}
        }

        private void mnuPhone_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTfu fTFU = new frmTfu();
                fTFU.Show();
            }
        }

        private void mnuRAudit_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmRspAudit fRA = new frmRspAudit();  
                fRA.Show();
            }
        }

        private void mnuPAudit_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmProjectAudit fMAU = new frmProjectAudit(); 
                fMAU.Show();
            }
        }

        private void mnuPAccess_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmProjectAccess fMAC = new frmProjectAccess();
                fMAC.Show();
            }
        }

        private void mnuSAudit_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSystemAudit fMSA = new frmSystemAudit(); 
                fMSA.Show();
            }
        }


        private void mnuImpSearch_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImprovementSearch fIMPS = new frmImprovementSearch();
                fIMPS.Show();
            }
        }

        private void mnuImpFlag_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImpFlagsReview fIMPF = new frmImpFlagsReview();
                fIMPF.Show();
            }
        }

        private void mnuImpTab_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImpTabReview fIMPTR = new frmImpTabReview();
                fIMPTR.Show();
            }
        }

        private void mnuImpAccess_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCeProjectAccess fIMPAC = new frmCeProjectAccess();
                fIMPAC.Show();
            }
        }

        private void mnuImpAudit_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCeProjectAudit fIMPAU = new frmCeProjectAudit();
                fIMPAU.Show();
            }
        }

        private void mnuImpMonth_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImpMonthProc fIMPMP = new frmImpMonthProc();
                fIMPMP.Show();
            }
        }


        private void mnuImpUnlock_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImpUnlockCase fIMPUL = new frmImpUnlockCase();
                fIMPUL.Show();
            }
        }

        private void mnuMarkCase_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {      
                this.Close();
                frmImpMarkCaseReview fIMPMC = new frmImpMarkCaseReview();
                fIMPMC.Show();
            }
        }

        private void mnuMarkCaseReview_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmMarkCaseReview fMC = new frmMarkCaseReview();
                fMC.Show();
            }
        }

        private void mnuRespUpd_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmRespAddrUpdate fRU = new frmRespAddrUpdate();
                fRU.Show();
            }
        }

        private void mnuReferralReview_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmReferralReview fRFR = new frmReferralReview();
                fRFR.Show();
            }
        }

        private void mnuImpReferralReview_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImpReferralReview fIMPRR = new frmImpReferralReview();
                fIMPRR.Show();
            }
        }

        //Verify Form closing 
        public virtual bool VerifyFormClosing()
        {
            return true;
        }

        private void mnuCcmail_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCCMail fCCM = new frmCCMail();
                fCCM.Show();
            }
        }

        private void mnuTcDescription_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTCDescriptionReview fTCD = new frmTCDescriptionReview();
                fTCD.Show();
            }
        }

        private void mnuHelp_Click(object sender, EventArgs e)
        {
            //if (VerifyFormClosing())
            //{
            //    this.Close();
            //    frmHelp fH = new frmHelp();
            //    fH.Show();
            //}
        }

        private void mnuInitialsMF_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmMfInitial fMFI = new frmMfInitial();
                fMFI.Show();
            }
        }

        private void mnuMfInitialReview_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmMfInitialReview fMFIR = new frmMfInitialReview();
                fMFIR.Show();
            }
        }

        private void mnuCurrUsers_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCurrentUsers fCus = new frmCurrentUsers();
                fCus.Show();
            }
        }

        private void mnuAuthUsers_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmAuthUsers fAus = new frmAuthUsers();
                fAus.Show();
            }

        }

        private void munPrinters_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmPrinters fp = new frmPrinters();
                fp.Show();
            }
        }

        private void mnuMonProc_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmMonthlyProcess fp = new frmMonthlyProcess();
                fp.Show();
            }
        }

        private void mnuUnlock_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmUnlock fp = new frmUnlock();
                fp.Show();
            }
        }

        private void mnuWorkload_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmNPCActiveLoad fp = new frmNPCActiveLoad();
                fp.Show();
            }
        }

        private void mnuAttempts_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmNPCAttempts fp = new frmNPCAttempts();
                fp.Show();
            }
        }

        private void mnuCases_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCaseManagementReport fp = new frmCaseManagementReport();
                fp.Show();
            }
        }

        private void mnuMySectors_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmMySectors fp = new frmMySectors();
                fp.Show();
            }
        }

        private void mnuTabMonth_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTotalVip fp = new frmTotalVip();
                fp.Show();
            }
        }

        private void mnuInitialsDodge_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmDodgeInital fp = new frmDodgeInital();
                fp.Show();
            }
        }

        private void mnuDodgeInitialReview_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmDodgeInitialReview fp = new frmDodgeInitialReview();
                fp.Show();
            }
        }

        private void mnuVipMonth_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmVipReviewMon fp = new frmVipReviewMon();
                fp.Show();
            }
        }

        private void mnuVipAnnual_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmVipReviewAnn fp = new frmVipReviewAnn();
                fp.Show();
            }
        }

        private void mnuTabArtbaMonthly_Click(object sender, EventArgs e)
        {
            this.Close();
            frmArtbaMon fp = new frmArtbaMon();
            fp.Show();
        }

        private void mnuTabArtbaAnnual_Click(object sender, EventArgs e)
        {
            this.Close();
            frmArtbaAnn fp = new frmArtbaAnn();
            fp.Show();
        }

        private void mnuTabGlobalMonthly_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmGlobalInsightMon fp = new frmGlobalInsightMon();
                fp.Show();
            }
        }

        private void mnuTabGlobalAnnual_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmGlobalInsightAnn fp = new frmGlobalInsightAnn();
                fp.Show();
            }
        }

        private void mnuFlags_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmFlagTypeReport fp = new frmFlagTypeReport();
                fp.Show();
            }
        }

        private void mnuLate_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmLSF fp = new frmLSF();
                fp.Show();
            }
        }

        private void mnuBoost_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmBF fp = new frmBF();
                fp.Show();
            }
        }

        private void mnuViewTsarSeries_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTsarSeriesViewer fp = new frmTsarSeriesViewer();
                fp.Show();
            }
            
        }

        private void mnuInternalTable_Click(object sender, EventArgs e)
        {
           if (VerifyFormClosing())
           {
               this.Close();
               frmTimeInternal fp = new frmTimeInternal();
               fp.Show();
           }
        }

        private void mnuExternalTable_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTimeExternal fp = new frmTimeExternal();
                fp.Show();
            }
        }


        private void mnuSpecialPrivate_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTableSpecialPrivate fp = new frmTableSpecialPrivate();
                fp.Show();
            }
        }

        private void mnuMonthlyRelease_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTableMonRelease fp = new frmTableMonRelease();
                fp.Show();
            }
        }

        private void mnuPressRelease_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTablePressRelease fp = new frmTablePressRelease();
                fp.Show();
            }
        }

        private void mnuSeasonalFactor_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTableSeasonalFactor fp = new frmTableSeasonalFactor();
                fp.Show();
            }
        }

        private void mnuStandardError_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTableStandardErrors fp = new frmTableStandardErrors();
                fp.Show();
            }
        }

        private void mnuTCSetup_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecialPriorityTC fp = new frmSpecialPriorityTC();
                fp.Show();
            }
        }

        private void mnuUnavailableDays_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmUnavailableDays fp = new frmUnavailableDays();
                fp.Show();
            }
        }

        private void mnuScheduler_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCallSchedulerCounts fp = new frmCallSchedulerCounts();
                fp.Show();
            }
        }
        private void mnuNpSpecial_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmNPNonResSpecialCases fp = new frmNPNonResSpecialCases();
                fp.Show();
            }
        }

        private void mnuDupResearch_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmDCPDup fp = new frmDCPDup();
                fp.Show();
            }
        }

        private void mnuTabStart_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecStrt fp = new frmSpecStrt();
                fp.Show();
            }
        }
        

        private void numTabBeaSLMon_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecBEASLMon fp = new frmSpecBEASLMon();
                fp.Show();
            }
        }

        private void mnuTabBeaSLAnn_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecBEASLAnn fp = new frmSpecBEASLAnn();
                fp.Show();
            }
        }

        private void mnuTabBeadCDMon_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecBEACDMon fp = new frmSpecBEACDMon();
                fp.Show();
            }
        }

        private void mnuTabBeaCDAnn_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecBEACDAnn fp = new frmSpecBEACDAnn();
                fp.Show();
            }
        }

        private void mnuTabGeoPrivate_Click(object sender, EventArgs e)
        {
            
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecGeogPriv fp = new frmSpecGeogPriv();
                fp.Show();
            }
            
        }

        private void mnuTabGeoPublic_Click(object sender, EventArgs e)
        {
            
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecGeogPub fp = new frmSpecGeogPub();
                fp.Show();
            }
            
        }

        private void mnuTabValue_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecProjectValue fp = new frmSpecProjectValue();
                fp.Show();
            }
        }

        private void mnuTrenderData_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmBenTrender fp = new frmBenTrender();
                fp.Show();
            }
        }

        private void mnuAnnualData_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmBenAnnual fp = new frmBenAnnual();
                fp.Show();
            }
        }

        private void mnuOutputData_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmBenOutputData fp = new frmBenOutputData();
                fp.Show();
            }
        }

        private void mnuTabAnnualBea_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTabAnnualBea fp = new frmTabAnnualBea();
                fp.Show();
            }
        }

        private void mnuTabAnnualAnnual_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmTabAnnualTabs fp = new frmTabAnnualTabs();
                fp.Show();
            }
        }

        private void mnuExportData_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmBenExport fp = new frmBenExport();
                fp.Show();
            }
        }

        private void mnuRunBCF_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmBenRunBCF fp = new frmBenRunBCF();
                fp.Show();
            }
        }

        private void mnuTabLength_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecTimeLen fp = new frmSpecTimeLen();
                fp.Show();
            }
        }

        private void mnuAnnualProc_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmAnnualProcess fp = new frmAnnualProcess();
                fp.Show();
            }
        }

        private void mnuImpAnnual_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmImpAnnualProc fp = new frmImpAnnualProc();
                fp.Show();
            }
        }

        private void mnuFailedVerificationReview_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmCenturionFailedVerif fp = new frmCenturionFailedVerif();
                fp.Show();
            }
        }

        private void mnuResponseRates_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmResponseRate fp = new frmResponseRate();
                fp.Show();
            }
        }

        private void mnuNpcSpecials_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmNPCSpecials fp = new frmNPCSpecials();
                fp.Show();
            }
        }

        private void mnuhelpCPRSUserManual_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "1";
                fp.Show();
            }
        }

        private void mnuHelpCenturionUserManual_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "2";
                fp.Show();
            }
        }

        private void mnuHelpDodgeRESPIDProcedures_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "3";
                fp.Show();
            }
        }

        private void mnuHelpMultifamilyCAPIProcedures_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "4";
                fp.Show();
            }
        }

        private void mnuHelpRespondentSearchProcedures_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "5";
                fp.Show();
            }
        }

        private void mnuHelpTelephoneFollowUp_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "6";
                fp.Show();
            }
        }

        private void mnuHelpFormEntryProcedures_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "7";
                fp.Show();
            }
        }

        private void mnuHelpFederalContactNumbers_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmHelp fp = new frmHelp();
                fp.mnunum = "8";
                fp.Show();
            }
        }

        private void mnuFormlist_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmAdminFormlist fp = new frmAdminFormlist();
                fp.Show();
            }
        }

        private void monthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecManufacturingMon fp = new frmSpecManufacturingMon();
                fp.Show();
            }
        }

        private void annualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecManufacturingAnn fp = new frmSpecManufacturingAnn();
                fp.Show();
            }
        }

        private void mnuSpecialEvents_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmSpecialEvents fp = new frmSpecialEvents();
                
                fp.Show();
            }
        }

        private void mnuRobocalls_Click(object sender, EventArgs e)
        {
            if (VerifyFormClosing())
            {
                this.Close();
                frmRobocalls fp = new frmRobocalls();

                fp.Show();
            }
        }
    }
}
