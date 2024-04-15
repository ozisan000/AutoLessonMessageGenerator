using Xunit.Abstractions;
using AutoSchoolMessageGenerator;
using AutoSchoolMessageGenerator.Logic;
using AutoSchoolMessageGenerator.Generator;

namespace AutoSchoolMessageGeneratorTest
{
    public class GuideGenerateUnitTest
    {
        private readonly ITestOutputHelper _output;
        private const int testFee = 1650;
        private const string testScheduleMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\AutoSchoolMessageGenerator\\TestScheduleMarkUp.txt";
        private const string testGuideMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\AutoSchoolMessageGenerator\\TestGuideMarkUp.txt";

        public GuideGenerateUnitTest (ITestOutputHelper output) {
            _output = output;
            _output.WriteLine("------Start {0}------\n", nameof(GuideGenerateUnitTest));
            Create();
        }

        [Fact]
        public void Create()
        {
            _output.WriteLine($"---Start Test \"{nameof(Create)}\" Method---\n");

            //クラスのインスタンス化をテスト
            var testReservation = new Reservation(testFee);
            Assert.NotNull(testReservation);
            ReservationInfo(testReservation);

            //間違ったパスで例外発生することを確認(正しい処理です)
            var dateItemGenerator = new DateItemGenerator(testScheduleMarkUpPath);
            Assert.NotNull(dateItemGenerator);

            //間違ったパスで例外発生することを確認(正しい処理です)
            var guideGenerator = new GuideGenerator(dateItemGenerator,testGuideMarkUpPath);
            Assert.NotNull(guideGenerator);

            _output.WriteLine($"\n---End Test \"{nameof(Create)}\" Method---\n\n\n");
        }

        [Fact]
        public void SimpleScheduleGenerate()
        {
            _output.WriteLine($"---Start Test \"{nameof(SimpleScheduleGenerate)}\" Method---\n");

            //正しいマークアップファイルのパスが指定されており、正しい金額が渡されている状態
            var testReservation = new Reservation(testFee);
            var dateItemGenerator = new DateItemGenerator(testScheduleMarkUpPath);
            var guideGenerator = new GuideGenerator(dateItemGenerator, testGuideMarkUpPath);

            //
            testReservation = testReservation.AddSchedule(
                new Schedule(CreateSimpleHour(2024, 4, 1, 20), CreateSimpleHour(2024, 4, 1, 21))
                );
            Assert.NotNull(testReservation);
            ReservationInfo(testReservation);

            //生成テスト
            OutputGuideMessage(testReservation, guideGenerator);

            _output.WriteLine($"\n---End Test \"{nameof(SimpleScheduleGenerate)}\" Method---\n\n\n");
        }

        private void ReservationInfo(Reservation reservation)
        {
            _output.WriteLine($"{nameof(reservation.LessonFee)}:{reservation.LessonFee}\n");
            _output.WriteLine($"{nameof(reservation.TotalLessonCount)}:{reservation.TotalLessonCount}\n");
            _output.WriteLine($"{nameof(reservation.TotalLessonFee)}:{reservation.TotalLessonFee}\n");
        }

        private void OutputGuideMessage(Reservation reservation,GuideGenerator generator)
        {
            //文章作成のテスト
            string resultText = generator.GenerateGuideText(reservation);
            _output.WriteLine($"---Output Test Guide Message---\n{resultText}");
        }

        private DateTime CreateSimpleHour(int year,int month,int day,int hour)
        {
            return new DateTime(year, month, day, hour, 0, 0);
        }
    }
}