using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using CprsBLL;
using CprsDAL;

namespace Cprs
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.ThreadException += applicationThreadException;

            // Set the unhandled exception mode to force all Windows Forms 
            // errors to go through our handler.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            AppDomain.CurrentDomain.UnhandledException += currentDomainUnhandledException;
            
            // Handle the ApplicationExit event to know when the application is exiting.
            Application.ApplicationExit += new EventHandler(OnApplicationExit);


            Application.Run(new frmWelcome());
        }

        private static void OnApplicationExit(object sender, EventArgs e)
        {
            try
            {
                CurrentUsersData cuData = new CurrentUsersData();
                cuData.DeleteCurrentUsersData();
            }
            catch { }
        }


        private static void currentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            handleException(e.ExceptionObject as Exception);
        }

        private static void applicationThreadException(object sender, ThreadExceptionEventArgs e)
        {
            handleException(e.Exception);
        }

        private static void handleException(Exception exception)
        {
            CurrentUsersData cuData = new CurrentUsersData();
            cuData.DeleteCurrentUsersData();

            using (var form = new frmError(exception))
            {
                var result = form.ShowDialog();

                if (result == DialogResult.OK)
                {
                    Application.Exit();
                }
            }
        }
    }
}
