using System.Linq.Expressions;

namespace SimpleLinq.Expressions.ResultOperators
{
    internal abstract class ResultOperatorExpression : ParsedExpression
    {
        public ResultOperator ResultType { get; protected set; }

        protected internal override Expression Accept(CompilingExpressionVisitor visitor)
        {
            return visitor.VisitResultOperator(this);
        }
    }
}
