using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;

namespace Blog.Infrastructure
{
    /// <summary>Dapper extensions.
    /// </summary>
    public static class MySqlMapperExtensions
    {
        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _paramCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();


        public static T MyQuery<T>(this IDbConnection connection, string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            return connection.Query<T>($"{QueryStr} ", Parms, transaction, true, commandTimeout,commandType).FirstOrDefault();
        }

        public static async Task<T> MyQueryAsync<T>(this IDbConnection connection, string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {

            var data = await connection.QueryAsync<T>($"{QueryStr} ", Parms, transaction, commandTimeout,commandType);
            return data.FirstOrDefault();
        }
        /// <summary>Insert data into table.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Int32 Insert(this IDbConnection connection, dynamic data, String table, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var columns = String.Join(",", properties);
            var values = String.Join(",", properties.Select(p => "@" + p));
            var sql = String.Format("insert into {0} ({1}) values ({2});", table, columns, values);

            return connection.Execute(sql, obj, transaction, commandTimeout);
        }
        /// <summary>Insert data async into table.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<Int32> InsertAsync(this IDbConnection connection, dynamic data, String table, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var obj = data as object;
            var properties = GetProperties(obj);
            var columns = String.Join(",", properties);
            var values = String.Join(",", properties.Select(p => "@" + p));
            var sql = String.Format("insert into `{0}` ({1}) values ({2});", table, columns, values);

            return connection.ExecuteAsync(sql, obj, transaction, commandTimeout);
        }

        /// <summary>Updata data for table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Int32 Update(this IDbConnection connection, dynamic data, dynamic condition, String table, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var obj = data as object;
            var conditionObj = condition as object;

            var updatePropertyInfos = GetPropertyInfos(obj);
            var wherePropertyInfos = GetPropertyInfos(conditionObj);

            var updateProperties = updatePropertyInfos.Select(p => p.Name);
            var whereProperties = wherePropertyInfos.Select(p => p.Name);

            var updateFields = String.Join(",", updateProperties.Select(p => p + " = @" + p));
            var whereFields = String.Empty;

            if (whereProperties.Any())
            {
                whereFields = " where " + String.Join(" and ", whereProperties.Select(p => p + " = @w_" + p));
            }

            var sql = String.Format("update {0} set {1}{2}", table, updateFields, whereFields);

            var parameters = new DynamicParameters(data);
            var expandoObject = new ExpandoObject() as IDictionary<String, object>;
            wherePropertyInfos.ForEach(p => expandoObject.Add("w_" + p.Name, p.GetValue(conditionObj, null)));
            parameters.AddDynamicParams(expandoObject);

