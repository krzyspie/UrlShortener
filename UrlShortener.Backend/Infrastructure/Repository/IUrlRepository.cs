namespace Infrastructure.Repository
{
    public interface IUrlRepository
    {
        void SaveShortUrl(string originUrl, string urlShortcut);

        string GetShortUrl(string urlShortcut);
    }
}