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
        private const string loadDefautGuideText = "Not founded select file,Loaded default guide xml!";
        private const string loadDefautScheduleText = "Not founded select file,Loaded default schedule xml!";

        public GeneratorController(MainWindow view)
        {
            _view = view;

            _currentGuide = new GeneratorCurrentConfig(GetLocalDataPath() + GetConfig(CurrentGuideKey));
            _currentSchedule = new GeneratorCurrentConfig(GetLocalDataPath() + GetConfig(CurrentScheduleKey));
            _customGuide = new GeneratorCustomConfig(GetLocalDataPath() + GetConfig(CustomGuideKey));
        }

        public void SelectGuideXml(Reservation reservation, string guidePath)
        {
            _currentGuide.CurrentPath = guidePath;
            LoadCurrentXml(reservation);
        }

        public void LoadDefaultXml(Reservation reservation)
        {
            LoadDefaultGuideXml();
            LoadDefaultScheduleXml();
            LoadCurrentXml(reservation);
        }

        public bool LoadCurrentXml(Reservation reservation)
        {
            bool isShowFlyout = true;
            string loadText = "";
            if (!GeneratorDirectory.CheckFile(_currentGuide.CurrentPath)) {
                isShowFlyout = false;
                loadText += loadDefautGuideText;
                LoadDefaultGuideXml();
            }
            if (!GeneratorDirectory.CheckFile(_currentSchedule.CurrentPath))
            {
                if (!isShowFlyout) 
                    loadText += "\n";
                else
                    isShowFlyout = false;
                
                loadText += loadDefautScheduleText;
                LoadDefaultScheduleXml();
            }

            if(!isShowFlyout) _view.ShowErrorFlyout(loadText);

            return CreateGenerator(reservation, isShowFlyout);
        }

        public string GenerateScheduleText(DaySchedule schedule)
        {
            return _scheduleGenerator.GenerateScheduleText(schedule);
        }

        public string GenerateGuide(Reservation reservation)
        {
            return _guideGenerator.GenerateGuide(reservation,_scheduleGenerator);
        }

        private bool CreateGenerator(Reservation reservation,bool isShowFlyout = true)
        {
            _view.EnabledAddButton = false;
            _view.EnabledGenerateButton = false;

            GenerateXmlGuide xmlGuide;
            GenerateXmlSchedule xmlSchedule;

            try
            {
                xmlSchedule = new GenerateXmlSchedule(_currentSchedule.CurrentPath);
                xmlGuide = new GenerateXmlGuide(_currentGuide.CurrentPath);
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

            _guideGenerator = xmlGuide;
            _scheduleGenerator = xmlSchedule;

            if (isShowFlyout) _view.ShowErrorFlyout(sucsessText);

            _view.EnabledGenerateButton = reservation.LessonSchedules.Count > 0 ? true : false;
            _view.EnabledAddButton = true;
            return true;
        }

        private void LoadDefaultGuideXml()
        {
            string path = GetDefaultXmlPath(DefaultGuideKey);
            _currentGuide.CurrentPath = path;
        }

        private void LoadDefaultScheduleXml()
        {
            string path = GetDefaultXmlPath(DefaultScheduleKey);
            _currentSchedule.CurrentPath = path;
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
