using MessageGeneratorSystem.Generator.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace MessageGeneratorTest.Generator.Xml
{
    public class XmlTitleTest
    {
        private ITestOutputHelper _output;
        private const string SimpleTitlePath = @"\TestResources\SimpleTitle.xml";

        public XmlTitleTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Theory]
        [InlineData(SimpleTitlePath,"")]
        private void CheckSimpleTitle(string filePath,string check)
        {
            XmlTestHelper.QuickCheckXmlGenerate(_output, XmlTestHelper.CreateSimpleElementList(), filePath, check);
        }
    }
}
