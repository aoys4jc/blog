using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Blog.Common;
using StackExchange.Redis;

namespace Blog.Infrastructure
{
    public static class RedisHelper
    {
        public static bool HashSet<T>(this IDatabase client, string hashid, string key, T vaule)
        {
            try
            {
                return client.HashSetAsync(hashid, key, ProtobufHelper.Serialize<T>(vaule)).Result;
            }
            catch
            {
                return false;
            }
        }
        public static async Task HashSetAsync<T>(this IDatabase client, string hashid, string key, T vaule, bool IsFireAndForget = true)
        {

            if (IsFireAndForget)
            {
                await client.HashSetAsync(hashid, key, ProtobufHelper.Serialize<T>(vaule), flags: CommandFlags.FireAndForget);
            }
            else
            {
                await client.HashSetAsync(hashid, key, ProtobufHelper.Serialize<T>(vaule));
            }
        }

        public static T HashGet<T>(this IDatabase client, string hashid, string key)
        {
            try
            {
                var result = client.HashGetAsync(hashid, key).Result;
                if (!result.IsNullOrEmpty)
                {
                    return ProtobufHelper.DeSerialize<T>(result);
                }
                return default(T);

            }
            catch
            {
                return default(T);
            }

        }
        /// <summary>
        /// 获取单个key value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Get<T>(this IDatabase client, string key)
        {
            try
            {
                var result = client.StringGetAsync(key).Result;
                if (!result.IsNullOrEmpty)
                {
                    return ProtobufHelper.DeSerialize<T>(result);
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }

        }
        public static async Task<T> GetAsync<T>(this IDatabase client, string key)
        {
            var response = default(T);
            try
            {
                var result = await client.StringGetAsync(key);
                if (!result.IsNullOrEmpty)
                {
                    response = ProtobufHelper.DeSerialize<T>(result);
                }
                return response;
            }
            catch
            {
                return response;
            }

        }

        public static async Task<T> GetByJsonAsync<T>(this IDatabase client, string key)
        {
            var response = default(T);
            try
            {
                var result = await client.StringGetAsync(key);
                if (!result.IsNullOrEmpty)
                {
                    response = ObjectUtils.ToObj<T>(result);
                }
                return response;
            }
            catch
            {
                return response;
            }

        }
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="extime"></param>
        public static bool Set<T>(this IDatabase client, string key, T value, TimeSpan? extime = default(TimeSpan?))
        {
            try
            {
                return client.StringSetAsync(key, ProtobufHelper.Serialize<T>(value)).Result;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<Boolean> SetAsync<T>(this IDatabase client, string key, T value, TimeSpan? extime = default(TimeSpan?), bool IsFireAndForget = true)
        {

            if (IsFireAndForget)
            {
                return await client.StringSetAsync(key, ProtobufHelper.Serialize<T>(value), extime, flags: CommandFlags.FireAndForget);
            }
            else
            {
                return await client.StringSetAsync(key, ProtobufHelper.Serialize<T>(value), extime);
            }
        }

        public static async Task<Boolean> SetByJsonAsync<T>(this IDatabase client, string key, T value, TimeSpan? extime = default(TimeSpan?), bool IsFireAndForget = true)
        {

            if (IsFireAndForget)
            {
                return await client.StringSetAsync(key, ObjectUtils.ToString<T>(value), extime, flags: CommandFlags.FireAndForget);
            }
            else
            {
                return await client.StringSetAsync(key, ObjectUtils.ToString<T>(value), extime);
            }
        }

        /// <summary>
        /// 返回集合的所有数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static IEnumerable<T> SetMembers<T>(this IDatabase client, string key)
        {
            try
            {
                var values = client.SetMembersAsync(key).Result;
                if (values == null)
                {
                    return null;
                }
                var list = new List<T>();
                foreach (var item in values)
                {
                    list.Add(ProtobufHelper.DeSerialize<T>((string)item));
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 添加单个集合单条数据
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool SetAdd<T>(this IDatabase client, string key, T value)
        {
            try
            {
                return client.SetAddAsync(key, ProtobufHelper.Serialize<T>(value)).Result;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 将一个值value插入到列表key的表尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 ListRightPush<T>(this IDatabase client, string key, T value)
        {
            try
            {
                return client.ListRightPush(key, ProtobufHelper.Serialize<T>(value));
            }
            catch
            {
                return 0;
            }
        }
        public static async Task<Int64> ListRightPushAsync<T>(this IDatabase client, string key, T value)
        {
            try
            {
                return await client.ListRightPushAsync(key, ProtobufHelper.Serialize<T>(value));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将一个值value插入到列表key的表头
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Int64 ListLeftPush<T>(this IDatabase client, string key, T value)
        {
            try
            {
                return client.ListLeftPush(key, ProtobufHelper.Serialize<T>(value));
            }
            catch
            {
                return 0;
            }
        }
        public static async Task<Int64> ListLeftPushAsync<T>(this IDatabase client, string key, T value)
        {
            try
            {
                return await client.ListLeftPushAsync(key, ProtobufHelper.Serialize<T>(value));
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// 将一个值从key的表头取出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ListLeftPop<T>(this IDatabase client, string key)
        {
            try
            {
                return ProtobufHelper.DeSerialize<T>((String)client.ListLeftPop(key));
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将一个值从key的表尾取出
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T ListRightPop<T>(this IDatabase client, string key)
        {
            try
            {
                return ProtobufHelper.DeSerialize<T>((String)client.ListRightPop(key));
            }
            catch
            {
                return default(T);
            }
        }
        /// <summary>
        /// 返回列表key中指定区间内的元素，区间以偏移量start和stop指定。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="client"></param>
        /// <param name="key"></param>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <returns></returns>
        public static IEnumerable<T> ListRange<T>(this IDatabase client, string key, Int64 start = 0, Int64 stop = -1)
        {
            try
            {
                var list = new List<T>();
                var values = client.ListRangeAsync(key, start, stop).Result;
                if (values == null)
                {
                    return null;
                }
                foreach (var item in values)
                {
                    list.Add(ProtobufHelper.DeSerialize<T>(item));
                }
                return list;
            }
            catch
            {
                return null;
            }
        }

    }
}
