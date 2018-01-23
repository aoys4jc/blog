using System;

namespace Blog.Common
{
    public class VerifyCodeGenerator
    {
        public static string Next(int length)
        {
            string result = "";
            System.Random random = new Random();
            for (int i = 0; i < length; i++)
            {
                result += random.Next(10).ToString();
            }
            return result;
        }
    }
}
