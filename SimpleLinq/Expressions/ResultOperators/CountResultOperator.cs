using System.Linq.Expressions;

namespace SimpleLinq1.Expressions.ResultOperators
{
    internal class CountResultOperator : ResultOperatorExpression
    {
        public CountResultOperator()
        {
            this.ResultType = ResultOperator.Count;
        }
    }
}
