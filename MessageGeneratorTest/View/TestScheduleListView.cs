using AutoSchoolMessageGenerator;
using AutoSchoolMessageGeneratorTest.Logic;
using Microsoft.VisualStudio.TestPlatform.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Xunit.Abstractions;

namespace AutoSchoolMessageGeneratorTest.View
{
    public class TestScheduleListView
    {
        private ITestOutputHelper _output;

        public TestScheduleListView(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("------Start {0}------\n", nameof(GuideGenerateUnitTest));
        }

        [Fact]
        public void TestCreateScheduleControl()
        {

        }
    }
}
