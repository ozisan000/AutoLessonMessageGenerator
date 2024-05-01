using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoSchoolMessageGenerator.View
{
    internal class SchedulePicker
    {
        private readonly DatePicker _datePicker;
        private readonly TimeCombo _timeCombo;

        //public DateTime Schedule => new()

        public SchedulePicker(DatePicker datePicker,TimeCombo timeCombo) {
            _datePicker = datePicker;
            _timeCombo = timeCombo;
        }

        public DateTime GetDateTime()
        {
            DateTime selectedDay = _datePicker.SelectedDate.Value;
            return new DateTime(
                selectedDay.Year, 
                selectedDay.Month, 
                selectedDay.Day, 
                _timeCombo.Hour, 
                _timeCombo.Minute, 
                0);
        }
    }
}
