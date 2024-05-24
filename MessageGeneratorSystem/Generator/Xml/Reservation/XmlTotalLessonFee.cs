using MessageGeneratorSystem.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlTotalLessonFee : XmlIntValue
    {
        public override string Key => "totalLessonFee";

        public XmlTotalLessonFee(Logic.Reservation reservation) : base(reservation.TotalLessonFee) { }
    }
}
