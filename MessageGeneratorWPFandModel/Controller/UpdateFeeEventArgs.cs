using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchoolMessageGenerator.Controller
{
    internal class UpdateFeeEventArgs:EventArgs
    {
        public int NewLessonFee { get; }

        public UpdateFeeEventArgs(int lessonFee) : base()
        {
            NewLessonFee = lessonFee;
        }
    }
}
