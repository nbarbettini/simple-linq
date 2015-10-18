using System.Linq.Expressions;

namespace SimpleLinq.Expressions.ResultOperators
{
    internal class FirstResultOperator : ResultOperatorExpression
    {
        public FirstResultOperator(bool defaultIfEmpty)
        {
            this.ResultType = ResultOperator.First;
            this.DefaultIfEmpty = defaultIfEmpty;
        }

        public bool DefaultIfEmpty { get; private set; }
    }
}
