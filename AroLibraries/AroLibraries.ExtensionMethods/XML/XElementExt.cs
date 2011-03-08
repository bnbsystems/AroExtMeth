using System.Xml;
using System.Xml.Linq;

namespace AroLibraries.ExtensionMethods.XML
{
    public static class XElementExt
    {
        public static XmlNode ToXmlNode(this XElement element)
        {
            using (XmlReader xmlReader = element.CreateReader())
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlReader);
                return xmlDoc;
            }
        }
    }
}