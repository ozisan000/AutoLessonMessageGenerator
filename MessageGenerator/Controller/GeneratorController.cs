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
        private GenerateXmlSchedule _scheduleGenerator;
        private GenerateXmlGuide _guideGenerator;

        private const string ScheduleMarkUpKey = "ScheduleMarkUpFile";
        private const string GuideMarkUpKey = "GuideMarkUpFile";
        private const string DebugScheduleMarkUpKey = "DebugScheduleMarkUpFile";
        private const string DebugGuideMarkUpKey = "DebugGuideMarkUpFile";
        private const string DocumentFolderKey = "DocumentFolder";
        public GeneratorController(MainWindow view,Reservation reservation)
        {
            view.EnabledAddButton = false;
            view.EnabledGenerateButton = false;
            /*//#if DEBUG
            //string current = AppContext.BaseDirectory;
            //scheduleXmlPath = current + ConfigurationManager.AppSettings[DebugScheduleMarkUpKey];
            //guideXmlPath = current + ConfigurationManager.AppSettings[DebugGuideMarkUpKey];

            // UWPのみで利用できるLocalForderクラス
            //string test = ApplicationData.Current.LocalFolder.Path;

            //var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal);
            //_mainView.ShowErrorDialog(config.FilePath);

            //var stt = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //_mainView.ShowErrorDialog(stt);
            //#else*/
            string documentFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            documentFolder += ConfigurationManager.AppSettings[DocumentFolderKey];
            if (!Directory.Exists(documentFolder))
            {
                Directory.CreateDirectory(documentFolder);
            }

            string schedulePath = documentFolder + ConfigurationManager.AppSettings[ScheduleMarkUpKey];
            string guidePath = documentFolder + ConfigurationManager.AppSettings[GuideMarkUpKey];

            string localDataPath = GeneratorDirectory.GetLocalDataPath(Assembly.GetExecutingAssembly());
            string localDataSchedulePath = localDataPath + ConfigurationManager.AppSettings[ScheduleMarkUpKey];
            string localDataGuidePath = localDataPath + ConfigurationManager.AppSettings[GuideMarkUpKey];

            GeneratorDirectory.CheckAndCopyFile(localDataSchedulePath, schedulePath);
            GeneratorDirectory.CheckAndCopyFile(localDataGuidePath, guidePath);

            //#endif

            try
            {
                _scheduleGenerator = new GenerateXmlSchedule(schedulePath);
                _guideGenerator = new GenerateXmlGuide(_scheduleGenerator, guidePath);
            }
            catch (System.Xml.XmlException ex)
            {
                view.ShowErrorDialog(ex.Message);
                return;
            }
            catch (FileNotFoundException ex)
            {
                view.ShowErrorDialog(ex.Message);
                return;
            }
            catch (DirectoryNotFoundException ex)
            {
                view.ShowErrorDialog(ex.Message);
                return;
            }

            view.EnabledGenerateButton = reservation.LessonSchedules.Count > 0 ? true : false;
            view.EnabledAddButton = true;
        }

        public string GenerateScheduleText(DaySchedule schedule)
        {
            return _scheduleGenerator.GenerateScheduleText(schedule);
        }

        public string GenerateGuide(Reservation reservation)
        {
            return _guideGenerator.GenerateGuide(reservation);
        }
    }
}
