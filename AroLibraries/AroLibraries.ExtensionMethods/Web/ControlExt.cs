using System.Text;
using System.Web.UI;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AroLibraries.ExtensionMethods.Web
{
    public static class ControlExt
    {
        public static void SetFocus(this Control control) //for form
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("\r\n<script language='JavaScript'>\r\n");
            sb.Append("<!--\r\n");
            sb.Append("function SetFocus()\r\n");
            sb.Append("{\r\n");
            sb.Append("\tdocument.");

            Control p = control.Parent;
            while (!(p is System.Web.UI.HtmlControls.HtmlForm))
            {
                p = p.Parent;
            }
            sb.Append(p.ClientID);
            sb.Append("['");
            sb.Append(control.UniqueID);
            sb.Append("'].focus();\r\n");
            sb.Append("}\r\n");
            sb.Append("window.onload = SetFocus;\r\n");
            sb.Append("// -->\r\n");
            sb.Append("</script>");

            control.Page.RegisterClientScriptBlock("SetFocus", sb.ToString());
        }

        public static IEnumerable<Control> FindControl<T>(this Control iControl)
            where T : Control
        {
            //todo: ControlExt | Check this code 
            if (iControl!= null && iControl.Controls != null)
            {
                foreach (var item in iControl.Controls)
                {
                    if (item is T)
                    {
                        yield return item as T;
                    }
                }
            }
        
        }


        public static IEnumerable<Control> FindControl<T>(this Control iControl, string iRegexMatchPatternId)
                where T : Control
        {
            if (iControl != null && iControl.Controls != null)
            {
                foreach (var item in iControl.Controls)
                {
                    if (item is T)
                    {
                        var itemT = item as T;
                        if (Regex.IsMatch(itemT.ID, iRegexMatchPatternId))
                        {
                            yield return itemT;
                        }
                    }
                }
            }
        }

    }
    }
