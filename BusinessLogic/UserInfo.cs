/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : UserInfo.cs
Programmer    : Christine Zhang
Creation Date : April 29 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : User Info object
Change Request: 
Detailed Design: N/A
Rev History   : See Below
Other         : N/A
************************************************************************
Modified Date : 06/09/2016
Modified By   : Cestine Gill
Keyword       : cg060916
Change Request: na
Description   : Added Grade to user info object
************************************************************************
Modified Date : 06/22/2016
Modified By   : Kevin Montgomery
Keyword       : kjm06222016
Change Request: na
Description   : Added New EmumGroup - NPCLead
***********************************************************************
Modified Date : 11/17/2016
Modified By   : Christine
Change Request: na
Description   : Added New InitSL, InitFD, InitNR, InitMF, 
                ContSL, ContMF, ContNR, ContFD
***********************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public enum EnumGroups
    {
        Programmer     =0,
        HQManager      =1,
        HQAnalyst      =2,
        NPCManager     =3,
        NPCLead        =4,
        NPCInterviewer =5,
        HQSupport      =6,
        HQMathStat     =7,
        HQTester       =8
    }

    public static class UserInfo
    {
        public static string UserName {get; set;}
        public static EnumGroups GroupCode { get; set; }
        public static string PrinterQ { get; set; }
        public static string Grade { get; set; }  /*cg060916*/
        public static string InitSL { get; set; }
        public static string InitFD { get; set; }
        public static string InitNR { get; set; }
        public static string InitMF { get; set; }
        public static string ContSL { get; set; }
        public static string ContFD { get; set; }
        public static string ContNR { get; set; }
        public static string ContMF { get; set; }

    }
    
}
