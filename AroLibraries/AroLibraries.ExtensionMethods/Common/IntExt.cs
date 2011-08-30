using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using AroLibraries.ExtensionMethods.Objects;

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
            yield return end;
        }

        public static IEnumerable<int> IterateTo(this int start, int end, int every)
        {
            var diffOrder = end - start > 0 ? 1 : -1;
            var diff = diffOrder * every;
            for (var current = start; current < diffOrder * end; current += diff)
            {
                yield return current;
            }
            yield return end;
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

        public static int ToIntPossibleMax(this int iString, IEnumerable<int> possibleNumbers)
        {
            return possibleNumbers.Select(x => x.ToString()).Where(x => x.StartsWith(iString.ToString()))
                .Select(x => x.ToInt(0)).Max();
        }

        public static string ToHex(this int iInt)
        {
            return iInt.ToString("X");
        }

        #endregion ConvertTo
    }
}