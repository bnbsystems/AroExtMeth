using System;
using System.Collections.Generic;

namespace AroLibraries.ExtensionMethods
{
    public static class DateTimeExt
    {
        public static bool IsWeekend(this DateTime value)
        {
            return (value.DayOfWeek == DayOfWeek.Sunday || value.DayOfWeek == DayOfWeek.Saturday);
        }

        public static bool IsLeapYear(this DateTime value)
        {
            bool rLeap = false;
            int year = value.Year;
            if ((year % 4 == 0 && year % 100 != 0) || year % 400 == 0)
            {
                rLeap = true;
            }
            return rLeap;
        }

        #region To

        public static string ToSimpleDate(this DateTime iDateTime) //TODO: testit
        {
            string rSimpleDate = iDateTime.ToString("yyyyMMdd");
            return rSimpleDate;
        }

        public static DateTime ToSpecifyKind(this DateTime iDateTime, DateTimeKind iDateTimeKind)
        {
            return DateTime.SpecifyKind(iDateTime, iDateTimeKind);
        }


        internal static readonly DateTime gDateTime_JAN_01_1970 = DateTime.SpecifyKind(new DateTime(1970, 1, 1, 0, 0, 0), DateTimeKind.Utc);

        public static DateTime GetDataTimeForEpoch
        {
            get { return gDateTime_JAN_01_1970; }
        }

        public static long ToSecondsFromEpoch(this DateTime iDateTime)
        {
            DateTime dt = iDateTime.ToUniversalTime();
            TimeSpan ts = dt.Subtract(gDateTime_JAN_01_1970);
            return (long)ts.TotalSeconds;
        }

        #endregion To

        #region EqualsTo

        public static bool EqualsToYear(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.Year == iDateTimeTo.Year)
            {
                return true;
            }
            return false;
        }

        public static bool EqualsToMonth(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.EqualsToYear(iDateTimeTo) && iDateTime.Month == iDateTimeTo.Month)
            {
                return true;
            }
            return false;
        }

        public static bool EqualsToDay(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.EqualsToMonth(iDateTimeTo) && iDateTime.Day == iDateTimeTo.Day)
            {
                return true;
            }
            return false;
        }

        public static bool EqualsToHour(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.EqualsToDay(iDateTimeTo) && iDateTime.Hour == iDateTimeTo.Hour)
            {
                return true;
            }
            return false;
        }

        public static bool EqualsToMinute(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.EqualsToHour(iDateTimeTo) && iDateTime.Minute == iDateTimeTo.Minute)
            {
                return true;
            }
            return false;
        }

        public static bool EqualsToSecond(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.EqualsToMinute(iDateTimeTo) && iDateTime.Second == iDateTimeTo.Second)
            {
                return true;
            }
            return false;
        }

        public static bool EqualsToMillisecond(this DateTime iDateTime, DateTime iDateTimeTo)
        {
            if (iDateTime.EqualsToSecond(iDateTimeTo) && iDateTime.Millisecond == iDateTimeTo.Millisecond)
            {
                return true;
            }
            return false;
        }

        #endregion EqualsTo

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