using System.Windows.Controls;

namespace AutoSchoolMessageGenerator.View
{
    internal class TimeCombo
    {
        public const int DayHour = 24;
        public const int HourMinute = 60;
        public const string Format = "D2";

        private readonly ComboBox _hourBox;
        private readonly ComboBox _minutesBox;

        public int Hour => _hourBox.SelectedIndex;
        public int Minute => _minutesBox.SelectedIndex;

        public TimeCombo(ComboBox hourBox,ComboBox minutesBox)
        {
            _hourBox = hourBox;
            _minutesBox = minutesBox;
            ViewHelper.ZetoToNumComboBox(_hourBox, DayHour, Format);
            ViewHelper.ZetoToNumComboBox(_minutesBox, HourMinute, Format);
        }
    }
}