            return connection.Execute(sql, parameters, transaction, commandTimeout);
        }
        /// <summary>Updata data async for table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="data"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<Int32> UpdateAsync(this IDbConnection connection, dynamic data, dynamic condition, String table, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var obj = data as object;
            var conditionObj = condition as object;

            var updatePropertyInfos = GetPropertyInfos(obj);
            var wherePropertyInfos = GetPropertyInfos(conditionObj);

            var updateProperties = updatePropertyInfos.Select(p => p.Name);
            var whereProperties = wherePropertyInfos.Select(p => p.Name);

            var updateFields = String.Join(",", updateProperties.Select(p => p + " = @" + p));
            var whereFields = String.Empty;

            if (whereProperties.Any())
            {
                whereFields = " where " + String.Join(" and ", whereProperties.Select(p => p + " = @w_" + p));
            }

            var sql = String.Format("update {0} set {1}{2}", table, updateFields, whereFields);

            var parameters = new DynamicParameters(data);
            var expandoObject = new ExpandoObject() as IDictionary<String, object>;
            wherePropertyInfos.ForEach(p => expandoObject.Add("w_" + p.Name, p.GetValue(conditionObj, null)));
            parameters.AddDynamicParams(expandoObject);

            return connection.ExecuteAsync(sql, parameters, transaction, commandTimeout);
        }

        /// <summary>Delete data from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Int32 Delete(this IDbConnection connection, dynamic condition, String table, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = String.Empty;
            var whereProperties = GetProperties(conditionObj);
            if (whereProperties.Count > 0)
            {
                whereFields = " where " + String.Join(" and ", whereProperties.Select(p => p + " = @" + p));
            }

            var sql = String.Format("delete from {0}{1}", table, whereFields);

            return connection.Execute(sql, conditionObj, transaction, commandTimeout);
        }
        /// <summary>Delete data async from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<Int32> DeleteAsync(this IDbConnection connection, dynamic condition, String table, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = String.Empty;
            var whereProperties = GetProperties(conditionObj);
            if (whereProperties.Count > 0)
            {
                whereFields = " where " + String.Join(" and ", whereProperties.Select(p => p + " = @" + p));
            }

            var sql = String.Format("delete from {0}{1}", table, whereFields);

            return connection.ExecuteAsync(sql, conditionObj, transaction, commandTimeout);
        }

        /// <summary>Get data count from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Int32 GetCount(this IDbConnection connection, object condition, String table, bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return QueryList<Int32>(connection, condition, table, "count(*)", isOr, transaction, commandTimeout).Single();
        }
        /// <summary>Get data count async from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<Int32> GetCountAsync(this IDbConnection connection, object condition, String table, bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return QueryListAsync<Int32>(connection, condition, table, "count(*)", isOr, transaction, commandTimeout).ContinueWith<Int32>(t => t.Result.Single());
        }

        /// <summary>Query a list of data from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> QueryList(this IDbConnection connection, dynamic condition, String table, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return QueryList<dynamic>(connection, condition, table, columns, isOr, transaction, commandTimeout);
        }
        /// <summary>Query a list of data async from table with a specified condition.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> QueryListAsync(this IDbConnection connection, dynamic condition, String table, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return QueryListAsync<dynamic>(connection, condition, table, columns, isOr, transaction, commandTimeout);
        }
        /// <summary>Query a list of data from table with specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryList<T>(this IDbConnection connection, object condition, String table, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return connection.Query<T>(BuildQuerySQL(condition, table, columns, isOr), condition, transaction, true, commandTimeout);
        }
        /// <summary>Query a list of data async from table with specified condition.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> QueryListAsync<T>(this IDbConnection connection, object condition, String table, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return connection.QueryAsync<T>(BuildQuerySQL(condition, table, columns, isOr), condition, transaction, commandTimeout);
        }

        /// <summary>Query paged data from a single table.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<dynamic> QueryPaged(this IDbConnection connection, dynamic condition, String table, String orderBy, Int32 pageIndex, Int32 pageSize, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return QueryPaged<dynamic>(connection, condition, table, orderBy, pageIndex, pageSize, columns, isOr, transaction, commandTimeout);
        }
        /// <summary>Query paged data async from a single table.
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<dynamic>> QueryPagedAsync(this IDbConnection connection, dynamic condition, String table, String orderBy, Int32 pageIndex, Int32 pageSize, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            return QueryPagedAsync<dynamic>(connection, condition, table, orderBy, pageIndex, pageSize, columns, isOr, transaction, commandTimeout);
        }
        /// <summary>Query paged data from a single table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static IEnumerable<T> QueryPaged<T>(this IDbConnection connection, dynamic condition, String table, String orderBy, Int32 pageIndex, Int32 pageSize, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = String.Empty;
            var properties = GetProperties(conditionObj);
            if (properties.Count > 0)
            {
                var separator = isOr ? " OR " : " AND ";
                whereFields = " WHERE " + String.Join(separator, properties.Select(p => p + " = @" + p));
            }
            var sql = String.Format("SELECT {0} FROM {2}{3} ORDER BY {1} LIMIT {4},{5};", columns, orderBy, table, whereFields, (pageIndex - 1) * pageSize, pageSize);

            return connection.Query<T>(sql, conditionObj, transaction, true, commandTimeout);
        }
        /// <summary>Query paged data async from a single table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connection"></param>
        /// <param name="condition"></param>
        /// <param name="table"></param>
        /// <param name="orderBy"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="columns"></param>
        /// <param name="isOr"></param>
        /// <param name="transaction"></param>
        /// <param name="commandTimeout"></param>
        /// <returns></returns>
        public static Task<IEnumerable<T>> QueryPagedAsync<T>(this IDbConnection connection, dynamic condition, String table, String orderBy, Int32 pageIndex, Int32 pageSize, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            var conditionObj = condition as object;
            var whereFields = String.Empty;
            var properties = GetProperties(conditionObj);
            if (properties.Count > 0)
            {
                var separator = isOr ? " OR " : " AND ";
                whereFields = " WHERE " + String.Join(separator, properties.Select(p => p + " = @" + p));
            }
            var sql = String.Format("SELECT {0}  FROM {2}{3}  ORDER BY {1} LIMIT {4},{5};", columns, orderBy, table, whereFields, (pageIndex - 1) * pageSize, pageSize);

            return connection.QueryAsync<T>(sql, conditionObj, transaction, commandTimeout);
        }

        private static String BuildQuerySQL(dynamic condition, String table, String selectPart = "*", bool isOr = false)
        {
            var conditionObj = condition as object;
            var properties = GetProperties(conditionObj);
            if (properties.Count == 0)
            {
                return String.Format("SELECT {1} FROM `{0}`", table, selectPart);
            }

            var separator = isOr ? " OR " : " AND ";
            var wherePart = String.Join(separator, properties.Select(p => p + " = @" + p));

            return String.Format("SELECT {2} FROM `{0}` WHERE {1}", table, wherePart, selectPart);
        }
        private static List<String> GetProperties(object obj)
        {
            if (obj == null)
            {
                return new List<String>();
            }
            if (obj is DynamicParameters)
            {
                return (obj as DynamicParameters).ParameterNames.ToList();
            }
            return GetPropertyInfos(obj).Select(x => x.Name).ToList();
        }
        private static List<PropertyInfo> GetPropertyInfos(object obj)
        {
            if (obj == null)
            {
                return new List<PropertyInfo>();
            }

            List<PropertyInfo> properties;
            if (_paramCache.TryGetValue(obj.GetType(), out properties)) return properties.ToList();
            properties = obj.GetType().GetProperties(BindingFlags.GetProperty | BindingFlags.Instance | BindingFlags.Public).ToList();
            _paramCache[obj.GetType()] = properties;
            return properties;
        }
    }
}
