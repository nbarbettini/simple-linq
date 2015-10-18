using System.Linq.Expressions;

namespace SimpleLinq.Expressions
{
    internal class WhereMemberExpression : ParsedExpression
    {
        public WhereMemberExpression(string field, string value, WhereComparison comparison)
        {
            this.FieldName = field;
            this.Value = value;
            this.Comparison = comparison;
        }

        public string FieldName { get; private set; }

        public string Value { get; private set; }

        public WhereComparison Comparison { get; private set; }

        protected internal override Expression Accept(CompilingExpressionVisitor visitor)
        {
            return visitor.VisitWhereMember(this);
        }
    }
}
