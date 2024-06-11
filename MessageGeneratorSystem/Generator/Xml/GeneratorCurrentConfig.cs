using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml
{
    public class GeneratorCurrentConfig
    {
        private XDocument _xDocument;
        private readonly string currentXmlPath;
        private const string PathKey = "path";

        public string CurrentPath
        {
            get
            {
                if (_xDocument.Root == null) return "";
                var result = _xDocument.Root.Elements().FirstOrDefault(x => x.Name == PathKey);
                if (result == null) return "";
                return result.Value;
            }

            set
            {
                if (_xDocument.Root == null) return;
                var result = _xDocument.Root.Elements().FirstOrDefault(x => x.Name == PathKey);
                if (result == null) {
                    _xDocument.Root.Add(new XElement(PathKey, value));
                }
                else
                {
                    result.Value = value;
                }
                _xDocument.Save(currentXmlPath);
            }
        }

        public GeneratorCurrentConfig(string currentPath)
        {
            this.currentXmlPath = currentPath;
            _xDocument = XDocument.Load(currentXmlPath, LoadOptions.PreserveWhitespace);
        }

        private XElement? GetPathElement()
        {
            if (_xDocument.Root == null) return null;
            return _xDocument.Root.Elements().FirstOrDefault(x => x.Name == PathKey);
        }
    }
}
