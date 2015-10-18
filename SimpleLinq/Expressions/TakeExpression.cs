using System.Linq.Expressions;

namespace SimpleLinq.Expressions
{
    internal class TakeExpression : ParsedExpression
    {
        public TakeExpression(int value)
        {
            this.Value = value;
        }

        public int Value { get; private set; }

        protected internal override Expression Accept(CompilingExpressionVisitor visitor)
        {
            return visitor.VisitTake(this);
        }
    }
}
