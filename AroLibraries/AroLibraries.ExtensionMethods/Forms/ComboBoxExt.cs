using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AroLibraries.ExtensionMethods.Forms
{
    public static class ComboBoxExt
    {
        public static int FillItems<T>(this ComboBox comboBox, IEnumerable<T> enumerable)
        {
            int counter = 0;
            comboBox.Items.Clear();
            foreach (var item in enumerable)
            {
                counter++;
                comboBox.Items.Add(item);
            }
            return counter;
        }
    }
}