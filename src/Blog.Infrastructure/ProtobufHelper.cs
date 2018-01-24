using System;
using System.IO;
using ProtoBuf;

namespace Blog.Infrastructure
{
    public class ProtobufHelper
    {
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string Serialize<T>(T t)
        {
            if (t == null)
            {
                return String.Empty;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                Serializer.Serialize<T>(ms, t);
                ArraySegment<byte> buffer;
                if (!ms.TryGetBuffer(out buffer))
                {
                    throw new InvalidOperationException();
                }
                return Convert.ToBase64String(buffer.Array, buffer.Offset, buffer.Count);
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(string content)
        {

            if (String.IsNullOrEmpty(content))
            {
                return default(T);
            }
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(content)))
            {
                T t = Serializer.Deserialize<T>(ms);
                return t;
            }
        }
    }
}
