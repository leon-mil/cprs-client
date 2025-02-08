/**************************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       CprsBLL.NameAddrcomments.cs	    	
Programmer:         Srini Natarajan
Creation Date:      11/16/2015
Inputs:             MCDID
Parameters:	        None 
Outputs:	        Cecomments, Cecomment, CecommentUpdateEventArgs
Description:	    This class creates the getters and setters and stores
                    the data that will be used in the improvement screen
                    include structures of commentUpdateEventArgs
Detailed Design:    None 
Other:	            Called By: frmName, , 
 
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
    public class NameAddrcomments
    {
        /*************class of NameAddrcomments ***********/

        public NameAddrcomments(string passed_id)
        {
            id = passed_id;
        }

        /*readonly property id */
        private string id;
        public string Id
        {
            get { return id; }
        }
        public List<NameAddrcomment> NameAddrcommentlist = new List<NameAddrcomment>();

    }
    
    /*************Struture of NameAddrComment ***********/

    public class NameAddrcomment
    {   
        /*Construction*/
        public NameAddrcomment()
        {
           
        }

        /*property date8 */
        private string date8;
        public string Date8
        {
            get { return date8; }
            set { date8 = value; }
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

        /*property id */
        private string id;
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

       
    }

    /************** Class CecommentUpdateEventArgs ********************/

    public class NameAddrcommentUpdateEventArgs
    {
        private string date8;
        private string comment;

        // class constructor
        public NameAddrcommentUpdateEventArgs(string sDate8, string sComment)
        {
            this.date8 = sDate8;
            this.comment = sComment;
        }

        /*property date8 */
        public string Date8
        {
            get
            {
                return date8;
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
