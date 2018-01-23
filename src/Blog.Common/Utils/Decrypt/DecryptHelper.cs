using System;
using System.Security.Cryptography;
using System.Text;

namespace Blog.Common
{
    public class DecryptHelper
    {
        public static string EncrytMd5FromString(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            if (buffer.Length == 0) return String.Empty;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                sb.Append(buffer[i].ToString("X2"));
            }
            return sb.ToString();
        } 
    }

}
