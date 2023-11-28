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

        public string CreateShortUrl(string url)
        {
            var db = _redisConnection.GetDatabase();

            var result = db.StringSetAndGet("url", url);

            return result;
        }
    }
}
