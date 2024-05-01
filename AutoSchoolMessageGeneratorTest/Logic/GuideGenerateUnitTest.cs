using Xunit.Abstractions;
using AutoSchoolMessageGenerator.Logic;
using AutoSchoolMessageGenerator.Generator;


namespace AutoSchoolMessageGeneratorTest.Logic
{
    using static GuideGenerateTestHelper;

    public class GuideGenerateUnitTest
    {
        private readonly ITestOutputHelper _output;
        private const int TestCorrectFee = 100;
        private const int TestWrongFee = 0;
        private const string testScheduleMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\AutoSchoolMessageGenerator\\TestScheduleMarkUp.txt";
        private const string testGuideMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\AutoSchoolMessageGenerator\\TestGuideMarkUp.txt";
        private const string TestDummyDirectory = "G:\\Dummy\\a.txt";
        private const string TestDummyMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\AutoSchoolMessageGenerator\\dummy";
        private const string TestDummyMarkUpFile = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\AutoSchoolMessageGenerator\\DummyMarkUp.txt";

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
            var successData = (CreateSimpleHour(2024, 1, 1, 1), CreateSimpleHour(2024, 1, 1, 2));

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

            //シンプルな予定（正常なスケジュール）表示するコード
            testReservation = testReservation.AddSchedule(
                new DaySchedule(       //西暦 月 日 時
                    CreateSimpleHour(year: 2024, month: 4, day: 1, hour: 20),
                    CreateSimpleHour(year: 2024, month: 4, day: 1, hour: 21))
                );
            Assert.NotNull(testReservation);
            _output.WriteLine(ReservationInfo(testReservation));

            //生成テスト
            _output.WriteLine(OutputGuideMessage(testReservation, guideGenerator));
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