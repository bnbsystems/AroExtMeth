using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace AroLibraries.ExtensionMethods.Strings
{
    public static class StringExt
    {
        public static string GetRightSideOfString(this string s, int count)
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

        public static int CountLinesInString(this string str)
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

        public static string Reverse(this string s)
        {
            if (s == null) throw new ArgumentNullException("String is NULL");

            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static IList<char> GetUsedChars(this string iString)
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

        public static string ReplaceOnce(this string iString, string iOldValue, string iNewValue) //todo: test it
        {
            if (string.IsNullOrEmpty(iString) || string.IsNullOrEmpty(iOldValue)
                || iNewValue == null) throw new ArgumentException("String is NULL or Empty");

            string rString = iString;
            string vString = rString;
            if (iString.Contains(iOldValue))
            {
                var vStartindex = iString.IndexOf(iOldValue);

                rString = iString.Substring(0, vStartindex);
                rString += iNewValue;
                rString += iString.Substring(vStartindex + iOldValue.Length);
            }
            return rString;
        }

        public static string FormatWith(this string iString, params object[] iArguments)
        {
            return string.Format(iString, iArguments);
        }

        #region is

        public static bool IsMatch(this string iString, string iRegularExpression, bool matchEntirely)
        {
            return Regex.IsMatch(iString, matchEntirely ? "\\A" + iRegularExpression + "\\z" : iRegularExpression);
        }

        public static bool IsNullOrEmpty(this string str)
        {
            return String.IsNullOrEmpty(str);
        }

        public static bool IsNullOrWhitespace(this string str)
        {
            return String.IsNullOrEmpty(str) || String.IsNullOrEmpty(str.Trim());
        }

        #endregion is

        #region Convert To

        public static int ToInt(this string iString)
        {
            int rInt;
            int.TryParse(iString, out rInt);
            return rInt;
        }

        public static decimal ToDecimal(this string iString)
        {
            decimal rDecimal;
            decimal.TryParse(iString, out rDecimal);
            return rDecimal;
        }

        public static double ToDouble(this string iString)
        {
            double rDecimal;
            double.TryParse(iString, out rDecimal);
            return rDecimal;
        }

        public static bool ToBool(this string iString)
        {
            bool rBool = false;
            if (ReferenceEquals(iString, null) == false)
            {
                bool.TryParse(iString, out rBool);
            }
            return rBool;
        }

        public static DateTime ToDateTime(this string iString, DateTime defaultValue)
        {
            DateTime res;
            if (!string.IsNullOrEmpty(iString))
                return DateTime.TryParse(iString, out res) ? res : defaultValue;
            else
                return defaultValue;
        }

        public static FileInfo ToFileInfo(this string iFileString)
        {
            FileInfo rFileInfo = new FileInfo(iFileString);
            if (rFileInfo.Exists)
            {
                return rFileInfo;
            }
            throw new FileNotFoundException("NO FILE at: " + iFileString, iFileString);
        }

        public static string ToUpperFirstChar(this string iString)
        {
            if (iString == null) throw new ArgumentNullException("String is NULL");
            var chars = iString.ToCharArray();
            if (chars.Length > 0)
            {
                chars[0] = Char.ToUpper(chars[0]);
            }
            string rString = new String(chars);
            return rString;
        }

        #endregion Convert To
    }
}