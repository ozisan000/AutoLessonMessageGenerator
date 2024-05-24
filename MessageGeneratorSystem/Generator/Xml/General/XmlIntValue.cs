using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.General
{
    public abstract class XmlIntValue : ITextGenXmlElement
    {
        public abstract string Key { get; }
        protected readonly int value;
        protected readonly string format;

        public XmlIntValue(int value , string format = "")
        {
            this.format = format;
            this.value = value;
        }

        public string GenerateText(XElement element)
        {
            return value.ToString(format);
        }
    }
}
