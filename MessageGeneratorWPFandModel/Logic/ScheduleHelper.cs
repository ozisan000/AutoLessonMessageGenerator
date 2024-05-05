using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchoolMessageGenerator.Logic
{
    public class ScheduleHelper
    {
        public static DaySchedule CreateSchedule(DateTime start,int endHour,int endMinites)
        {
            var end = new DateTime(start.Year, start.Month, start.Day, endHour, endMinites, 0);
            return new DaySchedule(start, end);
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

        public static bool EqualOverlapOfLine(Vector2 s1, Vector2 s2)
        {

            float S1 = (s1.X - s2.X) * (s1.Y - s2.Y);
            float S2 = (s1.X - s2.X) * (s1.Y - s2.Y);
            if ((S1 > 0 && S2 < 0) || (S1 < 0 && S2 < 0)) return true;
            return false;
        }
    }
}
