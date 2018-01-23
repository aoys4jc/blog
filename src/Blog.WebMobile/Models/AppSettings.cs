using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebMobile.Models
{
    public class AppSettings
    {
        public string AppID { get; set; }
        public string AppSecret { get; set; }

        public string Token { get; set; }

        public string EncodingAESKey { get; set; }

        public string HostName { get; set; }

        public string Redis { get; set; }

        public string MySQL { get; set; }
    }
}
