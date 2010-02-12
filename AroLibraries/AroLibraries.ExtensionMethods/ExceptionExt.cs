using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AroLibraries.ExtensionMethods
{
    public static class ExceptionExt
    {
        public static Exception Ext_GetMostInner(this Exception ex)
        {
            Exception ActualInnerEx = ex;

            while (ActualInnerEx != null)
            {
                ActualInnerEx = ActualInnerEx.InnerException;
                if (ActualInnerEx != null)
                    ex = ActualInnerEx;
            }
            return ex;
        }
    }
}
