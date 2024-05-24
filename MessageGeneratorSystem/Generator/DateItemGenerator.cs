using MessageGenerator.Helper;
using MessageGeneratorSystem.Logic;
using System.Xml;

namespace MessageGenerator.Generator
{
    public class DateItemGenerator
    {
        private readonly string itemFormat;

        public DateItemGenerator(string filePath)
        {
            string checkFormat = GeneratorHelper.ReadOrCreateMarkUpFile(filePath);
            var doc = new XmlDocument();
            doc.Load(filePath);


            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.DateTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.StartHourTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.StartMinuteTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.LessonCountTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.EndHourTag);
            GeneratorHelper.CheckWritingNeedTag(checkFormat, GeneratorTags.EndMinuteTag);

            itemFormat = checkFormat;
        }

        public string GenerateDateItemText(DaySchedule schedule)
        {
            string dateItemText = itemFormat;
            dateItemText = dateItemText.Replace(GeneratorTags.DateTag,          schedule.StartSchedule.ToString("MM/dd"));
            dateItemText = dateItemText.Replace(GeneratorTags.StartHourTag,     schedule.StartSchedule.Hour.ToString());
            dateItemText = dateItemText.Replace(GeneratorTags.StartMinuteTag,     schedule.StartSchedule.Minute.ToString());
            dateItemText = dateItemText.Replace(GeneratorTags.EndHourTag,       schedule.EndSchedule.Hour.ToString());
            dateItemText = dateItemText.Replace(GeneratorTags.EndMinuteTag,       schedule.EndSchedule.Minute.ToString());
            dateItemText = dateItemText.Replace(GeneratorTags.LessonCountTag,   schedule.LessonCount.ToString());
            return dateItemText;
        }
    }
}
