using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using AroLibraries.ExtensionMethods.Objects;

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

        public static string Combine(this string iString, string iPathCombineWith)
        {
            return Path.Combine(iString, iPathCombineWith);
        }

        #region Format

        public static string FormatWith(this string iString, params object[] iArguments)
        {
            return string.Format(iString, iArguments);
        }

        public static string FormatWith(this string iStirng, object arg0)
        {
            return string.Format(iStirng, arg0);
        }

        public static string FormatWith(this string iStirng, object arg0, object arg1)
        {
            return string.Format(iStirng, arg0, arg1);
        }

        public static string FormatWith(this string iStirng, object arg0, object arg1, object arg2)
        {
            return string.Format(iStirng, arg0, arg1, arg2);
        }

        #endregion Format

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

        public static bool IsNumber(this string str)
        {
            return str.ToCharArray().All(x => Char.IsNumber(x));
        }

        public static bool IsDigit(this string str)
        {
            return str.ToCharArray().All(x => Char.IsDigit(x));
        }

        public static bool IsLower(this string str)
        {
            return str.ToCharArray().All(x => Char.IsLower(x));
        }

        public static bool IsUpper(this string str)
        {
            return str.ToCharArray().All(x => Char.IsUpper(x));
        }

        public static bool IsControl(this string str)
        {
            return str.ToCharArray().All(x => Char.IsControl(x));
        }

        public static bool IsContained(this string str, IEnumerable<string> strs)
        {
            foreach (var item in strs)
            {
                if (item.Contains(str))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsStringNull(this string iString)
        {
            if (ReferenceEquals(iString, null))
            {
                return true;
            }
            else if (iString.ToLower().Trim() == "null"
                    || iString.ToLower().Trim() == "nil"
                    || iString.ToLower().Trim() == "none"
                    || iString.ToLower().Trim() == "nothing"
                    )
            {
                return true;
            }
            return false;
        }

        #endregion is

        #region Convert To

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

        public static DateTime ToDateTimeEnd(this string iString)
        {
            return ToDateTimeEnd(iString, DateTime.MaxValue);
        }

        private static IEnumerable<int> gHourIterator = 0.IterateTo(23);
        private static IEnumerable<int> gMonthIterator = 1.IterateTo(12);
        private static IList<int> gDayIterator = 1.IterateTo(28).ToList();
        private static IEnumerable<int> gMinuteIterator = 0.IterateTo(59);

        public static DateTime ToDateTimeEnd(this string iString, DateTime defaultValue) //todo: test it
        {
            if (ReferenceEquals(iString, null))
            {
                return defaultValue;
            }
            var vString = iString.Trim().Replace(" ", "");
            if (vString.IsDigit())
            {
                string strYear = "";
                string strMonth = "";
                string strDay = "";
                string strHour = "";
                string strMinute = "";
                string strSecond = "";
                for (int i = 0; i < vString.Length && i < 14; i++)
                {
                    var ch = vString[i];

                    if (i.IsBetween(0, 3))
                    {
                        strYear += ch;
                    }
                    else if (i.IsBetween(4, 5))
                    {
                        strMonth += ch;
                    }
                    else if (i.IsBetween(6, 7))
                    {
                        strDay += ch;
                    }
                    else if (i.IsBetween(8, 9))
                    {
                        strHour += ch;
                    }
                    else if (i.IsBetween(10, 11))
                    {
                        strMinute += ch;
                    }
                    else if (i.IsBetween(12, 13))
                    {
                        strSecond += ch;
                    }
                    else
                    {
                    }
                }
                var intYear = strYear.PadRight(4, '9').ToInt();
                var intMonth = strMonth.ToInt().GetInRangeValue(1, 12).ToIntPossibleMax(gMonthIterator);

                if (intYear.ToDateTime().IsLeapYear() == false)
                {
                    gDayIterator.Add(29);
                }
                if (intMonth.IsOneOf(new int[] { 4, 6, 9, 11 }))
                {
                    gDayIterator.Add(30);
                }
                if (intMonth.IsOneOf(new int[] { 1, 3, 5, 7, 8, 10, 12 }))
                {
                    gDayIterator.Add(31);
                }
                var intDay = strDay.ToInt().GetInRangeValue(1, gDayIterator.Max()).ToIntPossibleMax(gDayIterator);
                var intHour = strHour.ToInt().GetInRangeValue(1, 23).ToIntPossibleMax(gHourIterator);
                var intMinute = strMinute.ToInt().GetInRangeValue(0, 59).ToIntPossibleMax(gMinuteIterator);
                var intSecond = strSecond.ToInt().GetInRangeValue(0, 59).ToIntPossibleMax(gMinuteIterator);

                var endDateTime = new DateTime(intYear, intMonth, intDay, intHour, intMinute, intSecond);

                return endDateTime;
            }
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

        public static DirectoryInfo ToDirectoryInfo(this string iDirectoryString)
        {
            DirectoryInfo rDirectoryInfo = new DirectoryInfo(iDirectoryString);
            if (rDirectoryInfo.Exists)
            {
                return rDirectoryInfo;
            }
            throw new DirectoryNotFoundException("NO DIRECTORY at: " + iDirectoryString);
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

        public static Byte[] ToByteArray(this string iString)
        {
            if (iString == null) throw new ArgumentNullException("String is NULL");
            Byte[] rData = Encoding.ASCII.GetBytes(iString);
            return rData;
        }

        public static string ToStringFromHex(this string iStringHex)
        {
            if (iStringHex == null) throw new ArgumentNullException("String is NULL");
            iStringHex = iStringHex.Replace("-", "");
            byte[] raw = new byte[iStringHex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(iStringHex.Substring(i * 2, 2), 16);
            }
            return Encoding.ASCII.GetString(raw);
        }

        public static System.Security.SecureString ToSecureString(this string iString)
        {
            System.Security.SecureString rPassWdStr = new System.Security.SecureString();
            if (iString != null)
            {
                foreach (var item in iString)
                {
                    rPassWdStr.AppendChar(item);
                }
            }

            return rPassWdStr;
        }

        public static TEnum ToEnum<TEnum>(this string iString)
            where TEnum : struct
        {
            return (TEnum)Enum.Parse(typeof(TEnum), iString, true);
        }

        #endregion Convert To

        #region Files

        public static bool TryDeleteFile(this string iFileString)
        {
            if (File.Exists(iFileString))
            {
                try
                {
                    File.Delete(iFileString);
                }
                catch (Exception)
                {
                    return false;
                }
                return true;
            }
            return false;
        }

        public static bool FileExist(this string iFileString)
        {
            if (File.Exists(iFileString))
            {
                return true;
            }
            return false;
        }

        public static byte[] ReadBytes(this string iFileStrim)
        {
            return File.ReadAllBytes(iFileStrim);
        }

        #endregion Files

        public static string Remove(this string iString, char iCharToRemove)
        {
            char ichar = CharExt.GetEmpty();
            return iString.Replace(iCharToRemove, ichar);
        }

        public static string Remove(this string iString, string iStringToRemove)
        {
            return iString.Replace(iStringToRemove, "");
        }

        public static string SubstringUntil(this string iString, int startIndexing, string iUntilString)
        {
            int untilIndex = iString.IndexOf(iUntilString);
            string vString = iString.Substring(startIndexing, untilIndex);
            return vString;
        }
    }
}