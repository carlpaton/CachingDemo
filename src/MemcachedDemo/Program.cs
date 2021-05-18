using System;
using Microsoft.Extensions.DependencyInjection;

namespace MemcachedDemo
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
            var valFromCache = cacheRepository.Get<string>(key);

            Console.WriteLine($"Value from cache = `{valFromCache}`");


            // more meaningful key/values
            var key2 = "1:42:seed_string";
            var val2 = "{\"Id\":4603,\"SomeGuid\":\"7f944b94-8008-4c48-a7e8-aa4c1be1772f\",\"DateTime\":\"2021-05-12T03:30:40.3217943+00:00\"}\"";
            cacheRepository.Set(key2, val2);

            var valFromCache2 = cacheRepository.Get<string>(key2);
            Console.WriteLine($"Value from cache = `{valFromCache2}`");
        }
    }
}
