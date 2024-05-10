using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MessageGenerator.Logic
{
    public class ScheduleHelper
    {
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
