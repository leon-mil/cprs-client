/************************************************************************
Econ App Name:      CPRS
Project Name:       CPRS Interactive Screens System
Program Name:       MonthlyProcessingSelector.cs
Programmer:         Leon Mil
Creation Date:      11/20/2024

Inputs:             Application configuration values:
                        - AppSettings["MonthlyProcessingMode"]
                        - AppSettings["MonthlyProcessingCustomDate"]
Parameters:         None (configuration-driven; no runtime parameters)
Outputs:            MonthlyProcessingContext.StatisticalPeriod (yyyyMM)

Description:        Centralizes selection of the active monthly statistical
                    period used by CPRS. The selector inspects configuration
                    flags to decide whether to use the current calendar month
                    ("Normal" mode) or a configured override month
                    ("Shutdown" mode) for reruns and backfills.

Detailed Design:    - Reads AppSettings["MonthlyProcessingMode"]:
                        * "Normal"   → use DateTime.Today (current month)
                        * "Shutdown" → use AppSettings["MonthlyProcessingCustomDate"]
                      (case-insensitive; defaults to "Normal" if not set)

                    - When "Shutdown" is selected, reads
                      AppSettings["MonthlyProcessingCustomDate"] and parses it
                      as a date using one of the following formats:
                        * Full date (any DateTime-parsable format)
                        * "yyyyMM"   (uses day = 1)
                        * "yyyyMMdd" (uses exact day)

                    - The resolved DateTime is converted to a "yyyyMM" string
                      and stored in MonthlyProcessingContext.StatisticalPeriod
                      for use by downstream database operations and batch jobs.

Other:              Consumed by:
                        - frmMonthlyProcess
                        - frmMonthlyProcessPopup

Revision History:
*********************************************************************
Modified Date   :   
Modified By     :   
Keyword         :   
Change Request  :   
Description     :   
                   
*********************************************************************/

using System;
using System.Configuration;

namespace CprsBLL
{
    /// <summary>
    /// Encapsulates the resolved monthly statistical period used by CPRS.
    /// </summary>
    public class MonthlyProcessingContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MonthlyProcessingContext"/> class.
        /// </summary>
        /// <param name="statisticalPeriod">
        /// The active statistical period in yyyyMM format.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when <paramref name="statisticalPeriod"/> is null, empty, or whitespace.
        /// </exception>
        public MonthlyProcessingContext(string statisticalPeriod)
        {
            if (string.IsNullOrWhiteSpace(statisticalPeriod))
            {
                throw new ArgumentException("Statistical period is required.", nameof(statisticalPeriod));
            }

            StatisticalPeriod = statisticalPeriod;
        }

