using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using SimpleLinq.Expressions;

namespace SimpleLinq
{
    internal sealed class WhereExpressionVisitor : ExpressionVisitor
    {
        public static List<ParsedExpression> GetParsedExpressions(Expression predicate)
        {
            var visitor = new WhereExpressionVisitor();
            visitor.Visit(predicate);
            return visitor.parsedExpressions;
        }

        private WhereExpressionVisitor()
        {
            this.parsedExpressions = new List<ParsedExpression>();
        }

        private List<ParsedExpression> parsedExpressions;

        protected override Expression VisitBinary(BinaryExpression node)
        {
            var comparison = this.GetBinaryComparisonType(node.NodeType);

            if (node.Left.NodeType == ExpressionType.MemberAccess &&
                node.Right.NodeType == ExpressionType.Constant)
            {
                this.parsedExpressions.Add(
                    new WhereMemberExpression(
                        (node.Left as MemberExpression).Member.Name,
                        (string)(node.Right as ConstantExpression).Value,
                        comparison));

                return node; // done
            }

            if (node.Left.NodeType == ExpressionType.Constant &&
                node.Right.NodeType == ExpressionType.MemberAccess)
            {
                this.parsedExpressions.Add(
                    new WhereMemberExpression(
                        (node.Right as MemberExpression).Member.Name,
                        (string)(node.Left as ConstantExpression).Value,
                        comparison));

                return node; // done
            }

            throw new NotSupportedException("A Where expression must contain a method call, or member access and constant expressions.");
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            bool correctOverload =
                node.Arguments.Count == 1 &&
                node.Arguments[0].NodeType == ExpressionType.Constant;
            if (!correctOverload)
                throw new NotSupportedException($"The method {node.Method} is not supported.");

            if (node.Method.Name == "Equals")
            {
                this.parsedExpressions.Add(
                    new WhereMemberExpression(
                        (node.Object as MemberExpression).Member.Name,
                        (string)(node.Arguments[0] as ConstantExpression).Value,
                        WhereComparison.Equals));

                return node; // done
            }

            if (node.Method.Name == "StartsWith")
            {
                this.parsedExpressions.Add(
                    new WhereMemberExpression(
                        (node.Object as MemberExpression).Member.Name,
                        (string)(node.Arguments[0] as ConstantExpression).Value,
                        WhereComparison.StartsWith));

                return node; // done
            }

            if (node.Method.Name == "EndsWith")
            {
                this.parsedExpressions.Add(
                    new WhereMemberExpression(
                        (node.Object as MemberExpression).Member.Name,
                        (string)(node.Arguments[0] as ConstantExpression).Value,
                        WhereComparison.EndsWith));

                return node; // done
            }

            if (node.Method.Name == "Contains")
            {
                this.parsedExpressions.Add(
                    new WhereMemberExpression(
                        (node.Object as MemberExpression).Member.Name,
                        (string)(node.Arguments[0] as ConstantExpression).Value,
                        WhereComparison.Contains));

                return node; // done
            }

            throw new NotSupportedException($"The method {node.Method.Name} is not supported.");
        }

        private WhereComparison GetBinaryComparisonType(ExpressionType type)
        {
            if (type == ExpressionType.Equal)
                return WhereComparison.Equals;

            throw new NotSupportedException($"The comparison operator {type} is not supported.");
        }
    }
}
