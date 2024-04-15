using System.Linq;
namespace AutoSchoolMessageGenerator.Logic
{
    public class Reservation
    {
        public readonly IReadOnlyList<Schedule> LessonSchedules;
        public readonly int LessonFee;

        public int TotalLessonCount => LessonSchedules.Sum(e => e.LessonCount);

        public int TotalLessonFee => TotalLessonCount * LessonFee;

        public Reservation(int lessonFee)
        {
            this.LessonFee = lessonFee;
            LessonSchedules = new List<Schedule>();
        }

        private Reservation(Reservation oldReservation, IReadOnlyList<Schedule> schedules)
        {
            LessonSchedules = schedules;
            this.LessonFee = oldReservation.LessonFee;
        }

        public Reservation AddSchedule(Schedule addSchedule)
        {
            List<Schedule> newStackEvent = new List<Schedule>(LessonSchedules)
            {
                addSchedule
            };

            return new Reservation(this, newStackEvent);
        }

        public Reservation RemoveSchedule(int index)
        {
            List<Schedule> newStackEvent = new List<Schedule>(LessonSchedules);
            newStackEvent.RemoveAt(index);
            return new Reservation(this, newStackEvent);
        }
    }
}
