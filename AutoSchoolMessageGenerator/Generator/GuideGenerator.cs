using AutoSchoolMessageGenerator.Helper;
using AutoSchoolMessageGenerator.Logic;

namespace AutoSchoolMessageGenerator.Generator
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

        public string GenerateGuideText(Reservation reservation)
        {
            string guideText = guideFormat;

            //日程のテキストを作成
           string scheduleText = "";
            foreach (var calenderEvent in reservation.LessonSchedules.OrderBy(o => o.Date))
                scheduleText += _dateItemGenerator.GenerateDateItemText(calenderEvent) + "\n";

            guideText.Replace(GeneratorTags.LessonScheduleTag, scheduleText);
            guideText.Replace(GeneratorTags.LessonFeeTag, reservation.LessonFee.ToString());
            guideText.Replace(GeneratorTags.TotalLessonCountTag, reservation.TotalLessonCount.ToString());
            guideText.Replace(GeneratorTags.TotalLessonFeeTag, reservation.TotalLessonFee.ToString());
            return guideText;
        }
    }
}
