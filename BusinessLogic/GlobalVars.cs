using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public static class GlobalVars
    {
        static string databasename = "CPRSPROD";
        //static string databasename = "CPRSTEST";
        
        static int session = 0;
        static string help_dir = @"\\esmpd002fs\cprs\CPRS II\Help\";
        static string batch_dir = @"\\esmpd002fs\cprs\PROD\BATCH\";
        static string centurion_dir = @"\\esmpd002fs\cprs\PROD\DATA\CENTURION\NIGHTLY";
        
        /// <summary>
        /// Access routine for global variable.
        /// </summary>
        public static string Databasename
        {
            get
            {
                return databasename;
            }
            set
            {
                databasename = value;
            }
        }

        public static int Session
        {
            get
            {
                return session;
            }
            set
            {
                session = value;
            }
        }

        public static string HelpDir
        {
            get
            {
                return help_dir;
            }
        }

        public static string BatchDir
        {
            get
            {
                return batch_dir;
            }
        }

        public static string CenturionDir
        {
            get
            {
                return centurion_dir;
            }
        }
    }
}
