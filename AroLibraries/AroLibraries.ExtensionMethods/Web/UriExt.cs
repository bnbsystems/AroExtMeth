using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace AroLibraries.ExtensionMethods.Web
{
    public static class UriExt
    {
        public static string GetPageContent(this Uri iUri)
        {
            string PageContent = "";
            HttpWebRequest WebRequestObject = (HttpWebRequest)HttpWebRequest.Create(iUri.OriginalString);

            using (WebResponse Response = WebRequestObject.GetResponse())
            {
                using (StreamReader Reader = new StreamReader(Response.GetResponseStream()))
                {
                    PageContent = Reader.ReadToEnd();
                }
            }
            return PageContent;
        }

    }
}
