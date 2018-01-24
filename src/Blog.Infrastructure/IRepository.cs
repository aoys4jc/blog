using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Blog.Infrastructure
{
    public interface IRepository<T> where T: class ,new()
    {

        T Query<T>(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null,CommandType commandType=CommandType.Text);
        Task<T> QueryAsync<T>(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);
        IEnumerable<T> QueryList(object condition, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null);
        Task<IEnumerable<T>> QueryListAsync(object condition, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null);
        IEnumerable<T> QueryList<T>(string QueryStr, Object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null);
        Task<IEnumerable<T>> QueryListAsync<T>(string QueryStr, Object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null);

        Int32 Execute(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);
        Task<Int32> ExecuteAsync(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text);

        Boolean Insert(Object entity);
        Task<Boolean> InsertAsync(Object entity);
        Boolean Modify(Object entity, Object condition);
        Task<Boolean> ModifyAsync(Object entity, Object condition);
        T FindByProperties(Object condition, String column = "*");
        Task<T> FindByPropertiesAsync(Object condition, String column = "*");

        IEnumerable<T> QueryPaged(Object condition, String orderby, int index = 1, int size = 10, String column = "*");
        Task<IEnumerable<T>> QueryPagedAsync(Object condition,String orderby, int index = 1, int size = 10,String column="*");

        Boolean IsExistRec(Object condition);
        Task<Boolean> IsExistRecAsync(Object condition);

        Int32 GetTotalCount(Object condition);
        Task<Int32> GetTotalCountAsync(Object condition);
    }
}
