using MessageGeneratorSystem.Generator.Xml;
using MessageGeneratorSystem.Generator.Xml.General;
using System.Xml.Linq;
using Xunit.Abstractions;

namespace MessageGeneratorTest.Generator.Xml
{
    internal class XmlTestHelper
    {
        public static string QuickCheckXmlGenerate(
            ITestOutputHelper output,
            IReadOnlyList<ITextGenXmlElement> xmlElementList,
            string filePath, 
            string check = "")
        {
            XDocument xml = LoadXml(filePath, output);
            string result = "";
            Assert.NotNull(xml.Root);
            foreach (var element in xml.Root.Elements())
            {
                ITextGenXmlElement? genElement = xmlElementList.Where(x => x.Key == element.Name).FirstOrDefault();
                if (genElement == null) continue;
                string text = genElement.GenerateText(element);
                result += text;
                output.WriteLine($"--Generate Added--\n{text}");
            }
            if (check != "") Assert.Equal(check, result);
            output.WriteLine($"------------Result Generate------------ \n\n{result}");
            return result;
        }

        public static IReadOnlyList<ITextGenXmlElement> CreateSimpleElementList(List<ITextGenXmlElement>? textElements = null)
        {
            XmlText xmlText = new XmlText();
            XmlTitle xmlTitle = new XmlTitle();
            List<ITextGenXmlElement> textElementList;
            if (textElements != null) 
                textElementList = new(textElements);
            else
                textElementList = new();
            textElementList.Add(xmlText);
            textElementList.Add(xmlTitle);

            var xmlLine = new XmlLine(textElementList);
            textElementList.Add(xmlLine);
            return textElementList;
        }

        public static XDocument LoadXml(string file, ITestOutputHelper output)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            output.WriteLine($"------------XmlFileLoad------------");
            output.WriteLine($"Load : {currentDirectory}\n");
            return XDocument.Load(currentDirectory + file, LoadOptions.PreserveWhitespace);
        }
    }
}
