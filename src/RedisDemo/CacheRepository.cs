using Microsoft.Extensions.Caching.Distributed;

namespace RedisDemo 
{
    internal class CacheRepository : ICacheRepository
    {
        private readonly IDistributedCache _distributedCache;

        public CacheRepository(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public string Get(string key)
        {
            return _distributedCache.GetString(key);
        }

        public void Set(string key, string value)
        {
            _distributedCache.SetString(key, value);
        }
    }
}
