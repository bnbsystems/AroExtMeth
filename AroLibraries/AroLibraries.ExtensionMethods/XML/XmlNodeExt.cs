using System.Xml;
using System.Xml.Linq;

namespace AroLibraries.ExtensionMethods.XML
{
    public static class XmlNodeExt
    {
        public static XElement ToXElement(this XmlNode node)
        {
            XDocument xDoc = new XDocument();
            using (XmlWriter xmlWriter = xDoc.CreateWriter())
                node.WriteTo(xmlWriter);
            return xDoc.Root;
        }
    }
}