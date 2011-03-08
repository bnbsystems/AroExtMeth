﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class IntExt
    {
        #region Iterator

        public static IEnumerable<int> IterateTo(this int start, int end)
        {
            var diff = end - start > 0 ? 1 : -1;
            for (var current = start; current != end; current += diff)
            {
                yield return current;
            }
        }

        public static IEnumerable<int> IterateFor(this int start, int count)
        {
            if (count < 0) yield break;
            int iterator = 0;
            while (iterator < count)
            {
                yield return start;
                iterator++;
                start++;
            }
        }

        #endregion Iterator

        public static bool IsEven(this int iInt)
        {
            if ((iInt % 2) == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsPositive(this int iInt)
        {
            if (iInt > 0)
                return true;
            return false;
        }

        #region ConvertTo

        public static string ToMonthName(this int iMountNumber, string iDefaultMonthName)
        {
            if (iMountNumber > 0 && iMountNumber < 13)
            {
                string rMonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(iMountNumber);
                return rMonthName;
            }
            else
            {
                return iDefaultMonthName;
            }
        }

        public static bool ToBool(this int i)
        {
            if (i > 0)
                return true;
            return false;
        }

        #endregion ConvertTo
    }
}