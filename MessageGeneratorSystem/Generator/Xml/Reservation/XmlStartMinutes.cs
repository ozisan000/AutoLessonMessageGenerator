using MessageGeneratorSystem.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlStartMinutes : XmlIntValue
    {
        public override string Key => "startMinutes";

        public XmlStartMinutes(DaySchedule schedule) : base(schedule.StartSchedule.Minute) { }
    }
}
