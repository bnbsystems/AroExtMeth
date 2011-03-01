using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;

namespace AroLibraries.ExtensionMethods.Web
{
    public static class DropDownListExt
    {
        public static DropDownList FillElements(this DropDownList iDropDownList, IDictionary<string, string> iDictionary)
        {
            IEnumerable<ListItem> vListItems = iDictionary.Select(x => new ListItem(x.Key, x.Value));
            iDropDownList.Items.AddRange(vListItems.ToArray());
            return iDropDownList;
        }
    }
}