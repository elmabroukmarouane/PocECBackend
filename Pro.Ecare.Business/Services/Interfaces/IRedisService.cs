using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pro.Ecare.Business.Services.Interfaces
{
    public interface IRedisService
    {
        Task<bool> Set(string key, string value);
        Task<string> Get(string key);
        Task<bool> Delete(string key);
        Task<long> Delete(RedisKey[] redisKeys);
    }
}
