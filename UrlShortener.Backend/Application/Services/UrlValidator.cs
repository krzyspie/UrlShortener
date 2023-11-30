using Application.Interfaces;

namespace Application.Services
{
    public class UrlValidator : IUrlValidator
    {
        public bool IsValid(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
