using System;
using System.Runtime.Serialization;

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
            if (start.Year != end.Year) throw new NotCoveredDateException();
            if (start.Month != end.Month) throw new NotCoveredDateException();
            if (start.Day != end.Day) throw new NotCoveredDateException();
            if (start == end) throw new PerfectMatchTimeException();
            if (start.Hour > end.Hour) throw new EndTimeMinException();

            int timeDifference = ConvertHourToMinite(end) - ConvertHourToMinite(start);
            float lessonHour = (float)timeDifference / (float)HourToMin;
            //if (lessonHour < lessonCountHourRate) throw new ArgumentOutOfRangeException();
            int lessonCount = (int)Math.Ceiling(lessonHour);
            if (lessonCount < MinLessonCount) lessonCount = 0;

            this.StartSchedule = start;
            this.EndSchedule = end;
            this.LessonCount = lessonCount;
        }

        public static int ConvertHourToMinite(DateTime time)
        {
            return time.Hour * HourToMin + time.Minute;
        }
    }

    [Serializable]
    public class NotCoveredDateException : Exception
    {
        private const string ErrorMessage = "Dates do not match.";
        public NotCoveredDateException() : base(ErrorMessage) { }
        public NotCoveredDateException(string message) : base(message) { }
        protected NotCoveredDateException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class PerfectMatchTimeException : Exception
    {
        private const string ErrorMessage = "Time match perfectly.";
        public PerfectMatchTimeException() : base(ErrorMessage) { }
        public PerfectMatchTimeException(string message) : base(message) { }
        protected PerfectMatchTimeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class EndTimeMinException : Exception
    {
        private const string ErrorMessage = "The end time is earlier than the start time.";
        public EndTimeMinException() : base(ErrorMessage) { }
        public EndTimeMinException(string message) : base(message) { }
        protected EndTimeMinException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
