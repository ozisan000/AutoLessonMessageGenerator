namespace AutoSchoolMessageGenerator.Logic
{
    public class Schedule
    {
        public readonly DateTime Date;
        public readonly int StartTime;
        public readonly int EndTime;
        public readonly int LessonCount;

        public Schedule(DateTime date, int startTime, int endTime)
        {
            this.Date = date;
            this.StartTime = startTime;
            this.EndTime = endTime;
            int lessonCount = endTime - startTime;
            if (lessonCount < 1) throw new ArgumentOutOfRangeException();
        }
    }
}
