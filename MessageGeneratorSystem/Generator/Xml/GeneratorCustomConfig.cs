using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml
{
    public class GeneratorCustomConfig
    {
        private readonly XDocument _xDocument;
        private const string PathKey = "path";
        private readonly string customXmlPath;

        public GeneratorCustomConfig(string customXmlPath)
        {
            _xDocument = XDocument.Load(customXmlPath, LoadOptions.PreserveWhitespace);
            this.customXmlPath = customXmlPath;
        }

        public bool AddPath(string path)
        {
            if (_xDocument.Root == null) return false;
            _xDocument.Root.Add(new XElement(PathKey, path));
            _xDocument.Save(customXmlPath);
            return true;
        }

        public IReadOnlyList<string>? GetPathList()
        {
            if (_xDocument.Root == null) return null;
            return _xDocument.Root.Elements().Select(e => e.Value).ToList();
        }

        public bool ClearConfig()
        {
            if (_xDocument.Root == null) return false;
            _xDocument.Root.RemoveAll();
            _xDocument.Save(customXmlPath);
            return true;
        }
    }
}
