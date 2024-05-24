using MessageGeneratorSystem.Logic;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlDate : ITextGenXmlElement
    {
        public string Key => "date";
        public event Func<string,string>? GetCustomStyleDate;
        private readonly DateTime date;

        public XmlDate(DaySchedule schedule) {
            date = schedule.StartSchedule;
        }

        public string GenerateText(XElement element)
        {
            return date.ToString(element.Value);
        }
    }
}
