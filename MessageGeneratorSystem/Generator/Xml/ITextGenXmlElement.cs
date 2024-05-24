using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml
{
    public interface ITextGenXmlElement
    {
        public string Key { get; }
        public string GenerateText(XElement element);
    }
}
