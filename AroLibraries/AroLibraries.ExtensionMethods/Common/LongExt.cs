using System;

namespace AroLibraries.ExtensionMethods
{
    public static class LongExt
    {
        public static DateTime ToDateTimeFromEpoch(this long secFromEpoch)
        {
            return DateTimeExt.gDateTime_JAN_01_1970.AddSeconds(secFromEpoch);
        }
    }
}