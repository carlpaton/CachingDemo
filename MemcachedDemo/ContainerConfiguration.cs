using Enyim.Caching.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace MemcachedDemo
{
    internal static class ContainerConfiguration
    {
        public static IServiceProvider Configure()
        {
            var services = new ServiceCollection();
            services.AddLogging();

            services.AddEnyimMemcached(option =>
                option.Servers = new List<Server> { 
                    new Server { 
                        Address = "localhost", 
                        Port = 11211 
                    } 
                });

            services.AddSingleton<ICacheRepository, CacheRepository>();

            return services.BuildServiceProvider();
        }
    }
}
