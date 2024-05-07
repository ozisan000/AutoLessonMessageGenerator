using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace MessageGenerator.View
{
    internal class DateTimePicker
    {
        private readonly DatePicker _datePicker;
        private readonly TimeCombo _timeCombo;

        public bool IsSelectedDate => _datePicker.SelectedDate != null;

        public DateTimePicker(DatePicker datePicker,TimeCombo timeCombo) {
            _datePicker = datePicker;
            _timeCombo = timeCombo;
        }

        public DateTime GetDateTime()
        {
            //if (_datePicker.SelectedDate == null) throw new NullReferenceException();
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
