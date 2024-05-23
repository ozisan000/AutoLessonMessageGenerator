using MessageGenerator.Generator.Xml;
using Xunit.Abstractions;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;

namespace MessageGeneratorTest.Generator.Xml
{
    public class XmlLineTest
    {
        private ITestOutputHelper _output;
        private readonly string CurrentDirectory = Directory.GetCurrentDirectory();
        private const string SimpleTextXmlPath = @"\TestResources\SimpleLine.xml";
        private const string TextAndLineXmlPath = @"\TestResources\TextAndLine.xml";
        private const string TextScheduleXmlPath = @"\TestResources\TestSchedule.xml";
        private const string TextGuideXmlPath = @"\TestResources\TestGuide.xml";

        private const string SimpleLineText = "SimpleElement1 SimpleElement2\n";
        private const string TextAndLineText = "SimpleElement1 SimpleElement2\nSimpleElement3 SimpleElement4\n";

        public XmlLineTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine(CurrentDirectory);
        }

        [Theory]
        [InlineData(SimpleLineText, SimpleTextXmlPath)]
        [InlineData(TextAndLineText, TextAndLineXmlPath)]
        [InlineData("", TextScheduleXmlPath)]
        [InlineData("", TextGuideXmlPath)]
        public void CheckGenerateText(string check,string filePath)
        {
            XDocument xml = LoadXml(filePath);
            var xmlElementList = CreateElementList();
            string result = "";
            Assert.NotNull(xml.Root);
            foreach (var element in xml.Root.Elements())
            {
                ITextGenXmlElement? genElement = xmlElementList.Where(x => x.Key == element.Name).FirstOrDefault();
                if (genElement == null) continue;
                string text = genElement.GenerateText(element);
                result += text;
                _output.WriteLine($"--Generate Added--\n{text}");
            }
            if (check != "") Assert.Equal(check, result);
            _output.WriteLine($"------Result Generate------ \n{result}");
        }


        [Fact]
        private IReadOnlyList<ITextGenXmlElement> CreateElementList()
        {
            XmlText xmlText = new XmlText();
            List<ITextGenXmlElement> textElementList = new List<ITextGenXmlElement>() { xmlText };

            XmlLine xmlLine = new XmlLine(textElementList);
            List<ITextGenXmlElement> xmlElementList = new() {
                xmlText,
                xmlLine
            };

            return xmlElementList;
        }

        private XDocument LoadXml(string file)
        {
            return XDocument.Load(CurrentDirectory + file, LoadOptions.PreserveWhitespace);
        }
    }
}
