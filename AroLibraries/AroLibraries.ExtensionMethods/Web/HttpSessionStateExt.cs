using System.Web.SessionState;

namespace AroLibraries.ExtensionMethods.Web
{
    public static class HttpSessionStateExt
    {
        public static T GetValue<T>(this HttpSessionState iSession, string iNameString)
        {
            if (ReferenceEquals(iSession, null) || string.IsNullOrEmpty(iNameString))
            {
                return default(T);
            }
            T rValue = default(T);
            try
            {
                rValue = (T)iSession[iNameString];
                if (ReferenceEquals(rValue, null))
                {
                    return default(T);
                }
            }
            catch (System.Exception)
            {
                return default(T);
            }
            return rValue;
        }
    }
}