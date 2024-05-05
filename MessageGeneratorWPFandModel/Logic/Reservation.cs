using System.Linq;
namespace AutoSchoolMessageGenerator.Logic
{
    public class Reservation
    {
        public readonly IReadOnlyList<DaySchedule> LessonSchedules;
        public readonly int LessonFee;

        public int TotalLessonCount => LessonSchedules.Sum(e => e.LessonCount);

        public int TotalLessonFee => TotalLessonCount * LessonFee;

        public Reservation(int lessonFee)
        {
            if (lessonFee <= 0) throw new ArgumentOutOfRangeException();
            this.LessonFee = lessonFee;
            LessonSchedules = new List<DaySchedule>();
        }

        public Reservation(Reservation oldReservation, int newLessonFee)
        {
            if (newLessonFee <= 0) throw new ArgumentOutOfRangeException();
            LessonSchedules = oldReservation.LessonSchedules;
            this.LessonFee = newLessonFee;
        }

        private Reservation(Reservation oldReservation, IReadOnlyList<DaySchedule> schedules)
        {
            LessonSchedules = schedules;
            this.LessonFee = oldReservation.LessonFee;
        }

        public Reservation AddSchedule(DaySchedule addSchedule)
        {
            if (ScheduleHelper.ForSimilaritySchedule(LessonSchedules, addSchedule))
                throw new ArgumentException();

            List<DaySchedule> newStackEvent = new List<DaySchedule>(LessonSchedules)
            {
                addSchedule
            };

            return new Reservation(this, newStackEvent);
        }

        public Reservation RemoveSchedule(int index)
        {
            List<DaySchedule> newStackEvent = new List<DaySchedule>(LessonSchedules);
            newStackEvent.RemoveAt(index);
            return new Reservation(this, newStackEvent);
        }
    }
}
