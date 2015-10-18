using System.Linq.Expressions;

namespace SimpleLinq.Expressions
{
    internal class FooExpression : ParsedExpression
    {
        public FooExpression(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }

        protected internal override Expression Accept(CompilingExpressionVisitor visitor)
        {
            return visitor.VisitFoo(this);
        }
    }
}
