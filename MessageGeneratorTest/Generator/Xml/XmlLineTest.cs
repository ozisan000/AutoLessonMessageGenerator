using MessageGenerator.Generator.Xml;
using Xunit.Abstractions;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using MessageGeneratorSystem.Generator.Xml;

namespace MessageGeneratorTest.Generator.Xml
{
    public class XmlLineTest
    {
        private ITestOutputHelper _output;
        private const string SimpleTextXmlPath = @"\TestResources\SimpleLine.xml";
        private const string TextAndLineXmlPath = @"\TestResources\TextAndLine.xml";
        private const string TextScheduleXmlPath = @"\TestResources\TestSchedule.xml";
        private const string TextGuideXmlPath = @"\TestResources\TestGuide.xml";

        private const string SimpleLineText = "SimpleElement1 SimpleElement2\n";
        private const string TextAndLineText = "SimpleElement1 SimpleElement2\nSimpleElement3 SimpleElement4\n";

        public XmlLineTest(ITestOutputHelper output)
        {
            _output = output;
            //_output.WriteLine(CurrentDirectory);
        }

        //実装していないクラスもあるためスキップさせる
        //あくまでテキストと改行を組み合わせたロジックがうまく作動しているかチェック
        [Theory]
        [InlineData(SimpleLineText, SimpleTextXmlPath)]
        [InlineData(TextAndLineText, TextAndLineXmlPath)]
        [InlineData("", TextScheduleXmlPath)]
        [InlineData("", TextGuideXmlPath)]
        public void CheckGenerateText(string check,string filePath)
        {
            XmlTestHelper.QuickCheckXmlGenerate(
                _output,
                XmlTestHelper.CreateSimpleElementList(),
                filePath,
                check);
        }
    }
}
