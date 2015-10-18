using System.Linq.Expressions;

namespace SimpleLinq1.Expressions
{
    internal class OrderByExpression : ParsedExpression
    {
        public OrderByExpression(string fieldName, OrderByDirection direction)
        {
            this.FieldName = fieldName;
            this.Direction = direction;
        }

        public string FieldName { get; private set; }

        public OrderByDirection Direction { get; private set; }

        protected internal override Expression Accept(CompilingExpressionVisitor visitor)
        {
            return visitor.VisitOrderBy(this);
        }
    }
}
