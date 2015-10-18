using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq.Expressions;
using SimpleLinq.Expressions;
using SimpleLinq.Expressions.ResultOperators;

namespace SimpleLinq
{
    internal sealed class DiscoveringExpressionVisitor : ExpressionVisitor
    {
        private readonly List<Expression> expressions;

        public ReadOnlyCollection<Expression> Expressions => new ReadOnlyCollection<Expression>(expressions);

        public DiscoveringExpressionVisitor()
        {
            this.expressions = new List<Expression>();
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            // Query operators
            if (HandleWhereMethod(node) ||
                HandleOrderByMethod(node) ||
                HandleThenByMethod(node) ||
                HandleTakeMethod(node) ||
                HandleSkipMethod(node) ||
                HandleFooExtensionMethod(node))
            {
                this.Visit(node.Arguments[0]);
                return node;
            }

            // Result operators
            if (HandleAnyResultOperator(node) ||
                HandleCountResultOperator(node) ||
                HandleFirstResultOperator(node) ||
                HandleSingleResultOperator(node))
            {
                this.Visit(node.Arguments[0]);
                return node;
            }

            throw new NotSupportedException($"The '{node.Method.Name}' method is unsupported.");
        }

        private bool HandleWhereMethod(MethodCallExpression node)
        {
            if (node.Method.Name != "Where")
                return false;

            var whereExpressions = WhereExpressionVisitor.GetParsedExpressions(node.Arguments[1]);
            this.expressions.AddRange(whereExpressions);

            return true;
        }

        private bool HandleOrderByMethod(MethodCallExpression node)
        {
            OrderByDirection? direction = null;

            if (node.Method.Name == "OrderBy")
                direction = OrderByDirection.Ascending;
            else if (node.Method.Name == "OrderByDescending")
                direction = OrderByDirection.Descending;
            else
                return false;

            var field = ((node.Arguments[1] as UnaryExpression)?.Operand as LambdaExpression)?.Body as MemberExpression;
            if (field == null)
                throw new NotSupportedException($"{node.Method.Name} must operate on a supported field.");

            expressions.Add(new OrderByExpression(field.Member.Name, direction.Value));

            return true;
        }

        private bool HandleThenByMethod(MethodCallExpression node)
        {
            OrderByDirection? direction = null;

            if (node.Method.Name == "ThenBy")
                direction = OrderByDirection.Ascending;
            else if (node.Method.Name == "ThenByDescending")
                direction = OrderByDirection.Descending;
            else
                return false;

            var field = ((node.Arguments[1] as UnaryExpression)?.Operand as LambdaExpression)?.Body as MemberExpression;
            if (field == null)
                throw new NotSupportedException($"{node.Method.Name} must operate on a supported field.");

            expressions.Add(new OrderByExpression(field.Member.Name, direction.Value));

            return true;
        }

        private bool HandleTakeMethod(MethodCallExpression node)
        {
            if (node.Method.Name != "Take")
                return false;

            var value = node.Arguments[1] as ConstantExpression;
            if (value == null)
                throw new ArgumentException("Value passed to Take operator is not supported.");

            expressions.Add(new TakeExpression((int)value.Value));

            return true;
        }

        private bool HandleSkipMethod(MethodCallExpression node)
        {
            if (node.Method.Name != "Skip")
                return false;

            var value = node.Arguments[1] as ConstantExpression;
            if (value == null)
                throw new ArgumentException("Value passed to Take operator is not supported.");

            expressions.Add(new SkipExpression((int)value.Value));

            return true;
        }

        private bool HandleFooExtensionMethod(MethodCallExpression node)
        {
            if (node.Method.Name != "Foo")
                return false;

            var value = node.Arguments[1] as ConstantExpression;
            if (value == null)
                throw new ArgumentException("Value passed to Take operator is not supported.");

            expressions.Add(new FooExpression((string)value.Value));

            return true;
        }

        private bool HandleAnyResultOperator(MethodCallExpression node)
        {
            if (node.Method.Name != "Any")
                return false;

            expressions.Add(new AnyResultOperator());

            return true;
        }

        private bool HandleCountResultOperator(MethodCallExpression node)
        {
            if (node.Method.Name != "Count")
                return false;

            expressions.Add(new CountResultOperator());

            return true;
        }

        private bool HandleFirstResultOperator(MethodCallExpression node)
        {
            bool? defaultIfEmpty = null;

            if (node.Method.Name == "First")
                defaultIfEmpty = false;
            else if (node.Method.Name == "FirstOrDefault")
                defaultIfEmpty = true;
            else
                return false;

            expressions.Add(new FirstResultOperator(defaultIfEmpty.Value));

            return true;
        }

        private bool HandleSingleResultOperator(MethodCallExpression node)
        {
            bool? defaultIfEmpty = null;

            if (node.Method.Name == "Single")
                defaultIfEmpty = false;
            else if (node.Method.Name == "SingleOrDefault")
                defaultIfEmpty = true;
            else
                return false;

            expressions.Add(new SingleResultOperator(defaultIfEmpty.Value));

            return true;
        }
    }
}
