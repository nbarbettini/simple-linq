using System.Linq.Expressions;

namespace SimpleLinq.Expressions.ResultOperators
{
    internal class AnyResultOperator : ResultOperatorExpression
    {
        public AnyResultOperator()
        {
            this.ResultType = ResultOperator.Any;
        }
    }
}
