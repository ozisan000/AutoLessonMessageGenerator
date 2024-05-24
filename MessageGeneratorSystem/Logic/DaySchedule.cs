using System;

namespace MessageGeneratorSystem.Logic
{
    public class DaySchedule
    {
        public readonly int LessonCount;
        public readonly DateTime StartSchedule;
        public readonly DateTime EndSchedule;
        public const int HourToMin = 60;
        private const int MinLessonCount = 1;

        public DaySchedule(DateTime start, DateTime end,float lessonCountHourRate = 1.0f)
        {
            //if (start < DateTime.Now) throw new ArgumentOutOfRangeException();
            if (start.Year != end.Year) throw new ArgumentOutOfRangeException();
            if (start.Month != end.Month) throw new ArgumentOutOfRangeException();
            if (start.Day != end.Day) throw new ArgumentOutOfRangeException();

            int timeDifference = ConvertHourToMinite(end) - ConvertHourToMinite(start);
            float lessonHour = (float)timeDifference / (float)HourToMin;
            if (lessonHour < lessonCountHourRate) throw new ArgumentOutOfRangeException();
            int lessonCount = (int)Math.Ceiling(lessonHour);
            if (lessonCount < MinLessonCount) throw new ArgumentOutOfRangeException();

            this.StartSchedule = start;
            this.EndSchedule = end;
            this.LessonCount = lessonCount;
        }

        public static int ConvertHourToMinite(DateTime time)
        {
            return time.Hour * HourToMin + time.Minute;
        }
    }
}
