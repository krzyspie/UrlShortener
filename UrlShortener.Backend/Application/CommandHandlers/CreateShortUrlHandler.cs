using Application.Commands;
using Application.Interfaces;
using Infrastructure.Repository;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateShortUrlHandler : IRequestHandler<CreateShortUrl, string>
    {
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly IUrlRepository _urlRepository;

        public CreateShortUrlHandler(IRandomStringGenerator randomStringGenerator, IUrlRepository urlRepository)
        {
            _randomStringGenerator = randomStringGenerator;
            _urlRepository = urlRepository;
        }

        public Task<string> Handle(CreateShortUrl request, CancellationToken cancellationToken)
        {
            string shortUrl = _randomStringGenerator.Generate();
            var result = _urlRepository.SaveShortUrl(request.OriginUrl, shortUrl);
            return Task.FromResult(result);
        }
    }
}
