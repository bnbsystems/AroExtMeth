using log4net;

namespace AroLibraries.ExtensionMethods.Log4net
{
    public static class ILogExt
    {
        public static void DebugParams(this ILog iLog, params object[] values)
        {
            if (values != null)
            {
                foreach (var item in values)
                {
                    iLog.Debug("PARAMS:" + item);
                }
            }
        }
    }
}