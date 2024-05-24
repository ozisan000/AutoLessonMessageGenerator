﻿using MessageGenerator.Logic;
using MessageGeneratorSystem.Generator.Xml.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Reservation
{
    public class XmlEndHour : XmlIntValue
    {
        public override string Key => "endHour";

        public XmlEndHour(DaySchedule schedule) : base(schedule.EndSchedule.Hour) { }
    }
}
