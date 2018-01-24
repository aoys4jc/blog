using System;
using Blog.Common;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace Blog.Infrastructure
{
    public class RedisDb
    {
        protected readonly IOptions<AppSetting> _appSetting;
        protected static String _defaultStr { get; private set; }
        public IDatabase DefaultClient { get; set; }
        public ISubscriber DefaultScriber { get; set; }
        public RedisDb(IOptions<AppSetting> appSetting)
        {
            _defaultStr = appSetting.Value.Redis;
           var _defaultConn = new Lazy<ConnectionMultiplexer>(
                 () => ConnectionMultiplexer.Connect(_defaultStr));
            DefaultClient = _defaultConn.Value.GetDatabase();
            DefaultScriber = _defaultConn.Value.GetSubscriber();
        }
    }
}
