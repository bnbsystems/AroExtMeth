using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class DateTimeExt
    {
        public static bool IsWeekend(this DateTime value)
        {
            return (value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
        }

        public static string ToSimpleDate(this DateTime iDateTime) //TODO: testit
        {
            string rSimpleDate = iDateTime.ToString("yyyyMMdd");
            return rSimpleDate;
        }

        #region Iterator

        public static IEnumerable<DateTime> IterateByDayTo(this DateTime iDateTimeFrom, DateTime iDateTimeTo) //TODO: test it
        {
            DateTime rDateTime = iDateTimeFrom;
            double vAddedDayes = rDateTime < iDateTimeTo ? 1D : -1D;
            Predicate<DateTime> vPredicate = x => x <= iDateTimeTo;
            if (rDateTime >= iDateTimeTo)
            {
                vPredicate = x => x >= iDateTimeTo;
            }
            while (vPredicate(rDateTime))
            {
                yield return rDateTime;
                rDateTime = rDateTime.AddDays(vAddedDayes);
            }
        }

        public static IEnumerable<DateTime> IterateByDayFor(this DateTime iDateTimeFrom, int iCount) //TODO: test it
        {
            double vAddedDayes = iCount > 0 ? 1D : -1D;
            DateTime rDateTime = iDateTimeFrom;
            for (int iterator = 0; iterator < iCount; iterator++)
            {
                yield return rDateTime;
                rDateTime = rDateTime.AddDays(vAddedDayes);
            }
        }

        #endregion Iterator
    }
}