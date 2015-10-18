using Shouldly;
using SimpleLinq.Extensions;
using Xunit;

namespace SimpleLinq.Tests
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
