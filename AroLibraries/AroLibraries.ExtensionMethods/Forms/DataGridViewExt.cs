using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AroLibraries.ExtensionMethods.Forms
{
    public static class DataGridViewExt
    {
        public static IList<string> Ext_GetColumnNames(this DataGridView iDataGridView)
        {
            IList<string> oColumnanames = new List<string>();
            foreach (DataGridViewColumn vColumn in iDataGridView.Columns)
            {
                string columnaName = vColumn.Name;
                oColumnanames.Add(columnaName);
            }
            return oColumnanames;
        }


    }
}
