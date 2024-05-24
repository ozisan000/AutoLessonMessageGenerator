using MessageGenerator.Generator.Xml;
using Xunit.Abstractions;
using System.Xml.Linq;

namespace MessageGeneratorTest.Generator.Xml
{
    public class XmlTextTest
    {
        private ITestOutputHelper _output;
        //private readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private const string SimpleTextXmlPath = @"\TestResources\SimpleText.xml";

        private const string SimpleText = "SimpleElement";

        public XmlTextTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void SimpleTextTest()
        {
            XDocument xml = XmlTestHelper.LoadXml(SimpleTextXmlPath,_output);
            XmlText xmlText = new XmlText();
            foreach (var element in xml.Elements())
            {
                _output.WriteLine($"element : <{element.Name}>{element.Value}</{element.Name}>");
                string result = xmlText.GenerateText(element);
                Assert.Equal(SimpleText, result);
                _output.WriteLine($"Generate:{result}");
            }
        }
    }
}