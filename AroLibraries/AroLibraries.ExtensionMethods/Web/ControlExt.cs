using System.Text;
using System.Web.UI;

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
    }
}