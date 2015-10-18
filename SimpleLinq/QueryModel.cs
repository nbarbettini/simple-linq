using System.Collections.Generic;

namespace SimpleLinq
{
    public class QueryModel
    {
        public int? Take { get; set; }
        
        public int? Skip { get; set; }

        public string Foo { get; set; }

        public List<WhereItem> Wheres { get; set; } = new List<WhereItem>();

        public List<OrderByItem> OrderBys { get; set; } = new List<OrderByItem>();

        public ResultOperator? ResultOperator { get; set; }

        public bool DefaultIfEmpty { get; set; }
    }
}
