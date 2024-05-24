﻿using System.Collections.ObjectModel;

namespace MessageGeneratorSystem.Logic
{
    public class Reservation
    {
        public readonly IReadOnlyList<DaySchedule> LessonSchedules;
        public readonly int LessonFee;

        public int TotalLessonCount => LessonSchedules.Sum(e => e.LessonCount);

        public int TotalLessonFee => TotalLessonCount * LessonFee;

        public Reservation(int lessonFee)
        {
            if (lessonFee < 0) throw new ArgumentOutOfRangeException();
            this.LessonFee = lessonFee;
            LessonSchedules = new ObservableCollection<DaySchedule>();
        }

        public Reservation(Reservation oldReservation, int newLessonFee)
        {
            if (newLessonFee < 0) throw new ArgumentOutOfRangeException();
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
            if (ForSimilaritySchedule(LessonSchedules, addSchedule))
                throw new ArgumentException();
            List<DaySchedule> newSchedules = new List<DaySchedule>(LessonSchedules)
            {
                addSchedule
            };
            return new Reservation(this, newSchedules);
        }

        public Reservation RemoveScheduleAt(int index)
        {
            List<DaySchedule> newSchedules = new List<DaySchedule>(LessonSchedules);
            newSchedules.RemoveAt(index);
            return new Reservation(this, newSchedules);
        }

        public Reservation RemoveSchedule(DaySchedule removeSchedule)
        {
            List<DaySchedule> newSchedules = new List<DaySchedule>(LessonSchedules);
            newSchedules.Remove(removeSchedule);
            return new Reservation(this, newSchedules);
        }

        public static bool ForSimilaritySchedule(IEnumerable<DaySchedule> schedules, DaySchedule checkSchedule)
        {
            if (schedules.Count() <= 0) return false;
            var BeSchedule = (DaySchedule schedule) =>
            {
                DateTime oriStart = schedule.StartSchedule;
                DateTime oriEnd = schedule.EndSchedule;
                DateTime checkStart = checkSchedule.StartSchedule;
                DateTime checkEnd = checkSchedule.EndSchedule;


                return (checkStart < oriStart && checkEnd <= oriStart) ||
                (oriEnd <= checkStart && oriEnd < checkEnd);
            };

            return !schedules.Any(schedule => BeSchedule(schedule));
        }
    }
}
