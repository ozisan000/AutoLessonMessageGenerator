using MessageGeneratorSystem.Generator.Xml.General.Title;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.General
{
    public class XmlTitle : ITextGenXmlElement
    {
        public string Key => "title";
        private readonly ITitleOrnament _titleOrnament;

        public XmlTitle()
        {
            _titleOrnament = new XmlTitleOrnament();
        }

        public XmlTitle(ITitleOrnament ornament)
        {
            _titleOrnament = ornament;
        }

        public string GenerateText(XElement element)
        {
            return _titleOrnament.GenerateTitle(element.Value);
        }
    }
}
