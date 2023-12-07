namespace Infrastructure.Repository
{
    public interface IUrlRepository
    {
        Task SaveShortUrl(string originUrl, string urlShortcut);

        Task<string> GetShortUrl(string urlShortcut);
    }
}