namespace AutoSchoolMessageGenerator.Logic
{
    public class DaySchedule
    {
        public readonly int LessonCount;
        public readonly DateTime StartSchedule;
        public readonly DateTime EndSchedule;

        public DaySchedule(DateTime start, DateTime end)
        {
            if (start < DateTime.Now) throw new ArgumentOutOfRangeException();
            if (start.Year != end.Year) throw new ArgumentOutOfRangeException();
            if (start.Month != end.Month) throw new ArgumentOutOfRangeException();
            if (start.Day != end.Day) throw new ArgumentOutOfRangeException();
            int lessonCount = end.Hour - start.Hour;
            if (lessonCount < 1) throw new ArgumentOutOfRangeException();
            this.StartSchedule = start;
            this.EndSchedule = end;
            this.LessonCount = lessonCount;
        }
    }
}