        /// <summary>
        /// Gets the active monthly statistical period in yyyyMM format.
        /// </summary>
        public string StatisticalPeriod { get; }
    }

    /// <summary>
    /// Defines the supported processing modes for selecting the active statistical period.
    /// </summary>
    public static class MonthlyProcessingModes
    {
        /// <summary>
        /// Indicates that the system should use the current calendar month
        /// (based on <see cref="DateTime.Today"/>) as the processing period.
        /// </summary>
        public const string ModeNormal = "Normal";

        /// <summary>
        /// Indicates that the system should use a custom month configured in
        /// application settings (AppSettings["MonthlyProcessingCustomDate"]).
        /// </summary>
        public const string ModeShutdown = "Shutdown";
    }

    /// <summary>
    /// Resolves the statistical period for monthly processing based on
    /// application configuration values.
    /// </summary>
    /// <remarks>
    /// Reads the following appSettings:
    /// <list type="bullet">
    /// <item>
    /// <description>
    /// <c>MonthlyProcessingMode</c>: "Normal" (default) or "Shutdown".
    /// </description>
    /// </item>
    /// <item>
    /// <description>
    /// <c>MonthlyProcessingCustomDate</c>: required when mode is "Shutdown".
    /// Supports DateTime-parsable formats, "yyyyMM", or "yyyyMMdd".
    /// </description>
    /// </item>
    /// </list>
    /// Returns a <see cref="MonthlyProcessingContext"/> whose
    /// <see cref="MonthlyProcessingContext.StatisticalPeriod"/> is formatted as yyyyMM.
    /// </remarks>
    public static class MonthlyProcessingSelector
    {
        private const string ModeKey = "MonthlyProcessingMode";
        private const string CustomDateKey = "MonthlyProcessingCustomDate";

        /// <summary>
        /// Resolves the current <see cref="MonthlyProcessingContext"/> based on
        /// the configured monthly processing mode and optional custom date.
        /// </summary>
        /// <returns>
        /// A <see cref="MonthlyProcessingContext"/> representing the active
        /// statistical period in yyyyMM format.
        /// </returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Thrown when:
        /// <list type="bullet">
        /// <item><description>
        /// <c>MonthlyProcessingMode</c> is not "Normal" or "Shutdown".
        /// </description></item>
        /// <item><description>
        /// Mode is "Shutdown" but <c>MonthlyProcessingCustomDate</c> is missing
        /// or invalid.
        /// </description></item>
        /// </list>
        /// </exception>
        public static MonthlyProcessingContext GetCurrentContext()
        {
            var mode = (ConfigurationManager.AppSettings[ModeKey] ?? MonthlyProcessingModes.ModeNormal).Trim();

            if (string.Equals(mode, MonthlyProcessingModes.ModeShutdown, StringComparison.OrdinalIgnoreCase))
            {
                var configuredDate = ConfigurationManager.AppSettings[CustomDateKey];
                var overrideDate = ParseConfiguredDate(configuredDate);
                return new MonthlyProcessingContext(overrideDate.ToString("yyyyMM"));
            }

            if (!string.Equals(mode, MonthlyProcessingModes.ModeNormal, StringComparison.OrdinalIgnoreCase))
            {
                throw new ConfigurationErrorsException(
                    $"MonthlyProcessingMode must be either '{MonthlyProcessingModes.ModeNormal}' or '{MonthlyProcessingModes.ModeShutdown}'.");
            }

            var today = DateTime.Today;
            return new MonthlyProcessingContext(today.ToString("yyyyMM"));
        }

        /// <summary>
        /// Parses the configured custom date value into a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="configuredDate">
        /// The configured date string from <c>MonthlyProcessingCustomDate</c>.
        /// Expected formats:
        /// <list type="bullet">
        /// <item><description>
        /// Any string directly parsable by <see cref="DateTime.TryParse(string, out DateTime)"/>.
        /// </description></item>
        /// <item><description>
        /// "yyyyMM" → returned as the first day of that month.
        /// </description></item>
        /// <item><description>
        /// "yyyyMMdd" → returned as the exact year, month, and day.
        /// </description></item>
        /// </list>
        /// </param>
        /// <returns>A <see cref="DateTime"/> representing the override date.</returns>
        /// <exception cref="ConfigurationErrorsException">
        /// Thrown when the value is missing or cannot be parsed using the
        /// supported formats.
        /// </exception>
        private static DateTime ParseConfiguredDate(string configuredDate)
        {
            if (string.IsNullOrWhiteSpace(configuredDate))
            {
                throw new ConfigurationErrorsException(
                    "MonthlyProcessingCustomDate must be provided when MonthlyProcessingMode is set to Shutdown.");
            }

            if (DateTime.TryParse(configuredDate, out var parsedDate))
            {
                return parsedDate;
            }

            if (configuredDate.Length == 6 &&
                int.TryParse(configuredDate.Substring(0, 4), out var year) &&
                int.TryParse(configuredDate.Substring(4, 2), out var month))
            {
                return new DateTime(year, month, 1);
            }

            if (configuredDate.Length == 8 &&
                int.TryParse(configuredDate.Substring(0, 4), out year) &&
                int.TryParse(configuredDate.Substring(4, 2), out month) &&
                int.TryParse(configuredDate.Substring(6, 2), out var day))
            {
                return new DateTime(year, month, day);
            }

            throw new ConfigurationErrorsException(
                "MonthlyProcessingCustomDate must be a valid date (e.g. 2024-03-01, 202403, or 20240301).");
        }
    }
}
