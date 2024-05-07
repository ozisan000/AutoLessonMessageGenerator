using Xunit.Abstractions;
using MessageGenerator.Logic;
using MessageGenerator.Generator;


namespace AutoSchoolMessageGeneratorTest.Logic
{
    using static GuideGenerateTestHelper;

    public class GuideGenerateUnitTest
    {
        private readonly ITestOutputHelper _output;
        private const int TestCorrectFee = 100;
        private const int TestWrongFee = 0;
        private const string testScheduleMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\MessageGeneratorWPFandModel\\TestScheduleMarkUp.txt";
        private const string testGuideMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\MessageGeneratorWPFandModel\\TestGuideMarkUp.txt";
        private const string TestDummyDirectory = "G:\\Dummy\\a.txt";
        private const string TestDummyMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\MessageGeneratorWPFandModel\\dummy";
        private const string TestDummyMarkUpFile = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\MessageGeneratorWPFandModel\\DummyMarkUp.txt";

        public GuideGenerateUnitTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("------Start {0}------\n", nameof(GuideGenerateUnitTest));
        }

        [Fact]
        public void CreateReservation()
        {
            //Test instancing class.
            Reservation testReservation;
            Assert.Throws<ArgumentOutOfRangeException>(() => testReservation = new Reservation(TestWrongFee));
            testReservation = new Reservation(TestCorrectFee);
            Assert.NotNull(testReservation);
            _output.WriteLine(ReservationInfo(testReservation));
        }

        [Fact]
        public void CreateDateItemGenerator()
        {
            DateItemGenerator dateItemGenerator;
            Assert.Throws<ArgumentException>(() => dateItemGenerator = new DateItemGenerator(""));
            Assert.Throws<DirectoryNotFoundException>(() => dateItemGenerator = new DateItemGenerator(TestDummyDirectory));
            Assert.Throws<FileNotFoundException>(() => dateItemGenerator = new DateItemGenerator(TestDummyMarkUpPath));
            Assert.Throws<FormatException>(() => dateItemGenerator = new DateItemGenerator(TestDummyMarkUpFile));

            dateItemGenerator = new DateItemGenerator(testScheduleMarkUpPath);
            Assert.NotNull(dateItemGenerator);
        }

        [Fact]
        public void CreateGuideGenerator()
        {
            DateItemGenerator dateItemGenerator = new DateItemGenerator(testScheduleMarkUpPath);

            GuideGenerator guideGenerator;
            Assert.Throws<ArgumentException>(() => guideGenerator = new GuideGenerator(dateItemGenerator, ""));
            Assert.Throws<DirectoryNotFoundException>(() => guideGenerator = new GuideGenerator(dateItemGenerator, TestDummyDirectory));
            Assert.Throws<FileNotFoundException>(() => guideGenerator = new GuideGenerator(dateItemGenerator, TestDummyMarkUpPath));
            Assert.Throws<FormatException>(() => guideGenerator = new GuideGenerator(dateItemGenerator, TestDummyMarkUpFile));

            guideGenerator = new GuideGenerator(dateItemGenerator, testGuideMarkUpPath);
            Assert.NotNull(guideGenerator);
        }

        [Fact]
        public void CreateSchedule()
        {
            var throwYear = (CreateSimpleHour(2024, 1, 1, 1), CreateSimpleHour(1999, 1, 1, 2));
            var throwMonth = (CreateSimpleHour(2024, 1, 1, 1), CreateSimpleHour(2024, 2, 1, 2));
            var throwDay = (CreateSimpleHour(2024, 1, 1, 1), CreateSimpleHour(2024, 1, 2, 2));
            var throwHour = (CreateSimpleHour(2024, 1, 1, 1), CreateSimpleHour(2024, 1, 1, 1));
            var successData = (CreateTestNowSchedule(), CreateTestNowSchedule(offsetHour: 1));

            DaySchedule schedule;
            Action<(DateTime, DateTime)> checkThrowsMacro = (data) =>
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => schedule = new DaySchedule(data.Item1, data.Item2));
            };

            checkThrowsMacro(throwYear);
            checkThrowsMacro(throwMonth);
            checkThrowsMacro(throwDay);
            checkThrowsMacro(throwHour);
            schedule = new DaySchedule(successData.Item1, successData.Item2);
        }

        [Fact]
        public void SimpleScheduleGenerate()
        {
            var testReservation = new Reservation(TestCorrectFee);
            var dateItemGenerator = new DateItemGenerator(testScheduleMarkUpPath);
            var guideGenerator = new GuideGenerator(dateItemGenerator, testGuideMarkUpPath);

            Action AddSchedule = ()=>{
                testReservation = testReservation.AddSchedule(
                    new DaySchedule(       //西暦 月 日 時
                    CreateTestNowSchedule(),
                    CreateTestNowSchedule(1))
                );
            };

            //シンプルな予定（正常なスケジュール）表示するコード
            AddSchedule();
            Assert.NotNull(testReservation);

            _output.WriteLine(ReservationInfo(testReservation));
            _output.WriteLine(OutputGuideMessage(testReservation, guideGenerator));

            //ダブりテスト
            Assert.Throws<ArgumentException>(AddSchedule);
        }

        [Theory]
        [ClassData(typeof(DayOfTheWeekSchedule))]
        public void WeekScheduleGenerate(List<(DateTime Start, DateTime End)> schedules)
        {
            var testReservation = new Reservation(TestCorrectFee);
            var dateItemGenerator = new DateItemGenerator(testScheduleMarkUpPath);
            var guideGenerator = new GuideGenerator(dateItemGenerator, testGuideMarkUpPath);

            //Add Schedule
            foreach (var schedule in schedules)
                testReservation = testReservation.AddSchedule(new DaySchedule(schedule.Start, schedule.End));

            _output.WriteLine(ReservationInfo(testReservation));
            _output.WriteLine(OutputGuideMessage(testReservation, guideGenerator));
        }
    }
}