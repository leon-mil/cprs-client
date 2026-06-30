/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       frmCprsMessageBox.cs
Programmer:         Leon Mil
Creation Date:      June 26, 2026
Inputs:             Owner Form, Message, Title, MessageBoxIcon
Parameters:
Outputs:            DialogResult
Description:        Displays a CPRS-themed message dialog that provides a
                    consistent user interface for informational, warning,
                    and error messages throughout the application.
Detailed Design:    Wraps the standard Windows Forms dialog functionality
                    inside a reusable CPRS dialog that supports custom
                    titles, messages, icons, and inherits the parent
                    form's application icon for a consistent user
                    experience.
Other:              Intended to replace standard MessageBox.Show() calls
                    throughout the CPRS application.

Revision History:
***********************************************************************************
Modified Date :  6/30/2026
Modified By   :  Leon Mil
Keyword       :  CPRS Message Dialog
Change Request:
Description   :  Added reusable CPRS-themed message dialog to provide a
                 consistent look and feel for application validation,
                 warning, informational, and error messages. Supports
                 MessageBox.Show()-style overloads to simplify migration
                 from the standard Windows Forms message box.
***********************************************************************************/

using System;
using System.Windows.Forms;

namespace Cprs
{
    public partial class frmCprsMessageBox : Form
    {
        public frmCprsMessageBox()
        {
            InitializeComponent();
        }

        // LM 2026-06-26:
        // Creates a CPRS-themed message dialog that provides a consistent
        // appearance throughout the application. The dialog displays a
        // caller-supplied title, message, and icon while inheriting the
        // parent form's icon for a consistent user experience.
        internal frmCprsMessageBox(
            Form owner,
            string message,
            string title,
            MessageBoxIcon icon)
            : this()
        {            
            lblTitle.Text = title;
            lblMessage.Text = message;
            lblIcon.Text = GetIconText(icon);

            if (owner != null && owner.Icon != null)
            {
                Icon = owner.Icon;
            }

            AcceptButton = btnOk;
            CancelButton = btnOk;
            btnOk.Focus();
        }

        // LM 2026-06-26:
        // Returns a simple text representation of the requested message
        // box icon. The dialog uses text instead of system icons to
        // maintain a consistent appearance across the CPRS application.
        private static string GetIconText(MessageBoxIcon icon)
        {
            switch (icon)
            {
                case MessageBoxIcon.Error:
                    return "X";
                case MessageBoxIcon.Warning:
                    return "!";
                case MessageBoxIcon.Information:
                    return "i";
                case MessageBoxIcon.Question:
                    return "?";
                default:
                    return "!";
            }
        }

        // LM 2026-06-26:
        // Displays the CPRS message dialog using the specified owner,
        // message, title, and icon. This method mirrors the standard
        // MessageBox.Show() API to simplify migration from the built-in
        // Windows Forms message box.
        public static DialogResult Show(
            Form owner,
            string message,
            string title,
            MessageBoxIcon icon)
        {
            using (frmCprsMessageBox popup =
                new frmCprsMessageBox(owner, message, title, icon))
            {
                return popup.ShowDialog(owner);
            }
        }

        // LM 2026-06-26:
        // Convenience overload that displays an informational message
        // when a specific icon is not supplied by the caller.
        public static DialogResult Show(
            Form owner,
            string message,
            string title)
        {
            return Show(owner, message, title, MessageBoxIcon.Information);
        }

        // LM 2026-06-26:
        // Convenience overload that displays an informational message
        // when a specific icon is not supplied by the caller.
        public static DialogResult Show(
            string message,
            string title)
        {
            return Show(null, message, title, MessageBoxIcon.Information);
        }
    }
}