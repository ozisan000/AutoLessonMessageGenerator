using MessageGenerator.Generator;
using MessageGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            _reservation = new Reservation(1650);

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

        public void UpdateLessonFee(object? sender, UpdateFeeEventArgs e)
        {
            _reservation = new Reservation(_reservation, e.NewLessonFee);
        }

        /// <summary>
        /// メッセージを生成し、クリップボードにコピーする
        /// </summary>
        /// <returns></returns>
        public string GenerateGuideMessage()
        {
            string generated = _guideGenerator.GenerateGuideText(_reservation);
            Clipboard.SetData(DataFormats.UnicodeText, generated);
            return generated;
        }
    }
}
