using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SimpleLinq
{
    public sealed class SimpleQueryable<TData> : IOrderedQueryable<TData>
    {
        public SimpleQueryable()
        {
            this.Provider = new SimpleQueryProvider();
            this.Expression = Expression.Constant(this);
        }

        // Called internally by LINQ
        public SimpleQueryable(SimpleQueryProvider provider, Expression expression)
        {
            if (provider == null)
                throw new ArgumentNullException("provider");
            if (expression == null)
                throw new ArgumentNullException("expression");
            if (!typeof(IQueryable<TData>).IsAssignableFrom(expression.Type))
                throw new ArgumentOutOfRangeException("expression");

            this.Provider = provider;
            this.Expression = expression;
        }

        public IQueryProvider Provider { get; private set; }
        public Expression Expression { get; private set; }

        public Type ElementType => typeof(TData);

        public IEnumerator<TData> GetEnumerator()
            => (Provider.Execute<IEnumerable<TData>>(Expression)).GetEnumerator();

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            => (Provider.Execute<System.Collections.IEnumerable>(Expression)).GetEnumerator();
    }
}
