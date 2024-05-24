using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using MessageGeneratorSystem.Generator.Xml;

namespace MessageGeneratorSystem.Generator.Xml.General
{
    public class XmlText : ITextGenXmlElement
    {
        public string Key => "t";

        public XmlText() { }

        public string GenerateText(XElement element)
        {
            return element.Value;
        }
    }
}
