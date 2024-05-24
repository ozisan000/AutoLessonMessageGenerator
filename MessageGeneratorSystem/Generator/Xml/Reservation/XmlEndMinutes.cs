using MessageGeneratorSystem.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlEndMinutes : XmlIntValue
    {
        public override string Key => "endMinutes";

        public XmlEndMinutes(DaySchedule schedule) : base(schedule.EndSchedule.Minute) { }
    }
}
