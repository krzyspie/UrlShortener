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

        public void SaveShortUrl(string originUrl, string urlShortcut)
        {
            var db = _redisConnection.GetDatabase();

            db.StringSet(urlShortcut, originUrl, TimeSpan.FromMinutes(60));
        }

        public string GetShortUrl(string urlShortcut)
        {
            var db = _redisConnection.GetDatabase();

            var result = db.StringGet(urlShortcut);

            return result;
        }
    }
}
