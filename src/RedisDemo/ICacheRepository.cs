namespace RedisDemo
{
    internal interface ICacheRepository
    {
        string Get(string key);
        void Set(string key, string value);
    }
}
