﻿using System;
using System.Linq.Expressions;

namespace SimpleLinq
{
    internal sealed class SimpleExecutor
    {
        internal static object Execute(Expression expression, bool isEnumerable)
        {
            throw new NotSupportedException("Can't actually execute!");
        }
    }
}
