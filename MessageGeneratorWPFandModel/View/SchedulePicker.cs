using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchoolMessageGenerator.View
{
    internal class SchedulePicker
    {
        public DateTimePicker StartSchedulePicker { get; }
        public DateTimePicker EndSchedulePicker { get; }

        public SchedulePicker(DateTimePicker start,DateTimePicker end)
        {
            StartSchedulePicker = start;
            EndSchedulePicker = end;
        }
    }
}
