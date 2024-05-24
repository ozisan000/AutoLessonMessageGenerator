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
    public class XmlLessonFee : XmlIntValue
    {
        public override string Key => "lessonFee";

        public XmlLessonFee(MessageGenerator.Logic.Reservation reservation) : base(reservation.LessonFee) { }
    }
}
