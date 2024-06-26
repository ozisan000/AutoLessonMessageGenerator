﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchoolMessageGeneratorTest.Logic
{
    using static GuideGenerateTestHelper;

    /// <summary>
    /// Check the correctness of schedule for each day of the week.
    /// </summary>
    internal class DayOfTheWeekSchedule : IEnumerable<object[]>
    {
        private readonly List<object[]> _testData = new();
        private readonly List<(DateTime Start, DateTime End)> _testSchedules = new();
        private readonly int Year = DateTime.Now.Year;
        private readonly int Month = DateTime.Now.Month + 1;
        private const int Hour = 20;
        private readonly DayOfWeek[] CheckDayOfWeeks = new DayOfWeek[]{
            DayOfWeek.Monday ,
            DayOfWeek.Thursday
        };

        public DayOfTheWeekSchedule()
        {
            ProccessForDaysInMonth(Year, Month, (day) =>
            {
                //If DayOfWeek'sn't agree,early return.
                if (!CheckDayOfWeeks.Contains(new DateTime(Year, Month, day).DayOfWeek)) return;

                var schedule = (CreateSimpleHour(Year, Month, day, Hour),
                                CreateSimpleHour(Year, Month, day, Hour + 1));
                _testSchedules.Add(schedule);
            });

            _testData.Add(new object[] { _testSchedules });
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            return _testData.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
