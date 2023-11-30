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

        public string SaveShortUrl(string originUrl, string urlShortcut)
        {
            var db = _redisConnection.GetDatabase();

            var result = db.StringSetAndGet(originUrl, urlShortcut);

            return result;
        }

        public string GetShortUrl(string originUrl)
        {
            var db = _redisConnection.GetDatabase();

            var result = db.StringGet(originUrl);

            return result;
        }
    }
}
