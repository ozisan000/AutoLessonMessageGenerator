namespace MessageGenerator.Helper
{
    internal class GeneratorTags
    {
        //ガイド生成で必要なタグ
        public const string LessonScheduleTag = "<LS>";
        public const string LessonFeeTag = "<LF>";
        public const string TotalLessonCountTag = "<TLC>";
        public const string TotalLessonFeeTag = "<TLF>";

        //スケジュール生成で必要なタグ
        public const string DateTag = "<D>";
        public const string StartHourTag = "<SH>";
        public const string StartMinuteTag = "<SM>";
        public const string LessonCountTag = "<LC>";
        public const string EndHourTag = "<EH>";
        public const string EndMinuteTag = "<EM>";
    }
}
