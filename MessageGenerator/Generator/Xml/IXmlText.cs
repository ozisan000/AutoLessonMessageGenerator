using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace MessageGenerator.Generator.Xml
{
    public interface IXmlText
    {
        public string Key { get; }
        public string GenerateText(XElement element);
    }
}
