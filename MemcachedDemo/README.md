# Memcached

> "Memcached is an in-memory key-value store for small chunks of arbitrary data (strings, objects) from results of database calls, API calls, or page rendering."

## Data types

Memcached has no data types, as it stores strings indexed by a string key which then has some value.

```json
{
	"some-string-key-42":"some string value that you need fast access to!"
}
```

The key can also be a composite and the value a serialized json object, they can be anything.
So for the below key `1:42:seed_string` the `1`, `42` and `seed_string` could be meaningful to your application.

```json
{
	"1:42:seed_string":"{"Id":1346,"SomeGuid":"b8319ace-b602-4b69-bf07-82f4fb50602a","DateTime":"2021-05-12T03:30:40.3195421+00:00"}"
}
```

## Infrastructure

Spin up the docker container from the [memcached](https://hub.docker.com/_/memcached) image. This is the infrastructure that [Program.cs](Program.cs) will read/write to.

```shell
docker run -p 11211:11211 --name foo-memcache -d memcached -m 64
```

## References 

- https://www.memcached.org/
- https://hub.docker.com/_/memcached
- https://dotnetcorecentral.com/blog/using-memcached-as-distributed-cache-in-net-core/
- https://aws.amazon.com/elasticache/memcached/