using System.Xml.Linq;

namespace MessageGenerator.Generator.Xml
{
    public interface ITextGenXmlElement
    {
        public string Key { get; }
        public string GenerateText(XElement element);
    }
}
