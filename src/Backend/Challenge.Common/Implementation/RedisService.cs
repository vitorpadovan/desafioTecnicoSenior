using Challenge.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Challenge.Common.Implementation
{
    public class RedisService : ICacheService
    {
        private IConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly ILogger<RedisService> _logger;
        private readonly IConfiguration _configuration;

        public RedisService(ILogger<RedisService> logger, IConfiguration configuration, IConnectionMultiplexer connectionMultiplexer)
        {
            _logger = logger;
            _configuration = configuration;
            this._redis = connectionMultiplexer;
            this._database = this._redis.GetDatabase();
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            _logger.LogDebug("Getting info from key: {Key}", key);
            string? value = await this._database.StringGetAsync(key);
            if (value == null)
            {
                return default!;
            }
            return JsonSerializer.Deserialize<T>(value)!;
        }

        public async Task SetValueAsync<T>(string key, T value, int ttl = 28000)
        {
            _logger.LogDebug("Setting cache key: {Key} with ttl {Ttl}", key, ttl);
            await this._database.StringSetAsync(key, JsonSerializer.Serialize(value), TimeSpan.FromSeconds(ttl));
        }

        public async Task<int> AddCounter(string key, int quantity = 1)
        {
            _logger.LogDebug("Incrementing counter for key: {Key} by {Quantity}", key, quantity);
            return (int) await this._database.StringIncrementAsync(key, quantity);
        }

        public async Task DeleteCounter(string key)
        {
            _logger.LogDebug("Deleting counter for key: {Key}", key);
            await this._database.KeyDeleteAsync(key);
        }
    }
}
