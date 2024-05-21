using System.Xml.Linq;

namespace MessageGenerator.Generator.Xml
{
    public class XmlLine : ITextGenXmlElement
    {
        private readonly IReadOnlyList<ITextGenXmlElement> _lineElements;

        public string Key => "line";
        private const string NewLine = "\n";

        public XmlLine(IReadOnlyList<ITextGenXmlElement> lineElements)
        {
            _lineElements = lineElements;
        }

        public string GenerateText(XElement element)
        {
            string result = "";
            foreach (var item in element.Elements())
            {
                ITextGenXmlElement? lineElement = _lineElements.FirstOrDefault(X => X.Key == item.Name);
                if (lineElement == null) continue;
                result += lineElement.GenerateText(item);
            }

            return result + NewLine;
        }
    }
}
