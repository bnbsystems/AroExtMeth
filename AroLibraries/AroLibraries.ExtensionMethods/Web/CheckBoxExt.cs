using System.Web.UI.WebControls;

namespace AroLibraries.ExtensionMethods.Web
{
    public static class CheckBoxExt
    {
        public static bool SetReadOnly(this CheckBox iCheckBox, bool isReadonly)
        {
            if (isReadonly)
            {
                iCheckBox.Attributes.Add("onclick", "return false;");
                return true;
            }
            else
            {
                //todo: del onclick atribute
                return true;
            }
            return false;
        }
    }
}