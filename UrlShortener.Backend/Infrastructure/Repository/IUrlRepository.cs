namespace Infrastructure.Repository
{
    public interface IUrlRepository
    {
        string SaveShortUrl(string originUrl, string urlShortcut);

        string GetShortUrl(string originUrl);
    }
}