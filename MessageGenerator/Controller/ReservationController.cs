using MessageGenerator.Generator;
using MessageGenerator.Logic;
using MessageGenerator.View;
using Microsoft.UI.Xaml.Controls;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace MessageGenerator.Controller
{
    internal class ReservationController
    {
        private Reservation _reservation;
        private DateItemGenerator _dateItemGenerator;
        private GuideGenerator _guideGenerator;
        private string dateItemPath = "";
        private const string testScheduleMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\MessageGeneratorWPFandModel\\TestScheduleMarkUp.txt";
        private const string testGuideMarkUpPath = "G:\\Code\\CCharp\\AutoSchoolMessageGenerator\\MessageGeneratorWPFandModel\\TestGuideMarkUp.txt";

        public ReservationController()
        {
            _reservation = new Reservation(0);

            //TestCode
            _dateItemGenerator = new(testScheduleMarkUpPath);
            _guideGenerator = new(_dateItemGenerator, testGuideMarkUpPath);
        }

        public string AddSchedule(DateTime start,DateTime end)
        {
            var newSchedule = new DaySchedule(start, end);
            _reservation = _reservation.AddSchedule(newSchedule);
            return _dateItemGenerator.GenerateDateItemText(newSchedule);
        }

        public void RemoveSchedule(int index)
        {
            _reservation = _reservation.RemoveSchedule(index);
        }

        public void UpdateLessonFee(object? sender, NumberBoxValueChangedEventArgs e)
        {
            _reservation = new Reservation(_reservation, (int)e.NewValue);
        }

        /// <summary>
        /// メッセージを生成し、クリップボードにコピーする、生成に失敗した場合nullを返す
        /// </summary>
        /// <returns></returns>
        public string GenerateGuideMessage()
        {
            if (_reservation.LessonSchedules.Count <= 0) return null;
            return _guideGenerator.GenerateGuideText(_reservation);
        }
    }
}
