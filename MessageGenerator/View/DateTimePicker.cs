using Microsoft.UI.Xaml.Controls;
using System;

namespace MessageGenerator.View
{
    internal class DateTimePicker
    {
        private readonly CalendarDatePicker _datePicker;
        private readonly TimePicker _timePicker;

        public bool IsSelectedDate => _datePicker.Date != null;

        public DateTimePicker(CalendarDatePicker date, TimePicker time) {
            _datePicker = date;
            this._timePicker = time;
        }

        public DateTime GetDateTime()
        {
            //if (_datePicker.SelectedDate == null) throw new NullReferenceException();
            DateTime selectedDay = _datePicker.Date.Value.DateTime;
            return new DateTime(selectedDay.Year,
                selectedDay.Month,
                selectedDay.Day,
                _timePicker.Time.Hours,
                _timePicker.Time.Minutes,
                0);
        }
    }
}
