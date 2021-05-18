namespace MemcachedDemo
{
    internal interface ICacheRepository
    {
        T Get<T>(string key);
        void Set<T>(string key, T value);
    }
}
