using System.Diagnostics;

namespace AroLibraries.ExtensionMethods.Common
{
    public static class StopwatchExt
    {
        public static void StartNew(this Stopwatch iStopwatch)
        {
            iStopwatch.Reset();
            iStopwatch.Start();
        }
    }
}