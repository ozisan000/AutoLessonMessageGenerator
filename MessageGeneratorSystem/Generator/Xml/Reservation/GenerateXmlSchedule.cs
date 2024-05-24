using MessageGeneratorSystem.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class GenerateXmlSchedule
    {
        private readonly XDocument _xDocument;
        private const string defaultText = "[NotRootSchedule]";
        private const string NewLine = "\n";

        public GenerateXmlSchedule(string path)
        {
            _xDocument = XDocument.Load(path, LoadOptions.PreserveWhitespace);
        }

        public string GenerateScheduleText(DaySchedule schedule)
        {
            if (_xDocument.Root == null) return defaultText;

            var scheduleElements = new List<ITextGenXmlElement>()
            {
                new XmlDate(schedule),
                new XmlStartHour(schedule),
                new XmlEndHour(schedule),
                new XmlStartMinutes(schedule),
                new XmlEndMinutes(schedule),
                new XmlLessonCount(schedule),
                new XmlText()
            };

            string result = "";
            foreach(XElement element in _xDocument.Root.Elements())
            {
                ITextGenXmlElement? genElement = scheduleElements.Where(x => x.Key == element.Name).FirstOrDefault();
                if (genElement == null) continue;
                result += genElement.GenerateText(element);
            }
            return result + NewLine + NewLine;
        }
    }
}
