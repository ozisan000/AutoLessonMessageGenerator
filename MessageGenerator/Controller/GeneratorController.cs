using MessageGeneratorSystem.Generator.Xml.Reservation;
using MessageGeneratorSystem.Generator.Xml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessageGeneratorSystem.Logic;
using System.IO;
using System.Configuration;
using System.Reflection;
using MessageGeneratorSystem.Generator;

namespace MessageGenerator.Controller
{
    internal class GeneratorController
    {
        private GenerateXmlSchedule _scheduleGenerator = null;
        private GenerateXmlGuide _guideGenerator = null;

        private readonly MainWindow _view;
        private readonly GeneratorCurrentConfig _currentGuide;
        private readonly GeneratorCurrentConfig _currentSchedule;
        private readonly GeneratorCustomConfig _customGuide;

        private const string DefaultScheduleKey = "ScheduleMarkUpFile";
        private const string DefaultGuideKey = "GuideMarkUpFile";
        private const string DocumentFolderKey = "DocumentFolder";
        private const string CustomGuideKey = "CustomGuide";
        private const string CurrentGuideKey = "CurrentGuide";
        private const string CurrentScheduleKey = "CurrentSchedule";
        private const string sucsessText = "Loaded Xml File!";

        public GeneratorController(MainWindow view)
        {
            _view = view;

            _currentGuide = new GeneratorCurrentConfig(GetLocalDataPath() + GetConfig(CurrentGuideKey));
            _currentSchedule = new GeneratorCurrentConfig(GetLocalDataPath() + GetConfig(CurrentScheduleKey));
            _customGuide = new GeneratorCustomConfig(GetLocalDataPath() + GetConfig(CustomGuideKey));
        }

        public void LoadDefaultGuideXml()
        {
            string path = GetDefaultXmlPath(DefaultGuideKey);
            _currentGuide.CurrentPath = path;
        }

        public void LoadDefaultScheduleXml()
        {
            string path = GetDefaultXmlPath(DefaultScheduleKey);
            _currentSchedule.CurrentPath = path;
        }

        public void LoadCurrentXml(Reservation reservation)
        {
            if (!GeneratorDirectory.CheckFile(_currentGuide.CurrentPath)) 
                LoadDefaultGuideXml();
            if (!GeneratorDirectory.CheckFile(_currentSchedule.CurrentPath)) 
                LoadDefaultScheduleXml();
            CreateGenerator(reservation);
        }

        public void LoadXml(Reservation reservation, int index)
        {
            string path = _customGuide.GetPathList()[index];
            LoadCurrentXml(reservation);
        }

        public string GenerateScheduleText(Reservation reservation, DaySchedule schedule)
        {
            if (_scheduleGenerator == null)
            {
                LoadCurrentXml(reservation);
            }
            return _scheduleGenerator.GenerateScheduleText(schedule);
        }

        public string GenerateGuide(Reservation reservation)
        {
            if (_scheduleGenerator == null)
            {
                LoadCurrentXml(reservation);
            }
            return _guideGenerator.GenerateGuide(reservation);
        }

        private bool CreateGenerator(Reservation reservation)
        {
            _view.EnabledAddButton = false;
            _view.EnabledGenerateButton = false;

            try
            {
                _scheduleGenerator = new GenerateXmlSchedule(_currentSchedule.CurrentPath);
                _guideGenerator = new GenerateXmlGuide(_scheduleGenerator, _currentGuide.CurrentPath);
            }
            catch (System.Xml.XmlException ex)
            {
                _view.ShowErrorDialog(ex.Message);
                return false;
            }
            catch (FileNotFoundException ex)
            {
                _view.ShowErrorDialog(ex.Message);
                return false;
            }
            catch (DirectoryNotFoundException ex)
            {
                _view.ShowErrorDialog(ex.Message);
                return false;
            }

            _view.EnabledGenerateButton = reservation.LessonSchedules.Count > 0 ? true : false;
            _view.EnabledAddButton = true;
            return true;
        }

        private string GetLocalDataPath()
        {
            return GeneratorDirectory.GetLocalDataPath(Assembly.GetExecutingAssembly());
        }

        private string GetConfig(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        private string GetDefaultXmlPath(string fileKey)
        {
            string documentFolder = GeneratorDirectory.GetDocumentFolderPath() + GetConfig(DocumentFolderKey);

            string defaultPath = documentFolder + GetConfig(fileKey);

            string localDataPath = GetLocalDataPath();
            string localDataSchedulePath = localDataPath + GetConfig(fileKey);

            GeneratorDirectory.CheckAndCopyFile(localDataSchedulePath, defaultPath);
            return defaultPath;
        }
    }
}
