using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Linq;
using Blog.Common;
using Dapper;
using Microsoft.Extensions.Options;

namespace Blog.Infrastructure
{
    public class DefaultRepository<T> :IRepository<T> where T : class,new()
    {
        #region Properties
        protected String MySql1 { get; private set; }
        #endregion     

        #region Constructor

        protected readonly IOptions<AppSetting> _appString; 
        
        public DefaultRepository(IOptions<AppSetting> appSetting)
        {
            MySql1 = appSetting.Value.Mysql;
            _appString = appSetting;
        }

        #endregion

        #region Public Method

        public virtual T Query<T>(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null,CommandType commandType=CommandType.Text)
        {
            using (var con = GetDbConnetion())
            {
                return con.MyQuery<T>(QueryStr, Parms, transaction,commandTimeout, commandType);
            }
        }

        public virtual async Task<T> QueryAsync<T>(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (var con = GetDbConnetion())
            {
                return await con.MyQueryAsync<T>(QueryStr, Parms, transaction, commandTimeout,commandType);
            }
        }

        public virtual Int32 Execute(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (var con = GetDbConnetion())
            {
                return con.Execute(QueryStr, Parms, transaction, commandTimeout,commandType);
            }
        }

        public virtual async Task<Int32> ExecuteAsync(string QueryStr, object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType commandType = CommandType.Text)
        {
            using (var con = GetDbConnetion())
            {
                return await con.ExecuteAsync(QueryStr, Parms, transaction, commandTimeout,commandType);
            }
        }

        public virtual IEnumerable<T> QueryList<T>(string QueryStr, Object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var con = GetDbConnetion())
            {
                return con.Query<T>(QueryStr, Parms, transaction, true, commandTimeout);
            }
        }
        public virtual IEnumerable<T> QueryList(object condition, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            using (var con = GetDbConnetion())
            {
                return con.QueryList<T>(condition, typeof(T).Name.ToLower(), columns = "*", isOr, transaction, commandTimeout);
            }
        }

        public virtual async Task<IEnumerable<T>> QueryListAsync<T>(string QueryStr, Object Parms = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            using (var con = GetDbConnetion())
            {
                return await con.QueryAsync<T>(QueryStr, Parms, transaction,commandTimeout);
            }
        }

        public virtual async Task<IEnumerable<T>> QueryListAsync(object condition, String columns = "*", bool isOr = false, IDbTransaction transaction = null, Int32? commandTimeout = null)
        {
            using (var con = GetDbConnetion())
            {
                return await con.QueryListAsync<T>(condition, typeof(T).Name.ToLower(), columns = "*",isOr ,transaction, commandTimeout);
            }
        }


        public virtual T FindByProperties(Object condition, String column = "*")
        {
            using (var con = GetDbConnetion())
            {
                var entity= con.QueryList<T>(condition, typeof(T).Name.ToLower(), column);
                return entity.FirstOrDefault();
            }
        }

        public virtual async Task<T> FindByPropertiesAsync(Object condition, String column = "*")
        {
            try
            {
                using (var con = GetDbConnetion())
                {
                    var entity = await con.QueryListAsync<T>(condition, typeof(T).Name.ToLower(), column);

                    return entity.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }

        public virtual Boolean Insert(Object entity)
        {
            using (var con = GetDbConnetion())
            {
                var result = con.Insert(entity, typeof(T).Name.ToLower());
                return result > 0;
            }
        }
        public virtual async Task<Boolean> InsertAsync(Object entity)
        {
            using (var con = GetDbConnetion())
            {
                var result = await con.InsertAsync(entity, typeof(T).Name.ToLower());
               return result > 0;
            }
        }

        public virtual Boolean Modify(Object entity, Object condition)
        {
            using (var con = GetDbConnetion())
            {
               var result = con.Update(entity, condition, typeof(T).Name.ToLower()) ;
                return result>0;
            }
        }

        public virtual async Task<Boolean> ModifyAsync(Object entity, Object condition)
        {
            using (var con = GetDbConnetion())
            {
                var result = await con.UpdateAsync(entity, condition, typeof(T).Name.ToLower());
               
                return result>0;
            }
        }

        public virtual IEnumerable<T> QueryPaged(Object condition,String orderby, int index = 1, int size = 10, String column = "*")
        {
            using (var con = GetDbConnetion())
            {
                var data = con.QueryPaged<T>(condition, typeof(T).Name.ToLower(), orderby, index, size, column);
                return data;
            }
        }

        public virtual async Task<IEnumerable<T>> QueryPagedAsync(Object condition, String orderby, int index = 1, int size = 10, String column = "*")
        {
            using (var con = GetDbConnetion())
            {
                var list = await con.QueryPagedAsync<T>(condition, typeof(T).Name.ToLower(), orderby, index, size,column);
               return list;
            }
        }

        public virtual Boolean IsExistRec(object condition)
        {
            using (var con = GetDbConnetion())
            {
                var list = con.GetCount(condition,typeof(T).Name.ToLower());;
                return list > 0;
            }
        }

        public virtual async Task<Boolean> IsExistRecAsync(object condition)
        {
            using (var con = GetDbConnetion())
            {
                var list = await con.GetCountAsync(condition, typeof(T).Name.ToLower());
               
                return list > 0;
            }
        }

        public virtual Int32 GetTotalCount(object condition)
        {
            using (var con = GetDbConnetion())
            {
                var count =  con.GetCount(condition, typeof(T).Name.ToLower());

                return count;
            }
        }

        public virtual async Task<Int32> GetTotalCountAsync(object condition)
        {
            using (var con = GetDbConnetion())
            {
                var count = await con.GetCountAsync(condition, typeof(T).Name.ToLower());

                return count;
            }
        }

        #endregion

        protected virtual MySqlConnection GetDbConnetion()
        {
            return new MySqlConnection(MySql1);
        }
    }
}
