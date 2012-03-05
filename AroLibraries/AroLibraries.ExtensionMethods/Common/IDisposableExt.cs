using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class IDisposableExt
    {
        public static void Using(this IDisposable iIDisposable, Action<IDisposable> action)
        {
            using (iIDisposable)
            {
                action(iIDisposable);
            }
        }
    }
}