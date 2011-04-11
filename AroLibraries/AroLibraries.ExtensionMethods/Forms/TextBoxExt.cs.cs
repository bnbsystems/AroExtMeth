using System.Windows.Forms;

namespace AroLibraries.ExtensionMethods.Forms
{
    public static class TextBoxExt
    {
        public static void WriteLine(this TextBox iTextBox, string iString)
        {
            if (ReferenceEquals(iTextBox, null))
            {
                return;
            }
            iTextBox.Text += iString + "\n";
        }
    }
}