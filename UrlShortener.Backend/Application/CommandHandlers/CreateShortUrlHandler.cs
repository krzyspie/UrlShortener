using Application.Commands;
using Application.Interfaces;
using MediatR;

namespace Application.CommandHandlers
{
    public class CreateShortUrlHandler : IRequestHandler<CreateShortUrl, string>
    {
        private readonly IRandomStringGenerator _randomStringGenerator;

        public CreateShortUrlHandler(IRandomStringGenerator randomStringGenerator)
        {
            _randomStringGenerator = randomStringGenerator;
        }

        public Task<string> Handle(CreateShortUrl request, CancellationToken cancellationToken)
        {
            string shortUrl = _randomStringGenerator.Generate();

            return Task.FromResult(shortUrl);
        }
    }
}
