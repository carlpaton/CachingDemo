using Microsoft.Extensions.DependencyInjection;
using System;

namespace RedisDemo
{
    internal static class ContainerConfiguration
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();

            services.AddDistributedRedisCache(option =>
            {
                option.Configuration = "localhost:6379,allowAdmin=true";
                option.InstanceName = "foo-redis";
            });

            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services.BuildServiceProvider();
        }
    }
}
