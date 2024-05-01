using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AutoSchoolMessageGenerator.View
{
    internal class LessonFeeBox
    {
        public event EventHandler<int>? LessonFeeChanged;

        private TextBox _lessonFeeBox;
        public int LessonFee { get; private set; }

        public LessonFeeBox(TextBox box)
        {
            _lessonFeeBox = box;
            _lessonFeeBox.TextChanged += TextChanged;
            ParseLessonFee();
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            ParseLessonFee();
            LessonFeeChanged?.Invoke(this, LessonFee);
        }

        private void ParseLessonFee()
        {
            LessonFee = int.Parse(_lessonFeeBox.Text);
        }
    }
}
