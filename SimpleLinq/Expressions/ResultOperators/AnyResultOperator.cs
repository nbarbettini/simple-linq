using System.Linq.Expressions;

namespace SimpleLinq1.Expressions.ResultOperators
{
    internal class AnyResultOperator : ResultOperatorExpression
    {
        public AnyResultOperator()
        {
            this.ResultType = ResultOperator.Any;
        }
    }
}
