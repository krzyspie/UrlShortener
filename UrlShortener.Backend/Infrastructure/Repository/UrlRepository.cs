using StackExchange.Redis;

namespace Infrastructure.Repository
{
    public class UrlRepository : IUrlRepository
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public UrlRepository(IConnectionMultiplexer connectionMultiplexer)
        {
            _redisConnection = connectionMultiplexer;
        }

        public async Task SaveShortUrl(string originUrl, string urlShortcut)
        {
            var db = _redisConnection.GetDatabase();

            await db.StringSetAsync(urlShortcut, originUrl, TimeSpan.FromMinutes(60));
        }

        public async Task<string> GetShortUrl(string urlShortcut)
        {
            var db = _redisConnection.GetDatabase();

            var result = await db.StringGetAsync(urlShortcut);

            return result;
        }
    }
}
