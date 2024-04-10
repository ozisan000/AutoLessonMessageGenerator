using Xunit.Abstractions;

namespace AutoSchoolMessageGeneratorTest
{
    public class UnitTest1
    {
        private readonly ITestOutputHelper _output;

        public UnitTest1 (ITestOutputHelper output) {
            _output = output;
            _output.WriteLine("Start {0}", nameof(UnitTest1));
        }

        [Fact]
        public void Test1()
        {

        }
    }
}