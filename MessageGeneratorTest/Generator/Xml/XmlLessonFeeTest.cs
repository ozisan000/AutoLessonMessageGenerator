using MessageGenerator.Logic;
using MessageGeneratorSystem.Generator.Xml;
using MessageGeneratorSystem.Generator.Xml.Reservation;
using Xunit.Abstractions;

namespace MessageGeneratorTest.Generator.Xml
{
    public class XmlLessonFeeTest
    {
        private ITestOutputHelper _output;
        //private const string TestGuidePath = @"\TestResources\TestGuide.xml";
        private const string TestLessonFeePath = @"\TestResources\SimpleLessonFee.xml";
        private const int TestFee = 100;
        private const string TestFeeText = "100\n";

        public XmlLessonFeeTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        private void CheckLessonFee()
        {
            Reservation reservation = new(TestFee);
            XmlLessonFee xmlLessonFee = new(reservation);

            List<ITextGenXmlElement> textGenXmlElements = new List<ITextGenXmlElement>()
            {
                xmlLessonFee
            };

            var elementList = XmlTestHelper.CreateSimpleElementList(textGenXmlElements);

            XmlTestHelper.QuickCheckXmlGenerate(
                _output,
                elementList,
                TestLessonFeePath,
                TestFeeText);
        }
    }
}
