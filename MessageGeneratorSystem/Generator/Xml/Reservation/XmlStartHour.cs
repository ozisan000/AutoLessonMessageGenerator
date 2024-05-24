using MessageGeneratorSystem.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlStartHour : XmlIntValue
    {
        public override string Key => "startHour";

        public XmlStartHour(DaySchedule schedule, string format = "00") : base(schedule.StartSchedule.Hour,format) { }
    }
}
