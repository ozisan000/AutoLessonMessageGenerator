using MessageGenerator.Generator;
using MessageGeneratorSystem.Generator.Xml;
using MessageGeneratorSystem.Generator.Xml.Reservation;
using MessageGeneratorSystem.Logic;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Configuration;
using System.IO;
using System.Reflection;

namespace MessageGenerator.Controller
{
    internal class ReservationController
    {
        private MainWindow _mainView;
        private Reservation _reservation;
        private readonly GeneratorController _generatorController;

        //private DateItemGenerator _dateItemGenerator;
        //private GuideGenerator _guideGenerator;
        private GenerateXmlSchedule _scheduleGenerator;
        private GenerateXmlGuide _guideGenerator;

        private readonly string scheduleXmlPath;
        private readonly string guideXmlPath;

        private const string ScheduleMarkUpKey = "ScheduleMarkUpFile";
        private const string GuideMarkUpKey = "GuideMarkUpFile";
        private const string DocumentFolderKey = "DocumentFolder";

        public ReservationController(MainWindow mainView)
        {
            _mainView = mainView;
            _reservation = new Reservation(0);
            _generatorController = new GeneratorController(_mainView);
            _mainView.ChangedLessonFee += UpdateLessonFee;
            _mainView.AddedShecule += AddSchedule;
            _mainView.RemovedAtSchedule += RemoveAtSchedule;
            _mainView.GeneratedMessage += GenerateGuideMessage;
            _mainView.DefaultLoadXml += () => _generatorController.LoadDefaultXml(_reservation);
            _mainView.SelectGuideXml += (path) => _generatorController.SelectGuideXml(_reservation, path);
        }

        public void AfterLoadedUIInit()
        {
            _generatorController.LoadCurrentXml(_reservation);
        }

        private void RefreshSchedules()
        {
            _mainView.ClearScheduleElements();
            foreach (var schedule in _reservation.LessonSchedules)
            {
                string scheduleText = _generatorController.GenerateScheduleText(schedule);
                _mainView.AddScheduleElement(scheduleText);
            }
        }

        private void AddSchedule(DateTime start,DateTime end)
        {
            DaySchedule newSchedule;
            try
            {
                newSchedule = new DaySchedule(start, end);
            }
            catch (PerfectMatchTimeException ex)
            {
                _mainView.ShowErrorFlyout(ex.Message);
                return;
            }
            catch (NotCoveredDateException ex)
            {
                _mainView.ShowErrorFlyout(ex.Message);
                return;
            }catch (EndTimeMinException ex)
            {
                _mainView.ShowErrorFlyout(ex.Message);
                return;
            }

            (Reservation result, ReservationError error) newReservation = _reservation.AddSchedule(newSchedule);
            ReservationErrorHandler check = new();
            if (check.CheckError(newReservation.error))
            {
                string subTitle = check.GetErrorMessage(newReservation.error);
                _mainView.ShowErrorFlyout(subTitle);
                return;
            }

            _reservation = newReservation.result;
            RefreshSchedules();
            _mainView.EnabledGenerateButton = true;
        }

        private void RemoveAtSchedule(int index)
        {
            _reservation = _reservation.RemoveScheduleAt(index);
            RefreshSchedules();
            if (_reservation.LessonSchedules.Count <= 0)
                _mainView.EnabledGenerateButton = false;
        }

        private void UpdateLessonFee(object? sender, NumberBoxValueChangedEventArgs e)
        {
            _reservation = new Reservation(_reservation, (int)e.NewValue);
        }

        /// <summary>
        /// メッセージを生成し、クリップボードにコピーする、生成に失敗した場合nullを返す
        /// </summary>
        /// <returns></returns>
        private void GenerateGuideMessage()
        {
            if (_reservation.LessonSchedules.Count <= 0) return;
            string result = _generatorController.GenerateGuide(_reservation);

            //mainViewで結果を表示
            _mainView.ShowGenerateDialog(result);
        }
    }
}
