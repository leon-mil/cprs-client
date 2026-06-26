/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       MissingBstDataException.cs
Programmer:         Leon Mil
Creation Date:      June 26, 2026
Inputs:             Owner, TC, Survey Month, Historical BST Record Count
Parameters:
Outputs:
Description:        Custom exception thrown when the required historical BST
                    records cannot be found for a Federal worksheet.
Detailed Design:    Enables the UI layer to display a meaningful error message
                    and terminate the worksheet gracefully instead of allowing
                    an unhandled ArgumentOutOfRangeException to occur.
Other:

Revision History:
***********************************************************************************
Modified Date :  6/26/2026
Modified By   :  Leon Mil
Keyword       :  Missing BST History
Change Request:
Description   :  Added custom exception to handle missing historical BSTSAV
                 records required for Federal worksheets. This allows the
                 application to notify the user of the missing data and close
                 the worksheet gracefully instead of terminating with an
                 ArgumentOutOfRangeException.
***********************************************************************************/
using System;

namespace CprsDAL.Exceptions
{
    public class MissingBstDataException : Exception
    {
        public MissingBstDataException(
            string owner,
            string newtc,
            string sdate,
            int count)
            : base(
                $"Missing BSTSAV data. Expected 4 prior-month BST records but found {count}. " +
                $"Owner={owner}, TC={newtc}, sdate={sdate}.")
        { }
    }
}
