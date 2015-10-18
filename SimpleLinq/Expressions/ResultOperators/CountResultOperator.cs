using System.Linq.Expressions;

namespace SimpleLinq.Expressions.ResultOperators
{
    internal class CountResultOperator : ResultOperatorExpression
    {
        public CountResultOperator()
        {
            this.ResultType = ResultOperator.Count;
        }
    }
}
