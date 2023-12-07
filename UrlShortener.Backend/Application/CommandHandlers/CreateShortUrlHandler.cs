using Application.Commands;
using Application.Interfaces;
using Infrastructure.Repository;
using MediatR;

namespace Application.CommandHandlers
{
    internal class CreateShortUrlHandler : IRequestHandler<CreateShortUrl, string>
    {
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly IUrlRepository _urlRepository;

        public CreateShortUrlHandler(IRandomStringGenerator randomStringGenerator, IUrlRepository urlRepository)
        {
            _randomStringGenerator = randomStringGenerator;
            _urlRepository = urlRepository;
        }

        public async Task<string> Handle(CreateShortUrl request, CancellationToken cancellationToken)
        {
            string existingShortUrl = await _urlRepository.GetShortUrl(request.OriginUrl);
            if (!string.IsNullOrWhiteSpace(existingShortUrl))
            {
                return existingShortUrl;
            }

            string shortUrl = _randomStringGenerator.Generate();
            
            await _urlRepository.SaveShortUrl(request.OriginUrl, shortUrl);

            return shortUrl;
        }
    }
}
