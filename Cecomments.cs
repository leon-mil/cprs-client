/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.Cecomments.cs	    	
Programmer:         Christine Zhang
Creation Date:      08/4/2015
Inputs:             MCDID
Parameters:	        None 
Outputs:	        Cecomments, Cecomment, CecommentUpdateEventArgs
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen
                    include structures of Cecomments, Cecomment, CecommentUpdateEventArgs
Detailed Design:    None 
Other:	            Called By: frmImprovement, , frmCeHistoryPopup
 
Revision History:	
*********************************************************************
 Modified Date :  
 Modified By   :  
 Keyword       :  
 Change Request:  
 Description   :  
********************************************************************* 
****************************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CprsBLL
{
    public class Cecomments
    {
        /*************class of Cecomments ***********/

        public Cecomments(string passed_id)
        {
            id = passed_id;
        }

        /*readonly property id */
        private string id;
        public string Id
        {
            get { return id; }
        }
        public List<Cecomment> Cecommentlist = new List<Cecomment>();

    }
    
    /*************Struture of Cecomment ***********/

    public class Cecomment
    {   
        /*Construction*/
        public Cecomment()
        {
           
        }

        /*property commdate */
        private string commdate;
        public string Commdate
        {
            get { return commdate; }
            set { commdate = value; }
        }

        /*property commtime */
        private string commtime;
        public string Commtime
        {
            get { return commtime; }
            set { commtime = value; }
        }

        /*property usrnme */
        private string usrnme;
        public string Usrnme
        {
            get { return usrnme; }
            set { usrnme = value; }
        }

        /*property commtext */
        private string commtext;
        public string Commtext
        {
            get { return commtext; }
            set { commtext = value; }
        }

       
    }

    /************** Class CecommentUpdateEventArgs ********************/

    public class CecommentUpdateEventArgs
    {
        private string commdate;
        private string comment;

        // class constructor
        public CecommentUpdateEventArgs(string sDate8, string sComment)
        {
            this.commdate = sDate8;
            this.comment = sComment;
        }

        /*property commdate */
        public string Commdate
        {
            get
            {
                return commdate;
            }
        }

        /*property comment */
        public string Comment
        {
            get
            {
                return comment;
            }
        }

    }
}
