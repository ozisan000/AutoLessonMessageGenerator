using MessageGenerator.Generator;
using MessageGeneratorSystem.Generator.Xml;
using MessageGeneratorSystem.Generator.Xml.Reservation;
using MessageGeneratorSystem.Logic;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Configuration;
using System.IO;

namespace MessageGenerator.Controller
{
    internal class ReservationController
    {
        private MainWindow _mainView;
        private Reservation _reservation;
        //private DateItemGenerator _dateItemGenerator;
        //private GuideGenerator _guideGenerator;
        private GenerateXmlSchedule _scheduleGenerator;
        private GenerateXmlGuide _guideGenerator;

        private readonly string scheduleXmlPath;
        private readonly string guideXmlPath;

        private const string ScheduleMarkUpKey = "ScheduleMarkUpFile";
        private const string GuideMarkUpKey = "GuideMarkUpFile";
        private const string DefaultScheduleMarkUpKey = "ScheduleMarkUpFile";
        private const string DefaultGuideMarkUpKey = "GuideMarkUpFile";

        public ReservationController(MainWindow mainView)
        {
            _mainView = mainView;
            _reservation = new Reservation(0);


            _mainView.ChangedLessonFee += UpdateLessonFee;
            _mainView.AddedShecule += AddSchedule;
            _mainView.RemovedAtSchedule += RemoveAtSchedule;
            _mainView.GeneratedMessage += GenerateGuideMessage;

            string current = AppContext.BaseDirectory;
            scheduleXmlPath = current + ConfigurationManager.AppSettings[ScheduleMarkUpKey];
            guideXmlPath = current + ConfigurationManager.AppSettings[GuideMarkUpKey];

            _scheduleGenerator = new GenerateXmlSchedule(scheduleXmlPath);
            _guideGenerator = new GenerateXmlGuide(_scheduleGenerator, guideXmlPath);
        }

        private string AddSchedule(DateTime start,DateTime end)
        {
            var newSchedule = new DaySchedule(start, end);
            _reservation = _reservation.AddSchedule(newSchedule);
            return _scheduleGenerator.GenerateScheduleText(newSchedule);
        }

        private void RemoveAtSchedule(int index)
        {
            _reservation = _reservation.RemoveScheduleAt(index);
        }

        private void UpdateLessonFee(object? sender, NumberBoxValueChangedEventArgs e)
        {
            _reservation = new Reservation(_reservation, (int)e.NewValue);
        }

        /// <summary>
        /// メッセージを生成し、クリップボードにコピーする、生成に失敗した場合nullを返す
        /// </summary>
        /// <returns></returns>
        private string GenerateGuideMessage()
        {
            if (_reservation.LessonSchedules.Count <= 0) return null;
            return _guideGenerator.GenerateGuide(_reservation);
        }

        //private void CreateDateItemGenerator()
        //{
        //    try
        //    {
        //        _dateItemGenerator = new DateItemGenerator(ConfigurationManager.AppSettings[ScheduleMarkUpKey]);
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
    }
}
