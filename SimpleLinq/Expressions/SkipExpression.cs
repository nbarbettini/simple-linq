﻿using System.Linq.Expressions;

namespace SimpleLinq.Expressions
{
    internal class SkipExpression : ParsedExpression
    {
        public SkipExpression(int value)
        {
            this.Value = value;
        }

        public int Value { get; private set; }

        protected internal override Expression Accept(CompilingExpressionVisitor visitor)
        {
            return visitor.VisitSkip(this);
        }
    }
}
