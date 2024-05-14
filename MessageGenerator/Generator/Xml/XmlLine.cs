using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace MessageGenerator.Generator.Xml
{
    public class XmlLine : IXmlText
    {
        private readonly IReadOnlyList<IXmlText> _lineElements;

        public string Key => "line";
        private const string NewLine = "\n";

        public XmlLine(IReadOnlyList<IXmlText> lineElements)
        {
            _lineElements = lineElements;
        }

        public string GenerateText(XElement element)
        {
            string result = "";
            foreach (var item in element.Elements())
            {
                IXmlText lineElement = _lineElements.FirstOrDefault(X => X.Key == item.Name);
                if (lineElement == null) continue;
                result += lineElement.GenerateText(item);
            }

            return result + NewLine;
        }
    }
}
