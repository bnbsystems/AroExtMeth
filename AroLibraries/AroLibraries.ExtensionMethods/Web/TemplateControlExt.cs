using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web;

namespace AroLibraries.ExtensionMethods.Web
{
    public static class TemplateControlExt
    {

        public static string GetAspxFilename(this  TemplateControl iTemplateControl)
        {
            HttpRequest vRequest = null;
            if (iTemplateControl is Page)
            {
                Page vPage = iTemplateControl as Page;
                vRequest = vPage.Request;
            }
            else if (iTemplateControl is UserControl)
            {
                UserControl vControl = iTemplateControl as UserControl;
                vRequest = vControl.Request;
            }
            if (vRequest != null)
            {
                var strTemp = vRequest.ServerVariables["SCRIPT_NAME"].ToString();
                return System.IO.Path.GetFileName(vRequest.PhysicalPath);
            }
            return null;
        }



    }
}
