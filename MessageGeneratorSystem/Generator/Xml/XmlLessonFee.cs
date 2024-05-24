using MessageGenerator.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MessageGeneratorSystem.Generator.Xml
{
    public class XmlLessonFee : ITextGenXmlElement
    {
        public string Key => "lessonFee";
        public event Func<int>? LessonFee;
        private const string DefaultText = "NotRegistFee";

        public string GenerateText(XElement element)
        {
            string result = DefaultText;
            if (LessonFee != null) result = LessonFee().ToString();
            return result;
        }
    }
}
