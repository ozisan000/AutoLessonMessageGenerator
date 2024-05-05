namespace AutoSchoolMessageGenerator.Logic
{
    public class DaySchedule
    {
        public readonly int LessonCount;
        public readonly DateTime StartSchedule;
        public readonly DateTime EndSchedule;
        public const int OneHour = 60;

        public DaySchedule(DateTime start, DateTime end,float lessonCountHour = 1.0f)
        {
            //if (start < DateTime.Now) throw new ArgumentOutOfRangeException();
            if (start.Year != end.Year) throw new ArgumentOutOfRangeException();
            if (start.Month != end.Month) throw new ArgumentOutOfRangeException();
            if (start.Day != end.Day) throw new ArgumentOutOfRangeException();

            int timeDifference = (end.Hour * OneHour + end.Minute) - (start.Hour * OneHour + start.Minute);
            float lessonHour = (float)timeDifference / (float)OneHour;
            if (lessonHour < lessonCountHour) throw new ArgumentOutOfRangeException();
            int lessonCount = (int)Math.Ceiling(lessonHour);
            if (lessonCount < 1) throw new ArgumentOutOfRangeException();

            this.StartSchedule = start;
            this.EndSchedule = end;
            this.LessonCount = lessonCount;
        }
    }
}
