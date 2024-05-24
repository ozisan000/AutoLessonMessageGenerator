using MessageGeneratorSystem.Generator.Xml.General;
using MessageGeneratorSystem.Generator.Xml.Reservation;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml
{
    public class GenerateXmlGuide
    {
        private readonly string guideXmlPath;
        private readonly XmlText _defaultText = new XmlText();
        private readonly XmlTitle _defaultTitle = new XmlTitle();
        private readonly GenerateXmlSchedule _generateXmlSchedule;
        private readonly string defaultText;

        public GenerateXmlGuide(GenerateXmlSchedule generate,string guidePath,string defaultText = "[LoadGuideError]")
        {
            this.guideXmlPath = guidePath;
            _generateXmlSchedule = generate;
            this.defaultText = defaultText;
        }

        public string GenerateGuide(Logic.Reservation reservation)
        {
            XmlLessonFee xmlLessonFee = new(reservation);
            XmlTotalLessonFee xmlTotalLessonFee = new(reservation);
            XmlTotalLessonCount xmlTotalLessonCount = new(reservation);
            XmlSchedules xmlSchedules = new(reservation, _generateXmlSchedule);

            List<ITextGenXmlElement> xmlElements = new List<ITextGenXmlElement>()
            {
                xmlTotalLessonFee,
                xmlTotalLessonCount,
                xmlLessonFee,
                xmlSchedules,
                _defaultText,
                _defaultTitle
            };
            XmlLine xmlLine = new XmlLine(xmlElements);
            xmlElements.Add(xmlLine);

            XDocument xml = XDocument.Load(guideXmlPath, LoadOptions.PreserveWhitespace);

            if (xml.Root == null) return defaultText;

            string result = "";
            foreach (var element in xml.Root.Elements())
            {
                ITextGenXmlElement? genElement = xmlElements.Where(x => x.Key == element.Name).FirstOrDefault();
                if (genElement == null) continue;
                string text = genElement.GenerateText(element);
                result += text;
            }
            return result;
        }
    }
}
