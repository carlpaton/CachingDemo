# Redis

> "Redis is an open source (BSD licensed), in-memory data structure store, used as a database, cache, and message broker. "

## Data types

Redis has several data types such as `String`, `Sorted Set`, `Set`, `List`, `Hash` and `Stream`, I've had a play with some of these in the past in [RedisAdministrator](https://github.com/carlpaton/RedisAdministrator/tree/master/RedisRepository) so for this cache demo I just used the simplest `String` type which is a key/value.

```json
{
	"some-string-key-42":"some string value that you need fast access to!"
}
```

- https://redis.io/topics/data-types
- https://redis.io/topics/data-types-intro

## Infrastructure

Spin up the docker container from the [redis](https://hub.docker.com/_/redis) image. This is the infrastructure that [Program.cs](Program.cs) will read/write to. I used [RedisAdministrator](https://github.com/carlpaton/RedisAdministrator/tree/master/RedisRepository) to view the data which needed a network between the isolated containers. Bridge is the simplest. 

```shell
docker network create --driver bridge redis-bridge-network
docker run --name foo-redis -d -p 6379:6379 --network redis-bridge-network redis redis-server --appendonly yes
docker run --name red-admin -d -p 8080:80 --network redis-bridge-network --env REDIS_CONNECTION=foo-redis,allowAdmin=true carlpaton/redis-administrator:latest
```

## Dependancy Injection

### AddStackExchangeRedisExtensions

TODO test with

```c#
// register 
services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>((options) =>
{
  return Configuration.GetSection("Redis").Get<RedisConfiguration>();
});

// inject
private readonly IRedisCacheClient _redisCacheClient;
public FooCtr(IRedisCacheClient redisCacheClient)
{
	_redisCacheClient = redisCacheClient;
}
```

### AddDistributedRedisCache

I tried adding to the container using `AddDistributedRedisCache` from `Microsoft.Extensions.DependencyInjection` but when consuming the service and adding a redis item it was added as a HASH when I was expecting STRING.

```c#
services.AddDistributedRedisCache(option =>
{
	option.Configuration = "localhost:6379,allowAdmin=true";
	option.InstanceName = "foo-redis";
});
```

```c#
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
```

## References 

- https://redis.io/
- https://github.com/carlpaton/RedisAdministrator/tree/master/RedisRepository
- https://dotnetcoretutorials.com/2017/01/06/using-redis-cache-net-core/
- https://www.learmoreseekmore.com/2020/11/stackexchange-redis-extension-library.html