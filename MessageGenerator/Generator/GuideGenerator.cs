using MessageGenerator.Helper;
using MessageGenerator.Logic;
using System.Linq;

namespace MessageGenerator.Generator
{
    public class GuideGenerator
    {
        private readonly string guideFormat;
        private readonly DateItemGenerator _dateItemGenerator;

        public GuideGenerator(DateItemGenerator itemGenerator, string filePath)
        {
            string checkFormat = GeneratorHelper.ReadMarkUpFile(filePath);

            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.LessonScheduleTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.LessonFeeTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.TotalLessonCountTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.TotalLessonFeeTag);

            guideFormat = checkFormat;
            _dateItemGenerator = itemGenerator;
        }

        /// <summary>
        /// オンライン授業の案内文章を作成する、その際予約内容を昇順に並び変える
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        public string GenerateGuideText(Reservation reservation)
        {
            string guideText = guideFormat;

            //日程のテキストを作成
            string scheduleText = "";

            foreach (var calenderEvent in reservation.LessonSchedules.OrderBy(o => o.StartSchedule))
                scheduleText += _dateItemGenerator.GenerateDateItemText(calenderEvent) + "\n";

            guideText = guideText.Replace(GeneratorTags.LessonScheduleTag, scheduleText);
            guideText = guideText.Replace(GeneratorTags.LessonFeeTag, reservation.LessonFee.ToString());
            guideText = guideText.Replace(GeneratorTags.TotalLessonCountTag, reservation.TotalLessonCount.ToString());
            guideText = guideText.Replace(GeneratorTags.TotalLessonFeeTag, reservation.TotalLessonFee.ToString());
            return guideText;
        }
    }
}
