using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SunnySide
{
    internal class EntityMetaData
    {
        private Type _type;
        private IEnumerable<PropertyInfo> _properties;

        public EntityMetaData (Type type)
	    {
            _type = type;
            this.InitializeMembers();
	    }

        private void InitializeMembers()
        {
 	        _properties = _type.GetProperties();
        }

        public T FromDataRow<T>(SqlDataReader reader) where T : class
        {
            T instance = Activator.CreateInstance(_type) as T;

            foreach (var property in _properties)
            {
                property.SetValue(instance, reader.GetValue(reader.GetOrdinal(property.Name)));
            }

            return instance;

        }
    }

    internal static class EntityMetaDataHolder
    {
        private static Dictionary<Type, EntityMetaData> _metaData = new Dictionary<Type, EntityMetaData>();

        internal static Dictionary<Type, EntityMetaData> MetaData { get { return _metaData; }}

        internal static void Add(Type type, EntityMetaData metaData)
        {
            _metaData.Add(type, metaData);
        }
    }

    public interface IDbTable<T> : IDbTable where T : class
    {
        IQuery<T> Select(Expression<Func<T, bool>> expression);
    }

    public interface IDbTable
    { }

    public class DbTable<T> : IDbTable<T> where T : class
    {
        private SqlConnection _connection;
        private string _tableName;

        public DbTable(SqlConnection connection, string tableName)
        {
            _connection = connection;
            _tableName = tableName;
        }

        public IQuery<T> Select(Expression<Func<T, bool>> expression)
        {
            return new Query<T>(expression, QueryType.Select, _tableName);
            //string query = expression.SqlExpression<T>();
            //SqlDataReader reader = this.ExecuteSql(string.Format("SELECT * FROM {0} WHERE {1}", _tableName, query));
            //var task = this.ReadToEntititesAsync(reader);
            //task.Wait();
            //IEnumerable<T> entities = task.Result;
            //return entities;
        }

        private async Task<IEnumerable<T>> ReadToEntititesAsync(SqlDataReader reader)
        {
            EntityMetaData metaData = EntityMetaDataHolder.MetaData[typeof(T)];
            ICollection<T> entities = new List<T>();

            while (await reader.ReadAsync())
            {
                entities.Add(metaData.FromDataRow<T>(reader));
            }

            reader.Close();
            return entities;
        }

        private SqlDataReader ExecuteSql(string query)
        {
            SqlCommand cmd = new SqlCommand(query, _connection);
            return cmd.ExecuteReader();
        }

        private void EnsureConnectionOpen()
        {
            
        }
    }

    internal static class ExpressionExtensions
    {
        public static string SqlExpression<T>(this Expression<Func<T, bool>> expression)
        {
            ExpressionSqlTree tree = new ExpressionSqlTree(expression.Body);
            return tree.ToString();
        }
    }

    public abstract class DbContext
    {
        private string _conStringName;

        private SqlConnection _connection;
        public SqlConnection Connection
        {
            get { return _connection; }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get { return _connectionString; }
        }
        
        public DbContext(string connectionStringOrName)
        {
            this.InitializeConnection(connectionStringOrName);
            _connection.Open();
            this.InitializeTables();
        }

        private void InitializeTables()
        {
            var properties = this.GetType().GetProperties().Where(p => typeof(IDbTable).IsAssignableFrom(p.PropertyType));
            Type generic = typeof(DbTable<>);

            foreach (var property in properties)
            {
                Type tableType = property.PropertyType.GenericTypeArguments.First();
                Type propertyType = generic.MakeGenericType(new Type[] { tableType });
                var instance = Activator.CreateInstance(propertyType, new object[2] { _connection, property.Name });
                property.SetValue(this, instance);
                EntityMetaDataHolder.Add(tableType, new EntityMetaData(tableType));
            }
        }

        private void InitializeConnection(string connectionStringOrName)
        {
            if(string.IsNullOrWhiteSpace(connectionStringOrName)) throw new ArgumentException("Value cannot be null or empty", "connectionStringOrName");

            var conString = ConfigurationManager.ConnectionStrings[connectionStringOrName];

            if(conString == null)
            {
                _connectionString = connectionStringOrName;
                _connection = new SqlConnection(_connectionString);
                return;
            }

            _connection = new SqlConnection(conString.ConnectionString);
        }
    }
}
