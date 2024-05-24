using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.General
{
    public class XmlLine : ITextGenXmlElement
    {
        private readonly IReadOnlyList<ITextGenXmlElement> _lineElements;

        public string Key => "l";
        private const string NewLine = "\n";

        public XmlLine(IReadOnlyList<ITextGenXmlElement> elements)
        {
            _lineElements = elements;
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
