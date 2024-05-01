using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
