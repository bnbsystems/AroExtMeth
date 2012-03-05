using System;

namespace AroLibraries.ExtensionMethods
{
    public static class LongExt
    {
        public static DateTime ToDateTimeFromEpoch(this long secFromEpoch)
        {
            return DateTimeExt.gDateTime_JAN_01_1970.AddSeconds(secFromEpoch);
        }




        #region Numbers
        public static long GetThousand(this long ilong)
        {
            return 1000;
        }

        public static long GetMillion(this long ilong)
        {
            return 1000000;
        }

        public static long GetBillion(this long ilong)
        {
            return 1000000000;
        }

        public static long GetTrillion(this long ilong)
        {
            return 1000000000000;
        }

        public static long GetQuadrillion(this long ilong)
        {
            return 1000000000000000;
        }

        public static long GetQulongillion(this long ilong)
        {
            return 1000000000000000000;
        }







        #endregion
    }
}