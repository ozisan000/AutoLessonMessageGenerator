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
            dateItemText.Replace(GeneratorTags.DateTag, item.Date.Date.ToString());
            dateItemText.Replace(GeneratorTags.StartTimeTag, item.StartTime.ToString());
            dateItemText.Replace(GeneratorTags.EndTimeTag, item.EndTime.ToString());
            dateItemText.Replace(GeneratorTags.LessonCountTag, item.LessonCount.ToString());
            return dateItemText;
        }
    }
}
