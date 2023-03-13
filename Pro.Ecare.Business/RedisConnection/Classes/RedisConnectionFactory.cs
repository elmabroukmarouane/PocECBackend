using Pro.Ecare.Business.RedisConnection.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.RedisConnection.Classes
{
    public class RedisConnectionFactory : IRedisConnectionFactory
    {
        private readonly Lazy<ConnectionMultiplexer> _connectionMultiplexer;

        public RedisConnectionFactory(string connectionString)
        {
            _connectionMultiplexer = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(connectionString));
        }

        public ConnectionMultiplexer GetConnection() => _connectionMultiplexer.Value;
    }
}
