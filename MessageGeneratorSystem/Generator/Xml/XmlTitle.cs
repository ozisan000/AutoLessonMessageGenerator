using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MessageGeneratorSystem.Generator.Xml.Title;

namespace MessageGeneratorSystem.Generator.Xml
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
