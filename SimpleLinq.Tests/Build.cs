using System;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleLinq.Tests
{
    public static class Build
    {
        public static QueryModel Query(Expression<Func<IQueryable<TestItem>, IQueryable<TestItem>>> queryExpression)
        {
            var testQueryable = new SimpleQueryable<TestItem>();
            
            return QueryModelCompiler.Compile(queryExpression);
        }

        public static QueryModel ResultQuery<T>(Expression<Func<IQueryable<TestItem>, T>> queryExpression)
        {
            return QueryModelCompiler.Compile(queryExpression);
        }
    }
}
