using System.Linq;
using Shouldly;
using System;
using Xunit;

namespace SimpleLinq1.Tests
{
    public class Where_tests
    {
        [Fact]
        public void Where_member_equals_implicit()
        {
            var model = Build.Query(x => x
                .Where(y => y.Bar == "Test"));

            model.Wheres.ShouldContain(wc =>
                wc.FieldName == "Bar" &&
                wc.Value == "Test" &&
                wc.Comparison == WhereComparison.Equals);
        }

        [Fact]
        public void Where_member_equals()
        {
            var model = Build.Query(x => x
                .Where(y => y.Bar.Equals("Test")));

            model.Wheres.ShouldContain(wc =>
                wc.FieldName == "Bar" &&
                wc.Value == "Test" &&
                wc.Comparison == WhereComparison.Equals);
        }

        [Fact]
        public void Throws_for_higher_overloads()
        {
            Should.Throw<NotSupportedException>(() =>
            {
                var model = Build.Query(x => x
                    .Where(y => y.Bar.Equals("Test", StringComparison.CurrentCulture)));
            });
        }

        [Fact]
        public void Where_member_starts_with()
        {
            var model = Build.Query(x => x
                .Where(y => y.Bar.StartsWith("Test")));

            model.Wheres.ShouldContain(wc =>
                wc.FieldName == "Bar" &&
                wc.Value == "Test" &&
                wc.Comparison == WhereComparison.StartsWith);
        }

        [Fact]
        public void Where_member_ends_with()
        {
            var model = Build.Query(x => x
                .Where(y => y.Bar.EndsWith("Test")));

            model.Wheres.ShouldContain(wc =>
                wc.FieldName == "Bar" &&
                wc.Value == "Test" &&
                wc.Comparison == WhereComparison.EndsWith);
        }

        [Fact]
        public void Where_member_contains()
        {
            var model = Build.Query(x => x
                .Where(y => y.Bar.Contains("Test")));

            model.Wheres.ShouldContain(wc =>
                wc.FieldName == "Bar" &&
                wc.Value == "Test" &&
                wc.Comparison == WhereComparison.Contains);
        }
    }
}
