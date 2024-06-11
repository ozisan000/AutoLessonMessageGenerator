using MessageGeneratorSystem.Logic;
using MessageGeneratorSystem.Generator.Xml;
using MessageGeneratorSystem.Generator.Xml.General;
using MessageGeneratorSystem.Generator.Xml.Reservation;
using Xunit.Abstractions;

namespace MessageGeneratorTest.Generator.Xml
{
    public class XmlSchedulesTest
    {
        private ITestOutputHelper _output;
        private const string TestGuidePath = @"\TestResources\TestGuide.xml";
        private const string TestSchedulePath = @"\TestResources\TestSchedule.xml";
        private const int TestFee = 100;

        public XmlSchedulesTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        private void CheckSchedules()
        {
            Reservation reservation = new(TestFee);
            //reservation = reservation.AddSchedule(new DaySchedule(
            //    new DateTime(2024, 1, 1, 1, 1, 0),
            //    new DateTime(2024, 1, 1, 2, 1, 0)
            //    ));

            //予約の内容が確定したのちの処理
            GenerateXmlSchedule generateSchedule = new(Directory.GetCurrentDirectory() + TestSchedulePath);

            XmlLessonFee xmlLessonFee = new(reservation);
            XmlTotalLessonFee xmlTotalLessonFee = new(reservation);
            XmlTotalLessonCount xmlTotalLessonCount = new(reservation);
            XmlSchedules xmlSchedules = new(reservation, generateSchedule);

            XmlText xmlText = new XmlText();
            XmlTitle xmlTitle = new XmlTitle();
            List<ITextGenXmlElement> xmlElements = new List<ITextGenXmlElement>()
            {
                xmlTotalLessonFee,
                xmlTotalLessonCount,
                xmlLessonFee,
                xmlSchedules,
                xmlText,
                xmlTitle
            };
            XmlLine xmlLine = new XmlLine(xmlElements);
            xmlElements.Add(xmlLine);

            XmlTestHelper.QuickCheckXmlGenerate(
                _output,
                xmlElements,
                TestGuidePath);
        }
    }
}
