using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MessageGenerator.View
{
    internal enum DateTimePickerError
    {
        SUCSESS,
        NotPickDay,
        NotPickTime
    }

    internal class DateTimePicker
    {
        private readonly CalendarDatePicker _datePicker;
        private readonly TimePicker _timePicker;

        public bool IsSelectedDate => _datePicker.Date != null;

        public DateTimePicker(CalendarDatePicker date, TimePicker time) {
            _datePicker = date;
            this._timePicker = time;
        }

        public (DateTime,DateTimePickerError) GetDateTime()
        {
            if (_datePicker.Date == null) return new(new DateTime(), DateTimePickerError.NotPickDay);
            if(_timePicker.SelectedTime == null) return new(new DateTime(), DateTimePickerError.NotPickTime);

            DateTime selectedDay = _datePicker.Date.Value.DateTime;
            selectedDay = new DateTime(
                selectedDay.Year,
                selectedDay.Month,
                selectedDay.Day,
                _timePicker.Time.Hours,
                _timePicker.Time.Minutes,
                0);

            return (selectedDay,DateTimePickerError.SUCSESS);
        }
    }

    internal class DateTimePickerErrorHandler
    {
        private readonly List<IDateTimePickerError> _errorList = new()
        {
            new NotPickDayError(),
            new NotPickTimeError()
        };

        public bool CheckError(DateTimePickerError error)
        {
            IDateTimePickerError result = GetIDateTimePickerError(error);
            if (result == null) return false;
            return true;
        }

        public string GetErrorMessage(DateTimePickerError error)
        {
            IDateTimePickerError result = GetIDateTimePickerError(error);
            if (result != null) return result.ErrorMessage;
            return DateTimePickerError.SUCSESS.ToString();
        }

        public IDateTimePickerError GetIDateTimePickerError(DateTimePickerError error)
        {
            return _errorList.FirstOrDefault(item => item.Error == error);
        }
    }

    internal interface IDateTimePickerError
    {
        DateTimePickerError Error { get; }
        string ErrorMessage { get; }
    }

    internal class NotPickDayError: IDateTimePickerError
    {
        public DateTimePickerError Error => DateTimePickerError.NotPickDay;
        public string ErrorMessage => "Not Pick Day!";
    }

    internal class NotPickTimeError : IDateTimePickerError
    {
        public DateTimePickerError Error => DateTimePickerError.NotPickTime;
        public string ErrorMessage => "Not Pick Time!";
    }
}
