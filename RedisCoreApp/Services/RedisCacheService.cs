using StackExchange.Redis;

namespace RedisCoreApp.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _database;
        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _database = redis.GetDatabase();
        }
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _database.StringSetAsync(key, value, expiry);
        }
        public async Task<string?> GetAsync(string key)
        {
            return await _database.StringGetAsync(key);
        }
        public async Task RemoveAsync(string key)
        {
            await _database.KeyDeleteAsync(key);
        }
    }
}
