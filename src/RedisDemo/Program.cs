using System;
using Microsoft.Extensions.DependencyInjection;

namespace RedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var provider = ContainerConfiguration.Configure();
            var cacheRepository = provider.GetService<ICacheRepository>();

            var key = "some-string-key-42";
            var val = "some string value that you need fast access to!";

            cacheRepository.Set(key, val);
            var valFromCache = cacheRepository.Get(key);

            Console.WriteLine($"REDIS: Value from cache = `{valFromCache}`");
        }
    }
}
