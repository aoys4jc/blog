using System;

namespace Blog.Common
{
    public class AppSetting
    {
        /// <summary>
        /// 缓存库
        /// </summary>
        public string Redis { get; set; }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public String Server { get; set; }

        public string AppID { get; set; }
        public string AppSecret { get; set; }

        public string Token { get; set; }

        public string EncodingAESKey { get; set; }

        public string HostName { get; set; }

        /// <summary>
        ///  App库
        /// </summary>
        public string MySQL { get; set; }
    }
}
