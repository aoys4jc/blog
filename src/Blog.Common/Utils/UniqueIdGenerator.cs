using System;
using System.Threading.Tasks;

namespace Blog.Common
{
   public class UniqueIdGenerator
   {
      
        /// <summary>
        /// 生成订单Id
        /// </summary>
        /// <returns></returns>
        public static String GenerateOrderId()
        {
            return OrderIdGenerator.GenerateOrderNumber();
        }

        /// <summary>
        /// 生成指定位数的随机密码
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String GenerateRandPassword(Int32 length)
        {
            //声明要返回的字符串    
            string tmpstr = "";
            //密码中包含的字符数组    
            string pwdchars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            //数组索引随机数    
            int iRandNum;
            //随机数生成器    
            Random rnd = new Random();
            for (int i = 0; i < length; i++)
            {      //Random类的Next方法生成一个指定范围的随机数     
                iRandNum = rnd.Next(pwdchars.Length);
                //tmpstr随机添加一个字符     
                tmpstr += pwdchars[iRandNum];
            }
            return tmpstr;
        }

        /// <summary>
        /// 生成指定位数的随机数字
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static String GenerateCheckCode(int length)
        {
            return VerifyCodeGenerator.Next(length);
        }

       /// <summary>
       /// 生成唯一的字符串
       /// </summary>
       /// <returns></returns>
       public static String GenerateUniqueId()
       {
           return ObjectId.GenerateNewStringId();
       }

        /// <summary>
        /// 生成Id
        /// </summary>
        /// <returns></returns>
        public static ObjectId GenerateId()
        {
            return ObjectId.GenerateNewId();
        }
    }
}
