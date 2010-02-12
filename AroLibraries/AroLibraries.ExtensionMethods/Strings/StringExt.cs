using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AroLibraries.ExtensionMethods.Strings
{
    public static class StringExt
    {
        #region Valid
        public static bool Ext_IsValidOneWord(this string iString)
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
        public static bool Ext_IsValidEmailAddress(this string s)
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
        public static bool Ext_IsValidGuid(this string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            Regex format = new Regex("^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2},{0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            Match match = format.Match(s);
            return match.Success;
        }
        public static bool Ext_IsValidUrl(this string text)
        {
            ///Uri temp; return Uri.TryCreate(text);
            Regex rx = new Regex(@"http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?", RegexOptions.Compiled);
            return rx.IsMatch(text);
        }

        #endregion

        #region is
        public static bool Ext_IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }


        #endregion
        public static string Ext_GetRightSideOfString(this string s, int count)
        {
            string newString = String.Empty;
            if (s != null && count > 0)
            {
                int startIndex = s.Length - count;
                if (startIndex > 0)
                    newString = s.Substring(startIndex, count);
                else
                    newString = s;
            }
            return newString;
        }
        public static int Ext_CountLinesInString(this string str)
        {
            if (str == null) throw new ArgumentNullException("String is NULL");

            int counter = 1;
            string[] strTemp = str.Split(new string[] { "\n" }, StringSplitOptions.None);
            if (strTemp.Length > 0)
            {
                counter = strTemp.Length;
            }
            return counter;
        }
        public static string Ext_Reverse(this string s)
        {
            if (s == null) throw new ArgumentNullException("String is NULL");

            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }


        public static IList<char> Ext_GetUsedChars(this string iString)
        {
            IList<char> vList = new List<char>();
            for (int i = 0; i < iString.Length; i++)
            {
                char vChar = iString[i];
                char cCharLower = Char.ToLower(vChar);
                if (!vList.Contains(cCharLower) && Char.IsLetter(cCharLower))
                {
                    vList.Add(cCharLower);
                }
            }
            return vList;
        }

    }
}
