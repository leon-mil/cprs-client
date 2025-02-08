/**********************************************************************
Econ App Name : Construction Progress Report Survey (CPRS)
Project Name  : Construction Progress Report Survey(CPRS)
Program Name  : frmMessageWait.cs
Programmer    : Christine Zhang
Creation Date : March 31 2015
Parameters    : N/A
Inputs        : N/A
Outputs       : N/A
Description   : Display please wait screen for where get data
Change Request: 
Detailed Design: N/A
Rev History   : See Below
Other         : used by Project audit screen
 ***********************************************************************
Modified Date :
Modified By   :
Keyword       :
Change Request:
Description   :
***********************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Cprs
{
    public partial class frmMessageWait : Form
    {
        public frmMessageWait()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            progressBar1.Increment(1);
            if (progressBar1.Value == 100)
            {
                timer1.Stop();
               // Close();
            }
        }

        //Public method to close the form
        public void ExternalClose()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => { ExternalClose(); }));
            }
            else
            {
                Close();
            }
        }
  
    }
}
