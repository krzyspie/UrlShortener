namespace Infrastructure.Repository
{
    public interface IUrlRepository
    {
        string CreateShortUrl(string url);
    }
}