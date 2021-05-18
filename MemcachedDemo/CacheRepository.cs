using Enyim.Caching;

namespace MemcachedDemo
{
    internal class CacheRepository : ICacheRepository
    {
        private readonly IMemcachedClient _memcachedClient;
        private readonly int _cacheSeconds = 60;

        public CacheRepository(IMemcachedClient memcachedClient)
        {
            _memcachedClient = memcachedClient;
        }

        public T Get<T>(string key)
        {
            return _memcachedClient.Get<T>(key);
        }

        public void Set<T>(string key, T value)
        {
            _memcachedClient.Set(key, value, _cacheSeconds);
        }
    }
}
