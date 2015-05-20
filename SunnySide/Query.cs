using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SunnySide
{
    public interface IQuery<T>// where T : class
    {
        IQuery<T> AddIsInCondition(string fieldName, IEnumerable<string> values);
        IEnumerable<T> Execute();
    }

    public class Query<T> : IQuery<T> where T : class
    {
        private ExpressionSqlTree _expression;
        private QueryType _queryType;
        private string _tableName;
        private string _isIn = string.Empty;

        public Query(Expression expression, QueryType queryType, string tableName)
        {
            _expression = new ExpressionSqlTree(expression);
            _queryType = queryType;
            _tableName = tableName;
        }

        public IQuery<T> AddIsInCondition(string fieldName, IEnumerable<string> values)
        {
            
            return this;
        }

        public IEnumerable<T> Execute()
        {
            return Enumerable.Empty<T>();
        }
    }

    public static class QueryExtensions
    {
        public static IQuery<Tsource> IsIn<Tsource, Tcollection, Tkey>(this IQuery<Tsource> query, Func<Tsource, Tkey> selector, IEnumerable<Tcollection> values)
        {
            return query;
            //query.AddIsInCondition(selector)
        }

        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return Enumerable.Empty<TResult>();
        }
    }

    public enum QueryType
    {
        Select = 0,
        Insert = 1,
        Update = 2,
        Delete = 3
    };
}
