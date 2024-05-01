using AutoSchoolMessageGenerator.Generator;
using AutoSchoolMessageGenerator.Logic;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoSchoolMessageGeneratorTest.Logic
{
    public class GuideGenerateTestHelper
    {
        public static string ReservationInfo(Reservation reservation)
        {
            string outputText
                        = $"{nameof(reservation.LessonFee)}:{reservation.LessonFee}\n";
            outputText += $"{nameof(reservation.TotalLessonCount)}:{reservation.TotalLessonCount}\n";
            outputText += $"{nameof(reservation.TotalLessonFee)}:{reservation.TotalLessonFee}\n";
            return outputText;
        }

        /// <summary>
        /// テキストを表示する
        /// </summary>
        /// <param name="reservation"></param>
        /// <param name="generator"></param>
        /// <returns></returns>
        public static string OutputGuideMessage(Reservation reservation, GuideGenerator generator)
        {
            return $"---Output Test Guide Message---\n{generator.GenerateGuideText(reservation)}\n";
        }

        /// <summary>
        /// 分と秒を無視したDateTimeを生成する
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="hour"></param>
        /// <returns></returns>
        public static DateTime CreateSimpleHour(int year, int month, int day, int hour)
        {
            return new DateTime(year, month, day, hour, 0, 0);
        }

        public static void ProccessForDaysInMonth(int year, int month, Action<int> process)
        {
            var days = DateTime.DaysInMonth(year, month);
            for (int day = 1; day <= days; day++)
                process(day);
        }
    }
}
