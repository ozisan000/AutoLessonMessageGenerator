using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MessageGeneratorSystem.Generator.Xml.Title
{
    public class XmlTitleOrnament : ITitleOrnament
    {
        private readonly string ornament = "-";
        //private readonly int marginLength = 3;
        private readonly int ornamentMargin = 3;
        //private const string Space = " ";
        private const string NewLine = "\n";
        private readonly float multiByteRate = 2.1f;
        private const int ShiftJISCode = 932;

        public XmlTitleOrnament() { }

        public string GenerateTitle(string titleText)
        {
            string ornament = GenerateOrnament(TextByteCount(titleText));
            return $"{ornament}{titleText}\n{ornament}";
        }

        private string GenerateOrnament(int length)
        {
            string genOrnament = "";
            for (int i = 0; i < length + ornamentMargin; i++)
                genOrnament += ornament;
            return genOrnament + NewLine;
        }

        private int TextByteCount(string text)
        {
            Encoding e;
            try
            {
                e = Encoding.GetEncoding(ShiftJISCode);
            }
            catch(NotSupportedException ex)
            {
                e = Encoding.UTF8;
            }

            char[] chars = text.ToCharArray();
            float cnt = 0;
            for (int index = 0; index < text.Length; index++)
            {
                float byteSize = e.GetByteCount(chars, index, 1);
                if (1 < byteSize) byteSize = multiByteRate;
                cnt += byteSize;
            }
            return (int)cnt;
        }

        //private string EditMargin(string text)
        //{
        //    string textMargin = "";
        //    for (int i = 0; i < marginLength * 1.5; i++)
        //        textMargin += Space;
        //    return textMargin + text + textMargin + NewLine;
        //}
    }
}
