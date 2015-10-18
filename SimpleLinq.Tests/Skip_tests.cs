using System.Linq;
using Shouldly;
using Xunit;

namespace SimpleLinq1.Tests
{
    public class Skip_tests
    {
        [Fact]
        public void Skip_with_int_argument()
        {
            var model = Build.Query(x => x.Skip(5));

            model.Skip.ShouldBe(5);
        }
    }
}
