using Pro.Ecare.Business.RedisConnection.Interfaces;
using Pro.Ecare.Business.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.Services.Classes
{
    public class RedisService : IRedisService
    {
        private readonly ConnectionMultiplexer _connection;

        public RedisService(IRedisConnectionFactory connectionFactory)
        {
            _connection = connectionFactory.GetConnection();
        }

        public async Task<string> Get(string key)
        {
            return await _connection.GetDatabase().StringGetAsync(key);
        }

        public async Task<RedisValue[]> Get(RedisKey[] redisKeys)
        {
            return await _connection.GetDatabase().StringGetAsync(redisKeys);
        }

        public async Task<bool> Set(string key, string value)
        {
            return await _connection.GetDatabase().StringSetAsync(key, value);
        }

        public async Task<bool> Set(KeyValuePair<RedisKey, RedisValue>[] redisPairs)
        {
            return await _connection.GetDatabase().StringSetAsync(redisPairs);
        }

        public async Task<bool> Delete(string key)
        {
            return await _connection.GetDatabase().KeyDeleteAsync(key);
        }

        public async Task<long> Delete(RedisKey[] redisKeys)
        {
            return await _connection.GetDatabase().KeyDeleteAsync(redisKeys);
        }
    }
}
