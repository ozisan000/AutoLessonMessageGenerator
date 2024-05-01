using AutoSchoolMessageGenerator.Generator;
using AutoSchoolMessageGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchoolMessageGenerator.Controller
{
    internal class ReservationController
    {
        private Reservation _reservation;
        private DateItemGenerator _dateItemGenerator;
        private GuideGenerator _guideGenerator;
        private string dateItemPath = "";

        public ReservationController()
        {
            //_reservation = new Reservation(lessonFee);
            //_dateItemGenerator = new()
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

        public void UpdateLessonFee(int newLessonFee)
        {
            _reservation = new Reservation(_reservation, newLessonFee);
        }
    }
}
