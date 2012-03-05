using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods.Enumerable
{
    public static class ILookupExt
    {
        //todo: ILookupExt | Check this code 
        public static int CountInternal<TKey, TElemnent>(this ILookup<TKey, TElemnent> iLookup)
        {
            int counter = 0;
            foreach (var lookup in iLookup)
            {
                int innerCount = lookup.Count();

            }

            return counter;
        }




    }
}
