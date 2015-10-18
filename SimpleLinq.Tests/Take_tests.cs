using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace SimpleLinq1.Tests
{
    public class Take_tests
    {
        [Fact]
        public void Take_with_int_argument()
        {
            var model = Build.Query(x => x.Take(5));

            model.Take.ShouldBe(5);
        }

        [Fact]
        public void Take_with_variable_argument()
        {
            var takeThisMany = 10;

            var model = Build.Query(x => x.Take(takeThisMany));

            model.Take.ShouldBe(10);
        }

        [Fact]
        public void Take_with_lazy_argument()
        {
            var takeThisManyLazily = new Lazy<int>(() =>
            {
                System.Threading.Thread.Sleep(5);
                return 7;
            });

            var model = Build.Query(x => x.Take(takeThisManyLazily.Value));

            model.Take.ShouldBe(7);
        }

        [Fact]
        public void Take_with_func_argument()
        {
            var takeThisManyFunc = new Func<int>(() => 3);

            var model = Build.Query(x => x.Take(takeThisManyFunc()));

            model.Take.ShouldBe(3);
        }
    }
}
