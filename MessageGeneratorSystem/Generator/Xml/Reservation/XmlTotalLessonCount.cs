using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlTotalLessonCount : XmlIntValue
    {
        public override string Key => "totalLessonCount";

        public XmlTotalLessonCount(MessageGenerator.Logic.Reservation reservation) : base(reservation.TotalLessonCount) { }
    }
}
