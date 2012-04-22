using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AroLibraries.ExtensionMethods.Strings.Validator
{
    public static class StringExtValidator
    {
        #region Valid

        public static bool IsValidOneWord(this string iString)
        {
            if (!String.IsNullOrEmpty(iString))
            {
                string word = iString.Trim().ToLower();
                if (!String.IsNullOrEmpty(word))
                {
                    foreach (char c in word)
                    {
                        if (char.IsLetter(c) == false)
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }
            return false;
        }

        public static bool IsValidEmailAddress(this string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        /// <summary> Converts the string representation of a Guid toits Guid
        ///  equivalent. A return value indicates whether the operation succeeded.
        /// </summary>
        /// <param name="s">A string containing a Guid to convert.</param>
        /// <param name="result"> /// When this method returns, contains the Guid value equivalent to /// the Guid contained in <paramref name="s"/>, if the conversion  succeeded, or <see cref="Guid.Empty"/> if theconversion failed. /// The conversion fails if the
        /// <paramref name="s"/> parameter is a /// <see langword="null" /> reference (<see langword="Nothing" /> in /// Visual Basic), or is not of the correct format.
        /// </param> ///
        /// <value> /// <see langword="true" /> if <paramref name="s"/> was converted /// successfully; otherwise, <see langword="false" />.  </value>
        /// <exception cref="ArgumentNullException"> /// Thrown if <pararef name="s"/> is <see langword="null"/>. /// </exception>
        /// <remarks> /// Original code at https://connect.microsoft.com/VisualStudio/feedback/ViewFeedback.aspx?FeedbackID=94072&wa=wsignin1.0#tabs  </remarks>
        public static bool IsValidGuid(this string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            Regex format = new Regex("^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2},{0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            Match match = format.Match(s);
            return match.Success;
        }

        public static bool IsValidUrl(this string text)
        {
            ///Uri temp; return Uri.TryCreate(text);
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Compiled);
            return rx.IsMatch(text);
        }

        #endregion Valid

        public static string GetRegexMail()
        {
            return @"[a-zA-Z0-9.\-_]+@[a-zA-Z0-9\-.]+\.[a-zA-Z]{2,4}";
        }
    }
}