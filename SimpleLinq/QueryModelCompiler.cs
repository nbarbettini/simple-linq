using System.Linq.Expressions;

namespace SimpleLinq
{
    public class QueryModelCompiler
    {
        public static QueryModel Compile(Expression expression)
        {
            var compiler = new QueryModelCompiler();
            return compiler.GenerateQueryModel(expression);
        }

        private QueryModel GenerateQueryModel(Expression expression)
        {
            // Partial evaluation
            var evaluatedExpression = Evaluator.PartialEval(expression);

            // Discover
            var discoveringVisitor = new DiscoveringExpressionVisitor();
            discoveringVisitor.Visit(evaluatedExpression);

            // Compile
            var compilingVisitor = new CompilingExpressionVisitor();
            compilingVisitor.Visit(discoveringVisitor.Expressions);
            return compilingVisitor.Model;
        }
    }
}
