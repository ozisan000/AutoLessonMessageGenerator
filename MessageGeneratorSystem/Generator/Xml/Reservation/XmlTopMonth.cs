using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    internal class XmlTopMonth : XmlIntValue
    {
        public override string Key => "topMonth";

        public XmlTopMonth(Logic.Reservation reservation) : 
            base(reservation.LessonSchedules.First().StartSchedule.Month) { }
    }
}
