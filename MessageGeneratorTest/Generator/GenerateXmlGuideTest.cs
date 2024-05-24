using MessageGeneratorSystem.Generator.Xml.General;
using MessageGeneratorSystem.Generator.Xml.Reservation;
using MessageGeneratorSystem.Generator.Xml;
using MessageGeneratorSystem.Logic;
using MessageGeneratorTest.Generator.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Abstractions;

namespace MessageGeneratorTest.Generator
{
    public class GenerateXmlGuideTest
    {
        private ITestOutputHelper _output;
        private const string TestGuidePath = @"\TestResources\TestGuide.xml";
        private const string TestSchedulePath = @"\TestResources\TestSchedule.xml";
        private const int TestFee = 100;

        public GenerateXmlGuideTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        private void CheckGenerateXmlGuide()
        {
            Reservation reservation = new(TestFee);
            reservation = reservation.AddSchedule(new DaySchedule(
                new DateTime(2024, 1, 1, 1, 1, 0),
                new DateTime(2024, 1, 1, 2, 1, 0)
                ));

            reservation = reservation.AddSchedule(new DaySchedule(
                new DateTime(2024, 2, 1, 1, 1, 0),
                new DateTime(2024, 2, 1, 2, 1, 0)
            ));

            reservation = reservation.AddSchedule(new DaySchedule(
                new DateTime(2024, 2, 6, 7, 1, 0),
                new DateTime(2024, 2, 6, 10, 1, 0)
            ));


            //予約の内容が確定したのちの処理
            GenerateXmlSchedule generateSchedule = new(Directory.GetCurrentDirectory() + TestSchedulePath);
            GenerateXmlGuide generateXmlGuide = new(generateSchedule, Directory.GetCurrentDirectory() + TestGuidePath);
            _output.WriteLine($"{generateXmlGuide.GenerateGuide(reservation)}");
        }
    }
}
