using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebMobile.Models
{
    public class WechatBaseRequestModel
    {
        public string signature { get; set; }
        public string timestamp { get; set; }

        public string nonce { get; set; }

        public string echostr { get; set; }
    }
}
