using MessageGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlSchedules : ITextGenXmlElement
    {
        public string Key => "schedules";
        private readonly IReadOnlyList<DaySchedule> _schedules;
        private readonly GenerateXmlSchedule _generateXmlSchedule;
        private readonly string defaultText;

        public XmlSchedules(
            MessageGenerator.Logic.Reservation reservation,
            GenerateXmlSchedule generateSchedule,
            string defaultText = "[NotRegistedSchedules]")
        {
            _schedules = reservation.LessonSchedules;
            _generateXmlSchedule = generateSchedule;
            this.defaultText = defaultText;
        }

        public string GenerateText(XElement element)
        {
            if(_schedules.Count <= 0) return defaultText;
            string result = "";
            foreach (var schedule in _schedules.OrderBy(s => s.StartSchedule))
            {
                result += _generateXmlSchedule.GenerateScheduleText(schedule);
            }
            return result;
        }
    }
}
