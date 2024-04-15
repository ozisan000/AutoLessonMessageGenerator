namespace AutoSchoolMessageGenerator.Logic
{
    public class Reservation
    {
        public readonly IReadOnlyList<Schedule> LessonSchedules;
        public readonly int LessonFee;

        public int TotalLessonCount => LessonSchedules.Sum(e => e.LessonCount);

        public int TotalLessonFee => LessonSchedules.Sum(e => e.LessonCount) * LessonFee;

        public Reservation(int lessonFee)
        {
            this.LessonFee = lessonFee;
            LessonSchedules = new List<Schedule>();
        }

        private Reservation(Reservation oldReservation, IReadOnlyList<Schedule> stackEvents)
        {
            LessonSchedules = stackEvents;
            this.LessonFee = oldReservation.LessonFee;
        }

        public Reservation AddEvent(Schedule addEvent)
        {
            List<Schedule> newStackEvent = new List<Schedule>(LessonSchedules)
            {
                addEvent
            };

            return new Reservation(this, newStackEvent);
        }

        public Reservation RemoveEvent(int index)
        {
            List<Schedule> newStackEvent = new List<Schedule>(LessonSchedules);
            newStackEvent.RemoveAt(index);
            return new Reservation(this, newStackEvent);
        }
    }
}
