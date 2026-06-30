/**********************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       MissingBstDataException.cs
Programmer:         Leon Mil
Creation Date:      June 26, 2026
Inputs:             Owner, NEWTC, Survey Month, Historical BST Record Count
Parameters:
Outputs:            MissingBstDataException
Description:        Custom exception thrown when the historical BST data
                    required to calculate worksheet values cannot be found.
Detailed Design:    Encapsulates worksheet context (Owner, NEWTC, Survey
                    Month, and BST record counts) so the presentation layer
                    can display a detailed, user-friendly message while
                    keeping data access and presentation concerns separate.
Other:              Intended for worksheet validation when historical BST
                    records are missing or incomplete.

Revision History:
***********************************************************************************
Modified Date :  6/30/2026
Modified By   :  Leon Mil
Keyword       :  Missing Historical BST Data
Change Request:
Description   :  Added custom exception to gracefully handle missing
                 historical BST data required for worksheet calculations.
                 Stores worksheet context as strongly typed properties so
                 the UI layer can format informative validation messages
                 without coupling presentation logic to the data access
                 layer.
***********************************************************************************/
using System;

namespace CprsDAL.Exceptions
{
    // LM 2026-06-26:
    // Exception thrown when the historical BST data required to calculate
    // worksheet values cannot be found. In addition to a standard error
    // message, the exception carries contextual information that the UI
    // layer can use to present a detailed, user-friendly message without
    // coupling the data access layer to presentation formatting.
    public class MissingBstDataException : Exception
    {
        public string Owner { get; private set; }
        public string Newtc { get; private set; }
        public string SurveyMonth { get; private set; }
        public int ExpectedRecordCount { get; private set; }
        public int ActualRecordCount { get; private set; }

        // LM 2026-06-26:
        // Initializes the exception with worksheet context needed by the
        // presentation layer to describe the missing historical BST data.
        public MissingBstDataException(
            string owner,
            string newtc,
            string surveyMonth,
            int actualRecordCount)
            : base(DefaultMessage)
        {
            Owner = owner;
            Newtc = newtc;
            SurveyMonth = surveyMonth;
            ExpectedRecordCount = 4;
            ActualRecordCount = actualRecordCount;
        }

        // LM 2026-06-26:
        // Default exception message displayed to users. Additional
        // diagnostic details are formatted by the UI layer using the
        // exception properties.
        private const string DefaultMessage =
            "The worksheet cannot be opened because required historical BST data is incomplete.";
    }
}