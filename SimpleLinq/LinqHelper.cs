using System;
using System.Linq.Expressions;
using System.Reflection;

namespace SimpleLinq1
{
    internal static class LinqHelper
    {
        public static Expression MethodCall(MethodInfo method, params Expression[] expressions)
            => Expression.Call(null, method, expressions);

        public static MethodInfo GetMethodInfo<T1, T2>(Func<T1, T2> func, T1 ignored)
            => func.Method;

        public static MethodInfo GetMethodInfo<T1, T2, T3>(Func<T1, T2, T3> func, T1 ignored1, T2 ignored2)
            => func.Method;
    }
}