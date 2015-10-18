using System.Linq;
using System.Linq.Expressions;

namespace SimpleLinq1.Extensions
{
    public static class FooExtension
    {
        public static IQueryable<TSource> Foo<TSource>(this IQueryable<TSource> source, string bar)
        {
            return source.Provider.CreateQuery<TSource>(
                            LinqHelper.MethodCall(
                                LinqHelper.GetMethodInfo(Foo, source, bar),
                                source.Expression,
                                Expression.Constant(bar)));
        }
    }
}
