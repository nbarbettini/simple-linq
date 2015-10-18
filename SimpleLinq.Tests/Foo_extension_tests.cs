using Shouldly;
using SimpleLinq1.Extensions;
using Xunit;

namespace SimpleLinq1.Tests
{
    public class Foo_extension_tests
    {
        [Fact]
        public void Foo_by_itself()
        {
            var model = Build.Query(x => x.Foo("baz"));

            model.Foo.ShouldBe("baz");
        }
    }
}
