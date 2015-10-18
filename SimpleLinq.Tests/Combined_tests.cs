using System.Linq;
using Shouldly;
using Xunit;

namespace SimpleLinq.Tests
{
    public class Combined_tests
    {
        [Fact]
        public void Skip_and_take()
        {
            var model = Build.Query(x => x
                .Skip(5)
                .Take(10));

            model.Skip.ShouldBe(5);
            model.Take.ShouldBe(10);
        }

        [Fact]
        public void Where_and_take()
        {
            var model = Build.Query(x => x
                .Where(y => y.Bar == "Baz")
                .Take(10));

            model.Wheres.ShouldContain(w =>
                w.FieldName == "Bar" &&
                w.Value == "Baz" &&
                w.Comparison == WhereComparison.Equals);

            model.Take.ShouldBe(10);
        }

        [Fact]
        public void OrderBy_at_beginning()
        {
            var model = Build.Query(x => x
                .OrderBy(y => y.Bar)
                .ThenByDescending(y => y.Baz)
                .Take(2)
                .Where(y => y.Bar == "foo"));

            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Bar" &&
                o.Direction == OrderByDirection.Ascending);
            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Baz" &&
                o.Direction == OrderByDirection.Descending);

            model.Take.ShouldBe(2);

            model.Wheres.ShouldContain(w =>
                w.FieldName == "Bar" &&
                w.Value == "foo" &&
                w.Comparison == WhereComparison.Equals);
        }

        [Fact]
        public void FirstOrDefault_result_operator()
        {
            var model = Build.ResultQuery(x => x
                .Where(y => y.Bar == "foo123")
                .Skip(5)
                .FirstOrDefault());

            model.Wheres.ShouldContain(w =>
                w.FieldName == "Bar" &&
                w.Value == "foo123" &&
                w.Comparison == WhereComparison.Equals);
            model.Skip.ShouldBe(5);
            model.ResultOperator.ShouldBe(ResultOperator.First);
            model.DefaultIfEmpty.ShouldBeTrue();
        }
    }
}
