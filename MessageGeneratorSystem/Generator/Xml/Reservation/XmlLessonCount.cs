using MessageGenerator.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlLessonCount : XmlIntValue
    {
        public override string Key => "lessonCount";

        public XmlLessonCount(DaySchedule schedule) : base(schedule.LessonCount) { }
    }
}
