using AutoSchoolMessageGenerator.Helper;
using AutoSchoolMessageGenerator.Logic;

namespace AutoSchoolMessageGenerator.Generator
{
    public class DateItemGenerator
    {
        private readonly string itemFormat;

        public DateItemGenerator(string filePath)
        {
            string checkFormat = GeneratorHelper.ReadMarkUpFile(filePath);

            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.DateTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.StartTimeTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.LessonCountTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.EndTimeTag);

            itemFormat = checkFormat;
        }

        public string GenerateDateItemText(Schedule item)
        {
            string dateItemText = itemFormat;
            dateItemText = dateItemText.Replace(GeneratorTags.DateTag, item.StartSchedule.ToString("yyyy/MM/dd"));
            dateItemText = dateItemText.Replace(GeneratorTags.StartTimeTag, item.StartSchedule.Hour.ToString());
            dateItemText = dateItemText.Replace(GeneratorTags.EndTimeTag, item.EndSchedule.Hour.ToString());
            dateItemText = dateItemText.Replace(GeneratorTags.LessonCountTag, item.LessonCount.ToString());
            return dateItemText;
        }
    }
}
