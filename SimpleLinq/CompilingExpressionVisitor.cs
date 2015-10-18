using System;
using System.Linq.Expressions;
using SimpleLinq1.Expressions;
using SimpleLinq1.Expressions.ResultOperators;

namespace SimpleLinq1
{
    internal class CompilingExpressionVisitor : ExpressionVisitor
    {
        public QueryModel Model { get; private set; }

        public CompilingExpressionVisitor()
        {
            Model = new QueryModel();
        }

        protected internal Expression VisitParsedExpression(ParsedExpression node)
        {
            throw new NotSupportedException($"{node.GetType()} is an unsupported expression.");
        }

        protected internal Expression VisitTake(TakeExpression node)
        {
            Model.Take = node.Value;

            return node;
        }

        protected internal Expression VisitSkip(SkipExpression node)
        {
            Model.Skip = node.Value;

            return node;
        }

        protected internal Expression VisitFoo(FooExpression node)
        {
            Model.Foo = node.Value;

            return node;
        }

        protected internal Expression VisitWhereMember(WhereMemberExpression node)
        {
            Model.Wheres.Add(new WhereItem()
            {
                FieldName = node.FieldName,
                Comparison = node.Comparison,
                Value = node.Value
            });

            return node;
        }

        protected internal Expression VisitOrderBy(OrderByExpression node)
        {
            Model.OrderBys.Add(new OrderByItem()
            {
                FieldName = node.FieldName,
                Direction = node.Direction
            });

            return node;
        }

        protected internal Expression VisitResultOperator(ResultOperatorExpression node)
        {
            Model.ResultOperator = node.ResultType;

            if (node.ResultType == ResultOperator.First)
            {
                Model.DefaultIfEmpty = (node as FirstResultOperator).DefaultIfEmpty;
            }
            else if (node.ResultType == ResultOperator.Single)
            {
                Model.DefaultIfEmpty = (node as SingleResultOperator).DefaultIfEmpty;
            }

            return node;
        }
    }
}
