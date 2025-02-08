using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Searchinfo
    {
        private static string screen;
        private static string type;
        private static string source;
        private static string dodgenum;
        private static string csdnum;

        private static int totalrec;
        private static int currentrec;

        public static string Screen
        {
            get { return screen; }
            set { screen = value; }
        }

        public static string Type
        {
            get { return type; }
            set { type = value; }
        }

        public static string Source
        {
            get { return source; }
            set { source = value; }
        }

        public static string Dodgenum
        {
            get { return dodgenum; }
            set { dodgenum = value; }
        }

        public static string Csdnum
        {
            get { return csdnum; }
            set { csdnum = value; }
        }

        public static int Totalrec
        {
            get { return totalrec; }
            set { totalrec = value; }
        }

        public static int Currentrec
        {
            get { return currentrec; }
            set { currentrec = value; }
        }
    }
}
