namespace AutoSchoolMessageGenerator.Helper
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
        public const string StartTimeTag = "<ST>";
        public const string LessonCountTag = "<LC>";
        public const string EndTimeTag = "<ET>";
    }
}
