using System.Linq;
using Shouldly;
using Xunit;

namespace SimpleLinq.Tests
{
    public class OrderBy_tests
    {
        [Fact]
        public void OrderBy_field()
        {
            var model = Build.Query(x => x.OrderBy(y => y.Bar));

            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Bar" &&
                o.Direction == OrderByDirection.Ascending);
        }

        [Fact]
        public void OrderBy_field_descending()
        {
            var model = Build.Query(x => x.OrderByDescending(y => y.Baz));

            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Baz" &&
                o.Direction == OrderByDirection.Descending);
        }

        [Fact]
        public void OrderBy_multiple_fields()
        {
            var model = Build.Query(x => x
                .OrderBy(y => y.Bar)
                .ThenBy(y => y.Baz));

            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Bar" &&
                o.Direction == OrderByDirection.Ascending);
            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Baz" &&
                o.Direction == OrderByDirection.Ascending);
        }

        [Fact]
        public void OrderBy_multiple_fields_descending()
        {
            var model = Build.Query(x => x
                .OrderByDescending(y => y.Bar)
                .ThenByDescending(y => y.Baz));

            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Bar" &&
                o.Direction == OrderByDirection.Descending);
            model.OrderBys.ShouldContain(o =>
                o.FieldName == "Baz" &&
                o.Direction == OrderByDirection.Descending);
        }
    }
}
