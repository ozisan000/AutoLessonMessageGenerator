namespace AutoSchoolMessageGenerator.Logic
{
    public class Schedule
    {
        public readonly int LessonCount;
        public readonly DateTime StartSchedule;
        public readonly DateTime EndSchedule;

        public Schedule(DateTime start,DateTime end)
        {
            this.StartSchedule = start;
            this.EndSchedule = end;
            int lessonCount = EndSchedule.Hour - StartSchedule.Hour;
            if (lessonCount < 1) throw new ArgumentOutOfRangeException();
            this.LessonCount = lessonCount;
        }
    }
}
