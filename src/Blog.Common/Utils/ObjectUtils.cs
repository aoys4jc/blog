using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Blog.Common
{
    public static class ObjectUtils
    {
        public static string ToString<T>(T obj, bool isIndented = false)
        {
            try
            {
                if (obj != null)
                {
                    return JsonConvert.SerializeObject(obj, Formatting.Indented);
                }
                return default(String);
            }
            catch
            {
                return default(String);
            }
        }

        public static T ToObj<T>(string data)
        {
            try
            {
                if (!String.IsNullOrEmpty(data))
                {
                    return JsonConvert.DeserializeObject<T>(data);
                }
                return default(T);
            }
            catch
            {
                return default(T);
            }
        }
        public static Dictionary<string, T> ObjectToDic<T>(object data)
        {
            var str = ToString(data);
            return StringToDic<T>(str);
        }

        public static Dictionary<string, T> StringToDic<T>(string data)
        {
            return ToObj<Dictionary<string, T>>(data);
        }

        private static readonly ConcurrentDictionary<Type, List<PropertyInfo>> _paramCache = new ConcurrentDictionary<Type, List<PropertyInfo>>();

        public static T ConvertType<T>(object value)
        {
            var properties = GetPropertyInfos(value);
            var model = Activator.CreateInstance<T>();
            var modelProperty = GetPropertyInfos(model);
            foreach (var item in properties)
            {
                foreach (var prop in modelProperty)
                {
                    if (item.Name.ToLower() == prop.Name.ToLower())
                    {
                        prop.SetValue(model, item.GetValue(value, null));
                    }
                }
            }
            return model;
        }

        public static Dictionary<String, String> ConvertToDic(object value)
        {
            var properties = GetPropertyInfos(value);
            var dic = new Dictionary<String, String>();
            foreach (var item in properties)
            {
                dic.Add(item.Name.ToLower(), item.GetValue(value, null).ToString());
            }
            return dic;
        }

        public static Dictionary<String, String> FileConvert2Dic(Object value)
        {
            var properties = GetPropertyInfos(value);
            var dic = new Dictionary<String, String>();
            foreach (var item in properties)
            {
                if (item.Name.ToLower() == "file")
                {
                    dic.Add(item.Name.ToLower(), JsonConvert.SerializeObject(item.GetValue(value, null)));
                }
                else
                {
                    dic.Add(item.Name.ToLower(), item.GetValue(value, null).ToString());
                }

            }
            return dic;
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
